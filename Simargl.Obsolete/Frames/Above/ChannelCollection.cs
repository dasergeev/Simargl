using Simargl.Designing.Utilities;
using Simargl.Designing;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Simargl.Frames;

/// <summary>
/// Представляет коллекцию каналов.
/// </summary>
public class ChannelCollection :
    IEnumerable<Channel>
{
    /// <summary>
    /// Поле для хранения элементов коллекции.
    /// </summary>
    private readonly List<Channel> _Items;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public ChannelCollection()
    {
        //  Создание хранилища элементов коллекции.
        _Items = new();
    }

    /// <summary>
    /// Возвращает количество элементов в коллекции.
    /// </summary>
    public int Count => _Items.Count;

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
    /// В параметре <paramref name="item"/> передана пустая ссылка.
    /// </exception>
    public bool Contains(Channel item) => _Items.Contains(IsNotNull(item, nameof(item)));

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
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="item"/> передана пустая ссылка.
    /// </exception>
    public int IndexOf(Channel item) => _Items.IndexOf(IsNotNull(item, nameof(item)));

    /// <summary>
    /// Возвращает или задаёт канал с указанным индексом.
    /// </summary>
    /// <param name="index">
    /// Индекс канала.
    /// </param>
    /// <returns>
    /// Канал с указанным индексом.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано значение большее или равное <see cref="Count"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано значение, которое уже содержится коллекции.
    /// </exception>
    public Channel this[int index]
    {
        get => _Items[IsIndex(index, Count, nameof(index))];
        set
        {
            //  Проверка индекса.
            IsIndex(index, Count, nameof(index));

            //  Проверка ссылки на значение.
            IsNotNull(value, nameof(Channel));

            //  Поиск индекса элемента.
            int actualIndex = _Items.IndexOf(value);

            //  Проверка индекса.
            if (actualIndex != -1 && actualIndex != index)
            {
                //  Передано значение, которое уже содержится коллекции.
                throw new ArgumentOutOfRangeException(nameof(Channel), $"В параметре {nameof(Channel)} передано значение, которое уже содержится коллекции.");
            }

            //  Установка значения.
            _Items[index] = value;
        }
    }

    /// <summary>
    /// Добавляет канал.
    /// </summary>
    /// <param name="index">
    /// Индекс места вставки.
    /// </param>
    /// <param name="item">
    /// Канал, который добавляется.
    /// </param>
    public void Insert(int index, Channel item)
    {
        _Items.Insert(index, item);
    }

    /// <summary>
    /// Добавляет канал в конец коллекции.
    /// </summary>
    /// <param name="item">
    /// Канал, который добавляется в конец коллекции.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="item"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="item"/> передано значение, которое уже содержится коллекции.
    /// </exception>
    public void Add(Channel item)
    {
        //  Проверка ссылки на канал.
        IsNotNull(item, nameof(item));

        //  Проверка вхождения в коллекцию.
        if (_Items.Contains(item))
        {
            //  Передано значение, которое уже содержится коллекции.
            throw new ArgumentOutOfRangeException(nameof(item), $"В параметре {nameof(item)} передано значение, которое уже содержится коллекции.");
        }

        //  Добавление в коллекцию.
        _Items.Add(item);
    }

    /// <summary>
    /// Добавляет каналы в конец коллекции.
    /// </summary>
    /// <param name="collection">
    /// Коллекция добавляемых каналов.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="collection"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Коллекция <paramref name="collection"/> содержит значение, являющееся пустой ссылкой.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Коллекция <paramref name="collection"/> содержит значение, которое уже содержится коллекции.
    /// </exception>
    public void AddRange(IEnumerable<Channel> collection)
    {
        //  Проверка ссылки на коллекцию.
        IsNotNull(collection, nameof(collection));

        ////  Проверка пустых ссылок в коллекции.
        //if (collection.Any(x => x is null))
        //{
        //    //  В параметре передана пустая ссылка.
        //    throw ExceptionCreator.ArgumentNullReference(nameof(collection));
        //}

        //  Проверка вхождения в коллекцию.
        if (collection.Any(x => _Items.Contains(x)))
        {
            //  Передано значение, которое уже содержится коллекции.
            throw new ArgumentOutOfRangeException(nameof(collection), $"В параметре {nameof(collection)} передано значение, которое уже содержится коллекции.");
        }

        //  Добавление в коллекцию.
        _Items.AddRange(collection);
    }

    /// <summary>
    /// Удаляет элемент из коллекции.
    /// </summary>
    /// <param name="item">
    /// Элемент, который необходимо удалить из коллекции.
    /// </param>
    /// <returns>
    /// Значение <c>true</c>, если элемент <paramref name="item"/> успешно удален, в противном случае - значение <c>false</c>.
    /// Этот метод также возвращает <c>false</c>, если элемент <paramref name="item"/> не найден в последовательности.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="item"/> передана пустая ссылка.
    /// </exception>
    public bool Remove(Channel item)
    {
        //  Проверка ссылки на канал.
        IsNotNull(item, nameof(item));

        //  Удаление элемента из коллекции.
        return _Items.Remove(item);
    }

    /// <summary>
    /// Удаляет все каналы из коллекции.
    /// </summary>
    public void Clear()
    {
        //  Очистка хранилища элементов.
        _Items.Clear();
    }

    /// <summary>
    /// Проверяет, содержится ли канал с указанным именем в коллеции.
    /// </summary>
    /// <param name="name">
    /// Имя канала.
    /// </param>
    /// <returns>
    /// Результат проверки.
    /// </returns>
    public bool Contains(string name)
    {
        //  Проверка ссылки на имя.
        IsNotNull(name, nameof(name));

        //  Поиск канала.
        return _Items.Any(x => x.Name == name);
    }

    /// <summary>
    /// Возвращает первый канал с указанным именем.
    /// </summary>
    /// <param name="name">
    /// Имя канала.
    /// </param>
    /// <returns>
    /// Первый канал с указанным именем.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="name"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="name"/> передано значение, которое не содержится в коллекции.
    /// </exception>
    public Channel this[string name]
    {
        get
        {
            //  Проверка имени.
            name = IsNotNull(name, nameof(name));

            //  Поиск канала.
            Channel? channel = _Items.FirstOrDefault(x => x.Name == name) ?? throw ExceptionsCreator.ArgumentNotContainedInCollection(nameof(name));

            //  Возврат найденного канала.
            return channel;
        }
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<Channel> GetEnumerator()
    {
        //  Возврат перечислителя хранилища элементов.
        return ((IEnumerable<Channel>)_Items).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат перечислителя хранилища элементов.
        return ((IEnumerable)_Items).GetEnumerator();
    }
}
