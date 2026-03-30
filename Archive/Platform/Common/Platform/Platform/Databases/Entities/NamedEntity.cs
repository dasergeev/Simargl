using Microsoft.EntityFrameworkCore;

namespace Apeiron.Platform.Databases.Entities;

/// <summary>
/// Представляет именованную сущность базы данных.
/// </summary>
[CLSCompliant(false)]
[Index(nameof(Name), IsUnique = true)]
public abstract class NamedEntity :
    Entity
{
    /// <summary>
    /// Возвращает или задаёт имя.
    /// </summary>
    public string Name { get; set; } = null!;
}
