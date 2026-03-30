using Apeiron.Oriole.Server.Services;

namespace Apeiron.Oriole.Server.Workers.Common;

/// <summary>
/// Представляет фоновый процесс службы.
/// </summary>
/// <typeparam name="TWorker">
/// Тип фонового процесса.
/// </typeparam>
public abstract class Worker<TWorker> :
    Service<TWorker>
    where TWorker : Worker<TWorker>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="logger">
    /// Средство записи в журнал службы.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logger"/> передана пустая ссылка.
    /// </exception>
    public Worker(ILogger<TWorker> logger) :
        base(logger)
    {

    }
}
