using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RailTest.Satellite.Autonomic.Support
{
    /// <summary>
    /// Представляет главное окно приложения.
    /// </summary>
    public partial class PlacingForm : Form
    {
        /// <summary>
        /// Постоянная для хранения исходного пути.
        /// </summary>
        const string _SourcePath = @"D:\Source Code\Compile\Binary\";

        /// <summary>
        /// Постоянная для хранения пути на разведчике.
        /// </summary>
        const string _ScoutPath = @"\\192.168.1.3\Programs\Release\Binary\";
        //const string _ScoutPath = @"\\192.168.42.31\Programs\Release\Binary\";

        /// <summary>
        /// Постоянная для хранения пути на сервере.
        /// </summary>
        const string _ServerPath = @"\\PROD-01\Apeiron\Autonomic\Binary\";

        /// <summary>
        /// Поле для хранения коллекции общих файлов.
        /// </summary>
        readonly List<string> _CommonFiles = new List<string>()
        {
            "RailTest.dll",
            //"RailTest.Algebra.dll",
            //"RailTest.Frames.dll",
            "RailTest.Satellite.Autonomic.dll",
            "RailTest.Satellite.Autonomic.Telemetry.dll",
        };

        /// <summary>
        /// Поле для хранения коллекции файлов для сервера.
        /// </summary>
        readonly List<string> _ServerFiles = new List<string>()
        {
            "RailTest.Satellite.Autonomic.Server.Repeater.exe",
            "RailTest.Satellite.Autonomic.Oriole.exe"
        };


        /// <summary>
        /// Поле для хранения коллекции файлов для разведчика.
        /// </summary>
        readonly List<string> _ScoutFiles = new List<string>()
        {
            "RailTest.Satellite.Autonomic.Registrar.Assistant.exe",
            "RailTest.Satellite.Autonomic.Registrar.Translator.exe",
            "RailTest.Satellite.Autonomic.Registrar.RecycleBin.exe",

            "RailTest.Satellite.Autonomic.Registrar.QuantumX.exe",
            "RailTest.Satellite.Autonomic.Registrar.Teltonika.exe",
            "RailTest.Satellite.Autonomic.Registrar.Recorder.exe",

            "RailTest.Satellite.Autonomic.Registrar.dll",

            "Gibraltar.Agent.dll",
            "Hbm.Api.Common.dll",
            "Hbm.Api.GenericStreaming.dll",
            "Hbm.Api.Logging.dll",
            "Hbm.Api.Mgc.dll",
            "Hbm.Api.Pmx.dll",
            "Hbm.Api.QuantumX.dll",
            "Hbm.Api.Scan.dll",
            "Hbm.Api.SensorDB.dll",
            "Hbm.Api.Utils.dll",
            "HBM.TEDS.dll",
            "HBM_QX_Framework.dll",
            "HBM_Scan.dll",
            "HBM_Streaming.dll",
            "HBM_Update.dll",
            "HBMTedsDll.dll",
            "IBR.XPDF.dll",
            "ICSharpCode.SharpZipLib.dll",
            "JetBrains.Annotations.dll",
            "Newtonsoft.Json.dll",
            "NLog.dll",
            "VistaDB.5.NET40.dll",
            "XPDF.dll",
            "DeviceDriver.plugins",
        };

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public PlacingForm()
        {
            InitializeComponent();
            Output = _OutputView.Output;

            _ScoutFiles.AddRange(_CommonFiles);
            _ServerFiles.AddRange(_CommonFiles);
        }

        /// <summary>
        /// Возвращает средство вывода текстовой информации.
        /// </summary>
        Output Output { get; }

        private void RegistrarButton_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                Output.Clear();
                Output.WriteLine(Color.DarkBlue, "Копирование файлов на разведчик.");
                foreach (var name in _ScoutFiles)
                {
                    try
                    {
                        File.Copy(_SourcePath + name, _ScoutPath + name, true);
                        Output.WriteLine(Color.DarkGreen, name);
                    }
                    catch (Exception ex)
                    {
                        Output.WriteLine(Color.DarkRed, $"{name}: \"{ex.Message}\"");
                    }
                }
                Output.WriteLine(Color.DarkBlue, "Копирование файлов завершено.");
            });
        }

        private void ServerButton_Click(object sender, EventArgs e)
        {
            Output.Clear();
            Output.WriteLine(Color.DarkBlue, "Копирование файлов на сервер.");
            foreach (var name in _ServerFiles)
            {
                try
                {
                    File.Copy(_SourcePath + name, _ServerPath + name, true);
                    Output.WriteLine(Color.DarkGreen, name);
                }
                catch (Exception ex)
                {
                    Output.WriteLine(Color.DarkRed, $"{name}: {ex.Message}");
                }
            }
        }

        private void TestButton_Click(object sender, EventArgs e)
        {

            Encoding encoding = Encoding.GetEncoding(1251);
            var preamble = encoding.GetPreamble();


            Output.WriteLine($"preamble = {preamble.Length}");


            //char getChar(int code)
            //{
            //    char[] chars = encoding.GetChars(new byte[] { (byte)code });
            //    if (chars.Length > 1)
            //    {
            //        throw new Exception();
            //    }
            //    return chars[0];
            //}

            //string getLabel(char @char)
            //{
            //    string label = ((ushort)@char).ToString("x");
            //    while (label.Length < 4)
            //    {
            //        label = "0" + label;
            //    }
            //    return $"\'\\u{label}\'";
            //}

            //for (int i = 0; i != 64; ++i)
            //{
            //    for (int j = 0; j != 4; ++j)
            //    {
            //        int code = 4 * i + j;
            //        char @char = getChar(code);
            //        string codeText = code.ToString("x");
            //        if (codeText.Length == 1)
            //        {
            //            codeText = "0" + codeText;
            //        }
            //        codeText = "0x" + codeText;

            //        //case '\u0000': return 0;
            //        Output.Write($"{codeText} => {getLabel(@char)},");
            //        if (j != 3)
            //        {
            //            Output.Write(" ");
            //        }
            //    }
            //    Output.WriteLine();
            //}

            

        }
    }
}
