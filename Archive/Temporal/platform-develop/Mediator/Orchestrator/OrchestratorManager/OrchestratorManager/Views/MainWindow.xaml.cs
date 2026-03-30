using Apeiron.Platform.MediatorLibrary;
using Apeiron.Platform.MediatorLibrary.Responce;
using Apeiron.Support;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Windows;

namespace Apeiron.Platform.OrchestratorManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IPEndPoint _IPEndPoint;

        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

           _IPEndPoint = new(MediatorSettings.MediatorServerAddress, MediatorSettings.MediatorServerPort);
        }


        /// <summary>
        /// Проверка соединения с Медиатор сервером.
        /// </summary>
#pragma warning disable VSTHRD100 // Avoid async void methods
        private async void CheckConnectionToMediator_Click(object sender, RoutedEventArgs e)
#pragma warning restore VSTHRD100 // Avoid async void methods
        {
            try
            {
                CommandTextBox.AppendText(nameof(MediatorMethodId.CheckConnectionToMediator) + "\r");

                string? result = await MediatorTcpClient.RemoteMethodCallAsync(_IPEndPoint,
                MediatorMethodId.CheckConnectionToMediator,
                async (spreader, cancellationToken) =>
                {
                    //  Проверка токена отмены.
                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);
                },
                async (spreader, cancellationToken) =>
                {
                    //  Проверка токена отмены.
                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);
                    return await spreader.ReadStringAsync(cancellationToken).ConfigureAwait(false);
                },
                CancellationToken.None);

                OutputTextBox.AppendText(result + "\r");
            }
            catch (Exception ex)
            {
                OutputTextBox.AppendText(ex.ToString() + "\r");
            }
        }

        /// <summary>
        /// Проверка соединения с Оркестратор - сервером.
        /// </summary>
#pragma warning disable VSTHRD100 // Avoid async void methods
        private async void GetHostListFromMediatorServer_Click(object sender, RoutedEventArgs e)
#pragma warning restore VSTHRD100 // Avoid async void methods
        {
            try
            {
                CommandTextBox.AppendText(nameof(MediatorMethodId.GetHostListFromMediatorServer) + "\r");

                List<HostInfo>? result = await MediatorTcpClient.RemoteMethodCallAsync(_IPEndPoint,
                MediatorMethodId.GetHostListFromMediatorServer,
                async (spreader, cancellationToken) =>
                {
                    //  Проверка токена отмены.
                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);
                },
                async (spreader, cancellationToken) =>
                {
                    //  Проверка токена отмены.
                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                    int count = await spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false);
                    List<HostInfo> hostList = new();
                    for (int i = 0; i < count; i++)
                    {
                        hostList.Add(await new HostInfo().LoadAsync(spreader.Stream, cancellationToken).ConfigureAwait(false));
                    }

                    return hostList;
                },
                CancellationToken.None);

                // Вывод результатов в окно вывода.
                if (result is not null)
                {
                    foreach (var item in result)
                    {
                        OutputTextBox.AppendText(item.Hostname + " : " + item.RequestTime + "\r");
                    }
                }
            }
            catch (Exception ex)
            {
                OutputTextBox.AppendText(ex.ToString() + "\r");
            }
        }

#pragma warning disable VSTHRD100 // Avoid async void methods
        private async void Command1_Click(object sender, RoutedEventArgs e)
#pragma warning restore VSTHRD100 // Avoid async void methods
        {
            // Тестовая команда получения имени Оркестратор сервера.
            try
            {
                CommandTextBox.AppendText(nameof(MediatorMethodId.GetHostInfo) + "\r");

                string? result = await MediatorTcpClient.RemoteMethodCallAsync(_IPEndPoint,
                MediatorMethodId.GetHostInfo,
                async (spreader, cancellationToken) =>
                {
                    //  Проверка токена отмены.
                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                    await spreader.WriteStringAsync(Environment.MachineName.ToLower(), cancellationToken).ConfigureAwait(false);
                },
                async (spreader, cancellationToken) =>
                {
                    //  Проверка токена отмены.
                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);
                    return await spreader.ReadStringAsync(cancellationToken).ConfigureAwait(false);
                },
                CancellationToken.None);

                OutputTextBox.AppendText(result + "\r");
            }
            catch (Exception ex)
            {
                OutputTextBox.AppendText(ex.ToString() + "\r");
            }

        }
    }
}
