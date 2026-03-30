using RailTest.Memory;
using System;
using System.Numerics;

namespace RailTest.Algebra;

/// <summary>
/// Представляет вектор с комплексными значениями.
/// </summary>
public unsafe class ComplexVector : Vector
{
    /// <summary>
    /// Поле для хранения указателя на область памяти, в которой расположены элементы вектора.
    /// </summary>
    private Complex* _Items;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public ComplexVector() :
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
    public ComplexVector(int length) :
        base(sizeof(Complex), length)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="vector">
    /// Вектор с действительными значениями.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="vector"/> передана пустая ссылка.
    /// </exception>
    public ComplexVector(RealVector vector) :
        this(vector is not null ? vector.Length : throw new ArgumentNullException(nameof(vector), "Передана пустая ссылка."))
    {
        int length = Length;
        double* source = (double*)vector.Pointer;
        Complex* target = _Items;
        for (int i = 0; i != length; ++i)
        {
            target[i] = source[i];
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
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано означение, большее или равное <see cref="Vector.Length"/>.
    /// </exception>
    public Complex this[int index]
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
            _Items[index] = value;
        }
    }

    /// <summary>
    /// Выполняет преобразование Фурье данного вектора.
    /// </summary>
    public void FourierTransform()
    {
        int length = Length;
        if (length > 1)
        {
            int twoDegreeLength = Natural_TwoDegree(length);
            Complex* series = null;
            try
            {
                if (length == twoDegreeLength)
                {
                    series = _Items;
                }
                else
                {
                    series = (Complex*)MemoryManager.Alloc(twoDegreeLength * (long)sizeof(Complex)).ToPointer();
                    MemoryManager.Copy(series, _Items, length * (long)sizeof(Complex));
                }

                Local_FastFourierTransform(series, twoDegreeLength);

                if (length != twoDegreeLength)
                {
                    MemoryManager.Copy(_Items, series, length * (long)sizeof(Complex));
                }
            }
            finally
            {
                if (length != twoDegreeLength)
                {
                    if (series != null)
                    {
                        try
                        {
                            MemoryManager.Free(series);
                        }
                        catch
                        {

                        }
                    }
                }
            }

        }
    }


    /// <summary>
    /// Выполняет обратное преобразование Фурье данного вектора.
    /// </summary>
    public void InverseFourierTransform()
    {
        int length = Length;
        if (length > 1)
        {
            int twoDegreeLength = Natural_TwoDegree(length);
            Complex* series = null;
            try
            {
                if (length == twoDegreeLength)
                {
                    series = _Items;
                }
                else
                {
                    series = (Complex*)MemoryManager.Alloc(twoDegreeLength * (long)sizeof(Complex)).ToPointer();
                    MemoryManager.Copy(series, _Items, length * (long)sizeof(Complex));
                }

                Local_FastInverseFourierTransform(series, twoDegreeLength);

                if (length != twoDegreeLength)
                {
                    MemoryManager.Copy(_Items, series, length * (long)sizeof(Complex));
                }
            }
            finally
            {
                if (length != twoDegreeLength)
                {
                    if (series != null)
                    {
                        try
                        {
                            MemoryManager.Free(series);
                        }
                        catch
                        {

                        }
                    }
                }
            }

        }
    }

    /// <summary>
    /// Возвращает вектор с действительными значениями.
    /// </summary>
    /// <returns>
    /// Вектор с действительными значениями.
    /// </returns>
    public RealVector ToRealVector()
    {
        int length = Length;
        RealVector vector = new(length);
        Complex* source = _Items;
        double* target = (double*)vector.Pointer.ToPointer();
        for (int i = 0; i != length; ++i)
        {
            target[i] = source[i].Real;
        }
        return vector;
    }

    /// <summary>
    /// Масштабирует значения всех элементов вектора.
    /// </summary>
    /// <param name="factor">
    /// Масштабный множитель.
    /// </param>
    public void Scale(Complex factor)
    {
        int length = Length;
        for (int i = 0; i != length; ++i)
        {
            _Items[i] *= factor;
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
    public static ComplexVector operator +(ComplexVector left, ComplexVector rigth)
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
        var result = new ComplexVector(length);
        if (length != 0)
        {
            Complex* leftData = left._Items;
            Complex* rigthData = rigth._Items;
            Complex* resultData = result._Items;
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
    public static ComplexVector operator -(ComplexVector left, ComplexVector rigth)
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
        var result = new ComplexVector(length);
        if (length != 0)
        {
            Complex* leftData = left._Items;
            Complex* rigthData = rigth._Items;
            Complex* resultData = result._Items;
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
    public static ComplexVector operator *(ComplexVector left, ComplexVector rigth)
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
        var result = new ComplexVector(length);
        if (length != 0)
        {
            Complex* leftData = left._Items;
            Complex* rigthData = rigth._Items;
            Complex* resultData = result._Items;
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
    public static ComplexVector operator /(ComplexVector left, ComplexVector rigth)
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
        var result = new ComplexVector(length);
        if (length != 0)
        {
            Complex* leftData = left._Items;
            Complex* rigthData = rigth._Items;
            Complex* resultData = result._Items;
            for (int i = 0; i != length; ++i)
            {
                resultData[i] = leftData[i] / rigthData[i];
            }
        }
        return result;
    }

    /// <summary>
    /// Вызывает событие <see cref="Vector.PointerChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected override void OnPointerChanged(EventArgs e)
    {
        _Items = (Complex*)Pointer.ToPointer();
        base.OnPointerChanged(e);
    }

    unsafe void RealSeries_FourierFiltering(double* series, int length, double sampling, double lowerFrequency, double highFrequency, bool isInversion)
    {
        Complex* target = null;
        try

        {
            int actualLength = Natural_TwoDegree(length);
            int halfLength = actualLength >> 1;
            target = (Complex*)MemoryManager.Alloc(actualLength * sizeof(Complex));
            RealSeries_ToComplex(target, series, length);
            ComplexSeries_FastFourierTransform(target, actualLength);
            double stepFrequency = sampling / (double)actualLength;

            try

            {
                if (isInversion)
                {
                    if (lowerFrequency <= 0)
                    {
                        target[0] = Complex.Zero;
                    }

                    for (int i = 1; i <= halfLength; ++i)
                    {
                        double frequency = stepFrequency * (double)i;
                        if (frequency >= lowerFrequency && frequency <= highFrequency)
                        {
                            target[i] = Complex.Zero;
                            target[actualLength - i] = Complex.Zero;
                        }
                    }
                }
                else
                {
                    if (lowerFrequency > 0)
                    {
                        target[0] = Complex.Zero;
                    }
                    for (int i = 1; i <= halfLength; ++i)
                    {
                        double frequency = stepFrequency * (double)i;
                        if (frequency < lowerFrequency || frequency > highFrequency)
                        {
                            target[i] = Complex.Zero;
                            target[actualLength - i] = Complex.Zero;
                        }
                    }
                }
            }
            catch

            {

            }

            ComplexSeries_FastInverseFourierTransform(target, actualLength);
            ComplexSeries_ToReal(series, target, length);
        }
        finally

        {
            MemoryManager.Free(new IntPtr(target));
        }
    }

    unsafe void ComplexSeries_FastInverseFourierTransform(Complex* series, int length)
    {
        Local_FastInverseFourierTransform(series, length);
    }

    unsafe void ComplexSeries_ToReal(double* target, Complex* source, int length)
    {
        for (int i = 0; i != length; ++i)
        {
            target[i] = source[i].Real;
        }
    }


    unsafe void Local_FastFourierTransform(Complex* series, int length)
    {
        Complex root;
        {
            double angle = 6.283185307179586476925286766559 / (double)length;
            root = new Complex(Math.Cos(angle), Math.Sin(angle));
        }

        int stackIndex = 0;
        for (int step = length; step != 1; step >>= 1) ++stackIndex;

        Reposition(series, length);
        Complex* roots = null;
        try

        {
            roots = (Complex*)MemoryManager.Alloc(sizeof(Complex) * stackIndex);
            stackIndex = 0;
            for (int step = length; step != 1; step >>= 1)
            {
                roots[stackIndex++] = root;
                root *= root;
            }
            for (int step = 2; step <= length; step <<= 1)
            {
                root = roots[--stackIndex];
                for (int i = 0; i != length; i += step) MassifButterfly(series + i, step, &root);
            }
        }
        finally

        {
            MemoryManager.Free(new IntPtr(roots));
        }
    }

    unsafe static void Reposition(Complex* series, int size)
    {
        int length = 0;
        while (1 << length < size) ++length;

        for (int i = 0; i != size; ++i)
        {
            int j = Backwards(i, length);
            if (i <= j)
            {
                (series[i], series[j]) = (series[j], series[i]);
            }
        }
    }

    unsafe static void MassifButterfly(Complex* series, int size, Complex* w)
    {
        Complex power = new(1.0, 0.0);
        int n = size >> 1;
        for (int i = 0; i != n; ++i)
        {
            Butterfly(series + i, series + i + n, &power);
            Complex oldPower = power;
            power = oldPower * (*w);
        }
    }

    static int Backwards(int x, int length)
    {
        int result = 0;
        int bit = 1;
        int reverse = 1 << (length - 1);
        for (int i = 0; i < length && x != 0; ++i)
        {
            if ((x & bit) != 0)
            {
                result |= reverse;
                x &= ~bit;
            }
            bit <<= 1;
            reverse >>= 1;
        }
        return result;
    }

    unsafe static void Butterfly(Complex* x, Complex* y, Complex* w)
    {
        Complex p, q;
        p = *x;
        q = *y * (*w);
        *x = p + q;
        *y = p - q;
    }

    unsafe void Local_FastInverseFourierTransform(Complex* series, int length)
    {
        Conjugate(series, length);
        Local_FastFourierTransform(series, length);
        Conjugate(series, length);
        double factor = 1.0 / (double)length;
        for (int i = 0; i != length; ++i)
        {
            series[i] *= factor;
        }
    }

    unsafe static void Conjugate(Complex* series, int size)
    {
        for (int i = 0; i != size; ++i)
        {
            series[i] = new Complex(series[i].Real, -series[i].Imaginary);
        }
    }

    static int Natural_TwoDegree(int argument)
    {
        if (argument <= 0) return 0;
        int result = 1;
        while (argument > result) result <<= 1;
        return result;
    }

    unsafe void RealSeries_ToComplex(Complex* target, double* source, int length)
    {
        for (int i = 0; i != length; ++i)
        {
            target[i] = new Complex(source[i], 0);
        }
    }

    unsafe void ComplexSeries_FastFourierTransform(Complex* series, int length)
    {
        Local_FastFourierTransform(series, length);
    }

}
