using Microsoft.Extensions.Logging;
using Simargl.Hardware.Recorder.Core;
using Simargl.Hardware.Recorder.Measurement;

namespace Simargl.Hardware.Recorder.Services.Sensors;

/// <summary>
/// Представляет службу индикаторов измерений.
/// </summary>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
/// <param name="heart">
/// Сердце приложения.
/// </param>
public sealed class IndicatorService(
    ILogger<IndicatorService> logger, Heart heart) :
    Service<IndicatorService>(logger, heart)
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
        //  Основной цикл обновления.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Перебор индикаторов.
            foreach (MeasurementIndicator indicator in Heart.Measurer.Indicators)
            {
                //  Обновление индикатора.
                await indicator.UpdateAsync(cancellationToken).ConfigureAwait(false);
            }

            //  Перебор индикаторов.
            foreach (MeasurementIndicator indicator in Heart.Measurer.RSIndicators)
            {
                //  Обновление индикатора.
                await indicator.UpdateAsync(cancellationToken).ConfigureAwait(false);
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
        }
    }
}
