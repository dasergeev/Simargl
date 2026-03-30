using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;

namespace Simargl.Synergy.Hub;

partial class Worker
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
    private async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Вывод информации в журнал.
        _Logger.LogInformation("Запуск службы.");
        _Logger.LogInformation("Номер порта для подключения к службе: {port}", _Tunings.Port);

        //  Создание средства прослушивания сети.
        TcpListener listener = new(IPAddress.Any, _Tunings.Port);

        //  Блок с гарантированным завершением.
        try
        {
            //  Запуск средства прослушивания сети.
            listener.Start();

            //  Вывод информации в журнал.
            _Logger.LogInformation("Средство прослушивания сети запущено.");

            //  Основной цикл работы.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Ожидание подключения клиента.
                TcpClient client = await listener.AcceptTcpClientAsync(cancellationToken).ConfigureAwait(false);

                //  Запуск асинхронной задачи.
                _ = Task.Run(async delegate
                {
                    //  Работа с клиентом.
                    await TcpClientAsync(client, cancellationToken).ConfigureAwait(false);
                }, CancellationToken.None);
            }
        }
        finally
        {
            try
            {
                //  Остановка средства прослушивания сети.
                listener.Stop();
            }
            catch { }
        }
    }
}
