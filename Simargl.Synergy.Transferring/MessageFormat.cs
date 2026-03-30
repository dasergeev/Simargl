namespace Simargl.Synergy.Transferring;

/// <summary>
/// Представляет значение, определяющее формат сообщения.
/// </summary>
public enum MessageFormat
{
    /// <summary>
    /// Запрос на подключение.
    /// </summary>
    ConnectionRequest = 0,

    /// <summary>
    /// Подтверждение подключения.
    /// </summary>
    ConnectionConfirmation = 1,

    /// <summary>
    /// Данные файла.
    /// </summary>
    FileData = 2,

    /// <summary>
    /// Подтверждение получения данных файла.
    /// </summary>
    FileDataConfirmation = 3,
}
