namespace Simargl.Net.Modbus.Interfaces;

/// <summary>
/// 
/// </summary>
[CLSCompliant(false)]
public interface ISlaveHandlerContext
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="functionCode"></param>
    /// <returns></returns>
    IModbusFunctionService GetHandler(byte functionCode);
}
