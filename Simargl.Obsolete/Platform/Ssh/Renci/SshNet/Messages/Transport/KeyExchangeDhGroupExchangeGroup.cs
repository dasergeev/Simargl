
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
using Simargl.Zero.Ssh.Renci.SshNet.Common;

namespace Simargl.Zero.Ssh.Renci.SshNet.Messages.Transport;

/// <summary>
/// Represents SSH_MSG_KEX_DH_GEX_GROUP message.
/// </summary>
internal class KeyExchangeDhGroupExchangeGroup : Message
{
    private byte[] _safePrime;
    private byte[] _subGroup;

    /// <inheritdoc />
    public override string MessageName
    {
        get
        {
            return "SSH_MSG_KEX_DH_GEX_GROUP";
        }
    }

    /// <inheritdoc />
    public override byte MessageNumber
    {
        get
        {
            return 31;
        }
    }

    /// <summary>
    /// Gets the safe prime.
    /// </summary>
    /// <value>
    /// The safe prime.
    /// </value>
    public BigInteger SafePrime
    {
        get { return _safePrime.ToBigInteger(); }
    }

    /// <summary>
    /// Gets the generator for subgroup in GF(p).
    /// </summary>
    /// <value>
    /// The sub group.
    /// </value>
    public BigInteger SubGroup
    {
        get { return _subGroup.ToBigInteger(); }
    }

    /// <summary>
    /// Gets the size of the message in bytes.
    /// </summary>
    /// <value>
    /// The size of the messages in bytes.
    /// </value>
    protected override int BufferCapacity
    {
        get
        {
            var capacity = base.BufferCapacity;
            capacity += 4; // SafePrime length
            capacity += _safePrime.Length; // SafePrime
            capacity += 4; // SubGroup length
            capacity += _subGroup.Length; // SubGroup

            return capacity;
        }
    }

    /// <summary>
    /// Called when type specific data need to be loaded.
    /// </summary>
    protected override void LoadData()
    {
        _safePrime = ReadBinary();
        _subGroup = ReadBinary();
    }

    /// <summary>
    /// Called when type specific data need to be saved.
    /// </summary>
    protected override void SaveData()
    {
        WriteBinaryString(_safePrime);
        WriteBinaryString(_subGroup);
    }

    internal override void Process(Session session)
    {
        session.OnKeyExchangeDhGroupExchangeGroupReceived(this);
    }
}
