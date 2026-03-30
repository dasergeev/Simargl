
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
using System;

namespace Simargl.Zero.Ssh.Renci.SshNet.Common;

/// <summary>
/// Provides data for Shell DataReceived event.
/// </summary>
internal class ShellDataEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ShellDataEventArgs"/> class.
    /// </summary>
    /// <param name="data">The data.</param>
    public ShellDataEventArgs(byte[] data)
    {
        Data = data;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ShellDataEventArgs"/> class.
    /// </summary>
    /// <param name="line">The line.</param>
    public ShellDataEventArgs(string line)
    {
        Line = line;
    }

    /// <summary>
    /// Gets the data.
    /// </summary>
    public byte[] Data { get; }

    /// <summary>
    /// Gets the line data.
    /// </summary>
    public string Line { get; }
}
