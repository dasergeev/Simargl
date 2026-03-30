namespace Apeiron.Platform.Demo.AdxlDemo.OpenGL;

/// <summary>
/// Представляет значение, определяющее маску очищаемых буферов.
/// </summary>
[Flags]
public enum ClearBufferMask
{
    /// <summary>
    /// Буфер глубины.
    /// </summary>
    /// <remarks>
    /// Оригинальное имя: GL_DEPTH_BUFFER_BIT.
    /// </remarks>
    Depth = 0x00000100,

    /// <summary>
    /// Буфер накопления.
    /// </summary>
    /// <remarks>
    /// Оригинальное имя: GL_ACCUM_BUFFER_BIT.
    /// </remarks>
    Accumulation = 0x00000200,

    /// <summary>
    /// Буфер трафарета.
    /// </summary>
    /// <remarks>
    /// Оригинальное имя: GL_STENCIL_BUFFER_BIT.
    /// </remarks>
    Stencil = 0x00000400,

    /// <summary>
    /// Буферы, которые в настоящее время включены для записи цветов.
    /// </summary>
    /// <remarks>
    /// Оригинальное имя: GL_COLOR_BUFFER_BIT.
    /// </remarks>
    Color = 0x00004000,
}
