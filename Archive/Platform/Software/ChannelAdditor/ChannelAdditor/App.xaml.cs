namespace Apeiron.Platform.Software.ChannelAdditor.UserInterface;

/// <summary>
/// Представляет приложение.
/// </summary>
public partial class App :
    Application
{
    /// <summary>
    /// Представляет точку входа в приложение.
    /// </summary>
    [STAThread]
    public static void Main()
    {
        //  Создание приложения.
        App application = new();

        //  Инициализация основных компонентов.
        application.InitializeComponent();

        //  Запуск приложения.
        application.Run();
    }
}
