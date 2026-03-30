namespace Apeiron.Platform.Databases.CentralDatabase.Entities;

public partial class FileStorage
{
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

    /// <summary>
    /// Возвращает коллекцию абсолютных путей.
    /// </summary>
    /// <returns>
    /// Коллекция абсолютных путей к файловому хранилищу.
    /// </returns>
    public IEnumerable<string> GetAbsolutePaths()
    {
        //  Возврат коллекции абсолютных путей.
        return Connectors
            .OrderBy(connector => connector.Priority)
            .Select(connector => PathBuilder.Normalize(connector.Path));
    }
}
