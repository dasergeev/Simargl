using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;

namespace Simargl.Trials.Aurora.Aurora01.FileMover;

/// <summary>
/// Представляет основной фоновый процесс.
/// </summary>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
public class Worker(ILogger<Worker> logger) :
    BackgroundService
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
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        //  Основной цикл.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Создание источника токена отмены.
                using CancellationTokenSource linkedTokenSource =
                    CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

                //  Выполнение работы в корневом каталоге.
                await directoryAsync(
                    new DirectoryInfo(Aurora01Tunings.TransferringRawDataPath),
                    string.Empty,
                    linkedTokenSource.Token);
            }
            catch (Exception ex)
            {
                //  Проверка токена отмены.
                if (!cancellationToken.IsCancellationRequested)
                {
                    //  Вывод сообщения в журнал.
                    logger.LogError("{ex}", ex);

                    //  Ожидание перед следующей попыткой.
                    await Task.Delay(10_000, cancellationToken).ConfigureAwait(false);

                    //  Переход к следующей попытке.
                    continue;
                }
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(3600_000, cancellationToken).ConfigureAwait(false);
        }

        //  Асинхронно выполняет работу в каталоге.
        async Task directoryAsync(
            DirectoryInfo directory, string path,
            CancellationToken cancellationToken)
        {
            //  Проверка токена отмены.
            await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

            //  Вывод информации в журнал.
            logger.LogInformation("{path}", path);

            //  Список задач.
            List<Task> tasks = [];

            //  Перебор подкаталогов.
            foreach (DirectoryInfo subDirectory in directory.GetDirectories("*", SearchOption.TopDirectoryOnly))
            {
                //  Проверка токена отмены.
                await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

                //  Добавление новой задачи.
                tasks.Add(Task.Run(async delegate
                {
                    //  Проверка токена отмены.
                    await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

                    //  Выполнение работы в подкаталоге.
                    await directoryAsync(subDirectory,
                        string.IsNullOrEmpty(path) ? subDirectory.Name : Path.Combine(path, subDirectory.Name),
                        cancellationToken);
                }, cancellationToken));
            }

            //  Перебор файлов.
            foreach (FileInfo file in directory.GetFiles("*", SearchOption.TopDirectoryOnly))
            {
                //  Проверка токена отмены.
                await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

                //  Получение локального пути.
                string localPath = string.IsNullOrEmpty(path) ? file.Name : Path.Combine(path, file.Name);

                //  Получение исходного пути.
                string sourcePath = Path.Combine(Aurora01Tunings.TransferringRawDataPath, localPath);

                //  Получение целевого пути.
                string targetPath = Path.Combine(Aurora01Tunings.NetworkRawDataPath, localPath);

                //  Блок перехвата всех исключений.
                try
                {
                    //  Получение целевого каталога.
                    DirectoryInfo targetDirectory = new FileInfo(targetPath).Directory!;

                    //  Создание целевого каталога.
                    Directory.CreateDirectory(targetDirectory.FullName);

                    //  Перемещение файла.
                    File.Move(sourcePath, targetPath, true);
                }
                catch (Exception ex)
                {
                    //  Вывод сообщения в журнал.
                    logger.LogError("{localPath}: {ex}", localPath, ex);
                }
            }

            //  Ожидание всех задач.
            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
    }
}
