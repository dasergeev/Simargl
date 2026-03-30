#pragma warning disable CS1591

namespace Simargl.Algorithms.Raw;

public class matdet
{
    /*************************************************************************
    Determinant calculation of the matrix given by its LU decomposition.

    Input parameters:
        A       -   LU decomposition of the matrix (output of
                    RMatrixLU subroutine).
        Pivots  -   table of permutations which were made during
                    the LU decomposition.
                    Output of RMatrixLU subroutine.
        N       -   (optional) size of matrix A:
                    * if given, only principal NxN submatrix is processed and
                      overwritten. other elements are unchanged.
                    * if not given, automatically determined from matrix size
                      (A must be square matrix)

    Result: matrix determinant.

      -- ALGLIB --
         Copyright 2005 by Bochkanov Sergey
    *************************************************************************/
    public static double rmatrixludet(double[,] a,
        int[] pivots,
        int n,
        xparams _params)
    {
        double result = 0;
        int i = 0;
        int s = 0;

        ap.assert(n >= 1, "RMatrixLUDet: N<1!");
        ap.assert(ap.len(pivots) >= n, "RMatrixLUDet: Pivots array is too short!");
        ap.assert(ap.rows(a) >= n, "RMatrixLUDet: rows(A)<N!");
        ap.assert(ap.cols(a) >= n, "RMatrixLUDet: cols(A)<N!");
        ap.assert(apserv.apservisfinitematrix(a, n, n, _params), "RMatrixLUDet: A contains infinite or NaN values!");
        result = 1;
        s = 1;
        for (i = 0; i <= n - 1; i++)
        {
            result = result * a[i, i];
            if (pivots[i] != i)
            {
                s = -s;
            }
        }
        result = result * s;
        return result;
    }


    /*************************************************************************
    Calculation of the determinant of a general matrix

    Input parameters:
        A       -   matrix, array[0..N-1, 0..N-1]
        N       -   (optional) size of matrix A:
                    * if given, only principal NxN submatrix is processed and
                      overwritten. other elements are unchanged.
                    * if not given, automatically determined from matrix size
                      (A must be square matrix)

    Result: determinant of matrix A.

      -- ALGLIB --
         Copyright 2005 by Bochkanov Sergey
    *************************************************************************/
    public static double rmatrixdet(double[,] a,
        int n,
        xparams _params)
    {
        double result = 0;
        int[] pivots = new int[0];

        a = (double[,])a.Clone();

        ap.assert(n >= 1, "RMatrixDet: N<1!");
        ap.assert(ap.rows(a) >= n, "RMatrixDet: rows(A)<N!");
        ap.assert(ap.cols(a) >= n, "RMatrixDet: cols(A)<N!");
        ap.assert(apserv.apservisfinitematrix(a, n, n, _params), "RMatrixDet: A contains infinite or NaN values!");
        trfac.rmatrixlu(a, n, n, ref pivots, _params);
        result = rmatrixludet(a, pivots, n, _params);
        return result;
    }


    /*************************************************************************
    Determinant calculation of the matrix given by its LU decomposition.

    Input parameters:
        A       -   LU decomposition of the matrix (output of
                    RMatrixLU subroutine).
        Pivots  -   table of permutations which were made during
                    the LU decomposition.
                    Output of RMatrixLU subroutine.
        N       -   (optional) size of matrix A:
                    * if given, only principal NxN submatrix is processed and
                      overwritten. other elements are unchanged.
                    * if not given, automatically determined from matrix size
                      (A must be square matrix)

    Result: matrix determinant.

      -- ALGLIB --
         Copyright 2005 by Bochkanov Sergey
    *************************************************************************/
    public static complex cmatrixludet(complex[,] a,
        int[] pivots,
        int n,
        xparams _params)
    {
        complex result = 0;
        int i = 0;
        int s = 0;

        ap.assert(n >= 1, "CMatrixLUDet: N<1!");
        ap.assert(ap.len(pivots) >= n, "CMatrixLUDet: Pivots array is too short!");
        ap.assert(ap.rows(a) >= n, "CMatrixLUDet: rows(A)<N!");
        ap.assert(ap.cols(a) >= n, "CMatrixLUDet: cols(A)<N!");
        ap.assert(apserv.apservisfinitecmatrix(a, n, n, _params), "CMatrixLUDet: A contains infinite or NaN values!");
        result = 1;
        s = 1;
        for (i = 0; i <= n - 1; i++)
        {
            result = result * a[i, i];
            if (pivots[i] != i)
            {
                s = -s;
            }
        }
        result = result * s;
        return result;
    }


    /*************************************************************************
    Calculation of the determinant of a general matrix

    Input parameters:
        A       -   matrix, array[0..N-1, 0..N-1]
        N       -   (optional) size of matrix A:
                    * if given, only principal NxN submatrix is processed and
                      overwritten. other elements are unchanged.
                    * if not given, automatically determined from matrix size
                      (A must be square matrix)

    Result: determinant of matrix A.

      -- ALGLIB --
         Copyright 2005 by Bochkanov Sergey
    *************************************************************************/
    public static complex cmatrixdet(complex[,] a,
        int n,
        xparams _params)
    {
        complex result = 0;
        int[] pivots = new int[0];

        a = (complex[,])a.Clone();

        ap.assert(n >= 1, "CMatrixDet: N<1!");
        ap.assert(ap.rows(a) >= n, "CMatrixDet: rows(A)<N!");
        ap.assert(ap.cols(a) >= n, "CMatrixDet: cols(A)<N!");
        ap.assert(apserv.apservisfinitecmatrix(a, n, n, _params), "CMatrixDet: A contains infinite or NaN values!");
        trfac.cmatrixlu(a, n, n, ref pivots, _params);
        result = cmatrixludet(a, pivots, n, _params);
        return result;
    }


    /*************************************************************************
    Determinant calculation of the matrix given by the Cholesky decomposition.

    Input parameters:
        A       -   Cholesky decomposition,
                    output of SMatrixCholesky subroutine.
        N       -   (optional) size of matrix A:
                    * if given, only principal NxN submatrix is processed and
                      overwritten. other elements are unchanged.
                    * if not given, automatically determined from matrix size
                      (A must be square matrix)

    As the determinant is equal to the product of squares of diagonal elements,
    it's not necessary to specify which triangle - lower or upper - the matrix
    is stored in.

    Result:
        matrix determinant.

      -- ALGLIB --
         Copyright 2005-2008 by Bochkanov Sergey
    *************************************************************************/
    public static double spdmatrixcholeskydet(double[,] a,
        int n,
        xparams _params)
    {
        double result = 0;
        int i = 0;
        bool f = new bool();

        ap.assert(n >= 1, "SPDMatrixCholeskyDet: N<1!");
        ap.assert(ap.rows(a) >= n, "SPDMatrixCholeskyDet: rows(A)<N!");
        ap.assert(ap.cols(a) >= n, "SPDMatrixCholeskyDet: cols(A)<N!");
        f = true;
        for (i = 0; i <= n - 1; i++)
        {
            f = f && math.isfinite(a[i, i]);
        }
        ap.assert(f, "SPDMatrixCholeskyDet: A contains infinite or NaN values!");
        result = 1;
        for (i = 0; i <= n - 1; i++)
        {
            result = result * math.sqr(a[i, i]);
        }
        return result;
    }


    /*************************************************************************
    Determinant calculation of the symmetric positive definite matrix.

    Input parameters:
        A       -   matrix, array[N,N]
        N       -   (optional) size of matrix A:
                    * if given, only principal NxN submatrix is processed and
                      overwritten. other elements are unchanged.
                    * if not given, automatically determined from matrix size
                      (A must be square matrix)
        IsUpper -   storage type:
                    * if True, symmetric matrix  A  is  given  by  its  upper
                      triangle, and the lower triangle isn't used/changed  by
                      function
                    * if False, symmetric matrix  A  is  given  by  its lower
                      triangle, and the upper triangle isn't used/changed  by
                      function

    Result:
        determinant of matrix A.
        If matrix A is not positive definite, an exception is generated.

      -- ALGLIB --
         Copyright 2005-2008 by Bochkanov Sergey
    *************************************************************************/
    public static double spdmatrixdet(double[,] a,
        int n,
        bool isupper,
        xparams _params)
    {
        double result = 0;
        bool b = new bool();

        a = (double[,])a.Clone();

        ap.assert(n >= 1, "SPDMatrixDet: N<1!");
        ap.assert(ap.rows(a) >= n, "SPDMatrixDet: rows(A)<N!");
        ap.assert(ap.cols(a) >= n, "SPDMatrixDet: cols(A)<N!");
        ap.assert(apserv.isfinitertrmatrix(a, n, isupper, _params), "SPDMatrixDet: A contains infinite or NaN values!");
        b = trfac.spdmatrixcholesky(a, n, isupper, _params);
        ap.assert(b, "SPDMatrixDet: A is not SPD!");
        result = spdmatrixcholeskydet(a, n, _params);
        return result;
    }


}
