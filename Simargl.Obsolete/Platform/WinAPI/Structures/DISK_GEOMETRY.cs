using System;
using System.Runtime.InteropServices;

namespace Simargl.Platform.WinAPI;

/// <summary>
/// 
/// </summary>
[CLSCompliant(false)]
[StructLayout(LayoutKind.Sequential, Pack = 8)]
public struct DISK_GEOMETRY
{
    /// <summary>
    /// 
    /// </summary>
    public long Cylinders;

    /// <summary>
    /// 
    /// </summary>
    public MEDIA_TYPE MediaType;

    /// <summary>
    /// 
    /// </summary>
    public uint TracksPerCylinder;

    /// <summary>
    /// 
    /// </summary>
    public uint SectorsPerTrack;

    /// <summary>
    /// 
    /// </summary>
    public uint BytesPerSector;
};
