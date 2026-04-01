namespace Simargl.AdxlRecorder;

/// <summary>
/// Представляет настройки.
/// </summary>
public sealed class Tunings
{
    /// <summary>
    /// Возвращает или инициализирует порт для подключения к узлу.
    /// </summary>
    public int Port { get; init; }

    /// <summary>
    /// Возвращает или инициализирует путь к корневому каталогу с файлами.
    /// </summary>
    public string Path { get; init; } = null!;
}
