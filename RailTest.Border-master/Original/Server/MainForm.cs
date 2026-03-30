using System;
using System.Collections;
using System.Windows.Forms;

namespace RailTest.Border.Server
{
    /// <summary>
    /// Представляет главное окно приложения.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Поле для хранения главного элемента управления.
        /// </summary>
        private readonly MainControl _MainControl;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            _MainControl = new MainControl(this)
            {
                Dock = DockStyle.Fill
            };
            _MainPanel.Controls.Add(_MainControl);

            Instance.Started += Instance_Started;
            Instance.Stopped += Instance_Stopped;
        }

        /// <summary>
        /// Возвращает экземпляр приложения.
        /// </summary>
        public Instance Instance
        {
            get
            {
                return _MainControl.Instance;
            }
        }
        
        /// <summary>
        /// Возвращает средство вывода текстовой информации.
        /// </summary>
        public Output Output
        {
            get
            {
                return _MainControl.Output;
            }
        }

        class ListViewItemComparer : IComparer
        {
            private readonly int col;
            public ListViewItemComparer()
            {
                col = 0;
            }
            public ListViewItemComparer(int column)
            {
                col = column;
            }
            public int Compare(object x, object y)
            {
                int returnVal = string.Compare(((ListViewItem)x).SubItems[col].Text,
                    ((ListViewItem)y).SubItems[col].Text);
                return returnVal;
            }
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void StartMenuItem_Click(object sender, EventArgs e)
        {
            Instance.Start();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Instance.Start();
        }

        private void StopMenuItem_Click(object sender, EventArgs e)
        {
            Instance.Stop();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            Instance.Stop();
        }

        private void ZeroButton_Click(object sender, EventArgs e)
        {
            Instance.GetEquipment().Groups.Zero();
        }

        private void ProcessingButton_Click(object sender, EventArgs e)
        {
            _ProcessingButton.Checked = !_ProcessingButton.Checked;
            if (_ProcessingButton.Checked)
            {
                Instance.GetEquipment().Processing.Start();
            }
            else
            {
                Instance.GetEquipment().Processing.Stop();
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
                _ExitMenuItem.Enabled = false;
                _StartMenuItem.Enabled = false;
                _StartButton.Enabled = false;
                _StopMenuItem.Enabled = true;
                _StopButton.Enabled = true;
                _ZeroButton.Enabled = true;
                _ProcessingButton.Enabled = true;
                _ProcessingButton.Checked = false;
                _TimeStatusLabel.Visible = true;
                _TimeStatusLabel.Text = "";
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
                _ExitMenuItem.Enabled = true;
                _StartMenuItem.Enabled = true;
                _StartButton.Enabled = true;
                _StopMenuItem.Enabled = false;
                _StopButton.Enabled = false;
                _ZeroButton.Enabled = false;
                _ProcessingButton.Enabled = false;
                _ProcessingButton.Checked = false;
                _TimeStatusLabel.Visible = false;
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Instance.IsWork)
            {
                MessageBox.Show("Перед закрытием приложения необходимо остановить сервер.",
                    "Закрытие приложения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Instance.Stop();
        }

        private void Timer_Tick_1(object sender, EventArgs e)
        {

        }
    }
}
