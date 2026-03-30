using Simargl.AccelEth3T.AccelEth3TViewer.Nodes;
using Simargl.AccelEth3T.AccelEth3TViewer.UI;

namespace Simargl.AccelEth3T.AccelEth3TViewer;

/// <summary>
/// Предоставляет точку входа в приложение.
/// </summary>
public static class Program
{
    /// <summary>
    /// Представляет точку входа приложения.
    /// </summary>
    [STAThread]
    public static void Main()
    {
        // Создаём объект приложения
        Simargl.UI.Application app = new();

        //  Создание корневого узла.
        Root = new();

        // Создаём главное окно
        System.Windows.Window mainWindow = new()
        {
            Title = "Просмотр данных датчика AccelEth3T",
            Content = new MainControl(),
        };

        // Запускаем приложение и открываем главное окно
        app.Run(mainWindow);
    }

    /// <summary>
    /// Возвращает корневой узел.
    /// </summary>
    public static RootNode Root { get; private set; } = null!;
}
