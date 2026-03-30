using System;

using Simargl.Zero.Ssh.Renci.SshNet.Common;

namespace Simargl.Zero.Ssh.Renci.SshNet.Sftp;

internal sealed class SftpOpenAsyncResult : AsyncResult<byte[]>
{
    public SftpOpenAsyncResult(AsyncCallback asyncCallback, object state)
        : base(asyncCallback, state)
    {
    }
}
