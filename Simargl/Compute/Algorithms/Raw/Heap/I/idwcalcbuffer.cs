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
Buffer  object  which  is  used  to  perform  evaluation  requests  in  the
multithreaded mode (multiple threads working with same IDW object).

This object should be created with idwcreatecalcbuffer().
*************************************************************************/
public class idwcalcbuffer : alglibobject
{
    //
    // Public declarations
    //

    public idwcalcbuffer()
    {
        _innerobj = new idw.idwcalcbuffer();
    }

    public override alglibobject make_copy()
    {
        return new idwcalcbuffer((idw.idwcalcbuffer)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private idw.idwcalcbuffer _innerobj;
    public idw.idwcalcbuffer innerobj { get { return _innerobj; } }
    public idwcalcbuffer(idw.idwcalcbuffer obj)
    {
        _innerobj = obj;
    }
}
