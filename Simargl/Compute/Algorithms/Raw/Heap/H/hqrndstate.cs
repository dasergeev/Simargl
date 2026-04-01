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
Portable high quality random number generator state.
Initialized with HQRNDRandomize() or HQRNDSeed().

Fields:
    S1, S2      -   seed values
    V           -   precomputed value
    MagicV      -   'magic' value used to determine whether State structure
                    was correctly initialized.
*************************************************************************/
public class hqrndstate : alglibobject
{
    //
    // Public declarations
    //

    public hqrndstate()
    {
        _innerobj = new hqrnd.hqrndstate();
    }

    public override alglibobject make_copy()
    {
        return new hqrndstate((hqrnd.hqrndstate)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private hqrnd.hqrndstate _innerobj;
    public hqrnd.hqrndstate innerobj { get { return _innerobj; } }
    public hqrndstate(hqrnd.hqrndstate obj)
    {
        _innerobj = obj;
    }
}
