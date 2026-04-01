#pragma warning disable CS8625 // Литерал, равный NULL, не может быть преобразован в ссылочный тип, не допускающий значение NULL.
using System;

using Simargl.Zero.Ssh.Renci.SshNet.Security.Org.BouncyCastle.Math.EC;
using Simargl.Zero.Ssh.Renci.SshNet.Security.Org.BouncyCastle.Utilities;

namespace Simargl.Zero.Ssh.Renci.SshNet.Security.Org.BouncyCastle.Asn1.X9;

internal class X9Curve
{
    private readonly ECCurve curve;
    private readonly byte[] seed;

    public X9Curve(
        ECCurve curve)
        : this(curve, null)
    {
    }

    public X9Curve(
        ECCurve	curve,
        byte[]	seed)
    {
        if (curve == null)
            throw new ArgumentNullException("curve");

        this.curve = curve;
        this.seed = Arrays.Clone(seed);
    }

    public ECCurve Curve
    {
        get { return curve; }
    }

    public byte[] GetSeed()
    {
        return Arrays.Clone(seed);
    }
}