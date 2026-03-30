namespace Apeiron.Platform.Databases.CentralDatabase.Entities;

public partial class FileSystemElement
{
    /// <summary>
    /// Возвращает полный относительный путь.
    /// </summary>
    public abstract string GetFullRelativePath();

    /// <summary>
    /// Возвращает коллекцию абсолютных путей.
    /// </summary>
    public abstract IEnumerable<string> GetAbsolutePaths();
}
