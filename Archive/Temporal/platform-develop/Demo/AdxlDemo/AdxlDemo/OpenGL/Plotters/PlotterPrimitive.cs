using Apeiron.Platform.Demo.AdxlDemo.OpenGL.Windows;

namespace Apeiron.Platform.Demo.AdxlDemo.OpenGL.Plotters;

/// <summary>
/// Представляет примитив, отображаемый при рендеринге графиков в OpenGL.
/// </summary>
public abstract class PlotterPrimitive
{
    /// <summary>
    /// Выполняет рендеринг.
    /// </summary>
    /// <param name="renderer">
    /// Средство рендеринга.
    /// </param>
    /// <param name="metrics">
    /// Метрики рабочего пространства рендеринга графиков.
    /// </param>
    public abstract void Render(Renderer renderer, PlotterWorkspaceMetrics metrics);
}
