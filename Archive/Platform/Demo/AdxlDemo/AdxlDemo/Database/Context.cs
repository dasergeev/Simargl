using Apeiron.Platform.Demo.AdxlDemo.Channels;
using Microsoft.EntityFrameworkCore;

namespace Apeiron.Platform.Demo.AdxlDemo.Database;

/// <summary>
/// Представляет контекст базы данных.
/// </summary>
[CLSCompliant(false)]
public sealed class Context :
    DbContext
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public Context()
    {
        //  Установка времени ожидания, которое будет использоваться для команд,
        //  выполняемых с этого контекста сеанса работы с базой данных.
        Database.SetCommandTimeout(TimeSpan.FromMinutes(60));
    }

    /// <summary>
    /// Возвращает или инициализирует таблицу информации о каналах.
    /// </summary>
    public DbSet<ChannelInfo> ChannelInfos { get; init; } = null!;

    /// <summary>
    /// Возвращает или инициализирует таблицу информации о фрагментах каналов.
    /// </summary>
    public DbSet<ChannelFragmentInfo> ChannelFragmentInfos { get; init; } = null!;

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
            //  Получение пути к файлу базы данных.
            //string databasePath = ((App)Application.Current).Settings.DatabasePath;
            string databasePath = Settings.DebugDatabasePath;

            //  Установка пути к файлу базы данных.
            optionsBuilder.UseSqlite($"Filename = \"{databasePath}\"");
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
        modelBuilder.Entity<ChannelInfo>(ChannelInfo.BuildAction);
        modelBuilder.Entity<ChannelFragmentInfo>(ChannelFragmentInfo.BuildAction);
    }
}
