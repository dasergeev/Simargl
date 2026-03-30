using System.Net;

namespace Simargl.Platform.Expanse.Core;

/// <summary>
/// Предоставляет информацию о глобальных настройках.
/// </summary>
public static class GlobalSettings
{
    /// <summary>
    /// Возвращает порт для подключения к серверному пространству.
    /// </summary>
    public static int ExpansePort { get; } = 7031;

    /// <summary>
    /// Возвращает адрес для подключения к серверному пространству.
    /// </summary>
    public static IPAddress ExpanseAddress { get; } = IPAddress.Parse("10.69.16.237");
}
