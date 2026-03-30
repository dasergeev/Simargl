using System;
using System.Drawing;
using System.Windows.Forms;

namespace RailTest.Border.Support.Analysis
{
    /// <summary>
    /// Представляет главное окно приложения.
    /// </summary>
    public partial class AnalysisForm : Form
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public AnalysisForm()
        {
            InitializeComponent();
            Output = _OutputView.Output;

            _WorkersBox.Items.Add(new Preparation(Output));
            _WorkersBox.Items.Add(new Processing(Output));
            _WorkersBox.Items.Add(new StaticCalibration(Output));

            _WorkersBox.SelectedIndex = 2;
        }

        /// <summary>
        /// Возвращает средство вывода текстовой информации.
        /// </summary>
        public Output Output { get; }

        /// <summary>
        /// Запускает обработку.
        /// </summary>
        /// <param name="sender">
        /// Объект, создавший событие.
        /// </param>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        private void StartButton_Click(object sender, System.EventArgs e)
        {
            if (_WorkersBox.SelectedItem is Worker worker)
            {
                worker.Started += Worker_Started;
                worker.Stopped += Worker_Stopped;
                worker.Failed += Worker_Failed;
                worker.Start();
            }
        }

        /// <summary>
        /// Останавливает обработку.
        /// </summary>
        /// <param name="sender">
        /// Объект, создавший событие.
        /// </param>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        private void StopButton_Click(object sender, System.EventArgs e)
        {
            if (_WorkersBox.SelectedItem is Worker worker)
            {
                worker.Stop();
            }
        }

        private void Worker_Started(object sender, System.EventArgs e)
        {
            Invoke(new Action(() =>
            {
                _StartButton.Enabled = false;
                _StopButton.Enabled = true;
                _WorkersBox.Enabled = false;
            }));
        }

        private void Worker_Stopped(object sender, System.EventArgs e)
        {
            if (sender is Worker worker)
            {
                worker.Started -= Worker_Started;
                worker.Stopped -= Worker_Stopped;
                worker.Failed -= Worker_Stopped;
            }
            Invoke(new Action(() =>
            {
                _StartButton.Enabled = true;
                _StopButton.Enabled = false;
                _WorkersBox.Enabled = true;
            }));
        }

        private void Worker_Failed(object sender, EventArgs e)
        {
            if (sender is Worker worker)
            {
                var output = Output.CreateSubOutput();
                output.WriteLine(Color.DarkRed, "Произошло исключение:");
                output.TabLevelUp();
                output.WriteLine(worker.Exception.ToString());
                output.TabLevelDown();
                output.Flush();
            }
            Worker_Stopped(sender, e);
        }

    }
}
