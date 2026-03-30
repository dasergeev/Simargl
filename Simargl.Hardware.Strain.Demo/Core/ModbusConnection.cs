using Simargl.Hardware.Modbus.Core;
using Simargl.Hardware.Strain.Demo.ReWrite;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Simargl.Hardware.Strain.Demo.Core;

/// <summary>
/// Представляет Modbus-соединение.
/// </summary>
public sealed class ModbusConnection :
    Anything
{
    /// <summary>
    /// Поле для хранения таймаута.
    /// </summary>
    private int _Timeout = System.Threading.Timeout.Infinite;

    /// <summary>
    /// Поле для хранения очереди транзакций.
    /// </summary>
    private readonly ConcurrentQueue<ModbusTransaction> _Transaction;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="transaction">
    /// Очередь транзакций.
    /// </param>
    public ModbusConnection(ConcurrentQueue<ModbusTransaction> transaction)
    {
        //  Обращение к объекту.
        Lay();

        //  Установка очереди транзакций.
        _Transaction = transaction;
    }

    /// <summary>
    /// Возвращает или задаёт таймаут ожидания.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передан недопустимый таймаут.
    /// </exception>
    public TimeSpan Timeout
    {
        get => TimeSpan.FromMilliseconds(_Timeout);
        set => _Timeout = IsTimeout(value, nameof(Timeout));
    }

    /// <summary>
    /// Асинхронно выполняет транзакцию.
    /// </summary>
    /// <param name="request">
    /// Запрос.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая транзакцию.
    /// </returns>
    private async Task<TcpAduPackage> TransactionAsync(TcpAduPackage request, CancellationToken cancellationToken)
    {
        //  Создание источника завершения задачи.
        TaskCompletionSource<TcpAduPackage> taskCompletionSource = new();

        //  Добавление завершения при отмене.
        cancellationToken.Register(delegate
        {
            //  Установка состояния отмены.
            taskCompletionSource.TrySetCanceled();
        });

        //  Создание транзакции.
        ModbusTransaction transaction = new(request, taskCompletionSource);

        //  Добавление транзакции в очередь.
        _Transaction.Enqueue(transaction);

        //  Ожидание выполнения.
        return await taskCompletionSource.Task.ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение регистров хранения.
    /// </summary>
    /// <param name="start">
    /// Номер первого регистра.
    /// </param>
    /// <param name="count">
    /// Количество регистров.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Массив прочитанных значений.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="TimeoutException">
    /// Завершился таймаут ожидания.
    /// </exception>
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
    public async Task<ushort[]> ReadHoldingsAsync(int start, int count, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание источника токена отмены по таймауту.
        using CancellationTokenSource timeoutTokenSource = new();

        //  Создание связанного токена отмены.
        using CancellationTokenSource linkedTokenSource =
            CancellationTokenSource.CreateLinkedTokenSource(
                timeoutTokenSource.Token, cancellationToken);

        //  Запуск таймаута.
        timeoutTokenSource.CancelAfter(_Timeout);

        //  Блок перехвата всех исключений.
        try
        {
            //  Чтение регистров.
            return await ReadHoldingsCoreAsync(start, count,
                linkedTokenSource.Token).ConfigureAwait(false);
        }
        catch
        {
            //  Проверка токена отмены по таймауту.
            if (timeoutTokenSource.IsCancellationRequested)
            {
                //  Завершился таймаут ожидания.
                throw new TimeoutException();
            }

            //  Повторный выброс исключения.
            throw;
        }
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
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="TimeoutException">
    /// Завершился таймаут ожидания.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="start"/> передано значение,
    /// которое меньше значения <see cref="ushort.MinValue"/>
    /// или больше значения <see cref="ushort.MaxValue"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="registers"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверный формат потока.
    /// </exception>
    [CLSCompliant(false)]
    public async Task WriteHoldingsAsync(int start, ushort[] registers, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание источника токена отмены по таймауту.
        using CancellationTokenSource timeoutTokenSource = new();

        //  Создание связанного токена отмены.
        using CancellationTokenSource linkedTokenSource =
            CancellationTokenSource.CreateLinkedTokenSource(
                timeoutTokenSource.Token, cancellationToken);

        //  Запуск таймаута.
        timeoutTokenSource.CancelAfter(_Timeout);

        //  Блок перехвата всех исключений.
        try
        {
            //  Запись регистров.
            await WriteHoldingsCoreAsync(start, registers,
                linkedTokenSource.Token).ConfigureAwait(false);
        }
        catch
        {
            //  Проверка токена отмены по таймауту.
            if (timeoutTokenSource.IsCancellationRequested)
            {
                //  Завершился таймаут ожидания.
                throw new TimeoutException();
            }

            //  Повторный выброс исключения.
            throw;
        }
    }

    /// <summary>
    /// Выполняет чтение регистров хранения.
    /// </summary>
    /// <param name="start">
    /// Номер первого регистра.
    /// </param>
    /// <param name="count">
    /// Количество регистров.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Массив прочитанных значений.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="TimeoutException">
    /// Завершился таймаут ожидания.
    /// </exception>
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
    public async Task<byte[]> ReadBytesAsync(int start, int count, CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        ushort[] registers = await ReadHoldingsAsync(
            start, count, cancellationToken).ConfigureAwait(false);

        //  Возврат массива байт.
        return ToBytes(registers);
    }

    /// <summary>
    /// Выполняет запись регистров хранения.
    /// </summary>
    /// <param name="start">
    /// Номер первого регистра.
    /// </param>
    /// <param name="bytes">
    /// Значения регистров.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="TimeoutException">
    /// Завершился таймаут ожидания.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="start"/> передано значение,
    /// которое меньше значения <see cref="ushort.MinValue"/>
    /// или больше значения <see cref="ushort.MaxValue"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="bytes"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверный формат потока.
    /// </exception>
    [CLSCompliant(false)]
    public async Task WriteBytesAsync(int start, byte[] bytes, CancellationToken cancellationToken)
    {
        //  Получение регистров.
        ushort[] registers = FromBytes(bytes);

        //  Запись значений.
        await WriteHoldingsAsync(start, registers, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение регистров хранения.
    /// </summary>
    /// <param name="start">
    /// Номер первого регистра.
    /// </param>
    /// <param name="count">
    /// Количество регистров.
    /// </param>
    /// <param name="encoding">
    /// Кодировка символов.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Массив прочитанных значений.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="TimeoutException">
    /// Завершился таймаут ожидания.
    /// </exception>
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
    public async Task<string> ReadStringAsync(int start, int count, Encoding encoding, CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        ushort[] registers = await ReadHoldingsAsync(
            start, count, cancellationToken).ConfigureAwait(false);

        //  Возврат прочитанной строки.
        return ToString(registers, encoding);
    }

    /// <summary>
    /// Выполняет чтение регистров хранения.
    /// </summary>
    /// <param name="start">
    /// Номер первого регистра.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    [CLSCompliant(false)]
    public async Task<uint> ReadUInt32Async(int start, CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        ushort[] registers = await ReadHoldingsAsync(
            start, 2, cancellationToken).ConfigureAwait(false);

        //  Возврат прочитанного значения.
        return registers[1] | ((uint)registers[0] << 16);
    }

    /// <summary>
    /// Выполняет чтение регистров хранения.
    /// </summary>
    /// <param name="start">
    /// Номер первого регистра.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    public async Task<bool> ReadBooleanAsync(int start, CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        ushort[] registers = await ReadHoldingsAsync(
            start, 1, cancellationToken).ConfigureAwait(false);

        //  Возврат прочитанного значения.
        return registers[0] != 0;
    }

    /// <summary>
    /// Выполняет запись регистров хранения.
    /// </summary>
    /// <param name="start">
    /// Номер первого регистра.
    /// </param>
    /// <param name="value">
    /// Значения регистров.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    [CLSCompliant(false)]
    public async Task WriteBooleanAsync(int start, bool value, CancellationToken cancellationToken)
    {
        //  Получение регистров.
        ushort[] registers = [(ushort)(value ? 1 : 0)];

        //  Запись регистров.
        await WriteHoldingsAsync(start, registers, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение регистров хранения.
    /// </summary>
    /// <param name="start">
    /// Номер первого регистра.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    [CLSCompliant(false)]
    public async Task<ushort> ReadUInt16Async(int start, CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        ushort[] registers = await ReadHoldingsAsync(
            start, 1, cancellationToken).ConfigureAwait(false);

        //  Возврат прочитанного значения.
        return registers[0];
    }

    /// <summary>
    /// Выполняет запись регистров хранения.
    /// </summary>
    /// <param name="start">
    /// Номер первого регистра.
    /// </param>
    /// <param name="value">
    /// Значения регистров.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    [CLSCompliant(false)]
    public async Task WriteUInt16Async(int start, ushort value, CancellationToken cancellationToken)
    {
        //  Получение регистров.
        ushort[] registers = [value];

        //  Запись регистров.
        await WriteHoldingsAsync(start, registers, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение регистров хранения.
    /// </summary>
    /// <param name="start">
    /// Номер первого регистра.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    public async Task<float> ReadFloat32Async(int start, CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        ushort[] registers = await ReadHoldingsAsync(
            start, 2, cancellationToken).ConfigureAwait(false);

        //  Возврат прочитанного значения.
        return ToFloat32(registers);
    }

    /// <summary>
    /// Выполняет запись регистров хранения.
    /// </summary>
    /// <param name="start">
    /// Номер первого регистра.
    /// </param>
    /// <param name="value">
    /// Значения регистров.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    [CLSCompliant(false)]
    public async Task WriteFloat32Async(int start, float value, CancellationToken cancellationToken)
    {
        //  Получение регистров.
        ushort[] registers = FromFloat32(value);

        //  Запись регистров.
        await WriteHoldingsAsync(start, registers, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение регистров хранения.
    /// </summary>
    /// <param name="start">
    /// Номер первого регистра.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    public async Task<IPv4Address> ReadIPv4AddressAsync(int start, CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        ushort[] registers = await ReadHoldingsAsync(
            start, 2, cancellationToken).ConfigureAwait(false);

        //  Возврат прочитанного значения.
        return ToIPv4Address(registers);
    }

    /// <summary>
    /// Выполняет запись регистров хранения.
    /// </summary>
    /// <param name="start">
    /// Номер первого регистра.
    /// </param>
    /// <param name="value">
    /// Значения регистров.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    [CLSCompliant(false)]
    public async Task WriteIPv4AddressAsync(int start, IPv4Address value, CancellationToken cancellationToken)
    {
        //  Получение регистров.
        ushort[] registers = FromIPv4Address(value);

        //  Запись регистров.
        await WriteHoldingsAsync(start, registers, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Преобразует значение регистров в значение типа <see cref="IPv4Address"/>.
    /// </summary>
    /// <param name="registers">
    /// Массив регистров.
    /// </param>
    /// <returns>
    /// Значение типа <see cref="float"/>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static IPv4Address ToIPv4Address([NoVerify] ushort[] registers)
    {
        //  Создание массива данных.
        byte[] data = ToBytes(registers);

        //  Заглушка.
        (data[0], data[2]) = (data[2], data[0]);
        (data[1], data[3]) = (data[3], data[1]);

        //  Возврат прочитанного значения.
        return new(new IPAddress(data));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ushort[] FromIPv4Address(IPv4Address value)
    {
        //  Получение массива байт.
        byte[] data = value.GetAddressBytes();

        //  Заглушка.
        (data[0], data[2]) = (data[2], data[0]);
        (data[1], data[3]) = (data[3], data[1]);

        //  Возврат массива регистров.
        return FromBytes(data);
    }

    /// <summary>
    /// Преобразует значение регистров в значение типа <see cref="float"/>.
    /// </summary>
    /// <param name="registers">
    /// Массив регистров.
    /// </param>
    /// <returns>
    /// Значение типа <see cref="float"/>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static float ToFloat32([NoVerify] ushort[] registers)
    {
        //  Создание массива данных.
        byte[] data = ToBytes(registers);

        //  Заглушка.
        (data[0], data[2]) = (data[2], data[0]);
        (data[1], data[3]) = (data[3], data[1]);


        //  Возврат прочитанного значения.
        return BitConverter.ToSingle(data);
    }

    /// <summary>
    /// Преобразует значение регистров в текстовое представление.
    /// </summary>
    /// <param name="registers">
    /// Массив регистров.
    /// </param>
    /// <param name="encoding">
    /// Кодировка символов.
    /// </param>
    /// <returns>
    /// Текстовое представление.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string ToString([NoVerify] ushort[] registers, Encoding encoding)
    {
        //  Создание массива данных.
        byte[] data = ToBytes(registers);

        //  Заглушка.
        for (int i = 0; i < data.Length; i += 2)
        {
            (data[i], data[i + 1]) = (data[i + 1], data[i]);
        }

        //  Возврат прочитанной строки.
        return encoding.GetString(data).Trim('\0');
    }

    /// <summary>
    /// Преобразует значение регистров в массив байт.
    /// </summary>
    /// <param name="registers">
    /// Массив регистров.
    /// </param>
    /// <returns>
    /// Массив байт.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static byte[] ToBytes([NoVerify] ushort[] registers)
    {
        //  Создание массива данных.
        byte[] data = new byte[registers.Length << 1];

        //  Заполнение массива данных.
        for (int i = 0; i < registers.Length; i++)
        {
            data[2 * i] = unchecked((byte)registers[i]);
            data[2 * i + 1] = unchecked((byte)(registers[i] >> 8));
        }

        //  Возврат массива данных.
        return data;
    }

    /// <summary>
    /// Преобразует значение типа <see cref="float"/> в массив регистров.
    /// </summary>
    /// <param name="value">
    /// Значение.
    /// </param>
    /// <returns>
    /// Массив регистров.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ushort[] FromFloat32([NoVerify] float value)
    {
        //  Создание массива данных.
        byte[] data = BitConverter.GetBytes(value);

        //  Заглушка.
        (data[0], data[2]) = (data[2], data[0]);
        (data[1], data[3]) = (data[3], data[1]);

        //  Возврат прочитанного значения.
        return FromBytes(data);
    }

    /// <summary>
    /// Преобразует массив байт в массив регистров.
    /// </summary>
    /// <param name="bytes">
    /// Массив байт.
    /// </param>
    /// <returns>
    /// Массив регистров.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ushort[] FromBytes([NoVerify] byte[] bytes)
    {
        //  Созадние массива регистров.
        ushort[] registers = new ushort[bytes.Length >> 1];

        //  Заполнение массива байт.
        for (int i = 0; i < registers.Length; i++)
        {
            registers[i] = (ushort)(bytes[2 * i] | (bytes[2 * i + 1] << 8));
        }

        //  Возврат массива регистров.
        return registers;
    }

    /// <summary>
    /// Поле для хранения идентификатора транзакции.
    /// </summary>
    private int _TransactionIdentifier;

    /// <summary>
    /// Возвращает идентификатор устройства.
    /// </summary>
    private byte SlaveIdentifier { get; } = 1;

    /// <summary>
    /// Выполняет чтение регистров хранения.
    /// </summary>
    /// <param name="start">
    /// Номер первого регистра.
    /// </param>
    /// <param name="count">
    /// Количество регистров.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
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
    private async Task<ushort[]> ReadHoldingsCoreAsync(int start, int count, CancellationToken cancellationToken)
    {
        //  Проверка входных параметров.
        IsInRange(start, ushort.MinValue, ushort.MaxValue, nameof(start));
        IsInRange(count, byte.MinValue, byte.MaxValue, nameof(count));

        //  Создание запроса.
        TcpAduPackage request = RequestReadHoldings(start, count);

        //  Выполнение транзакции.
        TcpAduPackage response = await TransactionAsync(request, cancellationToken).ConfigureAwait(false);

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
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="start"/> передано значение,
    /// которое меньше значения <see cref="ushort.MinValue"/>
    /// или больше значения <see cref="ushort.MaxValue"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="registers"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверный формат потока.
    /// </exception>
    private async Task WriteHoldingsCoreAsync(int start, ushort[] registers, CancellationToken cancellationToken)
    {
        //  Проверка номера первого регистра.
        IsInRange(start, ushort.MinValue, ushort.MaxValue, nameof(start));

        //  Проверка ссылки на массив значений регистров.
        IsNotNull(registers, nameof(registers));

        //  Создание запроса.
        TcpAduPackage request = RequestWriteHoldings(start, registers);

        //  Выполнение транзакции.
        TcpAduPackage response = await TransactionAsync(request, cancellationToken).ConfigureAwait(false);

        //  Проверка ответа.
        IsResponse(response, request);

        //  Проверка размера полученных данных.
        if (response.PduPackage.Data.Length != 4)
        {
            //  Неверный формат потока.
            throw new InvalidDataException();
        }

        //  Чтение первого адресса.
        int startAddress = (response.PduPackage.Data[0] << 8) | response.PduPackage.Data[1];

        //  Чтение количества регистров.
        int countRegister = (response.PduPackage.Data[2] << 8) | response.PduPackage.Data[3];

        //  Проверка прочитанных значений.
        if (startAddress != start || countRegister != registers.Length)
        {
            //  Неверный формат потока.
            throw new InvalidDataException();
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
    private TcpAduPackage RequestReadHoldings([NoVerify] int start, [NoVerify] int count)
    {
        //  Получение идентификатора транзакции.
        int transactionIdentifier = Interlocked.Increment(ref _TransactionIdentifier);

        //  Создание запроса.
        TcpAduPackage request = new(transactionIdentifier, SlaveIdentifier,
            PduFunctionCode.ReadHoldings,
            [
                unchecked((byte)(start >> 8)),
                unchecked((byte)(start >> 0)),
                unchecked((byte)(count >> 8)),
                unchecked((byte)(count >> 0))
            ]);

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
    private TcpAduPackage RequestWriteHoldings([NoVerify] int start, [NoVerify] ushort[] registers)
    {
        //  Созадние списка байт.
        List<byte> bytes =
        [
            unchecked((byte)(start >> 8)),
            unchecked((byte)(start >> 0)),
        ];

        //  Получение количества регистров.
        int count = registers.Length;

        //  Запись количества регистров.
        bytes.AddRange(
        [
            unchecked((byte)(count >> 8)),
            unchecked((byte)(count >> 0)),
        ]);

        //  Определение длинны данных.
        byte length = (byte)(count * 2);

        //  Запись длинны данных
        bytes.Add(length);

        //  Перебор регистров.
        for (int i = 0; i < count; i++)
        {
            //  Запись значения регистра.
            bytes.AddRange(
            [
                unchecked((byte)(registers[i] >> 8)),
                unchecked((byte)(registers[i] >> 0)),
            ]);
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
        [NoVerify] TcpAduPackage response,
        [NoVerify] TcpAduPackage request)
    {
        if (response.Header.TransactionIdentifier != request.Header.TransactionIdentifier ||
            response.Header.SlaveIdentifier != request.Header.SlaveIdentifier ||
            response.PduPackage.FunctionCode != request.PduPackage.FunctionCode)
        {
            //  Неверный формат потока.
            throw new InvalidDataException();
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
    private static ushort[] ToHoldings([NoVerify] TcpAduPackage package)
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
