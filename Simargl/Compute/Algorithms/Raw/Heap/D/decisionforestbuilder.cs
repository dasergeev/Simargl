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
A random forest (decision forest) builder object.

Used to store dataset and specify decision forest training algorithm settings.
*************************************************************************/
public class decisionforestbuilder : alglibobject
{
    //
    // Public declarations
    //

    public decisionforestbuilder()
    {
        _innerobj = new dforest.decisionforestbuilder();
    }

    public override alglibobject make_copy()
    {
        return new decisionforestbuilder((dforest.decisionforestbuilder)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private dforest.decisionforestbuilder _innerobj;
    public dforest.decisionforestbuilder innerobj { get { return _innerobj; } }
    public decisionforestbuilder(dforest.decisionforestbuilder obj)
    {
        _innerobj = obj;
    }
}
