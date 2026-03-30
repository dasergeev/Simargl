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


public class idw
{
    /*************************************************************************
    Buffer  object  which  is  used  to  perform  evaluation  requests  in  the
    multithreaded mode (multiple threads working with same IDW object).

    This object should be created with idwcreatecalcbuffer().
    *************************************************************************/
    public class idwcalcbuffer : apobject
    {
        public double[] x;
        public double[] y;
        public double[] tsyw;
        public double[] tsw;
        public double[,] tsxy;
        public double[] tsdist;
        public nearestneighbor.kdtreerequestbuffer requestbuffer;
        public idwcalcbuffer()
        {
            init();
        }
        public override void init()
        {
            x = new double[0];
            y = new double[0];
            tsyw = new double[0];
            tsw = new double[0];
            tsxy = new double[0, 0];
            tsdist = new double[0];
            requestbuffer = new nearestneighbor.kdtreerequestbuffer();
        }
        public override apobject make_copy()
        {
            idwcalcbuffer _result = new idwcalcbuffer();
            _result.x = (double[])x.Clone();
            _result.y = (double[])y.Clone();
            _result.tsyw = (double[])tsyw.Clone();
            _result.tsw = (double[])tsw.Clone();
            _result.tsxy = (double[,])tsxy.Clone();
            _result.tsdist = (double[])tsdist.Clone();
            _result.requestbuffer = (nearestneighbor.kdtreerequestbuffer)requestbuffer.make_copy();
            return _result;
        }
    };


    /*************************************************************************
    IDW (Inverse Distance Weighting) model object.
    *************************************************************************/
    public class idwmodel : apobject
    {
        public int nx;
        public int ny;
        public double[] globalprior;
        public int algotype;
        public int nlayers;
        public double r0;
        public double rdecay;
        public double lambda0;
        public double lambdalast;
        public double lambdadecay;
        public double shepardp;
        public nearestneighbor.kdtree tree;
        public int npoints;
        public double[] shepardxy;
        public idwcalcbuffer buffer;
        public idwmodel()
        {
            init();
        }
        public override void init()
        {
            globalprior = new double[0];
            tree = new nearestneighbor.kdtree();
            shepardxy = new double[0];
            buffer = new idwcalcbuffer();
        }
        public override apobject make_copy()
        {
            idwmodel _result = new idwmodel();
            _result.nx = nx;
            _result.ny = ny;
            _result.globalprior = (double[])globalprior.Clone();
            _result.algotype = algotype;
            _result.nlayers = nlayers;
            _result.r0 = r0;
            _result.rdecay = rdecay;
            _result.lambda0 = lambda0;
            _result.lambdalast = lambdalast;
            _result.lambdadecay = lambdadecay;
            _result.shepardp = shepardp;
            _result.tree = (nearestneighbor.kdtree)tree.make_copy();
            _result.npoints = npoints;
            _result.shepardxy = (double[])shepardxy.Clone();
            _result.buffer = (idwcalcbuffer)buffer.make_copy();
            return _result;
        }
    };


    /*************************************************************************
    Builder object used to generate IDW (Inverse Distance Weighting) model.
    *************************************************************************/
    public class idwbuilder : apobject
    {
        public int priortermtype;
        public double[] priortermval;
        public int algotype;
        public int nlayers;
        public double r0;
        public double rdecay;
        public double lambda0;
        public double lambdalast;
        public double lambdadecay;
        public double shepardp;
        public double[] xy;
        public int npoints;
        public int nx;
        public int ny;
        public double[,] tmpxy;
        public double[,] tmplayers;
        public int[] tmptags;
        public double[] tmpdist;
        public double[] tmpx;
        public double[] tmpwy;
        public double[] tmpw;
        public nearestneighbor.kdtree tmptree;
        public double[] tmpmean;
        public idwbuilder()
        {
            init();
        }
        public override void init()
        {
            priortermval = new double[0];
            xy = new double[0];
            tmpxy = new double[0, 0];
            tmplayers = new double[0, 0];
            tmptags = new int[0];
            tmpdist = new double[0];
            tmpx = new double[0];
            tmpwy = new double[0];
            tmpw = new double[0];
            tmptree = new nearestneighbor.kdtree();
            tmpmean = new double[0];
        }
        public override apobject make_copy()
        {
            idwbuilder _result = new idwbuilder();
            _result.priortermtype = priortermtype;
            _result.priortermval = (double[])priortermval.Clone();
            _result.algotype = algotype;
            _result.nlayers = nlayers;
            _result.r0 = r0;
            _result.rdecay = rdecay;
            _result.lambda0 = lambda0;
            _result.lambdalast = lambdalast;
            _result.lambdadecay = lambdadecay;
            _result.shepardp = shepardp;
            _result.xy = (double[])xy.Clone();
            _result.npoints = npoints;
            _result.nx = nx;
            _result.ny = ny;
            _result.tmpxy = (double[,])tmpxy.Clone();
            _result.tmplayers = (double[,])tmplayers.Clone();
            _result.tmptags = (int[])tmptags.Clone();
            _result.tmpdist = (double[])tmpdist.Clone();
            _result.tmpx = (double[])tmpx.Clone();
            _result.tmpwy = (double[])tmpwy.Clone();
            _result.tmpw = (double[])tmpw.Clone();
            _result.tmptree = (nearestneighbor.kdtree)tmptree.make_copy();
            _result.tmpmean = (double[])tmpmean.Clone();
            return _result;
        }
    };


    /*************************************************************************
    IDW fitting report:
        rmserror        RMS error
        avgerror        average error
        maxerror        maximum error
        r2              coefficient of determination,  R-squared, 1-RSS/TSS
    *************************************************************************/
    public class idwreport : apobject
    {
        public double rmserror;
        public double avgerror;
        public double maxerror;
        public double r2;
        public idwreport()
        {
            init();
        }
        public override void init()
        {
        }
        public override apobject make_copy()
        {
            idwreport _result = new idwreport();
            _result.rmserror = rmserror;
            _result.avgerror = avgerror;
            _result.maxerror = maxerror;
            _result.r2 = r2;
            return _result;
        }
    };




    public const double w0 = 1.0;
    public const double meps = 1.0E-50;
    public const int defaultnlayers = 16;
    public const double defaultlambda0 = 0.3333;


    /*************************************************************************
    This function creates buffer  structure  which  can  be  used  to  perform
    parallel  IDW  model  evaluations  (with  one  IDW  model  instance  being
    used from multiple threads, as long as  different  threads  use  different
    instances of buffer).

    This buffer object can be used with  idwtscalcbuf()  function  (here  "ts"
    stands for "thread-safe", "buf" is a suffix which denotes  function  which
    reuses previously allocated output space).

    How to use it:
    * create IDW model structure or load it from file
    * call idwcreatecalcbuffer(), once per thread working with IDW model  (you
      should call this function only AFTER model initialization, see below for
      more information)
    * call idwtscalcbuf() from different threads,  with  each  thread  working
      with its own copy of buffer object.

    INPUT PARAMETERS
        S           -   IDW model

    OUTPUT PARAMETERS
        Buf         -   external buffer.
        
        
    IMPORTANT: buffer object should be used only with  IDW model object  which
               was used to initialize buffer. Any attempt to use buffer   with
               different object is dangerous - you may  get  memory  violation
               error because sizes of internal arrays do not fit to dimensions
               of the IDW structure.
               
    IMPORTANT: you  should  call  this function only for model which was built
               with model builder (or unserialized from file). Sizes  of  some
               internal structures are determined only after model  is  built,
               so buffer object created before model construction  stage  will
               be useless (and any attempt to use it will result in exception).

      -- ALGLIB --
         Copyright 22.10.2018 by Sergey Bochkanov
    *************************************************************************/
    public static void idwcreatecalcbuffer(idwmodel s,
        idwcalcbuffer buf,
        xparams _params)
    {
        ap.assert(s.nx >= 1, "IDWCreateCalcBuffer: integrity check failed");
        ap.assert(s.ny >= 1, "IDWCreateCalcBuffer: integrity check failed");
        ap.assert(s.nlayers >= 0, "IDWCreateCalcBuffer: integrity check failed");
        ap.assert(s.algotype >= 0, "IDWCreateCalcBuffer: integrity check failed");
        if (s.nlayers >= 1 && s.algotype != 0)
        {
            nearestneighbor.kdtreecreaterequestbuffer(s.tree, buf.requestbuffer, _params);
        }
        apserv.rvectorsetlengthatleast(ref buf.x, s.nx, _params);
        apserv.rvectorsetlengthatleast(ref buf.y, s.ny, _params);
        apserv.rvectorsetlengthatleast(ref buf.tsyw, s.ny * Math.Max(s.nlayers, 1), _params);
        apserv.rvectorsetlengthatleast(ref buf.tsw, Math.Max(s.nlayers, 1), _params);
    }


    /*************************************************************************
    This subroutine creates builder object used  to  generate IDW  model  from
    irregularly sampled (scattered) dataset.  Multidimensional  scalar/vector-
    -valued are supported.

    Builder object is used to fit model to data as follows:
    * builder object is created with idwbuildercreate() function
    * dataset is added with idwbuildersetpoints() function
    * one of the modern IDW algorithms is chosen with either:
      * idwbuildersetalgomstab()            - Multilayer STABilized algorithm (interpolation)
      Alternatively, one of the textbook algorithms can be chosen (not recommended):
      * idwbuildersetalgotextbookshepard()  - textbook Shepard algorithm
      * idwbuildersetalgotextbookmodshepard()-textbook modified Shepard algorithm
    * finally, model construction is performed with idwfit() function.

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

    INPUT PARAMETERS:
        NX  -   dimensionality of the argument, NX>=1
        NY  -   dimensionality of the function being modeled, NY>=1;
                NY=1 corresponds to classic scalar function, NY>=1 corresponds
                to vector-valued function.
        
    OUTPUT PARAMETERS:
        State-  builder object

      -- ALGLIB PROJECT --
         Copyright 22.10.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void idwbuildercreate(int nx,
        int ny,
        idwbuilder state,
        xparams _params)
    {
        ap.assert(nx >= 1, "IDWBuilderCreate: NX<=0");
        ap.assert(ny >= 1, "IDWBuilderCreate: NY<=0");

        //
        // We choose reasonable defaults for the algorithm:
        // * MSTAB algorithm
        // * 12 layers
        // * default radius
        // * default Lambda0
        //
        state.algotype = 2;
        state.priortermtype = 2;
        apserv.rvectorsetlengthatleast(ref state.priortermval, ny, _params);
        state.nlayers = defaultnlayers;
        state.r0 = 0;
        state.rdecay = 0.5;
        state.lambda0 = defaultlambda0;
        state.lambdalast = 0;
        state.lambdadecay = 1.0;

        //
        // Other parameters, not used but initialized
        //
        state.shepardp = 0;

        //
        // Initial dataset is empty
        //
        state.npoints = 0;
        state.nx = nx;
        state.ny = ny;
    }


    /*************************************************************************
    This function changes number of layers used by IDW-MSTAB algorithm.

    The more layers you have, the finer details can  be  reproduced  with  IDW
    model. The less layers you have, the less memory and CPU time is  consumed
    by the model.

    Memory consumption grows linearly with layers count,  running  time  grows
    sub-linearly.

    The default number of layers is 16, which allows you to reproduce  details
    at distance down to SRad/65536. You will rarely need to change it.

    INPUT PARAMETERS:
        State   -   builder object
        NLayers -   NLayers>=1, the number of layers used by the model.

      -- ALGLIB --
         Copyright 22.10.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void idwbuildersetnlayers(idwbuilder state,
        int nlayers,
        xparams _params)
    {
        ap.assert(nlayers >= 1, "IDWBuilderSetNLayers: N<1");
        state.nlayers = nlayers;
    }


    /*************************************************************************
    This function adds dataset to the builder object.

    This function overrides results of the previous calls, i.e. multiple calls
    of this function will result in only the last set being added.

    INPUT PARAMETERS:
        State   -   builder object
        XY      -   points, array[N,NX+NY]. One row  corresponds to  one point
                    in the dataset. First NX elements  are  coordinates,  next
                    NY elements are function values. Array may  be larger than 
                    specified, in  this  case  only leading [N,NX+NY] elements 
                    will be used.
        N       -   number of points in the dataset, N>=0.

      -- ALGLIB --
         Copyright 22.10.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void idwbuildersetpoints(idwbuilder state,
        double[,] xy,
        int n,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int ew = 0;

        ap.assert(n >= 0, "IDWBuilderSetPoints: N<0");
        ap.assert(ap.rows(xy) >= n, "IDWBuilderSetPoints: Rows(XY)<N");
        ap.assert(n == 0 || ap.cols(xy) >= state.nx + state.ny, "IDWBuilderSetPoints: Cols(XY)<NX+NY");
        ap.assert(apserv.apservisfinitematrix(xy, n, state.nx + state.ny, _params), "IDWBuilderSetPoints: XY contains infinite or NaN values!");
        state.npoints = n;
        ew = state.nx + state.ny;
        apserv.rvectorsetlengthatleast(ref state.xy, n * ew, _params);
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= ew - 1; j++)
            {
                state.xy[i * ew + j] = xy[i, j];
            }
        }
    }


    /*************************************************************************
    This function sets IDW model  construction  algorithm  to  the  Multilayer
    Stabilized IDW method (IDW-MSTAB), a  latest  incarnation  of  the inverse
    distance weighting interpolation which fixes shortcomings of  the original
    and modified Shepard's variants.

    The distinctive features of IDW-MSTAB are:
    1) exact interpolation  is  pursued  (as  opposed  to  fitting  and  noise
       suppression)
    2) improved robustness when compared with that of other algorithms:
       * MSTAB shows almost no strange  fitting  artifacts  like  ripples  and
         sharp spikes (unlike N-dimensional splines and HRBFs)
       * MSTAB does not return function values far from the  interval  spanned
         by the dataset; say, if all your points have |f|<=1, you  can be sure
         that model value won't deviate too much from [-1,+1]
    3) good model construction time competing with that of HRBFs  and  bicubic
       splines
    4) ability to work with any number of dimensions, starting from NX=1

    The drawbacks of IDW-MSTAB (and all IDW algorithms in general) are:
    1) dependence of the model evaluation time on the search radius
    2) bad extrapolation properties, models built by this method  are  usually
       conservative in their predictions

    Thus, IDW-MSTAB is  a  good  "default"  option  if  you  want  to  perform
    scattered multidimensional interpolation. Although it has  its  drawbacks,
    it is easy to use and robust, which makes it a good first step.


    INPUT PARAMETERS:
        State   -   builder object
        SRad    -   initial search radius, SRad>0 is required. A model  value
                    is obtained by "smart" averaging  of  the  dataset  points
                    within search radius.

    NOTE 1: IDW interpolation can  correctly  handle  ANY  dataset,  including
            datasets with non-distinct points. In case non-distinct points are
            found, an average value for this point will be calculated.
            
    NOTE 2: the memory requirements for model storage are O(NPoints*NLayers).
            The model construction needs twice as much memory as model storage.
      
    NOTE 3: by default 16 IDW layers are built which is enough for most cases.
            You can change this parameter with idwbuildersetnlayers()  method.
            Larger values may be necessary if you need to reproduce  extrafine
            details at distances smaller than SRad/65536.  Smaller value   may
            be necessary if you have to save memory and  computing  time,  and
            ready to sacrifice some model quality.


    ALGORITHM DESCRIPTION
      
    ALGLIB implementation of IDW is somewhat similar to the modified Shepard's
    method (one with search radius R) but overcomes several of its  drawbacks,
    namely:
    1) a tendency to show stepwise behavior for uniform datasets
    2) a tendency to show terrible interpolation properties for highly
       nonuniform datasets which often arise in geospatial tasks
      (function values are densely sampled across multiple separated
      "tracks")

    IDW-MSTAB method performs several passes over dataset and builds a sequence
    of progressively refined IDW models  (layers),  which starts from one with
    largest search radius SRad  and continues to smaller  search  radii  until
    required number of  layers  is  built.  Highest  layers  reproduce  global
    behavior of the target function at larger distances  whilst  lower  layers
    reproduce fine details at smaller distances.

    Each layer is an IDW model built with following modifications:
    * weights go to zero when distance approach to the current search radius
    * an additional regularizing term is added to the distance: w=1/(d^2+lambda)
    * an additional fictional term with unit weight and zero function value is
      added in order to promote continuity  properties  at  the  isolated  and
      boundary points
      
    By default, 16 layers is built, which is enough for most  cases.  You  can
    change this parameter with idwbuildersetnlayers() method.
       
      -- ALGLIB --
         Copyright 22.10.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void idwbuildersetalgomstab(idwbuilder state,
        double srad,
        xparams _params)
    {
        ap.assert(math.isfinite(srad), "IDWBuilderSetAlgoMSTAB: SRad is not finite");
        ap.assert((double)(srad) > (double)(0), "IDWBuilderSetAlgoMSTAB: SRad<=0");

        //
        // Set algorithm
        //
        state.algotype = 2;

        //
        // Set options
        //
        state.r0 = srad;
        state.rdecay = 0.5;
        state.lambda0 = defaultlambda0;
        state.lambdalast = 0;
        state.lambdadecay = 1.0;
    }


    /*************************************************************************
    This function sets  IDW  model  construction  algorithm  to  the  textbook
    Shepard's algorithm with custom (user-specified) power parameter.

    IMPORTANT: we do NOT recommend using textbook IDW algorithms because  they
               have terrible interpolation properties. Use MSTAB in all cases.

    INPUT PARAMETERS:
        State   -   builder object
        P       -   power parameter, P>0; good value to start with is 2.0

    NOTE 1: IDW interpolation can  correctly  handle  ANY  dataset,  including
            datasets with non-distinct points. In case non-distinct points are
            found, an average value for this point will be calculated.
       
      -- ALGLIB --
         Copyright 22.10.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void idwbuildersetalgotextbookshepard(idwbuilder state,
        double p,
        xparams _params)
    {
        ap.assert(math.isfinite(p), "IDWBuilderSetAlgoShepard: P is not finite");
        ap.assert((double)(p) > (double)(0), "IDWBuilderSetAlgoShepard: P<=0");

        //
        // Set algorithm and options
        //
        state.algotype = 0;
        state.shepardp = p;
    }


    /*************************************************************************
    This function sets  IDW  model  construction  algorithm  to the 'textbook'
    modified Shepard's algorithm with user-specified search radius.

    IMPORTANT: we do NOT recommend using textbook IDW algorithms because  they
               have terrible interpolation properties. Use MSTAB in all cases.

    INPUT PARAMETERS:
        State   -   builder object
        R       -   search radius

    NOTE 1: IDW interpolation can  correctly  handle  ANY  dataset,  including
            datasets with non-distinct points. In case non-distinct points are
            found, an average value for this point will be calculated.
       
      -- ALGLIB --
         Copyright 22.10.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void idwbuildersetalgotextbookmodshepard(idwbuilder state,
        double r,
        xparams _params)
    {
        ap.assert(math.isfinite(r), "IDWBuilderSetAlgoModShepard: R is not finite");
        ap.assert((double)(r) > (double)(0), "IDWBuilderSetAlgoModShepard: R<=0");

        //
        // Set algorithm and options
        //
        state.algotype = 1;
        state.r0 = r;
    }


    /*************************************************************************
    This function sets prior term (model value at infinity) as  user-specified
    value.

    INPUT PARAMETERS:
        S       -   spline builder
        V       -   value for user-defined prior
        
    NOTE: for vector-valued models all components of the prior are set to same
          user-specified value

      -- ALGLIB --
         Copyright 29.10.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void idwbuildersetuserterm(idwbuilder state,
        double v,
        xparams _params)
    {
        int j = 0;

        ap.assert(math.isfinite(v), "IDWBuilderSetUserTerm: infinite/NAN value passed");
        state.priortermtype = 0;
        for (j = 0; j <= state.ny - 1; j++)
        {
            state.priortermval[j] = v;
        }
    }


    /*************************************************************************
    This function sets constant prior term (model value at infinity).

    Constant prior term is determined as mean value over dataset.

    INPUT PARAMETERS:
        S       -   spline builder

      -- ALGLIB --
         Copyright 29.10.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void idwbuildersetconstterm(idwbuilder state,
        xparams _params)
    {
        state.priortermtype = 2;
    }


    /*************************************************************************
    This function sets zero prior term (model value at infinity).

    INPUT PARAMETERS:
        S       -   spline builder

      -- ALGLIB --
         Copyright 29.10.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void idwbuildersetzeroterm(idwbuilder state,
        xparams _params)
    {
        state.priortermtype = 3;
    }


    /*************************************************************************
    IDW interpolation: scalar target, 1-dimensional argument

    NOTE: this function modifies internal temporaries of the  IDW  model, thus
          IT IS NOT  THREAD-SAFE!  If  you  want  to  perform  parallel  model
          evaluation from the multiple threads, use idwtscalcbuf()  with  per-
          thread buffer object. 
          
    INPUT PARAMETERS:
        S   -   IDW interpolant built with IDW builder
        X0  -   argument value

    Result:
        IDW interpolant S(X0)

      -- ALGLIB --
         Copyright 22.10.2018 by Bochkanov Sergey
    *************************************************************************/
    public static double idwcalc1(idwmodel s,
        double x0,
        xparams _params)
    {
        double result = 0;

        ap.assert(s.nx == 1, "IDWCalc1: S.NX<>1");
        ap.assert(s.ny == 1, "IDWCalc1: S.NY<>1");
        ap.assert(math.isfinite(x0), "IDWCalc1: X0 is INF or NAN");
        s.buffer.x[0] = x0;
        idwtscalcbuf(s, s.buffer, s.buffer.x, ref s.buffer.y, _params);
        result = s.buffer.y[0];
        return result;
    }


    /*************************************************************************
    IDW interpolation: scalar target, 2-dimensional argument

    NOTE: this function modifies internal temporaries of the  IDW  model, thus
          IT IS NOT  THREAD-SAFE!  If  you  want  to  perform  parallel  model
          evaluation from the multiple threads, use idwtscalcbuf()  with  per-
          thread buffer object. 
          
    INPUT PARAMETERS:
        S       -   IDW interpolant built with IDW builder
        X0, X1  -   argument value

    Result:
        IDW interpolant S(X0,X1)

      -- ALGLIB --
         Copyright 22.10.2018 by Bochkanov Sergey
    *************************************************************************/
    public static double idwcalc2(idwmodel s,
        double x0,
        double x1,
        xparams _params)
    {
        double result = 0;

        ap.assert(s.nx == 2, "IDWCalc2: S.NX<>2");
        ap.assert(s.ny == 1, "IDWCalc2: S.NY<>1");
        ap.assert(math.isfinite(x0), "IDWCalc2: X0 is INF or NAN");
        ap.assert(math.isfinite(x1), "IDWCalc2: X1 is INF or NAN");
        s.buffer.x[0] = x0;
        s.buffer.x[1] = x1;
        idwtscalcbuf(s, s.buffer, s.buffer.x, ref s.buffer.y, _params);
        result = s.buffer.y[0];
        return result;
    }


    /*************************************************************************
    IDW interpolation: scalar target, 3-dimensional argument

    NOTE: this function modifies internal temporaries of the  IDW  model, thus
          IT IS NOT  THREAD-SAFE!  If  you  want  to  perform  parallel  model
          evaluation from the multiple threads, use idwtscalcbuf()  with  per-
          thread buffer object. 

    INPUT PARAMETERS:
        S       -   IDW interpolant built with IDW builder
        X0,X1,X2-   argument value

    Result:
        IDW interpolant S(X0,X1,X2)

      -- ALGLIB --
         Copyright 22.10.2018 by Bochkanov Sergey
    *************************************************************************/
    public static double idwcalc3(idwmodel s,
        double x0,
        double x1,
        double x2,
        xparams _params)
    {
        double result = 0;

        ap.assert(s.nx == 3, "IDWCalc3: S.NX<>3");
        ap.assert(s.ny == 1, "IDWCalc3: S.NY<>1");
        ap.assert(math.isfinite(x0), "IDWCalc3: X0 is INF or NAN");
        ap.assert(math.isfinite(x1), "IDWCalc3: X1 is INF or NAN");
        ap.assert(math.isfinite(x2), "IDWCalc3: X2 is INF or NAN");
        s.buffer.x[0] = x0;
        s.buffer.x[1] = x1;
        s.buffer.x[2] = x2;
        idwtscalcbuf(s, s.buffer, s.buffer.x, ref s.buffer.y, _params);
        result = s.buffer.y[0];
        return result;
    }


    /*************************************************************************
    This function calculates values of the IDW model at the given point.

    This is general function which can be used for arbitrary NX (dimension  of 
    the space of arguments) and NY (dimension of the function itself). However
    when  you  have  NY=1  you  may  find more convenient to  use  idwcalc1(),
    idwcalc2() or idwcalc3().

    NOTE: this function modifies internal temporaries of the  IDW  model, thus
          IT IS NOT  THREAD-SAFE!  If  you  want  to  perform  parallel  model
          evaluation from the multiple threads, use idwtscalcbuf()  with  per-
          thread buffer object. 
          
    INPUT PARAMETERS:
        S       -   IDW model
        X       -   coordinates, array[NX]. X may have more than NX  elements,
                    in this case only leading NX will be used.

    OUTPUT PARAMETERS:
        Y       -   function value, array[NY]. Y is out-parameter and will  be
                    reallocated after call to this function. In case you  want
                    to reuse previously allocated Y, you may use idwcalcbuf(),
                    which reallocates Y only when it is too small.

      -- ALGLIB --
         Copyright 22.10.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void idwcalc(idwmodel s,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        y = new double[0];

        idwtscalcbuf(s, s.buffer, x, ref y, _params);
    }


    /*************************************************************************
    This function calculates values of the IDW model at the given point.

    Same as idwcalc(), but does not reallocate Y when in is large enough to 
    store function values.

    NOTE: this function modifies internal temporaries of the  IDW  model, thus
          IT IS NOT  THREAD-SAFE!  If  you  want  to  perform  parallel  model
          evaluation from the multiple threads, use idwtscalcbuf()  with  per-
          thread buffer object. 
          
    INPUT PARAMETERS:
        S       -   IDW model
        X       -   coordinates, array[NX]. X may have more than NX  elements,
                    in this case only leading NX will be used.
        Y       -   possibly preallocated array

    OUTPUT PARAMETERS:
        Y       -   function value, array[NY]. Y is not reallocated when it
                    is larger than NY.

      -- ALGLIB --
         Copyright 22.10.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void idwcalcbuf(idwmodel s,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        idwtscalcbuf(s, s.buffer, x, ref y, _params);
    }


    /*************************************************************************
    This function calculates values of the IDW model at the given point, using
    external  buffer  object  (internal  temporaries  of  IDW  model  are  not
    modified).

    This function allows to use same IDW model object  in  different  threads,
    assuming  that  different   threads  use different instances of the buffer
    structure.

    INPUT PARAMETERS:
        S       -   IDW model, may be shared between different threads
        Buf     -   buffer object created for this particular instance of  IDW
                    model with idwcreatecalcbuffer().
        X       -   coordinates, array[NX]. X may have more than NX  elements,
                    in this case only  leading NX will be used.
        Y       -   possibly preallocated array

    OUTPUT PARAMETERS:
        Y       -   function value, array[NY]. Y is not reallocated when it
                    is larger than NY.

      -- ALGLIB --
         Copyright 13.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void idwtscalcbuf(idwmodel s,
        idwcalcbuffer buf,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int ew = 0;
        int k = 0;
        int layeridx = 0;
        int nx = 0;
        int ny = 0;
        int npoints = 0;
        double v = 0;
        double vv = 0;
        double f = 0;
        double p = 0;
        double r = 0;
        double eps = 0;
        double lambdacur = 0;
        double lambdadecay = 0;
        double invrdecay = 0;
        double invr = 0;
        bool fastcalcpossible = new bool();
        double wf0 = 0;
        double ws0 = 0;
        double wf1 = 0;
        double ws1 = 0;

        nx = s.nx;
        ny = s.ny;
        ap.assert(ap.len(x) >= nx, "IDWTsCalcBuf: Length(X)<NX");
        ap.assert(apserv.isfinitevector(x, nx, _params), "IDWTsCalcBuf: X contains infinite or NaN values");

        //
        // Avoid spurious compiler warnings
        //
        wf0 = 0;
        ws0 = 0;
        wf1 = 0;
        ws1 = 0;

        //
        // Allocate output
        //
        if (ap.len(y) < ny)
        {
            y = new double[ny];
        }

        //
        // Quick exit for NLayers=0 (no dataset)
        //
        if (s.nlayers == 0)
        {
            for (j = 0; j <= ny - 1; j++)
            {
                y[j] = s.globalprior[j];
            }
            return;
        }

        //
        // Textbook Shepard's method
        //
        if (s.algotype == 0)
        {
            npoints = s.npoints;
            ap.assert(npoints > 0, "IDWTsCalcBuf: integrity check failed");
            eps = 1.0E-50;
            ew = nx + ny;
            p = s.shepardp;
            for (j = 0; j <= ny - 1; j++)
            {
                y[j] = 0;
                buf.tsyw[j] = eps;
            }
            for (i = 0; i <= npoints - 1; i++)
            {

                //
                // Compute squared distance
                //
                v = 0;
                for (j = 0; j <= nx - 1; j++)
                {
                    vv = s.shepardxy[i * ew + j] - x[j];
                    v = v + vv * vv;
                }

                //
                // Compute weight (with small regularizing addition)
                //
                v = Math.Pow(v, p * 0.5);
                v = 1 / (eps + v);

                //
                // Accumulate
                //
                for (j = 0; j <= ny - 1; j++)
                {
                    y[j] = y[j] + v * s.shepardxy[i * ew + nx + j];
                    buf.tsyw[j] = buf.tsyw[j] + v;
                }
            }
            for (j = 0; j <= ny - 1; j++)
            {
                y[j] = y[j] / buf.tsyw[j] + s.globalprior[j];
            }
            return;
        }

        //
        // Textbook modified Shepard's method
        //
        if (s.algotype == 1)
        {
            eps = 1.0E-50;
            r = s.r0;
            for (j = 0; j <= ny - 1; j++)
            {
                y[j] = 0;
                buf.tsyw[j] = eps;
            }
            k = nearestneighbor.kdtreetsqueryrnn(s.tree, buf.requestbuffer, x, r, true, _params);
            nearestneighbor.kdtreetsqueryresultsxy(s.tree, buf.requestbuffer, ref buf.tsxy, _params);
            nearestneighbor.kdtreetsqueryresultsdistances(s.tree, buf.requestbuffer, ref buf.tsdist, _params);
            for (i = 0; i <= k - 1; i++)
            {
                v = buf.tsdist[i];
                v = (r - v) / (r * v + eps);
                v = v * v;
                for (j = 0; j <= ny - 1; j++)
                {
                    y[j] = y[j] + v * buf.tsxy[i, nx + j];
                    buf.tsyw[j] = buf.tsyw[j] + v;
                }
            }
            for (j = 0; j <= ny - 1; j++)
            {
                y[j] = y[j] / buf.tsyw[j] + s.globalprior[j];
            }
            return;
        }

        //
        // MSTAB
        //
        if (s.algotype == 2)
        {
            ap.assert((double)(w0) == (double)(1), "IDWTsCalcBuf: unexpected W0, integrity check failed");
            invrdecay = 1 / s.rdecay;
            invr = 1 / s.r0;
            lambdadecay = s.lambdadecay;
            fastcalcpossible = (ny == 1 && s.nlayers >= 3) && (double)(lambdadecay) == (double)(1);
            if (fastcalcpossible)
            {

                //
                // Important special case, NY=1, no lambda-decay,
                // we can perform optimized fast evaluation
                //
                wf0 = 0;
                ws0 = w0;
                wf1 = 0;
                ws1 = w0;
                for (j = 0; j <= s.nlayers - 1; j++)
                {
                    buf.tsyw[j] = 0;
                    buf.tsw[j] = w0;
                }
            }
            else
            {

                //
                // Setup variables for generic evaluation path
                //
                for (j = 0; j <= ny * s.nlayers - 1; j++)
                {
                    buf.tsyw[j] = 0;
                }
                for (j = 0; j <= s.nlayers - 1; j++)
                {
                    buf.tsw[j] = w0;
                }
            }
            k = nearestneighbor.kdtreetsqueryrnnu(s.tree, buf.requestbuffer, x, s.r0, true, _params);
            nearestneighbor.kdtreetsqueryresultsxy(s.tree, buf.requestbuffer, ref buf.tsxy, _params);
            nearestneighbor.kdtreetsqueryresultsdistances(s.tree, buf.requestbuffer, ref buf.tsdist, _params);
            for (i = 0; i <= k - 1; i++)
            {
                lambdacur = s.lambda0;
                vv = buf.tsdist[i] * invr;
                if (fastcalcpossible)
                {

                    //
                    // Important special case, fast evaluation possible
                    //
                    v = vv * vv;
                    v = (1 - v) * (1 - v) / (v + lambdacur);
                    f = buf.tsxy[i, nx + 0];
                    wf0 = wf0 + v * f;
                    ws0 = ws0 + v;
                    vv = vv * invrdecay;
                    if (vv >= 1.0)
                    {
                        continue;
                    }
                    v = vv * vv;
                    v = (1 - v) * (1 - v) / (v + lambdacur);
                    f = buf.tsxy[i, nx + 1];
                    wf1 = wf1 + v * f;
                    ws1 = ws1 + v;
                    vv = vv * invrdecay;
                    if (vv >= 1.0)
                    {
                        continue;
                    }
                    for (layeridx = 2; layeridx <= s.nlayers - 1; layeridx++)
                    {
                        if (layeridx == s.nlayers - 1)
                        {
                            lambdacur = s.lambdalast;
                        }
                        v = vv * vv;
                        v = (1 - v) * (1 - v) / (v + lambdacur);
                        f = buf.tsxy[i, nx + layeridx];
                        buf.tsyw[layeridx] = buf.tsyw[layeridx] + v * f;
                        buf.tsw[layeridx] = buf.tsw[layeridx] + v;
                        vv = vv * invrdecay;
                        if (vv >= 1.0)
                        {
                            break;
                        }
                    }
                }
                else
                {

                    //
                    // General case
                    //
                    for (layeridx = 0; layeridx <= s.nlayers - 1; layeridx++)
                    {
                        if (layeridx == s.nlayers - 1)
                        {
                            lambdacur = s.lambdalast;
                        }
                        if (vv >= 1.0)
                        {
                            break;
                        }
                        v = vv * vv;
                        v = (1 - v) * (1 - v) / (v + lambdacur);
                        for (j = 0; j <= ny - 1; j++)
                        {
                            f = buf.tsxy[i, nx + layeridx * ny + j];
                            buf.tsyw[layeridx * ny + j] = buf.tsyw[layeridx * ny + j] + v * f;
                        }
                        buf.tsw[layeridx] = buf.tsw[layeridx] + v;
                        lambdacur = lambdacur * lambdadecay;
                        vv = vv * invrdecay;
                    }
                }
            }
            if (fastcalcpossible)
            {

                //
                // Important special case, finalize evaluations
                //
                buf.tsyw[0] = wf0;
                buf.tsw[0] = ws0;
                buf.tsyw[1] = wf1;
                buf.tsw[1] = ws1;
            }
            for (j = 0; j <= ny - 1; j++)
            {
                y[j] = s.globalprior[j];
            }
            for (layeridx = 0; layeridx <= s.nlayers - 1; layeridx++)
            {
                for (j = 0; j <= ny - 1; j++)
                {
                    y[j] = y[j] + buf.tsyw[layeridx * ny + j] / buf.tsw[layeridx];
                }
            }
            return;
        }

        //
        //
        //
        ap.assert(false, "IDWTsCalcBuf: unexpected AlgoType");
    }


    /*************************************************************************
    This function fits IDW model to the dataset using current IDW construction
    algorithm. A model being built and fitting report are returned.

    INPUT PARAMETERS:
        State   -   builder object

    OUTPUT PARAMETERS:
        Model   -   an IDW model built with current algorithm
        Rep     -   model fitting report, fields of this structure contain
                    information about average fitting errors.
                    
    NOTE: although IDW-MSTAB algorithm is an  interpolation  method,  i.e.  it
          tries to fit the model exactly, it can  handle  datasets  with  non-
          distinct points which can not be fit exactly; in such  cases  least-
          squares fitting is performed.
       
      -- ALGLIB --
         Copyright 22.10.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void idwfit(idwbuilder state,
        idwmodel model,
        idwreport rep,
        xparams _params)
    {
        int i = 0;
        int i0 = 0;
        int j = 0;
        int k = 0;
        int layeridx = 0;
        int srcidx = 0;
        double v = 0;
        double vv = 0;
        int npoints = 0;
        int nx = 0;
        int ny = 0;
        double rcur = 0;
        double lambdacur = 0;
        double rss = 0;
        double tss = 0;

        nx = state.nx;
        ny = state.ny;
        npoints = state.npoints;

        //
        // Clear report fields
        //
        rep.rmserror = 0;
        rep.avgerror = 0;
        rep.maxerror = 0;
        rep.r2 = 1.0;

        //
        // Quick exit for empty dataset
        //
        if (state.npoints == 0)
        {
            model.nx = nx;
            model.ny = ny;
            model.globalprior = new double[ny];
            for (i = 0; i <= ny - 1; i++)
            {
                model.globalprior[i] = 0;
            }
            model.algotype = 0;
            model.nlayers = 0;
            model.r0 = 1;
            model.rdecay = 0.5;
            model.lambda0 = 0;
            model.lambdalast = 0;
            model.lambdadecay = 1;
            model.shepardp = 2;
            model.npoints = 0;
            idwcreatecalcbuffer(model, model.buffer, _params);
            return;
        }

        //
        // Compute temporaries which will be required later:
        // * global mean
        //
        ap.assert(state.npoints > 0, "IDWFit: integrity check failed");
        apserv.rvectorsetlengthatleast(ref state.tmpmean, ny, _params);
        for (j = 0; j <= ny - 1; j++)
        {
            state.tmpmean[j] = 0;
        }
        for (i = 0; i <= npoints - 1; i++)
        {
            for (j = 0; j <= ny - 1; j++)
            {
                state.tmpmean[j] = state.tmpmean[j] + state.xy[i * (nx + ny) + nx + j];
            }
        }
        for (j = 0; j <= ny - 1; j++)
        {
            state.tmpmean[j] = state.tmpmean[j] / npoints;
        }

        //
        // Compute global prior
        //
        // NOTE: for original Shepard's method it is always mean value
        //
        apserv.rvectorsetlengthatleast(ref model.globalprior, ny, _params);
        for (j = 0; j <= ny - 1; j++)
        {
            model.globalprior[j] = state.tmpmean[j];
        }
        if (state.algotype != 0)
        {

            //
            // Algorithm is set to one of the "advanced" versions with search
            // radius which can handle non-mean prior term
            //
            if (state.priortermtype == 0)
            {

                //
                // User-specified prior
                //
                for (j = 0; j <= ny - 1; j++)
                {
                    model.globalprior[j] = state.priortermval[j];
                }
            }
            if (state.priortermtype == 3)
            {

                //
                // Zero prior
                //
                for (j = 0; j <= ny - 1; j++)
                {
                    model.globalprior[j] = 0;
                }
            }
        }

        //
        // Textbook Shepard
        //
        if (state.algotype == 0)
        {

            //
            // Initialize model
            //
            model.algotype = 0;
            model.nx = nx;
            model.ny = ny;
            model.nlayers = 1;
            model.r0 = 1;
            model.rdecay = 0.5;
            model.lambda0 = 0;
            model.lambdalast = 0;
            model.lambdadecay = 1;
            model.shepardp = state.shepardp;

            //
            // Copy dataset
            //
            apserv.rvectorsetlengthatleast(ref model.shepardxy, npoints * (nx + ny), _params);
            for (i = 0; i <= npoints - 1; i++)
            {
                for (j = 0; j <= nx - 1; j++)
                {
                    model.shepardxy[i * (nx + ny) + j] = state.xy[i * (nx + ny) + j];
                }
                for (j = 0; j <= ny - 1; j++)
                {
                    model.shepardxy[i * (nx + ny) + nx + j] = state.xy[i * (nx + ny) + nx + j] - model.globalprior[j];
                }
            }
            model.npoints = npoints;

            //
            // Prepare internal buffer
            // Evaluate report fields
            //
            idwcreatecalcbuffer(model, model.buffer, _params);
            errormetricsviacalc(state, model, rep, _params);
            return;
        }

        //
        // Textbook modified Shepard's method
        //
        if (state.algotype == 1)
        {

            //
            // Initialize model
            //
            model.algotype = 1;
            model.nx = nx;
            model.ny = ny;
            model.nlayers = 1;
            model.r0 = state.r0;
            model.rdecay = 1;
            model.lambda0 = 0;
            model.lambdalast = 0;
            model.lambdadecay = 1;
            model.shepardp = 0;

            //
            // Build kd-tree search structure
            //
            apserv.rmatrixsetlengthatleast(ref state.tmpxy, npoints, nx + ny, _params);
            for (i = 0; i <= npoints - 1; i++)
            {
                for (j = 0; j <= nx - 1; j++)
                {
                    state.tmpxy[i, j] = state.xy[i * (nx + ny) + j];
                }
                for (j = 0; j <= ny - 1; j++)
                {
                    state.tmpxy[i, nx + j] = state.xy[i * (nx + ny) + nx + j] - model.globalprior[j];
                }
            }
            nearestneighbor.kdtreebuild(state.tmpxy, npoints, nx, ny, 2, model.tree, _params);

            //
            // Prepare internal buffer
            // Evaluate report fields
            //
            idwcreatecalcbuffer(model, model.buffer, _params);
            errormetricsviacalc(state, model, rep, _params);
            return;
        }

        //
        // MSTAB algorithm
        //
        if (state.algotype == 2)
        {
            ap.assert(state.nlayers >= 1, "IDWFit: integrity check failed");

            //
            // Initialize model
            //
            model.algotype = 2;
            model.nx = nx;
            model.ny = ny;
            model.nlayers = state.nlayers;
            model.r0 = state.r0;
            model.rdecay = 0.5;
            model.lambda0 = state.lambda0;
            model.lambdadecay = 1.0;
            model.lambdalast = meps;
            model.shepardp = 0;

            //
            // Build kd-tree search structure,
            // prepare input residuals for the first layer of the model
            //
            apserv.rmatrixsetlengthatleast(ref state.tmpxy, npoints, nx, _params);
            apserv.rmatrixsetlengthatleast(ref state.tmplayers, npoints, nx + ny * (state.nlayers + 1), _params);
            apserv.ivectorsetlengthatleast(ref state.tmptags, npoints, _params);
            for (i = 0; i <= npoints - 1; i++)
            {
                for (j = 0; j <= nx - 1; j++)
                {
                    v = state.xy[i * (nx + ny) + j];
                    state.tmpxy[i, j] = v;
                    state.tmplayers[i, j] = v;
                }
                state.tmptags[i] = i;
                for (j = 0; j <= ny - 1; j++)
                {
                    state.tmplayers[i, nx + j] = state.xy[i * (nx + ny) + nx + j] - model.globalprior[j];
                }
            }
            nearestneighbor.kdtreebuildtagged(state.tmpxy, state.tmptags, npoints, nx, 0, 2, state.tmptree, _params);

            //
            // Iteratively build layer by layer
            //
            apserv.rvectorsetlengthatleast(ref state.tmpx, nx, _params);
            apserv.rvectorsetlengthatleast(ref state.tmpwy, ny, _params);
            apserv.rvectorsetlengthatleast(ref state.tmpw, ny, _params);
            for (layeridx = 0; layeridx <= state.nlayers - 1; layeridx++)
            {

                //
                // Determine layer metrics
                //
                rcur = model.r0 * Math.Pow(model.rdecay, layeridx);
                lambdacur = model.lambda0 * Math.Pow(model.lambdadecay, layeridx);
                if (layeridx == state.nlayers - 1)
                {
                    lambdacur = model.lambdalast;
                }

                //
                // For each point compute residual from fitting with current layer
                //
                for (i = 0; i <= npoints - 1; i++)
                {
                    for (j = 0; j <= nx - 1; j++)
                    {
                        state.tmpx[j] = state.tmplayers[i, j];
                    }
                    k = nearestneighbor.kdtreequeryrnn(state.tmptree, state.tmpx, rcur, true, _params);
                    nearestneighbor.kdtreequeryresultstags(state.tmptree, ref state.tmptags, _params);
                    nearestneighbor.kdtreequeryresultsdistances(state.tmptree, ref state.tmpdist, _params);
                    for (j = 0; j <= ny - 1; j++)
                    {
                        state.tmpwy[j] = 0;
                        state.tmpw[j] = w0;
                    }
                    for (i0 = 0; i0 <= k - 1; i0++)
                    {
                        vv = state.tmpdist[i0] / rcur;
                        vv = vv * vv;
                        v = (1 - vv) * (1 - vv) / (vv + lambdacur);
                        srcidx = state.tmptags[i0];
                        for (j = 0; j <= ny - 1; j++)
                        {
                            state.tmpwy[j] = state.tmpwy[j] + v * state.tmplayers[srcidx, nx + layeridx * ny + j];
                            state.tmpw[j] = state.tmpw[j] + v;
                        }
                    }
                    for (j = 0; j <= ny - 1; j++)
                    {
                        v = state.tmplayers[i, nx + layeridx * ny + j];
                        state.tmplayers[i, nx + (layeridx + 1) * ny + j] = v - state.tmpwy[j] / state.tmpw[j];
                    }
                }
            }
            nearestneighbor.kdtreebuild(state.tmplayers, npoints, nx, ny * state.nlayers, 2, model.tree, _params);

            //
            // Evaluate report fields
            //
            rep.rmserror = 0;
            rep.avgerror = 0;
            rep.maxerror = 0;
            rss = 0;
            tss = 0;
            for (i = 0; i <= npoints - 1; i++)
            {
                for (j = 0; j <= ny - 1; j++)
                {
                    v = Math.Abs(state.tmplayers[i, nx + state.nlayers * ny + j]);
                    rep.rmserror = rep.rmserror + v * v;
                    rep.avgerror = rep.avgerror + v;
                    rep.maxerror = Math.Max(rep.maxerror, Math.Abs(v));
                    rss = rss + v * v;
                    tss = tss + math.sqr(state.xy[i * (nx + ny) + nx + j] - state.tmpmean[j]);
                }
            }
            rep.rmserror = Math.Sqrt(rep.rmserror / (npoints * ny));
            rep.avgerror = rep.avgerror / (npoints * ny);
            rep.r2 = 1.0 - rss / apserv.coalesce(tss, 1.0, _params);

            //
            // Prepare internal buffer
            //
            idwcreatecalcbuffer(model, model.buffer, _params);
            return;
        }

        //
        // Unknown algorithm
        //
        ap.assert(false, "IDWFit: integrity check failed, unexpected algorithm");
    }


    /*************************************************************************
    Serializer: allocation

      -- ALGLIB --
         Copyright 28.02.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void idwalloc(serializer s,
        idwmodel model,
        xparams _params)
    {
        bool processed = new bool();


        //
        // Header
        //
        s.alloc_entry();

        //
        // Algorithm type and fields which are set for all algorithms
        //
        s.alloc_entry();
        s.alloc_entry();
        s.alloc_entry();
        apserv.allocrealarray(s, model.globalprior, -1, _params);
        s.alloc_entry();
        s.alloc_entry();
        s.alloc_entry();
        s.alloc_entry();
        s.alloc_entry();
        s.alloc_entry();
        s.alloc_entry();

        //
        // Algorithm-specific fields
        //
        processed = false;
        if (model.algotype == 0)
        {
            s.alloc_entry();
            apserv.allocrealarray(s, model.shepardxy, -1, _params);
            processed = true;
        }
        if (model.algotype > 0)
        {
            nearestneighbor.kdtreealloc(s, model.tree, _params);
            processed = true;
        }
        ap.assert(processed, "IDW: integrity check failed during serialization");
    }


    /*************************************************************************
    Serializer: serialization

      -- ALGLIB --
         Copyright 28.02.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void idwserialize(serializer s,
        idwmodel model,
        xparams _params)
    {
        bool processed = new bool();


        //
        // Header
        //
        s.serialize_int(scodes.getidwserializationcode(_params));

        //
        // Algorithm type and fields which are set for all algorithms
        //
        s.serialize_int(model.algotype);
        s.serialize_int(model.nx);
        s.serialize_int(model.ny);
        apserv.serializerealarray(s, model.globalprior, -1, _params);
        s.serialize_int(model.nlayers);
        s.serialize_double(model.r0);
        s.serialize_double(model.rdecay);
        s.serialize_double(model.lambda0);
        s.serialize_double(model.lambdalast);
        s.serialize_double(model.lambdadecay);
        s.serialize_double(model.shepardp);

        //
        // Algorithm-specific fields
        //
        processed = false;
        if (model.algotype == 0)
        {
            s.serialize_int(model.npoints);
            apserv.serializerealarray(s, model.shepardxy, -1, _params);
            processed = true;
        }
        if (model.algotype > 0)
        {
            nearestneighbor.kdtreeserialize(s, model.tree, _params);
            processed = true;
        }
        ap.assert(processed, "IDW: integrity check failed during serialization");
    }


    /*************************************************************************
    Serializer: unserialization

      -- ALGLIB --
         Copyright 28.02.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void idwunserialize(serializer s,
        idwmodel model,
        xparams _params)
    {
        bool processed = new bool();
        int scode = 0;


        //
        // Header
        //
        scode = s.unserialize_int();
        ap.assert(scode == scodes.getidwserializationcode(_params), "IDWUnserialize: stream header corrupted");

        //
        // Algorithm type and fields which are set for all algorithms
        //
        model.algotype = s.unserialize_int();
        model.nx = s.unserialize_int();
        model.ny = s.unserialize_int();
        apserv.unserializerealarray(s, ref model.globalprior, _params);
        model.nlayers = s.unserialize_int();
        model.r0 = s.unserialize_double();
        model.rdecay = s.unserialize_double();
        model.lambda0 = s.unserialize_double();
        model.lambdalast = s.unserialize_double();
        model.lambdadecay = s.unserialize_double();
        model.shepardp = s.unserialize_double();

        //
        // Algorithm-specific fields
        //
        processed = false;
        if (model.algotype == 0)
        {
            model.npoints = s.unserialize_int();
            apserv.unserializerealarray(s, ref model.shepardxy, _params);
            processed = true;
        }
        if (model.algotype > 0)
        {
            nearestneighbor.kdtreeunserialize(s, model.tree, _params);
            processed = true;
        }
        ap.assert(processed, "IDW: integrity check failed during serialization");

        //
        // Temporary buffers
        //
        idwcreatecalcbuffer(model, model.buffer, _params);
    }


    /*************************************************************************
    This function evaluates error metrics for the model  using  IDWTsCalcBuf()
    to calculate model at each point.

    NOTE: modern IDW algorithms (MSTAB, MSMOOTH) can generate residuals during
          model construction, so they do not need this function  in  order  to
          evaluate error metrics.

    Following fields of Rep are filled:
    * rep.rmserror
    * rep.avgerror
    * rep.maxerror
    * rep.r2
       
      -- ALGLIB --
         Copyright 22.10.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void errormetricsviacalc(idwbuilder state,
        idwmodel model,
        idwreport rep,
        xparams _params)
    {
        int npoints = 0;
        int nx = 0;
        int ny = 0;
        int i = 0;
        int j = 0;
        double v = 0;
        double vv = 0;
        double rss = 0;
        double tss = 0;

        npoints = state.npoints;
        nx = state.nx;
        ny = state.ny;
        if (npoints == 0)
        {
            rep.rmserror = 0;
            rep.avgerror = 0;
            rep.maxerror = 0;
            rep.r2 = 1;
            return;
        }
        rep.rmserror = 0;
        rep.avgerror = 0;
        rep.maxerror = 0;
        rss = 0;
        tss = 0;
        for (i = 0; i <= npoints - 1; i++)
        {
            for (j = 0; j <= nx - 1; j++)
            {
                model.buffer.x[j] = state.xy[i * (nx + ny) + j];
            }
            idwtscalcbuf(model, model.buffer, model.buffer.x, ref model.buffer.y, _params);
            for (j = 0; j <= ny - 1; j++)
            {
                vv = state.xy[i * (nx + ny) + nx + j];
                v = Math.Abs(vv - model.buffer.y[j]);
                rep.rmserror = rep.rmserror + v * v;
                rep.avgerror = rep.avgerror + v;
                rep.maxerror = Math.Max(rep.maxerror, v);
                rss = rss + v * v;
                tss = tss + math.sqr(vv - state.tmpmean[j]);
            }
        }
        rep.rmserror = Math.Sqrt(rep.rmserror / (npoints * ny));
        rep.avgerror = rep.avgerror / (npoints * ny);
        rep.r2 = 1.0 - rss / apserv.coalesce(tss, 1.0, _params);
    }


}
