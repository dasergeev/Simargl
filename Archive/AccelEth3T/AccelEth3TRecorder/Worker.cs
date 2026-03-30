using System.Drawing;
using System.Net;
using System.Net.Sockets;

namespace AccelEth3TRecorder;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _Logger;

    public Worker(ILogger<Worker> logger)
    {
        _Logger = logger;
        _ = this;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await ListenerAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _Logger.LogError("{ex}", ex);
            }

            await Task.Delay(100,cancellationToken).ConfigureAwait(false);
        }
    }

    private async Task ListenerAsync(CancellationToken cancellationToken)
    {
        TcpListener listener = new(IPAddress.Any, 49001);

        try
        {
            listener.Start();

            TcpClient client = await listener.AcceptTcpClientAsync(cancellationToken).ConfigureAwait(false);
            _ = Task.Run(async delegate
            {
                await ClientAsync(client, cancellationToken).ConfigureAwait(false);
            }, CancellationToken.None);
        }
        finally
        {
            listener.Stop();
        }
    }

    private async Task ClientAsync(TcpClient tcpClient, CancellationToken cancellationToken)
    {
        const int size = 400 * 636;
        byte[] buffer = new byte[size];
        using TcpClient client = tcpClient;
        if (client.Client.RemoteEndPoint is not IPEndPoint endPoint ||
            endPoint.AddressFamily != AddressFamily.InterNetwork)
        {
            return;
        }
        string address = endPoint.Address.ToString();

        _Logger.LogInformation("Подключился клиент {address}.", address);
        using NetworkStream stream = client.GetStream();

        while (!cancellationToken.IsCancellationRequested)
        {
            await stream.ReadExactlyAsync(buffer, cancellationToken).ConfigureAwait(false);
            DateTime time = DateTime.Now;
            string path = $"C:\\RawData\\Temp\\{time.Year:0000}-{time.Month:00}-{time.Day:00}-{time.Hour:00}\\Adxl-{address}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, $"Adxl-{address}-{time.Year:0000}-{time.Month:00}-{time.Day:00}-{time.Hour:00}-{time.Minute:00}-{time.Second:00}-{time.Millisecond:000}.adxl");
            File.WriteAllBytes(path, buffer);
        }
    }

}
