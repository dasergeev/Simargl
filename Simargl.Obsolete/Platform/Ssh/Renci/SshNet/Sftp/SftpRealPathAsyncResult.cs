using System;

using Simargl.Zero.Ssh.Renci.SshNet.Common;

namespace Simargl.Zero.Ssh.Renci.SshNet.Sftp;

internal sealed class SftpRealPathAsyncResult : AsyncResult<string>
{
    public SftpRealPathAsyncResult(AsyncCallback asyncCallback, object state)
        : base(asyncCallback, state)
    {
    }
}
