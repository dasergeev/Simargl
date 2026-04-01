using System;

namespace Hbm.Devices.Scan.Announcing;

/// <summary>
/// 
/// </summary>
public class UpdateDeviceEventArgs :
    EventArgs
{
    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    public Announce? NewAnnounce { get; internal set; }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    public Announce? OldAnnounce { get; internal set; }
}
