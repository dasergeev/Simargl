#pragma warning disable CS1591

#if ALGLIB_USE_SIMD
#define _ALGLIB_ALREADY_DEFINED_SIMD_ALIASES
using Sse2 = System.Runtime.Intrinsics.X86.Sse2;
using Avx2 = System.Runtime.Intrinsics.X86.Avx2;
using Fma  = System.Runtime.Intrinsics.X86.Fma;
using Intrinsics = System.Runtime.Intrinsics;
#endif

#if ALGLIB_USE_SIMD && !_ALGLIB_ALREADY_DEFINED_SIMD_ALIASES
#define _ALGLIB_ALREADY_DEFINED_SIMD_ALIASES
using Sse2 = System.Runtime.Intrinsics.X86.Sse2;
using Avx2 = System.Runtime.Intrinsics.X86.Avx2;
using Fma  = System.Runtime.Intrinsics.X86.Fma;
using Intrinsics = System.Runtime.Intrinsics;
#endif

namespace Simargl.Algorithms.Raw;



/*************************************************************************
IDW fitting report:
    rmserror        RMS error
    avgerror        average error
    maxerror        maximum error
    r2              coefficient of determination,  R-squared, 1-RSS/TSS
*************************************************************************/
public class idwreport : alglibobject
{
    //
    // Public declarations
    //
    public double rmserror { get { return _innerobj.rmserror; } set { _innerobj.rmserror = value; } }
    public double avgerror { get { return _innerobj.avgerror; } set { _innerobj.avgerror = value; } }
    public double maxerror { get { return _innerobj.maxerror; } set { _innerobj.maxerror = value; } }
    public double r2 { get { return _innerobj.r2; } set { _innerobj.r2 = value; } }

    public idwreport()
    {
        _innerobj = new idw.idwreport();
    }

    public override alglibobject make_copy()
    {
        return new idwreport((idw.idwreport)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private idw.idwreport _innerobj;
    public idw.idwreport innerobj { get { return _innerobj; } }
    public idwreport(idw.idwreport obj)
    {
        _innerobj = obj;
    }
}
