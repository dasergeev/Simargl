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
Decision forest (random forest) model.
*************************************************************************/
public class decisionforest : alglibobject
{
    //
    // Public declarations
    //

    public decisionforest()
    {
        _innerobj = new dforest.decisionforest();
    }

    public override alglibobject make_copy()
    {
        return new decisionforest((dforest.decisionforest)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private dforest.decisionforest _innerobj;
    public dforest.decisionforest innerobj { get { return _innerobj; } }
    public decisionforest(dforest.decisionforest obj)
    {
        _innerobj = obj;
    }
}
