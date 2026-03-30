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
Training report:
    * RelCLSError   -   fraction of misclassified cases.
    * AvgCE         -   acerage cross-entropy
    * RMSError      -   root-mean-square error
    * AvgError      -   average error
    * AvgRelError   -   average relative error
    * NGrad         -   number of gradient calculations
    * NHess         -   number of Hessian calculations
    * NCholesky     -   number of Cholesky decompositions

NOTE 1: RelCLSError/AvgCE are zero on regression problems.

NOTE 2: on classification problems  RMSError/AvgError/AvgRelError  contain
        errors in prediction of posterior probabilities
*************************************************************************/
public class mlpreport : alglibobject
{
    //
    // Public declarations
    //
    public double relclserror { get { return _innerobj.relclserror; } set { _innerobj.relclserror = value; } }
    public double avgce { get { return _innerobj.avgce; } set { _innerobj.avgce = value; } }
    public double rmserror { get { return _innerobj.rmserror; } set { _innerobj.rmserror = value; } }
    public double avgerror { get { return _innerobj.avgerror; } set { _innerobj.avgerror = value; } }
    public double avgrelerror { get { return _innerobj.avgrelerror; } set { _innerobj.avgrelerror = value; } }
    public int ngrad { get { return _innerobj.ngrad; } set { _innerobj.ngrad = value; } }
    public int nhess { get { return _innerobj.nhess; } set { _innerobj.nhess = value; } }
    public int ncholesky { get { return _innerobj.ncholesky; } set { _innerobj.ncholesky = value; } }

    public mlpreport()
    {
        _innerobj = new mlptrain.mlpreport();
    }

    public override alglibobject make_copy()
    {
        return new mlpreport((mlptrain.mlpreport)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private mlptrain.mlpreport _innerobj;
    public mlptrain.mlpreport innerobj { get { return _innerobj; } }
    public mlpreport(mlptrain.mlpreport obj)
    {
        _innerobj = obj;
    }
}
