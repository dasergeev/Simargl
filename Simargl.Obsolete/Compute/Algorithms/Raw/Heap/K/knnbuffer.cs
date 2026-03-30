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
Buffer object which is used to perform  various  requests  (usually  model
inference) in the multithreaded mode (multiple threads working  with  same
KNN object).

This object should be created with KNNCreateBuffer().
*************************************************************************/
public class knnbuffer : alglibobject
{
    //
    // Public declarations
    //

    public knnbuffer()
    {
        _innerobj = new knn.knnbuffer();
    }

    public override alglibobject make_copy()
    {
        return new knnbuffer((knn.knnbuffer)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private knn.knnbuffer _innerobj;
    public knn.knnbuffer innerobj { get { return _innerobj; } }
    public knnbuffer(knn.knnbuffer obj)
    {
        _innerobj = obj;
    }
}
