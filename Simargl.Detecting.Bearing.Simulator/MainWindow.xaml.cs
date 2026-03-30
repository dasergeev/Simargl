using Simargl.Detecting.Bearing.Simulator.Logging;

namespace Simargl.Detecting.Bearing.Simulator;

/// <summary>
/// Представляет главное окно приложения.
/// </summary>
partial class MainWindow
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public MainWindow(/*UILogSink logSink*/)
    {
        //  Инициализиция основных компонентов.
        InitializeComponent();

        //  Получение экземпляра приемника журнала из контейнера служб.
        UILogSink logSink =
            ((App)Application.Current).Host.Services.GetRequiredService<UILogSink>();

        DataContext = logSink;
    }
}
