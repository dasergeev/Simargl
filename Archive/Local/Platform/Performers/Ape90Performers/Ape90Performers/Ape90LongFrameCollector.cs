using Apeiron.Frames;

namespace Apeiron.Platform.Performers.Ape90Performers;

/// <summary>
/// Представляет исполнителя, выполняющего сборку длинных кадров регистрации.
/// </summary>
public sealed class Ape90LongFrameCollector :
    Performer
{
    /// <summary>
    /// Постоянная, определюящая путь к кадрам.
    /// </summary>
    public const string FramesRootPath = @"\\NtoData\NTO_Files\Records\2021\0002\000 Long Frames";

    /// <summary>
    /// Поле для хранения начальной даты.
    /// </summary>
    private readonly DateTime _BeginTime = new(2021, 11, 9);

    /// <summary>
    /// Поле для хранения конечной даты.
    /// </summary>
    private readonly DateTime _EndTime = new(2022, 02, 10);

    /// <summary>
    /// Постоянная, определяющая шаг по дням кадров.
    /// </summary>
    private const int _StepDays = 3;

    /// <summary>
    /// Поятоянная, определяющая длину канала.
    /// </summary>
    private const int _Length = _StepDays * 24 * 3600;

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
    public Ape90LongFrameCollector(Journal journal) :
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
        //  Поддержка выполнения.
        await KeepAsync(async cancellationToken =>
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Список времён.
            List<DateTime> times = new();

            //  Формирование списка времён.
            for (DateTime time = _BeginTime; time < _EndTime; time = time.AddDays(_StepDays))
            {
                //  Добавление в список.
                times.Add(time);
            }

            //  Создание генератора случайных чисел.
            Random random = new(unchecked((int)(DateTime.Now.Ticks % int.MaxValue)));

            //  Изменение порядка списка времён.
            for (int i = 0; i < times.Count; i++)
            {
                //  Получение нового индекса.
                int index = random.Next() % times.Count;

                //  Перестановка элементов.
                (times[i], times[index]) = (times[index], times[i]);
            }

            //  Выполнение работы по временным интервалам.
            await Parallel.ForEachAsync(
                times,
                new ParallelOptions
                {
                    CancellationToken = cancellationToken,
                },
                WorkWithTimeAsync).ConfigureAwait(false);
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет работу с временным интервалом.
    /// </summary>
    /// <param name="beginTime">
    /// Начало временного интервала.
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
    private async ValueTask WorkWithTimeAsync(DateTime beginTime, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Безопасный вызов.
        await SafeCallAsync(async cancellationToken =>
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Определение окончания временного интервала.
            DateTime endTime = beginTime.AddDays(_StepDays);

            //  Создание контекста сеанса работы с базой данных.
            using Ape90DatabaseContext context = Ape90DatabaseContextProvider.CreateContext();

            //  Запрос всех незагруженных фрагментов временного интервала.
            int allFragmentsCount = await context.Fragments
                .Where(fragment => !fragment.IsLoadedIntoLongFrame &&
                    beginTime <= fragment.Time && fragment.Time < endTime)
                .CountAsync(cancellationToken).ConfigureAwait(false);

            //  Проверка количества фрагментов.
            if (allFragmentsCount == 0)
            {
                //  Завершение работы с фрагментом.
                return;
            }

            //  Запрос всех фрагментов временного интервала.
            Fragment[] allFragments = await context.Fragments
                .Where(fragment => beginTime <= fragment.Time && fragment.Time < endTime)
                .ToArrayAsync(cancellationToken).ConfigureAwait(false);

            //  Собираемый кадр.
            Frame frame = new();

            //  Создание общих каналов.
            Channel isDataValid = new("IsDataValid", "bool", 1, 1, _Length);
            Channel isGpsValid = new("IsGpsValid", "bool", 1, 1, _Length);
            Channel speed = new("V_GPS", "kmph", 1, 1, _Length);
            Channel latitude = new("Lat_GPS", "°", 1, 1, _Length);
            Channel longitude = new("Lon_GPS", "°", 1, 1, _Length);
            Channel altitude = new("A_GPS", "m", 1, 1, _Length);

            //  Добавление общих каналов в кадр.
            frame.Channels.AddRange(new Channel[] { isDataValid , isGpsValid, speed, latitude, longitude, altitude });

            //  Перебор каналов.
            foreach (ChannelInfo channelInfo in _ChannelInfos)
            {
                //  Получение имени канала.
                string name = channelInfo.Name;

                //  Получение размерности канала.
                string unit = channelInfo.Unit;

                //  Создание каналов.
                Channel average = new(name, unit, 1, 1, _Length);
                Channel speedZero = new(name + "VZero", unit, 1, 1, _Length);
                Channel min = new(name + "Min", unit, 1, 1, _Length);
                Channel max = new(name + "Max", unit, 1, 1, _Length);
                Channel deviation = new(name + "Dev", unit, 1, 1, _Length);

                //  Добавление каналов в кадр.
                frame.Channels.AddRange(new Channel[] { average, speedZero, min, max, deviation });

                //  Получение фрагментов.
                Fragment[] fragments = allFragments
                    .Where(fragment => fragment.ChannelInfoId == channelInfo.Id)
                    .ToArray();

                //  Перебор фрагментов.
                foreach (Fragment fragment in fragments)
                {
                    //  Определение индекса фрагмента.
                    int index = (int)(fragment.Time - beginTime).TotalSeconds;

                    //  Установка значений.
                    isDataValid[index] *= fragment.IsDataValid ? 1 : 0;
                    isGpsValid[index] *= fragment.IsGpsValid ? 1 : 0;
                    speed[index] = fragment.Speed;
                    latitude[index] = fragment.Latitude;
                    longitude[index] = fragment.Longitude;
                    altitude[index] = fragment.Altitude;

                    average[index] = fragment.Average;
                    speedZero[index] = fragment.SpeedZero;
                    min[index] = fragment.Min;
                    max[index] = fragment.Max;
                    deviation[index] = fragment.Deviation;
                }
            }

            //  Определение номера кадра.
            int frameNumber = (int)((beginTime - _BeginTime).TotalDays / _StepDays) + 1;

            //  Определение пути к кадру.
            string path = PathBuilder.Combine(FramesRootPath, $"Vp0_0 {beginTime:yy-MM-dd}.{frameNumber:0000}");

            //  Сохранение кадра.
            frame.Save(path, StorageFormat.TestLab);

            //  Начало транзакции.
            using var transaction = context.Database.BeginTransaction();

            //  Перебор всех фрагментов.
            foreach (Fragment fragment in allFragments)
            {
                //  Установка флага загрузки фрагмента в длинный кадр.
                fragment.IsLoadedIntoLongFrame = true;
            }

            //  Сохранение изменений в базу данных.
            context.SaveChanges();

            //  Фиксирование изменений.
            transaction.Commit();

            //  Вывод в журнал.
            await Journal.LogInformationAsync(
                $"Собран длинный кадр {path}",
                cancellationToken).ConfigureAwait(false);
        }, cancellationToken).ConfigureAwait(false);
    }
}
