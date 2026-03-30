
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
using System;

using Simargl.Zero.Ssh.Renci.SshNet.Common;
using Simargl.Zero.Ssh.Renci.SshNet.Messages.Transport;

namespace Simargl.Zero.Ssh.Renci.SshNet.Security;

/// <summary>
/// Represents base class for Diffie Hellman key exchange algorithm.
/// </summary>
internal abstract class KeyExchangeDiffieHellman : KeyExchange
{
    /// <summary>
    /// Specifies key exchange group number.
    /// </summary>
    protected BigInteger _group;

    /// <summary>
    /// Specifies key exchange prime number.
    /// </summary>
    protected BigInteger _prime;

    /// <summary>
    /// Specifies client payload.
    /// </summary>
    protected byte[] _clientPayload;

    /// <summary>
    /// Specifies server payload.
    /// </summary>
    protected byte[] _serverPayload;

    /// <summary>
    /// Specifies client exchange number.
    /// </summary>
    protected byte[] _clientExchangeValue;

    /// <summary>
    /// Specifies server exchange number.
    /// </summary>
    protected byte[] _serverExchangeValue;

    /// <summary>
    /// Specifies random generated number.
    /// </summary>
    protected BigInteger _privateExponent;

    /// <summary>
    /// Specifies host key data.
    /// </summary>
    protected byte[] _hostKey;

    /// <summary>
    /// Specifies signature data.
    /// </summary>
    protected byte[] _signature;

    /// <summary>
    /// Gets the size, in bits, of the computed hash code.
    /// </summary>
    /// <value>
    /// The size, in bits, of the computed hash code.
    /// </value>
    protected abstract int HashSize { get; }

    /// <summary>
    /// Validates the exchange hash.
    /// </summary>
    /// <returns>
    /// true if exchange hash is valid; otherwise false.
    /// </returns>
    protected override bool ValidateExchangeHash()
    {
        return ValidateExchangeHash(_hostKey, _signature);
    }

    /// <inheritdoc/>
    public override void Start(Session session, KeyExchangeInitMessage message, bool sendClientInitMessage)
    {
        base.Start(session, message, sendClientInitMessage);

        _serverPayload = message.GetBytes();
        _clientPayload = Session.ClientInitMessage.GetBytes();
    }

    /// <summary>
    /// Populates the client exchange value.
    /// </summary>
    protected void PopulateClientExchangeValue()
    {
        if (_group.IsZero)
        {
            throw new ArgumentNullException("_group");
        }

        if (_prime.IsZero)
        {
            throw new ArgumentNullException("_prime");
        }

        // generate private exponent that is twice the hash size (RFC 4419) with a minimum
        // of 1024 bits (whatever is less)
        var privateExponentSize = Math.Max(HashSize * 2, 1024);

        BigInteger clientExchangeValue;

        do
        {
            // Create private component
            _privateExponent = BigInteger.Random(privateExponentSize);

            // Generate public component
            clientExchangeValue = BigInteger.ModPow(_group, _privateExponent, _prime);
        }
        while (clientExchangeValue < 1 || clientExchangeValue > (_prime - 1));

        _clientExchangeValue = clientExchangeValue.ToByteArray().ReverseBytes();
    }

    /// <summary>
    /// Handles the server DH reply message.
    /// </summary>
    /// <param name="hostKey">The host key.</param>
    /// <param name="serverExchangeValue">The server exchange value.</param>
    /// <param name="signature">The signature.</param>
    protected virtual void HandleServerDhReply(byte[] hostKey, byte[] serverExchangeValue, byte[] signature)
    {
        _serverExchangeValue = serverExchangeValue;
        _hostKey = hostKey;
        SharedKey = BigInteger.ModPow(serverExchangeValue.ToBigInteger(), _privateExponent, _prime).ToByteArray().ReverseBytes();
        _signature = signature;
    }
}
