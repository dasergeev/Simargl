using System;

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


public class ratint
{
    /*************************************************************************
    Barycentric interpolant.
    *************************************************************************/
    public class barycentricinterpolant : apobject
    {
        public int n;
        public double sy;
        public double[] x;
        public double[] y;
        public double[] w;
        public barycentricinterpolant()
        {
            init();
        }
        public override void init()
        {
            x = new double[0];
            y = new double[0];
            w = new double[0];
        }
        public override apobject make_copy()
        {
            barycentricinterpolant _result = new barycentricinterpolant();
            _result.n = n;
            _result.sy = sy;
            _result.x = (double[])x.Clone();
            _result.y = (double[])y.Clone();
            _result.w = (double[])w.Clone();
            return _result;
        }
    };




    /*************************************************************************
    Rational interpolation using barycentric formula

    F(t) = SUM(i=0,n-1,w[i]*f[i]/(t-x[i])) / SUM(i=0,n-1,w[i]/(t-x[i]))

    Input parameters:
        B   -   barycentric interpolant built with one of model building
                subroutines.
        T   -   interpolation point

    Result:
        barycentric interpolant F(t)

      -- ALGLIB --
         Copyright 17.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static double barycentriccalc(barycentricinterpolant b,
        double t,
        xparams _params)
    {
        double result = 0;
        double s1 = 0;
        double s2 = 0;
        double s = 0;
        double v = 0;
        int i = 0;

        ap.assert(!Double.IsInfinity(t), "BarycentricCalc: infinite T!");

        //
        // special case: NaN
        //
        if (Double.IsNaN(t))
        {
            result = Double.NaN;
            return result;
        }

        //
        // special case: N=1
        //
        if (b.n == 1)
        {
            result = b.sy * b.y[0];
            return result;
        }

        //
        // Here we assume that task is normalized, i.e.:
        // 1. abs(Y[i])<=1
        // 2. abs(W[i])<=1
        // 3. X[] is ordered
        //
        s = Math.Abs(t - b.x[0]);
        for (i = 0; i <= b.n - 1; i++)
        {
            v = b.x[i];
            if ((double)(v) == (double)(t))
            {
                result = b.sy * b.y[i];
                return result;
            }
            v = Math.Abs(t - v);
            if ((double)(v) < (double)(s))
            {
                s = v;
            }
        }
        s1 = 0;
        s2 = 0;
        for (i = 0; i <= b.n - 1; i++)
        {
            v = s / (t - b.x[i]);
            v = v * b.w[i];
            s1 = s1 + v * b.y[i];
            s2 = s2 + v;
        }
        result = b.sy * s1 / s2;
        return result;
    }


    /*************************************************************************
    Differentiation of barycentric interpolant: first derivative.

    Algorithm used in this subroutine is very robust and should not fail until
    provided with values too close to MaxRealNumber  (usually  MaxRealNumber/N
    or greater will overflow).

    INPUT PARAMETERS:
        B   -   barycentric interpolant built with one of model building
                subroutines.
        T   -   interpolation point

    OUTPUT PARAMETERS:
        F   -   barycentric interpolant at T
        DF  -   first derivative
        
    NOTE


      -- ALGLIB --
         Copyright 17.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void barycentricdiff1(barycentricinterpolant b,
        double t,
        ref double f,
        ref double df,
        xparams _params)
    {
        double v = 0;
        double vv = 0;
        int i = 0;
        int k = 0;
        double n0 = 0;
        double n1 = 0;
        double d0 = 0;
        double d1 = 0;
        double s0 = 0;
        double s1 = 0;
        double xk = 0;
        double xi = 0;
        double xmin = 0;
        double xmax = 0;
        double xscale1 = 0;
        double xoffs1 = 0;
        double xscale2 = 0;
        double xoffs2 = 0;
        double xprev = 0;

        f = 0;
        df = 0;

        ap.assert(!Double.IsInfinity(t), "BarycentricDiff1: infinite T!");

        //
        // special case: NaN
        //
        if (Double.IsNaN(t))
        {
            f = Double.NaN;
            df = Double.NaN;
            return;
        }

        //
        // special case: N=1
        //
        if (b.n == 1)
        {
            f = b.sy * b.y[0];
            df = 0;
            return;
        }
        if ((double)(b.sy) == (double)(0))
        {
            f = 0;
            df = 0;
            return;
        }
        ap.assert((double)(b.sy) > (double)(0), "BarycentricDiff1: internal error");

        //
        // We assume than N>1 and B.SY>0. Find:
        // 1. pivot point (X[i] closest to T)
        // 2. width of interval containing X[i]
        //
        v = Math.Abs(b.x[0] - t);
        k = 0;
        xmin = b.x[0];
        xmax = b.x[0];
        for (i = 1; i <= b.n - 1; i++)
        {
            vv = b.x[i];
            if ((double)(Math.Abs(vv - t)) < (double)(v))
            {
                v = Math.Abs(vv - t);
                k = i;
            }
            xmin = Math.Min(xmin, vv);
            xmax = Math.Max(xmax, vv);
        }

        //
        // pivot point found, calculate dNumerator and dDenominator
        //
        xscale1 = 1 / (xmax - xmin);
        xoffs1 = -(xmin / (xmax - xmin)) + 1;
        xscale2 = 2;
        xoffs2 = -3;
        t = t * xscale1 + xoffs1;
        t = t * xscale2 + xoffs2;
        xk = b.x[k];
        xk = xk * xscale1 + xoffs1;
        xk = xk * xscale2 + xoffs2;
        v = t - xk;
        n0 = 0;
        n1 = 0;
        d0 = 0;
        d1 = 0;
        xprev = -2;
        for (i = 0; i <= b.n - 1; i++)
        {
            xi = b.x[i];
            xi = xi * xscale1 + xoffs1;
            xi = xi * xscale2 + xoffs2;
            ap.assert((double)(xi) > (double)(xprev), "BarycentricDiff1: points are too close!");
            xprev = xi;
            if (i != k)
            {
                vv = math.sqr(t - xi);
                s0 = (t - xk) / (t - xi);
                s1 = (xk - xi) / vv;
            }
            else
            {
                s0 = 1;
                s1 = 0;
            }
            vv = b.w[i] * b.y[i];
            n0 = n0 + s0 * vv;
            n1 = n1 + s1 * vv;
            vv = b.w[i];
            d0 = d0 + s0 * vv;
            d1 = d1 + s1 * vv;
        }
        f = b.sy * n0 / d0;
        df = (n1 * d0 - n0 * d1) / math.sqr(d0);
        if ((double)(df) != (double)(0))
        {
            df = Math.Sign(df) * Math.Exp(Math.Log(Math.Abs(df)) + Math.Log(b.sy) + Math.Log(xscale1) + Math.Log(xscale2));
        }
    }


    /*************************************************************************
    Differentiation of barycentric interpolant: first/second derivatives.

    INPUT PARAMETERS:
        B   -   barycentric interpolant built with one of model building
                subroutines.
        T   -   interpolation point

    OUTPUT PARAMETERS:
        F   -   barycentric interpolant at T
        DF  -   first derivative
        D2F -   second derivative

    NOTE: this algorithm may fail due to overflow/underflor if  used  on  data
    whose values are close to MaxRealNumber or MinRealNumber.  Use more robust
    BarycentricDiff1() subroutine in such cases.


      -- ALGLIB --
         Copyright 17.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void barycentricdiff2(barycentricinterpolant b,
        double t,
        ref double f,
        ref double df,
        ref double d2f,
        xparams _params)
    {
        double v = 0;
        double vv = 0;
        int i = 0;
        int k = 0;
        double n0 = 0;
        double n1 = 0;
        double n2 = 0;
        double d0 = 0;
        double d1 = 0;
        double d2 = 0;
        double s0 = 0;
        double s1 = 0;
        double s2 = 0;
        double xk = 0;
        double xi = 0;

        f = 0;
        df = 0;
        d2f = 0;

        ap.assert(!Double.IsInfinity(t), "BarycentricDiff1: infinite T!");

        //
        // special case: NaN
        //
        if (Double.IsNaN(t))
        {
            f = Double.NaN;
            df = Double.NaN;
            d2f = Double.NaN;
            return;
        }

        //
        // special case: N=1
        //
        if (b.n == 1)
        {
            f = b.sy * b.y[0];
            df = 0;
            d2f = 0;
            return;
        }
        if ((double)(b.sy) == (double)(0))
        {
            f = 0;
            df = 0;
            d2f = 0;
            return;
        }

        //
        // We assume than N>1 and B.SY>0. Find:
        // 1. pivot point (X[i] closest to T)
        // 2. width of interval containing X[i]
        //
        ap.assert((double)(b.sy) > (double)(0), "BarycentricDiff: internal error");
        f = 0;
        df = 0;
        d2f = 0;
        v = Math.Abs(b.x[0] - t);
        k = 0;
        for (i = 1; i <= b.n - 1; i++)
        {
            vv = b.x[i];
            if ((double)(Math.Abs(vv - t)) < (double)(v))
            {
                v = Math.Abs(vv - t);
                k = i;
            }
        }

        //
        // pivot point found, calculate dNumerator and dDenominator
        //
        xk = b.x[k];
        v = t - xk;
        n0 = 0;
        n1 = 0;
        n2 = 0;
        d0 = 0;
        d1 = 0;
        d2 = 0;
        for (i = 0; i <= b.n - 1; i++)
        {
            if (i != k)
            {
                xi = b.x[i];
                vv = math.sqr(t - xi);
                s0 = (t - xk) / (t - xi);
                s1 = (xk - xi) / vv;
                s2 = -(2 * (xk - xi) / (vv * (t - xi)));
            }
            else
            {
                s0 = 1;
                s1 = 0;
                s2 = 0;
            }
            vv = b.w[i] * b.y[i];
            n0 = n0 + s0 * vv;
            n1 = n1 + s1 * vv;
            n2 = n2 + s2 * vv;
            vv = b.w[i];
            d0 = d0 + s0 * vv;
            d1 = d1 + s1 * vv;
            d2 = d2 + s2 * vv;
        }
        f = b.sy * n0 / d0;
        df = b.sy * (n1 * d0 - n0 * d1) / math.sqr(d0);
        d2f = b.sy * ((n2 * d0 - n0 * d2) * math.sqr(d0) - (n1 * d0 - n0 * d1) * 2 * d0 * d1) / math.sqr(math.sqr(d0));
    }


    /*************************************************************************
    This subroutine performs linear transformation of the argument.

    INPUT PARAMETERS:
        B       -   rational interpolant in barycentric form
        CA, CB  -   transformation coefficients: x = CA*t + CB

    OUTPUT PARAMETERS:
        B       -   transformed interpolant with X replaced by T

      -- ALGLIB PROJECT --
         Copyright 19.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void barycentriclintransx(barycentricinterpolant b,
        double ca,
        double cb,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double v = 0;


        //
        // special case, replace by constant F(CB)
        //
        if ((double)(ca) == (double)(0))
        {
            b.sy = barycentriccalc(b, cb, _params);
            v = 1;
            for (i = 0; i <= b.n - 1; i++)
            {
                b.y[i] = 1;
                b.w[i] = v;
                v = -v;
            }
            return;
        }

        //
        // general case: CA<>0
        //
        for (i = 0; i <= b.n - 1; i++)
        {
            b.x[i] = (b.x[i] - cb) / ca;
        }
        if ((double)(ca) < (double)(0))
        {
            for (i = 0; i <= b.n - 1; i++)
            {
                if (i < b.n - 1 - i)
                {
                    j = b.n - 1 - i;
                    v = b.x[i];
                    b.x[i] = b.x[j];
                    b.x[j] = v;
                    v = b.y[i];
                    b.y[i] = b.y[j];
                    b.y[j] = v;
                    v = b.w[i];
                    b.w[i] = b.w[j];
                    b.w[j] = v;
                }
                else
                {
                    break;
                }
            }
        }
    }


    /*************************************************************************
    This  subroutine   performs   linear  transformation  of  the  barycentric
    interpolant.

    INPUT PARAMETERS:
        B       -   rational interpolant in barycentric form
        CA, CB  -   transformation coefficients: B2(x) = CA*B(x) + CB

    OUTPUT PARAMETERS:
        B       -   transformed interpolant

      -- ALGLIB PROJECT --
         Copyright 19.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void barycentriclintransy(barycentricinterpolant b,
        double ca,
        double cb,
        xparams _params)
    {
        int i = 0;
        double v = 0;
        int i_ = 0;

        for (i = 0; i <= b.n - 1; i++)
        {
            b.y[i] = ca * b.sy * b.y[i] + cb;
        }
        b.sy = 0;
        for (i = 0; i <= b.n - 1; i++)
        {
            b.sy = Math.Max(b.sy, Math.Abs(b.y[i]));
        }
        if ((double)(b.sy) > (double)(0))
        {
            v = 1 / b.sy;
            for (i_ = 0; i_ <= b.n - 1; i_++)
            {
                b.y[i_] = v * b.y[i_];
            }
        }
    }


    /*************************************************************************
    Extracts X/Y/W arrays from rational interpolant

    INPUT PARAMETERS:
        B   -   barycentric interpolant

    OUTPUT PARAMETERS:
        N   -   nodes count, N>0
        X   -   interpolation nodes, array[0..N-1]
        F   -   function values, array[0..N-1]
        W   -   barycentric weights, array[0..N-1]

      -- ALGLIB --
         Copyright 17.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void barycentricunpack(barycentricinterpolant b,
        ref int n,
        ref double[] x,
        ref double[] y,
        ref double[] w,
        xparams _params)
    {
        double v = 0;
        int i_ = 0;

        n = 0;
        x = new double[0];
        y = new double[0];
        w = new double[0];

        n = b.n;
        x = new double[n];
        y = new double[n];
        w = new double[n];
        v = b.sy;
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            x[i_] = b.x[i_];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            y[i_] = v * b.y[i_];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            w[i_] = b.w[i_];
        }
    }


    /*************************************************************************
    Rational interpolant from X/Y/W arrays

    F(t) = SUM(i=0,n-1,w[i]*f[i]/(t-x[i])) / SUM(i=0,n-1,w[i]/(t-x[i]))

    INPUT PARAMETERS:
        X   -   interpolation nodes, array[0..N-1]
        F   -   function values, array[0..N-1]
        W   -   barycentric weights, array[0..N-1]
        N   -   nodes count, N>0

    OUTPUT PARAMETERS:
        B   -   barycentric interpolant built from (X, Y, W)

      -- ALGLIB --
         Copyright 17.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void barycentricbuildxyw(double[] x,
        double[] y,
        double[] w,
        int n,
        barycentricinterpolant b,
        xparams _params)
    {
        int i_ = 0;

        ap.assert(n > 0, "BarycentricBuildXYW: incorrect N!");

        //
        // fill X/Y/W
        //
        b.x = new double[n];
        b.y = new double[n];
        b.w = new double[n];
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            b.x[i_] = x[i_];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            b.y[i_] = y[i_];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            b.w[i_] = w[i_];
        }
        b.n = n;

        //
        // Normalize
        //
        barycentricnormalize(b, _params);
    }


    /*************************************************************************
    Rational interpolant without poles

    The subroutine constructs the rational interpolating function without real
    poles  (see  'Barycentric rational interpolation with no  poles  and  high
    rates of approximation', Michael S. Floater. and  Kai  Hormann,  for  more
    information on this subject).

    Input parameters:
        X   -   interpolation nodes, array[0..N-1].
        Y   -   function values, array[0..N-1].
        N   -   number of nodes, N>0.
        D   -   order of the interpolation scheme, 0 <= D <= N-1.
                D<0 will cause an error.
                D>=N it will be replaced with D=N-1.
                if you don't know what D to choose, use small value about 3-5.

    Output parameters:
        B   -   barycentric interpolant.

    Note:
        this algorithm always succeeds and calculates the weights  with  close
        to machine precision.

      -- ALGLIB PROJECT --
         Copyright 17.06.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void barycentricbuildfloaterhormann(double[] x,
        double[] y,
        int n,
        int d,
        barycentricinterpolant b,
        xparams _params)
    {
        double s0 = 0;
        double s = 0;
        double v = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        int[] perm = new int[0];
        double[] wtemp = new double[0];
        double[] sortrbuf = new double[0];
        double[] sortrbuf2 = new double[0];
        int i_ = 0;

        ap.assert(n > 0, "BarycentricFloaterHormann: N<=0!");
        ap.assert(d >= 0, "BarycentricFloaterHormann: incorrect D!");

        //
        // Prepare
        //
        if (d > n - 1)
        {
            d = n - 1;
        }
        b.n = n;

        //
        // special case: N=1
        //
        if (n == 1)
        {
            b.x = new double[n];
            b.y = new double[n];
            b.w = new double[n];
            b.x[0] = x[0];
            b.y[0] = y[0];
            b.w[0] = 1;
            barycentricnormalize(b, _params);
            return;
        }

        //
        // Fill X/Y
        //
        b.x = new double[n];
        b.y = new double[n];
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            b.x[i_] = x[i_];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            b.y[i_] = y[i_];
        }
        tsort.tagsortfastr(ref b.x, ref b.y, ref sortrbuf, ref sortrbuf2, n, _params);

        //
        // Calculate Wk
        //
        b.w = new double[n];
        s0 = 1;
        for (k = 1; k <= d; k++)
        {
            s0 = -s0;
        }
        for (k = 0; k <= n - 1; k++)
        {

            //
            // Wk
            //
            s = 0;
            for (i = Math.Max(k - d, 0); i <= Math.Min(k, n - 1 - d); i++)
            {
                v = 1;
                for (j = i; j <= i + d; j++)
                {
                    if (j != k)
                    {
                        v = v / Math.Abs(b.x[k] - b.x[j]);
                    }
                }
                s = s + v;
            }
            b.w[k] = s0 * s;

            //
            // Next S0
            //
            s0 = -s0;
        }

        //
        // Normalize
        //
        barycentricnormalize(b, _params);
    }


    /*************************************************************************
    Copying of the barycentric interpolant (for internal use only)

    INPUT PARAMETERS:
        B   -   barycentric interpolant

    OUTPUT PARAMETERS:
        B2  -   copy(B1)

      -- ALGLIB --
         Copyright 17.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void barycentriccopy(barycentricinterpolant b,
        barycentricinterpolant b2,
        xparams _params)
    {
        int i_ = 0;

        b2.n = b.n;
        b2.sy = b.sy;
        b2.x = new double[b2.n];
        b2.y = new double[b2.n];
        b2.w = new double[b2.n];
        for (i_ = 0; i_ <= b2.n - 1; i_++)
        {
            b2.x[i_] = b.x[i_];
        }
        for (i_ = 0; i_ <= b2.n - 1; i_++)
        {
            b2.y[i_] = b.y[i_];
        }
        for (i_ = 0; i_ <= b2.n - 1; i_++)
        {
            b2.w[i_] = b.w[i_];
        }
    }


    /*************************************************************************
    Normalization of barycentric interpolant:
    * B.N, B.X, B.Y and B.W are initialized
    * B.SY is NOT initialized
    * Y[] is normalized, scaling coefficient is stored in B.SY
    * W[] is normalized, no scaling coefficient is stored
    * X[] is sorted

    Internal subroutine.
    *************************************************************************/
    private static void barycentricnormalize(barycentricinterpolant b,
        xparams _params)
    {
        int[] p1 = new int[0];
        int[] p2 = new int[0];
        int i = 0;
        int j = 0;
        int j2 = 0;
        double v = 0;
        int i_ = 0;


        //
        // Normalize task: |Y|<=1, |W|<=1, sort X[]
        //
        b.sy = 0;
        for (i = 0; i <= b.n - 1; i++)
        {
            b.sy = Math.Max(b.sy, Math.Abs(b.y[i]));
        }
        if ((double)(b.sy) > (double)(0) && (double)(Math.Abs(b.sy - 1)) > (double)(10 * math.machineepsilon))
        {
            v = 1 / b.sy;
            for (i_ = 0; i_ <= b.n - 1; i_++)
            {
                b.y[i_] = v * b.y[i_];
            }
        }
        v = 0;
        for (i = 0; i <= b.n - 1; i++)
        {
            v = Math.Max(v, Math.Abs(b.w[i]));
        }
        if ((double)(v) > (double)(0) && (double)(Math.Abs(v - 1)) > (double)(10 * math.machineepsilon))
        {
            v = 1 / v;
            for (i_ = 0; i_ <= b.n - 1; i_++)
            {
                b.w[i_] = v * b.w[i_];
            }
        }
        for (i = 0; i <= b.n - 2; i++)
        {
            if ((double)(b.x[i + 1]) < (double)(b.x[i]))
            {
                tsort.tagsort(ref b.x, b.n, ref p1, ref p2, _params);
                for (j = 0; j <= b.n - 1; j++)
                {
                    j2 = p2[j];
                    v = b.y[j];
                    b.y[j] = b.y[j2];
                    b.y[j2] = v;
                    v = b.w[j];
                    b.w[j] = b.w[j2];
                    b.w[j2] = v;
                }
                break;
            }
        }
    }


}
