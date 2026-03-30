using Npgsql;

namespace Apeiron.Platform.Databases.BomPgDatabase;

/// <summary>
/// Контекст базы данных.
/// </summary>
[CLSCompliant(false)]
public partial class BomPgDatabaseContext : DbContext
{
    /// <summary>
    /// Конструктор по умолчанию.
    /// </summary>
    public BomPgDatabaseContext()
    {
    }

    /// <summary>
    /// Конструктор с параметрами.
    /// </summary>
    /// <param name="options">Параметры контекста.</param>
    public BomPgDatabaseContext(DbContextOptions<BomPgDatabaseContext> options)
        : base(options)
    {
    }


    /// <summary>
    /// Представляет таблицу с узлами(точками).
    /// </summary>
    public virtual DbSet<Entities.Teltonika> Teltonika { get; set; } = null!;


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
            NpgsqlConnectionStringBuilder npgsqlConnectionStringBuilder = new()
            {
                Host = "10.69.16.239",
                Port = 5432,
                Database = "Bom",
                Username = "postgres",
                Password = "!TTCRTdbsa2",
                Pooling = true,
                ConnectionLifetime = 600,
            };

            //  Установка строки подключения к серверу баз данных.
            optionsBuilder.UseNpgsql(npgsqlConnectionStringBuilder.ConnectionString, x => x.UseNetTopologySuite());
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
