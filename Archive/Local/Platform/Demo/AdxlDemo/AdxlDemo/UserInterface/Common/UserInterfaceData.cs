using Apeiron.Platform.Demo.AdxlDemo.Logging;
using System.Collections.ObjectModel;

namespace Apeiron.Platform.Demo.AdxlDemo.UserInterface;

/// <summary>
/// Представляет данные пользовательского интерфейса.
/// </summary>
public sealed class UserInterfaceData
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public UserInterfaceData()
    {
        //  Создание коллекции сообщений журнала.
        LogMessages = new();
    }

    /// <summary>
    /// Возвращает коллекцию сообщений журнала.
    /// </summary>
    public ObservableCollection<LogMessage> LogMessages { get; }
}
