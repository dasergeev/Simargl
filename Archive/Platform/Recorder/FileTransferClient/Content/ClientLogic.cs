using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Simargl.Platform.Recorder.FileTransferClient.Content;

/// <summary>
/// Представляет логику работы клиента.
/// </summary>
public class ClientLogic
{
    /// <summary>
    /// Возвращает или задаёт информацию о подключении к серверу.
    /// </summary>
    public ClientConnection? Connection { get; init; }

    /// <summary>
    /// Возвращает коллекцию правил.
    /// </summary>
    public ClientRuleCollection? Rules { get; init; }

    /// <summary>
    /// Асинхронно проверяет логику.
    /// </summary>
    /// <param name="logger">
    /// Средство записи в журнал службы.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая проверку логики.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logger"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Недопустимый формат данных.
    /// </exception>
    public async Task CheckValidityAsync(ILogger logger, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на средство записи в журнал.
        logger = IsNotNull(logger, nameof(logger));

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание средства записи текстовой информации.
        using StringWriter writer = new();

        //  Вывод информации о проверке логики работы.
        await writer.WriteAsync("\nПроверка логики:\n".AsMemory(), cancellationToken).ConfigureAwait(false);

        //  Проверка установки информации о подключении к серверу.
        if (Connection is null)
        {
            //  Недопустимый формат данных.
            throw new InvalidDataException("Не задана информация о подключении.");
        }

        //  Проверка информации о подключении к серверу.
        await Connection.CheckValidityAsync(writer, 1, cancellationToken).ConfigureAwait(false);

        //  Проверка установки правил.
        if (Rules is null)
        {
            //  Недопустимый формат данных.
            throw new InvalidDataException("Не заданы правила.");
        }

        //  Проверка правил.
        await Rules.CheckValidityAsync(writer, 1, cancellationToken).ConfigureAwait(false);

        //  Вывод информации в журнал.
        logger.LogInformation("{message}", writer);
    }
}
