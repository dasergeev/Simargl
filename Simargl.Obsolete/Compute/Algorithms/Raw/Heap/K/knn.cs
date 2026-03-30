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

public class knn
{
    /*************************************************************************
    Buffer object which is used to perform  various  requests  (usually  model
    inference) in the multithreaded mode (multiple threads working  with  same
    KNN object).

    This object should be created with KNNCreateBuffer().
    *************************************************************************/
    public class knnbuffer : apobject
    {
        public nearestneighbor.kdtreerequestbuffer treebuf;
        public double[] x;
        public double[] y;
        public int[] tags;
        public double[,] xy;
        public knnbuffer()
        {
            init();
        }
        public override void init()
        {
            treebuf = new nearestneighbor.kdtreerequestbuffer();
            x = new double[0];
            y = new double[0];
            tags = new int[0];
            xy = new double[0, 0];
        }
        public override apobject make_copy()
        {
            knnbuffer _result = new knnbuffer();
            _result.treebuf = (nearestneighbor.kdtreerequestbuffer)treebuf.make_copy();
            _result.x = (double[])x.Clone();
            _result.y = (double[])y.Clone();
            _result.tags = (int[])tags.Clone();
            _result.xy = (double[,])xy.Clone();
            return _result;
        }
    };


    /*************************************************************************
    A KNN builder object; this object encapsulates  dataset  and  all  related
    settings, it is used to create an actual instance of KNN model.
    *************************************************************************/
    public class knnbuilder : apobject
    {
        public int dstype;
        public int npoints;
        public int nvars;
        public bool iscls;
        public int nout;
        public double[,] dsdata;
        public double[] dsrval;
        public int[] dsival;
        public int knnnrm;
        public knnbuilder()
        {
            init();
        }
        public override void init()
        {
            dsdata = new double[0, 0];
            dsrval = new double[0];
            dsival = new int[0];
        }
        public override apobject make_copy()
        {
            knnbuilder _result = new knnbuilder();
            _result.dstype = dstype;
            _result.npoints = npoints;
            _result.nvars = nvars;
            _result.iscls = iscls;
            _result.nout = nout;
            _result.dsdata = (double[,])dsdata.Clone();
            _result.dsrval = (double[])dsrval.Clone();
            _result.dsival = (int[])dsival.Clone();
            _result.knnnrm = knnnrm;
            return _result;
        }
    };


    /*************************************************************************
    KNN model, can be used for classification or regression
    *************************************************************************/
    public class knnmodel : apobject
    {
        public int nvars;
        public int nout;
        public int k;
        public double eps;
        public bool iscls;
        public bool isdummy;
        public nearestneighbor.kdtree tree;
        public knnbuffer buffer;
        public knnmodel()
        {
            init();
        }
        public override void init()
        {
            tree = new nearestneighbor.kdtree();
            buffer = new knnbuffer();
        }
        public override apobject make_copy()
        {
            knnmodel _result = new knnmodel();
            _result.nvars = nvars;
            _result.nout = nout;
            _result.k = k;
            _result.eps = eps;
            _result.iscls = iscls;
            _result.isdummy = isdummy;
            _result.tree = (nearestneighbor.kdtree)tree.make_copy();
            _result.buffer = (knnbuffer)buffer.make_copy();
            return _result;
        }
    };


    /*************************************************************************
    KNN training report.

    Following fields store training set errors:
    * relclserror       -   fraction of misclassified cases, [0,1]
    * avgce             -   average cross-entropy in bits per symbol
    * rmserror          -   root-mean-square error
    * avgerror          -   average error
    * avgrelerror       -   average relative error

    For classification problems:
    * RMS, AVG and AVGREL errors are calculated for posterior probabilities

    For regression problems:
    * RELCLS and AVGCE errors are zero
    *************************************************************************/
    public class knnreport : apobject
    {
        public double relclserror;
        public double avgce;
        public double rmserror;
        public double avgerror;
        public double avgrelerror;
        public knnreport()
        {
            init();
        }
        public override void init()
        {
        }
        public override apobject make_copy()
        {
            knnreport _result = new knnreport();
            _result.relclserror = relclserror;
            _result.avgce = avgce;
            _result.rmserror = rmserror;
            _result.avgerror = avgerror;
            _result.avgrelerror = avgrelerror;
            return _result;
        }
    };




    public const int knnfirstversion = 0;


    /*************************************************************************
    This function creates buffer  structure  which  can  be  used  to  perform
    parallel KNN requests.

    KNN subpackage provides two sets of computing functions - ones  which  use
    internal buffer of KNN model (these  functions are single-threaded because
    they use same buffer, which can not  shared  between  threads),  and  ones
    which use external buffer.

    This function is used to initialize external buffer.

    INPUT PARAMETERS
        Model       -   KNN model which is associated with newly created buffer

    OUTPUT PARAMETERS
        Buf         -   external buffer.
        
        
    IMPORTANT: buffer object should be used only with model which was used  to
               initialize buffer. Any attempt to  use  buffer  with  different
               object is dangerous - you  may   get  integrity  check  failure
               (exception) because sizes of internal  arrays  do  not  fit  to
               dimensions of the model structure.

      -- ALGLIB --
         Copyright 15.02.2019 by Bochkanov Sergey
    *************************************************************************/
    public static void knncreatebuffer(knnmodel model,
        knnbuffer buf,
        xparams _params)
    {
        if (!model.isdummy)
        {
            nearestneighbor.kdtreecreaterequestbuffer(model.tree, buf.treebuf, _params);
        }
        buf.x = new double[model.nvars];
        buf.y = new double[model.nout];
    }


    /*************************************************************************
    This subroutine creates KNNBuilder object which is used to train KNN models.

    By default, new builder stores empty dataset and some  reasonable  default
    settings. At the very least, you should specify dataset prior to  building
    KNN model. You can also tweak settings of the model construction algorithm
    (recommended, although default settings should work well).

    Following actions are mandatory:
    * calling knnbuildersetdataset() to specify dataset
    * calling knnbuilderbuildknnmodel() to build KNN model using current
      dataset and default settings
      
    Additionally, you may call:
    * knnbuildersetnorm() to change norm being used

    INPUT PARAMETERS:
        none

    OUTPUT PARAMETERS:
        S           -   KNN builder

      -- ALGLIB --
         Copyright 15.02.2019 by Bochkanov Sergey
    *************************************************************************/
    public static void knnbuildercreate(knnbuilder s,
        xparams _params)
    {

        //
        // Empty dataset
        //
        s.dstype = -1;
        s.npoints = 0;
        s.nvars = 0;
        s.iscls = false;
        s.nout = 1;

        //
        // Default training settings
        //
        s.knnnrm = 2;
    }


    /*************************************************************************
    Specifies regression problem (one or more continuous  output variables are
    predicted). There also exists "classification" version of this function.

    This subroutine adds dense dataset to the internal storage of the  builder
    object. Specifying your dataset in the dense format means that  the  dense
    version of the KNN construction algorithm will be invoked.

    INPUT PARAMETERS:
        S           -   KNN builder object
        XY          -   array[NPoints,NVars+NOut] (note: actual  size  can  be
                        larger, only leading part is used anyway), dataset:
                        * first NVars elements of each row store values of the
                          independent variables
                        * next NOut elements store  values  of  the  dependent
                          variables
        NPoints     -   number of rows in the dataset, NPoints>=1
        NVars       -   number of independent variables, NVars>=1 
        NOut        -   number of dependent variables, NOut>=1

    OUTPUT PARAMETERS:
        S           -   KNN builder

      -- ALGLIB --
         Copyright 15.02.2019 by Bochkanov Sergey
    *************************************************************************/
    public static void knnbuildersetdatasetreg(knnbuilder s,
        double[,] xy,
        int npoints,
        int nvars,
        int nout,
        xparams _params)
    {
        int i = 0;
        int j = 0;


        //
        // Check parameters
        //
        ap.assert(npoints >= 1, "knnbuildersetdatasetreg: npoints<1");
        ap.assert(nvars >= 1, "knnbuildersetdatasetreg: nvars<1");
        ap.assert(nout >= 1, "knnbuildersetdatasetreg: nout<1");
        ap.assert(ap.rows(xy) >= npoints, "knnbuildersetdatasetreg: rows(xy)<npoints");
        ap.assert(ap.cols(xy) >= nvars + nout, "knnbuildersetdatasetreg: cols(xy)<nvars+nout");
        ap.assert(apserv.apservisfinitematrix(xy, npoints, nvars + nout, _params), "knnbuildersetdatasetreg: xy parameter contains INFs or NANs");

        //
        // Set dataset
        //
        s.dstype = 0;
        s.iscls = false;
        s.npoints = npoints;
        s.nvars = nvars;
        s.nout = nout;
        apserv.rmatrixsetlengthatleast(ref s.dsdata, npoints, nvars, _params);
        for (i = 0; i <= npoints - 1; i++)
        {
            for (j = 0; j <= nvars - 1; j++)
            {
                s.dsdata[i, j] = xy[i, j];
            }
        }
        apserv.rvectorsetlengthatleast(ref s.dsrval, npoints * nout, _params);
        for (i = 0; i <= npoints - 1; i++)
        {
            for (j = 0; j <= nout - 1; j++)
            {
                s.dsrval[i * nout + j] = xy[i, nvars + j];
            }
        }
    }


    /*************************************************************************
    Specifies classification problem (two  or  more  classes  are  predicted).
    There also exists "regression" version of this function.

    This subroutine adds dense dataset to the internal storage of the  builder
    object. Specifying your dataset in the dense format means that  the  dense
    version of the KNN construction algorithm will be invoked.

    INPUT PARAMETERS:
        S           -   KNN builder object
        XY          -   array[NPoints,NVars+1] (note:   actual   size  can  be
                        larger, only leading part is used anyway), dataset:
                        * first NVars elements of each row store values of the
                          independent variables
                        * next element stores class index, in [0,NClasses)
        NPoints     -   number of rows in the dataset, NPoints>=1
        NVars       -   number of independent variables, NVars>=1 
        NClasses    -   number of classes, NClasses>=2

    OUTPUT PARAMETERS:
        S           -   KNN builder

      -- ALGLIB --
         Copyright 15.02.2019 by Bochkanov Sergey
    *************************************************************************/
    public static void knnbuildersetdatasetcls(knnbuilder s,
        double[,] xy,
        int npoints,
        int nvars,
        int nclasses,
        xparams _params)
    {
        int i = 0;
        int j = 0;


        //
        // Check parameters
        //
        ap.assert(npoints >= 1, "knnbuildersetdatasetcls: npoints<1");
        ap.assert(nvars >= 1, "knnbuildersetdatasetcls: nvars<1");
        ap.assert(nclasses >= 2, "knnbuildersetdatasetcls: nclasses<2");
        ap.assert(ap.rows(xy) >= npoints, "knnbuildersetdatasetcls: rows(xy)<npoints");
        ap.assert(ap.cols(xy) >= nvars + 1, "knnbuildersetdatasetcls: cols(xy)<nvars+1");
        ap.assert(apserv.apservisfinitematrix(xy, npoints, nvars + 1, _params), "knnbuildersetdatasetcls: xy parameter contains INFs or NANs");
        for (i = 0; i <= npoints - 1; i++)
        {
            j = (int)Math.Round(xy[i, nvars]);
            ap.assert(j >= 0 && j < nclasses, "knnbuildersetdatasetcls: last column of xy contains invalid class number");
        }

        //
        // Set dataset
        //
        s.iscls = true;
        s.dstype = 0;
        s.npoints = npoints;
        s.nvars = nvars;
        s.nout = nclasses;
        apserv.rmatrixsetlengthatleast(ref s.dsdata, npoints, nvars, _params);
        for (i = 0; i <= npoints - 1; i++)
        {
            for (j = 0; j <= nvars - 1; j++)
            {
                s.dsdata[i, j] = xy[i, j];
            }
        }
        apserv.ivectorsetlengthatleast(ref s.dsival, npoints, _params);
        for (i = 0; i <= npoints - 1; i++)
        {
            s.dsival[i] = (int)Math.Round(xy[i, nvars]);
        }
    }


    /*************************************************************************
    This function sets norm type used for neighbor search.

    INPUT PARAMETERS:
        S           -   decision forest builder object
        NormType    -   norm type:
                        * 0      inf-norm
                        * 1      1-norm
                        * 2      Euclidean norm (default)

    OUTPUT PARAMETERS:
        S           -   decision forest builder

      -- ALGLIB --
         Copyright 15.02.2019 by Bochkanov Sergey
    *************************************************************************/
    public static void knnbuildersetnorm(knnbuilder s,
        int nrmtype,
        xparams _params)
    {
        ap.assert((nrmtype == 0 || nrmtype == 1) || nrmtype == 2, "knnbuildersetnorm: unexpected norm type");
        s.knnnrm = nrmtype;
    }


    /*************************************************************************
    This subroutine builds KNN model  according  to  current  settings,  using
    dataset internally stored in the builder object.

    The model being built performs inference using Eps-approximate  K  nearest
    neighbors search algorithm, with:
    * K=1,  Eps=0 corresponding to the "nearest neighbor algorithm"
    * K>1,  Eps=0 corresponding to the "K nearest neighbors algorithm"
    * K>=1, Eps>0 corresponding to "approximate nearest neighbors algorithm"

    An approximate KNN is a good option for high-dimensional  datasets  (exact
    KNN works slowly when dimensions count grows).

    An ALGLIB implementation of kd-trees is used to perform k-nn searches.

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
        S       -   KNN builder object
        K       -   number of neighbors to search for, K>=1
        Eps     -   approximation factor:
                    * Eps=0 means that exact kNN search is performed
                    * Eps>0 means that (1+Eps)-approximate search is performed

    OUTPUT PARAMETERS:
        Model       -   KNN model
        Rep         -   report

      -- ALGLIB --
         Copyright 15.02.2019 by Bochkanov Sergey
    *************************************************************************/
    public static void knnbuilderbuildknnmodel(knnbuilder s,
        int k,
        double eps,
        knnmodel model,
        knnreport rep,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int nvars = 0;
        int nout = 0;
        int npoints = 0;
        bool iscls = new bool();
        double[,] xy = new double[0, 0];
        int[] tags = new int[0];

        npoints = s.npoints;
        nvars = s.nvars;
        nout = s.nout;
        iscls = s.iscls;

        //
        // Check settings
        //
        ap.assert(k >= 1, "knnbuilderbuildknnmodel: k<1");
        ap.assert(math.isfinite(eps) && (double)(eps) >= (double)(0), "knnbuilderbuildknnmodel: eps<0");

        //
        // Prepare output
        //
        clearreport(rep, _params);
        model.nvars = nvars;
        model.nout = nout;
        model.iscls = iscls;
        model.k = k;
        model.eps = eps;
        model.isdummy = false;

        //
        // Quick exit for empty dataset
        //
        if (s.dstype == -1)
        {
            model.isdummy = true;
            return;
        }

        //
        // Build kd-tree
        //
        if (iscls)
        {
            xy = new double[npoints, nvars + 1];
            tags = new int[npoints];
            for (i = 0; i <= npoints - 1; i++)
            {
                for (j = 0; j <= nvars - 1; j++)
                {
                    xy[i, j] = s.dsdata[i, j];
                }
                xy[i, nvars] = s.dsival[i];
                tags[i] = s.dsival[i];
            }
            nearestneighbor.kdtreebuildtagged(xy, tags, npoints, nvars, 0, s.knnnrm, model.tree, _params);
        }
        else
        {
            xy = new double[npoints, nvars + nout];
            for (i = 0; i <= npoints - 1; i++)
            {
                for (j = 0; j <= nvars - 1; j++)
                {
                    xy[i, j] = s.dsdata[i, j];
                }
                for (j = 0; j <= nout - 1; j++)
                {
                    xy[i, nvars + j] = s.dsrval[i * nout + j];
                }
            }
            nearestneighbor.kdtreebuild(xy, npoints, nvars, nout, s.knnnrm, model.tree, _params);
        }

        //
        // Build buffer
        //
        knncreatebuffer(model, model.buffer, _params);

        //
        // Report
        //
        knnallerrors(model, xy, npoints, rep, _params);
    }


    /*************************************************************************
    Changing search settings of KNN model.

    K and EPS parameters of KNN  (AKNN)  search  are  specified  during  model
    construction. However, plain KNN algorithm with Euclidean distance  allows
    you to change them at any moment.

    NOTE: future versions of KNN model may support advanced versions  of  KNN,
          such as NCA or LMNN. It is possible that such algorithms won't allow
          you to change search settings on the fly. If you call this  function
          for an algorithm which does not support on-the-fly changes, it  will
          throw an exception.

    INPUT PARAMETERS:
        Model   -   KNN model
        K       -   K>=1, neighbors count
        EPS     -   accuracy of the EPS-approximate NN search. Set to 0.0,  if
                    you want to perform "classic" KNN search.  Specify  larger
                    values  if  you  need  to  speed-up  high-dimensional  KNN
                    queries.

    OUTPUT PARAMETERS:
        nothing on success, exception on failure

      -- ALGLIB --
         Copyright 15.02.2019 by Bochkanov Sergey
    *************************************************************************/
    public static void knnrewritekeps(knnmodel model,
        int k,
        double eps,
        xparams _params)
    {
        ap.assert(k >= 1, "knnrewritekeps: k<1");
        ap.assert(math.isfinite(eps) && (double)(eps) >= (double)(0), "knnrewritekeps: eps<0");
        model.k = k;
        model.eps = eps;
    }


    /*************************************************************************
    Inference using KNN model.

    See also knnprocess0(), knnprocessi() and knnclassify() for options with a
    bit more convenient interface.

    IMPORTANT: this function is thread-unsafe and modifies internal structures
               of the model! You can not use same model  object  for  parallel
               evaluation from several threads.
               
               Use knntsprocess() with independent  thread-local  buffers,  if
               you need thread-safe evaluation.

    INPUT PARAMETERS:
        Model   -   KNN model
        X       -   input vector,  array[0..NVars-1].
        Y       -   possible preallocated buffer. Reused if long enough.

    OUTPUT PARAMETERS:
        Y       -   result. Regression estimate when solving regression  task,
                    vector of posterior probabilities for classification task.

      -- ALGLIB --
         Copyright 15.02.2019 by Bochkanov Sergey
    *************************************************************************/
    public static void knnprocess(knnmodel model,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        knntsprocess(model, model.buffer, x, ref y, _params);
    }


    /*************************************************************************
    This function returns first component of the  inferred  vector  (i.e.  one
    with index #0).

    It is a convenience wrapper for knnprocess() intended for either:
    * 1-dimensional regression problems
    * 2-class classification problems

    In the former case this function returns inference result as scalar, which
    is definitely more convenient that wrapping it as vector.  In  the  latter
    case it returns probability of object belonging to class #0.

    If you call it for anything different from two cases above, it  will  work
    as defined, i.e. return y[0], although it is of less use in such cases.

    IMPORTANT: this function is thread-unsafe and modifies internal structures
               of the model! You can not use same model  object  for  parallel
               evaluation from several threads.
               
               Use knntsprocess() with independent  thread-local  buffers,  if
               you need thread-safe evaluation.

    INPUT PARAMETERS:
        Model   -   KNN model
        X       -   input vector,  array[0..NVars-1].

    RESULT:
        Y[0]

      -- ALGLIB --
         Copyright 15.02.2019 by Bochkanov Sergey
    *************************************************************************/
    public static double knnprocess0(knnmodel model,
        double[] x,
        xparams _params)
    {
        double result = 0;
        int i = 0;
        int nvars = 0;

        nvars = model.nvars;
        for (i = 0; i <= nvars - 1; i++)
        {
            model.buffer.x[i] = x[i];
        }
        processinternal(model, model.buffer, _params);
        result = model.buffer.y[0];
        return result;
    }


    /*************************************************************************
    This function returns most probable class number for an  input  X.  It  is
    same as calling knnprocess(model,x,y), then determining i=argmax(y[i]) and
    returning i.

    A class number in [0,NOut) range in returned for classification  problems,
    -1 is returned when this function is called for regression problems.

    IMPORTANT: this function is thread-unsafe and modifies internal structures
               of the model! You can not use same model  object  for  parallel
               evaluation from several threads.
               
               Use knntsprocess() with independent  thread-local  buffers,  if
               you need thread-safe evaluation.

    INPUT PARAMETERS:
        Model   -   KNN model
        X       -   input vector,  array[0..NVars-1].

    RESULT:
        class number, -1 for regression tasks

      -- ALGLIB --
         Copyright 15.02.2019 by Bochkanov Sergey
    *************************************************************************/
    public static int knnclassify(knnmodel model,
        double[] x,
        xparams _params)
    {
        int result = 0;
        int i = 0;
        int nvars = 0;
        int nout = 0;

        if (!model.iscls)
        {
            result = -1;
            return result;
        }
        nvars = model.nvars;
        nout = model.nout;
        for (i = 0; i <= nvars - 1; i++)
        {
            model.buffer.x[i] = x[i];
        }
        processinternal(model, model.buffer, _params);
        result = 0;
        for (i = 1; i <= nout - 1; i++)
        {
            if (model.buffer.y[i] > model.buffer.y[result])
            {
                result = i;
            }
        }
        return result;
    }


    /*************************************************************************
    'interactive' variant of knnprocess()  for  languages  like  Python  which
    support constructs like "y = knnprocessi(model,x)" and interactive mode of
    the interpreter.

    This function allocates new array on each call,  so  it  is  significantly
    slower than its 'non-interactive' counterpart, but it is  more  convenient
    when you call it from command line.

    IMPORTANT: this  function  is  thread-unsafe  and  may   modify   internal
               structures of the model! You can not use same model  object for
               parallel evaluation from several threads.
               
               Use knntsprocess()  with  independent  thread-local  buffers if
               you need thread-safe evaluation.

      -- ALGLIB --
         Copyright 15.02.2019 by Bochkanov Sergey
    *************************************************************************/
    public static void knnprocessi(knnmodel model,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        y = new double[0];

        knnprocess(model, x, ref y, _params);
    }


    /*************************************************************************
    Thread-safe procesing using external buffer for temporaries.

    This function is thread-safe (i.e .  you  can  use  same  KNN  model  from
    multiple threads) as long as you use different buffer objects for different
    threads.

    INPUT PARAMETERS:
        Model   -   KNN model
        Buf     -   buffer object, must be  allocated  specifically  for  this
                    model with knncreatebuffer().
        X       -   input vector,  array[NVars]

    OUTPUT PARAMETERS:
        Y       -   result, array[NOut].   Regression  estimate  when  solving
                    regression task,  vector  of  posterior  probabilities for
                    a classification task.

      -- ALGLIB --
         Copyright 15.02.2019 by Bochkanov Sergey
    *************************************************************************/
    public static void knntsprocess(knnmodel model,
        knnbuffer buf,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        int i = 0;
        int nvars = 0;
        int nout = 0;

        nvars = model.nvars;
        nout = model.nout;
        for (i = 0; i <= nvars - 1; i++)
        {
            buf.x[i] = x[i];
        }
        processinternal(model, buf, _params);
        if (ap.len(y) < nout)
        {
            y = new double[nout];
        }
        for (i = 0; i <= nout - 1; i++)
        {
            y[i] = buf.y[i];
        }
    }


    /*************************************************************************
    Relative classification error on the test set

    INPUT PARAMETERS:
        Model   -   KNN model
        XY      -   test set
        NPoints -   test set size

    RESULT:
        percent of incorrectly classified cases.
        Zero if model solves regression task.
        
    NOTE: if  you  need several different kinds of error metrics, it is better
          to use knnallerrors() which computes all error metric  with just one
          pass over dataset. 

      -- ALGLIB --
         Copyright 15.02.2019 by Bochkanov Sergey
    *************************************************************************/
    public static double knnrelclserror(knnmodel model,
        double[,] xy,
        int npoints,
        xparams _params)
    {
        double result = 0;
        knnreport rep = new knnreport();

        knnallerrors(model, xy, npoints, rep, _params);
        result = rep.relclserror;
        return result;
    }


    /*************************************************************************
    Average cross-entropy (in bits per element) on the test set

    INPUT PARAMETERS:
        Model   -   KNN model
        XY      -   test set
        NPoints -   test set size

    RESULT:
        CrossEntropy/NPoints.
        Zero if model solves regression task.

    NOTE: the cross-entropy metric is too unstable when used to  evaluate  KNN
          models (such models can report exactly  zero probabilities),  so  we
          do not recommend using it.
        
    NOTE: if  you  need several different kinds of error metrics, it is better
          to use knnallerrors() which computes all error metric  with just one
          pass over dataset. 

      -- ALGLIB --
         Copyright 15.02.2019 by Bochkanov Sergey
    *************************************************************************/
    public static double knnavgce(knnmodel model,
        double[,] xy,
        int npoints,
        xparams _params)
    {
        double result = 0;
        knnreport rep = new knnreport();

        knnallerrors(model, xy, npoints, rep, _params);
        result = rep.avgce;
        return result;
    }


    /*************************************************************************
    RMS error on the test set.

    Its meaning for regression task is obvious. As for classification problems,
    RMS error means error when estimating posterior probabilities.

    INPUT PARAMETERS:
        Model   -   KNN model
        XY      -   test set
        NPoints -   test set size

    RESULT:
        root mean square error.
        
    NOTE: if  you  need several different kinds of error metrics, it is better
          to use knnallerrors() which computes all error metric  with just one
          pass over dataset.

      -- ALGLIB --
         Copyright 15.02.2019 by Bochkanov Sergey
    *************************************************************************/
    public static double knnrmserror(knnmodel model,
        double[,] xy,
        int npoints,
        xparams _params)
    {
        double result = 0;
        knnreport rep = new knnreport();

        knnallerrors(model, xy, npoints, rep, _params);
        result = rep.rmserror;
        return result;
    }


    /*************************************************************************
    Average error on the test set

    Its meaning for regression task is obvious. As for classification problems,
    average error means error when estimating posterior probabilities.

    INPUT PARAMETERS:
        Model   -   KNN model
        XY      -   test set
        NPoints -   test set size

    RESULT:
        average error
        
    NOTE: if  you  need several different kinds of error metrics, it is better
          to use knnallerrors() which computes all error metric  with just one
          pass over dataset.

      -- ALGLIB --
         Copyright 15.02.2019 by Bochkanov Sergey
    *************************************************************************/
    public static double knnavgerror(knnmodel model,
        double[,] xy,
        int npoints,
        xparams _params)
    {
        double result = 0;
        knnreport rep = new knnreport();

        knnallerrors(model, xy, npoints, rep, _params);
        result = rep.avgerror;
        return result;
    }


    /*************************************************************************
    Average relative error on the test set

    Its meaning for regression task is obvious. As for classification problems,
    average relative error means error when estimating posterior probabilities.

    INPUT PARAMETERS:
        Model   -   KNN model
        XY      -   test set
        NPoints -   test set size

    RESULT:
        average relative error
        
    NOTE: if  you  need several different kinds of error metrics, it is better
          to use knnallerrors() which computes all error metric  with just one
          pass over dataset.

      -- ALGLIB --
         Copyright 15.02.2019 by Bochkanov Sergey
    *************************************************************************/
    public static double knnavgrelerror(knnmodel model,
        double[,] xy,
        int npoints,
        xparams _params)
    {
        double result = 0;
        knnreport rep = new knnreport();

        knnallerrors(model, xy, npoints, rep, _params);
        result = rep.avgrelerror;
        return result;
    }


    /*************************************************************************
    Calculates all kinds of errors for the model in one call.

    INPUT PARAMETERS:
        Model   -   KNN model
        XY      -   test set:
                    * one row per point
                    * first NVars columns store independent variables
                    * depending on problem type:
                      * next column stores class number in [0,NClasses) -  for
                        classification problems
                      * next NOut columns  store  dependent  variables  -  for
                        regression problems
        NPoints -   test set size, NPoints>=0

    OUTPUT PARAMETERS:
        Rep     -   following fields are loaded with errors for both regression
                    and classification models:
                    * rep.rmserror - RMS error for the output
                    * rep.avgerror - average error
                    * rep.avgrelerror - average relative error
                    following fields are set only  for classification  models,
                    zero for regression ones:
                    * relclserror   - relative classification error, in [0,1]
                    * avgce - average cross-entropy in bits per dataset entry

    NOTE: the cross-entropy metric is too unstable when used to  evaluate  KNN
          models (such models can report exactly  zero probabilities),  so  we
          do not recommend using it.

      -- ALGLIB --
         Copyright 15.02.2019 by Bochkanov Sergey
    *************************************************************************/
    public static void knnallerrors(knnmodel model,
        double[,] xy,
        int npoints,
        knnreport rep,
        xparams _params)
    {
        knnbuffer buf = new knnbuffer();
        double[] desiredy = new double[0];
        double[] errbuf = new double[0];
        int nvars = 0;
        int nout = 0;
        int ny = 0;
        bool iscls = new bool();
        int i = 0;
        int j = 0;

        nvars = model.nvars;
        nout = model.nout;
        iscls = model.iscls;
        if (iscls)
        {
            ny = 1;
        }
        else
        {
            ny = nout;
        }

        //
        // Check input
        //
        ap.assert(npoints >= 0, "knnallerrors: npoints<0");
        ap.assert(ap.rows(xy) >= npoints, "knnallerrors: rows(xy)<npoints");
        ap.assert(ap.cols(xy) >= nvars + ny, "knnallerrors: cols(xy)<nvars+nout");
        ap.assert(apserv.apservisfinitematrix(xy, npoints, nvars + ny, _params), "knnallerrors: xy parameter contains INFs or NANs");

        //
        // Clean up report
        //
        clearreport(rep, _params);

        //
        // Quick exit if needed
        //
        if (model.isdummy || npoints == 0)
        {
            return;
        }

        //
        // Process using local buffer
        //
        knncreatebuffer(model, buf, _params);
        if (iscls)
        {
            bdss.dserrallocate(nout, ref errbuf, _params);
        }
        else
        {
            bdss.dserrallocate(-nout, ref errbuf, _params);
        }
        desiredy = new double[ny];
        for (i = 0; i <= npoints - 1; i++)
        {
            for (j = 0; j <= nvars - 1; j++)
            {
                buf.x[j] = xy[i, j];
            }
            if (iscls)
            {
                j = (int)Math.Round(xy[i, nvars]);
                ap.assert(j >= 0 && j < nout, "knnallerrors: one of the class labels is not in [0,NClasses)");
                desiredy[0] = j;
            }
            else
            {
                for (j = 0; j <= nout - 1; j++)
                {
                    desiredy[j] = xy[i, nvars + j];
                }
            }
            processinternal(model, buf, _params);
            bdss.dserraccumulate(ref errbuf, buf.y, desiredy, _params);
        }
        bdss.dserrfinish(ref errbuf, _params);

        //
        // Extract results
        //
        if (iscls)
        {
            rep.relclserror = errbuf[0];
            rep.avgce = errbuf[1];
        }
        rep.rmserror = errbuf[2];
        rep.avgerror = errbuf[3];
        rep.avgrelerror = errbuf[4];
    }


    /*************************************************************************
    Serializer: allocation

      -- ALGLIB --
         Copyright 15.02.2019 by Bochkanov Sergey
    *************************************************************************/
    public static void knnalloc(serializer s,
        knnmodel model,
        xparams _params)
    {
        s.alloc_entry();
        s.alloc_entry();
        s.alloc_entry();
        s.alloc_entry();
        s.alloc_entry();
        s.alloc_entry();
        s.alloc_entry();
        s.alloc_entry();
        if (!model.isdummy)
        {
            nearestneighbor.kdtreealloc(s, model.tree, _params);
        }
    }


    /*************************************************************************
    Serializer: serialization

      -- ALGLIB --
         Copyright 15.02.2019 by Bochkanov Sergey
    *************************************************************************/
    public static void knnserialize(serializer s,
        knnmodel model,
        xparams _params)
    {
        s.serialize_int(scodes.getknnserializationcode(_params));
        s.serialize_int(knnfirstversion);
        s.serialize_int(model.nvars);
        s.serialize_int(model.nout);
        s.serialize_int(model.k);
        s.serialize_double(model.eps);
        s.serialize_bool(model.iscls);
        s.serialize_bool(model.isdummy);
        if (!model.isdummy)
        {
            nearestneighbor.kdtreeserialize(s, model.tree, _params);
        }
    }


    /*************************************************************************
    Serializer: unserialization

      -- ALGLIB --
         Copyright 15.02.2019 by Bochkanov Sergey
    *************************************************************************/
    public static void knnunserialize(serializer s,
        knnmodel model,
        xparams _params)
    {
        int i0 = 0;
        int i1 = 0;


        //
        // check correctness of header
        //
        i0 = s.unserialize_int();
        ap.assert(i0 == scodes.getknnserializationcode(_params), "KNNUnserialize: stream header corrupted");
        i1 = s.unserialize_int();
        ap.assert(i1 == knnfirstversion, "KNNUnserialize: stream header corrupted");

        //
        // Unserialize data
        //
        model.nvars = s.unserialize_int();
        model.nout = s.unserialize_int();
        model.k = s.unserialize_int();
        model.eps = s.unserialize_double();
        model.iscls = s.unserialize_bool();
        model.isdummy = s.unserialize_bool();
        if (!model.isdummy)
        {
            nearestneighbor.kdtreeunserialize(s, model.tree, _params);
        }

        //
        // Prepare local buffer
        //
        knncreatebuffer(model, model.buffer, _params);
    }


    /*************************************************************************
    Sets report fields to their default values

      -- ALGLIB --
         Copyright 15.02.2019 by Bochkanov Sergey
    *************************************************************************/
    private static void clearreport(knnreport rep,
        xparams _params)
    {
        rep.relclserror = 0;
        rep.avgce = 0;
        rep.rmserror = 0;
        rep.avgerror = 0;
        rep.avgrelerror = 0;
    }


    /*************************************************************************
    This function processes buf.X and stores result to buf.Y

    INPUT PARAMETERS
        Model       -   KNN model
        Buf         -   processing buffer.
        
        
    IMPORTANT: buffer object should be used only with model which was used  to
               initialize buffer. Any attempt to  use  buffer  with  different
               object is dangerous - you  may   get  integrity  check  failure
               (exception) because sizes of internal  arrays  do  not  fit  to
               dimensions of the model structure.

      -- ALGLIB --
         Copyright 15.02.2019 by Bochkanov Sergey
    *************************************************************************/
    private static void processinternal(knnmodel model,
        knnbuffer buf,
        xparams _params)
    {
        int nvars = 0;
        int nout = 0;
        bool iscls = new bool();
        int nncnt = 0;
        int i = 0;
        int j = 0;
        double v = 0;

        nvars = model.nvars;
        nout = model.nout;
        iscls = model.iscls;

        //
        // Quick exit if needed
        //
        if (model.isdummy)
        {
            for (i = 0; i <= nout - 1; i++)
            {
                buf.y[i] = 0;
            }
            return;
        }

        //
        // Perform request, average results
        //
        for (i = 0; i <= nout - 1; i++)
        {
            buf.y[i] = 0;
        }
        nncnt = nearestneighbor.kdtreetsqueryaknn(model.tree, buf.treebuf, buf.x, model.k, true, model.eps, _params);
        v = 1 / apserv.coalesce(nncnt, 1, _params);
        if (iscls)
        {
            nearestneighbor.kdtreetsqueryresultstags(model.tree, buf.treebuf, ref buf.tags, _params);
            for (i = 0; i <= nncnt - 1; i++)
            {
                j = buf.tags[i];
                buf.y[j] = buf.y[j] + v;
            }
        }
        else
        {
            nearestneighbor.kdtreetsqueryresultsxy(model.tree, buf.treebuf, ref buf.xy, _params);
            for (i = 0; i <= nncnt - 1; i++)
            {
                for (j = 0; j <= nout - 1; j++)
                {
                    buf.y[j] = buf.y[j] + v * buf.xy[i, nvars + j];
                }
            }
        }
    }


}
