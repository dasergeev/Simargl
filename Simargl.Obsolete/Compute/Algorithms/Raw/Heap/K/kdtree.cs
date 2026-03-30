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
KD-tree object.
*************************************************************************/
public class kdtree : alglibobject
{
    //
    // Public declarations
    //

    public kdtree()
    {
        _innerobj = new nearestneighbor.kdtree();
    }

    public override alglibobject make_copy()
    {
        return new kdtree((nearestneighbor.kdtree)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private nearestneighbor.kdtree _innerobj;
    public nearestneighbor.kdtree innerobj { get { return _innerobj; } }
    public kdtree(nearestneighbor.kdtree obj)
    {
        _innerobj = obj;
    }
}

