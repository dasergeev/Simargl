namespace Simargl.Trials.Aurora.Aurora01.Storage.Entities;

/// <summary>
/// Представляет значение, определяющее состояние каталога, содержащего данные за час.
/// </summary>
public enum HourDirectoryState :
    byte
{
    /// <summary>
    /// Каталог зарегистрирован.
    /// </summary>
    Registered = 0,

    /// <summary>
    /// Каталог разобран.
    /// </summary>
    Parsed = 1,
}
