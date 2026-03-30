#pragma warning disable CS8618
#pragma warning disable CS1591

using System;

namespace Simargl.Algorithms.Raw;

public class amdordering
{
    /*************************************************************************
    This structure is used to store K sets of N possible integers each.
    The structure needs at least O(N) temporary memory.
    *************************************************************************/
    public class amdknset : apobject
    {
        public int k;
        public int n;
        public int[] flagarray;
        public int[] vbegin;
        public int[] vallocated;
        public int[] vcnt;
        public int[] data;
        public int dataused;
        public int iterrow;
        public int iteridx;
        public amdknset()
        {
            init();
        }
        public override void init()
        {
            flagarray = new int[0];
            vbegin = new int[0];
            vallocated = new int[0];
            vcnt = new int[0];
            data = new int[0];
        }
        public override apobject make_copy()
        {
            amdknset _result = new amdknset();
            _result.k = k;
            _result.n = n;
            _result.flagarray = (int[])flagarray.Clone();
            _result.vbegin = (int[])vbegin.Clone();
            _result.vallocated = (int[])vallocated.Clone();
            _result.vcnt = (int[])vcnt.Clone();
            _result.data = (int[])data.Clone();
            _result.dataused = dataused;
            _result.iterrow = iterrow;
            _result.iteridx = iteridx;
            return _result;
        }
    };


    /*************************************************************************
    This structure is used to store vertex degrees, with  ability  to  quickly
    (in O(1) time) select one with smallest degree
    *************************************************************************/
    public class amdvertexset : apobject
    {
        public int n;
        public bool checkexactdegrees;
        public int smallestdegree;
        public int[] approxd;
        public int[] optionalexactd;
        public bool[] isvertex;
        public int[] vbegin;
        public int[] vprev;
        public int[] vnext;
        public amdvertexset()
        {
            init();
        }
        public override void init()
        {
            approxd = new int[0];
            optionalexactd = new int[0];
            isvertex = new bool[0];
            vbegin = new int[0];
            vprev = new int[0];
            vnext = new int[0];
        }
        public override apobject make_copy()
        {
            amdvertexset _result = new amdvertexset();
            _result.n = n;
            _result.checkexactdegrees = checkexactdegrees;
            _result.smallestdegree = smallestdegree;
            _result.approxd = (int[])approxd.Clone();
            _result.optionalexactd = (int[])optionalexactd.Clone();
            _result.isvertex = (bool[])isvertex.Clone();
            _result.vbegin = (int[])vbegin.Clone();
            _result.vprev = (int[])vprev.Clone();
            _result.vnext = (int[])vnext.Clone();
            return _result;
        }
    };


    /*************************************************************************
    This structure is used to store linked list NxN matrix.

    The fields are:
    * VBegin - array[2*N+1], stores first entries in each row (N values),  col
      (N values), list of free entries (1 value), 2*N+1 in total
    * Entries - stores EntriesInitialized elements, each occupying llmEntrySize
      elements of array. These entries are organized into linked row and column
      list, with each entry belonging to both row list and column list.
    *************************************************************************/
    public class amdllmatrix : apobject
    {
        public int n;
        public int[] vbegin;
        public int[] vcolcnt;
        public int[] entries;
        public int entriesinitialized;
        public amdllmatrix()
        {
            init();
        }
        public override void init()
        {
            vbegin = new int[0];
            vcolcnt = new int[0];
            entries = new int[0];
        }
        public override apobject make_copy()
        {
            amdllmatrix _result = new amdllmatrix();
            _result.n = n;
            _result.vbegin = (int[])vbegin.Clone();
            _result.vcolcnt = (int[])vcolcnt.Clone();
            _result.entries = (int[])entries.Clone();
            _result.entriesinitialized = entriesinitialized;
            return _result;
        }
    };


    /*************************************************************************
    This structure is used to store temporaries for AMD ordering
    *************************************************************************/
    public class amdbuffer : apobject
    {
        public int n;
        public bool extendeddebug;
        public bool checkexactdegrees;
        public bool[] iseliminated;
        public bool[] issupernode;
        public amdknset setsuper;
        public amdknset seta;
        public amdknset sete;
        public amdllmatrix mtxl;
        public amdvertexset vertexdegrees;
        public apstruct.niset setq;
        public int[] perm;
        public int[] invperm;
        public int[] columnswaps;
        public apstruct.niset setp;
        public apstruct.niset lp;
        public apstruct.niset setrp;
        public apstruct.niset ep;
        public apstruct.niset adji;
        public apstruct.niset adjj;
        public int[] ls;
        public int lscnt;
        public apstruct.niset setqsupercand;
        public apstruct.niset exactdegreetmp0;
        public amdknset hashbuckets;
        public apstruct.niset nonemptybuckets;
        public int[] sncandidates;
        public int[] tmp0;
        public int[] arrwe;
        public double[,] dbga;
        public amdbuffer()
        {
            init();
        }
        public override void init()
        {
            iseliminated = new bool[0];
            issupernode = new bool[0];
            setsuper = new amdknset();
            seta = new amdknset();
            sete = new amdknset();
            mtxl = new amdllmatrix();
            vertexdegrees = new amdvertexset();
            setq = new apstruct.niset();
            perm = new int[0];
            invperm = new int[0];
            columnswaps = new int[0];
            setp = new apstruct.niset();
            lp = new apstruct.niset();
            setrp = new apstruct.niset();
            ep = new apstruct.niset();
            adji = new apstruct.niset();
            adjj = new apstruct.niset();
            ls = new int[0];
            setqsupercand = new apstruct.niset();
            exactdegreetmp0 = new apstruct.niset();
            hashbuckets = new amdknset();
            nonemptybuckets = new apstruct.niset();
            sncandidates = new int[0];
            tmp0 = new int[0];
            arrwe = new int[0];
            dbga = new double[0, 0];
        }
        public override apobject make_copy()
        {
            amdbuffer _result = new amdbuffer();
            _result.n = n;
            _result.extendeddebug = extendeddebug;
            _result.checkexactdegrees = checkexactdegrees;
            _result.iseliminated = (bool[])iseliminated.Clone();
            _result.issupernode = (bool[])issupernode.Clone();
            _result.setsuper = (amdknset)setsuper.make_copy();
            _result.seta = (amdknset)seta.make_copy();
            _result.sete = (amdknset)sete.make_copy();
            _result.mtxl = (amdllmatrix)mtxl.make_copy();
            _result.vertexdegrees = (amdvertexset)vertexdegrees.make_copy();
            _result.setq = (apstruct.niset)setq.make_copy();
            _result.perm = (int[])perm.Clone();
            _result.invperm = (int[])invperm.Clone();
            _result.columnswaps = (int[])columnswaps.Clone();
            _result.setp = (apstruct.niset)setp.make_copy();
            _result.lp = (apstruct.niset)lp.make_copy();
            _result.setrp = (apstruct.niset)setrp.make_copy();
            _result.ep = (apstruct.niset)ep.make_copy();
            _result.adji = (apstruct.niset)adji.make_copy();
            _result.adjj = (apstruct.niset)adjj.make_copy();
            _result.ls = (int[])ls.Clone();
            _result.lscnt = lscnt;
            _result.setqsupercand = (apstruct.niset)setqsupercand.make_copy();
            _result.exactdegreetmp0 = (apstruct.niset)exactdegreetmp0.make_copy();
            _result.hashbuckets = (amdknset)hashbuckets.make_copy();
            _result.nonemptybuckets = (apstruct.niset)nonemptybuckets.make_copy();
            _result.sncandidates = (int[])sncandidates.Clone();
            _result.tmp0 = (int[])tmp0.Clone();
            _result.arrwe = (int[])arrwe.Clone();
            _result.dbga = (double[,])dbga.Clone();
            return _result;
        }
    };




    public const int knsheadersize = 2;
    public const int llmentrysize = 6;


    /*************************************************************************
    This function generates approximate minimum degree ordering

    INPUT PARAMETERS
        A           -   lower triangular sparse matrix  in  CRS  format.  Only
                        sparsity structure (as given by Idx[] field)  matters,
                        specific values of matrix elements are ignored.
        N           -   problem size
        Buf         -   reusable buffer object, does not need special initialization
        
    OUTPUT PARAMETERS
        Perm        -   array[N], maps original indexes I to permuted indexes
        InvPerm     -   array[N], maps permuted indexes I to original indexes
        
    NOTE: definite 'DEBUG.SLOW' trace tag will  activate  extra-slow  (roughly
          N^3 ops) integrity checks, in addition to cheap O(1) ones.

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    public static void generateamdpermutation(sparse.sparsematrix a,
        int n,
        ref int[] perm,
        ref int[] invperm,
        amdbuffer buf,
        xparams _params)
    {
        int r = 0;
        bool[] dummy = new bool[0];

        r = generateamdpermutationx(a, dummy, n, 0.0, ref perm, ref invperm, 0, buf, _params);
        ap.assert(r == n, "GenerateAMDPermutation: integrity check failed, the matrix is only partially processed");
    }


    /*************************************************************************
    This  function  generates  approximate  minimum  degree ordering,   either
    classic or improved with better support for dense rows:
    * the classic version processed entire matrix and returns N as result. The
      problem with classic version is that it may be slow  for  matrices  with
      dense or nearly dense rows
    * the improved version processes K most sparse rows, and moves  other  N-K
      ones to the end. The number of sparse rows  K  is  returned.  The  tail,
      which is now a (N-K)*(N-K) matrix, should be repeatedly processed by the
      same function until zero is returned.

    INPUT PARAMETERS
        A           -   lower triangular sparse matrix in CRS format
        Eligible    -   array[N], set of boolean flags that mark columns of  A
                        as eligible for ordering. Columns that are not eligible
                        are postponed (moved to the end) by the  improved  AMD
                        algorithm. This array is ignored  (not  referenced  at
                        all) when AMDType=0.
        N           -   problem size
        PromoteAbove-   columns with degrees higher than PromoteAbove*max(MEAN(Degree),1)
                        may be postponed. Ignored for AMDType<>1.
                        This parameter controls postponement of dense columns
                        (and algorithm ability to efficiently handle them):
                        * big PromoteAbove (N or more) effectively means that
                          no eligible columns are postponed. Better to combine
                          with your own heuristic to choose eligible columns,
                          otherwise algorithm will have hard time on problems
                          with dense columns in the eligible set.
                        * values between 2 and 10 are usually  a  good  choice
                          for manual control
                        * zero  value  means   that   appropriate   value   is
                          automatically chosen. Specific value may  change  in
                          future ALGLIB versions. Recommended.
        AMDType     -   ordering type:
                        * 0 for the classic AMD
                        * 1 for the improved AMD
        Buf         -   reusable buffer object, does not need special initialization
        
    OUTPUT PARAMETERS
        Perm        -   array[N], maps original indexes I to permuted indexes
        InvPerm     -   array[N], maps permuted indexes I to original indexes
        
    RESULT:
        number of successfully ordered rows/cols;
        for AMDType=0:  Result=N
        for AMDType=1:  0<=Result<=N. Result=0 is returned only when there are
                        no columns that are both sparse enough and eligible.
        
    NOTE: defining 'DEBUG.SLOW' trace tag will  activate  extra-slow  (roughly
          N^3 ops) integrity checks, in addition to cheap O(1) ones.

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    public static int generateamdpermutationx(sparse.sparsematrix a,
        bool[] eligible,
        int n,
        double promoteabove,
        ref int[] perm,
        ref int[] invperm,
        int amdtype,
        amdbuffer buf,
        xparams _params)
    {
        int result = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        int p = 0;
        int setprealloc = 0;
        int inithashbucketsize = 0;
        bool extendeddebug = new bool();
        int nodesize = 0;
        int cnt0 = 0;
        int cnt1 = 0;
        int tau = 0;
        double meand = 0;
        int d = 0;

        ap.assert(amdtype == 0 || amdtype == 1, "GenerateAMDPermutationX: unexpected ordering type");
        ap.assert(amdtype == 0 || (math.isfinite(promoteabove) && (double)(promoteabove) >= (double)(0)), "GenerateAMDPermutationX: unexpected PromoteAbove - infinite or negative");
        setprealloc = 3;
        inithashbucketsize = 16;
        extendeddebug = ap.istraceenabled("DEBUG.SLOW", _params) && n <= 100;
        result = n;
        buf.n = n;
        buf.checkexactdegrees = extendeddebug;
        buf.extendeddebug = extendeddebug;
        mtxinit(n, buf.mtxl, _params);
        knsinitfroma(a, n, buf.seta, _params);
        knsinit(n, n, setprealloc, buf.setsuper, _params);
        for (i = 0; i <= n - 1; i++)
        {
            knsaddnewelement(buf.setsuper, i, i, _params);
        }
        knsinit(n, n, setprealloc, buf.sete, _params);
        knsinit(n, n, inithashbucketsize, buf.hashbuckets, _params);
        apstruct.nisinitemptyslow(n, buf.nonemptybuckets, _params);
        apserv.ivectorsetlengthatleast(ref buf.perm, n, _params);
        apserv.ivectorsetlengthatleast(ref buf.invperm, n, _params);
        apserv.ivectorsetlengthatleast(ref buf.columnswaps, n, _params);
        for (i = 0; i <= n - 1; i++)
        {
            buf.perm[i] = i;
            buf.invperm[i] = i;
            buf.columnswaps[i] = i;
        }
        vtxinit(a, n, buf.checkexactdegrees, buf.vertexdegrees, _params);
        ablasf.bsetallocv(n, true, ref buf.issupernode, _params);
        ablasf.bsetallocv(n, false, ref buf.iseliminated, _params);
        ablasf.isetallocv(n, -1, ref buf.arrwe, _params);
        ablasf.iallocv(n, ref buf.ls, _params);
        apstruct.nisinitemptyslow(n, buf.setp, _params);
        apstruct.nisinitemptyslow(n, buf.lp, _params);
        apstruct.nisinitemptyslow(n, buf.setrp, _params);
        apstruct.nisinitemptyslow(n, buf.ep, _params);
        apstruct.nisinitemptyslow(n, buf.exactdegreetmp0, _params);
        apstruct.nisinitemptyslow(n, buf.adji, _params);
        apstruct.nisinitemptyslow(n, buf.adjj, _params);
        apstruct.nisinitemptyslow(n, buf.setq, _params);
        apstruct.nisinitemptyslow(n, buf.setqsupercand, _params);
        if (extendeddebug)
        {
            buf.dbga = new double[n, n];
            for (i = 0; i <= n - 1; i++)
            {
                for (j = 0; j <= n - 1; j++)
                {
                    if ((j <= i && sparse.sparseexists(a, i, j, _params)) || (j >= i && sparse.sparseexists(a, j, i, _params)))
                    {
                        buf.dbga[i, j] = 0.1 / n * (Math.Sin(i + 0.17) + Math.Cos(Math.Sqrt(j + 0.65)));
                    }
                    else
                    {
                        buf.dbga[i, j] = 0;
                    }
                }
            }
            for (i = 0; i <= n - 1; i++)
            {
                buf.dbga[i, i] = 1;
            }
        }
        tau = 0;
        if (amdtype == 1)
        {
            ap.assert(ap.len(eligible) >= n, "GenerateAMDPermutationX: length(Eligible)<N");
            meand = 0.0;
            for (i = 0; i <= n - 1; i++)
            {
                d = vtxgetapprox(buf.vertexdegrees, i, _params);
                meand = meand + d;
            }
            meand = meand / n;
            tau = (int)Math.Round(apserv.rcase2((double)(promoteabove) > (double)(0), promoteabove, 10, _params) * Math.Max(meand, 1));
            tau = Math.Max(tau, 1);
            for (i = 0; i <= n - 1; i++)
            {
                if (!eligible[i] || vtxgetapprox(buf.vertexdegrees, i, _params) > tau)
                {
                    apstruct.nisaddelement(buf.setqsupercand, i, _params);
                }
            }
            amdmovetoquasidense(buf, buf.setqsupercand, -1, _params);
        }
        k = 0;
        while (k < n - apstruct.niscount(buf.setq, _params))
        {
            amdselectpivotelement(buf, k, ref p, ref nodesize, _params);
            amdcomputelp(buf, p, _params);
            amdmasselimination(buf, p, k, tau, _params);
            amdmovetoquasidense(buf, buf.setqsupercand, p, _params);
            amddetectsupernodes(buf, _params);
            if (extendeddebug)
            {
                ap.assert(buf.checkexactdegrees, "AMD: extended debug needs exact degrees");
                for (i = k; i <= k + nodesize - 1; i++)
                {
                    if (buf.columnswaps[i] != i)
                    {
                        apserv.swaprows(buf.dbga, i, buf.columnswaps[i], n, _params);
                        apserv.swapcols(buf.dbga, i, buf.columnswaps[i], n, _params);
                    }
                }
                for (i = 0; i <= nodesize - 1; i++)
                {
                    ablas.rmatrixgemm(n - k - i, n - k - i, k + i, -1.0, buf.dbga, k + i, 0, 0, buf.dbga, 0, k + i, 0, 1.0, buf.dbga, k + i, k + i, _params);
                }
                cnt0 = apstruct.niscount(buf.lp, _params);
                cnt1 = 0;
                for (i = k + 1; i <= n - 1; i++)
                {
                    if ((double)(buf.dbga[i, k]) != (double)(0))
                    {
                        apserv.inc(ref cnt1, _params);
                    }
                }
                ap.assert(cnt0 + nodesize - 1 == cnt1, "AMD: integrity check 7344 failed");
                ap.assert(vtxgetapprox(buf.vertexdegrees, p, _params) >= vtxgetexact(buf.vertexdegrees, p, _params), "AMD: integrity check for ApproxD failed");
                ap.assert(vtxgetexact(buf.vertexdegrees, p, _params) == cnt0, "AMD: integrity check for ExactD failed");
            }
            ap.assert(vtxgetapprox(buf.vertexdegrees, p, _params) >= apstruct.niscount(buf.lp, _params), "AMD: integrity check 7956 failed");
            ap.assert((knscountkth(buf.sete, p, _params) > 2 || apstruct.niscount(buf.setq, _params) > 0) || vtxgetapprox(buf.vertexdegrees, p, _params) == apstruct.niscount(buf.lp, _params), "AMD: integrity check 7295 failed");
            knsstartenumeration(buf.sete, p, _params);
            while (knsenumerate(buf.sete, ref j, _params))
            {
                mtxclearcolumn(buf.mtxl, j, _params);
            }
            knsstartenumeration(buf.setsuper, p, _params);
            while (knsenumerate(buf.setsuper, ref j, _params))
            {
                buf.iseliminated[j] = true;
                mtxclearrow(buf.mtxl, j, _params);
            }
            knsclearkthreclaim(buf.seta, p, _params);
            knsclearkthreclaim(buf.sete, p, _params);
            buf.issupernode[p] = false;
            vtxremovevertex(buf.vertexdegrees, p, _params);
            k = k + nodesize;
        }
        ap.assert(k + apstruct.niscount(buf.setq, _params) == n, "AMD: integrity check 6326 failed");
        ap.assert(k > 0 || amdtype == 1, "AMD: integrity check 9463 failed");
        result = k;
        apserv.ivectorsetlengthatleast(ref perm, n, _params);
        apserv.ivectorsetlengthatleast(ref invperm, n, _params);
        for (i = 0; i <= n - 1; i++)
        {
            perm[i] = buf.perm[i];
            invperm[i] = buf.invperm[i];
        }
        return result;
    }


    /*************************************************************************
    Add K-th set from the source kn-set

    INPUT PARAMETERS
        SA          -   set
        Src, K      -   source kn-set and set index K
        
    OUTPUT PARAMETERS
        SA          -   modified SA

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void nsaddkth(apstruct.niset sa,
        amdknset src,
        int k,
        xparams _params)
    {
        int idxbegin = 0;
        int idxend = 0;
        int j = 0;
        int ns = 0;

        idxbegin = src.vbegin[k];
        idxend = idxbegin + src.vcnt[k];
        ns = sa.nstored;
        while (idxbegin < idxend)
        {
            j = src.data[idxbegin];
            if (sa.locationof[j] < 0)
            {
                sa.locationof[j] = ns;
                sa.items[ns] = j;
                ns = ns + 1;
            }
            idxbegin = idxbegin + 1;
        }
        sa.nstored = ns;
    }


    /*************************************************************************
    Subtracts K-th set from the source structure

    INPUT PARAMETERS
        SA          -   set
        Src, K      -   source kn-set and set index K
        
    OUTPUT PARAMETERS
        SA          -   modified SA

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void nssubtractkth(apstruct.niset sa,
        amdknset src,
        int k,
        xparams _params)
    {
        int idxbegin = 0;
        int idxend = 0;
        int j = 0;
        int loc = 0;
        int ns = 0;
        int item = 0;

        idxbegin = src.vbegin[k];
        idxend = idxbegin + src.vcnt[k];
        ns = sa.nstored;
        while (idxbegin < idxend)
        {
            j = src.data[idxbegin];
            loc = sa.locationof[j];
            if (loc >= 0)
            {
                item = sa.items[ns - 1];
                sa.items[loc] = item;
                sa.locationof[item] = loc;
                sa.locationof[j] = -1;
                ns = ns - 1;
            }
            idxbegin = idxbegin + 1;
        }
        sa.nstored = ns;
    }


    /*************************************************************************
    Counts set elements not present in the K-th set of the source structure

    INPUT PARAMETERS
        SA          -   set
        Src, K      -   source kn-set and set index K
        
    RESULT
        number of elements in SA not present in Src[K]

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static int nscountnotkth(apstruct.niset sa,
        amdknset src,
        int k,
        xparams _params)
    {
        int result = 0;
        int idxbegin = 0;
        int idxend = 0;
        int intersectcnt = 0;

        idxbegin = src.vbegin[k];
        idxend = idxbegin + src.vcnt[k];
        intersectcnt = 0;
        while (idxbegin < idxend)
        {
            if (sa.locationof[src.data[idxbegin]] >= 0)
            {
                intersectcnt = intersectcnt + 1;
            }
            idxbegin = idxbegin + 1;
        }
        result = sa.nstored - intersectcnt;
        return result;
    }


    /*************************************************************************
    Counts set elements also present in the K-th set of the source structure

    INPUT PARAMETERS
        SA          -   set
        Src, K      -   source kn-set and set index K
        
    RESULT
        number of elements in SA also present in Src[K]

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static int nscountandkth(apstruct.niset sa,
        amdknset src,
        int k,
        xparams _params)
    {
        int result = 0;
        int idxbegin = 0;
        int idxend = 0;

        idxbegin = src.vbegin[k];
        idxend = idxbegin + src.vcnt[k];
        result = 0;
        while (idxbegin < idxend)
        {
            if (sa.locationof[src.data[idxbegin]] >= 0)
            {
                result = result + 1;
            }
            idxbegin = idxbegin + 1;
        }
        return result;
    }


    /*************************************************************************
    Compresses internal storage, reclaiming previously dropped blocks. To be
    used internally by kn-set modification functions.

    INPUT PARAMETERS
        SA          -   kn-set to compress

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void knscompressstorage(amdknset sa,
        xparams _params)
    {
        int i = 0;
        int blocklen = 0;
        int setidx = 0;
        int srcoffs = 0;
        int dstoffs = 0;

        srcoffs = 0;
        dstoffs = 0;
        while (srcoffs < sa.dataused)
        {
            blocklen = sa.data[srcoffs + 0];
            setidx = sa.data[srcoffs + 1];
            ap.assert(blocklen >= knsheadersize, "knsCompressStorage: integrity check 6385 failed");
            if (setidx < 0)
            {
                srcoffs = srcoffs + blocklen;
                continue;
            }
            if (srcoffs != dstoffs)
            {
                for (i = 0; i <= blocklen - 1; i++)
                {
                    sa.data[dstoffs + i] = sa.data[srcoffs + i];
                }
                sa.vbegin[setidx] = dstoffs + knsheadersize;
            }
            dstoffs = dstoffs + blocklen;
            srcoffs = srcoffs + blocklen;
        }
        ap.assert(srcoffs == sa.dataused, "knsCompressStorage: integrity check 9464 failed");
        sa.dataused = dstoffs;
    }


    /*************************************************************************
    Reallocates internal storage for set #SetIdx, increasing its  capacity  to
    NewAllocated exactly. This function may invalidate internal  pointers  for
    ALL   sets  in  the  kn-set  structure  because  it  may  perform  storage
    compression in order to reclaim previously freed space.

    INPUT PARAMETERS
        SA          -   kn-set structure
        SetIdx      -   set to reallocate
        NewAllocated -  new size for the set, must be at least equal to already
                        allocated

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void knsreallocate(amdknset sa,
        int setidx,
        int newallocated,
        xparams _params)
    {
        int oldbegin = 0;
        int oldcnt = 0;
        int newbegin = 0;
        int j = 0;

        if (ap.len(sa.data) < sa.dataused + knsheadersize + newallocated)
        {
            knscompressstorage(sa, _params);
            if (ap.len(sa.data) < sa.dataused + knsheadersize + newallocated)
            {
                apserv.ivectorgrowto(ref sa.data, sa.dataused + knsheadersize + newallocated, _params);
            }
        }
        oldbegin = sa.vbegin[setidx];
        oldcnt = sa.vcnt[setidx];
        newbegin = sa.dataused + knsheadersize;
        sa.vbegin[setidx] = newbegin;
        sa.vallocated[setidx] = newallocated;
        sa.data[oldbegin - 1] = -1;
        sa.data[newbegin - 2] = knsheadersize + newallocated;
        sa.data[newbegin - 1] = setidx;
        sa.dataused = sa.dataused + sa.data[newbegin - 2];
        for (j = 0; j <= oldcnt - 1; j++)
        {
            sa.data[newbegin + j] = sa.data[oldbegin + j];
        }
    }


    /*************************************************************************
    Initialize kn-set

    INPUT PARAMETERS
        K           -   sets count
        N           -   set size
        kPrealloc   -   preallocate place per set (can be zero)
        
    OUTPUT PARAMETERS
        SA          -   K sets of N elements, initially empty

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void knsinit(int k,
        int n,
        int kprealloc,
        amdknset sa,
        xparams _params)
    {
        int i = 0;

        sa.k = n;
        sa.n = n;
        ablasf.isetallocv(n, -1, ref sa.flagarray, _params);
        ablasf.isetallocv(n, kprealloc, ref sa.vallocated, _params);
        apserv.ivectorsetlengthatleast(ref sa.vbegin, n, _params);
        sa.vbegin[0] = knsheadersize;
        for (i = 1; i <= n - 1; i++)
        {
            sa.vbegin[i] = sa.vbegin[i - 1] + sa.vallocated[i - 1] + knsheadersize;
        }
        sa.dataused = sa.vbegin[n - 1] + sa.vallocated[n - 1];
        apserv.ivectorsetlengthatleast(ref sa.data, sa.dataused, _params);
        for (i = 0; i <= n - 1; i++)
        {
            sa.data[sa.vbegin[i] - 2] = knsheadersize + sa.vallocated[i];
            sa.data[sa.vbegin[i] - 1] = i;
        }
        ablasf.isetallocv(n, 0, ref sa.vcnt, _params);
    }


    /*************************************************************************
    Initialize kn-set from lower triangle of symmetric A

    INPUT PARAMETERS
        A           -   lower triangular sparse matrix in CRS format
        N           -   problem size
        
    OUTPUT PARAMETERS
        SA          -   N sets of N elements, reproducing both lower and upper
                        triangles of A

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void knsinitfroma(sparse.sparsematrix a,
        int n,
        amdknset sa,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int jj = 0;
        int j0 = 0;
        int j1 = 0;

        sa.k = n;
        sa.n = n;
        ablasf.isetallocv(n, -1, ref sa.flagarray, _params);
        apserv.ivectorsetlengthatleast(ref sa.vallocated, n, _params);
        for (i = 0; i <= n - 1; i++)
        {
            ap.assert(a.didx[i] < a.uidx[i], "knsInitFromA: integrity check for diagonal of A failed");
            j0 = a.ridx[i];
            j1 = a.didx[i] - 1;
            sa.vallocated[i] = 1 + (j1 - j0 + 1);
            for (jj = j0; jj <= j1; jj++)
            {
                j = a.idx[jj];
                sa.vallocated[j] = sa.vallocated[j] + 1;
            }
        }
        apserv.ivectorsetlengthatleast(ref sa.vbegin, n, _params);
        sa.vbegin[0] = knsheadersize;
        for (i = 1; i <= n - 1; i++)
        {
            sa.vbegin[i] = sa.vbegin[i - 1] + sa.vallocated[i - 1] + knsheadersize;
        }
        sa.dataused = sa.vbegin[n - 1] + sa.vallocated[n - 1];
        apserv.ivectorsetlengthatleast(ref sa.data, sa.dataused, _params);
        for (i = 0; i <= n - 1; i++)
        {
            sa.data[sa.vbegin[i] - 2] = knsheadersize + sa.vallocated[i];
            sa.data[sa.vbegin[i] - 1] = i;
        }
        ablasf.isetallocv(n, 0, ref sa.vcnt, _params);
        for (i = 0; i <= n - 1; i++)
        {
            sa.data[sa.vbegin[i] + sa.vcnt[i]] = i;
            sa.vcnt[i] = sa.vcnt[i] + 1;
            j0 = a.ridx[i];
            j1 = a.didx[i] - 1;
            for (jj = j0; jj <= j1; jj++)
            {
                j = a.idx[jj];
                sa.data[sa.vbegin[i] + sa.vcnt[i]] = j;
                sa.data[sa.vbegin[j] + sa.vcnt[j]] = i;
                sa.vcnt[i] = sa.vcnt[i] + 1;
                sa.vcnt[j] = sa.vcnt[j] + 1;
            }
        }
    }


    /*************************************************************************
    Prepares iteration over I-th set

    INPUT PARAMETERS
        SA          -   kn-set
        I           -   set index
        
    OUTPUT PARAMETERS
        SA          -   SA ready for repeated calls of knsEnumerate()

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void knsstartenumeration(amdknset sa,
        int i,
        xparams _params)
    {
        sa.iterrow = i;
        sa.iteridx = 0;
    }


    /*************************************************************************
    Iterates over I-th set (as specified during recent knsStartEnumeration call).
    Subsequent calls return True and set J to new set item until iteration
    stops and False is returned.

    INPUT PARAMETERS
        SA          -   kn-set
        
    OUTPUT PARAMETERS
        J           -   if:
                        * Result=True - index of element in the set
                        * Result=False - not set


      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static bool knsenumerate(amdknset sa,
        ref int i,
        xparams _params)
    {
        bool result = new bool();

        i = 0;

        if (sa.iteridx < sa.vcnt[sa.iterrow])
        {
            i = sa.data[sa.vbegin[sa.iterrow] + sa.iteridx];
            sa.iteridx = sa.iteridx + 1;
            result = true;
        }
        else
        {
            result = false;
        }
        return result;
    }


    /*************************************************************************
    Allows direct access to internal storage  of  kn-set  structure  - returns
    range of elements SA.Data[idxBegin...idxEnd-1] used to store K-th set

    INPUT PARAMETERS
        SA          -   kn-set
        K           -   set index
        
    OUTPUT PARAMETERS
        idxBegin,
        idxEnd      -   half-range [idxBegin,idxEnd) of SA.Data that stores
                        K-th set


      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void knsdirectaccess(amdknset sa,
        int k,
        ref int idxbegin,
        ref int idxend,
        xparams _params)
    {
        idxbegin = 0;
        idxend = 0;

        idxbegin = sa.vbegin[k];
        idxend = idxbegin + sa.vcnt[k];
    }


    /*************************************************************************
    Add K-th element to I-th set. The caller guarantees that  the  element  is
    not present in the target set.

    INPUT PARAMETERS
        SA          -   kn-set
        I           -   set index
        K           -   element to add
        
    OUTPUT PARAMETERS
        SA          -   modified SA

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void knsaddnewelement(amdknset sa,
        int i,
        int k,
        xparams _params)
    {
        int cnt = 0;

        cnt = sa.vcnt[i];
        if (cnt == sa.vallocated[i])
        {
            knsreallocate(sa, i, 2 * sa.vallocated[i] + 1, _params);
        }
        sa.data[sa.vbegin[i] + cnt] = k;
        sa.vcnt[i] = cnt + 1;
    }


    /*************************************************************************
    Subtracts source n-set from the I-th set of the destination kn-set.

    INPUT PARAMETERS
        SA          -   destination kn-set structure
        I           -   set index in the structure
        Src         -   source n-set
        
    OUTPUT PARAMETERS
        SA          -   I-th set except for elements in Src

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void knssubtract1(amdknset sa,
        int i,
        apstruct.niset src,
        xparams _params)
    {
        int j = 0;
        int idxbegin = 0;
        int idxend = 0;
        int cnt = 0;

        cnt = sa.vcnt[i];
        idxbegin = sa.vbegin[i];
        idxend = idxbegin + cnt;
        while (idxbegin < idxend)
        {
            j = sa.data[idxbegin];
            if (src.locationof[j] >= 0)
            {
                sa.data[idxbegin] = sa.data[idxend - 1];
                idxend = idxend - 1;
                cnt = cnt - 1;
            }
            else
            {
                idxbegin = idxbegin + 1;
            }
        }
        sa.vcnt[i] = cnt;
    }


    /*************************************************************************
    Adds Kth set of the source kn-set to the I-th destination set. The  caller
    guarantees that SA[I] and Src[J] do NOT intersect, i.e. do not have shared
    elements - it allows to use faster algorithms.

    INPUT PARAMETERS
        SA          -   destination kn-set structure
        I           -   set index in the structure
        Src         -   source kn-set
        K           -   set index
        
    OUTPUT PARAMETERS
        SA          -   I-th set plus for elements in K-th set of Src

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void knsaddkthdistinct(amdknset sa,
        int i,
        amdknset src,
        int k,
        xparams _params)
    {
        int idxdst = 0;
        int idxsrcbegin = 0;
        int cnt = 0;
        int srccnt = 0;
        int j = 0;

        cnt = sa.vcnt[i];
        srccnt = src.vcnt[k];
        if (cnt + srccnt > sa.vallocated[i])
        {
            knsreallocate(sa, i, 2 * (cnt + srccnt) + 1, _params);
        }
        idxsrcbegin = src.vbegin[k];
        idxdst = sa.vbegin[i] + cnt;
        for (j = 0; j <= srccnt - 1; j++)
        {
            sa.data[idxdst] = src.data[idxsrcbegin + j];
            idxdst = idxdst + 1;
        }
        sa.vcnt[i] = cnt + srccnt;
    }


    /*************************************************************************
    Counts elements of K-th set of S0

    INPUT PARAMETERS
        S0          -   kn-set structure
        K           -   set index in the structure S0
        
    RESULT
        K-th set element count

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static int knscountkth(amdknset s0,
        int k,
        xparams _params)
    {
        int result = 0;

        result = s0.vcnt[k];
        return result;
    }


    /*************************************************************************
    Counts elements of I-th set of S0 not present in S1

    INPUT PARAMETERS
        S0          -   kn-set structure
        I           -   set index in the structure S0
        S1          -   kn-set to compare against
        
    RESULT
        count

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static int knscountnot(amdknset s0,
        int i,
        apstruct.niset s1,
        xparams _params)
    {
        int result = 0;
        int idxbegin0 = 0;
        int cnt0 = 0;
        int j = 0;

        cnt0 = s0.vcnt[i];
        idxbegin0 = s0.vbegin[i];
        result = 0;
        for (j = 0; j <= cnt0 - 1; j++)
        {
            if (s1.locationof[s0.data[idxbegin0 + j]] < 0)
            {
                result = result + 1;
            }
        }
        return result;
    }


    /*************************************************************************
    Counts elements of I-th set of S0 not present in K-th set of S1

    INPUT PARAMETERS
        S0          -   kn-set structure
        I           -   set index in the structure S0
        S1          -   kn-set to compare against
        K           -   set index in the structure S1
        
    RESULT
        count

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static int knscountnotkth(amdknset s0,
        int i,
        amdknset s1,
        int k,
        xparams _params)
    {
        int result = 0;
        int idxbegin0 = 0;
        int idxbegin1 = 0;
        int cnt0 = 0;
        int cnt1 = 0;
        int j = 0;

        cnt0 = s0.vcnt[i];
        cnt1 = s1.vcnt[k];
        idxbegin0 = s0.vbegin[i];
        idxbegin1 = s1.vbegin[k];
        for (j = 0; j <= cnt1 - 1; j++)
        {
            s0.flagarray[s1.data[idxbegin1 + j]] = 1;
        }
        result = 0;
        for (j = 0; j <= cnt0 - 1; j++)
        {
            if (s0.flagarray[s0.data[idxbegin0 + j]] < 0)
            {
                result = result + 1;
            }
        }
        for (j = 0; j <= cnt1 - 1; j++)
        {
            s0.flagarray[s1.data[idxbegin1 + j]] = -1;
        }
        return result;
    }


    /*************************************************************************
    Counts elements of I-th set of S0 that are also present in K-th set of S1

    INPUT PARAMETERS
        S0          -   kn-set structure
        I           -   set index in the structure S0
        S1          -   kn-set to compare against
        K           -   set index in the structure S1
        
    RESULT
        count

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static int knscountandkth(amdknset s0,
        int i,
        amdknset s1,
        int k,
        xparams _params)
    {
        int result = 0;
        int idxbegin0 = 0;
        int idxbegin1 = 0;
        int cnt0 = 0;
        int cnt1 = 0;
        int j = 0;

        cnt0 = s0.vcnt[i];
        cnt1 = s1.vcnt[k];
        idxbegin0 = s0.vbegin[i];
        idxbegin1 = s1.vbegin[k];
        for (j = 0; j <= cnt1 - 1; j++)
        {
            s0.flagarray[s1.data[idxbegin1 + j]] = 1;
        }
        result = 0;
        for (j = 0; j <= cnt0 - 1; j++)
        {
            if (s0.flagarray[s0.data[idxbegin0 + j]] > 0)
            {
                result = result + 1;
            }
        }
        for (j = 0; j <= cnt1 - 1; j++)
        {
            s0.flagarray[s1.data[idxbegin1 + j]] = -1;
        }
        return result;
    }


    /*************************************************************************
    Sums elements in I-th set of S0, returns sum.

    INPUT PARAMETERS
        S0          -   kn-set structure
        I           -   set index in the structure S0
        
    RESULT
        sum

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static int knssumkth(amdknset s0,
        int i,
        xparams _params)
    {
        int result = 0;
        int idxbegin0 = 0;
        int cnt0 = 0;
        int j = 0;

        cnt0 = s0.vcnt[i];
        idxbegin0 = s0.vbegin[i];
        result = 0;
        for (j = 0; j <= cnt0 - 1; j++)
        {
            result = result + s0.data[idxbegin0 + j];
        }
        return result;
    }


    /*************************************************************************
    Clear k-th kn-set in collection.

    Freed memory is NOT reclaimed for future garbage collection.

    INPUT PARAMETERS
        SA          -   kn-set structure
        K           -   set index
        
    OUTPUT PARAMETERS
        SA          -   K-th set was cleared

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void knsclearkthnoreclaim(amdknset sa,
        int k,
        xparams _params)
    {
        sa.vcnt[k] = 0;
    }


    /*************************************************************************
    Clear k-th kn-set in collection.

    Freed memory is reclaimed for future garbage collection. This function  is
    NOT recommended if you intend to add elements to this set in some  future,
    because every addition will result in  reallocation  of  previously  freed
    memory. Use knsClearKthNoReclaim().

    INPUT PARAMETERS
        SA          -   kn-set structure
        K           -   set index
        
    OUTPUT PARAMETERS
        SA          -   K-th set was cleared

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void knsclearkthreclaim(amdknset sa,
        int k,
        xparams _params)
    {
        int idxbegin = 0;
        int allocated = 0;

        idxbegin = sa.vbegin[k];
        allocated = sa.vallocated[k];
        sa.vcnt[k] = 0;
        if (allocated >= knsheadersize)
        {
            sa.data[idxbegin - 2] = 2;
            sa.data[idxbegin + 0] = allocated;
            sa.data[idxbegin + 1] = -1;
            sa.vallocated[k] = 0;
        }
    }


    /*************************************************************************
    Initialize linked list matrix

    INPUT PARAMETERS
        N           -   matrix size
        
    OUTPUT PARAMETERS
        A           -   NxN linked list matrix

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void mtxinit(int n,
        amdllmatrix a,
        xparams _params)
    {
        a.n = n;
        ablasf.isetallocv(2 * n + 1, -1, ref a.vbegin, _params);
        ablasf.isetallocv(n, 0, ref a.vcolcnt, _params);
        a.entriesinitialized = 0;
    }


    /*************************************************************************
    Adds column from matrix to n-set

    INPUT PARAMETERS
        A           -   NxN linked list matrix
        J           -   column index to add
        S           -   target n-set
        
    OUTPUT PARAMETERS
        S           -   elements from J-th column are added to S
        

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void mtxaddcolumnto(amdllmatrix a,
        int j,
        apstruct.niset s,
        xparams _params)
    {
        int n = 0;
        int eidx = 0;

        n = a.n;
        eidx = a.vbegin[n + j];
        while (eidx >= 0)
        {
            apstruct.nisaddelement(s, a.entries[eidx * llmentrysize + 4], _params);
            eidx = a.entries[eidx * llmentrysize + 3];
        }
    }


    /*************************************************************************
    Inserts new element into column J, row I. The caller guarantees  that  the
    element being inserted is NOT already present in the matrix.

    INPUT PARAMETERS
        A           -   NxN linked list matrix
        I           -   row index
        J           -   column index
        
    OUTPUT PARAMETERS
        A           -   element (I,J) added to the list.
        

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void mtxinsertnewelement(amdllmatrix a,
        int i,
        int j,
        xparams _params)
    {
        int n = 0;
        int k = 0;
        int newsize = 0;
        int eidx = 0;
        int offs = 0;

        n = a.n;
        if (a.vbegin[2 * n] < 0)
        {
            newsize = 2 * a.entriesinitialized + 1;
            apserv.ivectorresize(ref a.entries, newsize * llmentrysize, _params);
            for (k = a.entriesinitialized; k <= newsize - 2; k++)
            {
                a.entries[k * llmentrysize + 0] = k + 1;
            }
            a.entries[(newsize - 1) * llmentrysize + 0] = a.vbegin[2 * n];
            a.vbegin[2 * n] = a.entriesinitialized;
            a.entriesinitialized = newsize;
        }
        eidx = a.vbegin[2 * n];
        offs = eidx * llmentrysize;
        a.vbegin[2 * n] = a.entries[offs + 0];
        a.entries[offs + 0] = -1;
        a.entries[offs + 1] = a.vbegin[i];
        if (a.vbegin[i] >= 0)
        {
            a.entries[a.vbegin[i] * llmentrysize + 0] = eidx;
        }
        a.entries[offs + 2] = -1;
        a.entries[offs + 3] = a.vbegin[j + n];
        if (a.vbegin[j + n] >= 0)
        {
            a.entries[a.vbegin[j + n] * llmentrysize + 2] = eidx;
        }
        a.entries[offs + 4] = i;
        a.entries[offs + 5] = j;
        a.vbegin[i] = eidx;
        a.vbegin[j + n] = eidx;
        a.vcolcnt[j] = a.vcolcnt[j] + 1;
    }


    /*************************************************************************
    Counts elements in J-th column that are not present in n-set S

    INPUT PARAMETERS
        A           -   NxN linked list matrix
        J           -   column index
        S           -   n-set to compare against
        
    RESULT
        element count
        

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static int mtxcountcolumnnot(amdllmatrix a,
        int j,
        apstruct.niset s,
        xparams _params)
    {
        int result = 0;
        int n = 0;
        int eidx = 0;

        n = a.n;
        result = 0;
        eidx = a.vbegin[n + j];
        while (eidx >= 0)
        {
            if (s.locationof[a.entries[eidx * llmentrysize + 4]] < 0)
            {
                result = result + 1;
            }
            eidx = a.entries[eidx * llmentrysize + 3];
        }
        return result;
    }


    /*************************************************************************
    Counts elements in J-th column

    INPUT PARAMETERS
        A           -   NxN linked list matrix
        J           -   column index
        
    RESULT
        element count
        

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static int mtxcountcolumn(amdllmatrix a,
        int j,
        xparams _params)
    {
        int result = 0;

        result = a.vcolcnt[j];
        return result;
    }


    /*************************************************************************
    Clears K-th column or row

    INPUT PARAMETERS
        A           -   NxN linked list matrix
        K           -   column/row index to clear
        IsCol       -   whether we want to clear row or column
        
    OUTPUT PARAMETERS
        A           -   K-th column or row is empty
        

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void mtxclearx(amdllmatrix a,
        int k,
        bool iscol,
        xparams _params)
    {
        int n = 0;
        int eidx = 0;
        int enext = 0;
        int idxprev = 0;
        int idxnext = 0;
        int idxr = 0;
        int idxc = 0;

        n = a.n;
        if (iscol)
        {
            eidx = a.vbegin[n + k];
        }
        else
        {
            eidx = a.vbegin[k];
        }
        while (eidx >= 0)
        {
            idxr = a.entries[eidx * llmentrysize + 4];
            idxc = a.entries[eidx * llmentrysize + 5];
            if (iscol)
            {
                enext = a.entries[eidx * llmentrysize + 3];
            }
            else
            {
                enext = a.entries[eidx * llmentrysize + 1];
            }
            idxprev = a.entries[eidx * llmentrysize + 0];
            idxnext = a.entries[eidx * llmentrysize + 1];
            if (idxprev >= 0)
            {
                a.entries[idxprev * llmentrysize + 1] = idxnext;
            }
            else
            {
                a.vbegin[idxr] = idxnext;
            }
            if (idxnext >= 0)
            {
                a.entries[idxnext * llmentrysize + 0] = idxprev;
            }
            idxprev = a.entries[eidx * llmentrysize + 2];
            idxnext = a.entries[eidx * llmentrysize + 3];
            if (idxprev >= 0)
            {
                a.entries[idxprev * llmentrysize + 3] = idxnext;
            }
            else
            {
                a.vbegin[idxc + n] = idxnext;
            }
            if (idxnext >= 0)
            {
                a.entries[idxnext * llmentrysize + 2] = idxprev;
            }
            a.entries[eidx * llmentrysize + 0] = a.vbegin[2 * n];
            a.vbegin[2 * n] = eidx;
            eidx = enext;
            if (!iscol)
            {
                a.vcolcnt[idxc] = a.vcolcnt[idxc] - 1;
            }
        }
        if (iscol)
        {
            a.vcolcnt[k] = 0;
        }
    }


    /*************************************************************************
    Clears J-th column

    INPUT PARAMETERS
        A           -   NxN linked list matrix
        J           -   column index to clear
        
    OUTPUT PARAMETERS
        A           -   J-th column is empty
        

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void mtxclearcolumn(amdllmatrix a,
        int j,
        xparams _params)
    {
        mtxclearx(a, j, true, _params);
    }


    /*************************************************************************
    Clears J-th row

    INPUT PARAMETERS
        A           -   NxN linked list matrix
        J           -   row index to clear
        
    OUTPUT PARAMETERS
        A           -   J-th row is empty
        

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void mtxclearrow(amdllmatrix a,
        int j,
        xparams _params)
    {
        mtxclearx(a, j, false, _params);
    }


    /*************************************************************************
    Initialize vertex storage using A to estimate initial degrees

    INPUT PARAMETERS
        A           -   NxN lower triangular sparse CRS matrix
        N           -   problem size
        CheckExactDegrees-
                        whether we want to maintain additional exact degress
                        (the search is still done using approximate ones)
        
    OUTPUT PARAMETERS
        S           -   vertex set
        

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void vtxinit(sparse.sparsematrix a,
        int n,
        bool checkexactdegrees,
        amdvertexset s,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int jj = 0;
        int j0 = 0;
        int j1 = 0;

        s.n = n;
        s.checkexactdegrees = checkexactdegrees;
        s.smallestdegree = 0;
        ablasf.bsetallocv(n, true, ref s.isvertex, _params);
        ablasf.isetallocv(n, 0, ref s.approxd, _params);
        for (i = 0; i <= n - 1; i++)
        {
            j0 = a.ridx[i];
            j1 = a.didx[i] - 1;
            s.approxd[i] = j1 - j0 + 1;
            for (jj = j0; jj <= j1; jj++)
            {
                j = a.idx[jj];
                s.approxd[j] = s.approxd[j] + 1;
            }
        }
        if (checkexactdegrees)
        {
            ablasf.icopyallocv(n, s.approxd, ref s.optionalexactd, _params);
        }
        ablasf.isetallocv(n, -1, ref s.vbegin, _params);
        ablasf.isetallocv(n, -1, ref s.vprev, _params);
        ablasf.isetallocv(n, -1, ref s.vnext, _params);
        for (i = 0; i <= n - 1; i++)
        {
            j = s.approxd[i];
            j0 = s.vbegin[j];
            s.vbegin[j] = i;
            s.vnext[i] = j0;
            s.vprev[i] = -1;
            if (j0 >= 0)
            {
                s.vprev[j0] = i;
            }
        }
    }


    /*************************************************************************
    Removes vertex from the storage

    INPUT PARAMETERS
        S           -   vertex set
        P           -   vertex to be removed
        
    OUTPUT PARAMETERS
        S           -   modified
        

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void vtxremovevertex(amdvertexset s,
        int p,
        xparams _params)
    {
        int d = 0;
        int idxprev = 0;
        int idxnext = 0;

        d = s.approxd[p];
        idxprev = s.vprev[p];
        idxnext = s.vnext[p];
        if (idxprev >= 0)
        {
            s.vnext[idxprev] = idxnext;
        }
        else
        {
            s.vbegin[d] = idxnext;
        }
        if (idxnext >= 0)
        {
            s.vprev[idxnext] = idxprev;
        }
        s.isvertex[p] = false;
        s.approxd[p] = -9999999;
        if (s.checkexactdegrees)
        {
            s.optionalexactd[p] = -9999999;
        }
    }


    /*************************************************************************
    Get approximate degree. Result is undefined for removed vertexes.

    INPUT PARAMETERS
        S           -   vertex set
        P           -   vertex index
        
    RESULT
        vertex degree
        

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static int vtxgetapprox(amdvertexset s,
        int p,
        xparams _params)
    {
        int result = 0;

        result = s.approxd[p];
        return result;
    }


    /*************************************************************************
    Get exact degree (or 0, if not supported).  Result is undefined for
    removed vertexes.

    INPUT PARAMETERS
        S           -   vertex set
        P           -   vertex index
        
    RESULT
        vertex degree
        

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static int vtxgetexact(amdvertexset s,
        int p,
        xparams _params)
    {
        int result = 0;

        if (s.checkexactdegrees)
        {
            result = s.optionalexactd[p];
        }
        else
        {
            result = 0;
        }
        return result;
    }


    /*************************************************************************
    Returns index of vertex with minimum approximate degree, or -1 when there
    is no vertex.

    INPUT PARAMETERS
        S           -   vertex set
        
    RESULT
        vertex index, or -1
        

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static int vtxgetapproxmindegree(amdvertexset s,
        xparams _params)
    {
        int result = 0;
        int i = 0;
        int n = 0;

        n = s.n;
        result = -1;
        for (i = s.smallestdegree; i <= n - 1; i++)
        {
            if (s.vbegin[i] >= 0)
            {
                s.smallestdegree = i;
                result = s.vbegin[i];
                return result;
            }
        }
        return result;
    }


    /*************************************************************************
    Update approximate degree

    INPUT PARAMETERS
        S           -   vertex set
        P           -   vertex to be updated
        DNew        -   new degree
        
    OUTPUT PARAMETERS
        S           -   modified
        

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void vtxupdateapproximatedegree(amdvertexset s,
        int p,
        int dnew,
        xparams _params)
    {
        int dold = 0;
        int idxprev = 0;
        int idxnext = 0;
        int oldbegin = 0;

        dold = s.approxd[p];
        if (dold == dnew)
        {
            return;
        }
        idxprev = s.vprev[p];
        idxnext = s.vnext[p];
        if (idxprev >= 0)
        {
            s.vnext[idxprev] = idxnext;
        }
        else
        {
            s.vbegin[dold] = idxnext;
        }
        if (idxnext >= 0)
        {
            s.vprev[idxnext] = idxprev;
        }
        oldbegin = s.vbegin[dnew];
        s.vbegin[dnew] = p;
        s.vnext[p] = oldbegin;
        s.vprev[p] = -1;
        if (oldbegin >= 0)
        {
            s.vprev[oldbegin] = p;
        }
        s.approxd[p] = dnew;
        if (dnew < s.smallestdegree)
        {
            s.smallestdegree = dnew;
        }
    }


    /*************************************************************************
    Update optional exact degree. Silently returns if vertex set does not store
    exact degrees.

    INPUT PARAMETERS
        S           -   vertex set
        P           -   vertex to be updated
        D           -   new degree
        
    OUTPUT PARAMETERS
        S           -   modified
        

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void vtxupdateexactdegree(amdvertexset s,
        int p,
        int d,
        xparams _params)
    {
        if (!s.checkexactdegrees)
        {
            return;
        }
        s.optionalexactd[p] = d;
    }


    /*************************************************************************
    This function selects K-th  pivot  with  minimum  approximate  degree  and
    generates permutation that reorders variable to the K-th position  in  the
    matrix.

    Due to supernodal structure of the matrix more than one pivot variable can
    be selected and moved to the beginning. The actual count of pivots selected
    is returned in NodeSize.

    INPUT PARAMETERS
        Buf         -   properly initialized buffer object
        K           -   pivot index
        
    OUTPUT PARAMETERS
        Buf.Perm    -   entries [K,K+NodeSize) are initialized by permutation
        Buf.InvPerm -   entries [K,K+NodeSize) are initialized by permutation
        Buf.ColumnSwaps-entries [K,K+NodeSize) are initialized by permutation
        P           -   pivot supervariable
        NodeSize    -   supernode size

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void amdselectpivotelement(amdbuffer buf,
        int k,
        ref int p,
        ref int nodesize,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        p = 0;
        nodesize = 0;

        p = vtxgetapproxmindegree(buf.vertexdegrees, _params);
        ap.assert(p >= 0, "GenerateAMDPermutation: integrity check 3634 failed");
        ap.assert(vtxgetapprox(buf.vertexdegrees, p, _params) >= 0, "integrity check RDFD2 failed");
        nodesize = 0;
        knsstartenumeration(buf.setsuper, p, _params);
        while (knsenumerate(buf.setsuper, ref j, _params))
        {
            i = buf.perm[j];
            buf.columnswaps[k + nodesize] = i;
            buf.invperm[i] = buf.invperm[k + nodesize];
            buf.invperm[k + nodesize] = j;
            buf.perm[buf.invperm[i]] = i;
            buf.perm[buf.invperm[k + nodesize]] = k + nodesize;
            apserv.inc(ref nodesize, _params);
        }
        ap.assert(vtxgetapprox(buf.vertexdegrees, p, _params) >= 0 && (!buf.checkexactdegrees || vtxgetexact(buf.vertexdegrees, p, _params) >= 0), "AMD: integrity check RDFD failed");
    }


    /*************************************************************************
    This function computes nonzero pattern of Lp, the column that is added  to
    the lower triangular Cholesky factor.

    INPUT PARAMETERS
        Buf         -   properly initialized buffer object
        P           -   pivot column
        
    OUTPUT PARAMETERS
        Buf.setP    -   initialized with setSuper[P]
        Buf.Lp      -   initialized with Lp\P
        Buf.setRp   -   initialized with Lp\{P+Q}
        Buf.Ep      -   initialized with setE[P]
        Buf.mtxL    -   L := L+Lp
        Buf.Ls      -   first Buf.LSCnt elements contain subset of Lp elements
                        that are principal nodes in supervariables.

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void amdcomputelp(amdbuffer buf,
        int p,
        xparams _params)
    {
        int i = 0;

        apstruct.nisclear(buf.setp, _params);
        nsaddkth(buf.setp, buf.setsuper, p, _params);
        apstruct.nisclear(buf.lp, _params);
        nsaddkth(buf.lp, buf.seta, p, _params);
        knsstartenumeration(buf.sete, p, _params);
        while (knsenumerate(buf.sete, ref i, _params))
        {
            mtxaddcolumnto(buf.mtxl, i, buf.lp, _params);
        }
        nssubtractkth(buf.lp, buf.setsuper, p, _params);
        apstruct.niscopy(buf.lp, buf.setrp, _params);
        apstruct.nissubtract1(buf.setrp, buf.setq, _params);
        buf.lscnt = 0;
        apstruct.nisstartenumeration(buf.lp, _params);
        while (apstruct.nisenumerate(buf.lp, ref i, _params))
        {
            ap.assert(!buf.iseliminated[i], "AMD: integrity check 0740 failed");
            mtxinsertnewelement(buf.mtxl, i, p, _params);
            if (buf.issupernode[i])
            {
                buf.ls[buf.lscnt] = i;
                buf.lscnt = buf.lscnt + 1;
            }
        }
        apstruct.nisclear(buf.ep, _params);
        nsaddkth(buf.ep, buf.sete, p, _params);
    }


    /*************************************************************************
    Having output of AMDComputeLp() in the Buf object, this function  performs
    mass elimination in the quotient graph.

    INPUT PARAMETERS
        Buf         -   properly initialized buffer object
        P           -   pivot column
        K           -   number of already eliminated columns (P-th is not counted)
        Tau         -   variables with degrees higher than Tau will be classified
                        as quasidense
        
    OUTPUT PARAMETERS
        Buf.setA    -   Lp is eliminated from setA
        Buf.setE    -   Ep is eliminated from setE, P is added
        approxD     -   updated
        Buf.setQSuperCand-   contains candidates for quasidense status assignment

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void amdmasselimination(amdbuffer buf,
        int p,
        int k,
        int tau,
        xparams _params)
    {
        int n = 0;
        int lidx = 0;
        int lpi = 0;
        int cntsuperi = 0;
        int cntq = 0;
        int cntainoti = 0;
        int cntainotqi = 0;
        int cntlpnoti = 0;
        int cntlpnotqi = 0;
        int cc = 0;
        int j = 0;
        int e = 0;
        int we = 0;
        int cnttoclean = 0;
        int idxbegin = 0;
        int idxend = 0;
        int jj = 0;
        int bnd0 = 0;
        int bnd1 = 0;
        int bnd2 = 0;
        int d = 0;

        n = buf.n;
        apserv.ivectorsetlengthatleast(ref buf.tmp0, n, _params);
        cnttoclean = 0;
        for (lidx = 0; lidx <= buf.lscnt - 1; lidx++)
        {
            if (buf.setq.locationof[buf.ls[lidx]] < 0)
            {
                lpi = buf.ls[lidx];
                cntsuperi = knscountkth(buf.setsuper, lpi, _params);
                knsdirectaccess(buf.sete, lpi, ref idxbegin, ref idxend, _params);
                for (jj = idxbegin; jj <= idxend - 1; jj++)
                {
                    e = buf.sete.data[jj];
                    we = buf.arrwe[e];
                    if (we < 0)
                    {
                        we = mtxcountcolumnnot(buf.mtxl, e, buf.setq, _params);
                        buf.tmp0[cnttoclean] = e;
                        cnttoclean = cnttoclean + 1;
                    }
                    buf.arrwe[e] = we - cntsuperi;
                }
            }
        }
        apstruct.nisclear(buf.setqsupercand, _params);
        for (lidx = 0; lidx <= buf.lscnt - 1; lidx++)
        {
            if (buf.setq.locationof[buf.ls[lidx]] < 0)
            {
                lpi = buf.ls[lidx];
                knssubtract1(buf.seta, lpi, buf.lp, _params);
                knssubtract1(buf.seta, lpi, buf.setp, _params);
                knssubtract1(buf.sete, lpi, buf.ep, _params);
                knsaddnewelement(buf.sete, lpi, p, _params);
                if (buf.extendeddebug)
                {
                    ap.assert(knscountnotkth(buf.seta, lpi, buf.setsuper, lpi, _params) == knscountkth(buf.seta, lpi, _params), "AMD: integrity check 454F failed");
                    ap.assert(knscountandkth(buf.seta, lpi, buf.setsuper, lpi, _params) == 0, "AMD: integrity check kl5nv failed");
                    ap.assert(nscountandkth(buf.lp, buf.setsuper, lpi, _params) == knscountkth(buf.setsuper, lpi, _params), "AMD: integrity check 8463 failed");
                }
                cntq = apstruct.niscount(buf.setq, _params);
                cntsuperi = knscountkth(buf.setsuper, lpi, _params);
                cntainoti = knscountkth(buf.seta, lpi, _params);
                if (cntq > 0)
                {
                    cntainotqi = knscountnot(buf.seta, lpi, buf.setq, _params);
                }
                else
                {
                    cntainotqi = cntainoti;
                }
                cntlpnoti = apstruct.niscount(buf.lp, _params) - cntsuperi;
                cntlpnotqi = apstruct.niscount(buf.setrp, _params) - cntsuperi;
                cc = 0;
                knsdirectaccess(buf.sete, lpi, ref idxbegin, ref idxend, _params);
                for (jj = idxbegin; jj <= idxend - 1; jj++)
                {
                    j = buf.sete.data[jj];
                    if (j == p)
                    {
                        continue;
                    }
                    e = buf.arrwe[j];
                    if (e < 0)
                    {
                        if (cntq > 0)
                        {
                            e = mtxcountcolumnnot(buf.mtxl, j, buf.setq, _params);
                        }
                        else
                        {
                            e = mtxcountcolumn(buf.mtxl, j, _params);
                        }
                    }
                    cc = cc + e;
                }
                bnd0 = n - k - apstruct.niscount(buf.setp, _params);
                bnd1 = vtxgetapprox(buf.vertexdegrees, lpi, _params) + cntlpnoti;
                bnd2 = cntq + cntainotqi + cntlpnotqi + cc;
                d = apserv.imin3(bnd0, bnd1, bnd2, _params);
                vtxupdateapproximatedegree(buf.vertexdegrees, lpi, d, _params);
                if (tau > 0 && d + cntsuperi > tau)
                {
                    apstruct.nisaddelement(buf.setqsupercand, lpi, _params);
                }
                if (buf.checkexactdegrees)
                {
                    apstruct.nisclear(buf.exactdegreetmp0, _params);
                    knsstartenumeration(buf.sete, lpi, _params);
                    while (knsenumerate(buf.sete, ref j, _params))
                    {
                        mtxaddcolumnto(buf.mtxl, j, buf.exactdegreetmp0, _params);
                    }
                    vtxupdateexactdegree(buf.vertexdegrees, lpi, cntainoti + nscountnotkth(buf.exactdegreetmp0, buf.setsuper, lpi, _params), _params);
                    ap.assert((knscountkth(buf.sete, lpi, _params) > 2 || cntq > 0) || vtxgetapprox(buf.vertexdegrees, lpi, _params) == vtxgetexact(buf.vertexdegrees, lpi, _params), "AMD: integrity check 7206 failed");
                    ap.assert(vtxgetapprox(buf.vertexdegrees, lpi, _params) >= vtxgetexact(buf.vertexdegrees, lpi, _params), "AMD: integrity check 8206 failed");
                }
            }
        }
        for (j = 0; j <= cnttoclean - 1; j++)
        {
            buf.arrwe[buf.tmp0[j]] = -1;
        }
    }


    /*************************************************************************
    After mass elimination, but before removal of vertex  P,  we  may  perform
    supernode detection. Only variables/supernodes in  Lp  (P  itself  is  NOT
    included) can be merged into larger supernodes.

    INPUT PARAMETERS
        Buf         -   properly initialized buffer object
        
    OUTPUT PARAMETERS
        Buf         -   following fields of Buf may be modified:
                        * Buf.setSuper
                        * Buf.setA
                        * Buf.setE
                        * Buf.IsSupernode
                        * ApproxD and ExactD

      -- ALGLIB PROJECT --
         Copyright 05.10.2020 by Bochkanov Sergey.
    *************************************************************************/
    private static void amddetectsupernodes(amdbuffer buf,
        xparams _params)
    {
        int n = 0;
        int i = 0;
        int j = 0;
        int cnt = 0;
        int lpi = 0;
        int lpj = 0;
        int nj = 0;
        int hashi = 0;

        n = buf.n;
        apserv.ivectorsetlengthatleast(ref buf.sncandidates, n, _params);
        if (buf.lscnt < 2)
        {
            return;
        }
        for (i = 0; i <= buf.lscnt - 1; i++)
        {
            if (buf.setq.locationof[buf.ls[i]] < 0)
            {
                lpi = buf.ls[i];
                hashi = (knssumkth(buf.seta, lpi, _params) + knssumkth(buf.sete, lpi, _params)) % n;
                apstruct.nisaddelement(buf.nonemptybuckets, hashi, _params);
                knsaddnewelement(buf.hashbuckets, hashi, lpi, _params);
            }
        }
        apstruct.nisstartenumeration(buf.nonemptybuckets, _params);
        while (apstruct.nisenumerate(buf.nonemptybuckets, ref hashi, _params))
        {
            if (knscountkth(buf.hashbuckets, hashi, _params) >= 2)
            {
                cnt = 0;
                knsstartenumeration(buf.hashbuckets, hashi, _params);
                while (knsenumerate(buf.hashbuckets, ref i, _params))
                {
                    buf.sncandidates[cnt] = i;
                    cnt = cnt + 1;
                }
                for (i = cnt - 1; i >= 0; i--)
                {
                    for (j = cnt - 1; j >= i + 1; j--)
                    {
                        if (buf.issupernode[buf.sncandidates[i]] && buf.issupernode[buf.sncandidates[j]])
                        {
                            lpi = buf.sncandidates[i];
                            lpj = buf.sncandidates[j];
                            apstruct.nisclear(buf.adji, _params);
                            apstruct.nisclear(buf.adjj, _params);
                            nsaddkth(buf.adji, buf.seta, lpi, _params);
                            nsaddkth(buf.adjj, buf.seta, lpj, _params);
                            nsaddkth(buf.adji, buf.sete, lpi, _params);
                            nsaddkth(buf.adjj, buf.sete, lpj, _params);
                            apstruct.nisaddelement(buf.adji, lpi, _params);
                            apstruct.nisaddelement(buf.adji, lpj, _params);
                            apstruct.nisaddelement(buf.adjj, lpi, _params);
                            apstruct.nisaddelement(buf.adjj, lpj, _params);
                            if (!apstruct.nisequal(buf.adji, buf.adjj, _params))
                            {
                                continue;
                            }
                            if (buf.extendeddebug)
                            {
                                ap.assert(vtxgetapprox(buf.vertexdegrees, lpi, _params) >= 1 && (!buf.checkexactdegrees || vtxgetexact(buf.vertexdegrees, lpi, _params) >= 1), "AMD: integrity check &GBFF1 failed");
                                ap.assert(vtxgetapprox(buf.vertexdegrees, lpj, _params) >= 1 && (!buf.checkexactdegrees || vtxgetexact(buf.vertexdegrees, lpj, _params) >= 1), "AMD: integrity check &GBFF2 failed");
                                ap.assert(knscountandkth(buf.setsuper, lpi, buf.setsuper, lpj, _params) == 0, "AMD: integrity check &GBFF3 failed");
                            }
                            nj = knscountkth(buf.setsuper, lpj, _params);
                            knsaddkthdistinct(buf.setsuper, lpi, buf.setsuper, lpj, _params);
                            knsclearkthreclaim(buf.setsuper, lpj, _params);
                            knsclearkthreclaim(buf.seta, lpj, _params);
                            knsclearkthreclaim(buf.sete, lpj, _params);
                            buf.issupernode[lpj] = false;
                            vtxremovevertex(buf.vertexdegrees, lpj, _params);
                            vtxupdateapproximatedegree(buf.vertexdegrees, lpi, vtxgetapprox(buf.vertexdegrees, lpi, _params) - nj, _params);
                            if (buf.checkexactdegrees)
                            {
                                vtxupdateexactdegree(buf.vertexdegrees, lpi, vtxgetexact(buf.vertexdegrees, lpi, _params) - nj, _params);
                            }
                        }
                    }
                }
            }
            knsclearkthnoreclaim(buf.hashbuckets, hashi, _params);
        }
        apstruct.nisclear(buf.nonemptybuckets, _params);
    }


    /*************************************************************************
    Assign quasidense status to proposed supervars,  perform all the necessary
    cleanup (remove vertices, etc)

    INPUT PARAMETERS
        Buf         -   properly initialized buffer object
        Cand        -   supervariables to be moved to quasidense status
        P           -   current pivot element (used for integrity checks)
                        or -1, when this function is used for initial status
                        assignment.
        
    OUTPUT PARAMETERS
        Buf         -   variables belonging  to  supervariables  in  cand  are
                        added to SetQ. Supervariables are removed from all lists

      -- ALGLIB PROJECT --
         Copyright 15.11.2021 by Bochkanov Sergey.
    *************************************************************************/
    private static void amdmovetoquasidense(amdbuffer buf,
        apstruct.niset cand,
        int p,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        apstruct.nisstartenumeration(cand, _params);
        while (apstruct.nisenumerate(cand, ref j, _params))
        {
            ap.assert(j != p, "AMD: integrity check 9464 failed");
            ap.assert(buf.issupernode[j], "AMD: integrity check 6284 failed");
            ap.assert(!buf.iseliminated[j], "AMD: integrity check 3858 failed");
            knsstartenumeration(buf.setsuper, j, _params);
            while (knsenumerate(buf.setsuper, ref i, _params))
            {
                apstruct.nisaddelement(buf.setq, i, _params);
            }
            knsclearkthreclaim(buf.seta, j, _params);
            knsclearkthreclaim(buf.sete, j, _params);
            buf.issupernode[j] = false;
            vtxremovevertex(buf.vertexdegrees, j, _params);
        }
    }


}
