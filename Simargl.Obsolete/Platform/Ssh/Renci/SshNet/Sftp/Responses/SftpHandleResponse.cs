
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
namespace Simargl.Zero.Ssh.Renci.SshNet.Sftp.Responses;

internal sealed class SftpHandleResponse : SftpResponse
{
    public override SftpMessageTypes SftpMessageType
    {
        get { return SftpMessageTypes.Handle; }
    }

    public byte[] Handle { get; set; }

    public SftpHandleResponse(uint protocolVersion)
        : base(protocolVersion)
    {
    }

    protected override void LoadData()
    {
        base.LoadData();

        Handle = ReadBinary();
    }

    protected override void SaveData()
    {
        base.SaveData();

        WriteBinary(Handle, 0, Handle.Length);
    }
}
