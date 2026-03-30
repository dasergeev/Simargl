using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Simargl.Projects.Oriole.Oriole01Storage.Entities;

/// <summary>
/// Представляет каталог, содержащий данные за час.
/// </summary>
public class HourDirectoryData
{
    /// <summary>
    /// Возвращает или инициализирует ключ сущности.
    /// </summary>
    public long Key { get; init; }

    /// <summary>
    /// Возвращает или задаёт метку времени.
    /// </summary>
    public long Timestamp { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее состояние каталога, содержащего данные за час.
    /// </summary>
    public HourDirectoryState State { get; set; }

    /// <summary>
    /// Возвращает время.
    /// </summary>
    /// <returns>
    /// Время.
    /// </returns>
    public DateTime GetTime() => new(Timestamp);

    /// <summary>
    /// Возвращает коллекцию информации о файлах данных датчиков.
    /// </summary>
    public HashSet<AdxlFileData> AdxlFiles { get; set; } = [];

    /// <summary>
    /// Возвращает коллекцию информации о файлах данных Nmea.
    /// </summary>
    public HashSet<NmeaFileData> NmeaFiles { get; set; } = [];

    /// <summary>
    /// Устанавливает время.
    /// </summary>
    /// <param name="time">
    /// Время.
    /// </param>
    public void SetTime(DateTime time)
    {
        //  Нормализация времени.
        time = new(time.Year, time.Month, time.Day, time.Hour, 0, 0);

        //  Установка метки времени.
        Timestamp = time.Ticks;
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
        DateTime time = GetTime();

        //  Возврат имени.
        return $"{time.Year:0000}-{time.Month:00}-{time.Day:00}-{time.Hour:00}";
    }

    /// <summary>
    /// Устанавливает имя каталога.
    /// </summary>
    /// <param name="name">
    /// Имя каталога.
    /// </param>
    public void SetName(string name)
    {
        //  Нормализация имени.
        name = name.Trim();

        //  Разбор имени.
        int year = int.Parse(name[..4]);
        int month = int.Parse(name.Substring(5, 2));
        int day = int.Parse(name.Substring(8, 2));
        int hour = int.Parse(name.Substring(11, 2));

        //  Получение времени.
        DateTime time = new(year, month, day, hour, 0, 0);

        //  Установка времени.
        SetTime(time);
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
        //  Настройка первичного ключа.
        typeBuilder.HasKey(x => x.Key);

        //  Настройка уникальной метки времени.
        typeBuilder.HasIndex(x => x.Timestamp).IsUnique(true);
    }
}
