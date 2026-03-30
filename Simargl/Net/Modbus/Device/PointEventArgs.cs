namespace Simargl.Net.Modbus.Device;

/// <summary>
/// Modbus Slave request event args containing information on the message.
/// </summary>
public class PointEventArgs : EventArgs
{
    private ushort _numberOfPoints;

    private ushort _startAddress;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="startAddress"></param>
    /// <param name="numberOfPoints"></param>
    [CLSCompliant(false)]
    public PointEventArgs(ushort startAddress, ushort numberOfPoints)
    {
        _startAddress = startAddress;
        _numberOfPoints = numberOfPoints;
    }

    /// <summary>
    /// 
    /// </summary>
    [CLSCompliant(false)]
    public ushort NumberOfPoints => _numberOfPoints;

    /// <summary>
    /// 
    /// </summary>
    [CLSCompliant(false)]
    public ushort StartAddress => _startAddress;
}
