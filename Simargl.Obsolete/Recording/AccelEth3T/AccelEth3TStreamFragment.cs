using System.IO;

namespace Simargl.Recording.AccelEth3T;

/// <summary>
/// Представляет фрагмент потока данных с датчика AccelEth3T.
/// </summary>
public sealed class AccelEth3TStreamFragment
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="packages">
    /// Коллекция информации о пакетах.
    /// </param>
    /// <param name="time">
    /// Время начала регистрации данных.
    /// </param>
    /// <param name="duration">
    /// Длительность регистрации данных.
    /// </param>
    private AccelEth3TStreamFragment(
        AccelEth3TDataPackageInfoCollection packages, DateTime time, TimeSpan duration)
    {
        //  Установка значений свойств.
        Packages = packages;
        Time = time;
        Duration = duration;
    }

    /// <summary>
    /// Возвращает коллекцию информации о пакетах.
    /// </summary>
    public AccelEth3TDataPackageInfoCollection Packages { get; }

    /// <summary>
    /// Возвращает время начала регистрации данных.
    /// </summary>
    public DateTime Time { get; }

    /// <summary>
    /// Возвращает длительность регистрации данных.
    /// </summary>
    public TimeSpan Duration { get; }

    /// <summary>
    /// Загружает данные из файла.
    /// </summary>
    /// <param name="path">
    /// Путь к файлу.
    /// </param>
    /// <param name="period">
    /// Период поступления пакетов.
    /// </param>
    /// <param name="endTime">
    /// Время окончания записи.
    /// </param>
    /// <returns>
    /// Фрагмент потока данных с датчика AccelEth3T.
    /// </returns>
    public static AccelEth3TStreamFragment Load(string path, TimeSpan period, DateTime endTime)
    {
        //  Проверка пути.
        IsNotNull(path);

        //  Чтение данных из файла.
        byte[] buffer = File.ReadAllBytes(path);

        //  Загрузка данных из буфера.
        return Load(buffer, endTime, period);
    }

    /// <summary>
    /// Асинхронно загружает данные из файла.
    /// </summary>
    /// <param name="path">
    /// Путь к файлу.
    /// </param>
    /// <param name="period">
    /// Период поступления пакетов.
    /// </param>
    /// <param name="endTime">
    /// Время окончания записи.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, загружающая данные из файла.
    /// </returns>
    public static async Task<AccelEth3TStreamFragment> LoadAsync(
        string path, TimeSpan period, DateTime endTime,
        CancellationToken cancellationToken)
    {
        //  Проверка пути.
        IsNotNull(path);

        //  Чтение данных из файла.
        byte[] buffer = await File.ReadAllBytesAsync(path, cancellationToken).ConfigureAwait(false);

        //  Загрузка данных из буфера.
        return Load(buffer, endTime, period);
    }

    /// <summary>
    /// Объединяет фрагменты.
    /// </summary>
    /// <param name="fragments">
    /// Коллекция фрагментов.
    /// </param>
    /// <param name="maxGap">
    /// Максимальный разрыв между фрагментами.
    /// </param>
    /// <returns>
    /// Объединённый фрагмент.
    /// </returns>
    public static AccelEth3TStreamFragment Join(
        IEnumerable<AccelEth3TStreamFragment> fragments, TimeSpan maxGap)
    {
        //  Проверка коллекции фрагментов.
        IsNotEmpty(fragments);

        //  Сортировка фрагментов.
        AccelEth3TStreamFragment[] sources = [.. fragments.OrderBy(x => x.Time)];

        //  Проверка количества фрагментов.
        if (sources.Length == 1)
        {
            //  Возврат копии фрагмента.
            return new(sources[0].Packages, sources[0].Time, sources[0].Duration);
        }

        //  Проверка разрывов.
        for (int i = 1; i < sources.Length; i++)
        {
            //  Определение разрыва.
            TimeSpan gap = sources[i].Time - sources[i - 1].Time - sources[i - 1].Duration;

            //  Проверка разрыва.
            if (Math.Abs(gap.TotalSeconds) > Math.Abs(maxGap.TotalSeconds))
            {
                //  Не удалось объединить фаргменты.
                throw new InvalidOperationException(
                    $"Разрыв между фрагментами больше допустимого: {Math.Abs(gap.TotalMilliseconds)} мс.");
            }
        }

        //  Определение начала и конца записи фрагментов.
        DateTime beginTime = sources[0].Time;
        DateTime endTime = sources[^1].Time;

        //  Общая длительность.
        TimeSpan duration = TimeSpan.Zero;

        //  Перебор фрагментов.
        foreach (AccelEth3TStreamFragment fragment in sources)
        {
            //  Корректировка длительности.
            duration += fragment.Duration;
        }

        //  Определение поправки по времени.
        TimeSpan amendment = 0.5 * (endTime - beginTime - duration);

        //  Корректировка времени.
        beginTime += amendment;

        //  Текущее время.
        DateTime time = beginTime;

        //  Список информации о пакетах.
        List<AccelEth3TDataPackageInfo> infos = [];

        //  Перебор фрагментов.
        foreach (AccelEth3TStreamFragment fragment in sources)
        {
            //  Перебор информации.
            foreach (AccelEth3TDataPackageInfo package in fragment.Packages)
            {
                //  Добавление информации.
                infos.Add(new(time, package.Duration, package.Package));

                //  Корректировка времени.
                time += package.Duration;
            }
        }

        //  Возврат фрагмента.
        return new(new([..infos]), beginTime, duration);
    }

    /// <summary>
    /// Возвращает данные по оси Ox.
    /// </summary>
    /// <param name="beginTime">
    /// Время начала данных.
    /// </param>
    /// <param name="duration">
    /// Длительность данных.
    /// </param>
    /// <returns>
    /// Данные.
    /// </returns>
    public double[] GetXData(DateTime beginTime, TimeSpan duration)
    {
        //  Возврат данных.
        return GetData(beginTime, duration, x => x.XSignal);
    }

    /// <summary>
    /// Возвращает данные по оси Oy.
    /// </summary>
    /// <param name="beginTime">
    /// Время начала данных.
    /// </param>
    /// <param name="duration">
    /// Длительность данных.
    /// </param>
    /// <returns>
    /// Данные.
    /// </returns>
    public double[] GetYData(DateTime beginTime, TimeSpan duration)
    {
        //  Возврат данных.
        return GetData(beginTime, duration, x => x.YSignal);
    }

    /// <summary>
    /// Возвращает данные по оси Oz.
    /// </summary>
    /// <param name="beginTime">
    /// Время начала данных.
    /// </param>
    /// <param name="duration">
    /// Длительность данных.
    /// </param>
    /// <returns>
    /// Данные.
    /// </returns>
    public double[] GetZData(DateTime beginTime, TimeSpan duration)
    {
        //  Возврат данных.
        return GetData(beginTime, duration, x => x.ZSignal);
    }

    /// <summary>
    /// Возвращает асинхронные данные.
    /// </summary>
    /// <param name="beginTime">
    /// Время начала данных.
    /// </param>
    /// <param name="duration">
    /// Длительность данных.
    /// </param>
    /// <param name="index">
    /// Индекс асинхронных данных.
    /// </param>
    /// <returns>
    /// Данные.
    /// </returns>
    public double[] GetAsyncData(DateTime beginTime, TimeSpan duration, int index)
    {
        //  Определение конечного времени.
        DateTime endTime = beginTime + duration;

        //  Проверка длительности.
        if (beginTime < Time || endTime > Time + Duration)
            throw new InvalidOperationException("Фрагмент не содержит запрашиваемые данные.");

        //  Создание списка данных.
        List<double> data = [];

        //  Перебор пакетов.
        foreach (AccelEth3TDataPackageInfo info in Packages)
        {
            //  Проверка времени завершения.
            if (info.Time > endTime)
            {
                //  Завершение сбора данных.
                break;
            }

            //  Проверка времени начала.
            if (info.Time + info.Duration < beginTime)
            {
                //  Переход к следующему пакету.
                continue;
            }

            //  Добавление значения.
            data.Add(info.Package.AsyncValues[index]);
        }

        //  Возврат данных.
        return [.. data];
    }

    /// <summary>
    /// Возвращает данные.
    /// </summary>
    /// <param name="beginTime">
    /// Время начала данных.
    /// </param>
    /// <param name="duration">
    /// Длительность данных.
    /// </param>
    /// <param name="getSignal">
    /// Функция, возвращающая целевой сигнал.
    /// </param>
    /// <returns>
    /// Данные.
    /// </returns>
    private double[] GetData(DateTime beginTime, TimeSpan duration,
        Func<AccelEth3TSignalCollection, AccelEth3TSignal> getSignal)
    {
        //  Определение конечного времени.
        DateTime endTime = beginTime + duration;

        //  Проверка длительности.
        if (beginTime < Time || endTime > Time + Duration)
            throw new InvalidOperationException("Фрагмент не содержит запрашиваемые данные.");

        //  Создание списка данных.
        List<double> data = [];

        //  Перебор пакетов.
        foreach (AccelEth3TDataPackageInfo info in Packages)
        {
            //  Получение сигнала.
            AccelEth3TSignal signal = getSignal(info.Package.Signals);

            //  Определение шага по времени.
            TimeSpan delta = info.Duration / signal.Length;

            //  Проверка времени завершения.
            if (info.Time > endTime)
            {
                //  Завершение сбора данных.
                break;
            }

            //  Проверка времени начала.
            if (info.Time + info.Duration < beginTime)
            {
                //  Переход к следующему пакету.
                continue;
            }

            //  Перебор значений.
            for (int i = 0; i < signal.Length; i++)
            {
                //  Определение времени.
                DateTime time = info.Time + i * delta;

                //  Проверка времени.
                if (beginTime <= time && time < endTime)
                {
                    //  Добавление значения.
                    data.Add(signal[i]);
                }
            }
        }

        //  Возврат данных.
        return [.. data];
    }

    /// <summary>
    /// Загружает данные из буфера.
    /// </summary>
    /// <param name="buffer">
    /// Буфер, содержащий данные.
    /// </param>
    /// <param name="endTime">
    /// Время окончания записи.
    /// </param>
    /// <param name="period">
    /// Период поступления пакетов.
    /// </param>
    /// <returns>
    /// Фрагмент потока данных с датчика AccelEth3T.
    /// </returns>
    private static AccelEth3TStreamFragment Load(byte[] buffer, DateTime endTime, TimeSpan period)
    {
        //  Загрузка данных.
        List<AccelEth3TDataPackage> packages = Load(buffer);

        //  Определение количества пакетов.
        int count = packages.Count;

        //  Определение длительности регистрации данных.
        TimeSpan duration = period * count;

        //  Определение времени начала регистрации данных.
        DateTime beginTime = endTime - duration;

        //  Массив информации о пакетах.
        AccelEth3TDataPackageInfo[] infos = new AccelEth3TDataPackageInfo[packages.Count];

        //  Перебор пакетов.
        for (int i = 0; i < count; i++)
        {
            //  Создание информации о пакете.
            infos[i] = new(beginTime - period * i, period, packages[i]);
        }

        //  Возврат фрагмента.
        return new(new(infos), beginTime, duration);
    }

    /// <summary>
    /// Загружает данные из буфера.
    /// </summary>
    /// <param name="buffer">
    /// Буфер, содержащий данные.
    /// </param>
    /// <returns>
    /// Спиоск пакетов данных датчика AccelEth3T.
    /// </returns>
    private static List<AccelEth3TDataPackage> Load(byte[] buffer)
    {
        //  Текущее положения в буфере.
        int position = 0;

        ////  Поиск сигнатуры.
        //while (position + 8 < buffer.Length)
        //{
        //    //  Проверка сигнатуры.
        //    if (BitConverter.ToInt64(buffer, position) == AccelEth3TDataPackage.Signature)
        //    {
        //        //  Завершение поиска.
        //        break;
        //    }

        //    //  Смещение положения в буфере.
        //    position++;
        //}

        //  Создание потока для чтения данных из буфера.
        using MemoryStream stream = new(buffer, position, buffer.Length - position, false);

        //  Создание списка пакетов.
        List<AccelEth3TDataPackage> packages = [];

        //  Основной цикл чтения.
        while (stream.Position < stream.Length)
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Чтение пакета.
                AccelEth3TDataPackage package = AccelEth3TDataPackage.Load(stream);

                //  Добавление пакета в список.
                packages.Add(package);
            }
            catch
            {
                ////  Проверка достижения конца потока.
                //if (stream.Position < stream.Length)
                {
                    //  Повторный выброс исключения.
                    throw;
                }
            }
        }

        //  Возврат списка пакетов.
        return packages;
    }
}
