using System;
using System.Windows.Forms;

namespace RailTest.Border.Server
{
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
            try
            {
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Trace", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
    }
}
