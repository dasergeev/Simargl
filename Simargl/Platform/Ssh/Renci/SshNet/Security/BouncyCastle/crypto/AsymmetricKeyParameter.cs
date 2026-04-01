
#pragma warning disable CS8765 // Допустимость значений NULL для типа параметра не соответствует переопределенному элементу (возможно, из-за атрибутов допустимости значений NULL).
#pragma warning disable IDE0019 // Используйте сопоставление шаблонов
#pragma warning disable CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
namespace Simargl.Zero.Ssh.Renci.SshNet.Security.Org.BouncyCastle.Crypto;

internal abstract class AsymmetricKeyParameter
{
    private readonly bool privateKey;

    protected AsymmetricKeyParameter(
        bool privateKey)
    {
        this.privateKey = privateKey;
    }

    public bool IsPrivate
    {
        get { return privateKey; }
    }

    public override bool Equals(
        object obj)
    {
        AsymmetricKeyParameter other = obj as AsymmetricKeyParameter;

        if (other == null)
        {
            return false;
        }

        return Equals(other);
    }

    protected bool Equals(
        AsymmetricKeyParameter other)
    {
        return privateKey == other.privateKey;
    }

    public override int GetHashCode()
    {
        return privateKey.GetHashCode();
    }
}
