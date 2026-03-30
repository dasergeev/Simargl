namespace Apeiron.Support;

/// <summary>
/// Представляет атрибут, сообщающий о том, что значение параметра не проверяется. 
/// </summary>
[AttributeUsage(AttributeTargets.Parameter)]
public class ParameterNoChecksAttribute :
    Attribute
{

}
