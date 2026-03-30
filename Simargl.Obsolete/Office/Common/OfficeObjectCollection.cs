using System.Collections;
using System.Collections.Generic;

namespace Simargl.Office;

/// <summary>
/// Представляет коллекцию объектов Office.
/// </summary>
/// <typeparam name="TApplication">
/// Тип приложения Office.
/// </typeparam>
/// <typeparam name="TObject">
/// Тип объекта Office.
/// </typeparam>
public abstract class OfficeObjectCollection<TApplication, TObject> :
    OfficeObject<TApplication>,
    IEnumerable<TObject>
    where TApplication : OfficeApplication<TApplication>
    where TObject : OfficeObject<TApplication>
{
    /// <summary>
    /// Поле для хранения метода, создающего объекты.
    /// </summary>
    private readonly Func<TApplication, object, TObject> _Creator;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="application">
    /// Приложение.
    /// </param>
    /// <param name="obj">
    /// COM-объект.
    /// </param>
    /// <param name="creator">
    /// Метод, создающий элементы коллекции.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="application"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="obj"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="creator"/> передана пустая ссылка.
    /// </exception>
    internal OfficeObjectCollection(TApplication application, object obj, Func<TApplication, object, TObject> creator) :
        base(application, obj)
    {
        //  Установка метода, создающего объекты.
        _Creator = IsNotNull(creator, nameof(creator));
    }

    /// <summary>
    /// Возвращает количество элементов в коллекции.
    /// </summary>
    public int Count => Invoke(comObject => comObject.Count);

    /// <summary>
    /// Возвращает элемент с указанным индексом.
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
    /// В параметре <paramref name="index"/> передано значение большее или равное <see cref="Count"/>.
    /// </exception>
    public TObject this[int index]
    {
        get
        {
            //  Проверка индекса.
            IsIndex(index, Count, nameof(index));

            //  Получение COM-объекта.
            var comObject = Invoke(comObject => comObject[index + 1]);

            //  Создание объекта.
            TObject obj = _Creator(Application, comObject);

            //  Добавление в коллекцию вложенных оболочек.
            NestedWrappers.Add(obj);

            //  Возврат объекта.
            return obj;
        }
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<TObject> GetEnumerator()
    {
        //  Блокировка критического объекта.
        lock (SyncRoot)
        {
            //  Определение количества элементов в коллекции.
            int count = Count;

            //  Создание массива для элементов.
            TObject[] items = new TObject[count];

            //  Перебор всех элементов.
            for (int i = 0; i < count; i++)
            {
                //  Получение элемента.
                items[i] = this[i];
            }

            //  Возврат перечислителя массива элементов.
            return ((IEnumerable<TObject>)items).GetEnumerator();
        }
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
