//using Simargl.Synergy.Core;
//using System.Net.Security;
//using System.Net.Sockets;
//using System.Security.Cryptography.X509Certificates;

//namespace Simargl.Synergy;

///// <summary>
///// Представляет соединение.
///// </summary>
//public abstract class Join :
//    Anything
//{
//    /// <summary>
//    /// Поле для хранения источника токена отмены.
//    /// </summary>
//    private CancellationTokenSource? _CancellationTokenSource;

//    ///// <summary>
//    ///// Асинхронно создаёт соединение.
//    ///// </summary>
//    ///// <param name="cancellationToken">
//    ///// Токен отмены.
//    ///// </param>
//    ///// <returns>
//    ///// Задача, создающая соединение.
//    ///// </returns>
//    //internal abstract Task<Connection> CreateConnectionAsync(CancellationToken cancellationToken);

//    /// <summary>
//    /// Асинхронно создаёт соединение.
//    /// </summary>
//    /// <param name="host">
//    /// DNS-имя удаленного узла.
//    /// </param>
//    /// <param name="port">
//    /// Номер порта удаленного узла.
//    /// </param>
//    /// <param name="name">
//    /// Имя соединения.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, создающая соединение.
//    /// </returns>
//    public static async Task<Join> CreateAsync(
//        string host, int port, string name,
//        CancellationToken cancellationToken)
//    {
//        //  Проверка токена отмены.
//        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

//        //  Создание именнованного соединения.
//        NamedJoin join = new(host, port, name)
//        {
//            //  Создание источника токена отмены.
//            _CancellationTokenSource = new()
//        };

//        //  Получение токена отмены.
//        CancellationToken token = join._CancellationTokenSource.Token;

//        //  Запуск асинхронной задачи.
//        _ = Task.Run(async delegate
//        {
//            //  Запуск основной работы.
//            await join.InvokeAsync(cancellationToken).ConfigureAwait(false);
//        }, CancellationToken.None);

//        //  Возврат именнованного соединения.
//        return join;
//    }

//    /// <summary>
//    /// Асинхронно выполняет основную работу соединения.
//    /// </summary>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая основную работу.
//    /// </returns>
//    private async Task InvokeAsync(CancellationToken cancellationToken)
//    {
//        //  Создание соединения.
//        using Connection connection = await CreateConnectionAsync(cancellationToken).ConfigureAwait(false);

//        _ = this;
//        await Task.Delay(-1, cancellationToken).ConfigureAwait(false);
//    }

//    /// <summary>
//    /// Асинхронно создаёт соединение.
//    /// </summary>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, создающая соединение.
//    /// </returns>
//    private async Task<Connection> CreateConnectionAsync(CancellationToken cancellationToken)
//    {
//        //  TCP-клиент.
//        TcpClient? tcpClient = null;

//        //  Блок перехвата всех исключений.
//        try
//        {

//        }
//        catch
//        {
//            //  Блок перехвата всех исключений.
//            try
//            {
//                //  Проверка TCP-клиента.
//                if (tcpClient is not null)
//                {
//                    //  Блок перехвата всех исключений.
//                    try
//                    {
//                        //  Закрытие соединения.
//                        tcpClient.Close();
//                    }
//                    catch { }

//                    //  Блок перехвата всех исключений.
//                    try
//                    {
//                        //  Разрушение TCP-клиента.
//                        tcpClient.Close();
//                    }
//                    catch { }

//                    //  Блок перехвата всех исключений.
//                    try
//                    {

//                    }
//                    catch { }

//                }
//            }
//            catch { }

//            //  Блок перехвата всех исключений.
//            try
//            {

//            }
//            catch { }

//            //  Блок перехвата всех исключений.
//            try
//            {

//            }
//            catch { }

//            //  Повторный выброс исключения.
//            throw;
//        }

//        ////  Создание подключения.
//        //using TcpClient client = new();

//    }

//    /// <summary>
//    /// Выполняет проверку сертификата.
//    /// </summary>
//    private static bool CertificateValidation(
//        object sender, X509Certificate? certificate, X509Chain? chain,
//        SslPolicyErrors sslPolicyErrors)
//    {
//        //  Проверка.
//        return sslPolicyErrors == SslPolicyErrors.None;
//    }
//}



///////// <summary>
///////// Пердставляет соединение.
///////// </summary>
//////public sealed class Join :
//////    IDisposable
//////{
//////    /// <summary>
//////    /// Поле для хранения источника токена отмены.
//////    /// </summary>
//////    private CancellationTokenSource? _CancellationTokenSource;

//////    /// <summary>
//////    /// Поле для хранения TCP-клиента.
//////    /// </summary>
//////    private TcpClient? _TcpClient;

//////    /// <summary>
//////    /// Поле для хранения TCP-потока.
//////    /// </summary>
//////    private NetworkStream? _TcpStream;

//////    /// <summary>
//////    /// Поле для хранения SSL-потока.
//////    /// </summary>
//////    private SslStream? _SslStream;

//////    /// <summary>
//////    /// Инициализирует новый экземпляр.
//////    /// </summary>
//////    /// <param name="tcpClient">
//////    /// TCP-клиент.
//////    /// </param>
//////    /// <param name="tcpStream">
//////    /// TCP-поток.
//////    /// </param>
//////    /// <param name="sslStream">
//////    /// SSL-поток.
//////    /// </param>
//////    /// <param name="cancellationToken">
//////    /// Токен отмены.
//////    /// </param>
//////    private Join(TcpClient tcpClient, NetworkStream tcpStream, SslStream sslStream, CancellationToken cancellationToken)
//////    {
//////        //  Блок перехвата всех исключений.
//////        try
//////        {
//////            //  Установка полей.
//////            _TcpClient = tcpClient;
//////            _TcpStream = tcpStream;
//////            _SslStream = sslStream;

//////            //  Создание связанного источника токена отмены.
//////            _CancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

//////            //  Запуск асинхронной задачи.
//////            _ = Task.Run(async delegate
//////            {
//////                //  Выполнение основной работы.
//////                await InvokeAsync(_CancellationTokenSource.Token).ConfigureAwait(false);
//////            }, cancellationToken);
//////        }
//////        catch
//////        {
//////            //  Разрушение объекта.
//////            Dispose();

//////            //  Повторный выброс исключения.
//////            throw;
//////        }
//////    }

//////    /// <summary>
//////    /// Разрушает объект.
//////    /// </summary>
//////    public void Dispose()
//////    {
//////        //  Блок перехвата всех исключений.
//////        try
//////        {
//////            //  Получение источника токена отмены.
//////            CancellationTokenSource? cancellationTokenSource = Interlocked.Exchange(ref _CancellationTokenSource, null);

//////            //  Проверка источника токена отмены.
//////            if (cancellationTokenSource is not null)
//////            {
//////                //  Блок перехвата всех исключений.
//////                try
//////                {
//////                    //  Отправка запроса на отмену.
//////                    cancellationTokenSource.Cancel();
//////                }
//////                catch { }

//////                //  Блок перехвата всех исключений.
//////                try
//////                {
//////                    //  Разрушение источника токена отмены.
//////                    cancellationTokenSource.Dispose();
//////                }
//////                catch { }
//////            }
//////        }
//////        catch { }
//////    }

//////    /// <summary>
//////    /// Асинхронно выполняет основную работу.
//////    /// </summary>
//////    /// <param name="cancellationToken">
//////    /// Токен отмены.
//////    /// </param>
//////    /// <returns>
//////    /// Задача, выполняющая основную работу.
//////    /// </returns>
//////    private async Task InvokeAsync(CancellationToken cancellationToken)
//////    {

//////    }

//////    /// <summary>
//////    /// Асинхронно создаёт соединение.
//////    /// </summary>
//////    /// <param name="host">
//////    /// DNS-имя удаленного узла.
//////    /// </param>
//////    /// <param name="port">
//////    /// Номер порта удаленного узла.
//////    /// </param>
//////    /// <param name="name">
//////    /// Имя текущий точки.
//////    /// </param>
//////    /// <param name="cancellationToken">
//////    /// Токен отмены.
//////    /// </param>
//////    /// <returns>
//////    /// Задача, создающая соединение.
//////    /// </returns>
//////    public static async Task<Join> CreateAsync(
//////        string host, int port, string name, CancellationToken cancellationToken)
//////    {
//////        //  Предварительная проверка входных параметров.
//////        IsNotNull(host);
//////        IsNotEmpty(name);

//////        //  Проверка токена отмены.
//////        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

//////        //  TCP-клиент.
//////        TcpClient? tcpClient = null;

//////        //  TCP-поток.
//////        NetworkStream? tcpStream = null;

//////        //  SSL-поток.
//////        SslStream? sslStream = null;

//////        //  Блок перехвата всех исключений.
//////        try
//////        {
//////            //  Создание TCP-клиента.
//////            tcpClient = new();

//////            //  Подключение TCP-клиента.
//////            await tcpClient.ConnectAsync(host, port).ConfigureAwait(false);

//////            //  Получение TCP-потока.
//////            tcpStream = tcpClient.GetStream();

//////            //  Создание SSL-потока.
//////            sslStream = new(tcpStream, false, CertificateValidation);

//////            //  Отправка порции именнованного входа.
//////            await new NamedEntryPortion(name).SaveAsync(sslStream, cancellationToken).ConfigureAwait(false);

//////            //  Получение подтверждения.
//////            if ((await Portion.LoadAsync(sslStream, cancellationToken).ConfigureAwait(false)).Format != PortionFormat.Confirm)
//////                throw new InvalidOperationException("Не удалось создать соединение.");

//////            //  Возврат соединения.
//////            return new(tcpClient, tcpStream, sslStream, cancellationToken);
//////        }
//////        catch
//////        {
//////            //  Блок перехвата всех исключений.
//////            try
//////            {
//////                //  Разрушение SSL-потока.
//////                sslStream?.Dispose();
//////            }
//////            catch { }

//////            //  Блок перехвата всех исключений.
//////            try
//////            {
//////                //  Разрушение TCP-потока.
//////                tcpStream?.Dispose();
//////            }
//////            catch { }

//////            //  Блок перехвата всех исключений.
//////            try
//////            {
//////                //  Разрушение TCP-клиента.
//////                tcpClient?.Dispose();
//////            }
//////            catch { }

//////            //  Повторный выброс исключения.
//////            throw;
//////        }
//////    }

//////}
