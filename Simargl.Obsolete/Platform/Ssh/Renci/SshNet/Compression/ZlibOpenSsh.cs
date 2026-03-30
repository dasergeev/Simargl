
#pragma warning disable CS8622 // Допустимость значений NULL для ссылочных типов в типе параметра не соответствует целевому объекту делегирования (возможно, из-за атрибутов допустимости значений NULL).
using Simargl.Zero.Ssh.Renci.SshNet.Messages.Authentication;

namespace Simargl.Zero.Ssh.Renci.SshNet.Compression;

/// <summary>
/// Represents "zlib@openssh.org" compression implementation.
/// </summary>
internal class ZlibOpenSsh : Compressor
{
    /// <summary>
    /// Gets algorithm name.
    /// </summary>
    public override string Name
    {
        get { return "zlib@openssh.org"; }
    }

    /// <summary>
    /// Initializes the algorithm.
    /// </summary>
    /// <param name="session">The session.</param>
    public override void Init(Session session)
    {
        base.Init(session);

        session.UserAuthenticationSuccessReceived += Session_UserAuthenticationSuccessReceived;
    }

    private void Session_UserAuthenticationSuccessReceived(object sender, MessageEventArgs<SuccessMessage> e)
    {
        IsActive = true;
        Session.UserAuthenticationSuccessReceived -= Session_UserAuthenticationSuccessReceived;
    }
}
