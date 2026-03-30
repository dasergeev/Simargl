using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace RailTest.Satellite.Autonomic
{
    /// <summary>
    /// Представляет главное окно приложения.
    /// </summary>
    public partial class OrioleForm : Form
    {
        /// <summary>
        /// Поле для хранения буфферов значений каналов.
        /// </summary>
        private readonly SortedDictionary<DateTime, double>[] _Buffers;

        private double _Speed;
        private double _Longitude;
        private double _Latitude;
        readonly ListViewItem _TimeItem;
        readonly ListViewItem _SpeedItem;
        readonly ListViewItem _LongitudeItem;
        readonly ListViewItem _LatitudeItem;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public OrioleForm()
        {
            InitializeComponent();
            Output = _OutputView.Output;
            _ListView.SmallImageList = new ImageList();

            for (int i = 0; i != Settings.CountSignals; ++i)
            {
                var series = new Series();
                series.ChartArea = "ChartArea1";
                series.ChartType = SeriesChartType.FastLine;
                series.Legend = "Legend1";
                series.Color = Settings.Colors[i];
                _Chart.Series.Add(series);
            }


            for (int i = 0; i != Settings.CountSignals; ++i)
            {
                _ListView.Items.Add(Settings.ChannelNames[i]).Tag = _Chart.Series[i];
            }

            //_ListView.Items.Add("UX1").Tag = _Chart.Series[0];
            //_ListView.Items.Add("UY1").Tag = _Chart.Series[1];
            //_ListView.Items.Add("UZ1").Tag = _Chart.Series[2];
            //_ListView.Items.Add("UX2").Tag = _Chart.Series[3];
            //_ListView.Items.Add("UY2").Tag = _Chart.Series[4];
            //_ListView.Items.Add("UZ2").Tag = _Chart.Series[5];
            //_ListView.Items.Add("UYb1").Tag = _Chart.Series[6];
            //_ListView.Items.Add("UYb2").Tag = _Chart.Series[7];
            //_ListView.Items.Add("UXm1").Tag = _Chart.Series[8];
            //_ListView.Items.Add("UYm1").Tag = _Chart.Series[9];
            //_ListView.Items.Add("UZm1").Tag = _Chart.Series[10];
            //_ListView.Items.Add("UXm2").Tag = _Chart.Series[11];
            //_ListView.Items.Add("UYm2").Tag = _Chart.Series[12];
            //_ListView.Items.Add("UZm2").Tag = _Chart.Series[13];
            //_ListView.Items.Add("UYmb1").Tag = _Chart.Series[14];
            //_ListView.Items.Add("UYmb2").Tag = _Chart.Series[15];

            for (int i = 0; i != Settings.CountSignals; ++i)
            {
                ListViewItem item = _ListView.Items[i];
                var series = _Chart.Series[i];
                series.Color = Settings.Colors[i];
                item.Checked = true;
                Image image = new Bitmap(64, 64);
                using (var graphics = Graphics.FromImage(image))
                {
                    graphics.FillRectangle(new SolidBrush(_ListView.BackColor), 0, 0, 64, 64);
                    graphics.FillRectangle(new SolidBrush(series.Color), 8, 8, 48, 48);
                    graphics.Flush();
                }
                _ListView.SmallImageList.Images.Add(image);
                item.ImageIndex = i;
            }

            _Buffers = new SortedDictionary<DateTime, double>[Settings.CountSignals];
            for (int i = 0; i != Settings.CountSignals; ++i)
            {
                _Buffers[i] = new SortedDictionary<DateTime, double>();
            }

            Client = new Client(this);
            Client.Start();
            _Timer.Enabled = true;

            _TimeItem = _ParmsListView.Items.Add("Время");
            _SpeedItem = _ParmsListView.Items.Add("Скорость");
            _LongitudeItem = _ParmsListView.Items.Add("Долгота");
            _LatitudeItem = _ParmsListView.Items.Add("Широта");

            _TimeItem.SubItems.Add("-");
            _SpeedItem.SubItems.Add("-");
            _LongitudeItem.SubItems.Add("-");
            _LatitudeItem.SubItems.Add("-");
        }

        /// <summary>
        /// Возвращает средство записи текстовой информации.
        /// </summary>
        public Output Output { get; }

        /// <summary>
        /// Возвращает клиента.
        /// </summary>
        public Client Client { get; }

        /// <summary>
        /// Происходит при закрытии окна.
        /// </summary>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        protected override void OnClosed(EventArgs e)
        {
            _Timer.Enabled = false;
            Client.Stop();
            base.OnClosed(e);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int packagesCount = 0;
            DateTime currentTime = DateTime.Now + Client.Discrepancy;
            double seconds(DateTime time)
            {
                return 0.1 * ((currentTime - time).Ticks / 1000000);
            }

            foreach (var package in Client.GetPackages())
            {
                ++packagesCount;
                _Speed = package.Speed;
                _Longitude = package.Longitude;
                _Latitude = package.Latitude;

                for (int i = 0; i != Settings.CountSignals; ++i)
                {
                    var values = package.GetChannelData(i);
                    var buffer = _Buffers[i];
                    for (int j = 0; j != 10; ++j)
                    {
                        DateTime key = package.Time.AddMilliseconds(100 * j);
                        if (!buffer.ContainsKey(key))
                        {
                            buffer.Add(key, values[j]);
                        }
                    }
                }
            }
            foreach (var buffer in _Buffers)
            {
                List<DateTime> remove = new List<DateTime>();
                foreach (var time in buffer.Keys)
                {
                    if (seconds(time) > 60)
                    {
                        remove.Add(time);
                    }
                }
                foreach (var time in remove)
                {
                    buffer.Remove(time);
                }
            }

            for (int i = 0; i != Settings.CountSignals; ++i)
            {
                var series = _Chart.Series[i];
                var buffer = _Buffers[i];
                series.Points.Clear();
                foreach (var item in buffer)
                {
                    double second = seconds(item.Key);
                    if (second >= 0.0)
                    {
                        series.Points.AddXY(-second, item.Value);
                    }
                }
            }

            if (packagesCount != 0)
            {
                _TimeItem.SubItems[1].Text = Client.Time.ToString("u").Replace("Z", "");
                _SpeedItem.SubItems[1].Text = _Speed.ToString("0.00");
                _LongitudeItem.SubItems[1].Text = _Longitude.ToString("0.0000");
                _LatitudeItem.SubItems[1].Text = _Latitude.ToString("0.0000");
            }
        }

        private void ListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Tag is Series series)
            {
                series.Enabled = e.Item.Checked;
            }
        }

        private void MapButton_Click(object sender, EventArgs e)
        {
            string longitudeText = _Longitude.ToString("0.0000").Replace(',', '.');
            string latitudeText = _Latitude.ToString("0.0000").Replace(',', '.');

            string link = $"https://yandex.ru/maps/?ll={longitudeText}%2C{latitudeText}&pt={longitudeText}%2C{latitudeText}&z=19";
            System.Diagnostics.Process.Start(link);
        }
    }
}
