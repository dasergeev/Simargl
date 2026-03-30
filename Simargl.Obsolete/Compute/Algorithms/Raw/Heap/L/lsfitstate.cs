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
Nonlinear fitter.

You should use ALGLIB functions to work with fitter.
Never try to access its fields directly!
*************************************************************************/
public class lsfitstate : alglibobject
{
    //
    // Public declarations
    //
    public bool needf { get { return _innerobj.needf; } set { _innerobj.needf = value; } }
    public bool needfg { get { return _innerobj.needfg; } set { _innerobj.needfg = value; } }
    public bool needfgh { get { return _innerobj.needfgh; } set { _innerobj.needfgh = value; } }
    public bool xupdated { get { return _innerobj.xupdated; } set { _innerobj.xupdated = value; } }
    public double[] c { get { return _innerobj.c; } }
    public double f { get { return _innerobj.f; } set { _innerobj.f = value; } }
    public double[] g { get { return _innerobj.g; } }
    public double[,] h { get { return _innerobj.h; } }
    public double[] x { get { return _innerobj.x; } }

    public lsfitstate()
    {
        _innerobj = new lsfit.lsfitstate();
    }

    public override alglibobject make_copy()
    {
        return new lsfitstate((lsfit.lsfitstate)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private lsfit.lsfitstate _innerobj;
    public lsfit.lsfitstate innerobj { get { return _innerobj; } }
    public lsfitstate(lsfit.lsfitstate obj)
    {
        _innerobj = obj;
    }
}




