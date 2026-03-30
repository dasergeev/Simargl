namespace Apeiron.Platform.Performers.OrioleKmtPerformers;

/// <summary>
/// Представляет исполнителя, выполняющего нормализацию времени исходных кадров регистрации.
/// </summary>
public sealed class OrioleKmtNormalizationRawFrameTime :
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
    public OrioleKmtNormalizationRawFrameTime(Journal journal) :
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

        //  Поддержка выполнения.
        await KeepAsync(async cancellationToken =>
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Создание контекста сеанса работы с базой данных.
            using OrioleKmtDatabaseContext context = new();

            //  Начало транзакции.
            using var transaction = context.Database.BeginTransaction();

            //  Загрузка исходных файлов регистрации.
            RawFrame[] rawFrames = await context.RawFrames
                .OrderBy(rawFrame => rawFrame.Time)
                .ToArrayAsync(cancellationToken)
                .ConfigureAwait(false);

            //  Проверка количества кадров.
            if (rawFrames.Length == 0)
            {
                //  Завершение работы с каталогом.
                return;
            }

            //  Нормализация первого кадра.
            initNormalization(rawFrames[0]);

            //  Перебор кадров.
            for (int i = 1; i < rawFrames.Length; i++)
            {
                //  Определение смещения кадра.
                double offset = (rawFrames[i].Time - rawFrames[i - 1].Time).TotalSeconds;

                //  Проверка смещения.
                if (offset > 120)
                {
                    //  Начало новой последовательности кадров.
                    initNormalization(rawFrames[i]);
                }
                else
                {
                    //  Установка значений кадра.
                    rawFrames[i].IsNormalizedTime = true;
                    rawFrames[i].BeginTime = rawFrames[i - 1].BeginTime.AddSeconds(60);
                    rawFrames[i].EndTime = rawFrames[i - 1].EndTime.AddSeconds(60);
                }

                //  Вывод информации в журнал.
                await Journal.LogInformationAsync(
                    $"Нормализован кадр {rawFrames[i].FilePath}.",
                    cancellationToken).ConfigureAwait(false);
            }

            //  Сохранение изменений в базу данных.
            context.SaveChanges();

            //  Фиксирование изменений.
            transaction.Commit();

            //  Нормализация начального кадра.
            static void initNormalization(RawFrame frame)
            {
                //  Определение времени завершения записи.
                DateTime endTime = frame.Time;

                //  Нормализация времени завершения записи.
                endTime = new(endTime.Year, endTime.Month, endTime.Day, endTime.Hour, endTime.Minute, 0);

                //  Установка значений кадра.
                frame.IsNormalizedTime = true;
                frame.EndTime = endTime;
                frame.BeginTime = endTime.AddSeconds(-60);
            }

            //  Вывод информации в журнал.
            await Journal.LogInformationAsync(
                "Завершена нормализация времени исходных кадров регистрации.",
                cancellationToken).ConfigureAwait(false);
        }, cancellationToken).ConfigureAwait(false);
    }
}
