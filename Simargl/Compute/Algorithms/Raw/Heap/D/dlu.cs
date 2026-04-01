#pragma warning disable CS1591

using System;

namespace Simargl.Algorithms.Raw;

public class dlu
{
    /*************************************************************************
    Recurrent complex LU subroutine.
    Never call it directly.

      -- ALGLIB routine --
         04.01.2010
         Bochkanov Sergey
    *************************************************************************/
    public static void cmatrixluprec(ref complex[,] a,
        int offs,
        int m,
        int n,
        ref int[] pivots,
        ref complex[] tmp,
        xparams _params)
    {
        int i = 0;
        int m1 = 0;
        int m2 = 0;
        int i_ = 0;
        int i1_ = 0;

        if (Math.Min(m, n) <= ablas.ablascomplexblocksize(a, _params))
        {
            cmatrixlup2(ref a, offs, m, n, ref pivots, ref tmp, _params);
            return;
        }
        if (m > n)
        {
            cmatrixluprec(ref a, offs, n, n, ref pivots, ref tmp, _params);
            for (i = 0; i <= n - 1; i++)
            {
                i1_ = (offs + n) - (0);
                for (i_ = 0; i_ <= m - n - 1; i_++)
                {
                    tmp[i_] = a[i_ + i1_, offs + i];
                }
                for (i_ = offs + n; i_ <= offs + m - 1; i_++)
                {
                    a[i_, offs + i] = a[i_, pivots[offs + i]];
                }
                i1_ = (0) - (offs + n);
                for (i_ = offs + n; i_ <= offs + m - 1; i_++)
                {
                    a[i_, pivots[offs + i]] = tmp[i_ + i1_];
                }
            }
            ablas.cmatrixrighttrsm(m - n, n, a, offs, offs, true, true, 0, a, offs + n, offs, _params);
            return;
        }
        ablas.ablascomplexsplitlength(a, m, ref m1, ref m2, _params);
        cmatrixluprec(ref a, offs, m1, n, ref pivots, ref tmp, _params);
        if (m2 > 0)
        {
            for (i = 0; i <= m1 - 1; i++)
            {
                if (offs + i != pivots[offs + i])
                {
                    i1_ = (offs + m1) - (0);
                    for (i_ = 0; i_ <= m2 - 1; i_++)
                    {
                        tmp[i_] = a[i_ + i1_, offs + i];
                    }
                    for (i_ = offs + m1; i_ <= offs + m - 1; i_++)
                    {
                        a[i_, offs + i] = a[i_, pivots[offs + i]];
                    }
                    i1_ = (0) - (offs + m1);
                    for (i_ = offs + m1; i_ <= offs + m - 1; i_++)
                    {
                        a[i_, pivots[offs + i]] = tmp[i_ + i1_];
                    }
                }
            }
            ablas.cmatrixrighttrsm(m2, m1, a, offs, offs, true, true, 0, a, offs + m1, offs, _params);
            ablas.cmatrixgemm(m - m1, n - m1, m1, -1.0, a, offs + m1, offs, 0, a, offs, offs + m1, 0, 1.0, a, offs + m1, offs + m1, _params);
            cmatrixluprec(ref a, offs + m1, m - m1, n - m1, ref pivots, ref tmp, _params);
            for (i = 0; i <= m2 - 1; i++)
            {
                if (offs + m1 + i != pivots[offs + m1 + i])
                {
                    i1_ = (offs) - (0);
                    for (i_ = 0; i_ <= m1 - 1; i_++)
                    {
                        tmp[i_] = a[i_ + i1_, offs + m1 + i];
                    }
                    for (i_ = offs; i_ <= offs + m1 - 1; i_++)
                    {
                        a[i_, offs + m1 + i] = a[i_, pivots[offs + m1 + i]];
                    }
                    i1_ = (0) - (offs);
                    for (i_ = offs; i_ <= offs + m1 - 1; i_++)
                    {
                        a[i_, pivots[offs + m1 + i]] = tmp[i_ + i1_];
                    }
                }
            }
        }
    }


    /*************************************************************************
    Recurrent real LU subroutine.
    Never call it directly.

      -- ALGLIB routine --
         04.01.2010
         Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixluprec(ref double[,] a,
        int offs,
        int m,
        int n,
        ref int[] pivots,
        ref double[] tmp,
        xparams _params)
    {
        int i = 0;
        int m1 = 0;
        int m2 = 0;
        int i_ = 0;
        int i1_ = 0;

        if (Math.Min(m, n) <= ablas.ablasblocksize(a, _params))
        {
            rmatrixlup2(ref a, offs, m, n, ref pivots, ref tmp, _params);
            return;
        }
        if (m > n)
        {
            rmatrixluprec(ref a, offs, n, n, ref pivots, ref tmp, _params);
            for (i = 0; i <= n - 1; i++)
            {
                if (offs + i != pivots[offs + i])
                {
                    i1_ = (offs + n) - (0);
                    for (i_ = 0; i_ <= m - n - 1; i_++)
                    {
                        tmp[i_] = a[i_ + i1_, offs + i];
                    }
                    for (i_ = offs + n; i_ <= offs + m - 1; i_++)
                    {
                        a[i_, offs + i] = a[i_, pivots[offs + i]];
                    }
                    i1_ = (0) - (offs + n);
                    for (i_ = offs + n; i_ <= offs + m - 1; i_++)
                    {
                        a[i_, pivots[offs + i]] = tmp[i_ + i1_];
                    }
                }
            }
            ablas.rmatrixrighttrsm(m - n, n, a, offs, offs, true, true, 0, a, offs + n, offs, _params);
            return;
        }
        ablas.ablassplitlength(a, m, ref m1, ref m2, _params);
        rmatrixluprec(ref a, offs, m1, n, ref pivots, ref tmp, _params);
        if (m2 > 0)
        {
            for (i = 0; i <= m1 - 1; i++)
            {
                if (offs + i != pivots[offs + i])
                {
                    i1_ = (offs + m1) - (0);
                    for (i_ = 0; i_ <= m2 - 1; i_++)
                    {
                        tmp[i_] = a[i_ + i1_, offs + i];
                    }
                    for (i_ = offs + m1; i_ <= offs + m - 1; i_++)
                    {
                        a[i_, offs + i] = a[i_, pivots[offs + i]];
                    }
                    i1_ = (0) - (offs + m1);
                    for (i_ = offs + m1; i_ <= offs + m - 1; i_++)
                    {
                        a[i_, pivots[offs + i]] = tmp[i_ + i1_];
                    }
                }
            }
            ablas.rmatrixrighttrsm(m2, m1, a, offs, offs, true, true, 0, a, offs + m1, offs, _params);
            ablas.rmatrixgemm(m - m1, n - m1, m1, -1.0, a, offs + m1, offs, 0, a, offs, offs + m1, 0, 1.0, a, offs + m1, offs + m1, _params);
            rmatrixluprec(ref a, offs + m1, m - m1, n - m1, ref pivots, ref tmp, _params);
            for (i = 0; i <= m2 - 1; i++)
            {
                if (offs + m1 + i != pivots[offs + m1 + i])
                {
                    i1_ = (offs) - (0);
                    for (i_ = 0; i_ <= m1 - 1; i_++)
                    {
                        tmp[i_] = a[i_ + i1_, offs + m1 + i];
                    }
                    for (i_ = offs; i_ <= offs + m1 - 1; i_++)
                    {
                        a[i_, offs + m1 + i] = a[i_, pivots[offs + m1 + i]];
                    }
                    i1_ = (0) - (offs);
                    for (i_ = offs; i_ <= offs + m1 - 1; i_++)
                    {
                        a[i_, pivots[offs + m1 + i]] = tmp[i_ + i1_];
                    }
                }
            }
        }
    }


    /*************************************************************************
    Recurrent complex LU subroutine.
    Never call it directly.

      -- ALGLIB routine --
         04.01.2010
         Bochkanov Sergey
    *************************************************************************/
    public static void cmatrixplurec(complex[,] a,
        int offs,
        int m,
        int n,
        ref int[] pivots,
        ref complex[] tmp,
        xparams _params)
    {
        int i = 0;
        int n1 = 0;
        int n2 = 0;
        int tsa = 0;
        int tsb = 0;
        int i_ = 0;
        int i1_ = 0;

        tsa = apserv.matrixtilesizea(_params) / 2;
        tsb = apserv.matrixtilesizeb(_params);
        if (n <= tsa)
        {
            cmatrixplu2(a, offs, m, n, ref pivots, ref tmp, _params);
            return;
        }
        if (n > m)
        {
            cmatrixplurec(a, offs, m, m, ref pivots, ref tmp, _params);
            for (i = 0; i <= m - 1; i++)
            {
                i1_ = (offs + m) - (0);
                for (i_ = 0; i_ <= n - m - 1; i_++)
                {
                    tmp[i_] = a[offs + i, i_ + i1_];
                }
                for (i_ = offs + m; i_ <= offs + n - 1; i_++)
                {
                    a[offs + i, i_] = a[pivots[offs + i], i_];
                }
                i1_ = (0) - (offs + m);
                for (i_ = offs + m; i_ <= offs + n - 1; i_++)
                {
                    a[pivots[offs + i], i_] = tmp[i_ + i1_];
                }
            }
            ablas.cmatrixlefttrsm(m, n - m, a, offs, offs, false, true, 0, a, offs, offs + m, _params);
            return;
        }
        if (n > tsb)
        {
            n1 = tsb;
            n2 = n - n1;
        }
        else
        {
            apserv.tiledsplit(n, tsa, ref n1, ref n2, _params);
        }
        cmatrixplurec(a, offs, m, n1, ref pivots, ref tmp, _params);
        if (n2 > 0)
        {
            for (i = 0; i <= n1 - 1; i++)
            {
                if (offs + i != pivots[offs + i])
                {
                    i1_ = (offs + n1) - (0);
                    for (i_ = 0; i_ <= n2 - 1; i_++)
                    {
                        tmp[i_] = a[offs + i, i_ + i1_];
                    }
                    for (i_ = offs + n1; i_ <= offs + n - 1; i_++)
                    {
                        a[offs + i, i_] = a[pivots[offs + i], i_];
                    }
                    i1_ = (0) - (offs + n1);
                    for (i_ = offs + n1; i_ <= offs + n - 1; i_++)
                    {
                        a[pivots[offs + i], i_] = tmp[i_ + i1_];
                    }
                }
            }
            ablas.cmatrixlefttrsm(n1, n2, a, offs, offs, false, true, 0, a, offs, offs + n1, _params);
            ablas.cmatrixgemm(m - n1, n - n1, n1, -1.0, a, offs + n1, offs, 0, a, offs, offs + n1, 0, 1.0, a, offs + n1, offs + n1, _params);
            cmatrixplurec(a, offs + n1, m - n1, n - n1, ref pivots, ref tmp, _params);
            for (i = 0; i <= n2 - 1; i++)
            {
                if (offs + n1 + i != pivots[offs + n1 + i])
                {
                    i1_ = (offs) - (0);
                    for (i_ = 0; i_ <= n1 - 1; i_++)
                    {
                        tmp[i_] = a[offs + n1 + i, i_ + i1_];
                    }
                    for (i_ = offs; i_ <= offs + n1 - 1; i_++)
                    {
                        a[offs + n1 + i, i_] = a[pivots[offs + n1 + i], i_];
                    }
                    i1_ = (0) - (offs);
                    for (i_ = offs; i_ <= offs + n1 - 1; i_++)
                    {
                        a[pivots[offs + n1 + i], i_] = tmp[i_ + i1_];
                    }
                }
            }
        }
    }


    /*************************************************************************
    Recurrent real LU subroutine.
    Never call it directly.

      -- ALGLIB routine --
         04.01.2010
         Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixplurec(double[,] a,
        int offs,
        int m,
        int n,
        ref int[] pivots,
        ref double[] tmp,
        xparams _params)
    {
        int i = 0;
        int n1 = 0;
        int n2 = 0;
        int tsa = 0;
        int tsb = 0;
        int i_ = 0;
        int i1_ = 0;

        tsa = apserv.matrixtilesizea(_params);
        tsb = apserv.matrixtilesizeb(_params);
        if (n <= tsb)
        {
            if (ablasmkl.rmatrixplumkl(a, offs, m, n, ref pivots, _params))
            {
                return;
            }
        }
        if (n <= tsa)
        {
            rmatrixplu2(a, offs, m, n, ref pivots, ref tmp, _params);
            return;
        }
        if (n > m)
        {
            rmatrixplurec(a, offs, m, m, ref pivots, ref tmp, _params);
            for (i = 0; i <= m - 1; i++)
            {
                i1_ = (offs + m) - (0);
                for (i_ = 0; i_ <= n - m - 1; i_++)
                {
                    tmp[i_] = a[offs + i, i_ + i1_];
                }
                for (i_ = offs + m; i_ <= offs + n - 1; i_++)
                {
                    a[offs + i, i_] = a[pivots[offs + i], i_];
                }
                i1_ = (0) - (offs + m);
                for (i_ = offs + m; i_ <= offs + n - 1; i_++)
                {
                    a[pivots[offs + i], i_] = tmp[i_ + i1_];
                }
            }
            ablas.rmatrixlefttrsm(m, n - m, a, offs, offs, false, true, 0, a, offs, offs + m, _params);
            return;
        }
        if (n > tsb)
        {
            n1 = tsb;
            n2 = n - n1;
        }
        else
        {
            apserv.tiledsplit(n, tsa, ref n1, ref n2, _params);
        }
        rmatrixplurec(a, offs, m, n1, ref pivots, ref tmp, _params);
        if (n2 > 0)
        {
            for (i = 0; i <= n1 - 1; i++)
            {
                if (offs + i != pivots[offs + i])
                {
                    i1_ = (offs + n1) - (0);
                    for (i_ = 0; i_ <= n2 - 1; i_++)
                    {
                        tmp[i_] = a[offs + i, i_ + i1_];
                    }
                    for (i_ = offs + n1; i_ <= offs + n - 1; i_++)
                    {
                        a[offs + i, i_] = a[pivots[offs + i], i_];
                    }
                    i1_ = (0) - (offs + n1);
                    for (i_ = offs + n1; i_ <= offs + n - 1; i_++)
                    {
                        a[pivots[offs + i], i_] = tmp[i_ + i1_];
                    }
                }
            }
            ablas.rmatrixlefttrsm(n1, n2, a, offs, offs, false, true, 0, a, offs, offs + n1, _params);
            ablas.rmatrixgemm(m - n1, n - n1, n1, -1.0, a, offs + n1, offs, 0, a, offs, offs + n1, 0, 1.0, a, offs + n1, offs + n1, _params);
            rmatrixplurec(a, offs + n1, m - n1, n - n1, ref pivots, ref tmp, _params);
            for (i = 0; i <= n2 - 1; i++)
            {
                if (offs + n1 + i != pivots[offs + n1 + i])
                {
                    i1_ = (offs) - (0);
                    for (i_ = 0; i_ <= n1 - 1; i_++)
                    {
                        tmp[i_] = a[offs + n1 + i, i_ + i1_];
                    }
                    for (i_ = offs; i_ <= offs + n1 - 1; i_++)
                    {
                        a[offs + n1 + i, i_] = a[pivots[offs + n1 + i], i_];
                    }
                    i1_ = (0) - (offs);
                    for (i_ = offs; i_ <= offs + n1 - 1; i_++)
                    {
                        a[pivots[offs + n1 + i], i_] = tmp[i_ + i1_];
                    }
                }
            }
        }
    }


    /*************************************************************************
    Complex LUP kernel

      -- ALGLIB routine --
         10.01.2010
         Bochkanov Sergey
    *************************************************************************/
    private static void cmatrixlup2(ref complex[,] a,
        int offs,
        int m,
        int n,
        ref int[] pivots,
        ref complex[] tmp,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int jp = 0;
        complex s = 0;
        int i_ = 0;
        int i1_ = 0;

        if (m == 0 || n == 0)
        {
            return;
        }
        for (j = 0; j <= Math.Min(m - 1, n - 1); j++)
        {
            jp = j;
            for (i = j + 1; i <= n - 1; i++)
            {
                if ((double)(math.abscomplex(a[offs + j, offs + i])) > (double)(math.abscomplex(a[offs + j, offs + jp])))
                {
                    jp = i;
                }
            }
            pivots[offs + j] = offs + jp;
            if (jp != j)
            {
                i1_ = (offs) - (0);
                for (i_ = 0; i_ <= m - 1; i_++)
                {
                    tmp[i_] = a[i_ + i1_, offs + j];
                }
                for (i_ = offs; i_ <= offs + m - 1; i_++)
                {
                    a[i_, offs + j] = a[i_, offs + jp];
                }
                i1_ = (0) - (offs);
                for (i_ = offs; i_ <= offs + m - 1; i_++)
                {
                    a[i_, offs + jp] = tmp[i_ + i1_];
                }
            }
            if (a[offs + j, offs + j] != 0 && j + 1 <= n - 1)
            {
                s = 1 / a[offs + j, offs + j];
                for (i_ = offs + j + 1; i_ <= offs + n - 1; i_++)
                {
                    a[offs + j, i_] = s * a[offs + j, i_];
                }
            }
            if (j < Math.Min(m - 1, n - 1))
            {
                i1_ = (offs + j + 1) - (0);
                for (i_ = 0; i_ <= m - j - 2; i_++)
                {
                    tmp[i_] = a[i_ + i1_, offs + j];
                }
                i1_ = (offs + j + 1) - (m);
                for (i_ = m; i_ <= m + n - j - 2; i_++)
                {
                    tmp[i_] = -a[offs + j, i_ + i1_];
                }
                ablas.cmatrixrank1(m - j - 1, n - j - 1, a, offs + j + 1, offs + j + 1, tmp, 0, tmp, m, _params);
            }
        }
    }


    /*************************************************************************
    Real LUP kernel

      -- ALGLIB routine --
         10.01.2010
         Bochkanov Sergey
    *************************************************************************/
    private static void rmatrixlup2(ref double[,] a,
        int offs,
        int m,
        int n,
        ref int[] pivots,
        ref double[] tmp,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int jp = 0;
        double s = 0;
        int i_ = 0;
        int i1_ = 0;

        if (m == 0 || n == 0)
        {
            return;
        }
        for (j = 0; j <= Math.Min(m - 1, n - 1); j++)
        {
            jp = j;
            for (i = j + 1; i <= n - 1; i++)
            {
                if ((double)(Math.Abs(a[offs + j, offs + i])) > (double)(Math.Abs(a[offs + j, offs + jp])))
                {
                    jp = i;
                }
            }
            pivots[offs + j] = offs + jp;
            if (jp != j)
            {
                i1_ = (offs) - (0);
                for (i_ = 0; i_ <= m - 1; i_++)
                {
                    tmp[i_] = a[i_ + i1_, offs + j];
                }
                for (i_ = offs; i_ <= offs + m - 1; i_++)
                {
                    a[i_, offs + j] = a[i_, offs + jp];
                }
                i1_ = (0) - (offs);
                for (i_ = offs; i_ <= offs + m - 1; i_++)
                {
                    a[i_, offs + jp] = tmp[i_ + i1_];
                }
            }
            if ((double)(a[offs + j, offs + j]) != (double)(0) && j + 1 <= n - 1)
            {
                s = 1 / a[offs + j, offs + j];
                for (i_ = offs + j + 1; i_ <= offs + n - 1; i_++)
                {
                    a[offs + j, i_] = s * a[offs + j, i_];
                }
            }
            if (j < Math.Min(m - 1, n - 1))
            {
                i1_ = (offs + j + 1) - (0);
                for (i_ = 0; i_ <= m - j - 2; i_++)
                {
                    tmp[i_] = a[i_ + i1_, offs + j];
                }
                i1_ = (offs + j + 1) - (m);
                for (i_ = m; i_ <= m + n - j - 2; i_++)
                {
                    tmp[i_] = -a[offs + j, i_ + i1_];
                }
                ablas.rmatrixrank1(m - j - 1, n - j - 1, a, offs + j + 1, offs + j + 1, tmp, 0, tmp, m, _params);
            }
        }
    }


    /*************************************************************************
    Complex PLU kernel

      -- LAPACK routine (version 3.0) --
         Univ. of Tennessee, Univ. of California Berkeley, NAG Ltd.,
         Courant Institute, Argonne National Lab, and Rice University
         June 30, 1992
    *************************************************************************/
    private static void cmatrixplu2(complex[,] a,
        int offs,
        int m,
        int n,
        ref int[] pivots,
        ref complex[] tmp,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int jp = 0;
        complex s = 0;
        int i_ = 0;
        int i1_ = 0;

        if (m == 0 || n == 0)
        {
            return;
        }
        for (j = 0; j <= Math.Min(m - 1, n - 1); j++)
        {
            jp = j;
            for (i = j + 1; i <= m - 1; i++)
            {
                if ((double)(math.abscomplex(a[offs + i, offs + j])) > (double)(math.abscomplex(a[offs + jp, offs + j])))
                {
                    jp = i;
                }
            }
            pivots[offs + j] = offs + jp;
            if (a[offs + jp, offs + j] != 0)
            {
                if (jp != j)
                {
                    for (i = 0; i <= n - 1; i++)
                    {
                        s = a[offs + j, offs + i];
                        a[offs + j, offs + i] = a[offs + jp, offs + i];
                        a[offs + jp, offs + i] = s;
                    }
                }
                if (j + 1 <= m - 1)
                {
                    s = 1 / a[offs + j, offs + j];
                    for (i_ = offs + j + 1; i_ <= offs + m - 1; i_++)
                    {
                        a[i_, offs + j] = s * a[i_, offs + j];
                    }
                }
            }
            if (j < Math.Min(m, n) - 1)
            {
                i1_ = (offs + j + 1) - (0);
                for (i_ = 0; i_ <= m - j - 2; i_++)
                {
                    tmp[i_] = a[i_ + i1_, offs + j];
                }
                i1_ = (offs + j + 1) - (m);
                for (i_ = m; i_ <= m + n - j - 2; i_++)
                {
                    tmp[i_] = -a[offs + j, i_ + i1_];
                }
                ablas.cmatrixrank1(m - j - 1, n - j - 1, a, offs + j + 1, offs + j + 1, tmp, 0, tmp, m, _params);
            }
        }
    }


    /*************************************************************************
    Real PLU kernel

      -- LAPACK routine (version 3.0) --
         Univ. of Tennessee, Univ. of California Berkeley, NAG Ltd.,
         Courant Institute, Argonne National Lab, and Rice University
         June 30, 1992
    *************************************************************************/
    private static void rmatrixplu2(double[,] a,
        int offs,
        int m,
        int n,
        ref int[] pivots,
        ref double[] tmp,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int jp = 0;
        double s = 0;
        int i_ = 0;
        int i1_ = 0;

        if (m == 0 || n == 0)
        {
            return;
        }
        for (j = 0; j <= Math.Min(m - 1, n - 1); j++)
        {
            jp = j;
            for (i = j + 1; i <= m - 1; i++)
            {
                if ((double)(Math.Abs(a[offs + i, offs + j])) > (double)(Math.Abs(a[offs + jp, offs + j])))
                {
                    jp = i;
                }
            }
            pivots[offs + j] = offs + jp;
            if ((double)(a[offs + jp, offs + j]) != (double)(0))
            {
                if (jp != j)
                {
                    for (i = 0; i <= n - 1; i++)
                    {
                        s = a[offs + j, offs + i];
                        a[offs + j, offs + i] = a[offs + jp, offs + i];
                        a[offs + jp, offs + i] = s;
                    }
                }
                if (j + 1 <= m - 1)
                {
                    s = 1 / a[offs + j, offs + j];
                    for (i_ = offs + j + 1; i_ <= offs + m - 1; i_++)
                    {
                        a[i_, offs + j] = s * a[i_, offs + j];
                    }
                }
            }
            if (j < Math.Min(m, n) - 1)
            {
                i1_ = (offs + j + 1) - (0);
                for (i_ = 0; i_ <= m - j - 2; i_++)
                {
                    tmp[i_] = a[i_ + i1_, offs + j];
                }
                i1_ = (offs + j + 1) - (m);
                for (i_ = m; i_ <= m + n - j - 2; i_++)
                {
                    tmp[i_] = -a[offs + j, i_ + i1_];
                }
                ablas.rmatrixrank1(m - j - 1, n - j - 1, a, offs + j + 1, offs + j + 1, tmp, 0, tmp, m, _params);
            }
        }
    }


}
