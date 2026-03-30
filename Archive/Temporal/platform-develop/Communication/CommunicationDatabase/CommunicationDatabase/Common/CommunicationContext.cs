using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Apeiron.Platform.Communication.CommunicationDatabase;

/// <summary>
/// Представляет контекст базы данных коммуникации.
/// </summary>
[CLSCompliant(false)]
public sealed class CommunicationContext :
    DbContext
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public CommunicationContext()
    {
        //  Установка времени ожидания, которое будет использоваться для команд,
        //  выполняемых с этого контекста сеанса работы с базой данных.
        Database.SetCommandTimeout(TimeSpan.FromMinutes(60));
    }

    /// <summary>
    /// Возвращает таблицу пользователей.
    /// </summary>
    public DbSet<UserData> Users { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу сообщений.
    /// </summary>
    public DbSet<MessageData> Messages { get; init; } = null!;

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
                ApplicationName = @"Communication",
                InitialCatalog = @"Communication",
                //DataSource = @"10.69.16.236\MSSQL",
                DataSource = @"192.168.1.10\MSSQL",
                UserID = @"sa",
                Password = @"!TTCRTdbsa",
                MultipleActiveResultSets = true,
                ConnectTimeout = 600,
                ConnectRetryCount = 255,
                ConnectRetryInterval = 60,
                Pooling = true,
                TrustServerCertificate = true,
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
        modelBuilder.Entity<UserData>(UserData.BuildAction);
        modelBuilder.Entity<MessageData>(MessageData.BuildAction);
    }
}
