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

public class intcomp
{
    /*************************************************************************
    This function is left for backward compatibility.
    Use fitspheremc() instead.

                                        
      -- ALGLIB --
         Copyright 14.04.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void nsfitspheremcc(double[,] xy,
        int npoints,
        int nx,
        ref double[] cx,
        ref double rhi,
        xparams _params)
    {
        double dummy = 0;

        cx = new double[0];
        rhi = 0;

        nsfitspherex(xy, npoints, nx, 1, 0.0, 0, 0.0, ref cx, ref dummy, ref rhi, _params);
    }


    /*************************************************************************
    This function is left for backward compatibility.
    Use fitspheremi() instead.
                                        
      -- ALGLIB --
         Copyright 14.04.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void nsfitspheremic(double[,] xy,
        int npoints,
        int nx,
        ref double[] cx,
        ref double rlo,
        xparams _params)
    {
        double dummy = 0;

        cx = new double[0];
        rlo = 0;

        nsfitspherex(xy, npoints, nx, 2, 0.0, 0, 0.0, ref cx, ref rlo, ref dummy, _params);
    }


    /*************************************************************************
    This function is left for backward compatibility.
    Use fitspheremz() instead.
                                        
      -- ALGLIB --
         Copyright 14.04.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void nsfitspheremzc(double[,] xy,
        int npoints,
        int nx,
        ref double[] cx,
        ref double rlo,
        ref double rhi,
        xparams _params)
    {
        cx = new double[0];
        rlo = 0;
        rhi = 0;

        nsfitspherex(xy, npoints, nx, 3, 0.0, 0, 0.0, ref cx, ref rlo, ref rhi, _params);
    }


    /*************************************************************************
    This function is left for backward compatibility.
    Use fitspherex() instead.
                                        
      -- ALGLIB --
         Copyright 14.04.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void nsfitspherex(double[,] xy,
        int npoints,
        int nx,
        int problemtype,
        double epsx,
        int aulits,
        double penalty,
        ref double[] cx,
        ref double rlo,
        ref double rhi,
        xparams _params)
    {
        cx = new double[0];
        rlo = 0;
        rhi = 0;

        fitsphere.fitspherex(xy, npoints, nx, problemtype, epsx, aulits, penalty, ref cx, ref rlo, ref rhi, _params);
    }


    /*************************************************************************
    This function is an obsolete and deprecated version of fitting by
    penalized cubic spline.

    It was superseded by spline1dfit(), which is an orders of magnitude faster
    and more memory-efficient implementation.

    Do NOT use this function in the new code!

      -- ALGLIB PROJECT --
         Copyright 18.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dfitpenalized(double[] x,
        double[] y,
        int n,
        int m,
        double rho,
        ref int info,
        spline1d.spline1dinterpolant s,
        spline1d.spline1dfitreport rep,
        xparams _params)
    {
        double[] w = new double[0];
        int i = 0;

        x = (double[])x.Clone();
        y = (double[])y.Clone();
        info = 0;

        ap.assert(n >= 1, "Spline1DFitPenalized: N<1!");
        ap.assert(m >= 4, "Spline1DFitPenalized: M<4!");
        ap.assert(ap.len(x) >= n, "Spline1DFitPenalized: Length(X)<N!");
        ap.assert(ap.len(y) >= n, "Spline1DFitPenalized: Length(Y)<N!");
        ap.assert(apserv.isfinitevector(x, n, _params), "Spline1DFitPenalized: X contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(y, n, _params), "Spline1DFitPenalized: Y contains infinite or NAN values!");
        ap.assert(math.isfinite(rho), "Spline1DFitPenalized: Rho is infinite!");
        w = new double[n];
        for (i = 0; i <= n - 1; i++)
        {
            w[i] = 1;
        }
        spline1dfitpenalizedw(x, y, w, n, m, rho, ref info, s, rep, _params);
    }


    /*************************************************************************
    This function is an obsolete and deprecated version of fitting by
    penalized cubic spline.

    It was superseded by spline1dfit(), which is an orders of magnitude faster
    and more memory-efficient implementation.

    Do NOT use this function in the new code!

      -- ALGLIB PROJECT --
         Copyright 19.10.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dfitpenalizedw(double[] x,
        double[] y,
        double[] w,
        int n,
        int m,
        double rho,
        ref int info,
        spline1d.spline1dinterpolant s,
        spline1d.spline1dfitreport rep,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int b = 0;
        double v = 0;
        double relcnt = 0;
        double xa = 0;
        double xb = 0;
        double sa = 0;
        double sb = 0;
        double[] xoriginal = new double[0];
        double[] yoriginal = new double[0];
        double pdecay = 0;
        double tdecay = 0;
        double[,] fmatrix = new double[0, 0];
        double[] fcolumn = new double[0];
        double[] y2 = new double[0];
        double[] w2 = new double[0];
        double[] xc = new double[0];
        double[] yc = new double[0];
        int[] dc = new int[0];
        double fdmax = 0;
        double admax = 0;
        double[,] amatrix = new double[0, 0];
        double[,] d2matrix = new double[0, 0];
        double fa = 0;
        double ga = 0;
        double fb = 0;
        double gb = 0;
        double lambdav = 0;
        double[] bx = new double[0];
        double[] by = new double[0];
        double[] bd1 = new double[0];
        double[] bd2 = new double[0];
        double[] tx = new double[0];
        double[] ty = new double[0];
        double[] td = new double[0];
        spline1d.spline1dinterpolant bs = new spline1d.spline1dinterpolant();
        double[,] nmatrix = new double[0, 0];
        double[] rightpart = new double[0];
        fbls.fblslincgstate cgstate = new fbls.fblslincgstate();
        double[] c = new double[0];
        double[] tmp0 = new double[0];
        int i_ = 0;
        int i1_ = 0;

        x = (double[])x.Clone();
        y = (double[])y.Clone();
        w = (double[])w.Clone();
        info = 0;

        ap.assert(n >= 1, "Spline1DFitPenalizedW: N<1!");
        ap.assert(m >= 4, "Spline1DFitPenalizedW: M<4!");
        ap.assert(ap.len(x) >= n, "Spline1DFitPenalizedW: Length(X)<N!");
        ap.assert(ap.len(y) >= n, "Spline1DFitPenalizedW: Length(Y)<N!");
        ap.assert(ap.len(w) >= n, "Spline1DFitPenalizedW: Length(W)<N!");
        ap.assert(apserv.isfinitevector(x, n, _params), "Spline1DFitPenalizedW: X contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(y, n, _params), "Spline1DFitPenalizedW: Y contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(w, n, _params), "Spline1DFitPenalizedW: Y contains infinite or NAN values!");
        ap.assert(math.isfinite(rho), "Spline1DFitPenalizedW: Rho is infinite!");

        //
        // Prepare LambdaV
        //
        v = -(Math.Log(math.machineepsilon) / Math.Log(10));
        if ((double)(rho) < (double)(-v))
        {
            rho = -v;
        }
        if ((double)(rho) > (double)(v))
        {
            rho = v;
        }
        lambdav = Math.Pow(10, rho);

        //
        // Sort X, Y, W
        //
        spline1d.heapsortdpoints(ref x, ref y, ref w, n, _params);

        //
        // Scale X, Y, XC, YC
        //
        intfitserv.lsfitscalexy(ref x, ref y, ref w, n, ref xc, ref yc, dc, 0, ref xa, ref xb, ref sa, ref sb, ref xoriginal, ref yoriginal, _params);

        //
        // Allocate space
        //
        fmatrix = new double[n, m];
        amatrix = new double[m, m];
        d2matrix = new double[m, m];
        bx = new double[m];
        by = new double[m];
        fcolumn = new double[n];
        nmatrix = new double[m, m];
        rightpart = new double[m];
        tmp0 = new double[Math.Max(m, n)];
        c = new double[m];

        //
        // Fill:
        // * FMatrix by values of basis functions
        // * TmpAMatrix by second derivatives of I-th function at J-th point
        // * CMatrix by constraints
        //
        fdmax = 0;
        for (b = 0; b <= m - 1; b++)
        {

            //
            // Prepare I-th basis function
            //
            for (j = 0; j <= m - 1; j++)
            {
                bx[j] = (double)(2 * j) / (double)(m - 1) - 1;
                by[j] = 0;
            }
            by[b] = 1;
            spline1d.spline1dgriddiff2cubic(bx, by, m, 2, 0.0, 2, 0.0, ref bd1, ref bd2, _params);
            spline1d.spline1dbuildcubic(bx, by, m, 2, 0.0, 2, 0.0, bs, _params);

            //
            // Calculate B-th column of FMatrix
            // Update FDMax (maximum column norm)
            //
            spline1d.spline1dconvcubic(bx, by, m, 2, 0.0, 2, 0.0, x, n, ref fcolumn, _params);
            for (i_ = 0; i_ <= n - 1; i_++)
            {
                fmatrix[i_, b] = fcolumn[i_];
            }
            v = 0;
            for (i = 0; i <= n - 1; i++)
            {
                v = v + math.sqr(w[i] * fcolumn[i]);
            }
            fdmax = Math.Max(fdmax, v);

            //
            // Fill temporary with second derivatives of basis function
            //
            for (i_ = 0; i_ <= m - 1; i_++)
            {
                d2matrix[b, i_] = bd2[i_];
            }
        }

        //
        // * calculate penalty matrix A
        // * calculate max of diagonal elements of A
        // * calculate PDecay - coefficient before penalty matrix
        //
        for (i = 0; i <= m - 1; i++)
        {
            for (j = i; j <= m - 1; j++)
            {

                //
                // calculate integral(B_i''*B_j'') where B_i and B_j are
                // i-th and j-th basis splines.
                // B_i and B_j are piecewise linear functions.
                //
                v = 0;
                for (b = 0; b <= m - 2; b++)
                {
                    fa = d2matrix[i, b];
                    fb = d2matrix[i, b + 1];
                    ga = d2matrix[j, b];
                    gb = d2matrix[j, b + 1];
                    v = v + (bx[b + 1] - bx[b]) * (fa * ga + (fa * (gb - ga) + ga * (fb - fa)) / 2 + (fb - fa) * (gb - ga) / 3);
                }
                amatrix[i, j] = v;
                amatrix[j, i] = v;
            }
        }
        admax = 0;
        for (i = 0; i <= m - 1; i++)
        {
            admax = Math.Max(admax, Math.Abs(amatrix[i, i]));
        }
        pdecay = lambdav * fdmax / admax;

        //
        // Calculate TDecay for Tikhonov regularization
        //
        tdecay = fdmax * (1 + pdecay) * 10 * math.machineepsilon;

        //
        // Prepare system
        //
        // NOTE: FMatrix is spoiled during this process
        //
        for (i = 0; i <= n - 1; i++)
        {
            v = w[i];
            for (i_ = 0; i_ <= m - 1; i_++)
            {
                fmatrix[i, i_] = v * fmatrix[i, i_];
            }
        }
        ablas.rmatrixgemm(m, m, n, 1.0, fmatrix, 0, 0, 1, fmatrix, 0, 0, 0, 0.0, nmatrix, 0, 0, _params);
        for (i = 0; i <= m - 1; i++)
        {
            for (j = 0; j <= m - 1; j++)
            {
                nmatrix[i, j] = nmatrix[i, j] + pdecay * amatrix[i, j];
            }
        }
        for (i = 0; i <= m - 1; i++)
        {
            nmatrix[i, i] = nmatrix[i, i] + tdecay;
        }
        for (i = 0; i <= m - 1; i++)
        {
            rightpart[i] = 0;
        }
        for (i = 0; i <= n - 1; i++)
        {
            v = y[i] * w[i];
            for (i_ = 0; i_ <= m - 1; i_++)
            {
                rightpart[i_] = rightpart[i_] + v * fmatrix[i, i_];
            }
        }

        //
        // Solve system
        //
        if (!trfac.spdmatrixcholesky(nmatrix, m, true, _params))
        {
            info = -4;
            return;
        }
        fbls.fblscholeskysolve(nmatrix, 1.0, m, true, rightpart, ref tmp0, _params);
        for (i_ = 0; i_ <= m - 1; i_++)
        {
            c[i_] = rightpart[i_];
        }

        //
        // add nodes to force linearity outside of the fitting interval
        //
        spline1d.spline1dgriddiffcubic(bx, c, m, 2, 0.0, 2, 0.0, ref bd1, _params);
        tx = new double[m + 2];
        ty = new double[m + 2];
        td = new double[m + 2];
        i1_ = (0) - (1);
        for (i_ = 1; i_ <= m; i_++)
        {
            tx[i_] = bx[i_ + i1_];
        }
        i1_ = (0) - (1);
        for (i_ = 1; i_ <= m; i_++)
        {
            ty[i_] = rightpart[i_ + i1_];
        }
        i1_ = (0) - (1);
        for (i_ = 1; i_ <= m; i_++)
        {
            td[i_] = bd1[i_ + i1_];
        }
        tx[0] = tx[1] - (tx[2] - tx[1]);
        ty[0] = ty[1] - td[1] * (tx[2] - tx[1]);
        td[0] = td[1];
        tx[m + 1] = tx[m] + (tx[m] - tx[m - 1]);
        ty[m + 1] = ty[m] + td[m] * (tx[m] - tx[m - 1]);
        td[m + 1] = td[m];
        spline1d.spline1dbuildhermite(tx, ty, td, m + 2, s, _params);
        spline1d.spline1dlintransx(s, 2 / (xb - xa), -((xa + xb) / (xb - xa)), _params);
        spline1d.spline1dlintransy(s, sb - sa, sa, _params);
        info = 1;

        //
        // Fill report
        //
        rep.rmserror = 0;
        rep.avgerror = 0;
        rep.avgrelerror = 0;
        rep.maxerror = 0;
        relcnt = 0;
        spline1d.spline1dconvcubic(bx, rightpart, m, 2, 0.0, 2, 0.0, x, n, ref fcolumn, _params);
        for (i = 0; i <= n - 1; i++)
        {
            v = (sb - sa) * fcolumn[i] + sa;
            rep.rmserror = rep.rmserror + math.sqr(v - yoriginal[i]);
            rep.avgerror = rep.avgerror + Math.Abs(v - yoriginal[i]);
            if ((double)(yoriginal[i]) != (double)(0))
            {
                rep.avgrelerror = rep.avgrelerror + Math.Abs(v - yoriginal[i]) / Math.Abs(yoriginal[i]);
                relcnt = relcnt + 1;
            }
            rep.maxerror = Math.Max(rep.maxerror, Math.Abs(v - yoriginal[i]));
        }
        rep.rmserror = Math.Sqrt(rep.rmserror / n);
        rep.avgerror = rep.avgerror / n;
        if ((double)(relcnt) != (double)(0))
        {
            rep.avgrelerror = rep.avgrelerror / relcnt;
        }
    }


    /*************************************************************************
    Deprecated fitting function with O(N*M^2+M^3) running time. Superseded  by
    spline1dfit().

      -- ALGLIB PROJECT --
         Copyright 18.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dfitcubic(double[] x,
        double[] y,
        int n,
        int m,
        spline1d.spline1dinterpolant s,
        spline1d.spline1dfitreport rep,
        xparams _params)
    {
        lsfit.spline1dfitcubicdeprecated(x, y, n, m, s, rep, _params);
    }


    /*************************************************************************
    Deprecated fitting function with O(N*M^2+M^3) running time. Superseded  by
    spline1dfit().

      -- ALGLIB PROJECT --
         Copyright 18.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dfithermite(double[] x,
        double[] y,
        int n,
        int m,
        spline1d.spline1dinterpolant s,
        spline1d.spline1dfitreport rep,
        xparams _params)
    {
        lsfit.spline1dfithermitedeprecated(x, y, n, m, s, rep, _params);
    }


}
