using System.Security.Cryptography;

namespace Simargl.Cryptography;

/// <summary>
/// Реализует 32-битный хеш-алгоритм CRC.
/// </summary>
public class Crc32 :
    HashAlgorithm
{
    /// <summary>
    /// Постоянная, определяющая полином по умолчанию.
    /// </summary>
    public const long DefaultPolynomial = 0xedb88320u;

    /// <summary>
    /// Постоянная, определяющая начальное число по умолчанию.
    /// </summary>
    public const long DefaultSeed = 0xffffffffu;

    /// <summary>
    /// Поле для хранения таблицы по умолчанию.
    /// </summary>
    static uint[]? _DefaultTable;

    /// <summary>
    /// Поле для хранения начального числа.
    /// </summary>
    readonly uint _Seed;

    /// <summary>
    /// Поле для хранения таблицы.
    /// </summary>
    readonly uint[] _Table;

    /// <summary>
    /// Поле для хранения хеш-значения.
    /// </summary>
    uint _Hash;

    /// <summary>
    /// Инициализация нового экземпляра класса.
    /// </summary>
    public Crc32() :
        this(DefaultPolynomial, DefaultSeed)
    {

    }

    /// <summary>
    /// Инициализация нового экземпляра класса.
    /// </summary>
    /// <param name="polynomial">
    /// Полином.
    /// </param>
    /// <param name="seed">
    /// Начальное значение.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="polynomial"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="polynomial"/> передано значение большее <see cref="uint.MaxValue"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="seed"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="seed"/> передано значение большее <see cref="uint.MaxValue"/>.
    /// </exception>
    /// <exception cref="PlatformNotSupportedException">
    /// Не поддерживается на текущей платформе.
    /// </exception>
    public Crc32(long polynomial, long seed)
    {
        //  Проверка полинома.
        CheckPolynomial(polynomial);

        //  Проверка начального значения.
        CheckSeed(seed);

        //  Проверка платформы.
        if (!BitConverter.IsLittleEndian)
        {
            throw new PlatformNotSupportedException("Операция не поддерживается.");
        }

        //  Инициализация таблицы.
        _Table = InitializeTable((uint)polynomial);

        //  Инициализация параметров.
        _Seed = (uint)seed;
        _Hash = (uint)seed;
    }

    /// <summary>
    /// Сбрасывает хэш-алгоритм в исходное состояние.
    /// </summary>
    public override void Initialize()
    {
        _Hash = _Seed;
    }

    /// <summary>
    /// Передает данные, записанные в объект, на вход хэш-алгоритма для вычисления хэша.
    /// </summary>
    /// <param name="array">
    /// Входные данные, для которых вычисляется хэш-код.
    /// </param>
    /// <param name="ibStart">
    /// Смещение в массиве байтов, начиная с которого следует использовать данные.
    /// </param>
    /// <param name="cbSize">
    /// Число байтов в массиве для использования в качестве данных.
    /// </param>
    protected override void HashCore(byte[] array, int ibStart, int cbSize)
    {
        //  Вычисление хеш-значения.
        _Hash = CalculateHash(_Table, _Hash, array, ibStart, cbSize);
    }

    /// <summary>
    /// Завершает вычисление хэша после обработки последних данных криптографическим хэш-алгоритмом.
    /// </summary>
    /// <returns>
    /// Вычисляемый хэш-код.
    /// </returns>
    protected override byte[] HashFinal()
    {
        //  Вычисление хэш-кода.
        HashValue = ToBytes(~_Hash);

        //  Возврат хэш-кода.
        return HashValue;
    }

    /// <summary>
    /// Возвращает размер вычисляемого хэш-кода в битах.
    /// </summary>
    public override int HashSize => 32;

    /// <summary>
    /// Вычисляет хэш-код.
    /// </summary>
    /// <param name="buffer">
    /// Буфер.
    /// </param>
    /// <returns>
    /// Хэш-код.
    /// </returns>
    [CLSCompliant(false)]
    public static uint Compute(byte[] buffer)
    {
        return Compute((uint)DefaultSeed, buffer);
    }

    /// <summary>
    /// Вычисляет хэш-код.
    /// </summary>
    /// <param name="seed">
    /// Начальное значение.
    /// </param>
    /// <param name="buffer">
    /// Буфер.
    /// </param>
    /// <returns>
    /// Хэш-код.
    /// </returns>
    [CLSCompliant(false)]
    public static uint Compute(uint seed, byte[] buffer)
    {
        return Compute((uint)DefaultPolynomial, seed, buffer);
    }

    /// <summary>
    /// Вычисляет хэш-код.
    /// </summary>
    /// <param name="polynomial">
    /// Полином.
    /// </param>
    /// <param name="seed">
    /// Начальное значение.
    /// </param>
    /// <param name="buffer">
    /// Буфер.
    /// </param>
    /// <returns>
    /// Хэш-код.
    /// </returns>
    [CLSCompliant(false)]
    public static uint Compute(uint polynomial, uint seed, byte[] buffer)
    {
        return ~CalculateHash(InitializeTable(polynomial), seed, buffer, 0, buffer.Length);
    }

    /// <summary>
    /// Инициализирует таблицу.
    /// </summary>
    /// <param name="polynomial">
    /// Полином.
    /// </param>
    /// <returns>
    /// Таблица.
    /// </returns>
    static uint[] InitializeTable(uint polynomial)
    {
        if (polynomial == DefaultPolynomial && _DefaultTable is not null)
        {
            return _DefaultTable;
        }

        var createTable = new uint[256];
        for (var i = 0; i != 256; ++i)
        {
            uint entry = (uint)i;
            for (var j = 0; j != 8; ++j)
            {
                if ((entry & 1) == 1)
                {
                    entry = (entry >> 1) ^ polynomial;
                }
                else
                {
                    entry >>= 1;
                }
            }
            createTable[i] = entry;
        }

        if (polynomial == DefaultPolynomial)
        {
            _DefaultTable = createTable;
        }

        return createTable;
    }

    /// <summary>
    /// Вычисляет хэш-код.
    /// </summary>
    /// <param name="table">
    /// Таблица.
    /// </param>
    /// <param name="seed">
    /// Начальное значение.
    /// </param>
    /// <param name="buffer">
    /// Буфер.
    /// </param>
    /// <param name="start">
    /// Начальная позиция в буфере.
    /// </param>
    /// <param name="size">
    /// Размер области буфера.
    /// </param>
    /// <returns>
    /// Хэш-код.
    /// </returns>
    static uint CalculateHash(uint[] table, uint seed, IList<byte> buffer, int start, int size)
    {
        var hash = seed;
        for (var i = start; i < start + size; i++)
        {
            hash = (hash >> 8) ^ table[buffer[i] ^ hash & 0xff];
        }
        return hash;
    }

    /// <summary>
    /// Преобразует значение в массив байт.
    /// </summary>
    /// <param name="uint32">
    /// Значение.
    /// </param>
    /// <returns>
    /// Массив байт.
    /// </returns>
    static byte[] ToBytes(uint uint32)
    {
        //  Получение массива байт.
        var result = BitConverter.GetBytes(uint32);

        //  Реверсирование последовательности байт.
        Array.Reverse(result);

        //  Возврат массива.
        return result;
    }

    /// <summary>
    /// Выполняет проверку полинома.
    /// </summary>
    /// <param name="polynomial">
    /// Полином.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="polynomial"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="polynomial"/> передано значение большее <see cref="uint.MaxValue"/>.
    /// </exception>
    static void CheckPolynomial(long polynomial)
    {
        //  Проверка на отрицательное значение.
        IsNotNegative(polynomial, nameof(polynomial));

        //  Проверка на переполнение.
        IsNotLess(polynomial, uint.MaxValue, nameof(polynomial));
    }

    /// <summary>
    /// Выполняет проверку начального значения.
    /// </summary>
    /// <param name="seed">
    /// Полином.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="seed"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="seed"/> передано значение большее <see cref="uint.MaxValue"/>.
    /// </exception>
    static void CheckSeed(long seed)
    {
        //  Проверка на отрицательное значение.
        IsNotNegative(seed, nameof(seed));

        //  Проверка на переполнение.
        IsNotLess(seed, uint.MaxValue, nameof(seed));
    }
}
