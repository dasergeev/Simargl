namespace Apeiron.Platform.Databases.CentralDatabase;

/// <summary>
/// Представляет контекст сеанса работы с центральной базой данных.
/// </summary>
[CLSCompliant(false)]
public sealed partial class CentralDatabaseContext :
    DatabaseContext
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public CentralDatabaseContext()
    {
        //  Инициализация контекста.
        CentralDatabaseAgent.InitializeContext(this);
    }

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
            optionsBuilder.UseSqlServer(CentralDatabaseAgent.Logic.Connection.ConnectionString);
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
        //  Настройка модели.
        OnModelCreatingCore(modelBuilder);

        //  Настройка таблицы узелов карты.
        modelBuilder.Entity<ECMapNode>(ECMapNode.BuildAction);
    }
}
