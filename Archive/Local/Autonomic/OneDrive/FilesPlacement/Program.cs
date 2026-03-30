using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RailTest.Satellite.Autonomic
{
    /// <summary>
    /// Представляет приложение.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Определяет точку входа в приложение.
        /// </summary>
        /// <param name="args">
        /// Аргументы.
        /// </param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Создает конструктор хоста.
        /// </summary>
        /// <param name="args">
        /// Аргументы.
        /// </param>
        /// <returns>
        /// Конструктор хоста.
        /// </returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
    }
}
