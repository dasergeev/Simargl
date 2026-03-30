
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
using System;
using Simargl.Zero.Ssh.Renci.SshNet.Sftp.Responses;

namespace Simargl.Zero.Ssh.Renci.SshNet.Sftp.Requests;

internal sealed class SftpFSetStatRequest : SftpRequest
{
    private byte[] _attributesBytes;

    public override SftpMessageTypes SftpMessageType
    {
        get { return SftpMessageTypes.FSetStat; }
    }

    public byte[] Handle { get; private set; }

    private SftpFileAttributes Attributes { get; set; }

    private byte[] AttributesBytes
    {
        get
        {
            _attributesBytes ??= Attributes.GetBytes();
            return _attributesBytes;
        }
    }

    /// <summary>
    /// Gets the size of the message in bytes.
    /// </summary>
    /// <value>
    /// The size of the messages in bytes.
    /// </value>
    protected override int BufferCapacity
    {
        get
        {
            var capacity = base.BufferCapacity;
            capacity += 4; // Handle length
            capacity += Handle.Length; // Handle
            capacity += AttributesBytes.Length; // Attributes
            return capacity;
        }
    }

    public SftpFSetStatRequest(uint protocolVersion, uint requestId, byte[] handle, SftpFileAttributes attributes, Action<SftpStatusResponse> statusAction)
        : base(protocolVersion, requestId, statusAction)
    {
        Handle = handle;
        Attributes = attributes;
    }

    protected override void LoadData()
    {
        base.LoadData();

        Handle = ReadBinary();
        Attributes = ReadAttributes();
    }

    protected override void SaveData()
    {
        base.SaveData();

        WriteBinaryString(Handle);
        Write(AttributesBytes);
    }
}
