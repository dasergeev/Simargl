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
Matrix inverse report:
* terminationtype   completion code:
                    *  1 for success
                    * -3 for failure due to the matrix being singular or
                         nearly-singular
* r1                reciprocal of condition number in 1-norm
* rinf              reciprocal of condition number in inf-norm
*************************************************************************/
public class matinvreport : alglibobject
{
    //
    // Public declarations
    //
    public int terminationtype { get { return _innerobj.terminationtype; } set { _innerobj.terminationtype = value; } }
    public double r1 { get { return _innerobj.r1; } set { _innerobj.r1 = value; } }
    public double rinf { get { return _innerobj.rinf; } set { _innerobj.rinf = value; } }

    public matinvreport()
    {
        _innerobj = new matinv.matinvreport();
    }

    public override alglibobject make_copy()
    {
        return new matinvreport((matinv.matinvreport)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private matinv.matinvreport _innerobj;
    public matinv.matinvreport innerobj { get { return _innerobj; } }
    public matinvreport(matinv.matinvreport obj)
    {
        _innerobj = obj;
    }
}

