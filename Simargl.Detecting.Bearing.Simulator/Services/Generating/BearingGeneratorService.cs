using Simargl.Detecting.Bearing.Simulator.Data;

namespace Simargl.Detecting.Bearing.Simulator.Services.Generating;

/// <summary>
/// Представляет службу генерации данных подшипника.
/// </summary>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
/// <param name="dataHub">
/// Узел данных.
/// </param>
public sealed class BearingGeneratorService(ILogger<BearingGeneratorService> logger, DataHub dataHub) :
    BaseService(logger)
{
    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая асинхронную операцию.
    /// </returns>
    protected override sealed async Task InvokeAsync(CancellationToken cancellationToken)
    {
        _ = dataHub;

    }
}
