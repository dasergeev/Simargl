
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
using System;
using Simargl.Zero.Ssh.Renci.SshNet.Sftp.Responses;

namespace Simargl.Zero.Ssh.Renci.SshNet.Sftp.Requests;

internal abstract class SftpExtendedRequest : SftpRequest
{
    private byte[] _nameBytes;
    private string _name;

    public override SftpMessageTypes SftpMessageType
    {
        get { return SftpMessageTypes.Extended; }
    }

    public string Name
    {
        get
        {
            return _name;
        }
        private set
        {
            _name = value;
            _nameBytes = Utf8.GetBytes(value);
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
            capacity += 4; // Name length
            capacity += _nameBytes.Length; // Name
            return capacity;
        }
    }

    protected SftpExtendedRequest(uint protocolVersion, uint requestId, Action<SftpStatusResponse> statusAction, string name)
        : base(protocolVersion, requestId, statusAction)
    {
        Name = name;
    }

    protected override void SaveData()
    {
        base.SaveData();
        WriteBinaryString(_nameBytes);
    }
}
