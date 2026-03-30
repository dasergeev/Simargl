using Simargl.Hardware.Strain.Demo.Main;
using Simargl.Hardware.Strain.Demo.Nodes;
using Simargl.Hardware.Strain.Demo.UI;
using System.Drawing;
using System.Windows;
using System.Windows.Forms.DataVisualization.Charting;

namespace Simargl.Hardware.Strain.Demo.Microservices;

/// <summary>
/// Представляет микросервис управляющий отображением графиков.
/// </summary>
public sealed class ChartManager :
    Microservice
{
    int _ColorIndex = 0;
    readonly List<Color> _Colors = GenerateContrastingDistinctColors(64);
    readonly ChartArea _ChartArea;

    /// <summary>
    /// 
    /// </summary>
    public const double Duration = 25;

    private readonly List<Signal> _Signals = [];

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="heart">
    /// Сердце приложения.
    /// </param>
    /// <param name="chart">
    /// Элемент управления, отображающий графики.
    /// </param>
    [CLSCompliant(false)]
    public ChartManager(Heart heart, TargetChart chart) :
        base(heart)
    {
        //  Обращение к объекту.
        Lay();

        //  Установка элемента управления, отображающего графики.
        Chart = chart;

        _ChartArea = new()
        {
            Name = "ChartArea"
        };
        chart.ChartAreas.Add(_ChartArea);

        _ChartArea.AxisX.Minimum = -Duration;
        _ChartArea.AxisX.Maximum = 0;
        _ChartArea.AxisX.LabelStyle.Format = "00";
        _ChartArea.AxisY.LabelStyle.Format = "0.0000";

        Heart.RootNode.Equipment.Nodes.CollectionChanged += (s, e) =>
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems is not null)
                {
                    foreach (var item in e.NewItems)
                    {
                        if (item is SensorNode node)
                        {
                            foreach (var signal in node.Sensor.Signals)
                            {
                                _Signals.Add(signal);
                                var series = signal.Series;
                                series.ChartArea = _ChartArea.Name;
                                series.ChartType = SeriesChartType.FastLine;
                                series.Color = _Colors[_ColorIndex];
                                series.Name = $"Series{_ColorIndex}";
                                _ColorIndex = (_ColorIndex + 1) % _Colors.Count;
                                Chart.Series.Add(series);

                                signal.ColorImage = CreateSolidColorBitmap(32, 32, series.Color);

                                //series.Points.AddXY(-10, -10);
                                //series.Points.AddXY(0, 0);
                                //series.Points.AddXY(10, 10);
                                //series.Points.AddXY(20, 20);
                            }
                        }
                    }
                }
            }
        };


        /*

            
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            _ToolStrip = new System.Windows.Forms.ToolStrip();
            _RichTextBoxButton = new System.Windows.Forms.ToolStripButton();
            _StoreButton = new System.Windows.Forms.ToolStripButton();
            _ControlButton = new System.Windows.Forms.ToolStripButton();
            _StatusStrip = new System.Windows.Forms.StatusStrip();
            _TreeView = new System.Windows.Forms.TreeView();
            _ImageList = new System.Windows.Forms.ImageList(components);
            _TreeViewSplitter = new System.Windows.Forms.Splitter();
            _Timer = new System.Windows.Forms.Timer(components);
            _RichTextBox = new System.Windows.Forms.RichTextBox();
            _RichTextBoxSplitter = new System.Windows.Forms.Splitter();
            _Chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            _ToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_Chart).BeginInit();
            SuspendLayout();



            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Color = System.Drawing.Color.Maroon;
            series2.Name = "Series2";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series3.Color = System.Drawing.Color.Maroon;
            series3.Name = "Series3";

            _Chart.Series.Add(series2);
            _Chart.Series.Add(series3);
            _Chart.Size = new System.Drawing.Size(604, 271);
            _Chart.TabIndex = 6;
            _Chart.Text = "Графики";

        */
    }

    /// <summary>
    /// Возвращает элемент управления, отображающий графики.
    /// </summary>
    [CLSCompliant(false)]
    public TargetChart Chart { get; }

    /// <summary>
    /// Обновляет кисти.
    /// </summary>
    /// <param name="background">
    /// Фон.
    /// </param>
    /// <param name="foreground">
    /// Передний план.
    /// </param>
    public void UpdateGround(System.Windows.Media.Brush background, System.Windows.Media.Brush foreground)
    {
        //  Получение текущих цветов.
        Color backColor = Chart.BackColor;
        Color foreColor = Chart.ForeColor;

        //  Извлечение цветов.
        if (background is System.Windows.Media.SolidColorBrush backgroundColorBrush &&
            foreground is System.Windows.Media.SolidColorBrush foregroundColorBrush)
        {
            //  Установка цветов.
            backColor = Color.FromArgb(
                backgroundColorBrush.Color.R,
                backgroundColorBrush.Color.G,
                backgroundColorBrush.Color.B);
            foreColor = Color.FromArgb(
                foregroundColorBrush.Color.R,
                foregroundColorBrush.Color.G,
                foregroundColorBrush.Color.B);
        }

        //  Установка цветов.
        Chart.BackColor = backColor;
        Chart.ForeColor = foreColor;

        _ChartArea.BackColor = backColor;


        Color gridColor = Color.FromArgb(
            move(backColor.R, foreColor.R, 0.5f),
            move(backColor.G, foreColor.G, 0.5f),
            move(backColor.B, foreColor.B, 0.5f));


        _ChartArea.AxisX.LineColor = gridColor;
        _ChartArea.AxisX.MajorGrid.LineColor = gridColor;
        _ChartArea.AxisX.MinorGrid.LineColor = gridColor;
        _ChartArea.AxisX.LabelStyle.ForeColor = gridColor;

        _ChartArea.AxisY.LineColor = gridColor;
        _ChartArea.AxisY.MajorGrid.LineColor = gridColor;
        _ChartArea.AxisY.MinorGrid.LineColor = gridColor;
        _ChartArea.AxisY.LabelStyle.ForeColor = gridColor;

        //// Настройка сетки и подписей по оси Y
        //_ChartArea.AxisY.MajorGrid.Enabled = true;
        //_ChartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
        //_ChartArea.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

        //_ChartArea.AxisY.MinorGrid.Enabled = false; // можно включить при необходимости

        //_ChartArea.AxisY.LabelStyle.Enabled = true;
        //_ChartArea.AxisY.LabelStyle.Font = new Font("Arial", 8);
        //_ChartArea.AxisY.LabelStyle.ForeColor = Color.Black;

        static byte move(byte background, byte foreground, float factor)
        {
            int value = (int)(background * (1 - factor) + foreground * factor);
            if (value < 0) return 0;
            if (value > 255) return 255;
            return (byte)value;
        }
    }

    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    public async Task InvokeAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            double minValue = double.MaxValue;
            double maxValue = double.MinValue;
            int count = 0;

            Signal[] signals = await Invoker.InvokeAsync(_Signals.ToArray, cancellationToken);
            foreach (Signal signal in signals)
            {

                List<DataPoint> newPoints = [];
                using (await signal.Lock.LockAsync(cancellationToken))
                {
                    newPoints.AddRange(signal.Points);
                    signal.Points.Clear();
                }

                var seriesPoints = signal.Series.Points;

                DateTime now = DateTime.Now;
                double offset = (now - signal.MoveTime).TotalSeconds;
                signal.MoveTime = now;

                foreach (var point in seriesPoints)
                {
                    point.XValue -= offset;
                }

                var remove = seriesPoints.Where(x => x.XValue < -Duration).ToList();

                await Invoker.InvokeAsync(delegate
                {
                    Chart.SuspendLayout();
                    foreach (var item in remove)
                    {
                        seriesPoints.Remove(item);
                    }
                    foreach (var item in newPoints)
                    {
                        seriesPoints.Add(item);
                    }
                }, cancellationToken).ConfigureAwait(false);

                if (signal.IsChecked ?? false)
                {
                    minValue = Math.Min(minValue, seriesPoints.Min(x => x.YValues[0]));
                    maxValue = Math.Max(maxValue, seriesPoints.Max(x => x.YValues[0]));
                    ++count;
                }
            }

            if (count == 0)
            {
                minValue = 0;
                maxValue = 1;
            }

            double range = maxValue - minValue;
            if (range <= 0.1)
            {
                range = 0.1;
            }
            else
            {
                range *= 0.1;
            }

            minValue -= range;
            maxValue += range;

            await Invoker.InvokeAsync(delegate
            {
                _ChartArea.AxisY.Minimum = minValue;
                _ChartArea.AxisY.Maximum = maxValue;
                Chart.ResumeLayout();
                Chart.Invalidate();
            }, cancellationToken).ConfigureAwait(false);

            await Task.Delay(50, cancellationToken).ConfigureAwait(false);
        }
    }

    static List<Color> GenerateContrastingDistinctColors(int count)
    {
        var colors = new List<Color>(count);

        double saturation = 0.8; // высокая насыщенность
        double value = 0.75;     // средняя яркость для хорошего контраста

        for (int i = 0; i < count; i++)
        {
            double hue = (360.0 / count) * i; // равномерное распределение по кругу
            Color color = ColorFromHSV(hue, saturation, value);
            colors.Add(color);
        }

        var result = new List<Color>(count);
        Random random = new(1);
        while (colors.Count > 0)
        {
            int index = random.Next(0, colors.Count);
            result.Add(colors[index]);
            colors.RemoveAt(index);
        }

        return result;
    }

    // HSV to RGB
    static Color ColorFromHSV(double hue, double saturation, double value)
    {
        int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
        double f = hue / 60 - Math.Floor(hue / 60);

        value = value * 255;
        int v = Convert.ToInt32(value);
        int p = Convert.ToInt32(value * (1 - saturation));
        int q = Convert.ToInt32(value * (1 - f * saturation));
        int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

        return hi switch
        {
            0 => Color.FromArgb(v, t, p),
            1 => Color.FromArgb(q, v, p),
            2 => Color.FromArgb(p, v, t),
            3 => Color.FromArgb(p, q, v),
            4 => Color.FromArgb(t, p, v),
            5 => Color.FromArgb(v, p, q),
            _ => Color.Black
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    public static System.Windows.Media.Imaging.BitmapSource CreateSolidColorBitmap(int width, int height, System.Drawing.Color color)
    {
        var pixels = new byte[width * height * 4]; // BGRA

        for (int i = 0; i < width * height; i++)
        {
            pixels[i * 4 + 0] = color.B; // Blue
            pixels[i * 4 + 1] = color.G; // Green
            pixels[i * 4 + 2] = color.R; // Red
            pixels[i * 4 + 3] = color.A; // Alpha
        }
        

        var bitmap = System.Windows.Media.Imaging.BitmapSource.Create(
            width,
            height,
            96, // dpiX
            96, // dpiY
            System.Windows.Media.PixelFormats.Bgra32,
            null,
            pixels,
            width * 4 // stride = bytes per row
        );

        return bitmap;
    }
}
