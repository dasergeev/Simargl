namespace Apeiron.Platform.Databases.CentralDatabase.Entities;

/// <summary>
/// Представляет значение, определяющее формат файла.
/// </summary>
public enum InternalFileFormat
{
    /// <summary>
    /// Неизвестный формат файла.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Кадр регистрации.
    /// </summary>
    Frame = 1,
}
