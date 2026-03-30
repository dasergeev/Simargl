using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using RailTest.Algebra;

namespace RailTest.Border.Server
{
    /// <summary>
    /// Представляет элемент управления, отображающий информацию об оси.
    /// </summary>
    public partial class AxisControl : UserControl
    {
        /// <summary>
        /// Поле для хранения полного размера.
        /// </summary>
        private readonly int _FullSize;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="axis">
        /// Ось, информацию о которой отображает элемент управления.
        /// </param>
        public AxisControl(Axis axis)
        {
            Axis = axis;
            InitializeComponent();
            _FullSize = Height;
            Height = 32;

            Type type = _ListView.GetType();
            PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            propertyInfo.SetValue(_ListView, true, null);

            foreach (ListViewItem item in _ListView.Items)
            {
                item.SubItems.Add("-");
                item.SubItems.Add("-");
                item.SubItems.Add("-");
                item.SubItems.Add("-");
                item.SubItems.Add("-");
                item.SubItems.Add("-");
                item.SubItems.Add("-");
                item.SubItems.Add("-");
                item.SubItems.Add("-");
            }

            _СomboBox.SelectedIndex = 0;
            UpdateLabel();
        }

        /// <summary>
        /// Возвращает ось, информацию о которой отображает элемент управления.
        /// </summary>
        public Axis Axis { get; }

        /// <summary>
        /// Обновляет надпись.
        /// </summary>
        private void UpdateLabel()
        {
            _Label.Text = $"Ось {Axis.Number} (количество сечений: {Axis.CountSections})";
        }

        private void _Timer_Tick(object sender, EventArgs e)
        {
            UpdateLabel();
            string format = "0.00";

            for (int i = 0; i != 21; ++i)
            {
                AxisSectionInfo info = Axis.SectionsInfo[i];
                if (!ReferenceEquals(info, null))
                {
                    ListViewItem item = _ListView.Items[i];
                    item.SubItems[1].Text = info.Time.ToString(format);
                    if (!double.IsNaN(info.Speed))
                    {
                        item.SubItems[2].Text = (info.Speed * 3.6).ToString(format);
                    }
                    if (!double.IsNaN(info.P[_СomboBox.SelectedIndex]))
                    {
                        item.SubItems[3].Text = info.P[_СomboBox.SelectedIndex].ToString(format);
                    }
                    if (!double.IsNaN(info.F[_СomboBox.SelectedIndex]))
                    {
                        item.SubItems[4].Text = info.F[_СomboBox.SelectedIndex].ToString(format);
                    }
                    if (!double.IsNaN(info.M[_СomboBox.SelectedIndex]))
                    {
                        item.SubItems[5].Text = (info.M[_СomboBox.SelectedIndex] / 1000).ToString(format);
                    }
                    if (!double.IsNaN(info.P2Mean[_СomboBox.SelectedIndex]))
                    {
                        item.SubItems[6].Text = info.P2Mean[_СomboBox.SelectedIndex].ToString(format);
                    }
                    if (!double.IsNaN(info.P2Min[_СomboBox.SelectedIndex]))
                    {
                        item.SubItems[7].Text = info.P2Min[_СomboBox.SelectedIndex].ToString(format);
                    }
                    if (!double.IsNaN(info.P2Max[_СomboBox.SelectedIndex]))
                    {
                        item.SubItems[8].Text = info.P2Max[_СomboBox.SelectedIndex].ToString(format);
                    }
                    if (!double.IsNaN(info.P2Sko[_СomboBox.SelectedIndex]))
                    {
                        item.SubItems[9].Text = info.P2Sko[_СomboBox.SelectedIndex].ToString(format);
                    }
                }
            }

            for (int i = 2; i != 10; ++i)
            {
                List<double> values = new List<double>();
                for (int j = 0; j != 21; ++j)
                {
                    string text = _ListView.Items[j].SubItems[i].Text;
                    if (text != "-")
                    {
                        values.Add(double.Parse(text));
                    }
                }
                if (values.Count > 3)
                {
                    RealVector vector = new RealVector(values.Count);
                    for (int j = 0; j != values.Count; ++j)
                    {
                        vector[j] = values[j];
                    }
                    _ListView.Items[21].SubItems[i].Text = vector.Average.ToString(format);
                    _ListView.Items[22].SubItems[i].Text = vector.Min.ToString(format);
                    _ListView.Items[23].SubItems[i].Text = vector.Max.ToString(format);
                    _ListView.Items[24].SubItems[i].Text = vector.StandardDeviation.ToString(format);
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (Height == _FullSize)
            {
                Height = 32;
                _Button.Text = "Развернуть";
            }
            else
            {
                Height = _FullSize;
                _Button.Text = "Свернуть";
            }
        }
    }
}
