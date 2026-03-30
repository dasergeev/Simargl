namespace Apeiron.Platform.Databases.OsmRussiaDatabase;

/// <summary>
/// Представляет класс заглушку для сопостоваления встроенных функций БД.
/// </summary>
public static class DbFunctionsExtensions
{
    /// <summary>
    /// Работа со вложенным словарём. Поиск по ключу и значени.
    /// </summary>
    /// <param name="dic">Словарь.</param>
    /// <param name="key">Ключ.</param>
    /// <param name="operator">Операция.</param>
    /// <param name="value">Значение.</param>
    public static bool HasKeyValue(Dictionary<string, string> dic, string key, string @operator, string value)
    {
        throw new NotImplementedException("For use only as an EF core Db function");
    }

    /// <summary>
    /// Работа со вложенным словарём. Поиск по ключу.
    /// </summary>
    /// <param name="dic">Словарь.</param>
    /// <param name="key">Ключ.</param>
    public static bool HasKey(Dictionary<string, string> dic, string key)
    {
        throw new NotImplementedException("For use only as an EF core Db function");
    }
}
