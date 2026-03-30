using Microsoft.Data.SqlClient;

namespace Apeiron.Platform.Databases.BomDatabase;

/// <summary>
/// Контекст базы данных.
/// </summary>
[CLSCompliant(false)]
public sealed class BomDatabaseContext : DbContext
{
    /// <summary>
    /// Конструктор по умолчанию.
    /// </summary>
    public BomDatabaseContext()
    {
    }

    /// <summary>
    /// Конструктор с параметрами.
    /// </summary>
    /// <param name="options">Параметры контекста.</param>
    public BomDatabaseContext(DbContextOptions<BomDatabaseContext> options)
        : base(options)
    {
    }


    /// <summary>
    /// Представляет таблицу с узлами(точками).
    /// </summary>
    public DbSet<Entities.Teltonika> Teltonika { get; set; } = null!;


    /// <summary>
    /// Выполняет настройку базы данных.
    /// </summary>
    /// <param name="optionsBuilder">
    /// Построитель, используемый для создания или изменения параметров этого контекста.
    /// </param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //  Проверка настроенных параметров.
        if (!optionsBuilder.IsConfigured)
        {
            //  Создание построителя строки подключения к серверу баз данных.
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new()
            {
                ApplicationName = "Bom",
                InitialCatalog = "Bom",
                DataSource = @"10.69.16.236\MSSQL",
                UserID = "sa",
                Password = "!TTCRTdbsa",
                MultipleActiveResultSets = true,
                ConnectTimeout = 600,
                ConnectRetryCount = 255,
                ConnectRetryInterval = 60,
                Pooling = true,
            };

            //  Установка строки подключения к серверу баз данных.
            optionsBuilder.UseSqlServer(sqlConnectionStringBuilder.ConnectionString);
        }
    }


    /// <summary>
    /// Выполняет настройку модели базы данных.
    /// </summary>
    /// <param name="modelBuilder">
    /// Построитель, используемый для настройки модели базы данных.
    /// </param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Настройка сущностей.
        modelBuilder.Entity<Entities.Teltonika>(Entities.Teltonika.BuildAction);
    }
}
