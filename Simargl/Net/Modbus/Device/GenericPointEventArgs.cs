namespace Simargl.Net.Modbus.Device;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class PointEventArgs<T> :
    PointEventArgs
    where T : struct
{
    private readonly T[] _points;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="startAddress"></param>
    /// <param name="points"></param>
    [CLSCompliant(false)]
    public PointEventArgs(ushort startAddress, T[] points) :
        base(startAddress, (ushort)points.Length)
    {
        _points = points;
    }

    /// <summary>
    /// 
    /// </summary>
    public T[] Points => _points;
}
