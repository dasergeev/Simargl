using Apeiron.IO;
using Apeiron.Oriole.Server.Workers.Common;
using Apeiron.Platform.Databases.OrioleDatabase;
using Apeiron.Platform.Databases.OrioleDatabase.Entities;
using Apeiron.Recording.Adxl357;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Apeiron.Oriole.Server.Workers.Search;

/// <summary>
/// Представляет фоновый процесс, выполняющий поиск пакетов с исходными данными.
/// </summary>
public class PackageSearch :
    Worker<PackageSearch>
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
    public PackageSearch(ILogger<PackageSearch> logger) :
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
            await Task.Delay(60000, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Выполняет поиск информации в базе данных.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая поиск информаци.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async Task SearchInDatabaseAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Запрос необработанных файлов.
        PackageFile[] request = await OrioleDatabaseManager.RequestAsync(
            async (database, cancellationToken) => await database.PackageFiles
            .AsNoTracking()
            .Where(packageFile => !packageFile.IsLoaded)
            .Include(packageFile => packageFile.RawDirectory)
            .OrderBy(packageFile => packageFile.Time)
            .ToArrayAsync(cancellationToken),
            cancellationToken).ConfigureAwait(false);

        //  Параллельная обработка результатов запроса.
        await Parallel.ForEachAsync(
            request,
            new ParallelOptions()
            {
                CancellationToken = cancellationToken
            },
            SearchInPackageFileAsync).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет поиск информации в файле, содержащим пакеты.
    /// </summary>
    /// <param name="packageFile">
    /// Файл, в котором необходимо выполнить поиск.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая поиск информаци.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async ValueTask SearchInPackageFileAsync(PackageFile packageFile, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        string path = string.Empty;

        try
        {
            //  Получение пути к файлу.
            path = PathBuilder.Combine(
                packageFile.RawDirectory.Path,
                $"{packageFile.Time:yyyy-MM-dd-HH}",
                $"Adxl{packageFile.Format:000}",
                $"{packageFile.Time:yyyy-MM-dd-HH-mm-ss-fff}.adxl");

            //  Чтение массива данных из файла.
            byte[] bytes = await File.ReadAllBytesAsync(path, cancellationToken);

            //  Создание потока для чтения файла.
            using MemoryStream stream = new(bytes);

            //  Создание средства чтения двоичных данных.
            Spreader spreader = new(stream, Encoding.UTF8);

            //  Текущее положение в потоке.
            int position = 0;

            //  Длина синхронных сигналов.
            int length = 0;

            //  Список пакетов.
            List<Package> packages = new();

            //  Чтение всех пакетов.
            while (position < stream.Length)
            {
                //  Проверка токена отмены.
                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                //  Чтение очередного пакета.
                Adxl357DataPackage dataPackage = await Adxl357DataPackage.LoadAsync(stream, cancellationToken);

                //  Добавление нового пакета в список.
                packages.Add(new()
                {
                    RawDirectoryId = packageFile.RawDirectoryId,
                    Format = packageFile.Format,
                    FileTime = packageFile.Time,
                    FileOffset = position,
                    Synchromarker = dataPackage.Synchromarker.Ticks,
                    Length = dataPackage.Length,
                });

                //  Корректировка длины синхронных сигналов.
                length += dataPackage.Length;

                //  Определение положения следующего пакета.
                position = (int)stream.Position;
            }

            await OrioleDatabaseManager.TransactionAsync(async (database, cancellationToken) =>
            {
                //  Проверка токена отмены.
                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                //  Добавление новых пакетов.
                await database.Packages.AddRangeAsync(packages, cancellationToken);

                //  Корректировка записи файла.
                packageFile.IsLoaded = true;
                packageFile.PackageCount = packages.Count;
                packageFile.Length = length;

                //  Установка состояния изменения записи о файле.
                database.Entry(packageFile).State = EntityState.Modified;

                //  Обновление записи о файле.
                database.PackageFiles.Update(packageFile);
            }, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Logger.LogError("({path}){message}", path, ex);
        }
    }
}
