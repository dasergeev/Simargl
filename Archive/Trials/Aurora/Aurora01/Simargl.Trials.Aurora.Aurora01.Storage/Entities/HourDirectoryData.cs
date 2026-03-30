using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Simargl.Trials.Aurora.Aurora01.Storage.Entities;

/// <summary>
/// Представляет каталог, содержащий данные за час.
/// </summary>
public class HourDirectoryData
{
    /// <summary>
    /// Возвращает или задаёт метку времени.
    /// </summary>
    public int Timestamp { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее состояние каталога, содержащего данные за час.
    /// </summary>
    public HourDirectoryState State { get; set; }

    /// <summary>
    /// Возвращает коллекцию информации о файлах данных датчиков.
    /// </summary>
    public HashSet<AdxlFileData> AdxlFiles { get; set; } = [];

    /// <summary>
    /// Возвращает коллекцию информации о файлах данных Nmea.
    /// </summary>
    public HashSet<NmeaFileData> NmeaFiles { get; set; } = [];

    /// <summary>
    /// Возвращает коллекцию информации о файлах сырых кадров.
    /// </summary>
    public HashSet<RawFrameFileData> RawFrameFiles { get; set; } = [];

    /// <summary>
    /// Возвращает время.
    /// </summary>
    /// <param name="timestamp">
    /// Метка времени.
    /// </param>
    /// <returns>
    /// Время.
    /// </returns>
    public static DateTime ToTime(int timestamp) => new(timestamp * TimeSpan.TicksPerHour);

    /// <summary>
    /// Преобразует время.
    /// </summary>
    /// <param name="time">
    /// Время.
    /// </param>
    /// <returns>
    /// Метка времени.
    /// </returns>
    public static int FromTime(DateTime time)
    {
        //  Нормализация времени.
        time = new(time.Year, time.Month, time.Day, time.Hour, 0, 0);

        //  Возврат метки времени.
        return (int)(time.Ticks / TimeSpan.TicksPerHour);
    }

    /// <summary>
    /// Возвращает имя каталога.
    /// </summary>
    /// <returns>
    /// Имя каталога.
    /// </returns>
    public string GetName()
    {
        //  Получение времени.
        DateTime time = ToTime(Timestamp);

        //  Возврат имени.
        return $"{time.Year:0000}-{time.Month:00}-{time.Day:00}-{time.Hour:00}";
    }

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    internal static void BuildAction(
        EntityTypeBuilder<HourDirectoryData> typeBuilder)
    {
        //  Установка первичного ключа.
        typeBuilder.HasKey(x => x.Timestamp);

        //  Отключение автоинкремента.
        typeBuilder.Property(x => x.Timestamp).ValueGeneratedNever();
    }
}
