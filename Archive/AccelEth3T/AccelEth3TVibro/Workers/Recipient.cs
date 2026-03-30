using Simargl.Recording.AccelEth3T;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет объект, получающий даные от устройства.
/// </summary>
/// <param name="core">
/// Ядро.
/// </param>
/// <param name="client">
/// Клиент TCP-соединения.
/// </param>
public sealed class Recipient(Core core, TcpClient client) :
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
        //  Блок перехвата всех исключений.
        try
        {
            //  Блок с гаратированным завершением.
            try
            {
                //  Проверка токена отмены.
                await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

                //  Вывод информации в журнал.
                Journal.Add($"Подключился новый клиент: {client.Client.RemoteEndPoint}.");

                //  Проверка типа соединения.
                if (client.Client.RemoteEndPoint is not IPEndPoint ipEndPoint)
                {
                    //  Завершение работы с подключением.
                    return;
                }

                //  Получение данных адреса.
                byte[] bytes = ipEndPoint.Address.GetAddressBytes();

                //  Проверка данных адреса.
                if (bytes.Length != 4 ||
                    bytes[0] != 192 || bytes[1] != 168 || bytes[2] != 1)
                {
                    //  Завершение работы с подключением.
                    return;
                }

                //  Получение номера устройства.
                int number = bytes[3];

                //  Устройство.
                Device? device = null;

                //  Поиск устройства.
                foreach (Device item in Core.Devices)
                {
                    //  Проверка номера.
                    if (item.Number == number)
                    {
                        //  Установка устройства.
                        device = item;

                        //  Завершение поиска.
                        break;
                    }
                }

                //  Проверка устройства.
                if (device is null)
                {
                    //  Завершение работы с подключением.
                    return;
                }

                //  Вывод информации в журнал.
                Journal.Add($"Подключено устройство: \"{device.Name}\".");

                //  Получение потока.
                using NetworkStream stream = client.GetStream();

                //  Основной цикл чтения.
                while (!cancellationToken.IsCancellationRequested)
                {
                    //  Создание источника токена отмены по таймауту.
                    using CancellationTokenSource timeoutTokenSource = new();

                    //  Создание связанного токена отмены.
                    using CancellationTokenSource linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
                        timeoutTokenSource.Token, cancellationToken);

                    //  Настройка таймаута.
                    timeoutTokenSource.CancelAfter(500);

                    //  Чтение пакета.
                    AccelEth3TDataPackage package = await AccelEth3TDataPackage.LoadAsync(stream, linkedTokenSource.Token).ConfigureAwait(false);

                    //  Добавление пакета в очередь устройства.
                    device.AddPackage(package);
                }
            }
            finally
            {
                //  Разрушение клиента.
                client.Dispose();
            }
        }
        catch { }
    }
}
