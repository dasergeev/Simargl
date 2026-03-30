namespace Apeiron.Platform.Performers.Ape90Performers;

/// <summary>
/// Представляет исполнителя, определяющего метрики исходных файлов.
/// </summary>
public sealed class Ape90DefinitionRawFileMetrics :
    Performer
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="journal">
    /// Журнал.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="journal"/> передана пустая ссылка.
    /// </exception>
    public Ape90DefinitionRawFileMetrics(Journal journal) :
        base(journal)
    {

    }

    /// <summary>
    /// Асинхронно выполняет работу исполнителя.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу исполнителя.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public override sealed async Task PerformAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Выполнение работы с различными типами файлов.
        await Parallel.ForEachAsync(
            new Func<CancellationToken, ValueTask>[]
            {
                WorkWithRawFileAsync<RawFrame>,
                WorkWithRawFileAsync<RawGeolocation>,
                WorkWithRawFileAsync<RawPower>
            },
            new ParallelOptions()
            {
                CancellationToken = cancellationToken,
            },
            async (func, cancellationToken) => await func(cancellationToken))
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет работу с исходными файлами.
    /// </summary>
    /// <typeparam name="TRawFile">
    /// Тип исходного файла.
    /// </typeparam>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с исходными файлами.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async ValueTask WorkWithRawFileAsync<TRawFile>(CancellationToken cancellationToken)
        where TRawFile : RawFile
    {
        //  Поддержка выполнения.
        await KeepAsync(async cancellationToken =>
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Создание контекста сеанса работы с базой данных.
            using Ape90DatabaseContext context = Ape90DatabaseContextProvider.CreateContext();

            //  Запрос файлов с неопределёнными метриками.
            IAsyncEnumerable<TRawFile> rawFiles = context.Set<TRawFile>()
                .Where(rawFile => !rawFile.IsMetrics)
                .AsAsyncEnumerable();

            //  Перебор файлов.
            await foreach (TRawFile rawFile in rawFiles)
            {
                //  Безопасный вызов.
                await SafeCallAsync(async cancellationToken =>
                {
                    //  Проверка токена отмены.
                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                    //  Получение информации о файле.
                    FileInfo fileInfo = new(rawFile.Path);

                    //  Создание контекста сеанса работы с базой данных для транзакции.
                    using Ape90DatabaseContext context = Ape90DatabaseContextProvider.CreateContext();

                    //  Начало транзакции.
                    using var transaction = context.Database.BeginTransaction();

                    //  Присоединение сущности к контексту.
                    context.Attach(rawFile);

                    //  Установка значения, определяющего определены ли метрики файла.
                    rawFile.IsMetrics = true;

                    //  Установка метрик файла.
                    rawFile.NameTime = GetNameTime(fileInfo);
                    rawFile.CreationTime = fileInfo.CreationTime;
                    rawFile.LastAccessTime = fileInfo.LastAccessTime;
                    rawFile.LastWriteTime = fileInfo.LastWriteTime;
                    rawFile.Size = fileInfo.Length;

                    //  Сохранение изменений в базу данных.
                    context.SaveChanges();

                    //  Фиксирование изменений.
                    transaction.Commit();

                    //  Вывод информации в журнал.
                    await Journal.LogInformationAsync(
                        $"Определены метрики файла: {fileInfo.FullName}",
                        cancellationToken).ConfigureAwait(false);
                }, cancellationToken).ConfigureAwait(false);
            }
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Определяет время по имени файла.
    /// </summary>
    /// <param name="file">
    /// Информация о файле.
    /// </param>
    /// <returns>
    /// Время, которое содержится в имени файла.
    /// </returns>
    private static DateTime GetNameTime(FileInfo file)
    {
        //  Получение имени файла.
        string name = file.Name[..^file.Extension.Length];

        //  Проверка расширения файла.
        if (file.Extension == ".txt" || file.Extension == ".power")
        {
            //  Разбивка имени на части.
            string[] parts = name.Split('-');

            //  Возврат времени.
            return new(
                int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]),
                int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]),
                int.Parse(parts[6]));
        }
        else
        {
            //  Разбивка на блоки.
            string[] blocks = name.Split(' ');

            //  Разбивка блока даты на части.
            string[] dateParts = blocks[1].Split('.');

            //  Разбивка блока времени на части.
            string[] timeParts = blocks[2].Split('_');

            //  Возврат времени.
            return new(
                int.Parse(dateParts[2]), int.Parse(dateParts[1]), int.Parse(dateParts[0]),
                int.Parse(timeParts[0]), int.Parse(timeParts[1]), int.Parse(timeParts[2]));
        }
    }
}
