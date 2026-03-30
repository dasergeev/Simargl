
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
using System;
using System.Text;
using Simargl.Zero.Ssh.Renci.SshNet.Sftp.Responses;

namespace Simargl.Zero.Ssh.Renci.SshNet.Sftp.Requests;

internal sealed class SftpStatRequest : SftpRequest
{
    private readonly Action<SftpAttrsResponse> _attrsAction;
    private byte[] _path;

    public override SftpMessageTypes SftpMessageType
    {
        get { return SftpMessageTypes.Stat; }
    }

    public string Path
    {
        get { return Encoding.GetString(_path, 0, _path.Length); }
        private set { _path = Encoding.GetBytes(value); }
    }

    public Encoding Encoding { get; }

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
            capacity += 4; // Path length
            capacity += _path.Length; // Path
            return capacity;
        }
    }

    public SftpStatRequest(uint protocolVersion, uint requestId, string path, Encoding encoding, Action<SftpAttrsResponse> attrsAction, Action<SftpStatusResponse> statusAction)
        : base(protocolVersion, requestId, statusAction)
    {
        Encoding = encoding;
        Path = path;
        _attrsAction = attrsAction;
    }

    protected override void LoadData()
    {
        base.LoadData();

        _path = ReadBinary();
    }

    protected override void SaveData()
    {
        base.SaveData();

        WriteBinaryString(_path);
    }

    public override void Complete(SftpResponse response)
    {
        if (response is SftpAttrsResponse attrsResponse)
        {
            _attrsAction(attrsResponse);
        }
        else
        {
            base.Complete(response);
        }
    }
}
