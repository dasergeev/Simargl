using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Simargl.Trials.Aurora.Aurora01.Nmea;

/// <summary>
/// Представляет основной фоновый процесс.
/// </summary>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
public partial class Worker(ILogger<Worker> logger) :
    BackgroundService
{
    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        //  Ожидание инициализации консоли.
        await Task.Delay(1000, cancellationToken).ConfigureAwait(false);

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Вывод сообщения в журнал.
        logger.LogInformation("Начало работы.");

        //  Блок перехвата всех исключений.
        try
        {
            //  Импорт данных.
            await ImportAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            //  Проверка токена отмены.
            if (!cancellationToken.IsCancellationRequested)
            {
                //  Вывод сообщения в журнал.
                logger.LogError("{ex}", ex);
            }
        }

        //  Вывод сообщения в журнал.
        logger.LogInformation("Конец работы.");
    }
}
