using Simargl.Templates;
using System.Runtime.CompilerServices;
using Simargl.Designing.Utilities;

namespace Simargl.Algebra;

/// <summary>
/// Представляет вектор.
/// </summary>
/// <typeparam name="T">
/// Тип элемента вектора.
/// </typeparam>
public sealed class Vector<T> :
    IEnumerable<T>,
    ICloneable
{
    /// <summary>
    /// Поле для хранения массива элементов вектора.
    /// </summary>
    private T[] _Items;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public Vector()
    {
        //  Установка пустого массива элементов.
        _Items = [];
    }

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="length">
    /// Длина вектора.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="length"/> передано отрицательное значение.
    /// </exception>
    public Vector(int length)
    {
        //  Проверка длины вектора.
        IsNotNegative(length, nameof(length));

        //  Создание массива элементов.
        _Items = new T[length];
    }

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="length">
    /// Длина вектора.
    /// </param>
    /// <param name="initializer">
    /// Инициализатор элементов.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="length"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="initializer"/> передана пустая ссылка.
    /// </exception>
    public Vector(int length, Func<int, T> initializer) :
        this(length)
    {
        //  Проверка ссылки на инициализатор.
        IsNotNull(initializer, nameof(initializer));

        //  Перебор всех элементов.
        for (int i = 0; i < _Items.Length; i++)
        {
            //  Инициализация элемента.
            _Items[i] = initializer(i);
        }
    }

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="items">
    /// Элементы вектора.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="items"/> передана пустая ссылка.
    /// </exception>
    public Vector(params T[] items)
    {
        //  Установка массива элементов.
        _Items = IsNotNull(items, nameof(items));
    }

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="collection">
    /// Коллекция элементов.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="collection"/> передана пустая ссылка.
    /// </exception>
    public Vector(IEnumerable<T> collection)
    {
        //  Проверка коллекции.
        IsNotNull(collection, nameof(collection));

        //  Проверка массива.
        if (collection is T[] items)
        {
            //  Установка массива элементов.
            _Items = items;
        }
        else
        {
            //  Получение массива элементов.
            _Items = collection.ToArray();
        }
    }

    /// <summary>
    /// Выполняет операцию проверки на равенство.
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
    /// <exception cref="InvalidOperationException">
    /// Операция не поддерживается.
    /// </exception>
    public static bool operator ==(Vector<T> left, Vector<T> right)
    {
        //  Проверка ссылок на операнды.
        IsNotNull(left, nameof(left));
        IsNotNull(right, nameof(right));

        //  Проверка равенства ссылок.
        if (ReferenceEquals(left, right))
        {
            //  Один и тот же объект.
            return true;
        }

        //  Проверка длины операндов.
        if (left.Length != right.Length)
        {
            //  Длины векторов не совпадают.
            return false;
        }

        //  Выполнение операции.
        return BinaryOperations<T, T>.Equal(left._Items, right._Items);
    }

    /// <summary>
    /// Выполняет операцию проверки на неравенство.
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
    /// <exception cref="InvalidOperationException">
    /// Операция не поддерживается.
    /// </exception>
    public static bool operator !=(Vector<T> left, Vector<T> right)
    {
        //  Выполнение операции.
        return !(left == right);
    }

    /// <summary>
    /// Выполняет операцию сложения двух векторов.
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
    /// <exception cref="InvalidOperationException">
    /// Длины операндов <paramref name="left"/> и <paramref name="right"/> не совпадают.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Операция не поддерживается.
    /// </exception>
    public static Vector<T> operator +(Vector<T> left, Vector<T> right)
    {
        //  Проверка ссылок на операнды.
        IsNotNull(left, nameof(left));
        IsNotNull(right, nameof(right));

        //  Выполнение операции.
        return new(BinaryOperations<T, T, T>.Add(left._Items, right._Items));
    }

    /// <summary>
    /// Выполняет операцию вычитания двух векторов.
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
    /// <exception cref="InvalidOperationException">
    /// Длины операндов <paramref name="left"/> и <paramref name="right"/> не совпадают.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Операция не поддерживается.
    /// </exception>
    public static Vector<T> operator -(Vector<T> left, Vector<T> right)
    {
        //  Проверка ссылок на операнды.
        IsNotNull(left, nameof(left));
        IsNotNull(right, nameof(right));

        //  Выполнение операции.
        return new(BinaryOperations<T, T, T>.Subtract(left._Items, right._Items));
    }

    /// <summary>
    /// Выполняет операцию поточечного умножения двух векторов.
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
    /// <exception cref="InvalidOperationException">
    /// Длины операндов <paramref name="left"/> и <paramref name="right"/> не совпадают.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Операция не поддерживается.
    /// </exception>
    public static Vector<T> operator *(Vector<T> left, Vector<T> right)
    {
        //  Проверка ссылок на операнды.
        IsNotNull(left, nameof(left));
        IsNotNull(right, nameof(right));

        //  Выполнение операции.
        return new(BinaryOperations<T, T, T>.Multiply(left._Items, right._Items));
    }

    /// <summary>
    /// Выполняет операцию поточечного деления двух векторов.
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
    /// <exception cref="InvalidOperationException">
    /// Длины операндов <paramref name="left"/> и <paramref name="right"/> не совпадают.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Операция не поддерживается.
    /// </exception>
    public static Vector<T> operator /(Vector<T> left, Vector<T> right)
    {
        //  Проверка ссылок на операнды.
        IsNotNull(left, nameof(left));
        IsNotNull(right, nameof(right));

        //  Выполнение операции.
        return new(BinaryOperations<T, T, T>.Divide(left._Items, right._Items));
    }

    /// <summary>
    /// Возвращает или задаёт массив элементов вектора.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public T[] Items
    {
        get => _Items;
        set => _Items = IsNotNull(value, nameof(Items));
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
    public T this[int index]
    {
        get => _Items[IsIndex(index, _Items.Length, nameof(index))];
        set => _Items[IsIndex(index, _Items.Length, nameof(index))] = value;
    }

    /// <summary>
    /// Возвращает или задаёт длину вектора.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано отрицательное значение.
    /// </exception>
    public int Length
    {
        get => _Items.Length;
        set
        {
            //  Проверка нового значения.
            IsNotNegative(value, nameof(Length));

            //  Проверка изменения значения.
            if (_Items.Length != value)
            {
                //  Изменение массива элементов.
                Array.Resize(ref _Items, value);
            }
        }
    }

    /// <summary>
    /// <para>Создаёт новый вектор из диапазона элементов текущего вектора.</para>
    /// <para>Диапазон начинается с элемента <paramref name="index"/> и продолжается до конца вектора.</para>
    /// </summary>
    /// <param name="index">
    /// Начальный индекс диапазона элементов.
    /// </param>
    /// <returns>
    /// Новый вектор.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано значение большее или равное <see cref="Vector{T}.Length"/>.
    /// </exception>
    public Vector<T> Subvector(int index) => Subvector(index, Length - index);

    /// <summary>
    /// Создаёт новый вектор из диапазона элементов текущего вектора.
    /// </summary>
    /// <param name="index">
    /// Начальный индекс диапазона элементов.
    /// </param>
    /// <param name="length">
    /// Число элементов.
    /// </param>
    /// <returns>
    /// Новый вектор.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано значение большее или равное <see cref="Vector{T}.Length"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="length"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="length"/> передано значение не соответствующее допустимому диапазону значений.
    /// </exception>
    public Vector<T> Subvector(int index, int length)
    {
        //  Проверка диапазона элементов.
        CheckRangeCore(index, length);

        //  Создание подвектора.
        Vector<T> subvector = new(length);

        //  Копирование элементов в подвектор.
        Array.Copy(_Items, index, subvector._Items, 0, length);

        //  Возврат подвектора.
        return subvector;
    }

    /// <summary>
    /// Создаёт копию текущего вектора.
    /// </summary>
    /// <returns>
    /// Копия текущего вектора.
    /// </returns>
    public Vector<T> Clone()
    {
        //  Возврат копии текущего вектора.
        return new((T[])_Items.Clone());
    }

    /// <summary>
    /// Создаёт копию текущего вектора.
    /// </summary>
    /// <returns>
    /// Копия текущего вектора.
    /// </returns>
    object ICloneable.Clone()
    {
        //  Возврат копии текущего вектора.
        return Clone();
    }

    /// <summary>
    /// Создаёт массив из элементов вектора.
    /// </summary>
    /// <returns>
    /// Массив из элементов вектора.
    /// </returns>
    public T[] ToArray()
    {
        //  Проверка длины вектора.
        if (Length == 0)
        {
            //  Возврат пустого массива.
            return Array.Empty<T>();
        }
        else
        {
            //  Создание массива.
            T[] items = new T[Length];

            //  Копирование элементов.
            Array.Copy(_Items, items, Length);

            //  Возврат массива.
            return items;
        }
    }

    /// <summary>
    /// Служит в качестве хэш-функции по умолчанию.
    /// </summary>
    /// <returns>
    /// Хэш-код для текущего объекта.
    /// </returns>
    public override int GetHashCode()
    {
        //  Возврат хэш-кода массива элементов.
        return _Items.GetHashCode();
    }

    /// <summary>
    /// Определяет, равен ли указанный объект текущему объекту.
    /// </summary>
    /// <param name="obj">
    /// Объект, который требуется сравнить с текущим объектом.
    /// </param>
    /// <returns>
    /// Значение <c>true</c> если указанный объект равен текущему объекту; в противном случае - значение <c>false</c>.
    /// </returns>
    public override bool Equals(object? obj)
    {
        //  Проверка ссылки на объект.
        if (obj is null)
        {
            return false;
        }

        //  Проверка типа объекта.
        if (obj is Vector<T> vector)
        {
            //  Проверка поэлементного равенства.
            return this == vector;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Возвращает перечислитель элементов вектора.
    /// </summary>
    /// <returns>
    /// Перечислитель элементов вектора.
    /// </returns>
    public IEnumerator<T> GetEnumerator()
    {
        //  Возврат перечислителя массива элементов.
        return ((IEnumerable<T>)_Items).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель элементов вектора.
    /// </summary>
    /// <returns>
    /// Перечислитель элементов вектора.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат перечислителя массива элементов.
        return _Items.GetEnumerator();
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
    /// которое равно или превышает значение <see cref="Vector{T}.Length"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private int CheckIndexCore(int index)
    {
        //  Проверка на отрицательность.
        IsNotNegative(index, nameof(index));

        //  Проверка на превышение максимального значения.
        IsLess(index, Length, nameof(index));

        //  Возврат проверенного значения.
        return index;
    }

    /// <summary>
    /// Выполняет проверку диапазона элементов.
    /// </summary>
    /// <param name="index">
    /// Начальный индекс диапазона элементов.
    /// </param>
    /// <param name="length">
    /// Число элементов.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано значение,
    /// которое равно или превышает значение <see cref="Vector{T}.Length"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="length"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="length"/> передано значение не соответствующее допустимому диапазону значений.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void CheckRangeCore(int index, int length)
    {
        //  Проверка начального индекса.
        CheckIndexCore(index);

        //  Проверка длины.
        IsNotNegative(length, nameof(length));

        //  Проверка диапазона.
        if (index + length > Length)
        {
            throw ExceptionsCreator.ArgumentOutOfRange(nameof(length));
        }
    }
}
