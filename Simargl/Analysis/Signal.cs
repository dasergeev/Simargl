namespace Simargl.Analysis;

/// <summary>
/// Представляет дискретный временной сигнал.
/// </summary>
public sealed class Signal :
    ICloneable
{
    /// <summary>
    /// Происходит при изменении значения свойства <see cref="Sampling"/>.
    /// </summary>
    public event EventHandler? SamplingChanged;

    /// <summary>
    /// Поле для хранения частоты дискретизации в Гц.
    /// </summary>
    private double _Sampling;

    /// <summary>
    /// Поле для хранения элементов сигнала.
    /// </summary>
    private double[] _Items;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="sampling">
    /// Частота дискретизации в Гц.
    /// </param>
    public Signal(double sampling)
    {
        //  Установка частоты дискретизации.
        _Sampling = sampling;

        //  Установка пустого массива элементов сигнала.
        _Items = [];
    }
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="sampling">
    /// Частота дискретизации в Гц.
    /// </param>
    /// <param name="length">
    /// Длина сигнала.
    /// </param>
    public Signal(double sampling, int length)
    {
        //  Установка частоты дискретизации.
        _Sampling = sampling;

        //  Установка элементов сигнала.
        _Items = new double[length];
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="sampling">
    /// Частота дискретизации в Гц.
    /// </param>
    /// <param name="items">
    /// Элементы сигнала.
    /// </param>
    public Signal(double sampling, double[] items)
    {
        //  Установка частоты дискретизации.
        _Sampling = sampling;

        //  Установка элементов сигнала.
        _Items = items;
    }

    /// <summary>
    /// Возвращает или задаёт частоту дискретизации в Гц.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано бесконечное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано нулевое значение.
    /// </exception>
    public double Sampling
    {
        get => _Sampling;
        set
        {
            //  Проверка изменения значения.
            if (_Sampling != value)
            {
                //  Установка нового значения.
                _Sampling = value;

                //  Вызов события об изменении значения.
                OnSamplingChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт значения сигнала.
    /// </summary>
    public double[] Items
    {
        get => _Items;
        set => _Items = value;
    }

    /// <summary>
    /// Возвращает длительность сигнала в секундах.
    /// </summary>
    public double Duration => _Items.Length / Sampling;

    /// <summary>
    /// Возвращает или задаёт длину сигнала.
    /// </summary>
    public int Length
    {
        get => _Items.Length;
        set
        {
            //  Проверка изменения значения.
            if (_Items.Length != value)
            {
                //  Изменение массива элементов.
                Array.Resize(ref _Items, value);
            }
        }
    }

    /// <summary>
    /// Создаёт копию текущего объекта.
    /// </summary>
    /// <returns>
    /// Копия текущего объекта.
    /// </returns>
    public Signal Clone()
    {
        return new(Sampling, (double[])_Items.Clone());
    }

    /// <summary>
    /// Создаёт копию текущего объекта.
    /// </summary>
    /// <returns>
    /// Копия текущего объекта.
    /// </returns>
    object ICloneable.Clone()
    {
        //  Возврат копии текущего объекта.
        return Clone();
    }

    /// <summary>
    /// Вызывает событие <see cref="SamplingChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnSamplingChanged(EventArgs e)
    {
        //  Вызов события.
        SamplingChanged?.Invoke(this, e);
    }
}
