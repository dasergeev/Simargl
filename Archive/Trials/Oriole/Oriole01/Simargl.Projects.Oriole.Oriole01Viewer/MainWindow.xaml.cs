using Microsoft.EntityFrameworkCore;
using Simargl.Performing;
using Simargl.Projects.Oriole.Oriole01Storage;
using Simargl.Projects.Oriole.Oriole01Storage.Entities;
using System.Collections.Concurrent;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Simargl.Projects.Oriole.Oriole01Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private readonly Node _RootNode;
        private readonly ConcurrentQueue<Action> _Actions;
        private readonly Invoker _Invoker;

        public MainWindow()
        {
            InitializeComponent();
            CancellationToken = ((App)System.Windows.Application.Current).CancellationToken;

            _Actions = [];
            _Invoker = new(_Actions.Enqueue, CancellationToken);
            _RootNode = new("Root");
            _TreeView.ItemsSource = _RootNode.Nodes;
            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromMilliseconds(100),
            };
            timer.Tick += Timer_Tick;
            timer.Start();

            Task.Run(async delegate
            {
                await LoaderAsync(CancellationToken).ConfigureAwait(false);
            });
        }

        public CancellationToken CancellationToken { get; }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            while (_Actions.TryDequeue(out Action? action))
            {
                try
                {
                    action?.Invoke();
                }
                catch { }
            }
        }

        private async Task LoaderAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    using Oriole01StorageContext context = new();
                    IAsyncEnumerable<AdxlFileData> adxlFiles = context
                        .AdxlFiles
                        .Include(x => x.Adxl)
                        .Include(x => x.HourDirectory)
                        .AsAsyncEnumerable();
                    await foreach (AdxlFileData adxlFile in adxlFiles.WithCancellation(cancellationToken))
                    {
                        Node adxlNode = await GetAdxlNodeAsync(adxlFile.Adxl, cancellationToken).ConfigureAwait(false);
                        Node dayNode = await GetDayNodeAsync(adxlNode, adxlFile.HourDirectory, cancellationToken).ConfigureAwait(false);
                        Node hourNode = await GetHourNodeAsync(dayNode, adxlFile.HourDirectory, cancellationToken).ConfigureAwait(false);
                        Node fileNode = await GetFileNodeAsync(hourNode, adxlFile, cancellationToken).ConfigureAwait(false);
                    }
                }
                catch { }

                await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
            }
        }

        private async Task<Node> GetFileNodeAsync(Node hourNode, AdxlFileData adxlFile, CancellationToken cancellationToken)
        {
            DateTime time = new(adxlFile.Timestamp);
            string name = $"{time.Hour:00}-{time.Minute:00}-{time.Second:00}-{time.Millisecond:000}";
            return await GetNodeAsync(hourNode, name, adxlFile, cancellationToken).ConfigureAwait(false);
        }

        private async Task<Node> GetHourNodeAsync(Node dayNode, HourDirectoryData hourDirectory, CancellationToken cancellationToken)
        {
            DateTime time = hourDirectory.GetTime();
            string name = $"{time.Hour:00}:XX";
            return await GetNodeAsync(dayNode, name, null, cancellationToken).ConfigureAwait(false);
        }

        private async Task<Node> GetDayNodeAsync(Node adxlNode, HourDirectoryData hourDirectory, CancellationToken cancellationToken)
        {
            DateTime time = hourDirectory.GetTime();
            string name = $"{time.Year:0000}-{time.Month:00}-{time.Day:00}";
            return await GetNodeAsync(adxlNode, name, null, cancellationToken).ConfigureAwait(false);
        }

        private async Task<Node> GetAdxlNodeAsync(AdxlData data, CancellationToken cancellationToken)
        {
            string ipAddress = data.GetIPAddress();
            string serial = _Serials[ipAddress];
            (int channelIndex, string xName) = _Channels[(serial, "X")];
            string yName = _Channels[(serial, "Y")].Name;
            string zName = _Channels[(serial, "Z")].Name;
            string fullName = $"{channelIndex:00} {xName}, {yName}, {zName}";
            return await GetNodeAsync(_RootNode, fullName, null, cancellationToken).ConfigureAwait(false);
        }

        private async Task<Node> GetNodeAsync(Node parentNode, string name, object? tag, CancellationToken cancellationToken)
        {
            int index = 0;
            foreach (Node node in parentNode.Nodes)
            {
                if (node.Name == name)
                {
                    return node;
                }
                if (string.Compare(node.Name, name) > 0)
                {
                    break;
                }
                ++index;
            }
            Node newNode = new(name)
            {
                Tag = tag,
            };
            await _Invoker.InvokeAsync(delegate
            {
                parentNode.Nodes.Insert(index, newNode);
            }, cancellationToken).ConfigureAwait(false);
            return newNode;
        }

        private static readonly Dictionary<string, string> _Serials = new()
        {
            {"192.168.1.109", "76001121"},
            {"192.168.1.132", "13CC37C1"},
            {"192.168.1.123", "1DB7584F"},
            {"192.168.1.122", "66C9BB8B"},
            {"192.168.1.101", "6B001F4F"},
            {"192.168.1.107", "6B00FD6A"},
            {"192.168.1.108", "7500398F"},
            {"192.168.1.102", "75008AD0"},
            {"192.168.1.104", "7500A4D0"},
            {"192.168.1.105", "7500B1C9"},
            {"192.168.1.120", "7500EE57"},
            {"192.168.1.106", "7500FF3A"},
            {"192.168.1.121", "9991D6F4"},
            {"192.168.1.110", "C9A80663"},
        };

        private static readonly Dictionary<(string Serial, string Name), (int Index, string Name)> _Channels = new()
        {
            {("6B001F4F", "X"), (1, "КX1") },
            {("6B001F4F", "Y"), (1, "КY1") },
            {("6B001F4F", "Z"), (1, "КZ1") },
            {("75008AD0", "Y"), (2, "ТвнX1") },
            {("75008AD0", "X"), (2, "ТвнY1") },
            {("75008AD0", "Z"), (2, "ТвнZ1") },
            {("76001121", "Y"), (3, "ТнX1") },
            {("76001121", "X"), (3, "ТнY1") },
            {("76001121", "Z"), (3, "ТнZ1") },
            {("1DB7584F", "X"), (4, "КX01") },
            {("1DB7584F", "Y"), (4, "КY01") },
            {("1DB7584F", "Z"), (4, "КZ01") },
            {("7500A4D0", "Z"), (5, "ТвнX01") },
            {("7500A4D0", "X"), (5, "ТвнY01") },
            {("7500A4D0", "Y"), (5, "ТвнZ01") },
            {("7500B1C9", "Z"), (6, "RX01") },
            {("7500B1C9", "X"), (6, "RY01") },
            {("7500B1C9", "Y"), (6, "RZ01") },
            {("66C9BB8B", "X"), (7, "КX2") },
            {("66C9BB8B", "Y"), (7, "КY2") },
            {("66C9BB8B", "Z"), (7, "КZ2") },
            {("7500FF3A", "Y"), (8, "ТвнX2") },
            {("7500FF3A", "X"), (8, "ТвнY2") },
            {("7500FF3A", "Z"), (8, "ТвнZ2") },
            {("6B00FD6A", "Z"), (9, "RX2") },
            {("6B00FD6A", "X"), (9, "RY2") },
            {("6B00FD6A", "Y"), (9, "RZ2") },
            {("13CC37C1", "X"), (10, "КX02") },
            {("13CC37C1", "Y"), (10, "КY02") },
            {("13CC37C1", "Z"), (10, "КZ02") },
            {("9991D6F4", "Y"), (11, "ТвнX02") },
            {("9991D6F4", "X"), (11, "ТвнY02") },
            {("9991D6F4", "Z"), (11, "ТвнZ02") },
            {("7500EE57", "Y"), (12, "ТнX02") },
            {("7500EE57", "X"), (12, "ТнY02") },
            {("7500EE57", "Z"), (12, "ТнZ02") },
            {("C9A80663", "Z"), (13, "МХ1") },
            {("C9A80663", "Y"), (13, "МY1") },
            {("C9A80663", "X"), (13, "МZ1") },
            {("7500398F", "Z"), (14, "МХ01") },
            {("7500398F", "Y"), (14, "МY01") },
            {("7500398F", "X"), (14, "МZ01") },
        };
    }
}
