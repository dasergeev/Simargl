using Apeiron.Gps.Nmea;

namespace Apeiron.Platform.Performers.Ape90Performers;

/// <summary>
/// Представляет исполнителя, выполняющего анализ геолокационных файлов.
/// </summary>
public sealed class Ape90AnalysisGeolocationFiles :
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
    public Ape90AnalysisGeolocationFiles(Journal journal) :
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
        //  Поддержка выполнения.
        await KeepAsync(async cancellationToken =>
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Создание контекста сеанса работы с базой данных.
            using Ape90DatabaseContext context = Ape90DatabaseContextProvider.CreateContext();

            //  Запрос необработанных файлов.
            var rawGeolocations = await context.RawGeolocations
                .Where(rawGeolocation => !rawGeolocation.IsLoaded)
                .ToArrayAsync(cancellationToken);

            //  Создание генератора случайных чисел.
            Random random = new(unchecked((int)(DateTime.Now.Ticks % int.MaxValue)));

            //  Перестановка массива файлов.
            for (int i = 0; i < rawGeolocations.Length; i++)
            {
                //  Получение нового индекса.
                int index = random.Next() % rawGeolocations.Length;

                //  Перестановка файлов.
                (rawGeolocations[i], rawGeolocations[index]) = (rawGeolocations[index], rawGeolocations[i]);
            }

            //  Перебор необработанных файлов.
            await Parallel.ForEachAsync(
                rawGeolocations,
                new ParallelOptions()
                {
                    CancellationToken = cancellationToken,
                },
                async (rawGeolocation, cancellationToken) =>
                {
                    //  Безопасный вызов.
                    await SafeCallAsync(async cancellationToken =>
                    {
                        //  Проверка токена отмены.
                        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                        //  Создание контекста сеанса работы с базой данных.
                        using Ape90DatabaseContext context = Ape90DatabaseContextProvider.CreateContext();

                        //  Поиск результатов обработки в базе данных.
                        if (context.RawGeolocations
                            .Any(geolocation => geolocation.Id == rawGeolocation.Id && geolocation.IsLoaded))
                        {
                            //  Файл уже загружен.
                            return;
                        }

                        //  Начало транзакции.
                        using var transaction = context.Database.BeginTransaction();

                        //  Присоединение сущности к контексту.
                        context.Attach(rawGeolocation);

                        //  Установка флага загрузки.
                        rawGeolocation.IsLoaded = true;

                        //  Установка идексов сообщений в файле.
                        int ggaIndex = 0;
                        int vtgIndex = 0;
                        int rmcIndex = 0;

                        //  Открытие файла для чтения.
                        using StreamReader reader = new(rawGeolocation.Path);

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
                                    context.GgaMessages.Add(new()
                                    {
                                        RawGeolocationId = rawGeolocation.Id,
                                        FileTime = rawGeolocation.NameTime,
                                        Index = ggaIndex,

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
                                    context.VtgMessages.Add(new()
                                    {
                                        RawGeolocationId = rawGeolocation.Id,
                                        FileTime = rawGeolocation.NameTime,
                                        Index = vtgIndex,

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
                                    context.RmcMessages.Add(new()
                                    {
                                        RawGeolocationId = rawGeolocation.Id,
                                        FileTime = rawGeolocation.NameTime,
                                        Index = rmcIndex,

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

                        //  Сохранение изменений в базу данных.
                        context.SaveChanges();

                        //  Фиксирование изменений.
                        transaction.Commit();

                        //  Вывод информации в журнал.
                        await Journal.LogInformationAsync(
                            $"Загружены геолокационные данные из файла: {rawGeolocation.Path}",
                            cancellationToken).ConfigureAwait(false);
                    }, cancellationToken).ConfigureAwait(false);
                }).ConfigureAwait(false);
        }, cancellationToken).ConfigureAwait(false);
    }
}
