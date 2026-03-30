namespace Apeiron.Platform.MediatorLibrary.Requests;

/// <summary>
/// Представляет базовый класс запроса.
/// </summary>
public abstract class Request
{
    /// <summary>
    /// Возвращает размер.
    /// </summary>
    public virtual int Size { get; }

    /// <summary>
    /// Конструкторо по умолчанию.
    /// </summary>
    public Request()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public virtual Task SaveAsync(Stream stream, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public virtual Task<Request> LoadAsync(Stream stream, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
