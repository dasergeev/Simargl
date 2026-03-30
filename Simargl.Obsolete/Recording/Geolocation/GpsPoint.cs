using System.Runtime.CompilerServices;

namespace Simargl.Recording.Geolocation;

/// <summary>
/// Представляет точку GPS.
/// </summary>
public struct GpsPoint :
    IEquatable<GpsPoint>
{
    /// <summary>
    /// Постоянная, определяющая минимальное значение широты в градусах.
    /// </summary>
    public const double MinLatitude = -90;

    /// <summary>
    /// Постоянная, определяющая максимальное значение широты в градусах.
    /// </summary>
    public const double MaxLatitude = 90;

    /// <summary>
    /// Поле для хранения широты в градусах.
    /// </summary>
    private readonly double _Latitude;

    /// <summary>
    /// Поле для хранения долготы в градусах.
    /// </summary>
    private readonly double _Longitude;

    /// <summary>
    /// Поле для хранения высоты над уровнем моря в метрах.
    /// </summary>
    private readonly double _Altitude;

    /// <summary>
    /// Инициализирует новый экземпляр структуры.
    /// </summary>
    /// <param name="latitude">
    /// Широта в градусах.
    /// </param>
    /// <param name="longitude">
    /// Долгота в градусах.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="latitude"/> передано значение,
    /// которое меньше значения <see cref="MinLatitude"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="latitude"/> передано значение,
    /// которое превышает значение <see cref="MaxLatitude"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="latitude"/> передано бесконечное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="latitude"/> передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="longitude"/> передано бесконечное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="longitude"/> передано нечисловое значение.
    /// </exception>
    public GpsPoint(double latitude, double longitude)
    {
        //  Инициализация широты.
        _Latitude = CheckLatitudeCore(latitude, nameof(latitude));

        //  Инициализация долготы.
        _Longitude = NormalizeLongitudeCore(longitude, nameof(longitude));

        //  Инициализация высоты над уровнем моря.
        _Altitude = double.NaN;
    }

    /// <summary>
    /// Инициализирует новый экземпляр структуры.
    /// </summary>
    /// <param name="latitude">
    /// Широта в градусах.
    /// </param>
    /// <param name="longitude">
    /// Долгота в градусах.
    /// </param>
    /// <param name="altitude">
    /// Высота над уровнем моря в метрах.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="latitude"/> передано значение,
    /// которое меньше значения <see cref="MinLatitude"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="latitude"/> передано значение,
    /// которое превышает значение <see cref="MaxLatitude"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="latitude"/> передано бесконечное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="latitude"/> передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="longitude"/> передано бесконечное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="longitude"/> передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="altitude"/> передано бесконечное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="altitude"/> передано нечисловое значение.
    /// </exception>
    public GpsPoint(double latitude, double longitude, double altitude)
    {
        //  Инициализация широты.
        _Latitude = CheckLatitudeCore(latitude, nameof(latitude));

        //  Инициализация долготы.
        _Longitude = NormalizeLongitudeCore(longitude, nameof(longitude));

        //  Инициализация высоты над уровнем моря.
        _Altitude = CheckAltitudeCore(altitude, nameof(altitude));
    }

    /// <summary>
    /// Выполняет операцию проверки на равенство.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="rigth">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    public static bool operator ==(GpsPoint left, GpsPoint rigth) =>
        left._Latitude == rigth._Latitude && left._Longitude == rigth._Longitude && left._Altitude == rigth._Altitude;

    /// <summary>
    /// Выполняет операцию проверки на равенство.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="rigth">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    public static bool operator !=(GpsPoint left, GpsPoint rigth) =>
        left._Latitude != rigth._Latitude || left._Longitude != rigth._Longitude || left._Altitude != rigth._Altitude;

    /// <summary>
    /// Возвращает или инициализирует широту в градусах.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано значение, которое меньше значения <see cref="MinLatitude"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано значение, которое превышает значение <see cref="MaxLatitude"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано бесконечное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано нечисловое значение.
    /// </exception>
    public readonly double Latitude
    {
        get => _Latitude;
        init => _Latitude = CheckLatitudeCore(value, nameof(Latitude));
    }

    /// <summary>
    /// Возвращает или инициализирует долготу в градусах.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано бесконечное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано нечисловое значение.
    /// </exception>
    public readonly double Longitude
    {
        get => _Longitude;
        init => _Longitude = NormalizeLongitudeCore(value, nameof(Longitude));
    }

    /// <summary>
    /// Возвращает или инициализирует высоту над уровнем моря в метрах.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано бесконечное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано нечисловое значение.
    /// </exception>
    public readonly double Altitude
    {
        get => _Altitude;
        init => _Altitude = CheckAltitudeCore(value, nameof(Altitude));
    }

    /// <summary>
    /// Указывает, равен ли этот экземпляр заданному объекту.
    /// </summary>
    /// <param name="other">
    /// Объект для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    /// Значение <c>true</c>, если <paramref name="other"/> и данный экземпляр представляют одинаковые значения;
    /// в противном случае - значение <c>false</c>.
    /// </returns>
    public readonly bool Equals(GpsPoint other)
    {
        //  Сравнение объектов.
        return other != this;
    }

    /// <summary>
    /// Указывает, равен ли этот экземпляр заданному объекту.
    /// </summary>
    /// <param name="other">
    /// Объект для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    /// Значение <c>true</c>, если <paramref name="other"/> и данный экземпляр представляют одинаковые значения;
    /// в противном случае - значение <c>false</c>.
    /// </returns>
    public readonly bool Equals(GpsPoint? other)
    {
        //  Проверка ссылки.
        if (other is null)
        {
            return false;
        }

        //  Сравнение объектов.
        return other != this;
    }

    /// <summary>
    /// Указывает, равен ли этот экземпляр заданному объекту.
    /// </summary>
    /// <param name="obj">
    /// Объект для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    /// Значение <c>true</c>, если <paramref name="obj"/> и данный экземпляр относятся к одному типу
    /// и представляют одинаковые значения;
    /// в противном случае - значение <c>false</c>.
    /// </returns>
    public override readonly bool Equals(object? obj)
    {
        //  Проверка типа.
        if (obj is GpsPoint point)
        {
            //  Сравнение с текущим экземпляром.
            return this == point;
        }

        //  Объект не приводится к целевому типу.
        return false;
    }

    /// <summary>
    /// Возвращает хэш-код данного экземпляра.
    /// </summary>
    /// <returns>
    /// 32-разрядное целое число со знаком, являющееся хэш-кодом для данного экземпляра.
    /// </returns>
    public override readonly int GetHashCode()
    {
        //  Возврат комбинированного хэш-кода.
        return base.GetHashCode();
    }

    /// <summary>
    /// Выполняет нормализацию значения долготы.
    /// </summary>
    /// <param name="longitude">
    /// Значение долготы.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Нормализованное знчение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="longitude"/> передано бесконечное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="longitude"/> передано нечисловое значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double NormalizeLongitudeCore(double longitude, string? paramName)
    {
        //  Проверка значения.
        CheckLongitudeCore(longitude, paramName);

        //  Корректировка долготы.
        longitude -= 360 * (Math.Ceiling((longitude + 180) / 360.0) - 1);

        //  Возврат нормализованного значения.
        return longitude;
    }

    /// <summary>
    /// Выполняет проверку значения широты.
    /// </summary>
    /// <param name="latitude">
    /// Значение широты.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="latitude"/> передано значение,
    /// которое меньше значения <see cref="MinLatitude"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="latitude"/> передано значение,
    /// которое превышает значение <see cref="MaxLatitude"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="latitude"/> передано бесконечное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="latitude"/> передано нечисловое значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double CheckLatitudeCore(double latitude, string? paramName)
    {
        //  Проверка на минимальное значение.
        IsNotLess(latitude, MinLatitude, paramName);

        //  Проверка на максимальное значение.
        IsNotLarger(latitude, MaxLatitude, paramName);

        //  Проверка на бесконечное значение.
        IsNotInfinity(latitude, paramName);

        //  Проверка на нечисловое значение.
        IsNotNaN(latitude, paramName);

        //  Возврат проверенного значения.
        return latitude;
    }

    /// <summary>
    /// Выполняет проверку значения долготы.
    /// </summary>
    /// <param name="longitude">
    /// Значение долготы.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="longitude"/> передано бесконечное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="longitude"/> передано нечисловое значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double CheckLongitudeCore(double longitude, string? paramName)
    {
        //  Проверка на бесконечное значение.
        IsNotInfinity(longitude, paramName);

        //  Проверка на нечисловое значение.
        IsNotNaN(longitude, paramName);

        //  Возврат проверенного значения.
        return longitude;
    }

    /// <summary>
    /// Выполняет проверку значения высоты над уровнем моря.
    /// </summary>
    /// <param name="altitude">
    /// Значение высоты над уровнем моря.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="altitude"/> передано бесконечное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="altitude"/> передано нечисловое значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double CheckAltitudeCore(double altitude, string? paramName)
    {
        //  Проверка на бесконечное значение.
        IsNotInfinity(altitude, paramName);

        //  Проверка на нечисловое значение.
        IsNotNaN(altitude, paramName);

        //  Возврат проверенного значения.
        return altitude;
    }
}
