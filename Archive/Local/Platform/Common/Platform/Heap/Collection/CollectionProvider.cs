using System.Collections;
using System.Collections.ObjectModel;

namespace Apeiron.Collections;

/// <summary>
/// Представляет поставщика коллекции.
/// </summary>
/// <typeparam name="T">
/// Тип элементов коллекции.
/// </typeparam>
public class CollectionProvider<T> :
    IList<T>
{
    /// <summary>
    /// Поле для хранения списка элементов.
    /// </summary>
    private readonly IList<T> _Items;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="options">
    /// Настройки коллекции.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="options"/> передана пустая ссылка.
    /// </exception>
    internal CollectionProvider(CollectionOptions options) :
        this(options, new List<T>(), new())
    {
            
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="options">
    /// Настройки коллекции.
    /// </param>
    /// <param name="items">
    /// Список для хранения элементов.
    /// </param>
    /// <param name="sync">
    /// Объект, с помощью которого можно синхронизировать доступ к коллекции.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="options"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="items"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="sync"/> передана пустая ссылка.
    /// </exception>
    internal CollectionProvider(CollectionOptions options, IList<T> items, object sync)
    {
        //  Установка настроек коллекции.
        Options = Check.IsNotNull(options, nameof(options));

        //  Установка списка элементов.
        _Items = Check.IsNotNull(items, nameof(items));

        //  Установка объекта, с помощью которого можно синхронизировать доступ к коллекции.
        SyncRoot = Check.IsNotNull(sync, nameof(sync));
    }

    /// <summary>
    /// Возвращает объект, с помощью которого можно синхронизировать доступ к коллекции.
    /// </summary>
    public object SyncRoot { get; }

    /// <summary>
    /// Возвращает настройки коллекции.
    /// </summary>
    public CollectionOptions Options { get; }

    /// <summary>
    /// Возвращает количество элементов в коллекции.
    /// </summary>
    public int Count => LockCore(() => _Items.Count);

    /// <summary>
    /// Возвращает значение, определяющее, доступна ли коллекция только для чтения.
    /// </summary>
    bool ICollection<T>.IsReadOnly => false;

    /// <summary>
    /// Возвращает или задаёт элемент с указанным индексом.
    /// </summary>
    /// <param name="index">
    /// Индекс элемента.
    /// </param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано значение большее или равное <see cref="Count"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В настройках коллекции установлен флаг <see cref="CollectionOptions.IsCheckNull"/>
    /// и передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В настройках коллекции установлен флаг <see cref="CollectionOptions.IsCheckDuplicate"/>
    /// и передано значение, которое уже содержится коллекции.
    /// </exception>
    public T this[int index]
    {
        get => LockCore(() => _Items[Check.IsIndex(index, _Items.Count, nameof(index))]);
        set
        {
            LockCore(
                delegate
                {
                    //  Проверка индекса.
                    Check.IsIndex(index, _Items.Count, nameof(index));

                    //  Проверка пустых ссылок.
                    CheckNullCore(value);

                    //  Проверка дублирования.
                    CheckDuplicateCore(value, index);

                    //  Установка значения.
                    _Items[index] = value;
                });
        }
    }

    /// <summary>
    /// Проверяет содержится ли элемент в коллекции.
    /// </summary>
    /// <param name="item">
    /// Элемент для проверки.
    /// </param>
    /// <returns>
    /// Значение <c>true</c>, если элемент содержится в коллекции;
    /// в противном случае - <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В настройках коллекции установлен флаг <see cref="CollectionOptions.IsCheckNull"/>
    /// и в параметре <paramref name="item"/> передана пустая ссылка.
    /// </exception>
    public bool Contains(T item) => LockCore(() => _Items.Contains(CheckNullCore(item)));

    /// <summary>
    /// Возвращает индекс первого вхождения элемента в коллекцию.
    /// </summary>
    /// <param name="item">
    /// Элемент, индекс первого вхождения которого необходимо определить.
    /// </param>
    /// <returns>
    /// Индекс первого вхождения элемента в коллекцию, если элемент содержится в коллекции;
    /// В противном случае - значение -1.
    /// </returns>
    public int IndexOf(T item) => LockCore(() => _Items.IndexOf(CheckNullCore(item)));

    /// <summary>
    /// Добавляет новый элемент в конец коллекции.
    /// </summary>
    /// <param name="item">
    /// Элемент, добавляемый в конец коллекции.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В настройках коллекции установлен флаг <see cref="CollectionOptions.IsCheckNull"/>
    /// и в параметре <paramref name="item"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В настройках коллекции установлен флаг <see cref="CollectionOptions.IsCheckDuplicate"/>
    /// и передано значение, которое уже содержится коллекции.
    /// </exception>
    public void Add(T item)
    {
        //  Блокировка критического объекта.
        LockCore(
            delegate
            {
                //  Проверка пустых ссылок.
                CheckNullCore(item);

                //  Проверка дублирования.
                CheckDuplicateCore(item);

                //  Добавление элемента.
                _Items.Add(item);
            });

    }

    /// <summary>
    /// Добавляет коллекцию элементов.
    /// </summary>
    /// <param name="collection">
    /// Коллекция добавляемых элементов.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="collection"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В настройках коллекции установлен флаг <see cref="CollectionOptions.IsCheckNull"/>
    /// и коллекция <paramref name="collection"/> содержит значение, являющееся пустой ссылкой.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В настройках коллекции установлен флаг <see cref="CollectionOptions.IsCheckDuplicate"/>
    /// и коллекция <paramref name="collection"/> содержит значение, которое уже содержится коллекции.
    /// </exception>
    public void AddRange(IEnumerable<T> collection)
    {
        //  Проверка ссылки на коллекцию.
        collection = Check.IsNotNull(collection, nameof(collection));

        //  Блокировка критического объекта.
        LockCore(
            delegate
            {
                //  Блокировка критического объекта.
                lock (SyncRoot)
                {
                    //  Перебор добавляемых элементов.
                    foreach (var item in collection)
                    {
                        //  Добавление элемента.
                        Add(item);
                    }
                }
            });
    }

    /// <summary>
    /// Вставляет элемент на указанную позицию.
    /// </summary>
    /// <param name="index">
    /// Индекс для вставки элемента.
    /// </param>
    /// <param name="item">
    /// Вставляемый элемент.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано значение,
    /// которое превышает значение <see cref="Count"/>.
    /// </exception>
    public void Insert(int index, T item)
    {
        //  Блокировка критического объекта.
        LockCore(
            delegate
            {
                //  Проверка индекса.
                Check.IsNotNegative(index, nameof(index));
                Check.IsNotLarger(index, _Items.Count, nameof(index));

                //  Проверка пустых ссылок.
                CheckNullCore(item);

                //  Проверка дублирования.
                CheckDuplicateCore(item);

                //  Вставка элемента.
                _Items.Insert(index, item);
            });
    }

    /// <summary>
    /// Перемещает элемент с указанным индексом в новое место.
    /// </summary>
    /// <param name="oldIndex">
    /// Индекс, указывающий положение перемещаемого элемента.
    /// </param>
    /// <param name="newIndex">
    /// Индекс, указывающий новое положение элемента.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="oldIndex"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="oldIndex"/> передано значение,
    /// которое равно или превышает значение <see cref="Count"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="newIndex"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="newIndex"/> передано значение,
    /// которое равно или превышает значение <see cref="Count"/>.
    /// </exception>
    public void Move(int oldIndex, int newIndex)
    {
        //  Блокировка критического объекта.
        LockCore(
            delegate
            {
                //  Проверка индексов.
                Check.IsIndex(oldIndex, _Items.Count, nameof(oldIndex));
                Check.IsIndex(newIndex, _Items.Count, nameof(newIndex));

                //  Проверка необходимости перемещения.
                if (oldIndex != newIndex)
                {
                    //  Проверка типа списка.
                    if (_Items is ObservableCollection<T> observable)
                    {
                        //  Перемещение элемента.
                        observable.Move(oldIndex, newIndex);
                    }
                    else
                    {
                        //  Получение перемещаемого элемента.
                        T item = _Items[oldIndex];

                        //  Удаление перемещаемого элемента из текущего положения.
                        _Items.RemoveAt(oldIndex);

                        //  Вставка перемещаемого элемента в новое положение.
                        _Items.Insert(newIndex, item);
                    }
                }
            });
    }

    /// <summary>
    /// Удаляет элемент с указанным индексом.
    /// </summary>
    /// <param name="index">
    /// Индекс удаляемого элемента.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано значение большее или равное <see cref="Count"/>.
    /// </exception>
    public void RemoveAt(int index)
    {
        //  Блокировка критического объекта.
        LockCore(
            delegate
            {
                //  Проверка индекса.
                Check.IsIndex(index, _Items.Count, nameof(index));

                //  Удаление элемента.
                _Items.RemoveAt(index);
            });
    }

    /// <summary>
    /// Удаляет элемент из коллекции.
    /// </summary>
    /// <param name="item">
    /// Элемент, который необходимо удалить из коллекции.
    /// </param>
    /// <returns>
    /// Значение <c>true</c>, если элемент успешно удалён из коллекции;
    /// в противном случае - значение <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В настройках коллекции установлен флаг <see cref="CollectionOptions.IsCheckNull"/>
    /// и в параметре <paramref name="item"/> передана пустая ссылка.
    /// </exception>
    public bool Remove(T item)
    {
        //  Блокировка критического объекта.
        return LockCore(
            delegate
            {
                //  Проверка пустых ссылок.
                CheckNullCore(item);

                //  Удаление элемента.
                return _Items.Remove(item);
            });
    }

    /// <summary>
    /// Удаляет все элементы из коллекции.
    /// </summary>
    public void Clear() => LockCore(() => _Items.Clear());

    /// <summary>
    /// Выполняет копирование элементов коллекции в заданный массив.
    /// </summary>
    /// <param name="array">
    /// Массив, в который необходимо скопировать элементы.
    /// </param>
    /// <param name="index">
    /// Начальный индекс в массивве, начиная с которого необходимо записать элементы.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="array"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано значение,
    /// которое превышает длину массива <paramref name="array"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Сумма значений параметров <paramref name="index"/> и <see cref="Count"/>
    /// превышает длину массива <paramref name="array"/>.
    /// </exception>
    public void CopyTo(T[] array, int index)
    {
        //  Блокировка критического объекта.
        LockCore(
            delegate
            {
                //  Проверка массива.
                Check.IsRange(array, index, _Items.Count, nameof(array), nameof(index), nameof(Count));

                //  Копирование элементов.
                _Items.CopyTo(array, index);
            });
    }

    /// <summary>
    /// Возвращает массив элементов коллекции.
    /// </summary>
    /// <returns>
    /// Массив элементов коллекции.
    /// </returns>
    public T[] ToArray()
    {
        //  Блокировка критического объекта.
        return LockCore(
            delegate
            {
                //  Создание массива.
                T[] array = new T[_Items.Count];

                //  Копирование элементов.
                _Items.CopyTo(array, 0);

                //  Возврат массива.
                return array;
            });
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<T> GetEnumerator()
    {
        //  Возврат перечислителя списка элементов.
        return _Items.GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат перечислителя списка элементов.
        return ((IEnumerable)_Items).GetEnumerator();
    }

    /// <summary>
    /// Выполняет метод с блокировкой.
    /// </summary>
    /// <param name="method">
    /// Метод, который необходимо выполнить с блокировкой.
    /// </param>
    /// <returns>
    /// Результат метода.
    /// </returns>
    /// <remarks>
    /// Метод не выполняет проверку входных параметров.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void LockCore(Action method)
    {
        //  Проверка потокобезопасности.
        if (Options.IsConcurrent)
        {
            //  Захват критического объекта.
            lock (SyncRoot)
            {
                //  Вызов метода.
                method();
            }
        }
        else
        {
            //  Вызов метода.
            method();
        }
    }

    /// <summary>
    /// Выполняет метод с блокировкой.
    /// </summary>
    /// <typeparam name="TResult">
    /// Тип результата.
    /// </typeparam>
    /// <param name="method">
    /// Метод, который необходимо выполнить с блокировкой.
    /// </param>
    /// <returns>
    /// Результат метода.
    /// </returns>
    /// <remarks>
    /// Метод не выполняет проверку входных параметров.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TResult LockCore<TResult>(Func<TResult> method)
    {
        //  Проверка потокобезопасности.
        if (Options.IsConcurrent)
        {
            //  Захват критического объекта.
            lock (SyncRoot)
            {
                //  Вызов метода.
                return method();
            }
        }
        else
        {
            //  Вызов метода.
            return method();
        }
    }

    /// <summary>
    /// Выполняет проверку ссылки на элемент.
    /// </summary>
    /// <param name="item">
    /// Сылка на элемент.
    /// </param>
    /// <returns>
    /// Проверенная ссылка.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В настройках коллекции установлен флаг <see cref="CollectionOptions.IsCheckNull"/>
    /// и в параметре <paramref name="item"/> передана пустая ссылка.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private T CheckNullCore(T item)
    {
        //  Проверка пустых ссылок.
        if (Options.IsCheckNull)
        {
            //  Проверка ссылки на элемент.
            Check.IsNotNull(item, nameof(item));
        }

        //  Возврат проверенного элемента.
        return item;
    }

    /// <summary>
    /// Выполняет проверку дублирования.
    /// </summary>
    /// <param name="item">
    /// Проверяемый элемент.
    /// </param>
    /// <returns>
    /// Проверенный элемент.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В настройках коллекции установлен флаг <see cref="CollectionOptions.IsCheckDuplicate"/>
    /// и передано значение, которое уже содержится коллекции.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private T CheckDuplicateCore(T item)
    {
        //  Проверка дублирования.
        if (Options.IsCheckDuplicate)
        {
            //  Проверка вхождения в коллекцию.
            if (_Items.Contains(item))
            {
                //  Передано значение, которое уже содержится коллекции.
                throw Exceptions.ArgumentAlreadyContainedInCollection(nameof(item));
            }
        }

        //  Возврат проверенного элемента.
        return item;
    }

    /// <summary>
    /// Выполняет проверку дублирования.
    /// </summary>
    /// <param name="item">
    /// Проверяемый элемент.
    /// </param>
    /// <param name="index">
    /// Индекс элемента.
    /// </param>
    /// <returns>
    /// Проверенный элемент.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В настройках коллекции установлен флаг <see cref="CollectionOptions.IsCheckDuplicate"/>
    /// и передано значение, которое уже содержится коллекции.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private T CheckDuplicateCore(T item, int index)
    {
        //  Проверка дублирования.
        if (Options.IsCheckDuplicate)
        {
            //  Поиск индекса элемента.
            int actualIndex = _Items.IndexOf(item);

            //  Проверка индекса.
            if (actualIndex != -1 && actualIndex != index)
            {
                //  Передано значение, которое уже содержится коллекции.
                throw Exceptions.ArgumentAlreadyContainedInCollection(nameof(item));
            }
        }

        //  Возврат проверенного элемента.
        return item;
    }
}
