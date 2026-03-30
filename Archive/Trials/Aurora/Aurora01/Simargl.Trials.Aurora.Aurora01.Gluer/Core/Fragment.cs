using Simargl.Frames.OldStyle;
using Simargl.Frames;
using Simargl.Frames.TestLab;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Simargl.Recording.AccelEth3T;

namespace Simargl.Trials.Aurora.Aurora01.Gluer.Core;

/// <summary>
/// Представляет фрагмент записи.
/// </summary>
public sealed class Fragment
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="displacement">
    /// Начальное смещение.
    /// </param>
    /// <param name="compression">
    /// Коэффициент сжатия.
    /// </param>
    /// <param name="times">
    /// Массив времён.
    /// </param>
    /// <param name="files">
    /// Массив файлов.
    /// </param>
    private Fragment(TimeSpan displacement, double compression, DateTime[] times, FileInfo[] files)
    {
        //  Установка значений свойств.
        Displacement = displacement;
        Compression = compression;
        Times = times;
        Files = files;

        //  Определение целевых границ по времени.
        DateTime beginTargetTime = GluerTunnings.Date.ToDateTime(default);
        DateTime endTargetTime = beginTargetTime.AddDays(1);

        //  Определение начального и конечного времени.
        DateTime beginTime = Times[0] + Displacement;
        DateTime endTime = beginTime + Times.Length * GluerTunnings.FrameStep * Compression;

        //  Нормализация.
        beginTime = new(beginTime.Year, beginTime.Month, beginTime.Day, beginTime.Hour, 0, 0);
        endTime = new(endTime.Year, endTime.Month, endTime.Day, endTime.Hour, 0, 0);

        //  Сдвиг.
        while (beginTime < Times[0] + Displacement)
        {
            beginTime = beginTime.AddMinutes(1);
        }

        //  Проверка ограничений времени.
        if (beginTime < beginTargetTime) beginTime = beginTargetTime;
        if (beginTime > endTargetTime) beginTime = endTargetTime;
        if (endTime < beginTargetTime) endTime = beginTargetTime;
        if (endTime > endTargetTime) endTime = endTargetTime;

        //  Установка времен и длительности.
        BeginTime = beginTime;
        EndTime = endTime;
        Duration = EndTime - BeginTime;
    }

    /// <summary>
    /// Возвращает начальное смещение.
    /// </summary>
    public TimeSpan Displacement { get; }

    /// <summary>
    /// Возвращает коэффициент сжатия.
    /// </summary>
    public double Compression { get; }

    /// <summary>
    /// Возвращает массив времён.
    /// </summary>
    public DateTime[] Times { get; }

    /// <summary>
    /// Возвращает массив файлов.
    /// </summary>
    public FileInfo[] Files { get; }

    /// <summary>
    /// Возвращает начальное время.
    /// </summary>
    public DateTime BeginTime { get; }

    /// <summary>
    /// Возвращает конечное время.
    /// </summary>
    public DateTime EndTime { get; }

    /// <summary>
    /// Возвращает длительность фрагмента.
    /// </summary>
    public TimeSpan Duration { get; }

    /// <summary>
    /// Асинхронно загружает данные из кадров.
    /// </summary>
    /// <param name="minute">
    /// Индекс кадра.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая загрузку данных.
    /// </returns>
    public async Task<Channel[]?> LoadFrameAsync(int minute, CancellationToken cancellationToken)
    {
        //  Определение времён кадра.
        DateTime beginTime = GluerTunnings.Date.ToDateTime(default).AddMinutes(minute);
        DateTime endTime = beginTime.AddMinutes(1);

        //  Определение шага.
        TimeSpan step = GluerTunnings.FrameStep * Compression;

        //  Определение начального времени.
        DateTime time0 = Times[0];

        //  Создание карты фрагментов.
        SortedDictionary<DateTime, Frame> map = [];

        //  Перебор файлов.
        for (int i = 0; i < Times.Length; i++)
        {
            //  Определение времён файла.
            DateTime beginFileTime = time0 + Displacement + i * step;
            DateTime endFileTime = beginFileTime + step;

            //  Проверка вхождения данных в файл.
            if ((beginFileTime <= beginTime && beginTime < endFileTime) ||
                (beginFileTime < endTime && endTime <= endFileTime) ||
                (beginTime <= beginFileTime && endFileTime < endTime))
            {
                //  Проверка токена отмены.
                await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

                //  Загрузка кадра.
                Frame frame = new(Files[i].FullName);

                //  Добавление в карту.
                map.Add(beginFileTime, frame);
            }
        }

        //  Получение массива сырых кадров.
        Frame[] rawFrames = [.. map.Values];

        //  Коррекстировка начального времени.
        time0 = map.Keys.First();

        //  Создание массивов для данных.
        double[][] values = new double[GluerTunnings.RawChannels.Length][];

        //  Создание списка каналов.
        List<Channel> channels = [];

        //  step -> 60 * 1200 значений за step
        //  index = k * i + b
        //  index -> time0 + index * step / (60 * 1200);
        //  i -> beginTime + i / 1200
        //  beginTime + i / 1200 = time0 + index * step / (60 * 1200)
        //  index = (beginTime - time0 + i / 1200) * (60 * 1200) / step;

        //  Перебор каналов.
        for (int channelIndex = 0; channelIndex < values.Length; channelIndex++)
        {
            //  Создание массива данных.
            values[channelIndex] = new double[60 * 1200 * rawFrames.Length];

            //  Перебор кадров.
            for (int frameIndex = 0; frameIndex < rawFrames.Length; frameIndex++)
            {
                //  Получение канала.
                Channel rawChannel = rawFrames[frameIndex].Channels[channelIndex];

                //  проверка заголовка канала.
                if (rawChannel.Header is TestLabChannelHeader header)
                {
                    //  Смещение значений.
                    rawChannel.Move(header.Offset);
                }

                //  Копирование данных.
                Array.Copy(rawChannel.Items, 0, values[channelIndex], 60 * 1200 * frameIndex, 60 * 1200);
            }

            //  Создание целевого канала.
            Channel channel = new(
                GluerTunnings.RawChannels[channelIndex].Name,
                GluerTunnings.RawChannels[channelIndex].Unit,
                1200,
                500,
                60 * 1200);

            //  Добавление канала в список.
            channels.Add(channel);

            //  Получение массива значений канала.
            double[] items = channel.Items;

            //  Перебор значений канала.
            for (int i = 0; i < items.Length; i++)
            {
                //  Определение целевого индекса.
                int index = (int)Math.Round(((beginTime - time0).TotalSeconds + i / 1200.0) * (60 * 1200) / step.TotalSeconds);

                //  Установка значения.
                items[i] = values[channelIndex][index];
            }
        }

        //  Возврат массива каналов.
        return [.. channels];
    }


    /// <summary>
    /// Асинхронно загружает данные Adxl.
    /// </summary>
    /// <param name="minute">
    /// Индекс кадра.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая загрузку данных.
    /// </returns>
    public async Task<Channel[]?> LoadAdxlAsync(int minute, CancellationToken cancellationToken)
    {
        //  Определение времён кадра.
        DateTime beginTime = GluerTunnings.Date.ToDateTime(default).AddMinutes(minute);
        DateTime endTime = beginTime.AddMinutes(1);

        //  Определение шага.
        TimeSpan step = GluerTunnings.AdxlStep * Compression;

        //  Определение начального времени.
        DateTime time0 = Times[0];

        //  Создание списка каналов.
        List<Channel> channels = [];

        //  Создание карты фрагментов.
        SortedDictionary<DateTime, Frame> map = [];

        //  Перебор файлов.
        for (int i = 0; i < Times.Length; i++)
        {
            //  Определение времён файла.
            DateTime beginFileTime = time0 + Displacement + i * step;
            DateTime endFileTime = beginFileTime + step;

            //  Проверка вхождения данных в файл.
            if ((beginFileTime <= beginTime && beginTime < endFileTime) ||
                (beginFileTime < endTime && endTime <= endFileTime) ||
                (beginTime <= beginFileTime && endFileTime < endTime))
            {
                //  Проверка токена отмены.
                await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

                //  Получение данных.
                byte[] buffer = await File.ReadAllBytesAsync(Files[i].FullName, cancellationToken).ConfigureAwait(false);

                //  Проверка размера буфера.
                if (buffer.Length != 400 * 636) throw new InvalidDataException("Некорректный размер файла.");

                //  Создание средства чтения данных.
                using MemoryStream stream = new(buffer);

                //  Список пакетов.
                List<AccelEth3TDataPackage> packages = [];

                //  Разбор потока.
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        //  Чтение пакета.
                        packages.Add(await AccelEth3TDataPackage.LoadAsync(stream, cancellationToken).ConfigureAwait(false));
                    }
                    catch (EndOfStreamException)
                    {
                        //  Заверешение разбора.
                        break;
                    }
                }

                //  Определение длины канала.
                int length = 50 * packages.Count;

                //  Создание каналов.
                Channel[] rawChannels =
                [
                    new(string.Empty, string.Empty, 2000, 500, length),
                    new(string.Empty, string.Empty, 2000, 500, length),
                    new(string.Empty, string.Empty, 2000, 500, length),
                ];

                //  Перебор пакетов.
                for (int packgeIndex = 0; packgeIndex < packages.Count; packgeIndex++)
                {
                    //  Получение пакета.
                    AccelEth3TDataPackage package = packages[packgeIndex];

                    //  Перебор каналов.
                    for (int channelIndex = 0; channelIndex < 3; channelIndex++)
                    {
                        //  Получение канала.
                        Channel channel = rawChannels[channelIndex];

                        //  Получение сигнала.
                        AccelEth3TSignal signal = package.Signals[channelIndex];

                        //  Перебор значений.
                        for (int valueIndex = 0; valueIndex < 50; valueIndex++)
                        {
                            //  Установка значения канала.
                            channel[50 * packgeIndex + valueIndex] = signal[valueIndex];
                        }
                    }
                }

                //  Создание кадра.
                Frame frame = new();

                //  Добавление каналов.
                frame.Channels.AddRange(rawChannels);

                //  Добавление в карту.
                map.Add(beginFileTime, frame);
            }
        }

        //  Получение массива сырых кадров.
        Frame[] rawFrames = [.. map.Values];

        //  Корректировка начального времени.
        time0 = map.Keys.First();

        //  Создание массивов для данных.
        double[][] values = new double[3][];

        //  Перебор каналов.
        for (int channelIndex = 0; channelIndex < values.Length; channelIndex++)
        {
            //  Создание массива данных.
            values[channelIndex] = new double[10 * 2000 * rawFrames.Length];

            //  Перебор кадров.
            for (int frameIndex = 0; frameIndex < rawFrames.Length; frameIndex++)
            {
                //  Получение канала.
                Channel rawChannel = rawFrames[frameIndex].Channels[channelIndex];

                //  Копирование данных.
                Array.Copy(rawChannel.Items, 0, values[channelIndex], 10 * 2000 * frameIndex, 10 * 2000);
            }

            //  Создание целевого канала.
            Channel channel = new(
                string.Empty,
                "g",
                2000,
                500,
                60 * 2000);

            //  Добавление канала в список.
            channels.Add(channel);

            //  Получение массива значений канала.
            double[] items = channel.Items;

            //  Перебор значений канала.
            for (int i = 0; i < items.Length; i++)
            {
                //  Определение целевого индекса.
                int index = (int)Math.Round(((beginTime - time0).TotalSeconds + i / 2000.0) * (10 * 2000) / step.TotalSeconds);

                //  Установка значения.
                items[i] = values[channelIndex][index];
            }
        }

        //  Возврат массива каналов.
        return [.. channels];
    }

    /// <summary>
    /// Асинхронно выполняет разбор записей.
    /// </summary>
    /// <param name="map">
    /// Карта файлов.
    /// </param>
    /// <param name="step">
    /// Шаг записи.
    /// </param>
    /// <param name="minCount">
    /// Минимально допустимое количество файлов во фрагменте.
    /// </param>
    /// <param name="minDisplacement">
    /// Минимально допустимое смещение.
    /// </param>
    /// <param name="maxDisplacement">
    /// Максимально допустимое смещение.
    /// </param>
    /// <param name="minCompression">
    /// Минимально допустимое сжатие.
    /// </param>
    /// <param name="maxCompression">
    /// Максимально допустимое сжатие.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая разбор записей.
    /// </returns>
    public static async Task<Fragment[]> ParseAsync(
        SortedDictionary<DateTime, FileInfo> map,
        TimeSpan step, int minCount,
        TimeSpan minDisplacement, TimeSpan maxDisplacement,
        double minCompression, double maxCompression,
        CancellationToken cancellationToken)
    {
        //  Проверка количества файлов.
        if (map.Count == 0) return [];

        //  Список фрагментов.
        List<Fragment> fragments = [];

        //  Получение массива времён.
        DateTime[] times = [.. map.Keys];

        //  Получение массива файлов.
        FileInfo[] files = [.. map.Values];

        //  Получение массива значений времён.
        double[] timeValues = [.. times.Select(x => (x - times[0]).TotalSeconds)];

        //  Начальное смещение.
        int offset = 0;

        //  Преобразование основных параметров.
        double stepValue = step.TotalSeconds;
        double minDisplacementValue = minDisplacement.TotalSeconds;
        double maxDisplacementValue = maxDisplacement.TotalSeconds;

        //  Основной цикл разбора.
        while (offset < timeValues.Length)
        {
            //  Выполнение шага разбора.
            (int count, double displacement, double compression) = await ParseCoreAsync(
                timeValues, offset, stepValue, minDisplacementValue, maxDisplacementValue,
                minCompression, maxCompression, cancellationToken).ConfigureAwait(false);

            //  Проверка количества файлов.
            if (count >= minCount)
            {
                //  Создание массивов данных.
                DateTime[] fragmentTimes = new DateTime[count];
                FileInfo[] fragmentFiles = new FileInfo[count];

                //  Копирование данных.
                Array.Copy(times, offset, fragmentTimes, 0, count);
                Array.Copy(files, offset, fragmentFiles, 0, count);

                //  Создание нового фрагмента.
                Fragment fragment = new(TimeSpan.FromSeconds(displacement), compression, fragmentTimes, fragmentFiles);

                //  Проверка длительности фрагмента.
                if (fragment.Duration > TimeSpan.Zero)
                {
                    //  Добавление нового фрагмента.
                    fragments.Add(new(TimeSpan.FromSeconds(displacement), compression, fragmentTimes, fragmentFiles));
                }
            }

            //  Корректировка смещения.
            offset += count;
        }

        //  Возврат фрагментов.
        return [.. fragments];
    }

    /// <summary>
    /// Асинхронно выполняет разбор.
    /// </summary>
    /// <param name="times">
    /// Область времён.
    /// </param>
    /// <param name="offset">
    /// Смещение в области времён.
    /// </param>
    /// <param name="step">
    /// Шаг по времени.
    /// </param>
    /// <param name="minDisplacement">
    /// Минимально допустимое смещение.
    /// </param>
    /// <param name="maxDisplacement">
    /// Максимально допустимое смещение.
    /// </param>
    /// <param name="minCompression">
    /// Минимально допустимое сжатие.
    /// </param>
    /// <param name="maxCompression">
    /// Максимально допустимое сжатие.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая разбор.
    /// </returns>
    private static async Task<(int Count, double Displacement, double Compression)> ParseCoreAsync(
        double[] times, int offset, double step,
        double minDisplacement, double maxDisplacement,
        double minCompression, double maxCompression,
        CancellationToken cancellationToken)
    {
        //  Проверка количества времён.
        if (times.Length == offset) return (0, 0, 0);

        //  Определение общих параметров.
        double time0 = times[offset];
        double beta = 1 / step;
        double A = time0;

        //  Создание вспомогательных массивов.
        double[] displacements = new double[times.Length - offset];
        double[] compressions = new double[times.Length - offset];

        //  Установка начальных значений.
        displacements[0] = 0;
        compressions[0] = 1;

        //  Расчёт значений.
        for (int i = 1; i < times.Length - offset; i++)
        {
            //  Текущее последнее значение.
            double time = times[offset + i];

            //  Корректировка суммы.
            A += time;

            //  Определение целевых значений.
            displacements[i] = A / (i + 1) - 0.5 * (time + time0);
            compressions[i] = beta * (time - time0) / i;
        }

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Поиск лучшего значения.
        for (int count = displacements.Length; count > 0; count--)
        {
            //  Получение значений.
            double displacement = displacements[count - 1];
            double compression = compressions[count - 1];

            //  Проверка значений.
            if (minDisplacement <= displacement && displacement <= maxDisplacement &&
                minCompression <= compression && compression <= maxCompression)
            {
                //  Найден наибольший фрагмент, удовлетворяющий условиям.
                return (count, displacement, compression);
            }
        }

        //  Возврат фаргмента из одной записи.
        return (1, 0, 1);
    }
}
