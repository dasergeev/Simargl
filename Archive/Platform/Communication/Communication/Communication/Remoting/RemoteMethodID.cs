namespace Apeiron.Platform.Communication.Remoting;

/// <summary>
/// Представляет идентификатор удалённого метода.
/// </summary>
public enum RemoteMethodID
{
    /// <summary>
    /// Выполнение авторизации.
    /// </summary>
    Authorization = 0,

    /// <summary>
    /// Запрос всех пользователей.
    /// </summary>
    RequestAllUsers = 1,

    /// <summary>
    /// Отправка сообщения.
    /// </summary>
    SendMessage = 2,

    /// <summary>
    /// Запрос временного диапазона диалога.
    /// </summary>
    RequestDialogRange = 3,

    /// <summary>
    /// Запрос количества сообщений.
    /// </summary>
    RequestCountMessages = 4,

    /// <summary>
    /// Запрос идентификаторов сообщений.
    /// </summary>
    RequestIDMessages = 5,

    /// <summary>
    /// Запрос сообщения.
    /// </summary>
    RequestMessage = 6,
}
