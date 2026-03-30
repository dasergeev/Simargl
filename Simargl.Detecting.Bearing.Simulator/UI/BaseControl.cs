using System.ComponentModel;

namespace Simargl.Detecting.Bearing.Simulator.UI;

/// <summary>
/// Представляет базовый элемент управления.
/// </summary>
public class BaseControl :
    UserControl
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public BaseControl()
    {
        //  Определение режима выполнения.
        IsRuntime = !DesignerProperties.GetIsInDesignMode(this);

        //  Проверка режима выполнения.
        if (IsRuntime)
        {
            //  Установка приложения.
            Application = (App)System.Windows.Application.Current;

            //  Установка контейнера зависимостей.
            Host = Application.Host;
        }
        else
        {
            //  Установка фиктивных значений.
            Application = null!;
            Host = null!;
        }
    }

    /// <summary>
    /// Возвращает признак режима выполнения.
    /// </summary>
    public bool IsRuntime { get; }

    /// <summary>
    /// Возвращает приложение.
    /// </summary>
    public App Application { get; }

    /// <summary>
    /// Возвращает контейнер зависимостей.
    /// </summary>
    public IHost Host { get; }
}
