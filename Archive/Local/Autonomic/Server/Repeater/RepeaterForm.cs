using RailTest.Fibers;
using RailTest.Satellite.Autonomic.Telemetry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RailTest.Satellite.Autonomic.Server
{
    /// <summary>
    /// Представляет главное окно приложения.
    /// </summary>
    public partial class RepeaterForm : Form
    {
        /// <summary>
        /// Поле для хранения волокна.
        /// </summary>
        private readonly Fiber _Fiber;

        /// <summary>
        /// Поле для хранения волокна обслуживания клиентов.
        /// </summary>
        private readonly Fiber _ClientFiber;

        /// <summary>
        /// Поле для хранения списка регистраторов.
        /// </summary>
        public readonly List<TcpClient> _RecorderClients;

        /// <summary>
        /// Поле для хранения списка клиентов.
        /// </summary>
        public readonly List<TcpClient> _Clients;

        /// <summary>
        /// Поле для хранения счётчика принятых пакетов.
        /// </summary>
        private long _Counter;

        /// <summary>
        /// Поле для хранения полученных пакетов.
        /// </summary>
        private readonly List<Package> _Packages;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public RepeaterForm()
        {
            InitializeComponent();
            _Packages = new List<Package>();
            _RecorderClients = new List<TcpClient>();
            _Clients = new List<TcpClient>();
            _Fiber = new Fiber(FiberEntryPoint, "Слушатель", ThreadPriority.Highest, true);
            _ClientFiber = new Fiber(ClientFiberEntryPoint, "Клиенты", ThreadPriority.Highest, true);
            _Fiber.Start();
            _ClientFiber.Start();
            _Counter = 0;
        }

        /// <summary>
        /// Обновляет счётчик.
        /// </summary>
        private void UpdateCounter()
        {
            void update()
            {
                _CounterLabel.Text = $"{DateTime.Now} Принято пакетов: {_Counter}";
            }
            if (InvokeRequired)
            {
                Invoke(new Action(() => update()));
            }
            else
            {
                update();
            }
        }

        /// <summary>
        /// Происходит при закрытии окна.
        /// </summary>
        protected override void OnClosed(EventArgs e)
        {
            _Fiber.Stop(5000);
            _ClientFiber.Stop(5000);
            base.OnClosed(e);
        }

        /// <summary>
        /// Возвращает средство записи текстовой информации.
        /// </summary>
        public Output Output => _OutputView.Output;

        /// <summary>
        /// Добавляет новый пакет.
        /// </summary>
        /// <param name="package">
        /// Пакет.
        /// </param>
        private void NewPackage(Package package)
        {
            ++_Counter;
            UpdateCounter();
            lock (_Packages)
            {
                _Packages.Add(package);
            }
        }

        /// <summary>
        /// Представляет точку входа волокна, обслуживающего клиентов.
        /// </summary>
        /// <param name="context">
        /// Контекст.
        /// </param>
        private void ClientFiberEntryPoint(FiberContext context)
        {
            try
            {
                TcpListener listener = new TcpListener(IPAddress.Any, Settings.ClientPort);
                listener.Start();
                Output.WriteLine($"{DateTime.Now} Обслуживание клиентов запущено {listener.LocalEndpoint}.");
                try
                {
                    while (context.IsWork)
                    {
                        if (listener.Pending())
                        {
                            try
                            {
                                TcpClient client = listener.AcceptTcpClient();
                                _Clients.Add(client);
                                Output.WriteLine($"{DateTime.Now} Установлена связь с клиентом {client.Client.RemoteEndPoint}.");
                            }
                            catch
                            {

                            }
                        }
                        List<Package> packages = new List<Package>();
                        lock (_Packages)
                        {
                            packages.AddRange(_Packages);
                            _Packages.Clear();
                        }

                        foreach (Package package in packages)
                        {
                            foreach (var client in new List<TcpClient>(_Clients))
                            {
                                try
                                {
                                    ClientLoop(client, package);
                                }
                                catch
                                {
                                    RemoveClient(client);
                                }
                            }
                        }

                        foreach (var client in new List<TcpClient>(_Clients))
                        {
                            try
                            {
                                var count = client.Available;
                                if (count != 0)
                                {
                                    var buffer = new byte[count];
                                    client.GetStream().Read(buffer, 0, count);
                                }
                            }
                            catch
                            {
                                RemoveClient(client);
                            }
                        }

                        Thread.Sleep(10);
                    }
                }
                finally
                {
                    try
                    {
                        listener.Stop();
                    }
                    catch
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Output.WriteLine(ex.ToString());
            }
            Output.WriteLine($"{DateTime.Now} Обслуживание клиентов остановлено.");
        }

        /// <summary>
        /// Представляет точку входа волокна.
        /// </summary>
        /// <param name="context">
        /// Контекст.
        /// </param>
        private void FiberEntryPoint(FiberContext context)
        {
            try
            {
                TcpListener listener = new TcpListener(IPAddress.Any, Settings.TcpPort);
                listener.Start();
                Output.WriteLine($"{DateTime.Now} Сервер запущен.");
                try
                {
                    while (context.IsWork)
                    {
                        if (listener.Pending())
                        {
                            try
                            {
                                TcpClient client = listener.AcceptTcpClient();
                                _RecorderClients.Add(client);
                                Output.WriteLine($"{DateTime.Now} Установлена связь с регистратором {client.Client.RemoteEndPoint}.");
                            }
                            catch
                            {

                            }
                        }
                        foreach (var client in new List<TcpClient>(_RecorderClients))
                        {
                            try
                            {
                                RecorderLoop(client);
                            }
                            catch
                            {

                            }
                        }
                        Thread.Sleep(10);
                    }
                }
                finally
                {
                    try
                    {
                        listener.Stop();
                    }
                    catch
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Output.WriteLine(ex.ToString());
            }
            Output.WriteLine($"{DateTime.Now} Сервер остановлен.");
        }

        /// <summary>
        /// Удаляет регистратора.
        /// </summary>
        /// <param name="client">
        /// Клиент.
        /// </param>
        private void RemoveRecorder(TcpClient client)
        {
            try
            {
                client.Close();
            }
            catch
            {

            }
            _RecorderClients.Remove(client);
            Output.WriteLine($"{DateTime.Now} Связь с регистратором разорвана {client.Client.RemoteEndPoint}.");
        }

        /// <summary>
        /// Обработка регистратора.
        /// </summary>
        /// <param name="client">
        /// Клиент.
        /// </param>
        private void RecorderLoop(TcpClient client)
        {
            if (client.Connected)
            {
                if (client.Available >= Package.Size)
                {
                    using (BinaryReader reader = new BinaryReader(client.GetStream(), Encoding.UTF8, true))
                    {
                        try
                        {
                            Package package = new Package(reader.ReadBytes(Package.Size));
                            if (package.Identifier != Package.StandardIdentifier)
                            {
                                RemoveRecorder(client);
                                return;
                            }
                            NewPackage(package);
                        }
                        catch (Exception ex)
                        {
                            Output.WriteLine(ex.ToString());
                        }
                    }
                }
            }
            else
            {
                RemoveRecorder(client);
            }
        }

        /// <summary>
        /// Обработка клиента.
        /// </summary>
        /// <param name="client">
        /// Клиент.
        /// </param>
        /// <param name="package">
        /// Пакет, который необходимо отправить клиенту.
        /// </param>
        private void ClientLoop(TcpClient client, Package package)
        {
            if (client.Connected)
            {
                using (BinaryWriter writer = new BinaryWriter(client.GetStream(), Encoding.UTF8, true))
                {
                    package.Write(writer);
                }
            }
            else
            {
                RemoveClient(client);
            }
        }

        /// <summary>
        /// Удаляет клиента.
        /// </summary>
        /// <param name="client">
        /// Клиент.
        /// </param>
        private void RemoveClient(TcpClient client)
        {
            try
            {
                Output.WriteLine($"{DateTime.Now} Связь с клиентом разорвана {client.Client.RemoteEndPoint}.");
                client.Close();
            }
            catch
            {

            }
            _Clients.Remove(client);
        }
    }
}
