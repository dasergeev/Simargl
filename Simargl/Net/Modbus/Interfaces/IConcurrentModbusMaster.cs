namespace Simargl.Net.Modbus.Interfaces;

/// <summary>
/// 
/// </summary>
[CLSCompliant(false)]
public interface IConcurrentModbusMaster :
    IDisposable
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="slaveAddress"></param>
    /// <param name="startAddress"></param>
    /// <param name="numberOfPoints"></param>
    /// <param name="blockSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ushort[]> ReadInputRegistersAsync(byte slaveAddress, ushort startAddress, ushort numberOfPoints, ushort blockSize = 125, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slaveAddress"></param>
    /// <param name="startAddress"></param>
    /// <param name="numberOfPoints"></param>
    /// <param name="blockSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ushort[]> ReadHoldingRegistersAsync(byte slaveAddress, ushort startAddress, ushort numberOfPoints, ushort blockSize = 125, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slaveAddress"></param>
    /// <param name="startAddress"></param>
    /// <param name="data"></param>
    /// <param name="blockSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task WriteMultipleRegistersAsync(byte slaveAddress, ushort startAddress, ushort[] data, ushort blockSize = 121, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slaveAddress"></param>
    /// <param name="startAddress"></param>
    /// <param name="number"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool[]> ReadCoilsAsync(byte slaveAddress, ushort startAddress, ushort number, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slaveAddress"></param>
    /// <param name="startAddress"></param>
    /// <param name="number"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool[]> ReadDiscretesAsync(byte slaveAddress, ushort startAddress, ushort number, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slaveAddress"></param>
    /// <param name="startAddress"></param>
    /// <param name="data"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task WriteCoilsAsync(byte slaveAddress, ushort startAddress, bool[] data, CancellationToken cancellationToken = default(CancellationToken));
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="slaveAddress"></param>
    /// <param name="coilAddress"></param>
    /// <param name="value"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task WriteSingleCoilAsync(byte slaveAddress, ushort coilAddress, bool value, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slaveAddress"></param>
    /// <param name="address"></param>
    /// <param name="value"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task WriteSingleRegisterAsync(byte slaveAddress, ushort address, ushort value, CancellationToken cancellationToken = default(CancellationToken));
}
