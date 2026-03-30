namespace Apeiron.Services.GlobalIdentity;

/// <summary>
/// Предоставляет статические настройки.
/// </summary>
public static class StaticSettings
{
    /// <summary>
    /// Постоянная, определяющая текущую версию пакетов, сообщающих состояние.
    /// </summary>
    public const int StatusPacketVersion = 3;

    /// <summary>
    /// Постоянная, определяющая текущую версию пакетов, сообщающих ответ сервера.
    /// </summary>
    public const int AnswerPacketVersion = 1;

    /// <summary>
    /// Постоянная, определяющая неизвестный идентификатор.
    /// </summary>
    public const long GlobalUnknownIdentifier = 1;
}
