using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RailTest.Satellite.Autonomic
{
    /// <summary>
    /// Представляет фоновый сервис.
    /// </summary>
    /// <seealso cref="BackgroundService"/>
    public class Worker : BackgroundService
    {
        /// <summary>
        /// Поле для хранения журнала.
        /// </summary>
        private readonly ILogger<Worker> _Logger;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="logger">
        /// Журнал.
        /// </param>
        public Worker(ILogger<Worker> logger)
        {
            _Logger = logger;
        }

        /// <summary>
        /// Этот метод вызывается при запуске <see cref="IHostedService"/>.
        /// Реализация должна возвращать задачу, которая представляет время существования выполняемых длительных операций.
        /// </summary>
        /// <param name="stoppingToken">
        /// Токен отмены.
        /// </param>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //  Получение текущего времени.
                var currentTime = DateTime.Now;

                //  Проверка файлов в источнике.
                foreach (var file in new DirectoryInfo(Customization.SourcePath).GetFiles())
                {
                    //  Получение даты
                    var fileTime = GetFileTime(file);

                    //  Получение тайм-аута.
                    var timeout = currentTime - fileTime;

                    //  Проверка условия переноса.
                    var execute = timeout > Customization.TransferTimeout;

                    //  Перенос файла.
                    if (execute)
                    {
                        //  Строка вывода.
                        StringBuilder information = new StringBuilder();

                        //  Вывод информации о файле.
                        information.Append($"Перенос файла \"{file.Name}\": ");

                        //  Исходный путь к файлу.
                        string sourceFileName = file.FullName;

                        //  Новый путь к файлу.
                        string destinationFileName = Path.Combine(Customization.DestinationPath, file.Name);

                        try
                        {
                            //  Перенос файла.
                            File.Move(sourceFileName, destinationFileName, true);

                            //  Вывод информации об успешном результате.
                            information.Append($"успешно.");
                        }
                        catch (Exception ex)
                        {
                            //  Вывод информации об ошибке.
                            information.Append($"ошибка ({ex.Message}).");
                        }

                        //  Вывод информации.
                        _Logger.LogInformation(information.ToString());
                    }
                }

                await Task.Delay(10000, stoppingToken);
            }
        }

        /// <summary>
        /// Получение времени файла.
        /// </summary>
        /// <param name="file">
        /// Файл.
        /// </param>
        /// <returns>
        /// Время файла.
        /// </returns>
        static DateTime GetFileTime(FileInfo file)
        {
            var creationTime = File.GetCreationTime(file.FullName);
            var lastWriteTime = File.GetLastWriteTime(file.FullName);
            var lastAccessTime = File.GetLastAccessTime(file.FullName);

            var time = creationTime;

            if (time < lastWriteTime)
            {
                time = lastWriteTime;
            }

            if (time < lastAccessTime)
            {
                time = lastAccessTime;
            }

            return time;
        }
    }
}
