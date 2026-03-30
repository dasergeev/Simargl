namespace Apeiron.Platform.Demo.AdxlDemo.OpenGL;

/// <summary>
/// Представляет значение, определяющее режим компиляции списка отображения.
/// </summary>
public enum ListMode
{
    /// <summary>
    /// Команды просто компилируются.
    /// </summary>
    /// <remarks>
    /// Оригинальное имя: GL_COMPILE.
    /// </remarks>
    Compile = 0x1300,

    /// <summary>
    /// Команды выполняются по мере их компиляции в отображаемый список.
    /// </summary>
    /// <remarks>
    /// Оригинальное имя: GL_COMPILE_AND_EXECUTE.
    /// </remarks>
    CompileAndExecute = 0x1301,
}
