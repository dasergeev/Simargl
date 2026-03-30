using RailTest.Fibers;
using RailTest.Satellite.Autonomic.Telemetry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RailTest.Satellite.Autonomic
{
    /// <summary>
    /// Представляет клиента.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Поле для хранения волокна.
        /// </summary>
        private readonly Fiber _Fiber;

        /// <summary>
        /// Поле для хранения подготовленных пакетов.
        /// </summary>
        private readonly List<Package> _Packages;

        /// <summary>
        /// Поле для хранения клиента.
        /// </summary>
        private TcpClient _TcpClient;

        /// <summary>
        /// Поле для хранения средства чтения.
        /// </summary>
        private BinaryReader _Reader;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="orioleForm">
        /// Главное окно приложения.
        /// </param>
        public Client(OrioleForm orioleForm)
        {
            OrioleForm = orioleForm;
            _Fiber = new Fiber(FiberEntryPoint, "Клиент", ThreadPriority.Highest, true);
            _Packages = new List<Package>();
        }

        /// <summary>
        /// Возвращает главное окно приложения.
        /// </summary>
        public OrioleForm OrioleForm { get; }

        /// <summary>
        /// Возвращает средство вывода текстовой информации.
        /// </summary>
        public Output Output => OrioleForm.Output;

        /// <summary>
        /// Возвращает время регистратора.
        /// </summary>
        public DateTime Time { get; private set; }

        /// <summary>
        /// Возвращает задержку времени.
        /// </summary>
        public TimeSpan Discrepancy { get; private set; }

        /// <summary>
        /// Добавляет пакет.
        /// </summary>
        /// <param name="package">
        /// Пакет.
        /// </param>
        public void AddPackage(Package package)
        {
            lock (_Packages)
            {
                _Packages.Add(package);
                Time = package.Time;
                Discrepancy = Time - DateTime.Now;
            }
        }

        /// <summary>
        /// Возвращает последние пакеты.
        /// </summary>
        /// <returns>
        /// Коллекция последних пакетов.
        /// </returns>
        public IEnumerable<Package> GetPackages()
        {
            List<Package> packages = new List<Package>();
            lock (_Packages)
            {
                packages.AddRange(_Packages);
                _Packages.Clear();
            }
            return packages;
        }

        /// <summary>
        /// Запускает клиента.
        /// </summary>
        public void Start()
        {
            _Fiber.Start();
        }

        /// <summary>
        /// Останавливает клиента.
        /// </summary>
        public void Stop()
        {
            _Fiber.Stop(5000);
        }

        /// <summary>
        /// Представляет точку входа волокна.
        /// </summary>
        /// <param name="context">
        /// Контекст.
        /// </param>
        private void FiberEntryPoint(FiberContext context)
        {
            while (context.IsWork)
            {
                if (_TcpClient is null)
                {
                    try
                    {
                        _TcpClient = new TcpClient();
                        _TcpClient.Connect(Telemetry.Settings.Address, Telemetry.Settings.ClientPort);
                        _Reader = new BinaryReader(_TcpClient.GetStream(), Encoding.UTF8, true);
                        Output.WriteLine("Подключен к серверу.");
                    }
                    catch (Exception ex)
                    {
                        Output.WriteLine(ex.ToString());
                        _Reader = null;
                        _TcpClient = null;
                    }
                }
                else
                {
                    if (_TcpClient.Connected)
                    {
                        try
                        {
                            if (_TcpClient.Available >= Package.Size)
                            {
                                Package package = new Package(_Reader.ReadBytes(Package.Size));
                                if (package.Identifier != Package.StandardIdentifier)
                                {
                                    throw new InvalidOperationException("Неверные данные.");
                                }
                                AddPackage(package);
                            }
                            else
                            {
                                _TcpClient.GetStream().WriteByte(0);
                            }
                        }
                        catch
                        {
                            try
                            {
                                _TcpClient.Close();
                            }
                            catch
                            {

                            }
                            try
                            {
                                _TcpClient.Dispose();
                            }
                            catch
                            {

                            }
                            _TcpClient = null;
                            Output.WriteLine("Отключён от сервера.");
                        }

                    }
                    else
                    {
                        _TcpClient = null;
                    }
                }
                Thread.Sleep(100);
            }
        }
    }
}
