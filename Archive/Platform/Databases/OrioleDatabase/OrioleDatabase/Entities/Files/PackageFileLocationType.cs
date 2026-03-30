namespace Apeiron.Platform.Databases.OrioleDatabase.Entities;

/// <summary>
/// Представляет значение, определяющее тип расположения файла в последовательности.
/// </summary>
public enum PackageFileLocationType
{
    /// <summary>
    /// Некорректный файл.
    /// </summary>
    Incorrect,

    /// <summary>
    /// Одиночный файл.
    /// </summary>
    Single,

    /// <summary>
    /// Первый файл.
    /// </summary>
    First,

    /// <summary>
    /// Внутренний файл.
    /// </summary>
    Internal,

    /// <summary>
    /// Последний файл.
    /// </summary>
    Last,
}
