namespace Apeiron.Platform.Medium;

/*  
 *  ╭───────────────┬───────┬───────┬───────────────────────────────────╮
 *  │ Имя           │Размер │  Тип  │ Описание                          │
 *  ├───────────────┼───────┼───────┼───────────────────────────────────┤
 *  │ Signature     │   4   │ uint  │ Сигнатура пакета: символы "Aprn"  │
 *  ├───────────────┼───────┼───────┼───────────────────────────────────┤
 *  │ Format        │   4   │ uint  │ Формат пакета: 1                  │
 *  ├───────────────┼───────┼───────┼───────────────────────────────────┤
 *  │ FullSize      │   8   │ long  │ Полный размер пакета              │
 *  ├───────────────┼───────┼───────┼───────────────────────────────────┤
 *  │ SenderId      │   4   │ int   │ Идентификатор отправителя         │
 *  ├───────────────┼───────┼───────┼───────────────────────────────────┤
 *  │ RecipientId   │   4   │ int   │ Идентификатор получателя          │
 *  ├───────────────┼───────┼───────┼───────────────────────────────────┤
 *  │ ...           │  ...  │ ...   │ Данные пакета                     │
 *  ╰───────────────┴───────┴───────┴───────────────────────────────────╯
 */

/// <summary>
/// Представляет датаграмму исполнителя, выполняющего обмен данными.
/// </summary>
public sealed class MediumDatagram
{
    /// <summary>
    /// Постоянная, определяющая сигнатуру датаграммы.
    /// </summary>
    private const uint _Signature = 0x6e727041;

    /// <summary>
    /// Постоянная, определяющая формат датаграммы.
    /// </summary>
    private const uint _Format = 1;

    /// <summary>
    /// Постоянная, определяющая минимальный размер пакета.
    /// </summary>
    private const int _MinSize = 24;

    ///// <summary>
    ///// Поле для хранения данных пакета.
    ///// </summary>
    //private readonly byte[] _Data;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="senderId">
    /// Идентификатор отправителя.
    /// </param>
    /// <param name="recipientId">
    /// Идентификатор получателя.
    /// </param>
    /// <param name="data">
    /// Данные пакета.
    /// </param>
    private MediumDatagram(int senderId, int recipientId, [ParameterNoChecks] byte[] data)
    {
        //  Установка идентификатора отправителя.
        SenderId = senderId;

        //  Установка идентификатора получателя.
        RecipientId = recipientId;

        ////  Установка данных пакета.
        //_Data = data;
        _ = data;
    }

    /// <summary>
    /// Возвращает идентификатор отправителя.
    /// </summary>
    public int SenderId { get; }

    /// <summary>
    /// Возвращает идентификатор получателя.
    /// </summary>
    public int RecipientId { get; }

    /// <summary>
    /// Асинхронно выполняет разбор буфера с данными, полученными в UDP-пакете.
    /// </summary>
    /// <param name="buffer">
    /// Буфер с данными, полученными в UDP-пакете.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая разбор буфера с данными, полученными в UDP-пакете.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public static async Task<MediumDatagram?> TryParseAsync(
        byte[] buffer, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка ссылки на буфер.
        if (buffer is null)
        {
            //  Буфер не содержит данных.
            return null;
        }

        //  Определение длины буфера.
        int length = buffer.Length;

        //  Проверка размера пакета.
        if (length < _MinSize)
        {
            //  Буфер содержит недостаточно данных.
            return null;
        }

        //  Создание потока для чтения данных.
        await using MemoryStream stream = new(buffer);

        //  Создание средства чтения двоичных данных.
        using BinaryReader reader = new(stream);

        //  Проверка сигнатуры.
        if (reader.ReadUInt32() != _Signature)
        {
            //  Неверная сигнатура.
            return null;
        }

        //  Проверка формата.
        if (reader.ReadUInt32() != _Format)
        {
            //  Неверный формат.
            return null;
        }

        //  Проверка размера.
        if (reader.ReadInt64() != length)
        {
            //  Неверный размер.
            return null;
        }

        //  Чтение идентификатора отправителя.
        int senderId = reader.ReadInt32();

        //  Чтение идентификатора получателя.
        int recipientId = reader.ReadInt32();

        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Определение размера данных.
        length -= _MinSize;

        //  Проверка размера данных.
        if (length == 0)
        {
            //  Возврат пустого пакета.
            return new(senderId, recipientId, Array.Empty<byte>());
        }
        else
        {
            //  Выделение массива данных.
            byte[] data = new byte[length];

            //  Копирование данных.
            Buffer.BlockCopy(buffer, _MinSize, data, 0, length);

            //  Возврат пакета.
            return new(senderId, recipientId, data);
        }
    }
}
