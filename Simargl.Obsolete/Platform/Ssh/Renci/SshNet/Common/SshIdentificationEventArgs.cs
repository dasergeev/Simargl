using System;

using Simargl.Zero.Ssh.Renci.SshNet.Connection;

namespace Simargl.Zero.Ssh.Renci.SshNet.Common;

/// <summary>
/// Provides data for the ServerIdentificationReceived events.
/// </summary>
internal class SshIdentificationEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SshIdentificationEventArgs"/> class.
    /// </summary>
    /// <param name="sshIdentification">The SSH identification.</param>
    public SshIdentificationEventArgs(SshIdentification sshIdentification)
    {
        SshIdentification = sshIdentification;
    }

    /// <summary>
    /// Gets the SSH identification.
    /// </summary>
    public SshIdentification SshIdentification { get; private set; }
}
