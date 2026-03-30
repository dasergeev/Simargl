using Apeiron.Platform.Databases.OsmRussiaDatabase.Entities;
using Npgsql;

namespace Apeiron.Platform.Databases.OsmRussiaDatabase;

/// <summary>
/// Контекст базы данных.
/// </summary>
[CLSCompliant(false)]
public partial class OsmRussiaDatabaseContext : DbContext
{
    /// <summary>
    /// Конструктор по умолчанию.
    /// </summary>
    public OsmRussiaDatabaseContext()
    {
    }

    /// <summary>
    /// Конструктор с параметрами.
    /// </summary>
    /// <param name="options">Параметры контекста.</param>
    public OsmRussiaDatabaseContext(DbContextOptions<OsmRussiaDatabaseContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Представляет таблицу с узлами(точками).
    /// </summary>
    public virtual DbSet<Node> Nodes { get; set; } = null!;
    /// <summary>
    /// Представляет таблицу с отношениями.
    /// </summary>
    public virtual DbSet<Relation> Relations { get; set; } = null!;
    /// <summary>
    /// Представляет таблицу с объектами отношений.
    /// </summary>
    public virtual DbSet<RelationMember> RelationMembers { get; set; } = null!;
    /// <summary>
    /// Представляет таблицу с описанием схемы БД и версии.
    /// </summary>
    public virtual DbSet<SchemaInfo> SchemaInfos { get; set; } = null!;
    /// <summary>
    /// Представляет таблицу с пользователями.
    /// </summary>
    public virtual DbSet<User> Users { get; set; } = null!;
    /// <summary>
    /// Представлет таблицу с линиями.
    /// </summary>
    public virtual DbSet<Way> Ways { get; set; } = null!;
    /// <summary>
    /// Представляет таблицу с описанием какие точки входят в линию.
    /// </summary>
    public virtual DbSet<WayNode> WayNodes { get; set; } = null!;

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
                Database = "OsmRussia",
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
        // Настройка расширений базы данных.
        modelBuilder.HasPostgresExtension("hstore")
            .HasPostgresExtension("postgis");

#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
        modelBuilder.HasDbFunction(typeof(DbFunctionsExtensions)
            .GetMethod(nameof(DbFunctionsExtensions.HasKeyValue)))
            .HasName("hstore_has_key_value");
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.

#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
        modelBuilder.HasDbFunction(typeof(DbFunctionsExtensions)
            .GetMethod(nameof(DbFunctionsExtensions.HasKey)))
            .HasName("hstore_has_key");
        //modelBuilder.HasDbFunction(typeof(DbFunctionsExtensions).GetMethods()
        //   .Where(m =>
        //   {
        //       return m.Name == "HasKeyValue";
        //   })
        //   .SingleOrDefault())
        //   .HasName("public.hstore_has_key_value");

        // Настройка сущностей.
        modelBuilder.Entity<Node>(Node.BuildAction);
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
        modelBuilder.Entity<Relation>(Relation.BuildAction);
        modelBuilder.Entity<RelationMember>(RelationMember.BuildAction);
        modelBuilder.Entity<SchemaInfo>(SchemaInfo.BuildAction);
        modelBuilder.Entity<User>(User.BuildAction);
        modelBuilder.Entity<Way>(Way.BuildAction);
        modelBuilder.Entity<WayNode>(WayNode.BuildAction);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
