using Apeiron.Platform.Communication.Elements;
using Apeiron.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace Apeiron.Platform.Communication.Remoting;

/// <summary>
/// Представляет клиента удалённого подключения.
/// </summary>
internal sealed class RemoteClient :
    Element,
    IDisposable
{
    /// <summary>
    /// Поле для хранения TCP-клиента.
    /// </summary>
    private readonly TcpClient _TcpClient;

    /// <summary>
    /// Поле для хранения сетевого потока.
    /// </summary>
    private readonly NetworkStream _Stream;

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
    /// <param name="address">
    /// IP-адрес подключения.
    /// </param>
    /// <param name="tcpClient">
    /// TCP-клиент.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="communicator"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="address"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="tcpClient"/> передана пустая ссылка.
    /// </exception>
    private RemoteClient(Communicator communicator, IPAddress address, TcpClient tcpClient) :
        base(communicator)
    {
        //  Установка IP-адреса подключения.
        Address = IsNotNull(address, nameof(address));

        //  Установка TCP-клиента.
        _TcpClient = IsNotNull(tcpClient, nameof(tcpClient));

        //  Установка сетевого потока.
        _Stream = _TcpClient.GetStream();

        //  Установка значения, определяющего разрушен ли объект.
        _IsDisposed = false;
    }

    /// <summary>
    /// Возвращает IP-адрес подключения.
    /// </summary>
    public IPAddress Address { get; }

    /// <summary>
    /// Возвращает значение, определяющее разрушен ли объект.
    /// </summary>
    public bool IsDisposed => _IsDisposed;

    /// <summary>
    /// Асинхронно выполняет действие с текущим клиентом.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить в контексте текущего клиента.
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
    public async Task InvokeAsync(AsyncAction<NetworkStream> action, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на действие.
        IsNotNull(action, nameof(action));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка клиента.
        IsNotDisposed();

        //  Блок перехвата исключений.
        try
        {
            //  Выполнение действия.
            await action(_Stream, cancellationToken).ConfigureAwait(false);
        }
        catch
        {
            //  Разрушение клиента.
            ((IDisposable)this).Dispose();

            //  Повторный выброс исключения.
            throw;
        }
    }

    /// <summary>
    /// Асинхронно создаёт клиента удалённого подключения.
    /// </summary>
    /// <param name="communicator">
    /// Коммуникатор с серверным узлом.
    /// </param>
    /// <param name="user">
    /// Пользователь.
    /// </param>
    /// <param name="address">
    /// IP-адрес подключения.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, создающая клиента удалённого подключения.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="communicator"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="user"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="address"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public static async Task<RemoteClient> CreateAsync(Communicator communicator,
        User user, IPAddress address, CancellationToken cancellationToken)
    {
        //  Проверка входных параметров.
        IsNotNull(communicator, nameof(communicator));
        IsNotNull(user, nameof(user));
        IsNotNull(address, nameof(address));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание TCP-клиента.
        TcpClient tcpClient = new();

        //  Блок перехвата всех некритических исключений.
        try
        {
            //  Установка соединения.
            await tcpClient.ConnectAsync(address, communicator.Options.ConnectionOptions.Port,
                cancellationToken).ConfigureAwait(false);

            //  Выполнение авторизации.
            user.ID = await RemoteSpreader.AuthorizationAsync(
                communicator.Options.ConnectionOptions.Login,
                communicator.Options.ConnectionOptions.Password,
                tcpClient.GetStream(), cancellationToken);

            //  Возврат нового клиента.
            return new(communicator, address, tcpClient);
        }
        catch
        {
            //  Разрушение TCP-клиента.
            tcpClient.Dispose();

            //  Повторный выброс исключения.
            throw;
        }
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

            //  Разрушение TCP-клиента.
            _TcpClient.Dispose();
        }
    }
}
