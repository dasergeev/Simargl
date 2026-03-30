using System.Net;
using System.Net.Sockets;

namespace GeolocationRecorder;

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
            catch
            {

            }

            await Task.Delay(100, cancellationToken).ConfigureAwait(false);
        }
    }

    private async Task ListenerAsync(CancellationToken cancellationToken)
    {
        _ = this;
        using UdpClient client = new (8500);
        IPEndPoint endPoint = new(IPAddress.Parse("192.168.1.1"), 8500);

        while (!cancellationToken.IsCancellationRequested)
        {
            UdpReceiveResult result = await client.ReceiveAsync(cancellationToken).ConfigureAwait(false);
            using var stream = new MemoryStream(result.Buffer);
            using var reader = new StreamReader(stream, Encoding.ASCII);
            string text = await reader.ReadToEndAsync(cancellationToken).ConfigureAwait(false);
            DateTime time = DateTime.Now;
            string path = $"D:\\RawData\\{time.Year:0000}-{time.Month:00}-{time.Day:00}-{time.Hour:00}\\Nmea";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, $"Nmea-{time.Year:0000}-{time.Month:00}-{time.Day:00}-{time.Hour:00}-{time.Minute:00}.nmea");
            File.AppendAllText(path, text);
        }
    }
}
