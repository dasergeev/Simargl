namespace Apeiron.Platform.Communication.Remoting;

/// <summary>
/// Представляет значение, определяющее тип результата.
/// </summary>
public enum RemoteResult
{
    /// <summary>
    /// Данные отсутствуют.
    /// </summary>
    Void = 0,

    /// <summary>
    /// В результате вызова получены данные.
    /// </summary>
    Data = 1,

    /// <summary>
    /// В результате вызова произошло исключения.
    /// </summary>
    Exception = 2,
}
