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
This is a debug class intended for testing ALGLIB interface generator.
Never use it in any real life project.

  -- ALGLIB --
     Copyright 20.07.2021 by Bochkanov Sergey
*************************************************************************/
public class xdebugrecord1 : alglibobject
{
    //
    // Public declarations
    //
    public int i { get { return _innerobj.i; } set { _innerobj.i = value; } }
    public complex c { get { return _innerobj.c; } set { _innerobj.c = value; } }
    public double[] a { get { return _innerobj.a; } set { _innerobj.a = value; } }

    public xdebugrecord1()
    {
        _innerobj = new xdebug.xdebugrecord1();
    }

    public override alglibobject make_copy()
    {
        return new xdebugrecord1((xdebug.xdebugrecord1)_innerobj.make_copy());
    }

    //
    // Although some of declarations below are public, you should not use them
    // They are intended for internal use only
    //
    private xdebug.xdebugrecord1 _innerobj;
    public xdebug.xdebugrecord1 innerobj { get { return _innerobj; } }
    public xdebugrecord1(xdebug.xdebugrecord1 obj)
    {
        _innerobj = obj;
    }
}
