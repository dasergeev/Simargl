namespace Apeiron.Platform.Testing;

/// <summary>
/// Представляет тестовую службу.
/// </summary>
public class CommonTester :
    BackgroundService
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="logger">
    /// Средство записи в журнал службы.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logger"/> передана пустая ссылка.
    /// </exception>
    public CommonTester(ILogger<CommonTester> logger)
    {
        //  Установка средства записи в журнал службы.
        Logger = Check.IsNotNull(logger, nameof(logger));
    }

    /// <summary>
    /// Возвращает средство записи в журнал службы.
    /// </summary>
    protected ILogger<CommonTester> Logger { get; }

    /// <summary>
    /// Асинхронно выполняет основную работу службы.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу службы.
    /// </returns>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Блока перехвата всех исключений.
        try
        {

            //var d = await Apeiron.Platform.Databases.CentralDatabase.CentralDatabaseAgent.FileSystem.GetDirectoryAsync(
            //    @"\\railtest.ru\Data\06-НТО\RawData\FileTransferTest\2022", cancellationToken).ConfigureAwait(false);

            //Logger.LogInformation("{message}", $"{d.Id}: {d.Path}");

            ////  Создание посторителя ответа для вывода.
            //StringBuilder output = new();

            //void write(string text) => output.Append(text);
            //void writeLine(string text) => write(text + Environment.NewLine);
            //writeLine(string.Empty);


            //await FileStorage.CreateAsync($"Storage[{DateTime.Now}]", cancellationToken);

            //IEnumerable<FileStorage> fileStorages =
            //    (await FileStorage.GetAllAsync(cancellationToken).ConfigureAwait(false))
            //    .OrderBy(fileStorage => fileStorage.Id);

            //foreach (FileStorage fileStorage in fileStorages)
            //{
            //    writeLine($"{fileStorage.Id}: {fileStorage.Name}");
            //}
            //writeLine(string.Empty);

            //long minId = fileStorages.Select(fileStorage => fileStorage.Id).Where(id => id > 5).Min();
            //long maxId = fileStorages.Select(fileStorage => fileStorage.Id).Max();

            //for (long id = 1; id <= maxId; id++)
            //{
            //    try
            //    {
            //        FileStorage fileStorage = await FileStorage.FromIdAsync(id, cancellationToken).ConfigureAwait(false);
            //        writeLine($"{fileStorage.Id}: {fileStorage.Name}");
            //    }
            //    catch (Exception ex)
            //    {
            //        writeLine($"{id}: {ex.Message}");
            //    }
            //}
            //writeLine(string.Empty);

            //{
            //    FileStorage fileStorage = await FileStorage
            //        .FromNameAsync("Storage-2", cancellationToken).ConfigureAwait(false);
            //    writeLine($"{fileStorage.Id}: {fileStorage.Name}");
            //}
            //writeLine(string.Empty);

            //FileStorage fileStorageFirst = await FileStorage.FromIdAsync(5, cancellationToken).ConfigureAwait(false);
            //FileStorage fileStorageSecond = await FileStorage.FromIdAsync(5, cancellationToken).ConfigureAwait(false);

            //writeLine($"fileStorageFirst {fileStorageFirst.Id}: {fileStorageFirst.Name}");
            //writeLine($"fileStorageSecond {fileStorageSecond.Id}: {fileStorageSecond.Name}");
            //writeLine(string.Empty);

            //await fileStorageFirst.RenameAsync($"Storage[{DateTime.Now}]", cancellationToken).ConfigureAwait(false);
            //await fileStorageSecond.UpdateAsync(cancellationToken).ConfigureAwait(false);

            //writeLine($"fileStorageFirst {fileStorageFirst.Id}: {fileStorageFirst.Name}");
            //writeLine($"fileStorageSecond {fileStorageSecond.Id}: {fileStorageSecond.Name}");
            //writeLine(string.Empty);

            //{
            //    FileStorage fileStorage = await FileStorage
            //        .FromIdAsync(minId, cancellationToken).ConfigureAwait(false);
            //    await fileStorage.RemoveAsync(cancellationToken).ConfigureAwait(false);
            //}

            //fileStorages =
            //    (await FileStorage.GetAllAsync(cancellationToken).ConfigureAwait(false))
            //    .OrderBy(fileStorage => fileStorage.Id);

            //foreach (FileStorage fileStorage in fileStorages)
            //{
            //    writeLine($"{fileStorage.Id}: {fileStorage.Name}");
            //}
            //writeLine(string.Empty);

            ////  Вывод информации в журнал.
            //Logger.LogInformation("{message}", output);
        }
        catch (Exception ex)
        {
            //  Проверка запроса отмены.
            if (!cancellationToken.IsCancellationRequested)
            {
                //  Добавление записи в журнал с описанием ошибки.
                Logger.LogError("{exception}", ex);
            }
        }
    }
}
