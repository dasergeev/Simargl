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

public class polint
{
    /*************************************************************************
    Conversion from barycentric representation to Chebyshev basis.
    This function has O(N^2) complexity.

    INPUT PARAMETERS:
        P   -   polynomial in barycentric form
        A,B -   base interval for Chebyshev polynomials (see below)
                A<>B

    OUTPUT PARAMETERS
        T   -   coefficients of Chebyshev representation;
                P(x) = sum { T[i]*Ti(2*(x-A)/(B-A)-1), i=0..N-1 },
                where Ti - I-th Chebyshev polynomial.

    NOTES:
        barycentric interpolant passed as P may be either polynomial  obtained
        from  polynomial  interpolation/ fitting or rational function which is
        NOT polynomial. We can't distinguish between these two cases, and this
        algorithm just tries to work assuming that P IS a polynomial.  If not,
        algorithm will return results, but they won't have any meaning.

      -- ALGLIB --
         Copyright 30.09.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void polynomialbar2cheb(ratint.barycentricinterpolant p,
        double a,
        double b,
        ref double[] t,
        xparams _params)
    {
        int i = 0;
        int k = 0;
        double[] vp = new double[0];
        double[] vx = new double[0];
        double[] tk = new double[0];
        double[] tk1 = new double[0];
        double v = 0;
        int i_ = 0;

        t = new double[0];

        ap.assert(math.isfinite(a), "PolynomialBar2Cheb: A is not finite!");
        ap.assert(math.isfinite(b), "PolynomialBar2Cheb: B is not finite!");
        ap.assert((double)(a) != (double)(b), "PolynomialBar2Cheb: A=B!");
        ap.assert(p.n > 0, "PolynomialBar2Cheb: P is not correctly initialized barycentric interpolant!");

        //
        // Calculate function values on a Chebyshev grid
        //
        vp = new double[p.n];
        vx = new double[p.n];
        for (i = 0; i <= p.n - 1; i++)
        {
            vx[i] = Math.Cos(Math.PI * (i + 0.5) / p.n);
            vp[i] = ratint.barycentriccalc(p, 0.5 * (vx[i] + 1) * (b - a) + a, _params);
        }

        //
        // T[0]
        //
        t = new double[p.n];
        v = 0;
        for (i = 0; i <= p.n - 1; i++)
        {
            v = v + vp[i];
        }
        t[0] = v / p.n;

        //
        // other T's.
        //
        // NOTES:
        // 1. TK stores T{k} on VX, TK1 stores T{k-1} on VX
        // 2. we can do same calculations with fast DCT, but it
        //    * adds dependencies
        //    * still leaves us with O(N^2) algorithm because
        //      preparation of function values is O(N^2) process
        //
        if (p.n > 1)
        {
            tk = new double[p.n];
            tk1 = new double[p.n];
            for (i = 0; i <= p.n - 1; i++)
            {
                tk[i] = vx[i];
                tk1[i] = 1;
            }
            for (k = 1; k <= p.n - 1; k++)
            {

                //
                // calculate discrete product of function vector and TK
                //
                v = 0.0;
                for (i_ = 0; i_ <= p.n - 1; i_++)
                {
                    v += tk[i_] * vp[i_];
                }
                t[k] = v / (0.5 * p.n);

                //
                // Update TK and TK1
                //
                for (i = 0; i <= p.n - 1; i++)
                {
                    v = 2 * vx[i] * tk[i] - tk1[i];
                    tk1[i] = tk[i];
                    tk[i] = v;
                }
            }
        }
    }


    /*************************************************************************
    Conversion from Chebyshev basis to barycentric representation.
    This function has O(N^2) complexity.

    INPUT PARAMETERS:
        T   -   coefficients of Chebyshev representation;
                P(x) = sum { T[i]*Ti(2*(x-A)/(B-A)-1), i=0..N },
                where Ti - I-th Chebyshev polynomial.
        N   -   number of coefficients:
                * if given, only leading N elements of T are used
                * if not given, automatically determined from size of T
        A,B -   base interval for Chebyshev polynomials (see above)
                A<B

    OUTPUT PARAMETERS
        P   -   polynomial in barycentric form

      -- ALGLIB --
         Copyright 30.09.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void polynomialcheb2bar(double[] t,
        int n,
        double a,
        double b,
        ratint.barycentricinterpolant p,
        xparams _params)
    {
        int i = 0;
        int k = 0;
        double[] y = new double[0];
        double tk = 0;
        double tk1 = 0;
        double vx = 0;
        double vy = 0;
        double v = 0;

        ap.assert(math.isfinite(a), "PolynomialBar2Cheb: A is not finite!");
        ap.assert(math.isfinite(b), "PolynomialBar2Cheb: B is not finite!");
        ap.assert((double)(a) != (double)(b), "PolynomialBar2Cheb: A=B!");
        ap.assert(n >= 1, "PolynomialBar2Cheb: N<1");
        ap.assert(ap.len(t) >= n, "PolynomialBar2Cheb: Length(T)<N");
        ap.assert(apserv.isfinitevector(t, n, _params), "PolynomialBar2Cheb: T[] contains INF or NAN");

        //
        // Calculate function values on a Chebyshev grid spanning [-1,+1]
        //
        y = new double[n];
        for (i = 0; i <= n - 1; i++)
        {

            //
            // Calculate value on a grid spanning [-1,+1]
            //
            vx = Math.Cos(Math.PI * (i + 0.5) / n);
            vy = t[0];
            tk1 = 1;
            tk = vx;
            for (k = 1; k <= n - 1; k++)
            {
                vy = vy + t[k] * tk;
                v = 2 * vx * tk - tk1;
                tk1 = tk;
                tk = v;
            }
            y[i] = vy;
        }

        //
        // Build barycentric interpolant, map grid from [-1,+1] to [A,B]
        //
        polynomialbuildcheb1(a, b, y, n, p, _params);
    }


    /*************************************************************************
    Conversion from barycentric representation to power basis.
    This function has O(N^2) complexity.

    INPUT PARAMETERS:
        P   -   polynomial in barycentric form
        C   -   offset (see below); 0.0 is used as default value.
        S   -   scale (see below);  1.0 is used as default value. S<>0.

    OUTPUT PARAMETERS
        A   -   coefficients, P(x) = sum { A[i]*((X-C)/S)^i, i=0..N-1 }
        N   -   number of coefficients (polynomial degree plus 1)

    NOTES:
    1.  this function accepts offset and scale, which can be  set  to  improve
        numerical properties of polynomial. For example, if P was obtained  as
        result of interpolation on [-1,+1],  you  can  set  C=0  and  S=1  and
        represent  P  as sum of 1, x, x^2, x^3 and so on. In most cases you it
        is exactly what you need.

        However, if your interpolation model was built on [999,1001], you will
        see significant growth of numerical errors when using {1, x, x^2, x^3}
        as basis. Representing P as sum of 1, (x-1000), (x-1000)^2, (x-1000)^3
        will be better option. Such representation can be  obtained  by  using
        1000.0 as offset C and 1.0 as scale S.

    2.  power basis is ill-conditioned and tricks described above can't  solve
        this problem completely. This function  will  return  coefficients  in
        any  case,  but  for  N>8  they  will  become unreliable. However, N's
        less than 5 are pretty safe.
        
    3.  barycentric interpolant passed as P may be either polynomial  obtained
        from  polynomial  interpolation/ fitting or rational function which is
        NOT polynomial. We can't distinguish between these two cases, and this
        algorithm just tries to work assuming that P IS a polynomial.  If not,
        algorithm will return results, but they won't have any meaning.

      -- ALGLIB --
         Copyright 30.09.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void polynomialbar2pow(ratint.barycentricinterpolant p,
        double c,
        double s,
        ref double[] a,
        xparams _params)
    {
        int i = 0;
        int k = 0;
        double e = 0;
        double d = 0;
        double[] vp = new double[0];
        double[] vx = new double[0];
        double[] tk = new double[0];
        double[] tk1 = new double[0];
        double[] t = new double[0];
        double v = 0;
        double c0 = 0;
        double s0 = 0;
        double va = 0;
        double vb = 0;
        double[] vai = new double[0];
        double[] vbi = new double[0];
        double minx = 0;
        double maxx = 0;
        int i_ = 0;

        a = new double[0];


        //
        // We have barycentric model built using set of points X[], and we
        // want to convert it to power basis centered about point  C  with
        // scale S: I-th basis function is ((X-C)/S)^i.
        //
        // We use following three-stage algorithm:
        //
        // 1. we build Chebyshev representation of polynomial using
        //    intermediate center C0 and scale S0, which are derived from X[]:
        //    C0 = 0.5*(min(X)+max(X)), S0 = 0.5*(max(X)-min(X)). Chebyshev
        //    representation is built by sampling points around center C0,
        //    with typical distance between them proportional to S0.
        // 2. then we transform form Chebyshev basis to intermediate power
        //    basis, using same center/scale C0/S0.
        // 3. after that, we apply linear transformation to intermediate
        //    power basis which moves it to final center/scale C/S.
        //
        // The idea of such multi-stage algorithm is that it is much easier to
        // transform barycentric model to Chebyshev basis, and only later to
        // power basis, than transforming it directly to power basis. It is
        // also more numerically stable to sample points using intermediate C0/S0,
        // which are derived from user-supplied model, than using "final" C/S,
        // which may be unsuitable for sampling (say, if S=1, we may have stability
        // problems when working with models built from dataset with non-unit
        // scale of abscissas).
        //
        ap.assert(math.isfinite(c), "PolynomialBar2Pow: C is not finite!");
        ap.assert(math.isfinite(s), "PolynomialBar2Pow: S is not finite!");
        ap.assert((double)(s) != (double)(0), "PolynomialBar2Pow: S=0!");
        ap.assert(p.n > 0, "PolynomialBar2Pow: P is not correctly initialized barycentric interpolant!");

        //
        // Select intermediate center/scale
        //
        minx = p.x[0];
        maxx = p.x[0];
        for (i = 1; i <= p.n - 1; i++)
        {
            minx = Math.Min(minx, p.x[i]);
            maxx = Math.Max(maxx, p.x[i]);
        }
        if ((double)(minx) == (double)(maxx))
        {
            c0 = minx;
            s0 = 1.0;
        }
        else
        {
            c0 = 0.5 * (maxx + minx);
            s0 = 0.5 * (maxx - minx);
        }

        //
        // Calculate function values on a Chebyshev grid using intermediate C0/S0
        //
        vp = new double[p.n + 1];
        vx = new double[p.n];
        for (i = 0; i <= p.n - 1; i++)
        {
            vx[i] = Math.Cos(Math.PI * (i + 0.5) / p.n);
            vp[i] = ratint.barycentriccalc(p, s0 * vx[i] + c0, _params);
        }

        //
        // T[0]
        //
        t = new double[p.n];
        v = 0;
        for (i = 0; i <= p.n - 1; i++)
        {
            v = v + vp[i];
        }
        t[0] = v / p.n;

        //
        // other T's.
        //
        // NOTES:
        // 1. TK stores T{k} on VX, TK1 stores T{k-1} on VX
        // 2. we can do same calculations with fast DCT, but it
        //    * adds dependencies
        //    * still leaves us with O(N^2) algorithm because
        //      preparation of function values is O(N^2) process
        //
        if (p.n > 1)
        {
            tk = new double[p.n];
            tk1 = new double[p.n];
            for (i = 0; i <= p.n - 1; i++)
            {
                tk[i] = vx[i];
                tk1[i] = 1;
            }
            for (k = 1; k <= p.n - 1; k++)
            {

                //
                // calculate discrete product of function vector and TK
                //
                v = 0.0;
                for (i_ = 0; i_ <= p.n - 1; i_++)
                {
                    v += tk[i_] * vp[i_];
                }
                t[k] = v / (0.5 * p.n);

                //
                // Update TK and TK1
                //
                for (i = 0; i <= p.n - 1; i++)
                {
                    v = 2 * vx[i] * tk[i] - tk1[i];
                    tk1[i] = tk[i];
                    tk[i] = v;
                }
            }
        }

        //
        // Convert from Chebyshev basis to power basis
        //
        a = new double[p.n];
        for (i = 0; i <= p.n - 1; i++)
        {
            a[i] = 0;
        }
        d = 0;
        for (i = 0; i <= p.n - 1; i++)
        {
            for (k = i; k <= p.n - 1; k++)
            {
                e = a[k];
                a[k] = 0;
                if (i <= 1 && k == i)
                {
                    a[k] = 1;
                }
                else
                {
                    if (i != 0)
                    {
                        a[k] = 2 * d;
                    }
                    if (k > i + 1)
                    {
                        a[k] = a[k] - a[k - 2];
                    }
                }
                d = e;
            }
            d = a[i];
            e = 0;
            k = i;
            while (k <= p.n - 1)
            {
                e = e + a[k] * t[k];
                k = k + 2;
            }
            a[i] = e;
        }

        //
        // Apply linear transformation which converts basis from intermediate
        // one Fi=((x-C0)/S0)^i to final one Fi=((x-C)/S)^i.
        //
        // We have y=(x-C0)/S0, z=(x-C)/S, and coefficients A[] for basis Fi(y).
        // Because we have y=A*z+B, for A=s/s0 and B=c/s0-c0/s0, we can perform
        // substitution and get coefficients A_new[] in basis Fi(z).
        //
        ap.assert(ap.len(vp) >= p.n + 1, "PolynomialBar2Pow: internal error");
        ap.assert(ap.len(t) >= p.n, "PolynomialBar2Pow: internal error");
        for (i = 0; i <= p.n - 1; i++)
        {
            t[i] = 0.0;
        }
        va = s / s0;
        vb = c / s0 - c0 / s0;
        vai = new double[p.n];
        vbi = new double[p.n];
        vai[0] = 1;
        vbi[0] = 1;
        for (k = 1; k <= p.n - 1; k++)
        {
            vai[k] = vai[k - 1] * va;
            vbi[k] = vbi[k - 1] * vb;
        }
        for (k = 0; k <= p.n - 1; k++)
        {

            //
            // Generate set of binomial coefficients in VP[]
            //
            if (k > 0)
            {
                vp[k] = 1;
                for (i = k - 1; i >= 1; i--)
                {
                    vp[i] = vp[i] + vp[i - 1];
                }
                vp[0] = 1;
            }
            else
            {
                vp[0] = 1;
            }

            //
            // Update T[] with expansion of K-th basis function
            //
            for (i = 0; i <= k; i++)
            {
                t[i] = t[i] + a[k] * vai[i] * vbi[k - i] * vp[i];
            }
        }
        for (k = 0; k <= p.n - 1; k++)
        {
            a[k] = t[k];
        }
    }


    /*************************************************************************
    Conversion from power basis to barycentric representation.
    This function has O(N^2) complexity.

    INPUT PARAMETERS:
        A   -   coefficients, P(x) = sum { A[i]*((X-C)/S)^i, i=0..N-1 }
        N   -   number of coefficients (polynomial degree plus 1)
                * if given, only leading N elements of A are used
                * if not given, automatically determined from size of A
        C   -   offset (see below); 0.0 is used as default value.
        S   -   scale (see below);  1.0 is used as default value. S<>0.

    OUTPUT PARAMETERS
        P   -   polynomial in barycentric form


    NOTES:
    1.  this function accepts offset and scale, which can be  set  to  improve
        numerical properties of polynomial. For example, if you interpolate on
        [-1,+1],  you  can  set C=0 and S=1 and convert from sum of 1, x, x^2,
        x^3 and so on. In most cases you it is exactly what you need.

        However, if your interpolation model was built on [999,1001], you will
        see significant growth of numerical errors when using {1, x, x^2, x^3}
        as  input  basis.  Converting  from  sum  of  1, (x-1000), (x-1000)^2,
        (x-1000)^3 will be better option (you have to specify 1000.0 as offset
        C and 1.0 as scale S).

    2.  power basis is ill-conditioned and tricks described above can't  solve
        this problem completely. This function  will  return barycentric model
        in any case, but for N>8 accuracy well degrade. However, N's less than
        5 are pretty safe.

      -- ALGLIB --
         Copyright 30.09.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void polynomialpow2bar(double[] a,
        int n,
        double c,
        double s,
        ratint.barycentricinterpolant p,
        xparams _params)
    {
        int i = 0;
        int k = 0;
        double[] y = new double[0];
        double vx = 0;
        double vy = 0;
        double px = 0;

        ap.assert(math.isfinite(c), "PolynomialPow2Bar: C is not finite!");
        ap.assert(math.isfinite(s), "PolynomialPow2Bar: S is not finite!");
        ap.assert((double)(s) != (double)(0), "PolynomialPow2Bar: S is zero!");
        ap.assert(n >= 1, "PolynomialPow2Bar: N<1");
        ap.assert(ap.len(a) >= n, "PolynomialPow2Bar: Length(A)<N");
        ap.assert(apserv.isfinitevector(a, n, _params), "PolynomialPow2Bar: A[] contains INF or NAN");

        //
        // Calculate function values on a Chebyshev grid spanning [-1,+1]
        //
        y = new double[n];
        for (i = 0; i <= n - 1; i++)
        {

            //
            // Calculate value on a grid spanning [-1,+1]
            //
            vx = Math.Cos(Math.PI * (i + 0.5) / n);
            vy = a[0];
            px = vx;
            for (k = 1; k <= n - 1; k++)
            {
                vy = vy + px * a[k];
                px = px * vx;
            }
            y[i] = vy;
        }

        //
        // Build barycentric interpolant, map grid from [-1,+1] to [A,B]
        //
        polynomialbuildcheb1(c - s, c + s, y, n, p, _params);
    }


    /*************************************************************************
    Lagrange intepolant: generation of the model on the general grid.
    This function has O(N^2) complexity.

    INPUT PARAMETERS:
        X   -   abscissas, array[0..N-1]
        Y   -   function values, array[0..N-1]
        N   -   number of points, N>=1

    OUTPUT PARAMETERS
        P   -   barycentric model which represents Lagrange interpolant
                (see ratint unit info and BarycentricCalc() description for
                more information).

      -- ALGLIB --
         Copyright 02.12.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void polynomialbuild(double[] x,
        double[] y,
        int n,
        ratint.barycentricinterpolant p,
        xparams _params)
    {
        int j = 0;
        int k = 0;
        double[] w = new double[0];
        double b = 0;
        double a = 0;
        double v = 0;
        double mx = 0;
        double[] sortrbuf = new double[0];
        double[] sortrbuf2 = new double[0];
        int i_ = 0;

        x = (double[])x.Clone();
        y = (double[])y.Clone();

        ap.assert(n > 0, "PolynomialBuild: N<=0!");
        ap.assert(ap.len(x) >= n, "PolynomialBuild: Length(X)<N!");
        ap.assert(ap.len(y) >= n, "PolynomialBuild: Length(Y)<N!");
        ap.assert(apserv.isfinitevector(x, n, _params), "PolynomialBuild: X contains infinite or NaN values!");
        ap.assert(apserv.isfinitevector(y, n, _params), "PolynomialBuild: Y contains infinite or NaN values!");
        tsort.tagsortfastr(ref x, ref y, ref sortrbuf, ref sortrbuf2, n, _params);
        ap.assert(apserv.aredistinct(x, n, _params), "PolynomialBuild: at least two consequent points are too close!");

        //
        // calculate W[j]
        // multi-pass algorithm is used to avoid overflow
        //
        w = new double[n];
        a = x[0];
        b = x[0];
        for (j = 0; j <= n - 1; j++)
        {
            w[j] = 1;
            a = Math.Min(a, x[j]);
            b = Math.Max(b, x[j]);
        }
        for (k = 0; k <= n - 1; k++)
        {

            //
            // W[K] is used instead of 0.0 because
            // cycle on J does not touch K-th element
            // and we MUST get maximum from ALL elements
            //
            mx = Math.Abs(w[k]);
            for (j = 0; j <= n - 1; j++)
            {
                if (j != k)
                {
                    v = (b - a) / (x[j] - x[k]);
                    w[j] = w[j] * v;
                    mx = Math.Max(mx, Math.Abs(w[j]));
                }
            }
            if (k % 5 == 0)
            {

                //
                // every 5-th run we renormalize W[]
                //
                v = 1 / mx;
                for (i_ = 0; i_ <= n - 1; i_++)
                {
                    w[i_] = v * w[i_];
                }
            }
        }
        ratint.barycentricbuildxyw(x, y, w, n, p, _params);
    }


    /*************************************************************************
    Lagrange intepolant: generation of the model on equidistant grid.
    This function has O(N) complexity.

    INPUT PARAMETERS:
        A   -   left boundary of [A,B]
        B   -   right boundary of [A,B]
        Y   -   function values at the nodes, array[0..N-1]
        N   -   number of points, N>=1
                for N=1 a constant model is constructed.

    OUTPUT PARAMETERS
        P   -   barycentric model which represents Lagrange interpolant
                (see ratint unit info and BarycentricCalc() description for
                more information).

      -- ALGLIB --
         Copyright 03.12.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void polynomialbuildeqdist(double a,
        double b,
        double[] y,
        int n,
        ratint.barycentricinterpolant p,
        xparams _params)
    {
        int i = 0;
        double[] w = new double[0];
        double[] x = new double[0];
        double v = 0;

        ap.assert(n > 0, "PolynomialBuildEqDist: N<=0!");
        ap.assert(ap.len(y) >= n, "PolynomialBuildEqDist: Length(Y)<N!");
        ap.assert(math.isfinite(a), "PolynomialBuildEqDist: A is infinite or NaN!");
        ap.assert(math.isfinite(b), "PolynomialBuildEqDist: B is infinite or NaN!");
        ap.assert(apserv.isfinitevector(y, n, _params), "PolynomialBuildEqDist: Y contains infinite or NaN values!");
        ap.assert((double)(b) != (double)(a), "PolynomialBuildEqDist: B=A!");
        ap.assert((double)(a + (b - a) / n) != (double)(a), "PolynomialBuildEqDist: B is too close to A!");

        //
        // Special case: N=1
        //
        if (n == 1)
        {
            x = new double[1];
            w = new double[1];
            x[0] = 0.5 * (b + a);
            w[0] = 1;
            ratint.barycentricbuildxyw(x, y, w, 1, p, _params);
            return;
        }

        //
        // general case
        //
        x = new double[n];
        w = new double[n];
        v = 1;
        for (i = 0; i <= n - 1; i++)
        {
            w[i] = v;
            x[i] = a + (b - a) * i / (n - 1);
            v = -(v * (n - 1 - i));
            v = v / (i + 1);
        }
        ratint.barycentricbuildxyw(x, y, w, n, p, _params);
    }


    /*************************************************************************
    Lagrange intepolant on Chebyshev grid (first kind).
    This function has O(N) complexity.

    INPUT PARAMETERS:
        A   -   left boundary of [A,B]
        B   -   right boundary of [A,B]
        Y   -   function values at the nodes, array[0..N-1],
                Y[I] = Y(0.5*(B+A) + 0.5*(B-A)*Cos(PI*(2*i+1)/(2*n)))
        N   -   number of points, N>=1
                for N=1 a constant model is constructed.

    OUTPUT PARAMETERS
        P   -   barycentric model which represents Lagrange interpolant
                (see ratint unit info and BarycentricCalc() description for
                more information).

      -- ALGLIB --
         Copyright 03.12.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void polynomialbuildcheb1(double a,
        double b,
        double[] y,
        int n,
        ratint.barycentricinterpolant p,
        xparams _params)
    {
        int i = 0;
        double[] w = new double[0];
        double[] x = new double[0];
        double v = 0;
        double t = 0;

        ap.assert(n > 0, "PolynomialBuildCheb1: N<=0!");
        ap.assert(ap.len(y) >= n, "PolynomialBuildCheb1: Length(Y)<N!");
        ap.assert(math.isfinite(a), "PolynomialBuildCheb1: A is infinite or NaN!");
        ap.assert(math.isfinite(b), "PolynomialBuildCheb1: B is infinite or NaN!");
        ap.assert(apserv.isfinitevector(y, n, _params), "PolynomialBuildCheb1: Y contains infinite or NaN values!");
        ap.assert((double)(b) != (double)(a), "PolynomialBuildCheb1: B=A!");

        //
        // Special case: N=1
        //
        if (n == 1)
        {
            x = new double[1];
            w = new double[1];
            x[0] = 0.5 * (b + a);
            w[0] = 1;
            ratint.barycentricbuildxyw(x, y, w, 1, p, _params);
            return;
        }

        //
        // general case
        //
        x = new double[n];
        w = new double[n];
        v = 1;
        for (i = 0; i <= n - 1; i++)
        {
            t = Math.Tan(0.5 * Math.PI * (2 * i + 1) / (2 * n));
            w[i] = 2 * v * t / (1 + math.sqr(t));
            x[i] = 0.5 * (b + a) + 0.5 * (b - a) * (1 - math.sqr(t)) / (1 + math.sqr(t));
            v = -v;
        }
        ratint.barycentricbuildxyw(x, y, w, n, p, _params);
    }


    /*************************************************************************
    Lagrange intepolant on Chebyshev grid (second kind).
    This function has O(N) complexity.

    INPUT PARAMETERS:
        A   -   left boundary of [A,B]
        B   -   right boundary of [A,B]
        Y   -   function values at the nodes, array[0..N-1],
                Y[I] = Y(0.5*(B+A) + 0.5*(B-A)*Cos(PI*i/(n-1)))
        N   -   number of points, N>=1
                for N=1 a constant model is constructed.

    OUTPUT PARAMETERS
        P   -   barycentric model which represents Lagrange interpolant
                (see ratint unit info and BarycentricCalc() description for
                more information).

      -- ALGLIB --
         Copyright 03.12.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void polynomialbuildcheb2(double a,
        double b,
        double[] y,
        int n,
        ratint.barycentricinterpolant p,
        xparams _params)
    {
        int i = 0;
        double[] w = new double[0];
        double[] x = new double[0];
        double v = 0;

        ap.assert(n > 0, "PolynomialBuildCheb2: N<=0!");
        ap.assert(ap.len(y) >= n, "PolynomialBuildCheb2: Length(Y)<N!");
        ap.assert(math.isfinite(a), "PolynomialBuildCheb2: A is infinite or NaN!");
        ap.assert(math.isfinite(b), "PolynomialBuildCheb2: B is infinite or NaN!");
        ap.assert((double)(b) != (double)(a), "PolynomialBuildCheb2: B=A!");
        ap.assert(apserv.isfinitevector(y, n, _params), "PolynomialBuildCheb2: Y contains infinite or NaN values!");

        //
        // Special case: N=1
        //
        if (n == 1)
        {
            x = new double[1];
            w = new double[1];
            x[0] = 0.5 * (b + a);
            w[0] = 1;
            ratint.barycentricbuildxyw(x, y, w, 1, p, _params);
            return;
        }

        //
        // general case
        //
        x = new double[n];
        w = new double[n];
        v = 1;
        for (i = 0; i <= n - 1; i++)
        {
            if (i == 0 || i == n - 1)
            {
                w[i] = v * 0.5;
            }
            else
            {
                w[i] = v;
            }
            x[i] = 0.5 * (b + a) + 0.5 * (b - a) * Math.Cos(Math.PI * i / (n - 1));
            v = -v;
        }
        ratint.barycentricbuildxyw(x, y, w, n, p, _params);
    }


    /*************************************************************************
    Fast equidistant polynomial interpolation function with O(N) complexity

    INPUT PARAMETERS:
        A   -   left boundary of [A,B]
        B   -   right boundary of [A,B]
        F   -   function values, array[0..N-1]
        N   -   number of points on equidistant grid, N>=1
                for N=1 a constant model is constructed.
        T   -   position where P(x) is calculated

    RESULT
        value of the Lagrange interpolant at T
        
    IMPORTANT
        this function provides fast interface which is not overflow-safe
        nor it is very precise.
        the best option is to use  PolynomialBuildEqDist()/BarycentricCalc()
        subroutines unless you are pretty sure that your data will not result
        in overflow.

      -- ALGLIB --
         Copyright 02.12.2009 by Bochkanov Sergey
    *************************************************************************/
    public static double polynomialcalceqdist(double a,
        double b,
        double[] f,
        int n,
        double t,
        xparams _params)
    {
        double result = 0;
        double s1 = 0;
        double s2 = 0;
        double v = 0;
        double threshold = 0;
        double s = 0;
        double h = 0;
        int i = 0;
        int j = 0;
        double w = 0;
        double x = 0;

        ap.assert(n > 0, "PolynomialCalcEqDist: N<=0!");
        ap.assert(ap.len(f) >= n, "PolynomialCalcEqDist: Length(F)<N!");
        ap.assert(math.isfinite(a), "PolynomialCalcEqDist: A is infinite or NaN!");
        ap.assert(math.isfinite(b), "PolynomialCalcEqDist: B is infinite or NaN!");
        ap.assert(apserv.isfinitevector(f, n, _params), "PolynomialCalcEqDist: F contains infinite or NaN values!");
        ap.assert((double)(b) != (double)(a), "PolynomialCalcEqDist: B=A!");
        ap.assert(!Double.IsInfinity(t), "PolynomialCalcEqDist: T is infinite!");

        //
        // Special case: T is NAN
        //
        if (Double.IsNaN(t))
        {
            result = Double.NaN;
            return result;
        }

        //
        // Special case: N=1
        //
        if (n == 1)
        {
            result = f[0];
            return result;
        }

        //
        // First, decide: should we use "safe" formula (guarded
        // against overflow) or fast one?
        //
        threshold = Math.Sqrt(math.minrealnumber);
        j = 0;
        s = t - a;
        for (i = 1; i <= n - 1; i++)
        {
            x = a + (double)i / (double)(n - 1) * (b - a);
            if ((double)(Math.Abs(t - x)) < (double)(Math.Abs(s)))
            {
                s = t - x;
                j = i;
            }
        }
        if ((double)(s) == (double)(0))
        {
            result = f[j];
            return result;
        }
        if ((double)(Math.Abs(s)) > (double)(threshold))
        {

            //
            // use fast formula
            //
            j = -1;
            s = 1.0;
        }

        //
        // Calculate using safe or fast barycentric formula
        //
        s1 = 0;
        s2 = 0;
        w = 1.0;
        h = (b - a) / (n - 1);
        for (i = 0; i <= n - 1; i++)
        {
            if (i != j)
            {
                v = s * w / (t - (a + i * h));
                s1 = s1 + v * f[i];
                s2 = s2 + v;
            }
            else
            {
                v = w;
                s1 = s1 + v * f[i];
                s2 = s2 + v;
            }
            w = -(w * (n - 1 - i));
            w = w / (i + 1);
        }
        result = s1 / s2;
        return result;
    }


    /*************************************************************************
    Fast polynomial interpolation function on Chebyshev points (first kind)
    with O(N) complexity.

    INPUT PARAMETERS:
        A   -   left boundary of [A,B]
        B   -   right boundary of [A,B]
        F   -   function values, array[0..N-1]
        N   -   number of points on Chebyshev grid (first kind),
                X[i] = 0.5*(B+A) + 0.5*(B-A)*Cos(PI*(2*i+1)/(2*n))
                for N=1 a constant model is constructed.
        T   -   position where P(x) is calculated

    RESULT
        value of the Lagrange interpolant at T

    IMPORTANT
        this function provides fast interface which is not overflow-safe
        nor it is very precise.
        the best option is to use  PolIntBuildCheb1()/BarycentricCalc()
        subroutines unless you are pretty sure that your data will not result
        in overflow.

      -- ALGLIB --
         Copyright 02.12.2009 by Bochkanov Sergey
    *************************************************************************/
    public static double polynomialcalccheb1(double a,
        double b,
        double[] f,
        int n,
        double t,
        xparams _params)
    {
        double result = 0;
        double s1 = 0;
        double s2 = 0;
        double v = 0;
        double threshold = 0;
        double s = 0;
        int i = 0;
        int j = 0;
        double a0 = 0;
        double delta = 0;
        double alpha = 0;
        double beta = 0;
        double ca = 0;
        double sa = 0;
        double tempc = 0;
        double temps = 0;
        double x = 0;
        double w = 0;
        double p1 = 0;

        ap.assert(n > 0, "PolynomialCalcCheb1: N<=0!");
        ap.assert(ap.len(f) >= n, "PolynomialCalcCheb1: Length(F)<N!");
        ap.assert(math.isfinite(a), "PolynomialCalcCheb1: A is infinite or NaN!");
        ap.assert(math.isfinite(b), "PolynomialCalcCheb1: B is infinite or NaN!");
        ap.assert(apserv.isfinitevector(f, n, _params), "PolynomialCalcCheb1: F contains infinite or NaN values!");
        ap.assert((double)(b) != (double)(a), "PolynomialCalcCheb1: B=A!");
        ap.assert(!Double.IsInfinity(t), "PolynomialCalcCheb1: T is infinite!");

        //
        // Special case: T is NAN
        //
        if (Double.IsNaN(t))
        {
            result = Double.NaN;
            return result;
        }

        //
        // Special case: N=1
        //
        if (n == 1)
        {
            result = f[0];
            return result;
        }

        //
        // Prepare information for the recurrence formula
        // used to calculate sin(pi*(2j+1)/(2n+2)) and
        // cos(pi*(2j+1)/(2n+2)):
        //
        // A0    = pi/(2n+2)
        // Delta = pi/(n+1)
        // Alpha = 2 sin^2 (Delta/2)
        // Beta  = sin(Delta)
        //
        // so that sin(..) = sin(A0+j*delta) and cos(..) = cos(A0+j*delta).
        // Then we use
        //
        // sin(x+delta) = sin(x) - (alpha*sin(x) - beta*cos(x))
        // cos(x+delta) = cos(x) - (alpha*cos(x) - beta*sin(x))
        //
        // to repeatedly calculate sin(..) and cos(..).
        //
        threshold = Math.Sqrt(math.minrealnumber);
        t = (t - 0.5 * (a + b)) / (0.5 * (b - a));
        a0 = Math.PI / (2 * (n - 1) + 2);
        delta = 2 * Math.PI / (2 * (n - 1) + 2);
        alpha = 2 * math.sqr(Math.Sin(delta / 2));
        beta = Math.Sin(delta);

        //
        // First, decide: should we use "safe" formula (guarded
        // against overflow) or fast one?
        //
        ca = Math.Cos(a0);
        sa = Math.Sin(a0);
        j = 0;
        x = ca;
        s = t - x;
        for (i = 1; i <= n - 1; i++)
        {

            //
            // Next X[i]
            //
            temps = sa - (alpha * sa - beta * ca);
            tempc = ca - (alpha * ca + beta * sa);
            sa = temps;
            ca = tempc;
            x = ca;

            //
            // Use X[i]
            //
            if ((double)(Math.Abs(t - x)) < (double)(Math.Abs(s)))
            {
                s = t - x;
                j = i;
            }
        }
        if ((double)(s) == (double)(0))
        {
            result = f[j];
            return result;
        }
        if ((double)(Math.Abs(s)) > (double)(threshold))
        {

            //
            // use fast formula
            //
            j = -1;
            s = 1.0;
        }

        //
        // Calculate using safe or fast barycentric formula
        //
        s1 = 0;
        s2 = 0;
        ca = Math.Cos(a0);
        sa = Math.Sin(a0);
        p1 = 1.0;
        for (i = 0; i <= n - 1; i++)
        {

            //
            // Calculate X[i], W[i]
            //
            x = ca;
            w = p1 * sa;

            //
            // Proceed
            //
            if (i != j)
            {
                v = s * w / (t - x);
                s1 = s1 + v * f[i];
                s2 = s2 + v;
            }
            else
            {
                v = w;
                s1 = s1 + v * f[i];
                s2 = s2 + v;
            }

            //
            // Next CA, SA, P1
            //
            temps = sa - (alpha * sa - beta * ca);
            tempc = ca - (alpha * ca + beta * sa);
            sa = temps;
            ca = tempc;
            p1 = -p1;
        }
        result = s1 / s2;
        return result;
    }


    /*************************************************************************
    Fast polynomial interpolation function on Chebyshev points (second kind)
    with O(N) complexity.

    INPUT PARAMETERS:
        A   -   left boundary of [A,B]
        B   -   right boundary of [A,B]
        F   -   function values, array[0..N-1]
        N   -   number of points on Chebyshev grid (second kind),
                X[i] = 0.5*(B+A) + 0.5*(B-A)*Cos(PI*i/(n-1))
                for N=1 a constant model is constructed.
        T   -   position where P(x) is calculated

    RESULT
        value of the Lagrange interpolant at T

    IMPORTANT
        this function provides fast interface which is not overflow-safe
        nor it is very precise.
        the best option is to use PolIntBuildCheb2()/BarycentricCalc()
        subroutines unless you are pretty sure that your data will not result
        in overflow.

      -- ALGLIB --
         Copyright 02.12.2009 by Bochkanov Sergey
    *************************************************************************/
    public static double polynomialcalccheb2(double a,
        double b,
        double[] f,
        int n,
        double t,
        xparams _params)
    {
        double result = 0;
        double s1 = 0;
        double s2 = 0;
        double v = 0;
        double threshold = 0;
        double s = 0;
        int i = 0;
        int j = 0;
        double a0 = 0;
        double delta = 0;
        double alpha = 0;
        double beta = 0;
        double ca = 0;
        double sa = 0;
        double tempc = 0;
        double temps = 0;
        double x = 0;
        double w = 0;
        double p1 = 0;

        ap.assert(n > 0, "PolynomialCalcCheb2: N<=0!");
        ap.assert(ap.len(f) >= n, "PolynomialCalcCheb2: Length(F)<N!");
        ap.assert(math.isfinite(a), "PolynomialCalcCheb2: A is infinite or NaN!");
        ap.assert(math.isfinite(b), "PolynomialCalcCheb2: B is infinite or NaN!");
        ap.assert((double)(b) != (double)(a), "PolynomialCalcCheb2: B=A!");
        ap.assert(apserv.isfinitevector(f, n, _params), "PolynomialCalcCheb2: F contains infinite or NaN values!");
        ap.assert(!Double.IsInfinity(t), "PolynomialCalcEqDist: T is infinite!");

        //
        // Special case: T is NAN
        //
        if (Double.IsNaN(t))
        {
            result = Double.NaN;
            return result;
        }

        //
        // Special case: N=1
        //
        if (n == 1)
        {
            result = f[0];
            return result;
        }

        //
        // Prepare information for the recurrence formula
        // used to calculate sin(pi*i/n) and
        // cos(pi*i/n):
        //
        // A0    = 0
        // Delta = pi/n
        // Alpha = 2 sin^2 (Delta/2)
        // Beta  = sin(Delta)
        //
        // so that sin(..) = sin(A0+j*delta) and cos(..) = cos(A0+j*delta).
        // Then we use
        //
        // sin(x+delta) = sin(x) - (alpha*sin(x) - beta*cos(x))
        // cos(x+delta) = cos(x) - (alpha*cos(x) - beta*sin(x))
        //
        // to repeatedly calculate sin(..) and cos(..).
        //
        threshold = Math.Sqrt(math.minrealnumber);
        t = (t - 0.5 * (a + b)) / (0.5 * (b - a));
        a0 = 0.0;
        delta = Math.PI / (n - 1);
        alpha = 2 * math.sqr(Math.Sin(delta / 2));
        beta = Math.Sin(delta);

        //
        // First, decide: should we use "safe" formula (guarded
        // against overflow) or fast one?
        //
        ca = Math.Cos(a0);
        sa = Math.Sin(a0);
        j = 0;
        x = ca;
        s = t - x;
        for (i = 1; i <= n - 1; i++)
        {

            //
            // Next X[i]
            //
            temps = sa - (alpha * sa - beta * ca);
            tempc = ca - (alpha * ca + beta * sa);
            sa = temps;
            ca = tempc;
            x = ca;

            //
            // Use X[i]
            //
            if ((double)(Math.Abs(t - x)) < (double)(Math.Abs(s)))
            {
                s = t - x;
                j = i;
            }
        }
        if ((double)(s) == (double)(0))
        {
            result = f[j];
            return result;
        }
        if ((double)(Math.Abs(s)) > (double)(threshold))
        {

            //
            // use fast formula
            //
            j = -1;
            s = 1.0;
        }

        //
        // Calculate using safe or fast barycentric formula
        //
        s1 = 0;
        s2 = 0;
        ca = Math.Cos(a0);
        sa = Math.Sin(a0);
        p1 = 1.0;
        for (i = 0; i <= n - 1; i++)
        {

            //
            // Calculate X[i], W[i]
            //
            x = ca;
            if (i == 0 || i == n - 1)
            {
                w = 0.5 * p1;
            }
            else
            {
                w = 1.0 * p1;
            }

            //
            // Proceed
            //
            if (i != j)
            {
                v = s * w / (t - x);
                s1 = s1 + v * f[i];
                s2 = s2 + v;
            }
            else
            {
                v = w;
                s1 = s1 + v * f[i];
                s2 = s2 + v;
            }

            //
            // Next CA, SA, P1
            //
            temps = sa - (alpha * sa - beta * ca);
            tempc = ca - (alpha * ca + beta * sa);
            sa = temps;
            ca = tempc;
            p1 = -p1;
        }
        result = s1 / s2;
        return result;
    }


}
