using System;

#pragma warning disable CS3008
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

public class ablas
{
    public const int blas2minvendorkernelsize = 8;


    /*************************************************************************
    Splits matrix length in two parts, left part should match ABLAS block size

    INPUT PARAMETERS
        A   -   real matrix, is passed to ensure that we didn't split
                complex matrix using real splitting subroutine.
                matrix itself is not changed.
        N   -   length, N>0

    OUTPUT PARAMETERS
        N1  -   length
        N2  -   length

    N1+N2=N, N1>=N2, N2 may be zero

      -- ALGLIB routine --
         15.12.2009
         Bochkanov Sergey
    *************************************************************************/
    public static void ablassplitlength(double[,] a,
        int n,
        ref int n1,
        ref int n2,
        xparams _params)
    {
        n1 = 0;
        n2 = 0;

        if (n > ablasblocksize(a, _params))
        {
            ablasinternalsplitlength(n, ablasblocksize(a, _params), ref n1, ref n2, _params);
        }
        else
        {
            ablasinternalsplitlength(n, ablasmicroblocksize(_params), ref n1, ref n2, _params);
        }
    }


    /*************************************************************************
    Complex ABLASSplitLength

      -- ALGLIB routine --
         15.12.2009
         Bochkanov Sergey
    *************************************************************************/
    public static void ablascomplexsplitlength(complex[,] a,
        int n,
        ref int n1,
        ref int n2,
        xparams _params)
    {
        n1 = 0;
        n2 = 0;

        if (n > ablascomplexblocksize(a, _params))
        {
            ablasinternalsplitlength(n, ablascomplexblocksize(a, _params), ref n1, ref n2, _params);
        }
        else
        {
            ablasinternalsplitlength(n, ablasmicroblocksize(_params), ref n1, ref n2, _params);
        }
    }


    /*************************************************************************
    Returns switch point for parallelism.

      -- ALGLIB routine --
         15.12.2009
         Bochkanov Sergey
    *************************************************************************/
    public static int gemmparallelsize(xparams _params)
    {
        int result = 0;

        result = 64;
        return result;
    }


    /*************************************************************************
    Returns block size - subdivision size where  cache-oblivious  soubroutines
    switch to the optimized kernel.

    INPUT PARAMETERS
        A   -   real matrix, is passed to ensure that we didn't split
                complex matrix using real splitting subroutine.
                matrix itself is not changed.

      -- ALGLIB routine --
         15.12.2009
         Bochkanov Sergey
    *************************************************************************/
    public static int ablasblocksize(double[,] a,
        xparams _params)
    {
        int result = 0;

        result = 32;
        return result;
    }


    /*************************************************************************
    Block size for complex subroutines.

      -- ALGLIB routine --
         15.12.2009
         Bochkanov Sergey
    *************************************************************************/
    public static int ablascomplexblocksize(complex[,] a,
        xparams _params)
    {
        int result = 0;

        result = 24;
        return result;
    }


    /*************************************************************************
    Microblock size

      -- ALGLIB routine --
         15.12.2009
         Bochkanov Sergey
    *************************************************************************/
    public static int ablasmicroblocksize(xparams _params)
    {
        int result = 0;

        result = 8;
        return result;
    }


    /*************************************************************************
    Generation of an elementary reflection transformation

    The subroutine generates elementary reflection H of order N, so that, for
    a given X, the following equality holds true:

        ( X(1) )   ( Beta )
    H * (  ..  ) = (  0   )
        ( X(n) )   (  0   )

    where
                  ( V(1) )
    H = 1 - Tau * (  ..  ) * ( V(1), ..., V(n) )
                  ( V(n) )

    where the first component of vector V equals 1.

    Input parameters:
        X   -   vector. Array whose index ranges within [1..N].
        N   -   reflection order.

    Output parameters:
        X   -   components from 2 to N are replaced with vector V.
                The first component is replaced with parameter Beta.
        Tau -   scalar value Tau. If X is a null vector, Tau equals 0,
                otherwise 1 <= Tau <= 2.

    This subroutine is the modification of the DLARFG subroutines from
    the LAPACK library.

    MODIFICATIONS:
        24.12.2005 sign(Alpha) was replaced with an analogous to the Fortran SIGN code.

      -- LAPACK auxiliary routine (version 3.0) --
         Univ. of Tennessee, Univ. of California Berkeley, NAG Ltd.,
         Courant Institute, Argonne National Lab, and Rice University
         September 30, 1994
    *************************************************************************/
    public static void generatereflection(ref double[] x,
        int n,
        ref double tau,
        xparams _params)
    {
        int j = 0;
        double alpha = 0;
        double xnorm = 0;
        double v = 0;
        double beta = 0;
        double mx = 0;
        double s = 0;
        int i_ = 0;

        tau = 0;

        if (n <= 1)
        {
            tau = 0;
            return;
        }

        //
        // Scale if needed (to avoid overflow/underflow during intermediate
        // calculations).
        //
        mx = 0;
        for (j = 1; j <= n; j++)
        {
            mx = Math.Max(Math.Abs(x[j]), mx);
        }
        s = 1;
        if ((double)(mx) != (double)(0))
        {
            if ((double)(mx) <= (double)(math.minrealnumber / math.machineepsilon))
            {
                s = math.minrealnumber / math.machineepsilon;
                v = 1 / s;
                for (i_ = 1; i_ <= n; i_++)
                {
                    x[i_] = v * x[i_];
                }
                mx = mx * v;
            }
            else
            {
                if ((double)(mx) >= (double)(math.maxrealnumber * math.machineepsilon))
                {
                    s = math.maxrealnumber * math.machineepsilon;
                    v = 1 / s;
                    for (i_ = 1; i_ <= n; i_++)
                    {
                        x[i_] = v * x[i_];
                    }
                    mx = mx * v;
                }
            }
        }

        //
        // XNORM = DNRM2( N-1, X, INCX )
        //
        alpha = x[1];
        xnorm = 0;
        if ((double)(mx) != (double)(0))
        {
            for (j = 2; j <= n; j++)
            {
                xnorm = xnorm + math.sqr(x[j] / mx);
            }
            xnorm = Math.Sqrt(xnorm) * mx;
        }
        if ((double)(xnorm) == (double)(0))
        {

            //
            // H  =  I
            //
            tau = 0;
            x[1] = x[1] * s;
            return;
        }

        //
        // general case
        //
        mx = Math.Max(Math.Abs(alpha), Math.Abs(xnorm));
        beta = -(mx * Math.Sqrt(math.sqr(alpha / mx) + math.sqr(xnorm / mx)));
        if ((double)(alpha) < (double)(0))
        {
            beta = -beta;
        }
        tau = (beta - alpha) / beta;
        v = 1 / (alpha - beta);
        for (i_ = 2; i_ <= n; i_++)
        {
            x[i_] = v * x[i_];
        }
        x[1] = beta;

        //
        // Scale back outputs
        //
        x[1] = x[1] * s;
    }


    /*************************************************************************
    Application of an elementary reflection to a rectangular matrix of size MxN

    The algorithm pre-multiplies the matrix by an elementary reflection transformation
    which is given by column V and scalar Tau (see the description of the
    GenerateReflection procedure). Not the whole matrix but only a part of it
    is transformed (rows from M1 to M2, columns from N1 to N2). Only the elements
    of this submatrix are changed.

    Input parameters:
        C       -   matrix to be transformed.
        Tau     -   scalar defining the transformation.
        V       -   column defining the transformation.
                    Array whose index ranges within [1..M2-M1+1].
        M1, M2  -   range of rows to be transformed.
        N1, N2  -   range of columns to be transformed.
        WORK    -   working array whose indexes goes from N1 to N2.

    Output parameters:
        C       -   the result of multiplying the input matrix C by the
                    transformation matrix which is given by Tau and V.
                    If N1>N2 or M1>M2, C is not modified.

      -- LAPACK auxiliary routine (version 3.0) --
         Univ. of Tennessee, Univ. of California Berkeley, NAG Ltd.,
         Courant Institute, Argonne National Lab, and Rice University
         September 30, 1994
    *************************************************************************/
    public static void applyreflectionfromtheleft(double[,] c,
        double tau,
        double[] v,
        int m1,
        int m2,
        int n1,
        int n2,
        ref double[] work,
        xparams _params)
    {
        if (((double)(tau) == (double)(0) || n1 > n2) || m1 > m2)
        {
            return;
        }
        apserv.rvectorsetlengthatleast(ref work, n2 - n1 + 1, _params);
        rmatrixgemv(n2 - n1 + 1, m2 - m1 + 1, 1.0, c, m1, n1, 1, v, 1, 0.0, work, 0, _params);
        rmatrixger(m2 - m1 + 1, n2 - n1 + 1, c, m1, n1, -tau, v, 1, work, 0, _params);
    }


    /*************************************************************************
    Application of an elementary reflection to a rectangular matrix of size MxN

    The algorithm post-multiplies the matrix by an elementary reflection transformation
    which is given by column V and scalar Tau (see the description of the
    GenerateReflection procedure). Not the whole matrix but only a part of it
    is transformed (rows from M1 to M2, columns from N1 to N2). Only the
    elements of this submatrix are changed.

    Input parameters:
        C       -   matrix to be transformed.
        Tau     -   scalar defining the transformation.
        V       -   column defining the transformation.
                    Array whose index ranges within [1..N2-N1+1].
        M1, M2  -   range of rows to be transformed.
        N1, N2  -   range of columns to be transformed.
        WORK    -   working array whose indexes goes from M1 to M2.

    Output parameters:
        C       -   the result of multiplying the input matrix C by the
                    transformation matrix which is given by Tau and V.
                    If N1>N2 or M1>M2, C is not modified.

      -- LAPACK auxiliary routine (version 3.0) --
         Univ. of Tennessee, Univ. of California Berkeley, NAG Ltd.,
         Courant Institute, Argonne National Lab, and Rice University
         September 30, 1994
    *************************************************************************/
    public static void applyreflectionfromtheright(double[,] c,
        double tau,
        double[] v,
        int m1,
        int m2,
        int n1,
        int n2,
        ref double[] work,
        xparams _params)
    {
        if (((double)(tau) == (double)(0) || n1 > n2) || m1 > m2)
        {
            return;
        }
        apserv.rvectorsetlengthatleast(ref work, m2 - m1 + 1, _params);
        rmatrixgemv(m2 - m1 + 1, n2 - n1 + 1, 1.0, c, m1, n1, 0, v, 1, 0.0, work, 0, _params);
        rmatrixger(m2 - m1 + 1, n2 - n1 + 1, c, m1, n1, -tau, work, 0, v, 1, _params);
    }


    /*************************************************************************
    Cache-oblivous complex "copy-and-transpose"

    Input parameters:
        M   -   number of rows
        N   -   number of columns
        A   -   source matrix, MxN submatrix is copied and transposed
        IA  -   submatrix offset (row index)
        JA  -   submatrix offset (column index)
        B   -   destination matrix, must be large enough to store result
        IB  -   submatrix offset (row index)
        JB  -   submatrix offset (column index)
    *************************************************************************/
    public static void cmatrixtranspose(int m,
        int n,
        complex[,] a,
        int ia,
        int ja,
        complex[,] b,
        int ib,
        int jb,
        xparams _params)
    {
        int i = 0;
        int s1 = 0;
        int s2 = 0;
        int i_ = 0;
        int i1_ = 0;

        if (m <= 2 * ablascomplexblocksize(a, _params) && n <= 2 * ablascomplexblocksize(a, _params))
        {

            //
            // base case
            //
            for (i = 0; i <= m - 1; i++)
            {
                i1_ = (ja) - (ib);
                for (i_ = ib; i_ <= ib + n - 1; i_++)
                {
                    b[i_, jb + i] = a[ia + i, i_ + i1_];
                }
            }
        }
        else
        {

            //
            // Cache-oblivious recursion
            //
            if (m > n)
            {
                ablascomplexsplitlength(a, m, ref s1, ref s2, _params);
                cmatrixtranspose(s1, n, a, ia, ja, b, ib, jb, _params);
                cmatrixtranspose(s2, n, a, ia + s1, ja, b, ib, jb + s1, _params);
            }
            else
            {
                ablascomplexsplitlength(a, n, ref s1, ref s2, _params);
                cmatrixtranspose(m, s1, a, ia, ja, b, ib, jb, _params);
                cmatrixtranspose(m, s2, a, ia, ja + s1, b, ib + s1, jb, _params);
            }
        }
    }


    /*************************************************************************
    Cache-oblivous real "copy-and-transpose"

    Input parameters:
        M   -   number of rows
        N   -   number of columns
        A   -   source matrix, MxN submatrix is copied and transposed
        IA  -   submatrix offset (row index)
        JA  -   submatrix offset (column index)
        B   -   destination matrix, must be large enough to store result
        IB  -   submatrix offset (row index)
        JB  -   submatrix offset (column index)
    *************************************************************************/
    public static void rmatrixtranspose(int m,
        int n,
        double[,] a,
        int ia,
        int ja,
        double[,] b,
        int ib,
        int jb,
        xparams _params)
    {
        int i = 0;
        int s1 = 0;
        int s2 = 0;
        int i_ = 0;
        int i1_ = 0;

        if (m <= 2 * ablasblocksize(a, _params) && n <= 2 * ablasblocksize(a, _params))
        {

            //
            // base case
            //
            for (i = 0; i <= m - 1; i++)
            {
                i1_ = (ja) - (ib);
                for (i_ = ib; i_ <= ib + n - 1; i_++)
                {
                    b[i_, jb + i] = a[ia + i, i_ + i1_];
                }
            }
        }
        else
        {

            //
            // Cache-oblivious recursion
            //
            if (m > n)
            {
                ablassplitlength(a, m, ref s1, ref s2, _params);
                rmatrixtranspose(s1, n, a, ia, ja, b, ib, jb, _params);
                rmatrixtranspose(s2, n, a, ia + s1, ja, b, ib, jb + s1, _params);
            }
            else
            {
                ablassplitlength(a, n, ref s1, ref s2, _params);
                rmatrixtranspose(m, s1, a, ia, ja, b, ib, jb, _params);
                rmatrixtranspose(m, s2, a, ia, ja + s1, b, ib + s1, jb, _params);
            }
        }
    }


    /*************************************************************************
    This code enforces symmetricy of the matrix by copying Upper part to lower
    one (or vice versa).

    INPUT PARAMETERS:
        A   -   matrix
        N   -   number of rows/columns
        IsUpper - whether we want to copy upper triangle to lower one (True)
                or vice versa (False).
    *************************************************************************/
    public static void rmatrixenforcesymmetricity(double[,] a,
        int n,
        bool isupper,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        if (isupper)
        {
            for (i = 0; i <= n - 1; i++)
            {
                for (j = i + 1; j <= n - 1; j++)
                {
                    a[j, i] = a[i, j];
                }
            }
        }
        else
        {
            for (i = 0; i <= n - 1; i++)
            {
                for (j = i + 1; j <= n - 1; j++)
                {
                    a[i, j] = a[j, i];
                }
            }
        }
    }


    /*************************************************************************
    Copy

    Input parameters:
        M   -   number of rows
        N   -   number of columns
        A   -   source matrix, MxN submatrix is copied and transposed
        IA  -   submatrix offset (row index)
        JA  -   submatrix offset (column index)
        B   -   destination matrix, must be large enough to store result
        IB  -   submatrix offset (row index)
        JB  -   submatrix offset (column index)
    *************************************************************************/
    public static void cmatrixcopy(int m,
        int n,
        complex[,] a,
        int ia,
        int ja,
        complex[,] b,
        int ib,
        int jb,
        xparams _params)
    {
        int i = 0;
        int i_ = 0;
        int i1_ = 0;

        if (m == 0 || n == 0)
        {
            return;
        }
        for (i = 0; i <= m - 1; i++)
        {
            i1_ = (ja) - (jb);
            for (i_ = jb; i_ <= jb + n - 1; i_++)
            {
                b[ib + i, i_] = a[ia + i, i_ + i1_];
            }
        }
    }


    /*************************************************************************
    Copy

    Input parameters:
        N   -   subvector size
        A   -   source vector, N elements are copied
        IA  -   source offset (first element index)
        B   -   destination vector, must be large enough to store result
        IB  -   destination offset (first element index)
    *************************************************************************/
    public static void rvectorcopy(int n,
        double[] a,
        int ia,
        double[] b,
        int ib,
        xparams _params)
    {
        if (n == 0)
        {
            return;
        }
        if (ia == 0 && ib == 0)
        {
            ablasf.rcopyv(n, a, b, _params);
        }
        else
        {
            ablasf.rcopyvx(n, a, ia, b, ib, _params);
        }
    }


    /*************************************************************************
    Copy

    Input parameters:
        M   -   number of rows
        N   -   number of columns
        A   -   source matrix, MxN submatrix is copied and transposed
        IA  -   submatrix offset (row index)
        JA  -   submatrix offset (column index)
        B   -   destination matrix, must be large enough to store result
        IB  -   submatrix offset (row index)
        JB  -   submatrix offset (column index)
    *************************************************************************/
    public static void rmatrixcopy(int m,
        int n,
        double[,] a,
        int ia,
        int ja,
        double[,] b,
        int ib,
        int jb,
        xparams _params)
    {
        int i = 0;
        int i_ = 0;
        int i1_ = 0;

        if (m == 0 || n == 0)
        {
            return;
        }
        for (i = 0; i <= m - 1; i++)
        {
            i1_ = (ja) - (jb);
            for (i_ = jb; i_ <= jb + n - 1; i_++)
            {
                b[ib + i, i_] = a[ia + i, i_ + i1_];
            }
        }
    }


    /*************************************************************************
    Performs generalized copy: B := Beta*B + Alpha*A.

    If Beta=0, then previous contents of B is simply ignored. If Alpha=0, then
    A is ignored and not referenced. If both Alpha and Beta  are  zero,  B  is
    filled by zeros.

    Input parameters:
        M   -   number of rows
        N   -   number of columns
        Alpha-  coefficient
        A   -   source matrix, MxN submatrix is copied and transposed
        IA  -   submatrix offset (row index)
        JA  -   submatrix offset (column index)
        Beta-   coefficient
        B   -   destination matrix, must be large enough to store result
        IB  -   submatrix offset (row index)
        JB  -   submatrix offset (column index)
    *************************************************************************/
    public static void rmatrixgencopy(int m,
        int n,
        double alpha,
        double[,] a,
        int ia,
        int ja,
        double beta,
        double[,] b,
        int ib,
        int jb,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        if (m == 0 || n == 0)
        {
            return;
        }

        //
        // Zero-fill
        //
        if ((double)(alpha) == (double)(0) && (double)(beta) == (double)(0))
        {
            for (i = 0; i <= m - 1; i++)
            {
                for (j = 0; j <= n - 1; j++)
                {
                    b[ib + i, jb + j] = 0;
                }
            }
            return;
        }

        //
        // Inplace multiply
        //
        if ((double)(alpha) == (double)(0))
        {
            for (i = 0; i <= m - 1; i++)
            {
                for (j = 0; j <= n - 1; j++)
                {
                    b[ib + i, jb + j] = beta * b[ib + i, jb + j];
                }
            }
            return;
        }

        //
        // Multiply and copy
        //
        if ((double)(beta) == (double)(0))
        {
            for (i = 0; i <= m - 1; i++)
            {
                for (j = 0; j <= n - 1; j++)
                {
                    b[ib + i, jb + j] = alpha * a[ia + i, ja + j];
                }
            }
            return;
        }

        //
        // Generic
        //
        for (i = 0; i <= m - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                b[ib + i, jb + j] = alpha * a[ia + i, ja + j] + beta * b[ib + i, jb + j];
            }
        }
    }


    /*************************************************************************
    Rank-1 correction: A := A + alpha*u*v'

    NOTE: this  function  expects  A  to  be  large enough to store result. No
          automatic preallocation happens for  smaller  arrays.  No  integrity
          checks is performed for sizes of A, u, v.

    INPUT PARAMETERS:
        M   -   number of rows
        N   -   number of columns
        A   -   target matrix, MxN submatrix is updated
        IA  -   submatrix offset (row index)
        JA  -   submatrix offset (column index)
        Alpha-  coefficient
        U   -   vector #1
        IU  -   subvector offset
        V   -   vector #2
        IV  -   subvector offset


      -- ALGLIB routine --

         16.10.2017
         Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixger(int m,
        int n,
        double[,] a,
        int ia,
        int ja,
        double alpha,
        double[] u,
        int iu,
        double[] v,
        int iv,
        xparams _params)
    {
        int i = 0;
        double s = 0;
        int i_ = 0;
        int i1_ = 0;


        //
        // Quick exit
        //
        if (m <= 0 || n <= 0)
        {
            return;
        }

        //
        // Try fast kernels:
        // * vendor kernel
        // * internal kernel
        //
        if (m > blas2minvendorkernelsize && n > blas2minvendorkernelsize)
        {

            //
            // Try MKL kernel first
            //
            if (ablasmkl.rmatrixgermkl(m, n, a, ia, ja, alpha, u, iu, v, iv, _params))
            {
                return;
            }
        }
        if (ablasf.rmatrixgerf(m, n, a, ia, ja, alpha, u, iu, v, iv, _params))
        {
            return;
        }

        //
        // Generic code
        //
        for (i = 0; i <= m - 1; i++)
        {
            s = alpha * u[iu + i];
            i1_ = (iv) - (ja);
            for (i_ = ja; i_ <= ja + n - 1; i_++)
            {
                a[ia + i, i_] = a[ia + i, i_] + s * v[i_ + i1_];
            }
        }
    }


    /*************************************************************************
    Rank-1 correction: A := A + u*v'

    INPUT PARAMETERS:
        M   -   number of rows
        N   -   number of columns
        A   -   target matrix, MxN submatrix is updated
        IA  -   submatrix offset (row index)
        JA  -   submatrix offset (column index)
        U   -   vector #1
        IU  -   subvector offset
        V   -   vector #2
        IV  -   subvector offset
    *************************************************************************/
    public static void cmatrixrank1(int m,
        int n,
        complex[,] a,
        int ia,
        int ja,
        complex[] u,
        int iu,
        complex[] v,
        int iv,
        xparams _params)
    {
        int i = 0;
        complex s = 0;
        int i_ = 0;
        int i1_ = 0;


        //
        // Quick exit
        //
        if (m <= 0 || n <= 0)
        {
            return;
        }

        //
        // Try fast kernels:
        // * vendor kernel
        // * internal kernel
        //
        if (m > blas2minvendorkernelsize && n > blas2minvendorkernelsize)
        {

            //
            // Try MKL kernel first
            //
            if (ablasmkl.cmatrixrank1mkl(m, n, a, ia, ja, u, iu, v, iv, _params))
            {
                return;
            }
        }
        if (ablasf.cmatrixrank1f(m, n, a, ia, ja, u, iu, v, iv, _params))
        {
            return;
        }

        //
        // Generic code
        //
        for (i = 0; i <= m - 1; i++)
        {
            s = u[iu + i];
            i1_ = (iv) - (ja);
            for (i_ = ja; i_ <= ja + n - 1; i_++)
            {
                a[ia + i, i_] = a[ia + i, i_] + s * v[i_ + i1_];
            }
        }
    }


    /*************************************************************************
    IMPORTANT: this function is deprecated since ALGLIB 3.13. Use RMatrixGER()
               which is more generic version of this function.

    Rank-1 correction: A := A + u*v'

    INPUT PARAMETERS:
        M   -   number of rows
        N   -   number of columns
        A   -   target matrix, MxN submatrix is updated
        IA  -   submatrix offset (row index)
        JA  -   submatrix offset (column index)
        U   -   vector #1
        IU  -   subvector offset
        V   -   vector #2
        IV  -   subvector offset
    *************************************************************************/
    public static void rmatrixrank1(int m,
        int n,
        double[,] a,
        int ia,
        int ja,
        double[] u,
        int iu,
        double[] v,
        int iv,
        xparams _params)
    {
        int i = 0;
        double s = 0;
        int i_ = 0;
        int i1_ = 0;


        //
        // Quick exit
        //
        if (m <= 0 || n <= 0)
        {
            return;
        }

        //
        // Try fast kernels:
        // * vendor kernel
        // * internal kernel
        //
        if (m > blas2minvendorkernelsize && n > blas2minvendorkernelsize)
        {

            //
            // Try MKL kernel first
            //
            if (ablasmkl.rmatrixrank1mkl(m, n, a, ia, ja, u, iu, v, iv, _params))
            {
                return;
            }
        }
        if (ablasf.rmatrixrank1f(m, n, a, ia, ja, u, iu, v, iv, _params))
        {
            return;
        }

        //
        // Generic code
        //
        for (i = 0; i <= m - 1; i++)
        {
            s = u[iu + i];
            i1_ = (iv) - (ja);
            for (i_ = ja; i_ <= ja + n - 1; i_++)
            {
                a[ia + i, i_] = a[ia + i, i_] + s * v[i_ + i1_];
            }
        }
    }


    public static void rmatrixgemv(int m,
        int n,
        double alpha,
        double[,] a,
        int ia,
        int ja,
        int opa,
        double[] x,
        int ix,
        double beta,
        double[] y,
        int iy,
        xparams _params)
    {

        //
        // Quick exit for M=0, N=0 or Alpha=0.
        //
        // After this block we have M>0, N>0, Alpha<>0.
        //
        if (m <= 0)
        {
            return;
        }
        if (n <= 0 || (double)(alpha) == (double)(0.0))
        {
            if ((double)(beta) != (double)(0))
            {
                ablasf.rmulvx(m, beta, y, iy, _params);
            }
            else
            {
                ablasf.rsetvx(m, 0.0, y, iy, _params);
            }
            return;
        }

        //
        // Try fast kernels
        //
        if (m > blas2minvendorkernelsize && n > blas2minvendorkernelsize)
        {

            //
            // Try MKL kernel
            //
            if (ablasmkl.rmatrixgemvmkl(m, n, alpha, a, ia, ja, opa, x, ix, beta, y, iy, _params))
            {
                return;
            }
        }
        if (ia + ja + ix + iy == 0)
        {
            ablasf.rgemv(m, n, alpha, a, opa, x, beta, y, _params);
        }
        else
        {
            ablasf.rgemvx(m, n, alpha, a, ia, ja, opa, x, ix, beta, y, iy, _params);
        }
    }


    /*************************************************************************
    Matrix-vector product: y := op(A)*x

    INPUT PARAMETERS:
        M   -   number of rows of op(A)
                M>=0
        N   -   number of columns of op(A)
                N>=0
        A   -   target matrix
        IA  -   submatrix offset (row index)
        JA  -   submatrix offset (column index)
        OpA -   operation type:
                * OpA=0     =>  op(A) = A
                * OpA=1     =>  op(A) = A^T
                * OpA=2     =>  op(A) = A^H
        X   -   input vector
        IX  -   subvector offset
        IY  -   subvector offset
        Y   -   preallocated matrix, must be large enough to store result

    OUTPUT PARAMETERS:
        Y   -   vector which stores result

    if M=0, then subroutine does nothing.
    if N=0, Y is filled by zeros.


      -- ALGLIB routine --

         28.01.2010
         Bochkanov Sergey
    *************************************************************************/
    public static void cmatrixmv(int m,
        int n,
        complex[,] a,
        int ia,
        int ja,
        int opa,
        complex[] x,
        int ix,
        complex[] y,
        int iy,
        xparams _params)
    {
        int i = 0;
        complex v = 0;
        int i_ = 0;
        int i1_ = 0;


        //
        // Quick exit
        //
        if (m == 0)
        {
            return;
        }
        if (n == 0)
        {
            for (i = 0; i <= m - 1; i++)
            {
                y[iy + i] = 0;
            }
            return;
        }

        //
        // Try fast kernels
        //
        if (m > blas2minvendorkernelsize && n > blas2minvendorkernelsize)
        {

            //
            // Try MKL kernel
            //
            if (ablasmkl.cmatrixmvmkl(m, n, a, ia, ja, opa, x, ix, y, iy, _params))
            {
                return;
            }
        }

        //
        // Generic code
        //
        if (opa == 0)
        {

            //
            // y = A*x
            //
            for (i = 0; i <= m - 1; i++)
            {
                i1_ = (ix) - (ja);
                v = 0.0;
                for (i_ = ja; i_ <= ja + n - 1; i_++)
                {
                    v += a[ia + i, i_] * x[i_ + i1_];
                }
                y[iy + i] = v;
            }
            return;
        }
        if (opa == 1)
        {

            //
            // y = A^T*x
            //
            for (i = 0; i <= m - 1; i++)
            {
                y[iy + i] = 0;
            }
            for (i = 0; i <= n - 1; i++)
            {
                v = x[ix + i];
                i1_ = (ja) - (iy);
                for (i_ = iy; i_ <= iy + m - 1; i_++)
                {
                    y[i_] = y[i_] + v * a[ia + i, i_ + i1_];
                }
            }
            return;
        }
        if (opa == 2)
        {

            //
            // y = A^H*x
            //
            for (i = 0; i <= m - 1; i++)
            {
                y[iy + i] = 0;
            }
            for (i = 0; i <= n - 1; i++)
            {
                v = x[ix + i];
                i1_ = (ja) - (iy);
                for (i_ = iy; i_ <= iy + m - 1; i_++)
                {
                    y[i_] = y[i_] + v * math.conj(a[ia + i, i_ + i1_]);
                }
            }
            return;
        }
    }


    /*************************************************************************
    IMPORTANT: this function is deprecated since ALGLIB 3.13. Use RMatrixGEMV()
               which is more generic version of this function.
               
    Matrix-vector product: y := op(A)*x

    INPUT PARAMETERS:
        M   -   number of rows of op(A)
        N   -   number of columns of op(A)
        A   -   target matrix
        IA  -   submatrix offset (row index)
        JA  -   submatrix offset (column index)
        OpA -   operation type:
                * OpA=0     =>  op(A) = A
                * OpA=1     =>  op(A) = A^T
        X   -   input vector
        IX  -   subvector offset
        IY  -   subvector offset
        Y   -   preallocated matrix, must be large enough to store result

    OUTPUT PARAMETERS:
        Y   -   vector which stores result

    if M=0, then subroutine does nothing.
    if N=0, Y is filled by zeros.


      -- ALGLIB routine --

         28.01.2010
         Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixmv(int m,
        int n,
        double[,] a,
        int ia,
        int ja,
        int opa,
        double[] x,
        int ix,
        double[] y,
        int iy,
        xparams _params)
    {
        int i = 0;
        double v = 0;
        int i_ = 0;
        int i1_ = 0;


        //
        // Quick exit
        //
        if (m == 0)
        {
            return;
        }
        if (n == 0)
        {
            for (i = 0; i <= m - 1; i++)
            {
                y[iy + i] = 0;
            }
            return;
        }

        //
        // Try fast kernels
        //
        if (m > blas2minvendorkernelsize && n > blas2minvendorkernelsize)
        {

            //
            // Try MKL kernel
            //
            if (ablasmkl.rmatrixmvmkl(m, n, a, ia, ja, opa, x, ix, y, iy, _params))
            {
                return;
            }
        }

        //
        // Generic code
        //
        if (opa == 0)
        {

            //
            // y = A*x
            //
            for (i = 0; i <= m - 1; i++)
            {
                i1_ = (ix) - (ja);
                v = 0.0;
                for (i_ = ja; i_ <= ja + n - 1; i_++)
                {
                    v += a[ia + i, i_] * x[i_ + i1_];
                }
                y[iy + i] = v;
            }
            return;
        }
        if (opa == 1)
        {

            //
            // y = A^T*x
            //
            for (i = 0; i <= m - 1; i++)
            {
                y[iy + i] = 0;
            }
            for (i = 0; i <= n - 1; i++)
            {
                v = x[ix + i];
                i1_ = (ja) - (iy);
                for (i_ = iy; i_ <= iy + m - 1; i_++)
                {
                    y[i_] = y[i_] + v * a[ia + i, i_ + i1_];
                }
            }
            return;
        }
    }


    public static void rmatrixsymv(int n,
        double alpha,
        double[,] a,
        int ia,
        int ja,
        bool isupper,
        double[] x,
        int ix,
        double beta,
        double[] y,
        int iy,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double v = 0;
        double vr = 0;
        double vx = 0;


        //
        // Quick exit for M=0, N=0 or Alpha=0.
        //
        // After this block we have M>0, N>0, Alpha<>0.
        //
        if (n <= 0)
        {
            return;
        }
        if ((double)(alpha) == (double)(0.0))
        {
            if ((double)(beta) != (double)(0))
            {
                for (i = 0; i <= n - 1; i++)
                {
                    y[iy + i] = beta * y[iy + i];
                }
            }
            else
            {
                for (i = 0; i <= n - 1; i++)
                {
                    y[iy + i] = 0.0;
                }
            }
            return;
        }

        //
        // Try fast kernels
        //
        if (n > blas2minvendorkernelsize)
        {

            //
            // Try MKL kernel
            //
            if (ablasmkl.rmatrixsymvmkl(n, alpha, a, ia, ja, isupper, x, ix, beta, y, iy, _params))
            {
                return;
            }
        }

        //
        // Generic code
        //
        if ((double)(beta) != (double)(0))
        {
            for (i = 0; i <= n - 1; i++)
            {
                y[iy + i] = beta * y[iy + i];
            }
        }
        else
        {
            for (i = 0; i <= n - 1; i++)
            {
                y[iy + i] = 0.0;
            }
        }
        if (isupper)
        {

            //
            // Upper triangle of A is stored
            //
            for (i = 0; i <= n - 1; i++)
            {

                //
                // Process diagonal element
                //
                v = alpha * a[ia + i, ja + i];
                y[iy + i] = y[iy + i] + v * x[ix + i];

                //
                // Process off-diagonal elements
                //
                vr = 0.0;
                vx = x[ix + i];
                for (j = i + 1; j <= n - 1; j++)
                {
                    v = alpha * a[ia + i, ja + j];
                    y[iy + j] = y[iy + j] + v * vx;
                    vr = vr + v * x[ix + j];
                }
                y[iy + i] = y[iy + i] + vr;
            }
        }
        else
        {

            //
            // Lower triangle of A is stored
            //
            for (i = 0; i <= n - 1; i++)
            {

                //
                // Process diagonal element
                //
                v = alpha * a[ia + i, ja + i];
                y[iy + i] = y[iy + i] + v * x[ix + i];

                //
                // Process off-diagonal elements
                //
                vr = 0.0;
                vx = x[ix + i];
                for (j = 0; j <= i - 1; j++)
                {
                    v = alpha * a[ia + i, ja + j];
                    y[iy + j] = y[iy + j] + v * vx;
                    vr = vr + v * x[ix + j];
                }
                y[iy + i] = y[iy + i] + vr;
            }
        }
    }


    public static double rmatrixsyvmv(int n,
        double[,] a,
        int ia,
        int ja,
        bool isupper,
        double[] x,
        int ix,
        double[] tmp,
        xparams _params)
    {
        double result = 0;
        int i = 0;


        //
        // Quick exit for N=0
        //
        if (n <= 0)
        {
            result = 0;
            return result;
        }

        //
        // Generic code
        //
        rmatrixsymv(n, 1.0, a, ia, ja, isupper, x, ix, 0.0, tmp, 0, _params);
        result = 0;
        for (i = 0; i <= n - 1; i++)
        {
            result = result + x[ix + i] * tmp[i];
        }
        return result;
    }


    /*************************************************************************
    This subroutine solves linear system op(A)*x=b where:
    * A is NxN upper/lower triangular/unitriangular matrix
    * X and B are Nx1 vectors
    * "op" may be identity transformation or transposition

    Solution replaces X.

    IMPORTANT: * no overflow/underflow/denegeracy tests is performed.
               * no integrity checks for operand sizes, out-of-bounds accesses
                 and so on is performed

    INPUT PARAMETERS
        N   -   matrix size, N>=0
        A       -   matrix, actial matrix is stored in A[IA:IA+N-1,JA:JA+N-1]
        IA      -   submatrix offset
        JA      -   submatrix offset
        IsUpper -   whether matrix is upper triangular
        IsUnit  -   whether matrix is unitriangular
        OpType  -   transformation type:
                    * 0 - no transformation
                    * 1 - transposition
        X       -   right part, actual vector is stored in X[IX:IX+N-1]
        IX      -   offset
        
    OUTPUT PARAMETERS
        X       -   solution replaces elements X[IX:IX+N-1]

      -- ALGLIB routine / remastering of LAPACK's DTRSV --
         (c) 2017 Bochkanov Sergey - converted to ALGLIB
         (c) 2016 Reference BLAS level1 routine (LAPACK version 3.7.0)
         Reference BLAS is a software package provided by Univ. of Tennessee,
         Univ. of California Berkeley, Univ. of Colorado Denver and NAG Ltd.
    *************************************************************************/
    public static void rmatrixtrsv(int n,
        double[,] a,
        int ia,
        int ja,
        bool isupper,
        bool isunit,
        int optype,
        double[] x,
        int ix,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double v = 0;


        //
        // Quick exit
        //
        if (n <= 0)
        {
            return;
        }

        //
        // Try fast kernels
        //
        if (n > blas2minvendorkernelsize)
        {

            //
            // Try MKL kernel
            //
            if (ablasmkl.rmatrixtrsvmkl(n, a, ia, ja, isupper, isunit, optype, x, ix, _params))
            {
                return;
            }
        }

        //
        // Generic code
        //
        if (optype == 0 && isupper)
        {
            for (i = n - 1; i >= 0; i--)
            {
                v = x[ix + i];
                for (j = i + 1; j <= n - 1; j++)
                {
                    v = v - a[ia + i, ja + j] * x[ix + j];
                }
                if (!isunit)
                {
                    v = v / a[ia + i, ja + i];
                }
                x[ix + i] = v;
            }
            return;
        }
        if (optype == 0 && !isupper)
        {
            for (i = 0; i <= n - 1; i++)
            {
                v = x[ix + i];
                for (j = 0; j <= i - 1; j++)
                {
                    v = v - a[ia + i, ja + j] * x[ix + j];
                }
                if (!isunit)
                {
                    v = v / a[ia + i, ja + i];
                }
                x[ix + i] = v;
            }
            return;
        }
        if (optype == 1 && isupper)
        {
            for (i = 0; i <= n - 1; i++)
            {
                v = x[ix + i];
                if (!isunit)
                {
                    v = v / a[ia + i, ja + i];
                }
                x[ix + i] = v;
                if (v == 0)
                {
                    continue;
                }
                for (j = i + 1; j <= n - 1; j++)
                {
                    x[ix + j] = x[ix + j] - v * a[ia + i, ja + j];
                }
            }
            return;
        }
        if (optype == 1 && !isupper)
        {
            for (i = n - 1; i >= 0; i--)
            {
                v = x[ix + i];
                if (!isunit)
                {
                    v = v / a[ia + i, ja + i];
                }
                x[ix + i] = v;
                if (v == 0)
                {
                    continue;
                }
                for (j = 0; j <= i - 1; j++)
                {
                    x[ix + j] = x[ix + j] - v * a[ia + i, ja + j];
                }
            }
            return;
        }
        ap.assert(false, "RMatrixTRSV: unexpected operation type");
    }


    /*************************************************************************
    This subroutine calculates X*op(A^-1) where:
    * X is MxN general matrix
    * A is NxN upper/lower triangular/unitriangular matrix
    * "op" may be identity transformation, transposition, conjugate transposition
    Multiplication result replaces X.

    INPUT PARAMETERS
        N   -   matrix size, N>=0
        M   -   matrix size, N>=0
        A       -   matrix, actial matrix is stored in A[I1:I1+N-1,J1:J1+N-1]
        I1      -   submatrix offset
        J1      -   submatrix offset
        IsUpper -   whether matrix is upper triangular
        IsUnit  -   whether matrix is unitriangular
        OpType  -   transformation type:
                    * 0 - no transformation
                    * 1 - transposition
                    * 2 - conjugate transposition
        X   -   matrix, actial matrix is stored in X[I2:I2+M-1,J2:J2+N-1]
        I2  -   submatrix offset
        J2  -   submatrix offset

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

      -- ALGLIB routine --
         20.01.2018
         Bochkanov Sergey
    *************************************************************************/
    public static void cmatrixrighttrsm(int m,
        int n,
        complex[,] a,
        int i1,
        int j1,
        bool isupper,
        bool isunit,
        int optype,
        complex[,] x,
        int i2,
        int j2,
        xparams _params)
    {
        int s1 = 0;
        int s2 = 0;
        int tsa = 0;
        int tsb = 0;
        int tscur = 0;

        tsa = apserv.matrixtilesizea(_params) / 2;
        tsb = apserv.matrixtilesizeb(_params);
        tscur = tsb;
        if (apserv.imax2(m, n, _params) <= tsb)
        {
            tscur = tsa;
        }
        ap.assert(tscur >= 1, "CMatrixRightTRSM: integrity check failed");

        //
        // Upper level parallelization:
        // * decide whether it is feasible to activate multithreading
        // * perform optionally parallelized splits on M
        //
        if (m >= 2 * tsb && (double)(4 * apserv.rmul3(m, n, n, _params)) >= (double)(apserv.smpactivationlevel(_params)))
        {
            if (_trypexec_cmatrixrighttrsm(m, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params))
            {
                return;
            }
        }
        if (m >= 2 * tsb)
        {

            //
            // Split X: X*A = (X1 X2)^T*A
            //
            apserv.tiledsplit(m, tsb, ref s1, ref s2, _params);
            cmatrixrighttrsm(s1, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
            cmatrixrighttrsm(s2, n, a, i1, j1, isupper, isunit, optype, x, i2 + s1, j2, _params);
            return;
        }

        //
        // Basecase: either MKL-supported code or ALGLIB basecase code
        //
        if (apserv.imax2(m, n, _params) <= tsb)
        {
            if (ablasmkl.cmatrixrighttrsmmkl(m, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params))
            {
                return;
            }
        }
        if (apserv.imax2(m, n, _params) <= tsa)
        {
            cmatrixrighttrsm2(m, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
            return;
        }

        //
        // Recursive subdivision
        //
        if (m >= n)
        {

            //
            // Split X: X*A = (X1 X2)^T*A
            //
            apserv.tiledsplit(m, tscur, ref s1, ref s2, _params);
            cmatrixrighttrsm(s1, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
            cmatrixrighttrsm(s2, n, a, i1, j1, isupper, isunit, optype, x, i2 + s1, j2, _params);
        }
        else
        {

            //
            // Split A:
            //               (A1  A12)
            // X*op(A) = X*op(       )
            //               (     A2)
            //
            // Different variants depending on
            // IsUpper/OpType combinations
            //
            apserv.tiledsplit(n, tscur, ref s1, ref s2, _params);
            if (isupper && optype == 0)
            {

                //
                //                  (A1  A12)-1
                // X*A^-1 = (X1 X2)*(       )
                //                  (     A2)
                //
                cmatrixrighttrsm(m, s1, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
                cmatrixgemm(m, s2, s1, -1.0, x, i2, j2, 0, a, i1, j1 + s1, 0, 1.0, x, i2, j2 + s1, _params);
                cmatrixrighttrsm(m, s2, a, i1 + s1, j1 + s1, isupper, isunit, optype, x, i2, j2 + s1, _params);
            }
            if (isupper && optype != 0)
            {

                //
                //                  (A1'     )-1
                // X*A^-1 = (X1 X2)*(        )
                //                  (A12' A2')
                //
                cmatrixrighttrsm(m, s2, a, i1 + s1, j1 + s1, isupper, isunit, optype, x, i2, j2 + s1, _params);
                cmatrixgemm(m, s1, s2, -1.0, x, i2, j2 + s1, 0, a, i1, j1 + s1, optype, 1.0, x, i2, j2, _params);
                cmatrixrighttrsm(m, s1, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
            }
            if (!isupper && optype == 0)
            {

                //
                //                  (A1     )-1
                // X*A^-1 = (X1 X2)*(       )
                //                  (A21  A2)
                //
                cmatrixrighttrsm(m, s2, a, i1 + s1, j1 + s1, isupper, isunit, optype, x, i2, j2 + s1, _params);
                cmatrixgemm(m, s1, s2, -1.0, x, i2, j2 + s1, 0, a, i1 + s1, j1, 0, 1.0, x, i2, j2, _params);
                cmatrixrighttrsm(m, s1, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
            }
            if (!isupper && optype != 0)
            {

                //
                //                  (A1' A21')-1
                // X*A^-1 = (X1 X2)*(        )
                //                  (     A2')
                //
                cmatrixrighttrsm(m, s1, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
                cmatrixgemm(m, s2, s1, -1.0, x, i2, j2, 0, a, i1 + s1, j1, optype, 1.0, x, i2, j2 + s1, _params);
                cmatrixrighttrsm(m, s2, a, i1 + s1, j1 + s1, isupper, isunit, optype, x, i2, j2 + s1, _params);
            }
        }
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_cmatrixrighttrsm(int m,
        int n,
        complex[,] a,
        int i1,
        int j1,
        bool isupper,
        bool isunit,
        int optype,
        complex[,] x,
        int i2,
        int j2, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    This subroutine calculates op(A^-1)*X where:
    * X is MxN general matrix
    * A is MxM upper/lower triangular/unitriangular matrix
    * "op" may be identity transformation, transposition, conjugate transposition
    Multiplication result replaces X.

    INPUT PARAMETERS
        N   -   matrix size, N>=0
        M   -   matrix size, N>=0
        A       -   matrix, actial matrix is stored in A[I1:I1+M-1,J1:J1+M-1]
        I1      -   submatrix offset
        J1      -   submatrix offset
        IsUpper -   whether matrix is upper triangular
        IsUnit  -   whether matrix is unitriangular
        OpType  -   transformation type:
                    * 0 - no transformation
                    * 1 - transposition
                    * 2 - conjugate transposition
        X   -   matrix, actial matrix is stored in X[I2:I2+M-1,J2:J2+N-1]
        I2  -   submatrix offset
        J2  -   submatrix offset

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

      -- ALGLIB routine --
         15.12.2009-22.01.2018
         Bochkanov Sergey
    *************************************************************************/
    public static void cmatrixlefttrsm(int m,
        int n,
        complex[,] a,
        int i1,
        int j1,
        bool isupper,
        bool isunit,
        int optype,
        complex[,] x,
        int i2,
        int j2,
        xparams _params)
    {
        int s1 = 0;
        int s2 = 0;
        int tsa = 0;
        int tsb = 0;
        int tscur = 0;

        tsa = apserv.matrixtilesizea(_params) / 2;
        tsb = apserv.matrixtilesizeb(_params);
        tscur = tsb;
        if (apserv.imax2(m, n, _params) <= tsb)
        {
            tscur = tsa;
        }
        ap.assert(tscur >= 1, "CMatrixLeftTRSM: integrity check failed");

        //
        // Upper level parallelization:
        // * decide whether it is feasible to activate multithreading
        // * perform optionally parallelized splits on N
        //
        if (n >= 2 * tsb && (double)(4 * apserv.rmul3(n, m, m, _params)) >= (double)(apserv.smpactivationlevel(_params)))
        {
            if (_trypexec_cmatrixlefttrsm(m, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params))
            {
                return;
            }
        }
        if (n >= 2 * tsb)
        {
            apserv.tiledsplit(n, tscur, ref s1, ref s2, _params);
            cmatrixlefttrsm(m, s2, a, i1, j1, isupper, isunit, optype, x, i2, j2 + s1, _params);
            cmatrixlefttrsm(m, s1, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
            return;
        }

        //
        // Basecase: either MKL-supported code or ALGLIB basecase code
        //
        if (apserv.imax2(m, n, _params) <= tsb)
        {
            if (ablasmkl.cmatrixlefttrsmmkl(m, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params))
            {
                return;
            }
        }
        if (apserv.imax2(m, n, _params) <= tsa)
        {
            cmatrixlefttrsm2(m, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
            return;
        }

        //
        // Recursive subdivision
        //
        if (n >= m)
        {

            //
            // Split X: op(A)^-1*X = op(A)^-1*(X1 X2)
            //
            apserv.tiledsplit(n, tscur, ref s1, ref s2, _params);
            cmatrixlefttrsm(m, s1, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
            cmatrixlefttrsm(m, s2, a, i1, j1, isupper, isunit, optype, x, i2, j2 + s1, _params);
        }
        else
        {

            //
            // Split A
            //
            apserv.tiledsplit(m, tscur, ref s1, ref s2, _params);
            if (isupper && optype == 0)
            {

                //
                //           (A1  A12)-1  ( X1 )
                // A^-1*X* = (       )   *(    )
                //           (     A2)    ( X2 )
                //
                cmatrixlefttrsm(s2, n, a, i1 + s1, j1 + s1, isupper, isunit, optype, x, i2 + s1, j2, _params);
                cmatrixgemm(s1, n, s2, -1.0, a, i1, j1 + s1, 0, x, i2 + s1, j2, 0, 1.0, x, i2, j2, _params);
                cmatrixlefttrsm(s1, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
            }
            if (isupper && optype != 0)
            {

                //
                //          (A1'     )-1 ( X1 )
                // A^-1*X = (        )  *(    )
                //          (A12' A2')   ( X2 )
                //
                cmatrixlefttrsm(s1, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
                cmatrixgemm(s2, n, s1, -1.0, a, i1, j1 + s1, optype, x, i2, j2, 0, 1.0, x, i2 + s1, j2, _params);
                cmatrixlefttrsm(s2, n, a, i1 + s1, j1 + s1, isupper, isunit, optype, x, i2 + s1, j2, _params);
            }
            if (!isupper && optype == 0)
            {

                //
                //          (A1     )-1 ( X1 )
                // A^-1*X = (       )  *(    )
                //          (A21  A2)   ( X2 )
                //
                cmatrixlefttrsm(s1, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
                cmatrixgemm(s2, n, s1, -1.0, a, i1 + s1, j1, 0, x, i2, j2, 0, 1.0, x, i2 + s1, j2, _params);
                cmatrixlefttrsm(s2, n, a, i1 + s1, j1 + s1, isupper, isunit, optype, x, i2 + s1, j2, _params);
            }
            if (!isupper && optype != 0)
            {

                //
                //          (A1' A21')-1 ( X1 )
                // A^-1*X = (        )  *(    )
                //          (     A2')   ( X2 )
                //
                cmatrixlefttrsm(s2, n, a, i1 + s1, j1 + s1, isupper, isunit, optype, x, i2 + s1, j2, _params);
                cmatrixgemm(s1, n, s2, -1.0, a, i1 + s1, j1, optype, x, i2 + s1, j2, 0, 1.0, x, i2, j2, _params);
                cmatrixlefttrsm(s1, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
            }
        }
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_cmatrixlefttrsm(int m,
        int n,
        complex[,] a,
        int i1,
        int j1,
        bool isupper,
        bool isunit,
        int optype,
        complex[,] x,
        int i2,
        int j2, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    This subroutine calculates X*op(A^-1) where:
    * X is MxN general matrix
    * A is NxN upper/lower triangular/unitriangular matrix
    * "op" may be identity transformation, transposition
    Multiplication result replaces X.

    INPUT PARAMETERS
        N   -   matrix size, N>=0
        M   -   matrix size, N>=0
        A       -   matrix, actial matrix is stored in A[I1:I1+N-1,J1:J1+N-1]
        I1      -   submatrix offset
        J1      -   submatrix offset
        IsUpper -   whether matrix is upper triangular
        IsUnit  -   whether matrix is unitriangular
        OpType  -   transformation type:
                    * 0 - no transformation
                    * 1 - transposition
        X   -   matrix, actial matrix is stored in X[I2:I2+M-1,J2:J2+N-1]
        I2  -   submatrix offset
        J2  -   submatrix offset

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

      -- ALGLIB routine --
         15.12.2009-22.01.2018
         Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixrighttrsm(int m,
        int n,
        double[,] a,
        int i1,
        int j1,
        bool isupper,
        bool isunit,
        int optype,
        double[,] x,
        int i2,
        int j2,
        xparams _params)
    {
        int s1 = 0;
        int s2 = 0;
        int tsa = 0;
        int tsb = 0;
        int tscur = 0;

        tsa = apserv.matrixtilesizea(_params);
        tsb = apserv.matrixtilesizeb(_params);
        tscur = tsb;
        if (apserv.imax2(m, n, _params) <= tsb)
        {
            tscur = tsa;
        }
        ap.assert(tscur >= 1, "RMatrixRightTRSM: integrity check failed");

        //
        // Upper level parallelization:
        // * decide whether it is feasible to activate multithreading
        // * perform optionally parallelized splits on M
        //
        if (m >= 2 * tsb && (double)(apserv.rmul3(m, n, n, _params)) >= (double)(apserv.smpactivationlevel(_params)))
        {
            if (_trypexec_rmatrixrighttrsm(m, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params))
            {
                return;
            }
        }
        if (m >= 2 * tsb)
        {

            //
            // Split X: X*A = (X1 X2)^T*A
            //
            apserv.tiledsplit(m, tsb, ref s1, ref s2, _params);
            rmatrixrighttrsm(s1, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
            rmatrixrighttrsm(s2, n, a, i1, j1, isupper, isunit, optype, x, i2 + s1, j2, _params);
            return;
        }

        //
        // Basecase: MKL or ALGLIB code
        //
        if (apserv.imax2(m, n, _params) <= tsb)
        {
            if (ablasmkl.rmatrixrighttrsmmkl(m, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params))
            {
                return;
            }
        }
        if (apserv.imax2(m, n, _params) <= tsa)
        {
            rmatrixrighttrsm2(m, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
            return;
        }

        //
        // Recursive subdivision
        //
        if (m >= n)
        {

            //
            // Split X: X*A = (X1 X2)^T*A
            //
            apserv.tiledsplit(m, tscur, ref s1, ref s2, _params);
            rmatrixrighttrsm(s1, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
            rmatrixrighttrsm(s2, n, a, i1, j1, isupper, isunit, optype, x, i2 + s1, j2, _params);
        }
        else
        {

            //
            // Split A:
            //               (A1  A12)
            // X*op(A) = X*op(       )
            //               (     A2)
            //
            // Different variants depending on
            // IsUpper/OpType combinations
            //
            apserv.tiledsplit(n, tscur, ref s1, ref s2, _params);
            if (isupper && optype == 0)
            {

                //
                //                  (A1  A12)-1
                // X*A^-1 = (X1 X2)*(       )
                //                  (     A2)
                //
                rmatrixrighttrsm(m, s1, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
                rmatrixgemm(m, s2, s1, -1.0, x, i2, j2, 0, a, i1, j1 + s1, 0, 1.0, x, i2, j2 + s1, _params);
                rmatrixrighttrsm(m, s2, a, i1 + s1, j1 + s1, isupper, isunit, optype, x, i2, j2 + s1, _params);
            }
            if (isupper && optype != 0)
            {

                //
                //                  (A1'     )-1
                // X*A^-1 = (X1 X2)*(        )
                //                  (A12' A2')
                //
                rmatrixrighttrsm(m, s2, a, i1 + s1, j1 + s1, isupper, isunit, optype, x, i2, j2 + s1, _params);
                rmatrixgemm(m, s1, s2, -1.0, x, i2, j2 + s1, 0, a, i1, j1 + s1, optype, 1.0, x, i2, j2, _params);
                rmatrixrighttrsm(m, s1, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
            }
            if (!isupper && optype == 0)
            {

                //
                //                  (A1     )-1
                // X*A^-1 = (X1 X2)*(       )
                //                  (A21  A2)
                //
                rmatrixrighttrsm(m, s2, a, i1 + s1, j1 + s1, isupper, isunit, optype, x, i2, j2 + s1, _params);
                rmatrixgemm(m, s1, s2, -1.0, x, i2, j2 + s1, 0, a, i1 + s1, j1, 0, 1.0, x, i2, j2, _params);
                rmatrixrighttrsm(m, s1, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
            }
            if (!isupper && optype != 0)
            {

                //
                //                  (A1' A21')-1
                // X*A^-1 = (X1 X2)*(        )
                //                  (     A2')
                //
                rmatrixrighttrsm(m, s1, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
                rmatrixgemm(m, s2, s1, -1.0, x, i2, j2, 0, a, i1 + s1, j1, optype, 1.0, x, i2, j2 + s1, _params);
                rmatrixrighttrsm(m, s2, a, i1 + s1, j1 + s1, isupper, isunit, optype, x, i2, j2 + s1, _params);
            }
        }
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_rmatrixrighttrsm(int m,
        int n,
        double[,] a,
        int i1,
        int j1,
        bool isupper,
        bool isunit,
        int optype,
        double[,] x,
        int i2,
        int j2, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    This subroutine calculates op(A^-1)*X where:
    * X is MxN general matrix
    * A is MxM upper/lower triangular/unitriangular matrix
    * "op" may be identity transformation, transposition
    Multiplication result replaces X.

    INPUT PARAMETERS
        N   -   matrix size, N>=0
        M   -   matrix size, N>=0
        A       -   matrix, actial matrix is stored in A[I1:I1+M-1,J1:J1+M-1]
        I1      -   submatrix offset
        J1      -   submatrix offset
        IsUpper -   whether matrix is upper triangular
        IsUnit  -   whether matrix is unitriangular
        OpType  -   transformation type:
                    * 0 - no transformation
                    * 1 - transposition
        X   -   matrix, actial matrix is stored in X[I2:I2+M-1,J2:J2+N-1]
        I2  -   submatrix offset
        J2  -   submatrix offset

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

      -- ALGLIB routine --
         15.12.2009-22.01.2018
         Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixlefttrsm(int m,
        int n,
        double[,] a,
        int i1,
        int j1,
        bool isupper,
        bool isunit,
        int optype,
        double[,] x,
        int i2,
        int j2,
        xparams _params)
    {
        int s1 = 0;
        int s2 = 0;
        int tsa = 0;
        int tsb = 0;
        int tscur = 0;

        tsa = apserv.matrixtilesizea(_params);
        tsb = apserv.matrixtilesizeb(_params);
        tscur = tsb;
        if (apserv.imax2(m, n, _params) <= tsb)
        {
            tscur = tsa;
        }
        ap.assert(tscur >= 1, "RMatrixLeftTRSMRec: integrity check failed");

        //
        // Upper level parallelization:
        // * decide whether it is feasible to activate multithreading
        // * perform optionally parallelized splits on N
        //
        if (n >= 2 * tsb && (double)(apserv.rmul3(n, m, m, _params)) >= (double)(apserv.smpactivationlevel(_params)))
        {
            if (_trypexec_rmatrixlefttrsm(m, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params))
            {
                return;
            }
        }
        if (n >= 2 * tsb)
        {
            apserv.tiledsplit(n, tscur, ref s1, ref s2, _params);
            rmatrixlefttrsm(m, s2, a, i1, j1, isupper, isunit, optype, x, i2, j2 + s1, _params);
            rmatrixlefttrsm(m, s1, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
            return;
        }

        //
        // Basecase: MKL or ALGLIB code
        //
        if (apserv.imax2(m, n, _params) <= tsb)
        {
            if (ablasmkl.rmatrixlefttrsmmkl(m, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params))
            {
                return;
            }
        }
        if (apserv.imax2(m, n, _params) <= tsa)
        {
            rmatrixlefttrsm2(m, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
            return;
        }

        //
        // Recursive subdivision
        //
        if (n >= m)
        {

            //
            // Split X: op(A)^-1*X = op(A)^-1*(X1 X2)
            //
            apserv.tiledsplit(n, tscur, ref s1, ref s2, _params);
            rmatrixlefttrsm(m, s1, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
            rmatrixlefttrsm(m, s2, a, i1, j1, isupper, isunit, optype, x, i2, j2 + s1, _params);
        }
        else
        {

            //
            // Split A
            //
            apserv.tiledsplit(m, tscur, ref s1, ref s2, _params);
            if (isupper && optype == 0)
            {

                //
                //           (A1  A12)-1  ( X1 )
                // A^-1*X* = (       )   *(    )
                //           (     A2)    ( X2 )
                //
                rmatrixlefttrsm(s2, n, a, i1 + s1, j1 + s1, isupper, isunit, optype, x, i2 + s1, j2, _params);
                rmatrixgemm(s1, n, s2, -1.0, a, i1, j1 + s1, 0, x, i2 + s1, j2, 0, 1.0, x, i2, j2, _params);
                rmatrixlefttrsm(s1, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
            }
            if (isupper && optype != 0)
            {

                //
                //          (A1'     )-1 ( X1 )
                // A^-1*X = (        )  *(    )
                //          (A12' A2')   ( X2 )
                //
                rmatrixlefttrsm(s1, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
                rmatrixgemm(s2, n, s1, -1.0, a, i1, j1 + s1, optype, x, i2, j2, 0, 1.0, x, i2 + s1, j2, _params);
                rmatrixlefttrsm(s2, n, a, i1 + s1, j1 + s1, isupper, isunit, optype, x, i2 + s1, j2, _params);
            }
            if (!isupper && optype == 0)
            {

                //
                //          (A1     )-1 ( X1 )
                // A^-1*X = (       )  *(    )
                //          (A21  A2)   ( X2 )
                //
                rmatrixlefttrsm(s1, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
                rmatrixgemm(s2, n, s1, -1.0, a, i1 + s1, j1, 0, x, i2, j2, 0, 1.0, x, i2 + s1, j2, _params);
                rmatrixlefttrsm(s2, n, a, i1 + s1, j1 + s1, isupper, isunit, optype, x, i2 + s1, j2, _params);
            }
            if (!isupper && optype != 0)
            {

                //
                //          (A1' A21')-1 ( X1 )
                // A^-1*X = (        )  *(    )
                //          (     A2')   ( X2 )
                //
                rmatrixlefttrsm(s2, n, a, i1 + s1, j1 + s1, isupper, isunit, optype, x, i2 + s1, j2, _params);
                rmatrixgemm(s1, n, s2, -1.0, a, i1 + s1, j1, optype, x, i2 + s1, j2, 0, 1.0, x, i2, j2, _params);
                rmatrixlefttrsm(s1, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params);
            }
        }
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_rmatrixlefttrsm(int m,
        int n,
        double[,] a,
        int i1,
        int j1,
        bool isupper,
        bool isunit,
        int optype,
        double[,] x,
        int i2,
        int j2, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    This subroutine calculates  C=alpha*A*A^H+beta*C  or  C=alpha*A^H*A+beta*C
    where:
    * C is NxN Hermitian matrix given by its upper/lower triangle
    * A is NxK matrix when A*A^H is calculated, KxN matrix otherwise

    Additional info:
    * multiplication result replaces C. If Beta=0, C elements are not used in
      calculations (not multiplied by zero - just not referenced)
    * if Alpha=0, A is not used (not multiplied by zero - just not referenced)
    * if both Beta and Alpha are zero, C is filled by zeros.

    INPUT PARAMETERS
        N       -   matrix size, N>=0
        K       -   matrix size, K>=0
        Alpha   -   coefficient
        A       -   matrix
        IA      -   submatrix offset (row index)
        JA      -   submatrix offset (column index)
        OpTypeA -   multiplication type:
                    * 0 - A*A^H is calculated
                    * 2 - A^H*A is calculated
        Beta    -   coefficient
        C       -   preallocated input/output matrix
        IC      -   submatrix offset (row index)
        JC      -   submatrix offset (column index)
        IsUpper -   whether upper or lower triangle of C is updated;
                    this function updates only one half of C, leaving
                    other half unchanged (not referenced at all).

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

      -- ALGLIB routine --
         16.12.2009-22.01.2018
         Bochkanov Sergey
    *************************************************************************/
    public static void cmatrixherk(int n,
        int k,
        double alpha,
        complex[,] a,
        int ia,
        int ja,
        int optypea,
        double beta,
        complex[,] c,
        int ic,
        int jc,
        bool isupper,
        xparams _params)
    {
        int s1 = 0;
        int s2 = 0;
        int tsa = 0;
        int tsb = 0;
        int tscur = 0;

        tsa = apserv.matrixtilesizea(_params) / 2;
        tsb = apserv.matrixtilesizeb(_params);
        tscur = tsb;
        if (apserv.imax2(n, k, _params) <= tsb)
        {
            tscur = tsa;
        }
        ap.assert(tscur >= 1, "CMatrixHERK: integrity check failed");

        //
        // Decide whether it is feasible to activate multithreading
        //
        if (n >= 2 * tsb && (double)(8 * apserv.rmul3(k, n, n, _params) / 2) >= (double)(apserv.smpactivationlevel(_params)))
        {
            if (_trypexec_cmatrixherk(n, k, alpha, a, ia, ja, optypea, beta, c, ic, jc, isupper, _params))
            {
                return;
            }
        }

        //
        // Use MKL or ALGLIB basecase code
        //
        if (apserv.imax2(n, k, _params) <= tsb)
        {
            if (ablasmkl.cmatrixherkmkl(n, k, alpha, a, ia, ja, optypea, beta, c, ic, jc, isupper, _params))
            {
                return;
            }
        }
        if (apserv.imax2(n, k, _params) <= tsa)
        {
            cmatrixherk2(n, k, alpha, a, ia, ja, optypea, beta, c, ic, jc, isupper, _params);
            return;
        }

        //
        // Recursive division of the problem
        //
        if (k >= n)
        {

            //
            // Split K
            //
            apserv.tiledsplit(k, tscur, ref s1, ref s2, _params);
            if (optypea == 0)
            {
                cmatrixherk(n, s1, alpha, a, ia, ja, optypea, beta, c, ic, jc, isupper, _params);
                cmatrixherk(n, s2, alpha, a, ia, ja + s1, optypea, 1.0, c, ic, jc, isupper, _params);
            }
            else
            {
                cmatrixherk(n, s1, alpha, a, ia, ja, optypea, beta, c, ic, jc, isupper, _params);
                cmatrixherk(n, s2, alpha, a, ia + s1, ja, optypea, 1.0, c, ic, jc, isupper, _params);
            }
        }
        else
        {

            //
            // Split N
            //
            apserv.tiledsplit(n, tscur, ref s1, ref s2, _params);
            if (optypea == 0 && isupper)
            {
                cmatrixherk(s1, k, alpha, a, ia, ja, optypea, beta, c, ic, jc, isupper, _params);
                cmatrixherk(s2, k, alpha, a, ia + s1, ja, optypea, beta, c, ic + s1, jc + s1, isupper, _params);
                cmatrixgemm(s1, s2, k, alpha, a, ia, ja, 0, a, ia + s1, ja, 2, beta, c, ic, jc + s1, _params);
            }
            if (optypea == 0 && !isupper)
            {
                cmatrixherk(s1, k, alpha, a, ia, ja, optypea, beta, c, ic, jc, isupper, _params);
                cmatrixherk(s2, k, alpha, a, ia + s1, ja, optypea, beta, c, ic + s1, jc + s1, isupper, _params);
                cmatrixgemm(s2, s1, k, alpha, a, ia + s1, ja, 0, a, ia, ja, 2, beta, c, ic + s1, jc, _params);
            }
            if (optypea != 0 && isupper)
            {
                cmatrixherk(s1, k, alpha, a, ia, ja, optypea, beta, c, ic, jc, isupper, _params);
                cmatrixherk(s2, k, alpha, a, ia, ja + s1, optypea, beta, c, ic + s1, jc + s1, isupper, _params);
                cmatrixgemm(s1, s2, k, alpha, a, ia, ja, 2, a, ia, ja + s1, 0, beta, c, ic, jc + s1, _params);
            }
            if (optypea != 0 && !isupper)
            {
                cmatrixherk(s1, k, alpha, a, ia, ja, optypea, beta, c, ic, jc, isupper, _params);
                cmatrixherk(s2, k, alpha, a, ia, ja + s1, optypea, beta, c, ic + s1, jc + s1, isupper, _params);
                cmatrixgemm(s2, s1, k, alpha, a, ia, ja + s1, 2, a, ia, ja, 0, beta, c, ic + s1, jc, _params);
            }
        }
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_cmatrixherk(int n,
        int k,
        double alpha,
        complex[,] a,
        int ia,
        int ja,
        int optypea,
        double beta,
        complex[,] c,
        int ic,
        int jc,
        bool isupper, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    This subroutine calculates  C=alpha*A*A^T+beta*C  or  C=alpha*A^T*A+beta*C
    where:
    * C is NxN symmetric matrix given by its upper/lower triangle
    * A is NxK matrix when A*A^T is calculated, KxN matrix otherwise

    Additional info:
    * multiplication result replaces C. If Beta=0, C elements are not used in
      calculations (not multiplied by zero - just not referenced)
    * if Alpha=0, A is not used (not multiplied by zero - just not referenced)
    * if both Beta and Alpha are zero, C is filled by zeros.

    INPUT PARAMETERS
        N       -   matrix size, N>=0
        K       -   matrix size, K>=0
        Alpha   -   coefficient
        A       -   matrix
        IA      -   submatrix offset (row index)
        JA      -   submatrix offset (column index)
        OpTypeA -   multiplication type:
                    * 0 - A*A^T is calculated
                    * 2 - A^T*A is calculated
        Beta    -   coefficient
        C       -   preallocated input/output matrix
        IC      -   submatrix offset (row index)
        JC      -   submatrix offset (column index)
        IsUpper -   whether C is upper triangular or lower triangular

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

      -- ALGLIB routine --
         16.12.2009-22.01.2018
         Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixsyrk(int n,
        int k,
        double alpha,
        double[,] a,
        int ia,
        int ja,
        int optypea,
        double beta,
        double[,] c,
        int ic,
        int jc,
        bool isupper,
        xparams _params)
    {
        int s1 = 0;
        int s2 = 0;
        int tsa = 0;
        int tsb = 0;
        int tscur = 0;

        tsa = apserv.matrixtilesizea(_params);
        tsb = apserv.matrixtilesizeb(_params);
        tscur = tsb;
        if (apserv.imax2(n, k, _params) <= tsb)
        {
            tscur = tsa;
        }
        ap.assert(tscur >= 1, "RMatrixSYRK: integrity check failed");

        //
        // Decide whether it is feasible to activate multithreading
        //
        if (n >= 2 * tsb && (double)(2 * apserv.rmul3(k, n, n, _params) / 2) >= (double)(apserv.smpactivationlevel(_params)))
        {
            if (_trypexec_rmatrixsyrk(n, k, alpha, a, ia, ja, optypea, beta, c, ic, jc, isupper, _params))
            {
                return;
            }
        }

        //
        // Use MKL or generic basecase code
        //
        if (apserv.imax2(n, k, _params) <= tsb)
        {
            if (ablasmkl.rmatrixsyrkmkl(n, k, alpha, a, ia, ja, optypea, beta, c, ic, jc, isupper, _params))
            {
                return;
            }
        }
        if (apserv.imax2(n, k, _params) <= tsa)
        {
            rmatrixsyrk2(n, k, alpha, a, ia, ja, optypea, beta, c, ic, jc, isupper, _params);
            return;
        }

        //
        // Recursive subdivision of the problem
        //
        if (k >= n)
        {

            //
            // Split K
            //
            apserv.tiledsplit(k, tscur, ref s1, ref s2, _params);
            if (optypea == 0)
            {
                rmatrixsyrk(n, s1, alpha, a, ia, ja, optypea, beta, c, ic, jc, isupper, _params);
                rmatrixsyrk(n, s2, alpha, a, ia, ja + s1, optypea, 1.0, c, ic, jc, isupper, _params);
            }
            else
            {
                rmatrixsyrk(n, s1, alpha, a, ia, ja, optypea, beta, c, ic, jc, isupper, _params);
                rmatrixsyrk(n, s2, alpha, a, ia + s1, ja, optypea, 1.0, c, ic, jc, isupper, _params);
            }
        }
        else
        {

            //
            // Split N
            //
            apserv.tiledsplit(n, tscur, ref s1, ref s2, _params);
            if (optypea == 0 && isupper)
            {
                rmatrixsyrk(s1, k, alpha, a, ia, ja, optypea, beta, c, ic, jc, isupper, _params);
                rmatrixsyrk(s2, k, alpha, a, ia + s1, ja, optypea, beta, c, ic + s1, jc + s1, isupper, _params);
                rmatrixgemm(s1, s2, k, alpha, a, ia, ja, 0, a, ia + s1, ja, 1, beta, c, ic, jc + s1, _params);
            }
            if (optypea == 0 && !isupper)
            {
                rmatrixsyrk(s1, k, alpha, a, ia, ja, optypea, beta, c, ic, jc, isupper, _params);
                rmatrixsyrk(s2, k, alpha, a, ia + s1, ja, optypea, beta, c, ic + s1, jc + s1, isupper, _params);
                rmatrixgemm(s2, s1, k, alpha, a, ia + s1, ja, 0, a, ia, ja, 1, beta, c, ic + s1, jc, _params);
            }
            if (optypea != 0 && isupper)
            {
                rmatrixsyrk(s1, k, alpha, a, ia, ja, optypea, beta, c, ic, jc, isupper, _params);
                rmatrixsyrk(s2, k, alpha, a, ia, ja + s1, optypea, beta, c, ic + s1, jc + s1, isupper, _params);
                rmatrixgemm(s1, s2, k, alpha, a, ia, ja, 1, a, ia, ja + s1, 0, beta, c, ic, jc + s1, _params);
            }
            if (optypea != 0 && !isupper)
            {
                rmatrixsyrk(s1, k, alpha, a, ia, ja, optypea, beta, c, ic, jc, isupper, _params);
                rmatrixsyrk(s2, k, alpha, a, ia, ja + s1, optypea, beta, c, ic + s1, jc + s1, isupper, _params);
                rmatrixgemm(s2, s1, k, alpha, a, ia, ja + s1, 1, a, ia, ja, 0, beta, c, ic + s1, jc, _params);
            }
        }
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_rmatrixsyrk(int n,
        int k,
        double alpha,
        double[,] a,
        int ia,
        int ja,
        int optypea,
        double beta,
        double[,] c,
        int ic,
        int jc,
        bool isupper, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    This subroutine calculates C = alpha*op1(A)*op2(B) +beta*C where:
    * C is MxN general matrix
    * op1(A) is MxK matrix
    * op2(B) is KxN matrix
    * "op" may be identity transformation, transposition, conjugate transposition

    Additional info:
    * cache-oblivious algorithm is used.
    * multiplication result replaces C. If Beta=0, C elements are not used in
      calculations (not multiplied by zero - just not referenced)
    * if Alpha=0, A is not used (not multiplied by zero - just not referenced)
    * if both Beta and Alpha are zero, C is filled by zeros.

    IMPORTANT:

    This function does NOT preallocate output matrix C, it MUST be preallocated
    by caller prior to calling this function. In case C does not have  enough
    space to store result, exception will be generated.

    INPUT PARAMETERS
        M       -   matrix size, M>0
        N       -   matrix size, N>0
        K       -   matrix size, K>0
        Alpha   -   coefficient
        A       -   matrix
        IA      -   submatrix offset
        JA      -   submatrix offset
        OpTypeA -   transformation type:
                    * 0 - no transformation
                    * 1 - transposition
                    * 2 - conjugate transposition
        B       -   matrix
        IB      -   submatrix offset
        JB      -   submatrix offset
        OpTypeB -   transformation type:
                    * 0 - no transformation
                    * 1 - transposition
                    * 2 - conjugate transposition
        Beta    -   coefficient
        C       -   matrix (PREALLOCATED, large enough to store result)
        IC      -   submatrix offset
        JC      -   submatrix offset

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

      -- ALGLIB routine --
         2009-2019
         Bochkanov Sergey
    *************************************************************************/
    public static void cmatrixgemm(int m,
        int n,
        int k,
        complex alpha,
        complex[,] a,
        int ia,
        int ja,
        int optypea,
        complex[,] b,
        int ib,
        int jb,
        int optypeb,
        complex beta,
        complex[,] c,
        int ic,
        int jc,
        xparams _params)
    {
        int ts = 0;

        ts = apserv.matrixtilesizeb(_params);

        //
        // Check input sizes for correctness
        //
        ap.assert((optypea == 0 || optypea == 1) || optypea == 2, "CMatrixGEMM: incorrect OpTypeA (must be 0 or 1 or 2)");
        ap.assert((optypeb == 0 || optypeb == 1) || optypeb == 2, "CMatrixGEMM: incorrect OpTypeB (must be 0 or 1 or 2)");
        ap.assert(ic + m <= ap.rows(c), "CMatrixGEMM: incorect size of output matrix C");
        ap.assert(jc + n <= ap.cols(c), "CMatrixGEMM: incorect size of output matrix C");

        //
        // Decide whether it is feasible to activate multithreading
        //
        if ((m >= 2 * ts || n >= 2 * ts) && (double)(8 * apserv.rmul3(m, n, k, _params)) >= (double)(apserv.smpactivationlevel(_params)))
        {
            if (_trypexec_cmatrixgemm(m, n, k, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc, _params))
            {
                return;
            }
        }

        //
        // Start actual work
        //
        cmatrixgemmrec(m, n, k, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc, _params);
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_cmatrixgemm(int m,
        int n,
        int k,
        complex alpha,
        complex[,] a,
        int ia,
        int ja,
        int optypea,
        complex[,] b,
        int ib,
        int jb,
        int optypeb,
        complex beta,
        complex[,] c,
        int ic,
        int jc, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    This subroutine calculates C = alpha*op1(A)*op2(B) +beta*C where:
    * C is MxN general matrix
    * op1(A) is MxK matrix
    * op2(B) is KxN matrix
    * "op" may be identity transformation, transposition

    Additional info:
    * cache-oblivious algorithm is used.
    * multiplication result replaces C. If Beta=0, C elements are not used in
      calculations (not multiplied by zero - just not referenced)
    * if Alpha=0, A is not used (not multiplied by zero - just not referenced)
    * if both Beta and Alpha are zero, C is filled by zeros.

    IMPORTANT:

    This function does NOT preallocate output matrix C, it MUST be preallocated
    by caller prior to calling this function. In case C does not have  enough
    space to store result, exception will be generated.

    INPUT PARAMETERS
        M       -   matrix size, M>0
        N       -   matrix size, N>0
        K       -   matrix size, K>0
        Alpha   -   coefficient
        A       -   matrix
        IA      -   submatrix offset
        JA      -   submatrix offset
        OpTypeA -   transformation type:
                    * 0 - no transformation
                    * 1 - transposition
        B       -   matrix
        IB      -   submatrix offset
        JB      -   submatrix offset
        OpTypeB -   transformation type:
                    * 0 - no transformation
                    * 1 - transposition
        Beta    -   coefficient
        C       -   PREALLOCATED output matrix, large enough to store result
        IC      -   submatrix offset
        JC      -   submatrix offset

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

      -- ALGLIB routine --
         2009-2019
         Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixgemm(int m,
        int n,
        int k,
        double alpha,
        double[,] a,
        int ia,
        int ja,
        int optypea,
        double[,] b,
        int ib,
        int jb,
        int optypeb,
        double beta,
        double[,] c,
        int ic,
        int jc,
        xparams _params)
    {
        int ts = 0;

        ts = apserv.matrixtilesizeb(_params);

        //
        // Check input sizes for correctness
        //
        ap.assert(optypea == 0 || optypea == 1, "RMatrixGEMM: incorrect OpTypeA (must be 0 or 1)");
        ap.assert(optypeb == 0 || optypeb == 1, "RMatrixGEMM: incorrect OpTypeB (must be 0 or 1)");
        ap.assert(ic + m <= ap.rows(c), "RMatrixGEMM: incorect size of output matrix C");
        ap.assert(jc + n <= ap.cols(c), "RMatrixGEMM: incorect size of output matrix C");

        //
        // Decide whether it is feasible to activate multithreading
        //
        if ((m >= 2 * ts || n >= 2 * ts) && (double)(2 * apserv.rmul3(m, n, k, _params)) >= (double)(apserv.smpactivationlevel(_params)))
        {
            if (_trypexec_rmatrixgemm(m, n, k, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc, _params))
            {
                return;
            }
        }

        //
        // Start actual work
        //
        rmatrixgemmrec(m, n, k, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc, _params);
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_rmatrixgemm(int m,
        int n,
        int k,
        double alpha,
        double[,] a,
        int ia,
        int ja,
        int optypea,
        double[,] b,
        int ib,
        int jb,
        int optypeb,
        double beta,
        double[,] c,
        int ic,
        int jc, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    This subroutine is an older version of CMatrixHERK(), one with wrong  name
    (it is HErmitian update, not SYmmetric). It  is  left  here  for  backward
    compatibility.

      -- ALGLIB routine --
         16.12.2009
         Bochkanov Sergey
    *************************************************************************/
    public static void cmatrixsyrk(int n,
        int k,
        double alpha,
        complex[,] a,
        int ia,
        int ja,
        int optypea,
        double beta,
        complex[,] c,
        int ic,
        int jc,
        bool isupper,
        xparams _params)
    {
        cmatrixherk(n, k, alpha, a, ia, ja, optypea, beta, c, ic, jc, isupper, _params);
    }


    /*************************************************************************
    Performs one step  of stable Gram-Schmidt  process  on  vector  X[]  using
    set of orthonormal rows Q[].

    INPUT PARAMETERS:
        Q       -   array[M,N], matrix with orthonormal rows
        M, N    -   rows/cols
        X       -   array[N], vector to process
        NeedQX  -   whether we need QX or not 

    OUTPUT PARAMETERS:
        X       -   stores X - Q'*(Q*X)
        QX      -   if NeedQX is True, array[M] filled with elements  of  Q*X,
                    reallocated if length is less than M.
                    Ignored otherwise.
                    
    NOTE: this function silently exits when M=0, doing nothing

      -- ALGLIB --
         Copyright 20.01.2020 by Bochkanov Sergey
    *************************************************************************/
    public static void rowwisegramschmidt(double[,] q,
        int m,
        int n,
        double[] x,
        ref double[] qx,
        bool needqx,
        xparams _params)
    {
        int i = 0;
        double v = 0;

        if (m == 0)
        {
            return;
        }
        if (needqx)
        {
            apserv.rvectorsetlengthatleast(ref qx, m, _params);
        }
        for (i = 0; i <= m - 1; i++)
        {
            v = ablasf.rdotvr(n, x, q, i, _params);
            ablasf.raddrv(n, -v, q, i, x, _params);
            if (needqx)
            {
                qx[i] = v;
            }
        }
    }


    /*************************************************************************
    Complex ABLASSplitLength

      -- ALGLIB routine --
         15.12.2009
         Bochkanov Sergey
    *************************************************************************/
    private static void ablasinternalsplitlength(int n,
        int nb,
        ref int n1,
        ref int n2,
        xparams _params)
    {
        int r = 0;

        n1 = 0;
        n2 = 0;

        if (n <= nb)
        {

            //
            // Block size, no further splitting
            //
            n1 = n;
            n2 = 0;
        }
        else
        {

            //
            // Greater than block size
            //
            if (n % nb != 0)
            {

                //
                // Split remainder
                //
                n2 = n % nb;
                n1 = n - n2;
            }
            else
            {

                //
                // Split on block boundaries
                //
                n2 = n / 2;
                n1 = n - n2;
                if (n1 % nb == 0)
                {
                    return;
                }
                r = nb - n1 % nb;
                n1 = n1 + r;
                n2 = n2 - r;
            }
        }
    }


    /*************************************************************************
    Level 2 variant of CMatrixRightTRSM
    *************************************************************************/
    private static void cmatrixrighttrsm2(int m,
        int n,
        complex[,] a,
        int i1,
        int j1,
        bool isupper,
        bool isunit,
        int optype,
        complex[,] x,
        int i2,
        int j2,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        complex vc = 0;
        complex vd = 0;
        int i_ = 0;
        int i1_ = 0;


        //
        // Special case
        //
        if (n * m == 0)
        {
            return;
        }

        //
        // Try to call fast TRSM
        //
        if (ablasf.cmatrixrighttrsmf(m, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params))
        {
            return;
        }

        //
        // General case
        //
        if (isupper)
        {

            //
            // Upper triangular matrix
            //
            if (optype == 0)
            {

                //
                // X*A^(-1)
                //
                for (i = 0; i <= m - 1; i++)
                {
                    for (j = 0; j <= n - 1; j++)
                    {
                        if (isunit)
                        {
                            vd = 1;
                        }
                        else
                        {
                            vd = a[i1 + j, j1 + j];
                        }
                        x[i2 + i, j2 + j] = x[i2 + i, j2 + j] / vd;
                        if (j < n - 1)
                        {
                            vc = x[i2 + i, j2 + j];
                            i1_ = (j1 + j + 1) - (j2 + j + 1);
                            for (i_ = j2 + j + 1; i_ <= j2 + n - 1; i_++)
                            {
                                x[i2 + i, i_] = x[i2 + i, i_] - vc * a[i1 + j, i_ + i1_];
                            }
                        }
                    }
                }
                return;
            }
            if (optype == 1)
            {

                //
                // X*A^(-T)
                //
                for (i = 0; i <= m - 1; i++)
                {
                    for (j = n - 1; j >= 0; j--)
                    {
                        vc = 0;
                        vd = 1;
                        if (j < n - 1)
                        {
                            i1_ = (j1 + j + 1) - (j2 + j + 1);
                            vc = 0.0;
                            for (i_ = j2 + j + 1; i_ <= j2 + n - 1; i_++)
                            {
                                vc += x[i2 + i, i_] * a[i1 + j, i_ + i1_];
                            }
                        }
                        if (!isunit)
                        {
                            vd = a[i1 + j, j1 + j];
                        }
                        x[i2 + i, j2 + j] = (x[i2 + i, j2 + j] - vc) / vd;
                    }
                }
                return;
            }
            if (optype == 2)
            {

                //
                // X*A^(-H)
                //
                for (i = 0; i <= m - 1; i++)
                {
                    for (j = n - 1; j >= 0; j--)
                    {
                        vc = 0;
                        vd = 1;
                        if (j < n - 1)
                        {
                            i1_ = (j1 + j + 1) - (j2 + j + 1);
                            vc = 0.0;
                            for (i_ = j2 + j + 1; i_ <= j2 + n - 1; i_++)
                            {
                                vc += x[i2 + i, i_] * math.conj(a[i1 + j, i_ + i1_]);
                            }
                        }
                        if (!isunit)
                        {
                            vd = math.conj(a[i1 + j, j1 + j]);
                        }
                        x[i2 + i, j2 + j] = (x[i2 + i, j2 + j] - vc) / vd;
                    }
                }
                return;
            }
        }
        else
        {

            //
            // Lower triangular matrix
            //
            if (optype == 0)
            {

                //
                // X*A^(-1)
                //
                for (i = 0; i <= m - 1; i++)
                {
                    for (j = n - 1; j >= 0; j--)
                    {
                        if (isunit)
                        {
                            vd = 1;
                        }
                        else
                        {
                            vd = a[i1 + j, j1 + j];
                        }
                        x[i2 + i, j2 + j] = x[i2 + i, j2 + j] / vd;
                        if (j > 0)
                        {
                            vc = x[i2 + i, j2 + j];
                            i1_ = (j1) - (j2);
                            for (i_ = j2; i_ <= j2 + j - 1; i_++)
                            {
                                x[i2 + i, i_] = x[i2 + i, i_] - vc * a[i1 + j, i_ + i1_];
                            }
                        }
                    }
                }
                return;
            }
            if (optype == 1)
            {

                //
                // X*A^(-T)
                //
                for (i = 0; i <= m - 1; i++)
                {
                    for (j = 0; j <= n - 1; j++)
                    {
                        vc = 0;
                        vd = 1;
                        if (j > 0)
                        {
                            i1_ = (j1) - (j2);
                            vc = 0.0;
                            for (i_ = j2; i_ <= j2 + j - 1; i_++)
                            {
                                vc += x[i2 + i, i_] * a[i1 + j, i_ + i1_];
                            }
                        }
                        if (!isunit)
                        {
                            vd = a[i1 + j, j1 + j];
                        }
                        x[i2 + i, j2 + j] = (x[i2 + i, j2 + j] - vc) / vd;
                    }
                }
                return;
            }
            if (optype == 2)
            {

                //
                // X*A^(-H)
                //
                for (i = 0; i <= m - 1; i++)
                {
                    for (j = 0; j <= n - 1; j++)
                    {
                        vc = 0;
                        vd = 1;
                        if (j > 0)
                        {
                            i1_ = (j1) - (j2);
                            vc = 0.0;
                            for (i_ = j2; i_ <= j2 + j - 1; i_++)
                            {
                                vc += x[i2 + i, i_] * math.conj(a[i1 + j, i_ + i1_]);
                            }
                        }
                        if (!isunit)
                        {
                            vd = math.conj(a[i1 + j, j1 + j]);
                        }
                        x[i2 + i, j2 + j] = (x[i2 + i, j2 + j] - vc) / vd;
                    }
                }
                return;
            }
        }
    }


    /*************************************************************************
    Level-2 subroutine
    *************************************************************************/
    private static void cmatrixlefttrsm2(int m,
        int n,
        complex[,] a,
        int i1,
        int j1,
        bool isupper,
        bool isunit,
        int optype,
        complex[,] x,
        int i2,
        int j2,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        complex vc = 0;
        complex vd = 0;
        int i_ = 0;


        //
        // Special case
        //
        if (n * m == 0)
        {
            return;
        }

        //
        // Try to call fast TRSM
        //
        if (ablasf.cmatrixlefttrsmf(m, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params))
        {
            return;
        }

        //
        // General case
        //
        if (isupper)
        {

            //
            // Upper triangular matrix
            //
            if (optype == 0)
            {

                //
                // A^(-1)*X
                //
                for (i = m - 1; i >= 0; i--)
                {
                    for (j = i + 1; j <= m - 1; j++)
                    {
                        vc = a[i1 + i, j1 + j];
                        for (i_ = j2; i_ <= j2 + n - 1; i_++)
                        {
                            x[i2 + i, i_] = x[i2 + i, i_] - vc * x[i2 + j, i_];
                        }
                    }
                    if (!isunit)
                    {
                        vd = 1 / a[i1 + i, j1 + i];
                        for (i_ = j2; i_ <= j2 + n - 1; i_++)
                        {
                            x[i2 + i, i_] = vd * x[i2 + i, i_];
                        }
                    }
                }
                return;
            }
            if (optype == 1)
            {

                //
                // A^(-T)*X
                //
                for (i = 0; i <= m - 1; i++)
                {
                    if (isunit)
                    {
                        vd = 1;
                    }
                    else
                    {
                        vd = 1 / a[i1 + i, j1 + i];
                    }
                    for (i_ = j2; i_ <= j2 + n - 1; i_++)
                    {
                        x[i2 + i, i_] = vd * x[i2 + i, i_];
                    }
                    for (j = i + 1; j <= m - 1; j++)
                    {
                        vc = a[i1 + i, j1 + j];
                        for (i_ = j2; i_ <= j2 + n - 1; i_++)
                        {
                            x[i2 + j, i_] = x[i2 + j, i_] - vc * x[i2 + i, i_];
                        }
                    }
                }
                return;
            }
            if (optype == 2)
            {

                //
                // A^(-H)*X
                //
                for (i = 0; i <= m - 1; i++)
                {
                    if (isunit)
                    {
                        vd = 1;
                    }
                    else
                    {
                        vd = 1 / math.conj(a[i1 + i, j1 + i]);
                    }
                    for (i_ = j2; i_ <= j2 + n - 1; i_++)
                    {
                        x[i2 + i, i_] = vd * x[i2 + i, i_];
                    }
                    for (j = i + 1; j <= m - 1; j++)
                    {
                        vc = math.conj(a[i1 + i, j1 + j]);
                        for (i_ = j2; i_ <= j2 + n - 1; i_++)
                        {
                            x[i2 + j, i_] = x[i2 + j, i_] - vc * x[i2 + i, i_];
                        }
                    }
                }
                return;
            }
        }
        else
        {

            //
            // Lower triangular matrix
            //
            if (optype == 0)
            {

                //
                // A^(-1)*X
                //
                for (i = 0; i <= m - 1; i++)
                {
                    for (j = 0; j <= i - 1; j++)
                    {
                        vc = a[i1 + i, j1 + j];
                        for (i_ = j2; i_ <= j2 + n - 1; i_++)
                        {
                            x[i2 + i, i_] = x[i2 + i, i_] - vc * x[i2 + j, i_];
                        }
                    }
                    if (isunit)
                    {
                        vd = 1;
                    }
                    else
                    {
                        vd = 1 / a[i1 + j, j1 + j];
                    }
                    for (i_ = j2; i_ <= j2 + n - 1; i_++)
                    {
                        x[i2 + i, i_] = vd * x[i2 + i, i_];
                    }
                }
                return;
            }
            if (optype == 1)
            {

                //
                // A^(-T)*X
                //
                for (i = m - 1; i >= 0; i--)
                {
                    if (isunit)
                    {
                        vd = 1;
                    }
                    else
                    {
                        vd = 1 / a[i1 + i, j1 + i];
                    }
                    for (i_ = j2; i_ <= j2 + n - 1; i_++)
                    {
                        x[i2 + i, i_] = vd * x[i2 + i, i_];
                    }
                    for (j = i - 1; j >= 0; j--)
                    {
                        vc = a[i1 + i, j1 + j];
                        for (i_ = j2; i_ <= j2 + n - 1; i_++)
                        {
                            x[i2 + j, i_] = x[i2 + j, i_] - vc * x[i2 + i, i_];
                        }
                    }
                }
                return;
            }
            if (optype == 2)
            {

                //
                // A^(-H)*X
                //
                for (i = m - 1; i >= 0; i--)
                {
                    if (isunit)
                    {
                        vd = 1;
                    }
                    else
                    {
                        vd = 1 / math.conj(a[i1 + i, j1 + i]);
                    }
                    for (i_ = j2; i_ <= j2 + n - 1; i_++)
                    {
                        x[i2 + i, i_] = vd * x[i2 + i, i_];
                    }
                    for (j = i - 1; j >= 0; j--)
                    {
                        vc = math.conj(a[i1 + i, j1 + j]);
                        for (i_ = j2; i_ <= j2 + n - 1; i_++)
                        {
                            x[i2 + j, i_] = x[i2 + j, i_] - vc * x[i2 + i, i_];
                        }
                    }
                }
                return;
            }
        }
    }


    /*************************************************************************
    Level 2 subroutine

      -- ALGLIB routine --
         15.12.2009
         Bochkanov Sergey
    *************************************************************************/
    private static void rmatrixrighttrsm2(int m,
        int n,
        double[,] a,
        int i1,
        int j1,
        bool isupper,
        bool isunit,
        int optype,
        double[,] x,
        int i2,
        int j2,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double vr = 0;
        double vd = 0;
        int i_ = 0;
        int i1_ = 0;


        //
        // Special case
        //
        if (n * m == 0)
        {
            return;
        }

        //
        // Try to use "fast" code
        //
        if (ablasf.rmatrixrighttrsmf(m, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params))
        {
            return;
        }

        //
        // General case
        //
        if (isupper)
        {

            //
            // Upper triangular matrix
            //
            if (optype == 0)
            {

                //
                // X*A^(-1)
                //
                for (i = 0; i <= m - 1; i++)
                {
                    for (j = 0; j <= n - 1; j++)
                    {
                        if (isunit)
                        {
                            vd = 1;
                        }
                        else
                        {
                            vd = a[i1 + j, j1 + j];
                        }
                        x[i2 + i, j2 + j] = x[i2 + i, j2 + j] / vd;
                        if (j < n - 1)
                        {
                            vr = x[i2 + i, j2 + j];
                            i1_ = (j1 + j + 1) - (j2 + j + 1);
                            for (i_ = j2 + j + 1; i_ <= j2 + n - 1; i_++)
                            {
                                x[i2 + i, i_] = x[i2 + i, i_] - vr * a[i1 + j, i_ + i1_];
                            }
                        }
                    }
                }
                return;
            }
            if (optype == 1)
            {

                //
                // X*A^(-T)
                //
                for (i = 0; i <= m - 1; i++)
                {
                    for (j = n - 1; j >= 0; j--)
                    {
                        vr = 0;
                        vd = 1;
                        if (j < n - 1)
                        {
                            i1_ = (j1 + j + 1) - (j2 + j + 1);
                            vr = 0.0;
                            for (i_ = j2 + j + 1; i_ <= j2 + n - 1; i_++)
                            {
                                vr += x[i2 + i, i_] * a[i1 + j, i_ + i1_];
                            }
                        }
                        if (!isunit)
                        {
                            vd = a[i1 + j, j1 + j];
                        }
                        x[i2 + i, j2 + j] = (x[i2 + i, j2 + j] - vr) / vd;
                    }
                }
                return;
            }
        }
        else
        {

            //
            // Lower triangular matrix
            //
            if (optype == 0)
            {

                //
                // X*A^(-1)
                //
                for (i = 0; i <= m - 1; i++)
                {
                    for (j = n - 1; j >= 0; j--)
                    {
                        if (isunit)
                        {
                            vd = 1;
                        }
                        else
                        {
                            vd = a[i1 + j, j1 + j];
                        }
                        x[i2 + i, j2 + j] = x[i2 + i, j2 + j] / vd;
                        if (j > 0)
                        {
                            vr = x[i2 + i, j2 + j];
                            i1_ = (j1) - (j2);
                            for (i_ = j2; i_ <= j2 + j - 1; i_++)
                            {
                                x[i2 + i, i_] = x[i2 + i, i_] - vr * a[i1 + j, i_ + i1_];
                            }
                        }
                    }
                }
                return;
            }
            if (optype == 1)
            {

                //
                // X*A^(-T)
                //
                for (i = 0; i <= m - 1; i++)
                {
                    for (j = 0; j <= n - 1; j++)
                    {
                        vr = 0;
                        vd = 1;
                        if (j > 0)
                        {
                            i1_ = (j1) - (j2);
                            vr = 0.0;
                            for (i_ = j2; i_ <= j2 + j - 1; i_++)
                            {
                                vr += x[i2 + i, i_] * a[i1 + j, i_ + i1_];
                            }
                        }
                        if (!isunit)
                        {
                            vd = a[i1 + j, j1 + j];
                        }
                        x[i2 + i, j2 + j] = (x[i2 + i, j2 + j] - vr) / vd;
                    }
                }
                return;
            }
        }
    }


    /*************************************************************************
    Level 2 subroutine
    *************************************************************************/
    private static void rmatrixlefttrsm2(int m,
        int n,
        double[,] a,
        int i1,
        int j1,
        bool isupper,
        bool isunit,
        int optype,
        double[,] x,
        int i2,
        int j2,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double vr = 0;
        double vd = 0;
        int i_ = 0;


        //
        // Special case
        //
        if (n == 0 || m == 0)
        {
            return;
        }

        //
        // Try fast code
        //
        if (ablasf.rmatrixlefttrsmf(m, n, a, i1, j1, isupper, isunit, optype, x, i2, j2, _params))
        {
            return;
        }

        //
        // General case
        //
        if (isupper)
        {

            //
            // Upper triangular matrix
            //
            if (optype == 0)
            {

                //
                // A^(-1)*X
                //
                for (i = m - 1; i >= 0; i--)
                {
                    for (j = i + 1; j <= m - 1; j++)
                    {
                        vr = a[i1 + i, j1 + j];
                        for (i_ = j2; i_ <= j2 + n - 1; i_++)
                        {
                            x[i2 + i, i_] = x[i2 + i, i_] - vr * x[i2 + j, i_];
                        }
                    }
                    if (!isunit)
                    {
                        vd = 1 / a[i1 + i, j1 + i];
                        for (i_ = j2; i_ <= j2 + n - 1; i_++)
                        {
                            x[i2 + i, i_] = vd * x[i2 + i, i_];
                        }
                    }
                }
                return;
            }
            if (optype == 1)
            {

                //
                // A^(-T)*X
                //
                for (i = 0; i <= m - 1; i++)
                {
                    if (isunit)
                    {
                        vd = 1;
                    }
                    else
                    {
                        vd = 1 / a[i1 + i, j1 + i];
                    }
                    for (i_ = j2; i_ <= j2 + n - 1; i_++)
                    {
                        x[i2 + i, i_] = vd * x[i2 + i, i_];
                    }
                    for (j = i + 1; j <= m - 1; j++)
                    {
                        vr = a[i1 + i, j1 + j];
                        for (i_ = j2; i_ <= j2 + n - 1; i_++)
                        {
                            x[i2 + j, i_] = x[i2 + j, i_] - vr * x[i2 + i, i_];
                        }
                    }
                }
                return;
            }
        }
        else
        {

            //
            // Lower triangular matrix
            //
            if (optype == 0)
            {

                //
                // A^(-1)*X
                //
                for (i = 0; i <= m - 1; i++)
                {
                    for (j = 0; j <= i - 1; j++)
                    {
                        vr = a[i1 + i, j1 + j];
                        for (i_ = j2; i_ <= j2 + n - 1; i_++)
                        {
                            x[i2 + i, i_] = x[i2 + i, i_] - vr * x[i2 + j, i_];
                        }
                    }
                    if (isunit)
                    {
                        vd = 1;
                    }
                    else
                    {
                        vd = 1 / a[i1 + j, j1 + j];
                    }
                    for (i_ = j2; i_ <= j2 + n - 1; i_++)
                    {
                        x[i2 + i, i_] = vd * x[i2 + i, i_];
                    }
                }
                return;
            }
            if (optype == 1)
            {

                //
                // A^(-T)*X
                //
                for (i = m - 1; i >= 0; i--)
                {
                    if (isunit)
                    {
                        vd = 1;
                    }
                    else
                    {
                        vd = 1 / a[i1 + i, j1 + i];
                    }
                    for (i_ = j2; i_ <= j2 + n - 1; i_++)
                    {
                        x[i2 + i, i_] = vd * x[i2 + i, i_];
                    }
                    for (j = i - 1; j >= 0; j--)
                    {
                        vr = a[i1 + i, j1 + j];
                        for (i_ = j2; i_ <= j2 + n - 1; i_++)
                        {
                            x[i2 + j, i_] = x[i2 + j, i_] - vr * x[i2 + i, i_];
                        }
                    }
                }
                return;
            }
        }
    }


    /*************************************************************************
    Level 2 subroutine
    *************************************************************************/
    private static void cmatrixherk2(int n,
        int k,
        double alpha,
        complex[,] a,
        int ia,
        int ja,
        int optypea,
        double beta,
        complex[,] c,
        int ic,
        int jc,
        bool isupper,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int j1 = 0;
        int j2 = 0;
        complex v = 0;
        int i_ = 0;
        int i1_ = 0;


        //
        // Fast exit (nothing to be done)
        //
        if (((double)(alpha) == (double)(0) || k == 0) && (double)(beta) == (double)(1))
        {
            return;
        }

        //
        // Try to call fast SYRK
        //
        if (ablasf.cmatrixherkf(n, k, alpha, a, ia, ja, optypea, beta, c, ic, jc, isupper, _params))
        {
            return;
        }

        //
        // SYRK
        //
        if (optypea == 0)
        {

            //
            // C=alpha*A*A^H+beta*C
            //
            for (i = 0; i <= n - 1; i++)
            {
                if (isupper)
                {
                    j1 = i;
                    j2 = n - 1;
                }
                else
                {
                    j1 = 0;
                    j2 = i;
                }
                for (j = j1; j <= j2; j++)
                {
                    if ((double)(alpha) != (double)(0) && k > 0)
                    {
                        v = 0.0;
                        for (i_ = ja; i_ <= ja + k - 1; i_++)
                        {
                            v += a[ia + i, i_] * math.conj(a[ia + j, i_]);
                        }
                    }
                    else
                    {
                        v = 0;
                    }
                    if ((double)(beta) == (double)(0))
                    {
                        c[ic + i, jc + j] = alpha * v;
                    }
                    else
                    {
                        c[ic + i, jc + j] = beta * c[ic + i, jc + j] + alpha * v;
                    }
                }
            }
            return;
        }
        else
        {

            //
            // C=alpha*A^H*A+beta*C
            //
            for (i = 0; i <= n - 1; i++)
            {
                if (isupper)
                {
                    j1 = i;
                    j2 = n - 1;
                }
                else
                {
                    j1 = 0;
                    j2 = i;
                }
                if ((double)(beta) == (double)(0))
                {
                    for (j = j1; j <= j2; j++)
                    {
                        c[ic + i, jc + j] = 0;
                    }
                }
                else
                {
                    for (i_ = jc + j1; i_ <= jc + j2; i_++)
                    {
                        c[ic + i, i_] = beta * c[ic + i, i_];
                    }
                }
            }
            if ((double)(alpha) != (double)(0) && k > 0)
            {
                for (i = 0; i <= k - 1; i++)
                {
                    for (j = 0; j <= n - 1; j++)
                    {
                        if (isupper)
                        {
                            j1 = j;
                            j2 = n - 1;
                        }
                        else
                        {
                            j1 = 0;
                            j2 = j;
                        }
                        v = alpha * math.conj(a[ia + i, ja + j]);
                        i1_ = (ja + j1) - (jc + j1);
                        for (i_ = jc + j1; i_ <= jc + j2; i_++)
                        {
                            c[ic + j, i_] = c[ic + j, i_] + v * a[ia + i, i_ + i1_];
                        }
                    }
                }
            }
            return;
        }
    }


    /*************************************************************************
    Level 2 subrotuine
    *************************************************************************/
    private static void rmatrixsyrk2(int n,
        int k,
        double alpha,
        double[,] a,
        int ia,
        int ja,
        int optypea,
        double beta,
        double[,] c,
        int ic,
        int jc,
        bool isupper,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int j1 = 0;
        int j2 = 0;
        double v = 0;
        int i_ = 0;
        int i1_ = 0;


        //
        // Fast exit (nothing to be done)
        //
        if (((double)(alpha) == (double)(0) || k == 0) && (double)(beta) == (double)(1))
        {
            return;
        }

        //
        // Try to call fast SYRK
        //
        if (ablasf.rmatrixsyrkf(n, k, alpha, a, ia, ja, optypea, beta, c, ic, jc, isupper, _params))
        {
            return;
        }

        //
        // SYRK
        //
        if (optypea == 0)
        {

            //
            // C=alpha*A*A^H+beta*C
            //
            for (i = 0; i <= n - 1; i++)
            {
                if (isupper)
                {
                    j1 = i;
                    j2 = n - 1;
                }
                else
                {
                    j1 = 0;
                    j2 = i;
                }
                for (j = j1; j <= j2; j++)
                {
                    if ((double)(alpha) != (double)(0) && k > 0)
                    {
                        v = 0.0;
                        for (i_ = ja; i_ <= ja + k - 1; i_++)
                        {
                            v += a[ia + i, i_] * a[ia + j, i_];
                        }
                    }
                    else
                    {
                        v = 0;
                    }
                    if ((double)(beta) == (double)(0))
                    {
                        c[ic + i, jc + j] = alpha * v;
                    }
                    else
                    {
                        c[ic + i, jc + j] = beta * c[ic + i, jc + j] + alpha * v;
                    }
                }
            }
            return;
        }
        else
        {

            //
            // C=alpha*A^H*A+beta*C
            //
            for (i = 0; i <= n - 1; i++)
            {
                if (isupper)
                {
                    j1 = i;
                    j2 = n - 1;
                }
                else
                {
                    j1 = 0;
                    j2 = i;
                }
                if ((double)(beta) == (double)(0))
                {
                    for (j = j1; j <= j2; j++)
                    {
                        c[ic + i, jc + j] = 0;
                    }
                }
                else
                {
                    for (i_ = jc + j1; i_ <= jc + j2; i_++)
                    {
                        c[ic + i, i_] = beta * c[ic + i, i_];
                    }
                }
            }
            if ((double)(alpha) != (double)(0) && k > 0)
            {
                for (i = 0; i <= k - 1; i++)
                {
                    for (j = 0; j <= n - 1; j++)
                    {
                        if (isupper)
                        {
                            j1 = j;
                            j2 = n - 1;
                        }
                        else
                        {
                            j1 = 0;
                            j2 = j;
                        }
                        v = alpha * a[ia + i, ja + j];
                        i1_ = (ja + j1) - (jc + j1);
                        for (i_ = jc + j1; i_ <= jc + j2; i_++)
                        {
                            c[ic + j, i_] = c[ic + j, i_] + v * a[ia + i, i_ + i1_];
                        }
                    }
                }
            }
            return;
        }
    }


    /*************************************************************************
    This subroutine is an actual implementation of CMatrixGEMM.  It  does  not
    perform some integrity checks performed in the  driver  function,  and  it
    does not activate multithreading  framework  (driver  decides  whether  to
    activate workers or not).

      -- ALGLIB routine --
         10.01.2019
         Bochkanov Sergey
    *************************************************************************/
    private static void cmatrixgemmrec(int m,
        int n,
        int k,
        complex alpha,
        complex[,] a,
        int ia,
        int ja,
        int optypea,
        complex[,] b,
        int ib,
        int jb,
        int optypeb,
        complex beta,
        complex[,] c,
        int ic,
        int jc,
        xparams _params)
    {
        int s1 = 0;
        int s2 = 0;
        int tsa = 0;
        int tsb = 0;
        int tscur = 0;


        //
        // Tile hierarchy: B -> A -> A/2
        //
        tsa = apserv.matrixtilesizea(_params) / 2;
        tsb = apserv.matrixtilesizeb(_params);
        tscur = tsb;
        if (apserv.imax3(m, n, k, _params) <= tsb)
        {
            tscur = tsa;
        }
        ap.assert(tscur >= 1, "CMatrixGEMMRec: integrity check failed");

        //
        // Use MKL or ALGLIB basecase code
        //
        if (apserv.imax3(m, n, k, _params) <= tsb)
        {
            if (ablasmkl.cmatrixgemmmkl(m, n, k, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc, _params))
            {
                return;
            }
        }
        if (apserv.imax3(m, n, k, _params) <= tsa)
        {
            ablasf.cmatrixgemmk(m, n, k, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc, _params);
            return;
        }

        //
        // Recursive algorithm: parallel splitting on M/N
        //
        if (m >= n && m >= k)
        {

            //
            // A*B = (A1 A2)^T*B
            //
            apserv.tiledsplit(m, tscur, ref s1, ref s2, _params);
            cmatrixgemmrec(s1, n, k, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc, _params);
            if (optypea == 0)
            {
                cmatrixgemmrec(s2, n, k, alpha, a, ia + s1, ja, optypea, b, ib, jb, optypeb, beta, c, ic + s1, jc, _params);
            }
            else
            {
                cmatrixgemmrec(s2, n, k, alpha, a, ia, ja + s1, optypea, b, ib, jb, optypeb, beta, c, ic + s1, jc, _params);
            }
            return;
        }
        if (n >= m && n >= k)
        {

            //
            // A*B = A*(B1 B2)
            //
            apserv.tiledsplit(n, tscur, ref s1, ref s2, _params);
            if (optypeb == 0)
            {
                cmatrixgemmrec(m, s1, k, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc, _params);
                cmatrixgemmrec(m, s2, k, alpha, a, ia, ja, optypea, b, ib, jb + s1, optypeb, beta, c, ic, jc + s1, _params);
            }
            else
            {
                cmatrixgemmrec(m, s1, k, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc, _params);
                cmatrixgemmrec(m, s2, k, alpha, a, ia, ja, optypea, b, ib + s1, jb, optypeb, beta, c, ic, jc + s1, _params);
            }
            return;
        }

        //
        // Recursive algorithm: serial splitting on K
        //

        //
        // A*B = (A1 A2)*(B1 B2)^T
        //
        apserv.tiledsplit(k, tscur, ref s1, ref s2, _params);
        if (optypea == 0 && optypeb == 0)
        {
            cmatrixgemmrec(m, n, s1, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc, _params);
            cmatrixgemmrec(m, n, s2, alpha, a, ia, ja + s1, optypea, b, ib + s1, jb, optypeb, 1.0, c, ic, jc, _params);
        }
        if (optypea == 0 && optypeb != 0)
        {
            cmatrixgemmrec(m, n, s1, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc, _params);
            cmatrixgemmrec(m, n, s2, alpha, a, ia, ja + s1, optypea, b, ib, jb + s1, optypeb, 1.0, c, ic, jc, _params);
        }
        if (optypea != 0 && optypeb == 0)
        {
            cmatrixgemmrec(m, n, s1, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc, _params);
            cmatrixgemmrec(m, n, s2, alpha, a, ia + s1, ja, optypea, b, ib + s1, jb, optypeb, 1.0, c, ic, jc, _params);
        }
        if (optypea != 0 && optypeb != 0)
        {
            cmatrixgemmrec(m, n, s1, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc, _params);
            cmatrixgemmrec(m, n, s2, alpha, a, ia + s1, ja, optypea, b, ib, jb + s1, optypeb, 1.0, c, ic, jc, _params);
        }
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_cmatrixgemmrec(int m,
        int n,
        int k,
        complex alpha,
        complex[,] a,
        int ia,
        int ja,
        int optypea,
        complex[,] b,
        int ib,
        int jb,
        int optypeb,
        complex beta,
        complex[,] c,
        int ic,
        int jc, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    This subroutine is an actual implementation of RMatrixGEMM.  It  does  not
    perform some integrity checks performed in the  driver  function,  and  it
    does not activate multithreading  framework  (driver  decides  whether  to
    activate workers or not).

      -- ALGLIB routine --
         10.01.2019
         Bochkanov Sergey
    *************************************************************************/
    private static void rmatrixgemmrec(int m,
        int n,
        int k,
        double alpha,
        double[,] a,
        int ia,
        int ja,
        int optypea,
        double[,] b,
        int ib,
        int jb,
        int optypeb,
        double beta,
        double[,] c,
        int ic,
        int jc,
        xparams _params)
    {
        int s1 = 0;
        int s2 = 0;
        int tsa = 0;
        int tsb = 0;
        int tscur = 0;

        tsa = apserv.matrixtilesizea(_params);
        tsb = apserv.matrixtilesizeb(_params);
        tscur = tsb;
        if (apserv.imax3(m, n, k, _params) <= tsb)
        {
            tscur = tsa;
        }
        ap.assert(tscur >= 1, "RMatrixGEMMRec: integrity check failed");

        //
        // Use MKL or ALGLIB basecase code
        //
        if ((m <= tsb && n <= tsb) && k <= tsb)
        {
            if (ablasmkl.rmatrixgemmmkl(m, n, k, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc, _params))
            {
                return;
            }
        }
        if ((m <= tsa && n <= tsa) && k <= tsa)
        {
            ablasf.rmatrixgemmk(m, n, k, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc, _params);
            return;
        }

        //
        // Recursive algorithm: split on M or N
        //
        if (m >= n && m >= k)
        {

            //
            // A*B = (A1 A2)^T*B
            //
            apserv.tiledsplit(m, tscur, ref s1, ref s2, _params);
            if (optypea == 0)
            {
                rmatrixgemmrec(s2, n, k, alpha, a, ia + s1, ja, optypea, b, ib, jb, optypeb, beta, c, ic + s1, jc, _params);
                rmatrixgemmrec(s1, n, k, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc, _params);
            }
            else
            {
                rmatrixgemmrec(s2, n, k, alpha, a, ia, ja + s1, optypea, b, ib, jb, optypeb, beta, c, ic + s1, jc, _params);
                rmatrixgemmrec(s1, n, k, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc, _params);
            }
            return;
        }
        if (n >= m && n >= k)
        {

            //
            // A*B = A*(B1 B2)
            //
            apserv.tiledsplit(n, tscur, ref s1, ref s2, _params);
            if (optypeb == 0)
            {
                rmatrixgemmrec(m, s2, k, alpha, a, ia, ja, optypea, b, ib, jb + s1, optypeb, beta, c, ic, jc + s1, _params);
                rmatrixgemmrec(m, s1, k, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc, _params);
            }
            else
            {
                rmatrixgemmrec(m, s2, k, alpha, a, ia, ja, optypea, b, ib + s1, jb, optypeb, beta, c, ic, jc + s1, _params);
                rmatrixgemmrec(m, s1, k, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc, _params);
            }
            return;
        }

        //
        // Recursive algorithm: split on K
        //

        //
        // A*B = (A1 A2)*(B1 B2)^T
        //
        apserv.tiledsplit(k, tscur, ref s1, ref s2, _params);
        if (optypea == 0 && optypeb == 0)
        {
            rmatrixgemmrec(m, n, s1, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc, _params);
            rmatrixgemmrec(m, n, s2, alpha, a, ia, ja + s1, optypea, b, ib + s1, jb, optypeb, 1.0, c, ic, jc, _params);
        }
        if (optypea == 0 && optypeb != 0)
        {
            rmatrixgemmrec(m, n, s1, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc, _params);
            rmatrixgemmrec(m, n, s2, alpha, a, ia, ja + s1, optypea, b, ib, jb + s1, optypeb, 1.0, c, ic, jc, _params);
        }
        if (optypea != 0 && optypeb == 0)
        {
            rmatrixgemmrec(m, n, s1, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc, _params);
            rmatrixgemmrec(m, n, s2, alpha, a, ia + s1, ja, optypea, b, ib + s1, jb, optypeb, 1.0, c, ic, jc, _params);
        }
        if (optypea != 0 && optypeb != 0)
        {
            rmatrixgemmrec(m, n, s1, alpha, a, ia, ja, optypea, b, ib, jb, optypeb, beta, c, ic, jc, _params);
            rmatrixgemmrec(m, n, s2, alpha, a, ia + s1, ja, optypea, b, ib, jb + s1, optypeb, 1.0, c, ic, jc, _params);
        }
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_rmatrixgemmrec(int m,
        int n,
        int k,
        double alpha,
        double[,] a,
        int ia,
        int ja,
        int optypea,
        double[,] b,
        int ib,
        int jb,
        int optypeb,
        double beta,
        double[,] c,
        int ic,
        int jc, xparams _params)
    {
        return false;
    }


}
