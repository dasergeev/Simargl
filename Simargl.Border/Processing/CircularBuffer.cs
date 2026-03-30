using Simargl.Border.Hardware;

namespace Simargl.Border.Processing;

/// <summary>
/// Представляет циклический буфер.
/// </summary>
/// <typeparam name="T">
/// Тип элемента.
/// </typeparam>
/// <param name="size">
/// Размер буфера.
/// </param>
public sealed class CircularBuffer<T>(int size) :
    IEnumerable<T?>
{
    /// <summary>
    /// Поле для хранения массива элементов.
    /// </summary>
    private readonly T?[] _Items = new T?[size];

    /// <summary>
    /// Возвращает размер буфера.
    /// </summary>
    public int Size { get; } = size;

    /// <summary>
    /// Возвращает последний синхромаркер.
    /// </summary>
    public Synchromarker Synchromarker { get; private set; }

    /// <summary>
    /// Возвращает количество непрерывных синхромаркеров.
    /// </summary>
    public long Count { get; private set; }

    /// <summary>
    /// Возвращает элемент с указанным синхромаркером.
    /// </summary>
    /// <param name="synchromarker">
    /// Синхромаркер.
    /// </param>
    /// <returns>
    /// Элемент с указанным синхромаркером.
    /// </returns>
    public T? this[Synchromarker synchromarker]
    {
        get
        {
            //  Определение расхождения.
            long delta = Synchromarker - synchromarker;

            //  Проверка синхромаркера.
            if (delta < 0 || delta >= Size)
            {
                //  Нет доступного элемента.
                return default;
            }

            //  Получение индекса элемента.
            int index = (int)(synchromarker.Value % Size);

            //  Возврат элемента.
            return _Items[index];
        }
    }

    /// <summary>
    /// Регистрирует новый элемент.
    /// </summary>
    /// <param name="synchromarker">
    /// Синхромаркер.
    /// </param>
    /// <param name="item">
    /// Новый элемент.
    /// </param>
    public void Register(Synchromarker synchromarker, T item)
    {
        //  Получение индекса элемента.
        int index = (int)(synchromarker.Value % Size);

        //  Проверка последовательности синхромаркеров.
        if (synchromarker - Synchromarker == 1)
        {
            //  Увеличение счётчика.
            ++Count;
        }
        else
        {
            //  Перебор синхромаркеров.
            while (synchromarker - Synchromarker > 1)
            {
                //  Смещение синхромаркера.
                Synchromarker += 1;

                //  Сброс элемента.
                _Items[(int)(Synchromarker.Value % Size)] = default;
            }

            //  Сброс счётчика.
            Count = 1;
        }

        //  Установка элемента.
        _Items[index] = item;

        //  Обновление синхромаркера.
        Synchromarker = synchromarker;
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<T?> GetEnumerator()
    {
        return ((IEnumerable<T?>)_Items).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return _Items.GetEnumerator();
    }
}
