namespace Apeiron.Platform.Performers.Ape90Performers;

/// <summary>
/// Представляет исполнителя, выполняющего поиск исходных файлов.
/// </summary>
public sealed class Ape90SearchRawFiles :
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
    public Ape90SearchRawFiles(Journal journal) :
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

        //  Создание контекста сеанса работы с базой данных.
        using Ape90DatabaseContext context = Ape90DatabaseContextProvider.CreateContext();

        //  Получение идентификаторов каталогов с исходными данными.
        long[] rawDirectoryIds = await context.RawDirectories
            .Select(rawDirectory => rawDirectory.Id)
            .ToArrayAsync(cancellationToken)
            .ConfigureAwait(false);

        //  Поиск по каталогам.
        await Parallel.ForEachAsync(
            rawDirectoryIds,
            new ParallelOptions()
            {
                CancellationToken = cancellationToken,
            },
            SearchInRawDirectoryAsync)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет поиск в каталоге исходных данных.
    /// </summary>
    /// <param name="rawDirectoryId">
    /// Идентификатор каталога исходных данных.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая поиск.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async ValueTask SearchInRawDirectoryAsync(long rawDirectoryId, CancellationToken cancellationToken)
    {
        //  Поддержка выполнения.
        await KeepAsync(async cancellationToken =>
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Создание контекста сеанса работы с базой данных.
            using Ape90DatabaseContext context = Ape90DatabaseContextProvider.CreateContext();

            //  Загрузка информации о каталоге.
            RawDirectory? rawDirectory = await context.RawDirectories
                .FindAsync(new object?[] { rawDirectoryId }, cancellationToken)
                .ConfigureAwait(false);

            //  Проверка информации.
            if (rawDirectory is not null)
            {
                //  Получение информации о каталоге.
                DirectoryInfo directory = new(PathBuilder.Normalize(rawDirectory.Path));

                //  Поиск в каталоге.
                await SearchInDirectoryAsync(rawDirectory, directory, cancellationToken).ConfigureAwait(false);
            }
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет поиск в каталоге.
    /// </summary>
    /// <param name="rawDirectory">
    /// Информация о каталоге исходных данных.
    /// </param>
    /// <param name="directory">
    /// Информация о каталоге.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая поиск.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async ValueTask SearchInDirectoryAsync(
        RawDirectory rawDirectory, DirectoryInfo directory, CancellationToken cancellationToken)
    {
        //  Безопасный вызов.
        await SafeCallAsync(async cancellationToken =>
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Поиск в подкаталогах.
            await Parallel.ForEachAsync(
                directory.GetDirectories(),
                new ParallelOptions()
                {
                    CancellationToken = cancellationToken,
                },
                async (subDirectory, cancellationToken) =>
                {
                    //  Поиск в подкаталоге.
                    await SearchInDirectoryAsync(rawDirectory, subDirectory, cancellationToken).ConfigureAwait(false);
                })
            .ConfigureAwait(false);

            //  Обработка файлов.
            await Parallel.ForEachAsync(
                directory.GetFiles(),
                new ParallelOptions()
                {
                    CancellationToken = cancellationToken,
                },
                async (file, cancellationToken) =>
                {
                    //  Поиск в подкаталоге.
                    await WorkWithFileAsync(rawDirectory, file, cancellationToken).ConfigureAwait(false);
                })
            .ConfigureAwait(false);
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет работу с файлом.
    /// </summary>
    /// <param name="rawDirectory">
    /// Информация о каталоге исходных данных.
    /// </param>
    /// <param name="file">
    /// Информация о каталоге.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с файлом.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async ValueTask WorkWithFileAsync(
        RawDirectory rawDirectory, FileInfo file, CancellationToken cancellationToken)
    {
        //  Безопасный вызов.
        await SafeCallAsync(async cancellationToken =>
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Создание контекста сеанса работы с базой данных.
            using Ape90DatabaseContext context = Ape90DatabaseContextProvider.CreateContext();

            //  Определение типа файла.
            switch (file.Extension)
            {
                case ".txt":    //  GPS-данные.
                    //  Поиск файла в базе.
                    if (!await context.RawGeolocations.AnyAsync(
                        rawFrame => rawFrame.Path == file.FullName,
                        cancellationToken).ConfigureAwait(false))
                    {
                        //  Добавление файла в базу.
                        context.RawGeolocations.Add(new()
                        {
                            Path = file.FullName,
                            RawDirectoryId = rawDirectory.Id,
                        });

                        //  Вывод информации в журнал.
                        await Journal.LogInformationAsync(
                            $"Найден новый файл с геолокационными данными: {file.FullName}",
                            cancellationToken).ConfigureAwait(false);
                    }
                    break;
                case ".power":  //  Данные питания.
                    //  Поиск файла в базе.
                    if (!await context.RawPowers.AnyAsync(
                        rawFrame => rawFrame.Path == file.FullName,
                        cancellationToken).ConfigureAwait(false))
                    {
                        //  Добавление файла в базу.
                        context.RawPowers.Add(new()
                        {
                            Path = file.FullName,
                            RawDirectoryId = rawDirectory.Id,
                        });

                        //  Вывод информации в журнал.
                        await Journal.LogInformationAsync(
                            $"Найден новый файл с данными о питании: {file.FullName}",
                            cancellationToken).ConfigureAwait(false);
                    }
                    break;
                default:
                    if (int.TryParse(file.Extension[1..], out int _))
                    //  Кадр регистрации.
                    {
                        //  Поиск кадра в базе.
                        if (!await context.RawFrames.AnyAsync(
                            rawFrame => rawFrame.Path == file.FullName,
                            cancellationToken).ConfigureAwait(false))
                        {
                            //  Добавление кадра регистрации в базу.
                            context.RawFrames.Add(new()
                            {
                                Path = file.FullName,
                                RawDirectoryId = rawDirectory.Id,
                            });

                            //  Вывод информации в журнал.
                            await Journal.LogInformationAsync(
                                $"Найден новый кадр: {file.FullName}",
                                cancellationToken).ConfigureAwait(false);
                        }
                    }
                    break;
            }

            //  Сохранение результатов.
            await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }, cancellationToken).ConfigureAwait(false);
    }
}
