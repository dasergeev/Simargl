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
This structure stores state of the integration algorithm.

Although this class has public fields,  they are not intended for external
use. You should use ALGLIB functions to work with this class:
* autogksmooth()/AutoGKSmoothW()/... to create objects
* autogkintegrate() to begin integration
* autogkresults() to get results
*************************************************************************/
public class autogkstate : alglibobject
{
    //
    // Public declarations
    //
    public bool needf { get { return _innerobj.needf; } set { _innerobj.needf = value; } }
    public double x { get { return _innerobj.x; } set { _innerobj.x = value; } }
    public double xminusa { get { return _innerobj.xminusa; } set { _innerobj.xminusa = value; } }
    public double bminusx { get { return _innerobj.bminusx; } set { _innerobj.bminusx = value; } }
    public double f { get { return _innerobj.f; } set { _innerobj.f = value; } }

    public autogkstate()
    {
        _innerobj = new autogk.autogkstate();
    }

    public override alglibobject make_copy()
    {
        return new autogkstate((autogk.autogkstate)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private autogk.autogkstate _innerobj;
    public autogk.autogkstate innerobj { get { return _innerobj; } }
    public autogkstate(autogk.autogkstate obj)
    {
        _innerobj = obj;
    }
}
