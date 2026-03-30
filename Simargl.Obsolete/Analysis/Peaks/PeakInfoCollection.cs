using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Simargl.Analysis.Peaks;

/// <summary>
/// Представляет коллекцию информации о пиках.
/// </summary>
public sealed class PeakInfoCollection :
    INotifyCollectionChanged,
    INotifyPropertyChanged,
    IEnumerable<PeakInfo>,
    ICollection<PeakInfo>,
    IList<PeakInfo>,
    ICloneable
{
    /// <summary>
    /// Происходит при изменении свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Происходит при изменении свойства <see cref="Count"/>.
    /// </summary>
    public event EventHandler? CountChanged;

    /// <summary>
    /// Происходит при изменении коллекции.
    /// </summary>
    public event NotifyCollectionChangedEventHandler? CollectionChanged
    {
        add => _Items.CollectionChanged += value;
        remove => _Items.CollectionChanged -= value;
    }

    /// <summary>
    /// Поле для хранения количества элементов.
    /// </summary>
    int _Count;

    /// <summary>
    /// Поле для хранения элементов коллекции.
    /// </summary>
    readonly ObservableCollection<PeakInfo> _Items;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public PeakInfoCollection() :
        this(new ObservableCollection<PeakInfo>())
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="items">
    /// Элементы коллекции.
    /// </param>
    PeakInfoCollection(ObservableCollection<PeakInfo> items)
    {
        _Count = items.Count;
        _Items = items;
    }

    /// <summary>
    /// Возвращает количество элементов.
    /// </summary>
    public int Count
    {
        get => _Count;
        private set
        {
            if (_Count != value)
            {
                _Count = value;
                OnCountChanged(EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Возвращает или задает элемент по указанному индексу.
    /// </summary>
    /// <param name="index">
    /// Отсчитываемый от нуля индекс элемента, который требуется возвратить или задать.
    /// </param>
    /// <returns>
    /// Элемент, расположенный по указанному индексу.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано значение большее или равное количеству элементов <see cref="Count"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="value"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Элемент уже содержится в коллекции.
    /// </exception>
    public PeakInfo this[int index]
    {
        get
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Передано отрицательное значение.");
            }
            if (index >= _Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Передано значение большее или равное количеству элементов.");
            }
            return _Items[index];
        }
        set
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Передано отрицательное значение.");
            }
            if (index >= _Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Передано значение большее или равное количеству элементов.");
            }
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value), "Передана пустая ссылка.");
            }
            int currentIndex = IndexOf(value);
            if (currentIndex == -1 || currentIndex == index)
            {
                _Items[index] = value;
            }
            else
            {
                throw new InvalidOperationException("Элемент уже содержится в коллекции.");
            }
        }
    }

    /// <summary>
    /// Добавляет элемент в конец коллекции.
    /// </summary>
    /// <param name="item">
    /// Элемент, добавляемый в конец коллекции.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="item"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Элемент уже содержится в коллекции.
    /// </exception>
    public void Add(PeakInfo item)
    {
        //if (Contains(item))
        //{
        //    throw new InvalidOperationException("Элемент уже содержится в коллекции.");
        //}
        _Items.Add(item);
        ++Count;
    }

    /// <summary>
    /// Вставляет элемент в коллекцию по указанному индексу.
    /// </summary>
    /// <param name="index">
    /// Отсчитываемый от нуля индекс, по которому следует вставить элемент <paramref name="item"/>.
    /// </param>
    /// <param name="item">
    /// Элемент, вставляемый в коллекцию.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано значение большее количества элементов <see cref="Count"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="item"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Элемент уже содержится в коллекции.
    /// </exception>
    public void Insert(int index, PeakInfo item)
    {
        if (index < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Передано отрицательное значение.");
        }
        if (index > _Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Передано значение больше количества элементов.");
        }
        if (Contains(item))
        {
            throw new InvalidOperationException("Элемент уже содержится в коллекции.");
        }
        _Items.Insert(index, item);
        ++Count;
    }

    /// <summary>
    /// Удаляет все элементы из коллекции.
    /// </summary>
    public void Clear()
    {
        _Items.Clear();
        Count = 0;
    }

    /// <summary>
    /// Определяет, содержит ли коллекция указанное значение.
    /// </summary>
    /// <param name="item">
    /// Объект для поиска в коллекции.
    /// </param>
    /// <returns>
    /// Результат поиска.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="item"/> передана пустая ссылка.
    /// </exception>
    public bool Contains(PeakInfo item)
    {
        return IndexOf(item) != -1;
    }

    /// <summary>
    /// Копирует элементы коллекции в массив, начиная с указанного индекса массива.
    /// </summary>
    /// <param name="array">
    /// Одномерный массив, в который копируются элементы коллекции.
    /// </param>
    /// <param name="arrayIndex">
    /// Отсчитываемый от нуля индекс в массиве <paramref name="array"/>, указывающий начало копирования.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="array"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="arrayIndex"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Число элементов в коллекции больше доступного места в массиве <paramref name="array"/>.
    /// </exception>
    public void CopyTo(PeakInfo[] array, int arrayIndex)
    {
        if (array is null)
        {
            throw new ArgumentNullException(nameof(array), "Передана пустая ссылка.");
        }
        if (arrayIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Передано отрицательное значение.");
        }
        try
        {
            _Items.CopyTo(array, arrayIndex);
        }
        catch (ArgumentException)
        {
            throw new ArgumentException("Число элементов в коллекции больше доступного места в массиве.", nameof(array));
        }
    }

    /// <summary>
    /// Удаляет первое вхождение указанного объекта из коллекции.
    /// </summary>
    /// <param name="item">
    /// Элемент, который необходимо удалить из коллекции.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="item"/> передана пустая ссылка.
    /// </exception>
    public bool Remove(PeakInfo item)
    {
        int index = IndexOf(item);
        if (index != -1)
        {
            _Items.RemoveAt(index);
            --Count;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Удаляет элемент, расположенный по указанному индексу.
    /// </summary>
    /// <param name="index">
    /// Отсчитываемый от нуля индекс удаляемого элемента.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано значение большее или равное количеству элементов <see cref="Count"/>.
    /// </exception>
    public void RemoveAt(int index)
    {
        if (index < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Передано отрицательное значение.");
        }
        if (index >= _Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Передано значение большее или равное количеству элементов.");
        }
        _Items.RemoveAt(index);
        --Count;
    }

    /// <summary>
    /// Определяет индекс заданного элемента в коллекции.
    /// </summary>
    /// <param name="item">
    /// Элемент для поиска.
    /// </param>
    /// <returns>
    /// Индекс элемента, если он найден в коллекции; в противном случае - значение -1.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="item"/> передана пустая ссылка.
    /// </exception>
    public int IndexOf(PeakInfo item)
    {
        if (item is null)
        {
            throw new ArgumentNullException(nameof(item), "Передана пустая ссылка.");
        }
        for (int i = 0; i != _Count; ++i)
        {
            if (item == _Items[i])
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// Создает новый объект, являющийся копией текущего экземпляра.
    /// </summary>
    /// <returns>
    /// Новый объект, являющийся копией этого экземпляра.
    /// </returns>
    public PeakInfoCollection Clone()
    {
        ObservableCollection<PeakInfo> items = new();
        foreach (var item in this)
        {
            items.Add(item.Clone());
        }
        return new PeakInfoCollection(items);
    }

    /// <summary>
    /// Создает новый объект, являющийся копией текущего экземпляра.
    /// </summary>
    /// <returns>
    /// Новый объект, являющийся копией этого экземпляра.
    /// </returns>
    object ICloneable.Clone()
    {
        return Clone();
    }

    /// <summary>
    /// Вызывает событие <see cref="PropertyChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        PropertyChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="CountChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnCountChanged(EventArgs e)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
        CountChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<PeakInfo> GetEnumerator() => _Items.GetEnumerator();

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_Items).GetEnumerator();

    /// <summary>
    /// Получает значение, указывающее, является ли коллекция доступной только для чтения.
    /// </summary>
    bool ICollection<PeakInfo>.IsReadOnly => false;
}
