namespace Apeiron.Platform.Performers.Ape90Performers;

/// <summary>
/// Представляет исполнителя, выполняющего сбор данных геолокации.
/// </summary>
public sealed class Ape90GeolocationCollector :
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
    public Ape90GeolocationCollector(Journal journal) :
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

        //  Вывод информации в журнал.
        await Journal.LogInformationAsync("Ape90GeolocationCollector Run", cancellationToken).ConfigureAwait(false);

        //  Поддержка выполнения.
        await KeepAsync(async cancellationToken =>
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Работа с сообщениями GPS, содержащими минимальный рекомендованный набор данных.
            await WorkInRmcMessagesAsync(cancellationToken).ConfigureAwait(false);

            //  Работа с сообщениями GPS, содержащими данные местоположения.
            await WorkInGgaMessagesAsync(cancellationToken).ConfigureAwait(false);

            //  Работа с сообщениями GPS, содержащими данные о наземном курсе и скорости.
            await WorkInVtgMessagesAsync(cancellationToken).ConfigureAwait(false);
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет работу с сообщениями GPS, содержащими минимальный рекомендованный набор данных.
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
    private async Task WorkInRmcMessagesAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Безопасный вызов.
        await SafeCallAsync(async cancellationToken =>
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Создание контекста сеанса работы с базой данных.
            using Ape90DatabaseContext context = Ape90DatabaseContextProvider.CreateContext();

            //  Запрос сообщений для анализа.
            RmcMessage[] messages = await context
                .RmcMessages
                .Where(rmcMessage => !rmcMessage.IsAnalyzed)
                .OrderBy(rmcMessage => rmcMessage.FileTime)
                .ToArrayAsync(cancellationToken).ConfigureAwait(false);

            //  Проверка количества сообщений.
            if (messages.Length == 0)
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
                    //  Безопасный вызов.
                    await SafeCallAsync(async cancellationToken =>
                    {
                        //  Проверка токена отмены.
                        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                        //  Определение времени получения сообщения (сообщения приходят один раз за секунду).
                        DateTime receiveTime = message.FileTime.AddSeconds(message.Index);

                        //  Определение времени по GPS.
                        DateTime? time = null;

                        //  Проверка данных в сообщении.
                        if (message.Time.HasValue && message.Date.HasValue)
                        {
                            //  Определение времени.
                            time = (message.Date.Value + message.Time.Value).AddYears(2000).AddHours(3);
                        }

                        //  Создание контекста сеанса работы с базой данных для транзакции.
                        using Ape90DatabaseContext context = Ape90DatabaseContextProvider.CreateContext();

                        //  Начало транзакции.
                        using var transaction = context.Database.BeginTransaction();

                        //  Запрос текущих геолокационных данных.
                        Geolocation? geolocation = await context.Geolocations.FirstOrDefaultAsync(
                                ggeolocation => ggeolocation.RecTime == receiveTime,
                                cancellationToken);

                        //  Проверка текущих геолокационных данных.
                        if (geolocation is not null)
                        {
                            //  Проверка необходимости корректировки времени.
                            bool isNeedTimeAligning = time.HasValue && (!geolocation.GpsTime.HasValue ||
                                    Math.Abs((receiveTime - time.Value).TotalSeconds) <
                                    Math.Abs((receiveTime - geolocation.GpsTime.Value).TotalSeconds));

                            //  Проверка необходимости обновления времени.
                            if (isNeedTimeAligning)
                            {
                                //  Установка времени.
                                geolocation.GpsTime = time;

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
                            context.Geolocations.Add(new()
                            {
                                RecTime = receiveTime,
                                IsAnalyzed = false,
                                GpsTime = time,
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

                        //  Обновление записи о сообщении.
                        message = context.RmcMessages.First(e => e.Id == message.Id);

                        //  Отметка об анализе сообщения.
                        message.IsAnalyzed = true;

                        //  Сохранение изменений в базу данных.
                        context.SaveChanges();

                        //  Фиксирование изменений.
                        transaction.Commit();
                    }, cancellationToken).ConfigureAwait(false);
                });
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет работу с сообщениями GPS, содержащими данные местоположения.
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
    private async Task WorkInGgaMessagesAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Безопасный вызов.
        await SafeCallAsync(async cancellationToken =>
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Создание контекста сеанса работы с базой данных для транзакции.
            using Ape90DatabaseContext context = Ape90DatabaseContextProvider.CreateContext();

            //  Запрос сообщений для анализа.
            GgaMessage[] messages = await context
                    .GgaMessages
                    .Where(ggaMessage => !ggaMessage.IsAnalyzed)
                    .OrderBy(ggaMessage => ggaMessage.FileTime)
                    .ToArrayAsync(cancellationToken).ConfigureAwait(false);

            //  Проверка количества сообщений.
            if (messages.Length == 0)
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
                    //  Безопасный вызов.
                    await SafeCallAsync(async cancellationToken =>
                    {
                        //  Проверка токена отмены.
                        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                        //  Определение времени получения сообщения (сообщения приходят один раз за пять секунд).
                        DateTime receiveTime = message.FileTime.AddSeconds(5 * message.Index);

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

                        //  Создание контекста сеанса работы с базой данных для транзакции.
                        using Ape90DatabaseContext context = Ape90DatabaseContextProvider.CreateContext();

                        //  Начало транзакции.
                        using var transaction = context.Database.BeginTransaction();

                        //  Запрос текущих геолокационных данных.
                        Geolocation? geolocation = await context.Geolocations.FirstOrDefaultAsync(
                                geolocation => geolocation.RecTime == receiveTime, cancellationToken);

                        //  Проверка текущих геолокационных данных.
                        if (geolocation is not null)
                        {
                            //  Проверка необходимости корректировки времени.
                            bool isNeedTimeAligning = time.HasValue && (!geolocation.GpsTime.HasValue ||
                                    Math.Abs((receiveTime - time.Value).TotalSeconds) <
                                    Math.Abs((receiveTime - geolocation.GpsTime.Value).TotalSeconds));

                            //  Проверка необходимости обновления времени.
                            if (isNeedTimeAligning)
                            {
                                //  Установка времени.
                                geolocation.GpsTime = time;

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
                            context.Geolocations.Add(new()
                            {
                                RecTime = receiveTime,
                                IsAnalyzed = false,
                                GpsTime = time,
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

                        //  Обновление записи о сообщении.
                        message = context.GgaMessages.First(e => e.Id == message.Id);

                        //  Отметка об анализе сообщения.
                        message.IsAnalyzed = true;

                        //  Сохранение изменений в базу данных.
                        context.SaveChanges();

                        //  Фиксирование изменений.
                        transaction.Commit();
                    }, cancellationToken).ConfigureAwait(false);
                });
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет работу с сообщениями GPS, содержащими данные о наземном курсе и скорости.
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
    private async Task WorkInVtgMessagesAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Безопасный вызов.
        await SafeCallAsync(async cancellationToken =>
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Создание контекста сеанса работы с базой данных для транзакции.
            using Ape90DatabaseContext context = Ape90DatabaseContextProvider.CreateContext();

            //  Запрос сообщений для анализа.
            VtgMessage[] messages = await context
                    .VtgMessages
                    .Where(vtgMessage => !vtgMessage.IsAnalyzed)
                    .OrderBy(vtgMessage => vtgMessage.FileTime)
                    .ToArrayAsync(cancellationToken).ConfigureAwait(false);

            //  Проверка количества сообщений.
            if (messages.Length == 0)
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
                    //  Безопасный вызов.
                    await SafeCallAsync(async cancellationToken =>
                    {
                        //  Проверка токена отмены.
                        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                        //  Определение времени получения сообщения (сообщения приходят один раз за секунду).
                        DateTime receiveTime = message.FileTime.AddSeconds(message.Index);

                        //  Создание контекста сеанса работы с базой данных для транзакции.
                        using Ape90DatabaseContext context = Ape90DatabaseContextProvider.CreateContext();

                        //  Начало транзакции.
                        using var transaction = context.Database.BeginTransaction();

                        //  Запрос текущих геолокационных данных.
                        Geolocation? geolocation = await context.Geolocations.FirstOrDefaultAsync(
                                geolocation => geolocation.RecTime == receiveTime, cancellationToken);

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
                            context.Geolocations.Add(new()
                            {
                                RecTime = receiveTime,
                                IsAnalyzed = false,
                                GpsTime = null,
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


                        //  Обновление записи о сообщении.
                        message = context.VtgMessages.First(e => e.Id == message.Id);

                        //  Отметка об анализе сообщения.
                        message.IsAnalyzed = true;

                        //  Сохранение изменений в базу данных.
                        context.SaveChanges();

                        //  Фиксирование изменений.
                        transaction.Commit();
                    }, cancellationToken).ConfigureAwait(false);
                });

        }, cancellationToken).ConfigureAwait(false);
    }
}
