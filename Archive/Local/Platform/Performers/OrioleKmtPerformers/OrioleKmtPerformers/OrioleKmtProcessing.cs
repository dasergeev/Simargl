using Apeiron.Frames;

namespace Apeiron.Platform.Performers.OrioleKmtPerformers;

/// <summary>
/// Представляет класс обработки кадров.
/// </summary>
public sealed class OrioleKmtProcessing : Performer
{
    /// <summary>
    /// Представляет путь к корневому каталогу размещения файлов.
    /// </summary>
    private const string _PathToRecordFiles = @"\\railtest.ru\Data\06-НТО\Records\2021\0021";

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="journal">Журнал.</param>
    public OrioleKmtProcessing(Journal journal) : base(journal)
    {
    }

    /// <summary>
    /// Асинхронно выполняет работу исполнителя.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу исполнителя.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public override async Task PerformAsync(CancellationToken cancellationToken)
    {
        //  Основной цикл.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Доп. задержка, для вывода изначальных сообщений в консоль.
            await Task.Delay(50, cancellationToken).ConfigureAwait(false);

            await Journal.LogInformationAsync(
                    $"Поиск файлов", cancellationToken).ConfigureAwait(false);

            // Запуск обработки файлов кадров.
            await ProcessingFileAsync(_PathToRecordFiles, cancellationToken).ConfigureAwait(false);

            await Journal.LogInformationAsync(
                    $"Анализ файлов кадров завершен.", cancellationToken).ConfigureAwait(false);

            //  Доп. задержка.
            await Task.Delay(15000, cancellationToken).ConfigureAwait(false);
        }
    }


    /// <summary>
    /// Асинхронно выполняет поиск информации по каталогу необработанных данных.
    /// </summary>
    /// <param name="filePath">Идентификатор каталога необработанных данных.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Задача, выполняющая поиск.</returns>
    /// <exception cref="OperationCanceledException">Операция отменена.</exception>
    private async Task ProcessingFileAsync(string filePath, CancellationToken cancellationToken)
    {
        Check.IsNotNull(filePath, nameof(filePath));

        //  Безопасное выполнение операции.
        await SafeCallAsync(async cancellationToken =>
        {
            //  Создание контекста сеанса работы с базой данных.
            using OrioleKmtDatabaseContext database = new();

            // Получение списка необработанных файлов и БД.
            var analyzedFrameFileList = await database.AnalyzedFrames
                .AsNoTracking()
                .Where(x => x.IsPocessed == false)
                .Select(y => y)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            //  Определение количества файлов.
            int fileCount = analyzedFrameFileList.Count;

            //  Вывод информации в журнал.
            await Journal.LogInformationAsync(
                $"Найдено {fileCount} новых файлов для обработки кадров. ",
                cancellationToken).ConfigureAwait(false);

            // Если есть файлы.
            if (fileCount > 0)
            {
                // Перемещивание списка.
                //  Создание генератора случайных чисел.
                Random random = new(unchecked((int)(DateTime.Now.Ticks % int.MaxValue)));

                //  Изменение порядка временных интервалов.
                for (int i = 0; i < analyzedFrameFileList.Count; i++)
                {
                    //  Получение нового индекса.
                    int index = random.Next() % analyzedFrameFileList.Count;

                    //  Перестановка элементов.
                    (analyzedFrameFileList[i], analyzedFrameFileList[index]) = (analyzedFrameFileList[index], analyzedFrameFileList[i]);
                }

                //  Асинхронное добавление информации о новых файлах в базу данных.
                await Parallel.ForEachAsync(analyzedFrameFileList, new ParallelOptions() { CancellationToken = cancellationToken },
                    async (analyzedFrame, cancellationToken) =>
                    {
                        //  Проверка токена отмены.
                        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                        //  Безопасное выполнение операции.
                        await SafeCallAsync(async cancellationToken =>
                        {
                            // Вывод информации в журнал.
                            await Journal.LogInformationAsync(
                            $"Обрабатывается файл {analyzedFrame.FilePath}",
                            cancellationToken).ConfigureAwait(false);

                            // Создание контекста сеанса работы с базой данных.
                            using OrioleKmtDatabaseContext database = new();

                            //  Начало транзакции.
                            using var transaction = await database.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

                            // Создание фрейма для работы с ним.
                            Frame processingFrame = new(analyzedFrame.FilePath);

                            analyzedFrame.Latitude = processingFrame.Channels["Lat_GPS"].Average();
                            analyzedFrame.Longitude = processingFrame.Channels["Lon_GPS"].Average();

                            // Канал curvatureChannel
                            Channel curvatureChannel = processingFrame.Channels["Curvature"];

                            if (curvatureChannel is not null)
                            {
                                double curvatureMin = curvatureChannel.Min();
                                double curvatureMax = curvatureChannel.Max();
                                double curvatureAverage = curvatureChannel.Average();

                                analyzedFrame.CurvatureMin = curvatureMin < double.MaxValue ? curvatureMin : double.MaxValue;
                                analyzedFrame.CurvatureMax = curvatureMax < double.MaxValue ? curvatureMax: double.MaxValue;
                                analyzedFrame.CurvatureAverage = curvatureAverage < double.MaxValue ? curvatureAverage : double.MaxValue;   
                            }

                            // Счетчики.
                            int countParking = 0;
                            int countTraction = 0;
                            int countBraking = 0;
                            int countRunout = 0;

                            // Получаем канал режимов для работы с ним.
                            var regimesChannel = processingFrame.Channels["Regimes"];

                            // Перебор значений в канале.
                            for (int i = 0; i < regimesChannel.Length; i++)
                            {
                                //  Проверка токена отмены.
                                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                                double value = regimesChannel[i];

                                switch (value)
                                {
                                    case > -1 and < 1:
                                        countParking++;
                                        break;
                                    case > 49 and < 51:
                                        countTraction++;
                                        break;
                                    case > -51 and < -49:
                                        countBraking++;
                                        break;
                                    case > 29 and < 31:
                                        countRunout++;
                                        break;
                                    default:
                                        break;
                                };
                            }

                            // Подсчет длительностей.
                            analyzedFrame.DurationParking = countParking / regimesChannel.Sampling;
                            analyzedFrame.DurationTraction = countTraction / regimesChannel.Sampling;
                            analyzedFrame.DurationBraking = countBraking / regimesChannel.Sampling;
                            analyzedFrame.DurationRunout = countRunout / regimesChannel.Sampling;

                            // Канал Mr.
                            Channel mrChannel = processingFrame.Channels["Mr"];

                            if (mrChannel is not null)
                            {
                                double tractionEffortMin = mrChannel.Min();
                                double tractionEffortMax = mrChannel.Max();
                                double tractionEffortSum = mrChannel.Sum();
                                double tractionEffortSquaredSum = mrChannel.Sum(x => x * x);

                                analyzedFrame.TractionEffortMin = tractionEffortMin < double.MaxValue ? tractionEffortMin : double.MaxValue;
                                analyzedFrame.TractionEffortMax = tractionEffortMax < double.MaxValue ? tractionEffortMax : double.MaxValue;
                                analyzedFrame.TractionEffortSum = tractionEffortSum < double.MaxValue ? tractionEffortSum : double.MaxValue;
                                analyzedFrame.TractionEffortSquaredSum = tractionEffortSquaredSum < double.MaxValue ? tractionEffortSquaredSum : double.MaxValue; 
                                analyzedFrame.TractionEffortCount = mrChannel.Length;
                            }

                            analyzedFrame.IsPocessed = true;

                            //  Установка состояния изменения записи о файле.
                            database.Entry(analyzedFrame).State = EntityState.Modified;
                            //  Обновление записи в БД.
                            database.AnalyzedFrames.Update(analyzedFrame);

                            //  Сохранение изменений в базу данных.
                            int count = await database.SaveChangesAsync(cancellationToken);

                            //  Фиксирование изменений.
                            await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);

                        }, cancellationToken).ConfigureAwait(false);

                    }).ConfigureAwait(false);
            }
            else
            {
                //  Вывод информации в журнал.
                await Journal.LogWarningAsync(
                    $"Файлы кадры для обработки не найдены!",
                    cancellationToken).ConfigureAwait(false);
            }

        }, cancellationToken).ConfigureAwait(false);

        //  Вывод информации в журнал.
        await Journal.LogWarningAsync(
            $"Обработка завершена!",
            cancellationToken).ConfigureAwait(false);
    }

}