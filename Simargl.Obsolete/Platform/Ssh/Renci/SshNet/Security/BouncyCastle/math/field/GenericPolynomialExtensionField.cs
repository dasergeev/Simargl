
#pragma warning disable CS8765 // Допустимость значений NULL для типа параметра не соответствует переопределенному элементу (возможно, из-за атрибутов допустимости значений NULL).
#pragma warning disable IDE0019 // Используйте сопоставление шаблонов
#pragma warning disable CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
using Simargl.Zero.Ssh.Renci.SshNet.Security.Org.BouncyCastle.Utilities;

namespace Simargl.Zero.Ssh.Renci.SshNet.Security.Org.BouncyCastle.Math.Field;

internal class GenericPolynomialExtensionField
    : IPolynomialExtensionField
{
    protected readonly IFiniteField subfield;
    protected readonly IPolynomial minimalPolynomial;

    internal GenericPolynomialExtensionField(IFiniteField subfield, IPolynomial polynomial)
    {
        this.subfield = subfield;
        this.minimalPolynomial = polynomial;
    }

    public virtual BigInteger Characteristic
    {
        get { return subfield.Characteristic; }
    }

    public virtual int Dimension
    {
        get { return subfield.Dimension * minimalPolynomial.Degree; }
    }

    public virtual IFiniteField Subfield
    {
        get { return subfield; }
    }

    public virtual int Degree
    {
        get { return minimalPolynomial.Degree; }
    }

    public virtual IPolynomial MinimalPolynomial
    {
        get { return minimalPolynomial; }
    }

    public override bool Equals(object obj)
    {
        if (this == obj)
        {
            return true;
        }
        GenericPolynomialExtensionField other = obj as GenericPolynomialExtensionField;
        if (null == other)
        {
            return false;
        }
        return subfield.Equals(other.subfield) && minimalPolynomial.Equals(other.minimalPolynomial);
    }

    public override int GetHashCode()
    {
        return subfield.GetHashCode() ^ Integers.RotateLeft(minimalPolynomial.GetHashCode(), 16);
    }
}
