
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
namespace Simargl.Zero.Ssh.Renci.SshNet.Sftp.Responses;

internal sealed class SftpAttrsResponse : SftpResponse
{
    public override SftpMessageTypes SftpMessageType
    {
        get { return SftpMessageTypes.Attrs; }
    }

    public SftpFileAttributes Attributes { get; private set; }

    public SftpAttrsResponse(uint protocolVersion)
        : base(protocolVersion)
    {
    }

    protected override void LoadData()
    {
        base.LoadData();

        Attributes = ReadAttributes();
    }
}
