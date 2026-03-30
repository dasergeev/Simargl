
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
using System.Collections.Generic;

namespace Simargl.Zero.Ssh.Renci.SshNet.Sftp.Responses;

internal sealed class SftpVersionResponse : SftpMessage
{
    public override SftpMessageTypes SftpMessageType
    {
        get { return SftpMessageTypes.Version; }
    }

    public uint Version { get; set; }

    public IDictionary<string, string> Extentions { get; set; }

    protected override void LoadData()
    {
        base.LoadData();

        Version = ReadUInt32();
        Extentions = ReadExtensionPair();
    }

    protected override void SaveData()
    {
        base.SaveData();

        Write(Version);

        if (Extentions != null)
        {
            Write(Extentions);
        }
    }
}
