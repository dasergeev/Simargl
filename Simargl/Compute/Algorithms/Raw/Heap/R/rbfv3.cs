using System;

#pragma warning disable CS3008
#pragma warning disable CS8618
#pragma warning disable CS8604
#pragma warning disable CS8600
#pragma warning disable CS8631
#pragma warning disable CS8602
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

public class rbfv3
{
    /*************************************************************************
    Buffer object for parallel evaluation on the model matrix
    *************************************************************************/
    public class rbf3evaluatorbuffer : apobject
    {
        public double[] x;
        public double[] y;
        public double[] coeffbuf;
        public double[] funcbuf;
        public double[] wrkbuf;
        public double[] mindist2;
        public double[] df1;
        public double[] df2;
        public double[] x2;
        public double[] y2;
        public double[,] deltabuf;
        public rbf3evaluatorbuffer()
        {
            init();
        }
        public override void init()
        {
            x = new double[0];
            y = new double[0];
            coeffbuf = new double[0];
            funcbuf = new double[0];
            wrkbuf = new double[0];
            mindist2 = new double[0];
            df1 = new double[0];
            df2 = new double[0];
            x2 = new double[0];
            y2 = new double[0];
            deltabuf = new double[0, 0];
        }
        public override apobject make_copy()
        {
            rbf3evaluatorbuffer _result = new rbf3evaluatorbuffer();
            _result.x = (double[])x.Clone();
            _result.y = (double[])y.Clone();
            _result.coeffbuf = (double[])coeffbuf.Clone();
            _result.funcbuf = (double[])funcbuf.Clone();
            _result.wrkbuf = (double[])wrkbuf.Clone();
            _result.mindist2 = (double[])mindist2.Clone();
            _result.df1 = (double[])df1.Clone();
            _result.df2 = (double[])df2.Clone();
            _result.x2 = (double[])x2.Clone();
            _result.y2 = (double[])y2.Clone();
            _result.deltabuf = (double[,])deltabuf.Clone();
            return _result;
        }
    };


    /*************************************************************************
    A points cluster for the fast evaluator
    *************************************************************************/
    public class rbf3panel : apobject
    {
        public int paneltype;
        public double clusterrad;
        public double[] clustercenter;
        public double c0;
        public double c1;
        public double c2;
        public double c3;
        public int farfieldexpansion;
        public double farfielddistance;
        public int idx0;
        public int idx1;
        public int childa;
        public int childb;
        public int[] ptidx;
        public double[,] xt;
        public double[,] wt;
        public rbfv3farfields.biharmonicpanel bhexpansion;
        public rbf3evaluatorbuffer tgtbuf;
        public rbf3panel()
        {
            init();
        }
        public override void init()
        {
            clustercenter = new double[0];
            ptidx = new int[0];
            xt = new double[0, 0];
            wt = new double[0, 0];
            bhexpansion = new rbfv3farfields.biharmonicpanel();
            tgtbuf = new rbf3evaluatorbuffer();
        }
        public override apobject make_copy()
        {
            rbf3panel _result = new rbf3panel();
            _result.paneltype = paneltype;
            _result.clusterrad = clusterrad;
            _result.clustercenter = (double[])clustercenter.Clone();
            _result.c0 = c0;
            _result.c1 = c1;
            _result.c2 = c2;
            _result.c3 = c3;
            _result.farfieldexpansion = farfieldexpansion;
            _result.farfielddistance = farfielddistance;
            _result.idx0 = idx0;
            _result.idx1 = idx1;
            _result.childa = childa;
            _result.childb = childb;
            _result.ptidx = (int[])ptidx.Clone();
            _result.xt = (double[,])xt.Clone();
            _result.wt = (double[,])wt.Clone();
            _result.bhexpansion = (rbfv3farfields.biharmonicpanel)bhexpansion.make_copy();
            _result.tgtbuf = (rbf3evaluatorbuffer)tgtbuf.make_copy();
            return _result;
        }
    };


    /*************************************************************************
    Fast model evaluator
    *************************************************************************/
    public class rbf3fastevaluator : apobject
    {
        public int n;
        public int nx;
        public int ny;
        public int maxpanelsize;
        public int functype;
        public double funcparam;
        public double[,] permx;
        public int[] origptidx;
        public double[,] wstoredorig;
        public bool isloaded;
        public ap.objarray panels;
        public rbfv3farfields.biharmonicevaluator bheval;
        public smp.shared_pool bufferpool;
        public double[,] tmpx3w;
        public bool usedebugcounters;
        public int dbgpanel2panelcnt;
        public int dbgfield2panelcnt;
        public int dbgpanelscnt;
        public rbf3fastevaluator()
        {
            init();
        }
        public override void init()
        {
            permx = new double[0, 0];
            origptidx = new int[0];
            wstoredorig = new double[0, 0];
            panels = new ap.objarray();
            bheval = new rbfv3farfields.biharmonicevaluator();
            bufferpool = new smp.shared_pool();
            tmpx3w = new double[0, 0];
        }
        public override apobject make_copy()
        {
            rbf3fastevaluator _result = new rbf3fastevaluator();
            _result.n = n;
            _result.nx = nx;
            _result.ny = ny;
            _result.maxpanelsize = maxpanelsize;
            _result.functype = functype;
            _result.funcparam = funcparam;
            _result.permx = (double[,])permx.Clone();
            _result.origptidx = (int[])origptidx.Clone();
            _result.wstoredorig = (double[,])wstoredorig.Clone();
            _result.isloaded = isloaded;
            _result.panels = (ap.objarray)panels.make_copy();
            _result.bheval = (rbfv3farfields.biharmonicevaluator)bheval.make_copy();
            _result.bufferpool = (smp.shared_pool)bufferpool.make_copy();
            _result.tmpx3w = (double[,])tmpx3w.Clone();
            _result.usedebugcounters = usedebugcounters;
            _result.dbgpanel2panelcnt = dbgpanel2panelcnt;
            _result.dbgfield2panelcnt = dbgfield2panelcnt;
            _result.dbgpanelscnt = dbgpanelscnt;
            return _result;
        }
    };


    /*************************************************************************
    Model evaluator:
    *************************************************************************/
    public class rbf3evaluator : apobject
    {
        public int n;
        public int storagetype;
        public double[,] f;
        public int nx;
        public int functype;
        public double funcparam;
        public int chunksize;
        public int[] entireset;
        public double[,] x;
        public double[,] xtchunked;
        public smp.shared_pool bufferpool;
        public double[] chunk1;
        public rbf3evaluator()
        {
            init();
        }
        public override void init()
        {
            f = new double[0, 0];
            entireset = new int[0];
            x = new double[0, 0];
            xtchunked = new double[0, 0];
            bufferpool = new smp.shared_pool();
            chunk1 = new double[0];
        }
        public override apobject make_copy()
        {
            rbf3evaluator _result = new rbf3evaluator();
            _result.n = n;
            _result.storagetype = storagetype;
            _result.f = (double[,])f.Clone();
            _result.nx = nx;
            _result.functype = functype;
            _result.funcparam = funcparam;
            _result.chunksize = chunksize;
            _result.entireset = (int[])entireset.Clone();
            _result.x = (double[,])x.Clone();
            _result.xtchunked = (double[,])xtchunked.Clone();
            _result.bufferpool = (smp.shared_pool)bufferpool.make_copy();
            _result.chunk1 = (double[])chunk1.Clone();
            return _result;
        }
    };


    /*************************************************************************
    Buffer object  which  is  used  to  perform  evaluation  requests  in  the
    multithreaded mode (multiple threads working with same RBF object).
    *************************************************************************/
    public class rbfv3calcbuffer : apobject
    {
        public double[] x;
        public rbf3evaluatorbuffer evalbuf;
        public double[] x123;
        public double[] y123;
        public double[,] x2d;
        public double[,] y2d;
        public double[] xg;
        public double[] yg;
        public rbfv3calcbuffer()
        {
            init();
        }
        public override void init()
        {
            x = new double[0];
            evalbuf = new rbf3evaluatorbuffer();
            x123 = new double[0];
            y123 = new double[0];
            x2d = new double[0, 0];
            y2d = new double[0, 0];
            xg = new double[0];
            yg = new double[0];
        }
        public override apobject make_copy()
        {
            rbfv3calcbuffer _result = new rbfv3calcbuffer();
            _result.x = (double[])x.Clone();
            _result.evalbuf = (rbf3evaluatorbuffer)evalbuf.make_copy();
            _result.x123 = (double[])x123.Clone();
            _result.y123 = (double[])y123.Clone();
            _result.x2d = (double[,])x2d.Clone();
            _result.y2d = (double[,])y2d.Clone();
            _result.xg = (double[])xg.Clone();
            _result.yg = (double[])yg.Clone();
            return _result;
        }
    };


    /*************************************************************************
    Approximate Cardinal Basis Function builder object

    Following fields store problem formulation:
    * NTotal                -   total points count in the dataset
    * NX                    -   dimensions count
    * XX                    -   array[NTotal,NX], points
    * FuncType              -   basis function type
    * FuncParam             -   basis function parameter
    * RoughDatasetDiameter  -   a rough upper bound on the dataset diameter

    Following global parameters are set:
    * NGlobal               -   global nodes count, >=0
    * GlobalGrid            -   global nodes
    * GlobalGridSeparation  -   maximum distance between any pair of grid nodes;
                                also an upper bound on distance between any random
                                point in the dataset and a nearest grid node
    * NLocal                -   number of nearest neighbors select for each
                                node.
    * NCorrection           -   nodes count for each corrector layer
    * CorrectorGrowth       -   growth factor for corrector layer
    * BatchSize             -   batch size for ACBF construction
    * LambdaV               -   smoothing coefficient, LambdaV>=0
    * ATerm                 -   linear term for basis functions:
                                * 1 = linear polynomial        (STRONGLY RECOMMENDED)
                                * 2 = constant polynomial term (may break convergence for thin plate splines)
                                * 3 = zero polynomial term     (may break convergence for all types of splines)

    Following fields are initialized:
    * KDT                   -   KD-tree search structure for the entire dataset
    * KDT1, KDT2            -   simplified KD-trees (build with progressively
                                sparsified dataset)
    * BufferPool            -   shared pool for ACBFBuffer instances
    * ChunksProducer        -   shared pool seeded with an instance of ACBFChunk
                                object (several rows of the preconditioner)
    * ChunksPool            -   shared pool that contains computed preconditioner
                                chunks as recycled entries

    Temporaries:
    * WrkIdx
    *************************************************************************/
    public class acbfbuilder : apobject
    {
        public bool dodetailedtrace;
        public int ntotal;
        public int nx;
        public double[,] xx;
        public int functype;
        public double funcparam;
        public double roughdatasetdiameter;
        public int nglobal;
        public int[] globalgrid;
        public double globalgridseparation;
        public int nlocal;
        public int ncorrection;
        public double correctorgrowth;
        public int batchsize;
        public double lambdav;
        public int aterm;
        public nearestneighbor.kdtree kdt;
        public nearestneighbor.kdtree kdt1;
        public nearestneighbor.kdtree kdt2;
        public smp.shared_pool bufferpool;
        public smp.shared_pool chunksproducer;
        public smp.shared_pool chunkspool;
        public int[] wrkidx;
        public acbfbuilder()
        {
            init();
        }
        public override void init()
        {
            xx = new double[0, 0];
            globalgrid = new int[0];
            kdt = new nearestneighbor.kdtree();
            kdt1 = new nearestneighbor.kdtree();
            kdt2 = new nearestneighbor.kdtree();
            bufferpool = new smp.shared_pool();
            chunksproducer = new smp.shared_pool();
            chunkspool = new smp.shared_pool();
            wrkidx = new int[0];
        }
        public override apobject make_copy()
        {
            acbfbuilder _result = new acbfbuilder();
            _result.dodetailedtrace = dodetailedtrace;
            _result.ntotal = ntotal;
            _result.nx = nx;
            _result.xx = (double[,])xx.Clone();
            _result.functype = functype;
            _result.funcparam = funcparam;
            _result.roughdatasetdiameter = roughdatasetdiameter;
            _result.nglobal = nglobal;
            _result.globalgrid = (int[])globalgrid.Clone();
            _result.globalgridseparation = globalgridseparation;
            _result.nlocal = nlocal;
            _result.ncorrection = ncorrection;
            _result.correctorgrowth = correctorgrowth;
            _result.batchsize = batchsize;
            _result.lambdav = lambdav;
            _result.aterm = aterm;
            _result.kdt = (nearestneighbor.kdtree)kdt.make_copy();
            _result.kdt1 = (nearestneighbor.kdtree)kdt1.make_copy();
            _result.kdt2 = (nearestneighbor.kdtree)kdt2.make_copy();
            _result.bufferpool = (smp.shared_pool)bufferpool.make_copy();
            _result.chunksproducer = (smp.shared_pool)chunksproducer.make_copy();
            _result.chunkspool = (smp.shared_pool)chunkspool.make_copy();
            _result.wrkidx = (int[])wrkidx.Clone();
            return _result;
        }
    };


    /*************************************************************************
    Temporary buffers used by divide-and-conquer ACBF preconditioner.

    This structure is initialized at the beginning of DC  procedure  and  put
    into shared pool. Basecase handling routine retrieves it  from  the  bool
    and returns back.

    Following fields can be used:
    * bFlags        -   boolean array[N], all values are set to False on  the
                        retrieval, and MUST  be  False  when  the  buffer  is
                        returned to the pool
    * KDTBuf        -   KD-tree request buffer for thread-safe requests
    * KDT1Buf, KDT2Buf- buffers for simplified KD-trees

    Additional preallocated temporaries are provided:
    * tmpBoxMin     -   array[NX], no special properties
    * tmpBoxMax     -   array[NX], no special properties
    * TargetNodes   -   dynamically resized as needed
    *************************************************************************/
    public class acbfbuffer : apobject
    {
        public bool[] bflags;
        public nearestneighbor.kdtreerequestbuffer kdtbuf;
        public nearestneighbor.kdtreerequestbuffer kdt1buf;
        public nearestneighbor.kdtreerequestbuffer kdt2buf;
        public double[] tmpboxmin;
        public double[] tmpboxmax;
        public int[] currentnodes;
        public int[] neighbors;
        public int[] chosenneighbors;
        public double[] y;
        public double[] z;
        public double[] d;
        public double[,] atwrk;
        public double[,] xq;
        public double[,] q;
        public double[,] q1;
        public double[,] wrkq;
        public double[,] b;
        public double[,] c;
        public double[] choltmp;
        public double[] tau;
        public double[,] r;
        public int[] perm;
        public acbfbuffer()
        {
            init();
        }
        public override void init()
        {
            bflags = new bool[0];
            kdtbuf = new nearestneighbor.kdtreerequestbuffer();
            kdt1buf = new nearestneighbor.kdtreerequestbuffer();
            kdt2buf = new nearestneighbor.kdtreerequestbuffer();
            tmpboxmin = new double[0];
            tmpboxmax = new double[0];
            currentnodes = new int[0];
            neighbors = new int[0];
            chosenneighbors = new int[0];
            y = new double[0];
            z = new double[0];
            d = new double[0];
            atwrk = new double[0, 0];
            xq = new double[0, 0];
            q = new double[0, 0];
            q1 = new double[0, 0];
            wrkq = new double[0, 0];
            b = new double[0, 0];
            c = new double[0, 0];
            choltmp = new double[0];
            tau = new double[0];
            r = new double[0, 0];
            perm = new int[0];
        }
        public override apobject make_copy()
        {
            acbfbuffer _result = new acbfbuffer();
            _result.bflags = (bool[])bflags.Clone();
            _result.kdtbuf = (nearestneighbor.kdtreerequestbuffer)kdtbuf.make_copy();
            _result.kdt1buf = (nearestneighbor.kdtreerequestbuffer)kdt1buf.make_copy();
            _result.kdt2buf = (nearestneighbor.kdtreerequestbuffer)kdt2buf.make_copy();
            _result.tmpboxmin = (double[])tmpboxmin.Clone();
            _result.tmpboxmax = (double[])tmpboxmax.Clone();
            _result.currentnodes = (int[])currentnodes.Clone();
            _result.neighbors = (int[])neighbors.Clone();
            _result.chosenneighbors = (int[])chosenneighbors.Clone();
            _result.y = (double[])y.Clone();
            _result.z = (double[])z.Clone();
            _result.d = (double[])d.Clone();
            _result.atwrk = (double[,])atwrk.Clone();
            _result.xq = (double[,])xq.Clone();
            _result.q = (double[,])q.Clone();
            _result.q1 = (double[,])q1.Clone();
            _result.wrkq = (double[,])wrkq.Clone();
            _result.b = (double[,])b.Clone();
            _result.c = (double[,])c.Clone();
            _result.choltmp = (double[])choltmp.Clone();
            _result.tau = (double[])tau.Clone();
            _result.r = (double[,])r.Clone();
            _result.perm = (int[])perm.Clone();
            return _result;
        }
    };


    /*************************************************************************
    Several rows of the ACBF preconditioner
    *************************************************************************/
    public class acbfchunk : apobject
    {
        public int ntargetrows;
        public int ntargetcols;
        public int[] targetrows;
        public int[] targetcols;
        public double[,] s;
        public acbfchunk()
        {
            init();
        }
        public override void init()
        {
            targetrows = new int[0];
            targetcols = new int[0];
            s = new double[0, 0];
        }
        public override apobject make_copy()
        {
            acbfchunk _result = new acbfchunk();
            _result.ntargetrows = ntargetrows;
            _result.ntargetcols = ntargetcols;
            _result.targetrows = (int[])targetrows.Clone();
            _result.targetcols = (int[])targetcols.Clone();
            _result.s = (double[,])s.Clone();
            return _result;
        }
    };


    /*************************************************************************
    Temporary buffers used by divide-and-conquer DDM solver

    This structure is initialized at the beginning of DC  procedure  and  put
    into shared pool. Basecase handling routine retrieves it  from  the  pool
    and returns back.

    Following fields can be used:
    * bFlags        -   boolean array[N], all values are set to False on  the
                        retrieval, and MUST  be  False  when  the  buffer  is
                        returned to the pool
    * KDTBuf        -   KD-tree request buffer for thread-safe requests

    Additional preallocated temporaries are provided:
    * Idx2PrecCol   -   integer array[N+NX+1], no special properties
    * tmpBoxMin     -   array[NX], no special properties
    * tmpBoxMax     -   array[NX], no special properties
    *************************************************************************/
    public class rbf3ddmbuffer : apobject
    {
        public bool[] bflags;
        public int[] idx2preccol;
        public nearestneighbor.kdtreerequestbuffer kdtbuf;
        public double[] tmpboxmin;
        public double[] tmpboxmax;
        public rbf3ddmbuffer()
        {
            init();
        }
        public override void init()
        {
            bflags = new bool[0];
            idx2preccol = new int[0];
            kdtbuf = new nearestneighbor.kdtreerequestbuffer();
            tmpboxmin = new double[0];
            tmpboxmax = new double[0];
        }
        public override apobject make_copy()
        {
            rbf3ddmbuffer _result = new rbf3ddmbuffer();
            _result.bflags = (bool[])bflags.Clone();
            _result.idx2preccol = (int[])idx2preccol.Clone();
            _result.kdtbuf = (nearestneighbor.kdtreerequestbuffer)kdtbuf.make_copy();
            _result.tmpboxmin = (double[])tmpboxmin.Clone();
            _result.tmpboxmax = (double[])tmpboxmax.Clone();
            return _result;
        }
    };


    /*************************************************************************
    Subproblem for DDM algorithm, stores precomputed factorization and  other
    information.

    Following fields are set during construction:
    * IsValid       -   whether instance is valid subproblem or not
    * NTarget       -   number of target nodes in the subproblem, NTarget>=1
    * TargetNodes   -   array containing target node indexes
    * NWork         -   number of working nodes in the subproblem, NWork>=NTarget
    * WorkingNodes  -   array containing working node indexes
    * RegSystem     -   smoothed (regularized) working system
    * Decomposition -   decomposition type:
                        * 0 for LU
                        * 1 for regularized QR
    * WrkLU         -   NWork*NWork sized LU factorization of the subproblem
    * WrkP          -   pivots for the LU decomposition
    * WrkQ, WrkR    -   NWork*NWork sized matrices, factors of QR decomposition
                        of RegSystem. Due to regularization rows added, the
                        Q factor is actually an 2NWork*NWork matrix, but in
                        order to solve the system we need only leading NWork
                        rows, so the rest is not stored.
    *************************************************************************/
    public class rbf3ddmsubproblem : apobject
    {
        public bool isvalid;
        public int ntarget;
        public int[] targetnodes;
        public int nwork;
        public int[] workingnodes;
        public double[,] regsystem;
        public int decomposition;
        public double[,] wrklu;
        public double[,] rhs;
        public double[,] qtrhs;
        public double[,] sol;
        public double[,] pred;
        public int[] wrkp;
        public double[,] wrkq;
        public double[,] wrkr;
        public rbf3ddmsubproblem()
        {
            init();
        }
        public override void init()
        {
            targetnodes = new int[0];
            workingnodes = new int[0];
            regsystem = new double[0, 0];
            wrklu = new double[0, 0];
            rhs = new double[0, 0];
            qtrhs = new double[0, 0];
            sol = new double[0, 0];
            pred = new double[0, 0];
            wrkp = new int[0];
            wrkq = new double[0, 0];
            wrkr = new double[0, 0];
        }
        public override apobject make_copy()
        {
            rbf3ddmsubproblem _result = new rbf3ddmsubproblem();
            _result.isvalid = isvalid;
            _result.ntarget = ntarget;
            _result.targetnodes = (int[])targetnodes.Clone();
            _result.nwork = nwork;
            _result.workingnodes = (int[])workingnodes.Clone();
            _result.regsystem = (double[,])regsystem.Clone();
            _result.decomposition = decomposition;
            _result.wrklu = (double[,])wrklu.Clone();
            _result.rhs = (double[,])rhs.Clone();
            _result.qtrhs = (double[,])qtrhs.Clone();
            _result.sol = (double[,])sol.Clone();
            _result.pred = (double[,])pred.Clone();
            _result.wrkp = (int[])wrkp.Clone();
            _result.wrkq = (double[,])wrkq.Clone();
            _result.wrkr = (double[,])wrkr.Clone();
            return _result;
        }
    };


    /*************************************************************************
    DDM solver

    Following fields store information about problem:
        LambdaV     -   smoothing coefficient
        
    Following fields related to DDM part are present:
        SubproblemsCnt-  number of subproblems created, SubproblemCnt>=1
        SubproblemsPool- shared pool seeded with instance of RBFV3DDMSubproblem
                        class (default seed has Seed.IsValid=False).  It  also
                        contains exactly SubproblemCnt subproblem instances as
                        recycled  entries,  each  of   these   instances   has
                        Seed.IsValid=True and  contains  a  partition  of  the
                        complete  problem  into  subproblems  and  precomputed
                        factorization
        SubproblemsBuffer-shared pool seeded with instance of RBFV3DDMSubproblem
                        class (default seed has Seed.IsValid=False).  Contains
                        no recycled entries, should be used just for temporary
                        storage of the already processed subproblems.
                        
    Following fields store information about corrector spline:
        NCorrector  -   corrector nodes count, NCorrector>0
        CorrQ       -   Q factor from the QR decomposition of the corrector
                        linear system, array[NCorrector,NCorrector]
        CorrR       -   R factor from the QR decomposition of the corrector
                        linear system, array[NCorrector,NCorrector]
        CorrNodes   -   array[NCorrector], indexes of dataset nodes chosen
                        for the corrector spline
        CorrX       -   array[NCorrector,NX], dataset points

    Following fields store information that is used for logging and testing:
        CntLU       -   number of subproblems solved with LU (well conditioned)
        CntRegQR    -   number of subproblems solved with Reg-QR (badly conditioned)    
    *************************************************************************/
    public class rbf3ddmsolver : apobject
    {
        public double lambdav;
        public nearestneighbor.kdtree kdt;
        public smp.shared_pool bufferpool;
        public int subproblemscnt;
        public smp.shared_pool subproblemspool;
        public smp.shared_pool subproblemsbuffer;
        public int ncorrector;
        public double[,] corrq;
        public double[,] corrr;
        public int[] corrnodes;
        public double[,] corrx;
        public double[,] tmpres1;
        public double[,] tmpupd1;
        public int cntlu;
        public int cntregqr;
        public rbf3ddmsolver()
        {
            init();
        }
        public override void init()
        {
            kdt = new nearestneighbor.kdtree();
            bufferpool = new smp.shared_pool();
            subproblemspool = new smp.shared_pool();
            subproblemsbuffer = new smp.shared_pool();
            corrq = new double[0, 0];
            corrr = new double[0, 0];
            corrnodes = new int[0];
            corrx = new double[0, 0];
            tmpres1 = new double[0, 0];
            tmpupd1 = new double[0, 0];
        }
        public override apobject make_copy()
        {
            rbf3ddmsolver _result = new rbf3ddmsolver();
            _result.lambdav = lambdav;
            _result.kdt = (nearestneighbor.kdtree)kdt.make_copy();
            _result.bufferpool = (smp.shared_pool)bufferpool.make_copy();
            _result.subproblemscnt = subproblemscnt;
            _result.subproblemspool = (smp.shared_pool)subproblemspool.make_copy();
            _result.subproblemsbuffer = (smp.shared_pool)subproblemsbuffer.make_copy();
            _result.ncorrector = ncorrector;
            _result.corrq = (double[,])corrq.Clone();
            _result.corrr = (double[,])corrr.Clone();
            _result.corrnodes = (int[])corrnodes.Clone();
            _result.corrx = (double[,])corrx.Clone();
            _result.tmpres1 = (double[,])tmpres1.Clone();
            _result.tmpupd1 = (double[,])tmpupd1.Clone();
            _result.cntlu = cntlu;
            _result.cntregqr = cntregqr;
            return _result;
        }
    };


    /*************************************************************************
    RBF model.

    Never try to work with fields of this object directly - always use  ALGLIB
    functions to use this object.
    *************************************************************************/
    public class rbfv3model : apobject
    {
        public int ny;
        public int nx;
        public int bftype;
        public double bfparam;
        public double[] s;
        public double[,] v;
        public double[] cw;
        public int[] pointindexes;
        public int nc;
        public rbf3evaluator evaluator;
        public rbf3fastevaluator fasteval;
        public double[,] wchunked;
        public rbfv3calcbuffer calcbuf;
        public bool dbgregqrusedforddm;
        public double dbgworstfirstdecay;
        public rbfv3model()
        {
            init();
        }
        public override void init()
        {
            s = new double[0];
            v = new double[0, 0];
            cw = new double[0];
            pointindexes = new int[0];
            evaluator = new rbf3evaluator();
            fasteval = new rbf3fastevaluator();
            wchunked = new double[0, 0];
            calcbuf = new rbfv3calcbuffer();
        }
        public override apobject make_copy()
        {
            rbfv3model _result = new rbfv3model();
            _result.ny = ny;
            _result.nx = nx;
            _result.bftype = bftype;
            _result.bfparam = bfparam;
            _result.s = (double[])s.Clone();
            _result.v = (double[,])v.Clone();
            _result.cw = (double[])cw.Clone();
            _result.pointindexes = (int[])pointindexes.Clone();
            _result.nc = nc;
            _result.evaluator = (rbf3evaluator)evaluator.make_copy();
            _result.fasteval = (rbf3fastevaluator)fasteval.make_copy();
            _result.wchunked = (double[,])wchunked.Clone();
            _result.calcbuf = (rbfv3calcbuffer)calcbuf.make_copy();
            _result.dbgregqrusedforddm = dbgregqrusedforddm;
            _result.dbgworstfirstdecay = dbgworstfirstdecay;
            return _result;
        }
    };


    /*************************************************************************
    RBF solution report:
    * TerminationType   -   termination type, positive values - success,
                            non-positive - failure.
    *************************************************************************/
    public class rbfv3report : apobject
    {
        public int terminationtype;
        public double maxerror;
        public double rmserror;
        public int iterationscount;
        public rbfv3report()
        {
            init();
        }
        public override void init()
        {
        }
        public override apobject make_copy()
        {
            rbfv3report _result = new rbfv3report();
            _result.terminationtype = terminationtype;
            _result.maxerror = maxerror;
            _result.rmserror = rmserror;
            _result.iterationscount = iterationscount;
            return _result;
        }
    };




    public const double epsred = 0.999999;
    public const int maxddmits = 25;
    public const double polyharmonic2scale = 4.0;
    public const int acbfparallelthreshold = 512;
    public const int ddmparallelthreshold = 512;
    public const int bfparallelthreshold = 512;
    public const int defaultmaxpanelsize = 128;
    public const int maxcomputebatchsize = 128;
    public const int minfarfieldsize = 256;
    public const int biharmonicseriesmax = 15;
    public const int farfieldnone = -1;
    public const int farfieldbiharmonic = 1;
    public const double defaultfastevaltol = 1.0E-3;
    public const bool userelaxederrorestimates = true;


    /*************************************************************************
    This function creates RBF  model  for  a  scalar (NY=1)  or  vector (NY>1)
    function in a NX-dimensional space (NX>=1).

    INPUT PARAMETERS:
        NX      -   dimension of the space, NX>=1
        NY      -   function dimension, NY>=1
        BF      -   basis function type:
                    * 1 for biharmonic/multiquadric f=sqrt(r^2+alpha^2) (with f=r being a special case)
                    * 2 for polyharmonic f=r^2*ln(r)
        BFP     -   basis function parameter:
                    * BF=0      parameter ignored

    OUTPUT PARAMETERS:
        S       -   RBF model (initially equals to zero)

      -- ALGLIB --
         Copyright 13.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv3create(int nx,
        int ny,
        int bf,
        double bfp,
        rbfv3model s,
        xparams _params)
    {
        ap.assert(nx >= 1, "RBFCreate: NX<1");
        ap.assert(ny >= 1, "RBFCreate: NY<1");
        ap.assert(bf == 1 || bf == 2, "RBFCreate: unsupported basis function type");
        ap.assert(math.isfinite(bfp) && (double)(bfp) >= (double)(0), "RBFCreate: infinite or negative basis function parameter");

        //
        // Serializable parameters
        //
        s.nx = nx;
        s.ny = ny;
        s.bftype = bf;
        s.bfparam = bfp;
        s.nc = 0;
        ablasf.rsetallocv(nx, 1.0, ref s.s, _params);
        ablasf.rsetallocm(ny, nx + 1, 0.0, ref s.v, _params);
        allocatecalcbuffer(s, s.calcbuf, _params);

        //
        // Debug counters
        //
        s.dbgregqrusedforddm = false;
        s.dbgworstfirstdecay = 0.0;
    }


    /*************************************************************************
    This function creates buffer  structure  which  can  be  used  to  perform
    parallel  RBF  model  evaluations  (with  one  RBF  model  instance  being
    used from multiple threads, as long as  different  threads  use  different
    instances of buffer).

    This buffer object can be used with  rbftscalcbuf()  function  (here  "ts"
    stands for "thread-safe", "buf" is a suffix which denotes  function  which
    reuses previously allocated output space).

    How to use it:
    * create RBF model structure with rbfcreate()
    * load data, tune parameters
    * call rbfbuildmodel()
    * call rbfcreatecalcbuffer(), once per thread working with RBF model  (you
      should call this function only AFTER call to rbfbuildmodel(), see  below
      for more information)
    * call rbftscalcbuf() from different threads,  with  each  thread  working
      with its own copy of buffer object.

    INPUT PARAMETERS
        S           -   RBF model

    OUTPUT PARAMETERS
        Buf         -   external buffer.
        
        
    IMPORTANT: buffer object should be used only with  RBF model object  which
               was used to initialize buffer. Any attempt to use buffer   with
               different object is dangerous - you may  get  memory  violation
               error because sizes of internal arrays do not fit to dimensions
               of RBF structure.
               
    IMPORTANT: you  should  call  this function only for model which was built
               with rbfbuildmodel() function, after successful  invocation  of
               rbfbuildmodel().  Sizes   of   some   internal  structures  are
               determined only after model is built, so buffer object  created
               before model  construction  stage  will  be  useless  (and  any
               attempt to use it will result in exception).

      -- ALGLIB --
         Copyright 02.04.2022 by Sergey Bochkanov
    *************************************************************************/
    public static void rbfv3createcalcbuffer(rbfv3model s,
        rbfv3calcbuffer buf,
        xparams _params)
    {
        allocatecalcbuffer(s, buf, _params);
    }


    /*************************************************************************
    This function builds hierarchical RBF model.

    INPUT PARAMETERS:
        X       -   array[N,S.NX], X-values
        Y       -   array[N,S.NY], Y-values
        ScaleVec-   array[S.NX], vector of per-dimension scales
        N       -   points count
        BFtype  -   basis function type:
                    * 1 for biharmonic spline f=r or multiquadric f=sqrt(r^2+param^2)
                    * 2 for thin plate spline f=r^2*ln(r)
        BFParam -   for BFType=1 zero value means biharmonic, nonzero means multiquadric
                    ignored for BFType=2
        LambdaV -   regularization parameter
        ATerm   -   polynomial term type:
                    * 1 for linear term (STRONGLY RECOMMENDED)
                    * 2 for constant term (may break convergence guarantees for thin plate splines)
                    * 3 for zero term (may break convergence guarantees for all types of splines)
        RBFProfile- RBF profile to use:
                    *  0 for the 'standard' profile
                    * -1 for the 'debug' profile intended to test all possible code branches even
                         on small-scale problems. The idea is to choose very small batch sizes and
                         threshold values, such that small problems with N=100..200 can test all
                         nested levels of the algorithm.
        TOL     -   desired relative accuracy:
                    * should between 1E-3 and 1E-6
                    * values higher than 1E-3 usually make no sense (bad accuracy, no performance benefits)
                    * values below 1E-6 may result in algorithm taking too much time,
                      so we silently override them to 1.0E-6
        S       -   RBF model, already initialized by RBFCreate() call.
        progress10000- variable used for progress reports, it is regularly set
                    to the current progress multiplied by 10000, in order to
                    get value in [0,10000] range. The rationale for such scaling
                    is that it allows us to use integer type to store progress,
                    which has less potential for non-atomic corruption on unprotected
                    reads from another threads.
                    You can read this variable from some other thread to get
                    estimate of the current progress.
                    Initial value of this variable is ignored, it is written by
                    this function, but not read.
        terminationrequest - variable used for termination requests; its initial
                    value must be False, and you can set it to True from some
                    other thread. This routine regularly checks this variable
                    and will terminate model construction shortly upon discovering
                    that termination was requested.
        
    OUTPUT PARAMETERS:
        S       -   updated model (for rep.terminationtype>0, unchanged otherwise)
        Rep     -   report:
                    * Rep.TerminationType:
                      *  1 - successful termination
                      *  8 terminated by user via rbfrequesttermination()
                    Fields are used for debugging purposes:
                    * Rep.IterationsCount - iterations count of the GMRES solver

    NOTE:  failure  to  build  model will leave current state of the structure
    unchanged.

      -- ALGLIB --
         Copyright 12.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv3build(double[,] xraw,
        double[,] yraw,
        int nraw,
        double[] scaleraw,
        int bftype,
        double bfparamraw,
        double lambdavraw,
        int aterm,
        int rbfprofile,
        double tol,
        rbfv3model s,
        ref int progress10000,
        ref bool terminationrequest,
        rbfv3report rep,
        xparams _params)
    {
        double fastevaltol = 0;
        int n = 0;
        int nx = 0;
        int ny = 0;
        double bfparamscaled = 0;
        double lambdavwrk = 0;
        double rescaledby = 0;
        double mergetol = 0;
        int matrixformat = 0;
        int acbfbatch = 0;
        int acbfglobal = 0;
        int acbflocal = 0;
        int acbfcorrection = 0;
        int ddmbatch = 0;
        int ddmneighbors = 0;
        int ddmcoarse = 0;
        int maxpanelsize = 0;
        double[,] xscaled = new double[0, 0];
        double[,] yscaled = new double[0, 0];
        double[,] xcoarse = new double[0, 0];
        double[,] x1t = new double[0, 0];
        rbf3evaluator bfmatrix = new rbf3evaluator();
        rbf3fastevaluator fasteval = new rbf3fastevaluator();
        double[] b = new double[0];
        double[] x0 = new double[0];
        double[] x1 = new double[0];
        double[] y0 = new double[0];
        double[] y1 = new double[0];
        double[] sft = new double[0];
        double[] scalewrk = new double[0];
        double[,] c2 = new double[0, 0];
        double[,] res = new double[0, 0];
        double[,] upd0 = new double[0, 0];
        double[,] upd1 = new double[0, 0];
        double[,] ortbasis = new double[0, 0];
        int ortbasissize = 0;
        int[] raw2wrkmap = new int[0];
        int[] wrk2rawmap = new int[0];
        int[] idummy = new int[0];
        sparse.sparsematrix sp = new sparse.sparsematrix();
        iterativesparse.sparsesolverstate ss = new iterativesparse.sparsesolverstate();
        directsparsesolvers.sparsesolverreport ssrep = new directsparsesolvers.sparsesolverreport();
        rbf3ddmsolver ddmsolver = new rbf3ddmsolver();
        double resnrm = 0;
        double res0nrm = 0;
        int iteridx = 0;
        int yidx = 0;
        bool dotrace = new bool();
        bool dodetailedtrace = new bool();
        fbls.fblsgmresstate gmressolver = new fbls.fblsgmresstate();
        double orterr = 0;
        double l1nrm = 0;
        double linfnrm = 0;
        int timeprec = 0;
        int timedesign = 0;
        int timeddminit = 0;
        int timeddmsolve = 0;
        int timecorrinit = 0;
        int timecorrsolve = 0;
        int timereeval = 0;
        int timetotal = 0;
        apserv.savgcounter dbgfarfieldspeedup = new apserv.savgcounter();
        int i = 0;
        int j = 0;
        int k = 0;
        double v = 0;
        double vv = 0;
        double[,] refrhs = new double[0, 0];
        double[] refrhs1 = new double[0];
        double[] refsol1 = new double[0];
        double debugdamping = 0;

        mergetol = 1000 * math.machineepsilon;
        ap.assert(math.isfinite(tol), "RBFV3Build: incorrect TOL");
        ap.assert(s.nx > 0, "RBFV3Build: incorrect NX");
        ap.assert(s.ny > 0, "RBFV3Build: incorrect NY");
        ap.assert((bftype == 1 || bftype == 2) || bftype == 3, "RBFV3Build: incorrect BFType");
        ap.assert((aterm == 1 || aterm == 2) || aterm == 3, "RBFV3Build: incorrect ATerm");
        ap.assert((rbfprofile == -2 || rbfprofile == -1) || rbfprofile == 0, "RBFV3Build: incorrect RBFProfile");
        for (j = 0; j <= s.nx - 1; j++)
        {
            ap.assert((double)(scaleraw[j]) > (double)(0), "RBFV2BuildHierarchical: incorrect ScaleVec");
        }
        nx = s.nx;
        ny = s.ny;
        bfparamscaled = bfparamraw;

        //
        // Trace output (if needed)
        //
        dotrace = ap.istraceenabled("RBF", _params);
        dodetailedtrace = dotrace && ap.istraceenabled("RBF.DETAILED", _params);
        if (dotrace)
        {
            ap.trace("\n\n");
            ap.trace("////////////////////////////////////////////////////////////////////////////////////////////////////\n");
            ap.trace("// DDM-RBF builder started                                                                        //\n");
            ap.trace("////////////////////////////////////////////////////////////////////////////////////////////////////\n");
        }

        //
        // Clean up communication and report fields
        //
        progress10000 = 0;
        rep.maxerror = 0;
        rep.rmserror = 0;
        rep.iterationscount = 0;
        timeprec = 0;
        timedesign = 0;
        timeddminit = 0;
        timeddmsolve = 0;
        timecorrinit = 0;
        timecorrsolve = 0;
        timereeval = 0;
        timetotal = 0 - unchecked((int)(System.DateTime.UtcNow.Ticks / 10000));
        s.dbgregqrusedforddm = false;
        s.dbgworstfirstdecay = 0.0;
        apserv.savgcounterinit(dbgfarfieldspeedup, 0.0, _params);

        //
        // Quick exit when we have no points
        //
        if (nraw == 0)
        {
            zerofill(s, nx, ny, _params);
            rep.terminationtype = 1;
            progress10000 = 10000;
            return;
        }

        //
        // Preprocess dataset (scale points, merge nondistinct ones)
        //
        preprocessdataset(xraw, mergetol, yraw, scaleraw, nraw, nx, ny, bftype, bfparamraw, lambdavraw, ref xscaled, ref yscaled, ref raw2wrkmap, ref wrk2rawmap, ref n, ref scalewrk, ref sft, ref bfparamscaled, ref lambdavwrk, ref rescaledby, _params);
        ablasf.rallocm(nx + 1, n, ref x1t, _params);
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= nx - 1; j++)
            {
                x1t[j, i] = xscaled[i, j];
            }
            x1t[nx, i] = 1.0;
        }

        //
        // Set algorithm parameters according to the current profile
        //
        ap.assert((rbfprofile == -2 || rbfprofile == -1) || rbfprofile == 0, "RBFV3Build: incorrect RBFProfile");
        if (dotrace)
        {
            ap.trace("=== PRINTING ALGORITHM SETTINGS ====================================================================\n");
            ap.trace(System.String.Format("TOL         = {0,0:E2}\nPROFILE     = {1,0:d}\n", tol, rbfprofile));
        }
        tol = Math.Max(tol, 1.0E-6);
        acbfglobal = 0;
        acbflocal = Math.Max((int)Math.Round(Math.Pow(5.0, nx)), 25);
        acbfcorrection = (int)Math.Round(Math.Pow(5, nx));
        acbfbatch = 32;
        ddmneighbors = 0;
        ddmbatch = apserv.imin2(1000, n, _params);
        ddmcoarse = apserv.imin3((int)Math.Round(0.1 * n + 10), 2048, n, _params);
        maxpanelsize = defaultmaxpanelsize;
        debugdamping = 0.0;
        if (rbfprofile == -1 || rbfprofile == -2)
        {

            //
            // Decrease batch sizes and corrector efficiency.
            // Add debug damping which produces suboptimal ACBF basis.
            //
            if (dotrace)
            {
                ap.trace("> debug profile activated\n");
            }
            acbfbatch = 16;
            ddmneighbors = 3;
            ddmbatch = 16;
            ddmcoarse = apserv.imin3((int)Math.Round(0.05 * n + 2), 512, n, _params);
            maxpanelsize = 16;
            if (rbfprofile == -2)
            {
                debugdamping = 0.000001;
            }
        }

        //
        // Prepare fast evaluator
        //
        // NOTE: we set fast evaluation tolerance to TOL. Actually, it is better to have it somewhat below
        //       TOL, e.g. TOL/10 or TOL/100. However, we rely on the fact that fast evaluator error estimate
        //       is inherently pessimistic, i.e. actual evaluation accuracy is better than that.
        //
        fastevaltol = tol;
        fastevaluatorinit(fasteval, xscaled, n, nx, 1, maxpanelsize, bftype, bfparamscaled, dotrace, _params);

        //
        // Compute design matrix
        //
        matrixformat = 1;
        if (dotrace)
        {
            ap.trace("=== MODEL MATRIX INITIALIZATION STARTED ============================================================\n");
            ap.trace(System.String.Format("N           = {0,0:d}\nNX          = {1,0:d}\nNY          = {2,0:d}\n", n, nx, ny));
            ap.trace(System.String.Format("BFType      = {0,0:d}", bftype));
            if (bftype == 1 && (double)(bfparamraw) > (double)(0))
            {
                ap.trace(System.String.Format("  ( f=sqrt(r^2+alpha^2), alpha={0,0:F3}, multiquadric with manual radius)", bfparamraw));
            }
            if (bftype == 1 && (double)(bfparamraw) == (double)(0))
            {
                ap.trace("  ( f=r, biharmonic spline )");
            }
            if (bftype == 1 && (double)(bfparamraw) < (double)(0))
            {
                ap.trace(System.String.Format("  ( f=sqrt(r^2+alpha^2), alpha=AUTO*{0,0:F3}={1,0:E2}, multiquadric )", -bfparamraw, bfparamscaled));
            }
            if (bftype == 2)
            {
                ap.trace("  ( f=log(r)*r^2, thin plate spline )");
            }
            if (bftype == 3)
            {
                ap.trace("  ( f=r^3 )");
            }
            ap.trace("\n");
            ap.trace(System.String.Format("Polinom.term= {0,0:d} ", aterm));
            if (aterm == 1)
            {
                ap.trace("(linear term)");
            }
            if (aterm == 2)
            {
                ap.trace("(constant term)");
            }
            if (aterm == 3)
            {
                ap.trace("(zero term)");
            }
            ap.trace("\n");
            ap.trace(System.String.Format("LambdaV     = {0,0:E2} (raw value of the smoothing parameter; effective value after adjusting for data spread is {1,0:E2})\n", lambdavraw, lambdavwrk));
            ap.trace("VarScales   = ");
            apserv.tracevectore3(scaleraw, 0, nx, _params);
            ap.trace(" (raw values of variable scales)\n");
        }
        timedesign = timedesign - unchecked((int)(System.DateTime.UtcNow.Ticks / 10000));
        modelmatrixinit(xscaled, n, nx, bftype, bfparamscaled, matrixformat, bfmatrix, _params);
        timedesign = timedesign + unchecked((int)(System.DateTime.UtcNow.Ticks / 10000));
        if (dotrace)
        {
            ap.trace(System.String.Format("> model matrix initialized in {0,0:d} ms\n", timedesign));
        }

        //
        // Build orthogonal basis of the subspace spanned by polynomials of 1st degree.
        // This basis is used later to check orthogonality conditions for the coefficients.
        //
        ablasf.rallocm(nx + 1, n, ref ortbasis, _params);
        ablasf.rsetr(n, 1 / Math.Sqrt(n), ortbasis, 0, _params);
        ortbasissize = 1;
        ablasf.rallocv(n, ref x0, _params);
        for (k = 0; k <= nx - 1; k++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                x0[j] = xscaled[j, k];
            }
            v = Math.Sqrt(ablasf.rdotv2(n, x0, _params));
            ablas.rowwisegramschmidt(ortbasis, ortbasissize, n, x0, ref x0, false, _params);
            vv = Math.Sqrt(ablasf.rdotv2(n, x0, _params));
            if ((double)(vv) > (double)(Math.Sqrt(math.machineepsilon) * (v + 1)))
            {
                ablasf.rcopymulvr(n, 1 / vv, x0, ortbasis, ortbasissize, _params);
                ortbasissize = ortbasissize + 1;
            }
        }

        //
        // Build preconditioner
        //
        if (dotrace)
        {
            ap.trace("=== PRECONDITIONER CONSTRUCTION STARTED ============================================================\n");
            ap.trace(System.String.Format("nglobal     = {0,0:d}\nnlocal      = {1,0:d}\nncorrection = {2,0:d}\nnbatch      = {3,0:d}\n", acbfglobal, acbflocal, acbfcorrection, acbfbatch));
        }
        timeprec = timeprec - unchecked((int)(System.DateTime.UtcNow.Ticks / 10000));
        computeacbfpreconditioner(xscaled, n, nx, bftype, bfparamscaled, aterm, acbfbatch, acbfglobal, acbflocal, acbfcorrection, 5, 2, lambdavwrk + debugdamping, sp, _params);
        timeprec = timeprec + unchecked((int)(System.DateTime.UtcNow.Ticks / 10000));
        if (dotrace)
        {
            ap.trace(System.String.Format("> ACBF preconditioner computed in {0,0:d} ms\n", timeprec));
        }

        //
        // DDM
        //
        if (dotrace)
        {
            ap.trace("=== DOMAIN DECOMPOSITION METHOD STARTED ============================================================\n");
        }
        ablasf.rsetallocm(n + nx + 1, ny, 0.0, ref c2, _params);
        if (dotrace)
        {
            ap.trace("> problem metrics and settings\n");
            ap.trace(System.String.Format("NNeighbors  = {0,0:d}\n", ddmneighbors));
            ap.trace(System.String.Format("NBatch      = {0,0:d}\n", ddmbatch));
            ap.trace(System.String.Format("NCoarse     = {0,0:d}\n", ddmcoarse));
        }
        ddmsolverinit(xscaled, rescaledby, n, nx, bfmatrix, bftype, bfparamscaled, lambdavwrk, aterm, sp, ddmneighbors, ddmbatch, ddmcoarse, dotrace, dodetailedtrace, ddmsolver, ref timeddminit, ref timecorrinit, _params);
        if (dotrace)
        {
            ap.trace(System.String.Format("> DDM initialization done in {0,0:d} ms, {1,0:d} subproblems solved ({2,0:d} well-conditioned, {3,0:d} ill-conditioned)\n", timeddminit, ddmsolver.subproblemscnt, ddmsolver.cntlu, ddmsolver.cntregqr));
        }

        //
        // Use preconditioned GMRES
        //
        rep.rmserror = 0;
        rep.maxerror = 0;
        rep.iterationscount = 0;
        for (yidx = 0; yidx <= ny - 1; yidx++)
        {
            if (dotrace)
            {
                ap.trace(System.String.Format("> solving for component {0,2:d}:\n", yidx));
            }
            ablasf.rsetallocv(n + nx + 1, 0.0, ref y0, _params);
            ablasf.rsetallocv(n + nx + 1, 0.0, ref y1, _params);
            ablasf.rcopycv(n, yscaled, yidx, y0, _params);
            res0nrm = Math.Sqrt(ablasf.rdotv2(n, y0, _params));
            fbls.fblsgmrescreate(y0, n, Math.Min(maxddmits, n), gmressolver, _params);
            gmressolver.epsres = tol;
            gmressolver.epsred = epsred;
            iteridx = 0;
            while (fbls.fblsgmresiteration(gmressolver, _params))
            {
                if (dotrace)
                {
                    ap.trace(System.String.Format(">> DDM iteration {0,2:d}: {1,0:E2} relative residual\n", iteridx, gmressolver.reprelres));
                }
                ablasf.rallocv(n + nx + 1, ref y0, _params);
                ablasf.rallocv(n + nx + 1, ref y1, _params);
                ddmsolverrun1(ddmsolver, gmressolver.x, n, nx, sp, bfmatrix, fasteval, fastevaltol, ref y0, ref timeddmsolve, ref timecorrsolve, _params);
                timereeval = timereeval - unchecked((int)(System.DateTime.UtcNow.Ticks / 10000));
                fastevaluatorloadcoeffs1(fasteval, y0, _params);
                fastevaluatorpushtol(fasteval, fastevaltol * res0nrm, _params);
                fastevaluatorcomputeall(fasteval, ref y1, _params);
                apserv.savgcounterenqueue(dbgfarfieldspeedup, math.sqr(fasteval.dbgpanelscnt) / apserv.coalesce(fasteval.dbgpanel2panelcnt + fasteval.dbgfield2panelcnt, 1, _params), _params);
                ablasf.rgemvx(n, nx + 1, 1.0, x1t, 0, 0, 1, y0, n, 1.0, y1, 0, _params);
                for (i = 0; i <= n - 1; i++)
                {
                    y1[i] = y1[i] + lambdavwrk * y0[i];
                }
                timereeval = timereeval + unchecked((int)(System.DateTime.UtcNow.Ticks / 10000));
                ablasf.rcopyv(n, y1, gmressolver.ax, _params);
                rep.iterationscount = rep.iterationscount + 1;
                if (iteridx == 1)
                {
                    s.dbgworstfirstdecay = Math.Max(gmressolver.reprelres, s.dbgworstfirstdecay);
                }
                iteridx = iteridx + 1;
            }
            ddmsolverrun1(ddmsolver, gmressolver.xs, n, nx, sp, bfmatrix, fasteval, fastevaltol, ref x1, ref timeddmsolve, ref timecorrsolve, _params);
            ap.assert(math.isfinite(ablasf.rdotv2(n + nx + 1, x1, _params)), "RBF3: integrity check 4359 failed");
            ablasf.rcopyvc(n + nx + 1, x1, c2, yidx, _params);

            //
            // Compute predictions and errors
            //
            // NOTE: because dataset preprocessing may reorder and merge points we have
            //       to use raw-to-work mapping in order to be able to compute correct
            //       error metrics.
            //
            timereeval = timereeval - unchecked((int)(System.DateTime.UtcNow.Ticks / 10000));
            fastevaluatorloadcoeffs1(fasteval, x1, _params);
            fastevaluatorpushtol(fasteval, fastevaltol * res0nrm, _params);
            fastevaluatorcomputeall(fasteval, ref y1, _params);
            ablasf.rgemvx(n, nx + 1, 1.0, x1t, 0, 0, 1, x1, n, 1.0, y1, 0, _params);
            timereeval = timereeval + unchecked((int)(System.DateTime.UtcNow.Ticks / 10000));
            resnrm = 0;
            for (i = 0; i <= n - 1; i++)
            {
                resnrm = resnrm + math.sqr(yscaled[i, yidx] - y1[i] - lambdavwrk * x1[i]);
            }
            resnrm = Math.Sqrt(resnrm);
            for (i = 0; i <= nraw - 1; i++)
            {
                v = yraw[i, yidx] - y1[raw2wrkmap[i]];
                rep.maxerror = Math.Max(rep.maxerror, Math.Abs(v));
                rep.rmserror = rep.rmserror + v * v;
            }
            if (dotrace)
            {
                ap.trace(System.String.Format(">> done with {0,0:E2} relative residual, GMRES completion code {1,0:d}\n", resnrm / apserv.coalesce(res0nrm, 1, _params), gmressolver.retcode));
            }
        }
        rep.rmserror = Math.Sqrt(rep.rmserror / (nraw * ny));
        timetotal = timetotal + unchecked((int)(System.DateTime.UtcNow.Ticks / 10000));
        if (dotrace)
        {
            ablasf.rallocv(n, ref y0, _params);
            orterr = 0;
            l1nrm = 0;
            linfnrm = 0;
            for (k = 0; k <= ny - 1; k++)
            {
                ablasf.rcopycv(n, c2, k, y0, _params);
                linfnrm = Math.Max(linfnrm, ablasf.rmaxabsv(n, y0, _params));
                for (i = 0; i <= n - 1; i++)
                {
                    l1nrm = l1nrm + Math.Abs(y0[i]);
                }
                for (i = 0; i <= ortbasissize - 1; i++)
                {
                    orterr = Math.Max(orterr, Math.Abs(ablasf.rdotvr(n, y0, ortbasis, i, _params)));
                }
            }
            ap.trace("=== PRINTING RBF SOLVER RESULTS ====================================================================\n");
            ap.trace("> errors\n");
            ap.trace(System.String.Format("RMS.err     = {0,0:E2}\n", rep.rmserror));
            ap.trace(System.String.Format("MAX.err     = {0,0:E2}\n", rep.maxerror));
            ap.trace(System.String.Format("ORT.err     = {0,0:E2} (orthogonality condition)\n", orterr));
            ap.trace("> solution statistics:\n");
            ap.trace(System.String.Format("L1-norm     = {0,0:E2}\n", l1nrm));
            ap.trace(System.String.Format("Linf-norm   = {0,0:E2}\n", linfnrm));
            ap.trace("> DDM iterations\n");
            ap.trace(System.String.Format("ItsCnt      = {0,0:d}\n", rep.iterationscount));
            ap.trace("> speedup due to far field expansions (ok to be 1.0x for datasets below 100K):\n");
            ap.trace(System.String.Format("reeval      = {0,0:F1}x (speed-up of the model reevaluation phase)\n", apserv.savgcounterget(dbgfarfieldspeedup, _params)));
            ap.trace(System.String.Format("overall     = {0,0:F1}x (overall speed-up)\n", (timetotal + timereeval * (apserv.savgcounterget(dbgfarfieldspeedup, _params) - 1)) / (timetotal + math.machineepsilon)));
            ap.trace(System.String.Format("> total running time is {0,0:d} ms, including:\n", timetotal));
            ap.trace(System.String.Format(">> model matrix generation               {0,8:d} ms\n", timedesign));
            ap.trace(System.String.Format(">> ACBF preconditioner construction      {0,8:d} ms\n", timeprec));
            ap.trace(System.String.Format(">> DDM solver initialization             {0,8:d} ms\n", timeddminit));
            ap.trace(System.String.Format(">> DDM corrector initialization          {0,8:d} ms\n", timecorrinit));
            ap.trace(System.String.Format(">> DDM solution phase                    {0,8:d} ms\n", timeddmsolve));
            ap.trace(System.String.Format(">> DDM correction phase                  {0,8:d} ms\n", timecorrsolve));
            ap.trace(System.String.Format(">> DDM solver model reevaluation         {0,8:d} ms\n", timereeval));
        }
        s.bftype = bftype;
        s.bfparam = bfparamscaled;
        ablasf.rcopyallocv(nx, scalewrk, ref s.s, _params);
        for (j = 0; j <= ny - 1; j++)
        {
            s.v[j, nx] = c2[n + nx, j];
            for (i = 0; i <= nx - 1; i++)
            {
                s.v[j, i] = c2[n + i, j] / scalewrk[i];
                s.v[j, nx] = s.v[j, nx] - c2[n + i, j] * sft[i] / scalewrk[i];
            }
        }
        ablasf.rallocv(n * (nx + ny), ref s.cw, _params);
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= nx - 1; j++)
            {
                s.cw[i * (nx + ny) + j] = xscaled[i, j] + sft[j] / scalewrk[j];
            }
            for (j = 0; j <= ny - 1; j++)
            {
                s.cw[i * (nx + ny) + nx + j] = c2[i, j];
            }
        }
        ablasf.icopyallocv(n, wrk2rawmap, ref s.pointindexes, _params);
        s.nc = n;
        createfastevaluator(s, _params);

        //
        // Set up debug fields
        //
        s.dbgregqrusedforddm = ddmsolver.cntregqr > 0;

        //
        // Update progress reports
        //
        rep.terminationtype = 1;
        progress10000 = 10000;
    }


    /*************************************************************************
    Serializer: allocation

      -- ALGLIB --
         Copyright 12.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv3alloc(serializer s,
        rbfv3model model,
        xparams _params)
    {

        //
        // Data
        //
        s.alloc_entry();
        s.alloc_entry();
        s.alloc_entry();
        s.alloc_entry();
        s.alloc_entry();
        apserv.allocrealarray(s, model.s, model.nx, _params);
        apserv.allocrealmatrix(s, model.v, model.ny, model.nx + 1, _params);
        apserv.allocrealarray(s, model.cw, model.nc * (model.nx + model.ny), _params);
        apserv.allocintegerarray(s, model.pointindexes, model.nc, _params);

        //
        // End of stream, no additional data
        //
        s.alloc_entry();
    }


    /*************************************************************************
    Serializer: serialization

      -- ALGLIB --
         Copyright 12.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv3serialize(serializer s,
        rbfv3model model,
        xparams _params)
    {

        //
        // Data
        //
        s.serialize_int(model.nx);
        s.serialize_int(model.ny);
        s.serialize_int(model.bftype);
        s.serialize_double(model.bfparam);
        s.serialize_int(model.nc);
        apserv.serializerealarray(s, model.s, model.nx, _params);
        apserv.serializerealmatrix(s, model.v, model.ny, model.nx + 1, _params);
        apserv.serializerealarray(s, model.cw, model.nc * (model.nx + model.ny), _params);
        apserv.serializeintegerarray(s, model.pointindexes, model.nc, _params);

        //
        // End of stream, no additional data
        //
        s.serialize_int(117256);
    }


    /*************************************************************************
    Serializer: unserialization

      -- ALGLIB --
         Copyright 12.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv3unserialize(serializer s,
        rbfv3model model,
        xparams _params)
    {
        int nx = 0;
        int ny = 0;
        int bftype = 0;
        int k = 0;
        double bfparam = 0;


        //
        // Unserialize primary model parameters, initialize model.
        //
        // It is necessary to call RBFCreate() because some internal fields
        // which are NOT unserialized will need initialization.
        //
        nx = s.unserialize_int();
        ny = s.unserialize_int();
        bftype = s.unserialize_int();
        bfparam = s.unserialize_double();
        rbfv3create(nx, ny, bftype, bfparam, model, _params);
        model.nc = s.unserialize_int();
        apserv.unserializerealarray(s, ref model.s, _params);
        apserv.unserializerealmatrix(s, ref model.v, _params);
        apserv.unserializerealarray(s, ref model.cw, _params);
        apserv.unserializeintegerarray(s, ref model.pointindexes, _params);

        //
        // End of stream, check that no additional data is present
        //
        k = s.unserialize_int();
        ap.assert(k == 117256, "RBFV3Unserialize: unexpected payload detected in the data stream. Integrity check failed");

        //
        // Finalize construction
        //
        createfastevaluator(model, _params);
    }


    /*************************************************************************
    This function calculates values of the RBF model in the given point.

    This function should be used when we have NY=1 (scalar function) and  NX=1
    (1-dimensional space).

    This function returns 0.0 when:
    * the model is not initialized
    * NX<>1
     *NY<>1

    INPUT PARAMETERS:
        S       -   RBF model
        X0      -   X-coordinate, finite number

    RESULT:
        value of the model or 0.0 (as defined above)

      -- ALGLIB --
         Copyright 12.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static double rbfv3calc1(rbfv3model s,
        double x0,
        xparams _params)
    {
        double result = 0;

        ap.assert(math.isfinite(x0), "RBFCalc1: invalid value for X0 (X0 is Inf)!");
        if (s.ny != 1 || s.nx != 1)
        {
            result = 0;
            return result;
        }
        result = s.v[0, 0] * x0 - s.v[0, 1];
        s.calcbuf.x123[0] = x0;
        rbfv3tscalcbuf(s, s.calcbuf, s.calcbuf.x123, ref s.calcbuf.y123, _params);
        result = s.calcbuf.y123[0];
        return result;
    }


    /*************************************************************************
    This function calculates values of the RBF model in the given point.

    This function should be used when we have NY=1 (scalar function) and  NX=2
    (2-dimensional space). If you have 3-dimensional space, use RBFCalc3(). If
    you have general situation (NX-dimensional space, NY-dimensional function)
    you should use general, less efficient implementation RBFCalc().

    If  you  want  to  calculate  function  values  many times, consider using 
    RBFGridCalc2(), which is far more efficient than many subsequent calls  to
    RBFCalc2().

    This function returns 0.0 when:
    * model is not initialized
    * NX<>2
     *NY<>1

    INPUT PARAMETERS:
        S       -   RBF model
        X0      -   first coordinate, finite number
        X1      -   second coordinate, finite number

    RESULT:
        value of the model or 0.0 (as defined above)

      -- ALGLIB --
         Copyright 12.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static double rbfv3calc2(rbfv3model s,
        double x0,
        double x1,
        xparams _params)
    {
        double result = 0;

        ap.assert(math.isfinite(x0), "RBFCalc2: invalid value for X0 (X0 is Inf)!");
        ap.assert(math.isfinite(x1), "RBFCalc2: invalid value for X1 (X1 is Inf)!");
        if (s.ny != 1 || s.nx != 2)
        {
            result = 0;
            return result;
        }
        result = s.v[0, 0] * x0 + s.v[0, 1] * x1 + s.v[0, 2];
        if (s.nc == 0)
        {
            return result;
        }
        s.calcbuf.x123[0] = x0;
        s.calcbuf.x123[1] = x1;
        rbfv3tscalcbuf(s, s.calcbuf, s.calcbuf.x123, ref s.calcbuf.y123, _params);
        result = s.calcbuf.y123[0];
        return result;
    }


    /*************************************************************************
    This function calculates values of the RBF model in the given point.

    This function should be used when we have NY=1 (scalar function) and  NX=3
    (3-dimensional space). If you have 2-dimensional space, use RBFCalc2(). If
    you have general situation (NX-dimensional space, NY-dimensional function)
    you should use general, less efficient implementation RBFCalc().

    This function returns 0.0 when:
    * model is not initialized
    * NX<>3
     *NY<>1

    INPUT PARAMETERS:
        S       -   RBF model
        X0      -   first coordinate, finite number
        X1      -   second coordinate, finite number
        X2      -   third coordinate, finite number

    RESULT:
        value of the model or 0.0 (as defined above)

      -- ALGLIB --
         Copyright 12.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static double rbfv3calc3(rbfv3model s,
        double x0,
        double x1,
        double x2,
        xparams _params)
    {
        double result = 0;

        ap.assert(math.isfinite(x0), "RBFCalc3: invalid value for X0 (X0 is Inf or NaN)!");
        ap.assert(math.isfinite(x1), "RBFCalc3: invalid value for X1 (X1 is Inf or NaN)!");
        ap.assert(math.isfinite(x2), "RBFCalc3: invalid value for X2 (X2 is Inf or NaN)!");
        if (s.ny != 1 || s.nx != 3)
        {
            result = 0;
            return result;
        }
        result = s.v[0, 0] * x0 + s.v[0, 1] * x1 + s.v[0, 2] * x2 + s.v[0, 3];
        if (s.nc == 0)
        {
            return result;
        }
        s.calcbuf.x123[0] = x0;
        s.calcbuf.x123[1] = x1;
        s.calcbuf.x123[2] = x2;
        rbfv3tscalcbuf(s, s.calcbuf, s.calcbuf.x123, ref s.calcbuf.y123, _params);
        result = s.calcbuf.y123[0];
        return result;
    }


    /*************************************************************************
    This function calculates values of the RBF model at the given point.

    Same as RBFCalc(), but does not reallocate Y when in is large enough to 
    store function values.

    INPUT PARAMETERS:
        S       -   RBF model
        X       -   coordinates, array[NX].
                    X may have more than NX elements, in this case only 
                    leading NX will be used.
        Y       -   possibly preallocated array

    OUTPUT PARAMETERS:
        Y       -   function value, array[NY]. Y is not reallocated when it
                    is larger than NY.

      -- ALGLIB --
         Copyright 13.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv3calcbuf(rbfv3model s,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        rbfv3tscalcbuf(s, s.calcbuf, x, ref y, _params);
    }


    /*************************************************************************
    This function calculates values of the RBF model at the given point, using
    external  buffer  object  (internal  temporaries  of  RBF  model  are  not
    modified).

    This function allows to use same RBF model object  in  different  threads,
    assuming  that  different   threads  use  different  instances  of  buffer
    structure.

    INPUT PARAMETERS:
        S       -   RBF model, may be shared between different threads
        Buf     -   buffer object created for this particular instance of  RBF
                    model with rbfcreatecalcbuffer().
        X       -   coordinates, array[NX].
                    X may have more than NX elements, in this case only 
                    leading NX will be used.
        Y       -   possibly preallocated array

    OUTPUT PARAMETERS:
        Y       -   function value, array[NY]. Y is not reallocated when it
                    is larger than NY.

      -- ALGLIB --
         Copyright 12.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv3tscalcbuf(rbfv3model s,
        rbfv3calcbuffer buf,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        int nx = 0;
        int ny = 0;
        int i = 0;
        int j = 0;
        double distance0 = 0;
        int colidx = 0;
        int srcidx = 0;
        int widx = 0;
        int curchunk = 0;

        ap.assert(ap.len(x) >= s.nx, "RBFV3TsCalcBuf: Length(X)<NX");
        ap.assert(apserv.isfinitevector(x, s.nx, _params), "RBFV3TsCalcBuf: X contains infinite or NaN values");
        nx = s.nx;
        ny = s.ny;

        //
        // Handle linear term
        //
        if (ap.len(y) < ny)
        {
            y = new double[ny];
        }
        for (i = 0; i <= ny - 1; i++)
        {
            y[i] = s.v[i, nx];
            for (j = 0; j <= nx - 1; j++)
            {
                y[i] = y[i] + s.v[i, j] * x[j];
            }
        }
        if (s.nc == 0)
        {
            return;
        }

        //
        // Handle RBF term
        //
        ap.assert((s.bftype == 1 || s.bftype == 2) || s.bftype == 3, "RBFV3TsCalcBuf: unsupported basis function type");
        for (j = 0; j <= nx - 1; j++)
        {
            buf.x[j] = x[j] / s.s[j];
        }
        ablasf.rallocv(s.evaluator.chunksize, ref buf.evalbuf.funcbuf, _params);
        ablasf.rallocv(s.evaluator.chunksize, ref buf.evalbuf.wrkbuf, _params);
        colidx = 0;
        srcidx = 0;
        widx = 0;
        distance0 = 1.0E-50;
        if (s.bftype == 1)
        {

            //
            // Kernels that add squared parameter to the squared distance
            //
            distance0 = math.sqr(s.bfparam);
        }
        while (colidx < s.nc)
        {

            //
            // Handle basecase with size at most ChunkSize*ChunkSize
            //
            curchunk = Math.Min(s.evaluator.chunksize, s.nc - colidx);
            computerowchunk(s.evaluator, buf.x, buf.evalbuf, curchunk, srcidx, distance0, 0, _params);
            for (i = 0; i <= ny - 1; i++)
            {
                y[i] = y[i] + ablasf.rdotvr(curchunk, buf.evalbuf.funcbuf, s.wchunked, widx + i, _params);
            }
            colidx = colidx + curchunk;
            srcidx = srcidx + nx;
            widx = widx + ny;
        }
    }


    /*************************************************************************
    This function performs fast calculation  using  far  field  expansion  (if
    supported for a current model, and if model size justifies utilization  of
    far fields), using currently stored fast evaluation tolerance.

    If no far field is present, straightforward O(N) evaluation is performed.

    This function allows to use same RBF model object  in  different  threads,
    assuming  that  different   threads  use  different  instances  of  buffer
    structure.

    INPUT PARAMETERS:
        S       -   RBF model, may be shared between different threads
        Buf     -   buffer object created for this particular instance of  RBF
                    model with rbfcreatecalcbuffer().
        X       -   coordinates, array[NX].
                    X may have more than NX elements, in this case only 
                    leading NX will be used.
        Y       -   possibly preallocated array

    OUTPUT PARAMETERS:
        Y       -   function value, array[NY]. Y is not reallocated when it
                    is larger than NY.

      -- ALGLIB --
         Copyright 01.11.2022 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv3tsfastcalcbuf(rbfv3model s,
        rbfv3calcbuffer buf,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        int nx = 0;
        int ny = 0;
        int i = 0;
        int j = 0;

        ap.assert(ap.len(x) >= s.nx, "RBFV3TsCalcBuf: Length(X)<NX");
        ap.assert(apserv.isfinitevector(x, s.nx, _params), "RBFV3TsCalcBuf: X contains infinite or NaN values");
        nx = s.nx;
        ny = s.ny;

        //
        // Handle linear term
        //
        if (ap.len(y) < ny)
        {
            y = new double[ny];
        }
        for (i = 0; i <= ny - 1; i++)
        {
            y[i] = s.v[i, nx];
            for (j = 0; j <= nx - 1; j++)
            {
                y[i] = y[i] + s.v[i, j] * x[j];
            }
        }
        if (s.nc == 0)
        {
            return;
        }

        //
        // Handle RBF term
        //
        ablasf.rallocm(1, nx, ref buf.x2d, _params);
        for (j = 0; j <= nx - 1; j++)
        {
            buf.x2d[0, j] = x[j] / s.s[j];
        }
        fastevaluatorcomputebatch(s.fasteval, buf.x2d, 1, true, ref buf.y2d, _params);
        for (i = 0; i <= ny - 1; i++)
        {
            y[i] = y[i] + buf.y2d[i, 0];
        }
    }


    /*************************************************************************
    This function calculates values of the RBF model at the  given  point  and
    its derivatives, using external buffer object (internal temporaries of the
    RBF model are not modified).

    This function allows to use same RBF model object  in  different  threads,
    assuming  that  different   threads  use  different  instances  of  buffer
    structure.

    INPUT PARAMETERS:
        S       -   RBF model, may be shared between different threads
        Buf     -   buffer object created for this particular instance of  RBF
                    model with rbfcreatecalcbuffer().
        X       -   coordinates, array[NX].
                    X may have more than NX elements, in this case only 
                    leading NX will be used.
        Y, DY   -   possibly preallocated arrays

    OUTPUT PARAMETERS:
        Y       -   function value, array[NY]. Y is not reallocated when it
                    is larger than NY.
        DY      -   derivatives, array[NY*NX]. DY is not reallocated when it
                    is larger than NY*NX.

      -- ALGLIB --
         Copyright 13.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv3tsdiffbuf(rbfv3model s,
        rbfv3calcbuffer buf,
        double[] x,
        ref double[] y,
        ref double[] dy,
        xparams _params)
    {
        int nx = 0;
        int ny = 0;
        int i = 0;
        int j = 0;
        double smalldist2 = 0;
        bool nograd = new bool();
        int colidx = 0;
        int srcidx = 0;
        int widx = 0;
        int curchunk = 0;
        int maxchunksize = 0;
        double distance0 = 0;

        ap.assert(ap.len(x) >= s.nx, "RBFV3TsCalcBuf: Length(X)<NX");
        ap.assert(apserv.isfinitevector(x, s.nx, _params), "RBFV3TsCalcBuf: X contains infinite or NaN values");
        nx = s.nx;
        ny = s.ny;

        //
        // Handle linear term
        //
        if (ap.len(y) < ny)
        {
            y = new double[ny];
        }
        if (ap.len(dy) < s.ny * s.nx)
        {
            dy = new double[s.ny * s.nx];
        }
        for (i = 0; i <= ny - 1; i++)
        {
            y[i] = s.v[i, nx];
            for (j = 0; j <= nx - 1; j++)
            {
                y[i] = y[i] + s.v[i, j] * x[j];
                dy[i * nx + j] = s.v[i, j];
            }
        }
        if (s.nc == 0)
        {
            return;
        }

        //
        // Rescale X and DY to the internal scaling used by the RBF model
        //
        for (j = 0; j <= nx - 1; j++)
        {
            buf.x[j] = x[j] / s.s[j];
        }
        for (i = 0; i <= ny - 1; i++)
        {
            for (j = 0; j <= nx - 1; j++)
            {
                dy[i * nx + j] = dy[i * nx + j] * s.s[j];
            }
        }

        //
        // Prepare information necessary for the detection of the nonexistent gradient
        //
        nograd = false;
        smalldist2 = (ablasf.rdotv2(nx, buf.x, _params) + 1.0) * math.sqr(100 * math.machineepsilon);

        //
        // Handle RBF term
        //
        ap.assert((s.bftype == 1 || s.bftype == 2) || s.bftype == 3, "RBFV3TsDiffBuf: unsupported basis function type");
        ap.assert(s.bftype != 1 || (double)(s.bfparam) >= (double)(0), "RBFV3TsDiffBuf: inconsistent BFType/BFParam");
        maxchunksize = s.evaluator.chunksize;
        ablasf.rallocv(maxchunksize, ref buf.evalbuf.funcbuf, _params);
        ablasf.rallocv(maxchunksize, ref buf.evalbuf.wrkbuf, _params);
        ablasf.rallocv(maxchunksize, ref buf.evalbuf.df1, _params);
        ablasf.rallocm(nx, maxchunksize, ref buf.evalbuf.deltabuf, _params);
        ablasf.rsetallocv(maxchunksize, 1.0E50, ref buf.evalbuf.mindist2, _params);
        colidx = 0;
        srcidx = 0;
        widx = 0;
        distance0 = 1.0E-50;
        if (s.bftype == 1)
        {

            //
            // Kernels that add squared parameter to the squared distance
            //
            distance0 = math.sqr(s.bfparam);
        }
        while (colidx < s.nc)
        {

            //
            // Handle basecase with size at most ChunkSize*ChunkSize
            //
            curchunk = Math.Min(maxchunksize, s.nc - colidx);
            computerowchunk(s.evaluator, buf.x, buf.evalbuf, curchunk, srcidx, distance0, 1, _params);
            for (j = 0; j <= nx - 1; j++)
            {
                ablasf.rmergemulvr(curchunk, buf.evalbuf.df1, buf.evalbuf.deltabuf, j, _params);
            }
            for (i = 0; i <= ny - 1; i++)
            {
                y[i] = y[i] + ablasf.rdotvr(curchunk, buf.evalbuf.funcbuf, s.wchunked, widx + i, _params);
                for (j = 0; j <= nx - 1; j++)
                {
                    dy[i * nx + j] = dy[i * nx + j] + 2 * ablasf.rdotrr(curchunk, s.wchunked, widx + i, buf.evalbuf.deltabuf, j, _params);
                }
            }
            colidx = colidx + curchunk;
            srcidx = srcidx + nx;
            widx = widx + ny;
        }
        if (s.bftype == 1 && (double)(s.bfparam) == (double)(0))
        {

            //
            // The kernel function is nondifferentiable at nodes, check whether we are close to one of the nodes or not
            //
            for (i = 0; i <= maxchunksize - 1; i++)
            {
                nograd = nograd || buf.evalbuf.mindist2[i] <= smalldist2;
            }
            if (nograd)
            {

                //
                // The gradient is undefined at the trial point, flush it to zero
                //
                ablasf.rsetv(ny * nx, 0.0, dy, _params);
            }
        }

        //
        // Rescale derivatives back
        //
        for (i = 0; i <= ny - 1; i++)
        {
            for (j = 0; j <= nx - 1; j++)
            {
                dy[i * nx + j] = dy[i * nx + j] / s.s[j];
            }
        }
    }


    /*************************************************************************
    This function calculates values of the RBF model at the  given  point  and
    its first and second derivatives, using external buffer  object  (internal
    temporaries of the RBF model are not modified).

    This function allows to use same RBF model object  in  different  threads,
    assuming  that  different   threads  use  different  instances  of  buffer
    structure.

    INPUT PARAMETERS:
        S       -   RBF model, may be shared between different threads
        Buf     -   buffer object created for this particular instance of  RBF
                    model with rbfcreatecalcbuffer().
        X       -   coordinates, array[NX].
                    X may have more than NX elements, in this case only 
                    leading NX will be used.
        Y,DY,D2Y -  possibly preallocated arrays

    OUTPUT PARAMETERS:
        Y       -   function value, array[NY]. Y is not reallocated when it
                    is larger than NY.
        DY      -   derivatives, array[NY*NX]. DY is not reallocated when it
                    is larger than NY*NX.
        D2Y     -   second derivatives, array[NY*NX*NX].
                    D2Y is not reallocated when it is larger than NY*NX*NX.

      -- ALGLIB --
         Copyright 13.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv3tshessbuf(rbfv3model s,
        rbfv3calcbuffer buf,
        double[] x,
        ref double[] y,
        ref double[] dy,
        ref double[] d2y,
        xparams _params)
    {
        int nx = 0;
        int ny = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        int k0 = 0;
        int k1 = 0;
        bool nearnode = new bool();
        bool nograd = new bool();
        bool nohess = new bool();
        double smalldist2 = 0;
        int colidx = 0;
        int srcidx = 0;
        int widx = 0;
        int curchunk = 0;
        int maxchunksize = 0;
        double distance0 = 0;

        ap.assert(ap.len(x) >= s.nx, "RBFV3TsCalcBuf: Length(X)<NX");
        ap.assert(apserv.isfinitevector(x, s.nx, _params), "RBFV3TsCalcBuf: X contains infinite or NaN values");
        nx = s.nx;
        ny = s.ny;

        //
        // Handle linear term
        //
        if (ap.len(y) < ny)
        {
            y = new double[ny];
        }
        if (ap.len(dy) < s.ny * s.nx)
        {
            dy = new double[s.ny * s.nx];
        }
        if (ap.len(d2y) < ny * nx * nx)
        {
            d2y = new double[ny * nx * nx];
        }
        for (i = 0; i <= ny - 1; i++)
        {
            y[i] = s.v[i, nx];
            for (j = 0; j <= nx - 1; j++)
            {
                y[i] = y[i] + s.v[i, j] * x[j];
                dy[i * nx + j] = s.v[i, j];
            }
        }
        ablasf.rsetv(ny * nx * nx, 0.0, d2y, _params);
        if (s.nc == 0)
        {
            return;
        }

        //
        // Rescale X and DY to the internal scaling used by the RBF model (D2Y is zero,
        // so it does not need rescaling).
        //
        for (j = 0; j <= nx - 1; j++)
        {
            buf.x[j] = x[j] / s.s[j];
        }
        for (i = 0; i <= ny - 1; i++)
        {
            for (j = 0; j <= nx - 1; j++)
            {
                dy[i * nx + j] = dy[i * nx + j] * s.s[j];
            }
        }

        //
        // Prepare information necessary for the detection of the nonexistent Hessian
        //
        nograd = false;
        nohess = false;
        smalldist2 = (ablasf.rdotv2(nx, buf.x, _params) + 1.0) * math.sqr(100 * math.machineepsilon);

        //
        // Handle RBF term
        //
        ap.assert(s.bftype == 1 || s.bftype == 2, "RBFV3TsHessBuf: unsupported basis function type");
        ap.assert(s.bftype != 1 || (double)(s.bfparam) >= (double)(0), "RBFV3TsHessBuf: inconsistent BFType/BFParam");
        maxchunksize = s.evaluator.chunksize;
        ablasf.rallocv(maxchunksize, ref buf.evalbuf.funcbuf, _params);
        ablasf.rallocv(maxchunksize, ref buf.evalbuf.wrkbuf, _params);
        ablasf.rallocv(maxchunksize, ref buf.evalbuf.df1, _params);
        ablasf.rallocv(maxchunksize, ref buf.evalbuf.df2, _params);
        ablasf.rallocm(nx, maxchunksize, ref buf.evalbuf.deltabuf, _params);
        ablasf.rsetallocv(maxchunksize, 1.0E50, ref buf.evalbuf.mindist2, _params);
        colidx = 0;
        srcidx = 0;
        widx = 0;
        distance0 = 1.0E-50;
        if (s.bftype == 1)
        {

            //
            // Kernels that add squared parameter to the squared distance
            //
            distance0 = math.sqr(s.bfparam);
        }
        while (colidx < s.nc)
        {

            //
            // Handle basecase with size at most ChunkSize*ChunkSize
            //
            curchunk = Math.Min(maxchunksize, s.nc - colidx);
            computerowchunk(s.evaluator, buf.x, buf.evalbuf, curchunk, srcidx, distance0, 2, _params);
            for (i = 0; i <= ny - 1; i++)
            {
                y[i] = y[i] + ablasf.rdotvr(curchunk, buf.evalbuf.funcbuf, s.wchunked, widx + i, _params);
                for (k0 = 0; k0 <= nx - 1; k0++)
                {
                    ablasf.rcopyrv(curchunk, buf.evalbuf.deltabuf, k0, buf.evalbuf.wrkbuf, _params);
                    ablasf.rmergemulv(curchunk, buf.evalbuf.df1, buf.evalbuf.wrkbuf, _params);
                    dy[i * nx + k0] = dy[i * nx + k0] + 2 * ablasf.rdotvr(curchunk, buf.evalbuf.wrkbuf, s.wchunked, widx + i, _params);
                }
                for (k0 = 0; k0 <= nx - 1; k0++)
                {
                    for (k1 = 0; k1 <= nx - 1; k1++)
                    {
                        ablasf.rcopyv(curchunk, buf.evalbuf.df2, buf.evalbuf.wrkbuf, _params);
                        ablasf.rmergemulrv(curchunk, buf.evalbuf.deltabuf, k0, buf.evalbuf.wrkbuf, _params);
                        ablasf.rmergemulrv(curchunk, buf.evalbuf.deltabuf, k1, buf.evalbuf.wrkbuf, _params);
                        d2y[i * nx * nx + k0 * nx + k1] = d2y[i * nx * nx + k0 * nx + k1] + 4 * ablasf.rdotvr(curchunk, buf.evalbuf.wrkbuf, s.wchunked, widx + i, _params);
                        if (k0 == k1)
                        {
                            d2y[i * nx * nx + k0 * nx + k1] = d2y[i * nx * nx + k0 * nx + k1] + 2 * ablasf.rdotvr(curchunk, buf.evalbuf.df1, s.wchunked, widx + i, _params);
                        }
                    }
                }
            }
            colidx = colidx + curchunk;
            srcidx = srcidx + nx;
            widx = widx + ny;
        }
        nearnode = false;
        if ((s.bftype == 1 && (double)(s.bfparam) == (double)(0)) || s.bftype == 2)
        {

            //
            // The kernel function is nondifferentiable at nodes, check whether we are close to one of the nodes or not
            //
            for (i = 0; i <= maxchunksize - 1; i++)
            {
                nearnode = nearnode || buf.evalbuf.mindist2[i] <= smalldist2;
            }
        }
        nograd = nearnode && (s.bftype == 1 && (double)(s.bfparam) == (double)(0));
        nohess = nearnode && ((s.bftype == 1 && (double)(s.bfparam) == (double)(0)) || s.bftype == 2);
        if (nograd)
        {

            //
            // The gradient is undefined at the trial point, flush it to zero
            //
            ablasf.rsetv(ny * nx, 0.0, dy, _params);
        }
        if (nohess)
        {

            //
            // The Hessian is undefined at the trial point, flush it to zero
            //
            ablasf.rsetv(ny * nx * nx, 0.0, d2y, _params);
        }

        //
        // Rescale derivatives back
        //
        for (i = 0; i <= ny - 1; i++)
        {
            for (j = 0; j <= nx - 1; j++)
            {
                dy[i * nx + j] = dy[i * nx + j] / s.s[j];
            }
        }
        for (i = 0; i <= ny - 1; i++)
        {
            for (j = 0; j <= nx - 1; j++)
            {
                for (k = 0; k <= nx - 1; k++)
                {
                    d2y[i * nx * nx + j * nx + k] = d2y[i * nx * nx + j * nx + k] / (s.s[j] * s.s[k]);
                }
            }
        }
    }


    /*************************************************************************
    This function is used to perform gridded calculation  for  2D,  3D  or  4D
    problems. It accepts parameters X0...X3 and counters N0...N3. If RBF model
    has dimensionality less than 4, corresponding arrays should  contain  just
    one element equal to zero, and corresponding N's should be equal to 1.

    NOTE: array Y should be preallocated by caller.

      -- ALGLIB --
         Copyright 12.07.2016 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv3gridcalcvx(rbfv3model s,
        double[] x0,
        int n0,
        double[] x1,
        int n1,
        double[] x2,
        int n2,
        double[] x3,
        int n3,
        bool[] flagy,
        bool sparsey,
        double[] y,
        xparams _params)
    {
        rbfv3calcbuffer bufseed = new rbfv3calcbuffer();
        smp.shared_pool bufpool = new smp.shared_pool();
        int simdwidth = 0;
        int tilescnt = 0;


        //
        // Perform integrity checks
        //
        ap.assert(s.nx == 2 || s.nx == 3, "RBFGridCalcVX: integrity check failed");
        ap.assert(((n0 >= 1 && n1 >= 1) && n2 >= 1) && n3 >= 1, "RBFGridCalcVX: integrity check failed");
        ap.assert(s.nx >= 4 || ((ap.len(x3) >= 1 && (double)(x3[0]) == (double)(0)) && n3 == 1), "RBFGridCalcVX: integrity check failed");
        ap.assert(s.nx >= 3 || ((ap.len(x2) >= 1 && (double)(x2[0]) == (double)(0)) && n2 == 1), "RBFGridCalcVX: integrity check failed");
        ap.assert(s.nx >= 2 || ((ap.len(x1) >= 1 && (double)(x1[0]) == (double)(0)) && n1 == 1), "RBFGridCalcVX: integrity check failed");
        ap.assert(!sparsey || ap.len(flagy) >= n0 * n1 * n2 * n3, "RBFGridCalcVX: integrity check failed");

        //
        // Prepare shared pool
        //
        rbfv3createcalcbuffer(s, bufseed, _params);
        smp.ae_shared_pool_set_seed(bufpool, bufseed);

        //
        // Call worker function
        //
        simdwidth = 8;
        tilescnt = apserv.idivup(n0, simdwidth, _params) * apserv.idivup(n1, simdwidth, _params) * apserv.idivup(n2, simdwidth, _params) * apserv.idivup(n3, simdwidth, _params);
        gridcalcrec(s, simdwidth, 0, tilescnt, x0, n0, x1, n1, x2, n2, x3, n3, flagy, sparsey, y, bufpool, true, _params);
    }


    /*************************************************************************
    This function "unpacks" RBF model by extracting its coefficients.

    INPUT PARAMETERS:
        S       -   RBF model

    OUTPUT PARAMETERS:
        NX      -   dimensionality of argument
        NY      -   dimensionality of the target function
        XWR     -   model information, array[NC,NX+NY+NX+2].
                    One row of the array corresponds to one basis function
                    * first NX columns  - coordinates of the center 
                    * next  NY columns  - weights, one per dimension of the 
                                          function being modeled
                    * next NX columns   - radii, one per dimension
                    * next column       - basis function type:
                                          * 1  for f=r
                                          * 2  for f=r^2*ln(r)
                                          * 10 for multiquadric f=sqrt(r^2+alpha^2)
                    * next column       - basis function parameter:
                                          * alpha, for basis function type 10
                                          * ignored (zero) for other basis function types
                    * next column       - point index in the original dataset,
                                          or -1 for an artificial node created
                                          by the solver. The algorithm may reorder
                                          the nodes, drop some nodes or add
                                          artificial nodes. Thus, one parsing
                                          this column should expect all these
                                          kinds of alterations in the dataset.
        NC      -   number of the centers
        V       -   polynomial  term , array[NY,NX+1]. One row per one 
                    dimension of the function being modelled. First NX 
                    elements are linear coefficients, V[NX] is equal to the 
                    constant part.

      -- ALGLIB --
         Copyright 12.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv3unpack(rbfv3model s,
        ref int nx,
        ref int ny,
        ref double[,] xwr,
        ref int nc,
        ref double[,] v,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int cwwidth = 0;
        bool recognized = new bool();

        nx = 0;
        ny = 0;
        xwr = new double[0, 0];
        nc = 0;
        v = new double[0, 0];

        nx = s.nx;
        ny = s.ny;
        nc = s.nc;

        //
        // Fill V
        //
        v = new double[s.ny, s.nx + 1];
        for (i = 0; i <= s.ny - 1; i++)
        {
            ablasf.rcopyrr(nx + 1, s.v, i, v, i, _params);
        }

        //
        // Fill XWR
        //
        if (nc > 0)
        {
            cwwidth = nx + ny;
            xwr = new double[nc, nx + ny + nx + 3];
            for (i = 0; i <= nc - 1; i++)
            {

                //
                // Output centers (in the original variable scaling), weights and radii
                //
                for (j = 0; j <= nx - 1; j++)
                {
                    xwr[i, j] = s.cw[i * cwwidth + j] * s.s[j];
                }
                for (j = 0; j <= ny - 1; j++)
                {
                    xwr[i, nx + j] = s.cw[i * cwwidth + nx + j];
                }
                for (j = 0; j <= nx - 1; j++)
                {
                    xwr[i, nx + ny + j] = s.s[j];
                }

                //
                // Recognize specific basis function used and perform post-processing
                //
                recognized = false;
                if (s.bftype == 1 && (double)(s.bfparam) == (double)(0))
                {

                    //
                    // Biharmonic kernel f=r
                    //
                    // Weights are multiplied by -1 because actually it is f=-r (the latter
                    // is conditionally positive definite basis function, and the former is
                    // how it is known to most users)
                    //
                    xwr[i, nx + ny + nx + 0] = 1;
                    xwr[i, nx + ny + nx + 1] = 0.0;
                    for (j = 0; j <= ny - 1; j++)
                    {
                        xwr[i, nx + j] = -xwr[i, nx + j];
                    }
                    recognized = true;
                }
                if (s.bftype == 1 && (double)(s.bfparam) > (double)(0))
                {

                    //
                    // Multiquadric f=sqrt(r^2+alpha^2)
                    //
                    // Weights are multiplied by -1 because actually it is f=-sqrt(r^2+alpha^2)
                    // (the latter is conditionally positive definite basis function, and the
                    // former is how it is known to most users)
                    //
                    xwr[i, nx + ny + nx + 0] = 10;
                    xwr[i, nx + ny + nx + 1] = s.bfparam;
                    for (j = 0; j <= ny - 1; j++)
                    {
                        xwr[i, nx + j] = -xwr[i, nx + j];
                    }
                    recognized = true;
                }
                if (s.bftype == 2)
                {

                    //
                    // Thin plate spline f=r^2*ln(r)
                    //
                    xwr[i, nx + ny + nx + 0] = 2;
                    xwr[i, nx + ny + nx + 1] = 0;
                    recognized = true;
                }
                ap.assert(recognized, "RBFV3: integrity check 5342 failed");

                //
                // Output indexes
                //
                xwr[i, nx + ny + nx + 2] = s.pointindexes[i];
            }
        }
    }


    /*************************************************************************
    Get maximum panel size for a fast evaluator

      -- ALGLIB --
         Copyright 27.08.2022 by Sergey Bochkanov
    *************************************************************************/
    public static int rbf3getmaxpanelsize(xparams _params)
    {
        int result = 0;

        result = defaultmaxpanelsize;
        return result;
    }


    /*************************************************************************
    Changes fast evaluator tolerance.

    The  original  fast  evaluator  provides too conservative error estimates,
    even when relaxed tolerances are used.  Thus,  far  field  expansions  are
    often ignored when it is pretty safe  to use them and  save  computational
    time.

    This function does the following:
    * pushes 'raw' tolerance specified by user to the fast evaluator
    * samples about 100 randomly selected points, computes  true  average  and
      maximum errors of the fast evaluator on these points.
      Usually these errors are 1000x-10000x less than ones allowed by user.
    * adjusts 'raw' tolerance, producing so called 'working' tolerance in order
      to bring maximum error closer to limits, improving performance due to
      looser tolerances

    The running time of this function is O(N). It is not thread-safe.

      -- ALGLIB --
         Copyright 01.11.2022 by Sergey Bochkanov
    *************************************************************************/
    public static void rbf3pushfastevaltol(rbfv3model s,
        double tol,
        xparams _params)
    {
        int seed0 = 0;
        int seed1 = 0;
        hqrnd.hqrndstate rs = new hqrnd.hqrndstate();
        int i = 0;
        int j = 0;
        int k = 0;
        int nsampled = 0;
        double[] x = new double[0];
        double[] ya = new double[0];
        double[] yb = new double[0];
        double avgrawerror = 0;
        double maxrawerror = 0;
        rbfv3calcbuffer buf = new rbfv3calcbuffer();
        double tolgrowth = 0;
        double maxtolgrowth = 0;

        ap.assert((double)(tol) > (double)(0), "RBF3PushFastEvalTol: TOL<=0");
        if (s.nc == 0)
        {
            return;
        }
        rbfv3createcalcbuffer(s, buf, _params);

        //
        // Choose seeds for RNG that produces indexes of points being sampled
        //
        nsampled = 100;
        maxtolgrowth = 1.0E6;
        seed0 = 47623;
        seed1 = 83645264;

        //
        // Push 'raw' tolerance, sample random points and compute errors
        //
        fastevaluatorpushtol(s.fasteval, tol, _params);
        avgrawerror = 0;
        maxrawerror = 0;
        ablasf.rallocv(s.nx, ref x, _params);
        hqrnd.hqrndseed(seed0, seed1, rs, _params);
        for (i = 0; i <= nsampled - 1; i++)
        {

            //
            // Sample point
            //
            k = hqrnd.hqrnduniformi(rs, s.nc, _params);
            for (j = 0; j <= s.nx - 1; j++)
            {
                x[j] = s.cw[(s.nx + s.ny) * k + j];
            }

            //
            // Compute reference value, fast value, compare
            //
            rbfv3tscalcbuf(s, buf, x, ref ya, _params);
            rbfv3tsfastcalcbuf(s, buf, x, ref yb, _params);
            for (j = 0; j <= s.ny - 1; j++)
            {
                avgrawerror = avgrawerror + Math.Abs(ya[j] - yb[j]);
                maxrawerror = Math.Max(maxrawerror, Math.Abs(ya[j] - yb[j]));
            }
        }
        avgrawerror = avgrawerror / (nsampled * s.ny);

        //
        // Compute proposed growth for the target tolerance.
        //
        // NOTE: a heuristic formula is used which works well in practice.
        //
        tolgrowth = tol / Math.Max(avgrawerror * 25 + tol / maxtolgrowth, maxrawerror * 5 + tol / maxtolgrowth);
        if ((double)(tolgrowth) < (double)(1))
        {
            return;
        }

        //
        // Adjust tolerance
        //
        fastevaluatorpushtol(s.fasteval, tol * tolgrowth, _params);
    }


    /*************************************************************************
    Allocate temporaries in the evaluator buffer
    *************************************************************************/
    private static void evalbufferinit(rbf3evaluatorbuffer buf,
        int nx,
        int maxpanelsize,
        xparams _params)
    {
        ablasf.rallocv(maxpanelsize, ref buf.funcbuf, _params);
        ablasf.rallocv(maxpanelsize, ref buf.wrkbuf, _params);
        ablasf.rallocv(maxpanelsize, ref buf.df1, _params);
        ablasf.rallocv(maxpanelsize, ref buf.df2, _params);
        ablasf.rallocm(nx, maxpanelsize, ref buf.deltabuf, _params);
        ablasf.rallocv(maxpanelsize, ref buf.mindist2, _params);
        ablasf.rallocv(maxpanelsize, ref buf.coeffbuf, _params);
        ablasf.rallocv(nx, ref buf.x, _params);
    }


    /*************************************************************************
    Recursive function for the fast evaluator
    *************************************************************************/
    private static int fastevaluatorinitrec(rbf3fastevaluator eval,
        double[,] xx,
        int[] ptidx,
        double[] coordbuf,
        int idx0,
        int idx1,
        apserv.nrpool nxpool,
        xparams _params)
    {
        int result = 0;
        rbf3panel panel = null;
        double[] boxmin = new double[0];
        double[] boxmax = new double[0];
        int i = 0;
        int j = 0;
        double v = 0;
        int idxmid = 0;
        int subsetlength = 0;
        int subset0 = 0;
        int subset1 = 0;
        int largestdim = 0;

        ap.assert(idx1 > idx0, "FastEvaluatorInitRec: Idx1<=Idx0");
        subsetlength = idx1 - idx0;

        //
        // Add panel to the array, prepare to return its index
        //
        panel = new rbf3panel();
        result = eval.panels.append(panel);

        //
        // Prepare panel fields that are always set:
        // * panel center and radius
        // * default far field state (not present)
        //
        ablasf.rsetallocv(eval.nx, 0.0, ref panel.clustercenter, _params);
        for (i = idx0; i <= idx1 - 1; i++)
        {
            for (j = 0; j <= eval.nx - 1; j++)
            {
                panel.clustercenter[j] = panel.clustercenter[j] + xx[ptidx[i], j];
            }
        }
        for (j = 0; j <= eval.nx - 1; j++)
        {
            panel.clustercenter[j] = panel.clustercenter[j] / subsetlength;
        }
        if (eval.nx <= 4 && eval.nx > 0)
        {
            panel.c0 = panel.clustercenter[0];
        }
        if (eval.nx <= 4 && eval.nx > 1)
        {
            panel.c1 = panel.clustercenter[1];
        }
        if (eval.nx <= 4 && eval.nx > 2)
        {
            panel.c2 = panel.clustercenter[2];
        }
        if (eval.nx <= 4 && eval.nx > 3)
        {
            panel.c3 = panel.clustercenter[3];
        }
        panel.clusterrad = 1.0E-50;
        for (i = idx0; i <= idx1 - 1; i++)
        {
            v = 0;
            for (j = 0; j <= eval.nx - 1; j++)
            {
                v = v + math.sqr(panel.clustercenter[j] - xx[ptidx[i], j]);
            }
            panel.clusterrad = Math.Max(panel.clusterrad, v);
        }
        panel.clusterrad = Math.Sqrt(panel.clusterrad);
        panel.farfieldexpansion = farfieldnone;
        panel.farfielddistance = 0.0;
        panel.idx0 = idx0;
        panel.idx1 = idx1;

        //
        // Handle leaf panel (small enough)
        //
        if (subsetlength <= eval.maxpanelsize)
        {
            panel.paneltype = 0;
            ablasf.iallocv(subsetlength, ref panel.ptidx, _params);
            ablasf.rallocm(eval.nx, subsetlength, ref panel.xt, _params);
            for (i = idx0; i <= idx1 - 1; i++)
            {
                panel.ptidx[i - idx0] = ptidx[i];
                for (j = 0; j <= eval.nx - 1; j++)
                {
                    v = xx[ptidx[i], j];
                    panel.xt[j, i - idx0] = v;
                    eval.permx[i, j] = v;
                }
            }
            ablasf.rsetallocm(eval.ny, subsetlength, 0.0, ref panel.wt, _params);
            evalbufferinit(panel.tgtbuf, eval.nx, eval.maxpanelsize, _params);
            return result;
        }

        //
        // Prepare temporaries
        //
        apserv.nrpoolretrieve(nxpool, ref boxmin, _params);
        apserv.nrpoolretrieve(nxpool, ref boxmax, _params);

        //
        // Prepare to split large panel:
        // * compute bounding box and its largest dimension
        // * sort points by largest dimension of the bounding box
        // * split in two
        //
        ablasf.rcopyrv(eval.nx, xx, ptidx[idx0], boxmin, _params);
        ablasf.rcopyrv(eval.nx, xx, ptidx[idx0], boxmax, _params);
        for (i = idx0 + 1; i <= idx1 - 1; i++)
        {
            for (j = 0; j <= eval.nx - 1; j++)
            {
                boxmin[j] = Math.Min(boxmin[j], xx[ptidx[i], j]);
                boxmax[j] = Math.Max(boxmax[j], xx[ptidx[i], j]);
            }
        }
        largestdim = 0;
        for (j = 1; j <= eval.nx - 1; j++)
        {
            if ((double)(boxmax[j] - boxmin[j]) > (double)(boxmax[largestdim] - boxmin[largestdim]))
            {
                largestdim = j;
            }
        }
        for (i = idx0; i <= idx1 - 1; i++)
        {
            coordbuf[i] = xx[ptidx[i], largestdim];
        }
        tsort.tagsortmiddleri(coordbuf, ptidx, idx0, subsetlength, _params);
        ap.assert(subsetlength > eval.maxpanelsize, "RBF3: integrity check 2955 failed");
        apserv.tiledsplit(subsetlength, apserv.icase2(subsetlength > minfarfieldsize, minfarfieldsize, eval.maxpanelsize, _params), ref subset0, ref subset1, _params);
        idxmid = idx0 + subset0;

        //
        // Return temporaries back to nxPool and perform recursive processing
        //
        apserv.nrpoolrecycle(nxpool, ref boxmin, _params);
        apserv.nrpoolrecycle(nxpool, ref boxmax, _params);
        panel.paneltype = 1;
        panel.childa = fastevaluatorinitrec(eval, xx, ptidx, coordbuf, idx0, idxmid, nxpool, _params);
        panel.childb = fastevaluatorinitrec(eval, xx, ptidx, coordbuf, idxmid, idx1, nxpool, _params);
        return result;
    }


    /*************************************************************************
    Initialize fast model evaluator using current dataset and default  (zero)
    coefficients.

    The coefficients can be loaded later  with  RBF3FastEvaluatorLoadCoeffs()
    or RBF3FastEvaluatorLoadCoeffs1().
    *************************************************************************/
    private static void fastevaluatorinit(rbf3fastevaluator eval,
        double[,] x,
        int n,
        int nx,
        int ny,
        int maxpanelsize,
        int bftype,
        double bfparam,
        bool usedebugcounters,
        xparams _params)
    {
        double[] coordbuf = new double[0];
        int rootidx = 0;
        apserv.nrpool nxpool = new apserv.nrpool();
        int i = 0;
        rbf3evaluatorbuffer bufseed = new rbf3evaluatorbuffer();

        x = (double[,])x.Clone();


        //
        // Prepare the evaluator and temporaries
        //
        eval.n = n;
        eval.nx = nx;
        eval.ny = ny;
        eval.maxpanelsize = maxpanelsize;
        eval.functype = bftype;
        eval.funcparam = bfparam;
        eval.panels.clear();
        ablasf.rsetallocm(n, 3 + ny, 0.0, ref eval.tmpx3w, _params);
        ablasf.rsetallocm(ny, n, 0.0, ref eval.wstoredorig, _params);
        ablasf.rallocm(n, nx, ref eval.permx, _params);
        evalbufferinit(bufseed, eval.nx, eval.maxpanelsize, _params);
        smp.ae_shared_pool_set_seed(eval.bufferpool, bufseed);
        eval.usedebugcounters = usedebugcounters;
        eval.dbgpanel2panelcnt = 0;
        eval.dbgfield2panelcnt = 0;
        eval.dbgpanelscnt = 0;
        eval.isloaded = false;

        //
        // Perform recursive subdivision, generate panels
        //
        ablasf.iallocv(n, ref eval.origptidx, _params);
        for (i = 0; i <= n - 1; i++)
        {
            eval.origptidx[i] = i;
        }
        ablasf.rallocv(n, ref coordbuf, _params);
        apserv.nrpoolinit(nxpool, nx, _params);
        rootidx = fastevaluatorinitrec(eval, x, eval.origptidx, coordbuf, 0, n, nxpool, _params);
        ap.assert(rootidx == 0, "FastEvaluatorInit: integrity check for RootIdx failed");
    }


    /*************************************************************************
    Recursive subroutine that loads coefficients into the fast evaluator. The
    coefficients are expected to be in Eval.WStoredOrig[NY,N]

    Depending  on  the  settings  specified  during evaluator creation (basis
    function type and parameter), far field expansion can be  built  for  the
    model.
    *************************************************************************/
    private static void fastevaluatorloadcoeffsrec(rbf3fastevaluator eval,
        int treenodeidx,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int npts = 0;
        rbf3panel panel = null;

        eval.panels.get(treenodeidx, ref panel);
        npts = panel.idx1 - panel.idx0;

        //
        // Create far field expansion, if possible
        //
        panel.farfieldexpansion = farfieldnone;
        if (((eval.functype == 1 && npts >= minfarfieldsize) && (double)(eval.funcparam) == (double)(0.0)) && eval.nx <= 3)
        {

            //
            // Use far field expansions for a biharmonic kernel
            //
            for (i = panel.idx0; i <= panel.idx1 - 1; i++)
            {
                for (j = 0; j <= eval.nx - 1; j++)
                {
                    eval.tmpx3w[i, j] = eval.permx[i, j];
                }
                for (j = 0; j <= eval.ny - 1; j++)
                {
                    eval.tmpx3w[i, 3 + j] = eval.wstoredorig[j, eval.origptidx[i]];
                }
            }
            rbfv3farfields.bhpanelinit(panel.bhexpansion, eval.tmpx3w, panel.idx0, panel.idx1, eval.ny, eval.bheval, _params);
            panel.farfieldexpansion = farfieldbiharmonic;
            panel.farfielddistance = panel.bhexpansion.useatdistance;
        }

        //
        // Non-leaf panel with two children
        //
        if (panel.paneltype == 1)
        {

            //
            // Load coefficients into child panels.
            //
            fastevaluatorloadcoeffsrec(eval, panel.childa, _params);
            fastevaluatorloadcoeffsrec(eval, panel.childb, _params);
            return;
        }

        //
        // Leaf panel
        //
        ap.assert(panel.paneltype == 0, "RBF3: integrity check 4594 failed");
        for (i = 0; i <= eval.ny - 1; i++)
        {
            for (j = 0; j <= npts - 1; j++)
            {
                panel.wt[i, j] = eval.wstoredorig[i, panel.ptidx[j]];
            }
        }
    }


    /*************************************************************************
    Loads coefficients into fast evaluator built with NY=1.

    Depending  on  the  settings  specified  during evaluator creation (basis
    function type and parameter), far field expansion can be  built  for  the
    model.

    INPUT PARAMETERS:
        Eval            -   fast evaluator to load coefficients to
        W               -   coeffs vector
    *************************************************************************/
    private static void fastevaluatorloadcoeffs1(rbf3fastevaluator eval,
        double[] w,
        xparams _params)
    {
        ap.assert(eval.ny == 1, "FastEvaluatorLoadCoeffs1: Eval.NY<>1");
        ap.assert(eval.panels.getlength() > 0, "FastEvaluatorLoadCoeffs1: Length(Panels)=0");

        //
        // Prepare problem-specific evaluator, if any
        //
        if ((eval.functype == 1 && (double)(eval.funcparam) == (double)(0.0)) && eval.nx <= 3)
        {
            rbfv3farfields.biharmonicevaluatorinit(eval.bheval, biharmonicseriesmax, _params);
        }

        //
        // Recursively load coefficients into panels
        //
        ablasf.rcopyvr(eval.n, w, eval.wstoredorig, 0, _params);
        fastevaluatorloadcoeffsrec(eval, 0, _params);

        //
        // Done
        //
        eval.isloaded = true;
    }


    /*************************************************************************
    Loads coefficients into fast evaluator (works with any NY>=1)

    Depending  on  the  settings  specified  during evaluator creation (basis
    function type and parameter), far field expansion can be  built  for  the
    model.

    INPUT PARAMETERS:
        Eval            -   fast evaluator to load coefficients to
        W               -   coeffs vector, array[NY,N]
    *************************************************************************/
    private static void fastevaluatorloadcoeffs(rbf3fastevaluator eval,
        double[,] w,
        xparams _params)
    {
        ap.assert(eval.ny <= ap.rows(w), "FastEvaluatorLoadCoeffs: Eval.NY>Rows(W)");
        ap.assert(eval.panels.getlength() > 0, "FastEvaluatorLoadCoeffs: Length(Panels)=0");

        //
        // Prepare problem-specific evaluator, if any
        //
        if ((eval.functype == 1 && (double)(eval.funcparam) == (double)(0.0)) && eval.nx <= 3)
        {
            rbfv3farfields.biharmonicevaluatorinit(eval.bheval, biharmonicseriesmax, _params);
        }

        //
        // Recursively load coefficients into panels
        //
        ablas.rmatrixcopy(eval.ny, eval.n, w, 0, 0, eval.wstoredorig, 0, 0, _params);
        fastevaluatorloadcoeffsrec(eval, 0, _params);

        //
        // Done
        //
        eval.isloaded = true;
    }


    /*************************************************************************
    Recursive subroutine that recomputes far field radii according  to  user-
    specified accuracy requirements.

    It should be called only for properly initialized models with coefficients
    being loaded into them.
    *************************************************************************/
    private static void fastevaluatorpushtolrec(rbf3fastevaluator eval,
        int treenodeidx,
        bool dotrace,
        int dbglevel,
        double maxcomputeerr,
        xparams _params)
    {
        double childerr = 0;
        rbf3panel panel = null;
        bool farfieldreconfigured = new bool();

        eval.panels.get(treenodeidx, ref panel);

        //
        // Reconfigure far field expansion, if present
        //
        if (panel.farfieldexpansion != farfieldnone)
        {
            farfieldreconfigured = false;

            //
            // Far field expansions for a biharmonic kernel
            //
            if (panel.farfieldexpansion == farfieldbiharmonic)
            {
                rbfv3farfields.bhpanelsetprec(panel.bhexpansion, maxcomputeerr, _params);
                panel.farfielddistance = panel.bhexpansion.useatdistance;
                farfieldreconfigured = true;
                if (dotrace)
                {
                    apserv.tracespaces(dbglevel, _params);
                    ap.trace(System.String.Format("* n={0,0:d}, |c|={1,0:E1}, r/R={2,0:F1}\n", panel.idx1 - panel.idx0, panel.bhexpansion.maxsumabs, panel.bhexpansion.useatdistance / (panel.bhexpansion.rmax + 1.0E-50)));
                }
            }

            //
            // Check that far field was recognized and processed
            //
            ap.assert(farfieldreconfigured, "RBF3: unexpected far field at PushTolRec()");
        }

        //
        // Non-leaf panel with two children
        //
        if (panel.paneltype == 1)
        {

            //
            // Propagate relaxed error bounds to children. Instead of requiring it to be
            // MaxComputeErr/2 for each of the subpanels (the sum is MaxComputeErr, which
            // guarantees that the error bound is satisfied) we use larger value: MaxComputeErr/sqrt(2).
            //
            // The idea is that individual panel errors are uncorrelated, so we can achieve
            // better results by using relaxed error tolerances, but still having total
            // sum - on average - within prescribed bounds. Because error estimates are
            // overly cautious, it usually works fine.
            //
            childerr = apserv.rcase2(userelaxederrorestimates, maxcomputeerr / 1.41, maxcomputeerr / 2, _params);
            fastevaluatorpushtolrec(eval, panel.childa, dotrace, dbglevel + 1, childerr, _params);
            fastevaluatorpushtolrec(eval, panel.childb, dotrace, dbglevel + 1, childerr, _params);
            return;
        }
    }


    /*************************************************************************
    Sets fast evaluator tolerance, reconfiguring far field  radii  all  along
    the evaluation tree. Assumes that coefficients were loaded  with  one  of
    FastEvaluatorLoadCoeffs()/FastEvaluatorLoadCoeffs1() calls.

    INPUT PARAMETERS:
        Eval            -   fast evaluator with loaded coefficients
        MaxComputeErr   -   non-negative value which controls accuracy of the
                            model evaluation:
                            * =0 means that we try to perform exact evaluation
                                 (subject to rounding errors) and do not try
                                 to utilize far field expansions
                            * >0 means that far field expansions are used when
                                 we can save some time. Maximum absolute error
                                 of the model will be at most MaxComputeErr.
    *************************************************************************/
    private static void fastevaluatorpushtol(rbf3fastevaluator eval,
        double maxcomputeerr,
        xparams _params)
    {
        bool dotrace = new bool();

        ap.assert(math.isfinite(maxcomputeerr), "FastEvaluatorPushTol: MaxComputeErr is not finite");
        ap.assert((double)(maxcomputeerr) >= (double)(0), "FastEvaluatorPushTol: MaxComputeErr<0");
        ap.assert(eval.isloaded, "FastEvaluatorPushTol: coefficients are not loaded");
        dotrace = ap.istraceenabled("RBF.DETAILED", _params);
        if (dotrace)
        {
            ap.trace("----- recomputing fast eval tolerances, printing far field info ------------------------------------\n");
            ap.trace(System.String.Format("> new tolerance is {0,0:E3}\n", maxcomputeerr));
        }
        fastevaluatorpushtolrec(eval, 0, dotrace, 0, maxcomputeerr, _params);
    }


    /*************************************************************************
    Updates Y[] with result of panel-to-panel interaction using straightforward
    O(PANELSIZE^2) computation formula.
    *************************************************************************/
    private static void fastevaluatorcomputepanel2panel(rbf3fastevaluator eval,
        rbf3panel dstpanel,
        rbf3panel srcpanel,
        rbf3evaluatorbuffer buf,
        double[] y,
        xparams _params)
    {
        int ndstpts = 0;
        int i0 = 0;
        int k = 0;
        double distance0 = 0;
        int srcsize = 0;

        ap.assert(eval.ny == 1, "RBF3Panel2Panel: ny>1");
        ap.assert(dstpanel.paneltype == 0 && dstpanel.idx1 - dstpanel.idx0 <= eval.maxpanelsize, "RBF3: integrity check 2735 failed");
        ap.assert(srcpanel.paneltype == 0 && srcpanel.idx1 - srcpanel.idx0 <= eval.maxpanelsize, "RBF3: integrity check 2736 failed");
        ndstpts = dstpanel.idx1 - dstpanel.idx0;
        srcsize = srcpanel.idx1 - srcpanel.idx0;
        distance0 = 1.0E-50;
        if (eval.functype == 1)
        {
            distance0 = distance0 + math.sqr(eval.funcparam);
        }
        ap.assert(eval.functype == 1 || eval.functype == 2, "RBF3: integrity check 9132 failed");
        for (i0 = 0; i0 <= ndstpts - 1; i0++)
        {
            ablasf.rsetv(srcsize, distance0, buf.funcbuf, _params);
            for (k = 0; k <= eval.nx - 1; k++)
            {
                ablasf.rsetv(srcsize, dstpanel.xt[k, i0], buf.wrkbuf, _params);
                ablasf.raddrv(srcsize, -1.0, srcpanel.xt, k, buf.wrkbuf, _params);
                ablasf.rmuladdv(srcsize, buf.wrkbuf, buf.wrkbuf, buf.funcbuf, _params);
            }
            if (eval.functype == 1)
            {

                //
                // f=-sqrt(r^2+alpha^2), including f=-r as a special case
                //
                ablasf.rsqrtv(srcsize, buf.funcbuf, _params);
                ablasf.rmulv(srcsize, -1.0, buf.funcbuf, _params);
            }
            if (eval.functype == 2)
            {

                //
                // f=r^2*ln(r)
                //
                // NOTE: FuncBuf[] is always positive due to small correction added,
                //       thus we have no need to handle ln(0) as a special case.
                //
                for (k = 0; k <= srcsize - 1; k++)
                {
                    buf.funcbuf[k] = buf.funcbuf[k] * 0.5 * Math.Log(buf.funcbuf[k]);
                }
            }
            y[dstpanel.ptidx[i0]] = y[dstpanel.ptidx[i0]] + ablasf.rdotvr(srcsize, buf.funcbuf, srcpanel.wt, 0, _params);
        }
    }


    /*************************************************************************
    Recursive evaluation function that recursively evaluates all source panels.

    The plan is for each target panel to evaluate all source panels that may
    contribute to model values at target points. This function evaluates all
    sources, given some target.

    This function has to be called with SourceTreeNode=0.
    *************************************************************************/
    private static void fastevaluatorcomputeallrecurseonsources(rbf3fastevaluator eval,
        rbf3panel dstpanel,
        rbf3evaluatorbuffer buf,
        int sourcetreenode,
        double[] y,
        xparams _params)
    {
        rbf3panel srcpanel = null;
        int i0 = 0;
        int k = 0;
        double v = 0;
        double vv = 0;
        double dstpaneldistance = 0;
        bool farfieldprocessed = new bool();
        double c0 = 0;
        double c1 = 0;
        double c2 = 0;
        int ndstpts = 0;

        ndstpts = dstpanel.idx1 - dstpanel.idx0;
        eval.panels.get(sourcetreenode, ref srcpanel);

        //
        // Analyze possibility of applying far field expansion
        //
        if (srcpanel.farfieldexpansion != farfieldnone)
        {
            dstpaneldistance = 0;
            for (k = 0; k <= eval.nx - 1; k++)
            {
                dstpaneldistance = dstpaneldistance + math.sqr(dstpanel.clustercenter[k] - srcpanel.clustercenter[k]);
            }
            dstpaneldistance = Math.Sqrt(dstpaneldistance);
            dstpaneldistance = dstpaneldistance - dstpanel.clusterrad;
            if ((double)(dstpaneldistance) > (double)(srcpanel.farfielddistance))
            {
                farfieldprocessed = false;
                c0 = 0;
                c1 = 0;
                c2 = 0;
                if (srcpanel.farfieldexpansion == farfieldbiharmonic)
                {
                    for (i0 = 0; i0 <= ndstpts - 1; i0++)
                    {
                        if (eval.nx >= 1)
                        {
                            c0 = dstpanel.xt[0, i0];
                        }
                        if (eval.nx >= 2)
                        {
                            c1 = dstpanel.xt[1, i0];
                        }
                        if (eval.nx >= 3)
                        {
                            c2 = dstpanel.xt[2, i0];
                        }
                        rbfv3farfields.bhpaneleval1(srcpanel.bhexpansion, eval.bheval, c0, c1, c2, ref v, false, ref vv, _params);
                        y[dstpanel.ptidx[i0]] = y[dstpanel.ptidx[i0]] + v;
                    }
                    farfieldprocessed = true;
                }
                ap.assert(farfieldprocessed, "RBF3: integrity check 4832 failed");
                if (eval.usedebugcounters)
                {
                    apserv.threadunsafeinc(ref eval.dbgfield2panelcnt, _params);
                }
                return;
            }
        }

        //
        // Far field expansion is not present, or we are too close to the expansion center.
        // Try recursive processing or handle leaf panel.
        //
        if (srcpanel.paneltype == 1)
        {
            fastevaluatorcomputeallrecurseonsources(eval, dstpanel, buf, srcpanel.childa, y, _params);
            fastevaluatorcomputeallrecurseonsources(eval, dstpanel, buf, srcpanel.childb, y, _params);
        }
        else
        {
            fastevaluatorcomputepanel2panel(eval, dstpanel, srcpanel, buf, y, _params);
            if (eval.usedebugcounters)
            {
                apserv.threadunsafeinc(ref eval.dbgpanel2panelcnt, _params);
            }
        }
    }


    /*************************************************************************
    Recursive evaluation function that recursively evaluates all target panels.

    The plan is for each target panel to evaluate all source panels that may
    contribute to model values at target points. This function evaluates all
    targets, and it passes control to another function that evaluates all
    sources.

    This function has to be called with TargetTreeNode=0.
    It can parallelize its computations.
    *************************************************************************/
    private static void fastevaluatorcomputeallrecurseontargets(rbf3fastevaluator eval,
        int targettreenode,
        double[] y,
        xparams _params)
    {
        rbf3panel dstpanel = null;


        //
        // Do we need parallel execution?
        // Checked only at the root.
        //
        if ((targettreenode == 0 && (double)(apserv.rmul2(eval.n, eval.n, _params)) > (double)(apserv.smpactivationlevel(_params))) && eval.panels.getlength() > 1)
        {
            if (_trypexec_fastevaluatorcomputeallrecurseontargets(eval, targettreenode, y, _params))
            {
                return;
            }
        }

        //
        // Evaluate destination panel
        //
        eval.panels.get(targettreenode, ref dstpanel);
        if (dstpanel.paneltype == 1)
        {
            fastevaluatorcomputeallrecurseontargets(eval, dstpanel.childa, y, _params);
            fastevaluatorcomputeallrecurseontargets(eval, dstpanel.childb, y, _params);
            return;
        }
        ap.assert(dstpanel.paneltype == 0, "RBF3: integrity check 2735 failed");

        //
        // Recurse on sources.
        // Use evaluator buffer stored in target panel for temporaries.
        //
        fastevaluatorcomputeallrecurseonsources(eval, dstpanel, dstpanel.tgtbuf, 0, y, _params);
        apserv.threadunsafeinc(ref eval.dbgpanelscnt, _params);
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_fastevaluatorcomputeallrecurseontargets(rbf3fastevaluator eval,
        int targettreenode,
        double[] y, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    Performs batch evaluation at each node (assuming NY=1).
    *************************************************************************/
    private static void fastevaluatorcomputeall(rbf3fastevaluator eval,
        ref double[] y,
        xparams _params)
    {
        ap.assert(eval.ny == 1, "FastEvaluatorComputeAll: Eval.NY<>1");
        ablasf.rsetallocv(eval.n, 0.0, ref y, _params);
        eval.dbgpanel2panelcnt = 0;
        eval.dbgfield2panelcnt = 0;
        eval.dbgpanelscnt = 0;
        fastevaluatorcomputeallrecurseontargets(eval, 0, y, _params);
    }


    /*************************************************************************
    Recursion on source panels for batch evaluation

    NOTE: this function is thread-safe, the same Eval object can be  used  by
          multiple threads calling ComputeBatch() concurrently. Although  for
          technical reasons Eval is accepted as non-const parameter,  thread-
          safety is guaranteed.

    INPUT PARAMETERS:
        Eval        -   evaluator   
        X           -   array[N,NX], dataset
        TgtIdx      -   target row index
        SourceTreeNode- index of the current panel in the tree; must be zero.
        UseFarFields-   use far fields to accelerate computations - or use
                        slow but exact formulae

    OUTPUT PARAMETERS:
        Y           -   array[NY,N], column TgtIdx is updated
    *************************************************************************/
    private static void fastevaluatorcomputebatchrecurseonsources(rbf3fastevaluator eval,
        double[,] x,
        int tgtidx,
        int sourcetreenode,
        bool usefarfields,
        rbf3evaluatorbuffer buf,
        double[,] y,
        xparams _params)
    {
        rbf3panel srcpanel = null;
        int srcsize = 0;
        int k = 0;
        int d = 0;
        double distance0 = 0;
        double paneldistance = 0;
        bool farfieldprocessed = new bool();
        double c0 = 0;
        double c1 = 0;
        double c2 = 0;
        double v = 0;
        double vv = 0;

        eval.panels.get(sourcetreenode, ref srcpanel);

        //
        // Analyze possibility of applying far field expansion
        //
        if (srcpanel.farfieldexpansion != farfieldnone && usefarfields)
        {
            paneldistance = 0;
            for (k = 0; k <= eval.nx - 1; k++)
            {
                paneldistance = paneldistance + math.sqr(x[tgtidx, k] - srcpanel.clustercenter[k]);
            }
            paneldistance = Math.Sqrt(paneldistance);
            if ((double)(paneldistance) > (double)(srcpanel.farfielddistance))
            {
                farfieldprocessed = false;
                c0 = 0;
                c1 = 0;
                c2 = 0;
                if (srcpanel.farfieldexpansion == farfieldbiharmonic)
                {
                    if (eval.nx >= 1)
                    {
                        c0 = x[tgtidx, 0];
                    }
                    if (eval.nx >= 2)
                    {
                        c1 = x[tgtidx, 1];
                    }
                    if (eval.nx >= 3)
                    {
                        c2 = x[tgtidx, 2];
                    }
                    if (eval.ny == 1)
                    {
                        rbfv3farfields.bhpaneleval1(srcpanel.bhexpansion, eval.bheval, c0, c1, c2, ref v, false, ref vv, _params);
                        y[0, tgtidx] = y[0, tgtidx] + v;
                    }
                    else
                    {
                        rbfv3farfields.bhpaneleval(srcpanel.bhexpansion, eval.bheval, c0, c1, c2, ref buf.y, false, ref vv, _params);
                        for (k = 0; k <= eval.ny - 1; k++)
                        {
                            y[k, tgtidx] = y[k, tgtidx] + buf.y[k];
                        }
                    }
                    farfieldprocessed = true;
                }
                ap.assert(farfieldprocessed, "RBF3: integrity check 4832 failed");
                if (eval.usedebugcounters)
                {
                    apserv.threadunsafeinc(ref eval.dbgfield2panelcnt, _params);
                }
                return;
            }
        }

        //
        // Perform recursive processing if needed
        //
        if (srcpanel.paneltype == 1)
        {
            fastevaluatorcomputebatchrecurseonsources(eval, x, tgtidx, srcpanel.childa, usefarfields, buf, y, _params);
            fastevaluatorcomputebatchrecurseonsources(eval, x, tgtidx, srcpanel.childb, usefarfields, buf, y, _params);
            return;
        }
        ap.assert(srcpanel.paneltype == 0 && srcpanel.idx1 - srcpanel.idx0 <= eval.maxpanelsize, "RBF3: integrity check 2735 failed");

        //
        // Obtain evaluation buffer and process panel.
        // Recycle the buffer later.
        //
        ap.assert(eval.functype == 1 || eval.functype == 2, "RBF3: integrity check 1132 failed");
        srcsize = srcpanel.idx1 - srcpanel.idx0;
        distance0 = 1.0E-50;
        if (eval.functype == 1)
        {
            distance0 = distance0 + math.sqr(eval.funcparam);
        }
        ablasf.rsetv(srcsize, distance0, buf.funcbuf, _params);
        for (k = 0; k <= eval.nx - 1; k++)
        {
            ablasf.rsetv(srcsize, x[tgtidx, k], buf.wrkbuf, _params);
            ablasf.raddrv(srcsize, -1.0, srcpanel.xt, k, buf.wrkbuf, _params);
            ablasf.rmuladdv(srcsize, buf.wrkbuf, buf.wrkbuf, buf.funcbuf, _params);
        }
        if (eval.functype == 1)
        {

            //
            // f=-sqrt(r^2+alpha^2), including f=-r as a special case
            //
            ablasf.rsqrtv(srcsize, buf.funcbuf, _params);
            ablasf.rmulv(srcsize, -1.0, buf.funcbuf, _params);
        }
        if (eval.functype == 2)
        {

            //
            // f=r^2*ln(r)
            //
            // NOTE: FuncBuf[] is always positive due to small correction added,
            //       thus we have no need to handle ln(0) as a special case.
            //
            for (k = 0; k <= srcsize - 1; k++)
            {
                buf.funcbuf[k] = buf.funcbuf[k] * 0.5 * Math.Log(buf.funcbuf[k]);
            }
        }
        for (d = 0; d <= eval.ny - 1; d++)
        {
            y[d, tgtidx] = y[d, tgtidx] + ablasf.rdotvr(srcsize, buf.funcbuf, srcpanel.wt, d, _params);
        }
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_fastevaluatorcomputebatchrecurseonsources(rbf3fastevaluator eval,
        double[,] x,
        int tgtidx,
        int sourcetreenode,
        bool usefarfields,
        rbf3evaluatorbuffer buf,
        double[,] y, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    Recursion on targets for batch evaluation, splits interval into smaller
    ones.

    NOTE: this function is thread-safe, the same Eval object can be  used  by
          multiple threads calling ComputeBatch() concurrently. Although  for
          technical reasons Eval is accepted as non-const parameter,  thread-
          safety is guaranteed.

    INPUT PARAMETERS:
        Eval        -   evaluator   
        X           -   array[N,NX], dataset
        Idx0,Idx1   -   target range
        IsRootCall  -   must be True
        UseFarFields-   use far fields to accelerate computations - or use
                        slow but exact formulae

    OUTPUT PARAMETERS:
        Y           -   array[NY,N], computed values
    *************************************************************************/
    private static void fastevaluatorcomputebatchrecurseontargets(rbf3fastevaluator eval,
        double[,] x,
        int idx0,
        int idx1,
        bool isrootcall,
        bool usefarfields,
        double[,] y,
        xparams _params)
    {
        int i = 0;
        int size0 = 0;
        int size1 = 0;
        rbf3evaluatorbuffer buf = null;


        //
        // Do we need parallel execution?
        // Checked only at the root.
        //
        if ((isrootcall && idx1 - idx0 > maxcomputebatchsize) && (double)(apserv.rmul2(eval.n, idx1 - idx0, _params)) > (double)(apserv.smpactivationlevel(_params)))
        {
            if (_trypexec_fastevaluatorcomputebatchrecurseontargets(eval, x, idx0, idx1, isrootcall, usefarfields, y, _params))
            {
                return;
            }
        }

        //
        // Split targets
        //
        if (idx1 - idx0 > maxcomputebatchsize)
        {
            apserv.tiledsplit(idx1 - idx0, maxcomputebatchsize, ref size0, ref size1, _params);
            fastevaluatorcomputebatchrecurseontargets(eval, x, idx0, idx0 + size0, false, usefarfields, y, _params);
            fastevaluatorcomputebatchrecurseontargets(eval, x, idx0 + size0, idx1, false, usefarfields, y, _params);
            return;
        }

        //
        // Run recursion on sources
        //
        smp.ae_shared_pool_retrieve(eval.bufferpool, ref buf);
        for (i = idx0; i <= idx1 - 1; i++)
        {
            fastevaluatorcomputebatchrecurseonsources(eval, x, i, 0, usefarfields, buf, y, _params);
        }
        smp.ae_shared_pool_recycle(eval.bufferpool, ref buf);
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_fastevaluatorcomputebatchrecurseontargets(rbf3fastevaluator eval,
        double[,] x,
        int idx0,
        int idx1,
        bool isrootcall,
        bool usefarfields,
        double[,] y, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    Performs batch evaluation at user-specified points which does not have to
    coincide with nodes. Assumes NY=1.

    NOTE: this function is thread-safe, the same Eval object can be  used  by
          multiple threads calling ComputeBatch() concurrently. Although  for
          technical reasons Eval is accepted as non-const parameter,  thread-
          safety is guaranteed.

    INPUT PARAMETERS:
        Eval        -   evaluator
        X           -   array[N,NX], dataset
        N           -   rows count
        UseFarFields-   use far fields to accelerate computations - or use
                        slow but exact formulae

    OUTPUT PARAMETERS:
        Y           -   array[NY,N], computed values.
                        The array is reallocated if needed.
    *************************************************************************/
    private static void fastevaluatorcomputebatch(rbf3fastevaluator eval,
        double[,] x,
        int n,
        bool usefarfields,
        ref double[,] y,
        xparams _params)
    {
        ablasf.rsetallocm(eval.ny, n, 0.0, ref y, _params);
        fastevaluatorcomputebatchrecurseontargets(eval, x, 0, n, true, usefarfields, y, _params);
    }


    /*************************************************************************
    Creates fast evaluation structures after initialization of the model

      -- ALGLIB --
         Copyright 12.12.2021 by Sergey Bochkanov
    *************************************************************************/
    private static void createfastevaluator(rbfv3model model,
        xparams _params)
    {
        int offs = 0;
        int ontheflystorage = 0;
        int i = 0;
        int j = 0;
        int nchunks = 0;
        int srcoffs = 0;
        int dstoffs = 0;
        int curlen = 0;
        double[,] xx = new double[0, 0];
        double[,] ct = new double[0, 0];


        //
        // Extract dataset into separate matrix
        //
        ablasf.rallocm(model.nc, model.nx, ref xx, _params);
        ablasf.rallocm(model.ny, model.nc, ref ct, _params);
        offs = 0;
        for (i = 0; i <= model.nc - 1; i++)
        {
            for (j = 0; j <= model.nx - 1; j++)
            {
                xx[i, j] = model.cw[offs + j];
            }
            offs = offs + model.nx;
            for (j = 0; j <= model.ny - 1; j++)
            {
                ct[j, i] = model.cw[offs + j];
            }
            offs = offs + model.ny;
        }

        //
        // Prepare fast evaluator
        //
        fastevaluatorinit(model.fasteval, xx, model.nc, model.nx, model.ny, defaultmaxpanelsize, model.bftype, model.bfparam, false, _params);
        fastevaluatorloadcoeffs(model.fasteval, ct, _params);
        fastevaluatorpushtol(model.fasteval, defaultfastevaltol, _params);

        //
        // Setup model matrix structure
        //
        ontheflystorage = 1;
        modelmatrixinit(xx, model.nc, model.nx, model.bftype, model.bfparam, ontheflystorage, model.evaluator, _params);

        //
        // Store model coefficients in the efficient chunked format (chunk size is aligned with that
        // of the Model.Evaluator).
        //
        ap.assert(model.evaluator.chunksize >= 1, "RBFV3: integrity check 3535 failed");
        nchunks = apserv.idivup(model.nc, model.evaluator.chunksize, _params);
        ablasf.rsetallocm(nchunks * model.ny, model.evaluator.chunksize, 0.0, ref model.wchunked, _params);
        srcoffs = 0;
        dstoffs = 0;
        while (srcoffs < model.nc)
        {
            curlen = Math.Min(model.evaluator.chunksize, model.nc - srcoffs);
            for (i = 0; i <= curlen - 1; i++)
            {
                for (j = 0; j <= model.ny - 1; j++)
                {
                    model.wchunked[dstoffs + j, i] = model.cw[(srcoffs + i) * (model.nx + model.ny) + model.nx + j];
                }
            }
            srcoffs = srcoffs + curlen;
            dstoffs = dstoffs + model.ny;
        }
    }


    /*************************************************************************
    Recursive worker function for gridded calculation

      -- ALGLIB --
         Copyright 01.05.2022 by Bochkanov Sergey
    *************************************************************************/
    private static void gridcalcrec(rbfv3model s,
        int simdwidth,
        int tileidx0,
        int tileidx1,
        double[] x0,
        int n0,
        double[] x1,
        int n1,
        double[] x2,
        int n2,
        double[] x3,
        int n3,
        bool[] flagy,
        bool sparsey,
        double[] y,
        smp.shared_pool calcpool,
        bool isrootcall,
        xparams _params)
    {
        int ny = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        int dstoffs = 0;
        int l = 0;
        rbfv3calcbuffer buf = null;
        double problemcost = 0;
        int tileidxm = 0;
        int k0 = 0;
        int k1 = 0;
        int k2 = 0;
        int r0a = 0;
        int r0b = 0;
        int r1a = 0;
        int r1b = 0;
        int r2a = 0;
        int r2b = 0;

        ny = s.ny;

        //
        // Try parallelism if needed; then perform parallel subdivision:
        // * make all dimensions either (a) multiples of SIMDWidth or (b) less than SIMDWidth by
        //   splitting small chunks from tails
        // * after that iteratively subdivide largest side of the grid (one having largest length,
        //   not largest points count) until we have a chunk with nodes count not greater than SIMDWidth
        //
        problemcost = apserv.rmul2(tileidx1 - tileidx0, s.nc, _params);
        problemcost = problemcost * apserv.rmul4(Math.Min(n0, simdwidth), Math.Min(n1, simdwidth), Math.Min(n2, simdwidth), Math.Min(n3, simdwidth), _params);
        if (isrootcall && (double)(problemcost) >= (double)(apserv.smpactivationlevel(_params)))
        {
            if (_trypexec_gridcalcrec(s, simdwidth, tileidx0, tileidx1, x0, n0, x1, n1, x2, n2, x3, n3, flagy, sparsey, y, calcpool, isrootcall, _params))
            {
                return;
            }
        }
        if ((double)(problemcost) >= (double)(apserv.spawnlevel(_params)) && tileidx1 - tileidx0 >= 2)
        {
        }
        if (tileidx1 - tileidx0 >= 2)
        {
            tileidxm = tileidx0 + apserv.idivup(tileidx1 - tileidx0, 2, _params);
            gridcalcrec(s, simdwidth, tileidx0, tileidxm, x0, n0, x1, n1, x2, n2, x3, n3, flagy, sparsey, y, calcpool, false, _params);
            gridcalcrec(s, simdwidth, tileidxm, tileidx1, x0, n0, x1, n1, x2, n2, x3, n3, flagy, sparsey, y, calcpool, false, _params);
            return;
        }

        //
        // Handle basecase
        //
        k = tileidx0;
        k0 = k % apserv.idivup(n0, simdwidth, _params);
        k = k / apserv.idivup(n0, simdwidth, _params);
        k1 = k % apserv.idivup(n1, simdwidth, _params);
        k = k / apserv.idivup(n1, simdwidth, _params);
        k2 = k % apserv.idivup(n2, simdwidth, _params);
        k = k / apserv.idivup(n2, simdwidth, _params);
        k = k / apserv.idivup(n3, simdwidth, _params);
        ap.assert(k == 0, "RBFV3: integrity check 7350 failed");
        r0a = k0 * simdwidth;
        r0b = Math.Min(r0a + simdwidth, n0);
        r1a = k1 * simdwidth;
        r1b = Math.Min(r1a + simdwidth, n1);
        r2a = k2 * simdwidth;
        r2b = Math.Min(r2a + simdwidth, n2);
        smp.ae_shared_pool_retrieve(calcpool, ref buf);
        for (i = r0a; i <= r0b - 1; i++)
        {
            for (j = r1a; j <= r1b - 1; j++)
            {
                for (k = r2a; k <= r2b - 1; k++)
                {
                    dstoffs = i + j * n0 + k * n0 * n1;
                    if (sparsey && !flagy[dstoffs])
                    {
                        for (l = 0; l <= ny - 1; l++)
                        {
                            y[l + ny * dstoffs] = 0;
                        }
                        continue;
                    }
                    buf.xg[0] = x0[i];
                    buf.xg[1] = x1[j];
                    buf.xg[2] = x2[k];
                    rbfv3tscalcbuf(s, buf, buf.xg, ref buf.yg, _params);
                    for (l = 0; l <= ny - 1; l++)
                    {
                        y[l + ny * dstoffs] = buf.yg[l];
                    }
                }
            }
        }
        smp.ae_shared_pool_recycle(calcpool, ref buf);
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_gridcalcrec(rbfv3model s,
        int simdwidth,
        int tileidx0,
        int tileidx1,
        double[] x0,
        int n0,
        double[] x1,
        int n1,
        double[] x2,
        int n2,
        double[] x3,
        int n3,
        bool[] flagy,
        bool sparsey,
        double[] y,
        smp.shared_pool calcpool,
        bool isrootcall, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    This function fills RBF model by zeros, also cleans up debug fields.

      -- ALGLIB --
         Copyright 12.12.2021 by Bochkanov Sergey
    *************************************************************************/
    private static void zerofill(rbfv3model s,
        int nx,
        int ny,
        xparams _params)
    {
        s.bftype = 0;
        s.bfparam = 0;
        s.nc = 0;
        ablasf.rsetallocv(nx, 1.0, ref s.s, _params);
        ablasf.rsetallocm(ny, nx + 1, 0.0, ref s.v, _params);
    }


    /*************************************************************************
    Reallocates calcBuf if necessary, reuses previously allocated space if
    possible.

      -- ALGLIB --
         Copyright 12.12.2021 by Sergey Bochkanov
    *************************************************************************/
    private static void allocatecalcbuffer(rbfv3model s,
        rbfv3calcbuffer buf,
        xparams _params)
    {
        if (ap.len(buf.x) < s.nx)
        {
            buf.x = new double[s.nx];
        }
        if (ap.len(buf.x123) < s.nx)
        {
            buf.x123 = new double[s.nx];
        }
        if (ap.len(buf.y123) < s.ny)
        {
            buf.y123 = new double[s.ny];
        }
        if (ap.len(buf.xg) < 4)
        {
            buf.xg = new double[4];
        }
        if (ap.len(buf.yg) < s.ny)
        {
            buf.yg = new double[s.ny];
        }
    }


    /*************************************************************************
    Recursive function that merges points, used by PreprocessDataset()

      -- ALGLIB --
         Copyright 12.12.2021 by Sergey Bochkanov
    *************************************************************************/
    private static void preprocessdatasetrec(double[,] xbuf,
        double[,] ybuf,
        int[] initidx,
        int wrk0,
        int wrk1,
        int nx,
        int ny,
        double mergetol,
        ref double[] tmpboxmin,
        ref double[] tmpboxmax,
        double[,] xout,
        double[,] yout,
        int[] raw2wrkmap,
        int[] wrk2rawmap,
        ref int nout,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k0 = 0;
        int k1 = 0;
        int largestdim = 0;
        double splitval = 0;

        if (wrk1 <= wrk0)
        {
            return;
        }

        //
        // Analyze current working set
        //
        ablasf.rallocv(nx, ref tmpboxmin, _params);
        ablasf.rallocv(nx, ref tmpboxmax, _params);
        ablasf.rcopyrv(nx, xbuf, wrk0, tmpboxmin, _params);
        ablasf.rcopyrv(nx, xbuf, wrk0, tmpboxmax, _params);
        for (i = wrk0 + 1; i <= wrk1 - 1; i++)
        {
            for (j = 0; j <= nx - 1; j++)
            {
                tmpboxmin[j] = Math.Min(tmpboxmin[j], xbuf[i, j]);
                tmpboxmax[j] = Math.Max(tmpboxmax[j], xbuf[i, j]);
            }
        }
        largestdim = 0;
        for (j = 1; j <= nx - 1; j++)
        {
            if ((double)(tmpboxmax[j] - tmpboxmin[j]) > (double)(tmpboxmax[largestdim] - tmpboxmin[largestdim]))
            {
                largestdim = j;
            }
        }

        //
        // Handle basecase or perform recursive split
        //
        if (wrk1 - wrk0 == 1 || (double)(tmpboxmax[largestdim] - tmpboxmin[largestdim]) < (double)(mergetol * apserv.rmax3(ablasf.rmaxabsv(nx, tmpboxmax, _params), ablasf.rmaxabsv(nx, tmpboxmin, _params), 1, _params)))
        {

            //
            // Merge all points, output
            //
            ablasf.rsetr(nx, 0.0, xout, nout, _params);
            ablasf.rsetr(ny, 0.0, yout, nout, _params);
            for (i = wrk0; i <= wrk1 - 1; i++)
            {
                ablasf.raddrr(nx, (double)1 / (double)(wrk1 - wrk0), xbuf, i, xout, nout, _params);
                ablasf.raddrr(ny, (double)1 / (double)(wrk1 - wrk0), ybuf, i, yout, nout, _params);
                raw2wrkmap[initidx[i]] = nout;
            }
            wrk2rawmap[nout] = initidx[wrk0];
            nout = nout + 1;
        }
        else
        {

            //
            // Perform recursive split along largest axis
            //
            splitval = 0.5 * (tmpboxmax[largestdim] + tmpboxmin[largestdim]);
            k0 = wrk0;
            k1 = wrk1 - 1;
            while (k0 <= k1)
            {
                if ((double)(xbuf[k0, largestdim]) <= (double)(splitval))
                {
                    k0 = k0 + 1;
                    continue;
                }
                if ((double)(xbuf[k1, largestdim]) > (double)(splitval))
                {
                    k1 = k1 - 1;
                    continue;
                }
                apserv.swaprows(xbuf, k0, k1, nx, _params);
                apserv.swaprows(ybuf, k0, k1, ny, _params);
                apserv.swapelementsi(initidx, k0, k1, _params);
                k0 = k0 + 1;
                k1 = k1 - 1;
            }
            ap.assert(k0 > wrk0 && k1 < wrk1 - 1, "RBFV3: integrity check 5843 in the recursive subdivision code failed");
            ap.assert(k0 == k1 + 1, "RBFV3: integrity check 5364 in the recursive subdivision code failed");
            preprocessdatasetrec(xbuf, ybuf, initidx, wrk0, k0, nx, ny, mergetol, ref tmpboxmin, ref tmpboxmax, xout, yout, raw2wrkmap, wrk2rawmap, ref nout, _params);
            preprocessdatasetrec(xbuf, ybuf, initidx, k0, wrk1, nx, ny, mergetol, ref tmpboxmin, ref tmpboxmax, xout, yout, raw2wrkmap, wrk2rawmap, ref nout, _params);
        }
    }


    /*************************************************************************
    This function preprocesses dataset by:
    * merging non-distinct points
    * centering points
    * applying user scale to X-values
    * performing additional scaling of X-values
    * normalizing Y-values

    INPUT PARAMETERS:
        XRaw        -   array[NRaw,NX], variable values
        YRaw        -   array[NRaw,NY], target values
        XScaleRaw   -   array[NX], user scales
        NRaw,NX,NY  -   metrics; N>0, NX>0, NY>0
        BFType      -   basis function type
        BFParamRaw  -   initial value for basis function paramerer (before
                        applying additional rescaling AddXRescaleAplied)
        LambdaVRaw  -   smoothing coefficient, as specified by user
        
    OUTPUT PARAMETERS:
        XWrk        -   array[NWrk,NX], processed points, XWrk=(XRaw-XShift)/XScaleWrk
        YWrk        -   array[NWrk,NY], targets, scaled by dividing by YScale
        PointIndexes-   array[NWrk], point indexes in the original dataset
        NWrk        -   number of points after preprocessing, 0<NWrk<=NRaw
        XScaleWrk   -   array[NX], XScaleWrk[]=XScaleRaw[]*AddXRescaleAplied
        XShift      -   array[NX], centering coefficients
        YScale      -   common scaling for targets
        BFParamWrk  -   BFParamRaw/AddXRescaleAplied
        LambdaVWrk  -   LambdaV after dataset scaling, automatically adjusted for
                        dataset spread
        AddXRescaleAplied-additional scaling applied after user scaling

      -- ALGLIB --
         Copyright 12.12.2021 by Sergey Bochkanov
    *************************************************************************/
    private static void preprocessdataset(double[,] xraw,
        double mergetol,
        double[,] yraw,
        double[] xscaleraw,
        int nraw,
        int nx,
        int ny,
        int bftype,
        double bfparamraw,
        double lambdavraw,
        ref double[,] xwrk,
        ref double[,] ywrk,
        ref int[] raw2wrkmap,
        ref int[] wrk2rawmap,
        ref int nwrk,
        ref double[] xscalewrk,
        ref double[] xshift,
        ref double bfparamwrk,
        ref double lambdavwrk,
        ref double addxrescaleaplied,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double diag2 = 0;
        double v = 0;
        double[,] xbuf = new double[0, 0];
        double[,] ybuf = new double[0, 0];
        double[] tmp0 = new double[0];
        double[] tmp1 = new double[0];
        double[] boxmin = new double[0];
        double[] boxmax = new double[0];
        int[] initidx = new int[0];

        xraw = (double[,])xraw.Clone();
        yraw = (double[,])yraw.Clone();
        xscaleraw = (double[])xscaleraw.Clone();
        xwrk = new double[0, 0];
        ywrk = new double[0, 0];
        raw2wrkmap = new int[0];
        wrk2rawmap = new int[0];
        nwrk = 0;
        xscalewrk = new double[0];
        xshift = new double[0];
        bfparamwrk = 0;
        lambdavwrk = 0;
        addxrescaleaplied = 0;

        ap.assert(nraw >= 1, "RBFV3: integrity check 7295 failed");

        //
        // Scale dataset:
        // * first, scale it according to user-supplied scale
        // * second, analyze original dataset and rescale it one more time (same scaling across
        //   all dimensions) so it has zero mean and unit deviation
        // As a result, user-supplied scaling handles dimensionality issues and our additional
        // scaling normalizes data.
        //
        // After this block we have NRaw-sized dataset in XWrk/YWrk
        //
        ablasf.rcopyallocv(nx, xscaleraw, ref xscalewrk, _params);
        ablasf.rsetallocv(nx, 0.0, ref xshift, _params);
        ablasf.rallocm(nraw, nx, ref xwrk, _params);
        for (i = 0; i <= nraw - 1; i++)
        {
            for (j = 0; j <= nx - 1; j++)
            {
                xwrk[i, j] = xraw[i, j] / xscalewrk[j];
                xshift[j] = xshift[j] + xwrk[i, j];
            }
        }
        ablasf.rmulv(nx, (double)1 / (double)nraw, xshift, _params);
        v = 0;
        for (i = 0; i <= nraw - 1; i++)
        {
            for (j = 0; j <= nx - 1; j++)
            {
                v = v + (xwrk[i, j] - xshift[j]) * (xwrk[i, j] - xshift[j]);
            }
        }
        addxrescaleaplied = Math.Sqrt((v + Math.Sqrt(math.machineepsilon)) / (nraw * nx));
        bfparamwrk = bfparamraw;
        if (bftype == 1)
        {

            //
            // Basis function parameter needs rescaling
            //
            if ((double)(bfparamraw) < (double)(0))
            {
                bfparamwrk = autodetectscaleparameter(xwrk, nraw, nx, _params) * -bfparamraw / addxrescaleaplied;
            }
            else
            {
                bfparamwrk = bfparamraw / addxrescaleaplied;
            }
        }
        else
        {
            if (bftype == 2)
            {

                //
                // Thin plate splines need special scaling; no params to rescale
                //
                addxrescaleaplied = polyharmonic2scale * addxrescaleaplied;
            }
            else
            {
                ap.assert(false, "RBFV3: integrity check 0632 failed");
            }
        }
        ablasf.rmulv(nx, addxrescaleaplied, xscalewrk, _params);
        for (i = 0; i <= nraw - 1; i++)
        {
            for (j = 0; j <= nx - 1; j++)
            {
                xwrk[i, j] = (xraw[i, j] - xshift[j]) / xscalewrk[j];
            }
        }
        ablasf.rcopyallocm(nraw, ny, yraw, ref ywrk, _params);

        //
        // Merge nondistinct points
        //
        ablasf.iallocv(nraw, ref initidx, _params);
        for (i = 0; i <= nraw - 1; i++)
        {
            initidx[i] = i;
        }
        ablasf.rcopyallocm(nraw, nx, xwrk, ref xbuf, _params);
        ablasf.rcopyallocm(nraw, ny, ywrk, ref ybuf, _params);
        ablasf.iallocv(nraw, ref raw2wrkmap, _params);
        ablasf.iallocv(nraw, ref wrk2rawmap, _params);
        nwrk = 0;
        preprocessdatasetrec(xbuf, ybuf, initidx, 0, nraw, nx, ny, mergetol, ref tmp0, ref tmp1, xwrk, ywrk, raw2wrkmap, wrk2rawmap, ref nwrk, _params);

        //
        // Compute LambdaV:
        // * compute bounding box
        // * compute DIAG2 = squared diagonal of the box
        // * set LambdaVWrk = LambdaVRaw times upper bound of the basis function value
        //
        ablasf.rallocv(nx, ref boxmin, _params);
        ablasf.rallocv(nx, ref boxmax, _params);
        ablasf.rcopyrv(nx, xwrk, 0, boxmin, _params);
        ablasf.rcopyrv(nx, xwrk, 0, boxmax, _params);
        for (i = 1; i <= nwrk - 1; i++)
        {
            ablasf.rmergeminrv(nx, xwrk, i, boxmin, _params);
            ablasf.rmergemaxrv(nx, xwrk, i, boxmax, _params);
        }
        diag2 = 0;
        for (i = 0; i <= nx - 1; i++)
        {
            diag2 = diag2 + math.sqr(boxmax[i] - boxmin[i]);
        }
        diag2 = Math.Max(diag2, 1);
        if (bftype == 1)
        {
            lambdavwrk = lambdavraw * Math.Sqrt(diag2 + bfparamwrk * bfparamwrk);
        }
        else
        {
            if (bftype == 2)
            {
                lambdavwrk = lambdavraw * diag2 * Math.Max(Math.Abs(0.5 * Math.Log(diag2)), 1.0);
            }
            else
            {
                lambdavwrk = lambdavraw;
                ap.assert(false, "RBFV3: integrity check 7232 failed");
            }
        }
        lambdavwrk = lambdavwrk / math.sqr(addxrescaleaplied);
    }


    /*************************************************************************
    This function selects NSpec global nodes for approximate cardinal basis functions.

    This function has O(N*NSpec) running time and O(N) memory requirements.

    Each approximate cardinal basis function is a combination of several local
    nodes (ones nearby to the center) and several global nodes (ones scattered
    over entire dataset span).

      -- ALGLIB --
         Copyright 12.12.2021 by Sergey Bochkanov
    *************************************************************************/
    private static void selectglobalnodes(double[,] xx,
        int n,
        int nx,
        int[] existingnodes,
        int nexisting,
        int nspec,
        ref int[] nodes,
        ref int nchosen,
        ref double maxdist,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        double[] d2 = new double[0];
        double[] x = new double[0];
        bool[] busy = new bool[0];
        double v = 0;
        double vv = 0;

        nchosen = 0;
        maxdist = 0;

        ap.assert(n >= 1, "RBFV3: integrity check 6429 failed");
        ap.assert(nexisting >= 0, "RBFV3: integrity check 6412 failed");
        ap.assert(nspec >= 1, "RBFV3: integrity check 6430 failed");
        nspec = Math.Min(nspec, n);
        ablasf.rsetallocv(n, 1.0E50, ref d2, _params);
        ablasf.rsetallocv(nx, 0.0, ref x, _params);
        ablasf.bsetallocv(n, false, ref busy, _params);
        if (nexisting == 0)
        {

            //
            // No initial grid is provided, start distance evaluation from the data center
            //
            for (i = 0; i <= n - 1; i++)
            {
                ablasf.rcopyrv(nx, xx, i, x, _params);
            }
            ablasf.rmulv(nx, (double)1 / (double)n, x, _params);
        }
        else
        {

            //
            //
            //
            ap.assert(false, "SelectGlobalNodes: NExisting<>0");
        }
        ablasf.iallocv(nspec, ref nodes, _params);
        nchosen = 0;
        maxdist = math.maxrealnumber;
        while (nchosen < nspec)
        {

            //
            // Update distances using last added point stored in X.
            //
            for (j = 0; j <= n - 1; j++)
            {
                v = 0;
                for (k = 0; k <= nx - 1; k++)
                {
                    vv = x[k] - xx[j, k];
                    v = v + vv * vv;
                }
                d2[j] = Math.Min(d2[j], v);
            }

            //
            // Select point with largest distance, add
            //
            k = 0;
            for (j = 0; j <= n - 1; j++)
            {
                if ((double)(d2[j]) > (double)(d2[k]) && !busy[j])
                {
                    k = j;
                }
            }
            if (busy[k])
            {
                break;
            }
            maxdist = Math.Min(maxdist, d2[k]);
            nodes[nchosen] = k;
            busy[k] = true;
            ablasf.rcopyrv(nx, xx, k, x, _params);
            nchosen = nchosen + 1;
        }
        maxdist = Math.Sqrt(maxdist);
        ap.assert(nchosen >= 1 || nexisting > 0, "RBFV3: integrity check 6431 failed");
    }


    /*************************************************************************
    This function builds simplified tagged KD-tree: it assigns a tag (index in
    the dataset) to each point, then drops most points (leaving  approximately
    1/ReduceFactor of the entire dataset)  trying to  spread  residual  points
    uniformly, and then constructs KD-tree.

    It ensures that at least min(N,MinSize) points is retained.

      -- ALGLIB --
         Copyright 12.12.2021 by Sergey Bochkanov
    *************************************************************************/
    private static void buildsimplifiedkdtree(double[,] xx,
        int n,
        int nx,
        int reducefactor,
        int minsize,
        nearestneighbor.kdtree kdt,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        int ns = 0;
        double[,] xs = new double[0, 0];
        int[] idx = new int[0];
        hqrnd.hqrndstate rs = new hqrnd.hqrndstate();

        ap.assert(n >= 1, "BuildSimplifiedKDTree: N<1");
        ap.assert(reducefactor >= 1, "BuildSimplifiedKDTree: ReduceFactor<1");
        ap.assert(minsize >= 0, "BuildSimplifiedKDTree: ReduceFactor<1");
        hqrnd.hqrndseed(7674, 45775, rs, _params);
        ns = apserv.imax3((int)Math.Round((double)n / (double)reducefactor), minsize, 1, _params);
        ns = Math.Min(ns, n);
        ablasf.iallocv(n, ref idx, _params);
        ablasf.rallocm(ns, nx, ref xs, _params);
        for (i = 0; i <= n - 1; i++)
        {
            idx[i] = i;
        }
        for (i = 0; i <= ns - 1; i++)
        {
            j = i + hqrnd.hqrnduniformi(rs, n - i, _params);
            k = idx[i];
            idx[i] = idx[j];
            idx[j] = k;
            ablasf.rcopyrr(nx, xx, idx[i], xs, i, _params);
        }
        nearestneighbor.kdtreebuildtagged(xs, idx, ns, nx, 0, 2, kdt, _params);
    }


    /*************************************************************************
    Compute design matrices for the target-scatter preconditioner

      -- ALGLIB --
         Copyright 12.12.2021 by Sergey Bochkanov
    *************************************************************************/
    private static void computetargetscatterdesignmatrices(double[,] xx,
        int ntotal,
        int nx,
        int functype,
        double funcparam,
        int[] workingnodes,
        int nwrk,
        int[] scatternodes,
        int nscatter,
        ref double[,] atwrk,
        ref double[,] atsctr,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        double v = 0;
        double vv = 0;
        int ni = 0;
        int nj = 0;
        double alpha2 = 0;


        //
        // Compute working set and scatter set design matrices ATWrk and ATSctr
        //
        // ATWrk  is a (NWrk+NX+1)*NWrk matrix whose entries a[i,j] store:
        // * for I<NWrk             BasisFunc(X[wrk[i]]-X[wrj[j]])
        // * for NWrk<=I<NWrk+NX    X[wrk[j]], coordinate #(i-NWrk)
        // * for I=NWrk+NX          1.0
        //
        // ATSctr is a (NWrk+NX+1)*NScatter matrix whose entries a[i,j] store:
        // * for I<NWrk             BasisFunc(X[wrk[i]]-X[scatter[j]])
        // * for NWrk<=I<NWrk+NX    X[scatter[j]], coordinate #(i-NWrk)
        // * for I=NWrk+NX          1.0
        //
        ap.assert((functype == 1 || functype == 2) || functype == 3, "ACBF: unexpected basis function type");
        alpha2 = funcparam * funcparam;
        ablasf.rallocm(nwrk + nx + 1, nwrk, ref atwrk, _params);
        for (i = 0; i <= nwrk - 1; i++)
        {
            ni = workingnodes[i];
            for (j = i; j <= nwrk - 1; j++)
            {
                nj = workingnodes[j];
                v = 0;
                for (k = 0; k <= nx - 1; k++)
                {
                    vv = xx[ni, k] - xx[nj, k];
                    v = v + vv * vv;
                }
                if (functype == 1)
                {
                    v = -Math.Sqrt(v + alpha2);
                }
                if (functype == 2)
                {
                    if (v != 0)
                    {
                        v = v * 0.5 * Math.Log(v);
                    }
                    else
                    {
                        v = 0;
                    }
                }
                if (functype == 3)
                {
                    v = v * Math.Sqrt(v);
                }
                atwrk[i, j] = v;
                atwrk[j, i] = v;
            }
        }
        for (j = 0; j <= nwrk - 1; j++)
        {
            nj = workingnodes[j];
            for (i = 0; i <= nx - 1; i++)
            {
                atwrk[nwrk + i, j] = xx[nj, i];
            }
        }
        for (j = 0; j <= nwrk - 1; j++)
        {
            atwrk[nwrk + nx, j] = 1.0;
        }
        if (nscatter > 0)
        {

            //
            // We have scattered points too
            //
            ablasf.rallocm(nwrk + nx + 1, nscatter, ref atsctr, _params);
            for (i = 0; i <= nwrk - 1; i++)
            {
                ni = workingnodes[i];
                for (j = 0; j <= nscatter - 1; j++)
                {
                    nj = scatternodes[j];
                    v = 0;
                    for (k = 0; k <= nx - 1; k++)
                    {
                        vv = xx[ni, k] - xx[nj, k];
                        v = v + vv * vv;
                    }
                    if (functype == 1)
                    {
                        v = -Math.Sqrt(v + alpha2);
                    }
                    if (functype == 2)
                    {
                        if (v != 0)
                        {
                            v = v * 0.5 * Math.Log(v);
                        }
                        else
                        {
                            v = 0;
                        }
                    }
                    if (functype == 3)
                    {
                        v = v * Math.Sqrt(v);
                    }
                    atsctr[i, j] = v;
                }
            }
            for (j = 0; j <= nscatter - 1; j++)
            {
                nj = scatternodes[j];
                for (i = 0; i <= nx - 1; i++)
                {
                    atsctr[nwrk + i, j] = xx[nj, i];
                }
            }
            for (j = 0; j <= nscatter - 1; j++)
            {
                atsctr[nwrk + nx, j] = 1.0;
            }
        }
    }


    /*************************************************************************
    ACBF preconditioner generation basecase.

    PARAMETERS:
        Builder             -   ACBF builder object
        Wrk0, Wrk1          -   elements [Wrk0...Wrk1-1] of Builder.WrkIdx[]
                                array store row indexes of XX that are processed.
        
    OUTPUT:
        Builder.OutputPool is updated with new chunks

      -- ALGLIB --
         Copyright 12.12.2021 by Sergey Bochkanov
    *************************************************************************/
    private static void computeacbfpreconditionerbasecase(acbfbuilder builder,
        acbfbuffer buf,
        int wrk0,
        int wrk1,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        int widx = 0;
        int targetidx = 0;
        int nx = 0;
        int nglobal = 0;
        int nlocal = 0;
        int ncorrection = 0;
        int ncenters = 0;
        int nchosen = 0;
        int ncoeff = 0;
        int batchsize = 0;
        int nk = 0;
        int nq = 0;
        double[] x = new double[0];
        double[] batchcenter = new double[0];
        int[] idummy = new int[0];
        double localrad = 0;
        double currentrad = 0;
        double reg = 0;
        double v = 0;
        double vv = 0;
        double mx = 0;
        double maxdist2 = 0;
        int ortbasissize = 0;
        int[] ortbasismap = new int[0];
        acbfchunk precchunk = null;
        int expansionscount = 0;
        double[,] dbgb = new double[0, 0];
        double dbgerrnodes = 0;
        double dbgerrort = 0;
        double dbgcondq = 0;
        double dbgmaxc = 0;

        if (wrk1 <= wrk0)
        {
            return;
        }
        nx = builder.nx;
        nglobal = builder.nglobal;
        nlocal = builder.nlocal;
        ncorrection = builder.ncorrection;
        reg = Math.Sqrt(math.machineepsilon);
        ablasf.rallocv(nx, ref x, _params);
        expansionscount = 0;

        //
        // First, select a batch of central points and compute batch center
        //
        batchsize = wrk1 - wrk0;
        ablasf.iallocv(batchsize, ref buf.currentnodes, _params);
        ablasf.rsetallocv(nx, 0.0, ref batchcenter, _params);
        for (i = 0; i <= batchsize - 1; i++)
        {
            targetidx = builder.wrkidx[wrk0 + i];
            buf.currentnodes[i] = targetidx;
            buf.bflags[targetidx] = true;
            ablasf.raddrv(nx, (double)1 / (double)batchsize, builder.xx, builder.wrkidx[wrk0 + i], batchcenter, _params);
        }
        ncenters = batchsize;

        //
        // Then, add a hull of nearest neighbors and compute its radius
        //
        localrad = 0;
        for (widx = 0; widx <= batchsize - 1; widx++)
        {

            //
            // Select immediate neighbors
            //
            ablasf.rcopyrv(nx, builder.xx, builder.wrkidx[wrk0 + widx], x, _params);
            nq = nearestneighbor.kdtreetsqueryknn(builder.kdt, buf.kdtbuf, x, nlocal, true, _params);
            nearestneighbor.kdtreetsqueryresultstags(builder.kdt, buf.kdtbuf, ref buf.neighbors, _params);
            nearestneighbor.kdtreetsqueryresultsdistances(builder.kdt, buf.kdtbuf, ref buf.d, _params);
            for (k = 0; k <= nq - 1; k++)
            {
                nk = buf.neighbors[k];
                if (!buf.bflags[nk])
                {
                    buf.bflags[nk] = true;
                    ablasf.igrowv(ncenters + 1, ref buf.currentnodes, _params);
                    buf.currentnodes[ncenters] = nk;
                    ncenters = ncenters + 1;
                    v = 0;
                    for (j = 0; j <= nx - 1; j++)
                    {
                        v = v + (builder.xx[nk, j] - batchcenter[j]) * (builder.xx[nk, j] - batchcenter[j]);
                    }
                    localrad = Math.Max(localrad, v);
                }
            }
        }
        localrad = Math.Sqrt(localrad);
        currentrad = localrad;

        //
        // Add global grid
        //
        if (nglobal > 0)
        {
            for (k = 0; k <= nglobal - 1; k++)
            {
                nk = builder.globalgrid[k];
                if (!buf.bflags[nk])
                {
                    buf.bflags[nk] = true;
                    ablasf.igrowv(ncenters + 1, ref buf.currentnodes, _params);
                    buf.currentnodes[ncenters] = nk;
                    ncenters = ncenters + 1;
                }
            }
        }

        //
        // Add local correction grid: select more distant neighbors
        //
        while ((ncorrection > 0 && (double)(currentrad) > (double)(0)) && (double)(currentrad) < (double)(builder.roughdatasetdiameter))
        {

            //
            // Select neighbors within CurrentRad*Builder.CorrectorGrowth
            //
            if (expansionscount == 0)
            {

                //
                // First expansion, use simplified kd-tree #1
                //
                nq = nearestneighbor.kdtreetsqueryrnn(builder.kdt1, buf.kdt1buf, batchcenter, currentrad * builder.correctorgrowth, true, _params);
                nearestneighbor.kdtreetsqueryresultstags(builder.kdt1, buf.kdt1buf, ref buf.neighbors, _params);
            }
            else
            {

                //
                // Subsequent expansions, use simplified kd-tree #2
                //
                nq = nearestneighbor.kdtreetsqueryrnn(builder.kdt2, buf.kdt2buf, batchcenter, currentrad * builder.correctorgrowth, true, _params);
                nearestneighbor.kdtreetsqueryresultstags(builder.kdt2, buf.kdt2buf, ref buf.neighbors, _params);
            }

            //
            // Compute a grid of well-separated nodes using neighbors
            //
            ablasf.rallocm(nq, nx, ref buf.xq, _params);
            for (k = 0; k <= nq - 1; k++)
            {
                nk = buf.neighbors[k];
                for (j = 0; j <= nx - 1; j++)
                {
                    buf.xq[k, j] = builder.xx[nk, j];
                }
            }
            selectglobalnodes(buf.xq, nq, nx, idummy, 0, ncorrection, ref buf.chosenneighbors, ref nchosen, ref maxdist2, _params);

            //
            // Select neighbrs that are NOT within CurrentRad from the batch center
            // and that are NOT already chosen.
            //
            for (k = 0; k <= nchosen - 1; k++)
            {
                nk = buf.neighbors[buf.chosenneighbors[k]];
                v = 0;
                for (j = 0; j <= nx - 1; j++)
                {
                    v = v + math.sqr(builder.xx[nk, j] - batchcenter[j]);
                }
                v = Math.Sqrt(v);
                if (!buf.bflags[nk] && (double)(v) > (double)(currentrad))
                {
                    buf.bflags[nk] = true;
                    ablasf.igrowv(ncenters + 1, ref buf.currentnodes, _params);
                    buf.currentnodes[ncenters] = nk;
                    ncenters = ncenters + 1;
                }
            }

            //
            // Update radius and debug counters
            //
            currentrad = currentrad * builder.correctorgrowth;
            apserv.inc(ref expansionscount, _params);
        }

        //
        // Clean up bFlags[]
        //
        for (k = 0; k <= ncenters - 1; k++)
        {
            buf.bflags[buf.currentnodes[k]] = false;
        }

        //
        // Compute working set and scatter set design matrices ATWrk and ATSctr
        //
        // ATWrk  is a (NWrk+NX+1)*NWrk matrix whose entries a[i,j] store:
        // * for I<NWrk             BasisFunc(X[wrk[i]]-X[wrj[j]])
        // * for NWrk<=I<NWrk+NX    X[wrk[j]], coordinate #(i-NWrk)
        // * for I=NWrk+NX          1.0
        //
        // ATSctr is a (NWrk+NX+1)*NScatter matrix whose entries a[i,j] store:
        // * for I<NWrk             BasisFunc(X[wrk[i]]-X[scatter[j]])
        // * for NWrk<=I<NWrk+NX    X[scatter[j]], coordinate #(i-NWrk)
        // * for I=NWrk+NX          1.0
        //
        computetargetscatterdesignmatrices(builder.xx, builder.ntotal, nx, builder.functype, builder.funcparam, buf.currentnodes, ncenters, buf.currentnodes, 0, ref buf.atwrk, ref buf.atwrk, _params);

        //
        // Prepare and solve linear system, coefficients are stored in rows of Buf.B
        //
        // Depending on whether the basis is conditionally positive definite (given current polynomial term type),
        // we either:
        // * use generic QR solver to solve the linear system (when the basis is not CPD)
        // * use specialized CPD solver that is several times faster and is more accurate
        //
        if (builder.aterm != 1 || !iscpdfunction(builder.functype, builder.aterm, _params))
        {
            ap.assert((double)(builder.lambdav) >= (double)(0), "RBF3: integrity check 8363 failed");
            ap.assert((builder.aterm == 1 || builder.aterm == 2) || builder.aterm == 3, "RBF3: integrity check 8364 failed");

            //
            // Basis function has no conditional positive definiteness guarantees (given the linear term type).
            //
            // Solve using QR decomposition.
            //
            ncoeff = ncenters + nx + 1;
            ablasf.rsetallocm(ncoeff, ncoeff + batchsize, 0.0, ref buf.q, _params);
            for (i = 0; i <= ncenters - 1; i++)
            {
                ablasf.rcopyrr(ncenters, buf.atwrk, i, buf.q, i, _params);
                buf.q[i, i] = buf.q[i, i] + builder.lambdav;
            }
            if (builder.aterm == 1)
            {

                //
                // Linear term is used
                //
                for (i = 0; i <= nx; i++)
                {
                    for (j = 0; j <= ncenters - 1; j++)
                    {
                        buf.q[ncenters + i, j] = buf.atwrk[ncenters + i, j];
                        buf.q[j, ncenters + i] = buf.atwrk[ncenters + i, j];
                    }
                }
            }
            if (builder.aterm == 2)
            {

                //
                // Constant term is used
                //
                for (i = 0; i <= nx - 1; i++)
                {
                    buf.q[ncenters + i, ncenters + i] = 1.0;
                }
                for (j = 0; j <= ncenters - 1; j++)
                {
                    buf.q[ncenters + nx, j] = 1.0;
                    buf.q[j, ncenters + nx] = 1.0;
                }
            }
            if (builder.aterm == 3)
            {

                //
                // Zero term is used
                //
                for (i = 0; i <= nx; i++)
                {
                    buf.q[ncenters + i, ncenters + i] = 1.0;
                }
            }
            for (i = 0; i <= batchsize - 1; i++)
            {
                buf.q[i, ncoeff + i] = 1.0;
            }
            mx = 1.0;
            for (i = 0; i <= ncoeff - 1; i++)
            {
                for (j = i; j <= ncoeff - 1; j++)
                {
                    mx = Math.Max(mx, Math.Abs(buf.q[i, j]));
                }
            }
            for (j = 0; j <= ncoeff - 1; j++)
            {
                buf.q[j, j] = buf.q[j, j] + reg * mx * apserv.possign(buf.q[j, j], _params);
            }
            ortfac.rmatrixqr(buf.q, ncoeff, ncoeff + batchsize, ref buf.tau, _params);
            ablasf.rallocm(batchsize, ncoeff, ref buf.b, _params);
            ablas.rmatrixtranspose(ncoeff, batchsize, buf.q, 0, ncoeff, buf.b, 0, 0, _params);
            ablas.rmatrixrighttrsm(batchsize, ncoeff, buf.q, 0, 0, true, false, 1, buf.b, 0, 0, _params);
        }
        else
        {
            ap.assert((double)(builder.lambdav) >= (double)(0), "RBF3: integrity check 8368 failed");
            ap.assert(builder.aterm == 1, "RBF3: integrity check 7365 failed");
            ncoeff = ncenters + nx + 1;

            //
            // First, compute orthogonal basis of space spanned by polynomials of degree 1
            //
            ablasf.rallocm(ncenters, ncenters, ref buf.r, _params);
            ablasf.rallocm(nx + 1, ncenters, ref buf.q1, _params);
            ablasf.iallocv(nx + 1, ref ortbasismap, _params);
            ablasf.rsetr(ncenters, 1 / Math.Sqrt(ncenters), buf.q1, 0, _params);
            buf.r[0, 0] = Math.Sqrt(ncenters);
            ortbasismap[0] = nx;
            ortbasissize = 1;
            ablasf.rallocv(ncenters, ref buf.z, _params);
            for (k = 0; k <= nx - 1; k++)
            {
                for (j = 0; j <= ncenters - 1; j++)
                {
                    buf.z[j] = buf.atwrk[ncenters + k, j];
                }
                v = Math.Sqrt(ablasf.rdotv2(ncenters, buf.z, _params));
                ablas.rowwisegramschmidt(buf.q1, ortbasissize, ncenters, buf.z, ref buf.y, true, _params);
                vv = Math.Sqrt(ablasf.rdotv2(ncenters, buf.z, _params));
                if ((double)(vv) > (double)(Math.Sqrt(math.machineepsilon) * (v + 1)))
                {
                    ablasf.rcopymulvr(ncenters, 1 / vv, buf.z, buf.q1, ortbasissize, _params);
                    ablasf.rcopyvc(ortbasissize, buf.y, buf.r, ortbasissize, _params);
                    buf.r[ortbasissize, ortbasissize] = vv;
                    ortbasismap[ortbasissize] = k;
                    ortbasissize = ortbasissize + 1;
                }
            }

            //
            // Second, compute system matrix Q and target values for cardinal basis functions B.
            //
            // The Q is conditionally positive definite, i.e. x'*Q*x>0 for any x satisfying orthogonality conditions
            // (orthogonal with respect to basis stored in Q1).
            //
            ablasf.rsetallocm(ncenters, ncenters, 0.0, ref buf.q, _params);
            for (i = 0; i <= ncenters - 1; i++)
            {
                ablasf.rcopyrr(ncenters, buf.atwrk, i, buf.q, i, _params);
            }
            ablasf.rsetallocm(batchsize, ncoeff, 0.0, ref buf.b, _params);
            for (i = 0; i <= batchsize - 1; i++)
            {
                buf.b[i, i] = 1.0;
            }
            for (i = 0; i <= ncenters - 1; i++)
            {
                buf.q[i, i] = buf.q[i, i] + builder.lambdav;
            }

            //
            // Transform Q from conditionally positive definite to the (simply) positive definite one:
            // multiply linear system Q*x=RHS from both sides by (I-Q1'*Q1), apply additional regularization.
            //
            // NOTE: RHS is also multiplied by (I-Q1'*Q1), but from the left only.
            //
            ablasf.rallocv(ncenters, ref buf.z, _params);
            for (i = 0; i <= ncenters - 1; i++)
            {
                ablasf.rcopyrv(ncenters, buf.q, i, buf.z, _params);
                ablas.rowwisegramschmidt(buf.q1, ortbasissize, ncenters, buf.z, ref buf.y, false, _params);
                ablasf.rcopyvr(ncenters, buf.z, buf.q, i, _params);
            }
            for (i = 0; i <= ncenters - 1; i++)
            {
                ablasf.rcopycv(ncenters, buf.q, i, buf.z, _params);
                ablas.rowwisegramschmidt(buf.q1, ortbasissize, ncenters, buf.z, ref buf.y, false, _params);
                ablasf.rcopyvc(ncenters, buf.z, buf.q, i, _params);
            }
            for (i = 0; i <= batchsize - 1; i++)
            {
                ablasf.rcopyrv(ncenters, buf.b, i, buf.z, _params);
                ablas.rowwisegramschmidt(buf.q1, ortbasissize, ncenters, buf.z, ref buf.y, false, _params);
                ablasf.rcopyvr(ncenters, buf.z, buf.b, i, _params);
            }
            mx = 1.0;
            for (i = 0; i <= ncenters - 1; i++)
            {
                mx = Math.Max(mx, Math.Abs(buf.q[i, i]));
            }
            for (i = 0; i <= ncenters - 1; i++)
            {
                ablasf.rcopyrv(ncenters, buf.q, i, buf.z, _params);
                for (j = 0; j <= ortbasissize - 1; j++)
                {
                    ablasf.raddrv(ncenters, mx * buf.q1[j, i], buf.q1, j, buf.z, _params);
                }
                ablasf.rcopyvr(ncenters, buf.z, buf.q, i, _params);
            }
            if (builder.dodetailedtrace)
            {

                //
                // Compute condition number for future reports
                //
                dbgcondq = 1 / (rcond.spdmatrixrcond(buf.q, ncenters, false, _params) + math.machineepsilon);
            }
            else
            {
                dbgcondq = 0;
            }
            for (i = 0; i <= ncenters - 1; i++)
            {
                buf.q[i, i] = buf.q[i, i] + reg * mx;
            }

            //
            // Perform Cholesky factorization, solve and obtain RBF coefficients (we still have
            // to compute polynomial term - it will be done later)
            //
            if (!trfac.spdmatrixcholeskyrec(buf.q, 0, ncenters, false, ref buf.choltmp, _params))
            {
                ap.assert(false, "RBFV3: ACBF solver failed due to extreme degeneracy");
            }
            ablas.rmatrixrighttrsm(batchsize, ncenters, buf.q, 0, 0, false, false, 1, buf.b, 0, 0, _params);
            ablas.rmatrixrighttrsm(batchsize, ncenters, buf.q, 0, 0, false, false, 0, buf.b, 0, 0, _params);

            //
            // Now, having RBF coefficients we can compute residual from fitting ACBF targets
            // with pure RBF term and fit polynomial term to this residual. In the ideal world
            // it should result in the nice and precise polynomial coefficients.
            //
            ablasf.rallocv(ncenters, ref buf.z, _params);
            ablasf.rsetallocm(batchsize, ncenters, 0.0, ref buf.c, _params);
            for (i = 0; i <= batchsize - 1; i++)
            {
                buf.c[i, i] = 1.0;
            }
            ablas.rmatrixgemm(batchsize, ncenters, ncenters, -1.0, buf.b, 0, 0, 0, buf.atwrk, 0, 0, 1, 1.0, buf.c, 0, 0, _params);
            for (i = 0; i <= batchsize - 1; i++)
            {
                ablasf.rcopyrv(ncenters, buf.c, i, buf.z, _params);
                ablas.rowwisegramschmidt(buf.q1, ortbasissize, ncenters, buf.z, ref buf.y, true, _params);
                ablas.rmatrixtrsv(ortbasissize, buf.r, 0, 0, true, false, 0, buf.y, 0, _params);
                for (j = 0; j <= nx; j++)
                {
                    buf.b[i, ncenters + j] = 0.0;
                }
                for (j = 0; j <= ortbasissize - 1; j++)
                {
                    buf.b[i, ncenters + ortbasismap[j]] = buf.y[j];
                }
            }

            //
            // Trace if needeed
            //
            if (builder.dodetailedtrace)
            {
                ablasf.rallocm(batchsize, ncenters, ref dbgb, _params);
                ablas.rmatrixgemm(batchsize, ncenters, ncoeff, -1.0, buf.b, 0, 0, 0, buf.atwrk, 0, 0, 0, 0.0, dbgb, 0, 0, _params);
                for (i = 0; i <= batchsize - 1; i++)
                {
                    dbgb[i, i] = dbgb[i, i] + 1.0;
                }
                dbgmaxc = 0;
                for (i = 0; i <= batchsize - 1; i++)
                {
                    dbgmaxc = Math.Max(dbgmaxc, ablasf.rmaxabsr(ncenters, buf.b, i, _params));
                }
                dbgerrnodes = 0;
                for (i = 0; i <= batchsize - 1; i++)
                {
                    dbgerrnodes = dbgerrnodes + ablasf.rdotrr(ncenters, dbgb, i, dbgb, i, _params);
                }
                dbgerrnodes = Math.Sqrt(dbgerrnodes / (batchsize * ncenters));
                dbgerrort = 0;
                for (i = 0; i <= batchsize - 1; i++)
                {
                    for (j = 0; j <= ortbasissize - 1; j++)
                    {
                        dbgerrort = Math.Max(dbgerrort, Math.Abs(ablasf.rdotrr(ncenters, buf.b, i, buf.q1, j, _params)));
                    }
                }
                ap.trace(System.String.Format("[ACBF_subprob]    BatchSize={0,3:d}    NCenters={1,4:d}    RadiusExpansions={2,0:d}    cond(Q)={3,0:E2}    max|C|={4,0:E2}    rmsErr={5,0:E2}    OrtErr={6,0:E2}\n", batchsize, ncenters, expansionscount, dbgcondq, dbgmaxc, dbgerrnodes, dbgerrort));
            }
        }

        //
        // Solve and save solution to Builder.ChunksPool
        //
        smp.ae_shared_pool_retrieve(builder.chunksproducer, ref precchunk);
        ap.assert(precchunk.ntargetrows == -117, "RBFV3: integrity check 9724 failed");
        ap.assert(precchunk.ntargetcols == -119, "RBFV3: integrity check 9725 failed");
        precchunk.ntargetrows = batchsize;
        precchunk.ntargetcols = ncoeff;
        ablasf.iallocv(precchunk.ntargetrows, ref precchunk.targetrows, _params);
        ablasf.iallocv(precchunk.ntargetcols, ref precchunk.targetcols, _params);
        ablasf.rallocm(batchsize, ncoeff, ref precchunk.s, _params);
        for (widx = 0; widx <= batchsize - 1; widx++)
        {
            precchunk.targetrows[widx] = builder.wrkidx[wrk0 + widx];
        }
        ablasf.iallocv(ncoeff, ref buf.perm, _params);
        for (k = 0; k <= ncoeff - 1; k++)
        {
            if (k < ncenters)
            {
                precchunk.targetcols[k] = buf.currentnodes[k];
            }
            else
            {
                precchunk.targetcols[k] = builder.ntotal + (k - ncenters);
            }
            buf.perm[k] = k;
        }
        tsort.tagsortmiddleii(ref precchunk.targetcols, ref buf.perm, 0, ncoeff, _params);
        for (widx = 0; widx <= batchsize - 1; widx++)
        {
            for (k = 0; k <= ncoeff - 1; k++)
            {
                precchunk.s[widx, k] = buf.b[widx, buf.perm[k]];
            }
        }
        smp.ae_shared_pool_recycle(builder.chunkspool, ref precchunk);
    }


    /*************************************************************************
    Recursive ACBF preconditioner generation subroutine.

    PARAMETERS:
        Builder             -   ACBF builder object
        Wrk0, Wrk1          -   elements [Wrk0...Wrk1-1] of Builder.WrkIdx[]
                                array store row indexes of XX that are processed.
        
    OUTPUT:
        Builder.OutputPool is updated with new chunks

      -- ALGLIB --
         Copyright 12.12.2021 by Sergey Bochkanov
    *************************************************************************/
    private static void computeacbfpreconditionerrecv2(acbfbuilder builder,
        int wrk0,
        int wrk1,
        xparams _params)
    {
        int nx = 0;
        int i = 0;
        int j = 0;
        int k0 = 0;
        int k1 = 0;
        int largestdim = 0;
        double splitval = 0;
        double basecasecomplexity = 0;
        acbfbuffer buf = null;

        nx = builder.nx;
        if (wrk1 <= wrk0)
        {
            return;
        }
        basecasecomplexity = apserv.rmul3(builder.nglobal + builder.nlocal + 2 * builder.ncorrection, builder.nglobal + builder.nlocal + 2 * builder.ncorrection, builder.nglobal + builder.nlocal + 2 * builder.ncorrection, _params);

        //
        // Decide on parallelism
        //
        if ((double)(apserv.rmul2(builder.ntotal, basecasecomplexity, _params)) >= (double)(apserv.smpactivationlevel(_params)) && builder.ntotal >= acbfparallelthreshold)
        {
            if (_trypexec_computeacbfpreconditionerrecv2(builder, wrk0, wrk1, _params))
            {
                return;
            }
        }

        //
        // Retrieve temporary buffer
        //
        smp.ae_shared_pool_retrieve(builder.bufferpool, ref buf);

        //
        // Analyze current working set
        //
        ablasf.rallocv(nx, ref buf.tmpboxmin, _params);
        ablasf.rallocv(nx, ref buf.tmpboxmax, _params);
        ablasf.rcopyrv(nx, builder.xx, builder.wrkidx[wrk0], buf.tmpboxmin, _params);
        ablasf.rcopyrv(nx, builder.xx, builder.wrkidx[wrk0], buf.tmpboxmax, _params);
        for (i = wrk0 + 1; i <= wrk1 - 1; i++)
        {
            for (j = 0; j <= nx - 1; j++)
            {
                buf.tmpboxmin[j] = Math.Min(buf.tmpboxmin[j], builder.xx[builder.wrkidx[i], j]);
                buf.tmpboxmax[j] = Math.Max(buf.tmpboxmax[j], builder.xx[builder.wrkidx[i], j]);
            }
        }
        largestdim = 0;
        for (j = 1; j <= nx - 1; j++)
        {
            if ((double)(buf.tmpboxmax[j] - buf.tmpboxmin[j]) > (double)(buf.tmpboxmax[largestdim] - buf.tmpboxmin[largestdim]))
            {
                largestdim = j;
            }
        }

        //
        // Perform either batch processing or recursive split
        //
        if (wrk1 - wrk0 <= builder.batchsize || (double)(buf.tmpboxmax[largestdim]) == (double)(buf.tmpboxmin[largestdim]))
        {

            //
            // Either working set size is small enough or all points are non-distinct.
            // Perform batch processing
            //
            computeacbfpreconditionerbasecase(builder, buf, wrk0, wrk1, _params);

            //
            // Recycle temporary buffers
            //
            smp.ae_shared_pool_recycle(builder.bufferpool, ref buf);
        }
        else
        {

            //
            // Compute recursive split along largest axis
            //
            splitval = 0.5 * (buf.tmpboxmax[largestdim] + buf.tmpboxmin[largestdim]);
            k0 = wrk0;
            k1 = wrk1 - 1;
            while (k0 <= k1)
            {
                if ((double)(builder.xx[builder.wrkidx[k0], largestdim]) <= (double)(splitval))
                {
                    k0 = k0 + 1;
                    continue;
                }
                if ((double)(builder.xx[builder.wrkidx[k1], largestdim]) > (double)(splitval))
                {
                    k1 = k1 - 1;
                    continue;
                }
                apserv.swapelementsi(builder.wrkidx, k0, k1, _params);
                k0 = k0 + 1;
                k1 = k1 - 1;
            }
            ap.assert(k0 > wrk0 && k1 < wrk1 - 1, "ACBF: integrity check 2843 in the recursive subdivision code failed");
            ap.assert(k0 == k1 + 1, "ACBF: integrity check 8364 in the recursive subdivision code failed");

            //
            // Recycle temporary buffer, perform recursive calls
            //
            smp.ae_shared_pool_recycle(builder.bufferpool, ref buf);
            computeacbfpreconditionerrecv2(builder, wrk0, k0, _params);
            computeacbfpreconditionerrecv2(builder, k0, wrk1, _params);
        }
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_computeacbfpreconditionerrecv2(acbfbuilder builder,
        int wrk0,
        int wrk1, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    This function generates ACBF (approximate cardinal basis functions)
    preconditioner.

    PARAMETERS:
        XX                  -   dataset (X-values), array[N,NX]
        N                   -   points count, N>=1
        NX                  -   dimensions count, NX>=1
        FuncType            -   basis function type
        
    OUTPUT:
        SP                  -   preconditioner, sparse matrix in CRS format

      -- ALGLIB --
         Copyright 12.12.2021 by Sergey Bochkanov
    *************************************************************************/
    private static void computeacbfpreconditioner(double[,] xx,
        int n,
        int nx,
        int functype,
        double funcparam,
        int aterm,
        int batchsize,
        int nglobal,
        int nlocal,
        int ncorrection,
        int correctorgrowth,
        int simplificationfactor,
        double lambdav,
        sparse.sparsematrix sp,
        xparams _params)
    {
        acbfbuilder builder = new acbfbuilder();
        acbfbuffer bufferseed = new acbfbuffer();
        acbfchunk chunkseed = new acbfchunk();
        acbfchunk precchunk = null;
        int i = 0;
        int j = 0;
        int offs = 0;
        int[] idummy = new int[0];
        int[] rowsizes = new int[0];
        double[] boxmin = new double[0];
        double[] boxmax = new double[0];

        ap.assert(n >= 1, "RBFV3: integrity check 2524 failed");

        //
        // Prepare builder
        //
        builder.dodetailedtrace = ap.istraceenabled("RBF.DETAILED", _params);
        builder.functype = functype;
        builder.funcparam = funcparam;
        builder.ntotal = n;
        builder.nx = nx;
        builder.batchsize = batchsize;
        if (nglobal > 0)
        {
            selectglobalnodes(xx, n, nx, idummy, 0, nglobal, ref builder.globalgrid, ref builder.nglobal, ref builder.globalgridseparation, _params);
        }
        else
        {
            builder.nglobal = 0;
        }
        builder.nlocal = nlocal;
        builder.ncorrection = ncorrection;
        builder.correctorgrowth = correctorgrowth;
        builder.lambdav = lambdav;
        builder.aterm = aterm;
        ablasf.rcopyallocm(n, nx, xx, ref builder.xx, _params);
        ablasf.rallocv(nx, ref boxmin, _params);
        ablasf.rallocv(nx, ref boxmax, _params);
        ablasf.rcopyrv(nx, xx, 0, boxmin, _params);
        ablasf.rcopyrv(nx, xx, 0, boxmax, _params);
        for (i = 1; i <= n - 1; i++)
        {
            ablasf.rmergeminrv(nx, xx, i, boxmin, _params);
            ablasf.rmergemaxrv(nx, xx, i, boxmax, _params);
        }
        builder.roughdatasetdiameter = 0;
        for (i = 0; i <= nx - 1; i++)
        {
            builder.roughdatasetdiameter = builder.roughdatasetdiameter + math.sqr(boxmax[i] - boxmin[i]);
        }
        builder.roughdatasetdiameter = Math.Sqrt(builder.roughdatasetdiameter);
        ablasf.iallocv(n, ref builder.wrkidx, _params);
        for (i = 0; i <= n - 1; i++)
        {
            builder.wrkidx[i] = i;
        }
        nearestneighbor.kdtreebuildtagged(xx, builder.wrkidx, n, nx, 0, 2, builder.kdt, _params);
        buildsimplifiedkdtree(xx, n, nx, (int)Math.Round(Math.Pow(simplificationfactor, nx)), (int)Math.Round(Math.Pow(5, nx)), builder.kdt1, _params);
        buildsimplifiedkdtree(xx, n, nx, (int)Math.Round(Math.Pow(simplificationfactor, 2 * nx)), (int)Math.Round(Math.Pow(5, nx)), builder.kdt2, _params);
        ablasf.bsetallocv(n, false, ref bufferseed.bflags, _params);
        ablasf.rallocv(nx, ref bufferseed.tmpboxmin, _params);
        ablasf.rallocv(nx, ref bufferseed.tmpboxmax, _params);
        nearestneighbor.kdtreecreaterequestbuffer(builder.kdt, bufferseed.kdtbuf, _params);
        nearestneighbor.kdtreecreaterequestbuffer(builder.kdt1, bufferseed.kdt1buf, _params);
        nearestneighbor.kdtreecreaterequestbuffer(builder.kdt2, bufferseed.kdt2buf, _params);
        smp.ae_shared_pool_set_seed(builder.bufferpool, bufferseed);
        chunkseed.ntargetrows = -117;
        chunkseed.ntargetcols = -119;
        smp.ae_shared_pool_set_seed(builder.chunksproducer, chunkseed);
        smp.ae_shared_pool_set_seed(builder.chunkspool, chunkseed);

        //
        // Prepare preconditioner matrix
        //
        computeacbfpreconditionerrecv2(builder, 0, n, _params);
        ablasf.isetallocv(n, -1, ref rowsizes, _params);
        smp.ae_shared_pool_first_recycled(builder.chunkspool, ref precchunk);
        while (precchunk != null)
        {
            for (i = 0; i <= precchunk.ntargetrows - 1; i++)
            {
                ap.assert(rowsizes[precchunk.targetrows[i]] == -1, "RBFV3: integrity check 2568 failed");
                rowsizes[precchunk.targetrows[i]] = precchunk.ntargetcols;
            }
            smp.ae_shared_pool_next_recycled(builder.chunkspool, ref precchunk);
        }
        sp.matrixtype = 1;
        sp.m = n + nx + 1;
        sp.n = n + nx + 1;
        ablasf.iallocv(n + nx + 2, ref sp.ridx, _params);
        sp.ridx[0] = 0;
        for (i = 0; i <= n - 1; i++)
        {
            ap.assert(rowsizes[i] > 0, "RBFV3: integrity check 2668 failed");
            sp.ridx[i + 1] = sp.ridx[i] + rowsizes[i];
        }
        for (i = n; i <= n + nx; i++)
        {
            sp.ridx[i + 1] = sp.ridx[i] + 1;
        }
        ablasf.iallocv(sp.ridx[sp.m], ref sp.idx, _params);
        ablasf.rallocv(sp.ridx[sp.m], ref sp.vals, _params);
        for (i = n; i <= n + nx; i++)
        {
            sp.idx[sp.ridx[i]] = i;
            sp.vals[sp.ridx[i]] = 1.0;
        }
        smp.ae_shared_pool_first_recycled(builder.chunkspool, ref precchunk);
        while (precchunk != null)
        {
            for (i = 0; i <= precchunk.ntargetrows - 1; i++)
            {
                offs = sp.ridx[precchunk.targetrows[i]];
                for (j = 0; j <= precchunk.ntargetcols - 1; j++)
                {
                    sp.idx[offs + j] = precchunk.targetcols[j];
                    sp.vals[offs + j] = precchunk.s[i, j];
                }
            }
            smp.ae_shared_pool_next_recycled(builder.chunkspool, ref precchunk);
        }
        sp.ninitialized = sp.ridx[sp.m];
        sparse.sparseinitduidx(sp, _params);
    }


    /*************************************************************************
    Basecase initialization routine for DDM solver.


    Appends an instance of RBF3DDMSubproblem to Solver.SubproblemsPool.

    INPUT PARAMETERS:
        Solver      -   solver object. This function may be  called  from  the
                        multiple threads, so it  is  important  to  work  with
                        Solver object using only thread-safe functions.
        X           -   array[N,NX], dataset points
        N, NX       -   dataset metrics, N>0, NX>0
        BFMatrix    -   basis function matrix object
        LambdaV     -   smoothing parameter
        SP          -   sparse ACBF preconditioner, (N+NX+1)*(N+NX+1) matrix
                        stored in CRS format
        Buf         -   an instance of RBF3DDMBuffer, reusable temporary buffers
        TgtIdx      -   array[], contains indexes of points in the current target
                        set. Elements [Tgt0,Tgt1) are processed by this function.
        NNeighbors  -   neighbors count; NNeighbors nearby nodes are added  to
                        inner points of the chunk
        DoDetailedTrace-whether trace output is needed or not. When trace is
                        activated, solver computes condition numbers. It results
                        in the several-fold slowdown of the algorithm.

      -- ALGLIB --
         Copyright 12.12.2021 by Sergey Bochkanov
    *************************************************************************/
    private static void ddmsolverinitbasecase(rbf3ddmsolver solver,
        double[,] x,
        int n,
        int nx,
        rbf3evaluator bfmatrix,
        double lambdav,
        sparse.sparsematrix sp,
        rbf3ddmbuffer buf,
        int[] tgtidx,
        int tgt0,
        int tgt1,
        int nneighbors,
        bool dodetailedtrace,
        xparams _params)
    {
        rbf3ddmsubproblem subproblem = null;
        int i = 0;
        int j = 0;
        int k = 0;
        int nc = 0;
        int nk = 0;
        double v = 0;
        double reg = 0;
        int nwrk = 0;
        int npreccol = 0;
        int[] neighbors = new int[0];
        int[] workingnodes = new int[0];
        int[] preccolumns = new int[0];
        double[] tau = new double[0];
        double[,] q = new double[0, 0];
        double[] x0 = new double[0];
        double lurcond = 0;
        bool lusuccess = new bool();
        int ni = 0;
        int j0 = 0;
        int j1 = 0;
        int jj = 0;
        double[,] suba = new double[0, 0];
        double[,] subsp = new double[0, 0];
        double[,] dbga = new double[0, 0];

        ap.assert(tgt1 - tgt0 > 0, "RBFV3: integrity check 7364 failed");
        ap.assert(nneighbors >= 0, "RBFV3: integrity check 7365 failed");
        reg = (100 + Math.Sqrt(tgt1 - tgt0 + nneighbors)) * math.machineepsilon;

        //
        // Retrieve fresh subproblem. We expect that Solver.SubproblemsBuffer contains
        // no recycled entries and that fresh subproblem with Subproblem.IsValid=False
        // is returned.
        //
        // Start initialization
        //
        smp.ae_shared_pool_retrieve(solver.subproblemsbuffer, ref subproblem);
        ap.assert(!subproblem.isvalid, "RBFV3: SubproblemsBuffer integrity check failed");
        subproblem.isvalid = true;
        subproblem.ntarget = tgt1 - tgt0;
        ablasf.iallocv(tgt1 - tgt0, ref subproblem.targetnodes, _params);
        ablasf.icopyvx(tgt1 - tgt0, tgtidx, tgt0, subproblem.targetnodes, 0, _params);

        //
        // Prepare working arrays
        //
        ablasf.rallocv(nx, ref x0, _params);

        //
        // Determine working set: target nodes + neighbors of targets.
        // Prepare mapping from node index to position in WorkingNodes[]
        //
        nwrk = 0;
        ablasf.iallocv(tgt1 - tgt0, ref workingnodes, _params);
        for (i = tgt0; i <= tgt1 - 1; i++)
        {
            nk = tgtidx[i];
            buf.bflags[nk] = true;
            workingnodes[nwrk] = nk;
            nwrk = nwrk + 1;
        }
        for (i = tgt0; i <= tgt1 - 1; i++)
        {
            ablasf.rcopyrv(nx, x, tgtidx[i], x0, _params);
            nc = nearestneighbor.kdtreetsqueryknn(solver.kdt, buf.kdtbuf, x0, nneighbors + 1, true, _params);
            nearestneighbor.kdtreetsqueryresultstags(solver.kdt, buf.kdtbuf, ref neighbors, _params);
            for (k = 0; k <= nc - 1; k++)
            {
                nk = neighbors[k];
                if (!buf.bflags[nk])
                {
                    buf.bflags[nk] = true;
                    ablasf.igrowv(nwrk + 1, ref workingnodes, _params);
                    workingnodes[nwrk] = nk;
                    nwrk = nwrk + 1;
                }
            }
        }
        for (i = 0; i <= nwrk - 1; i++)
        {
            buf.bflags[workingnodes[i]] = false;
        }
        ap.assert(nwrk > 0, "ACBF: integrity check for NWrk failed");
        subproblem.nwork = nwrk;
        ablasf.icopyallocv(nwrk, workingnodes, ref subproblem.workingnodes, _params);

        //
        // Determine preconditioner columns that have nonzeros in rows corresponding
        // to working nodes. Prepare mapping from [0,N+NX+1) column indexing to [0,NPrecCol)
        // compressed one. Only these columns are extracted from the preconditioner
        // during design system computation.
        //
        // NOTE: we ensure that preconditioner columns N...N+NX which correspond to linear
        //       terms are placed last. It greatly simplifies desi
        //
        npreccol = 0;
        for (i = 0; i <= nwrk - 1; i++)
        {
            j0 = sp.ridx[workingnodes[i]];
            j1 = sp.ridx[workingnodes[i] + 1] - 1;
            for (jj = j0; jj <= j1; jj++)
            {
                j = sp.idx[jj];
                if (j < n && !buf.bflags[j])
                {
                    buf.bflags[j] = true;
                    ablasf.igrowv(npreccol + 1, ref preccolumns, _params);
                    preccolumns[npreccol] = j;
                    npreccol = npreccol + 1;
                }
            }
        }
        for (j = n; j <= n + nx; j++)
        {
            ap.assert(!buf.bflags[j], "RBFV3: integrity check 9435 failed");
            buf.bflags[j] = true;
            ablasf.igrowv(npreccol + 1, ref preccolumns, _params);
            preccolumns[npreccol] = j;
            npreccol = npreccol + 1;
        }
        for (i = 0; i <= npreccol - 1; i++)
        {
            buf.idx2preccol[preccolumns[i]] = i;
            buf.bflags[preccolumns[i]] = false;
        }

        //
        // Generate working system, apply regularization
        //
        ablasf.rsetallocm(nwrk, npreccol, 0.0, ref suba, _params);
        modelmatrixcomputepartial(bfmatrix, workingnodes, nwrk, preccolumns, npreccol - (nx + 1), ref suba, _params);
        for (i = 0; i <= nwrk - 1; i++)
        {
            ni = workingnodes[i];
            for (j = 0; j <= nx - 1; j++)
            {
                suba[i, npreccol - (nx + 1) + j] = x[ni, j];
            }
            suba[i, npreccol - 1] = 1.0;
        }
        for (i = 0; i <= nwrk - 1; i++)
        {
            j = buf.idx2preccol[workingnodes[i]];
            suba[i, j] = suba[i, j] + lambdav;
        }
        ablasf.rsetallocm(nwrk, npreccol, 0.0, ref subsp, _params);
        for (i = 0; i <= nwrk - 1; i++)
        {
            ni = workingnodes[i];
            j0 = sp.ridx[ni];
            j1 = sp.ridx[ni + 1] - 1;
            for (jj = j0; jj <= j1; jj++)
            {
                subsp[i, buf.idx2preccol[sp.idx[jj]]] = sp.vals[jj];
            }
        }
        ablasf.rallocm(nwrk, nwrk, ref subproblem.regsystem, _params);
        ablas.rmatrixgemm(nwrk, nwrk, npreccol, 1.0, suba, 0, 0, 0, subsp, 0, 0, 1, 0.0, subproblem.regsystem, 0, 0, _params);

        //
        // Try solving with LU decomposition
        //
        ablasf.rcopyallocm(nwrk, nwrk, subproblem.regsystem, ref subproblem.wrklu, _params);
        trfac.rmatrixlu(subproblem.wrklu, nwrk, nwrk, ref subproblem.wrkp, _params);
        lurcond = rcond.rmatrixlurcondinf(subproblem.wrklu, nwrk, _params);
        if ((double)(lurcond) > (double)(Math.Sqrt(math.machineepsilon)))
        {

            //
            // LU success
            //
            subproblem.decomposition = 0;
            lusuccess = true;
            if (dodetailedtrace)
            {
                ap.trace(System.String.Format(">> DDM subproblem:  LU success, |target|={0,4:d},  |wrk|={1,4:d},  |preccol|={2,4:d}, cond(LU)={3,0:E2}\n", tgt1 - tgt0, nwrk, npreccol, 1 / (lurcond + math.machineepsilon)));
            }
        }
        else
        {
            lusuccess = false;
        }

        //
        // Apply regularized QR if needed
        //
        if (!lusuccess)
        {
            ablasf.rsetallocm(2 * nwrk, nwrk, 0.0, ref subproblem.wrkr, _params);
            ablasf.rcopym(nwrk, nwrk, subproblem.regsystem, subproblem.wrkr, _params);
            v = Math.Sqrt(reg);
            for (i = 0; i <= nwrk - 1; i++)
            {
                subproblem.wrkr[nwrk + i, i] = v;
            }
            ortfac.rmatrixqr(subproblem.wrkr, 2 * nwrk, nwrk, ref tau, _params);
            ortfac.rmatrixqrunpackq(subproblem.wrkr, 2 * nwrk, nwrk, tau, nwrk, ref subproblem.wrkq, _params);
            subproblem.decomposition = 1;
            if (dodetailedtrace)
            {
                ap.trace(System.String.Format(">> DDM subproblem:  LU failure, using reg-QR, |target|={0,4:d},  |wrk|={1,4:d},  |preccol|={2,4:d}, cond(R)={3,0:E2} (cond(LU)={4,0:E2})\n", tgt1 - tgt0, nwrk, npreccol, 1 / (rcond.rmatrixtrrcondinf(subproblem.wrkr, nwrk, true, false, _params) + math.machineepsilon), 1 / (lurcond + math.machineepsilon)));
            }
        }

        //
        // Subproblem is ready.
        // Move it to the SubproblemsPool
        //
        smp.ae_shared_pool_recycle(solver.subproblemspool, ref subproblem);
    }


    /*************************************************************************
    Recursive initialization routine for DDM solver

    INPUT PARAMETERS:
        Solver      -   solver structure
        X           -   array[N,NX], dataset points
        N, NX       -   dataset metrics, N>0, NX>0
        BFMatrix    -   basis function evaluator
        LambdaV     -   smoothing parameter
        SP          -   sparse ACBF preconditioner, (N+NX+1)*(N+NX+1) matrix
                        stored in CRS format
        WrkIdx      -   array[], contains indexes of points in the current working
                        set. Elements [Wrk0,Wrk1) are processed by this function.
        NNeighbors  -   neighbors count; NNeighbors nearby nodes are added  to
                        inner points of the chunk
        NBatch      -   batch size
        DoDetailedTrace-whether trace output is needed or not. When trace is
                        activated, solver computes condition numbers. It results
                        in the several-fold slowdown of the algorithm.

      -- ALGLIB --
         Copyright 12.12.2021 by Sergey Bochkanov
    *************************************************************************/
    private static void ddmsolverinitrec(rbf3ddmsolver solver,
        double[,] x,
        int n,
        int nx,
        rbf3evaluator bfmatrix,
        double lambdav,
        sparse.sparsematrix sp,
        int[] wrkidx,
        int wrk0,
        int wrk1,
        int nneighbors,
        int nbatch,
        bool dodetailedtrace,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k0 = 0;
        int k1 = 0;
        int largestdim = 0;
        double splitval = 0;
        double basecasecomplexity = 0;
        rbf3ddmbuffer buf = null;

        if (wrk1 <= wrk0)
        {
            return;
        }
        basecasecomplexity = apserv.rmul3(nbatch + nneighbors + nx + 1, nbatch + nneighbors + nx + 1, nbatch + nneighbors + nx + 1, _params);

        //
        // Decide on parallelism
        //
        if (((double)(basecasecomplexity * ((double)n / (double)nbatch)) >= (double)(apserv.smpactivationlevel(_params)) && (double)((double)n / (double)nbatch) >= (double)(2)) && n >= ddmparallelthreshold)
        {
            if (_trypexec_ddmsolverinitrec(solver, x, n, nx, bfmatrix, lambdav, sp, wrkidx, wrk0, wrk1, nneighbors, nbatch, dodetailedtrace, _params))
            {
                return;
            }
        }

        //
        // Retrieve temporary buffer
        //
        smp.ae_shared_pool_retrieve(solver.bufferpool, ref buf);

        //
        // Analyze current working set
        //
        ablasf.rallocv(nx, ref buf.tmpboxmin, _params);
        ablasf.rallocv(nx, ref buf.tmpboxmax, _params);
        ablasf.rcopyrv(nx, x, wrkidx[wrk0], buf.tmpboxmin, _params);
        ablasf.rcopyrv(nx, x, wrkidx[wrk0], buf.tmpboxmax, _params);
        for (i = wrk0 + 1; i <= wrk1 - 1; i++)
        {
            for (j = 0; j <= nx - 1; j++)
            {
                buf.tmpboxmin[j] = Math.Min(buf.tmpboxmin[j], x[wrkidx[i], j]);
                buf.tmpboxmax[j] = Math.Max(buf.tmpboxmax[j], x[wrkidx[i], j]);
            }
        }
        largestdim = 0;
        for (j = 1; j <= nx - 1; j++)
        {
            if ((double)(buf.tmpboxmax[j] - buf.tmpboxmin[j]) > (double)(buf.tmpboxmax[largestdim] - buf.tmpboxmin[largestdim]))
            {
                largestdim = j;
            }
        }

        //
        // Perform either batch processing or recursive split
        //
        if (wrk1 - wrk0 <= nbatch || (double)(buf.tmpboxmax[largestdim]) == (double)(buf.tmpboxmin[largestdim]))
        {

            //
            // Either working set size is small enough or all points are non-distinct.
            // Stop recursive subdivision.
            //
            ddmsolverinitbasecase(solver, x, n, nx, bfmatrix, lambdav, sp, buf, wrkidx, wrk0, wrk1, nneighbors, dodetailedtrace, _params);

            //
            // Recycle temporary buffers
            //
            smp.ae_shared_pool_recycle(solver.bufferpool, ref buf);
        }
        else
        {

            //
            // Compute recursive split along largest axis
            //
            splitval = 0.5 * (buf.tmpboxmax[largestdim] + buf.tmpboxmin[largestdim]);
            k0 = wrk0;
            k1 = wrk1 - 1;
            while (k0 <= k1)
            {
                if ((double)(x[wrkidx[k0], largestdim]) <= (double)(splitval))
                {
                    k0 = k0 + 1;
                    continue;
                }
                if ((double)(x[wrkidx[k1], largestdim]) > (double)(splitval))
                {
                    k1 = k1 - 1;
                    continue;
                }
                apserv.swapelementsi(wrkidx, k0, k1, _params);
                k0 = k0 + 1;
                k1 = k1 - 1;
            }
            ap.assert(k0 > wrk0 && k1 < wrk1 - 1, "ACBF: integrity check 2843 in the recursive subdivision code failed");
            ap.assert(k0 == k1 + 1, "ACBF: integrity check 8364 in the recursive subdivision code failed");

            //
            // Recycle temporary buffer, perform recursive calls
            //
            smp.ae_shared_pool_recycle(solver.bufferpool, ref buf);
            ddmsolverinitrec(solver, x, n, nx, bfmatrix, lambdav, sp, wrkidx, wrk0, k0, nneighbors, nbatch, dodetailedtrace, _params);
            ddmsolverinitrec(solver, x, n, nx, bfmatrix, lambdav, sp, wrkidx, k0, wrk1, nneighbors, nbatch, dodetailedtrace, _params);
        }
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_ddmsolverinitrec(rbf3ddmsolver solver,
        double[,] x,
        int n,
        int nx,
        rbf3evaluator bfmatrix,
        double lambdav,
        sparse.sparsematrix sp,
        int[] wrkidx,
        int wrk0,
        int wrk1,
        int nneighbors,
        int nbatch,
        bool dodetailedtrace, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    This function prepares domain decomposition method for  RBF  interpolation
    problem  -  it  partitions  problem  into   subproblems   and  precomputes
    factorizations, and prepares a smaller correction spline that is  used  to
    correct distortions introduced by domain decomposition  and  imperfections
    in approximate cardinal basis.

    INPUT PARAMETERS:
        X           -   array[N,NX], dataset points
        RescaledBy  -   additional scaling coefficient that was applied to the
                        dataset by preprocessor. Used ONLY for logging purposes
                        - without it all distances will be reported  in  [0,1]
                        scale, not one set by user.
        N, NX       -   dataset metrics, N>0, NX>0
        BFMatrix    -   RBF evaluator
        BFType      -   basis function type
        BFParam     -   basis function parameter
        LambdaV     -   regularization parameter, >=0
        ATerm       -   polynomial term type (1 for linear, 2 for constant, 3 for zero)
        SP          -   sparse ACBF preconditioner, (N+NX+1)*(N+NX+1) matrix
                        stored in CRS format
        NNeighbors  -   neighbors count; NNeighbors nearby nodes are added  to
                        inner points of the batch
        NBatch      -   batch size
        NCorrector  -   nodes count for correction spline
        DoTrace     -   whether low overhead logging is needed or not
        DoDetailedTrace-whether detailed trace output is needed or not. When trace is
                        activated, solver computes condition numbers. It results
                        in the small slowdown of the algorithm.
        
    OUTPUT PARAMETERS:
        Solver      -   DDM solver
        timeDDMInit-    time used by the DDM part initialization, ms
        timeCorrInit-   time used by the corrector initialization, ms

      -- ALGLIB --
         Copyright 12.12.2021 by Sergey Bochkanov
    *************************************************************************/
    private static void ddmsolverinit(double[,] x,
        double rescaledby,
        int n,
        int nx,
        rbf3evaluator bfmatrix,
        int bftype,
        double bfparam,
        double lambdav,
        int aterm,
        sparse.sparsematrix sp,
        int nneighbors,
        int nbatch,
        int ncorrector,
        bool dotrace,
        bool dodetailedtrace,
        rbf3ddmsolver solver,
        ref int timeddminit,
        ref int timecorrinit,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int[] idx = new int[0];
        rbf3ddmbuffer bufferseed = new rbf3ddmbuffer();
        rbf3ddmsubproblem subproblem = new rbf3ddmsubproblem();
        rbf3ddmsubproblem p = null;
        double correctorgridseparation = 0;
        double[,] corrsys = new double[0, 0];
        double[] corrtau = new double[0];
        int[] idummy = new int[0];
        double reg = 0;
        int nsys = 0;

        timeddminit = 0;
        timecorrinit = 0;

        ap.assert((aterm == 1 || aterm == 2) || aterm == 3, "RBF3: integrity check 3320 failed");

        //
        // Start DDM part
        //
        timeddminit = unchecked((int)(System.DateTime.UtcNow.Ticks / 10000));

        //
        // Save problem info
        //
        solver.lambdav = lambdav;

        //
        // Prepare KD-tree
        //
        ablasf.iallocv(n, ref idx, _params);
        for (i = 0; i <= n - 1; i++)
        {
            idx[i] = i;
        }
        nearestneighbor.kdtreebuildtagged(x, idx, n, nx, 0, 2, solver.kdt, _params);

        //
        // Prepare temporary buffer pool
        //
        ablasf.bsetallocv(n + nx + 1, false, ref bufferseed.bflags, _params);
        ablasf.iallocv(n + nx + 1, ref bufferseed.idx2preccol, _params);
        ablasf.rallocv(nx, ref bufferseed.tmpboxmin, _params);
        ablasf.rallocv(nx, ref bufferseed.tmpboxmax, _params);
        nearestneighbor.kdtreecreaterequestbuffer(solver.kdt, bufferseed.kdtbuf, _params);
        smp.ae_shared_pool_set_seed(solver.bufferpool, bufferseed);

        //
        // Prepare default subproblems buffer, run recursive procedure
        // and count subproblems in the buffer
        //
        subproblem.isvalid = false;
        smp.ae_shared_pool_set_seed(solver.subproblemspool, subproblem);
        smp.ae_shared_pool_set_seed(solver.subproblemsbuffer, subproblem);
        ddmsolverinitrec(solver, x, n, nx, bfmatrix, solver.lambdav, sp, idx, 0, n, nneighbors, nbatch, dodetailedtrace, _params);
        solver.subproblemscnt = 0;
        solver.cntlu = 0;
        solver.cntregqr = 0;
        smp.ae_shared_pool_first_recycled(solver.subproblemspool, ref p);
        while (p != null)
        {
            solver.subproblemscnt = solver.subproblemscnt + 1;
            if (p.decomposition == 0)
            {
                apserv.inc(ref solver.cntlu, _params);
            }
            if (p.decomposition == 1)
            {
                apserv.inc(ref solver.cntregqr, _params);
            }
            smp.ae_shared_pool_next_recycled(solver.subproblemspool, ref p);
        }
        ap.assert(solver.cntlu + solver.cntregqr == solver.subproblemscnt, "RBFV3: integrity check 5296 failed");
        ap.assert(solver.subproblemscnt > 0, "RBFV3: subproblems pool is empty, critical integrity check failed");

        //
        // DDM part is done
        //
        timeddminit = unchecked((int)(System.DateTime.UtcNow.Ticks / 10000)) - timeddminit;
        if (dotrace)
        {
            ap.trace(System.String.Format("> DDM part was prepared in {0,0:d} ms, {1,0:d} subproblems solved ({2,0:d} well-conditioned, {3,0:d} ill-conditioned)\n", timeddminit, solver.subproblemscnt, solver.cntlu, solver.cntregqr));
        }

        //
        // Prepare correction spline
        //
        timecorrinit = unchecked((int)(System.DateTime.UtcNow.Ticks / 10000));
        selectglobalnodes(x, n, nx, idummy, 0, ncorrector, ref solver.corrnodes, ref solver.ncorrector, ref correctorgridseparation, _params);
        ncorrector = solver.ncorrector;
        ap.assert(ncorrector > 0, "RBFV3: NCorrector=0");
        nsys = ncorrector + nx + 1;
        ablasf.rsetallocm(2 * nsys, nsys, 0.0, ref corrsys, _params);
        ablasf.rallocm(ncorrector, nx, ref solver.corrx, _params);
        for (i = 0; i <= ncorrector - 1; i++)
        {
            ablasf.rcopyrr(nx, x, solver.corrnodes[i], solver.corrx, i, _params);
        }
        computebfmatrix(solver.corrx, ncorrector, nx, bftype, bfparam, ref corrsys, _params);
        if (aterm == 1)
        {

            //
            // Use linear term
            //
            for (i = 0; i <= nx - 1; i++)
            {
                for (j = 0; j <= ncorrector - 1; j++)
                {
                    corrsys[ncorrector + i, j] = x[solver.corrnodes[j], i];
                    corrsys[j, ncorrector + i] = x[solver.corrnodes[j], i];
                }
            }
            for (j = 0; j <= ncorrector - 1; j++)
            {
                corrsys[ncorrector + nx, j] = 1.0;
                corrsys[j, ncorrector + nx] = 1.0;
            }
        }
        if (aterm == 2)
        {

            //
            // Use constant term
            //
            for (i = 0; i <= nx - 1; i++)
            {
                corrsys[ncorrector + i, ncorrector + i] = 1.0;
            }
            for (j = 0; j <= ncorrector - 1; j++)
            {
                corrsys[ncorrector + nx, j] = 1.0;
                corrsys[j, ncorrector + nx] = 1.0;
            }
        }
        if (aterm == 3)
        {

            //
            // Use zero term
            //
            for (i = 0; i <= nx; i++)
            {
                corrsys[ncorrector + i, ncorrector + i] = 1.0;
            }
        }
        for (j = 0; j <= ncorrector - 1; j++)
        {
            corrsys[j, j] = corrsys[j, j] + solver.lambdav;
        }
        reg = 1.0;
        for (i = 0; i <= nsys - 1; i++)
        {
            reg = Math.Max(reg, ablasf.rmaxabsr(nsys, corrsys, i, _params));
        }
        reg = Math.Sqrt(math.machineepsilon) * reg;
        for (j = 0; j <= nsys - 1; j++)
        {
            corrsys[nsys + j, j] = reg;
        }
        ortfac.rmatrixqr(corrsys, 2 * nsys, nsys, ref corrtau, _params);
        ortfac.rmatrixqrunpackq(corrsys, 2 * nsys, nsys, corrtau, nsys, ref solver.corrq, _params);
        ortfac.rmatrixqrunpackr(corrsys, 2 * nsys, nsys, ref solver.corrr, _params);
        timecorrinit = unchecked((int)(System.DateTime.UtcNow.Ticks / 10000)) - timecorrinit;
        if (dotrace)
        {
            ap.trace(System.String.Format("> Corrector spline was prepared in {0,0:d} ms ({1,0:d} nodes, max distance from dataset points to nearest grid node is {2,0:E2})\n", timecorrinit, ncorrector, correctorgridseparation * rescaledby));
        }
        if (dodetailedtrace)
        {
            ap.trace("> printing condition numbers for correction spline:\n");
            ap.trace(System.String.Format("cond(A)     = {0,0:E2} (Linf norm, leading NCoarsexNCoarse block)\n", 1 / (rcond.rmatrixtrrcondinf(solver.corrr, ncorrector, true, false, _params) + math.machineepsilon)));
            ap.trace(System.String.Format("cond(A)     = {0,0:E2} (Linf norm, full system)\n", 1 / (rcond.rmatrixtrrcondinf(solver.corrr, nsys, true, false, _params) + math.machineepsilon)));
        }
    }


    /*************************************************************************
    Recursive subroutine for DDM method. Given initial subproblems count  Cnt,
    it perform two recursive calls (spawns children in parallel when possible)
    with Cnt~Cnt/2 until we end up with Cnt=1.

    Case with Cnt=1 is handled by retrieving subproblem from Solver.SubproblemsPool,
    solving it and pushing subproblem to Solver.SubproblemsBuffer.

    INPUT PARAMETERS:
        Solver      -   DDM solver object
        Res         -   array[N,NY], current residuals
        N, NX, NY   -   dataset metrics, N>0, NX>0, NY>0
        C           -   preallocated array[N+NX+1,NY]
        Cnt         -   number of subproblems to process
        
    OUTPUT PARAMETERS:
        C           -   rows 0..N-1  contain spline coefficients
                        rows N..N+NX are filled by zeros

      -- ALGLIB --
         Copyright 12.12.2021 by Sergey Bochkanov
    *************************************************************************/
    private static void ddmsolverrunrec(rbf3ddmsolver solver,
        double[,] res,
        int n,
        int nx,
        int ny,
        double[,] c,
        int cnt,
        xparams _params)
    {
        int nwrk = 0;
        int ntarget = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        double v = 0;
        rbf3ddmsubproblem subproblem = null;


        //
        // Run recursive procedure if needed
        //
        if (cnt > 1)
        {
            k = cnt / 2;
            ap.assert(k <= cnt - k, "RBFV3: integrity check 2733 failed");
            ddmsolverrunrec(solver, res, n, nx, ny, c, cnt - k, _params);
            ddmsolverrunrec(solver, res, n, nx, ny, c, k, _params);
            return;
        }

        //
        // Retrieve subproblem from the source pool, solve it
        //
        smp.ae_shared_pool_retrieve(solver.subproblemspool, ref subproblem);
        ap.assert(subproblem != null && subproblem.isvalid, "RBFV3: integrity check 1742 failed");
        nwrk = subproblem.nwork;
        ntarget = subproblem.ntarget;
        if (subproblem.decomposition == 0)
        {

            //
            // Solve using LU decomposition (the fastest option)
            //
            ablasf.rallocm(nwrk, ny, ref subproblem.rhs, _params);
            for (i = 0; i <= nwrk - 1; i++)
            {
                for (j = 0; j <= ny - 1; j++)
                {
                    subproblem.rhs[i, j] = res[subproblem.workingnodes[i], j];
                }
            }
            for (i = 0; i <= nwrk - 1; i++)
            {
                if (subproblem.wrkp[i] != i)
                {
                    for (j = 0; j <= ny - 1; j++)
                    {
                        v = subproblem.rhs[i, j];
                        subproblem.rhs[i, j] = subproblem.rhs[subproblem.wrkp[i], j];
                        subproblem.rhs[subproblem.wrkp[i], j] = v;
                    }
                }
            }
            ablas.rmatrixlefttrsm(nwrk, ny, subproblem.wrklu, 0, 0, false, true, 0, subproblem.rhs, 0, 0, _params);
            ablas.rmatrixlefttrsm(nwrk, ny, subproblem.wrklu, 0, 0, true, false, 0, subproblem.rhs, 0, 0, _params);
            ablasf.rcopyallocm(nwrk, ny, subproblem.rhs, ref subproblem.sol, _params);
        }
        else
        {

            //
            // Solve using regularized QR (well, we tried LU but it failed)
            //
            ap.assert(subproblem.decomposition == 1, "RBFV3: integrity check 1743 failed");
            ablasf.rallocm(nwrk, ny, ref subproblem.rhs, _params);
            for (i = 0; i <= nwrk - 1; i++)
            {
                for (j = 0; j <= ny - 1; j++)
                {
                    subproblem.rhs[i, j] = res[subproblem.workingnodes[i], j];
                }
            }
            ablasf.rallocm(nwrk, ny, ref subproblem.qtrhs, _params);
            ablas.rmatrixgemm(nwrk, ny, nwrk, 1.0, subproblem.wrkq, 0, 0, 1, subproblem.rhs, 0, 0, 0, 0.0, subproblem.qtrhs, 0, 0, _params);
            ablas.rmatrixlefttrsm(nwrk, ny, subproblem.wrkr, 0, 0, true, false, 0, subproblem.qtrhs, 0, 0, _params);
            ablasf.rcopyallocm(nwrk, ny, subproblem.qtrhs, ref subproblem.sol, _params);
        }
        for (i = 0; i <= ntarget - 1; i++)
        {
            for (j = 0; j <= ny - 1; j++)
            {
                c[subproblem.targetnodes[i], j] = subproblem.sol[i, j];
            }
        }

        //
        // Push to the destination pool
        //
        smp.ae_shared_pool_recycle(solver.subproblemsbuffer, ref subproblem);
    }


    /*************************************************************************
    This function APPROXIMATELY solves RBF interpolation problem using  domain
    decomposition method. Given current residuals Res, it computes approximate
    basis function coefficients C (but does  NOT  compute linear  coefficients
    - these are set to zero).

    This function is a linear operator with respect to its input RES, thus  it
    can be used as a preconditioner for an iterative linear solver like GMRES.

    INPUT PARAMETERS:
        Solver      -   DDM solver object
        Res         -   array[N,NY], current residuals
        N, NX, NY   -   dataset metrics, N>0, NX>0, NY>0
        SP          -   preconditioner, (N+NX+1)*(N+NX+1) sparse matrix
        BFMatrix    -   basis functions evaluator
        C           -   preallocated array[N+NX+1,NY]
        timeDDMSolve,
        timeCorrSolve-  on input contain already accumulated timings
                        for DDM and CORR parts
        
    OUTPUT PARAMETERS:
        C           -   rows 0..N-1  contain spline coefficients
                        rows N..N+NX are filled by zeros
        timeDDMSolve,
        timeCorrSolve-  updated with new timings
        

      -- ALGLIB --
         Copyright 12.12.2021 by Sergey Bochkanov
    *************************************************************************/
    private static void ddmsolverrun(rbf3ddmsolver solver,
        double[,] res,
        int n,
        int nx,
        int ny,
        sparse.sparsematrix sp,
        rbf3evaluator bfmatrix,
        rbf3fastevaluator fasteval,
        double fastevaltol,
        ref double[,] upd,
        ref int timeddmsolve,
        ref int timecorrsolve,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        rbf3ddmsubproblem subproblem = null;
        double[,] c = new double[0, 0];
        double[] x0 = new double[0];
        double[] x1 = new double[0];
        double[] refrhs1 = new double[0];
        double[,] corrpred = new double[0, 0];
        double[,] updt = new double[0, 0];

        ablasf.rsetallocm(ny, n + nx + 1, 0.0, ref updt, _params);
        ablasf.rsetallocm(n + nx + 1, ny, 0.0, ref c, _params);
        for (j = 0; j <= ny - 1; j++)
        {
            for (i = n; i <= n + nx; i++)
            {
                c[i, j] = 0;
            }
        }

        //
        // Solve DDM part:
        // * run recursive procedure that computes DDM part.
        // * clean-up: move processed subproblems from Solver.SubproblemsBuffer back to Solver.SubproblemsPool
        // * multiply solution by the preconditioner matrix
        //
        timeddmsolve = timeddmsolve - unchecked((int)(System.DateTime.UtcNow.Ticks / 10000));
        ddmsolverrunrec(solver, res, n, nx, ny, c, solver.subproblemscnt, _params);
        for (i = 0; i <= solver.subproblemscnt - 1; i++)
        {
            smp.ae_shared_pool_retrieve(solver.subproblemsbuffer, ref subproblem);
            ap.assert(subproblem != null && subproblem.isvalid, "RBFV3: integrity check 5223 failed");
            smp.ae_shared_pool_recycle(solver.subproblemspool, ref subproblem);
        }
        timeddmsolve = timeddmsolve + unchecked((int)(System.DateTime.UtcNow.Ticks / 10000));
        ablasf.rallocv(n + nx + 1, ref x0, _params);
        ablasf.rallocv(n + nx + 1, ref x1, _params);
        for (j = 0; j <= ny - 1; j++)
        {
            ablasf.rcopycv(n + nx + 1, c, j, x0, _params);
            sparse.sparsegemv(sp, 1.0, 1, x0, 0, 0.0, x1, 0, _params);
            ablasf.rcopyvr(n + nx + 1, x1, updt, j, _params);
        }

        //
        // Compute correction spline that fixes oscillations introduced by the DDM part
        //
        timecorrsolve = timecorrsolve - unchecked((int)(System.DateTime.UtcNow.Ticks / 10000));
        ablasf.rallocv(solver.ncorrector + nx + 1, ref x0, _params);
        ablasf.rallocv(n + nx + 1, ref x1, _params);
        for (j = 0; j <= ny - 1; j++)
        {

            //
            // Prepare right-hand side for the QR solver
            //
            ablasf.rsetallocm(1, solver.ncorrector + nx + 1, 0.0, ref corrpred, _params);
            ablasf.rsetallocv(solver.ncorrector + nx + 1, 0.0, ref refrhs1, _params);
            ablasf.rcopyrv(n + nx + 1, updt, j, x1, _params);
            fastevaluatorloadcoeffs1(fasteval, x1, _params);
            fastevaluatorpushtol(fasteval, fastevaltol, _params);
            fastevaluatorcomputebatch(fasteval, solver.corrx, solver.ncorrector, true, ref corrpred, _params);
            for (i = 0; i <= solver.ncorrector - 1; i++)
            {
                refrhs1[i] = res[solver.corrnodes[i], j] - corrpred[0, i];
                for (k = 0; k <= nx - 1; k++)
                {
                    refrhs1[i] = refrhs1[i] - solver.corrx[i, k] * x1[n + k];
                }
                refrhs1[i] = refrhs1[i] - x1[n + nx];
                refrhs1[i] = refrhs1[i] - solver.lambdav * x1[solver.corrnodes[i]];
            }

            //
            // Solve QR-factorized system
            //
            ablasf.rgemv(solver.ncorrector + nx + 1, solver.ncorrector + nx + 1, 1.0, solver.corrq, 1, refrhs1, 0.0, x0, _params);
            ablas.rmatrixtrsv(solver.ncorrector + nx + 1, solver.corrr, 0, 0, true, false, 0, x0, 0, _params);
            for (i = 0; i <= solver.ncorrector - 1; i++)
            {
                updt[j, solver.corrnodes[i]] = updt[j, solver.corrnodes[i]] + x0[i];
            }
            for (i = 0; i <= nx; i++)
            {
                updt[j, n + i] = updt[j, n + i] + x0[solver.ncorrector + i];
            }
        }
        timecorrsolve = timecorrsolve + unchecked((int)(System.DateTime.UtcNow.Ticks / 10000));
        ablasf.rallocm(n + nx + 1, ny, ref upd, _params);
        ablas.rmatrixtranspose(ny, n + nx + 1, updt, 0, 0, upd, 0, 0, _params);
    }


    /*************************************************************************
    This function is a specialized version of DDMSolverRun() for NY=1.

      -- ALGLIB --
         Copyright 12.12.2021 by Sergey Bochkanov
    *************************************************************************/
    private static void ddmsolverrun1(rbf3ddmsolver solver,
        double[] res,
        int n,
        int nx,
        sparse.sparsematrix sp,
        rbf3evaluator bfmatrix,
        rbf3fastevaluator fasteval,
        double fastevaltol,
        ref double[] upd,
        ref int timeddmsolve,
        ref int timecorrsolve,
        xparams _params)
    {
        ablasf.rallocm(n, 1, ref solver.tmpres1, _params);
        ablasf.rcopyvc(n, res, solver.tmpres1, 0, _params);
        ddmsolverrun(solver, solver.tmpres1, n, nx, 1, sp, bfmatrix, fasteval, fastevaltol, ref solver.tmpupd1, ref timeddmsolve, ref timecorrsolve, _params);
        ablasf.rallocv(n + nx + 1, ref upd, _params);
        ablasf.rcopycv(n + nx + 1, solver.tmpupd1, 0, upd, _params);
    }


    /*************************************************************************
    Automatically detect scale parameter as a mean distance towards nearest
    neighbor (not counting nearest neighbors that are too close)

    PARAMETERS:
        XX                  -   dataset (X-values), array[N,NX]
        N                   -   points count, N>=1
        NX                  -   dimensions count, NX>=1
        
    RESULT:
        suggested scale

      -- ALGLIB --
         Copyright 12.12.2021 by Sergey Bochkanov
    *************************************************************************/
    private static double autodetectscaleparameter(double[,] xx,
        int n,
        int nx,
        xparams _params)
    {
        double result = 0;
        int i = 0;
        int j = 0;
        int nq = 0;
        int nlocal = 0;
        nearestneighbor.kdtree kdt = new nearestneighbor.kdtree();
        double[] x = new double[0];
        double[] d = new double[0];

        ap.assert(n >= 1, "RBFV3: integrity check 7624 failed");
        ablasf.rallocv(nx, ref x, _params);
        nearestneighbor.kdtreebuild(xx, n, nx, 0, 2, kdt, _params);
        nlocal = (int)Math.Round(Math.Pow(2, nx) + 1);
        result = 0;
        for (i = 0; i <= n - 1; i++)
        {

            //
            // Query a batch of nearest neighbors
            //
            ablasf.rcopyrv(nx, xx, i, x, _params);
            nq = nearestneighbor.kdtreequeryknn(kdt, x, nlocal, true, _params);
            ap.assert(nq >= 1, "RBFV3: integrity check 7625 failed");
            nearestneighbor.kdtreequeryresultsdistances(kdt, ref d, _params);

            //
            // In order to filter out nearest neighbors that are too close,
            // we use distance R toward most distant of NQ nearest neighbors as
            // a reference and select nearest neighbor with distance >=0.5*R/NQ
            //
            for (j = 0; j <= nq - 1; j++)
            {
                if ((double)(d[j]) >= (double)(0.5 * d[nq - 1] / nq))
                {
                    result = result + d[j];
                    break;
                }
            }
        }
        result = result / n;
        return result;
    }


    /*************************************************************************
    Recursive functions matrix computation

      -- ALGLIB --
         Copyright 12.12.2021 by Sergey Bochkanov
    *************************************************************************/
    private static void computebfmatrixrec(double[,] xx,
        int range0,
        int range1,
        int n,
        int nx,
        int functype,
        double funcparam,
        double[,] f,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        double v = 0;
        double vv = 0;
        double elemcost = 0;
        double alpha2 = 0;

        ap.assert((functype == 1 || functype == 2) || functype == 3, "RBFV3.ComputeTransposedDesignSystem: unexpected FuncType");

        //
        // Try to parallelize
        //
        elemcost = 10.0;
        if (((range0 == 0 && range1 == n) && (double)(0.5 * apserv.rmul3(n, n, elemcost, _params)) >= (double)(apserv.smpactivationlevel(_params))) && n >= bfparallelthreshold)
        {
            if (_trypexec_computebfmatrixrec(xx, range0, range1, n, nx, functype, funcparam, f, _params))
            {
                return;
            }
        }

        //
        // Try recursive splits
        //
        if (range1 - range0 > 16)
        {
            k = range0 + (range1 - range0) / 2;
            computebfmatrixrec(xx, range0, k, n, nx, functype, funcparam, f, _params);
            computebfmatrixrec(xx, k, range1, n, nx, functype, funcparam, f, _params);
            return;
        }

        //
        // Serial processing
        //
        alpha2 = funcparam * funcparam;
        for (i = range0; i <= range1 - 1; i++)
        {
            for (j = i; j <= n - 1; j++)
            {
                v = 0;
                for (k = 0; k <= nx - 1; k++)
                {
                    vv = xx[i, k] - xx[j, k];
                    v = v + vv * vv;
                }
                if (functype == 1)
                {
                    v = -Math.Sqrt(v + alpha2);
                }
                if (functype == 2)
                {
                    if (v != 0.0)
                    {
                        v = v * 0.5 * Math.Log(v);
                    }
                    else
                    {
                        v = 0.0;
                    }
                }
                if (functype == 3)
                {
                    v = v * Math.Sqrt(v);
                }
                f[i, j] = v;
                f[j, i] = v;
            }
        }
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_computebfmatrixrec(double[,] xx,
        int range0,
        int range1,
        int n,
        int nx,
        int functype,
        double funcparam,
        double[,] f, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    This function computes  basis  functions  matrix  (both  upper  and  lower
    triangles)


          [ f(dist(x0,x0))     ... f(dist(x0,x(n-1)))     ]
          [ f(dist(x1,x0))     ... f(dist(x1,x(n-1)))     ]
          [ ............................................. ]
          [ f(dist(x(n-1),x0)) ... f(dist(x(n-1),x(n-1))) ]
          
    NOTE: if F is large enough to store result, it is not reallocated. Values
          outside of [0,N)x[0,N) range are not modified.

      -- ALGLIB --
         Copyright 12.12.2021 by Sergey Bochkanov
    *************************************************************************/
    private static void computebfmatrix(double[,] xx,
        int n,
        int nx,
        int functype,
        double funcparam,
        ref double[,] f,
        xparams _params)
    {
        ablasf.rallocm(n, n, ref f, _params);
        computebfmatrixrec(xx, 0, n, n, nx, functype, funcparam, f, _params);
    }


    /*************************************************************************
    Initializes model matrix using specified matrix storage format:
    * StorageType=0     a N*N matrix of basis function values is stored 
    * StorageType=1     basis function values are recomputed on demand

      -- ALGLIB --
         Copyright 12.12.2021 by Sergey Bochkanov
    *************************************************************************/
    private static void modelmatrixinit(double[,] xx,
        int n,
        int nx,
        int functype,
        double funcparam,
        int storagetype,
        rbf3evaluator modelmatrix,
        xparams _params)
    {
        int nchunks = 0;
        int i = 0;
        int j = 0;
        int srcoffs = 0;
        int dstoffs = 0;
        int curlen = 0;
        rbf3evaluatorbuffer bufseed = new rbf3evaluatorbuffer();

        ap.assert(storagetype == 0 || storagetype == 1, "RBFV3: unexpected StorageType for ModelMatrixInit()");
        modelmatrix.n = n;
        modelmatrix.storagetype = storagetype;
        if (storagetype == 0)
        {
            computebfmatrix(xx, n, nx, functype, funcparam, ref modelmatrix.f, _params);
            return;
        }
        if (storagetype == 1)
        {

            //
            // Save model parameters
            //
            modelmatrix.nx = nx;
            modelmatrix.functype = functype;
            modelmatrix.funcparam = funcparam;
            modelmatrix.chunksize = 128;

            //
            // Prepare temporary buffers
            //
            smp.ae_shared_pool_set_seed(modelmatrix.bufferpool, bufseed);
            ablasf.rsetallocv(modelmatrix.chunksize, 1.0, ref modelmatrix.chunk1, _params);

            //
            // Store dataset in the chunked row storage format (rows with size at most ChunkSize, one row per dimension/chunk)
            //
            ablasf.iallocv(n, ref modelmatrix.entireset, _params);
            for (i = 0; i <= n - 1; i++)
            {
                modelmatrix.entireset[i] = i;
            }
            ablasf.rcopyallocm(n, nx, xx, ref modelmatrix.x, _params);
            nchunks = apserv.idivup(n, modelmatrix.chunksize, _params);
            ablasf.rsetallocm(nchunks * nx, modelmatrix.chunksize, 0.0, ref modelmatrix.xtchunked, _params);
            srcoffs = 0;
            dstoffs = 0;
            while (srcoffs < n)
            {
                curlen = Math.Min(modelmatrix.chunksize, n - srcoffs);
                for (i = 0; i <= curlen - 1; i++)
                {
                    for (j = 0; j <= nx - 1; j++)
                    {
                        modelmatrix.xtchunked[dstoffs + j, i] = xx[srcoffs + i, j];
                    }
                }
                srcoffs = srcoffs + curlen;
                dstoffs = dstoffs + nx;
            }
            return;
        }
        ap.assert(false, "ModelMatrixInit: integrity check failed");
    }


    /*************************************************************************
    Computes subset of the model matrix (subset of rows, subset of columns) and
    writes result to R.

    NOTE: If R is longer than M0xM1, it is not reallocated and additional elements
          are not modified.

      -- ALGLIB --
         Copyright 12.12.2021 by Sergey Bochkanov
    *************************************************************************/
    private static void modelmatrixcomputepartial(rbf3evaluator modelmatrix,
        int[] ridx,
        int m0,
        int[] cidx,
        int m1,
        ref double[,] r,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        int ni = 0;
        int nj = 0;
        double v = 0;
        double vv = 0;

        ap.assert(modelmatrix.storagetype == 0 || modelmatrix.storagetype == 1, "ModelMatrixComputePartial: unexpected StorageType");
        ablasf.rallocm(m0, m1, ref r, _params);
        if (modelmatrix.storagetype == 0)
        {
            for (i = 0; i <= m0 - 1; i++)
            {
                ni = ridx[i];
                for (j = 0; j <= m1 - 1; j++)
                {
                    r[i, j] = modelmatrix.f[ni, cidx[j]];
                }
            }
            return;
        }
        if (modelmatrix.storagetype == 1)
        {
            ap.assert(modelmatrix.functype == 1 || modelmatrix.functype == 2, "ModelMatrixComputePartial: unexpected FuncType");
            for (i = 0; i <= m0 - 1; i++)
            {
                ni = ridx[i];
                for (j = 0; j <= m1 - 1; j++)
                {
                    nj = cidx[j];
                    v = 0;
                    if (modelmatrix.functype == 1)
                    {
                        v = modelmatrix.funcparam * modelmatrix.funcparam;
                    }
                    if (modelmatrix.functype == 2)
                    {
                        v = 1.0E-50;
                    }
                    for (k = 0; k <= modelmatrix.nx - 1; k++)
                    {
                        vv = modelmatrix.x[ni, k] - modelmatrix.x[nj, k];
                        v = v + vv * vv;
                    }
                    if (modelmatrix.functype == 1)
                    {
                        v = -Math.Sqrt(v);
                    }
                    if (modelmatrix.functype == 2)
                    {
                        v = v * 0.5 * Math.Log(v);
                    }
                    r[i, j] = v;
                }
            }
            return;
        }
        ap.assert(false, "ModelMatrixComputePartial: integrity check failed");
    }


    /*************************************************************************
    This function computes ChunkSize basis function values and stores them in
    the evaluator buffer. This function does not modify Evaluator object, thus
    it can be used in multiple threads with the same evaluator as long as different
    buffers are used.

    INPUT PARAMETERS:
        Evaluator       -   evaluator object
        X               -   origin point
        Buf             -   preallocated buffers. Following fields are used and
                            must have at least ChunkSize elements:
                            * Buf.FuncBuf
                            * Buf.WrkBuf
                            When NeedGradInfo>=1, additionally we need
                            the following fields to be preallocated:
                            * Buf.MinDist2 - array[ChunkSize], filled by some
                              positive values; on the very first call it is 1.0E50
                              or something comparably large
                            * Buf.DeltaBuf - array[NX,ChunkSize]
                            * Buf.DF1 - array[ChunkSize]
                            When NeedGradInfo>=2, additionally we need
                            the following fields to be preallocated:
                            * Buf.DF2 - array[ChunkSize]
        ChunkSize       -   amount of basis functions to compute,
                            0<ChunkSize<=Evaluator.ChunkSize
        ChunkIdx        -   index of the chunk in Evaluator.XTChunked times NX
        Distance0       -   strictly positive value that is added to the
                            squared distance prior to passing it to the multiquadric
                            kernel function. For other kernels - set it to small
                            nonnegative value like 1.0E-50.
        NeedGradInfo    -   whether gradient-related information is needed or
                            not:
                            * if 0, only FuncBuf is set on exit
                            * if 1, MinDist2, DeltaBuf and DF1 are also set on exit
                            * if 2, additionally DF2 is set on exit
        
    OUTPUT PARAMETERS:
        Buf.FuncBuf     -   array[ChunkSize], basis function values
        Buf.MinDist2    -   array[ChunkSize], if NeedGradInfo>=1 then its
                            I-th element is updated as MinDist2[I]:=min(MinDist2[I],DISTANCE_SQUARED(X,CENTER[I]))
        Buf.DeltaBuf    -   array[NX,ChunkSize], if NeedGradInfo>=1 then
                            J-th element of K-th row is set to X[K]-CENTER[J,K]
        Buf.DF1         -   array[ChunkSize], if NeedGradInfo>=1 then
                            J-th element is derivative of the kernel function
                            with respect to its input (squared distance)
        Buf.DF2         -   array[ChunkSize], if NeedGradInfo>=2 then
                            J-th element is derivative of the kernel function
                            with respect to its input (squared distance)
        
      -- ALGLIB --
         Copyright 12.12.2021 by Sergey Bochkanov
    *************************************************************************/
    private static void computerowchunk(rbf3evaluator evaluator,
        double[] x,
        rbf3evaluatorbuffer buf,
        int chunksize,
        int chunkidx,
        double distance0,
        int needgradinfo,
        xparams _params)
    {
        int k = 0;
        double r2 = 0;
        double lnr = 0;


        //
        // Compute squared distance in Buf.FuncBuf
        //
        ablasf.rsetv(chunksize, distance0, buf.funcbuf, _params);
        for (k = 0; k <= evaluator.nx - 1; k++)
        {
            ablasf.rsetv(chunksize, x[k], buf.wrkbuf, _params);
            ablasf.raddrv(chunksize, -1.0, evaluator.xtchunked, chunkidx + k, buf.wrkbuf, _params);
            ablasf.rmuladdv(chunksize, buf.wrkbuf, buf.wrkbuf, buf.funcbuf, _params);
            if (needgradinfo >= 1)
            {
                ablasf.rcopyvr(chunksize, buf.wrkbuf, buf.deltabuf, k, _params);
            }
        }
        if (needgradinfo >= 1)
        {
            ablasf.rmergeminv(chunksize, buf.funcbuf, buf.mindist2, _params);
        }

        //
        // Apply kernel function
        //
        if (evaluator.functype == 1)
        {

            //
            // f=-sqrt(r^2+alpha^2), including f=-r as a special case
            //
            if (needgradinfo == 0)
            {

                //
                // Only target f(r2)=-sqrt(r2) is needed
                //
                ablasf.rsqrtv(chunksize, buf.funcbuf, _params);
                ablasf.rmulv(chunksize, -1.0, buf.funcbuf, _params);
            }
            if (needgradinfo == 1)
            {

                //
                // First derivative is needed:
                //
                // f(r2)   = -sqrt(r2)
                // f'(r2)  = -0.5/sqrt(r2)
                //
                // NOTE: FuncBuf[] is always positive due to small correction added,
                //       thus we have no need to handle zero value as a special case
                //
                ablasf.rsqrtv(chunksize, buf.funcbuf, _params);
                ablasf.rmulv(chunksize, -1.0, buf.funcbuf, _params);
                ablasf.rsetv(chunksize, 0.5, buf.df1, _params);
                ablasf.rmergedivv(chunksize, buf.funcbuf, buf.df1, _params);
            }
            if (needgradinfo == 2)
            {

                //
                // Second derivatives is needed:
                //
                // f(r2)   = -sqrt(r2+alpha2)
                // f'(r2)  = -0.5/sqrt(r2+alpha2)
                // f''(r2) =  0.25/((r2+alpha2)^(3/2))
                //
                // NOTE: FuncBuf[] is always positive due to small correction added,
                //       thus we have no need to handle zero value as a special case
                //
                ablasf.rcopymulv(chunksize, -2.0, buf.funcbuf, buf.wrkbuf, _params);
                ablasf.rsqrtv(chunksize, buf.funcbuf, _params);
                ablasf.rmulv(chunksize, -1.0, buf.funcbuf, _params);
                ablasf.rsetv(chunksize, 0.5, buf.df1, _params);
                ablasf.rmergedivv(chunksize, buf.funcbuf, buf.df1, _params);
                ablasf.rcopyv(chunksize, buf.df1, buf.df2, _params);
                ablasf.rmergedivv(chunksize, buf.wrkbuf, buf.df2, _params);
            }
            return;
        }
        if (evaluator.functype == 2)
        {

            //
            // f=r^2*ln(r)
            //
            // NOTE: FuncBuf[] is always positive due to small correction added,
            //       thus we have no need to handle ln(0) as a special case.
            //
            if (needgradinfo == 0)
            {

                //
                // No gradient info is required
                //
                // NOTE: FuncBuf[] is always positive due to small correction added,
                //       thus we have no need to handle zero value as a special case
                //
                for (k = 0; k <= chunksize - 1; k++)
                {
                    buf.funcbuf[k] = buf.funcbuf[k] * 0.5 * Math.Log(buf.funcbuf[k]);
                }
            }
            if (needgradinfo == 1)
            {

                //
                // First derivative is needed:
                //
                // f(r2)  = 0.5*r2*ln(r2)
                // f'(r2) = 0.5*ln(r2) + 0.5 = 0.5*(ln(r2)+1) =ln(r)+0.5
                //
                // NOTE: FuncBuf[] is always positive due to small correction added,
                //       thus we have no need to handle zero value as a special case
                //
                for (k = 0; k <= chunksize - 1; k++)
                {
                    r2 = buf.funcbuf[k];
                    lnr = 0.5 * Math.Log(r2);
                    buf.funcbuf[k] = r2 * lnr;
                    buf.df1[k] = lnr + 0.5;
                }
            }
            if (needgradinfo == 2)
            {

                //
                // Second derivative is needed:
                //
                // f(r2)  = 0.5*r2*ln(r2)
                // f'(r2) = 0.5*ln(r2) + 0.5 = 0.5*(ln(r2)+1) =ln(r)+0.5
                // f''(r2)= 0.5/r2
                //
                // NOTE: FuncBuf[] is always positive due to small correction added,
                //       thus we have no need to handle zero value as a special case
                //
                for (k = 0; k <= chunksize - 1; k++)
                {
                    r2 = buf.funcbuf[k];
                    lnr = 0.5 * Math.Log(r2);
                    buf.funcbuf[k] = r2 * lnr;
                    buf.df1[k] = lnr + 0.5;
                    buf.df2[k] = 0.5 / r2;
                }
            }
            return;
        }
        ap.assert(false, "RBFV3: unexpected FuncType in ComputeRowChunk()");
    }


    /*************************************************************************
    Checks whether basis function is conditionally positive definite or not,
    given the polynomial term type (ATerm=1 means linear, ATerm=2 means constant,
    ATerm=3 means no polynomial term).

      -- ALGLIB --
         Copyright 12.12.2021 by Sergey Bochkanov
    *************************************************************************/
    private static bool iscpdfunction(int functype,
        int aterm,
        xparams _params)
    {
        bool result = new bool();

        ap.assert((aterm == 1 || aterm == 2) || aterm == 3, "RBFV3: integrity check 3563 failed");
        result = false;
        if (functype == 1)
        {
            result = aterm == 2 || aterm == 1;
            return result;
        }
        if (functype == 2)
        {
            result = aterm == 1;
            return result;
        }
        ap.assert(false, "IsCPDFunction: unexpected FuncType");
        return result;
    }


}
