namespace Simargl.Engineering.Utilities.CertificateSheet;

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
        Heart = new(async delegate(Action action)
        {
            //  Исключение во время работы.
            Exception? exception = null;

            //  Выполнение в основном потоке.
            await Dispatcher.InvokeAsync(delegate
            {
                //  Блок перехвата всех исключений.
                try
                {
                    //  Выполнение действия.
                    action();
                }
                catch (Exception ex)
                {
                    //  Установка исключения.
                    exception = ex;
                }
            });

            //  Проверка исключения.
            if (exception is not null)
            {
                //  Выброс исключения.
                throw new InvalidOperationException(
                    "Произошла ошибка при выполнении действия в основном потоке",
                    exception);
            }
        });

        //  Добавление обработчика события завершения приложения.
        Exit += (object sender, ExitEventArgs e) => ((IDisposable)Heart).Dispose();

        //  Инициализация основных компонентов.
        InitializeComponent();
    }

    /// <summary>
    /// Возвращает сердце приложения.
    /// </summary>
    public Heart Heart { get; }
}
