using System;
using System.Windows.Forms;

namespace RailTest.Satellite.Autonomic.Support
{
    /// <summary>
    /// Представляет главное окно приложения.
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
            Application.Run(new PlacingForm());
        }
    }
}
