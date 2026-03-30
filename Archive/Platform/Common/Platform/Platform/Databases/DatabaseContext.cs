using Microsoft.EntityFrameworkCore;

namespace Apeiron.Platform.Databases;

/// <summary>
/// Представляет контекст сеанса работы с базой данных.
/// </summary>
[CLSCompliant(false)]
public abstract class DatabaseContext :
    DbContext
{
    /// <summary>
    /// 
    /// </summary>
    protected DatabaseContext()
        : base()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public DatabaseContext(DbContextOptions options)
        : base(options)
    {

    }

}
