using Apeiron.Platform.Demo.AdxlDemo.UserInterface;
using System.Globalization;

namespace Apeiron.Platform.Demo.AdxlDemo;

/// <summary>
/// Представляет приложение.
/// </summary>
public partial class App :
    Application
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public App()
    {
        //  Создание общих настроек приложения.
        Settings = new(this);

        //  Создание данных пользовательского интерфейса.
        UserInterfaceData = new();

        //  Создание основного активного объекта.
        Engine = new(this);
    }

    /// <summary>
    /// Возвращает общие настройки приложения.
    /// </summary>
    public Settings Settings { get; }

    /// <summary>
    /// Возвращает данные пользовательского интерфейса.
    /// </summary>
    public UserInterfaceData UserInterfaceData { get; }

    /// <summary>
    /// Возвращает основной активный объект.
    /// </summary>
    public Engine Engine { get; }

    /// <summary>
    /// Вызывает событие запуска приложения.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected override void OnStartup(StartupEventArgs e)
    {
        CultureInfo ci = new(Thread.CurrentThread.CurrentCulture.Name);
        ci.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy HH:mm:ss";
        Thread.CurrentThread.CurrentCulture = ci;

        base.OnStartup(e);
    }
}
