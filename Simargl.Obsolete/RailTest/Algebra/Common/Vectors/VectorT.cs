using System;
using System.Text;

namespace RailTest.Algebra;

/// <summary>
/// Представляет вектор.
/// </summary>
/// <typeparam name="T">
/// Тип элемента вектора.
/// </typeparam>
public class Vector<T> : ICloneable
{
    /// <summary>
    /// Поле для хранения операции сложения компонент.
    /// </summary>
    static readonly Func<T, T, T> _Addition;

    /// <summary>
    /// Инициализирует статические поля класса.
    /// </summary>
    static Vector()
    {
        _Addition = (T left, T right) => (T)((dynamic)left! + (dynamic)right!);
    }

    /// <summary>
    /// Поле для хранения массива компонент вектора.
    /// </summary>
    T[] _Items;

    /// <summary>
    /// Поле для хранения длины вектора.
    /// </summary>
    int _Length;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public Vector() : this(0)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="length">
    /// Длина вектора.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="length"/> передано отрицательное значение.
    /// </exception>
    public Vector(int length)
    {
        _Length = length >= 0 ? length : throw new ArgumentOutOfRangeException(nameof(length), "Передано отрицательное значение.");
        _Items = new T[length];
    }

    /// <summary>
    /// Возвращает или задаёт длину вектора.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано отрицательное значение.
    /// </exception>
    public int Length
    {
        get => _Length;
        set
        {
            if (_Length != value)
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Length", "Передано отрицательное значение.");
                }
                Array.Resize(ref _Items, value);
                _Length = value;
            }
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
    /// В параметре <paramref name="index"/> передано значение, большее или равное <see cref="Length"/>.
    /// </exception>
    public T this[int index]
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
    /// Выполняет опреацию сложения.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="left"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="right"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Переданы векторы различной длины.
    /// </exception>
    public static Vector<T> operator + (Vector<T> left, Vector<T> right)
    {
        if (left is null)
        {
            throw new ArgumentNullException(nameof(left), "Передана пустая ссылка.");
        }
        if (right is null)
        {
            throw new ArgumentNullException(nameof(right), "Передана пустая ссылка.");
        }
        int length = left._Length;
        if (length != right._Length)
        {
            throw new ArgumentException("Переданы векторы различной длины.");
        }
        Vector<T> result = new(length);
        T[] resultItems = result._Items;
        T[] leftItems = left._Items;
        T[] rightItems = right._Items;
        for (int i = 0; i != length; ++i)
        {
            resultItems[i] = _Addition(leftItems[i], rightItems[i]);
        }
        return result;
    }

    /// <summary>
    /// Создаёт копию вектора.
    /// </summary>
    /// <returns>
    /// Копия вектора.
    /// </returns>
    public Vector<T> Clone()
    {
        Vector<T> duplicate = new();
        if (_Length > 0)
        {
            duplicate._Items = (T[])_Items.Clone();
            duplicate._Length = _Length;
        }
        return duplicate;
    }

    /// <summary>
    /// Возвращает текстовое представление объекта.
    /// </summary>
    /// <returns>
    /// Текстовое представление объекта.
    /// </returns>
    public override string ToString()
    {
        StringBuilder text = new("[\n");
        int length = _Length;
        for (int i = 0; i != length; ++i)
        {
            text.Append(_Items[i]);
            text.Append('\n');
        }
        text.Append(']');
        return text.ToString();
    }


    /// <summary>
    /// Создаёт копию вектора.
    /// </summary>
    /// <returns>
    /// Копия вектора.
    /// </returns>
    object ICloneable.Clone()
    {
        return Clone();
    }
}
