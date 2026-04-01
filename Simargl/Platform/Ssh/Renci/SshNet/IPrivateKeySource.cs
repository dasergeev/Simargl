using System.Collections.Generic;

using Simargl.Zero.Ssh.Renci.SshNet.Security;

namespace Simargl.Zero.Ssh.Renci.SshNet;

/// <summary>
/// Represents private key source interface.
/// </summary>
internal interface IPrivateKeySource
{
    /// <summary>
    /// Gets the host keys algorithms.
    /// </summary>
    /// <remarks>
    /// In situations where there is a preferred order of usage of the host algorithms,
    /// the collection should be ordered from most preferred to least.
    /// </remarks>
    IReadOnlyCollection<HostAlgorithm> HostKeyAlgorithms { get; }
}
