using Apeiron.IO;
using System.IO;

namespace Apeiron.Platform.Demo.AdxlDemo.Modbus;

/// <summary>
/// Представляет заголовок пакета формата MBAP.
/// </summary>
public sealed class MbapHeader
{
    /// <summary>
    /// Постоаянная, определяющая идентификатор протокола.
    /// </summary>
    private const ushort _ProtocolIdentifier = 0;

    /// <summary>
    /// Поле для хранения идентификатора транзакции.
    /// </summary>
    private readonly ushort _TransactionIdentifier;

    /// <summary>
    /// Поле для хранения длины данных.
    /// </summary>
    private readonly ushort _Length;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="transactionIdentifier">
    /// Идентификатор транзакции.
    /// </param>
    /// <param name="length">
    /// Длина данных.
    /// </param>
    /// <param name="slaveIdentifier">
    /// Идентификатор устройства.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="transactionIdentifier"/> передано значение,
    /// которое меньше значения <see cref="ushort.MinValue"/>
    /// или больше значения <see cref="ushort.MaxValue"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="length"/> передано значение,
    /// которое меньше значения <see cref="ushort.MinValue"/>
    /// или больше значения <see cref="ushort.MaxValue"/>.
    /// </exception>
    public MbapHeader(int transactionIdentifier, int length, byte slaveIdentifier)
    {
        //  Установка идентификатора транзакции.
        _TransactionIdentifier = unchecked((ushort)IsInRange(transactionIdentifier,
            ushort.MinValue, ushort.MaxValue, nameof(transactionIdentifier)));

        //  Установка длины данных.
        _Length = unchecked((ushort)IsInRange(length,
            ushort.MinValue, ushort.MaxValue, nameof(length)));

        //  Установка идентификатора устройства.
        SlaveIdentifier = slaveIdentifier;
    }

    /// <summary>
    /// Возвращает идентификатор транзакции.
    /// </summary>
    public int TransactionIdentifier => _TransactionIdentifier;

    /// <summary>
    /// Возвращает длину данных.
    /// </summary>
    public int Length => _Length;

    /// <summary>
    /// Возвращает идентификатор устройства.
    /// </summary>
    public byte SlaveIdentifier { get; }

    /// <summary>
    /// Сохраняет данные в поток.
    /// </summary>
    /// <param name="spreader">
    /// Распределитель данных потока.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="spreader"/> передана пустая ссылка.
    /// </exception>
    public void Save(Spreader spreader)
    {
        //  Проверка ссылки на распределитель данных потока.
        IsNotNull(spreader, nameof(spreader));

        //  Запись идентификатора транзакции.
        spreader.WriteUInt8(unchecked((byte)(_TransactionIdentifier >> 8)));
        spreader.WriteUInt8(unchecked((byte)_TransactionIdentifier));

        //  Запись идентификатора протокола.
        spreader.WriteUInt8(_ProtocolIdentifier >> 8);
        spreader.WriteUInt8(unchecked((byte)_ProtocolIdentifier));

        //  Запись длины данных.
        spreader.WriteUInt8(unchecked((byte)(_Length >> 8)));
        spreader.WriteUInt8(unchecked((byte)_Length));

        //  Запись идентификатора устройства.
        spreader.WriteUInt8(SlaveIdentifier);
    }

    /// <summary>
    /// Асинхронно сохраняет данные в поток.
    /// </summary>
    /// <param name="spreader">
    /// Распределитель данных потока.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись данных в поток.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="spreader"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task SaveAsync(Spreader spreader, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на распределитель данных потока.
        IsNotNull(spreader, nameof(spreader));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Запись идентификатора транзакции.
        await spreader.WriteUInt8Async(unchecked((byte)(_TransactionIdentifier >> 8)), cancellationToken).ConfigureAwait(false);
        await spreader.WriteUInt8Async(unchecked((byte)_TransactionIdentifier), cancellationToken).ConfigureAwait(false);

        //  Запись идентификатора протокола.
        await spreader.WriteUInt8Async(_ProtocolIdentifier >> 8, cancellationToken).ConfigureAwait(false);
        await spreader.WriteUInt8Async(unchecked((byte)_ProtocolIdentifier), cancellationToken).ConfigureAwait(false);

        //  Запись длины данных.
        await spreader.WriteUInt8Async(unchecked((byte)(_Length >> 8)), cancellationToken).ConfigureAwait(false);
        await spreader.WriteUInt8Async(unchecked((byte)_Length), cancellationToken).ConfigureAwait(false);

        //  Запись идентификатора устройства.
        await spreader.WriteUInt8Async(SlaveIdentifier, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Загружает данные из потока.
    /// </summary>
    /// <param name="spreader">
    /// Распределитель данных потока.
    /// </param>
    /// <returns>
    /// Прочитанные данные из потока.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="spreader"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="IOException">
    /// Неверный формат потока.
    /// </exception>
    public static MbapHeader Load(Spreader spreader)
    {
        //  Проверка ссылки на распределитель данных потока.
        IsNotNull(spreader, nameof(spreader));

        //  Чтение идентификатора транзакции.
        int transactionIdentifier = (spreader.ReadUInt8() << 8) | spreader.ReadUInt8();

        //  Чтение идентификатора протокола.
        int protocolIdentifier = (spreader.ReadUInt8() << 8) | spreader.ReadUInt8();

        //  Проверка идентификатора протокола
        if (protocolIdentifier != _ProtocolIdentifier)
        {
            throw Exceptions.StreamInvalidFormat();
        }

        //  Чтение длины данных.
        int length = (spreader.ReadUInt8() << 8) | spreader.ReadUInt8();

        //  Чтение идентификатора устройства.
        byte slaveIdentifier = spreader.ReadUInt8();

        //  Возврат прочитанных данных.
        return new(transactionIdentifier, length, slaveIdentifier);
    }

    /// <summary>
    /// Асинхронно загружает данные из потока.
    /// </summary>
    /// <param name="spreader">
    /// Распределитель данных потока.
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    /// Задача, выполняющая загрузку данных из потока.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="spreader"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="IOException">
    /// Неверный формат потока.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public static async Task<MbapHeader> LoadAsync(Spreader spreader, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на распределитель данных потока.
        IsNotNull(spreader, nameof(spreader));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Чтение идентификатора транзакции.
        int transactionIdentifier = (await spreader.ReadUInt8Async(cancellationToken).ConfigureAwait(false) << 8) | await spreader.ReadUInt8Async(cancellationToken).ConfigureAwait(false);

        //  Чтение идентификатора протокола.
        int protocolIdentifier = (await spreader.ReadUInt8Async(cancellationToken).ConfigureAwait(false) << 8) | await spreader.ReadUInt8Async(cancellationToken).ConfigureAwait(false);

        //  Проверка идентификатора протокола
        if (protocolIdentifier != _ProtocolIdentifier)
        {
            throw Exceptions.StreamInvalidFormat();
        }

        //  Чтение длины данных.
        int length = (await spreader.ReadUInt8Async(cancellationToken).ConfigureAwait(false) << 8) | await spreader.ReadUInt8Async(cancellationToken).ConfigureAwait(false);

        //  Чтение идентификатора устройства.
        byte slaveIdentifier = await spreader.ReadUInt8Async(cancellationToken).ConfigureAwait(false);

        //  Возврат прочитанных данных.
        return new(transactionIdentifier, length, slaveIdentifier);
    }
}
