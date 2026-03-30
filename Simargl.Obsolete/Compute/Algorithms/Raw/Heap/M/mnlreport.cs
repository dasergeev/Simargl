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
MNLReport structure contains information about training process:
* NGrad     -   number of gradient calculations
* NHess     -   number of Hessian calculations
*************************************************************************/
public class mnlreport : alglibobject
{
    //
    // Public declarations
    //
    public int ngrad { get { return _innerobj.ngrad; } set { _innerobj.ngrad = value; } }
    public int nhess { get { return _innerobj.nhess; } set { _innerobj.nhess = value; } }

    public mnlreport()
    {
        _innerobj = new logit.mnlreport();
    }

    public override alglibobject make_copy()
    {
        return new mnlreport((logit.mnlreport)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private logit.mnlreport _innerobj;
    public logit.mnlreport innerobj { get { return _innerobj; } }
    public mnlreport(logit.mnlreport obj)
    {
        _innerobj = obj;
    }
}

