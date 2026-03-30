using System.Collections;
using System.Runtime.CompilerServices;
using System.Windows.Markup;

namespace Apeiron.Platform.Management.Controls;

/// <summary>
/// Представляет коллекцию рабочих элементов.
/// </summary>
public sealed class WorkControlCollection :
    IEnumerable<WorkControl>
{
    /// <summary>
    /// Поле для хранения рабочего пространства.
    /// </summary>
    private readonly WorkSpace _WorkSpace;

    /// <summary>
    /// Поле для хранения сетки элементов.
    /// </summary>
    private readonly Grid _Grid;

    /// <summary>
    /// Поле для хранения карты элементов коллекции.
    /// </summary>
    private readonly SortedDictionary<WorkControlFormat, WorkControl> _Map;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="workSpace">
    /// Рабочее пространство.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="workSpace"/> передана пустая ссылка.
    /// </exception>
    internal WorkControlCollection(WorkSpace workSpace)
    {
        //  Установка рабочего пространства.
        _WorkSpace = Check.IsNotNull(workSpace, nameof(workSpace));

        //  Создание карты элементов коллекции.
        _Map = new();

        //  Создание сетки элементов.
        _Grid = new();

        //  Добавление сетки элементов.
        ((IAddChild)_WorkSpace).AddChild(_Grid);
    }

    /// <summary>
    /// Возвращает рабочий элемент управления заданного формата.
    /// </summary>
    /// <param name="format">
    /// Формат рабочего элемента управления.
    /// </param>
    /// <returns>
    /// Рабочий элемент управления.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="format"/> передано значение,
    /// которое не содержится в перечислении <see cref="WorkControlFormat"/>.
    /// </exception>
    public WorkControl this[WorkControlFormat format]
    {
        get
        {
            //  Проверка содержания элемента в карте.
            if (_Map.TryGetValue(format, out WorkControl? value))
            {
                //  Возврат элемента управления из карты.
                return value;
            }
            else
            {
                //  Создание элемента управления.
                WorkControl control = CreateControl(format);

                //  Добавление элемента в карту.
                _Map.Add(format, control);

                //  Возврат добавленного элемента управления.
                return control;
            }
        }
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<WorkControl> GetEnumerator()
    {
        //  Возврат перечислителя карты элементов коллекции.
        return _Map.Values.GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат универсального перечислителя коллекции.
        return GetEnumerator();
    }

    /// <summary>
    /// Создаёт элемент управления.
    /// </summary>
    /// <param name="format">
    /// Формат элемента управления.
    /// </param>
    /// <returns>
    /// Новый элемент управления.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="format"/> передано значение,
    /// которое не содержится в перечислении <see cref="WorkControlFormat"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private WorkControl CreateControl(WorkControlFormat format)
    {
        //  Создание элемента управления.
        WorkControl control = format switch
        {
            WorkControlFormat.Default => new DefaultView(),
            WorkControlFormat.DatabaseTables => new DatabaseTablesView(),
            _ => throw Exceptions.ArgumentNotContainedInEnumeration<WorkControlFormat>(nameof(format)),
        };

        //  Настройка элемента управления.
        control.Visibility = Visibility.Collapsed;

        //  Добавление элемента управления.
        ((IAddChild)_Grid).AddChild(control);

        //  Возврат элемента управления.
        return control;
    }
}
