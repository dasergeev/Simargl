using MathNet.Numerics.Interpolation;
using MathNet.Numerics.LinearAlgebra;
using Simargl.Border.Geometry;
using Simargl.Border.Processing.Core;
using Simargl.Border.Processing.Support;
using System.IO;

namespace Simargl.Border.Processing;

/// <summary>
/// Представляет устройство предобработки.
/// </summary>
public sealed class Preprocessor :
    ProcessorUnit
{
    /// <summary>
    /// Поле для хранения длины канала.
    /// </summary>
    private readonly int _ChannelLength;

    /// <summary>
    /// Поле для хранения исходного кадра.
    /// </summary>
    private readonly LinkedFrame _SourceFrame;

    /// <summary>
    /// Поле для хранения целевого кадра.
    /// </summary>
    private readonly LinkedFrame _TargetFrame;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="processor">
    /// Устройство обработки.
    /// </param>
    /// <param name="sourcePath">
    /// Путь к исходному кадру.
    /// </param>
    /// <param name="targetPath">
    /// Путь к целевому кадру.
    /// </param>
    public Preprocessor(Processor processor, string sourcePath, string targetPath) :
        base(processor)
    {
        //  Блок перехвата всех исключений.
        try
        {
            //  Удаление каталога.
            Directory.Delete(targetPath, true);
        }
        catch { }

        //  Создание каталога.
        Directory.CreateDirectory(targetPath);

        //  Карта исходного кадра.
        Dictionary<string, int> sourceMap = Enumerable
            .Range(0, processor.SectionGroups.Sources.Count)
            .Select(x => new
            {
                Index = x,
                processor.SectionGroups.Sources[x].Name
            })
            .ToDictionary(x => x.Name, x => x.Index);

        //  Создание исходного кадра.
        _SourceFrame = new(sourcePath, x => sourceMap[x], -1);

        //  Определение длины канала.
        _ChannelLength = _SourceFrame[processor.SectionGroups.Sources[0].Name].Items.Length;

        //  Создание целевого кадра.
        _TargetFrame = new(targetPath, x => BasisConstants.TargetNames[x], _ChannelLength);
    }

    /// <summary>
    /// Поле для хранения списка имён плохих каналов.
    /// </summary>
    private readonly List<string> _BadChannelNames = [];

    /// <summary>
    /// Возвращает карту нажимов.
    /// </summary>
    public PressureMap PressureMap { get; private set; } = null!;

    /// <summary>
    /// Возвращает коллекцию осей.
    /// </summary>
    public AxisCollection Axes { get; private set; } = null!;

    /// <summary>
    /// Асинхронно выполняет предобработку.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая предобработку.
    /// </returns>
    public async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Копирование каналов.
        await CopyAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка каналов.
        await ValidationAsync(cancellationToken).ConfigureAwait(false);

        //  Определение нулевых значений.
        await ZeroAsync(cancellationToken).ConfigureAwait(false);

        //  Построение каналов сил по методу двух сечений.
        await ContinuousBuildAsync(cancellationToken).ConfigureAwait(false);

        //  Построение счётчиков.
        await CounterBuildAsync(cancellationToken).ConfigureAwait(false);

        //  Подсчёт осей.
        await CountingAxesAsync(cancellationToken).ConfigureAwait(false);

        //  Построение осей.
        await AxesBuildAsync(cancellationToken).ConfigureAwait(false);

        //  Построение скорости.
        await SpeedBuildAsync(cancellationToken).ConfigureAwait(false);

        //  Построение статистики.
        await StatisticsBuildAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет построение статистики.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая построение статистики.
    /// </returns>
    private async Task StatisticsBuildAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение параметров.
        var preprocessor = BasisConstants.Preprocessor;

        //  Получение каналов.
        LinkedChannel speed = GetChannel("Speed");

        //  Получение значений.
        double[] speedItems = speed.Items;

        //  Перебор сечений.
        for (int section = 1; section <= preprocessor.SectionCount; section++)
        {
            //  Проверка токена отмены.
            await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

            //  Получение каналов.
            LinkedChannel left = GetContinuous(section, Rail.Left);
            LinkedChannel right = GetContinuous(section, Rail.Right);

            //  Получение значений.
            double[] leftItems = left.Items;
            double[] rightItems = right.Items;

            //  Перебор осей.
            foreach (Axis axis in Axes)
            {
                //  Перебор взаимодействий оси.
                foreach (AxisInteraction? interaction in axis.Interactions)
                {
                    //  Проверка взаимодействия.
                    if (interaction is null)
                    {
                        //  Переход к следующему взаимодействию.
                        continue;
                    }

                    //  Проверка сечения.
                    if (interaction.Section != section)
                    {
                        //  Переход к следующему взаимодействию.
                        continue;
                    }

                    //  Определение начального индекса и длины.
                    int length = (interaction.Pressure.End - interaction.Pressure.Begin) >> 2;
                    int begin = ((interaction.Pressure.Begin + interaction.Pressure.End) >> 1) - (length >> 1);

                    //  Проверка длины.
                    if (length < 3) length = 3;

                    //  Создание масивов данных.
                    double[] speedData = new double[length];
                    double[] leftData = new double[length];
                    double[] rightData = new double[length];

                    //  Загрузка данных.
                    Array.Copy(speedItems, begin, speedData, 0, length);
                    Array.Copy(leftItems, begin, leftData, 0, length);
                    Array.Copy(rightItems, begin, rightData, 0, length);

                    //  Определение начальных значений.
                    double speedSum = 0;
                    double leftSum = 0;
                    double rightSum = 0;
                    double speedSquaresSum = 0;
                    double leftSquaresSum = 0;
                    double rightSquaresSum = 0;
                    double leftMax = double.MinValue;
                    double rightMax = double.MinValue;

                    //  Перебор значений.
                    for (int i = 0; i < length; i++)
                    {
                        //  Чтение значений.
                        double speedValue = speedData[i];
                        double leftValue = leftData[i];
                        double rightValue = rightData[i];

                        //  Корректировка сумм.
                        speedSum += speedValue;
                        leftSum += leftValue;
                        rightSum += rightValue;
                        speedSquaresSum += speedValue * speedValue;
                        leftSquaresSum += leftValue * leftValue;
                        rightSquaresSum += rightValue * rightValue;

                        //  Корректировка экстремальных значений.
                        leftMax = Math.Max(leftMax, leftValue);
                        rightMax = Math.Max(rightMax, rightValue);
                    }

                    //  Расчёт средних значений.
                    double speedAverage = speedSum / length;
                    double leftAverage = leftSum / length;
                    double rightAverage = rightSum / length;

                    //  Расчёт отклонений.
                    double speedDeviation = Math.Sqrt((length * speedSquaresSum - speedSum * speedSum) / (length * (length - 1)));
                    double leftDeviation = Math.Sqrt((length * leftSquaresSum - leftSum * leftSum) / (length * (length - 1)));
                    double rightDeviation = Math.Sqrt((length * rightSquaresSum - rightSum * rightSum) / (length * (length - 1)));

                    //  Установка значений.
                    interaction.Length = length;
                    interaction.SpeedSum = speedSum;
                    interaction.SpeedSquaresSum = speedSquaresSum;
                    interaction.SpeedAverage = speedAverage;
                    interaction.SpeedDeviation = speedDeviation;

                    interaction.LeftSum = leftSum;
                    interaction.LeftSquaresSum = leftSquaresSum;
                    interaction.LeftAverage = leftAverage;
                    interaction.LeftDeviation = leftDeviation;
                    interaction.LeftMax = leftMax;

                    interaction.RightSum = rightSum;
                    interaction.RightSquaresSum = rightSquaresSum;
                    interaction.RightMax = rightMax;
                    interaction.RightAverage = rightAverage;
                    interaction.RightDeviation = rightDeviation;
                }
            }
        }
    }

    /// <summary>
    /// Асинхронно выполняет построение скорости.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая построение скорости.
    /// </returns>
    private async Task SpeedBuildAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение параметров.
        var preprocessor = BasisConstants.Preprocessor;

        //  Получение точек скорости.
        var points = Axes
            .SelectMany(x => x.Interactions)
            .Where(x => x is not null && double.IsFinite(x.Speed) && Math.Abs(x.Speed) <= preprocessor.MaxSpeed)
            .Select(x => x!)
            .Select(x => new
            {
                Index = (int)(x.Time * preprocessor.Sampling),
                x.Speed,
            })
            .Where(x => 0 < x.Index && x.Index < _ChannelLength)
            .GroupBy(x => x.Index)
            .Select(x => new
            {
                Index = x.Key,
                Speed = x.Average(x => x.Speed)
            })
            .OrderBy(x => x.Index)
            .ToArray();

        //  Получение каналов.
        LinkedChannel preSpeed = GetChannel("PreSpeed");
        LinkedChannel speed = GetChannel("Speed");

        //  Получение значений.
        double[] preSpeedItems = preSpeed.Items;
        double[] speedItems = speed.Items;

        //  Перебор точек.
        foreach (var point in points)
        {
            //  Установка значения.
            preSpeedItems[point.Index] = point.Speed;
        }

        //  Блок перехвата всех исключений.
        try
        {
            //  Определение граничных индексов.
            int begin = points[0].Index - preprocessor.TimeRange;
            int end = points[^1].Index + preprocessor.TimeRange;

            //  Нормализация индексов.
            if (begin < 0) begin = 0;
            if (end > _ChannelLength) end = _ChannelLength;

            //  Группировка данных.
            points = [.. points.GroupBy(x => x.Index / preprocessor.SpeedIndexStep)
            .Select(x => new
            {
                Index = x.Key * preprocessor.SpeedIndexStep,
                Speed = x.Average(x => x.Speed)
            })
            .OrderBy(x => x.Index)];

            //  Получение массивов точек.
            double[] x = [.. points.Select(x => x.Index / (double)preprocessor.Sampling)];
            double[] y = [.. points.Select(x => x.Speed)];

            //  Построение сплайна.
            CubicSpline spline = CubicSpline.InterpolateAkimaSorted(x, y);

            //  Перебор точек.
            for (int i = begin; i < end; i++)
            {
                speedItems[i] = spline.Interpolate(i / (double)preprocessor.Sampling);
            }
        }
        catch { }

        //  Сохранение значений.
        preSpeed.Save();
        speed.Save();
    }

    /// <summary>
    /// Асинхронно выполняет построение осей.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая построение осей.
    /// </returns>
    private async Task AxesBuildAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение параметров.
        var preprocessor = BasisConstants.Preprocessor;

        //  Создание коллекции осей.
        Axes = [];

        //  Проверка количества осей.
        if (PressureMap.AxesCount < preprocessor.MinAxesCount)
        {
            //  Завершение работы.
            return;
        }

        //  Перебор осей.
        for (int axisIndex = 0; axisIndex < PressureMap.AxesCount; axisIndex++)
        {
            //  Создание оси.
            Axis axis = new()
            {
                Index = axisIndex,
            };

            //  Перебор сечений.
            for (int sectionIndex = 1; sectionIndex <= preprocessor.SectionCount; sectionIndex++)
            {
                //  Получение коллекции нажимов.
                PressureCollection pressures = PressureMap.GetPressures(sectionIndex);

                //  Проверка количества нажимов.
                if (pressures.Count == PressureMap.AxesCount)
                {
                    //  Получение нажима.
                    Pressure pressure = pressures[axisIndex];

                    //  Добавление взаимодействия.
                    axis.Interactions[sectionIndex] = new()
                    {
                        Section = sectionIndex,
                        Pressure = pressure,
                        Position = Processor.SectionGroups[sectionIndex].Position,
                    };
                }
            }

            //  Добавление в коллекцию осей.
            Axes.Add(axis);
        }

        //  Построение осей.
        Axes.Build();
    }

    /// <summary>
    /// Асинхронно выполняет подсчёт осей.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая подсчёт осей.
    /// </returns>
    private async Task CountingAxesAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение параметров.
        var preprocessor = BasisConstants.Preprocessor;

        //  Создание карты нажимов.
        PressureMap = new(Processor);

        //  Перебор сечений.
        for (int section = 1; section <= preprocessor.SectionCount; section++)
        {
            //  Проверка токена отмены.
            await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

            //  Получение канала.
            LinkedChannel counter = GetCounter(section);

            //  Проверка канала.
            if (_BadChannelNames.Contains(counter.Name))
            {
                //  Переход к следующему сечению.
                continue;
            }

            //  Получение коллекции нажимов.
            PressureCollection pressures = PressureMap.GetPressures(section);

            //  Получение значений.
            double[] counterItems = counter.Items;

            //  Начальный и конечный индексы счётчика.
            int beginIndex = 0;

            //  Перебор значений.
            for (int i = 1; i < counterItems.Length; i++)
            {
                //  Проверка начала счётчика.
                if (counterItems[i - 1] == 0 && counterItems[i] == 1)
                {
                    //  Установка начального индекса.
                    beginIndex = i;
                }

                //  Проверка конца счётчика.
                if (counterItems[i - 1] == 1 && counterItems[i] == 0)
                {
                    //  Создание нажима.
                    Pressure pressure = new() { Begin = beginIndex, End = i, };

                    //  Добавление нажима.
                    pressures.Add(pressure);
                }
            }
        }

        //  Выполнение анализа карты нажимов.
        await PressureMap.AnalysisAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет построение счётчиков.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая построение счётчиков.
    /// </returns>
    private async Task CounterBuildAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение параметров.
        var preprocessor = BasisConstants.Preprocessor;

        //  Перебор сечений.
        for (int section = 1; section <= preprocessor.SectionCount; section++)
        {
            //  Проверка токена отмены.
            await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

            //  Получение каналов.
            LinkedChannel left = GetContinuous(section, Rail.Left);
            LinkedChannel right = GetContinuous(section, Rail.Right);
            LinkedChannel counter = GetCounter(section);

            //  Проверка каналов.
            if (_BadChannelNames.Contains(left.Name) ||
                _BadChannelNames.Contains(right.Name))
            {
                //  Добавление канала в список.
                _BadChannelNames.Add(counter.Name);
            }

            //  Получение значений.
            double[] leftItems = left.Items;
            double[] rightItems = right.Items;
            double[] counterItems = counter.Items;

            //  Перебор блоков.
            for (int block = 0; block * preprocessor.BlockSize < _ChannelLength; block++)
            {
                //  Начальный индекс.
                int beginIndex = block * preprocessor.BlockSize;

                //  Значение.
                double value = 0;

                //  Перебор значений.
                for (int i = 0; i < preprocessor.BlockSize; i++)
                {
                    //  Корректировка значения.
                    value += leftItems[beginIndex + i] + rightItems[beginIndex + i];
                }

                //  Усреднение значения.
                value /= preprocessor.BlockSize;

                //  Нормализация значения.
                value = value > preprocessor.CounterThreshold ? 1 : 0;

                //  Перебор значений.
                for (int i = 0; i < preprocessor.BlockSize; i++)
                {
                    //  Установка значения.
                    counterItems[beginIndex + i] = value;
                }
            }

            //  Сохранение данных.
            counter.Save();
        }
    }

    /// <summary>
    /// Асинхронно выполняет построение каналов сил по методу двух сечений.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая построение каналов сил по методу двух сечений.
    /// </returns>
    private async Task ContinuousBuildAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение параметров.
        var preprocessor = BasisConstants.Preprocessor;

        //  Перебор сечений.
        for (int section = 1; section <= preprocessor.SectionCount; section++)
        {
            //  Перебор сторон.
            foreach (Rail rail in new Rail[] { Rail.Right, Rail.Left })
            {
                //  Определение метки рельса.
                string railLabel = rail == Rail.Left ? "L" : "R";

                //  Получение каналов.
                LinkedChannel external = GetSignal(section, rail, Side.External, 0);
                LinkedChannel @internal = GetSignal(section, rail, Side.Internal, 0); ;
                LinkedChannel continuous = GetContinuous(section, rail);

                //  Проверка каналов.
                if (_BadChannelNames.Contains(external.Name) ||
                    _BadChannelNames.Contains(@internal.Name))
                {
                    //  Добавление канала в список.
                    _BadChannelNames.Add(continuous.Name);
                }

                //  Получение значений.
                double[] externalItems = external.Items;
                double[] internalItems = @internal.Items;
                double[] continuousItems = continuous.Items;

                //  Получение масштаба.
                double scale = Processor.Scheme.Modules.First(x => x.Rail == rail && x.Section == section).Scale;

                //  Перебор значений.
                for (int i = 0; i < continuousItems.Length; i++)
                {
                    //  Установка значения.
                    continuousItems[i] = scale * (externalItems[i] - internalItems[i]);
                }

                //  Сохранение данных.
                continuous.Save();
            }
        }
    }

    /// <summary>
    /// Асинхронно определяет нулевые значения.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, определяющая нулевые значения.
    /// </returns>
    private async Task ZeroAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение параметров.
        var preprocessor = BasisConstants.Preprocessor;

        //  Определение конечного индекса.
        int endIndex = (int)(preprocessor.Sampling * preprocessor.Zero.TotalSeconds);

        //  Перебор имён каналов.
        foreach (string name in Processor.SectionGroups.Sources.Select(x => x.Name))
        {
            //  Получение канала.
            LinkedChannel channel = _TargetFrame[name];

            //  Проверка токена отмены.
            await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

            //  Проверка канала.
            if (_BadChannelNames.Contains(channel.Name))
            {
                //  Переход к следующему каналу.
                continue;
            }

            //  Получение данных канала.
            double[] items = channel.Items;

            //  Среднее значение.
            double average = 0;

            //  Перебор начальных значений.
            for (int i = 0; i < endIndex; i++)
            {
                //  Корректировка среднего значения.
                average += items[i];
            }

            //  Нормализация среднего значения.
            average /= endIndex;

            //  Перебор всех значений.
            for (int i = 0; i < items.Length; i++)
            {
                //  Смещение значения.
                items[i] -= average;
            }

            //  Сохранение данных.
            channel.Save();
        }
    }

    /// <summary>
    /// Асинхронно проверяет каналы.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, проверяющая каналы.
    /// </returns>
    private async Task ValidationAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение параметров.
        var preprocessor = BasisConstants.Preprocessor;

        //  Перебор имён каналов.
        foreach (string name in Processor.SectionGroups.Sources.Select(x => x.Name))
        {
            //  Получение канала.
            LinkedChannel channel = _TargetFrame[name];

            //  Проверка токена отмены.
            await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

            //  Получение данных канала.
            double[] items = channel.Items;

            //  Перебор блоков.
            for (int block = 0; block * preprocessor.BlockSize < _ChannelLength; block++)
            {
                //  Флаг пустого блока.
                bool isEmpty = true;

                //  Начальный индекс.
                int beginIndex = block * preprocessor.BlockSize;

                //  Перебор значений.
                for (int i = 0; i < preprocessor.BlockSize; i++)
                {
                    //  Проверка значения.
                    if (items[beginIndex + i] != 0)
                    {
                        //  Сброс флага.
                        isEmpty = false;

                        //  Завершение работы с блоком.
                        break;
                    }
                }

                //  Проверка флага.
                if (isEmpty)
                {
                    //  Добавление в список плохих каналов.
                    _BadChannelNames.Add(channel.Name);

                    //  Переход к следующему каналу.
                    continue;
                }
            }
        }
    }

    /// <summary>
    /// Асинхронно копирует каналы.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, копирующая каналы.
    /// </returns>
    private async Task CopyAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Перебор исходных каналов.
        foreach (string name in Processor.SectionGroups.Sources.Select(x => x.Name))
        {
            //  Получение каналов.
            LinkedChannel source = _SourceFrame[name];
            LinkedChannel target = _TargetFrame[name];

            //  Копирование данных.
            Array.Copy(source.Items, target.Items, _ChannelLength);

            //  Сохранение данных.
            target.Save();
        }
    }
    /// <summary>
    /// Возвращает канал.
    /// </summary>
    /// <returns>
    /// Канал.
    /// </returns>
    private LinkedChannel GetChannel(string name)
    {
        //  Возврат канала.
        return _TargetFrame[name];
    }

    /// <summary>
    /// Возвращает счётчик.
    /// </summary>
    /// <param name="section">
    /// Номер сечения.
    /// </param>
    /// <returns>
    /// Счётчик.
    /// </returns>
    private LinkedChannel GetCounter(int section)
    {
        //  Определение имени канала.
        string name = $"Counter{section:00}";

        //  Возврат канала.
        return _TargetFrame[name];
    }

    /// <summary>
    /// Возвращает канал силы по методу двух сечений.
    /// </summary>
    /// <param name="section">
    /// Номер сечения.
    /// </param>
    /// <param name="rail">
    /// Рельс.
    /// </param>
    /// <returns>
    /// Канал силы по методу двух сечений.
    /// </returns>
    private LinkedChannel GetContinuous(int section, Rail rail)
    {
        //  Определение имени канала.
        string name = $"PC{(rail == Rail.Left ? "L" : "R")}{section:00}";

        //  Возврат канала.
        return _TargetFrame[name];
    }

    /// <summary>
    /// Возвращает сигнал.
    /// </summary>
    /// <param name="section">
    /// Номер сечения.
    /// </param>
    /// <param name="rail">
    /// Рельс.
    /// </param>
    /// <param name="side">
    /// Сторона.
    /// </param>
    /// <param name="index">
    /// Индекс сигнала.
    /// </param>
    /// <returns>
    /// Сигнал.
    /// </returns>
    private LinkedChannel GetSignal(int section, Rail rail, Side side, int index)
    {
        //  Возврат канала.
        return _TargetFrame[$"S{(rail == Rail.Left ? "L" : "R")}{(side == Side.External ? "e" : "i")}{section:00}_{index}"];
    }
}
