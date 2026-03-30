using Apeiron.Algebra;
using Apeiron.Analysis;

namespace Apeiron.Frames;

/// <summary>
/// Представляет канал кадра регистрации.
/// </summary>
public sealed class Channel :
    IEnumerable<double>
{
    /// <summary>
    /// Поле для хранения заголовка канала.
    /// </summary>
    private ChannelHeader _Header;

    /// <summary>
    /// Поле для хранения сигнала.
    /// </summary>
    private Signal _Signal;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="signal">
    /// Сигнал.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="signal"/> передана пустая ссылка.
    /// </exception>
    public Channel(Signal signal) :
        this(new ChannelHeader(), signal)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="header">
    /// Заголовок канала.
    /// </param>
    /// <param name="signal">
    /// Сигнал.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="header"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="signal"/> передана пустая ссылка.
    /// </exception>
    public Channel(ChannelHeader header, Signal signal)
    {
        //  Установка заголовка канала.
        _Header = IsNotNull(header, nameof(header));

        //  Установка сигнала.
        _Signal = IsNotNull(signal, nameof(signal));
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="header">
    /// Заголовок канала.
    /// </param>
    /// <param name="sampling">
    /// Частота дискретизации.
    /// </param>
    /// <param name="vector">
    /// Вектор дыннх сигнала.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="header"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="sampling"/> передано бесконечное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="sampling"/> передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="sampling"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="sampling"/> передано нулевое значение.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="vector"/> передана пустая ссылка.
    /// </exception>
    public Channel(ChannelHeader header, double sampling, Vector<double> vector) :
        this(header, new Signal(sampling, vector))
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="name">
    /// Имя канала.
    /// </param>
    /// <param name="unit">
    /// Единица измерения.
    /// </param>
    /// <param name="sampling">
    /// Частота дискретизации.
    /// </param>
    /// <param name="cutoff">
    /// Частота среза фильтра.
    /// </param>
    /// <param name="length">
    /// Длина массива данных.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="name"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="unit"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="sampling"/> передано бесконечное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="sampling"/> передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="sampling"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="sampling"/> передано нулевое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="length"/> передано отрицательное значение.
    /// </exception>
    public Channel(string name, string unit, double sampling, double cutoff, int length) :
        this(new ChannelHeader(name, unit, cutoff), sampling, new Vector<double>(length))
    {

    }

    /// <summary>
    /// Возвращает или задаёт заголовок канала.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public ChannelHeader Header
    {
        get => _Header;
        set => _Header = IsNotNull(value, nameof(Header));
    }

    /// <summary>
    /// Возвращает или задаёт сигнал канала.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public Signal Signal
    {
        get => _Signal;
        set => _Signal = IsNotNull(value, nameof(value));
    }

    /// <summary>
    /// Возвращает или задаёт вектор данных.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public Vector<double> Vector
    {
        get => _Signal.Vector;
        set => _Signal.Vector = value;
    }

    /// <summary>
    /// Возвращает или задаёт массив элементов сигнала.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public double[] Items
    {
        get => _Signal.Items;
        set => _Signal.Items = value;
    }

    /// <summary>
    /// Возвращает или задаёт формат канала.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Произошла попытка преобразовать заголовок канала в неизвестный формат.
    /// </exception>
    public StorageFormat Format
    {
        get => _Header.Format;
        set
        {
            //  Проверка необходимости изменения формата.
            if (_Header.Format != value)
            {
                //  Преобразование заголовка канала.
                _Header = _Header.Convert(value);
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт имя канала.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string Name
    {
        get => _Header.Name;
        set => _Header.Name = value;
    }

    /// <summary>
    /// Возвращает или задаёт единицу измерения.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string Unit
    {
        get => _Header.Unit;
        set => _Header.Unit = value;
    }

    /// <summary>
    /// Возвращает или задаёт частоту дискретизации.
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
        get => _Signal.Sampling;
        set => _Signal.Sampling = value;
    }

    /// <summary>
    /// Возвращает или задаёт частоту среза фильтра.
    /// </summary>
    public double Cutoff
    {
        get => _Header.Cutoff;
        set => _Header.Cutoff = value;
    }

    /// <summary>
    /// Возвращает или задаёт длину канала.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано отрицательное значение.
    /// </exception>
    public int Length
    {
        get => Vector.Length;
        set => Vector.Length = value;
    }

    /// <summary>
    /// Возвращает или задаёт элемент с указанным индексом.
    /// </summary>
    /// <param name="index">
    /// Индекс элемента.
    /// </param>
    /// <returns>
    /// Элемент с указанным индексом.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано значение большее или равное <see cref="Length"/>.
    /// </exception>
    public double this[int index]
    {
        get => _Signal[index];
        set => _Signal[index] = value;
    }

    /// <summary>
    /// Возвращает значение, в указанное время.
    /// </summary>
    /// <param name="time">
    /// Время.
    /// </param>
    /// <returns>
    /// Значение.
    /// </returns>
    internal double AtTimeSafe(double time)
    {
        int index = (int)(time * Sampling);
        if (index < 0)
        {
            index = 0;
        }
        if (index >= Length)
        {
            index = Length - 1;
        }
        return this[index];
    }

    ///// <summary>
    ///// Масштабирует значения.
    ///// </summary>
    ///// <param name="factor">
    ///// Масштабный множитель.
    ///// </param>
    //internal void Scale(double factor)
    //{
    //    Vector.Scale(factor);
    //}

    ///// <summary>
    ///// Смещает значения.
    ///// </summary>
    ///// <param name="offset">
    ///// Смещение.
    ///// </param>
    //internal void Move(double offset)
    //{
    //    Vector.Move(offset);
    //}

    ///// <summary>
    ///// Изменяет частоту дискретизации.
    ///// </summary>
    ///// <param name="sampling">
    ///// Новая частота дискретизации.
    ///// </param>
    //internal void Resampling(double sampling)
    //{
    //    int length = (int)Math.Floor(Length * sampling / Sampling);
    //    Resampling(sampling, length);
    //}

    ///// <summary>
    ///// Изменяет частоту дискретизации.
    ///// </summary>
    ///// <param name="sampling">
    ///// Новая частота дискретизации.
    ///// </param>
    ///// <param name="length">
    ///// Новая длина канала.
    ///// </param>
    //internal void Resampling(double sampling, int length)
    //{
    //    Vector<double> vector = new(length);
    //    for (int i = 0; i != length; ++i)
    //    {
    //        double time = i / sampling;
    //        int beginSourceIndex = (int)Math.Round(time * Sampling);
    //        if (beginSourceIndex < 0)
    //        {
    //            beginSourceIndex = 0;
    //        }
    //        if (beginSourceIndex > Length - 1)
    //        {
    //            beginSourceIndex = Length - 1;
    //        }
    //        vector[i] = _Vector[beginSourceIndex];
    //    }
    //    _Vector = vector;
    //    Sampling = sampling;
    //}

    /// <summary>
    /// Создаёт копию канала.
    /// </summary>
    /// <returns>
    /// Копия канала.
    /// </returns>
    public Channel Clone()
    {
        return new(Header.Clone(), Signal.Clone());
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<double> GetEnumerator()
    {
        //  Возврат перечислителя сигнала.
        return _Signal.GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат перечислителя сигнала.
        return ((IEnumerable)_Signal).GetEnumerator();
    }
}
