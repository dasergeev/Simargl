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

public class rbfv1
{
    /*************************************************************************
    Buffer object which is used to perform nearest neighbor  requests  in  the
    multithreaded mode (multiple threads working with same KD-tree object).

    This object should be created with KDTreeCreateBuffer().
    *************************************************************************/
    public class rbfv1calcbuffer : apobject
    {
        public double[] calcbufxcx;
        public double[,] calcbufx;
        public int[] calcbuftags;
        public nearestneighbor.kdtreerequestbuffer requestbuffer;
        public rbfv1calcbuffer()
        {
            init();
        }
        public override void init()
        {
            calcbufxcx = new double[0];
            calcbufx = new double[0, 0];
            calcbuftags = new int[0];
            requestbuffer = new nearestneighbor.kdtreerequestbuffer();
        }
        public override apobject make_copy()
        {
            rbfv1calcbuffer _result = new rbfv1calcbuffer();
            _result.calcbufxcx = (double[])calcbufxcx.Clone();
            _result.calcbufx = (double[,])calcbufx.Clone();
            _result.calcbuftags = (int[])calcbuftags.Clone();
            _result.requestbuffer = (nearestneighbor.kdtreerequestbuffer)requestbuffer.make_copy();
            return _result;
        }
    };


    /*************************************************************************
    RBF model.

    Never try to directly work with fields of this object - always use  ALGLIB
    functions to use this object.
    *************************************************************************/
    public class rbfv1model : apobject
    {
        public int ny;
        public int nx;
        public int nc;
        public int nl;
        public nearestneighbor.kdtree tree;
        public double[,] xc;
        public double[,] wr;
        public double rmax;
        public double[,] v;
        public double[] calcbufxcx;
        public double[,] calcbufx;
        public int[] calcbuftags;
        public rbfv1model()
        {
            init();
        }
        public override void init()
        {
            tree = new nearestneighbor.kdtree();
            xc = new double[0, 0];
            wr = new double[0, 0];
            v = new double[0, 0];
            calcbufxcx = new double[0];
            calcbufx = new double[0, 0];
            calcbuftags = new int[0];
        }
        public override apobject make_copy()
        {
            rbfv1model _result = new rbfv1model();
            _result.ny = ny;
            _result.nx = nx;
            _result.nc = nc;
            _result.nl = nl;
            _result.tree = (nearestneighbor.kdtree)tree.make_copy();
            _result.xc = (double[,])xc.Clone();
            _result.wr = (double[,])wr.Clone();
            _result.rmax = rmax;
            _result.v = (double[,])v.Clone();
            _result.calcbufxcx = (double[])calcbufxcx.Clone();
            _result.calcbufx = (double[,])calcbufx.Clone();
            _result.calcbuftags = (int[])calcbuftags.Clone();
            return _result;
        }
    };


    /*************************************************************************
    Internal buffer for GridCalc3
    *************************************************************************/
    public class gridcalc3v1buf : apobject
    {
        public double[] tx;
        public double[] cx;
        public double[] ty;
        public bool[] flag0;
        public bool[] flag1;
        public bool[] flag2;
        public bool[] flag12;
        public double[] expbuf0;
        public double[] expbuf1;
        public double[] expbuf2;
        public nearestneighbor.kdtreerequestbuffer requestbuf;
        public double[,] calcbufx;
        public int[] calcbuftags;
        public gridcalc3v1buf()
        {
            init();
        }
        public override void init()
        {
            tx = new double[0];
            cx = new double[0];
            ty = new double[0];
            flag0 = new bool[0];
            flag1 = new bool[0];
            flag2 = new bool[0];
            flag12 = new bool[0];
            expbuf0 = new double[0];
            expbuf1 = new double[0];
            expbuf2 = new double[0];
            requestbuf = new nearestneighbor.kdtreerequestbuffer();
            calcbufx = new double[0, 0];
            calcbuftags = new int[0];
        }
        public override apobject make_copy()
        {
            gridcalc3v1buf _result = new gridcalc3v1buf();
            _result.tx = (double[])tx.Clone();
            _result.cx = (double[])cx.Clone();
            _result.ty = (double[])ty.Clone();
            _result.flag0 = (bool[])flag0.Clone();
            _result.flag1 = (bool[])flag1.Clone();
            _result.flag2 = (bool[])flag2.Clone();
            _result.flag12 = (bool[])flag12.Clone();
            _result.expbuf0 = (double[])expbuf0.Clone();
            _result.expbuf1 = (double[])expbuf1.Clone();
            _result.expbuf2 = (double[])expbuf2.Clone();
            _result.requestbuf = (nearestneighbor.kdtreerequestbuffer)requestbuf.make_copy();
            _result.calcbufx = (double[,])calcbufx.Clone();
            _result.calcbuftags = (int[])calcbuftags.Clone();
            return _result;
        }
    };


    /*************************************************************************
    RBF solution report:
    * TerminationType   -   termination type, positive values - success,
                            non-positive - failure.
    *************************************************************************/
    public class rbfv1report : apobject
    {
        public int arows;
        public int acols;
        public int annz;
        public int iterationscount;
        public int nmv;
        public int terminationtype;
        public rbfv1report()
        {
            init();
        }
        public override void init()
        {
        }
        public override apobject make_copy()
        {
            rbfv1report _result = new rbfv1report();
            _result.arows = arows;
            _result.acols = acols;
            _result.annz = annz;
            _result.iterationscount = iterationscount;
            _result.nmv = nmv;
            _result.terminationtype = terminationtype;
            return _result;
        }
    };




    public const int mxnx = 3;
    public const double rbffarradius = 6;
    public const double rbfnearradius = 2.1;
    public const double rbfmlradius = 3;
    public const double minbasecasecost = 100000;


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
    public static void rbfv1create(int nx,
        int ny,
        rbfv1model s,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        ap.assert(nx == 2 || nx == 3, "RBFCreate: NX<>2 and NX<>3");
        ap.assert(ny >= 1, "RBFCreate: NY<1");
        s.nx = nx;
        s.ny = ny;
        s.nl = 0;
        s.nc = 0;
        s.v = new double[ny, mxnx + 1];
        for (i = 0; i <= ny - 1; i++)
        {
            for (j = 0; j <= mxnx; j++)
            {
                s.v[i, j] = 0;
            }
        }
        s.rmax = 0;
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
    public static void rbfv1createcalcbuffer(rbfv1model s,
        rbfv1calcbuffer buf,
        xparams _params)
    {
        nearestneighbor.kdtreecreaterequestbuffer(s.tree, buf.requestbuffer, _params);
    }


    /*************************************************************************
    This   function  builds  RBF  model  and  returns  report  (contains  some 
    information which can be used for evaluation of the algorithm properties).

    Call to this function modifies RBF model by calculating its centers/radii/
    weights  and  saving  them  into  RBFModel  structure.  Initially RBFModel 
    contain zero coefficients, but after call to this function  we  will  have
    coefficients which were calculated in order to fit our dataset.

    After you called this function you can call RBFCalc(),  RBFGridCalc()  and
    other model calculation functions.

    INPUT PARAMETERS:
        S       -   RBF model, initialized by RBFCreate() call
        Rep     -   report:
                    * Rep.TerminationType:
                      * -5 - non-distinct basis function centers were detected,
                             interpolation aborted
                      * -4 - nonconvergence of the internal SVD solver
                      *  1 - successful termination
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
         Copyright 13.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv1buildmodel(double[,] x,
        double[,] y,
        int n,
        int aterm,
        int algorithmtype,
        int nlayers,
        double radvalue,
        double radzvalue,
        double lambdav,
        double epsort,
        double epserr,
        int maxits,
        rbfv1model s,
        rbfv1report rep,
        xparams _params)
    {
        nearestneighbor.kdtree tree = new nearestneighbor.kdtree();
        nearestneighbor.kdtree ctree = new nearestneighbor.kdtree();
        double[] dist = new double[0];
        double[] xcx = new double[0];
        double[,] a = new double[0, 0];
        double[,] v = new double[0, 0];
        double[,] omega = new double[0, 0];
        double[,] residualy = new double[0, 0];
        double[] radius = new double[0];
        double[,] xc = new double[0, 0];
        int nc = 0;
        double rmax = 0;
        int[] tags = new int[0];
        int[] ctags = new int[0];
        int i = 0;
        int j = 0;
        int k = 0;
        int snnz = 0;
        double[] tmp0 = new double[0];
        double[] tmp1 = new double[0];
        int layerscnt = 0;
        bool modelstatus = new bool();

        ap.assert(s.nx == 2 || s.nx == 3, "RBFBuildModel: S.NX<>2 or S.NX<>3!");

        //
        // Quick exit when we have no points
        //
        if (n == 0)
        {
            rep.terminationtype = 1;
            rep.iterationscount = 0;
            rep.nmv = 0;
            rep.arows = 0;
            rep.acols = 0;
            nearestneighbor.kdtreebuildtagged(s.xc, tags, 0, mxnx, 0, 2, s.tree, _params);
            s.xc = new double[0, 0];
            s.wr = new double[0, 0];
            s.nc = 0;
            s.rmax = 0;
            s.v = new double[s.ny, mxnx + 1];
            for (i = 0; i <= s.ny - 1; i++)
            {
                for (j = 0; j <= mxnx; j++)
                {
                    s.v[i, j] = 0;
                }
            }
            return;
        }

        //
        // General case, N>0
        //
        rep.annz = 0;
        rep.iterationscount = 0;
        rep.nmv = 0;
        xcx = new double[mxnx];

        //
        // First model in a sequence - linear model.
        // Residuals from linear regression are stored in the ResidualY variable
        // (used later to build RBF models).
        //
        residualy = new double[n, s.ny];
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= s.ny - 1; j++)
            {
                residualy[i, j] = y[i, j];
            }
        }
        if (!rbfv1buildlinearmodel(x, ref residualy, n, s.ny, aterm, ref v, _params))
        {
            rep.terminationtype = -5;
            return;
        }

        //
        // Handle special case: multilayer model with NLayers=0.
        // Quick exit.
        //
        if (algorithmtype == 2 && nlayers == 0)
        {
            rep.terminationtype = 1;
            rep.iterationscount = 0;
            rep.nmv = 0;
            rep.arows = 0;
            rep.acols = 0;
            nearestneighbor.kdtreebuildtagged(s.xc, tags, 0, mxnx, 0, 2, s.tree, _params);
            s.xc = new double[0, 0];
            s.wr = new double[0, 0];
            s.nc = 0;
            s.rmax = 0;
            s.v = new double[s.ny, mxnx + 1];
            for (i = 0; i <= s.ny - 1; i++)
            {
                for (j = 0; j <= mxnx; j++)
                {
                    s.v[i, j] = v[i, j];
                }
            }
            return;
        }

        //
        // Second model in a sequence - RBF term.
        //
        // NOTE: assignments below are not necessary, but without them
        //       MSVC complains about unitialized variables.
        //
        nc = 0;
        rmax = 0;
        layerscnt = 0;
        modelstatus = false;
        if (algorithmtype == 1)
        {

            //
            // Add RBF model.
            // This model uses local KD-trees to speed-up nearest neighbor searches.
            //
            nc = n;
            xc = new double[nc, mxnx];
            for (i = 0; i <= nc - 1; i++)
            {
                for (j = 0; j <= mxnx - 1; j++)
                {
                    xc[i, j] = x[i, j];
                }
            }
            rmax = 0;
            radius = new double[nc];
            ctags = new int[nc];
            for (i = 0; i <= nc - 1; i++)
            {
                ctags[i] = i;
            }
            nearestneighbor.kdtreebuildtagged(xc, ctags, nc, mxnx, 0, 2, ctree, _params);
            if (nc == 0)
            {
                rmax = 1;
            }
            else
            {
                if (nc == 1)
                {
                    radius[0] = radvalue;
                    rmax = radius[0];
                }
                else
                {

                    //
                    // NC>1, calculate radii using distances to nearest neigbors
                    //
                    for (i = 0; i <= nc - 1; i++)
                    {
                        for (j = 0; j <= mxnx - 1; j++)
                        {
                            xcx[j] = xc[i, j];
                        }
                        if (nearestneighbor.kdtreequeryknn(ctree, xcx, 1, false, _params) > 0)
                        {
                            nearestneighbor.kdtreequeryresultsdistances(ctree, ref dist, _params);
                            radius[i] = radvalue * dist[0];
                        }
                        else
                        {

                            //
                            // No neighbors found (it will happen when we have only one center).
                            // Initialize radius with default value.
                            //
                            radius[i] = 1.0;
                        }
                    }

                    //
                    // Apply filtering
                    //
                    apserv.rvectorsetlengthatleast(ref tmp0, nc, _params);
                    for (i = 0; i <= nc - 1; i++)
                    {
                        tmp0[i] = radius[i];
                    }
                    tsort.tagsortfast(ref tmp0, ref tmp1, nc, _params);
                    for (i = 0; i <= nc - 1; i++)
                    {
                        radius[i] = Math.Min(radius[i], radzvalue * tmp0[nc / 2]);
                    }

                    //
                    // Calculate RMax, check that all radii are non-zero
                    //
                    for (i = 0; i <= nc - 1; i++)
                    {
                        rmax = Math.Max(rmax, radius[i]);
                    }
                    for (i = 0; i <= nc - 1; i++)
                    {
                        if ((double)(radius[i]) == (double)(0))
                        {
                            rep.terminationtype = -5;
                            return;
                        }
                    }
                }
            }
            apserv.ivectorsetlengthatleast(ref tags, n, _params);
            for (i = 0; i <= n - 1; i++)
            {
                tags[i] = i;
            }
            nearestneighbor.kdtreebuildtagged(x, tags, n, mxnx, 0, 2, tree, _params);
            buildrbfmodellsqr(x, ref residualy, xc, radius, n, nc, s.ny, tree, ctree, epsort, epserr, maxits, ref rep.annz, ref snnz, ref omega, ref rep.terminationtype, ref rep.iterationscount, ref rep.nmv, _params);
            layerscnt = 1;
            modelstatus = true;
        }
        if (algorithmtype == 2)
        {
            rmax = radvalue;
            buildrbfmlayersmodellsqr(x, ref residualy, ref xc, radvalue, ref radius, n, ref nc, s.ny, nlayers, ctree, 1.0E-6, 1.0E-6, 50, lambdav, ref rep.annz, ref omega, ref rep.terminationtype, ref rep.iterationscount, ref rep.nmv, _params);
            layerscnt = nlayers;
            modelstatus = true;
        }
        ap.assert(modelstatus, "RBFBuildModel: integrity error");
        if (rep.terminationtype <= 0)
        {
            return;
        }

        //
        // Model is built
        //
        s.nc = nc / layerscnt;
        s.rmax = rmax;
        s.nl = layerscnt;
        s.xc = new double[s.nc, mxnx];
        s.wr = new double[s.nc, 1 + s.nl * s.ny];
        s.v = new double[s.ny, mxnx + 1];
        for (i = 0; i <= s.nc - 1; i++)
        {
            for (j = 0; j <= mxnx - 1; j++)
            {
                s.xc[i, j] = xc[i, j];
            }
        }
        apserv.ivectorsetlengthatleast(ref tags, s.nc, _params);
        for (i = 0; i <= s.nc - 1; i++)
        {
            tags[i] = i;
        }
        nearestneighbor.kdtreebuildtagged(s.xc, tags, s.nc, mxnx, 0, 2, s.tree, _params);
        for (i = 0; i <= s.nc - 1; i++)
        {
            s.wr[i, 0] = radius[i];
            for (k = 0; k <= layerscnt - 1; k++)
            {
                for (j = 0; j <= s.ny - 1; j++)
                {
                    s.wr[i, 1 + k * s.ny + j] = omega[k * s.nc + i, j];
                }
            }
        }
        for (i = 0; i <= s.ny - 1; i++)
        {
            for (j = 0; j <= mxnx; j++)
            {
                s.v[i, j] = v[i, j];
            }
        }
        rep.terminationtype = 1;
        rep.arows = n;
        rep.acols = s.nc;
    }


    /*************************************************************************
    Serializer: allocation

      -- ALGLIB --
         Copyright 02.02.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv1alloc(serializer s,
        rbfv1model model,
        xparams _params)
    {

        //
        // Data
        //
        s.alloc_entry();
        s.alloc_entry();
        s.alloc_entry();
        s.alloc_entry();
        nearestneighbor.kdtreealloc(s, model.tree, _params);
        apserv.allocrealmatrix(s, model.xc, -1, -1, _params);
        apserv.allocrealmatrix(s, model.wr, -1, -1, _params);
        s.alloc_entry();
        apserv.allocrealmatrix(s, model.v, -1, -1, _params);
    }


    /*************************************************************************
    Serializer: serialization

      -- ALGLIB --
         Copyright 02.02.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv1serialize(serializer s,
        rbfv1model model,
        xparams _params)
    {

        //
        // Data
        //
        s.serialize_int(model.nx);
        s.serialize_int(model.ny);
        s.serialize_int(model.nc);
        s.serialize_int(model.nl);
        nearestneighbor.kdtreeserialize(s, model.tree, _params);
        apserv.serializerealmatrix(s, model.xc, -1, -1, _params);
        apserv.serializerealmatrix(s, model.wr, -1, -1, _params);
        s.serialize_double(model.rmax);
        apserv.serializerealmatrix(s, model.v, -1, -1, _params);
    }


    /*************************************************************************
    Serializer: unserialization

      -- ALGLIB --
         Copyright 02.02.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv1unserialize(serializer s,
        rbfv1model model,
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
        rbfv1create(nx, ny, model, _params);
        model.nc = s.unserialize_int();
        model.nl = s.unserialize_int();
        nearestneighbor.kdtreeunserialize(s, model.tree, _params);
        apserv.unserializerealmatrix(s, ref model.xc, _params);
        apserv.unserializerealmatrix(s, ref model.wr, _params);
        model.rmax = s.unserialize_double();
        apserv.unserializerealmatrix(s, ref model.v, _params);
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
    public static double rbfv1calc2(rbfv1model s,
        double x0,
        double x1,
        xparams _params)
    {
        double result = 0;
        int i = 0;
        int j = 0;
        int lx = 0;
        int tg = 0;
        double d2 = 0;
        double t = 0;
        double bfcur = 0;
        double rcur = 0;

        ap.assert(math.isfinite(x0), "RBFCalc2: invalid value for X0 (X0 is Inf)!");
        ap.assert(math.isfinite(x1), "RBFCalc2: invalid value for X1 (X1 is Inf)!");
        if (s.ny != 1 || s.nx != 2)
        {
            result = 0;
            return result;
        }
        result = s.v[0, 0] * x0 + s.v[0, 1] * x1 + s.v[0, mxnx];
        if (s.nc == 0)
        {
            return result;
        }
        apserv.rvectorsetlengthatleast(ref s.calcbufxcx, mxnx, _params);
        for (i = 0; i <= mxnx - 1; i++)
        {
            s.calcbufxcx[i] = 0.0;
        }
        s.calcbufxcx[0] = x0;
        s.calcbufxcx[1] = x1;
        lx = nearestneighbor.kdtreequeryrnn(s.tree, s.calcbufxcx, s.rmax * rbffarradius, true, _params);
        nearestneighbor.kdtreequeryresultsx(s.tree, ref s.calcbufx, _params);
        nearestneighbor.kdtreequeryresultstags(s.tree, ref s.calcbuftags, _params);
        for (i = 0; i <= lx - 1; i++)
        {
            tg = s.calcbuftags[i];
            d2 = math.sqr(x0 - s.calcbufx[i, 0]) + math.sqr(x1 - s.calcbufx[i, 1]);
            rcur = s.wr[tg, 0];
            bfcur = Math.Exp(-(d2 / (rcur * rcur)));
            for (j = 0; j <= s.nl - 1; j++)
            {
                result = result + bfcur * s.wr[tg, 1 + j];
                rcur = 0.5 * rcur;
                t = bfcur * bfcur;
                bfcur = t * t;
            }
        }
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
    public static double rbfv1calc3(rbfv1model s,
        double x0,
        double x1,
        double x2,
        xparams _params)
    {
        double result = 0;
        int i = 0;
        int j = 0;
        int lx = 0;
        int tg = 0;
        double t = 0;
        double rcur = 0;
        double bf = 0;

        ap.assert(math.isfinite(x0), "RBFCalc3: invalid value for X0 (X0 is Inf or NaN)!");
        ap.assert(math.isfinite(x1), "RBFCalc3: invalid value for X1 (X1 is Inf or NaN)!");
        ap.assert(math.isfinite(x2), "RBFCalc3: invalid value for X2 (X2 is Inf or NaN)!");
        if (s.ny != 1 || s.nx != 3)
        {
            result = 0;
            return result;
        }
        result = s.v[0, 0] * x0 + s.v[0, 1] * x1 + s.v[0, 2] * x2 + s.v[0, mxnx];
        if (s.nc == 0)
        {
            return result;
        }

        //
        // calculating value for F(X)
        //
        apserv.rvectorsetlengthatleast(ref s.calcbufxcx, mxnx, _params);
        for (i = 0; i <= mxnx - 1; i++)
        {
            s.calcbufxcx[i] = 0.0;
        }
        s.calcbufxcx[0] = x0;
        s.calcbufxcx[1] = x1;
        s.calcbufxcx[2] = x2;
        lx = nearestneighbor.kdtreequeryrnn(s.tree, s.calcbufxcx, s.rmax * rbffarradius, true, _params);
        nearestneighbor.kdtreequeryresultsx(s.tree, ref s.calcbufx, _params);
        nearestneighbor.kdtreequeryresultstags(s.tree, ref s.calcbuftags, _params);
        for (i = 0; i <= lx - 1; i++)
        {
            tg = s.calcbuftags[i];
            rcur = s.wr[tg, 0];
            bf = Math.Exp(-((math.sqr(x0 - s.calcbufx[i, 0]) + math.sqr(x1 - s.calcbufx[i, 1]) + math.sqr(x2 - s.calcbufx[i, 2])) / math.sqr(rcur)));
            for (j = 0; j <= s.nl - 1; j++)
            {
                result = result + bf * s.wr[tg, 1 + j];
                t = bf * bf;
                bf = t * t;
            }
        }
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
    public static void rbfv1calcbuf(rbfv1model s,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        int lx = 0;
        int tg = 0;
        double t = 0;
        double rcur = 0;
        double bf = 0;

        ap.assert(ap.len(x) >= s.nx, "RBFCalcBuf: Length(X)<NX");
        ap.assert(apserv.isfinitevector(x, s.nx, _params), "RBFCalcBuf: X contains infinite or NaN values");
        if (ap.len(y) < s.ny)
        {
            y = new double[s.ny];
        }
        for (i = 0; i <= s.ny - 1; i++)
        {
            y[i] = s.v[i, mxnx];
            for (j = 0; j <= s.nx - 1; j++)
            {
                y[i] = y[i] + s.v[i, j] * x[j];
            }
        }
        if (s.nc == 0)
        {
            return;
        }
        apserv.rvectorsetlengthatleast(ref s.calcbufxcx, mxnx, _params);
        for (i = 0; i <= mxnx - 1; i++)
        {
            s.calcbufxcx[i] = 0.0;
        }
        for (i = 0; i <= s.nx - 1; i++)
        {
            s.calcbufxcx[i] = x[i];
        }
        lx = nearestneighbor.kdtreequeryrnn(s.tree, s.calcbufxcx, s.rmax * rbffarradius, true, _params);
        nearestneighbor.kdtreequeryresultsx(s.tree, ref s.calcbufx, _params);
        nearestneighbor.kdtreequeryresultstags(s.tree, ref s.calcbuftags, _params);
        for (i = 0; i <= s.ny - 1; i++)
        {
            for (j = 0; j <= lx - 1; j++)
            {
                tg = s.calcbuftags[j];
                rcur = s.wr[tg, 0];
                bf = Math.Exp(-((math.sqr(s.calcbufxcx[0] - s.calcbufx[j, 0]) + math.sqr(s.calcbufxcx[1] - s.calcbufx[j, 1]) + math.sqr(s.calcbufxcx[2] - s.calcbufx[j, 2])) / math.sqr(rcur)));
                for (k = 0; k <= s.nl - 1; k++)
                {
                    y[i] = y[i] + bf * s.wr[tg, 1 + k * s.ny + i];
                    t = bf * bf;
                    bf = t * t;
                }
            }
        }
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
    public static void rbfv1tscalcbuf(rbfv1model s,
        rbfv1calcbuffer buf,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        int lx = 0;
        int tg = 0;
        double t = 0;
        double rcur = 0;
        double bf = 0;

        ap.assert(ap.len(x) >= s.nx, "RBFCalcBuf: Length(X)<NX");
        ap.assert(apserv.isfinitevector(x, s.nx, _params), "RBFCalcBuf: X contains infinite or NaN values");
        if (ap.len(y) < s.ny)
        {
            y = new double[s.ny];
        }
        for (i = 0; i <= s.ny - 1; i++)
        {
            y[i] = s.v[i, mxnx];
            for (j = 0; j <= s.nx - 1; j++)
            {
                y[i] = y[i] + s.v[i, j] * x[j];
            }
        }
        if (s.nc == 0)
        {
            return;
        }
        apserv.rvectorsetlengthatleast(ref buf.calcbufxcx, mxnx, _params);
        for (i = 0; i <= mxnx - 1; i++)
        {
            buf.calcbufxcx[i] = 0.0;
        }
        for (i = 0; i <= s.nx - 1; i++)
        {
            buf.calcbufxcx[i] = x[i];
        }
        lx = nearestneighbor.kdtreetsqueryrnn(s.tree, buf.requestbuffer, buf.calcbufxcx, s.rmax * rbffarradius, true, _params);
        nearestneighbor.kdtreetsqueryresultsx(s.tree, buf.requestbuffer, ref buf.calcbufx, _params);
        nearestneighbor.kdtreetsqueryresultstags(s.tree, buf.requestbuffer, ref buf.calcbuftags, _params);
        for (i = 0; i <= s.ny - 1; i++)
        {
            for (j = 0; j <= lx - 1; j++)
            {
                tg = buf.calcbuftags[j];
                rcur = s.wr[tg, 0];
                bf = Math.Exp(-((math.sqr(buf.calcbufxcx[0] - buf.calcbufx[j, 0]) + math.sqr(buf.calcbufxcx[1] - buf.calcbufx[j, 1]) + math.sqr(buf.calcbufxcx[2] - buf.calcbufx[j, 2])) / math.sqr(rcur)));
                for (k = 0; k <= s.nl - 1; k++)
                {
                    y[i] = y[i] + bf * s.wr[tg, 1 + k * s.ny + i];
                    t = bf * bf;
                    bf = t * t;
                }
            }
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
    public static void rbfv1tsdiffbuf(rbfv1model s,
        rbfv1calcbuffer buf,
        double[] x,
        ref double[] y,
        ref double[] dy,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        int kk = 0;
        int lx = 0;
        int tg = 0;
        double t = 0;
        double rcur = 0;
        double invrcur2 = 0;
        double f = 0;
        double df = 0;
        double w = 0;

        ap.assert(ap.len(x) >= s.nx, "RBFDiffBuf: Length(X)<NX");
        ap.assert(apserv.isfinitevector(x, s.nx, _params), "RBFDiffBuf: X contains infinite or NaN values");
        if (ap.len(y) < s.ny)
        {
            y = new double[s.ny];
        }
        if (ap.len(dy) < s.ny * s.nx)
        {
            dy = new double[s.ny * s.nx];
        }
        for (i = 0; i <= s.ny - 1; i++)
        {
            y[i] = s.v[i, mxnx];
            for (j = 0; j <= s.nx - 1; j++)
            {
                y[i] = y[i] + s.v[i, j] * x[j];
                dy[i * s.nx + j] = s.v[i, j];
            }
        }
        if (s.nc == 0)
        {
            return;
        }
        apserv.rvectorsetlengthatleast(ref buf.calcbufxcx, mxnx, _params);
        for (i = 0; i <= mxnx - 1; i++)
        {
            buf.calcbufxcx[i] = 0.0;
        }
        for (i = 0; i <= s.nx - 1; i++)
        {
            buf.calcbufxcx[i] = x[i];
        }
        lx = nearestneighbor.kdtreetsqueryrnn(s.tree, buf.requestbuffer, buf.calcbufxcx, s.rmax * rbffarradius, true, _params);
        nearestneighbor.kdtreetsqueryresultsx(s.tree, buf.requestbuffer, ref buf.calcbufx, _params);
        nearestneighbor.kdtreetsqueryresultstags(s.tree, buf.requestbuffer, ref buf.calcbuftags, _params);
        for (i = 0; i <= s.ny - 1; i++)
        {
            for (j = 0; j <= lx - 1; j++)
            {
                tg = buf.calcbuftags[j];
                rcur = s.wr[tg, 0];
                invrcur2 = 1 / (rcur * rcur);
                f = Math.Exp(-((math.sqr(buf.calcbufxcx[0] - buf.calcbufx[j, 0]) + math.sqr(buf.calcbufxcx[1] - buf.calcbufx[j, 1]) + math.sqr(buf.calcbufxcx[2] - buf.calcbufx[j, 2])) * invrcur2));
                df = -f;
                for (k = 0; k <= s.nl - 1; k++)
                {
                    w = s.wr[tg, 1 + k * s.ny + i];
                    y[i] = y[i] + f * w;
                    for (kk = 0; kk <= s.nx - 1; kk++)
                    {
                        dy[i * s.nx + kk] = dy[i * s.nx + kk] + w * df * invrcur2 * 2 * (buf.calcbufxcx[kk] - buf.calcbufx[j, kk]);
                    }
                    t = f * f;
                    f = t * t;
                    df = -f;
                    invrcur2 = 4 * invrcur2;
                }
            }
        }
    }


    /*************************************************************************
    This function calculates values of the RBF model at the  given  point  and
    its first/second  derivatives,  using  external  buffer  object  (internal
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
        Y, DY, D2Y -   possibly preallocated arrays

    OUTPUT PARAMETERS:
        Y       -   function value, array[NY]. Y is not reallocated when it
                    is larger than NY.
        DY      -   derivatives, array[NY*NX]. DY is not reallocated when it
                    is larger than NY*NX.
        D2Y     -   derivatives, array[NY*NX*NX]. D2Y is not reallocated when
                    it is larger than NY*NX*NX.

      -- ALGLIB --
         Copyright 13.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv1tshessbuf(rbfv1model s,
        rbfv1calcbuffer buf,
        double[] x,
        ref double[] y,
        ref double[] dy,
        ref double[] d2y,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        int i0 = 0;
        int i1 = 0;
        int lx = 0;
        int tg = 0;
        double t = 0;
        double rcur = 0;
        double invrcur2 = 0;
        double f = 0;
        double df = 0;
        double d2f = 0;
        double w = 0;

        ap.assert(ap.len(x) >= s.nx, "RBFDiffBuf: Length(X)<NX");
        ap.assert(apserv.isfinitevector(x, s.nx, _params), "RBFDiffBuf: X contains infinite or NaN values");
        if (ap.len(y) < s.ny)
        {
            y = new double[s.ny];
        }
        if (ap.len(dy) < s.ny * s.nx)
        {
            dy = new double[s.ny * s.nx];
        }
        if (ap.len(d2y) < s.ny * s.nx * s.nx)
        {
            d2y = new double[s.ny * s.nx * s.nx];
        }
        for (i = 0; i <= s.ny - 1; i++)
        {
            y[i] = s.v[i, mxnx];
            for (j = 0; j <= s.nx - 1; j++)
            {
                y[i] = y[i] + s.v[i, j] * x[j];
                dy[i * s.nx + j] = s.v[i, j];
            }
        }
        ablasf.rsetv(s.ny * s.nx * s.nx, 0.0, d2y, _params);
        if (s.nc == 0)
        {
            return;
        }
        apserv.rvectorsetlengthatleast(ref buf.calcbufxcx, mxnx, _params);
        for (i = 0; i <= mxnx - 1; i++)
        {
            buf.calcbufxcx[i] = 0.0;
        }
        for (i = 0; i <= s.nx - 1; i++)
        {
            buf.calcbufxcx[i] = x[i];
        }
        lx = nearestneighbor.kdtreetsqueryrnn(s.tree, buf.requestbuffer, buf.calcbufxcx, s.rmax * rbffarradius, true, _params);
        nearestneighbor.kdtreetsqueryresultsx(s.tree, buf.requestbuffer, ref buf.calcbufx, _params);
        nearestneighbor.kdtreetsqueryresultstags(s.tree, buf.requestbuffer, ref buf.calcbuftags, _params);
        for (i = 0; i <= s.ny - 1; i++)
        {
            for (j = 0; j <= lx - 1; j++)
            {
                tg = buf.calcbuftags[j];
                rcur = s.wr[tg, 0];
                invrcur2 = 1 / (rcur * rcur);
                f = Math.Exp(-((math.sqr(buf.calcbufxcx[0] - buf.calcbufx[j, 0]) + math.sqr(buf.calcbufxcx[1] - buf.calcbufx[j, 1]) + math.sqr(buf.calcbufxcx[2] - buf.calcbufx[j, 2])) * invrcur2));
                df = -f;
                d2f = f;
                for (k = 0; k <= s.nl - 1; k++)
                {
                    w = s.wr[tg, 1 + k * s.ny + i];
                    y[i] = y[i] + f * w;
                    for (i0 = 0; i0 <= s.nx - 1; i0++)
                    {
                        for (i1 = 0; i1 <= s.nx - 1; i1++)
                        {
                            if (i0 == i1)
                            {

                                //
                                // Compute derivative and diagonal element of the Hessian
                                //
                                dy[i * s.nx + i0] = dy[i * s.nx + i0] + w * df * invrcur2 * 2 * (buf.calcbufxcx[i0] - buf.calcbufx[j, i0]);
                                d2y[i * s.nx * s.nx + i0 * s.nx + i1] = d2y[i * s.nx * s.nx + i0 * s.nx + i1] + w * (d2f * invrcur2 * invrcur2 * 4 * math.sqr(buf.calcbufxcx[i0] - buf.calcbufx[j, i0]) + df * invrcur2 * 2);
                            }
                            else
                            {

                                //
                                // Compute off-diagonal element of the Hessian
                                //
                                d2y[i * s.nx * s.nx + i0 * s.nx + i1] = d2y[i * s.nx * s.nx + i0 * s.nx + i1] + w * d2f * invrcur2 * invrcur2 * 4 * (buf.calcbufxcx[i0] - buf.calcbufx[j, i0]) * (buf.calcbufxcx[i1] - buf.calcbufx[j, i1]);
                            }
                        }
                    }
                    t = f * f;
                    f = t * t;
                    df = -f;
                    d2f = f;
                    invrcur2 = 4 * invrcur2;
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
    public static void rbfv1gridcalc2(rbfv1model s,
        double[] x0,
        int n0,
        double[] x1,
        int n1,
        ref double[,] y,
        xparams _params)
    {
        double[] cpx0 = new double[0];
        double[] cpx1 = new double[0];
        int[] p01 = new int[0];
        int[] p11 = new int[0];
        int[] p2 = new int[0];
        double rlimit = 0;
        double xcnorm2 = 0;
        int hp01 = 0;
        double hcpx0 = 0;
        double xc0 = 0;
        double xc1 = 0;
        double omega = 0;
        double radius = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        int d = 0;
        int i00 = 0;
        int i01 = 0;
        int i10 = 0;
        int i11 = 0;

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
        if ((s.ny != 1 || s.nx != 2) || s.nc == 0)
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

        //
        //calculate function's value
        //
        for (i = 0; i <= s.nc - 1; i++)
        {
            radius = s.wr[i, 0];
            for (d = 0; d <= s.nl - 1; d++)
            {
                omega = s.wr[i, 1 + d];
                rlimit = radius * rbffarradius;

                //
                //search lower and upper indexes
                //
                i00 = tsort.lowerbound(cpx0, n0, s.xc[i, 0] - rlimit, _params);
                i01 = tsort.upperbound(cpx0, n0, s.xc[i, 0] + rlimit, _params);
                i10 = tsort.lowerbound(cpx1, n1, s.xc[i, 1] - rlimit, _params);
                i11 = tsort.upperbound(cpx1, n1, s.xc[i, 1] + rlimit, _params);
                xc0 = s.xc[i, 0];
                xc1 = s.xc[i, 1];
                for (j = i00; j <= i01 - 1; j++)
                {
                    hcpx0 = cpx0[j];
                    hp01 = p01[j];
                    for (k = i10; k <= i11 - 1; k++)
                    {
                        xcnorm2 = math.sqr(hcpx0 - xc0) + math.sqr(cpx1[k] - xc1);
                        if ((double)(xcnorm2) <= (double)(rlimit * rlimit))
                        {
                            y[hp01, p11[k]] = y[hp01, p11[k]] + Math.Exp(-(xcnorm2 / math.sqr(radius))) * omega;
                        }
                    }
                }
                radius = 0.5 * radius;
            }
        }

        //
        //add linear term
        //
        for (i = 0; i <= n0 - 1; i++)
        {
            for (j = 0; j <= n1 - 1; j++)
            {
                y[i, j] = y[i, j] + s.v[0, 0] * x0[i] + s.v[0, 1] * x1[j] + s.v[0, mxnx];
            }
        }
    }


    public static void rbfv1gridcalc3vrec(rbfv1model s,
        double[] x0,
        int n0,
        double[] x1,
        int n1,
        double[] x2,
        int n2,
        int[] blocks0,
        int block0a,
        int block0b,
        int[] blocks1,
        int block1a,
        int block1b,
        int[] blocks2,
        int block2a,
        int block2b,
        bool[] flagy,
        bool sparsey,
        double searchradius,
        double avgfuncpernode,
        smp.shared_pool bufpool,
        double[] y,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        int t = 0;
        int l = 0;
        int i0 = 0;
        int i1 = 0;
        int i2 = 0;
        int ic = 0;
        gridcalc3v1buf pbuf = null;
        int flag12dim1 = 0;
        int flag12dim2 = 0;
        double problemcost = 0;
        int maxbs = 0;
        int nx = 0;
        int ny = 0;
        double v = 0;
        int kc = 0;
        int tg = 0;
        double rcur = 0;
        double rcur2 = 0;
        double basisfuncval = 0;
        int dstoffs = 0;
        int srcoffs = 0;
        int ubnd = 0;
        double w0 = 0;
        double w1 = 0;
        double w2 = 0;
        bool allnodes = new bool();
        bool somenodes = new bool();

        nx = s.nx;
        ny = s.ny;

        //
        // Try to split large problem
        //
        problemcost = (s.nl + 1) * s.ny * 2 * (avgfuncpernode + 1);
        problemcost = problemcost * (blocks0[block0b] - blocks0[block0a]);
        problemcost = problemcost * (blocks1[block1b] - blocks1[block1a]);
        problemcost = problemcost * (blocks2[block2b] - blocks2[block2a]);
        maxbs = 0;
        maxbs = Math.Max(maxbs, block0b - block0a);
        maxbs = Math.Max(maxbs, block1b - block1a);
        maxbs = Math.Max(maxbs, block2b - block2a);
        if ((double)(problemcost) >= (double)(minbasecasecost) && maxbs >= 2)
        {
            if (block0b - block0a == maxbs)
            {
                rbfv1gridcalc3vrec(s, x0, n0, x1, n1, x2, n2, blocks0, block0a, block0a + maxbs / 2, blocks1, block1a, block1b, blocks2, block2a, block2b, flagy, sparsey, searchradius, avgfuncpernode, bufpool, y, _params);
                rbfv1gridcalc3vrec(s, x0, n0, x1, n1, x2, n2, blocks0, block0a + maxbs / 2, block0b, blocks1, block1a, block1b, blocks2, block2a, block2b, flagy, sparsey, searchradius, avgfuncpernode, bufpool, y, _params);
                return;
            }
            if (block1b - block1a == maxbs)
            {
                rbfv1gridcalc3vrec(s, x0, n0, x1, n1, x2, n2, blocks0, block0a, block0b, blocks1, block1a, block1a + maxbs / 2, blocks2, block2a, block2b, flagy, sparsey, searchradius, avgfuncpernode, bufpool, y, _params);
                rbfv1gridcalc3vrec(s, x0, n0, x1, n1, x2, n2, blocks0, block0a, block0b, blocks1, block1a + maxbs / 2, block1b, blocks2, block2a, block2b, flagy, sparsey, searchradius, avgfuncpernode, bufpool, y, _params);
                return;
            }
            if (block2b - block2a == maxbs)
            {
                rbfv1gridcalc3vrec(s, x0, n0, x1, n1, x2, n2, blocks0, block0a, block0b, blocks1, block1a, block1b, blocks2, block2a, block2a + maxbs / 2, flagy, sparsey, searchradius, avgfuncpernode, bufpool, y, _params);
                rbfv1gridcalc3vrec(s, x0, n0, x1, n1, x2, n2, blocks0, block0a, block0b, blocks1, block1a, block1b, blocks2, block2a + maxbs / 2, block2b, flagy, sparsey, searchradius, avgfuncpernode, bufpool, y, _params);
                return;
            }
        }

        //
        // Retrieve buffer object from pool (it will be returned later)
        //
        smp.ae_shared_pool_retrieve(bufpool, ref pbuf);

        //
        // Calculate RBF model
        //
        for (i2 = block2a; i2 <= block2b - 1; i2++)
        {
            for (i1 = block1a; i1 <= block1b - 1; i1++)
            {
                for (i0 = block0a; i0 <= block0b - 1; i0++)
                {

                    //
                    // Analyze block - determine what elements are needed and what are not.
                    //
                    // After this block is done, two flag variables can be used:
                    // * SomeNodes, which is True when there are at least one node which have
                    //   to be calculated
                    // * AllNodes, which is True when all nodes are required
                    //
                    somenodes = true;
                    allnodes = true;
                    flag12dim1 = blocks1[i1 + 1] - blocks1[i1];
                    flag12dim2 = blocks2[i2 + 1] - blocks2[i2];
                    if (sparsey)
                    {

                        //
                        // Use FlagY to determine what is required.
                        //
                        apserv.bvectorsetlengthatleast(ref pbuf.flag0, n0, _params);
                        apserv.bvectorsetlengthatleast(ref pbuf.flag1, n1, _params);
                        apserv.bvectorsetlengthatleast(ref pbuf.flag2, n2, _params);
                        apserv.bvectorsetlengthatleast(ref pbuf.flag12, flag12dim1 * flag12dim2, _params);
                        for (i = blocks0[i0]; i <= blocks0[i0 + 1] - 1; i++)
                        {
                            pbuf.flag0[i] = false;
                        }
                        for (j = blocks1[i1]; j <= blocks1[i1 + 1] - 1; j++)
                        {
                            pbuf.flag1[j] = false;
                        }
                        for (k = blocks2[i2]; k <= blocks2[i2 + 1] - 1; k++)
                        {
                            pbuf.flag2[k] = false;
                        }
                        for (i = 0; i <= flag12dim1 * flag12dim2 - 1; i++)
                        {
                            pbuf.flag12[i] = false;
                        }
                        somenodes = false;
                        allnodes = true;
                        for (k = blocks2[i2]; k <= blocks2[i2 + 1] - 1; k++)
                        {
                            for (j = blocks1[i1]; j <= blocks1[i1 + 1] - 1; j++)
                            {
                                dstoffs = j - blocks1[i1] + flag12dim1 * (k - blocks2[i2]);
                                srcoffs = j * n0 + k * n0 * n1;
                                for (i = blocks0[i0]; i <= blocks0[i0 + 1] - 1; i++)
                                {
                                    if (flagy[srcoffs + i])
                                    {
                                        pbuf.flag0[i] = true;
                                        pbuf.flag1[j] = true;
                                        pbuf.flag2[k] = true;
                                        pbuf.flag12[dstoffs] = true;
                                        somenodes = true;
                                    }
                                    else
                                    {
                                        allnodes = false;
                                    }
                                }
                            }
                        }
                    }

                    //
                    // Skip block if it is completely empty.
                    //
                    if (!somenodes)
                    {
                        continue;
                    }

                    //
                    // compute linear term for block (I0,I1,I2)
                    //
                    for (k = blocks2[i2]; k <= blocks2[i2 + 1] - 1; k++)
                    {
                        for (j = blocks1[i1]; j <= blocks1[i1 + 1] - 1; j++)
                        {

                            //
                            // do we need this micro-row?
                            //
                            if (!allnodes && !pbuf.flag12[j - blocks1[i1] + flag12dim1 * (k - blocks2[i2])])
                            {
                                continue;
                            }

                            //
                            // Compute linear term
                            //
                            for (i = blocks0[i0]; i <= blocks0[i0 + 1] - 1; i++)
                            {
                                pbuf.tx[0] = x0[i];
                                pbuf.tx[1] = x1[j];
                                pbuf.tx[2] = x2[k];
                                for (l = 0; l <= s.ny - 1; l++)
                                {
                                    v = s.v[l, mxnx];
                                    for (t = 0; t <= nx - 1; t++)
                                    {
                                        v = v + s.v[l, t] * pbuf.tx[t];
                                    }
                                    y[l + ny * (i + j * n0 + k * n0 * n1)] = v;
                                }
                            }
                        }
                    }

                    //
                    // compute RBF term for block (I0,I1,I2)
                    //
                    pbuf.tx[0] = 0.5 * (x0[blocks0[i0]] + x0[blocks0[i0 + 1] - 1]);
                    pbuf.tx[1] = 0.5 * (x1[blocks1[i1]] + x1[blocks1[i1 + 1] - 1]);
                    pbuf.tx[2] = 0.5 * (x2[blocks2[i2]] + x2[blocks2[i2 + 1] - 1]);
                    kc = nearestneighbor.kdtreetsqueryrnn(s.tree, pbuf.requestbuf, pbuf.tx, searchradius, true, _params);
                    nearestneighbor.kdtreetsqueryresultsx(s.tree, pbuf.requestbuf, ref pbuf.calcbufx, _params);
                    nearestneighbor.kdtreetsqueryresultstags(s.tree, pbuf.requestbuf, ref pbuf.calcbuftags, _params);
                    for (ic = 0; ic <= kc - 1; ic++)
                    {
                        pbuf.cx[0] = pbuf.calcbufx[ic, 0];
                        pbuf.cx[1] = pbuf.calcbufx[ic, 1];
                        pbuf.cx[2] = pbuf.calcbufx[ic, 2];
                        tg = pbuf.calcbuftags[ic];
                        rcur = s.wr[tg, 0];
                        rcur2 = rcur * rcur;
                        for (i = blocks0[i0]; i <= blocks0[i0 + 1] - 1; i++)
                        {
                            if (allnodes || pbuf.flag0[i])
                            {
                                pbuf.expbuf0[i] = Math.Exp(-(math.sqr(x0[i] - pbuf.cx[0]) / rcur2));
                            }
                            else
                            {
                                pbuf.expbuf0[i] = 0.0;
                            }
                        }
                        for (j = blocks1[i1]; j <= blocks1[i1 + 1] - 1; j++)
                        {
                            if (allnodes || pbuf.flag1[j])
                            {
                                pbuf.expbuf1[j] = Math.Exp(-(math.sqr(x1[j] - pbuf.cx[1]) / rcur2));
                            }
                            else
                            {
                                pbuf.expbuf1[j] = 0.0;
                            }
                        }
                        for (k = blocks2[i2]; k <= blocks2[i2 + 1] - 1; k++)
                        {
                            if (allnodes || pbuf.flag2[k])
                            {
                                pbuf.expbuf2[k] = Math.Exp(-(math.sqr(x2[k] - pbuf.cx[2]) / rcur2));
                            }
                            else
                            {
                                pbuf.expbuf2[k] = 0.0;
                            }
                        }
                        for (t = 0; t <= s.nl - 1; t++)
                        {

                            //
                            // Calculate
                            //
                            for (k = blocks2[i2]; k <= blocks2[i2 + 1] - 1; k++)
                            {
                                for (j = blocks1[i1]; j <= blocks1[i1 + 1] - 1; j++)
                                {

                                    //
                                    // do we need this micro-row?
                                    //
                                    if (!allnodes && !pbuf.flag12[j - blocks1[i1] + flag12dim1 * (k - blocks2[i2])])
                                    {
                                        continue;
                                    }

                                    //
                                    // Prepare local variables
                                    //
                                    dstoffs = ny * (blocks0[i0] + j * n0 + k * n0 * n1);
                                    v = pbuf.expbuf1[j] * pbuf.expbuf2[k];

                                    //
                                    // Optimized for NY=1
                                    //
                                    if (s.ny == 1)
                                    {
                                        w0 = s.wr[tg, 1 + t * s.ny + 0];
                                        ubnd = blocks0[i0 + 1] - 1;
                                        for (i = blocks0[i0]; i <= ubnd; i++)
                                        {
                                            basisfuncval = pbuf.expbuf0[i] * v;
                                            y[dstoffs] = y[dstoffs] + basisfuncval * w0;
                                            dstoffs = dstoffs + 1;
                                        }
                                        continue;
                                    }

                                    //
                                    // Optimized for NY=2
                                    //
                                    if (s.ny == 2)
                                    {
                                        w0 = s.wr[tg, 1 + t * s.ny + 0];
                                        w1 = s.wr[tg, 1 + t * s.ny + 1];
                                        ubnd = blocks0[i0 + 1] - 1;
                                        for (i = blocks0[i0]; i <= ubnd; i++)
                                        {
                                            basisfuncval = pbuf.expbuf0[i] * v;
                                            y[dstoffs + 0] = y[dstoffs + 0] + basisfuncval * w0;
                                            y[dstoffs + 1] = y[dstoffs + 1] + basisfuncval * w1;
                                            dstoffs = dstoffs + 2;
                                        }
                                        continue;
                                    }

                                    //
                                    // Optimized for NY=3
                                    //
                                    if (s.ny == 3)
                                    {
                                        w0 = s.wr[tg, 1 + t * s.ny + 0];
                                        w1 = s.wr[tg, 1 + t * s.ny + 1];
                                        w2 = s.wr[tg, 1 + t * s.ny + 2];
                                        ubnd = blocks0[i0 + 1] - 1;
                                        for (i = blocks0[i0]; i <= ubnd; i++)
                                        {
                                            basisfuncval = pbuf.expbuf0[i] * v;
                                            y[dstoffs + 0] = y[dstoffs + 0] + basisfuncval * w0;
                                            y[dstoffs + 1] = y[dstoffs + 1] + basisfuncval * w1;
                                            y[dstoffs + 2] = y[dstoffs + 2] + basisfuncval * w2;
                                            dstoffs = dstoffs + 3;
                                        }
                                        continue;
                                    }

                                    //
                                    // General case
                                    //
                                    for (i = blocks0[i0]; i <= blocks0[i0 + 1] - 1; i++)
                                    {
                                        basisfuncval = pbuf.expbuf0[i] * v;
                                        for (l = 0; l <= s.ny - 1; l++)
                                        {
                                            y[l + dstoffs] = y[l + dstoffs] + basisfuncval * s.wr[tg, 1 + t * s.ny + l];
                                        }
                                        dstoffs = dstoffs + ny;
                                    }
                                }
                            }

                            //
                            // Update basis functions
                            //
                            if (t != s.nl - 1)
                            {
                                ubnd = blocks0[i0 + 1] - 1;
                                for (i = blocks0[i0]; i <= ubnd; i++)
                                {
                                    if (allnodes || pbuf.flag0[i])
                                    {
                                        v = pbuf.expbuf0[i] * pbuf.expbuf0[i];
                                        pbuf.expbuf0[i] = v * v;
                                    }
                                }
                                ubnd = blocks1[i1 + 1] - 1;
                                for (j = blocks1[i1]; j <= ubnd; j++)
                                {
                                    if (allnodes || pbuf.flag1[j])
                                    {
                                        v = pbuf.expbuf1[j] * pbuf.expbuf1[j];
                                        pbuf.expbuf1[j] = v * v;
                                    }
                                }
                                ubnd = blocks2[i2 + 1] - 1;
                                for (k = blocks2[i2]; k <= ubnd; k++)
                                {
                                    if (allnodes || pbuf.flag2[k])
                                    {
                                        v = pbuf.expbuf2[k] * pbuf.expbuf2[k];
                                        pbuf.expbuf2[k] = v * v;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //
        // Recycle buffer object back to pool
        //
        smp.ae_shared_pool_recycle(bufpool, ref pbuf);
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_rbfv1gridcalc3vrec(rbfv1model s,
        double[] x0,
        int n0,
        double[] x1,
        int n1,
        double[] x2,
        int n2,
        int[] blocks0,
        int block0a,
        int block0b,
        int[] blocks1,
        int block1a,
        int block1b,
        int[] blocks2,
        int block2a,
        int block2b,
        bool[] flagy,
        bool sparsey,
        double searchradius,
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
                    * last column       - radius, same for all dimensions of
                                          the function being modelled
        NC      -   number of the centers
        V       -   polynomial  term , array[NY,NX+1]. One row per one 
                    dimension of the function being modelled. First NX 
                    elements are linear coefficients, V[NX] is equal to the 
                    constant part.

      -- ALGLIB --
         Copyright 13.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfv1unpack(rbfv1model s,
        ref int nx,
        ref int ny,
        ref double[,] xwr,
        ref int nc,
        ref double[,] v,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double rcur = 0;
        int i_ = 0;
        int i1_ = 0;

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
            for (i_ = 0; i_ <= s.nx - 1; i_++)
            {
                v[i, i_] = s.v[i, i_];
            }
            v[i, s.nx] = s.v[i, mxnx];
        }

        //
        // Fill XWR and V
        //
        if (nc * s.nl > 0)
        {
            xwr = new double[s.nc * s.nl, s.nx + s.ny + 1];
            for (i = 0; i <= s.nc - 1; i++)
            {
                rcur = s.wr[i, 0];
                for (j = 0; j <= s.nl - 1; j++)
                {
                    for (i_ = 0; i_ <= s.nx - 1; i_++)
                    {
                        xwr[i * s.nl + j, i_] = s.xc[i, i_];
                    }
                    i1_ = (1 + j * s.ny) - (s.nx);
                    for (i_ = s.nx; i_ <= s.nx + s.ny - 1; i_++)
                    {
                        xwr[i * s.nl + j, i_] = s.wr[i, i_ + i1_];
                    }
                    xwr[i * s.nl + j, s.nx + s.ny] = rcur;
                    rcur = 0.5 * rcur;
                }
            }
        }
    }


    private static bool rbfv1buildlinearmodel(double[,] x,
        ref double[,] y,
        int n,
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
        ap.assert(ny > 0, "BuildLinearModel: NY<=0");

        //
        // Handle degenerate case (N=0)
        //
        result = true;
        v = new double[ny, mxnx + 1];
        if (n == 0)
        {
            for (j = 0; j <= mxnx; j++)
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
            a = new double[n, mxnx + 1];
            shifting = new double[mxnx];
            scaling = 0;
            for (i = 0; i <= mxnx - 1; i++)
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
                for (j = 0; j <= mxnx - 1; j++)
                {
                    a[i, j] = (x[i, j] - shifting[j]) / scaling;
                }
            }
            for (i = 0; i <= n - 1; i++)
            {
                a[i, mxnx] = 1;
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
                lsfit.lsfitlinear(tmpy, a, n, mxnx + 1, ref c, rep, _params);
                if (rep.terminationtype <= 0)
                {
                    result = false;
                    return result;
                }
                for (j = 0; j <= mxnx - 1; j++)
                {
                    v[i, j] = c[j] / scaling;
                }
                v[i, mxnx] = c[mxnx];
                for (j = 0; j <= mxnx - 1; j++)
                {
                    v[i, mxnx] = v[i, mxnx] - shifting[j] * v[i, j];
                }
                for (j = 0; j <= n - 1; j++)
                {
                    for (k = 0; k <= mxnx - 1; k++)
                    {
                        y[j, i] = y[j, i] - x[j, k] * v[i, k];
                    }
                    y[j, i] = y[j, i] - v[i, mxnx];
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
                for (j = 0; j <= mxnx; j++)
                {
                    v[i, j] = 0;
                }
                for (j = 0; j <= n - 1; j++)
                {
                    v[i, mxnx] = v[i, mxnx] + y[j, i];
                }
                if (n > 0)
                {
                    v[i, mxnx] = v[i, mxnx] / n;
                }
                for (j = 0; j <= n - 1; j++)
                {
                    y[j, i] = y[j, i] - v[i, mxnx];
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
            for (j = 0; j <= mxnx; j++)
            {
                v[i, j] = 0;
            }
        }
        return result;
    }


    private static void buildrbfmodellsqr(double[,] x,
        ref double[,] y,
        double[,] xc,
        double[] r,
        int n,
        int nc,
        int ny,
        nearestneighbor.kdtree pointstree,
        nearestneighbor.kdtree centerstree,
        double epsort,
        double epserr,
        int maxits,
        ref int gnnz,
        ref int snnz,
        ref double[,] w,
        ref int info,
        ref int iterationscount,
        ref int nmv,
        xparams _params)
    {
        linlsqr.linlsqrstate state = new linlsqr.linlsqrstate();
        linlsqr.linlsqrreport lsqrrep = new linlsqr.linlsqrreport();
        sparse.sparsematrix spg = new sparse.sparsematrix();
        sparse.sparsematrix sps = new sparse.sparsematrix();
        int[] nearcenterscnt = new int[0];
        int[] nearpointscnt = new int[0];
        int[] skipnearpointscnt = new int[0];
        int[] farpointscnt = new int[0];
        int maxnearcenterscnt = 0;
        int maxnearpointscnt = 0;
        int maxfarpointscnt = 0;
        int sumnearcenterscnt = 0;
        int sumnearpointscnt = 0;
        int sumfarpointscnt = 0;
        double maxrad = 0;
        int[] pointstags = new int[0];
        int[] centerstags = new int[0];
        double[,] nearpoints = new double[0, 0];
        double[,] nearcenters = new double[0, 0];
        double[,] farpoints = new double[0, 0];
        int tmpi = 0;
        int pointscnt = 0;
        int centerscnt = 0;
        double[] xcx = new double[0];
        double[] tmpy = new double[0];
        double[] tc = new double[0];
        double[] g = new double[0];
        double[] c = new double[0];
        int i = 0;
        int j = 0;
        int k = 0;
        int sind = 0;
        double[,] a = new double[0, 0];
        double vv = 0;
        double vx = 0;
        double vy = 0;
        double vz = 0;
        double vr = 0;
        double gnorm2 = 0;
        double[] tmp0 = new double[0];
        double[] tmp1 = new double[0];
        double[] tmp2 = new double[0];
        double fx = 0;
        double[,] xx = new double[0, 0];
        double[,] cx = new double[0, 0];
        double mrad = 0;
        int i_ = 0;

        gnnz = 0;
        snnz = 0;
        w = new double[0, 0];
        info = 0;
        iterationscount = 0;
        nmv = 0;


        //
        // Handle special cases: NC=0
        //
        if (nc == 0)
        {
            info = 1;
            iterationscount = 0;
            nmv = 0;
            return;
        }

        //
        // Prepare for general case, NC>0
        //
        xcx = new double[mxnx];
        pointstags = new int[n];
        centerstags = new int[nc];
        info = -1;
        iterationscount = 0;
        nmv = 0;

        //
        // This block prepares quantities used to compute approximate cardinal basis functions (ACBFs):
        // * NearCentersCnt[]   -   array[NC], whose elements store number of near centers used to build ACBF
        // * NearPointsCnt[]    -   array[NC], number of near points used to build ACBF
        // * FarPointsCnt[]     -   array[NC], number of far points (ones where ACBF is nonzero)
        // * MaxNearCentersCnt  -   max(NearCentersCnt)
        // * MaxNearPointsCnt   -   max(NearPointsCnt)
        // * SumNearCentersCnt  -   sum(NearCentersCnt)
        // * SumNearPointsCnt   -   sum(NearPointsCnt)
        // * SumFarPointsCnt    -   sum(FarPointsCnt)
        //
        nearcenterscnt = new int[nc];
        nearpointscnt = new int[nc];
        skipnearpointscnt = new int[nc];
        farpointscnt = new int[nc];
        maxnearcenterscnt = 0;
        maxnearpointscnt = 0;
        maxfarpointscnt = 0;
        sumnearcenterscnt = 0;
        sumnearpointscnt = 0;
        sumfarpointscnt = 0;
        for (i = 0; i <= nc - 1; i++)
        {
            for (j = 0; j <= mxnx - 1; j++)
            {
                xcx[j] = xc[i, j];
            }

            //
            // Determine number of near centers and maximum radius of near centers
            //
            nearcenterscnt[i] = nearestneighbor.kdtreequeryrnn(centerstree, xcx, r[i] * rbfnearradius, true, _params);
            nearestneighbor.kdtreequeryresultstags(centerstree, ref centerstags, _params);
            maxrad = 0;
            for (j = 0; j <= nearcenterscnt[i] - 1; j++)
            {
                maxrad = Math.Max(maxrad, Math.Abs(r[centerstags[j]]));
            }

            //
            // Determine number of near points (ones which used to build ACBF)
            // and skipped points (the most near points which are NOT used to build ACBF
            // and are NOT included in the near points count
            //
            skipnearpointscnt[i] = nearestneighbor.kdtreequeryrnn(pointstree, xcx, 0.1 * r[i], true, _params);
            nearpointscnt[i] = nearestneighbor.kdtreequeryrnn(pointstree, xcx, (r[i] + maxrad) * rbfnearradius, true, _params) - skipnearpointscnt[i];
            ap.assert(nearpointscnt[i] >= 0, "BuildRBFModelLSQR: internal error");

            //
            // Determine number of far points
            //
            farpointscnt[i] = nearestneighbor.kdtreequeryrnn(pointstree, xcx, Math.Max(r[i] * rbfnearradius + maxrad * rbffarradius, r[i] * rbffarradius), true, _params);

            //
            // calculate sum and max, make some basic checks
            //
            ap.assert(nearcenterscnt[i] > 0, "BuildRBFModelLSQR: internal error");
            maxnearcenterscnt = Math.Max(maxnearcenterscnt, nearcenterscnt[i]);
            maxnearpointscnt = Math.Max(maxnearpointscnt, nearpointscnt[i]);
            maxfarpointscnt = Math.Max(maxfarpointscnt, farpointscnt[i]);
            sumnearcenterscnt = sumnearcenterscnt + nearcenterscnt[i];
            sumnearpointscnt = sumnearpointscnt + nearpointscnt[i];
            sumfarpointscnt = sumfarpointscnt + farpointscnt[i];
        }
        snnz = sumnearcenterscnt;
        gnnz = sumfarpointscnt;
        ap.assert(maxnearcenterscnt > 0, "BuildRBFModelLSQR: internal error");

        //
        // Allocate temporaries.
        //
        // NOTE: we want to avoid allocation of zero-size arrays, so we
        //       use max(desired_size,1) instead of desired_size when performing
        //       memory allocation.
        //
        a = new double[maxnearpointscnt + maxnearcenterscnt, maxnearcenterscnt];
        tmpy = new double[maxnearpointscnt + maxnearcenterscnt];
        g = new double[maxnearcenterscnt];
        c = new double[maxnearcenterscnt];
        nearcenters = new double[maxnearcenterscnt, mxnx];
        nearpoints = new double[Math.Max(maxnearpointscnt, 1), mxnx];
        farpoints = new double[Math.Max(maxfarpointscnt, 1), mxnx];

        //
        // fill matrix SpG
        //
        sparse.sparsecreate(n, nc, gnnz, spg, _params);
        sparse.sparsecreate(nc, nc, snnz, sps, _params);
        for (i = 0; i <= nc - 1; i++)
        {
            centerscnt = nearcenterscnt[i];

            //
            // main center
            //
            for (j = 0; j <= mxnx - 1; j++)
            {
                xcx[j] = xc[i, j];
            }

            //
            // center's tree
            //
            tmpi = nearestneighbor.kdtreequeryknn(centerstree, xcx, centerscnt, true, _params);
            ap.assert(tmpi == centerscnt, "BuildRBFModelLSQR: internal error");
            nearestneighbor.kdtreequeryresultsx(centerstree, ref cx, _params);
            nearestneighbor.kdtreequeryresultstags(centerstree, ref centerstags, _params);

            //
            // point's tree
            //
            mrad = 0;
            for (j = 0; j <= centerscnt - 1; j++)
            {
                mrad = Math.Max(mrad, r[centerstags[j]]);
            }

            //
            // we need to be sure that 'CTree' contains
            // at least one side center
            //
            sparse.sparseset(sps, i, i, 1, _params);
            c[0] = 1.0;
            for (j = 1; j <= centerscnt - 1; j++)
            {
                c[j] = 0.0;
            }
            if (centerscnt > 1 && nearpointscnt[i] > 0)
            {

                //
                // first KDTree request for points
                //
                pointscnt = nearpointscnt[i];
                tmpi = nearestneighbor.kdtreequeryknn(pointstree, xcx, skipnearpointscnt[i] + nearpointscnt[i], true, _params);
                ap.assert(tmpi == skipnearpointscnt[i] + nearpointscnt[i], "BuildRBFModelLSQR: internal error");
                nearestneighbor.kdtreequeryresultsx(pointstree, ref xx, _params);
                sind = skipnearpointscnt[i];
                for (j = 0; j <= pointscnt - 1; j++)
                {
                    vx = xx[sind + j, 0];
                    vy = xx[sind + j, 1];
                    vz = xx[sind + j, 2];
                    for (k = 0; k <= centerscnt - 1; k++)
                    {
                        vr = 0.0;
                        vv = vx - cx[k, 0];
                        vr = vr + vv * vv;
                        vv = vy - cx[k, 1];
                        vr = vr + vv * vv;
                        vv = vz - cx[k, 2];
                        vr = vr + vv * vv;
                        vv = r[centerstags[k]];
                        a[j, k] = Math.Exp(-(vr / (vv * vv)));
                    }
                }
                for (j = 0; j <= centerscnt - 1; j++)
                {
                    g[j] = Math.Exp(-((math.sqr(xcx[0] - cx[j, 0]) + math.sqr(xcx[1] - cx[j, 1]) + math.sqr(xcx[2] - cx[j, 2])) / math.sqr(r[centerstags[j]])));
                }

                //
                // calculate the problem
                //
                gnorm2 = 0.0;
                for (i_ = 0; i_ <= centerscnt - 1; i_++)
                {
                    gnorm2 += g[i_] * g[i_];
                }
                for (j = 0; j <= pointscnt - 1; j++)
                {
                    vv = 0.0;
                    for (i_ = 0; i_ <= centerscnt - 1; i_++)
                    {
                        vv += a[j, i_] * g[i_];
                    }
                    vv = vv / gnorm2;
                    tmpy[j] = -vv;
                    for (i_ = 0; i_ <= centerscnt - 1; i_++)
                    {
                        a[j, i_] = a[j, i_] - vv * g[i_];
                    }
                }
                for (j = pointscnt; j <= pointscnt + centerscnt - 1; j++)
                {
                    for (k = 0; k <= centerscnt - 1; k++)
                    {
                        a[j, k] = 0.0;
                    }
                    a[j, j - pointscnt] = 1.0E-6;
                    tmpy[j] = 0.0;
                }
                fbls.fblssolvels(ref a, ref tmpy, pointscnt + centerscnt, centerscnt, ref tmp0, ref tmp1, ref tmp2, _params);
                for (i_ = 0; i_ <= centerscnt - 1; i_++)
                {
                    c[i_] = tmpy[i_];
                }
                vv = 0.0;
                for (i_ = 0; i_ <= centerscnt - 1; i_++)
                {
                    vv += g[i_] * c[i_];
                }
                vv = vv / gnorm2;
                for (i_ = 0; i_ <= centerscnt - 1; i_++)
                {
                    c[i_] = c[i_] - vv * g[i_];
                }
                vv = 1 / gnorm2;
                for (i_ = 0; i_ <= centerscnt - 1; i_++)
                {
                    c[i_] = c[i_] + vv * g[i_];
                }
                for (j = 0; j <= centerscnt - 1; j++)
                {
                    sparse.sparseset(sps, i, centerstags[j], c[j], _params);
                }
            }

            //
            // second KDTree request for points
            //
            pointscnt = farpointscnt[i];
            tmpi = nearestneighbor.kdtreequeryknn(pointstree, xcx, pointscnt, true, _params);
            ap.assert(tmpi == pointscnt, "BuildRBFModelLSQR: internal error");
            nearestneighbor.kdtreequeryresultsx(pointstree, ref xx, _params);
            nearestneighbor.kdtreequeryresultstags(pointstree, ref pointstags, _params);

            //
            //fill SpG matrix
            //
            for (j = 0; j <= pointscnt - 1; j++)
            {
                fx = 0;
                vx = xx[j, 0];
                vy = xx[j, 1];
                vz = xx[j, 2];
                for (k = 0; k <= centerscnt - 1; k++)
                {
                    vr = 0.0;
                    vv = vx - cx[k, 0];
                    vr = vr + vv * vv;
                    vv = vy - cx[k, 1];
                    vr = vr + vv * vv;
                    vv = vz - cx[k, 2];
                    vr = vr + vv * vv;
                    vv = r[centerstags[k]];
                    vv = vv * vv;
                    fx = fx + c[k] * Math.Exp(-(vr / vv));
                }
                sparse.sparseset(spg, pointstags[j], i, fx, _params);
            }
        }
        sparse.sparseconverttocrs(spg, _params);
        sparse.sparseconverttocrs(sps, _params);

        //
        // solve by LSQR method
        //
        tmpy = new double[n];
        tc = new double[nc];
        w = new double[nc, ny];
        linlsqr.linlsqrcreate(n, nc, state, _params);
        linlsqr.linlsqrsetcond(state, epsort, epserr, maxits, _params);
        for (i = 0; i <= ny - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                tmpy[j] = y[j, i];
            }
            linlsqr.linlsqrsolvesparse(state, spg, tmpy, _params);
            linlsqr.linlsqrresults(state, ref c, lsqrrep, _params);
            if (lsqrrep.terminationtype <= 0)
            {
                info = -4;
                return;
            }
            sparse.sparsemtv(sps, c, ref tc, _params);
            for (j = 0; j <= nc - 1; j++)
            {
                w[j, i] = tc[j];
            }
            iterationscount = iterationscount + lsqrrep.iterationscount;
            nmv = nmv + lsqrrep.nmv;
        }
        info = 1;
    }


    private static void buildrbfmlayersmodellsqr(double[,] x,
        ref double[,] y,
        ref double[,] xc,
        double rval,
        ref double[] r,
        int n,
        ref int nc,
        int ny,
        int nlayers,
        nearestneighbor.kdtree centerstree,
        double epsort,
        double epserr,
        int maxits,
        double lambdav,
        ref int annz,
        ref double[,] w,
        ref int info,
        ref int iterationscount,
        ref int nmv,
        xparams _params)
    {
        linlsqr.linlsqrstate state = new linlsqr.linlsqrstate();
        linlsqr.linlsqrreport lsqrrep = new linlsqr.linlsqrreport();
        sparse.sparsematrix spa = new sparse.sparsematrix();
        double anorm = 0;
        double[] omega = new double[0];
        double[] xx = new double[0];
        double[] tmpy = new double[0];
        double[,] cx = new double[0, 0];
        double yval = 0;
        int nec = 0;
        int[] centerstags = new int[0];
        int layer = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        double v = 0;
        double rmaxbefore = 0;
        double rmaxafter = 0;

        xc = new double[0, 0];
        r = new double[0];
        nc = 0;
        annz = 0;
        w = new double[0, 0];
        info = 0;
        iterationscount = 0;
        nmv = 0;

        ap.assert(nlayers >= 0, "BuildRBFMLayersModelLSQR: invalid argument(NLayers<0)");
        ap.assert(n >= 0, "BuildRBFMLayersModelLSQR: invalid argument(N<0)");
        ap.assert(mxnx > 0 && mxnx <= 3, "BuildRBFMLayersModelLSQR: internal error(invalid global const MxNX: either MxNX<=0 or MxNX>3)");
        annz = 0;
        if (n == 0 || nlayers == 0)
        {
            info = 1;
            iterationscount = 0;
            nmv = 0;
            return;
        }
        nc = n * nlayers;
        xx = new double[mxnx];
        centerstags = new int[n];
        xc = new double[nc, mxnx];
        r = new double[nc];
        for (i = 0; i <= nc - 1; i++)
        {
            for (j = 0; j <= mxnx - 1; j++)
            {
                xc[i, j] = x[i % n, j];
            }
        }
        for (i = 0; i <= nc - 1; i++)
        {
            r[i] = rval / Math.Pow(2, i / n);
        }
        for (i = 0; i <= n - 1; i++)
        {
            centerstags[i] = i;
        }
        nearestneighbor.kdtreebuildtagged(xc, centerstags, n, mxnx, 0, 2, centerstree, _params);
        omega = new double[n];
        tmpy = new double[n];
        w = new double[nc, ny];
        info = -1;
        iterationscount = 0;
        nmv = 0;
        linlsqr.linlsqrcreate(n, n, state, _params);
        linlsqr.linlsqrsetcond(state, epsort, epserr, maxits, _params);
        linlsqr.linlsqrsetlambdai(state, 1.0E-6, _params);

        //
        // calculate number of non-zero elements for sparse matrix
        //
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= mxnx - 1; j++)
            {
                xx[j] = x[i, j];
            }
            annz = annz + nearestneighbor.kdtreequeryrnn(centerstree, xx, r[0] * rbfmlradius, true, _params);
        }
        for (layer = 0; layer <= nlayers - 1; layer++)
        {

            //
            // Fill sparse matrix, calculate norm(A)
            //
            anorm = 0.0;
            sparse.sparsecreate(n, n, annz, spa, _params);
            for (i = 0; i <= n - 1; i++)
            {
                for (j = 0; j <= mxnx - 1; j++)
                {
                    xx[j] = x[i, j];
                }
                nec = nearestneighbor.kdtreequeryrnn(centerstree, xx, r[layer * n] * rbfmlradius, true, _params);
                nearestneighbor.kdtreequeryresultsx(centerstree, ref cx, _params);
                nearestneighbor.kdtreequeryresultstags(centerstree, ref centerstags, _params);
                for (j = 0; j <= nec - 1; j++)
                {
                    v = Math.Exp(-((math.sqr(xx[0] - cx[j, 0]) + math.sqr(xx[1] - cx[j, 1]) + math.sqr(xx[2] - cx[j, 2])) / math.sqr(r[layer * n + centerstags[j]])));
                    sparse.sparseset(spa, i, centerstags[j], v, _params);
                    anorm = anorm + math.sqr(v);
                }
            }
            anorm = Math.Sqrt(anorm);
            sparse.sparseconverttocrs(spa, _params);

            //
            // Calculate maximum residual before adding new layer.
            // This value is not used by algorithm, the only purpose is to make debugging easier.
            //
            rmaxbefore = 0.0;
            for (j = 0; j <= n - 1; j++)
            {
                for (i = 0; i <= ny - 1; i++)
                {
                    rmaxbefore = Math.Max(rmaxbefore, Math.Abs(y[j, i]));
                }
            }

            //
            // Process NY dimensions of the target function
            //
            for (i = 0; i <= ny - 1; i++)
            {
                for (j = 0; j <= n - 1; j++)
                {
                    tmpy[j] = y[j, i];
                }

                //
                // calculate Omega for current layer
                //
                linlsqr.linlsqrsetlambdai(state, lambdav * anorm / n, _params);
                linlsqr.linlsqrsolvesparse(state, spa, tmpy, _params);
                linlsqr.linlsqrresults(state, ref omega, lsqrrep, _params);
                if (lsqrrep.terminationtype <= 0)
                {
                    info = -4;
                    return;
                }

                //
                // calculate error for current layer
                //
                for (j = 0; j <= n - 1; j++)
                {
                    yval = 0;
                    for (k = 0; k <= mxnx - 1; k++)
                    {
                        xx[k] = x[j, k];
                    }
                    nec = nearestneighbor.kdtreequeryrnn(centerstree, xx, r[layer * n] * rbffarradius, true, _params);
                    nearestneighbor.kdtreequeryresultsx(centerstree, ref cx, _params);
                    nearestneighbor.kdtreequeryresultstags(centerstree, ref centerstags, _params);
                    for (k = 0; k <= nec - 1; k++)
                    {
                        yval = yval + omega[centerstags[k]] * Math.Exp(-((math.sqr(xx[0] - cx[k, 0]) + math.sqr(xx[1] - cx[k, 1]) + math.sqr(xx[2] - cx[k, 2])) / math.sqr(r[layer * n + centerstags[k]])));
                    }
                    y[j, i] = y[j, i] - yval;
                }

                //
                // write Omega in out parameter W
                //
                for (j = 0; j <= n - 1; j++)
                {
                    w[layer * n + j, i] = omega[j];
                }
                iterationscount = iterationscount + lsqrrep.iterationscount;
                nmv = nmv + lsqrrep.nmv;
            }

            //
            // Calculate maximum residual before adding new layer.
            // This value is not used by algorithm, the only purpose is to make debugging easier.
            //
            rmaxafter = 0.0;
            for (j = 0; j <= n - 1; j++)
            {
                for (i = 0; i <= ny - 1; i++)
                {
                    rmaxafter = Math.Max(rmaxafter, Math.Abs(y[j, i]));
                }
            }
        }
        info = 1;
    }


}
