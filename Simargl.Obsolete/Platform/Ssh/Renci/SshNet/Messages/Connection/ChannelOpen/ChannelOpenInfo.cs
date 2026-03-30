using Simargl.Zero.Ssh.Renci.SshNet.Common;

namespace Simargl.Zero.Ssh.Renci.SshNet.Messages.Connection;

/// <summary>
/// Base class for open channel messages.
/// </summary>
internal abstract class ChannelOpenInfo : SshData
{
    /// <summary>
    /// Gets the type of the channel to open.
    /// </summary>
    /// <value>
    /// The type of the channel to open.
    /// </value>
    public abstract string ChannelType { get; }

    /// <summary>
    /// Called when type specific data need to be loaded.
    /// </summary>
    protected override void LoadData()
    {
    }

    /// <summary>
    /// Called when type specific data need to be saved.
    /// </summary>
    protected override void SaveData()
    {
    }
}
