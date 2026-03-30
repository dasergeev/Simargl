using Simargl.IO;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Simargl.Synergy.Transferring;

/// <summary>
/// Представляет сообщение.
/// </summary>
public abstract class Message
{
    /// <summary>
    /// Постоянная, определяющая сигнатуру сообщения.
    /// </summary>
    private const ulong _Signature = 0x3BB4F2A358444BB6UL;

    /// <summary>
    /// Постоянная, определяющая текущую версию.
    /// </summary>
    private const int _CurrentVersion = 1;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="format">
    /// Значение, определяющее формат сообщения.
    /// </param>
    internal Message(MessageFormat format)
    {
        //  Установка значений свойств.
        Version = _CurrentVersion;
        Format = format;
    }

    /// <summary>
    /// Возвращает версию сообщения.
    /// </summary>
    internal int Version { get; }

    /// <summary>
    /// Возвращает значение, определяющее формат сообщения.
    /// </summary>
    internal MessageFormat Format { get; }

    /// <summary>
    /// Асинхронно сохраняет сообщение в поток.
    /// </summary>
    /// <param name="spreader">
    /// Распределитель данных потока.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, сохраняющая данные в поток.
    /// </returns>
    protected abstract Task SaveAsync(Spreader spreader, CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно сохраняет сообщение в поток.
    /// </summary>
    /// <param name="stream">
    /// Поток, в который необходимо сохранить сообщение.
    /// </param>
    /// <param name="timeout">
    /// Максимальное время ожидания.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, сохраняющая данные в поток.
    /// </returns>
    public async Task SaveAsync(Stream stream, int timeout, CancellationToken cancellationToken)
    {
        //  Создание источника связанного токена отмены.
        using CancellationTokenSource linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        //  Настройка отмены по времени ожидания.
        linkedTokenSource.CancelAfter(timeout);

        //  Замена токена отмены.
        cancellationToken = linkedTokenSource.Token;

        //  Создание распределителя данных потока.
        Spreader spreader = new(stream, Encoding.UTF8);

        //  Запись сигнатуры.
        await spreader.WriteUInt64Async(_Signature, cancellationToken).ConfigureAwait(false);

        //  Запись версии.
        await spreader.WriteInt32Async(Version, cancellationToken).ConfigureAwait(false);

        //  Запись формата.
        await spreader.WriteInt32Async((int)Format, cancellationToken).ConfigureAwait(false);

        //  Сохранение данных в поток.
        await SaveAsync(spreader, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно загружает данные из потока.
    /// </summary>
    /// <param name="stream">
    /// Поток, из которого необходимо загрузить данные.
    /// </param>
    /// <param name="timeout">
    /// Максимальное время ожидания.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, загружающая данные из потока.
    /// </returns>
    public static async Task<Message> LoadAsync(Stream stream, int timeout, CancellationToken cancellationToken)
    {
        //  Создание источника связанного токена отмены.
        using CancellationTokenSource linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        //  Настройка отмены по времени ожидания.
        linkedTokenSource.CancelAfter(timeout);

        //  Замена токена отмены.
        cancellationToken = linkedTokenSource.Token;

        //  Создание распределителя данных потока.
        Spreader spreader = new(stream, Encoding.UTF8);

        //  Чтение сигнатуры.
        ulong signature = await spreader.ReadUInt64Async(cancellationToken).ConfigureAwait(false);

        //  Проверка сигнатуры.
        if (signature != _Signature) throw new InvalidDataException("Получена неверная сигнатура сообщения.");

        //  Чтение версии.
        int version = await spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false);

        //  Проверка версии.
        if (version > _CurrentVersion) throw new InvalidDataException("Получено сообщение неподдерживаемой версии.");

        //  Чтение формата.
        MessageFormat format = (MessageFormat)await spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false);

        //  Разбор формата.
        return format switch
        {
            MessageFormat.ConnectionRequest => await ConnectionRequest.LoadAsync(spreader, cancellationToken).ConfigureAwait(false),
            MessageFormat.ConnectionConfirmation => await ConnectionConfirmation.LoadAsync(spreader, cancellationToken).ConfigureAwait(false),
            MessageFormat.FileData => await FileData.LoadAsync(spreader, cancellationToken).ConfigureAwait(false),
            MessageFormat.FileDataConfirmation => await FileDataConfirmation.LoadAsync(spreader, cancellationToken).ConfigureAwait(false),
            _ => throw new InvalidDataException("Получено сообщение неизвестного формата."),
        };
    }
}
