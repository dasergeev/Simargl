using Apeiron.Platform.Databases.GlobalIdentityDatabase;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Apeiron.Services.GlobalIdentity;

/// <summary>
/// Содержит методы расширения для контекста БД.
/// </summary>
public static class DatabaseContextExtentions
{
    /// <summary>
    /// Возвращает 
    /// </summary>
    /// <param name="databaseContext">Контекст базы данных.</param>
    /// <param name="connectTimeout">Параметр таймаута в сек. ожидания установления подключения к БД.</param>
    /// <returns>Возвращает DBContext с заданным таймаутом подключения к БД.</returns>
    public static GlobalIdentityDatabaseContext GetCustomDBContext (this DbContext databaseContext, int connectTimeout)
    {
        // Объект синхронизации.
        object syncRoot = new();
        SqlConnectionStringBuilder sqlConnectionStringBuilder;

        lock (syncRoot)
        {
            // Конфигурируем вспомогательный контекст БД для тестирования подключения.
            sqlConnectionStringBuilder = new(databaseContext.Database.GetConnectionString());
        }

        // Устанавливаем время ожидания подключения в сек.
        sqlConnectionStringBuilder.ConnectTimeout = connectTimeout;
        var optionsBuilder = new DbContextOptionsBuilder<GlobalIdentityDatabaseContext>();
        optionsBuilder.UseSqlServer(sqlConnectionStringBuilder.ConnectionString, providerOptions => providerOptions.EnableRetryOnFailure());

        // Создаём новый контекст подключения к базе данных с настройками для передачи в строке подключения небольшого таймаута.
        return new GlobalIdentityDatabaseContext(optionsBuilder.Options);
    }

    /// <summary>
    /// Проверяет есть ли подключение к БД.
    /// </summary>
    /// <param name="databaseContext">Контекст базы данных</param>
    /// <returns>Если соединение успешно устновлено, то возвращает Истина, иначе ложь.</returns>
    public static bool CheckConnectionDB(this DbContext databaseContext)
    {
        // Объект синхронизации.
        object syncRoot = new();

        try
        {
            lock (syncRoot)
            {
                databaseContext.Database.OpenConnection();
                databaseContext.Database.CloseConnection();
            }
            return true;
        }
        catch (Exception ex) when (ex is System.Data.SqlClient.SqlException || ex is SqlException)
        {
            return false;
        }
    }

    /// <summary>
    /// Ассинхронно проверяет подключение к БД.
    /// </summary>
    /// <param name="databaseContext">Контекст БД.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <exception cref = "OperationCanceledException" >
    /// Операция отменена.
    /// </exception>
    /// <returns>Если соединение успешно устновлено, то возвращает Истина, иначе ложь.</returns>
    public static async Task<bool> CheckConnectionDBAsync(this DbContext databaseContext, CancellationToken cancellationToken)
    {
        // Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken);

        try
        {
            await databaseContext.Database.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
            await databaseContext.Database.CloseConnectionAsync().ConfigureAwait(false);

            return true;
        }
        catch (Exception ex)
        {
            // Если исклюуение связанное с подключением к БД, то возвращаем false.
            if (ex is System.Data.SqlClient.SqlException || ex is SqlException)
            {
                return false;
            }

            // Проброс исключения на дальнейшую обработку.
            throw;
        }
    }

    /// <summary>
    /// Возвравщает имя БД.
    /// </summary>
    /// <param name="databaseContext">Контекст БД.</param>
    /// <returns>Имя БД.</returns>
    public static string GetDBName(this DbContext databaseContext)
    {
        // Объект синхронизации.
        object syncRoot = new();

        lock (syncRoot)
        {
            return databaseContext.Database.GetDbConnection().Database;
        }
    }
}
