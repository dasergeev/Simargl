namespace Apeiron.Platform.MediatorLibrary.Messages;

/// <summary>
/// Перечисление форматов пакетов.
/// </summary>
public enum MessageType : byte
{
    /// <summary>
    /// Простое результирующее сообщение без какого-либо ответа.
    /// </summary>
    VoidResponceMessage = 1,

    /// <summary>
    /// Результирующее сообщение с данными внутри.
    /// </summary>
    DataResponceMessage = 2,

    /// <summary>
    /// Результирующее сообщение содержащее внутри ошибку выполнения команды/функции.
    /// </summary>
    ErrorResponceMessage = 3,

    /// <summary>
    /// Тестовое сообщение.
    /// </summary>
    TestTextMessage = 4,

    /// <summary>
    /// Добавление в список сообщений.
    /// </summary>
    AddToMessageList = 5,

    /// <summary>
    /// Удаление из списка сообщений.
    /// </summary>
    DeleteFromMessageList = 6,

    /// <summary>
    /// Поиск в списке сообщений.
    /// </summary>
    SearchFromMessageList = 7
}
