using Apeiron.Services.GlobalIdentity.Packets;
using Apeiron.Services.GlobalIdentity.Tunings;
using System.Net;
using System.Net.Sockets;

namespace Apeiron.Services.GlobalIdentity.Workers;

/// <summary>
/// Представляет основной фоновый процесс службы глобальной идентификации,
/// получающий ответы от сервера.
/// </summary>
public class AnswerWorker :
    Worker<AnswerWorker, ClientTuning>
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
    public AnswerWorker(WorkerContext<AnswerWorker, ClientTuning> context) :
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
            //  Создание UDP-клиента для получения ответных пакетов.
            using UdpClient udpClient = new()
            {
                //  Разрешение использовать порт нескольким клиентам.
                ExclusiveAddressUse = false,
            };

            //  Настройка разрешения для UDP-клиента для использования одного адреса.
            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            //  Связывание UDP-клиента с конечной точкой.
            udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, Tuning.Port));

            //  Основной цикл службы.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Получение UDP-датаграммы.
                UdpReceiveResult receiveResult = await udpClient.ReceiveAsync(cancellationToken).ConfigureAwait(false);

                //  Разбор ответного пакета.
                if (AnswerPacket.TryParce(receiveResult.Buffer, out AnswerPacket packet))
                {
                    //  Проверка глобального идентификатора.
                    if (packet.GlobalIdentifier == Tuning.GlobalIdentifier)
                    {
                        //  Определение пути к файлу пакета.
                        string path = Tuning.GetHistoryPacketPath(packet.PacketIdentifier);

                        //  Удаление файла пакета.
                        Invoker.SafeNotCritical(() => File.Delete(path));

                        //  Вывод в журнал диагностического сообщения.
                        Logger.LogDebug("Получен ответ сервера: {identifier}", packet.PacketIdentifier);
                    }
                }
            }
        }
        finally
        {
            //  Ожидание в случае исключения.
            await Task.Delay(Tuning.ExceptionsTimeout, cancellationToken).ConfigureAwait(false);
        }
    }
}
