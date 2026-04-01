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

public class matgen
{
    /*************************************************************************
    Generation of a random uniformly distributed (Haar) orthogonal matrix

    INPUT PARAMETERS:
        N   -   matrix size, N>=1
        
    OUTPUT PARAMETERS:
        A   -   orthogonal NxN matrix, array[0..N-1,0..N-1]

    NOTE: this function uses algorithm  described  in  Stewart, G. W.  (1980),
          "The Efficient Generation of  Random  Orthogonal  Matrices  with  an
          Application to Condition Estimators".
          
          Speaking short, to generate an (N+1)x(N+1) orthogonal matrix, it:
          * takes an NxN one
          * takes uniformly distributed unit vector of dimension N+1.
          * constructs a Householder reflection from the vector, then applies
            it to the smaller matrix (embedded in the larger size with a 1 at
            the bottom right corner).

      -- ALGLIB routine --
         04.12.2009
         Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixrndorthogonal(int n,
        ref double[,] a,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        a = new double[0, 0];

        ap.assert(n >= 1, "RMatrixRndOrthogonal: N<1!");
        a = new double[n, n];
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                if (i == j)
                {
                    a[i, j] = 1;
                }
                else
                {
                    a[i, j] = 0;
                }
            }
        }
        rmatrixrndorthogonalfromtheright(a, n, n, _params);
    }


    /*************************************************************************
    Generation of random NxN matrix with given condition number and norm2(A)=1

    INPUT PARAMETERS:
        N   -   matrix size
        C   -   condition number (in 2-norm)

    OUTPUT PARAMETERS:
        A   -   random matrix with norm2(A)=1 and cond(A)=C

      -- ALGLIB routine --
         04.12.2009
         Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixrndcond(int n,
        double c,
        ref double[,] a,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double l1 = 0;
        double l2 = 0;
        hqrnd.hqrndstate rs = new hqrnd.hqrndstate();

        a = new double[0, 0];

        ap.assert(n >= 1 && (double)(c) >= (double)(1), "RMatrixRndCond: N<1 or C<1!");
        a = new double[n, n];
        if (n == 1)
        {

            //
            // special case
            //
            a[0, 0] = 2 * math.randominteger(2) - 1;
            return;
        }
        hqrnd.hqrndrandomize(rs, _params);
        l1 = 0;
        l2 = Math.Log(1 / c);
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                a[i, j] = 0;
            }
        }
        a[0, 0] = Math.Exp(l1);
        for (i = 1; i <= n - 2; i++)
        {
            a[i, i] = Math.Exp(hqrnd.hqrnduniformr(rs, _params) * (l2 - l1) + l1);
        }
        a[n - 1, n - 1] = Math.Exp(l2);
        rmatrixrndorthogonalfromtheleft(a, n, n, _params);
        rmatrixrndorthogonalfromtheright(a, n, n, _params);
    }


    /*************************************************************************
    Generation of a random Haar distributed orthogonal complex matrix

    INPUT PARAMETERS:
        N   -   matrix size, N>=1

    OUTPUT PARAMETERS:
        A   -   orthogonal NxN matrix, array[0..N-1,0..N-1]

    NOTE: this function uses algorithm  described  in  Stewart, G. W.  (1980),
          "The Efficient Generation of  Random  Orthogonal  Matrices  with  an
          Application to Condition Estimators".
          
          Speaking short, to generate an (N+1)x(N+1) orthogonal matrix, it:
          * takes an NxN one
          * takes uniformly distributed unit vector of dimension N+1.
          * constructs a Householder reflection from the vector, then applies
            it to the smaller matrix (embedded in the larger size with a 1 at
            the bottom right corner).

      -- ALGLIB routine --
         04.12.2009
         Bochkanov Sergey
    *************************************************************************/
    public static void cmatrixrndorthogonal(int n,
        ref complex[,] a,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        a = new complex[0, 0];

        ap.assert(n >= 1, "CMatrixRndOrthogonal: N<1!");
        a = new complex[n, n];
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                if (i == j)
                {
                    a[i, j] = 1;
                }
                else
                {
                    a[i, j] = 0;
                }
            }
        }
        cmatrixrndorthogonalfromtheright(a, n, n, _params);
    }


    /*************************************************************************
    Generation of random NxN complex matrix with given condition number C and
    norm2(A)=1

    INPUT PARAMETERS:
        N   -   matrix size
        C   -   condition number (in 2-norm)

    OUTPUT PARAMETERS:
        A   -   random matrix with norm2(A)=1 and cond(A)=C

      -- ALGLIB routine --
         04.12.2009
         Bochkanov Sergey
    *************************************************************************/
    public static void cmatrixrndcond(int n,
        double c,
        ref complex[,] a,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double l1 = 0;
        double l2 = 0;
        hqrnd.hqrndstate state = new hqrnd.hqrndstate();
        complex v = 0;

        a = new complex[0, 0];

        ap.assert(n >= 1 && (double)(c) >= (double)(1), "CMatrixRndCond: N<1 or C<1!");
        a = new complex[n, n];
        if (n == 1)
        {

            //
            // special case
            //
            hqrnd.hqrndrandomize(state, _params);
            hqrnd.hqrndunit2(state, ref v.x, ref v.y, _params);
            a[0, 0] = v;
            return;
        }
        hqrnd.hqrndrandomize(state, _params);
        l1 = 0;
        l2 = Math.Log(1 / c);
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                a[i, j] = 0;
            }
        }
        a[0, 0] = Math.Exp(l1);
        for (i = 1; i <= n - 2; i++)
        {
            a[i, i] = Math.Exp(hqrnd.hqrnduniformr(state, _params) * (l2 - l1) + l1);
        }
        a[n - 1, n - 1] = Math.Exp(l2);
        cmatrixrndorthogonalfromtheleft(a, n, n, _params);
        cmatrixrndorthogonalfromtheright(a, n, n, _params);
    }


    /*************************************************************************
    Generation of random NxN symmetric matrix with given condition number  and
    norm2(A)=1

    INPUT PARAMETERS:
        N   -   matrix size
        C   -   condition number (in 2-norm)

    OUTPUT PARAMETERS:
        A   -   random matrix with norm2(A)=1 and cond(A)=C

      -- ALGLIB routine --
         04.12.2009
         Bochkanov Sergey
    *************************************************************************/
    public static void smatrixrndcond(int n,
        double c,
        ref double[,] a,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double l1 = 0;
        double l2 = 0;
        hqrnd.hqrndstate rs = new hqrnd.hqrndstate();

        a = new double[0, 0];

        ap.assert(n >= 1 && (double)(c) >= (double)(1), "SMatrixRndCond: N<1 or C<1!");
        a = new double[n, n];
        if (n == 1)
        {

            //
            // special case
            //
            a[0, 0] = 2 * math.randominteger(2) - 1;
            return;
        }

        //
        // Prepare matrix
        //
        hqrnd.hqrndrandomize(rs, _params);
        l1 = 0;
        l2 = Math.Log(1 / c);
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                a[i, j] = 0;
            }
        }
        a[0, 0] = Math.Exp(l1);
        for (i = 1; i <= n - 2; i++)
        {
            a[i, i] = (2 * hqrnd.hqrnduniformi(rs, 2, _params) - 1) * Math.Exp(hqrnd.hqrnduniformr(rs, _params) * (l2 - l1) + l1);
        }
        a[n - 1, n - 1] = Math.Exp(l2);

        //
        // Multiply
        //
        smatrixrndmultiply(a, n, _params);
    }


    /*************************************************************************
    Generation of random NxN symmetric positive definite matrix with given
    condition number and norm2(A)=1

    INPUT PARAMETERS:
        N   -   matrix size
        C   -   condition number (in 2-norm)

    OUTPUT PARAMETERS:
        A   -   random SPD matrix with norm2(A)=1 and cond(A)=C

      -- ALGLIB routine --
         04.12.2009
         Bochkanov Sergey
    *************************************************************************/
    public static void spdmatrixrndcond(int n,
        double c,
        ref double[,] a,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double l1 = 0;
        double l2 = 0;
        hqrnd.hqrndstate rs = new hqrnd.hqrndstate();

        a = new double[0, 0];


        //
        // Special cases
        //
        if (n <= 0 || (double)(c) < (double)(1))
        {
            return;
        }
        a = new double[n, n];
        if (n == 1)
        {
            a[0, 0] = 1;
            return;
        }

        //
        // Prepare matrix
        //
        hqrnd.hqrndrandomize(rs, _params);
        l1 = 0;
        l2 = Math.Log(1 / c);
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                a[i, j] = 0;
            }
        }
        a[0, 0] = Math.Exp(l1);
        for (i = 1; i <= n - 2; i++)
        {
            a[i, i] = Math.Exp(hqrnd.hqrnduniformr(rs, _params) * (l2 - l1) + l1);
        }
        a[n - 1, n - 1] = Math.Exp(l2);

        //
        // Multiply
        //
        smatrixrndmultiply(a, n, _params);
    }


    /*************************************************************************
    Generation of random NxN Hermitian matrix with given condition number  and
    norm2(A)=1

    INPUT PARAMETERS:
        N   -   matrix size
        C   -   condition number (in 2-norm)

    OUTPUT PARAMETERS:
        A   -   random matrix with norm2(A)=1 and cond(A)=C

      -- ALGLIB routine --
         04.12.2009
         Bochkanov Sergey
    *************************************************************************/
    public static void hmatrixrndcond(int n,
        double c,
        ref complex[,] a,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double l1 = 0;
        double l2 = 0;
        hqrnd.hqrndstate rs = new hqrnd.hqrndstate();

        a = new complex[0, 0];

        ap.assert(n >= 1 && (double)(c) >= (double)(1), "HMatrixRndCond: N<1 or C<1!");
        a = new complex[n, n];
        if (n == 1)
        {

            //
            // special case
            //
            a[0, 0] = 2 * math.randominteger(2) - 1;
            return;
        }

        //
        // Prepare matrix
        //
        hqrnd.hqrndrandomize(rs, _params);
        l1 = 0;
        l2 = Math.Log(1 / c);
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                a[i, j] = 0;
            }
        }
        a[0, 0] = Math.Exp(l1);
        for (i = 1; i <= n - 2; i++)
        {
            a[i, i] = (2 * hqrnd.hqrnduniformi(rs, 2, _params) - 1) * Math.Exp(hqrnd.hqrnduniformr(rs, _params) * (l2 - l1) + l1);
        }
        a[n - 1, n - 1] = Math.Exp(l2);

        //
        // Multiply
        //
        hmatrixrndmultiply(a, n, _params);

        //
        // post-process to ensure that matrix diagonal is real
        //
        for (i = 0; i <= n - 1; i++)
        {
            a[i, i].y = 0;
        }
    }


    /*************************************************************************
    Generation of random NxN Hermitian positive definite matrix with given
    condition number and norm2(A)=1

    INPUT PARAMETERS:
        N   -   matrix size
        C   -   condition number (in 2-norm)

    OUTPUT PARAMETERS:
        A   -   random HPD matrix with norm2(A)=1 and cond(A)=C

      -- ALGLIB routine --
         04.12.2009
         Bochkanov Sergey
    *************************************************************************/
    public static void hpdmatrixrndcond(int n,
        double c,
        ref complex[,] a,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double l1 = 0;
        double l2 = 0;
        hqrnd.hqrndstate rs = new hqrnd.hqrndstate();

        a = new complex[0, 0];


        //
        // Special cases
        //
        if (n <= 0 || (double)(c) < (double)(1))
        {
            return;
        }
        a = new complex[n, n];
        if (n == 1)
        {
            a[0, 0] = 1;
            return;
        }

        //
        // Prepare matrix
        //
        hqrnd.hqrndrandomize(rs, _params);
        l1 = 0;
        l2 = Math.Log(1 / c);
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                a[i, j] = 0;
            }
        }
        a[0, 0] = Math.Exp(l1);
        for (i = 1; i <= n - 2; i++)
        {
            a[i, i] = Math.Exp(hqrnd.hqrnduniformr(rs, _params) * (l2 - l1) + l1);
        }
        a[n - 1, n - 1] = Math.Exp(l2);

        //
        // Multiply
        //
        hmatrixrndmultiply(a, n, _params);

        //
        // post-process to ensure that matrix diagonal is real
        //
        for (i = 0; i <= n - 1; i++)
        {
            a[i, i].y = 0;
        }
    }


    /*************************************************************************
    Multiplication of MxN matrix by NxN random Haar distributed orthogonal matrix

    INPUT PARAMETERS:
        A   -   matrix, array[0..M-1, 0..N-1]
        M, N-   matrix size

    OUTPUT PARAMETERS:
        A   -   A*Q, where Q is random NxN orthogonal matrix

      -- ALGLIB routine --
         04.12.2009
         Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixrndorthogonalfromtheright(double[,] a,
        int m,
        int n,
        xparams _params)
    {
        double tau = 0;
        double lambdav = 0;
        int s = 0;
        int i = 0;
        double u1 = 0;
        double u2 = 0;
        double[] w = new double[0];
        double[] v = new double[0];
        hqrnd.hqrndstate state = new hqrnd.hqrndstate();
        int i_ = 0;

        ap.assert(n >= 1 && m >= 1, "RMatrixRndOrthogonalFromTheRight: N<1 or M<1!");
        if (n == 1)
        {

            //
            // Special case
            //
            tau = 2 * math.randominteger(2) - 1;
            for (i = 0; i <= m - 1; i++)
            {
                a[i, 0] = a[i, 0] * tau;
            }
            return;
        }

        //
        // General case.
        // First pass.
        //
        w = new double[m];
        v = new double[n + 1];
        hqrnd.hqrndrandomize(state, _params);
        for (s = 2; s <= n; s++)
        {

            //
            // Prepare random normal v
            //
            do
            {
                i = 1;
                while (i <= s)
                {
                    hqrnd.hqrndnormal2(state, ref u1, ref u2, _params);
                    v[i] = u1;
                    if (i + 1 <= s)
                    {
                        v[i + 1] = u2;
                    }
                    i = i + 2;
                }
                lambdav = 0.0;
                for (i_ = 1; i_ <= s; i_++)
                {
                    lambdav += v[i_] * v[i_];
                }
            }
            while ((double)(lambdav) == (double)(0));

            //
            // Prepare and apply reflection
            //
            ablas.generatereflection(ref v, s, ref tau, _params);
            v[1] = 1;
            ablas.applyreflectionfromtheright(a, tau, v, 0, m - 1, n - s, n - 1, ref w, _params);
        }

        //
        // Second pass.
        //
        for (i = 0; i <= n - 1; i++)
        {
            tau = 2 * hqrnd.hqrnduniformi(state, 2, _params) - 1;
            for (i_ = 0; i_ <= m - 1; i_++)
            {
                a[i_, i] = tau * a[i_, i];
            }
        }
    }


    /*************************************************************************
    Multiplication of MxN matrix by MxM random Haar distributed orthogonal matrix

    INPUT PARAMETERS:
        A   -   matrix, array[0..M-1, 0..N-1]
        M, N-   matrix size

    OUTPUT PARAMETERS:
        A   -   Q*A, where Q is random MxM orthogonal matrix

      -- ALGLIB routine --
         04.12.2009
         Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixrndorthogonalfromtheleft(double[,] a,
        int m,
        int n,
        xparams _params)
    {
        double tau = 0;
        double lambdav = 0;
        int s = 0;
        int i = 0;
        int j = 0;
        double u1 = 0;
        double u2 = 0;
        double[] w = new double[0];
        double[] v = new double[0];
        hqrnd.hqrndstate state = new hqrnd.hqrndstate();
        int i_ = 0;

        ap.assert(n >= 1 && m >= 1, "RMatrixRndOrthogonalFromTheRight: N<1 or M<1!");
        if (m == 1)
        {

            //
            // special case
            //
            tau = 2 * math.randominteger(2) - 1;
            for (j = 0; j <= n - 1; j++)
            {
                a[0, j] = a[0, j] * tau;
            }
            return;
        }

        //
        // General case.
        // First pass.
        //
        w = new double[n];
        v = new double[m + 1];
        hqrnd.hqrndrandomize(state, _params);
        for (s = 2; s <= m; s++)
        {

            //
            // Prepare random normal v
            //
            do
            {
                i = 1;
                while (i <= s)
                {
                    hqrnd.hqrndnormal2(state, ref u1, ref u2, _params);
                    v[i] = u1;
                    if (i + 1 <= s)
                    {
                        v[i + 1] = u2;
                    }
                    i = i + 2;
                }
                lambdav = 0.0;
                for (i_ = 1; i_ <= s; i_++)
                {
                    lambdav += v[i_] * v[i_];
                }
            }
            while ((double)(lambdav) == (double)(0));

            //
            // Prepare and apply reflection
            //
            ablas.generatereflection(ref v, s, ref tau, _params);
            v[1] = 1;
            ablas.applyreflectionfromtheleft(a, tau, v, m - s, m - 1, 0, n - 1, ref w, _params);
        }

        //
        // Second pass.
        //
        for (i = 0; i <= m - 1; i++)
        {
            tau = 2 * hqrnd.hqrnduniformi(state, 2, _params) - 1;
            for (i_ = 0; i_ <= n - 1; i_++)
            {
                a[i, i_] = tau * a[i, i_];
            }
        }
    }


    /*************************************************************************
    Multiplication of MxN complex matrix by NxN random Haar distributed
    complex orthogonal matrix

    INPUT PARAMETERS:
        A   -   matrix, array[0..M-1, 0..N-1]
        M, N-   matrix size

    OUTPUT PARAMETERS:
        A   -   A*Q, where Q is random NxN orthogonal matrix

      -- ALGLIB routine --
         04.12.2009
         Bochkanov Sergey
    *************************************************************************/
    public static void cmatrixrndorthogonalfromtheright(complex[,] a,
        int m,
        int n,
        xparams _params)
    {
        complex lambdav = 0;
        complex tau = 0;
        int s = 0;
        int i = 0;
        complex[] w = new complex[0];
        complex[] v = new complex[0];
        hqrnd.hqrndstate state = new hqrnd.hqrndstate();
        int i_ = 0;

        ap.assert(n >= 1 && m >= 1, "CMatrixRndOrthogonalFromTheRight: N<1 or M<1!");
        if (n == 1)
        {

            //
            // Special case
            //
            hqrnd.hqrndrandomize(state, _params);
            hqrnd.hqrndunit2(state, ref tau.x, ref tau.y, _params);
            for (i = 0; i <= m - 1; i++)
            {
                a[i, 0] = a[i, 0] * tau;
            }
            return;
        }

        //
        // General case.
        // First pass.
        //
        w = new complex[m];
        v = new complex[n + 1];
        hqrnd.hqrndrandomize(state, _params);
        for (s = 2; s <= n; s++)
        {

            //
            // Prepare random normal v
            //
            do
            {
                for (i = 1; i <= s; i++)
                {
                    hqrnd.hqrndnormal2(state, ref tau.x, ref tau.y, _params);
                    v[i] = tau;
                }
                lambdav = 0.0;
                for (i_ = 1; i_ <= s; i_++)
                {
                    lambdav += v[i_] * math.conj(v[i_]);
                }
            }
            while (lambdav == 0);

            //
            // Prepare and apply reflection
            //
            creflections.complexgeneratereflection(ref v, s, ref tau, _params);
            v[1] = 1;
            creflections.complexapplyreflectionfromtheright(a, tau, v, 0, m - 1, n - s, n - 1, ref w, _params);
        }

        //
        // Second pass.
        //
        for (i = 0; i <= n - 1; i++)
        {
            hqrnd.hqrndunit2(state, ref tau.x, ref tau.y, _params);
            for (i_ = 0; i_ <= m - 1; i_++)
            {
                a[i_, i] = tau * a[i_, i];
            }
        }
    }


    /*************************************************************************
    Multiplication of MxN complex matrix by MxM random Haar distributed
    complex orthogonal matrix

    INPUT PARAMETERS:
        A   -   matrix, array[0..M-1, 0..N-1]
        M, N-   matrix size

    OUTPUT PARAMETERS:
        A   -   Q*A, where Q is random MxM orthogonal matrix

      -- ALGLIB routine --
         04.12.2009
         Bochkanov Sergey
    *************************************************************************/
    public static void cmatrixrndorthogonalfromtheleft(complex[,] a,
        int m,
        int n,
        xparams _params)
    {
        complex tau = 0;
        complex lambdav = 0;
        int s = 0;
        int i = 0;
        int j = 0;
        complex[] w = new complex[0];
        complex[] v = new complex[0];
        hqrnd.hqrndstate state = new hqrnd.hqrndstate();
        int i_ = 0;

        ap.assert(n >= 1 && m >= 1, "CMatrixRndOrthogonalFromTheRight: N<1 or M<1!");
        if (m == 1)
        {

            //
            // special case
            //
            hqrnd.hqrndrandomize(state, _params);
            hqrnd.hqrndunit2(state, ref tau.x, ref tau.y, _params);
            for (j = 0; j <= n - 1; j++)
            {
                a[0, j] = a[0, j] * tau;
            }
            return;
        }

        //
        // General case.
        // First pass.
        //
        w = new complex[n];
        v = new complex[m + 1];
        hqrnd.hqrndrandomize(state, _params);
        for (s = 2; s <= m; s++)
        {

            //
            // Prepare random normal v
            //
            do
            {
                for (i = 1; i <= s; i++)
                {
                    hqrnd.hqrndnormal2(state, ref tau.x, ref tau.y, _params);
                    v[i] = tau;
                }
                lambdav = 0.0;
                for (i_ = 1; i_ <= s; i_++)
                {
                    lambdav += v[i_] * math.conj(v[i_]);
                }
            }
            while (lambdav == 0);

            //
            // Prepare and apply reflection
            //
            creflections.complexgeneratereflection(ref v, s, ref tau, _params);
            v[1] = 1;
            creflections.complexapplyreflectionfromtheleft(a, tau, v, m - s, m - 1, 0, n - 1, ref w, _params);
        }

        //
        // Second pass.
        //
        for (i = 0; i <= m - 1; i++)
        {
            hqrnd.hqrndunit2(state, ref tau.x, ref tau.y, _params);
            for (i_ = 0; i_ <= n - 1; i_++)
            {
                a[i, i_] = tau * a[i, i_];
            }
        }
    }


    /*************************************************************************
    Symmetric multiplication of NxN matrix by random Haar distributed
    orthogonal  matrix

    INPUT PARAMETERS:
        A   -   matrix, array[0..N-1, 0..N-1]
        N   -   matrix size

    OUTPUT PARAMETERS:
        A   -   Q'*A*Q, where Q is random NxN orthogonal matrix

      -- ALGLIB routine --
         04.12.2009
         Bochkanov Sergey
    *************************************************************************/
    public static void smatrixrndmultiply(double[,] a,
        int n,
        xparams _params)
    {
        double tau = 0;
        double lambdav = 0;
        int s = 0;
        int i = 0;
        double u1 = 0;
        double u2 = 0;
        double[] w = new double[0];
        double[] v = new double[0];
        hqrnd.hqrndstate state = new hqrnd.hqrndstate();
        int i_ = 0;


        //
        // General case.
        //
        w = new double[n];
        v = new double[n + 1];
        hqrnd.hqrndrandomize(state, _params);
        for (s = 2; s <= n; s++)
        {

            //
            // Prepare random normal v
            //
            do
            {
                i = 1;
                while (i <= s)
                {
                    hqrnd.hqrndnormal2(state, ref u1, ref u2, _params);
                    v[i] = u1;
                    if (i + 1 <= s)
                    {
                        v[i + 1] = u2;
                    }
                    i = i + 2;
                }
                lambdav = 0.0;
                for (i_ = 1; i_ <= s; i_++)
                {
                    lambdav += v[i_] * v[i_];
                }
            }
            while ((double)(lambdav) == (double)(0));

            //
            // Prepare and apply reflection
            //
            ablas.generatereflection(ref v, s, ref tau, _params);
            v[1] = 1;
            ablas.applyreflectionfromtheright(a, tau, v, 0, n - 1, n - s, n - 1, ref w, _params);
            ablas.applyreflectionfromtheleft(a, tau, v, n - s, n - 1, 0, n - 1, ref w, _params);
        }

        //
        // Second pass.
        //
        for (i = 0; i <= n - 1; i++)
        {
            tau = 2 * hqrnd.hqrnduniformi(state, 2, _params) - 1;
            for (i_ = 0; i_ <= n - 1; i_++)
            {
                a[i_, i] = tau * a[i_, i];
            }
            for (i_ = 0; i_ <= n - 1; i_++)
            {
                a[i, i_] = tau * a[i, i_];
            }
        }

        //
        // Copy upper triangle to lower
        //
        for (i = 0; i <= n - 2; i++)
        {
            for (i_ = i + 1; i_ <= n - 1; i_++)
            {
                a[i_, i] = a[i, i_];
            }
        }
    }


    /*************************************************************************
    Hermitian multiplication of NxN matrix by random Haar distributed
    complex orthogonal matrix

    INPUT PARAMETERS:
        A   -   matrix, array[0..N-1, 0..N-1]
        N   -   matrix size

    OUTPUT PARAMETERS:
        A   -   Q^H*A*Q, where Q is random NxN orthogonal matrix

      -- ALGLIB routine --
         04.12.2009
         Bochkanov Sergey
    *************************************************************************/
    public static void hmatrixrndmultiply(complex[,] a,
        int n,
        xparams _params)
    {
        complex tau = 0;
        complex lambdav = 0;
        int s = 0;
        int i = 0;
        complex[] w = new complex[0];
        complex[] v = new complex[0];
        hqrnd.hqrndstate state = new hqrnd.hqrndstate();
        int i_ = 0;


        //
        // General case.
        //
        w = new complex[n];
        v = new complex[n + 1];
        hqrnd.hqrndrandomize(state, _params);
        for (s = 2; s <= n; s++)
        {

            //
            // Prepare random normal v
            //
            do
            {
                for (i = 1; i <= s; i++)
                {
                    hqrnd.hqrndnormal2(state, ref tau.x, ref tau.y, _params);
                    v[i] = tau;
                }
                lambdav = 0.0;
                for (i_ = 1; i_ <= s; i_++)
                {
                    lambdav += v[i_] * math.conj(v[i_]);
                }
            }
            while (lambdav == 0);

            //
            // Prepare and apply reflection
            //
            creflections.complexgeneratereflection(ref v, s, ref tau, _params);
            v[1] = 1;
            creflections.complexapplyreflectionfromtheright(a, tau, v, 0, n - 1, n - s, n - 1, ref w, _params);
            creflections.complexapplyreflectionfromtheleft(a, math.conj(tau), v, n - s, n - 1, 0, n - 1, ref w, _params);
        }

        //
        // Second pass.
        //
        for (i = 0; i <= n - 1; i++)
        {
            hqrnd.hqrndunit2(state, ref tau.x, ref tau.y, _params);
            for (i_ = 0; i_ <= n - 1; i_++)
            {
                a[i_, i] = tau * a[i_, i];
            }
            tau = math.conj(tau);
            for (i_ = 0; i_ <= n - 1; i_++)
            {
                a[i, i_] = tau * a[i, i_];
            }
        }

        //
        // Change all values from lower triangle by complex-conjugate values
        // from upper one
        //
        for (i = 0; i <= n - 2; i++)
        {
            for (i_ = i + 1; i_ <= n - 1; i_++)
            {
                a[i_, i] = a[i, i_];
            }
        }
        for (s = 0; s <= n - 2; s++)
        {
            for (i = s + 1; i <= n - 1; i++)
            {
                a[i, s].y = -a[i, s].y;
            }
        }
    }


}
