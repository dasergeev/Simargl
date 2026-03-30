namespace Simargl.QuantumX;

/// <summary>
/// Класс базовых констант и функций.
/// </summary>
public class Common
{
    /// <summary>
    /// Номер IP порта для подключения к потоку от устройства Quantum.
    /// </summary>
    public const string DAQSTREAM_PORT = "7411";

    /// <summary>
    /// Тег используемый для идентификации версии ПО устройства Quantum. 
    /// </summary>
    public const string META_METHOD_APIVERSION = "apiVersion";
    /// <summary>
    /// Тег для идентификации метаинформации инициализации потока.
    /// </summary>
    public const string META_METHOD_INIT = "init";
    /// <summary>
    /// Тег для идентификации метаинформации о времени.
    /// </summary>
    public const string META_METHOD_TIME = "time";
    /// <summary>
    /// Тег для идентификации метаинформации о доступных к подписке сигналах.
    /// </summary>
    public const string META_METHOD_AVAILABLE = "available";
    /// <summary>
    /// Тег для идентификации метаинформации о недоступных к подписке сигналах.
    /// </summary>
    public const string META_METHOD_UNAVAILABLE = "unavailable";
    /// <summary>
    /// Тег для идентификации информации о проблемах в потоке.
    /// </summary>
    public const string META_METHOD_ALIVE = "alive";
    /// <summary>
    /// Тег для идентификации информации о заполнении буфера устройства Quantum.
    /// </summary>
    public const string META_METHOD_FILL = "fill";
    /// <summary>
    /// Тег для идентификации сообщения о подписке на сигнал.
    /// </summary>
    public const string META_METHOD_SUBSCRIBE = "subscribe";
}
