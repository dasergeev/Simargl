using Apeiron.Platform.Databases.CentralDatabase;
using Apeiron.Platform.Databases.CentralDatabase.Entities;
using System.Windows.Data;

namespace Apeiron.Platform.Management.Controls;

/// <summary>
/// Представляет элемент управления, отображающий таблицы базы данных.
/// </summary>
public sealed class DatabaseTablesView :
    EntityTable<DatabaseTable>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public DatabaseTablesView() :
        base(new List<GridViewColumn>
        {
            new()
            {
                DisplayMemberBinding = new Binding("Name"),
                Header = "Имя таблицы",
            },
            new()
            {
                DisplayMemberBinding = new Binding("Count"),
                Header = "Количество элементов",
            },
            new()
            {
                DisplayMemberBinding = new Binding("ChangedInHour"),
                Header = "За час",
            },
            new()
            {
                DisplayMemberBinding = new Binding("ChangedInDay"),
                Header = "За день",
            },
            new()
            {
                DisplayMemberBinding = new Binding("ChangedInMonth"),
                Header = "За месяц",
            },
            new()
            {
                DisplayMemberBinding = new Binding("SpeedPerHour"),
                Header = "Элементов в час",
            },
            new()
            {
                DisplayMemberBinding = new Binding("SpeedPerDay"),
                Header = "Элементов в день",
            },
            new()
            {
                DisplayMemberBinding = new Binding("SpeedPerMonth"),
                Header = "Элементов в месяц",
            }
        })
    {

    }

    /// <summary>
    /// Устанавливает текущий узел.
    /// </summary>
    /// <param name="node">
    /// Текущий узел.
    /// </param>
    internal override void SetNode(ModelNode? node)
    {
        var entities = CentralDatabaseAgent.Request(session => session.DatabaseTables.ToList());

        SortedDictionary<long, Tuple<int, DatabaseTable>> map = new();
        for (int i = 0; i < Entities.Count; i++)
        {
            var entity = Entities[i];
            map.Add(entity.Id, new(i, entity));
        }

        List<DatabaseTable> added = new();
        List<DatabaseTable> removed = new(Entities);

        foreach (var item in entities)
        {
            if (map.TryGetValue(item.Id, out Tuple<int, DatabaseTable>? value))
            {
                var tuple = value;
                Entities[tuple.Item1] = item;
                removed.Remove(item);
            }
            else
            {
                added.Add(item);
            }
        }

        foreach (var item in removed)
        {
            Entities.Remove(item);
        }

        foreach (var item in added)
        {
            Entities.Add(item);
        }
    }
}
