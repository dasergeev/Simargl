using Simargl.Hardware.Strain.Demo.Microservices;
using System.Windows;
using System.Windows.Media;

namespace Simargl.Hardware.Strain.Demo;

/// <summary>
/// Представляет главное окно приложения.
/// </summary>
partial class MainWindow
{
    /// <summary>
    /// Поле для хранения сердца приложения.
    /// </summary>
    private readonly Heart _Heart;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public MainWindow()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        _Background = Background;
        _Foreground = Foreground;

        //  Получение сердца приложения.
        _Heart = _MainControl.GetHeart();

        UpdateGround();


        //App application = (App)Application.Current;
        //_ = Task.Run(async delegate
        //{

        //    await Task.Delay(5000, application.CancellationToken).ConfigureAwait(false);
        //    await application.Invoker.InvokeAsync(delegate
        //    {
        //        Background = new SolidColorBrush(Colors.ForestGreen);
        //    }, application.CancellationToken).ConfigureAwait(false);
        //}, application.CancellationToken);
    }

    private Brush _Background;
    private Brush _Foreground;

    private void UpdateGround()
    {
        //  Проверка значений.
        if (_Heart is not null && _Heart.ChartManager is ChartManager manager)
        {
            //  Оюновление.
            manager.UpdateGround(_Background, _Foreground);
        }
    }

    private void OnBackgroundChanged(Brush brush)
    {
        _Background = brush;
        UpdateGround();
    }

    private void OnForegroundChanged(Brush brush)
    {
        _Foreground = brush;
        UpdateGround();
    }

    /// <summary>
    /// Инициализирует статические данные.
    /// </summary>
    static MainWindow()
    {
        //  Добавление обработчиков событий.
        BackgroundProperty.OverrideMetadata(typeof(MainWindow),
            new FrameworkPropertyMetadata(null, OnBackgroundChanged));
        ForegroundProperty.OverrideMetadata(typeof(MainWindow),
            new FrameworkPropertyMetadata(null, OnForegroundChanged));
    }

    private static void OnBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        //  Проверка значений.
        if (d is MainWindow window && e.NewValue is Brush brush)
        {
            //  Отправка данных.
            window.OnBackgroundChanged(brush);
        }
    }

    private static void OnForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        //  Проверка значений.
        if (d is MainWindow window && e.NewValue is Brush brush)
        {
            //  Отправка данных.
            window.OnForegroundChanged(brush);
        }
    }
}
