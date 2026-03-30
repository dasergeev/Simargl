using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Simargl.Concurrent;
using Simargl.Trials.Aurora.Aurora01.Storage;

namespace Simargl.Trials.Aurora.Aurora01.Processor;

/// <summary>
/// Представляет основной фоновый процесс.
/// </summary>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
public partial class Worker(ILogger<Worker> logger) :
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
                //  Вывод информации в журнал.
                logger.LogInformation("Начало цикла обработки.");

                //  Создание источника токена отмены.
                using CancellationTokenSource linkedTokenSource =
                    CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

                //  Получение токена отмены.
                CancellationToken linkedToken = linkedTokenSource.Token;

                //  Подключение к базе данных.
                using Aurora01StorageContext storageContext = new();

                //  Создание критического объекта.
                using AsyncLock storageLock = new();

                //  Создание контекста обработки.
                ProcessorContext context = new();// (storageContext, storageLock);

                //  Создание списка задач.
                List<Task> tasks = [];

                //  Сканирование.
                run(ScanAsync);

                //  Работа с файлами Nmea.
                run(NmeaAsync);

                //  Работа с файлами Adxl.
                run(AdxlAsync);

                //  Работа с файлами RawFrame.
                run(RawFrameAsync);

                //  Ожидание выполнения всех задач.
                await Task.WhenAll(tasks).ConfigureAwait(false);

                //  Вывод информации в журнал.
                logger.LogInformation("Завершение цикла обработки.");

                //  Запускает задачу.
                void run(Func<ProcessorContext, CancellationToken, Task> action)
                {
                    //  Добавление задачи.
                    tasks.Add(Task.Run(async delegate
                    {
                        //  Основной цикл поддержки.
                        while (!cancellationToken.IsCancellationRequested)
                        {
                            //  Блок перехвата всех исключений.
                            try
                            {
                                //  Выполнения действия.
                                await action(context, linkedToken).ConfigureAwait(false);
                            }
                            catch (Exception ex)
                            {
                                //  Проверка токена отмены.
                                if (!cancellationToken.IsCancellationRequested)
                                {
                                    //  Вывод информации в журнал.
                                    logger.LogError("{action}: {ex}", action, ex);
                                }
                            }

                            //  Ожидание перед следующей попыткой.
                            await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
                        }
                    }, linkedToken));
                }
            }
            catch (Exception ex)
            {
                //  Проверка токена отмены.
                if (!cancellationToken.IsCancellationRequested)
                {
                    //  Вывод сообщения в журнал.
                    logger.LogError("{ex}", ex);
                }
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
        }
    }
}
