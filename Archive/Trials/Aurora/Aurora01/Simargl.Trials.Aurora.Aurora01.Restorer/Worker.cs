using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Simargl.Trials.Aurora.Aurora01.Restorer.Core;
using static Simargl.Trials.Aurora.Aurora01.Restorer.Core.Builder;
using static Simargl.Trials.Aurora.Aurora01.Restorer.RestorerTunnings;

namespace Simargl.Trials.Aurora.Aurora01.Restorer;

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
            //  Поиск непрерывных последовательностей файлов.
            if (IsSearch) await Explorer.SearchAsync(cancellationToken).ConfigureAwait(false);

            //  Сжатие кадров.
            if (IsCompression) await Compressor.CompressionAsync(cancellationToken).ConfigureAwait(false);

            //  Нормализация.
            await BuildAsync("Нормализация", NormalizeAsync, CompressedLabel, NormalizedLabel, cancellationToken).ConfigureAwait(false);

            //  Экспорт.
            await ExportAsync(cancellationToken).ConfigureAwait(false);

            //  Интегрирование.
            await BuildAsync("Интегрирование", IntegrateAsync, NormalizedLabel, IntegratedLabel, cancellationToken).ConfigureAwait(false);
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
