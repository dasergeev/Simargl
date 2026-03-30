namespace Simargl.Detecting.Bearing.Simulator.Logging;

/// <summary>
/// Представляет реализацию интерфейса <see cref="ILogger"/>,
/// выполняющую передачу сообщений журнала в пользовательский интерфейс.
/// </summary>
/// <param name="sink">
/// Приемник сообщений журнала.
/// </param>
/// <param name="category">
/// Категория журнала.
/// </param>
public sealed class UILogger(UILogSink sink, string category) : ILogger
{
    /// <summary>
    /// Поле для хранения категории журнала.
    /// </summary>
    private readonly string _Category = category.Split('.')[^1];

    /// <summary>
    /// Начинает логическую область выполнения.
    /// </summary>
    /// <typeparam name="TState">
    /// Тип состояния области.
    /// </typeparam>
    /// <param name="state">
    /// Состояние области.
    /// </param>
    /// <returns>
    /// Объект управления областью.
    /// </returns>
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

    /// <summary>
    /// Проверяет возможность записи сообщения указанного уровня.
    /// </summary>
    /// <param name="logLevel">
    /// Уровень журнала.
    /// </param>
    /// <returns>
    /// Значение <see langword="true"/>, если уровень поддерживается; 
    /// в противном случае — <see langword="false"/>.
    /// </returns>
    public bool IsEnabled(LogLevel logLevel) => true;

    /// <summary>
    /// Выполняет запись сообщения журнала.
    /// </summary>
    /// <typeparam name="TState">
    /// Тип состояния сообщения.
    /// </typeparam>
    /// <param name="logLevel">
    /// Уровень журнала.
    /// </param>
    /// <param name="eventId">
    /// Идентификатор события.
    /// </param>
    /// <param name="state">
    /// Состояние сообщения.
    /// </param>
    /// <param name="exception">
    /// Связанное исключение.
    /// </param>
    /// <param name="formatter">
    /// Функция форматирования сообщения.
    /// </param>
    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        //  Получение текста уровня.
        string level = logLevel.ToString();

        //  Проверка размера.
        if (level.Length > 4)
        {
            //  Образка текста.
            level = level[..4];
        }

        //  Формирование текстового сообщения журнала.
        string message =
            $"{DateTime.Now:HH:mm:ss} [{level}] {_Category}: {formatter(state, exception)}";

        //  Передача сообщения в приемник журнала.
        sink.Add(message);
    }
}
