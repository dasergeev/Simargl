//using Apeiron.Platform.Databases.CentralDatabase;
//using Apeiron.Platform.Databases.CentralDatabase.Entities;

//namespace Apeiron.Platform.Server.Services.Microservices;

///// <summary>
///// Представляет микрослужбу, выполняющую анализ базы данных.
///// </summary>
//public sealed class DatabaseAnalyser :
//    ServerMicroservice<DatabaseAnalyser>
//{
//    /// <summary>
//    /// Инициализирует новый экземпляр класса.
//    /// </summary>
//    /// <param name="logger">
//    /// Средство ведения журнала микрослужбы.
//    /// </param>
//    /// <exception cref="ArgumentNullException">
//    /// В параметре <paramref name="logger"/> передана пустая ссылка.
//    /// </exception>
//    public DatabaseAnalyser(ILogger<DatabaseAnalyser> logger) :
//        base(logger)
//    {

//    }

//    /// <summary>
//    /// Асинхронно выполняет шаг работы микрослужбы.
//    /// </summary>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая шаг работы микрослужбы.
//    /// </returns>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    protected override sealed async ValueTask MakeStepAsync(CancellationToken cancellationToken)
//    {
//        //  Проверка токена отмены.
//        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//        //  Получение информации о таблицах.
//        var infos = CentralDatabaseAgent.Request(session => session.GetAllTables()
//            .Select(table => new
//            {
//                table.Name,
//                table.Category,
//            }));

//        //  Проверка токена отмены.
//        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//        //  Перебор всех таблиц.
//        foreach (var info in infos)
//        {
//            //  Получение категории таблицы.
//            DatabaseTableCategory category = await CentralDatabaseAgent.RetrievalAsync(
//                category => category.Name == info.Category,
//                () => new DatabaseTableCategory(info.Category),
//                cancellationToken).ConfigureAwait(false);

//            //  Получение информации о таблице.
//            DatabaseTable table = await CentralDatabaseAgent.RetrievalAsync(
//                table => table.Name == info.Name,
//                () => new DatabaseTable(info.Name, category.Id),
//                cancellationToken).ConfigureAwait(false);

//            //  Получение идентификатора таблицы.
//            long tableId = table.Id;

//            //  Запрос количества элементов в таблице.
//            long count = await CentralDatabaseAgent.RequestAsync(
//                async (session, cancellationToken) => await session.GetTable(info.Name)
//                    .GetCountAsync(cancellationToken).ConfigureAwait(false),
//                cancellationToken)
//                .ConfigureAwait(false);

//            //  Определение времени внесения изменений.
//            DateTime determinationTime = DateTime.Now;

//            //  Создание метрики.
//            DatabaseTableMetric metric = new(tableId)
//            {
//                DeterminationTime = determinationTime,
//                Count = count,
//            };

//            //  Добавление метрики в базу данных.
//            await CentralDatabaseAgent.TransactionAsync(
//                async (session, cancellationToken) => await session.DatabaseTableMetrics
//                    .AddAsync(metric, cancellationToken)
//                    .ConfigureAwait(false),
//                cancellationToken).ConfigureAwait(false);

//            //  Запрос времени начала часа.
//            DateTime beginHour = await getBeginTimeAsync(
//                determinationTime.AddHours(-1),
//                cancellationToken).ConfigureAwait(false);

//            //  Запрос времени начала дня.
//            DateTime beginDay = await getBeginTimeAsync(
//                determinationTime.AddDays(-1),
//                cancellationToken).ConfigureAwait(false);

//            //  Запрос времени начала месяца.
//            DateTime beginMonth = await getBeginTimeAsync(
//                determinationTime.AddDays(-30),
//                cancellationToken).ConfigureAwait(false);

//            //  Определение изменения количества элементов в таблице за час.
//            long changedInHour = count - await getCountAsync(beginHour, cancellationToken).ConfigureAwait(false);

//            //  Определение изменения количества элементов в таблице за день.
//            long changedInDay = count - await getCountAsync(beginDay, cancellationToken).ConfigureAwait(false);

//            //  Определение изменения количества элементов в таблице за месяц.
//            long changedInMonth = count - await getCountAsync(beginMonth, cancellationToken).ConfigureAwait(false);

//            //  Определение длительности часового периода.
//            double durationHour = (determinationTime - beginHour).TotalHours;

//            //  Определение длительности дневого периода.
//            double durationDay = (determinationTime - beginDay).TotalDays;

//            //  Определение длительности месячного периода.
//            double durationMonth = (determinationTime - beginMonth).TotalDays / 30;

//            //  Определение скорости изменения количества элементов за час.
//            double speedPerHour = durationHour != 0 ? changedInHour / durationHour : 0;

//            //  Определение скорости изменения количества элементов за день.
//            double speedPerDay = durationDay != 0 ? changedInDay / durationDay : 0;

//            //  Определение скорости изменения количества элементов за месяц.
//            double speedPerMonth = durationMonth != 0 ? changedInMonth / durationMonth : 0;

//            //  Обновление данных таблицы.
//            await CentralDatabaseAgent.TransactionAsync(
//                async (session, cancellationToken) =>
//                {
//                    //  Проверка токена отмены.
//                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//                    //  Запрос таблицы.
//                    DatabaseTable table = await session.DatabaseTables
//                        .FirstAsync(
//                            table => table.Id == tableId,
//                            cancellationToken).ConfigureAwait(false);

//                    //  Обновление параметров таблицы.
//                    table.Count = count;
//                    table.ChangedInHour = changedInHour;
//                    table.ChangedInDay = changedInDay;
//                    table.ChangedInMonth = changedInMonth;
//                    table.SpeedPerHour = speedPerHour;
//                    table.SpeedPerDay = speedPerDay;
//                    table.SpeedPerMonth = speedPerMonth;
//                },
//                cancellationToken).ConfigureAwait(false);

//            //  Возвращает начальное время.
//            async Task<DateTime> getBeginTimeAsync(DateTime beginTime, CancellationToken cancellationToken) =>
//                await CentralDatabaseAgent.RequestAsync(
//                    async (session, cancellationToken) => await session.DatabaseTableMetrics
//                        .Where(metric => metric.TableId == tableId && metric.DeterminationTime >= beginTime)
//                        .Select(metric => metric.DeterminationTime)
//                        .OrderBy(time => time)
//                        .FirstAsync(cancellationToken)
//                        .ConfigureAwait(false),
//                    cancellationToken).ConfigureAwait(false);

//            //  Возвращает количество элементов в указанное время.
//            async Task<long> getCountAsync(DateTime time, CancellationToken cancellationToken) =>
//                await CentralDatabaseAgent.RequestAsync(
//                    async (session, cancellationToken) => await session.DatabaseTableMetrics
//                        .Where(metric => metric.TableId == tableId && metric.DeterminationTime == time)
//                        .Select(metric => metric.Count)
//                        .FirstAsync(cancellationToken)
//                        .ConfigureAwait(false),
//                    cancellationToken).ConfigureAwait(false);
//        }

//        //  Вывод информации в журнал.
//        Logger.LogInformation("Метрики таблиц базы данных обновлены.");
//    }
//}
