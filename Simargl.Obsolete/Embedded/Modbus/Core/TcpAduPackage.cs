using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Simargl.IO;

namespace Simargl.Embedded.Modbus;

/// <summary>
/// Представляет пакет, содержащий данные TCP/IP ADU.
/// </summary>
/// <param name="transactionIdentifier">
/// Идентификатор транзакции.
/// </param>
/// <param name="slaveIdentifier">
/// Идентификатор устройства.
/// </param>
/// <param name="functionCode">
/// Значение, определяющее код функции.
/// </param>
/// <param name="data">
/// Данные пакета.
/// </param>
/// <exception cref="ArgumentOutOfRangeException">
/// В параметре <paramref name="transactionIdentifier"/> передано значение,
/// которое меньше значения <see cref="ushort.MinValue"/>
/// или больше значения <see cref="ushort.MaxValue"/>.
/// </exception>
/// <exception cref="ArgumentOutOfRangeException">
/// В параметре <paramref name="functionCode"/> передано значение,
/// которое не содержится в перечислении <see cref="PduFunctionCode"/>.
/// </exception>
/// <exception cref="ArgumentNullException">
/// В параметре <paramref name="data"/> передана пустая ссылка.
/// </exception>
public sealed class TcpAduPackage(
    int transactionIdentifier, byte slaveIdentifier,
    PduFunctionCode functionCode, byte[] data)
{
    /// <summary>
    /// Возвращает заголовок пакета.
    /// </summary>
    public MbapHeader Header { get; } = new(transactionIdentifier, data.Length + 2, slaveIdentifier);

    /// <summary>
    /// Возвращает пакет, содержащий данные PDU.
    /// </summary>
    public PduPackage PduPackage { get; } = new(functionCode, data);

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

        //  Сохранение заголовка.
        Header.Save(spreader);

        //  Запись значения, определяющего код функции.
        spreader.WriteUInt8(unchecked((byte)PduPackage.FunctionCode));

        //  Запись данных пакета.
        spreader.WriteBytes(PduPackage.Data);
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
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Сохранение заголовка.
        await Header.SaveAsync(spreader, cancellationToken).ConfigureAwait(false);

        //  Запись значения, определяющего код функции.
        await spreader.WriteUInt8Async(unchecked((byte)PduPackage.FunctionCode), cancellationToken).ConfigureAwait(false);

        //  Запись данных пакета.
        await spreader.WriteBytesAsync(PduPackage.Data, cancellationToken).ConfigureAwait(false);
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
    /// <exception cref="InvalidDataException">
    /// Неверный формат потока.
    /// </exception>
    public static TcpAduPackage Load(Spreader spreader)
    {
        //  Проверка ссылки на распределитель данных потока.
        IsNotNull(spreader, nameof(spreader));

        //  Загрузка заголовка.
        MbapHeader header = MbapHeader.Load(spreader);

        //  Проверка длины данных.
        if (header.Length < 2)
        {
            //  Неверный формат потока.
            throw new InvalidDataException();
        }

        //  Чтение кода функции.
        PduFunctionCode functionCode = (PduFunctionCode)spreader.ReadUInt8();

        //  Проверка кода функции.
        if (!Enum.IsDefined(typeof(PduFunctionCode), functionCode))
        {
            //  Неверный формат потока.
            throw new InvalidDataException();
        }

        //  Чтение данных.
        byte[] data = spreader.ReadBytes(header.Length - 2);

        //  Возврат прочитанного объекта.
        return new(header.TransactionIdentifier, header.SlaveIdentifier, functionCode, data);
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
    /// <exception cref="InvalidDataException">
    /// Неверный формат потока.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public static async Task<TcpAduPackage> LoadAsync(Spreader spreader, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на распределитель данных потока.
        IsNotNull(spreader, nameof(spreader));

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Загрузка заголовка.
        MbapHeader header = await MbapHeader.LoadAsync(spreader, cancellationToken).ConfigureAwait(false);

        //  Проверка длины данных.
        if (header.Length < 2)
        {
            //  Неверный формат потока.
            throw new InvalidDataException();
        }

        //  Чтение кода функции.
        PduFunctionCode functionCode = (PduFunctionCode)await spreader.ReadUInt8Async(cancellationToken).ConfigureAwait(false);

        //  Проверка кода функции.
        if (!Enum.IsDefined(typeof(PduFunctionCode), functionCode))
        {
            //  Неверный формат потока.
            throw new InvalidDataException();
        }

        //  Чтение данных.
        byte[] data = await spreader.ReadBytesAsync(header.Length - 2, cancellationToken).ConfigureAwait(false);

        //  Возврат прочитанного объекта.
        return new(header.TransactionIdentifier, header.SlaveIdentifier, functionCode, data);
    }
}
