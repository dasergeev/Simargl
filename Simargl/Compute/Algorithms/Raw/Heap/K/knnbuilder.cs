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
A KNN builder object; this object encapsulates  dataset  and  all  related
settings, it is used to create an actual instance of KNN model.
*************************************************************************/
public class knnbuilder : alglibobject
{
    //
    // Public declarations
    //

    public knnbuilder()
    {
        _innerobj = new knn.knnbuilder();
    }

    public override alglibobject make_copy()
    {
        return new knnbuilder((knn.knnbuilder)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private knn.knnbuilder _innerobj;
    public knn.knnbuilder innerobj { get { return _innerobj; } }
    public knnbuilder(knn.knnbuilder obj)
    {
        _innerobj = obj;
    }
}
