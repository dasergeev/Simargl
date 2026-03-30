using Microsoft.Extensions.Logging;
using Simargl.Hardware.Recorder.Core;

namespace Simargl.Hardware.Recorder.Services.Sensors;

/// <summary>
/// Представляет службу для работы с данными геолокации.
/// </summary>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
/// <param name="heart">
/// Сердце приложения.
/// </param>
public sealed class RS485Service(
    ILogger<RS485Service> logger, Heart heart) :
    Service<RS485Service>(logger, heart)
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
        await Heart.Measurer.RS485InvokeAsync(Logger, cancellationToken).ConfigureAwait(false);
    }
}
