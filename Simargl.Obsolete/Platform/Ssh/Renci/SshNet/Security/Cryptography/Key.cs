
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
using Simargl.Zero.Ssh.Renci.SshNet.Common;
using Simargl.Zero.Ssh.Renci.SshNet.Security.Cryptography;

namespace Simargl.Zero.Ssh.Renci.SshNet.Security;

/// <summary>
/// Base class for asymmetric cipher algorithms.
/// </summary>
internal abstract class Key
{
    /// <summary>
    /// Gets the default digital signature implementation for this key.
    /// </summary>
    protected internal abstract DigitalSignature DigitalSignature { get; }

    /// <summary>
    /// Gets the public key.
    /// </summary>
    /// <value>
    /// The public.
    /// </value>
    public abstract BigInteger[] Public { get; }

    /// <summary>
    /// Gets the length of the key.
    /// </summary>
    /// <value>
    /// The length of the key.
    /// </value>
    public abstract int KeyLength { get; }

    /// <summary>
    /// Gets or sets the key comment.
    /// </summary>
    public string Comment { get; set; }

    /// <summary>
    /// Signs the specified data with the key.
    /// </summary>
    /// <param name="data">The data to sign.</param>
    /// <returns>
    /// Signed data.
    /// </returns>
    public byte[] Sign(byte[] data)
    {
        return DigitalSignature.Sign(data);
    }

    /// <summary>
    /// Verifies the signature.
    /// </summary>
    /// <param name="data">The data to verify.</param>
    /// <param name="signature">The signature to verify against.</param>
    /// <returns><see langword="true"/> is signature was successfully verifies; otherwise <see langword="false"/>.</returns>
    public bool VerifySignature(byte[] data, byte[] signature)
    {
        return DigitalSignature.Verify(data, signature);
    }
}
