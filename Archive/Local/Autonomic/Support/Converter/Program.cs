using System;
using System.Windows.Forms;

[assembly: CLSCompliant(true)]

namespace Converter
{
    /// <summary>
    /// Представляет приложение.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ConverterForm());
        }
    }
}
