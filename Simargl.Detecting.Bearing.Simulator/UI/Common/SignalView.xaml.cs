using OxyPlot;
using OxyPlot.Series;

namespace Simargl.Detecting.Bearing.Simulator.UI.Common;

/// <summary>
/// Представляет элемент управления, отображающий исходные сигналы.
/// </summary>
partial class SignalView
{
    /// <summary>
    /// 
    /// </summary>
    [CLSCompliant(false)]
    public PlotModel PlotModel { get; }

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public SignalView()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        PlotModel = new PlotModel { Title = "Signal" };

        var series = new LineSeries();

        for (int i = 0; i < 100; i++)
        {
            series.Points.Add(new DataPoint(i, Math.Sin(i * 0.1)));
        }

        PlotModel.Series.Add(series);
        DataContext = this;
    }
}
