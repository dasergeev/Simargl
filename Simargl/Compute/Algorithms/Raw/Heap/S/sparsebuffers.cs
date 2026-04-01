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
Temporary buffers for sparse matrix operations.

You should pass an instance of this structure to factorization  functions.
It allows to reuse memory during repeated sparse  factorizations.  You  do
not have to call some initialization function - simply passing an instance
to factorization function is enough.
*************************************************************************/
public class sparsebuffers : alglibobject
{
    //
    // Public declarations
    //

    public sparsebuffers()
    {
        _innerobj = new sparse.sparsebuffers();
    }

    public override alglibobject make_copy()
    {
        return new sparsebuffers((sparse.sparsebuffers)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private sparse.sparsebuffers _innerobj;
    public sparse.sparsebuffers innerobj { get { return _innerobj; } }
    public sparsebuffers(sparse.sparsebuffers obj)
    {
        _innerobj = obj;
    }
}
