using Apeiron.Platform.Utilities.Sources;

namespace Apeiron.Platform.Utilities.Generation.Schemes;

/// <summary>
/// Представляет общую схему.
/// </summary>
public sealed class GeneralScheme :
    Scheme
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    internal GeneralScheme() :
        base(null!)
    {
        //  Создание коллекции схем проверки значений.
        Checkings = new();

        //  Создание коллекции схем простых типов.
        SimpleTypes = new();

        //  Создание коллекции схем сущностей.
        Entities = new(typeof(Entity).Assembly.GetTypes()
            .Where(type => type.IsSubclassOf(typeof(Entity)))
            .Select(type => EntityScheme.CreateScheme(this, type)));
    }

    /// <summary>
    /// Возвращает коллекцию схем проверки значений.
    /// </summary>
    public CheckingSchemeCollection Checkings { get; }

    /// <summary>
    /// Возвращает коллекцию схем простых типов.
    /// </summary>
    public SimpleTypeSchemeCollection SimpleTypes { get; }

    /// <summary>
    /// Возвращает коллекцию схем сущностей.
    /// </summary>
    public EntitySchemeCollection Entities { get; }
}
