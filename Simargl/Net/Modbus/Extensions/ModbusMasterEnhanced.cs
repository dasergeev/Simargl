using Simargl.Net.Modbus.Extensions.Functions;
using Simargl.Net.Modbus.Interfaces;

namespace Simargl.Net.Modbus.Extensions;

/// <summary>
/// Utility Class to support Modbus 32/64bit devices. 
/// </summary>
public class ModbusMasterEnhanced
{
private readonly IModbusMaster master;
private readonly uint wordSize;
private readonly Func<byte[], byte[]> endian;
private readonly bool wordSwapped;


    /// <summary>
    /// Constructor with values to be used by all methods. 
    /// Default is 32bit, LittleEndian, with wordswapping.
    /// </summary>
    /// <param name="master">The Modbus master</param>
    /// <param name="wordSize">Wordsize used by device. 16/32/64 are valid.</param>
    /// <param name="endian">The endian encoding of the device.</param>
    /// <param name="wordSwapped">Should the ushort words mirrored then flattened to bytes.</param>
    [CLSCompliant(false)]
    public ModbusMasterEnhanced(IModbusMaster master, uint wordSize=32, Func<byte[], byte[]>? endian = null, bool wordSwapped = false)
{
  this.master = master;
  this.wordSize = wordSize;
  this.endian = endian ?? Endian.LittleEndian;
  this.wordSwapped = wordSwapped;
}

    /// <summary>
    /// Reads registers and converts the result into a char array.
    /// </summary>
    /// <param name="slaveAddress">Address of device to read values from.</param>
    /// <param name="startAddress">Address to begin reading.</param>
    /// <param name="numberOfPoints">Number of chars to read.</param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public char[] ReadCharHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
  => RegisterFunctions.ByteValueArraysToChars(
    RegisterFunctions.ReadRegisters(slaveAddress, startAddress, numberOfPoints, this.master, this.wordSize, this.endian, this.wordSwapped));

    /// <summary>
    /// Reads registers and converts the result into a ushort array.
    /// </summary>
    /// <param name="slaveAddress">Address of device to read values from.</param>
    /// <param name="startAddress">Address to begin reading.</param>
    /// <param name="numberOfPoints">Number of ushorts to read.</param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public ushort[] ReadUshortHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
    => RegisterFunctions.ByteValueArraysToUShorts(
      RegisterFunctions.ReadRegisters(slaveAddress, startAddress, numberOfPoints, this.master, this.wordSize, this.endian, this.wordSwapped));

    /// <summary>
    /// Reads registers and converts the result into a short array.
    /// </summary>
    /// <param name="slaveAddress">Address of device to read values from.</param>
    /// <param name="startAddress">Address to begin reading.</param>
    /// <param name="numberOfPoints">Number of shots to read.</param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public short[] ReadShortHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
  => RegisterFunctions.ByteValueArraysToShorts(
    RegisterFunctions.ReadRegisters(slaveAddress, startAddress, numberOfPoints, this.master, this.wordSize, this.endian, this.wordSwapped));

    /// <summary>
    /// Reads registers and converts the result into a uint array.
    /// </summary>
    /// <param name="slaveAddress">Address of device to read values from.</param>
    /// <param name="startAddress">Address to begin reading.</param>
    /// <param name="numberOfPoints">Number of uints to read.</param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public uint[] ReadUintHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
  => RegisterFunctions.ByteValueArraysToUInts(
    RegisterFunctions.ReadRegisters(slaveAddress, startAddress, numberOfPoints, this.master, this.wordSize, this.endian, this.wordSwapped));

    /// <summary>
    /// Reads registers and converts the result into a int array.
    /// </summary>
    /// <param name="slaveAddress">Address of device to read values from.</param>
    /// <param name="startAddress">Address to begin reading.</param>
    /// <param name="numberOfPoints">Number of ints to read.</param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public int[] ReadIntHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
  => RegisterFunctions.ByteValueArraysToInts(
    RegisterFunctions.ReadRegisters(slaveAddress, startAddress, numberOfPoints, this.master, this.wordSize, this.endian, this.wordSwapped));

    /// <summary>
    /// Reads registers and converts the result into a float array.
    /// </summary>
    /// <param name="slaveAddress">Address of device to read values from.</param>
    /// <param name="startAddress">Address to begin reading.</param>
    /// <param name="numberOfPoints">Number of floats to read.</param>
    /// <returns></returns>
    [CLSCompliant(false)]
    public float[] ReadFloatHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
  => RegisterFunctions.ByteValueArraysToFloats(
       RegisterFunctions.ReadRegisters(slaveAddress, startAddress, numberOfPoints, this.master, this.wordSize, this.endian, this.wordSwapped));

    /// <summary>
    ///     Write a char array to registers.
    /// </summary>
    /// <param name="slaveAddress">Address of the device to write to.</param>
    /// <param name="startAddress">Address to start writting values at.</param>
    /// <param name="data">Chars to write to device.</param>
    [CLSCompliant(false)]
    public void WriteCharHoldingRegisters(byte slaveAddress, ushort startAddress, char[] data)
  => RegisterFunctions.WriteRegistersFunc(
    slaveAddress,
    startAddress,
    RegisterFunctions.CharsToByteValueArrays(data, this.wordSize),
    this.master,
    this.wordSize,
    this.endian, this.wordSwapped);

    /// <summary>
    ///     Write a ushort array to registers.
    /// </summary>
    /// <param name="slaveAddress">Address of the device to write to.</param>
    /// <param name="startAddress">Address to start writting values at.</param>
    /// <param name="data">Ushorts to write to device.</param>
    [CLSCompliant(false)]
    public void WriteUshortHoldingRegisters(byte slaveAddress, ushort startAddress, ushort[] data)
  => RegisterFunctions.WriteRegistersFunc(
    slaveAddress,
    startAddress,
    RegisterFunctions.UShortsToByteValueArrays(data, this.wordSize),
    this.master,
    this.wordSize,
    this.endian, this.wordSwapped);

    /// <summary>
    ///     Write a short array to registers.
    /// </summary>
    /// <param name="slaveAddress">Address of the device to write to.</param>
    /// <param name="startAddress">Address to start writting values at.</param>
    /// <param name="data">Shorts to write to device.</param>
    [CLSCompliant(false)]
    public void WriteShortHoldingRegisters(byte slaveAddress, ushort startAddress, short[] data)
  => RegisterFunctions.WriteRegistersFunc(
    slaveAddress,
    startAddress,
    RegisterFunctions.ShortsToByteValueArrays(data, this.wordSize),
    this.master,
    this.wordSize,
    this.endian, this.wordSwapped);

    /// <summary>
    ///     Write a int array to registers.
    /// </summary>
    /// <param name="slaveAddress">Address of the device to write to.</param>
    /// <param name="startAddress">Address to start writting values at.</param>
    /// <param name="data">Ints to write to device.</param>
    [CLSCompliant(false)]
    public void WriteIntHoldingRegisters(byte slaveAddress, ushort startAddress, int[] data)
  => RegisterFunctions.WriteRegistersFunc(
    slaveAddress,
    startAddress,
    RegisterFunctions.IntToByteValueArrays(data, this.wordSize),
    this.master,
    this.wordSize,
    this.endian, this.wordSwapped);

    /// <summary>
    ///     Write a uint array to registers.
    /// </summary>
    /// <param name="slaveAddress">Address of the device to write to.</param>
    /// <param name="startAddress">Address to start writting values at.</param>
    /// <param name="data">Uints to write to device.</param>
    [CLSCompliant(false)]
    public void WriteUIntHoldingRegisters(byte slaveAddress, ushort startAddress, uint[] data)
  => RegisterFunctions.WriteRegistersFunc(
    slaveAddress,
    startAddress,
    RegisterFunctions.UIntToByteValueArrays(data, this.wordSize),
    this.master,
    this.wordSize,
    this.endian, this.wordSwapped);

    /// <summary>
    ///     Write a float array to registers.
    /// </summary>
    /// <param name="slaveAddress">Address of the device to write to.</param>
    /// <param name="startAddress">Address to start writting values at.</param>
    /// <param name="data">Floats to write to device.</param>
    [CLSCompliant(false)]
    public void WriteFloatHoldingRegisters(byte slaveAddress, ushort startAddress, float[] data)
  => RegisterFunctions.WriteRegistersFunc(
    slaveAddress,
    startAddress,
    RegisterFunctions.FloatToByteValueArrays(data, this.wordSize),
    this.master,
    this.wordSize,
    this.endian, this.wordSwapped);
}
