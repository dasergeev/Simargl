using Apeiron.Services.GlobalIdentity.Packets;
using Apeiron.Services.GlobalIdentity.Tunings;
using System.Net;
using System.Net.Sockets;

namespace Apeiron.Services.GlobalIdentity.Workers;

/// <summary>
/// Представляет основной фоновый процесс службы глобальной идентификации,
/// отправляющий пакеты из истории.
/// </summary>
public class HistoryWorker :
    Worker<HistoryWorker, ClientTuning>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="context">
    /// Контекст фонового процесса службы глобальной идентификации.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="context"/> передана пустая ссылка.
    /// </exception>
    public HistoryWorker(WorkerContext<HistoryWorker, ClientTuning> context) :
        base(context)
    {

    }

    /// <summary>
    /// Ассинхронно выполняет фоновую работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая фоновую работу.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected override async Task InvokeAsync(CancellationToken cancellationToken)
    {
        try
        {

            //  Основной цикл работы.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Получение файлов для отправки.
                string[] files = Directory.GetFiles(Tuning.HistoryPath);

                //  Проверка наличия файлов для отправки.
                if (files.Length != 0)
                {
                    //  Получение IP-адресов.
                    IPAddress[] addresses = await Dns.GetHostAddressesAsync(
                        Tuning.Domain, cancellationToken).ConfigureAwait(false);

                    //  Перебор всех адресов.
                    foreach (IPAddress address in addresses)
                    {
                        //  Запись диагностического сообщения в журнал.
                        Logger.LogDebug("Найден адрес для отправки: {address}.", address);
                    }

                    //  Перебор всех пакетов для отправки.
                    foreach (string file in files)
                    {
                        //  Получение датаграммы пакета.
                        byte[] datagram = File.ReadAllBytes(file);

                        //  Создание пакета, сообщающего состояние.
                        if (StatusPacket.TryParce(datagram, out StatusPacket status))
                        {
                            //  Перебор всех адресов.
                            foreach (IPAddress address in addresses)
                            {
                                //  Создание UDP-клиента.
                                using UdpClient udpClient = new()
                                {
                                    //  Разрешение использовать порт нескольким клиентам.
                                    ExclusiveAddressUse = false,
                                };

                                //  Настройка разрешения для UDP-клиента для использования одного адреса.
                                udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

                                //  Связывание UDP-клиента с конечной точкой.
                                udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, Tuning.Port));

                                //  Установка соедиения.
                                udpClient.Connect(address, Tuning.Port);

                                //  Отправка пакета.
                                await udpClient.SendAsync(datagram, cancellationToken).ConfigureAwait(false);
                            }

                            //  Запись диагностического сообщения в журнал.
                            Logger.LogDebug("Отправлена датаграмма.");
                        }
                        else
                        {
                            //  Удаление файла.
                            Invoker.SafeNotCritical(() => File.Delete(file));
                        }
                    }
                }

                //  Задержка перед следующей отправкой пакета.
                await Task.Delay(Tuning.HistoryTimeout, cancellationToken).ConfigureAwait(false);
            }
        }
        finally
        {
            //  Ожидание в случае исключения.
            await Task.Delay(Tuning.ExceptionsTimeout, cancellationToken).ConfigureAwait(false);
        }
    }
}

