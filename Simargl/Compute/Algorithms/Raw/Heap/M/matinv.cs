#pragma warning disable CS3008
#pragma warning disable CS1591

namespace Simargl.Algorithms.Raw;

public class matinv
{
    /*************************************************************************
    Matrix inverse report:
    * terminationtype   completion code:
                        *  1 for success
                        * -3 for failure due to the matrix being singular or
                             nearly-singular
    * r1                reciprocal of condition number in 1-norm
    * rinf              reciprocal of condition number in inf-norm
    *************************************************************************/
    public class matinvreport : apobject
    {
        public int terminationtype;
        public double r1;
        public double rinf;
        public matinvreport()
        {
            init();
        }
        public override void init()
        {
        }
        public override apobject make_copy()
        {
            matinvreport _result = new matinvreport();
            _result.terminationtype = terminationtype;
            _result.r1 = r1;
            _result.rinf = rinf;
            return _result;
        }
    };




    /*************************************************************************
    Inversion of a matrix given by its LU decomposition.

    INPUT PARAMETERS:
        A       -   LU decomposition of the matrix
                    (output of RMatrixLU subroutine).
        Pivots  -   table of permutations
                    (the output of RMatrixLU subroutine).
        N       -   size of the matrix A (optional):
                    * if given, only principal NxN submatrix is processed  and
                      overwritten. Trailing elements are unchanged.
                    * if not given, the size  is automatically determined from
                      the matrix size (A must be a square matrix)

    OUTPUT PARAMETERS:
        A       -   inverse of matrix A, array[N,N]:
                    * for rep.terminationtype>0, contains matrix inverse
                    * for rep.terminationtype<0, zero-filled
        Rep     -   solver report:
                    * rep.terminationtype>0 for success, <0 for failure
                    * see below for more info

    SOLVER REPORT

    Subroutine sets following fields of the Rep structure:
    * terminationtype   completion code:
                        *  1 for success 
                        * -3 for a singular or extremely ill-conditioned matrix
    * r1                reciprocal of condition number: 1/cond(A), 1-norm.
    * rinf              reciprocal of condition number: 1/cond(A), inf-norm.

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
         05.02.2010
         Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixluinverse(double[,] a,
        int[] pivots,
        int n,
        matinvreport rep,
        xparams _params)
    {
        double[] work = new double[0];
        int i = 0;
        int j = 0;
        int k = 0;
        double v = 0;

        ap.assert(n > 0, "RMatrixLUInverse: N<=0!");
        ap.assert(ap.cols(a) >= n, "RMatrixLUInverse: cols(A)<N!");
        ap.assert(ap.rows(a) >= n, "RMatrixLUInverse: rows(A)<N!");
        ap.assert(ap.len(pivots) >= n, "RMatrixLUInverse: len(Pivots)<N!");
        ap.assert(apserv.apservisfinitematrix(a, n, n, _params), "RMatrixLUInverse: A contains infinite or NaN values!");
        for (i = 0; i <= n - 1; i++)
        {
            if (pivots[i] > n - 1 || pivots[i] < 0)
            {
                ap.assert(false, "RMatrixLUInverse: incorrect Pivots array!");
            }
        }

        //
        // calculate condition numbers
        //
        rep.terminationtype = 1;
        rep.r1 = rcond.rmatrixlurcond1(a, n, _params);
        rep.rinf = rcond.rmatrixlurcondinf(a, n, _params);
        if ((double)(rep.r1) < (double)(rcond.rcondthreshold(_params)) || (double)(rep.rinf) < (double)(rcond.rcondthreshold(_params)))
        {
            for (i = 0; i <= n - 1; i++)
            {
                for (j = 0; j <= n - 1; j++)
                {
                    a[i, j] = 0;
                }
            }
            rep.terminationtype = -3;
            rep.r1 = 0;
            rep.rinf = 0;
            return;
        }

        //
        // Call cache-oblivious code
        //
        work = new double[n];
        rmatrixluinverserec(a, 0, n, work, rep, _params);

        //
        // apply permutations
        //
        for (i = 0; i <= n - 1; i++)
        {
            for (j = n - 2; j >= 0; j--)
            {
                k = pivots[j];
                v = a[i, j];
                a[i, j] = a[i, k];
                a[i, k] = v;
            }
        }
    }


    /*************************************************************************
    Inversion of a general matrix.

    INPUT PARAMETERS:
        A       -   matrix.
        N       -   size of the matrix A (optional):
                    * if given, only principal NxN submatrix is processed  and
                      overwritten. Trailing elements are unchanged.
                    * if not given, the size  is automatically determined from
                      the matrix size (A must be a square matrix)

    OUTPUT PARAMETERS:
        A       -   inverse of matrix A, array[N,N]:
                    * for rep.terminationtype>0, contains matrix inverse
                    * for rep.terminationtype<0, zero-filled
        Rep     -   solver report:
                    * rep.terminationtype>0 for success, <0 for failure
                    * see below for more info

    SOLVER REPORT

    Subroutine sets following fields of the Rep structure:
    * terminationtype   completion code:
                        *  1 for success 
                        * -3 for a singular or extremely ill-conditioned matrix
    * r1                reciprocal of condition number: 1/cond(A), 1-norm.
    * rinf              reciprocal of condition number: 1/cond(A), inf-norm.

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
         Copyright 2005-2010 by Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixinverse(double[,] a,
        int n,
        matinvreport rep,
        xparams _params)
    {
        int[] pivots = new int[0];

        ap.assert(n > 0, "RMatrixInverse: N<=0!");
        ap.assert(ap.cols(a) >= n, "RMatrixInverse: cols(A)<N!");
        ap.assert(ap.rows(a) >= n, "RMatrixInverse: rows(A)<N!");
        ap.assert(apserv.apservisfinitematrix(a, n, n, _params), "RMatrixInverse: A contains infinite or NaN values!");
        trfac.rmatrixlu(a, n, n, ref pivots, _params);
        rmatrixluinverse(a, pivots, n, rep, _params);
    }


    /*************************************************************************
    Inversion of a matrix given by its LU decomposition.

    INPUT PARAMETERS:
        A       -   LU decomposition of the matrix
                    (output of CMatrixLU subroutine).
        Pivots  -   table of permutations
                    (the output of CMatrixLU subroutine).
        N       -   size of the matrix A (optional):
                    * if given, only principal NxN submatrix is processed  and
                      overwritten. Trailing elements are unchanged.
                    * if not given, the size  is automatically determined from
                      the matrix size (A must be a square matrix)

    OUTPUT PARAMETERS:
        A       -   inverse of matrix A, array[N,N]:
                    * for rep.terminationtype>0, contains matrix inverse
                    * for rep.terminationtype<0, zero-filled
        Rep     -   solver report:
                    * rep.terminationtype>0 for success, <0 for failure
                    * see below for more info

    SOLVER REPORT

    Subroutine sets following fields of the Rep structure:
    * terminationtype   completion code:
                        *  1 for success 
                        * -3 for a singular or extremely ill-conditioned matrix
    * r1                reciprocal of condition number: 1/cond(A), 1-norm.
    * rinf              reciprocal of condition number: 1/cond(A), inf-norm.

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
         05.02.2010
         Bochkanov Sergey
    *************************************************************************/
    public static void cmatrixluinverse(complex[,] a,
        int[] pivots,
        int n,
        matinvreport rep,
        xparams _params)
    {
        complex[] work = new complex[0];
        int i = 0;
        int j = 0;
        int k = 0;
        complex v = 0;

        ap.assert(n > 0, "CMatrixLUInverse: N<=0!");
        ap.assert(ap.cols(a) >= n, "CMatrixLUInverse: cols(A)<N!");
        ap.assert(ap.rows(a) >= n, "CMatrixLUInverse: rows(A)<N!");
        ap.assert(ap.len(pivots) >= n, "CMatrixLUInverse: len(Pivots)<N!");
        ap.assert(apserv.apservisfinitecmatrix(a, n, n, _params), "CMatrixLUInverse: A contains infinite or NaN values!");
        for (i = 0; i <= n - 1; i++)
        {
            if (pivots[i] > n - 1 || pivots[i] < 0)
            {
                ap.assert(false, "CMatrixLUInverse: incorrect Pivots array!");
            }
        }

        //
        // calculate condition numbers
        //
        rep.terminationtype = 1;
        rep.r1 = rcond.cmatrixlurcond1(a, n, _params);
        rep.rinf = rcond.cmatrixlurcondinf(a, n, _params);
        if ((double)(rep.r1) < (double)(rcond.rcondthreshold(_params)) || (double)(rep.rinf) < (double)(rcond.rcondthreshold(_params)))
        {
            for (i = 0; i <= n - 1; i++)
            {
                for (j = 0; j <= n - 1; j++)
                {
                    a[i, j] = 0;
                }
            }
            rep.r1 = 0;
            rep.rinf = 0;
            rep.terminationtype = -3;
            return;
        }

        //
        // Call cache-oblivious code
        //
        work = new complex[n];
        cmatrixluinverserec(a, 0, n, work, rep, _params);

        //
        // apply permutations
        //
        for (i = 0; i <= n - 1; i++)
        {
            for (j = n - 2; j >= 0; j--)
            {
                k = pivots[j];
                v = a[i, j];
                a[i, j] = a[i, k];
                a[i, k] = v;
            }
        }
    }


    /*************************************************************************
    Inversion of a general matrix.

    Input parameters:
        A       -   matrix
        N       -   size of the matrix A (optional):
                    * if given, only principal NxN submatrix is processed  and
                      overwritten. Trailing elements are unchanged.
                    * if not given, the size  is automatically determined from
                      the matrix size (A must be a square matrix)

    Output parameters:
        A       -   inverse of matrix A, array[N,N]:
                    * for rep.terminationtype>0, contains matrix inverse
                    * for rep.terminationtype<0, zero-filled
        Rep     -   solver report:
                    * rep.terminationtype>0 for success, <0 for failure
                    * see below for more info

    SOLVER REPORT

    Subroutine sets following fields of the Rep structure:
    * terminationtype   completion code:
                        *  1 for success 
                        * -3 for a singular or extremely ill-conditioned matrix
    * r1                reciprocal of condition number: 1/cond(A), 1-norm.
    * rinf              reciprocal of condition number: 1/cond(A), inf-norm.

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
         Copyright 2005 by Bochkanov Sergey
    *************************************************************************/
    public static void cmatrixinverse(complex[,] a,
        int n,
        matinvreport rep,
        xparams _params)
    {
        int[] pivots = new int[0];

        ap.assert(n > 0, "CRMatrixInverse: N<=0!");
        ap.assert(ap.cols(a) >= n, "CRMatrixInverse: cols(A)<N!");
        ap.assert(ap.rows(a) >= n, "CRMatrixInverse: rows(A)<N!");
        ap.assert(apserv.apservisfinitecmatrix(a, n, n, _params), "CMatrixInverse: A contains infinite or NaN values!");
        trfac.cmatrixlu(a, n, n, ref pivots, _params);
        cmatrixluinverse(a, pivots, n, rep, _params);
    }


    /*************************************************************************
    Inversion of a symmetric positive definite matrix which is given
    by Cholesky decomposition.

    INPUT PARAMETERS:
        A       -   Cholesky decomposition of the matrix to be inverted:
                    A=U'*U or A = L*L'.
                    Output of  SPDMatrixCholesky subroutine.
        N       -   size of the matrix A (optional):
                    * if given, only principal NxN submatrix is processed  and
                      overwritten. Trailing elements are unchanged.
                    * if not given, the size  is automatically determined from
                      the matrix size (A must be a square matrix)
        IsUpper -   storage type:
                    * if True, the symmetric  matrix  A  is given by its upper
                      triangle, and the lower triangle isn't  used/changed  by
                      the function
                    * if False, the symmetric matrix  A  is given by its lower
                      triangle, and the  upper triangle isn't used/changed  by
                      the function

    OUTPUT PARAMETERS:
        A       -   inverse of matrix A, array[N,N]:
                    * for rep.terminationtype>0,   corresponding      triangle
                      contains inverse matrix,   the  other  triangle  is  not
                      modified.
                    * for rep.terminationtype<0,  corresponding  triangle   is
                      zero-filled; the other triangle is not modified.
        Rep     -   solver report:
                    * rep.terminationtype>0 for success, <0 for failure
                    * see below for more info

    SOLVER REPORT

    Subroutine sets following fields of the Rep structure:
    * terminationtype   completion code:
                        *  1 for success 
                        * -3 for a singular or extremely ill-conditioned matrix
    * r1                reciprocal of condition number: 1/cond(A), 1-norm.
    * rinf              reciprocal of condition number: 1/cond(A), inf-norm.

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
         10.02.2010
         Bochkanov Sergey
    *************************************************************************/
    public static void spdmatrixcholeskyinverse(double[,] a,
        int n,
        bool isupper,
        matinvreport rep,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double[] tmp = new double[0];

        ap.assert(n > 0, "SPDMatrixCholeskyInverse: N<=0!");
        ap.assert(ap.cols(a) >= n, "SPDMatrixCholeskyInverse: cols(A)<N!");
        ap.assert(ap.rows(a) >= n, "SPDMatrixCholeskyInverse: rows(A)<N!");
        ap.assert(apserv.isfinitertrmatrix(a, n, isupper, _params), "SPDMatrixCholeskyInverse: A contains infinite or NaN values!");

        //
        // calculate condition numbers
        //
        rep.terminationtype = 1;
        rep.r1 = rcond.spdmatrixcholeskyrcond(a, n, isupper, _params);
        rep.rinf = rep.r1;
        if ((double)(rep.r1) < (double)(rcond.rcondthreshold(_params)) || (double)(rep.rinf) < (double)(rcond.rcondthreshold(_params)))
        {
            if (isupper)
            {
                for (i = 0; i <= n - 1; i++)
                {
                    for (j = i; j <= n - 1; j++)
                    {
                        a[i, j] = 0;
                    }
                }
            }
            else
            {
                for (i = 0; i <= n - 1; i++)
                {
                    for (j = 0; j <= i; j++)
                    {
                        a[i, j] = 0;
                    }
                }
            }
            rep.r1 = 0;
            rep.rinf = 0;
            rep.terminationtype = -3;
            return;
        }

        //
        // Inverse
        //
        tmp = new double[n];
        spdmatrixcholeskyinverserec(a, 0, n, isupper, tmp, rep, _params);
    }


    /*************************************************************************
    Inversion of a symmetric positive definite matrix.

    Given an upper or lower triangle of a symmetric positive definite matrix,
    the algorithm generates matrix A^-1 and saves the upper or lower triangle
    depending on the input.

    INPUT PARAMETERS:
        A       -   matrix to be inverted (upper or lower triangle), array[N,N]
        N       -   size of the matrix A (optional):
                    * if given, only principal NxN submatrix is processed  and
                      overwritten. Trailing elements are unchanged.
                    * if not given, the size  is automatically determined from
                      the matrix size (A must be a square matrix)
        IsUpper -   storage type:
                    * if True, symmetric  matrix  A  is  given  by  its  upper
                      triangle, and the lower triangle isn't  used/changed  by
                      function
                    * if False,  symmetric matrix  A  is  given  by  its lower
                      triangle, and the  upper triangle isn't used/changed  by
                      function

    OUTPUT PARAMETERS:
        A       -   inverse of matrix A, array[N,N]:
                    * for rep.terminationtype>0, contains matrix inverse
                    * for rep.terminationtype<0, zero-filled
        Rep     -   solver report:
                    * rep.terminationtype>0 for success, <0 for failure
                    * see below for more info

    SOLVER REPORT

    Subroutine sets following fields of the Rep structure:
    * terminationtype   completion code:
                        *  1 for success 
                        * -3 for a singular or extremely ill-conditioned matrix
    * r1                reciprocal of condition number: 1/cond(A), 1-norm.
    * rinf              reciprocal of condition number: 1/cond(A), inf-norm.

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
         10.02.2010
         Bochkanov Sergey
    *************************************************************************/
    public static void spdmatrixinverse(double[,] a,
        int n,
        bool isupper,
        matinvreport rep,
        xparams _params)
    {
        ap.assert(n > 0, "SPDMatrixInverse: N<=0!");
        ap.assert(ap.cols(a) >= n, "SPDMatrixInverse: cols(A)<N!");
        ap.assert(ap.rows(a) >= n, "SPDMatrixInverse: rows(A)<N!");
        ap.assert(apserv.isfinitertrmatrix(a, n, isupper, _params), "SPDMatrixInverse: A contains infinite or NaN values!");
        rep.r1 = 0;
        rep.rinf = 0;
        rep.terminationtype = -3;
        if (!trfac.spdmatrixcholesky(a, n, isupper, _params))
        {
            return;
        }
        spdmatrixcholeskyinverse(a, n, isupper, rep, _params);
    }


    /*************************************************************************
    Inversion of a Hermitian positive definite matrix which is given
    by Cholesky decomposition.

    Input parameters:
        A       -   Cholesky decomposition of the matrix to be inverted:
                    A=U'*U or A = L*L'.
                    Output of  HPDMatrixCholesky subroutine.
        N       -   size of the matrix A (optional):
                    * if given, only principal NxN submatrix is processed  and
                      overwritten. Trailing elements are unchanged.
                    * if not given, the size  is automatically determined from
                      the matrix size (A must be a square matrix)
        IsUpper -   storage type:
                    * if True, symmetric  matrix  A  is  given  by  its  upper
                      triangle, and the lower triangle isn't  used/changed  by
                      function
                    * if False,  symmetric matrix  A  is  given  by  its lower
                      triangle, and the  upper triangle isn't used/changed  by
                      function

    OUTPUT PARAMETERS:
        A       -   inverse of matrix A, array[N,N]:
                    * for rep.terminationtype>0, contains matrix inverse
                    * for rep.terminationtype<0, zero-filled
        Rep     -   solver report:
                    * rep.terminationtype>0 for success, <0 for failure
                    * see below for more info

    SOLVER REPORT

    Subroutine sets following fields of the Rep structure:
    * terminationtype   completion code:
                        *  1 for success 
                        * -3 for a singular or extremely ill-conditioned matrix
    * r1                reciprocal of condition number: 1/cond(A), 1-norm.
    * rinf              reciprocal of condition number: 1/cond(A), inf-norm.

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
         10.02.2010
         Bochkanov Sergey
    *************************************************************************/
    public static void hpdmatrixcholeskyinverse(complex[,] a,
        int n,
        bool isupper,
        matinvreport rep,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        complex[] tmp = new complex[0];

        ap.assert(n > 0, "HPDMatrixCholeskyInverse: N<=0!");
        ap.assert(ap.cols(a) >= n, "HPDMatrixCholeskyInverse: cols(A)<N!");
        ap.assert(ap.rows(a) >= n, "HPDMatrixCholeskyInverse: rows(A)<N!");
        ap.assert(apserv.isfinitectrmatrix(a, n, isupper, _params), "HPDMatrixCholeskyInverse: A contains infinite/NAN values!");

        //
        // calculate condition numbers
        //
        rep.terminationtype = 1;
        rep.r1 = rcond.hpdmatrixcholeskyrcond(a, n, isupper, _params);
        rep.rinf = rep.r1;
        if ((double)(rep.r1) < (double)(rcond.rcondthreshold(_params)) || (double)(rep.rinf) < (double)(rcond.rcondthreshold(_params)))
        {
            if (isupper)
            {
                for (i = 0; i <= n - 1; i++)
                {
                    for (j = i; j <= n - 1; j++)
                    {
                        a[i, j] = 0;
                    }
                }
            }
            else
            {
                for (i = 0; i <= n - 1; i++)
                {
                    for (j = 0; j <= i; j++)
                    {
                        a[i, j] = 0;
                    }
                }
            }
            rep.r1 = 0;
            rep.rinf = 0;
            rep.terminationtype = -3;
            return;
        }

        //
        // Inverse
        //
        tmp = new complex[n];
        hpdmatrixcholeskyinverserec(a, 0, n, isupper, ref tmp, rep, _params);
    }


    /*************************************************************************
    Inversion of a Hermitian positive definite matrix.

    Given an upper or lower triangle of a Hermitian positive definite matrix,
    the algorithm generates matrix A^-1 and saves the upper or lower triangle
    depending on the input.
      
    INPUT PARAMETERS:
        A       -   matrix to be inverted (upper or lower triangle), array[N,N]
        N       -   size of the matrix A (optional):
                    * if given, only principal NxN submatrix is processed  and
                      overwritten. Trailing elements are unchanged.
                    * if not given, the size  is automatically determined from
                      the matrix size (A must be a square matrix)
        IsUpper -   storage type:
                    * if True, symmetric  matrix  A  is  given  by  its  upper
                      triangle, and the lower triangle isn't  used/changed  by
                      function
                    * if False,  symmetric matrix  A  is  given  by  its lower
                      triangle, and the  upper triangle isn't used/changed  by
                      function

    OUTPUT PARAMETERS:
        A       -   inverse of matrix A, array[N,N]:
                    * for rep.terminationtype>0, contains matrix inverse
                    * for rep.terminationtype<0, zero-filled
        Rep     -   solver report:
                    * rep.terminationtype>0 for success, <0 for failure
                    * see below for more info

    SOLVER REPORT

    Subroutine sets following fields of the Rep structure:
    * terminationtype   completion code:
                        *  1 for success 
                        * -3 for a singular or extremely ill-conditioned matrix
    * r1                reciprocal of condition number: 1/cond(A), 1-norm.
    * rinf              reciprocal of condition number: 1/cond(A), inf-norm.

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
         10.02.2010
         Bochkanov Sergey
    *************************************************************************/
    public static void hpdmatrixinverse(complex[,] a,
        int n,
        bool isupper,
        matinvreport rep,
        xparams _params)
    {
        ap.assert(n > 0, "HPDMatrixInverse: N<=0!");
        ap.assert(ap.cols(a) >= n, "HPDMatrixInverse: cols(A)<N!");
        ap.assert(ap.rows(a) >= n, "HPDMatrixInverse: rows(A)<N!");
        ap.assert(apserv.apservisfinitectrmatrix(a, n, isupper, _params), "HPDMatrixInverse: A contains infinite or NaN values!");
        rep.r1 = 0;
        rep.rinf = 0;
        rep.terminationtype = -3;
        if (!trfac.hpdmatrixcholesky(a, n, isupper, _params))
        {
            return;
        }
        hpdmatrixcholeskyinverse(a, n, isupper, rep, _params);
    }


    /*************************************************************************
    Triangular matrix inverse (real)

    The subroutine inverts the following types of matrices:
        * upper triangular
        * upper triangular with unit diagonal
        * lower triangular
        * lower triangular with unit diagonal

    In case of an upper (lower) triangular matrix,  the  inverse  matrix  will
    also be upper (lower) triangular, and after the end of the algorithm,  the
    inverse matrix replaces the source matrix. The elements  below (above) the
    main diagonal are not changed by the algorithm.

    If  the matrix  has a unit diagonal, the inverse matrix also  has  a  unit
    diagonal, and the diagonal elements are not passed to the algorithm.
      
    INPUT PARAMETERS:
        A       -   matrix, array[0..N-1, 0..N-1].
        N       -   size of the matrix A (optional):
                    * if given, only principal NxN submatrix is processed  and
                      overwritten. Trailing elements are unchanged.
                    * if not given, the size  is automatically determined from
                      the matrix size (A must be a square matrix)
        IsUpper -   True, if the matrix is upper triangular.
        IsUnit  -   diagonal type (optional):
                    * if True, matrix has unit diagonal (a[i,i] are NOT used)
                    * if False, matrix diagonal is arbitrary
                    * if not given, False is assumed

    OUTPUT PARAMETERS:
        A       -   inverse of matrix A, array[N,N]:
                    * for rep.terminationtype>0, contains matrix inverse
                    * for rep.terminationtype<0, zero-filled
        Rep     -   solver report:
                    * rep.terminationtype>0 for success, <0 for failure
                    * see below for more info

    SOLVER REPORT

    Subroutine sets following fields of the Rep structure:
    * terminationtype   completion code:
                        *  1 for success 
                        * -3 for a singular or extremely ill-conditioned matrix
    * r1                reciprocal of condition number: 1/cond(A), 1-norm.
    * rinf              reciprocal of condition number: 1/cond(A), inf-norm.

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
         Copyright 05.02.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixtrinverse(double[,] a,
        int n,
        bool isupper,
        bool isunit,
        matinvreport rep,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double[] tmp = new double[0];

        ap.assert(n > 0, "RMatrixTRInverse: N<=0!");
        ap.assert(ap.cols(a) >= n, "RMatrixTRInverse: cols(A)<N!");
        ap.assert(ap.rows(a) >= n, "RMatrixTRInverse: rows(A)<N!");
        ap.assert(apserv.isfinitertrmatrix(a, n, isupper, _params), "RMatrixTRInverse: A contains infinite or NaN values!");

        //
        // calculate condition numbers
        //
        rep.terminationtype = 1;
        rep.r1 = rcond.rmatrixtrrcond1(a, n, isupper, isunit, _params);
        rep.rinf = rcond.rmatrixtrrcondinf(a, n, isupper, isunit, _params);
        if ((double)(rep.r1) < (double)(rcond.rcondthreshold(_params)) || (double)(rep.rinf) < (double)(rcond.rcondthreshold(_params)))
        {
            for (i = 0; i <= n - 1; i++)
            {
                for (j = 0; j <= n - 1; j++)
                {
                    a[i, j] = 0;
                }
            }
            rep.r1 = 0;
            rep.rinf = 0;
            rep.terminationtype = -3;
            return;
        }

        //
        // Invert
        //
        tmp = new double[n];
        rmatrixtrinverserec(a, 0, n, isupper, isunit, tmp, rep, _params);
    }


    /*************************************************************************
    Triangular matrix inverse (complex)

    The subroutine inverts the following types of matrices:
        * upper triangular
        * upper triangular with unit diagonal
        * lower triangular
        * lower triangular with unit diagonal

    In case of an upper (lower) triangular matrix,  the  inverse  matrix  will
    also be upper (lower) triangular, and after the end of the algorithm,  the
    inverse matrix replaces the source matrix. The elements  below (above) the
    main diagonal are not changed by the algorithm.

    If  the matrix  has a unit diagonal, the inverse matrix also  has  a  unit
    diagonal, and the diagonal elements are not passed to the algorithm.

    INPUT PARAMETERS:
        A       -   matrix, array[0..N-1, 0..N-1].
        N       -   size of the matrix A (optional):
                    * if given, only principal NxN submatrix is processed  and
                      overwritten. Trailing elements are unchanged.
                    * if not given, the size  is automatically determined from
                      the matrix size (A must be a square matrix)
        IsUpper -   True, if the matrix is upper triangular.
        IsUnit  -   diagonal type (optional):
                    * if True, matrix has unit diagonal (a[i,i] are NOT used)
                    * if False, matrix diagonal is arbitrary
                    * if not given, False is assumed

    OUTPUT PARAMETERS:
        A       -   inverse of matrix A, array[N,N]:
                    * for rep.terminationtype>0, contains matrix inverse
                    * for rep.terminationtype<0, zero-filled
        Rep     -   solver report:
                    * rep.terminationtype>0 for success, <0 for failure
                    * see below for more info

    SOLVER REPORT

    Subroutine sets following fields of the Rep structure:
    * terminationtype   completion code:
                        *  1 for success 
                        * -3 for a singular or extremely ill-conditioned matrix
    * r1                reciprocal of condition number: 1/cond(A), 1-norm.
    * rinf              reciprocal of condition number: 1/cond(A), inf-norm.

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
         Copyright 05.02.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void cmatrixtrinverse(complex[,] a,
        int n,
        bool isupper,
        bool isunit,
        matinvreport rep,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        complex[] tmp = new complex[0];

        ap.assert(n > 0, "CMatrixTRInverse: N<=0!");
        ap.assert(ap.cols(a) >= n, "CMatrixTRInverse: cols(A)<N!");
        ap.assert(ap.rows(a) >= n, "CMatrixTRInverse: rows(A)<N!");
        ap.assert(apserv.apservisfinitectrmatrix(a, n, isupper, _params), "CMatrixTRInverse: A contains infinite or NaN values!");

        //
        // calculate condition numbers
        //
        rep.terminationtype = 1;
        rep.r1 = rcond.cmatrixtrrcond1(a, n, isupper, isunit, _params);
        rep.rinf = rcond.cmatrixtrrcondinf(a, n, isupper, isunit, _params);
        if ((double)(rep.r1) < (double)(rcond.rcondthreshold(_params)) || (double)(rep.rinf) < (double)(rcond.rcondthreshold(_params)))
        {
            for (i = 0; i <= n - 1; i++)
            {
                for (j = 0; j <= n - 1; j++)
                {
                    a[i, j] = 0;
                }
            }
            rep.r1 = 0;
            rep.rinf = 0;
            rep.terminationtype = -3;
            return;
        }

        //
        // Invert
        //
        tmp = new complex[n];
        cmatrixtrinverserec(a, 0, n, isupper, isunit, tmp, rep, _params);
    }


    /*************************************************************************
    Recursive subroutine for SPD inversion.

    NOTE: this function expects that matris is strictly positive-definite.

      -- ALGLIB routine --
         10.02.2010
         Bochkanov Sergey
    *************************************************************************/
    public static void spdmatrixcholeskyinverserec(double[,] a,
        int offs,
        int n,
        bool isupper,
        double[] tmp,
        matinvreport rep,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double v = 0;
        int n1 = 0;
        int n2 = 0;
        int tsa = 0;
        int tsb = 0;
        int tscur = 0;
        int i_ = 0;
        int i1_ = 0;

        if (n < 1)
        {
            return;
        }
        tsa = apserv.matrixtilesizea(_params);
        tsb = apserv.matrixtilesizeb(_params);
        tscur = tsb;
        if (n <= tsb)
        {
            tscur = tsa;
        }

        //
        // Base case
        //
        if (n <= tsa)
        {
            rmatrixtrinverserec(a, offs, n, isupper, false, tmp, rep, _params);
            ap.assert(rep.terminationtype > 0, "SPDMatrixCholeskyInverseRec: integrity check failed");
            if (isupper)
            {

                //
                // Compute the product U * U'.
                // NOTE: we never assume that diagonal of U is real
                //
                for (i = 0; i <= n - 1; i++)
                {
                    if (i == 0)
                    {

                        //
                        // 1x1 matrix
                        //
                        a[offs + i, offs + i] = math.sqr(a[offs + i, offs + i]);
                    }
                    else
                    {

                        //
                        // (I+1)x(I+1) matrix,
                        //
                        // ( A11  A12 )   ( A11^H        )   ( A11*A11^H+A12*A12^H  A12*A22^H )
                        // (          ) * (              ) = (                                )
                        // (      A22 )   ( A12^H  A22^H )   ( A22*A12^H            A22*A22^H )
                        //
                        // A11 is IxI, A22 is 1x1.
                        //
                        i1_ = (offs) - (0);
                        for (i_ = 0; i_ <= i - 1; i_++)
                        {
                            tmp[i_] = a[i_ + i1_, offs + i];
                        }
                        for (j = 0; j <= i - 1; j++)
                        {
                            v = a[offs + j, offs + i];
                            i1_ = (j) - (offs + j);
                            for (i_ = offs + j; i_ <= offs + i - 1; i_++)
                            {
                                a[offs + j, i_] = a[offs + j, i_] + v * tmp[i_ + i1_];
                            }
                        }
                        v = a[offs + i, offs + i];
                        for (i_ = offs; i_ <= offs + i - 1; i_++)
                        {
                            a[i_, offs + i] = v * a[i_, offs + i];
                        }
                        a[offs + i, offs + i] = math.sqr(a[offs + i, offs + i]);
                    }
                }
            }
            else
            {

                //
                // Compute the product L' * L
                // NOTE: we never assume that diagonal of L is real
                //
                for (i = 0; i <= n - 1; i++)
                {
                    if (i == 0)
                    {

                        //
                        // 1x1 matrix
                        //
                        a[offs + i, offs + i] = math.sqr(a[offs + i, offs + i]);
                    }
                    else
                    {

                        //
                        // (I+1)x(I+1) matrix,
                        //
                        // ( A11^H  A21^H )   ( A11      )   ( A11^H*A11+A21^H*A21  A21^H*A22 )
                        // (              ) * (          ) = (                                )
                        // (        A22^H )   ( A21  A22 )   ( A22^H*A21            A22^H*A22 )
                        //
                        // A11 is IxI, A22 is 1x1.
                        //
                        i1_ = (offs) - (0);
                        for (i_ = 0; i_ <= i - 1; i_++)
                        {
                            tmp[i_] = a[offs + i, i_ + i1_];
                        }
                        for (j = 0; j <= i - 1; j++)
                        {
                            v = a[offs + i, offs + j];
                            i1_ = (0) - (offs);
                            for (i_ = offs; i_ <= offs + j; i_++)
                            {
                                a[offs + j, i_] = a[offs + j, i_] + v * tmp[i_ + i1_];
                            }
                        }
                        v = a[offs + i, offs + i];
                        for (i_ = offs; i_ <= offs + i - 1; i_++)
                        {
                            a[offs + i, i_] = v * a[offs + i, i_];
                        }
                        a[offs + i, offs + i] = math.sqr(a[offs + i, offs + i]);
                    }
                }
            }
            return;
        }

        //
        // Recursive code: triangular factor inversion merged with
        // UU' or L'L multiplication
        //
        apserv.tiledsplit(n, tscur, ref n1, ref n2, _params);

        //
        // form off-diagonal block of trangular inverse
        //
        if (isupper)
        {
            for (i = 0; i <= n1 - 1; i++)
            {
                for (i_ = offs + n1; i_ <= offs + n - 1; i_++)
                {
                    a[offs + i, i_] = -1 * a[offs + i, i_];
                }
            }
            ablas.rmatrixlefttrsm(n1, n2, a, offs, offs, isupper, false, 0, a, offs, offs + n1, _params);
            ablas.rmatrixrighttrsm(n1, n2, a, offs + n1, offs + n1, isupper, false, 0, a, offs, offs + n1, _params);
        }
        else
        {
            for (i = 0; i <= n2 - 1; i++)
            {
                for (i_ = offs; i_ <= offs + n1 - 1; i_++)
                {
                    a[offs + n1 + i, i_] = -1 * a[offs + n1 + i, i_];
                }
            }
            ablas.rmatrixrighttrsm(n2, n1, a, offs, offs, isupper, false, 0, a, offs + n1, offs, _params);
            ablas.rmatrixlefttrsm(n2, n1, a, offs + n1, offs + n1, isupper, false, 0, a, offs + n1, offs, _params);
        }

        //
        // invert first diagonal block
        //
        spdmatrixcholeskyinverserec(a, offs, n1, isupper, tmp, rep, _params);

        //
        // update first diagonal block with off-diagonal block,
        // update off-diagonal block
        //
        if (isupper)
        {
            ablas.rmatrixsyrk(n1, n2, 1.0, a, offs, offs + n1, 0, 1.0, a, offs, offs, isupper, _params);
            ablas.rmatrixrighttrsm(n1, n2, a, offs + n1, offs + n1, isupper, false, 1, a, offs, offs + n1, _params);
        }
        else
        {
            ablas.rmatrixsyrk(n1, n2, 1.0, a, offs + n1, offs, 1, 1.0, a, offs, offs, isupper, _params);
            ablas.rmatrixlefttrsm(n2, n1, a, offs + n1, offs + n1, isupper, false, 1, a, offs + n1, offs, _params);
        }

        //
        // invert second diagonal block
        //
        spdmatrixcholeskyinverserec(a, offs + n1, n2, isupper, tmp, rep, _params);
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_spdmatrixcholeskyinverserec(double[,] a,
        int offs,
        int n,
        bool isupper,
        double[] tmp,
        matinvreport rep, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    Triangular matrix inversion, recursive subroutine

    NOTE: this function sets Rep.TermiantionType on failure, leaves it unchanged on success.

    NOTE: only Tmp[Offs:Offs+N-1] is modified, other entries of the temporary array are not modified

      -- ALGLIB --
         05.02.2010, Bochkanov Sergey.
         Univ. of Tennessee, Univ. of California Berkeley, NAG Ltd.,
         Courant Institute, Argonne National Lab, and Rice University
         February 29, 1992.
    *************************************************************************/
    private static void rmatrixtrinverserec(double[,] a,
        int offs,
        int n,
        bool isupper,
        bool isunit,
        double[] tmp,
        matinvreport rep,
        xparams _params)
    {
        int n1 = 0;
        int n2 = 0;
        int mn = 0;
        int i = 0;
        int j = 0;
        double v = 0;
        double ajj = 0;
        int tsa = 0;
        int tsb = 0;
        int tscur = 0;
        int i_ = 0;

        ap.assert(n >= 1, "MATINV: integrity check 6755 failed");
        tsa = apserv.matrixtilesizea(_params);
        tsb = apserv.matrixtilesizeb(_params);
        tscur = tsb;
        if (n <= tsb)
        {
            tscur = tsa;
        }

        //
        // Try to activate parallelism
        //
        if (n >= 2 * tsb && (double)(apserv.rmul3(n, n, n, _params) * ((double)1 / (double)3)) >= (double)(apserv.smpactivationlevel(_params)))
        {
            if (_trypexec_rmatrixtrinverserec(a, offs, n, isupper, isunit, tmp, rep, _params))
            {
                return;
            }
        }

        //
        // Base case
        //
        if (n <= tsa)
        {
            if (isupper)
            {

                //
                // Compute inverse of upper triangular matrix.
                //
                for (j = 0; j <= n - 1; j++)
                {
                    if (!isunit)
                    {
                        if ((double)(a[offs + j, offs + j]) == (double)(0))
                        {
                            rep.terminationtype = -3;
                            return;
                        }
                        a[offs + j, offs + j] = 1 / a[offs + j, offs + j];
                        ajj = -a[offs + j, offs + j];
                    }
                    else
                    {
                        ajj = -1;
                    }

                    //
                    // Compute elements 1:j-1 of j-th column.
                    //
                    if (j > 0)
                    {
                        for (i_ = offs + 0; i_ <= offs + j - 1; i_++)
                        {
                            tmp[i_] = a[i_, offs + j];
                        }
                        for (i = 0; i <= j - 1; i++)
                        {
                            if (i < j - 1)
                            {
                                v = 0.0;
                                for (i_ = offs + i + 1; i_ <= offs + j - 1; i_++)
                                {
                                    v += a[offs + i, i_] * tmp[i_];
                                }
                            }
                            else
                            {
                                v = 0;
                            }
                            if (!isunit)
                            {
                                a[offs + i, offs + j] = v + a[offs + i, offs + i] * tmp[offs + i];
                            }
                            else
                            {
                                a[offs + i, offs + j] = v + tmp[offs + i];
                            }
                        }
                        for (i_ = offs + 0; i_ <= offs + j - 1; i_++)
                        {
                            a[i_, offs + j] = ajj * a[i_, offs + j];
                        }
                    }
                }
            }
            else
            {

                //
                // Compute inverse of lower triangular matrix.
                //
                for (j = n - 1; j >= 0; j--)
                {
                    if (!isunit)
                    {
                        if ((double)(a[offs + j, offs + j]) == (double)(0))
                        {
                            rep.terminationtype = -3;
                            return;
                        }
                        a[offs + j, offs + j] = 1 / a[offs + j, offs + j];
                        ajj = -a[offs + j, offs + j];
                    }
                    else
                    {
                        ajj = -1;
                    }
                    if (j < n - 1)
                    {

                        //
                        // Compute elements j+1:n of j-th column.
                        //
                        for (i_ = offs + j + 1; i_ <= offs + n - 1; i_++)
                        {
                            tmp[i_] = a[i_, offs + j];
                        }
                        for (i = j + 1; i <= n - 1; i++)
                        {
                            if (i > j + 1)
                            {
                                v = 0.0;
                                for (i_ = offs + j + 1; i_ <= offs + i - 1; i_++)
                                {
                                    v += a[offs + i, i_] * tmp[i_];
                                }
                            }
                            else
                            {
                                v = 0;
                            }
                            if (!isunit)
                            {
                                a[offs + i, offs + j] = v + a[offs + i, offs + i] * tmp[offs + i];
                            }
                            else
                            {
                                a[offs + i, offs + j] = v + tmp[offs + i];
                            }
                        }
                        for (i_ = offs + j + 1; i_ <= offs + n - 1; i_++)
                        {
                            a[i_, offs + j] = ajj * a[i_, offs + j];
                        }
                    }
                }
            }
            return;
        }

        //
        // Recursive case
        //
        apserv.tiledsplit(n, tscur, ref n1, ref n2, _params);
        mn = apserv.imin2(n1, n2, _params);
        apserv.touchint(ref mn, _params);
        if (n2 > 0)
        {
            if (isupper)
            {
                for (i = 0; i <= n1 - 1; i++)
                {
                    for (i_ = offs + n1; i_ <= offs + n - 1; i_++)
                    {
                        a[offs + i, i_] = -1 * a[offs + i, i_];
                    }
                }
                ablas.rmatrixrighttrsm(n1, n2, a, offs + n1, offs + n1, isupper, isunit, 0, a, offs, offs + n1, _params);
                rmatrixtrinverserec(a, offs + n1, n2, isupper, isunit, tmp, rep, _params);
                ablas.rmatrixlefttrsm(n1, n2, a, offs, offs, isupper, isunit, 0, a, offs, offs + n1, _params);
            }
            else
            {
                for (i = 0; i <= n2 - 1; i++)
                {
                    for (i_ = offs; i_ <= offs + n1 - 1; i_++)
                    {
                        a[offs + n1 + i, i_] = -1 * a[offs + n1 + i, i_];
                    }
                }
                ablas.rmatrixlefttrsm(n2, n1, a, offs + n1, offs + n1, isupper, isunit, 0, a, offs + n1, offs, _params);
                rmatrixtrinverserec(a, offs + n1, n2, isupper, isunit, tmp, rep, _params);
                ablas.rmatrixrighttrsm(n2, n1, a, offs, offs, isupper, isunit, 0, a, offs + n1, offs, _params);
            }
        }
        rmatrixtrinverserec(a, offs, n1, isupper, isunit, tmp, rep, _params);
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_rmatrixtrinverserec(double[,] a,
        int offs,
        int n,
        bool isupper,
        bool isunit,
        double[] tmp,
        matinvreport rep, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    Triangular matrix inversion, recursive subroutine.

    Rep.TerminationType is modified on failure, left unchanged on success.

      -- ALGLIB --
         05.02.2010, Bochkanov Sergey.
         Univ. of Tennessee, Univ. of California Berkeley, NAG Ltd.,
         Courant Institute, Argonne National Lab, and Rice University
         February 29, 1992.
    *************************************************************************/
    private static void cmatrixtrinverserec(complex[,] a,
        int offs,
        int n,
        bool isupper,
        bool isunit,
        complex[] tmp,
        matinvreport rep,
        xparams _params)
    {
        int n1 = 0;
        int n2 = 0;
        int i = 0;
        int j = 0;
        complex v = 0;
        complex ajj = 0;
        int tsa = 0;
        int tsb = 0;
        int tscur = 0;
        int mn = 0;
        int i_ = 0;

        tsa = apserv.matrixtilesizea(_params) / 2;
        tsb = apserv.matrixtilesizeb(_params);
        tscur = tsb;
        if (n <= tsb)
        {
            tscur = tsa;
        }

        //
        // Try to activate parallelism
        //
        if (n >= 2 * tsb && (double)(apserv.rmul3(n, n, n, _params) * ((double)4 / (double)3)) >= (double)(apserv.smpactivationlevel(_params)))
        {
            if (_trypexec_cmatrixtrinverserec(a, offs, n, isupper, isunit, tmp, rep, _params))
            {
                return;
            }
        }

        //
        // Base case
        //
        if (n <= tsa)
        {
            if (isupper)
            {

                //
                // Compute inverse of upper triangular matrix.
                //
                for (j = 0; j <= n - 1; j++)
                {
                    if (!isunit)
                    {
                        if (a[offs + j, offs + j] == 0)
                        {
                            rep.terminationtype = -3;
                            return;
                        }
                        a[offs + j, offs + j] = 1 / a[offs + j, offs + j];
                        ajj = -a[offs + j, offs + j];
                    }
                    else
                    {
                        ajj = -1;
                    }

                    //
                    // Compute elements 1:j-1 of j-th column.
                    //
                    if (j > 0)
                    {
                        for (i_ = offs + 0; i_ <= offs + j - 1; i_++)
                        {
                            tmp[i_] = a[i_, offs + j];
                        }
                        for (i = 0; i <= j - 1; i++)
                        {
                            if (i < j - 1)
                            {
                                v = 0.0;
                                for (i_ = offs + i + 1; i_ <= offs + j - 1; i_++)
                                {
                                    v += a[offs + i, i_] * tmp[i_];
                                }
                            }
                            else
                            {
                                v = 0;
                            }
                            if (!isunit)
                            {
                                a[offs + i, offs + j] = v + a[offs + i, offs + i] * tmp[offs + i];
                            }
                            else
                            {
                                a[offs + i, offs + j] = v + tmp[offs + i];
                            }
                        }
                        for (i_ = offs + 0; i_ <= offs + j - 1; i_++)
                        {
                            a[i_, offs + j] = ajj * a[i_, offs + j];
                        }
                    }
                }
            }
            else
            {

                //
                // Compute inverse of lower triangular matrix.
                //
                for (j = n - 1; j >= 0; j--)
                {
                    if (!isunit)
                    {
                        if (a[offs + j, offs + j] == 0)
                        {
                            rep.terminationtype = -3;
                            return;
                        }
                        a[offs + j, offs + j] = 1 / a[offs + j, offs + j];
                        ajj = -a[offs + j, offs + j];
                    }
                    else
                    {
                        ajj = -1;
                    }
                    if (j < n - 1)
                    {

                        //
                        // Compute elements j+1:n of j-th column.
                        //
                        for (i_ = offs + j + 1; i_ <= offs + n - 1; i_++)
                        {
                            tmp[i_] = a[i_, offs + j];
                        }
                        for (i = j + 1; i <= n - 1; i++)
                        {
                            if (i > j + 1)
                            {
                                v = 0.0;
                                for (i_ = offs + j + 1; i_ <= offs + i - 1; i_++)
                                {
                                    v += a[offs + i, i_] * tmp[i_];
                                }
                            }
                            else
                            {
                                v = 0;
                            }
                            if (!isunit)
                            {
                                a[offs + i, offs + j] = v + a[offs + i, offs + i] * tmp[offs + i];
                            }
                            else
                            {
                                a[offs + i, offs + j] = v + tmp[offs + i];
                            }
                        }
                        for (i_ = offs + j + 1; i_ <= offs + n - 1; i_++)
                        {
                            a[i_, offs + j] = ajj * a[i_, offs + j];
                        }
                    }
                }
            }
            return;
        }

        //
        // Recursive case
        //
        apserv.tiledsplit(n, tscur, ref n1, ref n2, _params);
        mn = apserv.imin2(n1, n2, _params);
        apserv.touchint(ref mn, _params);
        if (n2 > 0)
        {
            if (isupper)
            {
                for (i = 0; i <= n1 - 1; i++)
                {
                    for (i_ = offs + n1; i_ <= offs + n - 1; i_++)
                    {
                        a[offs + i, i_] = -1 * a[offs + i, i_];
                    }
                }
                ablas.cmatrixrighttrsm(n1, n2, a, offs + n1, offs + n1, isupper, isunit, 0, a, offs, offs + n1, _params);
                cmatrixtrinverserec(a, offs + n1, n2, isupper, isunit, tmp, rep, _params);
                ablas.cmatrixlefttrsm(n1, n2, a, offs, offs, isupper, isunit, 0, a, offs, offs + n1, _params);
            }
            else
            {
                for (i = 0; i <= n2 - 1; i++)
                {
                    for (i_ = offs; i_ <= offs + n1 - 1; i_++)
                    {
                        a[offs + n1 + i, i_] = -1 * a[offs + n1 + i, i_];
                    }
                }
                ablas.cmatrixlefttrsm(n2, n1, a, offs + n1, offs + n1, isupper, isunit, 0, a, offs + n1, offs, _params);
                cmatrixtrinverserec(a, offs + n1, n2, isupper, isunit, tmp, rep, _params);
                ablas.cmatrixrighttrsm(n2, n1, a, offs, offs, isupper, isunit, 0, a, offs + n1, offs, _params);
            }
        }
        cmatrixtrinverserec(a, offs, n1, isupper, isunit, tmp, rep, _params);
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_cmatrixtrinverserec(complex[,] a,
        int offs,
        int n,
        bool isupper,
        bool isunit,
        complex[] tmp,
        matinvreport rep, xparams _params)
    {
        return false;
    }


    private static void rmatrixluinverserec(double[,] a,
        int offs,
        int n,
        double[] work,
        matinvreport rep,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double v = 0;
        int n1 = 0;
        int n2 = 0;
        int tsa = 0;
        int tsb = 0;
        int tscur = 0;
        int mn = 0;
        int i_ = 0;
        int i1_ = 0;

        ap.assert(n >= 1, "MATINV: integrity check 2553 failed");
        tsa = apserv.matrixtilesizea(_params);
        tsb = apserv.matrixtilesizeb(_params);
        tscur = tsb;
        if (n <= tsb)
        {
            tscur = tsa;
        }

        //
        // Try parallelism
        //
        if (n >= 2 * tsb && (double)((double)8 / (double)6 * apserv.rmul3(n, n, n, _params)) >= (double)(apserv.smpactivationlevel(_params)))
        {
            if (_trypexec_rmatrixluinverserec(a, offs, n, work, rep, _params))
            {
                return;
            }
        }

        //
        // Base case
        //
        if (n <= tsa)
        {

            //
            // Form inv(U)
            //
            rmatrixtrinverserec(a, offs, n, true, false, work, rep, _params);

            //
            // Solve the equation inv(A)*L = inv(U) for inv(A).
            //
            for (j = n - 1; j >= 0; j--)
            {

                //
                // Copy current column of L to WORK and replace with zeros.
                //
                for (i = j + 1; i <= n - 1; i++)
                {
                    work[i] = a[offs + i, offs + j];
                    a[offs + i, offs + j] = 0;
                }

                //
                // Compute current column of inv(A).
                //
                if (j < n - 1)
                {
                    for (i = 0; i <= n - 1; i++)
                    {
                        i1_ = (j + 1) - (offs + j + 1);
                        v = 0.0;
                        for (i_ = offs + j + 1; i_ <= offs + n - 1; i_++)
                        {
                            v += a[offs + i, i_] * work[i_ + i1_];
                        }
                        a[offs + i, offs + j] = a[offs + i, offs + j] - v;
                    }
                }
            }
            return;
        }

        //
        // Recursive code:
        //
        //         ( L1      )   ( U1  U12 )
        // A    =  (         ) * (         )
        //         ( L12  L2 )   (     U2  )
        //
        //         ( W   X )
        // A^-1 =  (       )
        //         ( Y   Z )
        //
        // In-place calculation can be done as follows:
        // * X := inv(U1)*U12*inv(U2)
        // * Y := inv(L2)*L12*inv(L1)
        // * W := inv(L1*U1)+X*Y
        // * X := -X*inv(L2)
        // * Y := -inv(U2)*Y
        // * Z := inv(L2*U2)
        //
        // Reordering w.r.t. interdependencies gives us:
        //
        // * X := inv(U1)*U12      \ suitable for parallel execution
        // * Y := L12*inv(L1)      / 
        //
        // * X := X*inv(U2)        \
        // * Y := inv(L2)*Y        | suitable for parallel execution
        // * W := inv(L1*U1)       / 
        //
        // * W := W+X*Y
        //
        // * X := -X*inv(L2)       \ suitable for parallel execution
        // * Y := -inv(U2)*Y       /
        //
        // * Z := inv(L2*U2)
        //
        apserv.tiledsplit(n, tscur, ref n1, ref n2, _params);
        mn = apserv.imin2(n1, n2, _params);
        apserv.touchint(ref mn, _params);
        ap.assert(n2 > 0, "LUInverseRec: internal error!");

        //
        // X := inv(U1)*U12
        // Y := L12*inv(L1)
        //
        ablas.rmatrixlefttrsm(n1, n2, a, offs, offs, true, false, 0, a, offs, offs + n1, _params);
        ablas.rmatrixrighttrsm(n2, n1, a, offs, offs, false, true, 0, a, offs + n1, offs, _params);

        //
        // X := X*inv(U2)
        // Y := inv(L2)*Y
        // W := inv(L1*U1)
        //
        ablas.rmatrixrighttrsm(n1, n2, a, offs + n1, offs + n1, true, false, 0, a, offs, offs + n1, _params);
        ablas.rmatrixlefttrsm(n2, n1, a, offs + n1, offs + n1, false, true, 0, a, offs + n1, offs, _params);
        rmatrixluinverserec(a, offs, n1, work, rep, _params);
        if (rep.terminationtype <= 0)
        {
            return;
        }

        //
        // W := W+X*Y
        //
        ablas.rmatrixgemm(n1, n1, n2, 1.0, a, offs, offs + n1, 0, a, offs + n1, offs, 0, 1.0, a, offs, offs, _params);

        //
        // X := -X*inv(L2)
        // Y := -inv(U2)*Y
        //
        ablas.rmatrixrighttrsm(n1, n2, a, offs + n1, offs + n1, false, true, 0, a, offs, offs + n1, _params);
        ablas.rmatrixlefttrsm(n2, n1, a, offs + n1, offs + n1, true, false, 0, a, offs + n1, offs, _params);
        for (i = 0; i <= n1 - 1; i++)
        {
            for (i_ = offs + n1; i_ <= offs + n - 1; i_++)
            {
                a[offs + i, i_] = -1 * a[offs + i, i_];
            }
        }
        for (i = 0; i <= n2 - 1; i++)
        {
            for (i_ = offs; i_ <= offs + n1 - 1; i_++)
            {
                a[offs + n1 + i, i_] = -1 * a[offs + n1 + i, i_];
            }
        }

        //
        // Z := inv(L2*U2)
        //
        rmatrixluinverserec(a, offs + n1, n2, work, rep, _params);
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_rmatrixluinverserec(double[,] a,
        int offs,
        int n,
        double[] work,
        matinvreport rep, xparams _params)
    {
        return false;
    }


    private static void cmatrixluinverserec(complex[,] a,
        int offs,
        int n,
        complex[] work,
        matinvreport rep,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        complex v = 0;
        int n1 = 0;
        int n2 = 0;
        int mn = 0;
        int tsa = 0;
        int tsb = 0;
        int tscur = 0;
        int i_ = 0;
        int i1_ = 0;

        tsa = apserv.matrixtilesizea(_params) / 2;
        tsb = apserv.matrixtilesizeb(_params);
        tscur = tsb;
        if (n <= tsb)
        {
            tscur = tsa;
        }

        //
        // Try parallelism
        //
        if (n >= 2 * tsb && (double)((double)32 / (double)6 * apserv.rmul3(n, n, n, _params)) >= (double)(apserv.smpactivationlevel(_params)))
        {
            if (_trypexec_cmatrixluinverserec(a, offs, n, work, rep, _params))
            {
                return;
            }
        }

        //
        // Base case
        //
        if (n <= tsa)
        {

            //
            // Form inv(U)
            //
            cmatrixtrinverserec(a, offs, n, true, false, work, rep, _params);
            if (rep.terminationtype <= 0)
            {
                return;
            }

            //
            // Solve the equation inv(A)*L = inv(U) for inv(A).
            //
            for (j = n - 1; j >= 0; j--)
            {

                //
                // Copy current column of L to WORK and replace with zeros.
                //
                for (i = j + 1; i <= n - 1; i++)
                {
                    work[i] = a[offs + i, offs + j];
                    a[offs + i, offs + j] = 0;
                }

                //
                // Compute current column of inv(A).
                //
                if (j < n - 1)
                {
                    for (i = 0; i <= n - 1; i++)
                    {
                        i1_ = (j + 1) - (offs + j + 1);
                        v = 0.0;
                        for (i_ = offs + j + 1; i_ <= offs + n - 1; i_++)
                        {
                            v += a[offs + i, i_] * work[i_ + i1_];
                        }
                        a[offs + i, offs + j] = a[offs + i, offs + j] - v;
                    }
                }
            }
            return;
        }

        //
        // Recursive code:
        //
        //         ( L1      )   ( U1  U12 )
        // A    =  (         ) * (         )
        //         ( L12  L2 )   (     U2  )
        //
        //         ( W   X )
        // A^-1 =  (       )
        //         ( Y   Z )
        //
        // In-place calculation can be done as follows:
        // * X := inv(U1)*U12*inv(U2)
        // * Y := inv(L2)*L12*inv(L1)
        // * W := inv(L1*U1)+X*Y
        // * X := -X*inv(L2)
        // * Y := -inv(U2)*Y
        // * Z := inv(L2*U2)
        //
        // Reordering w.r.t. interdependencies gives us:
        //
        // * X := inv(U1)*U12      \ suitable for parallel execution
        // * Y := L12*inv(L1)      / 
        //
        // * X := X*inv(U2)        \
        // * Y := inv(L2)*Y        | suitable for parallel execution
        // * W := inv(L1*U1)       / 
        //
        // * W := W+X*Y
        //
        // * X := -X*inv(L2)       \ suitable for parallel execution
        // * Y := -inv(U2)*Y       /
        //
        // * Z := inv(L2*U2)
        //
        apserv.tiledsplit(n, tscur, ref n1, ref n2, _params);
        mn = apserv.imin2(n1, n2, _params);
        apserv.touchint(ref mn, _params);
        ap.assert(n2 > 0, "LUInverseRec: internal error!");

        //
        // X := inv(U1)*U12
        // Y := L12*inv(L1)
        //
        ablas.cmatrixlefttrsm(n1, n2, a, offs, offs, true, false, 0, a, offs, offs + n1, _params);
        ablas.cmatrixrighttrsm(n2, n1, a, offs, offs, false, true, 0, a, offs + n1, offs, _params);

        //
        // X := X*inv(U2)
        // Y := inv(L2)*Y
        // W := inv(L1*U1)
        //
        ablas.cmatrixrighttrsm(n1, n2, a, offs + n1, offs + n1, true, false, 0, a, offs, offs + n1, _params);
        ablas.cmatrixlefttrsm(n2, n1, a, offs + n1, offs + n1, false, true, 0, a, offs + n1, offs, _params);
        cmatrixluinverserec(a, offs, n1, work, rep, _params);
        if (rep.terminationtype <= 0)
        {
            return;
        }

        //
        // W := W+X*Y
        //
        ablas.cmatrixgemm(n1, n1, n2, 1.0, a, offs, offs + n1, 0, a, offs + n1, offs, 0, 1.0, a, offs, offs, _params);

        //
        // X := -X*inv(L2)
        // Y := -inv(U2)*Y
        //
        ablas.cmatrixrighttrsm(n1, n2, a, offs + n1, offs + n1, false, true, 0, a, offs, offs + n1, _params);
        ablas.cmatrixlefttrsm(n2, n1, a, offs + n1, offs + n1, true, false, 0, a, offs + n1, offs, _params);
        for (i = 0; i <= n1 - 1; i++)
        {
            for (i_ = offs + n1; i_ <= offs + n - 1; i_++)
            {
                a[offs + i, i_] = -1 * a[offs + i, i_];
            }
        }
        for (i = 0; i <= n2 - 1; i++)
        {
            for (i_ = offs; i_ <= offs + n1 - 1; i_++)
            {
                a[offs + n1 + i, i_] = -1 * a[offs + n1 + i, i_];
            }
        }

        //
        // Z := inv(L2*U2)
        //
        cmatrixluinverserec(a, offs + n1, n2, work, rep, _params);
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_cmatrixluinverserec(complex[,] a,
        int offs,
        int n,
        complex[] work,
        matinvreport rep, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    Recursive subroutine for HPD inversion.

      -- ALGLIB routine --
         10.02.2010
         Bochkanov Sergey
    *************************************************************************/
    private static void hpdmatrixcholeskyinverserec(complex[,] a,
        int offs,
        int n,
        bool isupper,
        ref complex[] tmp,
        matinvreport rep,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        complex v = 0;
        int n1 = 0;
        int n2 = 0;
        int tsa = 0;
        int tsb = 0;
        int tscur = 0;
        int i_ = 0;
        int i1_ = 0;

        if (n < 1)
        {
            return;
        }
        tsa = apserv.matrixtilesizea(_params) / 2;
        tsb = apserv.matrixtilesizeb(_params);
        tscur = tsb;
        if (n <= tsb)
        {
            tscur = tsa;
        }

        //
        // Base case
        //
        if (n <= tsa)
        {
            cmatrixtrinverserec(a, offs, n, isupper, false, tmp, rep, _params);
            ap.assert(rep.terminationtype > 0, "HPDMatrixCholeskyInverseRec: integrity check failed");
            if (isupper)
            {

                //
                // Compute the product U * U'.
                // NOTE: we never assume that diagonal of U is real
                //
                for (i = 0; i <= n - 1; i++)
                {
                    if (i == 0)
                    {

                        //
                        // 1x1 matrix
                        //
                        a[offs + i, offs + i] = math.sqr(a[offs + i, offs + i].x) + math.sqr(a[offs + i, offs + i].y);
                    }
                    else
                    {

                        //
                        // (I+1)x(I+1) matrix,
                        //
                        // ( A11  A12 )   ( A11^H        )   ( A11*A11^H+A12*A12^H  A12*A22^H )
                        // (          ) * (              ) = (                                )
                        // (      A22 )   ( A12^H  A22^H )   ( A22*A12^H            A22*A22^H )
                        //
                        // A11 is IxI, A22 is 1x1.
                        //
                        i1_ = (offs) - (0);
                        for (i_ = 0; i_ <= i - 1; i_++)
                        {
                            tmp[i_] = math.conj(a[i_ + i1_, offs + i]);
                        }
                        for (j = 0; j <= i - 1; j++)
                        {
                            v = a[offs + j, offs + i];
                            i1_ = (j) - (offs + j);
                            for (i_ = offs + j; i_ <= offs + i - 1; i_++)
                            {
                                a[offs + j, i_] = a[offs + j, i_] + v * tmp[i_ + i1_];
                            }
                        }
                        v = math.conj(a[offs + i, offs + i]);
                        for (i_ = offs; i_ <= offs + i - 1; i_++)
                        {
                            a[i_, offs + i] = v * a[i_, offs + i];
                        }
                        a[offs + i, offs + i] = math.sqr(a[offs + i, offs + i].x) + math.sqr(a[offs + i, offs + i].y);
                    }
                }
            }
            else
            {

                //
                // Compute the product L' * L
                // NOTE: we never assume that diagonal of L is real
                //
                for (i = 0; i <= n - 1; i++)
                {
                    if (i == 0)
                    {

                        //
                        // 1x1 matrix
                        //
                        a[offs + i, offs + i] = math.sqr(a[offs + i, offs + i].x) + math.sqr(a[offs + i, offs + i].y);
                    }
                    else
                    {

                        //
                        // (I+1)x(I+1) matrix,
                        //
                        // ( A11^H  A21^H )   ( A11      )   ( A11^H*A11+A21^H*A21  A21^H*A22 )
                        // (              ) * (          ) = (                                )
                        // (        A22^H )   ( A21  A22 )   ( A22^H*A21            A22^H*A22 )
                        //
                        // A11 is IxI, A22 is 1x1.
                        //
                        i1_ = (offs) - (0);
                        for (i_ = 0; i_ <= i - 1; i_++)
                        {
                            tmp[i_] = a[offs + i, i_ + i1_];
                        }
                        for (j = 0; j <= i - 1; j++)
                        {
                            v = math.conj(a[offs + i, offs + j]);
                            i1_ = (0) - (offs);
                            for (i_ = offs; i_ <= offs + j; i_++)
                            {
                                a[offs + j, i_] = a[offs + j, i_] + v * tmp[i_ + i1_];
                            }
                        }
                        v = math.conj(a[offs + i, offs + i]);
                        for (i_ = offs; i_ <= offs + i - 1; i_++)
                        {
                            a[offs + i, i_] = v * a[offs + i, i_];
                        }
                        a[offs + i, offs + i] = math.sqr(a[offs + i, offs + i].x) + math.sqr(a[offs + i, offs + i].y);
                    }
                }
            }
            return;
        }

        //
        // Recursive code: triangular factor inversion merged with
        // UU' or L'L multiplication
        //
        apserv.tiledsplit(n, tscur, ref n1, ref n2, _params);

        //
        // form off-diagonal block of trangular inverse
        //
        if (isupper)
        {
            for (i = 0; i <= n1 - 1; i++)
            {
                for (i_ = offs + n1; i_ <= offs + n - 1; i_++)
                {
                    a[offs + i, i_] = -1 * a[offs + i, i_];
                }
            }
            ablas.cmatrixlefttrsm(n1, n2, a, offs, offs, isupper, false, 0, a, offs, offs + n1, _params);
            ablas.cmatrixrighttrsm(n1, n2, a, offs + n1, offs + n1, isupper, false, 0, a, offs, offs + n1, _params);
        }
        else
        {
            for (i = 0; i <= n2 - 1; i++)
            {
                for (i_ = offs; i_ <= offs + n1 - 1; i_++)
                {
                    a[offs + n1 + i, i_] = -1 * a[offs + n1 + i, i_];
                }
            }
            ablas.cmatrixrighttrsm(n2, n1, a, offs, offs, isupper, false, 0, a, offs + n1, offs, _params);
            ablas.cmatrixlefttrsm(n2, n1, a, offs + n1, offs + n1, isupper, false, 0, a, offs + n1, offs, _params);
        }

        //
        // invert first diagonal block
        //
        hpdmatrixcholeskyinverserec(a, offs, n1, isupper, ref tmp, rep, _params);

        //
        // update first diagonal block with off-diagonal block,
        // update off-diagonal block
        //
        if (isupper)
        {
            ablas.cmatrixherk(n1, n2, 1.0, a, offs, offs + n1, 0, 1.0, a, offs, offs, isupper, _params);
            ablas.cmatrixrighttrsm(n1, n2, a, offs + n1, offs + n1, isupper, false, 2, a, offs, offs + n1, _params);
        }
        else
        {
            ablas.cmatrixherk(n1, n2, 1.0, a, offs + n1, offs, 2, 1.0, a, offs, offs, isupper, _params);
            ablas.cmatrixlefttrsm(n2, n1, a, offs + n1, offs + n1, isupper, false, 2, a, offs + n1, offs, _params);
        }

        //
        // invert second diagonal block
        //
        hpdmatrixcholeskyinverserec(a, offs + n1, n2, isupper, ref tmp, rep, _params);
    }


}
