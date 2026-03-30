using System;

namespace Simargl.AccelEth3T;

/// <summary>
/// Предоставляет точку входа приложения.
/// </summary>
public static class Program
{
    /// <summary>
    /// Представляет точку входа в приложение.
    /// </summary>
    [STAThread]
    public static void Main()
    {
        //  Создание приложения.
        App application = new();

        //  Создание главного окна.
        MainWindow window = new();

        //  Отображение главного окна.
        window.Show();

        //  Запуск приложения.
        application.Run();
    }
}
