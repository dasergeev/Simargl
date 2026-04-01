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

public class safesolve
{
    /*************************************************************************
    Real implementation of CMatrixScaledTRSafeSolve

      -- ALGLIB routine --
         21.01.2010
         Bochkanov Sergey
    *************************************************************************/
    public static bool rmatrixscaledtrsafesolve(double[,] a,
        double sa,
        int n,
        ref double[] x,
        bool isupper,
        int trans,
        bool isunit,
        double maxgrowth,
        xparams _params)
    {
        bool result = new bool();
        double lnmax = 0;
        double nrmb = 0;
        double nrmx = 0;
        int i = 0;
        complex alpha = 0;
        complex beta = 0;
        double vr = 0;
        complex cx = 0;
        double[] tmp = new double[0];
        int i_ = 0;

        ap.assert(n > 0, "RMatrixTRSafeSolve: incorrect N!");
        ap.assert(trans == 0 || trans == 1, "RMatrixTRSafeSolve: incorrect Trans!");
        result = true;
        lnmax = Math.Log(math.maxrealnumber);

        //
        // Quick return if possible
        //
        if (n <= 0)
        {
            return result;
        }

        //
        // Load norms: right part and X
        //
        nrmb = 0;
        for (i = 0; i <= n - 1; i++)
        {
            nrmb = Math.Max(nrmb, Math.Abs(x[i]));
        }
        nrmx = 0;

        //
        // Solve
        //
        tmp = new double[n];
        result = true;
        if (isupper && trans == 0)
        {

            //
            // U*x = b
            //
            for (i = n - 1; i >= 0; i--)
            {

                //
                // Task is reduced to alpha*x[i] = beta
                //
                if (isunit)
                {
                    alpha = sa;
                }
                else
                {
                    alpha = a[i, i] * sa;
                }
                if (i < n - 1)
                {
                    for (i_ = i + 1; i_ <= n - 1; i_++)
                    {
                        tmp[i_] = sa * a[i, i_];
                    }
                    vr = 0.0;
                    for (i_ = i + 1; i_ <= n - 1; i_++)
                    {
                        vr += tmp[i_] * x[i_];
                    }
                    beta = x[i] - vr;
                }
                else
                {
                    beta = x[i];
                }

                //
                // solve alpha*x[i] = beta
                //
                result = cbasicsolveandupdate(alpha, beta, lnmax, nrmb, maxgrowth, ref nrmx, ref cx, _params);
                if (!result)
                {
                    return result;
                }
                x[i] = cx.x;
            }
            return result;
        }
        if (!isupper && trans == 0)
        {

            //
            // L*x = b
            //
            for (i = 0; i <= n - 1; i++)
            {

                //
                // Task is reduced to alpha*x[i] = beta
                //
                if (isunit)
                {
                    alpha = sa;
                }
                else
                {
                    alpha = a[i, i] * sa;
                }
                if (i > 0)
                {
                    for (i_ = 0; i_ <= i - 1; i_++)
                    {
                        tmp[i_] = sa * a[i, i_];
                    }
                    vr = 0.0;
                    for (i_ = 0; i_ <= i - 1; i_++)
                    {
                        vr += tmp[i_] * x[i_];
                    }
                    beta = x[i] - vr;
                }
                else
                {
                    beta = x[i];
                }

                //
                // solve alpha*x[i] = beta
                //
                result = cbasicsolveandupdate(alpha, beta, lnmax, nrmb, maxgrowth, ref nrmx, ref cx, _params);
                if (!result)
                {
                    return result;
                }
                x[i] = cx.x;
            }
            return result;
        }
        if (isupper && trans == 1)
        {

            //
            // U^T*x = b
            //
            for (i = 0; i <= n - 1; i++)
            {

                //
                // Task is reduced to alpha*x[i] = beta
                //
                if (isunit)
                {
                    alpha = sa;
                }
                else
                {
                    alpha = a[i, i] * sa;
                }
                beta = x[i];

                //
                // solve alpha*x[i] = beta
                //
                result = cbasicsolveandupdate(alpha, beta, lnmax, nrmb, maxgrowth, ref nrmx, ref cx, _params);
                if (!result)
                {
                    return result;
                }
                x[i] = cx.x;

                //
                // update the rest of right part
                //
                if (i < n - 1)
                {
                    vr = cx.x;
                    for (i_ = i + 1; i_ <= n - 1; i_++)
                    {
                        tmp[i_] = sa * a[i, i_];
                    }
                    for (i_ = i + 1; i_ <= n - 1; i_++)
                    {
                        x[i_] = x[i_] - vr * tmp[i_];
                    }
                }
            }
            return result;
        }
        if (!isupper && trans == 1)
        {

            //
            // L^T*x = b
            //
            for (i = n - 1; i >= 0; i--)
            {

                //
                // Task is reduced to alpha*x[i] = beta
                //
                if (isunit)
                {
                    alpha = sa;
                }
                else
                {
                    alpha = a[i, i] * sa;
                }
                beta = x[i];

                //
                // solve alpha*x[i] = beta
                //
                result = cbasicsolveandupdate(alpha, beta, lnmax, nrmb, maxgrowth, ref nrmx, ref cx, _params);
                if (!result)
                {
                    return result;
                }
                x[i] = cx.x;

                //
                // update the rest of right part
                //
                if (i > 0)
                {
                    vr = cx.x;
                    for (i_ = 0; i_ <= i - 1; i_++)
                    {
                        tmp[i_] = sa * a[i, i_];
                    }
                    for (i_ = 0; i_ <= i - 1; i_++)
                    {
                        x[i_] = x[i_] - vr * tmp[i_];
                    }
                }
            }
            return result;
        }
        result = false;
        return result;
    }


    /*************************************************************************
    Internal subroutine for safe solution of

        SA*op(A)=b
        
    where  A  is  NxN  upper/lower  triangular/unitriangular  matrix, op(A) is
    either identity transform, transposition or Hermitian transposition, SA is
    a scaling factor such that max(|SA*A[i,j]|) is close to 1.0 in magnutude.

    This subroutine  limits  relative  growth  of  solution  (in inf-norm)  by
    MaxGrowth,  returning  False  if  growth  exceeds MaxGrowth. Degenerate or
    near-degenerate matrices are handled correctly (False is returned) as long
    as MaxGrowth is significantly less than MaxRealNumber/norm(b).

      -- ALGLIB routine --
         21.01.2010
         Bochkanov Sergey
    *************************************************************************/
    public static bool cmatrixscaledtrsafesolve(complex[,] a,
        double sa,
        int n,
        ref complex[] x,
        bool isupper,
        int trans,
        bool isunit,
        double maxgrowth,
        xparams _params)
    {
        bool result = new bool();
        double lnmax = 0;
        double nrmb = 0;
        double nrmx = 0;
        int i = 0;
        complex alpha = 0;
        complex beta = 0;
        complex vc = 0;
        complex[] tmp = new complex[0];
        int i_ = 0;

        ap.assert(n > 0, "CMatrixTRSafeSolve: incorrect N!");
        ap.assert((trans == 0 || trans == 1) || trans == 2, "CMatrixTRSafeSolve: incorrect Trans!");
        result = true;
        lnmax = Math.Log(math.maxrealnumber);

        //
        // Quick return if possible
        //
        if (n <= 0)
        {
            return result;
        }

        //
        // Load norms: right part and X
        //
        nrmb = 0;
        for (i = 0; i <= n - 1; i++)
        {
            nrmb = Math.Max(nrmb, math.abscomplex(x[i]));
        }
        nrmx = 0;

        //
        // Solve
        //
        tmp = new complex[n];
        result = true;
        if (isupper && trans == 0)
        {

            //
            // U*x = b
            //
            for (i = n - 1; i >= 0; i--)
            {

                //
                // Task is reduced to alpha*x[i] = beta
                //
                if (isunit)
                {
                    alpha = sa;
                }
                else
                {
                    alpha = a[i, i] * sa;
                }
                if (i < n - 1)
                {
                    for (i_ = i + 1; i_ <= n - 1; i_++)
                    {
                        tmp[i_] = sa * a[i, i_];
                    }
                    vc = 0.0;
                    for (i_ = i + 1; i_ <= n - 1; i_++)
                    {
                        vc += tmp[i_] * x[i_];
                    }
                    beta = x[i] - vc;
                }
                else
                {
                    beta = x[i];
                }

                //
                // solve alpha*x[i] = beta
                //
                result = cbasicsolveandupdate(alpha, beta, lnmax, nrmb, maxgrowth, ref nrmx, ref vc, _params);
                if (!result)
                {
                    return result;
                }
                x[i] = vc;
            }
            return result;
        }
        if (!isupper && trans == 0)
        {

            //
            // L*x = b
            //
            for (i = 0; i <= n - 1; i++)
            {

                //
                // Task is reduced to alpha*x[i] = beta
                //
                if (isunit)
                {
                    alpha = sa;
                }
                else
                {
                    alpha = a[i, i] * sa;
                }
                if (i > 0)
                {
                    for (i_ = 0; i_ <= i - 1; i_++)
                    {
                        tmp[i_] = sa * a[i, i_];
                    }
                    vc = 0.0;
                    for (i_ = 0; i_ <= i - 1; i_++)
                    {
                        vc += tmp[i_] * x[i_];
                    }
                    beta = x[i] - vc;
                }
                else
                {
                    beta = x[i];
                }

                //
                // solve alpha*x[i] = beta
                //
                result = cbasicsolveandupdate(alpha, beta, lnmax, nrmb, maxgrowth, ref nrmx, ref vc, _params);
                if (!result)
                {
                    return result;
                }
                x[i] = vc;
            }
            return result;
        }
        if (isupper && trans == 1)
        {

            //
            // U^T*x = b
            //
            for (i = 0; i <= n - 1; i++)
            {

                //
                // Task is reduced to alpha*x[i] = beta
                //
                if (isunit)
                {
                    alpha = sa;
                }
                else
                {
                    alpha = a[i, i] * sa;
                }
                beta = x[i];

                //
                // solve alpha*x[i] = beta
                //
                result = cbasicsolveandupdate(alpha, beta, lnmax, nrmb, maxgrowth, ref nrmx, ref vc, _params);
                if (!result)
                {
                    return result;
                }
                x[i] = vc;

                //
                // update the rest of right part
                //
                if (i < n - 1)
                {
                    for (i_ = i + 1; i_ <= n - 1; i_++)
                    {
                        tmp[i_] = sa * a[i, i_];
                    }
                    for (i_ = i + 1; i_ <= n - 1; i_++)
                    {
                        x[i_] = x[i_] - vc * tmp[i_];
                    }
                }
            }
            return result;
        }
        if (!isupper && trans == 1)
        {

            //
            // L^T*x = b
            //
            for (i = n - 1; i >= 0; i--)
            {

                //
                // Task is reduced to alpha*x[i] = beta
                //
                if (isunit)
                {
                    alpha = sa;
                }
                else
                {
                    alpha = a[i, i] * sa;
                }
                beta = x[i];

                //
                // solve alpha*x[i] = beta
                //
                result = cbasicsolveandupdate(alpha, beta, lnmax, nrmb, maxgrowth, ref nrmx, ref vc, _params);
                if (!result)
                {
                    return result;
                }
                x[i] = vc;

                //
                // update the rest of right part
                //
                if (i > 0)
                {
                    for (i_ = 0; i_ <= i - 1; i_++)
                    {
                        tmp[i_] = sa * a[i, i_];
                    }
                    for (i_ = 0; i_ <= i - 1; i_++)
                    {
                        x[i_] = x[i_] - vc * tmp[i_];
                    }
                }
            }
            return result;
        }
        if (isupper && trans == 2)
        {

            //
            // U^H*x = b
            //
            for (i = 0; i <= n - 1; i++)
            {

                //
                // Task is reduced to alpha*x[i] = beta
                //
                if (isunit)
                {
                    alpha = sa;
                }
                else
                {
                    alpha = math.conj(a[i, i]) * sa;
                }
                beta = x[i];

                //
                // solve alpha*x[i] = beta
                //
                result = cbasicsolveandupdate(alpha, beta, lnmax, nrmb, maxgrowth, ref nrmx, ref vc, _params);
                if (!result)
                {
                    return result;
                }
                x[i] = vc;

                //
                // update the rest of right part
                //
                if (i < n - 1)
                {
                    for (i_ = i + 1; i_ <= n - 1; i_++)
                    {
                        tmp[i_] = sa * math.conj(a[i, i_]);
                    }
                    for (i_ = i + 1; i_ <= n - 1; i_++)
                    {
                        x[i_] = x[i_] - vc * tmp[i_];
                    }
                }
            }
            return result;
        }
        if (!isupper && trans == 2)
        {

            //
            // L^T*x = b
            //
            for (i = n - 1; i >= 0; i--)
            {

                //
                // Task is reduced to alpha*x[i] = beta
                //
                if (isunit)
                {
                    alpha = sa;
                }
                else
                {
                    alpha = math.conj(a[i, i]) * sa;
                }
                beta = x[i];

                //
                // solve alpha*x[i] = beta
                //
                result = cbasicsolveandupdate(alpha, beta, lnmax, nrmb, maxgrowth, ref nrmx, ref vc, _params);
                if (!result)
                {
                    return result;
                }
                x[i] = vc;

                //
                // update the rest of right part
                //
                if (i > 0)
                {
                    for (i_ = 0; i_ <= i - 1; i_++)
                    {
                        tmp[i_] = sa * math.conj(a[i, i_]);
                    }
                    for (i_ = 0; i_ <= i - 1; i_++)
                    {
                        x[i_] = x[i_] - vc * tmp[i_];
                    }
                }
            }
            return result;
        }
        result = false;
        return result;
    }


    /*************************************************************************
    complex basic solver-updater for reduced linear system

        alpha*x[i] = beta

    solves this equation and updates it in overlfow-safe manner (keeping track
    of relative growth of solution).

    Parameters:
        Alpha   -   alpha
        Beta    -   beta
        LnMax   -   precomputed Ln(MaxRealNumber)
        BNorm   -   inf-norm of b (right part of original system)
        MaxGrowth-  maximum growth of norm(x) relative to norm(b)
        XNorm   -   inf-norm of other components of X (which are already processed)
                    it is updated by CBasicSolveAndUpdate.
        X       -   solution

      -- ALGLIB routine --
         26.01.2009
         Bochkanov Sergey
    *************************************************************************/
    private static bool cbasicsolveandupdate(complex alpha,
        complex beta,
        double lnmax,
        double bnorm,
        double maxgrowth,
        ref double xnorm,
        ref complex x,
        xparams _params)
    {
        bool result = new bool();
        double v = 0;

        x = 0;

        result = false;
        if (alpha == 0)
        {
            return result;
        }
        if (beta != 0)
        {

            //
            // alpha*x[i]=beta
            //
            v = Math.Log(math.abscomplex(beta)) - Math.Log(math.abscomplex(alpha));
            if ((double)(v) > (double)(lnmax))
            {
                return result;
            }
            x = beta / alpha;
        }
        else
        {

            //
            // alpha*x[i]=0
            //
            x = 0;
        }

        //
        // update NrmX, test growth limit
        //
        xnorm = Math.Max(xnorm, math.abscomplex(x));
        if ((double)(xnorm) > (double)(maxgrowth * bnorm))
        {
            return result;
        }
        result = true;
        return result;
    }


}
