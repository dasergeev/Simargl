#pragma warning disable CS1591

using System;

namespace Simargl.Algorithms.Raw;

public class svd
{
    /*************************************************************************
    Singular value decomposition of a rectangular matrix.

      ! COMMERCIAL EDITION OF ALGLIB:
      ! 
      ! Commercial Edition of ALGLIB includes following important improvements
      ! of this function:
      ! * high-performance native backend with same C# interface (C# version)
      ! * hardware vendor (Intel) implementations of linear algebra primitives
      !   (C++ and C# versions, x86/x64 platform)
      ! 
      ! We recommend you to read 'Working with commercial version' section  of
      ! ALGLIB Reference Manual in order to find out how to  use  performance-
      ! related features provided by commercial edition of ALGLIB.

    The algorithm calculates the singular value decomposition of a matrix of
    size MxN: A = U * S * V^T

    The algorithm finds the singular values and, optionally, matrices U and V^T.
    The algorithm can find both first min(M,N) columns of matrix U and rows of
    matrix V^T (singular vectors), and matrices U and V^T wholly (of sizes MxM
    and NxN respectively).

    Take into account that the subroutine does not return matrix V but V^T.

    Input parameters:
        A           -   matrix to be decomposed.
                        Array whose indexes range within [0..M-1, 0..N-1].
        M           -   number of rows in matrix A.
        N           -   number of columns in matrix A.
        UNeeded     -   0, 1 or 2. See the description of the parameter U.
        VTNeeded    -   0, 1 or 2. See the description of the parameter VT.
        AdditionalMemory -
                        If the parameter:
                         * equals 0, the algorithm doesn't use additional
                           memory (lower requirements, lower performance).
                         * equals 1, the algorithm uses additional
                           memory of size min(M,N)*min(M,N) of real numbers.
                           It often speeds up the algorithm.
                         * equals 2, the algorithm uses additional
                           memory of size M*min(M,N) of real numbers.
                           It allows to get a maximum performance.
                        The recommended value of the parameter is 2.

    Output parameters:
        W           -   contains singular values in descending order.
        U           -   if UNeeded=0, U isn't changed, the left singular vectors
                        are not calculated.
                        if Uneeded=1, U contains left singular vectors (first
                        min(M,N) columns of matrix U). Array whose indexes range
                        within [0..M-1, 0..Min(M,N)-1].
                        if UNeeded=2, U contains matrix U wholly. Array whose
                        indexes range within [0..M-1, 0..M-1].
        VT          -   if VTNeeded=0, VT isn't changed, the right singular vectors
                        are not calculated.
                        if VTNeeded=1, VT contains right singular vectors (first
                        min(M,N) rows of matrix V^T). Array whose indexes range
                        within [0..min(M,N)-1, 0..N-1].
                        if VTNeeded=2, VT contains matrix V^T wholly. Array whose
                        indexes range within [0..N-1, 0..N-1].

      -- ALGLIB --
         Copyright 2005 by Bochkanov Sergey
    *************************************************************************/
    public static bool rmatrixsvd(double[,] a,
        int m,
        int n,
        int uneeded,
        int vtneeded,
        int additionalmemory,
        ref double[] w,
        ref double[,] u,
        ref double[,] vt,
        xparams _params)
    {
        bool result = new bool();
        double[] tauq = new double[0];
        double[] taup = new double[0];
        double[] tau = new double[0];
        double[] e = new double[0];
        double[] work = new double[0];
        double[,] t2 = new double[0, 0];
        bool isupper = new bool();
        int minmn = 0;
        int ncu = 0;
        int nrvt = 0;
        int nru = 0;
        int ncvt = 0;
        int i = 0;
        int j = 0;

        a = (double[,])a.Clone();
        w = new double[0];
        u = new double[0, 0];
        vt = new double[0, 0];

        result = true;
        if (m == 0 || n == 0)
        {
            return result;
        }
        ap.assert(uneeded >= 0 && uneeded <= 2, "SVDDecomposition: wrong parameters!");
        ap.assert(vtneeded >= 0 && vtneeded <= 2, "SVDDecomposition: wrong parameters!");
        ap.assert(additionalmemory >= 0 && additionalmemory <= 2, "SVDDecomposition: wrong parameters!");

        //
        // initialize
        //
        minmn = Math.Min(m, n);
        w = new double[minmn + 1];
        ncu = 0;
        nru = 0;
        if (uneeded == 1)
        {
            nru = m;
            ncu = minmn;
            u = new double[nru - 1 + 1, ncu - 1 + 1];
        }
        if (uneeded == 2)
        {
            nru = m;
            ncu = m;
            u = new double[nru - 1 + 1, ncu - 1 + 1];
        }
        nrvt = 0;
        ncvt = 0;
        if (vtneeded == 1)
        {
            nrvt = minmn;
            ncvt = n;
            vt = new double[nrvt - 1 + 1, ncvt - 1 + 1];
        }
        if (vtneeded == 2)
        {
            nrvt = n;
            ncvt = n;
            vt = new double[nrvt - 1 + 1, ncvt - 1 + 1];
        }

        //
        // M much larger than N
        // Use bidiagonal reduction with QR-decomposition
        //
        if ((double)(m) > (double)(1.6 * n))
        {
            if (uneeded == 0)
            {

                //
                // No left singular vectors to be computed
                //
                ortfac.rmatrixqr(a, m, n, ref tau, _params);
                for (i = 0; i <= n - 1; i++)
                {
                    for (j = 0; j <= i - 1; j++)
                    {
                        a[i, j] = 0;
                    }
                }
                ortfac.rmatrixbd(a, n, n, ref tauq, ref taup, _params);
                ortfac.rmatrixbdunpackpt(a, n, n, taup, nrvt, ref vt, _params);
                ortfac.rmatrixbdunpackdiagonals(a, n, n, ref isupper, ref w, ref e, _params);
                result = bdsvd.rmatrixbdsvd(w, e, n, isupper, false, u, 0, a, 0, vt, ncvt, _params);
                return result;
            }
            else
            {

                //
                // Left singular vectors (may be full matrix U) to be computed
                //
                ortfac.rmatrixqr(a, m, n, ref tau, _params);
                ortfac.rmatrixqrunpackq(a, m, n, tau, ncu, ref u, _params);
                for (i = 0; i <= n - 1; i++)
                {
                    for (j = 0; j <= i - 1; j++)
                    {
                        a[i, j] = 0;
                    }
                }
                ortfac.rmatrixbd(a, n, n, ref tauq, ref taup, _params);
                ortfac.rmatrixbdunpackpt(a, n, n, taup, nrvt, ref vt, _params);
                ortfac.rmatrixbdunpackdiagonals(a, n, n, ref isupper, ref w, ref e, _params);
                if (additionalmemory < 1)
                {

                    //
                    // No additional memory can be used
                    //
                    ortfac.rmatrixbdmultiplybyq(a, n, n, tauq, u, m, n, true, false, _params);
                    result = bdsvd.rmatrixbdsvd(w, e, n, isupper, false, u, m, a, 0, vt, ncvt, _params);
                }
                else
                {

                    //
                    // Large U. Transforming intermediate matrix T2
                    //
                    work = new double[Math.Max(m, n) + 1];
                    ortfac.rmatrixbdunpackq(a, n, n, tauq, n, ref t2, _params);
                    RawBlas.copymatrix(u, 0, m - 1, 0, n - 1, ref a, 0, m - 1, 0, n - 1, _params);
                    RawBlas.inplacetranspose(ref t2, 0, n - 1, 0, n - 1, ref work, _params);
                    result = bdsvd.rmatrixbdsvd(w, e, n, isupper, false, u, 0, t2, n, vt, ncvt, _params);
                    ablas.rmatrixgemm(m, n, n, 1.0, a, 0, 0, 0, t2, 0, 0, 1, 0.0, u, 0, 0, _params);
                }
                return result;
            }
        }

        //
        // N much larger than M
        // Use bidiagonal reduction with LQ-decomposition
        //
        if ((double)(n) > (double)(1.6 * m))
        {
            if (vtneeded == 0)
            {

                //
                // No right singular vectors to be computed
                //
                ortfac.rmatrixlq(a, m, n, ref tau, _params);
                for (i = 0; i <= m - 1; i++)
                {
                    for (j = i + 1; j <= m - 1; j++)
                    {
                        a[i, j] = 0;
                    }
                }
                ortfac.rmatrixbd(a, m, m, ref tauq, ref taup, _params);
                ortfac.rmatrixbdunpackq(a, m, m, tauq, ncu, ref u, _params);
                ortfac.rmatrixbdunpackdiagonals(a, m, m, ref isupper, ref w, ref e, _params);
                work = new double[m + 1];
                RawBlas.inplacetranspose(ref u, 0, nru - 1, 0, ncu - 1, ref work, _params);
                result = bdsvd.rmatrixbdsvd(w, e, m, isupper, false, a, 0, u, nru, vt, 0, _params);
                RawBlas.inplacetranspose(ref u, 0, nru - 1, 0, ncu - 1, ref work, _params);
                return result;
            }
            else
            {

                //
                // Right singular vectors (may be full matrix VT) to be computed
                //
                ortfac.rmatrixlq(a, m, n, ref tau, _params);
                ortfac.rmatrixlqunpackq(a, m, n, tau, nrvt, ref vt, _params);
                for (i = 0; i <= m - 1; i++)
                {
                    for (j = i + 1; j <= m - 1; j++)
                    {
                        a[i, j] = 0;
                    }
                }
                ortfac.rmatrixbd(a, m, m, ref tauq, ref taup, _params);
                ortfac.rmatrixbdunpackq(a, m, m, tauq, ncu, ref u, _params);
                ortfac.rmatrixbdunpackdiagonals(a, m, m, ref isupper, ref w, ref e, _params);
                work = new double[Math.Max(m, n) + 1];
                RawBlas.inplacetranspose(ref u, 0, nru - 1, 0, ncu - 1, ref work, _params);
                if (additionalmemory < 1)
                {

                    //
                    // No additional memory available
                    //
                    ortfac.rmatrixbdmultiplybyp(a, m, m, taup, vt, m, n, false, true, _params);
                    result = bdsvd.rmatrixbdsvd(w, e, m, isupper, false, a, 0, u, nru, vt, n, _params);
                }
                else
                {

                    //
                    // Large VT. Transforming intermediate matrix T2
                    //
                    ortfac.rmatrixbdunpackpt(a, m, m, taup, m, ref t2, _params);
                    result = bdsvd.rmatrixbdsvd(w, e, m, isupper, false, a, 0, u, nru, t2, m, _params);
                    RawBlas.copymatrix(vt, 0, m - 1, 0, n - 1, ref a, 0, m - 1, 0, n - 1, _params);
                    ablas.rmatrixgemm(m, n, m, 1.0, t2, 0, 0, 0, a, 0, 0, 0, 0.0, vt, 0, 0, _params);
                }
                RawBlas.inplacetranspose(ref u, 0, nru - 1, 0, ncu - 1, ref work, _params);
                return result;
            }
        }

        //
        // M<=N
        // We can use inplace transposition of U to get rid of columnwise operations
        //
        if (m <= n)
        {
            ortfac.rmatrixbd(a, m, n, ref tauq, ref taup, _params);
            ortfac.rmatrixbdunpackq(a, m, n, tauq, ncu, ref u, _params);
            ortfac.rmatrixbdunpackpt(a, m, n, taup, nrvt, ref vt, _params);
            ortfac.rmatrixbdunpackdiagonals(a, m, n, ref isupper, ref w, ref e, _params);
            work = new double[m + 1];
            RawBlas.inplacetranspose(ref u, 0, nru - 1, 0, ncu - 1, ref work, _params);
            result = bdsvd.rmatrixbdsvd(w, e, minmn, isupper, false, a, 0, u, nru, vt, ncvt, _params);
            RawBlas.inplacetranspose(ref u, 0, nru - 1, 0, ncu - 1, ref work, _params);
            return result;
        }

        //
        // Simple bidiagonal reduction
        //
        ortfac.rmatrixbd(a, m, n, ref tauq, ref taup, _params);
        ortfac.rmatrixbdunpackq(a, m, n, tauq, ncu, ref u, _params);
        ortfac.rmatrixbdunpackpt(a, m, n, taup, nrvt, ref vt, _params);
        ortfac.rmatrixbdunpackdiagonals(a, m, n, ref isupper, ref w, ref e, _params);
        if (additionalmemory < 2 || uneeded == 0)
        {

            //
            // We cant use additional memory or there is no need in such operations
            //
            result = bdsvd.rmatrixbdsvd(w, e, minmn, isupper, false, u, nru, a, 0, vt, ncvt, _params);
        }
        else
        {

            //
            // We can use additional memory
            //
            t2 = new double[minmn - 1 + 1, m - 1 + 1];
            RawBlas.copyandtranspose(u, 0, m - 1, 0, minmn - 1, ref t2, 0, minmn - 1, 0, m - 1, _params);
            result = bdsvd.rmatrixbdsvd(w, e, minmn, isupper, false, u, 0, t2, m, vt, ncvt, _params);
            RawBlas.copyandtranspose(t2, 0, minmn - 1, 0, m - 1, ref u, 0, m - 1, 0, minmn - 1, _params);
        }
        return result;
    }


}

