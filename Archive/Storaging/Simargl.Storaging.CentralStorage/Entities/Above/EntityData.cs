namespace Simargl.Storaging.CentralStorage.Entities.Above;

/// <summary>
/// Представляет данные сущности.
/// </summary>
public abstract class EntityData
{
    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    internal static void BuildAction<TEntity>(
        EntityTypeBuilder<TEntity> typeBuilder)
        where TEntity : EntityData
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(x => x.Key);

        //  Настройка автоинкремента ключа.
        typeBuilder.Property(x => x.Key).ValueGeneratedOnAdd();
    }

    /// <summary>
    /// Возвращает или инициализирует ключ сущности.
    /// </summary>
    public long Key { get; init; }
}
