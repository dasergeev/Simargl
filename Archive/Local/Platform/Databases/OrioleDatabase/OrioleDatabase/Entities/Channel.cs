using System.Collections.ObjectModel;

namespace Apeiron.Platform.Databases.OrioleDatabase.Entities;

/// <summary>
/// Представляет канал.
/// </summary>
public class Channel
{
    /// <summary>
    /// Возвращает или инициализирует идентификатор канала.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Возвращает или задаёт идентификатор регистратора.
    /// </summary>
    public int RegistrarId { get; set; }

    /// <summary>
    /// Возвращает или задаёт имя канала.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт описание канала.
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт частоту дискретизации канала.
    /// </summary>
    public double Sampling { get; set; }

    /// <summary>
    /// Возвращает или задаёт частоту среза фильтра.
    /// </summary>
    public double Cutoff { get; set; }

    /// <summary>
    /// Возвращает или задаёт регистратор.
    /// </summary>
    public Registrar Registrar { get; set; } = null!;

    /// <summary>
    /// Возвращает коллекцию источников.
    /// </summary>
    public ObservableCollection<Source> Sources { get; } = new();

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
}
