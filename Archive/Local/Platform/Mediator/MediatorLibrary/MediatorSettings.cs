using System.Net;

namespace Apeiron.Platform.MediatorLibrary;

/// <summary>
/// Предоставляет информацию о настройках.
/// </summary>
public class MediatorSettings
{
    /// <summary>
    /// Возвращает порт для подключения к серверному пространству.
    /// </summary>
    public static int MediatorServerPort { get; } = 48889;

    /// <summary>
    /// Возвращает адрес для подключения к серверному пространству.
    /// </summary>
    public static IPAddress MediatorServerAddress { get; } = IPAddress.Parse("10.69.16.135");
}
