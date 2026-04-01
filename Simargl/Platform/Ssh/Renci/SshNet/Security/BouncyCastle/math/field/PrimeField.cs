
#pragma warning disable CS8765 // Допустимость значений NULL для типа параметра не соответствует переопределенному элементу (возможно, из-за атрибутов допустимости значений NULL).
#pragma warning disable IDE0019 // Используйте сопоставление шаблонов
#pragma warning disable CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
namespace Simargl.Zero.Ssh.Renci.SshNet.Security.Org.BouncyCastle.Math.Field;

internal class PrimeField
    : IFiniteField
{
    protected readonly BigInteger characteristic;

    internal PrimeField(BigInteger characteristic)
    {
        this.characteristic = characteristic;
    }

    public virtual BigInteger Characteristic
    {
        get { return characteristic; }
    }

    public virtual int Dimension
    {
        get { return 1; }
    }

    public override bool Equals(object obj)
    {
        if (this == obj)
        {
            return true;
        }
        PrimeField other = obj as PrimeField;
        if (null == other)
        {
            return false;
        }
        return characteristic.Equals(other.characteristic);
    }

    public override int GetHashCode()
    {
        return characteristic.GetHashCode();
    }
}