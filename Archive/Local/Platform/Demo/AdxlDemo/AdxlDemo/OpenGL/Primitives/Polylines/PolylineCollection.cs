using Apeiron.Platform.Demo.AdxlDemo.OpenGL.Windows;
using System.Collections;
using System.Collections.Concurrent;

namespace Apeiron.Platform.Demo.AdxlDemo.OpenGL.Primitives;

/// <summary>
/// Представляет коллекцию ломаных линий.
/// </summary>
public sealed class PolylineCollection :
    Primitive,
    IEnumerable<Polyline>
{
    /// <summary>
    /// Поле для хранения критического объекта.
    /// </summary>
    private readonly object _SyncRoot;

    /// <summary>
    /// Поле для хранения элементов.
    /// </summary>
    private readonly ConcurrentBag<Polyline> _Items;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public PolylineCollection()
    {
        //  Создание критического объекта.
        _SyncRoot = new();

        //  Создание хранилища элементов.
        _Items = new();

        //  Коллекция пуста.
        IsEmpty = true;
    }

    /// <summary>
    /// Добавляет коллекцию ломаных линий.
    /// </summary>
    /// <param name="polylines">
    /// Коллекция ломаных линий.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="polylines"/> передана пустая ссылка.
    /// </exception>
    public void AddRange(IEnumerable<Polyline> polylines)
    {
        //  Проверка ссылки.
        IsNotNull(polylines, nameof(polylines));

        //  Перебор элементов коллекции.
        foreach (Polyline polyline in polylines)
        {
            //  Добавление ломаной линии.
            Add(polyline);
        }
    }

    /// <summary>
    /// Добавляет новую ломаную линию.
    /// </summary>
    /// <param name="polyline">
    /// Ломаная линия.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="polyline"/> передана пустая ссылка.
    /// </exception>
    public void Add(Polyline polyline)
    {
        //  Проверка ссылки.
        IsNotNull(polyline, nameof(polyline));

        //  Проверка необходимости добавления.
        if (polyline.IsEmpty)
        {
            //  Пустая ломаная линия.
            return;
        }

        //  Установка значения, определяющего, является ли коллекция пустой.
        IsEmpty = false;

        //  Блокировка критического объекта.
        lock (_SyncRoot)
        {
            //  Добавление элемента в коллекцию.
            _Items.Add(polyline);

            //  Корректировка предельных значений.
            XMin = Math.Min(XMin, polyline.XMin);
            XMax = Math.Max(XMax, polyline.XMax);
            YMin = Math.Min(YMin, polyline.YMin);
            YMax = Math.Max(YMax, polyline.YMax);
        }
    }

    /// <summary>
    /// Асинхронно добавляет элемент в коллекцию.
    /// </summary>
    /// <param name="polyline">
    /// Новый элемент.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, добавляющая новый элемент в коллекцию.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="polyline"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task AddAsync(Polyline polyline, CancellationToken cancellationToken)
    {
        //  Проверка ссылки.
        IsNotNull(polyline, nameof(polyline));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Асинхронное выполение.
        await Task.Run(delegate
        {
            //  Добавление элемента.
            Add(polyline);
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Возвращает значение, определяющее, является ли коллекция пустой.
    /// </summary>
    public bool IsEmpty { get; private set; }

    /// <summary>
    /// Возвращает минимальное значение по оси Ox.
    /// </summary>
    public double XMin { get; private set; } = double.MaxValue;

    /// <summary>
    /// Возвращает максимального значение по оси Ox.
    /// </summary>
    public double XMax { get; private set; } = double.MinValue;

    /// <summary>
    /// Возвращает минимальное значение по оси Oy.
    /// </summary>
    public double YMin { get; private set; } = double.MaxValue;

    /// <summary>
    /// Возвращает максимального значение по оси Oy.
    /// </summary>
    public double YMax { get; private set; } = double.MinValue;

    /// <summary>
    /// Выполняет рендеринг.
    /// </summary>
    /// <param name="renderer">
    /// Средство рендеринга.
    /// </param>
    public override void Render(Renderer renderer)
    {
        //  Перебор коллекции.
        foreach (Polyline polyline in _Items)
        {
            //  Отображение ломаной линии.
            polyline.Render(renderer);
        }
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<Polyline> GetEnumerator()
    {
        //  Возврат перечислителя хранилища элементов.
        return ((IEnumerable<Polyline>)_Items).GetEnumerator();
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
