using RailTest.Satellite.Autonomic.Registrar;
using RailTest.Satellite.Autonomic.Telemetry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Translator
{
    /// <summary>
    /// Представляет приложение.
    /// </summary>
    class Program
    {
        static void Trace(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Входная точка приложения.
        /// </summary>
        static void Main()
        {
            TcpClient client = null;
            BinaryWriter writer = null;
            MeasuringBuffer measuringBuffer = new MeasuringBuffer();
            const int length = 2 * RailTest.Satellite.Autonomic.Settings.Sampling * RailTest.Satellite.Autonomic.Settings.Duration;
            LocationBuffer locationBuffer = new LocationBuffer();
            DateTime startTime = measuringBuffer.StartTime;
            long lastPosition = measuringBuffer.Position;
            List<Package> packages = new List<Package>();

            new Thread(send) { IsBackground = true }.Start();

            void send()
            {
                while (true)
                {
                    if (client is null)
                    {
                        try
                        {
                            client = new TcpClient();
                            client.Connect(Settings.Address, Settings.TcpPort);
                            writer = new BinaryWriter(client.GetStream(), Encoding.UTF8, true);
                            Trace($"{DateTime.Now} Подключен к серверу.");
                        }
                        catch (Exception ex)
                        {
                            Trace($"{DateTime.Now} Не удалось подключиться к серверу: {ex}");
                            writer = null;
                            client = null;
                        }
                    }
                    else
                    {
                        try
                        {
                            Package package = null;
                            lock (packages)
                            {
                                while (packages.Count > 32)
                                {
                                    packages.RemoveAt(0);
                                }
                                if (packages.Count != 0)
                                {
                                    package = packages[packages.Count - 1];
                                }
                            }
                            if (package is object)
                            {
                                package.Write(writer);
                                writer.Flush();
                                if (client.Connected)
                                {
                                    lock (packages)
                                    {
                                        packages.Remove(package);
                                    }
                                }
                                else
                                {
                                    client = null;
                                }
                                Trace($"{DateTime.Now} Отправлен пакет.");
                            }
                        }
                        catch
                        {
                            try
                            {
                                client.Close();
                            }
                            catch
                            {

                            }
                            try
                            {
                                client.Dispose();
                            }
                            catch
                            {

                            }
                            client = null;
                            Trace($"{DateTime.Now} Отключён от сервера.");
                        }
                    }
                    Thread.Sleep(100);
                }
            }

            while (true)
            {
                long position = measuringBuffer.Position;
                if (startTime == measuringBuffer.StartTime)
                {
                    int count = (int)((position - lastPosition + length) % length);
                    if (count >= 1200)
                    {
                        double[,] channels = measuringBuffer.ReadLast(10, 120);
                        double[,] gps = locationBuffer.ReadLast(1);
                        Package package = new Package(DateTime.Now, gps[0, 0], gps[1, 0], gps[2, 0]);
                        for (int i = 0; i != 10; ++i)
                        {
                            for (int j = 0; j != RailTest.Satellite.Autonomic.Settings.CountSignals; ++j)
                            {
                                package.GetChannelData(j)[i] = channels[j, i] - measuringBuffer.GetZero(j);
                            }
                        }
                        packages.Add(package);
                        lastPosition = (lastPosition + 1200) % length;
                    }
                }
                else
                {
                    startTime = measuringBuffer.StartTime;
                    position = measuringBuffer.Position;
                    lastPosition = position;
                }
                Thread.Sleep(100);
            }
        }
    }
}
