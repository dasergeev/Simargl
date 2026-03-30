using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Apeiron.Platform.Performers.OsmPerformers;

/// <summary>
/// Представляет исполнителя, выполняющего загрузку узлов OSM-карт (Nodes) в базу.
/// </summary>
public sealed class OsmNodeLoader : Performer
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="journal">
    /// Журнал.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="journal"/> передана пустая ссылка.
    /// </exception>
    public OsmNodeLoader(Journal journal) :
        base(journal)
    {
    }

    /// <summary>
    /// Асинхронно выполняет работу исполнителя.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу исполнителя.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public override sealed async Task PerformAsync(CancellationToken cancellationToken)
    {
        // Основной цикл.
        while (!cancellationToken.IsCancellationRequested)
        {
            // Доп. задержка, для вывода изначальных сообщений в консоль.
            await Task.Delay(50, cancellationToken).ConfigureAwait(false);

            await Journal.LogInformationAsync(
                "Запуск поиска узлов (Nodes) в файлах.",
                cancellationToken).ConfigureAwait(false);

            //  Запуск загрузки узлов Nodes из файлов данных.
            await LoadNodesFromFilesAsync(cancellationToken).ConfigureAwait(false);


            await Journal.LogInformationAsync(
                "Итерация загрузки данных в БД завершена.",
                cancellationToken).ConfigureAwait(false);

            // Доп. задержка.
            await Task.Delay(15000, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Ассинхронно выполняет поиск информации в файлах.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    private async Task LoadNodesFromFilesAsync(CancellationToken cancellationToken)
    {
        // Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        // Создание контекста сеанса работы с базой данных.
        using OsmDatabaseContext database = new();

        // Получение списка необработанных файлов и БД.
        var osmFileList = await database.OsmFiles
            .AsNoTracking()
            .Where(x => x.IsAnalyzed == false && x.IsNodesEmpty == false && x.IsNodesLoad == false)
            .Select(f => f)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        // Определение количества файлов.
        int fileCount = osmFileList.Count;

        if (fileCount > 0)
        {
            // Вывод информации в журнал.
            await Journal.LogInformationAsync(
                $"Найдено {fileCount} новых файлов для поиска данных.",
                cancellationToken).ConfigureAwait(false);

            // Перемещивание списка.
            //  Создание генератора случайных чисел.
            Random random = new(unchecked((int)(DateTime.Now.Ticks % int.MaxValue)));

            //  Изменение порядка временных интервалов.
            for (int i = 0; i < osmFileList.Count; i++)
            {
                //  Получение нового индекса.
                int index = random.Next() % osmFileList.Count;

                //  Перестановка элементов.
                (osmFileList[i], osmFileList[index]) = (osmFileList[index], osmFileList[i]);
            }


            // Запуск перебора файлов.
            await ParceElementsAsync(osmFileList, cancellationToken).ConfigureAwait(false);
        }
        else
        {
            // Вывод информации в журнал.
            await Journal.LogInformationAsync(
                $"Не найдено новых файлов удовлетворяющих условие поиска.",
                cancellationToken).ConfigureAwait(false);
        }
    }


    /// <summary>
    /// Парсит XML файл и добавляет элементы в таблицу БД.
    /// </summary>
    /// <param name="osmFileList">Список файлов.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <exception cref="ArgumentException">В качестве входящего параметра передана пустая ссылка.</exception>
    /// <exception cref="OperationCanceledException">Операция прервана по токену отмены.</exception>
    private async Task ParceElementsAsync(IEnumerable<OsmFile> osmFileList, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        // Проверка входящего параметра.
        Check.IsNotNull(osmFileList, nameof(osmFileList));

        //  Параллельный перебор всех путей общих каталогов.
        await Parallel.ForEachAsync(osmFileList, new ParallelOptions() { CancellationToken = cancellationToken },
            async (osmFile, cancellationToken) =>
            {
                //  Проверка токена отмены.
                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                //  Безопасное выполнение операции.
                await SafeCallAsync(async cancellationToken =>
                {
                    // Получаем спиок Nodes если они есть в файле.
                    var osmInfo = await GetNodesFromFileAsync(osmFile.Id, osmFile.FilePath, cancellationToken);

                    // Если список не пустой.
                    if (osmInfo.OsmNodeCollection is not null && osmInfo.OsmNodeCollection.Count > 0)
                    {
                        //  Вывод информации в журнал.
                        await Journal.LogInformationAsync(
                            $"Добавление из файла {osmFile.Id} новых узлов {osmInfo.OsmNodeCollection.Count} ",
                            cancellationToken).ConfigureAwait(false);

                        //  Создание контекста сеанса работы с базой данных.
                        using OsmDatabaseContext database = new();

                        //  Начало транзакции.
                        using var transaction = await database.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

                        //  Добавление в базу записи.
                        await database.OsmNodes.AddRangeAsync(osmInfo.OsmNodeCollection!, cancellationToken).ConfigureAwait(false);

                        if (osmInfo.OsmTagCollection is not null && osmInfo.OsmTagCollection.Count > 0)
                        {
                            //  Вывод информации в журнал.
                            await Journal.LogInformationAsync(
                            $"Добавление Tag из файла {osmFile.Id} новых меток {osmInfo.OsmTagCollection.Count} ",
                            cancellationToken).ConfigureAwait(false);
                            
                            // Добавление данных в базу.
                            await database.OsmNodeTags.AddRangeAsync(osmInfo.OsmTagCollection, cancellationToken).ConfigureAwait(false);                         
                        }

                        //  Сохранение изменений в базу данных.
                        int count = await database.SaveChangesAsync(cancellationToken);

                        //  Фиксирование изменений.
                        await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);

                        //  Установка флагов, что точки Nodes и описание Tags загружены для данного файла.
                        await OsmFileTableUpdateAsync(osmFile, OsmFileStatus.IsNodesLoad, cancellationToken).ConfigureAwait(false);
                       
                        if (osmInfo?.OsmTagCollection?.Count > 0)
                            await OsmFileTableUpdateAsync(osmFile, OsmFileStatus.IsNodeTagsLoad, cancellationToken).ConfigureAwait(false);
                    }
                    else
                    {
                        //  Вывод информации в журнал.
                        await Journal.LogInformationAsync(
                            $"Файл {osmFile.FilePath} не содержит узлов!",
                            cancellationToken).ConfigureAwait(false);

                        //  Установка флагов, что точек Nodes нет в файле.
                        await OsmFileTableUpdateAsync(osmFile, OsmFileStatus.IsNodesEmpty, cancellationToken).ConfigureAwait(false);
                    }
                }, cancellationToken).ConfigureAwait(false);
            }).ConfigureAwait(false);
    }

    /// <summary>
    /// Обновляет информацию о файле в БД.
    /// </summary>
    /// <param name="osmFile">Файл информацию в котором необходимо обновить.</param>
    /// <param name="status">Поле которое необходимо обновить.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static async Task OsmFileTableUpdateAsync(OsmFile osmFile, OsmFileStatus status, CancellationToken cancellationToken)
    {
        //  Создание контекста сеанса работы с базой данных.
        using OsmDatabaseContext database = new();

        //  Начало транзакции.
        using var transaction = await database.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

        // Установка флага загрузки данных.
        switch (status)
        {
            case OsmFileStatus.IsNodesEmpty:
                osmFile.IsNodesEmpty = true;
                break;
            case OsmFileStatus.IsNodesLoad:
                osmFile.IsNodesLoad = true;
                break;
            case OsmFileStatus.IsNodeTagsLoad:
                osmFile.IsNodeTagsLoad = true;
                break;
            case OsmFileStatus.IsWaysEmpty:
                osmFile.IsWaysEmpty = true;
                break;
            case OsmFileStatus.IsWaysLoad:
                osmFile.IsWaysLoad = true;
                break;
            case OsmFileStatus.IsWayTagsLoad:
                osmFile.IsWayTagsLoad = true;
                break;
            default:
                break;
        }

        //  Установка состояния изменения записи о файле.
        database.Entry(osmFile).State = EntityState.Modified;

        //  Обновление записи в БД.
        database.OsmFiles.Update(osmFile);

        //  Сохранение изменений в базу данных.
        int count = await database.SaveChangesAsync(cancellationToken);

        //  Фиксирование изменений.
        await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Парсит XML файл на наличие узлов с именем Node и формирует список с полезной информацией.
    /// </summary>
    /// <param name="fileId">ID файла.</param>
    /// <param name="filePath">Путь к файлу.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список с найденными данными.</returns>
    private static async Task<OsmInfo> GetNodesFromFileAsync(long fileId, string filePath, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        // Представление XML документа.
        XmlDocument xDoc = new(); 

        // Загружаем файл xml.
        xDoc.Load(filePath);

        // Возвращает все дочерние узлы данного узла
        var nodeList = xDoc?.DocumentElement?.ChildNodes.Cast<XmlNode>();

        // Проверяем, что есть элементы в xml.
        if (nodeList is null)
            return new OsmInfo();

        // Создание класса, хранящего полезную информацию.
        var osmInfo = new OsmInfo();
        
        // Обход файла XML и выборка информации по Nodes узлам.
        Parallel.ForEach(nodeList, xNode =>
        {
            if (xNode.Name == "node")
            {
                // получаем атрибуты
                var idAttr = xNode?.Attributes?.GetNamedItem("id")?.Value;
                var latAttr = xNode?.Attributes?.GetNamedItem("lat")?.Value;
                var lonAttr = xNode?.Attributes?.GetNamedItem("lon")?.Value;

                if (idAttr is not null && latAttr is not null && lonAttr is not null)
                {
                    osmInfo.OsmNodeCollection?.Add(new OsmNode()
                    {
                        Id = long.Parse(idAttr),
                        OsmFileId = fileId,
                        Latitude = double.Parse(latAttr, CultureInfo.InvariantCulture),
                        Longitude = double.Parse(lonAttr, CultureInfo.InvariantCulture)
                    });
                }

                // Получаем вложенные элементы в Node.
                var xNodeChildList = xNode?.ChildNodes.Cast<XmlNode>();

                if (xNodeChildList is not null && xNodeChildList.Any())
                {
                    // Обход файла XML и выборка информации по Tag узлам.
                    Parallel.ForEach(xNodeChildList, xChild =>
                    {
                        if (xChild.Name == "tag")
                        {
                            // получаем атрибуты
                            var kAttr = xChild?.Attributes?.GetNamedItem("k")?.Value;
                            var vAttr = xChild?.Attributes?.GetNamedItem("v")?.Value;

                            if (kAttr is not null && vAttr is not null)
                            {
                                osmInfo.OsmTagCollection?.Add(new OsmNodeTag()
                                {
                                    OsmNodeId = long.Parse(idAttr!),
                                    Key = kAttr,
                                    Value = vAttr
                                });
                            }
                        }
                    });
                }
            }
        });

        return osmInfo;
    }

    ///// <summary>
    ///// Поиск и формирование Tag элементов.
    ///// </summary>
    ///// <param name="xmlParentNode">Елемент родитель, содержащий Tag элементы.</param>
    ///// <exception cref="ArgumentException">В качестве входящего параметра передана пустая ссылка.</exception>
    ///// <returns>Возвращает коллекцию Tag элементов.</returns>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //private static BlockingCollection<GenericTag> GetGenericTags(XmlNode xmlParentNode)
    //{
    //    // Проверка входящего параметра.
    //    Check.IsNotNull(xmlParentNode, nameof(xmlParentNode));

    //    // Получаем вложенные элементы в Node.
    //    IEnumerable<XmlNode> elementsXmlList = xmlParentNode.ChildNodes.Cast<XmlNode>();

    //    // Если у родительского элемента нет дочерних, то возвращается пустая коллекция.
    //    if (!elementsXmlList.Any())
    //        return new BlockingCollection<GenericTag>();

    //    // Id родительского элемента для связи таблиц.
    //   var idAttr = xmlParentNode.Attributes?.GetNamedItem("id")?.Value;

    //    // Коллекция Tag элементов.
    //    BlockingCollection<GenericTag> resultTagCollection = new();

    //    // Обход файла XML и выборка информации по Tag узлам.
    //    Parallel.ForEach(elementsXmlList, xElement =>
    //    {
    //        if (xElement?.Name == "tag")
    //        {
    //            // Получаем атрибуты
    //            var kAttr = xElement?.Attributes?.GetNamedItem("k")?.Value;
    //            var vAttr = xElement?.Attributes?.GetNamedItem("v")?.Value;

    //            if (kAttr is not null && vAttr is not null)
    //            {
    //                resultTagCollection.Add(new GenericTag()
    //                {
    //                    ParentId = long.Parse(idAttr!),
    //                    Key = kAttr,
    //                    Value = vAttr
    //                });
    //            }
    //        }
    //    });

    //    // Возвращаем полученную коллекцию.
    //    return resultTagCollection;
    //}
}