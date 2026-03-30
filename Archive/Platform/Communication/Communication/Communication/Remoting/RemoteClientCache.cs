using Apeiron.Platform.Communication.Elements;
using Apeiron.Threading;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace Apeiron.Platform.Communication.Remoting;

/// <summary>
/// Представляет кэш клиентов удалённого подключения.
/// </summary>
internal sealed class RemoteClientCache :
    Element,
    IDisposable
{
    /// <summary>
    /// Поле для хранения очереди клиентов.
    /// </summary>
    private readonly ConcurrentQueue<RemoteClient> _Clients;

    /// <summary>
    /// Поле для хранения значения, определяющего разрушен ли объект.
    /// </summary>
    private volatile bool _IsDisposed;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="communicator">
    /// Коммуникатор с серверным узлом.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="communicator"/> передана пустая ссылка.
    /// </exception>
    public RemoteClientCache(Communicator communicator) :
        base(communicator)
    {
        //  Создание очереди клиентов.
        _Clients = new();

        //  Установка значения, определяющего разрушен ли объект.
        _IsDisposed = false;

        //  Создание пользователя.
        User = new(communicator, 0, ConnectionOptions.Login);
    }

    /// <summary>
    /// Возвращает пользователя.
    /// </summary>
    public User User { get; }

    /// <summary>
    /// Асинхронно выполняет действие.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить.
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
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Не удалось установить соединение с сервером.
    /// </exception>
    public async Task InvokeAsync(AsyncAction<NetworkStream> action, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на действие.
        IsNotNull(action, nameof(action));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка кэша.
        IsNotDisposed();

        //  Извлечение клиента из очереди.
        if (!_Clients.TryDequeue(out RemoteClient? client))
        {
            //  Создание нового клиента.
            client = await CreateClientAsync(cancellationToken).ConfigureAwait(false);
        }

        //  Выполнение действия в контексте клиента.
        await client.InvokeAsync(action, cancellationToken).ConfigureAwait(false);

        //  Проверка клиента.
        if (!client.IsDisposed)
        {
            //  Возврат клиента в очередь.
            _Clients.Enqueue(client);
        }
    }

    /// <summary>
    /// Асинхронно создаёт новых клиентов удалённого подключения.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая создание клиентов.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    private async Task<RemoteClient> CreateClientAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка кэша.
        IsNotDisposed();

        //  Создание источника завершения задачи.
        TaskCompletionSource<RemoteClient> source = new();

        //  Запус асинхронного выполнения.
        _ = Task.Run(async delegate
        {
            //  Флаг найденного подключения.
            int isFind = 0;

            //  Блок с  гарантированным завершением.
            try
            {
                //  Проверка токена отмены.
                await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                //  Запрос IP-адресов.
                IPAddress[] addresses = await Dns.GetHostAddressesAsync(
                    ConnectionOptions.HostName, cancellationToken).ConfigureAwait(false);

                //  Список исключений.
                ConcurrentBag<Exception> exceptions = new();

                //  Асинхронная работа с подключениями.
                await Parallel.ForEachAsync(
                    addresses,
                    cancellationToken,
                    async (address, cancellationToken) =>
                    {
                        //  Проверка токена отмены.
                        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                        //  Блок перехвата всех некритических исключений.
                        try
                        {
                            //  Создание нового клиента.
                            RemoteClient client = await RemoteClient.CreateAsync(
                                Communicator, User, address, cancellationToken).ConfigureAwait(false);

                            //  Корректировка флага ожидания.
                            if (Interlocked.Exchange(ref isFind, 1) == 0)
                            {
                                //  Завершение задачи.
                                source.SetResult(client);
                            }
                            else
                            {
                                //  Добавление клиента в очередь.
                                _Clients.Enqueue(client);
                            }

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
                    });

                //  Корректировка флага ожидания.
                if (Interlocked.Exchange(ref isFind, 1) == 0)
                {
                    //  Не найдено доступное подключение.
                    source.SetException(new InvalidOperationException("Не удалось установить соединение с сервером.",
                        new AggregateException(exceptions)));
                }
            }
            finally
            {
                //  Проверка флага результата.
                if (isFind == 0)
                {
                    //  Задача отменена.
                    source.SetCanceled();
                }
            }
        }, cancellationToken);

        //  Ожидание первого клиента.
        return await source.Task.ConfigureAwait(false);
    }

    /// <summary>
    /// Проверяет, разрушен ли объект.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void IsNotDisposed()
    {
        //  Проверка значения, определяющего разрушен ли объект.
        if (_IsDisposed)
        {
            //  В результате операции произошло обращение к разрушенному объекту.
            throw Exceptions.OperationObjectDisposed(nameof(RemoteClientCache));
        }
    }

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    void IDisposable.Dispose()
    {
        //  Проверка необходимости разрушения.
        if (!_IsDisposed)
        {
            //  Установка значения, определяющего разрушен ли объект.
            _IsDisposed = true;

            //  Извлечение всех клиентов из очереди.
            while (_Clients.TryDequeue(out RemoteClient? client))
            {
                //  Блок перехвата всех некритических исключений.
                try
                {
                    //  Разрушение клиента.
                    ((IDisposable)client).Dispose();
                }
                catch (Exception ex)
                {
                    //  Проверка критического исключения.
                    if (ex.IsCritical())
                    {
                        //  Повторный выброс исключения.
                        throw;
                    }
                }
            }
        }
    }
}
