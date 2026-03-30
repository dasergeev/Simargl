using Simargl;

namespace Simargl.Designing;

/// <summary>
/// Представляет атрибут, указывающий, что проверка значения параметра не выполняется.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
public sealed class NoVerifyAttribute :
    Attribute,
    IAnything
{
    /// <summary>
    /// Возвращает базу объекта.
    /// </summary>
    /// <returns>
    /// База объекта.
    /// </returns>
    public AnythingBasis GetBasis()
    {
        //  Возврат исходной базы объекта.
        return this.GetOriginalBasis();
    }
}
