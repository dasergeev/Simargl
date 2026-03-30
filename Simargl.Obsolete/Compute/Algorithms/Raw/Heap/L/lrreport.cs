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
LRReport structure contains additional information about linear model:
* C             -   covariation matrix,  array[0..NVars,0..NVars].
                    C[i,j] = Cov(A[i],A[j])
* RMSError      -   root mean square error on a training set
* AvgError      -   average error on a training set
* AvgRelError   -   average relative error on a training set (excluding
                    observations with zero function value).
* CVRMSError    -   leave-one-out cross-validation estimate of
                    generalization error. Calculated using fast algorithm
                    with O(NVars*NPoints) complexity.
* CVAvgError    -   cross-validation estimate of average error
* CVAvgRelError -   cross-validation estimate of average relative error

All other fields of the structure are intended for internal use and should
not be used outside ALGLIB.
*************************************************************************/
public class lrreport : alglibobject
{
    //
    // Public declarations
    //
    public double[,] c { get { return _innerobj.c; } set { _innerobj.c = value; } }
    public double rmserror { get { return _innerobj.rmserror; } set { _innerobj.rmserror = value; } }
    public double avgerror { get { return _innerobj.avgerror; } set { _innerobj.avgerror = value; } }
    public double avgrelerror { get { return _innerobj.avgrelerror; } set { _innerobj.avgrelerror = value; } }
    public double cvrmserror { get { return _innerobj.cvrmserror; } set { _innerobj.cvrmserror = value; } }
    public double cvavgerror { get { return _innerobj.cvavgerror; } set { _innerobj.cvavgerror = value; } }
    public double cvavgrelerror { get { return _innerobj.cvavgrelerror; } set { _innerobj.cvavgrelerror = value; } }
    public int ncvdefects { get { return _innerobj.ncvdefects; } set { _innerobj.ncvdefects = value; } }
    public int[] cvdefects { get { return _innerobj.cvdefects; } set { _innerobj.cvdefects = value; } }

    public lrreport()
    {
        _innerobj = new linreg.lrreport();
    }

    public override alglibobject make_copy()
    {
        return new lrreport((linreg.lrreport)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private linreg.lrreport _innerobj;
    public linreg.lrreport innerobj { get { return _innerobj; } }
    public lrreport(linreg.lrreport obj)
    {
        _innerobj = obj;
    }
}

