using System;

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


public class fft
{
    /*************************************************************************
    1-dimensional complex FFT.

    Array size N may be arbitrary number (composite or prime).  Composite  N's
    are handled with cache-oblivious variation of  a  Cooley-Tukey  algorithm.
    Small prime-factors are transformed using hard coded  codelets (similar to
    FFTW codelets, but without low-level  optimization),  large  prime-factors
    are handled with Bluestein's algorithm.

    Fastests transforms are for smooth N's (prime factors are 2, 3,  5  only),
    most fast for powers of 2. When N have prime factors  larger  than  these,
    but orders of magnitude smaller than N, computations will be about 4 times
    slower than for nearby highly composite N's. When N itself is prime, speed
    will be 6 times lower.

    Algorithm has O(N*logN) complexity for any N (composite or prime).

    INPUT PARAMETERS
        A   -   array[0..N-1] - complex function to be transformed
        N   -   problem size
        
    OUTPUT PARAMETERS
        A   -   DFT of a input array, array[0..N-1]
                A_out[j] = SUM(A_in[k]*exp(-2*pi*sqrt(-1)*j*k/N), k = 0..N-1)


      -- ALGLIB --
         Copyright 29.05.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void fftc1d(complex[] a,
        int n,
        xparams _params)
    {
        ftbase.fasttransformplan plan = new ftbase.fasttransformplan();
        int i = 0;
        double[] buf = new double[0];

        ap.assert(n > 0, "FFTC1D: incorrect N!");
        ap.assert(ap.len(a) >= n, "FFTC1D: Length(A)<N!");
        ap.assert(apserv.isfinitecvector(a, n, _params), "FFTC1D: A contains infinite or NAN values!");

        //
        // Special case: N=1, FFT is just identity transform.
        // After this block we assume that N is strictly greater than 1.
        //
        if (n == 1)
        {
            return;
        }

        //
        // convert input array to the more convinient format
        //
        buf = new double[2 * n];
        for (i = 0; i <= n - 1; i++)
        {
            buf[2 * i + 0] = a[i].x;
            buf[2 * i + 1] = a[i].y;
        }

        //
        // Generate plan and execute it.
        //
        // Plan is a combination of a successive factorizations of N and
        // precomputed data. It is much like a FFTW plan, but is not stored
        // between subroutine calls and is much simpler.
        //
        ftbase.ftcomplexfftplan(n, 1, plan, _params);
        ftbase.ftapplyplan(plan, buf, 0, 1, _params);

        //
        // result
        //
        for (i = 0; i <= n - 1; i++)
        {
            a[i].x = buf[2 * i + 0];
            a[i].y = buf[2 * i + 1];
        }
    }


    /*************************************************************************
    1-dimensional complex inverse FFT.

    Array size N may be arbitrary number (composite or prime).  Algorithm  has
    O(N*logN) complexity for any N (composite or prime).

    See FFTC1D() description for more information about algorithm performance.

    INPUT PARAMETERS
        A   -   array[0..N-1] - complex array to be transformed
        N   -   problem size

    OUTPUT PARAMETERS
        A   -   inverse DFT of a input array, array[0..N-1]
                A_out[j] = SUM(A_in[k]/N*exp(+2*pi*sqrt(-1)*j*k/N), k = 0..N-1)


      -- ALGLIB --
         Copyright 29.05.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void fftc1dinv(complex[] a,
        int n,
        xparams _params)
    {
        int i = 0;

        ap.assert(n > 0, "FFTC1DInv: incorrect N!");
        ap.assert(ap.len(a) >= n, "FFTC1DInv: Length(A)<N!");
        ap.assert(apserv.isfinitecvector(a, n, _params), "FFTC1DInv: A contains infinite or NAN values!");

        //
        // Inverse DFT can be expressed in terms of the DFT as
        //
        //     invfft(x) = fft(x')'/N
        //
        // here x' means conj(x).
        //
        for (i = 0; i <= n - 1; i++)
        {
            a[i].y = -a[i].y;
        }
        fftc1d(a, n, _params);
        for (i = 0; i <= n - 1; i++)
        {
            a[i].x = a[i].x / n;
            a[i].y = -(a[i].y / n);
        }
    }


    /*************************************************************************
    1-dimensional real FFT.

    Algorithm has O(N*logN) complexity for any N (composite or prime).

    INPUT PARAMETERS
        A   -   array[0..N-1] - real function to be transformed
        N   -   problem size

    OUTPUT PARAMETERS
        F   -   DFT of a input array, array[0..N-1]
                F[j] = SUM(A[k]*exp(-2*pi*sqrt(-1)*j*k/N), k = 0..N-1)

    NOTE:
        F[] satisfies symmetry property F[k] = conj(F[N-k]),  so just one half
    of  array  is  usually needed. But for convinience subroutine returns full
    complex array (with frequencies above N/2), so its result may be  used  by
    other FFT-related subroutines.


      -- ALGLIB --
         Copyright 01.06.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void fftr1d(double[] a,
        int n,
        ref complex[] f,
        xparams _params)
    {
        int i = 0;
        int n2 = 0;
        int idx = 0;
        complex hn = 0;
        complex hmnc = 0;
        complex v = 0;
        double[] buf = new double[0];
        ftbase.fasttransformplan plan = new ftbase.fasttransformplan();
        int i_ = 0;

        f = new complex[0];

        ap.assert(n > 0, "FFTR1D: incorrect N!");
        ap.assert(ap.len(a) >= n, "FFTR1D: Length(A)<N!");
        ap.assert(apserv.isfinitevector(a, n, _params), "FFTR1D: A contains infinite or NAN values!");

        //
        // Special cases:
        // * N=1, FFT is just identity transform.
        // * N=2, FFT is simple too
        //
        // After this block we assume that N is strictly greater than 2
        //
        if (n == 1)
        {
            f = new complex[1];
            f[0] = a[0];
            return;
        }
        if (n == 2)
        {
            f = new complex[2];
            f[0].x = a[0] + a[1];
            f[0].y = 0;
            f[1].x = a[0] - a[1];
            f[1].y = 0;
            return;
        }

        //
        // Choose between odd-size and even-size FFTs
        //
        if (n % 2 == 0)
        {

            //
            // even-size real FFT, use reduction to the complex task
            //
            n2 = n / 2;
            buf = new double[n];
            for (i_ = 0; i_ <= n - 1; i_++)
            {
                buf[i_] = a[i_];
            }
            ftbase.ftcomplexfftplan(n2, 1, plan, _params);
            ftbase.ftapplyplan(plan, buf, 0, 1, _params);
            f = new complex[n];
            for (i = 0; i <= n2; i++)
            {
                idx = 2 * (i % n2);
                hn.x = buf[idx + 0];
                hn.y = buf[idx + 1];
                idx = 2 * ((n2 - i) % n2);
                hmnc.x = buf[idx + 0];
                hmnc.y = -buf[idx + 1];
                v.x = -Math.Sin(-(2 * Math.PI * i / n));
                v.y = Math.Cos(-(2 * Math.PI * i / n));
                f[i] = hn + hmnc - v * (hn - hmnc);
                f[i].x = 0.5 * f[i].x;
                f[i].y = 0.5 * f[i].y;
            }
            for (i = n2 + 1; i <= n - 1; i++)
            {
                f[i] = math.conj(f[n - i]);
            }
        }
        else
        {

            //
            // use complex FFT
            //
            f = new complex[n];
            for (i = 0; i <= n - 1; i++)
            {
                f[i] = a[i];
            }
            fftc1d(f, n, _params);
        }
    }


    /*************************************************************************
    1-dimensional real inverse FFT.

    Algorithm has O(N*logN) complexity for any N (composite or prime).

    INPUT PARAMETERS
        F   -   array[0..floor(N/2)] - frequencies from forward real FFT
        N   -   problem size

    OUTPUT PARAMETERS
        A   -   inverse DFT of a input array, array[0..N-1]

    NOTE:
        F[] should satisfy symmetry property F[k] = conj(F[N-k]), so just  one
    half of frequencies array is needed - elements from 0 to floor(N/2).  F[0]
    is ALWAYS real. If N is even F[floor(N/2)] is real too. If N is odd,  then
    F[floor(N/2)] has no special properties.

    Relying on properties noted above, FFTR1DInv subroutine uses only elements
    from 0th to floor(N/2)-th. It ignores imaginary part of F[0],  and in case
    N is even it ignores imaginary part of F[floor(N/2)] too.

    When you call this function using full arguments list - "FFTR1DInv(F,N,A)"
    - you can pass either either frequencies array with N elements or  reduced
    array with roughly N/2 elements - subroutine will  successfully  transform
    both.

    If you call this function using reduced arguments list -  "FFTR1DInv(F,A)"
    - you must pass FULL array with N elements (although higher  N/2 are still
    not used) because array size is used to automatically determine FFT length


      -- ALGLIB --
         Copyright 01.06.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void fftr1dinv(complex[] f,
        int n,
        ref double[] a,
        xparams _params)
    {
        int i = 0;
        double[] h = new double[0];
        complex[] fh = new complex[0];

        a = new double[0];

        ap.assert(n > 0, "FFTR1DInv: incorrect N!");
        ap.assert(ap.len(f) >= (int)Math.Floor((double)n / (double)2) + 1, "FFTR1DInv: Length(F)<Floor(N/2)+1!");
        ap.assert(math.isfinite(f[0].x), "FFTR1DInv: F contains infinite or NAN values!");
        for (i = 1; i <= (int)Math.Floor((double)n / (double)2) - 1; i++)
        {
            ap.assert(math.isfinite(f[i].x) && math.isfinite(f[i].y), "FFTR1DInv: F contains infinite or NAN values!");
        }
        ap.assert(math.isfinite(f[(int)Math.Floor((double)n / (double)2)].x), "FFTR1DInv: F contains infinite or NAN values!");
        if (n % 2 != 0)
        {
            ap.assert(math.isfinite(f[(int)Math.Floor((double)n / (double)2)].y), "FFTR1DInv: F contains infinite or NAN values!");
        }

        //
        // Special case: N=1, FFT is just identity transform.
        // After this block we assume that N is strictly greater than 1.
        //
        if (n == 1)
        {
            a = new double[1];
            a[0] = f[0].x;
            return;
        }

        //
        // inverse real FFT is reduced to the inverse real FHT,
        // which is reduced to the forward real FHT,
        // which is reduced to the forward real FFT.
        //
        // Don't worry, it is really compact and efficient reduction :)
        //
        h = new double[n];
        a = new double[n];
        h[0] = f[0].x;
        for (i = 1; i <= (int)Math.Floor((double)n / (double)2) - 1; i++)
        {
            h[i] = f[i].x - f[i].y;
            h[n - i] = f[i].x + f[i].y;
        }
        if (n % 2 == 0)
        {
            h[(int)Math.Floor((double)n / (double)2)] = f[(int)Math.Floor((double)n / (double)2)].x;
        }
        else
        {
            h[(int)Math.Floor((double)n / (double)2)] = f[(int)Math.Floor((double)n / (double)2)].x - f[(int)Math.Floor((double)n / (double)2)].y;
            h[(int)Math.Floor((double)n / (double)2) + 1] = f[(int)Math.Floor((double)n / (double)2)].x + f[(int)Math.Floor((double)n / (double)2)].y;
        }
        fftr1d(h, n, ref fh, _params);
        for (i = 0; i <= n - 1; i++)
        {
            a[i] = (fh[i].x - fh[i].y) / n;
        }
    }


    /*************************************************************************
    Internal subroutine. Never call it directly!


      -- ALGLIB --
         Copyright 01.06.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void fftr1dinternaleven(ref double[] a,
        int n,
        ref double[] buf,
        ftbase.fasttransformplan plan,
        xparams _params)
    {
        double x = 0;
        double y = 0;
        int i = 0;
        int n2 = 0;
        int idx = 0;
        complex hn = 0;
        complex hmnc = 0;
        complex v = 0;
        int i_ = 0;

        ap.assert(n > 0 && n % 2 == 0, "FFTR1DEvenInplace: incorrect N!");

        //
        // Special cases:
        // * N=2
        //
        // After this block we assume that N is strictly greater than 2
        //
        if (n == 2)
        {
            x = a[0] + a[1];
            y = a[0] - a[1];
            a[0] = x;
            a[1] = y;
            return;
        }

        //
        // even-size real FFT, use reduction to the complex task
        //
        n2 = n / 2;
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            buf[i_] = a[i_];
        }
        ftbase.ftapplyplan(plan, buf, 0, 1, _params);
        a[0] = buf[0] + buf[1];
        for (i = 1; i <= n2 - 1; i++)
        {
            idx = 2 * (i % n2);
            hn.x = buf[idx + 0];
            hn.y = buf[idx + 1];
            idx = 2 * (n2 - i);
            hmnc.x = buf[idx + 0];
            hmnc.y = -buf[idx + 1];
            v.x = -Math.Sin(-(2 * Math.PI * i / n));
            v.y = Math.Cos(-(2 * Math.PI * i / n));
            v = hn + hmnc - v * (hn - hmnc);
            a[2 * i + 0] = 0.5 * v.x;
            a[2 * i + 1] = 0.5 * v.y;
        }
        a[1] = buf[0] - buf[1];
    }


    /*************************************************************************
    Internal subroutine. Never call it directly!


      -- ALGLIB --
         Copyright 01.06.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void fftr1dinvinternaleven(ref double[] a,
        int n,
        ref double[] buf,
        ftbase.fasttransformplan plan,
        xparams _params)
    {
        double x = 0;
        double y = 0;
        double t = 0;
        int i = 0;
        int n2 = 0;

        ap.assert(n > 0 && n % 2 == 0, "FFTR1DInvInternalEven: incorrect N!");

        //
        // Special cases:
        // * N=2
        //
        // After this block we assume that N is strictly greater than 2
        //
        if (n == 2)
        {
            x = 0.5 * (a[0] + a[1]);
            y = 0.5 * (a[0] - a[1]);
            a[0] = x;
            a[1] = y;
            return;
        }

        //
        // inverse real FFT is reduced to the inverse real FHT,
        // which is reduced to the forward real FHT,
        // which is reduced to the forward real FFT.
        //
        // Don't worry, it is really compact and efficient reduction :)
        //
        n2 = n / 2;
        buf[0] = a[0];
        for (i = 1; i <= n2 - 1; i++)
        {
            x = a[2 * i + 0];
            y = a[2 * i + 1];
            buf[i] = x - y;
            buf[n - i] = x + y;
        }
        buf[n2] = a[1];
        fftr1dinternaleven(ref buf, n, ref a, plan, _params);
        a[0] = buf[0] / n;
        t = (double)1 / (double)n;
        for (i = 1; i <= n2 - 1; i++)
        {
            x = buf[2 * i + 0];
            y = buf[2 * i + 1];
            a[i] = t * (x - y);
            a[n - i] = t * (x + y);
        }
        a[n2] = buf[1] / n;
    }


}
