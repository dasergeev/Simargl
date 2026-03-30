namespace Apeiron.Platform.Databases.CentralDatabase.Entities;

public partial class InternalFile
{
    /// <summary>
    /// Возвращает полный относительный путь.
    /// </summary>
    public override string GetFullRelativePath()
    {
        //  Возврат пути.
        return PathBuilder.RelativeNormalize(
            PathBuilder.Combine(GeneralDirectory.Path, Path));
    }

    /// <summary>
    /// Возвращает коллекцию абсолютных путей.
    /// </summary>
    public override sealed IEnumerable<string> GetAbsolutePaths()
    {
        //  Возврат коллекции путей.
        return GeneralDirectory.GetAbsolutePaths()
            .Select(generalPath => PathBuilder.Combine(generalPath, Path));
    }
}
