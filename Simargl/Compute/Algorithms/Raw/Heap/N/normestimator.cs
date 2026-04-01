#pragma warning disable CS8618
#pragma warning disable CS1591

using System;

namespace Simargl.Algorithms.Raw;

public class normestimator
{
    /*************************************************************************
    This object stores state of the iterative norm estimation algorithm.

    You should use ALGLIB functions to work with this object.
    *************************************************************************/
    public class normestimatorstate : apobject
    {
        public int n;
        public int m;
        public int nstart;
        public int nits;
        public int seedval;
        public double[] x0;
        public double[] x1;
        public double[] t;
        public double[] xbest;
        public hqrnd.hqrndstate r;
        public double[] x;
        public double[] mv;
        public double[] mtv;
        public bool needmv;
        public bool needmtv;
        public double repnorm;
        public rcommstate rstate;
        public normestimatorstate()
        {
            init();
        }
        public override void init()
        {
            x0 = new double[0];
            x1 = new double[0];
            t = new double[0];
            xbest = new double[0];
            r = new hqrnd.hqrndstate();
            x = new double[0];
            mv = new double[0];
            mtv = new double[0];
            rstate = new rcommstate();
        }
        public override apobject make_copy()
        {
            normestimatorstate _result = new normestimatorstate();
            _result.n = n;
            _result.m = m;
            _result.nstart = nstart;
            _result.nits = nits;
            _result.seedval = seedval;
            _result.x0 = (double[])x0.Clone();
            _result.x1 = (double[])x1.Clone();
            _result.t = (double[])t.Clone();
            _result.xbest = (double[])xbest.Clone();
            _result.r = (hqrnd.hqrndstate)r.make_copy();
            _result.x = (double[])x.Clone();
            _result.mv = (double[])mv.Clone();
            _result.mtv = (double[])mtv.Clone();
            _result.needmv = needmv;
            _result.needmtv = needmtv;
            _result.repnorm = repnorm;
            _result.rstate = (rcommstate)rstate.make_copy();
            return _result;
        }
    };




    /*************************************************************************
    This procedure initializes matrix norm estimator.

    USAGE:
    1. User initializes algorithm state with NormEstimatorCreate() call
    2. User calls NormEstimatorEstimateSparse() (or NormEstimatorIteration())
    3. User calls NormEstimatorResults() to get solution.
       
    INPUT PARAMETERS:
        M       -   number of rows in the matrix being estimated, M>0
        N       -   number of columns in the matrix being estimated, N>0
        NStart  -   number of random starting vectors
                    recommended value - at least 5.
        NIts    -   number of iterations to do with best starting vector
                    recommended value - at least 5.

    OUTPUT PARAMETERS:
        State   -   structure which stores algorithm state

        
    NOTE: this algorithm is effectively deterministic, i.e. it always  returns
    same result when repeatedly called for the same matrix. In fact, algorithm
    uses randomized starting vectors, but internal  random  numbers  generator
    always generates same sequence of the random values (it is a  feature, not
    bug).

    Algorithm can be made non-deterministic with NormEstimatorSetSeed(0) call.

      -- ALGLIB --
         Copyright 06.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void normestimatorcreate(int m,
        int n,
        int nstart,
        int nits,
        normestimatorstate state,
        xparams _params)
    {
        ap.assert(m > 0, "NormEstimatorCreate: M<=0");
        ap.assert(n > 0, "NormEstimatorCreate: N<=0");
        ap.assert(nstart > 0, "NormEstimatorCreate: NStart<=0");
        ap.assert(nits > 0, "NormEstimatorCreate: NIts<=0");
        state.m = m;
        state.n = n;
        state.nstart = nstart;
        state.nits = nits;
        state.seedval = 11;
        hqrnd.hqrndrandomize(state.r, _params);
        state.x0 = new double[state.n];
        state.t = new double[state.m];
        state.x1 = new double[state.n];
        state.xbest = new double[state.n];
        state.x = new double[Math.Max(state.n, state.m)];
        state.mv = new double[state.m];
        state.mtv = new double[state.n];
        state.rstate.ia = new int[3 + 1];
        state.rstate.ra = new double[2 + 1];
        state.rstate.stage = -1;
    }


    /*************************************************************************
    This function changes seed value used by algorithm. In some cases we  need
    deterministic processing, i.e. subsequent calls must return equal results,
    in other cases we need non-deterministic algorithm which returns different
    results for the same matrix on every pass.

    Setting zero seed will lead to non-deterministic algorithm, while non-zero 
    value will make our algorithm deterministic.

    INPUT PARAMETERS:
        State       -   norm estimator state, must be initialized with a  call
                        to NormEstimatorCreate()
        SeedVal     -   seed value, >=0. Zero value = non-deterministic algo.

      -- ALGLIB --
         Copyright 06.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void normestimatorsetseed(normestimatorstate state,
        int seedval,
        xparams _params)
    {
        ap.assert(seedval >= 0, "NormEstimatorSetSeed: SeedVal<0");
        state.seedval = seedval;
    }


    /*************************************************************************

      -- ALGLIB --
         Copyright 06.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static bool normestimatoriteration(normestimatorstate state,
        xparams _params)
    {
        bool result = new bool();
        int n = 0;
        int m = 0;
        int i = 0;
        int itcnt = 0;
        double v = 0;
        double growth = 0;
        double bestgrowth = 0;
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
            m = state.rstate.ia[1];
            i = state.rstate.ia[2];
            itcnt = state.rstate.ia[3];
            v = state.rstate.ra[0];
            growth = state.rstate.ra[1];
            bestgrowth = state.rstate.ra[2];
        }
        else
        {
            n = 359;
            m = -58;
            i = -919;
            itcnt = -909;
            v = 81.0;
            growth = 255.0;
            bestgrowth = 74.0;
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
        n = state.n;
        m = state.m;
        if (state.seedval > 0)
        {
            hqrnd.hqrndseed(state.seedval, state.seedval + 2, state.r, _params);
        }
        bestgrowth = 0;
        state.xbest[0] = 1;
        for (i = 1; i <= n - 1; i++)
        {
            state.xbest[i] = 0;
        }
        itcnt = 0;
    lbl_4:
        if (itcnt > state.nstart - 1)
        {
            goto lbl_6;
        }
        do
        {
            v = 0;
            for (i = 0; i <= n - 1; i++)
            {
                state.x0[i] = hqrnd.hqrndnormal(state.r, _params);
                v = v + math.sqr(state.x0[i]);
            }
        }
        while ((double)(v) == (double)(0));
        v = 1 / Math.Sqrt(v);
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.x0[i_] = v * state.x0[i_];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.x[i_] = state.x0[i_];
        }
        state.needmv = true;
        state.needmtv = false;
        state.rstate.stage = 0;
        goto lbl_rcomm;
    lbl_0:
        for (i_ = 0; i_ <= m - 1; i_++)
        {
            state.x[i_] = state.mv[i_];
        }
        state.needmv = false;
        state.needmtv = true;
        state.rstate.stage = 1;
        goto lbl_rcomm;
    lbl_1:
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.x1[i_] = state.mtv[i_];
        }
        v = 0;
        for (i = 0; i <= n - 1; i++)
        {
            v = v + math.sqr(state.x1[i]);
        }
        growth = Math.Sqrt(Math.Sqrt(v));
        if ((double)(growth) > (double)(bestgrowth))
        {
            v = 1 / Math.Sqrt(v);
            for (i_ = 0; i_ <= n - 1; i_++)
            {
                state.xbest[i_] = v * state.x1[i_];
            }
            bestgrowth = growth;
        }
        itcnt = itcnt + 1;
        goto lbl_4;
    lbl_6:
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.x0[i_] = state.xbest[i_];
        }
        itcnt = 0;
    lbl_7:
        if (itcnt > state.nits - 1)
        {
            goto lbl_9;
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.x[i_] = state.x0[i_];
        }
        state.needmv = true;
        state.needmtv = false;
        state.rstate.stage = 2;
        goto lbl_rcomm;
    lbl_2:
        for (i_ = 0; i_ <= m - 1; i_++)
        {
            state.x[i_] = state.mv[i_];
        }
        state.needmv = false;
        state.needmtv = true;
        state.rstate.stage = 3;
        goto lbl_rcomm;
    lbl_3:
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.x1[i_] = state.mtv[i_];
        }
        v = 0;
        for (i = 0; i <= n - 1; i++)
        {
            v = v + math.sqr(state.x1[i]);
        }
        state.repnorm = Math.Sqrt(Math.Sqrt(v));
        if ((double)(v) != (double)(0))
        {
            v = 1 / Math.Sqrt(v);
            for (i_ = 0; i_ <= n - 1; i_++)
            {
                state.x0[i_] = v * state.x1[i_];
            }
        }
        itcnt = itcnt + 1;
        goto lbl_7;
    lbl_9:
        result = false;
        return result;

    //
    // Saving state
    //
    lbl_rcomm:
        result = true;
        state.rstate.ia[0] = n;
        state.rstate.ia[1] = m;
        state.rstate.ia[2] = i;
        state.rstate.ia[3] = itcnt;
        state.rstate.ra[0] = v;
        state.rstate.ra[1] = growth;
        state.rstate.ra[2] = bestgrowth;
        return result;
    }


    /*************************************************************************
    This function estimates norm of the sparse M*N matrix A.

    INPUT PARAMETERS:
        State       -   norm estimator state, must be initialized with a  call
                        to NormEstimatorCreate()
        A           -   sparse M*N matrix, must be converted to CRS format
                        prior to calling this function.

    After this function  is  over  you can call NormEstimatorResults() to get 
    estimate of the norm(A).

      -- ALGLIB --
         Copyright 06.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void normestimatorestimatesparse(normestimatorstate state,
        sparse.sparsematrix a,
        xparams _params)
    {
        normestimatorrestart(state, _params);
        while (normestimatoriteration(state, _params))
        {
            if (state.needmv)
            {
                sparse.sparsemv(a, state.x, ref state.mv, _params);
                continue;
            }
            if (state.needmtv)
            {
                sparse.sparsemtv(a, state.x, ref state.mtv, _params);
                continue;
            }
        }
    }


    /*************************************************************************
    Matrix norm estimation results

    INPUT PARAMETERS:
        State   -   algorithm state

    OUTPUT PARAMETERS:
        Nrm     -   estimate of the matrix norm, Nrm>=0

      -- ALGLIB --
         Copyright 06.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void normestimatorresults(normestimatorstate state,
        ref double nrm,
        xparams _params)
    {
        nrm = 0;

        nrm = state.repnorm;
    }


    /*************************************************************************
    This  function  restarts estimator and prepares it for the next estimation
    round.

    INPUT PARAMETERS:
        State   -   algorithm state
      -- ALGLIB --
         Copyright 06.12.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void normestimatorrestart(normestimatorstate state,
        xparams _params)
    {
        state.rstate.ia = new int[3 + 1];
        state.rstate.ra = new double[2 + 1];
        state.rstate.stage = -1;
    }


}
