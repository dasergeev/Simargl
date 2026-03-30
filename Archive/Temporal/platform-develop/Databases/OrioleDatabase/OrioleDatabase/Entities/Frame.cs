using Apeiron.IO;
using System.Collections.ObjectModel;

namespace Apeiron.Platform.Databases.OrioleDatabase.Entities;

/// <summary>
/// Представляет кадр регистрации.
/// </summary>
public class Frame
{
    /// <summary>
    /// Возвращает или задаёт идентификатор регистратора.
    /// </summary>
    public int RegistrarId { get; set; }

    /// <summary>
    /// Возвращает или задаёт метку времени получения данных.
    /// </summary>
    public long Timestamp { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее построены ли спектры по кадру.
    /// </summary>
    public bool IsSpectrum { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее расчитана ли статистика по кадру.
    /// </summary>
    public bool IsStatistic { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее расчитаны ли экстремумы по кадру.
    /// </summary>
    public bool IsExtremum { get; set; }

    /// <summary>
    /// Возвращает или задаёт время начала записи в кадр.
    /// </summary>
    public DateTime Time { get; set; }

    /// <summary>
    /// Возвращает или задаёт номер кадра.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Возвращает или задаёт путь к файлу регистрации.
    /// </summary>
    public string Path { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт регистратор.
    /// </summary>
    public Registrar Registrar { get; set; } = null!;

    /// <summary>
    /// Возвращает коллекцию спектров.
    /// </summary>
    public ObservableCollection<Spectrum> Spectrums { get; } = new();

    /// <summary>
    /// Возвращает коллекцию статистических данных.
    /// </summary>
    public ObservableCollection<Statistic> Statistics { get; } = new();

    /// <summary>
    /// Возвращает коллекцию экстремальных данных.
    /// </summary>
    public ObservableCollection<Extremum> Extremums { get; } = new();

    /// <summary>
    /// Возвращает путь к файлу.
    /// </summary>
    /// <returns>
    /// Путь к файлу.
    /// </returns>
    public string GetPath()
    {
        //  Получение пути к корневому каталогу.
        string rootPath = PathBuilder.Normalize(Registrar.RecordDirectories.First().Path);

        //  Определение пути к каталогу, содержащему кадр.
        string directoryPath = PathBuilder.Combine(rootPath, $"{FromTimestamp(Timestamp):yyyy-MM-dd}");

        //  Определение разрешения файла.
        string extension = $".{Number:0000}";

        //  Поиск файла, содержащего кадр в каталоге.
        FileInfo fileInfo = new DirectoryInfo(directoryPath).GetFiles()
            .Where(fileInfo => fileInfo.Extension == extension)
            .First();

        //  Возврат пути к файлу.
        return PathBuilder.Normalize(fileInfo.FullName);
    }

    /// <summary>
    /// Возвращает метку времени для времени.
    /// </summary>
    /// <param name="time">
    /// Время.
    /// </param>
    /// <returns>
    /// Метка времени.
    /// </returns>
    public static long ToTimestamp(DateTime time)
    {
        //  Расчёт метки времени.
        return time.Ticks / TimeSpan.TicksPerMinute;
    }

    /// <summary>
    /// Возвращает время для заданной метки времени.
    /// </summary>
    /// <param name="timestamp">
    /// Метка времени.
    /// </param>
    /// <returns>
    /// Время.
    /// </returns>
    public static DateTime FromTimestamp(long timestamp)
    {
        //  Расчёт времени.
        return new DateTime(timestamp * TimeSpan.TicksPerMinute);
    }
}
