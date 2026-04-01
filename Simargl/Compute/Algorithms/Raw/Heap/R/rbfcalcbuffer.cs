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
Buffer object which is used  to  perform  RBF  model  calculation  in  the
multithreaded mode (multiple threads working with same RBF object).

This object should be created with RBFCreateCalcBuffer().
*************************************************************************/
public class rbfcalcbuffer : alglibobject
{
    //
    // Public declarations
    //

    public rbfcalcbuffer()
    {
        _innerobj = new rbf.rbfcalcbuffer();
    }

    public override alglibobject make_copy()
    {
        return new rbfcalcbuffer((rbf.rbfcalcbuffer)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private rbf.rbfcalcbuffer _innerobj;
    public rbf.rbfcalcbuffer innerobj { get { return _innerobj; } }
    public rbfcalcbuffer(rbf.rbfcalcbuffer obj)
    {
        _innerobj = obj;
    }
}

