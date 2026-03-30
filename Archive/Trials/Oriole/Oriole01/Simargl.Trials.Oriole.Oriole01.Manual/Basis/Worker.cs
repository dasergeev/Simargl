using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Simargl.Trials.Oriole.Oriole01.Manual.Basis;

/// <summary>
/// Представляет основной фоновый процесс.
/// </summary>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
public abstract class Worker<TWorker>(ILogger<TWorker> logger) :
    BackgroundService
    where TWorker : Worker<TWorker>
{
    /// <summary>
    /// Предоставляет точку входа фонового процесса.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, представляющая точку входа фонового процесса.
    /// </returns>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        //  Ожидание для инициализации консоли.
        await Task.Delay(1000, cancellationToken).ConfigureAwait(false);

        //  Основной цикл поддержки.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Выполнение основной работы.
                await InvokeAsync(cancellationToken).ConfigureAwait(false);

                //  Завершение работы.
                break;
            }
            catch (Exception ex)
            {
                //  Проверка токена отмены.
                if (!cancellationToken.IsCancellationRequested)
                {
                    //  Вывод информации в журнал.
                    logger.LogError("{ex}", ex);
                }
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    protected abstract Task InvokeAsync(CancellationToken cancellationToken);
}
