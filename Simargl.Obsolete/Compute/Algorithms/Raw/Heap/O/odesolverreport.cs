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

*************************************************************************/
public class odesolverreport : alglibobject
{
    //
    // Public declarations
    //
    public int nfev { get { return _innerobj.nfev; } set { _innerobj.nfev = value; } }
    public int terminationtype { get { return _innerobj.terminationtype; } set { _innerobj.terminationtype = value; } }

    public odesolverreport()
    {
        _innerobj = new odesolver.odesolverreport();
    }

    public override alglibobject make_copy()
    {
        return new odesolverreport((odesolver.odesolverreport)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private odesolver.odesolverreport _innerobj;
    public odesolver.odesolverreport innerobj { get { return _innerobj; } }
    public odesolverreport(odesolver.odesolverreport obj)
    {
        _innerobj = obj;
    }
}


