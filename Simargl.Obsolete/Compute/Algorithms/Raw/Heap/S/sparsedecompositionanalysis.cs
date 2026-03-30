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
An analysis of the sparse matrix decomposition, performed prior to  actual
numerical factorization. You should not directly  access  fields  of  this
object - use appropriate ALGLIB functions to work with this object.
*************************************************************************/
public class sparsedecompositionanalysis : alglibobject
{
    //
    // Public declarations
    //

    public sparsedecompositionanalysis()
    {
        _innerobj = new trfac.sparsedecompositionanalysis();
    }

    public override alglibobject make_copy()
    {
        return new sparsedecompositionanalysis((trfac.sparsedecompositionanalysis)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private trfac.sparsedecompositionanalysis _innerobj;
    public trfac.sparsedecompositionanalysis innerobj { get { return _innerobj; } }
    public sparsedecompositionanalysis(trfac.sparsedecompositionanalysis obj)
    {
        _innerobj = obj;
    }
}


