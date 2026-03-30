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

public class rbf
{
    /*************************************************************************
    Buffer object which is used  to  perform  RBF  model  calculation  in  the
    multithreaded mode (multiple threads working with same RBF object).

    This object should be created with RBFCreateCalcBuffer().
    *************************************************************************/
    public class rbfcalcbuffer : apobject
    {
        public int modelversion;
        public rbfv1.rbfv1calcbuffer bufv1;
        public rbfv2.rbfv2calcbuffer bufv2;
        public rbfv3.rbfv3calcbuffer bufv3;
        public double[] x;
        public double[] y;
        public double[] dy;
        public rbfcalcbuffer()
        {
            init();
        }
        public override void init()
        {
            bufv1 = new rbfv1.rbfv1calcbuffer();
            bufv2 = new rbfv2.rbfv2calcbuffer();
            bufv3 = new rbfv3.rbfv3calcbuffer();
            x = new double[0];
            y = new double[0];
            dy = new double[0];
        }
        public override apobject make_copy()
        {
            rbfcalcbuffer _result = new rbfcalcbuffer();
            _result.modelversion = modelversion;
            _result.bufv1 = (rbfv1.rbfv1calcbuffer)bufv1.make_copy();
            _result.bufv2 = (rbfv2.rbfv2calcbuffer)bufv2.make_copy();
            _result.bufv3 = (rbfv3.rbfv3calcbuffer)bufv3.make_copy();
            _result.x = (double[])x.Clone();
            _result.y = (double[])y.Clone();
            _result.dy = (double[])dy.Clone();
            return _result;
        }
    };


    /*************************************************************************
    RBF model.

    Never try to directly work with fields of this object - always use  ALGLIB
    functions to use this object.
    *************************************************************************/
    public class rbfmodel : apobject
    {
        public int nx;
        public int ny;
        public int modelversion;
        public rbfv1.rbfv1model model1;
        public rbfv2.rbfv2model model2;
        public rbfv3.rbfv3model model3;
        public rbfcalcbuffer calcbuf;
        public double lambdav;
        public double radvalue;
        public double radzvalue;
        public int nlayers;
        public int aterm;
        public int algorithmtype;
        public int rbfprofile;
        public int bftype;
        public double bfparam;
        public double epsort;
        public double epserr;
        public int maxits;
        public double v3tol;
        public int nnmaxits;
        public int n;
        public double[,] x;
        public double[,] y;
        public bool hasscale;
        public double[] s;
        public double fastevaltol;
        public int progress10000;
        public bool terminationrequest;
        public rbfmodel()
        {
            init();
        }
        public override void init()
        {
            model1 = new rbfv1.rbfv1model();
            model2 = new rbfv2.rbfv2model();
            model3 = new rbfv3.rbfv3model();
            calcbuf = new rbfcalcbuffer();
            x = new double[0, 0];
            y = new double[0, 0];
            s = new double[0];
        }
        public override apobject make_copy()
        {
            rbfmodel _result = new rbfmodel();
            _result.nx = nx;
            _result.ny = ny;
            _result.modelversion = modelversion;
            _result.model1 = (rbfv1.rbfv1model)model1.make_copy();
            _result.model2 = (rbfv2.rbfv2model)model2.make_copy();
            _result.model3 = (rbfv3.rbfv3model)model3.make_copy();
            _result.calcbuf = (rbfcalcbuffer)calcbuf.make_copy();
            _result.lambdav = lambdav;
            _result.radvalue = radvalue;
            _result.radzvalue = radzvalue;
            _result.nlayers = nlayers;
            _result.aterm = aterm;
            _result.algorithmtype = algorithmtype;
            _result.rbfprofile = rbfprofile;
            _result.bftype = bftype;
            _result.bfparam = bfparam;
            _result.epsort = epsort;
            _result.epserr = epserr;
            _result.maxits = maxits;
            _result.v3tol = v3tol;
            _result.nnmaxits = nnmaxits;
            _result.n = n;
            _result.x = (double[,])x.Clone();
            _result.y = (double[,])y.Clone();
            _result.hasscale = hasscale;
            _result.s = (double[])s.Clone();
            _result.fastevaltol = fastevaltol;
            _result.progress10000 = progress10000;
            _result.terminationrequest = terminationrequest;
            return _result;
        }
    };


    /*************************************************************************
    RBF solution report:
    * TerminationType   -   termination type, positive values - success,
                            non-positive - failure.
                            
    Fields which are set by modern RBF solvers (hierarchical):
    * RMSError          -   root-mean-square error; NAN for old solvers (ML, QNN)
    * MaxError          -   maximum error; NAN for old solvers (ML, QNN)
    *************************************************************************/
    public class rbfreport : apobject
    {
        public double rmserror;
        public double maxerror;
        public int arows;
        public int acols;
        public int annz;
        public int iterationscount;
        public int nmv;
        public int terminationtype;
        public rbfreport()
        {
            init();
        }
        public override void init()
        {
        }
        public override apobject make_copy()
        {
            rbfreport _result = new rbfreport();
            _result.rmserror = rmserror;
            _result.maxerror = maxerror;
            _result.arows = arows;
            _result.acols = acols;
            _result.annz = annz;
            _result.iterationscount = iterationscount;
            _result.nmv = nmv;
            _result.terminationtype = terminationtype;
            return _result;
        }
    };




    public const double eps = 1.0E-6;
    public const double rbffarradius = 6;
    public const int rbffirstversion = 0;
    public const int rbfversion2 = 2;
    public const int rbfversion3 = 3;


    /*************************************************************************
    This function creates RBF  model  for  a  scalar (NY=1)  or  vector (NY>1)
    function in a NX-dimensional space (NX>=1).

    Newly created model is empty. It can be used for interpolation right after
    creation, but it just returns zeros. You have to add points to the  model,
    tune interpolation settings, and then  call  model  construction  function
    rbfbuildmodel() which will update model according to your specification.

    USAGE:
    1. User creates model with rbfcreate()
    2. User adds dataset with rbfsetpoints() or rbfsetpointsandscales()
    3. User selects RBF solver by calling:
       * rbfsetalgohierarchical() - for a HRBF solver,  a  hierarchical large-
         scale Gaussian RBFs  (works  well  for  uniformly  distributed  point
         clouds, but may fail when the data are non-uniform; use other solvers
         below in such cases)
       * rbfsetalgothinplatespline() - for a large-scale DDM-RBF  solver  with
         thin plate spline basis function being used
       * rbfsetalgobiharmonic() -  for  a  large-scale  DDM-RBF  solver   with
         biharmonic basis function being used
       * rbfsetalgomultiquadricauto() -  for a large-scale DDM-RBF solver with
         multiquadric basis function being used (automatic  selection  of  the
         scale parameter Alpha)
       * rbfsetalgomultiquadricmanual() -  for a  large-scale  DDM-RBF  solver
         with multiquadric basis function being used (manual selection  of the
         scale parameter Alpha)
    4. (OPTIONAL) User chooses polynomial term by calling:
       * rbflinterm() to set linear term (default)
       * rbfconstterm() to set constant term
       * rbfzeroterm() to set zero term
    5. User calls rbfbuildmodel() function which rebuilds model  according  to
       the specification
       
    INPUT PARAMETERS:
        NX      -   dimension of the space, NX>=1
        NY      -   function dimension, NY>=1

    OUTPUT PARAMETERS:
        S       -   RBF model (initially equals to zero)

    NOTE 1: memory requirements. RBF models require amount of memory  which is
            proportional  to the number of data points. Some additional memory
            is allocated during model construction, but most of this memory is
            freed after the model  coefficients  are   calculated.  Amount  of
            this additional memory depends  on  model  construction  algorithm
            being used.

      -- ALGLIB --
         Copyright 13.12.2011, 20.06.2016 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfcreate(int nx,
        int ny,
        rbfmodel s,
        xparams _params)
    {
        ap.assert(nx >= 1, "RBFCreate: NX<1");
        ap.assert(ny >= 1, "RBFCreate: NY<1");
        s.nx = nx;
        s.ny = ny;
        rbfpreparenonserializablefields(s, _params);

        //
        // Select default model version according to NX.
        //
        // The idea is that when we call this function with NX=2 or NX=3, backward
        // compatible dummy (zero) V1 model is created, so serialization produces
        // model which are compatible with pre-3.11 ALGLIB.
        //
        initializev1(nx, ny, s.model1, _params);
        initializev2(nx, ny, s.model2, _params);
        initializev3(nx, ny, s.model3, _params);
        if (nx == 2 || nx == 3)
        {
            s.modelversion = 1;
        }
        else
        {
            s.modelversion = 2;
        }

        //
        // Report fields
        //
        s.progress10000 = 0;
        s.terminationrequest = false;

        //
        // Prepare buffers
        //
        rbfcreatecalcbuffer(s, s.calcbuf, _params);
    }


    /*************************************************************************
    This function creates buffer  structure  which  can  be  used  to  perform
    parallel  RBF  model  evaluations  (with  one  RBF  model  instance  being
    used from multiple threads, as long as  different  threads  use  different
    instances of the buffer).

    This buffer object can be used with  rbftscalcbuf()  function  (here  "ts"
    stands for "thread-safe", "buf" is a suffix which denotes  function  which
    reuses previously allocated output space).

    A buffer creation function (this function) is also thread-safe.  I.e.  you
    may safely create multiple buffers for the same  RBF  model  from multiple
    threads.

    NOTE: the  buffer  object  is  just  a  collection of several preallocated
          dynamic arrays and precomputed values. If you  delete  its  "parent"
          RBF model when the buffer is still alive, nothing  bad  will  happen
          (no dangling pointers or resource leaks).  The  buffer  will  simply
          become useless.

    How to use it:
    * create RBF model structure with rbfcreate()
    * load data, tune parameters
    * call rbfbuildmodel()
    * call rbfcreatecalcbuffer(), once per thread working with RBF model  (you
      should call this function only AFTER call to rbfbuildmodel(), see  below
      for more information)
    * call rbftscalcbuf() from different threads,  with  each  thread  working
      with its own copy of buffer object.
    * it is recommended to reuse buffer as much  as  possible  because  buffer
      creation involves allocation of several large dynamic arrays.  It  is  a
      huge waste of resource to use it just once.

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
    public static void rbfcreatecalcbuffer(rbfmodel s,
        rbfcalcbuffer buf,
        xparams _params)
    {
        if (s.modelversion == 1)
        {
            buf.modelversion = 1;
            rbfv1.rbfv1createcalcbuffer(s.model1, buf.bufv1, _params);
            return;
        }
        if (s.modelversion == 2)
        {
            buf.modelversion = 2;
            rbfv2.rbfv2createcalcbuffer(s.model2, buf.bufv2, _params);
            return;
        }
        if (s.modelversion == 3)
        {
            buf.modelversion = 3;
            rbfv3.rbfv3createcalcbuffer(s.model3, buf.bufv3, _params);
            return;
        }
        ap.assert(false, "RBFCreateCalcBuffer: integrity check failed");
    }


    /*************************************************************************
    This function adds dataset.

    This function overrides results of the previous calls, i.e. multiple calls
    of this function will result in only the last set being added.

    IMPORTANT: ALGLIB version 3.11 and later allows you to specify  a  set  of
               per-dimension scales. Interpolation radii are multiplied by the
               scale vector. It may be useful if you have mixed spatio-temporal
               data (say, a set of 3D slices recorded at different times).
               You should call rbfsetpointsandscales() function  to  use  this
               feature.

    INPUT PARAMETERS:
        S       -   RBF model, initialized by rbfcreate() call.
        XY      -   points, array[N,NX+NY]. One row corresponds to  one  point
                    in the dataset. First NX elements  are  coordinates,  next
                    NY elements are function values. Array may  be larger than 
                    specified, in  this  case  only leading [N,NX+NY] elements 
                    will be used.
        N       -   number of points in the dataset

    After you've added dataset and (optionally) tuned algorithm  settings  you
    should call rbfbuildmodel() in order to build a model for you.

    NOTE: dataset added by this function is not saved during model serialization.
          MODEL ITSELF is serialized, but data used to build it are not.
          
          So, if you 1) add dataset to  empty  RBF  model,  2)  serialize  and
          unserialize it, then you will get an empty RBF model with no dataset
          being attached.
          
          From the other side, if you call rbfbuildmodel() between (1) and (2),
          then after (2) you will get your fully constructed RBF model  -  but
          again with no dataset attached, so subsequent calls to rbfbuildmodel()
          will produce empty model.
          

      -- ALGLIB --
         Copyright 13.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfsetpoints(rbfmodel s,
        double[,] xy,
        int n,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        ap.assert(n > 0, "RBFSetPoints: N<=0");
        ap.assert(ap.rows(xy) >= n, "RBFSetPoints: Rows(XY)<N");
        ap.assert(ap.cols(xy) >= s.nx + s.ny, "RBFSetPoints: Cols(XY)<NX+NY");
        ap.assert(apserv.apservisfinitematrix(xy, n, s.nx + s.ny, _params), "RBFSetPoints: XY contains infinite or NaN values!");
        s.n = n;
        s.hasscale = false;
        s.x = new double[s.n, s.nx];
        s.y = new double[s.n, s.ny];
        for (i = 0; i <= s.n - 1; i++)
        {
            for (j = 0; j <= s.nx - 1; j++)
            {
                s.x[i, j] = xy[i, j];
            }
            for (j = 0; j <= s.ny - 1; j++)
            {
                s.y[i, j] = xy[i, j + s.nx];
            }
        }
    }


    /*************************************************************************
    This function adds dataset and a vector of per-dimension scales.

    It may be useful if you have mixed spatio-temporal data - say, a set of 3D
    slices recorded at different times. Such data typically require  different
    RBF radii for spatial and temporal dimensions. ALGLIB solves this  problem
    by specifying single RBF radius, which is (optionally) multiplied  by  the
    scale vector.

    This function overrides results of the previous calls, i.e. multiple calls
    of this function will result in only the last set being added.

    IMPORTANT: only modern RBF algorithms  support  variable  scaling.  Legacy
               algorithms like RBF-ML or QNN algorithms  will  result  in   -3
               completion code being returned (incorrect algorithm).

    INPUT PARAMETERS:
        R       -   RBF model, initialized by rbfcreate() call.
        XY      -   points, array[N,NX+NY]. One row corresponds to  one  point
                    in the dataset. First NX elements  are  coordinates,  next
                    NY elements are function values. Array may  be larger than 
                    specified, in  this  case  only leading [N,NX+NY] elements 
                    will be used.
        N       -   number of points in the dataset
        S       -   array[NX], scale vector, S[i]>0.

    After you've added dataset and (optionally) tuned algorithm  settings  you
    should call rbfbuildmodel() in order to build a model for you.

    NOTE: dataset added by this function is not saved during model serialization.
          MODEL ITSELF is serialized, but data used to build it are not.
          
          So, if you 1) add dataset to  empty  RBF  model,  2)  serialize  and
          unserialize it, then you will get an empty RBF model with no dataset
          being attached.
          
          From the other side, if you call rbfbuildmodel() between (1) and (2),
          then after (2) you will get your fully constructed RBF model  -  but
          again with no dataset attached, so subsequent calls to rbfbuildmodel()
          will produce empty model.
          

      -- ALGLIB --
         Copyright 20.06.2016 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfsetpointsandscales(rbfmodel r,
        double[,] xy,
        int n,
        double[] s,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        ap.assert(n > 0, "RBFSetPointsAndScales: N<=0");
        ap.assert(ap.rows(xy) >= n, "RBFSetPointsAndScales: Rows(XY)<N");
        ap.assert(ap.cols(xy) >= r.nx + r.ny, "RBFSetPointsAndScales: Cols(XY)<NX+NY");
        ap.assert(ap.len(s) >= r.nx, "RBFSetPointsAndScales: Length(S)<NX");
        r.n = n;
        r.hasscale = true;
        r.x = new double[r.n, r.nx];
        r.y = new double[r.n, r.ny];
        for (i = 0; i <= r.n - 1; i++)
        {
            for (j = 0; j <= r.nx - 1; j++)
            {
                r.x[i, j] = xy[i, j];
            }
            for (j = 0; j <= r.ny - 1; j++)
            {
                r.y[i, j] = xy[i, j + r.nx];
            }
        }
        r.s = new double[r.nx];
        for (i = 0; i <= r.nx - 1; i++)
        {
            ap.assert(math.isfinite(s[i]), "RBFSetPointsAndScales: S[i] is not finite number");
            ap.assert((double)(s[i]) > (double)(0), "RBFSetPointsAndScales: S[i]<=0");
            r.s[i] = s[i];
        }
    }


    /*************************************************************************
    DEPRECATED: this function is deprecated. ALGLIB  includes  new  RBF  model
                construction algorithms: DDM-RBF (since version 3.19) and HRBF
                (since version 3.11).

      -- ALGLIB --
         Copyright 13.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfsetalgoqnn(rbfmodel s,
        double q,
        double z,
        xparams _params)
    {
        ap.assert(math.isfinite(q), "RBFSetAlgoQNN: Q is infinite or NAN");
        ap.assert((double)(q) > (double)(0), "RBFSetAlgoQNN: Q<=0");
        ap.assert(math.isfinite(z), "RBFSetAlgoQNN: Z is infinite or NAN");
        ap.assert((double)(z) > (double)(0), "RBFSetAlgoQNN: Z<=0");
        s.radvalue = q;
        s.radzvalue = z;
        s.algorithmtype = 1;
    }


    /*************************************************************************
    DEPRECATED: this function is deprecated. ALGLIB  includes  new  RBF  model
                construction algorithms: DDM-RBF (since version 3.19) and HRBF
                (since version 3.11).
       
      -- ALGLIB --
         Copyright 02.03.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfsetalgomultilayer(rbfmodel s,
        double rbase,
        int nlayers,
        double lambdav,
        xparams _params)
    {
        ap.assert(math.isfinite(rbase), "RBFSetAlgoMultiLayer: RBase is infinite or NaN");
        ap.assert((double)(rbase) > (double)(0), "RBFSetAlgoMultiLayer: RBase<=0");
        ap.assert(nlayers >= 0, "RBFSetAlgoMultiLayer: NLayers<0");
        ap.assert(math.isfinite(lambdav), "RBFSetAlgoMultiLayer: LambdaV is infinite or NAN");
        ap.assert((double)(lambdav) >= (double)(0), "RBFSetAlgoMultiLayer: LambdaV<0");
        s.radvalue = rbase;
        s.nlayers = nlayers;
        s.algorithmtype = 2;
        s.lambdav = lambdav;
    }


    /*************************************************************************
    This function chooses HRBF solver, a 2nd version of ALGLIB RBFs.

    This  algorithm is called Hierarchical RBF. It  similar  to  its  previous
    incarnation, RBF-ML, i.e.  it  also  builds  a  sequence  of  models  with
    decreasing radii. However, it uses more economical way of  building  upper
    layers (ones with large radii), which results in faster model construction
    and evaluation, as well as smaller memory footprint during construction.

    This algorithm has following important features:
    * ability to handle millions of points
    * controllable smoothing via nonlinearity penalization
    * support for specification of per-dimensional  radii  via  scale  vector,
      which is set by means of rbfsetpointsandscales() function. This  feature
      is useful if you solve  spatio-temporal  interpolation  problems,  where
      different radii are required for spatial and temporal dimensions.

    Running times are roughly proportional to:
    * N*log(N)*NLayers - for the model construction
    * N*NLayers - for the model evaluation
    You may see that running time does not depend on search radius  or  points
    density, just on the number of layers in the hierarchy.

    INPUT PARAMETERS:
        S       -   RBF model, initialized by rbfcreate() call
        RBase   -   RBase parameter, RBase>0
        NLayers -   NLayers parameter, NLayers>0, recommended value  to  start
                    with - about 5.
        LambdaNS-   >=0, nonlinearity penalty coefficient, negative values are
                    not allowed. This parameter adds controllable smoothing to
                    the problem, which may reduce noise. Specification of non-
                    zero lambda means that in addition to fitting error solver
                    will  also  minimize   LambdaNS*|S''(x)|^2  (appropriately
                    generalized to multiple dimensions.
                    
                    Specification of exactly zero value means that no  penalty
                    is added  (we  do  not  even  evaluate  matrix  of  second
                    derivatives which is necessary for smoothing).
                    
                    Calculation of nonlinearity penalty is costly - it results
                    in  several-fold  increase  of  model  construction  time.
                    Evaluation time remains the same.
                    
                    Optimal  lambda  is  problem-dependent and requires  trial
                    and  error.  Good  value to  start  from  is  1e-5...1e-6,
                    which corresponds to slightly noticeable smoothing  of the
                    function.  Value  1e-2  usually  means  that  quite  heavy
                    smoothing is applied.

    TUNING ALGORITHM

    In order to use this algorithm you have to choose three parameters:
    * initial radius RBase
    * number of layers in the model NLayers
    * penalty coefficient LambdaNS

    Initial radius is easy to choose - you can pick any number  several  times
    larger  than  the  average  distance between points. Algorithm won't break
    down if you choose radius which is too large (model construction time will
    increase, but model will be built correctly).

    Choose such number of layers that RLast=RBase/2^(NLayers-1)  (radius  used
    by  the  last  layer)  will  be  smaller than the typical distance between
    points.  In  case  model  error  is  too large, you can increase number of
    layers.  Having  more  layers  will make model construction and evaluation
    proportionally slower, but it will allow you to have model which precisely
    fits your data. From the other side, if you want to  suppress  noise,  you
    can DECREASE number of layers to make your model less flexible (or specify
    non-zero LambdaNS).

    TYPICAL ERRORS

    1. Using too small number of layers - RBF models with large radius are not
       flexible enough to reproduce small variations in the  target  function.
       You  need  many  layers  with  different radii, from large to small, in
       order to have good model.

    2. Using  initial  radius  which  is  too  small.  You will get model with
       "holes" in the areas which are too far away from interpolation centers.
       However, algorithm will work correctly (and quickly) in this case.

      -- ALGLIB --
         Copyright 20.06.2016 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfsetalgohierarchical(rbfmodel s,
        double rbase,
        int nlayers,
        double lambdans,
        xparams _params)
    {
        ap.assert(math.isfinite(rbase), "RBFSetAlgoHierarchical: RBase is infinite or NaN");
        ap.assert((double)(rbase) > (double)(0), "RBFSetAlgoHierarchical: RBase<=0");
        ap.assert(nlayers >= 0, "RBFSetAlgoHierarchical: NLayers<0");
        ap.assert(math.isfinite(lambdans) && (double)(lambdans) >= (double)(0), "RBFSetAlgoHierarchical: LambdaNS<0 or infinite");
        s.radvalue = rbase;
        s.nlayers = nlayers;
        s.algorithmtype = 3;
        s.lambdav = lambdans;
    }


    /*************************************************************************
    This function chooses a thin plate  spline  DDM-RBF  solver,  a  fast  RBF
    solver with f(r)=r^2*ln(r) basis function.

    This algorithm has following important features:
    * easy setup - no tunable parameters
    * C1 continuous RBF model (gradient is defined everywhere, but Hessian  is
      undefined at nodes), high-quality interpolation
    * fast  model construction algorithm with O(N) memory and  O(N^2)  running
      time requirements. Hundreds of thousands of points can be  handled  with
      this algorithm.
    * controllable smoothing via optional nonlinearity penalty

    INPUT PARAMETERS:
        S       -   RBF model, initialized by rbfcreate() call
        LambdaV -   smoothing parameter, LambdaV>=0, defaults to 0.0:
                    * LambdaV=0 means that no smoothing is applied,  i.e.  the
                      spline tries to pass through all dataset points exactly
                    * LambdaV>0 means that a smoothing thin  plate  spline  is
                      built, with larger LambdaV corresponding to models  with
                      less nonlinearities. Smoothing spline reproduces  target
                      values at nodes with small error; from the  other  side,
                      it is much more stable.
                      Recommended values:
                      * 1.0E-6 for minimal stability improving smoothing
                      * 1.0E-3 a good value to start experiments; first results
                        are visible
                      * 1.0 for strong smoothing

    IMPORTANT: this model construction algorithm was introduced in ALGLIB 3.19
               and  produces  models  which  are  INCOMPATIBLE  with  previous
               versions of ALGLIB. You can  not  unserialize  models  produced
               with this function in ALGLIB 3.18 or earlier.
               
    NOTE:      polyharmonic RBFs, including thin plate splines,  are  somewhat
               slower than compactly supported RBFs built with  HRBF algorithm
               due to the fact that non-compact basis function does not vanish
               far away from the nodes. From the other side, polyharmonic RBFs
               often produce much better results than HRBFs.

    NOTE:      this algorithm supports specification of per-dimensional  radii
               via scale vector, which is set by means of rbfsetpointsandscales()
               function. This feature is useful if  you solve  spatio-temporal
               interpolation problems where different radii are  required  for
               spatial and temporal dimensions.

      -- ALGLIB --
         Copyright 12.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfsetalgothinplatespline(rbfmodel s,
        double lambdav,
        xparams _params)
    {
        ap.assert(math.isfinite(lambdav), "RBFSetAlgoThinPlateSpline: LambdaV is not finite number");
        ap.assert((double)(lambdav) >= (double)(0), "RBFSetAlgoThinPlateSpline: LambdaV is negative");
        s.algorithmtype = 4;
        s.bftype = 2;
        s.bfparam = 0;
        s.lambdav = lambdav;
    }


    /*************************************************************************
    This function chooses a multiquadric DDM-RBF solver,  a  fast  RBF  solver
    with f(r)=sqrt(r^2+Alpha^2) as a basis function,  with  manual  choice  of
    the scale parameter Alpha.

    This algorithm has following important features:
    * C2 continuous RBF model (when Alpha>0 is used; for Alpha=0 the model  is
      merely C0 continuous)
    * fast  model construction algorithm with O(N) memory and  O(N^2)  running
      time requirements. Hundreds of thousands of points can be  handled  with
      this algorithm.
    * controllable smoothing via optional nonlinearity penalty
      
    One important point is that  this  algorithm  includes  tunable  parameter
    Alpha, which should be carefully chosen. Selecting too  large  value  will
    result in extremely badly  conditioned  problems  (interpolation  accuracy
    may degrade up to complete breakdown) whilst selecting too small value may
    produce models that are precise but nearly nonsmooth at the nodes.

    Good value to  start  from  is  mean  distance  between  nodes. Generally,
    choosing too small Alpha is better than choosing too large - in the former
    case you still have model that reproduces target values at the nodes.

    In most cases, better option is to choose good Alpha automatically - it is
    done by another version of the same algorithm that is activated by calling
    rbfsetalgomultiquadricauto() method.

    INPUT PARAMETERS:
        S       -   RBF model, initialized by rbfcreate() call
        Alpha   -   basis function parameter, Alpha>=0:
                    * Alpha>0  means that multiquadric algorithm is used which
                      produces C2-continuous RBF model
                    * Alpha=0  means that the multiquadric kernel  effectively
                      becomes a biharmonic one: f=r. As a  result,  the  model
                      becomes nonsmooth at nodes, and hence is C0 continuous
        LambdaV -   smoothing parameter, LambdaV>=0, defaults to 0.0:
                    * LambdaV=0 means that no smoothing is applied,  i.e.  the
                      spline tries to pass through all dataset points exactly
                    * LambdaV>0 means that a multiquadric spline is built with
                      larger  LambdaV   corresponding   to  models  with  less
                      nonlinearities.  Smoothing   spline   reproduces  target
                      values at nodes with small error; from the  other  side,
                      it is much more stable.
                      Recommended values:
                      * 1.0E-6 for minimal stability improving smoothing
                      * 1.0E-3 a good value to start experiments; first results
                        are visible
                      * 1.0 for strong smoothing

    IMPORTANT: this model construction algorithm was introduced in ALGLIB 3.19
               and  produces  models  which  are  INCOMPATIBLE  with  previous
               versions of ALGLIB. You can  not  unserialize  models  produced
               with this function in ALGLIB 3.18 or earlier.
               
    NOTE:      polyharmonic RBFs, including thin plate splines,  are  somewhat
               slower than compactly supported RBFs built with  HRBF algorithm
               due to the fact that non-compact basis function does not vanish
               far away from the nodes. From the other side, polyharmonic RBFs
               often produce much better results than HRBFs.

    NOTE:      this algorithm supports specification of per-dimensional  radii
               via scale vector, which is set by means of rbfsetpointsandscales()
               function. This feature is useful if  you solve  spatio-temporal
               interpolation problems where different radii are  required  for
               spatial and temporal dimensions.

      -- ALGLIB --
         Copyright 12.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfsetalgomultiquadricmanual(rbfmodel s,
        double alpha,
        double lambdav,
        xparams _params)
    {
        ap.assert(math.isfinite(alpha), "RBFSetAlgoMultiquadricManual: Alpha is infinite or NAN");
        ap.assert((double)(alpha) >= (double)(0), "RBFSetAlgoMultiquadricManual: Alpha<0");
        ap.assert(math.isfinite(lambdav), "RBFSetAlgoMultiquadricManual: LambdaV is not finite number");
        ap.assert((double)(lambdav) >= (double)(0), "RBFSetAlgoMultiquadricManual: LambdaV is negative");
        s.algorithmtype = 4;
        s.bftype = 1;
        s.bfparam = alpha;
        s.lambdav = lambdav;
    }


    /*************************************************************************
    This function chooses a multiquadric DDM-RBF solver,  a  fast  RBF  solver
    with f(r)=sqrt(r^2+Alpha^2)  as  a  basis  function,  with   Alpha   being
    automatically determined.

    This algorithm has following important features:
    * easy setup - no need to tune Alpha, good value is automatically assigned
    * C2 continuous RBF model
    * fast  model construction algorithm with O(N) memory and  O(N^2)  running
      time requirements. Hundreds of thousands of points can be  handled  with
      this algorithm.
    * controllable smoothing via optional nonlinearity penalty

    This algorithm automatically selects Alpha  as  a  mean  distance  to  the
    nearest neighbor (ignoring neighbors that are too close).

    INPUT PARAMETERS:
        S       -   RBF model, initialized by rbfcreate() call
        LambdaV -   smoothing parameter, LambdaV>=0, defaults to 0.0:
                    * LambdaV=0 means that no smoothing is applied,  i.e.  the
                      spline tries to pass through all dataset points exactly
                    * LambdaV>0 means that a multiquadric spline is built with
                      larger  LambdaV   corresponding   to  models  with  less
                      nonlinearities.  Smoothing   spline   reproduces  target
                      values at nodes with small error; from the  other  side,
                      it is much more stable.
                      Recommended values:
                      * 1.0E-6 for minimal stability improving smoothing
                      * 1.0E-3 a good value to start experiments; first results
                        are visible
                      * 1.0 for strong smoothing

    IMPORTANT: this model construction algorithm was introduced in ALGLIB 3.19
               and  produces  models  which  are  INCOMPATIBLE  with  previous
               versions of ALGLIB. You can  not  unserialize  models  produced
               with this function in ALGLIB 3.18 or earlier.
               
    NOTE:      polyharmonic RBFs, including thin plate splines,  are  somewhat
               slower than compactly supported RBFs built with  HRBF algorithm
               due to the fact that non-compact basis function does not vanish
               far away from the nodes. From the other side, polyharmonic RBFs
               often produce much better results than HRBFs.

    NOTE:      this algorithm supports specification of per-dimensional  radii
               via scale vector, which is set by means of rbfsetpointsandscales()
               function. This feature is useful if  you solve  spatio-temporal
               interpolation problems where different radii are  required  for
               spatial and temporal dimensions.

      -- ALGLIB --
         Copyright 12.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfsetalgomultiquadricauto(rbfmodel s,
        double lambdav,
        xparams _params)
    {
        ap.assert(math.isfinite(lambdav), "RBFSetAlgoMultiquadricAuto: LambdaV is not finite number");
        ap.assert((double)(lambdav) >= (double)(0), "RBFSetAlgoMultiquadricAuto: LambdaV is negative");
        s.algorithmtype = 4;
        s.bftype = 1;
        s.bfparam = -1.0;
        s.lambdav = lambdav;
    }


    /*************************************************************************
    This  function  chooses  a  biharmonic DDM-RBF solver, a fast  RBF  solver
    with f(r)=r as a basis function.

    This algorithm has following important features:
    * no tunable parameters
    * C0 continuous RBF model (the model has discontinuous derivatives at  the
      interpolation nodes)
    * fast model construction algorithm with O(N) memory and O(N*logN) running
      time requirements. Hundreds of thousands of points can be  handled  with
      this algorithm.
    * accelerated evaluation using far field expansions  (aka  fast multipoles
      method) is supported. See rbffastcalc() for more information.
    * controllable smoothing via optional nonlinearity penalty

    INPUT PARAMETERS:
        S       -   RBF model, initialized by rbfcreate() call
        LambdaV -   smoothing parameter, LambdaV>=0, defaults to 0.0:
                    * LambdaV=0 means that no smoothing is applied,  i.e.  the
                      spline tries to pass through all dataset points exactly
                    * LambdaV>0 means that a multiquadric spline is built with
                      larger  LambdaV   corresponding   to  models  with  less
                      nonlinearities.  Smoothing   spline   reproduces  target
                      values at nodes with small error; from the  other  side,
                      it is much more stable.
                      Recommended values:
                      * 1.0E-6 for minimal stability improving smoothing
                      * 1.0E-3 a good value to start experiments; first results
                        are visible
                      * 1.0 for strong smoothing

    IMPORTANT: this model construction algorithm was introduced in ALGLIB 3.19
               and  produces  models  which  are  INCOMPATIBLE  with  previous
               versions of ALGLIB. You can  not  unserialize  models  produced
               with this function in ALGLIB 3.18 or earlier.
               
    NOTE:      polyharmonic RBFs, including thin plate splines,  are  somewhat
               slower than compactly supported RBFs built with  HRBF algorithm
               due to the fact that non-compact basis function does not vanish
               far away from the nodes. From the other side, polyharmonic RBFs
               often produce much better results than HRBFs.

    NOTE:      this algorithm supports specification of per-dimensional  radii
               via scale vector, which is set by means of rbfsetpointsandscales()
               function. This feature is useful if  you solve  spatio-temporal
               interpolation problems where different radii are  required  for
               spatial and temporal dimensions.

      -- ALGLIB --
         Copyright 12.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfsetalgobiharmonic(rbfmodel s,
        double lambdav,
        xparams _params)
    {
        ap.assert(math.isfinite(lambdav), "RBFSetAlgoBiharmonic: LambdaV is not finite number");
        ap.assert((double)(lambdav) >= (double)(0), "RBFSetAlgoBiharmonic: LambdaV is negative");
        s.algorithmtype = 4;
        s.bftype = 1;
        s.bfparam = 0;
        s.lambdav = lambdav;
    }


    /*************************************************************************
    This function sets linear term (model is a sum of radial  basis  functions
    plus linear polynomial). This function won't have effect until  next  call 
    to RBFBuildModel().

    Using linear term is a default option and it is the best one - it provides
    best convergence guarantees for all RBF model  types: legacy  RBF-QNN  and
    RBF-ML, Gaussian HRBFs and all types of DDM-RBF models.

    Other options, like constant or zero term, work for HRBFs,  almost  always
    work for DDM-RBFs but provide no stability  guarantees  in the latter case
    (e.g. the solver may fail on some carefully prepared problems).

    INPUT PARAMETERS:
        S       -   RBF model, initialized by RBFCreate() call

      -- ALGLIB --
         Copyright 13.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfsetlinterm(rbfmodel s,
        xparams _params)
    {
        s.aterm = 1;
    }


    /*************************************************************************
    This function sets constant term (model is a sum of radial basis functions
    plus constant).  This  function  won't  have  effect  until  next  call to 
    RBFBuildModel().

    IMPORTANT: thin plate splines require  polynomial term to be  linear,  not
               constant,  in  order  to  provide   interpolation   guarantees.
               Although  failures  are  exceptionally  rare,  some  small  toy
               problems may result in degenerate linear systems. Thus,  it  is
               advised to use linear term when one fits data with TPS.

    INPUT PARAMETERS:
        S       -   RBF model, initialized by RBFCreate() call

      -- ALGLIB --
         Copyright 13.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfsetconstterm(rbfmodel s,
        xparams _params)
    {
        s.aterm = 2;
    }


    /*************************************************************************
    This  function  sets  zero  term (model is a sum of radial basis functions 
    without polynomial term). This function won't have effect until next  call
    to RBFBuildModel().

    IMPORTANT: only  Gaussian  RBFs  (HRBF  algorithm)  provide  interpolation
               guarantees when no polynomial term is used.  Most  other  RBFs,
               including   biharmonic  splines,   thin   plate   splines   and
               multiquadrics, require at least constant term  (biharmonic  and
               multiquadric) or linear one (thin plate splines)  in  order  to
               guarantee non-degeneracy of linear systems being solved.
               
               Although  failures  are  exceptionally  rare,  some  small  toy
               problems still may result in degenerate linear systems. Thus,it
               is advised to use constant/linear term, unless one is 100% sure
               that he needs zero term.

    INPUT PARAMETERS:
        S       -   RBF model, initialized by RBFCreate() call

      -- ALGLIB --
         Copyright 13.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfsetzeroterm(rbfmodel s,
        xparams _params)
    {
        s.aterm = 3;
    }


    /*************************************************************************
    This function sets basis function type, which can be:
    * 0 for classic Gaussian
    * 1 for fast and compact bell-like basis function, which  becomes  exactly
      zero at distance equal to 3*R (default option).

    INPUT PARAMETERS:
        S       -   RBF model, initialized by RBFCreate() call
        BF      -   basis function type:
                    * 0 - classic Gaussian
                    * 1 - fast and compact one

      -- ALGLIB --
         Copyright 01.02.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfsetv2bf(rbfmodel s,
        int bf,
        xparams _params)
    {
        ap.assert(bf == 0 || bf == 1, "RBFSetV2Its: BF<>0 and BF<>1");
        s.model2.basisfunction = bf;
    }


    /*************************************************************************
    This function sets stopping criteria of the underlying linear  solver  for
    hierarchical (version 2) RBF constructor.

    INPUT PARAMETERS:
        S       -   RBF model, initialized by RBFCreate() call
        MaxIts  -   this criterion will stop algorithm after MaxIts iterations.
                    Typically a few hundreds iterations is required,  with 400
                    being a good default value to start experimentation.
                    Zero value means that default value will be selected.

      -- ALGLIB --
         Copyright 01.02.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfsetv2its(rbfmodel s,
        int maxits,
        xparams _params)
    {
        ap.assert(maxits >= 0, "RBFSetV2Its: MaxIts is negative");
        s.model2.maxits = maxits;
    }


    /*************************************************************************
    This function sets support radius parameter  of  hierarchical  (version 2)
    RBF constructor.

    Hierarchical RBF model achieves great speed-up  by removing from the model
    excessive (too dense) nodes. Say, if you have RBF radius equal to 1 meter,
    and two nodes are just 1 millimeter apart, you  may  remove  one  of  them
    without reducing model quality.

    Support radius parameter is used to justify which points need removal, and
    which do not. If two points are less than  SUPPORT_R*CUR_RADIUS  units  of
    distance apart, one of them is removed from the model. The larger  support
    radius  is, the faster model  construction  AND  evaluation are.  However,
    too large values result in "bumpy" models.

    INPUT PARAMETERS:
        S       -   RBF model, initialized by RBFCreate() call
        R       -   support radius coefficient, >=0.
                    Recommended values are [0.1,0.4] range, with 0.1 being
                    default value.

      -- ALGLIB --
         Copyright 01.02.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfsetv2supportr(rbfmodel s,
        double r,
        xparams _params)
    {
        ap.assert(math.isfinite(r), "RBFSetV2SupportR: R is not finite");
        ap.assert((double)(r) >= (double)(0), "RBFSetV2SupportR: R<0");
        s.model2.supportr = r;
    }


    /*************************************************************************
    This function sets desired accuracy for a version 3 RBF model.

    As of ALGLIB 3.20.0, version 3 models include biharmonic RBFs, thin  plate
    splines, multiquadrics.

    Version 3 models are fit  with  specialized  domain  decomposition  method
    which splits problem into smaller  chunks.  Models  with  size  less  than
    the DDM chunk size are computed nearly exactly in one step. Larger  models
    are built with an iterative linear solver. This function controls accuracy
    of the solver.

    INPUT PARAMETERS:
        S       -   RBF model, initialized by RBFCreate() call
        TOL     -   desired precision:
                    * must be non-negative
                    * should be somewhere between 0.001 and 0.000001
                    * values higher than 0.001 make little sense   -  you  may
                      lose a lot of precision with no performance gains.
                    * values below 1E-6 usually require too much time to converge,
                      so they are silenly replaced by a 1E-6 cutoff value. Thus,
                      zero can be used to denote 'maximum precision'.

      -- ALGLIB --
         Copyright 01.10.2022 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfsetv3tol(rbfmodel s,
        double tol,
        xparams _params)
    {
        ap.assert(math.isfinite(tol) && (double)(tol) >= (double)(0), "RBFSetV3TOL: TOL is negative or infinite");
        s.v3tol = tol;
    }


    /*************************************************************************
    This function sets stopping criteria of the underlying linear solver.

    INPUT PARAMETERS:
        S       -   RBF model, initialized by RBFCreate() call
        EpsOrt  -   orthogonality stopping criterion, EpsOrt>=0. Algorithm will
                    stop when ||A'*r||<=EpsOrt where A' is a transpose of  the 
                    system matrix, r is a residual vector.
                    Recommended value of EpsOrt is equal to 1E-6.
                    This criterion will stop algorithm when we have "bad fit"
                    situation, i.e. when we should stop in a point with large,
                    nonzero residual.
        EpsErr  -   residual stopping  criterion.  Algorithm  will  stop  when
                    ||r||<=EpsErr*||b||, where r is a residual vector, b is  a
                    right part of the system (function values).
                    Recommended value of EpsErr is equal to 1E-3 or 1E-6.
                    This  criterion  will  stop  algorithm  in  a  "good  fit" 
                    situation when we have near-zero residual near the desired
                    solution.
        MaxIts  -   this criterion will stop algorithm after MaxIts iterations.
                    It should be used for debugging purposes only!
                    Zero MaxIts means that no limit is placed on the number of
                    iterations.

    We  recommend  to  set  moderate  non-zero  values   EpsOrt   and   EpsErr 
    simultaneously. Values equal to 10E-6 are good to start with. In case  you
    need high performance and do not need high precision ,  you  may  decrease
    EpsErr down to 0.001. However, we do not recommend decreasing EpsOrt.

    As for MaxIts, we recommend to leave it zero unless you know what you do.

    NOTE: this   function  has   some   serialization-related  subtleties.  We
          recommend you to study serialization examples from ALGLIB  Reference
          Manual if you want to perform serialization of your models.

      -- ALGLIB --
         Copyright 13.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfsetcond(rbfmodel s,
        double epsort,
        double epserr,
        int maxits,
        xparams _params)
    {
        ap.assert(math.isfinite(epsort) && (double)(epsort) >= (double)(0), "RBFSetCond: EpsOrt is negative, INF or NAN");
        ap.assert(math.isfinite(epserr) && (double)(epserr) >= (double)(0), "RBFSetCond: EpsB is negative, INF or NAN");
        ap.assert(maxits >= 0, "RBFSetCond: MaxIts is negative");
        if (((double)(epsort) == (double)(0) && (double)(epserr) == (double)(0)) && maxits == 0)
        {
            s.epsort = eps;
            s.epserr = eps;
            s.maxits = 0;
        }
        else
        {
            s.epsort = epsort;
            s.epserr = epserr;
            s.maxits = maxits;
        }
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
                             interpolation  aborted;  only  QNN  returns  this
                             error   code, other  algorithms  can  handle non-
                             distinct nodes.
                      * -4 - nonconvergence of the internal SVD solver
                      * -3   incorrect model construction algorithm was chosen:
                             QNN or RBF-ML, combined with one of the incompatible
                             features:
                             * NX=1 or NX>3
                             * points with per-dimension scales.
                      *  1 - successful termination
                      *  8 - a termination request was submitted via
                             rbfrequesttermination() function.
                    
                    Fields which are set only by modern RBF solvers (hierarchical
                    or nonnegative; older solvers like QNN and ML initialize these
                    fields by NANs):
                    * rep.rmserror - root-mean-square error at nodes
                    * rep.maxerror - maximum error at nodes
                    
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
    public static void rbfbuildmodel(rbfmodel s,
        rbfreport rep,
        xparams _params)
    {
        rbfv1.rbfv1report rep1 = new rbfv1.rbfv1report();
        rbfv2.rbfv2report rep2 = new rbfv2.rbfv2report();
        rbfv3.rbfv3report rep3 = new rbfv3.rbfv3report();
        double[,] x3 = new double[0, 0];
        double[] scalevec = new double[0];
        int i = 0;
        int v3bftype = 0;
        double v3bfparam = 0;
        int curalgorithmtype = 0;


        //
        // Clean fields prior to processing
        //
        clearreportfields(rep, _params);
        s.progress10000 = 0;
        s.terminationrequest = false;

        //
        // Autoselect algorithm
        //
        v3bftype = -999;
        v3bfparam = 0.0;
        if (s.algorithmtype == 0)
        {
            curalgorithmtype = 4;
            v3bftype = 2;
            v3bfparam = 0.0;
        }
        else
        {
            curalgorithmtype = s.algorithmtype;
            if (s.algorithmtype == 4)
            {
                v3bftype = s.bftype;
                v3bfparam = s.bfparam;
            }
        }

        //
        // Algorithms which generate V1 models
        //
        if (curalgorithmtype == 1 || curalgorithmtype == 2)
        {

            //
            // Perform compatibility checks
            //
            if ((s.nx < 2 || s.nx > 3) || s.hasscale)
            {
                rep.terminationtype = -3;
                return;
            }

            //
            // Try to build model.
            //
            // NOTE: due to historical reasons RBFV1BuildModel() accepts points
            //       cast to 3-dimensional space, even if they are really 2-dimensional.
            //       So, for 2D data we have to explicitly convert them to 3D.
            //
            if (s.nx == 2)
            {

                //
                // Convert data to 3D
                //
                apserv.rmatrixsetlengthatleast(ref x3, s.n, 3, _params);
                for (i = 0; i <= s.n - 1; i++)
                {
                    x3[i, 0] = s.x[i, 0];
                    x3[i, 1] = s.x[i, 1];
                    x3[i, 2] = 0;
                }
                rbfv1.rbfv1buildmodel(x3, s.y, s.n, s.aterm, curalgorithmtype, s.nlayers, s.radvalue, s.radzvalue, s.lambdav, s.epsort, s.epserr, s.maxits, s.model1, rep1, _params);
            }
            else
            {

                //
                // Work with raw data
                //
                rbfv1.rbfv1buildmodel(s.x, s.y, s.n, s.aterm, curalgorithmtype, s.nlayers, s.radvalue, s.radzvalue, s.lambdav, s.epsort, s.epserr, s.maxits, s.model1, rep1, _params);
            }
            s.modelversion = 1;
            rbfcreatecalcbuffer(s, s.calcbuf, _params);

            //
            // Convert report fields
            //
            rep.arows = rep1.arows;
            rep.acols = rep1.acols;
            rep.annz = rep1.annz;
            rep.iterationscount = rep1.iterationscount;
            rep.nmv = rep1.nmv;
            rep.terminationtype = rep1.terminationtype;

            //
            // Done
            //
            return;
        }

        //
        // Algorithms which generate V2 models
        //
        if (curalgorithmtype == 3)
        {

            //
            // Prepare scale vector - use unit values or user supplied ones
            //
            scalevec = new double[s.nx];
            for (i = 0; i <= s.nx - 1; i++)
            {
                if (s.hasscale)
                {
                    scalevec[i] = s.s[i];
                }
                else
                {
                    scalevec[i] = 1;
                }
            }

            //
            // Build model
            //
            rbfv2.rbfv2buildhierarchical(s.x, s.y, s.n, scalevec, s.aterm, s.nlayers, s.radvalue, s.lambdav, s.model2, ref s.progress10000, ref s.terminationrequest, rep2, _params);
            s.modelversion = 2;
            rbfcreatecalcbuffer(s, s.calcbuf, _params);

            //
            // Convert report fields
            //
            rep.terminationtype = rep2.terminationtype;
            rep.rmserror = rep2.rmserror;
            rep.maxerror = rep2.maxerror;

            //
            // Done
            //
            return;
        }

        //
        // Algorithms which generate DDM-RBF models
        //
        if (curalgorithmtype == 4)
        {

            //
            // Prepare scale vector - use unit values or user supplied ones
            //
            scalevec = new double[s.nx];
            for (i = 0; i <= s.nx - 1; i++)
            {
                if (s.hasscale)
                {
                    scalevec[i] = s.s[i];
                }
                else
                {
                    scalevec[i] = 1;
                }
            }

            //
            // Build model
            //
            rbfv3.rbfv3build(s.x, s.y, s.n, scalevec, v3bftype, v3bfparam, s.lambdav, s.aterm, s.rbfprofile, s.v3tol, s.model3, ref s.progress10000, ref s.terminationrequest, rep3, _params);
            s.modelversion = 3;
            rbfcreatecalcbuffer(s, s.calcbuf, _params);
            pushfastevaltol(s, s.fastevaltol, _params);

            //
            // Convert report fields
            //
            rep.iterationscount = rep3.iterationscount;
            rep.terminationtype = rep3.terminationtype;
            rep.rmserror = rep3.rmserror;
            rep.maxerror = rep3.maxerror;

            //
            // Done
            //
            return;
        }

        //
        // Critical error
        //
        ap.assert(false, "RBFBuildModel: integrity check failure");
    }


    /*************************************************************************
    This function calculates values of the 1-dimensional RBF model with scalar
    output (NY=1) at the given point.

    IMPORTANT: this function works only with modern  (hierarchical)  RBFs.  It 
               can not be used with legacy (version 1) RBFs because older  RBF
               code does not support 1-dimensional models.

    IMPORTANT: THIS FUNCTION IS THREAD-UNSAFE. It uses fields of  rbfmodel  as
               temporary arrays, i.e. it is  impossible  to  perform  parallel
               evaluation on the same rbfmodel object (parallel calls of  this
               function for independent rbfmodel objects are safe).
               If you want to perform parallel model evaluation  from multiple
               threads, use rbftscalcbuf() with per-thread buffer object.

    This function returns 0.0 when:
    * the model is not initialized
    * NX<>1
    * NY<>1

    INPUT PARAMETERS:
        S       -   RBF model
        X0      -   X-coordinate, finite number

    RESULT:
        value of the model or 0.0 (as defined above)

      -- ALGLIB --
         Copyright 13.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static double rbfcalc1(rbfmodel s,
        double x0,
        xparams _params)
    {
        double result = 0;

        ap.assert(math.isfinite(x0), "RBFCalc1: invalid value for X0 (X0 is Inf)!");
        result = 0;
        if (s.ny != 1 || s.nx != 1)
        {
            return result;
        }
        if (s.modelversion == 1)
        {
            result = 0;
            return result;
        }
        if (s.modelversion == 2)
        {
            result = rbfv2.rbfv2calc1(s.model2, x0, _params);
            return result;
        }
        if (s.modelversion == 3)
        {
            result = rbfv3.rbfv3calc1(s.model3, x0, _params);
            return result;
        }
        ap.assert(false, "RBFCalc1: integrity check failed");
        return result;
    }


    /*************************************************************************
    This function calculates values of the 2-dimensional RBF model with scalar
    output (NY=1) at the given point.

    IMPORTANT: THIS FUNCTION IS THREAD-UNSAFE. It uses fields of  rbfmodel  as
               temporary arrays, i.e. it is  impossible  to  perform  parallel
               evaluation on the same rbfmodel object (parallel calls of  this
               function for independent rbfmodel objects are safe).
               If you want to perform parallel model evaluation  from multiple
               threads, use rbftscalcbuf() with per-thread buffer object.

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
    public static double rbfcalc2(rbfmodel s,
        double x0,
        double x1,
        xparams _params)
    {
        double result = 0;

        ap.assert(math.isfinite(x0), "RBFCalc2: invalid value for X0 (X0 is Inf)!");
        ap.assert(math.isfinite(x1), "RBFCalc2: invalid value for X1 (X1 is Inf)!");
        result = 0;
        if (s.ny != 1 || s.nx != 2)
        {
            return result;
        }
        if (s.modelversion == 1)
        {
            result = rbfv1.rbfv1calc2(s.model1, x0, x1, _params);
            return result;
        }
        if (s.modelversion == 2)
        {
            result = rbfv2.rbfv2calc2(s.model2, x0, x1, _params);
            return result;
        }
        if (s.modelversion == 3)
        {
            result = rbfv3.rbfv3calc2(s.model3, x0, x1, _params);
            return result;
        }
        ap.assert(false, "RBFCalc2: integrity check failed");
        return result;
    }


    /*************************************************************************
    This function calculates values of the 3-dimensional RBF model with scalar
    output (NY=1) at the given point.

    IMPORTANT: THIS FUNCTION IS THREAD-UNSAFE. It uses fields of  rbfmodel  as
               temporary arrays, i.e. it is  impossible  to  perform  parallel
               evaluation on the same rbfmodel object (parallel calls of  this
               function for independent rbfmodel objects are safe).
               If you want to perform parallel model evaluation  from multiple
               threads, use rbftscalcbuf() with per-thread buffer object.

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
    public static double rbfcalc3(rbfmodel s,
        double x0,
        double x1,
        double x2,
        xparams _params)
    {
        double result = 0;

        ap.assert(math.isfinite(x0), "RBFCalc3: invalid value for X0 (X0 is Inf or NaN)!");
        ap.assert(math.isfinite(x1), "RBFCalc3: invalid value for X1 (X1 is Inf or NaN)!");
        ap.assert(math.isfinite(x2), "RBFCalc3: invalid value for X2 (X2 is Inf or NaN)!");
        result = 0;
        if (s.ny != 1 || s.nx != 3)
        {
            return result;
        }
        if (s.modelversion == 1)
        {
            result = rbfv1.rbfv1calc3(s.model1, x0, x1, x2, _params);
            return result;
        }
        if (s.modelversion == 2)
        {
            result = rbfv2.rbfv2calc3(s.model2, x0, x1, x2, _params);
            return result;
        }
        if (s.modelversion == 3)
        {
            result = rbfv3.rbfv3calc3(s.model3, x0, x1, x2, _params);
            return result;
        }
        ap.assert(false, "RBFCalc3: integrity check failed");
        return result;
    }


    /*************************************************************************
    This function calculates value and derivatives of  the  1-dimensional  RBF
    model with scalar output (NY=1) at the given point.

    IMPORTANT: THIS FUNCTION IS THREAD-UNSAFE. It uses fields of  rbfmodel  as
               temporary arrays, i.e. it is  impossible  to  perform  parallel
               evaluation on the same rbfmodel object (parallel calls of  this
               function for independent rbfmodel objects are safe).
               If you want to perform parallel model evaluation  from multiple
               threads, use rbftscalcbuf() with per-thread buffer object.

    This function returns 0.0 in Y and/or DY in the following cases:
    * the model is not initialized (Y=0, DY=0)
    * NX<>1 or NY<>1 (Y=0, DY=0)
    * the gradient is undefined at the trial point. Some basis  functions have
      discontinuous derivatives at the interpolation nodes:
      * biharmonic splines f=r have no Hessian and no gradient at the nodes
      In these cases only DY is set to zero (Y is still returned)

    INPUT PARAMETERS:
        S       -   RBF model
        X0      -   first coordinate, finite number

    OUTPUT PARAMETERS:
        Y       -   value of the model or 0.0 (as defined above)
        DY0     -   derivative with respect to X0

      -- ALGLIB --
         Copyright 13.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfdiff1(rbfmodel s,
        double x0,
        ref double y,
        ref double dy0,
        xparams _params)
    {
        y = 0;
        dy0 = 0;

        ap.assert(math.isfinite(x0), "RBFDiff1: invalid value for X0 (X0 is Inf or NaN)!");
        y = 0;
        dy0 = 0;
        if (s.ny != 1 || s.nx != 1)
        {
            return;
        }
        ablasf.rallocv(1, ref s.calcbuf.x, _params);
        s.calcbuf.x[0] = x0;
        rbftsdiffbuf(s, s.calcbuf, s.calcbuf.x, ref s.calcbuf.y, ref s.calcbuf.dy, _params);
        y = s.calcbuf.y[0];
        dy0 = s.calcbuf.dy[0];
    }


    /*************************************************************************
    This function calculates value and derivatives of  the  2-dimensional  RBF
    model with scalar output (NY=1) at the given point.

    IMPORTANT: THIS FUNCTION IS THREAD-UNSAFE. It uses fields of  rbfmodel  as
               temporary arrays, i.e. it is  impossible  to  perform  parallel
               evaluation on the same rbfmodel object (parallel calls of  this
               function for independent rbfmodel objects are safe).
               If you want to perform parallel model evaluation  from multiple
               threads, use rbftscalcbuf() with per-thread buffer object.

    This function returns 0.0 in Y and/or DY in the following cases:
    * the model is not initialized (Y=0, DY=0)
    * NX<>2 or NY<>1 (Y=0, DY=0)
    * the gradient is undefined at the trial point. Some basis  functions have
      discontinuous derivatives at the interpolation nodes:
      * biharmonic splines f=r have no Hessian and no gradient at the nodes
      In these cases only DY is set to zero (Y is still returned)

    INPUT PARAMETERS:
        S       -   RBF model
        X0      -   first coordinate, finite number
        X1      -   second coordinate, finite number

    OUTPUT PARAMETERS:
        Y       -   value of the model or 0.0 (as defined above)
        DY0     -   derivative with respect to X0
        DY1     -   derivative with respect to X1

      -- ALGLIB --
         Copyright 13.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfdiff2(rbfmodel s,
        double x0,
        double x1,
        ref double y,
        ref double dy0,
        ref double dy1,
        xparams _params)
    {
        y = 0;
        dy0 = 0;
        dy1 = 0;

        ap.assert(math.isfinite(x0), "RBFDiff2: invalid value for X0 (X0 is Inf or NaN)!");
        ap.assert(math.isfinite(x1), "RBFDiff2: invalid value for X1 (X1 is Inf or NaN)!");
        y = 0;
        dy0 = 0;
        dy1 = 0;
        if (s.ny != 1 || s.nx != 2)
        {
            return;
        }
        ablasf.rallocv(2, ref s.calcbuf.x, _params);
        s.calcbuf.x[0] = x0;
        s.calcbuf.x[1] = x1;
        rbftsdiffbuf(s, s.calcbuf, s.calcbuf.x, ref s.calcbuf.y, ref s.calcbuf.dy, _params);
        y = s.calcbuf.y[0];
        dy0 = s.calcbuf.dy[0];
        dy1 = s.calcbuf.dy[1];
    }


    /*************************************************************************
    This function calculates value and derivatives of  the  3-dimensional  RBF
    model with scalar output (NY=1) at the given point.

    IMPORTANT: THIS FUNCTION IS THREAD-UNSAFE. It uses fields of  rbfmodel  as
               temporary arrays, i.e. it is  impossible  to  perform  parallel
               evaluation on the same rbfmodel object (parallel calls of  this
               function for independent rbfmodel objects are safe).
               If you want to perform parallel model evaluation  from multiple
               threads, use rbftscalcbuf() with per-thread buffer object.

    This function returns 0.0 in Y and/or DY in the following cases:
    * the model is not initialized (Y=0, DY=0)
    * NX<>3 or NY<>1 (Y=0, DY=0)
    * the gradient is undefined at the trial point. Some basis  functions have
      discontinuous derivatives at the interpolation nodes:
      * biharmonic splines f=r have no Hessian and no gradient at the nodes
      In these cases only DY is set to zero (Y is still returned)

    INPUT PARAMETERS:
        S       -   RBF model
        X0      -   first coordinate, finite number
        X1      -   second coordinate, finite number
        X2      -   third coordinate, finite number

    OUTPUT PARAMETERS:
        Y       -   value of the model or 0.0 (as defined above)
        DY0     -   derivative with respect to X0
        DY1     -   derivative with respect to X1
        DY2     -   derivative with respect to X2

      -- ALGLIB --
         Copyright 13.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfdiff3(rbfmodel s,
        double x0,
        double x1,
        double x2,
        ref double y,
        ref double dy0,
        ref double dy1,
        ref double dy2,
        xparams _params)
    {
        y = 0;
        dy0 = 0;
        dy1 = 0;
        dy2 = 0;

        ap.assert(math.isfinite(x0), "RBFDiff3: invalid value for X0 (X0 is Inf or NaN)!");
        ap.assert(math.isfinite(x1), "RBFDiff3: invalid value for X1 (X1 is Inf or NaN)!");
        ap.assert(math.isfinite(x2), "RBFDiff3: invalid value for X2 (X2 is Inf or NaN)!");
        y = 0;
        dy0 = 0;
        dy1 = 0;
        dy2 = 0;
        if (s.ny != 1 || s.nx != 3)
        {
            return;
        }
        ablasf.rallocv(3, ref s.calcbuf.x, _params);
        s.calcbuf.x[0] = x0;
        s.calcbuf.x[1] = x1;
        s.calcbuf.x[2] = x2;
        rbftsdiffbuf(s, s.calcbuf, s.calcbuf.x, ref s.calcbuf.y, ref s.calcbuf.dy, _params);
        y = s.calcbuf.y[0];
        dy0 = s.calcbuf.dy[0];
        dy1 = s.calcbuf.dy[1];
        dy2 = s.calcbuf.dy[2];
    }


    /*************************************************************************
    This function sets absolute accuracy of a  fast evaluation  algorithm used
    by rbffastcalc() and other fast evaluation functions.

    A fast evaluation algorithm is model-dependent and is available  only  for
    some RBF models. Usually it utilizes far field expansions (a generalization
    of the fast multipoles  method).  If  no  approximate  fast  evaluator  is
    available for the  current RBF model type, this function has no effect.

    NOTE: this function can be called before or after the model was built. The
          result will be the same.

    NOTE: this  function  has  O(N) running time, where N is a  points  count.
          Most fast evaluators work by aggregating influence of  point groups,
          i.e. by computing so called far field. Changing evaluator  tolerance
          means that far field radii have to  be  recomputed  for  each  point
          cluster, and we have O(N) such clusters.
          
          This function is still very fast, but  it  should  not be called too
          often, e.g. every time you call rbffastcalc() in a loop.

    NOTE: the tolerance  set  by this function is an accuracy of an  evaluator
          which computes the value of the model. It is  NOT  accuracy  of  the
          model itself.
          
          E.g., if you set evaluation accuracy to 1E-12, the model value  will
          be computed with required precision. However, the model itself is an
          approximation of the target (the default requirement is to fit model
          with ~6 digits of precision) and THIS accuracy can  not  be  changed
          after the model was built.

    IMPORTANT: THIS FUNCTION IS THREAD-UNSAFE. Calling it while another thread
               tries to use rbffastcalc() is unsafe because it means that  the
               accuracy requirements will change in the middle of computations.
               The algorithm may behave unpredictably.

    INPUT PARAMETERS:
        S       -   RBF model
        TOL     -   TOL>0, desired evaluation tolerance:
                    * should be somewhere between 1E-3 and 1E-6
                    * values outside of this range will cause no problems (the
                      evaluator will do the job anyway). However,  too  strict
                      precision requirements may mean  that  no  approximation
                      speed-up will be achieved.

      -- ALGLIB --
         Copyright 19.09.2022 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfsetfastevaltol(rbfmodel s,
        double tol,
        xparams _params)
    {
        ap.assert(math.isfinite(tol), "RBFSetFastEvalTol: TOL is not a finite number");
        ap.assert((double)(tol) > (double)(0), "RBFSetFastEvalTol: TOL<=0");
        s.fastevaltol = tol;
        pushfastevaltol(s, tol, _params);
    }


    /*************************************************************************
    This function calculates values of the RBF model at the given point  using
    a fast approximate algorithm whenever possible. If no  fast  algorithm  is
    available for a given model type, traditional O(N) approach is used.

    Presently, fast evaluation is implemented only for biharmonic splines.

    The absolute approximation accuracy is controlled by the rbfsetfastevaltol()
    function.

    IMPORTANT: THIS FUNCTION IS THREAD-UNSAFE. It uses fields of  rbfmodel  as
               temporary arrays, i.e. it is  impossible  to  perform  parallel
               evaluation on the same rbfmodel object (parallel calls of  this
               function for independent rbfmodel objects are safe).
               If you want to perform parallel model evaluation  from multiple
               threads, use rbftscalcbuf() with a per-thread buffer object.

    This function returns 0.0 when model is not initialized.

    INPUT PARAMETERS:
        S       -   RBF model
        X       -   coordinates, array[NX].
                    X may have more than NX elements, in this case only 
                    leading NX will be used.

    OUTPUT PARAMETERS:
        Y       -   function value, array[NY]. Y is out-parameter and 
                    reallocated after call to this function. In case you  want
                    to reuse previously allocated Y, you may use RBFCalcBuf(),
                    which reallocates Y only when it is too small.

      -- ALGLIB --
         Copyright 19.09.2022 by Bochkanov Sergey
    *************************************************************************/
    public static void rbffastcalc(rbfmodel s,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        int i = 0;

        y = new double[0];

        ap.assert(ap.len(x) >= s.nx, "RBFCalc: Length(X)<NX");
        ap.assert(apserv.isfinitevector(x, s.nx, _params), "RBFCalc: X contains infinite or NaN values");
        if (ap.len(y) < s.ny)
        {
            y = new double[s.ny];
        }
        for (i = 0; i <= s.ny - 1; i++)
        {
            y[i] = 0;
        }
        if (s.modelversion == 1)
        {
            rbfv1.rbfv1calcbuf(s.model1, x, ref y, _params);
            return;
        }
        if (s.modelversion == 2)
        {
            rbfv2.rbfv2calcbuf(s.model2, x, ref y, _params);
            return;
        }
        if (s.modelversion == 3)
        {
            rbfv3.rbfv3tsfastcalcbuf(s.model3, s.model3.calcbuf, x, ref y, _params);
            return;
        }
        ap.assert(false, "RBFCalcBuf: integrity check failed");
    }


    /*************************************************************************
    This function calculates values of the RBF model at the given point.

    This is general function which can be used for arbitrary NX (dimension  of 
    the space of arguments) and NY (dimension of the function itself). However
    when  you  have  NY=1  you  may  find more convenient to use rbfcalc2() or 
    rbfcalc3().

    IMPORTANT: THIS FUNCTION IS THREAD-UNSAFE. It uses fields of  rbfmodel  as
               temporary arrays, i.e. it is  impossible  to  perform  parallel
               evaluation on the same rbfmodel object (parallel calls of  this
               function for independent rbfmodel objects are safe).
               If you want to perform parallel model evaluation  from multiple
               threads, use rbftscalcbuf() with per-thread buffer object.

    This function returns 0.0 when model is not initialized.

    INPUT PARAMETERS:
        S       -   RBF model
        X       -   coordinates, array[NX].
                    X may have more than NX elements, in this case only 
                    leading NX will be used.

    OUTPUT PARAMETERS:
        Y       -   function value, array[NY]. Y is out-parameter and 
                    reallocated after call to this function. In case you  want
                    to reuse previously allocated Y, you may use RBFCalcBuf(),
                    which reallocates Y only when it is too small.

      -- ALGLIB --
         Copyright 13.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfcalc(rbfmodel s,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        y = new double[0];

        ap.assert(ap.len(x) >= s.nx, "RBFCalc: Length(X)<NX");
        ap.assert(apserv.isfinitevector(x, s.nx, _params), "RBFCalc: X contains infinite or NaN values");
        rbfcalcbuf(s, x, ref y, _params);
    }


    /*************************************************************************
    This function calculates values of the RBF model and  its  derivatives  at
    the given point.

    This is general function which can be used for arbitrary NX (dimension  of 
    the space of arguments) and NY (dimension of the function itself). However
    if you have NX=3 and NY=1, you may find more convenient to use rbfdiff3().

    IMPORTANT: THIS FUNCTION IS THREAD-UNSAFE. It uses fields of  rbfmodel  as
               temporary arrays, i.e. it is  impossible  to  perform  parallel
               evaluation on the same rbfmodel object (parallel calls of  this
               function for independent rbfmodel objects are safe).
               
               If you want to perform parallel model evaluation  from multiple
               threads, use rbftsdiffbuf() with per-thread buffer object.

    This function returns 0.0 in Y and/or DY in the following cases:
    * the model is not initialized (Y=0, DY=0)
    * the gradient is undefined at the trial point. Some basis  functions have
      discontinuous derivatives at the interpolation nodes:
      * biharmonic splines f=r have no Hessian and no gradient at the nodes
      In these cases only DY is set to zero (Y is still returned)

    INPUT PARAMETERS:
        S       -   RBF model
        X       -   coordinates, array[NX].
                    X may have more than NX elements, in this case only 
                    leading NX will be used.

    OUTPUT PARAMETERS:
        Y       -   function value, array[NY]. Y is out-parameter and 
                    reallocated after call to this function. In case you  want
                    to reuse previously allocated Y, you may use RBFDiffBuf(),
                    which reallocates Y only when it is too small.
        DY      -   derivatives, array[NX*NY]:
                    * Y[I*NX+J] with 0<=I<NY and 0<=J<NX  stores derivative of
                      function component I with respect to input J.
                    * for NY=1 it is simply NX-dimensional gradient of the
                      scalar NX-dimensional function
                    DY is out-parameter and reallocated  after  call  to  this
                    function. In case you want to reuse  previously  allocated
                    DY, you may use RBFDiffBuf(), which  reallocates  DY  only
                    when it is too small to store the result.

      -- ALGLIB --
         Copyright 13.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfdiff(rbfmodel s,
        double[] x,
        ref double[] y,
        ref double[] dy,
        xparams _params)
    {
        y = new double[0];
        dy = new double[0];

        ap.assert(ap.len(x) >= s.nx, "RBFDiff: Length(X)<NX");
        ap.assert(apserv.isfinitevector(x, s.nx, _params), "RBFDiff: X contains infinite or NaN values");
        rbfdiffbuf(s, x, ref y, ref dy, _params);
    }


    /*************************************************************************
    This function calculates values of the RBF model and  its first and second
    derivatives (Hessian matrix) at the given point.

    This function supports both scalar (NY=1) and vector-valued (NY>1) RBFs.

    IMPORTANT: THIS FUNCTION IS THREAD-UNSAFE. It uses fields of  rbfmodel  as
               temporary arrays, i.e. it is  impossible  to  perform  parallel
               evaluation on the same rbfmodel object (parallel calls of  this
               function for independent rbfmodel objects are safe).
               
               If you want to perform parallel model evaluation  from multiple
               threads, use rbftshessbuf() with per-thread buffer object.

    This function returns 0 in Y and/or DY and/or D2Y in the following cases:
    * the model is not initialized (Y=0, DY=0, D2Y=0)
    * the gradient and/or Hessian is undefined at the trial point.  Some basis
      functions have discontinuous derivatives at the interpolation nodes:
      * thin plate splines have no Hessian at the nodes
      * biharmonic splines f=r have no Hessian and no gradient at the  nodes
      In these cases only corresponding derivative is set  to  zero,  and  the
      rest of the derivatives is still returned.

    INPUT PARAMETERS:
        S       -   RBF model
        X       -   coordinates, array[NX].
                    X may have more than NX elements, in this case only 
                    leading NX will be used.

    OUTPUT PARAMETERS:
        Y       -   function value, array[NY].
                    Y is out-parameter and  reallocated  after  call  to  this
                    function. In case you  want to reuse previously  allocated
                    Y, you may use RBFHessBuf(), which reallocates Y only when
                    it is too small.
        DY      -   first derivatives, array[NY*NX]:
                    * Y[I*NX+J] with 0<=I<NY and 0<=J<NX  stores derivative of
                      function component I with respect to input J.
                    * for NY=1 it is simply NX-dimensional gradient of the
                      scalar NX-dimensional function
                    DY is out-parameter and reallocated  after  call  to  this
                    function. In case you want to reuse  previously  allocated
                    DY, you may use RBFHessBuf(), which  reallocates  DY  only
                    when it is too small to store the result.
        D2Y     -   second derivatives, array[NY*NX*NX]:
                    * for NY=1 it is NX*NX array that stores  Hessian  matrix,
                      with Y[I*NX+J]=Y[J*NX+I].
                    * for  a  vector-valued  RBF  with  NY>1  it  contains  NY
                      subsequently stored Hessians: an element Y[K*NX*NX+I*NX+J]
                      with  0<=K<NY,  0<=I<NX  and  0<=J<NX    stores   second
                      derivative of the function #K  with  respect  to  inputs
                      #I and #J.
                    D2Y is out-parameter and reallocated  after  call  to this
                    function. In case you want to reuse  previously  allocated
                    D2Y, you may use RBFHessBuf(), which  reallocates D2Y only
                    when it is too small to store the result.

      -- ALGLIB --
         Copyright 13.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfhess(rbfmodel s,
        double[] x,
        ref double[] y,
        ref double[] dy,
        ref double[] d2y,
        xparams _params)
    {
        y = new double[0];
        dy = new double[0];
        d2y = new double[0];

        ap.assert(ap.len(x) >= s.nx, "RBFHess: Length(X)<NX");
        ap.assert(apserv.isfinitevector(x, s.nx, _params), "RBFHess: X contains infinite or NaN values");
        rbftshessbuf(s, s.calcbuf, x, ref y, ref dy, ref d2y, _params);
    }


    /*************************************************************************
    This function calculates values of the RBF model at the given point.

    Same as rbfcalc(), but does not reallocate Y when in is large enough to 
    store function values.

    IMPORTANT: THIS FUNCTION IS THREAD-UNSAFE. It uses fields of  rbfmodel  as
               temporary arrays, i.e. it is  impossible  to  perform  parallel
               evaluation on the same rbfmodel object (parallel calls of  this
               function for independent rbfmodel objects are safe).
               If you want to perform parallel model evaluation  from multiple
               threads, use rbftscalcbuf() with per-thread buffer object.

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
    public static void rbfcalcbuf(rbfmodel s,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        int i = 0;

        ap.assert(ap.len(x) >= s.nx, "RBFCalcBuf: Length(X)<NX");
        ap.assert(apserv.isfinitevector(x, s.nx, _params), "RBFCalcBuf: X contains infinite or NaN values");
        if (ap.len(y) < s.ny)
        {
            y = new double[s.ny];
        }
        for (i = 0; i <= s.ny - 1; i++)
        {
            y[i] = 0;
        }
        if (s.modelversion == 1)
        {
            rbfv1.rbfv1calcbuf(s.model1, x, ref y, _params);
            return;
        }
        if (s.modelversion == 2)
        {
            rbfv2.rbfv2calcbuf(s.model2, x, ref y, _params);
            return;
        }
        if (s.modelversion == 3)
        {
            rbfv3.rbfv3calcbuf(s.model3, x, ref y, _params);
            return;
        }
        ap.assert(false, "RBFCalcBuf: integrity check failed");
    }


    /*************************************************************************
    This function calculates values of the RBF model and  its  derivatives  at
    the given point. It is a buffered version of the RBFDiff() which tries  to
    reuse possibly preallocated output arrays Y/DY as much as possible.

    This is general function which can be used for arbitrary NX (dimension  of 
    the space of arguments) and NY (dimension of the function itself). However
    if you have NX=1, 2 or 3 and NY=1, you may find  more  convenient  to  use
    rbfdiff1(), rbfdiff2() or rbfdiff3().

    IMPORTANT: THIS FUNCTION IS THREAD-UNSAFE. It uses fields of  rbfmodel  as
               temporary arrays, i.e. it is  impossible  to  perform  parallel
               evaluation on the same rbfmodel object (parallel calls of  this
               function for independent rbfmodel objects are safe).
               
               If you want to perform parallel model evaluation  from multiple
               threads, use rbftsdiffbuf() with per-thread buffer object.

    This function returns 0.0 in Y and/or DY in the following cases:
    * the model is not initialized (Y=0, DY=0)
    * the gradient is undefined at the trial point. Some basis  functions have
      discontinuous derivatives at the interpolation nodes:
      * biharmonic splines f=r have no Hessian and no gradient at the nodes
      In these cases only DY is set to zero (Y is still returned)

    INPUT PARAMETERS:
        S       -   RBF model
        X       -   coordinates, array[NX].
                    X may have more than NX elements, in this case only 
                    leading NX will be used.
        Y, DY   -   possibly preallocated arrays; if array size is large enough
                    to store results, this function does not  reallocate  array
                    to fit output size exactly.

    OUTPUT PARAMETERS:
        Y       -   function value, array[NY].
        DY      -   derivatives, array[NX*NY]:
                    * Y[I*NX+J] with 0<=I<NY and 0<=J<NX  stores derivative of
                      function component I with respect to input J.
                    * for NY=1 it is simply NX-dimensional gradient of the
                      scalar NX-dimensional function

      -- ALGLIB --
         Copyright 13.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfdiffbuf(rbfmodel s,
        double[] x,
        ref double[] y,
        ref double[] dy,
        xparams _params)
    {
        int i = 0;

        ap.assert(ap.len(x) >= s.nx, "RBFDiffBuf: Length(X)<NX");
        ap.assert(apserv.isfinitevector(x, s.nx, _params), "RBFDiffBuf: X contains infinite or NaN values");
        ap.assert(s.modelversion == s.calcbuf.modelversion, "RBF: integrity check 3945 failed");
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
            y[i] = 0;
        }
        for (i = 0; i <= s.ny * s.nx - 1; i++)
        {
            dy[i] = 0;
        }
        if (s.modelversion == 1)
        {
            rbfv1.rbfv1tsdiffbuf(s.model1, s.calcbuf.bufv1, x, ref y, ref dy, _params);
            return;
        }
        if (s.modelversion == 2)
        {
            rbfv2.rbfv2tsdiffbuf(s.model2, s.calcbuf.bufv2, x, ref y, ref dy, _params);
            return;
        }
        if (s.modelversion == 3)
        {
            rbfv3.rbfv3tsdiffbuf(s.model3, s.calcbuf.bufv3, x, ref y, ref dy, _params);
            return;
        }
        ap.assert(false, "RBFDiffBuf: integrity check failed");
    }


    /*************************************************************************
    This function calculates values of the RBF model and  its first and second
    derivatives (Hessian matrix) at the given point. It is a buffered  version
    that reuses memory  allocated  in  output  buffers  Y/DY/D2Y  as  much  as
    possible.

    This function supports both scalar (NY=1) and vector-valued (NY>1) RBFs.

    IMPORTANT: THIS FUNCTION IS THREAD-UNSAFE. It uses fields of  rbfmodel  as
               temporary arrays, i.e. it is  impossible  to  perform  parallel
               evaluation on the same rbfmodel object (parallel calls of  this
               function for independent rbfmodel objects are safe).
               
               If you want to perform parallel model evaluation  from multiple
               threads, use rbftshessbuf() with per-thread buffer object.

    This function returns 0 in Y and/or DY and/or D2Y in the following cases:
    * the model is not initialized (Y=0, DY=0, D2Y=0)
    * the gradient and/or Hessian is undefined at the trial point.  Some basis
      functions have discontinuous derivatives at the interpolation nodes:
      * thin plate splines have no Hessian at the nodes
      * biharmonic splines f=r have no Hessian and no gradient at the  nodes
      In these cases only corresponding derivative is set  to  zero,  and  the
      rest of the derivatives is still returned.

    INPUT PARAMETERS:
        S       -   RBF model
        X       -   coordinates, array[NX].
                    X may have more than NX elements, in this case only 
                    leading NX will be used.
        Y,DY,D2Y-   possible preallocated output arrays. If these  arrays  are
                    smaller than  required  to  store  the  result,  they  are
                    automatically reallocated. If array is large enough, it is
                    not resized.

    OUTPUT PARAMETERS:
        Y       -   function value, array[NY].
        DY      -   first derivatives, array[NY*NX]:
                    * Y[I*NX+J] with 0<=I<NY and 0<=J<NX  stores derivative of
                      function component I with respect to input J.
                    * for NY=1 it is simply NX-dimensional gradient of the
                      scalar NX-dimensional function
        D2Y     -   second derivatives, array[NY*NX*NX]:
                    * for NY=1 it is NX*NX array that stores  Hessian  matrix,
                      with Y[I*NX+J]=Y[J*NX+I].
                    * for  a  vector-valued  RBF  with  NY>1  it  contains  NY
                      subsequently stored Hessians: an element Y[K*NX*NX+I*NX+J]
                      with  0<=K<NY,  0<=I<NX  and  0<=J<NX    stores   second
                      derivative of the function #K  with  respect  to  inputs
                      #I and #J.

      -- ALGLIB --
         Copyright 13.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfhessbuf(rbfmodel s,
        double[] x,
        ref double[] y,
        ref double[] dy,
        ref double[] d2y,
        xparams _params)
    {
        ap.assert(ap.len(x) >= s.nx, "RBFHess: Length(X)<NX");
        ap.assert(apserv.isfinitevector(x, s.nx, _params), "RBFHess: X contains infinite or NaN values");
        rbftshessbuf(s, s.calcbuf, x, ref y, ref dy, ref d2y, _params);
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
    public static void rbftscalcbuf(rbfmodel s,
        rbfcalcbuffer buf,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        int i = 0;

        ap.assert(ap.len(x) >= s.nx, "RBFCalcBuf: Length(X)<NX");
        ap.assert(apserv.isfinitevector(x, s.nx, _params), "RBFCalcBuf: X contains infinite or NaN values");
        ap.assert(s.modelversion == buf.modelversion, "RBFCalcBuf: buffer object is not compatible with RBF model");
        if (ap.len(y) < s.ny)
        {
            y = new double[s.ny];
        }
        for (i = 0; i <= s.ny - 1; i++)
        {
            y[i] = 0;
        }
        if (s.modelversion == 1)
        {
            rbfv1.rbfv1tscalcbuf(s.model1, buf.bufv1, x, ref y, _params);
            return;
        }
        if (s.modelversion == 2)
        {
            rbfv2.rbfv2tscalcbuf(s.model2, buf.bufv2, x, ref y, _params);
            return;
        }
        if (s.modelversion == 3)
        {
            rbfv3.rbfv3tscalcbuf(s.model3, buf.bufv3, x, ref y, _params);
            return;
        }
        ap.assert(false, "RBFTsCalcBuf: integrity check failed");
    }


    /*************************************************************************
    This function calculates values of the RBF model and  its  derivatives  at
    the given point, using external buffer object (internal temporaries of the
    RBF model are not modified).

    This function allows to use same RBF model object  in  different  threads,
    assuming  that  different   threads  use different instances of the buffer
    structure.

    This function returns 0.0 in Y and/or DY in the following cases:
    * the model is not initialized (Y=0, DY=0)
    * the gradient is undefined at the trial point. Some basis  functions have
      discontinuous derivatives at the interpolation nodes:
      * biharmonic splines f=r have no Hessian and no gradient at the nodes
      In these cases only DY is set to zero (Y is still returned)

    INPUT PARAMETERS:
        S       -   RBF model, may be shared between different threads
        Buf     -   buffer object created for this particular instance of  RBF
                    model with rbfcreatecalcbuffer().
        X       -   coordinates, array[NX].
                    X may have more than NX elements, in this case only 
                    leading NX will be used.
        Y, DY   -   possibly preallocated arrays; if array size is large enough
                    to store results, this function does not  reallocate  array
                    to fit output size exactly.

    OUTPUT PARAMETERS:
        Y       -   function value, array[NY].
        DY      -   derivatives, array[NX*NY]:
                    * Y[I*NX+J] with 0<=I<NY and 0<=J<NX  stores derivative of
                      function component I with respect to input J.
                    * for NY=1 it is simply NX-dimensional gradient of the
                      scalar NX-dimensional function
                    Zero is returned when the first derivative is undefined.

      -- ALGLIB --
         Copyright 13.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void rbftsdiffbuf(rbfmodel s,
        rbfcalcbuffer buf,
        double[] x,
        ref double[] y,
        ref double[] dy,
        xparams _params)
    {
        int i = 0;

        ap.assert(ap.len(x) >= s.nx, "RBFTsDiffBuf: Length(X)<NX");
        ap.assert(apserv.isfinitevector(x, s.nx, _params), "RBFTsDiffBuf: X contains infinite or NaN values");
        ap.assert(s.modelversion == buf.modelversion, "RBFTsDiffBuf: integrity check 3985 failed");
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
            y[i] = 0;
        }
        for (i = 0; i <= s.ny * s.nx - 1; i++)
        {
            dy[i] = 0;
        }
        if (s.modelversion == 1)
        {
            rbfv1.rbfv1tsdiffbuf(s.model1, buf.bufv1, x, ref y, ref dy, _params);
            return;
        }
        if (s.modelversion == 2)
        {
            rbfv2.rbfv2tsdiffbuf(s.model2, buf.bufv2, x, ref y, ref dy, _params);
            return;
        }
        if (s.modelversion == 3)
        {
            rbfv3.rbfv3tsdiffbuf(s.model3, buf.bufv3, x, ref y, ref dy, _params);
            return;
        }
        ap.assert(false, "RBFDiffBuf: integrity check failed");
    }


    /*************************************************************************
    This function calculates values of the RBF model and  its first and second
    derivatives (Hessian matrix) at the given  point,  using  external  buffer
    object (internal temporaries of the RBF  model  are  not  modified).

    This function allows to use same RBF model object  in  different  threads,
    assuming  that  different   threads  use different instances of the buffer
    structure.

    This function returns 0 in Y and/or DY and/or D2Y in the following cases:
    * the model is not initialized (Y=0, DY=0, D2Y=0)
    * the gradient and/or Hessian is undefined at the trial point.  Some basis
      functions have discontinuous derivatives at the interpolation nodes:
      * thin plate splines have no Hessian at the nodes
      * biharmonic splines f=r have no Hessian and no gradient at the  nodes
      In these cases only corresponding derivative is set  to  zero,  and  the
      rest of the derivatives is still returned.

    INPUT PARAMETERS:
        S       -   RBF model, may be shared between different threads
        Buf     -   buffer object created for this particular instance of  RBF
                    model with rbfcreatecalcbuffer().
        X       -   coordinates, array[NX].
                    X may have more than NX elements, in this case only 
                    leading NX will be used.
        Y,DY,D2Y-   possible preallocated output arrays. If these  arrays  are
                    smaller than  required  to  store  the  result,  they  are
                    automatically reallocated. If array is large enough, it is
                    not resized.

    OUTPUT PARAMETERS:
        Y       -   function value, array[NY].
        DY      -   first derivatives, array[NY*NX]:
                    * Y[I*NX+J] with 0<=I<NY and 0<=J<NX  stores derivative of
                      function component I with respect to input J.
                    * for NY=1 it is simply NX-dimensional gradient of the
                      scalar NX-dimensional function
                    Zero is returned when the first derivative is undefined.
        D2Y     -   second derivatives, array[NY*NX*NX]:
                    * for NY=1 it is NX*NX array that stores  Hessian  matrix,
                      with Y[I*NX+J]=Y[J*NX+I].
                    * for  a  vector-valued  RBF  with  NY>1  it  contains  NY
                      subsequently stored Hessians: an element Y[K*NX*NX+I*NX+J]
                      with  0<=K<NY,  0<=I<NX  and  0<=J<NX    stores   second
                      derivative of the function #K  with  respect  to  inputs
                      #I and #J.
                    Zero is returned when the second derivative is undefined.

      -- ALGLIB --
         Copyright 13.12.2021 by Bochkanov Sergey
    *************************************************************************/
    public static void rbftshessbuf(rbfmodel s,
        rbfcalcbuffer buf,
        double[] x,
        ref double[] y,
        ref double[] dy,
        ref double[] d2y,
        xparams _params)
    {
        int i = 0;

        ap.assert(ap.len(x) >= s.nx, "RBFTsHessBuf: Length(X)<NX");
        ap.assert(apserv.isfinitevector(x, s.nx, _params), "RBFTsHessBuf: X contains infinite or NaN values");
        ap.assert(s.modelversion == buf.modelversion, "RBFTsHessBuf: integrity check 3953 failed");
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
            y[i] = 0;
        }
        for (i = 0; i <= s.ny * s.nx - 1; i++)
        {
            dy[i] = 0;
        }
        for (i = 0; i <= s.ny * s.nx * s.nx - 1; i++)
        {
            d2y[i] = 0;
        }
        if (s.modelversion == 1)
        {
            rbfv1.rbfv1tshessbuf(s.model1, buf.bufv1, x, ref y, ref dy, ref d2y, _params);
            return;
        }
        if (s.modelversion == 2)
        {
            rbfv2.rbfv2tshessbuf(s.model2, buf.bufv2, x, ref y, ref dy, ref d2y, _params);
            return;
        }
        if (s.modelversion == 3)
        {
            rbfv3.rbfv3tshessbuf(s.model3, buf.bufv3, x, ref y, ref dy, ref d2y, _params);
            return;
        }
        ap.assert(false, "RBFDiffBuf: integrity check failed");
    }


    /*************************************************************************
    This is legacy function for gridded calculation of RBF model.

    It is superseded by rbfgridcalc2v() and  rbfgridcalc2vsubset()  functions.

      -- ALGLIB --
         Copyright 13.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfgridcalc2(rbfmodel s,
        double[] x0,
        int n0,
        double[] x1,
        int n1,
        ref double[,] y,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double[] yy = new double[0];

        y = new double[0, 0];

        ap.assert(n0 > 0, "RBFGridCalc2: invalid value for N0 (N0<=0)!");
        ap.assert(n1 > 0, "RBFGridCalc2: invalid value for N1 (N1<=0)!");
        ap.assert(ap.len(x0) >= n0, "RBFGridCalc2: Length(X0)<N0");
        ap.assert(ap.len(x1) >= n1, "RBFGridCalc2: Length(X1)<N1");
        ap.assert(apserv.isfinitevector(x0, n0, _params), "RBFGridCalc2: X0 contains infinite or NaN values!");
        ap.assert(apserv.isfinitevector(x1, n1, _params), "RBFGridCalc2: X1 contains infinite or NaN values!");
        if (s.modelversion == 1)
        {
            rbfv1.rbfv1gridcalc2(s.model1, x0, n0, x1, n1, ref y, _params);
            return;
        }
        if (s.modelversion == 2)
        {
            rbfv2.rbfv2gridcalc2(s.model2, x0, n0, x1, n1, ref y, _params);
            return;
        }
        if (s.modelversion == 3)
        {
            ablasf.rallocm(n0, n1, ref y, _params);
            if (s.nx != 2 || s.ny != 1)
            {
                ablasf.rsetm(n0, n1, 0.0, y, _params);
                return;
            }
            rbfgridcalc2v(s, x0, n0, x1, n1, ref yy, _params);
            for (i = 0; i <= n0 - 1; i++)
            {
                for (j = 0; j <= n1 - 1; j++)
                {
                    y[i, j] = yy[i + j * n0];
                }
            }
            return;
        }
        ap.assert(false, "RBFGridCalc2: integrity check failed");
    }


    /*************************************************************************
    This function calculates values of the RBF  model  at  the  regular  grid,
    which  has  N0*N1 points, with Point[I,J] = (X0[I], X1[J]).  Vector-valued
    RBF models are supported.

    This function returns 0.0 when:
    * model is not initialized
    * NX<>2

      ! COMMERCIAL EDITION OF ALGLIB:
      ! 
      ! Commercial Edition of ALGLIB includes following important improvements
      ! of this function:
      ! * high-performance native backend with same C# interface (C# version)
      ! * multithreading support (C++ and C# versions)
      ! 
      ! We recommend you to read 'Working with commercial version' section  of
      ! ALGLIB Reference Manual in order to find out how to  use  performance-
      ! related features provided by commercial edition of ALGLIB.

    NOTE: Parallel  processing  is  implemented only for modern (hierarchical)
          RBFs. Legacy version 1 RBFs (created  by  QNN  or  RBF-ML) are still
          processed serially.

    INPUT PARAMETERS:
        S       -   RBF model, used in read-only mode, can be  shared  between
                    multiple   invocations  of  this  function  from  multiple
                    threads.
        
        X0      -   array of grid nodes, first coordinates, array[N0].
                    Must be ordered by ascending. Exception is generated
                    if the array is not correctly ordered.
        N0      -   grid size (number of nodes) in the first dimension
        
        X1      -   array of grid nodes, second coordinates, array[N1]
                    Must be ordered by ascending. Exception is generated
                    if the array is not correctly ordered.
        N1      -   grid size (number of nodes) in the second dimension

    OUTPUT PARAMETERS:
        Y       -   function values, array[NY*N0*N1], where NY is a  number of
                    "output" vector values (this  function   supports  vector-
                    valued RBF models). Y is out-variable and  is  reallocated
                    by this function.
                    Y[K+NY*(I0+I1*N0)]=F_k(X0[I0],X1[I1]), for:
                    *  K=0...NY-1
                    * I0=0...N0-1
                    * I1=0...N1-1

    NOTE: this function supports weakly ordered grid nodes, i.e. you may  have                
          X[i]=X[i+1] for some i. It does  not  provide  you  any  performance
          benefits  due  to   duplication  of  points,  just  convenience  and
          flexibility.
          
    NOTE: this  function  is  re-entrant,  i.e.  you  may  use  same  rbfmodel
          structure in multiple threads calling  this function  for  different
          grids.
          
    NOTE: if you need function values on some subset  of  regular  grid, which
          may be described as "several compact and  dense  islands",  you  may
          use rbfgridcalc2vsubset().

      -- ALGLIB --
         Copyright 27.01.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfgridcalc2v(rbfmodel s,
        double[] x0,
        int n0,
        double[] x1,
        int n1,
        ref double[] y,
        xparams _params)
    {
        int i = 0;
        bool[] dummy = new bool[0];

        y = new double[0];

        ap.assert(n0 > 0, "RBFGridCalc2V: invalid value for N0 (N0<=0)!");
        ap.assert(n1 > 0, "RBFGridCalc2V: invalid value for N1 (N1<=0)!");
        ap.assert(ap.len(x0) >= n0, "RBFGridCalc2V: Length(X0)<N0");
        ap.assert(ap.len(x1) >= n1, "RBFGridCalc2V: Length(X1)<N1");
        ap.assert(apserv.isfinitevector(x0, n0, _params), "RBFGridCalc2V: X0 contains infinite or NaN values!");
        ap.assert(apserv.isfinitevector(x1, n1, _params), "RBFGridCalc2V: X1 contains infinite or NaN values!");
        for (i = 0; i <= n0 - 2; i++)
        {
            ap.assert((double)(x0[i]) <= (double)(x0[i + 1]), "RBFGridCalc2V: X0 is not ordered by ascending");
        }
        for (i = 0; i <= n1 - 2; i++)
        {
            ap.assert((double)(x1[i]) <= (double)(x1[i + 1]), "RBFGridCalc2V: X1 is not ordered by ascending");
        }
        rbfgridcalc2vx(s, x0, n0, x1, n1, dummy, false, ref y, _params);
    }


    /*************************************************************************
    This function calculates values of the RBF model at some subset of regular
    grid:
    * grid has N0*N1 points, with Point[I,J] = (X0[I], X1[J])
    * only values at some subset of this grid are required
    Vector-valued RBF models are supported.

    This function returns 0.0 when:
    * model is not initialized
    * NX<>2

      ! COMMERCIAL EDITION OF ALGLIB:
      ! 
      ! Commercial Edition of ALGLIB includes following important improvements
      ! of this function:
      ! * high-performance native backend with same C# interface (C# version)
      ! * multithreading support (C++ and C# versions)
      ! 
      ! We recommend you to read 'Working with commercial version' section  of
      ! ALGLIB Reference Manual in order to find out how to  use  performance-
      ! related features provided by commercial edition of ALGLIB.

    NOTE: Parallel  processing  is  implemented only for modern (hierarchical)
          RBFs. Legacy version 1 RBFs (created  by  QNN  or  RBF-ML) are still
          processed serially.

    INPUT PARAMETERS:
        S       -   RBF model, used in read-only mode, can be  shared  between
                    multiple   invocations  of  this  function  from  multiple
                    threads.
        
        X0      -   array of grid nodes, first coordinates, array[N0].
                    Must be ordered by ascending. Exception is generated
                    if the array is not correctly ordered.
        N0      -   grid size (number of nodes) in the first dimension
        
        X1      -   array of grid nodes, second coordinates, array[N1]
                    Must be ordered by ascending. Exception is generated
                    if the array is not correctly ordered.
        N1      -   grid size (number of nodes) in the second dimension
        
        FlagY   -   array[N0*N1]:
                    * Y[I0+I1*N0] corresponds to node (X0[I0],X1[I1])
                    * it is a "bitmap" array which contains  False  for  nodes
                      which are NOT calculated, and True for nodes  which  are
                      required.

    OUTPUT PARAMETERS:
        Y       -   function values, array[NY*N0*N1*N2], where NY is a  number
                    of "output" vector values (this function  supports vector-
                    valued RBF models):
                    * Y[K+NY*(I0+I1*N0)]=F_k(X0[I0],X1[I1]),
                      for K=0...NY-1, I0=0...N0-1, I1=0...N1-1.
                    * elements of Y[] which correspond  to  FlagY[]=True   are
                      loaded by model values (which may be  exactly  zero  for
                      some nodes).
                    * elements of Y[] which correspond to FlagY[]=False MAY be
                      initialized by zeros OR may be calculated. This function
                      processes  grid  as  a  hierarchy  of  nested blocks and
                      micro-rows. If just one element of micro-row is required,
                      entire micro-row (up to 8 nodes in the current  version,
                      but no promises) is calculated.

    NOTE: this function supports weakly ordered grid nodes, i.e. you may  have                
          X[i]=X[i+1] for some i. It does  not  provide  you  any  performance
          benefits  due  to   duplication  of  points,  just  convenience  and
          flexibility.
          
    NOTE: this  function  is  re-entrant,  i.e.  you  may  use  same  rbfmodel
          structure in multiple threads calling  this function  for  different
          grids.

      -- ALGLIB --
         Copyright 04.03.2016 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfgridcalc2vsubset(rbfmodel s,
        double[] x0,
        int n0,
        double[] x1,
        int n1,
        bool[] flagy,
        ref double[] y,
        xparams _params)
    {
        int i = 0;

        y = new double[0];

        ap.assert(n0 > 0, "RBFGridCalc2VSubset: invalid value for N0 (N0<=0)!");
        ap.assert(n1 > 0, "RBFGridCalc2VSubset: invalid value for N1 (N1<=0)!");
        ap.assert(ap.len(x0) >= n0, "RBFGridCalc2VSubset: Length(X0)<N0");
        ap.assert(ap.len(x1) >= n1, "RBFGridCalc2VSubset: Length(X1)<N1");
        ap.assert(ap.len(flagy) >= n0 * n1, "RBFGridCalc2VSubset: Length(FlagY)<N0*N1*N2");
        ap.assert(apserv.isfinitevector(x0, n0, _params), "RBFGridCalc2VSubset: X0 contains infinite or NaN values!");
        ap.assert(apserv.isfinitevector(x1, n1, _params), "RBFGridCalc2VSubset: X1 contains infinite or NaN values!");
        for (i = 0; i <= n0 - 2; i++)
        {
            ap.assert((double)(x0[i]) <= (double)(x0[i + 1]), "RBFGridCalc2VSubset: X0 is not ordered by ascending");
        }
        for (i = 0; i <= n1 - 2; i++)
        {
            ap.assert((double)(x1[i]) <= (double)(x1[i + 1]), "RBFGridCalc2VSubset: X1 is not ordered by ascending");
        }
        rbfgridcalc2vx(s, x0, n0, x1, n1, flagy, true, ref y, _params);
    }


    /*************************************************************************
    This function calculates values of the RBF  model  at  the  regular  grid,
    which  has  N0*N1*N2  points,  with  Point[I,J,K] = (X0[I], X1[J], X2[K]).
    Vector-valued RBF models are supported.

    This function returns 0.0 when:
    * model is not initialized
    * NX<>3

      ! COMMERCIAL EDITION OF ALGLIB:
      ! 
      ! Commercial Edition of ALGLIB includes following important improvements
      ! of this function:
      ! * high-performance native backend with same C# interface (C# version)
      ! * multithreading support (C++ and C# versions)
      ! 
      ! We recommend you to read 'Working with commercial version' section  of
      ! ALGLIB Reference Manual in order to find out how to  use  performance-
      ! related features provided by commercial edition of ALGLIB.

    NOTE: Parallel  processing  is  implemented only for modern (hierarchical)
          RBFs. Legacy version 1 RBFs (created  by  QNN  or  RBF-ML) are still
          processed serially.

    INPUT PARAMETERS:
        S       -   RBF model, used in read-only mode, can be  shared  between
                    multiple   invocations  of  this  function  from  multiple
                    threads.
        
        X0      -   array of grid nodes, first coordinates, array[N0].
                    Must be ordered by ascending. Exception is generated
                    if the array is not correctly ordered.
        N0      -   grid size (number of nodes) in the first dimension
        
        X1      -   array of grid nodes, second coordinates, array[N1]
                    Must be ordered by ascending. Exception is generated
                    if the array is not correctly ordered.
        N1      -   grid size (number of nodes) in the second dimension
        
        X2      -   array of grid nodes, third coordinates, array[N2]
                    Must be ordered by ascending. Exception is generated
                    if the array is not correctly ordered.
        N2      -   grid size (number of nodes) in the third dimension

    OUTPUT PARAMETERS:
        Y       -   function values, array[NY*N0*N1*N2], where NY is a  number
                    of "output" vector values (this function  supports vector-
                    valued RBF models). Y is out-variable and  is  reallocated
                    by this function.
                    Y[K+NY*(I0+I1*N0+I2*N0*N1)]=F_k(X0[I0],X1[I1],X2[I2]), for:
                    *  K=0...NY-1
                    * I0=0...N0-1
                    * I1=0...N1-1
                    * I2=0...N2-1

    NOTE: this function supports weakly ordered grid nodes, i.e. you may  have                
          X[i]=X[i+1] for some i. It does  not  provide  you  any  performance
          benefits  due  to   duplication  of  points,  just  convenience  and
          flexibility.
          
    NOTE: this  function  is  re-entrant,  i.e.  you  may  use  same  rbfmodel
          structure in multiple threads calling  this function  for  different
          grids.
          
    NOTE: if you need function values on some subset  of  regular  grid, which
          may be described as "several compact and  dense  islands",  you  may
          use rbfgridcalc3vsubset().

      -- ALGLIB --
         Copyright 04.03.2016 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfgridcalc3v(rbfmodel s,
        double[] x0,
        int n0,
        double[] x1,
        int n1,
        double[] x2,
        int n2,
        ref double[] y,
        xparams _params)
    {
        int i = 0;
        bool[] dummy = new bool[0];

        y = new double[0];

        ap.assert(n0 > 0, "RBFGridCalc3V: invalid value for N0 (N0<=0)!");
        ap.assert(n1 > 0, "RBFGridCalc3V: invalid value for N1 (N1<=0)!");
        ap.assert(n2 > 0, "RBFGridCalc3V: invalid value for N2 (N2<=0)!");
        ap.assert(ap.len(x0) >= n0, "RBFGridCalc3V: Length(X0)<N0");
        ap.assert(ap.len(x1) >= n1, "RBFGridCalc3V: Length(X1)<N1");
        ap.assert(ap.len(x2) >= n2, "RBFGridCalc3V: Length(X2)<N2");
        ap.assert(apserv.isfinitevector(x0, n0, _params), "RBFGridCalc3V: X0 contains infinite or NaN values!");
        ap.assert(apserv.isfinitevector(x1, n1, _params), "RBFGridCalc3V: X1 contains infinite or NaN values!");
        ap.assert(apserv.isfinitevector(x2, n2, _params), "RBFGridCalc3V: X2 contains infinite or NaN values!");
        for (i = 0; i <= n0 - 2; i++)
        {
            ap.assert((double)(x0[i]) <= (double)(x0[i + 1]), "RBFGridCalc3V: X0 is not ordered by ascending");
        }
        for (i = 0; i <= n1 - 2; i++)
        {
            ap.assert((double)(x1[i]) <= (double)(x1[i + 1]), "RBFGridCalc3V: X1 is not ordered by ascending");
        }
        for (i = 0; i <= n2 - 2; i++)
        {
            ap.assert((double)(x2[i]) <= (double)(x2[i + 1]), "RBFGridCalc3V: X2 is not ordered by ascending");
        }
        rbfgridcalc3vx(s, x0, n0, x1, n1, x2, n2, dummy, false, ref y, _params);
    }


    /*************************************************************************
    This function calculates values of the RBF model at some subset of regular
    grid:
    * grid has N0*N1*N2 points, with Point[I,J,K] = (X0[I], X1[J], X2[K])
    * only values at some subset of this grid are required
    Vector-valued RBF models are supported.

    This function returns 0.0 when:
    * model is not initialized
    * NX<>3

      ! COMMERCIAL EDITION OF ALGLIB:
      ! 
      ! Commercial Edition of ALGLIB includes following important improvements
      ! of this function:
      ! * high-performance native backend with same C# interface (C# version)
      ! * multithreading support (C++ and C# versions)
      ! 
      ! We recommend you to read 'Working with commercial version' section  of
      ! ALGLIB Reference Manual in order to find out how to  use  performance-
      ! related features provided by commercial edition of ALGLIB.

    NOTE: Parallel  processing  is  implemented only for modern (hierarchical)
          RBFs. Legacy version 1 RBFs (created  by  QNN  or  RBF-ML) are still
          processed serially.

    INPUT PARAMETERS:
        S       -   RBF model, used in read-only mode, can be  shared  between
                    multiple   invocations  of  this  function  from  multiple
                    threads.
        
        X0      -   array of grid nodes, first coordinates, array[N0].
                    Must be ordered by ascending. Exception is generated
                    if the array is not correctly ordered.
        N0      -   grid size (number of nodes) in the first dimension
        
        X1      -   array of grid nodes, second coordinates, array[N1]
                    Must be ordered by ascending. Exception is generated
                    if the array is not correctly ordered.
        N1      -   grid size (number of nodes) in the second dimension
        
        X2      -   array of grid nodes, third coordinates, array[N2]
                    Must be ordered by ascending. Exception is generated
                    if the array is not correctly ordered.
        N2      -   grid size (number of nodes) in the third dimension
        
        FlagY   -   array[N0*N1*N2]:
                    * Y[I0+I1*N0+I2*N0*N1] corresponds to node (X0[I0],X1[I1],X2[I2])
                    * it is a "bitmap" array which contains  False  for  nodes
                      which are NOT calculated, and True for nodes  which  are
                      required.

    OUTPUT PARAMETERS:
        Y       -   function values, array[NY*N0*N1*N2], where NY is a  number
                    of "output" vector values (this function  supports vector-
                    valued RBF models):
                    * Y[K+NY*(I0+I1*N0+I2*N0*N1)]=F_k(X0[I0],X1[I1],X2[I2]),
                      for K=0...NY-1, I0=0...N0-1, I1=0...N1-1, I2=0...N2-1.
                    * elements of Y[] which correspond  to  FlagY[]=True   are
                      loaded by model values (which may be  exactly  zero  for
                      some nodes).
                    * elements of Y[] which correspond to FlagY[]=False MAY be
                      initialized by zeros OR may be calculated. This function
                      processes  grid  as  a  hierarchy  of  nested blocks and
                      micro-rows. If just one element of micro-row is required,
                      entire micro-row (up to 8 nodes in the current  version,
                      but no promises) is calculated.

    NOTE: this function supports weakly ordered grid nodes, i.e. you may  have                
          X[i]=X[i+1] for some i. It does  not  provide  you  any  performance
          benefits  due  to   duplication  of  points,  just  convenience  and
          flexibility.
          
    NOTE: this  function  is  re-entrant,  i.e.  you  may  use  same  rbfmodel
          structure in multiple threads calling  this function  for  different
          grids.

      -- ALGLIB --
         Copyright 04.03.2016 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfgridcalc3vsubset(rbfmodel s,
        double[] x0,
        int n0,
        double[] x1,
        int n1,
        double[] x2,
        int n2,
        bool[] flagy,
        ref double[] y,
        xparams _params)
    {
        int i = 0;

        y = new double[0];

        ap.assert(n0 > 0, "RBFGridCalc3VSubset: invalid value for N0 (N0<=0)!");
        ap.assert(n1 > 0, "RBFGridCalc3VSubset: invalid value for N1 (N1<=0)!");
        ap.assert(n2 > 0, "RBFGridCalc3VSubset: invalid value for N2 (N2<=0)!");
        ap.assert(ap.len(x0) >= n0, "RBFGridCalc3VSubset: Length(X0)<N0");
        ap.assert(ap.len(x1) >= n1, "RBFGridCalc3VSubset: Length(X1)<N1");
        ap.assert(ap.len(x2) >= n2, "RBFGridCalc3VSubset: Length(X2)<N2");
        ap.assert(ap.len(flagy) >= n0 * n1 * n2, "RBFGridCalc3VSubset: Length(FlagY)<N0*N1*N2");
        ap.assert(apserv.isfinitevector(x0, n0, _params), "RBFGridCalc3VSubset: X0 contains infinite or NaN values!");
        ap.assert(apserv.isfinitevector(x1, n1, _params), "RBFGridCalc3VSubset: X1 contains infinite or NaN values!");
        ap.assert(apserv.isfinitevector(x2, n2, _params), "RBFGridCalc3VSubset: X2 contains infinite or NaN values!");
        for (i = 0; i <= n0 - 2; i++)
        {
            ap.assert((double)(x0[i]) <= (double)(x0[i + 1]), "RBFGridCalc3VSubset: X0 is not ordered by ascending");
        }
        for (i = 0; i <= n1 - 2; i++)
        {
            ap.assert((double)(x1[i]) <= (double)(x1[i + 1]), "RBFGridCalc3VSubset: X1 is not ordered by ascending");
        }
        for (i = 0; i <= n2 - 2; i++)
        {
            ap.assert((double)(x2[i]) <= (double)(x2[i + 1]), "RBFGridCalc3VSubset: X2 is not ordered by ascending");
        }
        rbfgridcalc3vx(s, x0, n0, x1, n1, x2, n2, flagy, true, ref y, _params);
    }


    /*************************************************************************
    This function, depending on SparseY, acts as RBFGridCalc2V (SparseY=False)
    or RBFGridCalc2VSubset (SparseY=True) function.  See  comments  for  these
    functions for more information

      -- ALGLIB --
         Copyright 04.03.2016 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfgridcalc2vx(rbfmodel s,
        double[] x0,
        int n0,
        double[] x1,
        int n1,
        bool[] flagy,
        bool sparsey,
        ref double[] y,
        xparams _params)
    {
        int nx = 0;
        int ny = 0;
        int ylen = 0;
        hqrnd.hqrndstate rs = new hqrnd.hqrndstate();
        double[] dummyx2 = new double[0];
        double[] dummyx3 = new double[0];
        int i = 0;
        int j = 0;
        int k = 0;
        int l = 0;
        double[] tx = new double[0];
        double[] ty = new double[0];
        int dstoffs = 0;
        rbfcalcbuffer calcbuf = new rbfcalcbuffer();

        ap.assert(n0 > 0, "RBFGridCalc2VX: invalid value for N0 (N0<=0)!");
        ap.assert(n1 > 0, "RBFGridCalc2VX: invalid value for N1 (N1<=0)!");
        ap.assert(ap.len(x0) >= n0, "RBFGridCalc2VX: Length(X0)<N0");
        ap.assert(ap.len(x1) >= n1, "RBFGridCalc2VX: Length(X1)<N1");
        ap.assert(apserv.isfinitevector(x0, n0, _params), "RBFGridCalc2VX: X0 contains infinite or NaN values!");
        ap.assert(apserv.isfinitevector(x1, n1, _params), "RBFGridCalc2VX: X1 contains infinite or NaN values!");
        for (i = 0; i <= n0 - 2; i++)
        {
            ap.assert((double)(x0[i]) <= (double)(x0[i + 1]), "RBFGridCalc2VX: X0 is not ordered by ascending");
        }
        for (i = 0; i <= n1 - 2; i++)
        {
            ap.assert((double)(x1[i]) <= (double)(x1[i + 1]), "RBFGridCalc2VX: X1 is not ordered by ascending");
        }

        //
        // Prepare local variables
        //
        nx = s.nx;
        ny = s.ny;
        hqrnd.hqrndseed(325, 46345, rs, _params);

        //
        // Prepare output array
        //
        ylen = ny * n0 * n1;
        y = new double[ylen];
        for (i = 0; i <= ylen - 1; i++)
        {
            y[i] = 0;
        }
        if (s.nx != 2)
        {
            return;
        }

        //
        // Reference code for V3 models
        //
        if (s.modelversion == 3)
        {
            dummyx2 = new double[1];
            dummyx2[0] = 0;
            dummyx3 = new double[1];
            dummyx3[0] = 0;
            rbfv3.rbfv3gridcalcvx(s.model3, x0, n0, x1, n1, dummyx2, 1, dummyx3, 1, flagy, sparsey, y, _params);
            return;
        }

        //
        // Process V2 model
        //
        if (s.modelversion == 2)
        {
            dummyx2 = new double[1];
            dummyx2[0] = 0;
            dummyx3 = new double[1];
            dummyx3[0] = 0;
            rbfv2.rbfv2gridcalcvx(s.model2, x0, n0, x1, n1, dummyx2, 1, dummyx3, 1, flagy, sparsey, y, _params);
            return;
        }

        //
        // Reference code for V1 models
        //
        if (s.modelversion == 1)
        {
            tx = new double[nx];
            rbfcreatecalcbuffer(s, calcbuf, _params);
            for (i = 0; i <= n0 - 1; i++)
            {
                for (j = 0; j <= n1 - 1; j++)
                {
                    k = i + j * n0;
                    dstoffs = ny * k;
                    if (sparsey && !flagy[k])
                    {
                        for (l = 0; l <= ny - 1; l++)
                        {
                            y[l + dstoffs] = 0;
                        }
                        continue;
                    }
                    tx[0] = x0[i];
                    tx[1] = x1[j];
                    rbftscalcbuf(s, calcbuf, tx, ref ty, _params);
                    for (l = 0; l <= ny - 1; l++)
                    {
                        y[l + dstoffs] = ty[l];
                    }
                }
            }
            return;
        }

        //
        // Unknown model
        //
        ap.assert(false, "RBFGridCalc2VX: integrity check failed");
    }


    /*************************************************************************
    This function, depending on SparseY, acts as RBFGridCalc3V (SparseY=False)
    or RBFGridCalc3VSubset (SparseY=True) function.  See  comments  for  these
    functions for more information

      -- ALGLIB --
         Copyright 04.03.2016 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfgridcalc3vx(rbfmodel s,
        double[] x0,
        int n0,
        double[] x1,
        int n1,
        double[] x2,
        int n2,
        bool[] flagy,
        bool sparsey,
        ref double[] y,
        xparams _params)
    {
        int i = 0;
        int ylen = 0;
        int nx = 0;
        int ny = 0;
        double rmax = 0;
        int[] blocks0 = new int[0];
        int[] blocks1 = new int[0];
        int[] blocks2 = new int[0];
        int blockscnt0 = 0;
        int blockscnt1 = 0;
        int blockscnt2 = 0;
        double blockwidth = 0;
        double searchradius = 0;
        double avgfuncpernode = 0;
        int ntrials = 0;
        int maxblocksize = 0;
        rbfv1.gridcalc3v1buf bufseedv1 = new rbfv1.gridcalc3v1buf();
        smp.shared_pool bufpool = new smp.shared_pool();
        hqrnd.hqrndstate rs = new hqrnd.hqrndstate();
        double[] dummyx3 = new double[0];

        ap.assert(n0 > 0, "RBFGridCalc3V: invalid value for N0 (N0<=0)!");
        ap.assert(n1 > 0, "RBFGridCalc3V: invalid value for N1 (N1<=0)!");
        ap.assert(n2 > 0, "RBFGridCalc3V: invalid value for N2 (N2<=0)!");
        ap.assert(ap.len(x0) >= n0, "RBFGridCalc3V: Length(X0)<N0");
        ap.assert(ap.len(x1) >= n1, "RBFGridCalc3V: Length(X1)<N1");
        ap.assert(ap.len(x2) >= n2, "RBFGridCalc3V: Length(X2)<N2");
        ap.assert(apserv.isfinitevector(x0, n0, _params), "RBFGridCalc3V: X0 contains infinite or NaN values!");
        ap.assert(apserv.isfinitevector(x1, n1, _params), "RBFGridCalc3V: X1 contains infinite or NaN values!");
        ap.assert(apserv.isfinitevector(x2, n2, _params), "RBFGridCalc3V: X2 contains infinite or NaN values!");
        for (i = 0; i <= n0 - 2; i++)
        {
            ap.assert((double)(x0[i]) <= (double)(x0[i + 1]), "RBFGridCalc3V: X0 is not ordered by ascending");
        }
        for (i = 0; i <= n1 - 2; i++)
        {
            ap.assert((double)(x1[i]) <= (double)(x1[i + 1]), "RBFGridCalc3V: X1 is not ordered by ascending");
        }
        for (i = 0; i <= n2 - 2; i++)
        {
            ap.assert((double)(x2[i]) <= (double)(x2[i + 1]), "RBFGridCalc3V: X2 is not ordered by ascending");
        }

        //
        // Prepare local variables
        //
        nx = s.nx;
        ny = s.ny;
        hqrnd.hqrndseed(325, 46345, rs, _params);

        //
        // Prepare output array
        //
        ylen = ny * n0 * n1 * n2;
        y = new double[ylen];
        for (i = 0; i <= ylen - 1; i++)
        {
            y[i] = 0;
        }
        if (s.nx != 3)
        {
            return;
        }

        //
        // Process V1 model
        //
        if (s.modelversion == 1)
        {

            //
            // Fast exit for models without centers
            //
            if (s.model1.nc == 0)
            {
                return;
            }

            //
            // Prepare seed, create shared pool of temporary buffers
            //
            bufseedv1.cx = new double[nx];
            bufseedv1.tx = new double[nx];
            bufseedv1.ty = new double[ny];
            bufseedv1.expbuf0 = new double[n0];
            bufseedv1.expbuf1 = new double[n1];
            bufseedv1.expbuf2 = new double[n2];
            nearestneighbor.kdtreecreaterequestbuffer(s.model1.tree, bufseedv1.requestbuf, _params);
            smp.ae_shared_pool_set_seed(bufpool, bufseedv1);

            //
            // Analyze input grid:
            // * analyze average number of basis functions per grid node
            // * partition grid in into blocks
            //
            rmax = s.model1.rmax;
            blockwidth = 2 * rmax;
            maxblocksize = 8;
            searchradius = rmax * rbffarradius + 0.5 * Math.Sqrt(s.nx) * blockwidth;
            ntrials = 100;
            avgfuncpernode = 0.0;
            for (i = 0; i <= ntrials - 1; i++)
            {
                bufseedv1.tx[0] = x0[hqrnd.hqrnduniformi(rs, n0, _params)];
                bufseedv1.tx[1] = x1[hqrnd.hqrnduniformi(rs, n1, _params)];
                bufseedv1.tx[2] = x2[hqrnd.hqrnduniformi(rs, n2, _params)];
                avgfuncpernode = avgfuncpernode + (double)nearestneighbor.kdtreetsqueryrnn(s.model1.tree, bufseedv1.requestbuf, bufseedv1.tx, searchradius, true, _params) / (double)ntrials;
            }
            blocks0 = new int[n0 + 1];
            blockscnt0 = 0;
            blocks0[0] = 0;
            for (i = 1; i <= n0 - 1; i++)
            {
                if ((double)(x0[i] - x0[blocks0[blockscnt0]]) > (double)(blockwidth) || i - blocks0[blockscnt0] >= maxblocksize)
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
                if ((double)(x1[i] - x1[blocks1[blockscnt1]]) > (double)(blockwidth) || i - blocks1[blockscnt1] >= maxblocksize)
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
                if ((double)(x2[i] - x2[blocks2[blockscnt2]]) > (double)(blockwidth) || i - blocks2[blockscnt2] >= maxblocksize)
                {
                    apserv.inc(ref blockscnt2, _params);
                    blocks2[blockscnt2] = i;
                }
            }
            apserv.inc(ref blockscnt2, _params);
            blocks2[blockscnt2] = n2;

            //
            // Perform calculation in multithreaded mode
            //
            rbfv1.rbfv1gridcalc3vrec(s.model1, x0, n0, x1, n1, x2, n2, blocks0, 0, blockscnt0, blocks1, 0, blockscnt1, blocks2, 0, blockscnt2, flagy, sparsey, searchradius, avgfuncpernode, bufpool, y, _params);

            //
            // Done
            //
            return;
        }

        //
        // Process V2 model
        //
        if (s.modelversion == 2)
        {
            dummyx3 = new double[1];
            dummyx3[0] = 0;
            rbfv2.rbfv2gridcalcvx(s.model2, x0, n0, x1, n1, x2, n2, dummyx3, 1, flagy, sparsey, y, _params);
            return;
        }

        //
        // Process V3 model
        //
        if (s.modelversion == 3)
        {
            dummyx3 = new double[1];
            dummyx3[0] = 0;
            rbfv3.rbfv3gridcalcvx(s.model3, x0, n0, x1, n1, x2, n2, dummyx3, 1, flagy, sparsey, y, _params);
            return;
        }

        //
        // Unknown model
        //
        ap.assert(false, "RBFGridCalc3VX: integrity check failed");
    }


    /*************************************************************************
    This function "unpacks" RBF model by extracting its coefficients.

    INPUT PARAMETERS:
        S       -   RBF model

    OUTPUT PARAMETERS:
        NX      -   dimensionality of argument
        NY      -   dimensionality of the target function
        XWR     -   model  information ,  2D  array.  One  row  of  the  array
                    corresponds to one basis function.
                    
                    For ModelVersion=1 we have NX+NY+1 columns:
                    * first NX columns  - coordinates of the center 
                    * next  NY columns  - weights, one per dimension of the 
                                          function being modeled
                    * last column       - radius, same for all dimensions of
                                          the function being modeled
                    
                    For ModelVersion=2 we have NX+NY+NX columns:
                    * first NX columns  - coordinates of the center 
                    * next  NY columns  - weights, one per dimension of the 
                                          function being modeled
                    * last NX columns   - radii, one per dimension
                    
                    For ModelVersion=3 we have NX+NY+NX+3 columns:
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
        ModelVersion-version of the RBF model:
                    * 1 - for models created by QNN and RBF-ML algorithms,
                      compatible with ALGLIB 3.10 or earlier.
                    * 2 - for models created by HierarchicalRBF, requires
                      ALGLIB 3.11 or later
                    * 3 - for models created by DDM-RBF, requires
                      ALGLIB 3.19 or later

      -- ALGLIB --
         Copyright 13.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfunpack(rbfmodel s,
        ref int nx,
        ref int ny,
        ref double[,] xwr,
        ref int nc,
        ref double[,] v,
        ref int modelversion,
        xparams _params)
    {
        nx = 0;
        ny = 0;
        xwr = new double[0, 0];
        nc = 0;
        v = new double[0, 0];
        modelversion = 0;

        if (s.modelversion == 1)
        {
            modelversion = 1;
            rbfv1.rbfv1unpack(s.model1, ref nx, ref ny, ref xwr, ref nc, ref v, _params);
            return;
        }
        if (s.modelversion == 2)
        {
            modelversion = 2;
            rbfv2.rbfv2unpack(s.model2, ref nx, ref ny, ref xwr, ref nc, ref v, _params);
            return;
        }
        if (s.modelversion == 3)
        {
            modelversion = 3;
            rbfv3.rbfv3unpack(s.model3, ref nx, ref ny, ref xwr, ref nc, ref v, _params);
            return;
        }
        ap.assert(false, "RBFUnpack: integrity check failure");
    }


    /*************************************************************************
    This function returns model version.

    INPUT PARAMETERS:
        S       -   RBF model

    RESULT:
        * 1 - for models created by QNN and RBF-ML algorithms,
          compatible with ALGLIB 3.10 or earlier.
        * 2 - for models created by HierarchicalRBF, requires
          ALGLIB 3.11 or later

      -- ALGLIB --
         Copyright 06.07.2016 by Bochkanov Sergey
    *************************************************************************/
    public static int rbfgetmodelversion(rbfmodel s,
        xparams _params)
    {
        int result = 0;

        result = s.modelversion;
        return result;
    }


    /*************************************************************************
    This function is used to peek into hierarchical RBF  construction  process
    from  some  other  thread  and  get current progress indicator. It returns
    value in [0,1].

    IMPORTANT: only HRBFs (hierarchical RBFs) support  peeking  into  progress
               indicator. Legacy RBF-ML and RBF-QNN do  not  support  it.  You
               will always get 0 value.

    INPUT PARAMETERS:
        S           -   RBF model object

    RESULT:
        progress value, in [0,1]

      -- ALGLIB --
         Copyright 17.11.2018 by Bochkanov Sergey
    *************************************************************************/
    public static double rbfpeekprogress(rbfmodel s,
        xparams _params)
    {
        double result = 0;

        result = (double)s.progress10000 / (double)10000;
        return result;
    }


    /*************************************************************************
    This function  is  used  to  submit  a  request  for  termination  of  the
    hierarchical RBF construction process from some other thread.  As  result,
    RBF construction is terminated smoothly (with proper deallocation  of  all
    necessary resources) and resultant model is filled by zeros.

    A rep.terminationtype=8 will be returned upon receiving such request.

    IMPORTANT: only  HRBFs  (hierarchical  RBFs) support termination requests.
               Legacy RBF-ML and RBF-QNN do not  support  it.  An  attempt  to
               terminate their construction will be ignored.

    IMPORTANT: termination request flag is cleared when the model construction
               starts. Thus, any pre-construction termination requests will be
               silently ignored - only ones submitted AFTER  construction  has
               actually began will be handled.

    INPUT PARAMETERS:
        S           -   RBF model object

      -- ALGLIB --
         Copyright 17.11.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfrequesttermination(rbfmodel s,
        xparams _params)
    {
        s.terminationrequest = true;
    }


    /*************************************************************************
    This function sets RBF profile to standard or debug

    INPUT PARAMETERS:
        S           -   RBF model object
        P           -   profile type:
                        * 0 for standard
                        * -1 for debug
                        * -2 for debug with artificially worsened numerical
                          precision. This profile is designed to test algorithm
                          ability to deal with difficult problems,

      -- ALGLIB --
         Copyright 17.11.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfsetprofile(rbfmodel s,
        int p,
        xparams _params)
    {
        ap.assert((p == -2 || p == -1) || p == 0, "RBFSetProfile: incorrect P");
        s.rbfprofile = p;
    }


    /*************************************************************************
    This function changes evaluation tolerance of a fast evaluator (if present).
    It is an actual implementation that is used by RBFSetFastEvalTol().

    It usually has O(N) running time because evaluator has to be rebuilt according
    to the new tolerance.

    INPUT PARAMETERS:
        S           -   RBF model object
        TOL         -   desired tolerance

      -- ALGLIB --
         Copyright 19.09.2022 by Bochkanov Sergey
    *************************************************************************/
    public static void pushfastevaltol(rbfmodel s,
        double tol,
        xparams _params)
    {
        if (s.modelversion != 3)
        {
            return;
        }
        rbfv3.rbf3pushfastevaltol(s.model3, tol, _params);
    }


    /*************************************************************************
    Serializer: allocation

      -- ALGLIB --
         Copyright 02.02.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfalloc(serializer s,
        rbfmodel model,
        xparams _params)
    {

        //
        // Header
        //
        s.alloc_entry();

        //
        // V1 model
        //
        if (model.modelversion == 1)
        {

            //
            // Header
            //
            s.alloc_entry();
            rbfv1.rbfv1alloc(s, model.model1, _params);
            return;
        }

        //
        // V2 model
        //
        if (model.modelversion == 2)
        {

            //
            // Header
            //
            s.alloc_entry();
            rbfv2.rbfv2alloc(s, model.model2, _params);
            return;
        }

        //
        // V3 model
        //
        if (model.modelversion == 3)
        {

            //
            // Header
            //
            s.alloc_entry();
            rbfv3.rbfv3alloc(s, model.model3, _params);
            return;
        }
        ap.assert(false);
    }


    /*************************************************************************
    Serializer: serialization

      -- ALGLIB --
         Copyright 02.02.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfserialize(serializer s,
        rbfmodel model,
        xparams _params)
    {

        //
        // Header
        //
        s.serialize_int(scodes.getrbfserializationcode(_params));

        //
        // V1 model
        //
        if (model.modelversion == 1)
        {
            s.serialize_int(rbffirstversion);
            rbfv1.rbfv1serialize(s, model.model1, _params);
            return;
        }

        //
        // V2 model
        //
        if (model.modelversion == 2)
        {

            //
            // Header
            //
            s.serialize_int(rbfversion2);
            rbfv2.rbfv2serialize(s, model.model2, _params);
            return;
        }

        //
        // V3 model
        //
        if (model.modelversion == 3)
        {

            //
            // Header
            //
            s.serialize_int(rbfversion3);
            rbfv3.rbfv3serialize(s, model.model3, _params);
            return;
        }
        ap.assert(false);
    }


    /*************************************************************************
    Serializer: unserialization

      -- ALGLIB --
         Copyright 02.02.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void rbfunserialize(serializer s,
        rbfmodel model,
        xparams _params)
    {
        int i0 = 0;
        int i1 = 0;

        rbfpreparenonserializablefields(model, _params);

        //
        // Header
        //
        i0 = s.unserialize_int();
        ap.assert(i0 == scodes.getrbfserializationcode(_params), "RBFUnserialize: stream header corrupted");
        i1 = s.unserialize_int();
        ap.assert((i1 == rbffirstversion || i1 == rbfversion2) || i1 == rbfversion3, "RBFUnserialize: stream header corrupted");

        //
        // V1 model
        //
        if (i1 == rbffirstversion)
        {
            rbfv1.rbfv1unserialize(s, model.model1, _params);
            model.modelversion = 1;
            model.ny = model.model1.ny;
            model.nx = model.model1.nx;
            initializev2(model.nx, model.ny, model.model2, _params);
            initializev3(model.nx, model.ny, model.model3, _params);
            rbfcreatecalcbuffer(model, model.calcbuf, _params);
            pushfastevaltol(model, model.fastevaltol, _params);
            return;
        }

        //
        // V2 model
        //
        if (i1 == rbfversion2)
        {
            rbfv2.rbfv2unserialize(s, model.model2, _params);
            model.modelversion = 2;
            model.ny = model.model2.ny;
            model.nx = model.model2.nx;
            initializev1(model.nx, model.ny, model.model1, _params);
            initializev3(model.nx, model.ny, model.model3, _params);
            rbfcreatecalcbuffer(model, model.calcbuf, _params);
            pushfastevaltol(model, model.fastevaltol, _params);
            return;
        }

        //
        // V3 model
        //
        if (i1 == rbfversion3)
        {
            rbfv3.rbfv3unserialize(s, model.model3, _params);
            model.modelversion = 3;
            model.ny = model.model3.ny;
            model.nx = model.model3.nx;
            initializev1(model.nx, model.ny, model.model1, _params);
            initializev2(model.nx, model.ny, model.model2, _params);
            rbfcreatecalcbuffer(model, model.calcbuf, _params);
            pushfastevaltol(model, model.fastevaltol, _params);
            return;
        }
        ap.assert(false, "RBF: unserialiation error (unexpected model type)");
    }


    /*************************************************************************
    Initialize empty model

      -- ALGLIB --
         Copyright 12.05.2016 by Bochkanov Sergey
    *************************************************************************/
    private static void rbfpreparenonserializablefields(rbfmodel s,
        xparams _params)
    {
        s.n = 0;
        s.hasscale = false;
        s.radvalue = 1;
        s.radzvalue = 5;
        s.nlayers = 0;
        s.lambdav = 0;
        s.aterm = 1;
        s.algorithmtype = 0;
        s.rbfprofile = 0;
        s.epsort = eps;
        s.epserr = eps;
        s.maxits = 0;
        s.v3tol = 1.0E-6;
        s.nnmaxits = 100;
        s.fastevaltol = 1.0E-3;
    }


    /*************************************************************************
    Initialize V1 model (skip initialization for NX=1 or NX>3)

      -- ALGLIB --
         Copyright 12.05.2016 by Bochkanov Sergey
    *************************************************************************/
    private static void initializev1(int nx,
        int ny,
        rbfv1.rbfv1model s,
        xparams _params)
    {
        if (nx == 2 || nx == 3)
        {
            rbfv1.rbfv1create(nx, ny, s, _params);
        }
    }


    /*************************************************************************
    Initialize V2 model

      -- ALGLIB --
         Copyright 12.05.2016 by Bochkanov Sergey
    *************************************************************************/
    private static void initializev2(int nx,
        int ny,
        rbfv2.rbfv2model s,
        xparams _params)
    {
        rbfv2.rbfv2create(nx, ny, s, _params);
    }


    /*************************************************************************
    Initialize V3 model

      -- ALGLIB --
         Copyright 12.05.2016 by Bochkanov Sergey
    *************************************************************************/
    private static void initializev3(int nx,
        int ny,
        rbfv3.rbfv3model s,
        xparams _params)
    {
        rbfv3.rbfv3create(nx, ny, 2, 0, s, _params);
    }


    /*************************************************************************
    Cleans report fields

      -- ALGLIB --
         Copyright 16.06.2016 by Bochkanov Sergey
    *************************************************************************/
    private static void clearreportfields(rbfreport rep,
        xparams _params)
    {
        rep.rmserror = Double.NaN;
        rep.maxerror = Double.NaN;
        rep.arows = 0;
        rep.acols = 0;
        rep.annz = 0;
        rep.iterationscount = 0;
        rep.nmv = 0;
        rep.terminationtype = 0;
    }


}
