using Microsoft.Extensions.Logging;
using Simargl.Hardware.Recorder.Core;

namespace Simargl.Hardware.Recorder.Services.Sensors;

/// <summary>
/// Представляет службу для работы с данными датчиков ускорений.
/// </summary>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
/// <param name="heart">
/// Сердце приложения.
/// </param>
public sealed class AdxlService(
    ILogger<AdxlService> logger, Heart heart) :
    Service<AdxlService>(logger, heart)
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
        //  Работа с данными.
        await Heart.Measurer.AdxlInvokeAsync(Logger, cancellationToken).ConfigureAwait(false);
    }
}
