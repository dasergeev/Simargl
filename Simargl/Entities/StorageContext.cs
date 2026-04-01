using Microsoft.EntityFrameworkCore;

namespace Simargl.Entities;

/// <summary>
/// Представляет контекст базы данных.
/// </summary>
[CLSCompliant(false)]
public abstract class StorageContext :
    DbContext
{

}
