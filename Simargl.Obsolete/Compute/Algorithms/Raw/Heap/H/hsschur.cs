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

public class hsschur
{
    public static void rmatrixinternalschurdecomposition(double[,] h,
        int n,
        int tneeded,
        int zneeded,
        ref double[] wr,
        ref double[] wi,
        ref double[,] z,
        ref int info,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double[,] h1 = new double[0, 0];
        double[,] z1 = new double[0, 0];
        double[] wr1 = new double[0];
        double[] wi1 = new double[0];

        wr = new double[0];
        wi = new double[0];
        info = 0;


        //
        // Allocate space
        //
        wr = new double[n];
        wi = new double[n];
        if (zneeded == 2)
        {
            apserv.rmatrixsetlengthatleast(ref z, n, n, _params);
        }

        //
        // MKL version
        //
        if (ablasmkl.rmatrixinternalschurdecompositionmkl(h, n, tneeded, zneeded, wr, wi, z, ref info, _params))
        {
            return;
        }

        //
        // ALGLIB version
        //
        h1 = new double[n + 1, n + 1];
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                h1[1 + i, 1 + j] = h[i, j];
            }
        }
        if (zneeded == 1)
        {
            z1 = new double[n + 1, n + 1];
            for (i = 0; i <= n - 1; i++)
            {
                for (j = 0; j <= n - 1; j++)
                {
                    z1[1 + i, 1 + j] = z[i, j];
                }
            }
        }
        internalschurdecomposition(ref h1, n, tneeded, zneeded, ref wr1, ref wi1, ref z1, ref info, _params);
        for (i = 0; i <= n - 1; i++)
        {
            wr[i] = wr1[i + 1];
            wi[i] = wi1[i + 1];
        }
        if (tneeded != 0)
        {
            for (i = 0; i <= n - 1; i++)
            {
                for (j = 0; j <= n - 1; j++)
                {
                    h[i, j] = h1[1 + i, 1 + j];
                }
            }
        }
        if (zneeded != 0)
        {
            apserv.rmatrixsetlengthatleast(ref z, n, n, _params);
            for (i = 0; i <= n - 1; i++)
            {
                for (j = 0; j <= n - 1; j++)
                {
                    z[i, j] = z1[1 + i, 1 + j];
                }
            }
        }
    }


    /*************************************************************************
    Subroutine performing  the  Schur  decomposition  of  a  matrix  in  upper
    Hessenberg form using the QR algorithm with multiple shifts.

    The  source matrix  H  is  represented as  S'*H*S = T, where H - matrix in
    upper Hessenberg form,  S - orthogonal matrix (Schur vectors),   T - upper
    quasi-triangular matrix (with blocks of sizes  1x1  and  2x2  on  the main
    diagonal).

    Input parameters:
        H   -   matrix to be decomposed.
                Array whose indexes range within [1..N, 1..N].
        N   -   size of H, N>=0.


    Output parameters:
        H   -   contains the matrix T.
                Array whose indexes range within [1..N, 1..N].
                All elements below the blocks on the main diagonal are equal
                to 0.
        S   -   contains Schur vectors.
                Array whose indexes range within [1..N, 1..N].

    Note 1:
        The block structure of matrix T could be easily recognized: since  all
        the elements  below  the blocks are zeros, the elements a[i+1,i] which
        are equal to 0 show the block border.

    Note 2:
        the algorithm  performance  depends  on  the  value  of  the  internal
        parameter NS of InternalSchurDecomposition  subroutine  which  defines
        the number of shifts in the QR algorithm (analog of  the  block  width
        in block matrix algorithms in linear algebra). If you require  maximum
        performance  on  your  machine,  it  is  recommended  to  adjust  this
        parameter manually.

    Result:
        True, if the algorithm has converged and the parameters H and S contain
            the result.
        False, if the algorithm has not converged.

    Algorithm implemented on the basis of subroutine DHSEQR (LAPACK 3.0 library).
    *************************************************************************/
    public static bool upperhessenbergschurdecomposition(ref double[,] h,
        int n,
        ref double[,] s,
        xparams _params)
    {
        bool result = new bool();
        double[] wi = new double[0];
        double[] wr = new double[0];
        int info = 0;

        s = new double[0, 0];

        internalschurdecomposition(ref h, n, 1, 2, ref wr, ref wi, ref s, ref info, _params);
        result = info == 0;
        return result;
    }


    public static void internalschurdecomposition(ref double[,] h,
        int n,
        int tneeded,
        int zneeded,
        ref double[] wr,
        ref double[] wi,
        ref double[,] z,
        ref int info,
        xparams _params)
    {
        double[] work = new double[0];
        int i = 0;
        int i1 = 0;
        int i2 = 0;
        int ierr = 0;
        int ii = 0;
        int itemp = 0;
        int itn = 0;
        int its = 0;
        int j = 0;
        int k = 0;
        int l = 0;
        int maxb = 0;
        int nr = 0;
        int ns = 0;
        int nv = 0;
        double absw = 0;
        double smlnum = 0;
        double tau = 0;
        double temp = 0;
        double tst1 = 0;
        double ulp = 0;
        double unfl = 0;
        double[,] s = new double[0, 0];
        double[] v = new double[0];
        double[] vv = new double[0];
        double[] workc1 = new double[0];
        double[] works1 = new double[0];
        double[] workv3 = new double[0];
        double[] tmpwr = new double[0];
        double[] tmpwi = new double[0];
        bool initz = new bool();
        bool wantt = new bool();
        bool wantz = new bool();
        double cnst = 0;
        bool failflag = new bool();
        int p1 = 0;
        int p2 = 0;
        double vt = 0;
        int i_ = 0;
        int i1_ = 0;

        wr = new double[0];
        wi = new double[0];
        info = 0;


        //
        // Set the order of the multi-shift QR algorithm to be used.
        // If you want to tune algorithm, change this values
        //
        ns = 12;
        maxb = 50;

        //
        // Now 2 < NS <= MAXB < NH.
        //
        maxb = Math.Max(3, maxb);
        ns = Math.Min(maxb, ns);

        //
        // Initialize
        //
        cnst = 1.5;
        work = new double[Math.Max(n, 1) + 1];
        s = new double[ns + 1, ns + 1];
        v = new double[ns + 1 + 1];
        vv = new double[ns + 1 + 1];
        wr = new double[Math.Max(n, 1) + 1];
        wi = new double[Math.Max(n, 1) + 1];
        workc1 = new double[1 + 1];
        works1 = new double[1 + 1];
        workv3 = new double[3 + 1];
        tmpwr = new double[Math.Max(n, 1) + 1];
        tmpwi = new double[Math.Max(n, 1) + 1];
        ap.assert(n >= 0, "InternalSchurDecomposition: incorrect N!");
        ap.assert(tneeded == 0 || tneeded == 1, "InternalSchurDecomposition: incorrect TNeeded!");
        ap.assert((zneeded == 0 || zneeded == 1) || zneeded == 2, "InternalSchurDecomposition: incorrect ZNeeded!");
        wantt = tneeded == 1;
        initz = zneeded == 2;
        wantz = zneeded != 0;
        info = 0;

        //
        // Initialize Z, if necessary
        //
        if (initz)
        {
            apserv.rmatrixsetlengthatleast(ref z, n + 1, n + 1, _params);
            for (i = 1; i <= n; i++)
            {
                for (j = 1; j <= n; j++)
                {
                    if (i == j)
                    {
                        z[i, j] = 1;
                    }
                    else
                    {
                        z[i, j] = 0;
                    }
                }
            }
        }

        //
        // Quick return if possible
        //
        if (n == 0)
        {
            return;
        }
        if (n == 1)
        {
            wr[1] = h[1, 1];
            wi[1] = 0;
            return;
        }

        //
        // Set rows and columns 1 to N to zero below the first
        // subdiagonal.
        //
        for (j = 1; j <= n - 2; j++)
        {
            for (i = j + 2; i <= n; i++)
            {
                h[i, j] = 0;
            }
        }

        //
        // Test if N is sufficiently small
        //
        if ((ns <= 2 || ns > n) || maxb >= n)
        {

            //
            // Use the standard double-shift algorithm
            //
            internalauxschur(wantt, wantz, n, 1, n, ref h, ref wr, ref wi, 1, n, ref z, ref work, ref workv3, ref workc1, ref works1, ref info, _params);

            //
            // fill entries under diagonal blocks of T with zeros
            //
            if (wantt)
            {
                j = 1;
                while (j <= n)
                {
                    if ((double)(wi[j]) == (double)(0))
                    {
                        for (i = j + 1; i <= n; i++)
                        {
                            h[i, j] = 0;
                        }
                        j = j + 1;
                    }
                    else
                    {
                        for (i = j + 2; i <= n; i++)
                        {
                            h[i, j] = 0;
                            h[i, j + 1] = 0;
                        }
                        j = j + 2;
                    }
                }
            }
            return;
        }
        unfl = math.minrealnumber;
        ulp = 2 * math.machineepsilon;
        smlnum = unfl * (n / ulp);

        //
        // I1 and I2 are the indices of the first row and last column of H
        // to which transformations must be applied. If eigenvalues only are
        // being computed, I1 and I2 are set inside the main loop.
        //
        i1 = 1;
        i2 = n;

        //
        // ITN is the total number of multiple-shift QR iterations allowed.
        //
        itn = 30 * n;

        //
        // The main loop begins here. I is the loop index and decreases from
        // IHI to ILO in steps of at most MAXB. Each iteration of the loop
        // works with the active submatrix in rows and columns L to I.
        // Eigenvalues I+1 to IHI have already converged. Either L = ILO or
        // H(L,L-1) is negligible so that the matrix splits.
        //
        i = n;
        while (true)
        {
            l = 1;
            if (i < 1)
            {

                //
                // fill entries under diagonal blocks of T with zeros
                //
                if (wantt)
                {
                    j = 1;
                    while (j <= n)
                    {
                        if ((double)(wi[j]) == (double)(0))
                        {
                            for (i = j + 1; i <= n; i++)
                            {
                                h[i, j] = 0;
                            }
                            j = j + 1;
                        }
                        else
                        {
                            for (i = j + 2; i <= n; i++)
                            {
                                h[i, j] = 0;
                                h[i, j + 1] = 0;
                            }
                            j = j + 2;
                        }
                    }
                }

                //
                // Exit
                //
                return;
            }

            //
            // Perform multiple-shift QR iterations on rows and columns ILO to I
            // until a submatrix of order at most MAXB splits off at the bottom
            // because a subdiagonal element has become negligible.
            //
            failflag = true;
            for (its = 0; its <= itn; its++)
            {

                //
                // Look for a single small subdiagonal element.
                //
                for (k = i; k >= l + 1; k--)
                {
                    tst1 = Math.Abs(h[k - 1, k - 1]) + Math.Abs(h[k, k]);
                    if ((double)(tst1) == (double)(0))
                    {
                        tst1 = RawBlas.upperhessenberg1norm(h, l, i, l, i, ref work, _params);
                    }
                    if ((double)(Math.Abs(h[k, k - 1])) <= (double)(Math.Max(ulp * tst1, smlnum)))
                    {
                        break;
                    }
                }
                l = k;
                if (l > 1)
                {

                    //
                    // H(L,L-1) is negligible.
                    //
                    h[l, l - 1] = 0;
                }

                //
                // Exit from loop if a submatrix of order <= MAXB has split off.
                //
                if (l >= i - maxb + 1)
                {
                    failflag = false;
                    break;
                }

                //
                // Now the active submatrix is in rows and columns L to I. If
                // eigenvalues only are being computed, only the active submatrix
                // need be transformed.
                //
                if (its == 20 || its == 30)
                {

                    //
                    // Exceptional shifts.
                    //
                    for (ii = i - ns + 1; ii <= i; ii++)
                    {
                        wr[ii] = cnst * (Math.Abs(h[ii, ii - 1]) + Math.Abs(h[ii, ii]));
                        wi[ii] = 0;
                    }
                }
                else
                {

                    //
                    // Use eigenvalues of trailing submatrix of order NS as shifts.
                    //
                    RawBlas.copymatrix(h, i - ns + 1, i, i - ns + 1, i, ref s, 1, ns, 1, ns, _params);
                    internalauxschur(false, false, ns, 1, ns, ref s, ref tmpwr, ref tmpwi, 1, ns, ref z, ref work, ref workv3, ref workc1, ref works1, ref ierr, _params);
                    for (p1 = 1; p1 <= ns; p1++)
                    {
                        wr[i - ns + p1] = tmpwr[p1];
                        wi[i - ns + p1] = tmpwi[p1];
                    }
                    if (ierr > 0)
                    {

                        //
                        // If DLAHQR failed to compute all NS eigenvalues, use the
                        // unconverged diagonal elements as the remaining shifts.
                        //
                        for (ii = 1; ii <= ierr; ii++)
                        {
                            wr[i - ns + ii] = s[ii, ii];
                            wi[i - ns + ii] = 0;
                        }
                    }
                }

                //
                // Form the first column of (G-w(1)) (G-w(2)) . . . (G-w(ns))
                // where G is the Hessenberg submatrix H(L:I,L:I) and w is
                // the vector of shifts (stored in WR and WI). The result is
                // stored in the local array V.
                //
                v[1] = 1;
                for (ii = 2; ii <= ns + 1; ii++)
                {
                    v[ii] = 0;
                }
                nv = 1;
                for (j = i - ns + 1; j <= i; j++)
                {
                    if ((double)(wi[j]) >= (double)(0))
                    {
                        if ((double)(wi[j]) == (double)(0))
                        {

                            //
                            // real shift
                            //
                            p1 = nv + 1;
                            for (i_ = 1; i_ <= p1; i_++)
                            {
                                vv[i_] = v[i_];
                            }
                            RawBlas.matrixvectormultiply(h, l, l + nv, l, l + nv - 1, false, vv, 1, nv, 1.0, ref v, 1, nv + 1, -wr[j], _params);
                            nv = nv + 1;
                        }
                        else
                        {
                            if ((double)(wi[j]) > (double)(0))
                            {

                                //
                                // complex conjugate pair of shifts
                                //
                                p1 = nv + 1;
                                for (i_ = 1; i_ <= p1; i_++)
                                {
                                    vv[i_] = v[i_];
                                }
                                RawBlas.matrixvectormultiply(h, l, l + nv, l, l + nv - 1, false, v, 1, nv, 1.0, ref vv, 1, nv + 1, -(2 * wr[j]), _params);
                                itemp = RawBlas.vectoridxabsmax(vv, 1, nv + 1, _params);
                                temp = 1 / Math.Max(Math.Abs(vv[itemp]), smlnum);
                                p1 = nv + 1;
                                for (i_ = 1; i_ <= p1; i_++)
                                {
                                    vv[i_] = temp * vv[i_];
                                }
                                absw = RawBlas.pythag2(wr[j], wi[j], _params);
                                temp = temp * absw * absw;
                                RawBlas.matrixvectormultiply(h, l, l + nv + 1, l, l + nv, false, vv, 1, nv + 1, 1.0, ref v, 1, nv + 2, temp, _params);
                                nv = nv + 2;
                            }
                        }

                        //
                        // Scale V(1:NV) so that max(abs(V(i))) = 1. If V is zero,
                        // reset it to the unit vector.
                        //
                        itemp = RawBlas.vectoridxabsmax(v, 1, nv, _params);
                        temp = Math.Abs(v[itemp]);
                        if ((double)(temp) == (double)(0))
                        {
                            v[1] = 1;
                            for (ii = 2; ii <= nv; ii++)
                            {
                                v[ii] = 0;
                            }
                        }
                        else
                        {
                            temp = Math.Max(temp, smlnum);
                            vt = 1 / temp;
                            for (i_ = 1; i_ <= nv; i_++)
                            {
                                v[i_] = vt * v[i_];
                            }
                        }
                    }
                }

                //
                // Multiple-shift QR step
                //
                for (k = l; k <= i - 1; k++)
                {

                    //
                    // The first iteration of this loop determines a reflection G
                    // from the vector V and applies it from left and right to H,
                    // thus creating a nonzero bulge below the subdiagonal.
                    //
                    // Each subsequent iteration determines a reflection G to
                    // restore the Hessenberg form in the (K-1)th column, and thus
                    // chases the bulge one step toward the bottom of the active
                    // submatrix. NR is the order of G.
                    //
                    nr = Math.Min(ns + 1, i - k + 1);
                    if (k > l)
                    {
                        p1 = k - 1;
                        p2 = k + nr - 1;
                        i1_ = (k) - (1);
                        for (i_ = 1; i_ <= nr; i_++)
                        {
                            v[i_] = h[i_ + i1_, p1];
                        }
                        apserv.touchint(ref p2, _params);
                    }
                    ablas.generatereflection(ref v, nr, ref tau, _params);
                    if (k > l)
                    {
                        h[k, k - 1] = v[1];
                        for (ii = k + 1; ii <= i; ii++)
                        {
                            h[ii, k - 1] = 0;
                        }
                    }
                    v[1] = 1;

                    //
                    // Apply G from the left to transform the rows of the matrix in
                    // columns K to I2.
                    //
                    ablas.applyreflectionfromtheleft(h, tau, v, k, k + nr - 1, k, i2, ref work, _params);

                    //
                    // Apply G from the right to transform the columns of the
                    // matrix in rows I1 to min(K+NR,I).
                    //
                    ablas.applyreflectionfromtheright(h, tau, v, i1, Math.Min(k + nr, i), k, k + nr - 1, ref work, _params);
                    if (wantz)
                    {

                        //
                        // Accumulate transformations in the matrix Z
                        //
                        ablas.applyreflectionfromtheright(z, tau, v, 1, n, k, k + nr - 1, ref work, _params);
                    }
                }
            }

            //
            // Failure to converge in remaining number of iterations
            //
            if (failflag)
            {
                info = i;
                return;
            }

            //
            // A submatrix of order <= MAXB in rows and columns L to I has split
            // off. Use the double-shift QR algorithm to handle it.
            //
            internalauxschur(wantt, wantz, n, l, i, ref h, ref wr, ref wi, 1, n, ref z, ref work, ref workv3, ref workc1, ref works1, ref info, _params);
            if (info > 0)
            {
                return;
            }

            //
            // Decrement number of remaining iterations, and return to start of
            // the main loop with a new value of I.
            //
            itn = itn - its;
            i = l - 1;

            //
            // Block below is never executed; it is necessary just to avoid
            // "unreachable code" warning about automatically generated code.
            //
            // We just need a way to transfer control to the end of the function,
            // even a fake way which is never actually traversed.
            //
            if (apserv.alwaysfalse(_params))
            {
                ap.assert(false);
                break;
            }
        }
    }


    /*************************************************************************
    Translation of DLAHQR from LAPACK.
    *************************************************************************/
    private static void internalauxschur(bool wantt,
        bool wantz,
        int n,
        int ilo,
        int ihi,
        ref double[,] h,
        ref double[] wr,
        ref double[] wi,
        int iloz,
        int ihiz,
        ref double[,] z,
        ref double[] work,
        ref double[] workv3,
        ref double[] workc1,
        ref double[] works1,
        ref int info,
        xparams _params)
    {
        double safmin = 0;
        double tst = 0;
        double ab = 0;
        double ba = 0;
        double aa = 0;
        double bb = 0;
        double rt1r = 0;
        double rt1i = 0;
        double rt2r = 0;
        double rt2i = 0;
        double tr = 0;
        double det = 0;
        double rtdisc = 0;
        double h21s = 0;
        int i = 0;
        int i1 = 0;
        int i2 = 0;
        int itmax = 0;
        int its = 0;
        int j = 0;
        int k = 0;
        int l = 0;
        int m = 0;
        int nh = 0;
        int nr = 0;
        int nz = 0;
        double cs = 0;
        double h11 = 0;
        double h12 = 0;
        double h21 = 0;
        double h22 = 0;
        double s = 0;
        double smlnum = 0;
        double sn = 0;
        double sum = 0;
        double t1 = 0;
        double t2 = 0;
        double t3 = 0;
        double v2 = 0;
        double v3 = 0;
        bool failflag = new bool();
        double dat1 = 0;
        double dat2 = 0;
        int p1 = 0;
        double him1im1 = 0;
        double him1i = 0;
        double hiim1 = 0;
        double hii = 0;
        double wrim1 = 0;
        double wri = 0;
        double wiim1 = 0;
        double wii = 0;
        double ulp = 0;

        info = 0;

        info = 0;
        dat1 = 0.75;
        dat2 = -0.4375;

        //
        // Quick return if possible
        //
        if (n == 0)
        {
            return;
        }
        if (ilo == ihi)
        {
            wr[ilo] = h[ilo, ilo];
            wi[ilo] = 0;
            return;
        }

        //
        // ==== clear out the trash ====
        //
        for (j = ilo; j <= ihi - 3; j++)
        {
            h[j + 2, j] = 0;
            h[j + 3, j] = 0;
        }
        if (ilo <= ihi - 2)
        {
            h[ihi, ihi - 2] = 0;
        }
        nh = ihi - ilo + 1;
        nz = ihiz - iloz + 1;

        //
        // Set machine-dependent constants for the stopping criterion.
        //
        safmin = math.minrealnumber;
        ulp = math.machineepsilon;
        smlnum = safmin * (nh / ulp);

        //
        // I1 and I2 are the indices of the first row and last column of H
        // to which transformations must be applied. If eigenvalues only are
        // being computed, I1 and I2 are set inside the main loop.
        //
        // Setting them to large negative value helps to debug possible errors
        // due to uninitialized variables; also it helps to avoid compiler
        // warnings.
        //
        i1 = -99999;
        i2 = -99999;
        if (wantt)
        {
            i1 = 1;
            i2 = n;
        }

        //
        // ITMAX is the total number of QR iterations allowed.
        //
        itmax = 30 * Math.Max(10, nh);

        //
        // The main loop begins here. I is the loop index and decreases from
        // IHI to ILO in steps of 1 or 2. Each iteration of the loop works
        // with the active submatrix in rows and columns L to I.
        // Eigenvalues I+1 to IHI have already converged. Either L = ILO or
        // H(L,L-1) is negligible so that the matrix splits.
        //
        i = ihi;
        while (true)
        {
            l = ilo;
            if (i < ilo)
            {
                return;
            }

            //
            // Perform QR iterations on rows and columns ILO to I until a
            // submatrix of order 1 or 2 splits off at the bottom because a
            // subdiagonal element has become negligible.
            //
            failflag = true;
            for (its = 0; its <= itmax; its++)
            {

                //
                // Look for a single small subdiagonal element.
                //
                for (k = i; k >= l + 1; k--)
                {
                    if ((double)(Math.Abs(h[k, k - 1])) <= (double)(smlnum))
                    {
                        break;
                    }
                    tst = Math.Abs(h[k - 1, k - 1]) + Math.Abs(h[k, k]);
                    if ((double)(tst) == (double)(0))
                    {
                        if (k - 2 >= ilo)
                        {
                            tst = tst + Math.Abs(h[k - 1, k - 2]);
                        }
                        if (k + 1 <= ihi)
                        {
                            tst = tst + Math.Abs(h[k + 1, k]);
                        }
                    }

                    //
                    // ==== The following is a conservative small subdiagonal
                    // .    deflation  criterion due to Ahues & Tisseur (LAWN 122,
                    // .    1997). It has better mathematical foundation and
                    // .    improves accuracy in some cases.  ====
                    //
                    if ((double)(Math.Abs(h[k, k - 1])) <= (double)(ulp * tst))
                    {
                        ab = Math.Max(Math.Abs(h[k, k - 1]), Math.Abs(h[k - 1, k]));
                        ba = Math.Min(Math.Abs(h[k, k - 1]), Math.Abs(h[k - 1, k]));
                        aa = Math.Max(Math.Abs(h[k, k]), Math.Abs(h[k - 1, k - 1] - h[k, k]));
                        bb = Math.Min(Math.Abs(h[k, k]), Math.Abs(h[k - 1, k - 1] - h[k, k]));
                        s = aa + ab;
                        if ((double)(ba * (ab / s)) <= (double)(Math.Max(smlnum, ulp * (bb * (aa / s)))))
                        {
                            break;
                        }
                    }
                }
                l = k;
                if (l > ilo)
                {

                    //
                    // H(L,L-1) is negligible
                    //
                    h[l, l - 1] = 0;
                }

                //
                // Exit from loop if a submatrix of order 1 or 2 has split off.
                //
                if (l >= i - 1)
                {
                    failflag = false;
                    break;
                }

                //
                // Now the active submatrix is in rows and columns L to I. If
                // eigenvalues only are being computed, only the active submatrix
                // need be transformed.
                //
                if (!wantt)
                {
                    i1 = l;
                    i2 = i;
                }

                //
                // Shifts
                //
                if (its == 10)
                {

                    //
                    // Exceptional shift.
                    //
                    s = Math.Abs(h[l + 1, l]) + Math.Abs(h[l + 2, l + 1]);
                    h11 = dat1 * s + h[l, l];
                    h12 = dat2 * s;
                    h21 = s;
                    h22 = h11;
                }
                else
                {
                    if (its == 20)
                    {

                        //
                        // Exceptional shift.
                        //
                        s = Math.Abs(h[i, i - 1]) + Math.Abs(h[i - 1, i - 2]);
                        h11 = dat1 * s + h[i, i];
                        h12 = dat2 * s;
                        h21 = s;
                        h22 = h11;
                    }
                    else
                    {

                        //
                        // Prepare to use Francis' double shift
                        // (i.e. 2nd degree generalized Rayleigh quotient)
                        //
                        h11 = h[i - 1, i - 1];
                        h21 = h[i, i - 1];
                        h12 = h[i - 1, i];
                        h22 = h[i, i];
                    }
                }
                s = Math.Abs(h11) + Math.Abs(h12) + Math.Abs(h21) + Math.Abs(h22);
                if ((double)(s) == (double)(0))
                {
                    rt1r = 0;
                    rt1i = 0;
                    rt2r = 0;
                    rt2i = 0;
                }
                else
                {
                    h11 = h11 / s;
                    h21 = h21 / s;
                    h12 = h12 / s;
                    h22 = h22 / s;
                    tr = (h11 + h22) / 2;
                    det = (h11 - tr) * (h22 - tr) - h12 * h21;
                    rtdisc = Math.Sqrt(Math.Abs(det));
                    if ((double)(det) >= (double)(0))
                    {

                        //
                        // ==== complex conjugate shifts ====
                        //
                        rt1r = tr * s;
                        rt2r = rt1r;
                        rt1i = rtdisc * s;
                        rt2i = -rt1i;
                    }
                    else
                    {

                        //
                        // ==== real shifts (use only one of them)  ====
                        //
                        rt1r = tr + rtdisc;
                        rt2r = tr - rtdisc;
                        if ((double)(Math.Abs(rt1r - h22)) <= (double)(Math.Abs(rt2r - h22)))
                        {
                            rt1r = rt1r * s;
                            rt2r = rt1r;
                        }
                        else
                        {
                            rt2r = rt2r * s;
                            rt1r = rt2r;
                        }
                        rt1i = 0;
                        rt2i = 0;
                    }
                }

                //
                // Look for two consecutive small subdiagonal elements.
                //
                for (m = i - 2; m >= l; m--)
                {

                    //
                    // Determine the effect of starting the double-shift QR
                    // iteration at row M, and see if this would make H(M,M-1)
                    // negligible.  (The following uses scaling to avoid
                    // overflows and most underflows.)
                    //
                    h21s = h[m + 1, m];
                    s = Math.Abs(h[m, m] - rt2r) + Math.Abs(rt2i) + Math.Abs(h21s);
                    h21s = h[m + 1, m] / s;
                    workv3[1] = h21s * h[m, m + 1] + (h[m, m] - rt1r) * ((h[m, m] - rt2r) / s) - rt1i * (rt2i / s);
                    workv3[2] = h21s * (h[m, m] + h[m + 1, m + 1] - rt1r - rt2r);
                    workv3[3] = h21s * h[m + 2, m + 1];
                    s = Math.Abs(workv3[1]) + Math.Abs(workv3[2]) + Math.Abs(workv3[3]);
                    workv3[1] = workv3[1] / s;
                    workv3[2] = workv3[2] / s;
                    workv3[3] = workv3[3] / s;
                    if (m == l)
                    {
                        break;
                    }
                    if ((double)(Math.Abs(h[m, m - 1]) * (Math.Abs(workv3[2]) + Math.Abs(workv3[3]))) <= (double)(ulp * Math.Abs(workv3[1]) * (Math.Abs(h[m - 1, m - 1]) + Math.Abs(h[m, m]) + Math.Abs(h[m + 1, m + 1]))))
                    {
                        break;
                    }
                }

                //
                // Double-shift QR step
                //
                for (k = m; k <= i - 1; k++)
                {

                    //
                    // The first iteration of this loop determines a reflection G
                    // from the vector V and applies it from left and right to H,
                    // thus creating a nonzero bulge below the subdiagonal.
                    //
                    // Each subsequent iteration determines a reflection G to
                    // restore the Hessenberg form in the (K-1)th column, and thus
                    // chases the bulge one step toward the bottom of the active
                    // submatrix. NR is the order of G.
                    //
                    nr = Math.Min(3, i - k + 1);
                    if (k > m)
                    {
                        for (p1 = 1; p1 <= nr; p1++)
                        {
                            workv3[p1] = h[k + p1 - 1, k - 1];
                        }
                    }
                    ablas.generatereflection(ref workv3, nr, ref t1, _params);
                    if (k > m)
                    {
                        h[k, k - 1] = workv3[1];
                        h[k + 1, k - 1] = 0;
                        if (k < i - 1)
                        {
                            h[k + 2, k - 1] = 0;
                        }
                    }
                    else
                    {
                        if (m > l)
                        {

                            //
                            // ==== Use the following instead of
                            // H( K, K-1 ) = -H( K, K-1 ) to
                            // avoid a bug when v(2) and v(3)
                            // underflow. ====
                            //
                            h[k, k - 1] = h[k, k - 1] * (1 - t1);
                        }
                    }
                    v2 = workv3[2];
                    t2 = t1 * v2;
                    if (nr == 3)
                    {
                        v3 = workv3[3];
                        t3 = t1 * v3;

                        //
                        // Apply G from the left to transform the rows of the matrix
                        // in columns K to I2.
                        //
                        for (j = k; j <= i2; j++)
                        {
                            sum = h[k, j] + v2 * h[k + 1, j] + v3 * h[k + 2, j];
                            h[k, j] = h[k, j] - sum * t1;
                            h[k + 1, j] = h[k + 1, j] - sum * t2;
                            h[k + 2, j] = h[k + 2, j] - sum * t3;
                        }

                        //
                        // Apply G from the right to transform the columns of the
                        // matrix in rows I1 to min(K+3,I).
                        //
                        for (j = i1; j <= Math.Min(k + 3, i); j++)
                        {
                            sum = h[j, k] + v2 * h[j, k + 1] + v3 * h[j, k + 2];
                            h[j, k] = h[j, k] - sum * t1;
                            h[j, k + 1] = h[j, k + 1] - sum * t2;
                            h[j, k + 2] = h[j, k + 2] - sum * t3;
                        }
                        if (wantz)
                        {

                            //
                            // Accumulate transformations in the matrix Z
                            //
                            for (j = iloz; j <= ihiz; j++)
                            {
                                sum = z[j, k] + v2 * z[j, k + 1] + v3 * z[j, k + 2];
                                z[j, k] = z[j, k] - sum * t1;
                                z[j, k + 1] = z[j, k + 1] - sum * t2;
                                z[j, k + 2] = z[j, k + 2] - sum * t3;
                            }
                        }
                    }
                    else
                    {
                        if (nr == 2)
                        {

                            //
                            // Apply G from the left to transform the rows of the matrix
                            // in columns K to I2.
                            //
                            for (j = k; j <= i2; j++)
                            {
                                sum = h[k, j] + v2 * h[k + 1, j];
                                h[k, j] = h[k, j] - sum * t1;
                                h[k + 1, j] = h[k + 1, j] - sum * t2;
                            }

                            //
                            // Apply G from the right to transform the columns of the
                            // matrix in rows I1 to min(K+3,I).
                            //
                            for (j = i1; j <= i; j++)
                            {
                                sum = h[j, k] + v2 * h[j, k + 1];
                                h[j, k] = h[j, k] - sum * t1;
                                h[j, k + 1] = h[j, k + 1] - sum * t2;
                            }
                            if (wantz)
                            {

                                //
                                // Accumulate transformations in the matrix Z
                                //
                                for (j = iloz; j <= ihiz; j++)
                                {
                                    sum = z[j, k] + v2 * z[j, k + 1];
                                    z[j, k] = z[j, k] - sum * t1;
                                    z[j, k + 1] = z[j, k + 1] - sum * t2;
                                }
                            }
                        }
                    }
                }
            }

            //
            // Failure to converge in remaining number of iterations
            //
            if (failflag)
            {
                info = i;
                return;
            }

            //
            // Convergence
            //
            if (l == i)
            {

                //
                // H(I,I-1) is negligible: one eigenvalue has converged.
                //
                wr[i] = h[i, i];
                wi[i] = 0;
            }
            else
            {
                if (l == i - 1)
                {

                    //
                    // H(I-1,I-2) is negligible: a pair of eigenvalues have converged.
                    //
                    // Transform the 2-by-2 submatrix to standard Schur form,
                    // and compute and store the eigenvalues.
                    //
                    him1im1 = h[i - 1, i - 1];
                    him1i = h[i - 1, i];
                    hiim1 = h[i, i - 1];
                    hii = h[i, i];
                    aux2x2schur(ref him1im1, ref him1i, ref hiim1, ref hii, ref wrim1, ref wiim1, ref wri, ref wii, ref cs, ref sn, _params);
                    wr[i - 1] = wrim1;
                    wi[i - 1] = wiim1;
                    wr[i] = wri;
                    wi[i] = wii;
                    h[i - 1, i - 1] = him1im1;
                    h[i - 1, i] = him1i;
                    h[i, i - 1] = hiim1;
                    h[i, i] = hii;
                    if (wantt)
                    {

                        //
                        // Apply the transformation to the rest of H.
                        //
                        if (i2 > i)
                        {
                            workc1[1] = cs;
                            works1[1] = sn;
                            rotations.applyrotationsfromtheleft(true, i - 1, i, i + 1, i2, workc1, works1, h, work, _params);
                        }
                        workc1[1] = cs;
                        works1[1] = sn;
                        rotations.applyrotationsfromtheright(true, i1, i - 2, i - 1, i, workc1, works1, h, work, _params);
                    }
                    if (wantz)
                    {

                        //
                        // Apply the transformation to Z.
                        //
                        workc1[1] = cs;
                        works1[1] = sn;
                        rotations.applyrotationsfromtheright(true, iloz, iloz + nz - 1, i - 1, i, workc1, works1, z, work, _params);
                    }
                }
            }

            //
            // return to start of the main loop with new value of I.
            //
            i = l - 1;
        }
    }


    private static void aux2x2schur(ref double a,
        ref double b,
        ref double c,
        ref double d,
        ref double rt1r,
        ref double rt1i,
        ref double rt2r,
        ref double rt2i,
        ref double cs,
        ref double sn,
        xparams _params)
    {
        double multpl = 0;
        double aa = 0;
        double bb = 0;
        double bcmax = 0;
        double bcmis = 0;
        double cc = 0;
        double cs1 = 0;
        double dd = 0;
        double eps = 0;
        double p = 0;
        double sab = 0;
        double sac = 0;
        double scl = 0;
        double sigma = 0;
        double sn1 = 0;
        double tau = 0;
        double temp = 0;
        double z = 0;

        rt1r = 0;
        rt1i = 0;
        rt2r = 0;
        rt2i = 0;
        cs = 0;
        sn = 0;

        multpl = 4.0;
        eps = math.machineepsilon;
        if ((double)(c) == (double)(0))
        {
            cs = 1;
            sn = 0;
        }
        else
        {
            if ((double)(b) == (double)(0))
            {

                //
                // Swap rows and columns
                //
                cs = 0;
                sn = 1;
                temp = d;
                d = a;
                a = temp;
                b = -c;
                c = 0;
            }
            else
            {
                if ((double)(a - d) == (double)(0) && extschursigntoone(b, _params) != extschursigntoone(c, _params))
                {
                    cs = 1;
                    sn = 0;
                }
                else
                {
                    temp = a - d;
                    p = 0.5 * temp;
                    bcmax = Math.Max(Math.Abs(b), Math.Abs(c));
                    bcmis = Math.Min(Math.Abs(b), Math.Abs(c)) * extschursigntoone(b, _params) * extschursigntoone(c, _params);
                    scl = Math.Max(Math.Abs(p), bcmax);
                    z = p / scl * p + bcmax / scl * bcmis;

                    //
                    // If Z is of the order of the machine accuracy, postpone the
                    // decision on the nature of eigenvalues
                    //
                    if ((double)(z) >= (double)(multpl * eps))
                    {

                        //
                        // Real eigenvalues. Compute A and D.
                        //
                        z = p + extschursign(Math.Sqrt(scl) * Math.Sqrt(z), p, _params);
                        a = d + z;
                        d = d - bcmax / z * bcmis;

                        //
                        // Compute B and the rotation matrix
                        //
                        tau = RawBlas.pythag2(c, z, _params);
                        cs = z / tau;
                        sn = c / tau;
                        b = b - c;
                        c = 0;
                    }
                    else
                    {

                        //
                        // Complex eigenvalues, or real (almost) equal eigenvalues.
                        // Make diagonal elements equal.
                        //
                        sigma = b + c;
                        tau = RawBlas.pythag2(sigma, temp, _params);
                        cs = Math.Sqrt(0.5 * (1 + Math.Abs(sigma) / tau));
                        sn = -(p / (tau * cs) * extschursign(1, sigma, _params));

                        //
                        // Compute [ AA  BB ] = [ A  B ] [ CS -SN ]
                        //         [ CC  DD ]   [ C  D ] [ SN  CS ]
                        //
                        aa = a * cs + b * sn;
                        bb = -(a * sn) + b * cs;
                        cc = c * cs + d * sn;
                        dd = -(c * sn) + d * cs;

                        //
                        // Compute [ A  B ] = [ CS  SN ] [ AA  BB ]
                        //         [ C  D ]   [-SN  CS ] [ CC  DD ]
                        //
                        a = aa * cs + cc * sn;
                        b = bb * cs + dd * sn;
                        c = -(aa * sn) + cc * cs;
                        d = -(bb * sn) + dd * cs;
                        temp = 0.5 * (a + d);
                        a = temp;
                        d = temp;
                        if ((double)(c) != (double)(0))
                        {
                            if ((double)(b) != (double)(0))
                            {
                                if (extschursigntoone(b, _params) == extschursigntoone(c, _params))
                                {

                                    //
                                    // Real eigenvalues: reduce to upper triangular form
                                    //
                                    sab = Math.Sqrt(Math.Abs(b));
                                    sac = Math.Sqrt(Math.Abs(c));
                                    p = extschursign(sab * sac, c, _params);
                                    tau = 1 / Math.Sqrt(Math.Abs(b + c));
                                    a = temp + p;
                                    d = temp - p;
                                    b = b - c;
                                    c = 0;
                                    cs1 = sab * tau;
                                    sn1 = sac * tau;
                                    temp = cs * cs1 - sn * sn1;
                                    sn = cs * sn1 + sn * cs1;
                                    cs = temp;
                                }
                            }
                            else
                            {
                                b = -c;
                                c = 0;
                                temp = cs;
                                cs = -sn;
                                sn = temp;
                            }
                        }
                    }
                }
            }
        }

        //
        // Store eigenvalues in (RT1R,RT1I) and (RT2R,RT2I).
        //
        rt1r = a;
        rt2r = d;
        if ((double)(c) == (double)(0))
        {
            rt1i = 0;
            rt2i = 0;
        }
        else
        {
            rt1i = Math.Sqrt(Math.Abs(b)) * Math.Sqrt(Math.Abs(c));
            rt2i = -rt1i;
        }
    }


    private static double extschursign(double a,
        double b,
        xparams _params)
    {
        double result = 0;

        if ((double)(b) >= (double)(0))
        {
            result = Math.Abs(a);
        }
        else
        {
            result = -Math.Abs(a);
        }
        return result;
    }


    private static int extschursigntoone(double b,
        xparams _params)
    {
        int result = 0;

        if ((double)(b) >= (double)(0))
        {
            result = 1;
        }
        else
        {
            result = -1;
        }
        return result;
    }


}
