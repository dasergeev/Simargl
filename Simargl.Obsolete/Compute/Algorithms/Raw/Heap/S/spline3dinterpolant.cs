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
3-dimensional spline inteprolant
*************************************************************************/
public class spline3dinterpolant : alglibobject
{
    //
    // Public declarations
    //

    public spline3dinterpolant()
    {
        _innerobj = new spline3d.spline3dinterpolant();
    }

    public override alglibobject make_copy()
    {
        return new spline3dinterpolant((spline3d.spline3dinterpolant)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private spline3d.spline3dinterpolant _innerobj;
    public spline3d.spline3dinterpolant innerobj { get { return _innerobj; } }
    public spline3dinterpolant(spline3d.spline3dinterpolant obj)
    {
        _innerobj = obj;
    }
}

