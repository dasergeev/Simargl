using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Runtime.CompilerServices;

namespace Apeiron.Platform.Communication.CommunicationDatabase;

/// <summary>
/// Представляет сущность базы данных.
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// Возвращает или задаёт идентификатор.
    /// </summary>
    public long ID { get; set; }

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <typeparam name="TEntity">
    /// Тип сущности.
    /// </typeparam>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction<TEntity>(EntityTypeBuilder<TEntity> typeBuilder)
        where TEntity : Entity
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(entity => entity.ID);
    }
}
