using System;

namespace Hbm.Devices.Scan.Announcing;

/// <summary>
/// 
/// </summary>
public class RemoveDeviceEventArgs :
    EventArgs
{
    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    public Announce? Announce { get; internal set; }
}
