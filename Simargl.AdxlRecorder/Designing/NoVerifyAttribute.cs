namespace Simargl.AdxlRecorder.Designing;

/// <summary>
/// Представляет атрибут, указывающий, что проверка значения параметра не выполняется.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
public sealed class NoVerifyAttribute :
    Attribute
{

}
