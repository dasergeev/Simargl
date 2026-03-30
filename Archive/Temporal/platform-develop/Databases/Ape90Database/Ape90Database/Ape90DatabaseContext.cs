using Apeiron.Platform.Databases.Ape90Database.Entities;

namespace Apeiron.Platform.Databases.Ape90Database;

/// <summary>
/// Представляет контекст сеанса работы с базой данных.
/// </summary>
[CLSCompliant(false)]
public class Ape90DatabaseContext :
    DatabaseContext
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public Ape90DatabaseContext()
    {
        //  Установка времени ожидания, которое будет использоваться для команд,
        //  выполняемых с этого контекста сеанса работы с базой данных.
        Database.SetCommandTimeout(TimeSpan.FromMinutes(60));
    }

    /// <summary>
    /// Возвращает таблицу каталогов исходных данных.
    /// </summary>
    public DbSet<RawDirectory> RawDirectories { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу исходных кадров регистрации.
    /// </summary>
    public DbSet<RawFrame> RawFrames { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу исходных файлов геолокационных данных.
    /// </summary>
    public DbSet<RawGeolocation> RawGeolocations { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу исходных файлов данных о питании.
    /// </summary>
    public DbSet<RawPower> RawPowers { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу сообщений GPS, содержащее данные местоположения.
    /// </summary>
    public DbSet<GgaMessage> GgaMessages { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу сообщений GPS, содержащее минимальный рекомендованный набор данных.
    /// </summary>
    public DbSet<RmcMessage> RmcMessages { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу сообщений GPS, содержащее данные о наземном курсе и скорости.
    /// </summary>
    public DbSet<VtgMessage> VtgMessages { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу геолокационных данных.
    /// </summary>
    public DbSet<Geolocation> Geolocations { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу информации о кадрах.
    /// </summary>
    public DbSet<FrameInfo> FrameInfos { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу информации о каналах.
    /// </summary>
    public DbSet<ChannelInfo> ChannelInfos { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу фрагментов.
    /// </summary>
    public DbSet<Fragment> Fragments { get; init; } = null!;

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
                ApplicationName = @"Ape90",
                InitialCatalog = @"Ape90",
                DataSource = @"10.69.16.236\MSSQL",
                UserID = @"sa",
                Password = @"!TTCRTdbsa",
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
        modelBuilder.Entity<RawDirectory>(RawDirectory.BuildAction);
        modelBuilder.Entity<RawFrame>(RawFrame.BuildAction);
        modelBuilder.Entity<RawGeolocation>(RawGeolocation.BuildAction);
        modelBuilder.Entity<RawPower>(RawPower.BuildAction);
        modelBuilder.Entity<GgaMessage>(GgaMessage.BuildAction);
        modelBuilder.Entity<RmcMessage>(RmcMessage.BuildAction);
        modelBuilder.Entity<VtgMessage>(VtgMessage.BuildAction);
        modelBuilder.Entity<Geolocation>(Geolocation.BuildAction);
        modelBuilder.Entity<FrameInfo>(FrameInfo.BuildAction);
        modelBuilder.Entity<ChannelInfo>(ChannelInfo.BuildAction);
        modelBuilder.Entity<Fragment>(Fragment.BuildAction);
    }
}

/// <summary>
/// Представляет контекст сеанса работы с базой данных Postgres.
/// </summary>
[CLSCompliant(false)]
public sealed class Ape90DatabasePostgresContext :
    Ape90DatabaseContext
{
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
            //  Установка строки подключения к серверу баз данных.
            optionsBuilder.UseNpgsql("Host=10.69.16.239;Port=5432;Database=Ape90;Username=postgres;Password=!TTCRTdbsa2");
        }
    }
}
