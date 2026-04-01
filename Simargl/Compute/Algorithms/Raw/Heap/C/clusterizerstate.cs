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
This structure is a clusterization engine.

You should not try to access its fields directly.
Use ALGLIB functions in order to work with this object.

  -- ALGLIB --
     Copyright 10.07.2012 by Bochkanov Sergey
*************************************************************************/
public class clusterizerstate : alglibobject
{
    //
    // Public declarations
    //

    public clusterizerstate()
    {
        _innerobj = new clustering.clusterizerstate();
    }

    public override alglibobject make_copy()
    {
        return new clusterizerstate((clustering.clusterizerstate)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private clustering.clusterizerstate _innerobj;
    public clustering.clusterizerstate innerobj { get { return _innerobj; } }
    public clusterizerstate(clustering.clusterizerstate obj)
    {
        _innerobj = obj;
    }
}
