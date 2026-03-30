using Apeiron.Platform.Utilities.Sources;
using System.Reflection;

namespace Apeiron.Platform.Utilities.Generation.Schemes;

/// <summary>
/// Представляет схему свойства.
/// </summary>
public abstract class PropertyScheme :
    Scheme
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="entityScheme">
    /// Схема сущности.
    /// </param>
    /// <param name="propertyInfo">
    /// Информация о свойстве.
    /// </param>
    internal PropertyScheme(
        [ParameterNoChecks] EntityScheme entityScheme,
        [ParameterNoChecks] PropertyInfo propertyInfo) :
        base(entityScheme.GeneralScheme)
    {
        //  Установка схемы сущности.
        EntityScheme = entityScheme;

        //  Установка названия свойства.
        Name = ((NameAttribute)propertyInfo.GetCustomAttributes(
            typeof(NameAttribute), false).First()).Name;

        //  Установка отображаемого имени свойства.
        DisplayName = ((DisplayNameAttribute)propertyInfo.GetCustomAttributes(
            typeof(DisplayNameAttribute), false).First()).DisplayName;

        //  Установка имени свойства.
        PropertyName = propertyInfo.Name;

        //  Установка имени параметра.
        ParameterName = PropertyName[..1].ToLower() + PropertyName[1..];

        //  Установка типа значения свойства.
        TypeName = propertyInfo.PropertyType.Name;

        //  Установка значения, определяющего, может ли значение быть пустым.
        Nullable = propertyInfo.GetCustomAttributes(
            typeof(NullableAttribute), false).FirstOrDefault() is not null;
    }

    /// <summary>
    /// Возвращает схему сущности.
    /// </summary>
    internal EntityScheme EntityScheme { get; }

    /// <summary>
    /// Возвращает название свойства.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Возвращает отображаемое имя свойства.
    /// </summary>
    public string DisplayName { get; }

    /// <summary>
    /// Возвращает имя свойства.
    /// </summary>
    public string PropertyName { get; }

    /// <summary>
    /// Возвращает имя параметра.
    /// </summary>
    public string ParameterName { get; }

    /// <summary>
    /// Возвращает имя типа значения свойства.
    /// </summary>
    public string TypeName { get; }

    /// <summary>
    /// Возвращает значение, определяющее, может ли значение быть пустым.
    /// </summary>
    public bool Nullable { get; }

    /// <summary>
    /// Создаёт схему свойства.
    /// </summary>
    /// <param name="entityScheme">
    /// Схема сущности.
    /// </param>
    /// <param name="propertyInfo">
    /// Информация о свойстве.
    /// </param>
    /// <returns>
    /// Схема свойства.
    /// </returns>
    public static PropertyScheme CreateScheme(
        [ParameterNoChecks] EntityScheme entityScheme,
        [ParameterNoChecks] PropertyInfo propertyInfo)
    {
        //  Проверка связи.
        if (propertyInfo.PropertyType.IsSubclassOf(typeof(Entity)))
        {
            //  Создание схемы свойства связи.
            return new RelationPropertyScheme(entityScheme, propertyInfo);
        }
        else
        {
            //  Создание схемы свойства простого типа.
            return new SimplePropertyScheme(entityScheme, propertyInfo);
        }
    }
}
