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
This object stores state of the SSA model.

You should use ALGLIB functions to work with this object.
*************************************************************************/
public class ssamodel : alglibobject
{
    //
    // Public declarations
    //

    public ssamodel()
    {
        _innerobj = new ssa.ssamodel();
    }

    public override alglibobject make_copy()
    {
        return new ssamodel((ssa.ssamodel)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private ssa.ssamodel _innerobj;
    public ssa.ssamodel innerobj { get { return _innerobj; } }
    public ssamodel(ssa.ssamodel obj)
    {
        _innerobj = obj;
    }
}

