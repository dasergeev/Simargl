using System.Collections.ObjectModel;

namespace Apeiron.Platform.Databases.OrioleDatabase.Entities;

/// <summary>
/// Представляет файл с пакетами данных.
/// </summary>
public class PackageFile :
    SourceFile
{
    /// <summary>
    /// Возвращает или задаёт формат данных.
    /// </summary>
    public int Format { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее загружен ли файл.
    /// </summary>
    public bool IsLoaded { get; set; }

    /// <summary>
    /// Возвращает или задаёт количество пакетов в файле.
    /// </summary>
    public int PackageCount { get; set; }

    /// <summary>
    /// Возвращает или задаёт длину синхронных сигналов.
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее нормализована ли запись.
    /// </summary>
    public bool IsNormalized { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее тип расположения файла в последовательности.
    /// </summary>
    public PackageFileLocationType LocationType { get; set; }

    /// <summary>
    /// Возвращает или задаёт индекс файла в последовательности.
    /// </summary>
    public int SequentialIndex  { get; set; }

    /// <summary>
    /// Возвращает или задаёт первый синхромаркер в тактах.
    /// </summary>
    public long FirstSynchromarker { get; set; }

    /// <summary>
    /// Возвращает или задаёт последний синхромаркер в тактах.
    /// </summary>
    public long LastSynchromarker { get; set; }

    /// <summary>
    /// Возвращает или задаёт частоту дискретизации.
    /// </summary>
    public double Sampling { get; set; }

    /// <summary>
    /// Возвращает или задаёт продолжительность синхронных сигналов в секундах.
    /// </summary>
    public double Duration { get; set; }

    /// <summary>
    /// Возвращает или задаёт нормализованное время начала файла.
    /// </summary>
    public DateTime NormalizedBeginTime { get; set; }

    /// <summary>
    /// Возвращает или задаёт нормализованное время конца файла.
    /// </summary>
    public DateTime NormalizedEndTime { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее проанализирован ли файл.
    /// </summary>
    public bool IsAnalyzed { get; set; }

    /// <summary>
    /// Возвращает коллекцию пакетов данных.
    /// </summary>
    public ObservableCollection<Package> Packages { get; } = new();
}
