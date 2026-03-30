namespace Simargl.Hardware.TuningsEditor;

/// <summary>
/// Представляет главное окно приложения.
/// </summary>
partial class MainWindow
{
    /// <summary>
    /// Поле для хранения ключа, для установки значения свойства зависимости для свойства <see cref="Kernel"/>.
    /// </summary>
    private static readonly DependencyPropertyKey _KernelPropertyKey =
        DependencyProperty.RegisterReadOnly(
            nameof(Kernel),
            typeof(Kernel),
            typeof(MainWindow),
            new PropertyMetadata(null));

    /// <summary>
    /// Поле для хранения свойства зависимости для свойства <see cref="Kernel"/>.
    /// </summary>
    public static readonly DependencyProperty KernelProperty = _KernelPropertyKey.DependencyProperty;

    /// <summary>
    /// Поле для хранения ядра приложения.
    /// </summary>
    private Kernel? _Kernel;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public MainWindow()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Создание ядра приложения.
        _Kernel = new();

        //  Установка значения свойства зависимости.
        SetValue(_KernelPropertyKey, _Kernel);

        //  Добавление обработчика события-запроса на закрытие окна.
        Closing += MainWindow_Closing;

        //  Установка контекста данных.
        DataContext = _Kernel;
    }

    /// <summary>
    /// Возвращает ядро приложения.
    /// </summary>
    public Kernel Kernel => (Kernel)GetValue(KernelProperty);

    /// <summary>
    /// Обрабатывает событие запроса на закрытие окна.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        //  Получение объекта ядра приложения.
        if (Interlocked.Exchange(ref _Kernel, null) is Kernel kernel)
        {
            //  Остановка ядра приложения.
            kernel.Stop();

            //  Временная отмена закрытия окна.
            e.Cancel = true;

            //  Запуск асинхронной задачи.
            _ = Task.Run(async delegate
            {
                //  Разрушение ядра приложения.
                await kernel.DisposeAsync().ConfigureAwait(false);

                //  Удаление обработчика события-запроса на закрытие окна.
                Closing -= MainWindow_Closing;

                //  Повторный запрос на закрытие окна.
                await Dispatcher.InvokeAsync(Close);
            });
        }
    }
}
