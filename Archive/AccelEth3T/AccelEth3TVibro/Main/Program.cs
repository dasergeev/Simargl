using System.Windows.Forms;

namespace Simargl.AccelEth3T;

/// <summary>
/// Предоставляет точку входа приложения.
/// </summary>
internal static class Program
{
    /// <summary>
    /// Точка входа приложения.
    /// </summary>
    [STAThread]
    static void Main()
    {
        //  Инициализация приложения.
        ApplicationConfiguration.Initialize();

        //  Запуск приложения.
        Application.Run(new MainForm());
    }
}
