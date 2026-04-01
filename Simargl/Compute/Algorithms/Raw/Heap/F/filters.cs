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

public class filters
{
    /*************************************************************************
    Filters: simple moving averages (unsymmetric).

    This filter replaces array by results of SMA(K) filter. SMA(K) is defined
    as filter which averages at most K previous points (previous - not points
    AROUND central point) - or less, in case of the first K-1 points.

    INPUT PARAMETERS:
        X           -   array[N], array to process. It can be larger than N,
                        in this case only first N points are processed.
        N           -   points count, N>=0
        K           -   K>=1 (K can be larger than N ,  such  cases  will  be
                        correctly handled). Window width. K=1 corresponds  to
                        identity transformation (nothing changes).

    OUTPUT PARAMETERS:
        X           -   array, whose first N elements were processed with SMA(K)

    NOTE 1: this function uses efficient in-place  algorithm  which  does not
            allocate temporary arrays.

    NOTE 2: this algorithm makes only one pass through array and uses running
            sum  to speed-up calculation of the averages. Additional measures
            are taken to ensure that running sum on a long sequence  of  zero
            elements will be correctly reset to zero even in the presence  of
            round-off error.

    NOTE 3: this  is  unsymmetric version of the algorithm,  which  does  NOT
            averages points after the current one. Only X[i], X[i-1], ... are
            used when calculating new value of X[i]. We should also note that
            this algorithm uses BOTH previous points and  current  one,  i.e.
            new value of X[i] depends on BOTH previous point and X[i] itself.

      -- ALGLIB --
         Copyright 25.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void filtersma(double[] x,
        int n,
        int k,
        xparams _params)
    {
        int i = 0;
        double runningsum = 0;
        double termsinsum = 0;
        int zeroprefix = 0;
        double v = 0;

        ap.assert(n >= 0, "FilterSMA: N<0");
        ap.assert(ap.len(x) >= n, "FilterSMA: Length(X)<N");
        ap.assert(apserv.isfinitevector(x, n, _params), "FilterSMA: X contains INF or NAN");
        ap.assert(k >= 1, "FilterSMA: K<1");

        //
        // Quick exit, if necessary
        //
        if (n <= 1 || k == 1)
        {
            return;
        }

        //
        // Prepare variables (see below for explanation)
        //
        runningsum = 0.0;
        termsinsum = 0;
        for (i = Math.Max(n - k, 0); i <= n - 1; i++)
        {
            runningsum = runningsum + x[i];
            termsinsum = termsinsum + 1;
        }
        i = Math.Max(n - k, 0);
        zeroprefix = 0;
        while (i <= n - 1 && (double)(x[i]) == (double)(0))
        {
            zeroprefix = zeroprefix + 1;
            i = i + 1;
        }

        //
        // General case: we assume that N>1 and K>1
        //
        // Make one pass through all elements. At the beginning of
        // the iteration we have:
        // * I              element being processed
        // * RunningSum     current value of the running sum
        //                  (including I-th element)
        // * TermsInSum     number of terms in sum, 0<=TermsInSum<=K
        // * ZeroPrefix     length of the sequence of zero elements
        //                  which starts at X[I-K+1] and continues towards X[I].
        //                  Equal to zero in case X[I-K+1] is non-zero.
        //                  This value is used to make RunningSum exactly zero
        //                  when it follows from the problem properties.
        //
        for (i = n - 1; i >= 0; i--)
        {

            //
            // Store new value of X[i], save old value in V
            //
            v = x[i];
            x[i] = runningsum / termsinsum;

            //
            // Update RunningSum and TermsInSum
            //
            if (i - k >= 0)
            {
                runningsum = runningsum - v + x[i - k];
            }
            else
            {
                runningsum = runningsum - v;
                termsinsum = termsinsum - 1;
            }

            //
            // Update ZeroPrefix.
            // In case we have ZeroPrefix=TermsInSum,
            // RunningSum is reset to zero.
            //
            if (i - k >= 0)
            {
                if ((double)(x[i - k]) != (double)(0))
                {
                    zeroprefix = 0;
                }
                else
                {
                    zeroprefix = Math.Min(zeroprefix + 1, k);
                }
            }
            else
            {
                zeroprefix = Math.Min(zeroprefix, i + 1);
            }
            if ((double)(zeroprefix) == (double)(termsinsum))
            {
                runningsum = 0;
            }
        }
    }


    /*************************************************************************
    Filters: exponential moving averages.

    This filter replaces array by results of EMA(alpha) filter. EMA(alpha) is
    defined as filter which replaces X[] by S[]:
        S[0] = X[0]
        S[t] = alpha*X[t] + (1-alpha)*S[t-1]

    INPUT PARAMETERS:
        X           -   array[N], array to process. It can be larger than N,
                        in this case only first N points are processed.
        N           -   points count, N>=0
        alpha       -   0<alpha<=1, smoothing parameter.

    OUTPUT PARAMETERS:
        X           -   array, whose first N elements were processed
                        with EMA(alpha)

    NOTE 1: this function uses efficient in-place  algorithm  which  does not
            allocate temporary arrays.

    NOTE 2: this algorithm uses BOTH previous points and  current  one,  i.e.
            new value of X[i] depends on BOTH previous point and X[i] itself.

    NOTE 3: technical analytis users quite often work  with  EMA  coefficient
            expressed in DAYS instead of fractions. If you want to  calculate
            EMA(N), where N is a number of days, you can use alpha=2/(N+1).

      -- ALGLIB --
         Copyright 25.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void filterema(double[] x,
        int n,
        double alpha,
        xparams _params)
    {
        int i = 0;

        ap.assert(n >= 0, "FilterEMA: N<0");
        ap.assert(ap.len(x) >= n, "FilterEMA: Length(X)<N");
        ap.assert(apserv.isfinitevector(x, n, _params), "FilterEMA: X contains INF or NAN");
        ap.assert((double)(alpha) > (double)(0), "FilterEMA: Alpha<=0");
        ap.assert((double)(alpha) <= (double)(1), "FilterEMA: Alpha>1");

        //
        // Quick exit, if necessary
        //
        if (n <= 1 || (double)(alpha) == (double)(1))
        {
            return;
        }

        //
        // Process
        //
        for (i = 1; i <= n - 1; i++)
        {
            x[i] = alpha * x[i] + (1 - alpha) * x[i - 1];
        }
    }


    /*************************************************************************
    Filters: linear regression moving averages.

    This filter replaces array by results of LRMA(K) filter.

    LRMA(K) is defined as filter which, for each data  point,  builds  linear
    regression  model  using  K  prevous  points (point itself is included in
    these K points) and calculates value of this linear model at the point in
    question.

    INPUT PARAMETERS:
        X           -   array[N], array to process. It can be larger than N,
                        in this case only first N points are processed.
        N           -   points count, N>=0
        K           -   K>=1 (K can be larger than N ,  such  cases  will  be
                        correctly handled). Window width. K=1 corresponds  to
                        identity transformation (nothing changes).

    OUTPUT PARAMETERS:
        X           -   array, whose first N elements were processed with LRMA(K)

    NOTE 1: this function uses efficient in-place  algorithm  which  does not
            allocate temporary arrays.

    NOTE 2: this algorithm makes only one pass through array and uses running
            sum  to speed-up calculation of the averages. Additional measures
            are taken to ensure that running sum on a long sequence  of  zero
            elements will be correctly reset to zero even in the presence  of
            round-off error.

    NOTE 3: this  is  unsymmetric version of the algorithm,  which  does  NOT
            averages points after the current one. Only X[i], X[i-1], ... are
            used when calculating new value of X[i]. We should also note that
            this algorithm uses BOTH previous points and  current  one,  i.e.
            new value of X[i] depends on BOTH previous point and X[i] itself.

      -- ALGLIB --
         Copyright 25.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void filterlrma(double[] x,
        int n,
        int k,
        xparams _params)
    {
        int i = 0;
        int m = 0;
        double[,] xy = new double[0, 0];
        double[] s = new double[0];
        double a = 0;
        double b = 0;
        double vara = 0;
        double varb = 0;
        double covab = 0;
        double corrab = 0;
        double p = 0;
        int i_ = 0;
        int i1_ = 0;

        ap.assert(n >= 0, "FilterLRMA: N<0");
        ap.assert(ap.len(x) >= n, "FilterLRMA: Length(X)<N");
        ap.assert(apserv.isfinitevector(x, n, _params), "FilterLRMA: X contains INF or NAN");
        ap.assert(k >= 1, "FilterLRMA: K<1");

        //
        // Quick exit, if necessary:
        // * either N is equal to 1 (nothing to average)
        // * or K is 1 (only point itself is used) or 2 (model is too simple,
        //   we will always get identity transformation)
        //
        if (n <= 1 || k <= 2)
        {
            return;
        }

        //
        // General case: K>2, N>1.
        // We do not process points with I<2 because first two points (I=0 and I=1) will be
        // left unmodified by LRMA filter in any case.
        //
        xy = new double[k, 2];
        s = new double[k];
        for (i = 0; i <= k - 1; i++)
        {
            xy[i, 0] = i;
            s[i] = 1.0;
        }
        for (i = n - 1; i >= 2; i--)
        {
            m = Math.Min(i + 1, k);
            i1_ = (i - m + 1) - (0);
            for (i_ = 0; i_ <= m - 1; i_++)
            {
                xy[i_, 1] = x[i_ + i1_];
            }
            linreg.lrlines(xy, s, m, ref a, ref b, ref vara, ref varb, ref covab, ref corrab, ref p, _params);
            x[i] = a + b * (m - 1);
        }
    }


}
