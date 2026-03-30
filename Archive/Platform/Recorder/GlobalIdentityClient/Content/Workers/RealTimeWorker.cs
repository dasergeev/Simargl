using Apeiron.Services.GlobalIdentity.Packets;
using Apeiron.Services.GlobalIdentity.Tunings;
using System.Net;
using System.Net.Sockets;

namespace Apeiron.Services.GlobalIdentity.Workers;

/// <summary>
/// Представляет основной фоновый процесс службы глобальной идентификации,
/// работающий в режиме реального времени.
/// </summary>
public class RealTimeWorker :
    Worker<RealTimeWorker, ClientTuning>
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
    public RealTimeWorker(WorkerContext<RealTimeWorker, ClientTuning> context) :
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


            //  Ссылка на пакет.
            StatusPacket packet;

            //  Основной цикл работы.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Получение IP-адресов.
                IPAddress[] addresses = await Dns.GetHostAddressesAsync(Tuning.Domain, cancellationToken).ConfigureAwait(false);

                //  Перебор всех адресов.
                foreach (IPAddress address in addresses)
                {
                    //  Запись диагностического сообщения в журнал.
                    Logger.LogDebug("Адрес {address}.", address);
                }

                //  Определение количества отправляемых пакетов.
                int packetCount = Tuning.RealTimePacketCount;

                //  Отправка пакетов без обновления IP-адресов.
                for (int i = 0; i < packetCount && !cancellationToken.IsCancellationRequested; i++)
                {
                    //  Асинхронное выполнение отправки пакета.
                    _ = Task.Run(async () =>
                    {
                        //  Блок перехвата исключений.
                        try
                        {
                            //  Проверка ссылки на пакет полученый UdpTeltonikaWorker.
                            if (UdpTeltonikaWorker.Package is null)
                            {
                                //  Создание пакета, сообщающего состояние, c не значащими значениями геолокации.
                                packet = new(Tuning.GlobalIdentifier)
                                {
                                    Source = StatusPacketSource.RealTime,
                                    Latitude = 0,
                                    Longitude = 0,
                                    Speed = 0,
                                    IsValid = false,
                                };
                            }
                            else
                            {
                                //  Создание пакета, сообщающего состояние, с последними значениями геолокации.
                                packet = new(Tuning.GlobalIdentifier)
                                {
                                    Source = StatusPacketSource.RealTime,
                                    Latitude = UdpTeltonikaWorker.Package.Latitude,
                                    Longitude = UdpTeltonikaWorker.Package.Longitude,
                                    Speed = UdpTeltonikaWorker.Package.Speed,
                                    IsValid = UdpTeltonikaWorker.IsValide,
                                };
                            }

                            //  Получение датаграммы пакета.
                            byte[] datagram = packet.GetDatagram();

                            //  Перебор всех адресов.
                            foreach (IPAddress address in addresses)
                            {
                                //  Создание UDP-клиента.
                                using UdpClient udpClient = new();

                                //  Установка соедиения.
                                udpClient.Connect(address, Tuning.Port);

                                //  Отправка пакета.
                                await udpClient.SendAsync(datagram, cancellationToken).ConfigureAwait(false);
                            }

                            //  Запись диагностического сообщения в журнал.
                            Logger.LogDebug("Отправлена датаграмма.");

                            //  Изменение источника пакета.
                            packet.Source = StatusPacketSource.History;

                            //  Получение датаграммы пакета.
                            datagram = packet.GetDatagram();

                            //  Путь к файлу для сохранения.
                            string path = Tuning.GetHistoryPacketPath(packet.PacketIdentifier);

                            //  Сохранение пакета в файл.
                            await File.WriteAllBytesAsync(path, datagram, cancellationToken).ConfigureAwait(false);
                        }
                        catch (Exception ex)
                        {
                            //  Запись об ошибке в журнал.
                            Logger.LogError(
                                "При записи пакета состояния в файл произошла ошибка: {exception}",
                                ex);

                            //  Повторный выброс исключения.
                            throw;
                        }
                    }, cancellationToken);

                    //  Задержка перед следующей отправкой пакета.
                    await Task.Delay(Tuning.Period, cancellationToken).ConfigureAwait(false);
                }

                //  Задержка перед следующей отправкой пакета.
                await Task.Delay(Tuning.Period, cancellationToken).ConfigureAwait(false);
            }
        }
        finally
        {
            //  Ожидание в случае исключения.
            await Task.Delay(Tuning.ExceptionsTimeout, cancellationToken).ConfigureAwait(false);
        }
    }
}

