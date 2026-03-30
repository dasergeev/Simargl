
#pragma warning disable CS8622 // Допустимость значений NULL для ссылочных типов в типе параметра не соответствует целевому объекту делегирования (возможно, из-за атрибутов допустимости значений NULL).
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
using Simargl.Zero.Ssh.Renci.SshNet.Abstractions;
using Simargl.Zero.Ssh.Renci.SshNet.Common;
using Simargl.Zero.Ssh.Renci.SshNet.Messages.Transport;
using Simargl.Zero.Ssh.Renci.SshNet.Security.Chaos.NaCl;
using Simargl.Zero.Ssh.Renci.SshNet.Security.Chaos.NaCl.Internal.Ed25519Ref10;

namespace Simargl.Zero.Ssh.Renci.SshNet.Security;

internal sealed class KeyExchangeECCurve25519 : KeyExchangeEC
{
    private byte[] _privateKey;

    /// <summary>
    /// Gets algorithm name.
    /// </summary>
    public override string Name
    {
        get { return "curve25519-sha256"; }
    }

    /// <summary>
    /// Gets the size, in bits, of the computed hash code.
    /// </summary>
    /// <value>
    /// The size, in bits, of the computed hash code.
    /// </value>
    protected override int HashSize
    {
        get { return 256; }
    }

    /// <inheritdoc/>
    public override void Start(Session session, KeyExchangeInitMessage message, bool sendClientInitMessage)
    {
        base.Start(session, message, sendClientInitMessage);

        Session.RegisterMessage("SSH_MSG_KEX_ECDH_REPLY");

        Session.KeyExchangeEcdhReplyMessageReceived += Session_KeyExchangeEcdhReplyMessageReceived;

        var basepoint = new byte[MontgomeryCurve25519.PublicKeySizeInBytes];
        basepoint[0] = 9;

        _privateKey = CryptoAbstraction.GenerateRandom(MontgomeryCurve25519.PrivateKeySizeInBytes);

        _clientExchangeValue = new byte[MontgomeryCurve25519.PublicKeySizeInBytes];
        MontgomeryOperations.scalarmult(_clientExchangeValue, 0, _privateKey, 0, basepoint, 0);

        SendMessage(new KeyExchangeEcdhInitMessage(_clientExchangeValue));
    }

    /// <summary>
    /// Finishes key exchange algorithm.
    /// </summary>
    public override void Finish()
    {
        base.Finish();

        Session.KeyExchangeEcdhReplyMessageReceived -= Session_KeyExchangeEcdhReplyMessageReceived;
    }

    /// <summary>
    /// Hashes the specified data bytes.
    /// </summary>
    /// <param name="hashData">The hash data.</param>
    /// <returns>
    /// The hash of the data.
    /// </returns>
    protected override byte[] Hash(byte[] hashData)
    {
        using (var sha256 = CryptoAbstraction.CreateSHA256())
        {
            return sha256.ComputeHash(hashData, 0, hashData.Length);
        }
    }

    private void Session_KeyExchangeEcdhReplyMessageReceived(object sender, MessageEventArgs<KeyExchangeEcdhReplyMessage> e)
    {
        var message = e.Message;

        // Unregister message once received
        Session.UnRegisterMessage("SSH_MSG_KEX_ECDH_REPLY");

        HandleServerEcdhReply(message.KS, message.QS, message.Signature);

        // When SSH_MSG_KEXDH_REPLY received key exchange is completed
        Finish();
    }

    /// <summary>
    /// Handles the server DH reply message.
    /// </summary>
    /// <param name="hostKey">The host key.</param>
    /// <param name="serverExchangeValue">The server exchange value.</param>
    /// <param name="signature">The signature.</param>
    private void HandleServerEcdhReply(byte[] hostKey, byte[] serverExchangeValue, byte[] signature)
    {
        _serverExchangeValue = serverExchangeValue;
        _hostKey = hostKey;
        _signature = signature;

        var sharedKey = new byte[MontgomeryCurve25519.PublicKeySizeInBytes];
        MontgomeryOperations.scalarmult(sharedKey, 0, _privateKey, 0, serverExchangeValue, 0);
        SharedKey = sharedKey.ToBigInteger2().ToByteArray().ReverseBytes();
    }
}
