
namespace Simargl.Server.Hub.Core
{
    class TestService(ILogger<TestService> logger) :
        BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Tick");

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
