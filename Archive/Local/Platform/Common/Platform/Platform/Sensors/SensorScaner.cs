using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Apeiron.Platform.Sensors
{
    /// <summary>
    /// Представляет класс сканирования датчиков.
    /// </summary>
    public static class SensorScaner
    {
        /// <summary>
        /// Представляет порт отправителя шировещательного пакета.
        /// </summary>
        private static readonly ushort _TargetPort = 49001;


        /// <summary>
        /// Представляет порт отправителя шировещательного пакета.
        /// </summary>
        private static readonly ushort _CountIteration = 50;


#pragma warning disable VSTHRD002
        /// <summary>
        /// Представляет функцию сканирования датчиков.
        /// </summary>
        /// <param name="token">
        /// Токен отмены.
        /// </param>
        public static void Scan(CancellationToken token)
        {
            //  Создание списка задач
            List<Task> tasks = new();

            //  Цикл по всем интерфейсам
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                //   Цикл по всем адрессам
                foreach (var info in ni.GetIPProperties().UnicastAddresses)
                {
                    //  Проверка что ipv4 и то что не 127.0.0.1
                    if (info.Address.AddressFamily == AddressFamily.InterNetwork 
                        && 
                        !info.Address.Equals(IPAddress.Loopback))
                    {
                        //  Массив IP адреса
                        byte[] ipArray = info.Address.GetAddressBytes();
                        
                        //  Массив маски сети
                        byte[] maskArray = info.IPv4Mask.GetAddressBytes();

                        //  Создание массива IP для broadcast
                        byte[] broadcastArray = new byte[4];

                        //  Логирование
                        // Console.WriteLine($"Address={info.Address}");
                        // Console.WriteLine($"IPv4Mask={info.IPv4Mask}");

                        //  Цикл по массиву IP
                        for (int i = 0; i < 4; i++)
                        {
                            //  Расчет байта broadcast ip
                            broadcastArray[i] = (byte)((ipArray[i] & maskArray[i]) | ((~maskArray[i])));
                        }

                        //  Получение broadcast IP
                        IPAddress broadcastIP = new(broadcastArray);

                        //  Логирование
                        // Console.WriteLine($"broadcastIP={broadcastIP}");

                        //  Отправка широковещательного запроса.
                        Task task = Task.Run(async () =>
                        await SendAsync(broadcastIP,token).ConfigureAwait(false), token);

                        //  Добавление задачи в списко
                        tasks.Add(task);    
                    }
                }
            }
            //  Синхронное ожидание выполнения всех задач
            Task.WaitAll(tasks.ToArray(), token);
        }

#pragma warning restore VSTHRD002
        /// <summary>
        /// Рассылает сообщение серии адресатов.
        /// </summary>
        /// <returns>
        /// Задача.
        /// </returns>
        private static async Task SendAsync(IPAddress broadcastIP , CancellationToken token)
        {
            //  Пересылка данных
            try
            {
                //  Получение IP адреса сервера NMEA
                IPAddress ipAddressSendServer = broadcastIP;
                //IPAddress ipAddressSendServer = IPAddress.Parse("192.168.1.128");

                //  Получение конечной точки сервера NMEA
                IPEndPoint endPoint = new(ipAddressSendServer, _TargetPort);

                //   Создание массива нулевой длинны.
                byte[] data = Array.Empty<byte>();

                //  Создание пакета
                BinPackage package = new((long)FormatPackages.IdentificationUdpPackage, data);

                //  Создание UDP-клиента.
                using UdpClient udpResender = new();

                //  Создание токена отмены
                using var source = new CancellationTokenSource();

                //  Отмена задачи пересылки в случае приостановки 
                source.CancelAfter(200);

                //  Пересылка данных
                await udpResender.SendAsync(package.ToArray(), endPoint, source.Token).ConfigureAwait(false);

                //  Основной цикл.
                for (int i = 0; i < _CountIteration; i++)
                {
                    if (udpResender.Available > 0)
                    {
                        //  Получение UDP-датаграммы.
                        UdpReceiveResult result = await udpResender.ReceiveAsync(token).ConfigureAwait(false);

                        //  Получение пакета.
                        var packageRecv = BinPackage.FromArray(result.Buffer);

                        //  Создание потока памяти
                        using MemoryStream memory = new(packageRecv.Data);

                        //  Создание читателя.
                        using BinaryReader reader = new(memory);

                        //  Чтение типа.
                        ulong type = reader.ReadUInt64();

                        //  Чтение серийного номера.
                        uint serial = reader.ReadUInt32();

                        //  Печать IP
                        Console.WriteLine($"{result.RemoteEndPoint}");

                        //  Продолжение цикла 
                        switch (type)
                        {
                            case 0xA0A0A0A0A0A0A0A0UL:
                                //  Печать типа датчика.
                                Console.WriteLine("TYPE = TEST_PROGRAM");
                                break;
                            case 0x842E783B54D5AA4CUL:
                                //  Печать типа датчика.
                                Console.WriteLine("TYPE = ADXL357");
                                break;
                            case 0xA2758EE46E4B43CFUL:
                                //  Печать типа датчика.
                                Console.WriteLine("TYPE = AD7730");
                                break;
                            default:
                                //  Печать типа датчика.
                                Console.WriteLine("TYPE = UNKNOWN");
                                break;

                        }

                        //  Печать серийного номера.
                        Console.WriteLine($"SERIAL = {serial:X8}");

                        //  Продолжение цикла.
                        continue;
                    }

                    //  Ожидaние.
                    await Task.Delay(100, token).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"{ex.Message}");
                //  Проверка исключения
                if (ex.IsSystem())
                {
                    //  Перенаправление исключения
                    throw;
                }

            }
        }
    }
}
