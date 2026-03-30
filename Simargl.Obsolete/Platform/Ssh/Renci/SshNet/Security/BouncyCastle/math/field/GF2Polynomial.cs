
#pragma warning disable CS8765 // Допустимость значений NULL для типа параметра не соответствует переопределенному элементу (возможно, из-за атрибутов допустимости значений NULL).
#pragma warning disable IDE0019 // Используйте сопоставление шаблонов
#pragma warning disable CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
using Simargl.Zero.Ssh.Renci.SshNet.Security.Org.BouncyCastle.Utilities;

namespace Simargl.Zero.Ssh.Renci.SshNet.Security.Org.BouncyCastle.Math.Field;

internal class GF2Polynomial
    : IPolynomial
{
    protected readonly int[] exponents;

    internal GF2Polynomial(int[] exponents)
    {
        this.exponents = Arrays.Clone(exponents);
    }

    public virtual int Degree
    {
        get { return exponents[exponents.Length - 1]; }
    }

    public virtual int[] GetExponentsPresent()
    {
        return Arrays.Clone(exponents);
    }

    public override bool Equals(object obj)
    {
        if (this == obj)
        {
            return true;
        }
        GF2Polynomial other = obj as GF2Polynomial;
        if (null == other)
        {
            return false;
        }
        return Arrays.AreEqual(exponents, other.exponents);
    }

    public override int GetHashCode()
    {
        return Arrays.GetHashCode(exponents);
    }
}
