namespace Simargl.Projects.Oriole.Oriole01Storage.Entities;

/// <summary>
/// Представляет значение, определяющее состояние данных файла датчика.
/// </summary>
public enum AdxlFileState
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
