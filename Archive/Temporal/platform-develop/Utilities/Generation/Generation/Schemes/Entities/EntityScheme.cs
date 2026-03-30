namespace Apeiron.Platform.Utilities.Generation.Schemes;

/// <summary>
/// Представляет схему сущности.
/// </summary>
public abstract class EntityScheme :
    Scheme
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="generalScheme">
    /// Общая схема.
    /// </param>
    /// <param name="sourceType">
    /// Исходный тип.
    /// </param>
    /// <param name="isFinal">
    /// Значение, определяющее, является ли сущность итоговой.
    /// </param>
    internal EntityScheme(
        [ParameterNoChecks] GeneralScheme generalScheme,
        [ParameterNoChecks] Type sourceType,
        [ParameterNoChecks] bool isFinal) :
        base(generalScheme)
    {
        //  Установка значения, определяющего, является ли сущность итоговой.
        IsFinal = isFinal;

        //  Установка названия сущности.
        Name = ((NameAttribute)sourceType.GetCustomAttributes(
            typeof(NameAttribute), false).First()).Name;

        //  Установка категории сущности.
        Category = ((CategoryAttribute)sourceType.GetCustomAttributes(
            typeof(CategoryAttribute), false).First()).Category;

        //  Установка имя типа сущности.
        TypeName = sourceType.Name;

        //  Установка имени родительской сущности.
        ParentTypeName = sourceType.BaseType!.Name;

        //  Создание коллекции свойств сущности.
        Properties = new(sourceType
            .GetProperties()
            .Where(propertyInfo => propertyInfo.DeclaringType == sourceType)
            .OrderBy(propertyInfo => ((OrderAttribute)propertyInfo.GetCustomAttributes(
                typeof(OrderAttribute), false).First()).Number)
            .Select(propertyInfo => PropertyScheme.CreateScheme(this, propertyInfo)));

        //  Создание коллекции схем альтернативных ключей.
        AlternateKeys = new(
            sourceType.GetCustomAttributes(
                typeof(AlternateKeyAttribute), true)
            .Select(attribute => new AlternateKeyScheme(this, (AlternateKeyAttribute)attribute)));
    }

    /// <summary>
    /// Возвращает значение, определяющее, является ли сущность итоговой.
    /// </summary>
    public bool IsFinal { get; }

    /// <summary>
    /// Возвращает название сущности.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Возвращает категорию сущности.
    /// </summary>
    public string Category { get; }

    /// <summary>
    /// Возвращает имя типа сущности.
    /// </summary>
    public string TypeName { get; }

    /// <summary>
    /// Возвращает имя родительской сущности.
    /// </summary>
    public string ParentTypeName { get; }

    /// <summary>
    /// Возвращает коллекцию свойств сущности.
    /// </summary>
    public PropertySchemeCollection Properties { get; }

    /// <summary>
    /// Возвращает коллекцию схем альтернативных ключей.
    /// </summary>
    public AlternateKeySchemeCollection AlternateKeys { get; }

    /// <summary>
    /// Создаёт схему сущности.
    /// </summary>
    /// <param name="generalScheme">
    /// Общая схема.
    /// </param>
    /// <param name="sourceType">
    /// Исходный тип.
    /// </param>
    /// <returns>
    /// Схема сущности.
    /// </returns>
    public static EntityScheme CreateScheme(
        [ParameterNoChecks] GeneralScheme generalScheme,
        [ParameterNoChecks] Type sourceType)
    {
        //  Проверка абстрактности класса.
        if (sourceType.IsAbstract)
        {
            //  Создание схемы родительской сущности.
            return new ParentEntityScheme(generalScheme, sourceType);
        }
        else
        {
            //  Создание схемы итоговой сущности.
            return new FinalEntityScheme(generalScheme, sourceType);
        }
    }
}
