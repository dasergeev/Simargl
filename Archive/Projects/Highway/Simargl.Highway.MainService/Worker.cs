using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Simargl.Highway.MainService
{
    /// <summary>
    /// 
    /// </summary>
    public class Worker : BackgroundService
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger<Worker> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
