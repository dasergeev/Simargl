using System;
using System.Runtime.InteropServices;

namespace Simargl.Platform.WinAPI;

/// <summary>
/// 
/// </summary>
[CLSCompliant(false)]
[StructLayout(LayoutKind.Sequential, Pack = 8)]
public struct DISK_GEOMETRY_EX
{
    /// <summary>
    /// 
    /// </summary>
    public DISK_GEOMETRY Geometry;

    /// <summary>
    /// 
    /// </summary>
    public long DiskSize;

    /// <summary>
    /// 
    /// </summary>
    public byte Data;
}
