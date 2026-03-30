using System;
using System.Runtime.CompilerServices;

namespace Simargl.Recording.Geolocation;

/// <summary>
/// Представляет свойства и методы для получения дополнительной информации
/// и выполнении операций над точкой GPS, определяющей местоположение
/// в МГС по ГОСТ Р 51794-2001 (WGS 84).
/// </summary>
public class GpsPointInfo
{
    /// <summary>
    /// Постоянная, определяющая значение большой полуоси общеземного эллипсоида
    /// в системе МГС (WGS 84) в метрах.
    /// </summary>
    /// <remarks>
    /// См. пункт 3.2. ГОСТ Р 51794-2001.
    /// </remarks>
    public const double SemimajorAxisEllipsoid = 6378137;

    /// <summary>
    /// Постоянная, определяющая сжатие общеземного эллипсоида
    /// в системе МГС (WGS 84).
    /// </summary>
    /// <remarks>
    /// См. пункт 3.2. ГОСТ Р 51794-2001.
    /// </remarks>
    public const double EllipsoidCompression = 1.0 / 298.257223563;

    /// <summary>
    /// Постоянная, определяющая квадрат эксцентриситета общеземного эллипсоида
    /// в системе МГС (WGS 84).
    /// </summary>
    /// <remarks>
    /// См. формулу (3) в пункте 4.1. ГОСТ Р 51794-2001.
    /// </remarks>
    public const double SquareEllipsoidEccentricity = (2.0 - EllipsoidCompression) * EllipsoidCompression;

    /// <summary>
    /// Происходит при изменении свойства <see cref="Point"/>.
    /// </summary>
    public event EventHandler? PointChanged;

    /// <summary>
    /// Поле для хранения точки GPS.
    /// </summary>
    private GpsPoint _Point;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="point">
    /// Точка GPS.
    /// </param>
    public GpsPointInfo(GpsPoint point)
    {
        //  Установка точки.
        _Point = point;

        //  Расчёт параметров.
        ParameterCalculationCore();
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="latitude">
    /// Широта в градусах.
    /// </param>
    /// <param name="longitude">
    /// Долгота в градусах.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="latitude"/> передано значение,
    /// которое меньше значения <see cref="GpsPoint.MinLatitude"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="latitude"/> передано значение,
    /// которое превышает значение <see cref="GpsPoint.MaxLatitude"/>.
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
    public GpsPointInfo(double latitude, double longitude) :
        this(new GpsPoint(latitude, longitude))
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
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
    /// которое меньше значения <see cref="GpsPoint.MinLatitude"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="latitude"/> передано значение,
    /// которое превышает значение <see cref="GpsPoint.MaxLatitude"/>.
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
    public GpsPointInfo(double latitude, double longitude, double altitude) :
        this(new GpsPoint(latitude, longitude, altitude))
    {

    }

    /// <summary>
    /// Возвращает или задаёт точку GPS.
    /// </summary>
    public GpsPoint Point
    {
        get => _Point;
        set
        {
            //  Проверка изменения значения.
            if (_Point != value)
            {
                //  Установка нового значения.
                _Point = value;

                //  Расчёт параметров.
                ParameterCalculationCore();

                //  Вызов события.
                OnPointChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Возвращает широту в градусах.
    /// </summary>
    public double Latitude => _Point.Latitude;

    /// <summary>
    /// Возвращает долготу в градусах.
    /// </summary>
    public double Longitude => _Point.Longitude;

    /// <summary>
    /// Возвращает высоту над уровнем моря в метрах.
    /// </summary>
    public double Altitude => _Point.Altitude;

    /// <summary>
    /// Возвращает радиус кривизны первого вертикала для данного метсоположения
    /// в системе МГС (WGS 84) в метрах.
    /// </summary>
    /// <remarks>
    /// См. формулу (2) в пункте 4.1. ГОСТ Р 51794-2001.
    /// </remarks>
    public double FirstVerticalRadius { get; private set; }

    /// <summary>
    /// Возвращает абсциссу точки в пространственной системе координат,
    /// определённой в пункте 3.1. ГОСТ Р 51794-2001.
    /// </summary>
    /// <remarks>
    /// См. формулу (1) в пункте 4.1. ГОСТ Р 51794-2001.
    /// </remarks>
    public double X { get; private set; }

    /// <summary>
    /// Возвращает ординату точки в пространственной системе координат,
    /// определённой в пункте 3.1. ГОСТ Р 51794-2001.
    /// </summary>
    /// <remarks>
    /// См. формулу (1) в пункте 4.1. ГОСТ Р 51794-2001.
    /// </remarks>
    public double Y { get; private set; }

    /// <summary>
    /// Возвращает аппликату точки в пространственной системе координат,
    /// определённой в пункте 3.1. ГОСТ Р 51794-2001.
    /// </summary>
    /// <remarks>
    /// См. формулу (1) в пункте 4.1. ГОСТ Р 51794-2001.
    /// </remarks>
    public double Z { get; private set; }

    /// <summary>
    /// Вызывает событие <see cref="PointChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnPointChanged(EventArgs e)
    {
        //  Вызов события.
        PointChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Выполняет расчёт параметров.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ParameterCalculationCore()
    {
        //  Коэффициент перевода градусов в радианы.
        const double radiansFactor = Math.PI / 180.0;

        //  Перевод угловых координат в радианы.
        double latitudeInRadians = _Point.Latitude * radiansFactor;
        double longitudeInRadians = _Point.Longitude * radiansFactor;

        //  Вычисление синуса широты.
        double sinLatitude = Math.Sin(latitudeInRadians);

        //  Вычисление радиуса кривизны первого вертикала.
        FirstVerticalRadius = SemimajorAxisEllipsoid / Math.Sqrt(
            1.0 - SquareEllipsoidEccentricity * sinLatitude * sinLatitude);

        //  Вычисление общей части абсциссы и ординаты.
        double xyPart = (FirstVerticalRadius + _Point.Altitude) * Math.Cos(latitudeInRadians);

        //  Вычисление декартовых координат.
        X = xyPart * Math.Cos(longitudeInRadians);
        Y = xyPart * Math.Sin(longitudeInRadians);
        Z = ((1 - SquareEllipsoidEccentricity) * FirstVerticalRadius + _Point.Altitude) * sinLatitude;
    }
}
