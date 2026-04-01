using System;

namespace Simargl.Zero.Ssh.Renci.SshNet;

/// <summary>
/// Supports port forwarding functionality.
/// </summary>
internal interface IForwardedPort : IDisposable
{
    /// <summary>
    /// The <see cref="Closing"/> event occurs as the forwarded port is being stopped.
    /// </summary>
    event EventHandler Closing;
}
