using System.Reflection;

namespace Apeiron.Platform.Utilities.Generation.Schemes;

/// <summary>
/// Представляет схему свойства простого типа.
/// </summary>
public sealed class SimplePropertyScheme :
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
    internal SimplePropertyScheme(
        [ParameterNoChecks] EntityScheme entityScheme,
        [ParameterNoChecks] PropertyInfo propertyInfo) :
        base(entityScheme, propertyInfo)
    {
        //  Установка значения, определяющего, является ли значение свойства неповторимым.
        Uniquable = propertyInfo.GetCustomAttributes(
            typeof(UniquableAttribute), false).FirstOrDefault() is not null;

        //  Установка значения, определяющего, является ли значение свойства индексируемым.
        Indexable = propertyInfo.GetCustomAttributes(
            typeof(IndexableAttribute), false).FirstOrDefault() is not null;

        //  Установка значения, определяющего, может ли значение свойства принимать только положительные значения.
        Positive = propertyInfo.GetCustomAttributes(
            typeof(PositiveAttribute), false).FirstOrDefault() is not null;
    }

    /// <summary>
    /// Возвращает значение, определяющее, является ли значение свойства неповторимым.
    /// </summary>
    public bool Uniquable { get; }

    /// <summary>
    /// Возвращает значение, определяющее, является ли значение свойства индексируемым.
    /// </summary>
    public bool Indexable { get; }

    /// <summary>
    /// Возвращает значение, определяющее, может ли значение свойства принимать только положительные значения.
    /// </summary>
    public bool Positive { get; }
}
