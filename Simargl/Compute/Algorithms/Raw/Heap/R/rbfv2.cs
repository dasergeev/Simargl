using System;

#pragma warning disable CS3008
#pragma warning disable CS8618
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

public class rbfv2
{
    /*************************************************************************
    Buffer object which is used to perform nearest neighbor  requests  in  the
    multithreaded mode (multiple threads working with same KD-tree object).

    This object should be created with KDTreeCreateBuffer().
    *************************************************************************/
    public class rbfv2calcbuffer : apobject
    {
        public double[] x;
        public double[] curboxmin;
        public double[] curboxmax;
        public double curdist2;
        public double[] x123;
        public double[] y123;
        public rbfv2calcbuffer()
        {
            init();
        }
        public override void init()
        {
            x = new double[0];
            curboxmin = new double[0];
            curboxmax = new double[0];
            x123 = new double[0];
            y123 = new double[0];
        }
        public override apobject make_copy()
        {
            rbfv2calcbuffer _result = new rbfv2calcbuffer();
            _result.x = (double[])x.Clone();
            _result.curboxmin = (double[])curboxmin.Clone();
            _result.curboxmax = (double[])curboxmax.Clone();
            _result.curdist2 = curdist2;
            _result.x123 = (double[])x123.Clone();
            _result.y123 = (double[])y123.Clone();
            return _result;
        }
    };


    /*************************************************************************
    RBF model.

    Never try to work with fields of this object directly - always use  ALGLIB
    functions to use this object.
    *************************************************************************/
    public class rbfv2model : apobject
    {
        public int ny;
        public int nx;
        public int bf;
        public int nh;
        public double[] ri;
        public double[] s;
        public int[] kdroots;
        public int[] kdnodes;
        public double[] kdsplits;
        public double[] kdboxmin;
        public double[] kdboxmax;
        public double[] cw;
        public double[,] v;
        public double lambdareg;
        public int maxits;
        public double supportr;
        public int basisfunction;
        public rbfv2calcbuffer calcbuf;
        public rbfv2model()
        {
            init();
        }
        public override void init()
        {
            ri = new double[0];
            s = new double[0];
            kdroots = new int[0];
            kdnodes = new int[0];
            kdsplits = new double[0];
            kdboxmin = new double[0];
            kdboxmax = new double[0];
            cw = new double[0];
            v = new double[0, 0];
            calcbuf = new rbfv2calcbuffer();
        }
        public override apobject make_copy()
        {
            rbfv2model _result = new rbfv2model();
            _result.ny = ny;
            _result.nx = nx;
            _result.bf = bf;
            _result.nh = nh;
            _result.ri = (double[])ri.Clone();
            _result.s = (double[])s.Clone();
            _result.kdroots = (int[])kdroots.Clone();
            _result.kdnodes = (int[])kdnodes.Clone();
            _result.kdsplits = (double[])kdsplits.Clone();
            _result.kdboxmin = (double[])kdboxmin.Clone();
            _result.kdboxmax = (double[])kdboxmax.Clone();
            _result.cw = (double[])cw.Clone();
            _result.v = (double[,])v.Clone();
            _result.lambdareg = lambdareg;
            _result.maxits = maxits;
            _result.supportr = supportr;
            _result.basisfunction = basisfunction;
            _result.calcbuf = (rbfv2calcbuffer)calcbuf.make_copy();
            return _result;
        }
    };


    /*************************************************************************
    Internal buffer for GridCalc3
    *************************************************************************/
    public class rbfv2gridcalcbuffer : apobject
    {
        public rbfv2calcbuffer calcbuf;
        public double[] cx;
        public double[] rx;
        public double[] ry;
        public double[] tx;
        public double[] ty;
        public bool[] rf;
        public rbfv2gridcalcbuffer()
        {
            init();
        }
        public override void init()
        {
            calcbuf = new rbfv2calcbuffer();
            cx = new double[0];
            rx = new double[0];
            ry = new double[0];
            tx = new double[0];
            ty = new double[0];
            rf = new bool[0];
        }
        public override apobject make_copy()
        {
            rbfv2gridcalcbuffer _result = new rbfv2gridcalcbuffer();
            _result.calcbuf = (rbfv2calcbuffer)calcbuf.make_copy();
            _result.cx = (double[])cx.Clone();
            _result.rx = (double[])rx.Clone();
            _result.ry = (double[])ry.Clone();
            _result.tx = (double[])tx.Clone();
            _result.ty = (double[])ty.Clone();
            _result.rf = (bool[])rf.Clone();
            return _result;
        }
    };


    /*************************************************************************
    RBF solution report:
    * TerminationType   -   termination type, positive values - success,
                            non-positive - failure.
    *************************************************************************/
    public class rbfv2report : apobject
    {
        public int terminationtype;
        public double maxerror;
        public double rmserror;
        public rbfv2report()
        {
            init();
        }
        public override void init()
        {
        }
        public override apobject make_copy()
        {
            rbfv2report _result = new rbfv2report();
            _result.terminationtype = terminationtype;
            _result.maxerror = maxerror;
            _result.rmserror = rmserror;
            return _result;
        }
    };




    public const double defaultlambdareg = 1.0E-6;
    public const double defaultsupportr = 0.10;
    public const int defaultmaxits = 400;
    public const int defaultbf = 1;
    public const int maxnodesize = 6;
    public const double complexitymultiplier = 100.0;


    /*************************************************************************
    This function creates RBF  model  for  a  scalar (NY=1)  or  vector (NY>1)
    function in a NX-dimensional space (NX=2 or NX=3).

    INPUT PARAMETERS:
        NX      -   dimension of the space, NX=2 or NX=3
        NY      -   function dimension, NY>=1

    OUTPUT PARAMETERS:
        S       -   RBF model (initially equals to zero)

      -- ALGLIB --
         Copyright 13.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv2create(int nx,
        int ny,
        rbfv2model s,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        ap.assert(nx >= 1, "RBFCreate: NX<1");
        ap.assert(ny >= 1, "RBFCreate: NY<1");

        //
        // Serializable parameters
        //
        s.nx = nx;
        s.ny = ny;
        s.bf = 0;
        s.nh = 0;
        s.v = new double[ny, nx + 1];
        for (i = 0; i <= ny - 1; i++)
        {
            for (j = 0; j <= nx; j++)
            {
                s.v[i, j] = 0;
            }
        }

        //
        // Non-serializable parameters
        //
        s.lambdareg = defaultlambdareg;
        s.maxits = defaultmaxits;
        s.supportr = defaultsupportr;
        s.basisfunction = defaultbf;
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
         Copyright 02.04.2016 by Sergey Bochkanov
    *************************************************************************/
    public static void rbfv2createcalcbuffer(rbfv2model s,
        rbfv2calcbuffer buf,
        xparams _params)
    {
        allocatecalcbuffer(s, buf, _params);
    }


    /*************************************************************************
    This   function  builds hierarchical RBF model.

    INPUT PARAMETERS:
        X       -   array[N,S.NX], X-values
        Y       -   array[N,S.NY], Y-values
        ScaleVec-   array[S.NX], vector of per-dimension scales
        N       -   points count
        ATerm   -   linear term type, 1 for linear, 2 for constant, 3 for zero.
        NH      -   hierarchy height
        RBase   -   base RBF radius
        BF      -   basis function type: 0 for Gaussian, 1 for compact
        LambdaNS-   non-smoothness penalty coefficient. Exactly zero value means
                    that no penalty is applied, and even system matrix does not
                    contain penalty-related rows. Value of 1 means
        S       -   RBF model, initialized by RBFCreate() call.
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
                      * -5 - non-distinct basis function centers were detected,
                             interpolation aborted
                      * -4 - nonconvergence of the internal SVD solver
                      *  1 - successful termination
                      *  8 terminated by user via rbfrequesttermination()
                    Fields are used for debugging purposes:
                    * Rep.IterationsCount - iterations count of the LSQR solver
                    * Rep.NMV - number of matrix-vector products
                    * Rep.ARows - rows count for the system matrix
                    * Rep.ACols - columns count for the system matrix
                    * Rep.ANNZ - number of significantly non-zero elements
                      (elements above some algorithm-determined threshold)

    NOTE:  failure  to  build  model will leave current state of the structure
    unchanged.

      -- ALGLIB --
         Copyright 20.06.2016 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv2buildhierarchical(double[,] x,
        double[,] y,
        int n,
        double[] scalevec,
        int aterm,
        int nh,
        double rbase,
        double lambdans,
        rbfv2model s,
        ref int progress10000,
        ref bool terminationrequest,
        rbfv2report rep,
        xparams _params)
    {
        int nx = 0;
        int ny = 0;
        int bf = 0;
        double[,] rhs = new double[0, 0];
        double[,] residualy = new double[0, 0];
        double[,] v = new double[0, 0];
        int rowsperpoint = 0;
        int[] hidx = new int[0];
        double[] xr = new double[0];
        double[] ri = new double[0];
        int[] kdroots = new int[0];
        int[] kdnodes = new int[0];
        double[] kdsplits = new double[0];
        double[] kdboxmin = new double[0];
        double[] kdboxmax = new double[0];
        double[] cw = new double[0];
        int[] cwrange = new int[0];
        double[,] curxy = new double[0, 0];
        int curn = 0;
        int nbasis = 0;
        nearestneighbor.kdtree curtree = new nearestneighbor.kdtree();
        nearestneighbor.kdtree globaltree = new nearestneighbor.kdtree();
        double[] x0 = new double[0];
        double[] x1 = new double[0];
        int[] tags = new int[0];
        double[] dist = new double[0];
        int[] nncnt = new int[0];
        int[] rowsizes = new int[0];
        double[] diagata = new double[0];
        double[] prec = new double[0];
        double[] tmpx = new double[0];
        int i = 0;
        int j = 0;
        int k = 0;
        int k2 = 0;
        int levelidx = 0;
        int offsi = 0;
        int offsj = 0;
        double val = 0;
        double criticalr = 0;
        int cnt = 0;
        double avgdiagata = 0;
        double[] avgrowsize = new double[0];
        double sumrowsize = 0;
        double rprogress = 0;
        int maxits = 0;
        linlsqr.linlsqrstate linstate = new linlsqr.linlsqrstate();
        linlsqr.linlsqrreport lsqrrep = new linlsqr.linlsqrreport();
        sparse.sparsematrix sparseacrs = new sparse.sparsematrix();
        double[] densew1 = new double[0];
        double[] denseb1 = new double[0];
        rbfv2calcbuffer calcbuf = new rbfv2calcbuffer();
        double[] vr2 = new double[0];
        int[] voffs = new int[0];
        int[] rowindexes = new int[0];
        double[] rowvals = new double[0];
        double penalty = 0;

        ap.assert(s.nx > 0, "RBFV2BuildHierarchical: incorrect NX");
        ap.assert(s.ny > 0, "RBFV2BuildHierarchical: incorrect NY");
        ap.assert((double)(lambdans) >= (double)(0), "RBFV2BuildHierarchical: incorrect LambdaNS");
        for (j = 0; j <= s.nx - 1; j++)
        {
            ap.assert((double)(scalevec[j]) > (double)(0), "RBFV2BuildHierarchical: incorrect ScaleVec");
        }
        nx = s.nx;
        ny = s.ny;
        bf = s.basisfunction;
        ap.assert(bf == 0 || bf == 1, "RBFV2BuildHierarchical: incorrect BF");

        //
        // Clean up communication and report fields
        //
        progress10000 = 0;
        rep.maxerror = 0;
        rep.rmserror = 0;

        //
        // Quick exit when we have no points
        //
        if (n == 0)
        {
            zerofill(s, nx, ny, bf, _params);
            rep.terminationtype = 1;
            progress10000 = 10000;
            return;
        }

        //
        // First model in a sequence - linear model.
        // Residuals from linear regression are stored in the ResidualY variable
        // (used later to build RBF models).
        //
        residualy = new double[n, ny];
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= ny - 1; j++)
            {
                residualy[i, j] = y[i, j];
            }
        }
        if (!rbfv2buildlinearmodel(x, ref residualy, n, nx, ny, aterm, ref v, _params))
        {
            zerofill(s, nx, ny, bf, _params);
            rep.terminationtype = -5;
            progress10000 = 10000;
            return;
        }

        //
        // Handle special case: multilayer model with NLayers=0.
        // Quick exit.
        //
        if (nh == 0)
        {
            rep.terminationtype = 1;
            zerofill(s, nx, ny, bf, _params);
            for (i = 0; i <= ny - 1; i++)
            {
                for (j = 0; j <= nx; j++)
                {
                    s.v[i, j] = v[i, j];
                }
            }
            rep.maxerror = 0;
            rep.rmserror = 0;
            for (i = 0; i <= n - 1; i++)
            {
                for (j = 0; j <= ny - 1; j++)
                {
                    rep.maxerror = Math.Max(rep.maxerror, Math.Abs(residualy[i, j]));
                    rep.rmserror = rep.rmserror + math.sqr(residualy[i, j]);
                }
            }
            rep.rmserror = Math.Sqrt(rep.rmserror / (n * ny));
            progress10000 = 10000;
            return;
        }

        //
        // Penalty coefficient is set to LambdaNS*RBase^2.
        //
        // We use such normalization because VALUES of radial basis
        // functions have roughly unit magnitude, but their DERIVATIVES
        // are (roughly) inversely proportional to the radius. Thus,
        // without additional scaling, regularization coefficient
        // looses invariancy w.r.t. scaling of variables.
        //
        if ((double)(lambdans) == (double)(0))
        {
            rowsperpoint = 1;
        }
        else
        {

            //
            // NOTE: simplified penalty function is used, which does not provide rotation invariance
            //
            rowsperpoint = 1 + nx;
        }
        penalty = lambdans * math.sqr(rbase);

        //
        // Prepare temporary structures
        //
        rhs = new double[n * rowsperpoint, ny];
        curxy = new double[n, nx + ny];
        x0 = new double[nx];
        x1 = new double[nx];
        tags = new int[n];
        dist = new double[n];
        vr2 = new double[n];
        voffs = new int[n];
        nncnt = new int[n];
        rowsizes = new int[n * rowsperpoint];
        denseb1 = new double[n * rowsperpoint];
        for (i = 0; i <= n * rowsperpoint - 1; i++)
        {
            for (j = 0; j <= ny - 1; j++)
            {
                rhs[i, j] = 0;
            }
        }
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= nx - 1; j++)
            {
                curxy[i, j] = x[i, j] / scalevec[j];
            }
            for (j = 0; j <= ny - 1; j++)
            {
                rhs[i * rowsperpoint, j] = residualy[i, j];
            }
            tags[i] = i;
        }
        nearestneighbor.kdtreebuildtagged(curxy, tags, n, nx, 0, 2, globaltree, _params);

        //
        // Generate sequence of layer radii.
        // Prepare assignment of different levels to points.
        //
        ap.assert(n > 0, "RBFV2BuildHierarchical: integrity check failed");
        ri = new double[nh];
        for (levelidx = 0; levelidx <= nh - 1; levelidx++)
        {
            ri[levelidx] = rbase * Math.Pow(2, -levelidx);
        }
        hidx = new int[n];
        xr = new double[n];
        for (i = 0; i <= n - 1; i++)
        {
            hidx[i] = nh;
            xr[i] = math.maxrealnumber;
            ap.assert((double)(xr[i]) > (double)(ri[0]), "RBFV2BuildHierarchical: integrity check failed");
        }
        for (levelidx = 0; levelidx <= nh - 1; levelidx++)
        {

            //
            // Scan dataset points, for each such point that distance to nearest
            // "support" point is larger than SupportR*Ri[LevelIdx] we:
            // * set distance of current point to 0 (it is support now) and update HIdx
            // * perform R-NN request with radius SupportR*Ri[LevelIdx]
            // * for each point in request update its distance
            //
            criticalr = s.supportr * ri[levelidx];
            for (i = 0; i <= n - 1; i++)
            {
                if ((double)(xr[i]) > (double)(criticalr))
                {

                    //
                    // Mark point as support
                    //
                    ap.assert(hidx[i] == nh, "RBFV2BuildHierarchical: integrity check failed");
                    hidx[i] = levelidx;
                    xr[i] = 0;

                    //
                    // Update neighbors
                    //
                    for (j = 0; j <= nx - 1; j++)
                    {
                        x0[j] = x[i, j] / scalevec[j];
                    }
                    k = nearestneighbor.kdtreequeryrnn(globaltree, x0, criticalr, true, _params);
                    nearestneighbor.kdtreequeryresultstags(globaltree, ref tags, _params);
                    nearestneighbor.kdtreequeryresultsdistances(globaltree, ref dist, _params);
                    for (j = 0; j <= k - 1; j++)
                    {
                        xr[tags[j]] = Math.Min(xr[tags[j]], dist[j]);
                    }
                }
            }
        }

        //
        // Build multitree (with zero weights) according to hierarchy.
        //
        // NOTE: this code assumes that during every iteration kdNodes,
        //       kdSplits and CW have size which EXACTLY fits their
        //       contents, and that these variables are resized at each
        //       iteration when we add new hierarchical model.
        //
        kdroots = new int[nh + 1];
        kdnodes = new int[0];
        kdsplits = new double[0];
        kdboxmin = new double[nx];
        kdboxmax = new double[nx];
        cw = new double[0];
        cwrange = new int[nh + 1];
        nearestneighbor.kdtreeexplorebox(globaltree, ref kdboxmin, ref kdboxmax, _params);
        cwrange[0] = 0;
        for (levelidx = 0; levelidx <= nh - 1; levelidx++)
        {

            //
            // Prepare radius and root offset
            //
            kdroots[levelidx] = ap.len(kdnodes);

            //
            // Generate LevelIdx-th tree and append to multi-tree
            //
            curn = 0;
            for (i = 0; i <= n - 1; i++)
            {
                if (hidx[i] <= levelidx)
                {
                    for (j = 0; j <= nx - 1; j++)
                    {
                        curxy[curn, j] = x[i, j] / scalevec[j];
                    }
                    for (j = 0; j <= ny - 1; j++)
                    {
                        curxy[curn, nx + j] = 0;
                    }
                    apserv.inc(ref curn, _params);
                }
            }
            ap.assert(curn > 0, "RBFV2BuildHierarchical: integrity check failed");
            nearestneighbor.kdtreebuild(curxy, curn, nx, ny, 2, curtree, _params);
            convertandappendtree(curtree, curn, nx, ny, ref kdnodes, ref kdsplits, ref cw, _params);

            //
            // Fill entry of CWRange (we assume that length of CW exactly fits its actual size)
            //
            cwrange[levelidx + 1] = ap.len(cw);
        }
        kdroots[nh] = ap.len(kdnodes);

        //
        // Prepare buffer and scaled dataset
        //
        allocatecalcbuffer(s, calcbuf, _params);
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= nx - 1; j++)
            {
                curxy[i, j] = x[i, j] / scalevec[j];
            }
        }

        //
        // Calculate average row sizes for each layer; these values are used
        // for smooth progress reporting (it adds some overhead, but in most
        // cases - insignificant one).
        //
        apserv.rvectorsetlengthatleast(ref avgrowsize, nh, _params);
        sumrowsize = 0;
        for (levelidx = 0; levelidx <= nh - 1; levelidx++)
        {
            cnt = 0;
            for (i = 0; i <= n - 1; i++)
            {
                for (j = 0; j <= nx - 1; j++)
                {
                    x0[j] = curxy[i, j];
                }
                cnt = cnt + designmatrixrowsize(kdnodes, kdsplits, cw, ri, kdroots, kdboxmin, kdboxmax, nx, ny, nh, levelidx, rbfv2nearradius(bf, _params), x0, calcbuf, _params);
            }
            avgrowsize[levelidx] = apserv.coalesce(cnt, 1, _params) / apserv.coalesce(n, 1, _params);
            sumrowsize = sumrowsize + avgrowsize[levelidx];
        }

        //
        // Build unconstrained model with LSQR solver, applied layer by layer
        //
        for (levelidx = 0; levelidx <= nh - 1; levelidx++)
        {

            //
            // Generate A - matrix of basis functions (near radius is used)
            //
            // NOTE: AvgDiagATA is average value of diagonal element of A^T*A.
            //       It is used to calculate value of Tikhonov regularization
            //       coefficient.
            //
            nbasis = (cwrange[levelidx + 1] - cwrange[levelidx]) / (nx + ny);
            ap.assert(cwrange[levelidx + 1] - cwrange[levelidx] == nbasis * (nx + ny));
            for (i = 0; i <= n - 1; i++)
            {
                for (j = 0; j <= nx - 1; j++)
                {
                    x0[j] = curxy[i, j];
                }
                cnt = designmatrixrowsize(kdnodes, kdsplits, cw, ri, kdroots, kdboxmin, kdboxmax, nx, ny, nh, levelidx, rbfv2nearradius(bf, _params), x0, calcbuf, _params);
                nncnt[i] = cnt;
                for (j = 0; j <= rowsperpoint - 1; j++)
                {
                    rowsizes[i * rowsperpoint + j] = cnt;
                }
            }
            apserv.ivectorsetlengthatleast(ref rowindexes, nbasis, _params);
            apserv.rvectorsetlengthatleast(ref rowvals, nbasis * rowsperpoint, _params);
            apserv.rvectorsetlengthatleast(ref diagata, nbasis, _params);
            sparse.sparsecreatecrsbuf(n * rowsperpoint, nbasis, rowsizes, sparseacrs, _params);
            avgdiagata = 0.0;
            for (j = 0; j <= nbasis - 1; j++)
            {
                diagata[j] = 0;
            }
            for (i = 0; i <= n - 1; i++)
            {

                //
                // Fill design matrix row, diagonal of A^T*A
                //
                for (j = 0; j <= nx - 1; j++)
                {
                    x0[j] = curxy[i, j];
                }
                designmatrixgeneraterow(kdnodes, kdsplits, cw, ri, kdroots, kdboxmin, kdboxmax, cwrange, nx, ny, nh, levelidx, bf, rbfv2nearradius(bf, _params), rowsperpoint, penalty, x0, calcbuf, vr2, voffs, rowindexes, rowvals, ref cnt, _params);
                ap.assert(cnt == nncnt[i], "RBFV2BuildHierarchical: integrity check failed");
                for (k = 0; k <= rowsperpoint - 1; k++)
                {
                    for (j = 0; j <= cnt - 1; j++)
                    {
                        val = rowvals[j * rowsperpoint + k];
                        sparse.sparseset(sparseacrs, i * rowsperpoint + k, rowindexes[j], val, _params);
                        avgdiagata = avgdiagata + math.sqr(val);
                        diagata[rowindexes[j]] = diagata[rowindexes[j]] + math.sqr(val);
                    }
                }

                //
                // Handle possible termination requests
                //
                if (terminationrequest)
                {

                    //
                    // Request for termination was submitted, terminate immediately
                    //
                    zerofill(s, nx, ny, bf, _params);
                    rep.terminationtype = 8;
                    progress10000 = 10000;
                    return;
                }
            }
            avgdiagata = avgdiagata / nbasis;
            apserv.rvectorsetlengthatleast(ref prec, nbasis, _params);
            for (j = 0; j <= nbasis - 1; j++)
            {
                prec[j] = 1 / apserv.coalesce(Math.Sqrt(diagata[j]), 1, _params);
            }

            //
            // solve
            //
            maxits = apserv.coalescei(s.maxits, defaultmaxits, _params);
            apserv.rvectorsetlengthatleast(ref tmpx, nbasis, _params);
            linlsqr.linlsqrcreate(n * rowsperpoint, nbasis, linstate, _params);
            linlsqr.linlsqrsetcond(linstate, 0.0, 0.0, maxits, _params);
            linlsqr.linlsqrsetlambdai(linstate, Math.Sqrt(s.lambdareg * avgdiagata), _params);
            for (j = 0; j <= ny - 1; j++)
            {
                for (i = 0; i <= n * rowsperpoint - 1; i++)
                {
                    denseb1[i] = rhs[i, j];
                }
                linlsqr.linlsqrsetb(linstate, denseb1, _params);
                linlsqr.linlsqrrestart(linstate, _params);
                linlsqr.linlsqrsetxrep(linstate, true, _params);
                while (linlsqr.linlsqriteration(linstate, _params))
                {
                    if (terminationrequest)
                    {

                        //
                        // Request for termination was submitted, terminate immediately
                        //
                        zerofill(s, nx, ny, bf, _params);
                        rep.terminationtype = 8;
                        progress10000 = 10000;
                        return;
                    }
                    if (linstate.needmv)
                    {
                        for (i = 0; i <= nbasis - 1; i++)
                        {
                            tmpx[i] = prec[i] * linstate.x[i];
                        }
                        sparse.sparsemv(sparseacrs, tmpx, ref linstate.mv, _params);
                        continue;
                    }
                    if (linstate.needmtv)
                    {
                        sparse.sparsemtv(sparseacrs, linstate.x, ref linstate.mtv, _params);
                        for (i = 0; i <= nbasis - 1; i++)
                        {
                            linstate.mtv[i] = prec[i] * linstate.mtv[i];
                        }
                        continue;
                    }
                    if (linstate.xupdated)
                    {
                        rprogress = 0;
                        for (i = 0; i <= levelidx - 1; i++)
                        {
                            rprogress = rprogress + maxits * ny * avgrowsize[i];
                        }
                        rprogress = rprogress + (linlsqr.linlsqrpeekiterationscount(linstate, _params) + j * maxits) * avgrowsize[levelidx];
                        rprogress = rprogress / (sumrowsize * maxits * ny);
                        rprogress = 10000 * rprogress;
                        rprogress = Math.Max(rprogress, 0);
                        rprogress = Math.Min(rprogress, 10000);
                        ap.assert(progress10000 <= (int)Math.Round(rprogress) + 1, "HRBF: integrity check failed (progress indicator) even after +1 safeguard correction");
                        progress10000 = (int)Math.Round(rprogress);
                        continue;
                    }
                    ap.assert(false, "HRBF: unexpected request from LSQR solver");
                }
                linlsqr.linlsqrresults(linstate, ref densew1, lsqrrep, _params);
                ap.assert(lsqrrep.terminationtype > 0, "RBFV2BuildHierarchical: integrity check failed");
                for (i = 0; i <= nbasis - 1; i++)
                {
                    densew1[i] = prec[i] * densew1[i];
                }
                for (i = 0; i <= nbasis - 1; i++)
                {
                    offsi = cwrange[levelidx] + (nx + ny) * i;
                    cw[offsi + nx + j] = densew1[i];
                }
            }

            //
            // Update residuals (far radius is used)
            //
            for (i = 0; i <= n - 1; i++)
            {
                for (j = 0; j <= nx - 1; j++)
                {
                    x0[j] = curxy[i, j];
                }
                designmatrixgeneraterow(kdnodes, kdsplits, cw, ri, kdroots, kdboxmin, kdboxmax, cwrange, nx, ny, nh, levelidx, bf, rbfv2farradius(bf, _params), rowsperpoint, penalty, x0, calcbuf, vr2, voffs, rowindexes, rowvals, ref cnt, _params);
                for (j = 0; j <= cnt - 1; j++)
                {
                    offsj = cwrange[levelidx] + (nx + ny) * rowindexes[j] + nx;
                    for (k = 0; k <= rowsperpoint - 1; k++)
                    {
                        val = rowvals[j * rowsperpoint + k];
                        for (k2 = 0; k2 <= ny - 1; k2++)
                        {
                            rhs[i * rowsperpoint + k, k2] = rhs[i * rowsperpoint + k, k2] - val * cw[offsj + k2];
                        }
                    }
                }
            }
        }

        //
        // Model is built.
        //
        // Copy local variables by swapping, global ones (ScaleVec) are copied
        // explicitly.
        //
        s.bf = bf;
        s.nh = nh;
        ap.swap(ref s.ri, ref ri);
        ap.swap(ref s.kdroots, ref kdroots);
        ap.swap(ref s.kdnodes, ref kdnodes);
        ap.swap(ref s.kdsplits, ref kdsplits);
        ap.swap(ref s.kdboxmin, ref kdboxmin);
        ap.swap(ref s.kdboxmax, ref kdboxmax);
        ap.swap(ref s.cw, ref cw);
        ap.swap(ref s.v, ref v);
        s.s = new double[nx];
        for (i = 0; i <= nx - 1; i++)
        {
            s.s[i] = scalevec[i];
        }
        rep.terminationtype = 1;

        //
        // Calculate maximum and RMS errors
        //
        rep.maxerror = 0;
        rep.rmserror = 0;
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= ny - 1; j++)
            {
                rep.maxerror = Math.Max(rep.maxerror, Math.Abs(rhs[i * rowsperpoint, j]));
                rep.rmserror = rep.rmserror + math.sqr(rhs[i * rowsperpoint, j]);
            }
        }
        rep.rmserror = Math.Sqrt(rep.rmserror / (n * ny));

        //
        // Update progress reports
        //
        progress10000 = 10000;
    }


    /*************************************************************************
    Serializer: allocation

      -- ALGLIB --
         Copyright 02.02.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv2alloc(serializer s,
        rbfv2model model,
        xparams _params)
    {

        //
        // Data
        //
        s.alloc_entry();
        s.alloc_entry();
        s.alloc_entry();
        s.alloc_entry();
        apserv.allocrealarray(s, model.ri, -1, _params);
        apserv.allocrealarray(s, model.s, -1, _params);
        apserv.allocintegerarray(s, model.kdroots, -1, _params);
        apserv.allocintegerarray(s, model.kdnodes, -1, _params);
        apserv.allocrealarray(s, model.kdsplits, -1, _params);
        apserv.allocrealarray(s, model.kdboxmin, -1, _params);
        apserv.allocrealarray(s, model.kdboxmax, -1, _params);
        apserv.allocrealarray(s, model.cw, -1, _params);
        apserv.allocrealmatrix(s, model.v, -1, -1, _params);
    }


    /*************************************************************************
    Serializer: serialization

      -- ALGLIB --
         Copyright 02.02.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv2serialize(serializer s,
        rbfv2model model,
        xparams _params)
    {

        //
        // Data
        //
        s.serialize_int(model.nx);
        s.serialize_int(model.ny);
        s.serialize_int(model.nh);
        s.serialize_int(model.bf);
        apserv.serializerealarray(s, model.ri, -1, _params);
        apserv.serializerealarray(s, model.s, -1, _params);
        apserv.serializeintegerarray(s, model.kdroots, -1, _params);
        apserv.serializeintegerarray(s, model.kdnodes, -1, _params);
        apserv.serializerealarray(s, model.kdsplits, -1, _params);
        apserv.serializerealarray(s, model.kdboxmin, -1, _params);
        apserv.serializerealarray(s, model.kdboxmax, -1, _params);
        apserv.serializerealarray(s, model.cw, -1, _params);
        apserv.serializerealmatrix(s, model.v, -1, -1, _params);
    }


    /*************************************************************************
    Serializer: unserialization

      -- ALGLIB --
         Copyright 02.02.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv2unserialize(serializer s,
        rbfv2model model,
        xparams _params)
    {
        int nx = 0;
        int ny = 0;


        //
        // Unserialize primary model parameters, initialize model.
        //
        // It is necessary to call RBFCreate() because some internal fields
        // which are NOT unserialized will need initialization.
        //
        nx = s.unserialize_int();
        ny = s.unserialize_int();
        rbfv2create(nx, ny, model, _params);
        model.nh = s.unserialize_int();
        model.bf = s.unserialize_int();
        apserv.unserializerealarray(s, ref model.ri, _params);
        apserv.unserializerealarray(s, ref model.s, _params);
        apserv.unserializeintegerarray(s, ref model.kdroots, _params);
        apserv.unserializeintegerarray(s, ref model.kdnodes, _params);
        apserv.unserializerealarray(s, ref model.kdsplits, _params);
        apserv.unserializerealarray(s, ref model.kdboxmin, _params);
        apserv.unserializerealarray(s, ref model.kdboxmax, _params);
        apserv.unserializerealarray(s, ref model.cw, _params);
        apserv.unserializerealmatrix(s, ref model.v, _params);
    }


    /*************************************************************************
    Returns far radius for basis function type
    *************************************************************************/
    public static double rbfv2farradius(int bf,
        xparams _params)
    {
        double result = 0;

        result = 1;
        if (bf == 0)
        {
            result = 5.0;
        }
        if (bf == 1)
        {
            result = 3;
        }
        return result;
    }


    /*************************************************************************
    Returns near radius for basis function type
    *************************************************************************/
    public static double rbfv2nearradius(int bf,
        xparams _params)
    {
        double result = 0;

        result = 1;
        if (bf == 0)
        {
            result = 3.0;
        }
        if (bf == 1)
        {
            result = 3;
        }
        return result;
    }


    /*************************************************************************
    Returns basis function value.
    Assumes that D2>=0
    *************************************************************************/
    public static double rbfv2basisfunc(int bf,
        double d2,
        xparams _params)
    {
        double result = 0;
        double v = 0;

        result = 0;
        if (bf == 0)
        {
            result = Math.Exp(-d2);
            return result;
        }
        if (bf == 1)
        {

            //
            // if D2<3:
            //     Exp(1)*Exp(-D2)*Exp(-1/(1-D2/9))
            // else:
            //     0
            //
            v = 1 - d2 / 9;
            if ((double)(v) <= (double)(0))
            {
                result = 0;
                return result;
            }
            result = 2.718281828459045 * Math.Exp(-d2) * Math.Exp(-(1 / v));
            return result;
        }
        ap.assert(false, "RBFV2BasisFunc: unknown BF type");
        return result;
    }


    /*************************************************************************
    Returns basis function value, first and second derivatives
    Assumes that D2>=0
    *************************************************************************/
    public static void rbfv2basisfuncdiff2(int bf,
        double d2,
        ref double f,
        ref double df,
        ref double d2f,
        xparams _params)
    {
        double v = 0;

        f = 0;
        df = 0;
        d2f = 0;

        if (bf == 0)
        {
            f = Math.Exp(-d2);
            df = -f;
            d2f = f;
            return;
        }
        if (bf == 1)
        {

            //
            // if D2<3:
            //       F = Exp(1)*Exp(-D2)*Exp(-1/(1-D2/9))
            //      dF =  -F * [pow(D2/9-1,-2)/9 + 1]
            //     d2F = -dF * [pow(D2/9-1,-2)/9 + 1] - F*(2/81)*pow(D2/9-1,-3)
            // else:
            //     0
            //
            v = 1 - d2 / 9;
            if ((double)(v) <= (double)(0))
            {
                f = 0;
                df = 0;
                d2f = 0;
                return;
            }
            f = Math.Exp(1) * Math.Exp(-d2) * Math.Exp(-(1 / v));
            df = -(f * (1 / (9 * v * v) + 1));
            d2f = -(df * (1 / (9 * v * v) + 1)) - f * ((double)2 / (double)81) / (v * v * v);
            return;
        }
        ap.assert(false, "RBFV2BasisFuncDiff2: unknown BF type");
    }


    /*************************************************************************
    This function calculates values of the RBF model in the given point.

    This function should be used when we have NY=1 (scalar function) and  NX=1
    (1-dimensional space).

    This function returns 0.0 when:
    * model is not initialized
    * NX<>1
     *NY<>1

    INPUT PARAMETERS:
        S       -   RBF model
        X0      -   X-coordinate, finite number

    RESULT:
        value of the model or 0.0 (as defined above)

      -- ALGLIB --
         Copyright 13.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static double rbfv2calc1(rbfv2model s,
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
        if (s.nh == 0)
        {
            return result;
        }
        allocatecalcbuffer(s, s.calcbuf, _params);
        s.calcbuf.x123[0] = x0;
        rbfv2tscalcbuf(s, s.calcbuf, s.calcbuf.x123, ref s.calcbuf.y123, _params);
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
         Copyright 13.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static double rbfv2calc2(rbfv2model s,
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
        if (s.nh == 0)
        {
            return result;
        }
        allocatecalcbuffer(s, s.calcbuf, _params);
        s.calcbuf.x123[0] = x0;
        s.calcbuf.x123[1] = x1;
        rbfv2tscalcbuf(s, s.calcbuf, s.calcbuf.x123, ref s.calcbuf.y123, _params);
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
         Copyright 13.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static double rbfv2calc3(rbfv2model s,
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
        if (s.nh == 0)
        {
            return result;
        }
        allocatecalcbuffer(s, s.calcbuf, _params);
        s.calcbuf.x123[0] = x0;
        s.calcbuf.x123[1] = x1;
        s.calcbuf.x123[2] = x2;
        rbfv2tscalcbuf(s, s.calcbuf, s.calcbuf.x123, ref s.calcbuf.y123, _params);
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
    public static void rbfv2calcbuf(rbfv2model s,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        rbfv2tscalcbuf(s, s.calcbuf, x, ref y, _params);
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
         Copyright 13.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv2tscalcbuf(rbfv2model s,
        rbfv2calcbuffer buf,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int levelidx = 0;
        double rcur = 0;
        double rquery2 = 0;
        double invrc2 = 0;
        int nx = 0;
        int ny = 0;

        ap.assert(ap.len(x) >= s.nx, "RBFCalcBuf: Length(X)<NX");
        ap.assert(apserv.isfinitevector(x, s.nx, _params), "RBFCalcBuf: X contains infinite or NaN values");
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
        if (s.nh == 0)
        {
            return;
        }

        //
        // Handle nonlinear term
        //
        allocatecalcbuffer(s, buf, _params);
        for (j = 0; j <= nx - 1; j++)
        {
            buf.x[j] = x[j] / s.s[j];
        }
        for (levelidx = 0; levelidx <= s.nh - 1; levelidx++)
        {

            //
            // Prepare fields of Buf required by PartialCalcRec()
            //
            buf.curdist2 = 0;
            for (j = 0; j <= nx - 1; j++)
            {
                buf.curboxmin[j] = s.kdboxmin[j];
                buf.curboxmax[j] = s.kdboxmax[j];
                if ((double)(buf.x[j]) < (double)(buf.curboxmin[j]))
                {
                    buf.curdist2 = buf.curdist2 + math.sqr(buf.curboxmin[j] - buf.x[j]);
                }
                else
                {
                    if ((double)(buf.x[j]) > (double)(buf.curboxmax[j]))
                    {
                        buf.curdist2 = buf.curdist2 + math.sqr(buf.x[j] - buf.curboxmax[j]);
                    }
                }
            }

            //
            // Call PartialCalcRec()
            //
            rcur = s.ri[levelidx];
            invrc2 = 1 / (rcur * rcur);
            rquery2 = math.sqr(rcur * rbfv2farradius(s.bf, _params));
            partialcalcrec(s, buf, s.kdroots[levelidx], invrc2, rquery2, buf.x, y, y, y, 0, _params);
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
    public static void rbfv2tsdiffbuf(rbfv2model s,
        rbfv2calcbuffer buf,
        double[] x,
        ref double[] y,
        ref double[] dy,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int levelidx = 0;
        double rcur = 0;
        double rquery2 = 0;
        double invrc2 = 0;
        int nx = 0;
        int ny = 0;

        ap.assert(ap.len(x) >= s.nx, "RBFDiffBuf: Length(X)<NX");
        ap.assert(apserv.isfinitevector(x, s.nx, _params), "RBFDiffBuf: X contains infinite or NaN values");
        nx = s.nx;
        ny = s.ny;
        if (ap.len(y) < s.ny)
        {
            y = new double[s.ny];
        }
        if (ap.len(dy) < s.ny * s.nx)
        {
            dy = new double[s.ny * s.nx];
        }

        //
        // Handle linear term
        //
        for (i = 0; i <= ny - 1; i++)
        {
            y[i] = s.v[i, nx];
            for (j = 0; j <= nx - 1; j++)
            {
                y[i] = y[i] + s.v[i, j] * x[j];
                dy[i * nx + j] = s.v[i, j];
            }
        }
        if (s.nh == 0)
        {
            return;
        }

        //
        // Handle nonlinear term
        //
        allocatecalcbuffer(s, buf, _params);
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
        for (levelidx = 0; levelidx <= s.nh - 1; levelidx++)
        {

            //
            // Prepare fields of Buf required by PartialCalcRec()
            //
            buf.curdist2 = 0;
            for (j = 0; j <= nx - 1; j++)
            {
                buf.curboxmin[j] = s.kdboxmin[j];
                buf.curboxmax[j] = s.kdboxmax[j];
                if ((double)(buf.x[j]) < (double)(buf.curboxmin[j]))
                {
                    buf.curdist2 = buf.curdist2 + math.sqr(buf.curboxmin[j] - buf.x[j]);
                }
                else
                {
                    if ((double)(buf.x[j]) > (double)(buf.curboxmax[j]))
                    {
                        buf.curdist2 = buf.curdist2 + math.sqr(buf.x[j] - buf.curboxmax[j]);
                    }
                }
            }

            //
            // Call PartialCalcRec()
            //
            rcur = s.ri[levelidx];
            invrc2 = 1 / (rcur * rcur);
            rquery2 = math.sqr(rcur * rbfv2farradius(s.bf, _params));
            partialcalcrec(s, buf, s.kdroots[levelidx], invrc2, rquery2, buf.x, y, dy, dy, 1, _params);
        }
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
        D2Y     -   second derivatives, array[NY*NX*NX]

      -- ALGLIB --
         Copyright 13.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv2tshessbuf(rbfv2model s,
        rbfv2calcbuffer buf,
        double[] x,
        ref double[] y,
        ref double[] dy,
        ref double[] d2y,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        int levelidx = 0;
        double rcur = 0;
        double rquery2 = 0;
        double invrc2 = 0;
        int nx = 0;
        int ny = 0;

        ap.assert(ap.len(x) >= s.nx, "RBFDiffBuf: Length(X)<NX");
        ap.assert(apserv.isfinitevector(x, s.nx, _params), "RBFDiffBuf: X contains infinite or NaN values");
        nx = s.nx;
        ny = s.ny;
        if (ap.len(y) < s.ny)
        {
            y = new double[s.ny];
        }
        if (ap.len(dy) < s.ny * s.nx)
        {
            dy = new double[s.ny * s.nx];
        }
        if (ap.len(d2y) < ny * nx * nx)
        {
            d2y = new double[ny * nx * nx];
        }

        //
        // Handle linear term
        //
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
        if (s.nh == 0)
        {
            return;
        }

        //
        // Handle nonlinear term
        //
        allocatecalcbuffer(s, buf, _params);
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
        for (levelidx = 0; levelidx <= s.nh - 1; levelidx++)
        {

            //
            // Prepare fields of Buf required by PartialCalcRec()
            //
            buf.curdist2 = 0;
            for (j = 0; j <= nx - 1; j++)
            {
                buf.curboxmin[j] = s.kdboxmin[j];
                buf.curboxmax[j] = s.kdboxmax[j];
                if ((double)(buf.x[j]) < (double)(buf.curboxmin[j]))
                {
                    buf.curdist2 = buf.curdist2 + math.sqr(buf.curboxmin[j] - buf.x[j]);
                }
                else
                {
                    if ((double)(buf.x[j]) > (double)(buf.curboxmax[j]))
                    {
                        buf.curdist2 = buf.curdist2 + math.sqr(buf.x[j] - buf.curboxmax[j]);
                    }
                }
            }

            //
            // Call PartialCalcRec()
            //
            rcur = s.ri[levelidx];
            invrc2 = 1 / (rcur * rcur);
            rquery2 = math.sqr(rcur * rbfv2farradius(s.bf, _params));
            partialcalcrec(s, buf, s.kdroots[levelidx], invrc2, rquery2, buf.x, y, dy, d2y, 2, _params);
        }
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
    This function calculates values of the RBF model at the regular grid.

    Grid have N0*N1 points, with Point[I,J] = (X0[I], X1[J])

    This function returns 0.0 when:
    * model is not initialized
    * NX<>2
     *NY<>1

    INPUT PARAMETERS:
        S       -   RBF model
        X0      -   array of grid nodes, first coordinates, array[N0]
        N0      -   grid size (number of nodes) in the first dimension
        X1      -   array of grid nodes, second coordinates, array[N1]
        N1      -   grid size (number of nodes) in the second dimension

    OUTPUT PARAMETERS:
        Y       -   function values, array[N0,N1]. Y is out-variable and 
                    is reallocated by this function.
                    
    NOTE: as a special exception, this function supports unordered  arrays  X0
          and X1. However, future versions may be  more  efficient  for  X0/X1
          ordered by ascending.

      -- ALGLIB --
         Copyright 13.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv2gridcalc2(rbfv2model s,
        double[] x0,
        int n0,
        double[] x1,
        int n1,
        ref double[,] y,
        xparams _params)
    {
        double[] cpx0 = new double[0];
        double[] cpx1 = new double[0];
        double[] dummyx2 = new double[0];
        double[] dummyx3 = new double[0];
        bool[] dummyflag = new bool[0];
        int[] p01 = new int[0];
        int[] p11 = new int[0];
        int[] p2 = new int[0];
        double[] vy = new double[0];
        int i = 0;
        int j = 0;

        y = new double[0, 0];

        ap.assert(n0 > 0, "RBFGridCalc2: invalid value for N0 (N0<=0)!");
        ap.assert(n1 > 0, "RBFGridCalc2: invalid value for N1 (N1<=0)!");
        ap.assert(ap.len(x0) >= n0, "RBFGridCalc2: Length(X0)<N0");
        ap.assert(ap.len(x1) >= n1, "RBFGridCalc2: Length(X1)<N1");
        ap.assert(apserv.isfinitevector(x0, n0, _params), "RBFGridCalc2: X0 contains infinite or NaN values!");
        ap.assert(apserv.isfinitevector(x1, n1, _params), "RBFGridCalc2: X1 contains infinite or NaN values!");
        y = new double[n0, n1];
        for (i = 0; i <= n0 - 1; i++)
        {
            for (j = 0; j <= n1 - 1; j++)
            {
                y[i, j] = 0;
            }
        }
        if (s.ny != 1 || s.nx != 2)
        {
            return;
        }

        //
        //create and sort arrays
        //
        cpx0 = new double[n0];
        for (i = 0; i <= n0 - 1; i++)
        {
            cpx0[i] = x0[i];
        }
        tsort.tagsort(ref cpx0, n0, ref p01, ref p2, _params);
        cpx1 = new double[n1];
        for (i = 0; i <= n1 - 1; i++)
        {
            cpx1[i] = x1[i];
        }
        tsort.tagsort(ref cpx1, n1, ref p11, ref p2, _params);
        dummyx2 = new double[1];
        dummyx2[0] = 0;
        dummyx3 = new double[1];
        dummyx3[0] = 0;
        vy = new double[n0 * n1];
        rbfv2gridcalcvx(s, cpx0, n0, cpx1, n1, dummyx2, 1, dummyx3, 1, dummyflag, false, vy, _params);
        for (i = 0; i <= n0 - 1; i++)
        {
            for (j = 0; j <= n1 - 1; j++)
            {
                y[i, j] = vy[i + j * n0];
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
    public static void rbfv2gridcalcvx(rbfv2model s,
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
        int nx = 0;
        int ny = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        double[] tx = new double[0];
        double[] ty = new double[0];
        double[] z = new double[0];
        int dstoffs = 0;
        int dummy = 0;
        rbfv2gridcalcbuffer bufseedv2 = new rbfv2gridcalcbuffer();
        smp.shared_pool bufpool = new smp.shared_pool();
        int rowidx = 0;
        int rowcnt = 0;
        double v = 0;
        double rcur = 0;
        int levelidx = 0;
        double searchradius2 = 0;
        int ntrials = 0;
        double avgfuncpernode = 0;
        hqrnd.hqrndstate rs = new hqrnd.hqrndstate();
        int[] blocks0 = new int[0];
        int[] blocks1 = new int[0];
        int[] blocks2 = new int[0];
        int[] blocks3 = new int[0];
        int blockscnt0 = 0;
        int blockscnt1 = 0;
        int blockscnt2 = 0;
        int blockscnt3 = 0;
        double blockwidth0 = 0;
        double blockwidth1 = 0;
        double blockwidth2 = 0;
        double blockwidth3 = 0;
        int maxblocksize = 0;

        nx = s.nx;
        ny = s.ny;
        hqrnd.hqrndseed(532, 54734, rs, _params);

        //
        // Perform integrity checks
        //
        ap.assert(s.nx == 2 || s.nx == 3, "RBFGridCalcVX: integrity check failed");
        ap.assert(s.nx >= 4 || ((ap.len(x3) >= 1 && (double)(x3[0]) == (double)(0)) && n3 == 1), "RBFGridCalcVX: integrity check failed");
        ap.assert(s.nx >= 3 || ((ap.len(x2) >= 1 && (double)(x2[0]) == (double)(0)) && n2 == 1), "RBFGridCalcVX: integrity check failed");
        ap.assert(s.nx >= 2 || ((ap.len(x1) >= 1 && (double)(x1[0]) == (double)(0)) && n1 == 1), "RBFGridCalcVX: integrity check failed");

        //
        // Allocate arrays
        //
        ap.assert(s.nx <= 4, "RBFGridCalcVX: integrity check failed");
        z = new double[ny];
        tx = new double[4];
        ty = new double[ny];

        //
        // Calculate linear term
        //
        rowcnt = n1 * n2 * n3;
        for (rowidx = 0; rowidx <= rowcnt - 1; rowidx++)
        {

            //
            // Calculate TX - current position
            //
            k = rowidx;
            tx[0] = 0;
            tx[1] = x1[k % n1];
            k = k / n1;
            tx[2] = x2[k % n2];
            k = k / n2;
            tx[3] = x3[k % n3];
            k = k / n3;
            ap.assert(k == 0, "RBFGridCalcVX: integrity check failed");
            for (j = 0; j <= ny - 1; j++)
            {
                v = s.v[j, nx];
                for (k = 1; k <= nx - 1; k++)
                {
                    v = v + tx[k] * s.v[j, k];
                }
                z[j] = v;
            }
            for (i = 0; i <= n0 - 1; i++)
            {
                dstoffs = ny * (rowidx * n0 + i);
                if (sparsey && !flagy[rowidx * n0 + i])
                {
                    for (j = 0; j <= ny - 1; j++)
                    {
                        y[j + dstoffs] = 0;
                    }
                    continue;
                }
                v = x0[i];
                for (j = 0; j <= ny - 1; j++)
                {
                    y[j + dstoffs] = z[j] + v * s.v[j, 0];
                }
            }
        }
        if (s.nh == 0)
        {
            return;
        }

        //
        // Process RBF terms, layer by layer
        //
        for (levelidx = 0; levelidx <= s.nh - 1; levelidx++)
        {
            rcur = s.ri[levelidx];
            blockwidth0 = 1;
            blockwidth1 = 1;
            blockwidth2 = 1;
            blockwidth3 = 1;
            if (nx >= 1)
            {
                blockwidth0 = rcur * s.s[0];
            }
            if (nx >= 2)
            {
                blockwidth1 = rcur * s.s[1];
            }
            if (nx >= 3)
            {
                blockwidth2 = rcur * s.s[2];
            }
            if (nx >= 4)
            {
                blockwidth3 = rcur * s.s[3];
            }
            maxblocksize = 8;

            //
            // Group grid nodes into blocks according to current radius
            //
            blocks0 = new int[n0 + 1];
            blockscnt0 = 0;
            blocks0[0] = 0;
            for (i = 1; i <= n0 - 1; i++)
            {
                if ((double)(x0[i] - x0[blocks0[blockscnt0]]) > (double)(blockwidth0) || i - blocks0[blockscnt0] >= maxblocksize)
                {
                    apserv.inc(ref blockscnt0, _params);
                    blocks0[blockscnt0] = i;
                }
            }
            apserv.inc(ref blockscnt0, _params);
            blocks0[blockscnt0] = n0;
            blocks1 = new int[n1 + 1];
            blockscnt1 = 0;
            blocks1[0] = 0;
            for (i = 1; i <= n1 - 1; i++)
            {
                if ((double)(x1[i] - x1[blocks1[blockscnt1]]) > (double)(blockwidth1) || i - blocks1[blockscnt1] >= maxblocksize)
                {
                    apserv.inc(ref blockscnt1, _params);
                    blocks1[blockscnt1] = i;
                }
            }
            apserv.inc(ref blockscnt1, _params);
            blocks1[blockscnt1] = n1;
            blocks2 = new int[n2 + 1];
            blockscnt2 = 0;
            blocks2[0] = 0;
            for (i = 1; i <= n2 - 1; i++)
            {
                if ((double)(x2[i] - x2[blocks2[blockscnt2]]) > (double)(blockwidth2) || i - blocks2[blockscnt2] >= maxblocksize)
                {
                    apserv.inc(ref blockscnt2, _params);
                    blocks2[blockscnt2] = i;
                }
            }
            apserv.inc(ref blockscnt2, _params);
            blocks2[blockscnt2] = n2;
            blocks3 = new int[n3 + 1];
            blockscnt3 = 0;
            blocks3[0] = 0;
            for (i = 1; i <= n3 - 1; i++)
            {
                if ((double)(x3[i] - x3[blocks3[blockscnt3]]) > (double)(blockwidth3) || i - blocks3[blockscnt3] >= maxblocksize)
                {
                    apserv.inc(ref blockscnt3, _params);
                    blocks3[blockscnt3] = i;
                }
            }
            apserv.inc(ref blockscnt3, _params);
            blocks3[blockscnt3] = n3;

            //
            // Prepare seed for shared pool
            //
            allocatecalcbuffer(s, bufseedv2.calcbuf, _params);
            smp.ae_shared_pool_set_seed(bufpool, bufseedv2);

            //
            // Determine average number of neighbor per node
            //
            searchradius2 = math.sqr(rcur * rbfv2farradius(s.bf, _params));
            ntrials = 100;
            avgfuncpernode = 0.0;
            for (i = 0; i <= ntrials - 1; i++)
            {
                tx[0] = x0[hqrnd.hqrnduniformi(rs, n0, _params)];
                tx[1] = x1[hqrnd.hqrnduniformi(rs, n1, _params)];
                tx[2] = x2[hqrnd.hqrnduniformi(rs, n2, _params)];
                tx[3] = x3[hqrnd.hqrnduniformi(rs, n3, _params)];
                preparepartialquery(tx, s.kdboxmin, s.kdboxmax, nx, bufseedv2.calcbuf, ref dummy, _params);
                avgfuncpernode = avgfuncpernode + (double)partialcountrec(s.kdnodes, s.kdsplits, s.cw, nx, ny, bufseedv2.calcbuf, s.kdroots[levelidx], searchradius2, tx, _params) / (double)ntrials;
            }

            //
            // Perform calculation in multithreaded mode
            //
            rbfv2partialgridcalcrec(s, x0, n0, x1, n1, x2, n2, x3, n3, blocks0, 0, blockscnt0, blocks1, 0, blockscnt1, blocks2, 0, blockscnt2, blocks3, 0, blockscnt3, flagy, sparsey, levelidx, avgfuncpernode, bufpool, y, _params);
        }
    }


    public static void rbfv2partialgridcalcrec(rbfv2model s,
        double[] x0,
        int n0,
        double[] x1,
        int n1,
        double[] x2,
        int n2,
        double[] x3,
        int n3,
        int[] blocks0,
        int block0a,
        int block0b,
        int[] blocks1,
        int block1a,
        int block1b,
        int[] blocks2,
        int block2a,
        int block2b,
        int[] blocks3,
        int block3a,
        int block3b,
        bool[] flagy,
        bool sparsey,
        int levelidx,
        double avgfuncpernode,
        smp.shared_pool bufpool,
        double[] y,
        xparams _params)
    {
        int nx = 0;
        int ny = 0;
        int k = 0;
        int l = 0;
        int blkidx = 0;
        int blkcnt = 0;
        int nodeidx = 0;
        int nodescnt = 0;
        int rowidx = 0;
        int rowscnt = 0;
        int i0 = 0;
        int i1 = 0;
        int i2 = 0;
        int i3 = 0;
        int j0 = 0;
        int j1 = 0;
        int j2 = 0;
        int j3 = 0;
        double rcur = 0;
        double invrc2 = 0;
        double rquery2 = 0;
        double rfar2 = 0;
        int dstoffs = 0;
        int srcoffs = 0;
        int dummy = 0;
        double rowwidth = 0;
        double maxrowwidth = 0;
        double problemcost = 0;
        int maxbs = 0;
        int midpoint = 0;
        bool emptyrow = new bool();
        rbfv2gridcalcbuffer buf = null;

        nx = s.nx;
        ny = s.ny;

        //
        // Integrity checks
        //
        ap.assert(s.nx == 2 || s.nx == 3, "RBFV2PartialGridCalcRec: integrity check failed");

        //
        // Try to split large problem
        //
        problemcost = s.ny * 2 * (avgfuncpernode + 1);
        problemcost = problemcost * (blocks0[block0b] - blocks0[block0a]);
        problemcost = problemcost * (blocks1[block1b] - blocks1[block1a]);
        problemcost = problemcost * (blocks2[block2b] - blocks2[block2a]);
        problemcost = problemcost * (blocks3[block3b] - blocks3[block3a]);
        maxbs = 0;
        maxbs = Math.Max(maxbs, block0b - block0a);
        maxbs = Math.Max(maxbs, block1b - block1a);
        maxbs = Math.Max(maxbs, block2b - block2a);
        maxbs = Math.Max(maxbs, block3b - block3a);
        if ((double)(problemcost * complexitymultiplier) >= (double)(apserv.smpactivationlevel(_params)))
        {
            if (_trypexec_rbfv2partialgridcalcrec(s, x0, n0, x1, n1, x2, n2, x3, n3, blocks0, block0a, block0b, blocks1, block1a, block1b, blocks2, block2a, block2b, blocks3, block3a, block3b, flagy, sparsey, levelidx, avgfuncpernode, bufpool, y, _params))
            {
                return;
            }
        }
        if ((double)(problemcost * complexitymultiplier) >= (double)(apserv.spawnlevel(_params)) && maxbs >= 2)
        {
            if (block0b - block0a == maxbs)
            {
                midpoint = block0a + maxbs / 2;
                rbfv2partialgridcalcrec(s, x0, n0, x1, n1, x2, n2, x3, n3, blocks0, block0a, midpoint, blocks1, block1a, block1b, blocks2, block2a, block2b, blocks3, block3a, block3b, flagy, sparsey, levelidx, avgfuncpernode, bufpool, y, _params);
                rbfv2partialgridcalcrec(s, x0, n0, x1, n1, x2, n2, x3, n3, blocks0, midpoint, block0b, blocks1, block1a, block1b, blocks2, block2a, block2b, blocks3, block3a, block3b, flagy, sparsey, levelidx, avgfuncpernode, bufpool, y, _params);
                return;
            }
            if (block1b - block1a == maxbs)
            {
                midpoint = block1a + maxbs / 2;
                rbfv2partialgridcalcrec(s, x0, n0, x1, n1, x2, n2, x3, n3, blocks0, block0a, block0b, blocks1, block1a, midpoint, blocks2, block2a, block2b, blocks3, block3a, block3b, flagy, sparsey, levelidx, avgfuncpernode, bufpool, y, _params);
                rbfv2partialgridcalcrec(s, x0, n0, x1, n1, x2, n2, x3, n3, blocks0, block0a, block0b, blocks1, midpoint, block1b, blocks2, block2a, block2b, blocks3, block3a, block3b, flagy, sparsey, levelidx, avgfuncpernode, bufpool, y, _params);
                return;
            }
            if (block2b - block2a == maxbs)
            {
                midpoint = block2a + maxbs / 2;
                rbfv2partialgridcalcrec(s, x0, n0, x1, n1, x2, n2, x3, n3, blocks0, block0a, block0b, blocks1, block1a, block1b, blocks2, block2a, midpoint, blocks3, block3a, block3b, flagy, sparsey, levelidx, avgfuncpernode, bufpool, y, _params);
                rbfv2partialgridcalcrec(s, x0, n0, x1, n1, x2, n2, x3, n3, blocks0, block0a, block0b, blocks1, block1a, block1b, blocks2, midpoint, block2b, blocks3, block3a, block3b, flagy, sparsey, levelidx, avgfuncpernode, bufpool, y, _params);
                return;
            }
            if (block3b - block3a == maxbs)
            {
                midpoint = block3a + maxbs / 2;
                rbfv2partialgridcalcrec(s, x0, n0, x1, n1, x2, n2, x3, n3, blocks0, block0a, block0b, blocks1, block1a, block1b, blocks2, block2a, block2b, blocks3, block3a, midpoint, flagy, sparsey, levelidx, avgfuncpernode, bufpool, y, _params);
                rbfv2partialgridcalcrec(s, x0, n0, x1, n1, x2, n2, x3, n3, blocks0, block0a, block0b, blocks1, block1a, block1b, blocks2, block2a, block2b, blocks3, midpoint, block3b, flagy, sparsey, levelidx, avgfuncpernode, bufpool, y, _params);
                return;
            }
            ap.assert(false, "RBFV2PartialGridCalcRec: integrity check failed");
        }

        //
        // Retrieve buffer object from pool (it will be returned later)
        //
        smp.ae_shared_pool_retrieve(bufpool, ref buf);

        //
        // Calculate RBF model
        //
        ap.assert(nx <= 4, "RBFV2PartialGridCalcRec: integrity check failed");
        buf.tx = new double[4];
        buf.cx = new double[4];
        buf.ty = new double[ny];
        rcur = s.ri[levelidx];
        invrc2 = 1 / (rcur * rcur);
        blkcnt = (block3b - block3a) * (block2b - block2a) * (block1b - block1a) * (block0b - block0a);
        for (blkidx = 0; blkidx <= blkcnt - 1; blkidx++)
        {

            //
            // Select block (I0,I1,I2,I3).
            //
            // NOTE: for problems with NX<4 corresponding I_? are zero.
            //
            k = blkidx;
            i0 = block0a + k % (block0b - block0a);
            k = k / (block0b - block0a);
            i1 = block1a + k % (block1b - block1a);
            k = k / (block1b - block1a);
            i2 = block2a + k % (block2b - block2a);
            k = k / (block2b - block2a);
            i3 = block3a + k % (block3b - block3a);
            k = k / (block3b - block3a);
            ap.assert(k == 0, "RBFV2PartialGridCalcRec: integrity check failed");

            //
            // We partitioned grid into blocks and selected block with
            // index (I0,I1,I2,I3). This block is a 4D cube (some dimensions
            // may be zero) of nodes with indexes (J0,J1,J2,J3), which is
            // further partitioned into a set of rows, each row corresponding
            // to indexes J1...J3 being fixed.
            //
            // We process block row by row, and each row may be handled
            // by either "generic" (nodes are processed separately) or
            // batch algorithm (that's the reason to use rows, after all).
            //
            //
            // Process nodes of the block
            //
            rowscnt = (blocks3[i3 + 1] - blocks3[i3]) * (blocks2[i2 + 1] - blocks2[i2]) * (blocks1[i1 + 1] - blocks1[i1]);
            for (rowidx = 0; rowidx <= rowscnt - 1; rowidx++)
            {

                //
                // Find out node indexes (*,J1,J2,J3).
                //
                // NOTE: for problems with NX<4 corresponding J_? are zero.
                //
                k = rowidx;
                j1 = blocks1[i1] + k % (blocks1[i1 + 1] - blocks1[i1]);
                k = k / (blocks1[i1 + 1] - blocks1[i1]);
                j2 = blocks2[i2] + k % (blocks2[i2 + 1] - blocks2[i2]);
                k = k / (blocks2[i2 + 1] - blocks2[i2]);
                j3 = blocks3[i3] + k % (blocks3[i3 + 1] - blocks3[i3]);
                k = k / (blocks3[i3 + 1] - blocks3[i3]);
                ap.assert(k == 0, "RBFV2PartialGridCalcRec: integrity check failed");

                //
                // Analyze row, skip completely empty rows
                //
                nodescnt = blocks0[i0 + 1] - blocks0[i0];
                srcoffs = blocks0[i0] + (j1 + (j2 + j3 * n2) * n1) * n0;
                emptyrow = true;
                for (nodeidx = 0; nodeidx <= nodescnt - 1; nodeidx++)
                {
                    emptyrow = emptyrow && (sparsey && !flagy[srcoffs + nodeidx]);
                }
                if (emptyrow)
                {
                    continue;
                }

                //
                // Process row - use either "batch" (rowsize>1) or "generic"
                // (row size is 1) algorithm.
                //
                // NOTE: "generic" version may also be used as fallback code for
                //       situations when we do not want to use batch code.
                //
                maxrowwidth = 0.5 * rbfv2nearradius(s.bf, _params) * rcur * s.s[0];
                rowwidth = x0[blocks0[i0 + 1] - 1] - x0[blocks0[i0]];
                if (nodescnt > 1 && (double)(rowwidth) <= (double)(maxrowwidth))
                {

                    //
                    // "Batch" code which processes entire row at once, saving
                    // some time in kd-tree search code.
                    //
                    rquery2 = math.sqr(rcur * rbfv2farradius(s.bf, _params) + 0.5 * rowwidth / s.s[0]);
                    rfar2 = math.sqr(rcur * rbfv2farradius(s.bf, _params));
                    j0 = blocks0[i0];
                    if (nx > 0)
                    {
                        buf.cx[0] = (x0[j0] + 0.5 * rowwidth) / s.s[0];
                    }
                    if (nx > 1)
                    {
                        buf.cx[1] = x1[j1] / s.s[1];
                    }
                    if (nx > 2)
                    {
                        buf.cx[2] = x2[j2] / s.s[2];
                    }
                    if (nx > 3)
                    {
                        buf.cx[3] = x3[j3] / s.s[3];
                    }
                    srcoffs = j0 + (j1 + (j2 + j3 * n2) * n1) * n0;
                    dstoffs = ny * srcoffs;
                    apserv.rvectorsetlengthatleast(ref buf.rx, nodescnt, _params);
                    apserv.bvectorsetlengthatleast(ref buf.rf, nodescnt, _params);
                    apserv.rvectorsetlengthatleast(ref buf.ry, nodescnt * ny, _params);
                    for (nodeidx = 0; nodeidx <= nodescnt - 1; nodeidx++)
                    {
                        buf.rx[nodeidx] = x0[j0 + nodeidx] / s.s[0];
                        buf.rf[nodeidx] = !sparsey || flagy[srcoffs + nodeidx];
                    }
                    for (k = 0; k <= nodescnt * ny - 1; k++)
                    {
                        buf.ry[k] = 0;
                    }
                    preparepartialquery(buf.cx, s.kdboxmin, s.kdboxmax, nx, buf.calcbuf, ref dummy, _params);
                    partialrowcalcrec(s, buf.calcbuf, s.kdroots[levelidx], invrc2, rquery2, rfar2, buf.cx, buf.rx, buf.rf, nodescnt, buf.ry, _params);
                    for (k = 0; k <= nodescnt * ny - 1; k++)
                    {
                        y[dstoffs + k] = y[dstoffs + k] + buf.ry[k];
                    }
                }
                else
                {

                    //
                    // "Generic" code. Although we usually move here
                    // only when NodesCnt=1, we still use a loop on
                    // NodeIdx just to be able to use this branch as
                    // fallback code without any modifications.
                    //
                    rquery2 = math.sqr(rcur * rbfv2farradius(s.bf, _params));
                    for (nodeidx = 0; nodeidx <= nodescnt - 1; nodeidx++)
                    {

                        //
                        // Prepare TX - current point
                        //
                        j0 = blocks0[i0] + nodeidx;
                        if (nx > 0)
                        {
                            buf.tx[0] = x0[j0] / s.s[0];
                        }
                        if (nx > 1)
                        {
                            buf.tx[1] = x1[j1] / s.s[1];
                        }
                        if (nx > 2)
                        {
                            buf.tx[2] = x2[j2] / s.s[2];
                        }
                        if (nx > 3)
                        {
                            buf.tx[3] = x3[j3] / s.s[3];
                        }

                        //
                        // Evaluate and add to Y
                        //
                        srcoffs = j0 + (j1 + (j2 + j3 * n2) * n1) * n0;
                        dstoffs = ny * srcoffs;
                        for (l = 0; l <= ny - 1; l++)
                        {
                            buf.ty[l] = 0;
                        }
                        if (!sparsey || flagy[srcoffs])
                        {
                            preparepartialquery(buf.tx, s.kdboxmin, s.kdboxmax, nx, buf.calcbuf, ref dummy, _params);
                            partialcalcrec(s, buf.calcbuf, s.kdroots[levelidx], invrc2, rquery2, buf.tx, buf.ty, buf.ty, buf.ty, 0, _params);
                        }
                        for (l = 0; l <= ny - 1; l++)
                        {
                            y[dstoffs + l] = y[dstoffs + l] + buf.ty[l];
                        }
                    }
                }
            }
        }

        //
        // Recycle buffer object back to pool
        //
        smp.ae_shared_pool_recycle(bufpool, ref buf);
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_rbfv2partialgridcalcrec(rbfv2model s,
        double[] x0,
        int n0,
        double[] x1,
        int n1,
        double[] x2,
        int n2,
        double[] x3,
        int n3,
        int[] blocks0,
        int block0a,
        int block0b,
        int[] blocks1,
        int block1a,
        int block1b,
        int[] blocks2,
        int block2a,
        int block2b,
        int[] blocks3,
        int block3a,
        int block3b,
        bool[] flagy,
        bool sparsey,
        int levelidx,
        double avgfuncpernode,
        smp.shared_pool bufpool,
        double[] y, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    This function "unpacks" RBF model by extracting its coefficients.

    INPUT PARAMETERS:
        S       -   RBF model

    OUTPUT PARAMETERS:
        NX      -   dimensionality of argument
        NY      -   dimensionality of the target function
        XWR     -   model information, array[NC,NX+NY+1].
                    One row of the array corresponds to one basis function:
                    * first NX columns  - coordinates of the center 
                    * next NY columns   - weights, one per dimension of the 
                                          function being modelled
                    * last NX columns   - radii, per dimension
        NC      -   number of the centers
        V       -   polynomial  term , array[NY,NX+1]. One row per one 
                    dimension of the function being modelled. First NX 
                    elements are linear coefficients, V[NX] is equal to the 
                    constant part.

      -- ALGLIB --
         Copyright 13.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv2unpack(rbfv2model s,
        ref int nx,
        ref int ny,
        ref double[,] xwr,
        ref int nc,
        ref double[,] v,
        xparams _params)
    {
        int i = 0;
        int ncactual = 0;
        int i_ = 0;

        nx = 0;
        ny = 0;
        xwr = new double[0, 0];
        nc = 0;
        v = new double[0, 0];

        nx = s.nx;
        ny = s.ny;
        nc = 0;

        //
        // Fill V
        //
        v = new double[s.ny, s.nx + 1];
        for (i = 0; i <= s.ny - 1; i++)
        {
            for (i_ = 0; i_ <= s.nx; i_++)
            {
                v[i, i_] = s.v[i, i_];
            }
        }

        //
        // Fill XWR
        //
        ap.assert(ap.len(s.cw) % (s.nx + s.ny) == 0, "RBFV2Unpack: integrity error");
        nc = ap.len(s.cw) / (s.nx + s.ny);
        ncactual = 0;
        if (nc > 0)
        {
            xwr = new double[nc, s.nx + s.ny + s.nx];
            for (i = 0; i <= s.nh - 1; i++)
            {
                partialunpackrec(s.kdnodes, s.kdsplits, s.cw, s.s, s.nx, s.ny, s.kdroots[i], s.ri[i], xwr, ref ncactual, _params);
            }
        }
        ap.assert(nc == ncactual, "RBFV2Unpack: integrity error");
    }


    private static bool rbfv2buildlinearmodel(double[,] x,
        ref double[,] y,
        int n,
        int nx,
        int ny,
        int modeltype,
        ref double[,] v,
        xparams _params)
    {
        bool result = new bool();
        double[] tmpy = new double[0];
        double[,] a = new double[0, 0];
        double scaling = 0;
        double[] shifting = new double[0];
        double mn = 0;
        double mx = 0;
        double[] c = new double[0];
        lsfit.lsfitreport rep = new lsfit.lsfitreport();
        int i = 0;
        int j = 0;
        int k = 0;

        v = new double[0, 0];

        ap.assert(n >= 0, "BuildLinearModel: N<0");
        ap.assert(nx > 0, "BuildLinearModel: NX<=0");
        ap.assert(ny > 0, "BuildLinearModel: NY<=0");

        //
        // Handle degenerate case (N=0)
        //
        result = true;
        v = new double[ny, nx + 1];
        if (n == 0)
        {
            for (j = 0; j <= nx; j++)
            {
                for (i = 0; i <= ny - 1; i++)
                {
                    v[i, j] = 0;
                }
            }
            return result;
        }

        //
        // Allocate temporaries
        //
        tmpy = new double[n];

        //
        // General linear model.
        //
        if (modeltype == 1)
        {

            //
            // Calculate scaling/shifting, transform variables, prepare LLS problem
            //
            a = new double[n, nx + 1];
            shifting = new double[nx];
            scaling = 0;
            for (i = 0; i <= nx - 1; i++)
            {
                mn = x[0, i];
                mx = mn;
                for (j = 1; j <= n - 1; j++)
                {
                    if ((double)(mn) > (double)(x[j, i]))
                    {
                        mn = x[j, i];
                    }
                    if ((double)(mx) < (double)(x[j, i]))
                    {
                        mx = x[j, i];
                    }
                }
                scaling = Math.Max(scaling, mx - mn);
                shifting[i] = 0.5 * (mx + mn);
            }
            if ((double)(scaling) == (double)(0))
            {
                scaling = 1;
            }
            else
            {
                scaling = 0.5 * scaling;
            }
            for (i = 0; i <= n - 1; i++)
            {
                for (j = 0; j <= nx - 1; j++)
                {
                    a[i, j] = (x[i, j] - shifting[j]) / scaling;
                }
            }
            for (i = 0; i <= n - 1; i++)
            {
                a[i, nx] = 1;
            }

            //
            // Solve linear system in transformed variables, make backward 
            //
            for (i = 0; i <= ny - 1; i++)
            {
                for (j = 0; j <= n - 1; j++)
                {
                    tmpy[j] = y[j, i];
                }
                lsfit.lsfitlinear(tmpy, a, n, nx + 1, ref c, rep, _params);
                if (rep.terminationtype <= 0)
                {
                    result = false;
                    return result;
                }
                for (j = 0; j <= nx - 1; j++)
                {
                    v[i, j] = c[j] / scaling;
                }
                v[i, nx] = c[nx];
                for (j = 0; j <= nx - 1; j++)
                {
                    v[i, nx] = v[i, nx] - shifting[j] * v[i, j];
                }
                for (j = 0; j <= n - 1; j++)
                {
                    for (k = 0; k <= nx - 1; k++)
                    {
                        y[j, i] = y[j, i] - x[j, k] * v[i, k];
                    }
                    y[j, i] = y[j, i] - v[i, nx];
                }
            }
            return result;
        }

        //
        // Constant model, very simple
        //
        if (modeltype == 2)
        {
            for (i = 0; i <= ny - 1; i++)
            {
                for (j = 0; j <= nx; j++)
                {
                    v[i, j] = 0;
                }
                for (j = 0; j <= n - 1; j++)
                {
                    v[i, nx] = v[i, nx] + y[j, i];
                }
                if (n > 0)
                {
                    v[i, nx] = v[i, nx] / n;
                }
                for (j = 0; j <= n - 1; j++)
                {
                    y[j, i] = y[j, i] - v[i, nx];
                }
            }
            return result;
        }

        //
        // Zero model
        //
        ap.assert(modeltype == 3, "BuildLinearModel: unknown model type");
        for (i = 0; i <= ny - 1; i++)
        {
            for (j = 0; j <= nx; j++)
            {
                v[i, j] = 0;
            }
        }
        return result;
    }


    /*************************************************************************
    Reallocates calcBuf if necessary, reuses previously allocated space if
    possible.

      -- ALGLIB --
         Copyright 20.06.2016 by Sergey Bochkanov
    *************************************************************************/
    private static void allocatecalcbuffer(rbfv2model s,
        rbfv2calcbuffer buf,
        xparams _params)
    {
        if (ap.len(buf.x) < s.nx)
        {
            buf.x = new double[s.nx];
        }
        if (ap.len(buf.curboxmin) < s.nx)
        {
            buf.curboxmin = new double[s.nx];
        }
        if (ap.len(buf.curboxmax) < s.nx)
        {
            buf.curboxmax = new double[s.nx];
        }
        if (ap.len(buf.x123) < s.nx)
        {
            buf.x123 = new double[s.nx];
        }
        if (ap.len(buf.y123) < s.ny)
        {
            buf.y123 = new double[s.ny];
        }
    }


    /*************************************************************************
    Extracts structure (and XY-values too) from  kd-tree  built  for  a  small
    subset of points and appends it to multi-tree.


      -- ALGLIB --
         Copyright 20.06.2016 by Sergey Bochkanov
    *************************************************************************/
    private static void convertandappendtree(nearestneighbor.kdtree curtree,
        int n,
        int nx,
        int ny,
        ref int[] kdnodes,
        ref double[] kdsplits,
        ref double[] cw,
        xparams _params)
    {
        int nodesbase = 0;
        int splitsbase = 0;
        int cwbase = 0;
        int[] localnodes = new int[0];
        double[] localsplits = new double[0];
        double[] localcw = new double[0];
        double[,] xybuf = new double[0, 0];
        int localnodessize = 0;
        int localsplitssize = 0;
        int localcwsize = 0;
        int i = 0;


        //
        // Calculate base offsets
        //
        nodesbase = ap.len(kdnodes);
        splitsbase = ap.len(kdsplits);
        cwbase = ap.len(cw);

        //
        // Prepare local copy of tree
        //
        localnodes = new int[n * maxnodesize];
        localsplits = new double[n];
        localcw = new double[(nx + ny) * n];
        localnodessize = 0;
        localsplitssize = 0;
        localcwsize = 0;
        converttreerec(curtree, n, nx, ny, 0, nodesbase, splitsbase, cwbase, localnodes, ref localnodessize, localsplits, ref localsplitssize, localcw, ref localcwsize, ref xybuf, _params);

        //
        // Append to multi-tree
        //
        apserv.ivectorresize(ref kdnodes, ap.len(kdnodes) + localnodessize, _params);
        apserv.rvectorresize(ref kdsplits, ap.len(kdsplits) + localsplitssize, _params);
        apserv.rvectorresize(ref cw, ap.len(cw) + localcwsize, _params);
        for (i = 0; i <= localnodessize - 1; i++)
        {
            kdnodes[nodesbase + i] = localnodes[i];
        }
        for (i = 0; i <= localsplitssize - 1; i++)
        {
            kdsplits[splitsbase + i] = localsplits[i];
        }
        for (i = 0; i <= localcwsize - 1; i++)
        {
            cw[cwbase + i] = localcw[i];
        }
    }


    /*************************************************************************
    Recurrent tree conversion

        CurTree         -   tree to convert
        N, NX, NY       -   dataset metrics
        NodeOffset      -   offset of current tree node, 0 for root
        NodesBase       -   a value which is added to intra-tree node indexes;
                            although this tree is stored in separate array, it
                            is intended to be stored in the larger tree,  with
                            localNodes being moved to offset NodesBase.
        SplitsBase      -   similarly, offset of localSplits in the final tree
        CWBase          -   similarly, offset of localCW in the final tree
    *************************************************************************/
    private static void converttreerec(nearestneighbor.kdtree curtree,
        int n,
        int nx,
        int ny,
        int nodeoffset,
        int nodesbase,
        int splitsbase,
        int cwbase,
        int[] localnodes,
        ref int localnodessize,
        double[] localsplits,
        ref int localsplitssize,
        double[] localcw,
        ref int localcwsize,
        ref double[,] xybuf,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int nodetype = 0;
        int cnt = 0;
        int d = 0;
        double s = 0;
        int nodele = 0;
        int nodege = 0;
        int oldnodessize = 0;

        nearestneighbor.kdtreeexplorenodetype(curtree, nodeoffset, ref nodetype, _params);

        //
        // Leaf node
        //
        if (nodetype == 0)
        {
            nearestneighbor.kdtreeexploreleaf(curtree, nodeoffset, ref xybuf, ref cnt, _params);
            ap.assert(ap.len(localnodes) >= localnodessize + 2, "ConvertTreeRec: integrity check failed");
            ap.assert(ap.len(localcw) >= localcwsize + cnt * (nx + ny), "ConvertTreeRec: integrity check failed");
            localnodes[localnodessize + 0] = cnt;
            localnodes[localnodessize + 1] = cwbase + localcwsize;
            localnodessize = localnodessize + 2;
            for (i = 0; i <= cnt - 1; i++)
            {
                for (j = 0; j <= nx + ny - 1; j++)
                {
                    localcw[localcwsize + i * (nx + ny) + j] = xybuf[i, j];
                }
            }
            localcwsize = localcwsize + cnt * (nx + ny);
            return;
        }

        //
        // Split node
        //
        if (nodetype == 1)
        {
            nearestneighbor.kdtreeexploresplit(curtree, nodeoffset, ref d, ref s, ref nodele, ref nodege, _params);
            ap.assert(ap.len(localnodes) >= localnodessize + maxnodesize, "ConvertTreeRec: integrity check failed");
            ap.assert(ap.len(localsplits) >= localsplitssize + 1, "ConvertTreeRec: integrity check failed");
            oldnodessize = localnodessize;
            localnodes[localnodessize + 0] = 0;
            localnodes[localnodessize + 1] = d;
            localnodes[localnodessize + 2] = splitsbase + localsplitssize;
            localnodes[localnodessize + 3] = -1;
            localnodes[localnodessize + 4] = -1;
            localnodessize = localnodessize + 5;
            localsplits[localsplitssize + 0] = s;
            localsplitssize = localsplitssize + 1;
            localnodes[oldnodessize + 3] = nodesbase + localnodessize;
            converttreerec(curtree, n, nx, ny, nodele, nodesbase, splitsbase, cwbase, localnodes, ref localnodessize, localsplits, ref localsplitssize, localcw, ref localcwsize, ref xybuf, _params);
            localnodes[oldnodessize + 4] = nodesbase + localnodessize;
            converttreerec(curtree, n, nx, ny, nodege, nodesbase, splitsbase, cwbase, localnodes, ref localnodessize, localsplits, ref localsplitssize, localcw, ref localcwsize, ref xybuf, _params);
            return;
        }

        //
        // Integrity error
        //
        ap.assert(false, "ConvertTreeRec: integrity check failed");
    }


    /*************************************************************************
    This function performs partial calculation of  hierarchical  model:  given
    evaluation point X and partially computed value Y, it updates Y by  values
    computed using part of multi-tree given by RootIdx.

    INPUT PARAMETERS:
        S       -   V2 model
        Buf     -   calc-buffer, this function uses following fields:
                    * Buf.CurBoxMin - should be set by caller
                    * Buf.CurBoxMax - should be set by caller
                    * Buf.CurDist2  - squared distance from X to current bounding box,
                      should be set by caller
        RootIdx -   offset of partial kd-tree
        InvR2   -   1/R^2, where R is basis function radius
        QueryR2 -   squared query radius, usually it is (R*FarRadius(BasisFunction))^2
        X       -   evaluation point, array[NX]
        Y       -   current value for target, array[NY]
        DY      -   current value for derivative, array[NY*NX], if NeedDY>=1
        D2Y     -   current value for derivative, array[NY*NX*NX], if NeedDY>=2
        NeedDY  -   whether derivatives are required or not:
                    * 0 if only Y is needed
                    * 1 if Y and DY are needed
                    * 2 if Y, DY, D2Y are needed
        
    OUTPUT PARAMETERS
        Y       -   updated partial value
        DY      -   updated derivatives, if NeedDY>=1
        D2Y     -   updated Hessian, if NeedDY>=2

      -- ALGLIB --
         Copyright 20.06.2016 by Bochkanov Sergey
    *************************************************************************/
    private static void partialcalcrec(rbfv2model s,
        rbfv2calcbuffer buf,
        int rootidx,
        double invr2,
        double queryr2,
        double[] x,
        double[] y,
        double[] dy,
        double[] d2y,
        int needdy,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        int k0 = 0;
        int k1 = 0;
        double ptdist2 = 0;
        double w = 0;
        double v = 0;
        double v0 = 0;
        double v1 = 0;
        int cwoffs = 0;
        int cwcnt = 0;
        int itemoffs = 0;
        double arg = 0;
        double val = 0;
        double df = 0;
        double d2f = 0;
        int d = 0;
        double split = 0;
        int childle = 0;
        int childge = 0;
        int childoffs = 0;
        bool updatemin = new bool();
        double prevdist2 = 0;
        double t1 = 0;
        int nx = 0;
        int ny = 0;

        nx = s.nx;
        ny = s.ny;

        //
        // Helps to avoid spurious warnings
        //
        val = 0;

        //
        // Leaf node.
        //
        if (s.kdnodes[rootidx] > 0)
        {
            cwcnt = s.kdnodes[rootidx + 0];
            cwoffs = s.kdnodes[rootidx + 1];
            for (i = 0; i <= cwcnt - 1; i++)
            {

                //
                // Calculate distance
                //
                itemoffs = cwoffs + i * (nx + ny);
                ptdist2 = 0;
                for (j = 0; j <= nx - 1; j++)
                {
                    v = s.cw[itemoffs + j] - x[j];
                    ptdist2 = ptdist2 + v * v;
                }

                //
                // Skip points if distance too large
                //
                if (ptdist2 >= queryr2)
                {
                    continue;
                }

                //
                // Update Y
                //
                arg = ptdist2 * invr2;
                val = 0;
                df = 0;
                d2f = 0;
                if (needdy == 2)
                {
                    if (s.bf == 0)
                    {
                        val = Math.Exp(-arg);
                        df = -val;
                        d2f = val;
                    }
                    else
                    {
                        if (s.bf == 1)
                        {
                            rbfv2basisfuncdiff2(s.bf, arg, ref val, ref df, ref d2f, _params);
                        }
                        else
                        {
                            ap.assert(false, "PartialCalcRec: integrity check failed");
                        }
                    }
                    for (j = 0; j <= ny - 1; j++)
                    {
                        y[j] = y[j] + val * s.cw[itemoffs + nx + j];
                        w = s.cw[itemoffs + nx + j];
                        v = w * df * invr2 * 2;
                        for (k0 = 0; k0 <= nx - 1; k0++)
                        {
                            for (k1 = 0; k1 <= nx - 1; k1++)
                            {
                                if (k0 == k1)
                                {

                                    //
                                    // Compute derivative and diagonal element of the Hessian
                                    //
                                    dy[j * nx + k0] = dy[j * nx + k0] + v * (x[k0] - s.cw[itemoffs + k0]);
                                    d2y[j * nx * nx + k0 * nx + k1] = d2y[j * nx * nx + k0 * nx + k1] + w * (d2f * invr2 * invr2 * 4 * math.sqr(x[k0] - s.cw[itemoffs + k0]) + df * invr2 * 2);
                                }
                                else
                                {

                                    //
                                    // Compute offdiagonal element of the Hessian
                                    //
                                    d2y[j * nx * nx + k0 * nx + k1] = d2y[j * nx * nx + k0 * nx + k1] + w * d2f * invr2 * invr2 * 4 * (x[k0] - s.cw[itemoffs + k0]) * (x[k1] - s.cw[itemoffs + k1]);
                                }
                            }
                        }
                    }
                }
                if (needdy == 1)
                {
                    if (s.bf == 0)
                    {
                        val = Math.Exp(-arg);
                        df = -val;
                    }
                    else
                    {
                        if (s.bf == 1)
                        {
                            rbfv2basisfuncdiff2(s.bf, arg, ref val, ref df, ref d2f, _params);
                        }
                        else
                        {
                            ap.assert(false, "PartialCalcRec: integrity check failed");
                        }
                    }
                    for (j = 0; j <= ny - 1; j++)
                    {
                        y[j] = y[j] + val * s.cw[itemoffs + nx + j];
                        v = s.cw[itemoffs + nx + j] * df * invr2 * 2;
                        for (k = 0; k <= nx - 1; k++)
                        {
                            dy[j * nx + k] = dy[j * nx + k] + v * (x[k] - s.cw[itemoffs + k]);
                        }
                    }
                }
                if (needdy == 0)
                {
                    if (s.bf == 0)
                    {
                        val = Math.Exp(-arg);
                    }
                    else
                    {
                        if (s.bf == 1)
                        {
                            val = rbfv2basisfunc(s.bf, arg, _params);
                        }
                        else
                        {
                            ap.assert(false, "PartialCalcRec: integrity check failed");
                        }
                    }
                    for (j = 0; j <= ny - 1; j++)
                    {
                        y[j] = y[j] + val * s.cw[itemoffs + nx + j];
                    }
                }
            }
            return;
        }

        //
        // Simple split
        //
        if (s.kdnodes[rootidx] == 0)
        {

            //
            // Load:
            // * D      dimension to split
            // * Split  split position
            // * ChildLE, ChildGE - indexes of childs
            //
            d = s.kdnodes[rootidx + 1];
            split = s.kdsplits[s.kdnodes[rootidx + 2]];
            childle = s.kdnodes[rootidx + 3];
            childge = s.kdnodes[rootidx + 4];

            //
            // Navigate through childs
            //
            for (i = 0; i <= 1; i++)
            {

                //
                // Select child to process:
                // * ChildOffs      current child offset in Nodes[]
                // * UpdateMin      whether minimum or maximum value
                //                  of bounding box is changed on update
                //
                updatemin = i != 0;
                if (i == 0)
                {
                    childoffs = childle;
                }
                else
                {
                    childoffs = childge;
                }

                //
                // Update bounding box and current distance
                //
                prevdist2 = buf.curdist2;
                t1 = x[d];
                if (updatemin)
                {
                    v = buf.curboxmin[d];
                    if (t1 <= split)
                    {
                        v0 = v - t1;
                        if (v0 < 0)
                        {
                            v0 = 0;
                        }
                        v1 = split - t1;
                        buf.curdist2 = buf.curdist2 - v0 * v0 + v1 * v1;
                    }
                    buf.curboxmin[d] = split;
                }
                else
                {
                    v = buf.curboxmax[d];
                    if (t1 >= split)
                    {
                        v0 = t1 - v;
                        if (v0 < 0)
                        {
                            v0 = 0;
                        }
                        v1 = t1 - split;
                        buf.curdist2 = buf.curdist2 - v0 * v0 + v1 * v1;
                    }
                    buf.curboxmax[d] = split;
                }

                //
                // Decide: to dive into cell or not to dive
                //
                if (buf.curdist2 < queryr2)
                {
                    partialcalcrec(s, buf, childoffs, invr2, queryr2, x, y, dy, d2y, needdy, _params);
                }

                //
                // Restore bounding box and distance
                //
                if (updatemin)
                {
                    buf.curboxmin[d] = v;
                }
                else
                {
                    buf.curboxmax[d] = v;
                }
                buf.curdist2 = prevdist2;
            }
            return;
        }

        //
        // Integrity failure
        //
        ap.assert(false, "PartialCalcRec: integrity check failed");
    }


    /*************************************************************************
    This function performs same operation as partialcalcrec(), but  for entire
    row of the grid. "Row" is a set of nodes (x0,x1,x2,x3) which share x1..x3,
    but have different x0's. (note: for 2D/3D problems x2..x3 are zero).

    Row is given by:
    * central point XC, which is located at the center of the row, and used to
      perform kd-tree requests
    * set of x0 coordinates stored in RX array (array may be unordered, but it
      is expected that spread of x0  is  no  more  than  R;  function  may  be
      inefficient for larger spreads).
    * set of YFlag values stored in RF

    INPUT PARAMETERS:
        S       -   V2 model
        Buf     -   calc-buffer, this function uses following fields:
                    * Buf.CurBoxMin - should be set by caller
                    * Buf.CurBoxMax - should be set by caller
                    * Buf.CurDist2  - squared distance from X to current bounding box,
                      should be set by caller
        RootIdx -   offset of partial kd-tree
        InvR2   -   1/R^2, where R is basis function radius
        RQuery2 -   squared query radius, usually it is (R*FarRadius(BasisFunction)+0.5*RowWidth)^2,
                    where RowWidth is its spatial  extent  (after  scaling  of
                    variables). This radius is used to perform  initial  query
                    for neighbors of CX.
        RFar2   -   squared far radius; far radius is used to perform actual
                    filtering of results of query made with RQuery2.
        CX      -   central point, array[NX], used for queries
        RX      -   x0 coordinates, array[RowSize]
        RF      -   sparsity flags, array[RowSize]
        RowSize -   row size in elements
        RY      -   input partial value, array[NY]
        
    OUTPUT PARAMETERS
        RY      -   updated partial value (function adds its results to RY)

      -- ALGLIB --
         Copyright 20.06.2016 by Bochkanov Sergey
    *************************************************************************/
    private static void partialrowcalcrec(rbfv2model s,
        rbfv2calcbuffer buf,
        int rootidx,
        double invr2,
        double rquery2,
        double rfar2,
        double[] cx,
        double[] rx,
        bool[] rf,
        int rowsize,
        double[] ry,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int i0 = 0;
        int i1 = 0;
        double partialptdist2 = 0;
        double ptdist2 = 0;
        double v = 0;
        double v0 = 0;
        double v1 = 0;
        int cwoffs = 0;
        int cwcnt = 0;
        int itemoffs = 0;
        int woffs = 0;
        double val = 0;
        int d = 0;
        double split = 0;
        int childle = 0;
        int childge = 0;
        int childoffs = 0;
        bool updatemin = new bool();
        double prevdist2 = 0;
        double t1 = 0;
        int nx = 0;
        int ny = 0;

        nx = s.nx;
        ny = s.ny;

        //
        // Leaf node.
        //
        if (s.kdnodes[rootidx] > 0)
        {
            cwcnt = s.kdnodes[rootidx + 0];
            cwoffs = s.kdnodes[rootidx + 1];
            for (i0 = 0; i0 <= cwcnt - 1; i0++)
            {

                //
                // Calculate partial distance (components from 1 to NX-1)
                //
                itemoffs = cwoffs + i0 * (nx + ny);
                partialptdist2 = 0;
                for (j = 1; j <= nx - 1; j++)
                {
                    v = s.cw[itemoffs + j] - cx[j];
                    partialptdist2 = partialptdist2 + v * v;
                }

                //
                // Process each element of the row
                //
                for (i1 = 0; i1 <= rowsize - 1; i1++)
                {
                    if (rf[i1])
                    {

                        //
                        // Calculate distance
                        //
                        v = s.cw[itemoffs] - rx[i1];
                        ptdist2 = partialptdist2 + v * v;

                        //
                        // Skip points if distance too large
                        //
                        if (ptdist2 >= rfar2)
                        {
                            continue;
                        }

                        //
                        // Update Y
                        //
                        val = rbfv2basisfunc(s.bf, ptdist2 * invr2, _params);
                        woffs = itemoffs + nx;
                        for (j = 0; j <= ny - 1; j++)
                        {
                            ry[j + i1 * ny] = ry[j + i1 * ny] + val * s.cw[woffs + j];
                        }
                    }
                }
            }
            return;
        }

        //
        // Simple split
        //
        if (s.kdnodes[rootidx] == 0)
        {

            //
            // Load:
            // * D      dimension to split
            // * Split  split position
            // * ChildLE, ChildGE - indexes of childs
            //
            d = s.kdnodes[rootidx + 1];
            split = s.kdsplits[s.kdnodes[rootidx + 2]];
            childle = s.kdnodes[rootidx + 3];
            childge = s.kdnodes[rootidx + 4];

            //
            // Navigate through childs
            //
            for (i = 0; i <= 1; i++)
            {

                //
                // Select child to process:
                // * ChildOffs      current child offset in Nodes[]
                // * UpdateMin      whether minimum or maximum value
                //                  of bounding box is changed on update
                //
                updatemin = i != 0;
                if (i == 0)
                {
                    childoffs = childle;
                }
                else
                {
                    childoffs = childge;
                }

                //
                // Update bounding box and current distance
                //
                prevdist2 = buf.curdist2;
                t1 = cx[d];
                if (updatemin)
                {
                    v = buf.curboxmin[d];
                    if (t1 <= split)
                    {
                        v0 = v - t1;
                        if (v0 < 0)
                        {
                            v0 = 0;
                        }
                        v1 = split - t1;
                        buf.curdist2 = buf.curdist2 - v0 * v0 + v1 * v1;
                    }
                    buf.curboxmin[d] = split;
                }
                else
                {
                    v = buf.curboxmax[d];
                    if (t1 >= split)
                    {
                        v0 = t1 - v;
                        if (v0 < 0)
                        {
                            v0 = 0;
                        }
                        v1 = t1 - split;
                        buf.curdist2 = buf.curdist2 - v0 * v0 + v1 * v1;
                    }
                    buf.curboxmax[d] = split;
                }

                //
                // Decide: to dive into cell or not to dive
                //
                if (buf.curdist2 < rquery2)
                {
                    partialrowcalcrec(s, buf, childoffs, invr2, rquery2, rfar2, cx, rx, rf, rowsize, ry, _params);
                }

                //
                // Restore bounding box and distance
                //
                if (updatemin)
                {
                    buf.curboxmin[d] = v;
                }
                else
                {
                    buf.curboxmax[d] = v;
                }
                buf.curdist2 = prevdist2;
            }
            return;
        }

        //
        // Integrity failure
        //
        ap.assert(false, "PartialCalcRec: integrity check failed");
    }


    /*************************************************************************
    This function prepares partial query

    INPUT PARAMETERS:
        X       -   query point
        kdBoxMin, kdBoxMax - current bounding box
        NX      -   problem size
        Buf     -   preallocated buffer; this function just loads data, but
                    does not allocate place for them.
        Cnt     -   counter variable which is set to zery by this function, as
                    convenience, and to remember about necessity to zero counter
                    prior to calling partialqueryrec().
        
    OUTPUT PARAMETERS
        Buf     -   calc-buffer:
                    * Buf.CurBoxMin - current box
                    * Buf.CurBoxMax - current box
                    * Buf.CurDist2  - squared distance from X to current box
        Cnt     -   set to zero

      -- ALGLIB --
         Copyright 20.06.2016 by Bochkanov Sergey
    *************************************************************************/
    private static void preparepartialquery(double[] x,
        double[] kdboxmin,
        double[] kdboxmax,
        int nx,
        rbfv2calcbuffer buf,
        ref int cnt,
        xparams _params)
    {
        int j = 0;

        cnt = 0;
        buf.curdist2 = 0;
        for (j = 0; j <= nx - 1; j++)
        {
            buf.curboxmin[j] = kdboxmin[j];
            buf.curboxmax[j] = kdboxmax[j];
            if ((double)(x[j]) < (double)(buf.curboxmin[j]))
            {
                buf.curdist2 = buf.curdist2 + math.sqr(buf.curboxmin[j] - x[j]);
            }
            else
            {
                if ((double)(x[j]) > (double)(buf.curboxmax[j]))
                {
                    buf.curdist2 = buf.curdist2 + math.sqr(x[j] - buf.curboxmax[j]);
                }
            }
        }
    }


    /*************************************************************************
    This function performs partial (for just one subtree of multi-tree)  query
    for neighbors located in R-sphere around X. It returns  squared  distances
    from X to points and offsets in S.CW[] array for points being found.

    INPUT PARAMETERS:
        kdNodes, kdSplits, CW, NX, NY - corresponding fields of V2 model
        Buf     -   calc-buffer, this function uses following fields:
                    * Buf.CurBoxMin - should be set by caller
                    * Buf.CurBoxMax - should be set by caller
                    * Buf.CurDist2  - squared distance from X to current
                      bounding box, should be set by caller
                    You may use preparepartialquery() function to initialize
                    these fields.
        RootIdx -   offset of partial kd-tree
        QueryR2 -   squared query radius
        X       -   array[NX], point being queried
        R2      -   preallocated output buffer; it is caller's responsibility
                    to make sure that R2 has enough space.
        Offs    -   preallocated output buffer; it is caller's responsibility
                    to make sure that Offs has enough space.
        K       -   MUST BE ZERO ON INITIAL CALL. This variable is incremented,
                    not set. So, any no-zero value will result in the incorrect
                    points count being returned.
        
    OUTPUT PARAMETERS
        R2      -   squared distances in first K elements
        Offs    -   offsets in S.CW in first K elements
        K       -   points count

      -- ALGLIB --
         Copyright 20.06.2016 by Bochkanov Sergey
    *************************************************************************/
    private static void partialqueryrec(int[] kdnodes,
        double[] kdsplits,
        double[] cw,
        int nx,
        int ny,
        rbfv2calcbuffer buf,
        int rootidx,
        double queryr2,
        double[] x,
        double[] r2,
        int[] offs,
        ref int k,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double ptdist2 = 0;
        double v = 0;
        int cwoffs = 0;
        int cwcnt = 0;
        int itemoffs = 0;
        int d = 0;
        double split = 0;
        int childle = 0;
        int childge = 0;
        int childoffs = 0;
        bool updatemin = new bool();
        double prevdist2 = 0;
        double t1 = 0;


        //
        // Leaf node.
        //
        if (kdnodes[rootidx] > 0)
        {
            cwcnt = kdnodes[rootidx + 0];
            cwoffs = kdnodes[rootidx + 1];
            for (i = 0; i <= cwcnt - 1; i++)
            {

                //
                // Calculate distance
                //
                itemoffs = cwoffs + i * (nx + ny);
                ptdist2 = 0;
                for (j = 0; j <= nx - 1; j++)
                {
                    v = cw[itemoffs + j] - x[j];
                    ptdist2 = ptdist2 + v * v;
                }

                //
                // Skip points if distance too large
                //
                if ((double)(ptdist2) >= (double)(queryr2))
                {
                    continue;
                }

                //
                // Output
                //
                r2[k] = ptdist2;
                offs[k] = itemoffs;
                k = k + 1;
            }
            return;
        }

        //
        // Simple split
        //
        if (kdnodes[rootidx] == 0)
        {

            //
            // Load:
            // * D      dimension to split
            // * Split  split position
            // * ChildLE, ChildGE - indexes of childs
            //
            d = kdnodes[rootidx + 1];
            split = kdsplits[kdnodes[rootidx + 2]];
            childle = kdnodes[rootidx + 3];
            childge = kdnodes[rootidx + 4];

            //
            // Navigate through childs
            //
            for (i = 0; i <= 1; i++)
            {

                //
                // Select child to process:
                // * ChildOffs      current child offset in Nodes[]
                // * UpdateMin      whether minimum or maximum value
                //                  of bounding box is changed on update
                //
                updatemin = i != 0;
                if (i == 0)
                {
                    childoffs = childle;
                }
                else
                {
                    childoffs = childge;
                }

                //
                // Update bounding box and current distance
                //
                prevdist2 = buf.curdist2;
                t1 = x[d];
                if (updatemin)
                {
                    v = buf.curboxmin[d];
                    if ((double)(t1) <= (double)(split))
                    {
                        buf.curdist2 = buf.curdist2 - math.sqr(Math.Max(v - t1, 0)) + math.sqr(split - t1);
                    }
                    buf.curboxmin[d] = split;
                }
                else
                {
                    v = buf.curboxmax[d];
                    if ((double)(t1) >= (double)(split))
                    {
                        buf.curdist2 = buf.curdist2 - math.sqr(Math.Max(t1 - v, 0)) + math.sqr(t1 - split);
                    }
                    buf.curboxmax[d] = split;
                }

                //
                // Decide: to dive into cell or not to dive
                //
                if ((double)(buf.curdist2) < (double)(queryr2))
                {
                    partialqueryrec(kdnodes, kdsplits, cw, nx, ny, buf, childoffs, queryr2, x, r2, offs, ref k, _params);
                }

                //
                // Restore bounding box and distance
                //
                if (updatemin)
                {
                    buf.curboxmin[d] = v;
                }
                else
                {
                    buf.curboxmax[d] = v;
                }
                buf.curdist2 = prevdist2;
            }
            return;
        }

        //
        // Integrity failure
        //
        ap.assert(false, "PartialQueryRec: integrity check failed");
    }


    /*************************************************************************
    This function performs  partial  (for  just  one  subtree  of  multi-tree)
    counting of neighbors located in R-sphere around X.

    This function does not guarantee consistency of results with other partial
    queries, it should be used only to get approximate estimates (well, we  do
    not  use   approximate   algorithms,  but  rounding  errors  may  give  us
    inconsistent results in just-at-the-boundary cases).

    INPUT PARAMETERS:
        kdNodes, kdSplits, CW, NX, NY - corresponding fields of V2 model
        Buf     -   calc-buffer, this function uses following fields:
                    * Buf.CurBoxMin - should be set by caller
                    * Buf.CurBoxMax - should be set by caller
                    * Buf.CurDist2  - squared distance from X to current
                      bounding box, should be set by caller
                    You may use preparepartialquery() function to initialize
                    these fields.
        RootIdx -   offset of partial kd-tree
        QueryR2 -   squared query radius
        X       -   array[NX], point being queried
        
    RESULT:
        points count

      -- ALGLIB --
         Copyright 20.06.2016 by Bochkanov Sergey
    *************************************************************************/
    private static int partialcountrec(int[] kdnodes,
        double[] kdsplits,
        double[] cw,
        int nx,
        int ny,
        rbfv2calcbuffer buf,
        int rootidx,
        double queryr2,
        double[] x,
        xparams _params)
    {
        int result = 0;
        int i = 0;
        int j = 0;
        double ptdist2 = 0;
        double v = 0;
        int cwoffs = 0;
        int cwcnt = 0;
        int itemoffs = 0;
        int d = 0;
        double split = 0;
        int childle = 0;
        int childge = 0;
        int childoffs = 0;
        bool updatemin = new bool();
        double prevdist2 = 0;
        double t1 = 0;

        result = 0;

        //
        // Leaf node.
        //
        if (kdnodes[rootidx] > 0)
        {
            cwcnt = kdnodes[rootidx + 0];
            cwoffs = kdnodes[rootidx + 1];
            for (i = 0; i <= cwcnt - 1; i++)
            {

                //
                // Calculate distance
                //
                itemoffs = cwoffs + i * (nx + ny);
                ptdist2 = 0;
                for (j = 0; j <= nx - 1; j++)
                {
                    v = cw[itemoffs + j] - x[j];
                    ptdist2 = ptdist2 + v * v;
                }

                //
                // Skip points if distance too large
                //
                if ((double)(ptdist2) >= (double)(queryr2))
                {
                    continue;
                }

                //
                // Output
                //
                result = result + 1;
            }
            return result;
        }

        //
        // Simple split
        //
        if (kdnodes[rootidx] == 0)
        {

            //
            // Load:
            // * D      dimension to split
            // * Split  split position
            // * ChildLE, ChildGE - indexes of childs
            //
            d = kdnodes[rootidx + 1];
            split = kdsplits[kdnodes[rootidx + 2]];
            childle = kdnodes[rootidx + 3];
            childge = kdnodes[rootidx + 4];

            //
            // Navigate through childs
            //
            for (i = 0; i <= 1; i++)
            {

                //
                // Select child to process:
                // * ChildOffs      current child offset in Nodes[]
                // * UpdateMin      whether minimum or maximum value
                //                  of bounding box is changed on update
                //
                updatemin = i != 0;
                if (i == 0)
                {
                    childoffs = childle;
                }
                else
                {
                    childoffs = childge;
                }

                //
                // Update bounding box and current distance
                //
                prevdist2 = buf.curdist2;
                t1 = x[d];
                if (updatemin)
                {
                    v = buf.curboxmin[d];
                    if ((double)(t1) <= (double)(split))
                    {
                        buf.curdist2 = buf.curdist2 - math.sqr(Math.Max(v - t1, 0)) + math.sqr(split - t1);
                    }
                    buf.curboxmin[d] = split;
                }
                else
                {
                    v = buf.curboxmax[d];
                    if ((double)(t1) >= (double)(split))
                    {
                        buf.curdist2 = buf.curdist2 - math.sqr(Math.Max(t1 - v, 0)) + math.sqr(t1 - split);
                    }
                    buf.curboxmax[d] = split;
                }

                //
                // Decide: to dive into cell or not to dive
                //
                if ((double)(buf.curdist2) < (double)(queryr2))
                {
                    result = result + partialcountrec(kdnodes, kdsplits, cw, nx, ny, buf, childoffs, queryr2, x, _params);
                }

                //
                // Restore bounding box and distance
                //
                if (updatemin)
                {
                    buf.curboxmin[d] = v;
                }
                else
                {
                    buf.curboxmax[d] = v;
                }
                buf.curdist2 = prevdist2;
            }
            return result;
        }

        //
        // Integrity failure
        //
        ap.assert(false, "PartialCountRec: integrity check failed");
        return result;
    }


    /*************************************************************************
    This function performs partial (for just one subtree of multi-tree) unpack
    for RBF model. It appends center coordinates,  weights  and  per-dimension
    radii (according to current scaling) to preallocated output array.

    INPUT PARAMETERS:
        kdNodes, kdSplits, CW, S, NX, NY - corresponding fields of V2 model
        RootIdx -   offset of partial kd-tree
        R       -   radius for current partial tree
        XWR     -   preallocated output buffer; it is caller's responsibility
                    to make sure that XWR has enough space. First K rows are
                    already occupied.
        K       -   number of already occupied rows in XWR.
        
    OUTPUT PARAMETERS
        XWR     -   updated XWR
        K       -   updated rows count

      -- ALGLIB --
         Copyright 20.06.2016 by Bochkanov Sergey
    *************************************************************************/
    private static void partialunpackrec(int[] kdnodes,
        double[] kdsplits,
        double[] cw,
        double[] s,
        int nx,
        int ny,
        int rootidx,
        double r,
        double[,] xwr,
        ref int k,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int childle = 0;
        int childge = 0;
        int itemoffs = 0;
        int cwoffs = 0;
        int cwcnt = 0;


        //
        // Leaf node.
        //
        if (kdnodes[rootidx] > 0)
        {
            cwcnt = kdnodes[rootidx + 0];
            cwoffs = kdnodes[rootidx + 1];
            for (i = 0; i <= cwcnt - 1; i++)
            {
                itemoffs = cwoffs + i * (nx + ny);
                for (j = 0; j <= nx + ny - 1; j++)
                {
                    xwr[k, j] = cw[itemoffs + j];
                }
                for (j = 0; j <= nx - 1; j++)
                {
                    xwr[k, j] = xwr[k, j] * s[j];
                }
                for (j = 0; j <= nx - 1; j++)
                {
                    xwr[k, nx + ny + j] = r * s[j];
                }
                k = k + 1;
            }
            return;
        }

        //
        // Simple split
        //
        if (kdnodes[rootidx] == 0)
        {

            //
            // Load:
            // * ChildLE, ChildGE - indexes of childs
            //
            childle = kdnodes[rootidx + 3];
            childge = kdnodes[rootidx + 4];

            //
            // Process both parts of split
            //
            partialunpackrec(kdnodes, kdsplits, cw, s, nx, ny, childle, r, xwr, ref k, _params);
            partialunpackrec(kdnodes, kdsplits, cw, s, nx, ny, childge, r, xwr, ref k, _params);
            return;
        }

        //
        // Integrity failure
        //
        ap.assert(false, "PartialUnpackRec: integrity check failed");
    }


    /*************************************************************************
    This function returns size of design matrix row for evaluation point X0,
    given:
    * query radius multiplier (either RBFV2NearRadius() or RBFV2FarRadius())
    * hierarchy level: value in [0,NH) for single-level model, or negative
      value for multilevel model (all levels of hierarchy in single matrix,
      like one used by nonnegative RBF)

    INPUT PARAMETERS:
        kdNodes, kdSplits, CW, Ri, kdRoots, kdBoxMin, kdBoxMax, NX, NY, NH - corresponding fields of V2 model
        Level   -   value in [0,NH) for single-level design matrix, negative
                    value for multilevel design matrix
        RCoeff  -   radius coefficient, either RBFV2NearRadius() or RBFV2FarRadius()
        X0      -   query point
        CalcBuf -   buffer for PreparePartialQuery(), allocated by caller
        
    RESULT:
        row size

      -- ALGLIB --
         Copyright 28.09.2016 by Bochkanov Sergey
    *************************************************************************/
    private static int designmatrixrowsize(int[] kdnodes,
        double[] kdsplits,
        double[] cw,
        double[] ri,
        int[] kdroots,
        double[] kdboxmin,
        double[] kdboxmax,
        int nx,
        int ny,
        int nh,
        int level,
        double rcoeff,
        double[] x0,
        rbfv2calcbuffer calcbuf,
        xparams _params)
    {
        int result = 0;
        int dummy = 0;
        int levelidx = 0;
        int level0 = 0;
        int level1 = 0;
        double curradius2 = 0;

        ap.assert(nh > 0, "DesignMatrixRowSize: integrity failure");
        if (level >= 0)
        {
            level0 = level;
            level1 = level;
        }
        else
        {
            level0 = 0;
            level1 = nh - 1;
        }
        result = 0;
        for (levelidx = level0; levelidx <= level1; levelidx++)
        {
            curradius2 = math.sqr(ri[levelidx] * rcoeff);
            preparepartialquery(x0, kdboxmin, kdboxmax, nx, calcbuf, ref dummy, _params);
            result = result + partialcountrec(kdnodes, kdsplits, cw, nx, ny, calcbuf, kdroots[levelidx], curradius2, x0, _params);
        }
        return result;
    }


    /*************************************************************************
    This function generates design matrix row for evaluation point X0, given:
    * query radius multiplier (either RBFV2NearRadius() or RBFV2FarRadius())
    * hierarchy level: value in [0,NH) for single-level model, or negative
      value for multilevel model (all levels of hierarchy in single matrix,
      like one used by nonnegative RBF)

    INPUT PARAMETERS:
        kdNodes, kdSplits, CW, Ri, kdRoots, kdBoxMin, kdBoxMax, NX, NY, NH - corresponding fields of V2 model

        CWRange -   internal array[NH+1] used by RBF construction function,
                    stores ranges of CW occupied by NH trees.
        Level   -   value in [0,NH) for single-level design matrix, negative
                    value for multilevel design matrix
        BF      -   basis function type
        RCoeff  -   radius coefficient, either RBFV2NearRadius() or RBFV2FarRadius()
        RowsPerPoint-equal to:
                    * 1 for unpenalized regression model
                    * 1+NX for basic form of nonsmoothness penalty
        Penalty -   nonsmoothness penalty coefficient
        
        X0      -   query point
        
        CalcBuf -   buffer for PreparePartialQuery(), allocated by caller
        R2      -   preallocated temporary buffer, size is at least NPoints;
                    it is caller's responsibility to make sure that R2 has enough space.
        Offs    -   preallocated temporary buffer; size is at least NPoints;
                    it is caller's responsibility to make sure that Offs has enough space.
        K       -   MUST BE ZERO ON INITIAL CALL. This variable is incremented,
                    not set. So, any no-zero value will result in the incorrect
                    points count being returned.
        RowIdx  -   preallocated array, at least RowSize elements
        RowVal  -   preallocated array, at least RowSize*RowsPerPoint elements
        
    RESULT:
        RowIdx  -   RowSize elements are filled with column indexes of non-zero
                    design matrix entries
        RowVal  -   RowSize*RowsPerPoint elements are filled with design matrix
                    values, with column RowIdx[0] being stored in first RowsPerPoint
                    elements of RowVal, column RowIdx[1] being stored in next
                    RowsPerPoint elements, and so on.
                    
                    First element in contiguous set of RowsPerPoint elements
                    corresponds to 
                    
        RowSize -   number of columns per row

      -- ALGLIB --
         Copyright 28.09.2016 by Bochkanov Sergey
    *************************************************************************/
    private static void designmatrixgeneraterow(int[] kdnodes,
        double[] kdsplits,
        double[] cw,
        double[] ri,
        int[] kdroots,
        double[] kdboxmin,
        double[] kdboxmax,
        int[] cwrange,
        int nx,
        int ny,
        int nh,
        int level,
        int bf,
        double rcoeff,
        int rowsperpoint,
        double penalty,
        double[] x0,
        rbfv2calcbuffer calcbuf,
        double[] tmpr2,
        int[] tmpoffs,
        int[] rowidx,
        double[] rowval,
        ref int rowsize,
        xparams _params)
    {
        int j = 0;
        int k = 0;
        int cnt = 0;
        int levelidx = 0;
        int level0 = 0;
        int level1 = 0;
        double invri2 = 0;
        double curradius2 = 0;
        double val = 0;
        double dval = 0;
        double d2val = 0;

        rowsize = 0;

        ap.assert(nh > 0, "DesignMatrixGenerateRow: integrity failure (a)");
        ap.assert(rowsperpoint == 1 || rowsperpoint == 1 + nx, "DesignMatrixGenerateRow: integrity failure (b)");
        if (level >= 0)
        {
            level0 = level;
            level1 = level;
        }
        else
        {
            level0 = 0;
            level1 = nh - 1;
        }
        rowsize = 0;
        for (levelidx = level0; levelidx <= level1; levelidx++)
        {
            curradius2 = math.sqr(ri[levelidx] * rcoeff);
            invri2 = 1 / math.sqr(ri[levelidx]);
            preparepartialquery(x0, kdboxmin, kdboxmax, nx, calcbuf, ref cnt, _params);
            partialqueryrec(kdnodes, kdsplits, cw, nx, ny, calcbuf, kdroots[levelidx], curradius2, x0, tmpr2, tmpoffs, ref cnt, _params);
            ap.assert(ap.len(tmpr2) >= cnt, "DesignMatrixRowSize: integrity failure (c)");
            ap.assert(ap.len(tmpoffs) >= cnt, "DesignMatrixRowSize: integrity failure (d)");
            ap.assert(ap.len(rowidx) >= rowsize + cnt, "DesignMatrixRowSize: integrity failure (e)");
            ap.assert(ap.len(rowval) >= rowsperpoint * (rowsize + cnt), "DesignMatrixRowSize: integrity failure (f)");
            for (j = 0; j <= cnt - 1; j++)
            {

                //
                // Generate element corresponding to fitting error.
                // Store derivative information which may be required later.
                //
                ap.assert((tmpoffs[j] - cwrange[level0]) % (nx + ny) == 0, "DesignMatrixRowSize: integrity failure (g)");
                rbfv2basisfuncdiff2(bf, tmpr2[j] * invri2, ref val, ref dval, ref d2val, _params);
                rowidx[rowsize + j] = (tmpoffs[j] - cwrange[level0]) / (nx + ny);
                rowval[(rowsize + j) * rowsperpoint + 0] = val;
                if (rowsperpoint == 1)
                {
                    continue;
                }

                //
                // Generate elements corresponding to nonsmoothness penalty
                //
                ap.assert(rowsperpoint == 1 + nx, "DesignMatrixRowSize: integrity failure (h)");
                for (k = 0; k <= nx - 1; k++)
                {
                    rowval[(rowsize + j) * rowsperpoint + 1 + k] = penalty * (dval * 2 * invri2 + d2val * math.sqr(2 * (x0[k] - cw[tmpoffs[j] + k]) * invri2));
                }
            }

            //
            // Update columns counter
            //
            rowsize = rowsize + cnt;
        }
    }


    /*************************************************************************
    This function fills RBF model by zeros.

      -- ALGLIB --
         Copyright 17.11.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void zerofill(rbfv2model s,
        int nx,
        int ny,
        int bf,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        s.bf = bf;
        s.nh = 0;
        s.ri = new double[0];
        s.s = new double[0];
        s.kdroots = new int[0];
        s.kdnodes = new int[0];
        s.kdsplits = new double[0];
        s.kdboxmin = new double[0];
        s.kdboxmax = new double[0];
        s.cw = new double[0];
        s.v = new double[ny, nx + 1];
        for (i = 0; i <= ny - 1; i++)
        {
            for (j = 0; j <= nx; j++)
            {
                s.v[i, j] = 0;
            }
        }
    }


}
