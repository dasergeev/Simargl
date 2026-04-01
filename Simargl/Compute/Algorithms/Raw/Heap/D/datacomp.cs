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

public class datacomp
{
    /*************************************************************************
    k-means++ clusterization.
    Backward compatibility function, we recommend to use CLUSTERING subpackage
    as better replacement.

      -- ALGLIB --
         Copyright 21.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void kmeansgenerate(double[,] xy,
        int npoints,
        int nvars,
        int k,
        int restarts,
        ref int info,
        ref double[,] c,
        ref int[] xyc,
        xparams _params)
    {
        double[,] dummy = new double[0, 0];
        int itscnt = 0;
        double e = 0;
        clustering.kmeansbuffers buf = new clustering.kmeansbuffers();

        info = 0;
        c = new double[0, 0];
        xyc = new int[0];

        clustering.kmeansinitbuf(buf, _params);
        clustering.kmeansgenerateinternal(xy, npoints, nvars, k, 0, 1, 0, restarts, false, ref info, ref itscnt, ref c, true, ref dummy, false, ref xyc, ref e, buf, _params);
    }


}
