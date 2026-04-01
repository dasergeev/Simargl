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
Spline 2D fitting report:
    rmserror        RMS error
    avgerror        average error
    maxerror        maximum error
    r2              coefficient of determination,  R-squared, 1-RSS/TSS
*************************************************************************/
public class spline2dfitreport : alglibobject
{
    //
    // Public declarations
    //
    public double rmserror { get { return _innerobj.rmserror; } set { _innerobj.rmserror = value; } }
    public double avgerror { get { return _innerobj.avgerror; } set { _innerobj.avgerror = value; } }
    public double maxerror { get { return _innerobj.maxerror; } set { _innerobj.maxerror = value; } }
    public double r2 { get { return _innerobj.r2; } set { _innerobj.r2 = value; } }

    public spline2dfitreport()
    {
        _innerobj = new spline2d.spline2dfitreport();
    }

    public override alglibobject make_copy()
    {
        return new spline2dfitreport((spline2d.spline2dfitreport)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private spline2d.spline2dfitreport _innerobj;
    public spline2d.spline2dfitreport innerobj { get { return _innerobj; } }
    public spline2dfitreport(spline2d.spline2dfitreport obj)
    {
        _innerobj = obj;
    }
}
