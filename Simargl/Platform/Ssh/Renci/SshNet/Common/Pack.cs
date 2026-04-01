using System;
using System.Buffers.Binary;

namespace Simargl.Zero.Ssh.Renci.SshNet.Common;

/// <summary>
/// Provides convenience methods for conversion to and from both Big Endian and Little Endian.
/// </summary>
internal static class Pack
{
    /// <summary>
    /// Converts little endian bytes into number.
    /// </summary>
    /// <param name="buffer">The buffer.</param>
    /// <returns>Converted <see cref="ushort" />.</returns>
    internal static ushort LittleEndianToUInt16(byte[] buffer)
    {
        return BinaryPrimitives.ReadUInt16LittleEndian(buffer);
    }

    /// <summary>
    /// Converts little endian bytes into number.
    /// </summary>
    /// <param name="buffer">The buffer.</param>
    /// <returns>Converted <see cref="uint" />.</returns>
    internal static uint LittleEndianToUInt32(byte[] buffer)
    {
        return BinaryPrimitives.ReadUInt32LittleEndian(buffer);
    }

    /// <summary>
    /// Converts little endian bytes into number.
    /// </summary>
    /// <param name="buffer">The buffer.</param>
    /// <returns>Converted <see cref="ulong" />.</returns>
    internal static ulong LittleEndianToUInt64(byte[] buffer)
    {
        return BinaryPrimitives.ReadUInt64LittleEndian(buffer);
    }

    /// <summary>
    /// Populates buffer with little endian number representation.
    /// </summary>
    /// <param name="value">The number to convert.</param>
    internal static byte[] UInt16ToLittleEndian(ushort value)
    {
        var buffer = new byte[2];
        UInt16ToLittleEndian(value, buffer);
        return buffer;
    }

    /// <summary>
    /// Populates buffer with little endian number representation.
    /// </summary>
    /// <param name="value">The number to convert.</param>
    /// <param name="buffer">The buffer.</param>
    private static void UInt16ToLittleEndian(ushort value, byte[] buffer)
    {
        BinaryPrimitives.WriteUInt16LittleEndian(buffer, value);
    }

    /// <summary>
    /// Populates buffer with little endian number representation.
    /// </summary>
    /// <param name="value">The number to convert.</param>
    internal static byte[] UInt32ToLittleEndian(uint value)
    {
        var buffer = new byte[4];
        UInt32ToLittleEndian(value, buffer);
        return buffer;
    }

    /// <summary>
    /// Populates buffer with little endian number representation.
    /// </summary>
    /// <param name="value">The number to convert.</param>
    /// <param name="buffer">The buffer.</param>
    private static void UInt32ToLittleEndian(uint value, byte[] buffer)
    {
        BinaryPrimitives.WriteUInt32LittleEndian(buffer, value);
    }

    /// <summary>
    /// Populates buffer with little endian number representation.
    /// </summary>
    /// <param name="value">The number to convert.</param>
    internal static byte[] UInt64ToLittleEndian(ulong value)
    {
        var buffer = new byte[8];
        UInt64ToLittleEndian(value, buffer);
        return buffer;
    }

    /// <summary>
    /// Populates buffer with little endian number representation.
    /// </summary>
    /// <param name="value">The number to convert.</param>
    /// <param name="buffer">The buffer.</param>
    private static void UInt64ToLittleEndian(ulong value, byte[] buffer)
    {
        BinaryPrimitives.WriteUInt64LittleEndian(buffer, value);
    }

    internal static byte[] UInt16ToBigEndian(ushort value)
    {
        var buffer = new byte[2];
        UInt16ToBigEndian(value, buffer, offset: 0);
        return buffer;
    }

    internal static void UInt16ToBigEndian(ushort value, byte[] buffer, int offset)
    {
        BinaryPrimitives.WriteUInt16BigEndian(buffer.AsSpan(offset), value);
    }

    internal static void UInt32ToBigEndian(uint value, byte[] buffer)
    {
        UInt32ToBigEndian(value, buffer, offset: 0);
    }

    internal static void UInt32ToBigEndian(uint value, byte[] buffer, int offset)
    {
        BinaryPrimitives.WriteUInt32BigEndian(buffer.AsSpan(offset), value);
    }

    internal static byte[] UInt32ToBigEndian(uint value)
    {
        var buffer = new byte[4];
        UInt32ToBigEndian(value, buffer);
        return buffer;
    }

    internal static byte[] UInt64ToBigEndian(ulong value)
    {
        var buffer = new byte[8];
        UInt64ToBigEndian(value, buffer, offset: 0);
        return buffer;
    }

    private static void UInt64ToBigEndian(ulong value, byte[] buffer, int offset)
    {
        BinaryPrimitives.WriteUInt64BigEndian(buffer.AsSpan(offset), value);
    }

    /// <summary>
    /// Converts big endian bytes into number.
    /// </summary>
    /// <param name="buffer">The buffer.</param>
    /// <returns>Converted <see cref="ushort" />.</returns>
    internal static ushort BigEndianToUInt16(byte[] buffer)
    {
        return BinaryPrimitives.ReadUInt16BigEndian(buffer);
    }

    /// <summary>
    /// Converts big endian bytes into number.
    /// </summary>
    /// <param name="buffer">The buffer.</param>
    /// <param name="offset">The buffer offset.</param>
    /// <returns>Converted <see cref="uint" />.</returns>
    internal static uint BigEndianToUInt32(byte[] buffer, int offset)
    {
        return BinaryPrimitives.ReadUInt32BigEndian(buffer.AsSpan(offset));
    }

    /// <summary>
    /// Converts big endian bytes into number.
    /// </summary>
    /// <param name="buffer">The buffer.</param>
    /// <returns>Converted <see cref="uint" />.</returns>
    internal static uint BigEndianToUInt32(byte[] buffer)
    {
        return BigEndianToUInt32(buffer, offset: 0);
    }

    /// <summary>
    /// Converts big endian bytes into number.
    /// </summary>
    /// <param name="buffer">The buffer.</param>
    /// <returns>Converted <see cref="ulong" />.</returns>
    internal static ulong BigEndianToUInt64(byte[] buffer)
    {
        return BinaryPrimitives.ReadUInt64BigEndian(buffer);
    }
}
