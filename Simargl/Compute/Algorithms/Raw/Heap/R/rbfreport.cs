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
RBF solution report:
* TerminationType   -   termination type, positive values - success,
                        non-positive - failure.

Fields which are set by modern RBF solvers (hierarchical):
* RMSError          -   root-mean-square error; NAN for old solvers (ML, QNN)
* MaxError          -   maximum error; NAN for old solvers (ML, QNN)
*************************************************************************/
public class rbfreport : alglibobject
{
    //
    // Public declarations
    //
    public double rmserror { get { return _innerobj.rmserror; } set { _innerobj.rmserror = value; } }
    public double maxerror { get { return _innerobj.maxerror; } set { _innerobj.maxerror = value; } }
    public int arows { get { return _innerobj.arows; } set { _innerobj.arows = value; } }
    public int acols { get { return _innerobj.acols; } set { _innerobj.acols = value; } }
    public int annz { get { return _innerobj.annz; } set { _innerobj.annz = value; } }
    public int iterationscount { get { return _innerobj.iterationscount; } set { _innerobj.iterationscount = value; } }
    public int nmv { get { return _innerobj.nmv; } set { _innerobj.nmv = value; } }
    public int terminationtype { get { return _innerobj.terminationtype; } set { _innerobj.terminationtype = value; } }

    public rbfreport()
    {
        _innerobj = new rbf.rbfreport();
    }

    public override alglibobject make_copy()
    {
        return new rbfreport((rbf.rbfreport)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private rbf.rbfreport _innerobj;
    public rbf.rbfreport innerobj { get { return _innerobj; } }
    public rbfreport(rbf.rbfreport obj)
    {
        _innerobj = obj;
    }
}
