using Apeiron.Platform.Databases.OsmDatabase.Entities;
using Microsoft.Data.SqlClient;

namespace Apeiron.Platform.Databases.OsmDatabase;

/// <summary>
/// Представляет контекст сеанса работы с базой данных.
/// </summary>
[CLSCompliant(false)]
public sealed class OsmDatabaseContext :
    DatabaseContext
{
    /// <summary>
    /// Возвращает таблицу информации о файлах.
    /// </summary>
    public DbSet<OsmFile> OsmFiles { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу информации о точках (Node) в файлах.
    /// </summary>
    public DbSet<OsmNode> OsmNodes { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу информации о метках (Tags) в точках Node.
    /// </summary>
    public DbSet<OsmNodeTag> OsmNodeTags { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу информации о линиях (Ways) в файлах.
    /// </summary>
    public DbSet<OsmWay> OsmWays { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу информаци о метках (Tags) в линиях Way.
    /// </summary>
    public DbSet<OsmWayTag> OsmWayTags { get; init; } = null!;

    /// <summary>
    /// Возращает таблицу информации о точках и линиях, реализует связь многие ко многим.
    /// </summary>
    public DbSet<OsmWayNode> OsmWayNodes { get; init; } = null!;

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
                ApplicationName = "Osm",
                InitialCatalog = "Osm",
                DataSource = @"10.47.49.57\MSSQL",
                UserID = "nto",
                Password = "RN@15tu*NSD",
                //DataSource = @"10.69.16.236\MSSQL",
                //UserID = "sa",
                //Password = "!TTCRTdbsa",
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
        modelBuilder.Entity<OsmFile>(OsmFile.BuildAction);
        modelBuilder.Entity<OsmNode>(OsmNode.BuildAction);
        modelBuilder.Entity<OsmNodeTag>(OsmNodeTag.BuildAction);
        modelBuilder.Entity<OsmWay>(OsmWay.BuildAction);
        modelBuilder.Entity<OsmWayTag>(OsmWayTag.BuildAction);
        modelBuilder.Entity<OsmWayNode>(OsmWayNode.BuildAction);
    }
}

