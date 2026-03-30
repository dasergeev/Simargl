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
This object stores state of the subspace iteration algorithm.

You should use ALGLIB functions to work with this object.
*************************************************************************/
public class eigsubspacestate : alglibobject
{
    //
    // Public declarations
    //

    public eigsubspacestate()
    {
        _innerobj = new evd.eigsubspacestate();
    }

    public override alglibobject make_copy()
    {
        return new eigsubspacestate((evd.eigsubspacestate)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private evd.eigsubspacestate _innerobj;
    public evd.eigsubspacestate innerobj { get { return _innerobj; } }
    public eigsubspacestate(evd.eigsubspacestate obj)
    {
        _innerobj = obj;
    }
}
