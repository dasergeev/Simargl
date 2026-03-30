using System.Text;

namespace Simargl.Embedded.Tunings.UdpClient;

internal class Program
{
    static async Task Main()
    {
        try
        {
            string message = "Hello, UDP!";
            byte[] data = Encoding.UTF8.GetBytes(message);

            data = await SendAsync(data);

            message = Encoding.UTF8.GetString(data);

            Console.WriteLine($"Ответ: {message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine(ex.ToString());
            Console.WriteLine();
        }
        
    }

    static async Task<byte[]> SendAsync(byte[] datagram)
    {
        string server = "127.0.0.1";
        int port = 49004;

        using System.Net.Sockets.UdpClient client = new();
        // Жёстко фиксируем адрес/порт получателя
        client.Connect(server, port);

        // Сообщение для отправки
        byte[] data = datagram;

        // Отправляем пакет
        int length = await client.SendAsync(data, data.Length);

        if (length != data.Length)
        {
            throw new InvalidOperationException("Не удалось отправить данные.");
        }

        // Устанавливаем таймаут ожидания (мс)
        client.Client.ReceiveTimeout = (int)(Timeout.Ticks / TimeSpan.TicksPerMillisecond);

        // Ждём ответ только от того же endpoint (т.к. был вызван Connect)
        var result = await client.ReceiveAsync();

        byte[] responseData = result.Buffer;

        return responseData;
    }

    private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(1);
}
