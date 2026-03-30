using Microsoft.EntityFrameworkCore;

namespace Simargl.Entities;

/// <summary>
/// Представляет поставщика таблицы данных.
/// </summary>
/// <typeparam name="TEntityData">
/// Тип данных сущности.
/// </typeparam>
[CLSCompliant(false)]
public interface IDataTableProvider<TEntityData>
    where TEntityData : EntityData
{
    /// <summary>
    /// Возвращает набор данных таблицы.
    /// </summary>
    public DbSet<TEntityData> GetSet();
}
