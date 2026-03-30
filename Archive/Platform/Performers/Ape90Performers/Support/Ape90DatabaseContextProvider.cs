namespace Apeiron.Platform.Performers.Support;

/// <summary>
/// Предоставляет контекст базы данных.
/// </summary>
internal static class Ape90DatabaseContextProvider
{
    /// <summary>
    /// Возвращает новый контекст базы данных.
    /// </summary>
    /// <returns>
    /// Новый контекст базы данных.
    /// </returns>
    public static Ape90DatabaseContext CreateContext()
    {
        ////  Создание нового контекста.
        //Ape90DatabasePostgresContext context = new();

        //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        ////  Возврат нового контекста.
        //return context;


        return new Ape90DatabaseContext();
    }
}
