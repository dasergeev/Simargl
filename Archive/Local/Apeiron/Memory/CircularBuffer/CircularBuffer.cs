using System.Collections;

namespace Apeiron.Memory;

/// <summary>
/// Представляет циклический буфер.
/// </summary>
/// <typeparam name="T">
/// Тип элементов буфера.
/// </typeparam>
/// <remarks>
/// Все методы класса являются потокобезопасными.
/// </remarks>
public class CircularBuffer<T> :
    IEnumerable<T>,
    ICloneable
{
    /// <summary>
    /// Происходит при изменении буфера.
    /// </summary>
    public event CircularBufferEventHandler? Changed
    {
        add
        {
            //  Блокировка объекта для синхронизации доступа к буферу.
            lock (SyncRoot)
            {
                //  Добавление обработчика события.
                _Changed += value;
            }
        }
        remove
        {
            //  Блокировка объекта для синхронизации доступа к буферу.
            lock (SyncRoot)
            {
                //  Удаление обработчика события.
                _Changed -= value;
            }
        }
    }

    /// <summary>
    /// Поле для хранения необработанного буфера.
    /// </summary>
    readonly RawBuffer<T> _RawBuffer;

    /// <summary>
    /// Поле для хранения начальной позиции.
    /// </summary>
    long _BeginPosition;

    /// <summary>
    /// Поле для хранения следующей за последней позиции.
    /// </summary>
    long _EndPosition;

    /// <summary>
    /// Поле для хранения обработчиков события <see cref="Changed"/>.
    /// </summary>
    CircularBufferEventHandler? _Changed;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="buffer">
    /// Кольцевой буфер, копия которого создаётся.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    CircularBuffer(CircularBuffer<T> buffer)
    {
        //  Проверка входного параметра.
        IsNotNull(buffer, nameof(buffer));

        //  Блокировка критического объекта.
        lock (buffer.SyncRoot)
        {
            //  Создание необработанного буфера.
            _RawBuffer = buffer._RawBuffer.Clone();

            //  Инициализация членов класса.
            SyncRoot = new object();
            Length = buffer.Length;
            _BeginPosition = buffer.BeginPosition;
            _EndPosition = buffer.EndPosition;
            _Changed = null;
        }
    }

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
    public CircularBuffer(int length)
    {
        //  Создание необработканного буфера.
        _RawBuffer = new RawBuffer<T>(length);

        //  Установка длины буфера.
        Length = length;

        //  Установка начальной позиции.
        _BeginPosition = 0;

        //  Установка следующей за последней позиции.
        _EndPosition = 0;

        //  Инициализация объекта, с помощью которого можно синхронизировать доступ к буферу.
        SyncRoot = new object();

        //  Инициализация обработчиков события изменения буфера.
        _Changed = null;
    }

    /// <summary>
    /// Возвращает значение из указанной позиции в буфере.
    /// </summary>
    /// <param name="position">
    /// Позиция в буфере.
    /// </param>
    /// <returns>
    /// Значение из указанной позиции.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="position"/> передано значение,
    /// которое меньше значения <see cref="BeginPosition"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="position"/> передано значение,
    /// которое равно или превышает значение <see cref="EndPosition"/>.
    /// </exception>
    public T this[long position] => Read(position);

    /// <summary>
    /// Возвращает объект, с помощью которого можно синхронизировать доступ к буферу.
    /// </summary>
    public object SyncRoot { get; }

    /// <summary>
    /// Возвращает длину буфера.
    /// </summary>
    public int Length { get; }

    /// <summary>
    /// Возвращает начальную позицию в буфере.
    /// </summary>
    public long BeginPosition
    {
        get
        {
            //  Блокировка объекта для синхронизации доступа к буферу.
            lock (SyncRoot)
            {
                //  Возврат начальной позиции.
                return _BeginPosition;
            }
        }
    }

    /// <summary>
    /// Возвращает следующую за последней позицию в буфере.
    /// </summary>
    public long EndPosition
    {
        get
        {
            //  Блокировка объекта для синхронизации доступа к буферу.
            lock (SyncRoot)
            {
                //  Возврат следующей за последней позиции.
                return _EndPosition;
            }
        }
    }

    /// <summary>
    /// Выполняет запись значения в буфер.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо записать в буфер.
    /// </param>
    /// <remarks>
    /// Метод является потокобезопасным.
    /// </remarks>
    /// <exception cref="OverflowException">
    /// Операция привела к переполнению:
    /// следующее значение позиции <see cref="EndPosition"/>
    /// превышает значение <see cref="long.MaxValue"/>.
    /// </exception>
    public void Write(T value)
    {
        //  Блокировка объекта для синхронизации доступа к буферу.
        lock (SyncRoot)
        {
            //  Проверка переполнения буфера.
            if (_EndPosition == long.MaxValue)
            {
                //  Операция привела к переполнению.
                throw Exceptions.OperationOverflow();
            }

            //  Запись в необработанный буфер.
            _RawBuffer.Write(_EndPosition, value);

            //  Расчёт следующей за последней позиции.
            ++_EndPosition;

            //  Расчёт начальной позиции.
            if (_EndPosition - _BeginPosition > Length)
            {
                _BeginPosition = _EndPosition - Length;
            }

            //  Вызов события, связанного с изменением буфера.
            OnChanged(new CircularBufferEventArgs(_BeginPosition, _EndPosition));
        }
    }

    /// <summary>
    /// Выполняет чтение значения из указанной позиции в буфере.
    /// </summary>
    /// <param name="position">
    /// Позиция в буфере.
    /// </param>
    /// <returns>
    /// Прочитанное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="position"/> передано значение,
    /// которое меньше значения <see cref="BeginPosition"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="position"/> передано значение,
    /// которое равно или превышает значение <see cref="EndPosition"/>.
    /// </exception>
    public T Read(long position)
    {
        //  Блокировка объекта для синхронизации доступа к буферу.
        lock (SyncRoot)
        {
            //  Проверка позиции снизу.
            IsNotLess(position, _BeginPosition, nameof(position));

            //  Проверка позиции сверху.
            IsLess(position, _EndPosition, nameof(position));

            //  Чтение значения из необработанного буфера.
            return _RawBuffer.Read(position);
        }
    }

    /// <summary>
    /// Выполняет запись массива значений в буфер.
    /// </summary>
    /// <param name="buffer">
    /// Массив, который содержит значения для записи.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OverflowException">
    /// Операция записи привела к переполнению буфера:
    /// значение позиции <see cref="EndPosition"/> после записи превысит значение <see cref="long.MaxValue"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="buffer"/> передан массив,
    /// размер которого больше длины буфера <see cref="Length"/>.
    /// </exception>
    public void Write(params T[] buffer)
    {
        //  Проверка ссылки на массив.
        IsNotNull(buffer, nameof(buffer));

        //  Проверка вместимости массива.
        if (buffer.Length > Length)
        {
            throw Exceptions.ArgumentArrayLargerMax(nameof(buffer));
        }

        //  Запись в буфер.
        Write(buffer, 0, buffer.Length);
    }

    /// <summary>
    /// Выполняет чтение массива значений из буфера.
    /// </summary>
    /// <param name="position">
    /// Позиция в буфере, начиная с которого необходимо прочитать элементы.
    /// </param>
    /// <param name="count">
    /// Количество элементов, которые необходимо прочитать из текущего буфера.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано значение большее <see cref="Length"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="position"/> передано значение меньшее <see cref="BeginPosition"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="position"/> передано значение большее или равное <see cref="EndPosition"/> - <paramref name="count"/>.
    /// </exception>
    public T[] Read(long position, int count)
    {
        //  Проверка количества элементов для чтения.
        IsNotNegative(count, nameof(count));

        //  Создание массива элементов.
        T[] buffer = new T[count];

        //  Чтение значений из буфера.
        Read(position, buffer, 0, count);

        //  Возвращение прочитанных значений.
        return buffer;
    }

    /// <summary>
    /// Выполняет запись массива значений в буфер.
    /// </summary>
    /// <param name="buffer">
    /// Массив, который содержит значения для записи.
    /// </param>
    /// <param name="offset">
    /// Смещение в массиве <paramref name="buffer"/>,
    /// с которого начинается копирование элементов в текущий буфер.
    /// </param>
    /// <param name="count">
    /// Количество элементов, которые необходимо записать в текущий буфер.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано значение,
    /// которое превышает значение <see cref="Length"/>.
    /// </exception>
    /// <exception cref="OverflowException">
    /// Операция записи привела к переполнению буфера:
    /// значение позиции <see cref="EndPosition"/> после записи превысит значение <see cref="long.MaxValue"/>.
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
    public void Write(T[] buffer, int offset, int count)
    {
        //  Проверка диапазона значений в массиве.
        IsRange(buffer, offset, count, nameof(buffer), nameof(offset), nameof(count));

        //  Проверка вместимости буфера.
        IsNotLarger(count, Length, nameof(count));

        //  Блокировка объекта для синхронизации доступа к буферу.
        lock (SyncRoot)
        {
            //  Проверка переполнения буфера.
            if (_EndPosition > long.MaxValue - count)
            {
                //  Операция привела к переполнению.
                throw Exceptions.OperationOverflow();
            }

            //  Запись в необработанный буфер.
            _RawBuffer.Write(_EndPosition, buffer, offset, count);

            //  Расчёт следующей за последней позиции.
            _EndPosition += count;

            //  Расчёт начальной позиции.
            if (_EndPosition - _BeginPosition > Length)
            {
                _BeginPosition = _EndPosition - Length;
            }

            //  Вызов события, связанного с изменением буфера.
            OnChanged(new CircularBufferEventArgs(_BeginPosition, _EndPosition));
        }
    }

    /// <summary>
    /// Выполняет чтение массива значений из буфера.
    /// </summary>
    /// <param name="position">
    /// Позиция в буфере, начиная с которого необходимо прочитать элементы.
    /// </param>
    /// <param name="buffer">
    /// Массив, в который необходимо поместить прочитанные элементы.
    /// </param>
    /// <param name="offset">
    /// Смещение в массиве <paramref name="buffer"/>,
    /// с которого начинается запись прочитанных элементов из текущего буфера.
    /// </param>
    /// <param name="count">
    /// Количество элементов, которые необходимо прочитать из текущего буфера.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="offset"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// В параметре <paramref name="buffer"/> передан массив,
    /// который не вмещает <paramref name="count"/> элементов начиная с позиции <paramref name="offset"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано значение большее <see cref="Length"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="position"/> передано значение меньшее <see cref="BeginPosition"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="position"/> передано значение большее <see cref="EndPosition"/> - <paramref name="count"/>.
    /// </exception>
    public void Read(long position, T[] buffer, int offset, int count)
    {
        //  Проверка вместимости буфера.
        IsNotLarger(count, Length, nameof(count));

        //  Блокировка объекта для синхронизации доступа к буферу.
        lock (SyncRoot)
        {
            //  Проверка позиции снизу.
            IsNotLess(position, _BeginPosition, nameof(position));

            //  Проверка позиции сверху.
            IsLess(position + count, _EndPosition, nameof(position));

            //  Чтение значений из необработанного буфера.
            _RawBuffer.Read(position, buffer, offset, count);
        }
    }

    /// <summary>
    /// Создаёт копию текущего экземпляра.
    /// </summary>
    /// <returns>
    /// Копия текущего экземпляра.
    /// </returns>
    public CircularBuffer<T> Clone() => new(this);

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<T> GetEnumerator() => new CircularBufferEnumerator<T>(this);

    /// <summary>
    /// Вызывает событие <see cref="Changed"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnChanged(CircularBufferEventArgs e)
    {
        //  Блокировка объекта для синхронизации доступа к буферу.
        lock (SyncRoot)
        {
            //  Вызов обработчиков события.
            _Changed?.Invoke(this, e);
        }
    }

    /// <summary>
    /// Создаёт копию текущего экземпляра.
    /// </summary>
    /// <returns>
    /// Копия текущего экземпляра.
    /// </returns>
    object ICloneable.Clone() => Clone();

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
