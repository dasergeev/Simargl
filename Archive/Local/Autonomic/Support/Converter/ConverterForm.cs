using RailTest;
using RailTest.Fibers;
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

namespace Converter
{
    /// <summary>
    /// Представляет главное окно приложения.
    /// </summary>
    public partial class ConverterForm : Form
    {
        /// <summary>
        /// Требуемая частота дискретизации.
        /// </summary>
        const int Sampling = 300;

        /// <summary>
        /// Шаг времени.
        /// </summary>
        const double DeltaTime = 1.0 / Sampling;

        /// <summary>
        /// Формат строки.
        /// </summary>
        const string Format = "0.00000000";

        /// <summary>
        /// Имя целевого каталога.
        /// </summary>
        const string Target = "Convert";

        /// <summary>
        /// Поле для хранения волокна.
        /// </summary>
        readonly Fiber _Fiber;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public ConverterForm()
        {
            InitializeComponent();
            Output = _OutputView.Output;
            _Fiber = new Fiber(FiberEntryPoint, "Конвертер", ThreadPriority.AboveNormal, true);
            _Fiber.Started += Fiber_Started;
            _Fiber.Stopped += Fiber_Stopped;
            _Fiber.Failed += Fiber_Failed;
        }

        /// <summary>
        /// Возвращает средство вывода текстовой информации.
        /// </summary>
        public Output Output { get; }

        /// <summary>
        /// Точка входа волокна.
        /// </summary>
        /// <param name="context">
        /// Контекст волокна.
        /// </param>
        void FiberEntryPoint(FiberContext context)
        {
            string path = null;
            Invoke(new Action(() =>
            {
                var dialog = new FolderBrowserDialog();
                dialog.SelectedPath = @"E:\OneDrive\Service\Work PC\Desktop\Кривые";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    path = dialog.SelectedPath;
                }
            }));

            if (path is object)
            {
                if (!Directory.Exists(Path.Combine(path, Target)))
                {
                    Directory.CreateDirectory(Path.Combine(path, Target));
                }

                var files = new DirectoryInfo(path).GetFiles();
                var count = files.Length;
                Invoke(new Action(() =>
                {
                    _ProgressBar.Maximum = count;
                }));

                void workWithFile(int index)
                {
                    var output = Output.CreateSubOutput();
                    var file = files[index];
                    output.WriteLine($"Работа с файлом \"{file.Name}\":");
                    output.TabLevelUp();

                    if (!context.IsWork) return;
                    Frame frame = null;
                    try
                    {
                        frame = new Frame(file.FullName);
                    }
                    catch (Exception ex)
                    {
                        while (ex is object)
                        {
                            if (ex is ThreadAbortException)
                            {
                                throw;
                            }
                            ex = ex.InnerException;
                        }
                    }

                    if (frame is object)
                    {
                        int length = int.MaxValue;

                        //  Изменение частоты дискретизации.
                        if (!context.IsWork) return;
                        foreach (var channel in frame.Channels)
                        {
                            channel.Resampling(Sampling);
                            if (length > channel.Length)
                            {
                                length = channel.Length;
                            }
                        }

                        //  Выравнивание длины каналов.
                        if (!context.IsWork) return;
                        foreach (var channel in frame.Channels)
                        {
                            channel.Length = length;
                        }

                        //  Экспорт данных.
                        if (!context.IsWork) return;

                        string[] channelNames = { "Qz1", "Qx1", "Qy1", "Qz2", "Qx2", "Qy2", "V_GPS" };

                        string fileName = file.Name.Substring(0, file.Name.Length - file.Extension.Length) + ".txt";
                        using (var stream = new StreamWriter(Path.Combine(path, Target, fileName)))
                        {
                            for (int i = 0; i != length; ++i)
                            {
                                stream.Write($"{(i * DeltaTime).ToString(Format)}\t");
                                foreach (var name in channelNames)
                                {
                                    var channel = frame.Channels[name];
                                    stream.Write($"{channel[i].ToString(Format)}\t");
                                }
                                stream.WriteLine();
                            }
                        }
                    }
                    else
                    {
                        output.WriteLine("Не является файлом регистрации.");
                    }

                    try
                    {

                    }
                    catch (Exception)
                    {

                        throw;
                    }

                    output.TabLevelDown();
                    Invoke(new Action(() =>
                    {
                        ++_ProgressBar.Value;
                    }));
                    output.Flush();
                }

                Parallel.For(0, count, workWithFile);

                //  Сохранение информации.
                using (var stream = new StreamWriter(Path.Combine(path, "info.txt")))
                {
                    stream.Write("File\tTime\t");
                    foreach (var name in new string[] {
                        "UX1", "UY1", "UZ1", "UX2", "UY2", "UZ2", "UYb1", "UYb2",
                        "UXm1", "UYm1", "UZm1", "UXm2", "UYm2", "UZm2", "UYmb1", "UYmb2",
                        "Speed", "Lon", "Lat"})
                    {
                        stream.Write($"{name}Aver\t");
                        stream.Write($"{name}Dev\t");
                        stream.Write($"{name}Min\t");
                        stream.Write($"{name}Max\t");
                    }
                    stream.WriteLine();
                }
            }
        }

        /// <summary>
        /// Обрабатывает событие нажатия кнопки "Начать".
        /// </summary>
        /// <param name="sender">
        /// Объект, создавший событие.
        /// </param>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        private void StartButton_Click(object sender, EventArgs e)
        {
            try
            {
                _Fiber.Start();
            }
            catch (InvalidOperationException)
            {

            }
        }

        /// <summary>
        /// Обрабатывает событие нажатия кнопки "Остановить".
        /// </summary>
        /// <param name="sender">
        /// Объект, создавший событие.
        /// </param>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        private void StopButton_Click(object sender, EventArgs e)
        {
            _Fiber.Stop(1000);
            Invoke(new Action(() =>
            {
                _StartButton.Enabled = true;
                _StopButton.Enabled = false;
            }));
        }

        /// <summary>
        /// Обрабатывает событие запуска волокна.
        /// </summary>
        /// <param name="sender">
        /// Объект, создавший событие.
        /// </param>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        void Fiber_Started(object sender, EventArgs e)
        {
            Output.WriteLine(Color.DarkGreen, "Запуск.");
            Invoke(new Action(() =>
            {
                _StartButton.Enabled = false;
                _StopButton.Enabled = true;
                _ProgressBar.Value = 0;
            }));
        }

        /// <summary>
        /// Обрабатывает событие остановки волокна.
        /// </summary>
        /// <param name="sender">
        /// Объект, создавший событие.
        /// </param>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        private void Fiber_Stopped(object sender, EventArgs e)
        {
            Output.WriteLine(Color.DarkGreen, "Остановка.");
            Invoke(new Action(() =>
            {
                _StartButton.Enabled = true;
                _StopButton.Enabled = false;
            }));
        }

        /// <summary>
        /// Обрабатывает событие неудачного завершения волокна.
        /// </summary>
        /// <param name="sender">
        /// Объект, создавший событие.
        /// </param>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        private void Fiber_Failed(object sender, EventArgs e)
        {
            var output = Output.CreateSubOutput();
            output.WriteLine(Color.DarkRed, "Ошибка:");
            output.TabLevelUp();
            output.WriteLine(_Fiber.Exception.ToString());
            output.TabLevelDown();
            output.Flush();
            Fiber_Stopped(sender, e);
        }
    }
}
