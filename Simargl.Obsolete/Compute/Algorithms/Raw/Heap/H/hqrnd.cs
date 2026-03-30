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

public class hqrnd
{
    /*************************************************************************
    Portable high quality random number generator state.
    Initialized with HQRNDRandomize() or HQRNDSeed().

    Fields:
        S1, S2      -   seed values
        V           -   precomputed value
        MagicV      -   'magic' value used to determine whether State structure
                        was correctly initialized.
    *************************************************************************/
    public class hqrndstate : apobject
    {
        public int s1;
        public int s2;
        public int magicv;
        public hqrndstate()
        {
            init();
        }
        public override void init()
        {
        }
        public override apobject make_copy()
        {
            hqrndstate _result = new hqrndstate();
            _result.s1 = s1;
            _result.s2 = s2;
            _result.magicv = magicv;
            return _result;
        }
    };




    public const int hqrndmax = 2147483561;
    public const int hqrndm1 = 2147483563;
    public const int hqrndm2 = 2147483399;
    public const int hqrndmagic = 1634357784;


    /*************************************************************************
    HQRNDState  initialization  with  random  values  which come from standard
    RNG.

      -- ALGLIB --
         Copyright 02.12.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void hqrndrandomize(hqrndstate state,
        xparams _params)
    {
        int s0 = 0;
        int s1 = 0;

        s0 = math.randominteger(hqrndm1);
        s1 = math.randominteger(hqrndm2);
        hqrndseed(s0, s1, state, _params);
    }


    /*************************************************************************
    HQRNDState initialization with seed values

      -- ALGLIB --
         Copyright 02.12.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void hqrndseed(int s1,
        int s2,
        hqrndstate state,
        xparams _params)
    {

        //
        // Protection against negative seeds:
        //
        //     SEED := -(SEED+1)
        //
        // We can use just "-SEED" because there exists such integer number  N
        // that N<0, -N=N<0 too. (This number is equal to 0x800...000).   Need
        // to handle such seed correctly forces us to use  a  bit  complicated
        // formula.
        //
        if (s1 < 0)
        {
            s1 = -(s1 + 1);
        }
        if (s2 < 0)
        {
            s2 = -(s2 + 1);
        }
        state.s1 = s1 % (hqrndm1 - 1) + 1;
        state.s2 = s2 % (hqrndm2 - 1) + 1;
        state.magicv = hqrndmagic;
    }


    /*************************************************************************
    This function generates random real number in (0,1),
    not including interval boundaries

    State structure must be initialized with HQRNDRandomize() or HQRNDSeed().

      -- ALGLIB --
         Copyright 02.12.2009 by Bochkanov Sergey
    *************************************************************************/
    public static double hqrnduniformr(hqrndstate state,
        xparams _params)
    {
        double result = 0;

        result = (double)(hqrndintegerbase(state, _params) + 1) / (double)(hqrndmax + 2);
        return result;
    }


    /*************************************************************************
    This function generates random integer number in [0, N)

    1. State structure must be initialized with HQRNDRandomize() or HQRNDSeed()
    2. N can be any positive number except for very large numbers:
       * close to 2^31 on 32-bit systems
       * close to 2^62 on 64-bit systems
       An exception will be generated if N is too large.

      -- ALGLIB --
         Copyright 02.12.2009 by Bochkanov Sergey
    *************************************************************************/
    public static int hqrnduniformi(hqrndstate state,
        int n,
        xparams _params)
    {
        int result = 0;
        int maxcnt = 0;
        int mx = 0;
        int a = 0;
        int b = 0;

        ap.assert(n > 0, "HQRNDUniformI: N<=0!");
        maxcnt = hqrndmax + 1;

        //
        // Two branches: one for N<=MaxCnt, another for N>MaxCnt.
        //
        if (n > maxcnt)
        {

            //
            // N>=MaxCnt.
            //
            // We have two options here:
            // a) N is exactly divisible by MaxCnt
            // b) N is not divisible by MaxCnt
            //
            // In both cases we reduce problem on interval spanning [0,N)
            // to several subproblems on intervals spanning [0,MaxCnt).
            //
            if (n % maxcnt == 0)
            {

                //
                // N is exactly divisible by MaxCnt.
                //
                // [0,N) range is dividided into N/MaxCnt bins,
                // each of them having length equal to MaxCnt.
                //
                // We generate:
                // * random bin number B
                // * random offset within bin A
                // Both random numbers are generated by recursively
                // calling HQRNDUniformI().
                //
                // Result is equal to A+MaxCnt*B.
                //
                ap.assert(n / maxcnt <= maxcnt, "HQRNDUniformI: N is too large");
                a = hqrnduniformi(state, maxcnt, _params);
                b = hqrnduniformi(state, n / maxcnt, _params);
                result = a + maxcnt * b;
            }
            else
            {

                //
                // N is NOT exactly divisible by MaxCnt.
                //
                // [0,N) range is dividided into Ceil(N/MaxCnt) bins,
                // each of them having length equal to MaxCnt.
                //
                // We generate:
                // * random bin number B in [0, Ceil(N/MaxCnt)-1]
                // * random offset within bin A
                // * if both of what is below is true
                //   1) bin number B is that of the last bin
                //   2) A >= N mod MaxCnt
                //   then we repeat generation of A/B.
                //   This stage is essential in order to avoid bias in the result.
                // * otherwise, we return A*MaxCnt+N
                //
                ap.assert(n / maxcnt + 1 <= maxcnt, "HQRNDUniformI: N is too large");
                result = -1;
                do
                {
                    a = hqrnduniformi(state, maxcnt, _params);
                    b = hqrnduniformi(state, n / maxcnt + 1, _params);
                    if (b == n / maxcnt && a >= n % maxcnt)
                    {
                        continue;
                    }
                    result = a + maxcnt * b;
                }
                while (result < 0);
            }
        }
        else
        {

            //
            // N<=MaxCnt
            //
            // Code below is a bit complicated because we can not simply
            // return "HQRNDIntegerBase() mod N" - it will be skewed for
            // large N's in [0.1*HQRNDMax...HQRNDMax].
            //
            mx = maxcnt - maxcnt % n;
            do
            {
                result = hqrndintegerbase(state, _params);
            }
            while (result >= mx);
            result = result % n;
        }
        return result;
    }


    /*************************************************************************
    Random number generator: normal numbers

    This function generates one random number from normal distribution.
    Its performance is equal to that of HQRNDNormal2()

    State structure must be initialized with HQRNDRandomize() or HQRNDSeed().

      -- ALGLIB --
         Copyright 02.12.2009 by Bochkanov Sergey
    *************************************************************************/
    public static double hqrndnormal(hqrndstate state,
        xparams _params)
    {
        double result = 0;
        double v1 = 0;
        double v2 = 0;

        hqrndnormal2(state, ref v1, ref v2, _params);
        result = v1;
        return result;
    }


    /*************************************************************************
    Random number generator: vector with random entries (normal distribution)

    This function generates N random numbers from normal distribution.

    State structure must be initialized with HQRNDRandomize() or HQRNDSeed().

      -- ALGLIB --
         Copyright 02.12.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void hqrndnormalv(hqrndstate state,
        int n,
        ref double[] x,
        xparams _params)
    {
        int i = 0;
        int n2 = 0;
        double v1 = 0;
        double v2 = 0;

        x = new double[0];

        n2 = n / 2;
        ablasf.rallocv(n, ref x, _params);
        for (i = 0; i <= n2 - 1; i++)
        {
            hqrndnormal2(state, ref v1, ref v2, _params);
            x[2 * i + 0] = v1;
            x[2 * i + 1] = v2;
        }
        if (n % 2 != 0)
        {
            hqrndnormal2(state, ref v1, ref v2, _params);
            x[n - 1] = v1;
        }
    }


    /*************************************************************************
    Random number generator: matrix with random entries (normal distribution)

    This function generates MxN random matrix.

    State structure must be initialized with HQRNDRandomize() or HQRNDSeed().

      -- ALGLIB --
         Copyright 02.12.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void hqrndnormalm(hqrndstate state,
        int m,
        int n,
        ref double[,] x,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int n2 = 0;
        double v1 = 0;
        double v2 = 0;

        x = new double[0, 0];

        n2 = n / 2;
        x = new double[m, n];
        for (i = 0; i <= m - 1; i++)
        {
            for (j = 0; j <= n2 - 1; j++)
            {
                hqrndnormal2(state, ref v1, ref v2, _params);
                x[i, 2 * j + 0] = v1;
                x[i, 2 * j + 1] = v2;
            }
            if (n % 2 != 0)
            {
                hqrndnormal2(state, ref v1, ref v2, _params);
                x[i, n - 1] = v1;
            }
        }
    }


    /*************************************************************************
    Random number generator: random X and Y such that X^2+Y^2=1

    State structure must be initialized with HQRNDRandomize() or HQRNDSeed().

      -- ALGLIB --
         Copyright 02.12.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void hqrndunit2(hqrndstate state,
        ref double x,
        ref double y,
        xparams _params)
    {
        double v = 0;
        double mx = 0;
        double mn = 0;

        x = 0;
        y = 0;

        do
        {
            hqrndnormal2(state, ref x, ref y, _params);
        }
        while (!((double)(x) != (double)(0) || (double)(y) != (double)(0)));
        mx = Math.Max(Math.Abs(x), Math.Abs(y));
        mn = Math.Min(Math.Abs(x), Math.Abs(y));
        v = mx * Math.Sqrt(1 + math.sqr(mn / mx));
        x = x / v;
        y = y / v;
    }


    /*************************************************************************
    Random number generator: normal numbers

    This function generates two independent random numbers from normal
    distribution. Its performance is equal to that of HQRNDNormal()

    State structure must be initialized with HQRNDRandomize() or HQRNDSeed().

      -- ALGLIB --
         Copyright 02.12.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void hqrndnormal2(hqrndstate state,
        ref double x1,
        ref double x2,
        xparams _params)
    {
        double u = 0;
        double v = 0;
        double s = 0;

        x1 = 0;
        x2 = 0;

        while (true)
        {
            u = 2 * hqrnduniformr(state, _params) - 1;
            v = 2 * hqrnduniformr(state, _params) - 1;
            s = math.sqr(u) + math.sqr(v);
            if ((double)(s) > (double)(0) && (double)(s) < (double)(1))
            {

                //
                // two Sqrt's instead of one to
                // avoid overflow when S is too small
                //
                s = Math.Sqrt(-(2 * Math.Log(s))) / Math.Sqrt(s);
                x1 = u * s;
                x2 = v * s;
                return;
            }
        }
    }


    /*************************************************************************
    Random number generator: exponential distribution

    State structure must be initialized with HQRNDRandomize() or HQRNDSeed().

      -- ALGLIB --
         Copyright 11.08.2007 by Bochkanov Sergey
    *************************************************************************/
    public static double hqrndexponential(hqrndstate state,
        double lambdav,
        xparams _params)
    {
        double result = 0;

        ap.assert((double)(lambdav) > (double)(0), "HQRNDExponential: LambdaV<=0!");
        result = -(Math.Log(hqrnduniformr(state, _params)) / lambdav);
        return result;
    }


    /*************************************************************************
    This function generates  random number from discrete distribution given by
    finite sample X.

    INPUT PARAMETERS
        State   -   high quality random number generator, must be
                    initialized with HQRNDRandomize() or HQRNDSeed().
            X   -   finite sample
            N   -   number of elements to use, N>=1

    RESULT
        this function returns one of the X[i] for random i=0..N-1

      -- ALGLIB --
         Copyright 08.11.2011 by Bochkanov Sergey
    *************************************************************************/
    public static double hqrnddiscrete(hqrndstate state,
        double[] x,
        int n,
        xparams _params)
    {
        double result = 0;

        ap.assert(n > 0, "HQRNDDiscrete: N<=0");
        ap.assert(n <= ap.len(x), "HQRNDDiscrete: Length(X)<N");
        result = x[hqrnduniformi(state, n, _params)];
        return result;
    }


    /*************************************************************************
    This function generates random number from continuous  distribution  given
    by finite sample X.

    INPUT PARAMETERS
        State   -   high quality random number generator, must be
                    initialized with HQRNDRandomize() or HQRNDSeed().
            X   -   finite sample, array[N] (can be larger, in this  case only
                    leading N elements are used). THIS ARRAY MUST BE SORTED BY
                    ASCENDING.
            N   -   number of elements to use, N>=1

    RESULT
        this function returns random number from continuous distribution which  
        tries to approximate X as mush as possible. min(X)<=Result<=max(X).

      -- ALGLIB --
         Copyright 08.11.2011 by Bochkanov Sergey
    *************************************************************************/
    public static double hqrndcontinuous(hqrndstate state,
        double[] x,
        int n,
        xparams _params)
    {
        double result = 0;
        double mx = 0;
        double mn = 0;
        int i = 0;

        ap.assert(n > 0, "HQRNDContinuous: N<=0");
        ap.assert(n <= ap.len(x), "HQRNDContinuous: Length(X)<N");
        if (n == 1)
        {
            result = x[0];
            return result;
        }
        i = hqrnduniformi(state, n - 1, _params);
        mn = x[i];
        mx = x[i + 1];
        ap.assert((double)(mx) >= (double)(mn), "HQRNDDiscrete: X is not sorted by ascending");
        if ((double)(mx) != (double)(mn))
        {
            result = (mx - mn) * hqrnduniformr(state, _params) + mn;
        }
        else
        {
            result = mn;
        }
        return result;
    }


    /*************************************************************************
    This function returns random integer in [0,HQRNDMax]

    L'Ecuyer, Efficient and portable combined random number generators
    *************************************************************************/
    private static int hqrndintegerbase(hqrndstate state,
        xparams _params)
    {
        int result = 0;
        int k = 0;

        ap.assert(state.magicv == hqrndmagic, "HQRNDIntegerBase: State is not correctly initialized!");
        k = state.s1 / 53668;
        state.s1 = 40014 * (state.s1 - k * 53668) - k * 12211;
        if (state.s1 < 0)
        {
            state.s1 = state.s1 + 2147483563;
        }
        k = state.s2 / 52774;
        state.s2 = 40692 * (state.s2 - k * 52774) - k * 3791;
        if (state.s2 < 0)
        {
            state.s2 = state.s2 + 2147483399;
        }

        //
        // Result
        //
        result = state.s1 - state.s2;
        if (result < 1)
        {
            result = result + 2147483562;
        }
        result = result - 1;
        return result;
    }


}
