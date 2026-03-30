namespace Simargl.Engineering.Utilities.StampWriter;

/// <summary>
/// Представляет приложение.
/// </summary>
partial class App
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public App()
    {
        //  Создание сердца приложения.
        Heart = new(Dispatcher.InvokeAsync);

        //  Добавление обработчика события.
        Exit += delegate (object sender, ExitEventArgs e)
        {
            //  Разрушение сердца приложения.
            ((IDisposable)Heart).Dispose();
        };

        //  Инициализация основных компонентов.
        InitializeComponent();
    }

    /// <summary>
    /// Возвращает сердце приложения.
    /// </summary>
    public Heart Heart { get; }
}
