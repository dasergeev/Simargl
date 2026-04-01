using System;

#pragma warning disable CS3008
#pragma warning disable CS8618
#pragma warning disable CS8600
#pragma warning disable CS8631
#pragma warning disable CS8602
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

public class spline2d
{
    /*************************************************************************
    2-dimensional spline inteprolant
    *************************************************************************/
    public class spline2dinterpolant : apobject
    {
        public int stype;
        public bool hasmissingcells;
        public int n;
        public int m;
        public int d;
        public double[] x;
        public double[] y;
        public double[] f;
        public bool[] ismissingnode;
        public bool[] ismissingcell;
        public spline2dinterpolant()
        {
            init();
        }
        public override void init()
        {
            x = new double[0];
            y = new double[0];
            f = new double[0];
            ismissingnode = new bool[0];
            ismissingcell = new bool[0];
        }
        public override apobject make_copy()
        {
            spline2dinterpolant _result = new spline2dinterpolant();
            _result.stype = stype;
            _result.hasmissingcells = hasmissingcells;
            _result.n = n;
            _result.m = m;
            _result.d = d;
            _result.x = (double[])x.Clone();
            _result.y = (double[])y.Clone();
            _result.f = (double[])f.Clone();
            _result.ismissingnode = (bool[])ismissingnode.Clone();
            _result.ismissingcell = (bool[])ismissingcell.Clone();
            return _result;
        }
    };


    /*************************************************************************
    Nonlinear least squares solver used to fit 2D splines to data
    *************************************************************************/
    public class spline2dbuilder : apobject
    {
        public int priorterm;
        public double priortermval;
        public int areatype;
        public double xa;
        public double xb;
        public double ya;
        public double yb;
        public int gridtype;
        public int kx;
        public int ky;
        public double smoothing;
        public int nlayers;
        public int solvertype;
        public double lambdabase;
        public double[] xy;
        public int npoints;
        public int d;
        public double sx;
        public double sy;
        public bool adddegreeoffreedom;
        public int interfacesize;
        public int lsqrcnt;
        public int maxcoresize;
        public spline2dbuilder()
        {
            init();
        }
        public override void init()
        {
            xy = new double[0];
        }
        public override apobject make_copy()
        {
            spline2dbuilder _result = new spline2dbuilder();
            _result.priorterm = priorterm;
            _result.priortermval = priortermval;
            _result.areatype = areatype;
            _result.xa = xa;
            _result.xb = xb;
            _result.ya = ya;
            _result.yb = yb;
            _result.gridtype = gridtype;
            _result.kx = kx;
            _result.ky = ky;
            _result.smoothing = smoothing;
            _result.nlayers = nlayers;
            _result.solvertype = solvertype;
            _result.lambdabase = lambdabase;
            _result.xy = (double[])xy.Clone();
            _result.npoints = npoints;
            _result.d = d;
            _result.sx = sx;
            _result.sy = sy;
            _result.adddegreeoffreedom = adddegreeoffreedom;
            _result.interfacesize = interfacesize;
            _result.lsqrcnt = lsqrcnt;
            _result.maxcoresize = maxcoresize;
            return _result;
        }
    };


    /*************************************************************************
    Spline 2D fitting report:
        rmserror        RMS error
        avgerror        average error
        maxerror        maximum error
        r2              coefficient of determination,  R-squared, 1-RSS/TSS
    *************************************************************************/
    public class spline2dfitreport : apobject
    {
        public double rmserror;
        public double avgerror;
        public double maxerror;
        public double r2;
        public spline2dfitreport()
        {
            init();
        }
        public override void init()
        {
        }
        public override apobject make_copy()
        {
            spline2dfitreport _result = new spline2dfitreport();
            _result.rmserror = rmserror;
            _result.avgerror = avgerror;
            _result.maxerror = maxerror;
            _result.r2 = r2;
            return _result;
        }
    };


    /*************************************************************************
    Design matrix stored in batch/block sparse format.

    The idea is that design matrix for bicubic spline fitting has very regular
    structure:

    1. I-th row has non-zero entries in elements with indexes starting
    from some IDX, and including: IDX, IDX+1, IDX+2, IDX+3, IDX+KX+0, IDX+KX+1,
    and so on, up to 16 elements in total.

    Rows corresponding to dataset points have 16 non-zero elements, rows
    corresponding to nonlinearity penalty have 9 non-zero elements, and rows
    of regularizer have 1 element.

    For the sake of simplicity, we can use 16 elements for dataset rows and
    penalty rows, and process regularizer explicitly.

    2. points located in the same cell of the grid have same pattern of non-zeros,
    so we can use dense Level 2 and Level 3 linear algebra to work with such
    matrices.
    *************************************************************************/
    public class spline2dxdesignmatrix : apobject
    {
        public int blockwidth;
        public int kx;
        public int ky;
        public int npoints;
        public int nrows;
        public int ndenserows;
        public int ndensebatches;
        public int d;
        public int maxbatch;
        public double[,] vals;
        public int[] batches;
        public int[] batchbases;
        public double lambdareg;
        public double[] tmp0;
        public double[] tmp1;
        public double[,] tmp2;
        public spline2dxdesignmatrix()
        {
            init();
        }
        public override void init()
        {
            vals = new double[0, 0];
            batches = new int[0];
            batchbases = new int[0];
            tmp0 = new double[0];
            tmp1 = new double[0];
            tmp2 = new double[0, 0];
        }
        public override apobject make_copy()
        {
            spline2dxdesignmatrix _result = new spline2dxdesignmatrix();
            _result.blockwidth = blockwidth;
            _result.kx = kx;
            _result.ky = ky;
            _result.npoints = npoints;
            _result.nrows = nrows;
            _result.ndenserows = ndenserows;
            _result.ndensebatches = ndensebatches;
            _result.d = d;
            _result.maxbatch = maxbatch;
            _result.vals = (double[,])vals.Clone();
            _result.batches = (int[])batches.Clone();
            _result.batchbases = (int[])batchbases.Clone();
            _result.lambdareg = lambdareg;
            _result.tmp0 = (double[])tmp0.Clone();
            _result.tmp1 = (double[])tmp1.Clone();
            _result.tmp2 = (double[,])tmp2.Clone();
            return _result;
        }
    };


    /*************************************************************************
    Temporaries for BlockLLS solver
    *************************************************************************/
    public class spline2dblockllsbuf : apobject
    {
        public linlsqr.linlsqrstate solver;
        public linlsqr.linlsqrreport solverrep;
        public double[,] blockata;
        public double[,] trsmbuf2;
        public double[,] cholbuf2;
        public double[] cholbuf1;
        public double[] tmp0;
        public double[] tmp1;
        public spline2dblockllsbuf()
        {
            init();
        }
        public override void init()
        {
            solver = new linlsqr.linlsqrstate();
            solverrep = new linlsqr.linlsqrreport();
            blockata = new double[0, 0];
            trsmbuf2 = new double[0, 0];
            cholbuf2 = new double[0, 0];
            cholbuf1 = new double[0];
            tmp0 = new double[0];
            tmp1 = new double[0];
        }
        public override apobject make_copy()
        {
            spline2dblockllsbuf _result = new spline2dblockllsbuf();
            _result.solver = (linlsqr.linlsqrstate)solver.make_copy();
            _result.solverrep = (linlsqr.linlsqrreport)solverrep.make_copy();
            _result.blockata = (double[,])blockata.Clone();
            _result.trsmbuf2 = (double[,])trsmbuf2.Clone();
            _result.cholbuf2 = (double[,])cholbuf2.Clone();
            _result.cholbuf1 = (double[])cholbuf1.Clone();
            _result.tmp0 = (double[])tmp0.Clone();
            _result.tmp1 = (double[])tmp1.Clone();
            return _result;
        }
    };


    /*************************************************************************
    Temporaries for FastDDM solver
    *************************************************************************/
    public class spline2dfastddmbuf : apobject
    {
        public spline2dxdesignmatrix xdesignmatrix;
        public double[] tmp0;
        public double[] tmpz;
        public spline2dfitreport dummyrep;
        public spline2dinterpolant localmodel;
        public spline2dblockllsbuf blockllsbuf;
        public spline2dfastddmbuf()
        {
            init();
        }
        public override void init()
        {
            xdesignmatrix = new spline2dxdesignmatrix();
            tmp0 = new double[0];
            tmpz = new double[0];
            dummyrep = new spline2dfitreport();
            localmodel = new spline2dinterpolant();
            blockllsbuf = new spline2dblockllsbuf();
        }
        public override apobject make_copy()
        {
            spline2dfastddmbuf _result = new spline2dfastddmbuf();
            _result.xdesignmatrix = (spline2dxdesignmatrix)xdesignmatrix.make_copy();
            _result.tmp0 = (double[])tmp0.Clone();
            _result.tmpz = (double[])tmpz.Clone();
            _result.dummyrep = (spline2dfitreport)dummyrep.make_copy();
            _result.localmodel = (spline2dinterpolant)localmodel.make_copy();
            _result.blockllsbuf = (spline2dblockllsbuf)blockllsbuf.make_copy();
            return _result;
        }
    };




    public const double cholreg = 1.0E-12;
    public const double lambdaregblocklls = 1.0E-6;
    public const double lambdaregfastddm = 1.0E-4;
    public const double lambdadecay = 0.5;


    /*************************************************************************
    This subroutine calculates the value of the bilinear or bicubic spline  at
    the given point X.

    Input parameters:
        C   -   2D spline object.
                Built by spline2dbuildbilinearv or spline2dbuildbicubicv.
        X, Y-   point

    Result:
        S(x,y)

      -- ALGLIB PROJECT --
         Copyright 05.07.2007 by Bochkanov Sergey
    *************************************************************************/
    public static double spline2dcalc(spline2dinterpolant c,
        double x,
        double y,
        xparams _params)
    {
        double result = 0;
        int ix = 0;
        int iy = 0;
        int l = 0;
        int r = 0;
        int h = 0;
        double t = 0;
        double dt = 0;
        double u = 0;
        double du = 0;
        double y1 = 0;
        double y2 = 0;
        double y3 = 0;
        double y4 = 0;
        int s1 = 0;
        int s2 = 0;
        int s3 = 0;
        int s4 = 0;
        int sfx = 0;
        int sfy = 0;
        int sfxy = 0;
        double t2 = 0;
        double t3 = 0;
        double u2 = 0;
        double u3 = 0;
        double ht00 = 0;
        double ht01 = 0;
        double ht10 = 0;
        double ht11 = 0;
        double hu00 = 0;
        double hu01 = 0;
        double hu10 = 0;
        double hu11 = 0;

        ap.assert(c.stype == -1 || c.stype == -3, "Spline2DCalc: incorrect C (incorrect parameter C.SType)");
        ap.assert(math.isfinite(x) && math.isfinite(y), "Spline2DCalc: X or Y contains NaN or Infinite value");
        if (c.d != 1)
        {
            result = 0;
            return result;
        }

        //
        // Determine evaluation interval
        //
        l = 0;
        r = c.n - 1;
        while (l != r - 1)
        {
            h = (l + r) / 2;
            if ((double)(c.x[h]) >= (double)(x))
            {
                r = h;
            }
            else
            {
                l = h;
            }
        }
        dt = 1.0 / (c.x[l + 1] - c.x[l]);
        t = (x - c.x[l]) * dt;
        ix = l;
        l = 0;
        r = c.m - 1;
        while (l != r - 1)
        {
            h = (l + r) / 2;
            if ((double)(c.y[h]) >= (double)(y))
            {
                r = h;
            }
            else
            {
                l = h;
            }
        }
        du = 1.0 / (c.y[l + 1] - c.y[l]);
        u = (y - c.y[l]) * du;
        iy = l;

        //
        // Handle possible missing cells
        //
        if (c.hasmissingcells && !adjustevaluationinterval(c, ref x, ref t, ref dt, ref ix, ref y, ref u, ref du, ref iy, _params))
        {
            result = Double.NaN;
            return result;
        }

        //
        // Bilinear interpolation
        //
        if (c.stype == -1)
        {
            y1 = c.f[c.n * iy + ix];
            y2 = c.f[c.n * iy + (ix + 1)];
            y3 = c.f[c.n * (iy + 1) + (ix + 1)];
            y4 = c.f[c.n * (iy + 1) + ix];
            result = (1 - t) * (1 - u) * y1 + t * (1 - u) * y2 + t * u * y3 + (1 - t) * u * y4;
            return result;
        }

        //
        // Bicubic interpolation:
        // * calculate Hermite basis for dimensions X and Y (variables T and U),
        //   here HTij means basis function whose I-th derivative has value 1 at T=J.
        //   Same for HUij.
        // * after initial calculation, apply scaling by DT/DU to the basis
        // * calculate using stored table of second derivatives
        //
        ap.assert(c.stype == -3, "Spline2DCalc: integrity check failed");
        sfx = c.n * c.m;
        sfy = 2 * c.n * c.m;
        sfxy = 3 * c.n * c.m;
        s1 = c.n * iy + ix;
        s2 = c.n * iy + (ix + 1);
        s3 = c.n * (iy + 1) + ix;
        s4 = c.n * (iy + 1) + (ix + 1);
        t2 = t * t;
        t3 = t * t2;
        u2 = u * u;
        u3 = u * u2;
        ht00 = 2 * t3 - 3 * t2 + 1;
        ht10 = t3 - 2 * t2 + t;
        ht01 = -(2 * t3) + 3 * t2;
        ht11 = t3 - t2;
        hu00 = 2 * u3 - 3 * u2 + 1;
        hu10 = u3 - 2 * u2 + u;
        hu01 = -(2 * u3) + 3 * u2;
        hu11 = u3 - u2;
        ht10 = ht10 / dt;
        ht11 = ht11 / dt;
        hu10 = hu10 / du;
        hu11 = hu11 / du;
        result = 0;
        result = result + c.f[s1] * ht00 * hu00 + c.f[s2] * ht01 * hu00 + c.f[s3] * ht00 * hu01 + c.f[s4] * ht01 * hu01;
        result = result + c.f[sfx + s1] * ht10 * hu00 + c.f[sfx + s2] * ht11 * hu00 + c.f[sfx + s3] * ht10 * hu01 + c.f[sfx + s4] * ht11 * hu01;
        result = result + c.f[sfy + s1] * ht00 * hu10 + c.f[sfy + s2] * ht01 * hu10 + c.f[sfy + s3] * ht00 * hu11 + c.f[sfy + s4] * ht01 * hu11;
        result = result + c.f[sfxy + s1] * ht10 * hu10 + c.f[sfxy + s2] * ht11 * hu10 + c.f[sfxy + s3] * ht10 * hu11 + c.f[sfxy + s4] * ht11 * hu11;
        return result;
    }


    /*************************************************************************
    This subroutine calculates the value of a bilinear or bicubic spline   and
    its derivatives.

    Use Spline2DDiff2() if you need second derivatives Sxx and Syy.

    Input parameters:
        C   -   spline interpolant.
        X, Y-   point

    Output parameters:
        F   -   S(x,y)
        FX  -   dS(x,y)/dX
        FY  -   dS(x,y)/dY

      -- ALGLIB PROJECT --
         Copyright 05.07.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2ddiff(spline2dinterpolant c,
        double x,
        double y,
        ref double f,
        ref double fx,
        ref double fy,
        xparams _params)
    {
        double t = 0;
        double dt = 0;
        double u = 0;
        double du = 0;
        int ix = 0;
        int iy = 0;
        int l = 0;
        int r = 0;
        int h = 0;
        int s1 = 0;
        int s2 = 0;
        int s3 = 0;
        int s4 = 0;
        int sfx = 0;
        int sfy = 0;
        int sfxy = 0;
        double y1 = 0;
        double y2 = 0;
        double y3 = 0;
        double y4 = 0;
        double v0 = 0;
        double v1 = 0;
        double v2 = 0;
        double v3 = 0;
        double t2 = 0;
        double t3 = 0;
        double u2 = 0;
        double u3 = 0;
        double ht00 = 0;
        double ht01 = 0;
        double ht10 = 0;
        double ht11 = 0;
        double hu00 = 0;
        double hu01 = 0;
        double hu10 = 0;
        double hu11 = 0;
        double dht00 = 0;
        double dht01 = 0;
        double dht10 = 0;
        double dht11 = 0;
        double dhu00 = 0;
        double dhu01 = 0;
        double dhu10 = 0;
        double dhu11 = 0;

        f = 0;
        fx = 0;
        fy = 0;

        ap.assert(c.stype == -1 || c.stype == -3, "Spline2DDiff: incorrect C (incorrect parameter C.SType)");
        ap.assert(math.isfinite(x) && math.isfinite(y), "Spline2DDiff: X or Y contains NaN or Infinite value");

        //
        // Prepare F, dF/dX, dF/dY, d2F/dXdY
        //
        f = 0;
        fx = 0;
        fy = 0;
        if (c.d != 1)
        {
            return;
        }

        //
        // Binary search in the [ x[0], ..., x[n-2] ] (x[n-1] is not included)
        //
        l = 0;
        r = c.n - 1;
        while (l != r - 1)
        {
            h = (l + r) / 2;
            if ((double)(c.x[h]) >= (double)(x))
            {
                r = h;
            }
            else
            {
                l = h;
            }
        }
        t = (x - c.x[l]) / (c.x[l + 1] - c.x[l]);
        dt = 1.0 / (c.x[l + 1] - c.x[l]);
        ix = l;

        //
        // Binary search in the [ y[0], ..., y[m-2] ] (y[m-1] is not included)
        //
        l = 0;
        r = c.m - 1;
        while (l != r - 1)
        {
            h = (l + r) / 2;
            if ((double)(c.y[h]) >= (double)(y))
            {
                r = h;
            }
            else
            {
                l = h;
            }
        }
        u = (y - c.y[l]) / (c.y[l + 1] - c.y[l]);
        du = 1.0 / (c.y[l + 1] - c.y[l]);
        iy = l;

        //
        // Handle possible missing cells
        //
        if (c.hasmissingcells && !adjustevaluationinterval(c, ref x, ref t, ref dt, ref ix, ref y, ref u, ref du, ref iy, _params))
        {
            f = Double.NaN;
            fx = Double.NaN;
            fy = Double.NaN;
            return;
        }

        //
        // Bilinear interpolation
        //
        if (c.stype == -1)
        {
            y1 = c.f[c.n * iy + ix];
            y2 = c.f[c.n * iy + (ix + 1)];
            y3 = c.f[c.n * (iy + 1) + (ix + 1)];
            y4 = c.f[c.n * (iy + 1) + ix];
            f = (1 - t) * (1 - u) * y1 + t * (1 - u) * y2 + t * u * y3 + (1 - t) * u * y4;
            fx = (-((1 - u) * y1) + (1 - u) * y2 + u * y3 - u * y4) * dt;
            fy = (-((1 - t) * y1) - t * y2 + t * y3 + (1 - t) * y4) * du;
            return;
        }

        //
        // Bicubic interpolation
        //
        if (c.stype == -3)
        {
            sfx = c.n * c.m;
            sfy = 2 * c.n * c.m;
            sfxy = 3 * c.n * c.m;
            s1 = c.n * iy + ix;
            s2 = c.n * iy + (ix + 1);
            s3 = c.n * (iy + 1) + ix;
            s4 = c.n * (iy + 1) + (ix + 1);
            t2 = t * t;
            t3 = t * t2;
            u2 = u * u;
            u3 = u * u2;
            ht00 = 2 * t3 - 3 * t2 + 1;
            ht10 = t3 - 2 * t2 + t;
            ht01 = -(2 * t3) + 3 * t2;
            ht11 = t3 - t2;
            hu00 = 2 * u3 - 3 * u2 + 1;
            hu10 = u3 - 2 * u2 + u;
            hu01 = -(2 * u3) + 3 * u2;
            hu11 = u3 - u2;
            ht10 = ht10 / dt;
            ht11 = ht11 / dt;
            hu10 = hu10 / du;
            hu11 = hu11 / du;
            dht00 = 6 * t2 - 6 * t;
            dht10 = 3 * t2 - 4 * t + 1;
            dht01 = -(6 * t2) + 6 * t;
            dht11 = 3 * t2 - 2 * t;
            dhu00 = 6 * u2 - 6 * u;
            dhu10 = 3 * u2 - 4 * u + 1;
            dhu01 = -(6 * u2) + 6 * u;
            dhu11 = 3 * u2 - 2 * u;
            dht00 = dht00 * dt;
            dht01 = dht01 * dt;
            dhu00 = dhu00 * du;
            dhu01 = dhu01 * du;
            f = 0;
            fx = 0;
            fy = 0;
            v0 = c.f[s1];
            v1 = c.f[s2];
            v2 = c.f[s3];
            v3 = c.f[s4];
            f = f + v0 * ht00 * hu00 + v1 * ht01 * hu00 + v2 * ht00 * hu01 + v3 * ht01 * hu01;
            fx = fx + v0 * dht00 * hu00 + v1 * dht01 * hu00 + v2 * dht00 * hu01 + v3 * dht01 * hu01;
            fy = fy + v0 * ht00 * dhu00 + v1 * ht01 * dhu00 + v2 * ht00 * dhu01 + v3 * ht01 * dhu01;
            v0 = c.f[sfx + s1];
            v1 = c.f[sfx + s2];
            v2 = c.f[sfx + s3];
            v3 = c.f[sfx + s4];
            f = f + v0 * ht10 * hu00 + v1 * ht11 * hu00 + v2 * ht10 * hu01 + v3 * ht11 * hu01;
            fx = fx + v0 * dht10 * hu00 + v1 * dht11 * hu00 + v2 * dht10 * hu01 + v3 * dht11 * hu01;
            fy = fy + v0 * ht10 * dhu00 + v1 * ht11 * dhu00 + v2 * ht10 * dhu01 + v3 * ht11 * dhu01;
            v0 = c.f[sfy + s1];
            v1 = c.f[sfy + s2];
            v2 = c.f[sfy + s3];
            v3 = c.f[sfy + s4];
            f = f + v0 * ht00 * hu10 + v1 * ht01 * hu10 + v2 * ht00 * hu11 + v3 * ht01 * hu11;
            fx = fx + v0 * dht00 * hu10 + v1 * dht01 * hu10 + v2 * dht00 * hu11 + v3 * dht01 * hu11;
            fy = fy + v0 * ht00 * dhu10 + v1 * ht01 * dhu10 + v2 * ht00 * dhu11 + v3 * ht01 * dhu11;
            v0 = c.f[sfxy + s1];
            v1 = c.f[sfxy + s2];
            v2 = c.f[sfxy + s3];
            v3 = c.f[sfxy + s4];
            f = f + v0 * ht10 * hu10 + v1 * ht11 * hu10 + v2 * ht10 * hu11 + v3 * ht11 * hu11;
            fx = fx + v0 * dht10 * hu10 + v1 * dht11 * hu10 + v2 * dht10 * hu11 + v3 * dht11 * hu11;
            fy = fy + v0 * ht10 * dhu10 + v1 * ht11 * dhu10 + v2 * ht10 * dhu11 + v3 * ht11 * dhu11;
            return;
        }
    }


    /*************************************************************************
    This subroutine calculates the value of a bilinear or bicubic spline   and
    its second derivatives.

    Input parameters:
        C   -   spline interpolant.
        X, Y-   point

    Output parameters:
        F   -   S(x,y)
        FX  -   dS(x,y)/dX
        FY  -   dS(x,y)/dY
        FXX -   d2S(x,y)/dXdX
        FXY -   d2S(x,y)/dXdY
        FYY -   d2S(x,y)/dYdY

      -- ALGLIB PROJECT --
         Copyright 17.04.2023 by Bochkanov Sergey.
         
         The second derivatives code was contributed by  Horst  Greiner  under
         public domain terms.
    *************************************************************************/
    public static void spline2ddiff2(spline2dinterpolant c,
        double x,
        double y,
        ref double f,
        ref double fx,
        ref double fy,
        ref double fxx,
        ref double fxy,
        ref double fyy,
        xparams _params)
    {
        double t = 0;
        double dt = 0;
        double u = 0;
        double du = 0;
        int ix = 0;
        int iy = 0;
        int l = 0;
        int r = 0;
        int h = 0;
        int s1 = 0;
        int s2 = 0;
        int s3 = 0;
        int s4 = 0;
        int sfx = 0;
        int sfy = 0;
        int sfxy = 0;
        double y1 = 0;
        double y2 = 0;
        double y3 = 0;
        double y4 = 0;
        double v0 = 0;
        double v1 = 0;
        double v2 = 0;
        double v3 = 0;
        double t2 = 0;
        double t3 = 0;
        double u2 = 0;
        double u3 = 0;
        double ht00 = 0;
        double ht01 = 0;
        double ht10 = 0;
        double ht11 = 0;
        double hu00 = 0;
        double hu01 = 0;
        double hu10 = 0;
        double hu11 = 0;
        double dht00 = 0;
        double dht01 = 0;
        double dht10 = 0;
        double dht11 = 0;
        double dhu00 = 0;
        double dhu01 = 0;
        double dhu10 = 0;
        double dhu11 = 0;
        double d2ht00 = 0;
        double d2ht01 = 0;
        double d2ht10 = 0;
        double d2ht11 = 0;
        double d2hu00 = 0;
        double d2hu01 = 0;
        double d2hu10 = 0;
        double d2hu11 = 0;

        f = 0;
        fx = 0;
        fy = 0;
        fxx = 0;
        fxy = 0;
        fyy = 0;

        ap.assert(c.stype == -1 || c.stype == -3, "Spline2DDiff: incorrect C (incorrect parameter C.SType)");
        ap.assert(math.isfinite(x) && math.isfinite(y), "Spline2DDiff: X or Y contains NaN or Infinite value");

        //
        // Prepare F, dF/dX, dF/dY, d2F/dXdY
        //
        f = 0;
        fx = 0;
        fy = 0;
        fxx = 0;
        fxy = 0;
        fyy = 0;
        if (c.d != 1)
        {
            return;
        }

        //
        // Binary search in the [ x[0], ..., x[n-2] ] (x[n-1] is not included)
        //
        l = 0;
        r = c.n - 1;
        while (l != r - 1)
        {
            h = (l + r) / 2;
            if ((double)(c.x[h]) >= (double)(x))
            {
                r = h;
            }
            else
            {
                l = h;
            }
        }
        t = (x - c.x[l]) / (c.x[l + 1] - c.x[l]);
        dt = 1.0 / (c.x[l + 1] - c.x[l]);
        ix = l;

        //
        // Binary search in the [ y[0], ..., y[m-2] ] (y[m-1] is not included)
        //
        l = 0;
        r = c.m - 1;
        while (l != r - 1)
        {
            h = (l + r) / 2;
            if ((double)(c.y[h]) >= (double)(y))
            {
                r = h;
            }
            else
            {
                l = h;
            }
        }
        u = (y - c.y[l]) / (c.y[l + 1] - c.y[l]);
        du = 1.0 / (c.y[l + 1] - c.y[l]);
        iy = l;

        //
        // Handle possible missing cells
        //
        if (c.hasmissingcells && !adjustevaluationinterval(c, ref x, ref t, ref dt, ref ix, ref y, ref u, ref du, ref iy, _params))
        {
            f = Double.NaN;
            fx = Double.NaN;
            fy = Double.NaN;
            fxx = Double.NaN;
            fxy = Double.NaN;
            fyy = Double.NaN;
            return;
        }

        //
        // Bilinear interpolation
        //
        if (c.stype == -1)
        {
            y1 = c.f[c.n * iy + ix];
            y2 = c.f[c.n * iy + (ix + 1)];
            y3 = c.f[c.n * (iy + 1) + (ix + 1)];
            y4 = c.f[c.n * (iy + 1) + ix];
            f = (1 - t) * (1 - u) * y1 + t * (1 - u) * y2 + t * u * y3 + (1 - t) * u * y4;
            fx = (-((1 - u) * y1) + (1 - u) * y2 + u * y3 - u * y4) * dt;
            fy = (-((1 - t) * y1) - t * y2 + t * y3 + (1 - t) * y4) * du;
            fxx = 0;
            fxy = (y1 - y2 + y3 - y4) * du * dt;
            fyy = 0;
            return;
        }

        //
        // Bicubic interpolation
        //
        if (c.stype == -3)
        {
            sfx = c.n * c.m;
            sfy = 2 * c.n * c.m;
            sfxy = 3 * c.n * c.m;
            s1 = c.n * iy + ix;
            s2 = c.n * iy + (ix + 1);
            s3 = c.n * (iy + 1) + ix;
            s4 = c.n * (iy + 1) + (ix + 1);
            t2 = t * t;
            t3 = t * t2;
            u2 = u * u;
            u3 = u * u2;
            ht00 = 2 * t3 - 3 * t2 + 1;
            ht10 = t3 - 2 * t2 + t;
            ht01 = -(2 * t3) + 3 * t2;
            ht11 = t3 - t2;
            hu00 = 2 * u3 - 3 * u2 + 1;
            hu10 = u3 - 2 * u2 + u;
            hu01 = -(2 * u3) + 3 * u2;
            hu11 = u3 - u2;
            ht10 = ht10 / dt;
            ht11 = ht11 / dt;
            hu10 = hu10 / du;
            hu11 = hu11 / du;
            dht00 = 6 * t2 - 6 * t;
            dht10 = 3 * t2 - 4 * t + 1;
            dht01 = -(6 * t2) + 6 * t;
            dht11 = 3 * t2 - 2 * t;
            dhu00 = 6 * u2 - 6 * u;
            dhu10 = 3 * u2 - 4 * u + 1;
            dhu01 = -(6 * u2) + 6 * u;
            dhu11 = 3 * u2 - 2 * u;
            dht00 = dht00 * dt;
            dht01 = dht01 * dt;
            dhu00 = dhu00 * du;
            dhu01 = dhu01 * du;
            d2ht00 = (12 * t - 6) * dt * dt;
            d2ht01 = (-(12 * t) + 6) * dt * dt;
            d2ht10 = (6 * t - 4) * dt;
            d2ht11 = (6 * t - 2) * dt;
            d2hu00 = (12 * u - 6) * du * du;
            d2hu01 = (-(12 * u) + 6) * du * du;
            d2hu10 = (6 * u - 4) * du;
            d2hu11 = (6 * u - 2) * du;
            f = 0;
            fx = 0;
            fy = 0;
            fxy = 0;
            v0 = c.f[s1];
            v1 = c.f[s2];
            v2 = c.f[s3];
            v3 = c.f[s4];
            f = f + v0 * ht00 * hu00 + v1 * ht01 * hu00 + v2 * ht00 * hu01 + v3 * ht01 * hu01;
            fx = fx + v0 * dht00 * hu00 + v1 * dht01 * hu00 + v2 * dht00 * hu01 + v3 * dht01 * hu01;
            fy = fy + v0 * ht00 * dhu00 + v1 * ht01 * dhu00 + v2 * ht00 * dhu01 + v3 * ht01 * dhu01;
            fxx = fxx + v0 * d2ht00 * hu00 + v1 * d2ht01 * hu00 + v2 * d2ht00 * hu01 + v3 * d2ht01 * hu01;
            fxy = fxy + v0 * dht00 * dhu00 + v1 * dht01 * dhu00 + v2 * dht00 * dhu01 + v3 * dht01 * dhu01;
            fyy = fyy + v0 * ht00 * d2hu00 + v1 * ht01 * d2hu00 + v2 * ht00 * d2hu01 + v3 * ht01 * d2hu01;
            v0 = c.f[sfx + s1];
            v1 = c.f[sfx + s2];
            v2 = c.f[sfx + s3];
            v3 = c.f[sfx + s4];
            f = f + v0 * ht10 * hu00 + v1 * ht11 * hu00 + v2 * ht10 * hu01 + v3 * ht11 * hu01;
            fx = fx + v0 * dht10 * hu00 + v1 * dht11 * hu00 + v2 * dht10 * hu01 + v3 * dht11 * hu01;
            fy = fy + v0 * ht10 * dhu00 + v1 * ht11 * dhu00 + v2 * ht10 * dhu01 + v3 * ht11 * dhu01;
            fxx = fxx + v0 * d2ht10 * hu00 + v1 * d2ht11 * hu00 + v2 * d2ht10 * hu01 + v3 * d2ht11 * hu01;
            fxy = fxy + v0 * dht10 * dhu00 + v1 * dht11 * dhu00 + v2 * dht10 * dhu01 + v3 * dht11 * dhu01;
            fyy = fyy + v0 * ht10 * d2hu00 + v1 * ht11 * d2hu00 + v2 * ht10 * d2hu01 + v3 * ht11 * d2hu01;
            v0 = c.f[sfy + s1];
            v1 = c.f[sfy + s2];
            v2 = c.f[sfy + s3];
            v3 = c.f[sfy + s4];
            f = f + v0 * ht00 * hu10 + v1 * ht01 * hu10 + v2 * ht00 * hu11 + v3 * ht01 * hu11;
            fx = fx + v0 * dht00 * hu10 + v1 * dht01 * hu10 + v2 * dht00 * hu11 + v3 * dht01 * hu11;
            fy = fy + v0 * ht00 * dhu10 + v1 * ht01 * dhu10 + v2 * ht00 * dhu11 + v3 * ht01 * dhu11;
            fxx = fxx + v0 * d2ht00 * hu10 + v1 * d2ht01 * hu10 + v2 * d2ht00 * hu11 + v3 * d2ht01 * hu11;
            fxy = fxy + v0 * dht00 * dhu10 + v1 * dht01 * dhu10 + v2 * dht00 * dhu11 + v3 * dht01 * dhu11;
            fyy = fyy + v0 * ht00 * d2hu10 + v1 * ht01 * d2hu10 + v2 * ht00 * d2hu11 + v3 * ht01 * d2hu11;
            v0 = c.f[sfxy + s1];
            v1 = c.f[sfxy + s2];
            v2 = c.f[sfxy + s3];
            v3 = c.f[sfxy + s4];
            f = f + v0 * ht10 * hu10 + v1 * ht11 * hu10 + v2 * ht10 * hu11 + v3 * ht11 * hu11;
            fx = fx + v0 * dht10 * hu10 + v1 * dht11 * hu10 + v2 * dht10 * hu11 + v3 * dht11 * hu11;
            fy = fy + v0 * ht10 * dhu10 + v1 * ht11 * dhu10 + v2 * ht10 * dhu11 + v3 * ht11 * dhu11;
            fxx = fxx + v0 * d2ht10 * hu10 + v1 * d2ht11 * hu10 + v2 * d2ht10 * hu11 + v3 * d2ht11 * hu11;
            fxy = fxy + v0 * dht10 * dhu10 + v1 * dht11 * dhu10 + v2 * dht10 * dhu11 + v3 * dht11 * dhu11;
            fyy = fyy + v0 * ht10 * d2hu10 + v1 * ht11 * d2hu10 + v2 * ht10 * d2hu11 + v3 * ht11 * d2hu11;
            return;
        }
    }


    /*************************************************************************
    This subroutine calculates bilinear or bicubic vector-valued spline at the
    given point (X,Y).

    If you need just some specific component of vector-valued spline, you  can
    use spline2dcalcvi() function.

    INPUT PARAMETERS:
        C   -   spline interpolant.
        X, Y-   point
        F   -   output buffer, possibly preallocated array. In case array size
                is large enough to store result, it is not reallocated.  Array
                which is too short will be reallocated

    OUTPUT PARAMETERS:
        F   -   array[D] (or larger) which stores function values

      -- ALGLIB PROJECT --
         Copyright 01.02.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dcalcvbuf(spline2dinterpolant c,
        double x,
        double y,
        ref double[] f,
        xparams _params)
    {
        int ix = 0;
        int iy = 0;
        int l = 0;
        int r = 0;
        int h = 0;
        int i = 0;
        double t = 0;
        double dt = 0;
        double u = 0;
        double du = 0;
        double y1 = 0;
        double y2 = 0;
        double y3 = 0;
        double y4 = 0;
        int s1 = 0;
        int s2 = 0;
        int s3 = 0;
        int s4 = 0;
        int sfx = 0;
        int sfy = 0;
        int sfxy = 0;
        double t2 = 0;
        double t3 = 0;
        double u2 = 0;
        double u3 = 0;
        double ht00 = 0;
        double ht01 = 0;
        double ht10 = 0;
        double ht11 = 0;
        double hu00 = 0;
        double hu01 = 0;
        double hu10 = 0;
        double hu11 = 0;

        ap.assert(c.stype == -1 || c.stype == -3, "Spline2DCalcVBuf: incorrect C (incorrect parameter C.SType)");
        ap.assert(math.isfinite(x) && math.isfinite(y), "Spline2DCalcVBuf: X or Y contains NaN or Infinite value");

        //
        // Allocate place for output
        //
        apserv.rvectorsetlengthatleast(ref f, c.d, _params);

        //
        // Determine evaluation interval
        //
        l = 0;
        r = c.n - 1;
        while (l != r - 1)
        {
            h = (l + r) / 2;
            if ((double)(c.x[h]) >= (double)(x))
            {
                r = h;
            }
            else
            {
                l = h;
            }
        }
        dt = 1.0 / (c.x[l + 1] - c.x[l]);
        t = (x - c.x[l]) * dt;
        ix = l;
        l = 0;
        r = c.m - 1;
        while (l != r - 1)
        {
            h = (l + r) / 2;
            if ((double)(c.y[h]) >= (double)(y))
            {
                r = h;
            }
            else
            {
                l = h;
            }
        }
        du = 1.0 / (c.y[l + 1] - c.y[l]);
        u = (y - c.y[l]) * du;
        iy = l;

        //
        // Handle possible missing cells
        //
        if (c.hasmissingcells && !adjustevaluationinterval(c, ref x, ref t, ref dt, ref ix, ref y, ref u, ref du, ref iy, _params))
        {
            ablasf.rsetv(c.d, Double.NaN, f, _params);
            return;
        }

        //
        // Bilinear interpolation
        //
        if (c.stype == -1)
        {
            for (i = 0; i <= c.d - 1; i++)
            {
                y1 = c.f[c.d * (c.n * iy + ix) + i];
                y2 = c.f[c.d * (c.n * iy + (ix + 1)) + i];
                y3 = c.f[c.d * (c.n * (iy + 1) + (ix + 1)) + i];
                y4 = c.f[c.d * (c.n * (iy + 1) + ix) + i];
                f[i] = (1 - t) * (1 - u) * y1 + t * (1 - u) * y2 + t * u * y3 + (1 - t) * u * y4;
            }
            return;
        }

        //
        // Bicubic interpolation:
        // * calculate Hermite basis for dimensions X and Y (variables T and U),
        //   here HTij means basis function whose I-th derivative has value 1 at T=J.
        //   Same for HUij.
        // * after initial calculation, apply scaling by DT/DU to the basis
        // * calculate using stored table of second derivatives
        //
        ap.assert(c.stype == -3, "Spline2DCalc: integrity check failed");
        sfx = c.n * c.m * c.d;
        sfy = 2 * c.n * c.m * c.d;
        sfxy = 3 * c.n * c.m * c.d;
        s1 = (c.n * iy + ix) * c.d;
        s2 = (c.n * iy + (ix + 1)) * c.d;
        s3 = (c.n * (iy + 1) + ix) * c.d;
        s4 = (c.n * (iy + 1) + (ix + 1)) * c.d;
        t2 = t * t;
        t3 = t * t2;
        u2 = u * u;
        u3 = u * u2;
        ht00 = 2 * t3 - 3 * t2 + 1;
        ht10 = t3 - 2 * t2 + t;
        ht01 = -(2 * t3) + 3 * t2;
        ht11 = t3 - t2;
        hu00 = 2 * u3 - 3 * u2 + 1;
        hu10 = u3 - 2 * u2 + u;
        hu01 = -(2 * u3) + 3 * u2;
        hu11 = u3 - u2;
        ht10 = ht10 / dt;
        ht11 = ht11 / dt;
        hu10 = hu10 / du;
        hu11 = hu11 / du;
        for (i = 0; i <= c.d - 1; i++)
        {

            //
            // Calculate I-th component
            //
            f[i] = 0;
            f[i] = f[i] + c.f[s1] * ht00 * hu00 + c.f[s2] * ht01 * hu00 + c.f[s3] * ht00 * hu01 + c.f[s4] * ht01 * hu01;
            f[i] = f[i] + c.f[sfx + s1] * ht10 * hu00 + c.f[sfx + s2] * ht11 * hu00 + c.f[sfx + s3] * ht10 * hu01 + c.f[sfx + s4] * ht11 * hu01;
            f[i] = f[i] + c.f[sfy + s1] * ht00 * hu10 + c.f[sfy + s2] * ht01 * hu10 + c.f[sfy + s3] * ht00 * hu11 + c.f[sfy + s4] * ht01 * hu11;
            f[i] = f[i] + c.f[sfxy + s1] * ht10 * hu10 + c.f[sfxy + s2] * ht11 * hu10 + c.f[sfxy + s3] * ht10 * hu11 + c.f[sfxy + s4] * ht11 * hu11;

            //
            // Advance source indexes
            //
            s1 = s1 + 1;
            s2 = s2 + 1;
            s3 = s3 + 1;
            s4 = s4 + 1;
        }
    }


    /*************************************************************************
    This subroutine calculates specific component of vector-valued bilinear or
    bicubic spline at the given point (X,Y).

    INPUT PARAMETERS:
        C   -   spline interpolant.
        X, Y-   point
        I   -   component index, in [0,D). An exception is generated for out
                of range values.

    RESULT:
        value of I-th component

      -- ALGLIB PROJECT --
         Copyright 01.02.2018 by Bochkanov Sergey
    *************************************************************************/
    public static double spline2dcalcvi(spline2dinterpolant c,
        double x,
        double y,
        int i,
        xparams _params)
    {
        double result = 0;
        int ix = 0;
        int iy = 0;
        int l = 0;
        int r = 0;
        int h = 0;
        double t = 0;
        double dt = 0;
        double u = 0;
        double du = 0;
        double y1 = 0;
        double y2 = 0;
        double y3 = 0;
        double y4 = 0;
        int s1 = 0;
        int s2 = 0;
        int s3 = 0;
        int s4 = 0;
        int sfx = 0;
        int sfy = 0;
        int sfxy = 0;
        double t2 = 0;
        double t3 = 0;
        double u2 = 0;
        double u3 = 0;
        double ht00 = 0;
        double ht01 = 0;
        double ht10 = 0;
        double ht11 = 0;
        double hu00 = 0;
        double hu01 = 0;
        double hu10 = 0;
        double hu11 = 0;

        ap.assert(c.stype == -1 || c.stype == -3, "Spline2DCalcVi: incorrect C (incorrect parameter C.SType)");
        ap.assert(math.isfinite(x) && math.isfinite(y), "Spline2DCalcVi: X or Y contains NaN or Infinite value");
        ap.assert(i >= 0 && i < c.d, "Spline2DCalcVi: incorrect I (I<0 or I>=D)");

        //
        // Determine evaluation interval
        //
        l = 0;
        r = c.n - 1;
        while (l != r - 1)
        {
            h = (l + r) / 2;
            if ((double)(c.x[h]) >= (double)(x))
            {
                r = h;
            }
            else
            {
                l = h;
            }
        }
        dt = 1.0 / (c.x[l + 1] - c.x[l]);
        t = (x - c.x[l]) * dt;
        ix = l;
        l = 0;
        r = c.m - 1;
        while (l != r - 1)
        {
            h = (l + r) / 2;
            if ((double)(c.y[h]) >= (double)(y))
            {
                r = h;
            }
            else
            {
                l = h;
            }
        }
        du = 1.0 / (c.y[l + 1] - c.y[l]);
        u = (y - c.y[l]) * du;
        iy = l;

        //
        // Handle possible missing cells
        //
        if (c.hasmissingcells && !adjustevaluationinterval(c, ref x, ref t, ref dt, ref ix, ref y, ref u, ref du, ref iy, _params))
        {
            result = Double.NaN;
            return result;
        }

        //
        // Bilinear interpolation
        //
        if (c.stype == -1)
        {
            y1 = c.f[c.d * (c.n * iy + ix) + i];
            y2 = c.f[c.d * (c.n * iy + (ix + 1)) + i];
            y3 = c.f[c.d * (c.n * (iy + 1) + (ix + 1)) + i];
            y4 = c.f[c.d * (c.n * (iy + 1) + ix) + i];
            result = (1 - t) * (1 - u) * y1 + t * (1 - u) * y2 + t * u * y3 + (1 - t) * u * y4;
            return result;
        }

        //
        // Bicubic interpolation:
        // * calculate Hermite basis for dimensions X and Y (variables T and U),
        //   here HTij means basis function whose I-th derivative has value 1 at T=J.
        //   Same for HUij.
        // * after initial calculation, apply scaling by DT/DU to the basis
        // * calculate using stored table of second derivatives
        //
        ap.assert(c.stype == -3, "Spline2DCalc: integrity check failed");
        sfx = c.n * c.m * c.d;
        sfy = 2 * c.n * c.m * c.d;
        sfxy = 3 * c.n * c.m * c.d;
        s1 = (c.n * iy + ix) * c.d;
        s2 = (c.n * iy + (ix + 1)) * c.d;
        s3 = (c.n * (iy + 1) + ix) * c.d;
        s4 = (c.n * (iy + 1) + (ix + 1)) * c.d;
        t2 = t * t;
        t3 = t * t2;
        u2 = u * u;
        u3 = u * u2;
        ht00 = 2 * t3 - 3 * t2 + 1;
        ht10 = t3 - 2 * t2 + t;
        ht01 = -(2 * t3) + 3 * t2;
        ht11 = t3 - t2;
        hu00 = 2 * u3 - 3 * u2 + 1;
        hu10 = u3 - 2 * u2 + u;
        hu01 = -(2 * u3) + 3 * u2;
        hu11 = u3 - u2;
        ht10 = ht10 / dt;
        ht11 = ht11 / dt;
        hu10 = hu10 / du;
        hu11 = hu11 / du;

        //
        // Advance source indexes to I-th position
        //
        s1 = s1 + i;
        s2 = s2 + i;
        s3 = s3 + i;
        s4 = s4 + i;

        //
        // Calculate I-th component
        //
        result = 0;
        result = result + c.f[s1] * ht00 * hu00 + c.f[s2] * ht01 * hu00 + c.f[s3] * ht00 * hu01 + c.f[s4] * ht01 * hu01;
        result = result + c.f[sfx + s1] * ht10 * hu00 + c.f[sfx + s2] * ht11 * hu00 + c.f[sfx + s3] * ht10 * hu01 + c.f[sfx + s4] * ht11 * hu01;
        result = result + c.f[sfy + s1] * ht00 * hu10 + c.f[sfy + s2] * ht01 * hu10 + c.f[sfy + s3] * ht00 * hu11 + c.f[sfy + s4] * ht01 * hu11;
        result = result + c.f[sfxy + s1] * ht10 * hu10 + c.f[sfxy + s2] * ht11 * hu10 + c.f[sfxy + s3] * ht10 * hu11 + c.f[sfxy + s4] * ht11 * hu11;
        return result;
    }


    /*************************************************************************
    This subroutine calculates bilinear or bicubic vector-valued spline at the
    given point (X,Y).

    INPUT PARAMETERS:
        C   -   spline interpolant.
        X, Y-   point

    OUTPUT PARAMETERS:
        F   -   array[D] which stores function values.  F is out-parameter and
                it  is  reallocated  after  call to this function. In case you
                want  to    reuse  previously  allocated  F,   you   may   use
                Spline2DCalcVBuf(),  which  reallocates  F only when it is too
                small.

      -- ALGLIB PROJECT --
         Copyright 16.04.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dcalcv(spline2dinterpolant c,
        double x,
        double y,
        ref double[] f,
        xparams _params)
    {
        f = new double[0];

        ap.assert(c.stype == -1 || c.stype == -3, "Spline2DCalcV: incorrect C (incorrect parameter C.SType)");
        ap.assert(math.isfinite(x) && math.isfinite(y), "Spline2DCalcV: either X=NaN/Infinite or Y=NaN/Infinite");
        spline2dcalcvbuf(c, x, y, ref f, _params);
    }


    /*************************************************************************
    This subroutine calculates the value and the derivatives of I-th component
    of a  vector-valued bilinear or bicubic spline.

    Input parameters:
        C   -   spline interpolant.
        X, Y-   point
        I   -   component index, in [0,D)

    Output parameters:
        F   -   S(x,y)
        FX  -   dS(x,y)/dX
        FY  -   dS(x,y)/dY

      -- ALGLIB PROJECT --
         Copyright 05.07.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2ddiffvi(spline2dinterpolant c,
        double x,
        double y,
        int i,
        ref double f,
        ref double fx,
        ref double fy,
        xparams _params)
    {
        int d = 0;
        double t = 0;
        double dt = 0;
        double u = 0;
        double du = 0;
        int ix = 0;
        int iy = 0;
        int l = 0;
        int r = 0;
        int h = 0;
        int s1 = 0;
        int s2 = 0;
        int s3 = 0;
        int s4 = 0;
        int sfx = 0;
        int sfy = 0;
        int sfxy = 0;
        double y1 = 0;
        double y2 = 0;
        double y3 = 0;
        double y4 = 0;
        double v0 = 0;
        double v1 = 0;
        double v2 = 0;
        double v3 = 0;
        double t2 = 0;
        double t3 = 0;
        double u2 = 0;
        double u3 = 0;
        double ht00 = 0;
        double ht01 = 0;
        double ht10 = 0;
        double ht11 = 0;
        double hu00 = 0;
        double hu01 = 0;
        double hu10 = 0;
        double hu11 = 0;
        double dht00 = 0;
        double dht01 = 0;
        double dht10 = 0;
        double dht11 = 0;
        double dhu00 = 0;
        double dhu01 = 0;
        double dhu10 = 0;
        double dhu11 = 0;

        f = 0;
        fx = 0;
        fy = 0;

        ap.assert(c.stype == -1 || c.stype == -3, "Spline2DDiffVI: incorrect C (incorrect parameter C.SType)");
        ap.assert(math.isfinite(x) && math.isfinite(y), "Spline2DDiffVI: X or Y contains NaN or Infinite value");
        ap.assert(i >= 0 && i < c.d, "Spline2DDiffVI: I<0 or I>=D");

        //
        // Prepare F, dF/dX, dF/dY, d2F/dXdY
        //
        f = 0;
        fx = 0;
        fy = 0;
        d = c.d;

        //
        // Binary search in the [ x[0], ..., x[n-2] ] (x[n-1] is not included)
        //
        l = 0;
        r = c.n - 1;
        while (l != r - 1)
        {
            h = (l + r) / 2;
            if ((double)(c.x[h]) >= (double)(x))
            {
                r = h;
            }
            else
            {
                l = h;
            }
        }
        t = (x - c.x[l]) / (c.x[l + 1] - c.x[l]);
        dt = 1.0 / (c.x[l + 1] - c.x[l]);
        ix = l;

        //
        // Binary search in the [ y[0], ..., y[m-2] ] (y[m-1] is not included)
        //
        l = 0;
        r = c.m - 1;
        while (l != r - 1)
        {
            h = (l + r) / 2;
            if ((double)(c.y[h]) >= (double)(y))
            {
                r = h;
            }
            else
            {
                l = h;
            }
        }
        u = (y - c.y[l]) / (c.y[l + 1] - c.y[l]);
        du = 1.0 / (c.y[l + 1] - c.y[l]);
        iy = l;

        //
        // Handle possible missing cells
        //
        if (c.hasmissingcells && !adjustevaluationinterval(c, ref x, ref t, ref dt, ref ix, ref y, ref u, ref du, ref iy, _params))
        {
            f = Double.NaN;
            fx = Double.NaN;
            fy = Double.NaN;
            return;
        }

        //
        // Bilinear interpolation
        //
        if (c.stype == -1)
        {
            y1 = c.f[d * (c.n * iy + ix) + i];
            y2 = c.f[d * (c.n * iy + (ix + 1)) + i];
            y3 = c.f[d * (c.n * (iy + 1) + (ix + 1)) + i];
            y4 = c.f[d * (c.n * (iy + 1) + ix) + i];
            f = (1 - t) * (1 - u) * y1 + t * (1 - u) * y2 + t * u * y3 + (1 - t) * u * y4;
            fx = (-((1 - u) * y1) + (1 - u) * y2 + u * y3 - u * y4) * dt;
            fy = (-((1 - t) * y1) - t * y2 + t * y3 + (1 - t) * y4) * du;
            return;
        }

        //
        // Bicubic interpolation
        //
        if (c.stype == -3)
        {
            sfx = c.n * c.m * d;
            sfy = 2 * c.n * c.m * d;
            sfxy = 3 * c.n * c.m * d;
            s1 = d * (c.n * iy + ix) + i;
            s2 = d * (c.n * iy + (ix + 1)) + i;
            s3 = d * (c.n * (iy + 1) + ix) + i;
            s4 = d * (c.n * (iy + 1) + (ix + 1)) + i;
            t2 = t * t;
            t3 = t * t2;
            u2 = u * u;
            u3 = u * u2;
            ht00 = 2 * t3 - 3 * t2 + 1;
            ht10 = t3 - 2 * t2 + t;
            ht01 = -(2 * t3) + 3 * t2;
            ht11 = t3 - t2;
            hu00 = 2 * u3 - 3 * u2 + 1;
            hu10 = u3 - 2 * u2 + u;
            hu01 = -(2 * u3) + 3 * u2;
            hu11 = u3 - u2;
            ht10 = ht10 / dt;
            ht11 = ht11 / dt;
            hu10 = hu10 / du;
            hu11 = hu11 / du;
            dht00 = 6 * t2 - 6 * t;
            dht10 = 3 * t2 - 4 * t + 1;
            dht01 = -(6 * t2) + 6 * t;
            dht11 = 3 * t2 - 2 * t;
            dhu00 = 6 * u2 - 6 * u;
            dhu10 = 3 * u2 - 4 * u + 1;
            dhu01 = -(6 * u2) + 6 * u;
            dhu11 = 3 * u2 - 2 * u;
            dht00 = dht00 * dt;
            dht01 = dht01 * dt;
            dhu00 = dhu00 * du;
            dhu01 = dhu01 * du;
            f = 0;
            fx = 0;
            fy = 0;
            v0 = c.f[s1];
            v1 = c.f[s2];
            v2 = c.f[s3];
            v3 = c.f[s4];
            f = f + v0 * ht00 * hu00 + v1 * ht01 * hu00 + v2 * ht00 * hu01 + v3 * ht01 * hu01;
            fx = fx + v0 * dht00 * hu00 + v1 * dht01 * hu00 + v2 * dht00 * hu01 + v3 * dht01 * hu01;
            fy = fy + v0 * ht00 * dhu00 + v1 * ht01 * dhu00 + v2 * ht00 * dhu01 + v3 * ht01 * dhu01;
            v0 = c.f[sfx + s1];
            v1 = c.f[sfx + s2];
            v2 = c.f[sfx + s3];
            v3 = c.f[sfx + s4];
            f = f + v0 * ht10 * hu00 + v1 * ht11 * hu00 + v2 * ht10 * hu01 + v3 * ht11 * hu01;
            fx = fx + v0 * dht10 * hu00 + v1 * dht11 * hu00 + v2 * dht10 * hu01 + v3 * dht11 * hu01;
            fy = fy + v0 * ht10 * dhu00 + v1 * ht11 * dhu00 + v2 * ht10 * dhu01 + v3 * ht11 * dhu01;
            v0 = c.f[sfy + s1];
            v1 = c.f[sfy + s2];
            v2 = c.f[sfy + s3];
            v3 = c.f[sfy + s4];
            f = f + v0 * ht00 * hu10 + v1 * ht01 * hu10 + v2 * ht00 * hu11 + v3 * ht01 * hu11;
            fx = fx + v0 * dht00 * hu10 + v1 * dht01 * hu10 + v2 * dht00 * hu11 + v3 * dht01 * hu11;
            fy = fy + v0 * ht00 * dhu10 + v1 * ht01 * dhu10 + v2 * ht00 * dhu11 + v3 * ht01 * dhu11;
            v0 = c.f[sfxy + s1];
            v1 = c.f[sfxy + s2];
            v2 = c.f[sfxy + s3];
            v3 = c.f[sfxy + s4];
            f = f + v0 * ht10 * hu10 + v1 * ht11 * hu10 + v2 * ht10 * hu11 + v3 * ht11 * hu11;
            fx = fx + v0 * dht10 * hu10 + v1 * dht11 * hu10 + v2 * dht10 * hu11 + v3 * dht11 * hu11;
            fy = fy + v0 * ht10 * dhu10 + v1 * ht11 * dhu10 + v2 * ht10 * dhu11 + v3 * ht11 * dhu11;
            return;
        }
    }


    /*************************************************************************
    This subroutine calculates the value and the derivatives of I-th component
    of a vector-valued bilinear or bicubic spline.

    Input parameters:
        C   -   spline interpolant.
        X, Y-   point
        I   -   component index, in [0,D)

    Output parameters:
        F   -   S(x,y)
        FX  -   dS(x,y)/dX
        FY  -   dS(x,y)/dY
        FXX -   d2S(x,y)/dXdX
        FXY -   d2S(x,y)/dXdY
        FYY -   d2S(x,y)/dYdY

      -- ALGLIB PROJECT --
         Copyright 17.04.2023 by Bochkanov Sergey.
         
         The second derivatives code was contributed by  Horst  Greiner  under
         public domain terms.
    *************************************************************************/
    public static void spline2ddiff2vi(spline2dinterpolant c,
        double x,
        double y,
        int i,
        ref double f,
        ref double fx,
        ref double fy,
        ref double fxx,
        ref double fxy,
        ref double fyy,
        xparams _params)
    {
        int d = 0;
        double t = 0;
        double dt = 0;
        double u = 0;
        double du = 0;
        int ix = 0;
        int iy = 0;
        int l = 0;
        int r = 0;
        int h = 0;
        int s1 = 0;
        int s2 = 0;
        int s3 = 0;
        int s4 = 0;
        int sfx = 0;
        int sfy = 0;
        int sfxy = 0;
        double y1 = 0;
        double y2 = 0;
        double y3 = 0;
        double y4 = 0;
        double v0 = 0;
        double v1 = 0;
        double v2 = 0;
        double v3 = 0;
        double t2 = 0;
        double t3 = 0;
        double u2 = 0;
        double u3 = 0;
        double ht00 = 0;
        double ht01 = 0;
        double ht10 = 0;
        double ht11 = 0;
        double hu00 = 0;
        double hu01 = 0;
        double hu10 = 0;
        double hu11 = 0;
        double dht00 = 0;
        double dht01 = 0;
        double dht10 = 0;
        double dht11 = 0;
        double dhu00 = 0;
        double dhu01 = 0;
        double dhu10 = 0;
        double dhu11 = 0;
        double d2ht00 = 0;
        double d2ht01 = 0;
        double d2ht10 = 0;
        double d2ht11 = 0;
        double d2hu00 = 0;
        double d2hu01 = 0;
        double d2hu10 = 0;
        double d2hu11 = 0;

        f = 0;
        fx = 0;
        fy = 0;
        fxx = 0;
        fxy = 0;
        fyy = 0;

        ap.assert(c.stype == -1 || c.stype == -3, "Spline2DDiffVI: incorrect C (incorrect parameter C.SType)");
        ap.assert(math.isfinite(x) && math.isfinite(y), "Spline2DDiffVI: X or Y contains NaN or Infinite value");
        ap.assert(i >= 0 && i < c.d, "Spline2DDiffVI: I<0 or I>=D");

        //
        // Prepare F, dF/dX, dF/dY, d2F/dXdY
        //
        f = 0;
        fx = 0;
        fy = 0;
        fxx = 0;
        fxy = 0;
        fyy = 0;
        d = c.d;

        //
        // Binary search in the [ x[0], ..., x[n-2] ] (x[n-1] is not included)
        //
        l = 0;
        r = c.n - 1;
        while (l != r - 1)
        {
            h = (l + r) / 2;
            if ((double)(c.x[h]) >= (double)(x))
            {
                r = h;
            }
            else
            {
                l = h;
            }
        }
        t = (x - c.x[l]) / (c.x[l + 1] - c.x[l]);
        dt = 1.0 / (c.x[l + 1] - c.x[l]);
        ix = l;

        //
        // Binary search in the [ y[0], ..., y[m-2] ] (y[m-1] is not included)
        //
        l = 0;
        r = c.m - 1;
        while (l != r - 1)
        {
            h = (l + r) / 2;
            if ((double)(c.y[h]) >= (double)(y))
            {
                r = h;
            }
            else
            {
                l = h;
            }
        }
        u = (y - c.y[l]) / (c.y[l + 1] - c.y[l]);
        du = 1.0 / (c.y[l + 1] - c.y[l]);
        iy = l;

        //
        // Handle possible missing cells
        //
        if (c.hasmissingcells && !adjustevaluationinterval(c, ref x, ref t, ref dt, ref ix, ref y, ref u, ref du, ref iy, _params))
        {
            f = Double.NaN;
            fx = Double.NaN;
            fy = Double.NaN;
            fxx = Double.NaN;
            fxy = Double.NaN;
            fyy = Double.NaN;
            return;
        }

        //
        // Bilinear interpolation
        //
        if (c.stype == -1)
        {
            y1 = c.f[d * (c.n * iy + ix) + i];
            y2 = c.f[d * (c.n * iy + (ix + 1)) + i];
            y3 = c.f[d * (c.n * (iy + 1) + (ix + 1)) + i];
            y4 = c.f[d * (c.n * (iy + 1) + ix) + i];
            f = (1 - t) * (1 - u) * y1 + t * (1 - u) * y2 + t * u * y3 + (1 - t) * u * y4;
            fx = (-((1 - u) * y1) + (1 - u) * y2 + u * y3 - u * y4) * dt;
            fy = (-((1 - t) * y1) - t * y2 + t * y3 + (1 - t) * y4) * du;
            fxx = 0;
            fxy = (y1 - y2 + y3 - y4) * du * dt;
            fyy = 0;
            return;
        }

        //
        // Bicubic interpolation
        //
        if (c.stype == -3)
        {
            sfx = c.n * c.m * d;
            sfy = 2 * c.n * c.m * d;
            sfxy = 3 * c.n * c.m * d;
            s1 = d * (c.n * iy + ix) + i;
            s2 = d * (c.n * iy + (ix + 1)) + i;
            s3 = d * (c.n * (iy + 1) + ix) + i;
            s4 = d * (c.n * (iy + 1) + (ix + 1)) + i;
            t2 = t * t;
            t3 = t * t2;
            u2 = u * u;
            u3 = u * u2;
            ht00 = 2 * t3 - 3 * t2 + 1;
            ht10 = t3 - 2 * t2 + t;
            ht01 = -(2 * t3) + 3 * t2;
            ht11 = t3 - t2;
            hu00 = 2 * u3 - 3 * u2 + 1;
            hu10 = u3 - 2 * u2 + u;
            hu01 = -(2 * u3) + 3 * u2;
            hu11 = u3 - u2;
            ht10 = ht10 / dt;
            ht11 = ht11 / dt;
            hu10 = hu10 / du;
            hu11 = hu11 / du;
            dht00 = 6 * t2 - 6 * t;
            dht10 = 3 * t2 - 4 * t + 1;
            dht01 = -(6 * t2) + 6 * t;
            dht11 = 3 * t2 - 2 * t;
            dhu00 = 6 * u2 - 6 * u;
            dhu10 = 3 * u2 - 4 * u + 1;
            dhu01 = -(6 * u2) + 6 * u;
            dhu11 = 3 * u2 - 2 * u;
            dht00 = dht00 * dt;
            dht01 = dht01 * dt;
            dhu00 = dhu00 * du;
            dhu01 = dhu01 * du;
            d2ht00 = (12 * t - 6) * dt * dt;
            d2ht01 = (-(12 * t) + 6) * dt * dt;
            d2ht10 = (6 * t - 4) * dt;
            d2ht11 = (6 * t - 2) * dt;
            d2hu00 = (12 * u - 6) * du * du;
            d2hu01 = (-(12 * u) + 6) * du * du;
            d2hu10 = (6 * u - 4) * du;
            d2hu11 = (6 * u - 2) * du;
            f = 0;
            fx = 0;
            fy = 0;
            fxy = 0;
            v0 = c.f[s1];
            v1 = c.f[s2];
            v2 = c.f[s3];
            v3 = c.f[s4];
            f = f + v0 * ht00 * hu00 + v1 * ht01 * hu00 + v2 * ht00 * hu01 + v3 * ht01 * hu01;
            fx = fx + v0 * dht00 * hu00 + v1 * dht01 * hu00 + v2 * dht00 * hu01 + v3 * dht01 * hu01;
            fy = fy + v0 * ht00 * dhu00 + v1 * ht01 * dhu00 + v2 * ht00 * dhu01 + v3 * ht01 * dhu01;
            fxx = fxx + v0 * d2ht00 * hu00 + v1 * d2ht01 * hu00 + v2 * d2ht00 * hu01 + v3 * d2ht01 * hu01;
            fxy = fxy + v0 * dht00 * dhu00 + v1 * dht01 * dhu00 + v2 * dht00 * dhu01 + v3 * dht01 * dhu01;
            fyy = fyy + v0 * ht00 * d2hu00 + v1 * ht01 * d2hu00 + v2 * ht00 * d2hu01 + v3 * ht01 * d2hu01;
            v0 = c.f[sfx + s1];
            v1 = c.f[sfx + s2];
            v2 = c.f[sfx + s3];
            v3 = c.f[sfx + s4];
            f = f + v0 * ht10 * hu00 + v1 * ht11 * hu00 + v2 * ht10 * hu01 + v3 * ht11 * hu01;
            fx = fx + v0 * dht10 * hu00 + v1 * dht11 * hu00 + v2 * dht10 * hu01 + v3 * dht11 * hu01;
            fy = fy + v0 * ht10 * dhu00 + v1 * ht11 * dhu00 + v2 * ht10 * dhu01 + v3 * ht11 * dhu01;
            fxx = fxx + v0 * d2ht10 * hu00 + v1 * d2ht11 * hu00 + v2 * d2ht10 * hu01 + v3 * d2ht11 * hu01;
            fxy = fxy + v0 * dht10 * dhu00 + v1 * dht11 * dhu00 + v2 * dht10 * dhu01 + v3 * dht11 * dhu01;
            fyy = fyy + v0 * ht10 * d2hu00 + v1 * ht11 * d2hu00 + v2 * ht10 * d2hu01 + v3 * ht11 * d2hu01;
            v0 = c.f[sfy + s1];
            v1 = c.f[sfy + s2];
            v2 = c.f[sfy + s3];
            v3 = c.f[sfy + s4];
            f = f + v0 * ht00 * hu10 + v1 * ht01 * hu10 + v2 * ht00 * hu11 + v3 * ht01 * hu11;
            fx = fx + v0 * dht00 * hu10 + v1 * dht01 * hu10 + v2 * dht00 * hu11 + v3 * dht01 * hu11;
            fy = fy + v0 * ht00 * dhu10 + v1 * ht01 * dhu10 + v2 * ht00 * dhu11 + v3 * ht01 * dhu11;
            fxx = fxx + v0 * d2ht00 * hu10 + v1 * d2ht01 * hu10 + v2 * d2ht00 * hu11 + v3 * d2ht01 * hu11;
            fxy = fxy + v0 * dht00 * dhu10 + v1 * dht01 * dhu10 + v2 * dht00 * dhu11 + v3 * dht01 * dhu11;
            fyy = fyy + v0 * ht00 * d2hu10 + v1 * ht01 * d2hu10 + v2 * ht00 * d2hu11 + v3 * ht01 * d2hu11;
            v0 = c.f[sfxy + s1];
            v1 = c.f[sfxy + s2];
            v2 = c.f[sfxy + s3];
            v3 = c.f[sfxy + s4];
            f = f + v0 * ht10 * hu10 + v1 * ht11 * hu10 + v2 * ht10 * hu11 + v3 * ht11 * hu11;
            fx = fx + v0 * dht10 * hu10 + v1 * dht11 * hu10 + v2 * dht10 * hu11 + v3 * dht11 * hu11;
            fy = fy + v0 * ht10 * dhu10 + v1 * ht11 * dhu10 + v2 * ht10 * dhu11 + v3 * ht11 * dhu11;
            fxx = fxx + v0 * d2ht10 * hu10 + v1 * d2ht11 * hu10 + v2 * d2ht10 * hu11 + v3 * d2ht11 * hu11;
            fxy = fxy + v0 * dht10 * dhu10 + v1 * dht11 * dhu10 + v2 * dht10 * dhu11 + v3 * dht11 * dhu11;
            fyy = fyy + v0 * ht10 * d2hu10 + v1 * ht11 * d2hu10 + v2 * ht10 * d2hu11 + v3 * ht11 * d2hu11;
            return;
        }
    }


    /*************************************************************************
    This subroutine performs linear transformation of the spline argument.

    Input parameters:
        C       -   spline interpolant
        AX, BX  -   transformation coefficients: x = A*t + B
        AY, BY  -   transformation coefficients: y = A*u + B
    Result:
        C   -   transformed spline

      -- ALGLIB PROJECT --
         Copyright 30.06.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dlintransxy(spline2dinterpolant c,
        double ax,
        double bx,
        double ay,
        double by,
        xparams _params)
    {
        double[] x = new double[0];
        double[] y = new double[0];
        double[] f = new double[0];
        double[] v = new double[0];
        bool[] missing = new bool[0];
        bool missingv = new bool();
        int i = 0;
        int j = 0;
        int k = 0;

        ap.assert(c.stype == -3 || c.stype == -1, "Spline2DLinTransXY: incorrect C (incorrect parameter C.SType)");
        ap.assert(math.isfinite(ax), "Spline2DLinTransXY: AX is infinite or NaN");
        ap.assert(math.isfinite(bx), "Spline2DLinTransXY: BX is infinite or NaN");
        ap.assert(math.isfinite(ay), "Spline2DLinTransXY: AY is infinite or NaN");
        ap.assert(math.isfinite(by), "Spline2DLinTransXY: BY is infinite or NaN");
        x = new double[c.n];
        y = new double[c.m];
        f = new double[c.m * c.n * c.d];
        for (j = 0; j <= c.n - 1; j++)
        {
            x[j] = c.x[j];
        }
        for (i = 0; i <= c.m - 1; i++)
        {
            y[i] = c.y[i];
        }
        for (i = 0; i <= c.m - 1; i++)
        {
            for (j = 0; j <= c.n - 1; j++)
            {
                for (k = 0; k <= c.d - 1; k++)
                {
                    f[c.d * (i * c.n + j) + k] = c.f[c.d * (i * c.n + j) + k];
                }
            }
        }

        //
        // Handle different combinations of AX/AY
        //
        ablasf.bsetallocv(c.n * c.m, false, ref missing, _params);
        if ((double)(ax) == (double)(0) && (double)(ay) != (double)(0))
        {
            for (i = 0; i <= c.m - 1; i++)
            {
                spline2dcalcvbuf(c, bx, y[i], ref v, _params);
                y[i] = (y[i] - by) / ay;
                missingv = !math.isfinite(v[0]);
                for (j = 0; j <= c.n - 1; j++)
                {
                    for (k = 0; k <= c.d - 1; k++)
                    {
                        f[c.d * (i * c.n + j) + k] = v[k];
                    }
                    missing[i * c.n + j] = missingv;
                }
            }
        }
        if ((double)(ax) != (double)(0) && (double)(ay) == (double)(0))
        {
            for (j = 0; j <= c.n - 1; j++)
            {
                spline2dcalcvbuf(c, x[j], by, ref v, _params);
                x[j] = (x[j] - bx) / ax;
                missingv = !math.isfinite(v[0]);
                for (i = 0; i <= c.m - 1; i++)
                {
                    for (k = 0; k <= c.d - 1; k++)
                    {
                        f[c.d * (i * c.n + j) + k] = v[k];
                    }
                    missing[i * c.n + j] = missingv;
                }
            }
        }
        if ((double)(ax) != (double)(0) && (double)(ay) != (double)(0))
        {
            for (j = 0; j <= c.n - 1; j++)
            {
                x[j] = (x[j] - bx) / ax;
            }
            for (i = 0; i <= c.m - 1; i++)
            {
                y[i] = (y[i] - by) / ay;
            }
            if (c.hasmissingcells)
            {
                ablasf.bcopyv(c.n * c.m, c.ismissingnode, missing, _params);
            }
        }
        if ((double)(ax) == (double)(0) && (double)(ay) == (double)(0))
        {
            spline2dcalcvbuf(c, bx, by, ref v, _params);
            for (i = 0; i <= c.m - 1; i++)
            {
                for (j = 0; j <= c.n - 1; j++)
                {
                    for (k = 0; k <= c.d - 1; k++)
                    {
                        f[c.d * (i * c.n + j) + k] = v[k];
                    }
                }
            }
            ablasf.bsetv(c.n * c.m, !math.isfinite(v[0]), missing, _params);
        }

        //
        // Rebuild spline
        //
        if (!c.hasmissingcells)
        {
            if (c.stype == -3)
            {
                spline2dbuildbicubicvbuf(x, c.n, y, c.m, f, c.d, c, _params);
            }
            if (c.stype == -1)
            {
                spline2dbuildbilinearvbuf(x, c.n, y, c.m, f, c.d, c, _params);
            }
        }
        else
        {
            if (c.stype == -3)
            {
                spline2dbuildbicubicmissingbuf(x, c.n, y, c.m, f, missing, c.d, c, _params);
            }
            if (c.stype == -1)
            {
                spline2dbuildbilinearmissingbuf(x, c.n, y, c.m, f, missing, c.d, c, _params);
            }
        }
    }


    /*************************************************************************
    This subroutine performs linear transformation of the spline.

    Input parameters:
        C   -   spline interpolant.
        A, B-   transformation coefficients: S2(x,y) = A*S(x,y) + B
        
    Output parameters:
        C   -   transformed spline

      -- ALGLIB PROJECT --
         Copyright 30.06.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dlintransf(spline2dinterpolant c,
        double a,
        double b,
        xparams _params)
    {
        double[] x = new double[0];
        double[] y = new double[0];
        double[] f = new double[0];
        bool[] missing = new bool[0];
        int i = 0;
        int j = 0;

        ap.assert(c.stype == -3 || c.stype == -1, "Spline2DLinTransF: incorrect C (incorrect parameter C.SType)");
        if (c.stype == -1)
        {

            //
            // Bilinear spline
            //
            if (!c.hasmissingcells)
            {

                //
                // Quick code for a spline without missing cells
                //
                for (i = 0; i <= c.m * c.n * c.d - 1; i++)
                {
                    c.f[i] = a * c.f[i] + b;
                }
            }
            else
            {

                //
                // Slower code for missing cells
                //
                for (i = 0; i <= c.m * c.n * c.d - 1; i++)
                {
                    if (!c.ismissingnode[i / c.d])
                    {
                        c.f[i] = a * c.f[i] + b;
                    }
                }
            }
        }
        else
        {

            //
            // Bicubic spline
            //
            if (!c.hasmissingcells)
            {

                //
                // Quick code for a spline without missing cells
                //
                x = new double[c.n];
                y = new double[c.m];
                f = new double[c.m * c.n * c.d];
                for (j = 0; j <= c.n - 1; j++)
                {
                    x[j] = c.x[j];
                }
                for (i = 0; i <= c.m - 1; i++)
                {
                    y[i] = c.y[i];
                }
                for (i = 0; i <= c.m * c.n * c.d - 1; i++)
                {
                    f[i] = a * c.f[i] + b;
                }
                spline2dbuildbicubicvbuf(x, c.n, y, c.m, f, c.d, c, _params);
            }
            else
            {

                //
                // Slower code for missing cells
                //
                x = new double[c.n];
                y = new double[c.m];
                ablasf.rsetallocv(c.m * c.n * c.d, 0.0, ref f, _params);
                for (j = 0; j <= c.n - 1; j++)
                {
                    x[j] = c.x[j];
                }
                for (i = 0; i <= c.m - 1; i++)
                {
                    y[i] = c.y[i];
                }
                for (i = 0; i <= c.m * c.n * c.d - 1; i++)
                {
                    if (!c.ismissingnode[i / c.d])
                    {
                        f[i] = a * c.f[i] + b;
                    }
                }
                ablasf.bcopyallocv(c.m * c.n, c.ismissingnode, ref missing, _params);
                spline2dbuildbicubicmissingbuf(x, c.n, y, c.m, f, missing, c.d, c, _params);
            }
        }
    }


    /*************************************************************************
    This subroutine makes the copy of the spline model.

    Input parameters:
        C   -   spline interpolant

    Output parameters:
        CC  -   spline copy

      -- ALGLIB PROJECT --
         Copyright 29.06.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dcopy(spline2dinterpolant c,
        spline2dinterpolant cc,
        xparams _params)
    {
        int tblsize = 0;
        int i_ = 0;

        ap.assert(c.stype == -1 || c.stype == -3, "Spline2DCopy: incorrect C (incorrect parameter C.SType)");
        cc.n = c.n;
        cc.m = c.m;
        cc.d = c.d;
        cc.stype = c.stype;
        cc.hasmissingcells = c.hasmissingcells;
        tblsize = -1;
        if (c.stype == -3)
        {
            tblsize = 4 * c.n * c.m * c.d;
        }
        if (c.stype == -1)
        {
            tblsize = c.n * c.m * c.d;
        }
        ap.assert(tblsize > 0, "Spline2DCopy: internal error");
        cc.x = new double[cc.n];
        cc.y = new double[cc.m];
        cc.f = new double[tblsize];
        for (i_ = 0; i_ <= cc.n - 1; i_++)
        {
            cc.x[i_] = c.x[i_];
        }
        for (i_ = 0; i_ <= cc.m - 1; i_++)
        {
            cc.y[i_] = c.y[i_];
        }
        for (i_ = 0; i_ <= tblsize - 1; i_++)
        {
            cc.f[i_] = c.f[i_];
        }
        if (c.hasmissingcells)
        {
            ablasf.bcopyallocv(c.n * c.m, c.ismissingnode, ref cc.ismissingnode, _params);
            ablasf.bcopyallocv((c.n - 1) * (c.m - 1), c.ismissingcell, ref cc.ismissingcell, _params);
        }
    }


    /*************************************************************************
    Bicubic spline resampling

    Input parameters:
        A           -   function values at the old grid,
                        array[0..OldHeight-1, 0..OldWidth-1]
        OldHeight   -   old grid height, OldHeight>1
        OldWidth    -   old grid width, OldWidth>1
        NewHeight   -   new grid height, NewHeight>1
        NewWidth    -   new grid width, NewWidth>1
        
    Output parameters:
        B           -   function values at the new grid,
                        array[0..NewHeight-1, 0..NewWidth-1]

      -- ALGLIB routine --
         15 May, 2007
         Copyright by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dresamplebicubic(double[,] a,
        int oldheight,
        int oldwidth,
        ref double[,] b,
        int newheight,
        int newwidth,
        xparams _params)
    {
        double[,] buf = new double[0, 0];
        double[] x = new double[0];
        double[] y = new double[0];
        spline1d.spline1dinterpolant c = new spline1d.spline1dinterpolant();
        int mw = 0;
        int mh = 0;
        int i = 0;
        int j = 0;

        b = new double[0, 0];

        ap.assert(oldwidth > 1 && oldheight > 1, "Spline2DResampleBicubic: width/height less than 1");
        ap.assert(newwidth > 1 && newheight > 1, "Spline2DResampleBicubic: width/height less than 1");

        //
        // Prepare
        //
        mw = Math.Max(oldwidth, newwidth);
        mh = Math.Max(oldheight, newheight);
        b = new double[newheight, newwidth];
        buf = new double[oldheight, newwidth];
        x = new double[Math.Max(mw, mh)];
        y = new double[Math.Max(mw, mh)];

        //
        // Horizontal interpolation
        //
        for (i = 0; i <= oldheight - 1; i++)
        {

            //
            // Fill X, Y
            //
            for (j = 0; j <= oldwidth - 1; j++)
            {
                x[j] = (double)j / (double)(oldwidth - 1);
                y[j] = a[i, j];
            }

            //
            // Interpolate and place result into temporary matrix
            //
            spline1d.spline1dbuildcubic(x, y, oldwidth, 0, 0.0, 0, 0.0, c, _params);
            for (j = 0; j <= newwidth - 1; j++)
            {
                buf[i, j] = spline1d.spline1dcalc(c, (double)j / (double)(newwidth - 1), _params);
            }
        }

        //
        // Vertical interpolation
        //
        for (j = 0; j <= newwidth - 1; j++)
        {

            //
            // Fill X, Y
            //
            for (i = 0; i <= oldheight - 1; i++)
            {
                x[i] = (double)i / (double)(oldheight - 1);
                y[i] = buf[i, j];
            }

            //
            // Interpolate and place result into B
            //
            spline1d.spline1dbuildcubic(x, y, oldheight, 0, 0.0, 0, 0.0, c, _params);
            for (i = 0; i <= newheight - 1; i++)
            {
                b[i, j] = spline1d.spline1dcalc(c, (double)i / (double)(newheight - 1), _params);
            }
        }
    }


    /*************************************************************************
    Bilinear spline resampling

    Input parameters:
        A           -   function values at the old grid,
                        array[0..OldHeight-1, 0..OldWidth-1]
        OldHeight   -   old grid height, OldHeight>1
        OldWidth    -   old grid width, OldWidth>1
        NewHeight   -   new grid height, NewHeight>1
        NewWidth    -   new grid width, NewWidth>1

    Output parameters:
        B           -   function values at the new grid,
                        array[0..NewHeight-1, 0..NewWidth-1]

      -- ALGLIB routine --
         09.07.2007
         Copyright by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dresamplebilinear(double[,] a,
        int oldheight,
        int oldwidth,
        ref double[,] b,
        int newheight,
        int newwidth,
        xparams _params)
    {
        int l = 0;
        int c = 0;
        double t = 0;
        double u = 0;
        int i = 0;
        int j = 0;

        b = new double[0, 0];

        ap.assert(oldwidth > 1 && oldheight > 1, "Spline2DResampleBilinear: width/height less than 1");
        ap.assert(newwidth > 1 && newheight > 1, "Spline2DResampleBilinear: width/height less than 1");
        b = new double[newheight, newwidth];
        for (i = 0; i <= newheight - 1; i++)
        {
            for (j = 0; j <= newwidth - 1; j++)
            {
                l = i * (oldheight - 1) / (newheight - 1);
                if (l == oldheight - 1)
                {
                    l = oldheight - 2;
                }
                u = (double)i / (double)(newheight - 1) * (oldheight - 1) - l;
                c = j * (oldwidth - 1) / (newwidth - 1);
                if (c == oldwidth - 1)
                {
                    c = oldwidth - 2;
                }
                t = (double)(j * (oldwidth - 1)) / (double)(newwidth - 1) - c;
                b[i, j] = (1 - t) * (1 - u) * a[l, c] + t * (1 - u) * a[l, c + 1] + t * u * a[l + 1, c + 1] + (1 - t) * u * a[l + 1, c];
            }
        }
    }


    /*************************************************************************
    This subroutine builds bilinear vector-valued spline.

    This function produces C0-continuous spline, i.e.  the  spline  itself  is
    continuous, however its first and second  derivatives have discontinuities
    at the spline cell boundaries.

    Input parameters:
        X   -   spline abscissas, array[0..N-1]
        Y   -   spline ordinates, array[0..M-1]
        F   -   function values, array[0..M*N*D-1]:
                * first D elements store D values at (X[0],Y[0])
                * next D elements store D values at (X[1],Y[0])
                * general form - D function values at (X[i],Y[j]) are stored
                  at F[D*(J*N+I)...D*(J*N+I)+D-1].
        M,N -   grid size, M>=2, N>=2
        D   -   vector dimension, D>=1

    Output parameters:
        C   -   spline interpolant

      -- ALGLIB PROJECT --
         Copyright 16.04.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dbuildbilinearv(double[] x,
        int n,
        double[] y,
        int m,
        double[] f,
        int d,
        spline2dinterpolant c,
        xparams _params)
    {
        spline2dbuildbilinearvbuf(x, n, y, m, f, d, c, _params);
    }


    /*************************************************************************
    This subroutine builds bilinear vector-valued spline.

    Buffered version of Spline2DBuildBilinearV() which reuses memory previously
    allocated in C as much as possible.

      -- ALGLIB PROJECT --
         Copyright 16.04.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dbuildbilinearvbuf(double[] x,
        int n,
        double[] y,
        int m,
        double[] f,
        int d,
        spline2dinterpolant c,
        xparams _params)
    {
        double t = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        int i0 = 0;

        ap.assert(n >= 2, "Spline2DBuildBilinearV: N is less then 2");
        ap.assert(m >= 2, "Spline2DBuildBilinearV: M is less then 2");
        ap.assert(d >= 1, "Spline2DBuildBilinearV: invalid argument D (D<1)");
        ap.assert(ap.len(x) >= n && ap.len(y) >= m, "Spline2DBuildBilinearV: length of X or Y is too short (Length(X/Y)<N/M)");
        ap.assert(apserv.isfinitevector(x, n, _params) && apserv.isfinitevector(y, m, _params), "Spline2DBuildBilinearV: X or Y contains NaN or Infinite value");
        k = n * m * d;
        ap.assert(ap.len(f) >= k, "Spline2DBuildBilinearV: length of F is too short (Length(F)<N*M*D)");
        ap.assert(apserv.isfinitevector(f, k, _params), "Spline2DBuildBilinearV: F contains NaN or Infinite value");

        //
        // Fill interpolant
        //
        c.n = n;
        c.m = m;
        c.d = d;
        c.stype = -1;
        c.hasmissingcells = false;
        c.x = new double[c.n];
        c.y = new double[c.m];
        c.f = new double[k];
        for (i = 0; i <= c.n - 1; i++)
        {
            c.x[i] = x[i];
        }
        for (i = 0; i <= c.m - 1; i++)
        {
            c.y[i] = y[i];
        }
        for (i = 0; i <= k - 1; i++)
        {
            c.f[i] = f[i];
        }

        //
        // Sort points
        //
        for (j = 0; j <= c.n - 1; j++)
        {
            k = j;
            for (i = j + 1; i <= c.n - 1; i++)
            {
                if ((double)(c.x[i]) < (double)(c.x[k]))
                {
                    k = i;
                }
            }
            if (k != j)
            {
                for (i = 0; i <= c.m - 1; i++)
                {
                    for (i0 = 0; i0 <= c.d - 1; i0++)
                    {
                        t = c.f[c.d * (i * c.n + j) + i0];
                        c.f[c.d * (i * c.n + j) + i0] = c.f[c.d * (i * c.n + k) + i0];
                        c.f[c.d * (i * c.n + k) + i0] = t;
                    }
                }
                t = c.x[j];
                c.x[j] = c.x[k];
                c.x[k] = t;
            }
        }
        for (i = 0; i <= c.m - 1; i++)
        {
            k = i;
            for (j = i + 1; j <= c.m - 1; j++)
            {
                if ((double)(c.y[j]) < (double)(c.y[k]))
                {
                    k = j;
                }
            }
            if (k != i)
            {
                for (j = 0; j <= c.n - 1; j++)
                {
                    for (i0 = 0; i0 <= c.d - 1; i0++)
                    {
                        t = c.f[c.d * (i * c.n + j) + i0];
                        c.f[c.d * (i * c.n + j) + i0] = c.f[c.d * (k * c.n + j) + i0];
                        c.f[c.d * (k * c.n + j) + i0] = t;
                    }
                }
                t = c.y[i];
                c.y[i] = c.y[k];
                c.y[k] = t;
            }
        }
    }


    /*************************************************************************
    This subroutine builds bilinear vector-valued  spline,  with  some  spline
    cells being missing due to missing nodes.

    This function produces C0-continuous spline, i.e.  the  spline  itself  is
    continuous, however its first and second  derivatives have discontinuities
    at the spline cell boundaries.

    When the node (i,j) is missing, it means that: a) we don't  have  function
    value at this point (elements of F[] are ignored), and  b)  we  don't need
    spline value at cells adjacent to the node (i,j), i.e. up to 4 spline cells
    will be dropped. An attempt to compute spline value at  the  missing  cell
    will return NAN.

    It is important to  understand  that  this  subroutine  does  NOT  support
    interpolation on scattered grids. It allows us to drop some nodes, but  at
    the cost of making a "hole in the spline" around this point. If  you  want
    function  that   can   "fill  the  gap",  use  RBF  or  another  scattered
    interpolation method.

    The  intended  usage  for  this  subroutine  are  regularly  sampled,  but
    non-rectangular datasets.

    Input parameters:
        X   -   spline abscissas, array[0..N-1]
        Y   -   spline ordinates, array[0..M-1]
        F   -   function values, array[0..M*N*D-1]:
                * first D elements store D values at (X[0],Y[0])
                * next D elements store D values at (X[1],Y[0])
                * general form - D function values at (X[i],Y[j]) are stored
                  at F[D*(J*N+I)...D*(J*N+I)+D-1].
                * missing values are ignored
        Missing array[M*N], Missing[J*N+I]=True means that corresponding entries
                of F[] are missing nodes.
        M,N -   grid size, M>=2, N>=2
        D   -   vector dimension, D>=1

    Output parameters:
        C   -   spline interpolant

      -- ALGLIB PROJECT --
         Copyright 27.06.2022 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dbuildbilinearmissing(double[] x,
        int n,
        double[] y,
        int m,
        double[] f,
        bool[] missing,
        int d,
        spline2dinterpolant c,
        xparams _params)
    {
        f = (double[])f.Clone();

        spline2dbuildbilinearmissingbuf(x, n, y, m, f, missing, d, c, _params);
    }


    /*************************************************************************
    This subroutine builds bilinear vector-valued  spline,  with  some  spline
    cells being missing due to missing nodes.

    Buffered version of  Spline2DBuildBilinearMissing()  which  reuses  memory
    previously allocated in C as much as possible.

      -- ALGLIB PROJECT --
         Copyright 27.06.2022 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dbuildbilinearmissingbuf(double[] x,
        int n,
        double[] y,
        int m,
        double[] f,
        bool[] missing,
        int d,
        spline2dinterpolant c,
        xparams _params)
    {
        double t = 0;
        bool tb = new bool();
        int i = 0;
        int j = 0;
        int k = 0;
        int i0 = 0;

        f = (double[])f.Clone();

        ap.assert(n >= 2, "Spline2DBuildBilinearMissing: N is less then 2");
        ap.assert(m >= 2, "Spline2DBuildBilinearMissing: M is less then 2");
        ap.assert(d >= 1, "Spline2DBuildBilinearMissing: invalid argument D (D<1)");
        ap.assert(ap.len(x) >= n && ap.len(y) >= m, "Spline2DBuildBilinearMissing: length of X or Y is too short (Length(X/Y)<N/M)");
        ap.assert(apserv.isfinitevector(x, n, _params) && apserv.isfinitevector(y, m, _params), "Spline2DBuildBilinearMissing: X or Y contains NaN or Infinite value");
        k = n * m * d;
        ap.assert(ap.len(f) >= k, "Spline2DBuildBilinearMissing: length of F is too short (Length(F)<N*M*D)");
        ap.assert(ap.len(missing) >= n * m, "Spline2DBuildBilinearMissing: Missing[] is shorter than M*N");
        for (i = 0; i <= k - 1; i++)
        {
            if (!missing[i / d] && !math.isfinite(f[i]))
            {
                ap.assert(false, "Spline2DBuildBilinearMissing: F[] contains NAN or INF in its non-missing entries");
            }
        }

        //
        // Fill interpolant.
        //
        // NOTE: we make sure that missing entries of F[] are filled by zeros.
        //
        c.n = n;
        c.m = m;
        c.d = d;
        c.stype = -1;
        c.hasmissingcells = true;
        c.x = new double[c.n];
        c.y = new double[c.m];
        ablasf.rsetallocv(k, 0.0, ref c.f, _params);
        for (i = 0; i <= c.n - 1; i++)
        {
            c.x[i] = x[i];
        }
        for (i = 0; i <= c.m - 1; i++)
        {
            c.y[i] = y[i];
        }
        for (i = 0; i <= k - 1; i++)
        {
            if (!missing[i / d])
            {
                c.f[i] = f[i];
            }
        }
        ablasf.bcopyallocv(c.n * c.m, missing, ref c.ismissingnode, _params);

        //
        // Sort points
        //
        for (j = 0; j <= c.n - 1; j++)
        {
            k = j;
            for (i = j + 1; i <= c.n - 1; i++)
            {
                if ((double)(c.x[i]) < (double)(c.x[k]))
                {
                    k = i;
                }
            }
            if (k != j)
            {
                for (i = 0; i <= c.m - 1; i++)
                {
                    for (i0 = 0; i0 <= c.d - 1; i0++)
                    {
                        t = c.f[c.d * (i * c.n + j) + i0];
                        c.f[c.d * (i * c.n + j) + i0] = c.f[c.d * (i * c.n + k) + i0];
                        c.f[c.d * (i * c.n + k) + i0] = t;
                    }
                    tb = c.ismissingnode[i * c.n + j];
                    c.ismissingnode[i * c.n + j] = c.ismissingnode[i * c.n + k];
                    c.ismissingnode[i * c.n + k] = tb;
                }
                t = c.x[j];
                c.x[j] = c.x[k];
                c.x[k] = t;
            }
        }
        for (i = 0; i <= c.m - 1; i++)
        {
            k = i;
            for (j = i + 1; j <= c.m - 1; j++)
            {
                if ((double)(c.y[j]) < (double)(c.y[k]))
                {
                    k = j;
                }
            }
            if (k != i)
            {
                for (j = 0; j <= c.n - 1; j++)
                {
                    for (i0 = 0; i0 <= c.d - 1; i0++)
                    {
                        t = c.f[c.d * (i * c.n + j) + i0];
                        c.f[c.d * (i * c.n + j) + i0] = c.f[c.d * (k * c.n + j) + i0];
                        c.f[c.d * (k * c.n + j) + i0] = t;
                    }
                    tb = c.ismissingnode[i * c.n + j];
                    c.ismissingnode[i * c.n + j] = c.ismissingnode[k * c.n + j];
                    c.ismissingnode[k * c.n + j] = tb;
                }
                t = c.y[i];
                c.y[i] = c.y[k];
                c.y[k] = t;
            }
        }

        //
        // 1. Determine cells that are not missing. A cell is non-missing if it has four non-missing
        //    nodes at its corners.
        // 2. Normalize IsMissingNode[] array - all isolated points that are not part of some non-missing
        //    cell are marked as missing too
        //
        ablasf.bsetallocv((c.m - 1) * (c.n - 1), true, ref c.ismissingcell, _params);
        for (i = 0; i <= c.m - 2; i++)
        {
            for (j = 0; j <= c.n - 2; j++)
            {
                if (((!c.ismissingnode[i * c.n + j] && !c.ismissingnode[(i + 1) * c.n + j]) && !c.ismissingnode[i * c.n + (j + 1)]) && !c.ismissingnode[(i + 1) * c.n + (j + 1)])
                {
                    c.ismissingcell[i * (c.n - 1) + j] = false;
                }
            }
        }
        ablasf.bsetv(c.m * c.n, true, c.ismissingnode, _params);
        for (i = 0; i <= c.m - 2; i++)
        {
            for (j = 0; j <= c.n - 2; j++)
            {
                if (!c.ismissingcell[i * (c.n - 1) + j])
                {
                    c.ismissingnode[i * c.n + j] = false;
                    c.ismissingnode[(i + 1) * c.n + j] = false;
                    c.ismissingnode[i * c.n + (j + 1)] = false;
                    c.ismissingnode[(i + 1) * c.n + (j + 1)] = false;
                }
            }
        }
    }


    /*************************************************************************
    This subroutine builds bicubic vector-valued spline.

    This function produces C2-continuous spline, i.e. the has smooth first and
    second derivatives both inside spline cells and at the boundaries.

    Input parameters:
        X   -   spline abscissas, array[0..N-1]
        Y   -   spline ordinates, array[0..M-1]
        F   -   function values, array[0..M*N*D-1]:
                * first D elements store D values at (X[0],Y[0])
                * next D elements store D values at (X[1],Y[0])
                * general form - D function values at (X[i],Y[j]) are stored
                  at F[D*(J*N+I)...D*(J*N+I)+D-1].
        M,N -   grid size, M>=2, N>=2
        D   -   vector dimension, D>=1

    Output parameters:
        C   -   spline interpolant

      -- ALGLIB PROJECT --
         Copyright 16.04.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dbuildbicubicv(double[] x,
        int n,
        double[] y,
        int m,
        double[] f,
        int d,
        spline2dinterpolant c,
        xparams _params)
    {
        f = (double[])f.Clone();

        spline2dbuildbicubicvbuf(x, n, y, m, f, d, c, _params);
    }


    /*************************************************************************
    This subroutine builds bicubic vector-valued spline.

    Buffered version of Spline2DBuildBicubicV() which reuses memory previously
    allocated in C as much as possible.

      -- ALGLIB PROJECT --
         Copyright 16.04.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dbuildbicubicvbuf(double[] x,
        int n,
        double[] y,
        int m,
        double[] f,
        int d,
        spline2dinterpolant c,
        xparams _params)
    {
        double[,] tf = new double[0, 0];
        double[,] dx = new double[0, 0];
        double[,] dy = new double[0, 0];
        double[,] dxy = new double[0, 0];
        double t = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        int di = 0;

        f = (double[])f.Clone();

        ap.assert(n >= 2, "Spline2DBuildBicubicV: N is less than 2");
        ap.assert(m >= 2, "Spline2DBuildBicubicV: M is less than 2");
        ap.assert(d >= 1, "Spline2DBuildBicubicV: invalid argument D (D<1)");
        ap.assert(ap.len(x) >= n && ap.len(y) >= m, "Spline2DBuildBicubicV: length of X or Y is too short (Length(X/Y)<N/M)");
        ap.assert(apserv.isfinitevector(x, n, _params) && apserv.isfinitevector(y, m, _params), "Spline2DBuildBicubicV: X or Y contains NaN or Infinite value");
        k = n * m * d;
        ap.assert(ap.len(f) >= k, "Spline2DBuildBicubicV: length of F is too short (Length(F)<N*M*D)");
        ap.assert(apserv.isfinitevector(f, k, _params), "Spline2DBuildBicubicV: F contains NaN or Infinite value");

        //
        // Fill interpolant:
        //  F[0]...F[N*M*D-1]:
        //      f(i,j) table. f(0,0), f(0, 1), f(0,2) and so on...
        //  F[N*M*D]...F[2*N*M*D-1]:
        //      df(i,j)/dx table.
        //  F[2*N*M*D]...F[3*N*M*D-1]:
        //      df(i,j)/dy table.
        //  F[3*N*M*D]...F[4*N*M*D-1]:
        //      d2f(i,j)/dxdy table.
        //
        c.d = d;
        c.n = n;
        c.m = m;
        c.stype = -3;
        c.hasmissingcells = false;
        k = 4 * k;
        c.x = new double[c.n];
        c.y = new double[c.m];
        c.f = new double[k];
        tf = new double[c.m, c.n];
        for (i = 0; i <= c.n - 1; i++)
        {
            c.x[i] = x[i];
        }
        for (i = 0; i <= c.m - 1; i++)
        {
            c.y[i] = y[i];
        }

        //
        // Sort points
        //
        for (j = 0; j <= c.n - 1; j++)
        {
            k = j;
            for (i = j + 1; i <= c.n - 1; i++)
            {
                if ((double)(c.x[i]) < (double)(c.x[k]))
                {
                    k = i;
                }
            }
            if (k != j)
            {
                for (i = 0; i <= c.m - 1; i++)
                {
                    for (di = 0; di <= c.d - 1; di++)
                    {
                        t = f[c.d * (i * c.n + j) + di];
                        f[c.d * (i * c.n + j) + di] = f[c.d * (i * c.n + k) + di];
                        f[c.d * (i * c.n + k) + di] = t;
                    }
                }
                t = c.x[j];
                c.x[j] = c.x[k];
                c.x[k] = t;
            }
        }
        for (i = 0; i <= c.m - 1; i++)
        {
            k = i;
            for (j = i + 1; j <= c.m - 1; j++)
            {
                if ((double)(c.y[j]) < (double)(c.y[k]))
                {
                    k = j;
                }
            }
            if (k != i)
            {
                for (j = 0; j <= c.n - 1; j++)
                {
                    for (di = 0; di <= c.d - 1; di++)
                    {
                        t = f[c.d * (i * c.n + j) + di];
                        f[c.d * (i * c.n + j) + di] = f[c.d * (k * c.n + j) + di];
                        f[c.d * (k * c.n + j) + di] = t;
                    }
                }
                t = c.y[i];
                c.y[i] = c.y[k];
                c.y[k] = t;
            }
        }
        for (di = 0; di <= c.d - 1; di++)
        {
            for (i = 0; i <= c.m - 1; i++)
            {
                for (j = 0; j <= c.n - 1; j++)
                {
                    tf[i, j] = f[c.d * (i * c.n + j) + di];
                }
            }
            bicubiccalcderivatives(tf, c.x, c.y, c.m, c.n, ref dx, ref dy, ref dxy, _params);
            for (i = 0; i <= c.m - 1; i++)
            {
                for (j = 0; j <= c.n - 1; j++)
                {
                    k = c.d * (i * c.n + j) + di;
                    c.f[k] = tf[i, j];
                    c.f[c.n * c.m * c.d + k] = dx[i, j];
                    c.f[2 * c.n * c.m * c.d + k] = dy[i, j];
                    c.f[3 * c.n * c.m * c.d + k] = dxy[i, j];
                }
            }
        }
    }


    /*************************************************************************
    This  subroutine builds bicubic vector-valued  spline,  with  some  spline
    cells being missing due to missing nodes.

    This function produces C2-continuous spline, i.e. the has smooth first and
    second derivatives both inside spline cells and at the boundaries.

    When the node (i,j) is missing, it means that: a) we don't  have  function
    value at this point (elements of F[] are ignored), and  b)  we  don't need
    spline value at cells adjacent to the node (i,j), i.e. up to 4 spline cells
    will be dropped. An attempt to compute spline value at  the  missing  cell
    will return NAN.

    It is important to  understand  that  this  subroutine  does  NOT  support
    interpolation on scattered grids. It allows us to drop some nodes, but  at
    the cost of making a "hole in the spline" around this point. If  you  want
    function  that   can   "fill  the  gap",  use  RBF  or  another  scattered
    interpolation method.

    The  intended  usage  for  this  subroutine  are  regularly  sampled,  but
    non-rectangular datasets.

    Input parameters:
        X   -   spline abscissas, array[0..N-1]
        Y   -   spline ordinates, array[0..M-1]
        F   -   function values, array[0..M*N*D-1]:
                * first D elements store D values at (X[0],Y[0])
                * next D elements store D values at (X[1],Y[0])
                * general form - D function values at (X[i],Y[j]) are stored
                  at F[D*(J*N+I)...D*(J*N+I)+D-1].
                * missing values are ignored
        Missing array[M*N], Missing[J*N+I]=True means that corresponding entries
                of F[] are missing nodes.
        M,N -   grid size, M>=2, N>=2
        D   -   vector dimension, D>=1

    Output parameters:
        C   -   spline interpolant

      -- ALGLIB PROJECT --
         Copyright 27.06.2022 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dbuildbicubicmissing(double[] x,
        int n,
        double[] y,
        int m,
        double[] f,
        bool[] missing,
        int d,
        spline2dinterpolant c,
        xparams _params)
    {
        f = (double[])f.Clone();

        spline2dbuildbicubicmissingbuf(x, n, y, m, f, missing, d, c, _params);
    }


    /*************************************************************************
    This  subroutine builds bicubic vector-valued  spline,  with  some  spline
    cells being missing due to missing nodes.

    Buffered version  of  Spline2DBuildBicubicMissing()  which  reuses  memory
    previously allocated in C as much as possible.

      -- ALGLIB PROJECT --
         Copyright 27.06.2022 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dbuildbicubicmissingbuf(double[] x,
        int n,
        double[] y,
        int m,
        double[] f,
        bool[] missing,
        int d,
        spline2dinterpolant c,
        xparams _params)
    {
        double[,] tf = new double[0, 0];
        double[,] dx = new double[0, 0];
        double[,] dy = new double[0, 0];
        double[,] dxy = new double[0, 0];
        double t = 0;
        bool tb = new bool();
        int i = 0;
        int j = 0;
        int k = 0;
        int di = 0;
        int i0 = 0;

        f = (double[])f.Clone();

        ap.assert(n >= 2, "Spline2DBuildBicubicMissing: N is less than 2");
        ap.assert(m >= 2, "Spline2DBuildBicubicMissing: M is less than 2");
        ap.assert(d >= 1, "Spline2DBuildBicubicMissing: invalid argument D (D<1)");
        ap.assert(ap.len(x) >= n && ap.len(y) >= m, "Spline2DBuildBicubicMissing: length of X or Y is too short (Length(X/Y)<N/M)");
        ap.assert(apserv.isfinitevector(x, n, _params) && apserv.isfinitevector(y, m, _params), "Spline2DBuildBicubicMissing: X or Y contains NaN or Infinite value");
        k = n * m * d;
        ap.assert(ap.len(f) >= k, "Spline2DBuildBicubicMissing: length of F is too short (Length(F)<N*M*D)");
        ap.assert(ap.len(missing) >= n * m, "Spline2DBuildBicubicMissing: Missing[] is shorter than M*N");
        for (i = 0; i <= k - 1; i++)
        {
            if (!missing[i / d] && !math.isfinite(f[i]))
            {
                ap.assert(false, "Spline2DBuildBicubicMissing: F[] contains NAN or INF in its non-missing entries");
            }
        }

        //
        // Fill interpolant:
        //  F[0]...F[N*M*D-1]:
        //      f(i,j) table. f(0,0), f(0, 1), f(0,2) and so on...
        //  F[N*M*D]...F[2*N*M*D-1]:
        //      df(i,j)/dx table.
        //  F[2*N*M*D]...F[3*N*M*D-1]:
        //      df(i,j)/dy table.
        //  F[3*N*M*D]...F[4*N*M*D-1]:
        //      d2f(i,j)/dxdy table.
        //
        c.d = d;
        c.n = n;
        c.m = m;
        c.stype = -3;
        c.hasmissingcells = true;
        c.x = new double[c.n];
        c.y = new double[c.m];
        ablasf.rsetallocv(4 * k, 0.0, ref c.f, _params);
        ablasf.bcopyallocv(c.n * c.m, missing, ref c.ismissingnode, _params);
        tf = new double[c.m, c.n];
        for (i = 0; i <= c.n - 1; i++)
        {
            c.x[i] = x[i];
        }
        for (i = 0; i <= c.m - 1; i++)
        {
            c.y[i] = y[i];
        }
        for (i = 0; i <= k - 1; i++)
        {
            if (!missing[i / d])
            {
                c.f[i] = f[i];
            }
        }

        //
        // Sort points
        //
        for (j = 0; j <= c.n - 1; j++)
        {
            k = j;
            for (i = j + 1; i <= c.n - 1; i++)
            {
                if ((double)(c.x[i]) < (double)(c.x[k]))
                {
                    k = i;
                }
            }
            if (k != j)
            {
                for (i = 0; i <= c.m - 1; i++)
                {
                    for (i0 = 0; i0 <= c.d - 1; i0++)
                    {
                        t = c.f[c.d * (i * c.n + j) + i0];
                        c.f[c.d * (i * c.n + j) + i0] = c.f[c.d * (i * c.n + k) + i0];
                        c.f[c.d * (i * c.n + k) + i0] = t;
                    }
                    tb = c.ismissingnode[i * c.n + j];
                    c.ismissingnode[i * c.n + j] = c.ismissingnode[i * c.n + k];
                    c.ismissingnode[i * c.n + k] = tb;
                }
                t = c.x[j];
                c.x[j] = c.x[k];
                c.x[k] = t;
            }
        }
        for (i = 0; i <= c.m - 1; i++)
        {
            k = i;
            for (j = i + 1; j <= c.m - 1; j++)
            {
                if ((double)(c.y[j]) < (double)(c.y[k]))
                {
                    k = j;
                }
            }
            if (k != i)
            {
                for (j = 0; j <= c.n - 1; j++)
                {
                    for (i0 = 0; i0 <= c.d - 1; i0++)
                    {
                        t = c.f[c.d * (i * c.n + j) + i0];
                        c.f[c.d * (i * c.n + j) + i0] = c.f[c.d * (k * c.n + j) + i0];
                        c.f[c.d * (k * c.n + j) + i0] = t;
                    }
                    tb = c.ismissingnode[i * c.n + j];
                    c.ismissingnode[i * c.n + j] = c.ismissingnode[k * c.n + j];
                    c.ismissingnode[k * c.n + j] = tb;
                }
                t = c.y[i];
                c.y[i] = c.y[k];
                c.y[k] = t;
            }
        }

        //
        // 1. Determine cells that are not missing. A cell is non-missing if it has four non-missing
        //    nodes at its corners.
        // 2. Normalize IsMissingNode[] array - all isolated points that are not part of some non-missing
        //    cell are marked as missing too
        //
        ablasf.bsetallocv((c.m - 1) * (c.n - 1), true, ref c.ismissingcell, _params);
        for (i = 0; i <= c.m - 2; i++)
        {
            for (j = 0; j <= c.n - 2; j++)
            {
                if (((!c.ismissingnode[i * c.n + j] && !c.ismissingnode[(i + 1) * c.n + j]) && !c.ismissingnode[i * c.n + (j + 1)]) && !c.ismissingnode[(i + 1) * c.n + (j + 1)])
                {
                    c.ismissingcell[i * (c.n - 1) + j] = false;
                }
            }
        }
        ablasf.bsetv(c.m * c.n, true, c.ismissingnode, _params);
        for (i = 0; i <= c.m - 2; i++)
        {
            for (j = 0; j <= c.n - 2; j++)
            {
                if (!c.ismissingcell[i * (c.n - 1) + j])
                {
                    c.ismissingnode[i * c.n + j] = false;
                    c.ismissingnode[(i + 1) * c.n + j] = false;
                    c.ismissingnode[i * c.n + (j + 1)] = false;
                    c.ismissingnode[(i + 1) * c.n + (j + 1)] = false;
                }
            }
        }
        for (di = 0; di <= c.d - 1; di++)
        {
            for (i = 0; i <= c.m - 1; i++)
            {
                for (j = 0; j <= c.n - 1; j++)
                {
                    tf[i, j] = c.f[c.d * (i * c.n + j) + di];
                }
            }
            bicubiccalcderivativesmissing(tf, c.ismissingnode, c.x, c.y, c.m, c.n, ref dx, ref dy, ref dxy, _params);
            for (i = 0; i <= c.m - 1; i++)
            {
                for (j = 0; j <= c.n - 1; j++)
                {
                    k = c.d * (i * c.n + j) + di;
                    c.f[k] = tf[i, j];
                    c.f[c.n * c.m * c.d + k] = dx[i, j];
                    c.f[2 * c.n * c.m * c.d + k] = dy[i, j];
                    c.f[3 * c.n * c.m * c.d + k] = dxy[i, j];
                }
            }
        }
    }


    /*************************************************************************
    This subroutine unpacks two-dimensional spline into the coefficients table

    Input parameters:
        C   -   spline interpolant.

    Result:
        M, N-   grid size (x-axis and y-axis)
        D   -   number of components
        Tbl -   coefficients table, unpacked format,
                D - components: [0..(N-1)*(M-1)*D-1, 0..20].
                For T=0..D-1 (component index), I = 0...N-2 (x index),
                J=0..M-2 (y index):
                    K :=  T + I*D + J*D*(N-1)
                    
                    K-th row stores decomposition for T-th component of the
                    vector-valued function
                    
                    Tbl[K,0] = X[i]
                    Tbl[K,1] = X[i+1]
                    Tbl[K,2] = Y[j]
                    Tbl[K,3] = Y[j+1]
                    Tbl[K,4] = C00
                    Tbl[K,5] = C01
                    Tbl[K,6] = C02
                    Tbl[K,7] = C03
                    Tbl[K,8] = C10
                    Tbl[K,9] = C11
                    ...
                    Tbl[K,19] = C33
                    Tbl[K,20] = 1 if the cell is present, 0 if the cell is missing.
                                In the latter case Tbl[4..19] are exactly zero.
                On each grid square spline is equals to:
                    S(x) = SUM(c[i,j]*(t^i)*(u^j), i=0..3, j=0..3)
                    t = x-x[j]
                    u = y-y[i]

      -- ALGLIB PROJECT --
         Copyright 16.04.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dunpackv(spline2dinterpolant c,
        ref int m,
        ref int n,
        ref int d,
        ref double[,] tbl,
        xparams _params)
    {
        int k = 0;
        int p = 0;
        int ci = 0;
        int cj = 0;
        int s1 = 0;
        int s2 = 0;
        int s3 = 0;
        int s4 = 0;
        int sfx = 0;
        int sfy = 0;
        int sfxy = 0;
        double y1 = 0;
        double y2 = 0;
        double y3 = 0;
        double y4 = 0;
        double dt = 0;
        double du = 0;
        int i = 0;
        int j = 0;
        int k0 = 0;

        m = 0;
        n = 0;
        d = 0;
        tbl = new double[0, 0];

        ap.assert(c.stype == -3 || c.stype == -1, "Spline2DUnpackV: incorrect C (incorrect parameter C.SType)");
        n = c.n;
        m = c.m;
        d = c.d;
        ablasf.rsetallocm((n - 1) * (m - 1) * d, 21, 0.0, ref tbl, _params);
        sfx = n * m * d;
        sfy = 2 * n * m * d;
        sfxy = 3 * n * m * d;
        for (i = 0; i <= m - 2; i++)
        {
            for (j = 0; j <= n - 2; j++)
            {
                for (k = 0; k <= d - 1; k++)
                {
                    p = d * (i * (n - 1) + j) + k;

                    //
                    // Set up cell dimensions (always present)
                    //
                    tbl[p, 0] = c.x[j];
                    tbl[p, 1] = c.x[j + 1];
                    tbl[p, 2] = c.y[i];
                    tbl[p, 3] = c.y[i + 1];
                    dt = 1 / (tbl[p, 1] - tbl[p, 0]);
                    du = 1 / (tbl[p, 3] - tbl[p, 2]);

                    //
                    // Skip cell if it is missing
                    //
                    if (c.hasmissingcells && c.ismissingcell[i * (c.n - 1) + j])
                    {
                        continue;
                    }
                    tbl[p, 20] = 1;

                    //
                    // Bilinear interpolation: output coefficients
                    //
                    if (c.stype == -1)
                    {
                        for (k0 = 4; k0 <= 19; k0++)
                        {
                            tbl[p, k0] = 0;
                        }
                        y1 = c.f[d * (n * i + j) + k];
                        y2 = c.f[d * (n * i + (j + 1)) + k];
                        y3 = c.f[d * (n * (i + 1) + (j + 1)) + k];
                        y4 = c.f[d * (n * (i + 1) + j) + k];
                        tbl[p, 4] = y1;
                        tbl[p, 4 + 1 * 4 + 0] = y2 - y1;
                        tbl[p, 4 + 0 * 4 + 1] = y4 - y1;
                        tbl[p, 4 + 1 * 4 + 1] = y3 - y2 - y4 + y1;
                    }

                    //
                    // Bicubic interpolation: output coefficients
                    //
                    if (c.stype == -3)
                    {
                        s1 = d * (n * i + j) + k;
                        s2 = d * (n * i + (j + 1)) + k;
                        s3 = d * (n * (i + 1) + (j + 1)) + k;
                        s4 = d * (n * (i + 1) + j) + k;
                        tbl[p, 4 + 0 * 4 + 0] = c.f[s1];
                        tbl[p, 4 + 0 * 4 + 1] = c.f[sfy + s1] / du;
                        tbl[p, 4 + 0 * 4 + 2] = -(3 * c.f[s1]) + 3 * c.f[s4] - 2 * c.f[sfy + s1] / du - c.f[sfy + s4] / du;
                        tbl[p, 4 + 0 * 4 + 3] = 2 * c.f[s1] - 2 * c.f[s4] + c.f[sfy + s1] / du + c.f[sfy + s4] / du;
                        tbl[p, 4 + 1 * 4 + 0] = c.f[sfx + s1] / dt;
                        tbl[p, 4 + 1 * 4 + 1] = c.f[sfxy + s1] / (dt * du);
                        tbl[p, 4 + 1 * 4 + 2] = -(3 * c.f[sfx + s1] / dt) + 3 * c.f[sfx + s4] / dt - 2 * c.f[sfxy + s1] / (dt * du) - c.f[sfxy + s4] / (dt * du);
                        tbl[p, 4 + 1 * 4 + 3] = 2 * c.f[sfx + s1] / dt - 2 * c.f[sfx + s4] / dt + c.f[sfxy + s1] / (dt * du) + c.f[sfxy + s4] / (dt * du);
                        tbl[p, 4 + 2 * 4 + 0] = -(3 * c.f[s1]) + 3 * c.f[s2] - 2 * c.f[sfx + s1] / dt - c.f[sfx + s2] / dt;
                        tbl[p, 4 + 2 * 4 + 1] = -(3 * c.f[sfy + s1] / du) + 3 * c.f[sfy + s2] / du - 2 * c.f[sfxy + s1] / (dt * du) - c.f[sfxy + s2] / (dt * du);
                        tbl[p, 4 + 2 * 4 + 2] = 9 * c.f[s1] - 9 * c.f[s2] + 9 * c.f[s3] - 9 * c.f[s4] + 6 * c.f[sfx + s1] / dt + 3 * c.f[sfx + s2] / dt - 3 * c.f[sfx + s3] / dt - 6 * c.f[sfx + s4] / dt + 6 * c.f[sfy + s1] / du - 6 * c.f[sfy + s2] / du - 3 * c.f[sfy + s3] / du + 3 * c.f[sfy + s4] / du + 4 * c.f[sfxy + s1] / (dt * du) + 2 * c.f[sfxy + s2] / (dt * du) + c.f[sfxy + s3] / (dt * du) + 2 * c.f[sfxy + s4] / (dt * du);
                        tbl[p, 4 + 2 * 4 + 3] = -(6 * c.f[s1]) + 6 * c.f[s2] - 6 * c.f[s3] + 6 * c.f[s4] - 4 * c.f[sfx + s1] / dt - 2 * c.f[sfx + s2] / dt + 2 * c.f[sfx + s3] / dt + 4 * c.f[sfx + s4] / dt - 3 * c.f[sfy + s1] / du + 3 * c.f[sfy + s2] / du + 3 * c.f[sfy + s3] / du - 3 * c.f[sfy + s4] / du - 2 * c.f[sfxy + s1] / (dt * du) - c.f[sfxy + s2] / (dt * du) - c.f[sfxy + s3] / (dt * du) - 2 * c.f[sfxy + s4] / (dt * du);
                        tbl[p, 4 + 3 * 4 + 0] = 2 * c.f[s1] - 2 * c.f[s2] + c.f[sfx + s1] / dt + c.f[sfx + s2] / dt;
                        tbl[p, 4 + 3 * 4 + 1] = 2 * c.f[sfy + s1] / du - 2 * c.f[sfy + s2] / du + c.f[sfxy + s1] / (dt * du) + c.f[sfxy + s2] / (dt * du);
                        tbl[p, 4 + 3 * 4 + 2] = -(6 * c.f[s1]) + 6 * c.f[s2] - 6 * c.f[s3] + 6 * c.f[s4] - 3 * c.f[sfx + s1] / dt - 3 * c.f[sfx + s2] / dt + 3 * c.f[sfx + s3] / dt + 3 * c.f[sfx + s4] / dt - 4 * c.f[sfy + s1] / du + 4 * c.f[sfy + s2] / du + 2 * c.f[sfy + s3] / du - 2 * c.f[sfy + s4] / du - 2 * c.f[sfxy + s1] / (dt * du) - 2 * c.f[sfxy + s2] / (dt * du) - c.f[sfxy + s3] / (dt * du) - c.f[sfxy + s4] / (dt * du);
                        tbl[p, 4 + 3 * 4 + 3] = 4 * c.f[s1] - 4 * c.f[s2] + 4 * c.f[s3] - 4 * c.f[s4] + 2 * c.f[sfx + s1] / dt + 2 * c.f[sfx + s2] / dt - 2 * c.f[sfx + s3] / dt - 2 * c.f[sfx + s4] / dt + 2 * c.f[sfy + s1] / du - 2 * c.f[sfy + s2] / du - 2 * c.f[sfy + s3] / du + 2 * c.f[sfy + s4] / du + c.f[sfxy + s1] / (dt * du) + c.f[sfxy + s2] / (dt * du) + c.f[sfxy + s3] / (dt * du) + c.f[sfxy + s4] / (dt * du);
                    }

                    //
                    // Rescale Cij
                    //
                    for (ci = 0; ci <= 3; ci++)
                    {
                        for (cj = 0; cj <= 3; cj++)
                        {
                            tbl[p, 4 + ci * 4 + cj] = tbl[p, 4 + ci * 4 + cj] * Math.Pow(dt, ci) * Math.Pow(du, cj);
                        }
                    }
                }
            }
        }
    }


    /*************************************************************************
    This subroutine was deprecated in ALGLIB 3.6.0

    We recommend you to switch  to  Spline2DBuildBilinearV(),  which  is  more
    flexible and accepts its arguments in more convenient order.

      -- ALGLIB PROJECT --
         Copyright 05.07.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dbuildbilinear(double[] x,
        double[] y,
        double[,] f,
        int m,
        int n,
        spline2dinterpolant c,
        xparams _params)
    {
        double t = 0;
        int i = 0;
        int j = 0;
        int k = 0;

        ap.assert(n >= 2, "Spline2DBuildBilinear: N<2");
        ap.assert(m >= 2, "Spline2DBuildBilinear: M<2");
        ap.assert(ap.len(x) >= n && ap.len(y) >= m, "Spline2DBuildBilinear: length of X or Y is too short (Length(X/Y)<N/M)");
        ap.assert(apserv.isfinitevector(x, n, _params) && apserv.isfinitevector(y, m, _params), "Spline2DBuildBilinear: X or Y contains NaN or Infinite value");
        ap.assert(ap.rows(f) >= m && ap.cols(f) >= n, "Spline2DBuildBilinear: size of F is too small (rows(F)<M or cols(F)<N)");
        ap.assert(apserv.apservisfinitematrix(f, m, n, _params), "Spline2DBuildBilinear: F contains NaN or Infinite value");

        //
        // Fill interpolant
        //
        c.n = n;
        c.m = m;
        c.d = 1;
        c.stype = -1;
        c.hasmissingcells = false;
        c.x = new double[c.n];
        c.y = new double[c.m];
        c.f = new double[c.n * c.m];
        for (i = 0; i <= c.n - 1; i++)
        {
            c.x[i] = x[i];
        }
        for (i = 0; i <= c.m - 1; i++)
        {
            c.y[i] = y[i];
        }
        for (i = 0; i <= c.m - 1; i++)
        {
            for (j = 0; j <= c.n - 1; j++)
            {
                c.f[i * c.n + j] = f[i, j];
            }
        }

        //
        // Sort points
        //
        for (j = 0; j <= c.n - 1; j++)
        {
            k = j;
            for (i = j + 1; i <= c.n - 1; i++)
            {
                if ((double)(c.x[i]) < (double)(c.x[k]))
                {
                    k = i;
                }
            }
            if (k != j)
            {
                for (i = 0; i <= c.m - 1; i++)
                {
                    t = c.f[i * c.n + j];
                    c.f[i * c.n + j] = c.f[i * c.n + k];
                    c.f[i * c.n + k] = t;
                }
                t = c.x[j];
                c.x[j] = c.x[k];
                c.x[k] = t;
            }
        }
        for (i = 0; i <= c.m - 1; i++)
        {
            k = i;
            for (j = i + 1; j <= c.m - 1; j++)
            {
                if ((double)(c.y[j]) < (double)(c.y[k]))
                {
                    k = j;
                }
            }
            if (k != i)
            {
                for (j = 0; j <= c.n - 1; j++)
                {
                    t = c.f[i * c.n + j];
                    c.f[i * c.n + j] = c.f[k * c.n + j];
                    c.f[k * c.n + j] = t;
                }
                t = c.y[i];
                c.y[i] = c.y[k];
                c.y[k] = t;
            }
        }
    }


    /*************************************************************************
    This subroutine was deprecated in ALGLIB 3.6.0

    We recommend you to switch  to  Spline2DBuildBicubicV(),  which  is  more
    flexible and accepts its arguments in more convenient order.

      -- ALGLIB PROJECT --
         Copyright 05.07.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dbuildbicubic(double[] x,
        double[] y,
        double[,] f,
        int m,
        int n,
        spline2dinterpolant c,
        xparams _params)
    {
        int sfx = 0;
        int sfy = 0;
        int sfxy = 0;
        double[,] dx = new double[0, 0];
        double[,] dy = new double[0, 0];
        double[,] dxy = new double[0, 0];
        double t = 0;
        int i = 0;
        int j = 0;
        int k = 0;

        f = (double[,])f.Clone();

        ap.assert(n >= 2, "Spline2DBuildBicubicSpline: N<2");
        ap.assert(m >= 2, "Spline2DBuildBicubicSpline: M<2");
        ap.assert(ap.len(x) >= n && ap.len(y) >= m, "Spline2DBuildBicubic: length of X or Y is too short (Length(X/Y)<N/M)");
        ap.assert(apserv.isfinitevector(x, n, _params) && apserv.isfinitevector(y, m, _params), "Spline2DBuildBicubic: X or Y contains NaN or Infinite value");
        ap.assert(ap.rows(f) >= m && ap.cols(f) >= n, "Spline2DBuildBicubic: size of F is too small (rows(F)<M or cols(F)<N)");
        ap.assert(apserv.apservisfinitematrix(f, m, n, _params), "Spline2DBuildBicubic: F contains NaN or Infinite value");

        //
        // Fill interpolant:
        //  F[0]...F[N*M-1]:
        //      f(i,j) table. f(0,0), f(0, 1), f(0,2) and so on...
        //  F[N*M]...F[2*N*M-1]:
        //      df(i,j)/dx table.
        //  F[2*N*M]...F[3*N*M-1]:
        //      df(i,j)/dy table.
        //  F[3*N*M]...F[4*N*M-1]:
        //      d2f(i,j)/dxdy table.
        //
        c.d = 1;
        c.n = n;
        c.m = m;
        c.stype = -3;
        c.hasmissingcells = false;
        sfx = c.n * c.m;
        sfy = 2 * c.n * c.m;
        sfxy = 3 * c.n * c.m;
        c.x = new double[c.n];
        c.y = new double[c.m];
        c.f = new double[4 * c.n * c.m];
        for (i = 0; i <= c.n - 1; i++)
        {
            c.x[i] = x[i];
        }
        for (i = 0; i <= c.m - 1; i++)
        {
            c.y[i] = y[i];
        }

        //
        // Sort points
        //
        for (j = 0; j <= c.n - 1; j++)
        {
            k = j;
            for (i = j + 1; i <= c.n - 1; i++)
            {
                if ((double)(c.x[i]) < (double)(c.x[k]))
                {
                    k = i;
                }
            }
            if (k != j)
            {
                for (i = 0; i <= c.m - 1; i++)
                {
                    t = f[i, j];
                    f[i, j] = f[i, k];
                    f[i, k] = t;
                }
                t = c.x[j];
                c.x[j] = c.x[k];
                c.x[k] = t;
            }
        }
        for (i = 0; i <= c.m - 1; i++)
        {
            k = i;
            for (j = i + 1; j <= c.m - 1; j++)
            {
                if ((double)(c.y[j]) < (double)(c.y[k]))
                {
                    k = j;
                }
            }
            if (k != i)
            {
                for (j = 0; j <= c.n - 1; j++)
                {
                    t = f[i, j];
                    f[i, j] = f[k, j];
                    f[k, j] = t;
                }
                t = c.y[i];
                c.y[i] = c.y[k];
                c.y[k] = t;
            }
        }
        bicubiccalcderivatives(f, c.x, c.y, c.m, c.n, ref dx, ref dy, ref dxy, _params);
        for (i = 0; i <= c.m - 1; i++)
        {
            for (j = 0; j <= c.n - 1; j++)
            {
                k = i * c.n + j;
                c.f[k] = f[i, j];
                c.f[sfx + k] = dx[i, j];
                c.f[sfy + k] = dy[i, j];
                c.f[sfxy + k] = dxy[i, j];
            }
        }
    }


    /*************************************************************************
    This subroutine was deprecated in ALGLIB 3.6.0

    We recommend you to switch  to  Spline2DUnpackV(),  which is more flexible
    and accepts its arguments in more convenient order.

      -- ALGLIB PROJECT --
         Copyright 29.06.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dunpack(spline2dinterpolant c,
        ref int m,
        ref int n,
        ref double[,] tbl,
        xparams _params)
    {
        int k = 0;
        int p = 0;
        int ci = 0;
        int cj = 0;
        int s1 = 0;
        int s2 = 0;
        int s3 = 0;
        int s4 = 0;
        int sfx = 0;
        int sfy = 0;
        int sfxy = 0;
        double y1 = 0;
        double y2 = 0;
        double y3 = 0;
        double y4 = 0;
        double dt = 0;
        double du = 0;
        int i = 0;
        int j = 0;

        m = 0;
        n = 0;
        tbl = new double[0, 0];

        ap.assert(c.stype == -3 || c.stype == -1, "Spline2DUnpack: incorrect C (incorrect parameter C.SType)");
        if (c.d != 1)
        {
            n = 0;
            m = 0;
            return;
        }
        n = c.n;
        m = c.m;
        tbl = new double[(n - 1) * (m - 1), 20];
        sfx = n * m;
        sfy = 2 * n * m;
        sfxy = 3 * n * m;

        //
        // Fill
        //
        for (i = 0; i <= m - 2; i++)
        {
            for (j = 0; j <= n - 2; j++)
            {
                p = i * (n - 1) + j;
                tbl[p, 0] = c.x[j];
                tbl[p, 1] = c.x[j + 1];
                tbl[p, 2] = c.y[i];
                tbl[p, 3] = c.y[i + 1];
                dt = 1 / (tbl[p, 1] - tbl[p, 0]);
                du = 1 / (tbl[p, 3] - tbl[p, 2]);

                //
                // Bilinear interpolation
                //
                if (c.stype == -1)
                {
                    for (k = 4; k <= 19; k++)
                    {
                        tbl[p, k] = 0;
                    }
                    y1 = c.f[n * i + j];
                    y2 = c.f[n * i + (j + 1)];
                    y3 = c.f[n * (i + 1) + (j + 1)];
                    y4 = c.f[n * (i + 1) + j];
                    tbl[p, 4] = y1;
                    tbl[p, 4 + 1 * 4 + 0] = y2 - y1;
                    tbl[p, 4 + 0 * 4 + 1] = y4 - y1;
                    tbl[p, 4 + 1 * 4 + 1] = y3 - y2 - y4 + y1;
                }

                //
                // Bicubic interpolation
                //
                if (c.stype == -3)
                {
                    s1 = n * i + j;
                    s2 = n * i + (j + 1);
                    s3 = n * (i + 1) + (j + 1);
                    s4 = n * (i + 1) + j;
                    tbl[p, 4 + 0 * 4 + 0] = c.f[s1];
                    tbl[p, 4 + 0 * 4 + 1] = c.f[sfy + s1] / du;
                    tbl[p, 4 + 0 * 4 + 2] = -(3 * c.f[s1]) + 3 * c.f[s4] - 2 * c.f[sfy + s1] / du - c.f[sfy + s4] / du;
                    tbl[p, 4 + 0 * 4 + 3] = 2 * c.f[s1] - 2 * c.f[s4] + c.f[sfy + s1] / du + c.f[sfy + s4] / du;
                    tbl[p, 4 + 1 * 4 + 0] = c.f[sfx + s1] / dt;
                    tbl[p, 4 + 1 * 4 + 1] = c.f[sfxy + s1] / (dt * du);
                    tbl[p, 4 + 1 * 4 + 2] = -(3 * c.f[sfx + s1] / dt) + 3 * c.f[sfx + s4] / dt - 2 * c.f[sfxy + s1] / (dt * du) - c.f[sfxy + s4] / (dt * du);
                    tbl[p, 4 + 1 * 4 + 3] = 2 * c.f[sfx + s1] / dt - 2 * c.f[sfx + s4] / dt + c.f[sfxy + s1] / (dt * du) + c.f[sfxy + s4] / (dt * du);
                    tbl[p, 4 + 2 * 4 + 0] = -(3 * c.f[s1]) + 3 * c.f[s2] - 2 * c.f[sfx + s1] / dt - c.f[sfx + s2] / dt;
                    tbl[p, 4 + 2 * 4 + 1] = -(3 * c.f[sfy + s1] / du) + 3 * c.f[sfy + s2] / du - 2 * c.f[sfxy + s1] / (dt * du) - c.f[sfxy + s2] / (dt * du);
                    tbl[p, 4 + 2 * 4 + 2] = 9 * c.f[s1] - 9 * c.f[s2] + 9 * c.f[s3] - 9 * c.f[s4] + 6 * c.f[sfx + s1] / dt + 3 * c.f[sfx + s2] / dt - 3 * c.f[sfx + s3] / dt - 6 * c.f[sfx + s4] / dt + 6 * c.f[sfy + s1] / du - 6 * c.f[sfy + s2] / du - 3 * c.f[sfy + s3] / du + 3 * c.f[sfy + s4] / du + 4 * c.f[sfxy + s1] / (dt * du) + 2 * c.f[sfxy + s2] / (dt * du) + c.f[sfxy + s3] / (dt * du) + 2 * c.f[sfxy + s4] / (dt * du);
                    tbl[p, 4 + 2 * 4 + 3] = -(6 * c.f[s1]) + 6 * c.f[s2] - 6 * c.f[s3] + 6 * c.f[s4] - 4 * c.f[sfx + s1] / dt - 2 * c.f[sfx + s2] / dt + 2 * c.f[sfx + s3] / dt + 4 * c.f[sfx + s4] / dt - 3 * c.f[sfy + s1] / du + 3 * c.f[sfy + s2] / du + 3 * c.f[sfy + s3] / du - 3 * c.f[sfy + s4] / du - 2 * c.f[sfxy + s1] / (dt * du) - c.f[sfxy + s2] / (dt * du) - c.f[sfxy + s3] / (dt * du) - 2 * c.f[sfxy + s4] / (dt * du);
                    tbl[p, 4 + 3 * 4 + 0] = 2 * c.f[s1] - 2 * c.f[s2] + c.f[sfx + s1] / dt + c.f[sfx + s2] / dt;
                    tbl[p, 4 + 3 * 4 + 1] = 2 * c.f[sfy + s1] / du - 2 * c.f[sfy + s2] / du + c.f[sfxy + s1] / (dt * du) + c.f[sfxy + s2] / (dt * du);
                    tbl[p, 4 + 3 * 4 + 2] = -(6 * c.f[s1]) + 6 * c.f[s2] - 6 * c.f[s3] + 6 * c.f[s4] - 3 * c.f[sfx + s1] / dt - 3 * c.f[sfx + s2] / dt + 3 * c.f[sfx + s3] / dt + 3 * c.f[sfx + s4] / dt - 4 * c.f[sfy + s1] / du + 4 * c.f[sfy + s2] / du + 2 * c.f[sfy + s3] / du - 2 * c.f[sfy + s4] / du - 2 * c.f[sfxy + s1] / (dt * du) - 2 * c.f[sfxy + s2] / (dt * du) - c.f[sfxy + s3] / (dt * du) - c.f[sfxy + s4] / (dt * du);
                    tbl[p, 4 + 3 * 4 + 3] = 4 * c.f[s1] - 4 * c.f[s2] + 4 * c.f[s3] - 4 * c.f[s4] + 2 * c.f[sfx + s1] / dt + 2 * c.f[sfx + s2] / dt - 2 * c.f[sfx + s3] / dt - 2 * c.f[sfx + s4] / dt + 2 * c.f[sfy + s1] / du - 2 * c.f[sfy + s2] / du - 2 * c.f[sfy + s3] / du + 2 * c.f[sfy + s4] / du + c.f[sfxy + s1] / (dt * du) + c.f[sfxy + s2] / (dt * du) + c.f[sfxy + s3] / (dt * du) + c.f[sfxy + s4] / (dt * du);
                }

                //
                // Rescale Cij
                //
                for (ci = 0; ci <= 3; ci++)
                {
                    for (cj = 0; cj <= 3; cj++)
                    {
                        tbl[p, 4 + ci * 4 + cj] = tbl[p, 4 + ci * 4 + cj] * Math.Pow(dt, ci) * Math.Pow(du, cj);
                    }
                }
            }
        }
    }


    /*************************************************************************
    This subroutine creates least squares solver used to  fit  2D  splines  to
    irregularly sampled (scattered) data.

    Solver object is used to perform spline fits as follows:
    * solver object is created with spline2dbuildercreate() function
    * dataset is added with spline2dbuildersetpoints() function
    * fit area is chosen:
      * spline2dbuildersetarea()     - for user-defined area
      * spline2dbuildersetareaauto() - for automatically chosen area
    * number of grid nodes is chosen with spline2dbuildersetgrid()
    * prior term is chosen with one of the following functions:
      * spline2dbuildersetlinterm()   to set linear prior
      * spline2dbuildersetconstterm() to set constant prior
      * spline2dbuildersetzeroterm()  to set zero prior
      * spline2dbuildersetuserterm()  to set user-defined constant prior
    * solver algorithm is chosen with either:
      * spline2dbuildersetalgoblocklls() - BlockLLS algorithm, medium-scale problems
      * spline2dbuildersetalgofastddm()  - FastDDM algorithm, large-scale problems
    * finally, fitting itself is performed with spline2dfit() function.

    Most of the steps above can be omitted,  solver  is  configured with  good
    defaults. The minimum is to call:
    * spline2dbuildercreate() to create solver object
    * spline2dbuildersetpoints() to specify dataset
    * spline2dbuildersetgrid() to tell how many nodes you need
    * spline2dfit() to perform fit

      ! COMMERCIAL EDITION OF ALGLIB:
      ! 
      ! Commercial Edition of ALGLIB includes following important improvements
      ! of this function:
      ! * high-performance native backend with same C# interface (C# version)
      ! * multithreading support (C++ and C# versions)
      ! * hardware vendor (Intel) implementations of linear algebra primitives
      !   (C++ and C# versions, x86/x64 platform)
      ! 
      ! We recommend you to read 'Working with commercial version' section  of
      ! ALGLIB Reference Manual in order to find out how to  use  performance-
      ! related features provided by commercial edition of ALGLIB.

    INPUT PARAMETERS:
        D   -   positive number, number of Y-components: D=1 for simple scalar
                fit, D>1 for vector-valued spline fitting.
        
    OUTPUT PARAMETERS:
        S   -   solver object

      -- ALGLIB PROJECT --
         Copyright 29.01.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dbuildercreate(int d,
        spline2dbuilder state,
        xparams _params)
    {
        ap.assert(d >= 1, "Spline2DBuilderCreate: D<=0");

        //
        // NOTES:
        //
        // 1. Prior term is set to linear one (good default option)
        // 2. Solver is set to BlockLLS - good enough for small-scale problems.
        // 3. Refinement rounds: 5; enough to get good convergence.
        //
        state.priorterm = 1;
        state.priortermval = 0;
        state.areatype = 0;
        state.gridtype = 0;
        state.smoothing = 0.0;
        state.nlayers = 0;
        state.solvertype = 1;
        state.npoints = 0;
        state.d = d;
        state.sx = 1.0;
        state.sy = 1.0;
        state.lsqrcnt = 5;

        //
        // Algorithm settings
        //
        state.adddegreeoffreedom = true;
        state.maxcoresize = 16;
        state.interfacesize = 5;
    }


    /*************************************************************************
    This function sets constant prior term (model is a sum of  bicubic  spline
    and global prior, which can be linear, constant, user-defined  constant or
    zero).

    Constant prior term is determined by least squares fitting.

    INPUT PARAMETERS:
        S       -   spline builder
        V       -   value for user-defined prior

      -- ALGLIB --
         Copyright 01.02.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dbuildersetuserterm(spline2dbuilder state,
        double v,
        xparams _params)
    {
        ap.assert(math.isfinite(v), "Spline2DBuilderSetUserTerm: infinite/NAN value passed");
        state.priorterm = 0;
        state.priortermval = v;
    }


    /*************************************************************************
    This function sets linear prior term (model is a sum of bicubic spline and
    global  prior,  which  can  be  linear, constant, user-defined constant or
    zero).

    Linear prior term is determined by least squares fitting.

    INPUT PARAMETERS:
        S       -   spline builder

      -- ALGLIB --
         Copyright 01.02.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dbuildersetlinterm(spline2dbuilder state,
        xparams _params)
    {
        state.priorterm = 1;
    }


    /*************************************************************************
    This function sets constant prior term (model is a sum of  bicubic  spline
    and global prior, which can be linear, constant, user-defined  constant or
    zero).

    Constant prior term is determined by least squares fitting.

    INPUT PARAMETERS:
        S       -   spline builder

      -- ALGLIB --
         Copyright 01.02.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dbuildersetconstterm(spline2dbuilder state,
        xparams _params)
    {
        state.priorterm = 2;
    }


    /*************************************************************************
    This function sets zero prior term (model is a sum of bicubic  spline  and
    global  prior,  which  can  be  linear, constant, user-defined constant or
    zero).

    INPUT PARAMETERS:
        S       -   spline builder

      -- ALGLIB --
         Copyright 01.02.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dbuildersetzeroterm(spline2dbuilder state,
        xparams _params)
    {
        state.priorterm = 3;
    }


    /*************************************************************************
    This function adds dataset to the builder object.

    This function overrides results of the previous calls, i.e. multiple calls
    of this function will result in only the last set being added.

    INPUT PARAMETERS:
        S       -   spline 2D builder object
        XY      -   points, array[N,2+D]. One  row  corresponds to  one  point
                    in the dataset. First 2  elements  are  coordinates,  next
                    D  elements are function values. Array may  be larger than 
                    specified, in  this  case  only leading [N,NX+NY] elements 
                    will be used.
        N       -   number of points in the dataset

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dbuildersetpoints(spline2dbuilder state,
        double[,] xy,
        int n,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int ew = 0;

        ap.assert(n > 0, "Spline2DBuilderSetPoints: N<0");
        ap.assert(ap.rows(xy) >= n, "Spline2DBuilderSetPoints: Rows(XY)<N");
        ap.assert(ap.cols(xy) >= 2 + state.d, "Spline2DBuilderSetPoints: Cols(XY)<NX+NY");
        ap.assert(apserv.apservisfinitematrix(xy, n, 2 + state.d, _params), "Spline2DBuilderSetPoints: XY contains infinite or NaN values!");
        state.npoints = n;
        ew = 2 + state.d;
        apserv.rvectorsetlengthatleast(ref state.xy, n * ew, _params);
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= ew - 1; j++)
            {
                state.xy[i * ew + j] = xy[i, j];
            }
        }
    }


    /*************************************************************************
    This function sets area where 2D spline interpolant is built. "Auto" means
    that area extent is determined automatically from dataset extent.

    INPUT PARAMETERS:
        S       -   spline 2D builder object

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dbuildersetareaauto(spline2dbuilder state,
        xparams _params)
    {
        state.areatype = 0;
    }


    /*************************************************************************
    This  function  sets  area  where  2D  spline  interpolant  is   built  to
    user-defined one: [XA,XB]*[YA,YB]

    INPUT PARAMETERS:
        S       -   spline 2D builder object
        XA,XB   -   spatial extent in the first (X) dimension, XA<XB
        YA,YB   -   spatial extent in the second (Y) dimension, YA<YB

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dbuildersetarea(spline2dbuilder state,
        double xa,
        double xb,
        double ya,
        double yb,
        xparams _params)
    {
        ap.assert(math.isfinite(xa), "Spline2DBuilderSetArea: XA is not finite");
        ap.assert(math.isfinite(xb), "Spline2DBuilderSetArea: XB is not finite");
        ap.assert(math.isfinite(ya), "Spline2DBuilderSetArea: YA is not finite");
        ap.assert(math.isfinite(yb), "Spline2DBuilderSetArea: YB is not finite");
        ap.assert((double)(xa) < (double)(xb), "Spline2DBuilderSetArea: XA>=XB");
        ap.assert((double)(ya) < (double)(yb), "Spline2DBuilderSetArea: YA>=YB");
        state.areatype = 1;
        state.xa = xa;
        state.xb = xb;
        state.ya = ya;
        state.yb = yb;
    }


    /*************************************************************************
    This  function  sets  nodes  count  for  2D spline interpolant. Fitting is
    performed on area defined with one of the "setarea"  functions;  this  one
    sets number of nodes placed upon the fitting area.

    INPUT PARAMETERS:
        S       -   spline 2D builder object
        KX      -   nodes count for the first (X) dimension; fitting  interval
                    [XA,XB] is separated into KX-1 subintervals, with KX nodes
                    created at the boundaries.
        KY      -   nodes count for the first (Y) dimension; fitting  interval
                    [YA,YB] is separated into KY-1 subintervals, with KY nodes
                    created at the boundaries.

    NOTE: at  least  4  nodes  is  created in each dimension, so KX and KY are
          silently increased if needed.

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dbuildersetgrid(spline2dbuilder state,
        int kx,
        int ky,
        xparams _params)
    {
        ap.assert(kx > 0, "Spline2DBuilderSetGridSizePrecisely: KX<=0");
        ap.assert(ky > 0, "Spline2DBuilderSetGridSizePrecisely: KY<=0");
        state.gridtype = 1;
        state.kx = Math.Max(kx, 4);
        state.ky = Math.Max(ky, 4);
    }


    /*************************************************************************
    This  function  allows  you to choose least squares solver used to perform
    fitting. This function sets solver algorithm to "FastDDM", which  performs
    fast parallel fitting by splitting problem into smaller chunks and merging
    results together.

    Unlike BlockLLS, this solver produces merely C1 continuous models  because
    domain decomposition part disrupts C2 continuity.

    This solver is optimized for large-scale problems, starting  from  256x256
    grids, and up to 10000x10000 grids. Of course, it will  work  for  smaller
    grids too.

    More detailed description of the algorithm is given below:
    * algorithm generates hierarchy  of  nested  grids,  ranging  from  ~16x16
      (topmost "layer" of the model) to ~KX*KY one (final layer). Upper layers
      model global behavior of the function, lower layers are  used  to  model
      fine details. Moving from layer to layer doubles grid density.
    * fitting  is  started  from  topmost  layer, subsequent layers are fitted
      using residuals from previous ones.
    * user may choose to skip generation of upper layers and generate  only  a
      few bottom ones, which  will  result  in  much  better  performance  and
      parallelization efficiency, at the cost of algorithm inability to "patch"
      large holes in the dataset.
    * every layer is regularized using progressively increasing regularization
      coefficient; thus, increasing  LambdaV  penalizes  fine  details  first,
      leaving lower frequencies almost intact for a while.
    * after fitting is done, all layers are merged together into  one  bicubic
      spline
      
    IMPORTANT: regularization coefficient used by  this  solver  is  different
               from the one used by  BlockLLS.  Latter  utilizes  nonlinearity
               penalty,  which  is  global  in  nature  (large  regularization
               results in global linear trend being  extracted);  this  solver
               uses another, localized form of penalty, which is suitable  for
               parallel processing.

    Notes on memory and performance:
    * memory requirements: most memory is consumed  during  modeling   of  the
      higher layers; ~[512*NPoints] bytes is required for a  model  with  full
      hierarchy of grids being generated. However, if you skip a  few  topmost
      layers, you will get nearly constant (wrt. points count and  grid  size)
      memory consumption.
    * serial running time: O(K*K)+O(NPoints) for a KxK grid
    * parallelism potential: good. You may get  nearly  linear  speed-up  when
      performing fitting with just a few layers. Adding more layers results in
      model becoming more global, which somewhat  reduces  efficiency  of  the
      parallel code.

      ! COMMERCIAL EDITION OF ALGLIB:
      ! 
      ! Commercial Edition of ALGLIB includes following important improvements
      ! of this function:
      ! * high-performance native backend with same C# interface (C# version)
      ! * multithreading support (C++ and C# versions)
      ! * hardware vendor (Intel) implementations of linear algebra primitives
      !   (C++ and C# versions, x86/x64 platform)
      ! 
      ! We recommend you to read 'Working with commercial version' section  of
      ! ALGLIB Reference Manual in order to find out how to  use  performance-
      ! related features provided by commercial edition of ALGLIB.

    INPUT PARAMETERS:
        S       -   spline 2D builder object
        NLayers -   number of layers in the model:
                    * NLayers>=1 means that up  to  chosen  number  of  bottom
                      layers is fitted
                    * NLayers=0 means that maximum number of layers is  chosen
                      (according to current grid size)
                    * NLayers<=-1 means that up to |NLayers| topmost layers is
                      skipped
                    Recommendations:
                    * good "default" value is 2 layers
                    * you may need  more  layers,  if  your  dataset  is  very
                      irregular and you want to "patch"  large  holes.  For  a
                      grid step H (equal to AreaWidth/GridSize) you may expect
                      that last layer reproduces variations at distance H (and
                      can patch holes that wide); that higher  layers  operate
                      at distances 2*H, 4*H, 8*H and so on.
                    * good value for "bullletproof" mode is  NLayers=0,  which
                      results in complete hierarchy of layers being generated.
        LambdaV -   regularization coefficient, chosen in such a way  that  it
                    penalizes bottom layers (fine details) first.
                    LambdaV>=0, zero value means that no penalty is applied.

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dbuildersetalgofastddm(spline2dbuilder state,
        int nlayers,
        double lambdav,
        xparams _params)
    {
        ap.assert(math.isfinite(lambdav), "Spline2DBuilderSetAlgoFastDDM: LambdaV is not finite value");
        ap.assert((double)(lambdav) >= (double)(0), "Spline2DBuilderSetAlgoFastDDM: LambdaV<0");
        state.solvertype = 3;
        state.nlayers = nlayers;
        state.smoothing = lambdav;
    }


    /*************************************************************************
    This  function  allows  you to choose least squares solver used to perform
    fitting. This function sets solver algorithm to "BlockLLS", which performs
    least squares fitting  with  fast  sparse  direct  solver,  with  optional
    nonsmoothness penalty being applied.

    This solver produces C2-continuous spline.

    Nonlinearity penalty has the following form:

                              [                                            ]
        P() ~ Lambda* integral[ (d2S/dx2)^2 + 2*(d2S/dxdy)^2 + (d2S/dy2)^2 ]dxdy
                              [                                            ]
                      
    here integral is calculated over entire grid, and "~" means "proportional"
    because integral is normalized after calcilation. Extremely  large  values
    of Lambda result in linear fit being performed.

    NOTE: this algorithm is the most robust and controllable one,  but  it  is
          limited by 512x512 grids and (say) up to 1.000.000 points.  However,
          ALGLIB has one more  spline  solver:  FastDDM  algorithm,  which  is
          intended for really large-scale problems (in 10M-100M range). FastDDM
          algorithm also has better parallelism properties.
          
    More information on BlockLLS solver:
    * memory requirements: ~[32*K^3+256*NPoints]  bytes  for  KxK  grid   with
      NPoints-sized dataset
    * serial running time: O(K^4+NPoints)
    * parallelism potential: limited. You may get some sublinear gain when
      working with large grids (K's in 256..512 range)

      ! COMMERCIAL EDITION OF ALGLIB:
      ! 
      ! Commercial Edition of ALGLIB includes following important improvements
      ! of this function:
      ! * high-performance native backend with same C# interface (C# version)
      ! * multithreading support (C++ and C# versions)
      ! * hardware vendor (Intel) implementations of linear algebra primitives
      !   (C++ and C# versions, x86/x64 platform)
      ! 
      ! We recommend you to read 'Working with commercial version' section  of
      ! ALGLIB Reference Manual in order to find out how to  use  performance-
      ! related features provided by commercial edition of ALGLIB.

    INPUT PARAMETERS:
        S       -   spline 2D builder object
        LambdaNS-   non-negative value:
                    * positive value means that some smoothing is applied
                    * zero value means  that  no  smoothing  is  applied,  and
                      corresponding entries of design matrix  are  numerically
                      zero and dropped from consideration.

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dbuildersetalgoblocklls(spline2dbuilder state,
        double lambdans,
        xparams _params)
    {
        ap.assert(math.isfinite(lambdans), "Spline2DBuilderSetAlgoBlockLLS: LambdaNS is not finite value");
        ap.assert((double)(lambdans) >= (double)(0), "Spline2DBuilderSetAlgoBlockLLS: LambdaNS<0");
        state.solvertype = 1;
        state.smoothing = lambdans;
    }


    /*************************************************************************
    This  function  allows  you to choose least squares solver used to perform
    fitting. This function sets solver algorithm to "NaiveLLS".

    IMPORTANT: NaiveLLS is NOT intended to be used in  real  life  code!  This
               algorithm solves problem by generated dense (K^2)x(K^2+NPoints)
               matrix and solves  linear  least  squares  problem  with  dense
               solver.
               
               It is here just  to  test  BlockLLS  against  reference  solver
               (and maybe for someone trying to compare well optimized  solver
               against straightforward approach to the LLS problem).

    More information on naive LLS solver:
    * memory requirements: ~[8*K^4+256*NPoints] bytes for KxK grid.
    * serial running time: O(K^6+NPoints) for KxK grid
    * when compared with BlockLLS,  NaiveLLS  has ~K  larger memory demand and
      ~K^2  larger running time.

    INPUT PARAMETERS:
        S       -   spline 2D builder object
        LambdaNS-   nonsmoothness penalty

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dbuildersetalgonaivells(spline2dbuilder state,
        double lambdans,
        xparams _params)
    {
        ap.assert(math.isfinite(lambdans), "Spline2DBuilderSetAlgoBlockLLS: LambdaNS is not finite value");
        ap.assert((double)(lambdans) >= (double)(0), "Spline2DBuilderSetAlgoBlockLLS: LambdaNS<0");
        state.solvertype = 2;
        state.smoothing = lambdans;
    }


    /*************************************************************************
    This function fits bicubic spline to current dataset, using current  area/
    grid and current LLS solver.

      ! COMMERCIAL EDITION OF ALGLIB:
      ! 
      ! Commercial Edition of ALGLIB includes following important improvements
      ! of this function:
      ! * high-performance native backend with same C# interface (C# version)
      ! * multithreading support (C++ and C# versions)
      ! * hardware vendor (Intel) implementations of linear algebra primitives
      !   (C++ and C# versions, x86/x64 platform)
      ! 
      ! We recommend you to read 'Working with commercial version' section  of
      ! ALGLIB Reference Manual in order to find out how to  use  performance-
      ! related features provided by commercial edition of ALGLIB.

    INPUT PARAMETERS:
        State   -   spline 2D builder object

    OUTPUT PARAMETERS:
        S       -   2D spline, fit result
        Rep     -   fitting report, which provides some additional info  about
                    errors, R2 coefficient and so on.

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dfit(spline2dbuilder state,
        spline2dinterpolant s,
        spline2dfitreport rep,
        xparams _params)
    {
        double xa = 0;
        double xb = 0;
        double ya = 0;
        double yb = 0;
        double xaraw = 0;
        double xbraw = 0;
        double yaraw = 0;
        double ybraw = 0;
        int kx = 0;
        int ky = 0;
        double hx = 0;
        double hy = 0;
        double invhx = 0;
        double invhy = 0;
        int gridexpansion = 0;
        int nzwidth = 0;
        int bfrad = 0;
        int npoints = 0;
        int d = 0;
        int ew = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        double v = 0;
        int k0 = 0;
        int k1 = 0;
        double vx = 0;
        double vy = 0;
        int arows = 0;
        int acopied = 0;
        int basecasex = 0;
        int basecasey = 0;
        double eps = 0;
        double[] xywork = new double[0];
        double[,] vterm = new double[0, 0];
        double[] tmpx = new double[0];
        double[] tmpy = new double[0];
        double[] tmp0 = new double[0];
        double[] tmp1 = new double[0];
        double[] meany = new double[0];
        int[] xyindex = new int[0];
        int[] tmpi = new int[0];
        spline1d.spline1dinterpolant basis1 = new spline1d.spline1dinterpolant();
        sparse.sparsematrix av = new sparse.sparsematrix();
        sparse.sparsematrix ah = new sparse.sparsematrix();
        spline2dxdesignmatrix xdesignmatrix = new spline2dxdesignmatrix();
        double[] z = new double[0];
        spline2dblockllsbuf blockllsbuf = new spline2dblockllsbuf();
        int sfx = 0;
        int sfy = 0;
        int sfxy = 0;
        double tss = 0;
        int dstidx = 0;

        nzwidth = 4;
        bfrad = 2;
        npoints = state.npoints;
        d = state.d;
        ew = 2 + d;

        //
        // Integrity checks
        //
        ap.assert((double)(state.sx) == (double)(1), "Spline2DFit: integrity error");
        ap.assert((double)(state.sy) == (double)(1), "Spline2DFit: integrity error");

        //
        // Determine actual area size and grid step
        //
        // NOTE: initialize vars by zeros in order to avoid spurious
        //       compiler warnings.
        //
        xa = 0;
        xb = 0;
        ya = 0;
        yb = 0;
        if (state.areatype == 0)
        {
            if (npoints > 0)
            {
                xa = state.xy[0];
                xb = state.xy[0];
                ya = state.xy[1];
                yb = state.xy[1];
                for (i = 1; i <= npoints - 1; i++)
                {
                    xa = Math.Min(xa, state.xy[i * ew + 0]);
                    xb = Math.Max(xb, state.xy[i * ew + 0]);
                    ya = Math.Min(ya, state.xy[i * ew + 1]);
                    yb = Math.Max(yb, state.xy[i * ew + 1]);
                }
            }
            else
            {
                xa = -1;
                xb = 1;
                ya = -1;
                yb = 1;
            }
        }
        else
        {
            if (state.areatype == 1)
            {
                xa = state.xa;
                xb = state.xb;
                ya = state.ya;
                yb = state.yb;
            }
            else
            {
                ap.assert(false);
            }
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
        if ((double)(ya) == (double)(yb))
        {
            v = ya;
            if ((double)(v) >= (double)(0))
            {
                ya = v / 2 - 1;
                yb = v * 2 + 1;
            }
            else
            {
                ya = v * 2 - 1;
                yb = v / 2 + 1;
            }
        }
        ap.assert((double)(xa) < (double)(xb), "Spline2DFit: integrity error");
        ap.assert((double)(ya) < (double)(yb), "Spline2DFit: integrity error");
        kx = 0;
        ky = 0;
        if (state.gridtype == 0)
        {
            kx = 4;
            ky = 4;
        }
        else
        {
            if (state.gridtype == 1)
            {
                kx = state.kx;
                ky = state.ky;
            }
            else
            {
                ap.assert(false);
            }
        }
        ap.assert(kx > 0, "Spline2DFit: integrity error");
        ap.assert(ky > 0, "Spline2DFit: integrity error");
        basecasex = -1;
        basecasey = -1;
        if (state.solvertype == 3)
        {

            //
            // Large-scale solver with special requirements to grid size.
            //
            kx = Math.Max(kx, nzwidth);
            ky = Math.Max(ky, nzwidth);
            k = 1;
            while (apserv.imin2(kx, ky, _params) > state.maxcoresize + 1)
            {
                kx = apserv.idivup(kx - 1, 2, _params) + 1;
                ky = apserv.idivup(ky - 1, 2, _params) + 1;
                k = k + 1;
            }
            basecasex = kx - 1;
            k0 = 1;
            while (kx > state.maxcoresize + 1)
            {
                basecasex = apserv.idivup(kx - 1, 2, _params);
                kx = basecasex + 1;
                k0 = k0 + 1;
            }
            while (k0 > 1)
            {
                kx = (kx - 1) * 2 + 1;
                k0 = k0 - 1;
            }
            basecasey = ky - 1;
            k1 = 1;
            while (ky > state.maxcoresize + 1)
            {
                basecasey = apserv.idivup(ky - 1, 2, _params);
                ky = basecasey + 1;
                k1 = k1 + 1;
            }
            while (k1 > 1)
            {
                ky = (ky - 1) * 2 + 1;
                k1 = k1 - 1;
            }
            while (k > 1)
            {
                kx = (kx - 1) * 2 + 1;
                ky = (ky - 1) * 2 + 1;
                k = k - 1;
            }

            //
            // Grid is NOT expanded. We have very strict requirements on
            // grid size, and we do not want to overcomplicate it by
            // playing with grid size in order to add one more degree of
            // freedom. It is not relevant for such large tasks.
            //
            gridexpansion = 0;
        }
        else
        {

            //
            // Medium-scale solvers which are tolerant to grid size.
            //
            kx = Math.Max(kx, nzwidth);
            ky = Math.Max(ky, nzwidth);

            //
            // Grid is expanded by 1 in order to add one more effective degree
            // of freedom to the spline. Having additional nodes outside of the
            // area allows us to emulate changes in the derivative at the bound
            // without having specialized "boundary" version of the basis function.
            //
            if (state.adddegreeoffreedom)
            {
                gridexpansion = 1;
            }
            else
            {
                gridexpansion = 0;
            }
        }
        hx = apserv.coalesce(xb - xa, 1.0, _params) / (kx - 1);
        hy = apserv.coalesce(yb - ya, 1.0, _params) / (ky - 1);
        invhx = 1 / hx;
        invhy = 1 / hy;

        //
        // We determined "raw" grid size. Now perform a grid correction according
        // to current grid expansion size.
        //
        xaraw = xa;
        yaraw = ya;
        xbraw = xb;
        ybraw = yb;
        xa = xa - hx * gridexpansion;
        ya = ya - hy * gridexpansion;
        xb = xb + hx * gridexpansion;
        yb = yb + hy * gridexpansion;
        kx = kx + 2 * gridexpansion;
        ky = ky + 2 * gridexpansion;

        //
        // Create output spline using transformed (unit-scale)
        // coordinates, fill by zero values
        //
        s.d = d;
        s.n = kx;
        s.m = ky;
        s.stype = -3;
        s.hasmissingcells = false;
        sfx = s.n * s.m * d;
        sfy = 2 * s.n * s.m * d;
        sfxy = 3 * s.n * s.m * d;
        s.x = new double[s.n];
        s.y = new double[s.m];
        s.f = new double[4 * s.n * s.m * d];
        for (i = 0; i <= s.n - 1; i++)
        {
            s.x[i] = i;
        }
        for (i = 0; i <= s.m - 1; i++)
        {
            s.y[i] = i;
        }
        for (i = 0; i <= 4 * s.n * s.m * d - 1; i++)
        {
            s.f[i] = 0.0;
        }

        //
        // Create local copy of dataset (only points in the grid are copied;
        // we allow small step out of the grid, by Eps*H, in order to deal
        // with numerical rounding errors).
        //
        // An additional copy of Y-values is created at columns beyond 2+J;
        // it is preserved during all transformations. This copy is used
        // to calculate error-related metrics.
        //
        // Calculate mean(Y), TSS
        //
        meany = new double[d];
        for (j = 0; j <= d - 1; j++)
        {
            meany[j] = 0;
        }
        apserv.rvectorsetlengthatleast(ref xywork, npoints * ew, _params);
        acopied = 0;
        eps = 1.0E-6;
        for (i = 0; i <= npoints - 1; i++)
        {
            vx = state.xy[i * ew + 0];
            vy = state.xy[i * ew + 1];
            if ((((double)(xaraw - eps * hx) <= (double)(vx) && (double)(vx) <= (double)(xbraw + eps * hx)) && (double)(yaraw - eps * hy) <= (double)(vy)) && (double)(vy) <= (double)(ybraw + eps * hy))
            {
                xywork[acopied * ew + 0] = (vx - xa) * invhx;
                xywork[acopied * ew + 1] = (vy - ya) * invhy;
                for (j = 0; j <= d - 1; j++)
                {
                    v = state.xy[i * ew + 2 + j];
                    xywork[acopied * ew + 2 + j] = v;
                    meany[j] = meany[j] + v;
                }
                acopied = acopied + 1;
            }
        }
        npoints = acopied;
        for (j = 0; j <= d - 1; j++)
        {
            meany[j] = meany[j] / apserv.coalesce(npoints, 1, _params);
        }
        tss = 0.0;
        for (i = 0; i <= npoints - 1; i++)
        {
            for (j = 0; j <= d - 1; j++)
            {
                tss = tss + math.sqr(xywork[i * ew + 2 + j] - meany[j]);
            }
        }
        tss = apserv.coalesce(tss, 1.0, _params);

        //
        // Handle prior term.
        // Modify output spline.
        // Quick exit if dataset is empty.
        //
        intfitserv.buildpriorterm1(xywork, npoints, 2, d, state.priorterm, state.priortermval, ref vterm, _params);
        if (npoints == 0)
        {

            //
            // Quick exit
            //
            for (k = 0; k <= s.n * s.m - 1; k++)
            {
                k0 = k % s.n;
                k1 = k / s.n;
                for (j = 0; j <= d - 1; j++)
                {
                    dstidx = d * (k1 * s.n + k0) + j;
                    s.f[dstidx] = s.f[dstidx] + vterm[j, 0] * s.x[k0] + vterm[j, 1] * s.y[k1] + vterm[j, 2];
                    s.f[sfx + dstidx] = s.f[sfx + dstidx] + vterm[j, 0];
                    s.f[sfy + dstidx] = s.f[sfy + dstidx] + vterm[j, 1];
                }
            }
            for (i = 0; i <= s.n - 1; i++)
            {
                s.x[i] = s.x[i] * hx + xa;
            }
            for (i = 0; i <= s.m - 1; i++)
            {
                s.y[i] = s.y[i] * hy + ya;
            }
            for (i = 0; i <= s.n * s.m * d - 1; i++)
            {
                s.f[sfx + i] = s.f[sfx + i] * invhx;
                s.f[sfy + i] = s.f[sfy + i] * invhy;
                s.f[sfxy + i] = s.f[sfxy + i] * invhx * invhy;
            }
            rep.rmserror = 0;
            rep.avgerror = 0;
            rep.maxerror = 0;
            rep.r2 = 1.0;
            return;
        }

        //
        // Build 1D compact basis function
        // Generate design matrix
        //
        tmpx = new double[7];
        tmpy = new double[7];
        tmpx[0] = -3;
        tmpx[1] = -2;
        tmpx[2] = -1;
        tmpx[3] = 0;
        tmpx[4] = 1;
        tmpx[5] = 2;
        tmpx[6] = 3;
        tmpy[0] = 0;
        tmpy[1] = 0;
        tmpy[2] = (double)1 / (double)12;
        tmpy[3] = (double)2 / (double)6;
        tmpy[4] = (double)1 / (double)12;
        tmpy[5] = 0;
        tmpy[6] = 0;
        spline1d.spline1dbuildcubic(tmpx, tmpy, ap.len(tmpx), 2, 0.0, 2, 0.0, basis1, _params);

        //
        // Solve.
        // Update spline.
        //
        if (state.solvertype == 1)
        {

            //
            // BlockLLS
            //
            reorderdatasetandbuildindex(xywork, npoints, d, tmp0, 0, kx, ky, ref xyindex, ref tmpi, _params);
            xdesigngenerate(xywork, xyindex, 0, kx, kx, 0, ky, ky, d, lambdaregblocklls, state.smoothing, basis1, xdesignmatrix, _params);
            blockllsfit(xdesignmatrix, state.lsqrcnt, ref z, rep, tss, blockllsbuf, _params);
            updatesplinetable(z, kx, ky, d, basis1, bfrad, s.f, s.m, s.n, 1, _params);
        }
        else
        {
            if (state.solvertype == 2)
            {

                //
                // NaiveLLS, reference implementation
                //
                generatedesignmatrix(xywork, npoints, d, kx, ky, state.smoothing, lambdaregblocklls, basis1, av, ah, ref arows, _params);
                naivellsfit(av, ah, arows, xywork, kx, ky, npoints, d, state.lsqrcnt, ref z, rep, tss, _params);
                updatesplinetable(z, kx, ky, d, basis1, bfrad, s.f, s.m, s.n, 1, _params);
            }
            else
            {
                if (state.solvertype == 3)
                {

                    //
                    // FastDDM method
                    //
                    ap.assert(basecasex > 0, "Spline2DFit: integrity error");
                    ap.assert(basecasey > 0, "Spline2DFit: integrity error");
                    fastddmfit(xywork, npoints, d, kx, ky, basecasex, basecasey, state.maxcoresize, state.interfacesize, state.nlayers, state.smoothing, state.lsqrcnt, basis1, s, rep, tss, _params);
                }
                else
                {
                    ap.assert(false, "Spline2DFit: integrity error");
                }
            }
        }

        //
        // Append prior term.
        // Transform spline to original coordinates
        //
        for (k = 0; k <= s.n * s.m - 1; k++)
        {
            k0 = k % s.n;
            k1 = k / s.n;
            for (j = 0; j <= d - 1; j++)
            {
                dstidx = d * (k1 * s.n + k0) + j;
                s.f[dstidx] = s.f[dstidx] + vterm[j, 0] * s.x[k0] + vterm[j, 1] * s.y[k1] + vterm[j, 2];
                s.f[sfx + dstidx] = s.f[sfx + dstidx] + vterm[j, 0];
                s.f[sfy + dstidx] = s.f[sfy + dstidx] + vterm[j, 1];
            }
        }
        for (i = 0; i <= s.n - 1; i++)
        {
            s.x[i] = s.x[i] * hx + xa;
        }
        for (i = 0; i <= s.m - 1; i++)
        {
            s.y[i] = s.y[i] * hy + ya;
        }
        for (i = 0; i <= s.n * s.m * d - 1; i++)
        {
            s.f[sfx + i] = s.f[sfx + i] * invhx;
            s.f[sfy + i] = s.f[sfy + i] * invhy;
            s.f[sfxy + i] = s.f[sfxy + i] * invhx * invhy;
        }
    }


    /*************************************************************************
    Serializer: allocation

      -- ALGLIB --
         Copyright 28.02.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dalloc(serializer s,
        spline2dinterpolant spline,
        xparams _params)
    {

        //
        // Which spline 2D format to use - V1 (no missing nodes) or V2 (missing nodes)?
        //
        if (!spline.hasmissingcells)
        {

            //
            // V1 format
            //
            s.alloc_entry();

            //
            // Data
            //
            s.alloc_entry();
            s.alloc_entry();
            s.alloc_entry();
            s.alloc_entry();
            apserv.allocrealarray(s, spline.x, -1, _params);
            apserv.allocrealarray(s, spline.y, -1, _params);
            apserv.allocrealarray(s, spline.f, -1, _params);
        }
        else
        {

            //
            // V2 format
            //
            s.alloc_entry();

            //
            // Data
            //
            s.alloc_entry();
            s.alloc_entry();
            s.alloc_entry();
            s.alloc_entry();
            apserv.allocrealarray(s, spline.x, -1, _params);
            apserv.allocrealarray(s, spline.y, -1, _params);
            apserv.allocrealarray(s, spline.f, -1, _params);
            apserv.allocbooleanarray(s, spline.ismissingnode, -1, _params);
            apserv.allocbooleanarray(s, spline.ismissingcell, -1, _params);
        }
    }


    /*************************************************************************
    Serializer: serialization

      -- ALGLIB --
         Copyright 28.02.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dserialize(serializer s,
        spline2dinterpolant spline,
        xparams _params)
    {

        //
        // Which spline 2D format to use - V1 (no missing nodes) or V2 (missing nodes)?
        //
        if (!spline.hasmissingcells)
        {

            //
            // V1 format
            //
            s.serialize_int(scodes.getspline2dserializationcode(_params));

            //
            // Data
            //
            s.serialize_int(spline.stype);
            s.serialize_int(spline.n);
            s.serialize_int(spline.m);
            s.serialize_int(spline.d);
            apserv.serializerealarray(s, spline.x, -1, _params);
            apserv.serializerealarray(s, spline.y, -1, _params);
            apserv.serializerealarray(s, spline.f, -1, _params);
        }
        else
        {

            //
            // V2 format
            //
            s.serialize_int(scodes.getspline2dwithmissingnodesserializationcode(_params));

            //
            // Data
            //
            s.serialize_int(spline.stype);
            s.serialize_int(spline.n);
            s.serialize_int(spline.m);
            s.serialize_int(spline.d);
            apserv.serializerealarray(s, spline.x, -1, _params);
            apserv.serializerealarray(s, spline.y, -1, _params);
            apserv.serializerealarray(s, spline.f, -1, _params);
            apserv.serializebooleanarray(s, spline.ismissingnode, -1, _params);
            apserv.serializebooleanarray(s, spline.ismissingcell, -1, _params);
        }
    }


    /*************************************************************************
    Serializer: unserialization

      -- ALGLIB --
         Copyright 28.02.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void spline2dunserialize(serializer s,
        spline2dinterpolant spline,
        xparams _params)
    {
        int scode = 0;


        //
        // Header
        //
        scode = s.unserialize_int();
        ap.assert(scode == scodes.getspline2dserializationcode(_params) || scode == scodes.getspline2dwithmissingnodesserializationcode(_params), "Spline2DUnserialize: stream header corrupted");

        //
        // Data
        //
        if (scode == scodes.getspline2dserializationcode(_params))
        {
            spline.stype = s.unserialize_int();
            spline.n = s.unserialize_int();
            spline.m = s.unserialize_int();
            spline.d = s.unserialize_int();
            apserv.unserializerealarray(s, ref spline.x, _params);
            apserv.unserializerealarray(s, ref spline.y, _params);
            apserv.unserializerealarray(s, ref spline.f, _params);
            spline.hasmissingcells = false;
        }
        else
        {
            spline.stype = s.unserialize_int();
            spline.n = s.unserialize_int();
            spline.m = s.unserialize_int();
            spline.d = s.unserialize_int();
            apserv.unserializerealarray(s, ref spline.x, _params);
            apserv.unserializerealarray(s, ref spline.y, _params);
            apserv.unserializerealarray(s, ref spline.f, _params);
            apserv.unserializebooleanarray(s, ref spline.ismissingnode, _params);
            apserv.unserializebooleanarray(s, ref spline.ismissingcell, _params);
            spline.hasmissingcells = true;
        }
    }


    /*************************************************************************
    Internal subroutine.
    Calculation of the first derivatives and the cross-derivative.
    *************************************************************************/
    private static void bicubiccalcderivatives(double[,] a,
        double[] x,
        double[] y,
        int m,
        int n,
        ref double[,] dx,
        ref double[,] dy,
        ref double[,] dxy,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double[] xt = new double[0];
        double[] ft = new double[0];
        double s = 0;
        double ds = 0;
        double d2s = 0;
        spline1d.spline1dinterpolant c = new spline1d.spline1dinterpolant();

        dx = new double[0, 0];
        dy = new double[0, 0];
        dxy = new double[0, 0];

        dx = new double[m, n];
        dy = new double[m, n];
        dxy = new double[m, n];

        //
        // dF/dX
        //
        xt = new double[n];
        ft = new double[n];
        for (i = 0; i <= m - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                xt[j] = x[j];
                ft[j] = a[i, j];
            }
            spline1d.spline1dbuildcubic(xt, ft, n, 0, 0.0, 0, 0.0, c, _params);
            for (j = 0; j <= n - 1; j++)
            {
                spline1d.spline1ddiff(c, x[j], ref s, ref ds, ref d2s, _params);
                dx[i, j] = ds;
            }
        }

        //
        // dF/dY
        //
        xt = new double[m];
        ft = new double[m];
        for (j = 0; j <= n - 1; j++)
        {
            for (i = 0; i <= m - 1; i++)
            {
                xt[i] = y[i];
                ft[i] = a[i, j];
            }
            spline1d.spline1dbuildcubic(xt, ft, m, 0, 0.0, 0, 0.0, c, _params);
            for (i = 0; i <= m - 1; i++)
            {
                spline1d.spline1ddiff(c, y[i], ref s, ref ds, ref d2s, _params);
                dy[i, j] = ds;
            }
        }

        //
        // d2F/dXdY
        //
        xt = new double[n];
        ft = new double[n];
        for (i = 0; i <= m - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                xt[j] = x[j];
                ft[j] = dy[i, j];
            }
            spline1d.spline1dbuildcubic(xt, ft, n, 0, 0.0, 0, 0.0, c, _params);
            for (j = 0; j <= n - 1; j++)
            {
                spline1d.spline1ddiff(c, x[j], ref s, ref ds, ref d2s, _params);
                dxy[i, j] = ds;
            }
        }
    }


    /*************************************************************************
    Internal subroutine.

    Calculation of the first derivatives and the cross-derivative  subject  to
    a missing values map in IsMissingNode[].

    The missing values map should be normalized, i.e. any isolated point  that
    is not part of some non-missing cell should be marked as missing too.
    *************************************************************************/
    private static void bicubiccalcderivativesmissing(double[,] a,
        bool[] ismissingnode,
        double[] x,
        double[] y,
        int m,
        int n,
        ref double[,] dx,
        ref double[,] dy,
        ref double[,] dxy,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k1 = 0;
        int k2 = 0;
        double[] xt = new double[0];
        double[] ft = new double[0];
        double[] t = new double[0];
        bool[] b = new bool[0];
        spline1d.spline1dinterpolant c = new spline1d.spline1dinterpolant();
        double s = 0;
        double ds = 0;
        double d2s = 0;
        int i_ = 0;
        int i1_ = 0;

        dx = new double[0, 0];
        dy = new double[0, 0];
        dxy = new double[0, 0];

        ap.assert(m >= 2, "BicubicCalcDerivativesMissing: internal error (M<2)");
        ap.assert(n >= 2, "BicubicCalcDerivativesMissing: internal error (N<2)");

        //
        // Allocate DX/DY/DXY and make initial fill by zeros
        //
        ablasf.rsetallocm(m, n, 0.0, ref dx, _params);
        ablasf.rsetallocm(m, n, 0.0, ref dy, _params);
        ablasf.rsetallocm(m, n, 0.0, ref dxy, _params);

        //
        // dF/dX
        //
        ablasf.ballocv(n, ref b, _params);
        xt = new double[n];
        ft = new double[n];
        t = new double[n];
        for (i = 0; i <= m - 1; i++)
        {
            k1 = -1;
            k2 = -1;
            ablasf.rcopyrv(n, a, i, t, _params);
            for (j = 0; j <= n - 1; j++)
            {
                b[j] = ismissingnode[i * n + j];
            }
            while (scanfornonmissingsegment(b, n, ref k1, ref k2, _params))
            {
                i1_ = (k1) - (0);
                for (i_ = 0; i_ <= k2 - k1; i_++)
                {
                    xt[i_] = x[i_ + i1_];
                }
                i1_ = (k1) - (0);
                for (i_ = 0; i_ <= k2 - k1; i_++)
                {
                    ft[i_] = t[i_ + i1_];
                }
                spline1d.spline1dbuildcubic(xt, ft, k2 - k1 + 1, 0, 0.0, 0, 0.0, c, _params);
                for (j = 0; j <= k2 - k1; j++)
                {
                    spline1d.spline1ddiff(c, x[k1 + j], ref s, ref ds, ref d2s, _params);
                    dx[i, k1 + j] = ds;
                }
            }
        }

        //
        // dF/dY
        //
        ablasf.ballocv(m, ref b, _params);
        xt = new double[m];
        ft = new double[m];
        t = new double[m];
        for (j = 0; j <= n - 1; j++)
        {
            k1 = -1;
            k2 = -1;
            for (i_ = 0; i_ <= m - 1; i_++)
            {
                t[i_] = a[i_, j];
            }
            for (i = 0; i <= m - 1; i++)
            {
                b[i] = ismissingnode[i * n + j];
            }
            while (scanfornonmissingsegment(b, m, ref k1, ref k2, _params))
            {
                i1_ = (k1) - (0);
                for (i_ = 0; i_ <= k2 - k1; i_++)
                {
                    xt[i_] = y[i_ + i1_];
                }
                i1_ = (k1) - (0);
                for (i_ = 0; i_ <= k2 - k1; i_++)
                {
                    ft[i_] = t[i_ + i1_];
                }
                spline1d.spline1dbuildcubic(xt, ft, k2 - k1 + 1, 0, 0.0, 0, 0.0, c, _params);
                for (i = 0; i <= k2 - k1; i++)
                {
                    spline1d.spline1ddiff(c, y[k1 + i], ref s, ref ds, ref d2s, _params);
                    dy[k1 + i, j] = ds;
                }
            }
        }

        //
        // d2F/dXdY
        //
        ablasf.ballocv(n, ref b, _params);
        xt = new double[n];
        ft = new double[n];
        t = new double[n];
        for (i = 0; i <= m - 1; i++)
        {
            k1 = -1;
            k2 = -1;
            for (i_ = 0; i_ <= n - 1; i_++)
            {
                t[i_] = dy[i, i_];
            }
            for (j = 0; j <= n - 1; j++)
            {
                b[j] = ismissingnode[i * n + j];
            }
            while (scanfornonmissingsegment(b, n, ref k1, ref k2, _params))
            {
                i1_ = (k1) - (0);
                for (i_ = 0; i_ <= k2 - k1; i_++)
                {
                    xt[i_] = x[i_ + i1_];
                }
                i1_ = (k1) - (0);
                for (i_ = 0; i_ <= k2 - k1; i_++)
                {
                    ft[i_] = t[i_ + i1_];
                }
                spline1d.spline1dbuildcubic(xt, ft, k2 - k1 + 1, 0, 0.0, 0, 0.0, c, _params);
                for (j = 0; j <= k2 - k1; j++)
                {
                    spline1d.spline1ddiff(c, x[k1 + j], ref s, ref ds, ref d2s, _params);
                    dxy[i, k1 + j] = ds;
                }
            }
        }
    }


    /*************************************************************************
    Internal subroutine.
    Scans array IsMissing[] for segment containing non-missing values.

    On the first call I1=I2=-1.
    Ater return from subsequent call either
    * 0<=I1<I2<N, B[I1:I2] is non-missing; result is True
    * I1=I2=N; result is False
    *************************************************************************/
    private static bool scanfornonmissingsegment(bool[] ismissing,
        int n,
        ref int i1,
        ref int i2,
        xparams _params)
    {
        bool result = new bool();
        int i = 0;

        ap.assert(n >= 2, "ScanForNonmissingSegment: internal error (N<2)");
        ap.assert(i1 <= i2, "ScanForNonmissingSegment: internal error (I1>I2)");
        result = false;

        //
        // Initial call: prepare and pass
        //
        if (i1 < 0 || i2 < 0)
        {
            i1 = -1;
            i2 = -1;
        }

        //
        // Scan for the next segment
        //
        if (i1 < n && i2 < n)
        {

            //
            // scan for the segment's start
            //
            i = i2 + 1;
            i1 = n;
            i2 = n;
            result = false;
            while (true)
            {
                if (i >= n)
                {
                    return result;
                }
                if (!ismissing[i])
                {
                    i1 = i;
                    break;
                }
                i = i + 1;
            }

            //
            // Scan for segment's end
            //
            while (true)
            {
                if (i >= n)
                {
                    i2 = n - 1;
                    break;
                }
                if (ismissing[i])
                {
                    i2 = i - 1;
                    break;
                }
                i = i + 1;
            }
            ap.assert(i2 > i1, "ScanForFiniteSegment: internal error (segment is too short)");
            result = true;
        }
        return result;
    }


    /*************************************************************************
    This function generates design matrix for the problem (in fact, two design
    matrices are generated: "vertical" one and transposed (horizontal) one.

    INPUT PARAMETERS:
        XY          -   array[NPoints*(2+D)]; dataset after scaling  in  such
                        way that grid step is equal to 1.0 in both dimensions.
        NPoints     -   dataset size, NPoints>=1
        KX, KY      -   grid size, KX,KY>=4
        Smoothing   -   nonlinearity penalty coefficient, >=0
        LambdaReg   -   regularization coefficient, >=0
        Basis1      -   basis spline, expected to be non-zero only at [-2,+2]
        AV, AH      -   possibly preallocated buffers

    OUTPUT PARAMETERS:
        AV          -   sparse matrix[ARows,KX*KY]; design matrix
        AH          -   transpose of AV
        ARows       -   number of rows in design matrix
        
      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void generatedesignmatrix(double[] xy,
        int npoints,
        int d,
        int kx,
        int ky,
        double smoothing,
        double lambdareg,
        spline1d.spline1dinterpolant basis1,
        sparse.sparsematrix av,
        sparse.sparsematrix ah,
        ref int arows,
        xparams _params)
    {
        int nzwidth = 0;
        int nzshift = 0;
        int ew = 0;
        int i = 0;
        int j0 = 0;
        int j1 = 0;
        int k0 = 0;
        int k1 = 0;
        int dstidx = 0;
        double v = 0;
        double v0 = 0;
        double v1 = 0;
        double v2 = 0;
        double w0 = 0;
        double w1 = 0;
        double w2 = 0;
        int[] crx = new int[0];
        int[] cry = new int[0];
        int[] nrs = new int[0];
        double[,] d2x = new double[0, 0];
        double[,] d2y = new double[0, 0];
        double[,] dxy = new double[0, 0];

        arows = 0;

        nzwidth = 4;
        nzshift = 1;
        ap.assert(npoints > 0, "Spline2DFit: integrity check failed");
        ap.assert(kx >= nzwidth, "Spline2DFit: integrity check failed");
        ap.assert(ky >= nzwidth, "Spline2DFit: integrity check failed");
        ew = 2 + d;

        //
        // Determine canonical rectangle for every point. Every point of the dataset is
        // influenced by at most NZWidth*NZWidth basis functions, which form NZWidth*NZWidth
        // canonical rectangle.
        //
        // Thus, we have (KX-NZWidth+1)*(KY-NZWidth+1) overlapping canonical rectangles.
        // Assigning every point to its rectangle simplifies creation of sparse basis
        // matrix at the next steps.
        //
        crx = new int[npoints];
        cry = new int[npoints];
        for (i = 0; i <= npoints - 1; i++)
        {
            crx[i] = apserv.iboundval((int)Math.Floor(xy[i * ew + 0]) - nzshift, 0, kx - nzwidth, _params);
            cry[i] = apserv.iboundval((int)Math.Floor(xy[i * ew + 1]) - nzshift, 0, ky - nzwidth, _params);
        }

        //
        // Create vertical and horizontal design matrices 
        //
        arows = npoints + kx * ky;
        if ((double)(smoothing) != (double)(0.0))
        {
            ap.assert((double)(smoothing) > (double)(0.0), "Spline2DFit: integrity check failed");
            arows = arows + 3 * (kx - 2) * (ky - 2);
        }
        nrs = new int[arows];
        dstidx = 0;
        for (i = 0; i <= npoints - 1; i++)
        {
            nrs[dstidx + i] = nzwidth * nzwidth;
        }
        dstidx = dstidx + npoints;
        for (i = 0; i <= kx * ky - 1; i++)
        {
            nrs[dstidx + i] = 1;
        }
        dstidx = dstidx + kx * ky;
        if ((double)(smoothing) != (double)(0.0))
        {
            for (i = 0; i <= 3 * (kx - 2) * (ky - 2) - 1; i++)
            {
                nrs[dstidx + i] = 3 * 3;
            }
            dstidx = dstidx + 3 * (kx - 2) * (ky - 2);
        }
        ap.assert(dstidx == arows, "Spline2DFit: integrity check failed");
        sparse.sparsecreatecrs(arows, kx * ky, nrs, av, _params);
        dstidx = 0;
        for (i = 0; i <= npoints - 1; i++)
        {
            for (j1 = 0; j1 <= nzwidth - 1; j1++)
            {
                for (j0 = 0; j0 <= nzwidth - 1; j0++)
                {
                    v0 = spline1d.spline1dcalc(basis1, xy[i * ew + 0] - (crx[i] + j0), _params);
                    v1 = spline1d.spline1dcalc(basis1, xy[i * ew + 1] - (cry[i] + j1), _params);
                    sparse.sparseset(av, dstidx + i, (cry[i] + j1) * kx + (crx[i] + j0), v0 * v1, _params);
                }
            }
        }
        dstidx = dstidx + npoints;
        for (i = 0; i <= kx * ky - 1; i++)
        {
            sparse.sparseset(av, dstidx + i, i, lambdareg, _params);
        }
        dstidx = dstidx + kx * ky;
        if ((double)(smoothing) != (double)(0.0))
        {

            //
            // Smoothing is applied. Because all grid nodes are same,
            // we apply same smoothing kernel, which is calculated only
            // once at the beginning of design matrix generation.
            //
            d2x = new double[3, 3];
            d2y = new double[3, 3];
            dxy = new double[3, 3];
            for (j1 = 0; j1 <= 2; j1++)
            {
                for (j0 = 0; j0 <= 2; j0++)
                {
                    d2x[j0, j1] = 0.0;
                    d2y[j0, j1] = 0.0;
                    dxy[j0, j1] = 0.0;
                }
            }
            for (k1 = 0; k1 <= 2; k1++)
            {
                for (k0 = 0; k0 <= 2; k0++)
                {
                    spline1d.spline1ddiff(basis1, -(k0 - 1), ref v0, ref v1, ref v2, _params);
                    spline1d.spline1ddiff(basis1, -(k1 - 1), ref w0, ref w1, ref w2, _params);
                    d2x[k0, k1] = d2x[k0, k1] + v2 * w0;
                    d2y[k0, k1] = d2y[k0, k1] + w2 * v0;
                    dxy[k0, k1] = dxy[k0, k1] + v1 * w1;
                }
            }

            //
            // Now, kernel is ready - apply it to all inner nodes of the grid.
            //
            for (j1 = 1; j1 <= ky - 2; j1++)
            {
                for (j0 = 1; j0 <= kx - 2; j0++)
                {

                    //
                    // d2F/dx2 term
                    //
                    v = smoothing;
                    for (k1 = -1; k1 <= 1; k1++)
                    {
                        for (k0 = -1; k0 <= 1; k0++)
                        {
                            sparse.sparseset(av, dstidx, (j1 + k1) * kx + (j0 + k0), v * d2x[1 + k0, 1 + k1], _params);
                        }
                    }
                    dstidx = dstidx + 1;

                    //
                    // d2F/dy2 term
                    //
                    v = smoothing;
                    for (k1 = -1; k1 <= 1; k1++)
                    {
                        for (k0 = -1; k0 <= 1; k0++)
                        {
                            sparse.sparseset(av, dstidx, (j1 + k1) * kx + (j0 + k0), v * d2y[1 + k0, 1 + k1], _params);
                        }
                    }
                    dstidx = dstidx + 1;

                    //
                    // 2*d2F/dxdy term
                    //
                    v = Math.Sqrt(2) * smoothing;
                    for (k1 = -1; k1 <= 1; k1++)
                    {
                        for (k0 = -1; k0 <= 1; k0++)
                        {
                            sparse.sparseset(av, dstidx, (j1 + k1) * kx + (j0 + k0), v * dxy[1 + k0, 1 + k1], _params);
                        }
                    }
                    dstidx = dstidx + 1;
                }
            }
        }
        ap.assert(dstidx == arows, "Spline2DFit: integrity check failed");
        sparse.sparsecopy(av, ah, _params);
        sparse.sparsetransposecrs(ah, _params);
    }


    /*************************************************************************
    This function updates table of spline values/derivatives using coefficients
    for a layer of basis functions.
        
      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void updatesplinetable(double[] z,
        int kx,
        int ky,
        int d,
        spline1d.spline1dinterpolant basis1,
        int bfrad,
        double[] ftbl,
        int m,
        int n,
        int scalexy,
        xparams _params)
    {
        int k = 0;
        int k0 = 0;
        int k1 = 0;
        int j = 0;
        int j0 = 0;
        int j1 = 0;
        int j0a = 0;
        int j0b = 0;
        int j1a = 0;
        int j1b = 0;
        double v = 0;
        double v0 = 0;
        double v1 = 0;
        double v01 = 0;
        double v11 = 0;
        double rdummy = 0;
        int dstidx = 0;
        int sfx = 0;
        int sfy = 0;
        int sfxy = 0;
        double invscalexy = 0;

        ap.assert(n == (kx - 1) * scalexy + 1, "Spline2DFit.UpdateSplineTable: integrity check failed");
        ap.assert(m == (ky - 1) * scalexy + 1, "Spline2DFit.UpdateSplineTable: integrity check failed");
        invscalexy = (double)1 / (double)scalexy;
        sfx = n * m * d;
        sfy = 2 * n * m * d;
        sfxy = 3 * n * m * d;
        for (k = 0; k <= kx * ky - 1; k++)
        {
            k0 = k % kx;
            k1 = k / kx;
            j0a = apserv.iboundval(k0 * scalexy - (bfrad * scalexy - 1), 0, n - 1, _params);
            j0b = apserv.iboundval(k0 * scalexy + (bfrad * scalexy - 1), 0, n - 1, _params);
            j1a = apserv.iboundval(k1 * scalexy - (bfrad * scalexy - 1), 0, m - 1, _params);
            j1b = apserv.iboundval(k1 * scalexy + (bfrad * scalexy - 1), 0, m - 1, _params);
            for (j1 = j1a; j1 <= j1b; j1++)
            {
                spline1d.spline1ddiff(basis1, (j1 - k1 * scalexy) * invscalexy, ref v1, ref v11, ref rdummy, _params);
                v11 = v11 * invscalexy;
                for (j0 = j0a; j0 <= j0b; j0++)
                {
                    spline1d.spline1ddiff(basis1, (j0 - k0 * scalexy) * invscalexy, ref v0, ref v01, ref rdummy, _params);
                    v01 = v01 * invscalexy;
                    for (j = 0; j <= d - 1; j++)
                    {
                        dstidx = d * (j1 * n + j0) + j;
                        v = z[j * kx * ky + k];
                        ftbl[dstidx] = ftbl[dstidx] + v0 * v1 * v;
                        ftbl[sfx + dstidx] = ftbl[sfx + dstidx] + v01 * v1 * v;
                        ftbl[sfy + dstidx] = ftbl[sfy + dstidx] + v0 * v11 * v;
                        ftbl[sfxy + dstidx] = ftbl[sfxy + dstidx] + v01 * v11 * v;
                    }
                }
            }
        }
    }


    /*************************************************************************
    This function performs fitting with FastDDM solver.
    Internal function, never use it directly.

    INPUT PARAMETERS:
        XY          -   array[NPoints*(2+D)], dataset; destroyed in process
        KX, KY      -   grid size
        TileSize    -   tile size
        InterfaceSize-  interface size
        NPoints     -   points count
        D           -   number of components in vector-valued spline, D>=1
        LSQRCnt     -   number of iterations, non-zero:
                        * LSQRCnt>0 means that specified amount of  preconditioned
                          LSQR  iterations  will  be  performed  to solve problem;
                          usually  we  need  2..5  its.  Recommended option - best
                          convergence and stability/quality.
                        * LSQRCnt<0 means that instead of LSQR  we  use  iterative
                          refinement on normal equations. Again, 2..5 its is enough.
        Basis1      -   basis spline, expected to be non-zero only at [-2,+2]
        Z           -   possibly preallocated buffer for solution
        Residuals   -   possibly preallocated buffer for residuals at dataset points
        Rep         -   report structure; fields which are not set by this function
                        are left intact
        TSS         -   total sum of squares; used to calculate R2
        

    OUTPUT PARAMETERS:
        XY          -   destroyed in process
        Z           -   array[KX*KY*D], filled by solution; KX*KY coefficients
                        corresponding to each of D dimensions are stored contiguously.
        Rep         -   following fields are set:
                        * Rep.RMSError
                        * Rep.AvgError
                        * Rep.MaxError
                        * Rep.R2

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void fastddmfit(double[] xy,
        int npoints,
        int d,
        int kx,
        int ky,
        int basecasex,
        int basecasey,
        int maxcoresize,
        int interfacesize,
        int nlayers,
        double smoothing,
        int lsqrcnt,
        spline1d.spline1dinterpolant basis1,
        spline2dinterpolant spline,
        spline2dfitreport rep,
        double tss,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int nzwidth = 0;
        int xew = 0;
        int ntotallayers = 0;
        int scaleidx = 0;
        int scalexy = 0;
        double invscalexy = 0;
        int kxcur = 0;
        int kycur = 0;
        int tilescount0 = 0;
        int tilescount1 = 0;
        double v = 0;
        double rss = 0;
        double[] yraw = new double[0];
        int[] xyindex = new int[0];
        double[] tmp0 = new double[0];
        int[] bufi = new int[0];
        spline2dfastddmbuf seed = new spline2dfastddmbuf();
        smp.shared_pool pool = new smp.shared_pool();
        spline2dxdesignmatrix xdesignmatrix = new spline2dxdesignmatrix();
        spline2dblockllsbuf blockllsbuf = new spline2dblockllsbuf();
        spline2dfitreport dummyrep = new spline2dfitreport();


        //
        // Dataset metrics and integrity checks
        //
        nzwidth = 4;
        xew = 2 + d;
        ap.assert(maxcoresize >= 2, "Spline2DFit: integrity check failed");
        ap.assert(interfacesize >= 1, "Spline2DFit: integrity check failed");
        ap.assert(kx >= nzwidth, "Spline2DFit: integrity check failed");
        ap.assert(ky >= nzwidth, "Spline2DFit: integrity check failed");

        //
        // Verify consistency of the grid size (KX,KY) with basecase sizes.
        // Determine full number of layers.
        //
        ap.assert(basecasex <= maxcoresize, "Spline2DFit: integrity error");
        ap.assert(basecasey <= maxcoresize, "Spline2DFit: integrity error");
        ntotallayers = 1;
        scalexy = 1;
        kxcur = kx;
        kycur = ky;
        while (kxcur > basecasex + 1 && kycur > basecasey + 1)
        {
            ap.assert(kxcur % 2 == 1, "Spline2DFit: integrity error");
            ap.assert(kycur % 2 == 1, "Spline2DFit: integrity error");
            kxcur = (kxcur - 1) / 2 + 1;
            kycur = (kycur - 1) / 2 + 1;
            scalexy = scalexy * 2;
            apserv.inc(ref ntotallayers, _params);
        }
        invscalexy = (double)1 / (double)scalexy;
        ap.assert((kxcur <= maxcoresize + 1 && kxcur == basecasex + 1) || kxcur % basecasex == 1, "Spline2DFit: integrity error");
        ap.assert((kycur <= maxcoresize + 1 && kycur == basecasey + 1) || kycur % basecasey == 1, "Spline2DFit: integrity error");
        ap.assert(kxcur == basecasex + 1 || kycur == basecasey + 1, "Spline2DFit: integrity error");

        //
        // Initial scaling of dataset.
        // Store original target values to YRaw.
        //
        apserv.rvectorsetlengthatleast(ref yraw, npoints * d, _params);
        for (i = 0; i <= npoints - 1; i++)
        {
            xy[xew * i + 0] = xy[xew * i + 0] * invscalexy;
            xy[xew * i + 1] = xy[xew * i + 1] * invscalexy;
            for (j = 0; j <= d - 1; j++)
            {
                yraw[i * d + j] = xy[xew * i + 2 + j];
            }
        }
        kxcur = (kx - 1) / scalexy + 1;
        kycur = (ky - 1) / scalexy + 1;

        //
        // Build initial dataset index; area is divided into (KXCur-1)*(KYCur-1)
        // cells, with contiguous storage of points in the same cell.
        // Iterate over different scales
        //
        smp.ae_shared_pool_set_seed(pool, seed);
        reorderdatasetandbuildindex(xy, npoints, d, yraw, d, kxcur, kycur, ref xyindex, ref bufi, _params);
        for (scaleidx = ntotallayers - 1; scaleidx >= 0; scaleidx--)
        {
            if ((nlayers > 0 && scaleidx < nlayers) || (nlayers <= 0 && scaleidx < apserv.imax2(ntotallayers + nlayers, 1, _params)))
            {

                //
                // Fit current layer
                //
                ap.assert(kxcur % basecasex == 1, "Spline2DFit: integrity error");
                ap.assert(kycur % basecasey == 1, "Spline2DFit: integrity error");
                tilescount0 = kxcur / basecasex;
                tilescount1 = kycur / basecasey;
                fastddmfitlayer(xy, d, scalexy, xyindex, basecasex, 0, tilescount0, tilescount0, basecasey, 0, tilescount1, tilescount1, maxcoresize, interfacesize, lsqrcnt, lambdaregfastddm + smoothing * Math.Pow(lambdadecay, scaleidx), basis1, pool, spline, _params);

                //
                // Compute residuals and update XY
                //
                computeresidualsfromscratch(xy, yraw, npoints, d, scalexy, spline, _params);
            }

            //
            // Move to the next level
            //
            if (scaleidx != 0)
            {

                //
                // Transform dataset (multply everything by 2.0) and refine grid.
                //
                kxcur = 2 * kxcur - 1;
                kycur = 2 * kycur - 1;
                scalexy = scalexy / 2;
                invscalexy = (double)1 / (double)scalexy;
                rescaledatasetandrefineindex(xy, npoints, d, yraw, d, kxcur, kycur, ref xyindex, ref bufi, _params);

                //
                // Clear temporaries from previous round.
                //
                // We have to do it because upper layer of the multilevel spline
                // needs more memory then subsequent layers, and we want to free
                // this memory as soon as possible.
                //
                smp.ae_shared_pool_clear_recycled(pool);
            }
        }

        //
        // Post-check
        //
        ap.assert(kxcur == kx, "Spline2DFit: integrity check failed");
        ap.assert(kycur == ky, "Spline2DFit: integrity check failed");
        ap.assert(scalexy == 1, "Spline2DFit: integrity check failed");

        //
        // Report
        //
        rep.rmserror = 0;
        rep.avgerror = 0;
        rep.maxerror = 0;
        rss = 0.0;
        for (i = 0; i <= npoints - 1; i++)
        {
            for (j = 0; j <= d - 1; j++)
            {
                v = xy[i * xew + 2 + j];
                rss = rss + v * v;
                rep.rmserror = rep.rmserror + math.sqr(v);
                rep.avgerror = rep.avgerror + Math.Abs(v);
                rep.maxerror = Math.Max(rep.maxerror, Math.Abs(v));
            }
        }
        rep.rmserror = Math.Sqrt(rep.rmserror / apserv.coalesce(npoints * d, 1.0, _params));
        rep.avgerror = rep.avgerror / apserv.coalesce(npoints * d, 1.0, _params);
        rep.r2 = 1.0 - rss / apserv.coalesce(tss, 1.0, _params);
    }


    /*************************************************************************
    Recursive fitting function for FastDDM algorithm.

    Works with KX*KY grid, with KX=BasecaseX*TilesCountX+1 and KY=BasecaseY*TilesCountY+1,
    which is partitioned into TilesCountX*TilesCountY tiles, each having size
    BasecaseX*BasecaseY.

    This function processes tiles in range [TileX0,TileX1)x[TileY0,TileY1) and
    recursively divides this range until we move down to single tile, which
    is processed with BlockLLS solver.

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void fastddmfitlayer(double[] xy,
        int d,
        int scalexy,
        int[] xyindex,
        int basecasex,
        int tilex0,
        int tilex1,
        int tilescountx,
        int basecasey,
        int tiley0,
        int tiley1,
        int tilescounty,
        int maxcoresize,
        int interfacesize,
        int lsqrcnt,
        double lambdareg,
        spline1d.spline1dinterpolant basis1,
        smp.shared_pool pool,
        spline2dinterpolant spline,
        xparams _params)
    {
        int kx = 0;
        int ky = 0;
        int i = 0;
        int j = 0;
        int j0 = 0;
        int j1 = 0;
        int bfrad = 0;
        int xa = 0;
        int xb = 0;
        int ya = 0;
        int yb = 0;
        int tile0 = 0;
        int tile1 = 0;
        int tilesize0 = 0;
        int tilesize1 = 0;
        int sfx = 0;
        int sfy = 0;
        int sfxy = 0;
        double dummytss = 0;
        double invscalexy = 0;
        int cnt0 = 0;
        int cnt1 = 0;
        int offs = 0;
        double vs = 0;
        double vsx = 0;
        double vsy = 0;
        double vsxx = 0;
        double vsxy = 0;
        double vsyy = 0;
        spline2dfastddmbuf buf = null;


        //
        // Dataset metrics and fast integrity checks;
        // no code with side effects is allowed before parallel split.
        //
        bfrad = 2;
        invscalexy = (double)1 / (double)scalexy;
        kx = basecasex * tilescountx + 1;
        ky = basecasey * tilescounty + 1;

        //
        // Parallelism; because this function is intended for
        // large-scale problems, we always try to:
        // * invoke parallel execution mode
        // * activate spawn support
        //
        if (_trypexec_fastddmfitlayer(xy, d, scalexy, xyindex, basecasex, tilex0, tilex1, tilescountx, basecasey, tiley0, tiley1, tilescounty, maxcoresize, interfacesize, lsqrcnt, lambdareg, basis1, pool, spline, _params))
        {
            return;
        }
        if (apserv.imax2(tiley1 - tiley0, tilex1 - tilex0, _params) >= 2)
        {
            if (tiley1 - tiley0 > tilex1 - tilex0)
            {

                //
                // Split problem in Y dimension
                //
                // NOTE: recursive calls to FastDDMFitLayer() compute
                //       residuals in the inner cells defined by XYIndex[],
                //       but we still have to compute residuals for cells
                //       BETWEEN two recursive subdivisions of the task.
                //
                apserv.tiledsplit(tiley1 - tiley0, 1, ref j0, ref j1, _params);
                fastddmfitlayer(xy, d, scalexy, xyindex, basecasex, tilex0, tilex1, tilescountx, basecasey, tiley0, tiley0 + j0, tilescounty, maxcoresize, interfacesize, lsqrcnt, lambdareg, basis1, pool, spline, _params);
                fastddmfitlayer(xy, d, scalexy, xyindex, basecasex, tilex0, tilex1, tilescountx, basecasey, tiley0 + j0, tiley1, tilescounty, maxcoresize, interfacesize, lsqrcnt, lambdareg, basis1, pool, spline, _params);
            }
            else
            {

                //
                // Split problem in X dimension
                //
                // NOTE: recursive calls to FastDDMFitLayer() compute
                //       residuals in the inner cells defined by XYIndex[],
                //       but we still have to compute residuals for cells
                //       BETWEEN two recursive subdivisions of the task.
                //
                apserv.tiledsplit(tilex1 - tilex0, 1, ref j0, ref j1, _params);
                fastddmfitlayer(xy, d, scalexy, xyindex, basecasex, tilex0, tilex0 + j0, tilescountx, basecasey, tiley0, tiley1, tilescounty, maxcoresize, interfacesize, lsqrcnt, lambdareg, basis1, pool, spline, _params);
                fastddmfitlayer(xy, d, scalexy, xyindex, basecasex, tilex0 + j0, tilex1, tilescountx, basecasey, tiley0, tiley1, tilescounty, maxcoresize, interfacesize, lsqrcnt, lambdareg, basis1, pool, spline, _params);
            }
            return;
        }
        ap.assert(tiley0 == tiley1 - 1, "Spline2DFit.FastDDMFitLayer: integrity check failed");
        ap.assert(tilex0 == tilex1 - 1, "Spline2DFit.FastDDMFitLayer: integrity check failed");
        tile1 = tiley0;
        tile0 = tilex0;

        //
        // Retrieve temporaries
        //
        smp.ae_shared_pool_retrieve(pool, ref buf);

        //
        // Analyze dataset
        //
        xa = apserv.iboundval(tile0 * basecasex - interfacesize, 0, kx, _params);
        xb = apserv.iboundval((tile0 + 1) * basecasex + interfacesize, 0, kx, _params);
        ya = apserv.iboundval(tile1 * basecasey - interfacesize, 0, ky, _params);
        yb = apserv.iboundval((tile1 + 1) * basecasey + interfacesize, 0, ky, _params);
        tilesize0 = xb - xa;
        tilesize1 = yb - ya;

        //
        // Solve current chunk with BlockLLS
        //
        dummytss = 1.0;
        xdesigngenerate(xy, xyindex, xa, xb, kx, ya, yb, ky, d, lambdareg, 0.0, basis1, buf.xdesignmatrix, _params);
        blockllsfit(buf.xdesignmatrix, lsqrcnt, ref buf.tmpz, buf.dummyrep, dummytss, buf.blockllsbuf, _params);
        buf.localmodel.d = d;
        buf.localmodel.m = tilesize1;
        buf.localmodel.n = tilesize0;
        buf.localmodel.stype = -3;
        buf.localmodel.hasmissingcells = false;
        apserv.rvectorsetlengthatleast(ref buf.localmodel.x, tilesize0, _params);
        apserv.rvectorsetlengthatleast(ref buf.localmodel.y, tilesize1, _params);
        apserv.rvectorsetlengthatleast(ref buf.localmodel.f, tilesize0 * tilesize1 * d * 4, _params);
        for (i = 0; i <= tilesize0 - 1; i++)
        {
            buf.localmodel.x[i] = xa + i;
        }
        for (i = 0; i <= tilesize1 - 1; i++)
        {
            buf.localmodel.y[i] = ya + i;
        }
        for (i = 0; i <= tilesize0 * tilesize1 * d * 4 - 1; i++)
        {
            buf.localmodel.f[i] = 0.0;
        }
        updatesplinetable(buf.tmpz, tilesize0, tilesize1, d, basis1, bfrad, buf.localmodel.f, tilesize1, tilesize0, 1, _params);

        //
        // Transform local spline to original coordinates
        //
        sfx = buf.localmodel.n * buf.localmodel.m * d;
        sfy = 2 * buf.localmodel.n * buf.localmodel.m * d;
        sfxy = 3 * buf.localmodel.n * buf.localmodel.m * d;
        for (i = 0; i <= tilesize0 - 1; i++)
        {
            buf.localmodel.x[i] = buf.localmodel.x[i] * scalexy;
        }
        for (i = 0; i <= tilesize1 - 1; i++)
        {
            buf.localmodel.y[i] = buf.localmodel.y[i] * scalexy;
        }
        for (i = 0; i <= tilesize0 * tilesize1 * d - 1; i++)
        {
            buf.localmodel.f[sfx + i] = buf.localmodel.f[sfx + i] * invscalexy;
            buf.localmodel.f[sfy + i] = buf.localmodel.f[sfy + i] * invscalexy;
            buf.localmodel.f[sfxy + i] = buf.localmodel.f[sfxy + i] * (invscalexy * invscalexy);
        }

        //
        // Output results; for inner and topmost/leftmost tiles we output only BasecaseX*BasecaseY
        // inner elements; for rightmost/bottom ones we also output one column/row of the interface
        // part.
        //
        // Such complexity is explained by the fact that area size (by design) is not evenly divisible
        // by the tile size; it is divisible with remainder=1, and we expect that interface size is
        // at least 1, so we can fill the missing rightmost/bottom elements of Z by the interface
        // values.
        //
        ap.assert(interfacesize >= 1, "Spline2DFit: integrity check failed");
        sfx = spline.n * spline.m * d;
        sfy = 2 * spline.n * spline.m * d;
        sfxy = 3 * spline.n * spline.m * d;
        cnt0 = basecasex * scalexy;
        cnt1 = basecasey * scalexy;
        if (tile0 == tilescountx - 1)
        {
            apserv.inc(ref cnt0, _params);
        }
        if (tile1 == tilescounty - 1)
        {
            apserv.inc(ref cnt1, _params);
        }
        offs = d * (spline.n * tile1 * basecasey * scalexy + tile0 * basecasex * scalexy);
        for (j1 = 0; j1 <= cnt1 - 1; j1++)
        {
            for (j0 = 0; j0 <= cnt0 - 1; j0++)
            {
                for (j = 0; j <= d - 1; j++)
                {
                    spline2ddiff2vi(buf.localmodel, tile0 * basecasex * scalexy + j0, tile1 * basecasey * scalexy + j1, j, ref vs, ref vsx, ref vsy, ref vsxx, ref vsxy, ref vsyy, _params);
                    spline.f[offs + d * (spline.n * j1 + j0) + j] = spline.f[offs + d * (spline.n * j1 + j0) + j] + vs;
                    spline.f[sfx + offs + d * (spline.n * j1 + j0) + j] = spline.f[sfx + offs + d * (spline.n * j1 + j0) + j] + vsx;
                    spline.f[sfy + offs + d * (spline.n * j1 + j0) + j] = spline.f[sfy + offs + d * (spline.n * j1 + j0) + j] + vsy;
                    spline.f[sfxy + offs + d * (spline.n * j1 + j0) + j] = spline.f[sfxy + offs + d * (spline.n * j1 + j0) + j] + vsxy;
                }
            }
        }

        //
        // Recycle temporaries
        //
        smp.ae_shared_pool_recycle(pool, ref buf);
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_fastddmfitlayer(double[] xy,
        int d,
        int scalexy,
        int[] xyindex,
        int basecasex,
        int tilex0,
        int tilex1,
        int tilescountx,
        int basecasey,
        int tiley0,
        int tiley1,
        int tilescounty,
        int maxcoresize,
        int interfacesize,
        int lsqrcnt,
        double lambdareg,
        spline1d.spline1dinterpolant basis1,
        smp.shared_pool pool,
        spline2dinterpolant spline, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    This function performs fitting with  BlockLLS solver.  Internal  function,
    never use it directly.

    IMPORTANT: performance  and  memory  requirements  of  this  function  are
               asymmetric w.r.t. KX and KY: it has
               * O(KY*KX^2) memory requirements
               * O(KY*KX^3) running time
               Thus, if you have large KY and small KX,  simple  transposition
               of your dataset may give you great speedup.

    INPUT PARAMETERS:
        AV      -   sparse matrix, [ARows,KX*KY] in size.  "Vertical"  version
                    of design matrix, rows [0,NPoints) contain values of basis
                    functions at dataset  points.  Other  rows  are  used  for
                    nonlinearity penalty and other stuff like that.
        AH      -   transpose(AV), "horizontal" version of AV
        ARows   -   rows count
        XY      -   array[NPoints*(2+D)], dataset
        KX, KY  -   grid size
        NPoints -   points count
        D       -   number of components in vector-valued spline, D>=1
        LSQRCnt -   number of iterations, non-zero:
                    * LSQRCnt>0 means that specified amount of  preconditioned
                      LSQR  iterations  will  be  performed  to solve problem;
                      usually  we  need  2..5  its.  Recommended option - best
                      convergence and stability/quality.
                    * LSQRCnt<0 means that instead of LSQR  we  use  iterative
                      refinement on normal equations. Again, 2..5 its is enough.
        Z       -   possibly preallocated buffer for solution
        Rep     -   report structure; fields which are not set by this function
                    are left intact
        TSS     -   total sum of squares; used to calculate R2
        

    OUTPUT PARAMETERS:
        XY      -   destroyed in process
        Z       -   array[KX*KY*D], filled by solution; KX*KY coefficients
                    corresponding to each of D dimensions are stored contiguously.
        Rep         -   following fields are set:
                        * Rep.RMSError
                        * Rep.AvgError
                        * Rep.MaxError
                        * Rep.R2

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void blockllsfit(spline2dxdesignmatrix xdesign,
        int lsqrcnt,
        ref double[] z,
        spline2dfitreport rep,
        double tss,
        spline2dblockllsbuf buf,
        xparams _params)
    {
        int blockbandwidth = 0;
        int d = 0;
        int i = 0;
        int j = 0;
        double lambdachol = 0;
        apserv.sreal mxata = new apserv.sreal();
        double v = 0;
        int celloffset = 0;
        int i0 = 0;
        int i1 = 0;
        double rss = 0;
        int arows = 0;
        int bw2 = 0;
        int kx = 0;
        int ky = 0;

        ap.assert(xdesign.blockwidth == 4, "Spline2DFit: integrity check failed");
        blockbandwidth = 3;
        d = xdesign.d;
        arows = xdesign.nrows;
        kx = xdesign.kx;
        ky = xdesign.ky;
        bw2 = xdesign.blockwidth * xdesign.blockwidth;

        //
        // Initial values for Z/Residuals
        //
        apserv.rvectorsetlengthatleast(ref z, kx * ky * d, _params);
        for (i = 0; i <= kx * ky * d - 1; i++)
        {
            z[i] = 0;
        }

        //
        // Create and factorize design matrix. Add regularizer if
        // factorization failed (happens sometimes with zero
        // smoothing and sparsely populated datasets).
        //
        // The algorithm below is refactoring of NaiveLLS algorithm,
        // which uses sparsity properties and compressed block storage.
        //
        // Problem sparsity pattern results in block-band-diagonal
        // matrix (block matrix with limited bandwidth, equal to 3
        // for bicubic splines). Thus, we have KY*KY blocks, each
        // of them is KX*KX in size. Design matrix is stored in
        // large NROWS*KX matrix, with NROWS=(BlockBandwidth+1)*KY*KX.
        //
        // We use adaptation of block skyline storage format, with
        // TOWERSIZE*KX skyline bands (towers) stored sequentially;
        // here TOWERSIZE=(BlockBandwidth+1)*KX. So, we have KY
        // "towers", stored one below other, in BlockATA matrix.
        // Every "tower" is a sequence of BlockBandwidth+1 cells,
        // each of them being KX*KX in size.
        //
        lambdachol = cholreg;
        apserv.rmatrixsetlengthatleast(ref buf.blockata, (blockbandwidth + 1) * ky * kx, kx, _params);
        while (true)
        {

            //
            // Parallel generation of squared design matrix.
            //
            xdesignblockata(xdesign, buf.blockata, ref mxata.val, _params);

            //
            // Regularization
            //
            v = apserv.coalesce(mxata.val, 1.0, _params) * lambdachol;
            for (i1 = 0; i1 <= ky - 1; i1++)
            {
                celloffset = getcelloffset(kx, ky, blockbandwidth, i1, i1, _params);
                for (i0 = 0; i0 <= kx - 1; i0++)
                {
                    buf.blockata[celloffset + i0, i0] = buf.blockata[celloffset + i0, i0] + v;
                }
            }

            //
            // Try Cholesky factorization.
            //
            if (!blockllscholesky(buf.blockata, kx, ky, ref buf.trsmbuf2, ref buf.cholbuf2, ref buf.cholbuf1, _params))
            {

                //
                // Factorization failed, increase regularizer and repeat
                //
                lambdachol = apserv.coalesce(10 * lambdachol, 1.0E-12, _params);
                continue;
            }
            break;
        }

        //
        // Solve
        //
        rss = 0.0;
        rep.rmserror = 0;
        rep.avgerror = 0;
        rep.maxerror = 0;
        ap.assert(lsqrcnt > 0, "Spline2DFit: integrity failure");
        apserv.rvectorsetlengthatleast(ref buf.tmp0, arows, _params);
        apserv.rvectorsetlengthatleast(ref buf.tmp1, kx * ky, _params);
        linlsqr.linlsqrcreatebuf(arows, kx * ky, buf.solver, _params);
        for (j = 0; j <= d - 1; j++)
        {

            //
            // Preconditioned LSQR:
            //
            // use Cholesky factor U of squared design matrix A'*A to
            // transform min|A*x-b| to min|[A*inv(U)]*y-b| with y=U*x.
            //
            // Preconditioned problem is solved with LSQR solver, which
            // gives superior results than normal equations.
            //
            for (i = 0; i <= arows - 1; i++)
            {
                if (i < xdesign.npoints)
                {
                    buf.tmp0[i] = xdesign.vals[i, bw2 + j];
                }
                else
                {
                    buf.tmp0[i] = 0.0;
                }
            }
            linlsqr.linlsqrrestart(buf.solver, _params);
            linlsqr.linlsqrsetb(buf.solver, buf.tmp0, _params);
            linlsqr.linlsqrsetcond(buf.solver, 1.0E-14, 1.0E-14, lsqrcnt, _params);
            while (linlsqr.linlsqriteration(buf.solver, _params))
            {
                if (buf.solver.needmv)
                {

                    //
                    // Use Cholesky factorization of the system matrix
                    // as preconditioner: solve TRSV(U,Solver.X)
                    //
                    for (i = 0; i <= kx * ky - 1; i++)
                    {
                        buf.tmp1[i] = buf.solver.x[i];
                    }
                    blockllstrsv(buf.blockata, kx, ky, false, buf.tmp1, _params);

                    //
                    // After preconditioning is done, multiply by A
                    //
                    xdesignmv(xdesign, buf.tmp1, ref buf.solver.mv, _params);
                }
                if (buf.solver.needmtv)
                {

                    //
                    // Multiply by design matrix A
                    //
                    xdesignmtv(xdesign, buf.solver.x, ref buf.solver.mtv, _params);

                    //
                    // Multiply by preconditioner: solve TRSV(U',A*Solver.X)
                    //
                    blockllstrsv(buf.blockata, kx, ky, true, buf.solver.mtv, _params);
                }
            }

            //
            // Get results and post-multiply by preconditioner to get
            // original variables.
            //
            linlsqr.linlsqrresults(buf.solver, ref buf.tmp1, buf.solverrep, _params);
            blockllstrsv(buf.blockata, kx, ky, false, buf.tmp1, _params);
            for (i = 0; i <= kx * ky - 1; i++)
            {
                z[kx * ky * j + i] = buf.tmp1[i];
            }

            //
            // Calculate model values
            //
            xdesignmv(xdesign, buf.tmp1, ref buf.tmp0, _params);
            for (i = 0; i <= xdesign.npoints - 1; i++)
            {
                v = xdesign.vals[i, bw2 + j] - buf.tmp0[i];
                rss = rss + v * v;
                rep.rmserror = rep.rmserror + math.sqr(v);
                rep.avgerror = rep.avgerror + Math.Abs(v);
                rep.maxerror = Math.Max(rep.maxerror, Math.Abs(v));
            }
        }
        rep.rmserror = Math.Sqrt(rep.rmserror / apserv.coalesce(xdesign.npoints * d, 1.0, _params));
        rep.avgerror = rep.avgerror / apserv.coalesce(xdesign.npoints * d, 1.0, _params);
        rep.r2 = 1.0 - rss / apserv.coalesce(tss, 1.0, _params);
    }


    /*************************************************************************
    This function performs fitting with  NaiveLLS solver.  Internal  function,
    never use it directly.

    INPUT PARAMETERS:
        AV      -   sparse matrix, [ARows,KX*KY] in size.  "Vertical"  version
                    of design matrix, rows [0,NPoints] contain values of basis
                    functions at dataset  points.  Other  rows  are  used  for
                    nonlinearity penalty and other stuff like that.
        AH      -   transpose(AV), "horizontal" version of AV
        ARows   -   rows count
        XY      -   array[NPoints*(2+D)], dataset
        KX, KY  -   grid size
        NPoints -   points count
        D       -   number of components in vector-valued spline, D>=1
        LSQRCnt -   number of iterations, non-zero:
                    * LSQRCnt>0 means that specified amount of  preconditioned
                      LSQR  iterations  will  be  performed  to solve problem;
                      usually  we  need  2..5  its.  Recommended option - best
                      convergence and stability/quality.
                    * LSQRCnt<0 means that instead of LSQR  we  use  iterative
                      refinement on normal equations. Again, 2..5 its is enough.
        Z       -   possibly preallocated buffer for solution
        Rep     -   report structure; fields which are not set by this function
                    are left intact
        TSS     -   total sum of squares; used to calculate R2
        

    OUTPUT PARAMETERS:
        XY      -   destroyed in process
        Z       -   array[KX*KY*D], filled by solution; KX*KY coefficients
                    corresponding to each of D dimensions are stored contiguously.
        Rep     -   following fields are set:
                        * Rep.RMSError
                        * Rep.AvgError
                        * Rep.MaxError
                        * Rep.R2

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void naivellsfit(sparse.sparsematrix av,
        sparse.sparsematrix ah,
        int arows,
        double[] xy,
        int kx,
        int ky,
        int npoints,
        int d,
        int lsqrcnt,
        ref double[] z,
        spline2dfitreport rep,
        double tss,
        xparams _params)
    {
        int ew = 0;
        int i = 0;
        int j = 0;
        int i0 = 0;
        int i1 = 0;
        int j0 = 0;
        int j1 = 0;
        double v = 0;
        int blockbandwidth = 0;
        double lambdareg = 0;
        int srci = 0;
        int srcj = 0;
        int idxi = 0;
        int idxj = 0;
        int endi = 0;
        int endj = 0;
        int rfsidx = 0;
        double[,] ata = new double[0, 0];
        double[] tmp0 = new double[0];
        double[] tmp1 = new double[0];
        double mxata = 0;
        linlsqr.linlsqrstate solver = new linlsqr.linlsqrstate();
        linlsqr.linlsqrreport solverrep = new linlsqr.linlsqrreport();
        double rss = 0;

        blockbandwidth = 3;
        ew = 2 + d;

        //
        // Initial values for Z/Residuals
        //
        apserv.rvectorsetlengthatleast(ref z, kx * ky * d, _params);
        for (i = 0; i <= kx * ky * d - 1; i++)
        {
            z[i] = 0;
        }

        //
        // Create and factorize design matrix.
        //
        // Add regularizer if factorization failed (happens sometimes
        // with zero smoothing and sparsely populated datasets).
        //
        lambdareg = cholreg;
        apserv.rmatrixsetlengthatleast(ref ata, kx * ky, kx * ky, _params);
        while (true)
        {
            mxata = 0.0;
            for (i = 0; i <= kx * ky - 1; i++)
            {
                for (j = i; j <= kx * ky - 1; j++)
                {

                    //
                    // Initialize by zero
                    //
                    ata[i, j] = 0;

                    //
                    // Determine grid nodes corresponding to I and J;
                    // skip if too far away
                    //
                    i0 = i % kx;
                    i1 = i / kx;
                    j0 = j % kx;
                    j1 = j / kx;
                    if (Math.Abs(i0 - j0) > blockbandwidth || Math.Abs(i1 - j1) > blockbandwidth)
                    {
                        continue;
                    }

                    //
                    // Nodes are close enough, calculate product of columns I and J of A.
                    //
                    v = 0;
                    srci = ah.ridx[i];
                    srcj = ah.ridx[j];
                    endi = ah.ridx[i + 1];
                    endj = ah.ridx[j + 1];
                    while (true)
                    {
                        if (srci >= endi || srcj >= endj)
                        {
                            break;
                        }
                        idxi = ah.idx[srci];
                        idxj = ah.idx[srcj];
                        if (idxi == idxj)
                        {
                            v = v + ah.vals[srci] * ah.vals[srcj];
                            srci = srci + 1;
                            srcj = srcj + 1;
                            continue;
                        }
                        if (idxi < idxj)
                        {
                            srci = srci + 1;
                        }
                        else
                        {
                            srcj = srcj + 1;
                        }
                    }
                    ata[i, j] = v;
                    mxata = Math.Max(mxata, Math.Abs(v));
                }
            }
            v = apserv.coalesce(mxata, 1.0, _params) * lambdareg;
            for (i = 0; i <= kx * ky - 1; i++)
            {
                ata[i, i] = ata[i, i] + v;
            }
            if (trfac.spdmatrixcholesky(ata, kx * ky, true, _params))
            {

                //
                // Success!
                //
                break;
            }

            //
            // Factorization failed, increase regularizer and repeat
            //
            lambdareg = apserv.coalesce(10 * lambdareg, 1.0E-12, _params);
        }

        //
        // Solve
        //
        // NOTE: we expect that Z is zero-filled, and we treat it
        //       like initial approximation to solution.
        //
        apserv.rvectorsetlengthatleast(ref tmp0, arows, _params);
        apserv.rvectorsetlengthatleast(ref tmp1, kx * ky, _params);
        if (lsqrcnt > 0)
        {
            linlsqr.linlsqrcreate(arows, kx * ky, solver, _params);
        }
        for (j = 0; j <= d - 1; j++)
        {
            ap.assert(lsqrcnt != 0, "Spline2DFit: integrity failure");
            if (lsqrcnt > 0)
            {

                //
                // Preconditioned LSQR:
                //
                // use Cholesky factor U of squared design matrix A'*A to
                // transform min|A*x-b| to min|[A*inv(U)]*y-b| with y=U*x.
                //
                // Preconditioned problem is solved with LSQR solver, which
                // gives superior results than normal equations.
                //
                linlsqr.linlsqrcreate(arows, kx * ky, solver, _params);
                for (i = 0; i <= arows - 1; i++)
                {
                    if (i < npoints)
                    {
                        tmp0[i] = xy[i * ew + 2 + j];
                    }
                    else
                    {
                        tmp0[i] = 0.0;
                    }
                }
                linlsqr.linlsqrsetb(solver, tmp0, _params);
                linlsqr.linlsqrsetcond(solver, 1.0E-14, 1.0E-14, lsqrcnt, _params);
                while (linlsqr.linlsqriteration(solver, _params))
                {
                    if (solver.needmv)
                    {

                        //
                        // Use Cholesky factorization of the system matrix
                        // as preconditioner: solve TRSV(U,Solver.X)
                        //
                        for (i = 0; i <= kx * ky - 1; i++)
                        {
                            tmp1[i] = solver.x[i];
                        }
                        ablas.rmatrixtrsv(kx * ky, ata, 0, 0, true, false, 0, tmp1, 0, _params);

                        //
                        // After preconditioning is done, multiply by A
                        //
                        sparse.sparsemv(av, tmp1, ref solver.mv, _params);
                    }
                    if (solver.needmtv)
                    {

                        //
                        // Multiply by design matrix A
                        //
                        sparse.sparsemv(ah, solver.x, ref solver.mtv, _params);

                        //
                        // Multiply by preconditioner: solve TRSV(U',A*Solver.X)
                        //
                        ablas.rmatrixtrsv(kx * ky, ata, 0, 0, true, false, 1, solver.mtv, 0, _params);
                    }
                }
                linlsqr.linlsqrresults(solver, ref tmp1, solverrep, _params);
                ablas.rmatrixtrsv(kx * ky, ata, 0, 0, true, false, 0, tmp1, 0, _params);
                for (i = 0; i <= kx * ky - 1; i++)
                {
                    z[kx * ky * j + i] = tmp1[i];
                }

                //
                // Calculate model values
                //
                sparse.sparsemv(av, tmp1, ref tmp0, _params);
                for (i = 0; i <= npoints - 1; i++)
                {
                    xy[i * ew + 2 + j] = xy[i * ew + 2 + j] - tmp0[i];
                }
            }
            else
            {

                //
                // Iterative refinement, inferior to LSQR
                //
                // For each dimension D:
                // * fetch current estimate for solution from Z to Tmp1
                // * calculate residual r for current estimate, store in Tmp0
                // * calculate product of residual and design matrix A'*r, store it in Tmp1
                // * Cholesky solver
                // * update current estimate
                //
                for (rfsidx = 1; rfsidx <= -lsqrcnt; rfsidx++)
                {
                    for (i = 0; i <= kx * ky - 1; i++)
                    {
                        tmp1[i] = z[kx * ky * j + i];
                    }
                    sparse.sparsemv(av, tmp1, ref tmp0, _params);
                    for (i = 0; i <= arows - 1; i++)
                    {
                        if (i < npoints)
                        {
                            v = xy[i * ew + 2 + j];
                        }
                        else
                        {
                            v = 0;
                        }
                        tmp0[i] = v - tmp0[i];
                    }
                    sparse.sparsemv(ah, tmp0, ref tmp1, _params);
                    ablas.rmatrixtrsv(kx * ky, ata, 0, 0, true, false, 1, tmp1, 0, _params);
                    ablas.rmatrixtrsv(kx * ky, ata, 0, 0, true, false, 0, tmp1, 0, _params);
                    for (i = 0; i <= kx * ky - 1; i++)
                    {
                        z[kx * ky * j + i] = z[kx * ky * j + i] + tmp1[i];
                    }
                }

                //
                // Calculate model values
                //
                for (i = 0; i <= kx * ky - 1; i++)
                {
                    tmp1[i] = z[kx * ky * j + i];
                }
                sparse.sparsemv(av, tmp1, ref tmp0, _params);
                for (i = 0; i <= npoints - 1; i++)
                {
                    xy[i * ew + 2 + j] = xy[i * ew + 2 + j] - tmp0[i];
                }
            }
        }

        //
        // Generate report
        //
        rep.rmserror = 0;
        rep.avgerror = 0;
        rep.maxerror = 0;
        rss = 0.0;
        for (i = 0; i <= npoints - 1; i++)
        {
            for (j = 0; j <= d - 1; j++)
            {
                v = xy[i * ew + 2 + j];
                rss = rss + v * v;
                rep.rmserror = rep.rmserror + math.sqr(v);
                rep.avgerror = rep.avgerror + Math.Abs(v);
                rep.maxerror = Math.Max(rep.maxerror, Math.Abs(v));
            }
        }
        rep.rmserror = Math.Sqrt(rep.rmserror / apserv.coalesce(npoints * d, 1.0, _params));
        rep.avgerror = rep.avgerror / apserv.coalesce(npoints * d, 1.0, _params);
        rep.r2 = 1.0 - rss / apserv.coalesce(tss, 1.0, _params);
    }


    /*************************************************************************
    This  is  convenience  function  for band block storage format; it returns
    offset of KX*KX-sized block (I,J) in a compressed 2D array.

    For specific offset=OFFSET,
    block (I,J) will be stored in entries BlockMatrix[OFFSET:OFFSET+KX-1,0:KX-1]

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    private static int getcelloffset(int kx,
        int ky,
        int blockbandwidth,
        int i,
        int j,
        xparams _params)
    {
        int result = 0;

        ap.assert(i >= 0 && i < ky, "Spline2DFit: GetCellOffset() integrity error");
        ap.assert(j >= 0 && j < ky, "Spline2DFit: GetCellOffset() integrity error");
        ap.assert(j >= i && j <= i + blockbandwidth, "Spline2DFit: GetCellOffset() integrity error");
        result = j * (blockbandwidth + 1) * kx;
        result = result + (blockbandwidth - (j - i)) * kx;
        return result;
    }


    /*************************************************************************
    This  is  convenience  function  for band block storage format; it  copies
    cell (I,J) from compressed format to uncompressed general matrix, at desired
    position.

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void copycellto(int kx,
        int ky,
        int blockbandwidth,
        double[,] blockata,
        int i,
        int j,
        double[,] dst,
        int dst0,
        int dst1,
        xparams _params)
    {
        int celloffset = 0;
        int idx0 = 0;
        int idx1 = 0;

        celloffset = getcelloffset(kx, ky, blockbandwidth, i, j, _params);
        for (idx0 = 0; idx0 <= kx - 1; idx0++)
        {
            for (idx1 = 0; idx1 <= kx - 1; idx1++)
            {
                dst[dst0 + idx0, dst1 + idx1] = blockata[celloffset + idx0, idx1];
            }
        }
    }


    /*************************************************************************
    This  is  convenience  function  for band block storage format; it
    truncates all elements of  cell (I,J) which are less than Eps in magnitude.

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void flushtozerocell(int kx,
        int ky,
        int blockbandwidth,
        double[,] blockata,
        int i,
        int j,
        double eps,
        xparams _params)
    {
        int celloffset = 0;
        int idx0 = 0;
        int idx1 = 0;
        double eps2 = 0;
        double v = 0;

        celloffset = getcelloffset(kx, ky, blockbandwidth, i, j, _params);
        eps2 = eps * eps;
        for (idx0 = 0; idx0 <= kx - 1; idx0++)
        {
            for (idx1 = 0; idx1 <= kx - 1; idx1++)
            {
                v = blockata[celloffset + idx0, idx1];
                if (v * v < eps2)
                {
                    blockata[celloffset + idx0, idx1] = 0;
                }
            }
        }
    }


    /*************************************************************************
    This function generates squared design matrix stored in block band format.

    We use adaptation of block skyline storage format, with
    TOWERSIZE*KX skyline bands (towers) stored sequentially;
    here TOWERSIZE=(BlockBandwidth+1)*KX. So, we have KY
    "towers", stored one below other, in BlockATA matrix.
    Every "tower" is a sequence of BlockBandwidth+1 cells,
    each of them being KX*KX in size.

    INPUT PARAMETERS:
        AH      -   sparse matrix, [KX*KY,ARows] in size. "Horizontal" version
                    of design matrix, cols [0,NPoints] contain values of basis
                    functions at dataset  points.  Other  cols  are  used  for
                    nonlinearity penalty and other stuff like that.
        KY0, KY1-   subset of output matrix bands to process; on entry it MUST
                    be set to 0 and KY respectively.
        KX, KY  -   grid size
        BlockATA-   array[KY*(BlockBandwidth+1)*KX,KX],  preallocated  storage
                    for output matrix in compressed block band format
        MXATA   -   on entry MUST be zero

    OUTPUT PARAMETERS:
        BlockATA-   AH*AH', stored in compressed block band format

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void blockllsgenerateata(sparse.sparsematrix ah,
        int ky0,
        int ky1,
        int kx,
        int ky,
        double[,] blockata,
        apserv.sreal mxata,
        xparams _params)
    {
        int blockbandwidth = 0;
        double avgrowlen = 0;
        double cellcost = 0;
        double totalcost = 0;
        apserv.sreal tmpmxata = new apserv.sreal();
        int i = 0;
        int j = 0;
        int i0 = 0;
        int i1 = 0;
        int j0 = 0;
        int j1 = 0;
        int celloffset = 0;
        double v = 0;
        int srci = 0;
        int srcj = 0;
        int idxi = 0;
        int idxj = 0;
        int endi = 0;
        int endj = 0;

        ap.assert((double)(mxata.val) >= (double)(0), "BlockLLSGenerateATA: integrity check failed");
        blockbandwidth = 3;

        //
        // Determine problem cost, perform recursive subdivision
        // (with optional parallelization)
        //
        avgrowlen = (double)ah.ridx[kx * ky] / (double)(kx * ky);
        cellcost = apserv.rmul3(kx, 1 + 2 * blockbandwidth, avgrowlen, _params);
        totalcost = apserv.rmul3(ky1 - ky0, 1 + 2 * blockbandwidth, cellcost, _params);
        if (ky1 - ky0 >= 2 && (double)(totalcost) > (double)(apserv.smpactivationlevel(_params)))
        {
            if (_trypexec_blockllsgenerateata(ah, ky0, ky1, kx, ky, blockata, mxata, _params))
            {
                return;
            }
        }
        if (ky1 - ky0 >= 2)
        {

            //
            // Split X: X*A = (X1 X2)^T*A
            //
            j = (ky1 - ky0) / 2;
            blockllsgenerateata(ah, ky0, ky0 + j, kx, ky, blockata, tmpmxata, _params);
            blockllsgenerateata(ah, ky0 + j, ky1, kx, ky, blockata, mxata, _params);
            mxata.val = Math.Max(mxata.val, tmpmxata.val);
            return;
        }

        //
        // Splitting in Y-dimension is done, fill I1-th "tower"
        //
        ap.assert(ky1 == ky0 + 1, "BlockLLSGenerateATA: integrity check failed");
        i1 = ky0;
        for (j1 = i1; j1 <= Math.Min(ky - 1, i1 + blockbandwidth); j1++)
        {
            celloffset = getcelloffset(kx, ky, blockbandwidth, i1, j1, _params);

            //
            // Clear cell (I1,J1)
            //
            for (i0 = 0; i0 <= kx - 1; i0++)
            {
                for (j0 = 0; j0 <= kx - 1; j0++)
                {
                    blockata[celloffset + i0, j0] = 0.0;
                }
            }

            //
            // Initialize cell internals
            //
            for (i0 = 0; i0 <= kx - 1; i0++)
            {
                for (j0 = 0; j0 <= kx - 1; j0++)
                {
                    if (Math.Abs(i0 - j0) <= blockbandwidth)
                    {

                        //
                        // Nodes are close enough, calculate product of columns I and J of A.
                        //
                        v = 0;
                        i = i1 * kx + i0;
                        j = j1 * kx + j0;
                        srci = ah.ridx[i];
                        srcj = ah.ridx[j];
                        endi = ah.ridx[i + 1];
                        endj = ah.ridx[j + 1];
                        while (true)
                        {
                            if (srci >= endi || srcj >= endj)
                            {
                                break;
                            }
                            idxi = ah.idx[srci];
                            idxj = ah.idx[srcj];
                            if (idxi == idxj)
                            {
                                v = v + ah.vals[srci] * ah.vals[srcj];
                                srci = srci + 1;
                                srcj = srcj + 1;
                                continue;
                            }
                            if (idxi < idxj)
                            {
                                srci = srci + 1;
                            }
                            else
                            {
                                srcj = srcj + 1;
                            }
                        }
                        blockata[celloffset + i0, j0] = v;
                        mxata.val = Math.Max(mxata.val, Math.Abs(v));
                    }
                }
            }
        }
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_blockllsgenerateata(sparse.sparsematrix ah,
        int ky0,
        int ky1,
        int kx,
        int ky,
        double[,] blockata,
        apserv.sreal mxata, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    This function performs Cholesky decomposition of squared design matrix
    stored in block band format.

    INPUT PARAMETERS:
        BlockATA        -   array[KY*(BlockBandwidth+1)*KX,KX], matrix in compressed
                            block band format
        KX, KY          -   grid size
        TrsmBuf2,
        CholBuf2,
        CholBuf1        -   buffers; reused by this function on subsequent calls,
                            automatically preallocated on the first call

    OUTPUT PARAMETERS:
        BlockATA-   Cholesky factor, in compressed block band format

    Result:
        True on success, False on Cholesky failure

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    private static bool blockllscholesky(double[,] blockata,
        int kx,
        int ky,
        ref double[,] trsmbuf2,
        ref double[,] cholbuf2,
        ref double[] cholbuf1,
        xparams _params)
    {
        bool result = new bool();
        int blockbandwidth = 0;
        int blockidx = 0;
        int i = 0;
        int j = 0;
        int celloffset = 0;
        int celloffset1 = 0;

        blockbandwidth = 3;
        apserv.rmatrixsetlengthatleast(ref trsmbuf2, (blockbandwidth + 1) * kx, (blockbandwidth + 1) * kx, _params);
        apserv.rmatrixsetlengthatleast(ref cholbuf2, kx, kx, _params);
        apserv.rvectorsetlengthatleast(ref cholbuf1, kx, _params);
        result = true;
        for (blockidx = 0; blockidx <= ky - 1; blockidx++)
        {

            //
            // TRSM for TRAIL*TRAIL block matrix before current cell;
            // here TRAIL=MinInt(BlockIdx,BlockBandwidth).
            //
            for (i = 0; i <= Math.Min(blockidx, blockbandwidth) - 1; i++)
            {
                for (j = i; j <= Math.Min(blockidx, blockbandwidth) - 1; j++)
                {
                    copycellto(kx, ky, blockbandwidth, blockata, Math.Max(blockidx - blockbandwidth, 0) + i, Math.Max(blockidx - blockbandwidth, 0) + j, trsmbuf2, i * kx, j * kx, _params);
                }
            }
            celloffset = getcelloffset(kx, ky, blockbandwidth, Math.Max(blockidx - blockbandwidth, 0), blockidx, _params);
            ablas.rmatrixlefttrsm(Math.Min(blockidx, blockbandwidth) * kx, kx, trsmbuf2, 0, 0, true, false, 1, blockata, celloffset, 0, _params);

            //
            // SYRK for diagonal cell: MaxInt(BlockIdx-BlockBandwidth,0)
            // cells above diagonal one are used for update.
            //
            celloffset = getcelloffset(kx, ky, blockbandwidth, Math.Max(blockidx - blockbandwidth, 0), blockidx, _params);
            celloffset1 = getcelloffset(kx, ky, blockbandwidth, blockidx, blockidx, _params);
            ablas.rmatrixsyrk(kx, Math.Min(blockidx, blockbandwidth) * kx, -1.0, blockata, celloffset, 0, 1, 1.0, blockata, celloffset1, 0, true, _params);

            //
            // Factorize diagonal cell
            //
            celloffset = getcelloffset(kx, ky, blockbandwidth, blockidx, blockidx, _params);
            ablas.rmatrixcopy(kx, kx, blockata, celloffset, 0, cholbuf2, 0, 0, _params);
            if (!trfac.spdmatrixcholeskyrec(cholbuf2, 0, kx, true, ref cholbuf1, _params))
            {
                result = false;
                return result;
            }
            ablas.rmatrixcopy(kx, kx, cholbuf2, 0, 0, blockata, celloffset, 0, _params);

            //
            // PERFORMANCE TWEAK: drop nearly-denormals from last "tower".
            //
            // Sparse matrices like these may produce denormal numbers on
            // sparse datasets, with significant (10x!) performance penalty
            // on Intel chips. In order to avoid it, we manually truncate
            // small enough numbers.
            //
            // We use 1.0E-50 as clipping level (not really denormal, but
            // such small numbers are not actually important anyway).
            //
            for (i = Math.Max(blockidx - blockbandwidth, 0); i <= blockidx; i++)
            {
                flushtozerocell(kx, ky, blockbandwidth, blockata, i, blockidx, 1.0E-50, _params);
            }
        }
        return result;
    }


    /*************************************************************************
    This function performs TRSV on upper triangular Cholesky factor U, solving
    either U*x=b or U'*x=b.

    INPUT PARAMETERS:
        BlockATA        -   array[KY*(BlockBandwidth+1)*KX,KX], matrix U
                            in compressed block band format
        KX, KY          -   grid size
        TransU          -   whether to transpose U or not
        B               -   array[KX*KY], on entry - stores right part B

    OUTPUT PARAMETERS:
        B               -   replaced by X

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void blockllstrsv(double[,] blockata,
        int kx,
        int ky,
        bool transu,
        double[] b,
        xparams _params)
    {
        int blockbandwidth = 0;
        int blockidx = 0;
        int blockidx1 = 0;
        int celloffset = 0;

        blockbandwidth = 3;
        if (!transu)
        {

            //
            // Solve U*x=b
            //
            for (blockidx = ky - 1; blockidx >= 0; blockidx--)
            {
                for (blockidx1 = 1; blockidx1 <= Math.Min(ky - (blockidx + 1), blockbandwidth); blockidx1++)
                {
                    celloffset = getcelloffset(kx, ky, blockbandwidth, blockidx, blockidx + blockidx1, _params);
                    ablas.rmatrixgemv(kx, kx, -1.0, blockata, celloffset, 0, 0, b, (blockidx + blockidx1) * kx, 1.0, b, blockidx * kx, _params);
                }
                celloffset = getcelloffset(kx, ky, blockbandwidth, blockidx, blockidx, _params);
                ablas.rmatrixtrsv(kx, blockata, celloffset, 0, true, false, 0, b, blockidx * kx, _params);
            }
        }
        else
        {

            //
            // Solve U'*x=b
            //
            for (blockidx = 0; blockidx <= ky - 1; blockidx++)
            {
                celloffset = getcelloffset(kx, ky, blockbandwidth, blockidx, blockidx, _params);
                ablas.rmatrixtrsv(kx, blockata, celloffset, 0, true, false, 1, b, blockidx * kx, _params);
                for (blockidx1 = 1; blockidx1 <= Math.Min(ky - (blockidx + 1), blockbandwidth); blockidx1++)
                {
                    celloffset = getcelloffset(kx, ky, blockbandwidth, blockidx, blockidx + blockidx1, _params);
                    ablas.rmatrixgemv(kx, kx, -1.0, blockata, celloffset, 0, 1, b, blockidx * kx, 1.0, b, (blockidx + blockidx1) * kx, _params);
                }
            }
        }
    }


    /*************************************************************************
    This function computes residuals for dataset XY[], using array of original
    values YRaw[], and loads residuals to XY.

    Processing is performed in parallel manner.

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void computeresidualsfromscratch(double[] xy,
        double[] yraw,
        int npoints,
        int d,
        int scalexy,
        spline2dinterpolant spline,
        xparams _params)
    {
        apserv.srealarray seed = new apserv.srealarray();
        smp.shared_pool pool = new smp.shared_pool();
        int chunksize = 0;
        double pointcost = 0;


        //
        // Setting up
        //
        chunksize = 1000;
        pointcost = 100.0;
        if ((double)(npoints * pointcost) > (double)(apserv.smpactivationlevel(_params)))
        {
            if (_trypexec_computeresidualsfromscratch(xy, yraw, npoints, d, scalexy, spline, _params))
            {
                return;
            }
        }
        smp.ae_shared_pool_set_seed(pool, seed);

        //
        // Call compute workhorse
        //
        computeresidualsfromscratchrec(xy, yraw, 0, npoints, chunksize, d, scalexy, spline, pool, _params);
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_computeresidualsfromscratch(double[] xy,
        double[] yraw,
        int npoints,
        int d,
        int scalexy,
        spline2dinterpolant spline, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    Recursive workhorse for ComputeResidualsFromScratch.

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void computeresidualsfromscratchrec(double[] xy,
        double[] yraw,
        int pt0,
        int pt1,
        int chunksize,
        int d,
        int scalexy,
        spline2dinterpolant spline,
        smp.shared_pool pool,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        apserv.srealarray pbuf = null;
        int xew = 0;

        xew = 2 + d;

        //
        // Parallelism
        //
        if (pt1 - pt0 > chunksize)
        {
            apserv.tiledsplit(pt1 - pt0, chunksize, ref i, ref j, _params);
            computeresidualsfromscratchrec(xy, yraw, pt0, pt0 + i, chunksize, d, scalexy, spline, pool, _params);
            computeresidualsfromscratchrec(xy, yraw, pt0 + i, pt1, chunksize, d, scalexy, spline, pool, _params);
            return;
        }

        //
        // Serial execution
        //
        smp.ae_shared_pool_retrieve(pool, ref pbuf);
        for (i = pt0; i <= pt1 - 1; i++)
        {
            spline2dcalcvbuf(spline, xy[i * xew + 0] * scalexy, xy[i * xew + 1] * scalexy, ref pbuf.val, _params);
            for (j = 0; j <= d - 1; j++)
            {
                xy[i * xew + 2 + j] = yraw[i * d + j] - pbuf.val[j];
            }
        }
        smp.ae_shared_pool_recycle(pool, ref pbuf);
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_computeresidualsfromscratchrec(double[] xy,
        double[] yraw,
        int pt0,
        int pt1,
        int chunksize,
        int d,
        int scalexy,
        spline2dinterpolant spline,
        smp.shared_pool pool, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    This function reorders dataset and builds index:
    * it is assumed that all points have X in [0,KX-1], Y in [0,KY-1]
    * area is divided into (KX-1)*(KY-1) cells
    * all points are reordered in such way that points in same cell are stored
      contiguously
    * dataset index, array[(KX-1)*(KY-1)+1], is generated. Points of cell I
      now have indexes XYIndex[I]..XYIndex[I+1]-1;

    INPUT PARAMETERS:
        XY              -   array[NPoints*(2+D)], dataset
        KX, KY, D       -   grid size and dimensionality of the outputs
        Shadow          -   shadow array[NPoints*NS], which is sorted together
                            with XY; if NS=0, it is not referenced at all.
        NS              -   entry width of shadow array
        BufI            -   possibly preallocated temporary buffer; resized if
                            needed.

    OUTPUT PARAMETERS:
        XY              -   reordered
        XYIndex         -   array[(KX-1)*(KY-1)+1], dataset index

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void reorderdatasetandbuildindex(double[] xy,
        int npoints,
        int d,
        double[] shadow,
        int ns,
        int kx,
        int ky,
        ref int[] xyindex,
        ref int[] bufi,
        xparams _params)
    {
        int i = 0;
        int i0 = 0;
        int i1 = 0;
        int entrywidth = 0;


        //
        // Set up
        //
        ap.assert(kx >= 2, "Spline2DFit.ReorderDatasetAndBuildIndex: integrity check failed");
        ap.assert(ky >= 2, "Spline2DFit.ReorderDatasetAndBuildIndex: integrity check failed");
        entrywidth = 2 + d;
        apserv.ivectorsetlengthatleast(ref xyindex, (kx - 1) * (ky - 1) + 1, _params);
        apserv.ivectorsetlengthatleast(ref bufi, npoints, _params);
        for (i = 0; i <= npoints - 1; i++)
        {
            i0 = apserv.iboundval((int)Math.Floor(xy[i * entrywidth + 0]), 0, kx - 2, _params);
            i1 = apserv.iboundval((int)Math.Floor(xy[i * entrywidth + 1]), 0, ky - 2, _params);
            bufi[i] = i1 * (kx - 1) + i0;
        }

        //
        // Reorder
        //
        reorderdatasetandbuildindexrec(xy, d, shadow, ns, bufi, 0, npoints, xyindex, 0, (kx - 1) * (ky - 1), true, _params);
        xyindex[(kx - 1) * (ky - 1)] = npoints;
    }


    /*************************************************************************
    This function multiplies all points in dataset by 2.0 and rebuilds  index,
    given previous index built for KX_prev=(KX-1)/2 and KY_prev=(KY-1)/2

    INPUT PARAMETERS:
        XY              -   array[NPoints*(2+D)], dataset BEFORE scaling
        NPoints, D      -   dataset size and dimensionality of the outputs
        Shadow          -   shadow array[NPoints*NS], which is sorted together
                            with XY; if NS=0, it is not referenced at all.
        NS              -   entry width of shadow array
        KX, KY          -   new grid dimensionality
        XYIndex         -   index built for previous values of KX and KY
        BufI            -   possibly preallocated temporary buffer; resized if
                            needed.

    OUTPUT PARAMETERS:
        XY              -   reordered and multiplied by 2.0
        XYIndex         -   array[(KX-1)*(KY-1)+1], dataset index

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void rescaledatasetandrefineindex(double[] xy,
        int npoints,
        int d,
        double[] shadow,
        int ns,
        int kx,
        int ky,
        ref int[] xyindex,
        ref int[] bufi,
        xparams _params)
    {
        int[] xyindexprev = new int[0];


        //
        // Set up
        //
        ap.assert(kx >= 2, "Spline2DFit.RescaleDataset2AndRefineIndex: integrity check failed");
        ap.assert(ky >= 2, "Spline2DFit.RescaleDataset2AndRefineIndex: integrity check failed");
        ap.assert((kx - 1) % 2 == 0, "Spline2DFit.RescaleDataset2AndRefineIndex: integrity check failed");
        ap.assert((ky - 1) % 2 == 0, "Spline2DFit.RescaleDataset2AndRefineIndex: integrity check failed");
        ap.swap(ref xyindex, ref xyindexprev);
        apserv.ivectorsetlengthatleast(ref xyindex, (kx - 1) * (ky - 1) + 1, _params);
        apserv.ivectorsetlengthatleast(ref bufi, npoints, _params);

        //
        // Refine
        //
        expandindexrows(xy, d, shadow, ns, bufi, 0, npoints, xyindexprev, 0, (ky + 1) / 2 - 1, xyindex, kx, ky, true, _params);
        xyindex[(kx - 1) * (ky - 1)] = npoints;

        //
        // Integrity check
        //
    }


    /*************************************************************************
    Recurrent divide-and-conquer indexing function

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void expandindexrows(double[] xy,
        int d,
        double[] shadow,
        int ns,
        int[] cidx,
        int pt0,
        int pt1,
        int[] xyindexprev,
        int row0,
        int row1,
        int[] xyindexnew,
        int kxnew,
        int kynew,
        bool rootcall,
        xparams _params)
    {
        int i = 0;
        int entrywidth = 0;
        int kxprev = 0;
        double v = 0;
        int i0 = 0;
        int i1 = 0;
        double efficiency = 0;
        double cost = 0;
        int rowmid = 0;

        kxprev = (kxnew + 1) / 2;
        entrywidth = 2 + d;
        efficiency = 0.1;
        cost = d * (pt1 - pt0 + 1) * (Math.Log(kxnew) / Math.Log(2)) / efficiency;
        ap.assert(xyindexprev[row0 * (kxprev - 1) + 0] == pt0, "Spline2DFit.ExpandIndexRows: integrity check failed");
        ap.assert(xyindexprev[row1 * (kxprev - 1) + 0] == pt1, "Spline2DFit.ExpandIndexRows: integrity check failed");

        //
        // Parallelism
        //
        if (((rootcall && pt1 - pt0 > 10000) && row1 - row0 >= 2) && (double)(cost) > (double)(apserv.smpactivationlevel(_params)))
        {
            if (_trypexec_expandindexrows(xy, d, shadow, ns, cidx, pt0, pt1, xyindexprev, row0, row1, xyindexnew, kxnew, kynew, rootcall, _params))
            {
                return;
            }
        }

        //
        // Partition
        //
        if (row1 - row0 >= 2)
        {
            apserv.tiledsplit(row1 - row0, 1, ref i0, ref i1, _params);
            rowmid = row0 + i0;
            expandindexrows(xy, d, shadow, ns, cidx, pt0, xyindexprev[rowmid * (kxprev - 1) + 0], xyindexprev, row0, rowmid, xyindexnew, kxnew, kynew, false, _params);
            expandindexrows(xy, d, shadow, ns, cidx, xyindexprev[rowmid * (kxprev - 1) + 0], pt1, xyindexprev, rowmid, row1, xyindexnew, kxnew, kynew, false, _params);
            return;
        }

        //
        // Serial execution
        //
        for (i = pt0; i <= pt1 - 1; i++)
        {
            v = 2 * xy[i * entrywidth + 0];
            xy[i * entrywidth + 0] = v;
            i0 = apserv.iboundval((int)Math.Floor(v), 0, kxnew - 2, _params);
            v = 2 * xy[i * entrywidth + 1];
            xy[i * entrywidth + 1] = v;
            i1 = apserv.iboundval((int)Math.Floor(v), 0, kynew - 2, _params);
            cidx[i] = i1 * (kxnew - 1) + i0;
        }
        reorderdatasetandbuildindexrec(xy, d, shadow, ns, cidx, pt0, pt1, xyindexnew, 2 * row0 * (kxnew - 1) + 0, 2 * row1 * (kxnew - 1) + 0, false, _params);
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_expandindexrows(double[] xy,
        int d,
        double[] shadow,
        int ns,
        int[] cidx,
        int pt0,
        int pt1,
        int[] xyindexprev,
        int row0,
        int row1,
        int[] xyindexnew,
        int kxnew,
        int kynew,
        bool rootcall, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    Recurrent divide-and-conquer indexing function

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void reorderdatasetandbuildindexrec(double[] xy,
        int d,
        double[] shadow,
        int ns,
        int[] cidx,
        int pt0,
        int pt1,
        int[] xyindex,
        int idx0,
        int idx1,
        bool rootcall,
        xparams _params)
    {
        int entrywidth = 0;
        int idxmid = 0;
        int wrk0 = 0;
        int wrk1 = 0;
        double efficiency = 0;
        double cost = 0;


        //
        // Efficiency - performance of the code when compared with that
        // of linear algebra code.
        //
        entrywidth = 2 + d;
        efficiency = 0.1;
        cost = d * (pt1 - pt0 + 1) * Math.Log(idx1 - idx0 + 1) / Math.Log(2) / efficiency;

        //
        // Parallelism
        //
        if (((rootcall && pt1 - pt0 > 10000) && idx1 - idx0 >= 2) && (double)(cost) > (double)(apserv.smpactivationlevel(_params)))
        {
            if (_trypexec_reorderdatasetandbuildindexrec(xy, d, shadow, ns, cidx, pt0, pt1, xyindex, idx0, idx1, rootcall, _params))
            {
                return;
            }
        }

        //
        // Store left bound to XYIndex
        //
        xyindex[idx0] = pt0;

        //
        // Quick exit strategies
        //
        if (idx1 <= idx0 + 1)
        {
            return;
        }
        if (pt0 == pt1)
        {
            for (idxmid = idx0 + 1; idxmid <= idx1 - 1; idxmid++)
            {
                xyindex[idxmid] = pt1;
            }
            return;
        }

        //
        // Select middle element
        //
        idxmid = idx0 + (idx1 - idx0) / 2;
        ap.assert(idx0 < idxmid && idxmid < idx1, "Spline2D: integrity check failed");
        wrk0 = pt0;
        wrk1 = pt1 - 1;
        while (true)
        {
            while (wrk0 < pt1 && cidx[wrk0] < idxmid)
            {
                wrk0 = wrk0 + 1;
            }
            while (wrk1 >= pt0 && cidx[wrk1] >= idxmid)
            {
                wrk1 = wrk1 - 1;
            }
            if (wrk1 <= wrk0)
            {
                break;
            }
            apserv.swapentries(xy, wrk0, wrk1, entrywidth, _params);
            if (ns > 0)
            {
                apserv.swapentries(shadow, wrk0, wrk1, ns, _params);
            }
            apserv.swapelementsi(cidx, wrk0, wrk1, _params);
        }
        reorderdatasetandbuildindexrec(xy, d, shadow, ns, cidx, pt0, wrk0, xyindex, idx0, idxmid, false, _params);
        reorderdatasetandbuildindexrec(xy, d, shadow, ns, cidx, wrk0, pt1, xyindex, idxmid, idx1, false, _params);
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_reorderdatasetandbuildindexrec(double[] xy,
        int d,
        double[] shadow,
        int ns,
        int[] cidx,
        int pt0,
        int pt1,
        int[] xyindex,
        int idx0,
        int idx1,
        bool rootcall, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    This function performs fitting with  BlockLLS solver.  Internal  function,
    never use it directly.

    INPUT PARAMETERS:
        XY      -   dataset, array[NPoints,2+D]
        XYIndex -   dataset index, see ReorderDatasetAndBuildIndex() for more info
        KX0, KX1-   X-indices of basis functions to select and fit;
                    range [KX0,KX1) is processed
        KXTotal -   total number of indexes in the entire grid
        KY0, KY1-   Y-indices of basis functions to select and fit;
                    range [KY0,KY1) is processed
        KYTotal -   total number of indexes in the entire grid
        D       -   number of components in vector-valued spline, D>=1
        LambdaReg-  regularization coefficient
        LambdaNS-   nonlinearity penalty, exactly zero value is specially handled
                    (entire set of rows is not added to the matrix)
        Basis1  -   single-dimensional B-spline
        

    OUTPUT PARAMETERS:
        A       -   design matrix

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void xdesigngenerate(double[] xy,
        int[] xyindex,
        int kx0,
        int kx1,
        int kxtotal,
        int ky0,
        int ky1,
        int kytotal,
        int d,
        double lambdareg,
        double lambdans,
        spline1d.spline1dinterpolant basis1,
        spline2dxdesignmatrix a,
        xparams _params)
    {
        int entrywidth = 0;
        int i = 0;
        int j = 0;
        int j0 = 0;
        int j1 = 0;
        int k0 = 0;
        int k1 = 0;
        int kx = 0;
        int ky = 0;
        int rowsdone = 0;
        int batchesdone = 0;
        int pt0 = 0;
        int pt1 = 0;
        int base0 = 0;
        int base1 = 0;
        int baseidx = 0;
        int nzshift = 0;
        int nzwidth = 0;
        double[,] d2x = new double[0, 0];
        double[,] d2y = new double[0, 0];
        double[,] dxy = new double[0, 0];
        double v = 0;
        double v0 = 0;
        double v1 = 0;
        double v2 = 0;
        double w0 = 0;
        double w1 = 0;
        double w2 = 0;

        nzshift = 1;
        nzwidth = 4;
        entrywidth = 2 + d;
        kx = kx1 - kx0;
        ky = ky1 - ky0;
        a.lambdareg = lambdareg;
        a.blockwidth = 4;
        a.kx = kx;
        a.ky = ky;
        a.d = d;
        a.npoints = 0;
        a.ndenserows = 0;
        a.ndensebatches = 0;
        a.maxbatch = 0;
        for (j1 = ky0; j1 <= ky1 - 2; j1++)
        {
            for (j0 = kx0; j0 <= kx1 - 2; j0++)
            {
                i = xyindex[j1 * (kxtotal - 1) + j0 + 1] - xyindex[j1 * (kxtotal - 1) + j0];
                a.npoints = a.npoints + i;
                a.ndenserows = a.ndenserows + i;
                a.ndensebatches = a.ndensebatches + 1;
                a.maxbatch = Math.Max(a.maxbatch, i);
            }
        }
        if ((double)(lambdans) != (double)(0))
        {
            ap.assert((double)(lambdans) >= (double)(0), "Spline2DFit: integrity check failed");
            a.ndenserows = a.ndenserows + 3 * (kx - 2) * (ky - 2);
            a.ndensebatches = a.ndensebatches + (kx - 2) * (ky - 2);
            a.maxbatch = Math.Max(a.maxbatch, 3);
        }
        a.nrows = a.ndenserows + kx * ky;
        apserv.rmatrixsetlengthatleast(ref a.vals, a.ndenserows, a.blockwidth * a.blockwidth + d, _params);
        apserv.ivectorsetlengthatleast(ref a.batches, a.ndensebatches + 1, _params);
        apserv.ivectorsetlengthatleast(ref a.batchbases, a.ndensebatches, _params);

        //
        // Setup output counters
        //
        batchesdone = 0;
        rowsdone = 0;

        //
        // Generate rows corresponding to dataset points
        //
        ap.assert(kx >= nzwidth, "Spline2DFit: integrity check failed");
        ap.assert(ky >= nzwidth, "Spline2DFit: integrity check failed");
        apserv.rvectorsetlengthatleast(ref a.tmp0, nzwidth, _params);
        apserv.rvectorsetlengthatleast(ref a.tmp1, nzwidth, _params);
        a.batches[batchesdone] = 0;
        for (j1 = ky0; j1 <= ky1 - 2; j1++)
        {
            for (j0 = kx0; j0 <= kx1 - 2; j0++)
            {
                pt0 = xyindex[j1 * (kxtotal - 1) + j0];
                pt1 = xyindex[j1 * (kxtotal - 1) + j0 + 1];
                base0 = apserv.iboundval(j0 - kx0 - nzshift, 0, kx - nzwidth, _params);
                base1 = apserv.iboundval(j1 - ky0 - nzshift, 0, ky - nzwidth, _params);
                baseidx = base1 * kx + base0;
                a.batchbases[batchesdone] = baseidx;
                for (i = pt0; i <= pt1 - 1; i++)
                {
                    for (k0 = 0; k0 <= nzwidth - 1; k0++)
                    {
                        a.tmp0[k0] = spline1d.spline1dcalc(basis1, xy[i * entrywidth + 0] - (base0 + kx0 + k0), _params);
                    }
                    for (k1 = 0; k1 <= nzwidth - 1; k1++)
                    {
                        a.tmp1[k1] = spline1d.spline1dcalc(basis1, xy[i * entrywidth + 1] - (base1 + ky0 + k1), _params);
                    }
                    for (k1 = 0; k1 <= nzwidth - 1; k1++)
                    {
                        for (k0 = 0; k0 <= nzwidth - 1; k0++)
                        {
                            a.vals[rowsdone, k1 * nzwidth + k0] = a.tmp0[k0] * a.tmp1[k1];
                        }
                    }
                    for (j = 0; j <= d - 1; j++)
                    {
                        a.vals[rowsdone, nzwidth * nzwidth + j] = xy[i * entrywidth + 2 + j];
                    }
                    rowsdone = rowsdone + 1;
                }
                batchesdone = batchesdone + 1;
                a.batches[batchesdone] = rowsdone;
            }
        }

        //
        // Generate rows corresponding to nonlinearity penalty
        //
        if ((double)(lambdans) > (double)(0))
        {

            //
            // Smoothing is applied. Because all grid nodes are same,
            // we apply same smoothing kernel, which is calculated only
            // once at the beginning of design matrix generation.
            //
            d2x = new double[3, 3];
            d2y = new double[3, 3];
            dxy = new double[3, 3];
            for (j1 = 0; j1 <= 2; j1++)
            {
                for (j0 = 0; j0 <= 2; j0++)
                {
                    d2x[j0, j1] = 0.0;
                    d2y[j0, j1] = 0.0;
                    dxy[j0, j1] = 0.0;
                }
            }
            for (k1 = 0; k1 <= 2; k1++)
            {
                for (k0 = 0; k0 <= 2; k0++)
                {
                    spline1d.spline1ddiff(basis1, -(k0 - 1), ref v0, ref v1, ref v2, _params);
                    spline1d.spline1ddiff(basis1, -(k1 - 1), ref w0, ref w1, ref w2, _params);
                    d2x[k0, k1] = d2x[k0, k1] + v2 * w0;
                    d2y[k0, k1] = d2y[k0, k1] + w2 * v0;
                    dxy[k0, k1] = dxy[k0, k1] + v1 * w1;
                }
            }

            //
            // Now, kernel is ready - apply it to all inner nodes of the grid.
            //
            for (j1 = 1; j1 <= ky - 2; j1++)
            {
                for (j0 = 1; j0 <= kx - 2; j0++)
                {
                    base0 = apserv.imax2(j0 - 2, 0, _params);
                    base1 = apserv.imax2(j1 - 2, 0, _params);
                    baseidx = base1 * kx + base0;
                    a.batchbases[batchesdone] = baseidx;

                    //
                    // d2F/dx2 term
                    //
                    v = lambdans;
                    for (j = 0; j <= nzwidth * nzwidth + d - 1; j++)
                    {
                        a.vals[rowsdone, j] = 0;
                    }
                    for (k1 = j1 - 1; k1 <= j1 + 1; k1++)
                    {
                        for (k0 = j0 - 1; k0 <= j0 + 1; k0++)
                        {
                            a.vals[rowsdone, nzwidth * (k1 - base1) + (k0 - base0)] = v * d2x[1 + (k0 - j0), 1 + (k1 - j1)];
                        }
                    }
                    rowsdone = rowsdone + 1;

                    //
                    // d2F/dy2 term
                    //
                    v = lambdans;
                    for (j = 0; j <= nzwidth * nzwidth + d - 1; j++)
                    {
                        a.vals[rowsdone, j] = 0;
                    }
                    for (k1 = j1 - 1; k1 <= j1 + 1; k1++)
                    {
                        for (k0 = j0 - 1; k0 <= j0 + 1; k0++)
                        {
                            a.vals[rowsdone, nzwidth * (k1 - base1) + (k0 - base0)] = v * d2y[1 + (k0 - j0), 1 + (k1 - j1)];
                        }
                    }
                    rowsdone = rowsdone + 1;

                    //
                    // 2*d2F/dxdy term
                    //
                    v = Math.Sqrt(2) * lambdans;
                    for (j = 0; j <= nzwidth * nzwidth + d - 1; j++)
                    {
                        a.vals[rowsdone, j] = 0;
                    }
                    for (k1 = j1 - 1; k1 <= j1 + 1; k1++)
                    {
                        for (k0 = j0 - 1; k0 <= j0 + 1; k0++)
                        {
                            a.vals[rowsdone, nzwidth * (k1 - base1) + (k0 - base0)] = v * dxy[1 + (k0 - j0), 1 + (k1 - j1)];
                        }
                    }
                    rowsdone = rowsdone + 1;
                    batchesdone = batchesdone + 1;
                    a.batches[batchesdone] = rowsdone;
                }
            }
        }

        //
        // Integrity post-check
        //
        ap.assert(batchesdone == a.ndensebatches, "Spline2DFit: integrity check failed");
        ap.assert(rowsdone == a.ndenserows, "Spline2DFit: integrity check failed");
    }


    /*************************************************************************
    This function performs matrix-vector product of design matrix and dense
    vector.

    INPUT PARAMETERS:
        A       -   design matrix, (a.nrows) X (a.kx*a.ky);
                    some fields of A are used for temporaries,
                    so it is non-constant.
        X       -   array[A.KX*A.KY]
        

    OUTPUT PARAMETERS:
        Y       -   product, array[A.NRows], automatically allocated

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void xdesignmv(spline2dxdesignmatrix a,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        int bidx = 0;
        int i = 0;
        int cnt = 0;
        double v = 0;
        int baseidx = 0;
        int outidx = 0;
        int batchsize = 0;
        int kx = 0;
        int k0 = 0;
        int k1 = 0;
        int nzwidth = 0;

        nzwidth = 4;
        ap.assert(a.blockwidth == nzwidth, "Spline2DFit: integrity check failed");
        ap.assert(ap.len(x) >= a.kx * a.ky, "Spline2DFit: integrity check failed");

        //
        // Prepare
        //
        apserv.rvectorsetlengthatleast(ref y, a.nrows, _params);
        apserv.rvectorsetlengthatleast(ref a.tmp0, nzwidth * nzwidth, _params);
        apserv.rvectorsetlengthatleast(ref a.tmp1, a.maxbatch, _params);
        kx = a.kx;
        outidx = 0;

        //
        // Process dense part
        //
        for (bidx = 0; bidx <= a.ndensebatches - 1; bidx++)
        {
            if (a.batches[bidx + 1] - a.batches[bidx] > 0)
            {
                batchsize = a.batches[bidx + 1] - a.batches[bidx];
                baseidx = a.batchbases[bidx];
                for (k1 = 0; k1 <= nzwidth - 1; k1++)
                {
                    for (k0 = 0; k0 <= nzwidth - 1; k0++)
                    {
                        a.tmp0[k1 * nzwidth + k0] = x[baseidx + k1 * kx + k0];
                    }
                }
                ablas.rmatrixgemv(batchsize, nzwidth * nzwidth, 1.0, a.vals, a.batches[bidx], 0, 0, a.tmp0, 0, 0.0, a.tmp1, 0, _params);
                for (i = 0; i <= batchsize - 1; i++)
                {
                    y[outidx + i] = a.tmp1[i];
                }
                outidx = outidx + batchsize;
            }
        }
        ap.assert(outidx == a.ndenserows, "Spline2DFit: integrity check failed");

        //
        // Process regularizer 
        //
        v = a.lambdareg;
        cnt = a.kx * a.ky;
        for (i = 0; i <= cnt - 1; i++)
        {
            y[outidx + i] = v * x[i];
        }
        outidx = outidx + cnt;

        //
        // Post-check
        //
        ap.assert(outidx == a.nrows, "Spline2DFit: integrity check failed");
    }


    /*************************************************************************
    This function performs matrix-vector product of transposed design matrix and dense
    vector.

    INPUT PARAMETERS:
        A       -   design matrix, (a.nrows) X (a.kx*a.ky);
                    some fields of A are used for temporaries,
                    so it is non-constant.
        X       -   array[A.NRows]
        

    OUTPUT PARAMETERS:
        Y       -   product, array[A.KX*A.KY], automatically allocated

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void xdesignmtv(spline2dxdesignmatrix a,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        int bidx = 0;
        int i = 0;
        int cnt = 0;
        double v = 0;
        int baseidx = 0;
        int inidx = 0;
        int batchsize = 0;
        int kx = 0;
        int k0 = 0;
        int k1 = 0;
        int nzwidth = 0;

        nzwidth = 4;
        ap.assert(a.blockwidth == nzwidth, "Spline2DFit: integrity check failed");
        ap.assert(ap.len(x) >= a.nrows, "Spline2DFit: integrity check failed");

        //
        // Prepare
        //
        apserv.rvectorsetlengthatleast(ref y, a.kx * a.ky, _params);
        apserv.rvectorsetlengthatleast(ref a.tmp0, nzwidth * nzwidth, _params);
        apserv.rvectorsetlengthatleast(ref a.tmp1, a.maxbatch, _params);
        kx = a.kx;
        inidx = 0;
        cnt = a.kx * a.ky;
        for (i = 0; i <= cnt - 1; i++)
        {
            y[i] = 0;
        }

        //
        // Process dense part
        //
        for (bidx = 0; bidx <= a.ndensebatches - 1; bidx++)
        {
            if (a.batches[bidx + 1] - a.batches[bidx] > 0)
            {
                batchsize = a.batches[bidx + 1] - a.batches[bidx];
                baseidx = a.batchbases[bidx];
                for (i = 0; i <= batchsize - 1; i++)
                {
                    a.tmp1[i] = x[inidx + i];
                }
                ablas.rmatrixgemv(nzwidth * nzwidth, batchsize, 1.0, a.vals, a.batches[bidx], 0, 1, a.tmp1, 0, 0.0, a.tmp0, 0, _params);
                for (k1 = 0; k1 <= nzwidth - 1; k1++)
                {
                    for (k0 = 0; k0 <= nzwidth - 1; k0++)
                    {
                        y[baseidx + k1 * kx + k0] = y[baseidx + k1 * kx + k0] + a.tmp0[k1 * nzwidth + k0];
                    }
                }
                inidx = inidx + batchsize;
            }
        }
        ap.assert(inidx == a.ndenserows, "Spline2DFit: integrity check failed");

        //
        // Process regularizer 
        //
        v = a.lambdareg;
        cnt = a.kx * a.ky;
        for (i = 0; i <= cnt - 1; i++)
        {
            y[i] = y[i] + v * x[inidx + i];
        }
        inidx = inidx + cnt;

        //
        // Post-check
        //
        ap.assert(inidx == a.nrows, "Spline2DFit: integrity check failed");
    }


    /*************************************************************************
    This function generates squared design matrix stored in block band format.

    We  use  an   adaptation   of   block   skyline   storage   format,   with
    TOWERSIZE*KX   skyline    bands   (towers)   stored   sequentially;   here
    TOWERSIZE=(BlockBandwidth+1)*KX. So, we have KY "towers", stored one below
    other, in BlockATA matrix. Every "tower" is a sequence of BlockBandwidth+1
    cells, each of them being KX*KX in size.

    INPUT PARAMETERS:
        A       -   design matrix; some of its fields are used for temporaries
        BlockATA-   array[KY*(BlockBandwidth+1)*KX,KX],  preallocated  storage
                    for output matrix in compressed block band format

    OUTPUT PARAMETERS:
        BlockATA-   AH*AH', stored in compressed block band format
        MXATA   -   max(|AH*AH'|), elementwise

      -- ALGLIB --
         Copyright 05.02.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void xdesignblockata(spline2dxdesignmatrix a,
        double[,] blockata,
        ref double mxata,
        xparams _params)
    {
        int blockbandwidth = 0;
        int nzwidth = 0;
        int kx = 0;
        int ky = 0;
        int i0 = 0;
        int i1 = 0;
        int j0 = 0;
        int j1 = 0;
        int celloffset = 0;
        int bidx = 0;
        int baseidx = 0;
        int batchsize = 0;
        int offs0 = 0;
        int offs1 = 0;
        double v = 0;

        blockbandwidth = 3;
        nzwidth = 4;
        kx = a.kx;
        ky = a.ky;
        ap.assert(a.blockwidth == nzwidth, "Spline2DFit: integrity check failed");
        apserv.rmatrixsetlengthatleast(ref a.tmp2, nzwidth * nzwidth, nzwidth * nzwidth, _params);

        //
        // Initial zero-fill:
        // * zero-fill ALL elements of BlockATA
        // * zero-fill ALL elements of Tmp2
        //
        // Filling ALL elements, including unused ones, is essential for the
        // purposes of calculating max(BlockATA).
        //
        for (i1 = 0; i1 <= ky - 1; i1++)
        {
            for (i0 = i1; i0 <= Math.Min(ky - 1, i1 + blockbandwidth); i0++)
            {
                celloffset = getcelloffset(kx, ky, blockbandwidth, i1, i0, _params);
                for (j1 = 0; j1 <= kx - 1; j1++)
                {
                    for (j0 = 0; j0 <= kx - 1; j0++)
                    {
                        blockata[celloffset + j1, j0] = 0.0;
                    }
                }
            }
        }
        for (j1 = 0; j1 <= nzwidth * nzwidth - 1; j1++)
        {
            for (j0 = 0; j0 <= nzwidth * nzwidth - 1; j0++)
            {
                a.tmp2[j1, j0] = 0.0;
            }
        }

        //
        // Process dense part of A
        //
        for (bidx = 0; bidx <= a.ndensebatches - 1; bidx++)
        {
            if (a.batches[bidx + 1] - a.batches[bidx] > 0)
            {

                //
                // Generate 16x16 U = BATCH'*BATCH and add it to ATA.
                //
                // NOTE: it is essential that lower triangle of Tmp2 is
                //       filled by zeros.
                //
                batchsize = a.batches[bidx + 1] - a.batches[bidx];
                ablas.rmatrixsyrk(nzwidth * nzwidth, batchsize, 1.0, a.vals, a.batches[bidx], 0, 2, 0.0, a.tmp2, 0, 0, true, _params);
                baseidx = a.batchbases[bidx];
                for (i1 = 0; i1 <= nzwidth - 1; i1++)
                {
                    for (j1 = i1; j1 <= nzwidth - 1; j1++)
                    {
                        celloffset = getcelloffset(kx, ky, blockbandwidth, baseidx / kx + i1, baseidx / kx + j1, _params);
                        offs0 = baseidx % kx;
                        offs1 = baseidx % kx;
                        for (i0 = 0; i0 <= nzwidth - 1; i0++)
                        {
                            for (j0 = 0; j0 <= nzwidth - 1; j0++)
                            {
                                v = a.tmp2[i1 * nzwidth + i0, j1 * nzwidth + j0];
                                blockata[celloffset + offs1 + i0, offs0 + j0] = blockata[celloffset + offs1 + i0, offs0 + j0] + v;
                            }
                        }
                    }
                }
            }
        }

        //
        // Process regularizer term
        //
        for (i1 = 0; i1 <= ky - 1; i1++)
        {
            celloffset = getcelloffset(kx, ky, blockbandwidth, i1, i1, _params);
            for (j1 = 0; j1 <= kx - 1; j1++)
            {
                blockata[celloffset + j1, j1] = blockata[celloffset + j1, j1] + math.sqr(a.lambdareg);
            }
        }

        //
        // Calculate max(ATA)
        //
        // NOTE: here we rely on zero initialization of unused parts of
        //       BlockATA and Tmp2.
        //
        mxata = 0.0;
        for (i1 = 0; i1 <= ky - 1; i1++)
        {
            for (i0 = i1; i0 <= Math.Min(ky - 1, i1 + blockbandwidth); i0++)
            {
                celloffset = getcelloffset(kx, ky, blockbandwidth, i1, i0, _params);
                for (j1 = 0; j1 <= kx - 1; j1++)
                {
                    for (j0 = 0; j0 <= kx - 1; j0++)
                    {
                        mxata = Math.Max(mxata, Math.Abs(blockata[celloffset + j1, j0]));
                    }
                }
            }
        }
    }


    /*************************************************************************
    Adjust evaluation interval: if we are inside missing cell, but very  close
    to the nonmissing one, move to its boundaries.

    This function is used to avoid situation when evaluation at the nodes adjacent
    to missing cells fails due to rounding errors that move us away  from  the
    feasible cell.

    Returns True if X/Y, DX/DY, IX/IY were successfully  repositioned  to  the
    nearest nonmissing cell (or were feasible from the very beginning).  False
    is returned if we are deep in the missing cell.

      -- ALGLIB --
         Copyright 26.06.2022 by Bochkanov Sergey
    *************************************************************************/
    private static bool adjustevaluationinterval(spline2dinterpolant s,
        ref double x,
        ref double t,
        ref double dt,
        ref int ix,
        ref double y,
        ref double u,
        ref double du,
        ref int iy,
        xparams _params)
    {
        bool result = new bool();
        double tol = 0;
        bool tryleftbndx = new bool();
        bool tryrightbndx = new bool();
        bool trycenterx = new bool();
        bool tryleftbndy = new bool();
        bool tryrightbndy = new bool();
        bool trycentery = new bool();


        //
        // Quick exit - no missing cells, or we are at non-missing cell
        //
        result = !s.hasmissingcells || !s.ismissingcell[(s.n - 1) * iy + ix];
        if (result)
        {
            return result;
        }

        //
        // Missing cell, but maybe we are really close to some non-missing cell?
        //
        tol = 1000 * math.machineepsilon;
        tryleftbndx = (double)(t) < (double)(tol) && ix > 0;
        tryrightbndx = (double)(t) > (double)(1 - tol) && ix + 1 < s.n - 1;
        trycenterx = true;
        tryleftbndy = (double)(u) < (double)(tol) && iy > 0;
        tryrightbndy = (double)(u) > (double)(1 - tol) && iy + 1 < s.m - 1;
        trycentery = true;
        if (((!result && tryleftbndx) && tryleftbndy) && !s.ismissingcell[(s.n - 1) * (iy - 1) + (ix - 1)])
        {
            ix = ix - 1;
            iy = iy - 1;
            x = s.x[ix + 1];
            y = s.y[iy + 1];
            result = true;
        }
        if (((!result && tryleftbndx) && trycentery) && !s.ismissingcell[(s.n - 1) * (iy + 0) + (ix - 1)])
        {
            ix = ix - 1;
            x = s.x[ix + 1];
            result = true;
        }
        if (((!result && tryleftbndx) && tryrightbndy) && !s.ismissingcell[(s.n - 1) * (iy + 1) + (ix - 1)])
        {
            ix = ix - 1;
            iy = iy + 1;
            x = s.x[ix + 1];
            y = s.y[iy];
            result = true;
        }
        if (((!result && trycenterx) && tryleftbndy) && !s.ismissingcell[(s.n - 1) * (iy - 1) + (ix + 0)])
        {
            iy = iy - 1;
            y = s.y[iy + 1];
            result = true;
        }
        if (((!result && trycenterx) && tryrightbndy) && !s.ismissingcell[(s.n - 1) * (iy + 1) + (ix + 0)])
        {
            iy = iy + 1;
            y = s.y[iy];
            result = true;
        }
        if (((!result && tryrightbndx) && tryleftbndy) && !s.ismissingcell[(s.n - 1) * (iy - 1) + (ix + 1)])
        {
            ix = ix + 1;
            iy = iy - 1;
            x = s.x[ix];
            y = s.y[iy + 1];
            result = true;
        }
        if (((!result && tryrightbndx) && trycentery) && !s.ismissingcell[(s.n - 1) * (iy + 0) + (ix + 1)])
        {
            ix = ix + 1;
            x = s.x[ix];
            result = true;
        }
        if (((!result && tryrightbndx) && tryrightbndy) && !s.ismissingcell[(s.n - 1) * (iy + 1) + (ix + 1)])
        {
            ix = ix + 1;
            iy = iy + 1;
            x = s.x[ix];
            y = s.y[iy];
            result = true;
        }
        if (result)
        {
            dt = 1.0 / (s.x[ix + 1] - s.x[ix]);
            t = (x - s.x[ix]) * dt;
            du = 1.0 / (s.y[iy + 1] - s.y[iy]);
            u = (y - s.y[iy]) * du;
        }
        return result;
    }


}
