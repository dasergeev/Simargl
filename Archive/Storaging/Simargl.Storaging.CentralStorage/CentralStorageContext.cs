namespace Simargl.Storaging.CentralStorage;

/// <summary>
/// Представляет контекст центральной базы данных.
/// </summary>
[CLSCompliant(false)]
public sealed class CentralStorageContext :
    DbContext
{
    /// <summary>
    /// Поле для хранения адреса для подключения базы данных.
    /// </summary>
    private readonly string _Host;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public CentralStorageContext() :
        this("192.168.15.202")
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public CentralStorageContext(string host)
    {
        // (localhost)
        _Host = host;
        //  Установка времени ожидания, которое будет использоваться для команд,
        //  выполняемых с этого контекста сеанса работы с базой данных.
        Database.SetCommandTimeout(TimeSpan.FromMinutes(5));
    }

    ///// <summary>
    ///// Возвращает или инициализирует таблицу каталогов с часовыми данными.
    ///// </summary>
    //public DbSet<HourDirectoryData> HourDirectories { get; init; } = null!;

    ///// <summary>
    ///// Возвращает или инициализирует таблицу датчиков.
    ///// </summary>
    //public DbSet<AdxlData> Adxls { get; init; } = null!;

    ///// <summary>
    ///// Возвращает или инициализирует таблицу файлов данных датчиков.
    ///// </summary>
    //public DbSet<AdxlFileData> AdxlFiles { get; init; } = null!;

    ///// <summary>
    ///// Возвращает или инициализирует таблицу файлов данных Nmea.
    ///// </summary>
    //public DbSet<NmeaFileData> NmeaFiles { get; init; } = null!;

    ///// <summary>
    ///// Возвращает или инициализирует таблицу файлов данных сырых кадров.
    ///// </summary>
    //public DbSet<RawFrameFileData> RawFrameFiles { get; init; } = null!;

    ///// <summary>
    ///// Возвращает или инициализирует таблицу данных Nmea.
    ///// </summary>
    //public DbSet<NmeaData> Nmeas { get; init; } = null!;

    ///// <summary>
    ///// Возвращает или инициализирует таблицу данных каналов Adxl.
    ///// </summary>
    //public DbSet<AdxlChannelData> AdxlChannels { get; init; } = null!;

    ///// <summary>
    ///// Возвращает или инициализирует таблицу данных каналов RawFrame.
    ///// </summary>
    //public DbSet<RawFrameChannelData> RawFrameChannels { get; init; } = null!;

    /// <summary>
    /// Выполняет настройку модели базы данных.
    /// </summary>
    /// <param name="modelBuilder">
    /// Построитель, используемый для настройки модели базы данных.
    /// </param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ////  Настройка моделей сущности.
        //modelBuilder.Entity<HourDirectoryData>(HourDirectoryData.BuildAction);
        //modelBuilder.Entity<AdxlData>(AdxlData.BuildAction);
        //modelBuilder.Entity<AdxlFileData>(AdxlFileData.BuildAction);
        //modelBuilder.Entity<NmeaFileData>(NmeaFileData.BuildAction);
        //modelBuilder.Entity<RawFrameFileData>(RawFrameFileData.BuildAction);
        //modelBuilder.Entity<NmeaData>(NmeaData.BuildAction);
        //modelBuilder.Entity<AdxlChannelData>(AdxlChannelData.BuildAction);
        //modelBuilder.Entity<RawFrameChannelData>(RawFrameChannelData.BuildAction);
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
                $"Host={_Host};" +
                "Port=5432;" +
                "Database=Central;" +
                "Username=postgres;" +
                "Password=$QLControl;" +
                "MaxPoolSize=1000");
        }
    }
}
