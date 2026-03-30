namespace Simargl.Embedded.Tunings.TuningsEditor.Core.Code.Writers;

/// <summary>
/// Представляет средство записи кода C++.
/// </summary>
public sealed class CppCodeWriter() :
    CodeWriter("\t")
{
    /// <summary>
    /// Записывает значение доступа.
    /// </summary>
    /// <param name="value">
    /// Значение доступа.
    /// </param>
    public void WriteAccess(string value)
    {
        //  Запись значения.
        DownIndent();
        WriteLine($"{value}:");
        UpIndent();
    }
}
