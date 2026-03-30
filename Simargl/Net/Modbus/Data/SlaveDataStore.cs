using Simargl.Net.Modbus.Interfaces;

namespace Simargl.Net.Modbus.Data;

/// <summary>
/// 
/// </summary>
public class SlaveDataStore :
    ISlaveDataStore
{
    /// <summary>
    /// 
    /// </summary>
    [CLSCompliant(false)]
    public PointSource<ushort> HoldingRegisters { get; } = new PointSource<ushort>();

    /// <summary>
    /// 
    /// </summary>
    [CLSCompliant(false)]
    public PointSource<ushort> InputRegisters { get; } = new PointSource<ushort>();

    /// <summary>
    /// 
    /// </summary>
    public PointSource<bool> CoilDiscretes { get; } = new PointSource<bool>();

    /// <summary>
    /// 
    /// </summary>
    public PointSource<bool> CoilInputs { get; } = new PointSource<bool>();

    IPointSource<ushort> ISlaveDataStore.HoldingRegisters => HoldingRegisters;

    IPointSource<ushort> ISlaveDataStore.InputRegisters => InputRegisters;

    IPointSource<bool> ISlaveDataStore.CoilDiscretes => CoilDiscretes;

    IPointSource<bool> ISlaveDataStore.CoilInputs => CoilInputs;
}
