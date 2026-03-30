namespace Apeiron.Platform.Databases.CentralDatabase.Entities;

public partial class GeneralDirectory
{
    /// <summary>
    /// Возвращает полный относительный путь.
    /// </summary>
    public override sealed string GetFullRelativePath() => PathBuilder.RelativeNormalize(Path);

    /// <summary>
    /// Возвращает коллекцию абсолютных путей.
    /// </summary>
    public override sealed IEnumerable<string> GetAbsolutePaths()
    {
        //  Получение относительного пути.
        string relativePath = GetFullRelativePath();

        //  Возврат коллекции абсолютных путей.
        return FileStorage.GetAbsolutePaths()
            .Select(storagePath => PathBuilder.Combine(storagePath, relativePath));
    }

    /// <summary>
    /// Асинхронно возвращает коллекцию абсолютных путей.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая возврат коллекции.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task<IEnumerable<string>> GetAbsolutePathsAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Возврат коллекции абсолютных путей.
        return GetAbsolutePaths();
    }
}
