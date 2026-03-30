namespace Simargl.Hardware.Recorder.Measurement;

/// <summary>
/// Представляет показатель геолокации.
/// </summary>
public sealed class GeolocationIndicator
{
    /// <summary>
    /// Поле для хранения данных широты.
    /// </summary>
    private readonly ValueData _Latitude = new("0.000000", "°");

    /// <summary>
    /// Поле для хранения данных долготы.
    /// </summary>
    private readonly ValueData _Longitude = new("0.000000", "°");

    /// <summary>
    /// Поле для хранения данных скорости.
    /// </summary>
    private readonly ValueData _Speed = new("0.00", "км/ч");

    /// <summary>
    /// Возвращает широту.
    /// </summary>
    public string Latitude => _Latitude.GetText();

    /// <summary>
    /// Возвращает долготу.
    /// </summary>
    public string Longitude => _Longitude.GetText();

    /// <summary>
    /// Возвращает скорость.
    /// </summary>
    public string Speed => _Speed.GetText();

    /// <summary>
    /// Возвращает значение, определяющее требуется ли прорисовывать карту.
    /// </summary>
    public bool IsMap => Latitude != "-" && Longitude != "-";

    /// <summary>
    /// Возвращает широту.
    /// </summary>
    public double LatitudeValue => _Latitude.Value;

    /// <summary>
    /// Возвращает долготу.
    /// </summary>
    public double LongitudeValue => _Longitude.Value;

    /// <summary>
    /// Обновляет значения.
    /// </summary>
    /// <param name="latitude">
    /// Широта в градусах.
    /// </param>
    /// <param name="longitude">
    /// Долгота в градусах.
    /// </param>
    /// <param name="speed">
    /// Скорость в километрах в час.
    /// </param>
    public void Update(double? latitude, double? longitude, double? speed)
    {
        //  Обновление значений.
        _Latitude.Update(latitude);
        _Longitude.Update(longitude);
        _Speed.Update(speed);
    }

    class ValueData(string format, string unit)
    {
        public double Value { get; private set; }
        public DateTime Time { get; private set; } = DateTime.MinValue;
        public string Format { get; } = format;
        public string Unit { get; } = unit;
        
        public void Update(double? value)
        {
            if (value.HasValue)
            {
                Value= value.Value;
                Time = DateTime.Now;
            }
        }
        
        public string GetText()
        {
            if (DateTime.Now - Time < TimeSpan.FromSeconds(30))
            {
                return Value.ToString(Format) + " " + Unit;
            }
            else
            {
                return "-";
            }
        }
    }
}
