using Apeiron.Gps.Nmea;
using Apeiron.IO;
using Apeiron.Oriole.Server.Workers.Common;
using Apeiron.Platform.Databases.OrioleDatabase;
using Apeiron.Platform.Databases.OrioleDatabase.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apeiron.Oriole.Server.Workers.Search;

/// <summary>
/// Представляет фоновый процесс, выполняющий поиск NMEA-сообщений.
/// </summary>
public class NmeaMessageSearch :
    Worker<NmeaMessageSearch>
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
    public NmeaMessageSearch(ILogger<NmeaMessageSearch> logger) :
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
        NmeaFile[] request = await OrioleDatabaseManager.RequestAsync(
            async (database, cancellationToken) => await database.NmeaFiles
            .AsNoTracking()
            .Where(nmeaFile => !nmeaFile.IsLoaded)
            .Include(nmeaFile => nmeaFile.RawDirectory)
            .OrderBy(nmeaFile => nmeaFile.Time)
            .ToArrayAsync(cancellationToken),
            cancellationToken).ConfigureAwait(false);

        //  Параллельная обработка результатов запроса.
        await Parallel.ForEachAsync(
            request,
            new ParallelOptions()
            {
                CancellationToken = cancellationToken
            },
            SearchInNmeaFileAsync).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет поиск информации в файле.
    /// </summary>
    /// <param name="nmeaFile">
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
    private async ValueTask SearchInNmeaFileAsync(NmeaFile nmeaFile, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение пути к файлу.
        string path = PathBuilder.Combine(
            nmeaFile.RawDirectory.Path,
            $"{nmeaFile.Time:yyyy-MM-dd-HH}",
            "Gps",
            $"{nmeaFile.Time:yyyy-MM-dd-HH-mm-ss-fff}.txt");

        //  Установка идексов сообщений в файле.
        int ggaIndex = 0;
        int vtgIndex = 0;
        int rmcIndex = 0;

        //  Создание списков новых сообщений.
        List<GgaMessage> ggaMessages = new();
        List<VtgMessage> vtgMessages = new();
        List<RmcMessage> rmcMessages = new();

        //  Открытие файла для чтения.
        using StreamReader reader = new(path);

        //  Чтение строки.
        string? line = await reader.ReadLineAsync().ConfigureAwait(false);

        //  Цикл по всем строкам.
        while (line is not null)
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Выполнение с перехватом всех несистемных исключений.
            await Invoker.SafeNotSystemAsync(async cancellationToken =>
            {
                //  Чтение сообщения.
                Gps.Nmea.NmeaMessage message = Gps.Nmea.NmeaMessage.Parse(line);

                //  Проверка токена отмены.
                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                //  Определение формата сообщения.
                if (message is NmeaGgaMessage ggaMessage)
                {
                    //  Добавление записи.
                    ggaMessages.Add(new()
                    {
                        RawDirectoryId = nmeaFile.RawDirectoryId,
                        FileTime = nmeaFile.Time,
                        Index = ggaIndex,
                        RegistrarId = nmeaFile.RawDirectory.RegistrarId,

                        Time = ggaMessage.Time.HasValue ? ggaMessage.Time.Value.ToTimeSpan() : null,
                        Latitude = ggaMessage.Latitude,
                        Longitude = ggaMessage.Longitude,
                        Solution = ggaMessage.Solution,
                        Satellites = ggaMessage.Satellites,
                        Hdop = ggaMessage.Hdop,
                        Altitude = ggaMessage.Altitude,
                        Geoidal = ggaMessage.Geoidal,
                        Age = ggaMessage.Age,
                        Station = ggaMessage.Station,
                    });

                    //  Корректировка индекса сообщения.
                    ggaIndex++;
                }
                else if (message is NmeaVtgMessage vtgMessage)
                {
                    //  Добавление записи.
                    vtgMessages.Add(new()
                    {
                        RawDirectoryId = nmeaFile.RawDirectoryId,
                        FileTime = nmeaFile.Time,
                        Index = vtgIndex,
                        RegistrarId = nmeaFile.RawDirectory.RegistrarId,

                        PoleCourse = vtgMessage.PoleCourse,
                        MagneticCourse = vtgMessage.MagneticCourse,
                        Knots = vtgMessage.Knots,
                        Speed = vtgMessage.Speed,
                        Mode = vtgMessage.Mode,
                    });

                    //  Корректировка индекса сообщения.
                    vtgIndex++;
                }
                else if (message is NmeaRmcMessage rmcMessage)
                {
                    //  Добавление записи.
                    rmcMessages.Add(new()
                    {
                        RawDirectoryId = nmeaFile.RawDirectoryId,
                        FileTime = nmeaFile.Time,
                        Index = rmcIndex,
                        RegistrarId = nmeaFile.RawDirectory.RegistrarId,

                        Time = rmcMessage.Time.HasValue ? rmcMessage.Time.Value.ToTimeSpan() : null,
                        Valid = rmcMessage.Valid,
                        Latitude = rmcMessage.Latitude,
                        Longitude = rmcMessage.Longitude,
                        Knots = rmcMessage.Knots,
                        Speed = rmcMessage.Speed,
                        PoleCourse = rmcMessage.PoleCourse,
                        Date = rmcMessage.Date.HasValue ? rmcMessage.Date.Value.ToDateTime(new(0, 0)) : null,
                        MagneticVariation = rmcMessage.MagneticVariation,
                        MagneticCourse = rmcMessage.MagneticCourse,
                        Mode = rmcMessage.Mode,
                    });

                    //  Корректировка индекса сообщения.
                    rmcIndex++;
                }
            }, cancellationToken).ConfigureAwait(false);

            //  Чтение следующей строки.
            line = await reader.ReadLineAsync().ConfigureAwait(false);
        }

        await OrioleDatabaseManager.TransactionAsync(async (database, cancellationToken) =>
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Добавление сообщений в базу данных.
            await database.GgaMessages.AddRangeAsync(ggaMessages, cancellationToken).ConfigureAwait(false);
            await database.VtgMessages.AddRangeAsync(vtgMessages, cancellationToken).ConfigureAwait(false);
            await database.RmcMessages.AddRangeAsync(rmcMessages, cancellationToken).ConfigureAwait(false);

            //  Корректировка записи файла.
            nmeaFile.IsLoaded = true;

            //  Установка состояния изменения записи о файле.
            database.Entry(nmeaFile).State = EntityState.Modified;

            //  Обновление записи о файле.
            database.NmeaFiles.Update(nmeaFile);
        }, cancellationToken).ConfigureAwait(false);
    }
}
