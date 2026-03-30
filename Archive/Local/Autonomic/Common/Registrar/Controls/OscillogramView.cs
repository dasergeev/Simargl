using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace RailTest.Satellite.Autonomic.Registrar.Controls
{
    /// <summary>
    /// Представляет элемент управления, отображающий осциллограммы.
    /// </summary>
    public partial class OscillogramView : UserControl
    {
        /// <summary>
        /// Поле для хранения буфера измерений.
        /// </summary>
        readonly MeasuringBuffer _MeasuringBuffer;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public OscillogramView()
        {
            Application.EnableVisualStyles();
            _MeasuringBuffer = new MeasuringBuffer();
            InitializeComponent();
            _ListView.SmallImageList = new ImageList();
            _Chart.Legends[0].Enabled = false;

            for (int i = 0; i != Settings.CountSignals; ++i)
            {
                var series = new Series();
                series.ChartArea = "ChartArea1";
                series.ChartType = SeriesChartType.FastLine;
                series.Legend = "_Legend";
                series.Color = Settings.Colors[i];
                _Chart.Series.Add(series);
            }

            for (int i = 0; i != Settings.CountSignals; ++i)
            {
                var series = _Chart.Series[i];
                series.Name = Settings.ChannelNames[i];

                _ListView.Items.Add(_Chart.Series[i].Name).Tag = series;
                ListViewItem item = _ListView.Items[i];
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
        }

        /// <summary>
        /// Обрабатывает событие таймера.
        /// </summary>
        /// <param name="sender">
        /// Объект, создавший событие.
        /// </param>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            const int count = 600;
            double[,] data = _MeasuringBuffer.ReadLast(count, 10);

            for (int i = 0; i != Settings.CountSignals; ++i)
            {
                double zero = _MeasuringBuffer.GetZero(i);
                var series = _Chart.Series[i];
                series.Points.Clear();
                for (int j = 0; j != count; ++j)
                {
                    series.Points.Add(data[i, j] - zero);
                }
            }
        }

        /// <summary>
        /// Обрабатывает событие изменения состояния флажка элемента списка.
        /// </summary>
        /// <param name="sender">
        /// Объект, создавший событие.
        /// </param>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        private void ListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Tag is Series series)
            {
                series.Enabled = e.Item.Checked;
            }
        }
    }
}
