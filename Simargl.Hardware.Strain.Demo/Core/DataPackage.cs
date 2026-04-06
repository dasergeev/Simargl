using System.Net;

namespace Simargl.Hardware.Strain.Demo.Core;

/// <summary>
/// Представляет пакет данных.
/// </summary>
public sealed class DataPackage
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public DataPackage()
    {

    }

    /// <summary>
    /// Возвращает или инициализирует конечную точку.
    /// </summary>
    public IPEndPoint EndPoint { get; init; } = null!;

    /// <summary>
    /// Возвращает или инициализирует идентификатор сеанса.
    /// </summary>
    public long SessionKey { get; init; }

    /// <summary>
    /// Возвращает или инициализирует время получения пакета.
    /// </summary>
    public DateTime ReceivingTime { get; init; }

    /// <summary>
    /// Возвращает или инициализирует полный размер пакета в байтах.
    /// </summary>
    public int FullPackageSize { get; init; }

    /// <summary>
    /// Возвращает или инициализирует серийный номер.
    /// </summary>
    [CLSCompliant(false)]
    public uint SerialNumber { get; init; }

    /// <summary>
    /// Возвращает или инициализирует флаг времени.???
    /// </summary>
    public byte SyncFlag { get; init; }

    /// <summary>
    /// Возвращает или инициализирует время в секундах.???
    /// </summary>
    [CLSCompliant(false)]
    public ulong TimeUnix { get; init; }

    /// <summary>
    /// Возвращает или инициализирует младшей части времени.???
    /// </summary>
    [CLSCompliant(false)]
    public uint TimeNano { get; init; }

    /// <summary>
    /// Возвращает или инициализирует температуры.???
    /// </summary>
    public float CpuTemp { get; init; }

    /// <summary>
    /// Возвращает или инициализирует температуры.???
    /// </summary>
    public float SensorTemp { get; init; }

    /// <summary>
    /// Возвращает или инициализирует напряжение питания.???
    /// </summary>
    public float CpuPower { get; init; }

    /// <summary>
    /// Возвращает или инициализирует данные.
    /// </summary>
    public float[][] Data { get; init; } = null!;

    /// <summary>
    /// Возвращает или задаёт частоту дискретизации.
    /// </summary>
    public float Sampling { get; set; }
}
