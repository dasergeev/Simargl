//using System.Net;
//using System.Net.Sockets;
//using Simargl.IO;
//using Simargl.Designing;
//using System.Collections.Generic;
//using System.IO;

//namespace Simargl.Embedded.Modbus;

///// <summary>
///// Представляет ведущее устройство Modbus TCP.
///// </summary>
//public sealed class TcpMaster
//{
//    /// <summary>
//    /// Постоянная, определяющая номер порта по умолчанию.
//    /// </summary>
//    public const int DefaultPort = 502;

//    /// <summary>
//    /// Поле для хранения идентификатора транзакции.
//    /// </summary>
//    private int _TransactionIdentifier;

//    /// <summary>
//    /// Инициализирует новый экземпляр класса.
//    /// </summary>
//    /// <param name="address">
//    /// Адрес для подключения к ведомому устройству.
//    /// </param>
//    /// <param name="slaveIdentifier">
//    /// Идентификатор устройства.
//    /// </param>
//    /// <exception cref="ArgumentNullException">
//    /// В параметре <paramref name="address"/> передана пустая ссылка.
//    /// </exception>
//    public TcpMaster(IPAddress address, byte slaveIdentifier)
//    {
//        //  Проверка адреса.
//        IsNotNull(address, nameof(address));

//        //  Установка конечной точки.
//        EndPoint = new(address, DefaultPort);

//        //  Установка идентификатора транзакции.
//        _TransactionIdentifier = 0;

//        //  Установка идентификатора устройства.
//        SlaveIdentifier = slaveIdentifier;
//    }

//    /// <summary>
//    /// Инициализирует новый экземпляр класса.
//    /// </summary>
//    /// <param name="endPoint">
//    /// Конечная точка для подключения к ведомому устройству.
//    /// </param>
//    /// <param name="slaveIdentifier">
//    /// Идентификатор устройства.
//    /// </param>
//    /// <exception cref="ArgumentNullException">
//    /// В параметре <paramref name="endPoint"/> передана пустая ссылка.
//    /// </exception>
//    public TcpMaster(IPEndPoint endPoint, byte slaveIdentifier)
//    {
//        //  Установка конечной точки.
//        EndPoint = IsNotNull(endPoint, nameof(endPoint));

//        //  Установка идентификатора транзакции.
//        _TransactionIdentifier = 0;

//        //  Установка идентификатора устройства.
//        SlaveIdentifier = slaveIdentifier;
//    }

//    /// <summary>
//    /// Возвращает конечную точку для подключения к ведомому устройству.
//    /// </summary>
//    public IPEndPoint EndPoint { get; }

//    /// <summary>
//    /// Возвращает идентификатор устройства.
//    /// </summary>
//    public byte SlaveIdentifier { get; }

//    /// <summary>
//    /// Выполняет тестовое подключение.
//    /// </summary>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая тестовое подключение.
//    /// </returns>
//    public async Task<bool> TestConnectAsync(CancellationToken cancellationToken)
//    {
//        //  Блок перехвата всех исключений.
//        try
//        {
//            //  Создание клиента
//            using TcpClient client = new();

//            //  Подключение по TCP/IP
//            await client.ConnectAsync(EndPoint, cancellationToken).ConfigureAwait(false);

//            //  Соединение успешно установлено.
//            return true;
//        }
//        catch
//        {
//            //  Соединение не установлено.
//            return false;
//        }
//    }

//    /// <summary>
//    /// Выполняет чтение регистров хранения.
//    /// </summary>
//    /// <param name="start">
//    /// Номер первого регистра.
//    /// </param>
//    /// <param name="count">
//    /// Количество регистров.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Массив прочитанных значений.
//    /// </returns>
//    /// <exception cref="ArgumentOutOfRangeException">
//    /// В параметре <paramref name="start"/> передано значение,
//    /// которое меньше значения <see cref="ushort.MinValue"/>
//    /// или больше значения <see cref="ushort.MaxValue"/>.
//    /// </exception>
//    /// <exception cref="ArgumentOutOfRangeException">
//    /// В параметре <paramref name="count"/> передано значение,
//    /// которое меньше значения <see cref="byte.MinValue"/>
//    /// или больше значения <see cref="byte.MaxValue"/>.
//    /// </exception>
//    [CLSCompliant(false)]
//    public async Task<ushort[]> ReadHoldingsAsync(int start, int count, CancellationToken cancellationToken)
//    {
//        //  Проверка входных параметров.
//        IsInRange(start, ushort.MinValue, ushort.MaxValue, nameof(start));
//        IsInRange(count, byte.MinValue, byte.MaxValue, nameof(count));

//        //  Создание клиента
//        using TcpClient client = new();

//        //  Подключение по TCP/IP
//        await client.ConnectAsync(EndPoint, cancellationToken).ConfigureAwait(false);

//        //  Получение потока.
//        NetworkStream stream = client.GetStream();

//        //  Создание распределителя данных потока.
//        Spreader spreader = new(stream);

//        //  Создание запроса.
//        TcpAduPackage request = RequestReadHoldings(start, count);

//        //  Запись в поток.
//        await request.SaveAsync(spreader, cancellationToken).ConfigureAwait(false);

//        //  Принудительная отправка данных.
//        await stream.FlushAsync(cancellationToken).ConfigureAwait(false);

//        //  Чтение ответа.
//        TcpAduPackage response = await TcpAduPackage.LoadAsync(spreader, cancellationToken).ConfigureAwait(false);

//        //  Проверка ответа.
//        IsResponse(response, request);

//        //  Возврат прочитанных регистров.
//        return ToHoldings(response);
//    }

//    /// <summary>
//    /// Выполняет запись регистров хранения.
//    /// </summary>
//    /// <param name="start">
//    /// Номер первого регистра.
//    /// </param>
//    /// <param name="registers">
//    /// Значения регистров.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <exception cref="ArgumentOutOfRangeException">
//    /// В параметре <paramref name="start"/> передано значение,
//    /// которое меньше значения <see cref="ushort.MinValue"/>
//    /// или больше значения <see cref="ushort.MaxValue"/>.
//    /// </exception>
//    /// <exception cref="ArgumentNullException">
//    /// В параметре <paramref name="registers"/> передана пустая ссылка.
//    /// </exception>
//    /// <exception cref="InvalidDataException">
//    /// Неверный формат потока.
//    /// </exception>
//    [CLSCompliant(false)]
//    public async Task WriteHoldingsAsync(int start, ushort[] registers, CancellationToken cancellationToken)
//    {
//        //  Проверка номера первого регистра.
//        IsInRange(start, ushort.MinValue, ushort.MaxValue, nameof(start));

//        //  Проверка ссылки на массив значений регистров.
//        IsNotNull(registers, nameof(registers));

//        //  Создание клиента
//        using TcpClient client = new();

//        //  Подключение по TCP/IP
//        await client.ConnectAsync(EndPoint, cancellationToken).ConfigureAwait(false);

//        //  Получение потока.
//        NetworkStream stream = client.GetStream();

//        //  Создание распределителя данных потока.
//        Spreader spreader = new(stream);

//        //  Создание запроса.
//        TcpAduPackage request = RequestWriteHoldings(start, registers);

//        //  Запись в поток.
//        await request.SaveAsync(spreader, cancellationToken).ConfigureAwait(false);

//        //  Принудительная отправка данных.
//        await stream.FlushAsync(cancellationToken).ConfigureAwait(false);

//        //  Чтение ответа.
//        TcpAduPackage response = await TcpAduPackage.LoadAsync(spreader, cancellationToken).ConfigureAwait(false);

//        //  Проверка ответа.
//        IsResponse(response, request);

//        //  Проверка размера полученных данных.
//        if (response.PduPackage.Data.Length != 4)
//        {
//            //  Неверный формат потока.
//            throw new InvalidDataException();
//        }

//        //  Чтение первого адресса.
//        int startAddress = (response.PduPackage.Data[0] << 8) | response.PduPackage.Data[1];

//        //  Чтение количества регистров.
//        int countRegister = (response.PduPackage.Data[2] << 8) | response.PduPackage.Data[3];

//        //  Проверка прочитанных значений.
//        if (startAddress != start || countRegister != registers.Length)
//        {
//            //  Неверный формат потока.
//            throw new InvalidDataException();
//        }
//    }

//    /// <summary>
//    /// Создаёт запрос на чтение регистров хранения.
//    /// </summary>
//    /// <param name="start">
//    /// Номер первого регистра.
//    /// </param>
//    /// <param name="count">
//    /// Количество регистров.
//    /// </param>
//    /// <returns>
//    /// Запрос на чтение.
//    /// </returns>
//    private TcpAduPackage RequestReadHoldings([NoVerify] int start, [NoVerify] int count)
//    {
//        //  Получение идентификатора транзакции.
//        int transactionIdentifier = Interlocked.Increment(ref _TransactionIdentifier);

//        //  Создание запроса.
//        TcpAduPackage request = new(transactionIdentifier, SlaveIdentifier,
//            PduFunctionCode.ReadHoldings,
//            [
//                unchecked((byte)(start >> 8)),
//                unchecked((byte)(start >> 0)),
//                unchecked((byte)(count >> 8)),
//                unchecked((byte)(count >> 0))
//            ]);

//        //  Возврат запроса.
//        return request;
//    }

//    /// <summary>
//    /// Создаёт запрос на запись регистров хранения.
//    /// </summary>
//    /// <param name="start">
//    /// Номер первого регистра.
//    /// </param>
//    /// <param name="registers">
//    /// Значения регистров.
//    /// </param>
//    /// <returns>
//    /// Запрос на запись регистров хранения.
//    /// </returns>
//    private TcpAduPackage RequestWriteHoldings([NoVerify] int start, [NoVerify] ushort[] registers)
//    {
//        //  Созадние списка байт.
//        List<byte> bytes =
//        [
//            unchecked((byte)(start >> 8)),
//            unchecked((byte)(start >> 0)),
//        ];

//        //  Получение количества регистров.
//        int count = registers.Length;

//        //  Запись количества регистров.
//        bytes.AddRange(
//        [
//            unchecked((byte)(count >> 8)),
//            unchecked((byte)(count >> 0)),
//        ]);

//        //  Определение длинны данных.
//        byte length = (byte)(count * 2);

//        //  Запись длинны данных
//        bytes.Add(length);

//        //  Перебор регистров.
//        for (int i = 0; i < count; i++)
//        {
//            //  Запись значения регистра.
//            bytes.AddRange(
//            [
//                unchecked((byte)(registers[i] >> 8)),
//                unchecked((byte)(registers[i] >> 0)),
//            ]);
//        }

//        //  Получение идентификатора транзакции.
//        int transactionIdentifier = Interlocked.Increment(ref _TransactionIdentifier);

//        //  Создание запроса.
//        TcpAduPackage request = new(transactionIdentifier, SlaveIdentifier,
//            PduFunctionCode.WriteHoldings, bytes.ToArray());

//        //  Возврат запроса.
//        return request;
//    }

//    /// <summary>
//    /// Выполняет проверку ответа.
//    /// </summary>
//    /// <param name="response">
//    /// Ответ.
//    /// </param>
//    /// <param name="request">
//    /// Запрос.
//    /// </param>
//    private static void IsResponse(
//        [NoVerify] TcpAduPackage response,
//        [NoVerify] TcpAduPackage request)
//    {
//        if (response.Header.TransactionIdentifier != request.Header.TransactionIdentifier ||
//            response.Header.SlaveIdentifier != request.Header.SlaveIdentifier ||
//            response.PduPackage.FunctionCode != request.PduPackage.FunctionCode)
//        {
//            //  Неверный формат потока.
//            throw new InvalidDataException();
//        }
//    }

//    /// <summary>
//    /// Извлекает значения регистров из пакета.
//    /// </summary>
//    /// <param name="package">
//    /// Пакет, из которого необходимо извлечь значения регистров.
//    /// </param>
//    /// <returns>
//    /// Массив значений регистров.
//    /// </returns>
//    private static ushort[] ToHoldings([NoVerify] TcpAduPackage package)
//    {
//        //  Получение массива данных.
//        byte[] data = package.PduPackage.Data;

//        //  Создание массива регистров.
//        ushort[] registers = new ushort[data.Length >> 1];

//        //  Заполнение массива.
//        for (int i = 0; i < registers.Length; i++)
//        {
//            registers[i] = (ushort)((data[2 * i + 1] << 8) | data[2 * i + 2]);
//        }

//        //  Возврат массива значений.
//        return registers;
//    }
//}
