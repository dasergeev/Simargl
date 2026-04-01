using System;

using Simargl.Zero.Ssh.Renci.SshNet.Common;

namespace Simargl.Zero.Ssh.Renci.SshNet;

/// <summary>
/// Provides additional information for asynchronous command execution.
/// </summary>
internal class ExpectAsyncResult : AsyncResult<string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExpectAsyncResult" /> class.
    /// </summary>
    /// <param name="asyncCallback">The async callback.</param>
    /// <param name="state">The state.</param>
    internal ExpectAsyncResult(AsyncCallback asyncCallback, object state)
        : base(asyncCallback, state)
    {
    }
}
