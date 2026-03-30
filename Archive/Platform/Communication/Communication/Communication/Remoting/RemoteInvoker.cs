using Apeiron.Platform.Communication.Elements;
using Apeiron.Threading;
using System.Net.Sockets;

namespace Apeiron.Platform.Communication.Remoting;

/// <summary>
/// Представляет средство вызова удалённых методов.
/// </summary>
internal sealed class RemoteInvoker :
    Element,
    IDisposable
{
    /// <summary>
    /// Поле для хранения кэша клиентов удалённого подключения.
    /// </summary>
    private readonly RemoteClientCache _ClientCache;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="communicator">
    /// Коммуникатор с серверным узлом.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="communicator"/> передана пустая ссылка.
    /// </exception>
    public RemoteInvoker(Communicator communicator) :
        base(communicator)
    {
        //  Создание кэша клиентов удалённого подключения.
        _ClientCache = new(communicator);
    }

    /// <summary>
    /// Возвращает пользователя.
    /// </summary>
    public User User => _ClientCache.User;

    /// <summary>
    /// Асинхронно выполняет запрос информации о всех пользователях.
    /// </summary>
    /// <param name="options">
    /// Параметры удалённого вызова.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая обновление.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Не удалось установить соединение с сервером.
    /// </exception>
    public async Task<User[]> RequestAllUsersAsync(
        RemoteInvokeOptions options, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Список пользователей.
        List<User> users = new();

        //  Безопасное выполнение действия.
        await SafeInvokeAsync(async (stream, cancellationToken) =>
        {
            //  Проверка токена отмены.
            await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Запрос информации о пользователях.
            (long ID, string Name)[] infos = await RemoteSpreader.RequestAllUsersAsync(
                stream, cancellationToken).ConfigureAwait(false);

            //  Перебор информации о пользователях.
            foreach ((long id, string name) in infos)
            {
                //  Добавление пользователя в список.
                users.Add(new(Communicator, id, name));
            }
        }, options, cancellationToken).ConfigureAwait(false);

        //  Возврат массива пользователей.
        return users.ToArray();
    }

    /// <summary>
    /// Асинхронно отправляет сообщение.
    /// </summary>
    /// <param name="recipientID">
    /// Идентификатор получателя.
    /// </param>
    /// <param name="text">
    /// Текс сообщения.
    /// </param>
    /// <param name="sendTime">
    /// Время отправки.
    /// </param>
    /// <param name="options">
    /// Параметры удалённого вызова.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая отправку сообщения.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="text"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Не удалось установить соединение с сервером.
    /// </exception>
    public async Task<(long ID, DateTime RegistrationTime)> SendMessageAsync(
        long recipientID, string text, DateTime sendTime,
        RemoteInvokeOptions options, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на текст.
        IsNotNull(text, nameof(text));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Индентификатор сообщения.
        long id = 0;

        //  Время регистрации сообщения.
        DateTime registrationTime = default;

        //  Безопасное выполнение действия.
        await SafeInvokeAsync(async (stream, cancellationToken) =>
        {
            //  Проверка токена отмены.
            await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Отправка сообщения.
            (id, registrationTime) = await RemoteSpreader.SendMessageAsync(
                recipientID, text, sendTime,
                stream, cancellationToken).ConfigureAwait(false);
        }, options, cancellationToken).ConfigureAwait(false);

        //  Возврат информации о сообщении.
        return (id, registrationTime);
    }

    /// <summary>
    /// Асинхронно запрашивает временной диапазон диалога.
    /// </summary>
    /// <param name="userID">
    /// Идентификатор пользователя.
    /// </param>
    /// <param name="companionID">
    /// Идентификатор собеседника.
    /// </param>
    /// <param name="options">
    /// Параметры удалённого вызова.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запрос.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Не удалось установить соединение с сервером.
    /// </exception>
    public async Task<(DateTime BeginTime, DateTime EndTime)> RequestDialogRangeAsync(
        long userID, long companionID,
        RemoteInvokeOptions options, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Время начала диалога.
        DateTime beginTime = default;

        //  Время окончания диалога.
        DateTime endTime = default;

        //  Безопасное выполнение действия.
        await SafeInvokeAsync(async (stream, cancellationToken) =>
        {
            //  Проверка токена отмены.
            await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Выполнение запроса.
            (beginTime, endTime) = await RemoteSpreader.RequestDialogRangeAsync(
                userID, companionID,
                stream, cancellationToken).ConfigureAwait(false);
        }, options, cancellationToken).ConfigureAwait(false);

        //  Возврат результата запроса.
        return (beginTime, endTime);
    }

    /// <summary>
    /// Асинхронно запрашивает количество сообщений.
    /// </summary>
    /// <param name="userID">
    /// Идентификатор пользователя.
    /// </param>
    /// <param name="companionID">
    /// Идентификатор собеседника.
    /// </param>
    /// <param name="beginTime">
    /// Время начала.
    /// </param>
    /// <param name="endTime">
    /// Время окончания.
    /// </param>
    /// <param name="options">
    /// Параметры удалённого вызова.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запрос.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Не удалось установить соединение с сервером.
    /// </exception>
    public async Task<long> RequestCountMessagesAsync(
        long userID, long companionID, DateTime beginTime, DateTime endTime,
        RemoteInvokeOptions options, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Количество сообщений.
        long count = 0;

        //  Безопасное выполнение действия.
        await SafeInvokeAsync(async (stream, cancellationToken) =>
        {
            //  Проверка токена отмены.
            await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Выполнение запроса.
            count = await RemoteSpreader.RequestCountMessagesAsync(
                userID, companionID, beginTime, endTime,
                stream, cancellationToken).ConfigureAwait(false);
        }, options, cancellationToken).ConfigureAwait(false);

        //  Возврат результата запроса.
        return count;
    }

    /// <summary>
    /// Асинхронно запрашивает идентификаторы сообщений.
    /// </summary>
    /// <param name="userID">
    /// Идентификатор пользователя.
    /// </param>
    /// <param name="companionID">
    /// Идентификатор собеседника.
    /// </param>
    /// <param name="beginTime">
    /// Время начала.
    /// </param>
    /// <param name="endTime">
    /// Время окончания.
    /// </param>
    /// <param name="options">
    /// Параметры удалённого вызова.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запрос.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Не удалось установить соединение с сервером.
    /// </exception>
    public async Task<long[]> RequestIDMessagesAsync(
        long userID, long companionID, DateTime beginTime, DateTime endTime,
        RemoteInvokeOptions options, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Массив идентификаторов.
        long[] ids = Array.Empty<long>();

        //  Безопасное выполнение действия.
        await SafeInvokeAsync(async (stream, cancellationToken) =>
        {
            //  Проверка токена отмены.
            await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Выполнение запроса.
            ids = await RemoteSpreader.RequestIDMessagesAsync(
                userID, companionID, beginTime, endTime,
                stream, cancellationToken).ConfigureAwait(false);
        }, options, cancellationToken).ConfigureAwait(false);

        //  Возврат результата запроса.
        return ids;
    }

    /// <summary>
    /// Асинхронно запрашивает идентификаторы сообщений.
    /// </summary>
    /// <param name="messageID">
    /// Идентификатор сообщения.
    /// </param>
    /// <param name="options">
    /// Параметры удалённого вызова.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запрос.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Не удалось установить соединение с сервером.
    /// </exception>
    public async Task<(long SenderID, string Text, DateTime SendTime, DateTime RegistrationTime)> RequestMessageAsync(
        long messageID,
        RemoteInvokeOptions options, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Параметры сообщения.
        string text = string.Empty;
        DateTime sendTime = default;
        DateTime registrationTime = default;
        long senderID = 0;

        //  Безопасное выполнение действия.
        await SafeInvokeAsync(async (stream, cancellationToken) =>
        {
            //  Проверка токена отмены.
            await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Выполнение запроса.
            (senderID, text, sendTime, registrationTime) = await RemoteSpreader.RequestMessageAsync(
                messageID,
                stream, cancellationToken).ConfigureAwait(false);
        }, options, cancellationToken).ConfigureAwait(false);

        //  Возврат результата запроса.
        return (senderID, text, sendTime, registrationTime);
    }

    /// <summary>
    /// Асинхронно безопасно выполняет действие.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить.
    /// </param>
    /// <param name="options">
    /// Параметры удалённого вызова.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая действие.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Не удалось установить соединение с сервером.
    /// </exception>
    private async Task SafeInvokeAsync(AsyncAction<NetworkStream> action,
        RemoteInvokeOptions options, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на действие.
        IsNotNull(action, nameof(action));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание списка исключений.
        List<Exception> exceptions = new();

        //  Номарлизация параметров удалённого вызова.
        options = Communicator.Options.NormalizationRemoteInvokeOptions(options);

        //  Цикл попыток.
        for (int attempt = 0; attempt < options.Attempts; attempt++)
        {
            //  Блок перехвата всех некритических исключений.
            try
            {
                //  Выполнение действия.
                await _ClientCache.InvokeAsync(async (stream, cancellationToken) =>
                {
                    //  Корректировка токена отмены, отслеживающего таймаут.
                    cancellationToken = await options.CreateTokenAsync(cancellationToken).ConfigureAwait(false);

                    //  Выполнение действия.
                    await action(stream, cancellationToken).ConfigureAwait(false);
                }, cancellationToken).ConfigureAwait(false);

                //  Метод успешно выполнен.
                return;
            }
            catch (Exception ex)
            {
                //  Проверка критического исключения.
                if (ex.IsCritical())
                {
                    //  Повторный выброс исключения.
                    throw;
                }

                //  Добавление исключения в список.
                exceptions.Add(ex);
            }

            //  Ожидание перед повторным вызовом.
            await Task.Delay(options.Delay, cancellationToken).ConfigureAwait(false);
        }

        //  Не удалось установить соединение с сервером.
        throw new InvalidOperationException("Не удалось установить соединение с сервером.",
            new AggregateException(exceptions));
    }

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    void IDisposable.Dispose()
    {
        //  Разрушение кэша клиентов удалённого подключения.
        ((IDisposable)_ClientCache).Dispose();
    }
}
