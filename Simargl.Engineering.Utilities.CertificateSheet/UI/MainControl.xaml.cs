namespace Simargl.Engineering.Utilities.CertificateSheet.UI;

/// <summary>
/// Представляет главный элемент управления.
/// </summary>
partial class MainControl
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public MainControl()
    {
        //  Инициализирует новый экземпляр.
        InitializeComponent();

        //  Проверка режима разработки.
        if (!DesignerProperties.GetIsInDesignMode(this))
        {
            //  Установка данных контекста.
            DataContext = Heart;
        }
    }

    /// <summary>
    /// Возвращает сердце приложения.
    /// </summary>
    public static Heart Heart => ((App)Application.Current).Heart;

    /// <summary>
    /// Обрабатывает событие нажатия кнопки запуска работы.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void StartButton_Click(object sender, RoutedEventArgs e)
    {
        //  Запуск работы.
        Heart.Start();
    }

    /// <summary>
    /// Обрабатывает событие нажатия кнопки остановки работы.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void StopButton_Click(object sender, RoutedEventArgs e)
    {
        //  Остановка работы.
        Heart.Stop();
    }
}
