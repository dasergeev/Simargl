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
public class odesolverstate : alglibobject
{
    //
    // Public declarations
    //
    public bool needdy { get { return _innerobj.needdy; } set { _innerobj.needdy = value; } }
    public double[] y { get { return _innerobj.y; } }
    public double[] dy { get { return _innerobj.dy; } }
    public double x { get { return _innerobj.x; } set { _innerobj.x = value; } }

    public odesolverstate()
    {
        _innerobj = new odesolver.odesolverstate();
    }

    public override alglibobject make_copy()
    {
        return new odesolverstate((odesolver.odesolverstate)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private odesolver.odesolverstate _innerobj;
    public odesolver.odesolverstate innerobj { get { return _innerobj; } }
    public odesolverstate(odesolver.odesolverstate obj)
    {
        _innerobj = obj;
    }
}
