using Apeiron.Analysis.Transforms;
using Apeiron.Frames;

namespace Apeiron.Platform.Databases.CentralDatabase;

public partial class CentralDatabaseAgent
{
    /// <summary>
    /// Предоставляет доступ к элементам обработки.
    /// </summary>
    public static class Recording
    {
        /// <summary>
        /// Асинхронно загружает кадр регистрации.
        /// </summary>
        /// <param name="frameInfo">
        /// Информация о кадре.
        /// </param>
        /// <param name="cancellationToken">
        /// Токен отмены.
        /// </param>
        /// <returns>
        /// Задача, загружающая кадр регистрации.
        /// </returns>
        /// <exception cref="OperationCanceledException">
        /// Операция отменена.
        /// </exception>
        public static async Task<Frame> LoadFrameAsync(FrameInfo frameInfo, CancellationToken cancellationToken)
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Перебор путей к файлу.
            foreach (string path in frameInfo.File.GetAbsolutePaths())
            {
                //  Проверка существования файла.
                if (File.Exists(path))
                {
                    //  Загрузка кадра регистрации.
                    return new(path);
                }
            }

            //  Генерация исключения.
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Асинхронно регистрирует имя канала.
        /// </summary>
        /// <param name="name">
        /// Регистрируемое имя.
        /// </param>
        /// <param name="cancellationToken">
        /// Токен отмены.
        /// </param>
        /// <returns>
        /// Задача, выполняющая регистрацию имени канала.
        /// </returns>
        /// <exception cref="OperationCanceledException">
        /// Операция отменена.
        /// </exception>
        public static async Task<ChannelName> ChannelNameRegistrationAsync(
            string name, CancellationToken cancellationToken)
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Возврат имени канала.
            return await RetrievalAsync(
                channelName => channelName.Name == name,
                () => new ChannelName(name),
                cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Асинхронно регистрирует единицу измерения канала.
        /// </summary>
        /// <param name="name">
        /// Регистрируемое имя.
        /// </param>
        /// <param name="cancellationToken">
        /// Токен отмены.
        /// </param>
        /// <returns>
        /// Задача, выполняющая регистрацию единицу измерения канала.
        /// </returns>
        /// <exception cref="OperationCanceledException">
        /// Операция отменена.
        /// </exception>
        public static async Task<ChannelUnit> ChannelUnitRegistrationAsync(
            string name, CancellationToken cancellationToken)
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Возврат единицы измерения.
            return await RetrievalAsync(
                channelUnit => channelUnit.Name == name,
                () => new ChannelUnit(name),
                cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Асинхронно загружает каналы кадра регистрации.
        /// </summary>
        /// <param name="frameInfo">
        /// Информация о кадре.
        /// </param>
        /// <param name="cancellationToken">
        /// Токен отмены.
        /// </param>
        /// <returns>
        /// Задача, загружающая каналы кадра регистрации.
        /// </returns>
        /// <exception cref="OperationCanceledException">
        /// Операция отменена.
        /// </exception>
        public static async Task LoadChannelsAsync(FrameInfo frameInfo, CancellationToken cancellationToken)
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Загрузка кадра.
            Frame frame = await LoadFrameAsync(frameInfo, cancellationToken).ConfigureAwait(false);

            //  Выполнение транзакции.
            await TransactionAsync(
                async (session, cancellationToken) =>
                {
                    //  Проверка токена отмены.
                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                    //  Перебор каналов.
                    for (int i = 0; i < frame.Channels.Count; i++)
                    {
                        //  Получение канала.
                        Channel channel = frame.Channels[i];

                        //  Добавление информации о канале.
                        await session.ChannelInfos.AddAsync(new(
                            frameInfo.Id,
                            (await ChannelNameRegistrationAsync(
                                channel.Name, cancellationToken).ConfigureAwait(false)).Id,
                            (await ChannelUnitRegistrationAsync(
                                channel.Unit, cancellationToken).ConfigureAwait(false)).Id
                            )
                        {
                            Index = i,
                            Sampling = channel.Sampling,
                            Cutoff = channel.Cutoff
                        }, cancellationToken).ConfigureAwait(false);
                    }
                },
                cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Асинхронно получает преобразование для указанного фильтра.
        /// </summary>
        /// <param name="filterInfo">
        /// Информация о фильтре.
        /// </param>
        /// <param name="cancellationToken">
        /// Токен отмены.
        /// </param>
        /// <returns>
        /// Задача, получающая преобразование.
        /// </returns>
        /// <exception cref="OperationCanceledException">
        /// Операция отменена.
        /// </exception>
        public static async Task<Transform> GetTransformAsync(FilterInfo filterInfo, CancellationToken cancellationToken)
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            ////  Получение нижней частоты фильтра.
            //double lower = filterInfo.LowerFrequency;

            //  Получение верхней частоты фильтра.
            double upper = filterInfo.UpperFrequency;

            //  Создание преобразователя частот.
            return filterInfo.Format.Name switch
            {
                "Identical" => Transform.Identity,
                "Ideal" => new SincFilter(upper),
                _ => throw new InvalidOperationException(),
            };
        }
    }
}
