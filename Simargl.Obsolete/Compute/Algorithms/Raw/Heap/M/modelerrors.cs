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
Model's errors:
    * RelCLSError   -   fraction of misclassified cases.
    * AvgCE         -   acerage cross-entropy
    * RMSError      -   root-mean-square error
    * AvgError      -   average error
    * AvgRelError   -   average relative error

NOTE 1: RelCLSError/AvgCE are zero on regression problems.

NOTE 2: on classification problems  RMSError/AvgError/AvgRelError  contain
        errors in prediction of posterior probabilities
*************************************************************************/
public class modelerrors : alglibobject
{
    //
    // Public declarations
    //
    public double relclserror { get { return _innerobj.relclserror; } set { _innerobj.relclserror = value; } }
    public double avgce { get { return _innerobj.avgce; } set { _innerobj.avgce = value; } }
    public double rmserror { get { return _innerobj.rmserror; } set { _innerobj.rmserror = value; } }
    public double avgerror { get { return _innerobj.avgerror; } set { _innerobj.avgerror = value; } }
    public double avgrelerror { get { return _innerobj.avgrelerror; } set { _innerobj.avgrelerror = value; } }

    public modelerrors()
    {
        _innerobj = new mlpbase.modelerrors();
    }

    public override alglibobject make_copy()
    {
        return new modelerrors((mlpbase.modelerrors)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private mlpbase.modelerrors _innerobj;
    public mlpbase.modelerrors innerobj { get { return _innerobj; } }
    public modelerrors(mlpbase.modelerrors obj)
    {
        _innerobj = obj;
    }
}
