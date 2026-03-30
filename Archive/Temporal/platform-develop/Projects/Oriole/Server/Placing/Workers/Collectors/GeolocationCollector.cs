using Apeiron.Oriole.Server.Workers.Common;
using Apeiron.Platform.Databases.OrioleDatabase;
using Apeiron.Platform.Databases.OrioleDatabase.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apeiron.Oriole.Server.Workers.Collectors;

/// <summary>
/// Представляет фоновый процесс, выполняющий сбор данных геолокации.
/// </summary>
public class GeolocationCollector :
    Worker<GeolocationCollector>
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
    public GeolocationCollector(ILogger<GeolocationCollector> logger) :
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
            await Task.Delay(60000, cancellationToken).ConfigureAwait(false);
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

        //  Запрос информации о регистраторах.
        int[] registrarIds = await OrioleDatabaseManager.RequestAsync(
            async (database, cancellationToken) => await database.Registrars
                .AsNoTracking()
                .Select(registrar => registrar.Id)
                .ToArrayAsync(cancellationToken),
            cancellationToken).ConfigureAwait(false);

        //  Асинхронная работа с регистратором.
        await Parallel.ForEachAsync(
            registrarIds,
            new ParallelOptions()
            {
                CancellationToken = cancellationToken,
            },
            WorkInRegistrarAsync).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет работу с регистратором.
    /// </summary>
    /// <param name="registrarId">
    /// Идентификатор регистратора.
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
    private async ValueTask WorkInRegistrarAsync(int registrarId, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Работа с сообщениями GPS, содержащими минимальный рекомендованный набор данных.
        await WorkInRmcMessagesAsync(registrarId, cancellationToken).ConfigureAwait(false);

        //  Работа с сообщениями GPS, содержащими данные местоположения.
        await WorkInGgaMessagesAsync(registrarId, cancellationToken).ConfigureAwait(false);

        //  Работа с сообщениями GPS, содержащими данные о наземном курсе и скорости.
        await WorkInVtgMessagesAsync(registrarId, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет работу с сообщениями GPS, содержащими минимальный рекомендованный набор данных.
    /// </summary>
    /// <param name="registrarId">
    /// Идентификатор регистратора.
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
    private static async Task WorkInRmcMessagesAsync(int registrarId, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Запрос сообщений для анализа.
        RmcMessage[] messages = await OrioleDatabaseManager.RequestAsync(async (database, cancellationToken) => await database
                           .RmcMessages
                           .Where(rmcMessage => !rmcMessage.IsAnalyzed && rmcMessage.RegistrarId == registrarId)
                           .OrderBy(rmcMessage => rmcMessage.FileTime)
                           .ToArrayAsync(cancellationToken).ConfigureAwait(false),
                    cancellationToken).ConfigureAwait(false);

        //  Определение количества сообщений.
        int messageCount = messages.Length;

        //  Проверка количества сообщений.
        if (messageCount == 0)
        {
            //  Нет сообщений для анализа.
            return;
        }

        //  Перебор сообщений.
        await Parallel.ForEachAsync(
            messages,
            new ParallelOptions()
            {
                CancellationToken = cancellationToken
            },
            async (message, cancellationToken) =>
            {
                //  Проверка токена отмены.
                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                //  Определение времени получения сообщения (сообщения приходят один раз за секунду).
                DateTime receiveTime = message.FileTime.AddSeconds(message.Index);

                //  Определение метки времени.
                long timestamp = Geolocation.ToTimestamp(receiveTime);

                //  Определение времени по GPS.
                DateTime? time = null;

                //  Проверка данных в сообщении.
                if (message.Time.HasValue && message.Date.HasValue)
                {
                    //  Определение времени.
                    time = (message.Date.Value + message.Time.Value).AddYears(2000).AddHours(3);
                }

                //  Обработка сообщения.
                await OrioleDatabaseManager.TryTransactionAsync(async (database, cancellationToken) =>
                {
                    //  Проверка токена отмены.
                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                    //  Запрос текущих геолокационных данных.
                    Geolocation? geolocation = await database.Geolocations.FindAsync(
                        new object?[] { registrarId, timestamp }, cancellationToken);

                    //  Проверка текущих геолокационных данных.
                    if (geolocation is not null)
                    {
                        //  Проверка необходимости корректировки времени.
                        bool isNeedTimeAligning = time.HasValue && (!geolocation.Time.HasValue ||
                            Math.Abs((receiveTime - time.Value).TotalSeconds) <
                            Math.Abs((receiveTime - geolocation.Time.Value).TotalSeconds));

                        //  Проверка необходимости обновления времени.
                        if (isNeedTimeAligning)
                        {
                            //  Установка времени.
                            geolocation.Time = time;

                            //  Данные требуют анализа.
                            geolocation.IsAnalyzed = false;
                        }

                        //  Проверка необходимости обновления широты.
                        if (isNeedTimeAligning ||
                            (!geolocation.Latitude.HasValue && message.Latitude.HasValue))
                        {
                            //  Установка широты.
                            geolocation.Latitude = message.Latitude;

                            //  Данные требуют анализа.
                            geolocation.IsAnalyzed = false;
                        }

                        //  Проверка необходимости обновления долготы.
                        if (isNeedTimeAligning ||
                            (!geolocation.Longitude.HasValue && message.Longitude.HasValue))
                        {
                            //  Установка долготы.
                            geolocation.Longitude = message.Longitude;

                            //  Данные требуют анализа.
                            geolocation.IsAnalyzed = false;
                        }

                        //  Проверка необходимости обновления скорости в километрах в час.
                        if (isNeedTimeAligning ||
                            (!geolocation.Speed.HasValue && message.Speed.HasValue))
                        {
                            //  Установка скорости в километрах в час.
                            geolocation.Speed = message.Speed;

                            //  Данные требуют анализа.
                            geolocation.IsAnalyzed = false;
                        }

                        //  Проверка необходимости обновления курса на истинный полюс.
                        if (isNeedTimeAligning ||
                            (!geolocation.PoleCourse.HasValue && message.PoleCourse.HasValue))
                        {
                            //  Установка курса на истинный полюс.
                            geolocation.PoleCourse = message.PoleCourse;

                            //  Данные требуют анализа.
                            geolocation.IsAnalyzed = false;
                        }

                        //  Проверка необходимости обновления курса на магнитный полюс.
                        if (isNeedTimeAligning ||
                            (!geolocation.MagneticCourse.HasValue && message.MagneticCourse.HasValue))
                        {
                            //  Установка курса на магнитный полюс.
                            geolocation.MagneticCourse = message.MagneticCourse;

                            //  Данные требуют анализа.
                            geolocation.IsAnalyzed = false;
                        }

                        //  Проверка необходимости обновления значения, определяющего достоверность координат.
                        if (isNeedTimeAligning ||
                            (!geolocation.Valid.HasValue && message.Valid.HasValue))
                        {
                            //  Установка значения, определяющего достоверность координат.
                            geolocation.Valid = message.Valid;

                            //  Данные требуют анализа.
                            geolocation.IsAnalyzed = false;
                        }

                        //  Проверка необходимости обновления скорости в узлах.
                        if (isNeedTimeAligning ||
                            (!geolocation.Knots.HasValue && message.Knots.HasValue))
                        {
                            //  Установка скорости в узлах.
                            geolocation.Knots = message.Knots;

                            //  Данные требуют анализа.
                            geolocation.IsAnalyzed = false;
                        }

                        //  Проверка необходимости обновления отклонения курса на магнитный полюс.
                        if (isNeedTimeAligning ||
                            (!geolocation.MagneticVariation.HasValue && message.MagneticVariation.HasValue))
                        {
                            //  Установка отклонения курса на магнитный полюс.
                            geolocation.MagneticVariation = message.MagneticVariation;

                            //  Данные требуют анализа.
                            geolocation.IsAnalyzed = false;
                        }

                        //  Проверка необходимости обновления значения, определяющего режим системы позиционирования.
                        if (isNeedTimeAligning ||
                            (!geolocation.Mode.HasValue && message.Mode.HasValue))
                        {
                            //  Установка значения, определяющего режим системы позиционирования.
                            geolocation.Mode = message.Mode;

                            //  Данные требуют анализа.
                            geolocation.IsAnalyzed = false;
                        }
                    }
                    else
                    {
                        //  Создание новых геолокационных данных.
                        database.Geolocations.Add(new()
                        {
                            RegistrarId = registrarId,
                            Timestamp = timestamp,
                            IsAnalyzed = false,
                            Time = time,
                            Latitude = message.Latitude,
                            Longitude = message.Longitude,
                            Altitude = null,
                            Speed = message.Speed,
                            PoleCourse = message.PoleCourse,
                            MagneticCourse = message.MagneticCourse,
                            Solution = null,
                            Satellites = null,
                            Hdop = null,
                            Geoidal = null,
                            Age = null,
                            Station = null,
                            Valid = message.Valid,
                            Knots = message.Knots,
                            MagneticVariation = message.MagneticVariation,
                            Mode = message.Mode,
                        });
                    }

                    //  Установка состояния изменения записи о сообщении.
                    database.Entry(message).State = EntityState.Modified;

                    //  Обновление записи о сообщении.
                    database.Update(message);

                    //  Отметка об анализе сообщения.
                    message.IsAnalyzed = true;
                }, cancellationToken).ConfigureAwait(false);
            });

        //foreach (RmcMessage message in messages)
        //{
            
        //}
    }

    /// <summary>
    /// Асинхронно выполняет работу с сообщениями GPS, содержащими данные местоположения.
    /// </summary>
    /// <param name="registrarId">
    /// Идентификатор регистратора.
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
    private static async Task WorkInGgaMessagesAsync(int registrarId, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Запрос сообщений для анализа.
        GgaMessage[] messages = await OrioleDatabaseManager.RequestAsync(async (database, cancellationToken) => await database
                .GgaMessages
                .Where(ggaMessage => !ggaMessage.IsAnalyzed && ggaMessage.RegistrarId == registrarId)
                .OrderBy(ggaMessage => ggaMessage.FileTime)
                .ToArrayAsync(cancellationToken).ConfigureAwait(false),
            cancellationToken).ConfigureAwait(false);

        //  Определение количества сообщений.
        int messageCount = messages.Length;

        //  Проверка количества сообщений.
        if (messageCount == 0)
        {
            //  Нет сообщений для анализа.
            return;
        }

        //  Перебор сообщений.
        await Parallel.ForEachAsync(
            messages,
            new ParallelOptions()
            {
                CancellationToken = cancellationToken
            },
            async (message, cancellationToken) =>
            {
                //  Проверка токена отмены.
                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                //  Определение времени получения сообщения (сообщения приходят один раз за пять секунд).
                DateTime receiveTime = message.FileTime.AddSeconds(5 * message.Index);

                //  Определение метки времени.
                long timestamp = Geolocation.ToTimestamp(receiveTime);

                //  Определение времени по GPS.
                DateTime? time = null;

                //  Проверка данных в сообщении.
                if (message.Time.HasValue)
                {
                    //  Определение времени.
                    time = (new DateTime(receiveTime.Year, receiveTime.Month, receiveTime.Day) + message.Time.Value).AddHours(3);

                    //  Разность времён в часах.
                    double dHours = (receiveTime - time.Value).TotalHours;

                    //  Проверка смещения на сутки назад.
                    if (dHours > 12)
                    {
                        //  Корректировка времени.
                        time = time.Value + TimeSpan.FromHours(24);
                    }

                    //  Проверка смещения на сутки вперёд.
                    if (dHours < -12)
                    {
                        //  Корректировка времени.
                        time = time.Value - TimeSpan.FromHours(24);
                    }
                }

                //  Обработка сообщения.
                await OrioleDatabaseManager.TryTransactionAsync(async (database, cancellationToken) =>
                {
                    //  Проверка токена отмены.
                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                    //  Запрос текущих геолокационных данных.
                    Geolocation? geolocation = await database.Geolocations.FindAsync(
                        new object?[] { registrarId, timestamp }, cancellationToken);

                    //  Проверка текущих геолокационных данных.
                    if (geolocation is not null)
                    {
                        //  Проверка необходимости корректировки времени.
                        bool isNeedTimeAligning = time.HasValue && (!geolocation.Time.HasValue ||
                            Math.Abs((receiveTime - time.Value).TotalSeconds) <
                            Math.Abs((receiveTime - geolocation.Time.Value).TotalSeconds));

                        //  Проверка необходимости обновления времени.
                        if (isNeedTimeAligning)
                        {
                            //  Установка времени.
                            geolocation.Time = time;

                            //  Данные требуют анализа.
                            geolocation.IsAnalyzed = false;
                        }

                        //  Проверка необходимости обновления широты.
                        if (isNeedTimeAligning ||
                            (!geolocation.Latitude.HasValue && message.Latitude.HasValue))
                        {
                            //  Установка широты.
                            geolocation.Latitude = message.Latitude;

                            //  Данные требуют анализа.
                            geolocation.IsAnalyzed = false;
                        }

                        //  Проверка необходимости обновления долготы.
                        if (isNeedTimeAligning ||
                            (!geolocation.Longitude.HasValue && message.Longitude.HasValue))
                        {
                            //  Установка долготы.
                            geolocation.Longitude = message.Longitude;

                            //  Данные требуют анализа.
                            geolocation.IsAnalyzed = false;
                        }

                        //  Проверка необходимости обновления высоты над средним уровнем моря в метрах.
                        if (isNeedTimeAligning ||
                            (!geolocation.Altitude.HasValue && message.Altitude.HasValue))
                        {
                            //  Установка высоты над средним уровнем моря в метрах.
                            geolocation.Altitude = message.Altitude;

                            //  Данные требуют анализа.
                            geolocation.IsAnalyzed = false;
                        }

                        //  Проверка необходимости обновления значения, определяющего способ вычисления координат.
                        if (isNeedTimeAligning ||
                            (!geolocation.Solution.HasValue && message.Solution.HasValue))
                        {
                            //  Установка значения, определяющего способ вычисления координат.
                            geolocation.Solution = message.Solution;

                            //  Данные требуют анализа.
                            geolocation.IsAnalyzed = false;
                        }

                        //  Проверка необходимости обновления количества активных спутников.
                        if (isNeedTimeAligning ||
                            (!geolocation.Satellites.HasValue && message.Satellites.HasValue))
                        {
                            //  Установка количества активных спутников.
                            geolocation.Satellites = message.Satellites;

                            //  Данные требуют анализа.
                            geolocation.IsAnalyzed = false;
                        }

                        //  Проверка необходимости обновления горизонтального снижения точности.
                        if (isNeedTimeAligning ||
                            (!geolocation.Hdop.HasValue && message.Hdop.HasValue))
                        {
                            //  Установка горизонтального снижения точности.
                            geolocation.Hdop = message.Hdop;

                            //  Данные требуют анализа.
                            geolocation.IsAnalyzed = false;
                        }

                        //  Проверка необходимости обновления отклонения геоида.
                        if (isNeedTimeAligning ||
                            (!geolocation.Geoidal.HasValue && message.Geoidal.HasValue))
                        {
                            //  Установка отклонения геоида.
                            geolocation.Geoidal = message.Geoidal;

                            //  Данные требуют анализа.
                            geolocation.IsAnalyzed = false;
                        }

                        //  Проверка необходимости обновления возраста дифференциальных поправок.
                        if (isNeedTimeAligning ||
                            (!geolocation.Age.HasValue && message.Age.HasValue))
                        {
                            //  Установка возраста дифференциальных поправок.
                            geolocation.Age = message.Age;

                            //  Данные требуют анализа.
                            geolocation.IsAnalyzed = false;
                        }

                        //  Проверка необходимости обновления идентификатора дифференциальной станции.
                        if (isNeedTimeAligning ||
                            (!geolocation.Station.HasValue && message.Station.HasValue))
                        {
                            //  Установка идентификатора дифференциальной станции.
                            geolocation.Station = message.Station;

                            //  Данные требуют анализа.
                            geolocation.IsAnalyzed = false;
                        }
                    }
                    else
                    {
                        //  Создание новых геолокационных данных.
                        database.Geolocations.Add(new()
                        {
                            RegistrarId = registrarId,
                            Timestamp = timestamp,
                            IsAnalyzed = false,
                            Time = time,
                            Latitude = message.Latitude,
                            Longitude = message.Longitude,
                            Altitude = message.Altitude,
                            Speed = null,
                            PoleCourse = null,
                            MagneticCourse = null,
                            Solution = message.Solution,
                            Satellites = message.Satellites,
                            Hdop = message.Hdop,
                            Geoidal = message.Geoidal,
                            Age = message.Age,
                            Station = message.Station,
                            Valid = null,
                            Knots = null,
                            MagneticVariation = null,
                            Mode = null,
                        });
                    }

                    //  Установка состояния изменения записи о сообщении.
                    database.Entry(message).State = EntityState.Modified;

                    //  Обновление записи о сообщении.
                    database.Update(message);

                    //  Отметка об анализе сообщения.
                    message.IsAnalyzed = true;
                }, cancellationToken).ConfigureAwait(false);
            });

        //foreach (GgaMessage message in messages)
        //{
            
        //}
    }

    /// <summary>
    /// Асинхронно выполняет работу с сообщениями GPS, содержащими данные о наземном курсе и скорости.
    /// </summary>
    /// <param name="registrarId">
    /// Идентификатор регистратора.
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
    private static async Task WorkInVtgMessagesAsync(int registrarId, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Запрос сообщений для анализа.
        VtgMessage[] messages = await OrioleDatabaseManager.RequestAsync(async (database, cancellationToken) => await database
                .VtgMessages
                .Where(vtgMessage => !vtgMessage.IsAnalyzed && vtgMessage.RegistrarId == registrarId)
                .OrderBy(vtgMessage => vtgMessage.FileTime)
                .ToArrayAsync(cancellationToken).ConfigureAwait(false),
            cancellationToken).ConfigureAwait(false);

        //  Определение количества сообщений.
        int messageCount = messages.Length;

        //  Проверка количества сообщений.
        if (messageCount == 0)
        {
            //  Нет сообщений для анализа.
            return;
        }

        //  Перебор сообщений.
        await Parallel.ForEachAsync(
            messages,
            new ParallelOptions()
            {
                CancellationToken = cancellationToken
            },
            async (message, cancellationToken) =>
            {
                //  Проверка токена отмены.
                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                //  Определение времени получения сообщения (сообщения приходят один раз за секунду).
                DateTime receiveTime = message.FileTime.AddSeconds(message.Index);

                //  Определение метки времени.
                long timestamp = Geolocation.ToTimestamp(receiveTime);

                //  Обработка сообщения.
                await OrioleDatabaseManager.TryTransactionAsync(async (database, cancellationToken) =>
                {
                    //  Проверка токена отмены.
                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                    //  Запрос текущих геолокационных данных.
                    Geolocation? geolocation = await database.Geolocations.FindAsync(
                        new object?[] { registrarId, timestamp }, cancellationToken);

                    //  Проверка текущих геолокационных данных.
                    if (geolocation is not null)
                    {
                        //  Проверка необходимости обновления скорости в километрах в час.
                        if (!geolocation.Speed.HasValue && message.Speed.HasValue)
                        {
                            //  Установка скорости в километрах в час.
                            geolocation.Speed = message.Speed;

                            //  Данные требуют анализа.
                            geolocation.IsAnalyzed = false;
                        }

                        //  Проверка необходимости обновления курса на истинный полюс в градусах.
                        if (!geolocation.PoleCourse.HasValue && message.PoleCourse.HasValue)
                        {
                            //  Установка курса на истинный полюс в градусах.
                            geolocation.PoleCourse = message.PoleCourse;

                            //  Данные требуют анализа.
                            geolocation.IsAnalyzed = false;
                        }

                        //  Проверка необходимости обновления курса на магнитный полюс в градусах.
                        if (!geolocation.MagneticCourse.HasValue && message.MagneticCourse.HasValue)
                        {
                            //  Установка курса на магнитный полюс в градусах.
                            geolocation.MagneticCourse = message.MagneticCourse;

                            //  Данные требуют анализа.
                            geolocation.IsAnalyzed = false;
                        }

                        //  Проверка необходимости обновления скорости в узлах.
                        if (!geolocation.Knots.HasValue && message.Knots.HasValue)
                        {
                            //  Установка скорости в узлах.
                            geolocation.Knots = message.Knots;

                            //  Данные требуют анализа.
                            geolocation.IsAnalyzed = false;
                        }

                        //  Проверка необходимости обновления отклонения курса на магнитный полюс.
                        if (!geolocation.MagneticVariation.HasValue && message.MagneticCourse.HasValue && message.PoleCourse.HasValue)
                        {
                            //  Установка отклонения курса на магнитный полюс.
                            geolocation.MagneticVariation = message.MagneticCourse - message.PoleCourse;

                            //  Данные требуют анализа.
                            geolocation.IsAnalyzed = false;
                        }

                        //  Проверка необходимости обновления значения, определяющего режим системы позиционирования.
                        if (!geolocation.Mode.HasValue && message.Mode.HasValue)
                        {
                            //  Установка значения, определяющего режим системы позиционирования.
                            geolocation.Mode = message.Mode;

                            //  Данные требуют анализа.
                            geolocation.IsAnalyzed = false;
                        }
                    }
                    else
                    {
                        //  Создание новых геолокационных данных.
                        database.Geolocations.Add(new()
                        {
                            RegistrarId = registrarId,
                            Timestamp = timestamp,
                            IsAnalyzed = false,
                            Time = null,
                            Latitude = null,
                            Longitude = null,
                            Altitude = null,
                            Speed = message.Speed,
                            PoleCourse = message.PoleCourse,
                            MagneticCourse = message.MagneticCourse,
                            Solution = null,
                            Satellites = null,
                            Hdop = null,
                            Geoidal = null,
                            Age = null,
                            Station = null,
                            Valid = null,
                            Knots = message.Knots,
                            MagneticVariation = message.MagneticCourse - message.PoleCourse,
                            Mode = message.Mode,
                        });
                    }

                    //  Установка состояния изменения записи о сообщении.
                    database.Entry(message).State = EntityState.Modified;

                    //  Обновление записи о сообщении.
                    database.Update(message);

                    //  Отметка об анализе сообщения.
                    message.IsAnalyzed = true;
                }, cancellationToken).ConfigureAwait(false);
            });


        //foreach (VtgMessage message in messages)
        //{
            
        //}
    }
}
