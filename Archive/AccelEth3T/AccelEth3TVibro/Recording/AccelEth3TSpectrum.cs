using Simargl.Analysis;
using System.Collections.Concurrent;
using System.Linq;

namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет спекр сигнала.
/// </summary>
public sealed class AccelEth3TSpectrum
{
    /// <summary>
    /// Поле для хранения исходного сигнала.
    /// </summary>
    private readonly AccelEth3TSignal _Signal;

    /// <summary>
    /// Поле для хранения количества данных в истории.
    /// </summary>
    private int _StoreCount;

    /// <summary>
    /// Поле для хранения количества суммы данных в истории.
    /// </summary>
    private readonly double[] _StoreSum;

    /// <summary>
    /// Поле для хранения количества суммы квадратов данных в истории.
    /// </summary>
    private readonly double[] _StoreSquares;

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
    /// <param name="signal">
    /// Исходный сигнал.
    /// </param>
    public AccelEth3TSpectrum(AccelEth3TSignal signal)
    {
        //  Установка исходного сигнала.
        _Signal = signal;

        //  Создание спектра.
        Spectrum = new(GetSubSignal(signal));

        //  Создание хранилища исторических данных.
        _StoreSum = new double[Spectrum.Count];
        _StoreSquares = new double[Spectrum.Count];
        StoreMin = new double[Spectrum.Count];
        StoreMax = new double[Spectrum.Count];

        //  Создание очереди нарушений.
        Violations = [];
    }

    /// <summary>
    /// Возвращает спектр.
    /// </summary>
    public Spectrum Spectrum { get; private set; }

    /// <summary>
    /// Возвращает минимально допустимое значение.
    /// </summary>
    public double[] StoreMin { get; }

    /// <summary>
    /// Возвращает максимально допустимое значение.
    /// </summary>
    public double[] StoreMax { get; }

    /// <summary>
    /// Возвращает очередь нарушений.
    /// </summary>
    public ConcurrentQueue<SpectrumViolation> Violations { get; }

    /// <summary>
    /// Возвращает комплексное превышение.
    /// </summary>
    public bool GetViolation(out SpectrumViolation violation)
    {
        double currentRange = 0;
        double actual = 0;
        double permissible = 0;
        double frequency = 0;
        int count = 0;

        while (Violations.TryDequeue(out var item))
        {
            double range = Math.Abs(item.Actual - item.Permissible);
            if (currentRange < range)
            {
                currentRange = range;
                actual = item.Actual;
                permissible = item.Permissible;
                frequency = item.Frequency;
                count += item.Count;
            }
        }

        violation = new(actual, permissible, frequency, count);

        return count != 0;
    }
    /// <summary>
    /// Обновляет спектр.
    /// </summary>
    public void Update()
    {
        //  Установка нового значения спектра.
        Spectrum = new(GetSubSignal(_Signal));

        //  Проверка необходимости обновления истории.
        if (_StoreEnable)
        {
            //  Получение данных спектра.
            double[] y = Spectrum.Select(value => value.Magnitude).ToArray();

            _StoreCount++;

            //  Перебор новых значений.
            for (int i = 0; i < y.Length; i++)
            {
                //  Корректировка показателей.
                _StoreSum[i] += y[i];
                _StoreSquares[i] += y[i] * y[i];

                //  Расчёт допустимого диапазона.
                double average = _StoreSum[i] / _StoreCount;
                double range = Settings.SpectrumQuantile * Math.Sqrt(_StoreSquares[i] / _StoreCount - average * average);
                StoreMin[i] = average - range;
                StoreMax[i] = average + range;

                if (StoreMin[i] < 0)
                {
                    StoreMin[i] = 0;
                }
            }
        }

        //  Проверка необходимости контроля.
        if (_ControlEnable)
        {
            //  Получение данных спектра.
            double[] y = Spectrum.Select(value => value.Magnitude).ToArray();

            //  Перебор новых значений.
            for (int i = 0; i < y.Length; i++)
            {
                //  Проверка значения.
                if (y[i] > StoreMax[i])
                {
                    //  Добавление нарушения.
                    Violations.Enqueue(new(y[i], StoreMax[i], i * Spectrum.FrequencyStep, 1));
                }

                //  Проверка значения.
                if (y[i] < StoreMin[i])
                {
                    //  Добавление нарушения.
                    Violations.Enqueue(new(y[i], StoreMin[i], i * Spectrum.FrequencyStep, 1));
                }
            }
        }
    }

    /// <summary>
    /// Начинает сохранение истории о нормальном состоянии.
    /// </summary>
    public void BeginStore()
    {
        _StoreCount = 0;
        Array.Clear(_StoreSum);
        Array.Clear(_StoreSquares);
        Array.Clear(StoreMin);
        Array.Clear(StoreMax);
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
    /// Возвращает подсигнал.
    /// </summary>
    /// <param name="signal">
    /// Исходный сигнал.
    /// </param>
    /// <returns>
    /// Подсигнал.
    /// </returns>
    private static Signal GetSubSignal(AccelEth3TSignal signal)
    {
        //  Возврат подсигнала.
        return new(signal.Signal.Sampling,
            signal.Signal.Vector.Subvector(signal.Signal.Length - Settings.SpectrumSignalLength));
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
