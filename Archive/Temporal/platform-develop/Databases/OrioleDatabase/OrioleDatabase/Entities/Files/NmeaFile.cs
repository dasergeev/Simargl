using System.Collections.ObjectModel;

namespace Apeiron.Platform.Databases.OrioleDatabase.Entities;

/// <summary>
/// Представляет файл, содержащий NMEA сообщения.
/// </summary>
public class NmeaFile :
    SourceFile
{
    /// <summary>
    /// Возвращает или задаёт значение, определяющее загружен ли файл.
    /// </summary>
    public bool IsLoaded { get; set; }

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
}
