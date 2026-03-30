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
public class corr
{
    /*************************************************************************
    1-dimensional complex cross-correlation.

    For given Pattern/Signal returns corr(Pattern,Signal) (non-circular).

    Correlation is calculated using reduction to  convolution.  Algorithm with
    max(N,N)*log(max(N,N)) complexity is used (see  ConvC1D()  for  more  info
    about performance).

    IMPORTANT:
        for  historical reasons subroutine accepts its parameters in  reversed
        order: CorrC1D(Signal, Pattern) = Pattern x Signal (using  traditional
        definition of cross-correlation, denoting cross-correlation as "x").

    INPUT PARAMETERS
        Signal  -   array[0..N-1] - complex function to be transformed,
                    signal containing pattern
        N       -   problem size
        Pattern -   array[0..M-1] - complex function to be transformed,
                    pattern to search withing signal
        M       -   problem size

    OUTPUT PARAMETERS
        R       -   cross-correlation, array[0..N+M-2]:
                    * positive lags are stored in R[0..N-1],
                      R[i] = sum(conj(pattern[j])*signal[i+j]
                    * negative lags are stored in R[N..N+M-2],
                      R[N+M-1-i] = sum(conj(pattern[j])*signal[-i+j]

    NOTE:
        It is assumed that pattern domain is [0..M-1].  If Pattern is non-zero
    on [-K..M-1],  you can still use this subroutine, just shift result by K.

      -- ALGLIB --
         Copyright 21.07.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void corrc1d(complex[] signal,
        int n,
        complex[] pattern,
        int m,
        ref complex[] r,
        xparams _params)
    {
        complex[] p = new complex[0];
        complex[] b = new complex[0];
        int i = 0;
        int i_ = 0;
        int i1_ = 0;

        r = new complex[0];

        ap.assert(n > 0 && m > 0, "CorrC1D: incorrect N or M!");
        p = new complex[m];
        for (i = 0; i <= m - 1; i++)
        {
            p[m - 1 - i] = math.conj(pattern[i]);
        }
        conv.convc1d(p, m, signal, n, ref b, _params);
        r = new complex[m + n - 1];
        i1_ = (m - 1) - (0);
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            r[i_] = b[i_ + i1_];
        }
        if (m + n - 2 >= n)
        {
            i1_ = (0) - (n);
            for (i_ = n; i_ <= m + n - 2; i_++)
            {
                r[i_] = b[i_ + i1_];
            }
        }
    }


    /*************************************************************************
    1-dimensional circular complex cross-correlation.

    For given Pattern/Signal returns corr(Pattern,Signal) (circular).
    Algorithm has linearithmic complexity for any M/N.

    IMPORTANT:
        for  historical reasons subroutine accepts its parameters in  reversed
        order:   CorrC1DCircular(Signal, Pattern) = Pattern x Signal    (using
        traditional definition of cross-correlation, denoting cross-correlation
        as "x").

    INPUT PARAMETERS
        Signal  -   array[0..N-1] - complex function to be transformed,
                    periodic signal containing pattern
        N       -   problem size
        Pattern -   array[0..M-1] - complex function to be transformed,
                    non-periodic pattern to search withing signal
        M       -   problem size

    OUTPUT PARAMETERS
        R   -   convolution: A*B. array[0..M-1].


      -- ALGLIB --
         Copyright 21.07.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void corrc1dcircular(complex[] signal,
        int m,
        complex[] pattern,
        int n,
        ref complex[] c,
        xparams _params)
    {
        complex[] p = new complex[0];
        complex[] b = new complex[0];
        int i1 = 0;
        int i2 = 0;
        int i = 0;
        int j2 = 0;
        int i_ = 0;
        int i1_ = 0;

        c = new complex[0];

        ap.assert(n > 0 && m > 0, "ConvC1DCircular: incorrect N or M!");

        //
        // normalize task: make M>=N,
        // so A will be longer (at least - not shorter) that B.
        //
        if (m < n)
        {
            b = new complex[m];
            for (i1 = 0; i1 <= m - 1; i1++)
            {
                b[i1] = 0;
            }
            i1 = 0;
            while (i1 < n)
            {
                i2 = Math.Min(i1 + m - 1, n - 1);
                j2 = i2 - i1;
                i1_ = (i1) - (0);
                for (i_ = 0; i_ <= j2; i_++)
                {
                    b[i_] = b[i_] + pattern[i_ + i1_];
                }
                i1 = i1 + m;
            }
            corrc1dcircular(signal, m, b, m, ref c, _params);
            return;
        }

        //
        // Task is normalized
        //
        p = new complex[n];
        for (i = 0; i <= n - 1; i++)
        {
            p[n - 1 - i] = math.conj(pattern[i]);
        }
        conv.convc1dcircular(signal, m, p, n, ref b, _params);
        c = new complex[m];
        i1_ = (n - 1) - (0);
        for (i_ = 0; i_ <= m - n; i_++)
        {
            c[i_] = b[i_ + i1_];
        }
        if (m - n + 1 <= m - 1)
        {
            i1_ = (0) - (m - n + 1);
            for (i_ = m - n + 1; i_ <= m - 1; i_++)
            {
                c[i_] = b[i_ + i1_];
            }
        }
    }


    /*************************************************************************
    1-dimensional real cross-correlation.

    For given Pattern/Signal returns corr(Pattern,Signal) (non-circular).

    Correlation is calculated using reduction to  convolution.  Algorithm with
    max(N,N)*log(max(N,N)) complexity is used (see  ConvC1D()  for  more  info
    about performance).

    IMPORTANT:
        for  historical reasons subroutine accepts its parameters in  reversed
        order: CorrR1D(Signal, Pattern) = Pattern x Signal (using  traditional
        definition of cross-correlation, denoting cross-correlation as "x").

    INPUT PARAMETERS
        Signal  -   array[0..N-1] - real function to be transformed,
                    signal containing pattern
        N       -   problem size
        Pattern -   array[0..M-1] - real function to be transformed,
                    pattern to search withing signal
        M       -   problem size

    OUTPUT PARAMETERS
        R       -   cross-correlation, array[0..N+M-2]:
                    * positive lags are stored in R[0..N-1],
                      R[i] = sum(pattern[j]*signal[i+j]
                    * negative lags are stored in R[N..N+M-2],
                      R[N+M-1-i] = sum(pattern[j]*signal[-i+j]

    NOTE:
        It is assumed that pattern domain is [0..M-1].  If Pattern is non-zero
    on [-K..M-1],  you can still use this subroutine, just shift result by K.

      -- ALGLIB --
         Copyright 21.07.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void corrr1d(double[] signal,
        int n,
        double[] pattern,
        int m,
        ref double[] r,
        xparams _params)
    {
        double[] p = new double[0];
        double[] b = new double[0];
        int i = 0;
        int i_ = 0;
        int i1_ = 0;

        r = new double[0];

        ap.assert(n > 0 && m > 0, "CorrR1D: incorrect N or M!");
        p = new double[m];
        for (i = 0; i <= m - 1; i++)
        {
            p[m - 1 - i] = pattern[i];
        }
        conv.convr1d(p, m, signal, n, ref b, _params);
        r = new double[m + n - 1];
        i1_ = (m - 1) - (0);
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            r[i_] = b[i_ + i1_];
        }
        if (m + n - 2 >= n)
        {
            i1_ = (0) - (n);
            for (i_ = n; i_ <= m + n - 2; i_++)
            {
                r[i_] = b[i_ + i1_];
            }
        }
    }


    /*************************************************************************
    1-dimensional circular real cross-correlation.

    For given Pattern/Signal returns corr(Pattern,Signal) (circular).
    Algorithm has linearithmic complexity for any M/N.

    IMPORTANT:
        for  historical reasons subroutine accepts its parameters in  reversed
        order:   CorrR1DCircular(Signal, Pattern) = Pattern x Signal    (using
        traditional definition of cross-correlation, denoting cross-correlation
        as "x").

    INPUT PARAMETERS
        Signal  -   array[0..N-1] - real function to be transformed,
                    periodic signal containing pattern
        N       -   problem size
        Pattern -   array[0..M-1] - real function to be transformed,
                    non-periodic pattern to search withing signal
        M       -   problem size

    OUTPUT PARAMETERS
        R   -   convolution: A*B. array[0..M-1].


      -- ALGLIB --
         Copyright 21.07.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void corrr1dcircular(double[] signal,
        int m,
        double[] pattern,
        int n,
        ref double[] c,
        xparams _params)
    {
        double[] p = new double[0];
        double[] b = new double[0];
        int i1 = 0;
        int i2 = 0;
        int i = 0;
        int j2 = 0;
        int i_ = 0;
        int i1_ = 0;

        c = new double[0];

        ap.assert(n > 0 && m > 0, "ConvC1DCircular: incorrect N or M!");

        //
        // normalize task: make M>=N,
        // so A will be longer (at least - not shorter) that B.
        //
        if (m < n)
        {
            b = new double[m];
            for (i1 = 0; i1 <= m - 1; i1++)
            {
                b[i1] = 0;
            }
            i1 = 0;
            while (i1 < n)
            {
                i2 = Math.Min(i1 + m - 1, n - 1);
                j2 = i2 - i1;
                i1_ = (i1) - (0);
                for (i_ = 0; i_ <= j2; i_++)
                {
                    b[i_] = b[i_] + pattern[i_ + i1_];
                }
                i1 = i1 + m;
            }
            corrr1dcircular(signal, m, b, m, ref c, _params);
            return;
        }

        //
        // Task is normalized
        //
        p = new double[n];
        for (i = 0; i <= n - 1; i++)
        {
            p[n - 1 - i] = pattern[i];
        }
        conv.convr1dcircular(signal, m, p, n, ref b, _params);
        c = new double[m];
        i1_ = (n - 1) - (0);
        for (i_ = 0; i_ <= m - n; i_++)
        {
            c[i_] = b[i_ + i1_];
        }
        if (m - n + 1 <= m - 1)
        {
            i1_ = (0) - (m - n + 1);
            for (i_ = m - n + 1; i_ <= m - 1; i_++)
            {
                c[i_] = b[i_ + i1_];
            }
        }
    }


}
