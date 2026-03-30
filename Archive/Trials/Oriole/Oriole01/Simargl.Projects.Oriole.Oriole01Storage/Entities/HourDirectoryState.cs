namespace Simargl.Projects.Oriole.Oriole01Storage.Entities;

/// <summary>
/// Представляет значение, определяющее состояние каталога, содержащего данные за час.
/// </summary>
public enum HourDirectoryState
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
