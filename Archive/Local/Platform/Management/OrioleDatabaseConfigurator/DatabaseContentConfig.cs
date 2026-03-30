using Apeiron.Platform.Databases.OrioleDatabase;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Apeiron.Oriole.DatabaseConfigurator;

/// <summary>
/// Настраивает DBContext.
/// </summary>
public static class DatabaseContentConfig
{
    /// <summary>
    /// Возвращает настроенный DbContextOptionsBuilder.
    /// </summary>
    public static DbContextOptionsBuilder<OrioleDatabaseContext> DbContextOptionsBuilder { get; }

    /// <summary>
    /// Статический конструктор.
    /// </summary>
    static DatabaseContentConfig()
    {
        //  Создание построителя строки подключения к серверу баз данных.
        SqlConnectionStringBuilder sqlConnectionStringBuilder = new()
        {
            ApplicationName = "Oriole-Test",
            InitialCatalog = "Oriole-Test",
            DataSource = "10.69.16.236\\MSSQL", //  "10.69.16.236\\MSSQL"
            UserID = "sa",
            Password = "!TTCRTdbsa",
            MultipleActiveResultSets = true,
            ConnectTimeout = 600,
            ConnectRetryCount = 255,
            ConnectRetryInterval = 60,
            Pooling = true,
        };

        // Конфигурирование контекста БД через Builder.
        var optionsBuilder = new DbContextOptionsBuilder<OrioleDatabaseContext>();
        optionsBuilder.UseSqlServer(sqlConnectionStringBuilder.ConnectionString,
            providerOptions => providerOptions.EnableRetryOnFailure());

#if DEBUG
        optionsBuilder.LogTo(message => Debug.WriteLine(message),
            new[] { DbLoggerCategory.Database.Command.Name },
            LogLevel.Information)
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors();
#endif
        // Создаём настройки.
        DbContextOptionsBuilder = optionsBuilder;
    }
}
