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

public class ssa
{
    /*************************************************************************
    This object stores state of the SSA model.

    You should use ALGLIB functions to work with this object.
    *************************************************************************/
    public class ssamodel : apobject
    {
        public int nsequences;
        public int[] sequenceidx;
        public double[] sequencedata;
        public int algotype;
        public int windowwidth;
        public int rtpowerup;
        public int topk;
        public int precomputedwidth;
        public int precomputednbasis;
        public double[,] precomputedbasis;
        public int defaultsubspaceits;
        public int memorylimit;
        public bool arebasisandsolvervalid;
        public double[,] basis;
        public double[,] basist;
        public double[] sv;
        public double[] forecasta;
        public int nbasis;
        public evd.eigsubspacestate solver;
        public double[,] xxt;
        public hqrnd.hqrndstate rs;
        public int rngseed;
        public int[] rtqueue;
        public int rtqueuecnt;
        public int rtqueuechunk;
        public int dbgcntevd;
        public double[] tmp0;
        public double[] tmp1;
        public evd.eigsubspacereport solverrep;
        public double[] alongtrend;
        public double[] alongnoise;
        public double[,] aseqtrajectory;
        public double[,] aseqtbproduct;
        public int[] aseqcounts;
        public double[] fctrend;
        public double[] fcnoise;
        public double[,] fctrendm;
        public double[,] uxbatch;
        public int uxbatchwidth;
        public int uxbatchsize;
        public int uxbatchlimit;
        public ssamodel()
        {
            init();
        }
        public override void init()
        {
            sequenceidx = new int[0];
            sequencedata = new double[0];
            precomputedbasis = new double[0, 0];
            basis = new double[0, 0];
            basist = new double[0, 0];
            sv = new double[0];
            forecasta = new double[0];
            solver = new evd.eigsubspacestate();
            xxt = new double[0, 0];
            rs = new hqrnd.hqrndstate();
            rtqueue = new int[0];
            tmp0 = new double[0];
            tmp1 = new double[0];
            solverrep = new evd.eigsubspacereport();
            alongtrend = new double[0];
            alongnoise = new double[0];
            aseqtrajectory = new double[0, 0];
            aseqtbproduct = new double[0, 0];
            aseqcounts = new int[0];
            fctrend = new double[0];
            fcnoise = new double[0];
            fctrendm = new double[0, 0];
            uxbatch = new double[0, 0];
        }
        public override apobject make_copy()
        {
            ssamodel _result = new ssamodel();
            _result.nsequences = nsequences;
            _result.sequenceidx = (int[])sequenceidx.Clone();
            _result.sequencedata = (double[])sequencedata.Clone();
            _result.algotype = algotype;
            _result.windowwidth = windowwidth;
            _result.rtpowerup = rtpowerup;
            _result.topk = topk;
            _result.precomputedwidth = precomputedwidth;
            _result.precomputednbasis = precomputednbasis;
            _result.precomputedbasis = (double[,])precomputedbasis.Clone();
            _result.defaultsubspaceits = defaultsubspaceits;
            _result.memorylimit = memorylimit;
            _result.arebasisandsolvervalid = arebasisandsolvervalid;
            _result.basis = (double[,])basis.Clone();
            _result.basist = (double[,])basist.Clone();
            _result.sv = (double[])sv.Clone();
            _result.forecasta = (double[])forecasta.Clone();
            _result.nbasis = nbasis;
            _result.solver = (evd.eigsubspacestate)solver.make_copy();
            _result.xxt = (double[,])xxt.Clone();
            _result.rs = (hqrnd.hqrndstate)rs.make_copy();
            _result.rngseed = rngseed;
            _result.rtqueue = (int[])rtqueue.Clone();
            _result.rtqueuecnt = rtqueuecnt;
            _result.rtqueuechunk = rtqueuechunk;
            _result.dbgcntevd = dbgcntevd;
            _result.tmp0 = (double[])tmp0.Clone();
            _result.tmp1 = (double[])tmp1.Clone();
            _result.solverrep = (evd.eigsubspacereport)solverrep.make_copy();
            _result.alongtrend = (double[])alongtrend.Clone();
            _result.alongnoise = (double[])alongnoise.Clone();
            _result.aseqtrajectory = (double[,])aseqtrajectory.Clone();
            _result.aseqtbproduct = (double[,])aseqtbproduct.Clone();
            _result.aseqcounts = (int[])aseqcounts.Clone();
            _result.fctrend = (double[])fctrend.Clone();
            _result.fcnoise = (double[])fcnoise.Clone();
            _result.fctrendm = (double[,])fctrendm.Clone();
            _result.uxbatch = (double[,])uxbatch.Clone();
            _result.uxbatchwidth = uxbatchwidth;
            _result.uxbatchsize = uxbatchsize;
            _result.uxbatchlimit = uxbatchlimit;
            return _result;
        }
    };




    /*************************************************************************
    This function creates SSA model object.  Right after creation model is  in
    "dummy" mode - you can add data,  but   analyzing/prediction  will  return
    just zeros (it assumes that basis is empty).

    HOW TO USE SSA MODEL:

    1. create model with ssacreate()
    2. add data with one/many ssaaddsequence() calls
    3. choose SSA algorithm with one of ssasetalgo...() functions:
       * ssasetalgotopkdirect() for direct one-run analysis
       * ssasetalgotopkrealtime() for algorithm optimized for many  subsequent
         runs with warm-start capabilities
       * ssasetalgoprecomputed() for user-supplied basis
    4. set window width with ssasetwindow()
    5. perform one of the analysis-related activities:
       a) call ssagetbasis() to get basis
       b) call ssaanalyzelast() ssaanalyzesequence() or ssaanalyzelastwindow()
          to perform analysis (trend/noise separation)
       c) call  one  of   the   forecasting   functions  (ssaforecastlast() or
          ssaforecastsequence()) to perform prediction; alternatively, you can
          extract linear recurrence coefficients with ssagetlrr().
       SSA analysis will be performed during first  call  to  analysis-related
       function. SSA model is smart enough to track all changes in the dataset
       and  model  settings,  to  cache  previously  computed  basis  and   to
       re-evaluate basis only when necessary.
          
    Additionally, if your setting involves constant stream  of  incoming data,
    you can perform quick update already calculated  model  with  one  of  the
    incremental   append-and-update   functions:  ssaappendpointandupdate() or
    ssaappendsequenceandupdate().

    NOTE: steps (2), (3), (4) can be performed in arbitrary order.
       
    INPUT PARAMETERS:
        none

    OUTPUT PARAMETERS:
        S               -   structure which stores model state

      -- ALGLIB --
         Copyright 30.10.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void ssacreate(ssamodel s,
        xparams _params)
    {

        //
        // Model data, algorithms and settings
        //
        s.nsequences = 0;
        s.sequenceidx = new int[1];
        s.sequenceidx[0] = 0;
        s.algotype = 0;
        s.windowwidth = 1;
        s.rtpowerup = 1;
        s.arebasisandsolvervalid = false;
        s.rngseed = 1;
        s.defaultsubspaceits = 10;
        s.memorylimit = 50000000;

        //
        // Debug counters
        //
        s.dbgcntevd = 0;
    }


    /*************************************************************************
    This function sets window width for SSA model. You should call  it  before
    analysis phase. Default window width is 1 (not for real use).

    Special notes:
    * this function call can be performed at any moment before  first call  to
      analysis-related functions
    * changing window width invalidates internally stored basis; if you change
      window width AFTER you call analysis-related  function,  next  analysis
      phase will require re-calculation of  the  basis  according  to  current
      algorithm.
    * calling this function with exactly  same window width as current one has
      no effect
    * if you specify window width larger  than any data sequence stored in the
      model, analysis will return zero basis.
       
    INPUT PARAMETERS:
        S               -   SSA model created with ssacreate()
        WindowWidth     -   >=1, new window width

    OUTPUT PARAMETERS:
        S               -   SSA model, updated

      -- ALGLIB --
         Copyright 30.10.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void ssasetwindow(ssamodel s,
        int windowwidth,
        xparams _params)
    {
        ap.assert(windowwidth >= 1, "SSASetWindow: WindowWidth<1");
        if (windowwidth == s.windowwidth)
        {
            return;
        }
        s.windowwidth = windowwidth;
        s.arebasisandsolvervalid = false;
    }


    /*************************************************************************
    This  function  sets  seed  which  is used to initialize internal RNG when
    we make pseudorandom decisions on model updates.

    By default, deterministic seed is used - which results in same sequence of
    pseudorandom decisions every time you run SSA model. If you  specify  non-
    deterministic seed value, then SSA  model  may  return  slightly different
    results after each run.

    This function can be useful when you have several SSA models updated  with
    sseappendpointandupdate() called with 0<UpdateIts<1 (fractional value) and
    due to performance limitations want them to perform updates  at  different
    moments.

    INPUT PARAMETERS:
        S       -   SSA model
        Seed    -   seed:
                    * positive values = use deterministic seed for each run of
                      algorithms which depend on random initialization
                    * zero or negative values = use non-deterministic seed

      -- ALGLIB --
         Copyright 03.11.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void ssasetseed(ssamodel s,
        int seed,
        xparams _params)
    {
        s.rngseed = seed;
    }


    /*************************************************************************
    This function sets length of power-up cycle for real-time algorithm.

    By default, this algorithm performs costly O(N*WindowWidth^2)  init  phase
    followed by full run of truncated  EVD.  However,  if  you  are  ready  to
    live with a bit lower-quality basis during first few iterations,  you  can
    split this O(N*WindowWidth^2) initialization  between  several  subsequent
    append-and-update rounds. It results in better latency of the algorithm.

    This function invalidates basis/solver, next analysis call will result  in
    full recalculation of everything.

    INPUT PARAMETERS:
        S       -   SSA model
        PWLen   -   length of the power-up stage:
                    * 0 means that no power-up is requested
                    * 1 is the same as 0
                    * >1 means that delayed power-up is performed

      -- ALGLIB --
         Copyright 03.11.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void ssasetpoweruplength(ssamodel s,
        int pwlen,
        xparams _params)
    {
        ap.assert(pwlen >= 0, "SSASetPowerUpLength: PWLen<0");
        s.rtpowerup = Math.Max(pwlen, 1);
        s.arebasisandsolvervalid = false;
    }


    /*************************************************************************
    This function sets memory limit of SSA analysis.

    Straightforward SSA with sequence length T and window width W needs O(T*W)
    memory. It is possible to reduce memory consumption by splitting task into
    smaller chunks.

    Thus function allows you to specify approximate memory limit (measured  in
    double precision numbers used for buffers). Actual memory consumption will
    be comparable to the number specified by you.

    Default memory limit is 50.000.000 (400Mbytes) in current version.

    INPUT PARAMETERS:
        S       -   SSA model
        MemLimit-   memory limit, >=0. Zero value means no limit.

      -- ALGLIB --
         Copyright 20.12.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void ssasetmemorylimit(ssamodel s,
        int memlimit,
        xparams _params)
    {
        if (memlimit < 0)
        {
            memlimit = 0;
        }
        s.memorylimit = memlimit;
    }


    /*************************************************************************
    This function adds data sequence to SSA  model.  Only   single-dimensional
    sequences are supported.

    What is a sequences? Following definitions/requirements apply:
    * a sequence  is  an  array of  values  measured  in  subsequent,  equally
      separated time moments (ticks).
    * you may have many sequences  in your  dataset;  say,  one  sequence  may
      correspond to one trading session.
    * sequence length should be larger  than current  window  length  (shorter
      sequences will be ignored during analysis).
    * analysis is performed within a  sequence; different  sequences  are  NOT
      stacked together to produce one large contiguous stream of data.
    * analysis is performed for all  sequences at once, i.e. same set of basis
      vectors is computed for all sequences
      
    INCREMENTAL ANALYSIS
      
    This function is non intended for  incremental updates of previously found
    SSA basis. Calling it invalidates  all previous analysis results (basis is
    reset and will be recalculated from zero during next analysis).

    If  you  want  to  perform   incremental/real-time  SSA,  consider   using
    following functions:
    * ssaappendpointandupdate() for appending one point
    * ssaappendsequenceandupdate() for appending new sequence
       
    INPUT PARAMETERS:
        S               -   SSA model created with ssacreate()
        X               -   array[N], data, can be larger (additional values
                            are ignored)
        N               -   data length, can be automatically determined from
                            the array length. N>=0.

    OUTPUT PARAMETERS:
        S               -   SSA model, updated
        
    NOTE: you can clear dataset with ssacleardata()

      -- ALGLIB --
         Copyright 30.10.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void ssaaddsequence(ssamodel s,
        double[] x,
        int n,
        xparams _params)
    {
        int i = 0;
        int offs = 0;

        ap.assert(n >= 0, "SSAAddSequence: N<0");
        ap.assert(ap.len(x) >= n, "SSAAddSequence: X is too short");
        ap.assert(apserv.isfinitevector(x, n, _params), "SSAAddSequence: X contains infinities NANs");

        //
        // Invalidate model
        //
        s.arebasisandsolvervalid = false;

        //
        // Add sequence
        //
        apserv.ivectorgrowto(ref s.sequenceidx, s.nsequences + 2, _params);
        s.sequenceidx[s.nsequences + 1] = s.sequenceidx[s.nsequences] + n;
        apserv.rvectorgrowto(ref s.sequencedata, s.sequenceidx[s.nsequences + 1], _params);
        offs = s.sequenceidx[s.nsequences];
        for (i = 0; i <= n - 1; i++)
        {
            s.sequencedata[offs + i] = x[i];
        }
        apserv.inc(ref s.nsequences, _params);
    }


    /*************************************************************************
    This function appends single point to last data sequence stored in the SSA
    model and tries to update model in the  incremental  manner  (if  possible
    with current algorithm).

    If you want to add more than one point at once:
    * if you want to add M points to the same sequence, perform M-1 calls with
      UpdateIts parameter set to 0.0, and last call with non-zero UpdateIts.
    * if you want to add new sequence, use ssaappendsequenceandupdate()

    Running time of this function does NOT depend on  dataset  size,  only  on
    window width and number of singular vectors. Depending on algorithm  being
    used, incremental update has complexity:
    * for top-K real time   - O(UpdateIts*K*Width^2), with fractional UpdateIts
    * for top-K direct      - O(Width^3) for any non-zero UpdateIts
    * for precomputed basis - O(1), no update is performed
       
    INPUT PARAMETERS:
        S               -   SSA model created with ssacreate()
        X               -   new point
        UpdateIts       -   >=0,  floating  point (!)  value,  desired  update
                            frequency:
                            * zero value means that point is  stored,  but  no
                              update is performed
                            * integer part of the value means  that  specified
                              number of iterations is always performed
                            * fractional part of  the  value  means  that  one
                              iteration is performed with this probability.
                              
                            Recommended value: 0<UpdateIts<=1.  Values  larger
                            than 1 are VERY seldom  needed.  If  your  dataset
                            changes slowly, you can set it  to  0.1  and  skip
                            90% of updates.
                            
                            In any case, no information is lost even with zero
                            value of UpdateIts! It will be  incorporated  into
                            model, sooner or later.

    OUTPUT PARAMETERS:
        S               -   SSA model, updated

    NOTE: this function uses internal  RNG  to  handle  fractional  values  of
          UpdateIts. By default it  is  initialized  with  fixed  seed  during
          initial calculation of basis. Thus subsequent calls to this function
          will result in the same sequence of pseudorandom decisions.
          
          However, if  you  have  several  SSA  models  which  are  calculated
          simultaneously, and if you want to reduce computational  bottlenecks
          by performing random updates at random moments, then fixed  seed  is
          not an option - all updates will fire at same moments.
          
          You may change it with ssasetseed() function.
          
    NOTE: this function throws an exception if called for empty dataset (there
          is no "last" sequence to modify).

      -- ALGLIB --
         Copyright 30.10.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void ssaappendpointandupdate(ssamodel s,
        double x,
        double updateits,
        xparams _params)
    {
        ap.assert(math.isfinite(x), "SSAAppendPointAndUpdate: X is not finite");
        ap.assert(math.isfinite(updateits), "SSAAppendPointAndUpdate: UpdateIts is not finite");
        ap.assert((double)(updateits) >= (double)(0), "SSAAppendPointAndUpdate: UpdateIts<0");
        ap.assert(s.nsequences > 0, "SSAAppendPointAndUpdate: dataset is empty, no sequence to modify");

        //
        // Append point to dataset
        //
        apserv.rvectorgrowto(ref s.sequencedata, s.sequenceidx[s.nsequences] + 1, _params);
        s.sequencedata[s.sequenceidx[s.nsequences]] = x;
        s.sequenceidx[s.nsequences] = s.sequenceidx[s.nsequences] + 1;

        //
        // Do we have something to analyze? If no, invalidate basis
        // (just to be sure) and exit.
        //
        if (!hassomethingtoanalyze(s, _params))
        {
            s.arebasisandsolvervalid = false;
            return;
        }

        //
        // Well, we have data to analyze and algorithm set, but basis is
        // invalid. Let's calculate it from scratch and exit.
        //
        if (!s.arebasisandsolvervalid)
        {
            updatebasis(s, 0, 0.0, _params);
            return;
        }

        //
        // Update already computed basis
        //
        updatebasis(s, 1, updateits, _params);
    }


    /*************************************************************************
    This function appends new sequence to dataset stored in the SSA  model and
    tries to update model in the incremental manner (if possible  with current
    algorithm).

    Notes:
    * if you want to add M sequences at once, perform M-1 calls with UpdateIts
      parameter set to 0.0, and last call with non-zero UpdateIts.
    * if you want to add just one point, use ssaappendpointandupdate()

    Running time of this function does NOT depend on  dataset  size,  only  on
    sequence length, window width and number of singular vectors. Depending on
    algorithm being used, incremental update has complexity:
    * for top-K real time   - O(UpdateIts*K*Width^2+(NTicks-Width)*Width^2)
    * for top-K direct      - O(Width^3+(NTicks-Width)*Width^2)
    * for precomputed basis - O(1), no update is performed
       
    INPUT PARAMETERS:
        S               -   SSA model created with ssacreate()
        X               -   new sequence, array[NTicks] or larget
        NTicks          -   >=1, number of ticks in the sequence
        UpdateIts       -   >=0,  floating  point (!)  value,  desired  update
                            frequency:
                            * zero value means that point is  stored,  but  no
                              update is performed
                            * integer part of the value means  that  specified
                              number of iterations is always performed
                            * fractional part of  the  value  means  that  one
                              iteration is performed with this probability.
                              
                            Recommended value: 0<UpdateIts<=1.  Values  larger
                            than 1 are VERY seldom  needed.  If  your  dataset
                            changes slowly, you can set it  to  0.1  and  skip
                            90% of updates.
                            
                            In any case, no information is lost even with zero
                            value of UpdateIts! It will be  incorporated  into
                            model, sooner or later.

    OUTPUT PARAMETERS:
        S               -   SSA model, updated

    NOTE: this function uses internal  RNG  to  handle  fractional  values  of
          UpdateIts. By default it  is  initialized  with  fixed  seed  during
          initial calculation of basis. Thus subsequent calls to this function
          will result in the same sequence of pseudorandom decisions.
          
          However, if  you  have  several  SSA  models  which  are  calculated
          simultaneously, and if you want to reduce computational  bottlenecks
          by performing random updates at random moments, then fixed  seed  is
          not an option - all updates will fire at same moments.
          
          You may change it with ssasetseed() function.

      -- ALGLIB --
         Copyright 30.10.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void ssaappendsequenceandupdate(ssamodel s,
        double[] x,
        int nticks,
        double updateits,
        xparams _params)
    {
        int i = 0;
        int offs = 0;

        ap.assert(nticks >= 0, "SSAAppendSequenceAndUpdate: NTicks<0");
        ap.assert(ap.len(x) >= nticks, "SSAAppendSequenceAndUpdate: X is too short");
        ap.assert(apserv.isfinitevector(x, nticks, _params), "SSAAppendSequenceAndUpdate: X contains infinities NANs");

        //
        // Add sequence
        //
        apserv.ivectorgrowto(ref s.sequenceidx, s.nsequences + 2, _params);
        s.sequenceidx[s.nsequences + 1] = s.sequenceidx[s.nsequences] + nticks;
        apserv.rvectorgrowto(ref s.sequencedata, s.sequenceidx[s.nsequences + 1], _params);
        offs = s.sequenceidx[s.nsequences];
        for (i = 0; i <= nticks - 1; i++)
        {
            s.sequencedata[offs + i] = x[i];
        }
        apserv.inc(ref s.nsequences, _params);

        //
        // Do we have something to analyze? If no, invalidate basis
        // (just to be sure) and exit.
        //
        if (!hassomethingtoanalyze(s, _params))
        {
            s.arebasisandsolvervalid = false;
            return;
        }

        //
        // Well, we have data to analyze and algorithm set, but basis is
        // invalid. Let's calculate it from scratch and exit.
        //
        if (!s.arebasisandsolvervalid)
        {
            updatebasis(s, 0, 0.0, _params);
            return;
        }

        //
        // Update already computed basis
        //
        if (nticks >= s.windowwidth)
        {
            updatebasis(s, nticks - s.windowwidth + 1, updateits, _params);
        }
    }


    /*************************************************************************
    This  function sets SSA algorithm to "precomputed vectors" algorithm.

    This  algorithm  uses  precomputed  set  of  orthonormal  (orthogonal  AND
    normalized) basis vectors supplied by user. Thus, basis calculation  phase
    is not performed -  we  already  have  our  basis  -  and  only  analysis/
    forecasting phase requires actual calculations.
          
    This algorithm may handle "append" requests which add just  one/few  ticks
    to the end of the last sequence in O(1) time.

    NOTE: this algorithm accepts both basis and window  width,  because  these
          two parameters are naturally aligned.  Calling  this  function  sets
          window width; if you call ssasetwindow() with  other  window  width,
          then during analysis stage algorithm will detect conflict and  reset
          to zero basis.

    INPUT PARAMETERS:
        S               -   SSA model
        A               -   array[WindowWidth,NBasis], orthonormalized  basis;
                            this function does NOT control  orthogonality  and
                            does NOT perform any kind of  renormalization.  It
                            is your responsibility to provide it with  correct
                            basis.
        WindowWidth     -   window width, >=1
        NBasis          -   number of basis vectors, 1<=NBasis<=WindowWidth

    OUTPUT PARAMETERS:
        S               -   updated model

    NOTE: calling this function invalidates basis in all cases.

      -- ALGLIB --
         Copyright 30.10.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void ssasetalgoprecomputed(ssamodel s,
        double[,] a,
        int windowwidth,
        int nbasis,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        ap.assert(windowwidth >= 1, "SSASetAlgoPrecomputed: WindowWidth<1");
        ap.assert(nbasis >= 1, "SSASetAlgoPrecomputed: NBasis<1");
        ap.assert(nbasis <= windowwidth, "SSASetAlgoPrecomputed: NBasis>WindowWidth");
        ap.assert(ap.rows(a) >= windowwidth, "SSASetAlgoPrecomputed: Rows(A)<WindowWidth");
        ap.assert(ap.cols(a) >= nbasis, "SSASetAlgoPrecomputed: Rows(A)<NBasis");
        ap.assert(apserv.apservisfinitematrix(a, windowwidth, nbasis, _params), "SSASetAlgoPrecomputed: Rows(A)<NBasis");
        s.algotype = 1;
        s.precomputedwidth = windowwidth;
        s.precomputednbasis = nbasis;
        s.windowwidth = windowwidth;
        apserv.rmatrixsetlengthatleast(ref s.precomputedbasis, windowwidth, nbasis, _params);
        for (i = 0; i <= windowwidth - 1; i++)
        {
            for (j = 0; j <= nbasis - 1; j++)
            {
                s.precomputedbasis[i, j] = a[i, j];
            }
        }
        s.arebasisandsolvervalid = false;
    }


    /*************************************************************************
    This  function sets SSA algorithm to "direct top-K" algorithm.

    "Direct top-K" algorithm performs full  SVD  of  the  N*WINDOW  trajectory
    matrix (hence its name - direct solver  is  used),  then  extracts  top  K
    components. Overall running time is O(N*WINDOW^2), where N is a number  of
    ticks in the dataset, WINDOW is window width.

    This algorithm may handle "append" requests which add just  one/few  ticks
    to the end of the last sequence in O(WINDOW^3) time,  which  is  ~N/WINDOW
    times faster than re-computing everything from scratch.

    INPUT PARAMETERS:
        S               -   SSA model
        TopK            -   number of components to analyze; TopK>=1.

    OUTPUT PARAMETERS:
        S               -   updated model


    NOTE: TopK>WindowWidth is silently decreased to WindowWidth during analysis
          phase
          
    NOTE: calling this function invalidates basis, except  for  the  situation
          when this algorithm was already set with same parameters.

      -- ALGLIB --
         Copyright 30.10.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void ssasetalgotopkdirect(ssamodel s,
        int topk,
        xparams _params)
    {
        ap.assert(topk >= 1, "SSASetAlgoTopKDirect: TopK<1");

        //
        // Ignore calls which change nothing
        //
        if (s.algotype == 2 && s.topk == topk)
        {
            return;
        }

        //
        // Update settings, invalidate model
        //
        s.algotype = 2;
        s.topk = topk;
        s.arebasisandsolvervalid = false;
    }


    /*************************************************************************
    This function sets SSA algorithm to "top-K real time algorithm". This algo
    extracts K components with largest singular values.

    It  is  real-time  version  of  top-K  algorithm  which  is  optimized for
    incremental processing and  fast  start-up. Internally  it  uses  subspace
    eigensolver for truncated SVD. It results  in  ability  to  perform  quick
    updates of the basis when only a few points/sequences is added to dataset.

    Performance profile of the algorithm is given below:
    * O(K*WindowWidth^2) running time for incremental update  of  the  dataset
      with one of the "append-and-update" functions (ssaappendpointandupdate()
      or ssaappendsequenceandupdate()).
    * O(N*WindowWidth^2) running time for initial basis evaluation (N=size  of
      dataset)
    * ability  to  split  costly  initialization  across  several  incremental
      updates of the basis (so called "Power-Up" functionality,  activated  by
      ssasetpoweruplength() function)
      
    INPUT PARAMETERS:
        S               -   SSA model
        TopK            -   number of components to analyze; TopK>=1.

    OUTPUT PARAMETERS:
        S               -   updated model

    NOTE: this  algorithm  is  optimized  for  large-scale  tasks  with  large
          datasets. On toy problems with just  5-10 points it can return basis
          which is slightly different from that returned by  direct  algorithm
          (ssasetalgotopkdirect() function). However, the  difference  becomes
          negligible as dataset grows.

    NOTE: TopK>WindowWidth is silently decreased to WindowWidth during analysis
          phase
          
    NOTE: calling this function invalidates basis, except  for  the  situation
          when this algorithm was already set with same parameters.

      -- ALGLIB --
         Copyright 30.10.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void ssasetalgotopkrealtime(ssamodel s,
        int topk,
        xparams _params)
    {
        ap.assert(topk >= 1, "SSASetAlgoTopKRealTime: TopK<1");

        //
        // Ignore calls which change nothing
        //
        if (s.algotype == 3 && s.topk == topk)
        {
            return;
        }

        //
        // Update settings, invalidate model
        //
        s.algotype = 3;
        s.topk = topk;
        s.arebasisandsolvervalid = false;
    }


    /*************************************************************************
    This function clears all data stored in the  model  and  invalidates  all
    basis components found so far.
       
    INPUT PARAMETERS:
        S               -   SSA model created with ssacreate()

    OUTPUT PARAMETERS:
        S               -   SSA model, updated

      -- ALGLIB --
         Copyright 30.10.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void ssacleardata(ssamodel s,
        xparams _params)
    {
        s.nsequences = 0;
        s.arebasisandsolvervalid = false;
    }


    /*************************************************************************
    This function executes SSA on internally stored dataset and returns  basis
    found by current method.
       
    INPUT PARAMETERS:
        S               -   SSA model

    OUTPUT PARAMETERS:
        A               -   array[WindowWidth,NBasis],   basis;  vectors  are
                            stored in matrix columns, by descreasing variance
        SV              -   array[NBasis]:
                            * zeros - for model initialized with SSASetAlgoPrecomputed()
                            * singular values - for other algorithms
        WindowWidth     -   current window
        NBasis          -   basis size
        

    CACHING/REUSE OF THE BASIS

    Caching/reuse of previous results is performed:
    * first call performs full run of SSA; basis is stored in the cache
    * subsequent calls reuse previously cached basis
    * if you call any function which changes model properties (window  length,
      algorithm, dataset), internal basis will be invalidated.
    * the only calls which do NOT invalidate basis are listed below:
      a) ssasetwindow() with same window length
      b) ssaappendpointandupdate()
      c) ssaappendsequenceandupdate()
      d) ssasetalgotopk...() with exactly same K
      Calling these functions will result in reuse of previously found basis.
      
      
    HANDLING OF DEGENERATE CASES
        
    Calling  this  function  in  degenerate  cases  (no  data  or all data are
    shorter than window size; no algorithm is specified)  returns  basis  with
    just one zero vector.

      -- ALGLIB --
         Copyright 30.10.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void ssagetbasis(ssamodel s,
        ref double[,] a,
        ref double[] sv,
        ref int windowwidth,
        ref int nbasis,
        xparams _params)
    {
        int i = 0;

        a = new double[0, 0];
        sv = new double[0];
        windowwidth = 0;
        nbasis = 0;


        //
        // Is it degenerate case?
        //
        if (!hassomethingtoanalyze(s, _params))
        {
            windowwidth = s.windowwidth;
            nbasis = 1;
            a = new double[windowwidth, 1];
            for (i = 0; i <= windowwidth - 1; i++)
            {
                a[i, 0] = 0.0;
            }
            sv = new double[1];
            sv[0] = 0.0;
            return;
        }

        //
        // Update basis.
        //
        // It will take care of basis validity flags. AppendLen=0 which means
        // that we perform initial basis evaluation.
        //
        updatebasis(s, 0, 0.0, _params);

        //
        // Output
        //
        ap.assert(s.nbasis > 0, "SSAGetBasis: integrity check failed");
        ap.assert(s.windowwidth > 0, "SSAGetBasis: integrity check failed");
        nbasis = s.nbasis;
        windowwidth = s.windowwidth;
        a = new double[windowwidth, nbasis];
        ablas.rmatrixcopy(windowwidth, nbasis, s.basis, 0, 0, a, 0, 0, _params);
        sv = new double[nbasis];
        for (i = 0; i <= nbasis - 1; i++)
        {
            sv[i] = s.sv[i];
        }
    }


    /*************************************************************************
    This function returns linear recurrence relation (LRR) coefficients  found
    by current SSA algorithm.
       
    INPUT PARAMETERS:
        S               -   SSA model

    OUTPUT PARAMETERS:
        A               -   array[WindowWidth-1]. Coefficients  of  the
                            linear recurrence of the form:
                            X[W-1] = X[W-2]*A[W-2] + X[W-3]*A[W-3] + ... + X[0]*A[0].
                            Empty array for WindowWidth=1.
        WindowWidth     -   current window width
        

    CACHING/REUSE OF THE BASIS

    Caching/reuse of previous results is performed:
    * first call performs full run of SSA; basis is stored in the cache
    * subsequent calls reuse previously cached basis
    * if you call any function which changes model properties (window  length,
      algorithm, dataset), internal basis will be invalidated.
    * the only calls which do NOT invalidate basis are listed below:
      a) ssasetwindow() with same window length
      b) ssaappendpointandupdate()
      c) ssaappendsequenceandupdate()
      d) ssasetalgotopk...() with exactly same K
      Calling these functions will result in reuse of previously found basis.
      
      
    HANDLING OF DEGENERATE CASES
        
    Calling  this  function  in  degenerate  cases  (no  data  or all data are
    shorter than window size; no algorithm is specified) returns zeros.

      -- ALGLIB --
         Copyright 30.10.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void ssagetlrr(ssamodel s,
        ref double[] a,
        ref int windowwidth,
        xparams _params)
    {
        int i = 0;

        a = new double[0];
        windowwidth = 0;

        ap.assert(s.windowwidth > 0, "SSAGetLRR: integrity check failed");

        //
        // Is it degenerate case?
        //
        if (!hassomethingtoanalyze(s, _params))
        {
            windowwidth = s.windowwidth;
            a = new double[windowwidth - 1];
            for (i = 0; i <= windowwidth - 2; i++)
            {
                a[i] = 0.0;
            }
            return;
        }

        //
        // Update basis.
        //
        // It will take care of basis validity flags. AppendLen=0 which means
        // that we perform initial basis evaluation.
        //
        updatebasis(s, 0, 0.0, _params);

        //
        // Output
        //
        windowwidth = s.windowwidth;
        a = new double[windowwidth - 1];
        for (i = 0; i <= windowwidth - 2; i++)
        {
            a[i] = s.forecasta[i];
        }
    }


    /*************************************************************************
    This  function  executes  SSA  on  internally  stored  dataset and returns
    analysis  for  the  last  window  of  the  last sequence. Such analysis is
    an lightweight alternative for full scale reconstruction (see below).

    Typical use case for this function is  real-time  setting,  when  you  are
    interested in quick-and-dirty (very quick and very  dirty)  processing  of
    just a few last ticks of the trend.

    IMPORTANT: full  scale  SSA  involves  analysis  of  the  ENTIRE  dataset,
               with reconstruction being done for  all  positions  of  sliding
               window with subsequent hankelization  (diagonal  averaging)  of
               the resulting matrix.
               
               Such analysis requires O((DataLen-Window)*Window*NBasis)  FLOPs
               and can be quite costly. However, it has  nice  noise-canceling
               effects due to averaging.
               
               This function performs REDUCED analysis of the last window.  It
               is much faster - just O(Window*NBasis),  but  its  results  are
               DIFFERENT from that of ssaanalyzelast(). In  particular,  first
               few points of the trend are much more prone to noise.
       
    INPUT PARAMETERS:
        S               -   SSA model

    OUTPUT PARAMETERS:
        Trend           -   array[WindowSize], reconstructed trend line
        Noise           -   array[WindowSize], the rest of the signal;
                            it holds that ActualData = Trend+Noise.
        NTicks          -   current WindowSize
        

    CACHING/REUSE OF THE BASIS

    Caching/reuse of previous results is performed:
    * first call performs full run of SSA; basis is stored in the cache
    * subsequent calls reuse previously cached basis
    * if you call any function which changes model properties (window  length,
      algorithm, dataset), internal basis will be invalidated.
    * the only calls which do NOT invalidate basis are listed below:
      a) ssasetwindow() with same window length
      b) ssaappendpointandupdate()
      c) ssaappendsequenceandupdate()
      d) ssasetalgotopk...() with exactly same K
      Calling these functions will result in reuse of previously found basis.

    In  any  case,  only  basis  is  reused. Reconstruction is performed  from
    scratch every time you call this function.
      
      
    HANDLING OF DEGENERATE CASES

    Following degenerate cases may happen:
    * dataset is empty (no analysis can be done)
    * all sequences are shorter than the window length,no analysis can be done
    * no algorithm is specified (no analysis can be done)
    * last sequence is shorter than the window length (analysis can  be  done,
      but we can not perform reconstruction on the last sequence)
        
    Calling this function in degenerate cases returns following result:
    * in any case, WindowWidth ticks is returned
    * trend is assumed to be zero
    * noise is initialized by the last sequence; if last sequence  is  shorter
      than the window size, it is moved to  the  end  of  the  array, and  the
      beginning of the noise array is filled by zeros
      
    No analysis is performed in degenerate cases (we immediately return  dummy
    values, no basis is constructed).

      -- ALGLIB --
         Copyright 30.10.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void ssaanalyzelastwindow(ssamodel s,
        ref double[] trend,
        ref double[] noise,
        ref int nticks,
        xparams _params)
    {
        int i = 0;
        int offs = 0;
        int cnt = 0;

        trend = new double[0];
        noise = new double[0];
        nticks = 0;


        //
        // Init
        //
        nticks = s.windowwidth;
        trend = new double[s.windowwidth];
        noise = new double[s.windowwidth];

        //
        // Is it degenerate case?
        //
        if (!hassomethingtoanalyze(s, _params) || !issequencebigenough(s, -1, _params))
        {
            for (i = 0; i <= nticks - 1; i++)
            {
                trend[i] = 0;
                noise[i] = 0;
            }
            if (s.nsequences >= 1)
            {
                cnt = Math.Min(s.sequenceidx[s.nsequences] - s.sequenceidx[s.nsequences - 1], nticks);
                offs = s.sequenceidx[s.nsequences] - cnt;
                for (i = 0; i <= cnt - 1; i++)
                {
                    noise[nticks - cnt + i] = s.sequencedata[offs + i];
                }
            }
            return;
        }

        //
        // Update basis.
        //
        // It will take care of basis validity flags. AppendLen=0 which means
        // that we perform initial basis evaluation.
        //
        updatebasis(s, 0, 0.0, _params);

        //
        // Perform analysis of the last window
        //
        ap.assert(s.sequenceidx[s.nsequences] - s.windowwidth >= 0, "SSAAnalyzeLastWindow: integrity check failed");
        apserv.rvectorsetlengthatleast(ref s.tmp0, s.nbasis, _params);
        ablas.rmatrixgemv(s.nbasis, s.windowwidth, 1.0, s.basist, 0, 0, 0, s.sequencedata, s.sequenceidx[s.nsequences] - s.windowwidth, 0.0, s.tmp0, 0, _params);
        ablas.rmatrixgemv(s.windowwidth, s.nbasis, 1.0, s.basis, 0, 0, 0, s.tmp0, 0, 0.0, trend, 0, _params);
        offs = s.sequenceidx[s.nsequences] - s.windowwidth;
        cnt = s.windowwidth;
        for (i = 0; i <= cnt - 1; i++)
        {
            noise[i] = s.sequencedata[offs + i] - trend[i];
        }
    }


    /*************************************************************************
    This function:
    * builds SSA basis using internally stored (entire) dataset
    * returns reconstruction for the last NTicks of the last sequence

    If you want to analyze some other sequence, use ssaanalyzesequence().

    Reconstruction phase involves  generation  of  NTicks-WindowWidth  sliding
    windows, their decomposition using empirical orthogonal functions found by
    SSA, followed by averaging of each data point across  several  overlapping
    windows. Thus, every point in the output trend is reconstructed  using  up
    to WindowWidth overlapping  windows  (WindowWidth windows exactly  in  the
    inner points, just one window at the extremal points).

    IMPORTANT: due to averaging this function returns  different  results  for
               different values of NTicks. It is expected and not a bug.
               
               For example:
               * Trend[NTicks-1] is always same because it is not averaged  in
                 any case (same applies to Trend[0]).
               * Trend[NTicks-2] has different values  for  NTicks=WindowWidth
                 and NTicks=WindowWidth+1 because former  case  means that  no
                 averaging is performed, and latter  case means that averaging
                 using two sliding windows  is  performed.  Larger  values  of
                 NTicks produce same results as NTicks=WindowWidth+1.
               * ...and so on...

    PERFORMANCE: this  function has O((NTicks-WindowWidth)*WindowWidth*NBasis)
                 running time. If you work  in  time-constrained  setting  and
                 have to analyze just a few last ticks, choosing NTicks  equal
                 to WindowWidth+SmoothingLen, with SmoothingLen=1...WindowWidth
                 will result in good compromise between noise cancellation and
                 analysis speed.

    INPUT PARAMETERS:
        S               -   SSA model
        NTicks          -   number of ticks to analyze, Nticks>=1.
                            * special case of NTicks<=WindowWidth  is  handled
                              by analyzing last window and  returning   NTicks 
                              last ticks.
                            * special case NTicks>LastSequenceLen  is  handled
                              by prepending result with NTicks-LastSequenceLen
                              zeros.

    OUTPUT PARAMETERS:
        Trend           -   array[NTicks], reconstructed trend line
        Noise           -   array[NTicks], the rest of the signal;
                            it holds that ActualData = Trend+Noise.
        

    CACHING/REUSE OF THE BASIS

    Caching/reuse of previous results is performed:
    * first call performs full run of SSA; basis is stored in the cache
    * subsequent calls reuse previously cached basis
    * if you call any function which changes model properties (window  length,
      algorithm, dataset), internal basis will be invalidated.
    * the only calls which do NOT invalidate basis are listed below:
      a) ssasetwindow() with same window length
      b) ssaappendpointandupdate()
      c) ssaappendsequenceandupdate()
      d) ssasetalgotopk...() with exactly same K
      Calling these functions will result in reuse of previously found basis.

    In  any  case,  only  basis  is  reused. Reconstruction is performed  from
    scratch every time you call this function.
      
      
    HANDLING OF DEGENERATE CASES

    Following degenerate cases may happen:
    * dataset is empty (no analysis can be done)
    * all sequences are shorter than the window length,no analysis can be done
    * no algorithm is specified (no analysis can be done)
    * last sequence is shorter than the window length (analysis  can  be done,
      but we can not perform reconstruction on the last sequence)
        
    Calling this function in degenerate cases returns following result:
    * in any case, NTicks ticks is returned
    * trend is assumed to be zero
    * noise is initialized by the last sequence; if last sequence  is  shorter
      than the window size, it is moved to  the  end  of  the  array, and  the
      beginning of the noise array is filled by zeros
      
    No analysis is performed in degenerate cases (we immediately return  dummy
    values, no basis is constructed).

      -- ALGLIB --
         Copyright 30.10.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void ssaanalyzelast(ssamodel s,
        int nticks,
        ref double[] trend,
        ref double[] noise,
        xparams _params)
    {
        int i = 0;
        int offs = 0;
        int cnt = 0;
        int cntzeros = 0;

        trend = new double[0];
        noise = new double[0];

        ap.assert(nticks >= 1, "SSAAnalyzeLast: NTicks<1");

        //
        // Init
        //
        trend = new double[nticks];
        noise = new double[nticks];

        //
        // Is it degenerate case?
        //
        if (!hassomethingtoanalyze(s, _params) || !issequencebigenough(s, -1, _params))
        {
            for (i = 0; i <= nticks - 1; i++)
            {
                trend[i] = 0;
                noise[i] = 0;
            }
            if (s.nsequences >= 1)
            {
                cnt = Math.Min(s.sequenceidx[s.nsequences] - s.sequenceidx[s.nsequences - 1], nticks);
                offs = s.sequenceidx[s.nsequences] - cnt;
                for (i = 0; i <= cnt - 1; i++)
                {
                    noise[nticks - cnt + i] = s.sequencedata[offs + i];
                }
            }
            return;
        }

        //
        // Fast exit: NTicks<=WindowWidth, just last window is analyzed
        //
        if (nticks <= s.windowwidth)
        {
            ssaanalyzelastwindow(s, ref s.alongtrend, ref s.alongnoise, ref cnt, _params);
            offs = s.windowwidth - nticks;
            for (i = 0; i <= nticks - 1; i++)
            {
                trend[i] = s.alongtrend[offs + i];
                noise[i] = s.alongnoise[offs + i];
            }
            return;
        }

        //
        // Update basis.
        //
        // It will take care of basis validity flags. AppendLen=0 which means
        // that we perform initial basis evaluation.
        //
        updatebasis(s, 0, 0.0, _params);

        //
        // Perform analysis:
        // * prepend max(NTicks-LastSequenceLength,0) zeros to the beginning
        //   of array
        // * analyze the rest with AnalyzeSequence() which assumes that we
        //   already have basis
        //
        ap.assert(s.sequenceidx[s.nsequences] - s.sequenceidx[s.nsequences - 1] >= s.windowwidth, "SSAAnalyzeLast: integrity check failed / 23vd4");
        cntzeros = Math.Max(nticks - (s.sequenceidx[s.nsequences] - s.sequenceidx[s.nsequences - 1]), 0);
        for (i = 0; i <= cntzeros - 1; i++)
        {
            trend[i] = 0.0;
            noise[i] = 0.0;
        }
        cnt = Math.Min(nticks, s.sequenceidx[s.nsequences] - s.sequenceidx[s.nsequences - 1]);
        analyzesequence(s, s.sequencedata, s.sequenceidx[s.nsequences] - cnt, s.sequenceidx[s.nsequences], trend, noise, cntzeros, _params);
    }


    /*************************************************************************
    This function:
    * builds SSA basis using internally stored (entire) dataset
    * returns reconstruction for the sequence being passed to this function

    If  you  want  to  analyze  last  sequence  stored  in   the   model,  use
    ssaanalyzelast().

    Reconstruction phase involves  generation  of  NTicks-WindowWidth  sliding
    windows, their decomposition using empirical orthogonal functions found by
    SSA, followed by averaging of each data point across  several  overlapping
    windows. Thus, every point in the output trend is reconstructed  using  up
    to WindowWidth overlapping  windows  (WindowWidth windows exactly  in  the
    inner points, just one window at the extremal points).

    PERFORMANCE: this  function has O((NTicks-WindowWidth)*WindowWidth*NBasis)
                 running time. If you work  in  time-constrained  setting  and
                 have to analyze just a few last ticks, choosing NTicks  equal
                 to WindowWidth+SmoothingLen, with SmoothingLen=1...WindowWidth
                 will result in good compromise between noise cancellation and
                 analysis speed.

    INPUT PARAMETERS:
        S               -   SSA model
        Data            -   array[NTicks], can be larger (only NTicks  leading
                            elements will be used)
        NTicks          -   number of ticks to analyze, Nticks>=1.
                            * special case of NTicks<WindowWidth  is   handled
                              by returning zeros as trend, and signal as noise

    OUTPUT PARAMETERS:
        Trend           -   array[NTicks], reconstructed trend line
        Noise           -   array[NTicks], the rest of the signal;
                            it holds that ActualData = Trend+Noise.
        

    CACHING/REUSE OF THE BASIS

    Caching/reuse of previous results is performed:
    * first call performs full run of SSA; basis is stored in the cache
    * subsequent calls reuse previously cached basis
    * if you call any function which changes model properties (window  length,
      algorithm, dataset), internal basis will be invalidated.
    * the only calls which do NOT invalidate basis are listed below:
      a) ssasetwindow() with same window length
      b) ssaappendpointandupdate()
      c) ssaappendsequenceandupdate()
      d) ssasetalgotopk...() with exactly same K
      Calling these functions will result in reuse of previously found basis.

    In  any  case,  only  basis  is  reused. Reconstruction is performed  from
    scratch every time you call this function.
      
      
    HANDLING OF DEGENERATE CASES

    Following degenerate cases may happen:
    * dataset is empty (no analysis can be done)
    * all sequences are shorter than the window length,no analysis can be done
    * no algorithm is specified (no analysis can be done)
    * sequence being passed is shorter than the window length
        
    Calling this function in degenerate cases returns following result:
    * in any case, NTicks ticks is returned
    * trend is assumed to be zero
    * noise is initialized by the sequence.
      
    No analysis is performed in degenerate cases (we immediately return  dummy
    values, no basis is constructed).

      -- ALGLIB --
         Copyright 30.10.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void ssaanalyzesequence(ssamodel s,
        double[] data,
        int nticks,
        ref double[] trend,
        ref double[] noise,
        xparams _params)
    {
        int i = 0;

        trend = new double[0];
        noise = new double[0];

        ap.assert(nticks >= 1, "SSAAnalyzeSequence: NTicks<1");
        ap.assert(ap.len(data) >= nticks, "SSAAnalyzeSequence: Data is too short");
        ap.assert(apserv.isfinitevector(data, nticks, _params), "SSAAnalyzeSequence: Data contains infinities NANs");

        //
        // Init
        //
        trend = new double[nticks];
        noise = new double[nticks];

        //
        // Is it degenerate case?
        //
        if (!hassomethingtoanalyze(s, _params) || nticks < s.windowwidth)
        {
            for (i = 0; i <= nticks - 1; i++)
            {
                trend[i] = 0;
                noise[i] = data[i];
            }
            return;
        }

        //
        // Update basis.
        //
        // It will take care of basis validity flags. AppendLen=0 which means
        // that we perform initial basis evaluation.
        //
        updatebasis(s, 0, 0.0, _params);

        //
        // Perform analysis
        //
        analyzesequence(s, data, 0, nticks, trend, noise, 0, _params);
    }


    /*************************************************************************
    This function builds SSA basis and performs forecasting  for  a  specified
    number of ticks, returning value of trend.

    Forecast is performed as follows:
    * SSA  trend  extraction  is  applied  to last WindowWidth elements of the
      internally stored dataset; this step is basically a noise reduction.
    * linear recurrence relation is applied to extracted trend

    This function has following running time:
    * O(NBasis*WindowWidth) for trend extraction phase (always performed)
    * O(WindowWidth*NTicks) for forecast phase

    NOTE: noise reduction is ALWAYS applied by this algorithm; if you want  to
          apply recurrence relation  to  raw  unprocessed  data,  use  another
          function - ssaforecastsequence() which allows to  turn  on  and  off
          noise reduction phase.
          
    NOTE: this algorithm performs prediction using only one - last  -  sliding
          window.  Predictions  produced   by   such   approach   are   smooth
          continuations of the reconstructed  trend  line,  but  they  can  be
          easily corrupted by noise. If you need  noise-resistant  prediction,
          use ssaforecastavglast() function, which averages predictions  built
          using several sliding windows.

    INPUT PARAMETERS:
        S               -   SSA model
        NTicks          -   number of ticks to forecast, NTicks>=1

    OUTPUT PARAMETERS:
        Trend           -   array[NTicks], predicted trend line
        

    CACHING/REUSE OF THE BASIS

    Caching/reuse of previous results is performed:
    * first call performs full run of SSA; basis is stored in the cache
    * subsequent calls reuse previously cached basis
    * if you call any function which changes model properties (window  length,
      algorithm, dataset), internal basis will be invalidated.
    * the only calls which do NOT invalidate basis are listed below:
      a) ssasetwindow() with same window length
      b) ssaappendpointandupdate()
      c) ssaappendsequenceandupdate()
      d) ssasetalgotopk...() with exactly same K
      Calling these functions will result in reuse of previously found basis.


    HANDLING OF DEGENERATE CASES

    Following degenerate cases may happen:
    * dataset is empty (no analysis can be done)
    * all sequences are shorter than the window length,no analysis can be done
    * no algorithm is specified (no analysis can be done)
    * last sequence is shorter than the WindowWidth   (analysis  can  be done,
      but we can not perform forecasting on the last sequence)
    * window lentgh is 1 (impossible to use for forecasting)
    * SSA analysis algorithm is  configured  to  extract  basis  whose size is
      equal to window length (impossible to use for  forecasting;  only  basis
      whose size is less than window length can be used).
        
    Calling this function in degenerate cases returns following result:
    * NTicks  copies  of  the  last  value is returned for non-empty task with
      large enough dataset, but with overcomplete  basis  (window  width=1  or
      basis size is equal to window width)
    * zero trend with length=NTicks is returned for empty task
      
    No analysis is performed in degenerate cases (we immediately return  dummy
    values, no basis is ever constructed).

      -- ALGLIB --
         Copyright 30.10.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void ssaforecastlast(ssamodel s,
        int nticks,
        ref double[] trend,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double v = 0;
        int winw = 0;

        trend = new double[0];

        ap.assert(nticks >= 1, "SSAForecast: NTicks<1");

        //
        // Init
        //
        winw = s.windowwidth;
        trend = new double[nticks];

        //
        // Is it degenerate case?
        //
        if (!hassomethingtoanalyze(s, _params))
        {
            for (i = 0; i <= nticks - 1; i++)
            {
                trend[i] = 0;
            }
            return;
        }
        ap.assert(s.nsequences > 0, "SSAForecastLast: integrity check failed");
        if (s.sequenceidx[s.nsequences] - s.sequenceidx[s.nsequences - 1] < winw)
        {
            for (i = 0; i <= nticks - 1; i++)
            {
                trend[i] = 0;
            }
            return;
        }
        if (winw == 1)
        {
            ap.assert(s.nsequences > 0, "SSAForecast: integrity check failed / 2355");
            ap.assert(s.sequenceidx[s.nsequences] - s.sequenceidx[s.nsequences - 1] > 0, "SSAForecast: integrity check failed");
            for (i = 0; i <= nticks - 1; i++)
            {
                trend[i] = s.sequencedata[s.sequenceidx[s.nsequences] - 1];
            }
            return;
        }

        //
        // Update basis and recurrent relation.
        //
        // It will take care of basis validity flags. AppendLen=0 which means
        // that we perform initial basis evaluation.
        //
        updatebasis(s, 0, 0.0, _params);
        ap.assert(s.nbasis <= winw && s.nbasis > 0, "SSAForecast: integrity check failed / 4f5et");
        if (s.nbasis == winw)
        {

            //
            // Handle degenerate situation with basis whose size
            // is equal to window length.
            //
            ap.assert(s.nsequences > 0, "SSAForecast: integrity check failed / 2355");
            ap.assert(s.sequenceidx[s.nsequences] - s.sequenceidx[s.nsequences - 1] > 0, "SSAForecast: integrity check failed");
            for (i = 0; i <= nticks - 1; i++)
            {
                trend[i] = s.sequencedata[s.sequenceidx[s.nsequences] - 1];
            }
            return;
        }

        //
        // Apply recurrent formula for SSA forecasting:
        // * first, perform smoothing of the last window
        // * second, perform analysis phase
        //
        ap.assert(s.nsequences > 0, "SSAForecastLast: integrity check failed");
        ap.assert(s.sequenceidx[s.nsequences] - s.sequenceidx[s.nsequences - 1] >= s.windowwidth, "SSAForecastLast: integrity check failed");
        apserv.rvectorsetlengthatleast(ref s.tmp0, s.nbasis, _params);
        apserv.rvectorsetlengthatleast(ref s.fctrend, s.windowwidth, _params);
        ablas.rmatrixgemv(s.nbasis, s.windowwidth, 1.0, s.basist, 0, 0, 0, s.sequencedata, s.sequenceidx[s.nsequences] - s.windowwidth, 0.0, s.tmp0, 0, _params);
        ablas.rmatrixgemv(s.windowwidth, s.nbasis, 1.0, s.basis, 0, 0, 0, s.tmp0, 0, 0.0, s.fctrend, 0, _params);
        apserv.rvectorsetlengthatleast(ref s.tmp1, winw - 1, _params);
        for (i = 1; i <= winw - 1; i++)
        {
            s.tmp1[i - 1] = s.fctrend[i];
        }
        for (i = 0; i <= nticks - 1; i++)
        {
            v = s.forecasta[0] * s.tmp1[0];
            for (j = 1; j <= winw - 2; j++)
            {
                v = v + s.forecasta[j] * s.tmp1[j];
                s.tmp1[j - 1] = s.tmp1[j];
            }
            trend[i] = v;
            s.tmp1[winw - 2] = v;
        }
    }


    /*************************************************************************
    This function builds SSA  basis  and  performs  forecasting  for  a  user-
    specified sequence, returning value of trend.

    Forecasting is done in two stages:
    * first,  we  extract  trend  from the WindowWidth  last  elements of  the
      sequence. This stage is optional, you  can  turn  it  off  if  you  pass
      data which are already processed with SSA. Of course, you  can  turn  it
      off even for raw data, but it is not recommended - noise suppression  is
      very important for correct prediction.
    * then, we apply LRR for last  WindowWidth-1  elements  of  the  extracted
      trend.

    This function has following running time:
    * O(NBasis*WindowWidth) for trend extraction phase
    * O(WindowWidth*NTicks) for forecast phase
          
    NOTE: this algorithm performs prediction using only one - last  -  sliding
          window.  Predictions  produced   by   such   approach   are   smooth
          continuations of the reconstructed  trend  line,  but  they  can  be
          easily corrupted by noise. If you need  noise-resistant  prediction,
          use ssaforecastavgsequence() function,  which  averages  predictions
          built using several sliding windows.

    INPUT PARAMETERS:
        S               -   SSA model
        Data            -   array[NTicks], data to forecast
        DataLen         -   number of ticks in the data, DataLen>=1
        ForecastLen     -   number of ticks to predict, ForecastLen>=1
        ApplySmoothing  -   whether to apply smoothing trend extraction or not;
                            if you do not know what to specify, pass True.

    OUTPUT PARAMETERS:
        Trend           -   array[ForecastLen], forecasted trend
        

    CACHING/REUSE OF THE BASIS

    Caching/reuse of previous results is performed:
    * first call performs full run of SSA; basis is stored in the cache
    * subsequent calls reuse previously cached basis
    * if you call any function which changes model properties (window  length,
      algorithm, dataset), internal basis will be invalidated.
    * the only calls which do NOT invalidate basis are listed below:
      a) ssasetwindow() with same window length
      b) ssaappendpointandupdate()
      c) ssaappendsequenceandupdate()
      d) ssasetalgotopk...() with exactly same K
      Calling these functions will result in reuse of previously found basis.


    HANDLING OF DEGENERATE CASES

    Following degenerate cases may happen:
    * dataset is empty (no analysis can be done)
    * all sequences are shorter than the window length,no analysis can be done
    * no algorithm is specified (no analysis can be done)
    * data sequence is shorter than the WindowWidth   (analysis  can  be done,
      but we can not perform forecasting on the last sequence)
    * window lentgh is 1 (impossible to use for forecasting)
    * SSA analysis algorithm is  configured  to  extract  basis  whose size is
      equal to window length (impossible to use for  forecasting;  only  basis
      whose size is less than window length can be used).
        
    Calling this function in degenerate cases returns following result:
    * ForecastLen copies of the last value is returned for non-empty task with
      large enough dataset, but with overcomplete  basis  (window  width=1  or
      basis size is equal to window width)
    * zero trend with length=ForecastLen is returned for empty task
      
    No analysis is performed in degenerate cases (we immediately return  dummy
    values, no basis is ever constructed).

      -- ALGLIB --
         Copyright 30.10.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void ssaforecastsequence(ssamodel s,
        double[] data,
        int datalen,
        int forecastlen,
        bool applysmoothing,
        ref double[] trend,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double v = 0;
        int winw = 0;

        trend = new double[0];

        ap.assert(datalen >= 1, "SSAForecastSequence: DataLen<1");
        ap.assert(ap.len(data) >= datalen, "SSAForecastSequence: Data is too short");
        ap.assert(apserv.isfinitevector(data, datalen, _params), "SSAForecastSequence: Data contains infinities NANs");
        ap.assert(forecastlen >= 1, "SSAForecastSequence: ForecastLen<1");

        //
        // Init
        //
        winw = s.windowwidth;
        trend = new double[forecastlen];

        //
        // Is it degenerate case?
        //
        if (!hassomethingtoanalyze(s, _params) || datalen < winw)
        {
            for (i = 0; i <= forecastlen - 1; i++)
            {
                trend[i] = 0;
            }
            return;
        }
        if (winw == 1)
        {
            for (i = 0; i <= forecastlen - 1; i++)
            {
                trend[i] = data[datalen - 1];
            }
            return;
        }

        //
        // Update basis.
        //
        // It will take care of basis validity flags. AppendLen=0 which means
        // that we perform initial basis evaluation.
        //
        updatebasis(s, 0, 0.0, _params);
        ap.assert(s.nbasis <= winw && s.nbasis > 0, "SSAForecast: integrity check failed / 4f5et");
        if (s.nbasis == winw)
        {

            //
            // Handle degenerate situation with basis whose size
            // is equal to window length.
            //
            for (i = 0; i <= forecastlen - 1; i++)
            {
                trend[i] = data[datalen - 1];
            }
            return;
        }

        //
        // Perform trend extraction
        //
        apserv.rvectorsetlengthatleast(ref s.fctrend, s.windowwidth, _params);
        if (applysmoothing)
        {
            ap.assert(datalen >= winw, "SSAForecastSequence: integrity check failed");
            apserv.rvectorsetlengthatleast(ref s.tmp0, s.nbasis, _params);
            ablas.rmatrixgemv(s.nbasis, winw, 1.0, s.basist, 0, 0, 0, data, datalen - winw, 0.0, s.tmp0, 0, _params);
            ablas.rmatrixgemv(winw, s.nbasis, 1.0, s.basis, 0, 0, 0, s.tmp0, 0, 0.0, s.fctrend, 0, _params);
        }
        else
        {
            for (i = 0; i <= winw - 1; i++)
            {
                s.fctrend[i] = data[datalen + i - winw];
            }
        }

        //
        // Apply recurrent formula for SSA forecasting
        //
        apserv.rvectorsetlengthatleast(ref s.tmp1, winw - 1, _params);
        for (i = 1; i <= winw - 1; i++)
        {
            s.tmp1[i - 1] = s.fctrend[i];
        }
        for (i = 0; i <= forecastlen - 1; i++)
        {
            v = s.forecasta[0] * s.tmp1[0];
            for (j = 1; j <= winw - 2; j++)
            {
                v = v + s.forecasta[j] * s.tmp1[j];
                s.tmp1[j - 1] = s.tmp1[j];
            }
            trend[i] = v;
            s.tmp1[winw - 2] = v;
        }
    }


    /*************************************************************************
    This function builds SSA basis and performs forecasting  for  a  specified
    number of ticks, returning value of trend.

    Forecast is performed as follows:
    * SSA  trend  extraction  is  applied to last  M  sliding windows  of  the
      internally stored dataset
    * for each of M sliding windows, M predictions are built
    * average value of M predictions is returned

    This function has following running time:
    * O(NBasis*WindowWidth*M) for trend extraction phase (always performed)
    * O(WindowWidth*NTicks*M) for forecast phase

    NOTE: noise reduction is ALWAYS applied by this algorithm; if you want  to
          apply recurrence relation  to  raw  unprocessed  data,  use  another
          function - ssaforecastsequence() which allows to  turn  on  and  off
          noise reduction phase.
          
    NOTE: combination of several predictions results in lesser sensitivity  to
          noise, but it may produce undesirable discontinuities  between  last
          point of the trend and first point of the prediction. The reason  is
          that  last  point  of  the  trend is usually corrupted by noise, but
          average  value of  several  predictions  is less sensitive to noise,
          thus discontinuity appears. It is not a bug.

    INPUT PARAMETERS:
        S               -   SSA model
        M               -   number  of  sliding  windows  to combine, M>=1. If
                            your dataset has less than M sliding windows, this
                            parameter will be silently reduced.
        NTicks          -   number of ticks to forecast, NTicks>=1

    OUTPUT PARAMETERS:
        Trend           -   array[NTicks], predicted trend line
        

    CACHING/REUSE OF THE BASIS

    Caching/reuse of previous results is performed:
    * first call performs full run of SSA; basis is stored in the cache
    * subsequent calls reuse previously cached basis
    * if you call any function which changes model properties (window  length,
      algorithm, dataset), internal basis will be invalidated.
    * the only calls which do NOT invalidate basis are listed below:
      a) ssasetwindow() with same window length
      b) ssaappendpointandupdate()
      c) ssaappendsequenceandupdate()
      d) ssasetalgotopk...() with exactly same K
      Calling these functions will result in reuse of previously found basis.


    HANDLING OF DEGENERATE CASES

    Following degenerate cases may happen:
    * dataset is empty (no analysis can be done)
    * all sequences are shorter than the window length,no analysis can be done
    * no algorithm is specified (no analysis can be done)
    * last sequence is shorter than the WindowWidth   (analysis  can  be done,
      but we can not perform forecasting on the last sequence)
    * window lentgh is 1 (impossible to use for forecasting)
    * SSA analysis algorithm is  configured  to  extract  basis  whose size is
      equal to window length (impossible to use for  forecasting;  only  basis
      whose size is less than window length can be used).
        
    Calling this function in degenerate cases returns following result:
    * NTicks  copies  of  the  last  value is returned for non-empty task with
      large enough dataset, but with overcomplete  basis  (window  width=1  or
      basis size is equal to window width)
    * zero trend with length=NTicks is returned for empty task
      
    No analysis is performed in degenerate cases (we immediately return  dummy
    values, no basis is ever constructed).

      -- ALGLIB --
         Copyright 30.10.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void ssaforecastavglast(ssamodel s,
        int m,
        int nticks,
        ref double[] trend,
        xparams _params)
    {
        int i = 0;
        int winw = 0;

        trend = new double[0];

        ap.assert(nticks >= 1, "SSAForecastAvgLast: NTicks<1");
        ap.assert(m >= 1, "SSAForecastAvgLast: M<1");

        //
        // Init
        //
        winw = s.windowwidth;
        trend = new double[nticks];

        //
        // Is it degenerate case?
        //
        if (!hassomethingtoanalyze(s, _params))
        {
            for (i = 0; i <= nticks - 1; i++)
            {
                trend[i] = 0;
            }
            return;
        }
        ap.assert(s.nsequences > 0, "SSAForecastAvgLast: integrity check failed");
        if (s.sequenceidx[s.nsequences] - s.sequenceidx[s.nsequences - 1] < winw)
        {
            for (i = 0; i <= nticks - 1; i++)
            {
                trend[i] = 0;
            }
            return;
        }
        if (winw == 1)
        {
            ap.assert(s.nsequences > 0, "SSAForecastAvgLast: integrity check failed / 2355");
            ap.assert(s.sequenceidx[s.nsequences] - s.sequenceidx[s.nsequences - 1] > 0, "SSAForecastAvgLast: integrity check failed");
            for (i = 0; i <= nticks - 1; i++)
            {
                trend[i] = s.sequencedata[s.sequenceidx[s.nsequences] - 1];
            }
            return;
        }

        //
        // Update basis and recurrent relation.
        //
        // It will take care of basis validity flags. AppendLen=0 which means
        // that we perform initial basis evaluation.
        //
        updatebasis(s, 0, 0.0, _params);
        ap.assert(s.nbasis <= winw && s.nbasis > 0, "SSAForecastAvgLast: integrity check failed / 4f5et");
        if (s.nbasis == winw)
        {

            //
            // Handle degenerate situation with basis whose size
            // is equal to window length.
            //
            ap.assert(s.nsequences > 0, "SSAForecastAvgLast: integrity check failed / 2355");
            ap.assert(s.sequenceidx[s.nsequences] - s.sequenceidx[s.nsequences - 1] > 0, "SSAForecastAvgLast: integrity check failed");
            for (i = 0; i <= nticks - 1; i++)
            {
                trend[i] = s.sequencedata[s.sequenceidx[s.nsequences] - 1];
            }
            return;
        }

        //
        // Decrease M if we have less than M sliding windows.
        // Forecast.
        //
        m = Math.Min(m, s.sequenceidx[s.nsequences] - s.sequenceidx[s.nsequences - 1] - winw + 1);
        ap.assert(m >= 1, "SSAForecastAvgLast: integrity check failed");
        forecastavgsequence(s, s.sequencedata, s.sequenceidx[s.nsequences - 1], s.sequenceidx[s.nsequences], m, nticks, true, trend, 0, _params);
    }


    /*************************************************************************
    This function builds SSA  basis  and  performs  forecasting  for  a  user-
    specified sequence, returning value of trend.

    Forecasting is done in two stages:
    * first,  we  extract  trend  from M last sliding windows of the sequence.
      This stage is optional, you can  turn  it  off  if  you  pass data which
      are already processed with SSA. Of course, you  can  turn  it  off  even
      for raw data, but it is not recommended  -  noise  suppression  is  very
      important for correct prediction.
    * then, we apply LRR independently for M sliding windows
    * average of M predictions is returned

    This function has following running time:
    * O(NBasis*WindowWidth*M) for trend extraction phase
    * O(WindowWidth*NTicks*M) for forecast phase
          
    NOTE: combination of several predictions results in lesser sensitivity  to
          noise, but it may produce undesirable discontinuities  between  last
          point of the trend and first point of the prediction. The reason  is
          that  last  point  of  the  trend is usually corrupted by noise, but
          average  value of  several  predictions  is less sensitive to noise,
          thus discontinuity appears. It is not a bug.

    INPUT PARAMETERS:
        S               -   SSA model
        Data            -   array[NTicks], data to forecast
        DataLen         -   number of ticks in the data, DataLen>=1
        M               -   number  of  sliding  windows  to combine, M>=1. If
                            your dataset has less than M sliding windows, this
                            parameter will be silently reduced.
        ForecastLen     -   number of ticks to predict, ForecastLen>=1
        ApplySmoothing  -   whether to apply smoothing trend extraction or not.
                            if you do not know what to specify, pass true.

    OUTPUT PARAMETERS:
        Trend           -   array[ForecastLen], forecasted trend
        

    CACHING/REUSE OF THE BASIS

    Caching/reuse of previous results is performed:
    * first call performs full run of SSA; basis is stored in the cache
    * subsequent calls reuse previously cached basis
    * if you call any function which changes model properties (window  length,
      algorithm, dataset), internal basis will be invalidated.
    * the only calls which do NOT invalidate basis are listed below:
      a) ssasetwindow() with same window length
      b) ssaappendpointandupdate()
      c) ssaappendsequenceandupdate()
      d) ssasetalgotopk...() with exactly same K
      Calling these functions will result in reuse of previously found basis.


    HANDLING OF DEGENERATE CASES

    Following degenerate cases may happen:
    * dataset is empty (no analysis can be done)
    * all sequences are shorter than the window length,no analysis can be done
    * no algorithm is specified (no analysis can be done)
    * data sequence is shorter than the WindowWidth   (analysis  can  be done,
      but we can not perform forecasting on the last sequence)
    * window lentgh is 1 (impossible to use for forecasting)
    * SSA analysis algorithm is  configured  to  extract  basis  whose size is
      equal to window length (impossible to use for  forecasting;  only  basis
      whose size is less than window length can be used).
        
    Calling this function in degenerate cases returns following result:
    * ForecastLen copies of the last value is returned for non-empty task with
      large enough dataset, but with overcomplete  basis  (window  width=1  or
      basis size is equal to window width)
    * zero trend with length=ForecastLen is returned for empty task
      
    No analysis is performed in degenerate cases (we immediately return  dummy
    values, no basis is ever constructed).

      -- ALGLIB --
         Copyright 30.10.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void ssaforecastavgsequence(ssamodel s,
        double[] data,
        int datalen,
        int m,
        int forecastlen,
        bool applysmoothing,
        ref double[] trend,
        xparams _params)
    {
        int i = 0;
        int winw = 0;

        trend = new double[0];

        ap.assert(datalen >= 1, "SSAForecastAvgSequence: DataLen<1");
        ap.assert(m >= 1, "SSAForecastAvgSequence: M<1");
        ap.assert(ap.len(data) >= datalen, "SSAForecastAvgSequence: Data is too short");
        ap.assert(apserv.isfinitevector(data, datalen, _params), "SSAForecastAvgSequence: Data contains infinities NANs");
        ap.assert(forecastlen >= 1, "SSAForecastAvgSequence: ForecastLen<1");

        //
        // Init
        //
        winw = s.windowwidth;
        trend = new double[forecastlen];

        //
        // Is it degenerate case?
        //
        if (!hassomethingtoanalyze(s, _params) || datalen < winw)
        {
            for (i = 0; i <= forecastlen - 1; i++)
            {
                trend[i] = 0;
            }
            return;
        }
        if (winw == 1)
        {
            for (i = 0; i <= forecastlen - 1; i++)
            {
                trend[i] = data[datalen - 1];
            }
            return;
        }

        //
        // Update basis.
        //
        // It will take care of basis validity flags. AppendLen=0 which means
        // that we perform initial basis evaluation.
        //
        updatebasis(s, 0, 0.0, _params);
        ap.assert(s.nbasis <= winw && s.nbasis > 0, "SSAForecast: integrity check failed / 4f5et");
        if (s.nbasis == winw)
        {

            //
            // Handle degenerate situation with basis whose size
            // is equal to window length.
            //
            for (i = 0; i <= forecastlen - 1; i++)
            {
                trend[i] = data[datalen - 1];
            }
            return;
        }

        //
        // Decrease M if we have less than M sliding windows.
        // Forecast.
        //
        m = Math.Min(m, datalen - winw + 1);
        ap.assert(m >= 1, "SSAForecastAvgLast: integrity check failed");
        forecastavgsequence(s, data, 0, datalen, m, forecastlen, applysmoothing, trend, 0, _params);
    }


    /*************************************************************************
    This function evaluates current model and tells whether we have some  data
    which can be analyzed by current algorithm, or not.

    No analysis can be done in the following degenerate cases:
    * dataset is empty
    * all sequences are shorter than the window length
    * no algorithm is specified

      -- ALGLIB --
         Copyright 30.10.2017 by Bochkanov Sergey
    *************************************************************************/
    private static bool hassomethingtoanalyze(ssamodel s,
        xparams _params)
    {
        bool result = new bool();
        int i = 0;
        bool allsmaller = new bool();
        bool isdegenerate = new bool();

        isdegenerate = false;
        isdegenerate = isdegenerate || s.algotype == 0;
        isdegenerate = isdegenerate || s.nsequences == 0;
        allsmaller = true;
        for (i = 0; i <= s.nsequences - 1; i++)
        {
            allsmaller = allsmaller && s.sequenceidx[i + 1] - s.sequenceidx[i] < s.windowwidth;
        }
        isdegenerate = isdegenerate || allsmaller;
        result = !isdegenerate;
        return result;
    }


    /*************************************************************************
    This function checks whether I-th sequence is big enough for analysis or not.

    I=-1 is used to denote last sequence (for NSequences=0)

      -- ALGLIB --
         Copyright 30.10.2017 by Bochkanov Sergey
    *************************************************************************/
    private static bool issequencebigenough(ssamodel s,
        int i,
        xparams _params)
    {
        bool result = new bool();

        ap.assert(i >= -1 && i < s.nsequences);
        result = false;
        if (s.nsequences == 0)
        {
            return result;
        }
        if (i < 0)
        {
            i = s.nsequences - 1;
        }
        result = s.sequenceidx[i + 1] - s.sequenceidx[i] >= s.windowwidth;
        return result;
    }


    /*************************************************************************
    This function performs basis update. Either full update (recalculated from
    the very beginning) or partial update (handles append to the  end  of  the
    dataset).

    With AppendLen=0 this function behaves as follows:
    * if AreBasisAndSolverValid=False, then  solver  object  is  created  from
      scratch, initial calculations are performed according  to  specific  SSA
      algorithm being chosen. Basis/Solver validity flag is set to True,  then
      we immediately return.
    * if AreBasisAndSolverValid=True, then nothing is done  -  we  immediately
      return.

    With AppendLen>0 this function behaves as follows:
    * if AreBasisAndSolverValid=False, then exception is  generated;  you  can
      append points only to fully constructed basis. Call this  function  with
      zero AppendLen BEFORE append, then perform append, then call it one more
      time with non-zero AppendLen.
    * if AreBasisAndSolverValid=True, then basis is incrementally updated.  It
      also updates recurrence relation used for prediction. It is expected that
      either AppendLen=1, or AppendLen=length(last_sequence). Basis update  is
      performed with probability UpdateIts (larger-than-one values  mean  that
      some amount of iterations is always performed).


    In any case, after calling this function we either:
    * have an exception
    * have completely valid basis

    IMPORTANT: this function expects that we do NOT call it for degenerate tasks
               (no data). So, call it after check with HasSomethingToAnalyze()
               returned True.

      -- ALGLIB --
         Copyright 30.10.2017 by Bochkanov Sergey
    *************************************************************************/
    private static void updatebasis(ssamodel s,
        int appendlen,
        double updateits,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        int srcoffs = 0;
        int dstoffs = 0;
        int winw = 0;
        int windowstotal = 0;
        int requesttype = 0;
        int requestsize = 0;
        double v = 0;
        bool degeneraterecurrence = new bool();
        double nu2 = 0;
        int subspaceits = 0;
        bool needevd = new bool();

        winw = s.windowwidth;

        //
        // Critical checks
        //
        ap.assert(appendlen >= 0, "SSA: incorrect parameters passed to UpdateBasis(), integrity check failed");
        ap.assert(!(!s.arebasisandsolvervalid && appendlen != 0), "SSA: incorrect parameters passed to UpdateBasis(), integrity check failed");
        ap.assert(!(appendlen == 0 && (double)(updateits) > (double)(0.0)), "SSA: incorrect parameters passed to UpdateBasis(), integrity check failed");

        //
        // Everything is OK, nothing to do
        //
        if (s.arebasisandsolvervalid && appendlen == 0)
        {
            return;
        }

        //
        // Seed RNG with fixed or random seed.
        //
        // RNG used when pseudorandomly deciding whether
        // to re-evaluate basis or not. Sandom seed is
        // important when we have several simultaneously
        // calculated SSA models - we do not want them
        // to be re-evaluated in same moments).
        //
        if (!s.arebasisandsolvervalid)
        {
            if (s.rngseed > 0)
            {
                hqrnd.hqrndseed(s.rngseed, s.rngseed + 235, s.rs, _params);
            }
            else
            {
                hqrnd.hqrndrandomize(s.rs, _params);
            }
        }

        //
        // Compute XXT for algorithms which need it
        //
        if (!s.arebasisandsolvervalid)
        {
            ap.assert(appendlen == 0, "SSA: integrity check failed / 34cx6");
            if (s.algotype == 2)
            {

                //
                // Compute X*X^T for direct algorithm.
                // Quite straightforward, no subtle optimizations.
                //
                apserv.rmatrixsetlengthatleast(ref s.xxt, winw, winw, _params);
                windowstotal = 0;
                for (i = 0; i <= s.nsequences - 1; i++)
                {
                    windowstotal = windowstotal + Math.Max(s.sequenceidx[i + 1] - s.sequenceidx[i] - winw + 1, 0);
                }
                ap.assert(windowstotal > 0, "SSA: integrity check in UpdateBasis() failed / 76t34");
                for (i = 0; i <= winw - 1; i++)
                {
                    for (j = 0; j <= winw - 1; j++)
                    {
                        s.xxt[i, j] = 0;
                    }
                }
                updatexxtprepare(s, windowstotal, winw, s.memorylimit, _params);
                for (i = 0; i <= s.nsequences - 1; i++)
                {
                    for (j = 0; j <= Math.Max(s.sequenceidx[i + 1] - s.sequenceidx[i] - winw + 1, 0) - 1; j++)
                    {
                        updatexxtsend(s, s.sequencedata, s.sequenceidx[i] + j, s.xxt, _params);
                    }
                }
                updatexxtfinalize(s, s.xxt, _params);
            }
            if (s.algotype == 3)
            {

                //
                // Compute X*X^T for real-time algorithm:
                // * prepare queue of windows to merge into XXT
                // * shuffle queue in order to avoid time-related biases in algorithm
                // * dequeue first chunk
                //
                apserv.rmatrixsetlengthatleast(ref s.xxt, winw, winw, _params);
                windowstotal = 0;
                for (i = 0; i <= s.nsequences - 1; i++)
                {
                    windowstotal = windowstotal + Math.Max(s.sequenceidx[i + 1] - s.sequenceidx[i] - winw + 1, 0);
                }
                ap.assert(windowstotal > 0, "SSA: integrity check in UpdateBasis() failed / 76t34");
                apserv.ivectorsetlengthatleast(ref s.rtqueue, windowstotal, _params);
                dstoffs = 0;
                for (i = 0; i <= s.nsequences - 1; i++)
                {
                    for (j = 0; j <= Math.Max(s.sequenceidx[i + 1] - s.sequenceidx[i] - winw + 1, 0) - 1; j++)
                    {
                        srcoffs = s.sequenceidx[i] + j;
                        s.rtqueue[dstoffs] = srcoffs;
                        apserv.inc(ref dstoffs, _params);
                    }
                }
                ap.assert(dstoffs == windowstotal, "SSA: integrity check in UpdateBasis() failed / fh45f");
                if (s.rtpowerup > 1)
                {

                    //
                    // Shuffle queue, it helps to avoid time-related bias in algorithm
                    //
                    for (i = 0; i <= windowstotal - 1; i++)
                    {
                        j = i + hqrnd.hqrnduniformi(s.rs, windowstotal - i, _params);
                        apserv.swapelementsi(s.rtqueue, i, j, _params);
                    }
                }
                s.rtqueuecnt = windowstotal;
                s.rtqueuechunk = 1;
                s.rtqueuechunk = Math.Max(s.rtqueuechunk, s.rtqueuecnt / s.rtpowerup);
                s.rtqueuechunk = Math.Max(s.rtqueuechunk, 2 * s.topk);
                realtimedequeue(s, 0.0, Math.Min(s.rtqueuechunk, s.rtqueuecnt), _params);
            }
        }

        //
        // Handle possible updates for XXT:
        // * check that append involves either last point of last sequence,
        //   or entire last sequence
        // * if last sequence is shorter than window width, perform quick exit -
        //   we have nothing to update - no windows to insert into XXT
        // * update XXT
        //
        if (appendlen > 0)
        {
            ap.assert(s.arebasisandsolvervalid, "SSA: integrity check failed / 5gvz3");
            ap.assert(s.nsequences >= 1, "SSA: integrity check failed / 658ev");
            ap.assert(appendlen == 1 || appendlen == s.sequenceidx[s.nsequences] - s.sequenceidx[s.nsequences - 1] - winw + 1, "SSA: integrity check failed / sd3g7");
            if (s.sequenceidx[s.nsequences] - s.sequenceidx[s.nsequences - 1] < winw)
            {

                //
                // Last sequence is too short, nothing to update
                //
                return;
            }
            if (s.algotype == 2 || s.algotype == 3)
            {
                if (appendlen > 1)
                {

                    //
                    // Long append, use GEMM for updates
                    //
                    updatexxtprepare(s, appendlen, winw, s.memorylimit, _params);
                    for (j = 0; j <= Math.Max(s.sequenceidx[s.nsequences] - s.sequenceidx[s.nsequences - 1] - winw + 1, 0) - 1; j++)
                    {
                        updatexxtsend(s, s.sequencedata, s.sequenceidx[s.nsequences - 1] + j, s.xxt, _params);
                    }
                    updatexxtfinalize(s, s.xxt, _params);
                }
                else
                {

                    //
                    // Just one element is added, use rank-1 update
                    //
                    ablas.rmatrixger(winw, winw, s.xxt, 0, 0, 1.0, s.sequencedata, s.sequenceidx[s.nsequences] - winw, s.sequencedata, s.sequenceidx[s.nsequences] - winw, _params);
                }
            }
        }

        //
        // Now, perform basis calculation - either full recalculation (AppendLen=0)
        // or quick update (AppendLen>0).
        //
        if (s.algotype == 1)
        {

            //
            // Precomputed basis
            //
            if (winw != s.precomputedwidth)
            {

                //
                // Window width has changed, reset basis to zeros
                //
                s.nbasis = 1;
                apserv.rmatrixsetlengthatleast(ref s.basis, winw, 1, _params);
                apserv.rvectorsetlengthatleast(ref s.sv, 1, _params);
                for (i = 0; i <= winw - 1; i++)
                {
                    s.basis[i, 0] = 0.0;
                }
                s.sv[0] = 0.0;
            }
            else
            {

                //
                // OK, use precomputed basis
                //
                s.nbasis = s.precomputednbasis;
                apserv.rmatrixsetlengthatleast(ref s.basis, winw, s.nbasis, _params);
                apserv.rvectorsetlengthatleast(ref s.sv, s.nbasis, _params);
                for (j = 0; j <= s.nbasis - 1; j++)
                {
                    s.sv[j] = 0.0;
                    for (i = 0; i <= winw - 1; i++)
                    {
                        s.basis[i, j] = s.precomputedbasis[i, j];
                    }
                }
            }
            apserv.rmatrixsetlengthatleast(ref s.basist, s.nbasis, winw, _params);
            ablas.rmatrixtranspose(winw, s.nbasis, s.basis, 0, 0, s.basist, 0, 0, _params);
        }
        else
        {
            if (s.algotype == 2)
            {

                //
                // Direct top-K algorithm
                //
                // Calculate eigenvectors with SMatrixEVD(), reorder by descending
                // of magnitudes.
                //
                // Update is performed for invalid basis or for non-zero UpdateIts.
                //
                needevd = !s.arebasisandsolvervalid;
                needevd = needevd || (double)(updateits) >= (double)(1);
                needevd = needevd || (double)(hqrnd.hqrnduniformr(s.rs, _params)) < (double)(updateits - (int)Math.Floor(updateits));
                if (needevd)
                {
                    apserv.inc(ref s.dbgcntevd, _params);
                    s.nbasis = Math.Min(winw, s.topk);
                    if (!evd.smatrixevd(s.xxt, winw, 1, true, ref s.sv, ref s.basis, _params))
                    {
                        ap.assert(false, "SSA: SMatrixEVD failed");
                    }
                    for (i = 0; i <= winw - 1; i++)
                    {
                        k = winw - 1 - i;
                        if (i >= k)
                        {
                            break;
                        }
                        v = s.sv[i];
                        s.sv[i] = s.sv[k];
                        s.sv[k] = v;
                        for (j = 0; j <= winw - 1; j++)
                        {
                            v = s.basis[j, i];
                            s.basis[j, i] = s.basis[j, k];
                            s.basis[j, k] = v;
                        }
                    }
                    for (i = 0; i <= s.nbasis - 1; i++)
                    {
                        s.sv[i] = Math.Sqrt(Math.Max(s.sv[i], 0.0));
                    }
                    apserv.rmatrixsetlengthatleast(ref s.basist, s.nbasis, winw, _params);
                    ablas.rmatrixtranspose(winw, s.nbasis, s.basis, 0, 0, s.basist, 0, 0, _params);
                }
            }
            else
            {
                if (s.algotype == 3)
                {

                    //
                    // Real-time top-K.
                    //
                    // Determine actual number of basis components, prepare subspace
                    // solver (either create from scratch or reuse).
                    //
                    // Update is always performed for invalid basis; for a valid basis
                    // it is performed with probability UpdateIts.
                    //
                    if (s.rtpowerup == 1)
                    {
                        subspaceits = s.defaultsubspaceits;
                    }
                    else
                    {
                        subspaceits = 3;
                    }
                    if (appendlen > 0)
                    {
                        ap.assert(s.arebasisandsolvervalid, "SSA: integrity check in UpdateBasis() failed / srg6f");
                        ap.assert((double)(updateits) >= (double)(0), "SSA: integrity check in UpdateBasis() failed / srg4f");
                        subspaceits = (int)Math.Floor(updateits);
                        if ((double)(hqrnd.hqrnduniformr(s.rs, _params)) < (double)(updateits - (int)Math.Floor(updateits)))
                        {
                            apserv.inc(ref subspaceits, _params);
                        }
                        ap.assert(subspaceits >= 0, "SSA: integrity check in UpdateBasis() failed / srg9f");
                    }

                    //
                    // Dequeue pending dataset and merge it into XXT.
                    //
                    // Dequeuing is done only for appends, and only when we have
                    // non-empty queue.
                    //
                    if (appendlen > 0 && s.rtqueuecnt > 0)
                    {
                        realtimedequeue(s, 1.0, Math.Min(s.rtqueuechunk, s.rtqueuecnt), _params);
                    }

                    //
                    // Now, proceed to solver
                    //
                    if (subspaceits > 0)
                    {
                        if (appendlen == 0)
                        {
                            s.nbasis = Math.Min(winw, s.topk);
                            evd.eigsubspacecreatebuf(winw, s.nbasis, s.solver, _params);
                        }
                        else
                        {
                            evd.eigsubspacesetwarmstart(s.solver, true, _params);
                        }
                        evd.eigsubspacesetcond(s.solver, 0.0, subspaceits, _params);

                        //
                        // Perform initial basis estimation
                        //
                        apserv.inc(ref s.dbgcntevd, _params);
                        evd.eigsubspaceoocstart(s.solver, 0, _params);
                        while (evd.eigsubspaceooccontinue(s.solver, _params))
                        {
                            evd.eigsubspaceoocgetrequestinfo(s.solver, ref requesttype, ref requestsize, _params);
                            ap.assert(requesttype == 0, "SSA: integrity check in UpdateBasis() failed / 346372");
                            ablas.rmatrixgemm(winw, requestsize, winw, 1.0, s.xxt, 0, 0, 0, s.solver.x, 0, 0, 0, 0.0, s.solver.ax, 0, 0, _params);
                        }
                        evd.eigsubspaceoocstop(s.solver, ref s.sv, ref s.basis, s.solverrep, _params);
                        for (i = 0; i <= s.nbasis - 1; i++)
                        {
                            s.sv[i] = Math.Sqrt(Math.Max(s.sv[i], 0.0));
                        }
                        apserv.rmatrixsetlengthatleast(ref s.basist, s.nbasis, winw, _params);
                        ablas.rmatrixtranspose(winw, s.nbasis, s.basis, 0, 0, s.basist, 0, 0, _params);
                    }
                }
                else
                {
                    ap.assert(false, "SSA: integrity check in UpdateBasis() failed / dfgs34");
                }
            }
        }

        //
        // Update recurrent relation
        //
        apserv.rvectorsetlengthatleast(ref s.forecasta, Math.Max(winw - 1, 1), _params);
        degeneraterecurrence = false;
        if (winw > 1)
        {

            //
            // Non-degenerate case
            //
            apserv.rvectorsetlengthatleast(ref s.tmp0, s.nbasis, _params);
            nu2 = 0.0;
            for (i = 0; i <= s.nbasis - 1; i++)
            {
                v = s.basist[i, winw - 1];
                s.tmp0[i] = v;
                nu2 = nu2 + v * v;
            }
            if ((double)(nu2) < (double)(1 - 1000 * math.machineepsilon))
            {
                ablas.rmatrixgemv(winw - 1, s.nbasis, 1 / (1 - nu2), s.basist, 0, 0, 1, s.tmp0, 0, 0.0, s.forecasta, 0, _params);
            }
            else
            {
                degeneraterecurrence = true;
            }
        }
        else
        {
            degeneraterecurrence = true;
        }
        if (degeneraterecurrence)
        {
            for (i = 0; i <= Math.Max(winw - 1, 1) - 1; i++)
            {
                s.forecasta[i] = 0.0;
            }
            s.forecasta[Math.Max(winw - 1, 1) - 1] = 1.0;
        }

        //
        // Set validity flag
        //
        s.arebasisandsolvervalid = true;
    }


    /*************************************************************************
    This function performs analysis using current basis. It assumes and checks
    that validity flag AreBasisAndSolverValid is set.

    INPUT PARAMETERS:
        S                   -   model
        Data                -   array which holds data in elements [I0,I1):
                                * right bound is not included.
                                * I1-I0>=WindowWidth (assertion is performed).
        Trend               -   preallocated output array, large enough
        Noise               -   preallocated output array, large enough
        Offs                -   offset in Trend/Noise where result is stored;
                                I1-I0 elements are written starting at offset
                                Offs.
                                
    OUTPUT PARAMETERS:
        Trend, Noise - processing results
                                

      -- ALGLIB --
         Copyright 30.10.2017 by Bochkanov Sergey
    *************************************************************************/
    private static void analyzesequence(ssamodel s,
        double[] data,
        int i0,
        int i1,
        double[] trend,
        double[] noise,
        int offs,
        xparams _params)
    {
        int winw = 0;
        int nwindows = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        int cnt = 0;
        int batchstart = 0;
        int batchlimit = 0;
        int batchsize = 0;

        ap.assert(s.arebasisandsolvervalid, "AnalyzeSequence: integrity check failed / d84sz0");
        ap.assert(i1 - i0 >= s.windowwidth, "AnalyzeSequence: integrity check failed / d84sz1");
        ap.assert(s.nbasis >= 1, "AnalyzeSequence: integrity check failed / d84sz2");
        nwindows = i1 - i0 - s.windowwidth + 1;
        winw = s.windowwidth;
        batchlimit = Math.Max(nwindows, 1);
        if (s.memorylimit > 0)
        {
            batchlimit = Math.Min(batchlimit, Math.Max(s.memorylimit / winw, 4 * winw));
        }

        //
        // Zero-initialize trend and counts
        //
        cnt = i1 - i0;
        apserv.ivectorsetlengthatleast(ref s.aseqcounts, cnt, _params);
        for (i = 0; i <= cnt - 1; i++)
        {
            s.aseqcounts[i] = 0;
            trend[offs + i] = 0.0;
        }

        //
        // Reset temporaries if algorithm settings changed since last round
        //
        if (ap.cols(s.aseqtrajectory) != winw)
        {
            s.aseqtrajectory = new double[0, 0];
        }
        if (ap.cols(s.aseqtbproduct) != s.nbasis)
        {
            s.aseqtbproduct = new double[0, 0];
        }

        //
        // Perform batch processing
        //
        apserv.rmatrixsetlengthatleast(ref s.aseqtrajectory, batchlimit, winw, _params);
        apserv.rmatrixsetlengthatleast(ref s.aseqtbproduct, batchlimit, s.nbasis, _params);
        batchsize = 0;
        batchstart = offs;
        for (i = 0; i <= nwindows - 1; i++)
        {

            //
            // Enqueue next row of trajectory matrix
            //
            if (batchsize == 0)
            {
                batchstart = i;
            }
            for (j = 0; j <= winw - 1; j++)
            {
                s.aseqtrajectory[batchsize, j] = data[i0 + i + j];
            }
            apserv.inc(ref batchsize, _params);

            //
            // Process batch
            //
            if (batchsize == batchlimit || i == nwindows - 1)
            {

                //
                // Project onto basis
                //
                ablas.rmatrixgemm(batchsize, s.nbasis, winw, 1.0, s.aseqtrajectory, 0, 0, 0, s.basist, 0, 0, 1, 0.0, s.aseqtbproduct, 0, 0, _params);
                ablas.rmatrixgemm(batchsize, winw, s.nbasis, 1.0, s.aseqtbproduct, 0, 0, 0, s.basist, 0, 0, 0, 0.0, s.aseqtrajectory, 0, 0, _params);

                //
                // Hankelize
                //
                for (k = 0; k <= batchsize - 1; k++)
                {
                    for (j = 0; j <= winw - 1; j++)
                    {
                        trend[offs + batchstart + k + j] = trend[offs + batchstart + k + j] + s.aseqtrajectory[k, j];
                        s.aseqcounts[batchstart + k + j] = s.aseqcounts[batchstart + k + j] + 1;
                    }
                }

                //
                // Reset batch size
                //
                batchsize = 0;
            }
        }
        for (i = 0; i <= cnt - 1; i++)
        {
            trend[offs + i] = trend[offs + i] / s.aseqcounts[i];
        }

        //
        // Output noise
        //
        for (i = 0; i <= cnt - 1; i++)
        {
            noise[offs + i] = data[i0 + i] - trend[offs + i];
        }
    }


    /*************************************************************************
    This function performs  averaged  forecasting.  It  assumes  that basis is
    already built, everything is valid and checked. See  comments  on  similar
    public functions to find out more about averaged predictions.

    INPUT PARAMETERS:
        S                   -   model
        Data                -   array which holds data in elements [I0,I1):
                                * right bound is not included.
                                * I1-I0>=WindowWidth (assertion is performed).
        M                   -   number  of  sliding  windows  to combine, M>=1. If
                                your dataset has less than M sliding windows, this
                                parameter will be silently reduced.
        ForecastLen         -   number of ticks to predict, ForecastLen>=1
        Trend               -   preallocated output array, large enough
        Offs                -   offset in Trend where result is stored;
                                I1-I0 elements are written starting at offset
                                Offs.

    OUTPUT PARAMETERS:
        Trend           -   array[ForecastLen], forecasted trend

      -- ALGLIB --
         Copyright 30.10.2017 by Bochkanov Sergey
    *************************************************************************/
    private static void forecastavgsequence(ssamodel s,
        double[] data,
        int i0,
        int i1,
        int m,
        int forecastlen,
        bool smooth,
        double[] trend,
        int offs,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        int winw = 0;

        ap.assert(s.arebasisandsolvervalid, "ForecastAvgSequence: integrity check failed / d84sz0");
        ap.assert(i1 - i0 - s.windowwidth + 1 >= m, "ForecastAvgSequence: integrity check failed / d84sz1");
        ap.assert(s.nbasis >= 1, "ForecastAvgSequence: integrity check failed / d84sz2");
        ap.assert(s.windowwidth >= 2, "ForecastAvgSequence: integrity check failed / 5tgdg5");
        ap.assert(s.windowwidth > s.nbasis, "ForecastAvgSequence: integrity check failed / d5g56w");
        winw = s.windowwidth;

        //
        // Prepare M synchronized predictions for the last known tick
        // (last one is an actual value of the trend, previous M-1 predictions
        // are predictions from differently positioned sliding windows).
        //
        apserv.rmatrixsetlengthatleast(ref s.fctrendm, m, winw, _params);
        apserv.rvectorsetlengthatleast(ref s.tmp0, Math.Max(m, s.nbasis), _params);
        apserv.rvectorsetlengthatleast(ref s.tmp1, winw, _params);
        for (k = 0; k <= m - 1; k++)
        {

            //
            // Perform prediction for rows [0,K-1]
            //
            ablas.rmatrixgemv(k, winw - 1, 1.0, s.fctrendm, 0, 1, 0, s.forecasta, 0, 0.0, s.tmp0, 0, _params);
            for (i = 0; i <= k - 1; i++)
            {
                for (j = 1; j <= winw - 1; j++)
                {
                    s.fctrendm[i, j - 1] = s.fctrendm[i, j];
                }
                s.fctrendm[i, winw - 1] = s.tmp0[i];
            }

            //
            // Perform trend extraction for row K, add it to dataset
            //
            if (smooth)
            {
                ablas.rmatrixgemv(s.nbasis, winw, 1.0, s.basist, 0, 0, 0, data, i1 - winw - (m - 1 - k), 0.0, s.tmp0, 0, _params);
                ablas.rmatrixgemv(s.windowwidth, s.nbasis, 1.0, s.basis, 0, 0, 0, s.tmp0, 0, 0.0, s.tmp1, 0, _params);
                for (j = 0; j <= winw - 1; j++)
                {
                    s.fctrendm[k, j] = s.tmp1[j];
                }
            }
            else
            {
                for (j = 0; j <= winw - 1; j++)
                {
                    s.fctrendm[k, j] = data[i1 - winw - (m - 1 - k) + j];
                }
            }
        }

        //
        // Now we have M synchronized predictions of the sequence state at the last
        // know moment (last "prediction" is just a copy of the trend). Let's start
        // batch prediction!
        //
        for (k = 0; k <= forecastlen - 1; k++)
        {
            ablas.rmatrixgemv(m, winw - 1, 1.0, s.fctrendm, 0, 1, 0, s.forecasta, 0, 0.0, s.tmp0, 0, _params);
            trend[offs + k] = 0.0;
            for (i = 0; i <= m - 1; i++)
            {
                for (j = 1; j <= winw - 1; j++)
                {
                    s.fctrendm[i, j - 1] = s.fctrendm[i, j];
                }
                s.fctrendm[i, winw - 1] = s.tmp0[i];
                trend[offs + k] = trend[offs + k] + s.tmp0[i];
            }
            trend[offs + k] = trend[offs + k] / m;
        }
    }


    /*************************************************************************
    This function extracts updates from real-time queue and  applies  them  to
    the S.XXT matrix. XXT is premultiplied by  Beta,  which  can  be  0.0  for
    initial creation, 1.0 for subsequent updates, or even within (0,1) for some
    kind of updates with decay.

    INPUT PARAMETERS:
        S                   -   model
        Beta                -   >=0, coefficient to premultiply XXT
        Cnt                 -   0<Cnt<=S.RTQueueCnt, number of updates to extract
                                from the end of the queue
                                
    OUTPUT PARAMETERS:
        S                   -   S.XXT updated, S.RTQueueCnt decreased

      -- ALGLIB --
         Copyright 30.10.2017 by Bochkanov Sergey
    *************************************************************************/
    private static void realtimedequeue(ssamodel s,
        double beta,
        int cnt,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int winw = 0;

        ap.assert(cnt > 0, "SSA: RealTimeDequeue() integrity check failed / 43tdv");
        ap.assert(math.isfinite(beta) && (double)(beta) >= (double)(0), "SSA: RealTimeDequeue() integrity check failed / 5gdg6");
        ap.assert(cnt <= s.rtqueuecnt, "SSA: RealTimeDequeue() integrity check failed / 547yh");
        ap.assert(ap.cols(s.xxt) >= s.windowwidth, "SSA: RealTimeDequeue() integrity check failed / 54bf4");
        ap.assert(ap.rows(s.xxt) >= s.windowwidth, "SSA: RealTimeDequeue() integrity check failed / 9gdfn");
        winw = s.windowwidth;

        //
        // Premultiply XXT by Beta
        //
        if ((double)(beta) != (double)(0))
        {
            for (i = 0; i <= winw - 1; i++)
            {
                for (j = 0; j <= winw - 1; j++)
                {
                    s.xxt[i, j] = s.xxt[i, j] * beta;
                }
            }
        }
        else
        {
            for (i = 0; i <= winw - 1; i++)
            {
                for (j = 0; j <= winw - 1; j++)
                {
                    s.xxt[i, j] = 0;
                }
            }
        }

        //
        // Dequeue
        //
        updatexxtprepare(s, cnt, winw, s.memorylimit, _params);
        for (i = 0; i <= cnt - 1; i++)
        {
            updatexxtsend(s, s.sequencedata, s.rtqueue[s.rtqueuecnt - 1], s.xxt, _params);
            apserv.dec(ref s.rtqueuecnt, _params);
        }
        updatexxtfinalize(s, s.xxt, _params);
    }


    /*************************************************************************
    This function prepares batch buffer for XXT update. The idea  is  that  we
    send a stream of "XXT += u*u'" updates, and we want to package  them  into
    one big matrix update U*U', applied with SYRK() kernel, but U can  consume
    too much memory, so we want to transparently divide it  into  few  smaller
    chunks.

    This set of functions solves this problem:
    * UpdateXXTPrepare() prepares temporary buffers
    * UpdateXXTSend() sends next u to the buffer, possibly initiating next SYRK()
    * UpdateXXTFinalize() performs last SYRK() update

    INPUT PARAMETERS:
        S                   -   model, only fields with UX prefix are used
        UpdateSize          -   number of updates
        WindowWidth         -   window width, >0
        MemoryLimit         -   memory limit, non-positive value means no limit
                                
    OUTPUT PARAMETERS:
        S                   -   UX temporaries updated

      -- ALGLIB --
         Copyright 20.12.2017 by Bochkanov Sergey
    *************************************************************************/
    private static void updatexxtprepare(ssamodel s,
        int updatesize,
        int windowwidth,
        int memorylimit,
        xparams _params)
    {
        ap.assert(windowwidth > 0, "UpdateXXTPrepare: WinW<=0");
        s.uxbatchlimit = Math.Max(updatesize, 1);
        if (memorylimit > 0)
        {
            s.uxbatchlimit = Math.Min(s.uxbatchlimit, Math.Max(memorylimit / windowwidth, 4 * windowwidth));
        }
        s.uxbatchwidth = windowwidth;
        s.uxbatchsize = 0;
        if (ap.cols(s.uxbatch) != windowwidth)
        {
            s.uxbatch = new double[0, 0];
        }
        apserv.rmatrixsetlengthatleast(ref s.uxbatch, s.uxbatchlimit, windowwidth, _params);
    }


    /*************************************************************************
    This function sends update u*u' to the batch buffer.

    INPUT PARAMETERS:
        S                   -   model, only fields with UX prefix are used
        U                   -   WindowWidth-sized update, starts at I0
        I0                  -   starting position for update
                                
    OUTPUT PARAMETERS:
        S                   -   UX temporaries updated
        XXT                 -   array[WindowWidth,WindowWidth], in the middle
                                of update. All intermediate updates are
                                applied to the upper triangle.

      -- ALGLIB --
         Copyright 20.12.2017 by Bochkanov Sergey
    *************************************************************************/
    private static void updatexxtsend(ssamodel s,
        double[] u,
        int i0,
        double[,] xxt,
        xparams _params)
    {
        int i_ = 0;
        int i1_ = 0;

        ap.assert(i0 + s.uxbatchwidth - 1 < ap.len(u), "UpdateXXTSend: incorrect U size");
        ap.assert(s.uxbatchsize >= 0, "UpdateXXTSend: integrity check failure");
        ap.assert(s.uxbatchsize <= s.uxbatchlimit, "UpdateXXTSend: integrity check failure");
        ap.assert(s.uxbatchlimit >= 1, "UpdateXXTSend: integrity check failure");

        //
        // Send pending batch if full
        //
        if (s.uxbatchsize == s.uxbatchlimit)
        {
            ablas.rmatrixsyrk(s.uxbatchwidth, s.uxbatchsize, 1.0, s.uxbatch, 0, 0, 2, 1.0, xxt, 0, 0, true, _params);
            s.uxbatchsize = 0;
        }

        //
        // Append update to batch
        //
        i1_ = (i0) - (0);
        for (i_ = 0; i_ <= s.uxbatchwidth - 1; i_++)
        {
            s.uxbatch[s.uxbatchsize, i_] = u[i_ + i1_];
        }
        apserv.inc(ref s.uxbatchsize, _params);
    }


    /*************************************************************************
    This function finalizes batch buffer. Call it after the last update.

    INPUT PARAMETERS:
        S                   -   model, only fields with UX prefix are used
                                
    OUTPUT PARAMETERS:
        S                   -   UX temporaries updated
        XXT                 -   array[WindowWidth,WindowWidth], updated with
                                all previous updates, both triangles of the
                                symmetric matrix are present.

      -- ALGLIB --
         Copyright 20.12.2017 by Bochkanov Sergey
    *************************************************************************/
    private static void updatexxtfinalize(ssamodel s,
        double[,] xxt,
        xparams _params)
    {
        ap.assert(s.uxbatchsize >= 0, "UpdateXXTFinalize: integrity check failure");
        ap.assert(s.uxbatchsize <= s.uxbatchlimit, "UpdateXXTFinalize: integrity check failure");
        ap.assert(s.uxbatchlimit >= 1, "UpdateXXTFinalize: integrity check failure");
        if (s.uxbatchsize > 0)
        {
            ablas.rmatrixsyrk(s.uxbatchwidth, s.uxbatchsize, 1.0, s.uxbatch, 0, 0, 2, 1.0, s.xxt, 0, 0, true, _params);
            s.uxbatchsize = 0;
        }
        ablas.rmatrixenforcesymmetricity(s.xxt, s.uxbatchwidth, true, _params);
    }


}
