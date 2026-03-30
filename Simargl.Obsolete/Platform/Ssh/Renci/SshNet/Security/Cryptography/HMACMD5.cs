using System.Security.Cryptography;
using Simargl.Zero.Ssh.Renci.SshNet.Common;

namespace Simargl.Zero.Ssh.Renci.SshNet.Security.Cryptography;

/// <summary>
/// Computes a Hash-based Message Authentication Code (HMAC) by using the <see cref="MD5"/> hash function.
/// </summary>
internal class HMACMD5 : System.Security.Cryptography.HMACMD5
{
    private readonly int _hashSize;

    /// <summary>
    /// Initializes a new instance of the <see cref="HMACMD5"/> class with the specified key.
    /// </summary>
    /// <param name="key">The key.</param>
    public HMACMD5(byte[] key)
#pragma warning disable CA5351 // Do Not Use Broken Cryptographic Algorithms
        : base(key)
#pragma warning restore CA5351 // Do Not Use Broken Cryptographic Algorithms
    {
        _hashSize = base.HashSize;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HMACMD5"/> class with the specified key
    /// and size of the computed hash code.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="hashSize">The size, in bits, of the computed hash code.</param>
    public HMACMD5(byte[] key, int hashSize)
#pragma warning disable CA5351 // Do Not Use Broken Cryptographic Algorithms
        : base(key)
#pragma warning restore CA5351 // Do Not Use Broken Cryptographic Algorithms
    {
        _hashSize = hashSize;
    }

    /// <summary>
    /// Gets the size, in bits, of the computed hash code.
    /// </summary>
    /// <value>
    /// The size, in bits, of the computed hash code.
    /// </value>
    public override int HashSize
    {
        get { return _hashSize; }
    }

    /// <summary>
    /// Finalizes the hash computation after the last data is processed by the cryptographic stream object.
    /// </summary>
    /// <returns>
    /// The computed hash code.
    /// </returns>
    protected override byte[] HashFinal()
    {
#pragma warning disable CA5351 // Do Not Use Broken Cryptographic Algorithms
        var hash = base.HashFinal();
#pragma warning restore CA5351 // Do Not Use Broken Cryptographic Algorithms
        return hash.Take(HashSize / 8);
    }
}
