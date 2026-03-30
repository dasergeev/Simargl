using Microsoft.Extensions.Logging;
using Simargl.Border.Processing;
using System.IO;

namespace Simargl.Border.Recorder.Services.Main;

/// <summary>
/// Представляет службу синхронизации.
/// </summary>
/// <param name="program">
/// Программа.
/// </param>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
public sealed class SynchronizationService(Program program, ILogger<SynchronizationService> logger) :
    Service(program, logger)
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
    protected override sealed async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Получение устройства обработки.
        Processor processor = await Program.GetProcessorAsync(cancellationToken).ConfigureAwait(false);

        //  Удаление каталога.
        if (Directory.Exists(BasisConstants.FrameQueuePath)) Directory.Delete(BasisConstants.FrameQueuePath, true);

        //  Создание каталога.
        Directory.CreateDirectory(BasisConstants.FrameQueuePath);

        //  Основной цикл работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Синхронизация данных.
            await processor.SynchronizationAsync(cancellationToken).ConfigureAwait(false);

            //  Ожидание перед следующим проходом.
            await Task.Delay(BasisConstants.MediumServicePeriod, cancellationToken).ConfigureAwait(false);
        }
    }
}
