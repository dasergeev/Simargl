using Apeiron.Platform.Databases.CentralDatabase;
using Apeiron.Platform.Databases.CentralDatabase.Entities;

namespace Apeiron.Platform.Server.Services.Microservices;

/// <summary>
/// Представляет микрослужбу, выполняющую поиск каналов кадров регистрации.
/// </summary>
public sealed class ChannelSearch :
    ServerMicroservice<ChannelSearch>
{

    /// <summary>
    /// Поле для хранения генератора случайных чисел.
    /// </summary>
    private readonly Random _Random;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="logger">
    /// Средство записи в журнал.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logger"/> передана пустая ссылка.
    /// </exception>
    public ChannelSearch(ILogger<ChannelSearch> logger) :
        base(logger)
    {
        //  Создание генератора случайных чисел.
        _Random = new(unchecked((int)(DateTime.Now.Ticks % int.MaxValue)));
    }

    /// <summary>
    /// Асинхронно выполняет шаг работы микрослужбы.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая шаг работы микрослужбы.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected override sealed async ValueTask MakeStepAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Запрос кадров с незарегистрированными каналами.
        FrameInfo[] frameInfos = await CentralDatabaseAgent.RequestAsync(
            async (session, cancellationToken) => await session.FrameInfos
                .Where(frameInfo => frameInfo.Channels.Count == 0)
                .ToArrayAsync(cancellationToken)
                .ConfigureAwait(false),
            cancellationToken).ConfigureAwait(false);

        //  Проверка количества кадров.
        if (frameInfos.Length == 0)
        {
            //  Во всех кадрах загружены каналы.
            return;
        }

        //  Изменение порядка списка кадров.
        for (int i = 0; i < frameInfos.Length; i++)
        {
            //  Получение нового индекса.
            int index = _Random.Next() % frameInfos.Length;

            //  Перестановка элементов.
            (frameInfos[i], frameInfos[index]) = (frameInfos[index], frameInfos[i]);
        }

        //  Аснихронная работа с кадрами.
        await Parallel.ForEachAsync(
            frameInfos,
            new ParallelOptions()
            {
                CancellationToken = cancellationToken,
            },
            async (frameInfo, cancellationToken) =>
            {
                //  Безопасное выполнение действия.
                await SafeCallAsync(async cancellationToken =>
                {
                    //  Проверка токена отмены.
                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                    //  Проверка количества каналов.
                    if (frameInfo.Channels.Count == 0)
                    {
                        //  Загрузка каналов.
                        await CentralDatabaseAgent.Recording
                            .LoadChannelsAsync(frameInfo, cancellationToken)
                            .ConfigureAwait(false);

                        //  Вывод информации в журнал.
                        Logger.LogInformation(
                            "Загружены каналы кадра {frameInfoId}",
                            frameInfo.Id);
                    }
                }, cancellationToken).ConfigureAwait(false);
            }).ConfigureAwait(false);
    }
}
