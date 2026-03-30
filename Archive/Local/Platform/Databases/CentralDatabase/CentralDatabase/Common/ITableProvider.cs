namespace Apeiron.Platform.Databases.CentralDatabase;

/// <summary>
/// Представляет поставщика таблицы базы данных.
/// </summary>
/// <typeparam name="TEntity">
/// Тип сущности.
/// </typeparam>
internal interface ITableProvider<TEntity>
    where TEntity : Entity
{
    /// <summary>
    /// Возвращает таблицу базы данных.
    /// </summary>
    Table<TEntity> Table { get; }
}
