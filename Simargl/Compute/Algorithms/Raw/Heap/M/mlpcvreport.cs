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
Cross-validation estimates of generalization error
*************************************************************************/
public class mlpcvreport : alglibobject
{
    //
    // Public declarations
    //
    public double relclserror { get { return _innerobj.relclserror; } set { _innerobj.relclserror = value; } }
    public double avgce { get { return _innerobj.avgce; } set { _innerobj.avgce = value; } }
    public double rmserror { get { return _innerobj.rmserror; } set { _innerobj.rmserror = value; } }
    public double avgerror { get { return _innerobj.avgerror; } set { _innerobj.avgerror = value; } }
    public double avgrelerror { get { return _innerobj.avgrelerror; } set { _innerobj.avgrelerror = value; } }

    public mlpcvreport()
    {
        _innerobj = new mlptrain.mlpcvreport();
    }

    public override alglibobject make_copy()
    {
        return new mlpcvreport((mlptrain.mlpcvreport)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private mlptrain.mlpcvreport _innerobj;
    public mlptrain.mlpcvreport innerobj { get { return _innerobj; } }
    public mlpcvreport(mlptrain.mlpcvreport obj)
    {
        _innerobj = obj;
    }
}
