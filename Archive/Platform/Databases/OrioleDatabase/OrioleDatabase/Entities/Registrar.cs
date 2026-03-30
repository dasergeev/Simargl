using System.Collections.ObjectModel;

namespace Apeiron.Platform.Databases.OrioleDatabase.Entities;

/// <summary>
/// Представляет регистратор.
/// </summary>
public class Registrar
{
    /// <summary>
    /// Возвращает или инициализирует идентификатор регистратора.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Возвращает или задаёт имя регистратора.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Возвращает коллекцию каталогов необработанных данных.
    /// </summary>
    public ObservableCollection<RawDirectory> RawDirectories { get; } = new();

    /// <summary>
    /// Возвращает коллекцию каталогов с записями.
    /// </summary>
    public ObservableCollection<RecordDirectory> RecordDirectories { get; } = new();

    /// <summary>
    /// Возвращает коллекцию каналов.
    /// </summary>
    public ObservableCollection<Channel> Channels { get; } = new();

    /// <summary>
    /// Возвращает коллекцию сообщений GPS, содержащих данные местоположения.
    /// </summary>
    public ObservableCollection<GgaMessage> GgaMessages { get; } = new();

    /// <summary>
    /// Возвращает коллекцию сообщений GPS, содержащих минимальный рекомендованный набор данных.
    /// </summary>
    public ObservableCollection<RmcMessage> RmcMessages { get; } = new();

    /// <summary>
    /// Возвращает коллекцию сообщений GPS, содержащих данные о наземном курсе и скорости.
    /// </summary>
    public ObservableCollection<VtgMessage> VtgMessages { get; } = new();

    /// <summary>
    /// Возвращает коллекцию геолокационных данных.
    /// </summary>
    public ObservableCollection<Geolocation> Geolocations { get; } = new();

    /// <summary>
    /// Возвращает коллекцию кадров.
    /// </summary>
    public ObservableCollection<Frame> Frames { get; } = new();

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
