
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
using System;
using System.Text;
using Simargl.Zero.Ssh.Renci.SshNet.Sftp.Responses;

namespace Simargl.Zero.Ssh.Renci.SshNet.Sftp.Requests;

internal sealed class SftpRemoveRequest : SftpRequest
{
    private byte[] _fileName;

    public override SftpMessageTypes SftpMessageType
    {
        get { return SftpMessageTypes.Remove; }
    }

    public string Filename
    {
        get { return Encoding.GetString(_fileName, 0, _fileName.Length); }
        private set { _fileName = Encoding.GetBytes(value); }
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
            capacity += 4; // FileName length
            capacity += _fileName.Length; // FileName
            return capacity;
        }
    }

    public SftpRemoveRequest(uint protocolVersion, uint requestId, string filename, Encoding encoding, Action<SftpStatusResponse> statusAction)
        : base(protocolVersion, requestId, statusAction)
    {
        Encoding = encoding;
        Filename = filename;
    }

    protected override void LoadData()
    {
        base.LoadData();
        _fileName = ReadBinary();
    }

    protected override void SaveData()
    {
        base.SaveData();
        WriteBinaryString(_fileName);
    }
}
