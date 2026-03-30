
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
namespace Simargl.Zero.Ssh.Renci.SshNet.Common;

/// <summary>
/// Provides data for <see cref="PasswordConnectionInfo.PasswordExpired"/> event.
/// </summary>
internal class AuthenticationPasswordChangeEventArgs : AuthenticationEventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationPasswordChangeEventArgs"/> class.
    /// </summary>
    /// <param name="username">The username.</param>
    public AuthenticationPasswordChangeEventArgs(string username)
        : base(username)
    {
    }

    /// <summary>
    /// Gets or sets the new password.
    /// </summary>
    /// <value>
    /// The new password.
    /// </value>
    public byte[] NewPassword { get; set; }
}
