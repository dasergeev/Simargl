namespace Simargl.Hardware.Strain.Demo.UI;

/// <summary>
/// Представляет главный элемент управления.
/// </summary>
partial class MainControl
{
    /// <summary>
    /// Поле для хранения сердца приложения.
    /// </summary>
    private readonly Heart _Heart;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public MainControl()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Создание сердца приложения.
        _Heart = new();

        //  Установка сердца приложения.
        SetValue(HeartProperty, _Heart);

        //  Запуск сердца приложения.
        _ = Task.Run(_Heart.RunAsync);
    }

    /// <summary>
    /// Возвращает сердце приложения.
    /// </summary>
    /// <returns>
    /// Сердце приложения.
    /// </returns>
    public Heart GetHeart() => _Heart;
}
