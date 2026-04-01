using System;

using Simargl.Zero.Ssh.Renci.SshNet.Common;

namespace Simargl.Zero.Ssh.Renci.SshNet.Sftp;

internal sealed class SFtpStatAsyncResult : AsyncResult<SftpFileAttributes>
{
    public SFtpStatAsyncResult(AsyncCallback asyncCallback, object state)
        : base(asyncCallback, state)
    {
    }
}
