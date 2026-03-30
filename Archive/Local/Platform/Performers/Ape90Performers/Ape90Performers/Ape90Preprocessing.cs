using Apeiron.Analysis.Transforms;
using Apeiron.Frames;

namespace Apeiron.Platform.Performers.Ape90Performers;

/// <summary>
/// Представляет исполнителя, выполняющего предобработку данных.
/// </summary>
public sealed class Ape90Preprocessing :
    Performer
{
    /// <summary>
    /// Поле для хранения массива информации о каналах.
    /// </summary>
    private readonly ChannelInfo[] _ChannelInfos;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="journal">
    /// Журнал.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="journal"/> передана пустая ссылка.
    /// </exception>
    public Ape90Preprocessing(Journal journal) :
        base(journal)
    {
        //  Создание контекста сеанса работы с базой данных.
        using Ape90DatabaseContext context = Ape90DatabaseContextProvider.CreateContext();

        //  Получение информации о каналах.
        _ChannelInfos = context.ChannelInfos.ToArray();
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
    public override sealed async Task PerformAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Поддержка выполнения.
        await KeepAsync(async cancellationToken =>
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Создание контекста сеанса работы с базой данных.
            using Ape90DatabaseContext context = Ape90DatabaseContextProvider.CreateContext();

            //  Запрос необработанных данных.
            FrameInfo[] frameInfos = await context.FrameInfos
                .Where(frameInfo => !frameInfo.IsAnalyzed)
                .ToArrayAsync(cancellationToken)
                .ConfigureAwait(false);

            //  Создание генератора случайных чисел.
            Random random = new(unchecked((int)(DateTime.Now.Ticks % int.MaxValue)));

            //  Изменение порядка временных интервалов.
            for (int i = 0; i < frameInfos.Length; i++)
            {
                //  Получение нового индекса.
                int index = random.Next() % frameInfos.Length;

                //  Перестановка элементов.
                (frameInfos[i], frameInfos[index]) = (frameInfos[index], frameInfos[i]);
            }

            //  Предобработка кадров.
            await Parallel.ForEachAsync(
                frameInfos,
                new ParallelOptions
                {
                    CancellationToken = cancellationToken,
                },
                WorkWithFrameAsync).ConfigureAwait(false);
        },  cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет работу с кадром регистрации.
    /// </summary>
    /// <param name="frameInfo">
    /// Информация о кадре.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая шаг работы микрослужбы.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async ValueTask WorkWithFrameAsync(FrameInfo frameInfo, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Безопасный вызов.
        await SafeCallAsync(async cancellationToken =>
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Создание контекста сеанса работы с базой данных.
            using Ape90DatabaseContext context = Ape90DatabaseContextProvider.CreateContext();

            context.Attach(frameInfo);

            ////  Обновление информации о кадре.
            //frameInfo = (await context.FrameInfos.FindAsync(
            //    new object?[] { frameInfo.Id },
            //    cancellationToken).ConfigureAwait(false))!;

            ////  Проверка необходимоти обработки кадра.
            //if (frameInfo is null || frameInfo.IsAnalyzed)
            //{
            //    //  Завершение работы с кадром.
            //    return;
            //}

            //  Начало транзакции.
            using var transaction = context.Database.BeginTransaction();

            //  Открытие кадра.
            Frame frame = new(frameInfo.Path);

            //  Получение GPS-каналов.
            Channel isGpsValid = frame.Channels["IsGPS"];
            Channel speed = frame.Channels["V_GPS"];
            Channel latitude = frame.Channels["Lat_GPS"];
            Channel longitude = frame.Channels["Lon_GPS"];
            Channel altitude = frame.Channels["A_GPS"];

            //  Перебор каналов.
            foreach (ChannelInfo channelInfo in _ChannelInfos)
            {
                //  Получение целевого канала.
                Channel targetChannel = frame.Channels[channelInfo.Name];

                //  Получение канала смещения.
                Channel moveChannel = frame.Channels[channelInfo.Name + "_VMove"];

                //  Создание преобразования.
                SincFilter transform = new(channelInfo.Cutoff);

                //  Фильтрация канала.
                transform.Invoke(targetChannel, targetChannel);

                //  Перебор фрагментов.
                for (int i = 0; i < 60; i++)
                {
                    //  Длина фрагмента.
                    int length = (int)channelInfo.Sampling;

                    //  Определение начально индекса.
                    int beginIndex = i * length;

                    //  Определение индекса следующего за последним.
                    int endIndex = beginIndex + length;

                    //  Минимальное значени.
                    double min = double.MaxValue;

                    //  Максимальное значение.
                    double max = double.MinValue;

                    //  Сумма значений.
                    double sum = 0;

                    //  Сумма квадратов.
                    double squaresSum = 0;

                    //  Ноль определённый по скорости движения.
                    double speedZero = 0;

                    //  Перебор значений фрагмента.
                    for (int j = beginIndex; j < endIndex; j++)
                    {
                        //  Получение текущего значения.
                        double value = targetChannel[j];

                        //  Проверка минимального значения.
                        if (min > value)
                        {
                            //  Установка минимального значения.
                            min = value;
                        }

                        //  Проверка максимального значения.
                        if (max < value)
                        {
                            //  Установка максимального значения.
                            max = value;
                        }

                        //  Корректировка суммы значений.
                        sum += value;

                        //  Корректировка суммы квадратов.
                        squaresSum += value * value;

                        //  Корректировка ноля определённый по скорости движения.
                        speedZero += moveChannel[j];
                    }

                    //  Определение СКО.
                    double deviation = Math.Sqrt((squaresSum - sum * sum / length) / (length - 1));

                    //  Создание нового фрагмента.
                    Fragment fragment = new()
                    {
                        ChannelInfoId = channelInfo.Id,
                        FrameInfoId = frameInfo.Id,
                        Index = i,
                        Time = frameInfo.BeginTime.AddSeconds(i),
                        IsDataValid = deviation > 0.001,
                        SpeedZero = speedZero / length,
                        Count = (int)channelInfo.Sampling,
                        Min = min,
                        Max = max,
                        Average = sum / length,
                        Deviation = deviation,
                        Sum = sum,
                        SquaresSum = squaresSum,
                        IsGpsValid = isGpsValid[i] > 0.1,
                        Speed = speed[i],
                        Latitude = latitude[i],
                        Longitude = longitude[i],
                        Altitude = altitude[i],
                    };

                    //  Добавление фрагмента в базу данных.
                    context.Add(fragment);
                }
            }

            //  Присоединение информации о кадре к контексту.
            context.Attach(frameInfo);

            //  Установка флага для проанализированного кадра.
            frameInfo.IsAnalyzed = true;

            //  Сохранение изменений в базу данных.
            context.SaveChanges();

            //  Фиксирование изменений.
            transaction.Commit();

            //  Вывод в журнал.
            await Journal.LogInformationAsync(
                $"Обработан кадр {frameInfo.Path}",
                cancellationToken).ConfigureAwait(false);
        }, cancellationToken).ConfigureAwait(false);
    }
}
