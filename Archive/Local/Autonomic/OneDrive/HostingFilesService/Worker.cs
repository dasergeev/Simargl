using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HostingFilesService
{
    public class Worker : BackgroundService
    {
        /// <summary>
        /// Возвращает тайм-аут переноса.
        /// </summary>
        public static TimeSpan TransferTimeout { get; } = new TimeSpan(0, 5, 0);

        const string _SourcePath = @"F:\Oriole";
        const string _ArchivePath = @"G:\Oriole Archive";

        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //  Получение текущего времени.
                var currentTime = DateTime.Now;

                var files = new DirectoryInfo(_SourcePath).GetFiles();
                foreach (var file in files)
                {
                    //  Получение даты
                    var fileTime = GetFileTime(file);

                    //  Получение тайм-аута.
                    var timeout = currentTime - fileTime;

                    //  Проверка условия переноса.
                    var execute = timeout > TransferTimeout;

                    //  Перенос файла.
                    if (execute)
                    {
                        //  Строка вывода.
                        StringBuilder information = new StringBuilder();

                        //  Вывод информации о файле.
                        information.Append($"Перенос файла \"{file.Name}\": ");

                        //  Получение даты изменения.
                        var time = File.GetLastWriteTime(file.FullName);

                        //  Формирование имени целевого каталога.
                        var path = Path.Combine(_ArchivePath, $"{time.Year:0000}-{time.Month:00}-{time.Day:00}");

                        //  Проверка существования каталога.
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        //  Учёт типа файла.
                        if (int.TryParse(file.Extension.Substring(1), out _))
                        {
                            //  Файл регистрации.
                            path = Path.Combine(path, "Frames");
                        }
                        else
                        {
                            //  Видео файл.
                            path = Path.Combine(path, "Video");
                        }

                        //  Проверка существования каталога.
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }


                        //  Исходный путь к файлу.
                        string sourceFileName = file.FullName;

                        //  Новый путь к файлу.
                        string destinationFileName = Path.Combine(path, file.Name);

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
                        _logger.LogInformation(information.ToString());
                    }
                }


                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
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
