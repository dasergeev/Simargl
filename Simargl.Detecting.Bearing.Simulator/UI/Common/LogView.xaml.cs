using Simargl.Detecting.Bearing.Simulator.Logging;

namespace Simargl.Detecting.Bearing.Simulator.UI.Common;

/// <summary>
/// Представляет элемент управления, отображающий журнал.
/// </summary>
partial class LogView
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public LogView()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Проверка режима выполнения.
        if (IsRuntime)
        {
            //  Получение экземпляра приемника журнала из контейнера служб.
            UILogSink logSink = Host.Services.GetRequiredService<UILogSink>();

            //  Установка контекста данных.
            DataContext = logSink;
        }
    }
}
