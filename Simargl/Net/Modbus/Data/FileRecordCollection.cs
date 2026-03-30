using Simargl.Net.Modbus.Unme.Common;

namespace Simargl.Net.Modbus.Data;

/// <summary>
/// 
/// </summary>
public class FileRecordCollection : IModbusMessageDataCollection
{
    private IReadOnlyList<byte> networkBytes;
    private IReadOnlyList<byte> dataBytes;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileNumber"></param>
    /// <param name="startingAddress"></param>
    /// <param name="data"></param>
    [CLSCompliant(false)]
    public FileRecordCollection(ushort fileNumber, ushort startingAddress, byte[] data)
    {
        networkBytes = null!;
        dataBytes = null!;
        Build(fileNumber, startingAddress, data);
        FileNumber = fileNumber;
        StartingAddress = startingAddress;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="messageFrame"></param>
    public FileRecordCollection(byte[] messageFrame)
    {
        networkBytes = null!;
        dataBytes = null!;
        var fileNumber = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(messageFrame, 4));
        var startingAdress = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(messageFrame, 6));
        var count = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(messageFrame, 8));
        var data = messageFrame.Slice(10, count * 2).ToArray();

        Build(fileNumber, startingAdress, data);
        FileNumber = fileNumber;
        StartingAddress = startingAdress;
    }

    private void Build(ushort fileNumber, ushort startingAddress, byte[] data)
    {
        if (data.Length % 2 != 0)
        {
            throw new FormatException("Number of bytes has to be even");
        }

        var values = new List<byte>
        {
            6, // Reference type, demanded by standard definition
        };
        
        void addAsBytes(int value)
        {
            values.AddRange(BitConverter.GetBytes((ushort)IPAddress.HostToNetworkOrder((short)value)));
        }

        addAsBytes(fileNumber);
        addAsBytes(startingAddress);
        addAsBytes(data.Length / 2);
        
        values.AddRange(data);

        dataBytes = data;
        networkBytes = values;
    }

    /// <summary>
    /// The Extended Memory file number
    /// </summary>
    [CLSCompliant(false)]
    public ushort FileNumber { get; }

    /// <summary>
    /// The starting register address within the file.
    /// </summary>
    [CLSCompliant(false)]
    public ushort StartingAddress { get; }

    /// <summary>
    ///  The bytes written to the extended memory file.
    /// </summary>
    public IReadOnlyList<byte> DataBytes => dataBytes;

    /// <summary>
    /// 
    /// </summary>
    public byte[] NetworkBytes => networkBytes.ToArray();

    /// <summary>
    ///     Gets the byte count.
    /// </summary>
    public byte ByteCount => (byte)networkBytes.Count;

    /// <summary>
    ///     Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
    /// </summary>
    /// <returns>
    ///     A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
    /// </returns>
    public override string ToString()
    {
        return string.Concat("{", string.Join(", ", this.networkBytes.Select(v => v.ToString()).ToArray()), "}");
    }
}
