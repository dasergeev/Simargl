namespace Simargl.Platform.WinAPI;

/// <summary>
/// 
/// </summary>
public enum MEDIA_TYPE
{
    /// <summary>
    /// 
    /// </summary>
    Unknown,                // Format is unknown

    /// <summary>
    /// 
    /// </summary>
    F5_1Pt2_512,            // 5.25", 1.2MB,  512 bytes/sector

    /// <summary>
    /// 
    /// </summary>
    F3_1Pt44_512,           // 3.5",  1.44MB, 512 bytes/sector

    /// <summary>
    /// 
    /// </summary>
    F3_2Pt88_512,           // 3.5",  2.88MB, 512 bytes/sector

    /// <summary>
    /// 
    /// </summary>
    F3_20Pt8_512,           // 3.5",  20.8MB, 512 bytes/sector

    /// <summary>
    /// 
    /// </summary>
    F3_720_512,             // 3.5",  720KB,  512 bytes/sector

    /// <summary>
    /// 
    /// </summary>
    F5_360_512,             // 5.25", 360KB,  512 bytes/sector

    /// <summary>
    /// 
    /// </summary>
    F5_320_512,             // 5.25", 320KB,  512 bytes/sector

    /// <summary>
    /// 
    /// </summary>
    F5_320_1024,            // 5.25", 320KB,  1024 bytes/sector

    /// <summary>
    /// 
    /// </summary>
    F5_180_512,             // 5.25", 180KB,  512 bytes/sector

    /// <summary>
    /// 
    /// </summary>
    F5_160_512,             // 5.25", 160KB,  512 bytes/sector

    /// <summary>
    /// 
    /// </summary>
    RemovableMedia,         // Removable media other than floppy

    /// <summary>
    /// 
    /// </summary>
    FixedMedia,             // Fixed hard disk media

    /// <summary>
    /// 
    /// </summary>
    F3_120M_512,            // 3.5", 120M Floppy

    /// <summary>
    /// 
    /// </summary>
    F3_640_512,             // 3.5" ,  640KB,  512 bytes/sector

    /// <summary>
    /// 
    /// </summary>
    F5_640_512,             // 5.25",  640KB,  512 bytes/sector

    /// <summary>
    /// 
    /// </summary>
    F5_720_512,             // 5.25",  720KB,  512 bytes/sector

    /// <summary>
    /// 
    /// </summary>
    F3_1Pt2_512,            // 3.5" ,  1.2Mb,  512 bytes/sector

    /// <summary>
    /// 
    /// </summary>
    F3_1Pt23_1024,          // 3.5" ,  1.23Mb, 1024 bytes/sector

    /// <summary>
    /// 
    /// </summary>
    F5_1Pt23_1024,          // 5.25",  1.23MB, 1024 bytes/sector

    /// <summary>
    /// 
    /// </summary>
    F3_128Mb_512,           // 3.5" MO 128Mb   512 bytes/sector

    /// <summary>
    /// 
    /// </summary>
    F3_230Mb_512,           // 3.5" MO 230Mb   512 bytes/sector

    /// <summary>
    /// 
    /// </summary>
    F8_256_128,             // 8",     256KB,  128 bytes/sector

    /// <summary>
    /// 
    /// </summary>
    F3_200Mb_512,           // 3.5",   200M Floppy (HiFD)

    /// <summary>
    /// 
    /// </summary>
    F3_240M_512,            // 3.5",   240Mb Floppy (HiFD)

    /// <summary>
    /// 
    /// </summary>
    F3_32M_512              // 3.5",   32Mb Floppy
};
