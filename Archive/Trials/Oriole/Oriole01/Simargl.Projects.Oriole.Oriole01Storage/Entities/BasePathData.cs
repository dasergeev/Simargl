namespace Simargl.Projects.Oriole.Oriole01Storage.Entities;

/// <summary>
/// Представляет базовые данные пути.
/// </summary>
public abstract class BasePathData
{
    /// <summary>
    /// Возвращает или инициализирует ключ сущности.
    /// </summary>
    public long Key { get; init; }

    /// <summary>
    /// Возвращает или задаёт полный путь к корневому каталогу.
    /// </summary>
    public string FullPath { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт приоритет.
    /// </summary>
    public int Priority { get; set; }
}
