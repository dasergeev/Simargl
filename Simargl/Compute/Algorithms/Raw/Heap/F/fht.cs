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






public class fht
{
    /*************************************************************************
    1-dimensional Fast Hartley Transform.

    Algorithm has O(N*logN) complexity for any N (composite or prime).

    INPUT PARAMETERS
        A   -   array[0..N-1] - real function to be transformed
        N   -   problem size
        
    OUTPUT PARAMETERS
        A   -   FHT of a input array, array[0..N-1],
                A_out[k] = sum(A_in[j]*(cos(2*pi*j*k/N)+sin(2*pi*j*k/N)), j=0..N-1)


      -- ALGLIB --
         Copyright 04.06.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void fhtr1d(double[] a,
        int n,
        xparams _params)
    {
        int i = 0;
        complex[] fa = new complex[0];

        ap.assert(n > 0, "FHTR1D: incorrect N!");

        //
        // Special case: N=1, FHT is just identity transform.
        // After this block we assume that N is strictly greater than 1.
        //
        if (n == 1)
        {
            return;
        }

        //
        // Reduce FHt to real FFT
        //
        fft.fftr1d(a, n, ref fa, _params);
        for (i = 0; i <= n - 1; i++)
        {
            a[i] = fa[i].x - fa[i].y;
        }
    }


    /*************************************************************************
    1-dimensional inverse FHT.

    Algorithm has O(N*logN) complexity for any N (composite or prime).

    INPUT PARAMETERS
        A   -   array[0..N-1] - complex array to be transformed
        N   -   problem size

    OUTPUT PARAMETERS
        A   -   inverse FHT of a input array, array[0..N-1]


      -- ALGLIB --
         Copyright 29.05.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void fhtr1dinv(double[] a,
        int n,
        xparams _params)
    {
        int i = 0;

        ap.assert(n > 0, "FHTR1DInv: incorrect N!");

        //
        // Special case: N=1, iFHT is just identity transform.
        // After this block we assume that N is strictly greater than 1.
        //
        if (n == 1)
        {
            return;
        }

        //
        // Inverse FHT can be expressed in terms of the FHT as
        //
        //     invfht(x) = fht(x)/N
        //
        fhtr1d(a, n, _params);
        for (i = 0; i <= n - 1; i++)
        {
            a[i] = a[i] / n;
        }
    }


}
