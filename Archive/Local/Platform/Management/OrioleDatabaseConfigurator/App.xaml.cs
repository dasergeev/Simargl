using System.Windows;

namespace Apeiron.Oriole.DatabaseConfigurator;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App :
    Application
{
    /// <summary>
    /// Получаем текущий домен и подписываемся на обработку не обработанных исключений.
    /// </summary>
    public App()
    {
        var currentDomain = AppDomain.CurrentDomain;
        currentDomain.UnhandledException += CurrentDomain_UnhandledException;
    }

    /// <summary>
    /// Событие - выводит на экран текст исключения при ошибке программы.
    /// </summary>
    /// <param name="sender">
    /// Объект создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы события.
    /// </param>
    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        Exception? ex = (Exception)e.ExceptionObject;
        MessageBox.Show($"Во время выполенния программы произошло исключение:\n\n\r{ex?.Message}\n\nСтек вызова:\n\r{ex?.StackTrace}\nВнутреннее исключение:\n\r{ex?.InnerException?.Message}", "Ошибка программы", MessageBoxButton.OK, MessageBoxImage.Error);

    }
}
