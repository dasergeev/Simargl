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
This structure is a MCPD training report:
    InnerIterationsCount    -   number of inner iterations of the
                                underlying optimization algorithm
    OuterIterationsCount    -   number of outer iterations of the
                                underlying optimization algorithm
    NFEV                    -   number of merit function evaluations
    TerminationType         -   termination type
                                (same as for MinBLEIC optimizer, positive
                                values denote success, negative ones -
                                failure)

  -- ALGLIB --
     Copyright 23.05.2010 by Bochkanov Sergey
*************************************************************************/
public class mcpdreport : alglibobject
{
    //
    // Public declarations
    //
    public int inneriterationscount { get { return _innerobj.inneriterationscount; } set { _innerobj.inneriterationscount = value; } }
    public int outeriterationscount { get { return _innerobj.outeriterationscount; } set { _innerobj.outeriterationscount = value; } }
    public int nfev { get { return _innerobj.nfev; } set { _innerobj.nfev = value; } }
    public int terminationtype { get { return _innerobj.terminationtype; } set { _innerobj.terminationtype = value; } }

    public mcpdreport()
    {
        _innerobj = new mcpd.mcpdreport();
    }

    public override alglibobject make_copy()
    {
        return new mcpdreport((mcpd.mcpdreport)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private mcpd.mcpdreport _innerobj;
    public mcpd.mcpdreport innerobj { get { return _innerobj; } }
    public mcpdreport(mcpd.mcpdreport obj)
    {
        _innerobj = obj;
    }
}
