using System;

namespace Hbm.Devices.Scan.Configure;

/// <summary>
/// 
/// </summary>
public class JsonRpcResponseEventArgs :
    EventArgs
{
    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    public JsonRpcResponse? Response { get; internal set; }
}
