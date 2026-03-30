using Apeiron.Gps;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace Apeiron.Platform.Databases.OrioleDatabase.Entities;

/// <summary>
/// Представляет местоположение.
/// </summary>
public class Location :
    Entity
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    internal Location()
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
    /// Высоты над средним уровнем моря в метрах.
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
    public Location(double latitude, double longitude, double altitude)
    {
        //  Создание информации о GPS-точке.
        GpsPointInfo info = new(latitude, longitude, altitude);

        //  Установка свойств класса.
        Latitude = info.Latitude;
        Longitude = info.Longitude;
        Altitude = info.Altitude;
        FirstVerticalRadius = info.FirstVerticalRadius;
        X = info.X;
        Y = info.Y;
        Z = info.Z;
    }

    /// <summary>
    /// Возвращает широту в градусах.
    /// </summary>
    public double Latitude { get; private set; }

    /// <summary>
    /// Возвращает долготу в градусах.
    /// </summary>
    public double Longitude { get; private set; }

    /// <summary>
    /// Возвращает высоту над средним уровнем моря в метрах.
    /// </summary>
    public double Altitude { get; private set; }

    /// <summary>
    /// Возвращает радиус кривизны первого вертикала для данного метсоположения в системе МГС (WGS 84) в метрах.
    /// </summary>
    public double FirstVerticalRadius { get; private set; }

    /// <summary>
    /// Возвращает абсциссу точки в пространственной системе координат, определённой в пункте 3.1. ГОСТ Р 51794-2001.
    /// </summary>
    public double X { get; private set; }

    /// <summary>
    /// Возвращает ординату точки в пространственной системе координат, определённой в пункте 3.1. ГОСТ Р 51794-2001.
    /// </summary>
    public double Y { get; private set; }

    /// <summary>
    /// Возвращает аппликату точки в пространственной системе координат, определённой в пункте 3.1. ГОСТ Р 51794-2001.
    /// </summary>
    public double Z { get; private set; }

    /// <summary>
    /// Возвращает коллекцию статистических данных.
    /// </summary>
    public ObservableCollection<Statistic> Statistics { get; } = new();

    /// <summary>
    /// Возвращает коллекцию экстремальных данных.
    /// </summary>
    public ObservableCollection<Extremum> Extremums { get; } = new();

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<Location> typeBuilder)
    {
        //  Базовая настройка.
        Entity.BuildAction(typeBuilder);

        //  Настройка широты.
        typeBuilder.Property(location => location.Latitude);
        typeBuilder.HasIndex(location => location.Latitude);

        //  Настройка долготы.
        typeBuilder.Property(location => location.Longitude);
        typeBuilder.HasIndex(location => location.Longitude);

        //  Настройка высоты над средним уровнем моря в метрах.
        typeBuilder.Property(location => location.Altitude);
        typeBuilder.HasIndex(location => location.Altitude);

        //  Настройка радиуса кривизны первого вертикала для данного метсоположения
        typeBuilder.Property(location => location.FirstVerticalRadius);
        typeBuilder.HasIndex(location => location.FirstVerticalRadius);

        //  Настройка абсциссы точки в пространственной системе координат.
        typeBuilder.Property(location => location.X);
        typeBuilder.HasIndex(location => location.X);

        //  Настройка ординаты точки в пространственной системе координат.
        typeBuilder.Property(location => location.Y);
        typeBuilder.HasIndex(location => location.Y);

        //  Настройка аппликаты точки в пространственной системе координат.
        typeBuilder.Property(location => location.Z);
        typeBuilder.HasIndex(location => location.Z);

        //  Настройка альтернативного ключа.
        typeBuilder.HasAlternateKey(location => new
        {
            location.Latitude,
            location.Longitude,
        });
    }
}
