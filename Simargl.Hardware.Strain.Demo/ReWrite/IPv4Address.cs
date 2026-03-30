using System.Net;
using System.Net.Sockets;

namespace Simargl.Hardware.Strain.Demo.ReWrite;

/// <summary>
/// Представляет IPv4 адрес.
/// </summary>
public readonly struct IPv4Address :
    IEquatable<IPv4Address>
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
        _Value = bytes[3] | (uint)bytes[2] << 8 | (uint)bytes[1] << 16 | (uint)bytes[0] << 24;
    }

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
    /// Возвращает текстовое представление объекта.
    /// </summary>
    /// <returns>
    /// Текстовое представление объекта.
    /// </returns>
    public override string ToString()
    {
        //  Получение массива данных.
        byte[] bytes = GetAddressBytes();

        //  Возврат тектового представления.
        return $"{bytes[0]}.{bytes[1]}.{bytes[2]}.{bytes[3]}";
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
}
