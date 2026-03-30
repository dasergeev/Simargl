using Apeiron.Platform.Databases.OrioleDatabase.Entities;
using Microsoft.Data.SqlClient;

namespace Apeiron.Platform.Databases.OrioleDatabase;

/// <summary>
/// Представляет контекст сеанса работы с базой данных.
/// </summary>
public class OrioleDatabaseContext :
    DatabaseContext
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public OrioleDatabaseContext()
    {
        //  Установка времени ожидания, которое будет использоваться для команд,
        //  выполняемых с этого контекста сеанса работы с базой данных.
        Database.SetCommandTimeout(TimeSpan.FromMinutes(60));
    }

    ///// <summary>
    ///// Инициализирует новый экземпляр класса, передаёт в родительский класс настройки подклключения к БД.
    ///// </summary>
    ///// <param name="options">Настройки.</param>
    //public OrioleDatabaseContext(DbContextOptions<OrioleDatabaseContext> options)
    //    : base(options)
    //{
    //    //  Установка времени ожидания, которое будет использоваться для команд,
    //    //  выполняемых с этого контекста сеанса работы с базой данных.
    //    Database.SetCommandTimeout(TimeSpan.FromMinutes(60));
    //}

    /// <summary>
    /// Возвращает таблицу регистраторов.
    /// </summary>
    public DbSet<Registrar> Registrars { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу каталогов необработанных данных.
    /// </summary>
    public DbSet<RawDirectory> RawDirectories { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу каталогов с записями.
    /// </summary>
    public DbSet<RecordDirectory> RecordDirectories { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу датчиков.
    /// </summary>
    public DbSet<Sensor> Sensors { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу каналов.
    /// </summary>
    public DbSet<Channel> Channels { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу источников.
    /// </summary>
    public DbSet<Source> Sources { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу файлов с пакетами данных.
    /// </summary>
    public DbSet<PackageFile> PackageFiles { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу пакетов.
    /// </summary>
    public DbSet<Package> Packages { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу файлов с пакетами данных.
    /// </summary>
    public DbSet<NmeaFile> NmeaFiles { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу сообщений GPS, содержащих данные местоположения.
    /// </summary>
    public DbSet<GgaMessage> GgaMessages { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу сообщений GPS, содержащих минимальный рекомендованный набор данных.
    /// </summary>
    public DbSet<RmcMessage> RmcMessages { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу сообщений GPS, содержащих данные о наземном курсе и скорости.
    /// </summary>
    public DbSet<VtgMessage> VtgMessages { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу геолокационных данных.
    /// </summary>
    public DbSet<Geolocation> Geolocations { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу кадров.
    /// </summary>
    public DbSet<Frame> Frames { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу спектров.
    /// </summary>
    public DbSet<Spectrum> Spectrums { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу амплитуд.
    /// </summary>
    public DbSet<Amplitude> Amplitudes { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу фильтров.
    /// </summary>
    public DbSet<Filter> Filters { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу местоположений.
    /// </summary>
    public DbSet<Location> Locations { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу статистических данных.
    /// </summary>
    public DbSet<Statistic> Statistics { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу экстремальных данных.
    /// </summary>
    public DbSet<Extremum> Extremums { get; init; } = null!;

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
                ApplicationName = "Oriole",
                InitialCatalog = "Oriole",
                DataSource = @"10.69.16.236\MSSQL", //  "10.69.16.236\\MSSQL"
                UserID = "sa",
                Password = "!TTCRTdbsa",
                MultipleActiveResultSets = true,
                ConnectTimeout = 600,
                ConnectRetryCount = 255,
                ConnectRetryInterval = 60,
                Pooling = true,
            };

            ////  Создание построителя строки подключения к серверу баз данных.
            //SqlConnectionStringBuilder sqlConnectionStringBuilder = new()
            //{
            //    ApplicationName = "Oriole",
            //    InitialCatalog = "Oriole",
            //    DataSource = "10.47.49.57\\MSSQL", //  "10.69.16.236\\MSSQL"
            //    UserID = "sa",
            //    Password = "!TTCRTdbsa",
            //    MultipleActiveResultSets = true,
            //    ConnectTimeout = 600,
            //    ConnectRetryCount = 255,
            //    ConnectRetryInterval = 60,
            //    Pooling = true,
            //};


            //  Установка строки подключения к серверу баз данных.
            optionsBuilder.UseSqlServer(sqlConnectionStringBuilder.ConnectionString);
            //optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Oriole;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            //
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
        modelBuilder.Entity<Registrar>(typeBuilder =>
        {
            typeBuilder.HasKey(registrar => registrar.Id);
        });

        modelBuilder.Entity<RawDirectory>(typeBuilder =>
        {
            typeBuilder.HasKey(rawDirectory => rawDirectory.Id);
            typeBuilder.HasIndex(rawDirectory => rawDirectory.RegistrarId);
            typeBuilder.HasOne(rawDirectory => rawDirectory.Registrar)
                .WithMany(registrar => registrar.RawDirectories)
                .HasForeignKey(rawDirectory => rawDirectory.RegistrarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RawDirectories_Registrars");
        });

        modelBuilder.Entity<RecordDirectory>(typeBuilder =>
        {
            typeBuilder.HasKey(recordDirectory => recordDirectory.Id);
            typeBuilder.HasIndex(recordDirectory => recordDirectory.RegistrarId);
            typeBuilder.HasOne(recordDirectory => recordDirectory.Registrar)
                .WithMany(registrar => registrar.RecordDirectories)
                .HasForeignKey(recordDirectory => recordDirectory.RegistrarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RecordDirectories_Registrars");
        });

        modelBuilder.Entity<Sensor>(typeBuilder =>
        {
            typeBuilder.HasKey(sensor => sensor.Id);
        });

        modelBuilder.Entity<Channel>(typeBuilder =>
        {
            typeBuilder.HasKey(channel => channel.Id);
            typeBuilder.HasIndex(channel => channel.RegistrarId);
            typeBuilder.HasIndex(channel => channel.Name);
            typeBuilder.HasIndex(channel => channel.Sampling);
            typeBuilder.HasIndex(channel => channel.Cutoff);
            typeBuilder.HasOne(channel => channel.Registrar)
                .WithMany(registrar => registrar.Channels)
                .HasForeignKey(channel => channel.RegistrarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Channels_Registrars");
        });

        modelBuilder.Entity<Source>(typeBuilder =>
        {
            typeBuilder.HasKey(source => source.Id);
            typeBuilder.HasIndex(source => source.SensorId);
            typeBuilder.HasIndex(source => source.ChannelId);
            typeBuilder.HasIndex(source => source.BeginTime);
            typeBuilder.HasIndex(source => source.EndTime);
            typeBuilder.HasIndex(source => source.Format);
            typeBuilder.HasIndex(source => source.Sampling);
            typeBuilder.HasOne(source => source.Sensor)
                .WithMany(sensor => sensor.Sources)
                .HasForeignKey(source => source.SensorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sources_Sensors");
            typeBuilder.HasOne(source => source.Channel)
                .WithMany(channel => channel.Sources)
                .HasForeignKey(source => source.ChannelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sources_Channels");
        });


        modelBuilder.Entity<PackageFile>(typeBuilder =>
        {
            typeBuilder.HasKey(packageFile => new
            {
                packageFile.RawDirectoryId,
                packageFile.Format,
                packageFile.Time,
            });
            typeBuilder.HasIndex(packageFile => packageFile.IsLoaded);
            typeBuilder.HasIndex(packageFile => packageFile.IsNormalized);
            typeBuilder.HasIndex(packageFile => packageFile.LocationType);
            typeBuilder.HasIndex(packageFile => packageFile.NormalizedBeginTime);
            typeBuilder.HasIndex(packageFile => packageFile.NormalizedEndTime);
            typeBuilder.HasIndex(packageFile => packageFile.IsAnalyzed);
            typeBuilder.HasOne(packageFile => packageFile.RawDirectory)
                .WithMany(rawDirectory => rawDirectory.PackageFiles)
                .HasForeignKey(packageFile => packageFile.RawDirectoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PackageFiles_RawDirectories");
        });


        modelBuilder.Entity<Package>(typeBuilder =>
        {
            typeBuilder.HasKey(package => new
            {
                package.RawDirectoryId,
                package.Format,
                package.FileTime,
                package.FileOffset,
            });
            typeBuilder.HasIndex(package => package.IsAnalyzed);
            typeBuilder.HasIndex(package => package.Synchromarker);
            typeBuilder.HasOne(package => package.RawDirectory)
                .WithMany(rawDirectory => rawDirectory.Packages)
                .HasForeignKey(package => package.RawDirectoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Packages_RawDirectories");
            typeBuilder.HasOne(package => package.PackageFile)
                .WithMany(packageFile => packageFile.Packages)
                .HasForeignKey(package => new
                {
                    package.RawDirectoryId,
                    package.Format,
                    package.FileTime,
                })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Packages_PackageFiles");
        });

        modelBuilder.Entity<NmeaFile>(typeBuilder =>
        {
            typeBuilder.HasKey(nmeaFile => new
            {
                nmeaFile.RawDirectoryId,
                nmeaFile.Time,
            });
            typeBuilder.HasIndex(nmeaFile => nmeaFile.IsLoaded);
            typeBuilder.HasOne(nmeaFile => nmeaFile.RawDirectory)
                .WithMany(rawDirectory => rawDirectory.NmeaFiles)
                .HasForeignKey(packageFile => packageFile.RawDirectoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NmeaFiles_RawDirectories");
        });

        modelBuilder.Entity<GgaMessage>(typeBuilder =>
        {
            typeBuilder.HasKey(ggaMessage => new
            {
                ggaMessage.RawDirectoryId,
                ggaMessage.FileTime,
                ggaMessage.Index,
            });
            typeBuilder.HasIndex(ggaMessage => ggaMessage.RawDirectoryId);
            typeBuilder.HasIndex(ggaMessage => ggaMessage.FileTime);
            typeBuilder.HasIndex(ggaMessage => ggaMessage.Index);
            typeBuilder.HasIndex(ggaMessage => ggaMessage.IsAnalyzed);
            typeBuilder.HasIndex(ggaMessage => ggaMessage.RegistrarId);
            typeBuilder.HasOne(ggaMessage => ggaMessage.Registrar)
                .WithMany(registrar => registrar.GgaMessages)
                .HasForeignKey(ggaMessage => ggaMessage.RegistrarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GgaMessages_Registrars");
            typeBuilder.HasOne(ggaMessage => ggaMessage.RawDirectory)
                .WithMany(rawDirectory => rawDirectory.GgaMessages)
                .HasForeignKey(package => package.RawDirectoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GgaMessages_RawDirectories");
            typeBuilder.HasOne(ggaMessage => ggaMessage.NmeaFile)
                .WithMany(nmeaFile => nmeaFile.GgaMessages)
                .HasForeignKey(ggaMessage => new
                {
                    ggaMessage.RawDirectoryId,
                    ggaMessage.FileTime,
                })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GgaMessages_NmeaFiles");
        });

        modelBuilder.Entity<RmcMessage>(typeBuilder =>
        {
            typeBuilder.HasKey(rmcMessage => new
            {
                rmcMessage.RawDirectoryId,
                rmcMessage.FileTime,
                rmcMessage.Index,
            });
            typeBuilder.HasIndex(rmcMessage => rmcMessage.RawDirectoryId);
            typeBuilder.HasIndex(rmcMessage => rmcMessage.FileTime);
            typeBuilder.HasIndex(rmcMessage => rmcMessage.Index);
            typeBuilder.HasIndex(rmcMessage => rmcMessage.IsAnalyzed);
            typeBuilder.HasIndex(rmcMessage => rmcMessage.RegistrarId);
            typeBuilder.HasOne(rmcMessage => rmcMessage.Registrar)
                .WithMany(registrar => registrar.RmcMessages)
                .HasForeignKey(rmcMessage => rmcMessage.RegistrarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RmcMessages_Registrars");
            typeBuilder.HasOne(rmcMessage => rmcMessage.RawDirectory)
                .WithMany(rawDirectory => rawDirectory.RmcMessages)
                .HasForeignKey(package => package.RawDirectoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RmcMessages_RawDirectories");
            typeBuilder.HasOne(rmcMessage => rmcMessage.NmeaFile)
                .WithMany(nmeaFile => nmeaFile.RmcMessages)
                .HasForeignKey(rmcMessage => new
                {
                    rmcMessage.RawDirectoryId,
                    rmcMessage.FileTime,
                })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RmcMessages_NmeaFiles");
        });

        modelBuilder.Entity<VtgMessage>(typeBuilder =>
        {
            typeBuilder.HasKey(vtgMessage => new
            {
                vtgMessage.RawDirectoryId,
                vtgMessage.FileTime,
                vtgMessage.Index,
            });
            typeBuilder.HasIndex(vtgMessage => vtgMessage.RawDirectoryId);
            typeBuilder.HasIndex(vtgMessage => vtgMessage.FileTime);
            typeBuilder.HasIndex(vtgMessage => vtgMessage.Index);
            typeBuilder.HasIndex(vtgMessage => vtgMessage.IsAnalyzed);
            typeBuilder.HasIndex(vtgMessage => vtgMessage.RegistrarId);
            typeBuilder.HasOne(vtgMessage => vtgMessage.Registrar)
                .WithMany(registrar => registrar.VtgMessages)
                .HasForeignKey(vtgMessage => vtgMessage.RegistrarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VtgMessages_Registrars");
            typeBuilder.HasOne(vtgMessage => vtgMessage.RawDirectory)
                .WithMany(rawDirectory => rawDirectory.VtgMessages)
                .HasForeignKey(package => package.RawDirectoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VtgMessages_RawDirectories");
            typeBuilder.HasOne(vtgMessage => vtgMessage.NmeaFile)
                .WithMany(nmeaFile => nmeaFile.VtgMessages)
                .HasForeignKey(vtgMessage => new
                {
                    vtgMessage.RawDirectoryId,
                    vtgMessage.FileTime,
                })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VtgMessages_NmeaFiles");
        });

        modelBuilder.Entity<Geolocation>(typeBuilder =>
        {
            typeBuilder.HasKey(geolocation => new
            {
                geolocation.RegistrarId,
                geolocation.Timestamp,
            });
            typeBuilder.HasIndex(geolocation => geolocation.RegistrarId);
            typeBuilder.HasIndex(geolocation => geolocation.Timestamp);
            typeBuilder.HasIndex(geolocation => geolocation.IsAnalyzed);
            typeBuilder.HasIndex(geolocation => geolocation.Time);
            typeBuilder.HasIndex(geolocation => geolocation.Latitude);
            typeBuilder.HasIndex(geolocation => geolocation.Longitude);
            typeBuilder.HasIndex(geolocation => geolocation.Speed);
            typeBuilder.HasOne(geolocation => geolocation.Registrar)
                .WithMany(registrar => registrar.Geolocations)
                .HasForeignKey(geolocation => geolocation.RegistrarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Geolocations_Registrars");
        });

        modelBuilder.Entity<Frame>(typeBuilder =>
        {
            typeBuilder.HasKey(frame => new
            {
                frame.RegistrarId,
                frame.Timestamp,
            });
            typeBuilder.HasIndex(frame => frame.RegistrarId);
            typeBuilder.HasIndex(frame => frame.Timestamp);
            typeBuilder.HasIndex(frame => frame.IsSpectrum);
            typeBuilder.HasIndex(frame => frame.IsStatistic);
            typeBuilder.HasIndex(frame => frame.IsExtremum);
            typeBuilder.HasIndex(frame => frame.Time);
            typeBuilder.HasOne(frame => frame.Registrar)
                .WithMany(registrar => registrar.Frames)
                .HasForeignKey(frame => frame.RegistrarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Frames_Registrars");
        });

        modelBuilder.Entity<Spectrum>(typeBuilder =>
        {
            typeBuilder.HasKey(spectrum => spectrum.Id);
            typeBuilder.HasOne(spectrum => spectrum.Registrar)
                .WithMany(registrar => registrar.Spectrums)
                .HasForeignKey(spectrum => spectrum.RegistrarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Spectrums_Registrars");
            typeBuilder.HasOne(spectrum => spectrum.Channel)
                .WithMany(channel => channel.Spectrums)
                .HasForeignKey(spectrum => spectrum.ChannelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Spectrums_Channels");
            typeBuilder.HasOne(spectrum => spectrum.Frame)
                .WithMany(frame => frame.Spectrums)
                .HasForeignKey(spectrum => new
                {
                    spectrum.RegistrarId,
                    spectrum.Timestamp,
                })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Spectrums_Frames");
        });

        modelBuilder.Entity<Amplitude>(typeBuilder =>
        {
            typeBuilder.HasKey(amplitude => amplitude.Id);
            typeBuilder.HasIndex(amplitude => amplitude.SpectrumId);
            typeBuilder.HasIndex(amplitude => amplitude.Frequency);
            typeBuilder.HasIndex(amplitude => amplitude.Magnitude);
            typeBuilder.HasOne(amplitude => amplitude.Spectrum)
                .WithMany(spectrum => spectrum.Amplitudes)
                .HasForeignKey(amplitude => amplitude.SpectrumId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Amplitudes_Spectrums");
        });

        modelBuilder.Entity<Filter>(Filter.BuildAction);
        modelBuilder.Entity<Location>(Location.BuildAction);
        modelBuilder.Entity<Statistic>(Statistic.BuildAction);
        modelBuilder.Entity<Extremum>(Extremum.BuildAction);
    }
}
