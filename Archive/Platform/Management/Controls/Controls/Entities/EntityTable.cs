using Apeiron.Platform.Databases.CentralDatabase.Entities;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Markup;

namespace Apeiron.Platform.Management.Controls;

/// <summary>
/// Представляет рабочий элемент управления, отображающий таблицу сущностей.
/// </summary>
/// <typeparam name="TEntity">
/// Тип сущности.
/// </typeparam>
public abstract class EntityTable<TEntity> :
    WorkControl
    where TEntity : Entity
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="columns">
    /// Коллекция столбцов.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal EntityTable([ParameterNoChecks] IEnumerable<GridViewColumn> columns)
    {
        //  Создание списка отображаемых сущностей.
        Entities = new();

        //  Создание элемента управления, отображающего список.
        ListView listView = new();

        //  Добавление списка к элементу управления.
        ((IAddChild)this).AddChild(listView);

        //  Создание режима просмотра.
        GridView gridView = new();

        //  Перебор столбцов.
        foreach (GridViewColumn column in columns)
        {
            //  Добавление столбца.
            gridView.Columns.Add(column);
        }

        //  Добавление режима просмотра к списку.
        listView.View = gridView;

        //  Установка источника списка.
        listView.ItemsSource = Entities;
    }

    /// <summary>
    /// Возвращает список отображаемых сущностей.
    /// </summary>
    protected ObservableCollection<TEntity> Entities { get; }
}
