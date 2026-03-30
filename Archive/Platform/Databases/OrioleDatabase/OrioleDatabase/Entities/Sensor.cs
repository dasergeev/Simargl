using System.Collections.ObjectModel;

namespace Apeiron.Platform.Databases.OrioleDatabase.Entities;

/// <summary>
/// Представляет датчик.
/// </summary>
public class Sensor
{
    /// <summary>
    /// Возвращает или инициализирует идентификатор датчика.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Возвращает или задаёт серийный номер датчика.
    /// </summary>
    public string SerialNumber { get; set; } = null!;

    /// <summary>
    /// Возвращает коллекцию источников.
    /// </summary>
    public ObservableCollection<Source> Sources { get; } = new();
}
