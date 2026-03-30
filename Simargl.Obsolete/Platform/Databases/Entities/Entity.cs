using System.ComponentModel.DataAnnotations;

namespace Simargl.Platform.Databases.Entities;

/// <summary>
/// Представляет сущность базы данных.
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// Возвращает или инициализирует идентификатор.
    /// </summary>
    [Key]
    public long Id { get; init; }
}
