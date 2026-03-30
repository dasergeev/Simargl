
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
using System;
using System.Text;
using Simargl.Zero.Ssh.Renci.SshNet.Sftp.Responses;

namespace Simargl.Zero.Ssh.Renci.SshNet.Sftp.Requests;

internal sealed class SftpRmDirRequest : SftpRequest
{
    private byte[] _path;

    public override SftpMessageTypes SftpMessageType
    {
        get { return SftpMessageTypes.RmDir; }
    }

    public string Path
    {
        get { return Encoding.GetString(_path, 0, _path.Length); }
        private set { _path = Encoding.GetBytes(value); }
    }

    public Encoding Encoding { get; private set; }

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

    public SftpRmDirRequest(uint protocolVersion, uint requestId, string path, Encoding encoding, Action<SftpStatusResponse> statusAction)
        : base(protocolVersion, requestId, statusAction)
    {
        Encoding = encoding;
        Path = path;
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
}
