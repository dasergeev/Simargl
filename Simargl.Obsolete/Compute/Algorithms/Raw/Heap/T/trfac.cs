#pragma warning disable CS8618
#pragma warning disable CS1591

using System;

namespace Simargl.Algorithms.Raw;

public class trfac
{
    /*************************************************************************
    An analysis of the sparse matrix decomposition, performed prior to  actual
    numerical factorization. You should not directly  access  fields  of  this
    object - use appropriate ALGLIB functions to work with this object.
    *************************************************************************/
    public class sparsedecompositionanalysis : apobject
    {
        public int n;
        public int facttype;
        public int permtype;
        public spchol.spcholanalysis analysis;
        public sparse.sparsematrix wrka;
        public sparse.sparsematrix wrkat;
        public sparse.sparsematrix crsa;
        public sparse.sparsematrix crsat;
        public sparsedecompositionanalysis()
        {
            init();
        }
        public override void init()
        {
            analysis = new spchol.spcholanalysis();
            wrka = new sparse.sparsematrix();
            wrkat = new sparse.sparsematrix();
            crsa = new sparse.sparsematrix();
            crsat = new sparse.sparsematrix();
        }
        public override apobject make_copy()
        {
            sparsedecompositionanalysis _result = new sparsedecompositionanalysis();
            _result.n = n;
            _result.facttype = facttype;
            _result.permtype = permtype;
            _result.analysis = (spchol.spcholanalysis)analysis.make_copy();
            _result.wrka = (sparse.sparsematrix)wrka.make_copy();
            _result.wrkat = (sparse.sparsematrix)wrkat.make_copy();
            _result.crsa = (sparse.sparsematrix)crsa.make_copy();
            _result.crsat = (sparse.sparsematrix)crsat.make_copy();
            return _result;
        }
    };




    /*************************************************************************
    LU decomposition of a general real matrix with row pivoting

    A is represented as A = P*L*U, where:
    * L is lower unitriangular matrix
    * U is upper triangular matrix
    * P = P0*P1*...*PK, K=min(M,N)-1,
      Pi - permutation matrix for I and Pivots[I]
      
    INPUT PARAMETERS:
        A       -   array[0..M-1, 0..N-1].
        M       -   number of rows in matrix A.
        N       -   number of columns in matrix A.


    OUTPUT PARAMETERS:
        A       -   matrices L and U in compact form:
                    * L is stored under main diagonal
                    * U is stored on and above main diagonal
        Pivots  -   permutation matrix in compact form.
                    array[0..Min(M-1,N-1)].
      
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
         10.01.2010
         Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixlu(double[,] a,
        int m,
        int n,
        ref int[] pivots,
        xparams _params)
    {
        pivots = new int[0];

        ap.assert(m > 0, "RMatrixLU: incorrect M!");
        ap.assert(n > 0, "RMatrixLU: incorrect N!");
        ap.assert(ap.rows(a) >= m, "RMatrixLU: rows(A)<M");
        ap.assert(ap.cols(a) >= n, "RMatrixLU: cols(A)<N");
        ap.assert(apserv.apservisfinitematrix(a, m, n, _params), "RMatrixLU: A contains infinite or NaN values!");
        rmatrixplu(a, m, n, ref pivots, _params);
    }


    /*************************************************************************
    LU decomposition of a general complex matrix with row pivoting

    A is represented as A = P*L*U, where:
    * L is lower unitriangular matrix
    * U is upper triangular matrix
    * P = P0*P1*...*PK, K=min(M,N)-1,
      Pi - permutation matrix for I and Pivots[I]

    INPUT PARAMETERS:
        A       -   array[0..M-1, 0..N-1].
        M       -   number of rows in matrix A.
        N       -   number of columns in matrix A.


    OUTPUT PARAMETERS:
        A       -   matrices L and U in compact form:
                    * L is stored under main diagonal
                    * U is stored on and above main diagonal
        Pivots  -   permutation matrix in compact form.
                    array[0..Min(M-1,N-1)].

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
         10.01.2010
         Bochkanov Sergey
    *************************************************************************/
    public static void cmatrixlu(complex[,] a,
        int m,
        int n,
        ref int[] pivots,
        xparams _params)
    {
        pivots = new int[0];

        ap.assert(m > 0, "CMatrixLU: incorrect M!");
        ap.assert(n > 0, "CMatrixLU: incorrect N!");
        ap.assert(ap.rows(a) >= m, "CMatrixLU: rows(A)<M");
        ap.assert(ap.cols(a) >= n, "CMatrixLU: cols(A)<N");
        ap.assert(apserv.isfinitecmatrix(a, m, n, _params), "CMatrixLU: A contains infinite or NaN values!");
        cmatrixplu(a, m, n, ref pivots, _params);
    }


    /*************************************************************************
    Cache-oblivious Cholesky decomposition

    The algorithm computes Cholesky decomposition  of  a  Hermitian  positive-
    definite matrix. The result of an algorithm is a representation  of  A  as
    A=U'*U  or A=L*L' (here X' denotes conj(X^T)).

    INPUT PARAMETERS:
        A       -   upper or lower triangle of a factorized matrix.
                    array with elements [0..N-1, 0..N-1].
        N       -   size of matrix A.
        IsUpper -   if IsUpper=True, then A contains an upper triangle of
                    a symmetric matrix, otherwise A contains a lower one.

    OUTPUT PARAMETERS:
        A       -   the result of factorization. If IsUpper=True, then
                    the upper triangle contains matrix U, so that A = U'*U,
                    and the elements below the main diagonal are not modified.
                    Similarly, if IsUpper = False.

    RESULT:
        If  the  matrix  is  positive-definite,  the  function  returns  True.
        Otherwise, the function returns False. Contents of A is not determined
        in such case.

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
    public static bool hpdmatrixcholesky(complex[,] a,
        int n,
        bool isupper,
        xparams _params)
    {
        bool result = new bool();
        complex[] tmp = new complex[0];

        ap.assert(n > 0, "HPDMatrixCholesky: incorrect N!");
        ap.assert(ap.rows(a) >= n, "HPDMatrixCholesky: rows(A)<N");
        ap.assert(ap.cols(a) >= n, "HPDMatrixCholesky: cols(A)<N");
        ap.assert(apserv.isfinitectrmatrix(a, n, isupper, _params), "HPDMatrixCholesky: A contains infinite or NaN values!");
        result = hpdmatrixcholeskyrec(a, 0, n, isupper, ref tmp, _params);
        return result;
    }


    /*************************************************************************
    Cache-oblivious Cholesky decomposition

    The algorithm computes Cholesky decomposition  of  a  symmetric  positive-
    definite matrix. The result of an algorithm is a representation  of  A  as
    A=U^T*U  or A=L*L^T

    INPUT PARAMETERS:
        A       -   upper or lower triangle of a factorized matrix.
                    array with elements [0..N-1, 0..N-1].
        N       -   size of matrix A.
        IsUpper -   if IsUpper=True, then A contains an upper triangle of
                    a symmetric matrix, otherwise A contains a lower one.

    OUTPUT PARAMETERS:
        A       -   the result of factorization. If IsUpper=True, then
                    the upper triangle contains matrix U, so that A = U^T*U,
                    and the elements below the main diagonal are not modified.
                    Similarly, if IsUpper = False.

    RESULT:
        If  the  matrix  is  positive-definite,  the  function  returns  True.
        Otherwise, the function returns False. Contents of A is not determined
        in such case.

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
         15.12.2009
         Bochkanov Sergey
    *************************************************************************/
    public static bool spdmatrixcholesky(double[,] a,
        int n,
        bool isupper,
        xparams _params)
    {
        bool result = new bool();
        double[] tmp = new double[0];

        ap.assert(n > 0, "SPDMatrixCholesky: incorrect N!");
        ap.assert(ap.rows(a) >= n, "SPDMatrixCholesky: rows(A)<N");
        ap.assert(ap.cols(a) >= n, "SPDMatrixCholesky: cols(A)<N");
        ap.assert(apserv.isfinitertrmatrix(a, n, isupper, _params), "SPDMatrixCholesky: A contains infinite or NaN values!");
        result = spdmatrixcholeskyrec(a, 0, n, isupper, ref tmp, _params);
        return result;
    }


    /*************************************************************************
    Update of Cholesky decomposition: rank-1 update to original A.  "Buffered"
    version which uses preallocated buffer which is saved  between  subsequent
    function calls.

    This function uses internally allocated buffer which is not saved  between
    subsequent  calls.  So,  if  you  perform  a lot  of  subsequent  updates,
    we  recommend   you   to   use   "buffered"   version   of  this function:
    SPDMatrixCholeskyUpdateAdd1Buf().

    INPUT PARAMETERS:
        A       -   upper or lower Cholesky factor.
                    array with elements [0..N-1, 0..N-1].
                    Exception is thrown if array size is too small.
        N       -   size of matrix A, N>0
        IsUpper -   if IsUpper=True, then A contains  upper  Cholesky  factor;
                    otherwise A contains a lower one.
        U       -   array[N], rank-1 update to A: A_mod = A + u*u'
                    Exception is thrown if array size is too small.

    OUTPUT PARAMETERS:
        A       -   updated factorization.  If  IsUpper=True,  then  the  upper
                    triangle contains matrix U, and the elements below the main
                    diagonal are not modified. Similarly, if IsUpper = False.
                    
    NOTE: this function always succeeds, so it does not return completion code

    NOTE: this function checks sizes of input arrays, but it does  NOT  checks
          for presence of infinities or NAN's.

      -- ALGLIB --
         03.02.2014
         Sergey Bochkanov
    *************************************************************************/
    public static void spdmatrixcholeskyupdateadd1(double[,] a,
        int n,
        bool isupper,
        double[] u,
        xparams _params)
    {
        double[] bufr = new double[0];

        ap.assert(n > 0, "SPDMatrixCholeskyUpdateAdd1: N<=0");
        ap.assert(ap.rows(a) >= n, "SPDMatrixCholeskyUpdateAdd1: Rows(A)<N");
        ap.assert(ap.cols(a) >= n, "SPDMatrixCholeskyUpdateAdd1: Cols(A)<N");
        ap.assert(ap.len(u) >= n, "SPDMatrixCholeskyUpdateAdd1: Length(U)<N");
        spdmatrixcholeskyupdateadd1buf(a, n, isupper, u, ref bufr, _params);
    }


    /*************************************************************************
    Update of Cholesky decomposition: "fixing" some variables.

    This function uses internally allocated buffer which is not saved  between
    subsequent  calls.  So,  if  you  perform  a lot  of  subsequent  updates,
    we  recommend   you   to   use   "buffered"   version   of  this function:
    SPDMatrixCholeskyUpdateFixBuf().

    "FIXING" EXPLAINED:

        Suppose we have N*N positive definite matrix A. "Fixing" some variable
        means filling corresponding row/column of  A  by  zeros,  and  setting
        diagonal element to 1.
        
        For example, if we fix 2nd variable in 4*4 matrix A, it becomes Af:
        
            ( A00  A01  A02  A03 )      ( Af00  0   Af02 Af03 )
            ( A10  A11  A12  A13 )      (  0    1    0    0   )
            ( A20  A21  A22  A23 )  =>  ( Af20  0   Af22 Af23 )
            ( A30  A31  A32  A33 )      ( Af30  0   Af32 Af33 )
        
        If we have Cholesky decomposition of A, it must be recalculated  after
        variables were  fixed.  However,  it  is  possible  to  use  efficient
        algorithm, which needs O(K*N^2)  time  to  "fix"  K  variables,  given
        Cholesky decomposition of original, "unfixed" A.

    INPUT PARAMETERS:
        A       -   upper or lower Cholesky factor.
                    array with elements [0..N-1, 0..N-1].
                    Exception is thrown if array size is too small.
        N       -   size of matrix A, N>0
        IsUpper -   if IsUpper=True, then A contains  upper  Cholesky  factor;
                    otherwise A contains a lower one.
        Fix     -   array[N], I-th element is True if I-th  variable  must  be
                    fixed. Exception is thrown if array size is too small.
        BufR    -   possibly preallocated  buffer;  automatically  resized  if
                    needed. It is recommended to  reuse  this  buffer  if  you
                    perform a lot of subsequent decompositions.

    OUTPUT PARAMETERS:
        A       -   updated factorization.  If  IsUpper=True,  then  the  upper
                    triangle contains matrix U, and the elements below the main
                    diagonal are not modified. Similarly, if IsUpper = False.
                    
    NOTE: this function always succeeds, so it does not return completion code

    NOTE: this function checks sizes of input arrays, but it does  NOT  checks
          for presence of infinities or NAN's.
          
    NOTE: this  function  is  efficient  only  for  moderate amount of updated
          variables - say, 0.1*N or 0.3*N. For larger amount of  variables  it
          will  still  work,  but  you  may  get   better   performance   with
          straightforward Cholesky.

      -- ALGLIB --
         03.02.2014
         Sergey Bochkanov
    *************************************************************************/
    public static void spdmatrixcholeskyupdatefix(double[,] a,
        int n,
        bool isupper,
        bool[] fix,
        xparams _params)
    {
        double[] bufr = new double[0];

        ap.assert(n > 0, "SPDMatrixCholeskyUpdateFix: N<=0");
        ap.assert(ap.rows(a) >= n, "SPDMatrixCholeskyUpdateFix: Rows(A)<N");
        ap.assert(ap.cols(a) >= n, "SPDMatrixCholeskyUpdateFix: Cols(A)<N");
        ap.assert(ap.len(fix) >= n, "SPDMatrixCholeskyUpdateFix: Length(Fix)<N");
        spdmatrixcholeskyupdatefixbuf(a, n, isupper, fix, ref bufr, _params);
    }


    /*************************************************************************
    Update of Cholesky decomposition: rank-1 update to original A.  "Buffered"
    version which uses preallocated buffer which is saved  between  subsequent
    function calls.

    See comments for SPDMatrixCholeskyUpdateAdd1() for more information.

    INPUT PARAMETERS:
        A       -   upper or lower Cholesky factor.
                    array with elements [0..N-1, 0..N-1].
                    Exception is thrown if array size is too small.
        N       -   size of matrix A, N>0
        IsUpper -   if IsUpper=True, then A contains  upper  Cholesky  factor;
                    otherwise A contains a lower one.
        U       -   array[N], rank-1 update to A: A_mod = A + u*u'
                    Exception is thrown if array size is too small.
        BufR    -   possibly preallocated  buffer;  automatically  resized  if
                    needed. It is recommended to  reuse  this  buffer  if  you
                    perform a lot of subsequent decompositions.

    OUTPUT PARAMETERS:
        A       -   updated factorization.  If  IsUpper=True,  then  the  upper
                    triangle contains matrix U, and the elements below the main
                    diagonal are not modified. Similarly, if IsUpper = False.

      -- ALGLIB --
         03.02.2014
         Sergey Bochkanov
    *************************************************************************/
    public static void spdmatrixcholeskyupdateadd1buf(double[,] a,
        int n,
        bool isupper,
        double[] u,
        ref double[] bufr,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int nz = 0;
        double cs = 0;
        double sn = 0;
        double v = 0;
        double vv = 0;

        ap.assert(n > 0, "SPDMatrixCholeskyUpdateAdd1Buf: N<=0");
        ap.assert(ap.rows(a) >= n, "SPDMatrixCholeskyUpdateAdd1Buf: Rows(A)<N");
        ap.assert(ap.cols(a) >= n, "SPDMatrixCholeskyUpdateAdd1Buf: Cols(A)<N");
        ap.assert(ap.len(u) >= n, "SPDMatrixCholeskyUpdateAdd1Buf: Length(U)<N");
        ap.assert(apserv.isfinitertrmatrix(a, n, isupper, _params), "SPDMatrixCholeskyUpdateAdd1Buf: A contains infinite/NAN values");
        ap.assert(apserv.isfinitevector(u, n, _params), "SPDMatrixCholeskyUpdateAdd1Buf: A contains infinite/NAN values");

        //
        // Find index of first non-zero entry in U
        //
        nz = n;
        for (i = 0; i <= n - 1; i++)
        {
            if ((double)(u[i]) != (double)(0))
            {
                nz = i;
                break;
            }
        }
        if (nz == n)
        {

            //
            // Nothing to update
            //
            return;
        }

        //
        // If working with upper triangular matrix
        //
        if (isupper)
        {

            //
            // Perform a sequence of updates which fix variables one by one.
            // This approach is different from one which is used when we work
            // with lower triangular matrix.
            //
            apserv.rvectorsetlengthatleast(ref bufr, n, _params);
            for (j = nz; j <= n - 1; j++)
            {
                bufr[j] = u[j];
            }
            for (i = nz; i <= n - 1; i++)
            {
                if ((double)(bufr[i]) != (double)(0))
                {
                    rotations.generaterotation(a[i, i], bufr[i], ref cs, ref sn, ref v, _params);
                    a[i, i] = v;
                    bufr[i] = 0.0;
                    for (j = i + 1; j <= n - 1; j++)
                    {
                        v = a[i, j];
                        vv = bufr[j];
                        a[i, j] = cs * v + sn * vv;
                        bufr[j] = -(sn * v) + cs * vv;
                    }
                }
            }
        }
        else
        {

            //
            // Calculate rows of modified Cholesky factor, row-by-row
            // (updates performed during variable fixing are applied
            // simultaneously to each row)
            //
            apserv.rvectorsetlengthatleast(ref bufr, 3 * n, _params);
            for (j = nz; j <= n - 1; j++)
            {
                bufr[j] = u[j];
            }
            for (i = nz; i <= n - 1; i++)
            {

                //
                // Update all previous updates [Idx+1...I-1] to I-th row
                //
                vv = bufr[i];
                for (j = nz; j <= i - 1; j++)
                {
                    cs = bufr[n + 2 * j + 0];
                    sn = bufr[n + 2 * j + 1];
                    v = a[i, j];
                    a[i, j] = cs * v + sn * vv;
                    vv = -(sn * v) + cs * vv;
                }

                //
                // generate rotation applied to I-th element of update vector
                //
                rotations.generaterotation(a[i, i], vv, ref cs, ref sn, ref v, _params);
                a[i, i] = v;
                bufr[n + 2 * i + 0] = cs;
                bufr[n + 2 * i + 1] = sn;
            }
        }
    }


    /*************************************************************************
    Update of Cholesky  decomposition:  "fixing"  some  variables.  "Buffered"
    version which uses preallocated buffer which is saved  between  subsequent
    function calls.

    See comments for SPDMatrixCholeskyUpdateFix() for more information.

    INPUT PARAMETERS:
        A       -   upper or lower Cholesky factor.
                    array with elements [0..N-1, 0..N-1].
                    Exception is thrown if array size is too small.
        N       -   size of matrix A, N>0
        IsUpper -   if IsUpper=True, then A contains  upper  Cholesky  factor;
                    otherwise A contains a lower one.
        Fix     -   array[N], I-th element is True if I-th  variable  must  be
                    fixed. Exception is thrown if array size is too small.
        BufR    -   possibly preallocated  buffer;  automatically  resized  if
                    needed. It is recommended to  reuse  this  buffer  if  you
                    perform a lot of subsequent decompositions.

    OUTPUT PARAMETERS:
        A       -   updated factorization.  If  IsUpper=True,  then  the  upper
                    triangle contains matrix U, and the elements below the main
                    diagonal are not modified. Similarly, if IsUpper = False.

      -- ALGLIB --
         03.02.2014
         Sergey Bochkanov
    *************************************************************************/
    public static void spdmatrixcholeskyupdatefixbuf(double[,] a,
        int n,
        bool isupper,
        bool[] fix,
        ref double[] bufr,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        int nfix = 0;
        int idx = 0;
        double cs = 0;
        double sn = 0;
        double v = 0;
        double vv = 0;

        ap.assert(n > 0, "SPDMatrixCholeskyUpdateFixBuf: N<=0");
        ap.assert(ap.rows(a) >= n, "SPDMatrixCholeskyUpdateFixBuf: Rows(A)<N");
        ap.assert(ap.cols(a) >= n, "SPDMatrixCholeskyUpdateFixBuf: Cols(A)<N");
        ap.assert(ap.len(fix) >= n, "SPDMatrixCholeskyUpdateFixBuf: Length(Fix)<N");
        ap.assert(apserv.isfinitertrmatrix(a, n, isupper, _params), "SPDMatrixCholeskyUpdateAdd1Buf: A contains infinite/NAN values");

        //
        // Count number of variables to fix.
        // Quick exit if NFix=0 or NFix=N
        //
        nfix = 0;
        for (i = 0; i <= n - 1; i++)
        {
            if (fix[i])
            {
                apserv.inc(ref nfix, _params);
            }
        }
        if (nfix == 0)
        {

            //
            // Nothing to fix
            //
            return;
        }
        if (nfix == n)
        {

            //
            // All variables are fixed.
            // Set A to identity and exit.
            //
            if (isupper)
            {
                for (i = 0; i <= n - 1; i++)
                {
                    a[i, i] = 1;
                    for (j = i + 1; j <= n - 1; j++)
                    {
                        a[i, j] = 0;
                    }
                }
            }
            else
            {
                for (i = 0; i <= n - 1; i++)
                {
                    for (j = 0; j <= i - 1; j++)
                    {
                        a[i, j] = 0;
                    }
                    a[i, i] = 1;
                }
            }
            return;
        }

        //
        // If working with upper triangular matrix
        //
        if (isupper)
        {

            //
            // Perform a sequence of updates which fix variables one by one.
            // This approach is different from one which is used when we work
            // with lower triangular matrix.
            //
            apserv.rvectorsetlengthatleast(ref bufr, n, _params);
            for (k = 0; k <= n - 1; k++)
            {
                if (fix[k])
                {
                    idx = k;

                    //
                    // Quick exit if it is last variable
                    //
                    if (idx == n - 1)
                    {
                        for (i = 0; i <= idx - 1; i++)
                        {
                            a[i, idx] = 0.0;
                        }
                        a[idx, idx] = 1.0;
                        continue;
                    }

                    //
                    // We have Cholesky decomposition of quadratic term in A,
                    // with upper triangle being stored as given below:
                    //
                    //         ( U00 u01 U02 )
                    //     U = (     u11 u12 )
                    //         (         U22 )
                    //
                    // Here u11 is diagonal element corresponding to variable K. We
                    // want to fix this variable, and we do so by modifying U as follows:
                    //
                    //             ( U00  0  U02 )
                    //     U_mod = (      1   0  )
                    //             (         U_m )
                    //
                    // with U_m = CHOLESKY [ (U22^T)*U22 + (u12^T)*u12 ]
                    //
                    // Of course, we can calculate U_m by calculating (U22^T)*U22 explicitly,
                    // modifying it and performing Cholesky decomposition of modified matrix.
                    // However, we can treat it as follows:
                    // * we already have CHOLESKY[(U22^T)*U22], which is equal to U22
                    // * we have rank-1 update (u12^T)*u12 applied to (U22^T)*U22
                    // * thus, we can calculate updated Cholesky with O(N^2) algorithm
                    //   instead of O(N^3) one
                    //
                    for (j = idx + 1; j <= n - 1; j++)
                    {
                        bufr[j] = a[idx, j];
                    }
                    for (i = 0; i <= idx - 1; i++)
                    {
                        a[i, idx] = 0.0;
                    }
                    a[idx, idx] = 1.0;
                    for (i = idx + 1; i <= n - 1; i++)
                    {
                        a[idx, i] = 0.0;
                    }
                    for (i = idx + 1; i <= n - 1; i++)
                    {
                        if ((double)(bufr[i]) != (double)(0))
                        {
                            rotations.generaterotation(a[i, i], bufr[i], ref cs, ref sn, ref v, _params);
                            a[i, i] = v;
                            bufr[i] = 0.0;
                            for (j = i + 1; j <= n - 1; j++)
                            {
                                v = a[i, j];
                                vv = bufr[j];
                                a[i, j] = cs * v + sn * vv;
                                bufr[j] = -(sn * v) + cs * vv;
                            }
                        }
                    }
                }
            }
        }
        else
        {

            //
            // Calculate rows of modified Cholesky factor, row-by-row
            // (updates performed during variable fixing are applied
            // simultaneously to each row)
            //
            apserv.rvectorsetlengthatleast(ref bufr, 3 * n, _params);
            for (k = 0; k <= n - 1; k++)
            {
                if (fix[k])
                {
                    idx = k;

                    //
                    // Quick exit if it is last variable
                    //
                    if (idx == n - 1)
                    {
                        for (i = 0; i <= idx - 1; i++)
                        {
                            a[idx, i] = 0.0;
                        }
                        a[idx, idx] = 1.0;
                        continue;
                    }

                    //
                    // store column to buffer and clear row/column of A
                    //
                    for (j = idx + 1; j <= n - 1; j++)
                    {
                        bufr[j] = a[j, idx];
                    }
                    for (i = 0; i <= idx - 1; i++)
                    {
                        a[idx, i] = 0.0;
                    }
                    a[idx, idx] = 1.0;
                    for (i = idx + 1; i <= n - 1; i++)
                    {
                        a[i, idx] = 0.0;
                    }

                    //
                    // Apply update to rows of A
                    //
                    for (i = idx + 1; i <= n - 1; i++)
                    {

                        //
                        // Update all previous updates [Idx+1...I-1] to I-th row
                        //
                        vv = bufr[i];
                        for (j = idx + 1; j <= i - 1; j++)
                        {
                            cs = bufr[n + 2 * j + 0];
                            sn = bufr[n + 2 * j + 1];
                            v = a[i, j];
                            a[i, j] = cs * v + sn * vv;
                            vv = -(sn * v) + cs * vv;
                        }

                        //
                        // generate rotation applied to I-th element of update vector
                        //
                        rotations.generaterotation(a[i, i], vv, ref cs, ref sn, ref v, _params);
                        a[i, i] = v;
                        bufr[n + 2 * i + 0] = cs;
                        bufr[n + 2 * i + 1] = sn;
                    }
                }
            }
        }
    }


    /*************************************************************************
    Sparse LU decomposition with column pivoting for sparsity and row pivoting
    for stability. Input must be square sparse matrix stored in CRS format.

    The algorithm  computes  LU  decomposition  of  a  general  square  matrix
    (rectangular ones are not supported). The result  of  an  algorithm  is  a
    representation of A as A = P*L*U*Q, where:
    * L is lower unitriangular matrix
    * U is upper triangular matrix
    * P = P0*P1*...*PK, K=N-1, Pi - permutation matrix for I and P[I]
    * Q = QK*...*Q1*Q0, K=N-1, Qi - permutation matrix for I and Q[I]
        
    This function pivots columns for higher sparsity, and then pivots rows for
    stability (larger element at the diagonal).

    INPUT PARAMETERS:
        A       -   sparse NxN matrix in CRS format. An exception is generated
                    if matrix is non-CRS or non-square.
        PivotType-  pivoting strategy:
                    * 0 for best pivoting available (2 in current version)
                    * 1 for row-only pivoting (NOT RECOMMENDED)
                    * 2 for complete pivoting which produces most sparse outputs

    OUTPUT PARAMETERS:
        A       -   the result of factorization, matrices L and U stored in
                    compact form using CRS sparse storage format:
                    * lower unitriangular L is stored strictly under main diagonal
                    * upper triangilar U is stored ON and ABOVE main diagonal
        P       -   row permutation matrix in compact form, array[N]
        Q       -   col permutation matrix in compact form, array[N]
        
    This function always succeeds, i.e. it ALWAYS returns valid factorization,
    but for your convenience it also returns  boolean  value  which  helps  to
    detect symbolically degenerate matrices:
    * function returns TRUE, if the matrix was factorized AND symbolically
      non-degenerate
    * function returns FALSE, if the matrix was factorized but U has strictly
      zero elements at the diagonal (the factorization is returned anyway).


      -- ALGLIB routine --
         03.09.2018
         Bochkanov Sergey
    *************************************************************************/
    public static bool sparselu(sparse.sparsematrix a,
        int pivottype,
        ref int[] p,
        ref int[] q,
        xparams _params)
    {
        bool result = new bool();
        sptrf.sluv2buffer buf2 = new sptrf.sluv2buffer();

        p = new int[0];
        q = new int[0];

        ap.assert((pivottype == 0 || pivottype == 1) || pivottype == 2, "SparseLU: unexpected pivot type");
        ap.assert(sparse.sparseiscrs(a, _params), "SparseLU: A is not stored in CRS format");
        ap.assert(sparse.sparsegetnrows(a, _params) == sparse.sparsegetncols(a, _params), "SparseLU: non-square A");
        result = sptrf.sptrflu(a, pivottype, ref p, ref q, buf2, _params);
        return result;
    }


    /*************************************************************************
    Sparse Cholesky decomposition for skyline matrixm using in-place algorithm
    without allocating additional storage.

    The algorithm computes Cholesky decomposition  of  a  symmetric  positive-
    definite sparse matrix. The result of an algorithm is a representation  of
    A as A=U^T*U or A=L*L^T

    This function allows to perform very efficient decomposition of low-profile
    matrices (average bandwidth is ~5-10 elements). For larger matrices it  is
    recommended to use supernodal Cholesky decomposition: SparseCholeskyP() or
    SparseCholeskyAnalyze()/SparseCholeskyFactorize().

    INPUT PARAMETERS:
        A       -   sparse matrix in skyline storage (SKS) format.
        N       -   size of matrix A (can be smaller than actual size of A)
        IsUpper -   if IsUpper=True, then factorization is performed on  upper
                    triangle. Another triangle is ignored (it may contant some
                    data, but it is not changed).
        

    OUTPUT PARAMETERS:
        A       -   the result of factorization, stored in SKS. If IsUpper=True,
                    then the upper  triangle  contains  matrix  U,  such  that
                    A = U^T*U. Lower triangle is not changed.
                    Similarly, if IsUpper = False. In this case L is returned,
                    and we have A = L*(L^T).
                    Note that THIS function does not  perform  permutation  of
                    rows to reduce bandwidth.

    RESULT:
        If  the  matrix  is  positive-definite,  the  function  returns  True.
        Otherwise, the function returns False. Contents of A is not determined
        in such case.

    NOTE: for  performance  reasons  this  function  does NOT check that input
          matrix  includes  only  finite  values. It is your responsibility to
          make sure that there are no infinite or NAN values in the matrix.

      -- ALGLIB routine --
         16.01.2014
         Bochkanov Sergey
    *************************************************************************/
    public static bool sparsecholeskyskyline(sparse.sparsematrix a,
        int n,
        bool isupper,
        xparams _params)
    {
        bool result = new bool();
        int i = 0;
        int j = 0;
        int k = 0;
        int jnz = 0;
        int jnza = 0;
        int jnzl = 0;
        double v = 0;
        double vv = 0;
        double a12 = 0;
        int nready = 0;
        int nadd = 0;
        int banda = 0;
        int offsa = 0;
        int offsl = 0;

        ap.assert(n >= 0, "SparseCholeskySkyline: N<0");
        ap.assert(sparse.sparsegetnrows(a, _params) >= n, "SparseCholeskySkyline: rows(A)<N");
        ap.assert(sparse.sparsegetncols(a, _params) >= n, "SparseCholeskySkyline: cols(A)<N");
        ap.assert(sparse.sparseissks(a, _params), "SparseCholeskySkyline: A is not stored in SKS format");
        result = false;

        //
        // transpose if needed
        //
        if (isupper)
        {
            sparse.sparsetransposesks(a, _params);
        }

        //
        // Perform Cholesky decomposition:
        // * we assume than leading NReady*NReady submatrix is done
        // * having Cholesky decomposition of NReady*NReady submatrix we
        //   obtain decomposition of larger (NReady+NAdd)*(NReady+NAdd) one.
        //
        // Here is algorithm. At the start we have
        //
        //     (      |   )
        //     (  L   |   )
        // S = (      |   )
        //     (----------)
        //     (  A   | B )
        //
        // with L being already computed Cholesky factor, A and B being
        // unprocessed parts of the matrix. Of course, L/A/B are stored
        // in SKS format.
        //
        // Then, we calculate A1:=(inv(L)*A')' and replace A with A1.
        // Then, we calculate B1:=B-A1*A1'     and replace B with B1
        //
        // Finally, we calculate small NAdd*NAdd Cholesky of B1 with
        // dense solver. Now, L/A1/B1 are Cholesky decomposition of the
        // larger (NReady+NAdd)*(NReady+NAdd) matrix.
        //
        nready = 0;
        nadd = 1;
        while (nready < n)
        {
            ap.assert(nadd == 1, "SkylineCholesky: internal error");

            //
            // Calculate A1:=(inv(L)*A')'
            //
            // Elements are calculated row by row (example below is given
            // for NAdd=1):
            // * first, we solve L[0,0]*A1[0]=A[0]
            // * then, we solve  L[1,0]*A1[0]+L[1,1]*A1[1]=A[1]
            // * then, we move to next row and so on
            // * during calculation of A1 we update A12 - squared norm of A1
            //
            // We extensively use sparsity of both A/A1 and L:
            // * first, equations from 0 to BANDWIDTH(A1)-1 are completely zero
            // * second, for I>=BANDWIDTH(A1), I-th equation is reduced from
            //     L[I,0]*A1[0] + L[I,1]*A1[1] + ... + L[I,I]*A1[I] = A[I]
            //   to
            //     L[I,JNZ]*A1[JNZ] + ... + L[I,I]*A1[I] = A[I]
            //   where JNZ = max(NReady-BANDWIDTH(A1),I-BANDWIDTH(L[i]))
            //   (JNZ is an index of the firts column where both A and L become
            //   nonzero).
            //
            // NOTE: we rely on details of SparseMatrix internal storage format.
            //       This is allowed by SparseMatrix specification.
            //
            a12 = 0.0;
            if (a.didx[nready] > 0)
            {
                banda = a.didx[nready];
                for (i = nready - banda; i <= nready - 1; i++)
                {

                    //
                    // Elements of A1[0:I-1] were computed:
                    // * A1[0:NReady-BandA-1] are zero (sparse)
                    // * A1[NReady-BandA:I-1] replaced corresponding elements of A
                    //
                    // Now it is time to get I-th one.
                    //
                    // First, we calculate:
                    // * JNZA  - index of the first column where A become nonzero
                    // * JNZL  - index of the first column where L become nonzero
                    // * JNZ   - index of the first column where both A and L become nonzero
                    // * OffsA - offset of A[JNZ] in A.Vals
                    // * OffsL - offset of L[I,JNZ] in A.Vals
                    //
                    // Then, we solve SUM(A1[j]*L[I,j],j=JNZ..I-1) + A1[I]*L[I,I] = A[I],
                    // with A1[JNZ..I-1] already known, and A1[I] unknown.
                    //
                    jnza = nready - banda;
                    jnzl = i - a.didx[i];
                    jnz = Math.Max(jnza, jnzl);
                    offsa = a.ridx[nready] + (jnz - jnza);
                    offsl = a.ridx[i] + (jnz - jnzl);
                    v = 0.0;
                    k = i - 1 - jnz;
                    for (j = 0; j <= k; j++)
                    {
                        v = v + a.vals[offsa + j] * a.vals[offsl + j];
                    }
                    vv = (a.vals[offsa + k + 1] - v) / a.vals[offsl + k + 1];
                    a.vals[offsa + k + 1] = vv;
                    a12 = a12 + vv * vv;
                }
            }

            //
            // Calculate CHOLESKY(B-A1*A1')
            //
            offsa = a.ridx[nready] + a.didx[nready];
            v = a.vals[offsa];
            if ((double)(v) <= (double)(a12))
            {
                result = false;
                return result;
            }
            a.vals[offsa] = Math.Sqrt(v - a12);

            //
            // Increase size of the updated matrix
            //
            apserv.inc(ref nready, _params);
        }

        //
        // transpose if needed
        //
        if (isupper)
        {
            sparse.sparsetransposesks(a, _params);
        }
        result = true;
        return result;
    }


    /*************************************************************************
    Sparse Cholesky decomposition for a matrix  stored  in  any sparse storage,
    without rows/cols permutation.

    This function is the most convenient (less parameters to specify), although
    less efficient, version of sparse Cholesky.

    Internally it:
    * calls SparseCholeskyAnalyze()  function  to  perform  symbolic  analysis
      phase with no permutation being configured.
    * calls SparseCholeskyFactorize() function to perform numerical  phase  of
      the factorization

    Following alternatives may result in better performance:
    * using SparseCholeskyP(), which selects best  pivoting  available,  which
      almost always results in improved sparsity and cache locality
    * using  SparseCholeskyAnalyze() and SparseCholeskyFactorize()   functions
      directly,  which  may  improve  performance of repetitive factorizations
      with same sparsity patterns.

    The latter also allows one to perform  LDLT  factorization  of  indefinite
    matrix (one with strictly diagonal D, which is known  to  be  stable  only
    in few special cases, like quasi-definite matrices).

    INPUT PARAMETERS:
        A       -   a square NxN sparse matrix, stored in any storage format.
        IsUpper -   if IsUpper=True, then factorization is performed on  upper
                    triangle.  Another triangle is ignored on  input,  dropped
                    on output. Similarly, if IsUpper=False, the lower triangle
                    is processed.

    OUTPUT PARAMETERS:
        A       -   the result of factorization, stored in CRS format:
                    * if IsUpper=True, then the upper triangle contains matrix
                      U such  that  A = U^T*U and the lower triangle is empty.
                    * similarly, if IsUpper=False, then lower triangular L  is
                      returned and we have A = L*(L^T).
                    Note that THIS function does not  perform  permutation  of
                    the rows to reduce fill-in.

    RESULT:
        If  the  matrix  is  positive-definite,  the  function  returns  True.
        Otherwise, the function returns False.  Contents  of  A  is  undefined
        in such case.

    NOTE: for  performance  reasons  this  function  does NOT check that input
          matrix  includes  only  finite  values. It is your responsibility to
          make sure that there are no infinite or NAN values in the matrix.

      -- ALGLIB routine --
         16.09.2020
         Bochkanov Sergey
    *************************************************************************/
    public static bool sparsecholesky(sparse.sparsematrix a,
        bool isupper,
        xparams _params)
    {
        bool result = new bool();
        sparsedecompositionanalysis analysis = new sparsedecompositionanalysis();
        int facttype = 0;
        int permtype = 0;
        int[] priorities = new int[0];
        double[] dummyd = new double[0];
        int[] dummyp = new int[0];

        ap.assert(sparse.sparsegetnrows(a, _params) == sparse.sparsegetncols(a, _params), "SparseCholesky: A is not square");

        //
        // Quick exit
        //
        if (sparse.sparsegetnrows(a, _params) == 0)
        {
            result = true;
            return result;
        }

        //
        // Choose factorization and permutation: vanilla Cholesky and no permutation,
        // Priorities[] array is not set.
        //
        facttype = 0;
        permtype = -1;

        //
        // Easy case - CRS matrix in lower triangle, no conversion or transposition is needed
        //
        if (sparse.sparseiscrs(a, _params) && !isupper)
        {
            result = spchol.spsymmanalyze(a, priorities, 0.0, facttype, permtype, analysis.analysis, _params);
            if (!result)
            {
                return result;
            }
            result = spchol.spsymmfactorize(analysis.analysis, _params);
            if (!result)
            {
                return result;
            }
            spchol.spsymmextract(analysis.analysis, a, ref dummyd, ref dummyp, _params);
            return result;
        }

        //
        // A bit more complex - we need conversion and/or transposition
        //
        if (isupper)
        {
            sparse.sparsecopytocrsbuf(a, analysis.wrkat, _params);
            sparse.sparsecopytransposecrsbuf(analysis.wrkat, analysis.wrka, _params);
        }
        else
        {
            sparse.sparsecopytocrsbuf(a, analysis.wrka, _params);
        }
        result = spchol.spsymmanalyze(analysis.wrka, priorities, 0.0, facttype, permtype, analysis.analysis, _params);
        if (!result)
        {
            return result;
        }
        result = spchol.spsymmfactorize(analysis.analysis, _params);
        if (!result)
        {
            return result;
        }
        spchol.spsymmextract(analysis.analysis, analysis.wrka, ref dummyd, ref dummyp, _params);
        if (isupper)
        {
            sparse.sparsecopytransposecrsbuf(analysis.wrka, a, _params);
        }
        else
        {
            sparse.sparsecopybuf(analysis.wrka, a, _params);
        }
        return result;
    }


    /*************************************************************************
    Sparse Cholesky decomposition for a matrix  stored  in  any sparse storage
    format, with performance-enhancing permutation of rows/cols.

    Present version is configured  to  perform  supernodal  permutation  which
    sparsity reducing ordering.

    This function is a wrapper around generic sparse  decomposition  functions
    that internally:
    * calls SparseCholeskyAnalyze()  function  to  perform  symbolic  analysis
      phase with best available permutation being configured.
    * calls SparseCholeskyFactorize() function to perform numerical  phase  of
      the factorization.

    NOTE: using  SparseCholeskyAnalyze() and SparseCholeskyFactorize() directly
          may improve  performance  of  repetitive  factorizations  with  same
          sparsity patterns. It also allows one to perform  LDLT factorization
          of  indefinite  matrix  -  a factorization with strictly diagonal D,
          which  is  known to be stable only in few special cases, like quasi-
          definite matrices.

    INPUT PARAMETERS:
        A       -   a square NxN sparse matrix, stored in any storage format.
        IsUpper -   if IsUpper=True, then factorization is performed on  upper
                    triangle.  Another triangle is ignored on  input,  dropped
                    on output. Similarly, if IsUpper=False, the lower triangle
                    is processed.

    OUTPUT PARAMETERS:
        A       -   the result of factorization, stored in CRS format:
                    * if IsUpper=True, then the upper triangle contains matrix
                      U such  that  A = U^T*U and the lower triangle is empty.
                    * similarly, if IsUpper=False, then lower triangular L  is
                      returned and we have A = L*(L^T).
        P       -   a row/column permutation, a product of P0*P1*...*Pk, k=N-1,
                    with Pi being permutation of rows/cols I and P[I]

    RESULT:
        If  the  matrix  is  positive-definite,  the  function  returns  True.
        Otherwise, the function returns False.  Contents  of  A  is  undefined
        in such case.

    NOTE: for  performance  reasons  this  function  does NOT check that input
          matrix  includes  only  finite  values. It is your responsibility to
          make sure that there are no infinite or NAN values in the matrix.

      -- ALGLIB routine --
         16.09.2020
         Bochkanov Sergey
    *************************************************************************/
    public static bool sparsecholeskyp(sparse.sparsematrix a,
        bool isupper,
        ref int[] p,
        xparams _params)
    {
        bool result = new bool();
        sparsedecompositionanalysis analysis = new sparsedecompositionanalysis();
        double[] dummyd = new double[0];
        int facttype = 0;
        int permtype = 0;
        int[] priorities = new int[0];

        p = new int[0];

        ap.assert(sparse.sparsegetnrows(a, _params) == sparse.sparsegetncols(a, _params), "SparseCholeskyP: A is not square");

        //
        // Quick exit
        //
        if (sparse.sparsegetnrows(a, _params) == 0)
        {
            result = true;
            return result;
        }

        //
        // Choose factorization and permutation: vanilla Cholesky and best permutation available.
        // Priorities[] array is not set.
        //
        facttype = 0;
        permtype = 0;

        //
        // Easy case - CRS matrix in lower triangle, no conversion or transposition is needed
        //
        if (sparse.sparseiscrs(a, _params) && !isupper)
        {
            result = spchol.spsymmanalyze(a, priorities, 0.0, facttype, permtype, analysis.analysis, _params);
            if (!result)
            {
                return result;
            }
            result = spchol.spsymmfactorize(analysis.analysis, _params);
            if (!result)
            {
                return result;
            }
            spchol.spsymmextract(analysis.analysis, a, ref dummyd, ref p, _params);
            return result;
        }

        //
        // A bit more complex - we need conversion and/or transposition
        //
        if (isupper)
        {
            sparse.sparsecopytocrsbuf(a, analysis.wrkat, _params);
            sparse.sparsecopytransposecrsbuf(analysis.wrkat, analysis.wrka, _params);
        }
        else
        {
            sparse.sparsecopytocrsbuf(a, analysis.wrka, _params);
        }
        result = spchol.spsymmanalyze(analysis.wrka, priorities, 0.0, facttype, permtype, analysis.analysis, _params);
        if (!result)
        {
            return result;
        }
        result = spchol.spsymmfactorize(analysis.analysis, _params);
        if (!result)
        {
            return result;
        }
        spchol.spsymmextract(analysis.analysis, analysis.wrka, ref dummyd, ref p, _params);
        if (isupper)
        {
            sparse.sparsecopytransposecrsbuf(analysis.wrka, a, _params);
        }
        else
        {
            sparse.sparsecopybuf(analysis.wrka, a, _params);
        }
        return result;
    }


    /*************************************************************************
    Sparse Cholesky/LDLT decomposition: symbolic analysis phase.

    This function is a part of the 'expert' sparse Cholesky API:
    * SparseCholeskyAnalyze(), that performs symbolic analysis phase and loads
      matrix to be factorized into internal storage
    * SparseCholeskySetModType(), that allows to  use  modified  Cholesky/LDLT
      with lower bounds on pivot magnitudes and additional overflow safeguards
    * SparseCholeskyFactorize(),  that performs  numeric  factorization  using
      precomputed symbolic analysis and internally stored matrix - and outputs
      result
    * SparseCholeskyReload(), that reloads one more matrix with same  sparsity
      pattern into internal storage so  one  may  reuse  previously  allocated
      temporaries and previously performed symbolic analysis

    This specific function performs preliminary analysis of the  Cholesky/LDLT
    factorization. It allows to choose  different  permutation  types  and  to
    choose between classic Cholesky and  indefinite  LDLT  factorization  (the
    latter is computed with strictly diagonal D, i.e.  without  Bunch-Kauffman
    pivoting).

    NOTE: L*D*LT family of factorization may be used to  factorize  indefinite
          matrices. However, numerical stability is guaranteed ONLY for a class
          of quasi-definite matrices.

    NOTE: all internal processing is performed with lower triangular  matrices
          stored  in  CRS  format.  Any  other  storage  formats  and/or upper
          triangular storage means  that  one  format  conversion  and/or  one
          transposition will be performed  internally  for  the  analysis  and
          factorization phases. Thus, highest  performance  is  achieved  when
          input is a lower triangular CRS matrix.

    INPUT PARAMETERS:
        A           -   sparse square matrix in any sparse storage format.
        IsUpper     -   whether upper or lower  triangle  is  decomposed  (the
                        other one is ignored).
        FactType    -   factorization type:
                        * 0 for traditional Cholesky of SPD matrix
                        * 1 for LDLT decomposition with strictly  diagonal  D,
                            which may have non-positive entries.
        PermType    -   permutation type:
                        *-1 for absence of permutation
                        * 0 for best fill-in reducing  permutation  available,
                            which is 3 in the current version
                        * 1 for supernodal ordering (improves locality and
                          performance, does NOT change fill-in factor)
                        * 2 for original AMD ordering
                        * 3 for  improved  AMD  (approximate  minimum  degree)
                            ordering with better  handling  of  matrices  with
                            dense rows/columns

    OUTPUT PARAMETERS:
        Analysis    -   contains:
                        * symbolic analysis of the matrix structure which will
                          be used later to guide numerical factorization.
                        * specific numeric values loaded into internal  memory
                          waiting for the factorization to be performed

    This function fails if and only if the matrix A is symbolically degenerate
    i.e. has diagonal element which is exactly zero. In  such  case  False  is
    returned, contents of Analysis object is undefined.

      -- ALGLIB routine --
         20.09.2020
         Bochkanov Sergey
    *************************************************************************/
    public static bool sparsecholeskyanalyze(sparse.sparsematrix a,
        bool isupper,
        int facttype,
        int permtype,
        sparsedecompositionanalysis analysis,
        xparams _params)
    {
        bool result = new bool();
        int[] priorities = new int[0];

        ap.assert(sparse.sparsegetnrows(a, _params) == sparse.sparsegetncols(a, _params), "SparseCholeskyAnalyze: A is not square");
        ap.assert(facttype == 0 || facttype == 1, "SparseCholeskyAnalyze: unexpected FactType");
        ap.assert((((((permtype == 0 || permtype == 1) || permtype == 2) || permtype == 3) || permtype == -1) || permtype == -2) || permtype == -3, "SparseCholeskyAnalyze: unexpected PermType");

        //
        // Prepare wrapper object
        //
        analysis.n = sparse.sparsegetnrows(a, _params);
        analysis.facttype = facttype;
        analysis.permtype = permtype;

        //
        // Prepare default priorities for the priority ordering
        //
        if (permtype == -3 || permtype == 3)
        {
            ablasf.isetallocv(analysis.n, 0, ref priorities, _params);
        }

        //
        // Analyse
        //
        if (!sparse.sparseiscrs(a, _params))
        {

            //
            // The matrix is stored in non-CRS format. First, we have to convert
            // it to CRS. Then we may need to transpose it in order to get lower
            // triangular one (as supported by SPSymmAnalyze).
            //
            sparse.sparsecopytocrs(a, analysis.crsa, _params);
            if (isupper)
            {
                sparse.sparsecopytransposecrsbuf(analysis.crsa, analysis.crsat, _params);
                result = spchol.spsymmanalyze(analysis.crsat, priorities, 0.0, facttype, permtype, analysis.analysis, _params);
            }
            else
            {
                result = spchol.spsymmanalyze(analysis.crsa, priorities, 0.0, facttype, permtype, analysis.analysis, _params);
            }
        }
        else
        {

            //
            // The matrix is stored in CRS format. However we may need to
            // transpose it in order to get lower triangular one (as supported
            // by SPSymmAnalyze).
            //
            if (isupper)
            {
                sparse.sparsecopytransposecrsbuf(a, analysis.crsat, _params);
                result = spchol.spsymmanalyze(analysis.crsat, priorities, 0.0, facttype, permtype, analysis.analysis, _params);
            }
            else
            {
                result = spchol.spsymmanalyze(a, priorities, 0.0, facttype, permtype, analysis.analysis, _params);
            }
        }
        return result;
    }


    /*************************************************************************
    Allows to control stability-improving  modification  strategy  for  sparse
    Cholesky/LDLT decompositions. Modified Cholesky is more  robust  than  its
    unmodified counterpart.

    This function is a part of the 'expert' sparse Cholesky API:
    * SparseCholeskyAnalyze(), that performs symbolic analysis phase and loads
      matrix to be factorized into internal storage
    * SparseCholeskySetModType(), that allows to  use  modified  Cholesky/LDLT
      with lower bounds on pivot magnitudes and additional overflow safeguards
    * SparseCholeskyFactorize(),  that performs  numeric  factorization  using
      precomputed symbolic analysis and internally stored matrix - and outputs
      result
    * SparseCholeskyReload(), that reloads one more matrix with same  sparsity
      pattern into internal storage so  one  may  reuse  previously  allocated
      temporaries and previously performed symbolic analysis

    INPUT PARAMETERS:
        Analysis    -   symbolic analysis of the matrix structure
        ModStrategy -   modification type:
                        * 0 for traditional Cholesky/LDLT (Cholesky fails when
                          encounters nonpositive pivot, LDLT fails  when  zero
                          pivot   is  encountered,  no  stability  checks  for
                          overflows/underflows)
                        * 1 for modified Cholesky with additional checks:
                          * pivots less than ModParam0 are increased; (similar
                            sign-preserving procedure is applied during LDLT)
                          * if,  at  some  moment,  sum  of absolute values of
                            elements in column  J  will  become  greater  than
                            ModParam1, Cholesky/LDLT will treat it as  failure
                            and will stop immediately
        P0, P1, P2,P3 - modification parameters #0 #1, #2 and #3.
                        Params #2 and #3 are ignored in current version.

    OUTPUT PARAMETERS:
        Analysis    -   symbolic analysis of the matrix structure, new strategy
                        Results will be seen with next SparseCholeskyFactorize()
                        call.

      -- ALGLIB routine --
         20.09.2020
         Bochkanov Sergey
    *************************************************************************/
    public static void sparsecholeskysetmodtype(sparsedecompositionanalysis analysis,
        int modstrategy,
        double p0,
        double p1,
        double p2,
        double p3,
        xparams _params)
    {
        spchol.spsymmsetmodificationstrategy(analysis.analysis, modstrategy, p0, p1, p2, p3, _params);
    }


    /*************************************************************************
    Sparse Cholesky decomposition: numerical analysis phase.

    This function is a part of the 'expert' sparse Cholesky API:
    * SparseCholeskyAnalyze(), that performs symbolic analysis phase and loads
      matrix to be factorized into internal storage
    * SparseCholeskySetModType(), that allows to  use  modified  Cholesky/LDLT
      with lower bounds on pivot magnitudes and additional overflow safeguards
    * SparseCholeskyFactorize(),  that performs  numeric  factorization  using
      precomputed symbolic analysis and internally stored matrix - and outputs
      result
    * SparseCholeskyReload(), that reloads one more matrix with same  sparsity
      pattern into internal storage so  one  may  reuse  previously  allocated
      temporaries and previously performed symbolic analysis

    Depending on settings specified during SparseCholeskyAnalyze() call it may
    produce classic Cholesky or L*D*LT  decomposition  (with strictly diagonal
    D), without permutation or with performance-enhancing permutation P.

    NOTE: all internal processing is performed with lower triangular  matrices
          stored  in  CRS  format.  Any  other  storage  formats  and/or upper
          triangular storage means  that  one  format  conversion  and/or  one
          transposition will be performed  internally  for  the  analysis  and
          factorization phases. Thus, highest  performance  is  achieved  when
          input is a lower triangular CRS matrix, and lower triangular  output
          is requested.

    NOTE: L*D*LT family of factorization may be used to  factorize  indefinite
          matrices. However, numerical stability is guaranteed ONLY for a class
          of quasi-definite matrices.

    INPUT PARAMETERS:
        Analysis    -   prior analysis with internally stored matrix which will
                        be factorized
        NeedUpper   -   whether upper triangular or lower triangular output is
                        needed

    OUTPUT PARAMETERS:
        A           -   Cholesky decomposition of A stored in lower triangular
                        CRS format, i.e. A=L*L' (or upper triangular CRS, with
                        A=U'*U, depending on NeedUpper parameter).
        D           -   array[N], diagonal factor. If no diagonal  factor  was
                        required during analysis  phase,  still  returned  but
                        filled with 1's
        P           -   array[N], pivots. Permutation matrix P is a product of
                        P(0)*P(1)*...*P(N-1), where P(i) is a  permutation  of
                        row/col I and P[I] (with P[I]>=I).
                        If no permutation was requested during analysis phase,
                        still returned but filled with identity permutation.
        
    The function returns True  when  factorization  resulted  in nondegenerate
    matrix. False is returned when factorization fails (Cholesky factorization
    of indefinite matrix) or LDLT factorization has exactly zero  elements  at
    the diagonal. In the latter case contents of A, D and P is undefined.

    The analysis object is not changed during  the  factorization.  Subsequent
    calls to SparseCholeskyFactorize() will result in same factorization being
    performed one more time.

      -- ALGLIB routine --
         20.09.2020
         Bochkanov Sergey
    *************************************************************************/
    public static bool sparsecholeskyfactorize(sparsedecompositionanalysis analysis,
        bool needupper,
        sparse.sparsematrix a,
        ref double[] d,
        ref int[] p,
        xparams _params)
    {
        bool result = new bool();

        d = new double[0];
        p = new int[0];

        if (needupper)
        {
            result = spchol.spsymmfactorize(analysis.analysis, _params);
            if (!result)
            {
                return result;
            }
            spchol.spsymmextract(analysis.analysis, analysis.wrka, ref d, ref p, _params);
            sparse.sparsecopytransposecrsbuf(analysis.wrka, a, _params);
        }
        else
        {
            result = spchol.spsymmfactorize(analysis.analysis, _params);
            if (!result)
            {
                return result;
            }
            spchol.spsymmextract(analysis.analysis, a, ref d, ref p, _params);
        }
        return result;
    }


    /*************************************************************************
    Sparse  Cholesky  decomposition:  update  internally  stored  matrix  with
    another one with exactly same sparsity pattern.

    This function is a part of the 'expert' sparse Cholesky API:
    * SparseCholeskyAnalyze(), that performs symbolic analysis phase and loads
      matrix to be factorized into internal storage
    * SparseCholeskySetModType(), that allows to  use  modified  Cholesky/LDLT
      with lower bounds on pivot magnitudes and additional overflow safeguards
    * SparseCholeskyFactorize(),  that performs  numeric  factorization  using
      precomputed symbolic analysis and internally stored matrix - and outputs
      result
    * SparseCholeskyReload(), that reloads one more matrix with same  sparsity
      pattern into internal storage so  one  may  reuse  previously  allocated
      temporaries and previously performed symbolic analysis

    This specific function replaces internally stored  numerical  values  with
    ones from another sparse matrix (but having exactly same sparsity  pattern
    as one that was used for initial SparseCholeskyAnalyze() call).

    NOTE: all internal processing is performed with lower triangular  matrices
          stored  in  CRS  format.  Any  other  storage  formats  and/or upper
          triangular storage means  that  one  format  conversion  and/or  one
          transposition will be performed  internally  for  the  analysis  and
          factorization phases. Thus, highest  performance  is  achieved  when
          input is a lower triangular CRS matrix.

    INPUT PARAMETERS:
        Analysis    -   analysis object
        A           -   sparse square matrix in any sparse storage format.  It
                        MUST have exactly same sparsity pattern as that of the
                        matrix that was passed to SparseCholeskyAnalyze().
                        Any difference (missing elements or additional elements)
                        may result in unpredictable and undefined  behavior  -
                        an algorithm may fail due to memory access violation.
        IsUpper     -   whether upper or lower  triangle  is  decomposed  (the
                        other one is ignored).

    OUTPUT PARAMETERS:
        Analysis    -   contains:
                        * symbolic analysis of the matrix structure which will
                          be used later to guide numerical factorization.
                        * specific numeric values loaded into internal  memory
                          waiting for the factorization to be performed

      -- ALGLIB routine --
         20.09.2020
         Bochkanov Sergey
    *************************************************************************/
    public static void sparsecholeskyreload(sparsedecompositionanalysis analysis,
        sparse.sparsematrix a,
        bool isupper,
        xparams _params)
    {
        ap.assert(sparse.sparsegetnrows(a, _params) == sparse.sparsegetncols(a, _params), "SparseCholeskyReload: A is not square");
        ap.assert(sparse.sparsegetnrows(a, _params) == analysis.n, "SparseCholeskyReload: size of A does not match that stored in Analysis");
        if (!sparse.sparseiscrs(a, _params))
        {

            //
            // The matrix is stored in non-CRS format. First, we have to convert
            // it to CRS. Then we may need to transpose it in order to get lower
            // triangular one (as supported by SPSymmAnalyze).
            //
            sparse.sparsecopytocrs(a, analysis.crsa, _params);
            if (isupper)
            {
                sparse.sparsecopytransposecrsbuf(analysis.crsa, analysis.crsat, _params);
                spchol.spsymmreload(analysis.analysis, analysis.crsat, _params);
            }
            else
            {
                spchol.spsymmreload(analysis.analysis, analysis.crsa, _params);
            }
        }
        else
        {

            //
            // The matrix is stored in CRS format. However we may need to
            // transpose it in order to get lower triangular one (as supported
            // by SPSymmAnalyze).
            //
            if (isupper)
            {
                sparse.sparsecopytransposecrsbuf(a, analysis.crsat, _params);
                spchol.spsymmreload(analysis.analysis, analysis.crsat, _params);
            }
            else
            {
                spchol.spsymmreload(analysis.analysis, a, _params);
            }
        }
    }


    public static void rmatrixlup(ref double[,] a,
        int m,
        int n,
        ref int[] pivots,
        xparams _params)
    {
        double[] tmp = new double[0];
        int i = 0;
        int j = 0;
        double mx = 0;
        double v = 0;
        int i_ = 0;

        pivots = new int[0];


        //
        // Internal LU decomposition subroutine.
        // Never call it directly.
        //
        ap.assert(m > 0, "RMatrixLUP: incorrect M!");
        ap.assert(n > 0, "RMatrixLUP: incorrect N!");

        //
        // Scale matrix to avoid overflows,
        // decompose it, then scale back.
        //
        mx = 0;
        for (i = 0; i <= m - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                mx = Math.Max(mx, Math.Abs(a[i, j]));
            }
        }
        if ((double)(mx) != (double)(0))
        {
            v = 1 / mx;
            for (i = 0; i <= m - 1; i++)
            {
                for (i_ = 0; i_ <= n - 1; i_++)
                {
                    a[i, i_] = v * a[i, i_];
                }
            }
        }
        pivots = new int[Math.Min(m, n)];
        tmp = new double[2 * Math.Max(m, n)];
        dlu.rmatrixluprec(ref a, 0, m, n, ref pivots, ref tmp, _params);
        if ((double)(mx) != (double)(0))
        {
            v = mx;
            for (i = 0; i <= m - 1; i++)
            {
                for (i_ = 0; i_ <= Math.Min(i, n - 1); i_++)
                {
                    a[i, i_] = v * a[i, i_];
                }
            }
        }
    }


    public static void cmatrixlup(ref complex[,] a,
        int m,
        int n,
        ref int[] pivots,
        xparams _params)
    {
        complex[] tmp = new complex[0];
        int i = 0;
        int j = 0;
        double mx = 0;
        double v = 0;
        int i_ = 0;

        pivots = new int[0];


        //
        // Internal LU decomposition subroutine.
        // Never call it directly.
        //
        ap.assert(m > 0, "CMatrixLUP: incorrect M!");
        ap.assert(n > 0, "CMatrixLUP: incorrect N!");

        //
        // Scale matrix to avoid overflows,
        // decompose it, then scale back.
        //
        mx = 0;
        for (i = 0; i <= m - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                mx = Math.Max(mx, math.abscomplex(a[i, j]));
            }
        }
        if ((double)(mx) != (double)(0))
        {
            v = 1 / mx;
            for (i = 0; i <= m - 1; i++)
            {
                for (i_ = 0; i_ <= n - 1; i_++)
                {
                    a[i, i_] = v * a[i, i_];
                }
            }
        }
        pivots = new int[Math.Min(m, n)];
        tmp = new complex[2 * Math.Max(m, n)];
        dlu.cmatrixluprec(ref a, 0, m, n, ref pivots, ref tmp, _params);
        if ((double)(mx) != (double)(0))
        {
            v = mx;
            for (i = 0; i <= m - 1; i++)
            {
                for (i_ = 0; i_ <= Math.Min(i, n - 1); i_++)
                {
                    a[i, i_] = v * a[i, i_];
                }
            }
        }
    }


    public static void rmatrixplu(double[,] a,
        int m,
        int n,
        ref int[] pivots,
        xparams _params)
    {
        double[] tmp = new double[0];
        int i = 0;
        int j = 0;
        double mx = 0;
        double v = 0;
        int i_ = 0;

        pivots = new int[0];


        //
        // Internal LU decomposition subroutine.
        // Never call it directly.
        //
        ap.assert(m > 0, "RMatrixPLU: incorrect M!");
        ap.assert(n > 0, "RMatrixPLU: incorrect N!");
        tmp = new double[2 * Math.Max(m, n)];
        pivots = new int[Math.Min(m, n)];

        //
        // Scale matrix to avoid overflows,
        // decompose it, then scale back.
        //
        mx = 0;
        for (i = 0; i <= m - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                mx = Math.Max(mx, Math.Abs(a[i, j]));
            }
        }
        if ((double)(mx) != (double)(0))
        {
            v = 1 / mx;
            for (i = 0; i <= m - 1; i++)
            {
                for (i_ = 0; i_ <= n - 1; i_++)
                {
                    a[i, i_] = v * a[i, i_];
                }
            }
        }
        dlu.rmatrixplurec(a, 0, m, n, ref pivots, ref tmp, _params);
        if ((double)(mx) != (double)(0))
        {
            v = mx;
            for (i = 0; i <= Math.Min(m, n) - 1; i++)
            {
                for (i_ = i; i_ <= n - 1; i_++)
                {
                    a[i, i_] = v * a[i, i_];
                }
            }
        }
    }


    public static void cmatrixplu(complex[,] a,
        int m,
        int n,
        ref int[] pivots,
        xparams _params)
    {
        complex[] tmp = new complex[0];
        int i = 0;
        int j = 0;
        double mx = 0;
        complex v = 0;
        int i_ = 0;

        pivots = new int[0];


        //
        // Internal LU decomposition subroutine.
        // Never call it directly.
        //
        ap.assert(m > 0, "CMatrixPLU: incorrect M!");
        ap.assert(n > 0, "CMatrixPLU: incorrect N!");
        tmp = new complex[2 * Math.Max(m, n)];
        pivots = new int[Math.Min(m, n)];

        //
        // Scale matrix to avoid overflows,
        // decompose it, then scale back.
        //
        mx = 0;
        for (i = 0; i <= m - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                mx = Math.Max(mx, math.abscomplex(a[i, j]));
            }
        }
        if ((double)(mx) != (double)(0))
        {
            v = 1 / mx;
            for (i = 0; i <= m - 1; i++)
            {
                for (i_ = 0; i_ <= n - 1; i_++)
                {
                    a[i, i_] = v * a[i, i_];
                }
            }
        }
        dlu.cmatrixplurec(a, 0, m, n, ref pivots, ref tmp, _params);
        if ((double)(mx) != (double)(0))
        {
            v = mx;
            for (i = 0; i <= Math.Min(m, n) - 1; i++)
            {
                for (i_ = i; i_ <= n - 1; i_++)
                {
                    a[i, i_] = v * a[i, i_];
                }
            }
        }
    }


    /*************************************************************************
    Advanced interface for SPDMatrixCholesky, performs no temporary allocations.

    INPUT PARAMETERS:
        A       -   matrix given by upper or lower triangle
        Offs    -   offset of diagonal block to decompose
        N       -   diagonal block size
        IsUpper -   what half is given
        Tmp     -   temporary array; allocated by function, if its size is too
                    small; can be reused on subsequent calls.
                    
    OUTPUT PARAMETERS:
        A       -   upper (or lower) triangle contains Cholesky decomposition

    RESULT:
        True, on success
        False, on failure

      -- ALGLIB routine --
         15.12.2009
         Bochkanov Sergey
    *************************************************************************/
    public static bool spdmatrixcholeskyrec(double[,] a,
        int offs,
        int n,
        bool isupper,
        ref double[] tmp,
        xparams _params)
    {
        bool result = new bool();
        int n1 = 0;
        int n2 = 0;
        int tsa = 0;
        int tsb = 0;

        tsa = apserv.matrixtilesizea(_params);
        tsb = apserv.matrixtilesizeb(_params);

        //
        // Allocate temporaries
        //
        if (ap.len(tmp) < 2 * n)
        {
            tmp = new double[2 * n];
        }

        //
        // Basecases
        //
        if (n < 1)
        {
            result = false;
            return result;
        }
        if (n == 1)
        {
            if ((double)(a[offs, offs]) > (double)(0))
            {
                a[offs, offs] = Math.Sqrt(a[offs, offs]);
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        if (n <= tsb)
        {
            if (ablasmkl.spdmatrixcholeskymkl(a, offs, n, isupper, ref result, _params))
            {
                return result;
            }
        }
        if (n <= tsa)
        {
            result = spdmatrixcholesky2(a, offs, n, isupper, tmp, _params);
            return result;
        }

        //
        // Split task into smaller ones
        //
        if (n > tsb)
        {

            //
            // Split leading B-sized block from the beginning (block-matrix approach)
            //
            n1 = tsb;
            n2 = n - n1;
        }
        else
        {

            //
            // Smaller than B-size, perform cache-oblivious split
            //
            apserv.tiledsplit(n, tsa, ref n1, ref n2, _params);
        }
        result = spdmatrixcholeskyrec(a, offs, n1, isupper, ref tmp, _params);
        if (!result)
        {
            return result;
        }
        if (n2 > 0)
        {
            if (isupper)
            {
                ablas.rmatrixlefttrsm(n1, n2, a, offs, offs, isupper, false, 1, a, offs, offs + n1, _params);
                ablas.rmatrixsyrk(n2, n1, -1.0, a, offs, offs + n1, 1, 1.0, a, offs + n1, offs + n1, isupper, _params);
            }
            else
            {
                ablas.rmatrixrighttrsm(n2, n1, a, offs, offs, isupper, false, 1, a, offs + n1, offs, _params);
                ablas.rmatrixsyrk(n2, n1, -1.0, a, offs + n1, offs, 0, 1.0, a, offs + n1, offs + n1, isupper, _params);
            }
            result = spdmatrixcholeskyrec(a, offs + n1, n2, isupper, ref tmp, _params);
            if (!result)
            {
                return result;
            }
        }
        return result;
    }


    /*************************************************************************
    Recursive computational subroutine for HPDMatrixCholesky

      -- ALGLIB routine --
         15.12.2009
         Bochkanov Sergey
    *************************************************************************/
    private static bool hpdmatrixcholeskyrec(complex[,] a,
        int offs,
        int n,
        bool isupper,
        ref complex[] tmp,
        xparams _params)
    {
        bool result = new bool();
        int n1 = 0;
        int n2 = 0;
        int tsa = 0;
        int tsb = 0;

        tsa = apserv.matrixtilesizea(_params) / 2;
        tsb = apserv.matrixtilesizeb(_params);

        //
        // check N
        //
        if (n < 1)
        {
            result = false;
            return result;
        }

        //
        // Prepare buffer
        //
        if (ap.len(tmp) < 2 * n)
        {
            tmp = new complex[2 * n];
        }

        //
        // Basecases
        //
        // NOTE: we do not use MKL for basecases because their price is only
        //       minor part of overall running time for N>256.
        //
        if (n == 1)
        {
            if ((double)(a[offs, offs].x) > (double)(0))
            {
                a[offs, offs] = Math.Sqrt(a[offs, offs].x);
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        if (n <= tsa)
        {
            result = hpdmatrixcholesky2(a, offs, n, isupper, ref tmp, _params);
            return result;
        }

        //
        // Split task into smaller ones
        //
        if (n > tsb)
        {

            //
            // Split leading B-sized block from the beginning (block-matrix approach)
            //
            n1 = tsb;
            n2 = n - n1;
        }
        else
        {

            //
            // Smaller than B-size, perform cache-oblivious split
            //
            apserv.tiledsplit(n, tsa, ref n1, ref n2, _params);
        }
        result = hpdmatrixcholeskyrec(a, offs, n1, isupper, ref tmp, _params);
        if (!result)
        {
            return result;
        }
        if (n2 > 0)
        {
            if (isupper)
            {
                ablas.cmatrixlefttrsm(n1, n2, a, offs, offs, isupper, false, 2, a, offs, offs + n1, _params);
                ablas.cmatrixherk(n2, n1, -1.0, a, offs, offs + n1, 2, 1.0, a, offs + n1, offs + n1, isupper, _params);
            }
            else
            {
                ablas.cmatrixrighttrsm(n2, n1, a, offs, offs, isupper, false, 2, a, offs + n1, offs, _params);
                ablas.cmatrixherk(n2, n1, -1.0, a, offs + n1, offs, 0, 1.0, a, offs + n1, offs + n1, isupper, _params);
            }
            result = hpdmatrixcholeskyrec(a, offs + n1, n2, isupper, ref tmp, _params);
            if (!result)
            {
                return result;
            }
        }
        return result;
    }


    /*************************************************************************
    Level-2 Hermitian Cholesky subroutine.

      -- LAPACK routine (version 3.0) --
         Univ. of Tennessee, Univ. of California Berkeley, NAG Ltd.,
         Courant Institute, Argonne National Lab, and Rice University
         February 29, 1992
    *************************************************************************/
    private static bool hpdmatrixcholesky2(complex[,] aaa,
        int offs,
        int n,
        bool isupper,
        ref complex[] tmp,
        xparams _params)
    {
        bool result = new bool();
        int i = 0;
        int j = 0;
        double ajj = 0;
        complex v = 0;
        double r = 0;
        int i_ = 0;
        int i1_ = 0;

        result = true;
        if (n < 0)
        {
            result = false;
            return result;
        }

        //
        // Quick return if possible
        //
        if (n == 0)
        {
            return result;
        }
        if (isupper)
        {

            //
            // Compute the Cholesky factorization A = U'*U.
            //
            for (j = 0; j <= n - 1; j++)
            {

                //
                // Compute U(J,J) and test for non-positive-definiteness.
                //
                v = 0.0;
                for (i_ = offs; i_ <= offs + j - 1; i_++)
                {
                    v += math.conj(aaa[i_, offs + j]) * aaa[i_, offs + j];
                }
                ajj = (aaa[offs + j, offs + j] - v).x;
                if ((double)(ajj) <= (double)(0))
                {
                    aaa[offs + j, offs + j] = ajj;
                    result = false;
                    return result;
                }
                ajj = Math.Sqrt(ajj);
                aaa[offs + j, offs + j] = ajj;

                //
                // Compute elements J+1:N-1 of row J.
                //
                if (j < n - 1)
                {
                    if (j > 0)
                    {
                        i1_ = (offs) - (0);
                        for (i_ = 0; i_ <= j - 1; i_++)
                        {
                            tmp[i_] = -math.conj(aaa[i_ + i1_, offs + j]);
                        }
                        ablas.cmatrixmv(n - j - 1, j, aaa, offs, offs + j + 1, 1, tmp, 0, tmp, n, _params);
                        i1_ = (n) - (offs + j + 1);
                        for (i_ = offs + j + 1; i_ <= offs + n - 1; i_++)
                        {
                            aaa[offs + j, i_] = aaa[offs + j, i_] + tmp[i_ + i1_];
                        }
                    }
                    r = 1 / ajj;
                    for (i_ = offs + j + 1; i_ <= offs + n - 1; i_++)
                    {
                        aaa[offs + j, i_] = r * aaa[offs + j, i_];
                    }
                }
            }
        }
        else
        {

            //
            // Compute the Cholesky factorization A = L*L'.
            //
            for (j = 0; j <= n - 1; j++)
            {

                //
                // Compute L(J+1,J+1) and test for non-positive-definiteness.
                //
                v = 0.0;
                for (i_ = offs; i_ <= offs + j - 1; i_++)
                {
                    v += math.conj(aaa[offs + j, i_]) * aaa[offs + j, i_];
                }
                ajj = (aaa[offs + j, offs + j] - v).x;
                if ((double)(ajj) <= (double)(0))
                {
                    aaa[offs + j, offs + j] = ajj;
                    result = false;
                    return result;
                }
                ajj = Math.Sqrt(ajj);
                aaa[offs + j, offs + j] = ajj;

                //
                // Compute elements J+1:N of column J.
                //
                if (j < n - 1)
                {
                    r = 1 / ajj;
                    if (j > 0)
                    {
                        i1_ = (offs) - (0);
                        for (i_ = 0; i_ <= j - 1; i_++)
                        {
                            tmp[i_] = math.conj(aaa[offs + j, i_ + i1_]);
                        }
                        ablas.cmatrixmv(n - j - 1, j, aaa, offs + j + 1, offs, 0, tmp, 0, tmp, n, _params);
                        for (i = 0; i <= n - j - 2; i++)
                        {
                            aaa[offs + j + 1 + i, offs + j] = (aaa[offs + j + 1 + i, offs + j] - tmp[n + i]) * r;
                        }
                    }
                    else
                    {
                        for (i = 0; i <= n - j - 2; i++)
                        {
                            aaa[offs + j + 1 + i, offs + j] = aaa[offs + j + 1 + i, offs + j] * r;
                        }
                    }
                }
            }
        }
        return result;
    }


    /*************************************************************************
    Level-2 Cholesky subroutine

      -- LAPACK routine (version 3.0) --
         Univ. of Tennessee, Univ. of California Berkeley, NAG Ltd.,
         Courant Institute, Argonne National Lab, and Rice University
         February 29, 1992
    *************************************************************************/
    private static bool spdmatrixcholesky2(double[,] aaa,
        int offs,
        int n,
        bool isupper,
        double[] tmp,
        xparams _params)
    {
        bool result = new bool();
        int i = 0;
        int j = 0;
        double ajj = 0;
        double v = 0;
        double r = 0;
        int i_ = 0;
        int i1_ = 0;

        result = true;
        if (n < 0)
        {
            result = false;
            return result;
        }

        //
        // Quick return if possible
        //
        if (n == 0)
        {
            return result;
        }
        if (isupper)
        {

            //
            // Compute the Cholesky factorization A = U'*U.
            //
            for (j = 0; j <= n - 1; j++)
            {

                //
                // Compute U(J,J) and test for non-positive-definiteness.
                //
                v = 0.0;
                for (i_ = offs; i_ <= offs + j - 1; i_++)
                {
                    v += aaa[i_, offs + j] * aaa[i_, offs + j];
                }
                ajj = aaa[offs + j, offs + j] - v;
                if ((double)(ajj) <= (double)(0))
                {
                    aaa[offs + j, offs + j] = ajj;
                    result = false;
                    return result;
                }
                ajj = Math.Sqrt(ajj);
                aaa[offs + j, offs + j] = ajj;

                //
                // Compute elements J+1:N-1 of row J.
                //
                if (j < n - 1)
                {
                    if (j > 0)
                    {
                        i1_ = (offs) - (0);
                        for (i_ = 0; i_ <= j - 1; i_++)
                        {
                            tmp[i_] = -aaa[i_ + i1_, offs + j];
                        }
                        ablas.rmatrixmv(n - j - 1, j, aaa, offs, offs + j + 1, 1, tmp, 0, tmp, n, _params);
                        i1_ = (n) - (offs + j + 1);
                        for (i_ = offs + j + 1; i_ <= offs + n - 1; i_++)
                        {
                            aaa[offs + j, i_] = aaa[offs + j, i_] + tmp[i_ + i1_];
                        }
                    }
                    r = 1 / ajj;
                    for (i_ = offs + j + 1; i_ <= offs + n - 1; i_++)
                    {
                        aaa[offs + j, i_] = r * aaa[offs + j, i_];
                    }
                }
            }
        }
        else
        {

            //
            // Compute the Cholesky factorization A = L*L'.
            //
            for (j = 0; j <= n - 1; j++)
            {

                //
                // Compute L(J+1,J+1) and test for non-positive-definiteness.
                //
                v = 0.0;
                for (i_ = offs; i_ <= offs + j - 1; i_++)
                {
                    v += aaa[offs + j, i_] * aaa[offs + j, i_];
                }
                ajj = aaa[offs + j, offs + j] - v;
                if ((double)(ajj) <= (double)(0))
                {
                    aaa[offs + j, offs + j] = ajj;
                    result = false;
                    return result;
                }
                ajj = Math.Sqrt(ajj);
                aaa[offs + j, offs + j] = ajj;

                //
                // Compute elements J+1:N of column J.
                //
                if (j < n - 1)
                {
                    r = 1 / ajj;
                    if (j > 0)
                    {
                        i1_ = (offs) - (0);
                        for (i_ = 0; i_ <= j - 1; i_++)
                        {
                            tmp[i_] = aaa[offs + j, i_ + i1_];
                        }
                        ablas.rmatrixmv(n - j - 1, j, aaa, offs + j + 1, offs, 0, tmp, 0, tmp, n, _params);
                        for (i = 0; i <= n - j - 2; i++)
                        {
                            aaa[offs + j + 1 + i, offs + j] = (aaa[offs + j + 1 + i, offs + j] - tmp[n + i]) * r;
                        }
                    }
                    else
                    {
                        for (i = 0; i <= n - j - 2; i++)
                        {
                            aaa[offs + j + 1 + i, offs + j] = aaa[offs + j + 1 + i, offs + j] * r;
                        }
                    }
                }
            }
        }
        return result;
    }


}
