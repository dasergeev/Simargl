namespace Apeiron.Platform.OsmLibrary;

/// <summary>
/// Представляет класс для хранения точки с координатами.
/// </summary>
public sealed class GeolocationPoint : IEquatable<GeolocationPoint?>
{
    /// <summary>
    /// Широта.
    /// </summary>
    public double Latitude { get; init; }

    /// <summary>
    /// Долгота.
    /// </summary>
    public double Longitude { get; init; }

    /// <summary>
    /// Постоянная, определяющая минимальное значение широты в градусах.
    /// </summary>
    private const double MinLatitude = -90.0;

    /// <summary>
    /// Постоянная, определяющая максимальное значение широты в градусах.
    /// </summary>
    private const double MaxLatitude = 90.0;

    /// <summary>
    /// Постоянная, определяющая минимальное значение долготы в градусах.
    /// </summary>
    private const double MinLongitude = -180;

    /// <summary>
    /// Постоянная, определяющая максимальное значение долготы в градусах.
    /// </summary>
    private const double MaxLongitude = 180;

    

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    private GeolocationPoint(double lat, double lon)
    {
        Latitude = lat;
        Longitude = lon;
    }

    
    /// <summary>
    /// Создаёт точку с координатами.
    /// </summary>
    /// <param name="lat">Широта.</param>
    /// <param name="lon">Долгота.</param>
    /// <returns>Возращает точку с переданными координатами.</returns>
    public static GeolocationPoint CreatePoint(double lat, double lon)
    {
        // Проверки входящих координат.
        if (lat < MinLatitude || lat > MaxLatitude || double.IsNaN(lat) || double.IsInfinity(lat))
            throw new ArgumentOutOfRangeException(nameof(lat), "Значение широты выходит за границы допустимых значений.");

        if (lon < MinLongitude || lon > MaxLongitude || double.IsNaN(lon) || double.IsInfinity(lon))
            throw new ArgumentOutOfRangeException(nameof(lon), "Значение долготы выходит за границы допустимых значений.");

        // Создаёт точку с проверенными координатами.
        return new GeolocationPoint(lat, lon);
    }

    /// <summary>
    /// Оператор - сравнивает равны ли точки с координатами.
    /// </summary>
    /// <param name="left">Первая точка с координатами.</param>
    /// <param name="right">Вторая точка с координатами.</param>
    /// <returns>True если координаты точек равны, иначе - ложь.</returns>
    public static bool operator ==(GeolocationPoint left, GeolocationPoint right)
    {
        if (left.Latitude == right.Latitude && left.Longitude == right.Longitude)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Оператор проверяет, что точки с координатами не равны.
    /// </summary>
    /// <param name="left">Первая точка с координатами.</param>
    /// <param name="right">Вторая точка с координатами.</param>
    /// <returns>True если координаты точек не равны, иначе - ложь.</returns>
    public static bool operator !=(GeolocationPoint left, GeolocationPoint right)
    {
        if (left.Latitude != right.Latitude || left.Longitude != right.Longitude)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Возвращает хэш-код данного экземпляра.
    /// </summary>
    /// <returns>
    /// 32-разрядное целое число со знаком, являющееся хэш-кодом для данного экземпляра.
    /// </returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Latitude, Longitude);
        //    return base.GetHashCode();
    }

    /// <summary>
    /// Указывает, равен ли этот экземпляр заданному объекту.
    /// </summary>
    /// <param name="other">Объект для сравнения с текущим экземпляром.</param>
    /// <returns>
    /// Значение true, если other и данный экземпляр представляют одинаковые значения;
    /// в противном случае - значение false.
    /// </returns>
    public bool Equals(GeolocationPoint? other)
    {
        // Проверка, что не передана пустая ссылка.
        if (other is null)
        {
            return false;
        }

        // Если ссылки указывают на один и тот же адрес, то их идентичность гарантирована.
        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return this == other;
    }

    /// <summary>
    /// Указывает, равен ли этот экземпляр заданному объекту.
    /// </summary>
    /// <param name="obj">Объект для сравнения с текущим экземпляром.</param>
    /// <returns> Значение true, если obj и данный экземпляр относятся к одному типу и представляют 
    /// одинаковые значения; в противном случае - значение false.
    /// </returns>
    public override bool Equals(object? obj)
    {
        // Если переданный объект сравнения соотвествует текущему типу, то приводим к типу
        if (obj is GeolocationPoint geolocationPoint)
        {
            // Сравниваем, через реализованный оператор сравнения.
            return this == geolocationPoint;
        }

        // Объект с другим типом. 
        return false;
    }

    /// <summary>
    /// Возаращает широту и долготу точки в формате строки.
    /// </summary>
    /// <returns>Широта и долгота в формате строки.</returns>
    public override string ToString()
    {
        return Latitude.ToString() + ", " + Longitude.ToString();
    }
}
