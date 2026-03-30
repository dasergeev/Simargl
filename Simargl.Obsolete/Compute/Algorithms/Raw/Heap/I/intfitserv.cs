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

public class intfitserv
{
    /*************************************************************************
    Internal subroutine: automatic scaling for LLS tasks.
    NEVER CALL IT DIRECTLY!

    Maps abscissas to [-1,1], standartizes ordinates and correspondingly scales
    constraints. It also scales weights so that max(W[i])=1

    Transformations performed:
    * X, XC         [XA,XB] => [-1,+1]
                    transformation makes min(X)=-1, max(X)=+1

    * Y             [SA,SB] => [0,1]
                    transformation makes mean(Y)=0, stddev(Y)=1
                    
    * YC            transformed accordingly to SA, SB, DC[I]

      -- ALGLIB PROJECT --
         Copyright 08.09.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void lsfitscalexy(ref double[] x,
        ref double[] y,
        ref double[] w,
        int n,
        ref double[] xc,
        ref double[] yc,
        int[] dc,
        int k,
        ref double xa,
        ref double xb,
        ref double sa,
        ref double sb,
        ref double[] xoriginal,
        ref double[] yoriginal,
        xparams _params)
    {
        double xmin = 0;
        double xmax = 0;
        int i = 0;
        double mx = 0;
        int i_ = 0;

        xa = 0;
        xb = 0;
        sa = 0;
        sb = 0;
        xoriginal = new double[0];
        yoriginal = new double[0];

        ap.assert(n >= 1, "LSFitScaleXY: incorrect N");
        ap.assert(k >= 0, "LSFitScaleXY: incorrect K");
        xmin = x[0];
        xmax = x[0];
        for (i = 1; i <= n - 1; i++)
        {
            xmin = Math.Min(xmin, x[i]);
            xmax = Math.Max(xmax, x[i]);
        }
        for (i = 0; i <= k - 1; i++)
        {
            xmin = Math.Min(xmin, xc[i]);
            xmax = Math.Max(xmax, xc[i]);
        }
        if ((double)(xmin) == (double)(xmax))
        {
            if ((double)(xmin) == (double)(0))
            {
                xmin = -1;
                xmax = 1;
            }
            else
            {
                if ((double)(xmin) > (double)(0))
                {
                    xmin = 0.5 * xmin;
                }
                else
                {
                    xmax = 0.5 * xmax;
                }
            }
        }
        xoriginal = new double[n];
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            xoriginal[i_] = x[i_];
        }
        xa = xmin;
        xb = xmax;
        for (i = 0; i <= n - 1; i++)
        {
            x[i] = 2 * (x[i] - 0.5 * (xa + xb)) / (xb - xa);
        }
        for (i = 0; i <= k - 1; i++)
        {
            ap.assert(dc[i] >= 0, "LSFitScaleXY: internal error!");
            xc[i] = 2 * (xc[i] - 0.5 * (xa + xb)) / (xb - xa);
            yc[i] = yc[i] * Math.Pow(0.5 * (xb - xa), dc[i]);
        }
        yoriginal = new double[n];
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            yoriginal[i_] = y[i_];
        }
        sa = 0;
        for (i = 0; i <= n - 1; i++)
        {
            sa = sa + y[i];
        }
        sa = sa / n;
        sb = 0;
        for (i = 0; i <= n - 1; i++)
        {
            sb = sb + math.sqr(y[i] - sa);
        }
        sb = Math.Sqrt(sb / n) + sa;
        if ((double)(sb) == (double)(sa))
        {
            sb = 2 * sa;
        }
        if ((double)(sb) == (double)(sa))
        {
            sb = sa + 1;
        }
        for (i = 0; i <= n - 1; i++)
        {
            y[i] = (y[i] - sa) / (sb - sa);
        }
        for (i = 0; i <= k - 1; i++)
        {
            if (dc[i] == 0)
            {
                yc[i] = (yc[i] - sa) / (sb - sa);
            }
            else
            {
                yc[i] = yc[i] / (sb - sa);
            }
        }
        mx = 0;
        for (i = 0; i <= n - 1; i++)
        {
            mx = Math.Max(mx, Math.Abs(w[i]));
        }
        if ((double)(mx) != (double)(0))
        {
            for (i = 0; i <= n - 1; i++)
            {
                w[i] = w[i] / mx;
            }
        }
    }


    public static void buildpriorterm(double[,] xy,
        int n,
        int nx,
        int ny,
        int modeltype,
        double priorval,
        ref double[,] v,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int j0 = 0;
        int j1 = 0;
        double rj = 0;
        double[,] araw = new double[0, 0];
        double[,] amod = new double[0, 0];
        double[,] braw = new double[0, 0];
        double[] tmp0 = new double[0];
        double lambdareg = 0;
        int rfsits = 0;

        v = new double[0, 0];

        ap.assert(n >= 0, "BuildPriorTerm: N<0");
        ap.assert(nx > 0, "BuildPriorTerm: NX<=0");
        ap.assert(ny > 0, "BuildPriorTerm: NY<=0");
        v = new double[ny, nx + 1];
        for (i = 0; i <= ap.rows(v) - 1; i++)
        {
            for (j = 0; j <= ap.cols(v) - 1; j++)
            {
                v[i, j] = 0;
            }
        }
        if (n == 0)
        {
            if (modeltype == 0)
            {
                for (i = 0; i <= ny - 1; i++)
                {
                    v[i, nx] = priorval;
                }
                return;
            }
            if (modeltype == 1)
            {
                return;
            }
            if (modeltype == 2)
            {
                return;
            }
            if (modeltype == 3)
            {
                return;
            }
            ap.assert(false, "BuildPriorTerm: unexpected model type");
        }
        if (modeltype == 0)
        {
            for (i = 0; i <= ny - 1; i++)
            {
                v[i, nx] = priorval;
            }
            for (i = 0; i <= n - 1; i++)
            {
                for (j = 0; j <= ny - 1; j++)
                {
                    xy[i, nx + j] = xy[i, nx + j] - priorval;
                }
            }
            return;
        }
        if (modeltype == 2)
        {
            for (i = 0; i <= n - 1; i++)
            {
                for (j = 0; j <= ny - 1; j++)
                {
                    v[j, nx] = v[j, nx] + xy[i, nx + j];
                }
            }
            for (j = 0; j <= ny - 1; j++)
            {
                v[j, nx] = v[j, nx] / apserv.coalesce(n, 1, _params);
            }
            for (i = 0; i <= n - 1; i++)
            {
                for (j = 0; j <= ny - 1; j++)
                {
                    xy[i, nx + j] = xy[i, nx + j] - v[j, nx];
                }
            }
            return;
        }
        if (modeltype == 3)
        {
            return;
        }
        ap.assert(modeltype == 1, "BuildPriorTerm: unexpected model type");
        lambdareg = 0.0;
        araw = new double[nx + 1, nx + 1];
        braw = new double[nx + 1, ny];
        tmp0 = new double[nx + 1];
        amod = new double[nx + 1, nx + 1];
        for (i = 0; i <= nx; i++)
        {
            for (j = 0; j <= nx; j++)
            {
                araw[i, j] = 0;
            }
        }
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= nx - 1; j++)
            {
                tmp0[j] = xy[i, j];
            }
            tmp0[nx] = 1.0;
            for (j0 = 0; j0 <= nx; j0++)
            {
                for (j1 = 0; j1 <= nx; j1++)
                {
                    araw[j0, j1] = araw[j0, j1] + tmp0[j0] * tmp0[j1];
                }
            }
        }
        for (rfsits = 1; rfsits <= 3; rfsits++)
        {
            for (i = 0; i <= nx; i++)
            {
                for (j = 0; j <= ny - 1; j++)
                {
                    braw[i, j] = 0;
                }
            }
            for (i = 0; i <= n - 1; i++)
            {
                for (j = 0; j <= nx - 1; j++)
                {
                    tmp0[j] = xy[i, j];
                }
                tmp0[nx] = 1.0;
                for (j = 0; j <= ny - 1; j++)
                {
                    rj = xy[i, nx + j];
                    for (j0 = 0; j0 <= nx; j0++)
                    {
                        rj = rj - tmp0[j0] * v[j, j0];
                    }
                    for (j0 = 0; j0 <= nx; j0++)
                    {
                        braw[j0, j] = braw[j0, j] + rj * tmp0[j0];
                    }
                }
            }
            while (true)
            {
                for (i = 0; i <= nx; i++)
                {
                    for (j = 0; j <= nx; j++)
                    {
                        amod[i, j] = araw[i, j];
                    }
                    amod[i, i] = amod[i, i] + lambdareg * apserv.coalesce(amod[i, i], 1, _params);
                }
                if (trfac.spdmatrixcholesky(amod, nx + 1, true, _params))
                {
                    break;
                }
                lambdareg = apserv.coalesce(10 * lambdareg, 1.0E-12, _params);
            }
            ablas.rmatrixlefttrsm(nx + 1, ny, amod, 0, 0, true, false, 1, braw, 0, 0, _params);
            ablas.rmatrixlefttrsm(nx + 1, ny, amod, 0, 0, true, false, 0, braw, 0, 0, _params);
            for (i = 0; i <= nx; i++)
            {
                for (j = 0; j <= ny - 1; j++)
                {
                    v[j, i] = v[j, i] + braw[i, j];
                }
            }
        }
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= nx - 1; j++)
            {
                tmp0[j] = xy[i, j];
            }
            tmp0[nx] = 1.0;
            for (j = 0; j <= ny - 1; j++)
            {
                rj = 0.0;
                for (j0 = 0; j0 <= nx; j0++)
                {
                    rj = rj + tmp0[j0] * v[j, j0];
                }
                xy[i, nx + j] = xy[i, nx + j] - rj;
            }
        }
    }


    public static void buildpriorterm1(double[] xy1,
        int n,
        int nx,
        int ny,
        int modeltype,
        double priorval,
        ref double[,] v,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int j0 = 0;
        int j1 = 0;
        int ew = 0;
        double rj = 0;
        double[,] araw = new double[0, 0];
        double[,] amod = new double[0, 0];
        double[,] braw = new double[0, 0];
        double[] tmp0 = new double[0];
        double lambdareg = 0;
        int rfsits = 0;

        v = new double[0, 0];

        ap.assert(n >= 0, "BuildPriorTerm: N<0");
        ap.assert(nx > 0, "BuildPriorTerm: NX<=0");
        ap.assert(ny > 0, "BuildPriorTerm: NY<=0");
        ew = nx + ny;
        v = new double[ny, nx + 1];
        for (i = 0; i <= ap.rows(v) - 1; i++)
        {
            for (j = 0; j <= ap.cols(v) - 1; j++)
            {
                v[i, j] = 0;
            }
        }
        if (n == 0)
        {
            if (modeltype == 0)
            {
                for (i = 0; i <= ny - 1; i++)
                {
                    v[i, nx] = priorval;
                }
                return;
            }
            if (modeltype == 1)
            {
                return;
            }
            if (modeltype == 2)
            {
                return;
            }
            if (modeltype == 3)
            {
                return;
            }
            ap.assert(false, "BuildPriorTerm: unexpected model type");
        }
        if (modeltype == 0)
        {
            for (i = 0; i <= ny - 1; i++)
            {
                v[i, nx] = priorval;
            }
            for (i = 0; i <= n - 1; i++)
            {
                for (j = 0; j <= ny - 1; j++)
                {
                    xy1[i * ew + nx + j] = xy1[i * ew + nx + j] - priorval;
                }
            }
            return;
        }
        if (modeltype == 2)
        {
            for (i = 0; i <= n - 1; i++)
            {
                for (j = 0; j <= ny - 1; j++)
                {
                    v[j, nx] = v[j, nx] + xy1[i * ew + nx + j];
                }
            }
            for (j = 0; j <= ny - 1; j++)
            {
                v[j, nx] = v[j, nx] / apserv.coalesce(n, 1, _params);
            }
            for (i = 0; i <= n - 1; i++)
            {
                for (j = 0; j <= ny - 1; j++)
                {
                    xy1[i * ew + nx + j] = xy1[i * ew + nx + j] - v[j, nx];
                }
            }
            return;
        }
        if (modeltype == 3)
        {
            return;
        }
        ap.assert(modeltype == 1, "BuildPriorTerm: unexpected model type");
        lambdareg = 0.0;
        araw = new double[nx + 1, nx + 1];
        braw = new double[nx + 1, ny];
        tmp0 = new double[nx + 1];
        amod = new double[nx + 1, nx + 1];
        for (i = 0; i <= nx; i++)
        {
            for (j = 0; j <= nx; j++)
            {
                araw[i, j] = 0;
            }
        }
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= nx - 1; j++)
            {
                tmp0[j] = xy1[i * ew + j];
            }
            tmp0[nx] = 1.0;
            for (j0 = 0; j0 <= nx; j0++)
            {
                for (j1 = 0; j1 <= nx; j1++)
                {
                    araw[j0, j1] = araw[j0, j1] + tmp0[j0] * tmp0[j1];
                }
            }
        }
        for (rfsits = 1; rfsits <= 1; rfsits++)
        {
            for (i = 0; i <= nx; i++)
            {
                for (j = 0; j <= ny - 1; j++)
                {
                    braw[i, j] = 0;
                }
            }
            for (i = 0; i <= n - 1; i++)
            {
                for (j = 0; j <= nx - 1; j++)
                {
                    tmp0[j] = xy1[i * ew + j];
                }
                tmp0[nx] = 1.0;
                for (j = 0; j <= ny - 1; j++)
                {
                    rj = xy1[i * ew + nx + j];
                    for (j0 = 0; j0 <= nx; j0++)
                    {
                        rj = rj - tmp0[j0] * v[j, j0];
                    }
                    for (j0 = 0; j0 <= nx; j0++)
                    {
                        braw[j0, j] = braw[j0, j] + rj * tmp0[j0];
                    }
                }
            }
            while (true)
            {
                for (i = 0; i <= nx; i++)
                {
                    for (j = 0; j <= nx; j++)
                    {
                        amod[i, j] = araw[i, j];
                    }
                    amod[i, i] = amod[i, i] + lambdareg * apserv.coalesce(amod[i, i], 1, _params);
                }
                if (trfac.spdmatrixcholesky(amod, nx + 1, true, _params))
                {
                    break;
                }
                lambdareg = apserv.coalesce(10 * lambdareg, 1.0E-12, _params);
            }
            ablas.rmatrixlefttrsm(nx + 1, ny, amod, 0, 0, true, false, 1, braw, 0, 0, _params);
            ablas.rmatrixlefttrsm(nx + 1, ny, amod, 0, 0, true, false, 0, braw, 0, 0, _params);
            for (i = 0; i <= nx; i++)
            {
                for (j = 0; j <= ny - 1; j++)
                {
                    v[j, i] = v[j, i] + braw[i, j];
                }
            }
        }
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= nx - 1; j++)
            {
                tmp0[j] = xy1[i * ew + j];
            }
            tmp0[nx] = 1.0;
            for (j = 0; j <= ny - 1; j++)
            {
                rj = 0.0;
                for (j0 = 0; j0 <= nx; j0++)
                {
                    rj = rj + tmp0[j0] * v[j, j0];
                }
                xy1[i * ew + nx + j] = xy1[i * ew + nx + j] - rj;
            }
        }
    }


}
