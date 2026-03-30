using System.Collections.ObjectModel;

namespace Apeiron.Platform.Databases.OrioleDatabase.Entities;

/// <summary>
/// Представляет спектр.
/// </summary>
public class Spectrum
{
    /// <summary>
    /// Возвращает или инициализирует идентификатор спектра.
    /// </summary>
    public long Id { get; init; }

    /// <summary>
    /// Возвращает или задаёт идентификатор канала.
    /// </summary>
    public int ChannelId { get; set; }

    /// <summary>
    /// Возвращает или задаёт идентификатор регистратора.
    /// </summary>
    public int RegistrarId { get; set; }

    /// <summary>
    /// Возвращает или задаёт метку времени получения данных кадра.
    /// </summary>
    public long Timestamp { get; set; }

    /// <summary>
    /// Возвращает или задаёт регистратор.
    /// </summary>
    public Registrar Registrar { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт канал.
    /// </summary>
    public Channel Channel { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт кадр.
    /// </summary>
    public Frame Frame { get; set; } = null!;

    /// <summary>
    /// Возвращает коллекцию амплитуд.
    /// </summary>
    public ObservableCollection<Amplitude> Amplitudes { get; } = new();
}
