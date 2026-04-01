using System;
using System.Collections;
using System.Collections.Generic;

namespace Simargl.Analysis.EffectWayIndicators;

/// <summary>
/// Представляет коллекцию данных.
/// </summary>
public sealed class WayParametersCollection :
    IEnumerable<WayParameters>
{
    /// <summary>
    /// Поле для хранения списка данных.
    /// </summary>
    private readonly List<WayParameters> _List;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="collection">
    /// Коллекция элементов.
    /// </param>
    internal WayParametersCollection(IEnumerable<WayParameters> collection)
    {
        _List = new List<WayParameters>(collection);
    }

    /// <summary>
    /// Возвращает количество элементов в коллекции.
    /// </summary>
    /// <returns>
    /// Количество элементов в коллекции.
    /// </returns>
    public int Count => _List.Count;

    /// <summary>
    /// Возвращает элемент с указанным индексом.
    /// </summary>
    /// <param name="index">
    /// Индекс элемента.
    /// </param>
    /// <returns>
    /// Элемент.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано значение большее или равное <see cref="Count"/>.
    /// </exception>
    public WayParameters this[int index]
    {
        get
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Значение меньше нуля.");
            }
            if (index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Значение больше допустимого.");
            }
            return _List[index];
        }
    }

    /// <summary>
    /// Возвращает элемент с указанным ключом.
    /// </summary>
    /// <param name="key">
    /// Ключ.
    /// </param>
    /// <returns>
    /// Элемент.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Происходит если коллекция не содержит элемент с ключом <paramref name="key"/>.
    /// </exception>
    public WayParameters this[WayParametersKey key]
    {
        get
        {
            foreach (var data in this)
            {
                if (data.Key == key)
                {
                    return data;
                }
            }
            throw new ArgumentOutOfRangeException(nameof(key), "Недопустимое значение ключа");
        }
    }

    /// <summary>
    /// Возвращает элемент с указанным ключом.
    /// </summary>
    /// <param name="name">
    /// Имя.
    /// </param>
    /// <returns>
    /// Элемент.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Происходит если коллекция не содержит элемент с именем <paramref name="name"/>.
    /// </exception>
    public WayParameters this[string name]
    {
        get
        {
            foreach (var data in this)
            {
                if (data.Name == name)
                {
                    return data;
                }
            }
            throw new ArgumentOutOfRangeException(nameof(name), "Недопустимое значение ключа");
        }
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<WayParameters> GetEnumerator()
    {
        return ((IEnumerable<WayParameters>)_List).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_List).GetEnumerator();
    }
}
