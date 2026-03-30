namespace Apeiron.Platform.Databases.OrioleDatabase.Entities;

/// <summary>
/// Представляет каталог с записями.
/// </summary>
public class RecordDirectory
{
    /// <summary>
    /// Возвращает или инициализирует каталога.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Возвращает или задаёт идентификатор регистратора.
    /// </summary>
    public int RegistrarId { get; set; }

    /// <summary>
    /// Возвращает или задаёт путь к каталогу.
    /// </summary>
    public string Path { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт регистратор.
    /// </summary>
    public Registrar Registrar { get; set; } = null!;
}
