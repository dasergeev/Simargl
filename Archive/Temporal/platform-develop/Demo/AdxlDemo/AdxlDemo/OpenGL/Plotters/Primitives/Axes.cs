using Apeiron.Platform.Demo.AdxlDemo.OpenGL.Windows;
using System.Drawing;

namespace Apeiron.Platform.Demo.AdxlDemo.OpenGL.Plotters;

/// <summary>
/// Представляет оси.
/// </summary>
public sealed class Axes :
    PlotterPrimitive
{
    /// <summary>
    /// Поле для хранения преобразователя значений по оси Ox.
    /// </summary>
    private Func<double, double> _XСonverter;

    /// <summary>
    /// Поле для хранения преобразователя значений по оси Oy.
    /// </summary>
    private Func<double, double> _YСonverter;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public Axes()
    {
        //  Создание преобразователей.
        _XСonverter = x => x;
        _YСonverter = x => x;
    }

    /// <summary>
    /// Возвращает или задаёт цвет текста.
    /// </summary>
    public Color TextColor { get; set; } = Color.Black;

    /// <summary>
    /// Возвращает или задаёт преобразователя значений по оси Ox.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// В параметре передана пустая ссылка.
    /// </exception>
    public Func<double, double> XСonverter
    {
        get => _XСonverter;
        set => _XСonverter = IsNotNull(value, nameof(XСonverter));
    }

    /// <summary>
    /// Возвращает или задаёт преобразователя значений по оси Oy.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// В параметре передана пустая ссылка.
    /// </exception>
    public Func<double, double> YСonverter
    {
        get => _YСonverter;
        set => _YСonverter = IsNotNull(value, nameof(YСonverter));
    }

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
        //  Установка цвета.
        renderer.Color(TextColor);

        //  Отображение горизонтальных подписей.
        double x = metrics.XGridBegin;
        while (x <= metrics.XMax)
        {
            int xLabel = (int)(metrics.XScale * x + metrics.XOffset);
            if (xLabel >= metrics.Padding.Left && xLabel <= metrics.Width - metrics.Padding.Right)
            {
                renderer.Text(xLabel, 10, _XСonverter(x).ToString("0.#####"));
            }
            x += metrics.XGridStep;
        }

        //  Отображение вертикальных подписей.
        double y = metrics.YGridBegin;
        while (y <= metrics.YMax)
        {
            int yLabel = (int)(metrics.YScale * y + metrics.YOffset);
            if (yLabel >= metrics.Padding.Top && yLabel <= metrics.Height - metrics.Padding.Bottom)
            {
                renderer.Text(5, metrics.Height - yLabel - 5, _YСonverter(y).ToString("0.#####"));
            }
            y += metrics.YGridStep;
        }
    }
}
