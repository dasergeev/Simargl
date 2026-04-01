using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;

namespace Simargl.Net;

/// <summary>
/// Представляет IPv4 адрес.
/// </summary>
public readonly struct IPv4Address :
    IEquatable<IPv4Address>,
    IComparable<IPv4Address>,
    IComparable
{
    /// <summary>
    /// Поле для хранения значения адреса.
    /// </summary>
    private readonly uint _Value;

    /// <summary>
    /// Инициализирует новый экземпляр структуры.
    /// </summary>
    /// <param name="value">
    /// Значение адреса.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано значение,
    /// которое меньше значения <see cref="uint.MinValue"/> или больше значения <see cref="uint.MaxValue"/>.
    /// </exception>
    public IPv4Address(long value)
    {
        //  Установка значения адреса.
        _Value = (uint)IsInRange(value, uint.MinValue, uint.MaxValue, nameof(value));
    }

    /// <summary>
    /// Инициализирует новый экземпляр структуры.
    /// </summary>
    /// <param name="address">
    /// IP-адрес.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="address"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="address"/> передано значение,
    /// которое не соответствует допустимому диапазону значений.
    /// </exception>
    public IPv4Address(IPAddress address)
    {
        //  Проверка адреса.
        if (IsNotNull(address, nameof(address)).AddressFamily != AddressFamily.InterNetwork)
        {
            //  Передано значение, которое не соответствует допустимому диапазону значений.
            throw new ArgumentOutOfRangeException(nameof(address));
        }

        //  Получениме массива данных адреса.
        byte[] bytes = address.GetAddressBytes();

        //  Проверка массива данных адреса.
        if (bytes.Length != 4)
        {
            //  Передано значение, которое не соответствует допустимому диапазону значений.
            throw new ArgumentOutOfRangeException(nameof(address));
        }

        //  Расчёт значения адреса.
        _Value = BinaryPrimitives.ReadUInt32BigEndian(bytes);
    }

    /// <summary>
    /// Возвращает значение, определяющее является ли адрес пустым.
    /// </summary>
    public bool IsEmpty => _Value == 0;

    /// <summary>
    /// Возвращает значение адреса.
    /// </summary>
    public long Value => _Value;

    /// <summary>
    /// Выполняет преобразование адреса.
    /// </summary>
    /// <param name="address">
    /// Адрес для преобразования.
    /// </param>
    public static implicit operator IPAddress(IPv4Address address)
    {
        //  Получение массива данных.
        byte[] bytes = address.GetAddressBytes();

        //  Возврат IP-адреса.
        return new(bytes);
    }

    /// <summary>
    /// Выполняет операцию проверки на равенство.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    public static bool operator ==(IPv4Address left, IPv4Address right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Выполняет операцию проверки на неравенство.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    public static bool operator !=(IPv4Address left, IPv4Address right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Выполняет операцию проверки "меньше".
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// <see langword="true"/>, если <paramref name="left"/> меньше <paramref name="right"/>.
    /// В противном случае — <see langword="false"/>.
    /// </returns>
    public static bool operator <(IPv4Address left, IPv4Address right)
    {
        //  Сравнение значений.
        return left._Value < right._Value;
    }

    /// <summary>
    /// Выполняет операцию проверки "больше".
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// <see langword="true"/>, если <paramref name="left"/> больше <paramref name="right"/>.
    /// В противном случае — <see langword="false"/>.
    /// </returns>
    public static bool operator >(IPv4Address left, IPv4Address right)
    {
        //  Сравнение значений.
        return left._Value > right._Value;
    }

    /// <summary>
    /// Выполняет операцию проверки "меньше или равно".
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// <see langword="true"/>, если <paramref name="left"/> меньше
    /// или равен <paramref name="right"/>.
    /// В противном случае — <see langword="false"/>.
    /// </returns>
    public static bool operator <=(IPv4Address left, IPv4Address right)
    {
        //  Сравнение значений.
        return left._Value <= right._Value;
    }

    /// <summary>
    /// Выполняет операцию проверки "больше или равно".
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// <see langword="true"/>, если <paramref name="left"/> больше
    /// или равен <paramref name="right"/>.
    /// В противном случае — <see langword="false"/>.
    /// </returns>
    public static bool operator >=(IPv4Address left, IPv4Address right)
    {
        //  Сравнение значений.
        return left._Value >= right._Value;
    }

    /// <summary>
    /// Выполяет операцию побитового и.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    public static IPv4Address operator &(IPv4Address left, IPv4Address right)
    {
        return new(left._Value & right._Value);
    }

    /// <summary>
    /// Выполяет операцию побитового или.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    public static IPv4Address operator |(IPv4Address left, IPv4Address right)
    {
        return new(left._Value | right._Value);
    }

    /// <summary>
    /// Возвращает массив байт адреса.
    /// </summary>
    /// <returns>
    /// Массив байт адреса.
    /// </returns>
    public byte[] GetAddressBytes()
    {
        //  Получение значения адреса.
        uint value = _Value;

        //  Создание массива данных.
        byte[] bytes =
        [
            unchecked((byte)(value >> 24)),
            unchecked((byte)(value >> 16)),
            unchecked((byte)(value >> 8)),
            unchecked((byte)(value >> 0)),
        ];

        //  Возврат массива данных.
        return bytes;
    }

    /// <summary>
    /// Выполняет попытку разбора строки.
    /// </summary>
    /// <param name="value">
    /// Строковое представление адреса.
    /// </param>
    /// <param name="address">
    /// Результирующий адрес.
    /// </param>
    /// <returns>
    /// <see langword="true"/>, если разбор выполнен успешно.
    /// В противном случае — <see langword="false"/>.
    /// </returns>
    public static bool TryParse(string? value, out IPv4Address address)
    {
        //  Проверка строки.
        if (!IPAddress.TryParse(value, out IPAddress? ip) ||
            ip.AddressFamily != AddressFamily.InterNetwork)
        {
            address = default;
            return false;
        }

        //  Создание экземпляра.
        address = new IPv4Address(ip);
        return true;
    }

    /// <summary>
    /// Возвращает текстовое представление объекта.
    /// </summary>
    /// <returns>
    /// Текстовое представление объекта.
    /// </returns>
    public override string ToString()
    {
        //  Возврат тектового представления.
        return $"{(_Value >> 24) & 0xFF}." +
            $"{(_Value >> 16) & 0xFF}." +
            $"{(_Value >> 8) & 0xFF}." +
            $"{_Value & 0xFF}";
    }

    /// <summary>
    /// Проверяет на равенство с заданным экземпляром.
    /// </summary>
    /// <param name="other">
    /// Экземпляр для сравнения.
    /// </param>
    /// <returns>
    /// Результат сравнения.
    /// </returns>
    public bool Equals(IPv4Address other)
    {
        //  Сравнение значений.
        return _Value == other._Value;
    }

    /// <summary>
    /// Проверяет на равенство с заданным экземпляром.
    /// </summary>
    /// <param name="obj">
    /// Экземпляр для сравнения.
    /// </param>
    /// <returns>
    /// Результат сравнения.
    /// </returns>
    public override bool Equals(object? obj)
    {
        //  Проверка типа и сравнение значений.
        return obj is IPv4Address address && Equals(address);
    }

    /// <summary>
    /// Возвращает хэш-код объекта.
    /// </summary>
    /// <returns>
    /// Хэш-код объекта.
    /// </returns>
    public override int GetHashCode()
    {
        //  Возврат хэш-кода значения.
        return _Value.GetHashCode();
    }

    /// <summary>
    /// Выполняет сравнение с заданным экземпляром.
    /// </summary>
    /// <param name="other">
    /// Экземпляр для сравнения.
    /// </param>
    /// <returns>
    /// Отрицательное число, если текущий экземпляр меньше <paramref name="other"/>.
    /// Ноль, если экземпляры равны.
    /// Положительное число, если текущий экземпляр больше <paramref name="other"/>.
    /// </returns>
    public int CompareTo(IPv4Address other)
    {
        //  Сравнение значений.
        return _Value.CompareTo(other._Value);
    }

    /// <summary>
    /// Выполняет сравнение с заданным экземпляром.
    /// </summary>
    /// <param name="obj">
    /// Экземпляр для сравнения.
    /// </param>
    /// <returns>
    /// Отрицательное число, если текущий экземпляр меньше <paramref name="obj"/>.
    /// Ноль, если экземпляры равны.
    /// Положительное число, если текущий экземпляр больше <paramref name="obj"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// В параметре <paramref name="obj"/> передано значение,
    /// которое не является экземпляром <see cref="IPv4Address"/>.
    /// </exception>
    public int CompareTo(object? obj)
    {
        //  Проверка на null.
        if (obj is null)
            return 1;

        //  Проверка типа.
        if (obj is not IPv4Address address)
            throw new ArgumentException(
                $"Объект должен иметь тип {nameof(IPv4Address)}.",
                nameof(obj));

        //  Сравнение значений.
        return CompareTo(address);
    }
}
