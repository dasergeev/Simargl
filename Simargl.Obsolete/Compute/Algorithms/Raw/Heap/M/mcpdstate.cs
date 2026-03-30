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
This structure is a MCPD (Markov Chains for Population Data) solver.

You should use ALGLIB functions in order to work with this object.

  -- ALGLIB --
     Copyright 23.05.2010 by Bochkanov Sergey
*************************************************************************/
public class mcpdstate : alglibobject
{
    //
    // Public declarations
    //

    public mcpdstate()
    {
        _innerobj = new mcpd.mcpdstate();
    }

    public override alglibobject make_copy()
    {
        return new mcpdstate((mcpd.mcpdstate)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private mcpd.mcpdstate _innerobj;
    public mcpd.mcpdstate innerobj { get { return _innerobj; } }
    public mcpdstate(mcpd.mcpdstate obj)
    {
        _innerobj = obj;
    }
}
