namespace Simargl.Payload;

/// <summary>
/// Представляет значение, определяющее формат двоичных данных.
/// </summary>
public enum DataPackageFormat
{
    /// <summary>
    /// UDP-датаграмма.
    /// </summary>
    UdpDatagram,

    /// <summary>
    /// Блок данных TCP.
    /// </summary>
    TcpDataBlock,
}
