#pragma warning disable CS1591

namespace Simargl.Algorithms.Raw;

public class schur
{
    /*************************************************************************
    Subroutine performing the Schur decomposition of a general matrix by using
    the QR algorithm with multiple shifts.

    COMMERCIAL EDITION OF ALGLIB:

      ! Commercial version of ALGLIB includes one  important  improvement   of
      ! this function, which can be used from C++ and C#:
      ! * Intel MKL support (lightweight Intel MKL is shipped with ALGLIB)
      !
      ! Intel MKL gives approximately constant  (with  respect  to  number  of
      ! worker threads) acceleration factor which depends on CPU  being  used,
      ! problem  size  and  "baseline"  ALGLIB  edition  which  is  used   for
      ! comparison.
      !
      ! Multithreaded acceleration is NOT supported for this function.
      !
      ! We recommend you to read 'Working with commercial version' section  of
      ! ALGLIB Reference Manual in order to find out how to  use  performance-
      ! related features provided by commercial edition of ALGLIB.

    The source matrix A is represented as S'*A*S = T, where S is an orthogonal
    matrix (Schur vectors), T - upper quasi-triangular matrix (with blocks of
    sizes 1x1 and 2x2 on the main diagonal).

    Input parameters:
        A   -   matrix to be decomposed.
                Array whose indexes range within [0..N-1, 0..N-1].
        N   -   size of A, N>=0.


    Output parameters:
        A   -   contains matrix T.
                Array whose indexes range within [0..N-1, 0..N-1].
        S   -   contains Schur vectors.
                Array whose indexes range within [0..N-1, 0..N-1].

    Note 1:
        The block structure of matrix T can be easily recognized: since all
        the elements below the blocks are zeros, the elements a[i+1,i] which
        are equal to 0 show the block border.

    Note 2:
        The algorithm performance depends on the value of the internal parameter
        NS of the InternalSchurDecomposition subroutine which defines the number
        of shifts in the QR algorithm (similarly to the block width in block-matrix
        algorithms in linear algebra). If you require maximum performance on
        your machine, it is recommended to adjust this parameter manually.

    Result:
        True,
            if the algorithm has converged and parameters A and S contain the result.
        False,
            if the algorithm has not converged.

    Algorithm implemented on the basis of the DHSEQR subroutine (LAPACK 3.0 library).
    *************************************************************************/
    public static bool rmatrixschur(double[,] a,
        int n,
        ref double[,] s,
        xparams _params)
    {
        bool result = new bool();
        double[] tau = new double[0];
        double[] wi = new double[0];
        double[] wr = new double[0];
        int info = 0;

        s = new double[0, 0];


        //
        // Upper Hessenberg form of the 0-based matrix
        //
        ortfac.rmatrixhessenberg(a, n, ref tau, _params);
        ortfac.rmatrixhessenbergunpackq(a, n, tau, ref s, _params);

        //
        // Schur decomposition
        //
        hsschur.rmatrixinternalschurdecomposition(a, n, 1, 1, ref wr, ref wi, ref s, ref info, _params);
        result = info == 0;
        return result;
    }


}
