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
Builder object used to generate IDW (Inverse Distance Weighting) model.
*************************************************************************/
public class idwbuilder : alglibobject
{
    //
    // Public declarations
    //

    public idwbuilder()
    {
        _innerobj = new idw.idwbuilder();
    }

    public override alglibobject make_copy()
    {
        return new idwbuilder((idw.idwbuilder)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private idw.idwbuilder _innerobj;
    public idw.idwbuilder innerobj { get { return _innerobj; } }
    public idwbuilder(idw.idwbuilder obj)
    {
        _innerobj = obj;
    }
}
