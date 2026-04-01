
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
using System;
using System.Text;

using Simargl.Zero.Ssh.Renci.SshNet.Sftp.Responses;

namespace Simargl.Zero.Ssh.Renci.SshNet.Sftp.Requests;

internal sealed class PosixRenameRequest : SftpExtendedRequest
{
    private byte[] _oldPath;
    private byte[] _newPath;

    public string OldPath
    {
        get { return Encoding.GetString(_oldPath, 0, _oldPath.Length); }
        private set { _oldPath = Encoding.GetBytes(value); }
    }

    public string NewPath
    {
        get { return Encoding.GetString(_newPath, 0, _newPath.Length); }
        private set { _newPath = Encoding.GetBytes(value); }
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
            capacity += 4; // OldPath length
            capacity += _oldPath.Length; // OldPath
            capacity += 4; // NewPath length
            capacity += _newPath.Length; // NewPath
            return capacity;
        }
    }

    public PosixRenameRequest(uint protocolVersion, uint requestId, string oldPath, string newPath, Encoding encoding, Action<SftpStatusResponse> statusAction)
        : base(protocolVersion, requestId, statusAction, "posix-rename@openssh.com")
    {
        Encoding = encoding;
        OldPath = oldPath;
        NewPath = newPath;
    }

    protected override void SaveData()
    {
        base.SaveData();

        WriteBinaryString(_oldPath);
        WriteBinaryString(_newPath);
    }
}
