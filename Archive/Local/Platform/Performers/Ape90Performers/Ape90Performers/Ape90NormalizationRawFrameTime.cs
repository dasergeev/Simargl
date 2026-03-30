namespace Apeiron.Platform.Performers.Ape90Performers;

/// <summary>
/// Представляет исполнителя, выполняющего нормализацию времени исходных кадров регистрации.
/// </summary>
public sealed class Ape90NormalizationRawFrameTime :
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
    public Ape90NormalizationRawFrameTime(Journal journal) :
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
            using Ape90DatabaseContext context = Ape90DatabaseContextProvider.CreateContext();

            //  Загрузка информации о каталогах с исходными данными.
            IAsyncEnumerable<RawDirectory> rawDirectories = context.RawDirectories
                .AsAsyncEnumerable();

            //  Перебор каталогов с исходными данными.
            await foreach (RawDirectory rawDirectory in rawDirectories)
            {
                //  Безопасный вызов.
                await SafeCallAsync(async cancellationToken =>
                {
                    //  Проверка токена отмены.
                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                    //  Создание контекста сеанса работы с базой данных.
                    using Ape90DatabaseContext context = Ape90DatabaseContextProvider.CreateContext();

                    //  Начало транзакции.
                    using var transaction = context.Database.BeginTransaction();

                    //  Загрузка исходных файлов регистрации.
                    RawFrame[] rawFrames = await context.RawFrames
                        .Where(rawFrame => rawFrame.RawDirectoryId == rawDirectory.Id &&
                            rawDirectory.BeginTime < rawFrame.LastWriteTime &&
                            rawFrame.LastWriteTime < rawDirectory.EndTime)
                        .OrderBy(rawFrame => rawFrame.LastWriteTime)
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
                        double offset = (rawFrames[i].LastWriteTime - rawFrames[i - 1].LastWriteTime).TotalSeconds;

                        //  Проверка смещения.
                        if (offset > 1.5 * rawDirectory.FrameDuration)
                        {
                            //  Начало новой последовательности кадров.
                            initNormalization(rawFrames[i]);
                        }
                        else
                        {
                            //  Установка значений кадра.
                            rawFrames[i].IsNormalizedTime = true;
                            rawFrames[i].Duration = rawDirectory.FrameDuration;
                            rawFrames[i].BeginTime = rawFrames[i - 1].BeginTime.AddSeconds(rawDirectory.FrameDuration);
                            rawFrames[i].EndTime = rawFrames[i - 1].EndTime.AddSeconds(rawDirectory.FrameDuration);
                        }

                        //  Вывод информации в журнал.
                        await Journal.LogInformationAsync(
                            $"Нормализован кадр {rawFrames[i].Path}.",
                            cancellationToken).ConfigureAwait(false);
                    }

                    //  Сохранение изменений в базу данных.
                    context.SaveChanges();

                    //  Фиксирование изменений.
                    transaction.Commit();

                }, cancellationToken).ConfigureAwait(false);

                //  Нормализация начального кадра.
                void initNormalization(RawFrame frame)
                {
                    //  Определение времени завершения записи.
                    DateTime endTime = frame.LastWriteTime;

                    //  Нормализация времени завершения записи.
                    endTime = new(endTime.Year, endTime.Month, endTime.Day, endTime.Hour, endTime.Minute, endTime.Second);

                    //  Установка значений кадра.
                    frame.IsNormalizedTime = true;
                    frame.Duration = rawDirectory.FrameDuration;
                    frame.EndTime = endTime;
                    frame.BeginTime = endTime.AddSeconds(-rawDirectory.FrameDuration);
                }
            }

            //  Вывод информации в журнал.
            await Journal.LogInformationAsync(
                "Завершена нормализация времени исходных кадров регистрации.",
                cancellationToken).ConfigureAwait(false);
        }, cancellationToken).ConfigureAwait(false);
    }
}
