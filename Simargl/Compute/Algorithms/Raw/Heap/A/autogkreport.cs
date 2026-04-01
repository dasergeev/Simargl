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
Integration report:
* TerminationType = completetion code:
    * -5    non-convergence of Gauss-Kronrod nodes
            calculation subroutine.
    * -1    incorrect parameters were specified
    *  1    OK
* Rep.NFEV countains number of function calculations
* Rep.NIntervals contains number of intervals [a,b]
  was partitioned into.
*************************************************************************/
public class autogkreport : alglibobject
{
    //
    // Public declarations
    //
    public int terminationtype { get { return _innerobj.terminationtype; } set { _innerobj.terminationtype = value; } }
    public int nfev { get { return _innerobj.nfev; } set { _innerobj.nfev = value; } }
    public int nintervals { get { return _innerobj.nintervals; } set { _innerobj.nintervals = value; } }

    public autogkreport()
    {
        _innerobj = new autogk.autogkreport();
    }

    public override alglibobject make_copy()
    {
        return new autogkreport((autogk.autogkreport)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private autogk.autogkreport _innerobj;
    public autogk.autogkreport innerobj { get { return _innerobj; } }
    public autogkreport(autogk.autogkreport obj)
    {
        _innerobj = obj;
    }
}
