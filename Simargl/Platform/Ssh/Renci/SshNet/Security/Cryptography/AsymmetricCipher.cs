namespace Simargl.Zero.Ssh.Renci.SshNet.Security.Cryptography;

/// <summary>
/// Base class for asymmetric cipher implementations.
/// </summary>
internal abstract class AsymmetricCipher : Cipher
{
    /// <summary>
    /// Gets the minimum data size.
    /// </summary>
    /// <value>
    /// The minimum data size.
    /// </value>
    public override byte MinimumSize
    {
        get { return 0; }
    }
}
