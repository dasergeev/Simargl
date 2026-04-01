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
This object stores state of the subspace iteration algorithm.

You should use ALGLIB functions to work with this object.
*************************************************************************/
public class eigsubspacereport : alglibobject
{
    //
    // Public declarations
    //
    public int iterationscount { get { return _innerobj.iterationscount; } set { _innerobj.iterationscount = value; } }

    public eigsubspacereport()
    {
        _innerobj = new evd.eigsubspacereport();
    }

    public override alglibobject make_copy()
    {
        return new eigsubspacereport((evd.eigsubspacereport)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private evd.eigsubspacereport _innerobj;
    public evd.eigsubspacereport innerobj { get { return _innerobj; } }
    public eigsubspacereport(evd.eigsubspacereport obj)
    {
        _innerobj = obj;
    }
}

