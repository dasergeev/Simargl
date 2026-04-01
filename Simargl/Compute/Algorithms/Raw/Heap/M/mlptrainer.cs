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
Trainer object for neural network.

You should not try to access fields of this object directly -  use  ALGLIB
functions to work with this object.
*************************************************************************/
public class mlptrainer : alglibobject
{
    //
    // Public declarations
    //

    public mlptrainer()
    {
        _innerobj = new mlptrain.mlptrainer();
    }

    public override alglibobject make_copy()
    {
        return new mlptrainer((mlptrain.mlptrainer)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private mlptrain.mlptrainer _innerobj;
    public mlptrain.mlptrainer innerobj { get { return _innerobj; } }
    public mlptrainer(mlptrain.mlptrainer obj)
    {
        _innerobj = obj;
    }
}
