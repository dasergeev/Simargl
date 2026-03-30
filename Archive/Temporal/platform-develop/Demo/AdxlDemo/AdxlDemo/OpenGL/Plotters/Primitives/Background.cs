using Apeiron.Platform.Demo.AdxlDemo.OpenGL.Windows;
using System.Drawing;

namespace Apeiron.Platform.Demo.AdxlDemo.OpenGL.Plotters;

/// <summary>
/// Представляет фон.
/// </summary>
public sealed class Background :
    PlotterPrimitive
{
    /// <summary>
    /// Возвращает или задаёт цвет левой верхней точки.
    /// </summary>
    public Color TopLeftColor { get; set; } = Color.FromArgb(255, 255, 255);

    /// <summary>
    /// Возвращает или задаёт цвет правой верхней точки.
    /// </summary>
    public Color TopRightColor { get; set; } = Color.FromArgb(250, 250, 250);

    /// <summary>
    /// Возвращает или задаёт цвет левой нижней точки.
    /// </summary>
    public Color BottomLeftColor { get; set; } = Color.FromArgb(235, 235, 235);

    /// <summary>
    /// Возвращает или задаёт цвет правой нижней точки.
    /// </summary>
    public Color BottomRightColor { get; set; } = Color.FromArgb(230, 230, 230);

    /// <summary>
    /// Выполняет рендеринг.
    /// </summary>
    /// <param name="renderer">
    /// Средство рендеринга.
    /// </param>
    /// <param name="metrics">
    /// Метрики рабочего пространства рендеринга графиков.
    /// </param>
    public override void Render(Renderer renderer, PlotterWorkspaceMetrics metrics)
    {
        Original.Begin(BeginMode.Quads);

        renderer.Color(BottomLeftColor);
        Original.Vertex(metrics.XMin, metrics.YMin);

        renderer.Color(BottomRightColor);
        Original.Vertex(metrics.XMax, metrics.YMin);

        renderer.Color(TopRightColor);
        Original.Vertex(metrics.XMax, metrics.YMax);

        renderer.Color(TopLeftColor);
        Original.Vertex(metrics.XMin, metrics.YMax);

        Original.End();
    }
}
