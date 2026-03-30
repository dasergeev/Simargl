
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
using Simargl.Zero.Ssh.Renci.SshNet.Common;

namespace Simargl.Zero.Ssh.Renci.SshNet.Sftp.Responses;

internal sealed class StatVfsReplyInfo : ExtendedReplyInfo
{
    public SftpFileSytemInformation Information { get; private set; }

    public override void LoadData(SshDataStream stream)
    {
        Information = new SftpFileSytemInformation(stream.ReadUInt64(), // FileSystemBlockSize
                                                   stream.ReadUInt64(), // BlockSize
                                                   stream.ReadUInt64(), // TotalBlocks
                                                   stream.ReadUInt64(), // FreeBlocks
                                                   stream.ReadUInt64(), // AvailableBlocks
                                                   stream.ReadUInt64(), // TotalNodes
                                                   stream.ReadUInt64(), // FreeNodes
                                                   stream.ReadUInt64(), // AvailableNodes
                                                   stream.ReadUInt64(), // Sid
                                                   stream.ReadUInt64(), // Flags
                                                   stream.ReadUInt64()); // MaxNameLenght
    }
}
