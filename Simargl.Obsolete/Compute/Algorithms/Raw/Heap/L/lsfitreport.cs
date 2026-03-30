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
Least squares fitting report. This structure contains informational fields
which are set by fitting functions provided by this unit.

Different functions initialize different sets of  fields,  so  you  should
read documentation on specific function you used in order  to  know  which
fields are initialized.

    TerminationType filled by all solvers:
                    * positive values, usually 1, denote success
                    * negative values denote various failure scenarios

    TaskRCond       reciprocal of task's condition number
    IterationsCount number of internal iterations

    VarIdx          if user-supplied gradient contains errors  which  were
                    detected by nonlinear fitter, this  field  is  set  to
                    index  of  the  first  component  of gradient which is
                    suspected to be spoiled by bugs.

    RMSError        RMS error
    AvgError        average error
    AvgRelError     average relative error (for non-zero Y[I])
    MaxError        maximum error

    WRMSError       weighted RMS error

    CovPar          covariance matrix for parameters, filled by some solvers
    ErrPar          vector of errors in parameters, filled by some solvers
    ErrCurve        vector of fit errors -  variability  of  the  best-fit
                    curve, filled by some solvers.
    Noise           vector of per-point noise estimates, filled by
                    some solvers.
    R2              coefficient of determination (non-weighted, non-adjusted),
                    filled by some solvers.
*************************************************************************/
public class lsfitreport : alglibobject
{
    //
    // Public declarations
    //
    public int terminationtype { get { return _innerobj.terminationtype; } set { _innerobj.terminationtype = value; } }
    public double taskrcond { get { return _innerobj.taskrcond; } set { _innerobj.taskrcond = value; } }
    public int iterationscount { get { return _innerobj.iterationscount; } set { _innerobj.iterationscount = value; } }
    public int varidx { get { return _innerobj.varidx; } set { _innerobj.varidx = value; } }
    public double rmserror { get { return _innerobj.rmserror; } set { _innerobj.rmserror = value; } }
    public double avgerror { get { return _innerobj.avgerror; } set { _innerobj.avgerror = value; } }
    public double avgrelerror { get { return _innerobj.avgrelerror; } set { _innerobj.avgrelerror = value; } }
    public double maxerror { get { return _innerobj.maxerror; } set { _innerobj.maxerror = value; } }
    public double wrmserror { get { return _innerobj.wrmserror; } set { _innerobj.wrmserror = value; } }
    public double[,] covpar { get { return _innerobj.covpar; } set { _innerobj.covpar = value; } }
    public double[] errpar { get { return _innerobj.errpar; } set { _innerobj.errpar = value; } }
    public double[] errcurve { get { return _innerobj.errcurve; } set { _innerobj.errcurve = value; } }
    public double[] noise { get { return _innerobj.noise; } set { _innerobj.noise = value; } }
    public double r2 { get { return _innerobj.r2; } set { _innerobj.r2 = value; } }

    public lsfitreport()
    {
        _innerobj = new lsfit.lsfitreport();
    }

    public override alglibobject make_copy()
    {
        return new lsfitreport((lsfit.lsfitreport)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private lsfit.lsfitreport _innerobj;
    public lsfit.lsfitreport innerobj { get { return _innerobj; } }
    public lsfitreport(lsfit.lsfitreport obj)
    {
        _innerobj = obj;
    }
}

