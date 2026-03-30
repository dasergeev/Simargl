using System.Text;

namespace Apeiron.Platform.Teltonika;

/// <summary>
/// Представляет класс данных геолокации.
/// </summary>
public class GeolocationInfo
{

    /// <summary>
    /// Возвращает широту.
    /// </summary>
    public float Latitude { get; private set; }

    /// <summary>
    /// Возвращает долготу.
    /// </summary>
    public float Longitude { get; private set; }

    /// <summary>
    /// Возвращает высоту.
    /// </summary>
    public float Altitude { get; private set; }

    /// <summary>
    /// Возвращает время GPS.
    /// </summary>
    public DateTime Time { get; private set; }

    /// <summary>
    /// Возвращает скорость.
    /// </summary>
    public int Speed { get; private set; }

    /// <summary>
    /// Возвращает количество спутников.
    /// </summary>
    public int SateliteCount { get; private set; }

    /// <summary>
    /// Возвращает точность.
    /// </summary>
    public float Accuracy { get; private set; }

    /// <summary>
    /// Инициализирует объект
    /// </summary>
    /// <param name="latitude">
    /// Широта
    /// </param>
    /// <param name="longitude">
    /// Долгота
    /// </param>
    /// <param name="altitude">
    /// Высота.
    /// </param>
    /// <param name="time">
    /// Время.
    /// </param>
    /// <param name="speed">
    /// Скорость.
    /// </param>
    /// <param name="sateliteCount">
    /// Количество спутников.
    /// </param>
    /// <param name="accuracy">
    /// Точность.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметер <paramref name="time"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="FormatException">
    /// В параметре <paramref name="time"/> передано время в неизвестном формате.
    /// </exception>
    public GeolocationInfo(float latitude, float longitude, float altitude, string time, int speed, int sateliteCount, float accuracy)
    {
        //  Инициализация широты.
        Latitude = latitude;

        //  Инициализация долготы.
        Longitude = longitude;
        
        //  Инициализация высоты.
        Altitude = altitude;

        //  Инициализация времени.
        Time = DateTime.Parse(time);

        //  Инициализация скорости.
        Speed = speed;

        //  Инициализация количества спутников.
        SateliteCount = sateliteCount;

        //  Инициализация точности.
        Accuracy = accuracy;
    }


    /// <summary>
    /// Преобразует данные в строки.
    /// </summary>
    /// <returns>
    /// Строка данных.
    /// </returns>
    public override string ToString()
    {
        //  Создание констрктора строки
        StringBuilder builder = new(2048);


        //  Добавление широты
        builder.Append($"Latitude\t{Latitude}\r\n");

        //  Добавление долготы.
        builder.Append($"Longitude\t{Longitude}\r\n");

        //  Добавление высоты
        builder.Append($"Altitude\t{Altitude}\r\n");

        //  Добавление скорости
        builder.Append($"Speed\t{Speed}\r\n");

        //  Добавление точности
        builder.Append($"Accuracy\t{Accuracy}\r\n");
        
        //  Добавление времени.
        builder.Append($"Time\t{Time}\r\n");

        //  Добавление количества спутников.
        builder.Append($"SateliteCount\t{SateliteCount}\r\n");
            
        //  Возврат результата
        return builder.ToString();
    }

}
