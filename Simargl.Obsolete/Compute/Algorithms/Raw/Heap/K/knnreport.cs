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
KNN training report.

Following fields store training set errors:
* relclserror       -   fraction of misclassified cases, [0,1]
* avgce             -   average cross-entropy in bits per symbol
* rmserror          -   root-mean-square error
* avgerror          -   average error
* avgrelerror       -   average relative error

For classification problems:
* RMS, AVG and AVGREL errors are calculated for posterior probabilities

For regression problems:
* RELCLS and AVGCE errors are zero
*************************************************************************/
public class knnreport : alglibobject
{
    //
    // Public declarations
    //
    public double relclserror { get { return _innerobj.relclserror; } set { _innerobj.relclserror = value; } }
    public double avgce { get { return _innerobj.avgce; } set { _innerobj.avgce = value; } }
    public double rmserror { get { return _innerobj.rmserror; } set { _innerobj.rmserror = value; } }
    public double avgerror { get { return _innerobj.avgerror; } set { _innerobj.avgerror = value; } }
    public double avgrelerror { get { return _innerobj.avgrelerror; } set { _innerobj.avgrelerror = value; } }

    public knnreport()
    {
        _innerobj = new knn.knnreport();
    }

    public override alglibobject make_copy()
    {
        return new knnreport((knn.knnreport)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private knn.knnreport _innerobj;
    public knn.knnreport innerobj { get { return _innerobj; } }
    public knnreport(knn.knnreport obj)
    {
        _innerobj = obj;
    }
}

