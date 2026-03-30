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

public class mcpd
{
    /*************************************************************************
    This structure is a MCPD (Markov Chains for Population Data) solver.

    You should use ALGLIB functions in order to work with this object.

      -- ALGLIB --
         Copyright 23.05.2010 by Bochkanov Sergey
    *************************************************************************/
    public class mcpdstate : apobject
    {
        public int n;
        public int[] states;
        public int npairs;
        public double[,] data;
        public double[,] ec;
        public double[,] bndl;
        public double[,] bndu;
        public double[,] c;
        public int[] ct;
        public int ccnt;
        public double[] pw;
        public double[,] priorp;
        public double regterm;
        public minbleic.minbleicstate bs;
        public int repinneriterationscount;
        public int repouteriterationscount;
        public int repnfev;
        public int repterminationtype;
        public minbleic.minbleicreport br;
        public double[] tmpp;
        public double[] effectivew;
        public double[] effectivebndl;
        public double[] effectivebndu;
        public double[,] effectivec;
        public int[] effectivect;
        public double[] h;
        public double[,] p;
        public mcpdstate()
        {
            init();
        }
        public override void init()
        {
            states = new int[0];
            data = new double[0, 0];
            ec = new double[0, 0];
            bndl = new double[0, 0];
            bndu = new double[0, 0];
            c = new double[0, 0];
            ct = new int[0];
            pw = new double[0];
            priorp = new double[0, 0];
            bs = new minbleic.minbleicstate();
            br = new minbleic.minbleicreport();
            tmpp = new double[0];
            effectivew = new double[0];
            effectivebndl = new double[0];
            effectivebndu = new double[0];
            effectivec = new double[0, 0];
            effectivect = new int[0];
            h = new double[0];
            p = new double[0, 0];
        }
        public override apobject make_copy()
        {
            mcpdstate _result = new mcpdstate();
            _result.n = n;
            _result.states = (int[])states.Clone();
            _result.npairs = npairs;
            _result.data = (double[,])data.Clone();
            _result.ec = (double[,])ec.Clone();
            _result.bndl = (double[,])bndl.Clone();
            _result.bndu = (double[,])bndu.Clone();
            _result.c = (double[,])c.Clone();
            _result.ct = (int[])ct.Clone();
            _result.ccnt = ccnt;
            _result.pw = (double[])pw.Clone();
            _result.priorp = (double[,])priorp.Clone();
            _result.regterm = regterm;
            _result.bs = (minbleic.minbleicstate)bs.make_copy();
            _result.repinneriterationscount = repinneriterationscount;
            _result.repouteriterationscount = repouteriterationscount;
            _result.repnfev = repnfev;
            _result.repterminationtype = repterminationtype;
            _result.br = (minbleic.minbleicreport)br.make_copy();
            _result.tmpp = (double[])tmpp.Clone();
            _result.effectivew = (double[])effectivew.Clone();
            _result.effectivebndl = (double[])effectivebndl.Clone();
            _result.effectivebndu = (double[])effectivebndu.Clone();
            _result.effectivec = (double[,])effectivec.Clone();
            _result.effectivect = (int[])effectivect.Clone();
            _result.h = (double[])h.Clone();
            _result.p = (double[,])p.Clone();
            return _result;
        }
    };


    /*************************************************************************
    This structure is a MCPD training report:
        InnerIterationsCount    -   number of inner iterations of the
                                    underlying optimization algorithm
        OuterIterationsCount    -   number of outer iterations of the
                                    underlying optimization algorithm
        NFEV                    -   number of merit function evaluations
        TerminationType         -   termination type
                                    (same as for MinBLEIC optimizer, positive
                                    values denote success, negative ones -
                                    failure)

      -- ALGLIB --
         Copyright 23.05.2010 by Bochkanov Sergey
    *************************************************************************/
    public class mcpdreport : apobject
    {
        public int inneriterationscount;
        public int outeriterationscount;
        public int nfev;
        public int terminationtype;
        public mcpdreport()
        {
            init();
        }
        public override void init()
        {
        }
        public override apobject make_copy()
        {
            mcpdreport _result = new mcpdreport();
            _result.inneriterationscount = inneriterationscount;
            _result.outeriterationscount = outeriterationscount;
            _result.nfev = nfev;
            _result.terminationtype = terminationtype;
            return _result;
        }
    };




    public const double xtol = 1.0E-8;


    /*************************************************************************
    DESCRIPTION:

    This function creates MCPD (Markov Chains for Population Data) solver.

    This  solver  can  be  used  to find transition matrix P for N-dimensional
    prediction  problem  where transition from X[i] to X[i+1] is  modelled  as
        X[i+1] = P*X[i]
    where X[i] and X[i+1] are N-dimensional population vectors (components  of
    each X are non-negative), and P is a N*N transition matrix (elements of  P
    are non-negative, each column sums to 1.0).

    Such models arise when when:
    * there is some population of individuals
    * individuals can have different states
    * individuals can transit from one state to another
    * population size is constant, i.e. there is no new individuals and no one
      leaves population
    * you want to model transitions of individuals from one state into another

    USAGE:

    Here we give very brief outline of the MCPD. We strongly recommend you  to
    read examples in the ALGLIB Reference Manual and to read ALGLIB User Guide
    on data analysis which is available at http://www.net/dataanalysis/

    1. User initializes algorithm state with MCPDCreate() call

    2. User  adds  one  or  more  tracks -  sequences of states which describe
       evolution of a system being modelled from different starting conditions

    3. User may add optional boundary, equality  and/or  linear constraints on
       the coefficients of P by calling one of the following functions:
       * MCPDSetEC() to set equality constraints
       * MCPDSetBC() to set bound constraints
       * MCPDSetLC() to set linear constraints

    4. Optionally,  user  may  set  custom  weights  for prediction errors (by
       default, algorithm assigns non-equal, automatically chosen weights  for
       errors in the prediction of different components of X). It can be  done
       with a call of MCPDSetPredictionWeights() function.

    5. User calls MCPDSolve() function which takes algorithm  state and
       pointer (delegate, etc.) to callback function which calculates F/G.

    6. User calls MCPDResults() to get solution

    INPUT PARAMETERS:
        N       -   problem dimension, N>=1

    OUTPUT PARAMETERS:
        State   -   structure stores algorithm state

      -- ALGLIB --
         Copyright 23.05.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void mcpdcreate(int n,
        mcpdstate s,
        xparams _params)
    {
        ap.assert(n >= 1, "MCPDCreate: N<1");
        mcpdinit(n, -1, -1, s, _params);
    }


    /*************************************************************************
    DESCRIPTION:

    This function is a specialized version of MCPDCreate()  function,  and  we
    recommend  you  to read comments for this function for general information
    about MCPD solver.

    This  function  creates  MCPD (Markov Chains for Population  Data)  solver
    for "Entry-state" model,  i.e. model  where transition from X[i] to X[i+1]
    is modelled as
        X[i+1] = P*X[i]
    where
        X[i] and X[i+1] are N-dimensional state vectors
        P is a N*N transition matrix
    and  one  selected component of X[] is called "entry" state and is treated
    in a special way:
        system state always transits from "entry" state to some another state
        system state can not transit from any state into "entry" state
    Such conditions basically mean that row of P which corresponds to  "entry"
    state is zero.

    Such models arise when:
    * there is some population of individuals
    * individuals can have different states
    * individuals can transit from one state to another
    * population size is NOT constant -  at every moment of time there is some
      (unpredictable) amount of "new" individuals, which can transit into  one
      of the states at the next turn, but still no one leaves population
    * you want to model transitions of individuals from one state into another
    * but you do NOT want to predict amount of "new"  individuals  because  it
      does not depends on individuals already present (hence  system  can  not
      transit INTO entry state - it can only transit FROM it).

    This model is discussed  in  more  details  in  the ALGLIB User Guide (see
    http://www.net/dataanalysis/ for more data).

    INPUT PARAMETERS:
        N       -   problem dimension, N>=2
        EntryState- index of entry state, in 0..N-1

    OUTPUT PARAMETERS:
        State   -   structure stores algorithm state

      -- ALGLIB --
         Copyright 23.05.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void mcpdcreateentry(int n,
        int entrystate,
        mcpdstate s,
        xparams _params)
    {
        ap.assert(n >= 2, "MCPDCreateEntry: N<2");
        ap.assert(entrystate >= 0, "MCPDCreateEntry: EntryState<0");
        ap.assert(entrystate < n, "MCPDCreateEntry: EntryState>=N");
        mcpdinit(n, entrystate, -1, s, _params);
    }


    /*************************************************************************
    DESCRIPTION:

    This function is a specialized version of MCPDCreate()  function,  and  we
    recommend  you  to read comments for this function for general information
    about MCPD solver.

    This  function  creates  MCPD (Markov Chains for Population  Data)  solver
    for "Exit-state" model,  i.e. model  where  transition from X[i] to X[i+1]
    is modelled as
        X[i+1] = P*X[i]
    where
        X[i] and X[i+1] are N-dimensional state vectors
        P is a N*N transition matrix
    and  one  selected component of X[] is called "exit"  state and is treated
    in a special way:
        system state can transit from any state into "exit" state
        system state can not transit from "exit" state into any other state
        transition operator discards "exit" state (makes it zero at each turn)
    Such  conditions  basically  mean  that  column  of P which corresponds to
    "exit" state is zero. Multiplication by such P may decrease sum of  vector
    components.

    Such models arise when:
    * there is some population of individuals
    * individuals can have different states
    * individuals can transit from one state to another
    * population size is NOT constant - individuals can move into "exit" state
      and leave population at the next turn, but there are no new individuals
    * amount of individuals which leave population can be predicted
    * you want to model transitions of individuals from one state into another
      (including transitions into the "exit" state)

    This model is discussed  in  more  details  in  the ALGLIB User Guide (see
    http://www.net/dataanalysis/ for more data).

    INPUT PARAMETERS:
        N       -   problem dimension, N>=2
        ExitState-  index of exit state, in 0..N-1

    OUTPUT PARAMETERS:
        State   -   structure stores algorithm state

      -- ALGLIB --
         Copyright 23.05.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void mcpdcreateexit(int n,
        int exitstate,
        mcpdstate s,
        xparams _params)
    {
        ap.assert(n >= 2, "MCPDCreateExit: N<2");
        ap.assert(exitstate >= 0, "MCPDCreateExit: ExitState<0");
        ap.assert(exitstate < n, "MCPDCreateExit: ExitState>=N");
        mcpdinit(n, -1, exitstate, s, _params);
    }


    /*************************************************************************
    DESCRIPTION:

    This function is a specialized version of MCPDCreate()  function,  and  we
    recommend  you  to read comments for this function for general information
    about MCPD solver.

    This  function  creates  MCPD (Markov Chains for Population  Data)  solver
    for "Entry-Exit-states" model, i.e. model where  transition  from  X[i] to
    X[i+1] is modelled as
        X[i+1] = P*X[i]
    where
        X[i] and X[i+1] are N-dimensional state vectors
        P is a N*N transition matrix
    one selected component of X[] is called "entry" state and is treated in  a
    special way:
        system state always transits from "entry" state to some another state
        system state can not transit from any state into "entry" state
    and another one component of X[] is called "exit" state and is treated  in
    a special way too:
        system state can transit from any state into "exit" state
        system state can not transit from "exit" state into any other state
        transition operator discards "exit" state (makes it zero at each turn)
    Such conditions basically mean that:
        row of P which corresponds to "entry" state is zero
        column of P which corresponds to "exit" state is zero
    Multiplication by such P may decrease sum of vector components.

    Such models arise when:
    * there is some population of individuals
    * individuals can have different states
    * individuals can transit from one state to another
    * population size is NOT constant
    * at every moment of time there is some (unpredictable)  amount  of  "new"
      individuals, which can transit into one of the states at the next turn
    * some  individuals  can  move  (predictably)  into "exit" state and leave
      population at the next turn
    * you want to model transitions of individuals from one state into another,
      including transitions from the "entry" state and into the "exit" state.
    * but you do NOT want to predict amount of "new"  individuals  because  it
      does not depends on individuals already present (hence  system  can  not
      transit INTO entry state - it can only transit FROM it).

    This model is discussed  in  more  details  in  the ALGLIB User Guide (see
    http://www.net/dataanalysis/ for more data).

    INPUT PARAMETERS:
        N       -   problem dimension, N>=2
        EntryState- index of entry state, in 0..N-1
        ExitState-  index of exit state, in 0..N-1

    OUTPUT PARAMETERS:
        State   -   structure stores algorithm state

      -- ALGLIB --
         Copyright 23.05.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void mcpdcreateentryexit(int n,
        int entrystate,
        int exitstate,
        mcpdstate s,
        xparams _params)
    {
        ap.assert(n >= 2, "MCPDCreateEntryExit: N<2");
        ap.assert(entrystate >= 0, "MCPDCreateEntryExit: EntryState<0");
        ap.assert(entrystate < n, "MCPDCreateEntryExit: EntryState>=N");
        ap.assert(exitstate >= 0, "MCPDCreateEntryExit: ExitState<0");
        ap.assert(exitstate < n, "MCPDCreateEntryExit: ExitState>=N");
        ap.assert(entrystate != exitstate, "MCPDCreateEntryExit: EntryState=ExitState");
        mcpdinit(n, entrystate, exitstate, s, _params);
    }


    /*************************************************************************
    This  function  is  used to add a track - sequence of system states at the
    different moments of its evolution.

    You  may  add  one  or several tracks to the MCPD solver. In case you have
    several tracks, they won't overwrite each other. For example,  if you pass
    two tracks, A1-A2-A3 (system at t=A+1, t=A+2 and t=A+3) and B1-B2-B3, then
    solver will try to model transitions from t=A+1 to t=A+2, t=A+2 to  t=A+3,
    t=B+1 to t=B+2, t=B+2 to t=B+3. But it WONT mix these two tracks - i.e. it
    wont try to model transition from t=A+3 to t=B+1.

    INPUT PARAMETERS:
        S       -   solver
        XY      -   track, array[K,N]:
                    * I-th row is a state at t=I
                    * elements of XY must be non-negative (exception will be
                      thrown on negative elements)
        K       -   number of points in a track
                    * if given, only leading K rows of XY are used
                    * if not given, automatically determined from size of XY

    NOTES:

    1. Track may contain either proportional or population data:
       * with proportional data all rows of XY must sum to 1.0, i.e. we have
         proportions instead of absolute population values
       * with population data rows of XY contain population counts and generally
         do not sum to 1.0 (although they still must be non-negative)

      -- ALGLIB --
         Copyright 23.05.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void mcpdaddtrack(mcpdstate s,
        double[,] xy,
        int k,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int n = 0;
        double s0 = 0;
        double s1 = 0;

        n = s.n;
        ap.assert(k >= 0, "MCPDAddTrack: K<0");
        ap.assert(ap.cols(xy) >= n, "MCPDAddTrack: Cols(XY)<N");
        ap.assert(ap.rows(xy) >= k, "MCPDAddTrack: Rows(XY)<K");
        ap.assert(apserv.apservisfinitematrix(xy, k, n, _params), "MCPDAddTrack: XY contains infinite or NaN elements");
        for (i = 0; i <= k - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                ap.assert((double)(xy[i, j]) >= (double)(0), "MCPDAddTrack: XY contains negative elements");
            }
        }
        if (k < 2)
        {
            return;
        }
        if (ap.rows(s.data) < s.npairs + k - 1)
        {
            apserv.rmatrixresize(ref s.data, Math.Max(2 * ap.rows(s.data), s.npairs + k - 1), 2 * n, _params);
        }
        for (i = 0; i <= k - 2; i++)
        {
            s0 = 0;
            s1 = 0;
            for (j = 0; j <= n - 1; j++)
            {
                if (s.states[j] >= 0)
                {
                    s0 = s0 + xy[i, j];
                }
                if (s.states[j] <= 0)
                {
                    s1 = s1 + xy[i + 1, j];
                }
            }
            if ((double)(s0) > (double)(0) && (double)(s1) > (double)(0))
            {
                for (j = 0; j <= n - 1; j++)
                {
                    if (s.states[j] >= 0)
                    {
                        s.data[s.npairs, j] = xy[i, j] / s0;
                    }
                    else
                    {
                        s.data[s.npairs, j] = 0.0;
                    }
                    if (s.states[j] <= 0)
                    {
                        s.data[s.npairs, n + j] = xy[i + 1, j] / s1;
                    }
                    else
                    {
                        s.data[s.npairs, n + j] = 0.0;
                    }
                }
                s.npairs = s.npairs + 1;
            }
        }
    }


    /*************************************************************************
    This function is used to add equality constraints on the elements  of  the
    transition matrix P.

    MCPD solver has four types of constraints which can be placed on P:
    * user-specified equality constraints (optional)
    * user-specified bound constraints (optional)
    * user-specified general linear constraints (optional)
    * basic constraints (always present):
      * non-negativity: P[i,j]>=0
      * consistency: every column of P sums to 1.0

    Final  constraints  which  are  passed  to  the  underlying  optimizer are
    calculated  as  intersection  of all present constraints. For example, you
    may specify boundary constraint on P[0,0] and equality one:
        0.1<=P[0,0]<=0.9
        P[0,0]=0.5
    Such  combination  of  constraints  will  be  silently  reduced  to  their
    intersection, which is P[0,0]=0.5.

    This  function  can  be  used  to  place equality constraints on arbitrary
    subset of elements of P. Set of constraints is specified by EC, which  may
    contain either NAN's or finite numbers from [0,1]. NAN denotes absence  of
    constraint, finite number denotes equality constraint on specific  element
    of P.

    You can also  use  MCPDAddEC()  function  which  allows  to  ADD  equality
    constraint  for  one  element  of P without changing constraints for other
    elements.

    These functions (MCPDSetEC and MCPDAddEC) interact as follows:
    * there is internal matrix of equality constraints which is stored in  the
      MCPD solver
    * MCPDSetEC() replaces this matrix by another one (SET)
    * MCPDAddEC() modifies one element of this matrix and  leaves  other  ones
      unchanged (ADD)
    * thus  MCPDAddEC()  call  preserves  all  modifications  done by previous
      calls,  while  MCPDSetEC()  completely discards all changes  done to the
      equality constraints.

    INPUT PARAMETERS:
        S       -   solver
        EC      -   equality constraints, array[N,N]. Elements of  EC  can  be
                    either NAN's or finite  numbers from  [0,1].  NAN  denotes
                    absence  of  constraints,  while  finite  value    denotes
                    equality constraint on the corresponding element of P.

    NOTES:

    1. infinite values of EC will lead to exception being thrown. Values  less
    than 0.0 or greater than 1.0 will lead to error code being returned  after
    call to MCPDSolve().

      -- ALGLIB --
         Copyright 23.05.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void mcpdsetec(mcpdstate s,
        double[,] ec,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int n = 0;

        n = s.n;
        ap.assert(ap.cols(ec) >= n, "MCPDSetEC: Cols(EC)<N");
        ap.assert(ap.rows(ec) >= n, "MCPDSetEC: Rows(EC)<N");
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                ap.assert(math.isfinite(ec[i, j]) || Double.IsNaN(ec[i, j]), "MCPDSetEC: EC containts infinite elements");
                s.ec[i, j] = ec[i, j];
            }
        }
    }


    /*************************************************************************
    This function is used to add equality constraints on the elements  of  the
    transition matrix P.

    MCPD solver has four types of constraints which can be placed on P:
    * user-specified equality constraints (optional)
    * user-specified bound constraints (optional)
    * user-specified general linear constraints (optional)
    * basic constraints (always present):
      * non-negativity: P[i,j]>=0
      * consistency: every column of P sums to 1.0

    Final  constraints  which  are  passed  to  the  underlying  optimizer are
    calculated  as  intersection  of all present constraints. For example, you
    may specify boundary constraint on P[0,0] and equality one:
        0.1<=P[0,0]<=0.9
        P[0,0]=0.5
    Such  combination  of  constraints  will  be  silently  reduced  to  their
    intersection, which is P[0,0]=0.5.

    This function can be used to ADD equality constraint for one element of  P
    without changing constraints for other elements.

    You  can  also  use  MCPDSetEC()  function  which  allows  you  to specify
    arbitrary set of equality constraints in one call.

    These functions (MCPDSetEC and MCPDAddEC) interact as follows:
    * there is internal matrix of equality constraints which is stored in the
      MCPD solver
    * MCPDSetEC() replaces this matrix by another one (SET)
    * MCPDAddEC() modifies one element of this matrix and leaves  other  ones
      unchanged (ADD)
    * thus  MCPDAddEC()  call  preserves  all  modifications done by previous
      calls,  while  MCPDSetEC()  completely discards all changes done to the
      equality constraints.

    INPUT PARAMETERS:
        S       -   solver
        I       -   row index of element being constrained
        J       -   column index of element being constrained
        C       -   value (constraint for P[I,J]).  Can  be  either  NAN  (no
                    constraint) or finite value from [0,1].
                    
    NOTES:

    1. infinite values of C  will lead to exception being thrown. Values  less
    than 0.0 or greater than 1.0 will lead to error code being returned  after
    call to MCPDSolve().

      -- ALGLIB --
         Copyright 23.05.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void mcpdaddec(mcpdstate s,
        int i,
        int j,
        double c,
        xparams _params)
    {
        ap.assert(i >= 0, "MCPDAddEC: I<0");
        ap.assert(i < s.n, "MCPDAddEC: I>=N");
        ap.assert(j >= 0, "MCPDAddEC: J<0");
        ap.assert(j < s.n, "MCPDAddEC: J>=N");
        ap.assert(Double.IsNaN(c) || math.isfinite(c), "MCPDAddEC: C is not finite number or NAN");
        s.ec[i, j] = c;
    }


    /*************************************************************************
    This function is used to add bound constraints  on  the  elements  of  the
    transition matrix P.

    MCPD solver has four types of constraints which can be placed on P:
    * user-specified equality constraints (optional)
    * user-specified bound constraints (optional)
    * user-specified general linear constraints (optional)
    * basic constraints (always present):
      * non-negativity: P[i,j]>=0
      * consistency: every column of P sums to 1.0

    Final  constraints  which  are  passed  to  the  underlying  optimizer are
    calculated  as  intersection  of all present constraints. For example, you
    may specify boundary constraint on P[0,0] and equality one:
        0.1<=P[0,0]<=0.9
        P[0,0]=0.5
    Such  combination  of  constraints  will  be  silently  reduced  to  their
    intersection, which is P[0,0]=0.5.

    This  function  can  be  used  to  place bound   constraints  on arbitrary
    subset  of  elements  of  P.  Set of constraints is specified by BndL/BndU
    matrices, which may contain arbitrary combination  of  finite  numbers  or
    infinities (like -INF<x<=0.5 or 0.1<=x<+INF).

    You can also use MCPDAddBC() function which allows to ADD bound constraint
    for one element of P without changing constraints for other elements.

    These functions (MCPDSetBC and MCPDAddBC) interact as follows:
    * there is internal matrix of bound constraints which is stored in the
      MCPD solver
    * MCPDSetBC() replaces this matrix by another one (SET)
    * MCPDAddBC() modifies one element of this matrix and  leaves  other  ones
      unchanged (ADD)
    * thus  MCPDAddBC()  call  preserves  all  modifications  done by previous
      calls,  while  MCPDSetBC()  completely discards all changes  done to the
      equality constraints.

    INPUT PARAMETERS:
        S       -   solver
        BndL    -   lower bounds constraints, array[N,N]. Elements of BndL can
                    be finite numbers or -INF.
        BndU    -   upper bounds constraints, array[N,N]. Elements of BndU can
                    be finite numbers or +INF.

      -- ALGLIB --
         Copyright 23.05.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void mcpdsetbc(mcpdstate s,
        double[,] bndl,
        double[,] bndu,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int n = 0;

        n = s.n;
        ap.assert(ap.cols(bndl) >= n, "MCPDSetBC: Cols(BndL)<N");
        ap.assert(ap.rows(bndl) >= n, "MCPDSetBC: Rows(BndL)<N");
        ap.assert(ap.cols(bndu) >= n, "MCPDSetBC: Cols(BndU)<N");
        ap.assert(ap.rows(bndu) >= n, "MCPDSetBC: Rows(BndU)<N");
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                ap.assert(math.isfinite(bndl[i, j]) || Double.IsNegativeInfinity(bndl[i, j]), "MCPDSetBC: BndL containts NAN or +INF");
                ap.assert(math.isfinite(bndu[i, j]) || Double.IsPositiveInfinity(bndu[i, j]), "MCPDSetBC: BndU containts NAN or -INF");
                s.bndl[i, j] = bndl[i, j];
                s.bndu[i, j] = bndu[i, j];
            }
        }
    }


    /*************************************************************************
    This function is used to add bound constraints  on  the  elements  of  the
    transition matrix P.

    MCPD solver has four types of constraints which can be placed on P:
    * user-specified equality constraints (optional)
    * user-specified bound constraints (optional)
    * user-specified general linear constraints (optional)
    * basic constraints (always present):
      * non-negativity: P[i,j]>=0
      * consistency: every column of P sums to 1.0

    Final  constraints  which  are  passed  to  the  underlying  optimizer are
    calculated  as  intersection  of all present constraints. For example, you
    may specify boundary constraint on P[0,0] and equality one:
        0.1<=P[0,0]<=0.9
        P[0,0]=0.5
    Such  combination  of  constraints  will  be  silently  reduced  to  their
    intersection, which is P[0,0]=0.5.

    This  function  can  be  used to ADD bound constraint for one element of P
    without changing constraints for other elements.

    You  can  also  use  MCPDSetBC()  function  which  allows to  place  bound
    constraints  on arbitrary subset of elements of P.   Set of constraints is
    specified  by  BndL/BndU matrices, which may contain arbitrary combination
    of finite numbers or infinities (like -INF<x<=0.5 or 0.1<=x<+INF).

    These functions (MCPDSetBC and MCPDAddBC) interact as follows:
    * there is internal matrix of bound constraints which is stored in the
      MCPD solver
    * MCPDSetBC() replaces this matrix by another one (SET)
    * MCPDAddBC() modifies one element of this matrix and  leaves  other  ones
      unchanged (ADD)
    * thus  MCPDAddBC()  call  preserves  all  modifications  done by previous
      calls,  while  MCPDSetBC()  completely discards all changes  done to the
      equality constraints.

    INPUT PARAMETERS:
        S       -   solver
        I       -   row index of element being constrained
        J       -   column index of element being constrained
        BndL    -   lower bound
        BndU    -   upper bound

      -- ALGLIB --
         Copyright 23.05.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void mcpdaddbc(mcpdstate s,
        int i,
        int j,
        double bndl,
        double bndu,
        xparams _params)
    {
        ap.assert(i >= 0, "MCPDAddBC: I<0");
        ap.assert(i < s.n, "MCPDAddBC: I>=N");
        ap.assert(j >= 0, "MCPDAddBC: J<0");
        ap.assert(j < s.n, "MCPDAddBC: J>=N");
        ap.assert(math.isfinite(bndl) || Double.IsNegativeInfinity(bndl), "MCPDAddBC: BndL is NAN or +INF");
        ap.assert(math.isfinite(bndu) || Double.IsPositiveInfinity(bndu), "MCPDAddBC: BndU is NAN or -INF");
        s.bndl[i, j] = bndl;
        s.bndu[i, j] = bndu;
    }


    /*************************************************************************
    This function is used to set linear equality/inequality constraints on the
    elements of the transition matrix P.

    This function can be used to set one or several general linear constraints
    on the elements of P. Two types of constraints are supported:
    * equality constraints
    * inequality constraints (both less-or-equal and greater-or-equal)

    Coefficients  of  constraints  are  specified  by  matrix  C (one  of  the
    parameters).  One  row  of  C  corresponds  to  one  constraint.   Because
    transition  matrix P has N*N elements,  we  need  N*N columns to store all
    coefficients  (they  are  stored row by row), and one more column to store
    right part - hence C has N*N+1 columns.  Constraint  kind is stored in the
    CT array.

    Thus, I-th linear constraint is
        P[0,0]*C[I,0] + P[0,1]*C[I,1] + .. + P[0,N-1]*C[I,N-1] +
            + P[1,0]*C[I,N] + P[1,1]*C[I,N+1] + ... +
            + P[N-1,N-1]*C[I,N*N-1]  ?=?  C[I,N*N]
    where ?=? can be either "=" (CT[i]=0), "<=" (CT[i]<0) or ">=" (CT[i]>0).

    Your constraint may involve only some subset of P (less than N*N elements).
    For example it can be something like
        P[0,0] + P[0,1] = 0.5
    In this case you still should pass matrix  with N*N+1 columns, but all its
    elements (except for C[0,0], C[0,1] and C[0,N*N-1]) will be zero.

    INPUT PARAMETERS:
        S       -   solver
        C       -   array[K,N*N+1] - coefficients of constraints
                    (see above for complete description)
        CT      -   array[K] - constraint types
                    (see above for complete description)
        K       -   number of equality/inequality constraints, K>=0:
                    * if given, only leading K elements of C/CT are used
                    * if not given, automatically determined from sizes of C/CT

      -- ALGLIB --
         Copyright 23.05.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void mcpdsetlc(mcpdstate s,
        double[,] c,
        int[] ct,
        int k,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int n = 0;

        n = s.n;
        ap.assert(ap.cols(c) >= n * n + 1, "MCPDSetLC: Cols(C)<N*N+1");
        ap.assert(ap.rows(c) >= k, "MCPDSetLC: Rows(C)<K");
        ap.assert(ap.len(ct) >= k, "MCPDSetLC: Len(CT)<K");
        ap.assert(apserv.apservisfinitematrix(c, k, n * n + 1, _params), "MCPDSetLC: C contains infinite or NaN values!");
        apserv.rmatrixsetlengthatleast(ref s.c, k, n * n + 1, _params);
        apserv.ivectorsetlengthatleast(ref s.ct, k, _params);
        for (i = 0; i <= k - 1; i++)
        {
            for (j = 0; j <= n * n; j++)
            {
                s.c[i, j] = c[i, j];
            }
            s.ct[i] = ct[i];
        }
        s.ccnt = k;
    }


    /*************************************************************************
    This function allows to  tune  amount  of  Tikhonov  regularization  being
    applied to your problem.

    By default, regularizing term is equal to r*||P-prior_P||^2, where r is  a
    small non-zero value,  P is transition matrix, prior_P is identity matrix,
    ||X||^2 is a sum of squared elements of X.

    This  function  allows  you to change coefficient r. You can  also  change
    prior values with MCPDSetPrior() function.

    INPUT PARAMETERS:
        S       -   solver
        V       -   regularization  coefficient, finite non-negative value. It
                    is  not  recommended  to specify zero value unless you are
                    pretty sure that you want it.

      -- ALGLIB --
         Copyright 23.05.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void mcpdsettikhonovregularizer(mcpdstate s,
        double v,
        xparams _params)
    {
        ap.assert(math.isfinite(v), "MCPDSetTikhonovRegularizer: V is infinite or NAN");
        ap.assert((double)(v) >= (double)(0.0), "MCPDSetTikhonovRegularizer: V is less than zero");
        s.regterm = v;
    }


    /*************************************************************************
    This  function  allows to set prior values used for regularization of your
    problem.

    By default, regularizing term is equal to r*||P-prior_P||^2, where r is  a
    small non-zero value,  P is transition matrix, prior_P is identity matrix,
    ||X||^2 is a sum of squared elements of X.

    This  function  allows  you to change prior values prior_P. You  can  also
    change r with MCPDSetTikhonovRegularizer() function.

    INPUT PARAMETERS:
        S       -   solver
        PP      -   array[N,N], matrix of prior values:
                    1. elements must be real numbers from [0,1]
                    2. columns must sum to 1.0.
                    First property is checked (exception is thrown otherwise),
                    while second one is not checked/enforced.

      -- ALGLIB --
         Copyright 23.05.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void mcpdsetprior(mcpdstate s,
        double[,] pp,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int n = 0;

        pp = (double[,])pp.Clone();

        n = s.n;
        ap.assert(ap.cols(pp) >= n, "MCPDSetPrior: Cols(PP)<N");
        ap.assert(ap.rows(pp) >= n, "MCPDSetPrior: Rows(PP)<K");
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                ap.assert(math.isfinite(pp[i, j]), "MCPDSetPrior: PP containts infinite elements");
                ap.assert((double)(pp[i, j]) >= (double)(0.0) && (double)(pp[i, j]) <= (double)(1.0), "MCPDSetPrior: PP[i,j] is less than 0.0 or greater than 1.0");
                s.priorp[i, j] = pp[i, j];
            }
        }
    }


    /*************************************************************************
    This function is used to change prediction weights

    MCPD solver scales prediction errors as follows
        Error(P) = ||W*(y-P*x)||^2
    where
        x is a system state at time t
        y is a system state at time t+1
        P is a transition matrix
        W is a diagonal scaling matrix

    By default, weights are chosen in order  to  minimize  relative prediction
    error instead of absolute one. For example, if one component of  state  is
    about 0.5 in magnitude and another one is about 0.05, then algorithm  will
    make corresponding weights equal to 2.0 and 20.0.

    INPUT PARAMETERS:
        S       -   solver
        PW      -   array[N], weights:
                    * must be non-negative values (exception will be thrown otherwise)
                    * zero values will be replaced by automatically chosen values

      -- ALGLIB --
         Copyright 23.05.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void mcpdsetpredictionweights(mcpdstate s,
        double[] pw,
        xparams _params)
    {
        int i = 0;
        int n = 0;

        n = s.n;
        ap.assert(ap.len(pw) >= n, "MCPDSetPredictionWeights: Length(PW)<N");
        for (i = 0; i <= n - 1; i++)
        {
            ap.assert(math.isfinite(pw[i]), "MCPDSetPredictionWeights: PW containts infinite or NAN elements");
            ap.assert((double)(pw[i]) >= (double)(0), "MCPDSetPredictionWeights: PW containts negative elements");
            s.pw[i] = pw[i];
        }
    }


    /*************************************************************************
    This function is used to start solution of the MCPD problem.

    After return from this function, you can use MCPDResults() to get solution
    and completion code.

      -- ALGLIB --
         Copyright 23.05.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void mcpdsolve(mcpdstate s,
        xparams _params)
    {
        int n = 0;
        int npairs = 0;
        int ccnt = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        int k2 = 0;
        double v = 0;
        double vv = 0;
        int i_ = 0;
        int i1_ = 0;

        n = s.n;
        npairs = s.npairs;

        //
        // init fields of S
        //
        s.repterminationtype = 0;
        s.repinneriterationscount = 0;
        s.repouteriterationscount = 0;
        s.repnfev = 0;
        for (k = 0; k <= n - 1; k++)
        {
            for (k2 = 0; k2 <= n - 1; k2++)
            {
                s.p[k, k2] = Double.NaN;
            }
        }

        //
        // Generate "effective" weights for prediction and calculate preconditioner
        //
        for (i = 0; i <= n - 1; i++)
        {
            if ((double)(s.pw[i]) == (double)(0))
            {
                v = 0;
                k = 0;
                for (j = 0; j <= npairs - 1; j++)
                {
                    if ((double)(s.data[j, n + i]) != (double)(0))
                    {
                        v = v + s.data[j, n + i];
                        k = k + 1;
                    }
                }
                if (k != 0)
                {
                    s.effectivew[i] = k / v;
                }
                else
                {
                    s.effectivew[i] = 1.0;
                }
            }
            else
            {
                s.effectivew[i] = s.pw[i];
            }
        }
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                s.h[i * n + j] = 2 * s.regterm;
            }
        }
        for (k = 0; k <= npairs - 1; k++)
        {
            for (i = 0; i <= n - 1; i++)
            {
                for (j = 0; j <= n - 1; j++)
                {
                    s.h[i * n + j] = s.h[i * n + j] + 2 * math.sqr(s.effectivew[i]) * math.sqr(s.data[k, j]);
                }
            }
        }
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                if ((double)(s.h[i * n + j]) == (double)(0))
                {
                    s.h[i * n + j] = 1;
                }
            }
        }

        //
        // Generate "effective" BndL/BndU
        //
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {

                //
                // Set default boundary constraints.
                // Lower bound is always zero, upper bound is calculated
                // with respect to entry/exit states.
                //
                s.effectivebndl[i * n + j] = 0.0;
                if (s.states[i] > 0 || s.states[j] < 0)
                {
                    s.effectivebndu[i * n + j] = 0.0;
                }
                else
                {
                    s.effectivebndu[i * n + j] = 1.0;
                }

                //
                // Calculate intersection of the default and user-specified bound constraints.
                // This code checks consistency of such combination.
                //
                if (math.isfinite(s.bndl[i, j]) && (double)(s.bndl[i, j]) > (double)(s.effectivebndl[i * n + j]))
                {
                    s.effectivebndl[i * n + j] = s.bndl[i, j];
                }
                if (math.isfinite(s.bndu[i, j]) && (double)(s.bndu[i, j]) < (double)(s.effectivebndu[i * n + j]))
                {
                    s.effectivebndu[i * n + j] = s.bndu[i, j];
                }
                if ((double)(s.effectivebndl[i * n + j]) > (double)(s.effectivebndu[i * n + j]))
                {
                    s.repterminationtype = -3;
                    return;
                }

                //
                // Calculate intersection of the effective bound constraints
                // and user-specified equality constraints.
                // This code checks consistency of such combination.
                //
                if (math.isfinite(s.ec[i, j]))
                {
                    if ((double)(s.ec[i, j]) < (double)(s.effectivebndl[i * n + j]) || (double)(s.ec[i, j]) > (double)(s.effectivebndu[i * n + j]))
                    {
                        s.repterminationtype = -3;
                        return;
                    }
                    s.effectivebndl[i * n + j] = s.ec[i, j];
                    s.effectivebndu[i * n + j] = s.ec[i, j];
                }
            }
        }

        //
        // Generate linear constraints:
        // * "default" sums-to-one constraints (not generated for "exit" states)
        //
        apserv.rmatrixsetlengthatleast(ref s.effectivec, s.ccnt + n, n * n + 1, _params);
        apserv.ivectorsetlengthatleast(ref s.effectivect, s.ccnt + n, _params);
        ccnt = s.ccnt;
        for (i = 0; i <= s.ccnt - 1; i++)
        {
            for (j = 0; j <= n * n; j++)
            {
                s.effectivec[i, j] = s.c[i, j];
            }
            s.effectivect[i] = s.ct[i];
        }
        for (i = 0; i <= n - 1; i++)
        {
            if (s.states[i] >= 0)
            {
                for (k = 0; k <= n * n - 1; k++)
                {
                    s.effectivec[ccnt, k] = 0;
                }
                for (k = 0; k <= n - 1; k++)
                {
                    s.effectivec[ccnt, k * n + i] = 1;
                }
                s.effectivec[ccnt, n * n] = 1.0;
                s.effectivect[ccnt] = 0;
                ccnt = ccnt + 1;
            }
        }

        //
        // create optimizer
        //
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                s.tmpp[i * n + j] = (double)1 / (double)n;
            }
        }
        minbleic.minbleicrestartfrom(s.bs, s.tmpp, _params);
        minbleic.minbleicsetbc(s.bs, s.effectivebndl, s.effectivebndu, _params);
        minbleic.minbleicsetlc(s.bs, s.effectivec, s.effectivect, ccnt, _params);
        minbleic.minbleicsetcond(s.bs, 0.0, 0.0, xtol, 0, _params);
        minbleic.minbleicsetprecdiag(s.bs, s.h, _params);

        //
        // solve problem
        //
        while (minbleic.minbleiciteration(s.bs, _params))
        {
            ap.assert(s.bs.needfg, "MCPDSolve: internal error");
            if (s.bs.needfg)
            {

                //
                // Calculate regularization term
                //
                s.bs.f = 0.0;
                vv = s.regterm;
                for (i = 0; i <= n - 1; i++)
                {
                    for (j = 0; j <= n - 1; j++)
                    {
                        s.bs.f = s.bs.f + vv * math.sqr(s.bs.x[i * n + j] - s.priorp[i, j]);
                        s.bs.g[i * n + j] = 2 * vv * (s.bs.x[i * n + j] - s.priorp[i, j]);
                    }
                }

                //
                // calculate prediction error/gradient for K-th pair
                //
                for (k = 0; k <= npairs - 1; k++)
                {
                    for (i = 0; i <= n - 1; i++)
                    {
                        i1_ = (0) - (i * n);
                        v = 0.0;
                        for (i_ = i * n; i_ <= i * n + n - 1; i_++)
                        {
                            v += s.bs.x[i_] * s.data[k, i_ + i1_];
                        }
                        vv = s.effectivew[i];
                        s.bs.f = s.bs.f + math.sqr(vv * (v - s.data[k, n + i]));
                        for (j = 0; j <= n - 1; j++)
                        {
                            s.bs.g[i * n + j] = s.bs.g[i * n + j] + 2 * vv * vv * (v - s.data[k, n + i]) * s.data[k, j];
                        }
                    }
                }

                //
                // continue
                //
                continue;
            }
        }
        minbleic.minbleicresultsbuf(s.bs, ref s.tmpp, s.br, _params);
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                s.p[i, j] = s.tmpp[i * n + j];
            }
        }
        s.repterminationtype = s.br.terminationtype;
        s.repinneriterationscount = s.br.inneriterationscount;
        s.repouteriterationscount = s.br.outeriterationscount;
        s.repnfev = s.br.nfev;
    }


    /*************************************************************************
    MCPD results

    INPUT PARAMETERS:
        State   -   algorithm state

    OUTPUT PARAMETERS:
        P       -   array[N,N], transition matrix
        Rep     -   optimization report. You should check Rep.TerminationType
                    in  order  to  distinguish  successful  termination  from
                    unsuccessful one. Speaking short, positive values  denote
                    success, negative ones are failures.
                    More information about fields of this  structure  can  be
                    found in the comments on MCPDReport datatype.


      -- ALGLIB --
         Copyright 23.05.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void mcpdresults(mcpdstate s,
        ref double[,] p,
        mcpdreport rep,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        p = new double[0, 0];

        p = new double[s.n, s.n];
        for (i = 0; i <= s.n - 1; i++)
        {
            for (j = 0; j <= s.n - 1; j++)
            {
                p[i, j] = s.p[i, j];
            }
        }
        rep.terminationtype = s.repterminationtype;
        rep.inneriterationscount = s.repinneriterationscount;
        rep.outeriterationscount = s.repouteriterationscount;
        rep.nfev = s.repnfev;
    }


    /*************************************************************************
    Internal initialization function

      -- ALGLIB --
         Copyright 23.05.2010 by Bochkanov Sergey
    *************************************************************************/
    private static void mcpdinit(int n,
        int entrystate,
        int exitstate,
        mcpdstate s,
        xparams _params)
    {
        int i = 0;
        int j = 0;

        ap.assert(n >= 1, "MCPDCreate: N<1");
        s.n = n;
        s.states = new int[n];
        for (i = 0; i <= n - 1; i++)
        {
            s.states[i] = 0;
        }
        if (entrystate >= 0)
        {
            s.states[entrystate] = 1;
        }
        if (exitstate >= 0)
        {
            s.states[exitstate] = -1;
        }
        s.npairs = 0;
        s.regterm = 1.0E-8;
        s.ccnt = 0;
        s.p = new double[n, n];
        s.ec = new double[n, n];
        s.bndl = new double[n, n];
        s.bndu = new double[n, n];
        s.pw = new double[n];
        s.priorp = new double[n, n];
        s.tmpp = new double[n * n];
        s.effectivew = new double[n];
        s.effectivebndl = new double[n * n];
        s.effectivebndu = new double[n * n];
        s.h = new double[n * n];
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= n - 1; j++)
            {
                s.p[i, j] = 0.0;
                s.priorp[i, j] = 0.0;
                s.bndl[i, j] = Double.NegativeInfinity;
                s.bndu[i, j] = Double.PositiveInfinity;
                s.ec[i, j] = Double.NaN;
            }
            s.pw[i] = 0.0;
            s.priorp[i, i] = 1.0;
        }
        s.data = new double[1, 2 * n];
        for (i = 0; i <= 2 * n - 1; i++)
        {
            s.data[0, i] = 0.0;
        }
        for (i = 0; i <= n * n - 1; i++)
        {
            s.tmpp[i] = 0.0;
        }
        minbleic.minbleiccreate(n * n, s.tmpp, s.bs, _params);
    }


}
