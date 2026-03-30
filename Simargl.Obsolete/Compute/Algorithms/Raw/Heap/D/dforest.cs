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

public class dforest
{
    /*************************************************************************
    A random forest (decision forest) builder object.

    Used to store dataset and specify decision forest training algorithm settings.
    *************************************************************************/
    public class decisionforestbuilder : apobject
    {
        public int dstype;
        public int npoints;
        public int nvars;
        public int nclasses;
        public double[] dsdata;
        public double[] dsrval;
        public int[] dsival;
        public int rdfalgo;
        public double rdfratio;
        public double rdfvars;
        public int rdfglobalseed;
        public int rdfsplitstrength;
        public int rdfimportance;
        public double[] dsmin;
        public double[] dsmax;
        public bool[] dsbinary;
        public double dsravg;
        public int[] dsctotals;
        public int rdfprogress;
        public int rdftotal;
        public smp.shared_pool workpool;
        public smp.shared_pool votepool;
        public smp.shared_pool treepool;
        public smp.shared_pool treefactory;
        public bool neediobmatrix;
        public bool[,] iobmatrix;
        public int[] varimpshuffle2;
        public decisionforestbuilder()
        {
            init();
        }
        public override void init()
        {
            dsdata = new double[0];
            dsrval = new double[0];
            dsival = new int[0];
            dsmin = new double[0];
            dsmax = new double[0];
            dsbinary = new bool[0];
            dsctotals = new int[0];
            workpool = new smp.shared_pool();
            votepool = new smp.shared_pool();
            treepool = new smp.shared_pool();
            treefactory = new smp.shared_pool();
            iobmatrix = new bool[0, 0];
            varimpshuffle2 = new int[0];
        }
        public override apobject make_copy()
        {
            decisionforestbuilder _result = new decisionforestbuilder();
            _result.dstype = dstype;
            _result.npoints = npoints;
            _result.nvars = nvars;
            _result.nclasses = nclasses;
            _result.dsdata = (double[])dsdata.Clone();
            _result.dsrval = (double[])dsrval.Clone();
            _result.dsival = (int[])dsival.Clone();
            _result.rdfalgo = rdfalgo;
            _result.rdfratio = rdfratio;
            _result.rdfvars = rdfvars;
            _result.rdfglobalseed = rdfglobalseed;
            _result.rdfsplitstrength = rdfsplitstrength;
            _result.rdfimportance = rdfimportance;
            _result.dsmin = (double[])dsmin.Clone();
            _result.dsmax = (double[])dsmax.Clone();
            _result.dsbinary = (bool[])dsbinary.Clone();
            _result.dsravg = dsravg;
            _result.dsctotals = (int[])dsctotals.Clone();
            _result.rdfprogress = rdfprogress;
            _result.rdftotal = rdftotal;
            _result.workpool = (smp.shared_pool)workpool.make_copy();
            _result.votepool = (smp.shared_pool)votepool.make_copy();
            _result.treepool = (smp.shared_pool)treepool.make_copy();
            _result.treefactory = (smp.shared_pool)treefactory.make_copy();
            _result.neediobmatrix = neediobmatrix;
            _result.iobmatrix = (bool[,])iobmatrix.Clone();
            _result.varimpshuffle2 = (int[])varimpshuffle2.Clone();
            return _result;
        }
    };


    public class dfworkbuf : apobject
    {
        public int[] classpriors;
        public int[] varpool;
        public int varpoolsize;
        public int[] trnset;
        public int trnsize;
        public double[] trnlabelsr;
        public int[] trnlabelsi;
        public int[] oobset;
        public int oobsize;
        public double[] ooblabelsr;
        public int[] ooblabelsi;
        public double[] treebuf;
        public double[] curvals;
        public double[] bestvals;
        public int[] tmp0i;
        public int[] tmp1i;
        public double[] tmp0r;
        public double[] tmp1r;
        public double[] tmp2r;
        public double[] tmp3r;
        public int[] tmpnrms2;
        public int[] classtotals0;
        public int[] classtotals1;
        public int[] classtotals01;
        public dfworkbuf()
        {
            init();
        }
        public override void init()
        {
            classpriors = new int[0];
            varpool = new int[0];
            trnset = new int[0];
            trnlabelsr = new double[0];
            trnlabelsi = new int[0];
            oobset = new int[0];
            ooblabelsr = new double[0];
            ooblabelsi = new int[0];
            treebuf = new double[0];
            curvals = new double[0];
            bestvals = new double[0];
            tmp0i = new int[0];
            tmp1i = new int[0];
            tmp0r = new double[0];
            tmp1r = new double[0];
            tmp2r = new double[0];
            tmp3r = new double[0];
            tmpnrms2 = new int[0];
            classtotals0 = new int[0];
            classtotals1 = new int[0];
            classtotals01 = new int[0];
        }
        public override apobject make_copy()
        {
            dfworkbuf _result = new dfworkbuf();
            _result.classpriors = (int[])classpriors.Clone();
            _result.varpool = (int[])varpool.Clone();
            _result.varpoolsize = varpoolsize;
            _result.trnset = (int[])trnset.Clone();
            _result.trnsize = trnsize;
            _result.trnlabelsr = (double[])trnlabelsr.Clone();
            _result.trnlabelsi = (int[])trnlabelsi.Clone();
            _result.oobset = (int[])oobset.Clone();
            _result.oobsize = oobsize;
            _result.ooblabelsr = (double[])ooblabelsr.Clone();
            _result.ooblabelsi = (int[])ooblabelsi.Clone();
            _result.treebuf = (double[])treebuf.Clone();
            _result.curvals = (double[])curvals.Clone();
            _result.bestvals = (double[])bestvals.Clone();
            _result.tmp0i = (int[])tmp0i.Clone();
            _result.tmp1i = (int[])tmp1i.Clone();
            _result.tmp0r = (double[])tmp0r.Clone();
            _result.tmp1r = (double[])tmp1r.Clone();
            _result.tmp2r = (double[])tmp2r.Clone();
            _result.tmp3r = (double[])tmp3r.Clone();
            _result.tmpnrms2 = (int[])tmpnrms2.Clone();
            _result.classtotals0 = (int[])classtotals0.Clone();
            _result.classtotals1 = (int[])classtotals1.Clone();
            _result.classtotals01 = (int[])classtotals01.Clone();
            return _result;
        }
    };


    public class dfvotebuf : apobject
    {
        public double[] trntotals;
        public double[] oobtotals;
        public int[] trncounts;
        public int[] oobcounts;
        public double[] giniimportances;
        public dfvotebuf()
        {
            init();
        }
        public override void init()
        {
            trntotals = new double[0];
            oobtotals = new double[0];
            trncounts = new int[0];
            oobcounts = new int[0];
            giniimportances = new double[0];
        }
        public override apobject make_copy()
        {
            dfvotebuf _result = new dfvotebuf();
            _result.trntotals = (double[])trntotals.Clone();
            _result.oobtotals = (double[])oobtotals.Clone();
            _result.trncounts = (int[])trncounts.Clone();
            _result.oobcounts = (int[])oobcounts.Clone();
            _result.giniimportances = (double[])giniimportances.Clone();
            return _result;
        }
    };


    /*************************************************************************
    Permutation importance buffer object, stores permutation-related losses
    for some subset of the dataset + some temporaries

    Losses      -   array[NVars+2], stores sum of squared residuals for each
                    permutation type:
                    * Losses[0..NVars-1] stores losses for permutation in J-th variable
                    * Losses[NVars] stores loss for all variables being randomly perturbed
                    * Losses[NVars+1] stores loss for unperturbed dataset
    *************************************************************************/
    public class dfpermimpbuf : apobject
    {
        public double[] losses;
        public double[] xraw;
        public double[] xdist;
        public double[] xcur;
        public double[] y;
        public double[] yv;
        public double[] targety;
        public int[] startnodes;
        public dfpermimpbuf()
        {
            init();
        }
        public override void init()
        {
            losses = new double[0];
            xraw = new double[0];
            xdist = new double[0];
            xcur = new double[0];
            y = new double[0];
            yv = new double[0];
            targety = new double[0];
            startnodes = new int[0];
        }
        public override apobject make_copy()
        {
            dfpermimpbuf _result = new dfpermimpbuf();
            _result.losses = (double[])losses.Clone();
            _result.xraw = (double[])xraw.Clone();
            _result.xdist = (double[])xdist.Clone();
            _result.xcur = (double[])xcur.Clone();
            _result.y = (double[])y.Clone();
            _result.yv = (double[])yv.Clone();
            _result.targety = (double[])targety.Clone();
            _result.startnodes = (int[])startnodes.Clone();
            return _result;
        }
    };


    public class dftreebuf : apobject
    {
        public double[] treebuf;
        public int treeidx;
        public dftreebuf()
        {
            init();
        }
        public override void init()
        {
            treebuf = new double[0];
        }
        public override apobject make_copy()
        {
            dftreebuf _result = new dftreebuf();
            _result.treebuf = (double[])treebuf.Clone();
            _result.treeidx = treeidx;
            return _result;
        }
    };


    /*************************************************************************
    Buffer object which is used to perform  various  requests  (usually  model
    inference) in the multithreaded mode (multiple threads working  with  same
    DF object).

    This object should be created with DFCreateBuffer().
    *************************************************************************/
    public class decisionforestbuffer : apobject
    {
        public double[] x;
        public double[] y;
        public decisionforestbuffer()
        {
            init();
        }
        public override void init()
        {
            x = new double[0];
            y = new double[0];
        }
        public override apobject make_copy()
        {
            decisionforestbuffer _result = new decisionforestbuffer();
            _result.x = (double[])x.Clone();
            _result.y = (double[])y.Clone();
            return _result;
        }
    };


    /*************************************************************************
    Decision forest (random forest) model.
    *************************************************************************/
    public class decisionforest : apobject
    {
        public int forestformat;
        public bool usemantissa8;
        public int nvars;
        public int nclasses;
        public int ntrees;
        public int bufsize;
        public double[] trees;
        public decisionforestbuffer buffer;
        public byte[] trees8;
        public decisionforest()
        {
            init();
        }
        public override void init()
        {
            trees = new double[0];
            buffer = new decisionforestbuffer();
            trees8 = new byte[0];
        }
        public override apobject make_copy()
        {
            decisionforest _result = new decisionforest();
            _result.forestformat = forestformat;
            _result.usemantissa8 = usemantissa8;
            _result.nvars = nvars;
            _result.nclasses = nclasses;
            _result.ntrees = ntrees;
            _result.bufsize = bufsize;
            _result.trees = (double[])trees.Clone();
            _result.buffer = (decisionforestbuffer)buffer.make_copy();
            _result.trees8 = (byte[])trees8.Clone();
            return _result;
        }
    };


    /*************************************************************************
    Decision forest training report.

    === training/oob errors ==================================================

    Following fields store training set errors:
    * relclserror           -   fraction of misclassified cases, [0,1]
    * avgce                 -   average cross-entropy in bits per symbol
    * rmserror              -   root-mean-square error
    * avgerror              -   average error
    * avgrelerror           -   average relative error

    Out-of-bag estimates are stored in fields with same names, but "oob" prefix.

    For classification problems:
    * RMS, AVG and AVGREL errors are calculated for posterior probabilities

    For regression problems:
    * RELCLS and AVGCE errors are zero

    === variable importance ==================================================

    Following fields are used to store variable importance information:

    * topvars               -   variables ordered from the most  important  to
                                less  important  ones  (according  to  current
                                choice of importance raiting).
                                For example, topvars[0] contains index of  the
                                most important variable, and topvars[0:2]  are
                                indexes of 3 most important ones and so on.
                                
    * varimportances        -   array[nvars], ratings (the  larger,  the  more
                                important the variable  is,  always  in  [0,1]
                                range).
                                By default, filled  by  zeros  (no  importance
                                ratings are  provided  unless  you  explicitly
                                request them).
                                Zero rating means that variable is not important,
                                however you will rarely encounter such a thing,
                                in many cases  unimportant  variables  produce
                                nearly-zero (but nonzero) ratings.

    Variable importance report must be EXPLICITLY requested by calling:
    * dfbuildersetimportancegini() function, if you need out-of-bag Gini-based
      importance rating also known as MDI  (fast to  calculate,  resistant  to
      overfitting  issues,   but   has   some   bias  towards  continuous  and
      high-cardinality categorical variables)
    * dfbuildersetimportancetrngini() function, if you need training set Gini-
      -based importance rating (what other packages typically report).
    * dfbuildersetimportancepermutation() function, if you  need  permutation-
      based importance rating also known as MDA (slower to calculate, but less
      biased)
    * dfbuildersetimportancenone() function,  if  you  do  not  need  importance
      ratings - ratings will be zero, topvars[] will be [0,1,2,...]

    Different importance ratings (Gini or permutation) produce  non-comparable
    values. Although in all cases rating values lie in [0,1] range, there  are
    exist differences:
    * informally speaking, Gini importance rating tends to divide "unit amount
      of importance"  between  several  important  variables, i.e. it produces
      estimates which roughly sum to 1.0 (or less than 1.0, if your  task  can
      not be solved exactly). If all variables  are  equally  important,  they
      will have same rating,  roughly  1/NVars,  even  if  every  variable  is
      critically important.
    * from the other side, permutation importance tells us what percentage  of
      the model predictive power will be ruined  by  permuting  this  specific
      variable. It does not produce estimates which  sum  to  one.  Critically
      important variable will have rating close  to  1.0,  and  you  may  have
      multiple variables with such a rating.

    More information on variable importance ratings can be found  in  comments
    on the dfbuildersetimportancegini() and dfbuildersetimportancepermutation()
    functions.
    *************************************************************************/
    public class dfreport : apobject
    {
        public double relclserror;
        public double avgce;
        public double rmserror;
        public double avgerror;
        public double avgrelerror;
        public double oobrelclserror;
        public double oobavgce;
        public double oobrmserror;
        public double oobavgerror;
        public double oobavgrelerror;
        public int[] topvars;
        public double[] varimportances;
        public dfreport()
        {
            init();
        }
        public override void init()
        {
            topvars = new int[0];
            varimportances = new double[0];
        }
        public override apobject make_copy()
        {
            dfreport _result = new dfreport();
            _result.relclserror = relclserror;
            _result.avgce = avgce;
            _result.rmserror = rmserror;
            _result.avgerror = avgerror;
            _result.avgrelerror = avgrelerror;
            _result.oobrelclserror = oobrelclserror;
            _result.oobavgce = oobavgce;
            _result.oobrmserror = oobrmserror;
            _result.oobavgerror = oobavgerror;
            _result.oobavgrelerror = oobavgrelerror;
            _result.topvars = (int[])topvars.Clone();
            _result.varimportances = (double[])varimportances.Clone();
            return _result;
        }
    };


    public class dfinternalbuffers : apobject
    {
        public double[] treebuf;
        public int[] idxbuf;
        public double[] tmpbufr;
        public double[] tmpbufr2;
        public int[] tmpbufi;
        public int[] classibuf;
        public double[] sortrbuf;
        public double[] sortrbuf2;
        public int[] sortibuf;
        public int[] varpool;
        public bool[] evsbin;
        public double[] evssplits;
        public dfinternalbuffers()
        {
            init();
        }
        public override void init()
        {
            treebuf = new double[0];
            idxbuf = new int[0];
            tmpbufr = new double[0];
            tmpbufr2 = new double[0];
            tmpbufi = new int[0];
            classibuf = new int[0];
            sortrbuf = new double[0];
            sortrbuf2 = new double[0];
            sortibuf = new int[0];
            varpool = new int[0];
            evsbin = new bool[0];
            evssplits = new double[0];
        }
        public override apobject make_copy()
        {
            dfinternalbuffers _result = new dfinternalbuffers();
            _result.treebuf = (double[])treebuf.Clone();
            _result.idxbuf = (int[])idxbuf.Clone();
            _result.tmpbufr = (double[])tmpbufr.Clone();
            _result.tmpbufr2 = (double[])tmpbufr2.Clone();
            _result.tmpbufi = (int[])tmpbufi.Clone();
            _result.classibuf = (int[])classibuf.Clone();
            _result.sortrbuf = (double[])sortrbuf.Clone();
            _result.sortrbuf2 = (double[])sortrbuf2.Clone();
            _result.sortibuf = (int[])sortibuf.Clone();
            _result.varpool = (int[])varpool.Clone();
            _result.evsbin = (bool[])evsbin.Clone();
            _result.evssplits = (double[])evssplits.Clone();
            return _result;
        }
    };




    public const int innernodewidth = 3;
    public const int leafnodewidth = 2;
    public const int dfusestrongsplits = 1;
    public const int dfuseevs = 2;
    public const int dfuncompressedv0 = 0;
    public const int dfcompressedv0 = 1;
    public const int needtrngini = 1;
    public const int needoobgini = 2;
    public const int needpermutation = 3;
    public const int permutationimportancebatchsize = 512;


    /*************************************************************************
    This function creates buffer  structure  which  can  be  used  to  perform
    parallel inference requests.

    DF subpackage  provides two sets of computing functions - ones  which  use
    internal buffer of DF model  (these  functions are single-threaded because
    they use same buffer, which can not  shared  between  threads),  and  ones
    which use external buffer.

    This function is used to initialize external buffer.

    INPUT PARAMETERS
        Model       -   DF model which is associated with newly created buffer

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
    public static void dfcreatebuffer(decisionforest model,
        decisionforestbuffer buf,
        xparams _params)
    {
        buf.x = new double[model.nvars];
        buf.y = new double[model.nclasses];
    }


    /*************************************************************************
    This subroutine creates DecisionForestBuilder  object  which  is  used  to
    train decision forests.

    By default, new builder stores empty dataset and some  reasonable  default
    settings. At the very least, you should specify dataset prior to  building
    decision forest. You can also tweak settings of  the  forest  construction
    algorithm (recommended, although default setting should work well).

    Following actions are mandatory:
    * calling dfbuildersetdataset() to specify dataset
    * calling dfbuilderbuildrandomforest()  to  build  decision  forest  using
      current dataset and default settings
      
    Additionally, you may call:
    * dfbuildersetrndvars() or dfbuildersetrndvarsratio() to specify number of
      variables randomly chosen for each split
    * dfbuildersetsubsampleratio() to specify fraction of the dataset randomly
      subsampled to build each tree
    * dfbuildersetseed() to control random seed chosen for tree construction

    INPUT PARAMETERS:
        none

    OUTPUT PARAMETERS:
        S           -   decision forest builder

      -- ALGLIB --
         Copyright 21.05.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void dfbuildercreate(decisionforestbuilder s,
        xparams _params)
    {

        //
        // Empty dataset
        //
        s.dstype = -1;
        s.npoints = 0;
        s.nvars = 0;
        s.nclasses = 1;

        //
        // Default training settings
        //
        s.rdfalgo = 0;
        s.rdfratio = 0.5;
        s.rdfvars = 0.0;
        s.rdfglobalseed = 0;
        s.rdfsplitstrength = 2;
        s.rdfimportance = 0;

        //
        // Other fields
        //
        s.rdfprogress = 0;
        s.rdftotal = 1;
    }


    /*************************************************************************
    This subroutine adds dense dataset to the internal storage of the  builder
    object. Specifying your dataset in the dense format means that  the  dense
    version of the forest construction algorithm will be invoked.

    INPUT PARAMETERS:
        S           -   decision forest builder object
        XY          -   array[NPoints,NVars+1] (minimum size; actual size  can
                        be larger, only leading part is used anyway), dataset:
                        * first NVars elements of each row store values of the
                          independent variables
                        * last  column  store class number (in 0...NClasses-1)
                          or real value of the dependent variable
        NPoints     -   number of rows in the dataset, NPoints>=1
        NVars       -   number of independent variables, NVars>=1 
        NClasses    -   indicates type of the problem being solved:
                        * NClasses>=2 means  that  classification  problem  is
                          solved  (last  column  of  the  dataset stores class
                          number)
                        * NClasses=1 means that regression problem  is  solved
                          (last column of the dataset stores variable value)

    OUTPUT PARAMETERS:
        S           -   decision forest builder

      -- ALGLIB --
         Copyright 21.05.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void dfbuildersetdataset(decisionforestbuilder s,
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
        ap.assert(npoints >= 1, "dfbuildersetdataset: npoints<1");
        ap.assert(nvars >= 1, "dfbuildersetdataset: nvars<1");
        ap.assert(nclasses >= 1, "dfbuildersetdataset: nclasses<1");
        ap.assert(ap.rows(xy) >= npoints, "dfbuildersetdataset: rows(xy)<npoints");
        ap.assert(ap.cols(xy) >= nvars + 1, "dfbuildersetdataset: cols(xy)<nvars+1");
        ap.assert(apserv.apservisfinitematrix(xy, npoints, nvars + 1, _params), "dfbuildersetdataset: xy parameter contains INFs or NANs");
        if (nclasses > 1)
        {
            for (i = 0; i <= npoints - 1; i++)
            {
                j = (int)Math.Round(xy[i, nvars]);
                ap.assert(j >= 0 && j < nclasses, "dfbuildersetdataset: last column of xy contains invalid class number");
            }
        }

        //
        // Set dataset
        //
        s.dstype = 0;
        s.npoints = npoints;
        s.nvars = nvars;
        s.nclasses = nclasses;
        apserv.rvectorsetlengthatleast(ref s.dsdata, npoints * nvars, _params);
        for (i = 0; i <= npoints - 1; i++)
        {
            for (j = 0; j <= nvars - 1; j++)
            {
                s.dsdata[j * npoints + i] = xy[i, j];
            }
        }
        if (nclasses > 1)
        {
            apserv.ivectorsetlengthatleast(ref s.dsival, npoints, _params);
            for (i = 0; i <= npoints - 1; i++)
            {
                s.dsival[i] = (int)Math.Round(xy[i, nvars]);
            }
        }
        else
        {
            apserv.rvectorsetlengthatleast(ref s.dsrval, npoints, _params);
            for (i = 0; i <= npoints - 1; i++)
            {
                s.dsrval[i] = xy[i, nvars];
            }
        }
    }


    /*************************************************************************
    This function sets number  of  variables  (in  [1,NVars]  range)  used  by
    decision forest construction algorithm.

    The default option is to use roughly sqrt(NVars) variables.

    INPUT PARAMETERS:
        S           -   decision forest builder object
        RndVars     -   number of randomly selected variables; values  outside
                        of [1,NVars] range are silently clipped.

    OUTPUT PARAMETERS:
        S           -   decision forest builder

      -- ALGLIB --
         Copyright 21.05.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void dfbuildersetrndvars(decisionforestbuilder s,
        int rndvars,
        xparams _params)
    {
        s.rdfvars = Math.Max(rndvars, 1);
    }


    /*************************************************************************
    This function sets number of variables used by decision forest construction
    algorithm as a fraction of total variable count (0,1) range.

    The default option is to use roughly sqrt(NVars) variables.

    INPUT PARAMETERS:
        S           -   decision forest builder object
        F           -   round(NVars*F) variables are selected

    OUTPUT PARAMETERS:
        S           -   decision forest builder

      -- ALGLIB --
         Copyright 21.05.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void dfbuildersetrndvarsratio(decisionforestbuilder s,
        double f,
        xparams _params)
    {
        ap.assert(math.isfinite(f), "dfbuildersetrndvarsratio: F is INF or NAN");
        s.rdfvars = -Math.Max(f, math.machineepsilon);
    }


    /*************************************************************************
    This function tells decision forest builder to automatically choose number
    of  variables  used  by  decision forest construction  algorithm.  Roughly
    sqrt(NVars) variables will be used.

    INPUT PARAMETERS:
        S           -   decision forest builder object

    OUTPUT PARAMETERS:
        S           -   decision forest builder

      -- ALGLIB --
         Copyright 21.05.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void dfbuildersetrndvarsauto(decisionforestbuilder s,
        xparams _params)
    {
        s.rdfvars = 0;
    }


    /*************************************************************************
    This function sets size of dataset subsample generated the decision forest
    construction algorithm. Size is specified as a fraction of  total  dataset
    size.

    The default option is to use 50% of the dataset for training, 50% for  the
    OOB estimates. You can decrease fraction F down to 10%, 1% or  even  below
    in order to reduce overfitting.

    INPUT PARAMETERS:
        S           -   decision forest builder object
        F           -   fraction of the dataset to use, in (0,1] range. Values
                        outside of this range will  be  silently  clipped.  At
                        least one element is always selected for the  training
                        set.

    OUTPUT PARAMETERS:
        S           -   decision forest builder

      -- ALGLIB --
         Copyright 21.05.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void dfbuildersetsubsampleratio(decisionforestbuilder s,
        double f,
        xparams _params)
    {
        ap.assert(math.isfinite(f), "dfbuildersetrndvarsfraction: F is INF or NAN");
        s.rdfratio = Math.Max(f, math.machineepsilon);
    }


    /*************************************************************************
    This function sets seed used by internal RNG for  random  subsampling  and
    random selection of variable subsets.

    By default random seed is used, i.e. every time you build decision forest,
    we seed generator with new value  obtained  from  system-wide  RNG.  Thus,
    decision forest builder returns non-deterministic results. You can  change
    such behavior by specyfing fixed positive seed value.

    INPUT PARAMETERS:
        S           -   decision forest builder object
        SeedVal     -   seed value:
                        * positive values are used for seeding RNG with fixed
                          seed, i.e. subsequent runs on same data will return
                          same decision forests
                        * non-positive seed means that random seed is used
                          for every run of builder, i.e. subsequent  runs  on
                          same  datasets  will  return   slightly   different
                          decision forests

    OUTPUT PARAMETERS:
        S           -   decision forest builder, see

      -- ALGLIB --
         Copyright 21.05.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void dfbuildersetseed(decisionforestbuilder s,
        int seedval,
        xparams _params)
    {
        s.rdfglobalseed = seedval;
    }


    /*************************************************************************
    This function sets random decision forest construction algorithm.

    As for now, only one decision forest construction algorithm is supported -
    a dense "baseline" RDF algorithm.

    INPUT PARAMETERS:
        S           -   decision forest builder object
        AlgoType    -   algorithm type:
                        * 0 = baseline dense RDF

    OUTPUT PARAMETERS:
        S           -   decision forest builder, see

      -- ALGLIB --
         Copyright 21.05.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void dfbuildersetrdfalgo(decisionforestbuilder s,
        int algotype,
        xparams _params)
    {
        ap.assert(algotype == 0, "dfbuildersetrdfalgo: unexpected algotype");
        s.rdfalgo = algotype;
    }


    /*************************************************************************
    This  function  sets  split  selection  algorithm used by decision  forest
    classifier. You may choose several algorithms, with  different  speed  and
    quality of the results.

    INPUT PARAMETERS:
        S           -   decision forest builder object
        SplitStrength-  split type:
                        * 0 = split at the random position, fastest one
                        * 1 = split at the middle of the range
                        * 2 = strong split at the best point of the range (default)

    OUTPUT PARAMETERS:
        S           -   decision forest builder, see

      -- ALGLIB --
         Copyright 21.05.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void dfbuildersetrdfsplitstrength(decisionforestbuilder s,
        int splitstrength,
        xparams _params)
    {
        ap.assert((splitstrength == 0 || splitstrength == 1) || splitstrength == 2, "dfbuildersetrdfsplitstrength: unexpected split type");
        s.rdfsplitstrength = splitstrength;
    }


    /*************************************************************************
    This  function  tells  decision  forest  construction  algorithm  to   use
    Gini impurity based variable importance estimation (also known as MDI).

    This version of importance estimation algorithm analyzes mean decrease  in
    impurity (MDI) on training sample during  splits.  The result  is  divided
    by impurity at the root node in order to produce estimate in [0,1] range.

    Such estimates are fast to calculate and beautifully  normalized  (sum  to
    one) but have following downsides:
    * They ALWAYS sum to 1.0, even if output is completely unpredictable. I.e.
      MDI allows to order variables by importance, but does not  tell us about
      "absolute" importances of variables
    * there exist some bias towards continuous and high-cardinality categorical
      variables
      
    NOTE: informally speaking, MDA (permutation importance) rating answers the
          question  "what  part  of  the  model  predictive power is ruined by
          permuting k-th variable?" while MDI tells us "what part of the model
          predictive power was achieved due to usage of k-th variable".

          Thus, MDA rates each variable independently at "0 to 1"  scale while
          MDI (and OOB-MDI too) tends to divide "unit  amount  of  importance"
          between several important variables.
          
          If  all  variables  are  equally  important,  they  will  have  same
          MDI/OOB-MDI rating, equal (for OOB-MDI: roughly equal)  to  1/NVars.
          However, roughly  same  picture  will  be  produced   for  the  "all
          variables provide information no one is critical" situation  and for
          the "all variables are critical, drop any one, everything is ruined"
          situation.
          
          Contrary to that, MDA will rate critical variable as ~1.0 important,
          and important but non-critical variable will  have  less  than  unit
          rating.

    NOTE: quite an often MDA and MDI return same results. It generally happens
          on problems with low test set error (a few  percents  at  most)  and
          large enough training set to avoid overfitting.
          
          The difference between MDA, MDI and OOB-MDI becomes  important  only
          on "hard" tasks with high test set error and/or small training set.

    INPUT PARAMETERS:
        S           -   decision forest builder object

    OUTPUT PARAMETERS:
        S           -   decision forest builder object. Next call to the forest
                        construction function will produce:
                        * importance estimates in rep.varimportances field
                        * variable ranks in rep.topvars field

      -- ALGLIB --
         Copyright 29.07.2019 by Bochkanov Sergey
    *************************************************************************/
    public static void dfbuildersetimportancetrngini(decisionforestbuilder s,
        xparams _params)
    {
        s.rdfimportance = needtrngini;
    }


    /*************************************************************************
    This  function  tells  decision  forest  construction  algorithm  to   use
    out-of-bag version of Gini variable importance estimation (also  known  as
    OOB-MDI).

    This version of importance estimation algorithm analyzes mean decrease  in
    impurity (MDI) on out-of-bag sample during splits. The result  is  divided
    by impurity at the root node in order to produce estimate in [0,1] range.

    Such estimates are fast to calculate and resistant to  overfitting  issues
    (thanks to the  out-of-bag  estimates  used). However, OOB Gini rating has
    following downsides:
    * there exist some bias towards continuous and high-cardinality categorical
      variables
    * Gini rating allows us to order variables by importance, but it  is  hard
      to define importance of the variable by itself.
      
    NOTE: informally speaking, MDA (permutation importance) rating answers the
          question  "what  part  of  the  model  predictive power is ruined by
          permuting k-th variable?" while MDI tells us "what part of the model
          predictive power was achieved due to usage of k-th variable".

          Thus, MDA rates each variable independently at "0 to 1"  scale while
          MDI (and OOB-MDI too) tends to divide "unit  amount  of  importance"
          between several important variables.
          
          If  all  variables  are  equally  important,  they  will  have  same
          MDI/OOB-MDI rating, equal (for OOB-MDI: roughly equal)  to  1/NVars.
          However, roughly  same  picture  will  be  produced   for  the  "all
          variables provide information no one is critical" situation  and for
          the "all variables are critical, drop any one, everything is ruined"
          situation.
          
          Contrary to that, MDA will rate critical variable as ~1.0 important,
          and important but non-critical variable will  have  less  than  unit
          rating.

    NOTE: quite an often MDA and MDI return same results. It generally happens
          on problems with low test set error (a few  percents  at  most)  and
          large enough training set to avoid overfitting.
          
          The difference between MDA, MDI and OOB-MDI becomes  important  only
          on "hard" tasks with high test set error and/or small training set.

    INPUT PARAMETERS:
        S           -   decision forest builder object

    OUTPUT PARAMETERS:
        S           -   decision forest builder object. Next call to the forest
                        construction function will produce:
                        * importance estimates in rep.varimportances field
                        * variable ranks in rep.topvars field

      -- ALGLIB --
         Copyright 29.07.2019 by Bochkanov Sergey
    *************************************************************************/
    public static void dfbuildersetimportanceoobgini(decisionforestbuilder s,
        xparams _params)
    {
        s.rdfimportance = needoobgini;
    }


    /*************************************************************************
    This  function  tells  decision  forest  construction  algorithm  to   use
    permutation variable importance estimator (also known as MDA).

    This version of importance estimation algorithm analyzes mean increase  in
    out-of-bag sum of squared  residuals  after  random  permutation  of  J-th
    variable. The result is divided by error computed with all variables being
    perturbed in order to produce R-squared-like estimate in [0,1] range.

    Such estimate  is  slower to calculate than Gini-based rating  because  it
    needs multiple inference runs for each of variables being studied.

    ALGLIB uses parallelized and highly  optimized  algorithm  which  analyzes
    path through the decision tree and allows  to  handle  most  perturbations
    in O(1) time; nevertheless, requesting MDA importances may increase forest
    construction time from 10% to 200% (or more,  if  you  have  thousands  of
    variables).

    However, MDA rating has following benefits over Gini-based ones:
    * no bias towards specific variable types
    * ability to directly evaluate "absolute" importance of some  variable  at
      "0 to 1" scale (contrary to Gini-based rating, which returns comparative
      importances).
      
    NOTE: informally speaking, MDA (permutation importance) rating answers the
          question  "what  part  of  the  model  predictive power is ruined by
          permuting k-th variable?" while MDI tells us "what part of the model
          predictive power was achieved due to usage of k-th variable".

          Thus, MDA rates each variable independently at "0 to 1"  scale while
          MDI (and OOB-MDI too) tends to divide "unit  amount  of  importance"
          between several important variables.
          
          If  all  variables  are  equally  important,  they  will  have  same
          MDI/OOB-MDI rating, equal (for OOB-MDI: roughly equal)  to  1/NVars.
          However, roughly  same  picture  will  be  produced   for  the  "all
          variables provide information no one is critical" situation  and for
          the "all variables are critical, drop any one, everything is ruined"
          situation.
          
          Contrary to that, MDA will rate critical variable as ~1.0 important,
          and important but non-critical variable will  have  less  than  unit
          rating.

    NOTE: quite an often MDA and MDI return same results. It generally happens
          on problems with low test set error (a few  percents  at  most)  and
          large enough training set to avoid overfitting.
          
          The difference between MDA, MDI and OOB-MDI becomes  important  only
          on "hard" tasks with high test set error and/or small training set.

    INPUT PARAMETERS:
        S           -   decision forest builder object

    OUTPUT PARAMETERS:
        S           -   decision forest builder object. Next call to the forest
                        construction function will produce:
                        * importance estimates in rep.varimportances field
                        * variable ranks in rep.topvars field

      -- ALGLIB --
         Copyright 29.07.2019 by Bochkanov Sergey
    *************************************************************************/
    public static void dfbuildersetimportancepermutation(decisionforestbuilder s,
        xparams _params)
    {
        s.rdfimportance = needpermutation;
    }


    /*************************************************************************
    This  function  tells  decision  forest  construction  algorithm  to  skip
    variable importance estimation.

    INPUT PARAMETERS:
        S           -   decision forest builder object

    OUTPUT PARAMETERS:
        S           -   decision forest builder object. Next call to the forest
                        construction function will result in forest being built
                        without variable importance estimation.

      -- ALGLIB --
         Copyright 29.07.2019 by Bochkanov Sergey
    *************************************************************************/
    public static void dfbuildersetimportancenone(decisionforestbuilder s,
        xparams _params)
    {
        s.rdfimportance = 0;
    }


    /*************************************************************************
    This function is an alias for dfbuilderpeekprogress(), left in ALGLIB  for
    backward compatibility reasons.

      -- ALGLIB --
         Copyright 21.05.2018 by Bochkanov Sergey
    *************************************************************************/
    public static double dfbuildergetprogress(decisionforestbuilder s,
        xparams _params)
    {
        double result = 0;

        result = dfbuilderpeekprogress(s, _params);
        return result;
    }


    /*************************************************************************
    This function is used to peek into  decision  forest  construction process
    from some other thread and get current progress indicator.

    It returns value in [0,1].

    INPUT PARAMETERS:
        S           -   decision forest builder object used  to  build  forest
                        in some other thread

    RESULT:
        progress value, in [0,1]

      -- ALGLIB --
         Copyright 21.05.2018 by Bochkanov Sergey
    *************************************************************************/
    public static double dfbuilderpeekprogress(decisionforestbuilder s,
        xparams _params)
    {
        double result = 0;

        result = s.rdfprogress / Math.Max(s.rdftotal, 1);
        result = Math.Max(result, 0);
        result = Math.Min(result, 1);
        return result;
    }


    /*************************************************************************
    This subroutine builds decision forest according to current settings using
    dataset internally stored in the builder object. Dense algorithm is used.

    NOTE: this   function   uses   dense  algorithm  for  forest  construction
          independently from the dataset format (dense or sparse).
      
    NOTE: forest built with this function is  stored  in-memory  using  64-bit
          data structures for offsets/indexes/split values. It is possible  to
          convert  forest  into  more  memory-efficient   compressed    binary
          representation.  Depending  on  the  problem  properties,  3.7x-5.7x
          compression factors are possible.
          
          The downsides of compression are (a) slight reduction in  the  model
          accuracy and (b) ~1.5x reduction in  the  inference  speed  (due  to
          increased complexity of the storage format).
          
          See comments on dfbinarycompression() for more info.

    Default settings are used by the algorithm; you can tweak  them  with  the
    help of the following functions:
    * dfbuildersetrfactor() - to control a fraction of the  dataset  used  for
      subsampling
    * dfbuildersetrandomvars() - to control number of variables randomly chosen
      for decision rule creation

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
        S           -   decision forest builder object
        NTrees      -   NTrees>=1, number of trees to train

    OUTPUT PARAMETERS:
        DF          -   decision forest. You can compress this forest to  more
                        compact 16-bit representation with dfbinarycompression()
        Rep         -   report, see below for information on its fields.
        
    === report information produced by forest construction function ==========

    Decision forest training report includes following information:
    * training set errors
    * out-of-bag estimates of errors
    * variable importance ratings

    Following fields are used to store information:
    * training set errors are stored in rep.relclserror, rep.avgce, rep.rmserror,
      rep.avgerror and rep.avgrelerror
    * out-of-bag estimates of errors are stored in rep.oobrelclserror, rep.oobavgce,
      rep.oobrmserror, rep.oobavgerror and rep.oobavgrelerror

    Variable importance reports, if requested by dfbuildersetimportancegini(),
    dfbuildersetimportancetrngini() or dfbuildersetimportancepermutation()
    call, are stored in:
    * rep.varimportances field stores importance ratings
    * rep.topvars stores variable indexes ordered from the most important to
      less important ones

    You can find more information about report fields in:
    * comments on dfreport structure
    * comments on dfbuildersetimportancegini function
    * comments on dfbuildersetimportancetrngini function
    * comments on dfbuildersetimportancepermutation function

      -- ALGLIB --
         Copyright 21.05.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void dfbuilderbuildrandomforest(decisionforestbuilder s,
        int ntrees,
        decisionforest df,
        dfreport rep,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int nvars = 0;
        int nclasses = 0;
        int npoints = 0;
        int trnsize = 0;
        int maxtreesize = 0;
        int sessionseed = 0;
        dfworkbuf workbufseed = new dfworkbuf();
        dfvotebuf votebufseed = new dfvotebuf();
        dftreebuf treebufseed = new dftreebuf();

        ap.assert(ntrees >= 1, "DFBuilderBuildRandomForest: ntrees<1");
        cleanreport(s, rep, _params);
        npoints = s.npoints;
        nvars = s.nvars;
        nclasses = s.nclasses;

        //
        // Set up progress counter
        //
        s.rdfprogress = 0;
        s.rdftotal = ntrees * npoints;
        if (s.rdfimportance == needpermutation)
        {
            s.rdftotal = s.rdftotal + ntrees * npoints;
        }

        //
        // Quick exit for empty dataset
        //
        if (s.dstype == -1 || npoints == 0)
        {
            ap.assert(leafnodewidth == 2, "DFBuilderBuildRandomForest: integrity check failed");
            df.forestformat = dfuncompressedv0;
            df.nvars = s.nvars;
            df.nclasses = s.nclasses;
            df.ntrees = 1;
            df.bufsize = 1 + leafnodewidth;
            df.trees = new double[1 + leafnodewidth];
            df.trees[0] = 1 + leafnodewidth;
            df.trees[1] = -1;
            df.trees[2] = 0.0;
            dfcreatebuffer(df, df.buffer, _params);
            return;
        }
        ap.assert(npoints > 0, "DFBuilderBuildRandomForest: integrity check failed");

        //
        // Analyze dataset statistics, perform preprocessing
        //
        analyzeandpreprocessdataset(s, _params);

        //
        // Prepare "work", "vote" and "tree" pools and other settings
        //
        trnsize = (int)Math.Round(npoints * s.rdfratio);
        trnsize = Math.Max(trnsize, 1);
        trnsize = Math.Min(trnsize, npoints);
        maxtreesize = 1 + innernodewidth * (trnsize - 1) + leafnodewidth * trnsize;
        workbufseed.varpool = new int[nvars];
        workbufseed.trnset = new int[trnsize];
        workbufseed.oobset = new int[npoints - trnsize];
        workbufseed.tmp0i = new int[npoints];
        workbufseed.tmp1i = new int[npoints];
        workbufseed.tmp0r = new double[npoints];
        workbufseed.tmp1r = new double[npoints];
        workbufseed.tmp2r = new double[npoints];
        workbufseed.tmp3r = new double[npoints];
        workbufseed.trnlabelsi = new int[npoints];
        workbufseed.trnlabelsr = new double[npoints];
        workbufseed.ooblabelsi = new int[npoints];
        workbufseed.ooblabelsr = new double[npoints];
        workbufseed.curvals = new double[npoints];
        workbufseed.bestvals = new double[npoints];
        workbufseed.classpriors = new int[nclasses];
        workbufseed.classtotals0 = new int[nclasses];
        workbufseed.classtotals1 = new int[nclasses];
        workbufseed.classtotals01 = new int[2 * nclasses];
        workbufseed.treebuf = new double[maxtreesize];
        workbufseed.trnsize = trnsize;
        workbufseed.oobsize = npoints - trnsize;
        votebufseed.trntotals = new double[npoints * nclasses];
        votebufseed.oobtotals = new double[npoints * nclasses];
        for (i = 0; i <= npoints * nclasses - 1; i++)
        {
            votebufseed.trntotals[i] = 0;
            votebufseed.oobtotals[i] = 0;
        }
        votebufseed.trncounts = new int[npoints];
        votebufseed.oobcounts = new int[npoints];
        for (i = 0; i <= npoints - 1; i++)
        {
            votebufseed.trncounts[i] = 0;
            votebufseed.oobcounts[i] = 0;
        }
        votebufseed.giniimportances = new double[nvars];
        for (i = 0; i <= nvars - 1; i++)
        {
            votebufseed.giniimportances[i] = 0.0;
        }
        treebufseed.treeidx = -1;
        smp.ae_shared_pool_set_seed(s.workpool, workbufseed);
        smp.ae_shared_pool_set_seed(s.votepool, votebufseed);
        smp.ae_shared_pool_set_seed(s.treepool, treebufseed);
        smp.ae_shared_pool_set_seed(s.treefactory, treebufseed);

        //
        // Select session seed (individual trees are constructed using
        // combination of session and local seeds).
        //
        sessionseed = s.rdfglobalseed;
        if (s.rdfglobalseed <= 0)
        {
            sessionseed = math.randominteger(30000);
        }

        //
        // Prepare In-and-Out-of-Bag matrix, if needed
        //
        s.neediobmatrix = s.rdfimportance == needpermutation;
        if (s.neediobmatrix)
        {

            //
            // Prepare default state of In-and-Out-of-Bag matrix
            //
            apserv.bmatrixsetlengthatleast(ref s.iobmatrix, ntrees, npoints, _params);
            for (i = 0; i <= ntrees - 1; i++)
            {
                for (j = 0; j <= npoints - 1; j++)
                {
                    s.iobmatrix[i, j] = false;
                }
            }
        }

        //
        // Build trees (in parallel, if possible)
        //
        buildrandomtree(s, 0, ntrees, _params);

        //
        // Merge trees and output result
        //
        mergetrees(s, df, _params);

        //
        // Process voting results and output training set and OOB errors.
        // Finalize tree construction.
        //
        processvotingresults(s, ntrees, votebufseed, rep, _params);
        dfcreatebuffer(df, df.buffer, _params);

        //
        // Perform variable importance estimation
        //
        estimatevariableimportance(s, sessionseed, df, ntrees, rep, _params);

        //
        // Update progress counter
        //
        s.rdfprogress = s.rdftotal;
    }


    /*************************************************************************
    This function performs binary compression of the decision forest.

    Original decision forest produced by the  forest  builder  is stored using
    64-bit representation for all numbers - offsets, variable  indexes,  split
    points.

    It is possible to significantly reduce model size by means of:
    * using compressed  dynamic encoding for integers  (offsets  and  variable
      indexes), which uses just 1 byte to store small ints  (less  than  128),
      just 2 bytes for larger values (less than 128^2) and so on
    * storing floating point numbers using 8-bit exponent and 16-bit mantissa

    As  result,  model  needs  significantly  less  memory (compression factor
    depends on  variable and class counts). In particular:
    * NVars<128   and NClasses<128 result in 4.4x-5.7x model size reduction
    * NVars<16384 and NClasses<128 result in 3.7x-4.5x model size reduction

    Such storage format performs lossless compression  of  all  integers,  but
    compression of floating point values (split values) is lossy, with roughly
    0.01% relative error introduced during rounding. Thus, we recommend you to
    re-evaluate model accuracy after compression.

    Another downside  of  compression  is  ~1.5x reduction  in  the  inference
    speed due to necessity of dynamic decompression of the compressed model.

    INPUT PARAMETERS:
        DF      -   decision forest built by forest builder

    OUTPUT PARAMETERS:
        DF      -   replaced by compressed forest

    RESULT:
        compression factor (in-RAM size of the compressed model vs than of the
        uncompressed one), positive number larger than 1.0

      -- ALGLIB --
         Copyright 22.07.2019 by Bochkanov Sergey
    *************************************************************************/
    public static double dfbinarycompression(decisionforest df,
        xparams _params)
    {
        double result = 0;

        result = binarycompression(df, false, _params);
        return result;
    }


    /*************************************************************************
    This is a 8-bit version of dfbinarycompression.
    Not recommended for external use because it is too lossy.

      -- ALGLIB --
         Copyright 22.07.2019 by Bochkanov Sergey
    *************************************************************************/
    public static double dfbinarycompression8(decisionforest df,
        xparams _params)
    {
        double result = 0;

        result = binarycompression(df, true, _params);
        return result;
    }


    /*************************************************************************
    Inference using decision forest

    IMPORTANT: this  function  is  thread-unsafe  and  may   modify   internal
               structures of the model! You can not use same model  object for
               parallel evaluation from several threads.
               
               Use dftsprocess()  with  independent  thread-local  buffers  if
               you need thread-safe evaluation.

    INPUT PARAMETERS:
        DF      -   decision forest model
        X       -   input vector,  array[NVars]
        Y       -   possibly preallocated buffer, reallocated if too small

    OUTPUT PARAMETERS:
        Y       -   result. Regression estimate when solving regression  task,
                    vector of posterior probabilities for classification task.

    See also DFProcessI.
          

      -- ALGLIB --
         Copyright 16.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void dfprocess(decisionforest df,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        int offs = 0;
        int i = 0;
        double v = 0;
        int treesize = 0;
        bool processed = new bool();
        int i_ = 0;


        //
        // Process
        //
        // Although comments above warn you about thread-unsafety of this
        // function, it is de facto thread-safe. However, thread safety is
        // an accidental side-effect of the specific inference algorithm
        // being used. It may disappear in the future versions of the DF
        // models, so you should NOT rely on it.
        //
        if (ap.len(y) < df.nclasses)
        {
            y = new double[df.nclasses];
        }
        for (i = 0; i <= df.nclasses - 1; i++)
        {
            y[i] = 0;
        }
        processed = false;
        if (df.forestformat == dfuncompressedv0)
        {

            //
            // Process trees stored in uncompressed format
            //
            offs = 0;
            for (i = 0; i <= df.ntrees - 1; i++)
            {
                dfprocessinternaluncompressed(df, offs, offs + 1, x, ref y, _params);
                offs = offs + (int)Math.Round(df.trees[offs]);
            }
            processed = true;
        }
        if (df.forestformat == dfcompressedv0)
        {

            //
            // Process trees stored in compressed format
            //
            offs = 0;
            for (i = 0; i <= df.ntrees - 1; i++)
            {
                treesize = unstreamuint(df.trees8, ref offs, _params);
                dfprocessinternalcompressed(df, offs, x, ref y, _params);
                offs = offs + treesize;
            }
            processed = true;
        }
        ap.assert(processed, "DFProcess: integrity check failed (unexpected format?)");
        v = (double)1 / (double)df.ntrees;
        for (i_ = 0; i_ <= df.nclasses - 1; i_++)
        {
            y[i_] = v * y[i_];
        }
    }


    /*************************************************************************
    'interactive' variant of DFProcess for languages like Python which support
    constructs like "Y = DFProcessI(DF,X)" and interactive mode of interpreter

    This function allocates new array on each call,  so  it  is  significantly
    slower than its 'non-interactive' counterpart, but it is  more  convenient
    when you call it from command line.

    IMPORTANT: this  function  is  thread-unsafe  and  may   modify   internal
               structures of the model! You can not use same model  object for
               parallel evaluation from several threads.
               
               Use dftsprocess()  with  independent  thread-local  buffers  if
               you need thread-safe evaluation.

      -- ALGLIB --
         Copyright 28.02.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void dfprocessi(decisionforest df,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        y = new double[0];

        dfprocess(df, x, ref y, _params);
    }


    /*************************************************************************
    This function returns first component of the  inferred  vector  (i.e.  one
    with index #0).

    It is a convenience wrapper for dfprocess() intended for either:
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
               
               Use dftsprocess() with  independent  thread-local  buffers,  if
               you need thread-safe evaluation.

    INPUT PARAMETERS:
        Model   -   DF model
        X       -   input vector,  array[0..NVars-1].

    RESULT:
        Y[0]

      -- ALGLIB --
         Copyright 15.02.2019 by Bochkanov Sergey
    *************************************************************************/
    public static double dfprocess0(decisionforest model,
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
        dfprocess(model, model.buffer.x, ref model.buffer.y, _params);
        result = model.buffer.y[0];
        return result;
    }


    /*************************************************************************
    This function returns most probable class number for an  input  X.  It  is
    same as calling  dfprocess(model,x,y), then determining i=argmax(y[i]) and
    returning i.

    A class number in [0,NOut) range in returned for classification  problems,
    -1 is returned when this function is called for regression problems.

    IMPORTANT: this function is thread-unsafe and modifies internal structures
               of the model! You can not use same model  object  for  parallel
               evaluation from several threads.
               
               Use dftsprocess()  with independent  thread-local  buffers,  if
               you need thread-safe evaluation.

    INPUT PARAMETERS:
        Model   -   decision forest model
        X       -   input vector,  array[0..NVars-1].

    RESULT:
        class number, -1 for regression tasks

      -- ALGLIB --
         Copyright 15.02.2019 by Bochkanov Sergey
    *************************************************************************/
    public static int dfclassify(decisionforest model,
        double[] x,
        xparams _params)
    {
        int result = 0;
        int i = 0;
        int nvars = 0;
        int nout = 0;

        if (model.nclasses < 2)
        {
            result = -1;
            return result;
        }
        nvars = model.nvars;
        nout = model.nclasses;
        for (i = 0; i <= nvars - 1; i++)
        {
            model.buffer.x[i] = x[i];
        }
        dfprocess(model, model.buffer.x, ref model.buffer.y, _params);
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
    Inference using decision forest

    Thread-safe procesing using external buffer for temporaries.

    This function is thread-safe (i.e .  you  can  use  same  DF   model  from
    multiple threads) as long as you use different buffer objects for different
    threads.

    INPUT PARAMETERS:
        DF      -   decision forest model
        Buf     -   buffer object, must be  allocated  specifically  for  this
                    model with dfcreatebuffer().
        X       -   input vector,  array[NVars]
        Y       -   possibly preallocated buffer, reallocated if too small

    OUTPUT PARAMETERS:
        Y       -   result. Regression estimate when solving regression  task,
                    vector of posterior probabilities for classification task.

    See also DFProcessI.
          

      -- ALGLIB --
         Copyright 16.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void dftsprocess(decisionforest df,
        decisionforestbuffer buf,
        double[] x,
        ref double[] y,
        xparams _params)
    {

        //
        // Although docs warn you about thread-unsafety of the dfprocess()
        // function, it is de facto thread-safe. However, thread safety is
        // an accidental side-effect of the specific inference algorithm
        // being used. It may disappear in the future versions of the DF
        // models, so you should NOT rely on it.
        //
        dfprocess(df, x, ref y, _params);
    }


    /*************************************************************************
    Relative classification error on the test set

    INPUT PARAMETERS:
        DF      -   decision forest model
        XY      -   test set
        NPoints -   test set size

    RESULT:
        percent of incorrectly classified cases.
        Zero if model solves regression task.

      -- ALGLIB --
         Copyright 16.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static double dfrelclserror(decisionforest df,
        double[,] xy,
        int npoints,
        xparams _params)
    {
        double result = 0;

        result = (double)dfclserror(df, xy, npoints, _params) / (double)npoints;
        return result;
    }


    /*************************************************************************
    Average cross-entropy (in bits per element) on the test set

    INPUT PARAMETERS:
        DF      -   decision forest model
        XY      -   test set
        NPoints -   test set size

    RESULT:
        CrossEntropy/(NPoints*LN(2)).
        Zero if model solves regression task.

      -- ALGLIB --
         Copyright 16.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static double dfavgce(decisionforest df,
        double[,] xy,
        int npoints,
        xparams _params)
    {
        double result = 0;
        double[] x = new double[0];
        double[] y = new double[0];
        int i = 0;
        int j = 0;
        int k = 0;
        int tmpi = 0;
        int i_ = 0;

        x = new double[df.nvars - 1 + 1];
        y = new double[df.nclasses - 1 + 1];
        result = 0;
        for (i = 0; i <= npoints - 1; i++)
        {
            for (i_ = 0; i_ <= df.nvars - 1; i_++)
            {
                x[i_] = xy[i, i_];
            }
            dfprocess(df, x, ref y, _params);
            if (df.nclasses > 1)
            {

                //
                // classification-specific code
                //
                k = (int)Math.Round(xy[i, df.nvars]);
                tmpi = 0;
                for (j = 1; j <= df.nclasses - 1; j++)
                {
                    if ((double)(y[j]) > (double)(y[tmpi]))
                    {
                        tmpi = j;
                    }
                }
                if ((double)(y[k]) != (double)(0))
                {
                    result = result - Math.Log(y[k]);
                }
                else
                {
                    result = result - Math.Log(math.minrealnumber);
                }
            }
        }
        result = result / npoints;
        return result;
    }


    /*************************************************************************
    RMS error on the test set

    INPUT PARAMETERS:
        DF      -   decision forest model
        XY      -   test set
        NPoints -   test set size

    RESULT:
        root mean square error.
        Its meaning for regression task is obvious. As for
        classification task, RMS error means error when estimating posterior
        probabilities.

      -- ALGLIB --
         Copyright 16.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static double dfrmserror(decisionforest df,
        double[,] xy,
        int npoints,
        xparams _params)
    {
        double result = 0;
        double[] x = new double[0];
        double[] y = new double[0];
        int i = 0;
        int j = 0;
        int k = 0;
        int tmpi = 0;
        int i_ = 0;

        x = new double[df.nvars - 1 + 1];
        y = new double[df.nclasses - 1 + 1];
        result = 0;
        for (i = 0; i <= npoints - 1; i++)
        {
            for (i_ = 0; i_ <= df.nvars - 1; i_++)
            {
                x[i_] = xy[i, i_];
            }
            dfprocess(df, x, ref y, _params);
            if (df.nclasses > 1)
            {

                //
                // classification-specific code
                //
                k = (int)Math.Round(xy[i, df.nvars]);
                tmpi = 0;
                for (j = 1; j <= df.nclasses - 1; j++)
                {
                    if ((double)(y[j]) > (double)(y[tmpi]))
                    {
                        tmpi = j;
                    }
                }
                for (j = 0; j <= df.nclasses - 1; j++)
                {
                    if (j == k)
                    {
                        result = result + math.sqr(y[j] - 1);
                    }
                    else
                    {
                        result = result + math.sqr(y[j]);
                    }
                }
            }
            else
            {

                //
                // regression-specific code
                //
                result = result + math.sqr(y[0] - xy[i, df.nvars]);
            }
        }
        result = Math.Sqrt(result / (npoints * df.nclasses));
        return result;
    }


    /*************************************************************************
    Average error on the test set

    INPUT PARAMETERS:
        DF      -   decision forest model
        XY      -   test set
        NPoints -   test set size

    RESULT:
        Its meaning for regression task is obvious. As for
        classification task, it means average error when estimating posterior
        probabilities.

      -- ALGLIB --
         Copyright 16.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static double dfavgerror(decisionforest df,
        double[,] xy,
        int npoints,
        xparams _params)
    {
        double result = 0;
        double[] x = new double[0];
        double[] y = new double[0];
        int i = 0;
        int j = 0;
        int k = 0;
        int i_ = 0;

        x = new double[df.nvars - 1 + 1];
        y = new double[df.nclasses - 1 + 1];
        result = 0;
        for (i = 0; i <= npoints - 1; i++)
        {
            for (i_ = 0; i_ <= df.nvars - 1; i_++)
            {
                x[i_] = xy[i, i_];
            }
            dfprocess(df, x, ref y, _params);
            if (df.nclasses > 1)
            {

                //
                // classification-specific code
                //
                k = (int)Math.Round(xy[i, df.nvars]);
                for (j = 0; j <= df.nclasses - 1; j++)
                {
                    if (j == k)
                    {
                        result = result + Math.Abs(y[j] - 1);
                    }
                    else
                    {
                        result = result + Math.Abs(y[j]);
                    }
                }
            }
            else
            {

                //
                // regression-specific code
                //
                result = result + Math.Abs(y[0] - xy[i, df.nvars]);
            }
        }
        result = result / (npoints * df.nclasses);
        return result;
    }


    /*************************************************************************
    Average relative error on the test set

    INPUT PARAMETERS:
        DF      -   decision forest model
        XY      -   test set
        NPoints -   test set size

    RESULT:
        Its meaning for regression task is obvious. As for
        classification task, it means average relative error when estimating
        posterior probability of belonging to the correct class.

      -- ALGLIB --
         Copyright 16.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static double dfavgrelerror(decisionforest df,
        double[,] xy,
        int npoints,
        xparams _params)
    {
        double result = 0;
        double[] x = new double[0];
        double[] y = new double[0];
        int relcnt = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        int i_ = 0;

        x = new double[df.nvars - 1 + 1];
        y = new double[df.nclasses - 1 + 1];
        result = 0;
        relcnt = 0;
        for (i = 0; i <= npoints - 1; i++)
        {
            for (i_ = 0; i_ <= df.nvars - 1; i_++)
            {
                x[i_] = xy[i, i_];
            }
            dfprocess(df, x, ref y, _params);
            if (df.nclasses > 1)
            {

                //
                // classification-specific code
                //
                k = (int)Math.Round(xy[i, df.nvars]);
                for (j = 0; j <= df.nclasses - 1; j++)
                {
                    if (j == k)
                    {
                        result = result + Math.Abs(y[j] - 1);
                        relcnt = relcnt + 1;
                    }
                }
            }
            else
            {

                //
                // regression-specific code
                //
                if ((double)(xy[i, df.nvars]) != (double)(0))
                {
                    result = result + Math.Abs((y[0] - xy[i, df.nvars]) / xy[i, df.nvars]);
                    relcnt = relcnt + 1;
                }
            }
        }
        if (relcnt > 0)
        {
            result = result / relcnt;
        }
        return result;
    }


    /*************************************************************************
    Copying of DecisionForest strucure

    INPUT PARAMETERS:
        DF1 -   original

    OUTPUT PARAMETERS:
        DF2 -   copy

      -- ALGLIB --
         Copyright 13.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void dfcopy(decisionforest df1,
        decisionforest df2,
        xparams _params)
    {
        int i = 0;
        int bufsize = 0;
        int i_ = 0;

        if (df1.forestformat == dfuncompressedv0)
        {
            df2.forestformat = df1.forestformat;
            df2.nvars = df1.nvars;
            df2.nclasses = df1.nclasses;
            df2.ntrees = df1.ntrees;
            df2.bufsize = df1.bufsize;
            df2.trees = new double[df1.bufsize];
            for (i_ = 0; i_ <= df1.bufsize - 1; i_++)
            {
                df2.trees[i_] = df1.trees[i_];
            }
            dfcreatebuffer(df2, df2.buffer, _params);
            return;
        }
        if (df1.forestformat == dfcompressedv0)
        {
            df2.forestformat = df1.forestformat;
            df2.usemantissa8 = df1.usemantissa8;
            df2.nvars = df1.nvars;
            df2.nclasses = df1.nclasses;
            df2.ntrees = df1.ntrees;
            bufsize = ap.len(df1.trees8);
            df2.trees8 = new byte[bufsize];
            for (i = 0; i <= bufsize - 1; i++)
            {
                df2.trees8[i] = unchecked((byte)(df1.trees8[i]));
            }
            dfcreatebuffer(df2, df2.buffer, _params);
            return;
        }
        ap.assert(false, "DFCopy: unexpected forest format");
    }


    /*************************************************************************
    Serializer: allocation

      -- ALGLIB --
         Copyright 14.03.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void dfalloc(serializer s,
        decisionforest forest,
        xparams _params)
    {
        if (forest.forestformat == dfuncompressedv0)
        {
            s.alloc_entry();
            s.alloc_entry();
            s.alloc_entry();
            s.alloc_entry();
            s.alloc_entry();
            s.alloc_entry();
            apserv.allocrealarray(s, forest.trees, forest.bufsize, _params);
            return;
        }
        if (forest.forestformat == dfcompressedv0)
        {
            s.alloc_entry();
            s.alloc_entry();
            s.alloc_entry();
            s.alloc_entry();
            s.alloc_entry();
            s.alloc_entry();
            s.alloc_byte_array(forest.trees8);
            return;
        }
        ap.assert(false, "DFAlloc: unexpected forest format");
    }


    /*************************************************************************
    Serializer: serialization

      -- ALGLIB --
         Copyright 14.03.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void dfserialize(serializer s,
        decisionforest forest,
        xparams _params)
    {
        if (forest.forestformat == dfuncompressedv0)
        {
            s.serialize_int(scodes.getrdfserializationcode(_params));
            s.serialize_int(dfuncompressedv0);
            s.serialize_int(forest.nvars);
            s.serialize_int(forest.nclasses);
            s.serialize_int(forest.ntrees);
            s.serialize_int(forest.bufsize);
            apserv.serializerealarray(s, forest.trees, forest.bufsize, _params);
            return;
        }
        if (forest.forestformat == dfcompressedv0)
        {
            s.serialize_int(scodes.getrdfserializationcode(_params));
            s.serialize_int(forest.forestformat);
            s.serialize_bool(forest.usemantissa8);
            s.serialize_int(forest.nvars);
            s.serialize_int(forest.nclasses);
            s.serialize_int(forest.ntrees);
            s.serialize_byte_array(forest.trees8);
            return;
        }
        ap.assert(false, "DFSerialize: unexpected forest format");
    }


    /*************************************************************************
    Serializer: unserialization

      -- ALGLIB --
         Copyright 14.03.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void dfunserialize(serializer s,
        decisionforest forest,
        xparams _params)
    {
        int i0 = 0;
        int forestformat = 0;
        bool processed = new bool();


        //
        // check correctness of header
        //
        i0 = s.unserialize_int();
        ap.assert(i0 == scodes.getrdfserializationcode(_params), "DFUnserialize: stream header corrupted");

        //
        // Read forest
        //
        forestformat = s.unserialize_int();
        processed = false;
        if (forestformat == dfuncompressedv0)
        {

            //
            // Unserialize data
            //
            forest.forestformat = forestformat;
            forest.nvars = s.unserialize_int();
            forest.nclasses = s.unserialize_int();
            forest.ntrees = s.unserialize_int();
            forest.bufsize = s.unserialize_int();
            apserv.unserializerealarray(s, ref forest.trees, _params);
            processed = true;
        }
        if (forestformat == dfcompressedv0)
        {

            //
            // Unserialize data
            //
            forest.forestformat = forestformat;
            forest.usemantissa8 = s.unserialize_bool();
            forest.nvars = s.unserialize_int();
            forest.nclasses = s.unserialize_int();
            forest.ntrees = s.unserialize_int();
            forest.trees8 = s.unserialize_byte_array();
            processed = true;
        }
        ap.assert(processed, "DFUnserialize: unexpected forest format");

        //
        // Prepare buffer
        //
        dfcreatebuffer(forest, forest.buffer, _params);
    }


    /*************************************************************************
    This subroutine builds random decision forest.

    --------- DEPRECATED VERSION! USE DECISION FOREST BUILDER OBJECT ---------

      -- ALGLIB --
         Copyright 19.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void dfbuildrandomdecisionforest(double[,] xy,
        int npoints,
        int nvars,
        int nclasses,
        int ntrees,
        double r,
        ref int info,
        decisionforest df,
        dfreport rep,
        xparams _params)
    {
        int samplesize = 0;

        info = 0;

        if ((double)(r) <= (double)(0) || (double)(r) > (double)(1))
        {
            info = -1;
            return;
        }
        samplesize = Math.Max((int)Math.Round(r * npoints), 1);
        dfbuildinternal(xy, npoints, nvars, nclasses, ntrees, samplesize, Math.Max(nvars / 2, 1), dfusestrongsplits + dfuseevs, ref info, df, rep, _params);
    }


    /*************************************************************************
    This subroutine builds random decision forest.

    --------- DEPRECATED VERSION! USE DECISION FOREST BUILDER OBJECT ---------

      -- ALGLIB --
         Copyright 19.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void dfbuildrandomdecisionforestx1(double[,] xy,
        int npoints,
        int nvars,
        int nclasses,
        int ntrees,
        int nrndvars,
        double r,
        ref int info,
        decisionforest df,
        dfreport rep,
        xparams _params)
    {
        int samplesize = 0;

        info = 0;

        if ((double)(r) <= (double)(0) || (double)(r) > (double)(1))
        {
            info = -1;
            return;
        }
        if (nrndvars <= 0 || nrndvars > nvars)
        {
            info = -1;
            return;
        }
        samplesize = Math.Max((int)Math.Round(r * npoints), 1);
        dfbuildinternal(xy, npoints, nvars, nclasses, ntrees, samplesize, nrndvars, dfusestrongsplits + dfuseevs, ref info, df, rep, _params);
    }


    public static void dfbuildinternal(double[,] xy,
        int npoints,
        int nvars,
        int nclasses,
        int ntrees,
        int samplesize,
        int nfeatures,
        int flags,
        ref int info,
        decisionforest df,
        dfreport rep,
        xparams _params)
    {
        decisionforestbuilder builder = new decisionforestbuilder();
        int i = 0;

        info = 0;


        //
        // Test for inputs
        //
        if ((((((npoints < 1 || samplesize < 1) || samplesize > npoints) || nvars < 1) || nclasses < 1) || ntrees < 1) || nfeatures < 1)
        {
            info = -1;
            return;
        }
        if (nclasses > 1)
        {
            for (i = 0; i <= npoints - 1; i++)
            {
                if ((int)Math.Round(xy[i, nvars]) < 0 || (int)Math.Round(xy[i, nvars]) >= nclasses)
                {
                    info = -2;
                    return;
                }
            }
        }
        info = 1;
        dfbuildercreate(builder, _params);
        dfbuildersetdataset(builder, xy, npoints, nvars, nclasses, _params);
        dfbuildersetsubsampleratio(builder, (double)samplesize / (double)npoints, _params);
        dfbuildersetrndvars(builder, nfeatures, _params);
        dfbuilderbuildrandomforest(builder, ntrees, df, rep, _params);
    }


    /*************************************************************************
    Builds a range of random trees [TreeIdx0,TreeIdx1) using decision forest
    algorithm. Tree index is used to seed per-tree RNG.

      -- ALGLIB --
         Copyright 21.05.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void buildrandomtree(decisionforestbuilder s,
        int treeidx0,
        int treeidx1,
        xparams _params)
    {
        int treeidx = 0;
        int i = 0;
        int j = 0;
        int npoints = 0;
        int nvars = 0;
        int nclasses = 0;
        hqrnd.hqrndstate rs = new hqrnd.hqrndstate();
        dfworkbuf workbuf = null;
        dfvotebuf votebuf = null;
        dftreebuf treebuf = null;
        int treesize = 0;
        int varstoselect = 0;
        int workingsetsize = 0;
        double meanloss = 0;


        //
        // Perform parallelization
        //
        if (treeidx1 - treeidx0 > 1)
        {
            if (_trypexec_buildrandomtree(s, treeidx0, treeidx1, _params))
            {
                return;
            }
            j = (treeidx1 - treeidx0) / 2;
            buildrandomtree(s, treeidx0, treeidx0 + j, _params);
            buildrandomtree(s, treeidx0 + j, treeidx1, _params);
            return;
        }
        else
        {
            ap.assert(treeidx1 - treeidx0 == 1, "RDF: integrity check failed");
            treeidx = treeidx0;
        }

        //
        // Prepare
        //
        npoints = s.npoints;
        nvars = s.nvars;
        nclasses = s.nclasses;
        if (s.rdfglobalseed > 0)
        {
            hqrnd.hqrndseed(s.rdfglobalseed, 1 + treeidx, rs, _params);
        }
        else
        {
            hqrnd.hqrndseed(math.randominteger(30000), 1 + treeidx, rs, _params);
        }

        //
        // Retrieve buffers.
        //
        smp.ae_shared_pool_retrieve(s.workpool, ref workbuf);
        smp.ae_shared_pool_retrieve(s.votepool, ref votebuf);

        //
        // Prepare everything for tree construction.
        //
        ap.assert(workbuf.trnsize >= 1, "DForest: integrity check failed (34636)");
        ap.assert(workbuf.oobsize >= 0, "DForest: integrity check failed (45745)");
        ap.assert(workbuf.trnsize + workbuf.oobsize == npoints, "DForest: integrity check failed (89415)");
        workingsetsize = -1;
        workbuf.varpoolsize = 0;
        for (i = 0; i <= nvars - 1; i++)
        {
            if ((double)(s.dsmin[i]) != (double)(s.dsmax[i]))
            {
                workbuf.varpool[workbuf.varpoolsize] = i;
                apserv.inc(ref workbuf.varpoolsize, _params);
            }
        }
        workingsetsize = workbuf.varpoolsize;
        ap.assert(workingsetsize >= 0, "DForest: integrity check failed (73f5)");
        for (i = 0; i <= npoints - 1; i++)
        {
            workbuf.tmp0i[i] = i;
        }
        for (i = 0; i <= workbuf.trnsize - 1; i++)
        {
            j = hqrnd.hqrnduniformi(rs, npoints - i, _params);
            apserv.swapelementsi(workbuf.tmp0i, i, i + j, _params);
            workbuf.trnset[i] = workbuf.tmp0i[i];
            if (nclasses > 1)
            {
                workbuf.trnlabelsi[i] = s.dsival[workbuf.tmp0i[i]];
            }
            else
            {
                workbuf.trnlabelsr[i] = s.dsrval[workbuf.tmp0i[i]];
            }
            if (s.neediobmatrix)
            {
                s.iobmatrix[treeidx, workbuf.trnset[i]] = true;
            }
        }
        for (i = 0; i <= workbuf.oobsize - 1; i++)
        {
            j = workbuf.tmp0i[workbuf.trnsize + i];
            workbuf.oobset[i] = j;
            if (nclasses > 1)
            {
                workbuf.ooblabelsi[i] = s.dsival[j];
            }
            else
            {
                workbuf.ooblabelsr[i] = s.dsrval[j];
            }
        }
        varstoselect = (int)Math.Round(Math.Sqrt(nvars));
        if ((double)(s.rdfvars) > (double)(0))
        {
            varstoselect = (int)Math.Round(s.rdfvars);
        }
        if ((double)(s.rdfvars) < (double)(0))
        {
            varstoselect = (int)Math.Round(-(nvars * s.rdfvars));
        }
        varstoselect = Math.Max(varstoselect, 1);
        varstoselect = Math.Min(varstoselect, nvars);

        //
        // Perform recurrent construction
        //
        if (s.rdfimportance == needtrngini)
        {
            meanloss = meannrms2(nclasses, workbuf.trnlabelsi, workbuf.trnlabelsr, 0, workbuf.trnsize, workbuf.trnlabelsi, workbuf.trnlabelsr, 0, workbuf.trnsize, ref workbuf.tmpnrms2, _params);
        }
        else
        {
            meanloss = meannrms2(nclasses, workbuf.trnlabelsi, workbuf.trnlabelsr, 0, workbuf.trnsize, workbuf.ooblabelsi, workbuf.ooblabelsr, 0, workbuf.oobsize, ref workbuf.tmpnrms2, _params);
        }
        treesize = 1;
        buildrandomtreerec(s, workbuf, workingsetsize, varstoselect, workbuf.treebuf, votebuf, rs, 0, workbuf.trnsize, 0, workbuf.oobsize, meanloss, meanloss, ref treesize, _params);
        workbuf.treebuf[0] = treesize;

        //
        // Store tree
        //
        smp.ae_shared_pool_retrieve(s.treefactory, ref treebuf);
        treebuf.treebuf = new double[treesize];
        for (i = 0; i <= treesize - 1; i++)
        {
            treebuf.treebuf[i] = workbuf.treebuf[i];
        }
        treebuf.treeidx = treeidx;
        smp.ae_shared_pool_recycle(s.treepool, ref treebuf);

        //
        // Return other buffers to appropriate pools
        //
        smp.ae_shared_pool_recycle(s.workpool, ref workbuf);
        smp.ae_shared_pool_recycle(s.votepool, ref votebuf);

        //
        // Update progress indicator
        //
        apserv.threadunsafeincby(ref s.rdfprogress, npoints, _params);
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_buildrandomtree(decisionforestbuilder s,
        int treeidx0,
        int treeidx1, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    Recurrent tree construction function using  caller-allocated  buffers  and
    caller-initialized RNG.

    Following iterms are processed:
    * items [Idx0,Idx1) of WorkBuf.TrnSet
    * items [OOBIdx0, OOBIdx1) of WorkBuf.OOBSet

    TreeSize on input must be 1 (header element of the tree), on output it
    contains size of the tree.

    OOBLoss on input must contain value of MeanNRMS2(...) computed for entire
    dataset.

    Variables from #0 to #WorkingSet-1 from WorkBuf.VarPool are used (for
    block algorithm: blocks, not vars)

      -- ALGLIB --
         Copyright 21.05.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void buildrandomtreerec(decisionforestbuilder s,
        dfworkbuf workbuf,
        int workingset,
        int varstoselect,
        double[] treebuf,
        dfvotebuf votebuf,
        hqrnd.hqrndstate rs,
        int idx0,
        int idx1,
        int oobidx0,
        int oobidx1,
        double meanloss,
        double topmostmeanloss,
        ref int treesize,
        xparams _params)
    {
        int npoints = 0;
        int nclasses = 0;
        int i = 0;
        int j = 0;
        int j0 = 0;
        double v = 0;
        bool labelsaresame = new bool();
        int offs = 0;
        int varbest = 0;
        double splitbest = 0;
        int i1 = 0;
        int i2 = 0;
        int idxtrn = 0;
        int idxoob = 0;
        double meanloss0 = 0;
        double meanloss1 = 0;

        ap.assert(s.dstype == 0, "not supported skbdgfsi!");
        ap.assert(idx0 < idx1, "BuildRandomTreeRec: integrity check failed (3445)");
        ap.assert(oobidx0 <= oobidx1, "BuildRandomTreeRec: integrity check failed (7452)");
        npoints = s.npoints;
        nclasses = s.nclasses;

        //
        // Check labels: all same or not?
        //
        if (nclasses > 1)
        {
            labelsaresame = true;
            for (i = 0; i <= nclasses - 1; i++)
            {
                workbuf.classpriors[i] = 0;
            }
            j0 = workbuf.trnlabelsi[idx0];
            for (i = idx0; i <= idx1 - 1; i++)
            {
                j = workbuf.trnlabelsi[i];
                workbuf.classpriors[j] = workbuf.classpriors[j] + 1;
                labelsaresame = labelsaresame && j0 == j;
            }
        }
        else
        {
            labelsaresame = false;
        }

        //
        // Leaf node
        //
        if (idx1 - idx0 == 1 || labelsaresame)
        {
            if (nclasses == 1)
            {
                outputleaf(s, workbuf, treebuf, votebuf, idx0, idx1, oobidx0, oobidx1, ref treesize, workbuf.trnlabelsr[idx0], _params);
            }
            else
            {
                outputleaf(s, workbuf, treebuf, votebuf, idx0, idx1, oobidx0, oobidx1, ref treesize, workbuf.trnlabelsi[idx0], _params);
            }
            return;
        }

        //
        // Non-leaf node.
        // Investigate possible splits.
        //
        ap.assert(s.rdfalgo == 0, "BuildRandomForest: unexpected algo");
        choosecurrentsplitdense(s, workbuf, ref workingset, varstoselect, rs, idx0, idx1, ref varbest, ref splitbest, _params);
        if (varbest < 0)
        {

            //
            // No good split was found; make leaf (label is randomly chosen) and exit.
            //
            if (nclasses > 1)
            {
                v = workbuf.trnlabelsi[idx0 + hqrnd.hqrnduniformi(rs, idx1 - idx0, _params)];
            }
            else
            {
                v = workbuf.trnlabelsr[idx0 + hqrnd.hqrnduniformi(rs, idx1 - idx0, _params)];
            }
            outputleaf(s, workbuf, treebuf, votebuf, idx0, idx1, oobidx0, oobidx1, ref treesize, v, _params);
            return;
        }

        //
        // Good split WAS found, we can perform it:
        // * first, we split training set
        // * then, we similarly split OOB set
        //
        ap.assert(s.dstype == 0, "not supported 54bfdh");
        offs = npoints * varbest;
        i1 = idx0;
        i2 = idx1 - 1;
        while (i1 <= i2)
        {

            //
            // Reorder indexes so that left partition is in [Idx0..I1),
            // and right partition is in [I2+1..Idx1)
            //
            if (workbuf.bestvals[i1] < splitbest)
            {
                i1 = i1 + 1;
                continue;
            }
            if (workbuf.bestvals[i2] >= splitbest)
            {
                i2 = i2 - 1;
                continue;
            }
            j = workbuf.trnset[i1];
            workbuf.trnset[i1] = workbuf.trnset[i2];
            workbuf.trnset[i2] = j;
            if (nclasses > 1)
            {
                j = workbuf.trnlabelsi[i1];
                workbuf.trnlabelsi[i1] = workbuf.trnlabelsi[i2];
                workbuf.trnlabelsi[i2] = j;
            }
            else
            {
                v = workbuf.trnlabelsr[i1];
                workbuf.trnlabelsr[i1] = workbuf.trnlabelsr[i2];
                workbuf.trnlabelsr[i2] = v;
            }
            i1 = i1 + 1;
            i2 = i2 - 1;
        }
        ap.assert(i1 == i2 + 1, "BuildRandomTreeRec: integrity check failed (45rds3)");
        idxtrn = i1;
        if (oobidx0 < oobidx1)
        {

            //
            // Unlike the training subset, the out-of-bag subset corresponding to the
            // current sequence of decisions can be empty; thus, we have to explicitly
            // handle situation of zero OOB subset.
            //
            i1 = oobidx0;
            i2 = oobidx1 - 1;
            while (i1 <= i2)
            {

                //
                // Reorder indexes so that left partition is in [Idx0..I1),
                // and right partition is in [I2+1..Idx1)
                //
                if (s.dsdata[offs + workbuf.oobset[i1]] < splitbest)
                {
                    i1 = i1 + 1;
                    continue;
                }
                if (s.dsdata[offs + workbuf.oobset[i2]] >= splitbest)
                {
                    i2 = i2 - 1;
                    continue;
                }
                j = workbuf.oobset[i1];
                workbuf.oobset[i1] = workbuf.oobset[i2];
                workbuf.oobset[i2] = j;
                if (nclasses > 1)
                {
                    j = workbuf.ooblabelsi[i1];
                    workbuf.ooblabelsi[i1] = workbuf.ooblabelsi[i2];
                    workbuf.ooblabelsi[i2] = j;
                }
                else
                {
                    v = workbuf.ooblabelsr[i1];
                    workbuf.ooblabelsr[i1] = workbuf.ooblabelsr[i2];
                    workbuf.ooblabelsr[i2] = v;
                }
                i1 = i1 + 1;
                i2 = i2 - 1;
            }
            ap.assert(i1 == i2 + 1, "BuildRandomTreeRec: integrity check failed (643fs3)");
            idxoob = i1;
        }
        else
        {
            idxoob = oobidx0;
        }

        //
        // Compute estimates of NRMS2 loss over TRN or OOB subsets, update Gini importances
        //
        if (s.rdfimportance == needtrngini)
        {
            meanloss0 = meannrms2(nclasses, workbuf.trnlabelsi, workbuf.trnlabelsr, idx0, idxtrn, workbuf.trnlabelsi, workbuf.trnlabelsr, idx0, idxtrn, ref workbuf.tmpnrms2, _params);
            meanloss1 = meannrms2(nclasses, workbuf.trnlabelsi, workbuf.trnlabelsr, idxtrn, idx1, workbuf.trnlabelsi, workbuf.trnlabelsr, idxtrn, idx1, ref workbuf.tmpnrms2, _params);
        }
        else
        {
            meanloss0 = meannrms2(nclasses, workbuf.trnlabelsi, workbuf.trnlabelsr, idx0, idxtrn, workbuf.ooblabelsi, workbuf.ooblabelsr, oobidx0, idxoob, ref workbuf.tmpnrms2, _params);
            meanloss1 = meannrms2(nclasses, workbuf.trnlabelsi, workbuf.trnlabelsr, idxtrn, idx1, workbuf.ooblabelsi, workbuf.ooblabelsr, idxoob, oobidx1, ref workbuf.tmpnrms2, _params);
        }
        votebuf.giniimportances[varbest] = votebuf.giniimportances[varbest] + (meanloss - (meanloss0 + meanloss1)) / (topmostmeanloss + 1.0e-20);

        //
        // Generate tree node and subtrees (recursively)
        //
        treebuf[treesize] = varbest;
        treebuf[treesize + 1] = splitbest;
        i = treesize;
        treesize = treesize + innernodewidth;
        buildrandomtreerec(s, workbuf, workingset, varstoselect, treebuf, votebuf, rs, idx0, idxtrn, oobidx0, idxoob, meanloss0, topmostmeanloss, ref treesize, _params);
        treebuf[i + 2] = treesize;
        buildrandomtreerec(s, workbuf, workingset, varstoselect, treebuf, votebuf, rs, idxtrn, idx1, idxoob, oobidx1, meanloss1, topmostmeanloss, ref treesize, _params);
    }


    /*************************************************************************
    Estimates permutation variable importance ratings for a range of dataset
    points.

    Initial call to this function should span entire range of the dataset,
    [Idx0,Idx1)=[0,NPoints), because function performs initialization of some
    internal structures when called with these arguments.

      -- ALGLIB --
         Copyright 21.05.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void estimatevariableimportance(decisionforestbuilder s,
        int sessionseed,
        decisionforest df,
        int ntrees,
        dfreport rep,
        xparams _params)
    {
        int npoints = 0;
        int nvars = 0;
        int nclasses = 0;
        int nperm = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        dfvotebuf vote = null;
        double[] tmpr0 = new double[0];
        double[] tmpr1 = new double[0];
        int[] tmpi0 = new int[0];
        double[] losses = new double[0];
        dfpermimpbuf permseed = new dfpermimpbuf();
        dfpermimpbuf permresult = null;
        smp.shared_pool permpool = new smp.shared_pool();
        double nopermloss = 0;
        double totalpermloss = 0;
        hqrnd.hqrndstate varimprs = new hqrnd.hqrndstate();

        npoints = s.npoints;
        nvars = s.nvars;
        nclasses = s.nclasses;

        //
        // No importance rating
        //
        if (s.rdfimportance == 0)
        {
            return;
        }

        //
        // Gini importance
        //
        if (s.rdfimportance == needtrngini || s.rdfimportance == needoobgini)
        {

            //
            // Merge OOB Gini importances computed during tree generation
            //
            smp.ae_shared_pool_first_recycled(s.votepool, ref vote);
            while (vote != null)
            {
                for (i = 0; i <= nvars - 1; i++)
                {
                    rep.varimportances[i] = rep.varimportances[i] + vote.giniimportances[i] / ntrees;
                }
                smp.ae_shared_pool_next_recycled(s.votepool, ref vote);
            }
            for (i = 0; i <= nvars - 1; i++)
            {
                rep.varimportances[i] = apserv.boundval(rep.varimportances[i], 0, 1, _params);
            }

            //
            // Compute topvars[] array
            //
            tmpr0 = new double[nvars];
            for (j = 0; j <= nvars - 1; j++)
            {
                tmpr0[j] = -rep.varimportances[j];
                rep.topvars[j] = j;
            }
            tsort.tagsortfasti(ref tmpr0, ref rep.topvars, ref tmpr1, ref tmpi0, nvars, _params);
            return;
        }

        //
        // Permutation importance
        //
        if (s.rdfimportance == needpermutation)
        {
            ap.assert(df.forestformat == dfuncompressedv0, "EstimateVariableImportance: integrity check failed (ff)");
            ap.assert(ap.rows(s.iobmatrix) >= ntrees && ap.cols(s.iobmatrix) >= npoints, "EstimateVariableImportance: integrity check failed (IOB)");

            //
            // Generate packed representation of the shuffle which is applied to all variables
            //
            // Ideally we want to apply different permutations to different variables,
            // i.e. we have to generate and store NPoints*NVars random numbers.
            // However due to performance and memory restrictions we prefer to use compact
            // representation:
            // * we store one "reference" permutation P_ref in VarImpShuffle2[0:NPoints-1]
            // * a permutation P_j applied to variable J is obtained by circularly shifting
            //   elements in P_ref by VarImpShuffle2[NPoints+J]
            //
            hqrnd.hqrndseed(sessionseed, 1117, varimprs, _params);
            apserv.ivectorsetlengthatleast(ref s.varimpshuffle2, npoints + nvars, _params);
            for (i = 0; i <= npoints - 1; i++)
            {
                s.varimpshuffle2[i] = i;
            }
            for (i = 0; i <= npoints - 2; i++)
            {
                j = i + hqrnd.hqrnduniformi(varimprs, npoints - i, _params);
                k = s.varimpshuffle2[i];
                s.varimpshuffle2[i] = s.varimpshuffle2[j];
                s.varimpshuffle2[j] = k;
            }
            for (i = 0; i <= nvars - 1; i++)
            {
                s.varimpshuffle2[npoints + i] = hqrnd.hqrnduniformi(varimprs, npoints, _params);
            }

            //
            // Prepare buffer object, seed pool
            //
            nperm = nvars + 2;
            permseed.losses = new double[nperm];
            for (j = 0; j <= nperm - 1; j++)
            {
                permseed.losses[j] = 0;
            }
            permseed.yv = new double[nperm * nclasses];
            permseed.xraw = new double[nvars];
            permseed.xdist = new double[nvars];
            permseed.xcur = new double[nvars];
            permseed.targety = new double[nclasses];
            permseed.startnodes = new int[nvars];
            permseed.y = new double[nclasses];
            smp.ae_shared_pool_set_seed(permpool, permseed);

            //
            // Recursively split subset and process (using parallel capabilities, if possible)
            //
            estimatepermutationimportances(s, df, ntrees, permpool, 0, npoints, _params);

            //
            // Merge results
            //
            losses = new double[nperm];
            for (j = 0; j <= nperm - 1; j++)
            {
                losses[j] = 1.0e-20;
            }
            smp.ae_shared_pool_first_recycled(permpool, ref permresult);
            while (permresult != null)
            {
                for (j = 0; j <= nperm - 1; j++)
                {
                    losses[j] = losses[j] + permresult.losses[j];
                }
                smp.ae_shared_pool_next_recycled(permpool, ref permresult);
            }

            //
            // Compute importances
            //
            nopermloss = losses[nvars + 1];
            totalpermloss = losses[nvars];
            for (i = 0; i <= nvars - 1; i++)
            {
                rep.varimportances[i] = 1 - nopermloss / totalpermloss - (1 - losses[i] / totalpermloss);
                rep.varimportances[i] = apserv.boundval(rep.varimportances[i], 0, 1, _params);
            }

            //
            // Compute topvars[] array
            //
            tmpr0 = new double[nvars];
            for (j = 0; j <= nvars - 1; j++)
            {
                tmpr0[j] = -rep.varimportances[j];
                rep.topvars[j] = j;
            }
            tsort.tagsortfasti(ref tmpr0, ref rep.topvars, ref tmpr1, ref tmpi0, nvars, _params);
            return;
        }
        ap.assert(false, "EstimateVariableImportance: unexpected importance type");
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_estimatevariableimportance(decisionforestbuilder s,
        int sessionseed,
        decisionforest df,
        int ntrees,
        dfreport rep, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    Estimates permutation variable importance ratings for a range of dataset
    points.

    Initial call to this function should span entire range of the dataset,
    [Idx0,Idx1)=[0,NPoints), because function performs initialization of some
    internal structures when called with these arguments.

      -- ALGLIB --
         Copyright 21.05.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void estimatepermutationimportances(decisionforestbuilder s,
        decisionforest df,
        int ntrees,
        smp.shared_pool permpool,
        int idx0,
        int idx1,
        xparams _params)
    {
        int npoints = 0;
        int nvars = 0;
        int nclasses = 0;
        int nperm = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        double v = 0;
        int treeroot = 0;
        int nodeoffs = 0;
        double prediction = 0;
        int varidx = 0;
        int oobcounts = 0;
        int srcidx = 0;
        dfpermimpbuf permimpbuf = null;

        npoints = s.npoints;
        nvars = s.nvars;
        nclasses = s.nclasses;
        ap.assert(df.forestformat == dfuncompressedv0, "EstimateVariableImportance: integrity check failed (ff)");
        ap.assert((idx0 >= 0 && idx0 <= idx1) && idx1 <= npoints, "EstimateVariableImportance: integrity check failed (idx)");
        ap.assert(ap.rows(s.iobmatrix) >= ntrees && ap.cols(s.iobmatrix) >= npoints, "EstimateVariableImportance: integrity check failed (IOB)");

        //
        // Perform parallelization if batch is too large
        //
        if (idx1 - idx0 > permutationimportancebatchsize)
        {
            if (_trypexec_estimatepermutationimportances(s, df, ntrees, permpool, idx0, idx1, _params))
            {
                return;
            }
            j = (idx1 - idx0) / 2;
            estimatepermutationimportances(s, df, ntrees, permpool, idx0, idx0 + j, _params);
            estimatepermutationimportances(s, df, ntrees, permpool, idx0 + j, idx1, _params);
            return;
        }

        //
        // Retrieve buffer object from pool
        //
        smp.ae_shared_pool_retrieve(permpool, ref permimpbuf);

        //
        // Process range of points [idx0,idx1)
        //
        nperm = nvars + 2;
        for (i = idx0; i <= idx1 - 1; i++)
        {
            ap.assert(s.dstype == 0, "EstimateVariableImportance: unexpected dataset type");
            for (j = 0; j <= nvars - 1; j++)
            {
                permimpbuf.xraw[j] = s.dsdata[j * npoints + i];
                srcidx = s.varimpshuffle2[(i + s.varimpshuffle2[npoints + j]) % npoints];
                permimpbuf.xdist[j] = s.dsdata[j * npoints + srcidx];
            }
            if (nclasses > 1)
            {
                for (j = 0; j <= nclasses - 1; j++)
                {
                    permimpbuf.targety[j] = 0;
                }
                permimpbuf.targety[s.dsival[i]] = 1;
            }
            else
            {
                permimpbuf.targety[0] = s.dsrval[i];
            }

            //
            // Process all trees, for each tree compute NPerm losses corresponding
            // to various permutations of variable values
            //
            for (j = 0; j <= nperm * nclasses - 1; j++)
            {
                permimpbuf.yv[j] = 0;
            }
            oobcounts = 0;
            treeroot = 0;
            for (k = 0; k <= ntrees - 1; k++)
            {
                if (!s.iobmatrix[k, i])
                {

                    //
                    // Process original (unperturbed) point and analyze path from the
                    // tree root to the final leaf. Output prediction to RawPrediction.
                    //
                    // Additionally, for each variable in [0,NVars-1] save offset of
                    // the first split on this variable. It allows us to quickly compute
                    // tree decision when perturbation does not change decision path.
                    //
                    ap.assert(df.forestformat == dfuncompressedv0, "EstimateVariableImportance: integrity check failed (ff)");
                    nodeoffs = treeroot + 1;
                    for (j = 0; j <= nvars - 1; j++)
                    {
                        permimpbuf.startnodes[j] = -1;
                    }
                    prediction = 0;
                    while (true)
                    {
                        if ((double)(df.trees[nodeoffs]) == (double)(-1))
                        {
                            prediction = df.trees[nodeoffs + 1];
                            break;
                        }
                        j = (int)Math.Round(df.trees[nodeoffs]);
                        if (permimpbuf.startnodes[j] < 0)
                        {
                            permimpbuf.startnodes[j] = nodeoffs;
                        }
                        if (permimpbuf.xraw[j] < df.trees[nodeoffs + 1])
                        {
                            nodeoffs = nodeoffs + innernodewidth;
                        }
                        else
                        {
                            nodeoffs = treeroot + (int)Math.Round(df.trees[nodeoffs + 2]);
                        }
                    }

                    //
                    // Save loss for unperturbed point
                    //
                    varidx = nvars + 1;
                    if (nclasses > 1)
                    {
                        j = (int)Math.Round(prediction);
                        permimpbuf.yv[varidx * nclasses + j] = permimpbuf.yv[varidx * nclasses + j] + 1;
                    }
                    else
                    {
                        permimpbuf.yv[varidx] = permimpbuf.yv[varidx] + prediction;
                    }

                    //
                    // Save loss for all variables being perturbed (XDist).
                    // This loss is used as a reference loss when we compute R-squared.
                    //
                    varidx = nvars;
                    for (j = 0; j <= nclasses - 1; j++)
                    {
                        permimpbuf.y[j] = 0;
                    }
                    dfprocessinternaluncompressed(df, treeroot, treeroot + 1, permimpbuf.xdist, ref permimpbuf.y, _params);
                    for (j = 0; j <= nclasses - 1; j++)
                    {
                        permimpbuf.yv[varidx * nclasses + j] = permimpbuf.yv[varidx * nclasses + j] + permimpbuf.y[j];
                    }

                    //
                    // Compute losses for variable #VarIdx being perturbed. Quite an often decision
                    // process does not actually depend on the variable #VarIdx (path from the tree
                    // root does not include splits on this variable). In such cases we perform
                    // quick exit from the loop with precomputed value.
                    //
                    for (j = 0; j <= nvars - 1; j++)
                    {
                        permimpbuf.xcur[j] = permimpbuf.xraw[j];
                    }
                    for (varidx = 0; varidx <= nvars - 1; varidx++)
                    {
                        if (permimpbuf.startnodes[varidx] >= 0)
                        {

                            //
                            // Path from tree root to the final leaf involves split on variable #VarIdx.
                            // Restart computation from the position first split on #VarIdx.
                            //
                            ap.assert(df.forestformat == dfuncompressedv0, "EstimateVariableImportance: integrity check failed (ff)");
                            permimpbuf.xcur[varidx] = permimpbuf.xdist[varidx];
                            nodeoffs = permimpbuf.startnodes[varidx];
                            while (true)
                            {
                                if ((double)(df.trees[nodeoffs]) == (double)(-1))
                                {
                                    if (nclasses > 1)
                                    {
                                        j = (int)Math.Round(df.trees[nodeoffs + 1]);
                                        permimpbuf.yv[varidx * nclasses + j] = permimpbuf.yv[varidx * nclasses + j] + 1;
                                    }
                                    else
                                    {
                                        permimpbuf.yv[varidx] = permimpbuf.yv[varidx] + df.trees[nodeoffs + 1];
                                    }
                                    break;
                                }
                                j = (int)Math.Round(df.trees[nodeoffs]);
                                if (permimpbuf.xcur[j] < df.trees[nodeoffs + 1])
                                {
                                    nodeoffs = nodeoffs + innernodewidth;
                                }
                                else
                                {
                                    nodeoffs = treeroot + (int)Math.Round(df.trees[nodeoffs + 2]);
                                }
                            }
                            permimpbuf.xcur[varidx] = permimpbuf.xraw[varidx];
                        }
                        else
                        {

                            //
                            // Path from tree root to the final leaf does NOT involve split on variable #VarIdx.
                            // Permutation does not change tree output, reuse already computed value.
                            //
                            if (nclasses > 1)
                            {
                                j = (int)Math.Round(prediction);
                                permimpbuf.yv[varidx * nclasses + j] = permimpbuf.yv[varidx * nclasses + j] + 1;
                            }
                            else
                            {
                                permimpbuf.yv[varidx] = permimpbuf.yv[varidx] + prediction;
                            }
                        }
                    }

                    //
                    // update OOB counter
                    //
                    apserv.inc(ref oobcounts, _params);
                }
                treeroot = treeroot + (int)Math.Round(df.trees[treeroot]);
            }

            //
            // Now YV[] stores NPerm versions of the forest output for various permutations of variable values.
            // Update losses.
            //
            for (j = 0; j <= nperm - 1; j++)
            {
                for (k = 0; k <= nclasses - 1; k++)
                {
                    permimpbuf.yv[j * nclasses + k] = permimpbuf.yv[j * nclasses + k] / apserv.coalesce(oobcounts, 1, _params);
                }
                v = 0;
                for (k = 0; k <= nclasses - 1; k++)
                {
                    v = v + math.sqr(permimpbuf.yv[j * nclasses + k] - permimpbuf.targety[k]);
                }
                permimpbuf.losses[j] = permimpbuf.losses[j] + v;
            }

            //
            // Update progress indicator
            //
            apserv.threadunsafeincby(ref s.rdfprogress, ntrees, _params);
        }

        //
        // Recycle buffer object with updated Losses[] field
        //
        smp.ae_shared_pool_recycle(permpool, ref permimpbuf);
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_estimatepermutationimportances(decisionforestbuilder s,
        decisionforest df,
        int ntrees,
        smp.shared_pool permpool,
        int idx0,
        int idx1, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    Sets report fields to their default values

      -- ALGLIB --
         Copyright 21.05.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void cleanreport(decisionforestbuilder s,
        dfreport rep,
        xparams _params)
    {
        int i = 0;

        rep.relclserror = 0;
        rep.avgce = 0;
        rep.rmserror = 0;
        rep.avgerror = 0;
        rep.avgrelerror = 0;
        rep.oobrelclserror = 0;
        rep.oobavgce = 0;
        rep.oobrmserror = 0;
        rep.oobavgerror = 0;
        rep.oobavgrelerror = 0;
        rep.topvars = new int[s.nvars];
        rep.varimportances = new double[s.nvars];
        for (i = 0; i <= s.nvars - 1; i++)
        {
            rep.topvars[i] = i;
            rep.varimportances[i] = 0;
        }
    }


    /*************************************************************************
    This function returns NRMS2 loss (sum of squared residuals) for a constant-
    output model:
    * model output is a mean over TRN set being passed (for classification
      problems - NClasses-dimensional vector of class probabilities)
    * model is evaluated over TST set being passed, with L2 loss being returned

    Input parameters:
        NClasses            -   ">1" for classification, "=1" for regression
        TrnLabelsI          -   training set labels, class indexes (for NClasses>1)
        TrnLabelsR          -   training set output values (for NClasses=1)
        TrnIdx0, TrnIdx1    -   a range [Idx0,Idx1) of elements in LabelsI/R is considered
        TstLabelsI          -   training set labels, class indexes (for NClasses>1)
        TstLabelsR          -   training set output values (for NClasses=1)
        TstIdx0, TstIdx1    -   a range [Idx0,Idx1) of elements in LabelsI/R is considered
        TmpI        -   temporary array, reallocated as needed
        
    Result:
        sum of squared residuals;
        for NClasses>=2 it coincides with Gini impurity times (Idx1-Idx0)

    Following fields of WorkBuf are used as temporaries:
    * TmpMeanNRMS2

      -- ALGLIB --
         Copyright 21.05.2018 by Bochkanov Sergey
    *************************************************************************/
    private static double meannrms2(int nclasses,
        int[] trnlabelsi,
        double[] trnlabelsr,
        int trnidx0,
        int trnidx1,
        int[] tstlabelsi,
        double[] tstlabelsr,
        int tstidx0,
        int tstidx1,
        ref int[] tmpi,
        xparams _params)
    {
        double result = 0;
        int i = 0;
        int k = 0;
        int ntrn = 0;
        int ntst = 0;
        double v = 0;
        double vv = 0;
        double invntrn = 0;
        double pitrn = 0;
        double nitst = 0;

        ap.assert(trnidx0 <= trnidx1, "MeanNRMS2: integrity check failed (8754)");
        ap.assert(tstidx0 <= tstidx1, "MeanNRMS2: integrity check failed (8754)");
        result = 0;
        ntrn = trnidx1 - trnidx0;
        ntst = tstidx1 - tstidx0;
        if (ntrn == 0 || ntst == 0)
        {
            return result;
        }
        invntrn = 1.0 / ntrn;
        if (nclasses > 1)
        {

            //
            // Classification problem
            //
            apserv.ivectorsetlengthatleast(ref tmpi, 2 * nclasses, _params);
            for (i = 0; i <= 2 * nclasses - 1; i++)
            {
                tmpi[i] = 0;
            }
            for (i = trnidx0; i <= trnidx1 - 1; i++)
            {
                k = trnlabelsi[i];
                tmpi[k] = tmpi[k] + 1;
            }
            for (i = tstidx0; i <= tstidx1 - 1; i++)
            {
                k = tstlabelsi[i];
                tmpi[k + nclasses] = tmpi[k + nclasses] + 1;
            }
            for (i = 0; i <= nclasses - 1; i++)
            {
                pitrn = tmpi[i] * invntrn;
                nitst = tmpi[i + nclasses];
                result = result + nitst * (1 - pitrn) * (1 - pitrn);
                result = result + (ntst - nitst) * pitrn * pitrn;
            }
        }
        else
        {

            //
            // regression-specific code
            //
            v = 0;
            for (i = trnidx0; i <= trnidx1 - 1; i++)
            {
                v = v + trnlabelsr[i];
            }
            v = v * invntrn;
            for (i = tstidx0; i <= tstidx1 - 1; i++)
            {
                vv = tstlabelsr[i] - v;
                result = result + vv * vv;
            }
        }
        return result;
    }


    /*************************************************************************
    This function is a part of the recurrent tree construction function; it
    selects variable for splitting according to current tree construction
    algorithm.

    Note: modifies VarsInPool, may decrease it if some variables become non-informative
    and leave the pool.

      -- ALGLIB --
         Copyright 21.05.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void choosecurrentsplitdense(decisionforestbuilder s,
        dfworkbuf workbuf,
        ref int varsinpool,
        int varstoselect,
        hqrnd.hqrndstate rs,
        int idx0,
        int idx1,
        ref int varbest,
        ref double splitbest,
        xparams _params)
    {
        int npoints = 0;
        double errbest = 0;
        int varstried = 0;
        int varcur = 0;
        bool valuesaresame = new bool();
        int offs = 0;
        double split = 0;
        int i = 0;
        double v = 0;
        double v0 = 0;
        double currms = 0;
        int info = 0;

        varbest = 0;
        splitbest = 0;

        ap.assert(s.dstype == 0, "sparsity is not supported 4terg!");
        ap.assert(s.rdfalgo == 0, "BuildRandomTreeRec: integrity check failed (1657)");
        ap.assert(idx0 < idx1, "BuildRandomTreeRec: integrity check failed (3445)");
        npoints = s.npoints;

        //
        // Select split according to dense direct RDF algorithm
        //
        varbest = -1;
        errbest = math.maxrealnumber;
        splitbest = 0;
        varstried = 0;
        while (varstried <= Math.Min(varstoselect, varsinpool) - 1)
        {

            //
            // select variables from pool
            //
            apserv.swapelementsi(workbuf.varpool, varstried, varstried + hqrnd.hqrnduniformi(rs, varsinpool - varstried, _params), _params);
            varcur = workbuf.varpool[varstried];

            //
            // Load variable values to working array.
            // If all variable values are same, variable is excluded from pool and we re-run variable selection.
            //
            valuesaresame = true;
            ap.assert(s.dstype == 0, "not supported segsv34fs");
            offs = npoints * varcur;
            v0 = s.dsdata[offs + workbuf.trnset[idx0]];
            for (i = idx0; i <= idx1 - 1; i++)
            {
                v = s.dsdata[offs + workbuf.trnset[i]];
                workbuf.curvals[i] = v;
                valuesaresame = valuesaresame && v == v0;
            }
            if (valuesaresame)
            {

                //
                // Variable does not change across current subset.
                // Exclude variable from pool, go to the next iteration.
                // VarsTried is not increased.
                //
                // NOTE: it is essential that updated VarsInPool is passed
                //       down to children but not up to caller - it is
                //       possible that one level higher this variable is
                //       not-fixed.
                //
                apserv.swapelementsi(workbuf.varpool, varstried, varsinpool - 1, _params);
                varsinpool = varsinpool - 1;
                continue;
            }

            //
            // Now we are ready to infer the split
            //
            evaluatedensesplit(s, workbuf, rs, varcur, idx0, idx1, ref info, ref split, ref currms, _params);
            if (info > 0 && (varbest < 0 || (double)(currms) <= (double)(errbest)))
            {
                errbest = currms;
                varbest = varcur;
                splitbest = split;
                for (i = idx0; i <= idx1 - 1; i++)
                {
                    workbuf.bestvals[i] = workbuf.curvals[i];
                }
            }

            //
            // Next iteration
            //
            varstried = varstried + 1;
        }
    }


    /*************************************************************************
    This function performs split on some specific dense variable whose values
    are stored in WorkBuf.CurVals[Idx0,Idx1) and labels are stored in
    WorkBuf.TrnLabelsR/I[Idx0,Idx1).

    It returns split value and associated RMS error. It is responsibility of
    the caller to make sure that variable has at least two distinct values,
    i.e. it is possible to make a split.

    Precomputed values of following fields of WorkBuf are used:
    * ClassPriors

    Following fields of WorkBuf are used as temporaries:
    * ClassTotals0,1,01
    * Tmp0I, Tmp1I, Tmp0R, Tmp1R, Tmp2R, Tmp3R

      -- ALGLIB --
         Copyright 21.05.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void evaluatedensesplit(decisionforestbuilder s,
        dfworkbuf workbuf,
        hqrnd.hqrndstate rs,
        int splitvar,
        int idx0,
        int idx1,
        ref int info,
        ref double split,
        ref double rms,
        xparams _params)
    {
        int nclasses = 0;
        int i = 0;
        int j = 0;
        int k0 = 0;
        int k1 = 0;
        double v = 0;
        double v0 = 0;
        double v1 = 0;
        double v2 = 0;
        int sl = 0;
        int sr = 0;

        info = 0;
        split = 0;
        rms = 0;

        ap.assert(idx0 < idx1, "BuildRandomTreeRec: integrity check failed (8754)");
        nclasses = s.nclasses;
        if (s.dsbinary[splitvar])
        {

            //
            // Try simple binary split, if possible
            // Split can be inferred from minimum/maximum values, just calculate RMS error
            //
            info = 1;
            split = getsplit(s, s.dsmin[splitvar], s.dsmax[splitvar], rs, _params);
            if (nclasses > 1)
            {

                //
                // Classification problem
                //
                for (j = 0; j <= nclasses - 1; j++)
                {
                    workbuf.classtotals0[j] = 0;
                }
                sl = 0;
                for (i = idx0; i <= idx1 - 1; i++)
                {
                    if (workbuf.curvals[i] < split)
                    {
                        j = workbuf.trnlabelsi[i];
                        workbuf.classtotals0[j] = workbuf.classtotals0[j] + 1;
                        sl = sl + 1;
                    }
                }
                sr = idx1 - idx0 - sl;
                ap.assert(sl != 0 && sr != 0, "BuildRandomTreeRec: something strange, impossible failure!");
                v0 = (double)1 / (double)sl;
                v1 = (double)1 / (double)sr;
                rms = 0;
                for (j = 0; j <= nclasses - 1; j++)
                {
                    k0 = workbuf.classtotals0[j];
                    k1 = workbuf.classpriors[j] - k0;
                    rms = rms + k0 * (1 - v0 * k0) + k1 * (1 - v1 * k1);
                }
                rms = Math.Sqrt(rms / (nclasses * (idx1 - idx0 + 1)));
            }
            else
            {

                //
                // regression-specific code
                //
                sl = 0;
                sr = 0;
                v1 = 0;
                v2 = 0;
                for (j = idx0; j <= idx1 - 1; j++)
                {
                    if (workbuf.curvals[j] < split)
                    {
                        v1 = v1 + workbuf.trnlabelsr[j];
                        sl = sl + 1;
                    }
                    else
                    {
                        v2 = v2 + workbuf.trnlabelsr[j];
                        sr = sr + 1;
                    }
                }
                ap.assert(sl != 0 && sr != 0, "BuildRandomTreeRec: something strange, impossible failure!");
                v1 = v1 / sl;
                v2 = v2 / sr;
                rms = 0;
                for (j = 0; j <= idx1 - idx0 - 1; j++)
                {
                    v = workbuf.trnlabelsr[idx0 + j];
                    if (workbuf.curvals[j] < split)
                    {
                        v = v - v1;
                    }
                    else
                    {
                        v = v - v2;
                    }
                    rms = rms + v * v;
                }
                rms = Math.Sqrt(rms / (idx1 - idx0 + 1));
            }
        }
        else
        {

            //
            // General split
            //
            info = 0;
            if (nclasses > 1)
            {
                for (i = 0; i <= idx1 - idx0 - 1; i++)
                {
                    workbuf.tmp0r[i] = workbuf.curvals[idx0 + i];
                    workbuf.tmp0i[i] = workbuf.trnlabelsi[idx0 + i];
                }
                classifiersplit(s, workbuf, ref workbuf.tmp0r, ref workbuf.tmp0i, idx1 - idx0, rs, ref info, ref split, ref rms, ref workbuf.tmp1r, ref workbuf.tmp1i, _params);
            }
            else
            {
                for (i = 0; i <= idx1 - idx0 - 1; i++)
                {
                    workbuf.tmp0r[i] = workbuf.curvals[idx0 + i];
                    workbuf.tmp1r[i] = workbuf.trnlabelsr[idx0 + i];
                }
                regressionsplit(s, workbuf, ref workbuf.tmp0r, ref workbuf.tmp1r, idx1 - idx0, ref info, ref split, ref rms, ref workbuf.tmp2r, ref workbuf.tmp3r, _params);
            }
        }
    }


    /*************************************************************************
    Classifier split
    *************************************************************************/
    private static void classifiersplit(decisionforestbuilder s,
        dfworkbuf workbuf,
        ref double[] x,
        ref int[] c,
        int n,
        hqrnd.hqrndstate rs,
        ref int info,
        ref double threshold,
        ref double e,
        ref double[] sortrbuf,
        ref int[] sortibuf,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        int n0 = 0;
        int n0prev = 0;
        double v = 0;
        int advanceby = 0;
        double rms = 0;
        int k0 = 0;
        int k1 = 0;
        double v0 = 0;
        double v1 = 0;
        int nclasses = 0;
        double vmin = 0;
        double vmax = 0;

        info = 0;
        threshold = 0;
        e = 0;

        ap.assert((s.rdfsplitstrength == 0 || s.rdfsplitstrength == 1) || s.rdfsplitstrength == 2, "RDF: unexpected split type at ClassifierSplit()");
        nclasses = s.nclasses;
        advanceby = 1;
        if (n >= 20)
        {
            advanceby = Math.Max(2, (int)Math.Round(n * 0.05));
        }
        info = -1;
        threshold = 0;
        e = math.maxrealnumber;

        //
        // Random split
        //
        if (s.rdfsplitstrength == 0)
        {

            //
            // Evaluate minimum, maximum and randomly selected values
            //
            vmin = x[0];
            vmax = x[0];
            for (i = 1; i <= n - 1; i++)
            {
                v = x[i];
                if (v < vmin)
                {
                    vmin = v;
                }
                if (v > vmax)
                {
                    vmax = v;
                }
            }
            if ((double)(vmin) == (double)(vmax))
            {
                return;
            }
            v = x[hqrnd.hqrnduniformi(rs, n, _params)];
            if ((double)(v) == (double)(vmin))
            {
                v = vmax;
            }

            //
            // Calculate RMS error associated with the split
            //
            for (i = 0; i <= nclasses - 1; i++)
            {
                workbuf.classtotals0[i] = 0;
            }
            n0 = 0;
            for (i = 0; i <= n - 1; i++)
            {
                if (x[i] < v)
                {
                    k = c[i];
                    workbuf.classtotals0[k] = workbuf.classtotals0[k] + 1;
                    n0 = n0 + 1;
                }
            }
            ap.assert(n0 > 0 && n0 < n, "RDF: critical integrity check failed at ClassifierSplit()");
            v0 = (double)1 / (double)n0;
            v1 = (double)1 / (double)(n - n0);
            rms = 0;
            for (j = 0; j <= nclasses - 1; j++)
            {
                k0 = workbuf.classtotals0[j];
                k1 = workbuf.classpriors[j] - k0;
                rms = rms + k0 * (1 - v0 * k0) + k1 * (1 - v1 * k1);
            }
            threshold = v;
            info = 1;
            e = rms;
            return;
        }

        //
        // Stronger splits which require us to sort the data
        // Quick check for degeneracy
        //
        tsort.tagsortfasti(ref x, ref c, ref sortrbuf, ref sortibuf, n, _params);
        v = 0.5 * (x[0] + x[n - 1]);
        if (!((double)(x[0]) < (double)(v) && (double)(v) < (double)(x[n - 1])))
        {
            return;
        }

        //
        // Split at the middle
        //
        if (s.rdfsplitstrength == 1)
        {

            //
            // Select split position
            //
            vmin = x[0];
            vmax = x[n - 1];
            v = x[n / 2];
            if ((double)(v) == (double)(vmin))
            {
                v = vmin + 0.001 * (vmax - vmin);
            }
            if ((double)(v) == (double)(vmin))
            {
                v = vmax;
            }

            //
            // Calculate RMS error associated with the split
            //
            for (i = 0; i <= nclasses - 1; i++)
            {
                workbuf.classtotals0[i] = 0;
            }
            n0 = 0;
            for (i = 0; i <= n - 1; i++)
            {
                if (x[i] < v)
                {
                    k = c[i];
                    workbuf.classtotals0[k] = workbuf.classtotals0[k] + 1;
                    n0 = n0 + 1;
                }
            }
            ap.assert(n0 > 0 && n0 < n, "RDF: critical integrity check failed at ClassifierSplit()");
            v0 = (double)1 / (double)n0;
            v1 = (double)1 / (double)(n - n0);
            rms = 0;
            for (j = 0; j <= nclasses - 1; j++)
            {
                k0 = workbuf.classtotals0[j];
                k1 = workbuf.classpriors[j] - k0;
                rms = rms + k0 * (1 - v0 * k0) + k1 * (1 - v1 * k1);
            }
            threshold = v;
            info = 1;
            e = rms;
            return;
        }

        //
        // Strong split
        //
        if (s.rdfsplitstrength == 2)
        {

            //
            // Prepare initial split.
            // Evaluate current split, prepare next one, repeat.
            //
            for (i = 0; i <= nclasses - 1; i++)
            {
                workbuf.classtotals0[i] = 0;
            }
            n0 = 1;
            while (n0 < n && x[n0] == x[n0 - 1])
            {
                n0 = n0 + 1;
            }
            ap.assert(n0 < n, "RDF: critical integrity check failed in ClassifierSplit()");
            for (i = 0; i <= n0 - 1; i++)
            {
                k = c[i];
                workbuf.classtotals0[k] = workbuf.classtotals0[k] + 1;
            }
            info = -1;
            threshold = x[n - 1];
            e = math.maxrealnumber;
            while (n0 < n)
            {

                //
                // RMS error associated with current split
                //
                v0 = (double)1 / (double)n0;
                v1 = (double)1 / (double)(n - n0);
                rms = 0;
                for (j = 0; j <= nclasses - 1; j++)
                {
                    k0 = workbuf.classtotals0[j];
                    k1 = workbuf.classpriors[j] - k0;
                    rms = rms + k0 * (1 - v0 * k0) + k1 * (1 - v1 * k1);
                }
                if (info < 0 || rms < e)
                {
                    info = 1;
                    e = rms;
                    threshold = 0.5 * (x[n0 - 1] + x[n0]);
                    if (threshold <= x[n0 - 1])
                    {
                        threshold = x[n0];
                    }
                }

                //
                // Advance
                //
                n0prev = n0;
                while (n0 < n && n0 - n0prev < advanceby)
                {
                    v = x[n0];
                    while (n0 < n && x[n0] == v)
                    {
                        k = c[n0];
                        workbuf.classtotals0[k] = workbuf.classtotals0[k] + 1;
                        n0 = n0 + 1;
                    }
                }
            }
            if (info > 0)
            {
                e = Math.Sqrt(e / (nclasses * n));
            }
            return;
        }
        ap.assert(false, "RDF: ClassifierSplit(), critical error");
    }


    /*************************************************************************
    Regression model split
    *************************************************************************/
    private static void regressionsplit(decisionforestbuilder s,
        dfworkbuf workbuf,
        ref double[] x,
        ref double[] y,
        int n,
        ref int info,
        ref double threshold,
        ref double e,
        ref double[] sortrbuf,
        ref double[] sortrbuf2,
        xparams _params)
    {
        int i = 0;
        double vmin = 0;
        double vmax = 0;
        double bnd01 = 0;
        double bnd12 = 0;
        double bnd23 = 0;
        int total0 = 0;
        int total1 = 0;
        int total2 = 0;
        int total3 = 0;
        int cnt0 = 0;
        int cnt1 = 0;
        int cnt2 = 0;
        int cnt3 = 0;
        int n0 = 0;
        int advanceby = 0;
        double v = 0;
        double v0 = 0;
        double v1 = 0;
        double rms = 0;
        int n0prev = 0;
        int k0 = 0;
        int k1 = 0;

        info = 0;
        threshold = 0;
        e = 0;

        advanceby = 1;
        if (n >= 20)
        {
            advanceby = Math.Max(2, (int)Math.Round(n * 0.05));
        }

        //
        // Sort data
        // Quick check for degeneracy
        //
        tsort.tagsortfastr(ref x, ref y, ref sortrbuf, ref sortrbuf2, n, _params);
        v = 0.5 * (x[0] + x[n - 1]);
        if (!((double)(x[0]) < (double)(v) && (double)(v) < (double)(x[n - 1])))
        {
            info = -1;
            threshold = x[n - 1];
            e = math.maxrealnumber;
            return;
        }

        //
        // Prepare initial split.
        // Evaluate current split, prepare next one, repeat.
        //
        vmin = y[0];
        vmax = y[0];
        for (i = 1; i <= n - 1; i++)
        {
            v = y[i];
            if (v < vmin)
            {
                vmin = v;
            }
            if (v > vmax)
            {
                vmax = v;
            }
        }
        bnd12 = 0.5 * (vmin + vmax);
        bnd01 = 0.5 * (vmin + bnd12);
        bnd23 = 0.5 * (vmax + bnd12);
        total0 = 0;
        total1 = 0;
        total2 = 0;
        total3 = 0;
        for (i = 0; i <= n - 1; i++)
        {
            v = y[i];
            if (v < bnd12)
            {
                if (v < bnd01)
                {
                    total0 = total0 + 1;
                }
                else
                {
                    total1 = total1 + 1;
                }
            }
            else
            {
                if (v < bnd23)
                {
                    total2 = total2 + 1;
                }
                else
                {
                    total3 = total3 + 1;
                }
            }
        }
        n0 = 1;
        while (n0 < n && x[n0] == x[n0 - 1])
        {
            n0 = n0 + 1;
        }
        ap.assert(n0 < n, "RDF: critical integrity check failed in ClassifierSplit()");
        cnt0 = 0;
        cnt1 = 0;
        cnt2 = 0;
        cnt3 = 0;
        for (i = 0; i <= n0 - 1; i++)
        {
            v = y[i];
            if (v < bnd12)
            {
                if (v < bnd01)
                {
                    cnt0 = cnt0 + 1;
                }
                else
                {
                    cnt1 = cnt1 + 1;
                }
            }
            else
            {
                if (v < bnd23)
                {
                    cnt2 = cnt2 + 1;
                }
                else
                {
                    cnt3 = cnt3 + 1;
                }
            }
        }
        info = -1;
        threshold = x[n - 1];
        e = math.maxrealnumber;
        while (n0 < n)
        {

            //
            // RMS error associated with current split
            //
            v0 = (double)1 / (double)n0;
            v1 = (double)1 / (double)(n - n0);
            rms = 0;
            k0 = cnt0;
            k1 = total0 - cnt0;
            rms = rms + k0 * (1 - v0 * k0) + k1 * (1 - v1 * k1);
            k0 = cnt1;
            k1 = total1 - cnt1;
            rms = rms + k0 * (1 - v0 * k0) + k1 * (1 - v1 * k1);
            k0 = cnt2;
            k1 = total2 - cnt2;
            rms = rms + k0 * (1 - v0 * k0) + k1 * (1 - v1 * k1);
            k0 = cnt3;
            k1 = total3 - cnt3;
            rms = rms + k0 * (1 - v0 * k0) + k1 * (1 - v1 * k1);
            if (info < 0 || rms < e)
            {
                info = 1;
                e = rms;
                threshold = 0.5 * (x[n0 - 1] + x[n0]);
                if (threshold <= x[n0 - 1])
                {
                    threshold = x[n0];
                }
            }

            //
            // Advance
            //
            n0prev = n0;
            while (n0 < n && n0 - n0prev < advanceby)
            {
                v0 = x[n0];
                while (n0 < n && x[n0] == v0)
                {
                    v = y[n0];
                    if (v < bnd12)
                    {
                        if (v < bnd01)
                        {
                            cnt0 = cnt0 + 1;
                        }
                        else
                        {
                            cnt1 = cnt1 + 1;
                        }
                    }
                    else
                    {
                        if (v < bnd23)
                        {
                            cnt2 = cnt2 + 1;
                        }
                        else
                        {
                            cnt3 = cnt3 + 1;
                        }
                    }
                    n0 = n0 + 1;
                }
            }
        }
        if (info > 0)
        {
            e = Math.Sqrt(e / (4 * n));
        }
    }


    /*************************************************************************
    Returns split: either deterministic split at the middle of [A,B], or randomly
    chosen split.

    It is guaranteed that A<Split<=B.

      -- ALGLIB --
         Copyright 21.05.2018 by Bochkanov Sergey
    *************************************************************************/
    private static double getsplit(decisionforestbuilder s,
        double a,
        double b,
        hqrnd.hqrndstate rs,
        xparams _params)
    {
        double result = 0;

        result = 0.5 * (a + b);
        if ((double)(result) <= (double)(a))
        {
            result = b;
        }
        return result;
    }


    /*************************************************************************
    Outputs leaf to the tree

    Following items of TRN and OOB sets are updated in the voting buffer:
    * items [Idx0,Idx1) of WorkBuf.TrnSet
    * items [OOBIdx0, OOBIdx1) of WorkBuf.OOBSet

      -- ALGLIB --
         Copyright 21.05.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void outputleaf(decisionforestbuilder s,
        dfworkbuf workbuf,
        double[] treebuf,
        dfvotebuf votebuf,
        int idx0,
        int idx1,
        int oobidx0,
        int oobidx1,
        ref int treesize,
        double leafval,
        xparams _params)
    {
        int leafvali = 0;
        int nclasses = 0;
        int i = 0;
        int j = 0;

        nclasses = s.nclasses;
        if (nclasses == 1)
        {

            //
            // Store split to the tree
            //
            treebuf[treesize] = -1;
            treebuf[treesize + 1] = leafval;

            //
            // Update training and OOB voting stats
            //
            for (i = idx0; i <= idx1 - 1; i++)
            {
                j = workbuf.trnset[i];
                votebuf.trntotals[j] = votebuf.trntotals[j] + leafval;
                votebuf.trncounts[j] = votebuf.trncounts[j] + 1;
            }
            for (i = oobidx0; i <= oobidx1 - 1; i++)
            {
                j = workbuf.oobset[i];
                votebuf.oobtotals[j] = votebuf.oobtotals[j] + leafval;
                votebuf.oobcounts[j] = votebuf.oobcounts[j] + 1;
            }
        }
        else
        {

            //
            // Store split to the tree
            //
            treebuf[treesize] = -1;
            treebuf[treesize + 1] = leafval;

            //
            // Update training and OOB voting stats
            //
            leafvali = (int)Math.Round(leafval);
            for (i = idx0; i <= idx1 - 1; i++)
            {
                j = workbuf.trnset[i];
                votebuf.trntotals[j * nclasses + leafvali] = votebuf.trntotals[j * nclasses + leafvali] + 1;
                votebuf.trncounts[j] = votebuf.trncounts[j] + 1;
            }
            for (i = oobidx0; i <= oobidx1 - 1; i++)
            {
                j = workbuf.oobset[i];
                votebuf.oobtotals[j * nclasses + leafvali] = votebuf.oobtotals[j * nclasses + leafvali] + 1;
                votebuf.oobcounts[j] = votebuf.oobcounts[j] + 1;
            }
        }
        treesize = treesize + leafnodewidth;
    }


    /*************************************************************************
    This function performs generic and algorithm-specific preprocessing of the
    dataset

      -- ALGLIB --
         Copyright 21.05.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void analyzeandpreprocessdataset(decisionforestbuilder s,
        xparams _params)
    {
        int nvars = 0;
        int nclasses = 0;
        int npoints = 0;
        int i = 0;
        int j = 0;
        bool isbinary = new bool();
        double v = 0;
        double v0 = 0;
        double v1 = 0;
        hqrnd.hqrndstate rs = new hqrnd.hqrndstate();

        ap.assert(s.dstype == 0, "no sparsity");
        npoints = s.npoints;
        nvars = s.nvars;
        nclasses = s.nclasses;

        //
        // seed local RNG
        //
        if (s.rdfglobalseed > 0)
        {
            hqrnd.hqrndseed(s.rdfglobalseed, 3532, rs, _params);
        }
        else
        {
            hqrnd.hqrndseed(math.randominteger(30000), 3532, rs, _params);
        }

        //
        // Generic processing
        //
        ap.assert(npoints >= 1, "BuildRandomForest: integrity check failed");
        apserv.rvectorsetlengthatleast(ref s.dsmin, nvars, _params);
        apserv.rvectorsetlengthatleast(ref s.dsmax, nvars, _params);
        apserv.bvectorsetlengthatleast(ref s.dsbinary, nvars, _params);
        for (i = 0; i <= nvars - 1; i++)
        {
            v0 = s.dsdata[i * npoints + 0];
            v1 = s.dsdata[i * npoints + 0];
            for (j = 1; j <= npoints - 1; j++)
            {
                v = s.dsdata[i * npoints + j];
                if (v < v0)
                {
                    v0 = v;
                }
                if (v > v1)
                {
                    v1 = v;
                }
            }
            s.dsmin[i] = v0;
            s.dsmax[i] = v1;
            ap.assert((double)(v0) <= (double)(v1), "BuildRandomForest: strange integrity check failure");
            isbinary = true;
            for (j = 0; j <= npoints - 1; j++)
            {
                v = s.dsdata[i * npoints + j];
                isbinary = isbinary && (v == v0 || v == v1);
            }
            s.dsbinary[i] = isbinary;
        }
        if (nclasses == 1)
        {
            s.dsravg = 0;
            for (i = 0; i <= npoints - 1; i++)
            {
                s.dsravg = s.dsravg + s.dsrval[i];
            }
            s.dsravg = s.dsravg / npoints;
        }
        else
        {
            apserv.ivectorsetlengthatleast(ref s.dsctotals, nclasses, _params);
            for (i = 0; i <= nclasses - 1; i++)
            {
                s.dsctotals[i] = 0;
            }
            for (i = 0; i <= npoints - 1; i++)
            {
                s.dsctotals[s.dsival[i]] = s.dsctotals[s.dsival[i]] + 1;
            }
        }
    }


    /*************************************************************************
    This function merges together trees generated during training and outputs
    it to the decision forest.

    INPUT PARAMETERS:
        S           -   decision forest builder object
        NTrees      -   NTrees>=1, number of trees to train

    OUTPUT PARAMETERS:
        DF          -   decision forest
        Rep         -   report

      -- ALGLIB --
         Copyright 21.05.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void mergetrees(decisionforestbuilder s,
        decisionforest df,
        xparams _params)
    {
        int i = 0;
        int cursize = 0;
        int offs = 0;
        dftreebuf tree = null;
        int[] treesizes = new int[0];
        int[] treeoffsets = new int[0];

        df.forestformat = dfuncompressedv0;
        df.nvars = s.nvars;
        df.nclasses = s.nclasses;
        df.bufsize = 0;
        df.ntrees = 0;

        //
        // Determine trees count
        //
        smp.ae_shared_pool_first_recycled(s.treepool, ref tree);
        while (tree != null)
        {
            df.ntrees = df.ntrees + 1;
            smp.ae_shared_pool_next_recycled(s.treepool, ref tree);
        }
        ap.assert(df.ntrees > 0, "MergeTrees: integrity check failed, zero trees count");

        //
        // Determine individual tree sizes and total buffer size
        //
        treesizes = new int[df.ntrees];
        for (i = 0; i <= df.ntrees - 1; i++)
        {
            treesizes[i] = -1;
        }
        smp.ae_shared_pool_first_recycled(s.treepool, ref tree);
        while (tree != null)
        {
            ap.assert(tree.treeidx >= 0 && tree.treeidx < df.ntrees, "MergeTrees: integrity check failed (wrong TreeIdx)");
            ap.assert(treesizes[tree.treeidx] < 0, "MergeTrees: integrity check failed (duplicate TreeIdx)");
            df.bufsize = df.bufsize + (int)Math.Round(tree.treebuf[0]);
            treesizes[tree.treeidx] = (int)Math.Round(tree.treebuf[0]);
            smp.ae_shared_pool_next_recycled(s.treepool, ref tree);
        }
        for (i = 0; i <= df.ntrees - 1; i++)
        {
            ap.assert(treesizes[i] > 0, "MergeTrees: integrity check failed (wrong TreeSize)");
        }

        //
        // Determine offsets for individual trees in output buffer
        //
        treeoffsets = new int[df.ntrees];
        treeoffsets[0] = 0;
        for (i = 1; i <= df.ntrees - 1; i++)
        {
            treeoffsets[i] = treeoffsets[i - 1] + treesizes[i - 1];
        }

        //
        // Output trees
        //
        // NOTE: since ALGLIB 3.16.0 trees are sorted by tree index prior to
        //       output (necessary for variable importance estimation), that's
        //       why we need array of tree offsets
        //
        df.trees = new double[df.bufsize];
        smp.ae_shared_pool_first_recycled(s.treepool, ref tree);
        while (tree != null)
        {
            cursize = (int)Math.Round(tree.treebuf[0]);
            offs = treeoffsets[tree.treeidx];
            for (i = 0; i <= cursize - 1; i++)
            {
                df.trees[offs + i] = tree.treebuf[i];
            }
            smp.ae_shared_pool_next_recycled(s.treepool, ref tree);
        }
    }


    /*************************************************************************
    This function post-processes voting array and calculates TRN and OOB errors.

    INPUT PARAMETERS:
        S           -   decision forest builder object
        NTrees      -   number of trees in the forest
        Buf         -   possibly preallocated vote buffer, its contents is
                        overwritten by this function

    OUTPUT PARAMETERS:
        Rep         -   report fields corresponding to errors are updated

      -- ALGLIB --
         Copyright 21.05.2018 by Bochkanov Sergey
    *************************************************************************/
    private static void processvotingresults(decisionforestbuilder s,
        int ntrees,
        dfvotebuf buf,
        dfreport rep,
        xparams _params)
    {
        dfvotebuf vote = null;
        int nvars = 0;
        int nclasses = 0;
        int npoints = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        int k1 = 0;
        double v = 0;
        int avgrelcnt = 0;
        int oobavgrelcnt = 0;

        npoints = s.npoints;
        nvars = s.nvars;
        nclasses = s.nclasses;
        ap.assert(npoints > 0, "DFOREST: integrity check failed");
        ap.assert(nvars > 0, "DFOREST: integrity check failed");
        ap.assert(nclasses > 0, "DFOREST: integrity check failed");

        //
        // Prepare vote buffer
        //
        apserv.rvectorsetlengthatleast(ref buf.trntotals, npoints * nclasses, _params);
        apserv.rvectorsetlengthatleast(ref buf.oobtotals, npoints * nclasses, _params);
        for (i = 0; i <= npoints * nclasses - 1; i++)
        {
            buf.trntotals[i] = 0;
            buf.oobtotals[i] = 0;
        }
        apserv.ivectorsetlengthatleast(ref buf.trncounts, npoints, _params);
        apserv.ivectorsetlengthatleast(ref buf.oobcounts, npoints, _params);
        for (i = 0; i <= npoints - 1; i++)
        {
            buf.trncounts[i] = 0;
            buf.oobcounts[i] = 0;
        }

        //
        // Merge voting arrays
        //
        smp.ae_shared_pool_first_recycled(s.votepool, ref vote);
        while (vote != null)
        {
            for (i = 0; i <= npoints * nclasses - 1; i++)
            {
                buf.trntotals[i] = buf.trntotals[i] + vote.trntotals[i] + vote.oobtotals[i];
                buf.oobtotals[i] = buf.oobtotals[i] + vote.oobtotals[i];
            }
            for (i = 0; i <= npoints - 1; i++)
            {
                buf.trncounts[i] = buf.trncounts[i] + vote.trncounts[i] + vote.oobcounts[i];
                buf.oobcounts[i] = buf.oobcounts[i] + vote.oobcounts[i];
            }
            smp.ae_shared_pool_next_recycled(s.votepool, ref vote);
        }
        for (i = 0; i <= npoints - 1; i++)
        {
            v = 1 / apserv.coalesce(buf.trncounts[i], 1, _params);
            for (j = 0; j <= nclasses - 1; j++)
            {
                buf.trntotals[i * nclasses + j] = buf.trntotals[i * nclasses + j] * v;
            }
            v = 1 / apserv.coalesce(buf.oobcounts[i], 1, _params);
            for (j = 0; j <= nclasses - 1; j++)
            {
                buf.oobtotals[i * nclasses + j] = buf.oobtotals[i * nclasses + j] * v;
            }
        }

        //
        // Use aggregated voting data to output error metrics
        //
        avgrelcnt = 0;
        oobavgrelcnt = 0;
        rep.rmserror = 0;
        rep.avgerror = 0;
        rep.avgrelerror = 0;
        rep.relclserror = 0;
        rep.avgce = 0;
        rep.oobrmserror = 0;
        rep.oobavgerror = 0;
        rep.oobavgrelerror = 0;
        rep.oobrelclserror = 0;
        rep.oobavgce = 0;
        for (i = 0; i <= npoints - 1; i++)
        {
            if (nclasses > 1)
            {

                //
                // classification-specific code
                //
                k = s.dsival[i];
                for (j = 0; j <= nclasses - 1; j++)
                {
                    v = buf.trntotals[i * nclasses + j];
                    if (j == k)
                    {
                        rep.avgce = rep.avgce - Math.Log(apserv.coalesce(v, math.minrealnumber, _params));
                        rep.rmserror = rep.rmserror + math.sqr(v - 1);
                        rep.avgerror = rep.avgerror + Math.Abs(v - 1);
                        rep.avgrelerror = rep.avgrelerror + Math.Abs(v - 1);
                        apserv.inc(ref avgrelcnt, _params);
                    }
                    else
                    {
                        rep.rmserror = rep.rmserror + math.sqr(v);
                        rep.avgerror = rep.avgerror + Math.Abs(v);
                    }
                    v = buf.oobtotals[i * nclasses + j];
                    if (j == k)
                    {
                        rep.oobavgce = rep.oobavgce - Math.Log(apserv.coalesce(v, math.minrealnumber, _params));
                        rep.oobrmserror = rep.oobrmserror + math.sqr(v - 1);
                        rep.oobavgerror = rep.oobavgerror + Math.Abs(v - 1);
                        rep.oobavgrelerror = rep.oobavgrelerror + Math.Abs(v - 1);
                        apserv.inc(ref oobavgrelcnt, _params);
                    }
                    else
                    {
                        rep.oobrmserror = rep.oobrmserror + math.sqr(v);
                        rep.oobavgerror = rep.oobavgerror + Math.Abs(v);
                    }
                }

                //
                // Classification errors are handled separately
                //
                k1 = 0;
                for (j = 1; j <= nclasses - 1; j++)
                {
                    if (buf.trntotals[i * nclasses + j] > buf.trntotals[i * nclasses + k1])
                    {
                        k1 = j;
                    }
                }
                if (k1 != k)
                {
                    rep.relclserror = rep.relclserror + 1;
                }
                k1 = 0;
                for (j = 1; j <= nclasses - 1; j++)
                {
                    if (buf.oobtotals[i * nclasses + j] > buf.oobtotals[i * nclasses + k1])
                    {
                        k1 = j;
                    }
                }
                if (k1 != k)
                {
                    rep.oobrelclserror = rep.oobrelclserror + 1;
                }
            }
            else
            {

                //
                // regression-specific code
                //
                v = buf.trntotals[i] - s.dsrval[i];
                rep.rmserror = rep.rmserror + math.sqr(v);
                rep.avgerror = rep.avgerror + Math.Abs(v);
                if ((double)(s.dsrval[i]) != (double)(0))
                {
                    rep.avgrelerror = rep.avgrelerror + Math.Abs(v / s.dsrval[i]);
                    avgrelcnt = avgrelcnt + 1;
                }
                v = buf.oobtotals[i] - s.dsrval[i];
                rep.oobrmserror = rep.oobrmserror + math.sqr(v);
                rep.oobavgerror = rep.oobavgerror + Math.Abs(v);
                if ((double)(s.dsrval[i]) != (double)(0))
                {
                    rep.oobavgrelerror = rep.oobavgrelerror + Math.Abs(v / s.dsrval[i]);
                    oobavgrelcnt = oobavgrelcnt + 1;
                }
            }
        }
        rep.relclserror = rep.relclserror / npoints;
        rep.rmserror = Math.Sqrt(rep.rmserror / (npoints * nclasses));
        rep.avgerror = rep.avgerror / (npoints * nclasses);
        rep.avgrelerror = rep.avgrelerror / apserv.coalesce(avgrelcnt, 1, _params);
        rep.oobrelclserror = rep.oobrelclserror / npoints;
        rep.oobrmserror = Math.Sqrt(rep.oobrmserror / (npoints * nclasses));
        rep.oobavgerror = rep.oobavgerror / (npoints * nclasses);
        rep.oobavgrelerror = rep.oobavgrelerror / apserv.coalesce(oobavgrelcnt, 1, _params);
    }


    /*************************************************************************
    This function performs binary compression of decision forest, using either
    8-bit mantissa (a bit more compact representation) or 16-bit mantissa  for
    splits and regression outputs.

    Forest is compressed in-place.

    Return value is a compression factor.

      -- ALGLIB --
         Copyright 22.07.2019 by Bochkanov Sergey
    *************************************************************************/
    private static double binarycompression(decisionforest df,
        bool usemantissa8,
        xparams _params)
    {
        double result = 0;
        int size8 = 0;
        int size8i = 0;
        int offssrc = 0;
        int offsdst = 0;
        int i = 0;
        int[] dummyi = new int[0];
        int maxrawtreesize = 0;
        int[] compressedsizes = new int[0];


        //
        // Quick exit if already compressed
        //
        if (df.forestformat == dfcompressedv0)
        {
            result = 1;
            return result;
        }

        //
        // Check that source format is supported
        //
        ap.assert(df.forestformat == dfuncompressedv0, "BinaryCompression: unexpected forest format");

        //
        // Compute sizes of uncompressed and compressed trees.
        //
        size8 = 0;
        offssrc = 0;
        maxrawtreesize = 0;
        for (i = 0; i <= df.ntrees - 1; i++)
        {
            size8i = computecompressedsizerec(df, usemantissa8, offssrc, offssrc + 1, dummyi, false, _params);
            size8 = size8 + computecompresseduintsize(size8i, _params) + size8i;
            maxrawtreesize = Math.Max(maxrawtreesize, (int)Math.Round(df.trees[offssrc]));
            offssrc = offssrc + (int)Math.Round(df.trees[offssrc]);
        }
        result = (double)(8 * ap.len(df.trees)) / (double)(size8 + 1);

        //
        // Allocate memory and perform compression
        //
        df.trees8 = new byte[size8];
        compressedsizes = new int[maxrawtreesize];
        offssrc = 0;
        offsdst = 0;
        for (i = 0; i <= df.ntrees - 1; i++)
        {

            //
            // Call compressed size evaluator one more time, now saving subtree sizes into temporary array
            //
            size8i = computecompressedsizerec(df, usemantissa8, offssrc, offssrc + 1, compressedsizes, true, _params);

            //
            // Output tree header (length in bytes)
            //
            streamuint(df.trees8, ref offsdst, size8i, _params);

            //
            // Compress recursively
            //
            compressrec(df, usemantissa8, offssrc, offssrc + 1, compressedsizes, df.trees8, ref offsdst, _params);

            //
            // Next tree
            //
            offssrc = offssrc + (int)Math.Round(df.trees[offssrc]);
        }
        ap.assert(offsdst == size8, "BinaryCompression: integrity check failed (stream length)");

        //
        // Finalize forest conversion, clear previously allocated memory
        //
        df.forestformat = dfcompressedv0;
        df.usemantissa8 = usemantissa8;
        df.trees = new double[0];
        return result;
    }


    /*************************************************************************
    This function returns exact number of bytes required to  store  compressed
    version of the tree starting at location TreeBase.

    PARAMETERS:
        DF              -   decision forest
        UseMantissa8    -   whether 8-bit or 16-bit mantissas are used to store
                            floating point numbers
        TreeRoot        -   root of the specific tree being stored (offset in DF.Trees)
        TreePos         -   position within tree (first location in the tree
                            is TreeRoot+1)
        CompressedSizes -   not referenced if SaveCompressedSizes is False;
                            otherwise, values computed by this function for
                            specific values of TreePos are stored to
                            CompressedSizes[TreePos-TreeRoot] (other elements
                            of the array are not referenced).
                            This array must be preallocated by caller.

      -- ALGLIB --
         Copyright 22.07.2019 by Bochkanov Sergey
    *************************************************************************/
    private static int computecompressedsizerec(decisionforest df,
        bool usemantissa8,
        int treeroot,
        int treepos,
        int[] compressedsizes,
        bool savecompressedsizes,
        xparams _params)
    {
        int result = 0;
        int jmponbranch = 0;
        int child0size = 0;
        int child1size = 0;
        int fpwidth = 0;

        if (usemantissa8)
        {
            fpwidth = 2;
        }
        else
        {
            fpwidth = 3;
        }

        //
        // Leaf or split?
        //
        if ((double)(df.trees[treepos]) == (double)(-1))
        {

            //
            // Leaf
            //
            result = computecompresseduintsize(2 * df.nvars, _params);
            if (df.nclasses == 1)
            {
                result = result + fpwidth;
            }
            else
            {
                result = result + computecompresseduintsize((int)Math.Round(df.trees[treepos + 1]), _params);
            }
        }
        else
        {

            //
            // Split
            //
            jmponbranch = (int)Math.Round(df.trees[treepos + 2]);
            child0size = computecompressedsizerec(df, usemantissa8, treeroot, treepos + innernodewidth, compressedsizes, savecompressedsizes, _params);
            child1size = computecompressedsizerec(df, usemantissa8, treeroot, treeroot + jmponbranch, compressedsizes, savecompressedsizes, _params);
            if (child0size <= child1size)
            {

                //
                // Child #0 comes first because it is shorter
                //
                result = computecompresseduintsize((int)Math.Round(df.trees[treepos]), _params);
                result = result + fpwidth;
                result = result + computecompresseduintsize(child0size, _params);
            }
            else
            {

                //
                // Child #1 comes first because it is shorter
                //
                result = computecompresseduintsize((int)Math.Round(df.trees[treepos]) + df.nvars, _params);
                result = result + fpwidth;
                result = result + computecompresseduintsize(child1size, _params);
            }
            result = result + child0size + child1size;
        }

        //
        // Do we have to save compressed sizes?
        //
        if (savecompressedsizes)
        {
            ap.assert(treepos - treeroot < ap.len(compressedsizes), "ComputeCompressedSizeRec: integrity check failed");
            compressedsizes[treepos - treeroot] = result;
        }
        return result;
    }


    /*************************************************************************
    This function returns exact number of bytes required to  store  compressed
    version of the tree starting at location TreeBase.

    PARAMETERS:
        DF              -   decision forest
        UseMantissa8    -   whether 8-bit or 16-bit mantissas are used to store
                            floating point numbers
        TreeRoot        -   root of the specific tree being stored (offset in DF.Trees)
        TreePos         -   position within tree (first location in the tree
                            is TreeRoot+1)
        CompressedSizes -   not referenced if SaveCompressedSizes is False;
                            otherwise, values computed by this function for
                            specific values of TreePos are stored to
                            CompressedSizes[TreePos-TreeRoot] (other elements
                            of the array are not referenced).
                            This array must be preallocated by caller.

      -- ALGLIB --
         Copyright 22.07.2019 by Bochkanov Sergey
    *************************************************************************/
    private static void compressrec(decisionforest df,
        bool usemantissa8,
        int treeroot,
        int treepos,
        int[] compressedsizes,
        byte[] buf,
        ref int dstoffs,
        xparams _params)
    {
        int jmponbranch = 0;
        int child0size = 0;
        int child1size = 0;
        int varidx = 0;
        double leafval = 0;
        double splitval = 0;
        int dstoffsold = 0;

        dstoffsold = dstoffs;

        //
        // Leaf or split?
        //
        varidx = (int)Math.Round(df.trees[treepos]);
        if (varidx == -1)
        {

            //
            // Leaf node:
            // * stream special value which denotes leaf (2*NVars)
            // * then, stream scalar value (floating point) or class number (unsigned integer)
            //
            leafval = df.trees[treepos + 1];
            streamuint(buf, ref dstoffs, 2 * df.nvars, _params);
            if (df.nclasses == 1)
            {
                streamfloat(buf, usemantissa8, ref dstoffs, leafval, _params);
            }
            else
            {
                streamuint(buf, ref dstoffs, (int)Math.Round(leafval), _params);
            }
        }
        else
        {

            //
            // Split node:
            // * fetch compressed sizes of child nodes, decide which child goes first
            //
            jmponbranch = (int)Math.Round(df.trees[treepos + 2]);
            splitval = df.trees[treepos + 1];
            child0size = compressedsizes[treepos + innernodewidth - treeroot];
            child1size = compressedsizes[treeroot + jmponbranch - treeroot];
            if (child0size <= child1size)
            {

                //
                // Child #0 comes first because it is shorter:
                // * stream variable index used for splitting;
                //   value in [0,NVars) range indicates that split is
                //   "if VAR<VAL then BRANCH0 else BRANCH1"
                // * stream value used for splitting
                // * stream children #0 and #1
                //
                streamuint(buf, ref dstoffs, varidx, _params);
                streamfloat(buf, usemantissa8, ref dstoffs, splitval, _params);
                streamuint(buf, ref dstoffs, child0size, _params);
                compressrec(df, usemantissa8, treeroot, treepos + innernodewidth, compressedsizes, buf, ref dstoffs, _params);
                compressrec(df, usemantissa8, treeroot, treeroot + jmponbranch, compressedsizes, buf, ref dstoffs, _params);
            }
            else
            {

                //
                // Child #1 comes first because it is shorter:
                // * stream variable index used for splitting + NVars;
                //   value in [NVars,2*NVars) range indicates that split is
                //   "if VAR>=VAL then BRANCH0 else BRANCH1"
                // * stream value used for splitting
                // * stream children #0 and #1
                //
                streamuint(buf, ref dstoffs, varidx + df.nvars, _params);
                streamfloat(buf, usemantissa8, ref dstoffs, splitval, _params);
                streamuint(buf, ref dstoffs, child1size, _params);
                compressrec(df, usemantissa8, treeroot, treeroot + jmponbranch, compressedsizes, buf, ref dstoffs, _params);
                compressrec(df, usemantissa8, treeroot, treepos + innernodewidth, compressedsizes, buf, ref dstoffs, _params);
            }
        }

        //
        // Integrity check at the end
        //
        ap.assert(dstoffs - dstoffsold == compressedsizes[treepos - treeroot], "CompressRec: integrity check failed (compressed size at leaf)");
    }


    /*************************************************************************
    This function returns exact number of bytes required to  store  compressed
    unsigned integer number (negative  arguments  result  in  assertion  being
    generated).

      -- ALGLIB --
         Copyright 22.07.2019 by Bochkanov Sergey
    *************************************************************************/
    private static int computecompresseduintsize(int v,
        xparams _params)
    {
        int result = 0;

        ap.assert(v >= 0);
        result = 1;
        while (v >= 128)
        {
            v = v / 128;
            result = result + 1;
        }
        return result;
    }


    /*************************************************************************
    This function stores compressed unsigned integer number (negative arguments
    result in assertion being generated) to byte array at  location  Offs  and
    increments Offs by number of bytes being stored.

      -- ALGLIB --
         Copyright 22.07.2019 by Bochkanov Sergey
    *************************************************************************/
    private static void streamuint(byte[] buf,
        ref int offs,
        int v,
        xparams _params)
    {
        int v0 = 0;

        ap.assert(v >= 0);
        while (true)
        {

            //
            // Save 7 least significant bits of V, use 8th bit as a flag which
            // tells us whether subsequent 7-bit packages will be sent.
            //
            v0 = v % 128;
            if (v >= 128)
            {
                v0 = v0 + 128;
            }
            buf[offs] = unchecked((byte)(v0));
            offs = offs + 1;
            v = v / 128;
            if (v == 0)
            {
                break;
            }
        }
    }


    /*************************************************************************
    This function reads compressed unsigned integer number from byte array
    starting at location Offs and increments Offs by number of bytes being
    read.

      -- ALGLIB --
         Copyright 22.07.2019 by Bochkanov Sergey
    *************************************************************************/
    private static int unstreamuint(byte[] buf,
        ref int offs,
        xparams _params)
    {
        int result = 0;
        int v0 = 0;
        int p = 0;

        result = 0;
        p = 1;
        while (true)
        {

            //
            // Rad 7 bits of V, use 8th bit as a flag which tells us whether
            // subsequent 7-bit packages will be received.
            //
            v0 = buf[offs];
            offs = offs + 1;
            result = result + v0 % 128 * p;
            if (v0 < 128)
            {
                break;
            }
            p = p * 128;
        }
        return result;
    }


    /*************************************************************************
    This function stores compressed floating point number  to  byte  array  at
    location  Offs and increments Offs by number of bytes being stored.

    Either 8-bit mantissa or 16-bit mantissa is used. The exponent  is  always
    7 bits of exponent + sign. Values which do not fit into exponent range are
    truncated to fit.

      -- ALGLIB --
         Copyright 22.07.2019 by Bochkanov Sergey
    *************************************************************************/
    private static void streamfloat(byte[] buf,
        bool usemantissa8,
        ref int offs,
        double v,
        xparams _params)
    {
        int signbit = 0;
        int e = 0;
        int m = 0;
        double twopow30 = 0;
        double twopowm30 = 0;
        double twopow10 = 0;
        double twopowm10 = 0;

        ap.assert(math.isfinite(v), "StreamFloat: V is not finite number");

        //
        // Special case: zero
        //
        if (v == 0.0)
        {
            if (usemantissa8)
            {
                buf[offs + 0] = unchecked((byte)(0));
                buf[offs + 1] = unchecked((byte)(0));
                offs = offs + 2;
            }
            else
            {
                buf[offs + 0] = unchecked((byte)(0));
                buf[offs + 1] = unchecked((byte)(0));
                buf[offs + 2] = unchecked((byte)(0));
                offs = offs + 3;
            }
            return;
        }

        //
        // Handle sign
        //
        signbit = 0;
        if (v < 0.0)
        {
            v = -v;
            signbit = 128;
        }

        //
        // Compute exponent
        //
        twopow30 = 1073741824;
        twopow10 = 1024;
        twopowm30 = 1.0 / twopow30;
        twopowm10 = 1.0 / twopow10;
        e = 0;
        while (v >= twopow30)
        {
            v = v * twopowm30;
            e = e + 30;
        }
        while (v >= twopow10)
        {
            v = v * twopowm10;
            e = e + 10;
        }
        while (v >= 1.0)
        {
            v = v * 0.5;
            e = e + 1;
        }
        while (v < twopowm30)
        {
            v = v * twopow30;
            e = e - 30;
        }
        while (v < twopowm10)
        {
            v = v * twopow10;
            e = e - 10;
        }
        while (v < 0.5)
        {
            v = v * 2;
            e = e - 1;
        }
        ap.assert(v >= 0.5 && v < 1.0, "StreamFloat: integrity check failed");

        //
        // Handle exponent underflow/overflow
        //
        if (e < -63)
        {
            signbit = 0;
            e = 0;
            v = 0;
        }
        if (e > 63)
        {
            e = 63;
            v = 1.0;
        }

        //
        // Save to stream
        //
        if (usemantissa8)
        {
            m = (int)Math.Round(v * 256);
            if (m == 256)
            {
                m = m / 2;
                e = Math.Min(e + 1, 63);
            }
            buf[offs + 0] = unchecked((byte)(e + 64 + signbit));
            buf[offs + 1] = unchecked((byte)(m));
            offs = offs + 2;
        }
        else
        {
            m = (int)Math.Round(v * 65536);
            if (m == 65536)
            {
                m = m / 2;
                e = Math.Min(e + 1, 63);
            }
            buf[offs + 0] = unchecked((byte)(e + 64 + signbit));
            buf[offs + 1] = unchecked((byte)(m % 256));
            buf[offs + 2] = unchecked((byte)(m / 256));
            offs = offs + 3;
        }
    }


    /*************************************************************************
    This function reads compressed floating point number from the byte array
    starting from location Offs and increments Offs by number of bytes being
    read.

    Either 8-bit mantissa or 16-bit mantissa is used. The exponent  is  always
    7 bits of exponent + sign. Values which do not fit into exponent range are
    truncated to fit.

      -- ALGLIB --
         Copyright 22.07.2019 by Bochkanov Sergey
    *************************************************************************/
    private static double unstreamfloat(byte[] buf,
        bool usemantissa8,
        ref int offs,
        xparams _params)
    {
        double result = 0;
        int e = 0;
        double v = 0;
        double inv256 = 0;


        //
        // Read from stream
        //
        inv256 = 1.0 / 256.0;
        if (usemantissa8)
        {
            e = buf[offs + 0];
            v = buf[offs + 1] * inv256;
            offs = offs + 2;
        }
        else
        {
            e = buf[offs + 0];
            v = (buf[offs + 1] * inv256 + buf[offs + 2]) * inv256;
            offs = offs + 3;
        }

        //
        // Decode
        //
        if (e > 128)
        {
            v = -v;
            e = e - 128;
        }
        e = e - 64;
        result = xfastpow(2, e, _params) * v;
        return result;
    }


    /*************************************************************************
    Classification error
    *************************************************************************/
    private static int dfclserror(decisionforest df,
        double[,] xy,
        int npoints,
        xparams _params)
    {
        int result = 0;
        double[] x = new double[0];
        double[] y = new double[0];
        int i = 0;
        int j = 0;
        int k = 0;
        int tmpi = 0;
        int i_ = 0;

        if (df.nclasses <= 1)
        {
            result = 0;
            return result;
        }
        x = new double[df.nvars - 1 + 1];
        y = new double[df.nclasses - 1 + 1];
        result = 0;
        for (i = 0; i <= npoints - 1; i++)
        {
            for (i_ = 0; i_ <= df.nvars - 1; i_++)
            {
                x[i_] = xy[i, i_];
            }
            dfprocess(df, x, ref y, _params);
            k = (int)Math.Round(xy[i, df.nvars]);
            tmpi = 0;
            for (j = 1; j <= df.nclasses - 1; j++)
            {
                if ((double)(y[j]) > (double)(y[tmpi]))
                {
                    tmpi = j;
                }
            }
            if (tmpi != k)
            {
                result = result + 1;
            }
        }
        return result;
    }


    /*************************************************************************
    Internal subroutine for processing one decision tree stored in uncompressed
    format starting at SubtreeRoot (this index points to the header of the tree,
    not its first node). First node being processed is located at NodeOffs.
    *************************************************************************/
    private static void dfprocessinternaluncompressed(decisionforest df,
        int subtreeroot,
        int nodeoffs,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        int idx = 0;

        ap.assert(df.forestformat == dfuncompressedv0, "DFProcessInternal: unexpected forest format");

        //
        // Navigate through the tree
        //
        while (true)
        {
            if ((double)(df.trees[nodeoffs]) == (double)(-1))
            {
                if (df.nclasses == 1)
                {
                    y[0] = y[0] + df.trees[nodeoffs + 1];
                }
                else
                {
                    idx = (int)Math.Round(df.trees[nodeoffs + 1]);
                    y[idx] = y[idx] + 1;
                }
                break;
            }
            if (x[(int)Math.Round(df.trees[nodeoffs])] < df.trees[nodeoffs + 1])
            {
                nodeoffs = nodeoffs + innernodewidth;
            }
            else
            {
                nodeoffs = subtreeroot + (int)Math.Round(df.trees[nodeoffs + 2]);
            }
        }
    }


    /*************************************************************************
    Internal subroutine for processing one decision tree stored in compressed
    format starting at Offs (this index points to the first node of the tree,
    right past the header field).
    *************************************************************************/
    private static void dfprocessinternalcompressed(decisionforest df,
        int offs,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        int leafindicator = 0;
        int varidx = 0;
        double splitval = 0;
        int jmplen = 0;
        double leafval = 0;
        int leafcls = 0;

        ap.assert(df.forestformat == dfcompressedv0, "DFProcessInternal: unexpected forest format");

        //
        // Navigate through the tree
        //
        leafindicator = 2 * df.nvars;
        while (true)
        {

            //
            // Read variable idx
            //
            varidx = unstreamuint(df.trees8, ref offs, _params);

            //
            // Is it leaf?
            //
            if (varidx == leafindicator)
            {
                if (df.nclasses == 1)
                {

                    //
                    // Regression forest
                    //
                    leafval = unstreamfloat(df.trees8, df.usemantissa8, ref offs, _params);
                    y[0] = y[0] + leafval;
                }
                else
                {

                    //
                    // Classification forest
                    //
                    leafcls = unstreamuint(df.trees8, ref offs, _params);
                    y[leafcls] = y[leafcls] + 1;
                }
                break;
            }

            //
            // Process node
            //
            splitval = unstreamfloat(df.trees8, df.usemantissa8, ref offs, _params);
            jmplen = unstreamuint(df.trees8, ref offs, _params);
            if (varidx < df.nvars)
            {

                //
                // The split rule is "if VAR<VAL then BRANCH0 else BRANCH1"
                //
                if (x[varidx] >= splitval)
                {
                    offs = offs + jmplen;
                }
            }
            else
            {

                //
                // The split rule is "if VAR>=VAL then BRANCH0 else BRANCH1"
                //
                varidx = varidx - df.nvars;
                if (x[varidx] < splitval)
                {
                    offs = offs + jmplen;
                }
            }
        }
    }


    /*************************************************************************
    Fast Pow

      -- ALGLIB --
         Copyright 24.08.2009 by Bochkanov Sergey
    *************************************************************************/
    private static double xfastpow(double r,
        int n,
        xparams _params)
    {
        double result = 0;

        result = 0;
        if (n > 0)
        {
            if (n % 2 == 0)
            {
                result = xfastpow(r, n / 2, _params);
                result = result * result;
            }
            else
            {
                result = r * xfastpow(r, n - 1, _params);
            }
            return result;
        }
        if (n == 0)
        {
            result = 1;
        }
        if (n < 0)
        {
            result = xfastpow(1 / r, -n, _params);
        }
        return result;
    }


}
