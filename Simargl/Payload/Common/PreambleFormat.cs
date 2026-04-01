using Simargl.Payload.Recording;

namespace Simargl.Payload.Common;

/// <summary>
/// Представляет значение, определяющее формат двоичных данных.
/// </summary>
public enum PreambleFormat
{
    /// <summary>
    /// 
    /// </summary>
    EmbeddedTuningsDatagram = Preamble.FirstEmbeddedFormat,

    /// <summary>
    /// UDP-датаграмма, см. <see cref="UdpDatagram"/>.
    /// </summary>
    RecordingUdpDatagram = Preamble.FirstManagedFormat + 1,

    /// <summary>
    /// Блок данных TCP, см. <see cref="TcpDataBlock"/>.
    /// </summary>
    RecordingTcpDataBlock = Preamble.FirstManagedFormat + 2,
}
