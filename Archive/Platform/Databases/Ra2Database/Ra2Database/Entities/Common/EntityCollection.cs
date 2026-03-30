using System.Runtime.CompilerServices;

namespace ApeironApeiron.Platform.Databases.Ra2Database.Entities;

/// <summary>
/// Представляет коллекцию сущностей.
/// </summary>
/// <typeparam name="TEntity">
/// Тип сущности.
/// </typeparam>
public sealed class EntityCollection<TEntity> :
    HashSet<TEntity>
    where TEntity : Entity
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal EntityCollection()
    {

    }
}

