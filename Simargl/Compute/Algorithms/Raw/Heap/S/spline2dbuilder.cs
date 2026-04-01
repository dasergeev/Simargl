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
Nonlinear least squares solver used to fit 2D splines to data
*************************************************************************/
public class spline2dbuilder : alglibobject
{
    //
    // Public declarations
    //

    public spline2dbuilder()
    {
        _innerobj = new spline2d.spline2dbuilder();
    }

    public override alglibobject make_copy()
    {
        return new spline2dbuilder((spline2d.spline2dbuilder)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private spline2d.spline2dbuilder _innerobj;
    public spline2d.spline2dbuilder innerobj { get { return _innerobj; } }
    public spline2dbuilder(spline2d.spline2dbuilder obj)
    {
        _innerobj = obj;
    }
}
