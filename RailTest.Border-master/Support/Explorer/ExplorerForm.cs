using RailTest.Frames;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RailTest.Border.Support.Explorer
{
    /// <summary>
    /// Представляет главное окно приложения.
    /// </summary>
    public partial class ExplorerForm : Form
    {
        /// <summary>
        /// Путь к исходным фалам.
        /// </summary>
        public const string Source = @"C:\Source";

        /// <summary>
        /// Путь к файлам для исследования.
        /// </summary>
        public const string Explorer = @"C:\Explorer";

        /// <summary>
        /// Поле для хранения значения, определяющего выполняется ли работа.
        /// </summary>
        private bool _IsWork;

        /// <summary>
        /// Поле для хранения потока.
        /// </summary>
        private Thread _Thread;

        /// <summary>
        /// Поле для хранения объекта синхронизации.
        /// </summary>
        private readonly object _SyncRoot;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public ExplorerForm()
        {
            InitializeComponent();
            _IsWork = false;
            _Thread = null;
            _SyncRoot = new object();
            Output = _OutputView.Output;
        }

        /// <summary>
        /// Возвращает средство вывода текстовой информации.
        /// </summary>
        public Output Output { get; }

        /// <summary>
        /// Входная точка потока.
        /// </summary>
        private void EntryPoint()
        {
            try
            {
                var files = new DirectoryInfo(Source).GetFiles().ToList();
                int count = files.Count;
                if (count != 0)
                {
                    double factor = 100.0 / count;
                    for (int i = 0; i != count; ++i)
                    {
                        if (_IsWork)
                        {
                            var file = files[i];
                            Frame frame = null;
                            try
                            {
                                frame = new Frame(file.FullName);
                            }
                            catch
                            {

                            }
                            if (frame is object)
                            {
                                bool interesting = false;
                                foreach (var channel in frame.Channels)
                                {
                                    if (channel.Name.Contains("Counter"))
                                    {
                                        if (channel.Max > 0.1)
                                        {
                                            interesting = true;
                                            break;
                                        }
                                    }
                                }
                                if (interesting)
                                {
                                    File.Copy(file.FullName, file.FullName.Replace(Source, Explorer));
                                }
                            }
                            {
                                int progress = (int)(i * factor);
                                if (progress > 100)
                                {
                                    progress = 100;
                                }
                                Invoke(new Action(() => _ProgressBar.Value = progress));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var output = Output.CreateSubOutput();
                output.WriteLine(Color.DarkRed, "Произошло исключение:");
                output.TabLevelUp();
                output.WriteLine(ex.ToString());
                output.TabLevelDown();
                output.Flush();
            }
            Invoke(new Action(() =>
            {
                _StartButton.Enabled = true;
                _StopButton.Enabled = false;
            }));
            _IsWork = false;
        }

        /// <summary>
        /// Начинает работу.
        /// </summary>
        private void StartButton_Click(object sender, EventArgs e)
        {
            lock (_SyncRoot)
            {
                if (!_IsWork)
                {
                    _IsWork = true;
                    _ProgressBar.Value = 0;
                    _StartButton.Enabled = false;
                    _StopButton.Enabled = true;
                    _Thread = new Thread(EntryPoint)
                    {
                        IsBackground = true,
                        Priority = ThreadPriority.Highest
                    };
                    _Thread.Start();
                }
            }
        }

        /// <summary>
        /// Останавливает работу.
        /// </summary>
        private void StopButton_Click(object sender, EventArgs e)
        {
            lock (_SyncRoot)
            {
                if (_IsWork)
                {
                    _IsWork = false;
                    try
                    {
                        _Thread?.Join();
                    }
                    catch (ThreadStateException)
                    {

                    }
                    catch (ThreadInterruptedException)
                    {

                    }
                }
            }
        }
    }
}
