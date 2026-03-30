using System.Collections;
using System.Runtime.CompilerServices;

namespace Apeiron.Analysis.Schematization;

/// <summary>
/// Представляет гистограмму амплитуд.
/// </summary>
public sealed class Histogram :
    IEnumerable<HistogramUnit>
{
    /// <summary>
    /// Поле для хранения массива единиц гистограммы.
    /// </summary>
    private readonly HistogramUnit[] _Units;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="maxScope">
    /// Максимальный размах.
    /// </param>
    /// <param name="classesCount">
    /// Количество классов.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="maxScope"/> передано бесконечное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="maxScope"/> передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="maxScope"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="maxScope"/> передано нулевое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="classesCount"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="classesCount"/> передано нулевое значение.
    /// </exception>
    public Histogram(double maxScope, int classesCount)
    {
        //  Проверка максимального размаха на равенство бесконечности.
        IsNotInfinity(maxScope, nameof(maxScope));

        //  Проверка максимального размаха на нечисловое значение.
        IsNotNaN(maxScope, nameof(maxScope));

        //  Проверка максимального размаха на отрицательность и равенство нулю.
        IsPositive(maxScope, nameof(maxScope));

        //  Проверка количества классов.
        IsPositive(classesCount, nameof(classesCount));

        //  Установка максимального размаха.
        MaxScope = maxScope;

        //  Определение ширины класса.
        WidthClass = maxScope / classesCount;

        //  Создание массива единиц гистограммы.
        _Units = new HistogramUnit[classesCount];

        //  Заполнение массива единиц гистограммы.
        for (int i = 0; i < classesCount; i++)
        {
            //  Установка единицы гистограммы.
            _Units[i] = new HistogramUnit((i + 1) * WidthClass / 2, 0);
        }
    }

    /// <summary>
    /// Возвращает максимальный размах.
    /// </summary>
    public double MaxScope { get; }

    /// <summary>
    /// Возвращает количество классов.
    /// </summary>
    [Obsolete("Использовать метод Histogram.Count")]
    public int CountClasses => Count;

    /// <summary>
    /// Возвращает ширину класса.
    /// </summary>
    public double WidthClass { get; }

    /// <summary>
    /// Возвращает общее количество амплитуд.
    /// </summary>
    public double TotalCount => _Units.Sum(unit => unit.Count);

    /// <summary>
    /// Возвращает количество единиц гистограммы.
    /// </summary>
    public int Count => _Units.Length;

    /// <summary>
    /// Возвращает или задаёт единицу гистограммы по указанному индексу.
    /// </summary>
    /// <param name="index">
    /// Индекс единицы гистограммы.
    /// </param>
    /// <returns>
    /// Единица гистограммы с указанным индексом.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано значение,
    /// которое равно или превышает значение <see cref="Count"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public HistogramUnit this[int index]
    {
        get => _Units[CheckIndexCore(index)];
        set
        {
            //  Проверка ссылки на единицу гистограммы.
            IsNotNull(value, nameof(HistogramUnit));

            //  Установка нового значения.
            _Units[CheckIndexCore(index)] = value;
        }
    }

    /// <summary>
    /// Выполняет расчёт количества амплитуд методом полных циклов и добавляет в гистограмму.
    /// </summary>
    /// <param name="sequence">
    /// Последовательность значений.
    /// </param>
    public void FullCycles(IEnumerable<double> sequence)
    {
        List<double> list = new(sequence);

        int d = 1;
        while (d < list.Count - 1)
        {
            if ((list[d] - list[d - 1]) * (list[d + 1] - list[d]) >= 0)
            {
                list.RemoveAt(d);
            }
            else
            {
                d++;
            }
        }

        int a = 2;
        while (a < list.Count - 1)
        {
            if (Math.Abs(list[a] - list[a - 1]) < WidthClass)
            {
                list.RemoveAt(a);
                list.RemoveAt(a - 1);
            }
            else
            {
                a++;
            }
        }

        for (int i = 0; i < _Units.Length; i++)
        {
            int count = 0;
            int b = 2;
            while (b < list.Count - 1)
            {
                double value = Math.Abs(list[b] - list[b - 1]);
                if (value >= (i + 1) * WidthClass && value < (i + 2) * WidthClass)
                {
                    list.RemoveAt(b);
                    list.RemoveAt(b - 1);
                    count++;
                }
                else
                {
                    b++;
                }
            }
            _Units[i].Count += count;
        }
    }

    /// <summary>
    /// Выполняет расчёт количества амплитуд методом полных циклов и добавляет в гистограмму.
    /// </summary>
    /// <param name="sequence">
    /// Последовательность значений.
    /// </param>
    public void FullCyclesOld(IEnumerable<double> sequence)
    {
        List<double> list = new(sequence);

        int d = 2;
        while (d < list.Count - 1)
        {
            if ((list[d] - list[d - 1]) * (list[d + 1] - list[d]) >= 0)
            {
                list.RemoveAt(d);
            }
            else
            {
                d++;
            }
        }

        double min = list.Min();
        List<int> listClass = new();
        for (int i = 0; i < list.Count; i++)
        {
            int c = (int)Math.Ceiling((list[i] - min) / WidthClass);
            if (c == 0) { c = 1; }
            listClass.Add(c);
        }


        int a = 2;
        while (a < listClass.Count - 1)
        {
            if (listClass[a] == listClass[a - 1])
            {
                listClass.RemoveAt(a);
                listClass.RemoveAt(a - 1);
            }
            else
            {
                a++;
            }
        }

        for (int i = 0; i < _Units.Length; i++)
        {
            int count = 0;
            int b = 2;
            while (b < listClass.Count - 1)
            {
                if (Math.Abs(listClass[b] - listClass[b - 1]) == i + 1)
                {
                    listClass.RemoveAt(b);
                    listClass.RemoveAt(b - 1);
                    count++;
                }
                else
                {
                    b++;
                }
            }
            _Units[i].Count += count;
        }
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<HistogramUnit> GetEnumerator()
    {
        //  Возврат перечислителя массива единиц гистограммы.
        return ((IEnumerable<HistogramUnit>)_Units).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат перечислителя массива единиц гистограммы.
        return _Units.GetEnumerator();
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
