namespace Simargl.Projects.Oriole.Oriole01Storage.Entities;

/// <summary>
/// Представляет значение, определяющее состояние данных файла Nmea.
/// </summary>
public enum NmeaFileState
{
    /// <summary>
    /// Файл зарегистрирован.
    /// </summary>
    Registered = 0,

    /// <summary>
    /// Файл разобран.
    /// </summary>
    Parsed = 1,
}
