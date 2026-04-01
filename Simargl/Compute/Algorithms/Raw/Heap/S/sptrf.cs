#pragma warning disable CS8618
#pragma warning disable CS1591

using System;

namespace Simargl.Algorithms.Raw;

public class sptrf
{
    /*************************************************************************
    This structure is used by sparse LU to store "left" and "upper" rectangular
    submatrices BL and BU, as defined below:


        [    |         :       ]
        [ LU |   BU    :       ]
        [    |         :       ]
        [--------------: dense ]
        [    |         : trail ]
        [    |  sparse :       ]
        [ BL |         :       ]
        [    |  trail  :       ]
        [    |         :       ]
        

    *************************************************************************/
    public class sluv2list1matrix : apobject
    {
        public int nfixed;
        public int ndynamic;
        public int[] idxfirst;
        public int[] strgidx;
        public double[] strgval;
        public int nallocated;
        public int nused;
        public sluv2list1matrix()
        {
            init();
        }
        public override void init()
        {
            idxfirst = new int[0];
            strgidx = new int[0];
            strgval = new double[0];
        }
        public override apobject make_copy()
        {
            sluv2list1matrix _result = new sluv2list1matrix();
            _result.nfixed = nfixed;
            _result.ndynamic = ndynamic;
            _result.idxfirst = (int[])idxfirst.Clone();
            _result.strgidx = (int[])strgidx.Clone();
            _result.strgval = (double[])strgval.Clone();
            _result.nallocated = nallocated;
            _result.nused = nused;
            return _result;
        }
    };


    /*************************************************************************
    This structure is used by sparse LU to store sparse trail submatrix as
    defined below:


        [    |         :       ]
        [ LU |   BU    :       ]
        [    |         :       ]
        [--------------: dense ]
        [    |         : trail ]
        [    |  sparse :       ]
        [ BL |         :       ]
        [    |  trail  :       ]
        [    |         :       ]
        

    *************************************************************************/
    public class sluv2sparsetrail : apobject
    {
        public int n;
        public int k;
        public int[] nzc;
        public int maxwrkcnt;
        public int maxwrknz;
        public int wrkcnt;
        public int[] wrkset;
        public int[] colid;
        public bool[] isdensified;
        public int[] slscolptr;
        public int[] slsrowptr;
        public int[] slsidx;
        public double[] slsval;
        public int slsused;
        public double[] tmp0;
        public sluv2sparsetrail()
        {
            init();
        }
        public override void init()
        {
            nzc = new int[0];
            wrkset = new int[0];
            colid = new int[0];
            isdensified = new bool[0];
            slscolptr = new int[0];
            slsrowptr = new int[0];
            slsidx = new int[0];
            slsval = new double[0];
            tmp0 = new double[0];
        }
        public override apobject make_copy()
        {
            sluv2sparsetrail _result = new sluv2sparsetrail();
            _result.n = n;
            _result.k = k;
            _result.nzc = (int[])nzc.Clone();
            _result.maxwrkcnt = maxwrkcnt;
            _result.maxwrknz = maxwrknz;
            _result.wrkcnt = wrkcnt;
            _result.wrkset = (int[])wrkset.Clone();
            _result.colid = (int[])colid.Clone();
            _result.isdensified = (bool[])isdensified.Clone();
            _result.slscolptr = (int[])slscolptr.Clone();
            _result.slsrowptr = (int[])slsrowptr.Clone();
            _result.slsidx = (int[])slsidx.Clone();
            _result.slsval = (double[])slsval.Clone();
            _result.slsused = slsused;
            _result.tmp0 = (double[])tmp0.Clone();
            return _result;
        }
    };


    /*************************************************************************
    This structure is used by sparse LU to store dense trail submatrix as
    defined below:


        [    |         :       ]
        [ LU |   BU    :       ]
        [    |         :       ]
        [--------------: dense ]
        [    |         : trail ]
        [    |  sparse :       ]
        [ BL |         :       ]
        [    |  trail  :       ]
        [    |         :       ]
        

    *************************************************************************/
    public class sluv2densetrail : apobject
    {
        public int n;
        public int ndense;
        public double[,] d;
        public int[] did;
        public sluv2densetrail()
        {
            init();
        }
        public override void init()
        {
            d = new double[0, 0];
            did = new int[0];
        }
        public override apobject make_copy()
        {
            sluv2densetrail _result = new sluv2densetrail();
            _result.n = n;
            _result.ndense = ndense;
            _result.d = (double[,])d.Clone();
            _result.did = (int[])did.Clone();
            return _result;
        }
    };


    /*************************************************************************
    This structure is used by sparse LU for buffer storage

    *************************************************************************/
    public class sluv2buffer : apobject
    {
        public int n;
        public sparse.sparsematrix sparsel;
        public sparse.sparsematrix sparseut;
        public sluv2list1matrix bleft;
        public sluv2list1matrix bupper;
        public sluv2sparsetrail strail;
        public sluv2densetrail dtrail;
        public int[] rowpermrawidx;
        public double[,] dbuf;
        public int[] v0i;
        public int[] v1i;
        public double[] v0r;
        public double[] v1r;
        public double[] tmp0;
        public int[] tmpi;
        public int[] tmpp;
        public sluv2buffer()
        {
            init();
        }
        public override void init()
        {
            sparsel = new sparse.sparsematrix();
            sparseut = new sparse.sparsematrix();
            bleft = new sluv2list1matrix();
            bupper = new sluv2list1matrix();
            strail = new sluv2sparsetrail();
            dtrail = new sluv2densetrail();
            rowpermrawidx = new int[0];
            dbuf = new double[0, 0];
            v0i = new int[0];
            v1i = new int[0];
            v0r = new double[0];
            v1r = new double[0];
            tmp0 = new double[0];
            tmpi = new int[0];
            tmpp = new int[0];
        }
        public override apobject make_copy()
        {
            sluv2buffer _result = new sluv2buffer();
            _result.n = n;
            _result.sparsel = (sparse.sparsematrix)sparsel.make_copy();
            _result.sparseut = (sparse.sparsematrix)sparseut.make_copy();
            _result.bleft = (sluv2list1matrix)bleft.make_copy();
            _result.bupper = (sluv2list1matrix)bupper.make_copy();
            _result.strail = (sluv2sparsetrail)strail.make_copy();
            _result.dtrail = (sluv2densetrail)dtrail.make_copy();
            _result.rowpermrawidx = (int[])rowpermrawidx.Clone();
            _result.dbuf = (double[,])dbuf.Clone();
            _result.v0i = (int[])v0i.Clone();
            _result.v1i = (int[])v1i.Clone();
            _result.v0r = (double[])v0r.Clone();
            _result.v1r = (double[])v1r.Clone();
            _result.tmp0 = (double[])tmp0.Clone();
            _result.tmpi = (int[])tmpi.Clone();
            _result.tmpp = (int[])tmpp.Clone();
            return _result;
        }
    };




    public const double densebnd = 0.10;
    public const int slswidth = 8;


    /*************************************************************************
    Sparse LU for square NxN CRS matrix with both row and column permutations.

    Represents A as Pr*L*U*Pc, where:
    * Pr is a product of row permutations Pr=Pr(0)*Pr(1)*...*Pr(n-2)*Pr(n-1)
    * Pc is a product of col permutations Pc=Pc(n-1)*Pc(n-2)*...*Pc(1)*Pc(0)
    * L is lower unitriangular
    * U is upper triangular

    INPUT PARAMETERS:
        A           -   sparse square matrix in CRS format
        PivotType   -   pivot type:
                        * 0 - for best pivoting available
                        * 1 - row-only pivoting
                        * 2 - row and column greedy pivoting  algorithm  (most
                              sparse pivot column is selected from the trailing
                              matrix at each step)
        Buf         -   temporary buffer, previously allocated memory is
                        reused as much as possible

    OUTPUT PARAMETERS:
        A           -   LU decomposition of A
        PR          -   array[N], row pivots
        PC          -   array[N], column pivots
        Buf         -   following fields of Buf are set:
                        * Buf.RowPermRawIdx[] - contains row permutation, with
                          RawIdx[I]=J meaning that J-th row  of  the  original
                          input matrix was moved to Ith position of the output
                          factorization
        
    This function always succeeds  i.e. it ALWAYS returns valid factorization,
    but for your convenience it also  returns boolean  value  which  helps  to
    detect symbolically degenerate matrix:
    * function returns TRUE if the matrix was factorized AND symbolically
      non-degenerate
    * function returns FALSE if the matrix was factorized but U has strictly
      zero elements at the diagonal (the factorization is returned anyway).

      -- ALGLIB routine --
         15.01.2019
         Bochkanov Sergey
    *************************************************************************/
    public static bool sptrflu(sparse.sparsematrix a,
        int pivottype,
        ref int[] pr,
        ref int[] pc,
        sluv2buffer buf,
        xparams _params)
    {
        bool result = new bool();
        int n = 0;
        int k = 0;
        int i = 0;
        int j = 0;
        int jp = 0;
        int i0 = 0;
        int i1 = 0;
        int ibest = 0;
        int jbest = 0;
        double v = 0;
        double v0 = 0;
        int nz0 = 0;
        int nz1 = 0;
        double uu = 0;
        int offs = 0;
        int tmpndense = 0;
        bool densificationsupported = new bool();
        int densifyabove = 0;

        ap.assert(sparse.sparseiscrs(a, _params), "SparseLU: A is not stored in CRS format");
        ap.assert(sparse.sparsegetnrows(a, _params) == sparse.sparsegetncols(a, _params), "SparseLU: non-square A");
        ap.assert((pivottype == 0 || pivottype == 1) || pivottype == 2, "SparseLU: unexpected pivot type");
        result = true;
        n = sparse.sparsegetnrows(a, _params);
        if (pivottype == 0)
        {
            pivottype = 2;
        }
        densificationsupported = pivottype == 2;

        //
        //
        //
        buf.n = n;
        apserv.ivectorsetlengthatleast(ref buf.rowpermrawidx, n, _params);
        for (i = 0; i <= n - 1; i++)
        {
            buf.rowpermrawidx[i] = i;
        }

        //
        // Allocate storage for sparse L and U factors
        //
        // NOTE: SparseMatrix structure for these factors is only
        //       partially initialized; we use it just as a temporary
        //       storage and do not intend to use facilities of the
        //       'sparse' subpackage to work with these objects.
        //
        buf.sparsel.matrixtype = 1;
        buf.sparsel.m = n;
        buf.sparsel.n = n;
        apserv.ivectorsetlengthatleast(ref buf.sparsel.ridx, n + 1, _params);
        buf.sparsel.ridx[0] = 0;
        buf.sparseut.matrixtype = 1;
        buf.sparseut.m = n;
        buf.sparseut.n = n;
        apserv.ivectorsetlengthatleast(ref buf.sparseut.ridx, n + 1, _params);
        buf.sparseut.ridx[0] = 0;

        //
        // Allocate unprocessed yet part of the matrix,
        // two submatrices:
        // * BU, upper J rows of columns [J,N), upper submatrix
        // * BL, left J  cols of rows [J,N), left submatrix
        // * B1, (N-J)*(N-J) square submatrix
        //
        sluv2list1init(n, buf.bleft, _params);
        sluv2list1init(n, buf.bupper, _params);
        apserv.ivectorsetlengthatleast(ref pr, n, _params);
        apserv.ivectorsetlengthatleast(ref pc, n, _params);
        apserv.ivectorsetlengthatleast(ref buf.v0i, n, _params);
        apserv.ivectorsetlengthatleast(ref buf.v1i, n, _params);
        apserv.rvectorsetlengthatleast(ref buf.v0r, n, _params);
        apserv.rvectorsetlengthatleast(ref buf.v1r, n, _params);
        sparsetrailinit(a, buf.strail, _params);

        //
        // Prepare dense trail, initial densification
        //
        densetrailinit(buf.dtrail, n, _params);
        densifyabove = (int)Math.Round(densebnd * n) + 1;
        if (densificationsupported)
        {
            for (i = 0; i <= n - 1; i++)
            {
                if (buf.strail.nzc[i] > densifyabove)
                {
                    sparsetraildensify(buf.strail, i, buf.bupper, buf.dtrail, _params);
                }
            }
        }

        //
        // Process sparse part
        //
        for (k = 0; k <= n - 1; k++)
        {

            //
            // Find pivot column and pivot row
            //
            if (!sparsetrailfindpivot(buf.strail, pivottype, ref ibest, ref jbest, _params))
            {

                //
                // Only densified columns are left, break sparse iteration
                //
                ap.assert(buf.dtrail.ndense + k == n, "SPTRF: integrity check failed (35741)");
                break;
            }
            pc[k] = jbest;
            pr[k] = ibest;
            j = buf.rowpermrawidx[k];
            buf.rowpermrawidx[k] = buf.rowpermrawidx[ibest];
            buf.rowpermrawidx[ibest] = j;

            //
            // Apply pivoting to BL and BU
            //
            sluv2list1swap(buf.bleft, k, ibest, _params);
            sluv2list1swap(buf.bupper, k, jbest, _params);

            //
            // Apply pivoting to sparse trail, pivot out
            //
            sparsetrailpivotout(buf.strail, ibest, jbest, ref uu, buf.v0i, buf.v0r, ref nz0, buf.v1i, buf.v1r, ref nz1, _params);
            result = result && uu != 0;

            //
            // Pivot dense trail
            //
            tmpndense = buf.dtrail.ndense;
            for (i = 0; i <= tmpndense - 1; i++)
            {
                v = buf.dtrail.d[k, i];
                buf.dtrail.d[k, i] = buf.dtrail.d[ibest, i];
                buf.dtrail.d[ibest, i] = v;
            }

            //
            // Output to LU matrix
            //
            sluv2list1appendsequencetomatrix(buf.bupper, k, true, uu, n, buf.sparseut, k, _params);
            sluv2list1appendsequencetomatrix(buf.bleft, k, false, 0.0, n, buf.sparsel, k, _params);

            //
            // Extract K-th col/row of B1, generate K-th col/row of BL/BU, update NZC
            //
            sluv2list1pushsparsevector(buf.bleft, buf.v0i, buf.v0r, nz0, _params);
            sluv2list1pushsparsevector(buf.bupper, buf.v1i, buf.v1r, nz1, _params);

            //
            // Update the rest of the matrix
            //
            if (nz0 * (nz1 + buf.dtrail.ndense) > 0)
            {

                //
                // Update dense trail
                //
                // NOTE: this update MUST be performed before we update sparse trail,
                //       because sparse update may move columns to dense storage after
                //       update is performed on them. Thus, we have to avoid applying
                //       same update twice.
                //
                if (buf.dtrail.ndense > 0)
                {
                    tmpndense = buf.dtrail.ndense;
                    for (i = 0; i <= nz0 - 1; i++)
                    {
                        i0 = buf.v0i[i];
                        v0 = buf.v0r[i];
                        for (j = 0; j <= tmpndense - 1; j++)
                        {
                            buf.dtrail.d[i0, j] = buf.dtrail.d[i0, j] - v0 * buf.dtrail.d[k, j];
                        }
                    }
                }

                //
                // Update sparse trail
                //
                sparsetrailupdate(buf.strail, buf.v0i, buf.v0r, nz0, buf.v1i, buf.v1r, nz1, buf.bupper, buf.dtrail, densificationsupported, _params);
            }
        }

        //
        // Process densified trail
        //
        if (buf.dtrail.ndense > 0)
        {
            tmpndense = buf.dtrail.ndense;

            //
            // Generate column pivots to bring actual order of columns in the
            // working part of the matrix to one used for dense storage
            //
            for (i = n - tmpndense; i <= n - 1; i++)
            {
                k = buf.dtrail.did[i - (n - tmpndense)];
                jp = -1;
                for (j = i; j <= n - 1; j++)
                {
                    if (buf.strail.colid[j] == k)
                    {
                        jp = j;
                        break;
                    }
                }
                ap.assert(jp >= 0, "SPTRF: integrity check failed during reordering");
                k = buf.strail.colid[i];
                buf.strail.colid[i] = buf.strail.colid[jp];
                buf.strail.colid[jp] = k;
                pc[i] = jp;
            }

            //
            // Perform dense LU decomposition on dense trail
            //
            apserv.rmatrixsetlengthatleast(ref buf.dbuf, buf.dtrail.ndense, buf.dtrail.ndense, _params);
            for (i = 0; i <= tmpndense - 1; i++)
            {
                for (j = 0; j <= tmpndense - 1; j++)
                {
                    buf.dbuf[i, j] = buf.dtrail.d[i + (n - tmpndense), j];
                }
            }
            apserv.rvectorsetlengthatleast(ref buf.tmp0, 2 * n, _params);
            apserv.ivectorsetlengthatleast(ref buf.tmpp, n, _params);
            dlu.rmatrixplurec(buf.dbuf, 0, tmpndense, tmpndense, ref buf.tmpp, ref buf.tmp0, _params);

            //
            // Convert indexes of rows pivots, swap elements of BLeft
            //
            for (i = 0; i <= tmpndense - 1; i++)
            {
                pr[i + (n - tmpndense)] = buf.tmpp[i] + (n - tmpndense);
                sluv2list1swap(buf.bleft, i + (n - tmpndense), pr[i + (n - tmpndense)], _params);
                j = buf.rowpermrawidx[i + (n - tmpndense)];
                buf.rowpermrawidx[i + (n - tmpndense)] = buf.rowpermrawidx[pr[i + (n - tmpndense)]];
                buf.rowpermrawidx[pr[i + (n - tmpndense)]] = j;
            }

            //
            // Convert U-factor
            //
            apserv.ivectorgrowto(ref buf.sparseut.idx, buf.sparseut.ridx[n - tmpndense] + n * tmpndense, _params);
            apserv.rvectorgrowto(ref buf.sparseut.vals, buf.sparseut.ridx[n - tmpndense] + n * tmpndense, _params);
            for (j = 0; j <= tmpndense - 1; j++)
            {
                offs = buf.sparseut.ridx[j + (n - tmpndense)];
                k = n - tmpndense;

                //
                // Convert leading N-NDense columns
                //
                for (i = 0; i <= k - 1; i++)
                {
                    v = buf.dtrail.d[i, j];
                    if (v != 0)
                    {
                        buf.sparseut.idx[offs] = i;
                        buf.sparseut.vals[offs] = v;
                        offs = offs + 1;
                    }
                }

                //
                // Convert upper diagonal elements
                //
                for (i = 0; i <= j - 1; i++)
                {
                    v = buf.dbuf[i, j];
                    if (v != 0)
                    {
                        buf.sparseut.idx[offs] = i + (n - tmpndense);
                        buf.sparseut.vals[offs] = v;
                        offs = offs + 1;
                    }
                }

                //
                // Convert diagonal element (always stored)
                //
                v = buf.dbuf[j, j];
                buf.sparseut.idx[offs] = j + (n - tmpndense);
                buf.sparseut.vals[offs] = v;
                offs = offs + 1;
                result = result && v != 0;

                //
                // Column is done
                //
                buf.sparseut.ridx[j + (n - tmpndense) + 1] = offs;
            }

            //
            // Convert L-factor
            //
            apserv.ivectorgrowto(ref buf.sparsel.idx, buf.sparsel.ridx[n - tmpndense] + n * tmpndense, _params);
            apserv.rvectorgrowto(ref buf.sparsel.vals, buf.sparsel.ridx[n - tmpndense] + n * tmpndense, _params);
            for (i = 0; i <= tmpndense - 1; i++)
            {
                sluv2list1appendsequencetomatrix(buf.bleft, i + (n - tmpndense), false, 0.0, n, buf.sparsel, i + (n - tmpndense), _params);
                offs = buf.sparsel.ridx[i + (n - tmpndense) + 1];
                for (j = 0; j <= i - 1; j++)
                {
                    v = buf.dbuf[i, j];
                    if (v != 0)
                    {
                        buf.sparsel.idx[offs] = j + (n - tmpndense);
                        buf.sparsel.vals[offs] = v;
                        offs = offs + 1;
                    }
                }
                buf.sparsel.ridx[i + (n - tmpndense) + 1] = offs;
            }
        }

        //
        // Allocate output
        //
        apserv.ivectorsetlengthatleast(ref buf.tmpi, n, _params);
        for (i = 0; i <= n - 1; i++)
        {
            buf.tmpi[i] = buf.sparsel.ridx[i + 1] - buf.sparsel.ridx[i];
        }
        for (i = 0; i <= n - 1; i++)
        {
            i0 = buf.sparseut.ridx[i];
            i1 = buf.sparseut.ridx[i + 1] - 1;
            for (j = i0; j <= i1; j++)
            {
                k = buf.sparseut.idx[j];
                buf.tmpi[k] = buf.tmpi[k] + 1;
            }
        }
        a.matrixtype = 1;
        a.ninitialized = buf.sparsel.ridx[n] + buf.sparseut.ridx[n];
        a.m = n;
        a.n = n;
        apserv.ivectorsetlengthatleast(ref a.ridx, n + 1, _params);
        apserv.ivectorsetlengthatleast(ref a.idx, a.ninitialized, _params);
        apserv.rvectorsetlengthatleast(ref a.vals, a.ninitialized, _params);
        a.ridx[0] = 0;
        for (i = 0; i <= n - 1; i++)
        {
            a.ridx[i + 1] = a.ridx[i] + buf.tmpi[i];
        }
        for (i = 0; i <= n - 1; i++)
        {
            i0 = buf.sparsel.ridx[i];
            i1 = buf.sparsel.ridx[i + 1] - 1;
            jp = a.ridx[i];
            for (j = i0; j <= i1; j++)
            {
                a.idx[jp + (j - i0)] = buf.sparsel.idx[j];
                a.vals[jp + (j - i0)] = buf.sparsel.vals[j];
            }
            buf.tmpi[i] = buf.sparsel.ridx[i + 1] - buf.sparsel.ridx[i];
        }
        apserv.ivectorsetlengthatleast(ref a.didx, n, _params);
        apserv.ivectorsetlengthatleast(ref a.uidx, n, _params);
        for (i = 0; i <= n - 1; i++)
        {
            a.didx[i] = a.ridx[i] + buf.tmpi[i];
            a.uidx[i] = a.didx[i] + 1;
            buf.tmpi[i] = a.didx[i];
        }
        for (i = 0; i <= n - 1; i++)
        {
            i0 = buf.sparseut.ridx[i];
            i1 = buf.sparseut.ridx[i + 1] - 1;
            for (j = i0; j <= i1; j++)
            {
                k = buf.sparseut.idx[j];
                offs = buf.tmpi[k];
                a.idx[offs] = i;
                a.vals[offs] = buf.sparseut.vals[j];
                buf.tmpi[k] = offs + 1;
            }
        }
        return result;
    }


    /*************************************************************************
    This function initialized rectangular submatrix structure.

    After initialization this structure stores  matrix[N,0],  which contains N
    rows (sequences), stored as single-linked lists.

      -- ALGLIB routine --
         15.01.2019
         Bochkanov Sergey
    *************************************************************************/
    private static void sluv2list1init(int n,
        sluv2list1matrix a,
        xparams _params)
    {
        int i = 0;

        ap.assert(n >= 1, "SLUV2List1Init: N<1");
        a.nfixed = n;
        a.ndynamic = 0;
        a.nallocated = n;
        a.nused = 0;
        apserv.ivectorgrowto(ref a.idxfirst, n, _params);
        apserv.ivectorgrowto(ref a.strgidx, 2 * a.nallocated, _params);
        apserv.rvectorgrowto(ref a.strgval, a.nallocated, _params);
        for (i = 0; i <= n - 1; i++)
        {
            a.idxfirst[i] = -1;
        }
    }


    /*************************************************************************
    This function swaps sequences #I and #J stored by the structure

      -- ALGLIB routine --
         15.01.2019
         Bochkanov Sergey
    *************************************************************************/
    private static void sluv2list1swap(sluv2list1matrix a,
        int i,
        int j,
        xparams _params)
    {
        int k = 0;

        k = a.idxfirst[i];
        a.idxfirst[i] = a.idxfirst[j];
        a.idxfirst[j] = k;
    }


    /*************************************************************************
    This function drops sequence #I from the structure

      -- ALGLIB routine --
         15.01.2019
         Bochkanov Sergey
    *************************************************************************/
    private static void sluv2list1dropsequence(sluv2list1matrix a,
        int i,
        xparams _params)
    {
        a.idxfirst[i] = -1;
    }


    /*************************************************************************
    This function appends sequence from the structure to the sparse matrix.

    It is assumed that S is a lower triangular  matrix,  and A stores strictly
    lower triangular elements (no diagonal ones!). You can explicitly  control
    whether you want to add diagonal elements or not.

    Output matrix is assumed to be stored in CRS format and  to  be  partially
    initialized (up to, but not including, Dst-th row). DIdx and UIdx are  NOT
    updated by this function as well as NInitialized.

    INPUT PARAMETERS:
        A           -   rectangular matrix structure
        Src         -   sequence (row or column) index in the structure
        HasDiagonal -   whether we want to add diagonal element
        D           -   diagonal element, if HasDiagonal=True
        NZMAX       -   maximum estimated number of non-zeros in the row,
                        this function will preallocate storage in the output
                        matrix.
        S           -   destination matrix in CRS format, partially initialized
        Dst         -   destination row index


      -- ALGLIB routine --
         15.01.2019
         Bochkanov Sergey
    *************************************************************************/
    private static void sluv2list1appendsequencetomatrix(sluv2list1matrix a,
        int src,
        bool hasdiagonal,
        double d,
        int nzmax,
        sparse.sparsematrix s,
        int dst,
        xparams _params)
    {
        int i = 0;
        int i0 = 0;
        int i1 = 0;
        int jp = 0;
        int nnz = 0;

        i0 = s.ridx[dst];
        apserv.ivectorgrowto(ref s.idx, i0 + nzmax, _params);
        apserv.rvectorgrowto(ref s.vals, i0 + nzmax, _params);
        if (hasdiagonal)
        {
            i1 = i0 + nzmax - 1;
            s.idx[i1] = dst;
            s.vals[i1] = d;
            nnz = 1;
        }
        else
        {
            i1 = i0 + nzmax;
            nnz = 0;
        }
        jp = a.idxfirst[src];
        while (jp >= 0)
        {
            i1 = i1 - 1;
            s.idx[i1] = a.strgidx[2 * jp + 1];
            s.vals[i1] = a.strgval[jp];
            nnz = nnz + 1;
            jp = a.strgidx[2 * jp + 0];
        }
        for (i = 0; i <= nnz - 1; i++)
        {
            s.idx[i0 + i] = s.idx[i1 + i];
            s.vals[i0 + i] = s.vals[i1 + i];
        }
        s.ridx[dst + 1] = s.ridx[dst] + nnz;
    }


    /*************************************************************************
    This function appends sparse column to the  matrix,  increasing  its  size
    from [N,K] to [N,K+1]

      -- ALGLIB routine --
         15.01.2019
         Bochkanov Sergey
    *************************************************************************/
    private static void sluv2list1pushsparsevector(sluv2list1matrix a,
        int[] si,
        double[] sv,
        int nz,
        xparams _params)
    {
        int idx = 0;
        int i = 0;
        int k = 0;
        int nused = 0;
        double v = 0;


        //
        // Fetch matrix size, increase
        //
        k = a.ndynamic;
        ap.assert(k < a.nfixed);
        a.ndynamic = k + 1;

        //
        // Allocate new storage if needed
        //
        nused = a.nused;
        a.nallocated = Math.Max(a.nallocated, nused + nz);
        apserv.ivectorgrowto(ref a.strgidx, 2 * a.nallocated, _params);
        apserv.rvectorgrowto(ref a.strgval, a.nallocated, _params);

        //
        // Append to list
        //
        for (idx = 0; idx <= nz - 1; idx++)
        {
            i = si[idx];
            v = sv[idx];
            a.strgidx[2 * nused + 0] = a.idxfirst[i];
            a.strgidx[2 * nused + 1] = k;
            a.strgval[nused] = v;
            a.idxfirst[i] = nused;
            nused = nused + 1;
        }
        a.nused = nused;
    }


    /*************************************************************************
    This function initializes dense trail, by default it is matrix[N,0]

      -- ALGLIB routine --
         15.01.2019
         Bochkanov Sergey
    *************************************************************************/
    private static void densetrailinit(sluv2densetrail d,
        int n,
        xparams _params)
    {
        int excessivesize = 0;


        //
        // Note: excessive rows are allocated to accomodate for situation when
        //       this buffer is used to solve successive problems with increasing
        //       sizes.
        //
        excessivesize = Math.Max((int)Math.Round(1.333 * n), n);
        d.n = n;
        d.ndense = 0;
        apserv.ivectorsetlengthatleast(ref d.did, n, _params);
        if (ap.rows(d.d) <= excessivesize)
        {
            apserv.rmatrixsetlengthatleast(ref d.d, n, 1, _params);
        }
        else
        {
            d.d = new double[excessivesize, 1];
        }
    }


    /*************************************************************************
    This function appends column with id=ID to the dense trail (column IDs are
    integer numbers in [0,N) which can be used to track column permutations).

      -- ALGLIB routine --
         15.01.2019
         Bochkanov Sergey
    *************************************************************************/
    private static void densetrailappendcolumn(sluv2densetrail d,
        double[] x,
        int id,
        xparams _params)
    {
        int n = 0;
        int i = 0;
        int targetidx = 0;

        n = d.n;

        //
        // Reallocate storage
        //
        apserv.rmatrixgrowcolsto(ref d.d, d.ndense + 1, n, _params);

        //
        // Copy to dense storage:
        // * BUpper
        // * BTrail
        // Remove from sparse storage
        //
        targetidx = d.ndense;
        for (i = 0; i <= n - 1; i++)
        {
            d.d[i, targetidx] = x[i];
        }
        d.did[targetidx] = id;
        d.ndense = targetidx + 1;
    }


    /*************************************************************************
    This function initializes sparse trail from the sparse matrix. By default,
    sparse trail spans columns and rows in [0,N)  range.  Subsequent  pivoting
    out of rows/columns changes its range to [K,N), [K+1,N) and so on.

      -- ALGLIB routine --
         15.01.2019
         Bochkanov Sergey
    *************************************************************************/
    private static void sparsetrailinit(sparse.sparsematrix s,
        sluv2sparsetrail a,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int n = 0;
        int j0 = 0;
        int j1 = 0;
        int jj = 0;
        int p = 0;
        int slsused = 0;

        ap.assert(s.m == s.n, "SparseTrailInit: M<>N");
        ap.assert(s.matrixtype == 1, "SparseTrailInit: non-CRS input");
        n = s.n;
        a.n = s.n;
        a.k = 0;
        apserv.ivectorsetlengthatleast(ref a.nzc, n, _params);
        apserv.ivectorsetlengthatleast(ref a.colid, n, _params);
        apserv.rvectorsetlengthatleast(ref a.tmp0, n, _params);
        for (i = 0; i <= n - 1; i++)
        {
            a.colid[i] = i;
        }
        apserv.bvectorsetlengthatleast(ref a.isdensified, n, _params);
        for (i = 0; i <= n - 1; i++)
        {
            a.isdensified[i] = false;
        }

        //
        // Working set of columns
        //
        a.maxwrkcnt = apserv.iboundval((int)Math.Round(1 + (double)n / (double)3), 1, Math.Min(n, 50), _params);
        a.wrkcnt = 0;
        apserv.ivectorsetlengthatleast(ref a.wrkset, a.maxwrkcnt, _params);

        //
        // Sparse linked storage (SLS). Store CRS matrix to SLS format,
        // row by row, starting from the last one.
        //
        apserv.ivectorsetlengthatleast(ref a.slscolptr, n, _params);
        apserv.ivectorsetlengthatleast(ref a.slsrowptr, n, _params);
        apserv.ivectorsetlengthatleast(ref a.slsidx, s.ridx[n] * slswidth, _params);
        apserv.rvectorsetlengthatleast(ref a.slsval, s.ridx[n], _params);
        for (i = 0; i <= n - 1; i++)
        {
            a.nzc[i] = 0;
        }
        for (i = 0; i <= n - 1; i++)
        {
            a.slscolptr[i] = -1;
            a.slsrowptr[i] = -1;
        }
        slsused = 0;
        for (i = n - 1; i >= 0; i--)
        {
            j0 = s.ridx[i];
            j1 = s.ridx[i + 1] - 1;
            for (jj = j1; jj >= j0; jj--)
            {
                j = s.idx[jj];

                //
                // Update non-zero counts for columns
                //
                a.nzc[j] = a.nzc[j] + 1;

                //
                // Insert into column list
                //
                p = a.slscolptr[j];
                if (p >= 0)
                {
                    a.slsidx[p * slswidth + 0] = slsused;
                }
                a.slsidx[slsused * slswidth + 0] = -1;
                a.slsidx[slsused * slswidth + 1] = p;
                a.slscolptr[j] = slsused;

                //
                // Insert into row list
                //
                p = a.slsrowptr[i];
                if (p >= 0)
                {
                    a.slsidx[p * slswidth + 2] = slsused;
                }
                a.slsidx[slsused * slswidth + 2] = -1;
                a.slsidx[slsused * slswidth + 3] = p;
                a.slsrowptr[i] = slsused;

                //
                // Store index and value
                //
                a.slsidx[slsused * slswidth + 4] = i;
                a.slsidx[slsused * slswidth + 5] = j;
                a.slsval[slsused] = s.vals[jj];
                slsused = slsused + 1;
            }
        }
        a.slsused = slsused;
    }


    /*************************************************************************
    This function searches for a appropriate pivot column/row.

    If there exists non-densified column, it returns indexes of  pivot  column
    and row, with most sparse column selected for column pivoting, and largest
    element selected for row pivoting. Function result is True.

    PivotType=1 means that no column pivoting is performed
    PivotType=2 means that both column and row pivoting are supported

    If all columns were densified, False is returned.

      -- ALGLIB routine --
         15.01.2019
         Bochkanov Sergey
    *************************************************************************/
    private static bool sparsetrailfindpivot(sluv2sparsetrail a,
        int pivottype,
        ref int ipiv,
        ref int jpiv,
        xparams _params)
    {
        bool result = new bool();
        int n = 0;
        int k = 0;
        int j = 0;
        int jp = 0;
        int entry = 0;
        int nz = 0;
        int maxwrknz = 0;
        int nnzbest = 0;
        double s = 0;
        double bbest = 0;
        int wrk0 = 0;
        int wrk1 = 0;

        ipiv = 0;
        jpiv = 0;

        n = a.n;
        k = a.k;
        nnzbest = n + 1;
        jpiv = -1;
        ipiv = -1;
        result = true;

        //
        // Select pivot column
        //
        if (pivottype == 1)
        {

            //
            // No column pivoting
            //
            ap.assert(!a.isdensified[k], "SparseTrailFindPivot: integrity check failed");
            jpiv = k;
        }
        else
        {

            //
            // Find pivot column
            //
            while (true)
            {

                //
                // Scan working set (if non-empty) for good columns
                //
                maxwrknz = a.maxwrknz;
                for (j = 0; j <= a.wrkcnt - 1; j++)
                {
                    jp = a.wrkset[j];
                    if (jp < k)
                    {
                        continue;
                    }
                    if (a.isdensified[jp])
                    {
                        continue;
                    }
                    nz = a.nzc[jp];
                    if (nz > maxwrknz)
                    {
                        continue;
                    }
                    if (jpiv < 0 || nz < nnzbest)
                    {
                        nnzbest = nz;
                        jpiv = jp;
                    }
                }
                if (jpiv >= 0)
                {
                    break;
                }

                //
                // Well, nothing found. Recompute working set:
                // * determine most sparse unprocessed yet column
                // * gather all columns with density in [Wrk0,Wrk1) range,
                //   increase range, repeat, until working set is full
                //
                a.wrkcnt = 0;
                a.maxwrknz = 0;
                wrk0 = n + 1;
                for (jp = k; jp <= n - 1; jp++)
                {
                    if (!a.isdensified[jp] && a.nzc[jp] < wrk0)
                    {
                        wrk0 = a.nzc[jp];
                    }
                }
                if (wrk0 > n)
                {

                    //
                    // Only densified columns are present, exit.
                    //
                    result = false;
                    return result;
                }
                wrk1 = wrk0 + 1;
                while (a.wrkcnt < a.maxwrkcnt && wrk0 <= n)
                {

                    //
                    // Find columns with non-zero count in [Wrk0,Wrk1) range
                    //
                    for (jp = k; jp <= n - 1; jp++)
                    {
                        if (a.wrkcnt == a.maxwrkcnt)
                        {
                            break;
                        }
                        if (a.isdensified[jp])
                        {
                            continue;
                        }
                        if (a.nzc[jp] >= wrk0 && a.nzc[jp] < wrk1)
                        {
                            a.wrkset[a.wrkcnt] = jp;
                            a.wrkcnt = a.wrkcnt + 1;
                            a.maxwrknz = Math.Max(a.maxwrknz, a.nzc[jp]);
                        }
                    }

                    //
                    // Advance scan range
                    //
                    jp = (int)Math.Round(1.41 * (wrk1 - wrk0)) + 1;
                    wrk0 = wrk1;
                    wrk1 = wrk0 + jp;
                }
            }
        }

        //
        // Select pivot row
        //
        bbest = 0;
        entry = a.slscolptr[jpiv];
        while (entry >= 0)
        {
            s = Math.Abs(a.slsval[entry]);
            if (ipiv < 0 || (double)(s) > (double)(bbest))
            {
                bbest = s;
                ipiv = a.slsidx[entry * slswidth + 4];
            }
            entry = a.slsidx[entry * slswidth + 1];
        }
        if (ipiv < 0)
        {
            ipiv = k;
        }
        return result;
    }


    /*************************************************************************
    This function pivots out specified row and column.

    Sparse trail range changes from [K,N) to [K+1,N).

    V0I, V0R, V1I, V1R must be preallocated arrays[N].

    Following data are returned:
    * UU - diagonal element (pivoted out), can be zero
    * V0I, V0R, NZ0 - sparse column pivoted out to the left (after permutation
      is applied to its elements) and divided by UU.
      V0I is array[NZ0] which stores row indexes in [K+1,N) range, V0R  stores
      values.
    * V1I, V1R, NZ1 - sparse row pivoted out to the top.

      -- ALGLIB routine --
         15.01.2019
         Bochkanov Sergey
    *************************************************************************/
    private static void sparsetrailpivotout(sluv2sparsetrail a,
        int ipiv,
        int jpiv,
        ref double uu,
        int[] v0i,
        double[] v0r,
        ref int nz0,
        int[] v1i,
        double[] v1r,
        ref int nz1,
        xparams _params)
    {
        int n = 0;
        int k = 0;
        int i = 0;
        int j = 0;
        int entry = 0;
        double v = 0;
        double s = 0;
        bool vb = new bool();
        int pos0k = 0;
        int pos0piv = 0;
        int pprev = 0;
        int pnext = 0;
        int pnextnext = 0;

        uu = 0;
        nz0 = 0;
        nz1 = 0;

        n = a.n;
        k = a.k;
        ap.assert(k < n, "SparseTrailPivotOut: integrity check failed");

        //
        // Pivot out column JPiv from the sparse linked storage:
        // * remove column JPiv from the matrix
        // * update column K:
        //   * change element indexes after it is permuted to JPiv
        //   * resort rows affected by move K->JPiv
        //
        // NOTE: this code leaves V0I/V0R/NZ0 in the unfinalized state,
        //       i.e. these arrays do not account for pivoting performed
        //       on rows. They will be post-processed later.
        //
        nz0 = 0;
        pos0k = -1;
        pos0piv = -1;
        entry = a.slscolptr[jpiv];
        while (entry >= 0)
        {

            //
            // Offload element
            //
            i = a.slsidx[entry * slswidth + 4];
            v0i[nz0] = i;
            v0r[nz0] = a.slsval[entry];
            if (i == k)
            {
                pos0k = nz0;
            }
            if (i == ipiv)
            {
                pos0piv = nz0;
            }
            nz0 = nz0 + 1;

            //
            // Remove element from the row list
            //
            pprev = a.slsidx[entry * slswidth + 2];
            pnext = a.slsidx[entry * slswidth + 3];
            if (pprev >= 0)
            {
                a.slsidx[pprev * slswidth + 3] = pnext;
            }
            else
            {
                a.slsrowptr[i] = pnext;
            }
            if (pnext >= 0)
            {
                a.slsidx[pnext * slswidth + 2] = pprev;
            }

            //
            // Select next entry
            //
            entry = a.slsidx[entry * slswidth + 1];
        }
        entry = a.slscolptr[k];
        a.slscolptr[jpiv] = entry;
        while (entry >= 0)
        {

            //
            // Change column index
            //
            a.slsidx[entry * slswidth + 5] = jpiv;

            //
            // Next entry
            //
            entry = a.slsidx[entry * slswidth + 1];
        }

        //
        // Post-process V0, account for pivoting.
        // Compute diagonal element UU.
        //
        uu = 0;
        if (pos0k >= 0 || pos0piv >= 0)
        {

            //
            // Apply permutation to rows of pivoted out column, specific
            // implementation depends on the sparsity at locations #Pos0K
            // and #Pos0Piv of the V0 array.
            //
            if (pos0k >= 0 && pos0piv >= 0)
            {

                //
                // Obtain diagonal element
                //
                uu = v0r[pos0piv];
                if (uu != 0)
                {
                    s = 1 / uu;
                }
                else
                {
                    s = 1;
                }

                //
                // Move pivoted out element, shift array by one in order
                // to remove heading diagonal element (not needed here
                // anymore).
                //
                v0r[pos0piv] = v0r[pos0k];
                for (i = 0; i <= nz0 - 2; i++)
                {
                    v0i[i] = v0i[i + 1];
                    v0r[i] = v0r[i + 1] * s;
                }
                nz0 = nz0 - 1;
            }
            if (pos0k >= 0 && pos0piv < 0)
            {

                //
                // Diagonal element is zero
                //
                uu = 0;

                //
                // Pivot out element, reorder array
                //
                v0i[pos0k] = ipiv;
                for (i = pos0k; i <= nz0 - 2; i++)
                {
                    if (v0i[i] < v0i[i + 1])
                    {
                        break;
                    }
                    j = v0i[i];
                    v0i[i] = v0i[i + 1];
                    v0i[i + 1] = j;
                    v = v0r[i];
                    v0r[i] = v0r[i + 1];
                    v0r[i + 1] = v;
                }
            }
            if (pos0k < 0 && pos0piv >= 0)
            {

                //
                // Get diagonal element
                //
                uu = v0r[pos0piv];
                if (uu != 0)
                {
                    s = 1 / uu;
                }
                else
                {
                    s = 1;
                }

                //
                // Shift array past the pivoted in element by one
                // in order to remove pivot
                //
                for (i = 0; i <= pos0piv - 1; i++)
                {
                    v0r[i] = v0r[i] * s;
                }
                for (i = pos0piv; i <= nz0 - 2; i++)
                {
                    v0i[i] = v0i[i + 1];
                    v0r[i] = v0r[i + 1] * s;
                }
                nz0 = nz0 - 1;
            }
        }

        //
        // Pivot out row IPiv from the sparse linked storage:
        // * remove row IPiv from the matrix
        // * reindex elements of row K after it is permuted to IPiv
        // * apply permutation to the cols of the pivoted out row,
        //   resort columns
        //
        nz1 = 0;
        entry = a.slsrowptr[ipiv];
        while (entry >= 0)
        {

            //
            // Offload element
            //
            j = a.slsidx[entry * slswidth + 5];
            v1i[nz1] = j;
            v1r[nz1] = a.slsval[entry];
            nz1 = nz1 + 1;

            //
            // Remove element from the column list
            //
            pprev = a.slsidx[entry * slswidth + 0];
            pnext = a.slsidx[entry * slswidth + 1];
            if (pprev >= 0)
            {
                a.slsidx[pprev * slswidth + 1] = pnext;
            }
            else
            {
                a.slscolptr[j] = pnext;
            }
            if (pnext >= 0)
            {
                a.slsidx[pnext * slswidth + 0] = pprev;
            }

            //
            // Select next entry
            //
            entry = a.slsidx[entry * slswidth + 3];
        }
        a.slsrowptr[ipiv] = a.slsrowptr[k];
        entry = a.slsrowptr[ipiv];
        while (entry >= 0)
        {

            //
            // Change row index
            //
            a.slsidx[entry * slswidth + 4] = ipiv;

            //
            // Resort column affected by row pivoting
            //
            j = a.slsidx[entry * slswidth + 5];
            pprev = a.slsidx[entry * slswidth + 0];
            pnext = a.slsidx[entry * slswidth + 1];
            while (pnext >= 0 && a.slsidx[pnext * slswidth + 4] < ipiv)
            {
                pnextnext = a.slsidx[pnext * slswidth + 1];

                //
                // prev->next
                //
                if (pprev >= 0)
                {
                    a.slsidx[pprev * slswidth + 1] = pnext;
                }
                else
                {
                    a.slscolptr[j] = pnext;
                }

                //
                // entry->prev, entry->next
                //
                a.slsidx[entry * slswidth + 0] = pnext;
                a.slsidx[entry * slswidth + 1] = pnextnext;

                //
                // next->prev, next->next
                //
                a.slsidx[pnext * slswidth + 0] = pprev;
                a.slsidx[pnext * slswidth + 1] = entry;

                //
                // nextnext->prev
                //
                if (pnextnext >= 0)
                {
                    a.slsidx[pnextnext * slswidth + 0] = entry;
                }

                //
                // PPrev, Item, PNext
                //
                pprev = pnext;
                pnext = pnextnext;
            }

            //
            // Next entry
            //
            entry = a.slsidx[entry * slswidth + 3];
        }

        //
        // Reorder other structures
        //
        i = a.nzc[k];
        a.nzc[k] = a.nzc[jpiv];
        a.nzc[jpiv] = i;
        i = a.colid[k];
        a.colid[k] = a.colid[jpiv];
        a.colid[jpiv] = i;
        vb = a.isdensified[k];
        a.isdensified[k] = a.isdensified[jpiv];
        a.isdensified[jpiv] = vb;

        //
        // Handle removal of col/row #K
        //
        for (i = 0; i <= nz1 - 1; i++)
        {
            j = v1i[i];
            a.nzc[j] = a.nzc[j] - 1;
        }
        a.k = a.k + 1;
    }


    /*************************************************************************
    This function densifies I1-th column of the sparse trail.

    PARAMETERS:
        A           -   sparse trail
        I1          -   column index
        BUpper      -   upper rectangular submatrix, updated during densification
                        of the columns (densified columns are removed)
        DTrail      -   dense trail, receives densified columns from sparse
                        trail and BUpper

      -- ALGLIB routine --
         15.01.2019
         Bochkanov Sergey
    *************************************************************************/
    private static void sparsetraildensify(sluv2sparsetrail a,
        int i1,
        sluv2list1matrix bupper,
        sluv2densetrail dtrail,
        xparams _params)
    {
        int n = 0;
        int k = 0;
        int i = 0;
        int jp = 0;
        int entry = 0;
        int pprev = 0;
        int pnext = 0;

        n = a.n;
        k = a.k;
        ap.assert(k < n, "SparseTrailDensify: integrity check failed");
        ap.assert(k <= i1, "SparseTrailDensify: integrity check failed");
        ap.assert(!a.isdensified[i1], "SparseTrailDensify: integrity check failed");

        //
        // Offload items [0,K) of densified column from BUpper
        //
        for (i = 0; i <= n - 1; i++)
        {
            a.tmp0[i] = 0;
        }
        jp = bupper.idxfirst[i1];
        while (jp >= 0)
        {
            a.tmp0[bupper.strgidx[2 * jp + 1]] = bupper.strgval[jp];
            jp = bupper.strgidx[2 * jp + 0];
        }
        sluv2list1dropsequence(bupper, i1, _params);

        //
        // Offload items [K,N) of densified column from BLeft
        //
        entry = a.slscolptr[i1];
        while (entry >= 0)
        {

            //
            // Offload element
            //
            i = a.slsidx[entry * slswidth + 4];
            a.tmp0[i] = a.slsval[entry];

            //
            // Remove element from the row list
            //
            pprev = a.slsidx[entry * slswidth + 2];
            pnext = a.slsidx[entry * slswidth + 3];
            if (pprev >= 0)
            {
                a.slsidx[pprev * slswidth + 3] = pnext;
            }
            else
            {
                a.slsrowptr[i] = pnext;
            }
            if (pnext >= 0)
            {
                a.slsidx[pnext * slswidth + 2] = pprev;
            }

            //
            // Select next entry
            //
            entry = a.slsidx[entry * slswidth + 1];
        }

        //
        // Densify
        //
        a.nzc[i1] = 0;
        a.isdensified[i1] = true;
        a.slscolptr[i1] = -1;
        densetrailappendcolumn(dtrail, a.tmp0, a.colid[i1], _params);
    }


    /*************************************************************************
    This function appends rank-1 update to the sparse trail.  Dense  trail  is
    not  updated  here,  but  we  may  move some columns to dense trail during
    update (i.e. densify them). Thus, you have to update  dense  trail  BEFORE
    you start updating sparse one (otherwise, recently densified columns  will
    be updated twice).

    PARAMETERS:
        A           -   sparse trail
        V0I, V0R    -   update column returned by SparseTrailPivotOut (MUST be
                        array[N] independently of the NZ0).
        NZ0         -   non-zero count for update column
        V1I, V1R    -   update row returned by SparseTrailPivotOut
        NZ1         -   non-zero count for update row
        BUpper      -   upper rectangular submatrix, updated during densification
                        of the columns (densified columns are removed)
        DTrail      -   dense trail, receives densified columns from sparse
                        trail and BUpper
        DensificationSupported- if False, no densification is performed

      -- ALGLIB routine --
         15.01.2019
         Bochkanov Sergey
    *************************************************************************/
    private static void sparsetrailupdate(sluv2sparsetrail a,
        int[] v0i,
        double[] v0r,
        int nz0,
        int[] v1i,
        double[] v1r,
        int nz1,
        sluv2list1matrix bupper,
        sluv2densetrail dtrail,
        bool densificationsupported,
        xparams _params)
    {
        int n = 0;
        int k = 0;
        int i = 0;
        int j = 0;
        int i0 = 0;
        int i1 = 0;
        double v1 = 0;
        int densifyabove = 0;
        int nnz = 0;
        int entry = 0;
        int newentry = 0;
        int pprev = 0;
        int pnext = 0;
        int p = 0;
        int nexti = 0;
        int newoffs = 0;

        n = a.n;
        k = a.k;
        ap.assert(k < n, "SparseTrailPivotOut: integrity check failed");
        densifyabove = (int)Math.Round(densebnd * (n - k)) + 1;
        ap.assert(ap.len(v0i) >= nz0 + 1, "SparseTrailUpdate: integrity check failed");
        ap.assert(ap.len(v0r) >= nz0 + 1, "SparseTrailUpdate: integrity check failed");
        v0i[nz0] = -1;
        v0r[nz0] = 0;

        //
        // Update sparse representation
        //
        apserv.ivectorgrowto(ref a.slsidx, (a.slsused + nz0 * nz1) * slswidth, _params);
        apserv.rvectorgrowto(ref a.slsval, a.slsused + nz0 * nz1, _params);
        for (j = 0; j <= nz1 - 1; j++)
        {
            if (nz0 == 0)
            {
                continue;
            }
            i1 = v1i[j];
            v1 = v1r[j];

            //
            // Update column #I1
            //
            nnz = a.nzc[i1];
            i = 0;
            i0 = v0i[i];
            entry = a.slscolptr[i1];
            pprev = -1;
            while (i < nz0)
            {

                //
                // Handle possible fill-in happening BEFORE already existing
                // entry of the column list (or simply fill-in, if no entry
                // is present).
                //
                pnext = entry;
                if (entry >= 0)
                {
                    nexti = a.slsidx[entry * slswidth + 4];
                }
                else
                {
                    nexti = n + 1;
                }
                while (i < nz0)
                {
                    if (i0 >= nexti)
                    {
                        break;
                    }

                    //
                    // Allocate new entry, store column/row/value
                    //
                    newentry = a.slsused;
                    a.slsused = newentry + 1;
                    nnz = nnz + 1;
                    newoffs = newentry * slswidth;
                    a.slsidx[newoffs + 4] = i0;
                    a.slsidx[newoffs + 5] = i1;
                    a.slsval[newentry] = -(v1 * v0r[i]);

                    //
                    // Insert entry into column list
                    //
                    a.slsidx[newoffs + 0] = pprev;
                    a.slsidx[newoffs + 1] = pnext;
                    if (pprev >= 0)
                    {
                        a.slsidx[pprev * slswidth + 1] = newentry;
                    }
                    else
                    {
                        a.slscolptr[i1] = newentry;
                    }
                    if (entry >= 0)
                    {
                        a.slsidx[entry * slswidth + 0] = newentry;
                    }

                    //
                    // Insert entry into row list
                    //
                    p = a.slsrowptr[i0];
                    a.slsidx[newoffs + 2] = -1;
                    a.slsidx[newoffs + 3] = p;
                    if (p >= 0)
                    {
                        a.slsidx[p * slswidth + 2] = newentry;
                    }
                    a.slsrowptr[i0] = newentry;

                    //
                    // Advance pointers
                    //
                    pprev = newentry;
                    i = i + 1;
                    i0 = v0i[i];
                }
                if (i >= nz0)
                {
                    break;
                }

                //
                // Update already existing entry of the column list, if needed
                //
                if (entry >= 0)
                {
                    if (i0 == nexti)
                    {
                        a.slsval[entry] = a.slsval[entry] - v1 * v0r[i];
                        i = i + 1;
                        i0 = v0i[i];
                    }
                    pprev = entry;
                }

                //
                // Advance to the next pre-existing entry (if present)
                //
                if (entry >= 0)
                {
                    entry = a.slsidx[entry * slswidth + 1];
                }
            }
            a.nzc[i1] = nnz;

            //
            // Densify column if needed
            //
            if ((densificationsupported && nnz > densifyabove) && !a.isdensified[i1])
            {
                sparsetraildensify(a, i1, bupper, dtrail, _params);
            }
        }
    }


}
