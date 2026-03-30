using Apeiron.IO;
using Apeiron.Threading;
using System.Net.Sockets;

namespace Apeiron.Platform.Communication.Remoting;

/// <summary>
/// Представляет средство размещения данных удалённых методов в потоке.
/// </summary>
internal static class RemoteSpreader
{
    /// <summary>
    /// Асинхронно выполняет авторизацию.
    /// </summary>
    /// <param name="login">
    /// Имя пользователя.
    /// </param>
    /// <param name="password">
    /// Пароль.
    /// </param>
    /// <param name="stream">
    /// Сетевой поток.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая удалённый метод.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="login"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="password"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="IOException">
    /// Поток имеет неверный формат.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public static async Task<long> AuthorizationAsync(
        string login, string password, NetworkStream stream, CancellationToken cancellationToken)
    {
        //  Проверка ссылок на парметры метода.
        IsNotNull(login, nameof(login));
        IsNotNull(password, nameof(password));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Идентификатор текущего пользователя.
        long userID = 0;

        //  Выполнение удалённого вызова.
        await RemoteInvokeAsync(
            RemoteMethodID.Authorization,
            async (spreader, cancellationToken) =>
            {
                //  Запись параметров.
                await spreader.WriteStringAsync(login, cancellationToken).ConfigureAwait(false);
                await spreader.WriteStringAsync(password, cancellationToken).ConfigureAwait(false);
            },
            async (spreader, cancellationToken) =>
            {
                //  Чтение идентификатора.
                userID = await spreader.ReadInt64Async(cancellationToken).ConfigureAwait(false);
            }, stream, cancellationToken).ConfigureAwait(false);

        //  Возврат прочитанного идентификатора.
        return userID;
    }

    /// <summary>
    /// Асинхронно выполняет запрос информации о всех пользователях.
    /// </summary>
    /// <param name="stream">
    /// Сетевой поток.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая удалённый метод.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="IOException">
    /// Поток имеет неверный формат.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public static async Task<(long ID, string Name)[]> RequestAllUsersAsync(
        NetworkStream stream, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Список информации о пользователях.
        List<(long ID, string Name)> info = new();

        //  Выполнение удалённого вызова.
        await RemoteInvokeAsync(
            RemoteMethodID.RequestAllUsers,
            async (spreader, cancellationToken) =>
            {
                //  Проверка токена отмены.
                await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);
            },
            async (spreader, cancellationToken) =>
            {
                //  Чтение количества пользователей.
                int count = await spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false);

                //  Перебор всех пользователей.
                for (int i = 0; i < count; i++)
                {
                    //  Чтение информации о пользователе.
                    long id = await spreader.ReadInt64Async(cancellationToken).ConfigureAwait(false);
                    string name = await spreader.ReadStringAsync(cancellationToken).ConfigureAwait(false);

                    //  Добавление информации о пользователе.
                    info.Add(new(id, name));
                }
            }, stream, cancellationToken).ConfigureAwait(false);

        //  Возврат информации о пользователях.
        return info.ToArray();
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
    /// <param name="stream">
    /// Сетевой поток.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая удалённый метод.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="text"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="IOException">
    /// Поток имеет неверный формат.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public static async Task<(long ID, DateTime RegistrationTime)> SendMessageAsync(
        long recipientID, string text, DateTime sendTime,
        NetworkStream stream, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на текст.
        IsNotNull(text, nameof(text));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Индентификатор сообщения.
        long id = 0;

        //  Время регистрации сообщения.
        DateTime registrationTime = default;

        //  Выполнение удалённого вызова.
        await RemoteInvokeAsync(
            RemoteMethodID.SendMessage,
            async (spreader, cancellationToken) =>
            {
                //  Запись параметров.
                await spreader.WriteInt64Async(recipientID, cancellationToken).ConfigureAwait(false);
                await spreader.WriteStringAsync(text, cancellationToken).ConfigureAwait(false);
                await spreader.WriteDateTimeAsync(sendTime, cancellationToken).ConfigureAwait(false);
            },
            async (spreader, cancellationToken) =>
            {
                //  Чтение результата.
                id = await spreader.ReadInt64Async(cancellationToken).ConfigureAwait(false);
                registrationTime = await spreader.ReadDateTimeAsync(cancellationToken).ConfigureAwait(false);
            }, stream, cancellationToken).ConfigureAwait(false);

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
    /// <param name="stream">
    /// Сетевой поток.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая удалённый метод.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="IOException">
    /// Поток имеет неверный формат.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public static async Task<(DateTime BeginTime, DateTime EndTime)> RequestDialogRangeAsync(
        long userID, long companionID,
        NetworkStream stream, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Время начала диалога.
        DateTime beginTime = default;

        //  Время окончания диалога.
        DateTime endTime = default;

        //  Выполнение удалённого вызова.
        await RemoteInvokeAsync(
            RemoteMethodID.RequestDialogRange,
            async (spreader, cancellationToken) =>
            {
                //  Запись параметров.
                await spreader.WriteInt64Async(userID, cancellationToken).ConfigureAwait(false);
                await spreader.WriteInt64Async(companionID, cancellationToken).ConfigureAwait(false);
            },
            async (spreader, cancellationToken) =>
            {
                //  Чтение результата.
                beginTime = await spreader.ReadDateTimeAsync(cancellationToken).ConfigureAwait(false);
                endTime = await spreader.ReadDateTimeAsync(cancellationToken).ConfigureAwait(false);
            }, stream, cancellationToken).ConfigureAwait(false);

        //  Возврат временного диапазона диалога.
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
    /// <param name="stream">
    /// Сетевой поток.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая удалённый метод.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="IOException">
    /// Поток имеет неверный формат.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public static async Task<long> RequestCountMessagesAsync(
        long userID, long companionID, DateTime beginTime, DateTime endTime,
        NetworkStream stream, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Количество сообщений.
        long count = 0;

        //  Выполнение удалённого вызова.
        await RemoteInvokeAsync(
            RemoteMethodID.RequestCountMessages,
            async (spreader, cancellationToken) =>
            {
                //  Запись параметров.
                await spreader.WriteInt64Async(userID, cancellationToken).ConfigureAwait(false);
                await spreader.WriteInt64Async(companionID, cancellationToken).ConfigureAwait(false);
                await spreader.WriteDateTimeAsync(beginTime, cancellationToken).ConfigureAwait(false);
                await spreader.WriteDateTimeAsync(endTime, cancellationToken).ConfigureAwait(false);
            },
            async (spreader, cancellationToken) =>
            {
                //  Чтение результата.
                count = await spreader.ReadInt64Async(cancellationToken).ConfigureAwait(false);
            }, stream, cancellationToken).ConfigureAwait(false);

        //  Возврат количества сообщений.
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
    /// <param name="stream">
    /// Сетевой поток.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая удалённый метод.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="IOException">
    /// Поток имеет неверный формат.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public static async Task<long[]> RequestIDMessagesAsync(
        long userID, long companionID, DateTime beginTime, DateTime endTime,
        NetworkStream stream, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Список идентификаторов.
        List<long> ids = new();

        //  Выполнение удалённого вызова.
        await RemoteInvokeAsync(
            RemoteMethodID.RequestIDMessages,
            async (spreader, cancellationToken) =>
            {
                //  Запись параметров.
                await spreader.WriteInt64Async(userID, cancellationToken).ConfigureAwait(false);
                await spreader.WriteInt64Async(companionID, cancellationToken).ConfigureAwait(false);
                await spreader.WriteDateTimeAsync(beginTime, cancellationToken).ConfigureAwait(false);
                await spreader.WriteDateTimeAsync(endTime, cancellationToken).ConfigureAwait(false);
            },
            async (spreader, cancellationToken) =>
            {
                //  Чтение количества идентификаторов.
                int count = await spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false);

                //  Чтение идентификаторов.
                for (int i = 0; i < count; i++)
                {
                    //  Чтение идентификатора.
                    long id = await spreader.ReadInt64Async(cancellationToken).ConfigureAwait(false);

                    //  Добавление идентификатора в список.
                    ids.Add(id);
                }
            }, stream, cancellationToken).ConfigureAwait(false);

        //  Возврат массива идентификаторов.
        return ids.ToArray();
    }

    /// <summary>
    /// Асинхронно запрашивает идентификаторы сообщений.
    /// </summary>
    /// <param name="messageID">
    /// Идентификатор сообщения.
    /// </param>
    /// <param name="stream">
    /// Сетевой поток.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая удалённый метод.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="IOException">
    /// Поток имеет неверный формат.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    public static async Task<(long SenderID, string Text, DateTime SendTime, DateTime RegistrationTime)> RequestMessageAsync(
        long messageID,
        NetworkStream stream, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Параметры сообщения.
        string text = string.Empty;
        DateTime sendTime = default;
        DateTime registrationTime = default;
        long senderID = 0;

        //  Выполнение удалённого вызова.
        await RemoteInvokeAsync(
            RemoteMethodID.RequestMessage,
            async (spreader, cancellationToken) =>
            {
                //  Запись параметров.
                await spreader.WriteInt64Async(messageID, cancellationToken).ConfigureAwait(false);
            },
            async (spreader, cancellationToken) =>
            {
                //  Чтение результата.
                senderID = await spreader.ReadInt64Async(cancellationToken).ConfigureAwait(false);
                text = await spreader.ReadStringAsync(cancellationToken).ConfigureAwait(false);
                sendTime = await spreader.ReadDateTimeAsync(cancellationToken).ConfigureAwait(false);
                registrationTime = await spreader.ReadDateTimeAsync(cancellationToken).ConfigureAwait(false);
            }, stream, cancellationToken).ConfigureAwait(false);

        //  Возврат информации о сообщении.
        return (senderID, text, sendTime, registrationTime);
    }

    /// <summary>
    /// Асинхронно выполняет удалённый метод.
    /// </summary>
    /// <param name="methodID">
    /// Идентификатор удалённого метода.
    /// </param>
    /// <param name="sender">
    /// Метод, выполняющий запись параметров метода.
    /// </param>
    /// <param name="recipient">
    /// Метод, выполняющий чтение результатов метода.
    /// </param>
    /// <param name="stream">
    /// Сетевой поток.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая удалённый вызов.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="methodID"/> передано значение,
    /// которое не содержится в перечислении <see cref="RemoteMethodID"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="sender"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="recipient"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="stream"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="IOException">
    /// Поток имеет неверный формат.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    private static async Task RemoteInvokeAsync(RemoteMethodID methodID,
        AsyncAction<Spreader> sender, AsyncAction<Spreader> recipient,
        NetworkStream stream, CancellationToken cancellationToken)
    {
        //  Проверка идентификатора метода.
        IsDefined(methodID, nameof(methodID));

        //  Проверка ссылок на методы.
        IsNotNull(sender, nameof(sender));
        IsNotNull(recipient, nameof(recipient));

        //  Проверка ссылки на распределитель данных котока.
        IsNotNull(stream, nameof(stream));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание распределителя данных потока.
        Spreader spreader = new(stream);

        //  Запись идентификатора метода.
        await spreader.WriteInt32Async((int)methodID, cancellationToken).ConfigureAwait(false);

        //  Запись данных метода.
        await sender(spreader, cancellationToken).ConfigureAwait(false);

        //  Сброс данных буферов.
        await stream.FlushAsync(cancellationToken).ConfigureAwait(false);

        //  Чтение типа результата.
        RemoteResult remoteResult = (RemoteResult)await spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false);

        //  Анализ типа результата.
        switch (remoteResult)
        {
            case RemoteResult.Void:
                break;
            case RemoteResult.Data:
                //  Чтение результата.
                await recipient(spreader, cancellationToken).ConfigureAwait(false);
                break;
            case RemoteResult.Exception:
                //  Чтение информации об исключении.
                string exceptionMessage = await spreader.ReadStringAsync(cancellationToken).ConfigureAwait(false);

                //  Произошла попытка выполнить недопустимую операцию.
                throw new InvalidOperationException(exceptionMessage);
            default:
                //  Поток имеет неверный формат.
                throw Exceptions.StreamInvalidFormat();
        }
    }
}
