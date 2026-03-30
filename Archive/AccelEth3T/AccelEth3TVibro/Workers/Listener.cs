using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет объект, выполняющий прислушивание сети.
/// </summary>
/// <param name="core">
/// Ядро.
/// </param>
public sealed class Listener(Core core) :
    Worker(core)
{
    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    protected override async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Вывод сообщения в журнал.
        Journal.Add("Запуск прослушивания сети.");

        //  Создание средства прослушивания TCP-соединений.
        TcpListener listener = new(IPAddress.Any, 49001);

        //  Запуск средства прослушивания.
        listener.Start();

        //  Основной цикл прослушивания.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Ожидание нового подключения.
            TcpClient client = await listener.AcceptTcpClientAsync(cancellationToken).ConfigureAwait(false);

            //  Создание нового устройства.
            _ = new Recipient(Core, client);
        }
    }
}
