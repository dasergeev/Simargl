using Simargl.Net.Modbus.Interfaces;
using Simargl.Net.Modbus.Unme.Common;

namespace Simargl.Net.Modbus.Data;

/// <summary>
/// Представляет простую реализацию источника точек Modbus,
/// хранящую данные во внутреннем массиве фиксированного размера (0..65535).
/// </summary>
/// <typeparam name="TPoint">
/// Тип хранимой точки.
/// </typeparam>
internal class DefaultPointSource<TPoint> :
    IPointSource<TPoint>
{
    /// <summary>
    /// Поле для хранения внутреннего массива точек.
    /// </summary>
    private readonly Lazy<TPoint[]> _Points = new Lazy<TPoint[]>(() => new TPoint[ushort.MaxValue + 1]);

    /// <summary>
    /// Поле для хранения критического объекта.
    /// </summary>
    private readonly Lock _SyncRoot = new();

    public TPoint[] ReadPoints(ushort startAddress, ushort numberOfPoints)
    {
        lock (_SyncRoot)
        {
            return [.. _Points.Value.Slice(startAddress, numberOfPoints)];
        }
    }

    public void WritePoints(ushort startAddress, TPoint[] points)
    {
        lock (_SyncRoot)
        {
            for (ushort index = 0; index < points.Length; index++)
            {
                _Points.Value[startAddress + index] = points[index];
            }
        }
    }
}