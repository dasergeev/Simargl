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
Polynomial fitting report:
    TerminationType completion code: >0 for success, <0 for failure
    TaskRCond       reciprocal of task's condition number
    RMSError        RMS error
    AvgError        average error
    AvgRelError     average relative error (for non-zero Y[I])
    MaxError        maximum error
*************************************************************************/
public class polynomialfitreport : alglibobject
{
    //
    // Public declarations
    //
    public int terminationtype { get { return _innerobj.terminationtype; } set { _innerobj.terminationtype = value; } }
    public double taskrcond { get { return _innerobj.taskrcond; } set { _innerobj.taskrcond = value; } }
    public double rmserror { get { return _innerobj.rmserror; } set { _innerobj.rmserror = value; } }
    public double avgerror { get { return _innerobj.avgerror; } set { _innerobj.avgerror = value; } }
    public double avgrelerror { get { return _innerobj.avgrelerror; } set { _innerobj.avgrelerror = value; } }
    public double maxerror { get { return _innerobj.maxerror; } set { _innerobj.maxerror = value; } }

    public polynomialfitreport()
    {
        _innerobj = new lsfit.polynomialfitreport();
    }

    public override alglibobject make_copy()
    {
        return new polynomialfitreport((lsfit.polynomialfitreport)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private lsfit.polynomialfitreport _innerobj;
    public lsfit.polynomialfitreport innerobj { get { return _innerobj; } }
    public polynomialfitreport(lsfit.polynomialfitreport obj)
    {
        _innerobj = obj;
    }
}
