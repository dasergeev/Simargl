#pragma warning disable CS8618
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


/********************************************************************
reverse communication structure
********************************************************************/
public class rcommstate : apobject
{
    public rcommstate()
    {
        init();
    }
    public override void init()
    {
        stage = -1;
        ia = new int[0];
        ba = new bool[0];
        ra = new double[0];
        ca = new complex[0];
    }
    public override apobject make_copy()
    {
        rcommstate result = new rcommstate();
        result.stage = stage;
        result.ia = (int[])ia.Clone();
        result.ba = (bool[])ba.Clone();
        result.ra = (double[])ra.Clone();
        result.ca = (complex[])ca.Clone();
        return result;
    }
    public int stage;
    public int[] ia;
    public bool[] ba;
    public double[] ra;
    public complex[] ca;
};
