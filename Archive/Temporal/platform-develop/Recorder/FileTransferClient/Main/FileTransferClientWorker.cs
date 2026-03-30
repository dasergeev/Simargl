using Apeiron.Support;

namespace Apeiron.Services.FileTransfer;

/// <summary>
/// Представляет основной фоновый процесс службы передачи файлов.
/// </summary>
public class FileTransferClientWorker :
    BackgroundService
{
    /// <summary>
    /// Поле для хранения конфигурации службы.
    /// </summary>
    private readonly IConfiguration _Configuration;

    /// <summary>
    /// Поле для хранения средства записи в журнал службы.
    /// </summary>
    private readonly ILogger<FileTransferClientWorker> _Logger;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="configuration">
    /// Конфигурация службы.
    /// </param>
    /// <param name="logger">
    /// Средство записи в журнал.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="configuration"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logger"/> передана пустая ссылка.
    /// </exception>
    public FileTransferClientWorker(IConfiguration configuration, ILogger<FileTransferClientWorker> logger)
    {
        //  Установка конфигурации службы.
        _Configuration = Check.IsNotNull(configuration, nameof(configuration));

        //  Установка средства записи в журнал службы.
        _Logger = Check.IsNotNull(logger, nameof(logger));
    }

    /// <summary>
    /// Ассинхронно выполняет фоновую работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая фоновую работу.
    /// </returns>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        // Задержка для инициализации консоли и выдачи служебных сообщений.
        await Task.Delay(50, cancellationToken).ConfigureAwait(false);

        //  Загрузка логики работы.
        ClientLogic logic = _Configuration.GetSection("Logic").Get<ClientLogic>()!;

        //  Блок перехвата исключений ошибки данных.
        try
        {
            //  Проверка логики работы.
            await logic.CheckValidityAsync(_Logger, cancellationToken).ConfigureAwait(false);
        }
        catch (InvalidDataException ex)
        {
            //  Вывод информации в журнал.
            _Logger.LogError("{exception}", ex.Message);

            //  Завершение работы службы.
            return;
        }

        //  Проверка ссылки на правила.
        if (logic.Rules is null)
        {
            //  Недопустимый формат данных.
            throw new InvalidDataException("Не заданы правила.");
        }

        //  Выполнение работы правил.
        await logic.Rules.InvokeAsync(_Logger, logic, cancellationToken).ConfigureAwait(false);
    }
}
