using Apeiron.Platform.Demo.AdxlDemo.OpenGL.Windows;

namespace Apeiron.Platform.Demo.AdxlDemo.OpenGL.Primitives;

/// <summary>
/// Представляет примитив, отображаемый OpenGL.
/// </summary>
public abstract class Primitive
{
    /// <summary>
    /// Выполняет рендеринг.
    /// </summary>
    /// <param name="renderer">
    /// Средство рендеринга.
    /// </param>
    public abstract void Render(Renderer renderer);
}
