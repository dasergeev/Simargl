using Simargl.Zero.Ssh.Renci.SshNet.Common;

namespace Simargl.Zero.Ssh.Renci.SshNet.Sftp.Responses;

internal abstract class ExtendedReplyInfo
{
    public abstract void LoadData(SshDataStream stream);
}
