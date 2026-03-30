
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
namespace Simargl.Zero.Ssh.Renci.SshNet.Security.Org.BouncyCastle.Asn1.X9;

internal abstract class X9ECParametersHolder
{
    private X9ECParameters parameters;

    public X9ECParameters Parameters
    {
        get
        {
            lock (this)
            {
                if (parameters == null)
                {
                    parameters = CreateParameters();
                }

                return parameters;
            }
        }
    }

    protected abstract X9ECParameters CreateParameters();
}
