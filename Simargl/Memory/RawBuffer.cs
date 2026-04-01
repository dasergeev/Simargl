using Simargl.Designing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Simargl.Memory;

/// <summary>
/// Представляет необработанный буфер.
/// </summary>
/// <typeparam name="T">
/// Тип элементов буфера.
/// </typeparam>
/// <remarks>
/// <para>
/// Необработанный буфер представляет собой циклический буфер,
/// который позволяет производить операции чтения из произвольной области
/// и записи в произвольную область.
/// </para>
/// <para>
/// Индексация в буфере находится в диапазоне от <see cref="long.MinValue"/> до <see cref="long.MaxValue"/>.
/// При обращении к внутренним данным все индексы автоматически приводятся к внутреннему диапазону
/// от 0 до <see cref="Length"/> - 1.
/// </para>
/// <para>
/// Буфер не является потокобезопасным.
/// </para>
/// </remarks>
public sealed class RawBuffer<T> :
    IEnumerable<T>,
    IReadOnlyList<T>,
    ICloneable
{
    /// <summary>
    /// Поле для хранения элементов буфера.
    /// </summary>
    readonly T[] _Items;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="length">
    /// Длина буфера.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="length"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="length"/> передано нулевое значение.
    /// </exception>
    public RawBuffer(int length)
    {
        //  Установка длины буфера.
        Length = IsPositive(length, nameof(length));

        //  Создание массива для хранения элементов.
        _Items = new T[length];
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="buffer">
    /// Массив начальных значений.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="buffer"/> передан пустой массив.
    /// </exception>
    RawBuffer(T[] buffer)
    {
        //  Проверка массива.
        IsNotEmpty(buffer, nameof(buffer));

        //  Определение длины буфера.
        Length = buffer.Length;

        //  Инициализация элементов буфера.
        _Items = (T[])buffer.Clone();
    }

    /// <summary>
    /// Возвращает или задаёт значение по указанному индексу.
    /// </summary>
    /// <param name="index">
    /// Индекс значения в буфере.
    /// </param>
    /// <returns>
    /// Значение с указанным индексом.
    /// </returns>
    /// <remarks>
    /// Индексатор не является потокобезопасным.
    /// </remarks>
    public T this[int index]
    {
        get => Read(index);
        set => Write(index, value);
    }

    /// <summary>
    /// Возвращает или задаёт значение по указанному индексу.
    /// </summary>
    /// <param name="index">
    /// Индекс значения в буфере.
    /// </param>
    /// <returns>
    /// Значение с указанным индексом.
    /// </returns>
    /// <remarks>
    /// Индексатор не является потокобезопасным.
    /// </remarks>
    public T this[long index]
    {
        get => Read(index);
        set => Write(index, value);
    }

    /// <summary>
    /// Возвращает длину буфера.
    /// </summary>
    /// <remarks>
    /// Свойство является потокобезопасным.
    /// </remarks>
    public int Length { get; }

    /// <summary>
    /// Выполняет запись значения в буфер по указанному индексу.
    /// </summary>
    /// <param name="index">
    /// Индекс значения в буфере.
    /// </param>
    /// <param name="value">
    /// Значение, которое необходимо записать в буфер.
    /// </param>
    /// <remarks>
    /// Метод не является потокобезопасным.
    /// </remarks>
    public void Write(long index, T value) => _Items[IndexNormalization(index)] = value;

    /// <summary>
    /// Выполняет чтение значения из буфера по указанному индексу.
    /// </summary>
    /// <param name="index">
    /// Индекс значения в буфере.
    /// </param>
    /// <returns>
    /// Прочитанное значение.
    /// </returns>
    /// <remarks>
    /// Метод не является потокобезопасным.
    /// </remarks>
    public T Read(long index) => _Items[IndexNormalization(index)];

    /// <summary>
    /// Выполняет запись массива значений в буфер.
    /// </summary>
    /// <param name="index">
    /// Индекс в буфере, начиная с которого необходимо произвести запись.
    /// </param>
    /// <param name="buffer">
    /// Массив, который содержит значения для записи.
    /// </param>
    /// <remarks>
    /// Метод не является потокобезопасным.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    public void Write(long index, params T[] buffer)
    {
        //  Проверка массива.
        IsNotNull(buffer, nameof(buffer));

        //  Запись значений в буфер.
        Write(index, buffer, 0, buffer.Length);
    }

    /// <summary>
    /// Выполняет запись массива значений в буфер.
    /// </summary>
    /// <param name="index">
    /// Индекс в буфере, начиная с которого необходимо произвести запись.
    /// </param>
    /// <param name="buffer">
    /// Массив, который содержит значения для записи.
    /// </param>
    /// <param name="offset">
    /// Смещение в массиве <paramref name="buffer"/>, с которого начинается копирование элементов в текущий буфер.
    /// </param>
    /// <param name="count">
    /// Количество элементов, которые необходимо записать в текущий буфер.
    /// </param>
    /// <remarks>
    /// Метод не является потокобезопасным.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="offset"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="offset"/> передано значение,
    /// которое превышает длину массива <paramref name="buffer"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Сумма значений параметров <paramref name="offset"/> и <paramref name="count"/>
    /// превышает длину массива <paramref name="buffer"/>.
    /// </exception>
    public void Write(long index, T[] buffer, int offset, int count)
    {
        //  Нормализация индекса в буфере.
        int localIndex = IndexNormalization(index);

        //  Проверка диапазона значений в массиве.
        IsRange(buffer, offset, count, nameof(buffer), nameof(offset), nameof(count));

        //  Если количество копируемых элементов равно нулю, то выполнение метода завершается.
        if (count == 0)
        {
            return;
        }

        if (count <= Length)
        {
            //  Запись в случае, если входной массив полностью умещается в буфере.
            if (localIndex + count <= Length)
            {
                //  Запись всех значений.
                Array.Copy(buffer, offset, _Items, localIndex, count);
            }
            else
            {
                //  Запись первых значений.
                Array.Copy(buffer, offset, _Items, localIndex, Length - localIndex);

                //  Запись последних значений.
                Array.Copy(buffer, offset + Length - localIndex, _Items, 0, count - Length + localIndex);
            }
        }
        else
        {
            //  Запись в случае, если входной массив полностью не умещается в буфер.
            if (localIndex == 0)
            {
                //  Запись всех значений.
                Array.Copy(buffer, offset + count - Length, _Items, 0, Length);
            }
            else
            {
                //  Запись первых значений.
                Array.Copy(buffer, offset + count - Length, _Items, localIndex, Length - localIndex);

                //  Запись последних значений.
                Array.Copy(buffer, offset + count - localIndex, _Items, 0, localIndex);
            }
        }
    }

    /// <summary>
    /// Выполняет чтение массива значений из буфера.
    /// </summary>
    /// <param name="index">
    /// Индекс в буфере, начиная с которого необходимо прочитать элементы.
    /// </param>
    /// <param name="count">
    /// Количество элементов, которые необходимо прочитать из текущего буфера.
    /// </param>
    /// <remarks>
    /// Метод не является потокобезопасным.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    public T[] Read(long index, int count)
    {
        //  Проверка количества элементов для чтения.
        IsNotNegative(count, nameof(count));

        //  Создание массива элементов.
        T[] buffer = new T[count];

        //  Чтение значений из буфера.
        Read(index, buffer, 0, count);

        //  Возвращение прочитанных значений.
        return buffer;
    }

    /// <summary>
    /// Выполняет чтение массива значений из буфера.
    /// </summary>
    /// <param name="index">
    /// Индекс в буфере, начиная с которого необходимо прочитать элементы.
    /// </param>
    /// <param name="buffer">
    /// Массив, в который необходимо поместить прочитанные элементы.
    /// </param>
    /// <param name="offset">
    /// Смещение в массиве <paramref name="buffer"/>, с которого начинается запись прочитанных элементов из текущего буфера.
    /// </param>
    /// <param name="count">
    /// Количество элементов, которые необходимо прочитать из текущего буфера.
    /// </param>
    /// <remarks>
    /// Метод не является потокобезопасным.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="offset"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="offset"/> передано значение,
    /// которое превышает длину массива <paramref name="buffer"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Сумма значений параметров <paramref name="offset"/> и <paramref name="count"/>
    /// превышает длину массива <paramref name="buffer"/>.
    /// </exception>
    public void Read(long index, T[] buffer, int offset, int count)
    {
        //  Нормализация индекса в буфере.
        int localIndex = IndexNormalization(index);

        //  Проверка диапазона значений в массиве.
        IsRange(buffer, offset, count, nameof(buffer), nameof(offset), nameof(count));

        //  Если количество элементов, которые необходимо прочитать, равно нулю, то выполнение метода завершается.
        if (count == 0)
        {
            return;
        }

        if (localIndex + count < Length)
        {
            //  Чтение всех значений.
            Array.Copy(_Items, localIndex, buffer, offset, count);
        }
        else
        {
            //  Чтение первых значений.
            {
                int copyLength = Length - localIndex;
                Array.Copy(_Items, localIndex, buffer, offset, copyLength);
                count -= copyLength;
                offset += copyLength;
            }

            //  Чтение целых блоков буфера.
            while (count >= Length)
            {
                Array.Copy(_Items, 0, buffer, offset, Length);
                offset += Length;
                count -= Length;
            }

            //  Чтение последних значений.
            if (count != 0)
            {
                Array.Copy(_Items, 0, buffer, offset, count);
            }
        }
    }

    /// <summary>
    /// Создаёт копию буфера.
    /// </summary>
    /// <returns>
    /// Копия буфера.
    /// </returns>
    /// <remarks>
    /// Метод не является потокобезопасным.
    /// </remarks>
    public RawBuffer<T> Clone() => new(_Items);

    /// <summary>
    /// Создаёт копию буфера.
    /// </summary>
    /// <returns>
    /// Копия буфера.
    /// </returns>
    object ICloneable.Clone() => Clone();

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    /// <remarks>
    /// Метод не является потокобезопасным.
    /// </remarks>
    public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)_Items).GetEnumerator();

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => _Items.GetEnumerator();

    /// <summary>
    /// Возвращает количество элементов коллекции.
    /// </summary>
    int IReadOnlyCollection<T>.Count => Length;

    /// <summary>
    /// Выполняет нормализацию индекса в буфере.
    /// </summary>
    /// <param name="index">
    /// Индекс в буфере.
    /// </param>
    /// <returns>
    /// Нормализованное значение индекса в буфере.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    int IndexNormalization(long index)
    {
        //  Получения остатка от деления на длину буфера.
        index %= Length;

        //  Проверка положительности остатка от деления.
        if (index < 0)
        {
            index += Length;
        }

        //  Возврат нормализованного значения.
        return (int)index;
    }
}
