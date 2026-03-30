namespace Apeiron.Platform.Performers.OsmPerformers;

/// <summary>
/// Представляет исполнителя, выполняющего загрузку линий OSM-карт (Ways) в базу.
/// </summary>
public class OsmWayLoader : Performer
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
    public OsmWayLoader(Journal journal) : base(journal)
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

            //  Запуск загрузки узлов Nodes из файлов данных.
            await LoadWaysFromFilesAsync(cancellationToken).ConfigureAwait(false);

            // Доп. задержка.
            await Task.Delay(15000, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Ассинхронно выполняет поиск информации в файлах.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    private async Task LoadWaysFromFilesAsync(CancellationToken cancellationToken)
    {
        // Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        // Создание контекста сеанса работы с базой данных.
        using OsmDatabaseContext database = new();

        // Получение списка необработанных файлов и БД.
        var osmFileList = await database.OsmFiles
            .AsNoTracking()
            .Where(x => x.IsAnalyzed == false && x.IsWaysEmpty && x.IsWaysLoad == false)
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
    private static async Task ParceElementsAsync(IEnumerable<OsmFile> osmFileList, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        // Проверка входящего параметра.
        Check.IsNotNull(osmFileList, nameof(osmFileList));


    }
}