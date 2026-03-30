using Apeiron.IO;
using System.Net;
using System.Net.Sockets;

namespace Apeiron.Platform.Demo.AdxlDemo.Modbus;

/// <summary>
/// Представляет ведущее устройство Modbus TCP.
/// </summary>
public sealed class TcpMaster
{
    /// <summary>
    /// Постоянная, определяющая номер порта по умолчанию.
    /// </summary>
    public const int DefaultPort = 502;

    /// <summary>
    /// Поле для хранения идентификатора транзакции.
    /// </summary>
    private int _TransactionIdentifier;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="address">
    /// Адрес для подключения к ведомому устройству.
    /// </param>
    /// <param name="slaveIdentifier">
    /// Идентификатор устройства.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="address"/> передана пустая ссылка.
    /// </exception>
    public TcpMaster(IPAddress address, byte slaveIdentifier)
    {
        //  ПРоверка адреса.
        IsNotNull(address, nameof(address));

        //  Установка конечной точки.
        EndPoint = new(address, DefaultPort);

        //  Установка идентификатора транзакции.
        _TransactionIdentifier = 0;

        //  Установка идентификатора устройства.
        SlaveIdentifier = slaveIdentifier;
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="endPoint">
    /// Конечная точка для подключения к ведомому устройству.
    /// </param>
    /// <param name="slaveIdentifier">
    /// Идентификатор устройства.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="endPoint"/> передана пустая ссылка.
    /// </exception>
    public TcpMaster(IPEndPoint endPoint, byte slaveIdentifier)
    {
        //  Установка конечной точки.
        EndPoint = IsNotNull(endPoint, nameof(endPoint));

        //  Установка идентификатора транзакции.
        _TransactionIdentifier = 0;

        //  Установка идентификатора устройства.
        SlaveIdentifier = slaveIdentifier;
    }

    /// <summary>
    /// Возвращает конечную точку для подключения к ведомому устройству.
    /// </summary>
    public IPEndPoint EndPoint { get; }

    /// <summary>
    /// Возвращает идентификатор устройства.
    /// </summary>
    public byte SlaveIdentifier { get; }

    /// <summary>
    /// Выполняет чтение регистров хранения.
    /// </summary>
    /// <param name="start">
    /// Номер первого регистра.
    /// </param>
    /// <param name="count">
    /// Количество регистров.
    /// </param>
    /// <returns>
    /// Массив прочитанных значений.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="start"/> передано значение,
    /// которое меньше значения <see cref="ushort.MinValue"/>
    /// или больше значения <see cref="ushort.MaxValue"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано значение,
    /// которое меньше значения <see cref="byte.MinValue"/>
    /// или больше значения <see cref="byte.MaxValue"/>.
    /// </exception>
    [CLSCompliant(false)]
    public ushort[] ReadHoldings(int start, int count)
    {
        //  Проверка входных параметров.
        IsInRange(start, ushort.MinValue, ushort.MaxValue, nameof(start));
        IsInRange(count, byte.MinValue, byte.MaxValue, nameof(count));

        //  Создание клиента
        using TcpClient client = new();

        //  Подключение по TCP/IP
        client.Connect(EndPoint);

        //  Запуск задачи отмены.
        _ = Task.Run(async delegate
        {
            //  Блок с гарантированным завершением.
            try
            {
                //  Ожидание.
                await Task.Delay(2000);
            }
            finally
            {
                //  Закрытие клиента.
                client.Close();
            }
        });

        //  Получение потока.
        NetworkStream stream = client.GetStream();

        //  Создание распределителя данных потока.
        Spreader spreader = new(stream);

        //  Создание запроса.
        TcpAduPackage request = RequestReadHoldings(start, count);

        //  Запись в поток.
        request.Save(spreader);

        //  Принудительная отправка данных.
        stream.Flush();

        //  Чтение ответа.
        TcpAduPackage response = TcpAduPackage.Load(spreader);

        //  Проверка ответа.
        IsResponse(response, request);

        //  Возврат прочитанных регистров.
        return ToHoldings(response);
    }

    /// <summary>
    /// Выполняет запись регистров хранения.
    /// </summary>
    /// <param name="start">
    /// Номер первого регистра.
    /// </param>
    /// <param name="registers">
    /// Значения регистров.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="start"/> передано значение,
    /// которое меньше значения <see cref="ushort.MinValue"/>
    /// или больше значения <see cref="ushort.MaxValue"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="registers"/> передана пустая ссылка.
    /// </exception>
    [CLSCompliant(false)]
    public void WriteHoldings(int start, ushort[] registers)
    {
        //  Проверка номера первого регистра.
        IsInRange(start, ushort.MinValue, ushort.MaxValue, nameof(start));

        //  Проверка ссылки на массив значений регистров.
        IsNotNull(registers, nameof(registers));

        //  Создание клиента
        using TcpClient client = new();

        //  Подключение по TCP/IP
        client.Connect(EndPoint);

        //  Запуск задачи отмены.
        _ = Task.Run(async delegate
        {
            //  Блок с гарантированным завершением.
            try
            {
                //  Ожидание.
                await Task.Delay(2000);
            }
            finally
            {
                //  Закрытие клиента.
                client.Close();
            }
        });

        //  Получение потока.
        NetworkStream stream = client.GetStream();

        //  Создание распределителя данных потока.
        Spreader spreader = new(stream);

        //  Создание запроса.
        TcpAduPackage request = RequestWriteHoldings(start, registers);

        //  Запись в поток.
        request.Save(spreader);

        //  Принудительная отправка данных.
        stream.Flush();

        //  Чтение ответа.
        TcpAduPackage response = TcpAduPackage.Load(spreader);

        //  Проверка ответа.
        IsResponse(response, request);

        //  Проверка размера полученных данных.
        if (response.PduPackage.Data.Length != 4)
        {
            throw Exceptions.OperationInvalid();
        }

        //  Чтение первого адресса.
        int startAddress = (response.PduPackage.Data[0] << 8) | response.PduPackage.Data[1];

        //  Чтение количества регистров.
        int countRegister = (response.PduPackage.Data[2] << 8) | response.PduPackage.Data[3];

        //  Проверка прочитанных значений.
        if (startAddress != start || countRegister != registers.Length)
        {
            throw Exceptions.OperationInvalid();
        }
    }

    /// <summary>
    /// Создаёт запрос на чтение регистров хранения.
    /// </summary>
    /// <param name="start">
    /// Номер первого регистра.
    /// </param>
    /// <param name="count">
    /// Количество регистров.
    /// </param>
    /// <returns>
    /// Запрос на чтение.
    /// </returns>
    private TcpAduPackage RequestReadHoldings([ParameterNoChecks] int start, [ParameterNoChecks] int count)
    {
        //  Получение идентификатора транзакции.
        int transactionIdentifier = Interlocked.Increment(ref _TransactionIdentifier);

        //  Создание запроса.
        TcpAduPackage request = new(transactionIdentifier, SlaveIdentifier,
            PduFunctionCode.ReadHoldings,
            new byte[]
            {
                unchecked((byte)(start >> 8)),
                unchecked((byte)(start >> 0)),
                unchecked((byte)(count >> 8)),
                unchecked((byte)(count >> 0))
            });

        //  Возврат запроса.
        return request;
    }

    /// <summary>
    /// Создаёт запрос на запись регистров хранения.
    /// </summary>
    /// <param name="start">
    /// Номер первого регистра.
    /// </param>
    /// <param name="registers">
    /// Значения регистров.
    /// </param>
    /// <returns>
    /// Запрос на запись регистров хранения.
    /// </returns>
    private TcpAduPackage RequestWriteHoldings([ParameterNoChecks] int start, [ParameterNoChecks] ushort[] registers)
    {
        //  Созадние списка байт.
        List<byte> bytes = new();

        //  Запись номера первого регистра.
        bytes.AddRange(new byte[]
        {
            unchecked((byte)(start >> 8)),
            unchecked((byte)(start >> 0)),
        });

        //  Получение количества регистров.
        int count = registers.Length;

        //  Запись количества регистров.
        bytes.AddRange(new byte[]
        {
            unchecked((byte)(count >> 8)),
            unchecked((byte)(count >> 0)),
        });

        //  Определение длинны данных.
        byte length = (byte)(count * 2);

        //  Запись длинны данных
        bytes.Add(length);

        //  Перебор регистров.
        for (int i = 0; i < count; i++)
        {
            //  Запись значения регистра.
            bytes.AddRange(new byte[]
            {
                unchecked((byte)(registers[i] >> 8)),
                unchecked((byte)(registers[i] >> 0)),
            });
        }

        //  Получение идентификатора транзакции.
        int transactionIdentifier = Interlocked.Increment(ref _TransactionIdentifier);

        //  Создание запроса.
        TcpAduPackage request = new(transactionIdentifier, SlaveIdentifier,
            PduFunctionCode.WriteHoldings, bytes.ToArray());

        //  Возврат запроса.
        return request;
    }

    /// <summary>
    /// Выполняет проверку ответа.
    /// </summary>
    /// <param name="response">
    /// Ответ.
    /// </param>
    /// <param name="request">
    /// Запрос.
    /// </param>
    private static void IsResponse(
        [ParameterNoChecks] TcpAduPackage response,
        [ParameterNoChecks] TcpAduPackage request)
    {
        if (response.Header.TransactionIdentifier != request.Header.TransactionIdentifier ||
            response.Header.SlaveIdentifier != request.Header.SlaveIdentifier ||
            response.PduPackage.FunctionCode != request.PduPackage.FunctionCode)
        {
            throw Exceptions.StreamInvalidFormat();
        }
    }

    /// <summary>
    /// Извлекает значения регистров из пакета.
    /// </summary>
    /// <param name="package">
    /// Пакет, из которого необходимо извлечь значения регистров.
    /// </param>
    /// <returns>
    /// Массив значений регистров.
    /// </returns>
    private static ushort[] ToHoldings([ParameterNoChecks] TcpAduPackage package)
    {
        //  Получение массива данных.
        byte[] data = package.PduPackage.Data;

        //  Создание массива регистров.
        ushort[] registers = new ushort[data.Length >> 1];

        //  Заполнение массива.
        for (int i = 0; i < registers.Length; i++)
        {
            registers[i] = (ushort)((data[2 * i + 1] << 8) | data[2 * i + 2]);
        }

        //  Возврат массива значений.
        return registers;
    }
}
