using Microsoft.EntityFrameworkCore;

namespace Apeiron.Oriole.DatabaseConfigurator;

/// <summary>
/// Конфигурирует и создаёт DBContext.
/// </summary>
public static class DatabaseContextExtentions
{
    /// <summary>
    /// Проверяет есть ли подключение к БД.
    /// </summary>
    /// <param name="databaseContext">Контекст базы данных</param>
    /// <returns>Если соединение успешно устновлено, то возвращает Истина, иначе ложь.</returns>
    public static bool CheckConnection(this DbContext databaseContext)
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
        catch (Exception ex) when (ex is System.Data.SqlClient.SqlException || ex is Microsoft.Data.SqlClient.SqlException)
        {
            return false;
        }
    }

    /// <summary>
    /// Возвравщает имя БД.
    /// </summary>
    /// <param name="databaseContext">Контекст БД.</param>
    /// <returns></returns>
    public static string GetBaseName(this DbContext databaseContext)
    {
        // Объект синхронизации.
        object syncRoot = new();

        lock (syncRoot)
        {
            return databaseContext.Database.GetDbConnection().Database;
        }
    }
}
