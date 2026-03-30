using Simargl.Border.Storage.Entities;
using System.Net;

namespace Simargl.Border.Storage;

/// <summary>
/// Представляет контекст базы данных.
/// </summary>
[CLSCompliant(false)]
public sealed class BorderStorageContext(IPEndPoint point, string database, string username, string password) :
    DbContext
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public BorderStorageContext() :
        this(new(IPAddress.Parse("10.7.0.17"), 5432), "Border", "postgres", "123QWEasd")
    {
        
    }

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="storage">
    /// Данные подключения.
    /// </param>
    public BorderStorageContext((IPEndPoint EndPoint, string Database, string Username, string Password) storage) :
        this(storage.EndPoint, storage.Database, storage.Username, storage.Password)
    {

    }

    /// <summary>
    /// Возвращает или инициализирует таблицу данных проездов.
    /// </summary>
    public DbSet<PassageData> Passages { get; init; } = null!;

    /// <summary>
    /// Возвращает или инициализирует таблицу осей.
    /// </summary>
    public DbSet<AxisData> Axes { get; init; } = null!;

    /// <summary>
    /// Возвращает или инициализирует таблицу взаимодействий.
    /// </summary>
    public DbSet<AxisInteractionData> AxisInteractions { get; init; } = null!;

    /// <summary>
    /// Выполняет настройку модели базы данных.
    /// </summary>
    /// <param name="builder">
    /// Построитель, используемый для настройки модели базы данных.
    /// </param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //  Настройка моделей сущности.
        builder.Entity<PassageData>(PassageData.BuildAction);
        builder.Entity<AxisData>(AxisData.BuildAction);
        builder.Entity<AxisInteractionData>(AxisInteractionData.BuildAction);
    }

    /// <summary>
    /// Выполняет настройку базы данных.
    /// </summary>
    /// <param name="optionsBuilder">
    /// Построитель, используемый для создания или изменения параметров этого контекста.
    /// </param>
    protected override sealed void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //  Проверка настроенных параметров.
        if (!optionsBuilder.IsConfigured)
        {
            //  Установка пути к файлу базы данных.
            optionsBuilder.UseNpgsql(
                $"Host={point.Address};" +
                $"Port={point.Port};" +
                $"Database={database};" +
                $"Username={username};" +
                $"Password={password}");
        }
    }
}
