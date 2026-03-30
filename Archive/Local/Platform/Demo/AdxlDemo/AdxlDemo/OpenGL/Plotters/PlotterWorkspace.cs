using System.Windows.Forms;

namespace Apeiron.Platform.Demo.AdxlDemo.OpenGL.Plotters;

/// <summary>
/// Представляет рабочее пространство рендеринга графиков.
/// </summary>
public sealed class PlotterWorkspace
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public PlotterWorkspace()
    {
        //  Установка отступов рабочего пространства.
        Padding = new(64, 16, 16, 32);

        //  Создание метрик.
        Metrics = new(this);

        //  Создание фона.
        Background = new();

        //  Создание сетки.
        Grid = new();

        //  Создание границы.
        Border = new();

        //  Создание осей.
        Axes = new();
    }

    /// <summary>
    /// Возвращает отступы рабочего пространства.
    /// </summary>
    public Padding Padding { get; }

    /// <summary>
    /// Возвращает метрики.
    /// </summary>
    public PlotterWorkspaceMetrics Metrics { get; }

    /// <summary>
    /// Возвращает фон.
    /// </summary>
    public Background Background { get; }

    /// <summary>
    /// Возвращает сетку.
    /// </summary>
    public Grid Grid { get; }

    /// <summary>
    /// Возвращает границу.
    /// </summary>
    public Border Border { get; }

    /// <summary>
    /// Возвращает оси.
    /// </summary>
    public Axes Axes { get; }
}
