using Simargl.Algebra;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Simargl.Analysis;

/// <summary>
/// Представляет дискретный временной сигнал.
/// </summary>
public sealed class Signal :
    IEnumerable<double>,
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
    /// Поле для хранения вектора значений сигнала.
    /// </summary>
    private Vector<double> _Vector;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="sampling">
    /// Частота дискретизации в Гц.
    /// </param>
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
    public Signal(double sampling)
    {
        //  Установка частоты дискретизации.
        _Sampling = IsSampling(sampling, nameof(sampling));

        //  Установка пустого вектора.
        _Vector = new();
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="sampling">
    /// Частота дискретизации в Гц.
    /// </param>
    /// <param name="vector">
    /// Вектор значений сигнала.
    /// </param>
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
    public Signal(double sampling, Vector<double> vector)
    {
        //  Установка частоты дискретизации.
        _Sampling = IsSampling(sampling, nameof(sampling));

        //  Установка вектора значений сигнала.
        _Vector = IsNotNull(vector, nameof(vector));
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
                _Sampling = IsSampling(value, nameof(Sampling));

                //  Вызов события об изменении значения.
                OnSamplingChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт вектор значений сигнала.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public Vector<double> Vector
    {
        get => _Vector;
        set => _Vector = IsNotNull(value, nameof(Vector));
    }

    /// <summary>
    /// Возвращает длительность сигнала в секундах.
    /// </summary>
    public double Duration => Vector.Length / Sampling;

    /// <summary>
    /// Возвращает или задаёт длину вектора значений сигнала.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано отрицательное значение.
    /// </exception>
    public int Length
    {
        get => _Vector.Length;
        set => _Vector.Length = value;
    }

    /// <summary>
    /// Возвращает или задаёт массив элементов сигнала.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public double[] Items
    {
        get => _Vector.Items;
        set => _Vector.Items = value;
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
        get => _Vector[index];
        set => _Vector[index] = value;
    }

    /// <summary>
    /// Создаёт копию текущего объекта.
    /// </summary>
    /// <returns>
    /// Копия текущего объекта.
    /// </returns>
    public Signal Clone()
    {
        return new(Sampling, Vector.Clone());
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

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<double> GetEnumerator()
    {
        return _Vector.GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_Vector).GetEnumerator();
    }

    /// <summary>
    /// Выполняет проверку значения частоты дискретизации.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано бесконечное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нулевое значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double IsSampling(double value, string? paramName)
    {
        //  Проверка на бесконечное значение.
        IsNotInfinity(value, paramName);

        //  Проверка на нечисловое значение.
        IsNotNaN(value, paramName);

        //  Проверка на отрицательное и нулевое значения.
        IsPositive(value, paramName);

        //  Возврат проверенного значения.
        return value;
    }
}
