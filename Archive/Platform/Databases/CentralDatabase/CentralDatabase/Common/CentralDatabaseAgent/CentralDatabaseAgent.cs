using System.Linq.Expressions;

namespace Apeiron.Platform.Databases.CentralDatabase;

/// <summary>
/// Представляет посредника для взаимодействия с центральной базой данных.
/// </summary>
public static partial class CentralDatabaseAgent
{
    /// <summary>
    /// Возвращает логику взаимодействия с центральной базой данных.
    /// </summary>
    public static Logic Logic { get; } = CreateLogic();

    /// <summary>
    /// Создаёт логику взаимодействия с центральной базой данных.
    /// </summary>
    /// <returns>
    /// Логика взаимодействия с центральной базой данных.
    /// </returns>
    private static Logic CreateLogic()
    {
        //  Создание логики.
        Logic logic = new();

        //  Настройка подключения.
        logic.Connection.ApplicationName = "Platform";
        logic.Connection.InitialCatalog = "Central";
        logic.Connection.DataSource = "10.69.16.236\\MSSQL";
        logic.Connection.UserID = "sa";
        logic.Connection.Password = "!TTCRTdbsa";
        logic.Connection.MultipleActiveResultSets = true;
        logic.Connection.ConnectTimeout = 600;
        logic.Connection.ConnectRetryCount = 255;
        logic.Connection.ConnectRetryInterval = 60;
        logic.Connection.Pooling = true;

        //  Настройка времени ожидания команд, выполняемых с базой данных.
        logic.CommandTimeout = TimeSpan.FromMinutes(30);

        //  Возврат логики.
        return logic;
    }

    /// <summary>
    /// Выполняет инициализацию контекста сеанса работы с центральной базой данных.
    /// </summary>
    /// <param name="context">
    /// Контекст сеанса работы с центральной базой данных.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void InitializeContext(CentralDatabaseContext context)
    {
        //  Установка времени ожидания команд, выполняемых с базой данных.
        context.Database.SetCommandTimeout(Logic.CommandTimeout);
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
    [CLSCompliant(false)]
    public static TResult Request<TResult>(Func<Session, TResult> request)
    {
        //  Контекст сеанса работы с базой данных.
        CentralDatabaseContext? context = null;

        try
        {
            //  Получение контекста сеанса работы с базой данных.
            context = Attach();

            //  Выполнение запроса.
            return request(new(context));
        }
        finally
        {
            //  Проверка ссылки на сеанс.
            if (context is not null)
            {
                //  Отсоединение.
                Detach(context);
            }
        }
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
    [CLSCompliant(false)]
    public static async Task<TResult> RequestAsync<TResult>(
        Func<Session, CancellationToken, Task<TResult>> request,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Контекст сеанса работы с базой данных.
        CentralDatabaseContext? context = null;

        try
        {
            //  Получение контекста сеанса работы с базой данных.
            context = await AttachAsync(cancellationToken).ConfigureAwait(false);

            //  Выполнение запроса.
            return await request(new(context), cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            //  Проверка ссылки на сеанс.
            if (context is not null)
            {
                //  Отсоединение.
                await DetachAsync(context);
            }
        }
    }

    /// <summary>
    /// Выполняет транзакцию.
    /// </summary>
    /// <param name="changer">
    /// Метод, вносящий изменения в базу данных.
    /// </param>
    [CLSCompliant(false)]
    public static void Transaction(Action<Session> changer)
    {
        //  Контекст сеанса работы с базой данных.
        CentralDatabaseContext? context = null;

        try
        {
            //  Получение контекста сеанса работы с базой данных.
            context = Attach();

            //  Начало транзакции.
            using var transaction = context.Database.BeginTransaction();

            //  Внесение изменений.
            changer(new(context));

            //  Сохранение изменений в базу данных.
            int count = context.SaveChanges();

            //  Фиксирование изменений.
            transaction.Commit();

            ////  Корректировка счётчика изменений.
            //Interlocked.Add(ref _ChangeCount, count);
        }
        finally
        {
            //  Проверка ссылки на сеанс.
            if (context is not null)
            {
                //  Отсоединение.
                Detach(context);
            }
        }
    }

    /// <summary>
    /// Асинхронно возвращает сущность, при необходимости создавая её.
    /// </summary>
    /// <typeparam name="TEntity">
    /// Тип сущности.
    /// </typeparam>
    /// <param name="predicate">
    /// Выражение для поиска сущности.
    /// </param>
    /// <param name="creator">
    /// Метод, создающий сущность.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, возвращающая сущность.
    /// </returns>
    /// <remarks>
    /// В случае создания сущности метод выполняет транзакцию.
    /// </remarks>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public static async Task<TEntity> RetrievalAsync<TEntity>(
        [ParameterNoChecks] Expression<Func<TEntity, bool>> predicate,
        [ParameterNoChecks] Func<TEntity> creator,
        CancellationToken cancellationToken)
        where TEntity : Entity
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Запрос сущности из базы данных.
        TEntity? entity = await RequestAsync(
            async (session, cancellationToken) => await session.GetTable<TEntity>()
                .AsNoTracking()
                .Where(predicate)
                .FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false),
            cancellationToken).ConfigureAwait(false);

        //  Проверка полученной сущности.
        if (entity is not null)
        {
            //  Возврат полученной сущности.
            return entity;
        }

        //  Создание новой сущности.
        entity = creator();

        //  Проверка созданной сущности.
        if (!predicate.Compile()(entity))
        {
            //  Созданная сущность не удовлетворяет условиям поиска.
            throw new InvalidOperationException("Созданная сущность не удовлетворяет условиям поиска.");
        }

        //  Добавление новой сущности в базу данных.
        await TransactionAsync(
            async (session, cancellationToken) => await session.GetTable<TEntity>()
                .AddAsync(entity, cancellationToken).ConfigureAwait(false),
            cancellationToken).ConfigureAwait(false);

        //  Возврат новой сущности.
        return entity;

        //try
        //{
        //}
        //catch (DbUpdateException)
        //{

        //}

        ////  Запрос сущности из базы данных.
        //return await RequestAsync(
        //    async (session, cancellationToken) => await session.GetTable<TEntity>()
        //        .AsNoTracking()
        //        .Where(predicate)
        //        .FirstAsync(cancellationToken).ConfigureAwait(false),
        //    cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно возвращает сущность, при необходимости создавая её.
    /// </summary>
    /// <typeparam name="TEntity">
    /// Тип сущности.
    /// </typeparam>
    /// <param name="predicate">
    /// Выражение для поиска сущности.
    /// </param>
    /// <param name="creator">
    /// Метод, создающий сущность.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, возвращающая сущность.
    /// </returns>
    /// <remarks>
    /// В случае создания сущности метод выполняет транзакцию.
    /// </remarks>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public static async Task<TEntity> RetrievalAsync<TEntity>(
        [ParameterNoChecks] Expression<Func<TEntity, bool>> predicate,
        [ParameterNoChecks] Func<CancellationToken, Task<TEntity>> creator,
        CancellationToken cancellationToken)
        where TEntity : Entity
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Запрос сущности из базы данных.
        TEntity? entity = await RequestAsync(
            async (session, cancellationToken) => await session.GetTable<TEntity>()
                .AsNoTracking()
                .Where(predicate)
                .FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false),
            cancellationToken).ConfigureAwait(false);

        //  Проверка полученной сущности.
        if (entity is not null)
        {
            //  Возврат полученной сущности.
            return entity;
        }

        //  Создание новой сущности.
        entity = await creator(cancellationToken).ConfigureAwait(false);

        //  Проверка созданной сущности.
        if (!predicate.Compile()(entity))
        {
            //  Созданная сущность не удовлетворяет условиям поиска.
            throw new InvalidOperationException("Созданная сущность не удовлетворяет условиям поиска.");
        }

        //  Добавление новой сущности в базу данных.
        await TransactionAsync(
            async (session, cancellationToken) => await session.GetTable<TEntity>()
                .AddAsync(entity, cancellationToken).ConfigureAwait(false),
            cancellationToken).ConfigureAwait(false);

        //  Возврат новой сущности.
        return entity;
    }

    /// <summary>
    /// Асинхронно выполняет транзакцию.
    /// </summary>
    /// <param name="changer">
    /// Метод, вносящий изменения в базу данных.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая транзакцию.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    [CLSCompliant(false)]
    public static async Task TransactionAsync(
        Func<Session, CancellationToken, Task> changer,
        CancellationToken cancellationToken)
    {
        //  Контекст сеанса работы с базой данных.
        CentralDatabaseContext? context = null;

        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        try
        {
            //  Получение контекста сеанса работы с базой данных.
            context = await AttachAsync(cancellationToken).ConfigureAwait(false);

            //  Начало транзакции.
            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

            //  Внесение изменений.
            await changer(new(context), cancellationToken).ConfigureAwait(false);

            ////  Создание объекта для прикрепления сущностей к контексту.
            //Attachment attachment = new(context);

            ////  Перебор всех отслеживаемых сущностей.
            //foreach (EntityEntry entityEntry in context.ChangeTracker.Entries())
            //{
            //    //  Проверка связанной сущности.
            //    if (entityEntry.Entity is Entity entity)
            //    {
            //        //  Прикрепление связанной сущности.
            //        entity.AttachToContext(attachment);
            //    }
            //}

            //  Сохранение изменений в базу данных.
            int count = await context.SaveChangesAsync(cancellationToken);

            //  Фиксирование изменений.
            await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);

            ////  Корректировка счётчика изменений.
            //Interlocked.Add(ref _ChangeCount, count);
        }
        finally
        {
            //  Проверка ссылки на сеанс.
            if (context is not null)
            {
                //  Отсоединение.
                await DetachAsync(context);
            }
        }
    }

    /// <summary>
    /// Получает контекст сеанса работы с базой данных.
    /// </summary>
    /// <returns>
    /// Контекст сеанса работы с базой данных.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static CentralDatabaseContext Attach()
    {
        //  Создание контекста сеанса работы с базой данных.
        CentralDatabaseContext context = new();

        //  Возврат контекста.
        return context;
    }

    /// <summary>
    /// Асинхронно получает контекст сеанса работы с базой данных.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, асинхронно получающая соединение к базе данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static async ValueTask<CentralDatabaseContext> AttachAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Синхронное получение контекста сеанса работы с базой данных.
        return Attach();
    }

    /// <summary>
    /// Освобождает контекст сеанса работы с базой данных.
    /// </summary>
    /// <param name="context">
    /// Контекст сеанса работы с базой данных.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Detach(CentralDatabaseContext context)
    {
        //  Разрушение контекста.
        context.Dispose();
    }

    /// <summary>
    /// Освобождает контекст сеанса работы с базой данных.
    /// </summary>
    /// <param name="context">
    /// Контекст сеанса работы с базой данных.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static async ValueTask DetachAsync(CentralDatabaseContext context)
    {
        //  Разрушение контекста.
        await context.DisposeAsync();
    }
}
