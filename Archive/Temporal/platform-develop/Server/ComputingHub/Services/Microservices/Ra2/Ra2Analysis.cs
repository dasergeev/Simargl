using Apeiron.Frames;
using Apeiron.Frames.TestLab;
using Apeiron.Platform.Databases.Ra2Database;
using ApeironApeiron.Platform.Databases.Ra2Database.Entities;

namespace Apeiron.Platform.Server.Services.Microservices;

/// <summary>
/// Представляет микрослужбу для обработки данных Ra2.
/// </summary>
public sealed class Ra2Analysis :
    ServerMicroservice<Ra2Analysis>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="logger">
    /// Средство записи в журнал.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logger"/> передана пустая ссылка.
    /// </exception>
    public Ra2Analysis(ILogger<Ra2Analysis> logger) : base(logger)
    {

    }

    /// <summary>
    /// Асинхронно выполняет шаг работы микрослужбы.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая шаг работы микрослужбы.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected override async ValueTask MakeStepAsync(CancellationToken cancellationToken)
    {
        Logger.LogInformation("Запуск поиска не проанализированных файлов");

        // Запуска поиска.
        await FramesAnalizedAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет поиск информации в базе данных.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая поиск.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async Task FramesAnalizedAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание контекста сеанса работы с базой данных.
        await using Ra2DatabaseContext context = new();

        long[] frameIds = await context.RawFiles
            .Where(x => !x.IsAnalyzed)
            .Select(x => x.Id)
            .ToArrayAsync(cancellationToken)
            .ConfigureAwait(false);

        //  Создание генератора случайных чисел.
        Random random = new(unchecked((int)(DateTime.Now.Ticks % int.MaxValue)));

        //  Перетасовка массива кадров.
        for (int i = 0; i < frameIds.Length; i++)
        {
            //  Получение нового индекса.
            int index = random.Next() % frameIds.Length;

            //  Перестановка файлов.
            (frameIds[i], frameIds[index]) = (frameIds[index], frameIds[i]);
        }

        //  Запись информации в журнал.
        Logger.LogInformation("Начало обработки исходных фалов: {filesCount}", frameIds.Length);

        //  Асинхронная параллельная обработка файлов.
        await Parallel.ForEachAsync(
            frameIds,
            new ParallelOptions()
            {
                CancellationToken = cancellationToken,
            },
            WorkingWithFrameAsync).ConfigureAwait(false);

        //  Запись информации в журнал.
        Logger.LogInformation("Обработка завершена.");
    }

    /// <summary>
    /// Асинхронно выполняет работу с кадром регистрации.
    /// </summary>
    /// <param name="fileId">
    /// Идентификатор кадра.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с кадром регистрации.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async ValueTask WorkingWithFrameAsync(long fileId, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Безопасная работа с файлом.
        await SafeInvokeAsync(async cancellationToken =>
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Создание подключения к базе данных.
            using Ra2DatabaseContext context = new();

            //  Поиск результатов обработки в базе данных.
            if (context.RawFiles
                .Any(x => x.Id == fileId && x.IsAnalyzed))
            {
                //  Кадр уже обработан.
                return;
            }

            //  Начало транзакции.
            using var transaction = context.Database.BeginTransaction();

            //  Получение информации о кадре.
            RawFile? rawFile = await context.RawFiles
                .Where(x => x.Id == fileId)
                .FirstOrDefaultAsync(cancellationToken);

            //  Проверка информации о кадре.
            if (rawFile is null)
            {
                //  Не удалось получить путь к кадру.
                return;
            }

            //  !!!!!!!!!!!!!!!!!!!!!!!!!

            //  Установка флага обработки.
            rawFile.IsAnalyzed = true;  //< -добавить когда всё ок будет!!
            rawFile.ChannelResults.Clear(); 
            //  Очистка данных файла!!!! - очистить список связанных сущностей с файлом.

            //  !!!!!!!!!!!!!!!!!!!!!!!!!


            //  Установка времени записи кадра.
            rawFile.Time = File.GetLastWriteTime(rawFile.FilePath);

            //  Открытие кадра.
            Frame frame = new(rawFile.FilePath);

            //  Получение канала широты.
            Channel latitudes = frame.Channels["Lat_GPS"];

            //  Получение канала долготы.
            Channel longitudes = frame.Channels["Lon_GPS"];

            //  Проверка пустого кадра.
            rawFile.IsEmpty = latitudes.Any(value => value == 0) || longitudes.Any(value => value == 0);

            //  Проверка GPS-данных.
            if (!rawFile.IsEmpty)
            {
                //  Определение средней скорости.
                rawFile.Speed = frame.Channels["V_GPS"].Average();

                //  Установка долготы.
                rawFile.Latitude = latitudes.Average();

                //  Установка широты.
                rawFile.Longitude = longitudes.Average();
            }

            //  Перебор всех каналов.
            foreach (Channel channel in frame.Channels)
            {
                //  Проверка заголовка канала.
                if (channel.Header is TestLabChannelHeader header)
                {
                    //  Формируем сущность.
                    ChannelResult channelResult = new()
                    {
                        RawFileId = fileId,
                        Name = channel.Name,
                        Unit = channel.Unit,
                        Sampling = channel.Sampling,
                        Cutoff = channel.Cutoff,
                        Offset = header.Offset,
                        Scale = header.Scale,
                        Count = channel.Length,
                        Min = channel.Min(),
                        Max = channel.Max(),
                        Sum = channel.Sum(),
                        SquaresSum = channel.Sum(x => x * x),
                    };

                    // Добавляем сущность в таблицу.
                    context.ChannelResults.Add(channelResult);
                }
            }

            //  Сохранение изменений в базу данных.
            context.SaveChanges();

            //  Фиксирование изменений.
            transaction.Commit();

        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно безопасно выполняет действие.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнеить.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, безопасно выполняющая действие.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async ValueTask SafeInvokeAsync(Func<CancellationToken, ValueTask> action, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Блок перехвата исключений для вывода информации в журнал.
        try
        {
            //  Выполнение действия.
            await action(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            //  Проверка токена отмены.
            if (!cancellationToken.IsCancellationRequested)
            {
                //  Запись информации об ошибке в журнал.
                Logger.LogError("{exception}", ex);
            }
            else
            {
                //  Повторный выброс исключения.
                throw;
            }
        }
    }
}
