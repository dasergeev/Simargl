#pragma warning disable CS8618
#pragma warning disable CS1591

using System;

namespace Simargl.Algorithms.Raw;

public class fbls
{
    /*************************************************************************
    Structure which stores state of linear CG solver between subsequent calls
    of FBLSCgIteration(). Initialized with FBLSCGCreate().

    USAGE:
    1. call to FBLSCGCreate()
    2. F:=FBLSCgIteration(State)
    3. if F is False, iterations are over
    4. otherwise, fill State.AX with A*x, State.XAX with x'*A*x
    5. goto 2

    If you want to rerminate iterations, pass zero or negative value to XAX.

    FIELDS:
        E1      -   2-norm of residual at the start
        E2      -   2-norm of residual at the end
        X       -   on return from FBLSCgIteration() it contains vector for
                    matrix-vector product
        AX      -   must be filled with A*x if FBLSCgIteration() returned True
        XAX     -   must be filled with x'*A*x
        XK      -   contains result (if FBLSCgIteration() returned False)
        
    Other fields are private and should not be used by outsiders.
    *************************************************************************/
    public class fblslincgstate : apobject
    {
        public double e1;
        public double e2;
        public double[] x;
        public double[] ax;
        public double xax;
        public int n;
        public double[] rk;
        public double[] rk1;
        public double[] xk;
        public double[] xk1;
        public double[] pk;
        public double[] pk1;
        public double[] b;
        public rcommstate rstate;
        public double[] tmp2;
        public fblslincgstate()
        {
            init();
        }
        public override void init()
        {
            x = new double[0];
            ax = new double[0];
            rk = new double[0];
            rk1 = new double[0];
            xk = new double[0];
            xk1 = new double[0];
            pk = new double[0];
            pk1 = new double[0];
            b = new double[0];
            rstate = new rcommstate();
            tmp2 = new double[0];
        }
        public override apobject make_copy()
        {
            fblslincgstate _result = new fblslincgstate();
            _result.e1 = e1;
            _result.e2 = e2;
            _result.x = (double[])x.Clone();
            _result.ax = (double[])ax.Clone();
            _result.xax = xax;
            _result.n = n;
            _result.rk = (double[])rk.Clone();
            _result.rk1 = (double[])rk1.Clone();
            _result.xk = (double[])xk.Clone();
            _result.xk1 = (double[])xk1.Clone();
            _result.pk = (double[])pk.Clone();
            _result.pk1 = (double[])pk1.Clone();
            _result.b = (double[])b.Clone();
            _result.rstate = (rcommstate)rstate.make_copy();
            _result.tmp2 = (double[])tmp2.Clone();
            return _result;
        }
    };


    /*************************************************************************
    Structure which stores state of basic GMRES(k)  solver  between subsequent
    calls of FBLSGMRESIteration(). Initialized with FBLSGMRESCreate().

    USAGE:
    1. call to FBLSCGCreate()
    2. F:=FBLSGMRESIteration(State)
    3. if F is False, iterations are over
    4. otherwise, fill State.AX with A*x
    5. goto 2

    RCOMM FIELDS:
        X       -   on return from FBLSCgIteration() it contains vector for
                    matrix-vector product
        AX      -   must be filled with A*x if FBLSCgIteration() returned True
        
    RESULT
        XS      -   contains result (if FBLSCgIteration() returned False)
        State   -   following fields can be used:
                    * ItsPerformed
                    * RetCode
        
    Other fields are private and should not be used by outsiders:
        Qi      -   rows store orthonormal basis of the Krylov subspace
        AQi     -   rows store products A*Qi
    *************************************************************************/
    public class fblsgmresstate : apobject
    {
        public double[] b;
        public double[] x;
        public double[] ax;
        public double[] xs;
        public double[,] qi;
        public double[,] aqi;
        public double[,] h;
        public double[,] hq;
        public double[,] hr;
        public double[] hqb;
        public double[] ys;
        public double[] tmp0;
        public double[] tmp1;
        public int n;
        public int itscnt;
        public double epsort;
        public double epsres;
        public double epsred;
        public double epsdiag;
        public int itsperformed;
        public int retcode;
        public double reprelres;
        public rcommstate rstate;
        public fblsgmresstate()
        {
            init();
        }
        public override void init()
        {
            b = new double[0];
            x = new double[0];
            ax = new double[0];
            xs = new double[0];
            qi = new double[0, 0];
            aqi = new double[0, 0];
            h = new double[0, 0];
            hq = new double[0, 0];
            hr = new double[0, 0];
            hqb = new double[0];
            ys = new double[0];
            tmp0 = new double[0];
            tmp1 = new double[0];
            rstate = new rcommstate();
        }
        public override apobject make_copy()
        {
            fblsgmresstate _result = new fblsgmresstate();
            _result.b = (double[])b.Clone();
            _result.x = (double[])x.Clone();
            _result.ax = (double[])ax.Clone();
            _result.xs = (double[])xs.Clone();
            _result.qi = (double[,])qi.Clone();
            _result.aqi = (double[,])aqi.Clone();
            _result.h = (double[,])h.Clone();
            _result.hq = (double[,])hq.Clone();
            _result.hr = (double[,])hr.Clone();
            _result.hqb = (double[])hqb.Clone();
            _result.ys = (double[])ys.Clone();
            _result.tmp0 = (double[])tmp0.Clone();
            _result.tmp1 = (double[])tmp1.Clone();
            _result.n = n;
            _result.itscnt = itscnt;
            _result.epsort = epsort;
            _result.epsres = epsres;
            _result.epsred = epsred;
            _result.epsdiag = epsdiag;
            _result.itsperformed = itsperformed;
            _result.retcode = retcode;
            _result.reprelres = reprelres;
            _result.rstate = (rcommstate)rstate.make_copy();
            return _result;
        }
    };




    /*************************************************************************
    Basic Cholesky solver for ScaleA*Cholesky(A)'*x = y.

    This subroutine assumes that:
    * A*ScaleA is well scaled
    * A is well-conditioned, so no zero divisions or overflow may occur

    INPUT PARAMETERS:
        CHA     -   Cholesky decomposition of A
        SqrtScaleA- square root of scale factor ScaleA
        N       -   matrix size, N>=0.
        IsUpper -   storage type
        XB      -   right part
        Tmp     -   buffer; function automatically allocates it, if it is  too
                    small.  It  can  be  reused  if function is called several
                    times.
                    
    OUTPUT PARAMETERS:
        XB      -   solution

    NOTE 1: no assertion or tests are done during algorithm operation
    NOTE 2: N=0 will force algorithm to silently return

      -- ALGLIB --
         Copyright 13.10.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void fblscholeskysolve(double[,] cha,
        double sqrtscalea,
        int n,
        bool isupper,
        double[] xb,
        ref double[] tmp,
        xparams _params)
    {
        double v = 0;
        int i_ = 0;

        if (n <= 0)
        {
            return;
        }
        if (ap.len(tmp) < n)
        {
            tmp = new double[n];
        }

        //
        // Scale right part
        //
        v = 1 / math.sqr(sqrtscalea);
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            xb[i_] = v * xb[i_];
        }

        //
        // Solve A = L*L' or A=U'*U
        //
        if (isupper)
        {

            //
            // Solve U'*y=b first.
            //
            ablas.rmatrixtrsv(n, cha, 0, 0, true, false, 1, xb, 0, _params);

            //
            // Solve U*x=y then.
            //
            ablas.rmatrixtrsv(n, cha, 0, 0, true, false, 0, xb, 0, _params);
        }
        else
        {

            //
            // Solve L*y=b first
            //
            ablas.rmatrixtrsv(n, cha, 0, 0, false, false, 0, xb, 0, _params);

            //
            // Solve L'*x=y then.
            //
            ablas.rmatrixtrsv(n, cha, 0, 0, false, false, 1, xb, 0, _params);
        }
    }


    /*************************************************************************
    Fast basic linear solver: linear SPD CG

    Solves (A^T*A + alpha*I)*x = b where:
    * A is MxN matrix
    * alpha>0 is a scalar
    * I is NxN identity matrix
    * b is Nx1 vector
    * X is Nx1 unknown vector.

    N iterations of linear conjugate gradient are used to solve problem.

    INPUT PARAMETERS:
        A   -   array[M,N], matrix
        M   -   number of rows
        N   -   number of unknowns
        B   -   array[N], right part
        X   -   initial approxumation, array[N]
        Buf -   buffer; function automatically allocates it, if it is too
                small. It can be reused if function is called several times
                with same M and N.
                
    OUTPUT PARAMETERS:
        X   -   improved solution
        
    NOTES:
    *   solver checks quality of improved solution. If (because of problem
        condition number, numerical noise, etc.) new solution is WORSE than
        original approximation, then original approximation is returned.
    *   solver assumes that both A, B, Alpha are well scaled (i.e. they are
        less than sqrt(overflow) and greater than sqrt(underflow)).
        
      -- ALGLIB --
         Copyright 20.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void fblssolvecgx(double[,] a,
        int m,
        int n,
        double alpha,
        double[] b,
        ref double[] x,
        ref double[] buf,
        xparams _params)
    {
        int k = 0;
        int offsrk = 0;
        int offsrk1 = 0;
        int offsxk = 0;
        int offsxk1 = 0;
        int offspk = 0;
        int offspk1 = 0;
        int offstmp1 = 0;
        int offstmp2 = 0;
        int bs = 0;
        double e1 = 0;
        double e2 = 0;
        double rk2 = 0;
        double rk12 = 0;
        double pap = 0;
        double s = 0;
        double betak = 0;
        double v1 = 0;
        double v2 = 0;
        int i_ = 0;
        int i1_ = 0;


        //
        // Test for special case: B=0
        //
        v1 = 0.0;
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            v1 += b[i_] * b[i_];
        }
        if ((double)(v1) == (double)(0))
        {
            for (k = 0; k <= n - 1; k++)
            {
                x[k] = 0;
            }
            return;
        }

        //
        // Offsets inside Buf for:
        // * R[K], R[K+1]
        // * X[K], X[K+1]
        // * P[K], P[K+1]
        // * Tmp1 - array[M], Tmp2 - array[N]
        //
        offsrk = 0;
        offsrk1 = offsrk + n;
        offsxk = offsrk1 + n;
        offsxk1 = offsxk + n;
        offspk = offsxk1 + n;
        offspk1 = offspk + n;
        offstmp1 = offspk1 + n;
        offstmp2 = offstmp1 + m;
        bs = offstmp2 + n;
        if (ap.len(buf) < bs)
        {
            buf = new double[bs];
        }

        //
        // x(0) = x
        //
        i1_ = (0) - (offsxk);
        for (i_ = offsxk; i_ <= offsxk + n - 1; i_++)
        {
            buf[i_] = x[i_ + i1_];
        }

        //
        // r(0) = b-A*x(0)
        // RK2 = r(0)'*r(0)
        //
        ablas.rmatrixmv(m, n, a, 0, 0, 0, buf, offsxk, buf, offstmp1, _params);
        ablas.rmatrixmv(n, m, a, 0, 0, 1, buf, offstmp1, buf, offstmp2, _params);
        i1_ = (offsxk) - (offstmp2);
        for (i_ = offstmp2; i_ <= offstmp2 + n - 1; i_++)
        {
            buf[i_] = buf[i_] + alpha * buf[i_ + i1_];
        }
        i1_ = (0) - (offsrk);
        for (i_ = offsrk; i_ <= offsrk + n - 1; i_++)
        {
            buf[i_] = b[i_ + i1_];
        }
        i1_ = (offstmp2) - (offsrk);
        for (i_ = offsrk; i_ <= offsrk + n - 1; i_++)
        {
            buf[i_] = buf[i_] - buf[i_ + i1_];
        }
        rk2 = 0.0;
        for (i_ = offsrk; i_ <= offsrk + n - 1; i_++)
        {
            rk2 += buf[i_] * buf[i_];
        }
        i1_ = (offsrk) - (offspk);
        for (i_ = offspk; i_ <= offspk + n - 1; i_++)
        {
            buf[i_] = buf[i_ + i1_];
        }
        e1 = Math.Sqrt(rk2);

        //
        // Cycle
        //
        for (k = 0; k <= n - 1; k++)
        {

            //
            // Calculate A*p(k) - store in Buf[OffsTmp2:OffsTmp2+N-1]
            // and p(k)'*A*p(k)  - store in PAP
            //
            // If PAP=0, break (iteration is over)
            //
            ablas.rmatrixmv(m, n, a, 0, 0, 0, buf, offspk, buf, offstmp1, _params);
            v1 = 0.0;
            for (i_ = offstmp1; i_ <= offstmp1 + m - 1; i_++)
            {
                v1 += buf[i_] * buf[i_];
            }
            v2 = 0.0;
            for (i_ = offspk; i_ <= offspk + n - 1; i_++)
            {
                v2 += buf[i_] * buf[i_];
            }
            pap = v1 + alpha * v2;
            ablas.rmatrixmv(n, m, a, 0, 0, 1, buf, offstmp1, buf, offstmp2, _params);
            i1_ = (offspk) - (offstmp2);
            for (i_ = offstmp2; i_ <= offstmp2 + n - 1; i_++)
            {
                buf[i_] = buf[i_] + alpha * buf[i_ + i1_];
            }
            if ((double)(pap) == (double)(0))
            {
                break;
            }

            //
            // S = (r(k)'*r(k))/(p(k)'*A*p(k))
            //
            s = rk2 / pap;

            //
            // x(k+1) = x(k) + S*p(k)
            //
            i1_ = (offsxk) - (offsxk1);
            for (i_ = offsxk1; i_ <= offsxk1 + n - 1; i_++)
            {
                buf[i_] = buf[i_ + i1_];
            }
            i1_ = (offspk) - (offsxk1);
            for (i_ = offsxk1; i_ <= offsxk1 + n - 1; i_++)
            {
                buf[i_] = buf[i_] + s * buf[i_ + i1_];
            }

            //
            // r(k+1) = r(k) - S*A*p(k)
            // RK12 = r(k+1)'*r(k+1)
            //
            // Break if r(k+1) small enough (when compared to r(k))
            //
            i1_ = (offsrk) - (offsrk1);
            for (i_ = offsrk1; i_ <= offsrk1 + n - 1; i_++)
            {
                buf[i_] = buf[i_ + i1_];
            }
            i1_ = (offstmp2) - (offsrk1);
            for (i_ = offsrk1; i_ <= offsrk1 + n - 1; i_++)
            {
                buf[i_] = buf[i_] - s * buf[i_ + i1_];
            }
            rk12 = 0.0;
            for (i_ = offsrk1; i_ <= offsrk1 + n - 1; i_++)
            {
                rk12 += buf[i_] * buf[i_];
            }
            if ((double)(Math.Sqrt(rk12)) <= (double)(100 * math.machineepsilon * Math.Sqrt(rk2)))
            {

                //
                // X(k) = x(k+1) before exit -
                // - because we expect to find solution at x(k)
                //
                i1_ = (offsxk1) - (offsxk);
                for (i_ = offsxk; i_ <= offsxk + n - 1; i_++)
                {
                    buf[i_] = buf[i_ + i1_];
                }
                break;
            }

            //
            // BetaK = RK12/RK2
            // p(k+1) = r(k+1)+betak*p(k)
            //
            betak = rk12 / rk2;
            i1_ = (offsrk1) - (offspk1);
            for (i_ = offspk1; i_ <= offspk1 + n - 1; i_++)
            {
                buf[i_] = buf[i_ + i1_];
            }
            i1_ = (offspk) - (offspk1);
            for (i_ = offspk1; i_ <= offspk1 + n - 1; i_++)
            {
                buf[i_] = buf[i_] + betak * buf[i_ + i1_];
            }

            //
            // r(k) := r(k+1)
            // x(k) := x(k+1)
            // p(k) := p(k+1)
            //
            i1_ = (offsrk1) - (offsrk);
            for (i_ = offsrk; i_ <= offsrk + n - 1; i_++)
            {
                buf[i_] = buf[i_ + i1_];
            }
            i1_ = (offsxk1) - (offsxk);
            for (i_ = offsxk; i_ <= offsxk + n - 1; i_++)
            {
                buf[i_] = buf[i_ + i1_];
            }
            i1_ = (offspk1) - (offspk);
            for (i_ = offspk; i_ <= offspk + n - 1; i_++)
            {
                buf[i_] = buf[i_ + i1_];
            }
            rk2 = rk12;
        }

        //
        // Calculate E2
        //
        ablas.rmatrixmv(m, n, a, 0, 0, 0, buf, offsxk, buf, offstmp1, _params);
        ablas.rmatrixmv(n, m, a, 0, 0, 1, buf, offstmp1, buf, offstmp2, _params);
        i1_ = (offsxk) - (offstmp2);
        for (i_ = offstmp2; i_ <= offstmp2 + n - 1; i_++)
        {
            buf[i_] = buf[i_] + alpha * buf[i_ + i1_];
        }
        i1_ = (0) - (offsrk);
        for (i_ = offsrk; i_ <= offsrk + n - 1; i_++)
        {
            buf[i_] = b[i_ + i1_];
        }
        i1_ = (offstmp2) - (offsrk);
        for (i_ = offsrk; i_ <= offsrk + n - 1; i_++)
        {
            buf[i_] = buf[i_] - buf[i_ + i1_];
        }
        v1 = 0.0;
        for (i_ = offsrk; i_ <= offsrk + n - 1; i_++)
        {
            v1 += buf[i_] * buf[i_];
        }
        e2 = Math.Sqrt(v1);

        //
        // Output result (if it was improved)
        //
        if ((double)(e2) < (double)(e1))
        {
            i1_ = (offsxk) - (0);
            for (i_ = 0; i_ <= n - 1; i_++)
            {
                x[i_] = buf[i_ + i1_];
            }
        }
    }


    /*************************************************************************
    Construction of linear conjugate gradient solver.

    State parameter passed using "shared" semantics (i.e. previous state is NOT
    erased). When it is already initialized, we can reause prevously allocated
    memory.

    INPUT PARAMETERS:
        X       -   initial solution
        B       -   right part
        N       -   system size
        State   -   structure; may be preallocated, if we want to reuse memory

    OUTPUT PARAMETERS:
        State   -   structure which is used by FBLSCGIteration() to store
                    algorithm state between subsequent calls.

    NOTE: no error checking is done; caller must check all parameters, prevent
          overflows, and so on.

      -- ALGLIB --
         Copyright 22.10.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void fblscgcreate(double[] x,
        double[] b,
        int n,
        fblslincgstate state,
        xparams _params)
    {
        int i_ = 0;

        if (ap.len(state.b) < n)
        {
            state.b = new double[n];
        }
        if (ap.len(state.rk) < n)
        {
            state.rk = new double[n];
        }
        if (ap.len(state.rk1) < n)
        {
            state.rk1 = new double[n];
        }
        if (ap.len(state.xk) < n)
        {
            state.xk = new double[n];
        }
        if (ap.len(state.xk1) < n)
        {
            state.xk1 = new double[n];
        }
        if (ap.len(state.pk) < n)
        {
            state.pk = new double[n];
        }
        if (ap.len(state.pk1) < n)
        {
            state.pk1 = new double[n];
        }
        if (ap.len(state.tmp2) < n)
        {
            state.tmp2 = new double[n];
        }
        if (ap.len(state.x) < n)
        {
            state.x = new double[n];
        }
        if (ap.len(state.ax) < n)
        {
            state.ax = new double[n];
        }
        state.n = n;
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.xk[i_] = x[i_];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.b[i_] = b[i_];
        }
        state.rstate.ia = new int[1 + 1];
        state.rstate.ra = new double[6 + 1];
        state.rstate.stage = -1;
    }


    /*************************************************************************
    Linear CG solver, function relying on reverse communication to calculate
    matrix-vector products.

    See comments for FBLSLinCGState structure for more info.

      -- ALGLIB --
         Copyright 22.10.2009 by Bochkanov Sergey
    *************************************************************************/
    public static bool fblscgiteration(fblslincgstate state,
        xparams _params)
    {
        bool result = new bool();
        int n = 0;
        int k = 0;
        double rk2 = 0;
        double rk12 = 0;
        double pap = 0;
        double s = 0;
        double betak = 0;
        double v1 = 0;
        double v2 = 0;
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
            k = state.rstate.ia[1];
            rk2 = state.rstate.ra[0];
            rk12 = state.rstate.ra[1];
            pap = state.rstate.ra[2];
            s = state.rstate.ra[3];
            betak = state.rstate.ra[4];
            v1 = state.rstate.ra[5];
            v2 = state.rstate.ra[6];
        }
        else
        {
            n = 359;
            k = -58;
            rk2 = -919.0;
            rk12 = -909.0;
            pap = 81.0;
            s = 255.0;
            betak = 74.0;
            v1 = -788.0;
            v2 = 809.0;
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

        //
        // Routine body
        //

        //
        // prepare locals
        //
        n = state.n;

        //
        // Test for special case: B=0
        //
        v1 = 0.0;
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            v1 += state.b[i_] * state.b[i_];
        }
        if ((double)(v1) == (double)(0))
        {
            for (k = 0; k <= n - 1; k++)
            {
                state.xk[k] = 0;
            }
            result = false;
            return result;
        }

        //
        // r(0) = b-A*x(0)
        // RK2 = r(0)'*r(0)
        //
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.x[i_] = state.xk[i_];
        }
        state.rstate.stage = 0;
        goto lbl_rcomm;
    lbl_0:
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.rk[i_] = state.b[i_];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.rk[i_] = state.rk[i_] - state.ax[i_];
        }
        rk2 = 0.0;
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            rk2 += state.rk[i_] * state.rk[i_];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.pk[i_] = state.rk[i_];
        }
        state.e1 = Math.Sqrt(rk2);

        //
        // Cycle
        //
        k = 0;
    lbl_3:
        if (k > n - 1)
        {
            goto lbl_5;
        }

        //
        // Calculate A*p(k) - store in State.Tmp2
        // and p(k)'*A*p(k)  - store in PAP
        //
        // If PAP=0, break (iteration is over)
        //
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.x[i_] = state.pk[i_];
        }
        state.rstate.stage = 1;
        goto lbl_rcomm;
    lbl_1:
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.tmp2[i_] = state.ax[i_];
        }
        pap = state.xax;
        if (!math.isfinite(pap))
        {
            goto lbl_5;
        }
        if ((double)(pap) <= (double)(0))
        {
            goto lbl_5;
        }

        //
        // S = (r(k)'*r(k))/(p(k)'*A*p(k))
        //
        s = rk2 / pap;

        //
        // x(k+1) = x(k) + S*p(k)
        //
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.xk1[i_] = state.xk[i_];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.xk1[i_] = state.xk1[i_] + s * state.pk[i_];
        }

        //
        // r(k+1) = r(k) - S*A*p(k)
        // RK12 = r(k+1)'*r(k+1)
        //
        // Break if r(k+1) small enough (when compared to r(k))
        //
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.rk1[i_] = state.rk[i_];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.rk1[i_] = state.rk1[i_] - s * state.tmp2[i_];
        }
        rk12 = 0.0;
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            rk12 += state.rk1[i_] * state.rk1[i_];
        }
        if ((double)(Math.Sqrt(rk12)) <= (double)(100 * math.machineepsilon * state.e1))
        {

            //
            // X(k) = x(k+1) before exit -
            // - because we expect to find solution at x(k)
            //
            for (i_ = 0; i_ <= n - 1; i_++)
            {
                state.xk[i_] = state.xk1[i_];
            }
            goto lbl_5;
        }

        //
        // BetaK = RK12/RK2
        // p(k+1) = r(k+1)+betak*p(k)
        //
        // NOTE: we expect that BetaK won't overflow because of
        // "Sqrt(RK12)<=100*MachineEpsilon*E1" test above.
        //
        betak = rk12 / rk2;
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.pk1[i_] = state.rk1[i_];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.pk1[i_] = state.pk1[i_] + betak * state.pk[i_];
        }

        //
        // r(k) := r(k+1)
        // x(k) := x(k+1)
        // p(k) := p(k+1)
        //
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.rk[i_] = state.rk1[i_];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.xk[i_] = state.xk1[i_];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.pk[i_] = state.pk1[i_];
        }
        rk2 = rk12;
        k = k + 1;
        goto lbl_3;
    lbl_5:

        //
        // Calculate E2
        //
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.x[i_] = state.xk[i_];
        }
        state.rstate.stage = 2;
        goto lbl_rcomm;
    lbl_2:
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.rk[i_] = state.b[i_];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.rk[i_] = state.rk[i_] - state.ax[i_];
        }
        v1 = 0.0;
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            v1 += state.rk[i_] * state.rk[i_];
        }
        state.e2 = Math.Sqrt(v1);
        result = false;
        return result;

    //
    // Saving state
    //
    lbl_rcomm:
        result = true;
        state.rstate.ia[0] = n;
        state.rstate.ia[1] = k;
        state.rstate.ra[0] = rk2;
        state.rstate.ra[1] = rk12;
        state.rstate.ra[2] = pap;
        state.rstate.ra[3] = s;
        state.rstate.ra[4] = betak;
        state.rstate.ra[5] = v1;
        state.rstate.ra[6] = v2;
        return result;
    }


    /*************************************************************************
    Construction of GMRES(k) solver.

    State parameter passed using "shared" semantics (i.e. previous state is NOT
    erased). When it is already initialized, we can reause prevously allocated
    memory.

    After (but not before!) initialization you can tweak following fields (they
    are initialized by default values, but you can change it):
    * State.EpsOrt - stop if norm of new candidate for orthogonalization is below EpsOrt
    * State.EpsRes - stop of residual decreased below EpsRes*|B|
    * State.EpsRed - stop if relative reduction of residual |R(k+1)|/|R(k)|>EpsRed

    INPUT PARAMETERS:
        B       -   right part
        N       -   system size
        K       -   iterations count, K>=1
        State   -   structure; may be preallocated, if we want to reuse memory

    OUTPUT PARAMETERS:
        State   -   structure which is used by FBLSGMRESIteration() to store
                    algorithm state between subsequent calls.

    NOTE: no error checking is done; caller must check all parameters, prevent
          overflows, and so on.

      -- ALGLIB --
         Copyright 18.11.2020 by Bochkanov Sergey
    *************************************************************************/
    public static void fblsgmrescreate(double[] b,
        int n,
        int k,
        fblsgmresstate state,
        xparams _params)
    {
        ap.assert((n > 0 && k > 0) && k <= n, "FBLSGMRESCreate: incorrect params");
        state.n = n;
        state.itscnt = k;
        state.epsort = (1000 + Math.Sqrt(n)) * math.machineepsilon;
        state.epsres = (1000 + Math.Sqrt(n)) * math.machineepsilon;
        state.epsred = 1.0;
        state.epsdiag = (10000 + n) * math.machineepsilon;
        state.itsperformed = 0;
        state.retcode = 0;
        ablasf.rcopyallocv(n, b, ref state.b, _params);
        ablasf.rallocv(n, ref state.x, _params);
        ablasf.rallocv(n, ref state.ax, _params);
        state.rstate.ia = new int[4 + 1];
        state.rstate.ra = new double[10 + 1];
        state.rstate.stage = -1;
    }


    /*************************************************************************
    Linear CG solver, function relying on reverse communication to calculate
    matrix-vector products.

    See comments for FBLSLinCGState structure for more info.

      -- ALGLIB --
         Copyright 22.10.2009 by Bochkanov Sergey
    *************************************************************************/
    public static bool fblsgmresiteration(fblsgmresstate state,
        xparams _params)
    {
        bool result = new bool();
        int n = 0;
        int itidx = 0;
        int kdim = 0;
        double rmax = 0;
        double rmindiag = 0;
        double cs = 0;
        double sn = 0;
        double v = 0;
        double vv = 0;
        double anrm = 0;
        double qnrm = 0;
        double bnrm = 0;
        double resnrm = 0;
        double prevresnrm = 0;
        int i = 0;
        int j = 0;


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
            itidx = state.rstate.ia[1];
            kdim = state.rstate.ia[2];
            i = state.rstate.ia[3];
            j = state.rstate.ia[4];
            rmax = state.rstate.ra[0];
            rmindiag = state.rstate.ra[1];
            cs = state.rstate.ra[2];
            sn = state.rstate.ra[3];
            v = state.rstate.ra[4];
            vv = state.rstate.ra[5];
            anrm = state.rstate.ra[6];
            qnrm = state.rstate.ra[7];
            bnrm = state.rstate.ra[8];
            resnrm = state.rstate.ra[9];
            prevresnrm = state.rstate.ra[10];
        }
        else
        {
            n = 205;
            itidx = -838;
            kdim = 939;
            i = -526;
            j = 763;
            rmax = -541.0;
            rmindiag = -698.0;
            cs = -900.0;
            sn = -318.0;
            v = -940.0;
            vv = 1016.0;
            anrm = -229.0;
            qnrm = -536.0;
            bnrm = 487.0;
            resnrm = -115.0;
            prevresnrm = 886.0;
        }
        if (state.rstate.stage == 0)
        {
            goto lbl_0;
        }

        //
        // Routine body
        //
        n = state.n;
        state.retcode = 1;

        //
        // Set up Q0
        //
        ablasf.rsetallocv(n, 0.0, ref state.xs, _params);
        bnrm = Math.Sqrt(ablasf.rdotv2(n, state.b, _params));
        if ((double)(bnrm) == (double)(0))
        {
            state.reprelres = 0;
            result = false;
            return result;
        }
        ablasf.rallocm(state.itscnt + 1, n, ref state.qi, _params);
        ablasf.rallocm(state.itscnt, n, ref state.aqi, _params);
        ablasf.rcopymulvr(n, 1 / bnrm, state.b, state.qi, 0, _params);
        ablasf.rsetallocm(state.itscnt + 1, state.itscnt, 0.0, ref state.h, _params);
        ablasf.rsetallocm(state.itscnt + 1, state.itscnt, 0.0, ref state.hr, _params);
        ablasf.rsetallocm(state.itscnt + 1, state.itscnt + 1, 0.0, ref state.hq, _params);
        for (i = 0; i <= state.itscnt; i++)
        {
            state.hq[i, i] = 1;
        }
        ablasf.rsetallocv(state.itscnt + 1, 0.0, ref state.hqb, _params);
        state.hqb[0] = bnrm;

        //
        // Perform iteration
        //
        resnrm = bnrm;
        kdim = 0;
        rmax = 0;
        rmindiag = 1.0E99;
        ablasf.rsetallocv(state.itscnt, 0.0, ref state.ys, _params);
        ablasf.rallocv(Math.Max(n, state.itscnt + 2), ref state.tmp0, _params);
        ablasf.rallocv(Math.Max(n, state.itscnt + 2), ref state.tmp1, _params);
        itidx = 0;
    lbl_1:
        if (itidx > state.itscnt - 1)
        {
            goto lbl_3;
        }
        prevresnrm = resnrm;
        state.reprelres = resnrm / bnrm;

        //
        // Compute A*Qi[ItIdx], then compute Qi[ItIdx+1]
        //
        ablasf.rcopyrv(n, state.qi, itidx, state.x, _params);
        state.rstate.stage = 0;
        goto lbl_rcomm;
    lbl_0:
        ablasf.rcopyvr(n, state.ax, state.aqi, itidx, _params);
        anrm = Math.Sqrt(ablasf.rdotv2(n, state.ax, _params));
        if ((double)(anrm) == (double)(0))
        {
            state.retcode = 2;
            goto lbl_3;
        }
        ablas.rowwisegramschmidt(state.qi, itidx + 1, n, state.ax, ref state.tmp0, true, _params);
        ablas.rowwisegramschmidt(state.qi, itidx + 1, n, state.ax, ref state.tmp1, true, _params);
        ablasf.raddvc(itidx + 1, 1.0, state.tmp0, state.h, itidx, _params);
        ablasf.raddvc(itidx + 1, 1.0, state.tmp1, state.h, itidx, _params);
        qnrm = Math.Sqrt(ablasf.rdotv2(n, state.ax, _params));
        state.h[itidx + 1, itidx] = qnrm;
        ablasf.rmulv(n, 1 / apserv.coalesce(qnrm, 1, _params), state.ax, _params);
        ablasf.rcopyvr(n, state.ax, state.qi, itidx + 1, _params);

        //
        // We have QR decomposition of H from the previous iteration:
        // * (ItIdx+1)*(ItIdx+1) orthogonal HQ embedded into larger (ItIdx+2)*(ItIdx+2) identity matrix
        // * (ItIdx+1)*ItIdx     triangular HR embedded into larger (ItIdx+2)*(ItIdx+1) zero matrix
        //
        // We just have to update QR decomposition after one more column is added to H:
        // * multiply this column by HQ to obtain (ItIdx+2)-dimensional vector X
        // * generate rotation to nullify last element of X to obtain (ItIdx+1)-dimensional vector Y
        //   that is copied into (ItIdx+1)-th column of HR
        // * apply same rotation to HQ
        // * apply same rotation to HQB - current right-hand side
        //
        ablasf.rcopycv(itidx + 2, state.h, itidx, state.tmp0, _params);
        ablas.rmatrixgemv(itidx + 2, itidx + 2, 1.0, state.hq, 0, 0, 0, state.tmp0, 0, 0.0, state.tmp1, 0, _params);
        rotations.generaterotation(state.tmp1[itidx], state.tmp1[itidx + 1], ref cs, ref sn, ref v, _params);
        state.tmp1[itidx] = v;
        state.tmp1[itidx + 1] = 0;
        rmax = Math.Max(rmax, ablasf.rmaxabsv(itidx + 2, state.tmp1, _params));
        rmindiag = Math.Min(rmindiag, Math.Abs(v));
        if ((double)(rmindiag) <= (double)(rmax * state.epsdiag))
        {
            state.retcode = 3;
            goto lbl_3;
        }
        ablasf.rcopyvc(itidx + 2, state.tmp1, state.hr, itidx, _params);
        for (j = 0; j <= itidx + 1; j++)
        {
            v = state.hq[itidx + 0, j];
            vv = state.hq[itidx + 1, j];
            state.hq[itidx + 0, j] = cs * v + sn * vv;
            state.hq[itidx + 1, j] = -(sn * v) + cs * vv;
        }
        v = state.hqb[itidx + 0];
        vv = state.hqb[itidx + 1];
        state.hqb[itidx + 0] = cs * v + sn * vv;
        state.hqb[itidx + 1] = -(sn * v) + cs * vv;
        resnrm = Math.Abs(state.hqb[itidx + 1]);

        //
        // Previous attempt to extend R was successful (no small diagonal elements).
        // Increase Krylov subspace dimensionality.
        //
        kdim = kdim + 1;

        //
        // Iteration is over.
        // Terminate if:
        // * last Qi was nearly zero after orthogonalization.
        // * sufficient decrease of residual
        // * stagnation of residual
        //
        state.itsperformed = state.itsperformed + 1;
        if ((double)(qnrm) <= (double)(state.epsort * anrm) || (double)(qnrm) == (double)(0))
        {
            state.retcode = 4;
            goto lbl_3;
        }
        if ((double)(resnrm) <= (double)(state.epsres * bnrm))
        {
            state.retcode = 5;
            goto lbl_3;
        }
        if ((double)(resnrm / prevresnrm) > (double)(state.epsred))
        {
            state.retcode = 6;
            goto lbl_3;
        }
        itidx = itidx + 1;
        goto lbl_1;
    lbl_3:

        //
        // Post-solve
        //
        if (kdim > 0)
        {
            ablasf.rcopyv(kdim, state.hqb, state.ys, _params);
            ablas.rmatrixtrsv(kdim, state.hr, 0, 0, true, false, 0, state.ys, 0, _params);
            ablas.rmatrixmv(n, kdim, state.qi, 0, 0, 1, state.ys, 0, state.xs, 0, _params);
        }
        result = false;
        return result;

    //
    // Saving state
    //
    lbl_rcomm:
        result = true;
        state.rstate.ia[0] = n;
        state.rstate.ia[1] = itidx;
        state.rstate.ia[2] = kdim;
        state.rstate.ia[3] = i;
        state.rstate.ia[4] = j;
        state.rstate.ra[0] = rmax;
        state.rstate.ra[1] = rmindiag;
        state.rstate.ra[2] = cs;
        state.rstate.ra[3] = sn;
        state.rstate.ra[4] = v;
        state.rstate.ra[5] = vv;
        state.rstate.ra[6] = anrm;
        state.rstate.ra[7] = qnrm;
        state.rstate.ra[8] = bnrm;
        state.rstate.ra[9] = resnrm;
        state.rstate.ra[10] = prevresnrm;
        return result;
    }


    /*************************************************************************
    Fast  least  squares  solver,  solves  well  conditioned  system   without
    performing  any  checks  for  degeneracy,  and using user-provided buffers
    (which are automatically reallocated if too small).

    This  function  is  intended  for solution of moderately sized systems. It
    uses factorization algorithms based on Level 2 BLAS  operations,  thus  it
    won't work efficiently on large scale systems.

    INPUT PARAMETERS:
        A       -   array[M,N], system matrix.
                    Contents of A is destroyed during solution.
        B       -   array[M], right part
        M       -   number of equations
        N       -   number of variables, N<=M
        Tmp0, Tmp1, Tmp2-
                    buffers; function automatically allocates them, if they are
                    too  small. They can  be  reused  if  function  is   called 
                    several times.
                    
    OUTPUT PARAMETERS:
        B       -   solution (first N components, next M-N are zero)

      -- ALGLIB --
         Copyright 20.01.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void fblssolvels(ref double[,] a,
        ref double[] b,
        int m,
        int n,
        ref double[] tmp0,
        ref double[] tmp1,
        ref double[] tmp2,
        xparams _params)
    {
        int i = 0;
        int k = 0;
        double v = 0;
        int i_ = 0;

        ap.assert(n > 0, "FBLSSolveLS: N<=0");
        ap.assert(m >= n, "FBLSSolveLS: M<N");
        ap.assert(ap.rows(a) >= m, "FBLSSolveLS: Rows(A)<M");
        ap.assert(ap.cols(a) >= n, "FBLSSolveLS: Cols(A)<N");
        ap.assert(ap.len(b) >= m, "FBLSSolveLS: Length(B)<M");

        //
        // Allocate temporaries
        //
        apserv.rvectorsetlengthatleast(ref tmp0, Math.Max(m, n) + 1, _params);
        apserv.rvectorsetlengthatleast(ref tmp1, Math.Max(m, n) + 1, _params);
        apserv.rvectorsetlengthatleast(ref tmp2, Math.Min(m, n), _params);

        //
        // Call basecase QR
        //
        ortfac.rmatrixqrbasecase(ref a, m, n, ref tmp0, ref tmp1, ref tmp2, _params);

        //
        // Multiply B by Q'
        //
        for (k = 0; k <= n - 1; k++)
        {
            for (i = 0; i <= k - 1; i++)
            {
                tmp0[i] = 0;
            }
            for (i_ = k; i_ <= m - 1; i_++)
            {
                tmp0[i_] = a[i_, k];
            }
            tmp0[k] = 1;
            v = 0.0;
            for (i_ = k; i_ <= m - 1; i_++)
            {
                v += tmp0[i_] * b[i_];
            }
            v = v * tmp2[k];
            for (i_ = k; i_ <= m - 1; i_++)
            {
                b[i_] = b[i_] - v * tmp0[i_];
            }
        }

        //
        // Solve triangular system
        //
        b[n - 1] = b[n - 1] / a[n - 1, n - 1];
        for (i = n - 2; i >= 0; i--)
        {
            v = 0.0;
            for (i_ = i + 1; i_ <= n - 1; i_++)
            {
                v += a[i, i_] * b[i_];
            }
            b[i] = (b[i] - v) / a[i, i];
        }
        for (i = n; i <= m - 1; i++)
        {
            b[i] = 0.0;
        }
    }


}
