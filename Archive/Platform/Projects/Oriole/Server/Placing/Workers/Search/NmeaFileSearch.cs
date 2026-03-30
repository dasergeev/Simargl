using Apeiron.IO;
using Apeiron.Oriole.Server.Workers.Common;
using Apeiron.Platform.Databases.OrioleDatabase;
using Apeiron.Platform.Databases.OrioleDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace Apeiron.Oriole.Server.Workers.Search;

/// <summary>
/// Представляет фоновый процесс, выполняющий поиск файлов с NMEA данными.
/// </summary>
public class NmeaFileSearch :
    Worker<NmeaFileSearch>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="logger">
    /// Средство записи в журнал службы.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logger"/> передана пустая ссылка.
    /// </exception>
    public NmeaFileSearch(ILogger<NmeaFileSearch> logger) :
        base(logger)
    {

    }

    /// <summary>
    /// Асинхронно выполняет фоновую работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая фоновую работу.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected override async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Основной цикл работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Поиск в базе данных.
            await SearchInDatabaseAsync(cancellationToken);

            //  Ожидание перед следующим поиском.
            await Task.Delay(300000, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет поиск информации в базе данных.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая поиск.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async Task SearchInDatabaseAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Запрос информации о каталогах необработанных данных.
        RawDirectory[] rawDirectories = await OrioleDatabaseManager.RequestAsync(
            async (database, cancellationToken) => await database.RawDirectories
                .AsNoTracking()
                .Include(rawDirectory => rawDirectory.Registrar)
                .Include(rawDirectories => rawDirectories.NmeaFiles)
                .ToArrayAsync(cancellationToken),
            cancellationToken).ConfigureAwait(false);

        //  Асинхронная работа с каталогом необработанных данных.
        await Parallel.ForEachAsync(
            rawDirectories,
            new ParallelOptions()
            {
                CancellationToken = cancellationToken,
            },
            SearchInRawDirectoryAsync).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет поиск информации по каталогу необработанных данных.
    /// </summary>
    /// <param name="rawDirectory">
    /// Каталог необработанных данных.
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
    private async ValueTask SearchInRawDirectoryAsync(RawDirectory rawDirectory, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание списка файлов в базе данных.
        IEnumerable<string> filesInDatabase = rawDirectory.NmeaFiles
            .Select(packageFile => PathBuilder.Combine(
                packageFile.RawDirectory.Path,
                $"{packageFile.Time:yyyy-MM-dd-HH}",
                "Gps",
                $"{packageFile.Time:yyyy-MM-dd-HH-mm-ss-fff}.txt"));

        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Загрузка всех фалов в файловой системе.
        IEnumerable<string> filesInFileSystem = await FindAllFilesAsync(
            rawDirectory.Path, cancellationToken).ConfigureAwait(false);

        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение списка файлов, которые необходимо добавить в базу данных.
        NmeaFile[] nmeaFiles = filesInFileSystem.Except(filesInDatabase)
            .Select(path => new FileInfo(path))
            .Select(fileInfo => new
            {
                IsValid = RawDirectory.TryParseFileTime(fileInfo.Name, out DateTime time),
                Time = time,
            })
            .Where(entity => entity.IsValid)
            .Select(entity => new NmeaFile()
            {
                RawDirectoryId = rawDirectory.Id,
                Time = entity.Time,
            })
            .ToArray();

        //  Определение количества файлов.
        int fileCount = nmeaFiles.Length;

        if (fileCount > 0)
        {
            //  Асинхронное добавление информации о новых файлах в базу данных.
            await Parallel.ForEachAsync(
                Partitioner.Create(0, fileCount, Math.Min(fileCount - 1, 1000)).GetDynamicPartitions(),
                new ParallelOptions()
                {
                    CancellationToken = cancellationToken,
                },
                async (range, cancellationToken) =>
                {
                    await OrioleDatabaseManager.TransactionAsync(async (database, cancellationToken) =>
                    {
                        //  Проверка токена отмены.
                        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                        //  Количество добавляемых файлов.
                        int count = range.Item2 - range.Item1;

                        //  Массив части новых файлов.
                        NmeaFile[] partialNmeaFiles = new NmeaFile[count];

                        //  Копирование части новых файлов.
                        Array.Copy(nmeaFiles, range.Item1, partialNmeaFiles, 0, count);

                        //  Добавление новых файлов.
                        await database.NmeaFiles.AddRangeAsync(partialNmeaFiles, cancellationToken);
                    }, cancellationToken).ConfigureAwait(false);
                }).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно находит все файлы в файловой системе.
    /// </summary>
    /// <param name="path">
    /// Путь для поиска.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая поиск файлов в файловой системе.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async Task<IEnumerable<string>> FindAllFilesAsync(string path, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Коллекция путей к файлам.
        ConcurrentBag<string> allFiles = new();

        //  Получение информации о корневом каталоге.
        DirectoryInfo rootDirectory = new(path);

        //  Получение массива подкаталогов.
        DirectoryInfo[] directories = rootDirectory.GetDirectories();

        //  Перебор каталогов.
        await Parallel.ForEachAsync(
            directories,
            new ParallelOptions
            {
                CancellationToken= cancellationToken,
            },
            async (directory, cancellationToken) =>
            {
                //  Безопасная работа с каталогом.
                await SafeCallAsync(async cancellationToken =>
                {
                    //  Проверка токена отмены.
                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                    //  Вывод информации в журнал.
                    Logger.LogInformation("Поиск в каталоге {path}", directory.FullName);

                    //  Получение файлов из каталога.
                    IEnumerable<string> files = directory
                        .GetFiles("*.txt", SearchOption.AllDirectories)
                        .Select(file => PathBuilder.Normalize(file.FullName));

                    //  Перебор найденных файлов.
                    foreach (string file in files)
                    {
                        //  Добавление файла в коллекцию.
                        allFiles.Add(file);
                    }
                }, cancellationToken).ConfigureAwait(false);
            }).ConfigureAwait(false);

        //  Нормализация и возврат путей к файлам.
        return allFiles;
    }
}
