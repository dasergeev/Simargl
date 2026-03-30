using System.Collections;
using System.Collections.Generic;

namespace Simargl.Frames.Mera;

/// <summary>
/// Представляет коллекцию информации о порциях записи при прерывистой записи.
/// </summary>
public sealed class MeraPortionInfoCollection :
    IEnumerable<MeraPortionInfo>
{
    /// <summary>
    /// Поле для хранения списка элементов коллекции.
    /// </summary>
    private readonly List<MeraPortionInfo> _Items;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    internal MeraPortionInfoCollection()
    {
        //  Создание списка элементов коллекции.
        _Items = [];
    }

    /// <summary>
    /// Возвращает количество элементов в коллекции.
    /// </summary>
    public int Count => _Items.Count;

    /// <summary>
    /// Возвращает или задаёт элемент с указанным индексом.
    /// </summary>
    /// <param name="index">
    /// Индекс элемента.
    /// </param>
    /// <returns>
    /// Элемент с указанным индексом.
    /// </returns>
    public MeraPortionInfo this[int index]
    {
        get => _Items[index];
        set => _Items[index] = IsNotNull(value, nameof(value));
    }

    /// <summary>
    /// Добавляет элемент в коллекцию.
    /// </summary>
    /// <param name="item">
    /// Элемент, добавляемый в коллекцию.
    /// </param>
    public void Add(MeraPortionInfo item) => _Items.Add(IsNotNull(item, nameof(item)));

    /// <summary>
    /// Удаляет элемент с указанным индексом.
    /// </summary>
    /// <param name="index">
    /// Индекс удаляемого элемента.
    /// </param>
    public void RemoveAt(int index) => _Items.RemoveAt(index);

    /// <summary>
    /// Очищаяет коллекцию.
    /// </summary>
    public void Clear() => _Items.Clear();

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<MeraPortionInfo> GetEnumerator() => ((IEnumerable<MeraPortionInfo>)_Items).GetEnumerator();

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_Items).GetEnumerator();
}
