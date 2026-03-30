using Apeiron.Platform.Communication.СommutatorDesktop.Logging;
using Apeiron.Platform.Communication.СommutatorDesktop.UserInterface;
using System.Collections.Concurrent;

namespace Apeiron.Platform.Communication.СommutatorDesktop;

/// <summary>
/// Представляет основные настройки приложения.
/// </summary>
public sealed class Setting
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public Setting()
    {
        //  Создание параметров коммуникатора.
        CommunicatorOptions = new(
            new("morphism.ru", 7013, "Дмитрий", "123QWEasd"),
            //new("morphism.ru", 7013, "Андрей", "123QWEasd"),
            //new("morphism.ru", 7013, "Сергей", "123QWEasd"),
            new(TimeSpan.FromMilliseconds(2000), 10, TimeSpan.FromMilliseconds(1000)));

        //  Установка времени жизни сообщения журнала по умолчанию.
        LogMessageLifeDuration = TimeSpan.FromSeconds(5);

        //  Уставнока интервала обновления элемента управления, отображающего журнал.
        LogViewUpdateInterval = TimeSpan.FromMilliseconds(500);

        //  Создание очереди сообщений журнала.
        LogMessages = new();
    }

    /// <summary>
    /// Возвращает параметры коммуникатора.
    /// </summary>
    public CommunicatorOptions CommunicatorOptions { get; }

    /// <summary>
    /// Возвращает время жизни сообщения журнала по умолчанию.
    /// </summary>
    public TimeSpan LogMessageLifeDuration { get; }

    /// <summary>
    /// Возвращает интервал обновления элемента управления, отображающего журнал.
    /// </summary>
    public TimeSpan LogViewUpdateInterval { get; }

    /// <summary>
    /// Возвращает очередь сообщений журнала.
    /// </summary>
    /// <remarks>
    /// К свойству должны обращаться только <see cref="Logger"/> или <see cref="LogView"/>.
    /// </remarks>
    public ConcurrentQueue<LogMessage> LogMessages { get; }
}
