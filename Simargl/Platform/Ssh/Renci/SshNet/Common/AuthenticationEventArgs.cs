using System;

namespace Simargl.Zero.Ssh.Renci.SshNet.Common;

/// <summary>
/// Base class for authentication events.
/// </summary>
internal abstract class AuthenticationEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationEventArgs"/> class.
    /// </summary>
    /// <param name="username">The username.</param>
    protected AuthenticationEventArgs(string username)
    {
        Username = username;
    }

    /// <summary>
    /// Gets the username.
    /// </summary>
    public string Username { get; }
}
