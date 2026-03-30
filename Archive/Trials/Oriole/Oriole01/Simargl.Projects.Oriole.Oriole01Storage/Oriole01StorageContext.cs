using Microsoft.EntityFrameworkCore;
using Simargl.Projects.Oriole.Oriole01Storage.Entities;

namespace Simargl.Projects.Oriole.Oriole01Storage;

/// <summary>
/// Представляет контекст базы данных.
/// </summary>
[CLSCompliant(false)]
public sealed class Oriole01StorageContext :
    DbContext
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public Oriole01StorageContext()
    {
        //  Установка времени ожидания, которое будет использоваться для команд,
        //  выполняемых с этого контекста сеанса работы с базой данных.
        Database.SetCommandTimeout(TimeSpan.FromMinutes(5));
    }

    /// <summary>
    /// Возвращает или инициализирует таблицу путей к корневому каталогу.
    /// </summary>
    public DbSet<PathData> Paths { get; init; } = null!;

    /// <summary>
    /// Возвращает или инициализирует таблицу путей к корневому каталогу собранных записей.
    /// </summary>
    public DbSet<RecordPathData> RecordPaths { get; init; } = null!;

    /// <summary>
    /// Возвращает или инициализирует таблицу каталогов с часовыми данными.
    /// </summary>
    public DbSet<HourDirectoryData> HourDirectories { get; init; } = null!;

    /// <summary>
    /// Возвращает или инициализирует таблицу датчиков.
    /// </summary>
    public DbSet<AdxlData> Adxls { get; init; } = null!;

    /// <summary>
    /// Возвращает или инициализирует таблицу файлов данных датчиков.
    /// </summary>
    public DbSet<AdxlFileData> AdxlFiles { get; init; } = null!;

    /// <summary>
    /// Возвращает или инициализирует таблицу файлов данных Nmea.
    /// </summary>
    public DbSet<NmeaFileData> NmeaFiles { get; init; } = null!;

    /// <summary>
    /// Возвращает или инициализирует таблицу данных Nmea.
    /// </summary>
    public DbSet<NmeaData> Nmeas { get; init; } = null!;

    /// <summary>
    /// Возвращает или инициализирует таблицу данных каналов.
    /// </summary>
    public DbSet<ChannelData> Channels { get; init; } = null!;

    /// <summary>
    /// Возвращает или инициализирует таблицу данных устройств.
    /// </summary>
    public DbSet<DeviceData> Devices { get; init; } = null!;

    /// <summary>
    /// Возвращает или инициализирует таблицу имён каналов.
    /// </summary>
    public DbSet<ChannelNameData> ChannelNames { get; init; } = null!;

    /// <summary>
    /// Возвращает или инициализирует таблицу дней.
    /// </summary>
    public DbSet<DayData> Days { get; init; } = null!;

    /// <summary>
    /// Выполняет настройку модели базы данных.
    /// </summary>
    /// <param name="modelBuilder">
    /// Построитель, используемый для настройки модели базы данных.
    /// </param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //  Настройка моделей сущности.
        modelBuilder.Entity<PathData>(PathData.BuildAction);
        modelBuilder.Entity<RecordPathData>(RecordPathData.BuildAction);
        modelBuilder.Entity<HourDirectoryData>(HourDirectoryData.BuildAction);
        modelBuilder.Entity<AdxlData>(AdxlData.BuildAction);
        modelBuilder.Entity<AdxlFileData>(AdxlFileData.BuildAction);
        modelBuilder.Entity<NmeaFileData>(NmeaFileData.BuildAction);
        modelBuilder.Entity<NmeaData>(NmeaData.BuildAction);
        modelBuilder.Entity<ChannelData>(ChannelData.BuildAction);
        modelBuilder.Entity<DeviceData>(DeviceData.BuildAction);
        modelBuilder.Entity<ChannelNameData>(ChannelNameData.BuildAction);
        modelBuilder.Entity<DayData>(DayData.BuildAction);
    }

    /// <summary>
    /// Выполняет настройку базы данных.
    /// </summary>
    /// <param name="optionsBuilder">
    /// Построитель, используемый для создания или изменения параметров этого контекста.
    /// </param>
    protected override sealed void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //  Проверка настроенных параметров.
        if (!optionsBuilder.IsConfigured)
        {
            //  Установка пути к файлу базы данных.
            optionsBuilder.UseNpgsql(
                "Host=192.168.15.203;" + //  10.7.0.6 (VPN), 127.0.0.1 (localhost)
                "Port=5432;" +
                //"Host=127.0.0.1;" + //  172.16.10.18 (VPN), 192.168.0.242 (Local), 127.0.0.1 (localhost)
                //"Port=5433;" +
                "Database=Oriole01;" +
                "Username=postgres;" +
                "Password=$QLControl");
        }
    }
}
