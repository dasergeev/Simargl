using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Simargl.Border.Recorder.Services.Critical;

/// <summary>
/// Представляет службу инициализации.
/// </summary>
/// <param name="program">
/// Программа.
/// </param>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
/// <param name="configuration">
/// Набор конфигурационных свойств приложения.
/// </param>
/// <param name="lifetime">
/// Уведомления о событиях жизненного цикла приложения.
/// </param>
public sealed class InitializationService(
    Program program, ILogger<InitializationService> logger,
    IConfiguration configuration, IHostApplicationLifetime lifetime) :
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
        try
        {
            //  Инициализация программы.
            await Program.InitializationAsync(Logger, configuration, cancellationToken).ConfigureAwait(false);
        }
        catch
        {
            //  Завершение работы приложения.
            lifetime.StopApplication();
        }
    }
}
