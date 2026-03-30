using RailTest.Frames;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RailTest.Border.Converter
{
    /// <summary>
    /// Представляет главное окно приложения.
    /// </summary>
    public partial class ConverterForm : Form
    {
        /// <summary>
        /// Поле для хранения объекта, который используется для синхронизации доступа.
        /// </summary>
        private readonly object _SyncRoot;

        /// <summary>
        /// Поле для хранения значения, определяющего выполняется ли работа.
        /// </summary>
        private bool _IsWork;

        /// <summary>
        /// Поле для хранения потока, в котором выполняется работа.
        /// </summary>
        private Thread _Thread;

        /// <summary>
        /// Поле для хранения пути к файлам.
        /// </summary>
        private string _Path;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public ConverterForm()
        {
            InitializeComponent();
            _SyncRoot = new object();
            _IsWork = false;
            _Thread = null;
        }
        
        /// <summary>
        /// Запускает работу.
        /// </summary>
        private void StartButton_Click(object sender, EventArgs e)
        {
            lock (_SyncRoot)
            {
                if (!_IsWork)
                {
                    var dialog = new FolderBrowserDialog();
                    if (dialog.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    _Path = dialog.SelectedPath;
                    _IsWork = true;
                    _StartButton.Enabled = false;
                    _StopButton.Enabled = true;
                    _ProgressBar.Value = 0;
                    _Thread = new Thread(ThreadEntry)
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
                    bool result;
                    try
                    {
                        result = _Thread.Join(1000);
                    }
                    catch (ThreadStateException)
                    {
                        result = false;
                    }
                    if (!result)
                    {
                        try
                        {
                            _Thread.Abort();
                        }
                        catch (SecurityException)
                        {

                        }
                        catch (ThreadStateException)
                        {

                        }
                    }
                    Reset();
                }
            }
        }

        /// <summary>
        /// Сбрасывает состояние всех элементов в начальное состояние.
        /// </summary>
        private void Reset()
        {
            void action()
            {
                _IsWork = false;
                _Thread = null;
                _StartButton.Enabled = true;
                _StopButton.Enabled = false;
            }
            if (InvokeRequired)
            {
                Invoke(new Action(action));
            }
            else
            {
                action();
            }
        }

        /// <summary>
        /// Выполняет работу.
        /// </summary>
        private void ThreadEntry()
        {
            try
            {
                SafeInvoke(() => _RichTextBox.Clear());
                WriteLine($"Выбран каталог: \"{_Path}\".");
                string target = Path.Combine(_Path, "Converter");
                if (!Directory.Exists(target))
                {
                    Directory.CreateDirectory(target);
                }
                var files = new DirectoryInfo(_Path).GetFiles().ToList();
                int count = files.Count;
                if (count != 0)
                {
                    for (int i = 0; i != count; ++i)
                    {
                        if (_IsWork)
                        {
                            var file = files[i];
                            Frame frame;
                            try
                            {
                                frame = new Frame(file.FullName);
                            }
                            catch
                            {
                                frame = null;
                            }
                            if (frame is object)
                            {
                                WriteLine($"Преобразование файла: {file.Name}");
                                string meraName = file.Name.Replace("Vp0_0.", "#");
                                string meraPath = Path.Combine(target, meraName);
                                if (!Directory.Exists(meraPath))
                                {
                                    Directory.CreateDirectory(meraPath);
                                }
                                using (var stream = new FileStream(Path.Combine(meraPath, meraName + ".mera"), FileMode.Create, FileAccess.Write))
                                {
                                    using (var writer = new StreamWriter(stream, Encoding.UTF8, 65536, true))
                                    {
                                        writer.WriteLine("[MERA]");
                                        writer.WriteLine("DataSourceApp=MERA Recorder");
                                        writer.WriteLine("DataSourceVer=3.2.0.7");

                                        writer.WriteLine("Time=00:00:00.000");
                                        writer.WriteLine("Date=01.01.2018");
                                        writer.WriteLine("Test=Test");
                                        writer.WriteLine("Prod=Prod");

                                        int index = 1;
                                        foreach (var channel in frame.Channels)
                                        {
                                            writer.WriteLine();
                                            writer.WriteLine($"[{channel.Name}]");
                                            writer.WriteLine($"Freq={channel.Sampling.ToString("0.000000").Replace(',', '.')}");
                                            writer.WriteLine("XUnits=сек.");
                                            writer.WriteLine("Comment=Comment");
                                            writer.WriteLine($"Address = m11 - 1 - {index++}");

                                            writer.WriteLine("ModSN=00000002");
                                            writer.WriteLine("ModName=MIC1100");
                                            if (!string.IsNullOrEmpty(channel.Unit))
                                            {
                                                writer.WriteLine($"YUnits = {channel.Unit}");
                                            }
                                            else
                                            {
                                                writer.WriteLine($"YUnits = -");
                                            }
                                            writer.WriteLine("Start=0.0000000000");
                                            writer.WriteLine("YFormat=R4");
                                        }
                                    }
                                    stream.Flush();
                                    stream.Close();
                                }
                                foreach (var channel in frame.Channels)
                                {
                                    using (var stream = new FileStream(Path.Combine(meraPath, channel.Name + ".dat"), FileMode.Create, FileAccess.Write))
                                    {
                                        using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
                                        {
                                            for (int j = 0; j < channel.Length; ++j)
                                            {
                                                writer.Write((float)channel[j]);
                                            }
                                        }
                                        stream.Flush();
                                        stream.Close();
                                    }
                                }
                            }
                            SafeInvoke(() => _ProgressBar.Value = 100 * i / count);
                        }
                    }
                }
                SafeInvoke(() => _ProgressBar.Value = 100);
            }
            catch
            {
                
            }
            Reset();
        }

        private void SafeInvoke(Action action)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(action));
            }
            else
            {
                action();
            }
        }

        /// <summary>
        /// Выводит текстовую строку.
        /// </summary>
        /// <param name="value">
        /// Текстовая строка.
        /// </param>
        private void WriteLine(string value)
        {
            void action()
            {
                _RichTextBox.AppendText(value + "\n");
            }
            if (InvokeRequired)
            {
                Invoke(new Action(action));
            }
            else
            {
                action();
            }
        }
    }
}
