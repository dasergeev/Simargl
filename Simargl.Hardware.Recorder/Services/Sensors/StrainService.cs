using Microsoft.Extensions.Logging;
using Simargl.Hardware.Recorder.Core;

namespace Simargl.Hardware.Recorder.Services.Sensors;

/// <summary>
/// Представляет службу для работы с данными тензомодулей.
/// </summary>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
/// <param name="heart">
/// Сердце приложения.
/// </param>
public sealed class StrainService(
    ILogger<StrainService> logger, Heart heart) :
    Service<StrainService>(logger, heart)
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
        await Heart.Measurer.StrainInvokeAsync(Logger, cancellationToken).ConfigureAwait(false);
    }
}
