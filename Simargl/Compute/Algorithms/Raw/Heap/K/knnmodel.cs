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
KNN model, can be used for classification or regression
*************************************************************************/
public class knnmodel : alglibobject
{
    //
    // Public declarations
    //

    public knnmodel()
    {
        _innerobj = new knn.knnmodel();
    }

    public override alglibobject make_copy()
    {
        return new knnmodel((knn.knnmodel)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private knn.knnmodel _innerobj;
    public knn.knnmodel innerobj { get { return _innerobj; } }
    public knnmodel(knn.knnmodel obj)
    {
        _innerobj = obj;
    }
}

