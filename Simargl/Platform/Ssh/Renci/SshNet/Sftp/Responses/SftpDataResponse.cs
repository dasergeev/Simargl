
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
namespace Simargl.Zero.Ssh.Renci.SshNet.Sftp.Responses;

internal sealed class SftpDataResponse : SftpResponse
{
    public override SftpMessageTypes SftpMessageType
    {
        get { return SftpMessageTypes.Data; }
    }

    public byte[] Data { get; set; }

    public SftpDataResponse(uint protocolVersion)
        : base(protocolVersion)
    {
    }

    protected override void LoadData()
    {
        base.LoadData();

        Data = ReadBinary();
    }

    protected override void SaveData()
    {
        base.SaveData();

        WriteBinary(Data, 0, Data.Length);
    }
}
