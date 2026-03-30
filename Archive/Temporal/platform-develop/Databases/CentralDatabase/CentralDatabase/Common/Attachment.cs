namespace Apeiron.Platform.Databases.CentralDatabase;

/// <summary>
/// Представляет объект, для прикрепления сущностей к контексту.
/// </summary>
internal sealed class Attachment
{
    /// <summary>
    /// Поле для хранения списка прикреплённых сущностей.
    /// </summary>
    private readonly List<Entity> _Entities;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="context">
    /// Контекст, к которому необходимо прикреплять сущности.
    /// </param>
    public Attachment([ParameterNoChecks] CentralDatabaseContext context)
    {
        //  Установка контекста.
        Context = context;

        //  Создание списка прикреплённых сущностей.
        _Entities = new();

        ////  Перебор текущих вхождений.
        //foreach (EntityEntry entityEntry in Context.ChangeTracker.Entries())
        //{
        //    //  Проверка типа сущности.
        //    if (entityEntry.Entity is Entity entity)
        //    {
        //        //  Добавление сущности в список.
        //        _Entities.Add(entity);
        //    }
        //}
    }

    /// <summary>
    /// Возвращает контекст сеанса работы с центральной базой данных.
    /// </summary>
    public CentralDatabaseContext Context { get; }

    /// <summary>
    /// Прикрепляет сущность.
    /// </summary>
    /// <param name="entity">
    /// Сущность, которую необходимо прикрепить.
    /// </param>
    /// <returns>
    /// Значение, определяющее, следует ли прикреплять связанные сущности.
    /// </returns>
    public bool Attach(Entity entity)
    {
        //  Проверка содержания сущности в списке.
        if (_Entities.Contains(entity))
        {
            //  Сущность уже присоединена.
            return false;
        }

        //  Добавление сущности в список.
        _Entities.Add(entity);

        //  Проверка необходимости присоединения сущности к контексту.
        if (entity.Id != default && Context.Attach(entity).State == EntityState.Added)
        {
            //  Присоединение сущности к контексту.
            Context.Attach(entity).State = EntityState.Modified;
        }

        //  Необходимо прикрепить связанные сущности.
        return true;
    }
}
