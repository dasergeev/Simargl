namespace Apeiron.Platform.Databases.FileStorageDatabase;

/// <summary>
/// Представляет контекст сеанса работы с базой данных файловых хранилищ.
/// </summary>
[CLSCompliant(false)]
public sealed class FileStorageDatabaseContext :
    DatabaseContext
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public FileStorageDatabaseContext()
    {
        //  Установка времени ожидания, которое будет использоваться для команд,
        //  выполняемых с этого контекста сеанса работы с базой данных.
        Database.SetCommandTimeout(TimeSpan.FromMinutes(60));
    }

    /// <summary>
    /// Возвращает таблицу файловых хранилищ.
    /// </summary>
    public DbSet<FileStorage> FileStorages { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу информации о соединениях с файловыми хранилищами.
    /// </summary>
    public DbSet<FileStorageConnector> FileStorageConnectors { get; init; } = null!;

    /// <summary>
    /// Выполняет настройку базы данных.
    /// </summary>
    /// <param name="optionsBuilder">
    /// Построитель, используемый для создания или изменения параметров этого контекста.
    /// </param>
    protected override void OnConfiguring([ParameterNoChecks] DbContextOptionsBuilder optionsBuilder)
    {
        //  Проверка настроенных параметров.
        if (!optionsBuilder.IsConfigured)
        {
            //  Создание построителя строки подключения к серверу баз данных.
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new()
            {
                ApplicationName = @"FileStorage",
                InitialCatalog = @"FileStorage",
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
}
