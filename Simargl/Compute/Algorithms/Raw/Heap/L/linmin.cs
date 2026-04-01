using System;

#pragma warning disable CS8618
#pragma warning disable CS0162
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

public class linmin
{
    public class linminstate : apobject
    {
        public bool brackt;
        public bool stage1;
        public int infoc;
        public double dg;
        public double dgm;
        public double dginit;
        public double dgtest;
        public double dgx;
        public double dgxm;
        public double dgy;
        public double dgym;
        public double finit;
        public double ftest1;
        public double fm;
        public double fx;
        public double fxm;
        public double fy;
        public double fym;
        public double stx;
        public double sty;
        public double stmin;
        public double stmax;
        public double width;
        public double width1;
        public double xtrapf;
        public linminstate()
        {
            init();
        }
        public override void init()
        {
        }
        public override apobject make_copy()
        {
            linminstate _result = new linminstate();
            _result.brackt = brackt;
            _result.stage1 = stage1;
            _result.infoc = infoc;
            _result.dg = dg;
            _result.dgm = dgm;
            _result.dginit = dginit;
            _result.dgtest = dgtest;
            _result.dgx = dgx;
            _result.dgxm = dgxm;
            _result.dgy = dgy;
            _result.dgym = dgym;
            _result.finit = finit;
            _result.ftest1 = ftest1;
            _result.fm = fm;
            _result.fx = fx;
            _result.fxm = fxm;
            _result.fy = fy;
            _result.fym = fym;
            _result.stx = stx;
            _result.sty = sty;
            _result.stmin = stmin;
            _result.stmax = stmax;
            _result.width = width;
            _result.width1 = width1;
            _result.xtrapf = xtrapf;
            return _result;
        }
    };


    public class armijostate : apobject
    {
        public bool needf;
        public double[] x;
        public double f;
        public int n;
        public double[] xbase;
        public double[] s;
        public double stplen;
        public double fcur;
        public double stpmax;
        public int fmax;
        public int nfev;
        public int info;
        public rcommstate rstate;
        public armijostate()
        {
            init();
        }
        public override void init()
        {
            x = new double[0];
            xbase = new double[0];
            s = new double[0];
            rstate = new rcommstate();
        }
        public override apobject make_copy()
        {
            armijostate _result = new armijostate();
            _result.needf = needf;
            _result.x = (double[])x.Clone();
            _result.f = f;
            _result.n = n;
            _result.xbase = (double[])xbase.Clone();
            _result.s = (double[])s.Clone();
            _result.stplen = stplen;
            _result.fcur = fcur;
            _result.stpmax = stpmax;
            _result.fmax = fmax;
            _result.nfev = nfev;
            _result.info = info;
            _result.rstate = (rcommstate)rstate.make_copy();
            return _result;
        }
    };




    public const double ftol = 0.001;
    public const double xtol = 100 * math.machineepsilon;
    public const int maxfev = 20;
    public const double stpmin = 1.0E-50;
    public const double defstpmax = 1.0E+50;
    public const double armijofactor = 1.3;


    /*************************************************************************
    Normalizes direction/step pair: makes |D|=1, scales Stp.
    If |D|=0, it returns, leavind D/Stp unchanged.

      -- ALGLIB --
         Copyright 01.04.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void linminnormalized(ref double[] d,
        ref double stp,
        int n,
        xparams _params)
    {
        double mx = 0;
        double s = 0;
        int i = 0;
        int i_ = 0;


        //
        // first, scale D to avoid underflow/overflow durng squaring
        //
        mx = 0;
        for (i = 0; i <= n - 1; i++)
        {
            mx = Math.Max(mx, Math.Abs(d[i]));
        }
        if ((double)(mx) == (double)(0))
        {
            return;
        }
        s = 1 / mx;
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            d[i_] = s * d[i_];
        }
        stp = stp / s;

        //
        // normalize D
        //
        s = 0.0;
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            s += d[i_] * d[i_];
        }
        s = 1 / Math.Sqrt(s);
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            d[i_] = s * d[i_];
        }
        stp = stp / s;
    }


    /*************************************************************************
    THE  PURPOSE  OF  MCSRCH  IS  TO  FIND A STEP WHICH SATISFIES A SUFFICIENT
    DECREASE CONDITION AND A CURVATURE CONDITION.

    AT EACH STAGE THE SUBROUTINE  UPDATES  AN  INTERVAL  OF  UNCERTAINTY  WITH
    ENDPOINTS  STX  AND  STY.  THE INTERVAL OF UNCERTAINTY IS INITIALLY CHOSEN
    SO THAT IT CONTAINS A MINIMIZER OF THE MODIFIED FUNCTION

        F(X+STP*S) - F(X) - FTOL*STP*(GRADF(X)'S).

    IF  A STEP  IS OBTAINED FOR  WHICH THE MODIFIED FUNCTION HAS A NONPOSITIVE
    FUNCTION  VALUE  AND  NONNEGATIVE  DERIVATIVE,   THEN   THE   INTERVAL  OF
    UNCERTAINTY IS CHOSEN SO THAT IT CONTAINS A MINIMIZER OF F(X+STP*S).

    THE  ALGORITHM  IS  DESIGNED TO FIND A STEP WHICH SATISFIES THE SUFFICIENT
    DECREASE CONDITION

        F(X+STP*S) .LE. F(X) + FTOL*STP*(GRADF(X)'S),

    AND THE CURVATURE CONDITION

        ABS(GRADF(X+STP*S)'S)) .LE. GTOL*ABS(GRADF(X)'S).

    IF  FTOL  IS  LESS  THAN GTOL AND IF, FOR EXAMPLE, THE FUNCTION IS BOUNDED
    BELOW,  THEN  THERE  IS  ALWAYS  A  STEP  WHICH SATISFIES BOTH CONDITIONS.
    IF  NO  STEP  CAN BE FOUND  WHICH  SATISFIES  BOTH  CONDITIONS,  THEN  THE
    ALGORITHM  USUALLY STOPS  WHEN  ROUNDING ERRORS  PREVENT FURTHER PROGRESS.
    IN THIS CASE STP ONLY SATISFIES THE SUFFICIENT DECREASE CONDITION.


    :::::::::::::IMPORTANT NOTES:::::::::::::

    NOTE 1:

    This routine  guarantees that it will stop at the last point where function
    value was calculated. It won't make several additional function evaluations
    after finding good point. So if you store function evaluations requested by
    this routine, you can be sure that last one is the point where we've stopped.

    NOTE 2:

    when 0<StpMax<StpMin, algorithm will terminate with INFO=5 and Stp=StpMax

    NOTE 3:

    this algorithm guarantees that, if MCINFO=1 or MCINFO=5, then:
    * F(final_point)<F(initial_point) - strict inequality
    * final_point<>initial_point - after rounding to machine precision

    NOTE 4:

    when non-descent direction is specified, algorithm stops with MCINFO=0,
    Stp=0 and initial point at X[].
    :::::::::::::::::::::::::::::::::::::::::


    PARAMETERS DESCRIPRION

    STAGE IS ZERO ON FIRST CALL, ZERO ON FINAL EXIT

    N IS A POSITIVE INTEGER INPUT VARIABLE SET TO THE NUMBER OF VARIABLES.

    X IS  AN  ARRAY  OF  LENGTH N. ON INPUT IT MUST CONTAIN THE BASE POINT FOR
    THE LINE SEARCH. ON OUTPUT IT CONTAINS X+STP*S.

    F IS  A  VARIABLE. ON INPUT IT MUST CONTAIN THE VALUE OF F AT X. ON OUTPUT
    IT CONTAINS THE VALUE OF F AT X + STP*S.

    G IS AN ARRAY OF LENGTH N. ON INPUT IT MUST CONTAIN THE GRADIENT OF F AT X.
    ON OUTPUT IT CONTAINS THE GRADIENT OF F AT X + STP*S.

    S IS AN INPUT ARRAY OF LENGTH N WHICH SPECIFIES THE SEARCH DIRECTION.

    STP  IS  A NONNEGATIVE VARIABLE. ON INPUT STP CONTAINS AN INITIAL ESTIMATE
    OF A SATISFACTORY STEP. ON OUTPUT STP CONTAINS THE FINAL ESTIMATE.

    FTOL AND GTOL ARE NONNEGATIVE INPUT VARIABLES. TERMINATION OCCURS WHEN THE
    SUFFICIENT DECREASE CONDITION AND THE DIRECTIONAL DERIVATIVE CONDITION ARE
    SATISFIED.

    XTOL IS A NONNEGATIVE INPUT VARIABLE. TERMINATION OCCURS WHEN THE RELATIVE
    WIDTH OF THE INTERVAL OF UNCERTAINTY IS AT MOST XTOL.

    STPMIN AND STPMAX ARE NONNEGATIVE INPUT VARIABLES WHICH SPECIFY LOWER  AND
    UPPER BOUNDS FOR THE STEP.

    MAXFEV IS A POSITIVE INTEGER INPUT VARIABLE. TERMINATION OCCURS WHEN THE
    NUMBER OF CALLS TO FCN IS AT LEAST MAXFEV BY THE END OF AN ITERATION.

    INFO IS AN INTEGER OUTPUT VARIABLE SET AS FOLLOWS:
        INFO = 0  IMPROPER INPUT PARAMETERS.

        INFO = 1  THE SUFFICIENT DECREASE CONDITION AND THE
                  DIRECTIONAL DERIVATIVE CONDITION HOLD.

        INFO = 2  RELATIVE WIDTH OF THE INTERVAL OF UNCERTAINTY
                  IS AT MOST XTOL.

        INFO = 3  NUMBER OF CALLS TO FCN HAS REACHED MAXFEV.

        INFO = 4  THE STEP IS AT THE LOWER BOUND STPMIN.

        INFO = 5  THE STEP IS AT THE UPPER BOUND STPMAX.

        INFO = 6  ROUNDING ERRORS PREVENT FURTHER PROGRESS.
                  THERE MAY NOT BE A STEP WHICH SATISFIES THE
                  SUFFICIENT DECREASE AND CURVATURE CONDITIONS.
                  TOLERANCES MAY BE TOO SMALL.

    NFEV IS AN INTEGER OUTPUT VARIABLE SET TO THE NUMBER OF CALLS TO FCN.

    WA IS A WORK ARRAY OF LENGTH N.

    ARGONNE NATIONAL LABORATORY. MINPACK PROJECT. JUNE 1983
    JORGE J. MORE', DAVID J. THUENTE
    *************************************************************************/
    public static void mcsrch(int n,
        ref double[] x,
        ref double f,
        ref double[] g,
        double[] s,
        ref double stp,
        double stpmax,
        double gtol,
        ref int info,
        ref int nfev,
        ref double[] wa,
        linminstate state,
        ref int stage,
        xparams _params)
    {
        int i = 0;
        double v = 0;
        double p5 = 0;
        double p66 = 0;
        double zero = 0;
        int i_ = 0;


        //
        // init
        //
        p5 = 0.5;
        p66 = 0.66;
        state.xtrapf = 4.0;
        zero = 0;
        if ((double)(stpmax) == (double)(0))
        {
            stpmax = defstpmax;
        }
        if ((double)(stp) < (double)(stpmin))
        {
            stp = stpmin;
        }
        if ((double)(stp) > (double)(stpmax))
        {
            stp = stpmax;
        }

        //
        // Main cycle
        //
        while (true)
        {
            if (stage == 0)
            {

                //
                // NEXT
                //
                stage = 2;
                continue;
            }
            if (stage == 2)
            {
                state.infoc = 1;
                info = 0;

                //
                //     CHECK THE INPUT PARAMETERS FOR ERRORS.
                //
                if ((double)(stpmax) < (double)(stpmin) && (double)(stpmax) > (double)(0))
                {
                    info = 5;
                    stp = stpmax;
                    stage = 0;
                    return;
                }
                if (((((((n <= 0 || (double)(stp) <= (double)(0)) || (double)(ftol) < (double)(0)) || (double)(gtol) < (double)(zero)) || (double)(xtol) < (double)(zero)) || (double)(stpmin) < (double)(zero)) || (double)(stpmax) < (double)(stpmin)) || maxfev <= 0)
                {
                    stage = 0;
                    return;
                }

                //
                //     COMPUTE THE INITIAL GRADIENT IN THE SEARCH DIRECTION
                //     AND CHECK THAT S IS A DESCENT DIRECTION.
                //
                v = 0.0;
                for (i_ = 0; i_ <= n - 1; i_++)
                {
                    v += g[i_] * s[i_];
                }
                state.dginit = v;
                if ((double)(state.dginit) >= (double)(0))
                {
                    stage = 0;
                    stp = 0;
                    return;
                }

                //
                //     INITIALIZE LOCAL VARIABLES.
                //
                state.brackt = false;
                state.stage1 = true;
                nfev = 0;
                state.finit = f;
                state.dgtest = ftol * state.dginit;
                state.width = stpmax - stpmin;
                state.width1 = state.width / p5;
                for (i_ = 0; i_ <= n - 1; i_++)
                {
                    wa[i_] = x[i_];
                }

                //
                //     THE VARIABLES STX, FX, DGX CONTAIN THE VALUES OF THE STEP,
                //     FUNCTION, AND DIRECTIONAL DERIVATIVE AT THE BEST STEP.
                //     THE VARIABLES STY, FY, DGY CONTAIN THE VALUE OF THE STEP,
                //     FUNCTION, AND DERIVATIVE AT THE OTHER ENDPOINT OF
                //     THE INTERVAL OF UNCERTAINTY.
                //     THE VARIABLES STP, F, DG CONTAIN THE VALUES OF THE STEP,
                //     FUNCTION, AND DERIVATIVE AT THE CURRENT STEP.
                //
                state.stx = 0;
                state.fx = state.finit;
                state.dgx = state.dginit;
                state.sty = 0;
                state.fy = state.finit;
                state.dgy = state.dginit;

                //
                // NEXT
                //
                stage = 3;
                continue;
            }
            if (stage == 3)
            {

                //
                //     START OF ITERATION.
                //
                //     SET THE MINIMUM AND MAXIMUM STEPS TO CORRESPOND
                //     TO THE PRESENT INTERVAL OF UNCERTAINTY.
                //
                if (state.brackt)
                {
                    if ((double)(state.stx) < (double)(state.sty))
                    {
                        state.stmin = state.stx;
                        state.stmax = state.sty;
                    }
                    else
                    {
                        state.stmin = state.sty;
                        state.stmax = state.stx;
                    }
                }
                else
                {
                    state.stmin = state.stx;
                    state.stmax = stp + state.xtrapf * (stp - state.stx);
                }

                //
                //        FORCE THE STEP TO BE WITHIN THE BOUNDS STPMAX AND STPMIN.
                //
                if ((double)(stp) > (double)(stpmax))
                {
                    stp = stpmax;
                }
                if ((double)(stp) < (double)(stpmin))
                {
                    stp = stpmin;
                }

                //
                //        IF AN UNUSUAL TERMINATION IS TO OCCUR THEN LET
                //        STP BE THE LOWEST POINT OBTAINED SO FAR.
                //
                if ((((state.brackt && ((double)(stp) <= (double)(state.stmin) || (double)(stp) >= (double)(state.stmax))) || nfev >= maxfev - 1) || state.infoc == 0) || (state.brackt && (double)(state.stmax - state.stmin) <= (double)(xtol * state.stmax)))
                {
                    stp = state.stx;
                }

                //
                //        EVALUATE THE FUNCTION AND GRADIENT AT STP
                //        AND COMPUTE THE DIRECTIONAL DERIVATIVE.
                //
                for (i_ = 0; i_ <= n - 1; i_++)
                {
                    x[i_] = wa[i_];
                }
                for (i_ = 0; i_ <= n - 1; i_++)
                {
                    x[i_] = x[i_] + stp * s[i_];
                }

                //
                // NEXT
                //
                stage = 4;
                return;
            }
            if (stage == 4)
            {
                info = 0;
                nfev = nfev + 1;
                v = 0.0;
                for (i_ = 0; i_ <= n - 1; i_++)
                {
                    v += g[i_] * s[i_];
                }
                state.dg = v;
                state.ftest1 = state.finit + stp * state.dgtest;

                //
                //        TEST FOR CONVERGENCE.
                //
                if ((state.brackt && ((double)(stp) <= (double)(state.stmin) || (double)(stp) >= (double)(state.stmax))) || state.infoc == 0)
                {
                    info = 6;
                }
                if ((((double)(stp) == (double)(stpmax) && (double)(f) < (double)(state.finit)) && (double)(f) <= (double)(state.ftest1)) && (double)(state.dg) <= (double)(state.dgtest))
                {
                    info = 5;
                }
                if ((double)(stp) == (double)(stpmin) && (((double)(f) >= (double)(state.finit) || (double)(f) > (double)(state.ftest1)) || (double)(state.dg) >= (double)(state.dgtest)))
                {
                    info = 4;
                }
                if (nfev >= maxfev)
                {
                    info = 3;
                }
                if (state.brackt && (double)(state.stmax - state.stmin) <= (double)(xtol * state.stmax))
                {
                    info = 2;
                }
                if (((double)(f) < (double)(state.finit) && (double)(f) <= (double)(state.ftest1)) && (double)(Math.Abs(state.dg)) <= (double)(-(gtol * state.dginit)))
                {
                    info = 1;
                }

                //
                //        CHECK FOR TERMINATION.
                //
                if (info != 0)
                {

                    //
                    // Check guarantees provided by the function for INFO=1 or INFO=5
                    //
                    if (info == 1 || info == 5)
                    {
                        v = 0.0;
                        for (i = 0; i <= n - 1; i++)
                        {
                            v = v + (wa[i] - x[i]) * (wa[i] - x[i]);
                        }
                        if ((double)(f) >= (double)(state.finit) || (double)(v) == (double)(0.0))
                        {
                            info = 6;
                        }
                    }
                    stage = 0;
                    return;
                }

                //
                //        IN THE FIRST STAGE WE SEEK A STEP FOR WHICH THE MODIFIED
                //        FUNCTION HAS A NONPOSITIVE VALUE AND NONNEGATIVE DERIVATIVE.
                //
                if ((state.stage1 && (double)(f) <= (double)(state.ftest1)) && (double)(state.dg) >= (double)(Math.Min(ftol, gtol) * state.dginit))
                {
                    state.stage1 = false;
                }

                //
                //        A MODIFIED FUNCTION IS USED TO PREDICT THE STEP ONLY IF
                //        WE HAVE NOT OBTAINED A STEP FOR WHICH THE MODIFIED
                //        FUNCTION HAS A NONPOSITIVE FUNCTION VALUE AND NONNEGATIVE
                //        DERIVATIVE, AND IF A LOWER FUNCTION VALUE HAS BEEN
                //        OBTAINED BUT THE DECREASE IS NOT SUFFICIENT.
                //
                if ((state.stage1 && (double)(f) <= (double)(state.fx)) && (double)(f) > (double)(state.ftest1))
                {

                    //
                    //           DEFINE THE MODIFIED FUNCTION AND DERIVATIVE VALUES.
                    //
                    state.fm = f - stp * state.dgtest;
                    state.fxm = state.fx - state.stx * state.dgtest;
                    state.fym = state.fy - state.sty * state.dgtest;
                    state.dgm = state.dg - state.dgtest;
                    state.dgxm = state.dgx - state.dgtest;
                    state.dgym = state.dgy - state.dgtest;

                    //
                    //           CALL CSTEP TO UPDATE THE INTERVAL OF UNCERTAINTY
                    //           AND TO COMPUTE THE NEW STEP.
                    //
                    mcstep(ref state.stx, ref state.fxm, ref state.dgxm, ref state.sty, ref state.fym, ref state.dgym, ref stp, state.fm, state.dgm, ref state.brackt, state.stmin, state.stmax, ref state.infoc, _params);

                    //
                    //           RESET THE FUNCTION AND GRADIENT VALUES FOR F.
                    //
                    state.fx = state.fxm + state.stx * state.dgtest;
                    state.fy = state.fym + state.sty * state.dgtest;
                    state.dgx = state.dgxm + state.dgtest;
                    state.dgy = state.dgym + state.dgtest;
                }
                else
                {

                    //
                    //           CALL MCSTEP TO UPDATE THE INTERVAL OF UNCERTAINTY
                    //           AND TO COMPUTE THE NEW STEP.
                    //
                    mcstep(ref state.stx, ref state.fx, ref state.dgx, ref state.sty, ref state.fy, ref state.dgy, ref stp, f, state.dg, ref state.brackt, state.stmin, state.stmax, ref state.infoc, _params);
                }

                //
                //        FORCE A SUFFICIENT DECREASE IN THE SIZE OF THE
                //        INTERVAL OF UNCERTAINTY.
                //
                if (state.brackt)
                {
                    if ((double)(Math.Abs(state.sty - state.stx)) >= (double)(p66 * state.width1))
                    {
                        stp = state.stx + p5 * (state.sty - state.stx);
                    }
                    state.width1 = state.width;
                    state.width = Math.Abs(state.sty - state.stx);
                }

                //
                //  NEXT.
                //
                stage = 3;
                continue;
            }
        }
    }


    /*************************************************************************
    These functions perform Armijo line search using  at  most  FMAX  function
    evaluations.  It  doesn't  enforce  some  kind  of  " sufficient decrease"
    criterion - it just tries different Armijo steps and returns optimum found
    so far.

    Optimization is done using F-rcomm interface:
    * ArmijoCreate initializes State structure
      (reusing previously allocated buffers)
    * ArmijoIteration is subsequently called
    * ArmijoResults returns results

    INPUT PARAMETERS:
        N       -   problem size
        X       -   array[N], starting point
        F       -   F(X+S*STP)
        S       -   step direction, S>0
        STP     -   step length
        STPMAX  -   maximum value for STP or zero (if no limit is imposed)
        FMAX    -   maximum number of function evaluations
        State   -   optimization state

      -- ALGLIB --
         Copyright 05.10.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void armijocreate(int n,
        double[] x,
        double f,
        double[] s,
        double stp,
        double stpmax,
        int fmax,
        armijostate state,
        xparams _params)
    {
        int i_ = 0;

        if (ap.len(state.x) < n)
        {
            state.x = new double[n];
        }
        if (ap.len(state.xbase) < n)
        {
            state.xbase = new double[n];
        }
        if (ap.len(state.s) < n)
        {
            state.s = new double[n];
        }
        state.stpmax = stpmax;
        state.fmax = fmax;
        state.stplen = stp;
        state.fcur = f;
        state.n = n;
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.xbase[i_] = x[i_];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.s[i_] = s[i_];
        }
        state.rstate.ia = new int[0 + 1];
        state.rstate.ra = new double[0 + 1];
        state.rstate.stage = -1;
    }


    /*************************************************************************
    This is rcomm-based search function

      -- ALGLIB --
         Copyright 05.10.2010 by Bochkanov Sergey
    *************************************************************************/
    public static bool armijoiteration(armijostate state,
        xparams _params)
    {
        bool result = new bool();
        double v = 0;
        int n = 0;
        int i_ = 0;


        //
        // Reverse communication preparations
        // I know it looks ugly, but it works the same way
        // anywhere from C++ to Python.
        //
        // This code initializes locals by:
        // * random values determined during code
        //   generation - on first subroutine call
        // * values from previous call - on subsequent calls
        //
        if (state.rstate.stage >= 0)
        {
            n = state.rstate.ia[0];
            v = state.rstate.ra[0];
        }
        else
        {
            n = 359;
            v = -58.0;
        }
        if (state.rstate.stage == 0)
        {
            goto lbl_0;
        }
        if (state.rstate.stage == 1)
        {
            goto lbl_1;
        }
        if (state.rstate.stage == 2)
        {
            goto lbl_2;
        }
        if (state.rstate.stage == 3)
        {
            goto lbl_3;
        }

        //
        // Routine body
        //
        if (((double)(state.stplen) <= (double)(0) || (double)(state.stpmax) < (double)(0)) || state.fmax < 2)
        {
            state.info = 0;
            result = false;
            return result;
        }
        if ((double)(state.stplen) <= (double)(stpmin))
        {
            state.info = 4;
            result = false;
            return result;
        }
        n = state.n;
        state.nfev = 0;

        //
        // We always need F
        //
        state.needf = true;

        //
        // Bound StpLen
        //
        if ((double)(state.stplen) > (double)(state.stpmax) && (double)(state.stpmax) != (double)(0))
        {
            state.stplen = state.stpmax;
        }

        //
        // Increase length
        //
        v = state.stplen * armijofactor;
        if ((double)(v) > (double)(state.stpmax) && (double)(state.stpmax) != (double)(0))
        {
            v = state.stpmax;
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.x[i_] = state.xbase[i_];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.x[i_] = state.x[i_] + v * state.s[i_];
        }
        state.rstate.stage = 0;
        goto lbl_rcomm;
    lbl_0:
        state.nfev = state.nfev + 1;
        if ((double)(state.f) >= (double)(state.fcur))
        {
            goto lbl_4;
        }
        state.stplen = v;
        state.fcur = state.f;
    lbl_6:
        if (false)
        {
            goto lbl_7;
        }

        //
        // test stopping conditions
        //
        if (state.nfev >= state.fmax)
        {
            state.info = 3;
            result = false;
            return result;
        }
        if ((double)(state.stplen) >= (double)(state.stpmax))
        {
            state.info = 5;
            result = false;
            return result;
        }

        //
        // evaluate F
        //
        v = state.stplen * armijofactor;
        if ((double)(v) > (double)(state.stpmax) && (double)(state.stpmax) != (double)(0))
        {
            v = state.stpmax;
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.x[i_] = state.xbase[i_];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.x[i_] = state.x[i_] + v * state.s[i_];
        }
        state.rstate.stage = 1;
        goto lbl_rcomm;
    lbl_1:
        state.nfev = state.nfev + 1;

        //
        // make decision
        //
        if ((double)(state.f) < (double)(state.fcur))
        {
            state.stplen = v;
            state.fcur = state.f;
        }
        else
        {
            state.info = 1;
            result = false;
            return result;
        }
        goto lbl_6;
    lbl_7:
    lbl_4:

        //
        // Decrease length
        //
        v = state.stplen / armijofactor;
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.x[i_] = state.xbase[i_];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.x[i_] = state.x[i_] + v * state.s[i_];
        }
        state.rstate.stage = 2;
        goto lbl_rcomm;
    lbl_2:
        state.nfev = state.nfev + 1;
        if ((double)(state.f) >= (double)(state.fcur))
        {
            goto lbl_8;
        }
        state.stplen = state.stplen / armijofactor;
        state.fcur = state.f;
    lbl_10:
        if (false)
        {
            goto lbl_11;
        }

        //
        // test stopping conditions
        //
        if (state.nfev >= state.fmax)
        {
            state.info = 3;
            result = false;
            return result;
        }
        if ((double)(state.stplen) <= (double)(stpmin))
        {
            state.info = 4;
            result = false;
            return result;
        }

        //
        // evaluate F
        //
        v = state.stplen / armijofactor;
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.x[i_] = state.xbase[i_];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.x[i_] = state.x[i_] + v * state.s[i_];
        }
        state.rstate.stage = 3;
        goto lbl_rcomm;
    lbl_3:
        state.nfev = state.nfev + 1;

        //
        // make decision
        //
        if ((double)(state.f) < (double)(state.fcur))
        {
            state.stplen = state.stplen / armijofactor;
            state.fcur = state.f;
        }
        else
        {
            state.info = 1;
            result = false;
            return result;
        }
        goto lbl_10;
    lbl_11:
    lbl_8:

        //
        // Nothing to be done
        //
        state.info = 1;
        result = false;
        return result;

    //
    // Saving state
    //
    lbl_rcomm:
        result = true;
        state.rstate.ia[0] = n;
        state.rstate.ra[0] = v;
        return result;
    }


    /*************************************************************************
    Results of Armijo search

    OUTPUT PARAMETERS:
        INFO    -   on output it is set to one of the return codes:
                    * 0     improper input params
                    * 1     optimum step is found with at most FMAX evaluations
                    * 3     FMAX evaluations were used,
                            X contains optimum found so far
                    * 4     step is at lower bound STPMIN
                    * 5     step is at upper bound
        STP     -   step length (in case of failure it is still returned)
        F       -   function value (in case of failure it is still returned)

      -- ALGLIB --
         Copyright 05.10.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void armijoresults(armijostate state,
        ref int info,
        ref double stp,
        ref double f,
        xparams _params)
    {
        info = state.info;
        stp = state.stplen;
        f = state.fcur;
    }


    private static void mcstep(ref double stx,
        ref double fx,
        ref double dx,
        ref double sty,
        ref double fy,
        ref double dy,
        ref double stp,
        double fp,
        double dp,
        ref bool brackt,
        double stmin,
        double stmax,
        ref int info,
        xparams _params)
    {
        bool bound = new bool();
        double gamma = 0;
        double p = 0;
        double q = 0;
        double r = 0;
        double s = 0;
        double sgnd = 0;
        double stpc = 0;
        double stpf = 0;
        double stpq = 0;
        double theta = 0;

        info = 0;

        //
        //     CHECK THE INPUT PARAMETERS FOR ERRORS.
        //
        if (((brackt && ((double)(stp) <= (double)(Math.Min(stx, sty)) || (double)(stp) >= (double)(Math.Max(stx, sty)))) || (double)(dx * (stp - stx)) >= (double)(0)) || (double)(stmax) < (double)(stmin))
        {
            return;
        }

        //
        //     DETERMINE IF THE DERIVATIVES HAVE OPPOSITE SIGN.
        //
        sgnd = dp * (dx / Math.Abs(dx));

        //
        //     FIRST CASE. A HIGHER FUNCTION VALUE.
        //     THE MINIMUM IS BRACKETED. IF THE CUBIC STEP IS CLOSER
        //     TO STX THAN THE QUADRATIC STEP, THE CUBIC STEP IS TAKEN,
        //     ELSE THE AVERAGE OF THE CUBIC AND QUADRATIC STEPS IS TAKEN.
        //
        if ((double)(fp) > (double)(fx))
        {
            info = 1;
            bound = true;
            theta = 3 * (fx - fp) / (stp - stx) + dx + dp;
            s = Math.Max(Math.Abs(theta), Math.Max(Math.Abs(dx), Math.Abs(dp)));
            gamma = s * Math.Sqrt(math.sqr(theta / s) - dx / s * (dp / s));
            if ((double)(stp) < (double)(stx))
            {
                gamma = -gamma;
            }
            p = gamma - dx + theta;
            q = gamma - dx + gamma + dp;
            r = p / q;
            stpc = stx + r * (stp - stx);
            stpq = stx + dx / ((fx - fp) / (stp - stx) + dx) / 2 * (stp - stx);
            if ((double)(Math.Abs(stpc - stx)) < (double)(Math.Abs(stpq - stx)))
            {
                stpf = stpc;
            }
            else
            {
                stpf = stpc + (stpq - stpc) / 2;
            }
            brackt = true;
        }
        else
        {
            if ((double)(sgnd) < (double)(0))
            {

                //
                //     SECOND CASE. A LOWER FUNCTION VALUE AND DERIVATIVES OF
                //     OPPOSITE SIGN. THE MINIMUM IS BRACKETED. IF THE CUBIC
                //     STEP IS CLOSER TO STX THAN THE QUADRATIC (SECANT) STEP,
                //     THE CUBIC STEP IS TAKEN, ELSE THE QUADRATIC STEP IS TAKEN.
                //
                info = 2;
                bound = false;
                theta = 3 * (fx - fp) / (stp - stx) + dx + dp;
                s = Math.Max(Math.Abs(theta), Math.Max(Math.Abs(dx), Math.Abs(dp)));
                gamma = s * Math.Sqrt(math.sqr(theta / s) - dx / s * (dp / s));
                if ((double)(stp) > (double)(stx))
                {
                    gamma = -gamma;
                }
                p = gamma - dp + theta;
                q = gamma - dp + gamma + dx;
                r = p / q;
                stpc = stp + r * (stx - stp);
                stpq = stp + dp / (dp - dx) * (stx - stp);
                if ((double)(Math.Abs(stpc - stp)) > (double)(Math.Abs(stpq - stp)))
                {
                    stpf = stpc;
                }
                else
                {
                    stpf = stpq;
                }
                brackt = true;
            }
            else
            {
                if ((double)(Math.Abs(dp)) < (double)(Math.Abs(dx)))
                {

                    //
                    //     THIRD CASE. A LOWER FUNCTION VALUE, DERIVATIVES OF THE
                    //     SAME SIGN, AND THE MAGNITUDE OF THE DERIVATIVE DECREASES.
                    //     THE CUBIC STEP IS ONLY USED IF THE CUBIC TENDS TO INFINITY
                    //     IN THE DIRECTION OF THE STEP OR IF THE MINIMUM OF THE CUBIC
                    //     IS BEYOND STP. OTHERWISE THE CUBIC STEP IS DEFINED TO BE
                    //     EITHER STPMIN OR STPMAX. THE QUADRATIC (SECANT) STEP IS ALSO
                    //     COMPUTED AND IF THE MINIMUM IS BRACKETED THEN THE THE STEP
                    //     CLOSEST TO STX IS TAKEN, ELSE THE STEP FARTHEST AWAY IS TAKEN.
                    //
                    info = 3;
                    bound = true;
                    theta = 3 * (fx - fp) / (stp - stx) + dx + dp;
                    s = Math.Max(Math.Abs(theta), Math.Max(Math.Abs(dx), Math.Abs(dp)));

                    //
                    //        THE CASE GAMMA = 0 ONLY ARISES IF THE CUBIC DOES NOT TEND
                    //        TO INFINITY IN THE DIRECTION OF THE STEP.
                    //
                    gamma = s * Math.Sqrt(Math.Max(0, math.sqr(theta / s) - dx / s * (dp / s)));
                    if ((double)(stp) > (double)(stx))
                    {
                        gamma = -gamma;
                    }
                    p = gamma - dp + theta;
                    q = gamma + (dx - dp) + gamma;
                    r = p / q;
                    if ((double)(r) < (double)(0) && (double)(gamma) != (double)(0))
                    {
                        stpc = stp + r * (stx - stp);
                    }
                    else
                    {
                        if ((double)(stp) > (double)(stx))
                        {
                            stpc = stmax;
                        }
                        else
                        {
                            stpc = stmin;
                        }
                    }
                    stpq = stp + dp / (dp - dx) * (stx - stp);
                    if (brackt)
                    {
                        if ((double)(Math.Abs(stp - stpc)) < (double)(Math.Abs(stp - stpq)))
                        {
                            stpf = stpc;
                        }
                        else
                        {
                            stpf = stpq;
                        }
                    }
                    else
                    {
                        if ((double)(Math.Abs(stp - stpc)) > (double)(Math.Abs(stp - stpq)))
                        {
                            stpf = stpc;
                        }
                        else
                        {
                            stpf = stpq;
                        }
                    }
                }
                else
                {

                    //
                    //     FOURTH CASE. A LOWER FUNCTION VALUE, DERIVATIVES OF THE
                    //     SAME SIGN, AND THE MAGNITUDE OF THE DERIVATIVE DOES
                    //     NOT DECREASE. IF THE MINIMUM IS NOT BRACKETED, THE STEP
                    //     IS EITHER STPMIN OR STPMAX, ELSE THE CUBIC STEP IS TAKEN.
                    //
                    info = 4;
                    bound = false;
                    if (brackt)
                    {
                        theta = 3 * (fp - fy) / (sty - stp) + dy + dp;
                        s = Math.Max(Math.Abs(theta), Math.Max(Math.Abs(dy), Math.Abs(dp)));
                        gamma = s * Math.Sqrt(math.sqr(theta / s) - dy / s * (dp / s));
                        if ((double)(stp) > (double)(sty))
                        {
                            gamma = -gamma;
                        }
                        p = gamma - dp + theta;
                        q = gamma - dp + gamma + dy;
                        r = p / q;
                        stpc = stp + r * (sty - stp);
                        stpf = stpc;
                    }
                    else
                    {
                        if ((double)(stp) > (double)(stx))
                        {
                            stpf = stmax;
                        }
                        else
                        {
                            stpf = stmin;
                        }
                    }
                }
            }
        }

        //
        //     UPDATE THE INTERVAL OF UNCERTAINTY. THIS UPDATE DOES NOT
        //     DEPEND ON THE NEW STEP OR THE CASE ANALYSIS ABOVE.
        //
        if ((double)(fp) > (double)(fx))
        {
            sty = stp;
            fy = fp;
            dy = dp;
        }
        else
        {
            if ((double)(sgnd) < (double)(0.0))
            {
                sty = stx;
                fy = fx;
                dy = dx;
            }
            stx = stp;
            fx = fp;
            dx = dp;
        }

        //
        //     COMPUTE THE NEW STEP AND SAFEGUARD IT.
        //
        stpf = Math.Min(stmax, stpf);
        stpf = Math.Max(stmin, stpf);
        stp = stpf;
        if (brackt && bound)
        {
            if ((double)(sty) > (double)(stx))
            {
                stp = Math.Min(stx + 0.66 * (sty - stx), stp);
            }
            else
            {
                stp = Math.Max(stx + 0.66 * (sty - stx), stp);
            }
        }
    }


}

