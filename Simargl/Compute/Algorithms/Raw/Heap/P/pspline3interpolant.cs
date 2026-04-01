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
Parametric spline inteprolant: 3-dimensional curve.

You should not try to access its members directly - use PSpline3XXXXXXXX()
functions instead.
*************************************************************************/
public class pspline3interpolant : alglibobject
{
    //
    // Public declarations
    //

    public pspline3interpolant()
    {
        _innerobj = new parametric.pspline3interpolant();
    }

    public override alglibobject make_copy()
    {
        return new pspline3interpolant((parametric.pspline3interpolant)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private parametric.pspline3interpolant _innerobj;
    public parametric.pspline3interpolant innerobj { get { return _innerobj; } }
    public pspline3interpolant(parametric.pspline3interpolant obj)
    {
        _innerobj = obj;
    }
}


