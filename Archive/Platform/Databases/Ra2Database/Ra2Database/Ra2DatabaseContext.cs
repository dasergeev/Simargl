using ApeironApeiron.Platform.Databases.Ra2Database.Entities;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace Apeiron.Platform.Databases.Ra2Database;

/// <summary>
/// Представляет контекст сеанса работы с базой данных.
/// </summary>
[CLSCompliant(false)]
public sealed class Ra2DatabaseContext :
    DatabaseContext
{
    /// <summary>
    /// Возвращает таблицу информации о службах.
    /// </summary>
    public DbSet<RawFile> RawFiles { get; init; } = null!;

    /// <summary>
    /// Возвращает таблицу информации о результатах.
    /// </summary>
    public DbSet<ChannelResult> ChannelResults { get; init; } = null!;

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
                ApplicationName = "Ra2",
                InitialCatalog = "Ra2",
                DataSource = @"10.69.16.236\MSSQL",
                UserID = "sa",
                Password = "!TTCRTdbsa",
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

    /// <summary>
    /// Выполняет настройку модели базы данных.
    /// </summary>
    /// <param name="modelBuilder">
    /// Построитель, используемый для настройки модели базы данных.
    /// </param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RawFile>(RawFile.BuildAction);
        modelBuilder.Entity<ChannelResult>(ChannelResult.BuildAction);
    }

    /// <summary>
    /// Выполняет запрос к базе данных.
    /// </summary>
    /// <typeparam name="TResult">
    /// Тип результата запроса.
    /// </typeparam>
    /// <param name="request">
    /// Запрос.
    /// </param>
    /// <returns>
    /// Результат запроса.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Request<TResult>([ParameterNoChecks] Func<Ra2DatabaseContext, TResult> request)
    {
        //  Создание контекста сеанса работы с базой данных.
        using Ra2DatabaseContext context = new();

        //  Выполнение запроса.
        return request(context);
    }

    /// <summary>
    /// Асинхронно выполняет запрос к базе данных.
    /// </summary>
    /// <typeparam name="TResult">
    /// Тип результата запроса.
    /// </typeparam>
    /// <param name="request">
    /// Запрос.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запрос к базе данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<TResult> RequestAsync<TResult>(
        [ParameterNoChecks] Func<Ra2DatabaseContext, CancellationToken, Task<TResult>> request,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание контекста сеанса работы с базой данных.
        await using Ra2DatabaseContext context = new();

        //  Выполнение запроса.
        return await request(context, cancellationToken).ConfigureAwait(false);
    }
}

