namespace Apeiron.Platform.Utilities.Generation.Attributes;

/// <summary>
/// Представляет атрибут, указывающий на порядок.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public sealed class OrderAttribute :
    Attribute
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="number">
    /// Номер.
    /// </param>
    public OrderAttribute([ParameterNoChecks] int number)
    {
        //  Установка номера.
        Number = number;
    }

    /// <summary>
    /// Возвращает номер.
    /// </summary>
    public int Number { get; }
}
