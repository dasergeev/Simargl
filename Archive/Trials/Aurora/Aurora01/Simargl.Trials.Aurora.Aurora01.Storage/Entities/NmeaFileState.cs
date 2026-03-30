namespace Simargl.Trials.Aurora.Aurora01.Storage.Entities;

/// <summary>
/// Представляет значение, определяющее состояние данных файла Nmea.
/// </summary>
public enum NmeaFileState :
    byte
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
