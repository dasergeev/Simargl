using System.Reflection;

namespace Apeiron.Platform.Utilities.Generation.Schemes;

/// <summary>
/// Представляет схему свойства связи.
/// </summary>
public sealed class RelationPropertyScheme :
    PropertyScheme
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
    internal RelationPropertyScheme(
        [ParameterNoChecks] EntityScheme entityScheme,
        [ParameterNoChecks] PropertyInfo propertyInfo) :
        base(entityScheme, propertyInfo)
    {
        //  Установка имени коллекции сущностей.
        RelationCollectionName = ((CollectionNameAttribute)propertyInfo.DeclaringType!.GetCustomAttributes(
            typeof(CollectionNameAttribute), false).First()).CollectionName;

        //  Установка значения, указывающего на каскадное удаление связанных сущностей.
        Cascade = propertyInfo.GetCustomAttributes(
            typeof(CascadeAttribute), false).FirstOrDefault() is not null;
    }

    /// <summary>
    /// Возвращает имя коллекции сущностей.
    /// </summary>
    public string RelationCollectionName { get; }

    /// <summary>
    /// Возвращает значение, указывающее на каскадное удаление связанных сущностей.
    /// </summary>
    public bool Cascade { get; }
}
