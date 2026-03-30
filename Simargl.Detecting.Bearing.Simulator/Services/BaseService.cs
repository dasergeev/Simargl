namespace Simargl.Detecting.Bearing.Simulator.Services;

/// <summary>
/// Представляет службу.
/// </summary>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
public abstract class BaseService(ILogger logger) :
    BackgroundService
{
    /// <summary>
    /// Возвращает средство ведения журнала.
    /// </summary>
    protected ILogger Logger { get; } = logger;

    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая асинхронную операцию.
    /// </returns>
    protected abstract Task InvokeAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно выполняет поддержку работы службы.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая асинхронную операцию.
    /// </returns>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        //  Основной цикл поддержки.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Выполнение основной работы.
                await InvokeAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                //  Проверка токена отмены.
                if (cancellationToken.IsCancellationRequested)
                {
                    //  Заверешение работы.
                    return;
                }

                //  Вывод информации в журнал.
                Logger.LogError(ex, "Произошла ошибка при работе службы.");
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(100, cancellationToken).ConfigureAwait(false);
        }
    }
}
