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


public class spline1d
{
    /*************************************************************************
    1-dimensional spline interpolant
    *************************************************************************/
    public class spline1dinterpolant : apobject
    {
        public bool periodic;
        public int n;
        public int k;
        public int continuity;
        public double[] x;
        public double[] c;
        public spline1dinterpolant()
        {
            init();
        }
        public override void init()
        {
            x = new double[0];
            c = new double[0];
        }
        public override apobject make_copy()
        {
            spline1dinterpolant _result = new spline1dinterpolant();
            _result.periodic = periodic;
            _result.n = n;
            _result.k = k;
            _result.continuity = continuity;
            _result.x = (double[])x.Clone();
            _result.c = (double[])c.Clone();
            return _result;
        }
    };


    /*************************************************************************
    Spline fitting report:
        TerminationType completion code:
                        * >0 for success
                        * <0 for failure
        RMSError        RMS error
        AvgError        average error
        AvgRelError     average relative error (for non-zero Y[I])
        MaxError        maximum error
        
    Fields  below are  filled  by   obsolete    functions   (Spline1DFitCubic,
    Spline1DFitHermite). Modern fitting functions do NOT fill these fields:
        TaskRCond       reciprocal of task's condition number
    *************************************************************************/
    public class spline1dfitreport : apobject
    {
        public int terminationtype;
        public double taskrcond;
        public double rmserror;
        public double avgerror;
        public double avgrelerror;
        public double maxerror;
        public spline1dfitreport()
        {
            init();
        }
        public override void init()
        {
        }
        public override apobject make_copy()
        {
            spline1dfitreport _result = new spline1dfitreport();
            _result.terminationtype = terminationtype;
            _result.taskrcond = taskrcond;
            _result.rmserror = rmserror;
            _result.avgerror = avgerror;
            _result.avgrelerror = avgrelerror;
            _result.maxerror = maxerror;
            return _result;
        }
    };


    /*************************************************************************
    A set of M B-spline basis functions for a 1D spline fitting problem.

    The functions are defined on [0,1],  with  0-th  function  having  maximum
    value at X=0, (M-1)-th function having maximum value at X=1, and the  rest
    being equdistantly spaced.

      -- ALGLIB PROJECT --
         Copyright 09.04.2022 by Bochkanov Sergey.
    *************************************************************************/
    public class spline1dbbasis : apobject
    {
        public int m;
        public int bfrad;
        public spline1dinterpolant s0;
        public spline1dinterpolant s1;
        public spline1dinterpolant s2;
        public double[] tmpx;
        public double[] tmpy;
        public spline1dbbasis()
        {
            init();
        }
        public override void init()
        {
            s0 = new spline1dinterpolant();
            s1 = new spline1dinterpolant();
            s2 = new spline1dinterpolant();
            tmpx = new double[0];
            tmpy = new double[0];
        }
        public override apobject make_copy()
        {
            spline1dbbasis _result = new spline1dbbasis();
            _result.m = m;
            _result.bfrad = bfrad;
            _result.s0 = (spline1dinterpolant)s0.make_copy();
            _result.s1 = (spline1dinterpolant)s1.make_copy();
            _result.s2 = (spline1dinterpolant)s2.make_copy();
            _result.tmpx = (double[])tmpx.Clone();
            _result.tmpy = (double[])tmpy.Clone();
            return _result;
        }
    };




    public const double lambdareg = 1.0e-10;
    public const double cholreg = 1.0e-14;


    /*************************************************************************
    This subroutine builds linear spline interpolant

    INPUT PARAMETERS:
        X   -   spline nodes, array[0..N-1]
        Y   -   function values, array[0..N-1]
        N   -   points count (optional):
                * N>=2
                * if given, only first N points are used to build spline
                * if not given, automatically detected from X/Y sizes
                  (len(X) must be equal to len(Y))
        
    OUTPUT PARAMETERS:
        C   -   spline interpolant


    ORDER OF POINTS

    Subroutine automatically sorts points, so caller may pass unsorted array.

      -- ALGLIB PROJECT --
         Copyright 24.06.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dbuildlinear(double[] x,
        double[] y,
        int n,
        spline1dinterpolant c,
        xparams _params)
    {
        spline1dbuildlinearbuf(x, y, n, c, _params);
    }


    /*************************************************************************
    This subroutine builds linear spline interpolant.

    Buffered version of Spline1DBuildLinear() which reused  memory  previously
    allocated in C as much as possible.

      -- ALGLIB PROJECT --
         Copyright 24.06.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dbuildlinearbuf(double[] x,
        double[] y,
        int n,
        spline1dinterpolant c,
        xparams _params)
    {
        int i = 0;

        x = (double[])x.Clone();
        y = (double[])y.Clone();

        ap.assert(n > 1, "Spline1DBuildLinear: N<2!");
        ap.assert(ap.len(x) >= n, "Spline1DBuildLinear: Length(X)<N!");
        ap.assert(ap.len(y) >= n, "Spline1DBuildLinear: Length(Y)<N!");

        //
        // check and sort points
        //
        ap.assert(apserv.isfinitevector(x, n, _params), "Spline1DBuildLinear: X contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(y, n, _params), "Spline1DBuildLinear: Y contains infinite or NAN values!");
        heapsortpoints(ref x, ref y, n, _params);
        ap.assert(apserv.aredistinct(x, n, _params), "Spline1DBuildLinear: at least two consequent points are too close!");

        //
        // Build
        //
        c.periodic = false;
        c.n = n;
        c.k = 3;
        c.continuity = 0;
        c.x = new double[n];
        c.c = new double[4 * (n - 1) + 2];
        for (i = 0; i <= n - 1; i++)
        {
            c.x[i] = x[i];
        }
        for (i = 0; i <= n - 2; i++)
        {
            c.c[4 * i + 0] = y[i];
            c.c[4 * i + 1] = (y[i + 1] - y[i]) / (x[i + 1] - x[i]);
            c.c[4 * i + 2] = 0;
            c.c[4 * i + 3] = 0;
        }
        c.c[4 * (n - 1) + 0] = y[n - 1];
        c.c[4 * (n - 1) + 1] = c.c[4 * (n - 2) + 1];
    }


    /*************************************************************************
    This subroutine builds cubic spline interpolant.

    INPUT PARAMETERS:
        X           -   spline nodes, array[0..N-1].
        Y           -   function values, array[0..N-1].
        
    OPTIONAL PARAMETERS:
        N           -   points count:
                        * N>=2
                        * if given, only first N points are used to build spline
                        * if not given, automatically detected from X/Y sizes
                          (len(X) must be equal to len(Y))
        BoundLType  -   boundary condition type for the left boundary
        BoundL      -   left boundary condition (first or second derivative,
                        depending on the BoundLType)
        BoundRType  -   boundary condition type for the right boundary
        BoundR      -   right boundary condition (first or second derivative,
                        depending on the BoundRType)

    OUTPUT PARAMETERS:
        C           -   spline interpolant

    ORDER OF POINTS

    Subroutine automatically sorts points, so caller may pass unsorted array.

    SETTING BOUNDARY VALUES:

    The BoundLType/BoundRType parameters can have the following values:
        * -1, which corresonds to the periodic (cyclic) boundary conditions.
              In this case:
              * both BoundLType and BoundRType must be equal to -1.
              * BoundL/BoundR are ignored
              * Y[last] is ignored (it is assumed to be equal to Y[first]).
        *  0, which  corresponds  to  the  parabolically   terminated  spline
              (BoundL and/or BoundR are ignored).
        *  1, which corresponds to the first derivative boundary condition
        *  2, which corresponds to the second derivative boundary condition
        *  by default, BoundType=0 is used

    PROBLEMS WITH PERIODIC BOUNDARY CONDITIONS:

    Problems with periodic boundary conditions have Y[first_point]=Y[last_point].
    However, this subroutine doesn't require you to specify equal  values  for
    the first and last points - it automatically forces them  to  be  equal by
    copying  Y[first_point]  (corresponds  to the leftmost,  minimal  X[])  to
    Y[last_point]. However it is recommended to pass consistent values of Y[],
    i.e. to make Y[first_point]=Y[last_point].

      -- ALGLIB PROJECT --
         Copyright 23.06.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dbuildcubic(double[] x,
        double[] y,
        int n,
        int boundltype,
        double boundl,
        int boundrtype,
        double boundr,
        spline1dinterpolant c,
        xparams _params)
    {
        double[] a1 = new double[0];
        double[] a2 = new double[0];
        double[] a3 = new double[0];
        double[] b = new double[0];
        double[] dt = new double[0];
        double[] d = new double[0];
        int[] p = new int[0];
        int ylen = 0;

        x = (double[])x.Clone();
        y = (double[])y.Clone();


        //
        // check correctness of boundary conditions
        //
        ap.assert(((boundltype == -1 || boundltype == 0) || boundltype == 1) || boundltype == 2, "Spline1DBuildCubic: incorrect BoundLType!");
        ap.assert(((boundrtype == -1 || boundrtype == 0) || boundrtype == 1) || boundrtype == 2, "Spline1DBuildCubic: incorrect BoundRType!");
        ap.assert((boundrtype == -1 && boundltype == -1) || (boundrtype != -1 && boundltype != -1), "Spline1DBuildCubic: incorrect BoundLType/BoundRType!");
        if (boundltype == 1 || boundltype == 2)
        {
            ap.assert(math.isfinite(boundl), "Spline1DBuildCubic: BoundL is infinite or NAN!");
        }
        if (boundrtype == 1 || boundrtype == 2)
        {
            ap.assert(math.isfinite(boundr), "Spline1DBuildCubic: BoundR is infinite or NAN!");
        }

        //
        // check lengths of arguments
        //
        ap.assert(n >= 2, "Spline1DBuildCubic: N<2!");
        ap.assert(ap.len(x) >= n, "Spline1DBuildCubic: Length(X)<N!");
        ap.assert(ap.len(y) >= n, "Spline1DBuildCubic: Length(Y)<N!");

        //
        // check and sort points
        //
        ylen = n;
        if (boundltype == -1)
        {
            ylen = n - 1;
        }
        ap.assert(apserv.isfinitevector(x, n, _params), "Spline1DBuildCubic: X contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(y, ylen, _params), "Spline1DBuildCubic: Y contains infinite or NAN values!");
        heapsortppoints(ref x, ref y, ref p, n, _params);
        ap.assert(apserv.aredistinct(x, n, _params), "Spline1DBuildCubic: at least two consequent points are too close!");

        //
        // Now we've checked and preordered everything,
        // so we can call internal function to calculate derivatives,
        // and then build Hermite spline using these derivatives
        //
        if (boundltype == -1 || boundrtype == -1)
        {
            y[n - 1] = y[0];
        }
        spline1dgriddiffcubicinternal(x, ref y, n, boundltype, boundl, boundrtype, boundr, ref d, ref a1, ref a2, ref a3, ref b, ref dt, _params);
        spline1dbuildhermite(x, y, d, n, c, _params);
        c.periodic = boundltype == -1 || boundrtype == -1;
        c.continuity = 2;
    }


    /*************************************************************************
    This function solves following problem: given table y[] of function values
    at nodes x[], it calculates and returns table of function derivatives  d[]
    (calculated at the same nodes x[]).

    This function yields same result as Spline1DBuildCubic() call followed  by
    sequence of Spline1DDiff() calls, but it can be several times faster  when
    called for ordered X[] and X2[].

    INPUT PARAMETERS:
        X           -   spline nodes
        Y           -   function values

    OPTIONAL PARAMETERS:
        N           -   points count:
                        * N>=2
                        * if given, only first N points are used
                        * if not given, automatically detected from X/Y sizes
                          (len(X) must be equal to len(Y))
        BoundLType  -   boundary condition type for the left boundary
        BoundL      -   left boundary condition (first or second derivative,
                        depending on the BoundLType)
        BoundRType  -   boundary condition type for the right boundary
        BoundR      -   right boundary condition (first or second derivative,
                        depending on the BoundRType)

    OUTPUT PARAMETERS:
        D           -   derivative values at X[]

    ORDER OF POINTS

    Subroutine automatically sorts points, so caller may pass unsorted array.
    Derivative values are correctly reordered on return, so  D[I]  is  always
    equal to S'(X[I]) independently of points order.

    SETTING BOUNDARY VALUES:

    The BoundLType/BoundRType parameters can have the following values:
        * -1, which corresonds to the periodic (cyclic) boundary conditions.
              In this case:
              * both BoundLType and BoundRType must be equal to -1.
              * BoundL/BoundR are ignored
              * Y[last] is ignored (it is assumed to be equal to Y[first]).
        *  0, which  corresponds  to  the  parabolically   terminated  spline
              (BoundL and/or BoundR are ignored).
        *  1, which corresponds to the first derivative boundary condition
        *  2, which corresponds to the second derivative boundary condition
        *  by default, BoundType=0 is used

    PROBLEMS WITH PERIODIC BOUNDARY CONDITIONS:

    Problems with periodic boundary conditions have Y[first_point]=Y[last_point].
    However, this subroutine doesn't require you to specify equal  values  for
    the first and last points - it automatically forces them  to  be  equal by
    copying  Y[first_point]  (corresponds  to the leftmost,  minimal  X[])  to
    Y[last_point]. However it is recommended to pass consistent values of Y[],
    i.e. to make Y[first_point]=Y[last_point].

      -- ALGLIB PROJECT --
         Copyright 03.09.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dgriddiffcubic(double[] x,
        double[] y,
        int n,
        int boundltype,
        double boundl,
        int boundrtype,
        double boundr,
        ref double[] d,
        xparams _params)
    {
        double[] a1 = new double[0];
        double[] a2 = new double[0];
        double[] a3 = new double[0];
        double[] b = new double[0];
        double[] dt = new double[0];
        int[] p = new int[0];
        int i = 0;
        int ylen = 0;
        int i_ = 0;

        x = (double[])x.Clone();
        y = (double[])y.Clone();
        d = new double[0];


        //
        // check correctness of boundary conditions
        //
        ap.assert(((boundltype == -1 || boundltype == 0) || boundltype == 1) || boundltype == 2, "Spline1DGridDiffCubic: incorrect BoundLType!");
        ap.assert(((boundrtype == -1 || boundrtype == 0) || boundrtype == 1) || boundrtype == 2, "Spline1DGridDiffCubic: incorrect BoundRType!");
        ap.assert((boundrtype == -1 && boundltype == -1) || (boundrtype != -1 && boundltype != -1), "Spline1DGridDiffCubic: incorrect BoundLType/BoundRType!");
        if (boundltype == 1 || boundltype == 2)
        {
            ap.assert(math.isfinite(boundl), "Spline1DGridDiffCubic: BoundL is infinite or NAN!");
        }
        if (boundrtype == 1 || boundrtype == 2)
        {
            ap.assert(math.isfinite(boundr), "Spline1DGridDiffCubic: BoundR is infinite or NAN!");
        }

        //
        // check lengths of arguments
        //
        ap.assert(n >= 2, "Spline1DGridDiffCubic: N<2!");
        ap.assert(ap.len(x) >= n, "Spline1DGridDiffCubic: Length(X)<N!");
        ap.assert(ap.len(y) >= n, "Spline1DGridDiffCubic: Length(Y)<N!");

        //
        // check and sort points
        //
        ylen = n;
        if (boundltype == -1)
        {
            ylen = n - 1;
        }
        ap.assert(apserv.isfinitevector(x, n, _params), "Spline1DGridDiffCubic: X contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(y, ylen, _params), "Spline1DGridDiffCubic: Y contains infinite or NAN values!");
        heapsortppoints(ref x, ref y, ref p, n, _params);
        ap.assert(apserv.aredistinct(x, n, _params), "Spline1DGridDiffCubic: at least two consequent points are too close!");

        //
        // Now we've checked and preordered everything,
        // so we can call internal function.
        //
        spline1dgriddiffcubicinternal(x, ref y, n, boundltype, boundl, boundrtype, boundr, ref d, ref a1, ref a2, ref a3, ref b, ref dt, _params);

        //
        // Remember that HeapSortPPoints() call?
        // Now we have to reorder them back.
        //
        if (ap.len(dt) < n)
        {
            dt = new double[n];
        }
        for (i = 0; i <= n - 1; i++)
        {
            dt[p[i]] = d[i];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            d[i_] = dt[i_];
        }
    }


    /*************************************************************************
    This function solves following problem: given table y[] of function values
    at  nodes  x[],  it  calculates  and  returns  tables  of first and second
    function derivatives d1[] and d2[] (calculated at the same nodes x[]).

    This function yields same result as Spline1DBuildCubic() call followed  by
    sequence of Spline1DDiff() calls, but it can be several times faster  when
    called for ordered X[] and X2[].

    INPUT PARAMETERS:
        X           -   spline nodes
        Y           -   function values

    OPTIONAL PARAMETERS:
        N           -   points count:
                        * N>=2
                        * if given, only first N points are used
                        * if not given, automatically detected from X/Y sizes
                          (len(X) must be equal to len(Y))
        BoundLType  -   boundary condition type for the left boundary
        BoundL      -   left boundary condition (first or second derivative,
                        depending on the BoundLType)
        BoundRType  -   boundary condition type for the right boundary
        BoundR      -   right boundary condition (first or second derivative,
                        depending on the BoundRType)

    OUTPUT PARAMETERS:
        D1          -   S' values at X[]
        D2          -   S'' values at X[]

    ORDER OF POINTS

    Subroutine automatically sorts points, so caller may pass unsorted array.
    Derivative values are correctly reordered on return, so  D[I]  is  always
    equal to S'(X[I]) independently of points order.

    SETTING BOUNDARY VALUES:

    The BoundLType/BoundRType parameters can have the following values:
        * -1, which corresonds to the periodic (cyclic) boundary conditions.
              In this case:
              * both BoundLType and BoundRType must be equal to -1.
              * BoundL/BoundR are ignored
              * Y[last] is ignored (it is assumed to be equal to Y[first]).
        *  0, which  corresponds  to  the  parabolically   terminated  spline
              (BoundL and/or BoundR are ignored).
        *  1, which corresponds to the first derivative boundary condition
        *  2, which corresponds to the second derivative boundary condition
        *  by default, BoundType=0 is used

    PROBLEMS WITH PERIODIC BOUNDARY CONDITIONS:

    Problems with periodic boundary conditions have Y[first_point]=Y[last_point].
    However, this subroutine doesn't require you to specify equal  values  for
    the first and last points - it automatically forces them  to  be  equal by
    copying  Y[first_point]  (corresponds  to the leftmost,  minimal  X[])  to
    Y[last_point]. However it is recommended to pass consistent values of Y[],
    i.e. to make Y[first_point]=Y[last_point].

      -- ALGLIB PROJECT --
         Copyright 03.09.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dgriddiff2cubic(double[] x,
        double[] y,
        int n,
        int boundltype,
        double boundl,
        int boundrtype,
        double boundr,
        ref double[] d1,
        ref double[] d2,
        xparams _params)
    {
        double[] a1 = new double[0];
        double[] a2 = new double[0];
        double[] a3 = new double[0];
        double[] b = new double[0];
        double[] dt = new double[0];
        int[] p = new int[0];
        int i = 0;
        int ylen = 0;
        double delta = 0;
        double delta2 = 0;
        double delta3 = 0;
        double s2 = 0;
        double s3 = 0;
        int i_ = 0;

        x = (double[])x.Clone();
        y = (double[])y.Clone();
        d1 = new double[0];
        d2 = new double[0];


        //
        // check correctness of boundary conditions
        //
        ap.assert(((boundltype == -1 || boundltype == 0) || boundltype == 1) || boundltype == 2, "Spline1DGridDiff2Cubic: incorrect BoundLType!");
        ap.assert(((boundrtype == -1 || boundrtype == 0) || boundrtype == 1) || boundrtype == 2, "Spline1DGridDiff2Cubic: incorrect BoundRType!");
        ap.assert((boundrtype == -1 && boundltype == -1) || (boundrtype != -1 && boundltype != -1), "Spline1DGridDiff2Cubic: incorrect BoundLType/BoundRType!");
        if (boundltype == 1 || boundltype == 2)
        {
            ap.assert(math.isfinite(boundl), "Spline1DGridDiff2Cubic: BoundL is infinite or NAN!");
        }
        if (boundrtype == 1 || boundrtype == 2)
        {
            ap.assert(math.isfinite(boundr), "Spline1DGridDiff2Cubic: BoundR is infinite or NAN!");
        }

        //
        // check lengths of arguments
        //
        ap.assert(n >= 2, "Spline1DGridDiff2Cubic: N<2!");
        ap.assert(ap.len(x) >= n, "Spline1DGridDiff2Cubic: Length(X)<N!");
        ap.assert(ap.len(y) >= n, "Spline1DGridDiff2Cubic: Length(Y)<N!");

        //
        // check and sort points
        //
        ylen = n;
        if (boundltype == -1)
        {
            ylen = n - 1;
        }
        ap.assert(apserv.isfinitevector(x, n, _params), "Spline1DGridDiff2Cubic: X contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(y, ylen, _params), "Spline1DGridDiff2Cubic: Y contains infinite or NAN values!");
        heapsortppoints(ref x, ref y, ref p, n, _params);
        ap.assert(apserv.aredistinct(x, n, _params), "Spline1DGridDiff2Cubic: at least two consequent points are too close!");

        //
        // Now we've checked and preordered everything,
        // so we can call internal function.
        //
        // After this call we will calculate second derivatives
        // (manually, by converting to the power basis)
        //
        spline1dgriddiffcubicinternal(x, ref y, n, boundltype, boundl, boundrtype, boundr, ref d1, ref a1, ref a2, ref a3, ref b, ref dt, _params);
        d2 = new double[n];
        delta = 0;
        s2 = 0;
        s3 = 0;
        for (i = 0; i <= n - 2; i++)
        {

            //
            // We convert from Hermite basis to the power basis.
            // Si is coefficient before x^i.
            //
            // Inside this cycle we need just S2,
            // because we calculate S'' exactly at spline node,
            // (only x^2 matters at x=0), but after iterations
            // will be over, we will need other coefficients
            // to calculate spline value at the last node.
            //
            delta = x[i + 1] - x[i];
            delta2 = math.sqr(delta);
            delta3 = delta * delta2;
            s2 = (3 * (y[i + 1] - y[i]) - 2 * d1[i] * delta - d1[i + 1] * delta) / delta2;
            s3 = (2 * (y[i] - y[i + 1]) + d1[i] * delta + d1[i + 1] * delta) / delta3;
            d2[i] = 2 * s2;
        }
        d2[n - 1] = 2 * s2 + 6 * s3 * delta;

        //
        // Remember that HeapSortPPoints() call?
        // Now we have to reorder them back.
        //
        if (ap.len(dt) < n)
        {
            dt = new double[n];
        }
        for (i = 0; i <= n - 1; i++)
        {
            dt[p[i]] = d1[i];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            d1[i_] = dt[i_];
        }
        for (i = 0; i <= n - 1; i++)
        {
            dt[p[i]] = d2[i];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            d2[i_] = dt[i_];
        }
    }


    /*************************************************************************
    This function solves following problem: given table y[] of function values
    at old nodes x[]  and new nodes  x2[],  it calculates and returns table of
    function values y2[] (calculated at x2[]).

    This function yields same result as Spline1DBuildCubic() call followed  by
    sequence of Spline1DDiff() calls, but it can be several times faster  when
    called for ordered X[] and X2[].

    INPUT PARAMETERS:
        X           -   old spline nodes
        Y           -   function values
        X2           -  new spline nodes

    OPTIONAL PARAMETERS:
        N           -   points count:
                        * N>=2
                        * if given, only first N points from X/Y are used
                        * if not given, automatically detected from X/Y sizes
                          (len(X) must be equal to len(Y))
        BoundLType  -   boundary condition type for the left boundary
        BoundL      -   left boundary condition (first or second derivative,
                        depending on the BoundLType)
        BoundRType  -   boundary condition type for the right boundary
        BoundR      -   right boundary condition (first or second derivative,
                        depending on the BoundRType)
        N2          -   new points count:
                        * N2>=2
                        * if given, only first N2 points from X2 are used
                        * if not given, automatically detected from X2 size

    OUTPUT PARAMETERS:
        F2          -   function values at X2[]

    ORDER OF POINTS

    Subroutine automatically sorts points, so caller  may pass unsorted array.
    Function  values  are correctly reordered on  return, so F2[I]  is  always
    equal to S(X2[I]) independently of points order.

    SETTING BOUNDARY VALUES:

    The BoundLType/BoundRType parameters can have the following values:
        * -1, which corresonds to the periodic (cyclic) boundary conditions.
              In this case:
              * both BoundLType and BoundRType must be equal to -1.
              * BoundL/BoundR are ignored
              * Y[last] is ignored (it is assumed to be equal to Y[first]).
        *  0, which  corresponds  to  the  parabolically   terminated  spline
              (BoundL and/or BoundR are ignored).
        *  1, which corresponds to the first derivative boundary condition
        *  2, which corresponds to the second derivative boundary condition
        *  by default, BoundType=0 is used

    PROBLEMS WITH PERIODIC BOUNDARY CONDITIONS:

    Problems with periodic boundary conditions have Y[first_point]=Y[last_point].
    However, this subroutine doesn't require you to specify equal  values  for
    the first and last points - it automatically forces them  to  be  equal by
    copying  Y[first_point]  (corresponds  to the leftmost,  minimal  X[])  to
    Y[last_point]. However it is recommended to pass consistent values of Y[],
    i.e. to make Y[first_point]=Y[last_point].

      -- ALGLIB PROJECT --
         Copyright 03.09.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dconvcubic(double[] x,
        double[] y,
        int n,
        int boundltype,
        double boundl,
        int boundrtype,
        double boundr,
        double[] x2,
        int n2,
        ref double[] y2,
        xparams _params)
    {
        double[] a1 = new double[0];
        double[] a2 = new double[0];
        double[] a3 = new double[0];
        double[] b = new double[0];
        double[] d = new double[0];
        double[] dt = new double[0];
        double[] d1 = new double[0];
        double[] d2 = new double[0];
        int[] p = new int[0];
        int[] p2 = new int[0];
        int i = 0;
        int ylen = 0;
        double t = 0;
        double t2 = 0;
        int i_ = 0;

        x = (double[])x.Clone();
        y = (double[])y.Clone();
        x2 = (double[])x2.Clone();
        y2 = new double[0];


        //
        // check correctness of boundary conditions
        //
        ap.assert(((boundltype == -1 || boundltype == 0) || boundltype == 1) || boundltype == 2, "Spline1DConvCubic: incorrect BoundLType!");
        ap.assert(((boundrtype == -1 || boundrtype == 0) || boundrtype == 1) || boundrtype == 2, "Spline1DConvCubic: incorrect BoundRType!");
        ap.assert((boundrtype == -1 && boundltype == -1) || (boundrtype != -1 && boundltype != -1), "Spline1DConvCubic: incorrect BoundLType/BoundRType!");
        if (boundltype == 1 || boundltype == 2)
        {
            ap.assert(math.isfinite(boundl), "Spline1DConvCubic: BoundL is infinite or NAN!");
        }
        if (boundrtype == 1 || boundrtype == 2)
        {
            ap.assert(math.isfinite(boundr), "Spline1DConvCubic: BoundR is infinite or NAN!");
        }

        //
        // check lengths of arguments
        //
        ap.assert(n >= 2, "Spline1DConvCubic: N<2!");
        ap.assert(ap.len(x) >= n, "Spline1DConvCubic: Length(X)<N!");
        ap.assert(ap.len(y) >= n, "Spline1DConvCubic: Length(Y)<N!");
        ap.assert(n2 >= 2, "Spline1DConvCubic: N2<2!");
        ap.assert(ap.len(x2) >= n2, "Spline1DConvCubic: Length(X2)<N2!");

        //
        // check and sort X/Y
        //
        ylen = n;
        if (boundltype == -1)
        {
            ylen = n - 1;
        }
        ap.assert(apserv.isfinitevector(x, n, _params), "Spline1DConvCubic: X contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(y, ylen, _params), "Spline1DConvCubic: Y contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(x2, n2, _params), "Spline1DConvCubic: X2 contains infinite or NAN values!");
        heapsortppoints(ref x, ref y, ref p, n, _params);
        ap.assert(apserv.aredistinct(x, n, _params), "Spline1DConvCubic: at least two consequent points are too close!");

        //
        // set up DT (we will need it below)
        //
        dt = new double[Math.Max(n, n2)];

        //
        // sort X2:
        // * use fake array DT because HeapSortPPoints() needs both integer AND real arrays
        // * if we have periodic problem, wrap points
        // * sort them, store permutation at P2
        //
        if (boundrtype == -1 && boundltype == -1)
        {
            for (i = 0; i <= n2 - 1; i++)
            {
                t = x2[i];
                apserv.apperiodicmap(ref t, x[0], x[n - 1], ref t2, _params);
                x2[i] = t;
            }
        }
        heapsortppoints(ref x2, ref dt, ref p2, n2, _params);

        //
        // Now we've checked and preordered everything, so we:
        // * call internal GridDiff() function to get Hermite form of spline
        // * convert using internal Conv() function
        // * convert Y2 back to original order
        //
        spline1dgriddiffcubicinternal(x, ref y, n, boundltype, boundl, boundrtype, boundr, ref d, ref a1, ref a2, ref a3, ref b, ref dt, _params);
        spline1dconvdiffinternal(x, y, d, n, x2, n2, ref y2, true, ref d1, false, ref d2, false, _params);
        ap.assert(ap.len(dt) >= n2, "Spline1DConvCubic: internal error!");
        for (i = 0; i <= n2 - 1; i++)
        {
            dt[p2[i]] = y2[i];
        }
        for (i_ = 0; i_ <= n2 - 1; i_++)
        {
            y2[i_] = dt[i_];
        }
    }


    /*************************************************************************
    This function solves following problem: given table y[] of function values
    at old nodes x[]  and new nodes  x2[],  it calculates and returns table of
    function values y2[] and derivatives d2[] (calculated at x2[]).

    This function yields same result as Spline1DBuildCubic() call followed  by
    sequence of Spline1DDiff() calls, but it can be several times faster  when
    called for ordered X[] and X2[].

    INPUT PARAMETERS:
        X           -   old spline nodes
        Y           -   function values
        X2           -  new spline nodes

    OPTIONAL PARAMETERS:
        N           -   points count:
                        * N>=2
                        * if given, only first N points from X/Y are used
                        * if not given, automatically detected from X/Y sizes
                          (len(X) must be equal to len(Y))
        BoundLType  -   boundary condition type for the left boundary
        BoundL      -   left boundary condition (first or second derivative,
                        depending on the BoundLType)
        BoundRType  -   boundary condition type for the right boundary
        BoundR      -   right boundary condition (first or second derivative,
                        depending on the BoundRType)
        N2          -   new points count:
                        * N2>=2
                        * if given, only first N2 points from X2 are used
                        * if not given, automatically detected from X2 size

    OUTPUT PARAMETERS:
        F2          -   function values at X2[]
        D2          -   first derivatives at X2[]

    ORDER OF POINTS

    Subroutine automatically sorts points, so caller  may pass unsorted array.
    Function  values  are correctly reordered on  return, so F2[I]  is  always
    equal to S(X2[I]) independently of points order.

    SETTING BOUNDARY VALUES:

    The BoundLType/BoundRType parameters can have the following values:
        * -1, which corresonds to the periodic (cyclic) boundary conditions.
              In this case:
              * both BoundLType and BoundRType must be equal to -1.
              * BoundL/BoundR are ignored
              * Y[last] is ignored (it is assumed to be equal to Y[first]).
        *  0, which  corresponds  to  the  parabolically   terminated  spline
              (BoundL and/or BoundR are ignored).
        *  1, which corresponds to the first derivative boundary condition
        *  2, which corresponds to the second derivative boundary condition
        *  by default, BoundType=0 is used

    PROBLEMS WITH PERIODIC BOUNDARY CONDITIONS:

    Problems with periodic boundary conditions have Y[first_point]=Y[last_point].
    However, this subroutine doesn't require you to specify equal  values  for
    the first and last points - it automatically forces them  to  be  equal by
    copying  Y[first_point]  (corresponds  to the leftmost,  minimal  X[])  to
    Y[last_point]. However it is recommended to pass consistent values of Y[],
    i.e. to make Y[first_point]=Y[last_point].

      -- ALGLIB PROJECT --
         Copyright 03.09.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dconvdiffcubic(double[] x,
        double[] y,
        int n,
        int boundltype,
        double boundl,
        int boundrtype,
        double boundr,
        double[] x2,
        int n2,
        ref double[] y2,
        ref double[] d2,
        xparams _params)
    {
        double[] a1 = new double[0];
        double[] a2 = new double[0];
        double[] a3 = new double[0];
        double[] b = new double[0];
        double[] d = new double[0];
        double[] dt = new double[0];
        double[] rt1 = new double[0];
        int[] p = new int[0];
        int[] p2 = new int[0];
        int i = 0;
        int ylen = 0;
        double t = 0;
        double t2 = 0;
        int i_ = 0;

        x = (double[])x.Clone();
        y = (double[])y.Clone();
        x2 = (double[])x2.Clone();
        y2 = new double[0];
        d2 = new double[0];


        //
        // check correctness of boundary conditions
        //
        ap.assert(((boundltype == -1 || boundltype == 0) || boundltype == 1) || boundltype == 2, "Spline1DConvDiffCubic: incorrect BoundLType!");
        ap.assert(((boundrtype == -1 || boundrtype == 0) || boundrtype == 1) || boundrtype == 2, "Spline1DConvDiffCubic: incorrect BoundRType!");
        ap.assert((boundrtype == -1 && boundltype == -1) || (boundrtype != -1 && boundltype != -1), "Spline1DConvDiffCubic: incorrect BoundLType/BoundRType!");
        if (boundltype == 1 || boundltype == 2)
        {
            ap.assert(math.isfinite(boundl), "Spline1DConvDiffCubic: BoundL is infinite or NAN!");
        }
        if (boundrtype == 1 || boundrtype == 2)
        {
            ap.assert(math.isfinite(boundr), "Spline1DConvDiffCubic: BoundR is infinite or NAN!");
        }

        //
        // check lengths of arguments
        //
        ap.assert(n >= 2, "Spline1DConvDiffCubic: N<2!");
        ap.assert(ap.len(x) >= n, "Spline1DConvDiffCubic: Length(X)<N!");
        ap.assert(ap.len(y) >= n, "Spline1DConvDiffCubic: Length(Y)<N!");
        ap.assert(n2 >= 2, "Spline1DConvDiffCubic: N2<2!");
        ap.assert(ap.len(x2) >= n2, "Spline1DConvDiffCubic: Length(X2)<N2!");

        //
        // check and sort X/Y
        //
        ylen = n;
        if (boundltype == -1)
        {
            ylen = n - 1;
        }
        ap.assert(apserv.isfinitevector(x, n, _params), "Spline1DConvDiffCubic: X contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(y, ylen, _params), "Spline1DConvDiffCubic: Y contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(x2, n2, _params), "Spline1DConvDiffCubic: X2 contains infinite or NAN values!");
        heapsortppoints(ref x, ref y, ref p, n, _params);
        ap.assert(apserv.aredistinct(x, n, _params), "Spline1DConvDiffCubic: at least two consequent points are too close!");

        //
        // set up DT (we will need it below)
        //
        dt = new double[Math.Max(n, n2)];

        //
        // sort X2:
        // * use fake array DT because HeapSortPPoints() needs both integer AND real arrays
        // * if we have periodic problem, wrap points
        // * sort them, store permutation at P2
        //
        if (boundrtype == -1 && boundltype == -1)
        {
            for (i = 0; i <= n2 - 1; i++)
            {
                t = x2[i];
                apserv.apperiodicmap(ref t, x[0], x[n - 1], ref t2, _params);
                x2[i] = t;
            }
        }
        heapsortppoints(ref x2, ref dt, ref p2, n2, _params);

        //
        // Now we've checked and preordered everything, so we:
        // * call internal GridDiff() function to get Hermite form of spline
        // * convert using internal Conv() function
        // * convert Y2 back to original order
        //
        spline1dgriddiffcubicinternal(x, ref y, n, boundltype, boundl, boundrtype, boundr, ref d, ref a1, ref a2, ref a3, ref b, ref dt, _params);
        spline1dconvdiffinternal(x, y, d, n, x2, n2, ref y2, true, ref d2, true, ref rt1, false, _params);
        ap.assert(ap.len(dt) >= n2, "Spline1DConvDiffCubic: internal error!");
        for (i = 0; i <= n2 - 1; i++)
        {
            dt[p2[i]] = y2[i];
        }
        for (i_ = 0; i_ <= n2 - 1; i_++)
        {
            y2[i_] = dt[i_];
        }
        for (i = 0; i <= n2 - 1; i++)
        {
            dt[p2[i]] = d2[i];
        }
        for (i_ = 0; i_ <= n2 - 1; i_++)
        {
            d2[i_] = dt[i_];
        }
    }


    /*************************************************************************
    This function solves following problem: given table y[] of function values
    at old nodes x[]  and new nodes  x2[],  it calculates and returns table of
    function  values  y2[],  first  and  second  derivatives  d2[]  and  dd2[]
    (calculated at x2[]).

    This function yields same result as Spline1DBuildCubic() call followed  by
    sequence of Spline1DDiff() calls, but it can be several times faster  when
    called for ordered X[] and X2[].

    INPUT PARAMETERS:
        X           -   old spline nodes
        Y           -   function values
        X2           -  new spline nodes

    OPTIONAL PARAMETERS:
        N           -   points count:
                        * N>=2
                        * if given, only first N points from X/Y are used
                        * if not given, automatically detected from X/Y sizes
                          (len(X) must be equal to len(Y))
        BoundLType  -   boundary condition type for the left boundary
        BoundL      -   left boundary condition (first or second derivative,
                        depending on the BoundLType)
        BoundRType  -   boundary condition type for the right boundary
        BoundR      -   right boundary condition (first or second derivative,
                        depending on the BoundRType)
        N2          -   new points count:
                        * N2>=2
                        * if given, only first N2 points from X2 are used
                        * if not given, automatically detected from X2 size

    OUTPUT PARAMETERS:
        F2          -   function values at X2[]
        D2          -   first derivatives at X2[]
        DD2         -   second derivatives at X2[]

    ORDER OF POINTS

    Subroutine automatically sorts points, so caller  may pass unsorted array.
    Function  values  are correctly reordered on  return, so F2[I]  is  always
    equal to S(X2[I]) independently of points order.

    SETTING BOUNDARY VALUES:

    The BoundLType/BoundRType parameters can have the following values:
        * -1, which corresonds to the periodic (cyclic) boundary conditions.
              In this case:
              * both BoundLType and BoundRType must be equal to -1.
              * BoundL/BoundR are ignored
              * Y[last] is ignored (it is assumed to be equal to Y[first]).
        *  0, which  corresponds  to  the  parabolically   terminated  spline
              (BoundL and/or BoundR are ignored).
        *  1, which corresponds to the first derivative boundary condition
        *  2, which corresponds to the second derivative boundary condition
        *  by default, BoundType=0 is used

    PROBLEMS WITH PERIODIC BOUNDARY CONDITIONS:

    Problems with periodic boundary conditions have Y[first_point]=Y[last_point].
    However, this subroutine doesn't require you to specify equal  values  for
    the first and last points - it automatically forces them  to  be  equal by
    copying  Y[first_point]  (corresponds  to the leftmost,  minimal  X[])  to
    Y[last_point]. However it is recommended to pass consistent values of Y[],
    i.e. to make Y[first_point]=Y[last_point].

      -- ALGLIB PROJECT --
         Copyright 03.09.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dconvdiff2cubic(double[] x,
        double[] y,
        int n,
        int boundltype,
        double boundl,
        int boundrtype,
        double boundr,
        double[] x2,
        int n2,
        ref double[] y2,
        ref double[] d2,
        ref double[] dd2,
        xparams _params)
    {
        double[] a1 = new double[0];
        double[] a2 = new double[0];
        double[] a3 = new double[0];
        double[] b = new double[0];
        double[] d = new double[0];
        double[] dt = new double[0];
        int[] p = new int[0];
        int[] p2 = new int[0];
        int i = 0;
        int ylen = 0;
        double t = 0;
        double t2 = 0;
        int i_ = 0;

        x = (double[])x.Clone();
        y = (double[])y.Clone();
        x2 = (double[])x2.Clone();
        y2 = new double[0];
        d2 = new double[0];
        dd2 = new double[0];


        //
        // check correctness of boundary conditions
        //
        ap.assert(((boundltype == -1 || boundltype == 0) || boundltype == 1) || boundltype == 2, "Spline1DConvDiff2Cubic: incorrect BoundLType!");
        ap.assert(((boundrtype == -1 || boundrtype == 0) || boundrtype == 1) || boundrtype == 2, "Spline1DConvDiff2Cubic: incorrect BoundRType!");
        ap.assert((boundrtype == -1 && boundltype == -1) || (boundrtype != -1 && boundltype != -1), "Spline1DConvDiff2Cubic: incorrect BoundLType/BoundRType!");
        if (boundltype == 1 || boundltype == 2)
        {
            ap.assert(math.isfinite(boundl), "Spline1DConvDiff2Cubic: BoundL is infinite or NAN!");
        }
        if (boundrtype == 1 || boundrtype == 2)
        {
            ap.assert(math.isfinite(boundr), "Spline1DConvDiff2Cubic: BoundR is infinite or NAN!");
        }

        //
        // check lengths of arguments
        //
        ap.assert(n >= 2, "Spline1DConvDiff2Cubic: N<2!");
        ap.assert(ap.len(x) >= n, "Spline1DConvDiff2Cubic: Length(X)<N!");
        ap.assert(ap.len(y) >= n, "Spline1DConvDiff2Cubic: Length(Y)<N!");
        ap.assert(n2 >= 2, "Spline1DConvDiff2Cubic: N2<2!");
        ap.assert(ap.len(x2) >= n2, "Spline1DConvDiff2Cubic: Length(X2)<N2!");

        //
        // check and sort X/Y
        //
        ylen = n;
        if (boundltype == -1)
        {
            ylen = n - 1;
        }
        ap.assert(apserv.isfinitevector(x, n, _params), "Spline1DConvDiff2Cubic: X contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(y, ylen, _params), "Spline1DConvDiff2Cubic: Y contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(x2, n2, _params), "Spline1DConvDiff2Cubic: X2 contains infinite or NAN values!");
        heapsortppoints(ref x, ref y, ref p, n, _params);
        ap.assert(apserv.aredistinct(x, n, _params), "Spline1DConvDiff2Cubic: at least two consequent points are too close!");

        //
        // set up DT (we will need it below)
        //
        dt = new double[Math.Max(n, n2)];

        //
        // sort X2:
        // * use fake array DT because HeapSortPPoints() needs both integer AND real arrays
        // * if we have periodic problem, wrap points
        // * sort them, store permutation at P2
        //
        if (boundrtype == -1 && boundltype == -1)
        {
            for (i = 0; i <= n2 - 1; i++)
            {
                t = x2[i];
                apserv.apperiodicmap(ref t, x[0], x[n - 1], ref t2, _params);
                x2[i] = t;
            }
        }
        heapsortppoints(ref x2, ref dt, ref p2, n2, _params);

        //
        // Now we've checked and preordered everything, so we:
        // * call internal GridDiff() function to get Hermite form of spline
        // * convert using internal Conv() function
        // * convert Y2 back to original order
        //
        spline1dgriddiffcubicinternal(x, ref y, n, boundltype, boundl, boundrtype, boundr, ref d, ref a1, ref a2, ref a3, ref b, ref dt, _params);
        spline1dconvdiffinternal(x, y, d, n, x2, n2, ref y2, true, ref d2, true, ref dd2, true, _params);
        ap.assert(ap.len(dt) >= n2, "Spline1DConvDiff2Cubic: internal error!");
        for (i = 0; i <= n2 - 1; i++)
        {
            dt[p2[i]] = y2[i];
        }
        for (i_ = 0; i_ <= n2 - 1; i_++)
        {
            y2[i_] = dt[i_];
        }
        for (i = 0; i <= n2 - 1; i++)
        {
            dt[p2[i]] = d2[i];
        }
        for (i_ = 0; i_ <= n2 - 1; i_++)
        {
            d2[i_] = dt[i_];
        }
        for (i = 0; i <= n2 - 1; i++)
        {
            dt[p2[i]] = dd2[i];
        }
        for (i_ = 0; i_ <= n2 - 1; i_++)
        {
            dd2[i_] = dt[i_];
        }
    }


    /*************************************************************************
    This subroutine builds Catmull-Rom spline interpolant.

    INPUT PARAMETERS:
        X           -   spline nodes, array[0..N-1].
        Y           -   function values, array[0..N-1].
        
    OPTIONAL PARAMETERS:
        N           -   points count:
                        * N>=2
                        * if given, only first N points are used to build spline
                        * if not given, automatically detected from X/Y sizes
                          (len(X) must be equal to len(Y))
        BoundType   -   boundary condition type:
                        * -1 for periodic boundary condition
                        *  0 for parabolically terminated spline (default)
        Tension     -   tension parameter:
                        * tension=0   corresponds to classic Catmull-Rom spline (default)
                        * 0<tension<1 corresponds to more general form - cardinal spline

    OUTPUT PARAMETERS:
        C           -   spline interpolant


    ORDER OF POINTS

    Subroutine automatically sorts points, so caller may pass unsorted array.

    PROBLEMS WITH PERIODIC BOUNDARY CONDITIONS:

    Problems with periodic boundary conditions have Y[first_point]=Y[last_point].
    However, this subroutine doesn't require you to specify equal  values  for
    the first and last points - it automatically forces them  to  be  equal by
    copying  Y[first_point]  (corresponds  to the leftmost,  minimal  X[])  to
    Y[last_point]. However it is recommended to pass consistent values of Y[],
    i.e. to make Y[first_point]=Y[last_point].

      -- ALGLIB PROJECT --
         Copyright 23.06.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dbuildcatmullrom(double[] x,
        double[] y,
        int n,
        int boundtype,
        double tension,
        spline1dinterpolant c,
        xparams _params)
    {
        double[] d = new double[0];
        int i = 0;

        x = (double[])x.Clone();
        y = (double[])y.Clone();

        ap.assert(n >= 2, "Spline1DBuildCatmullRom: N<2!");
        ap.assert(boundtype == -1 || boundtype == 0, "Spline1DBuildCatmullRom: incorrect BoundType!");
        ap.assert((double)(tension) >= (double)(0), "Spline1DBuildCatmullRom: Tension<0!");
        ap.assert((double)(tension) <= (double)(1), "Spline1DBuildCatmullRom: Tension>1!");
        ap.assert(ap.len(x) >= n, "Spline1DBuildCatmullRom: Length(X)<N!");
        ap.assert(ap.len(y) >= n, "Spline1DBuildCatmullRom: Length(Y)<N!");

        //
        // check and sort points
        //
        ap.assert(apserv.isfinitevector(x, n, _params), "Spline1DBuildCatmullRom: X contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(y, n, _params), "Spline1DBuildCatmullRom: Y contains infinite or NAN values!");
        heapsortpoints(ref x, ref y, n, _params);
        ap.assert(apserv.aredistinct(x, n, _params), "Spline1DBuildCatmullRom: at least two consequent points are too close!");

        //
        // Special cases:
        // * N=2, parabolic terminated boundary condition on both ends
        // * N=2, periodic boundary condition
        //
        if (n == 2 && boundtype == 0)
        {

            //
            // Just linear spline
            //
            spline1dbuildlinear(x, y, n, c, _params);
            return;
        }
        if (n == 2 && boundtype == -1)
        {

            //
            // Same as cubic spline with periodic conditions
            //
            spline1dbuildcubic(x, y, n, -1, 0.0, -1, 0.0, c, _params);
            return;
        }

        //
        // Periodic or non-periodic boundary conditions
        //
        if (boundtype == -1)
        {

            //
            // Periodic boundary conditions
            //
            y[n - 1] = y[0];
            d = new double[n];
            d[0] = (y[1] - y[n - 2]) / (2 * (x[1] - x[0] + x[n - 1] - x[n - 2]));
            for (i = 1; i <= n - 2; i++)
            {
                d[i] = (1 - tension) * (y[i + 1] - y[i - 1]) / (x[i + 1] - x[i - 1]);
            }
            d[n - 1] = d[0];

            //
            // Now problem is reduced to the cubic Hermite spline
            //
            spline1dbuildhermite(x, y, d, n, c, _params);
            c.periodic = true;
        }
        else
        {

            //
            // Non-periodic boundary conditions
            //
            d = new double[n];
            for (i = 1; i <= n - 2; i++)
            {
                d[i] = (1 - tension) * (y[i + 1] - y[i - 1]) / (x[i + 1] - x[i - 1]);
            }
            d[0] = 2 * (y[1] - y[0]) / (x[1] - x[0]) - d[1];
            d[n - 1] = 2 * (y[n - 1] - y[n - 2]) / (x[n - 1] - x[n - 2]) - d[n - 2];

            //
            // Now problem is reduced to the cubic Hermite spline
            //
            spline1dbuildhermite(x, y, d, n, c, _params);
        }
    }


    /*************************************************************************
    This subroutine builds Hermite spline interpolant.

    INPUT PARAMETERS:
        X           -   spline nodes, array[0..N-1]
        Y           -   function values, array[0..N-1]
        D           -   derivatives, array[0..N-1]
        N           -   points count (optional):
                        * N>=2
                        * if given, only first N points are used to build spline
                        * if not given, automatically detected from X/Y sizes
                          (len(X) must be equal to len(Y))

    OUTPUT PARAMETERS:
        C           -   spline interpolant.


    ORDER OF POINTS

    Subroutine automatically sorts points, so caller may pass unsorted array.

      -- ALGLIB PROJECT --
         Copyright 23.06.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dbuildhermite(double[] x,
        double[] y,
        double[] d,
        int n,
        spline1dinterpolant c,
        xparams _params)
    {
        spline1dbuildhermitebuf(x, y, d, n, c, _params);
    }


    /*************************************************************************
    This subroutine builds Hermite spline interpolant.

    Buffered version which reuses memory previously allocated in C as much  as
    possible.

      -- ALGLIB PROJECT --
         Copyright 23.06.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dbuildhermitebuf(double[] x,
        double[] y,
        double[] d,
        int n,
        spline1dinterpolant c,
        xparams _params)
    {
        int i = 0;
        double delta = 0;
        double delta2 = 0;
        double delta3 = 0;

        x = (double[])x.Clone();
        y = (double[])y.Clone();
        d = (double[])d.Clone();

        ap.assert(n >= 2, "Spline1DBuildHermite: N<2!");
        ap.assert(ap.len(x) >= n, "Spline1DBuildHermite: Length(X)<N!");
        ap.assert(ap.len(y) >= n, "Spline1DBuildHermite: Length(Y)<N!");
        ap.assert(ap.len(d) >= n, "Spline1DBuildHermite: Length(D)<N!");

        //
        // check and sort points
        //
        ap.assert(apserv.isfinitevector(x, n, _params), "Spline1DBuildHermite: X contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(y, n, _params), "Spline1DBuildHermite: Y contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(d, n, _params), "Spline1DBuildHermite: D contains infinite or NAN values!");
        heapsortdpoints(ref x, ref y, ref d, n, _params);
        ap.assert(apserv.aredistinct(x, n, _params), "Spline1DBuildHermite: at least two consequent points are too close!");

        //
        // Build
        //
        c.x = new double[n];
        c.c = new double[4 * (n - 1) + 2];
        c.periodic = false;
        c.k = 3;
        c.n = n;
        c.continuity = 1;
        for (i = 0; i <= n - 1; i++)
        {
            c.x[i] = x[i];
        }
        for (i = 0; i <= n - 2; i++)
        {
            delta = x[i + 1] - x[i];
            delta2 = math.sqr(delta);
            delta3 = delta * delta2;
            c.c[4 * i + 0] = y[i];
            c.c[4 * i + 1] = d[i];
            c.c[4 * i + 2] = (3 * (y[i + 1] - y[i]) - 2 * d[i] * delta - d[i + 1] * delta) / delta2;
            c.c[4 * i + 3] = (2 * (y[i] - y[i + 1]) + d[i] * delta + d[i + 1] * delta) / delta3;
        }
        c.c[4 * (n - 1) + 0] = y[n - 1];
        c.c[4 * (n - 1) + 1] = d[n - 1];
    }


    /*************************************************************************
    This subroutine builds Akima spline interpolant

    INPUT PARAMETERS:
        X           -   spline nodes, array[0..N-1]
        Y           -   function values, array[0..N-1]
        N           -   points count (optional):
                        * N>=2
                        * if given, only first N points are used to build spline
                        * if not given, automatically detected from X/Y sizes
                          (len(X) must be equal to len(Y))

    OUTPUT PARAMETERS:
        C           -   spline interpolant


    ORDER OF POINTS

    Subroutine automatically sorts points, so caller may pass unsorted array.

      -- ALGLIB PROJECT --
         Copyright 24.06.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dbuildakima(double[] x,
        double[] y,
        int n,
        spline1dinterpolant c,
        xparams _params)
    {
        int i = 0;
        double[] d = new double[0];
        double[] w = new double[0];
        double[] diff = new double[0];

        x = (double[])x.Clone();
        y = (double[])y.Clone();

        ap.assert(n >= 2, "Spline1DBuildAkima: N<2!");
        ap.assert(ap.len(x) >= n, "Spline1DBuildAkima: Length(X)<N!");
        ap.assert(ap.len(y) >= n, "Spline1DBuildAkima: Length(Y)<N!");

        //
        // check and sort points
        //
        ap.assert(apserv.isfinitevector(x, n, _params), "Spline1DBuildAkima: X contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(y, n, _params), "Spline1DBuildAkima: Y contains infinite or NAN values!");
        heapsortpoints(ref x, ref y, n, _params);
        ap.assert(apserv.aredistinct(x, n, _params), "Spline1DBuildAkima: at least two consequent points are too close!");

        //
        // Handle special cases: N=2, N=3, N=4
        //
        if (n <= 4)
        {
            spline1dbuildcubic(x, y, n, 0, 0.0, 0, 0.0, c, _params);
            return;
        }

        //
        // Prepare W (weights), Diff (divided differences)
        //
        w = new double[n - 1];
        diff = new double[n - 1];
        for (i = 0; i <= n - 2; i++)
        {
            diff[i] = (y[i + 1] - y[i]) / (x[i + 1] - x[i]);
        }
        for (i = 1; i <= n - 2; i++)
        {
            w[i] = Math.Abs(diff[i] - diff[i - 1]);
        }

        //
        // Prepare Hermite interpolation scheme
        //
        d = new double[n];
        for (i = 2; i <= n - 3; i++)
        {
            if ((double)(Math.Abs(w[i - 1]) + Math.Abs(w[i + 1])) != (double)(0))
            {
                d[i] = (w[i + 1] * diff[i - 1] + w[i - 1] * diff[i]) / (w[i + 1] + w[i - 1]);
            }
            else
            {
                d[i] = ((x[i + 1] - x[i]) * diff[i - 1] + (x[i] - x[i - 1]) * diff[i]) / (x[i + 1] - x[i - 1]);
            }
        }
        d[0] = diffthreepoint(x[0], x[0], y[0], x[1], y[1], x[2], y[2], _params);
        d[1] = diffthreepoint(x[1], x[0], y[0], x[1], y[1], x[2], y[2], _params);
        d[n - 2] = diffthreepoint(x[n - 2], x[n - 3], y[n - 3], x[n - 2], y[n - 2], x[n - 1], y[n - 1], _params);
        d[n - 1] = diffthreepoint(x[n - 1], x[n - 3], y[n - 3], x[n - 2], y[n - 2], x[n - 1], y[n - 1], _params);

        //
        // Build Akima spline using Hermite interpolation scheme
        //
        spline1dbuildhermite(x, y, d, n, c, _params);
    }


    /*************************************************************************
    This subroutine calculates the value of the spline at the given point X.

    INPUT PARAMETERS:
        C   -   spline interpolant
        X   -   point

    Result:
        S(x)

      -- ALGLIB PROJECT --
         Copyright 23.06.2007 by Bochkanov Sergey
    *************************************************************************/
    public static double spline1dcalc(spline1dinterpolant c,
        double x,
        xparams _params)
    {
        double result = 0;
        int l = 0;
        int r = 0;
        int m = 0;
        double t = 0;

        ap.assert(c.k == 3, "Spline1DCalc: internal error");
        ap.assert(!Double.IsInfinity(x), "Spline1DCalc: infinite X!");

        //
        // special case: NaN
        //
        if (Double.IsNaN(x))
        {
            result = Double.NaN;
            return result;
        }

        //
        // correct if periodic
        //
        if (c.periodic)
        {
            apserv.apperiodicmap(ref x, c.x[0], c.x[c.n - 1], ref t, _params);
        }

        //
        // Binary search in the [ x[0], ..., x[n-2] ] (x[n-1] is not included)
        //
        l = 0;
        r = c.n - 2 + 1;
        while (l != r - 1)
        {
            m = (l + r) / 2;
            if (c.x[m] >= x)
            {
                r = m;
            }
            else
            {
                l = m;
            }
        }

        //
        // Interpolation
        //
        x = x - c.x[l];
        m = 4 * l;
        result = c.c[m] + x * (c.c[m + 1] + x * (c.c[m + 2] + x * c.c[m + 3]));
        return result;
    }


    /*************************************************************************
    This subroutine differentiates the spline.

    INPUT PARAMETERS:
        C   -   spline interpolant.
        X   -   point

    Result:
        S   -   S(x)
        DS  -   S'(x)
        D2S -   S''(x)

      -- ALGLIB PROJECT --
         Copyright 24.06.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1ddiff(spline1dinterpolant c,
        double x,
        ref double s,
        ref double ds,
        ref double d2s,
        xparams _params)
    {
        int l = 0;
        int r = 0;
        int m = 0;
        double t = 0;

        s = 0;
        ds = 0;
        d2s = 0;

        ap.assert(c.k == 3, "Spline1DDiff: internal error");
        ap.assert(!Double.IsInfinity(x), "Spline1DDiff: infinite X!");

        //
        // special case: NaN
        //
        if (Double.IsNaN(x))
        {
            s = Double.NaN;
            ds = Double.NaN;
            d2s = Double.NaN;
            return;
        }

        //
        // correct if periodic
        //
        if (c.periodic)
        {
            apserv.apperiodicmap(ref x, c.x[0], c.x[c.n - 1], ref t, _params);
        }

        //
        // Binary search
        //
        l = 0;
        r = c.n - 2 + 1;
        while (l != r - 1)
        {
            m = (l + r) / 2;
            if (c.x[m] >= x)
            {
                r = m;
            }
            else
            {
                l = m;
            }
        }

        //
        // Differentiation
        //
        x = x - c.x[l];
        m = 4 * l;
        s = c.c[m] + x * (c.c[m + 1] + x * (c.c[m + 2] + x * c.c[m + 3]));
        ds = c.c[m + 1] + 2 * x * c.c[m + 2] + 3 * math.sqr(x) * c.c[m + 3];
        d2s = 2 * c.c[m + 2] + 6 * x * c.c[m + 3];
    }


    /*************************************************************************
    This subroutine makes the copy of the spline.

    INPUT PARAMETERS:
        C   -   spline interpolant.

    Result:
        CC  -   spline copy

      -- ALGLIB PROJECT --
         Copyright 29.06.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dcopy(spline1dinterpolant c,
        spline1dinterpolant cc,
        xparams _params)
    {
        int s = 0;
        int i_ = 0;

        cc.periodic = c.periodic;
        cc.n = c.n;
        cc.k = c.k;
        cc.continuity = c.continuity;
        cc.x = new double[cc.n];
        for (i_ = 0; i_ <= cc.n - 1; i_++)
        {
            cc.x[i_] = c.x[i_];
        }
        s = ap.len(c.c);
        cc.c = new double[s];
        for (i_ = 0; i_ <= s - 1; i_++)
        {
            cc.c[i_] = c.c[i_];
        }
    }


    /*************************************************************************
    Serializer: allocation

      -- ALGLIB --
         Copyright 16.04.2023 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dalloc(serializer s,
        spline1dinterpolant model,
        xparams _params)
    {

        //
        // Header
        //
        s.alloc_entry();
        s.alloc_entry();

        //
        // Data
        //
        s.alloc_entry();
        s.alloc_entry();
        s.alloc_entry();
        s.alloc_entry();
        apserv.allocrealarray(s, model.x, model.n, _params);
        apserv.allocrealarray(s, model.c, 4 * (model.n - 1) + 2, _params);
    }


    /*************************************************************************
    Serializer: serialization

      -- ALGLIB --
         Copyright 16.04.2023 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dserialize(serializer s,
        spline1dinterpolant model,
        xparams _params)
    {

        //
        // Header
        //
        s.serialize_int(scodes.getspline1dserializationcode(_params));
        s.serialize_int(0);

        //
        // Data
        //
        s.serialize_bool(model.periodic);
        s.serialize_int(model.n);
        s.serialize_int(model.k);
        s.serialize_int(model.continuity);
        apserv.serializerealarray(s, model.x, model.n, _params);
        apserv.serializerealarray(s, model.c, 4 * (model.n - 1) + 2, _params);
    }


    /*************************************************************************
    Serializer: unserialization

      -- ALGLIB --
         Copyright 16.04.2023 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dunserialize(serializer s,
        spline1dinterpolant model,
        xparams _params)
    {
        int k = 0;


        //
        // Header
        //
        k = s.unserialize_int();
        ap.assert(k == scodes.getspline1dserializationcode(_params), "Spline1DUnserialize: stream header corrupted or wrong data supplied to unserializer");
        k = s.unserialize_int();
        ap.assert(k == 0, "Spline1DUnserialize: unsupported spline version");

        //
        // Data
        //
        model.periodic = s.unserialize_bool();
        model.n = s.unserialize_int();
        model.k = s.unserialize_int();
        model.continuity = s.unserialize_int();
        apserv.unserializerealarray(s, ref model.x, _params);
        apserv.unserializerealarray(s, ref model.c, _params);
    }


    /*************************************************************************
    This subroutine unpacks the spline into the coefficients table.

    INPUT PARAMETERS:
        C   -   spline interpolant.
        X   -   point

    OUTPUT PARAMETERS:
        Tbl -   coefficients table, unpacked format, array[0..N-2, 0..5].
                For I = 0...N-2:
                    Tbl[I,0] = X[i]
                    Tbl[I,1] = X[i+1]
                    Tbl[I,2] = C0
                    Tbl[I,3] = C1
                    Tbl[I,4] = C2
                    Tbl[I,5] = C3
                On [x[i], x[i+1]] spline is equals to:
                    S(x) = C0 + C1*t + C2*t^2 + C3*t^3
                    t = x-x[i]
                    
    NOTE:
        You  can rebuild spline with  Spline1DBuildHermite()  function,  which
        accepts as inputs function values and derivatives at nodes, which  are
        easy to calculate when you have coefficients.

      -- ALGLIB PROJECT --
         Copyright 29.06.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dunpack(spline1dinterpolant c,
        ref int n,
        ref double[,] tbl,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        n = 0;
        tbl = new double[0, 0];

        tbl = new double[c.n - 2 + 1, 2 + c.k + 1];
        n = c.n;

        //
        // Fill
        //
        for (i = 0; i <= n - 2; i++)
        {
            tbl[i, 0] = c.x[i];
            tbl[i, 1] = c.x[i + 1];
            for (j = 0; j <= c.k; j++)
            {
                tbl[i, 2 + j] = c.c[(c.k + 1) * i + j];
            }
        }
    }


    /*************************************************************************
    This subroutine performs linear transformation of the spline argument.

    INPUT PARAMETERS:
        C   -   spline interpolant.
        A, B-   transformation coefficients: x = A*t + B
    Result:
        C   -   transformed spline

      -- ALGLIB PROJECT --
         Copyright 30.06.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dlintransx(spline1dinterpolant c,
        double a,
        double b,
        xparams _params)
    {
        int i = 0;
        int n = 0;
        double v = 0;
        double dv = 0;
        double d2v = 0;
        double[] x = new double[0];
        double[] y = new double[0];
        double[] d = new double[0];
        bool isperiodic = new bool();
        int contval = 0;

        ap.assert(c.k == 3, "Spline1DLinTransX: internal error");
        n = c.n;
        x = new double[n];
        y = new double[n];
        d = new double[n];

        //
        // Unpack, X, Y, dY/dX.
        // Scale and pack with Spline1DBuildHermite again.
        //
        if ((double)(a) == (double)(0))
        {

            //
            // Special case: A=0
            //
            v = spline1dcalc(c, b, _params);
            for (i = 0; i <= n - 1; i++)
            {
                x[i] = c.x[i];
                y[i] = v;
                d[i] = 0.0;
            }
        }
        else
        {

            //
            // General case, A<>0
            //
            for (i = 0; i <= n - 1; i++)
            {
                x[i] = c.x[i];
                spline1ddiff(c, x[i], ref v, ref dv, ref d2v, _params);
                x[i] = (x[i] - b) / a;
                y[i] = v;
                d[i] = a * dv;
            }
        }
        isperiodic = c.periodic;
        contval = c.continuity;
        if (contval > 0)
        {
            spline1dbuildhermitebuf(x, y, d, n, c, _params);
        }
        else
        {
            spline1dbuildlinearbuf(x, y, n, c, _params);
        }
        c.periodic = isperiodic;
        c.continuity = contval;
    }


    /*************************************************************************
    This subroutine performs linear transformation of the spline.

    INPUT PARAMETERS:
        C   -   spline interpolant.
        A, B-   transformation coefficients: S2(x) = A*S(x) + B
    Result:
        C   -   transformed spline

      -- ALGLIB PROJECT --
         Copyright 30.06.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dlintransy(spline1dinterpolant c,
        double a,
        double b,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int n = 0;

        ap.assert(c.k == 3, "Spline1DLinTransX: internal error");
        n = c.n;
        for (i = 0; i <= n - 2; i++)
        {
            c.c[4 * i] = a * c.c[4 * i] + b;
            for (j = 1; j <= 3; j++)
            {
                c.c[4 * i + j] = a * c.c[4 * i + j];
            }
        }
        c.c[4 * (n - 1) + 0] = a * c.c[4 * (n - 1) + 0] + b;
        c.c[4 * (n - 1) + 1] = a * c.c[4 * (n - 1) + 1];
    }


    /*************************************************************************
    This subroutine integrates the spline.

    INPUT PARAMETERS:
        C   -   spline interpolant.
        X   -   right bound of the integration interval [a, x],
                here 'a' denotes min(x[])
    Result:
        integral(S(t)dt,a,x)

      -- ALGLIB PROJECT --
         Copyright 23.06.2007 by Bochkanov Sergey
    *************************************************************************/
    public static double spline1dintegrate(spline1dinterpolant c,
        double x,
        xparams _params)
    {
        double result = 0;
        int n = 0;
        int i = 0;
        int j = 0;
        int l = 0;
        int r = 0;
        int m = 0;
        double w = 0;
        double v = 0;
        double t = 0;
        double intab = 0;
        double additionalterm = 0;

        n = c.n;

        //
        // Periodic splines require special treatment. We make
        // following transformation:
        //
        //     integral(S(t)dt,A,X) = integral(S(t)dt,A,Z)+AdditionalTerm
        //
        // here X may lie outside of [A,B], Z lies strictly in [A,B],
        // AdditionalTerm is equals to integral(S(t)dt,A,B) times some
        // integer number (may be zero).
        //
        if (c.periodic && ((double)(x) < (double)(c.x[0]) || (double)(x) > (double)(c.x[c.n - 1])))
        {

            //
            // compute integral(S(x)dx,A,B)
            //
            intab = 0;
            for (i = 0; i <= c.n - 2; i++)
            {
                w = c.x[i + 1] - c.x[i];
                m = (c.k + 1) * i;
                intab = intab + c.c[m] * w;
                v = w;
                for (j = 1; j <= c.k; j++)
                {
                    v = v * w;
                    intab = intab + c.c[m + j] * v / (j + 1);
                }
            }

            //
            // map X into [A,B]
            //
            apserv.apperiodicmap(ref x, c.x[0], c.x[c.n - 1], ref t, _params);
            additionalterm = t * intab;
        }
        else
        {
            additionalterm = 0;
        }

        //
        // Binary search in the [ x[0], ..., x[n-2] ] (x[n-1] is not included)
        //
        l = 0;
        r = n - 2 + 1;
        while (l != r - 1)
        {
            m = (l + r) / 2;
            if ((double)(c.x[m]) >= (double)(x))
            {
                r = m;
            }
            else
            {
                l = m;
            }
        }

        //
        // Integration
        //
        result = 0;
        for (i = 0; i <= l - 1; i++)
        {
            w = c.x[i + 1] - c.x[i];
            m = (c.k + 1) * i;
            result = result + c.c[m] * w;
            v = w;
            for (j = 1; j <= c.k; j++)
            {
                v = v * w;
                result = result + c.c[m + j] * v / (j + 1);
            }
        }
        w = x - c.x[l];
        m = (c.k + 1) * l;
        v = w;
        result = result + c.c[m] * w;
        for (j = 1; j <= c.k; j++)
        {
            v = v * w;
            result = result + c.c[m + j] * v / (j + 1);
        }
        result = result + additionalterm;
        return result;
    }


    /*************************************************************************
    Fitting by the smoothing (penalized) cubic spline.

    This function approximates N scattered points (some of X[] may be equal to
    each other) by the cubic spline with M equidistant nodes spanning interval
    [min(x),max(x)].

    The problem is regularized by adding nonlinearity  penalty  to  the  usual
    least squares penalty function:

        MERIT_FUNC = F_LS + F_NL

    where F_LS is a least squares error  term,  and  F_NL  is  a  nonlinearity
    penalty which is roughly proportional to LambdaNS*integral{ S''(x)^2*dx }.
    Algorithm applies automatic renormalization of F_NL  which  makes  penalty
    term roughly invariant to scaling of X[] and changes in M.

    This function is a new edition  of  penalized  regression  spline fitting,
    a fast and compact one which needs much less resources that  its  previous
    version: just O(maxMN) memory and O(maxMN) time.

    NOTE: it is OK to run this function with both M<<N and M>>N;  say,  it  is
          possible to process 100 points with 1000-node spline.
               
    INPUT PARAMETERS:
        X           -   points, array[0..N-1].
        Y           -   function values, array[0..N-1].
        N           -   number of points (optional):
                        * N>0
                        * if given, only first N elements of X/Y are processed
                        * if not given, automatically determined from lengths
        M           -   number of basis functions ( = number_of_nodes), M>=4.
        LambdaNS    -   LambdaNS>=0, regularization  constant  passed by user.
                        It penalizes nonlinearity in the regression spline.
                        Possible values to start from are 0.00001, 0.1, 1

    OUTPUT PARAMETERS:
        S   -   spline interpolant.
        Rep -   Following fields are set:
                * TerminationType set to 1
                * RMSError      rms error on the (X,Y).
                * AvgError      average error on the (X,Y).
                * AvgRelError   average relative error on the non-zero Y
                * MaxError      maximum error

      -- ALGLIB PROJECT --
         Copyright 10.04.2023 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dfit(double[] x,
        double[] y,
        int n,
        int m,
        double lambdans,
        spline1dinterpolant s,
        spline1dfitreport rep,
        xparams _params)
    {
        double xa = 0;
        double xb = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        int k0 = 0;
        int k1 = 0;
        double v = 0;
        double[] xywork = new double[0];
        double[,] vterm = new double[0, 0];
        double[] sx = new double[0];
        double[] sy = new double[0];
        double[] sdy = new double[0];
        sparse.sparsematrix av = new sparse.sparsematrix();
        sparse.sparsematrix ah = new sparse.sparsematrix();
        sparse.sparsematrix ata = new sparse.sparsematrix();
        double[] targets = new double[0];
        double meany = 0;
        int lsqrcnt = 0;
        int nrel = 0;
        double rss = 0;
        double tss = 0;
        int arows = 0;
        double[] tmp0 = new double[0];
        double[] tmp1 = new double[0];
        double[] tmp2 = new double[0];
        linlsqr.linlsqrstate solver = new linlsqr.linlsqrstate();
        linlsqr.linlsqrreport srep = new linlsqr.linlsqrreport();
        double creg = 0;
        double mxata = 0;
        int bw = 0;
        int[] nzidx0 = new int[0];
        double[] nzval0 = new double[0];
        int nzcnt0 = 0;
        int[] nzidx1 = new int[0];
        double[] nzval1 = new double[0];
        int nzcnt1 = 0;
        int nnz = 0;
        int offs = 0;
        int outrow = 0;
        double scaletargetsby = 0;
        double scalepenaltyby = 0;
        spline1dbbasis basis = new spline1dbbasis();
        bool dotrace = new bool();
        int itidx = 0;
        double[] tmpx = new double[0];
        double[] tmpy = new double[0];
        spline1dinterpolant tmps = new spline1dinterpolant();
        int tstart = 0;
        int t1 = 0;
        double[,] densea = new double[0, 0];
        double[] tau = new double[0];
        double f = 0;
        double df = 0;
        double d2f = 0;

        x = (double[])x.Clone();
        y = (double[])y.Clone();

        ap.assert(n >= 1, "Spline1DFit: N<1!");
        ap.assert(m >= 1, "Spline1DFit: M<1!");
        ap.assert(ap.len(x) >= n, "Spline1DFit: Length(X)<N!");
        ap.assert(ap.len(y) >= n, "Spline1DFit: Length(Y)<N!");
        ap.assert(apserv.isfinitevector(x, n, _params), "Spline1DFit: X contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(y, n, _params), "Spline1DFit: Y contains infinite or NAN values!");
        ap.assert(math.isfinite(lambdans), "Spline1DFit: LambdaNS is infinite!");
        ap.assert((double)(lambdans) >= (double)(0), "Spline1DFit: LambdaNS<0!");
        lsqrcnt = 10;
        m = Math.Max(m, 2);
        scaletargetsby = 1 / Math.Sqrt(n);
        scalepenaltyby = 1 / Math.Sqrt(m);
        dotrace = ap.istraceenabled("SPLINE1D.FIT", _params);
        tstart = 0;
        t1 = 0;
        if (dotrace)
        {
            tstart = unchecked((int)(System.DateTime.UtcNow.Ticks / 10000));
        }

        //
        // Trace
        //
        if (dotrace)
        {
            ap.trace("\n\n");
            ap.trace("////////////////////////////////////////////////////////////////////////////////////////////////////\n");
            ap.trace("//  SPLINE 1D FITTING STARTED                                                                     //\n");
            ap.trace("////////////////////////////////////////////////////////////////////////////////////////////////////\n");
            ap.trace(System.String.Format("N             = {0,9:d}    (points)\n", n));
            ap.trace(System.String.Format("M             = {0,9:d}    (nodes)\n", m));
            ap.trace(System.String.Format("LambdaNS      = {0,9:E3}  (penalty)\n", lambdans));
        }

        //
        // Determine actual area size, make sure that XA<XB
        //
        if (dotrace)
        {
            t1 = unchecked((int)(System.DateTime.UtcNow.Ticks / 10000));
        }
        xa = x[0];
        xb = x[0];
        for (i = 1; i <= n - 1; i++)
        {
            xa = Math.Min(xa, x[i]);
            xb = Math.Max(xb, x[i]);
        }
        if ((double)(xa) == (double)(xb))
        {
            v = xa;
            if ((double)(v) >= (double)(0))
            {
                xa = v / 2 - 1;
                xb = v * 2 + 1;
            }
            else
            {
                xa = v * 2 - 1;
                xb = v / 2 + 1;
            }
        }
        ap.assert((double)(xa) < (double)(xb), "Spline1DFit: integrity error");
        if (dotrace)
        {
            ap.trace(System.String.Format("> dataset sorted in {0,0:d} ms, interpolation interval determined to be [{1,0:E6},{2,0:E6}]\n", unchecked((int)(System.DateTime.UtcNow.Ticks / 10000)) - t1, xa, xb));
        }

        //
        // Convert X/Y to work representation, remove linear trend (in
        // order to improve condition number).
        //
        // Compute total-sum-of-squares (needed later for R2 coefficient).
        //
        xywork = new double[2 * n];
        for (i = 0; i <= n - 1; i++)
        {
            xywork[2 * i + 0] = (x[i] - xa) / (xb - xa);
            xywork[2 * i + 1] = y[i];
        }
        intfitserv.buildpriorterm1(xywork, n, 1, 1, 1, 0.0, ref vterm, _params);
        meany = 0;
        for (i = 0; i <= n - 1; i++)
        {
            meany = meany + y[i];
        }
        meany = meany / n;
        tss = 0;
        for (i = 0; i <= n - 1; i++)
        {
            tss = tss + math.sqr(y[i] - meany);
        }

        //
        // Depending on M, use either general purpose algorithm that needs M>=4 or a compact one
        //
        arows = n + m + m;
        if (m >= 4)
        {

            //
            // Generate design matrix and targets
            //
            if (dotrace)
            {
                t1 = unchecked((int)(System.DateTime.UtcNow.Ticks / 10000));
            }
            ablasf.rsetallocv(arows, 0.0, ref targets, _params);
            bbasisinit(basis, m, _params);
            nnz = (n + m) * 2 * basis.bfrad + m;
            av.m = arows;
            av.n = m;
            ablasf.iallocv(av.m + 1, ref av.ridx, _params);
            ablasf.iallocv(nnz, ref av.idx, _params);
            ablasf.rallocv(nnz, ref av.vals, _params);
            av.ridx[0] = 0;
            offs = 0;
            outrow = 0;
            for (i = 0; i <= n - 1; i++)
            {

                //
                // Generate design matrix row #I which corresponds to I-th dataset point
                //
                k = (int)Math.Floor(apserv.boundval(xywork[2 * i + 0] * (m - 1), 0, m - 1, _params));
                k0 = Math.Max(k - (basis.bfrad - 1), 0);
                k1 = Math.Min(k + 1 + (basis.bfrad - 1), m - 1);
                for (j = k0; j <= k1; j++)
                {
                    av.idx[offs] = j;
                    av.vals[offs] = basiscalc(basis, j, xywork[2 * i + 0], _params) * scaletargetsby;
                    offs = offs + 1;
                }
                targets[i] = xywork[2 * i + 1] * scaletargetsby;
                av.ridx[outrow + 1] = offs;
                outrow = outrow + 1;
            }
            for (i = 0; i <= m - 1; i++)
            {

                //
                // Generate design matrix row #(I+N) which corresponds to nonlinearity penalty at I-th node
                //
                k0 = Math.Max(i - (basis.bfrad - 1), 0);
                k1 = Math.Min(i + (basis.bfrad - 1), m - 1);
                for (j = k0; j <= k1; j++)
                {
                    av.idx[offs] = j;
                    av.vals[offs] = basisdiff2(basis, j, (double)i / (double)(m - 1), _params) * scalepenaltyby * lambdans;
                    offs = offs + 1;
                }
                av.ridx[outrow + 1] = offs;
                outrow = outrow + 1;
            }
            for (i = 0; i <= m - 1; i++)
            {

                //
                // Generate design matrix row #(I+N+M) which corresponds to regularization for I-th coefficient
                //
                av.idx[offs] = i;
                av.vals[offs] = lambdareg;
                offs = offs + 1;
                av.ridx[outrow + 1] = offs;
                outrow = outrow + 1;
            }
            ap.assert(outrow == av.m && offs <= nnz, "SPLINE1DFIT: integrity check 6606 failed");
            sparse.sparsecreatecrsinplace(av, _params);
            sparse.sparsecopytransposecrs(av, ah, _params);
            if (dotrace)
            {
                ap.trace(System.String.Format("> design matrix generated in {0,0:d} ms, {1,0:d} nonzeros\n", unchecked((int)(System.DateTime.UtcNow.Ticks / 10000)) - t1, av.ridx[av.m]));
            }

            //
            // Build 7-diagonal (bandwidth=3) normal equations matrix and perform Cholesky
            // decomposition (to be used later as preconditioner for LSQR iterations).
            //
            if (dotrace)
            {
                t1 = unchecked((int)(System.DateTime.UtcNow.Ticks / 10000));
            }
            bw = 2 * (basis.bfrad - 1);
            sparse.sparsecreatesksband(m, m, bw, ata, _params);
            mxata = 0;
            for (i = 0; i <= m - 1; i++)
            {
                for (j = i; j <= Math.Min(i + bw, m - 1); j++)
                {

                    //
                    // Get pattern of nonzeros in one of the rows (let it be I-th one)
                    // and compute dot product only for nonzero entries.
                    //
                    sparse.sparsegetcompressedrow(ah, i, ref nzidx0, ref nzval0, ref nzcnt0, _params);
                    sparse.sparsegetcompressedrow(ah, j, ref nzidx1, ref nzval1, ref nzcnt1, _params);
                    v = 0;
                    k0 = 0;
                    k1 = 0;
                    while (true)
                    {
                        if (k0 == nzcnt0)
                        {
                            break;
                        }
                        if (k1 == nzcnt1)
                        {
                            break;
                        }
                        if (nzidx0[k0] < nzidx1[k1])
                        {
                            k0 = k0 + 1;
                            continue;
                        }
                        if (nzidx0[k0] > nzidx1[k1])
                        {
                            k1 = k1 + 1;
                            continue;
                        }
                        v = v + nzval0[k0] * nzval1[k1];
                        k0 = k0 + 1;
                        k1 = k1 + 1;
                    }

                    //
                    // Update ATA and max(ATA)
                    //
                    sparse.sparseset(ata, i, j, v, _params);
                    if (i == j)
                    {
                        mxata = Math.Max(mxata, Math.Abs(v));
                    }
                }
            }
            mxata = apserv.coalesce(mxata, 1.0, _params);
            creg = cholreg;
            while (true)
            {

                //
                // Regularization
                //
                for (i = 0; i <= m - 1; i++)
                {
                    sparse.sparseset(ata, i, i, sparse.sparseget(ata, i, i, _params) + mxata * creg, _params);
                }

                //
                // Try Cholesky factorization.
                //
                if (!trfac.sparsecholeskyskyline(ata, m, true, _params))
                {

                    //
                    // Factorization failed, increase regularizer and repeat
                    //
                    creg = apserv.coalesce(10 * creg, 1.0E-12, _params);
                    if (dotrace)
                    {
                        ap.trace(System.String.Format("> preconditioner factorization failed, increasing regularization to {0,0:E2}\n", creg));
                    }
                    continue;
                }
                break;
            }
            if (dotrace)
            {
                ap.trace(System.String.Format("> preconditioner generated in {0,0:d} ms\n", unchecked((int)(System.DateTime.UtcNow.Ticks / 10000)) - t1));
            }

            //
            // Solve with preconditioned LSQR:
            //
            // use Cholesky factor U of squared design matrix A'*A to
            // transform min|A*x-b| to min|[A*inv(U)]*y-b| with y=U*x.
            //
            // Preconditioned problem is solved with LSQR solver, which
            // gives superior results to normal equations approach. Due
            // to Cholesky preconditioner being utilized we can solve
            // problem in just a few iterations.
            //
            ablasf.rallocv(arows, ref tmp0, _params);
            ablasf.rallocv(m, ref tmp1, _params);
            ablasf.rallocv(m, ref tmp2, _params);
            linlsqr.linlsqrcreatebuf(arows, m, solver, _params);
            linlsqr.linlsqrsetb(solver, targets, _params);
            linlsqr.linlsqrsetcond(solver, 1000 * math.machineepsilon, 1000 * math.machineepsilon, lsqrcnt, _params);
            if (dotrace)
            {
                t1 = unchecked((int)(System.DateTime.UtcNow.Ticks / 10000));
                ap.trace("> starting LSQR iterations:\n");
                linlsqr.linlsqrsetxrep(solver, true, _params);
            }
            itidx = 0;
            while (linlsqr.linlsqriteration(solver, _params))
            {
                if (solver.needmv)
                {
                    for (i = 0; i <= m - 1; i++)
                    {
                        tmp1[i] = solver.x[i];
                    }

                    //
                    // Use Cholesky factorization of the system matrix
                    // as preconditioner: solve TRSV(U,Solver.X)
                    //
                    sparse.sparsetrsv(ata, true, false, 0, tmp1, _params);

                    //
                    // After preconditioning is done, multiply by A
                    //
                    sparse.sparsemv(av, tmp1, ref solver.mv, _params);
                    continue;
                }
                if (solver.needmtv)
                {

                    //
                    // Multiply by design matrix A
                    //
                    sparse.sparsemtv(av, solver.x, ref solver.mtv, _params);

                    //
                    // Multiply by preconditioner: solve TRSV(U',A*Solver.X)
                    //
                    sparse.sparsetrsv(ata, true, false, 1, solver.mtv, _params);
                    continue;
                }
                if (solver.xupdated)
                {

                    //
                    // Compute squared residual for normal equations system (more meaningful reports)
                    //
                    if (dotrace)
                    {

                        //
                        // Compute A*x
                        //
                        ablasf.rcopyv(m, solver.x, tmp2, _params);
                        sparse.sparsetrsv(ata, true, false, 0, tmp2, _params);
                        sparse.sparsemv(av, tmp2, ref tmp0, _params);

                        //
                        // Compute residual r
                        //
                        ablasf.raddv(arows, -1.0, targets, tmp0, _params);

                        //
                        // Compute A'*r
                        //
                        sparse.sparsemtv(av, tmp0, ref tmp1, _params);
                        sparse.sparsetrsv(ata, true, false, 1, tmp1, _params);
                        ap.trace(System.String.Format(">> iteration {0,0:d},  |r|={1,0:E2},  |(A^T)*r|={2,0:E2}\n", itidx, Math.Sqrt(ablasf.rdotv2(arows, tmp0, _params)), Math.Sqrt(ablasf.rdotv2(m, tmp1, _params))));
                    }
                    itidx = itidx + 1;
                    continue;
                }
                ap.assert(false, "SPLINE1D: integrity check 6344 failed");
            }
            linlsqr.linlsqrresults(solver, ref tmp1, srep, _params);
            sparse.sparsetrsv(ata, true, false, 0, tmp1, _params);
            if (dotrace)
            {
                ap.trace(System.String.Format(">> solved in {0,0:d} ms, solution norm is {1,0:E2}\n", unchecked((int)(System.DateTime.UtcNow.Ticks / 10000)) - t1, Math.Sqrt(ablasf.rdotv2(m, tmp1, _params))));
            }

            //
            // Convert from B-basis to C2-continuous Hermite spline
            //
            ablasf.rallocv(m, ref sx, _params);
            for (i = 0; i <= m - 1; i++)
            {
                sx[i] = (double)i / (double)(m - 1);
            }
            ablasf.rsetallocv(m, 0.0, ref sy, _params);
            ablasf.rsetallocv(m, 0.0, ref sdy, _params);
            for (i = 0; i <= m - 1; i++)
            {
                for (j = Math.Max(i - (basis.bfrad - 1), 0); j <= Math.Min(i + (basis.bfrad - 1), m - 1); j++)
                {
                    sy[j] = sy[j] + tmp1[i] * basiscalc(basis, i, (double)j / (double)(m - 1), _params);
                    sdy[j] = sdy[j] + tmp1[i] * basisdiff(basis, i, (double)j / (double)(m - 1), _params);
                }
            }
            spline1dbuildhermite(sx, sy, sdy, m, tmps, _params);
        }
        else
        {

            //
            // A special purpose algorithm for M<4
            //
            ablasf.rsetallocm(arows, m + 1, 0.0, ref densea, _params);
            for (i = 0; i <= n - 1; i++)
            {
                densea[i, m] = xywork[2 * i + 1] * scaletargetsby;
            }
            ablasf.rallocv(m, ref tmpx, _params);
            for (i = 0; i <= m - 1; i++)
            {
                tmpx[i] = (double)i / (double)(m - 1);
            }
            for (j = 0; j <= m - 1; j++)
            {
                ablasf.rsetallocv(m, 0.0, ref tmpy, _params);
                tmpy[j] = 1;
                spline1dbuildcubic(tmpx, tmpy, m, 2, 0.0, 2, 0.0, tmps, _params);
                for (i = 0; i <= n - 1; i++)
                {
                    densea[i, j] = spline1dcalc(tmps, xywork[2 * i + 0], _params) * scaletargetsby;
                }
                for (i = 0; i <= m - 1; i++)
                {
                    spline1ddiff(tmps, (double)i / (double)(m - 1), ref f, ref df, ref d2f, _params);
                    densea[n + i, j] = d2f * scalepenaltyby * lambdans;
                }
            }
            for (i = 0; i <= m - 1; i++)
            {
                densea[n + m + i, i] = lambdareg;
            }
            ortfac.rmatrixqr(densea, arows, m + 1, ref tau, _params);
            if (dotrace)
            {
                ap.trace(System.String.Format("> running low-dimensional QR-based algorithm, |r|={0,0:E2}\n", Math.Abs(densea[m, m])));
            }
            ablasf.rallocv(m, ref sy, _params);
            ablasf.rcopycv(m, densea, m, sy, _params);
            ablas.rmatrixtrsv(m, densea, 0, 0, true, false, 0, sy, 0, _params);
            ablasf.rallocv(m, ref sx, _params);
            for (i = 0; i <= m - 1; i++)
            {
                sx[i] = (double)i / (double)(m - 1);
            }
            spline1dbuildcubic(sx, sy, m, 2, 0.0, 2, 0.0, tmps, _params);
            ablasf.rallocv(m, ref sdy, _params);
            for (i = 0; i <= m - 1; i++)
            {
                spline1ddiff(tmps, (double)i / (double)(m - 1), ref f, ref df, ref d2f, _params);
                sdy[i] = df;
            }
        }

        //
        // Calculate model values, report
        //
        rss = 0.0;
        nrel = 0;
        rep.terminationtype = 1;
        rep.rmserror = 0;
        rep.maxerror = 0;
        rep.avgerror = 0;
        rep.avgrelerror = 0;
        for (i = 0; i <= n - 1; i++)
        {
            v = spline1dcalc(tmps, xywork[2 * i + 0], _params) - xywork[2 * i + 1];
            rss = rss + v * v;
            rep.rmserror = rep.rmserror + math.sqr(v);
            rep.avgerror = rep.avgerror + Math.Abs(v);
            rep.maxerror = Math.Max(rep.maxerror, Math.Abs(v));
            if ((double)(y[i]) != (double)(0))
            {
                rep.avgrelerror = rep.avgrelerror + Math.Abs(v / y[i]);
                nrel = nrel + 1;
            }
        }
        rep.rmserror = Math.Sqrt(rep.rmserror / n);
        rep.avgerror = rep.avgerror / n;
        rep.avgrelerror = rep.avgrelerror / apserv.coalesce(nrel, 1.0, _params);
        if (dotrace)
        {
            ap.trace("> printing fitting errors:\n");
            ap.trace(System.String.Format("rms.err       = {0,9:E3}\n", rep.rmserror));
            ap.trace(System.String.Format("max.err       = {0,9:E3}\n", rep.maxerror));
            ap.trace(System.String.Format("avg.err       = {0,9:E3}\n", rep.avgerror));
            ap.trace(System.String.Format("avg.rel.err   = {0,9:E3}\n", rep.avgrelerror));
            ap.trace(System.String.Format("R2            = {0,0:F6}  (coefficient of determination)\n", 1.0 - rss / apserv.coalesce(tss, 1.0, _params)));
        }

        //
        // Append prior term.
        // Transform spline to original coordinates.
        // Output.
        //
        for (i = 0; i <= m - 1; i++)
        {
            sy[i] = sy[i] + vterm[0, 0] * sx[i] + vterm[0, 1];
            sdy[i] = sdy[i] + vterm[0, 0];
        }
        for (i = 0; i <= m - 1; i++)
        {
            sx[i] = sx[i] * (xb - xa) + xa;
            sdy[i] = sdy[i] / (xb - xa);
        }
        spline1dbuildhermite(sx, sy, sdy, m, s, _params);

        //
        // Done
        //
        if (dotrace)
        {
            ap.trace(System.String.Format("> done in {0,0:d} ms\n", unchecked((int)(System.DateTime.UtcNow.Ticks / 10000)) - tstart));
        }
    }


    /*************************************************************************
    Internal version of Spline1DConvDiff

    Converts from Hermite spline given by grid XOld to new grid X2

    INPUT PARAMETERS:
        XOld    -   old grid
        YOld    -   values at old grid
        DOld    -   first derivative at old grid
        N       -   grid size
        X2      -   new grid
        N2      -   new grid size
        Y       -   possibly preallocated output array
                    (reallocate if too small)
        NeedY   -   do we need Y?
        D1      -   possibly preallocated output array
                    (reallocate if too small)
        NeedD1  -   do we need D1?
        D2      -   possibly preallocated output array
                    (reallocate if too small)
        NeedD2  -   do we need D1?

    OUTPUT ARRAYS:
        Y       -   values, if needed
        D1      -   first derivative, if needed
        D2      -   second derivative, if needed

      -- ALGLIB PROJECT --
         Copyright 03.09.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dconvdiffinternal(double[] xold,
        double[] yold,
        double[] dold,
        int n,
        double[] x2,
        int n2,
        ref double[] y,
        bool needy,
        ref double[] d1,
        bool needd1,
        ref double[] d2,
        bool needd2,
        xparams _params)
    {
        int intervalindex = 0;
        int pointindex = 0;
        bool havetoadvance = new bool();
        double c0 = 0;
        double c1 = 0;
        double c2 = 0;
        double c3 = 0;
        double a = 0;
        double b = 0;
        double w = 0;
        double w2 = 0;
        double w3 = 0;
        double fa = 0;
        double fb = 0;
        double da = 0;
        double db = 0;
        double t = 0;


        //
        // Prepare space
        //
        if (needy && ap.len(y) < n2)
        {
            y = new double[n2];
        }
        if (needd1 && ap.len(d1) < n2)
        {
            d1 = new double[n2];
        }
        if (needd2 && ap.len(d2) < n2)
        {
            d2 = new double[n2];
        }

        //
        // These assignments aren't actually needed
        // (variables are initialized in the loop below),
        // but without them compiler will complain about uninitialized locals
        //
        c0 = 0;
        c1 = 0;
        c2 = 0;
        c3 = 0;
        a = 0;
        b = 0;

        //
        // Cycle
        //
        intervalindex = -1;
        pointindex = 0;
        while (true)
        {

            //
            // are we ready to exit?
            //
            if (pointindex >= n2)
            {
                break;
            }
            t = x2[pointindex];

            //
            // do we need to advance interval?
            //
            havetoadvance = false;
            if (intervalindex == -1)
            {
                havetoadvance = true;
            }
            else
            {
                if (intervalindex < n - 2)
                {
                    havetoadvance = (double)(t) >= (double)(b);
                }
            }
            if (havetoadvance)
            {
                intervalindex = intervalindex + 1;
                a = xold[intervalindex];
                b = xold[intervalindex + 1];
                w = b - a;
                w2 = w * w;
                w3 = w * w2;
                fa = yold[intervalindex];
                fb = yold[intervalindex + 1];
                da = dold[intervalindex];
                db = dold[intervalindex + 1];
                c0 = fa;
                c1 = da;
                c2 = (3 * (fb - fa) - 2 * da * w - db * w) / w2;
                c3 = (2 * (fa - fb) + da * w + db * w) / w3;
                continue;
            }

            //
            // Calculate spline and its derivatives using power basis
            //
            t = t - a;
            if (needy)
            {
                y[pointindex] = c0 + t * (c1 + t * (c2 + t * c3));
            }
            if (needd1)
            {
                d1[pointindex] = c1 + 2 * t * c2 + 3 * t * t * c3;
            }
            if (needd2)
            {
                d2[pointindex] = 2 * c2 + 6 * t * c3;
            }
            pointindex = pointindex + 1;
        }
    }


    /*************************************************************************
    This function finds all roots and extrema of the spline S(x)  defined  at
    [A,B] (interval which contains spline nodes).

    It  does not extrapolates function, so roots and extrema located  outside 
    of [A,B] will not be found. It returns all isolated (including  multiple)
    roots and extrema.

    INPUT PARAMETERS
        C           -   spline interpolant
        
    OUTPUT PARAMETERS
        R           -   array[NR], contains roots of the spline. 
                        In case there is no roots, this array has zero length.
        NR          -   number of roots, >=0
        DR          -   is set to True in case there is at least one interval
                        where spline is just a zero constant. Such degenerate
                        cases are not reported in the R/NR
        E           -   array[NE], contains  extrema  (maximums/minimums)  of 
                        the spline. In case there is no extrema,  this  array 
                        has zero length.
        ET          -   array[NE], extrema types:
                        * ET[i]>0 in case I-th extrema is a minimum
                        * ET[i]<0 in case I-th extrema is a maximum
        NE          -   number of extrema, >=0
        DE          -   is set to True in case there is at least one interval
                        where spline is a constant. Such degenerate cases are  
                        not reported in the E/NE.
                        
    NOTES:

    1. This function does NOT report following kinds of roots:
       * intervals where function is constantly zero
       * roots which are outside of [A,B] (note: it CAN return A or B)

    2. This function does NOT report following kinds of extrema:
       * intervals where function is a constant
       * extrema which are outside of (A,B) (note: it WON'T return A or B)
       
     -- ALGLIB PROJECT --
         Copyright 26.09.2011 by Bochkanov Sergey   
    *************************************************************************/
    public static void spline1drootsandextrema(spline1dinterpolant c,
        ref double[] r,
        ref int nr,
        ref bool dr,
        ref double[] e,
        ref int[] et,
        ref int ne,
        ref bool de,
        xparams _params)
    {
        double pl = 0;
        double ml = 0;
        double pll = 0;
        double pr = 0;
        double mr = 0;
        double[] tr = new double[0];
        double[] tmpr = new double[0];
        double[] tmpe = new double[0];
        int[] tmpet = new int[0];
        double[] tmpc = new double[0];
        double x0 = 0;
        double x1 = 0;
        double x2 = 0;
        double ex0 = 0;
        double ex1 = 0;
        int tne = 0;
        int tnr = 0;
        int i = 0;
        int j = 0;
        bool nstep = new bool();

        r = new double[0];
        nr = 0;
        dr = new bool();
        e = new double[0];
        et = new int[0];
        ne = 0;
        de = new bool();


        //
        //exception handling
        //
        ap.assert(c.k == 3, "Spline1DRootsAndExtrema : incorrect parameter C.K!");
        ap.assert(c.continuity >= 0, "Spline1DRootsAndExtrema : parameter C.Continuity must not be less than 0!");

        //
        //initialization of variable
        //
        nr = 0;
        ne = 0;
        dr = false;
        de = false;
        nstep = true;

        //
        //consider case, when C.Continuty=0
        //
        if (c.continuity == 0)
        {

            //
            //allocation for auxiliary arrays 
            //'TmpR ' - it stores a time value for roots
            //'TmpE ' - it stores a time value for extremums
            //'TmpET '- it stores a time value for extremums type
            //
            apserv.rvectorsetlengthatleast(ref tmpr, 3 * (c.n - 1), _params);
            apserv.rvectorsetlengthatleast(ref tmpe, 2 * (c.n - 1), _params);
            apserv.ivectorsetlengthatleast(ref tmpet, 2 * (c.n - 1), _params);

            //
            //start calculating
            //
            for (i = 0; i <= c.n - 2; i++)
            {

                //
                //initialization pL, mL, pR, mR
                //
                pl = c.c[4 * i];
                ml = c.c[4 * i + 1];
                pr = c.c[4 * (i + 1)];
                mr = c.c[4 * i + 1] + 2 * c.c[4 * i + 2] * (c.x[i + 1] - c.x[i]) + 3 * c.c[4 * i + 3] * (c.x[i + 1] - c.x[i]) * (c.x[i + 1] - c.x[i]);

                //
                //pre-searching roots and extremums
                //
                solvecubicpolinom(pl, ml, pr, mr, c.x[i], c.x[i + 1], ref x0, ref x1, ref x2, ref ex0, ref ex1, ref tnr, ref tne, ref tr, _params);
                dr = dr || tnr == -1;
                de = de || tne == -1;

                //
                //searching of roots
                //
                if (tnr == 1 && nstep)
                {

                    //
                    //is there roots?
                    //
                    if (nr > 0)
                    {

                        //
                        //is a next root equal a previous root?
                        //if is't, then write new root
                        //
                        if ((double)(x0) != (double)(tmpr[nr - 1]))
                        {
                            tmpr[nr] = x0;
                            nr = nr + 1;
                        }
                    }
                    else
                    {

                        //
                        //write a first root
                        //
                        tmpr[nr] = x0;
                        nr = nr + 1;
                    }
                }
                else
                {

                    //
                    //case when function at a segment identically to zero
                    //then we have to clear a root, if the one located on a 
                    //constant segment
                    //
                    if (tnr == -1)
                    {

                        //
                        //safe state variable as constant
                        //
                        if (nstep)
                        {
                            nstep = false;
                        }

                        //
                        //clear the root, if there is
                        //
                        if (nr > 0)
                        {
                            if ((double)(c.x[i]) == (double)(tmpr[nr - 1]))
                            {
                                nr = nr - 1;
                            }
                        }

                        //
                        //change state for 'DR'
                        //
                        if (!dr)
                        {
                            dr = true;
                        }
                    }
                    else
                    {
                        nstep = true;
                    }
                }

                //
                //searching of extremums
                //
                if (i > 0)
                {
                    pll = c.c[4 * (i - 1)];

                    //
                    //if pL=pLL or pL=pR then
                    //
                    if (tne == -1)
                    {
                        if (!de)
                        {
                            de = true;
                        }
                    }
                    else
                    {
                        if ((double)(pl) > (double)(pll) && (double)(pl) > (double)(pr))
                        {

                            //
                            //maximum
                            //
                            tmpet[ne] = -1;
                            tmpe[ne] = c.x[i];
                            ne = ne + 1;
                        }
                        else
                        {
                            if ((double)(pl) < (double)(pll) && (double)(pl) < (double)(pr))
                            {

                                //
                                //minimum
                                //
                                tmpet[ne] = 1;
                                tmpe[ne] = c.x[i];
                                ne = ne + 1;
                            }
                        }
                    }
                }
            }

            //
            //write final result
            //
            apserv.rvectorsetlengthatleast(ref r, nr, _params);
            apserv.rvectorsetlengthatleast(ref e, ne, _params);
            apserv.ivectorsetlengthatleast(ref et, ne, _params);

            //
            //write roots
            //
            for (i = 0; i <= nr - 1; i++)
            {
                r[i] = tmpr[i];
            }

            //
            //write extremums and their types
            //
            for (i = 0; i <= ne - 1; i++)
            {
                e[i] = tmpe[i];
                et[i] = tmpet[i];
            }
        }
        else
        {

            //
            //case, when C.Continuity>=1 
            //'TmpR ' - it stores a time value for roots
            //'TmpC' - it stores a time value for extremums and 
            //their function value (TmpC={EX0,F(EX0), EX1,F(EX1), ..., EXn,F(EXn)};)
            //'TmpE' - it stores a time value for extremums only
            //'TmpET'- it stores a time value for extremums type
            //
            apserv.rvectorsetlengthatleast(ref tmpr, 2 * c.n - 1, _params);
            apserv.rvectorsetlengthatleast(ref tmpc, 4 * c.n, _params);
            apserv.rvectorsetlengthatleast(ref tmpe, 2 * c.n, _params);
            apserv.ivectorsetlengthatleast(ref tmpet, 2 * c.n, _params);

            //
            //start calculating
            //
            for (i = 0; i <= c.n - 2; i++)
            {

                //
                //we calculate pL,mL, pR,mR as Fi+1(F'i+1) at left border
                //
                pl = c.c[4 * i];
                ml = c.c[4 * i + 1];
                pr = c.c[4 * (i + 1)];
                mr = c.c[4 * (i + 1) + 1];

                //
                //calculating roots and extremums at [X[i],X[i+1]]
                //
                solvecubicpolinom(pl, ml, pr, mr, c.x[i], c.x[i + 1], ref x0, ref x1, ref x2, ref ex0, ref ex1, ref tnr, ref tne, ref tr, _params);

                //
                //searching roots
                //
                if (tnr > 0)
                {

                    //
                    //re-init tR
                    //
                    if (tnr >= 1)
                    {
                        tr[0] = x0;
                    }
                    if (tnr >= 2)
                    {
                        tr[1] = x1;
                    }
                    if (tnr == 3)
                    {
                        tr[2] = x2;
                    }

                    //
                    //start root selection
                    //
                    if (nr > 0)
                    {
                        if ((double)(tmpr[nr - 1]) != (double)(x0))
                        {

                            //
                            //previous segment was't constant identical zero
                            //
                            if (nstep)
                            {
                                for (j = 0; j <= tnr - 1; j++)
                                {
                                    tmpr[nr + j] = tr[j];
                                }
                                nr = nr + tnr;
                            }
                            else
                            {

                                //
                                //previous segment was constant identical zero
                                //and we must ignore [NR+j-1] root
                                //
                                for (j = 1; j <= tnr - 1; j++)
                                {
                                    tmpr[nr + j - 1] = tr[j];
                                }
                                nr = nr + tnr - 1;
                                nstep = true;
                            }
                        }
                        else
                        {
                            for (j = 1; j <= tnr - 1; j++)
                            {
                                tmpr[nr + j - 1] = tr[j];
                            }
                            nr = nr + tnr - 1;
                        }
                    }
                    else
                    {

                        //
                        //write first root
                        //
                        for (j = 0; j <= tnr - 1; j++)
                        {
                            tmpr[nr + j] = tr[j];
                        }
                        nr = nr + tnr;
                    }
                }
                else
                {
                    if (tnr == -1)
                    {

                        //
                        //decrement 'NR' if at previous step was writen a root
                        //(previous segment identical zero)
                        //
                        if (nr > 0 && nstep)
                        {
                            nr = nr - 1;
                        }

                        //
                        //previous segment is't constant
                        //
                        if (nstep)
                        {
                            nstep = false;
                        }

                        //
                        //rewrite 'DR'
                        //
                        if (!dr)
                        {
                            dr = true;
                        }
                    }
                }

                //
                //searching extremums
                //write all term like extremums
                //
                if (tne == 1)
                {
                    if (ne > 0)
                    {

                        //
                        //just ignore identical extremums
                        //because he must be one
                        //
                        if ((double)(tmpc[ne - 2]) != (double)(ex0))
                        {
                            tmpc[ne] = ex0;
                            tmpc[ne + 1] = c.c[4 * i] + c.c[4 * i + 1] * (ex0 - c.x[i]) + c.c[4 * i + 2] * (ex0 - c.x[i]) * (ex0 - c.x[i]) + c.c[4 * i + 3] * (ex0 - c.x[i]) * (ex0 - c.x[i]) * (ex0 - c.x[i]);
                            ne = ne + 2;
                        }
                    }
                    else
                    {

                        //
                        //write first extremum and it function value
                        //
                        tmpc[ne] = ex0;
                        tmpc[ne + 1] = c.c[4 * i] + c.c[4 * i + 1] * (ex0 - c.x[i]) + c.c[4 * i + 2] * (ex0 - c.x[i]) * (ex0 - c.x[i]) + c.c[4 * i + 3] * (ex0 - c.x[i]) * (ex0 - c.x[i]) * (ex0 - c.x[i]);
                        ne = ne + 2;
                    }
                }
                else
                {
                    if (tne == 2)
                    {
                        if (ne > 0)
                        {

                            //
                            //ignore identical extremum
                            //
                            if ((double)(tmpc[ne - 2]) != (double)(ex0))
                            {
                                tmpc[ne] = ex0;
                                tmpc[ne + 1] = c.c[4 * i] + c.c[4 * i + 1] * (ex0 - c.x[i]) + c.c[4 * i + 2] * (ex0 - c.x[i]) * (ex0 - c.x[i]) + c.c[4 * i + 3] * (ex0 - c.x[i]) * (ex0 - c.x[i]) * (ex0 - c.x[i]);
                                ne = ne + 2;
                            }
                        }
                        else
                        {

                            //
                            //write first extremum
                            //
                            tmpc[ne] = ex0;
                            tmpc[ne + 1] = c.c[4 * i] + c.c[4 * i + 1] * (ex0 - c.x[i]) + c.c[4 * i + 2] * (ex0 - c.x[i]) * (ex0 - c.x[i]) + c.c[4 * i + 3] * (ex0 - c.x[i]) * (ex0 - c.x[i]) * (ex0 - c.x[i]);
                            ne = ne + 2;
                        }

                        //
                        //write second extremum
                        //
                        tmpc[ne] = ex1;
                        tmpc[ne + 1] = c.c[4 * i] + c.c[4 * i + 1] * (ex1 - c.x[i]) + c.c[4 * i + 2] * (ex1 - c.x[i]) * (ex1 - c.x[i]) + c.c[4 * i + 3] * (ex1 - c.x[i]) * (ex1 - c.x[i]) * (ex1 - c.x[i]);
                        ne = ne + 2;
                    }
                    else
                    {
                        if (tne == -1)
                        {
                            if (!de)
                            {
                                de = true;
                            }
                        }
                    }
                }
            }

            //
            //checking of arrays
            //get number of extremums (tNe=NE/2)
            //initialize pL as value F0(X[0]) and
            //initialize pR as value Fn-1(X[N])
            //
            tne = ne / 2;
            ne = 0;
            pl = c.c[0];
            pr = c.c[4 * (c.n - 1)];
            for (i = 0; i <= tne - 1; i++)
            {
                if (i > 0 && i < tne - 1)
                {
                    if ((double)(tmpc[2 * i + 1]) > (double)(tmpc[2 * (i - 1) + 1]) && (double)(tmpc[2 * i + 1]) > (double)(tmpc[2 * (i + 1) + 1]))
                    {

                        //
                        //maximum
                        //
                        tmpe[ne] = tmpc[2 * i];
                        tmpet[ne] = -1;
                        ne = ne + 1;
                    }
                    else
                    {
                        if ((double)(tmpc[2 * i + 1]) < (double)(tmpc[2 * (i - 1) + 1]) && (double)(tmpc[2 * i + 1]) < (double)(tmpc[2 * (i + 1) + 1]))
                        {

                            //
                            //minimum
                            //
                            tmpe[ne] = tmpc[2 * i];
                            tmpet[ne] = 1;
                            ne = ne + 1;
                        }
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        if ((double)(tmpc[2 * i]) != (double)(c.x[0]))
                        {
                            if ((double)(tmpc[2 * i + 1]) > (double)(pl) && (double)(tmpc[2 * i + 1]) > (double)(tmpc[2 * (i + 1) + 1]))
                            {

                                //
                                //maximum
                                //
                                tmpe[ne] = tmpc[2 * i];
                                tmpet[ne] = -1;
                                ne = ne + 1;
                            }
                            else
                            {
                                if ((double)(tmpc[2 * i + 1]) < (double)(pl) && (double)(tmpc[2 * i + 1]) < (double)(tmpc[2 * (i + 1) + 1]))
                                {

                                    //
                                    //minimum
                                    //
                                    tmpe[ne] = tmpc[2 * i];
                                    tmpet[ne] = 1;
                                    ne = ne + 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (i == tne - 1)
                        {
                            if ((double)(tmpc[2 * i]) != (double)(c.x[c.n - 1]))
                            {
                                if ((double)(tmpc[2 * i + 1]) > (double)(tmpc[2 * (i - 1) + 1]) && (double)(tmpc[2 * i + 1]) > (double)(pr))
                                {

                                    //
                                    //maximum
                                    //
                                    tmpe[ne] = tmpc[2 * i];
                                    tmpet[ne] = -1;
                                    ne = ne + 1;
                                }
                                else
                                {
                                    if ((double)(tmpc[2 * i + 1]) < (double)(tmpc[2 * (i - 1) + 1]) && (double)(tmpc[2 * i + 1]) < (double)(pr))
                                    {

                                        //
                                        //minimum
                                        //
                                        tmpe[ne] = tmpc[2 * i];
                                        tmpet[ne] = 1;
                                        ne = ne + 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //
            //final results
            //allocate R, E, ET
            //
            apserv.rvectorsetlengthatleast(ref r, nr, _params);
            apserv.rvectorsetlengthatleast(ref e, ne, _params);
            apserv.ivectorsetlengthatleast(ref et, ne, _params);

            //
            //write result for extremus and their types
            //
            for (i = 0; i <= ne - 1; i++)
            {
                e[i] = tmpe[i];
                et[i] = tmpet[i];
            }

            //
            //write result for roots
            //
            for (i = 0; i <= nr - 1; i++)
            {
                r[i] = tmpr[i];
            }
        }
    }


    /*************************************************************************
    Internal subroutine. Heap sort.
    *************************************************************************/
    public static void heapsortdpoints(ref double[] x,
        ref double[] y,
        ref double[] d,
        int n,
        xparams _params)
    {
        double[] rbuf = new double[0];
        int[] ibuf = new int[0];
        double[] rbuf2 = new double[0];
        int[] ibuf2 = new int[0];
        int i = 0;
        int i_ = 0;

        ibuf = new int[n];
        rbuf = new double[n];
        for (i = 0; i <= n - 1; i++)
        {
            ibuf[i] = i;
        }
        tsort.tagsortfasti(ref x, ref ibuf, ref rbuf2, ref ibuf2, n, _params);
        for (i = 0; i <= n - 1; i++)
        {
            rbuf[i] = y[ibuf[i]];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            y[i_] = rbuf[i_];
        }
        for (i = 0; i <= n - 1; i++)
        {
            rbuf[i] = d[ibuf[i]];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            d[i_] = rbuf[i_];
        }
    }


    /*************************************************************************
    This procedure search roots of an quadratic equation inside [0;1] and it number of roots.

    INPUT PARAMETERS:
        P0   -   value of a function at 0
        M0   -   value of a derivative at 0
        P1   -   value of a function at 1
        M1   -   value of a derivative at 1

    OUTPUT PARAMETERS:
        X0   -  first root of an equation
        X1   -  second root of an equation
        NR   -  number of roots
        
    RESTRICTIONS OF PARAMETERS:

    Parameters for this procedure has't to be zero simultaneously. Is expected, 
    that input polinom is't degenerate or constant identicaly ZERO.


    REMARK:

    The procedure always fill value for X1 and X2, even if it is't belongs to [0;1].
    But first true root(even if existing one) is in X1.
    Number of roots is NR.

     -- ALGLIB PROJECT --
         Copyright 26.09.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void solvepolinom2(double p0,
        double m0,
        double p1,
        double m1,
        ref double x0,
        ref double x1,
        ref int nr,
        xparams _params)
    {
        double a = 0;
        double b = 0;
        double c = 0;
        double dd = 0;
        double tmp = 0;
        double exf = 0;
        double extr = 0;

        x0 = 0;
        x1 = 0;
        nr = 0;


        //
        //calculate parameters for equation: A, B  and C
        //
        a = 6 * p0 + 3 * m0 - 6 * p1 + 3 * m1;
        b = -(6 * p0) - 4 * m0 + 6 * p1 - 2 * m1;
        c = m0;

        //
        //check case, when A=0
        //we are considering the linear equation
        //
        if ((double)(a) == (double)(0))
        {

            //
            //B<>0 and root inside [0;1]
            //one root
            //
            if (((double)(b) != (double)(0) && Math.Sign(c) * Math.Sign(b) <= 0) && (double)(Math.Abs(b)) >= (double)(Math.Abs(c)))
            {
                x0 = -(c / b);
                nr = 1;
                return;
            }
            else
            {
                nr = 0;
                return;
            }
        }

        //
        //consider case, when extremumu outside (0;1)
        //exist one root only
        //
        if ((double)(Math.Abs(2 * a)) <= (double)(Math.Abs(b)) || Math.Sign(b) * Math.Sign(a) >= 0)
        {
            if (Math.Sign(m0) * Math.Sign(m1) > 0)
            {
                nr = 0;
                return;
            }

            //
            //consider case, when the one exist
            //same sign of derivative
            //
            if (Math.Sign(m0) * Math.Sign(m1) < 0)
            {
                nr = 1;
                extr = -(b / (2 * a));
                dd = b * b - 4 * a * c;
                if ((double)(dd) < (double)(0))
                {
                    return;
                }
                x0 = (-b - Math.Sqrt(dd)) / (2 * a);
                x1 = (-b + Math.Sqrt(dd)) / (2 * a);
                if (((double)(extr) >= (double)(1) && (double)(x1) <= (double)(extr)) || ((double)(extr) <= (double)(0) && (double)(x1) >= (double)(extr)))
                {
                    x0 = x1;
                }
                return;
            }

            //
            //consider case, when the one is 0
            //
            if ((double)(m0) == (double)(0))
            {
                x0 = 0;
                nr = 1;
                return;
            }
            if ((double)(m1) == (double)(0))
            {
                x0 = 1;
                nr = 1;
                return;
            }
        }
        else
        {

            //
            //consider case, when both of derivatives is 0
            //
            if ((double)(m0) == (double)(0) && (double)(m1) == (double)(0))
            {
                x0 = 0;
                x1 = 1;
                nr = 2;
                return;
            }

            //
            //consider case, when derivative at 0 is 0, and derivative at 1 is't 0
            //
            if ((double)(m0) == (double)(0) && (double)(m1) != (double)(0))
            {
                dd = b * b - 4 * a * c;
                if ((double)(dd) < (double)(0))
                {
                    x0 = 0;
                    nr = 1;
                    return;
                }
                x0 = (-b - Math.Sqrt(dd)) / (2 * a);
                x1 = (-b + Math.Sqrt(dd)) / (2 * a);
                extr = -(b / (2 * a));
                exf = a * extr * extr + b * extr + c;
                if (Math.Sign(exf) * Math.Sign(m1) > 0)
                {
                    x0 = 0;
                    nr = 1;
                    return;
                }
                else
                {
                    if ((double)(extr) > (double)(x0))
                    {
                        x0 = 0;
                    }
                    else
                    {
                        x1 = 0;
                    }
                    nr = 2;

                    //
                    //roots must placed ascending
                    //
                    if ((double)(x0) > (double)(x1))
                    {
                        tmp = x0;
                        x0 = x1;
                        x1 = tmp;
                    }
                    return;
                }
            }
            if ((double)(m1) == (double)(0) && (double)(m0) != (double)(0))
            {
                dd = b * b - 4 * a * c;
                if ((double)(dd) < (double)(0))
                {
                    x0 = 1;
                    nr = 1;
                    return;
                }
                x0 = (-b - Math.Sqrt(dd)) / (2 * a);
                x1 = (-b + Math.Sqrt(dd)) / (2 * a);
                extr = -(b / (2 * a));
                exf = a * extr * extr + b * extr + c;
                if (Math.Sign(exf) * Math.Sign(m0) > 0)
                {
                    x0 = 1;
                    nr = 1;
                    return;
                }
                else
                {
                    if ((double)(extr) < (double)(x0))
                    {
                        x0 = 1;
                    }
                    else
                    {
                        x1 = 1;
                    }
                    nr = 2;

                    //
                    //roots must placed ascending
                    //
                    if ((double)(x0) > (double)(x1))
                    {
                        tmp = x0;
                        x0 = x1;
                        x1 = tmp;
                    }
                    return;
                }
            }
            else
            {
                extr = -(b / (2 * a));
                exf = a * extr * extr + b * extr + c;
                if (Math.Sign(exf) * Math.Sign(m0) > 0 && Math.Sign(exf) * Math.Sign(m1) > 0)
                {
                    nr = 0;
                    return;
                }
                dd = b * b - 4 * a * c;
                if ((double)(dd) < (double)(0))
                {
                    nr = 0;
                    return;
                }
                x0 = (-b - Math.Sqrt(dd)) / (2 * a);
                x1 = (-b + Math.Sqrt(dd)) / (2 * a);

                //
                //if EXF and m0, EXF and m1 has different signs, then equation has two roots              
                //
                if (Math.Sign(exf) * Math.Sign(m0) < 0 && Math.Sign(exf) * Math.Sign(m1) < 0)
                {
                    nr = 2;

                    //
                    //roots must placed ascending
                    //
                    if ((double)(x0) > (double)(x1))
                    {
                        tmp = x0;
                        x0 = x1;
                        x1 = tmp;
                    }
                    return;
                }
                else
                {
                    nr = 1;
                    if (Math.Sign(exf) * Math.Sign(m0) < 0)
                    {
                        if ((double)(x1) < (double)(extr))
                        {
                            x0 = x1;
                        }
                        return;
                    }
                    if (Math.Sign(exf) * Math.Sign(m1) < 0)
                    {
                        if ((double)(x1) > (double)(extr))
                        {
                            x0 = x1;
                        }
                        return;
                    }
                }
            }
        }
    }


    /*************************************************************************
    This procedure search roots of an cubic equation inside [A;B], it number of roots 
    and number of extremums.

    INPUT PARAMETERS:
        pA   -   value of a function at A
        mA   -   value of a derivative at A
        pB   -   value of a function at B
        mB   -   value of a derivative at B
        A0   -   left border [A0;B0]
        B0   -   right border [A0;B0]

    OUTPUT PARAMETERS:
        X0   -  first root of an equation
        X1   -  second root of an equation
        X2   -  third root of an equation
        EX0  -  first extremum of a function
        EX0  -  second extremum of a function
        NR   -  number of roots
        NR   -  number of extrmums
        
    RESTRICTIONS OF PARAMETERS:

    Length of [A;B] must be positive and is't zero, i.e. A<>B and A<B.


    REMARK:

    If 'NR' is -1 it's mean, than polinom has infiniti roots.
    If 'NE' is -1 it's mean, than polinom has infiniti extremums.

     -- ALGLIB PROJECT --
         Copyright 26.09.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void solvecubicpolinom(double pa,
        double ma,
        double pb,
        double mb,
        double a,
        double b,
        ref double x0,
        ref double x1,
        ref double x2,
        ref double ex0,
        ref double ex1,
        ref int nr,
        ref int ne,
        ref double[] tempdata,
        xparams _params)
    {
        int i = 0;
        double tmpma = 0;
        double tmpmb = 0;
        double tex0 = 0;
        double tex1 = 0;

        x0 = 0;
        x1 = 0;
        x2 = 0;
        ex0 = 0;
        ex1 = 0;
        nr = 0;
        ne = 0;

        apserv.rvectorsetlengthatleast(ref tempdata, 3, _params);

        //
        //case, when A>B
        //
        ap.assert((double)(a) < (double)(b), "\nSolveCubicPolinom: incorrect borders for [A;B]!\n");

        //
        //case 1    
        //function can be identicaly to ZERO
        //
        if ((((double)(ma) == (double)(0) && (double)(mb) == (double)(0)) && (double)(pa) == (double)(pb)) && (double)(pa) == (double)(0))
        {
            nr = -1;
            ne = -1;
            return;
        }
        if (((double)(ma) == (double)(0) && (double)(mb) == (double)(0)) && (double)(pa) == (double)(pb))
        {
            nr = 0;
            ne = -1;
            return;
        }
        tmpma = ma * (b - a);
        tmpmb = mb * (b - a);
        solvepolinom2(pa, tmpma, pb, tmpmb, ref ex0, ref ex1, ref ne, _params);
        ex0 = rescaleval(0, 1, a, b, ex0, _params);
        ex1 = rescaleval(0, 1, a, b, ex1, _params);

        //
        //case 3.1
        //no extremums at [A;B]
        //
        if (ne == 0)
        {
            nr = bisectmethod(pa, tmpma, pb, tmpmb, 0, 1, ref x0, _params);
            if (nr == 1)
            {
                x0 = rescaleval(0, 1, a, b, x0, _params);
            }
            return;
        }

        //
        //case 3.2
        //one extremum
        //
        if (ne == 1)
        {
            if ((double)(ex0) == (double)(a) || (double)(ex0) == (double)(b))
            {
                nr = bisectmethod(pa, tmpma, pb, tmpmb, 0, 1, ref x0, _params);
                if (nr == 1)
                {
                    x0 = rescaleval(0, 1, a, b, x0, _params);
                }
                return;
            }
            else
            {
                nr = 0;
                i = 0;
                tex0 = rescaleval(a, b, 0, 1, ex0, _params);
                nr = bisectmethod(pa, tmpma, pb, tmpmb, 0, tex0, ref x0, _params) + nr;
                if (nr > i)
                {
                    tempdata[i] = rescaleval(0, tex0, a, ex0, x0, _params);
                    i = i + 1;
                }
                nr = bisectmethod(pa, tmpma, pb, tmpmb, tex0, 1, ref x0, _params) + nr;
                if (nr > i)
                {
                    x0 = rescaleval(tex0, 1, ex0, b, x0, _params);
                    if (i > 0)
                    {
                        if ((double)(x0) != (double)(tempdata[i - 1]))
                        {
                            tempdata[i] = x0;
                            i = i + 1;
                        }
                        else
                        {
                            nr = nr - 1;
                        }
                    }
                    else
                    {
                        tempdata[i] = x0;
                        i = i + 1;
                    }
                }
                if (nr > 0)
                {
                    x0 = tempdata[0];
                    if (nr > 1)
                    {
                        x1 = tempdata[1];
                    }
                    return;
                }
            }
            return;
        }
        else
        {

            //
            //case 3.3
            //two extremums(or more, but it's impossible)
            //
            //
            //case 3.3.0
            //both extremums at the border
            //
            if ((double)(ex0) == (double)(a) && (double)(ex1) == (double)(b))
            {
                nr = bisectmethod(pa, tmpma, pb, tmpmb, 0, 1, ref x0, _params);
                if (nr == 1)
                {
                    x0 = rescaleval(0, 1, a, b, x0, _params);
                }
                return;
            }
            if ((double)(ex0) == (double)(a) && (double)(ex1) != (double)(b))
            {
                nr = 0;
                i = 0;
                tex1 = rescaleval(a, b, 0, 1, ex1, _params);
                nr = bisectmethod(pa, tmpma, pb, tmpmb, 0, tex1, ref x0, _params) + nr;
                if (nr > i)
                {
                    tempdata[i] = rescaleval(0, tex1, a, ex1, x0, _params);
                    i = i + 1;
                }
                nr = bisectmethod(pa, tmpma, pb, tmpmb, tex1, 1, ref x0, _params) + nr;
                if (nr > i)
                {
                    x0 = rescaleval(tex1, 1, ex1, b, x0, _params);
                    if ((double)(x0) != (double)(tempdata[i - 1]))
                    {
                        tempdata[i] = x0;
                        i = i + 1;
                    }
                    else
                    {
                        nr = nr - 1;
                    }
                }
                if (nr > 0)
                {
                    x0 = tempdata[0];
                    if (nr > 1)
                    {
                        x1 = tempdata[1];
                    }
                    return;
                }
            }
            if ((double)(ex1) == (double)(b) && (double)(ex0) != (double)(a))
            {
                nr = 0;
                i = 0;
                tex0 = rescaleval(a, b, 0, 1, ex0, _params);
                nr = bisectmethod(pa, tmpma, pb, tmpmb, 0, tex0, ref x0, _params) + nr;
                if (nr > i)
                {
                    tempdata[i] = rescaleval(0, tex0, a, ex0, x0, _params);
                    i = i + 1;
                }
                nr = bisectmethod(pa, tmpma, pb, tmpmb, tex0, 1, ref x0, _params) + nr;
                if (nr > i)
                {
                    x0 = rescaleval(tex0, 1, ex0, b, x0, _params);
                    if (i > 0)
                    {
                        if ((double)(x0) != (double)(tempdata[i - 1]))
                        {
                            tempdata[i] = x0;
                            i = i + 1;
                        }
                        else
                        {
                            nr = nr - 1;
                        }
                    }
                    else
                    {
                        tempdata[i] = x0;
                        i = i + 1;
                    }
                }
                if (nr > 0)
                {
                    x0 = tempdata[0];
                    if (nr > 1)
                    {
                        x1 = tempdata[1];
                    }
                    return;
                }
            }
            else
            {

                //
                //case 3.3.2
                //both extremums inside (0;1)
                //
                nr = 0;
                i = 0;
                tex0 = rescaleval(a, b, 0, 1, ex0, _params);
                tex1 = rescaleval(a, b, 0, 1, ex1, _params);
                nr = bisectmethod(pa, tmpma, pb, tmpmb, 0, tex0, ref x0, _params) + nr;
                if (nr > i)
                {
                    tempdata[i] = rescaleval(0, tex0, a, ex0, x0, _params);
                    i = i + 1;
                }
                nr = bisectmethod(pa, tmpma, pb, tmpmb, tex0, tex1, ref x0, _params) + nr;
                if (nr > i)
                {
                    x0 = rescaleval(tex0, tex1, ex0, ex1, x0, _params);
                    if (i > 0)
                    {
                        if ((double)(x0) != (double)(tempdata[i - 1]))
                        {
                            tempdata[i] = x0;
                            i = i + 1;
                        }
                        else
                        {
                            nr = nr - 1;
                        }
                    }
                    else
                    {
                        tempdata[i] = x0;
                        i = i + 1;
                    }
                }
                nr = bisectmethod(pa, tmpma, pb, tmpmb, tex1, 1, ref x0, _params) + nr;
                if (nr > i)
                {
                    x0 = rescaleval(tex1, 1, ex1, b, x0, _params);
                    if (i > 0)
                    {
                        if ((double)(x0) != (double)(tempdata[i - 1]))
                        {
                            tempdata[i] = x0;
                            i = i + 1;
                        }
                        else
                        {
                            nr = nr - 1;
                        }
                    }
                    else
                    {
                        tempdata[i] = x0;
                        i = i + 1;
                    }
                }

                //
                //write are found roots
                //
                if (nr > 0)
                {
                    x0 = tempdata[0];
                    if (nr > 1)
                    {
                        x1 = tempdata[1];
                    }
                    if (nr > 2)
                    {
                        x2 = tempdata[2];
                    }
                    return;
                }
            }
        }
    }


    /*************************************************************************
    Function for searching a root at [A;B] by bisection method and return number of roots
    (0 or 1)

    INPUT PARAMETERS:
        pA   -   value of a function at A
        mA   -   value of a derivative at A
        pB   -   value of a function at B
        mB   -   value of a derivative at B
        A0   -   left border [A0;B0] 
        B0   -   right border [A0;B0]
        
    RESTRICTIONS OF PARAMETERS:

    We assume, that B0>A0.


    REMARK:

    Assume, that exist one root only at [A;B], else 
    function may be work incorrectly.
    The function dont check value A0,B0!

     -- ALGLIB PROJECT --
         Copyright 26.09.2011 by Bochkanov Sergey
    *************************************************************************/
    public static int bisectmethod(double pa,
        double ma,
        double pb,
        double mb,
        double a,
        double b,
        ref double x,
        xparams _params)
    {
        int result = 0;
        double vacuum = 0;
        double eps = 0;
        double a0 = 0;
        double b0 = 0;
        double m = 0;
        double lf = 0;
        double rf = 0;
        double mf = 0;

        x = 0;


        //
        //accuracy
        //
        eps = 1000 * (b - a) * math.machineepsilon;

        //
        //initialization left and right borders
        //
        a0 = a;
        b0 = b;

        //
        //initialize function value at 'A' and 'B'
        //
        hermitecalc(pa, ma, pb, mb, a, ref lf, ref vacuum, _params);
        hermitecalc(pa, ma, pb, mb, b, ref rf, ref vacuum, _params);

        //
        //check, that 'A' and 'B' are't roots,
        //and that root exist
        //
        if (Math.Sign(lf) * Math.Sign(rf) > 0)
        {
            result = 0;
            return result;
        }
        else
        {
            if ((double)(lf) == (double)(0))
            {
                x = a;
                result = 1;
                return result;
            }
            else
            {
                if ((double)(rf) == (double)(0))
                {
                    x = b;
                    result = 1;
                    return result;
                }
            }
        }

        //
        //searching a root
        //
        do
        {
            m = (b0 + a0) / 2;
            hermitecalc(pa, ma, pb, mb, a0, ref lf, ref vacuum, _params);
            hermitecalc(pa, ma, pb, mb, b0, ref rf, ref vacuum, _params);
            hermitecalc(pa, ma, pb, mb, m, ref mf, ref vacuum, _params);
            if (Math.Sign(mf) * Math.Sign(lf) < 0)
            {
                b0 = m;
            }
            else
            {
                if (Math.Sign(mf) * Math.Sign(rf) < 0)
                {
                    a0 = m;
                }
                else
                {
                    if ((double)(lf) == (double)(0))
                    {
                        x = a0;
                        result = 1;
                        return result;
                    }
                    if ((double)(rf) == (double)(0))
                    {
                        x = b0;
                        result = 1;
                        return result;
                    }
                    if ((double)(mf) == (double)(0))
                    {
                        x = m;
                        result = 1;
                        return result;
                    }
                }
            }
        }
        while ((double)(Math.Abs(b0 - a0)) >= (double)(eps));
        x = m;
        result = 1;
        return result;
    }


    /*************************************************************************
    This function builds monotone cubic Hermite interpolant. This interpolant
    is monotonic in [x(0),x(n-1)] and is constant outside of this interval.

    In  case  y[]  form  non-monotonic  sequence,  interpolant  is  piecewise
    monotonic.  Say, for x=(0,1,2,3,4)  and  y=(0,1,2,1,0)  interpolant  will
    monotonically grow at [0..2] and monotonically decrease at [2..4].

    INPUT PARAMETERS:
        X           -   spline nodes, array[0..N-1]. Subroutine automatically
                        sorts points, so caller may pass unsorted array.
        Y           -   function values, array[0..N-1]
        N           -   the number of points(N>=2).

    OUTPUT PARAMETERS:
        C           -   spline interpolant.

     -- ALGLIB PROJECT --
         Copyright 21.06.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dbuildmonotone(double[] x,
        double[] y,
        int n,
        spline1dinterpolant c,
        xparams _params)
    {
        double[] d = new double[0];
        double[] ex = new double[0];
        double[] ey = new double[0];
        int[] p = new int[0];
        double delta = 0;
        double alpha = 0;
        double beta = 0;
        int tmpn = 0;
        int sn = 0;
        double ca = 0;
        double cb = 0;
        double epsilon = 0;
        int i = 0;
        int j = 0;

        x = (double[])x.Clone();
        y = (double[])y.Clone();


        //
        // Check lengths of arguments
        //
        ap.assert(n >= 2, "Spline1DBuildMonotone: N<2");
        ap.assert(ap.len(x) >= n, "Spline1DBuildMonotone: Length(X)<N");
        ap.assert(ap.len(y) >= n, "Spline1DBuildMonotone: Length(Y)<N");

        //
        // Check and sort points
        //
        ap.assert(apserv.isfinitevector(x, n, _params), "Spline1DBuildMonotone: X contains infinite or NAN values");
        ap.assert(apserv.isfinitevector(y, n, _params), "Spline1DBuildMonotone: Y contains infinite or NAN values");
        heapsortppoints(ref x, ref y, ref p, n, _params);
        ap.assert(apserv.aredistinct(x, n, _params), "Spline1DBuildMonotone: at least two consequent points are too close");
        epsilon = math.machineepsilon;
        n = n + 2;
        d = new double[n];
        ex = new double[n];
        ey = new double[n];
        ex[0] = x[0] - Math.Abs(x[1] - x[0]);
        ex[n - 1] = x[n - 3] + Math.Abs(x[n - 3] - x[n - 4]);
        ey[0] = y[0];
        ey[n - 1] = y[n - 3];
        for (i = 1; i <= n - 2; i++)
        {
            ex[i] = x[i - 1];
            ey[i] = y[i - 1];
        }

        //
        // Init sign of the function for first segment
        //
        i = 0;
        ca = 0;
        do
        {
            ca = ey[i + 1] - ey[i];
            i = i + 1;
        }
        while (!((double)(ca) != (double)(0) || i > n - 2));
        if ((double)(ca) != (double)(0))
        {
            ca = ca / Math.Abs(ca);
        }
        i = 0;
        while (i < n - 1)
        {

            //
            // Partition of the segment [X0;Xn]
            //
            tmpn = 1;
            for (j = i; j <= n - 2; j++)
            {
                cb = ey[j + 1] - ey[j];
                if ((double)(ca * cb) >= (double)(0))
                {
                    tmpn = tmpn + 1;
                }
                else
                {
                    ca = cb / Math.Abs(cb);
                    break;
                }
            }
            sn = i + tmpn;
            ap.assert(tmpn >= 2, "Spline1DBuildMonotone: internal error");

            //
            // Calculate derivatives for current segment
            //
            d[i] = 0;
            d[sn - 1] = 0;
            for (j = i + 1; j <= sn - 2; j++)
            {
                d[j] = ((ey[j] - ey[j - 1]) / (ex[j] - ex[j - 1]) + (ey[j + 1] - ey[j]) / (ex[j + 1] - ex[j])) / 2;
            }
            for (j = i; j <= sn - 2; j++)
            {
                delta = (ey[j + 1] - ey[j]) / (ex[j + 1] - ex[j]);
                if ((double)(Math.Abs(delta)) <= (double)(epsilon))
                {
                    d[j] = 0;
                    d[j + 1] = 0;
                }
                else
                {
                    alpha = d[j] / delta;
                    beta = d[j + 1] / delta;
                    if ((double)(alpha) != (double)(0))
                    {
                        cb = alpha * Math.Sqrt(1 + math.sqr(beta / alpha));
                    }
                    else
                    {
                        if ((double)(beta) != (double)(0))
                        {
                            cb = beta;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if ((double)(cb) > (double)(3))
                    {
                        d[j] = 3 * alpha * delta / cb;
                        d[j + 1] = 3 * beta * delta / cb;
                    }
                }
            }

            //
            // Transition to next segment
            //
            i = sn - 1;
        }
        spline1dbuildhermite(ex, ey, d, n, c, _params);
        c.continuity = 2;
    }


    /*************************************************************************
    Prepare approximate cardinal basis

      -- ALGLIB PROJECT --
         Copyright 09.04.2022 by Bochkanov Sergey.
    *************************************************************************/
    private static void bbasisinit(spline1dbbasis basis,
        int m,
        xparams _params)
    {
        ablasf.rallocv(7, ref basis.tmpx, _params);
        ablasf.rallocv(7, ref basis.tmpy, _params);

        //
        // Special cases: M=2 or M=3
        //
        if (m == 2)
        {
            basis.bfrad = 1;
            basis.m = 2;
            basis.tmpx[0] = (double)0 / (double)(m - 1);
            basis.tmpx[1] = (double)1 / (double)(m - 1);
            basis.tmpy[0] = 1;
            basis.tmpy[1] = 0;
            spline1dbuildcubic(basis.tmpx, basis.tmpy, 2, 2, 0.0, 2, 0.0, basis.s0, _params);
            return;
        }
        if (m == 3)
        {
            basis.bfrad = 2;
            basis.m = 3;
            basis.tmpx[0] = (double)0 / (double)(m - 1);
            basis.tmpx[1] = (double)1 / (double)(m - 1);
            basis.tmpx[2] = (double)2 / (double)(m - 1);
            basis.tmpy[0] = 1;
            basis.tmpy[1] = 0;
            basis.tmpy[2] = 0;
            spline1dbuildcubic(basis.tmpx, basis.tmpy, 3, 2, 0.0, 2, 0.0, basis.s0, _params);
            basis.tmpy[0] = 0;
            basis.tmpy[1] = 1;
            basis.tmpy[2] = 0;
            spline1dbuildcubic(basis.tmpx, basis.tmpy, 3, 2, 0.0, 2, 0.0, basis.s1, _params);
            return;
        }

        //
        // General case: M>=4
        //
        basis.bfrad = 2;
        basis.m = m;

        //
        // Generate S0 - leftmost kernel
        //
        basis.tmpx[0] = -((double)1 / (double)(m - 1));
        basis.tmpx[1] = (double)0 / (double)(m - 1);
        basis.tmpx[2] = (double)1 / (double)(m - 1);
        basis.tmpx[3] = (double)2 / (double)(m - 1);
        basis.tmpx[4] = (double)3 / (double)(m - 1);
        basis.tmpy[0] = 2;
        basis.tmpy[1] = 1;
        basis.tmpy[2] = (double)1 / (double)6;
        basis.tmpy[3] = 0;
        basis.tmpy[4] = 0;
        spline1dbuildcubic(basis.tmpx, basis.tmpy, 5, 2, 0.0, 2, 0.0, basis.s0, _params);

        //
        // Generate S1 - second from the left
        //
        basis.tmpx[0] = -((double)1 / (double)(m - 1));
        basis.tmpx[1] = (double)0 / (double)(m - 1);
        basis.tmpx[2] = (double)1 / (double)(m - 1);
        basis.tmpx[3] = (double)2 / (double)(m - 1);
        basis.tmpx[4] = (double)3 / (double)(m - 1);
        basis.tmpx[5] = (double)4 / (double)(m - 1);
        basis.tmpy[0] = -1;
        basis.tmpy[1] = 0;
        basis.tmpy[2] = (double)2 / (double)3;
        basis.tmpy[3] = (double)1 / (double)6;
        basis.tmpy[4] = 0;
        basis.tmpy[5] = 0;
        spline1dbuildcubic(basis.tmpx, basis.tmpy, 6, 2, 0.0, 2, 0.0, basis.s1, _params);

        //
        // Generate S2 - centrally symmetric kernel, generated only for M>=5
        //
        basis.tmpx[0] = -((double)3 / (double)(m - 1));
        basis.tmpx[1] = -((double)2 / (double)(m - 1));
        basis.tmpx[2] = -((double)1 / (double)(m - 1));
        basis.tmpx[3] = (double)0 / (double)(m - 1);
        basis.tmpx[4] = (double)1 / (double)(m - 1);
        basis.tmpx[5] = (double)2 / (double)(m - 1);
        basis.tmpx[6] = (double)3 / (double)(m - 1);
        if (m >= 5)
        {
            basis.tmpy[0] = 0;
            basis.tmpy[1] = 0;
            basis.tmpy[2] = (double)1 / (double)12;
            basis.tmpy[3] = (double)2 / (double)6;
            basis.tmpy[4] = (double)1 / (double)12;
            basis.tmpy[5] = 0;
            basis.tmpy[6] = 0;
        }
        else
        {
            ablasf.rsetv(7, 0.0, basis.tmpy, _params);
        }
        spline1dbuildcubic(basis.tmpx, basis.tmpy, 7, 2, 0.0, 2, 0.0, basis.s2, _params);
    }


    /*************************************************************************
    Computes B-basis function #K at point X.


      -- ALGLIB PROJECT --
         Copyright 09.04.2022 by Bochkanov Sergey.
    *************************************************************************/
    private static double basiscalc(spline1dbbasis basis,
        int k,
        double x,
        xparams _params)
    {
        double result = 0;
        double delta = 0;
        double y = 0;

        if (k > basis.m - 1 - k)
        {
            k = basis.m - 1 - k;
            x = 1 - x;
        }
        delta = (double)1 / (double)(basis.m - 1);
        y = x - k * delta;
        if ((double)(y) <= (double)(-(basis.bfrad * delta)) || (double)(y) >= (double)(basis.bfrad * delta))
        {
            result = 0;
            return result;
        }
        if (k == 0)
        {
            result = spline1dcalc(basis.s0, x, _params);
            return result;
        }
        if (k == 1)
        {
            result = spline1dcalc(basis.s1, x, _params);
            return result;
        }
        result = spline1dcalc(basis.s2, y, _params);
        return result;
    }


    /*************************************************************************
    Computes B-basis function #K at point X.


      -- ALGLIB PROJECT --
         Copyright 09.04.2022 by Bochkanov Sergey.
    *************************************************************************/
    private static double basisdiff(spline1dbbasis basis,
        int k,
        double x,
        xparams _params)
    {
        double result = 0;
        double delta = 0;
        double y = 0;
        double f = 0;
        double df = 0;
        double d2f = 0;
        double sgn = 0;

        sgn = 1;
        if (k > basis.m - 1 - k)
        {
            k = basis.m - 1 - k;
            x = 1 - x;
            sgn = -1;
        }
        delta = (double)1 / (double)(basis.m - 1);
        y = x - k * delta;
        if ((double)(y) <= (double)(-(basis.bfrad * delta)) || (double)(y) >= (double)(basis.bfrad * delta))
        {
            result = 0;
            return result;
        }
        if (k == 0)
        {
            spline1ddiff(basis.s0, x, ref f, ref df, ref d2f, _params);
            result = sgn * df;
            return result;
        }
        if (k == 1)
        {
            spline1ddiff(basis.s1, x, ref f, ref df, ref d2f, _params);
            result = sgn * df;
            return result;
        }
        spline1ddiff(basis.s2, y, ref f, ref df, ref d2f, _params);
        result = sgn * df;
        return result;
    }


    /*************************************************************************
    Computes B-basis function #K at point X.


      -- ALGLIB PROJECT --
         Copyright 09.04.2022 by Bochkanov Sergey.
    *************************************************************************/
    private static double basisdiff2(spline1dbbasis basis,
        int k,
        double x,
        xparams _params)
    {
        double result = 0;
        double delta = 0;
        double y = 0;
        double f = 0;
        double df = 0;
        double d2f = 0;

        if (k > basis.m - 1 - k)
        {
            k = basis.m - 1 - k;
            x = 1 - x;
        }
        delta = (double)1 / (double)(basis.m - 1);
        y = x - k * delta;
        if ((double)(y) <= (double)(-(basis.bfrad * delta)) || (double)(y) >= (double)(basis.bfrad * delta))
        {
            result = 0;
            return result;
        }
        if (k == 0)
        {
            spline1ddiff(basis.s0, x, ref f, ref df, ref d2f, _params);
            result = d2f;
            return result;
        }
        if (k == 1)
        {
            spline1ddiff(basis.s1, x, ref f, ref df, ref d2f, _params);
            result = d2f;
            return result;
        }
        spline1ddiff(basis.s2, y, ref f, ref df, ref d2f, _params);
        result = d2f;
        return result;
    }


    /*************************************************************************
    Internal version of Spline1DGridDiffCubic.

    Accepts pre-ordered X/Y, temporary arrays (which may be  preallocated,  if
    you want to save time, or not) and output array (which may be preallocated
    too).

    Y is passed as var-parameter because we may need to force last element  to
    be equal to the first one (if periodic boundary conditions are specified).

      -- ALGLIB PROJECT --
         Copyright 03.09.2010 by Bochkanov Sergey
    *************************************************************************/
    private static void spline1dgriddiffcubicinternal(double[] x,
        ref double[] y,
        int n,
        int boundltype,
        double boundl,
        int boundrtype,
        double boundr,
        ref double[] d,
        ref double[] a1,
        ref double[] a2,
        ref double[] a3,
        ref double[] b,
        ref double[] dt,
        xparams _params)
    {
        int i = 0;
        int i_ = 0;


        //
        // allocate arrays
        //
        if (ap.len(d) < n)
        {
            d = new double[n];
        }
        if (ap.len(a1) < n)
        {
            a1 = new double[n];
        }
        if (ap.len(a2) < n)
        {
            a2 = new double[n];
        }
        if (ap.len(a3) < n)
        {
            a3 = new double[n];
        }
        if (ap.len(b) < n)
        {
            b = new double[n];
        }
        if (ap.len(dt) < n)
        {
            dt = new double[n];
        }

        //
        // Special cases:
        // * N=2, parabolic terminated boundary condition on both ends
        // * N=2, periodic boundary condition
        //
        if ((n == 2 && boundltype == 0) && boundrtype == 0)
        {
            d[0] = (y[1] - y[0]) / (x[1] - x[0]);
            d[1] = d[0];
            return;
        }
        if ((n == 2 && boundltype == -1) && boundrtype == -1)
        {
            d[0] = 0;
            d[1] = 0;
            return;
        }

        //
        // Periodic and non-periodic boundary conditions are
        // two separate classes
        //
        if (boundrtype == -1 && boundltype == -1)
        {

            //
            // Periodic boundary conditions
            //
            y[n - 1] = y[0];

            //
            // Boundary conditions at N-1 points
            // (one point less because last point is the same as first point).
            //
            a1[0] = x[1] - x[0];
            a2[0] = 2 * (x[1] - x[0] + x[n - 1] - x[n - 2]);
            a3[0] = x[n - 1] - x[n - 2];
            b[0] = 3 * (y[n - 1] - y[n - 2]) / (x[n - 1] - x[n - 2]) * (x[1] - x[0]) + 3 * (y[1] - y[0]) / (x[1] - x[0]) * (x[n - 1] - x[n - 2]);
            for (i = 1; i <= n - 2; i++)
            {

                //
                // Altough last point is [N-2], we use X[N-1] and Y[N-1]
                // (because of periodicity)
                //
                a1[i] = x[i + 1] - x[i];
                a2[i] = 2 * (x[i + 1] - x[i - 1]);
                a3[i] = x[i] - x[i - 1];
                b[i] = 3 * (y[i] - y[i - 1]) / (x[i] - x[i - 1]) * (x[i + 1] - x[i]) + 3 * (y[i + 1] - y[i]) / (x[i + 1] - x[i]) * (x[i] - x[i - 1]);
            }

            //
            // Solve, add last point (with index N-1)
            //
            solvecyclictridiagonal(a1, a2, a3, b, n - 1, ref dt, _params);
            for (i_ = 0; i_ <= n - 2; i_++)
            {
                d[i_] = dt[i_];
            }
            d[n - 1] = d[0];
        }
        else
        {

            //
            // Non-periodic boundary condition.
            // Left boundary conditions.
            //
            if (boundltype == 0)
            {
                a1[0] = 0;
                a2[0] = 1;
                a3[0] = 1;
                b[0] = 2 * (y[1] - y[0]) / (x[1] - x[0]);
            }
            if (boundltype == 1)
            {
                a1[0] = 0;
                a2[0] = 1;
                a3[0] = 0;
                b[0] = boundl;
            }
            if (boundltype == 2)
            {
                a1[0] = 0;
                a2[0] = 2;
                a3[0] = 1;
                b[0] = 3 * (y[1] - y[0]) / (x[1] - x[0]) - 0.5 * boundl * (x[1] - x[0]);
            }

            //
            // Central conditions
            //
            for (i = 1; i <= n - 2; i++)
            {
                a1[i] = x[i + 1] - x[i];
                a2[i] = 2 * (x[i + 1] - x[i - 1]);
                a3[i] = x[i] - x[i - 1];
                b[i] = 3 * (y[i] - y[i - 1]) / (x[i] - x[i - 1]) * (x[i + 1] - x[i]) + 3 * (y[i + 1] - y[i]) / (x[i + 1] - x[i]) * (x[i] - x[i - 1]);
            }

            //
            // Right boundary conditions
            //
            if (boundrtype == 0)
            {
                a1[n - 1] = 1;
                a2[n - 1] = 1;
                a3[n - 1] = 0;
                b[n - 1] = 2 * (y[n - 1] - y[n - 2]) / (x[n - 1] - x[n - 2]);
            }
            if (boundrtype == 1)
            {
                a1[n - 1] = 0;
                a2[n - 1] = 1;
                a3[n - 1] = 0;
                b[n - 1] = boundr;
            }
            if (boundrtype == 2)
            {
                a1[n - 1] = 1;
                a2[n - 1] = 2;
                a3[n - 1] = 0;
                b[n - 1] = 3 * (y[n - 1] - y[n - 2]) / (x[n - 1] - x[n - 2]) + 0.5 * boundr * (x[n - 1] - x[n - 2]);
            }

            //
            // Solve
            //
            solvetridiagonal(a1, a2, a3, b, n, ref d, _params);
        }
    }


    /*************************************************************************
    Internal subroutine. Heap sort.
    *************************************************************************/
    private static void heapsortpoints(ref double[] x,
        ref double[] y,
        int n,
        xparams _params)
    {
        double[] bufx = new double[0];
        double[] bufy = new double[0];

        tsort.tagsortfastr(ref x, ref y, ref bufx, ref bufy, n, _params);
    }


    /*************************************************************************
    Internal subroutine. Heap sort.

    Accepts:
        X, Y    -   points
        P       -   empty or preallocated array
        
    Returns:
        X, Y    -   sorted by X
        P       -   array of permutations; I-th position of output
                    arrays X/Y contains (X[P[I]],Y[P[I]])
    *************************************************************************/
    private static void heapsortppoints(ref double[] x,
        ref double[] y,
        ref int[] p,
        int n,
        xparams _params)
    {
        double[] rbuf = new double[0];
        int[] ibuf = new int[0];
        int i = 0;
        int i_ = 0;

        if (ap.len(p) < n)
        {
            p = new int[n];
        }
        rbuf = new double[n];
        for (i = 0; i <= n - 1; i++)
        {
            p[i] = i;
        }
        tsort.tagsortfasti(ref x, ref p, ref rbuf, ref ibuf, n, _params);
        for (i = 0; i <= n - 1; i++)
        {
            rbuf[i] = y[p[i]];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            y[i_] = rbuf[i_];
        }
    }


    /*************************************************************************
    Internal subroutine. Tridiagonal solver. Solves

    ( B[0] C[0]                      
    ( A[1] B[1] C[1]                 )
    (      A[2] B[2] C[2]            )
    (            ..........          ) * X = D
    (            ..........          )
    (           A[N-2] B[N-2] C[N-2] )
    (                  A[N-1] B[N-1] )

    *************************************************************************/
    private static void solvetridiagonal(double[] a,
        double[] b,
        double[] c,
        double[] d,
        int n,
        ref double[] x,
        xparams _params)
    {
        int k = 0;
        double t = 0;

        b = (double[])b.Clone();
        d = (double[])d.Clone();

        if (ap.len(x) < n)
        {
            x = new double[n];
        }
        for (k = 1; k <= n - 1; k++)
        {
            t = a[k] / b[k - 1];
            b[k] = b[k] - t * c[k - 1];
            d[k] = d[k] - t * d[k - 1];
        }
        x[n - 1] = d[n - 1] / b[n - 1];
        for (k = n - 2; k >= 0; k--)
        {
            x[k] = (d[k] - c[k] * x[k + 1]) / b[k];
        }
    }


    /*************************************************************************
    Internal subroutine. Cyclic tridiagonal solver. Solves

    ( B[0] C[0]                 A[0] )
    ( A[1] B[1] C[1]                 )
    (      A[2] B[2] C[2]            )
    (            ..........          ) * X = D
    (            ..........          )
    (           A[N-2] B[N-2] C[N-2] )
    ( C[N-1]           A[N-1] B[N-1] )
    *************************************************************************/
    private static void solvecyclictridiagonal(double[] a,
        double[] b,
        double[] c,
        double[] d,
        int n,
        ref double[] x,
        xparams _params)
    {
        int k = 0;
        double alpha = 0;
        double beta = 0;
        double gamma = 0;
        double[] y = new double[0];
        double[] z = new double[0];
        double[] u = new double[0];

        b = (double[])b.Clone();

        if (ap.len(x) < n)
        {
            x = new double[n];
        }
        beta = a[0];
        alpha = c[n - 1];
        gamma = -b[0];
        b[0] = 2 * b[0];
        b[n - 1] = b[n - 1] - alpha * beta / gamma;
        u = new double[n];
        for (k = 0; k <= n - 1; k++)
        {
            u[k] = 0;
        }
        u[0] = gamma;
        u[n - 1] = alpha;
        solvetridiagonal(a, b, c, d, n, ref y, _params);
        solvetridiagonal(a, b, c, u, n, ref z, _params);
        for (k = 0; k <= n - 1; k++)
        {
            x[k] = y[k] - (y[0] + beta / gamma * y[n - 1]) / (1 + z[0] + beta / gamma * z[n - 1]) * z[k];
        }
    }


    /*************************************************************************
    Internal subroutine. Three-point differentiation
    *************************************************************************/
    private static double diffthreepoint(double t,
        double x0,
        double f0,
        double x1,
        double f1,
        double x2,
        double f2,
        xparams _params)
    {
        double result = 0;
        double a = 0;
        double b = 0;

        t = t - x0;
        x1 = x1 - x0;
        x2 = x2 - x0;
        a = (f2 - f0 - x2 / x1 * (f1 - f0)) / (math.sqr(x2) - x1 * x2);
        b = (f1 - f0 - a * math.sqr(x1)) / x1;
        result = 2 * a * t + b;
        return result;
    }


    /*************************************************************************
    Procedure for calculating value of a function is providet in the form of
    Hermite polinom  

    INPUT PARAMETERS:
        P0   -   value of a function at 0
        M0   -   value of a derivative at 0
        P1   -   value of a function at 1
        M1   -   value of a derivative at 1
        T    -   point inside [0;1]
        
    OUTPUT PARAMETERS:
        S    -   value of a function at T
        B0   -   value of a derivative function at T
        
     -- ALGLIB PROJECT --
         Copyright 26.09.2011 by Bochkanov Sergey
    *************************************************************************/
    private static void hermitecalc(double p0,
        double m0,
        double p1,
        double m1,
        double t,
        ref double s,
        ref double ds,
        xparams _params)
    {
        s = 0;
        ds = 0;

        s = p0 * (1 + 2 * t) * (1 - t) * (1 - t) + m0 * t * (1 - t) * (1 - t) + p1 * (3 - 2 * t) * t * t + m1 * t * t * (t - 1);
        ds = -(p0 * 6 * t * (1 - t)) + m0 * (1 - t) * (1 - 3 * t) + p1 * 6 * t * (1 - t) + m1 * t * (3 * t - 2);
    }


    /*************************************************************************
    Function for mapping from [A0;B0] to [A1;B1]

    INPUT PARAMETERS:
        A0   -   left border [A0;B0]
        B0   -   right border [A0;B0]
        A1   -   left border [A1;B1]
        B1   -   right border [A1;B1]
        T    -   value inside [A0;B0]  
        
    RESTRICTIONS OF PARAMETERS:

    We assume, that B0>A0 and B1>A1. But we chech, that T is inside [A0;B0], 
    and if T<A0 then T become A1, if T>B0 then T - B1. 

    INPUT PARAMETERS:
            A0   -   left border for segment [A0;B0] from 'T' is converted to [A1;B1] 
            B0   -   right border for segment [A0;B0] from 'T' is converted to [A1;B1] 
            A1   -   left border for segment [A1;B1] to 'T' is converted from [A0;B0] 
            B1   -   right border for segment [A1;B1] to 'T' is converted from [A0;B0] 
            T    -   the parameter is mapped from [A0;B0] to [A1;B1] 

    Result:
        is converted value for 'T' from [A0;B0] to [A1;B1]
             
    REMARK:

    The function dont check value A0,B0 and A1,B1!

     -- ALGLIB PROJECT --
         Copyright 26.09.2011 by Bochkanov Sergey
    *************************************************************************/
    private static double rescaleval(double a0,
        double b0,
        double a1,
        double b1,
        double t,
        xparams _params)
    {
        double result = 0;


        //
        //return left border
        //
        if ((double)(t) <= (double)(a0))
        {
            result = a1;
            return result;
        }

        //
        //return right border
        //
        if ((double)(t) >= (double)(b0))
        {
            result = b1;
            return result;
        }

        //
        //return value between left and right borders
        //
        result = (b1 - a1) * (t - a0) / (b0 - a0) + a1;
        return result;
    }


}
