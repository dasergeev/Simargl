using System.Collections.Concurrent;
using System.Numerics;
using System.Windows.Forms.DataVisualization.Charting;

namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет мощность сигнала.
/// </summary>
public sealed class AccelEth3TPower
{
    /// <summary>
    /// Поле для хранения спектра.
    /// </summary>
    private readonly AccelEth3TSpectrum _Spectrum;

    /// <summary>
    /// Поле для хранения количества данных в истории.
    /// </summary>
    private int _StoreCount;

    /// <summary>
    /// Поле для хранения количества суммы данных в истории.
    /// </summary>
    private double _StoreSum;

    /// <summary>
    /// Поле для хранения количества суммы квадратов данных в истории.
    /// </summary>
    private double _StoreSquares;

    /// <summary>
    /// Поле для хранения значения, определяющего ведётся ли сохранение истории.
    /// </summary>
    private bool _StoreEnable;

    /// <summary>
    /// Поле для хранения значения, определяющего ведётся ли контроль.
    /// </summary>
    private bool _ControlEnable;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="spectrum">
    /// Спектр.
    /// </param>
    public AccelEth3TPower(AccelEth3TSpectrum spectrum)
    {
        //  Установка спектра.
        _Spectrum = spectrum;

        //  Определение количества точек.
        int length = 1 + Settings.SignalLength / Settings.SpectrumUpdateLength;

        //  Создание коллекции точек графика.
        Points = new DataPoint[length];

        //  Определение шага времени.
        double dX = Settings.SpectrumUpdateLength / (double)Settings.SignalSampling;

        //  Определение смещения.
        int offset = 1 - length;

        //  Настройка точек.
        for (int i = 0; i < length; i++)
        {
            //  Установка точки.
            Points[i] = new((i + offset) * dX, 0);
        }

        //  Создание очереди нарушений.
        Violations = [];
    }

    /// <summary>
    /// Возвращает коллекцию точек графика.
    /// </summary>
    [CLSCompliant(false)]
    public DataPoint[] Points { get; }

    /// <summary>
    /// Возвращает минимально допустимое значение.
    /// </summary>
    public double StoreMin { get; private set; }

    /// <summary>
    /// Возвращает максимально допустимое значение.
    /// </summary>
    public double StoreMax { get; private set; }

    /// <summary>
    /// Возвращает очередь нарушений.
    /// </summary>
    public ConcurrentQueue<Violation> Violations { get; }

    /// <summary>
    /// Возвращает комплексное превышение.
    /// </summary>
    public bool GetViolation(out Violation violation)
    {
        double currentRange = 0;
        double actual = 0;
        double permissible = 0;
        int count = 0;

        while (Violations.TryDequeue(out var item))
        {
            double range = Math.Abs(item.Actual - item.Permissible);
            if (currentRange < range)
            {
                currentRange = range;
                actual = item.Actual;
                permissible = item.Permissible;
                count += item.Count;
            }
        }

        violation = new(actual, permissible, count);

        return count != 0;
    }

    /// <summary>
    /// Обновляет данные.
    /// </summary>
    public void Update()
    {
        //  Перебор точек.
        for (int i = 1; i < Points.Length; i++)
        {
            //  Сдвиг графика.
            Points[i - 1].YValues[0] = Points[i].YValues[0];
        }

        //  Текущее значение.
        double value = 0;

        //  Перебор точек спектра.
        foreach (Complex amplitude in _Spectrum.Spectrum)
        {
            //  Корректировка текущего значения.
            value += amplitude.Magnitude * amplitude.Magnitude;
        }

        //  Нормировка по времени.
        value /= (Settings.SpectrumUpdateLength / (double)Settings.SignalSampling);

        //  Установка текущего значения.
        Points[^1].YValues[0] = value;

        //  Проверка необходимости обновления истории.
        if (_StoreEnable)
        {
            //  Корректировка показателей.
            _StoreCount++;
            _StoreSum += value;
            _StoreSquares += value * value;

            //  Расчёт допустимого диапазона.
            double average = _StoreSum / _StoreCount;
            double range = Settings.SignalQuantile * Math.Sqrt(_StoreSquares / _StoreCount - average * average);
            StoreMin = average - range;
            StoreMax = average + range;
            if (StoreMin < 0)
            {
                StoreMin = 0;
            }
        }

        //  Проверка необходимости контроля.
        if (_ControlEnable)
        {
            //  Проверка значения.
            if (value > StoreMax)
            {
                //  Добавление нарушения.
                Violations.Enqueue(new(value, StoreMax, 1));
            }

            //  Проверка значения.
            if (value < StoreMin)
            {
                //  Добавление нарушения.
                Violations.Enqueue(new(value, StoreMin, 1));
            }
        }
    }

    /// <summary>
    /// Начинает сохранение истории о нормальном состоянии.
    /// </summary>
    public void BeginStore()
    {
        _StoreCount = 0;
        _StoreSum = 0;
        _StoreSquares = 0;
        StoreMin = 0;
        StoreMax = 0;
        _StoreEnable = true;
    }

    /// <summary>
    /// Завершает сохранение истории о нормальном состоянии.
    /// </summary>
    public void EndStore()
    {
        _StoreEnable = false;
    }

    /// <summary>
    /// Начинает контроль.
    /// </summary>
    public void BeginControl()
    {
        _ControlEnable = true;
    }

    /// <summary>
    /// Завершает контроль.
    /// </summary>
    public void EndControl()
    {
        _ControlEnable = false;
    }
}
