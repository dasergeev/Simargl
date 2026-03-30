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

public class lda
{
    /*************************************************************************
    Multiclass Fisher LDA

    The function finds coefficients of a linear  combination  which  optimally
    separates training set. Most suited for 2-class problems, see fisherldan()
    for an variant that returns N-dimensional basis.

    INPUT PARAMETERS:
        XY          -   training set, array[NPoints,NVars+1].
                        First NVars columns store values of independent
                        variables, the next column stores class index (from 0
                        to NClasses-1) which dataset element belongs to.
                        Fractional values are rounded to the nearest integer.
                        The class index must be in the [0,NClasses-1] range,
                        an exception is generated otherwise.
        NPoints     -   training set size, NPoints>=0
        NVars       -   number of independent variables, NVars>=1
        NClasses    -   number of classes, NClasses>=2


    OUTPUT PARAMETERS:
        W           -   linear combination coefficients, array[NVars]

      ! FREE EDITION OF ALGLIB:
      ! 
      ! Free Edition of ALGLIB supports following important features for  this
      ! function:
      ! * C++ version: x64 SIMD support using C++ intrinsics
      ! * C#  version: x64 SIMD support using NET5/NetCore hardware intrinsics
      !
      ! We  recommend  you  to  read  'Compiling ALGLIB' section of the ALGLIB
      ! Reference Manual in order  to  find  out  how to activate SIMD support
      ! in ALGLIB.

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

      -- ALGLIB --
         Copyright 31.05.2008 by Bochkanov Sergey
    *************************************************************************/
    public static void fisherlda(double[,] xy,
        int npoints,
        int nvars,
        int nclasses,
        ref double[] w,
        xparams _params)
    {
        double[,] w2 = new double[0, 0];
        int i_ = 0;

        w = new double[0];

        fisherldan(xy, npoints, nvars, nclasses, ref w2, _params);
        w = new double[nvars];
        for (i_ = 0; i_ <= nvars - 1; i_++)
        {
            w[i_] = w2[i_, 0];
        }
    }


    /*************************************************************************
    N-dimensional multiclass Fisher LDA

    Subroutine finds coefficients of linear combinations which optimally separates
    training set on classes. It returns N-dimensional basis whose vector are sorted
    by quality of training set separation (in descending order).

    INPUT PARAMETERS:
        XY          -   training set, array[NPoints,NVars+1].
                        First NVars columns store values of independent
                        variables, the next column stores class index (from 0
                        to NClasses-1) which dataset element belongs to.
                        Fractional values are rounded to the nearest integer.
                        The class index must be in the [0,NClasses-1] range,
                        an exception is generated otherwise.
        NPoints     -   training set size, NPoints>=0
        NVars       -   number of independent variables, NVars>=1
        NClasses    -   number of classes, NClasses>=2


    OUTPUT PARAMETERS:
        W           -   basis, array[NVars,NVars]
                        columns of matrix stores basis vectors, sorted by
                        quality of training set separation (in descending order)

      ! FREE EDITION OF ALGLIB:
      ! 
      ! Free Edition of ALGLIB supports following important features for  this
      ! function:
      ! * C++ version: x64 SIMD support using C++ intrinsics
      ! * C#  version: x64 SIMD support using NET5/NetCore hardware intrinsics
      !
      ! We  recommend  you  to  read  'Compiling ALGLIB' section of the ALGLIB
      ! Reference Manual in order  to  find  out  how to activate SIMD support
      ! in ALGLIB.

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

      -- ALGLIB --
         Copyright 31.05.2008 by Bochkanov Sergey
    *************************************************************************/
    public static void fisherldan(double[,] xy,
        int npoints,
        int nvars,
        int nclasses,
        ref double[,] w,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        int m = 0;
        double v = 0;
        int[] c = new int[0];
        double[] mu = new double[0];
        double[,] muc = new double[0, 0];
        int[] nc = new int[0];
        double[,] sw = new double[0, 0];
        double[,] st = new double[0, 0];
        double[,] z = new double[0, 0];
        double[,] z2 = new double[0, 0];
        double[,] tm = new double[0, 0];
        double[,] sbroot = new double[0, 0];
        double[,] a = new double[0, 0];
        double[,] xyc = new double[0, 0];
        double[,] xyproj = new double[0, 0];
        double[,] wproj = new double[0, 0];
        double[] tf = new double[0];
        double[] d = new double[0];
        double[] d2 = new double[0];
        double[] work = new double[0];
        int i_ = 0;

        w = new double[0, 0];


        //
        // Test data
        //
        ap.assert(!(npoints < 0), "FisherLDAN: NPoints<0");
        ap.assert(!(nvars < 1), "FisherLDAN: NVars<1");
        ap.assert(!(nclasses < 2), "FisherLDAN: NClasses<2");
        for (i = 0; i <= npoints - 1; i++)
        {
            if ((int)Math.Round(xy[i, nvars]) < 0 || (int)Math.Round(xy[i, nvars]) >= nclasses)
            {
                ap.assert(false, "FisherLDAN: class index is <0 or >NClasses-1");
            }
        }

        //
        // Special case: NPoints<=1
        // Degenerate task.
        //
        if (npoints <= 1)
        {
            w = new double[nvars, nvars];
            for (i = 0; i <= nvars - 1; i++)
            {
                for (j = 0; j <= nvars - 1; j++)
                {
                    if (i == j)
                    {
                        w[i, j] = 1;
                    }
                    else
                    {
                        w[i, j] = 0;
                    }
                }
            }
            return;
        }

        //
        // Prepare temporaries
        //
        tf = new double[nvars];
        work = new double[Math.Max(nvars, npoints) + 1];
        xyc = new double[npoints, nvars];

        //
        // Convert class labels from reals to integers (just for convenience)
        //
        c = new int[npoints];
        for (i = 0; i <= npoints - 1; i++)
        {
            c[i] = (int)Math.Round(xy[i, nvars]);
        }

        //
        // Calculate class sizes, class means
        //
        mu = new double[nvars];
        muc = new double[nclasses, nvars];
        nc = new int[nclasses];
        for (j = 0; j <= nvars - 1; j++)
        {
            mu[j] = 0;
        }
        for (i = 0; i <= nclasses - 1; i++)
        {
            nc[i] = 0;
            for (j = 0; j <= nvars - 1; j++)
            {
                muc[i, j] = 0;
            }
        }
        for (i = 0; i <= npoints - 1; i++)
        {
            for (i_ = 0; i_ <= nvars - 1; i_++)
            {
                mu[i_] = mu[i_] + xy[i, i_];
            }
            for (i_ = 0; i_ <= nvars - 1; i_++)
            {
                muc[c[i], i_] = muc[c[i], i_] + xy[i, i_];
            }
            nc[c[i]] = nc[c[i]] + 1;
        }
        for (i = 0; i <= nclasses - 1; i++)
        {
            v = (double)1 / (double)nc[i];
            for (i_ = 0; i_ <= nvars - 1; i_++)
            {
                muc[i, i_] = v * muc[i, i_];
            }
        }
        v = (double)1 / (double)npoints;
        for (i_ = 0; i_ <= nvars - 1; i_++)
        {
            mu[i_] = v * mu[i_];
        }

        //
        // Create ST matrix
        //
        st = new double[nvars, nvars];
        for (i = 0; i <= nvars - 1; i++)
        {
            for (j = 0; j <= nvars - 1; j++)
            {
                st[i, j] = 0;
            }
        }
        for (k = 0; k <= npoints - 1; k++)
        {
            for (i_ = 0; i_ <= nvars - 1; i_++)
            {
                xyc[k, i_] = xy[k, i_];
            }
            for (i_ = 0; i_ <= nvars - 1; i_++)
            {
                xyc[k, i_] = xyc[k, i_] - mu[i_];
            }
        }
        ablas.rmatrixgemm(nvars, nvars, npoints, 1.0, xyc, 0, 0, 1, xyc, 0, 0, 0, 0.0, st, 0, 0, _params);

        //
        // Create SW matrix
        //
        sw = new double[nvars, nvars];
        for (i = 0; i <= nvars - 1; i++)
        {
            for (j = 0; j <= nvars - 1; j++)
            {
                sw[i, j] = 0;
            }
        }
        for (k = 0; k <= npoints - 1; k++)
        {
            for (i_ = 0; i_ <= nvars - 1; i_++)
            {
                xyc[k, i_] = xy[k, i_];
            }
            for (i_ = 0; i_ <= nvars - 1; i_++)
            {
                xyc[k, i_] = xyc[k, i_] - muc[c[k], i_];
            }
        }
        ablas.rmatrixgemm(nvars, nvars, npoints, 1.0, xyc, 0, 0, 1, xyc, 0, 0, 0, 0.0, sw, 0, 0, _params);

        //
        // Maximize ratio J=(w'*ST*w)/(w'*SW*w).
        //
        // First, make transition from w to v such that w'*ST*w becomes v'*v:
        //    v  = root(ST)*w = R*w
        //    R  = root(D)*Z'
        //    w  = (root(ST)^-1)*v = RI*v
        //    RI = Z*inv(root(D))
        //    J  = (v'*v)/(v'*(RI'*SW*RI)*v)
        //    ST = Z*D*Z'
        //
        //    so we have
        //
        //    J = (v'*v) / (v'*(inv(root(D))*Z'*SW*Z*inv(root(D)))*v)  =
        //      = (v'*v) / (v'*A*v)
        //
        if (!evd.smatrixevd(st, nvars, 1, true, ref d, ref z, _params))
        {
            ap.assert(false, "FisherLDAN: EVD solver failure");
        }
        w = new double[nvars, nvars];
        if ((double)(d[nvars - 1]) <= (double)(0) || (double)(d[0]) <= (double)(1000 * math.machineepsilon * d[nvars - 1]))
        {

            //
            // Special case: D[NVars-1]<=0
            // Degenerate task (all variables takes the same value).
            //
            if ((double)(d[nvars - 1]) <= (double)(0))
            {
                for (i = 0; i <= nvars - 1; i++)
                {
                    for (j = 0; j <= nvars - 1; j++)
                    {
                        if (i == j)
                        {
                            w[i, j] = 1;
                        }
                        else
                        {
                            w[i, j] = 0;
                        }
                    }
                }
                return;
            }

            //
            // Special case: degenerate ST matrix, multicollinearity found.
            // Since we know ST eigenvalues/vectors we can translate task to
            // non-degenerate form.
            //
            // Let WG is orthogonal basis of the non zero variance subspace
            // of the ST and let WZ is orthogonal basis of the zero variance
            // subspace.
            //
            // Projection on WG allows us to use LDA on reduced M-dimensional
            // subspace, N-M vectors of WZ allows us to update reduced LDA
            // factors to full N-dimensional subspace.
            //
            m = 0;
            for (k = 0; k <= nvars - 1; k++)
            {
                if ((double)(d[k]) <= (double)(1000 * math.machineepsilon * d[nvars - 1]))
                {
                    m = k + 1;
                }
            }
            ap.assert(m != 0, "FisherLDAN: internal error #1");
            xyproj = new double[npoints, nvars - m + 1];
            ablas.rmatrixgemm(npoints, nvars - m, nvars, 1.0, xy, 0, 0, 0, z, 0, m, 0, 0.0, xyproj, 0, 0, _params);
            for (i = 0; i <= npoints - 1; i++)
            {
                xyproj[i, nvars - m] = xy[i, nvars];
            }
            fisherldan(xyproj, npoints, nvars - m, nclasses, ref wproj, _params);
            ablas.rmatrixgemm(nvars, nvars - m, nvars - m, 1.0, z, 0, m, 0, wproj, 0, 0, 0, 0.0, w, 0, 0, _params);
            for (k = nvars - m; k <= nvars - 1; k++)
            {
                for (i_ = 0; i_ <= nvars - 1; i_++)
                {
                    w[i_, k] = z[i_, k - (nvars - m)];
                }
            }
        }
        else
        {

            //
            // General case: no multicollinearity
            //
            tm = new double[nvars, nvars];
            a = new double[nvars, nvars];
            ablas.rmatrixgemm(nvars, nvars, nvars, 1.0, sw, 0, 0, 0, z, 0, 0, 0, 0.0, tm, 0, 0, _params);
            ablas.rmatrixgemm(nvars, nvars, nvars, 1.0, z, 0, 0, 1, tm, 0, 0, 0, 0.0, a, 0, 0, _params);
            for (i = 0; i <= nvars - 1; i++)
            {
                for (j = 0; j <= nvars - 1; j++)
                {
                    a[i, j] = a[i, j] / Math.Sqrt(d[i] * d[j]);
                }
            }
            if (!evd.smatrixevd(a, nvars, 1, true, ref d2, ref z2, _params))
            {
                ap.assert(false, "FisherLDAN: EVD solver failure");
            }
            for (i = 0; i <= nvars - 1; i++)
            {
                for (k = 0; k <= nvars - 1; k++)
                {
                    z2[i, k] = z2[i, k] / Math.Sqrt(d[i]);
                }
            }
            ablas.rmatrixgemm(nvars, nvars, nvars, 1.0, z, 0, 0, 0, z2, 0, 0, 0, 0.0, w, 0, 0, _params);
        }

        //
        // Post-processing:
        // * normalization
        // * converting to non-negative form, if possible
        //
        for (k = 0; k <= nvars - 1; k++)
        {
            v = 0.0;
            for (i_ = 0; i_ <= nvars - 1; i_++)
            {
                v += w[i_, k] * w[i_, k];
            }
            v = 1 / Math.Sqrt(v);
            for (i_ = 0; i_ <= nvars - 1; i_++)
            {
                w[i_, k] = v * w[i_, k];
            }
            v = 0;
            for (i = 0; i <= nvars - 1; i++)
            {
                v = v + w[i, k];
            }
            if ((double)(v) < (double)(0))
            {
                for (i_ = 0; i_ <= nvars - 1; i_++)
                {
                    w[i_, k] = -1 * w[i_, k];
                }
            }
        }
    }


}
