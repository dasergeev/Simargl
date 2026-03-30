using Apeiron.Platform.Demo.AdxlDemo.OpenGL.Windows;
using System.Drawing;

namespace Apeiron.Platform.Demo.AdxlDemo.OpenGL.Plotters;

/// <summary>
/// Представляет сетку.
/// </summary>
public sealed class Grid :
    PlotterPrimitive
{
    /// <summary>
    /// Возвращает или задаёт цвет сетки.
    /// </summary>
    public Color Color { get; set; } = Color.LightGray;

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

        //  Отображение вертикальных линий.
        double x = metrics.XGridBegin;
        Original.Begin(BeginMode.Lines);
        while (x <= metrics.XMax)
        {
            Original.Vertex(x, metrics.YMin);
            Original.Vertex(x, metrics.YMax);
            x += metrics.XGridStep;
        }
        Original.End();

        //  Отображение горизонтальных линий.
        double y = metrics.YGridBegin;
        Original.Begin(BeginMode.Lines);
        while (y <= metrics.YMax)
        {
            Original.Vertex(metrics.XMin, y);
            Original.Vertex(metrics.XMax, y);
            y += metrics.YGridStep;
        }
        Original.End();
    }
}
