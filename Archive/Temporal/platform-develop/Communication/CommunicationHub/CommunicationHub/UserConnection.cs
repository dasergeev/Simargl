using Apeiron.IO;
using Apeiron.Platform.Communication.CommunicationDatabase;
using Apeiron.Platform.Communication.Remoting;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace Apeiron.Platform.Communication.CommunicationHub;

/// <summary>
/// Представляет подключение пользователя.
/// </summary>
internal sealed class UserConnection
{
    /// <summary>
    /// Поле для хранения TCP клиента.
    /// </summary>
    private readonly TcpClient _Client;

    /// <summary>
    /// Поле для хранения средства ведения журнала.
    /// </summary>
    private readonly ILogger<CommunicationWorker> _Logger;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="userID">
    /// Идентификатор пользователя.
    /// </param>
    /// <param name="client">
    /// TCP-клиент.
    /// </param>
    /// <param name="logger">
    /// Средство ведения журнала.
    /// </param>
    public UserConnection([ParameterNoChecks] long userID, [ParameterNoChecks] TcpClient client,
        [ParameterNoChecks] ILogger<CommunicationWorker> logger)
    {
        //  Установка значений.
        UserID = userID;
        _Client = client;
        //  Установка средства ведения журнала.
        _Logger = logger;
    }

    /// <summary>
    /// Возвращает идентификатор пользователя.
    /// </summary>
    public long UserID { get; }

    /// <summary>
    /// Асинхронно выполняет работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с клиентом.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение потока для работы с клиентом.
        NetworkStream stream = _Client.GetStream();

        //  Создание распределителя потока.
        Spreader spreader = new(stream);

        //  Основной цикл работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Чтение идентификатора метода.
            RemoteMethodID methodID = (RemoteMethodID)await spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false);

            //  Вывод в журнал.
            _Logger.LogInformation("Запрос на вызов метода: {methodID}", methodID);

            //  Блок перехвата всех несистемных исключений.
            try
            {
                //  Анализ идентификатора.
                switch (methodID)
                {
                    case RemoteMethodID.RequestAllUsers:
                        await RequestAllUsersAsync(stream, cancellationToken).ConfigureAwait(false);
                        break;
                    case RemoteMethodID.SendMessage:
                        await SendMessageAsync(stream, cancellationToken).ConfigureAwait(false);
                        break;
                    case RemoteMethodID.RequestDialogRange:
                        await RequestDialogRangeAsync(stream, cancellationToken).ConfigureAwait(false);
                        break;
                    case RemoteMethodID.RequestCountMessages:
                        await RequestCountMessagesAsync(stream, cancellationToken).ConfigureAwait(false);
                        break;
                    case RemoteMethodID.RequestIDMessages:
                        await RequestIDMessagesAsync(stream, cancellationToken).ConfigureAwait(false);
                        break;
                    case RemoteMethodID.RequestMessage:
                        await RequestMessageAsync(stream, cancellationToken).ConfigureAwait(false);
                        break;
                    default:
                        //  Завершение работы с соединением.
                        throw Exceptions.StreamInvalidFormat();
                }
            }
            catch (Exception ex)
            {
                //  Проверка системного исключения.
                if (ex.IsSystem())
                {
                    //  Повторный выброс исключения.
                    throw;
                }

                //  Запись типа результата.
                await spreader.WriteInt32Async(
                    (int)RemoteResult.Exception, cancellationToken).ConfigureAwait(false);

                //  Запись текста исключения.
                await spreader.WriteStringAsync(ex.ToString(), cancellationToken).ConfigureAwait(false);

                //  Сброс данных в поток.
                await stream.FlushAsync(cancellationToken).ConfigureAwait(false);

                //  Повторный выброс исключения.
                throw;
            }
        }
    }

    /// <summary>
    /// Асинхронно выполняет метод <see cref="RemoteMethodID.RequestAllUsers"/>.
    /// </summary>
    /// <param name="stream">
    /// Сетевой поток.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая метод.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private static async Task RequestAllUsersAsync(NetworkStream stream, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание распределителя данных потока.
        Spreader spreader = new(stream);

        //  Создание подключения к базе данных.
        using CommunicationContext context = new();

        //  Получение всех пользователей.
        List<UserData> users = await context.Users.ToListAsync(cancellationToken).ConfigureAwait(false);

        //  Запись типа результата.
        await spreader.WriteInt32Async(
            (int)RemoteResult.Data, cancellationToken).ConfigureAwait(false);

        //  Запись количества пользователей.
        await spreader.WriteInt32Async(users.Count, cancellationToken).ConfigureAwait(false);

        //  Перебор всех пользователей.
        foreach (UserData user in users)
        {
            //  Запись информации о пользователе.
            await spreader.WriteInt64Async(user.ID, cancellationToken).ConfigureAwait(false);
            await spreader.WriteStringAsync(user.Name, cancellationToken).ConfigureAwait(false);
        }

        //  Сброс данных в поток.
        await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет метод <see cref="RemoteMethodID.SendMessage"/>.
    /// </summary>
    /// <param name="stream">
    /// Сетевой поток.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая метод.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async Task SendMessageAsync(NetworkStream stream, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание распределителя данных потока.
        Spreader spreader = new(stream);

        //  Чтение параметров.
        long recipientID = await spreader.ReadInt64Async(cancellationToken).ConfigureAwait(false);
        string text = await spreader.ReadStringAsync(cancellationToken).ConfigureAwait(false);
        DateTime sendTime = await spreader.ReadDateTimeAsync(cancellationToken).ConfigureAwait(false);
        DateTime registrationTime = DateTime.Now;

        //  Создание нового сообщения.
        MessageData message = new()
        {
            SenderID = UserID,
            RecipientID = recipientID,
            Text = text,
            SendTime = sendTime,
            RegistrationTime = registrationTime,
        };

        //  Создание подключения к базе данных.
        using CommunicationContext context = new();

        //  Начало транзакции.
        using var transaction = context.Database.BeginTransaction();

        //  Добавление записи в базу.
        await context.Messages.AddAsync(message, cancellationToken).ConfigureAwait(false);

        //  Сохранение изменений в базу данных.
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        //  Фиксирование изменений.
        transaction.Commit();

        //  Запись типа результата.
        await spreader.WriteInt32Async(
            (int)RemoteResult.Data, cancellationToken).ConfigureAwait(false);

        //  Запись результатов.
        await spreader.WriteInt64Async(message.ID, cancellationToken).ConfigureAwait(false);
        await spreader.WriteDateTimeAsync(message.RegistrationTime, cancellationToken).ConfigureAwait(false);

        //  Сброс данных в поток.
        await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет метод <see cref="RemoteMethodID.RequestDialogRange"/>.
    /// </summary>
    /// <param name="stream">
    /// Сетевой поток.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая метод.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private static async Task RequestDialogRangeAsync(NetworkStream stream, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание распределителя данных потока.
        Spreader spreader = new(stream);

        //  Чтение параметров.
        long userID = await spreader.ReadInt64Async(cancellationToken).ConfigureAwait(false);
        long companionID = await spreader.ReadInt64Async(cancellationToken).ConfigureAwait(false);

        //  Создание подключения к базе данных.
        using CommunicationContext context = new();

        //  Запрос в базу данных.
        DateTime beginTime = context.Messages
            .Where(message => (message.SenderID == userID && message.RecipientID == companionID) ||
            (message.SenderID == companionID && message.RecipientID == userID))
            .Select(message => message.RegistrationTime)
            .Min();

        //  Запрос в базу данных.
        DateTime endTime = context.Messages
            .Where(message => (message.SenderID == userID && message.RecipientID == companionID) ||
            (message.SenderID == companionID && message.RecipientID == userID))
            .Select(message => message.RegistrationTime)
            .Max();

        //  Запись типа результата.
        await spreader.WriteInt32Async(
            (int)RemoteResult.Data, cancellationToken).ConfigureAwait(false);

        //  Запись результатов.
        await spreader.WriteDateTimeAsync(beginTime, cancellationToken).ConfigureAwait(false);
        await spreader.WriteDateTimeAsync(endTime, cancellationToken).ConfigureAwait(false);

        //  Сброс данных в поток.
        await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет метод <see cref="RemoteMethodID.RequestCountMessages"/>.
    /// </summary>
    /// <param name="stream">
    /// Сетевой поток.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая метод.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private static async Task RequestCountMessagesAsync(NetworkStream stream, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание распределителя данных потока.
        Spreader spreader = new(stream);

        //  Чтение параметров.
        long userID = await spreader.ReadInt64Async(cancellationToken).ConfigureAwait(false);
        long companionID = await spreader.ReadInt64Async(cancellationToken).ConfigureAwait(false);
        DateTime beginTime = await spreader.ReadDateTimeAsync(cancellationToken).ConfigureAwait(false);
        DateTime endTime = await spreader.ReadDateTimeAsync(cancellationToken).ConfigureAwait(false);

        //  Создание подключения к базе данных.
        using CommunicationContext context = new();

        //  Запрос в базу данных.
        long count = context.Messages
            .Where(message => ((message.SenderID == userID && message.RecipientID == companionID) ||
            (message.SenderID == companionID && message.RecipientID == userID)) &&
            (message.RegistrationTime >= beginTime && message.RegistrationTime <= endTime))
            .LongCount();

        //  Запись типа результата.
        await spreader.WriteInt32Async(
            (int)RemoteResult.Data, cancellationToken).ConfigureAwait(false);

        //  Запись результатов.
        await spreader.WriteInt64Async(count, cancellationToken).ConfigureAwait(false);

        //  Сброс данных в поток.
        await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет метод <see cref="RemoteMethodID.RequestIDMessages"/>.
    /// </summary>
    /// <param name="stream">
    /// Сетевой поток.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая метод.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private static async Task RequestIDMessagesAsync(NetworkStream stream, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание распределителя данных потока.
        Spreader spreader = new(stream);

        //  Чтение параметров.
        long userID = await spreader.ReadInt64Async(cancellationToken).ConfigureAwait(false);
        long companionID = await spreader.ReadInt64Async(cancellationToken).ConfigureAwait(false);
        DateTime beginTime = await spreader.ReadDateTimeAsync(cancellationToken).ConfigureAwait(false);
        DateTime endTime = await spreader.ReadDateTimeAsync(cancellationToken).ConfigureAwait(false);

        //  Создание подключения к базе данных.
        using CommunicationContext context = new();

        //  Запрос в базу данных.
        long[] ids = context.Messages
            .Where(message => ((message.SenderID == userID && message.RecipientID == companionID) ||
            (message.SenderID == companionID && message.RecipientID == userID)) &&
            (message.RegistrationTime >= beginTime && message.RegistrationTime <= endTime))
            .Select(message => message.ID)
            .ToArray();

        //  Запись типа результата.
        await spreader.WriteInt32Async(
            (int)RemoteResult.Data, cancellationToken).ConfigureAwait(false);

        //  Запись количества идентификаторов.
        await spreader.WriteInt32Async(ids.Length, cancellationToken).ConfigureAwait(false);

        //  Запись идентификаторов.
        for (int i = 0; i < ids.Length; i++)
        {
            //  Запись идентификатора.
            await spreader.WriteInt64Async(ids[i], cancellationToken).ConfigureAwait(false);
        }

        //  Сброс данных в поток.
        await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет метод <see cref="RemoteMethodID.RequestMessage"/>.
    /// </summary>
    /// <param name="stream">
    /// Сетевой поток.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая метод.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private static async Task RequestMessageAsync(NetworkStream stream, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание распределителя данных потока.
        Spreader spreader = new(stream);

        //  Чтение параметров.
        long messageID = await spreader.ReadInt64Async(cancellationToken).ConfigureAwait(false);

        //  Создание подключения к базе данных.
        using CommunicationContext context = new();

        //  Запрос в базу данных.
        MessageData message = context.Messages.First(message => message.ID == messageID);

        //  Запись типа результата.
        await spreader.WriteInt32Async(
            (int)RemoteResult.Data, cancellationToken).ConfigureAwait(false);

        //  Запись результатов.
        await spreader.WriteInt64Async(message.SenderID, cancellationToken).ConfigureAwait(false);
        await spreader.WriteStringAsync(message.Text, cancellationToken).ConfigureAwait(false);
        await spreader.WriteDateTimeAsync(message.SendTime, cancellationToken).ConfigureAwait(false);
        await spreader.WriteDateTimeAsync(message.RegistrationTime, cancellationToken).ConfigureAwait(false);

        //  Сброс данных в поток.
        await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
    }
}
