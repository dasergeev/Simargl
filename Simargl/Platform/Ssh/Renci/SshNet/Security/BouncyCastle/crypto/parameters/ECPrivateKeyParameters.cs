
#pragma warning disable IDE0019 // »спользуйте сопоставление шаблонов
#pragma warning disable CS8600 // ѕреобразование литерала, допускающего значение NULL или возможного значени€ NULL в тип, не допускающий значение NULL.
using System;

using Simargl.Zero.Ssh.Renci.SshNet.Security.Org.BouncyCastle.Math;

namespace Simargl.Zero.Ssh.Renci.SshNet.Security.Org.BouncyCastle.Crypto.Parameters;

internal class ECPrivateKeyParameters
    : ECKeyParameters
{
    private readonly BigInteger d;

    public ECPrivateKeyParameters(
        BigInteger			d,
        ECDomainParameters	parameters)
        : this("EC", d, parameters)
    {
    }

    public ECPrivateKeyParameters(
        string				algorithm,
        BigInteger			d,
        ECDomainParameters	parameters)
        : base(algorithm, true, parameters)
    {
        if (d == null)
            throw new ArgumentNullException("d");

        this.d = d;
    }

    public BigInteger D
    {
        get { return d; }
    }

    public override bool Equals(
        object obj)
    {
        if (obj == this)
            return true;

        ECPrivateKeyParameters other = obj as ECPrivateKeyParameters;

        if (other == null)
            return false;

        return Equals(other);
    }

    protected bool Equals(
        ECPrivateKeyParameters other)
    {
        return d.Equals(other.d) && base.Equals(other);
    }

    public override int GetHashCode()
    {
        return d.GetHashCode() ^ base.GetHashCode();
    }
}