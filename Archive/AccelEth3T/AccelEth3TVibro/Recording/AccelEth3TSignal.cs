using Simargl.Analysis;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет сигнал.
/// </summary>
public sealed class AccelEth3TSignal
{
    /// <summary>
    /// Поле для хранения длины обновления.
    /// </summary>
    private int _UpdateLength = 0;

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
    /// <param name="name">
    /// Имя сигнала.
    /// </param>
    /// <param name="sampling">
    /// Частота дискретизации.
    /// </param>
    /// <param name="length">
    /// Длина сигнала.
    /// </param>
    public AccelEth3TSignal(string name, double sampling, int length)
    {
        //  Установка имени.
        Name = name;

        //  Создание сигнала.
        Signal = new(sampling, new(length));

        //  Создание спектра.
        Spectrum = new(this);

        //  Создание мощности.
        Power = new(Spectrum);

        //  Создание очереди нарушений.
        Violations = [];
    }

    /// <summary>
    /// Возвращает имя.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Возвращает сигнал.
    /// </summary>
    public Signal Signal { get; }

    /// <summary>
    /// Возвращает спектр.
    /// </summary>
    public AccelEth3TSpectrum Spectrum { get; }

    /// <summary>
    /// Возвращает мощность.
    /// </summary>
    public AccelEth3TPower Power { get; }

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
    /// Добавляет данные в сигнал.
    /// </summary>
    /// <param name="values">
    /// Значения, которые необходимо добавить в сигнал.
    /// </param>
    public void AddData(double[] values)
    {
        //  Получение длины добавляемых данных.
        int size = values.Length * sizeof(double);

        //  Получение массива элементов.
        double[] items = Signal.Items;

        //  Получение смещения текущих данных.
        int offset = items.Length * sizeof(double) - size;

        //  Смещение текущих значений.
        Buffer.BlockCopy(items, size, items, 0, offset);

        //  Копирование новых данных.
        Buffer.BlockCopy(values, 0, items, offset, size);

        //  Проверка необходимости обновления истории.
        if (_StoreEnable)
        {
            //  Перебор новых значений.
            foreach (double value in items)
            {
                //  Корректировка показателей.
                _StoreCount++;
                _StoreSum += value;
                _StoreSquares += value * value;
            }

            //  Расчёт допустимого диапазона.
            double average = _StoreSum / _StoreCount;
            double range = Settings.SignalQuantile * Math.Sqrt(_StoreSquares / _StoreCount - average * average);
            StoreMin = average - range;
            StoreMax = average + range;
        }

        //  Проверка необходимости контроля.
        if (_ControlEnable)
        {
            double currentRange = 0;
            double actual = 0;
            double permissible = 0;
            int count = 0;

            //  Перебор новых значений.
            foreach (double value in items)
            {
                //  Проверка значения.
                if (value > StoreMax)
                {
                    ++count;

                    //  Определение диапазона.
                    double range = value - StoreMax;

                    //  Проверка диапазона.
                    if (currentRange < range)
                    {
                        currentRange = range;
                        actual = value;
                        permissible = StoreMax;
                    }
                }

                //  Проверка значения.
                if (value < StoreMin)
                {
                    ++count;

                    //  Определение диапазона.
                    double range = StoreMin - value;

                    //  Проверка диапазона.
                    if (currentRange < range)
                    {
                        currentRange = range;
                        actual = value;
                        permissible = StoreMin;
                    }
                }
            }

            //  Проверка нарушения.
            if (count > 0)
            {
                //  Добавление нарушения.
                Violations.Enqueue(new(actual, permissible, count));
            }
        }

        //  Корректировка длины обновления.
        _UpdateLength += values.Length;

        //  Проверка необходимости обновления спектра.
        if (_UpdateLength >= Settings.SpectrumUpdateLength)
        {
            //  Сброс длины обновления.
            _UpdateLength = 0;

            //  Обновление спектра.
            Spectrum.Update();

            //  Обновление мощности.
            Power.Update();
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

        Spectrum.BeginStore();
        Power.BeginStore();
    }

    /// <summary>
    /// Завершает сохранение истории о нормальном состоянии.
    /// </summary>
    public void EndStore()
    {
        _StoreEnable = false;

        Spectrum.EndStore();
        Power.EndStore();
    }

    /// <summary>
    /// Начинает контроль.
    /// </summary>
    public void BeginControl()
    {
        _ControlEnable = true;

        Spectrum.BeginControl();
        Power.BeginControl();
    }

    /// <summary>
    /// Завершает контроль.
    /// </summary>
    public void EndControl()
    {
        _ControlEnable = false;

        Spectrum.EndControl();
        Power.EndControl();
    }
}
