namespace Apeiron.Platform.MediatorLibrary;

/// <summary>
/// Предоставляет идентификатор метода, выполняемого в серверном пространстве.
/// </summary>
public enum MediatorMethodId
{
    /// <summary>
    /// Тестирование подключения к службе Mediator.
    /// </summary>
    CheckConnectionToMediator = 0,

    /// <summary>
    /// Тестирование подключения к службе сервера Оркестратора.
    /// </summary>
    GetHostListFromMediatorServer = 1,

    /// <summary>
    /// Тестирование подключения к службе сервера Оркестратора.
    /// </summary>
    GetCommandsFromMediatorServer = 2,

    /// <summary>
    /// Получение данных оркестратора.
    /// </summary>
    GetHostInfo = 3
}
