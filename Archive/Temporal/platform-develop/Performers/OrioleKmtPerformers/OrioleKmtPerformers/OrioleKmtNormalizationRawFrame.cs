using Apeiron.Frames;

namespace Apeiron.Platform.Performers.OrioleKmtPerformers;

/// <summary>
/// Представляет класс для реализации нормализации файлов RawFrame.
/// </summary>
public sealed class OrioleKmtNormalizationRawFrame : Performer
{
    //// Путь к папке с файлами Oriole KMT.
    //private const string OrioleKmtPath = @"\\railtest.ru\Data\06-НТО\RawData\Oriole\KMT";

    // Путь к папке с обработанными файлами Oriole KMT.
    private const string _OrioleKmtPathRecords = @"\\railtest.ru\Data\06-НТО\Records\2021\0021";

    //// Имена каналов которые необходимо удалить.
    //private readonly string[] _ChannelNameForDelete = new[] { "CB_CH_15", "CB_CH_16", "KMT_CH_05", "KMT_CH_06", "KMT_CH_07", 
    //                                                         "KMT_CH_08", "KMT_CH_09", "KMT_CH_10", "KMT_CH_11", "KMT_CH_12",
    //                                                         "KMT_CH_13", "KMT_CH_14", "KMT_CH_15", "KMT_CH_16" };

    private readonly List<string> _ChannelNameListForOrder = new()
    {
        { "Uxb1" },
        { "Uyb1" },
        { "Uzb1" },
        { "Uxr" },
        { "Uyr" },
        { "Uzr" },
        { "Uxk1" },
        { "Uyk1" },
        { "Uzk1" },
        { "Uxk2" },
        { "Uyk2" },
        { "Uzk2" },
        { "Nk1" },
        { "Nk2" },
        { "Mr" },
        { "Mo" },
        { "Mir1" },
        { "Mir2" },
        { "V_GPS" },
        { "Lon_GPS" },
        { "Lat_GPS" }
    };

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="journal">
    /// Журнал.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="journal"/> передана пустая ссылка.
    /// </exception>
    public OrioleKmtNormalizationRawFrame(Journal journal) :
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
        //  Основной цикл.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Доп. задержка, для вывода изначальных сообщений в консоль.
            await Task.Delay(50, cancellationToken).ConfigureAwait(false);

            await Journal.LogInformationAsync($"Поиск кадров", cancellationToken).ConfigureAwait(false);

            // Поиск и нормализация каналов.
            await SearchChannalNamesAsync(cancellationToken).ConfigureAwait(false);

            //  Доп. задержка.
            await Task.Delay(30000, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет поиск информации в кадрах
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Задача, выполняющая поиск.</returns>
    /// <exception cref="OperationCanceledException">Операция отменена.</exception>
    private async Task SearchChannalNamesAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Безопасное выполнение операции.
        await SafeCallAsync(async cancellationToken =>
        {

            //  Создание контекста сеанса работы с базой данных.
            using OrioleKmtDatabaseContext database = new();

            // Получение списка необработанных файлов и БД.
            var rawFrameFileList = await database.RawFrames
                .AsNoTracking()
                .Where(x => x.IsAnalyzed == false)
                .Select(y => y)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            //  Определение количества файлов.
            int fileCount = rawFrameFileList.Count;

            //  Вывод информации в журнал.
            await Journal.LogInformationAsync(
                $"Найдено {fileCount} новых файлов для поиска кадров. ",
                cancellationToken).ConfigureAwait(false);

            // Если есть файлы.
            if (fileCount > 0)
            {
                // Перемещивание списка.
                //  Создание генератора случайных чисел.
                Random random = new(unchecked((int)(DateTime.Now.Ticks % int.MaxValue)));

                //  Изменение порядка временных интервалов.
                for (int i = 0; i < rawFrameFileList.Count; i++)
                {
                    //  Получение нового индекса.
                    int index = random.Next() % rawFrameFileList.Count;

                    //  Перестановка элементов.
                    (rawFrameFileList[i], rawFrameFileList[index]) = (rawFrameFileList[index], rawFrameFileList[i]);
                }

                //  Асинхронное добавление информации о новых файлах в базу данных.
                await Parallel.ForEachAsync(rawFrameFileList, new ParallelOptions() { CancellationToken = cancellationToken },
                    async (rawFrame, cancellationToken) =>
                    {
                        //  Проверка токена отмены.
                        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                        //  Безопасное выполнение операции.
                        await SafeCallAsync(async cancellationToken =>
                        {
                            // Создание контекста сеанса работы с базой данных.
                            using OrioleKmtDatabaseContext database = new();

                            //// Проверка, что текущий файл ещё не обработан.
                            //var rawFrameCheck = await database.RawFrames
                            //    .AsNoTracking()
                            //    .Where(x => x.Id == rawFrame.Id && x.IsAnalyzed == true)
                            //    .Select(y => y)
                            //    .ToListAsync(cancellationToken)
                            //    .ConfigureAwait(false);

                            //if (rawFrameCheck.Count > 0)
                            //{
                            //    return;
                            //}

                            // Вывод информации в журнал.
                            await Journal.LogInformationAsync(
                            $"Обрабатывается файл {rawFrame.FilePath}",
                            cancellationToken).ConfigureAwait(false);

                            // Создание фрейма для работы с ним.
                            Frame newFrame = new(rawFrame.FilePath);

                            // Поиск набора каналов с учётом периода вхождения.
                            var channelRenameSet = await database.TimeChunks
                                .AsNoTracking()
                                //.Where(x => x.BeginTime <= rawFrame.Time && x.EndTime > rawFrame.Time)
                                .Where(x => x.BeginTime <= rawFrame.BeginTime && x.EndTime > rawFrame.BeginTime)
                                .Select(y => y)
                                .Include(c => c.ChannelNames)
                                .ToListAsync(cancellationToken)
                                .ConfigureAwait(false);

                            //  Начало транзакции.
                            using var transaction = await database.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

                            // Переименовывание каналов.
                            foreach (var itemSet in channelRenameSet)
                            {
                                // Цикл по каналам в кадре.
                                for (int i = 0; i < newFrame.Channels.Count; i++)
                                {
                                    //  Проверка токена отмены.
                                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                                    // Находит имя канала по индексу в наборе имён каналов и присваеет.
                                    newFrame.Channels[i].Name = itemSet.ChannelNames.Where(x => x.Index == i).Select(x => x.Name).First();
                                }
                            }


                            // Добавление в словарь каналов после переименовывания и удаления.
                            var resultChannelDictionary = newFrame.Channels.ToDictionary(x => x.Name.ToLower(), x => x);
                            // Очистка кадра.
                            newFrame.Channels.Clear();

                            // Сортировка каналов внутри кадра.
                            foreach (var trueChannelName in _ChannelNameListForOrder)
                            {

                                Channel channel = resultChannelDictionary[trueChannelName.ToLower()];
                                channel.Name = trueChannelName;

                                newFrame.Channels.Add(channel);
                            }

                            // Функция добавления данных в кадр.
                            //CurvatureAndRegimes.AddCurvatureAndRegimesKMT(newFrame);

                            // Сохранение кадра.
                            //var newFrameFileName = await SaveFrameAsync(newFrame, rawFrame.Time, cancellationToken);
                            var newFrameFileName = await SaveFrameAsync(newFrame, rawFrame.BeginTime, cancellationToken);

                            // Добавление данных об обработанном кадре в таблицу обработанных кадров.
                            await database.AddAsync(
                                new AnalyzedFrame()
                                {
                                    FilePath = newFrameFileName
                                }, cancellationToken)
                                .ConfigureAwait(false);

                            // Установка флага об обработки кадра.
                            rawFrame.IsAnalyzed = true;
                            database.Entry(rawFrame).State = EntityState.Modified;

                            //  Сохранение изменений в базу данных.
                            int count = await database.SaveChangesAsync(cancellationToken);

                            //  Фиксирование изменений.
                            await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);

                        }, cancellationToken).ConfigureAwait(false);

                    }).ConfigureAwait(false);
            }
            else
            {
                //  Вывод информации в журнал.
                await Journal.LogWarningAsync(
                    $"Файлы для обработки не найдены!",
                    cancellationToken).ConfigureAwait(false);
            }
        }, cancellationToken).ConfigureAwait(false);

        //  Вывод информации в журнал.
        await Journal.LogWarningAsync(
            $"Обработка завершена!",
            cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Сохранение сформированного кадра.
    /// </summary>
    /// <param name="frame">Кадр.</param>
    /// <param name="dateTimeFrame">Время изменения кадра.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    private async Task<string> SaveFrameAsync(Frame frame, DateTime dateTimeFrame, CancellationToken cancellationToken)
    {
        // Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        // Проверка входящего параметра.
        Check.IsNotNull(frame, nameof(frame));

        // Формируем полный путь к каталогу с кадром.
        string fullDirectoryPath = Path.Combine(_OrioleKmtPathRecords, $"{dateTimeFrame:yyyy-MM-dd}");

        // Создаём путь к каталогу если он не существует.
        DirectoryInfo dirInfo = new(fullDirectoryPath);
        if (!dirInfo.Exists)
            dirInfo.Create();

        // Формируем расширение файла.
        int fileExtension = (int)dateTimeFrame.TimeOfDay.TotalMinutes + 1;

        // Формируем имя файла.
        var fileName = $"Vp{frame.Channels["V_GPS"].Average():000}_0.{fileExtension:0000}";

        // Полный путь и имя файла.
        var fullFileName = Path.Combine(fullDirectoryPath, fileName);

        // Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        // Сохранение обработанного кадра регистрации.
        frame.Save(fullFileName, StorageFormat.TestLab);

        //  Вывод информации в журнал.
        await Journal.LogInformationAsync(
            $"Сохранение {fullFileName} файла. ",
            cancellationToken).ConfigureAwait(false);

        return fullFileName;
    }

}

