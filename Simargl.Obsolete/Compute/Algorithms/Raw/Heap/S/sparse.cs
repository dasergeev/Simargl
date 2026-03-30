using System;

#pragma warning disable CS8618
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

public class sparse
{
    /*************************************************************************
    Sparse matrix structure.

    You should use ALGLIB functions to work with sparse matrix. Never  try  to
    access its fields directly!

    NOTES ON THE SPARSE STORAGE FORMATS

    Sparse matrices can be stored using several formats:
    * Hash-Table representation
    * Compressed Row Storage (CRS)
    * Skyline matrix storage (SKS)

    Each of the formats has benefits and drawbacks:
    * Hash-table is good for dynamic operations (insertion of new elements),
      but does not support linear algebra operations
    * CRS is good for operations like matrix-vector or matrix-matrix products,
      but its initialization is less convenient - you have to tell row   sizes 
      at the initialization, and you have to fill  matrix  only  row  by  row,
      from left to right.
    * SKS is a special format which is used to store triangular  factors  from
      Cholesky factorization. It does not support  dynamic  modification,  and
      support for linear algebra operations is very limited.

    Tables below outline information about these two formats:

        OPERATIONS WITH MATRIX      HASH        CRS         SKS
        creation                    +           +           +
        SparseGet                   +           +           +
        SparseExists                +           +           +
        SparseRewriteExisting       +           +           +
        SparseSet                   +           +           +
        SparseAdd                   +
        SparseGetRow                            +           +
        SparseGetCompressedRow                  +           +
        sparse-dense linear algebra             +           +

    *************************************************************************/
    public class sparsematrix : apobject
    {
        public double[] vals;
        public int[] idx;
        public int[] ridx;
        public int[] didx;
        public int[] uidx;
        public int matrixtype;
        public int m;
        public int n;
        public int nfree;
        public int ninitialized;
        public int tablesize;
        public sparsematrix()
        {
            init();
        }
        public override void init()
        {
            vals = new double[0];
            idx = new int[0];
            ridx = new int[0];
            didx = new int[0];
            uidx = new int[0];
        }
        public override apobject make_copy()
        {
            sparsematrix _result = new sparsematrix();
            _result.vals = (double[])vals.Clone();
            _result.idx = (int[])idx.Clone();
            _result.ridx = (int[])ridx.Clone();
            _result.didx = (int[])didx.Clone();
            _result.uidx = (int[])uidx.Clone();
            _result.matrixtype = matrixtype;
            _result.m = m;
            _result.n = n;
            _result.nfree = nfree;
            _result.ninitialized = ninitialized;
            _result.tablesize = tablesize;
            return _result;
        }
    };


    /*************************************************************************
    Temporary buffers for sparse matrix operations.

    You should pass an instance of this structure to factorization  functions.
    It allows to reuse memory during repeated sparse  factorizations.  You  do
    not have to call some initialization function - simply passing an instance
    to factorization function is enough.

    *************************************************************************/
    public class sparsebuffers : apobject
    {
        public int[] d;
        public int[] u;
        public sparsematrix s;
        public sparsebuffers()
        {
            init();
        }
        public override void init()
        {
            d = new int[0];
            u = new int[0];
            s = new sparsematrix();
        }
        public override apobject make_copy()
        {
            sparsebuffers _result = new sparsebuffers();
            _result.d = (int[])d.Clone();
            _result.u = (int[])u.Clone();
            _result.s = (sparsematrix)s.make_copy();
            return _result;
        }
    };




    public const double desiredloadfactor = 0.66;
    public const double maxloadfactor = 0.75;
    public const double growfactor = 2.00;
    public const int additional = 10;
    public const int linalgswitch = 16;


    /*************************************************************************
    This function creates sparse matrix in a Hash-Table format.

    This function creates Hast-Table matrix, which can be  converted  to  CRS
    format after its initialization is over. Typical  usage  scenario  for  a
    sparse matrix is:
    1. creation in a Hash-Table format
    2. insertion of the matrix elements
    3. conversion to the CRS representation
    4. matrix is passed to some linear algebra algorithm

    Some  information  about  different matrix formats can be found below, in
    the "NOTES" section.

    INPUT PARAMETERS
        M           -   number of rows in a matrix, M>=1
        N           -   number of columns in a matrix, N>=1
        K           -   K>=0, expected number of non-zero elements in a matrix.
                        K can be inexact approximation, can be less than actual
                        number  of  elements  (table will grow when needed) or 
                        even zero).
                        It is important to understand that although hash-table
                        may grow automatically, it is better to  provide  good
                        estimate of data size.

    OUTPUT PARAMETERS
        S           -   sparse M*N matrix in Hash-Table representation.
                        All elements of the matrix are zero.

    NOTE 1

    Hash-tables use memory inefficiently, and they have to keep  some  amount
    of the "spare memory" in order to have good performance. Hash  table  for
    matrix with K non-zero elements will  need  C*K*(8+2*sizeof(int))  bytes,
    where C is a small constant, about 1.5-2 in magnitude.

    CRS storage, from the other side, is  more  memory-efficient,  and  needs
    just K*(8+sizeof(int))+M*sizeof(int) bytes, where M is a number  of  rows
    in a matrix.

    When you convert from the Hash-Table to CRS  representation, all unneeded
    memory will be freed.

    NOTE 2

    Comments of SparseMatrix structure outline  information  about  different
    sparse storage formats. We recommend you to read them before starting  to
    use ALGLIB sparse matrices.

    NOTE 3

    This function completely  overwrites S with new sparse matrix. Previously
    allocated storage is NOT reused. If you  want  to reuse already allocated
    memory, call SparseCreateBuf function.

      -- ALGLIB PROJECT --
         Copyright 14.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsecreate(int m,
        int n,
        int k,
        sparsematrix s,
        xparams _params)
    {
        sparsecreatebuf(m, n, k, s, _params);
    }


    /*************************************************************************
    This version of SparseCreate function creates sparse matrix in Hash-Table
    format, reusing previously allocated storage as much  as  possible.  Read
    comments for SparseCreate() for more information.

    INPUT PARAMETERS
        M           -   number of rows in a matrix, M>=1
        N           -   number of columns in a matrix, N>=1
        K           -   K>=0, expected number of non-zero elements in a matrix.
                        K can be inexact approximation, can be less than actual
                        number  of  elements  (table will grow when needed) or 
                        even zero).
                        It is important to understand that although hash-table
                        may grow automatically, it is better to  provide  good
                        estimate of data size.
        S           -   SparseMatrix structure which MAY contain some  already
                        allocated storage.

    OUTPUT PARAMETERS
        S           -   sparse M*N matrix in Hash-Table representation.
                        All elements of the matrix are zero.
                        Previously allocated storage is reused, if  its  size
                        is compatible with expected number of non-zeros K.

      -- ALGLIB PROJECT --
         Copyright 14.01.2014 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsecreatebuf(int m,
        int n,
        int k,
        sparsematrix s,
        xparams _params)
    {
        int i = 0;

        ap.assert(m > 0, "SparseCreateBuf: M<=0");
        ap.assert(n > 0, "SparseCreateBuf: N<=0");
        ap.assert(k >= 0, "SparseCreateBuf: K<0");

        //
        // Hash-table size is max(existing_size,requested_size)
        //
        // NOTE: it is important to use ALL available memory for hash table
        //       because it is impossible to efficiently reallocate table
        //       without temporary storage. So, if we want table with up to
        //       1.000.000 elements, we have to create such table from the
        //       very beginning. Otherwise, the very idea of memory reuse
        //       will be compromised.
        //
        s.tablesize = (int)Math.Round(k / desiredloadfactor + additional);
        apserv.rvectorsetlengthatleast(ref s.vals, s.tablesize, _params);
        s.tablesize = ap.len(s.vals);

        //
        // Initialize other fields
        //
        s.matrixtype = 0;
        s.m = m;
        s.n = n;
        s.nfree = s.tablesize;
        apserv.ivectorsetlengthatleast(ref s.idx, 2 * s.tablesize, _params);
        for (i = 0; i <= s.tablesize - 1; i++)
        {
            s.idx[2 * i] = -1;
        }
    }


    /*************************************************************************
    This function creates sparse matrix in a CRS format (expert function for
    situations when you are running out of memory).

    This function creates CRS matrix. Typical usage scenario for a CRS matrix 
    is:
    1. creation (you have to tell number of non-zero elements at each row  at 
       this moment)
    2. insertion of the matrix elements (row by row, from left to right) 
    3. matrix is passed to some linear algebra algorithm

    This function is a memory-efficient alternative to SparseCreate(), but it
    is more complex because it requires you to know in advance how large your
    matrix is. Some  information about  different matrix formats can be found 
    in comments on SparseMatrix structure.  We recommend  you  to  read  them
    before starting to use ALGLIB sparse matrices..

    INPUT PARAMETERS
        M           -   number of rows in a matrix, M>=1
        N           -   number of columns in a matrix, N>=1
        NER         -   number of elements at each row, array[M], NER[I]>=0

    OUTPUT PARAMETERS
        S           -   sparse M*N matrix in CRS representation.
                        You have to fill ALL non-zero elements by calling
                        SparseSet() BEFORE you try to use this matrix.
                        
    NOTE: this function completely  overwrites  S  with  new  sparse  matrix.
          Previously allocated storage is NOT reused. If you  want  to  reuse
          already allocated memory, call SparseCreateCRSBuf function.

      -- ALGLIB PROJECT --
         Copyright 14.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsecreatecrs(int m,
        int n,
        int[] ner,
        sparsematrix s,
        xparams _params)
    {
        int i = 0;

        ap.assert(m > 0, "SparseCreateCRS: M<=0");
        ap.assert(n > 0, "SparseCreateCRS: N<=0");
        ap.assert(ap.len(ner) >= m, "SparseCreateCRS: Length(NER)<M");
        for (i = 0; i <= m - 1; i++)
        {
            ap.assert(ner[i] >= 0, "SparseCreateCRS: NER[] contains negative elements");
        }
        sparsecreatecrsbuf(m, n, ner, s, _params);
    }


    /*************************************************************************
    This function creates sparse matrix in a CRS format (expert function  for
    situations when you are running out  of  memory).  This  version  of  CRS
    matrix creation function may reuse memory already allocated in S.

    This function creates CRS matrix. Typical usage scenario for a CRS matrix 
    is:
    1. creation (you have to tell number of non-zero elements at each row  at 
       this moment)
    2. insertion of the matrix elements (row by row, from left to right) 
    3. matrix is passed to some linear algebra algorithm

    This function is a memory-efficient alternative to SparseCreate(), but it
    is more complex because it requires you to know in advance how large your
    matrix is. Some  information about  different matrix formats can be found 
    in comments on SparseMatrix structure.  We recommend  you  to  read  them
    before starting to use ALGLIB sparse matrices..

    INPUT PARAMETERS
        M           -   number of rows in a matrix, M>=1
        N           -   number of columns in a matrix, N>=1
        NER         -   number of elements at each row, array[M], NER[I]>=0
        S           -   sparse matrix structure with possibly preallocated
                        memory.

    OUTPUT PARAMETERS
        S           -   sparse M*N matrix in CRS representation.
                        You have to fill ALL non-zero elements by calling
                        SparseSet() BEFORE you try to use this matrix.

      -- ALGLIB PROJECT --
         Copyright 14.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsecreatecrsbuf(int m,
        int n,
        int[] ner,
        sparsematrix s,
        xparams _params)
    {
        int i = 0;
        int noe = 0;

        ap.assert(m > 0, "SparseCreateCRSBuf: M<=0");
        ap.assert(n > 0, "SparseCreateCRSBuf: N<=0");
        ap.assert(ap.len(ner) >= m, "SparseCreateCRSBuf: Length(NER)<M");
        noe = 0;
        s.matrixtype = 1;
        s.ninitialized = 0;
        s.m = m;
        s.n = n;
        apserv.ivectorsetlengthatleast(ref s.ridx, s.m + 1, _params);
        s.ridx[0] = 0;
        for (i = 0; i <= s.m - 1; i++)
        {
            ap.assert(ner[i] >= 0, "SparseCreateCRSBuf: NER[] contains negative elements");
            noe = noe + ner[i];
            s.ridx[i + 1] = s.ridx[i] + ner[i];
        }
        apserv.rvectorsetlengthatleast(ref s.vals, noe, _params);
        apserv.ivectorsetlengthatleast(ref s.idx, noe, _params);
        if (noe == 0)
        {
            sparseinitduidx(s, _params);
        }
    }


    /*************************************************************************
    This function creates sparse matrix in  a  SKS  format  (skyline  storage
    format). In most cases you do not need this function - CRS format  better
    suits most use cases.

    INPUT PARAMETERS
        M, N        -   number of rows(M) and columns (N) in a matrix:
                        * M=N (as for now, ALGLIB supports only square SKS)
                        * N>=1
                        * M>=1
        D           -   "bottom" bandwidths, array[M], D[I]>=0.
                        I-th element stores number of non-zeros at I-th  row,
                        below the diagonal (diagonal itself is not  included)
        U           -   "top" bandwidths, array[N], U[I]>=0.
                        I-th element stores number of non-zeros  at I-th row,
                        above the diagonal (diagonal itself  is not included)

    OUTPUT PARAMETERS
        S           -   sparse M*N matrix in SKS representation.
                        All elements are filled by zeros.
                        You may use sparseset() to change their values.
                        
    NOTE: this function completely  overwrites  S  with  new  sparse  matrix.
          Previously allocated storage is NOT reused. If you  want  to  reuse
          already allocated memory, call SparseCreateSKSBuf function.

      -- ALGLIB PROJECT --
         Copyright 13.01.2014 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsecreatesks(int m,
        int n,
        int[] d,
        int[] u,
        sparsematrix s,
        xparams _params)
    {
        int i = 0;

        ap.assert(m > 0, "SparseCreateSKS: M<=0");
        ap.assert(n > 0, "SparseCreateSKS: N<=0");
        ap.assert(m == n, "SparseCreateSKS: M<>N");
        ap.assert(ap.len(d) >= m, "SparseCreateSKS: Length(D)<M");
        ap.assert(ap.len(u) >= n, "SparseCreateSKS: Length(U)<N");
        for (i = 0; i <= m - 1; i++)
        {
            ap.assert(d[i] >= 0, "SparseCreateSKS: D[] contains negative elements");
            ap.assert(d[i] <= i, "SparseCreateSKS: D[I]>I for some I");
        }
        for (i = 0; i <= n - 1; i++)
        {
            ap.assert(u[i] >= 0, "SparseCreateSKS: U[] contains negative elements");
            ap.assert(u[i] <= i, "SparseCreateSKS: U[I]>I for some I");
        }
        sparsecreatesksbuf(m, n, d, u, s, _params);
    }


    /*************************************************************************
    This is "buffered"  version  of  SparseCreateSKS()  which  reuses  memory
    previously allocated in S (of course, memory is reallocated if needed).

    This function creates sparse matrix in  a  SKS  format  (skyline  storage
    format). In most cases you do not need this function - CRS format  better
    suits most use cases.

    INPUT PARAMETERS
        M, N        -   number of rows(M) and columns (N) in a matrix:
                        * M=N (as for now, ALGLIB supports only square SKS)
                        * N>=1
                        * M>=1
        D           -   "bottom" bandwidths, array[M], 0<=D[I]<=I.
                        I-th element stores number of non-zeros at I-th row,
                        below the diagonal (diagonal itself is not included)
        U           -   "top" bandwidths, array[N], 0<=U[I]<=I.
                        I-th element stores number of non-zeros at I-th row,
                        above the diagonal (diagonal itself is not included)

    OUTPUT PARAMETERS
        S           -   sparse M*N matrix in SKS representation.
                        All elements are filled by zeros.
                        You may use sparseset() to change their values.

      -- ALGLIB PROJECT --
         Copyright 13.01.2014 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsecreatesksbuf(int m,
        int n,
        int[] d,
        int[] u,
        sparsematrix s,
        xparams _params)
    {
        int i = 0;
        int minmn = 0;
        int nz = 0;
        int mxd = 0;
        int mxu = 0;

        ap.assert(m > 0, "SparseCreateSKSBuf: M<=0");
        ap.assert(n > 0, "SparseCreateSKSBuf: N<=0");
        ap.assert(m == n, "SparseCreateSKSBuf: M<>N");
        ap.assert(ap.len(d) >= m, "SparseCreateSKSBuf: Length(D)<M");
        ap.assert(ap.len(u) >= n, "SparseCreateSKSBuf: Length(U)<N");
        for (i = 0; i <= m - 1; i++)
        {
            ap.assert(d[i] >= 0, "SparseCreateSKSBuf: D[] contains negative elements");
            ap.assert(d[i] <= i, "SparseCreateSKSBuf: D[I]>I for some I");
        }
        for (i = 0; i <= n - 1; i++)
        {
            ap.assert(u[i] >= 0, "SparseCreateSKSBuf: U[] contains negative elements");
            ap.assert(u[i] <= i, "SparseCreateSKSBuf: U[I]>I for some I");
        }
        minmn = Math.Min(m, n);
        s.matrixtype = 2;
        s.ninitialized = 0;
        s.m = m;
        s.n = n;
        apserv.ivectorsetlengthatleast(ref s.ridx, minmn + 1, _params);
        s.ridx[0] = 0;
        nz = 0;
        for (i = 0; i <= minmn - 1; i++)
        {
            nz = nz + 1 + d[i] + u[i];
            s.ridx[i + 1] = s.ridx[i] + 1 + d[i] + u[i];
        }
        apserv.rvectorsetlengthatleast(ref s.vals, nz, _params);
        for (i = 0; i <= nz - 1; i++)
        {
            s.vals[i] = 0.0;
        }
        apserv.ivectorsetlengthatleast(ref s.didx, m + 1, _params);
        mxd = 0;
        for (i = 0; i <= m - 1; i++)
        {
            s.didx[i] = d[i];
            mxd = Math.Max(mxd, d[i]);
        }
        s.didx[m] = mxd;
        apserv.ivectorsetlengthatleast(ref s.uidx, n + 1, _params);
        mxu = 0;
        for (i = 0; i <= n - 1; i++)
        {
            s.uidx[i] = u[i];
            mxu = Math.Max(mxu, u[i]);
        }
        s.uidx[n] = mxu;
    }


    /*************************************************************************
    This function creates sparse matrix in  a  SKS  format  (skyline  storage
    format). Unlike more general  sparsecreatesks(),  this  function  creates
    sparse matrix with constant bandwidth.

    You may want to use this function instead of sparsecreatesks() when  your
    matrix has  constant  or  nearly-constant  bandwidth,  and  you  want  to
    simplify source code.

    INPUT PARAMETERS
        M, N        -   number of rows(M) and columns (N) in a matrix:
                        * M=N (as for now, ALGLIB supports only square SKS)
                        * N>=1
                        * M>=1
        BW          -   matrix bandwidth, BW>=0

    OUTPUT PARAMETERS
        S           -   sparse M*N matrix in SKS representation.
                        All elements are filled by zeros.
                        You may use sparseset() to  change  their values.
                        
    NOTE: this function completely  overwrites  S  with  new  sparse  matrix.
          Previously allocated storage is NOT reused. If you  want  to  reuse
          already allocated memory, call sparsecreatesksbandbuf function.

      -- ALGLIB PROJECT --
         Copyright 25.12.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsecreatesksband(int m,
        int n,
        int bw,
        sparsematrix s,
        xparams _params)
    {
        ap.assert(m > 0, "SparseCreateSKSBand: M<=0");
        ap.assert(n > 0, "SparseCreateSKSBand: N<=0");
        ap.assert(bw >= 0, "SparseCreateSKSBand: BW<0");
        ap.assert(m == n, "SparseCreateSKSBand: M!=N");
        sparsecreatesksbandbuf(m, n, bw, s, _params);
    }


    /*************************************************************************
    This is "buffered" version  of  sparsecreatesksband() which reuses memory
    previously allocated in S (of course, memory is reallocated if needed).

    You may want to use this function instead  of  sparsecreatesksbuf()  when
    your matrix has  constant or nearly-constant  bandwidth,  and you want to
    simplify source code.

    INPUT PARAMETERS
        M, N        -   number of rows(M) and columns (N) in a matrix:
                        * M=N (as for now, ALGLIB supports only square SKS)
                        * N>=1
                        * M>=1
        BW          -   bandwidth, BW>=0

    OUTPUT PARAMETERS
        S           -   sparse M*N matrix in SKS representation.
                        All elements are filled by zeros.
                        You may use sparseset() to change their values.

      -- ALGLIB PROJECT --
         Copyright 13.01.2014 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsecreatesksbandbuf(int m,
        int n,
        int bw,
        sparsematrix s,
        xparams _params)
    {
        int i = 0;
        int minmn = 0;
        int nz = 0;
        int mxd = 0;
        int mxu = 0;
        int dui = 0;

        ap.assert(m > 0, "SparseCreateSKSBandBuf: M<=0");
        ap.assert(n > 0, "SparseCreateSKSBandBuf: N<=0");
        ap.assert(m == n, "SparseCreateSKSBandBuf: M!=N");
        ap.assert(bw >= 0, "SparseCreateSKSBandBuf: BW<0");
        minmn = Math.Min(m, n);
        s.matrixtype = 2;
        s.ninitialized = 0;
        s.m = m;
        s.n = n;
        apserv.ivectorsetlengthatleast(ref s.ridx, minmn + 1, _params);
        s.ridx[0] = 0;
        nz = 0;
        for (i = 0; i <= minmn - 1; i++)
        {
            dui = Math.Min(i, bw);
            nz = nz + 1 + 2 * dui;
            s.ridx[i + 1] = s.ridx[i] + 1 + 2 * dui;
        }
        apserv.rvectorsetlengthatleast(ref s.vals, nz, _params);
        for (i = 0; i <= nz - 1; i++)
        {
            s.vals[i] = 0.0;
        }
        apserv.ivectorsetlengthatleast(ref s.didx, m + 1, _params);
        mxd = 0;
        for (i = 0; i <= m - 1; i++)
        {
            dui = Math.Min(i, bw);
            s.didx[i] = dui;
            mxd = Math.Max(mxd, dui);
        }
        s.didx[m] = mxd;
        apserv.ivectorsetlengthatleast(ref s.uidx, n + 1, _params);
        mxu = 0;
        for (i = 0; i <= n - 1; i++)
        {
            dui = Math.Min(i, bw);
            s.uidx[i] = dui;
            mxu = Math.Max(mxu, dui);
        }
        s.uidx[n] = mxu;
    }


    /*************************************************************************
    This function copies S0 to S1.
    This function completely deallocates memory owned by S1 before creating a
    copy of S0. If you want to reuse memory, use SparseCopyBuf.

    NOTE:  this  function  does  not verify its arguments, it just copies all
    fields of the structure.

      -- ALGLIB PROJECT --
         Copyright 14.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsecopy(sparsematrix s0,
        sparsematrix s1,
        xparams _params)
    {
        sparsecopybuf(s0, s1, _params);
    }


    /*************************************************************************
    This function copies S0 to S1.
    Memory already allocated in S1 is reused as much as possible.

    NOTE:  this  function  does  not verify its arguments, it just copies all
    fields of the structure.

      -- ALGLIB PROJECT --
         Copyright 14.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsecopybuf(sparsematrix s0,
        sparsematrix s1,
        xparams _params)
    {
        int l = 0;
        int i = 0;

        s1.matrixtype = s0.matrixtype;
        s1.m = s0.m;
        s1.n = s0.n;
        s1.nfree = s0.nfree;
        s1.ninitialized = s0.ninitialized;
        s1.tablesize = s0.tablesize;

        //
        // Initialization for arrays
        //
        l = ap.len(s0.vals);
        apserv.rvectorsetlengthatleast(ref s1.vals, l, _params);
        for (i = 0; i <= l - 1; i++)
        {
            s1.vals[i] = s0.vals[i];
        }
        l = ap.len(s0.ridx);
        apserv.ivectorsetlengthatleast(ref s1.ridx, l, _params);
        for (i = 0; i <= l - 1; i++)
        {
            s1.ridx[i] = s0.ridx[i];
        }
        l = ap.len(s0.idx);
        apserv.ivectorsetlengthatleast(ref s1.idx, l, _params);
        for (i = 0; i <= l - 1; i++)
        {
            s1.idx[i] = s0.idx[i];
        }

        //
        // Initalization for CRS-parameters
        //
        l = ap.len(s0.uidx);
        apserv.ivectorsetlengthatleast(ref s1.uidx, l, _params);
        for (i = 0; i <= l - 1; i++)
        {
            s1.uidx[i] = s0.uidx[i];
        }
        l = ap.len(s0.didx);
        apserv.ivectorsetlengthatleast(ref s1.didx, l, _params);
        for (i = 0; i <= l - 1; i++)
        {
            s1.didx[i] = s0.didx[i];
        }
    }


    /*************************************************************************
    This function efficiently swaps contents of S0 and S1.

      -- ALGLIB PROJECT --
         Copyright 16.01.2014 by Bochkanov Sergey
    *************************************************************************/
    public static void sparseswap(sparsematrix s0,
        sparsematrix s1,
        xparams _params)
    {
        apserv.swapi(ref s1.matrixtype, ref s0.matrixtype, _params);
        apserv.swapi(ref s1.m, ref s0.m, _params);
        apserv.swapi(ref s1.n, ref s0.n, _params);
        apserv.swapi(ref s1.nfree, ref s0.nfree, _params);
        apserv.swapi(ref s1.ninitialized, ref s0.ninitialized, _params);
        apserv.swapi(ref s1.tablesize, ref s0.tablesize, _params);
        ap.swap(ref s1.vals, ref s0.vals);
        ap.swap(ref s1.ridx, ref s0.ridx);
        ap.swap(ref s1.idx, ref s0.idx);
        ap.swap(ref s1.uidx, ref s0.uidx);
        ap.swap(ref s1.didx, ref s0.didx);
    }


    /*************************************************************************
    This function adds value to S[i,j] - element of the sparse matrix. Matrix
    must be in a Hash-Table mode.

    In case S[i,j] already exists in the table, V i added to  its  value.  In
    case  S[i,j]  is  non-existent,  it  is  inserted  in  the  table.  Table
    automatically grows when necessary.

    INPUT PARAMETERS
        S           -   sparse M*N matrix in Hash-Table representation.
                        Exception will be thrown for CRS matrix.
        I           -   row index of the element to modify, 0<=I<M
        J           -   column index of the element to modify, 0<=J<N
        V           -   value to add, must be finite number

    OUTPUT PARAMETERS
        S           -   modified matrix
        
    NOTE 1:  when  S[i,j]  is exactly zero after modification, it is  deleted
    from the table.

      -- ALGLIB PROJECT --
         Copyright 14.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void sparseadd(sparsematrix s,
        int i,
        int j,
        double v,
        xparams _params)
    {
        int hashcode = 0;
        int tcode = 0;
        int k = 0;

        ap.assert(s.matrixtype == 0, "SparseAdd: matrix must be in the Hash-Table mode to do this operation");
        ap.assert(i >= 0, "SparseAdd: I<0");
        ap.assert(i < s.m, "SparseAdd: I>=M");
        ap.assert(j >= 0, "SparseAdd: J<0");
        ap.assert(j < s.n, "SparseAdd: J>=N");
        ap.assert(math.isfinite(v), "SparseAdd: V is not finite number");
        if ((double)(v) == (double)(0))
        {
            return;
        }
        tcode = -1;
        k = s.tablesize;
        if ((double)((1 - maxloadfactor) * k) >= (double)(s.nfree))
        {
            sparseresizematrix(s, _params);
            k = s.tablesize;
        }
        hashcode = hash(i, j, k, _params);
        while (true)
        {
            if (s.idx[2 * hashcode] == -1)
            {
                if (tcode != -1)
                {
                    hashcode = tcode;
                }
                s.vals[hashcode] = v;
                s.idx[2 * hashcode] = i;
                s.idx[2 * hashcode + 1] = j;
                if (tcode == -1)
                {
                    s.nfree = s.nfree - 1;
                }
                return;
            }
            else
            {
                if (s.idx[2 * hashcode] == i && s.idx[2 * hashcode + 1] == j)
                {
                    s.vals[hashcode] = s.vals[hashcode] + v;
                    if ((double)(s.vals[hashcode]) == (double)(0))
                    {
                        s.idx[2 * hashcode] = -2;
                    }
                    return;
                }

                //
                // Is it deleted element?
                //
                if (tcode == -1 && s.idx[2 * hashcode] == -2)
                {
                    tcode = hashcode;
                }

                //
                // Next step
                //
                hashcode = (hashcode + 1) % k;
            }
        }
    }


    /*************************************************************************
    This function modifies S[i,j] - element of the sparse matrix.

    For Hash-based storage format:
    * this function can be called at any moment - during matrix initialization
      or later
    * new value can be zero or non-zero.  In case new value of S[i,j] is zero,
      this element is deleted from the table.
    * this  function  has  no  effect when called with zero V for non-existent
      element.

    For CRS-bases storage format:
    * this function can be called ONLY DURING MATRIX INITIALIZATION
    * zero values are stored in the matrix similarly to non-zero ones
    * elements must be initialized in correct order -  from top row to bottom,
      within row - from left to right.
      
    For SKS storage:
    * this function can be called at any moment - during matrix initialization
      or later
    * zero values are stored in the matrix similarly to non-zero ones
    * this function CAN NOT be called for non-existent (outside  of  the  band
      specified during SKS matrix creation) elements. Say, if you created  SKS
      matrix  with  bandwidth=2  and  tried to call sparseset(s,0,10,VAL),  an
      exception will be generated.

    INPUT PARAMETERS
        S           -   sparse M*N matrix in Hash-Table, SKS or CRS format.
        I           -   row index of the element to modify, 0<=I<M
        J           -   column index of the element to modify, 0<=J<N
        V           -   value to set, must be finite number, can be zero

    OUTPUT PARAMETERS
        S           -   modified matrix

      -- ALGLIB PROJECT --
         Copyright 14.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void sparseset(sparsematrix s,
        int i,
        int j,
        double v,
        xparams _params)
    {
        int hashcode = 0;
        int tcode = 0;
        int k = 0;
        bool b = new bool();

        ap.assert((s.matrixtype == 0 || s.matrixtype == 1) || s.matrixtype == 2, "SparseSet: unsupported matrix storage format");
        ap.assert(i >= 0, "SparseSet: I<0");
        ap.assert(i < s.m, "SparseSet: I>=M");
        ap.assert(j >= 0, "SparseSet: J<0");
        ap.assert(j < s.n, "SparseSet: J>=N");
        ap.assert(math.isfinite(v), "SparseSet: V is not finite number");

        //
        // Hash-table matrix
        //
        if (s.matrixtype == 0)
        {
            tcode = -1;
            k = s.tablesize;
            if ((double)((1 - maxloadfactor) * k) >= (double)(s.nfree))
            {
                sparseresizematrix(s, _params);
                k = s.tablesize;
            }
            hashcode = hash(i, j, k, _params);
            while (true)
            {
                if (s.idx[2 * hashcode] == -1)
                {
                    if ((double)(v) != (double)(0))
                    {
                        if (tcode != -1)
                        {
                            hashcode = tcode;
                        }
                        s.vals[hashcode] = v;
                        s.idx[2 * hashcode] = i;
                        s.idx[2 * hashcode + 1] = j;
                        if (tcode == -1)
                        {
                            s.nfree = s.nfree - 1;
                        }
                    }
                    return;
                }
                else
                {
                    if (s.idx[2 * hashcode] == i && s.idx[2 * hashcode + 1] == j)
                    {
                        if ((double)(v) == (double)(0))
                        {
                            s.idx[2 * hashcode] = -2;
                        }
                        else
                        {
                            s.vals[hashcode] = v;
                        }
                        return;
                    }
                    if (tcode == -1 && s.idx[2 * hashcode] == -2)
                    {
                        tcode = hashcode;
                    }

                    //
                    // Next step
                    //
                    hashcode = (hashcode + 1) % k;
                }
            }
        }

        //
        // CRS matrix
        //
        if (s.matrixtype == 1)
        {
            ap.assert(s.ridx[i] <= s.ninitialized, "SparseSet: too few initialized elements at some row (you have promised more when called SparceCreateCRS)");
            ap.assert(s.ridx[i + 1] > s.ninitialized, "SparseSet: too many initialized elements at some row (you have promised less when called SparceCreateCRS)");
            ap.assert(s.ninitialized == s.ridx[i] || s.idx[s.ninitialized - 1] < j, "SparseSet: incorrect column order (you must fill every row from left to right)");
            s.vals[s.ninitialized] = v;
            s.idx[s.ninitialized] = j;
            s.ninitialized = s.ninitialized + 1;

            //
            // If matrix has been created then
            // initiale 'S.UIdx' and 'S.DIdx'
            //
            if (s.ninitialized == s.ridx[s.m])
            {
                sparseinitduidx(s, _params);
            }
            return;
        }

        //
        // SKS matrix
        //
        if (s.matrixtype == 2)
        {
            b = sparserewriteexisting(s, i, j, v, _params);
            ap.assert(b, "SparseSet: an attempt to initialize out-of-band element of the SKS matrix");
            return;
        }
    }


    /*************************************************************************
    This function returns S[i,j] - element of the sparse matrix.  Matrix  can
    be in any mode (Hash-Table, CRS, SKS), but this function is less efficient
    for CRS matrices. Hash-Table and SKS matrices can find  element  in  O(1)
    time, while  CRS  matrices need O(log(RS)) time, where RS is an number of
    non-zero elements in a row.

    INPUT PARAMETERS
        S           -   sparse M*N matrix
        I           -   row index of the element to modify, 0<=I<M
        J           -   column index of the element to modify, 0<=J<N

    RESULT
        value of S[I,J] or zero (in case no element with such index is found)

      -- ALGLIB PROJECT --
         Copyright 14.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static double sparseget(sparsematrix s,
        int i,
        int j,
        xparams _params)
    {
        double result = 0;
        int hashcode = 0;
        int k = 0;
        int k0 = 0;
        int k1 = 0;

        ap.assert(i >= 0, "SparseGet: I<0");
        ap.assert(i < s.m, "SparseGet: I>=M");
        ap.assert(j >= 0, "SparseGet: J<0");
        ap.assert(j < s.n, "SparseGet: J>=N");
        result = 0.0;
        if (s.matrixtype == 0)
        {

            //
            // Hash-based storage
            //
            result = 0;
            k = s.tablesize;
            hashcode = hash(i, j, k, _params);
            while (true)
            {
                if (s.idx[2 * hashcode] == -1)
                {
                    return result;
                }
                if (s.idx[2 * hashcode] == i && s.idx[2 * hashcode + 1] == j)
                {
                    result = s.vals[hashcode];
                    return result;
                }
                hashcode = (hashcode + 1) % k;
            }
        }
        if (s.matrixtype == 1)
        {

            //
            // CRS
            //
            ap.assert(s.ninitialized == s.ridx[s.m], "SparseGet: some rows/elements of the CRS matrix were not initialized (you must initialize everything you promised to SparseCreateCRS)");
            k0 = s.ridx[i];
            k1 = s.ridx[i + 1] - 1;
            result = 0;
            while (k0 <= k1)
            {
                k = (k0 + k1) / 2;
                if (s.idx[k] == j)
                {
                    result = s.vals[k];
                    return result;
                }
                if (s.idx[k] < j)
                {
                    k0 = k + 1;
                }
                else
                {
                    k1 = k - 1;
                }
            }
            return result;
        }
        if (s.matrixtype == 2)
        {

            //
            // SKS
            //
            ap.assert(s.m == s.n, "SparseGet: non-square SKS matrix not supported");
            result = 0;
            if (i == j)
            {

                //
                // Return diagonal element
                //
                result = s.vals[s.ridx[i] + s.didx[i]];
                return result;
            }
            if (j < i)
            {

                //
                // Return subdiagonal element at I-th "skyline block"
                //
                k = s.didx[i];
                if (i - j <= k)
                {
                    result = s.vals[s.ridx[i] + k + j - i];
                }
            }
            else
            {

                //
                // Return superdiagonal element at J-th "skyline block"
                //
                k = s.uidx[j];
                if (j - i <= k)
                {
                    result = s.vals[s.ridx[j + 1] - (j - i)];
                }
                return result;
            }
            return result;
        }
        ap.assert(false, "SparseGet: unexpected matrix type");
        return result;
    }


    /*************************************************************************
    This function checks whether S[i,j] is present in the sparse  matrix.  It
    returns True even for elements  that  are  numerically  zero  (but  still
    have place allocated for them).

    The matrix  can be in any mode (Hash-Table, CRS, SKS), but this  function
    is less efficient for CRS matrices. Hash-Table and SKS matrices can  find
    element in O(1) time, while  CRS  matrices need O(log(RS)) time, where RS
    is an number of non-zero elements in a row.

    INPUT PARAMETERS
        S           -   sparse M*N matrix
        I           -   row index of the element to modify, 0<=I<M
        J           -   column index of the element to modify, 0<=J<N

    RESULT
        whether S[I,J] is present in the data structure or not

      -- ALGLIB PROJECT --
         Copyright 14.10.2020 by Bochkanov Sergey
    *************************************************************************/
    public static bool sparseexists(sparsematrix s,
        int i,
        int j,
        xparams _params)
    {
        bool result = new bool();
        int hashcode = 0;
        int k = 0;
        int k0 = 0;
        int k1 = 0;

        ap.assert(i >= 0, "SparseExists: I<0");
        ap.assert(i < s.m, "SparseExists: I>=M");
        ap.assert(j >= 0, "SparseExists: J<0");
        ap.assert(j < s.n, "SparseExists: J>=N");
        result = false;
        if (s.matrixtype == 0)
        {

            //
            // Hash-based storage
            //
            k = s.tablesize;
            hashcode = hash(i, j, k, _params);
            while (true)
            {
                if (s.idx[2 * hashcode] == -1)
                {
                    return result;
                }
                if (s.idx[2 * hashcode] == i && s.idx[2 * hashcode + 1] == j)
                {
                    result = true;
                    return result;
                }
                hashcode = (hashcode + 1) % k;
            }
        }
        if (s.matrixtype == 1)
        {

            //
            // CRS
            //
            ap.assert(s.ninitialized == s.ridx[s.m], "SparseExists: some rows/elements of the CRS matrix were not initialized (you must initialize everything you promised to SparseCreateCRS)");
            k0 = s.ridx[i];
            k1 = s.ridx[i + 1] - 1;
            while (k0 <= k1)
            {
                k = (k0 + k1) / 2;
                if (s.idx[k] == j)
                {
                    result = true;
                    return result;
                }
                if (s.idx[k] < j)
                {
                    k0 = k + 1;
                }
                else
                {
                    k1 = k - 1;
                }
            }
            return result;
        }
        if (s.matrixtype == 2)
        {

            //
            // SKS
            //
            ap.assert(s.m == s.n, "SparseExists: non-square SKS matrix not supported");
            if (i == j)
            {

                //
                // Return diagonal element
                //
                result = true;
                return result;
            }
            if (j < i)
            {

                //
                // Return subdiagonal element at I-th "skyline block"
                //
                if (i - j <= s.didx[i])
                {
                    result = true;
                }
            }
            else
            {

                //
                // Return superdiagonal element at J-th "skyline block"
                //
                if (j - i <= s.uidx[j])
                {
                    result = true;
                }
                return result;
            }
            return result;
        }
        ap.assert(false, "SparseExists: unexpected matrix type");
        return result;
    }


    /*************************************************************************
    This function returns I-th diagonal element of the sparse matrix.

    Matrix can be in any mode (Hash-Table or CRS storage), but this  function
    is most efficient for CRS matrices - it requires less than 50 CPU  cycles
    to extract diagonal element. For Hash-Table matrices we still  have  O(1)
    query time, but function is many times slower.

    INPUT PARAMETERS
        S           -   sparse M*N matrix in Hash-Table representation.
                        Exception will be thrown for CRS matrix.
        I           -   index of the element to modify, 0<=I<min(M,N)

    RESULT
        value of S[I,I] or zero (in case no element with such index is found)

      -- ALGLIB PROJECT --
         Copyright 14.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static double sparsegetdiagonal(sparsematrix s,
        int i,
        xparams _params)
    {
        double result = 0;

        ap.assert(i >= 0, "SparseGetDiagonal: I<0");
        ap.assert(i < s.m, "SparseGetDiagonal: I>=M");
        ap.assert(i < s.n, "SparseGetDiagonal: I>=N");
        result = 0;
        if (s.matrixtype == 0)
        {
            result = sparseget(s, i, i, _params);
            return result;
        }
        if (s.matrixtype == 1)
        {
            if (s.didx[i] != s.uidx[i])
            {
                result = s.vals[s.didx[i]];
            }
            return result;
        }
        if (s.matrixtype == 2)
        {
            ap.assert(s.m == s.n, "SparseGetDiagonal: non-square SKS matrix not supported");
            result = s.vals[s.ridx[i] + s.didx[i]];
            return result;
        }
        ap.assert(false, "SparseGetDiagonal: unexpected matrix type");
        return result;
    }


    /*************************************************************************
    This function calculates matrix-vector product  S*x.  Matrix  S  must  be
    stored in CRS or SKS format (exception will be thrown otherwise).

    INPUT PARAMETERS
        S           -   sparse M*N matrix in CRS or SKS format.
        X           -   array[N], input vector. For  performance  reasons  we 
                        make only quick checks - we check that array size  is
                        at least N, but we do not check for NAN's or INF's.
        Y           -   output buffer, possibly preallocated. In case  buffer
                        size is too small to store  result,  this  buffer  is
                        automatically resized.
        
    OUTPUT PARAMETERS
        Y           -   array[M], S*x
        
    NOTE: this function throws exception when called for non-CRS/SKS  matrix.
    You must convert your matrix with SparseConvertToCRS/SKS()  before  using
    this function.

      -- ALGLIB PROJECT --
         Copyright 14.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsemv(sparsematrix s,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        double tval = 0;
        double v = 0;
        double vv = 0;
        int i = 0;
        int j = 0;
        int lt = 0;
        int rt = 0;
        int lt1 = 0;
        int rt1 = 0;
        int n = 0;
        int m = 0;
        int d = 0;
        int u = 0;
        int ri = 0;
        int ri1 = 0;
        int i_ = 0;
        int i1_ = 0;

        ap.assert(ap.len(x) >= s.n, "SparseMV: length(X)<N");
        ap.assert(s.matrixtype == 1 || s.matrixtype == 2, "SparseMV: incorrect matrix type (convert your matrix to CRS/SKS)");
        apserv.rvectorsetlengthatleast(ref y, s.m, _params);
        n = s.n;
        m = s.m;
        if (s.matrixtype == 1)
        {

            //
            // CRS format.
            // Perform integrity check.
            //
            ap.assert(s.ninitialized == s.ridx[s.m], "SparseMV: some rows/elements of the CRS matrix were not initialized (you must initialize everything you promised to SparseCreateCRS)");

            //
            // Try vendor kernels
            //
            if (ablasmkl.sparsegemvcrsmkl(0, s.m, s.n, 1.0, s.vals, s.idx, s.ridx, x, 0, 0.0, y, 0, _params))
            {
                return;
            }

            //
            // Our own implementation
            //
            for (i = 0; i <= m - 1; i++)
            {
                tval = 0;
                lt = s.ridx[i];
                rt = s.ridx[i + 1] - 1;
                for (j = lt; j <= rt; j++)
                {
                    tval = tval + x[s.idx[j]] * s.vals[j];
                }
                y[i] = tval;
            }
            return;
        }
        if (s.matrixtype == 2)
        {

            //
            // SKS format
            //
            ap.assert(s.m == s.n, "SparseMV: non-square SKS matrices are not supported");
            for (i = 0; i <= n - 1; i++)
            {
                ri = s.ridx[i];
                ri1 = s.ridx[i + 1];
                d = s.didx[i];
                u = s.uidx[i];
                v = s.vals[ri + d] * x[i];
                if (d > 0)
                {
                    lt = ri;
                    rt = ri + d - 1;
                    lt1 = i - d;
                    rt1 = i - 1;
                    i1_ = (lt1) - (lt);
                    vv = 0.0;
                    for (i_ = lt; i_ <= rt; i_++)
                    {
                        vv += s.vals[i_] * x[i_ + i1_];
                    }
                    v = v + vv;
                }
                y[i] = v;
                if (u > 0)
                {
                    ablasf.raddvx(u, x[i], s.vals, ri1 - u, y, i - u, _params);
                }
            }
            apserv.touchint(ref rt1, _params);
            return;
        }
    }


    /*************************************************************************
    This function calculates matrix-vector product  S^T*x. Matrix S  must  be
    stored in CRS or SKS format (exception will be thrown otherwise).

    INPUT PARAMETERS
        S           -   sparse M*N matrix in CRS or SKS format.
        X           -   array[M], input vector. For  performance  reasons  we 
                        make only quick checks - we check that array size  is
                        at least M, but we do not check for NAN's or INF's.
        Y           -   output buffer, possibly preallocated. In case  buffer
                        size is too small to store  result,  this  buffer  is
                        automatically resized.
        
    OUTPUT PARAMETERS
        Y           -   array[N], S^T*x
        
    NOTE: this function throws exception when called for non-CRS/SKS  matrix.
    You must convert your matrix with SparseConvertToCRS/SKS()  before  using
    this function.

      -- ALGLIB PROJECT --
         Copyright 14.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsemtv(sparsematrix s,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int lt = 0;
        int rt = 0;
        int ct = 0;
        int lt1 = 0;
        int rt1 = 0;
        double v = 0;
        double vv = 0;
        int n = 0;
        int m = 0;
        int ri = 0;
        int ri1 = 0;
        int d = 0;
        int u = 0;
        int i_ = 0;
        int i1_ = 0;

        ap.assert(s.matrixtype == 1 || s.matrixtype == 2, "SparseMTV: incorrect matrix type (convert your matrix to CRS/SKS)");
        ap.assert(ap.len(x) >= s.m, "SparseMTV: Length(X)<M");
        n = s.n;
        m = s.m;
        apserv.rvectorsetlengthatleast(ref y, n, _params);
        for (i = 0; i <= n - 1; i++)
        {
            y[i] = 0;
        }
        if (s.matrixtype == 1)
        {

            //
            // CRS format
            // Perform integrity check.
            //
            ap.assert(s.ninitialized == s.ridx[m], "SparseMTV: some rows/elements of the CRS matrix were not initialized (you must initialize everything you promised to SparseCreateCRS)");

            //
            // Try vendor kernels
            //
            if (ablasmkl.sparsegemvcrsmkl(1, s.m, s.n, 1.0, s.vals, s.idx, s.ridx, x, 0, 0.0, y, 0, _params))
            {
                return;
            }

            //
            // Our own implementation
            //
            for (i = 0; i <= m - 1; i++)
            {
                lt = s.ridx[i];
                rt = s.ridx[i + 1];
                v = x[i];
                for (j = lt; j <= rt - 1; j++)
                {
                    ct = s.idx[j];
                    y[ct] = y[ct] + v * s.vals[j];
                }
            }
            return;
        }
        if (s.matrixtype == 2)
        {

            //
            // SKS format
            //
            ap.assert(s.m == s.n, "SparseMV: non-square SKS matrices are not supported");
            for (i = 0; i <= n - 1; i++)
            {
                ri = s.ridx[i];
                ri1 = s.ridx[i + 1];
                d = s.didx[i];
                u = s.uidx[i];
                if (d > 0)
                {
                    lt = ri;
                    lt1 = i - d;
                    v = x[i];
                    ablasf.raddvx(d, v, s.vals, lt, y, lt1, _params);
                }
                v = s.vals[ri + d] * x[i];
                if (u > 0)
                {
                    lt = ri1 - u;
                    rt = ri1 - 1;
                    lt1 = i - u;
                    rt1 = i - 1;
                    i1_ = (lt1) - (lt);
                    vv = 0.0;
                    for (i_ = lt; i_ <= rt; i_++)
                    {
                        vv += s.vals[i_] * x[i_ + i1_];
                    }
                    v = v + vv;
                }
                y[i] = v;
            }
            apserv.touchint(ref rt1, _params);
            return;
        }
    }


    /*************************************************************************
    This function calculates generalized sparse matrix-vector product

        y := alpha*op(S)*x + beta*y

    Matrix S must be stored in CRS or SKS format (exception  will  be  thrown
    otherwise). op(S) can be either S or S^T.

    NOTE: this  function  expects  Y  to  be  large enough to store result. No
          automatic preallocation happens for smaller arrays.

    INPUT PARAMETERS
        S           -   sparse matrix in CRS or SKS format.
        Alpha       -   source coefficient
        OpS         -   operation type:
                        * OpS=0     =>  op(S) = S
                        * OpS=1     =>  op(S) = S^T
        X           -   input vector, must have at least Cols(op(S))+IX elements
        IX          -   subvector offset
        Beta        -   destination coefficient
        Y           -   preallocated output array, must have at least Rows(op(S))+IY elements
        IY          -   subvector offset
        
    OUTPUT PARAMETERS
        Y           -   elements [IY...IY+Rows(op(S))-1] are replaced by result,
                        other elements are not modified

    HANDLING OF SPECIAL CASES:
    * below M=Rows(op(S)) and N=Cols(op(S)). Although current  ALGLIB  version
      does not allow you to  create  zero-sized  sparse  matrices,  internally
      ALGLIB  can  deal  with  such matrices. So, comments for M or N equal to
      zero are for internal use only.
    * if M=0, then subroutine does nothing. It does not even touch arrays.
    * if N=0 or Alpha=0.0, then:
      * if Beta=0, then Y is filled by zeros. S and X are  not  referenced  at
        all. Initial values of Y are ignored (we do not  multiply  Y by  zero,
        we just rewrite it by zeros)
      * if Beta<>0, then Y is replaced by Beta*Y
    * if M>0, N>0, Alpha<>0, but  Beta=0, then  Y is replaced by alpha*op(S)*x
      initial state of Y  is ignored (rewritten without initial multiplication
      by zeros).
        
    NOTE: this function throws exception when called for non-CRS/SKS  matrix.
    You must convert your matrix with SparseConvertToCRS/SKS()  before  using
    this function.
         
      -- ALGLIB PROJECT --
         Copyright 10.12.2019 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsegemv(sparsematrix s,
        double alpha,
        int ops,
        double[] x,
        int ix,
        double beta,
        double[] y,
        int iy,
        xparams _params)
    {
        int opm = 0;
        int opn = 0;
        int rawm = 0;
        int rawn = 0;
        int i = 0;
        int j = 0;
        double tval = 0;
        int lt = 0;
        int rt = 0;
        int ct = 0;
        int d = 0;
        int u = 0;
        int ri = 0;
        int ri1 = 0;
        double v = 0;
        double vv = 0;
        int lt1 = 0;
        int rt1 = 0;
        int i_ = 0;
        int i1_ = 0;

        ap.assert(ops == 0 || ops == 1, "SparseGEMV: incorrect OpS");
        ap.assert(s.matrixtype == 1 || s.matrixtype == 2, "SparseGEMV: incorrect matrix type (convert your matrix to CRS/SKS)");
        if (ops == 0)
        {
            opm = s.m;
            opn = s.n;
        }
        else
        {
            opm = s.n;
            opn = s.m;
        }
        ap.assert(opm >= 0 && opn >= 0, "SparseGEMV: op(S) has negative size");
        ap.assert(opn == 0 || ap.len(x) + ix >= opn, "SparseGEMV: X is too short");
        ap.assert(opm == 0 || ap.len(y) + iy >= opm, "SparseGEMV: X is too short");
        rawm = s.m;
        rawn = s.n;

        //
        // Quick exit strategies
        //
        if (opm == 0)
        {
            return;
        }
        if ((double)(beta) != (double)(0))
        {
            for (i = 0; i <= opm - 1; i++)
            {
                y[iy + i] = beta * y[iy + i];
            }
        }
        else
        {
            for (i = 0; i <= opm - 1; i++)
            {
                y[iy + i] = 0.0;
            }
        }
        if (opn == 0 || (double)(alpha) == (double)(0))
        {
            return;
        }

        //
        // Now we have OpM>=1, OpN>=1, Alpha<>0
        //
        if (ops == 0)
        {

            //
            // Compute generalized product y := alpha*S*x + beta*y
            // (with "beta*y" part already computed).
            //
            if (s.matrixtype == 1)
            {

                //
                // CRS format.
                // Perform integrity check.
                //
                ap.assert(s.ninitialized == s.ridx[s.m], "SparseGEMV: some rows/elements of the CRS matrix were not initialized (you must initialize everything you promised to SparseCreateCRS)");

                //
                // Try vendor kernels
                //
                if (ablasmkl.sparsegemvcrsmkl(0, s.m, s.n, alpha, s.vals, s.idx, s.ridx, x, ix, 1.0, y, iy, _params))
                {
                    return;
                }

                //
                // Our own implementation
                //
                for (i = 0; i <= rawm - 1; i++)
                {
                    tval = 0;
                    lt = s.ridx[i];
                    rt = s.ridx[i + 1] - 1;
                    for (j = lt; j <= rt; j++)
                    {
                        tval = tval + x[s.idx[j] + ix] * s.vals[j];
                    }
                    y[i + iy] = alpha * tval + y[i + iy];
                }
                return;
            }
            if (s.matrixtype == 2)
            {

                //
                // SKS format
                //
                ap.assert(s.m == s.n, "SparseMV: non-square SKS matrices are not supported");
                for (i = 0; i <= rawn - 1; i++)
                {
                    ri = s.ridx[i];
                    ri1 = s.ridx[i + 1];
                    d = s.didx[i];
                    u = s.uidx[i];
                    v = s.vals[ri + d] * x[i + ix];
                    if (d > 0)
                    {
                        lt = ri;
                        rt = ri + d - 1;
                        lt1 = i - d + ix;
                        rt1 = i - 1 + ix;
                        i1_ = (lt1) - (lt);
                        vv = 0.0;
                        for (i_ = lt; i_ <= rt; i_++)
                        {
                            vv += s.vals[i_] * x[i_ + i1_];
                        }
                        v = v + vv;
                    }
                    y[i + iy] = alpha * v + y[i + iy];
                    if (u > 0)
                    {
                        ablasf.raddvx(u, alpha * x[i + ix], s.vals, ri1 - u, y, i - u + iy, _params);
                    }
                }
                apserv.touchint(ref rt1, _params);
                return;
            }
        }
        else
        {

            //
            // Compute generalized product y := alpha*S^T*x + beta*y
            // (with "beta*y" part already computed).
            //
            if (s.matrixtype == 1)
            {

                //
                // CRS format
                // Perform integrity check.
                //
                ap.assert(s.ninitialized == s.ridx[s.m], "SparseGEMV: some rows/elements of the CRS matrix were not initialized (you must initialize everything you promised to SparseCreateCRS)");

                //
                // Try vendor kernels
                //
                if (ablasmkl.sparsegemvcrsmkl(1, s.m, s.n, alpha, s.vals, s.idx, s.ridx, x, ix, 1.0, y, iy, _params))
                {
                    return;
                }

                //
                // Our own implementation
                //
                for (i = 0; i <= rawm - 1; i++)
                {
                    lt = s.ridx[i];
                    rt = s.ridx[i + 1];
                    v = alpha * x[i + ix];
                    for (j = lt; j <= rt - 1; j++)
                    {
                        ct = s.idx[j] + iy;
                        y[ct] = y[ct] + v * s.vals[j];
                    }
                }
                return;
            }
            if (s.matrixtype == 2)
            {

                //
                // SKS format
                //
                ap.assert(s.m == s.n, "SparseGEMV: non-square SKS matrices are not supported");
                for (i = 0; i <= rawn - 1; i++)
                {
                    ri = s.ridx[i];
                    ri1 = s.ridx[i + 1];
                    d = s.didx[i];
                    u = s.uidx[i];
                    if (d > 0)
                    {
                        ablasf.raddvx(d, alpha * x[i + ix], s.vals, ri, y, i - d + iy, _params);
                    }
                    v = alpha * s.vals[ri + d] * x[i + ix];
                    if (u > 0)
                    {
                        lt = ri1 - u;
                        rt = ri1 - 1;
                        lt1 = i - u + ix;
                        rt1 = i - 1 + ix;
                        i1_ = (lt1) - (lt);
                        vv = 0.0;
                        for (i_ = lt; i_ <= rt; i_++)
                        {
                            vv += s.vals[i_] * x[i_ + i1_];
                        }
                        v = v + alpha * vv;
                    }
                    y[i + iy] = v + y[i + iy];
                }
                apserv.touchint(ref rt1, _params);
                return;
            }
        }
    }


    /*************************************************************************
    This function simultaneously calculates two matrix-vector products:
        S*x and S^T*x.
    S must be square (non-rectangular) matrix stored in  CRS  or  SKS  format
    (exception will be thrown otherwise).

    INPUT PARAMETERS
        S           -   sparse N*N matrix in CRS or SKS format.
        X           -   array[N], input vector. For  performance  reasons  we 
                        make only quick checks - we check that array size  is
                        at least N, but we do not check for NAN's or INF's.
        Y0          -   output buffer, possibly preallocated. In case  buffer
                        size is too small to store  result,  this  buffer  is
                        automatically resized.
        Y1          -   output buffer, possibly preallocated. In case  buffer
                        size is too small to store  result,  this  buffer  is
                        automatically resized.
        
    OUTPUT PARAMETERS
        Y0          -   array[N], S*x
        Y1          -   array[N], S^T*x
        
    NOTE: this function throws exception when called for non-CRS/SKS  matrix.
    You must convert your matrix with SparseConvertToCRS/SKS()  before  using
    this function.

      -- ALGLIB PROJECT --
         Copyright 14.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsemv2(sparsematrix s,
        double[] x,
        ref double[] y0,
        ref double[] y1,
        xparams _params)
    {
        int l = 0;
        double tval = 0;
        int i = 0;
        int j = 0;
        double vx = 0;
        double vs = 0;
        double v = 0;
        double vv = 0;
        double vd0 = 0;
        double vd1 = 0;
        int vi = 0;
        int j0 = 0;
        int j1 = 0;
        int n = 0;
        int ri = 0;
        int ri1 = 0;
        int d = 0;
        int u = 0;
        int lt = 0;
        int rt = 0;
        int lt1 = 0;
        int rt1 = 0;
        int i_ = 0;
        int i1_ = 0;

        ap.assert(s.matrixtype == 1 || s.matrixtype == 2, "SparseMV2: incorrect matrix type (convert your matrix to CRS/SKS)");
        ap.assert(s.m == s.n, "SparseMV2: matrix is non-square");
        l = ap.len(x);
        ap.assert(l >= s.n, "SparseMV2: Length(X)<N");
        n = s.n;
        apserv.rvectorsetlengthatleast(ref y0, l, _params);
        apserv.rvectorsetlengthatleast(ref y1, l, _params);
        for (i = 0; i <= n - 1; i++)
        {
            y0[i] = 0;
            y1[i] = 0;
        }
        if (s.matrixtype == 1)
        {

            //
            // CRS format
            //
            ap.assert(s.ninitialized == s.ridx[s.m], "SparseMV2: some rows/elements of the CRS matrix were not initialized (you must initialize everything you promised to SparseCreateCRS)");
            for (i = 0; i <= s.m - 1; i++)
            {
                tval = 0;
                vx = x[i];
                j0 = s.ridx[i];
                j1 = s.ridx[i + 1] - 1;
                for (j = j0; j <= j1; j++)
                {
                    vi = s.idx[j];
                    vs = s.vals[j];
                    tval = tval + x[vi] * vs;
                    y1[vi] = y1[vi] + vx * vs;
                }
                y0[i] = tval;
            }
            return;
        }
        if (s.matrixtype == 2)
        {

            //
            // SKS format
            //
            for (i = 0; i <= n - 1; i++)
            {
                ri = s.ridx[i];
                ri1 = s.ridx[i + 1];
                d = s.didx[i];
                u = s.uidx[i];
                vd0 = s.vals[ri + d] * x[i];
                vd1 = vd0;
                if (d > 0)
                {
                    lt = ri;
                    rt = ri + d - 1;
                    lt1 = i - d;
                    rt1 = i - 1;
                    v = x[i];
                    i1_ = (lt) - (lt1);
                    for (i_ = lt1; i_ <= rt1; i_++)
                    {
                        y1[i_] = y1[i_] + v * s.vals[i_ + i1_];
                    }
                    i1_ = (lt1) - (lt);
                    vv = 0.0;
                    for (i_ = lt; i_ <= rt; i_++)
                    {
                        vv += s.vals[i_] * x[i_ + i1_];
                    }
                    vd0 = vd0 + vv;
                }
                if (u > 0)
                {
                    lt = ri1 - u;
                    rt = ri1 - 1;
                    lt1 = i - u;
                    rt1 = i - 1;
                    v = x[i];
                    i1_ = (lt) - (lt1);
                    for (i_ = lt1; i_ <= rt1; i_++)
                    {
                        y0[i_] = y0[i_] + v * s.vals[i_ + i1_];
                    }
                    i1_ = (lt1) - (lt);
                    vv = 0.0;
                    for (i_ = lt; i_ <= rt; i_++)
                    {
                        vv += s.vals[i_] * x[i_ + i1_];
                    }
                    vd1 = vd1 + vv;
                }
                y0[i] = vd0;
                y1[i] = vd1;
            }
            return;
        }
    }


    /*************************************************************************
    This function calculates matrix-vector product  S*x, when S is  symmetric
    matrix. Matrix S  must be stored in CRS or SKS format  (exception will be
    thrown otherwise).

    INPUT PARAMETERS
        S           -   sparse M*M matrix in CRS or SKS format.
        IsUpper     -   whether upper or lower triangle of S is given:
                        * if upper triangle is given,  only   S[i,j] for j>=i
                          are used, and lower triangle is ignored (it can  be
                          empty - these elements are not referenced at all).
                        * if lower triangle is given,  only   S[i,j] for j<=i
                          are used, and upper triangle is ignored.
        X           -   array[N], input vector. For  performance  reasons  we 
                        make only quick checks - we check that array size  is
                        at least N, but we do not check for NAN's or INF's.
        Y           -   output buffer, possibly preallocated. In case  buffer
                        size is too small to store  result,  this  buffer  is
                        automatically resized.
        
    OUTPUT PARAMETERS
        Y           -   array[M], S*x
        
    NOTE: this function throws exception when called for non-CRS/SKS  matrix.
    You must convert your matrix with SparseConvertToCRS/SKS()  before  using
    this function.

      -- ALGLIB PROJECT --
         Copyright 14.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsesmv(sparsematrix s,
        bool isupper,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        int n = 0;
        int i = 0;
        int j = 0;
        int id = 0;
        int lt = 0;
        int rt = 0;
        double v = 0;
        double vv = 0;
        double vy = 0;
        double vx = 0;
        double vd = 0;
        int ri = 0;
        int ri1 = 0;
        int d = 0;
        int u = 0;
        int lt1 = 0;
        int rt1 = 0;
        int i_ = 0;
        int i1_ = 0;

        ap.assert(s.matrixtype == 1 || s.matrixtype == 2, "SparseSMV: incorrect matrix type (convert your matrix to CRS/SKS)");
        ap.assert(ap.len(x) >= s.n, "SparseSMV: length(X)<N");
        ap.assert(s.m == s.n, "SparseSMV: non-square matrix");
        n = s.n;
        apserv.rvectorsetlengthatleast(ref y, n, _params);
        for (i = 0; i <= n - 1; i++)
        {
            y[i] = 0;
        }
        if (s.matrixtype == 1)
        {

            //
            // CRS format
            //
            ap.assert(s.ninitialized == s.ridx[s.m], "SparseSMV: some rows/elements of the CRS matrix were not initialized (you must initialize everything you promised to SparseCreateCRS)");
            for (i = 0; i <= n - 1; i++)
            {
                if (s.didx[i] != s.uidx[i])
                {
                    y[i] = y[i] + s.vals[s.didx[i]] * x[s.idx[s.didx[i]]];
                }
                if (isupper)
                {
                    lt = s.uidx[i];
                    rt = s.ridx[i + 1];
                    vy = 0;
                    vx = x[i];
                    for (j = lt; j <= rt - 1; j++)
                    {
                        id = s.idx[j];
                        v = s.vals[j];
                        vy = vy + x[id] * v;
                        y[id] = y[id] + vx * v;
                    }
                    y[i] = y[i] + vy;
                }
                else
                {
                    lt = s.ridx[i];
                    rt = s.didx[i];
                    vy = 0;
                    vx = x[i];
                    for (j = lt; j <= rt - 1; j++)
                    {
                        id = s.idx[j];
                        v = s.vals[j];
                        vy = vy + x[id] * v;
                        y[id] = y[id] + vx * v;
                    }
                    y[i] = y[i] + vy;
                }
            }
            return;
        }
        if (s.matrixtype == 2)
        {

            //
            // SKS format
            //
            for (i = 0; i <= n - 1; i++)
            {
                ri = s.ridx[i];
                ri1 = s.ridx[i + 1];
                d = s.didx[i];
                u = s.uidx[i];
                vd = s.vals[ri + d] * x[i];
                if (d > 0 && !isupper)
                {
                    lt = ri;
                    rt = ri + d - 1;
                    lt1 = i - d;
                    rt1 = i - 1;
                    v = x[i];
                    i1_ = (lt) - (lt1);
                    for (i_ = lt1; i_ <= rt1; i_++)
                    {
                        y[i_] = y[i_] + v * s.vals[i_ + i1_];
                    }
                    i1_ = (lt1) - (lt);
                    vv = 0.0;
                    for (i_ = lt; i_ <= rt; i_++)
                    {
                        vv += s.vals[i_] * x[i_ + i1_];
                    }
                    vd = vd + vv;
                }
                if (u > 0 && isupper)
                {
                    lt = ri1 - u;
                    rt = ri1 - 1;
                    lt1 = i - u;
                    rt1 = i - 1;
                    v = x[i];
                    i1_ = (lt) - (lt1);
                    for (i_ = lt1; i_ <= rt1; i_++)
                    {
                        y[i_] = y[i_] + v * s.vals[i_ + i1_];
                    }
                    i1_ = (lt1) - (lt);
                    vv = 0.0;
                    for (i_ = lt; i_ <= rt; i_++)
                    {
                        vv += s.vals[i_] * x[i_ + i1_];
                    }
                    vd = vd + vv;
                }
                y[i] = vd;
            }
            return;
        }
    }


    /*************************************************************************
    This function calculates vector-matrix-vector product x'*S*x, where  S is
    symmetric matrix. Matrix S must be stored in CRS or SKS format (exception
    will be thrown otherwise).

    INPUT PARAMETERS
        S           -   sparse M*M matrix in CRS or SKS format.
        IsUpper     -   whether upper or lower triangle of S is given:
                        * if upper triangle is given,  only   S[i,j] for j>=i
                          are used, and lower triangle is ignored (it can  be
                          empty - these elements are not referenced at all).
                        * if lower triangle is given,  only   S[i,j] for j<=i
                          are used, and upper triangle is ignored.
        X           -   array[N], input vector. For  performance  reasons  we 
                        make only quick checks - we check that array size  is
                        at least N, but we do not check for NAN's or INF's.
        
    RESULT
        x'*S*x
        
    NOTE: this function throws exception when called for non-CRS/SKS  matrix.
    You must convert your matrix with SparseConvertToCRS/SKS()  before  using
    this function.

      -- ALGLIB PROJECT --
         Copyright 27.01.2014 by Bochkanov Sergey
    *************************************************************************/
    public static double sparsevsmv(sparsematrix s,
        bool isupper,
        double[] x,
        xparams _params)
    {
        double result = 0;
        int n = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        int id = 0;
        int lt = 0;
        int rt = 0;
        double v = 0;
        double v0 = 0;
        double v1 = 0;
        int ri = 0;
        int ri1 = 0;
        int d = 0;
        int u = 0;
        int lt1 = 0;

        ap.assert(s.matrixtype == 1 || s.matrixtype == 2, "SparseVSMV: incorrect matrix type (convert your matrix to CRS/SKS)");
        ap.assert(ap.len(x) >= s.n, "SparseVSMV: length(X)<N");
        ap.assert(s.m == s.n, "SparseVSMV: non-square matrix");
        n = s.n;
        result = 0.0;
        if (s.matrixtype == 1)
        {

            //
            // CRS format
            //
            ap.assert(s.ninitialized == s.ridx[s.m], "SparseVSMV: some rows/elements of the CRS matrix were not initialized (you must initialize everything you promised to SparseCreateCRS)");
            for (i = 0; i <= n - 1; i++)
            {
                if (s.didx[i] != s.uidx[i])
                {
                    v = x[s.idx[s.didx[i]]];
                    result = result + v * s.vals[s.didx[i]] * v;
                }
                if (isupper)
                {
                    lt = s.uidx[i];
                    rt = s.ridx[i + 1];
                }
                else
                {
                    lt = s.ridx[i];
                    rt = s.didx[i];
                }
                v0 = x[i];
                for (j = lt; j <= rt - 1; j++)
                {
                    id = s.idx[j];
                    v1 = x[id];
                    v = s.vals[j];
                    result = result + 2 * v0 * v1 * v;
                }
            }
            return result;
        }
        if (s.matrixtype == 2)
        {

            //
            // SKS format
            //
            for (i = 0; i <= n - 1; i++)
            {
                ri = s.ridx[i];
                ri1 = s.ridx[i + 1];
                d = s.didx[i];
                u = s.uidx[i];
                v = x[i];
                result = result + v * s.vals[ri + d] * v;
                if (d > 0 && !isupper)
                {
                    lt = ri;
                    lt1 = i - d;
                    k = d - 1;
                    v0 = x[i];
                    v = 0.0;
                    for (j = 0; j <= k; j++)
                    {
                        v = v + x[lt1 + j] * s.vals[lt + j];
                    }
                    result = result + 2.0 * v0 * v;
                }
                if (u > 0 && isupper)
                {
                    lt = ri1 - u;
                    lt1 = i - u;
                    k = u - 1;
                    v0 = x[i];
                    v = 0.0;
                    for (j = 0; j <= k; j++)
                    {
                        v = v + x[lt1 + j] * s.vals[lt + j];
                    }
                    result = result + 2.0 * v0 * v;
                }
            }
            return result;
        }
        return result;
    }


    /*************************************************************************
    This function calculates matrix-matrix product  S*A.  Matrix  S  must  be
    stored in CRS or SKS format (exception will be thrown otherwise).

    INPUT PARAMETERS
        S           -   sparse M*N matrix in CRS or SKS format.
        A           -   array[N][K], input dense matrix. For  performance reasons
                        we make only quick checks - we check that array size  
                        is at least N, but we do not check for NAN's or INF's.
        K           -   number of columns of matrix (A).
        B           -   output buffer, possibly preallocated. In case  buffer
                        size is too small to store  result,  this  buffer  is
                        automatically resized.
        
    OUTPUT PARAMETERS
        B           -   array[M][K], S*A
        
    NOTE: this function throws exception when called for non-CRS/SKS  matrix.
    You must convert your matrix with SparseConvertToCRS/SKS()  before  using
    this function.

      -- ALGLIB PROJECT --
         Copyright 14.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsemm(sparsematrix s,
        double[,] a,
        int k,
        ref double[,] b,
        xparams _params)
    {
        double tval = 0;
        double v = 0;
        int id = 0;
        int i = 0;
        int j = 0;
        int k0 = 0;
        int k1 = 0;
        int lt = 0;
        int rt = 0;
        int m = 0;
        int n = 0;
        int ri = 0;
        int ri1 = 0;
        int lt1 = 0;
        int rt1 = 0;
        int d = 0;
        int u = 0;
        double vd = 0;
        int i_ = 0;

        ap.assert(s.matrixtype == 1 || s.matrixtype == 2, "SparseMM: incorrect matrix type (convert your matrix to CRS/SKS)");
        ap.assert(ap.rows(a) >= s.n, "SparseMM: Rows(A)<N");
        ap.assert(k > 0, "SparseMM: K<=0");
        m = s.m;
        n = s.n;
        k1 = k - 1;
        apserv.rmatrixsetlengthatleast(ref b, m, k, _params);
        for (i = 0; i <= m - 1; i++)
        {
            for (j = 0; j <= k - 1; j++)
            {
                b[i, j] = 0;
            }
        }
        if (s.matrixtype == 1)
        {

            //
            // CRS format
            //
            ap.assert(s.ninitialized == s.ridx[m], "SparseMM: some rows/elements of the CRS matrix were not initialized (you must initialize everything you promised to SparseCreateCRS)");
            if (k < linalgswitch)
            {
                for (i = 0; i <= m - 1; i++)
                {
                    for (j = 0; j <= k - 1; j++)
                    {
                        tval = 0;
                        lt = s.ridx[i];
                        rt = s.ridx[i + 1];
                        for (k0 = lt; k0 <= rt - 1; k0++)
                        {
                            tval = tval + s.vals[k0] * a[s.idx[k0], j];
                        }
                        b[i, j] = tval;
                    }
                }
            }
            else
            {
                for (i = 0; i <= m - 1; i++)
                {
                    lt = s.ridx[i];
                    rt = s.ridx[i + 1];
                    for (j = lt; j <= rt - 1; j++)
                    {
                        id = s.idx[j];
                        v = s.vals[j];
                        for (i_ = 0; i_ <= k - 1; i_++)
                        {
                            b[i, i_] = b[i, i_] + v * a[id, i_];
                        }
                    }
                }
            }
            return;
        }
        if (s.matrixtype == 2)
        {

            //
            // SKS format
            //
            ap.assert(m == n, "SparseMM: non-square SKS matrices are not supported");
            for (i = 0; i <= n - 1; i++)
            {
                ri = s.ridx[i];
                ri1 = s.ridx[i + 1];
                d = s.didx[i];
                u = s.uidx[i];
                if (d > 0)
                {
                    lt = ri;
                    lt1 = i - d;
                    rt1 = i - 1;
                    for (j = lt1; j <= rt1; j++)
                    {
                        v = s.vals[lt + (j - lt1)];
                        if (k < linalgswitch)
                        {

                            //
                            // Use loop
                            //
                            for (k0 = 0; k0 <= k1; k0++)
                            {
                                b[i, k0] = b[i, k0] + v * a[j, k0];
                            }
                        }
                        else
                        {

                            //
                            // Use vector operation
                            //
                            for (i_ = 0; i_ <= k - 1; i_++)
                            {
                                b[i, i_] = b[i, i_] + v * a[j, i_];
                            }
                        }
                    }
                }
                if (u > 0)
                {
                    lt = ri1 - u;
                    lt1 = i - u;
                    rt1 = i - 1;
                    for (j = lt1; j <= rt1; j++)
                    {
                        v = s.vals[lt + (j - lt1)];
                        if (k < linalgswitch)
                        {

                            //
                            // Use loop
                            //
                            for (k0 = 0; k0 <= k1; k0++)
                            {
                                b[j, k0] = b[j, k0] + v * a[i, k0];
                            }
                        }
                        else
                        {

                            //
                            // Use vector operation
                            //
                            for (i_ = 0; i_ <= k - 1; i_++)
                            {
                                b[j, i_] = b[j, i_] + v * a[i, i_];
                            }
                        }
                    }
                }
                vd = s.vals[ri + d];
                for (i_ = 0; i_ <= k - 1; i_++)
                {
                    b[i, i_] = b[i, i_] + vd * a[i, i_];
                }
            }
            return;
        }
    }


    /*************************************************************************
    This function calculates matrix-matrix product  S^T*A. Matrix S  must  be
    stored in CRS or SKS format (exception will be thrown otherwise).

    INPUT PARAMETERS
        S           -   sparse M*N matrix in CRS or SKS format.
        A           -   array[M][K], input dense matrix. For performance reasons
                        we make only quick checks - we check that array size  is
                        at least M, but we do not check for NAN's or INF's.
        K           -   number of columns of matrix (A).                    
        B           -   output buffer, possibly preallocated. In case  buffer
                        size is too small to store  result,  this  buffer  is
                        automatically resized.
        
    OUTPUT PARAMETERS
        B           -   array[N][K], S^T*A
        
    NOTE: this function throws exception when called for non-CRS/SKS  matrix.
    You must convert your matrix with SparseConvertToCRS/SKS()  before  using
    this function.

      -- ALGLIB PROJECT --
         Copyright 14.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsemtm(sparsematrix s,
        double[,] a,
        int k,
        ref double[,] b,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k0 = 0;
        int k1 = 0;
        int lt = 0;
        int rt = 0;
        int ct = 0;
        double v = 0;
        int m = 0;
        int n = 0;
        int ri = 0;
        int ri1 = 0;
        int lt1 = 0;
        int rt1 = 0;
        int d = 0;
        int u = 0;
        int i_ = 0;

        ap.assert(s.matrixtype == 1 || s.matrixtype == 2, "SparseMTM: incorrect matrix type (convert your matrix to CRS/SKS)");
        ap.assert(ap.rows(a) >= s.m, "SparseMTM: Rows(A)<M");
        ap.assert(k > 0, "SparseMTM: K<=0");
        m = s.m;
        n = s.n;
        k1 = k - 1;
        apserv.rmatrixsetlengthatleast(ref b, n, k, _params);
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= k - 1; j++)
            {
                b[i, j] = 0;
            }
        }
        if (s.matrixtype == 1)
        {

            //
            // CRS format
            //
            ap.assert(s.ninitialized == s.ridx[m], "SparseMTM: some rows/elements of the CRS matrix were not initialized (you must initialize everything you promised to SparseCreateCRS)");
            if (k < linalgswitch)
            {
                for (i = 0; i <= m - 1; i++)
                {
                    lt = s.ridx[i];
                    rt = s.ridx[i + 1];
                    for (k0 = lt; k0 <= rt - 1; k0++)
                    {
                        v = s.vals[k0];
                        ct = s.idx[k0];
                        for (j = 0; j <= k - 1; j++)
                        {
                            b[ct, j] = b[ct, j] + v * a[i, j];
                        }
                    }
                }
            }
            else
            {
                for (i = 0; i <= m - 1; i++)
                {
                    lt = s.ridx[i];
                    rt = s.ridx[i + 1];
                    for (j = lt; j <= rt - 1; j++)
                    {
                        v = s.vals[j];
                        ct = s.idx[j];
                        for (i_ = 0; i_ <= k - 1; i_++)
                        {
                            b[ct, i_] = b[ct, i_] + v * a[i, i_];
                        }
                    }
                }
            }
            return;
        }
        if (s.matrixtype == 2)
        {

            //
            // SKS format
            //
            ap.assert(m == n, "SparseMTM: non-square SKS matrices are not supported");
            for (i = 0; i <= n - 1; i++)
            {
                ri = s.ridx[i];
                ri1 = s.ridx[i + 1];
                d = s.didx[i];
                u = s.uidx[i];
                if (d > 0)
                {
                    lt = ri;
                    lt1 = i - d;
                    rt1 = i - 1;
                    for (j = lt1; j <= rt1; j++)
                    {
                        v = s.vals[lt + (j - lt1)];
                        if (k < linalgswitch)
                        {

                            //
                            // Use loop
                            //
                            for (k0 = 0; k0 <= k1; k0++)
                            {
                                b[j, k0] = b[j, k0] + v * a[i, k0];
                            }
                        }
                        else
                        {

                            //
                            // Use vector operation
                            //
                            for (i_ = 0; i_ <= k - 1; i_++)
                            {
                                b[j, i_] = b[j, i_] + v * a[i, i_];
                            }
                        }
                    }
                }
                if (u > 0)
                {
                    lt = ri1 - u;
                    lt1 = i - u;
                    rt1 = i - 1;
                    for (j = lt1; j <= rt1; j++)
                    {
                        v = s.vals[lt + (j - lt1)];
                        if (k < linalgswitch)
                        {

                            //
                            // Use loop
                            //
                            for (k0 = 0; k0 <= k1; k0++)
                            {
                                b[i, k0] = b[i, k0] + v * a[j, k0];
                            }
                        }
                        else
                        {

                            //
                            // Use vector operation
                            //
                            for (i_ = 0; i_ <= k - 1; i_++)
                            {
                                b[i, i_] = b[i, i_] + v * a[j, i_];
                            }
                        }
                    }
                }
                v = s.vals[ri + d];
                for (i_ = 0; i_ <= k - 1; i_++)
                {
                    b[i, i_] = b[i, i_] + v * a[i, i_];
                }
            }
            return;
        }
    }


    /*************************************************************************
    This function simultaneously calculates two matrix-matrix products:
        S*A and S^T*A.
    S  must  be  square (non-rectangular) matrix stored in CRS or  SKS  format
    (exception will be thrown otherwise).

    INPUT PARAMETERS
        S           -   sparse N*N matrix in CRS or SKS format.
        A           -   array[N][K], input dense matrix. For performance reasons
                        we make only quick checks - we check that array size  is
                        at least N, but we do not check for NAN's or INF's.
        K           -   number of columns of matrix (A).                    
        B0          -   output buffer, possibly preallocated. In case  buffer
                        size is too small to store  result,  this  buffer  is
                        automatically resized.
        B1          -   output buffer, possibly preallocated. In case  buffer
                        size is too small to store  result,  this  buffer  is
                        automatically resized.
        
    OUTPUT PARAMETERS
        B0          -   array[N][K], S*A
        B1          -   array[N][K], S^T*A
        
    NOTE: this function throws exception when called for non-CRS/SKS  matrix.
    You must convert your matrix with SparseConvertToCRS/SKS()  before  using
    this function.

      -- ALGLIB PROJECT --
         Copyright 14.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsemm2(sparsematrix s,
        double[,] a,
        int k,
        ref double[,] b0,
        ref double[,] b1,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k0 = 0;
        int lt = 0;
        int rt = 0;
        int ct = 0;
        double v = 0;
        double tval = 0;
        int n = 0;
        int k1 = 0;
        int ri = 0;
        int ri1 = 0;
        int lt1 = 0;
        int rt1 = 0;
        int d = 0;
        int u = 0;
        int i_ = 0;

        ap.assert(s.matrixtype == 1 || s.matrixtype == 2, "SparseMM2: incorrect matrix type (convert your matrix to CRS/SKS)");
        ap.assert(s.m == s.n, "SparseMM2: matrix is non-square");
        ap.assert(ap.rows(a) >= s.n, "SparseMM2: Rows(A)<N");
        ap.assert(k > 0, "SparseMM2: K<=0");
        n = s.n;
        k1 = k - 1;
        apserv.rmatrixsetlengthatleast(ref b0, n, k, _params);
        apserv.rmatrixsetlengthatleast(ref b1, n, k, _params);
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= k - 1; j++)
            {
                b1[i, j] = 0;
                b0[i, j] = 0;
            }
        }
        if (s.matrixtype == 1)
        {

            //
            // CRS format
            //
            ap.assert(s.ninitialized == s.ridx[s.m], "SparseMM2: some rows/elements of the CRS matrix were not initialized (you must initialize everything you promised to SparseCreateCRS)");
            if (k < linalgswitch)
            {
                for (i = 0; i <= n - 1; i++)
                {
                    for (j = 0; j <= k - 1; j++)
                    {
                        tval = 0;
                        lt = s.ridx[i];
                        rt = s.ridx[i + 1];
                        v = a[i, j];
                        for (k0 = lt; k0 <= rt - 1; k0++)
                        {
                            ct = s.idx[k0];
                            b1[ct, j] = b1[ct, j] + s.vals[k0] * v;
                            tval = tval + s.vals[k0] * a[ct, j];
                        }
                        b0[i, j] = tval;
                    }
                }
            }
            else
            {
                for (i = 0; i <= n - 1; i++)
                {
                    lt = s.ridx[i];
                    rt = s.ridx[i + 1];
                    for (j = lt; j <= rt - 1; j++)
                    {
                        v = s.vals[j];
                        ct = s.idx[j];
                        for (i_ = 0; i_ <= k - 1; i_++)
                        {
                            b0[i, i_] = b0[i, i_] + v * a[ct, i_];
                        }
                        for (i_ = 0; i_ <= k - 1; i_++)
                        {
                            b1[ct, i_] = b1[ct, i_] + v * a[i, i_];
                        }
                    }
                }
            }
            return;
        }
        if (s.matrixtype == 2)
        {

            //
            // SKS format
            //
            ap.assert(s.m == s.n, "SparseMM2: non-square SKS matrices are not supported");
            for (i = 0; i <= n - 1; i++)
            {
                ri = s.ridx[i];
                ri1 = s.ridx[i + 1];
                d = s.didx[i];
                u = s.uidx[i];
                if (d > 0)
                {
                    lt = ri;
                    lt1 = i - d;
                    rt1 = i - 1;
                    for (j = lt1; j <= rt1; j++)
                    {
                        v = s.vals[lt + (j - lt1)];
                        if (k < linalgswitch)
                        {

                            //
                            // Use loop
                            //
                            for (k0 = 0; k0 <= k1; k0++)
                            {
                                b0[i, k0] = b0[i, k0] + v * a[j, k0];
                                b1[j, k0] = b1[j, k0] + v * a[i, k0];
                            }
                        }
                        else
                        {

                            //
                            // Use vector operation
                            //
                            for (i_ = 0; i_ <= k - 1; i_++)
                            {
                                b0[i, i_] = b0[i, i_] + v * a[j, i_];
                            }
                            for (i_ = 0; i_ <= k - 1; i_++)
                            {
                                b1[j, i_] = b1[j, i_] + v * a[i, i_];
                            }
                        }
                    }
                }
                if (u > 0)
                {
                    lt = ri1 - u;
                    lt1 = i - u;
                    rt1 = i - 1;
                    for (j = lt1; j <= rt1; j++)
                    {
                        v = s.vals[lt + (j - lt1)];
                        if (k < linalgswitch)
                        {

                            //
                            // Use loop
                            //
                            for (k0 = 0; k0 <= k1; k0++)
                            {
                                b0[j, k0] = b0[j, k0] + v * a[i, k0];
                                b1[i, k0] = b1[i, k0] + v * a[j, k0];
                            }
                        }
                        else
                        {

                            //
                            // Use vector operation
                            //
                            for (i_ = 0; i_ <= k - 1; i_++)
                            {
                                b0[j, i_] = b0[j, i_] + v * a[i, i_];
                            }
                            for (i_ = 0; i_ <= k - 1; i_++)
                            {
                                b1[i, i_] = b1[i, i_] + v * a[j, i_];
                            }
                        }
                    }
                }
                v = s.vals[ri + d];
                for (i_ = 0; i_ <= k - 1; i_++)
                {
                    b0[i, i_] = b0[i, i_] + v * a[i, i_];
                }
                for (i_ = 0; i_ <= k - 1; i_++)
                {
                    b1[i, i_] = b1[i, i_] + v * a[i, i_];
                }
            }
            return;
        }
    }


    /*************************************************************************
    This function calculates matrix-matrix product  S*A, when S  is  symmetric
    matrix. Matrix S must be stored in CRS or SKS format  (exception  will  be
    thrown otherwise).

    INPUT PARAMETERS
        S           -   sparse M*M matrix in CRS or SKS format.
        IsUpper     -   whether upper or lower triangle of S is given:
                        * if upper triangle is given,  only   S[i,j] for j>=i
                          are used, and lower triangle is ignored (it can  be
                          empty - these elements are not referenced at all).
                        * if lower triangle is given,  only   S[i,j] for j<=i
                          are used, and upper triangle is ignored.
        A           -   array[N][K], input dense matrix. For performance reasons
                        we make only quick checks - we check that array size is
                        at least N, but we do not check for NAN's or INF's.
        K           -   number of columns of matrix (A).  
        B           -   output buffer, possibly preallocated. In case  buffer
                        size is too small to store  result,  this  buffer  is
                        automatically resized.
        
    OUTPUT PARAMETERS
        B           -   array[M][K], S*A
        
    NOTE: this function throws exception when called for non-CRS/SKS  matrix.
    You must convert your matrix with SparseConvertToCRS/SKS()  before  using
    this function.

      -- ALGLIB PROJECT --
         Copyright 14.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsesmm(sparsematrix s,
        bool isupper,
        double[,] a,
        int k,
        ref double[,] b,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k0 = 0;
        int id = 0;
        int k1 = 0;
        int lt = 0;
        int rt = 0;
        double v = 0;
        double vb = 0;
        double va = 0;
        int n = 0;
        int ri = 0;
        int ri1 = 0;
        int lt1 = 0;
        int rt1 = 0;
        int d = 0;
        int u = 0;
        int i_ = 0;

        ap.assert(s.matrixtype == 1 || s.matrixtype == 2, "SparseSMM: incorrect matrix type (convert your matrix to CRS/SKS)");
        ap.assert(ap.rows(a) >= s.n, "SparseSMM: Rows(X)<N");
        ap.assert(s.m == s.n, "SparseSMM: matrix is non-square");
        n = s.n;
        k1 = k - 1;
        apserv.rmatrixsetlengthatleast(ref b, n, k, _params);
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= k - 1; j++)
            {
                b[i, j] = 0;
            }
        }
        if (s.matrixtype == 1)
        {

            //
            // CRS format
            //
            ap.assert(s.ninitialized == s.ridx[s.m], "SparseSMM: some rows/elements of the CRS matrix were not initialized (you must initialize everything you promised to SparseCreateCRS)");
            if (k > linalgswitch)
            {
                for (i = 0; i <= n - 1; i++)
                {
                    for (j = 0; j <= k - 1; j++)
                    {
                        if (s.didx[i] != s.uidx[i])
                        {
                            id = s.didx[i];
                            b[i, j] = b[i, j] + s.vals[id] * a[s.idx[id], j];
                        }
                        if (isupper)
                        {
                            lt = s.uidx[i];
                            rt = s.ridx[i + 1];
                            vb = 0;
                            va = a[i, j];
                            for (k0 = lt; k0 <= rt - 1; k0++)
                            {
                                id = s.idx[k0];
                                v = s.vals[k0];
                                vb = vb + a[id, j] * v;
                                b[id, j] = b[id, j] + va * v;
                            }
                            b[i, j] = b[i, j] + vb;
                        }
                        else
                        {
                            lt = s.ridx[i];
                            rt = s.didx[i];
                            vb = 0;
                            va = a[i, j];
                            for (k0 = lt; k0 <= rt - 1; k0++)
                            {
                                id = s.idx[k0];
                                v = s.vals[k0];
                                vb = vb + a[id, j] * v;
                                b[id, j] = b[id, j] + va * v;
                            }
                            b[i, j] = b[i, j] + vb;
                        }
                    }
                }
            }
            else
            {
                for (i = 0; i <= n - 1; i++)
                {
                    if (s.didx[i] != s.uidx[i])
                    {
                        id = s.didx[i];
                        v = s.vals[id];
                        for (i_ = 0; i_ <= k - 1; i_++)
                        {
                            b[i, i_] = b[i, i_] + v * a[s.idx[id], i_];
                        }
                    }
                    if (isupper)
                    {
                        lt = s.uidx[i];
                        rt = s.ridx[i + 1];
                        for (j = lt; j <= rt - 1; j++)
                        {
                            id = s.idx[j];
                            v = s.vals[j];
                            for (i_ = 0; i_ <= k - 1; i_++)
                            {
                                b[i, i_] = b[i, i_] + v * a[id, i_];
                            }
                            for (i_ = 0; i_ <= k - 1; i_++)
                            {
                                b[id, i_] = b[id, i_] + v * a[i, i_];
                            }
                        }
                    }
                    else
                    {
                        lt = s.ridx[i];
                        rt = s.didx[i];
                        for (j = lt; j <= rt - 1; j++)
                        {
                            id = s.idx[j];
                            v = s.vals[j];
                            for (i_ = 0; i_ <= k - 1; i_++)
                            {
                                b[i, i_] = b[i, i_] + v * a[id, i_];
                            }
                            for (i_ = 0; i_ <= k - 1; i_++)
                            {
                                b[id, i_] = b[id, i_] + v * a[i, i_];
                            }
                        }
                    }
                }
            }
            return;
        }
        if (s.matrixtype == 2)
        {

            //
            // SKS format
            //
            ap.assert(s.m == s.n, "SparseMM2: non-square SKS matrices are not supported");
            for (i = 0; i <= n - 1; i++)
            {
                ri = s.ridx[i];
                ri1 = s.ridx[i + 1];
                d = s.didx[i];
                u = s.uidx[i];
                if (d > 0 && !isupper)
                {
                    lt = ri;
                    lt1 = i - d;
                    rt1 = i - 1;
                    for (j = lt1; j <= rt1; j++)
                    {
                        v = s.vals[lt + (j - lt1)];
                        if (k < linalgswitch)
                        {

                            //
                            // Use loop
                            //
                            for (k0 = 0; k0 <= k1; k0++)
                            {
                                b[i, k0] = b[i, k0] + v * a[j, k0];
                                b[j, k0] = b[j, k0] + v * a[i, k0];
                            }
                        }
                        else
                        {

                            //
                            // Use vector operation
                            //
                            for (i_ = 0; i_ <= k - 1; i_++)
                            {
                                b[i, i_] = b[i, i_] + v * a[j, i_];
                            }
                            for (i_ = 0; i_ <= k - 1; i_++)
                            {
                                b[j, i_] = b[j, i_] + v * a[i, i_];
                            }
                        }
                    }
                }
                if (u > 0 && isupper)
                {
                    lt = ri1 - u;
                    lt1 = i - u;
                    rt1 = i - 1;
                    for (j = lt1; j <= rt1; j++)
                    {
                        v = s.vals[lt + (j - lt1)];
                        if (k < linalgswitch)
                        {

                            //
                            // Use loop
                            //
                            for (k0 = 0; k0 <= k1; k0++)
                            {
                                b[j, k0] = b[j, k0] + v * a[i, k0];
                                b[i, k0] = b[i, k0] + v * a[j, k0];
                            }
                        }
                        else
                        {

                            //
                            // Use vector operation
                            //
                            for (i_ = 0; i_ <= k - 1; i_++)
                            {
                                b[j, i_] = b[j, i_] + v * a[i, i_];
                            }
                            for (i_ = 0; i_ <= k - 1; i_++)
                            {
                                b[i, i_] = b[i, i_] + v * a[j, i_];
                            }
                        }
                    }
                }
                v = s.vals[ri + d];
                for (i_ = 0; i_ <= k - 1; i_++)
                {
                    b[i, i_] = b[i, i_] + v * a[i, i_];
                }
            }
            return;
        }
    }


    /*************************************************************************
    This function calculates matrix-vector product op(S)*x, when x is  vector,
    S is symmetric triangular matrix, op(S) is transposition or no  operation.
    Matrix S must be stored in CRS or SKS format  (exception  will  be  thrown
    otherwise).

    INPUT PARAMETERS
        S           -   sparse square matrix in CRS or SKS format.
        IsUpper     -   whether upper or lower triangle of S is used:
                        * if upper triangle is given,  only   S[i,j] for  j>=i
                          are used, and lower triangle is  ignored (it can  be
                          empty - these elements are not referenced at all).
                        * if lower triangle is given,  only   S[i,j] for  j<=i
                          are used, and upper triangle is ignored.
        IsUnit      -   unit or non-unit diagonal:
                        * if True, diagonal elements of triangular matrix  are
                          considered equal to 1.0. Actual elements  stored  in
                          S are not referenced at all.
                        * if False, diagonal stored in S is used
        OpType      -   operation type:
                        * if 0, S*x is calculated
                        * if 1, (S^T)*x is calculated (transposition)
        X           -   array[N] which stores input  vector.  For  performance
                        reasons we make only quick  checks  -  we  check  that
                        array  size  is  at  least  N, but we do not check for
                        NAN's or INF's.
        Y           -   possibly  preallocated  input   buffer.  Automatically 
                        resized if its size is too small.
        
    OUTPUT PARAMETERS
        Y           -   array[N], op(S)*x
        
    NOTE: this function throws exception when called for non-CRS/SKS  matrix.
    You must convert your matrix with SparseConvertToCRS/SKS()  before  using
    this function.

      -- ALGLIB PROJECT --
         Copyright 20.01.2014 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsetrmv(sparsematrix s,
        bool isupper,
        bool isunit,
        int optype,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        int n = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        int j0 = 0;
        int j1 = 0;
        double v = 0;
        int ri = 0;
        int ri1 = 0;
        int d = 0;
        int u = 0;
        int lt = 0;
        int rt = 0;
        int lt1 = 0;
        int rt1 = 0;
        int i_ = 0;
        int i1_ = 0;

        ap.assert(s.matrixtype == 1 || s.matrixtype == 2, "SparseTRMV: incorrect matrix type (convert your matrix to CRS/SKS)");
        ap.assert(optype == 0 || optype == 1, "SparseTRMV: incorrect operation type (must be 0 or 1)");
        ap.assert(ap.len(x) >= s.n, "SparseTRMV: Length(X)<N");
        ap.assert(s.m == s.n, "SparseTRMV: matrix is non-square");
        n = s.n;
        apserv.rvectorsetlengthatleast(ref y, n, _params);
        if (isunit)
        {

            //
            // Set initial value of y to x
            //
            for (i = 0; i <= n - 1; i++)
            {
                y[i] = x[i];
            }
        }
        else
        {

            //
            // Set initial value of y to 0
            //
            for (i = 0; i <= n - 1; i++)
            {
                y[i] = 0;
            }
        }
        if (s.matrixtype == 1)
        {

            //
            // CRS format
            //
            ap.assert(s.ninitialized == s.ridx[s.m], "SparseTRMV: some rows/elements of the CRS matrix were not initialized (you must initialize everything you promised to SparseCreateCRS)");
            for (i = 0; i <= n - 1; i++)
            {

                //
                // Depending on IsUpper/IsUnit, select range of indexes to process
                //
                if (isupper)
                {
                    if (isunit || s.didx[i] == s.uidx[i])
                    {
                        j0 = s.uidx[i];
                    }
                    else
                    {
                        j0 = s.didx[i];
                    }
                    j1 = s.ridx[i + 1] - 1;
                }
                else
                {
                    j0 = s.ridx[i];
                    if (isunit || s.didx[i] == s.uidx[i])
                    {
                        j1 = s.didx[i] - 1;
                    }
                    else
                    {
                        j1 = s.didx[i];
                    }
                }

                //
                // Depending on OpType, process subset of I-th row of input matrix
                //
                if (optype == 0)
                {
                    v = 0.0;
                    for (j = j0; j <= j1; j++)
                    {
                        v = v + s.vals[j] * x[s.idx[j]];
                    }
                    y[i] = y[i] + v;
                }
                else
                {
                    v = x[i];
                    for (j = j0; j <= j1; j++)
                    {
                        k = s.idx[j];
                        y[k] = y[k] + v * s.vals[j];
                    }
                }
            }
            return;
        }
        if (s.matrixtype == 2)
        {

            //
            // SKS format
            //
            ap.assert(s.m == s.n, "SparseTRMV: non-square SKS matrices are not supported");
            for (i = 0; i <= n - 1; i++)
            {
                ri = s.ridx[i];
                ri1 = s.ridx[i + 1];
                d = s.didx[i];
                u = s.uidx[i];
                if (!isunit)
                {
                    y[i] = y[i] + s.vals[ri + d] * x[i];
                }
                if (d > 0 && !isupper)
                {
                    lt = ri;
                    rt = ri + d - 1;
                    lt1 = i - d;
                    rt1 = i - 1;
                    if (optype == 0)
                    {
                        i1_ = (lt1) - (lt);
                        v = 0.0;
                        for (i_ = lt; i_ <= rt; i_++)
                        {
                            v += s.vals[i_] * x[i_ + i1_];
                        }
                        y[i] = y[i] + v;
                    }
                    else
                    {
                        v = x[i];
                        i1_ = (lt) - (lt1);
                        for (i_ = lt1; i_ <= rt1; i_++)
                        {
                            y[i_] = y[i_] + v * s.vals[i_ + i1_];
                        }
                    }
                }
                if (u > 0 && isupper)
                {
                    lt = ri1 - u;
                    rt = ri1 - 1;
                    lt1 = i - u;
                    rt1 = i - 1;
                    if (optype == 0)
                    {
                        v = x[i];
                        i1_ = (lt) - (lt1);
                        for (i_ = lt1; i_ <= rt1; i_++)
                        {
                            y[i_] = y[i_] + v * s.vals[i_ + i1_];
                        }
                    }
                    else
                    {
                        i1_ = (lt1) - (lt);
                        v = 0.0;
                        for (i_ = lt; i_ <= rt; i_++)
                        {
                            v += s.vals[i_] * x[i_ + i1_];
                        }
                        y[i] = y[i] + v;
                    }
                }
            }
            return;
        }
    }


    /*************************************************************************
    This function solves linear system op(S)*y=x  where  x  is  vector,  S  is
    symmetric  triangular  matrix,  op(S)  is  transposition  or no operation.
    Matrix S must be stored in CRS or SKS format  (exception  will  be  thrown
    otherwise).

    INPUT PARAMETERS
        S           -   sparse square matrix in CRS or SKS format.
        IsUpper     -   whether upper or lower triangle of S is used:
                        * if upper triangle is given,  only   S[i,j] for  j>=i
                          are used, and lower triangle is  ignored (it can  be
                          empty - these elements are not referenced at all).
                        * if lower triangle is given,  only   S[i,j] for  j<=i
                          are used, and upper triangle is ignored.
        IsUnit      -   unit or non-unit diagonal:
                        * if True, diagonal elements of triangular matrix  are
                          considered equal to 1.0. Actual elements  stored  in
                          S are not referenced at all.
                        * if False, diagonal stored in S is used. It  is  your
                          responsibility  to  make  sure  that   diagonal   is
                          non-zero.
        OpType      -   operation type:
                        * if 0, S*x is calculated
                        * if 1, (S^T)*x is calculated (transposition)
        X           -   array[N] which stores input  vector.  For  performance
                        reasons we make only quick  checks  -  we  check  that
                        array  size  is  at  least  N, but we do not check for
                        NAN's or INF's.
        
    OUTPUT PARAMETERS
        X           -   array[N], inv(op(S))*x
        
    NOTE: this function throws exception when called for  non-CRS/SKS  matrix.
          You must convert your matrix  with  SparseConvertToCRS/SKS()  before
          using this function.

    NOTE: no assertion or tests are done during algorithm  operation.   It  is
          your responsibility to provide invertible matrix to algorithm.

      -- ALGLIB PROJECT --
         Copyright 20.01.2014 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsetrsv(sparsematrix s,
        bool isupper,
        bool isunit,
        int optype,
        double[] x,
        xparams _params)
    {
        int n = 0;
        int fst = 0;
        int lst = 0;
        int stp = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        double v = 0;
        double vd = 0;
        double v0 = 0;
        int j0 = 0;
        int j1 = 0;
        int ri = 0;
        int ri1 = 0;
        int d = 0;
        int u = 0;
        int lt = 0;
        int lt1 = 0;

        ap.assert(s.matrixtype == 1 || s.matrixtype == 2, "SparseTRSV: incorrect matrix type (convert your matrix to CRS/SKS)");
        ap.assert(optype == 0 || optype == 1, "SparseTRSV: incorrect operation type (must be 0 or 1)");
        ap.assert(ap.len(x) >= s.n, "SparseTRSV: Length(X)<N");
        ap.assert(s.m == s.n, "SparseTRSV: matrix is non-square");
        n = s.n;
        if (s.matrixtype == 1)
        {

            //
            // CRS format.
            //
            // Several branches for different combinations of IsUpper and OpType
            //
            ap.assert(s.ninitialized == s.ridx[s.m], "SparseTRSV: some rows/elements of the CRS matrix were not initialized (you must initialize everything you promised to SparseCreateCRS)");
            if (optype == 0)
            {

                //
                // No transposition.
                //
                // S*x=y with upper or lower triangular S.
                //
                v0 = 0;
                if (isupper)
                {
                    fst = n - 1;
                    lst = 0;
                    stp = -1;
                }
                else
                {
                    fst = 0;
                    lst = n - 1;
                    stp = 1;
                }
                i = fst;
                while ((stp > 0 && i <= lst) || (stp < 0 && i >= lst))
                {

                    //
                    // Select range of indexes to process
                    //
                    if (isupper)
                    {
                        j0 = s.uidx[i];
                        j1 = s.ridx[i + 1] - 1;
                    }
                    else
                    {
                        j0 = s.ridx[i];
                        j1 = s.didx[i] - 1;
                    }

                    //
                    // Calculate X[I]
                    //
                    v = 0.0;
                    for (j = j0; j <= j1; j++)
                    {
                        v = v + s.vals[j] * x[s.idx[j]];
                    }
                    if (!isunit)
                    {
                        if (s.didx[i] == s.uidx[i])
                        {
                            vd = 0;
                        }
                        else
                        {
                            vd = s.vals[s.didx[i]];
                        }
                    }
                    else
                    {
                        vd = 1.0;
                    }
                    v = (x[i] - v) / vd;
                    x[i] = v;
                    v0 = 0.25 * v0 + v;

                    //
                    // Next I
                    //
                    i = i + stp;
                }
                ap.assert(math.isfinite(v0), "SparseTRSV: overflow or division by exact zero");
                return;
            }
            if (optype == 1)
            {

                //
                // Transposition.
                //
                // (S^T)*x=y with upper or lower triangular S.
                //
                if (isupper)
                {
                    fst = 0;
                    lst = n - 1;
                    stp = 1;
                }
                else
                {
                    fst = n - 1;
                    lst = 0;
                    stp = -1;
                }
                i = fst;
                v0 = 0;
                while ((stp > 0 && i <= lst) || (stp < 0 && i >= lst))
                {
                    v = x[i];
                    if (v != 0.0)
                    {

                        //
                        // X[i] already stores A[i,i]*Y[i], the only thing left
                        // is to divide by diagonal element.
                        //
                        if (!isunit)
                        {
                            if (s.didx[i] == s.uidx[i])
                            {
                                vd = 0;
                            }
                            else
                            {
                                vd = s.vals[s.didx[i]];
                            }
                        }
                        else
                        {
                            vd = 1.0;
                        }
                        v = v / vd;
                        x[i] = v;
                        v0 = 0.25 * v0 + v;

                        //
                        // For upper triangular case:
                        //     subtract X[i]*Ai from X[i+1:N-1]
                        //
                        // For lower triangular case:
                        //     subtract X[i]*Ai from X[0:i-1]
                        //
                        // (here Ai is I-th row of original, untransposed A).
                        //
                        if (isupper)
                        {
                            j0 = s.uidx[i];
                            j1 = s.ridx[i + 1] - 1;
                        }
                        else
                        {
                            j0 = s.ridx[i];
                            j1 = s.didx[i] - 1;
                        }
                        for (j = j0; j <= j1; j++)
                        {
                            k = s.idx[j];
                            x[k] = x[k] - s.vals[j] * v;
                        }
                    }

                    //
                    // Next I
                    //
                    i = i + stp;
                }
                ap.assert(math.isfinite(v0), "SparseTRSV: overflow or division by exact zero");
                return;
            }
            ap.assert(false, "SparseTRSV: internal error");
        }
        if (s.matrixtype == 2)
        {

            //
            // SKS format
            //
            ap.assert(s.m == s.n, "SparseTRSV: non-square SKS matrices are not supported");
            if ((optype == 0 && !isupper) || (optype == 1 && isupper))
            {

                //
                // Lower triangular op(S) (matrix itself can be upper triangular).
                //
                v0 = 0;
                for (i = 0; i <= n - 1; i++)
                {

                    //
                    // Select range of indexes to process
                    //
                    ri = s.ridx[i];
                    ri1 = s.ridx[i + 1];
                    d = s.didx[i];
                    u = s.uidx[i];
                    if (isupper)
                    {
                        lt = i - u;
                        lt1 = ri1 - u;
                        k = u - 1;
                    }
                    else
                    {
                        lt = i - d;
                        lt1 = ri;
                        k = d - 1;
                    }

                    //
                    // Calculate X[I]
                    //
                    v = 0.0;
                    for (j = 0; j <= k; j++)
                    {
                        v = v + s.vals[lt1 + j] * x[lt + j];
                    }
                    if (isunit)
                    {
                        vd = 1;
                    }
                    else
                    {
                        vd = s.vals[ri + d];
                    }
                    v = (x[i] - v) / vd;
                    x[i] = v;
                    v0 = 0.25 * v0 + v;
                }
                ap.assert(math.isfinite(v0), "SparseTRSV: overflow or division by exact zero");
                return;
            }
            if ((optype == 1 && !isupper) || (optype == 0 && isupper))
            {

                //
                // Upper triangular op(S) (matrix itself can be lower triangular).
                //
                v0 = 0;
                for (i = n - 1; i >= 0; i--)
                {
                    ri = s.ridx[i];
                    ri1 = s.ridx[i + 1];
                    d = s.didx[i];
                    u = s.uidx[i];

                    //
                    // X[i] already stores A[i,i]*Y[i], the only thing left
                    // is to divide by diagonal element.
                    //
                    if (isunit)
                    {
                        vd = 1;
                    }
                    else
                    {
                        vd = s.vals[ri + d];
                    }
                    v = x[i] / vd;
                    x[i] = v;
                    v0 = 0.25 * v0 + v;

                    //
                    // Subtract product of X[i] and I-th column of "effective" A from
                    // unprocessed variables.
                    //
                    v = x[i];
                    if (isupper)
                    {
                        lt = i - u;
                        lt1 = ri1 - u;
                        k = u - 1;
                    }
                    else
                    {
                        lt = i - d;
                        lt1 = ri;
                        k = d - 1;
                    }
                    for (j = 0; j <= k; j++)
                    {
                        x[lt + j] = x[lt + j] - v * s.vals[lt1 + j];
                    }
                }
                ap.assert(math.isfinite(v0), "SparseTRSV: overflow or division by exact zero");
                return;
            }
            ap.assert(false, "SparseTRSV: internal error");
        }
        ap.assert(false, "SparseTRSV: internal error");
    }


    /*************************************************************************
    This function applies permutation given by permutation table P (as opposed
    to product form of permutation) to sparse symmetric  matrix  A,  given  by
    either upper or lower triangle: B := P*A*P'.

    This function allocates completely new instance of B. Use buffered version
    SparseSymmPermTblBuf() if you want to reuse already allocated structure.

    INPUT PARAMETERS
        A           -   sparse square matrix in CRS format.
        IsUpper     -   whether upper or lower triangle of A is used:
                        * if upper triangle is given,  only   A[i,j] for  j>=i
                          are used, and lower triangle is  ignored (it can  be
                          empty - these elements are not referenced at all).
                        * if lower triangle is given,  only   A[i,j] for  j<=i
                          are used, and upper triangle is ignored.
        P           -   array[N] which stores permutation table;  P[I]=J means
                        that I-th row/column of matrix  A  is  moved  to  J-th
                        position. For performance reasons we do NOT check that
                        P[] is  a   correct   permutation  (that there  is  no
                        repetitions, just that all its elements  are  in [0,N)
                        range.
        
    OUTPUT PARAMETERS
        B           -   permuted matrix.  Permutation  is  applied  to A  from
                        the both sides, only upper or lower triangle (depending
                        on IsUpper) is stored.
        
    NOTE: this function throws exception when called for non-CRS  matrix.  You
          must convert your matrix with SparseConvertToCRS() before using this
          function.

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    public static void sparsesymmpermtbl(sparsematrix a,
        bool isupper,
        int[] p,
        sparsematrix b,
        xparams _params)
    {
        sparsesymmpermtblbuf(a, isupper, p, b, _params);
    }


    /*************************************************************************
    This function is a buffered version  of  SparseSymmPermTbl()  that  reuses
    previously allocated storage in B as much as possible.

    This function applies permutation given by permutation table P (as opposed
    to product form of permutation) to sparse symmetric  matrix  A,  given  by
    either upper or lower triangle: B := P*A*P'.

    INPUT PARAMETERS
        A           -   sparse square matrix in CRS format.
        IsUpper     -   whether upper or lower triangle of A is used:
                        * if upper triangle is given,  only   A[i,j] for  j>=i
                          are used, and lower triangle is  ignored (it can  be
                          empty - these elements are not referenced at all).
                        * if lower triangle is given,  only   A[i,j] for  j<=i
                          are used, and upper triangle is ignored.
        P           -   array[N] which stores permutation table;  P[I]=J means
                        that I-th row/column of matrix  A  is  moved  to  J-th
                        position. For performance reasons we do NOT check that
                        P[] is  a   correct   permutation  (that there  is  no
                        repetitions, just that all its elements  are  in [0,N)
                        range.
        B           -   sparse matrix object that will hold output.
                        Previously allocated memory will be reused as much  as
                        possible.
        
    OUTPUT PARAMETERS
        B           -   permuted matrix.  Permutation  is  applied  to A  from
                        the both sides, only upper or lower triangle (depending
                        on IsUpper) is stored.
        
    NOTE: this function throws exception when called for non-CRS  matrix.  You
          must convert your matrix with SparseConvertToCRS() before using this
          function.

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    public static void sparsesymmpermtblbuf(sparsematrix a,
        bool isupper,
        int[] p,
        sparsematrix b,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int jj = 0;
        int j0 = 0;
        int j1 = 0;
        int k0 = 0;
        int k1 = 0;
        int kk = 0;
        int n = 0;
        int dst = 0;
        bool bflag = new bool();

        ap.assert(a.matrixtype == 1, "SparseSymmPermTblBuf: incorrect matrix type (convert your matrix to CRS)");
        ap.assert(ap.len(p) >= a.n, "SparseSymmPermTblBuf: Length(P)<N");
        ap.assert(a.m == a.n, "SparseSymmPermTblBuf: matrix is non-square");
        bflag = true;
        for (i = 0; i <= a.n - 1; i++)
        {
            bflag = (bflag && p[i] >= 0) && p[i] < a.n;
        }
        ap.assert(bflag, "SparseSymmPermTblBuf: P[] contains values outside of [0,N) range");
        n = a.n;

        //
        // Prepare output
        //
        ap.assert(a.ninitialized == a.ridx[n], "SparseSymmPermTblBuf: integrity check failed");
        b.matrixtype = 1;
        b.n = n;
        b.m = n;
        apserv.ivectorsetlengthatleast(ref b.didx, n, _params);
        apserv.ivectorsetlengthatleast(ref b.uidx, n, _params);

        //
        // Determine row sizes (temporary stored in DIdx) and ranges
        //
        ablasf.isetv(n, 0, b.didx, _params);
        for (i = 0; i <= n - 1; i++)
        {
            if (isupper)
            {
                j0 = a.didx[i];
                j1 = a.ridx[i + 1] - 1;
                k0 = p[i];
                for (jj = j0; jj <= j1; jj++)
                {
                    k1 = p[a.idx[jj]];
                    if (k1 < k0)
                    {
                        b.didx[k1] = b.didx[k1] + 1;
                    }
                    else
                    {
                        b.didx[k0] = b.didx[k0] + 1;
                    }
                }
            }
            else
            {
                j0 = a.ridx[i];
                j1 = a.uidx[i] - 1;
                k0 = p[i];
                for (jj = j0; jj <= j1; jj++)
                {
                    k1 = p[a.idx[jj]];
                    if (k1 > k0)
                    {
                        b.didx[k1] = b.didx[k1] + 1;
                    }
                    else
                    {
                        b.didx[k0] = b.didx[k0] + 1;
                    }
                }
            }
        }
        apserv.ivectorsetlengthatleast(ref b.ridx, n + 1, _params);
        b.ridx[0] = 0;
        for (i = 0; i <= n - 1; i++)
        {
            b.ridx[i + 1] = b.ridx[i] + b.didx[i];
        }
        b.ninitialized = b.ridx[n];
        apserv.ivectorsetlengthatleast(ref b.idx, b.ninitialized, _params);
        apserv.rvectorsetlengthatleast(ref b.vals, b.ninitialized, _params);

        //
        // Process matrix
        //
        for (i = 0; i <= n - 1; i++)
        {
            b.uidx[i] = b.ridx[i];
        }
        for (i = 0; i <= n - 1; i++)
        {
            if (isupper)
            {
                j0 = a.didx[i];
                j1 = a.ridx[i + 1] - 1;
                for (jj = j0; jj <= j1; jj++)
                {
                    j = a.idx[jj];
                    k0 = p[i];
                    k1 = p[j];
                    if (k1 < k0)
                    {
                        kk = k0;
                        k0 = k1;
                        k1 = kk;
                    }
                    dst = b.uidx[k0];
                    b.idx[dst] = k1;
                    b.vals[dst] = a.vals[jj];
                    b.uidx[k0] = dst + 1;
                }
            }
            else
            {
                j0 = a.ridx[i];
                j1 = a.uidx[i] - 1;
                for (jj = j0; jj <= j1; jj++)
                {
                    j = a.idx[jj];
                    k0 = p[i];
                    k1 = p[j];
                    if (k1 > k0)
                    {
                        kk = k0;
                        k0 = k1;
                        k1 = kk;
                    }
                    dst = b.uidx[k0];
                    b.idx[dst] = k1;
                    b.vals[dst] = a.vals[jj];
                    b.uidx[k0] = dst + 1;
                }
            }
        }

        //
        // Finalize matrix
        //
        for (i = 0; i <= n - 1; i++)
        {
            tsort.tagsortmiddleir(ref b.idx, ref b.vals, b.ridx[i], b.ridx[i + 1] - b.ridx[i], _params);
        }
        sparseinitduidx(b, _params);
    }


    /*************************************************************************
    This procedure resizes Hash-Table matrix. It can be called when you  have
    deleted too many elements from the matrix, and you want to  free unneeded
    memory.

      -- ALGLIB PROJECT --
         Copyright 14.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void sparseresizematrix(sparsematrix s,
        xparams _params)
    {
        int k = 0;
        int k1 = 0;
        int i = 0;
        double[] tvals = new double[0];
        int[] tidx = new int[0];

        ap.assert(s.matrixtype == 0, "SparseResizeMatrix: incorrect matrix type");

        //
        // Initialization for length and number of non-null elementd
        //
        k = s.tablesize;
        k1 = 0;

        //
        // Calculating number of non-null elements
        //
        for (i = 0; i <= k - 1; i++)
        {
            if (s.idx[2 * i] >= 0)
            {
                k1 = k1 + 1;
            }
        }

        //
        // Initialization value for free space
        //
        s.tablesize = (int)Math.Round(k1 / desiredloadfactor * growfactor + additional);
        s.nfree = s.tablesize - k1;
        tvals = new double[s.tablesize];
        tidx = new int[2 * s.tablesize];
        ap.swap(ref s.vals, ref tvals);
        ap.swap(ref s.idx, ref tidx);
        for (i = 0; i <= s.tablesize - 1; i++)
        {
            s.idx[2 * i] = -1;
        }
        for (i = 0; i <= k - 1; i++)
        {
            if (tidx[2 * i] >= 0)
            {
                sparseset(s, tidx[2 * i], tidx[2 * i + 1], tvals[i], _params);
            }
        }
    }


    /*************************************************************************
    Procedure for initialization 'S.DIdx' and 'S.UIdx'


      -- ALGLIB PROJECT --
         Copyright 14.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void sparseinitduidx(sparsematrix s,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        int lt = 0;
        int rt = 0;

        ap.assert(s.matrixtype == 1, "SparseInitDUIdx: internal error, incorrect matrix type");
        apserv.ivectorsetlengthatleast(ref s.didx, s.m, _params);
        apserv.ivectorsetlengthatleast(ref s.uidx, s.m, _params);
        for (i = 0; i <= s.m - 1; i++)
        {
            s.uidx[i] = -1;
            s.didx[i] = -1;
            lt = s.ridx[i];
            rt = s.ridx[i + 1];
            for (j = lt; j <= rt - 1; j++)
            {
                k = s.idx[j];
                if (k == i)
                {
                    s.didx[i] = j;
                }
                else
                {
                    if (k > i && s.uidx[i] == -1)
                    {
                        s.uidx[i] = j;
                        break;
                    }
                }
            }
            if (s.uidx[i] == -1)
            {
                s.uidx[i] = s.ridx[i + 1];
            }
            if (s.didx[i] == -1)
            {
                s.didx[i] = s.uidx[i];
            }
        }
    }


    /*************************************************************************
    This function return average length of chain at hash-table.

      -- ALGLIB PROJECT --
         Copyright 14.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static double sparsegetaveragelengthofchain(sparsematrix s,
        xparams _params)
    {
        double result = 0;
        int nchains = 0;
        int talc = 0;
        int l = 0;
        int i = 0;
        int ind0 = 0;
        int ind1 = 0;
        int hashcode = 0;


        //
        // If matrix represent in CRS then return zero and exit
        //
        if (s.matrixtype != 0)
        {
            result = 0;
            return result;
        }
        nchains = 0;
        talc = 0;
        l = s.tablesize;
        for (i = 0; i <= l - 1; i++)
        {
            ind0 = 2 * i;
            if (s.idx[ind0] != -1)
            {
                nchains = nchains + 1;
                hashcode = hash(s.idx[ind0], s.idx[ind0 + 1], l, _params);
                while (true)
                {
                    talc = talc + 1;
                    ind1 = 2 * hashcode;
                    if (s.idx[ind0] == s.idx[ind1] && s.idx[ind0 + 1] == s.idx[ind1 + 1])
                    {
                        break;
                    }
                    hashcode = (hashcode + 1) % l;
                }
            }
        }
        if (nchains == 0)
        {
            result = 0;
        }
        else
        {
            result = (double)talc / (double)nchains;
        }
        return result;
    }


    /*************************************************************************
    This  function  is  used  to enumerate all elements of the sparse matrix.
    Before  first  call  user  initializes  T0 and T1 counters by zero. These
    counters are used to remember current position in a  matrix;  after  each
    call they are updated by the function.

    Subsequent calls to this function return non-zero elements of the  sparse
    matrix, one by one. If you enumerate CRS matrix, matrix is traversed from
    left to right, from top to bottom. In case you enumerate matrix stored as
    Hash table, elements are returned in random order.

    EXAMPLE
        > T0=0
        > T1=0
        > while SparseEnumerate(S,T0,T1,I,J,V) do
        >     ....do something with I,J,V

    INPUT PARAMETERS
        S           -   sparse M*N matrix in Hash-Table or CRS representation.
        T0          -   internal counter
        T1          -   internal counter
        
    OUTPUT PARAMETERS
        T0          -   new value of the internal counter
        T1          -   new value of the internal counter
        I           -   row index of non-zero element, 0<=I<M.
        J           -   column index of non-zero element, 0<=J<N
        V           -   value of the T-th element
        
    RESULT
        True in case of success (next non-zero element was retrieved)
        False in case all non-zero elements were enumerated
        
    NOTE: you may call SparseRewriteExisting() during enumeration, but it  is
          THE  ONLY  matrix  modification  function  you  can  call!!!  Other
          matrix modification functions should not be called during enumeration!

      -- ALGLIB PROJECT --
         Copyright 14.03.2012 by Bochkanov Sergey
    *************************************************************************/
    public static bool sparseenumerate(sparsematrix s,
        ref int t0,
        ref int t1,
        ref int i,
        ref int j,
        ref double v,
        xparams _params)
    {
        bool result = new bool();
        int sz = 0;
        int i0 = 0;

        i = 0;
        j = 0;
        v = 0;

        result = false;
        if (t0 < 0 || (s.matrixtype != 0 && t1 < 0))
        {

            //
            // Incorrect T0/T1, terminate enumeration
            //
            result = false;
            return result;
        }
        if (s.matrixtype == 0)
        {

            //
            // Hash-table matrix
            //
            sz = s.tablesize;
            for (i0 = t0; i0 <= sz - 1; i0++)
            {
                if (s.idx[2 * i0] == -1 || s.idx[2 * i0] == -2)
                {
                    continue;
                }
                else
                {
                    i = s.idx[2 * i0];
                    j = s.idx[2 * i0 + 1];
                    v = s.vals[i0];
                    t0 = i0 + 1;
                    result = true;
                    return result;
                }
            }
            t0 = 0;
            t1 = 0;
            result = false;
            return result;
        }
        if (s.matrixtype == 1)
        {

            //
            // CRS matrix
            //
            ap.assert(s.ninitialized == s.ridx[s.m], "SparseEnumerate: some rows/elements of the CRS matrix were not initialized (you must initialize everything you promised to SparseCreateCRS)");
            if (t0 >= s.ninitialized)
            {
                t0 = 0;
                t1 = 0;
                result = false;
                return result;
            }
            while (t0 > s.ridx[t1 + 1] - 1 && t1 < s.m)
            {
                t1 = t1 + 1;
            }
            i = t1;
            j = s.idx[t0];
            v = s.vals[t0];
            t0 = t0 + 1;
            result = true;
            return result;
        }
        if (s.matrixtype == 2)
        {

            //
            // SKS matrix:
            // * T0 stores current offset in Vals[] array
            // * T1 stores index of the diagonal block
            //
            ap.assert(s.m == s.n, "SparseEnumerate: non-square SKS matrices are not supported");
            if (t0 >= s.ridx[s.m])
            {
                t0 = 0;
                t1 = 0;
                result = false;
                return result;
            }
            while (t0 > s.ridx[t1 + 1] - 1 && t1 < s.m)
            {
                t1 = t1 + 1;
            }
            i0 = t0 - s.ridx[t1];
            if (i0 < s.didx[t1] + 1)
            {

                //
                // subdiagonal or diagonal element, row index is T1.
                //
                i = t1;
                j = t1 - s.didx[t1] + i0;
            }
            else
            {

                //
                // superdiagonal element, column index is T1.
                //
                i = t1 - (s.ridx[t1 + 1] - t0);
                j = t1;
            }
            v = s.vals[t0];
            t0 = t0 + 1;
            result = true;
            return result;
        }
        ap.assert(false, "SparseEnumerate: unexpected matrix type");
        return result;
    }


    /*************************************************************************
    This function rewrites existing (non-zero) element. It  returns  True   if
    element  exists  or  False,  when  it  is  called for non-existing  (zero)
    element.

    This function works with any kind of the matrix.

    The purpose of this function is to provide convenient thread-safe  way  to
    modify  sparse  matrix.  Such  modification  (already  existing element is
    rewritten) is guaranteed to be thread-safe without any synchronization, as
    long as different threads modify different elements.

    INPUT PARAMETERS
        S           -   sparse M*N matrix in any kind of representation
                        (Hash, SKS, CRS).
        I           -   row index of non-zero element to modify, 0<=I<M
        J           -   column index of non-zero element to modify, 0<=J<N
        V           -   value to rewrite, must be finite number

    OUTPUT PARAMETERS
        S           -   modified matrix
    RESULT
        True in case when element exists
        False in case when element doesn't exist or it is zero
        
      -- ALGLIB PROJECT --
         Copyright 14.03.2012 by Bochkanov Sergey
    *************************************************************************/
    public static bool sparserewriteexisting(sparsematrix s,
        int i,
        int j,
        double v,
        xparams _params)
    {
        bool result = new bool();
        int hashcode = 0;
        int k = 0;
        int k0 = 0;
        int k1 = 0;

        ap.assert(0 <= i && i < s.m, "SparseRewriteExisting: invalid argument I(either I<0 or I>=S.M)");
        ap.assert(0 <= j && j < s.n, "SparseRewriteExisting: invalid argument J(either J<0 or J>=S.N)");
        ap.assert(math.isfinite(v), "SparseRewriteExisting: invalid argument V(either V is infinite or V is NaN)");
        result = false;

        //
        // Hash-table matrix
        //
        if (s.matrixtype == 0)
        {
            k = s.tablesize;
            hashcode = hash(i, j, k, _params);
            while (true)
            {
                if (s.idx[2 * hashcode] == -1)
                {
                    return result;
                }
                if (s.idx[2 * hashcode] == i && s.idx[2 * hashcode + 1] == j)
                {
                    s.vals[hashcode] = v;
                    result = true;
                    return result;
                }
                hashcode = (hashcode + 1) % k;
            }
        }

        //
        // CRS matrix
        //
        if (s.matrixtype == 1)
        {
            ap.assert(s.ninitialized == s.ridx[s.m], "SparseRewriteExisting: some rows/elements of the CRS matrix were not initialized (you must initialize everything you promised to SparseCreateCRS)");
            k0 = s.ridx[i];
            k1 = s.ridx[i + 1] - 1;
            while (k0 <= k1)
            {
                k = (k0 + k1) / 2;
                if (s.idx[k] == j)
                {
                    s.vals[k] = v;
                    result = true;
                    return result;
                }
                if (s.idx[k] < j)
                {
                    k0 = k + 1;
                }
                else
                {
                    k1 = k - 1;
                }
            }
        }

        //
        // SKS
        //
        if (s.matrixtype == 2)
        {
            ap.assert(s.m == s.n, "SparseRewriteExisting: non-square SKS matrix not supported");
            if (i == j)
            {

                //
                // Rewrite diagonal element
                //
                result = true;
                s.vals[s.ridx[i] + s.didx[i]] = v;
                return result;
            }
            if (j < i)
            {

                //
                // Return subdiagonal element at I-th "skyline block"
                //
                k = s.didx[i];
                if (i - j <= k)
                {
                    s.vals[s.ridx[i] + k + j - i] = v;
                    result = true;
                }
            }
            else
            {

                //
                // Return superdiagonal element at J-th "skyline block"
                //
                k = s.uidx[j];
                if (j - i <= k)
                {
                    s.vals[s.ridx[j + 1] - (j - i)] = v;
                    result = true;
                }
            }
            return result;
        }
        return result;
    }


    /*************************************************************************
    This function returns I-th row of the sparse matrix. Matrix must be stored
    in CRS or SKS format.

    INPUT PARAMETERS:
        S           -   sparse M*N matrix in CRS format
        I           -   row index, 0<=I<M
        IRow        -   output buffer, can be  preallocated.  In  case  buffer
                        size  is  too  small  to  store  I-th   row,   it   is
                        automatically reallocated.
     
    OUTPUT PARAMETERS:
        IRow        -   array[M], I-th row.
        
    NOTE: this function has O(N) running time, where N is a  column  count. It
          allocates and fills N-element  array,  even  although  most  of  its
          elemets are zero.
          
    NOTE: If you have O(non-zeros-per-row) time and memory  requirements,  use
          SparseGetCompressedRow() function. It  returns  data  in  compressed
          format.

    NOTE: when  incorrect  I  (outside  of  [0,M-1]) or  matrix (non  CRS/SKS)
          is passed, this function throws exception.

      -- ALGLIB PROJECT --
         Copyright 10.12.2014 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsegetrow(sparsematrix s,
        int i,
        ref double[] irow,
        xparams _params)
    {
        int i0 = 0;
        int j0 = 0;
        int j1 = 0;
        int j = 0;
        int upperprofile = 0;

        ap.assert(s.matrixtype == 1 || s.matrixtype == 2, "SparseGetRow: S must be CRS/SKS-based matrix");
        ap.assert(i >= 0 && i < s.m, "SparseGetRow: I<0 or I>=M");

        //
        // Prepare output buffer
        //
        apserv.rvectorsetlengthatleast(ref irow, s.n, _params);
        for (i0 = 0; i0 <= s.n - 1; i0++)
        {
            irow[i0] = 0;
        }

        //
        // Output
        //
        if (s.matrixtype == 1)
        {
            for (i0 = s.ridx[i]; i0 <= s.ridx[i + 1] - 1; i0++)
            {
                irow[s.idx[i0]] = s.vals[i0];
            }
            return;
        }
        if (s.matrixtype == 2)
        {

            //
            // Copy subdiagonal and diagonal parts
            //
            ap.assert(s.n == s.m, "SparseGetRow: non-square SKS matrices are not supported");
            j0 = i - s.didx[i];
            i0 = -j0 + s.ridx[i];
            for (j = j0; j <= i; j++)
            {
                irow[j] = s.vals[j + i0];
            }

            //
            // Copy superdiagonal part
            //
            upperprofile = s.uidx[s.n];
            j0 = i + 1;
            j1 = Math.Min(s.n - 1, i + upperprofile);
            for (j = j0; j <= j1; j++)
            {
                if (j - i <= s.uidx[j])
                {
                    irow[j] = s.vals[s.ridx[j + 1] - (j - i)];
                }
            }
            return;
        }
    }


    /*************************************************************************
    This function returns I-th row of the sparse matrix IN COMPRESSED FORMAT -
    only non-zero elements are returned (with their indexes). Matrix  must  be
    stored in CRS or SKS format.

    INPUT PARAMETERS:
        S           -   sparse M*N matrix in CRS format
        I           -   row index, 0<=I<M
        ColIdx      -   output buffer for column indexes, can be preallocated.
                        In case buffer size is too small to store I-th row, it
                        is automatically reallocated.
        Vals        -   output buffer for values, can be preallocated. In case
                        buffer size is too small to  store  I-th  row,  it  is
                        automatically reallocated.
     
    OUTPUT PARAMETERS:
        ColIdx      -   column   indexes   of  non-zero  elements,  sorted  by
                        ascending. Symbolically non-zero elements are  counted
                        (i.e. if you allocated place for element, but  it  has
                        zero numerical value - it is counted).
        Vals        -   values. Vals[K] stores value of  matrix  element  with
                        indexes (I,ColIdx[K]). Symbolically non-zero  elements
                        are counted (i.e. if you allocated place for  element,
                        but it has zero numerical value - it is counted).
        NZCnt       -   number of symbolically non-zero elements per row.

    NOTE: when  incorrect  I  (outside  of  [0,M-1]) or  matrix (non  CRS/SKS)
          is passed, this function throws exception.
          
    NOTE: this function may allocate additional, unnecessary place for  ColIdx
          and Vals arrays. It is dictated by  performance  reasons  -  on  SKS
          matrices it is faster  to  allocate  space  at  the  beginning  with
          some "extra"-space, than performing two passes over matrix  -  first
          time to calculate exact space required for data, second  time  -  to
          store data itself.

      -- ALGLIB PROJECT --
         Copyright 10.12.2014 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsegetcompressedrow(sparsematrix s,
        int i,
        ref int[] colidx,
        ref double[] vals,
        ref int nzcnt,
        xparams _params)
    {
        int k = 0;
        int k0 = 0;
        int j = 0;
        int j0 = 0;
        int j1 = 0;
        int i0 = 0;
        int upperprofile = 0;

        nzcnt = 0;

        ap.assert(s.matrixtype == 1 || s.matrixtype == 2, "SparseGetRow: S must be CRS/SKS-based matrix");
        ap.assert(i >= 0 && i < s.m, "SparseGetRow: I<0 or I>=M");

        //
        // Initialize NZCnt
        //
        nzcnt = 0;

        //
        // CRS matrix - just copy data
        //
        if (s.matrixtype == 1)
        {
            nzcnt = s.ridx[i + 1] - s.ridx[i];
            apserv.ivectorsetlengthatleast(ref colidx, nzcnt, _params);
            apserv.rvectorsetlengthatleast(ref vals, nzcnt, _params);
            k0 = s.ridx[i];
            for (k = 0; k <= nzcnt - 1; k++)
            {
                colidx[k] = s.idx[k0 + k];
                vals[k] = s.vals[k0 + k];
            }
            return;
        }

        //
        // SKS matrix - a bit more complex sequence
        //
        if (s.matrixtype == 2)
        {
            ap.assert(s.n == s.m, "SparseGetCompressedRow: non-square SKS matrices are not supported");

            //
            // Allocate enough place for storage
            //
            upperprofile = s.uidx[s.n];
            apserv.ivectorsetlengthatleast(ref colidx, s.didx[i] + 1 + upperprofile, _params);
            apserv.rvectorsetlengthatleast(ref vals, s.didx[i] + 1 + upperprofile, _params);

            //
            // Copy subdiagonal and diagonal parts
            //
            j0 = i - s.didx[i];
            i0 = -j0 + s.ridx[i];
            for (j = j0; j <= i; j++)
            {
                colidx[nzcnt] = j;
                vals[nzcnt] = s.vals[j + i0];
                nzcnt = nzcnt + 1;
            }

            //
            // Copy superdiagonal part
            //
            j0 = i + 1;
            j1 = Math.Min(s.n - 1, i + upperprofile);
            for (j = j0; j <= j1; j++)
            {
                if (j - i <= s.uidx[j])
                {
                    colidx[nzcnt] = j;
                    vals[nzcnt] = s.vals[s.ridx[j + 1] - (j - i)];
                    nzcnt = nzcnt + 1;
                }
            }
            return;
        }
    }


    /*************************************************************************
    This function performs efficient in-place  transpose  of  SKS  matrix.  No
    additional memory is allocated during transposition.

    This function supports only skyline storage format (SKS).

    INPUT PARAMETERS
        S       -   sparse matrix in SKS format.

    OUTPUT PARAMETERS
        S           -   sparse matrix, transposed.

      -- ALGLIB PROJECT --
         Copyright 16.01.2014 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsetransposesks(sparsematrix s,
        xparams _params)
    {
        int n = 0;
        int d = 0;
        int u = 0;
        int i = 0;
        int k = 0;
        int t0 = 0;
        int t1 = 0;
        double v = 0;

        ap.assert(s.matrixtype == 2, "SparseTransposeSKS: only SKS matrices are supported");
        ap.assert(s.m == s.n, "SparseTransposeSKS: non-square SKS matrices are not supported");
        n = s.n;
        for (i = 1; i <= n - 1; i++)
        {
            d = s.didx[i];
            u = s.uidx[i];
            k = s.uidx[i];
            s.uidx[i] = s.didx[i];
            s.didx[i] = k;
            if (d == u)
            {

                //
                // Upper skyline height equal to lower skyline height,
                // simple exchange is needed for transposition
                //
                t0 = s.ridx[i];
                for (k = 0; k <= d - 1; k++)
                {
                    v = s.vals[t0 + k];
                    s.vals[t0 + k] = s.vals[t0 + d + 1 + k];
                    s.vals[t0 + d + 1 + k] = v;
                }
            }
            if (d > u)
            {

                //
                // Upper skyline height is less than lower skyline height.
                //
                // Transposition becomes a bit tricky: we have to rearrange
                // "L0 L1 D U" to "U D L0 L1", where |L0|=|U|=u, |L1|=d-u.
                //
                // In order to do this we perform a sequence of swaps and
                // in-place reversals:
                // * swap(L0,U)         =>  "U   L1  D   L0"
                // * reverse("L1 D L0") =>  "U   L0~ D   L1~" (where X~ is a reverse of X)
                // * reverse("L0~ D")   =>  "U   D   L0  L1~"
                // * reverse("L1")      =>  "U   D   L0  L1"
                //
                t0 = s.ridx[i];
                t1 = s.ridx[i] + d + 1;
                for (k = 0; k <= u - 1; k++)
                {
                    v = s.vals[t0 + k];
                    s.vals[t0 + k] = s.vals[t1 + k];
                    s.vals[t1 + k] = v;
                }
                t0 = s.ridx[i] + u;
                t1 = s.ridx[i + 1] - 1;
                while (t1 > t0)
                {
                    v = s.vals[t0];
                    s.vals[t0] = s.vals[t1];
                    s.vals[t1] = v;
                    t0 = t0 + 1;
                    t1 = t1 - 1;
                }
                t0 = s.ridx[i] + u;
                t1 = s.ridx[i] + u + u;
                while (t1 > t0)
                {
                    v = s.vals[t0];
                    s.vals[t0] = s.vals[t1];
                    s.vals[t1] = v;
                    t0 = t0 + 1;
                    t1 = t1 - 1;
                }
                t0 = s.ridx[i + 1] - (d - u);
                t1 = s.ridx[i + 1] - 1;
                while (t1 > t0)
                {
                    v = s.vals[t0];
                    s.vals[t0] = s.vals[t1];
                    s.vals[t1] = v;
                    t0 = t0 + 1;
                    t1 = t1 - 1;
                }
            }
            if (d < u)
            {

                //
                // Upper skyline height is greater than lower skyline height.
                //
                // Transposition becomes a bit tricky: we have to rearrange
                // "L D U0 U1" to "U0 U1 D L", where |U1|=|L|=d, |U0|=u-d.
                //
                // In order to do this we perform a sequence of swaps and
                // in-place reversals:
                // * swap(L,U1)         =>  "U1  D   U0  L"
                // * reverse("U1 D U0") =>  "U0~ D   U1~ L" (where X~ is a reverse of X)
                // * reverse("U0~")     =>  "U0  D   U1~ L"
                // * reverse("D U1~")   =>  "U0  U1  D   L"
                //
                t0 = s.ridx[i];
                t1 = s.ridx[i + 1] - d;
                for (k = 0; k <= d - 1; k++)
                {
                    v = s.vals[t0 + k];
                    s.vals[t0 + k] = s.vals[t1 + k];
                    s.vals[t1 + k] = v;
                }
                t0 = s.ridx[i];
                t1 = s.ridx[i] + u;
                while (t1 > t0)
                {
                    v = s.vals[t0];
                    s.vals[t0] = s.vals[t1];
                    s.vals[t1] = v;
                    t0 = t0 + 1;
                    t1 = t1 - 1;
                }
                t0 = s.ridx[i];
                t1 = s.ridx[i] + u - d - 1;
                while (t1 > t0)
                {
                    v = s.vals[t0];
                    s.vals[t0] = s.vals[t1];
                    s.vals[t1] = v;
                    t0 = t0 + 1;
                    t1 = t1 - 1;
                }
                t0 = s.ridx[i] + u - d;
                t1 = s.ridx[i + 1] - d - 1;
                while (t1 > t0)
                {
                    v = s.vals[t0];
                    s.vals[t0] = s.vals[t1];
                    s.vals[t1] = v;
                    t0 = t0 + 1;
                    t1 = t1 - 1;
                }
            }
        }
        k = s.uidx[n];
        s.uidx[n] = s.didx[n];
        s.didx[n] = k;
    }


    /*************************************************************************
    This function performs transpose of CRS matrix.

    INPUT PARAMETERS
        S       -   sparse matrix in CRS format.

    OUTPUT PARAMETERS
        S           -   sparse matrix, transposed.

    NOTE: internal  temporary  copy  is  allocated   for   the   purposes   of
          transposition. It is deallocated after transposition.

      -- ALGLIB PROJECT --
         Copyright 30.01.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsetransposecrs(sparsematrix s,
        xparams _params)
    {
        double[] oldvals = new double[0];
        int[] oldidx = new int[0];
        int[] oldridx = new int[0];
        int oldn = 0;
        int oldm = 0;
        int newn = 0;
        int newm = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        int nonne = 0;
        int[] counts = new int[0];

        ap.assert(s.matrixtype == 1, "SparseTransposeCRS: only CRS matrices are supported");
        ap.swap(ref s.vals, ref oldvals);
        ap.swap(ref s.idx, ref oldidx);
        ap.swap(ref s.ridx, ref oldridx);
        oldn = s.n;
        oldm = s.m;
        newn = oldm;
        newm = oldn;

        //
        // Update matrix size
        //
        s.n = newn;
        s.m = newm;

        //
        // Fill RIdx by number of elements per row:
        // RIdx[I+1] stores number of elements in I-th row.
        //
        // Convert RIdx from row sizes to row offsets.
        // Set NInitialized
        //
        nonne = 0;
        apserv.ivectorsetlengthatleast(ref s.ridx, newm + 1, _params);
        for (i = 0; i <= newm; i++)
        {
            s.ridx[i] = 0;
        }
        for (i = 0; i <= oldm - 1; i++)
        {
            for (j = oldridx[i]; j <= oldridx[i + 1] - 1; j++)
            {
                k = oldidx[j] + 1;
                s.ridx[k] = s.ridx[k] + 1;
                nonne = nonne + 1;
            }
        }
        for (i = 0; i <= newm - 1; i++)
        {
            s.ridx[i + 1] = s.ridx[i + 1] + s.ridx[i];
        }
        s.ninitialized = s.ridx[newm];

        //
        // Allocate memory and move elements to Vals/Idx.
        //
        counts = new int[newm];
        for (i = 0; i <= newm - 1; i++)
        {
            counts[i] = 0;
        }
        apserv.rvectorsetlengthatleast(ref s.vals, nonne, _params);
        apserv.ivectorsetlengthatleast(ref s.idx, nonne, _params);
        for (i = 0; i <= oldm - 1; i++)
        {
            for (j = oldridx[i]; j <= oldridx[i + 1] - 1; j++)
            {
                k = oldidx[j];
                k = s.ridx[k] + counts[k];
                s.idx[k] = i;
                s.vals[k] = oldvals[j];
                k = oldidx[j];
                counts[k] = counts[k] + 1;
            }
        }

        //
        // Initialization 'S.UIdx' and 'S.DIdx'
        //
        sparseinitduidx(s, _params);
    }


    /*************************************************************************
    This function performs copying with transposition of CRS matrix.

    INPUT PARAMETERS
        S0      -   sparse matrix in CRS format.

    OUTPUT PARAMETERS
        S1      -   sparse matrix, transposed

      -- ALGLIB PROJECT --
         Copyright 23.07.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsecopytransposecrs(sparsematrix s0,
        sparsematrix s1,
        xparams _params)
    {
        sparsecopytransposecrsbuf(s0, s1, _params);
    }


    /*************************************************************************
    This function performs copying with transposition of CRS matrix  (buffered
    version which reuses memory already allocated by  the  target as  much  as
    possible).

    INPUT PARAMETERS
        S0      -   sparse matrix in CRS format.

    OUTPUT PARAMETERS
        S1      -   sparse matrix, transposed; previously allocated memory  is
                    reused if possible.

      -- ALGLIB PROJECT --
         Copyright 23.07.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsecopytransposecrsbuf(sparsematrix s0,
        sparsematrix s1,
        xparams _params)
    {
        int oldn = 0;
        int oldm = 0;
        int newn = 0;
        int newm = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        int kk = 0;
        int j0 = 0;
        int j1 = 0;

        ap.assert(s0.matrixtype == 1, "SparseCopyTransposeCRSBuf: only CRS matrices are supported");
        oldn = s0.n;
        oldm = s0.m;
        newn = oldm;
        newm = oldn;

        //
        // Update matrix size
        //
        s1.matrixtype = 1;
        s1.n = newn;
        s1.m = newm;

        //
        // Fill RIdx by number of elements per row:
        // RIdx[I+1] stores number of elements in I-th row.
        //
        // Convert RIdx from row sizes to row offsets.
        // Set NInitialized
        //
        ablasf.isetallocv(newm + 1, 0, ref s1.ridx, _params);
        for (i = 0; i <= oldm - 1; i++)
        {
            j0 = s0.ridx[i];
            j1 = s0.ridx[i + 1] - 1;
            for (j = j0; j <= j1; j++)
            {
                k = s0.idx[j] + 1;
                s1.ridx[k] = s1.ridx[k] + 1;
            }
        }
        for (i = 0; i <= newm - 1; i++)
        {
            s1.ridx[i + 1] = s1.ridx[i + 1] + s1.ridx[i];
        }
        s1.ninitialized = s1.ridx[newm];

        //
        // Allocate memory and move elements to Vals/Idx.
        //
        apserv.ivectorsetlengthatleast(ref s1.didx, newm, _params);
        for (i = 0; i <= newm - 1; i++)
        {
            s1.didx[i] = s1.ridx[i];
        }
        apserv.rvectorsetlengthatleast(ref s1.vals, s1.ninitialized, _params);
        apserv.ivectorsetlengthatleast(ref s1.idx, s1.ninitialized, _params);
        for (i = 0; i <= oldm - 1; i++)
        {
            j0 = s0.ridx[i];
            j1 = s0.ridx[i + 1] - 1;
            for (j = j0; j <= j1; j++)
            {
                kk = s0.idx[j];
                k = s1.didx[kk];
                s1.idx[k] = i;
                s1.vals[k] = s0.vals[j];
                s1.didx[kk] = k + 1;
            }
        }

        //
        // Initialization 'S.UIdx' and 'S.DIdx'
        //
        sparseinitduidx(s1, _params);
    }


    /*************************************************************************
    This  function  performs  in-place  conversion  to  desired sparse storage
    format.

    INPUT PARAMETERS
        S0      -   sparse matrix in any format.
        Fmt     -   desired storage format  of  the  output,  as  returned  by
                    SparseGetMatrixType() function:
                    * 0 for hash-based storage
                    * 1 for CRS
                    * 2 for SKS

    OUTPUT PARAMETERS
        S0          -   sparse matrix in requested format.
        
    NOTE: in-place conversion wastes a lot of memory which is  used  to  store
          temporaries.  If  you  perform  a  lot  of  repeated conversions, we
          recommend to use out-of-place buffered  conversion  functions,  like
          SparseCopyToBuf(), which can reuse already allocated memory.

      -- ALGLIB PROJECT --
         Copyright 16.01.2014 by Bochkanov Sergey
    *************************************************************************/
    public static void sparseconvertto(sparsematrix s0,
        int fmt,
        xparams _params)
    {
        ap.assert((fmt == 0 || fmt == 1) || fmt == 2, "SparseConvertTo: invalid fmt parameter");
        if (fmt == 0)
        {
            sparseconverttohash(s0, _params);
            return;
        }
        if (fmt == 1)
        {
            sparseconverttocrs(s0, _params);
            return;
        }
        if (fmt == 2)
        {
            sparseconverttosks(s0, _params);
            return;
        }
        ap.assert(false, "SparseConvertTo: invalid matrix type");
    }


    /*************************************************************************
    This  function  performs out-of-place conversion to desired sparse storage
    format. S0 is copied to S1 and converted on-the-fly. Memory  allocated  in
    S1 is reused to maximum extent possible.

    INPUT PARAMETERS
        S0      -   sparse matrix in any format.
        Fmt     -   desired storage format  of  the  output,  as  returned  by
                    SparseGetMatrixType() function:
                    * 0 for hash-based storage
                    * 1 for CRS
                    * 2 for SKS

    OUTPUT PARAMETERS
        S1          -   sparse matrix in requested format.

      -- ALGLIB PROJECT --
         Copyright 16.01.2014 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsecopytobuf(sparsematrix s0,
        int fmt,
        sparsematrix s1,
        xparams _params)
    {
        ap.assert((fmt == 0 || fmt == 1) || fmt == 2, "SparseCopyToBuf: invalid fmt parameter");
        if (fmt == 0)
        {
            sparsecopytohashbuf(s0, s1, _params);
            return;
        }
        if (fmt == 1)
        {
            sparsecopytocrsbuf(s0, s1, _params);
            return;
        }
        if (fmt == 2)
        {
            sparsecopytosksbuf(s0, s1, _params);
            return;
        }
        ap.assert(false, "SparseCopyToBuf: invalid matrix type");
    }


    /*************************************************************************
    This function performs in-place conversion to Hash table storage.

    INPUT PARAMETERS
        S           -   sparse matrix in CRS format.

    OUTPUT PARAMETERS
        S           -   sparse matrix in Hash table format.

    NOTE: this  function  has   no  effect  when  called with matrix which  is
          already in Hash table mode.

    NOTE: in-place conversion involves allocation of temporary arrays. If  you
          perform a lot of repeated in- place  conversions,  it  may  lead  to
          memory fragmentation. Consider using out-of-place SparseCopyToHashBuf()
          function in this case.
        
      -- ALGLIB PROJECT --
         Copyright 20.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void sparseconverttohash(sparsematrix s,
        xparams _params)
    {
        int[] tidx = new int[0];
        int[] tridx = new int[0];
        int[] tdidx = new int[0];
        int[] tuidx = new int[0];
        double[] tvals = new double[0];
        int n = 0;
        int m = 0;
        int offs0 = 0;
        int i = 0;
        int j = 0;
        int k = 0;

        ap.assert((s.matrixtype == 0 || s.matrixtype == 1) || s.matrixtype == 2, "SparseConvertToHash: invalid matrix type");
        if (s.matrixtype == 0)
        {

            //
            // Already in Hash mode
            //
            return;
        }
        if (s.matrixtype == 1)
        {

            //
            // From CRS to Hash
            //
            s.matrixtype = 0;
            m = s.m;
            n = s.n;
            ap.swap(ref s.idx, ref tidx);
            ap.swap(ref s.ridx, ref tridx);
            ap.swap(ref s.vals, ref tvals);
            sparsecreatebuf(m, n, tridx[m], s, _params);
            for (i = 0; i <= m - 1; i++)
            {
                for (j = tridx[i]; j <= tridx[i + 1] - 1; j++)
                {
                    sparseset(s, i, tidx[j], tvals[j], _params);
                }
            }
            return;
        }
        if (s.matrixtype == 2)
        {

            //
            // From SKS to Hash
            //
            s.matrixtype = 0;
            m = s.m;
            n = s.n;
            ap.swap(ref s.ridx, ref tridx);
            ap.swap(ref s.didx, ref tdidx);
            ap.swap(ref s.uidx, ref tuidx);
            ap.swap(ref s.vals, ref tvals);
            sparsecreatebuf(m, n, tridx[m], s, _params);
            for (i = 0; i <= m - 1; i++)
            {

                //
                // copy subdiagonal and diagonal parts of I-th block
                //
                offs0 = tridx[i];
                k = tdidx[i] + 1;
                for (j = 0; j <= k - 1; j++)
                {
                    sparseset(s, i, i - tdidx[i] + j, tvals[offs0 + j], _params);
                }

                //
                // Copy superdiagonal part of I-th block
                //
                offs0 = tridx[i] + tdidx[i] + 1;
                k = tuidx[i];
                for (j = 0; j <= k - 1; j++)
                {
                    sparseset(s, i - k + j, i, tvals[offs0 + j], _params);
                }
            }
            return;
        }
        ap.assert(false, "SparseConvertToHash: invalid matrix type");
    }


    /*************************************************************************
    This  function  performs  out-of-place  conversion  to  Hash table storage
    format. S0 is copied to S1 and converted on-the-fly.

    INPUT PARAMETERS
        S0          -   sparse matrix in any format.

    OUTPUT PARAMETERS
        S1          -   sparse matrix in Hash table format.

    NOTE: if S0 is stored as Hash-table, it is just copied without conversion.

    NOTE: this function de-allocates memory  occupied  by  S1 before  starting
          conversion. If you perform a  lot  of  repeated  conversions, it may
          lead to memory fragmentation. In this case we recommend you  to  use
          SparseCopyToHashBuf() function which re-uses memory in S1 as much as
          possible.

      -- ALGLIB PROJECT --
         Copyright 20.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsecopytohash(sparsematrix s0,
        sparsematrix s1,
        xparams _params)
    {
        ap.assert((s0.matrixtype == 0 || s0.matrixtype == 1) || s0.matrixtype == 2, "SparseCopyToHash: invalid matrix type");
        sparsecopytohashbuf(s0, s1, _params);
    }


    /*************************************************************************
    This  function  performs  out-of-place  conversion  to  Hash table storage
    format. S0 is copied to S1 and converted on-the-fly. Memory  allocated  in
    S1 is reused to maximum extent possible.

    INPUT PARAMETERS
        S0          -   sparse matrix in any format.

    OUTPUT PARAMETERS
        S1          -   sparse matrix in Hash table format.

    NOTE: if S0 is stored as Hash-table, it is just copied without conversion.

      -- ALGLIB PROJECT --
         Copyright 20.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsecopytohashbuf(sparsematrix s0,
        sparsematrix s1,
        xparams _params)
    {
        double val = 0;
        int t0 = 0;
        int t1 = 0;
        int i = 0;
        int j = 0;

        ap.assert((s0.matrixtype == 0 || s0.matrixtype == 1) || s0.matrixtype == 2, "SparseCopyToHashBuf: invalid matrix type");
        if (s0.matrixtype == 0)
        {

            //
            // Already hash, just copy
            //
            sparsecopybuf(s0, s1, _params);
            return;
        }
        if (s0.matrixtype == 1)
        {

            //
            // CRS storage
            //
            t0 = 0;
            t1 = 0;
            sparsecreatebuf(s0.m, s0.n, s0.ridx[s0.m], s1, _params);
            while (sparseenumerate(s0, ref t0, ref t1, ref i, ref j, ref val, _params))
            {
                sparseset(s1, i, j, val, _params);
            }
            return;
        }
        if (s0.matrixtype == 2)
        {

            //
            // SKS storage
            //
            t0 = 0;
            t1 = 0;
            sparsecreatebuf(s0.m, s0.n, s0.ridx[s0.m], s1, _params);
            while (sparseenumerate(s0, ref t0, ref t1, ref i, ref j, ref val, _params))
            {
                sparseset(s1, i, j, val, _params);
            }
            return;
        }
        ap.assert(false, "SparseCopyToHashBuf: invalid matrix type");
    }


    /*************************************************************************
    This function converts matrix to CRS format.

    Some  algorithms  (linear  algebra ones, for example) require matrices in
    CRS format. This function allows to perform in-place conversion.

    INPUT PARAMETERS
        S           -   sparse M*N matrix in any format

    OUTPUT PARAMETERS
        S           -   matrix in CRS format
        
    NOTE: this   function  has  no  effect  when  called with matrix which is
          already in CRS mode.
          
    NOTE: this function allocates temporary memory to store a   copy  of  the
          matrix. If you perform a lot of repeated conversions, we  recommend
          you  to  use  SparseCopyToCRSBuf()  function,   which   can   reuse
          previously allocated memory.

      -- ALGLIB PROJECT --
         Copyright 14.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void sparseconverttocrs(sparsematrix s,
        xparams _params)
    {
        int m = 0;
        int i = 0;
        int j = 0;
        double[] tvals = new double[0];
        int[] tidx = new int[0];
        int[] temp = new int[0];
        int[] tridx = new int[0];
        int nonne = 0;
        int k = 0;
        int offs0 = 0;
        int offs1 = 0;

        m = s.m;
        if (s.matrixtype == 0)
        {

            //
            // From Hash-table to CRS.
            // First, create local copy of the hash table.
            //
            s.matrixtype = 1;
            k = s.tablesize;
            ap.swap(ref s.vals, ref tvals);
            ap.swap(ref s.idx, ref tidx);

            //
            // Fill RIdx by number of elements per row:
            // RIdx[I+1] stores number of elements in I-th row.
            //
            // Convert RIdx from row sizes to row offsets.
            // Set NInitialized
            //
            nonne = 0;
            apserv.ivectorsetlengthatleast(ref s.ridx, s.m + 1, _params);
            for (i = 0; i <= s.m; i++)
            {
                s.ridx[i] = 0;
            }
            for (i = 0; i <= k - 1; i++)
            {
                if (tidx[2 * i] >= 0)
                {
                    s.ridx[tidx[2 * i] + 1] = s.ridx[tidx[2 * i] + 1] + 1;
                    nonne = nonne + 1;
                }
            }
            for (i = 0; i <= s.m - 1; i++)
            {
                s.ridx[i + 1] = s.ridx[i + 1] + s.ridx[i];
            }
            s.ninitialized = s.ridx[s.m];

            //
            // Allocate memory and move elements to Vals/Idx.
            // Initially, elements are sorted by rows, but unsorted within row.
            // After initial insertion we sort elements within row.
            //
            temp = new int[s.m];
            for (i = 0; i <= s.m - 1; i++)
            {
                temp[i] = 0;
            }
            apserv.rvectorsetlengthatleast(ref s.vals, nonne, _params);
            apserv.ivectorsetlengthatleast(ref s.idx, nonne, _params);
            for (i = 0; i <= k - 1; i++)
            {
                if (tidx[2 * i] >= 0)
                {
                    s.vals[s.ridx[tidx[2 * i]] + temp[tidx[2 * i]]] = tvals[i];
                    s.idx[s.ridx[tidx[2 * i]] + temp[tidx[2 * i]]] = tidx[2 * i + 1];
                    temp[tidx[2 * i]] = temp[tidx[2 * i]] + 1;
                }
            }
            for (i = 0; i <= s.m - 1; i++)
            {
                tsort.tagsortmiddleir(ref s.idx, ref s.vals, s.ridx[i], s.ridx[i + 1] - s.ridx[i], _params);
            }

            //
            // Initialization 'S.UIdx' and 'S.DIdx'
            //
            sparseinitduidx(s, _params);
            return;
        }
        if (s.matrixtype == 1)
        {

            //
            // Already CRS
            //
            return;
        }
        if (s.matrixtype == 2)
        {
            ap.assert(s.m == s.n, "SparseConvertToCRS: non-square SKS matrices are not supported");

            //
            // From SKS to CRS.
            //
            // First, create local copy of the SKS matrix (Vals,
            // Idx, RIdx are stored; DIdx/UIdx for some time are
            // left in the SparseMatrix structure).
            //
            s.matrixtype = 1;
            ap.swap(ref s.vals, ref tvals);
            ap.swap(ref s.idx, ref tidx);
            ap.swap(ref s.ridx, ref tridx);

            //
            // Fill RIdx by number of elements per row:
            // RIdx[I+1] stores number of elements in I-th row.
            //
            // Convert RIdx from row sizes to row offsets.
            // Set NInitialized
            //
            apserv.ivectorsetlengthatleast(ref s.ridx, m + 1, _params);
            s.ridx[0] = 0;
            for (i = 1; i <= m; i++)
            {
                s.ridx[i] = 1;
            }
            nonne = 0;
            for (i = 0; i <= m - 1; i++)
            {
                s.ridx[i + 1] = s.didx[i] + s.ridx[i + 1];
                for (j = i - s.uidx[i]; j <= i - 1; j++)
                {
                    s.ridx[j + 1] = s.ridx[j + 1] + 1;
                }
                nonne = nonne + s.didx[i] + 1 + s.uidx[i];
            }
            for (i = 0; i <= s.m - 1; i++)
            {
                s.ridx[i + 1] = s.ridx[i + 1] + s.ridx[i];
            }
            s.ninitialized = s.ridx[s.m];

            //
            // Allocate memory and move elements to Vals/Idx.
            // Initially, elements are sorted by rows, and are sorted within row too.
            // No additional post-sorting is required.
            //
            temp = new int[m];
            for (i = 0; i <= m - 1; i++)
            {
                temp[i] = 0;
            }
            apserv.rvectorsetlengthatleast(ref s.vals, nonne, _params);
            apserv.ivectorsetlengthatleast(ref s.idx, nonne, _params);
            for (i = 0; i <= m - 1; i++)
            {

                //
                // copy subdiagonal and diagonal parts of I-th block
                //
                offs0 = tridx[i];
                offs1 = s.ridx[i] + temp[i];
                k = s.didx[i] + 1;
                for (j = 0; j <= k - 1; j++)
                {
                    s.vals[offs1 + j] = tvals[offs0 + j];
                    s.idx[offs1 + j] = i - s.didx[i] + j;
                }
                temp[i] = temp[i] + s.didx[i] + 1;

                //
                // Copy superdiagonal part of I-th block
                //
                offs0 = tridx[i] + s.didx[i] + 1;
                k = s.uidx[i];
                for (j = 0; j <= k - 1; j++)
                {
                    offs1 = s.ridx[i - k + j] + temp[i - k + j];
                    s.vals[offs1] = tvals[offs0 + j];
                    s.idx[offs1] = i;
                    temp[i - k + j] = temp[i - k + j] + 1;
                }
            }

            //
            // Initialization 'S.UIdx' and 'S.DIdx'
            //
            sparseinitduidx(s, _params);
            return;
        }
        ap.assert(false, "SparseConvertToCRS: invalid matrix type");
    }


    /*************************************************************************
    This  function  performs  out-of-place  conversion  to  CRS format.  S0 is
    copied to S1 and converted on-the-fly.

    INPUT PARAMETERS
        S0          -   sparse matrix in any format.

    OUTPUT PARAMETERS
        S1          -   sparse matrix in CRS format.
        
    NOTE: if S0 is stored as CRS, it is just copied without conversion.

    NOTE: this function de-allocates memory occupied by S1 before starting CRS
          conversion. If you perform a lot of repeated CRS conversions, it may
          lead to memory fragmentation. In this case we recommend you  to  use
          SparseCopyToCRSBuf() function which re-uses memory in S1 as much  as
          possible.

      -- ALGLIB PROJECT --
         Copyright 20.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsecopytocrs(sparsematrix s0,
        sparsematrix s1,
        xparams _params)
    {
        ap.assert((s0.matrixtype == 0 || s0.matrixtype == 1) || s0.matrixtype == 2, "SparseCopyToCRS: invalid matrix type");
        sparsecopytocrsbuf(s0, s1, _params);
    }


    /*************************************************************************
    This  function  performs  out-of-place  conversion  to  CRS format.  S0 is
    copied to S1 and converted on-the-fly. Memory allocated in S1 is reused to
    maximum extent possible.

    INPUT PARAMETERS
        S0          -   sparse matrix in any format.
        S1          -   matrix which may contain some pre-allocated memory, or
                        can be just uninitialized structure.

    OUTPUT PARAMETERS
        S1          -   sparse matrix in CRS format.
        
    NOTE: if S0 is stored as CRS, it is just copied without conversion.

      -- ALGLIB PROJECT --
         Copyright 20.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsecopytocrsbuf(sparsematrix s0,
        sparsematrix s1,
        xparams _params)
    {
        int[] temp = new int[0];
        int nonne = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        int offs0 = 0;
        int offs1 = 0;
        int m = 0;

        ap.assert((s0.matrixtype == 0 || s0.matrixtype == 1) || s0.matrixtype == 2, "SparseCopyToCRSBuf: invalid matrix type");
        m = s0.m;
        if (s0.matrixtype == 0)
        {

            //
            // Convert from hash-table to CRS
            // Done like ConvertToCRS function
            //
            s1.matrixtype = 1;
            s1.m = s0.m;
            s1.n = s0.n;
            s1.nfree = s0.nfree;
            nonne = 0;
            k = s0.tablesize;
            apserv.ivectorsetlengthatleast(ref s1.ridx, s1.m + 1, _params);
            for (i = 0; i <= s1.m; i++)
            {
                s1.ridx[i] = 0;
            }
            temp = new int[s1.m];
            for (i = 0; i <= s1.m - 1; i++)
            {
                temp[i] = 0;
            }

            //
            // Number of elements per row
            //
            for (i = 0; i <= k - 1; i++)
            {
                if (s0.idx[2 * i] >= 0)
                {
                    s1.ridx[s0.idx[2 * i] + 1] = s1.ridx[s0.idx[2 * i] + 1] + 1;
                    nonne = nonne + 1;
                }
            }

            //
            // Fill RIdx (offsets of rows)
            //
            for (i = 0; i <= s1.m - 1; i++)
            {
                s1.ridx[i + 1] = s1.ridx[i + 1] + s1.ridx[i];
            }

            //
            // Allocate memory
            //
            apserv.rvectorsetlengthatleast(ref s1.vals, nonne, _params);
            apserv.ivectorsetlengthatleast(ref s1.idx, nonne, _params);
            for (i = 0; i <= k - 1; i++)
            {
                if (s0.idx[2 * i] >= 0)
                {
                    s1.vals[s1.ridx[s0.idx[2 * i]] + temp[s0.idx[2 * i]]] = s0.vals[i];
                    s1.idx[s1.ridx[s0.idx[2 * i]] + temp[s0.idx[2 * i]]] = s0.idx[2 * i + 1];
                    temp[s0.idx[2 * i]] = temp[s0.idx[2 * i]] + 1;
                }
            }

            //
            // Set NInitialized
            //
            s1.ninitialized = s1.ridx[s1.m];

            //
            // Sorting of elements
            //
            for (i = 0; i <= s1.m - 1; i++)
            {
                tsort.tagsortmiddleir(ref s1.idx, ref s1.vals, s1.ridx[i], s1.ridx[i + 1] - s1.ridx[i], _params);
            }

            //
            // Initialization 'S.UIdx' and 'S.DIdx'
            //
            sparseinitduidx(s1, _params);
            return;
        }
        if (s0.matrixtype == 1)
        {

            //
            // Already CRS, just copy
            //
            sparsecopybuf(s0, s1, _params);
            return;
        }
        if (s0.matrixtype == 2)
        {
            ap.assert(s0.m == s0.n, "SparseCopyToCRS: non-square SKS matrices are not supported");

            //
            // From SKS to CRS.
            //
            s1.m = s0.m;
            s1.n = s0.n;
            s1.matrixtype = 1;

            //
            // Fill RIdx by number of elements per row:
            // RIdx[I+1] stores number of elements in I-th row.
            //
            // Convert RIdx from row sizes to row offsets.
            // Set NInitialized
            //
            apserv.ivectorsetlengthatleast(ref s1.ridx, m + 1, _params);
            s1.ridx[0] = 0;
            for (i = 1; i <= m; i++)
            {
                s1.ridx[i] = 1;
            }
            nonne = 0;
            for (i = 0; i <= m - 1; i++)
            {
                s1.ridx[i + 1] = s0.didx[i] + s1.ridx[i + 1];
                for (j = i - s0.uidx[i]; j <= i - 1; j++)
                {
                    s1.ridx[j + 1] = s1.ridx[j + 1] + 1;
                }
                nonne = nonne + s0.didx[i] + 1 + s0.uidx[i];
            }
            for (i = 0; i <= m - 1; i++)
            {
                s1.ridx[i + 1] = s1.ridx[i + 1] + s1.ridx[i];
            }
            s1.ninitialized = s1.ridx[m];

            //
            // Allocate memory and move elements to Vals/Idx.
            // Initially, elements are sorted by rows, and are sorted within row too.
            // No additional post-sorting is required.
            //
            temp = new int[m];
            for (i = 0; i <= m - 1; i++)
            {
                temp[i] = 0;
            }
            apserv.rvectorsetlengthatleast(ref s1.vals, nonne, _params);
            apserv.ivectorsetlengthatleast(ref s1.idx, nonne, _params);
            for (i = 0; i <= m - 1; i++)
            {

                //
                // copy subdiagonal and diagonal parts of I-th block
                //
                offs0 = s0.ridx[i];
                offs1 = s1.ridx[i] + temp[i];
                k = s0.didx[i] + 1;
                for (j = 0; j <= k - 1; j++)
                {
                    s1.vals[offs1 + j] = s0.vals[offs0 + j];
                    s1.idx[offs1 + j] = i - s0.didx[i] + j;
                }
                temp[i] = temp[i] + s0.didx[i] + 1;

                //
                // Copy superdiagonal part of I-th block
                //
                offs0 = s0.ridx[i] + s0.didx[i] + 1;
                k = s0.uidx[i];
                for (j = 0; j <= k - 1; j++)
                {
                    offs1 = s1.ridx[i - k + j] + temp[i - k + j];
                    s1.vals[offs1] = s0.vals[offs0 + j];
                    s1.idx[offs1] = i;
                    temp[i - k + j] = temp[i - k + j] + 1;
                }
            }

            //
            // Initialization 'S.UIdx' and 'S.DIdx'
            //
            sparseinitduidx(s1, _params);
            return;
        }
        ap.assert(false, "SparseCopyToCRSBuf: unexpected matrix type");
    }


    /*************************************************************************
    This function performs in-place conversion to SKS format.

    INPUT PARAMETERS
        S           -   sparse matrix in any format.

    OUTPUT PARAMETERS
        S           -   sparse matrix in SKS format.

    NOTE: this  function  has   no  effect  when  called with matrix which  is
          already in SKS mode.

    NOTE: in-place conversion involves allocation of temporary arrays. If  you
          perform a lot of repeated in- place  conversions,  it  may  lead  to
          memory fragmentation. Consider using out-of-place SparseCopyToSKSBuf()
          function in this case.
        
      -- ALGLIB PROJECT --
         Copyright 15.01.2014 by Bochkanov Sergey
    *************************************************************************/
    public static void sparseconverttosks(sparsematrix s,
        xparams _params)
    {
        int[] tridx = new int[0];
        int[] tdidx = new int[0];
        int[] tuidx = new int[0];
        double[] tvals = new double[0];
        int n = 0;
        int t0 = 0;
        int t1 = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        double v = 0;

        ap.assert((s.matrixtype == 0 || s.matrixtype == 1) || s.matrixtype == 2, "SparseConvertToSKS: invalid matrix type");
        ap.assert(s.m == s.n, "SparseConvertToSKS: rectangular matrices are not supported");
        n = s.n;
        if (s.matrixtype == 2)
        {

            //
            // Already in SKS mode
            //
            return;
        }

        //
        // Generate internal copy of SKS matrix
        //
        apserv.ivectorsetlengthatleast(ref tdidx, n + 1, _params);
        apserv.ivectorsetlengthatleast(ref tuidx, n + 1, _params);
        for (i = 0; i <= n; i++)
        {
            tdidx[i] = 0;
            tuidx[i] = 0;
        }
        t0 = 0;
        t1 = 0;
        while (sparseenumerate(s, ref t0, ref t1, ref i, ref j, ref v, _params))
        {
            if (j < i)
            {
                tdidx[i] = Math.Max(tdidx[i], i - j);
            }
            else
            {
                tuidx[j] = Math.Max(tuidx[j], j - i);
            }
        }
        apserv.ivectorsetlengthatleast(ref tridx, n + 1, _params);
        tridx[0] = 0;
        for (i = 1; i <= n; i++)
        {
            tridx[i] = tridx[i - 1] + tdidx[i - 1] + 1 + tuidx[i - 1];
        }
        apserv.rvectorsetlengthatleast(ref tvals, tridx[n], _params);
        k = tridx[n];
        for (i = 0; i <= k - 1; i++)
        {
            tvals[i] = 0.0;
        }
        t0 = 0;
        t1 = 0;
        while (sparseenumerate(s, ref t0, ref t1, ref i, ref j, ref v, _params))
        {
            if (j <= i)
            {
                tvals[tridx[i] + tdidx[i] - (i - j)] = v;
            }
            else
            {
                tvals[tridx[j + 1] - (j - i)] = v;
            }
        }
        for (i = 0; i <= n - 1; i++)
        {
            tdidx[n] = Math.Max(tdidx[n], tdidx[i]);
            tuidx[n] = Math.Max(tuidx[n], tuidx[i]);
        }
        s.matrixtype = 2;
        s.ninitialized = 0;
        s.nfree = 0;
        s.m = n;
        s.n = n;
        ap.swap(ref s.didx, ref tdidx);
        ap.swap(ref s.uidx, ref tuidx);
        ap.swap(ref s.ridx, ref tridx);
        ap.swap(ref s.vals, ref tvals);
    }


    /*************************************************************************
    This  function  performs  out-of-place  conversion  to SKS storage format.
    S0 is copied to S1 and converted on-the-fly.

    INPUT PARAMETERS
        S0          -   sparse matrix in any format.

    OUTPUT PARAMETERS
        S1          -   sparse matrix in SKS format.

    NOTE: if S0 is stored as SKS, it is just copied without conversion.

    NOTE: this function de-allocates memory  occupied  by  S1 before  starting
          conversion. If you perform a  lot  of  repeated  conversions, it may
          lead to memory fragmentation. In this case we recommend you  to  use
          SparseCopyToSKSBuf() function which re-uses memory in S1 as much  as
          possible.

      -- ALGLIB PROJECT --
         Copyright 20.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsecopytosks(sparsematrix s0,
        sparsematrix s1,
        xparams _params)
    {
        ap.assert((s0.matrixtype == 0 || s0.matrixtype == 1) || s0.matrixtype == 2, "SparseCopyToSKS: invalid matrix type");
        sparsecopytosksbuf(s0, s1, _params);
    }


    /*************************************************************************
    This  function  performs  out-of-place  conversion  to SKS format.  S0  is
    copied to S1 and converted on-the-fly. Memory  allocated  in S1 is  reused
    to maximum extent possible.

    INPUT PARAMETERS
        S0          -   sparse matrix in any format.

    OUTPUT PARAMETERS
        S1          -   sparse matrix in SKS format.

    NOTE: if S0 is stored as SKS, it is just copied without conversion.

      -- ALGLIB PROJECT --
         Copyright 20.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsecopytosksbuf(sparsematrix s0,
        sparsematrix s1,
        xparams _params)
    {
        double v = 0;
        int n = 0;
        int t0 = 0;
        int t1 = 0;
        int i = 0;
        int j = 0;
        int k = 0;

        ap.assert((s0.matrixtype == 0 || s0.matrixtype == 1) || s0.matrixtype == 2, "SparseCopyToSKSBuf: invalid matrix type");
        ap.assert(s0.m == s0.n, "SparseCopyToSKSBuf: rectangular matrices are not supported");
        n = s0.n;
        if (s0.matrixtype == 2)
        {

            //
            // Already SKS, just copy
            //
            sparsecopybuf(s0, s1, _params);
            return;
        }

        //
        // Generate copy of matrix in the SKS format
        //
        apserv.ivectorsetlengthatleast(ref s1.didx, n + 1, _params);
        apserv.ivectorsetlengthatleast(ref s1.uidx, n + 1, _params);
        for (i = 0; i <= n; i++)
        {
            s1.didx[i] = 0;
            s1.uidx[i] = 0;
        }
        t0 = 0;
        t1 = 0;
        while (sparseenumerate(s0, ref t0, ref t1, ref i, ref j, ref v, _params))
        {
            if (j < i)
            {
                s1.didx[i] = Math.Max(s1.didx[i], i - j);
            }
            else
            {
                s1.uidx[j] = Math.Max(s1.uidx[j], j - i);
            }
        }
        apserv.ivectorsetlengthatleast(ref s1.ridx, n + 1, _params);
        s1.ridx[0] = 0;
        for (i = 1; i <= n; i++)
        {
            s1.ridx[i] = s1.ridx[i - 1] + s1.didx[i - 1] + 1 + s1.uidx[i - 1];
        }
        apserv.rvectorsetlengthatleast(ref s1.vals, s1.ridx[n], _params);
        k = s1.ridx[n];
        for (i = 0; i <= k - 1; i++)
        {
            s1.vals[i] = 0.0;
        }
        t0 = 0;
        t1 = 0;
        while (sparseenumerate(s0, ref t0, ref t1, ref i, ref j, ref v, _params))
        {
            if (j <= i)
            {
                s1.vals[s1.ridx[i] + s1.didx[i] - (i - j)] = v;
            }
            else
            {
                s1.vals[s1.ridx[j + 1] - (j - i)] = v;
            }
        }
        for (i = 0; i <= n - 1; i++)
        {
            s1.didx[n] = Math.Max(s1.didx[n], s1.didx[i]);
            s1.uidx[n] = Math.Max(s1.uidx[n], s1.uidx[i]);
        }
        s1.matrixtype = 2;
        s1.ninitialized = 0;
        s1.nfree = 0;
        s1.m = n;
        s1.n = n;
    }


    /*************************************************************************
    This non-accessible to user function performs  in-place  creation  of  CRS
    matrix. It is expected that:
    * S.M and S.N are initialized
    * S.RIdx, S.Idx and S.Vals are loaded with values in CRS  format  used  by
      ALGLIB, with elements of S.Idx/S.Vals  possibly  being  unsorted  within
      each row (this constructor function may post-sort matrix,  assuming that
      it is sorted by rows).
      
    Only 5 fields should be set by caller. Other fields will be  rewritten  by
    this constructor function.

    This function performs integrity check on user-specified values, with  the
    only exception being Vals[] array:
    * it does not require values to be non-zero
    * it does not check for elements of Vals[] being finite IEEE-754 values

    INPUT PARAMETERS
        S   -   sparse matrix with corresponding fields set by caller

    OUTPUT PARAMETERS
        S   -   sparse matrix in CRS format.

      -- ALGLIB PROJECT --
         Copyright 20.08.2016 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsecreatecrsinplace(sparsematrix s,
        xparams _params)
    {
        int m = 0;
        int n = 0;
        int i = 0;
        int j = 0;
        int j0 = 0;
        int j1 = 0;

        m = s.m;
        n = s.n;

        //
        // Quick exit for M=0 or N=0
        //
        ap.assert(s.m >= 0, "SparseCreateCRSInplace: integrity check failed");
        ap.assert(s.n >= 0, "SparseCreateCRSInplace: integrity check failed");
        if (m == 0 || n == 0)
        {
            s.matrixtype = 1;
            s.ninitialized = 0;
            apserv.ivectorsetlengthatleast(ref s.ridx, s.m + 1, _params);
            apserv.ivectorsetlengthatleast(ref s.didx, s.m, _params);
            apserv.ivectorsetlengthatleast(ref s.uidx, s.m, _params);
            for (i = 0; i <= s.m - 1; i++)
            {
                s.ridx[i] = 0;
                s.uidx[i] = 0;
                s.didx[i] = 0;
            }
            s.ridx[s.m] = 0;
            return;
        }

        //
        // Perform integrity check
        //
        ap.assert(s.m > 0, "SparseCreateCRSInplace: integrity check failed");
        ap.assert(s.n > 0, "SparseCreateCRSInplace: integrity check failed");
        ap.assert(ap.len(s.ridx) >= m + 1, "SparseCreateCRSInplace: integrity check failed");
        for (i = 0; i <= m - 1; i++)
        {
            ap.assert(s.ridx[i] >= 0 && s.ridx[i] <= s.ridx[i + 1], "SparseCreateCRSInplace: integrity check failed");
        }
        ap.assert(s.ridx[m] <= ap.len(s.idx), "SparseCreateCRSInplace: integrity check failed");
        ap.assert(s.ridx[m] <= ap.len(s.vals), "SparseCreateCRSInplace: integrity check failed");
        for (i = 0; i <= m - 1; i++)
        {
            j0 = s.ridx[i];
            j1 = s.ridx[i + 1] - 1;
            for (j = j0; j <= j1; j++)
            {
                ap.assert(s.idx[j] >= 0 && s.idx[j] < n, "SparseCreateCRSInplace: integrity check failed");
            }
        }

        //
        // Initialize
        //
        s.matrixtype = 1;
        s.ninitialized = s.ridx[m];
        for (i = 0; i <= m - 1; i++)
        {
            tsort.tagsortmiddleir(ref s.idx, ref s.vals, s.ridx[i], s.ridx[i + 1] - s.ridx[i], _params);
        }
        sparseinitduidx(s, _params);
    }


    /*************************************************************************
    This function returns type of the matrix storage format.

    INPUT PARAMETERS:
        S           -   sparse matrix.

    RESULT:
        sparse storage format used by matrix:
            0   -   Hash-table
            1   -   CRS (compressed row storage)
            2   -   SKS (skyline)

    NOTE: future  versions  of  ALGLIB  may  include additional sparse storage
          formats.

        
      -- ALGLIB PROJECT --
         Copyright 20.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static int sparsegetmatrixtype(sparsematrix s,
        xparams _params)
    {
        int result = 0;

        ap.assert((((s.matrixtype == 0 || s.matrixtype == 1) || s.matrixtype == 2) || s.matrixtype == -10081) || s.matrixtype == -10082, "SparseGetMatrixType: invalid matrix type");
        result = s.matrixtype;
        return result;
    }


    /*************************************************************************
    This function checks matrix storage format and returns True when matrix is
    stored using Hash table representation.

    INPUT PARAMETERS:
        S   -   sparse matrix.

    RESULT:
        True if matrix type is Hash table
        False if matrix type is not Hash table 
        
      -- ALGLIB PROJECT --
         Copyright 20.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static bool sparseishash(sparsematrix s,
        xparams _params)
    {
        bool result = new bool();

        ap.assert((((s.matrixtype == 0 || s.matrixtype == 1) || s.matrixtype == 2) || s.matrixtype == -10081) || s.matrixtype == -10082, "SparseIsHash: invalid matrix type");
        result = s.matrixtype == 0;
        return result;
    }


    /*************************************************************************
    This function checks matrix storage format and returns True when matrix is
    stored using CRS representation.

    INPUT PARAMETERS:
        S   -   sparse matrix.

    RESULT:
        True if matrix type is CRS
        False if matrix type is not CRS
        
      -- ALGLIB PROJECT --
         Copyright 20.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static bool sparseiscrs(sparsematrix s,
        xparams _params)
    {
        bool result = new bool();

        ap.assert((((s.matrixtype == 0 || s.matrixtype == 1) || s.matrixtype == 2) || s.matrixtype == -10081) || s.matrixtype == -10082, "SparseIsCRS: invalid matrix type");
        result = s.matrixtype == 1;
        return result;
    }


    /*************************************************************************
    This function checks matrix storage format and returns True when matrix is
    stored using SKS representation.

    INPUT PARAMETERS:
        S   -   sparse matrix.

    RESULT:
        True if matrix type is SKS
        False if matrix type is not SKS
        
      -- ALGLIB PROJECT --
         Copyright 20.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static bool sparseissks(sparsematrix s,
        xparams _params)
    {
        bool result = new bool();

        ap.assert((((s.matrixtype == 0 || s.matrixtype == 1) || s.matrixtype == 2) || s.matrixtype == -10081) || s.matrixtype == -10082, "SparseIsSKS: invalid matrix type");
        result = s.matrixtype == 2;
        return result;
    }


    /*************************************************************************
    The function frees all memory occupied by  sparse  matrix.  Sparse  matrix
    structure becomes unusable after this call.

    OUTPUT PARAMETERS
        S   -   sparse matrix to delete
        
      -- ALGLIB PROJECT --
         Copyright 24.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsefree(sparsematrix s,
        xparams _params)
    {
        s.matrixtype = -1;
        s.m = 0;
        s.n = 0;
        s.nfree = 0;
        s.ninitialized = 0;
        s.tablesize = 0;
    }


    /*************************************************************************
    The function returns number of rows of a sparse matrix.

    RESULT: number of rows of a sparse matrix.
        
      -- ALGLIB PROJECT --
         Copyright 23.08.2012 by Bochkanov Sergey
    *************************************************************************/
    public static int sparsegetnrows(sparsematrix s,
        xparams _params)
    {
        int result = 0;

        result = s.m;
        return result;
    }


    /*************************************************************************
    The function returns number of columns of a sparse matrix.

    RESULT: number of columns of a sparse matrix.
        
      -- ALGLIB PROJECT --
         Copyright 23.08.2012 by Bochkanov Sergey
    *************************************************************************/
    public static int sparsegetncols(sparsematrix s,
        xparams _params)
    {
        int result = 0;

        result = s.n;
        return result;
    }


    /*************************************************************************
    The function returns number of strictly upper triangular non-zero elements
    in  the  matrix.  It  counts  SYMBOLICALLY non-zero elements, i.e. entries
    in the sparse matrix data structure. If some element  has  zero  numerical
    value, it is still counted.

    This function has different cost for different types of matrices:
    * for hash-based matrices it involves complete pass over entire hash-table
      with O(NNZ) cost, where NNZ is number of non-zero elements
    * for CRS and SKS matrix types cost of counting is O(N) (N - matrix size).

    RESULT: number of non-zero elements strictly above main diagonal
        
      -- ALGLIB PROJECT --
         Copyright 12.02.2014 by Bochkanov Sergey
    *************************************************************************/
    public static int sparsegetuppercount(sparsematrix s,
        xparams _params)
    {
        int result = 0;
        int sz = 0;
        int i0 = 0;
        int i = 0;

        result = -1;
        if (s.matrixtype == 0)
        {

            //
            // Hash-table matrix
            //
            result = 0;
            sz = s.tablesize;
            for (i0 = 0; i0 <= sz - 1; i0++)
            {
                i = s.idx[2 * i0];
                if (i >= 0 && s.idx[2 * i0 + 1] > i)
                {
                    result = result + 1;
                }
            }
            return result;
        }
        if (s.matrixtype == 1)
        {

            //
            // CRS matrix
            //
            ap.assert(s.ninitialized == s.ridx[s.m], "SparseGetUpperCount: some rows/elements of the CRS matrix were not initialized (you must initialize everything you promised to SparseCreateCRS)");
            result = 0;
            sz = s.m;
            for (i = 0; i <= sz - 1; i++)
            {
                result = result + (s.ridx[i + 1] - s.uidx[i]);
            }
            return result;
        }
        if (s.matrixtype == 2)
        {

            //
            // SKS matrix
            //
            ap.assert(s.m == s.n, "SparseGetUpperCount: non-square SKS matrices are not supported");
            result = 0;
            sz = s.m;
            for (i = 0; i <= sz - 1; i++)
            {
                result = result + s.uidx[i];
            }
            return result;
        }
        ap.assert(false, "SparseGetUpperCount: internal error");
        return result;
    }


    /*************************************************************************
    The function returns number of strictly lower triangular non-zero elements
    in  the  matrix.  It  counts  SYMBOLICALLY non-zero elements, i.e. entries
    in the sparse matrix data structure. If some element  has  zero  numerical
    value, it is still counted.

    This function has different cost for different types of matrices:
    * for hash-based matrices it involves complete pass over entire hash-table
      with O(NNZ) cost, where NNZ is number of non-zero elements
    * for CRS and SKS matrix types cost of counting is O(N) (N - matrix size).

    RESULT: number of non-zero elements strictly below main diagonal
        
      -- ALGLIB PROJECT --
         Copyright 12.02.2014 by Bochkanov Sergey
    *************************************************************************/
    public static int sparsegetlowercount(sparsematrix s,
        xparams _params)
    {
        int result = 0;
        int sz = 0;
        int i0 = 0;
        int i = 0;

        result = -1;
        if (s.matrixtype == 0)
        {

            //
            // Hash-table matrix
            //
            result = 0;
            sz = s.tablesize;
            for (i0 = 0; i0 <= sz - 1; i0++)
            {
                i = s.idx[2 * i0];
                if (i >= 0 && s.idx[2 * i0 + 1] < i)
                {
                    result = result + 1;
                }
            }
            return result;
        }
        if (s.matrixtype == 1)
        {

            //
            // CRS matrix
            //
            ap.assert(s.ninitialized == s.ridx[s.m], "SparseGetUpperCount: some rows/elements of the CRS matrix were not initialized (you must initialize everything you promised to SparseCreateCRS)");
            result = 0;
            sz = s.m;
            for (i = 0; i <= sz - 1; i++)
            {
                result = result + (s.didx[i] - s.ridx[i]);
            }
            return result;
        }
        if (s.matrixtype == 2)
        {

            //
            // SKS matrix
            //
            ap.assert(s.m == s.n, "SparseGetUpperCount: non-square SKS matrices are not supported");
            result = 0;
            sz = s.m;
            for (i = 0; i <= sz - 1; i++)
            {
                result = result + s.didx[i];
            }
            return result;
        }
        ap.assert(false, "SparseGetUpperCount: internal error");
        return result;
    }


    /*************************************************************************
    Serializer: allocation.

    INTERNAL-ONLY FUNCTION, SUPPORTS ONLY CRS MATRICES

      -- ALGLIB --
         Copyright 20.07.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void sparsealloc(serializer s,
        sparsematrix a,
        xparams _params)
    {
        int i = 0;
        int nused = 0;

        ap.assert((a.matrixtype == 0 || a.matrixtype == 1) || a.matrixtype == 2, "SparseAlloc: only CRS/SKS matrices are supported");

        //
        // Header
        //
        s.alloc_entry();
        s.alloc_entry();
        s.alloc_entry();

        //
        // Alloc other parameters
        //
        if (a.matrixtype == 0)
        {

            //
            // Alloc Hash
            //
            nused = 0;
            for (i = 0; i <= a.tablesize - 1; i++)
            {
                if (a.idx[2 * i + 0] >= 0)
                {
                    nused = nused + 1;
                }
            }
            s.alloc_entry();
            s.alloc_entry();
            s.alloc_entry();
            for (i = 0; i <= a.tablesize - 1; i++)
            {
                if (a.idx[2 * i + 0] >= 0)
                {
                    s.alloc_entry();
                    s.alloc_entry();
                    s.alloc_entry();
                }
            }
        }
        if (a.matrixtype == 1)
        {

            //
            // Alloc CRS
            //
            s.alloc_entry();
            s.alloc_entry();
            s.alloc_entry();
            apserv.allocintegerarray(s, a.ridx, a.m + 1, _params);
            apserv.allocintegerarray(s, a.idx, a.ridx[a.m], _params);
            apserv.allocrealarray(s, a.vals, a.ridx[a.m], _params);
        }
        if (a.matrixtype == 2)
        {

            //
            // Alloc SKS
            //
            ap.assert(a.m == a.n, "SparseAlloc: rectangular SKS serialization is not supported");
            s.alloc_entry();
            s.alloc_entry();
            apserv.allocintegerarray(s, a.ridx, a.m + 1, _params);
            apserv.allocintegerarray(s, a.didx, a.n + 1, _params);
            apserv.allocintegerarray(s, a.uidx, a.n + 1, _params);
            apserv.allocrealarray(s, a.vals, a.ridx[a.m], _params);
        }

        //
        // End of stream
        //
        s.alloc_entry();
    }


    /*************************************************************************
    Serializer: serialization

    INTERNAL-ONLY FUNCTION, SUPPORTS ONLY CRS MATRICES

      -- ALGLIB --
         Copyright 20.07.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void sparseserialize(serializer s,
        sparsematrix a,
        xparams _params)
    {
        int i = 0;
        int nused = 0;

        ap.assert((a.matrixtype == 0 || a.matrixtype == 1) || a.matrixtype == 2, "SparseSerialize: only CRS/SKS matrices are supported");

        //
        // Header
        //
        s.serialize_int(scodes.getsparsematrixserializationcode(_params));
        s.serialize_int(a.matrixtype);
        s.serialize_int(0);

        //
        // Serialize other parameters
        //
        if (a.matrixtype == 0)
        {

            //
            // Serialize Hash
            //
            nused = 0;
            for (i = 0; i <= a.tablesize - 1; i++)
            {
                if (a.idx[2 * i + 0] >= 0)
                {
                    nused = nused + 1;
                }
            }
            s.serialize_int(a.m);
            s.serialize_int(a.n);
            s.serialize_int(nused);
            for (i = 0; i <= a.tablesize - 1; i++)
            {
                if (a.idx[2 * i + 0] >= 0)
                {
                    s.serialize_int(a.idx[2 * i + 0]);
                    s.serialize_int(a.idx[2 * i + 1]);
                    s.serialize_double(a.vals[i]);
                }
            }
        }
        if (a.matrixtype == 1)
        {

            //
            // Serialize CRS
            //
            s.serialize_int(a.m);
            s.serialize_int(a.n);
            s.serialize_int(a.ninitialized);
            apserv.serializeintegerarray(s, a.ridx, a.m + 1, _params);
            apserv.serializeintegerarray(s, a.idx, a.ridx[a.m], _params);
            apserv.serializerealarray(s, a.vals, a.ridx[a.m], _params);
        }
        if (a.matrixtype == 2)
        {

            //
            // Serialize SKS
            //
            ap.assert(a.m == a.n, "SparseSerialize: rectangular SKS serialization is not supported");
            s.serialize_int(a.m);
            s.serialize_int(a.n);
            apserv.serializeintegerarray(s, a.ridx, a.m + 1, _params);
            apserv.serializeintegerarray(s, a.didx, a.n + 1, _params);
            apserv.serializeintegerarray(s, a.uidx, a.n + 1, _params);
            apserv.serializerealarray(s, a.vals, a.ridx[a.m], _params);
        }

        //
        // End of stream
        //
        s.serialize_int(117);
    }


    /*************************************************************************
    Serializer: unserialization

      -- ALGLIB --
         Copyright 20.07.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void sparseunserialize(serializer s,
        sparsematrix a,
        xparams _params)
    {
        int i = 0;
        int i0 = 0;
        int i1 = 0;
        int m = 0;
        int n = 0;
        int nused = 0;
        int k = 0;
        double v = 0;


        //
        // Check stream header: scode, matrix type, version type
        //
        k = s.unserialize_int();
        ap.assert(k == scodes.getsparsematrixserializationcode(_params), "SparseUnserialize: stream header corrupted");
        a.matrixtype = s.unserialize_int();
        ap.assert((a.matrixtype == 0 || a.matrixtype == 1) || a.matrixtype == 2, "SparseUnserialize: unexpected matrix type");
        k = s.unserialize_int();
        ap.assert(k == 0, "SparseUnserialize: stream header corrupted");

        //
        // Unserialize other parameters
        //
        if (a.matrixtype == 0)
        {

            //
            // Unerialize Hash
            //
            m = s.unserialize_int();
            n = s.unserialize_int();
            nused = s.unserialize_int();
            sparsecreate(m, n, nused, a, _params);
            for (i = 0; i <= nused - 1; i++)
            {
                i0 = s.unserialize_int();
                i1 = s.unserialize_int();
                v = s.unserialize_double();
                sparseset(a, i0, i1, v, _params);
            }
        }
        if (a.matrixtype == 1)
        {

            //
            // Unserialize CRS
            //
            a.m = s.unserialize_int();
            a.n = s.unserialize_int();
            a.ninitialized = s.unserialize_int();
            apserv.unserializeintegerarray(s, ref a.ridx, _params);
            apserv.unserializeintegerarray(s, ref a.idx, _params);
            apserv.unserializerealarray(s, ref a.vals, _params);
            sparseinitduidx(a, _params);
        }
        if (a.matrixtype == 2)
        {

            //
            // Unserialize SKS
            //
            a.m = s.unserialize_int();
            a.n = s.unserialize_int();
            ap.assert(a.m == a.n, "SparseUnserialize: rectangular SKS unserialization is not supported");
            apserv.unserializeintegerarray(s, ref a.ridx, _params);
            apserv.unserializeintegerarray(s, ref a.didx, _params);
            apserv.unserializeintegerarray(s, ref a.uidx, _params);
            apserv.unserializerealarray(s, ref a.vals, _params);
        }

        //
        // End of stream
        //
        k = s.unserialize_int();
        ap.assert(k == 117, "SparseMatrixUnserialize: end-of-stream marker not found");
    }


    /*************************************************************************
    This is hash function.

      -- ALGLIB PROJECT --
         Copyright 14.10.2011 by Bochkanov Sergey
    *************************************************************************/
    private static int hash(int i,
        int j,
        int tabsize,
        xparams _params)
    {
        int result = 0;
        hqrnd.hqrndstate r = new hqrnd.hqrndstate();

        hqrnd.hqrndseed(i, j, r, _params);
        result = hqrnd.hqrnduniformi(r, tabsize, _params);
        return result;
    }


}

