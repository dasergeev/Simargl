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
RBF model.

Never try to directly work with fields of this object - always use  ALGLIB
functions to use this object.
*************************************************************************/
public class rbfmodel : alglibobject
{
    //
    // Public declarations
    //

    public rbfmodel()
    {
        _innerobj = new rbf.rbfmodel();
    }

    public override alglibobject make_copy()
    {
        return new rbfmodel((rbf.rbfmodel)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private rbf.rbfmodel _innerobj;
    public rbf.rbfmodel innerobj { get { return _innerobj; } }
    public rbfmodel(rbf.rbfmodel obj)
    {
        _innerobj = obj;
    }
}
