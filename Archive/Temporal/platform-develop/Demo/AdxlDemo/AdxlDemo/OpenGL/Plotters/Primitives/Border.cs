using Apeiron.Platform.Demo.AdxlDemo.OpenGL.Windows;
using System.Drawing;

namespace Apeiron.Platform.Demo.AdxlDemo.OpenGL.Plotters;

/// <summary>
/// Представляет границу.
/// </summary>
public sealed class Border :
    PlotterPrimitive
{
    /// <summary>
    /// Возвращает или задаёт цвет границы.
    /// </summary>
    public Color Color { get; set; } = Color.DarkGray;

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
        //  Установка ширины линий.
        Original.LineWidth(1);

        //  Установка цвета линий.
        renderer.Color(Color);

        //  Отображение линий границы.
        Original.Begin(BeginMode.LineLoop);
        Original.Vertex(metrics.Padding.Left, metrics.Padding.Bottom);
        Original.Vertex(metrics.Width - metrics.Padding.Right, metrics.Padding.Bottom);
        Original.Vertex(metrics.Width - metrics.Padding.Right, metrics.Height - metrics.Padding.Top);
        Original.Vertex(metrics.Padding.Left, metrics.Height - metrics.Padding.Top);
        Original.End();
    }
}
