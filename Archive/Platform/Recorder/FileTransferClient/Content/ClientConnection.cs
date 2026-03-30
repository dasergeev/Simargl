using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Simargl.Platform.Recorder.FileTransferClient.Content;

/// <summary>
/// Представляет информацию о подключении к серверу.
/// </summary>
public class ClientConnection
{
    /// <summary>
    /// Возвращает идентификатор подключений.
    /// </summary>
    public int? Identifier { get; init; }

    /// <summary>
    /// Возвращает имя домена для подключения.
    /// </summary>
    public string? Domain { get; init; }

    /// <summary>
    /// Возвращает номер порта для подключения.
    /// </summary>
    public int? Port { get; init; }

    /// <summary>
    /// Асинхронно выполняет проверку данных.
    /// </summary>
    /// <param name="writer">
    /// Средство записи текстовой информации.
    /// </param>
    /// <param name="level">
    /// Уровень вложенности.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая проверку данных.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="writer"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="level"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Недопустимый формат данных.
    /// </exception>
    public async Task CheckValidityAsync(TextWriter writer, int level, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на средство записи в журнал.
        writer = IsNotNull(writer, nameof(writer));

        //  Проверка уровня вложенности.
        level = IsNotNegative(level, nameof(level));

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Формирование отступа.
        string indent = new(' ', 2 * level);

        //  Вывод информации о проверке информации о подключении к серверу.
        await writer.WriteAsync($"{indent}Подключение к серверу:\n".AsMemory(), cancellationToken).ConfigureAwait(false);

        //  Проверка установки идентификатора подключения.
        if (Identifier is null)
        {
            //  Недопустимый формат данных.
            throw new InvalidDataException("Не задан идентификатор подключения.");
        }

        //  Проверка значения идентификатора подключения.
        if (Identifier < 0)
        {
            //  Недопустимый формат данных.
            throw new InvalidDataException("Отрицательный идентификатор подключения.");
        }

        //  Вывод информации о проверке идентификатора.
        await writer.WriteAsync($"{indent}  Идентификатор: {Identifier}\n".AsMemory(), cancellationToken).ConfigureAwait(false);

        //  Проверка имени домена.
        if (Domain is null)
        {
            //  Недопустимый формат данных.
            throw new InvalidDataException("Не задано имя домена для подключения.");
        }

        //  Вывод информации о имени домена.
        await writer.WriteAsync($"{indent}  Имя домена: \"{Domain}\"\n".AsMemory(), cancellationToken).ConfigureAwait(false);

        //  Проверка установки порта для подключения.
        if (Port is null)
        {
            //  Недопустимый формат данных.
            throw new InvalidDataException("Не задан порт для подключения.");
        }

        //  Проверка значения идентификатора подключения.
        if (Port < IPEndPoint.MinPort || Port > IPEndPoint.MaxPort)
        {
            //  Недопустимый формат данных.
            throw new InvalidDataException("Не корректный порт для подключения.");
        }

        //  Вывод информации о порте для подключения.
        await writer.WriteAsync($"{indent}  Порт: {Port}\n".AsMemory(), cancellationToken).ConfigureAwait(false);
    }
}
