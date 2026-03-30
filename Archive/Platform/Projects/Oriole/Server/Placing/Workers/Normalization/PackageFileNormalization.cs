using Apeiron.Oriole.Server.Processing;
using Apeiron.Oriole.Server.Workers.Common;
using Apeiron.Platform.Databases.OrioleDatabase;
using Apeiron.Platform.Databases.OrioleDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace Apeiron.Oriole.Server.Workers.Normalization;

/// <summary>
/// Представляет фоновый процесс, выполняющий нормализацию файлов с исходными данными.
/// </summary>
public class PackageFileNormalization :
    Worker<PackageFileNormalization>
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
    public PackageFileNormalization(ILogger<PackageFileNormalization> logger) :
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
            //  Выполнение работы с базой данных.
            await WorkDatabaseAsync(cancellationToken);

            //  Ожидание перед следующим поиском.
            await Task.Delay(600000, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет работу с базой данных.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async Task WorkDatabaseAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Запрос источников данных из базы данных.
        Source[] sources = await OrioleDatabaseManager.RequestAsync(async (database, cancellationToken) =>
            await database.Sources
                .Include(source => source.Channel)
                .ToArrayAsync(cancellationToken)
                .ConfigureAwait(false),
        cancellationToken).ConfigureAwait(false);

        //  Асинхроная работа с каждым каналом.
        await Parallel.ForEachAsync(
            sources,
            new ParallelOptions
            {
                CancellationToken = cancellationToken,
            },
            WorkSourceAsync).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет работу с источником данных.
    /// </summary>
    /// <param name="source">
    /// Источник данных, с которым необходимо выполнить работу.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async ValueTask WorkSourceAsync(Source source, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение заявленной частоты дискретизации.
        double declaredSampling = source.Sampling;

        //  Корректировка заявленной частоты дискретизации.
        declaredSampling = declaredSampling switch
        {
            125 => 125.047,
            1000 => 1000.38,
            _ => declaredSampling,
        };

        //  Получение информации для нормализации файлов с пакетами данных.
        PackageFileNormalizationInfo[] normalizationInfos = await PackageFileNormalizationInfo.LoadInfosAsync(
            source.Channel.RegistrarId, source.Format, source.BeginTime, source.EndTime, declaredSampling,
            cancellationToken).ConfigureAwait(false);

        //  Определение количества файлов.
        int fileCount = normalizationInfos.Length;

        //  Проверка количества файлов.
        if (fileCount == 0)
        {
            //  Нет файлов для нормализации.
            return;
        }

        //  Нормализация файлов.
        foreach (PackageFileNormalizationInfo normalizationInfo in normalizationInfos)
        {
            //  Нормализация файла.
            await normalizationInfo.NormalizationAsync(cancellationToken).ConfigureAwait(false);
        }

        //  Определение файлов, требующих обновления.
        PackageFile[] files = normalizationInfos
            .Where(normalizationInfo => !normalizationInfo.PackageFile.IsNormalized)
            .Select(normalizationInfo => normalizationInfo.PackageFile)
            .ToArray();

        //  Определение количества файлов.
        fileCount = files.Length;

        //  Проверка количества файлов, требующих обновления.
        if (fileCount > 0)
        {
            //  Асинхронное добавление информации о новых файлах в базу данных.
            await Parallel.ForEachAsync(
                Partitioner.Create(0, fileCount, Math.Min(fileCount, 1000)).GetDynamicPartitions(),
                new ParallelOptions()
                {
                    CancellationToken = cancellationToken,
                },
                async (range, cancellationToken) =>
                {
                    try
                    {
                        await OrioleDatabaseManager.TransactionAsync(async (database, cancellationToken) =>
                        {
                            //  Проверка токена отмены.
                            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                            //  Перебор обновляемых файлов.
                            for (int i = range.Item1; i < range.Item2; i++)
                            {
                                //  

                                //  Обновляемый файл.
                                PackageFile packageFile = files[i];

                                //  Установка состояния изменения записи о файле.
                                database.Entry(packageFile).State = EntityState.Modified;

                                //  Установка флага нормализации.
                                packageFile.IsNormalized = true;

                                //  Загрузка пакетов.
                                database.Entry(packageFile)
                                    .Collection(packageFile => packageFile.Packages)
                                    .Load();

                                //  Перебор загруженных пакетов.
                                foreach (Package package in packageFile.Packages)
                                {
                                    //  Сброс флага анализа.
                                    package.IsAnalyzed = false;
                                }

                                //  Обновление записи о файле.
                                database.PackageFiles.Update(packageFile);
                            }
                        }, cancellationToken).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError("{excaption", ex);
                    }
                }).ConfigureAwait(false);
        }
    }




    ///// <summary>
    ///// Асинхронно выполняет работу с источником данных.
    ///// </summary>
    ///// <param name="source">
    ///// Источник данных, с которым необходимо выполнить работу.
    ///// </param>
    ///// <param name="cancellationToken">
    ///// Токен отмены.
    ///// </param>
    ///// <returns>
    ///// Задача, выполняющая работу.
    ///// </returns>
    ///// <exception cref="OperationCanceledException">
    ///// Операция отменена.
    ///// </exception>
    //private async ValueTask WorkSourceAsync(Source source, CancellationToken cancellationToken)
    //{
    //    //  Проверка токена отмены.
    //    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

    //    //  Получение заявленной частоты дискретизации.
    //    double declaredSampling = source.Sampling;

    //    //  Корректировка заявленной частоты дискретизации.
    //    declaredSampling = declaredSampling switch
    //    {
    //        125 => 125.047,
    //        1000 => 1000.38,
    //        _ => declaredSampling,
    //    };

    //    //  Запрос файлов, связанных с источником.
    //    PackageFile[] files = await Balancer.RequestAsync(async (database, cancellationToken) =>
    //        await database.PackageFiles
    //            .Where(packageFile => packageFile.IsLoaded &&
    //                packageFile.RawDirectory.RegistrarId == source.Channel.RegistrarId &&
    //                packageFile.Format == source.Format &&
    //                source.BeginTime <= packageFile.Time &&
    //                packageFile.Time < source.EndTime)
    //            .OrderBy(packageFile => packageFile.Time)
    //            .ToArrayAsync(cancellationToken)
    //            .ConfigureAwait(false),
    //            cancellationToken).ConfigureAwait(false);

    //    //  Определение количества файлов.
    //    int fileCount = files.Length;

    //    //  Проверка количества файлов.
    //    if (fileCount == 0)
    //    {
    //        //  Нет файлов для нормализации.
    //        return;
    //    }

    //    //  Предыдущий пакет.
    //    Package? previousPackage = null;

    //    //  Индекс начала последовательности файлов.
    //    int beginFileIndex = 0;

    //    //  Перебор файлов, связанных с источником.
    //    for (int fileIndex = 0; fileIndex < fileCount; fileIndex++)
    //    {
    //        //  Получение текущего файла.
    //        PackageFile file = files[fileIndex];

    //        //  Установка индекса пакета в последовательности.
    //        file.SequentialIndex = fileIndex - beginFileIndex;

    //        //  Загрузка пакетов.
    //        Package[] packages = await Balancer.RequestAsync(async (database, cancellationToken) =>
    //            await database.Packages
    //            .Where(package => package.RawDirectoryId == file.RawDirectoryId &&
    //                package.Format == file.Format &&
    //                package.FileTime == file.Time)
    //            .OrderBy(package => package.FileOffset)
    //            .ToArrayAsync(cancellationToken)
    //            .ConfigureAwait(false),
    //            cancellationToken).ConfigureAwait(false);

    //        //  Определение количества пакетов.
    //        int packageCount = packages.Length;

    //        //  Проверка количества пакетов.
    //        if (packageCount == 0)
    //        {
    //            //  Нормализация файлов.
    //            normalization(beginFileIndex, fileIndex);

    //            //  Сброс текущей последовательности.
    //            reset(file, fileIndex);

    //            //  Переход к следующему файлу.
    //            continue;
    //        }

    //        //  Установка синхромаркеров файла.
    //        file.FirstSynchromarker = packages[0].Synchromarker;
    //        file.LastSynchromarker = packages[packageCount - 1].Synchromarker;

    //        //  Предварительная установка корректности последовательности синхромаркеров.
    //        file.IsCorrectSynchromarkersChain = true;

    //        //  Перебор пакетов.
    //        for (int packageIndex = 0; packageIndex < packageCount; packageIndex++)
    //        {
    //            //  Получение текущего пакета.
    //            Package package = packages[packageIndex];

    //            //  Проверка ссылки на предыдущий пакет.
    //            if (previousPackage is not null)
    //            {
    //                //  Проверка обрыва последовательности пакетов.
    //                if (!IsPackagesChain(previousPackage, package, declaredSampling))
    //                {
    //                    //  Сброс индекса пакета в последовательности.
    //                    file.SequentialIndex = 0;

    //                    //  Нормализация файлов.
    //                    normalization(beginFileIndex, fileIndex);

    //                    //  Проверка начала файла.
    //                    if (packageIndex == 0)
    //                    {
    //                        //  Установка начального индекса следующей последовательности файлов.
    //                        beginFileIndex = fileIndex;
    //                    }
    //                    else
    //                    {
    //                        //  Сброс текущей последовательности.
    //                        reset(file, fileIndex);

    //                        //  Переход к следующему файлу.
    //                        break;
    //                    }
    //                }
    //            }

    //            //  Установка предыдущего пакета.
    //            previousPackage = package;
    //        }
    //    }

    //    //  Нормализация последней последовательности.
    //    normalization(beginFileIndex, fileCount);

    //    //  Сброс текущей последовательности.
    //    void reset(PackageFile file, int fileIndex)
    //    {
    //        //  Некорректная последовательность синхромаркеров.
    //        file.FirstSynchromarker = 0;
    //        file.LastSynchromarker = 0;
    //        file.IsCorrectSynchromarkersChain = false;

    //        //  Нормализация текущего файла.
    //        normalization(fileIndex, fileIndex + 1);

    //        //  Установка начального индекса следующей последовательности файлов.
    //        beginFileIndex = fileIndex + 1;

    //        //  Сброс предыдущего пакета.
    //        previousPackage = null;
    //    }

    //    //  Выполняет нормализацию последовательности.
    //    void normalization(int beginIndex, int endIndex)
    //    {
    //        //  Определение количества файлов в последовательности.
    //        int count = endIndex - beginIndex;

    //        //  Проверка пустой последовательности.
    //        if (count == 0)
    //        {
    //            //  Пустая последовательность.
    //            return;
    //        }

    //        //  Проверка количества файлов.
    //        if (count == 1)
    //        {
    //            //  Получение файла.
    //            PackageFile packageFile = files[beginIndex];

    //            //  Определение текущей длительности файла.
    //            double duration = TimeSpan.FromSeconds(packageFile.Length / declaredSampling).TotalSeconds;

    //            //  Проверка изменения свойств файла.
    //            packageFile.IsNormalized = packageFile.NormalizedTime == packageFile.Time &&
    //                packageFile.Sampling == declaredSampling && packageFile.Duration == duration;

    //            //  Установка свойств файла.
    //            packageFile.NormalizedTime = packageFile.Time;
    //            packageFile.Sampling = declaredSampling;
    //            packageFile.Duration = duration;
    //        }
    //        else
    //        {
    //            //  Длина последовательности.
    //            int length = 0;

    //            //  Перебор файлов в последовательности.
    //            for (int i = beginIndex; i < endIndex; i++)
    //            {
    //                //  Корректировка длины последовательности.
    //                length += files[i].Length;
    //            }

    //            //  Определение времени начала последовательности.
    //            DateTime normalizedTime = files[beginIndex].Time;

    //            //  Определение длительности последовательности.
    //            double duration = (files[endIndex - 1].Time - normalizedTime
    //                + TimeSpan.FromSeconds(files[endIndex - 1].Length / declaredSampling)).TotalSeconds;

    //            //  Проверка возможности корректировки по второму файлу.
    //            if (count > 1)
    //            {
    //                //  Корректировка длины последовательности.
    //                length -= files[beginIndex].Length;

    //                //  Определение корректирующего смещения.
    //                var t = files[beginIndex + 1].Time - normalizedTime;

    //                //  Корректировка длительности последовательности.
    //                duration -= (files[beginIndex + 1].Time - normalizedTime).TotalSeconds;
    //            }

    //            //  Определение фактической частоты дискретизации.
    //            double actualSampling = length / duration;

    //            //  Проверка возможности корректировки по второму файлу.
    //            if (count > 1)
    //            {
    //                //  Корректировка времени начала последовательности.
    //                normalizedTime = files[beginIndex + 1].Time.AddSeconds(-files[beginIndex].Length / actualSampling);
    //            }

    //            //  Перебор файлов в последовательности.
    //            for (int i = beginIndex; i < endIndex; i++)
    //            {
    //                //  Получение файла.
    //                PackageFile packageFile = files[i];

    //                //  Определение текущей длительности файла.
    //                duration = packageFile.Length / actualSampling;

    //                //  Проверка изменения свойств файла.
    //                packageFile.IsNormalized = packageFile.NormalizedTime == normalizedTime &&
    //                    packageFile.Sampling == actualSampling && packageFile.Duration == duration;

    //                //  Установка свойств файла.
    //                packageFile.NormalizedTime = normalizedTime;
    //                packageFile.Sampling = actualSampling;
    //                packageFile.Duration = duration;

    //                //  Смещение времени.
    //                normalizedTime = normalizedTime.AddSeconds(duration);
    //            }
    //        }
    //    }

    //    //  Определение файлов, требующих обновления.
    //    files = files
    //        .Where(packageFile => !packageFile.IsNormalized)
    //        .ToArray();

    //    //  Определение количества файлов.
    //    fileCount = files.Length;

    //    //  Проверка количества файлов, требующих обновления.
    //    if (fileCount > 0)
    //    {
    //        //  Асинхронное добавление информации о новых файлах в базу данных.
    //        await Parallel.ForEachAsync(
    //            Partitioner.Create(0, fileCount, Math.Min(fileCount - 1, 1000)).GetDynamicPartitions(),
    //            new ParallelOptions()
    //            {
    //                CancellationToken = cancellationToken,
    //            },
    //            async (range, cancellationToken) =>
    //            {
    //                await Balancer.TransactionAsync(async (database, cancellationToken) =>
    //                {
    //                    //  Проверка токена отмены.
    //                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

    //                    //  Перебор овновляемых файлов.
    //                    for (int i = range.Item1; i < range.Item2; i++)
    //                    {
    //                        //  Обновляемый файл.
    //                        PackageFile packageFile = files[i];

    //                        //  Установка состояния изменения записи о файле.
    //                        database.Entry(packageFile).State = EntityState.Modified;

    //                        //  Установка флага нормализации.
    //                        packageFile.IsNormalized = true;

    //                        //  Загрузка пакетов.
    //                        database.Entry(packageFile)
    //                            .Collection(packageFile => packageFile.Packages)
    //                            .Load();

    //                        //  Перебор загруженных пакетов.
    //                        foreach (Package package in packageFile.Packages)
    //                        {
    //                            //  Сброс флага анализа.
    //                            package.IsAnalyzed = false;
    //                        }

    //                        //  Обновление записи о файле.
    //                        database.PackageFiles.Update(packageFile);
    //                    }
    //                }, cancellationToken).ConfigureAwait(false);
    //            }).ConfigureAwait(false);
    //    }
    //}
}
