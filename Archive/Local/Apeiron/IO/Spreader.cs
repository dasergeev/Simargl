using System.Text;

namespace Apeiron.IO;

/// <summary>
/// Представляет распределитель данных потока.
/// </summary>
public sealed partial class Spreader
{
    /// <summary>
    /// Постоянная, определяющая размер значения типа <see cref="bool"/> в потоке.
    /// </summary>
    private const int _SizeOfBoolean = 1;

    /// <summary>
    /// Постоянная, определяющая размер значения типа <see cref="sbyte"/> в потоке.
    /// </summary>
    private const int _SizeOfSByte = 1;

    /// <summary>
    /// Постоянная, определяющая размер значения типа <see cref="byte"/> в потоке.
    /// </summary>
    private const int _SizeOfByte = 1;

    /// <summary>
    /// Постоянная, определяющая размер значения типа <see cref="short"/> в потоке.
    /// </summary>
    private const int _SizeOfInt16 = 2;

    /// <summary>
    /// Постоянная, определяющая размер значения типа <see cref="ushort"/> в потоке.
    /// </summary>
    private const int _SizeOfUInt16 = 2;

    /// <summary>
    /// Постоянная, определяющая размер значения типа <see cref="int"/> в потоке.
    /// </summary>
    private const int _SizeOfInt32 = 4;

    /// <summary>
    /// Постоянная, определяющая размер значения типа <see cref="uint"/> в потоке.
    /// </summary>
    private const int _SizeOfUInt32 = 4;

    /// <summary>
    /// Постоянная, определяющая размер значения типа <see cref="long"/> в потоке.
    /// </summary>
    private const int _SizeOfInt64 = 8;

    /// <summary>
    /// Постоянная, определяющая размер значения типа <see cref="ulong"/> в потоке.
    /// </summary>
    private const int _SizeOfUInt64 = 8;

    /// <summary>
    /// Постоянная, определяющая размер значения типа <see cref="float"/> в потоке.
    /// </summary>
    private const int _SizeOfSingle = 4;

    /// <summary>
    /// Постоянная, определяющая размер значения типа <see cref="double"/> в потоке.
    /// </summary>
    private const int _SizeOfDouble = 8;

    /// <summary>
    /// Постоянная, определяющая размер значения типа <see cref="decimal"/> в потоке.
    /// </summary>
    private const int _SizeOfDecimal = 16;

    /// <summary>
    /// Поле для хранения потока, с которым связан распределитель данных.
    /// </summary>
    private readonly Stream _Stream;

    /// <summary>
    /// Поле для хранения буфера распределителя данных потока.
    /// </summary>
    private readonly SpreaderBuffer _Buffer;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="stream">
    /// Поток, данные которого необходимо распределять.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    public Spreader(Stream stream) :
        this(stream, Encoding.UTF8)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="stream">
    /// Поток, данные которого необходимо распределять.
    /// </param>
    /// <param name="encoding">
    /// Кодировка, которую необходимо использовать при распределении данных потока.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="encoding"/> передана пустая ссылка.
    /// </exception>
    public Spreader(Stream stream, Encoding encoding)
    {
        //  Создание буфера.
        _Buffer = new(stream, encoding);

        //  Установка потока.
        _Stream = stream;

        //  Установка кодировки.
        Encoding = encoding;

        //  Установка текущей позиции записи.
        WritePosition = 0;
    }

    /// <summary>
    /// Возвращает кодировку, которую использует распределитель данных.
    /// </summary>
    public Encoding Encoding { get; }

    /// <summary>
    /// Возвращает текущую позицию чтения.
    /// </summary>
    public int ReadPosition
    {
        get => _Buffer.ReadPosition;
        private set => _Buffer.ReadPosition = value;
    }

    /// <summary>
    /// Возвращает текущую позицию записи.
    /// </summary>
    public int WritePosition { get; private set; }

    /// <summary>
    /// Считывает указанное количество байтов из потока, начиная с заданной точки в массиве байтов.
    /// </summary>
    /// <param name="buffer">
    /// Буфер, в который должны считываться данные.
    /// </param>
    /// <param name="index">
    /// Стартовая точка в буфере, начиная с которой считываемые данные записываются в буфер.
    /// </param>
    /// <param name="count">
    /// Количество байтов, чтение которых необходимо выполнить.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// В параметре <paramref name="buffer"/> передан массив,
    /// который не вмещает <paramref name="count"/> элементов начиная с позиции <paramref name="index"/>.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public void ReadBytes(byte[] buffer, int index, int count)
    {
        //  Проверка буфера.
        IsRange(buffer, index, count, nameof(buffer), nameof(index), nameof(count));

        //  Смещение позиции чтения.
        int offset = 0;

        //  Цикл чтения.
        while (offset < count)
        {
            //  Чтение из потока.
            int readBytes = _Stream.Read(buffer, index + offset, count - offset);

            //  Корректировка позиции чтения.
            ReadPosition += readBytes;

            //  Проверка количества полученных байт.
            if (readBytes == 0)
            {
                //  Достигнут конец потока.
                throw Exceptions.StreamEnd();
            }

            //  Смещение позиции чтения.
            offset += readBytes;
        }
    }

    /// <summary>
    /// Асинхронно считывает указанное количество байтов из потока, начиная с заданной точки в массиве байтов.
    /// </summary>
    /// <param name="buffer">
    /// Буфер, в который должны считываться данные.
    /// </param>
    /// <param name="index">
    /// Стартовая точка в буфере, начиная с которой считываемые данные записываются в буфер.
    /// </param>
    /// <param name="count">
    /// Количество байтов, чтение которых необходимо выполнить.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// В параметре <paramref name="buffer"/> передан массив,
    /// который не вмещает <paramref name="count"/> элементов начиная с позиции <paramref name="index"/>.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public async Task ReadBytesAsync(byte[] buffer, int index, int count, CancellationToken cancellationToken)
    {
        //  Проверка буфера.
        IsRange(buffer, index, count, nameof(buffer), nameof(index), nameof(count));

        //  Смещение позиции чтения.
        int offset = 0;

        //  Цикл чтения.
        while (offset < count)
        {
            //  Проверка токена отмены.
            await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Чтение из потока.
            int readBytes = await _Stream.ReadAsync(
                buffer.AsMemory(index + offset, count - offset), cancellationToken).ConfigureAwait(false);

            //  Корректировка позиции на чтение.
            ReadPosition += readBytes;

            //  Проверка количества полученных байт.
            if (readBytes == 0)
            {
                //  Достигнут конец потока.
                throw Exceptions.StreamEnd();
            }

            //  Смещение позиции чтения.
            offset += readBytes;
        }
    }

    /// <summary>
    /// Асинхронно считывает указанное количество байтов
    /// из текущего потока в массив байтов
    /// и перемещает текущую позицию на это количество байтов.
    /// </summary>
    /// <param name="count">
    /// Количество байтов, чтение которых необходимо выполнить.
    /// </param>
    /// <returns>
    /// Прочитанные данные.
    /// </returns>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public byte[] ReadBytes(int count)
    {
        //  Проверка количества байтов.
        IsNotNegative(count, nameof(count));

        //  Проверка необходимости чтения.
        if (count != 0)
        {
            //  Создание массива байтов.
            byte[] buffer = new byte[count];

            //  Заполнение массива байтов.
            ReadBytes(buffer, 0, count);

            //  Возврат масива байт.
            return buffer;
        }
        else
        {
            //  Возврат пустого массива.
            return Array.Empty<byte>();
        }
    }

    /// <summary>
    /// Асинхронно считывает указанное количество байтов
    /// из текущего потока в массив байтов
    /// и перемещает текущую позицию на это количество байтов.
    /// </summary>
    /// <param name="count">
    /// Количество байтов, чтение которых необходимо выполнить.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public async Task<byte[]> ReadBytesAsync(int count, CancellationToken cancellationToken)
    {
        //  Проверка количества байтов.
        IsNotNegative(count, nameof(count));

        //  Проверка необходимости чтения.
        if (count != 0)
        {
            //  Создание массива байтов.
            byte[] buffer = new byte[count];

            //  Заполнение массива байтов.
            await ReadBytesAsync(buffer, 0, count, cancellationToken).ConfigureAwait(false);

            //  Возврат масива байт.
            return buffer;
        }
        else
        {
            //  Возврат пустого массива.
            return Array.Empty<byte>();
        }
    }

    /// <summary>
    /// Считывает значение типа <see cref="bool"/>
    /// из текущего потока и перемещает текущую позицию в потоке на один байт вперед.
    /// </summary>
    /// <returns>
    /// Прочитанное значение.
    /// </returns>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public bool ReadBoolean()
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Загрузка в буфер данных из исходного потока.
        _Buffer.Load(_SizeOfBoolean);

        //  Чтение значения из буфера.
        return _Buffer.Reader.ReadBoolean();
    }

    /// <summary>
    /// Асинхронно считывает значение типа <see cref="bool"/>
    /// из текущего потока и перемещает текущую позицию в потоке на один байт вперед.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public async Task<bool> ReadBooleanAsync(CancellationToken cancellationToken)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Загрузка в буфер данных из исходного потока.
        await _Buffer.LoadAsync(_SizeOfBoolean, cancellationToken).ConfigureAwait(false);

        //  Чтение значения из буфера.
        return _Buffer.Reader.ReadBoolean();
    }

    /// <summary>
    /// Считывает значение типа <see cref="sbyte"/>
    /// из текущего потока и перемещает текущую позицию в потоке на один байт вперед.
    /// </summary>
    /// <returns>
    /// Прочитанное значение.
    /// </returns>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    [CLSCompliant(false)]
    public sbyte ReadInt8()
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Загрузка в буфер данных из исходного потока.
        _Buffer.Load(_SizeOfSByte);

        //  Чтение значения из буфера.
        return _Buffer.Reader.ReadSByte();
    }

    /// <summary>
    /// Асинхронно считывает значение типа <see cref="sbyte"/>
    /// из текущего потока и перемещает текущую позицию в потоке на один байт вперед.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    [CLSCompliant(false)]
    public async Task<sbyte> ReadInt8Async(CancellationToken cancellationToken)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Загрузка в буфер данных из исходного потока.
        await _Buffer.LoadAsync(_SizeOfSByte, cancellationToken).ConfigureAwait(false);

        //  Чтение значения из буфера.
        return _Buffer.Reader.ReadSByte();
    }

    /// <summary>
    /// Считывает значение типа <see cref="byte"/>
    /// из текущего потока и перемещает текущую позицию в потоке на один байт вперед.
    /// </summary>
    /// <returns>
    /// Прочитанное значение.
    /// </returns>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public byte ReadUInt8()
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Загрузка в буфер данных из исходного потока.
        _Buffer.Load(_SizeOfByte);

        //  Чтение значения из буфера.
        return _Buffer.Reader.ReadByte();
    }

    /// <summary>
    /// Асинхронно считывает значение типа <see cref="byte"/>
    /// из текущего потока и перемещает текущую позицию в потоке на один байт вперед.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public async Task<byte> ReadUInt8Async(CancellationToken cancellationToken)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Загрузка в буфер данных из исходного потока.
        await _Buffer.LoadAsync(_SizeOfByte, cancellationToken).ConfigureAwait(false);

        //  Чтение значения из буфера.
        return _Buffer.Reader.ReadByte();
    }

    /// <summary>
    /// Считывает значение типа <see cref="short"/>
    /// из текущего потока и перемещает текущую позицию в потоке на два байта вперед.
    /// </summary>
    /// <returns>
    /// Прочитанное значение.
    /// </returns>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public short ReadInt16()
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Загрузка в буфер данных из исходного потока.
        _Buffer.Load(_SizeOfInt16);

        //  Чтение значения из буфера.
        return _Buffer.Reader.ReadInt16();
    }

    /// <summary>
    /// Асинхронно считывает значение типа <see cref="short"/>
    /// из текущего потока и перемещает текущую позицию в потоке на два байта вперед.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public async Task<short> ReadInt16Async(CancellationToken cancellationToken)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Загрузка в буфер данных из исходного потока.
        await _Buffer.LoadAsync(_SizeOfInt16, cancellationToken).ConfigureAwait(false);

        //  Чтение значения из буфера.
        return _Buffer.Reader.ReadInt16();
    }

    /// <summary>
    /// Считывает значение типа <see cref="ushort"/>
    /// из текущего потока и перемещает текущую позицию в потоке на два байта вперед.
    /// </summary>
    /// <returns>
    /// Прочитанное значение.
    /// </returns>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    [CLSCompliant(false)]
    public ushort ReadUInt16()
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Загрузка в буфер данных из исходного потока.
        _Buffer.Load(_SizeOfUInt16);

        //  Чтение значения из буфера.
        return _Buffer.Reader.ReadUInt16();
    }

    /// <summary>
    /// Асинхронно считывает значение типа <see cref="ushort"/>
    /// из текущего потока и перемещает текущую позицию в потоке на два байта вперед.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    [CLSCompliant(false)]
    public async Task<ushort> ReadUInt16Async(CancellationToken cancellationToken)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Загрузка в буфер данных из исходного потока.
        await _Buffer.LoadAsync(_SizeOfUInt16, cancellationToken).ConfigureAwait(false);

        //  Чтение значения из буфера.
        return _Buffer.Reader.ReadUInt16();
    }

    /// <summary>
    /// Считывает значение типа <see cref="int"/>
    /// из текущего потока и перемещает текущую позицию в потоке на четыре байта вперед.
    /// </summary>
    /// <returns>
    /// Прочитанное значение.
    /// </returns>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public int ReadInt32()
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Загрузка в буфер данных из исходного потока.
        _Buffer.Load(_SizeOfInt32);

        //  Чтение значения из буфера.
        return _Buffer.Reader.ReadInt32();
    }

    /// <summary>
    /// Асинхронно считывает значение типа <see cref="int"/>
    /// из текущего потока и перемещает текущую позицию в потоке на четыре байта вперед.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public async Task<int> ReadInt32Async(CancellationToken cancellationToken)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Загрузка в буфер данных из исходного потока.
        await _Buffer.LoadAsync(_SizeOfInt32, cancellationToken).ConfigureAwait(false);

        //  Чтение значения из буфера.
        return _Buffer.Reader.ReadInt32();
    }

    /// <summary>
    /// Считывает значение типа <see cref="uint"/>
    /// из текущего потока и перемещает текущую позицию в потоке на четыре байта вперед.
    /// </summary>
    /// <returns>
    /// Прочитанное значение.
    /// </returns>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    [CLSCompliant(false)]
    public uint ReadUInt32()
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Загрузка в буфер данных из исходного потока.
        _Buffer.Load(_SizeOfUInt32);

        //  Чтение значения из буфера.
        return _Buffer.Reader.ReadUInt32();
    }

    /// <summary>
    /// Асинхронно считывает значение типа <see cref="uint"/>
    /// из текущего потока и перемещает текущую позицию в потоке на четыре байта вперед.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    [CLSCompliant(false)]
    public async Task<uint> ReadUInt32Async(CancellationToken cancellationToken)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Загрузка в буфер данных из исходного потока.
        await _Buffer.LoadAsync(_SizeOfUInt32, cancellationToken).ConfigureAwait(false);

        //  Чтение значения из буфера.
        return _Buffer.Reader.ReadUInt32();
    }

    /// <summary>
    /// Считывает значение типа <see cref="long"/>
    /// из текущего потока и перемещает текущую позицию в потоке на восемь байтов вперед.
    /// </summary>
    /// <returns>
    /// Прочитанное значение.
    /// </returns>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public long ReadInt64()
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Загрузка в буфер данных из исходного потока.
        _Buffer.Load(_SizeOfInt64);

        //  Чтение значения из буфера.
        return _Buffer.Reader.ReadInt64();
    }

    /// <summary>
    /// Асинхронно считывает значение типа <see cref="long"/>
    /// из текущего потока и перемещает текущую позицию в потоке на восемь байтов вперед.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public async Task<long> ReadInt64Async(CancellationToken cancellationToken)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Загрузка в буфер данных из исходного потока.
        await _Buffer.LoadAsync(_SizeOfInt64, cancellationToken).ConfigureAwait(false);

        //  Чтение значения из буфера.
        return _Buffer.Reader.ReadInt64();
    }

    /// <summary>
    /// Считывает значение типа <see cref="ulong"/>
    /// из текущего потока и перемещает текущую позицию в потоке на восемь байтов вперед.
    /// </summary>
    /// <returns>
    /// Прочитанное значение.
    /// </returns>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    [CLSCompliant(false)]
    public ulong ReadUInt64()
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Загрузка в буфер данных из исходного потока.
        _Buffer.Load(_SizeOfUInt64);

        //  Чтение значения из буфера.
        return _Buffer.Reader.ReadUInt64();
    }

    /// <summary>
    /// Асинхронно считывает значение типа <see cref="ulong"/>
    /// из текущего потока и перемещает текущую позицию в потоке на восемь байтов вперед.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    [CLSCompliant(false)]
    public async Task<ulong> ReadUInt64Async(CancellationToken cancellationToken)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Загрузка в буфер данных из исходного потока.
        await _Buffer.LoadAsync(_SizeOfUInt64, cancellationToken).ConfigureAwait(false);

        //  Чтение значения из буфера.
        return _Buffer.Reader.ReadUInt64();
    }

    /// <summary>
    /// Считывает значение типа <see cref="float"/>
    /// из текущего потока и перемещает текущую позицию в потоке на четыре байта вперед.
    /// </summary>
    /// <returns>
    /// Прочитанное значение.
    /// </returns>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public float ReadFloat32()
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Загрузка в буфер данных из исходного потока.
        _Buffer.Load(_SizeOfSingle);

        //  Чтение значения из буфера.
        return _Buffer.Reader.ReadSingle();
    }

    /// <summary>
    /// Асинхронно считывает значение типа <see cref="float"/>
    /// из текущего потока и перемещает текущую позицию в потоке на четыре байта вперед.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public async Task<float> ReadFloat32Async(CancellationToken cancellationToken)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Загрузка в буфер данных из исходного потока.
        await _Buffer.LoadAsync(_SizeOfSingle, cancellationToken).ConfigureAwait(false);

        //  Чтение значения из буфера.
        return _Buffer.Reader.ReadSingle();
    }

    /// <summary>
    /// Считывает значение типа <see cref="double"/>
    /// из текущего потока и перемещает текущую позицию в потоке на восемь байтов вперед.
    /// </summary>
    /// <returns>
    /// Прочитанное значение.
    /// </returns>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public double ReadFloat64()
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Загрузка в буфер данных из исходного потока.
        _Buffer.Load(_SizeOfDouble);

        //  Чтение значения из буфера.
        return _Buffer.Reader.ReadDouble();
    }

    /// <summary>
    /// Асинхронно считывает значение типа <see cref="double"/>
    /// из текущего потока и перемещает текущую позицию в потоке на восемь байтов вперед.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public async Task<double> ReadFloat64Async(CancellationToken cancellationToken)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Загрузка в буфер данных из исходного потока.
        await _Buffer.LoadAsync(_SizeOfDouble, cancellationToken).ConfigureAwait(false);

        //  Чтение значения из буфера.
        return _Buffer.Reader.ReadDouble();
    }

    /// <summary>
    /// Считывает значение типа <see cref="decimal"/>
    /// из текущего потока и перемещает текущую позицию в потоке на шестнадцать байтов вперед.
    /// </summary>
    /// <returns>
    /// Прочитанное значение.
    /// </returns>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public decimal ReadDecimal()
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Загрузка в буфер данных из исходного потока.
        _Buffer.Load(_SizeOfDecimal);

        //  Чтение значения из буфера.
        return _Buffer.Reader.ReadDecimal();
    }

    /// <summary>
    /// Асинхронно считывает значение типа <see cref="decimal"/>
    /// из текущего потока и перемещает текущую позицию в потоке на шестнадцать байтов вперед.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public async Task<decimal> ReadDecimalAsync(CancellationToken cancellationToken)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Загрузка в буфер данных из исходного потока.
        await _Buffer.LoadAsync(_SizeOfDecimal, cancellationToken).ConfigureAwait(false);

        //  Чтение значения из буфера.
        return _Buffer.Reader.ReadDecimal();
    }

    /// <summary>
    /// Считывает значение типа <see cref="DateTime"/>
    /// из текущего потока и перемещает текущую позицию в потоке на восемь байтов вперед.
    /// </summary>
    /// <returns>
    /// Прочитанное значение.
    /// </returns>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public DateTime ReadDateTime()
    {
        //  Чтение интервалов.
        long ticks = ReadInt64();

        //  Возврат прочитанного значение.
        return new(ticks);
    }

    /// <summary>
    /// Асинхронно считывает значение типа <see cref="DateTime"/>
    /// из текущего потока и перемещает текущую позицию в потоке на восемь байтов вперед.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    public async Task<DateTime> ReadDateTimeAsync(CancellationToken cancellationToken)
    {
        //  Чтение интервалов.
        long ticks = await ReadInt64Async(cancellationToken).ConfigureAwait(false);

        //  Возврат прочитанного значение.
        return new(ticks);
    }

    /// <summary>
    /// Выполняет чтение знака из базового потока
    /// и перемещает текущую позицию в потоке вперед в соответствии с используемым значением кодировки
    /// и конкретным знаком в потоке, чтение которого выполняется в настоящий момент.
    /// </summary>
    /// <returns>
    /// Прочитанное значение.
    /// </returns>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    /// <exception cref="IOException">
    /// Неверный формат потока.
    /// </exception>
    public char ReadChar()
    {
        //  Создание буфера.
        byte[] buffer = Array.Empty<byte>();

        //  Создание массива символов.
        char[] chars = new char[1];

        //  Блок перехвата исключений.
        try
        {
            //  Цикл чтения.
            while (Encoding.GetChars(buffer, 0, buffer.Length, chars, 0) == 0)
            {
                //  Изменение размера буфера.
                Array.Resize(ref buffer, buffer.Length + 1);

                //  Чтение новых байтов.
                ReadBytes(buffer, buffer.Length - 1, 1);
            }
        }
        catch (DecoderFallbackException)
        {
            //  Неверный формат потока.
            throw Exceptions.StreamInvalidFormat();
        }

        //  Возврат прочитанного символа.
        return chars[0];
    }

    /// <summary>
    /// Асинхронно выполняет чтение знака из базового потока
    /// и перемещает текущую позицию в потоке вперед в соответствии с используемым значением кодировки
    /// и конкретным знаком в потоке, чтение которого выполняется в настоящий момент.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    /// <exception cref="IOException">
    /// Неверный формат потока.
    /// </exception>
    public async Task<char> ReadCharAsync(CancellationToken cancellationToken)
    {
        //  Создание буфера.
        byte[] buffer = Array.Empty<byte>();

        //  Создание массива символов.
        char[] chars = new char[1];

        //  Блок перехвата исключений.
        try
        {
            //  Цикл чтения.
            while (Encoding.GetChars(buffer, 0, buffer.Length, chars, 0) == 0)
            {
                //  Изменение размера буфера.
                Array.Resize(ref buffer, buffer.Length + 1);

                //  Чтение новых байтов.
                await ReadBytesAsync(buffer, buffer.Length - 1, 1, cancellationToken).ConfigureAwait(false);
            }
        }
        catch (DecoderFallbackException)
        {
            //  Неверный формат потока.
            throw Exceptions.StreamInvalidFormat();
        }

        //  Возврат прочитанного символа.
        return chars[0];
    }

    /// <summary>
    /// Считывает указанное количество символов из потока,
    /// начиная с заданной точки в массиве символов.
    /// </summary>
    /// <param name="buffer">
    /// Буфер, в который необходимо считать данные.
    /// </param>
    /// <param name="index">
    /// Стартовая точка в буфере, начиная с которой считываемые данные записываются в буфер.
    /// </param>
    /// <param name="count">
    /// Количество символов, которые необходимо считать.
    /// </param>
    /// <returns>
    /// Прочитанные данные.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// В параметре <paramref name="buffer"/> передан массив,
    /// который не вмещает <paramref name="count"/> элементов начиная с позиции <paramref name="index"/>.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    /// <exception cref="IOException">
    /// Неверный формат потока.
    /// </exception>
    public void ReadChars(char[] buffer, int index, int count)
    {
        //  Проверка диапазона массива.
        IsRange(buffer, index, count, nameof(buffer), nameof(index), nameof(count));

        //  Цикл по символам.
        for (int i = 0; i != count; ++i)
        {
            //  Чтение символа.
            buffer[index + i] = ReadChar();
        }
    }

    /// <summary>
    /// Асинхронно считывает указанное количество символов из потока,
    /// начиная с заданной точки в массиве символов.
    /// </summary>
    /// <param name="buffer">
    /// Буфер, в который необходимо считать данные.
    /// </param>
    /// <param name="index">
    /// Стартовая точка в буфере, начиная с которой считываемые данные записываются в буфер.
    /// </param>
    /// <param name="count">
    /// Количество символов, которые необходимо считать.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// В параметре <paramref name="buffer"/> передан массив,
    /// который не вмещает <paramref name="count"/> элементов начиная с позиции <paramref name="index"/>.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    /// <exception cref="IOException">
    /// Неверный формат потока.
    /// </exception>
    public async Task ReadCharsAsync(char[] buffer, int index, int count, CancellationToken cancellationToken)
    {
        //  Проверка диапазона массива.
        IsRange(buffer, index, count, nameof(buffer), nameof(index), nameof(count));

        //  Цикл по символам.
        for (int i = 0; i != count; ++i)
        {
            //  Чтение символа.
            buffer[index + i] = await ReadCharAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Считывает указанное количество символов из текущего потока,
    /// возвращает данные в массив символов и перемещает текущую позицию
    /// в соответствии с используемой кодировкой и определенным символом, считываемым из потока.
    /// </summary>
    /// <param name="count">
    /// Количество символов, которые необходимо прочитать.
    /// </param>
    /// <returns>
    /// Прочитанные данные.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    /// <exception cref="IOException">
    /// Неверный формат потока.
    /// </exception>
    public char[] ReadChars(int count)
    {
        //  Проверка количества символов.
        IsNotNegative(count, nameof(count));

        //  Создание массива символов.
        var buffer = new char[count];

        //  Чтение массива символов.
        ReadChars(buffer, 0, count);

        //  Возврат массива символов.
        return buffer;
    }

    /// <summary>
    /// Асинхронно считывает указанное количество символов из текущего потока,
    /// возвращает данные в массив символов и перемещает текущую позицию
    /// в соответствии с используемой кодировкой и определенным символом, считываемым из потока.
    /// </summary>
    /// <param name="count">
    /// Количество символов, которые необходимо прочитать.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    /// <exception cref="IOException">
    /// Неверный формат потока.
    /// </exception>
    public async Task<char[]> ReadCharsAsync(int count, CancellationToken cancellationToken)
    {
        //  Проверка количества символов.
        IsNotNegative(count, nameof(count));

        //  Создание массива символов.
        var buffer = new char[count];

        //  Чтение массива символов.
        await ReadCharsAsync(buffer, 0, count, cancellationToken).ConfigureAwait(false);

        //  Возврат массива символов.
        return buffer;
    }

    /// <summary>
    /// Cчитывает значение типа <see cref="int"/> в 7-битной кодировке.
    /// </summary>
    /// <returns>
    /// Прочитанное значение.
    /// </returns>
    /// <exception cref="IOException">
    /// Неверный формат потока.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    private int Read7BitEncodedInt()
    {
        //  Результат чтения.
        int result = 0;

        //  Сдвиг.
        int shift = 0;

        //  Текущий байт.
        byte currentByte;

        do
        {
            //  Проверка на переполнение.
            if (shift == 5 * 7)
            {
                //  Неверный формат.
                throw Exceptions.StreamInvalidFormat();
            }

            //  Чтение текущего байта.
            currentByte = ReadUInt8();

            //  Модификация результата.
            result |= (currentByte & 0x7F) << shift;

            //  Модификация сдвига.
            shift += 7;
        } while ((currentByte & 0x80) != 0);

        //  Возврат результата.
        return result;
    }

    /// <summary>
    /// Асинхронно считывает значение типа <see cref="int"/> в 7-битной кодировке.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="IOException">
    /// Неверный формат потока.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    private async Task<int> Read7BitEncodedIntAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Результат чтения.
        int result = 0;

        //  Сдвиг.
        int shift = 0;

        //  Текущий байт.
        byte currentByte;

        do
        {
            //  Проверка на переполнение.
            if (shift == 5 * 7)
            {
                //  Неверный формат.
                throw Exceptions.StreamInvalidFormat();
            }

            //  Чтение текущего байта.
            currentByte = await ReadUInt8Async(cancellationToken).ConfigureAwait(false);

            //  Модификация результата.
            result |= (currentByte & 0x7F) << shift;

            //  Модификация сдвига.
            shift += 7;
        } while ((currentByte & 0x80) != 0);

        //  Возврат результата.
        return result;
    }

    /// <summary>
    /// Считывает строку из текущего потока.
    /// </summary>
    /// <returns>
    /// Прочитанное значение.
    /// </returns>
    /// <exception cref="IOException">
    /// Неверный формат потока.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// Произошла попытка обратиться к разрушенному объекту.
    /// </exception>
    public string ReadString()
    {
        //  Чтение размера данных строки.
        int size = Read7BitEncodedInt();

        //  Проверка длины строки.
        if (size < 0)
        {
            //  Неверный формат.
            throw Exceptions.StreamInvalidFormat();
        }

        //  Чтение данных строки.
        byte[] buffer = ReadBytes(size);

        //  Возврат прочитанной строки.
        return Encoding.GetString(buffer);
    }

    /// <summary>
    /// Асинхронно считывает строку из текущего потока.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="IOException">
    /// Неверный формат потока.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// Произошла попытка обратиться к разрушенному объекту.
    /// </exception>
    public async Task<string> ReadStringAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Чтение размера данных строки.
        int size = await Read7BitEncodedIntAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка длины строки.
        if (size < 0)
        {
            //  Неверный формат.
            throw Exceptions.StreamInvalidFormat();
        }

        //  Чтение данных строки.
        byte[] buffer = await ReadBytesAsync(size, cancellationToken).ConfigureAwait(false);

        //  Возврат прочитанной строки.
        return Encoding.GetString(buffer);
    }

    /// <summary>
    /// Выполняет запись части массива байтов в текущий поток.
    /// </summary>
    /// <param name="buffer">
    /// Массив байтов, содержащий записываемые в поток данные.
    /// </param>
    /// <param name="index">
    /// Индекс первого байта для чтения из <paramref name="buffer"/> и для записи в поток.
    /// </param>
    /// <param name="count">
    /// Количество байтов для чтения из <paramref name="buffer"/> и для записи в поток.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// В параметре <paramref name="buffer"/> передан массив,
    /// который не вмещает <paramref name="count"/> элементов начиная с позиции <paramref name="index"/>.
    /// </exception>
    public void WriteBytes(byte[] buffer, int index, int count)
    {
        //  Проверка буфера.
        IsRange(buffer, index, count, nameof(buffer), nameof(index), nameof(count));

        //  Запись в поток.
        _Stream.Write(buffer, index, count);

        //  Корректировка положения для записи.
        WritePosition += count;
    }

    /// <summary>
    /// Асинхронно выполняет запись части массива байтов в текущий поток.
    /// </summary>
    /// <param name="buffer">
    /// Массив байтов, содержащий записываемые в поток данные.
    /// </param>
    /// <param name="index">
    /// Индекс первого байта для чтения из <paramref name="buffer"/> и для записи в поток.
    /// </param>
    /// <param name="count">
    /// Количество байтов для чтения из <paramref name="buffer"/> и для записи в поток.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// В параметре <paramref name="buffer"/> передан массив,
    /// который не вмещает <paramref name="count"/> элементов начиная с позиции <paramref name="index"/>.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task WriteBytesAsync(byte[] buffer, int index, int count, CancellationToken cancellationToken)
    {
        //  Проверка буфера.
        IsRange(buffer, index, count, nameof(buffer), nameof(index), nameof(count));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Запись в поток.
        await _Stream.WriteAsync(buffer.AsMemory(index, count), cancellationToken).ConfigureAwait(false);

        //  Корректировка позиции на запись.
        WritePosition += count;
    }

    /// <summary>
    /// Записывает массив байтов в базовый поток.
    /// </summary>
    /// <param name="buffer">
    /// Массив байтов, содержащий записываемые в поток данные.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    public void WriteBytes(byte[] buffer)
    {
        //  Проверка ссылки на буфер.
        IsNotNull(buffer, nameof(buffer));

        //  Запись в поток.
        WriteBytes(buffer, 0, buffer.Length);
    }

    /// <summary>
    /// Асинхронно записывает массив байтов в базовый поток.
    /// </summary>
    /// <param name="buffer">
    /// Массив байтов, содержащий записываемые в поток данные.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение данных.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task WriteBytesAsync(byte[] buffer, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на буфер.
        IsNotNull(buffer, nameof(buffer));

        //  Асинхронная запись в поток.
        await WriteBytesAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Записывает значение типа <see cref="bool"/>
    /// длиной один байт в текущий поток,
    /// при этом 0 соответствует значению <c>false</c>, а 1 - значению <c>true</c>.
    /// </summary>
    /// <param name="value">
    /// Записываемое в поток значение.
    /// </param>
    public void WriteBoolean(bool value)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Запись значения в буфер.
        _Buffer.Writer.Write(value);

        //  Сохранение данных в потоке.
        _Buffer.Save(_SizeOfBoolean);
    }

    /// <summary>
    /// Асинхронно записывает значение типа <see cref="bool"/>
    /// длиной один байт в текущий поток,
    /// при этом 0 соответствует значению <c>false</c>, а 1 - значению <c>true</c>.
    /// </summary>
    /// <param name="value">
    /// Записываемое в поток значение.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task WriteBooleanAsync(bool value, CancellationToken cancellationToken)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Запись значения в буфер.
        _Buffer.Writer.Write(value);

        //  Сохранение данных в потоке.
        await _Buffer.SaveAsync(_SizeOfBoolean, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Записывает значение типа <see cref="sbyte"/>
    /// длиной один байт в текущий поток.
    /// </summary>
    /// <param name="value">
    /// Записываемое в поток значение.
    /// </param>
    [CLSCompliant(false)]
    public void WriteInt8(sbyte value)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Запись значения в буфер.
        _Buffer.Writer.Write(value);

        //  Сохранение данных в потоке.
        _Buffer.Save(_SizeOfSByte);
    }

    /// <summary>
    /// Асинхронно записывает значение типа <see cref="sbyte"/>
    /// длиной один байт в текущий поток.
    /// </summary>
    /// <param name="value">
    /// Записываемое в поток значение.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    [CLSCompliant(false)]
    public async Task WriteInt8Async(sbyte value, CancellationToken cancellationToken)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Запись значения в буфер.
        _Buffer.Writer.Write(value);

        //  Сохранение данных в потоке.
        await _Buffer.SaveAsync(_SizeOfSByte, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Записывает значение типа <see cref="byte"/>
    /// длиной один байт в текущий поток.
    /// </summary>
    /// <param name="value">
    /// Записываемое в поток значение.
    /// </param>
    public void WriteUInt8(byte value)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Запись значения в буфер.
        _Buffer.Writer.Write(value);

        //  Сохранение данных в потоке.
        _Buffer.Save(_SizeOfByte);
    }

    /// <summary>
    /// Асинхронно записывает значение типа <see cref="byte"/>
    /// длиной один байт в текущий поток.
    /// </summary>
    /// <param name="value">
    /// Записываемое в поток значение.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task WriteUInt8Async(byte value, CancellationToken cancellationToken)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Запись значения в буфер.
        _Buffer.Writer.Write(value);

        //  Сохранение данных в потоке.
        await _Buffer.SaveAsync(_SizeOfByte, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Записывает целое число со знаком размером 2 байта в текущий поток
    /// и перемещает позицию в потоке вперед на два байта.
    /// </summary>
    /// <param name="value">
    /// Записываемое значение.
    /// </param>
    public void WriteInt16(short value)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Запись значения в буфер.
        _Buffer.Writer.Write(value);

        //  Сохранение данных в потоке.
        _Buffer.Save(_SizeOfInt16);
    }

    /// <summary>
    /// Асинхронно записывает целое число со знаком размером 2 байта в текущий поток
    /// и перемещает позицию в потоке вперед на два байта.
    /// </summary>
    /// <param name="value">
    /// Записываемое значение.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task WriteInt16Async(short value, CancellationToken cancellationToken)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Запись значения в буфер.
        _Buffer.Writer.Write(value);

        //  Сохранение данных в потоке.
        await _Buffer.SaveAsync(_SizeOfInt16, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Записывает целое число без знака размером 2 байта в текущий поток
    /// и перемещает позицию в потоке вперед на два байта.
    /// </summary>
    /// <param name="value">
    /// Записываемое значение.
    /// </param>
    [CLSCompliant(false)]
    public void WriteUInt16(ushort value)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Запись значения в буфер.
        _Buffer.Writer.Write(value);

        //  Сохранение данных в потоке.
        _Buffer.Save(_SizeOfUInt16);
    }

    /// <summary>
    /// Асинхронно записывает целое число без знака размером 2 байта в текущий поток
    /// и перемещает позицию в потоке вперед на два байта.
    /// </summary>
    /// <param name="value">
    /// Записываемое значение.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    [CLSCompliant(false)]
    public async Task WriteUInt16Async(ushort value, CancellationToken cancellationToken)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Запись значения в буфер.
        _Buffer.Writer.Write(value);

        //  Сохранение данных в потоке.
        await _Buffer.SaveAsync(_SizeOfUInt16, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Записывает четырехбайтовое целое число со знаком в текущий поток
    /// и перемещает позицию в потоке на четыре байта вперед.
    /// </summary>
    /// <param name="value">
    /// Записываемое значение.
    /// </param>
    public void WriteInt32(int value)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Запись значения в буфер.
        _Buffer.Writer.Write(value);

        //  Сохранение данных в потоке.
        _Buffer.Save(_SizeOfInt32);
    }

    /// <summary>
    /// Асинхронно записывает четырехбайтовое целое число со знаком в текущий поток
    /// и перемещает позицию в потоке на четыре байта вперед.
    /// </summary>
    /// <param name="value">
    /// Записываемое значение.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task WriteInt32Async(int value, CancellationToken cancellationToken)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Запись значения в буфер.
        _Buffer.Writer.Write(value);

        //  Сохранение данных в потоке.
        await _Buffer.SaveAsync(_SizeOfInt32, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Записывает целое число без знака размером 4 байта в текущий поток
    /// и перемещает позицию в потоке вперед на четыре байта.
    /// </summary>
    /// <param name="value">
    /// Записываемое значение.
    /// </param>
    [CLSCompliant(false)]
    public void WriteUInt32(uint value)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Запись значения в буфер.
        _Buffer.Writer.Write(value);

        //  Сохранение данных в потоке.
        _Buffer.Save(_SizeOfUInt32);
    }

    /// <summary>
    /// Асинхронно записывает целое число без знака размером 4 байта в текущий поток
    /// и перемещает позицию в потоке вперед на четыре байта.
    /// </summary>
    /// <param name="value">
    /// Записываемое значение.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    [CLSCompliant(false)]
    public async Task WriteUInt32Async(uint value, CancellationToken cancellationToken)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Запись значения в буфер.
        _Buffer.Writer.Write(value);

        //  Сохранение данных в потоке.
        await _Buffer.SaveAsync(_SizeOfUInt32, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Записывает целое число со знаком размером 8 байт в текущий поток
    /// и перемещает позицию в потоке вперед на восемь байт.
    /// </summary>
    /// <param name="value">
    /// Записываемое значение.
    /// </param>
    public void WriteInt64(long value)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Запись значения в буфер.
        _Buffer.Writer.Write(value);

        //  Сохранение данных в потоке.
        _Buffer.Save(_SizeOfInt64);
    }

    /// <summary>
    /// Асинхронно записывает целое число со знаком размером 8 байт в текущий поток
    /// и перемещает позицию в потоке вперед на восемь байт.
    /// </summary>
    /// <param name="value">
    /// Записываемое значение.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task WriteInt64Async(long value, CancellationToken cancellationToken)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Запись значения в буфер.
        _Buffer.Writer.Write(value);

        //  Сохранение данных в потоке.
        await _Buffer.SaveAsync(_SizeOfInt64, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Записывает целое число без знака размером 8 байт в текущий поток
    /// и перемещает позицию в потоке вперед на восемь байт.
    /// </summary>
    /// <param name="value">
    /// Записываемое значение.
    /// </param>
    [CLSCompliant(false)]
    public void WriteUInt64(ulong value)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Запись значения в буфер.
        _Buffer.Writer.Write(value);

        //  Сохранение данных в потоке.
        _Buffer.Save(_SizeOfUInt64);
    }

    /// <summary>
    /// Асинхронно записывает целое число без знака размером 8 байт в текущий поток
    /// и перемещает позицию в потоке вперед на восемь байт.
    /// </summary>
    /// <param name="value">
    /// Записываемое значение.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    [CLSCompliant(false)]
    public async Task WriteUInt64Async(ulong value, CancellationToken cancellationToken)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Запись значения в буфер.
        _Buffer.Writer.Write(value);

        //  Сохранение данных в потоке.
        await _Buffer.SaveAsync(_SizeOfUInt64, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Записывает число с плавающей запятой длиной 4 байта в текущий поток
    /// и перемещает позицию в потоке вперед на четыре байта.
    /// </summary>
    /// <param name="value">
    /// Записываемое значение.
    /// </param>
    public void WriteFloat32(float value)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Запись значения в буфер.
        _Buffer.Writer.Write(value);

        //  Сохранение данных в потоке.
        _Buffer.Save(_SizeOfSingle);
    }

    /// <summary>
    /// Асинхронно записывает число с плавающей запятой длиной 4 байта в текущий поток
    /// и перемещает позицию в потоке вперед на четыре байта.
    /// </summary>
    /// <param name="value">
    /// Записываемое значение.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task WriteFloat32Async(float value, CancellationToken cancellationToken)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Запись значения в буфер.
        _Buffer.Writer.Write(value);

        //  Сохранение данных в потоке.
        await _Buffer.SaveAsync(_SizeOfSingle, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Записывает число с плавающей запятой размером 8 байт в текущий поток
    /// и перемещает позицию в потоке вперед на восемь байт.
    /// </summary>
    /// <param name="value">
    /// Записываемое значение.
    /// </param>
    public void WriteFloat64(double value)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Запись значения в буфер.
        _Buffer.Writer.Write(value);

        //  Сохранение данных в потоке.
        _Buffer.Save(_SizeOfDouble);
    }

    /// <summary>
    /// Асинхронно записывает число с плавающей запятой размером 8 байт в текущий поток
    /// и перемещает позицию в потоке вперед на восемь байт.
    /// </summary>
    /// <param name="value">
    /// Записываемое значение.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task WriteFloat64Async(double value, CancellationToken cancellationToken)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Запись значения в буфер.
        _Buffer.Writer.Write(value);

        //  Сохранение данных в потоке.
        await _Buffer.SaveAsync(_SizeOfDouble, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Записывает десятичное число в текущий поток
    /// и перемещает позицию в потоке на шестнадцать байтов.
    /// </summary>
    /// <param name="value">
    /// Записываемое значение.
    /// </param>
    public void WriteDecimal(decimal value)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Запись значения в буфер.
        _Buffer.Writer.Write(value);

        //  Сохранение данных в потоке.
        _Buffer.Save(_SizeOfDecimal);
    }

    /// <summary>
    /// Асинхронно записывает десятичное число в текущий поток
    /// и перемещает позицию в потоке на шестнадцать байтов.
    /// </summary>
    /// <param name="value">
    /// Записываемое значение.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task WriteDecimalAsync(decimal value, CancellationToken cancellationToken)
    {
        //  Сброс буфера.
        _Buffer.Reset();

        //  Запись значения в буфер.
        _Buffer.Writer.Write(value);

        //  Сохранение данных в потоке.
        await _Buffer.SaveAsync(_SizeOfDecimal, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет запись времени из потока.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо записать в поток.
    /// </param>
    public void WriteDateTime(DateTime value)
    {
        //  Запись интервалов.
        WriteInt64(value.Ticks);
    }

    /// <summary>
    /// Асинхронно выполняет запись времени из потока.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо записать в поток.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task WriteDateTimeAsync(DateTime value, CancellationToken cancellationToken)
    {
        //  Запись интервалов.
        await WriteInt64Async(value.Ticks, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет запись символа в текущий поток
    /// и перемещает текущую позицию в потоке вперед в соответствии с используемой
    /// кодировкой и количеством записанных в поток символов.
    /// </summary>
    /// <param name="value">
    /// Символ, который требуется записать.
    /// </param>
    public void WriteChar(char value)
    {
        //  Асинхронная запись в поток.
        WriteChars(new char[] { value });
    }

    /// <summary>
    /// Асинхронно выполняет запись символа в текущий поток
    /// и перемещает текущую позицию в потоке вперед в соответствии с используемой
    /// кодировкой и количеством записанных в поток символов.
    /// </summary>
    /// <param name="value">
    /// Символ, который требуется записать.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task WriteCharAsync(char value, CancellationToken cancellationToken)
    {
        //  Асинхронная запись в поток.
        await WriteCharsAsync(new char[] { value }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет запись массива символов в текущий поток
    /// и перемещает текущую позицию в потоке в соответствии с используемой кодировкой
    /// и количеством записанных в поток символов.
    /// </summary>
    /// <param name="buffer">
    /// Массив символов, содержащий записываемые в поток данные.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    public void WriteChars(char[] buffer)
    {
        //  Проверка ссылки на массив символов.
        IsNotNull(buffer, nameof(buffer));

        //  Создание буфера для записи.
        byte[] bytes = Encoding.GetBytes(buffer, 0, buffer.Length);

        //  Запись в поток.
        WriteBytes(bytes);
    }

    /// <summary>
    /// Асинхронно выполняет запись массива символов в текущий поток
    /// и перемещает текущую позицию в потоке в соответствии с используемой кодировкой
    /// и количеством записанных в поток символов.
    /// </summary>
    /// <param name="buffer">
    /// Массив символов, содержащий записываемые в поток данные.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись данных.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task WriteCharsAsync(char[] buffer, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на массив символов.
        IsNotNull(buffer, nameof(buffer));

        //  Создание буфера для записи.
        byte[] bytes = Encoding.GetBytes(buffer, 0, buffer.Length);

        //  Асинхронная запись в поток.
        await WriteBytesAsync(bytes, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Записывает значение типа <see cref="int"/> в 7-битной кодировке.
    /// </summary>
    /// <param name="value">
    /// Записываемое значение.
    /// </param>
    private void Write7BitEncodedInt([ParameterNoChecks] int value)
    {
        //  Приведение числа к беззнаковому представлению.
        uint unsigned = (uint)value;

        //  Основной цикл записи.
        while (unsigned >= 0x80)
        {
            //  Запись очередного байта.
            WriteUInt8((byte)(unsigned | 0x80));

            //  Сдвиг значения.
            unsigned >>= 7;
        }

        //  Запись последнего байта.
        WriteUInt8((byte)unsigned);
    }

    /// <summary>
    /// Асинхронно записывает значение типа <see cref="int"/> в 7-битной кодировке.
    /// </summary>
    /// <param name="value">
    /// Записываемое значение.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись.
    /// </returns>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async Task Write7BitEncodedIntAsync([ParameterNoChecks] int value, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Приведение числа к беззнаковому представлению.
        uint unsigned = (uint)value;

        //  Основной цикл записи.
        while (unsigned >= 0x80)
        {
            //  Запись очередного байта.
            await WriteUInt8Async((byte)(unsigned | 0x80), cancellationToken).ConfigureAwait(false);

            //  Сдвиг значения.
            unsigned >>= 7;
        }

        //  Запись последнего байта.
        await WriteUInt8Async((byte)unsigned, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Записывает строку в поток.
    /// </summary>
    /// <param name="value">
    /// Записываемое значение.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="value"/> передана пустая ссылка.
    /// </exception>
    public void WriteString(string value)
    {
        //  Проверка значения.
        IsNotNull(value, nameof(value));

        //  Определение размера данных строки.
        int size = Encoding.GetByteCount(value);

        //  Получение данных для записи.
        byte[] buffer = Encoding.GetBytes(value.ToCharArray());

        //  Запись размера данных строки.
        Write7BitEncodedInt(size);

        //  Запись данных строки.
        WriteBytes(buffer);
    }

    /// <summary>
    /// Записывает строку в поток.
    /// </summary>
    /// <param name="value">
    /// Записываемое значение.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="value"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task WriteStringAsync(string value, CancellationToken cancellationToken)
    {
        //  Проверка значения.
        IsNotNull(value, nameof(value));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Определение размера данных строки.
        int size = Encoding.GetByteCount(value);

        //  Получение данных для записи.
        byte[] buffer = Encoding.GetBytes(value.ToCharArray());

        //  Запись размера данных строки.
        await Write7BitEncodedIntAsync(size, cancellationToken).ConfigureAwait(false);

        //  Запись данных строки.
        await WriteBytesAsync(buffer, cancellationToken).ConfigureAwait(false);
    }
}
