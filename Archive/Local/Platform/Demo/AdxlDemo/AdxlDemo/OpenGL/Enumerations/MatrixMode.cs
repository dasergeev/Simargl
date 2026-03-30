namespace Apeiron.Platform.Demo.AdxlDemo.OpenGL;

/// <summary>
/// Значение, определяющее режим матричных операций.
/// </summary>
public enum MatrixMode
{
    /// <summary>
    /// Матричные операции применяются к стеку матрицы вида.
    /// </summary>
    /// <remarks>
    /// Оригинальное имя: GL_MODELVIEW.
    /// </remarks>
    ModelView = 0x1700,

    /// <summary>
    /// Матричные операции применяются к стеку матрицы проекции.
    /// </summary>
    /// <remarks>
    /// Оригинальное имя: GL_PROJECTION.
    /// </remarks>
    Projection = 0x1701,

    /// <summary>
    /// Матричные операции применяются к стеку матриц текстур.
    /// </summary>
    /// <remarks>
    /// Оригинальное имя: GL_TEXTURE.
    /// </remarks>
    Texture = 0x1702,
}
