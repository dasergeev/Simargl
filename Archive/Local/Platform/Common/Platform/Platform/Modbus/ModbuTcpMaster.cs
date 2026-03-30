using System.Net;
using System.Net.Sockets;

namespace Apeiron.Platform.Modbus;


/// <summary>
/// Представляет класс подключения по Modbus.
/// </summary>
public class ModbuTcpMaster
{
    /// <summary>
    /// Идентификатор транзакции.
    /// </summary>
    private ushort _TransactionIdentifier = 0x0001;

    /// <summary>
    /// Возвращает Modbus адресс устройства.
    /// </summary>
    public byte DeviceAddress { get; }
    /// <summary>
    /// Возвращает порт Slave - устройства.
    /// </summary>
    public int Port { get; }
    /// <summary>
    /// Возвращает IP - адресс устройства.
    /// </summary>
    public  IPAddress IP { get; }

    /// <summary>
    /// Представляет конструктор объекта.
    /// </summary>
    /// <param name="ip">IP адрес Slave устройства.</param>
    /// <param name="port">Порт Slave - устройства.</param>
    /// <param name="deviceAdress">Modbus адрес устройства.</param>
    public ModbuTcpMaster(IPAddress ip,int port,byte deviceAdress)
    {
        //  Проверка порта.
        Check.IsNotLarger(port, ushort.MaxValue, nameof(port));

        //  Проверка порта.
        Check.IsNotLess(port, ushort.MinValue, nameof(port));

        //  Установка и проверка IP
        IP = Check.IsNotNull(ip,nameof(ip));

        //  Установка порта.
        Port = (ushort)port;

        //  Установка modbus адреса.
        DeviceAddress = deviceAdress;
    }

    /// <summary>
    /// Представляет чтение Holding Register.
    /// </summary>
    /// <param name="start">
    /// Начальный адрес.
    /// </param>
    /// <param name="count">
    /// Количество
    /// </param>
    /// <param name="timeout">
    /// Максимальное время выполнения задачи.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Массив данных.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">В параметре <paramref name="start"/> передано не допустимое значение.</exception>
    /// <exception cref="ArgumentOutOfRangeException">В параметре <paramref name="count"/> передано не допустимое значение.</exception>
    /// <exception cref="IOException">Не верный протокол обмена.</exception>
    /// <exception cref="EndOfStreamException">Достигнут конец потока.</exception>
    /// <exception cref="FormatException">В modbus header пришел код ошибки.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Получено значение <see cref="ModbusTcpHeader.Length"/> вне допустимого диапозона.</exception>
    [CLSCompliant(false)]
    public async  Task<ushort[]> ReadMultipleRegistersAsync(int start, int count, ushort timeout, CancellationToken cancellationToken)
    {
        //  Создание источника отмены задачи.
        using CancellationTokenSource localSource = new ();

        //  Установка таймаута для задачи.
        localSource.CancelAfter(timeout);

        //  Получение токена
        CancellationToken localToken = localSource.Token;

        //  Cвязывание токенов.
        using CancellationTokenSource combinedSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, localToken);

        //  Получение связанного токена
        CancellationToken combinedToken = combinedSource.Token;

        //  Создание команды
        ReadMultipleHoldingRegisterCommand command = new(_TransactionIdentifier, DeviceAddress, start, count);

        _TransactionIdentifier = unchecked((ushort)(_TransactionIdentifier++));

        //  Создание клиента
        using TcpClient client = new();

        //  Подключение по TCP/IP
        await client.ConnectAsync(new IPEndPoint(IP, Port), combinedToken).ConfigureAwait(false);

        //  Получение потока.
        var stream = client.GetStream();

        //  Создание потока буфера
        using MemoryStream memory = new ();

        //  Запись команды в поток буфера.
        await command.SaveAsync(memory, combinedToken);

        //  Запись данных из буфера в поток клиента
        await stream.WriteAsync(memory.ToArray(), combinedToken).ConfigureAwait(false);

        //  Принудительная отправка.
        await stream.FlushAsync(combinedToken).ConfigureAwait(false);

        //  Чтение ответа.
        ReadMultipleHoldingRegisterResponse response = await
            new ReadMultipleHoldingRegisterResponse().LoadAsync(stream, combinedToken).ConfigureAwait(false);

        //Проверка функции заголовка
        if (response.FunctionalCode != 0x03)
        {
            throw Exceptions.StreamInvalidFormat();
        }

        //Возврат конечного массива данных
        return response.Registers;
    }

    /// <summary>
    /// Представляет запись в Holding Register.
    /// </summary>
    /// <param name="start">
    /// Начальный адрес.
    /// </param>
    /// <param name="registers">
    /// Данные
    /// </param>
    /// <param name="timeout">
    /// Максимальное время выполнения задачи.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача.
    /// </returns>
    /// <exception cref="ArgumentNullException">В параметре <paramref name="registers"/> передан пустая ссылка.</exception>
    /// <exception cref="ArgumentOutOfRangeException">В параметре <paramref name="registers"/> передан массив длинны больше допустимой.</exception>
    /// <exception cref="ArgumentOutOfRangeException">В параметре <paramref name="start"/> передано не допустимое значение.</exception>
    /// <exception cref="IOException">Не верный протокол обмена.</exception>
    /// <exception cref="EndOfStreamException">Достигнут конец потока.</exception>
    /// <exception cref="FormatException">В modbus header пришел код ошибки.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Получено значение <see cref="ModbusTcpHeader.Length"/> вне допустимого диапозона.</exception>
    [CLSCompliant(false)]
    public async  Task WriteMultipleRegistersAsync(int start, ushort[] registers, ushort timeout, CancellationToken cancellationToken)
    {
        //Проверка ссылки на данные
        registers = Check.IsNotNull(registers, nameof(registers));

        //  Создание источника отмены задачи.
        using CancellationTokenSource localSource = new();

        //  Установка таймаута для задачи.
        localSource.CancelAfter(timeout);

        //  Получение токена
        CancellationToken localToken = localSource.Token;

        //  Cвязывание токенов.
        using CancellationTokenSource combinedSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, localToken);

        //  Получение связанного токена
        CancellationToken combinedToken = combinedSource.Token;

        //  Создание команды
        WriteMultipleHodingRegisterCommand command = new(_TransactionIdentifier, DeviceAddress, start, registers);

        _TransactionIdentifier = unchecked((ushort)(_TransactionIdentifier++));

        //Создание клиента
        using TcpClient client = new();

        //  Подключение по TCP/IP
        await client.ConnectAsync(new IPEndPoint(IP, Port), combinedToken).ConfigureAwait(false);

        //  Получение потока
        var stream = client.GetStream();

        //  Создание потока буфера
        using MemoryStream memory = new();

        //  Запись команды в поток буфера.
        await command.SaveAsync(memory, combinedToken);

        //  Запись данных из буфера в поток клиента
        await stream.WriteAsync(memory.ToArray(), combinedToken).ConfigureAwait(false);

        //  Принудительная отправка.
        await stream.FlushAsync(combinedToken).ConfigureAwait(false);

        //  Чтение ответа.
        WriteMultipleHoldingRegisterResponse response = await
            new WriteMultipleHoldingRegisterResponse().LoadAsync(stream, combinedToken).ConfigureAwait(false);

        //Проверка функции заголовка
        if (response.FunctionalCode != command.FunctionalCode)
        {
            throw Exceptions.StreamInvalidFormat();
        }
    }

    /// <summary>
    /// Представляет запись в Holding Register без чтения.
    /// </summary>
    /// <param name="start">
    /// Начальный адрес.
    /// </param>
    /// <param name="registers">
    /// Данные
    /// </param>
    /// <param name="timeout">
    /// Максимальное время выполнения задачи.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача.
    /// </returns>   
    /// <exception cref="ArgumentNullException">В параметре <paramref name="registers"/> передан пустая ссылка.</exception>
    /// <exception cref="ArgumentOutOfRangeException">В параметре <paramref name="registers"/> передан массив длинны больше допустимой.</exception>
    /// <exception cref="ArgumentOutOfRangeException">В параметре <paramref name="start"/> передано не допустимое значение.</exception>
    [CLSCompliant(false)]
    public async Task WriteMultipleRegistersWithoutReadAsync( int start, ushort[] registers, ushort timeout, CancellationToken cancellationToken)
    {
        //  Создание источника отмены задачи.
        using CancellationTokenSource localSource = new ();

        //  Установка таймаута для задачи.
        localSource.CancelAfter(timeout);

        //  Получение токена
        CancellationToken localToken = localSource.Token;

        //  Cвязывание токенов.
        using CancellationTokenSource combinedSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, localToken);

        //  Получение связанного токена
        CancellationToken combinedToken = combinedSource.Token;

        //Проверка ссылки на данные
        registers = Check.IsNotNull(registers, nameof(registers));

        //  Создание команды
        WriteMultipleHodingRegisterCommand command = new(_TransactionIdentifier, DeviceAddress, start, registers);

        _TransactionIdentifier = unchecked((ushort)(_TransactionIdentifier++));

        //Создание клиента
        using TcpClient client = new();

        //Подключение по TCP/IP
        await client.ConnectAsync(new IPEndPoint(IP, Port), combinedToken).ConfigureAwait(false);
        
        //  Получение потока
        var stream = client.GetStream();

        //  Создание потока буфера
        using MemoryStream memory = new();

        //  Запись команды в поток буфера.
        await command.SaveAsync(memory, combinedToken);

        //  Запись данных из буфера в поток клиента
        await stream.WriteAsync(memory.ToArray(), combinedToken).ConfigureAwait(false);

        //  Принудительная отправка.
        await stream.FlushAsync(combinedToken).ConfigureAwait(false);
    }
}

