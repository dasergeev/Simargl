using Apeiron.Platform.Databases.GlobalIdentityDatabase.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apeiron.Platform.Databases.GlobalIdentityDatabase;

/// <summary>
/// Представляет контекст сеанса работы с базой данных.
/// </summary>
[CLSCompliant(false)]
public class GlobalIdentityDatabaseContext :
    DatabaseContext
{
    /// <summary>
    /// Возвращает или инициализирует таблицу глобальных идентификаторов.
    /// </summary>
    public DbSet<GlobalIdentifier> GlobalIdentifiers { get; init; } = null!;

    /// <summary>
    /// Возвращает или инициализирует таблицу идентификационных сообщений.
    /// </summary>
    public DbSet<IdentityMessage> IdentityMessages { get; init; } = null!;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public GlobalIdentityDatabaseContext()
    {
        //  Установка времени ожидания, которое будет использоваться для команд,
        //  выполняемых с этого контекста сеанса работы с базой данных.
        Database.SetCommandTimeout(TimeSpan.FromMinutes(60));
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса, передаёт в родительский класс настройки подклключения к БД.
    /// </summary>
    /// <param name="options">Настройки.</param>
    public GlobalIdentityDatabaseContext(DbContextOptions options) : base(options)
    {
        //  Установка времени ожидания, которое будет использоваться для команд,
        //  выполняемых с этого контекста сеанса работы с базой данных.
        Database.SetCommandTimeout(TimeSpan.FromMinutes(60));
    }

    /// <summary>
    /// Выполняет настройку базы данных.
    /// </summary>
    /// <param name="optionsBuilder">
    /// Построитель, используемый для создания или изменения параметров этого контекста.
    /// </param>
    protected override void OnConfiguring([ParameterNoChecks] DbContextOptionsBuilder optionsBuilder)
    {
        //  Проверка настроек параметров.
        if (!optionsBuilder.IsConfigured)
        {
            //  Настройка контекста для подключения к базе данных Microsoft SQL Server.
            optionsBuilder.UseSqlServer(
                "Server=10.69.16.236\\MSSQL;" +
                "Database=GlobalIdentity;" +
                "User Id=sa;" +
                "Password=!TTCRTdbsa;" +
                "MultipleActiveResultSets=true;");
        }
    }
}
