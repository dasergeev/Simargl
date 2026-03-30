namespace Simargl.Detecting.Bearing.Simulator.Services;

internal class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        int counter = 0;

        while (!stoppingToken.IsCancellationRequested)
        {
            counter++;

            _logger.LogInformation("Worker iteration {counter}", counter);

            await Task.Delay(1000, stoppingToken);
        }
    }
}