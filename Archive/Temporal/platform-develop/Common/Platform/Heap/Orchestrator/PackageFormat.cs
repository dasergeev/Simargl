namespace Apeiron.Platform.Server.Services.Orchestrator.Packages;

/// <summary>
/// Перечисление форматов пакетов.
/// </summary>
public enum PackageFormat : byte
{
    /// <summary>
    /// Идентификационный пакет от клиента(хоста) к серверу.
    /// </summary>
    IdHostPackage = 1,

    /// <summary>
    /// Представляет основной пакет от клиента(хоста) к серверу.
    /// </summary>
    GeneralConsolePackage,

    /// <summary>
    /// Представляет основной пакет от клиента(хоста) к серверу.
    /// </summary>
    GetHostListConsolePackage,

    /// <summary>
    /// Представляет основной пакет от клиента(хоста) к серверу.
    /// </summary>
    SendHostListConsolePackage,

    /// <summary>
    /// Представляет пакет от консоли на сервер для остановки служб на хостах.
    /// </summary>
    StopServicesConsolePackage,

    /// <summary>
    /// Представляет пакет от консоли на сервер для запуска службы на хостах.
    /// </summary>
    StartServicesConsolePackage,

    /// <summary>
    /// Представляет пакет от консоли на сервер для запуска копирования файлов служб на хосты.
    /// </summary>
    CopyServicesConsolePackage,

    /// <summary>
    /// Представляет пакет для обновления файлов служб на хостах.
    /// </summary>
    CopyServicesOnHostPackage,

    /// <summary>
    /// Прдеставляет пакет для остановки служб на хостах.
    /// </summary>
    SendStopServicesOnHostsPackage,

    /// <summary>
    /// Представляет пакет для запуска служб на хостах.
    /// </summary>
    SendStartServicesOnHostPackage
}

