using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;

namespace RailTest.Border.Server
{
    /// <summary>
    /// Представляет элемент управления, на котором отображаются осцилограммы.
    /// </summary>
    public partial class OscillogramControl : UserControl
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="instance">
        /// Экземпляр приложения.
        /// </param>
        public OscillogramControl(Instance instance)
        {
            Instance = instance;
            InitializeComponent();

            Type type = _ListView.GetType();
            PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            propertyInfo.SetValue(_ListView, true, null);

            Instance.Started += Instance_Started;
            Instance.Stopped += Instance_Stopped;
        }

        /// <summary>
        /// Возвращает экземпляр приложения.
        /// </summary>
        public Instance Instance { get; }

        /// <summary>
        /// Возвращает диаграмму.
        /// </summary>
        public System.Windows.Forms.DataVisualization.Charting.Chart Chart
        {
            get
            {
                return _Chart;
            }
        }

        /// <summary>
        /// Возвращает коллекцию групп каналов.
        /// </summary>
        public SectionGroupCollection Groups
        {
            get
            {
                return Instance.GetEquipment().Groups;
            }
        }

        /// <summary>
        /// Обрабатывает событие <see cref="Instance.Started"/>.
        /// </summary>
        /// <param name="sender">
        /// Объект, создавший событие.
        /// </param>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        private void Instance_Started(object sender, EventArgs e)
        {
            Action action = () =>
            {
                for (int i = 0; i != Groups.Count; ++i)
                {
                    var listGroup = _ListView.Groups.Add(i.ToString(), $"Сечение №{i + 1}");
                    foreach (Signal signal in Groups[i].Signals)
                    {
                        var listItem = _ListView.Items.Add(signal.Name);
                        listItem.Tag = signal;
                        listItem.Group = listGroup;
                        listItem.SubItems.Add("0");
                    }
                }
                _Chart.Series.Clear();
                _Timer.Enabled = true;
            };
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }

        /// <summary>
        /// Обрабатывает событие <see cref="Instance.Stopped"/>.
        /// </summary>
        /// <param name="sender">
        /// Объект, создавший событие.
        /// </param>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        private void Instance_Stopped(object sender, EventArgs e)
        {
            Action action = () =>
            {
                _ListView.Items.Clear();
                _ListView.Groups.Clear();
                _Chart.Series.Clear();
                _Timer.Enabled = false;
            };
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }

        private readonly object _ChartSync = new object();

        private void ListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            lock (_ChartSync)
            {
                List<Signal> signals = new List<Signal>();
                foreach (ListViewItem item in _ListView.CheckedItems)
                {
                    Signal signal = item.Tag as Signal;
                    if (signal is object)
                    {
                        signals.Add(signal);
                    }
                }
                _Chart.Series.Clear();
                foreach (Signal signal in signals)
                {
                    var series = _Chart.Series.Add(signal.Name);
                    series.Tag = signal;
                    series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                    series.YValuesPerPoint = 32;
                    _LastIndex = Instance.GetEquipment().Groups.BlockIndex;
                }
                _Count = 0;
                _Miliseconds = 0;
            }
        }

        /// <summary>
        /// Поле для хранения последнего прочитанного индеска.
        /// </summary>
        private int _LastIndex;

        /// <summary>
        /// Поле для хранения количества прочитанных блоков.
        /// </summary>
        private int _Count;

        private const int MaxCount = 30;
        private double _Miliseconds = 0;

        private void Timer_Tick(object sender, EventArgs e)
        {
            lock (_ChartSync)
            {
                try
                {
                    if (_Chart.Series.Count > 0)
                    {
                        int index = Instance.GetEquipment().Groups.BlockIndex;

                        while (_LastIndex != index)
                        {
                            double[] data = new double[SectionGroupCollection.BlockSize];
                            ++_Count;
                            foreach (var series in _Chart.Series)
                            {
                                Signal signal = (Signal)series.Tag;
                                var points = series.Points;
                                signal.Read(_LastIndex, data);
                                if (_Count > MaxCount)
                                {
                                    for (int i = 0; i != SectionGroupCollection.BlockSize; ++i)
                                    {
                                        points.RemoveAt(0);
                                    }
                                }
                                double miliseconds = _Miliseconds;
                                for (int i = 0; i != SectionGroupCollection.BlockSize; ++i)
                                {
                                    miliseconds += 0.5;
                                    points.AddXY(miliseconds, data[i]);
                                }
                            }
                            _Miliseconds += 0.5 * 128;
                            _LastIndex = (_LastIndex + 1) % SectionGroupCollection.BlockCount;
                        }

                        double minimum = double.MaxValue;
                        double maximum = double.MinValue;
                        double value;

                        foreach (var series in _Chart.Series)
                        {
                            var points = series.Points;
                            if (points.Count > 0)
                            {
                                value = points.FindMinByValue().YValues[0];
                                if (minimum > value)
                                {
                                    minimum = value;
                                }

                                value = points.FindMaxByValue().YValues[0];
                                if (maximum < value)
                                {
                                    maximum = value;
                                }
                            }
                        }

                        if (minimum == maximum)
                        {
                            minimum -= 1;
                            maximum += 1;
                        }

                        if (minimum < maximum)
                        {
                            _Chart.ChartAreas[0].AxisY.Minimum = minimum;
                            _Chart.ChartAreas[0].AxisY.Maximum = maximum;
                        }

                        minimum = _Miliseconds - MaxCount * SectionGroupCollection.BlockSize * 0.5;
                        maximum = _Miliseconds;
                        if (minimum < maximum)
                        {
                            _Chart.ChartAreas[0].AxisX.Interval = 250;
                            _Chart.ChartAreas[0].AxisX.Minimum = minimum;
                            _Chart.ChartAreas[0].AxisX.Maximum = maximum;
                        }

                    }
                }
                catch
                {

                }
            }

            foreach (ListViewItem item in _ListView.Items)
            {
                Signal signal = item.Tag as Signal;
                var data = signal.LastData;
                item.SubItems[1].Text = data.Average.ToString();
            }

        }
    }
}
