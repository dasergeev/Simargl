using System;

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


public class fitsphere
{
    public class fitsphereinternalreport : apobject
    {
        public int nfev;
        public int iterationscount;
        public fitsphereinternalreport()
        {
            init();
        }
        public override void init()
        {
        }
        public override apobject make_copy()
        {
            fitsphereinternalreport _result = new fitsphereinternalreport();
            _result.nfev = nfev;
            _result.iterationscount = iterationscount;
            return _result;
        }
    };




    /*************************************************************************
    Fits least squares (LS) circle (or NX-dimensional sphere) to data  (a  set
    of points in NX-dimensional space).

    Least squares circle minimizes sum of squared deviations between distances
    from points to the center and  some  "candidate"  radius,  which  is  also
    fitted to the data.

    INPUT PARAMETERS:
        XY      -   array[NPoints,NX] (or larger), contains dataset.
                    One row = one point in NX-dimensional space.
        NPoints -   dataset size, NPoints>0
        NX      -   space dimensionality, NX>0 (1, 2, 3, 4, 5 and so on)

    OUTPUT PARAMETERS:
        CX      -   central point for a sphere
        R       -   radius
                                        
      -- ALGLIB --
         Copyright 07.05.2018 by Bochkanov Sergey
    *************************************************************************/
    public static void fitspherels(double[,] xy,
        int npoints,
        int nx,
        ref double[] cx,
        ref double r,
        xparams _params)
    {
        double dummy = 0;

        cx = new double[0];
        r = 0;

        fitspherex(xy, npoints, nx, 0, 0.0, 0, 0.0, ref cx, ref dummy, ref r, _params);
    }


    /*************************************************************************
    Fits minimum circumscribed (MC) circle (or NX-dimensional sphere) to  data
    (a set of points in NX-dimensional space).

    INPUT PARAMETERS:
        XY      -   array[NPoints,NX] (or larger), contains dataset.
                    One row = one point in NX-dimensional space.
        NPoints -   dataset size, NPoints>0
        NX      -   space dimensionality, NX>0 (1, 2, 3, 4, 5 and so on)

    OUTPUT PARAMETERS:
        CX      -   central point for a sphere
        RHi     -   radius

    NOTE: this function is an easy-to-use wrapper around more powerful "expert"
          function fitspherex().
          
          This  wrapper  is optimized  for  ease of use and stability - at the
          cost of somewhat lower  performance  (we  have  to  use  very  tight
          stopping criteria for inner optimizer because we want to  make  sure
          that it will converge on any dataset).
          
          If you are ready to experiment with settings of  "expert"  function,
          you can achieve ~2-4x speedup over standard "bulletproof" settings.

                                        
      -- ALGLIB --
         Copyright 14.04.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void fitspheremc(double[,] xy,
        int npoints,
        int nx,
        ref double[] cx,
        ref double rhi,
        xparams _params)
    {
        double dummy = 0;

        cx = new double[0];
        rhi = 0;

        fitspherex(xy, npoints, nx, 1, 0.0, 0, 0.0, ref cx, ref dummy, ref rhi, _params);
    }


    /*************************************************************************
    Fits maximum inscribed circle (or NX-dimensional sphere) to data (a set of
    points in NX-dimensional space).

    INPUT PARAMETERS:
        XY      -   array[NPoints,NX] (or larger), contains dataset.
                    One row = one point in NX-dimensional space.
        NPoints -   dataset size, NPoints>0
        NX      -   space dimensionality, NX>0 (1, 2, 3, 4, 5 and so on)

    OUTPUT PARAMETERS:
        CX      -   central point for a sphere
        RLo     -   radius

    NOTE: this function is an easy-to-use wrapper around more powerful "expert"
          function fitspherex().
          
          This  wrapper  is optimized  for  ease of use and stability - at the
          cost of somewhat lower  performance  (we  have  to  use  very  tight
          stopping criteria for inner optimizer because we want to  make  sure
          that it will converge on any dataset).
          
          If you are ready to experiment with settings of  "expert"  function,
          you can achieve ~2-4x speedup over standard "bulletproof" settings.

                                        
      -- ALGLIB --
         Copyright 14.04.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void fitspheremi(double[,] xy,
        int npoints,
        int nx,
        ref double[] cx,
        ref double rlo,
        xparams _params)
    {
        double dummy = 0;

        cx = new double[0];
        rlo = 0;

        fitspherex(xy, npoints, nx, 2, 0.0, 0, 0.0, ref cx, ref rlo, ref dummy, _params);
    }


    /*************************************************************************
    Fits minimum zone circle (or NX-dimensional sphere)  to  data  (a  set  of
    points in NX-dimensional space).

    INPUT PARAMETERS:
        XY      -   array[NPoints,NX] (or larger), contains dataset.
                    One row = one point in NX-dimensional space.
        NPoints -   dataset size, NPoints>0
        NX      -   space dimensionality, NX>0 (1, 2, 3, 4, 5 and so on)

    OUTPUT PARAMETERS:
        CX      -   central point for a sphere
        RLo     -   radius of inscribed circle
        RHo     -   radius of circumscribed circle

    NOTE: this function is an easy-to-use wrapper around more powerful "expert"
          function fitspherex().
          
          This  wrapper  is optimized  for  ease of use and stability - at the
          cost of somewhat lower  performance  (we  have  to  use  very  tight
          stopping criteria for inner optimizer because we want to  make  sure
          that it will converge on any dataset).
          
          If you are ready to experiment with settings of  "expert"  function,
          you can achieve ~2-4x speedup over standard "bulletproof" settings.

                                        
      -- ALGLIB --
         Copyright 14.04.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void fitspheremz(double[,] xy,
        int npoints,
        int nx,
        ref double[] cx,
        ref double rlo,
        ref double rhi,
        xparams _params)
    {
        cx = new double[0];
        rlo = 0;
        rhi = 0;

        fitspherex(xy, npoints, nx, 3, 0.0, 0, 0.0, ref cx, ref rlo, ref rhi, _params);
    }


    /*************************************************************************
    Fitting minimum circumscribed, maximum inscribed or minimum  zone  circles
    (or NX-dimensional spheres)  to  data  (a  set of points in NX-dimensional
    space).

    This  is  expert  function  which  allows  to  tweak  many  parameters  of
    underlying nonlinear solver:
    * stopping criteria for inner iterations
    * number of outer iterations
    * penalty coefficient used to handle  nonlinear  constraints  (we  convert
      unconstrained nonsmooth optimization problem ivolving max() and/or min()
      operations to quadratically constrained smooth one).

    You may tweak all these parameters or only some  of  them,  leaving  other
    ones at their default state - just specify zero  value,  and  solver  will
    fill it with appropriate default one.

    These comments also include some discussion of  approach  used  to  handle
    such unusual fitting problem,  its  stability,  drawbacks  of  alternative
    methods, and convergence properties.
      
    INPUT PARAMETERS:
        XY      -   array[NPoints,NX] (or larger), contains dataset.
                    One row = one point in NX-dimensional space.
        NPoints -   dataset size, NPoints>0
        NX      -   space dimensionality, NX>0 (1, 2, 3, 4, 5 and so on)
        ProblemType-used to encode problem type:
                    * 0 for least squares circle
                    * 1 for minimum circumscribed circle/sphere fitting (MC)
                    * 2 for  maximum inscribed circle/sphere fitting (MI)
                    * 3 for minimum zone circle fitting (difference between
                        Rhi and Rlo is minimized), denoted as MZ
        EpsX    -   stopping condition for NLC optimizer:
                    * must be non-negative
                    * use 0 to choose default value (1.0E-12 is used by default)
                    * you may specify larger values, up to 1.0E-6, if you want
                      to   speed-up   solver;   NLC   solver  performs several
                      preconditioned  outer  iterations,   so   final   result
                      typically has precision much better than EpsX.
        AULIts  -   number of outer iterations performed by NLC optimizer:
                    * must be non-negative
                    * use 0 to choose default value (20 is used by default)
                    * you may specify values smaller than 20 if you want to
                      speed up solver; 10 often results in good combination of
                      precision and speed; sometimes you may get good results
                      with just 6 outer iterations.
                    Ignored for ProblemType=0.
        Penalty -   penalty coefficient for NLC optimizer:
                    * must be non-negative
                    * use 0 to choose default value (1.0E6 in current version)
                    * it should be really large, 1.0E6...1.0E7 is a good value
                      to start from;
                    * generally, default value is good enough
                    Ignored for ProblemType=0.

    OUTPUT PARAMETERS:
        CX      -   central point for a sphere
        RLo     -   radius:
                    * for ProblemType=2,3, radius of the inscribed sphere
                    * for ProblemType=0 - radius of the least squares sphere
                    * for ProblemType=1 - zero
        RHo     -   radius:
                    * for ProblemType=1,3, radius of the circumscribed sphere
                    * for ProblemType=0 - radius of the least squares sphere
                    * for ProblemType=2 - zero

    NOTE: ON THE UNIQUENESS OF SOLUTIONS

    ALGLIB provides solution to several related circle fitting  problems:   MC
    (minimum circumscribed), MI (maximum inscribed)   and   MZ  (minimum zone)
    fitting, LS (least squares) fitting.

    It  is  important  to  note  that  among these problems only MC and LS are
    convex and have unique solution independently from starting point.

    As  for MI,  it  may (or  may  not, depending on dataset properties)  have
    multiple solutions, and it always  has  one degenerate solution C=infinity
    which corresponds to infinitely large radius. Thus, there are no guarantees
    that solution to  MI returned by this solver will be the best one (and  no
    one can provide you with such guarantee because problem is  NP-hard).  The
    only guarantee you have is that this solution is locally optimal, i.e.  it
    can not be improved by infinitesimally small tweaks in the parameters.

    It  is  also  possible  to "run away" to infinity when  started  from  bad
    initial point located outside of point cloud (or when point cloud does not
    span entire circumference/surface of the sphere).

    Finally,  MZ (minimum zone circle) stands somewhere between MC  and  MI in
    stability. It is somewhat regularized by "circumscribed" term of the merit
    function; however, solutions to  MZ may be non-unique, and in some unlucky
    cases it is also possible to "run away to infinity".


    NOTE: ON THE NONLINEARLY CONSTRAINED PROGRAMMING APPROACH

    The problem formulation for MC  (minimum circumscribed   circle;  for  the
    sake of simplicity we omit MZ and MI here) is:

            [     [         ]2 ]
        min [ max [ XY[i]-C ]  ]
         C  [  i  [         ]  ]

    i.e. it is unconstrained nonsmooth optimization problem of finding  "best"
    central point, with radius R being unambiguously  determined  from  C.  In
    order to move away from non-smoothness we use following reformulation:

            [   ]                  [         ]2
        min [ R ] subject to R>=0, [ XY[i]-C ]  <= R^2
        C,R [   ]                  [         ]
        
    i.e. it becomes smooth quadratically constrained optimization problem with
    linear target function. Such problem statement is 100% equivalent  to  the
    original nonsmooth one, but much easier  to  approach.  We solve  it  with
    MinNLC solver provided by ALGLIB.


    NOTE: ON INSTABILITY OF SEQUENTIAL LINEARIZATION APPROACH

    ALGLIB  has  nonlinearly  constrained  solver which proved to be stable on
    such problems. However, some authors proposed to linearize constraints  in
    the vicinity of current approximation (Ci,Ri) and to get next  approximate
    solution (Ci+1,Ri+1) as solution to linear programming problem. Obviously,
    LP problems are easier than nonlinearly constrained ones.

    Indeed,  such approach  to   MC/MI/MZ   resulted   in  ~10-20x increase in
    performance (when compared with NLC solver). However, it turned  out  that
    in some cases linearized model fails to predict correct direction for next
    step and tells us that we converged to solution even when we are still 2-4
    digits of precision away from it.

    It is important that it is not failure of LP solver - it is failure of the
    linear model;  even  when  solved  exactly,  it  fails  to  handle  subtle
    nonlinearities which arise near the solution. We validated it by comparing
    results returned by ALGLIB linear solver with that of MATLAB.

    In our experiments with linearization:
    * MC failed most often, at both realistic and synthetic datasets
    * MI sometimes failed, but sometimes succeeded
    * MZ often  succeeded; our guess is that presence of two independent  sets
      of constraints (one set for Rlo and another one for Rhi) and  two  terms
      in the target function (Rlo and Rhi) regularizes task,  so  when  linear
      model fails to handle nonlinearities from Rlo, it uses  Rhi  as  a  hint
      (and vice versa).
      
    Because linearization approach failed to achieve stable results, we do not
    include it in ALGLIB.

                                        
      -- ALGLIB --
         Copyright 14.04.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void fitspherex(double[,] xy,
        int npoints,
        int nx,
        int problemtype,
        double epsx,
        int aulits,
        double penalty,
        ref double[] cx,
        ref double rlo,
        ref double rhi,
        xparams _params)
    {
        fitsphereinternalreport rep = new fitsphereinternalreport();

        cx = new double[0];
        rlo = 0;
        rhi = 0;

        ap.assert(math.isfinite(penalty) && (double)(penalty) >= (double)(0), "FitSphereX: Penalty<0 or is not finite");
        ap.assert(math.isfinite(epsx) && (double)(epsx) >= (double)(0), "FitSphereX: EpsX<0 or is not finite");
        ap.assert(aulits >= 0, "FitSphereX: AULIts<0");
        fitsphereinternal(xy, npoints, nx, problemtype, 0, epsx, aulits, penalty, ref cx, ref rlo, ref rhi, rep, _params);
    }


    /*************************************************************************
    Fitting minimum circumscribed, maximum inscribed or minimum  zone  circles
    (or NX-dimensional spheres)  to  data  (a  set of points in NX-dimensional
    space).

    Internal computational function.

    INPUT PARAMETERS:
        XY      -   array[NPoints,NX] (or larger), contains dataset.
                    One row = one point in NX-dimensional space.
        NPoints -   dataset size, NPoints>0
        NX      -   space dimensionality, NX>0 (1, 2, 3, 4, 5 and so on)
        ProblemType-used to encode problem type:
                    * 0 for least squares circle
                    * 1 for minimum circumscribed circle/sphere fitting (MC)
                    * 2 for  maximum inscribed circle/sphere fitting (MI)
                    * 3 for minimum zone circle fitting (difference between
                        Rhi and Rlo is minimized), denoted as MZ
        SolverType- solver to use:
                    * 0 use best solver available (1 in current version)
                    * 1 use nonlinearly constrained optimization approach, AUL
                        (it is roughly 10-20 times  slower  than  SPC-LIN, but
                        much more stable)
                    * 2 use special fast IMPRECISE solver, SPC-LIN  sequential
                        linearization approach; SPC-LIN is fast, but sometimes
                        fails to converge with more than 3 digits of precision;
                        see comments below.
                        NOT RECOMMENDED UNLESS YOU REALLY NEED HIGH PERFORMANCE
                        AT THE COST OF SOME PRECISION.
                    * 3 use nonlinearly constrained optimization approach, SLP
                        (most robust one, but somewhat slower than AUL)
                    Ignored for ProblemType=0.
        EpsX    -   stopping criteria for SLP and NLC optimizers:
                    * must be non-negative
                    * use 0 to choose default value (1.0E-12 is used by default)
                    * if you use SLP solver, you should use default values
                    * if you use NLC solver, you may specify larger values, up
                      to 1.0E-6, if you want to speed-up  solver;  NLC  solver
                      performs several preconditioned outer iterations, so final
                      result typically has precision much better than EpsX.
        AULIts  -   number of iterations performed by NLC optimizer:
                    * must be non-negative
                    * use 0 to choose default value (20 is used by default)
                    * you may specify values smaller than 20 if you want to
                      speed up solver; 10 often results in good combination of
                      precision and speed
                    Ignored for ProblemType=0.
        Penalty -   penalty coefficient for NLC optimizer (ignored  for  SLP):
                    * must be non-negative
                    * use 0 to choose default value (1.0E6 in current version)
                    * it should be really large, 1.0E6...1.0E7 is a good value
                      to start from;
                    * generally, default value is good enough
                    * ignored by SLP optimizer
                    Ignored for ProblemType=0.

    OUTPUT PARAMETERS:
        CX      -   central point for a sphere
        RLo     -   radius:
                    * for ProblemType=2,3, radius of the inscribed sphere
                    * for ProblemType=0 - radius of the least squares sphere
                    * for ProblemType=1 - zero
        RHo     -   radius:
                    * for ProblemType=1,3, radius of the circumscribed sphere
                    * for ProblemType=0 - radius of the least squares sphere
                    * for ProblemType=2 - zero
                                        
      -- ALGLIB --
         Copyright 14.04.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void fitsphereinternal(double[,] xy,
        int npoints,
        int nx,
        int problemtype,
        int solvertype,
        double epsx,
        int aulits,
        double penalty,
        ref double[] cx,
        ref double rlo,
        ref double rhi,
        fitsphereinternalreport rep,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double v = 0;
        double vv = 0;
        int cpr = 0;
        bool userlo = new bool();
        bool userhi = new bool();
        double vlo = 0;
        double vhi = 0;
        double[] vmin = new double[0];
        double[] vmax = new double[0];
        double spread = 0;
        double[] pcr = new double[0];
        double[] scr = new double[0];
        double[] bl = new double[0];
        double[] bu = new double[0];
        int suboffset = 0;
        int dstrow = 0;
        minnlc.minnlcstate nlcstate = new minnlc.minnlcstate();
        minnlc.minnlcreport nlcrep = new minnlc.minnlcreport();
        double[,] cmatrix = new double[0, 0];
        int[] ct = new int[0];
        int outeridx = 0;
        int maxouterits = 0;
        int maxits = 0;
        double safeguard = 0;
        double bi = 0;
        minbleic.minbleicstate blcstate = new minbleic.minbleicstate();
        minbleic.minbleicreport blcrep = new minbleic.minbleicreport();
        double[] prevc = new double[0];
        minlm.minlmstate lmstate = new minlm.minlmstate();
        minlm.minlmreport lmrep = new minlm.minlmreport();

        cx = new double[0];
        rlo = 0;
        rhi = 0;


        //
        // Check input parameters
        //
        ap.assert(npoints > 0, "FitSphereX: NPoints<=0");
        ap.assert(nx > 0, "FitSphereX: NX<=0");
        ap.assert(apserv.apservisfinitematrix(xy, npoints, nx, _params), "FitSphereX: XY contains infinite or NAN values");
        ap.assert(problemtype >= 0 && problemtype <= 3, "FitSphereX: ProblemType is neither 0, 1, 2 or 3");
        ap.assert(solvertype >= 0 && solvertype <= 3, "FitSphereX: ProblemType is neither 1, 2 or 3");
        ap.assert(math.isfinite(penalty) && (double)(penalty) >= (double)(0), "FitSphereX: Penalty<0 or is not finite");
        ap.assert(math.isfinite(epsx) && (double)(epsx) >= (double)(0), "FitSphereX: EpsX<0 or is not finite");
        ap.assert(aulits >= 0, "FitSphereX: AULIts<0");
        if (solvertype == 0)
        {
            solvertype = 1;
        }
        if ((double)(penalty) == (double)(0))
        {
            penalty = 1.0E6;
        }
        if ((double)(epsx) == (double)(0))
        {
            epsx = 1.0E-12;
        }
        if (aulits == 0)
        {
            aulits = 20;
        }
        safeguard = 10;
        maxouterits = 10;
        maxits = 10000;
        rep.nfev = 0;
        rep.iterationscount = 0;

        //
        // Determine initial values, initial estimates and spread of the points
        //
        vmin = new double[nx];
        vmax = new double[nx];
        cx = new double[nx];
        for (j = 0; j <= nx - 1; j++)
        {
            vmin[j] = xy[0, j];
            vmax[j] = xy[0, j];
            cx[j] = 0;
        }
        for (i = 0; i <= npoints - 1; i++)
        {
            for (j = 0; j <= nx - 1; j++)
            {
                cx[j] = cx[j] + xy[i, j];
                vmin[j] = Math.Min(vmin[j], xy[i, j]);
                vmax[j] = Math.Max(vmax[j], xy[i, j]);
            }
        }
        spread = 0;
        for (j = 0; j <= nx - 1; j++)
        {
            cx[j] = cx[j] / npoints;
            spread = Math.Max(spread, vmax[j] - vmin[j]);
        }
        rlo = math.maxrealnumber;
        rhi = 0;
        for (i = 0; i <= npoints - 1; i++)
        {
            v = 0;
            for (j = 0; j <= nx - 1; j++)
            {
                v = v + math.sqr(xy[i, j] - cx[j]);
            }
            v = Math.Sqrt(v);
            rhi = Math.Max(rhi, v);
            rlo = Math.Min(rlo, v);
        }

        //
        // Handle degenerate case of zero spread
        //
        if ((double)(spread) == (double)(0))
        {
            for (j = 0; j <= nx - 1; j++)
            {
                cx[j] = vmin[j];
            }
            rhi = 0;
            rlo = 0;
            return;
        }

        //
        // Prepare initial point for optimizer, scale vector and box constraints
        //
        pcr = new double[nx + 2];
        scr = new double[nx + 2];
        bl = new double[nx + 2];
        bu = new double[nx + 2];
        for (j = 0; j <= nx - 1; j++)
        {
            pcr[j] = cx[j];
            scr[j] = 0.1 * spread;
            bl[j] = cx[j] - safeguard * spread;
            bu[j] = cx[j] + safeguard * spread;
        }
        pcr[nx + 0] = rlo;
        pcr[nx + 1] = rhi;
        scr[nx + 0] = 0.5 * spread;
        scr[nx + 1] = 0.5 * spread;
        bl[nx + 0] = 0;
        bl[nx + 1] = 0;
        bu[nx + 0] = safeguard * rhi;
        bu[nx + 1] = safeguard * rhi;

        //
        // First branch: least squares fitting vs MI/MC/MZ fitting
        //
        if (problemtype == 0)
        {

            //
            // Solve problem with Levenberg-Marquardt algorithm
            //
            pcr[nx] = rhi;
            minlm.minlmcreatevj(nx + 1, npoints, pcr, lmstate, _params);
            minlm.minlmsetscale(lmstate, scr, _params);
            minlm.minlmsetbc(lmstate, bl, bu, _params);
            minlm.minlmsetcond(lmstate, epsx, maxits, _params);
            while (minlm.minlmiteration(lmstate, _params))
            {
                if (lmstate.needfij || lmstate.needfi)
                {
                    apserv.inc(ref rep.nfev, _params);
                    for (i = 0; i <= npoints - 1; i++)
                    {
                        v = 0;
                        for (j = 0; j <= nx - 1; j++)
                        {
                            v = v + math.sqr(lmstate.x[j] - xy[i, j]);
                        }
                        lmstate.fi[i] = Math.Sqrt(v) - lmstate.x[nx];
                        if (lmstate.needfij)
                        {
                            for (j = 0; j <= nx - 1; j++)
                            {
                                lmstate.j[i, j] = 0.5 / (1.0E-9 * spread + Math.Sqrt(v)) * 2 * (lmstate.x[j] - xy[i, j]);
                            }
                            lmstate.j[i, nx] = -1;
                        }
                    }
                    continue;
                }
                ap.assert(false);
            }
            minlm.minlmresults(lmstate, ref pcr, lmrep, _params);
            ap.assert(lmrep.terminationtype > 0, "FitSphereX: unexpected failure of LM solver");
            rep.iterationscount = rep.iterationscount + lmrep.iterationscount;

            //
            // Offload center coordinates from PCR to CX,
            // re-calculate exact value of RLo/RHi using CX.
            //
            for (j = 0; j <= nx - 1; j++)
            {
                cx[j] = pcr[j];
            }
            vv = 0;
            for (i = 0; i <= npoints - 1; i++)
            {
                v = 0;
                for (j = 0; j <= nx - 1; j++)
                {
                    v = v + math.sqr(xy[i, j] - cx[j]);
                }
                v = Math.Sqrt(v);
                vv = vv + v / npoints;
            }
            rlo = vv;
            rhi = vv;
        }
        else
        {

            //
            // MI, MC, MZ fitting.
            // Prepare problem metrics
            //
            userlo = problemtype == 2 || problemtype == 3;
            userhi = problemtype == 1 || problemtype == 3;
            if (userlo && userhi)
            {
                cpr = 2;
            }
            else
            {
                cpr = 1;
            }
            if (userlo)
            {
                vlo = 1;
            }
            else
            {
                vlo = 0;
            }
            if (userhi)
            {
                vhi = 1;
            }
            else
            {
                vhi = 0;
            }

            //
            // Solve with NLC solver; problem is treated as general nonlinearly constrained
            // programming, with augmented Lagrangian solver or SLP being used.
            //
            if (solvertype == 1 || solvertype == 3)
            {
                minnlc.minnlccreate(nx + 2, pcr, nlcstate, _params);
                minnlc.minnlcsetscale(nlcstate, scr, _params);
                minnlc.minnlcsetbc(nlcstate, bl, bu, _params);
                minnlc.minnlcsetnlc(nlcstate, 0, cpr * npoints, _params);
                minnlc.minnlcsetcond(nlcstate, epsx, maxits, _params);
                minnlc.minnlcsetprecexactrobust(nlcstate, 5, _params);
                minnlc.minnlcsetstpmax(nlcstate, 0.1, _params);
                if (solvertype == 1)
                {
                    minnlc.minnlcsetalgoaul(nlcstate, penalty, aulits, _params);
                }
                else
                {
                    minnlc.minnlcsetalgoslp(nlcstate, _params);
                }
                minnlc.minnlcrestartfrom(nlcstate, pcr, _params);
                while (minnlc.minnlciteration(nlcstate, _params))
                {
                    if (nlcstate.needfij)
                    {
                        apserv.inc(ref rep.nfev, _params);
                        nlcstate.fi[0] = vhi * nlcstate.x[nx + 1] - vlo * nlcstate.x[nx + 0];
                        for (j = 0; j <= nx - 1; j++)
                        {
                            nlcstate.j[0, j] = 0;
                        }
                        nlcstate.j[0, nx + 0] = -(1 * vlo);
                        nlcstate.j[0, nx + 1] = 1 * vhi;
                        for (i = 0; i <= npoints - 1; i++)
                        {
                            suboffset = 0;
                            if (userhi)
                            {
                                dstrow = 1 + cpr * i + suboffset;
                                v = 0;
                                for (j = 0; j <= nx - 1; j++)
                                {
                                    vv = nlcstate.x[j] - xy[i, j];
                                    v = v + vv * vv;
                                    nlcstate.j[dstrow, j] = 2 * vv;
                                }
                                vv = nlcstate.x[nx + 1];
                                v = v - vv * vv;
                                nlcstate.j[dstrow, nx + 0] = 0;
                                nlcstate.j[dstrow, nx + 1] = -(2 * vv);
                                nlcstate.fi[dstrow] = v;
                                apserv.inc(ref suboffset, _params);
                            }
                            if (userlo)
                            {
                                dstrow = 1 + cpr * i + suboffset;
                                v = 0;
                                for (j = 0; j <= nx - 1; j++)
                                {
                                    vv = nlcstate.x[j] - xy[i, j];
                                    v = v - vv * vv;
                                    nlcstate.j[dstrow, j] = -(2 * vv);
                                }
                                vv = nlcstate.x[nx + 0];
                                v = v + vv * vv;
                                nlcstate.j[dstrow, nx + 0] = 2 * vv;
                                nlcstate.j[dstrow, nx + 1] = 0;
                                nlcstate.fi[dstrow] = v;
                                apserv.inc(ref suboffset, _params);
                            }
                            ap.assert(suboffset == cpr);
                        }
                        continue;
                    }
                    ap.assert(false);
                }
                minnlc.minnlcresults(nlcstate, ref pcr, nlcrep, _params);
                ap.assert(nlcrep.terminationtype > 0, "FitSphereX: unexpected failure of NLC solver");
                rep.iterationscount = rep.iterationscount + nlcrep.iterationscount;

                //
                // Offload center coordinates from PCR to CX,
                // re-calculate exact value of RLo/RHi using CX.
                //
                for (j = 0; j <= nx - 1; j++)
                {
                    cx[j] = pcr[j];
                }
                rlo = math.maxrealnumber;
                rhi = 0;
                for (i = 0; i <= npoints - 1; i++)
                {
                    v = 0;
                    for (j = 0; j <= nx - 1; j++)
                    {
                        v = v + math.sqr(xy[i, j] - cx[j]);
                    }
                    v = Math.Sqrt(v);
                    rhi = Math.Max(rhi, v);
                    rlo = Math.Min(rlo, v);
                }
                if (!userlo)
                {
                    rlo = 0;
                }
                if (!userhi)
                {
                    rhi = 0;
                }
                return;
            }

            //
            // Solve problem with SLP (sequential LP) approach; this approach
            // is much faster than NLP, but often fails for MI and MC (for MZ
            // it performs well enough).
            //
            // REFERENCE: "On a sequential linear programming approach to finding
            //            the smallest circumscribed, largest inscribed, and minimum
            //            zone circle or sphere", Helmuth Spath and G.A.Watson
            //
            if (solvertype == 2)
            {
                cmatrix = new double[cpr * npoints, nx + 3];
                ct = new int[cpr * npoints];
                prevc = new double[nx];
                minbleic.minbleiccreate(nx + 2, pcr, blcstate, _params);
                minbleic.minbleicsetscale(blcstate, scr, _params);
                minbleic.minbleicsetbc(blcstate, bl, bu, _params);
                minbleic.minbleicsetcond(blcstate, 0, 0, epsx, maxits, _params);
                for (outeridx = 0; outeridx <= maxouterits - 1; outeridx++)
                {

                    //
                    // Prepare initial point for algorithm; center coordinates at
                    // PCR are used to calculate RLo/RHi and update PCR with them.
                    //
                    rlo = math.maxrealnumber;
                    rhi = 0;
                    for (i = 0; i <= npoints - 1; i++)
                    {
                        v = 0;
                        for (j = 0; j <= nx - 1; j++)
                        {
                            v = v + math.sqr(xy[i, j] - pcr[j]);
                        }
                        v = Math.Sqrt(v);
                        rhi = Math.Max(rhi, v);
                        rlo = Math.Min(rlo, v);
                    }
                    pcr[nx + 0] = rlo * 0.99999;
                    pcr[nx + 1] = rhi / 0.99999;

                    //
                    // Generate matrix of linear constraints
                    //
                    for (i = 0; i <= npoints - 1; i++)
                    {
                        v = 0;
                        for (j = 0; j <= nx - 1; j++)
                        {
                            v = v + math.sqr(xy[i, j]);
                        }
                        bi = -(v / 2);
                        suboffset = 0;
                        if (userhi)
                        {
                            dstrow = cpr * i + suboffset;
                            for (j = 0; j <= nx - 1; j++)
                            {
                                cmatrix[dstrow, j] = pcr[j] / 2 - xy[i, j];
                            }
                            cmatrix[dstrow, nx + 0] = 0;
                            cmatrix[dstrow, nx + 1] = -(rhi / 2);
                            cmatrix[dstrow, nx + 2] = bi;
                            ct[dstrow] = -1;
                            apserv.inc(ref suboffset, _params);
                        }
                        if (userlo)
                        {
                            dstrow = cpr * i + suboffset;
                            for (j = 0; j <= nx - 1; j++)
                            {
                                cmatrix[dstrow, j] = -(pcr[j] / 2 - xy[i, j]);
                            }
                            cmatrix[dstrow, nx + 0] = rlo / 2;
                            cmatrix[dstrow, nx + 1] = 0;
                            cmatrix[dstrow, nx + 2] = -bi;
                            ct[dstrow] = -1;
                            apserv.inc(ref suboffset, _params);
                        }
                        ap.assert(suboffset == cpr);
                    }

                    //
                    // Solve LP subproblem with MinBLEIC
                    //
                    for (j = 0; j <= nx - 1; j++)
                    {
                        prevc[j] = pcr[j];
                    }
                    minbleic.minbleicsetlc(blcstate, cmatrix, ct, cpr * npoints, _params);
                    minbleic.minbleicrestartfrom(blcstate, pcr, _params);
                    while (minbleic.minbleiciteration(blcstate, _params))
                    {
                        if (blcstate.needfg)
                        {
                            apserv.inc(ref rep.nfev, _params);
                            blcstate.f = vhi * blcstate.x[nx + 1] - vlo * blcstate.x[nx + 0];
                            for (j = 0; j <= nx - 1; j++)
                            {
                                blcstate.g[j] = 0;
                            }
                            blcstate.g[nx + 0] = -(1 * vlo);
                            blcstate.g[nx + 1] = 1 * vhi;
                            continue;
                        }
                    }
                    minbleic.minbleicresults(blcstate, ref pcr, blcrep, _params);
                    ap.assert(blcrep.terminationtype > 0, "FitSphereX: unexpected failure of BLEIC solver");
                    rep.iterationscount = rep.iterationscount + blcrep.iterationscount;

                    //
                    // Terminate iterations early if we converged
                    //
                    v = 0;
                    for (j = 0; j <= nx - 1; j++)
                    {
                        v = v + math.sqr(prevc[j] - pcr[j]);
                    }
                    v = Math.Sqrt(v);
                    if ((double)(v) <= (double)(epsx))
                    {
                        break;
                    }
                }

                //
                // Offload center coordinates from PCR to CX,
                // re-calculate exact value of RLo/RHi using CX.
                //
                for (j = 0; j <= nx - 1; j++)
                {
                    cx[j] = pcr[j];
                }
                rlo = math.maxrealnumber;
                rhi = 0;
                for (i = 0; i <= npoints - 1; i++)
                {
                    v = 0;
                    for (j = 0; j <= nx - 1; j++)
                    {
                        v = v + math.sqr(xy[i, j] - cx[j]);
                    }
                    v = Math.Sqrt(v);
                    rhi = Math.Max(rhi, v);
                    rlo = Math.Min(rlo, v);
                }
                if (!userlo)
                {
                    rlo = 0;
                }
                if (!userhi)
                {
                    rhi = 0;
                }
                return;
            }

            //
            // Oooops...!
            //
            ap.assert(false, "FitSphereX: integrity check failed");
        }
    }


}
