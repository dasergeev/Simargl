using RailTest.Memory;
using System;
using System.Runtime.InteropServices;

namespace RailTest.Algebra;

/// <summary>
/// Представляет вектор с действительными значениями.
/// </summary>
public unsafe sealed class RealVector : Vector
{
    /// <summary>
    /// Поле для хранения указателя на область памяти, в которой расположены элементы вектора.
    /// </summary>
    private double* _Items;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public RealVector() :
        this(0)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="length">
    /// Длина.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="length"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="OutOfMemoryException">
    /// Недостаточно памяти для выполнения запроса.
    /// </exception>
    public RealVector(int length) :
        base(sizeof(double), length)
    {
        
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="items">
    /// Массив значений.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="items"/> передана пустая ссылка.
    /// </exception>
    public RealVector(params double[] items) :
        this((items ?? throw new ArgumentNullException(nameof(items), "Передана пустая ссылка.")).Length)
    {
        int length = Length;
        for (int i = 0; i != length; ++i)
        {
            _Items[i] = items[i];
        }
    }

    /// <summary>
    /// Возвращает или задаёт значение по указанному индексу.
    /// </summary>
    /// <param name="index">
    /// Индекс значения.
    /// </param>
    /// <returns>
    /// Значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// - или -
    /// В параметре <paramref name="index"/> передано означение, большее или равное <see cref="Vector.Length"/>.
    /// - или -
    /// Передано нечисловое значение.
    /// - или -
    /// Передано бесконечное значение.
    /// </exception>
    public double this[int index]
    {
        get
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Передано отрицательное значение.");
            }
            else if (index >= Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Передано значение, большее или равное длине.");
            }
            return _Items[index];
        }
        set
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Передано отрицательное значение.");
            }
            else if (index >= Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Передано значение, большее или равное длине.");
            }
            if (double.IsNaN(value))
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Передано нечисловое значение.");
            }
            if (double.IsInfinity(value))
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Передано бесконечное значение.");
            }
            _Items[index] = value;
        }
    }

    /// <summary>
    /// Возвращает модуль вектора.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Длина <see cref="Vector.Length"/> равна нулю.
    /// </exception>
    public double Magnitude
    {
        get
        {
            int length = Length;
            if (length == 0)
            {
                throw new InvalidOperationException("Произошла попытка определить модуль вектора нулевой размерности.");
            }
            double magnitude = 0;
            for (int i = 0; i != length; ++i)
            {
                magnitude += _Items[i] * _Items[i];
            }
            return Math.Sqrt(magnitude);
        }
    }

    /// <summary>
    /// Возвращает среднее значение элементов.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Длина <see cref="Vector.Length"/> равна нулю.
    /// </exception>
    public double Average
    {
        get
        {
            int length = Length;
            if (length == 0)
            {
                throw new InvalidOperationException("Произошла попытка определить среднее значение нулевого количества элементов.");
            }
            double average = 0;
            for (int i = 0; i != length; ++i)
            {
                average += _Items[i];
            }
            return average / length;
        }
    }

    /// <summary>
    /// Возвращает минимальное значение.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Длина <see cref="Vector.Length"/> равна нулю.
    /// </exception>
    public double Min
    {
        get
        {
            int length = Length;
            if (length == 0)
            {
                throw new InvalidOperationException("Произошла попытка определить среднее значение нулевого количества элементов.");
            }
            double min = _Items[0];
            for (int i = 0; i != length; ++i)
            {
                if (min > _Items[i])
                {
                    min = _Items[i];
                }
            }
            return min;
        }
    }

    /// <summary>
    /// Возвращает максимальное значение.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Длина <see cref="Vector.Length"/> равна нулю.
    /// </exception>
    public double Max
    {
        get
        {
            int length = Length;
            if (length == 0)
            {
                throw new InvalidOperationException("Произошла попытка определить среднее значение нулевого количества элементов.");
            }
            double max = _Items[0];
            for (int i = 0; i != length; ++i)
            {
                if (max < _Items[i])
                {
                    max = _Items[i];
                }
            }
            return max;
        }
    }

    /// <summary>
    /// Возвращает индекс минимального значения.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Происходит в случае, если <see cref="Vector.Length"/> равно нулю.
    /// </exception>
    public int IndexMin
    {
        get
        {
            int length = Length;
            if (length == 0)
            {
                throw new InvalidOperationException("Нельзя определить минимальное значение вектора нулевой длины.");
            }
            int index = 0;
            double minimum = _Items[0];
            for (int i = 0; i != length; ++i)
            {
                if (minimum > _Items[i])
                {
                    minimum = _Items[i];
                    index = i;
                }
            }
            return index;
        }
    }

    /// <summary>
    /// Возвращает индекс максимального значения.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Происходит в случае, если <see cref="Vector.Length"/> равно нулю.
    /// </exception>
    public int IndexMax
    {
        get
        {
            int length = Length;
            if (length == 0)
            {
                throw new InvalidOperationException("Нельзя определить максимальное значение вектора нулевой длины.");
            }

            int index = 0;
            double maximum = 0;
            maximum = _Items[0];
            for (int i = 0; i != length; ++i)
            {
                if (maximum < _Items[i])
                {
                    maximum = _Items[i];
                    index = i;
                }
            }
            return index;
        }
    }

    /// <summary>
    /// Возвращает СКО.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Происходит в случае, если <see cref="Vector.Length"/> меньше 2.
    /// </exception>
    public double StandardDeviation
    {
        get
        {
            int length = Length;
            if (length < 2)
            {
                throw new InvalidOperationException("Нельзя определить СКО вектора длины меньшей 2.");
            }
            double mean = Average;
            double result = 0;
            for (int i = 0; i != length; ++i)
            {
                double value = _Items[i] - mean;
                result += value * value;
            }
            return Math.Sqrt(result / (length - 1));
        }
    }

    /// <summary>
    /// Возвращает минимально вероятное значение.
    /// </summary>
    /// <param name="probability">
    /// Вероятность.
    /// </param>
    /// <param name="type">
    /// Значение, определяющее тип закона распределения.
    /// </param>
    /// <returns>
    /// Минимально вероятное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Значение <paramref name="probability"/> меньше или равно нулю
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Значение <paramref name="probability"/> больше или равно единице.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Значение <paramref name="type"/> не содержится в перечислении <see cref="ProbabilityType"/>.
    /// </exception>
    public double GetMinProbable(double probability, ProbabilityType type)
    {
        if (probability <= 0 || probability >= 1)
        {
            throw new ArgumentOutOfRangeException(nameof(probability), "Вероятность должны быть больше нуля и меньше единицы.");
        }
        switch (type)
        {
            case ProbabilityType.Standard:
                {
                    double quantile = Real_StandardNormalQuantile(probability);
                    double mean = Average;
                    double deviation = StandardDeviation;
                    return mean - quantile * deviation;
                }
            case ProbabilityType.Empirical:
                return GetMaxProbable(1 - probability, ProbabilityType.Empirical);
            default:
                throw new ArgumentOutOfRangeException(nameof(type), "Значение не содержится в перечислении.");
        }
    }

    /// <summary>
    /// Возвращает минимально вероятное значение по теоретическому квантилю.
    /// </summary>
    /// <param name="probability">
    /// Вероятность.
    /// </param>
    /// <returns>
    /// Минимально вероятное значение по теоретическому квантилю.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Происходит в случае, если значение <paramref name="probability"/> меньше или равно нулю
    /// - или -
    /// значение <paramref name="probability"/> больше или равно единице.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Происходит в случае, если неуправляемая библиотека создала исключение.
    /// </exception>
    public double GetStandardMinProbable(double probability)
    {
        return GetMinProbable(probability, ProbabilityType.Standard);
    }

    /// <summary>
    /// Возвращает минимально вероятное значение по эмпирическому квантилю.
    /// </summary>
    /// <param name="probability">
    /// Вероятность.
    /// </param>
    /// <returns>
    /// Минимально вероятное значение по эмпирическому квантилю.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Происходит в случае, если значение <paramref name="probability"/> меньше или равно нулю
    /// - или -
    /// значение <paramref name="probability"/> больше или равно единице.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Происходит в случае, если неуправляемая библиотека создала исключение.
    /// </exception>
    public double GetEmpiricalMinProbable(double probability)
    {
        return GetMinProbable(probability, ProbabilityType.Empirical);
    }

    /// <summary>
    /// Возвращает максимально вероятное значение.
    /// </summary>
    /// <param name="probability">
    /// Вероятность.
    /// </param>
    /// <param name="type">
    /// Значение, определяющее тип закона распределения.
    /// </param>
    /// <returns>
    /// Максимально вероятное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Значение <paramref name="probability"/> меньше или равно нулю
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Значение <paramref name="probability"/> больше или равно единице.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Значение <paramref name="type"/> не содержится в перечислении <see cref="ProbabilityType"/>.
    /// </exception>
    public double GetMaxProbable(double probability, ProbabilityType type)
    {
        if (probability <= 0 || probability >= 1)
        {
            throw new ArgumentOutOfRangeException(nameof(probability), "Вероятность должны быть больше нуля и меньше единицы.");
        }
        switch (type)
        {
            case ProbabilityType.Standard:
                {
                    double quantile = Real_StandardNormalQuantile(probability);
                    double mean = Average;
                    double deviation = StandardDeviation;
                    return mean + quantile * deviation;
                }
            case ProbabilityType.Empirical:
                {
                    int length = Length;
                    double result = 0;
                    double* duplicate = null;
                    try
                    {
                        duplicate = (double*)MemoryManager.Alloc(length * sizeof(double));
                        MemoryManager.Copy(new IntPtr(duplicate), new IntPtr(_Items), length * sizeof(double));
                        RealSeries_Sort(duplicate, length);
                        int index = (length * probability > 1) ? (int)((length * probability) - 1) : 0;
                        if (index >= length) index = length - 1;
                        result = duplicate[index];
                    }
                    finally
                    {
                        MemoryManager.Free(new IntPtr(duplicate));
                    }
                    return result;
                }
            default:
                throw new ArgumentOutOfRangeException(nameof(type), "Значение не содержится в перечислении.");
        }
    }

    /// <summary>
    /// Возвращает максимально вероятное значение по теоретическому квантилю.
    /// </summary>
    /// <param name="probability">
    /// Вероятность.
    /// </param>
    /// <returns>
    /// Максимально вероятное значение по теоретическому квантилю.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Происходит в случае, если значение <paramref name="probability"/> меньше или равно нулю
    /// - или -
    /// значение <paramref name="probability"/> больше или равно единице.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Происходит в случае, если неуправляемая библиотека создала исключение.
    /// </exception>
    public double GetStandardMaxProbable(double probability)
    {
        return GetMaxProbable(probability, ProbabilityType.Standard);
    }

    /// <summary>
    /// Возвращает максимально вероятное значение по эмпирическому квантилю.
    /// </summary>
    /// <param name="probability">
    /// Вероятность.
    /// </param>
    /// <returns>
    /// Максимально вероятное значение по эмпирическому квантилю.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Происходит в случае, если значение <paramref name="probability"/> меньше или равно нулю
    /// - или -
    /// значение <paramref name="probability"/> больше или равно единице.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Происходит в случае, если неуправляемая библиотека создала исключение.
    /// </exception>
    public double GetEmpiricalMaxProbable(double probability)
    {
        return GetMaxProbable(probability, ProbabilityType.Empirical);
    }

    ///// <summary>
    ///// Выполняет нормализацию по правилу трёх сигм.
    ///// </summary>
    ///// <exception cref="InvalidOperationException">
    ///// Происходит в случае, если неуправляемая библиотека выдала исключение.
    ///// </exception>
    //public void ThreeSigmaNormalization()
    //{
    //    Import.RealVector_ThreeSigmaNormalization(Handle);
    //}

    /// <summary>
    /// Масштабирует значения всех элементов вектора.
    /// </summary>
    /// <param name="factor">
    /// Масштабный множитель.
    /// </param>
    public void Scale(double factor)
    {
        int length = Length;
        for (int i = 0; i != length; ++i)
        {
            _Items[i] *= factor;
        }
    }

    /// <summary>
    /// Смещает значения всех элементов вектора.
    /// </summary>
    /// <param name="offset">
    /// Смещение.
    /// </param>
    public void Move(double offset)
    {
        int length = Length;
        for (int i = 0; i != length; ++i)
        {
            _Items[i] += offset;
        }
    }

    /// <summary>
    /// Заполняет всех элементов вектора заданным значением.
    /// </summary>
    /// <param name="value">
    /// Значение, которым заполняется вектор.
    /// </param>
    public void Fill(double value)
    {
        int length = Length;
        for (int i = 0; i != length; ++i)
        {
            _Items[i] = value;
        }
    }

    /// <summary>
    /// Устанавливает все элементы вектора в нулевое значение.
    /// </summary>
    public void Zero()
    {
        int length = Length;
        if (length > 0)
        {
            MemoryManager.Zero(Pointer, length * (long)sizeof(double));
        }
    }

    /// <summary>
    /// Изменяет порядок элементов в указанном диапазоне.
    /// </summary>
    /// <param name="index">
    /// Отсчитываемый от нуля индекс начала диапазона, порядок элементов которого требуется изменить.
    /// </param>
    /// <param name="count">
    /// Число элементов в диапазоне, порядок сортировки в котором требуется изменить.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Параметры не указывают допустимый диапазон.
    /// </exception>
    public void Reverse(int index, int count)
    {
        if (index < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Передано отрицательное значение.");
        }
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "Передано отрицательное значение.");
        }
        if (index + count > Length)
        {
            throw new ArgumentException("Параметры не указывают допустимый диапазон.");
        }
        if (count > 1)
        {
            int halfCount = count >> 1;
            for (int i = 0; i != halfCount; ++i)
            {
                (_Items[index + count - i - 1], _Items[index + i]) = (_Items[index + i], _Items[index + count - i - 1]);
            }
        }
    }

    /// <summary>
    /// Изменяет порядок элементов во всем векторе на обратный.
    /// </summary>
    public void Reverse()
    {
        Reverse(0, Length);
    }


    /// <summary>
    /// Выполняет циклический сдвиг.
    /// </summary>
    /// <param name="offset">
    /// Смещение.
    /// </param>
    public void CyclicShift(int offset)
    {
        RealSeries_CyclicShift(_Items, Length, -offset);
    }

    private static void RealSeries_CyclicShift(double* series, int length, int offset)
    {
        if (length == 0 || offset == 0)
        {
            return;
        }
        int normalOffset = offset % length;
        if (normalOffset == 0)
        {
            return;
        }
        else if (normalOffset < 0)
        {
            normalOffset += length;
        }

        double* duplicate = null;
        try
        {
            duplicate = (double*)MemoryManager.Alloc(length * sizeof(double));
            MemoryManager.Copy(duplicate, series, length * sizeof(double));
            MemoryManager.Copy(series, duplicate + normalOffset, (length - normalOffset) * sizeof(double));
            MemoryManager.Copy(series + (length - normalOffset), duplicate, normalOffset * sizeof(double));
        }
        finally
        {
            MemoryManager.Free(duplicate);
        }
    }



    ///// <summary>
    ///// Определяет сдвиг относительно вектора-шаблона.
    ///// </summary>
    ///// <param name="pattern">
    ///// Вектор-шаблон.
    ///// </param>
    ///// <returns>
    ///// Сдвиг относительно вектора-шаблона.
    ///// </returns>
    ///// <exception cref="InvalidOperationException">
    ///// Происходит в случае, если неуправляемая библиотека выдала исключение.
    ///// </exception>
    //public int Shift(RealVector pattern)
    //{
    //    ulong result = LongShift(pattern);
    //    if (result > int.MaxValue)
    //    {
    //        throw new OverflowException("Результат не может уместиться в 32-битном числе.");
    //    }
    //    return (int)result;
    //}


    /// <summary>
    /// Возвращает подвектор.
    /// </summary>
    /// <param name="index">
    /// Индекс, с которого начинается подвектор.
    /// </param>
    /// <param name="length">
    /// Размерность подвектора.
    /// </param>
    /// <returns>
    /// Подвектор.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Происходит в случае, если значение параметра <paramref name="index"/> меньше нуля
    /// - или -
    /// значение параметра <paramref name="length"/> меньше нуля
    /// - или -
    /// сумма значений параметров <paramref name="index"/> и <paramref name="length"/> больше свойства <see cref="Vector.Length"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Происходит в случае, если неуправляемая библиотека выдала исключение.
    /// </exception>
    public RealVector GetSubVector(int index, int length)
    {
        if (index < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Произошла попытка получить подвектор отрицательного индекса.");
        }
        if (length < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length), "Произошла попытка получить подвектор отрицательной размерности.");
        }
        if ((ulong)index + (ulong)length > (ulong)Length)
        {
            throw new ArgumentOutOfRangeException("index + length", "Произошла попытка получить подвектор, который не умещается в векторе.");
        }
        RealVector subVector = new(length);
        if (length > 0)
        {
            MemoryManager.Copy(subVector.Pointer, new IntPtr(_Items + index), length * sizeof(double));
        }
        return subVector;
    }

    ///// <summary>
    ///// Выполняет преобразование вектора.
    ///// </summary>
    ///// <param name="target">
    ///// Результат преобразования.
    ///// </param>
    ///// <param name="source">
    ///// Исходный вектор.
    ///// </param>
    ///// <param name="matrix">
    ///// Матрица преобразования.
    ///// </param>
    ///// <exception cref="InvalidOperationException">
    ///// Происходит в случае, если неуправляемая библиотека выдала исключение.
    ///// </exception>
    //public static void Transform(RealVector target, RealVector source, RealMatrix matrix)
    //{
    //    if (matrix.Rows != target.Dimension || matrix.Columns != source.Dimension)
    //    {
    //        throw new ArgumentOutOfRangeException("matrix", "Матрица должна иметь подходящий размер.");
    //    }
    //    Import.RealVector_Transform(target.Handle, source.Handle, matrix.Handle);
    //}

    /// <summary>
    /// Создаёт копию вектора.
    /// </summary>
    /// <returns>
    /// Копия вектора.
    /// </returns>
    public RealVector Clone()
    {
        return GetSubVector(0, Length);
    }

    ///// <summary>
    ///// Выполняет свёртку двух векторов.
    ///// </summary>
    ///// <param name="result">
    ///// Результат операции.
    ///// </param>
    ///// <param name="left">
    ///// Левый операнд.
    ///// </param>
    ///// <param name="right">
    ///// Правый операнд.
    ///// </param>
    ///// <exception cref="ArgumentException">
    ///// Происходит в случае, если размерности векторов не совпадают.
    ///// </exception>
    ///// <exception cref="InvalidOperationException">
    ///// Происходит в случае, если неуправляемая библиотека выдала исключение.
    ///// </exception>
    //public static void Convolution(RealVector result, RealVector left, RealVector right)
    //{
    //    if (left.Dimension != result.Dimension)
    //    {
    //        throw new ArgumentException("left", "Размерности векторов не совпадают.");
    //    }
    //    if (right.Dimension != result.Dimension)
    //    {
    //        throw new ArgumentException("right", "Размерности векторов не совпадают.");
    //    }
    //    Import.RealVector_Convolution(result.Handle, left.Handle, right.Handle);
    //}





    ////
    //// Сводка:
    ////     Определяет, входит ли элемент в коллекцию System.Collections.Generic.List`1.
    ////
    //// Параметры:
    ////   item:
    ////     Объект для поиска в System.Collections.Generic.List`1. Для ссылочных типов допускается
    ////     значение null.
    ////
    //// Возврат:
    ////     Значение true, если параметр item найден в коллекции System.Collections.Generic.List`1;
    ////     в противном случае — значение false.
    //public bool Contains(T item);

    ////
    //// Сводка:
    ////     Преобразует элементы текущего списка System.Collections.Generic.List`1 в другой
    ////     тип и возвращает список преобразованных элементов.
    ////
    //// Параметры:
    ////   converter:
    ////     Делегат System.Converter`2, преобразующий каждый элемент из одного типа в другой.
    ////
    //// Параметры типа:
    ////   TOutput:
    ////     Тип элементов массива назначения.
    ////
    //// Возврат:
    ////     Список System.Collections.Generic.List`1 с элементами конечного типа, преобразованными
    ////     из текущего списка System.Collections.Generic.List`1.
    ////
    //// Исключения:
    ////   T:System.ArgumentNullException:
    ////     converter — null.
    //public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter);

    ////
    //// Сводка:
    ////     Копирует System.Collections.Generic.List`1 целиком в совместимый одномерный массив,
    ////     начиная с указанного индекса конечного массива.
    ////
    //// Параметры:
    ////   array:
    ////     Одномерный массив System.Array, в который копируются элементы из интерфейса System.Collections.Generic.List`1.
    ////     Массив System.Array должен иметь индексацию, начинающуюся с нуля.
    ////
    ////   arrayIndex:
    ////     Отсчитываемый от нуля индекс в массиве array, указывающий начало копирования.
    ////
    //// Исключения:
    ////   T:System.ArgumentNullException:
    ////     array — null.
    ////
    ////   T:System.ArgumentOutOfRangeException:
    ////     Значение параметра arrayIndex меньше 0.
    ////
    ////   T:System.ArgumentException:
    ////     Число элементов в исходной коллекции System.Collections.Generic.List`1 больше
    ////     доступного места от положения, заданного значением параметра arrayIndex, до конца
    ////     массива назначения array.
    //public void CopyTo(T[] array, int arrayIndex);

    ////
    //// Сводка:
    ////     Копирует диапазон элементов из списка System.Collections.Generic.List`1 в совместимый
    ////     одномерный массив, начиная с указанного индекса конечного массива.
    ////
    //// Параметры:
    ////   index:
    ////     Отсчитываемый от нуля индекс исходного списка System.Collections.Generic.List`1,
    ////     с которого начинается копирование.
    ////
    ////   array:
    ////     Одномерный массив System.Array, в который копируются элементы из интерфейса System.Collections.Generic.List`1.
    ////     Массив System.Array должен иметь индексацию, начинающуюся с нуля.
    ////
    ////   arrayIndex:
    ////     Отсчитываемый от нуля индекс в массиве array, указывающий начало копирования.
    ////
    ////   count:
    ////     Число элементов для копирования.
    ////
    //// Исключения:
    ////   T:System.ArgumentNullException:
    ////     array — null.
    ////
    ////   T:System.ArgumentOutOfRangeException:
    ////     Значение параметра index меньше 0. -или- Значение параметра arrayIndex меньше
    ////     0. -или- Значение параметра count меньше 0.
    ////
    ////   T:System.ArgumentException:
    ////     Значение параметра index больше или равно значению System.Collections.Generic.List`1.Count
    ////     исходного списка System.Collections.Generic.List`1. -или- Число элементов от
    ////     index до конца исходного списка System.Collections.Generic.List`1 больше доступного
    ////     места от положения, заданного значением параметра arrayIndex, до конца массива
    ////     назначения array.
    //public void CopyTo(int index, T[] array, int arrayIndex, int count);

    ////
    //// Сводка:
    ////     Копирует весь список System.Collections.Generic.List`1 в совместимый одномерный
    ////     массив, начиная с первого элемента целевого массива.
    ////
    //// Параметры:
    ////   array:
    ////     Одномерный массив System.Array, в который копируются элементы из интерфейса System.Collections.Generic.List`1.
    ////     Массив System.Array должен иметь индексацию, начинающуюся с нуля.
    ////
    //// Исключения:
    ////   T:System.ArgumentNullException:
    ////     array — null.
    ////
    ////   T:System.ArgumentException:
    ////     Число элементов в исходном массиве System.Collections.Generic.List`1 больше числа
    ////     элементов, которые может содержать массив назначения array.
    //public void CopyTo(T[] array);

    ////
    //// Сводка:
    ////     Определяет, содержит ли System.Collections.Generic.List`1 элементы, удовлетворяющие
    ////     условиям указанного предиката.
    ////
    //// Параметры:
    ////   match:
    ////     Делегат System.Predicate`1, определяющий условия поиска элементов.
    ////
    //// Возврат:
    ////     true, если System.Collections.Generic.List`1 содержит один или несколько элементов,
    ////     удовлетворяющих условиям указанного предиката, в противном случае — false.
    ////
    //// Исключения:
    ////   T:System.ArgumentNullException:
    ////     match — null.
    //public bool Exists(Predicate<T> match);

    ////
    //// Сводка:
    ////     Выполняет поиск элемента, удовлетворяющего условиям указанного предиката, и возвращает
    ////     первое найденное вхождение в пределах всего списка System.Collections.Generic.List`1.
    ////
    //// Параметры:
    ////   match:
    ////     Делегат System.Predicate`1, определяющий условия поиска элемента.
    ////
    //// Возврат:
    ////     Первый элемент, удовлетворяющий условиям указанного предиката, если такой элемент
    ////     найден; в противном случае — значение по умолчанию для типа T.
    ////
    //// Исключения:
    ////   T:System.ArgumentNullException:
    ////     match — null.
    //public T Find(Predicate<T> match);

    ////
    //// Сводка:
    ////     Извлекает все элементы, удовлетворяющие условиям указанного предиката.
    ////
    //// Параметры:
    ////   match:
    ////     Делегат System.Predicate`1, определяющий условия поиска элементов.
    ////
    //// Возврат:
    ////     Список System.Collections.Generic.List`1, содержащий все элементы, удовлетворяющие
    ////     условиям указанного предиката, если такие элементы найдены; в противном случае
    ////     — пустой список System.Collections.Generic.List`1.
    ////
    //// Исключения:
    ////   T:System.ArgumentNullException:
    ////     match — null.
    //public List<T> FindAll(Predicate<T> match);

    ////
    //// Сводка:
    ////     Выполняет поиск элемента, удовлетворяющего условиям указанного предиката, и возвращает
    ////     отсчитываемый от нуля индекс первого найденного вхождения в пределах всего списка
    ////     System.Collections.Generic.List`1.
    ////
    //// Параметры:
    ////   match:
    ////     Делегат System.Predicate`1, определяющий условия поиска элемента.
    ////
    //// Возврат:
    ////     Отсчитываемый от нуля индекс первого вхождения элемента, отвечающего условиям
    ////     предиката match, если такой элемент найден. В противном случае значение –1.
    ////
    //// Исключения:
    ////   T:System.ArgumentNullException:
    ////     match — null.
    //public int FindIndex(Predicate<T> match);

    ////
    //// Сводка:
    ////     Выполняет поиск элемента, удовлетворяющего условиям указанного предиката, и возвращает
    ////     отсчитываемый от нуля индекс первого вхождения в диапазоне элементов списка System.Collections.Generic.List`1,
    ////     начиная с заданного индекса и заканчивая последним элементом.
    ////
    //// Параметры:
    ////   startIndex:
    ////     Индекс (с нуля) начальной позиции поиска.
    ////
    ////   match:
    ////     Делегат System.Predicate`1, определяющий условия поиска элемента.
    ////
    //// Возврат:
    ////     Отсчитываемый от нуля индекс первого вхождения элемента, отвечающего условиям
    ////     предиката match, если такой элемент найден. В противном случае значение –1.
    ////
    //// Исключения:
    ////   T:System.ArgumentNullException:
    ////     match — null.
    ////
    ////   T:System.ArgumentOutOfRangeException:
    ////     startIndex находится вне диапазона допустимых индексов для System.Collections.Generic.List`1.
    //public int FindIndex(int startIndex, Predicate<T> match);

    ////
    //// Сводка:
    ////     Выполняет поиск элемента, удовлетворяющего условиям указанного предиката, и возвращает
    ////     отсчитываемый от нуля индекс первого вхождения в диапазоне элементов списка System.Collections.Generic.List`1,
    ////     начинающемся с заданного индекса и содержащем указанное число элементов.
    ////
    //// Параметры:
    ////   startIndex:
    ////     Индекс (с нуля) начальной позиции поиска.
    ////
    ////   count:
    ////     Число элементов в диапазоне, в котором выполняется поиск.
    ////
    ////   match:
    ////     Делегат System.Predicate`1, определяющий условия поиска элемента.
    ////
    //// Возврат:
    ////     Отсчитываемый от нуля индекс первого вхождения элемента, отвечающего условиям
    ////     предиката match, если такой элемент найден. В противном случае значение –1.
    ////
    //// Исключения:
    ////   T:System.ArgumentNullException:
    ////     match — null.
    ////
    ////   T:System.ArgumentOutOfRangeException:
    ////     startIndex находится вне диапазона допустимых индексов для System.Collections.Generic.List`1.
    ////     -или- Значение параметра count меньше 0. -или- startIndex и count не указывают
    ////     допустимый раздел в System.Collections.Generic.List`1.
    //public int FindIndex(int startIndex, int count, Predicate<T> match);

    ////
    //// Сводка:
    ////     Выполняет поиск элемента, удовлетворяющего условиям указанного предиката, и возвращает
    ////     последнее найденное вхождение в пределах всего списка System.Collections.Generic.List`1.
    ////
    //// Параметры:
    ////   match:
    ////     Делегат System.Predicate`1, определяющий условия поиска элемента.
    ////
    //// Возврат:
    ////     Последний элемент, удовлетворяющий условиям указанного предиката, если такой
    ////     элемент найден; в противном случае — значение по умолчанию для типа T.
    ////
    //// Исключения:
    ////   T:System.ArgumentNullException:
    ////     match — null.
    //public T FindLast(Predicate<T> match);

    ////
    //// Сводка:
    ////     Выполняет поиск элемента, удовлетворяющего условиям указанного предиката, и возвращает
    ////     отсчитываемый от нуля индекс последнего найденного вхождения в пределах всего
    ////     списка System.Collections.Generic.List`1.
    ////
    //// Параметры:
    ////   match:
    ////     Делегат System.Predicate`1, определяющий условия поиска элемента.
    ////
    //// Возврат:
    ////     Отсчитываемый от нуля индекс последнего вхождения элемента, удовлетворяющего
    ////     условиям предиката match, если такой элемент найден; в противном случае — значение
    ////     –1.
    ////
    //// Исключения:
    ////   T:System.ArgumentNullException:
    ////     match — null.
    //public int FindLastIndex(Predicate<T> match);

    ////
    //// Сводка:
    ////     Выполняет поиск элемента, удовлетворяющего условиям указанного предиката, и возвращает
    ////     отсчитываемый от нуля индекс последнего вхождения в диапазоне элементов списка
    ////     System.Collections.Generic.List`1, начиная с первого элемента и заканчивая элементом
    ////     с заданным индексом.
    ////
    //// Параметры:
    ////   startIndex:
    ////     Индекс (с нуля) начала диапазона поиска в обратном направлении.
    ////
    ////   match:
    ////     Делегат System.Predicate`1, определяющий условия поиска элемента.
    ////
    //// Возврат:
    ////     Отсчитываемый от нуля индекс последнего вхождения элемента, удовлетворяющего
    ////     условиям предиката match, если такой элемент найден; в противном случае — значение
    ////     –1.
    ////
    //// Исключения:
    ////   T:System.ArgumentNullException:
    ////     match — null.
    ////
    ////   T:System.ArgumentOutOfRangeException:
    ////     startIndex находится вне диапазона допустимых индексов для System.Collections.Generic.List`1.
    //public int FindLastIndex(int startIndex, Predicate<T> match);

    ////
    //// Сводка:
    ////     Выполняет поиск элемента, удовлетворяющего условиям указанного предиката, и возвращает
    ////     отсчитываемый от нуля индекс последнего вхождения в диапазоне элементов списка
    ////     System.Collections.Generic.List`1, содержащем указанное число элементов и заканчивающемся
    ////     элементом с заданным индексом.
    ////
    //// Параметры:
    ////   startIndex:
    ////     Индекс (с нуля) начала диапазона поиска в обратном направлении.
    ////
    ////   count:
    ////     Число элементов в диапазоне, в котором выполняется поиск.
    ////
    ////   match:
    ////     Делегат System.Predicate`1, определяющий условия поиска элемента.
    ////
    //// Возврат:
    ////     Отсчитываемый от нуля индекс последнего вхождения элемента, удовлетворяющего
    ////     условиям предиката match, если такой элемент найден; в противном случае — значение
    ////     –1.
    ////
    //// Исключения:
    ////   T:System.ArgumentNullException:
    ////     match — null.
    ////
    ////   T:System.ArgumentOutOfRangeException:
    ////     startIndex находится вне диапазона допустимых индексов для System.Collections.Generic.List`1.
    ////     -или- Значение параметра count меньше 0. -или- startIndex и count не указывают
    ////     допустимый раздел в System.Collections.Generic.List`1.
    //public int FindLastIndex(int startIndex, int count, Predicate<T> match);

    ////
    //// Сводка:
    ////     Выполняет указанное действие с каждым элементом списка System.Collections.Generic.List`1.
    ////
    //// Параметры:
    ////   action:
    ////     Делегат System.Action`1, выполняемый для каждого элемента списка System.Collections.Generic.List`1.
    ////
    //// Исключения:
    ////   T:System.ArgumentNullException:
    ////     action — null.
    ////
    ////   T:System.InvalidOperationException:
    ////     Элемент в коллекции изменен.
    //public void ForEach(Action<T> action);

    ////
    //// Сводка:
    ////     Выполняет поиск указанного объекта и возвращает отсчитываемый от нуля индекс
    ////     первого вхождения в диапазоне элементов списка System.Collections.Generic.List`1,
    ////     начинающемся с заданного индекса и содержащем указанное число элементов.
    ////
    //// Параметры:
    ////   item:
    ////     Объект для поиска в System.Collections.Generic.List`1. Для ссылочных типов допускается
    ////     значение null.
    ////
    ////   index:
    ////     Индекс (с нуля) начальной позиции поиска. Значение 0 (ноль) действительно в пустом
    ////     списке.
    ////
    ////   count:
    ////     Число элементов в диапазоне, в котором выполняется поиск.
    ////
    //// Возврат:
    ////     Отсчитываемый от нуля индекс первого вхождения item в диапазоне элементов списка
    ////     System.Collections.Generic.List`1, который начинается с позиции index и содержит
    ////     count элементов, если искомый объект найден; в противном случае значение –1.
    ////
    //// Исключения:
    ////   T:System.ArgumentOutOfRangeException:
    ////     index находится вне диапазона допустимых индексов для System.Collections.Generic.List`1.
    ////     -или- Значение параметра count меньше 0. -или- index и count не указывают допустимый
    ////     раздел в System.Collections.Generic.List`1.
    //public int IndexOf(T item, int index, int count);

    ////
    //// Сводка:
    ////     Осуществляет поиск указанного объекта и возвращает отсчитываемый от нуля индекс
    ////     первого вхождения в диапазоне элементов списка System.Collections.Generic.List`1,
    ////     начиная с заданного индекса и до последнего элемента.
    ////
    //// Параметры:
    ////   item:
    ////     Объект для поиска в System.Collections.Generic.List`1. Для ссылочных типов допускается
    ////     значение null.
    ////
    ////   index:
    ////     Индекс (с нуля) начальной позиции поиска. Значение 0 (ноль) действительно в пустом
    ////     списке.
    ////
    //// Возврат:
    ////     Отсчитываемый от нуля индекс первого вхождения элемента item в диапазоне элементов
    ////     списка System.Collections.Generic.List`1, начиная с позиции index и до конца
    ////     списка, если элемент найден; в противном случае значение –1.
    ////
    //// Исключения:
    ////   T:System.ArgumentOutOfRangeException:
    ////     index находится вне диапазона допустимых индексов для System.Collections.Generic.List`1.
    //public int IndexOf(T item, int index);

    ////
    //// Сводка:
    ////     Осуществляет поиск указанного объекта и возвращает отсчитываемый от нуля индекс
    ////     первого вхождения, найденного в пределах всего списка System.Collections.Generic.List`1.
    ////
    //// Параметры:
    ////   item:
    ////     Объект для поиска в System.Collections.Generic.List`1. Для ссылочных типов допускается
    ////     значение null.
    ////
    //// Возврат:
    ////     Индекс (с нуля) первого вхождения параметра item, если оно найдено в коллекции
    ////     System.Collections.Generic.List`1; в противном случае -1.
    //public int IndexOf(T item);

    ////
    //// Сводка:
    ////     Осуществляет поиск указанного объекта и возвращает отсчитываемый от нуля индекс
    ////     последнего вхождения, найденного в пределах всего списка System.Collections.Generic.List`1.
    ////
    //// Параметры:
    ////   item:
    ////     Объект для поиска в System.Collections.Generic.List`1. Для ссылочных типов допускается
    ////     значение null.
    ////
    //// Возврат:
    ////     Отсчитываемый от нуля индекс последнего вхождения item в пределах всего списка
    ////     System.Collections.Generic.List`1, если элемент найден; в противном случае значение
    ////     –1.
    //public int LastIndexOf(T item);

    ////
    //// Сводка:
    ////     Осуществляет поиск указанного объекта и возвращает отсчитываемый от нуля индекс
    ////     последнего вхождения в диапазоне элементов списка System.Collections.Generic.List`1,
    ////     начиная с первого элемента и до позиции с заданным индексом.
    ////
    //// Параметры:
    ////   item:
    ////     Объект для поиска в System.Collections.Generic.List`1. Для ссылочных типов допускается
    ////     значение null.
    ////
    ////   index:
    ////     Индекс (с нуля) начала диапазона поиска в обратном направлении.
    ////
    //// Возврат:
    ////     Отсчитываемый от нуля индекс последнего вхождения элемента item в диапазоне элементов
    ////     списка System.Collections.Generic.List`1, начиная с первого элемента и до позиции
    ////     index, если элемент найден; в противном случае значение -1.
    ////
    //// Исключения:
    ////   T:System.ArgumentOutOfRangeException:
    ////     index находится вне диапазона допустимых индексов для System.Collections.Generic.List`1.
    //public int LastIndexOf(T item, int index);

    ////
    //// Сводка:
    ////     Выполняет поиск указанного объекта и возвращает отсчитываемый от нуля индекс
    ////     последнего вхождения в диапазоне элементов списка System.Collections.Generic.List`1,
    ////     содержащем указанное число элементов и заканчивающемся в позиции с указанным
    ////     индексом.
    ////
    //// Параметры:
    ////   item:
    ////     Объект для поиска в System.Collections.Generic.List`1. Для ссылочных типов допускается
    ////     значение null.
    ////
    ////   index:
    ////     Индекс (с нуля) начала диапазона поиска в обратном направлении.
    ////
    ////   count:
    ////     Число элементов в диапазоне, в котором выполняется поиск.
    ////
    //// Возврат:
    ////     Отсчитываемый от нуля индекс последнего вхождения item в диапазоне элементов
    ////     списка System.Collections.Generic.List`1, состоящем из count элементов и заканчивающемся
    ////     в позиции index, если элемент найден. В противном случае значение –1.
    ////
    //// Исключения:
    ////   T:System.ArgumentOutOfRangeException:
    ////     index находится вне диапазона допустимых индексов для System.Collections.Generic.List`1.
    ////     -или- Значение параметра count меньше 0. -или- index и count не указывают допустимый
    ////     раздел в System.Collections.Generic.List`1.
    //public int LastIndexOf(T item, int index, int count);



    ////
    //// Сводка:
    ////     Сортирует элементы в диапазоне элементов списка System.Collections.Generic.List`1
    ////     с помощью указанной функции сравнения.
    ////
    //// Параметры:
    ////   index:
    ////     Индекс (с нуля) начала диапазона, который требуется отсортировать.
    ////
    ////   count:
    ////     Длина диапазона сортировки.
    ////
    ////   comparer:
    ////     Реализация System.Collections.Generic.IComparer`1, которую следует использовать
    ////     при сравнении элементов, или null, если должна использоваться функция сравнения
    ////     по умолчанию System.Collections.Generic.Comparer`1.Default.
    ////
    //// Исключения:
    ////   T:System.ArgumentOutOfRangeException:
    ////     Значение параметра index меньше 0. -или- Значение параметра count меньше 0.
    ////
    ////   T:System.ArgumentException:
    ////     index и count не указывают допустимый диапазон в System.Collections.Generic.List`1.
    ////     -или- Реализация comparer вызвала ошибку во время сортировки. Например, comparer
    ////     может не возвратить 0 при сравнении элемента с самим собой.
    ////
    ////   T:System.InvalidOperationException:
    ////     comparer является null, и функция сравнения по умолчанию System.Collections.Generic.Comparer`1.Default
    ////     не может найти реализацию универсального интерфейса System.IComparable`1 или
    ////     интерфейса System.IComparable для типа T.
    //public void Sort(int index, int count, IComparer<T> comparer);

    ////
    //// Сводка:
    ////     Сортирует элементы во всем списке System.Collections.Generic.List`1 с использованием
    ////     указанного System.Comparison`1.
    ////
    //// Параметры:
    ////   comparison:
    ////     System.Comparison`1, используемый при сравнении элементов.
    ////
    //// Исключения:
    ////   T:System.ArgumentNullException:
    ////     comparison — null.
    ////
    ////   T:System.ArgumentException:
    ////     Реализация comparison вызвала ошибку во время сортировки. Например, comparison
    ////     может не возвратить 0 при сравнении элемента с самим собой.
    //public void Sort(Comparison<T> comparison);


    /// <summary>
    /// Сортирует все элементы вектора в порядке возрастания.
    /// </summary>
    public void Sort()
    {
        void sortCore(double* series, int length, double* temporal)
        {
            if (length <= 1)
            {
                return;
            }
            int partition = length >> 1;
            sortCore(series, partition, temporal);
            sortCore(series + partition, length - partition, temporal);

            double* second = series + partition;

            int secondLength = length - partition;
            int firstIndex = 0;
            int secondIndex = 0;
            double* range = temporal;

            while (firstIndex != partition && secondIndex != secondLength)
            {
                if (series[firstIndex] <= second[secondIndex])
                {
                    *(range++) = series[firstIndex++];
                }
                else
                {
                    *(range++) = second[secondIndex++];
                }
            }

            if (firstIndex != partition)
            {
                MemoryManager.Copy(new IntPtr(range), new IntPtr(series + firstIndex), (partition - firstIndex) * sizeof(double));
            }
            else if (secondIndex != secondLength)
            {
                MemoryManager.Copy(new IntPtr(range), new IntPtr(second + secondIndex), (secondLength - secondIndex) * sizeof(double));
            }
            MemoryManager.Copy(new IntPtr(series), new IntPtr(temporal), (partition + secondLength) * sizeof(double));
        }

        void sortBase(double* series, int length)
        {
            if (length == 0)
            {
                return;
            }

            double* temporal = (double*)MemoryManager.Alloc(length * sizeof(double));
            try
            {
                sortCore(series, length, temporal);
            }
            finally
            {
                MemoryManager.Free(new IntPtr(temporal));
            }
        }

        sortBase(_Items, Length);
    }

    ////
    //// Сводка:
    ////     Сортирует элементы во всем списке System.Collections.Generic.List`1 с помощью
    ////     указанной функции сравнения.
    ////
    //// Параметры:
    ////   comparer:
    ////     Реализация System.Collections.Generic.IComparer`1, которую следует использовать
    ////     при сравнении элементов, или null, если должна использоваться функция сравнения
    ////     по умолчанию System.Collections.Generic.Comparer`1.Default.
    ////
    //// Исключения:
    ////   T:System.InvalidOperationException:
    ////     comparer является null, и функция сравнения по умолчанию System.Collections.Generic.Comparer`1.Default
    ////     не может найти реализацию универсального интерфейса System.IComparable`1 или
    ////     интерфейса System.IComparable для типа T.
    ////
    ////   T:System.ArgumentException:
    ////     Реализация comparer вызвала ошибку во время сортировки. Например, comparer может
    ////     не возвратить 0 при сравнении элемента с самим собой.
    //public void Sort(IComparer<T> comparer);

    /// <summary>
    /// Копирует элементы вектора в новый массив.
    /// </summary>
    /// <returns>
    /// Массив, содержащий копии элементов вектора.
    /// </returns>
    public double[] ToArray()
    {
        int length = Length;
        double[] array = new double[length];
        Marshal.Copy(Pointer, array, 0, length);
        return array;
    }

    ////
    //// Сводка:
    ////     Определяет, все ли элементы списка System.Collections.Generic.List`1 удовлетворяют
    ////     условиям указанного предиката.
    ////
    //// Параметры:
    ////   match:
    ////     Делегат System.Predicate`1, определяющий условия, проверяемые для элементов.
    ////
    //// Возврат:
    ////     true, если каждый элемент списка System.Collections.Generic.List`1 удовлетворяет
    ////     условиям заданного предиката, в противном случае — false. Если в списке нет элементов,
    ////     возвращается true.
    ////
    //// Исключения:
    ////   T:System.ArgumentNullException:
    ////     match — null.
    //public bool TrueForAll(Predicate<T> match);


    /// <summary>
    /// Вызывает событие <see cref="Vector.PointerChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected override void OnPointerChanged(EventArgs e)
    {
        _Items = (double*)Pointer.ToPointer();
        base.OnPointerChanged(e);
    }


    private double Real_StandardNormalQuantile(double argument)
    {
        return 1.4142135623730950488016887242097 * Real_InverseErrorFunction(2 * argument - 1);
    }

    private static double Real_ErrorFunction(double argument)
    {
        return SpecialFunction.erf(argument);
    }

    private double Real_InverseErrorFunction(double argument)
    {
        const double epsilon = 2 * double.Epsilon;
        const double maxArgument = 0.5 * double.MaxValue;
        if (argument < 0) return -Real_InverseErrorFunction(-argument);

        double leftArgument = 0;
        double rightArgument = maxArgument;
        double centerArgument = 0.5 * (leftArgument + rightArgument);
        double leftValue = Real_ErrorFunction(leftArgument) - argument;
        double rightValue = Real_ErrorFunction(rightArgument) - argument;
        double centerValue = Real_ErrorFunction(centerArgument) - argument;

        if (rightValue < 0) return rightArgument;
        while (centerValue > epsilon || centerValue < -epsilon)
        {
            if (leftValue < 0 && centerValue < 0)
            {
                leftArgument = centerArgument;
                leftValue = Real_ErrorFunction(leftArgument) - argument;
            }
            else
            {
                rightArgument = centerArgument;
                //rightValue = Real_ErrorFunction(rightArgument) - argument;
            }
            centerArgument = 0.5 * (leftArgument + rightArgument);
            centerValue = Real_ErrorFunction(centerArgument) - argument;
        }
        return centerArgument;
    }


    static void RealSeries_Sort_Core(double* series, int length, double* temporal)
    {
        if (length <= 1) return;
        int partition = length >> 1;
        RealSeries_Sort_Core(series, partition, temporal);
        RealSeries_Sort_Core(series + partition, length - partition, temporal);
        double* second = series + partition;
        int secondLength = length - partition;
        int firstIndex = 0;
        int secondIndex = 0;
        double* range = temporal;
        while (firstIndex != partition && secondIndex != secondLength)
        {
            if (series[firstIndex] <= second[secondIndex])
            {
                *(range++) = series[firstIndex++];
            }
            else
            {
                *(range++) = second[secondIndex++];
            }
        }
        if (firstIndex != partition)
        {
            MemoryManager.Copy(new IntPtr(range), new IntPtr(series + firstIndex), (partition - firstIndex) * sizeof(double));
        }
        else if (secondIndex != secondLength)
        {
            MemoryManager.Copy(new IntPtr(range), new IntPtr(second + secondIndex), (secondLength - secondIndex) * sizeof(double));
        }
        MemoryManager.Copy(new IntPtr(series), new IntPtr(temporal), (partition + secondLength) * sizeof(double));
    }

    static void RealSeries_Sort(double* series, int length)
    {
        double* temporal = (double*)MemoryManager.Alloc(length * sizeof(double));
        try
        {
            RealSeries_Sort_Core(series, length, temporal);
        }
        finally
        {
            MemoryManager.Free(new IntPtr(temporal));
        }
    }

    /// <summary>
    /// Выполняет операцию поточечного сложения.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="rigth">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="left"/> передана пустая сслыка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="rigth"/> передана пустая сслыка.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Переданы векторы различной длины.
    /// </exception>
    public static RealVector operator + (RealVector left, RealVector rigth)
    {
        if (left is null)
        {
            throw new ArgumentNullException(nameof(left), "Передана пустая ссылка.");
        }
        if (rigth is null)
        {
            throw new ArgumentNullException(nameof(rigth), "Передана пустая ссылка.");
        }
        int length = left.Length;
        if (length != rigth.Length)
        {
            throw new ArgumentException("Переданы векторы различной длины.");
        }
        var result = new RealVector(length);
        if (length != 0)
        {
            double* leftData = left._Items;
            double* rigthData = rigth._Items;
            double* resultData = result._Items;
            for (int i = 0; i != length; ++i)
            {
                resultData[i] = leftData[i] + rigthData[i];
            }
        }
        return result;
    }

    /// <summary>
    /// Выполняет операцию поточечного вычитания.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="rigth">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="left"/> передана пустая сслыка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="rigth"/> передана пустая сслыка.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Переданы векторы различной длины.
    /// </exception>
    public static RealVector operator -(RealVector left, RealVector rigth)
    {
        if (left is null)
        {
            throw new ArgumentNullException(nameof(left), "Передана пустая ссылка.");
        }
        if (rigth is null)
        {
            throw new ArgumentNullException(nameof(rigth), "Передана пустая ссылка.");
        }
        int length = left.Length;
        if (length != rigth.Length)
        {
            throw new ArgumentException("Переданы векторы различной длины.");
        }
        var result = new RealVector(length);
        if (length != 0)
        {
            double* leftData = left._Items;
            double* rigthData = rigth._Items;
            double* resultData = result._Items;
            for (int i = 0; i != length; ++i)
            {
                resultData[i] = leftData[i] - rigthData[i];
            }
        }
        return result;
    }

    /// <summary>
    /// Выполняет операцию поточечного умножения.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="rigth">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="left"/> передана пустая сслыка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="rigth"/> передана пустая сслыка.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Переданы векторы различной длины.
    /// </exception>
    public static RealVector operator *(RealVector left, RealVector rigth)
    {
        if (left is null)
        {
            throw new ArgumentNullException(nameof(left), "Передана пустая ссылка.");
        }
        if (rigth is null)
        {
            throw new ArgumentNullException(nameof(rigth), "Передана пустая ссылка.");
        }
        int length = left.Length;
        if (length != rigth.Length)
        {
            throw new ArgumentException("Переданы векторы различной длины.");
        }
        var result = new RealVector(length);
        if (length != 0)
        {
            double* leftData = left._Items;
            double* rigthData = rigth._Items;
            double* resultData = result._Items;
            for (int i = 0; i != length; ++i)
            {
                resultData[i] = leftData[i] * rigthData[i];
            }
        }
        return result;
    }

    /// <summary>
    /// Выполняет операцию поточечного деления.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="rigth">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="left"/> передана пустая сслыка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="rigth"/> передана пустая сслыка.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Переданы векторы различной длины.
    /// </exception>
    public static RealVector operator /(RealVector left, RealVector rigth)
    {
        if (left is null)
        {
            throw new ArgumentNullException(nameof(left), "Передана пустая ссылка.");
        }
        if (rigth is null)
        {
            throw new ArgumentNullException(nameof(rigth), "Передана пустая ссылка.");
        }
        int length = left.Length;
        if (length != rigth.Length)
        {
            throw new ArgumentException("Переданы векторы различной длины.");
        }
        var result = new RealVector(length);
        if (length != 0)
        {
            double* leftData = left._Items;
            double* rigthData = rigth._Items;
            double* resultData = result._Items;
            for (int i = 0; i != length; ++i)
            {
                resultData[i] = leftData[i] / rigthData[i];
            }
        }
        return result;
    }
}
