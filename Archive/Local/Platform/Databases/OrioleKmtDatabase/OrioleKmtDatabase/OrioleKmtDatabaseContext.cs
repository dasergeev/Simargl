using Apeiron.Platform.Databases.OrioleKmtDatabase.Entities;
using Microsoft.Data.SqlClient;

namespace Apeiron.Platform.Databases.OrioleKmtDatabase;

/// <summary>
/// Представляет контекст сеанса работы с базой данных.
/// </summary>
[CLSCompliant(false)]
public sealed class OrioleKmtDatabaseContext : DatabaseContext
{
    /// <summary>
    /// Возвращает таблицу информации об файлах.
    /// </summary>
    public DbSet<RawFrame> RawFrames { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу информации о частях времен в течении которых были разные имена каналов.
    /// </summary>
    public DbSet<TimeChunk> TimeChunks { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу информации об именах каналов.
    /// </summary>
    public DbSet<ChannelName> ChannelNames { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу информации об обработанных файлах.
    /// </summary>
    public DbSet<AnalyzedFrame> AnalyzedFrames { get; init; } = null!;


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
                ApplicationName = "OrioleKmt",
                InitialCatalog = "OrioleKmt",
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
        modelBuilder.Entity<RawFrame>(RawFrame.BuildAction);
        modelBuilder.Entity<TimeChunk>(TimeChunk.BuildAction);
        modelBuilder.Entity<ChannelName>(ChannelName.BuildAction);
        modelBuilder.Entity<AnalyzedFrame>(AnalyzedFrame.BuildAction);
    }

}
