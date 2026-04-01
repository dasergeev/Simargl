
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
using System;

namespace Simargl.Zero.Ssh.Renci.SshNet;

/// <summary>
/// Base class for all supported authentication methods.
/// </summary>
internal abstract class AuthenticationMethod : IAuthenticationMethod
{
    /// <summary>
    /// Gets the name of the authentication method.
    /// </summary>
    /// <value>
    /// The name of the authentication method.
    /// </value>
    public abstract string Name { get; }

    /// <summary>
    /// Gets connection username.
    /// </summary>
    public string Username { get; private set; }

    /// <summary>
    /// Gets or sets the list of allowed authentications.
    /// </summary>
    public string[] AllowedAuthentications { get; protected set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationMethod"/> class.
    /// </summary>
    /// <param name="username">The username.</param>
    /// <exception cref="ArgumentException"><paramref name="username"/> is whitespace or <see langword="null"/>.</exception>
    protected AuthenticationMethod(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("username");
        }

        Username = username;
    }

    /// <summary>
    /// Authenticates the specified session.
    /// </summary>
    /// <param name="session">The session to authenticate.</param>
    /// <returns>
    /// The result of the authentication process.
    /// </returns>
    public abstract AuthenticationResult Authenticate(Session session);

    /// <summary>
    /// Authenticates the specified session.
    /// </summary>
    /// <param name="session">The session to authenticate.</param>
    /// <returns>
    /// The result of the authentication process.
    /// </returns>
    AuthenticationResult IAuthenticationMethod.Authenticate(ISession session)
    {
        return Authenticate((Session) session);
    }
}
