//using Simargl.Net;
//using System.Collections.Concurrent;
//using System.IO;
//using System.Net;
//using System.Runtime.CompilerServices;

//namespace Simargl.Embedded.Modbus;

///// <summary>
///// Представляет соединение по Modbus TCP.
///// </summary>
///// <param name="address">
///// Адрес для подключения к ведомому устройству.
///// </param>
///// <param name="slaveIdentifier">
///// Идентификатор устройства.
///// </param>
///// <exception cref="ArgumentNullException">
///// В параметре <paramref name="address"/> передана пустая ссылка.
///// </exception>
//public sealed class ModbusConnect(IPAddress address, byte slaveIdentifier) :
//    Anything
//{
//    /// <summary>
//    /// Поле для хранения ведущего устройства.
//    /// </summary>
//    private readonly TcpMaster _TcpMaster = new(address, slaveIdentifier);

//    /// <summary>
//    /// Поле для хранения критического объекта.
//    /// </summary>
//    private readonly AsyncLock _Lock = new();

//    /// <summary>
//    /// Поле для хранения таймаута.
//    /// </summary>
//    private int _Timeout = System.Threading.Timeout.Infinite;

//    /// <summary>
//    /// Возвращает адрес устройства.
//    /// </summary>
//    public IPAddress Address { get; } = address;

//    /// <summary>
//    /// Возвращает или задаёт таймаут ожидания.
//    /// </summary>
//    /// <exception cref="ArgumentOutOfRangeException">
//    /// Передан недопустимый таймаут.
//    /// </exception>
//    public TimeSpan Timeout
//    {
//        get => TimeSpan.FromMilliseconds(_Timeout);
//        set => _Timeout = IsTimeout(value, nameof(Timeout));
//    }

//    /// <summary>
//    /// Асинхронно выполняет сканирование сети.
//    /// </summary>
//    /// <param name="first">
//    /// Первый IP-адрес.
//    /// </param>
//    /// <param name="last">
//    /// Последний IP-адрес.
//    /// </param>
//    /// <param name="slaveIdentifier">
//    /// Идентификатор устройства.
//    /// </param>
//    /// <param name="timeout">
//    /// Время ожидания ответа.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая сканирование сети.
//    /// </returns>
//    public static async Task<ModbusConnect[]> ScanAsync(
//        IPAddress first, IPAddress last, byte slaveIdentifier,
//        TimeSpan timeout, CancellationToken cancellationToken)
//    {
//        //  Проверка токена отмены.
//        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

//        //  Проверка таймаута.
//        IsTimeout(timeout, nameof(timeout));

//        //  Коллекция для хранения результата.
//        ConcurrentBag<ModbusConnect> connects = [];

//        //  Получение значений IP-адресов.
//        byte[] data = first.GetAddressBytes();
//        long firstIndex = ((long)data[0] << 24) + (data[1] << 16) + (data[2] << 8) + data[3];
//        data = last.GetAddressBytes();
//        long lastIndex = ((long)data[0] << 24) + (data[1] << 16) + (data[2] << 8) + data[3] + 1;

//        //  Создание списка IP-адресов.
//        List<IPAddress> addresses = [];

//        //  Перебор адресов.
//        for (long value = firstIndex; value < lastIndex; value++)
//        {
//            //  Получение данных адреса.
//            data[0] = (byte)(0xff & (value >> 24));
//            data[1] = (byte)(0xff & (value >> 16));
//            data[2] = (byte)(0xff & (value >> 8));
//            data[3] = (byte)(0xff & value);

//            //  Получение адреса.
//            IPAddress address = new(data);

//            //  Добавление адреса.
//            addresses.Add(address);
//        }

//        //  Перебор адресов.
//        await Parallel.ForEachAsync(
//            addresses,
//            cancellationToken,
//            async (address, cancellationToken) =>
//            {
//                //  Проверка токена отмены.
//                await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

//                //  Блок перехвата всех исключений.
//                try
//                {
//                    //  Создание соединения.
//                    ModbusConnect connect = new(address, slaveIdentifier)
//                    {
//                        Timeout = timeout,
//                    };

//                    //  Проверка соединения.
//                    if (await connect.TestConnectAsync(cancellationToken).ConfigureAwait(false))
//                    {
//                        //  Добавление соединения к результату.
//                        connects.Add(connect);
//                    }
//                }
//                catch { }
//            });

//        //  Возврат массива соединений
//        return [.. connects];
//    }

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
//        //  Проверка токена отмены.
//        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

//        //  Создание источника токена отмены по таймауту.
//        using CancellationTokenSource timeoutTokenSource = new();

//        //  Создание связанного токена отмены.
//        using CancellationTokenSource linkedTokenSource =
//            CancellationTokenSource.CreateLinkedTokenSource(
//                timeoutTokenSource.Token, cancellationToken);

//        //  Блокировка критического объекта.
//        using AsyncLocking locking = await _Lock.LockAsync(cancellationToken).ConfigureAwait(false);

//        //  Запуск таймаута.
//        timeoutTokenSource.CancelAfter(_Timeout);

//        //  Проверка соединения.
//        return await _TcpMaster.TestConnectAsync(linkedTokenSource.Token).ConfigureAwait(false);
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
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    /// <exception cref="TimeoutException">
//    /// Завершился таймаут ожидания.
//    /// </exception>
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
//        //  Проверка токена отмены.
//        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

//        //  Создание источника токена отмены по таймауту.
//        using CancellationTokenSource timeoutTokenSource = new();

//        //  Создание связанного токена отмены.
//        using CancellationTokenSource linkedTokenSource =
//            CancellationTokenSource.CreateLinkedTokenSource(
//                timeoutTokenSource.Token, cancellationToken);

//        //  Блокировка критического объекта.
//        using AsyncLocking locking = await _Lock.LockAsync(cancellationToken).ConfigureAwait(false);

//        //  Запуск таймаута.
//        timeoutTokenSource.CancelAfter(_Timeout);

//        //  Блок перехвата всех исключений.
//        try
//        {
//            //  Чтение регистров.
//            return await _TcpMaster.ReadHoldingsAsync(start, count,
//                linkedTokenSource.Token).ConfigureAwait(false);
//        }
//        catch
//        {
//            //  Проверка токена отмены по таймауту.
//            if (timeoutTokenSource.IsCancellationRequested)
//            {
//                //  Завершился таймаут ожидания.
//                throw new TimeoutException();
//            }

//            //  Повторный выброс исключения.
//            throw;
//        }
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
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    /// <exception cref="TimeoutException">
//    /// Завершился таймаут ожидания.
//    /// </exception>
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
//        //  Проверка токена отмены.
//        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

//        //  Создание источника токена отмены по таймауту.
//        using CancellationTokenSource timeoutTokenSource = new();

//        //  Создание связанного токена отмены.
//        using CancellationTokenSource linkedTokenSource =
//            CancellationTokenSource.CreateLinkedTokenSource(
//                timeoutTokenSource.Token, cancellationToken);

//        //  Блокировка критического объекта.
//        using AsyncLocking locking = await _Lock.LockAsync(cancellationToken).ConfigureAwait(false);

//        //  Запуск таймаута.
//        timeoutTokenSource.CancelAfter(_Timeout);

//        //  Блок перехвата всех исключений.
//        try
//        {
//            //  Запись регистров.
//            await _TcpMaster.WriteHoldingsAsync(start, registers,
//                linkedTokenSource.Token).ConfigureAwait(false);
//        }
//        catch
//        {
//            //  Проверка токена отмены по таймауту.
//            if (timeoutTokenSource.IsCancellationRequested)
//            {
//                //  Завершился таймаут ожидания.
//                throw new TimeoutException();
//            }

//            //  Повторный выброс исключения.
//            throw;
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
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    /// <exception cref="TimeoutException">
//    /// Завершился таймаут ожидания.
//    /// </exception>
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
//    public async Task<byte[]> ReadBytesAsync(int start, int count, CancellationToken cancellationToken)
//    {
//        //  Чтение регистров.
//        ushort[] registers = await ReadHoldingsAsync(
//            start, count, cancellationToken).ConfigureAwait(false);

//        //  Возврат массива байт.
//        return ToBytes(registers);
//    }

//    /// <summary>
//    /// Выполняет запись регистров хранения.
//    /// </summary>
//    /// <param name="start">
//    /// Номер первого регистра.
//    /// </param>
//    /// <param name="bytes">
//    /// Значения регистров.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    /// <exception cref="TimeoutException">
//    /// Завершился таймаут ожидания.
//    /// </exception>
//    /// <exception cref="ArgumentOutOfRangeException">
//    /// В параметре <paramref name="start"/> передано значение,
//    /// которое меньше значения <see cref="ushort.MinValue"/>
//    /// или больше значения <see cref="ushort.MaxValue"/>.
//    /// </exception>
//    /// <exception cref="ArgumentNullException">
//    /// В параметре <paramref name="bytes"/> передана пустая ссылка.
//    /// </exception>
//    /// <exception cref="InvalidDataException">
//    /// Неверный формат потока.
//    /// </exception>
//    [CLSCompliant(false)]
//    public async Task WriteBytesAsync(int start, byte[] bytes, CancellationToken cancellationToken)
//    {
//        //  Получение регистров.
//        ushort[] registers = FromBytes(bytes);

//        //  Запись значений.
//        await WriteHoldingsAsync(start, registers, cancellationToken).ConfigureAwait(false);
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
//    /// <param name="encoding">
//    /// Кодировка символов.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Массив прочитанных значений.
//    /// </returns>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    /// <exception cref="TimeoutException">
//    /// Завершился таймаут ожидания.
//    /// </exception>
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
//    public async Task<string> ReadStringAsync(int start, int count, Encoding encoding, CancellationToken cancellationToken)
//    {
//        //  Чтение регистров.
//        ushort[] registers = await ReadHoldingsAsync(
//            start, count, cancellationToken).ConfigureAwait(false);

//        //  Возврат прочитанной строки.
//        return ToString(registers, encoding);
//    }

//    /// <summary>
//    /// Выполняет чтение регистров хранения.
//    /// </summary>
//    /// <param name="start">
//    /// Номер первого регистра.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    [CLSCompliant(false)]
//    public async Task<uint> ReadUInt32Async(int start, CancellationToken cancellationToken)
//    {
//        //  Чтение регистров.
//        ushort[] registers = await ReadHoldingsAsync(
//            start, 2, cancellationToken).ConfigureAwait(false);

//        //  Возврат прочитанного значения.
//        return registers[1] | ((uint)registers[0] << 16);
//    }

//    /// <summary>
//    /// Выполняет чтение регистров хранения.
//    /// </summary>
//    /// <param name="start">
//    /// Номер первого регистра.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    public async Task<bool> ReadBooleanAsync(int start, CancellationToken cancellationToken)
//    {
//        //  Чтение регистров.
//        ushort[] registers = await ReadHoldingsAsync(
//            start, 1, cancellationToken).ConfigureAwait(false);

//        //  Возврат прочитанного значения.
//        return registers[0] != 0;
//    }

//    /// <summary>
//    /// Выполняет запись регистров хранения.
//    /// </summary>
//    /// <param name="start">
//    /// Номер первого регистра.
//    /// </param>
//    /// <param name="value">
//    /// Значения регистров.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    [CLSCompliant(false)]
//    public async Task WriteBooleanAsync(int start, bool value, CancellationToken cancellationToken)
//    {
//        //  Получение регистров.
//        ushort[] registers = [(ushort)(value ? 1: 0)];

//        //  Запись регистров.
//        await WriteHoldingsAsync(start, registers, cancellationToken).ConfigureAwait(false);
//    }

//    /// <summary>
//    /// Выполняет чтение регистров хранения.
//    /// </summary>
//    /// <param name="start">
//    /// Номер первого регистра.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    [CLSCompliant(false)]
//    public async Task<ushort> ReadUInt16Async(int start, CancellationToken cancellationToken)
//    {
//        //  Чтение регистров.
//        ushort[] registers = await ReadHoldingsAsync(
//            start, 1, cancellationToken).ConfigureAwait(false);

//        //  Возврат прочитанного значения.
//        return registers[0];
//    }

//    /// <summary>
//    /// Выполняет запись регистров хранения.
//    /// </summary>
//    /// <param name="start">
//    /// Номер первого регистра.
//    /// </param>
//    /// <param name="value">
//    /// Значения регистров.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    [CLSCompliant(false)]
//    public async Task WriteUInt16Async(int start, ushort value, CancellationToken cancellationToken)
//    {
//        //  Получение регистров.
//        ushort[] registers = { value };

//        //  Запись регистров.
//        await WriteHoldingsAsync(start, registers, cancellationToken).ConfigureAwait(false);
//    }

//    /// <summary>
//    /// Выполняет чтение регистров хранения.
//    /// </summary>
//    /// <param name="start">
//    /// Номер первого регистра.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    public async Task<float> ReadFloat32Async(int start, CancellationToken cancellationToken)
//    {
//        //  Чтение регистров.
//        ushort[] registers = await ReadHoldingsAsync(
//            start, 2, cancellationToken).ConfigureAwait(false);

//        //  Возврат прочитанного значения.
//        return ToFloat32(registers);
//    }

//    /// <summary>
//    /// Выполняет запись регистров хранения.
//    /// </summary>
//    /// <param name="start">
//    /// Номер первого регистра.
//    /// </param>
//    /// <param name="value">
//    /// Значения регистров.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    [CLSCompliant(false)]
//    public async Task WriteFloat32Async(int start, float value, CancellationToken cancellationToken)
//    {
//        //  Получение регистров.
//        ushort[] registers = FromFloat32(value);

//        //  Запись регистров.
//        await WriteHoldingsAsync(start, registers, cancellationToken).ConfigureAwait(false);
//    }

//    /// <summary>
//    /// Выполняет чтение регистров хранения.
//    /// </summary>
//    /// <param name="start">
//    /// Номер первого регистра.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    public async Task<IPv4Address> ReadIPv4AddressAsync(int start, CancellationToken cancellationToken)
//    {
//        //  Чтение регистров.
//        ushort[] registers = await ReadHoldingsAsync(
//            start, 2, cancellationToken).ConfigureAwait(false);

//        //  Возврат прочитанного значения.
//        return ToIPv4Address(registers);
//    }

//    /// <summary>
//    /// Выполняет запись регистров хранения.
//    /// </summary>
//    /// <param name="start">
//    /// Номер первого регистра.
//    /// </param>
//    /// <param name="value">
//    /// Значения регистров.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    [CLSCompliant(false)]
//    public async Task WriteIPv4AddressAsync(int start, IPv4Address value, CancellationToken cancellationToken)
//    {
//        //  Получение регистров.
//        ushort[] registers = FromIPv4Address(value);

//        //  Запись регистров.
//        await WriteHoldingsAsync(start, registers, cancellationToken).ConfigureAwait(false);
//    }

//    /// <summary>
//    /// Преобразует значение регистров в значение типа <see cref="IPv4Address"/>.
//    /// </summary>
//    /// <param name="registers">
//    /// Массив регистров.
//    /// </param>
//    /// <returns>
//    /// Значение типа <see cref="float"/>.
//    /// </returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private static IPv4Address ToIPv4Address([NoVerify] ushort[] registers)
//    {
//        //  Создание массива данных.
//        byte[] data = ToBytes(registers);

//        //  Возврат прочитанного значения.
//        return new(new IPAddress(data));
//    }


//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private static ushort[] FromIPv4Address(IPv4Address value)
//    {
//        //  Получение массива байт.
//        byte[] data = value.GetAddressBytes();

//        //  Возврат массива регистров.
//        return FromBytes(data);
//    }

//    /// <summary>
//    /// Преобразует значение регистров в значение типа <see cref="float"/>.
//    /// </summary>
//    /// <param name="registers">
//    /// Массив регистров.
//    /// </param>
//    /// <returns>
//    /// Значение типа <see cref="float"/>.
//    /// </returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private static float ToFloat32([NoVerify] ushort[] registers)
//    {
//        //  Создание массива данных.
//        byte[] data = ToBytes(registers);

//        //  Возврат прочитанного значения.
//        return BitConverter.ToSingle(data);
//    }

//    /// <summary>
//    /// Преобразует значение регистров в текстовое представление.
//    /// </summary>
//    /// <param name="registers">
//    /// Массив регистров.
//    /// </param>
//    /// <param name="encoding">
//    /// Кодировка символов.
//    /// </param>
//    /// <returns>
//    /// Текстовое представление.
//    /// </returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private static string ToString([NoVerify] ushort[] registers, Encoding encoding)
//    {
//        //  Создание массива данных.
//        byte[] data = ToBytes(registers);

//        //  Возврат прочитанной строки.
//        return encoding.GetString(data).Trim('\0');
//    }

//    /// <summary>
//    /// Преобразует значение регистров в массив байт.
//    /// </summary>
//    /// <param name="registers">
//    /// Массив регистров.
//    /// </param>
//    /// <returns>
//    /// Массив байт.
//    /// </returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private static byte[] ToBytes([NoVerify] ushort[] registers)
//    {
//        //  Создание массива данных.
//        byte[] data = new byte[registers.Length << 1];

//        //  Заполнение массива данных.
//        for (int i = 0; i < registers.Length; i++)
//        {
//            data[2 * i] = unchecked((byte)registers[i]);
//            data[2 * i + 1] = unchecked((byte)(registers[i] >> 8));
//        }

//        //  Возврат массива данных.
//        return data;
//    }

//    /// <summary>
//    /// Преобразует значение типа <see cref="float"/> в массив регистров.
//    /// </summary>
//    /// <param name="value">
//    /// Значение.
//    /// </param>
//    /// <returns>
//    /// Массив регистров.
//    /// </returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private static ushort[] FromFloat32([NoVerify] float value)
//    {
//        //  Создание массива данных.
//        byte[] data = BitConverter.GetBytes(value);

//        //  Возврат прочитанного значения.
//        return FromBytes(data);
//    }

//    /// <summary>
//    /// Преобразует массив байт в массив регистров.
//    /// </summary>
//    /// <param name="bytes">
//    /// Массив байт.
//    /// </param>
//    /// <returns>
//    /// Массив регистров.
//    /// </returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private static ushort[] FromBytes([NoVerify] byte[] bytes)
//    {
//        //  Созадние массива регистров.
//        ushort[] registers = new ushort[bytes.Length >> 1];

//        //  Заполнение массива байт.
//        for (int i = 0; i < registers.Length; i++)
//        {
//            registers[i] = (ushort)(bytes[2 * i] | (bytes[2 * i + 1] << 8));
//        }

//        //  Возврат массива регистров.
//        return registers;
//    }
//}
