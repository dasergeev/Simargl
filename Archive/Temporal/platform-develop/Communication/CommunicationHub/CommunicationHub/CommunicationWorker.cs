using Apeiron.IO;
using Apeiron.Platform.Communication.CommunicationDatabase;
using Apeiron.Platform.Communication.Remoting;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Sockets;

namespace Apeiron.Platform.Communication.CommunicationHub;

/// <summary>
/// Представляет основной фоновый процесс службы.
/// </summary>
public sealed class CommunicationWorker :
    BackgroundService
{
    /// <summary>
    /// Поле для хранения средства ведения журнала.
    /// </summary>
    private readonly ILogger<CommunicationWorker> _Logger;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="logger">
    /// Средство ведения журнала.
    /// </param>
    public CommunicationWorker([ParameterNoChecks] ILogger<CommunicationWorker> logger)
    {
        //  Установка средства ведения журнала.
        _Logger = logger;
    }

    /// <summary>
    /// Асинхронно выполняет основную работу службы.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Основной цикл поддержки.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Блок перехвата всех несистемных исключений.
            try
            {
                //  Создание средства прослушивания сети.
                TcpListener listener = new(IPAddress.Any, 7013);

                //  Запуск прослушивания сети.
                listener.Start();

                //  Блок с гарантированным завершением.
                try
                {
                    //  Основоной цикл задачи.
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        //  Ожидание входящего подключения.
                        TcpClient client = await listener.AcceptTcpClientAsync(cancellationToken).ConfigureAwait(false);

                        //  Запуск асинхронной задачи для работы с клиентом.
                        _ = Task.Run(async delegate
                        {
                            //  Асинхронная работа с клиентом.
                            await WorkWithClientAsync(client, cancellationToken).ConfigureAwait(false);
                        }, cancellationToken).ConfigureAwait(false);
                    }
                }
                finally
                {
                    //  Остановка прослушивания сети.
                    listener.Stop();
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

                //  Вывод информации об ошибке в журнал.
                _Logger.LogError("Произошло исключение: {ex}", ex);
            }
        }
    }

    /// <summary>
    /// Асинхронно выполняет работу с клиентом.
    /// </summary>
    /// <param name="client">
    /// Клиент.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с клиентом.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async Task WorkWithClientAsync([ParameterNoChecks] TcpClient client, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Вывод информации в журнал.
        _Logger.LogInformation("Новое входящее подключение {endPoint}.", client.Client.RemoteEndPoint);

        //  Блок с гарантированным завершением.
        try
        {
            //  Получение потока для работы с клиентом.
            NetworkStream stream = client.GetStream();

            //  Создание распределителя потока.
            Spreader spreader = new(stream);

            //  Чтение идентификатора метода.
            RemoteMethodID methodID = (RemoteMethodID)await spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false);

            //  Проверка идентификатора метода.
            if (methodID != RemoteMethodID.Authorization)
            {
                //  Завершение работы с соединением.
                throw Exceptions.StreamInvalidFormat();
            }

            //  Чтение логина и пароля.
            string name = await spreader.ReadStringAsync(cancellationToken).ConfigureAwait(false);
            string password = await spreader.ReadStringAsync(cancellationToken).ConfigureAwait(false);

            //  Идентификатор пользователя.
            long userID = 0;

            //  Блок перехвата всех несистемных исключений.
            try
            {
                //  Создание контекста для подключения к базе данных.
                using CommunicationContext context = new();

                //  Поиск пользователя.
                UserData? user = await context.Users.FirstOrDefaultAsync(
                    user => user.Name == name, cancellationToken).ConfigureAwait(false);

                //  Проверка пользователя.
                if (user is null)
                {
                    //  Пользователь не найден.
                    throw new InvalidDataException("Пользователь не найден.");
                }

                //  Проверка пароля.
                if (user.Password != password)
                {
                    //  Пользователь не найден.
                    throw new InvalidDataException("Неверный пароль.");
                }

                //  Установка идентификатора пользователя.
                userID = user.ID;

                //  Вывод информации в журнал.
                _Logger.LogInformation("Новое подключение пользователя {userName}.", user.Name);
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

            //  Запись типа результата.
            await spreader.WriteInt32Async(
                    (int)RemoteResult.Data, cancellationToken).ConfigureAwait(false);

            //  Запись идентификатора текущего пользователя.
            await spreader.WriteInt64Async(
                    userID, cancellationToken).ConfigureAwait(false);

            //  Сброс данных в поток.
            await stream.FlushAsync(cancellationToken).ConfigureAwait(false);

            //  Создание подключения пользователя.
            UserConnection connection = new(userID, client, _Logger);

            //  Асинхронная работа с подключением.
            await connection.InvokeAsync(cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            //  Разрушение клиента.
            client.Dispose();
        }
    }
}
