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





public class pca
{
    /*************************************************************************
    Principal components analysis

    This function builds orthogonal basis  where  first  axis  corresponds  to
    direction with maximum variance, second axis  maximizes  variance  in  the
    subspace orthogonal to first axis and so on.

    This function builds FULL basis, i.e. returns N vectors  corresponding  to
    ALL directions, no matter how informative. If you need  just a  few  (say,
    10 or 50) of the most important directions, you may find it faster to  use
    one of the reduced versions:
    * pcatruncatedsubspace() - for subspace iteration based method

    It should be noted that, unlike LDA, PCA does not use class labels.

    INPUT PARAMETERS:
        X           -   dataset, array[NPoints,NVars].
                        matrix contains ONLY INDEPENDENT VARIABLES.
        NPoints     -   dataset size, NPoints>=0
        NVars       -   number of independent variables, NVars>=1

    OUTPUT PARAMETERS:
        S2          -   array[NVars]. variance values corresponding
                        to basis vectors.
        V           -   array[NVars,NVars]
                        matrix, whose columns store basis vectors.

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
         Copyright 25.08.2008 by Bochkanov Sergey
    *************************************************************************/
    public static void pcabuildbasis(double[,] x,
        int npoints,
        int nvars,
        ref double[] s2,
        ref double[,] v,
        xparams _params)
    {
        double[,] a = new double[0, 0];
        double[,] u = new double[0, 0];
        double[,] vt = new double[0, 0];
        double[] m = new double[0];
        double[] t = new double[0];
        int i = 0;
        int j = 0;
        double mean = 0;
        double variance = 0;
        double skewness = 0;
        double kurtosis = 0;
        int i_ = 0;

        s2 = new double[0];
        v = new double[0, 0];


        //
        // Check input data
        //
        ap.assert(npoints >= 0, "PCABuildBasis: NPoints<0");
        ap.assert(nvars >= 1, "PCABuildBasis: NVars<1");
        ap.assert(ap.rows(x) >= npoints, "PCABuildBasis: rows(X)<NPoints");
        ap.assert(ap.cols(x) >= nvars || npoints == 0, "PCABuildBasis: cols(X)<NVars");
        ap.assert(apserv.apservisfinitematrix(x, npoints, nvars, _params), "PCABuildBasis: X contains INF/NAN");

        //
        // Special case: NPoints=0
        //
        if (npoints == 0)
        {
            s2 = new double[nvars];
            v = new double[nvars, nvars];
            for (i = 0; i <= nvars - 1; i++)
            {
                s2[i] = 0;
            }
            for (i = 0; i <= nvars - 1; i++)
            {
                for (j = 0; j <= nvars - 1; j++)
                {
                    if (i == j)
                    {
                        v[i, j] = 1;
                    }
                    else
                    {
                        v[i, j] = 0;
                    }
                }
            }
            return;
        }

        //
        // Calculate means
        //
        m = new double[nvars];
        t = new double[npoints];
        for (j = 0; j <= nvars - 1; j++)
        {
            for (i_ = 0; i_ <= npoints - 1; i_++)
            {
                t[i_] = x[i_, j];
            }
            basestat.samplemoments(t, npoints, ref mean, ref variance, ref skewness, ref kurtosis, _params);
            m[j] = mean;
        }

        //
        // Center, apply SVD, prepare output
        //
        a = new double[Math.Max(npoints, nvars), nvars];
        for (i = 0; i <= npoints - 1; i++)
        {
            for (i_ = 0; i_ <= nvars - 1; i_++)
            {
                a[i, i_] = x[i, i_];
            }
            for (i_ = 0; i_ <= nvars - 1; i_++)
            {
                a[i, i_] = a[i, i_] - m[i_];
            }
        }
        for (i = npoints; i <= nvars - 1; i++)
        {
            for (j = 0; j <= nvars - 1; j++)
            {
                a[i, j] = 0;
            }
        }
        if (!svd.rmatrixsvd(a, Math.Max(npoints, nvars), nvars, 0, 1, 2, ref s2, ref u, ref vt, _params))
        {
            ap.assert(false, "PCABuildBasis: internal SVD solver failure");
            return;
        }
        if (npoints != 1)
        {
            for (i = 0; i <= nvars - 1; i++)
            {
                s2[i] = math.sqr(s2[i]) / (npoints - 1);
            }
        }
        v = new double[nvars, nvars];
        RawBlas.copyandtranspose(vt, 0, nvars - 1, 0, nvars - 1, ref v, 0, nvars - 1, 0, nvars - 1, _params);
    }


    /*************************************************************************
    Principal components analysis

    This function performs truncated PCA, i.e. returns just a few most important
    directions.

    Internally it uses iterative eigensolver which is very efficient when only
    a minor fraction of full basis is required. Thus, if you need full  basis,
    it is better to use pcabuildbasis() function.

    It should be noted that, unlike LDA, PCA does not use class labels.

    INPUT PARAMETERS:
        X           -   dataset, array[0..NPoints-1,0..NVars-1].
                        matrix contains ONLY INDEPENDENT VARIABLES.
        NPoints     -   dataset size, NPoints>=0
        NVars       -   number of independent variables, NVars>=1
        NNeeded     -   number of requested components, in [1,NVars] range;
                        this function is efficient only for NNeeded<<NVars.
        Eps         -   desired  precision  of  vectors  returned;  underlying
                        solver will stop iterations as soon as absolute  error
                        in corresponding singular values  reduces  to  roughly
                        eps*MAX(lambda[]), with lambda[] being array of  eigen
                        values.
                        Zero value means that  algorithm  performs  number  of
                        iterations  specified  by  maxits  parameter,  without
                        paying attention to precision.
        MaxIts      -   number of iterations performed by  subspace  iteration
                        method. Zero value means that no  limit  on  iteration
                        count is placed (eps-based stopping condition is used).
                        

    OUTPUT PARAMETERS:
        S2          -   array[NNeeded]. Variance values corresponding
                        to basis vectors.
        V           -   array[NVars,NNeeded]
                        matrix, whose columns store basis vectors.
                        
    NOTE: passing eps=0 and maxits=0 results in small eps  being  selected  as
    stopping condition. Exact value of automatically selected eps is  version-
    -dependent.

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
         Copyright 10.01.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void pcatruncatedsubspace(double[,] x,
        int npoints,
        int nvars,
        int nneeded,
        double eps,
        int maxits,
        ref double[] s2,
        ref double[,] v,
        xparams _params)
    {
        double[,] a = new double[0, 0];
        double[,] b = new double[0, 0];
        double[] means = new double[0];
        int i = 0;
        int j = 0;
        int k = 0;
        double vv = 0;
        evd.eigsubspacestate solver = new evd.eigsubspacestate();
        evd.eigsubspacereport rep = new evd.eigsubspacereport();
        int i_ = 0;

        s2 = new double[0];
        v = new double[0, 0];

        ap.assert(npoints >= 0, "PCATruncatedSubspace: npoints<0");
        ap.assert(nvars >= 1, "PCATruncatedSubspace: nvars<1");
        ap.assert(nneeded > 0, "PCATruncatedSubspace: nneeded<1");
        ap.assert(nneeded <= nvars, "PCATruncatedSubspace: nneeded>nvars");
        ap.assert(maxits >= 0, "PCATruncatedSubspace: maxits<0");
        ap.assert(math.isfinite(eps) && (double)(eps) >= (double)(0), "PCATruncatedSubspace: eps<0 or is not finite");
        ap.assert(ap.rows(x) >= npoints, "PCATruncatedSubspace: rows(x)<npoints");
        ap.assert(ap.cols(x) >= nvars || npoints == 0, "PCATruncatedSubspace: cols(x)<nvars");
        ap.assert(apserv.apservisfinitematrix(x, npoints, nvars, _params), "PCATruncatedSubspace: X contains INF/NAN");

        //
        // Special case: NPoints=0
        //
        if (npoints == 0)
        {
            s2 = new double[nneeded];
            v = new double[nvars, nneeded];
            for (i = 0; i <= nvars - 1; i++)
            {
                s2[i] = 0;
            }
            for (i = 0; i <= nvars - 1; i++)
            {
                for (j = 0; j <= nneeded - 1; j++)
                {
                    if (i == j)
                    {
                        v[i, j] = 1;
                    }
                    else
                    {
                        v[i, j] = 0;
                    }
                }
            }
            return;
        }

        //
        // Center matrix
        //
        means = new double[nvars];
        for (i = 0; i <= nvars - 1; i++)
        {
            means[i] = 0;
        }
        vv = (double)1 / (double)npoints;
        for (i = 0; i <= npoints - 1; i++)
        {
            for (i_ = 0; i_ <= nvars - 1; i_++)
            {
                means[i_] = means[i_] + vv * x[i, i_];
            }
        }
        a = new double[npoints, nvars];
        for (i = 0; i <= npoints - 1; i++)
        {
            for (i_ = 0; i_ <= nvars - 1; i_++)
            {
                a[i, i_] = x[i, i_];
            }
            for (i_ = 0; i_ <= nvars - 1; i_++)
            {
                a[i, i_] = a[i, i_] - means[i_];
            }
        }

        //
        // Find eigenvalues with subspace iteration solver
        //
        evd.eigsubspacecreate(nvars, nneeded, solver, _params);
        evd.eigsubspacesetcond(solver, eps, maxits, _params);
        evd.eigsubspaceoocstart(solver, 0, _params);
        while (evd.eigsubspaceooccontinue(solver, _params))
        {
            ap.assert(solver.requesttype == 0, "PCATruncatedSubspace: integrity check failed");
            k = solver.requestsize;
            apserv.rmatrixsetlengthatleast(ref b, npoints, k, _params);
            ablas.rmatrixgemm(npoints, k, nvars, 1.0, a, 0, 0, 0, solver.x, 0, 0, 0, 0.0, b, 0, 0, _params);
            ablas.rmatrixgemm(nvars, k, npoints, 1.0, a, 0, 0, 1, b, 0, 0, 0, 0.0, solver.ax, 0, 0, _params);
        }
        evd.eigsubspaceoocstop(solver, ref s2, ref v, rep, _params);
        if (npoints != 1)
        {
            for (i = 0; i <= nneeded - 1; i++)
            {
                s2[i] = s2[i] / (npoints - 1);
            }
        }
    }


    /*************************************************************************
    Sparse truncated principal components analysis

    This function performs sparse truncated PCA, i.e. returns just a few  most
    important principal components for a sparse input X.

    Internally it uses iterative eigensolver which is very efficient when only
    a minor fraction of full basis is required.

    It should be noted that, unlike LDA, PCA does not use class labels.

    INPUT PARAMETERS:
        X           -   sparse dataset, sparse  npoints*nvars  matrix.  It  is
                        recommended to use CRS sparse storage format;  non-CRS
                        input will be internally converted to CRS.
                        Matrix contains ONLY INDEPENDENT VARIABLES,  and  must
                        be EXACTLY npoints*nvars.
        NPoints     -   dataset size, NPoints>=0
        NVars       -   number of independent variables, NVars>=1
        NNeeded     -   number of requested components, in [1,NVars] range;
                        this function is efficient only for NNeeded<<NVars.
        Eps         -   desired  precision  of  vectors  returned;  underlying
                        solver will stop iterations as soon as absolute  error
                        in corresponding singular values  reduces  to  roughly
                        eps*MAX(lambda[]), with lambda[] being array of  eigen
                        values.
                        Zero value means that  algorithm  performs  number  of
                        iterations  specified  by  maxits  parameter,  without
                        paying attention to precision.
        MaxIts      -   number of iterations performed by  subspace  iteration
                        method. Zero value means that no  limit  on  iteration
                        count is placed (eps-based stopping condition is used).
                        

    OUTPUT PARAMETERS:
        S2          -   array[NNeeded]. Variance values corresponding
                        to basis vectors.
        V           -   array[NVars,NNeeded]
                        matrix, whose columns store basis vectors.
                        
    NOTE: passing eps=0 and maxits=0 results in small eps  being  selected  as
          a stopping condition. Exact value of automatically selected  eps  is
          version-dependent.

    NOTE: zero  MaxIts  is  silently  replaced  by some reasonable value which
          prevents eternal loops (possible when inputs are degenerate and  too
          stringent stopping criteria are specified). In  current  version  it
          is 50+2*NVars.

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
         Copyright 10.01.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void pcatruncatedsubspacesparse(sparse.sparsematrix x,
        int npoints,
        int nvars,
        int nneeded,
        double eps,
        int maxits,
        ref double[] s2,
        ref double[,] v,
        xparams _params)
    {
        sparse.sparsematrix xcrs = new sparse.sparsematrix();
        double[] b1 = new double[0];
        double[] c1 = new double[0];
        double[] z1 = new double[0];
        int i = 0;
        int j = 0;
        int k = 0;
        double vv = 0;
        double[] means = new double[0];
        evd.eigsubspacestate solver = new evd.eigsubspacestate();
        evd.eigsubspacereport rep = new evd.eigsubspacereport();
        int i_ = 0;

        s2 = new double[0];
        v = new double[0, 0];

        ap.assert(npoints >= 0, "PCATruncatedSubspaceSparse: npoints<0");
        ap.assert(nvars >= 1, "PCATruncatedSubspaceSparse: nvars<1");
        ap.assert(nneeded > 0, "PCATruncatedSubspaceSparse: nneeded<1");
        ap.assert(nneeded <= nvars, "PCATruncatedSubspaceSparse: nneeded>nvars");
        ap.assert(maxits >= 0, "PCATruncatedSubspaceSparse: maxits<0");
        ap.assert(math.isfinite(eps) && (double)(eps) >= (double)(0), "PCATruncatedSubspaceSparse: eps<0 or is not finite");
        if (npoints > 0)
        {
            ap.assert(sparse.sparsegetnrows(x, _params) == npoints, "PCATruncatedSubspaceSparse: rows(x)!=npoints");
            ap.assert(sparse.sparsegetncols(x, _params) == nvars, "PCATruncatedSubspaceSparse: cols(x)!=nvars");
        }

        //
        // Special case: NPoints=0
        //
        if (npoints == 0)
        {
            s2 = new double[nneeded];
            v = new double[nvars, nneeded];
            for (i = 0; i <= nvars - 1; i++)
            {
                s2[i] = 0;
            }
            for (i = 0; i <= nvars - 1; i++)
            {
                for (j = 0; j <= nneeded - 1; j++)
                {
                    if (i == j)
                    {
                        v[i, j] = 1;
                    }
                    else
                    {
                        v[i, j] = 0;
                    }
                }
            }
            return;
        }

        //
        // If input data are not in CRS format, perform conversion to CRS
        //
        if (!sparse.sparseiscrs(x, _params))
        {
            sparse.sparsecopytocrs(x, xcrs, _params);
            pcatruncatedsubspacesparse(xcrs, npoints, nvars, nneeded, eps, maxits, ref s2, ref v, _params);
            return;
        }

        //
        // Initialize parameters, prepare buffers
        //
        b1 = new double[npoints];
        z1 = new double[nvars];
        if ((double)(eps) == (double)(0) && maxits == 0)
        {
            eps = 1.0E-6;
        }
        if (maxits == 0)
        {
            maxits = 50 + 2 * nvars;
        }

        //
        // Calculate mean values
        //
        vv = (double)1 / (double)npoints;
        for (i = 0; i <= npoints - 1; i++)
        {
            b1[i] = vv;
        }
        sparse.sparsemtv(x, b1, ref means, _params);

        //
        // Find eigenvalues with subspace iteration solver
        //
        evd.eigsubspacecreate(nvars, nneeded, solver, _params);
        evd.eigsubspacesetcond(solver, eps, maxits, _params);
        evd.eigsubspaceoocstart(solver, 0, _params);
        while (evd.eigsubspaceooccontinue(solver, _params))
        {
            ap.assert(solver.requesttype == 0, "PCATruncatedSubspace: integrity check failed");
            for (k = 0; k <= solver.requestsize - 1; k++)
            {

                //
                // Calculate B1=(X-meansX)*Zk
                //
                for (i_ = 0; i_ <= nvars - 1; i_++)
                {
                    z1[i_] = solver.x[i_, k];
                }
                sparse.sparsemv(x, z1, ref b1, _params);
                vv = 0.0;
                for (i_ = 0; i_ <= nvars - 1; i_++)
                {
                    vv += solver.x[i_, k] * means[i_];
                }
                for (i = 0; i <= npoints - 1; i++)
                {
                    b1[i] = b1[i] - vv;
                }

                //
                // Calculate (X-meansX)^T*B1
                //
                sparse.sparsemtv(x, b1, ref c1, _params);
                vv = 0;
                for (i = 0; i <= npoints - 1; i++)
                {
                    vv = vv + b1[i];
                }
                for (j = 0; j <= nvars - 1; j++)
                {
                    solver.ax[j, k] = c1[j] - vv * means[j];
                }
            }
        }
        evd.eigsubspaceoocstop(solver, ref s2, ref v, rep, _params);
        if (npoints != 1)
        {
            for (i = 0; i <= nneeded - 1; i++)
            {
                s2[i] = s2[i] / (npoints - 1);
            }
        }
    }


}
