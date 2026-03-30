namespace Simargl.Net.Modbus;

/// <summary>
/// Supported function codes
/// </summary>
public static class ModbusFunctionCodes
{
    /// <summary>
    /// 
    /// </summary>
    public const byte ReadCoils = 1;

    /// <summary>
    /// 
    /// </summary>
    public const byte ReadInputs = 2;

    /// <summary>
    /// 
    /// </summary>
    public const byte ReadHoldingRegisters = 3;

    /// <summary>
    /// 
    /// </summary>
    public const byte ReadInputRegisters = 4;

    /// <summary>
    /// 
    /// </summary>
    public const byte WriteSingleCoil = 5;

    /// <summary>
    /// 
    /// </summary>
    public const byte WriteSingleRegister = 6;

    /// <summary>
    /// 
    /// </summary>
    public const byte Diagnostics = 8;

    /// <summary>
    /// 
    /// </summary>
    [CLSCompliant(false)]
    public const ushort DiagnosticsReturnQueryData = 0;

    /// <summary>
    /// 
    /// </summary>
    public const byte WriteMultipleCoils = 15;

    /// <summary>
    /// 
    /// </summary>
    public const byte WriteMultipleRegisters = 16;
    
    /// <summary>
    /// 
    /// </summary>
    public const byte WriteFileRecord = 21;

    /// <summary>
    /// 
    /// </summary>
    public const byte ReadWriteMultipleRegisters = 23;
}
