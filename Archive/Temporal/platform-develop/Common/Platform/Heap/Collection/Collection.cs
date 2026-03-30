using System.Collections;

namespace Apeiron.Collections;

/// <summary>
/// Представляет коллекцию.
/// </summary>
/// <typeparam name="T">
/// Тип элементов коллекции.
/// </typeparam>
public abstract class Collection<T> :
    IEnumerable<T>
{
    /// <summary>
    /// Инициалазирует новый экземпляр класса.
    /// </summary>
    /// <remarks>
    /// Используются настройки по умолчанию <see cref="CollectionOptions.Default"/>.
    /// </remarks>
    public Collection() :
        this(CollectionOptions.Default)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="options">
    /// Настройки коллекции.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="options"/> передана пустая ссылка.
    /// </exception>
    public Collection(CollectionOptions options) :
        this(new CollectionProvider<T>(options))
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="provider">
    /// Поставщик коллекции.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="provider"/> передана пустая ссылка.
    /// </exception>
    internal Collection(CollectionProvider<T> provider)
    {
        //  Установка поставщика коллекции.
        Provider = Check.IsNotNull(provider, nameof(provider));
    }

    /// <summary>
    /// Возвращает поставщика коллекции.
    /// </summary>
    protected CollectionProvider<T> Provider { get; }

    /// <summary>
    /// Возвращает объект, с помощью которого можно синхронизировать доступ к коллекции.
    /// </summary>
    public object SyncRoot => Provider.SyncRoot;

    /// <summary>
    /// Возвращает настройки коллекции.
    /// </summary>
    public CollectionOptions Options => Provider.Options;

    /// <summary>
    /// Возвращает количество элементов в коллекции.
    /// </summary>
    public int Count => Provider.Count;

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
    public bool Contains(T item) => Provider.Contains(item);

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
    public int IndexOf(T item) => Provider.IndexOf(item);

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
    public void CopyTo(T[] array, int index) => Provider.CopyTo(array, index);

    /// <summary>
    /// Возвращает массив элементов коллекции.
    /// </summary>
    /// <returns>
    /// Массив элементов коллекции.
    /// </returns>
    public T[] ToArray() => Provider.ToArray();

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<T> GetEnumerator()
    {
        //  Возврат перечислителя поставщика.
        return Provider.GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат перечислителя поставщика.
        return ((IEnumerable)Provider).GetEnumerator();
    }
}
