using RailTest.Satellite.Autonomic.Logging;
using RailTest.Satellite.Autonomic.Services;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace RailTest.Satellite.Autonomic.Registrar
{
    /// <summary>
    /// Представляет службу для работы с тельтоникой.
    /// </summary>
    public class TeltonikaService : AutonomicService
    {
        /// <summary>
        /// Поле для хранения состояния системы.
        /// </summary>
        readonly SystemState _SystemState;

        /// <summary>
        /// Поле для хранения скорости.
        /// </summary>
        double _Speed;

        /// <summary>
        /// Поле для хранения долготы.
        /// </summary>
        double _Longitude;

        /// <summary>
        /// Поле для хранения широты.
        /// </summary>
        double _Latitude;

        /// <summary>
        /// Поле для хранения объекта, который используется для синхронизации доступа.
        /// </summary>
        object _SyncRoot;

        /// <summary>
        /// Поле для хранения буфера местоположений.
        /// </summary>
        LocationBuffer _LocationBuffer;

        /// <summary>
        /// Поле для хранения времени получения последнего пакета.
        /// </summary>
        DateTime _LastTime;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public TeltonikaService() :
            base(ServiceID.Teltonika)
        {
            _SystemState = new SystemState();
        }

        /// <summary>
        /// Выполняет основную работу.
        /// </summary>
        protected override void Invoke()
        {
            _Speed = 0;
            _Longitude = 0;
            _Latitude = 0;
            _SyncRoot = new object();
            _LocationBuffer = new LocationBuffer();
            _LocationBuffer.Reset();
            _LastTime = DateTime.Now;

            new Thread(ThreadEntry) { IsBackground = true }.Start();

            UdpClient client = new UdpClient(Settings.GpsPort);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(Settings.TeltonikaIPAddress), Settings.GpsPort);
            while (IsWork)
            {
                try
                {
                    byte[] bytes = client.Receive(ref endPoint);
                    lock (_SyncRoot)
                    {
                        _LastTime = DateTime.Now;
                    }
                    using (var stream = new MemoryStream(bytes))
                    {
                        using (var reader = new StreamReader(stream, Encoding.ASCII))
                        {
                            string text = reader.ReadLine();
                            var message = NmeaMessage.Parse(text);
                            if (message is GgaMessage ggaMessage)
                            {
                                lock (_SyncRoot)
                                {
                                    _Latitude = ggaMessage.Latitude;
                                    _Longitude = ggaMessage.Longitude;
                                }
                            }
                            if (message is RmcMessage rmcMessage)
                            {
                                lock (_SyncRoot)
                                {
                                    _Latitude = rmcMessage.Latitude;
                                    _Longitude = rmcMessage.Longitude;
                                }
                            }
                            if (message is VtgMessage vtgMessage)
                            {
                                lock (_SyncRoot)
                                {
                                    _Speed = vtgMessage.Speed;
                                }
                            }
                        }
                    }
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// Точка входа дополнительного потока.
        /// </summary>
        void ThreadEntry()
        {
            Thread.Sleep(1000);
            DateTime time = DateTime.Now;
            while (IsWork)
            {
                try
                {
                    if ((DateTime.Now - time).TotalSeconds > 1)
                    {
                        double speed = 0;
                        double longitude = 0;
                        double latitude = 0;
                        lock (_SyncRoot)
                        {
                            if ((DateTime.Now - _LastTime).TotalSeconds < 5)
                            {
                                speed = _Speed;
                                longitude = _Longitude;
                                latitude = _Latitude;
                            }
                        }
                        _SystemState.Speed = speed;
                        _SystemState.Longitude = longitude;
                        _SystemState.Latitude = latitude;
                        _LocationBuffer.Write(speed, longitude, latitude);
                        Logger.WriteLine($"({latitude:0.0000}, {longitude:0.0000}), V = {speed:0.00}");
                        time = time.AddSeconds(1);
                    }
                }
                catch
                {

                }
                Thread.Sleep(250);
            }
        }
    }
}
