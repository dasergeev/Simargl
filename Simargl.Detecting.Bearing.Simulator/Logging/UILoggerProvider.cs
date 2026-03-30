namespace Simargl.Detecting.Bearing.Simulator.Logging;

/// <summary>
/// Представляет провайдер логирования, выполняющий
/// передачу сообщений журнала в пользовательский интерфейс.
/// </summary>
/// <param name="sink">
/// Приемник сообщений журнала.
/// </param>
public class UILoggerProvider(UILogSink sink) :
    ILoggerProvider
{
    /// <summary>
    /// Создает экземпляр журнала для указанной категории.
    /// </summary>
    /// <param name="categoryName">
    /// Имя категории журнала.
    /// </param>
    /// <returns>
    /// Экземпляр <see cref="ILogger"/>.
    /// </returns>
    public ILogger CreateLogger(string categoryName)
    {
        //  Создание экземпляра журнала.
        return new UILogger(sink, categoryName);
    }

    /// <summary>
    /// Освобождает ресурсы провайдера логирования.
    /// </summary>
    public void Dispose()
    {
        //  Подавление финализации объекта сборщиком мусора.
        GC.SuppressFinalize(this);
    }
}
