using Apeiron.Compute.Cpu;
using Apeiron.Frames;
using System.Runtime.CompilerServices;

namespace Apeiron.Analysis;

/// <summary>
/// Представляет спектр сигнала.
/// </summary>
public sealed class Spectrum :
    IEnumerable<Complex>,
    ICloneable
{
    /// <summary>
    /// Поле для хранения массива амплитуд.
    /// </summary>
    private readonly Complex[] _Amplitudes;

    /// <summary>
    /// Поле для хранения длины сигнала.
    /// </summary>
    private readonly int _SignalLength;

    /// <summary>
    /// Поле для хранения значения, определяющего является ли длина сигнала чётной.
    /// </summary>
    private readonly bool _IsEven;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="amplitudes">
    /// Массив амплитуд.
    /// </param>
    /// <param name="signalLength">
    /// Длина сигнала.
    /// </param>
    /// <param name="frequencyStep">
    /// Шаг частоты.
    /// </param>
    private Spectrum([ParameterNoChecks] Complex[] amplitudes,
        [ParameterNoChecks] int signalLength, [ParameterNoChecks] double frequencyStep)
    {
        //  Установка полей.
        _Amplitudes = amplitudes;
        _SignalLength = signalLength;
        _IsEven = signalLength % 2 == 0;
        FrequencyStep = frequencyStep;
        Count = amplitudes.Length;
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="signal">
    /// Сигнал, для которого необходимо построить спектр.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="signal"/> передана пустая ссылка.
    /// </exception>
    public Spectrum(Signal signal)
    {
        //  Проверка ссылки на сигнал.
        IsNotNull(signal, nameof(signal));

        //  Получение частоты дискретизации.
        double sampling = signal.Sampling;

        //  Получение массива элементов сигнала.
        double[] items = signal.Items;

        //  Определение периода сигнала.
        double period = items.Length / sampling;

        //  Установка шага частоты.
        FrequencyStep = 1.0 / period;

        //  Определение длины сигнала.
        int length = items.Length;

        //  Установка длины сигнала.
        _SignalLength = length;

        //  Установка чётности длины сигнала.
        _IsEven = length % 2 == 0;

        //  Проверка длины.
        switch (length)
        {
            case 0:
                //  Установка количества амплитуд.
                Count = 0;

                //  Установка массива амплитуд.
                _Amplitudes = Array.Empty<Complex>();
                break;
            case 1:
                //  Установка количества амплитуд.
                Count = 1;

                //  Создание массива амплитуд.
                _Amplitudes = new Complex[Count];

                //  Установка нулевой амплитуды.
                _Amplitudes[0] = items[0];
                break;
            case 2:
                //  Установка количества амплитуд.
                Count = 2;

                //  Создание массива амплитуд.
                _Amplitudes = new Complex[Count];

                //  Установка нулевой амплитуды.
                _Amplitudes[0] = 0.5 * (items[0] + items[1]);

                //  Установка первой амплитуды.
                _Amplitudes[1] = 0.5 * (items[0] - items[1]);
                break;
            default:
                {
                    //  Массив для хранения спектра сигнала.
                    var spectrum = new Complex[length];

                    //  Заполнение массива для получения спектра.
                    for (int i = 0; i < length; i++)
                    {
                        //  Установка значения.
                        spectrum[i] = items[i];
                    }

                    //  Выполнение преобразования Фурье.
                    Fft.Direct(spectrum);

                    //  Множитель, учитывающий длину сигнала.
                    double lengthFactor = 1.0 / length;

                    //  Проверка чётности длины.
                    if (_IsEven)
                    {
                        //  Определение количества амплитуд.
                        Count = length >> 1;

                        //  Создание массива амплитуд.
                        _Amplitudes = new Complex[Count + 1];

                        //  Установка нулевой амплитуды.
                        _Amplitudes[0] = lengthFactor * spectrum[0];

                        //  Установка последней амплитуды.
                        _Amplitudes[Count] = lengthFactor * spectrum[Count];

                        //  Корректировка множителя, учитывающего длину сигнала.
                        lengthFactor *= 2.0;

                        //  Установка промежуточных амплитуд.
                        for (int i = 1; i < Count; i++)
                        {
                            //  Установка амплитуды.
                            _Amplitudes[i] = lengthFactor * spectrum[i];
                        }

                        //  Корректировка количества амплитуд.
                        ++Count;
                    }
                    else
                    {
                        //  Определение количества амплитуд.
                        Count = ((length - 1) >> 1) + 1;

                        //  Создание массива амплитуд.
                        _Amplitudes = new Complex[Count];

                        //  Установка нулевой амплитуды.
                        _Amplitudes[0] = lengthFactor * spectrum[0];

                        //  Корректировка множителя, учитывающего длину сигнала.
                        lengthFactor *= 2.0;

                        //  Установка промежуточных амплитуд.
                        for (int i = 1; i < Count; i++)
                        {
                            //  Установка амплитуды.
                            _Amplitudes[i] = lengthFactor * spectrum[i];
                        }
                    }
                }
                break;
        }
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="channel">
    /// Канал, для которого необходимо построить спектр.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="channel"/> передана пустая ссылка.
    /// </exception>
    public Spectrum(Channel channel) :
        this(IsNotNull(channel, nameof(channel)).Signal)
    {

    }

    /// <summary>
    /// Возвращает шаг частоты.
    /// </summary>
    public double FrequencyStep { get; }

    /// <summary>
    /// Возвращает количество амплитуд.
    /// </summary>
    public int Count { get; }

    /// <summary>
    /// Возвращает или задаёт элемент по указанному индексу.
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
    /// В параметре <paramref name="index"/> передано значение большее или равное <see cref="Count"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Значение с указанным индексом должно быть действительным.
    /// </exception>
    public Complex this[int index]
    {
        get
        {
            //  Проверка индекса.
            CheckIndexCore(index);

            //  Возврат элемента с указанным индексом.
            return _Amplitudes[index];
        }
        set
        {
            //  Проверка индекса.
            CheckIndexCore(index);

            //  Проверка значений специальных амплитуд.
            if (index == 0 || (index == Count - 1 && _IsEven))
            {
                //  Проверка мнимой части.
                if (value.Imaginary != 0)
                {
                    //  Значение должно быть действительным.
                    throw Exceptions.ArgumentOutOfRange(string.Empty);
                }
            }

            //  Установка значения элемента с указанным индексом.
            _Amplitudes[index] = value;
        }
    }

    /// <summary>
    /// Восстанавливает сигнал по спектру.
    /// </summary>
    /// <param name="channel">
    /// Канал, в который необходимо поместить восстанавливаемый сигнал.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="channel"/> передана пустая ссылка.
    /// </exception>
    public void Restore(Channel channel)
    {
        //  Проверка ссылки на канал.
        channel = IsNotNull(channel, nameof(channel));

        //  Восстановление сигнала.
        Restore(channel.Signal);
    }

    /// <summary>
    /// Восстанавливает сигнал по спектру.
    /// </summary>
    /// <param name="signal">
    /// Сигнал, в который необходимо поместить восстанавливаемый сигнал.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="signal"/> передана пустая ссылка.
    /// </exception>
    public void Restore(Signal signal)
    {
        //  Проверка ссылки на канал.
        signal = IsNotNull(signal, nameof(signal));

        //  Корректировка длины канала.
        signal.Length = _SignalLength;

        //  Корректировка частоты дискретизации канала.
        signal.Sampling = _SignalLength * FrequencyStep;

        //  Восстановление сигнала.
        Restore(signal.Items);
    }

    /// <summary>
    /// Восстанавливает сигнал по спектру.
    /// </summary>
    /// <param name="items">
    /// Массив, в который необходимо поместить восстанавливаемый сигнал.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="items"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Длина массива <paramref name="items"/> меньше допустимого значения.
    /// </exception>
    public void Restore(double[] items)
    {
        //  Проверка массива.
        IsArrayRoomy(items, _SignalLength, nameof(items));

        //  Проверка длины.
        switch (_SignalLength)
        {
            case 1:
                //  Установка нулевого значения.
                items[0] = _Amplitudes[0].Real;
                break;
            case 2:
                {
                    //  Получение нулевой амплитуды.
                    var zeroAmplitude = _Amplitudes[0].Real;

                    //  Получение первой асплитуды.
                    var firstAmplitude = _Amplitudes[1].Real;

                    //  Установка нулевого значения.
                    items[0] = zeroAmplitude + firstAmplitude;

                    //  Установка первого значения.
                    items[1] = zeroAmplitude - firstAmplitude;
                }
                break;
            default:
                {
                    //  Массив для хранения спектра сигнала.
                    var spectrum = new Complex[_SignalLength];

                    //  Множитель, учитывающий длину сигнала.
                    var lengthFactor = 0.5 * _SignalLength;

                    //  Проверка чётности длины.
                    if (_IsEven)
                    {
                        //  Установка нулевой амплитуды.
                        spectrum[0] = _SignalLength * _Amplitudes[0];

                        //  Установка последней амплитуды.
                        spectrum[Count - 1] = _SignalLength * _Amplitudes[Count - 1];

                        //  Установка промежуточных амплитуд.
                        for (int i = 1; i < Count - 1; i++)
                        {
                            //  Определение значения.
                            var value = lengthFactor * _Amplitudes[i];

                            //  Установка амплитуд.
                            spectrum[i] = value;
                            spectrum[_SignalLength - i] = Complex.Conjugate(value);
                        }
                    }
                    else
                    {
                        //  Установка нулевой амплитуды.
                        spectrum[0] = _SignalLength * _Amplitudes[0];

                        //  Установка промежуточных амплитуд.
                        for (int i = 1; i < Count; i++)
                        {
                            //  Определение значения.
                            var value = lengthFactor * _Amplitudes[i];

                            //  Установка амплитуд.
                            spectrum[i] = value;
                            spectrum[_SignalLength - i] = Complex.Conjugate(value);
                        }
                    }

                    //  Выполнение обратного преобразования Фурье.
                    Fft.Inverse(spectrum);

                    //  Заполнение массива сигнала.
                    for (int i = 0; i < _SignalLength; i++)
                    {
                        //  Установка значения.
                        items[i] = spectrum[i].Real;
                    }
                }
                break;
        }
    }

    /// <summary>
    /// Выполняет преобразование спектра.
    /// </summary>
    /// <param name="reformer">
    /// Метод, выполняющий преобразование спектра.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="reformer"/> передана пустая ссылка.
    /// </exception>
    public void Reform(SpectrumReformer reformer)
    {
        //  Проверка ссылки на метод, выполняющий преобразование.
        reformer = IsNotNull(reformer, nameof(reformer));

        //  Проверка длины.
        switch (_SignalLength)
        {
            case 1:
                //  Преобразование нулевой амплитуды.
                _Amplitudes[0] = reformer(0, _Amplitudes[0]).Real;
                break;
            case 2:
                {
                    //  Преобразование нулевой амплитуды.
                    _Amplitudes[0] = reformer(0, _Amplitudes[0]).Real;

                    //  Преобразование первой амплитуды.
                    _Amplitudes[1] = reformer(FrequencyStep, _Amplitudes[1]).Real;
                }
                break;
            default:
                {
                    //  Проверка чётности длины.
                    if (_IsEven)
                    {
                        //  Преобразование нулевой амплитуды.
                        _Amplitudes[0] = reformer(0, _Amplitudes[0]).Real;

                        //  Преобразование последней амплитуды.
                        _Amplitudes[Count - 1] = reformer(FrequencyStep * (Count - 1), _Amplitudes[Count - 1]).Real;

                        //  Преобразование промежуточных амплитуд.
                        for (int i = 1; i < Count - 1; i++)
                        {
                            //  Преобразование амплитуды.
                            _Amplitudes[i] = reformer(FrequencyStep * i, _Amplitudes[i]);
                        }
                    }
                    else
                    {
                        //  Преобразование нулевой амплитуды.
                        _Amplitudes[0] = reformer(0, _Amplitudes[0]).Real;

                        //  Преобразование промежуточных амплитуд.
                        for (int i = 1; i < Count; i++)
                        {
                            //  Преобразование амплитуды.
                            _Amplitudes[i] = reformer(FrequencyStep * i, _Amplitudes[i]);
                        }
                    }
                }
                break;
        }
    }

    /// <summary>
    /// Возвращает копию текущего объекта.
    /// </summary>
    /// <returns>
    /// Копия текущего объекта.
    /// </returns>
    public Spectrum Clone()
    {
        //  Возврат нового спектра.
        return new((Complex[])_Amplitudes.Clone(), _SignalLength, FrequencyStep);
    }

    /// <summary>
    /// Возвращает копию текущего объекта.
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
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<Complex> GetEnumerator()
    {
        return ((IEnumerable<Complex>)_Amplitudes).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return _Amplitudes.GetEnumerator();
    }

    /// <summary>
    /// Выполняет проверку индекса.
    /// </summary>
    /// <param name="index">
    /// Проверяемый индекс.
    /// </param>
    /// <returns>
    /// Проверенный индекс.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано значение,
    /// которое равно или превышает значение <see cref="Count"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal int CheckIndexCore(int index)
    {
        //  Проверка на отрицательность.
        IsNotNegative(index, nameof(index));

        //  Проверка на превышение максимального значения.
        IsLess(index, Count, nameof(index));

        //  Возврат проверенного значения.
        return index;
    }
}
