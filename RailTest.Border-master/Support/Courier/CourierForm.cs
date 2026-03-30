using RailTest.Algebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace RailTest.Border.Support.Courier
{
    /// <summary>
    /// Представляет главное окно приложения.
    /// </summary>
    public partial class CourierForm : Form
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public CourierForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Возвращает средство вывода текстовой информации.
        /// </summary>
        public Output Output => _OutputView.Output;

        private void PerformerButton_Click(object sender, EventArgs e)
        {
            var sourcePath = @"E:\Source Code\Compile\";
            var targetPath = @"\\192.168.42.6\Compile\";
            var files = new List<string>
            {
                //@"Binary\BorderKernel.dll",
                @"Binary\RailTest.dll",
                @"Binary\RailTest.Algebra.dll",
                @"Binary\RailTest.Frames.dll",
                @"Binary\RailTest.Border.dll",
                @"Binary\RailTest.Border.Module.dll",
                @"Binary\RailTest.Border.Server.exe",
                //@"Binary\RailTest.Border.Control.exe",
                //@"Binary\RailTest.Border.Support.Explorer.exe",
            };
            foreach (var file in files)
            {
                var sourceFile = sourcePath + file;
                var targetFile = targetPath + file;
                try
                {
                    File.Copy(sourceFile, targetFile, true);
                    Output.WriteLine(Color.DarkGreen, $"{file}: успешно.");
                }
                catch (Exception ex)
                {
                    Output.WriteLine(Color.DarkRed, $"{file}: ошибка ({ex.Message}).");
                }
            }
        }

        private void TestingButton_Click(object sender, EventArgs e)
        {
            //Vector<double> left = new Vector<double>(3);
            //Vector<double> right = new Vector<double>(3);
            //left[0] = 1;
            //left[1] = 2;
            //left[2] = 3;
            //right[0] = 4;
            //right[1] = 5;
            //right[2] = 6;

            //Output.WriteLine(left + right);
        }

        private void GetInfo<T>()
        {
            Type type = typeof(T);
            var members = type.GetMembers();
            foreach (var member in members)
            {
                if (member.MemberType == MemberTypes.Method)
                {
                    Output.WriteLine($"{member}: {member.MemberType}");
                }
            }
        }
    }
}
