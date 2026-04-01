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

public class lsfit
{
    /*************************************************************************
    Polynomial fitting report:
        TerminationType completion code: >0 for success, <0 for failure
        TaskRCond       reciprocal of task's condition number
        RMSError        RMS error
        AvgError        average error
        AvgRelError     average relative error (for non-zero Y[I])
        MaxError        maximum error
    *************************************************************************/
    public class polynomialfitreport : apobject
    {
        public int terminationtype;
        public double taskrcond;
        public double rmserror;
        public double avgerror;
        public double avgrelerror;
        public double maxerror;
        public polynomialfitreport()
        {
            init();
        }
        public override void init()
        {
        }
        public override apobject make_copy()
        {
            polynomialfitreport _result = new polynomialfitreport();
            _result.terminationtype = terminationtype;
            _result.taskrcond = taskrcond;
            _result.rmserror = rmserror;
            _result.avgerror = avgerror;
            _result.avgrelerror = avgrelerror;
            _result.maxerror = maxerror;
            return _result;
        }
    };


    /*************************************************************************
    Barycentric fitting report:
        TerminationType completion code: >0 for success, <0 for failure
        RMSError        RMS error
        AvgError        average error
        AvgRelError     average relative error (for non-zero Y[I])
        MaxError        maximum error
        TaskRCond       reciprocal of task's condition number
    *************************************************************************/
    public class barycentricfitreport : apobject
    {
        public int terminationtype;
        public double taskrcond;
        public int dbest;
        public double rmserror;
        public double avgerror;
        public double avgrelerror;
        public double maxerror;
        public barycentricfitreport()
        {
            init();
        }
        public override void init()
        {
        }
        public override apobject make_copy()
        {
            barycentricfitreport _result = new barycentricfitreport();
            _result.terminationtype = terminationtype;
            _result.taskrcond = taskrcond;
            _result.dbest = dbest;
            _result.rmserror = rmserror;
            _result.avgerror = avgerror;
            _result.avgrelerror = avgrelerror;
            _result.maxerror = maxerror;
            return _result;
        }
    };


    /*************************************************************************
    Least squares fitting report. This structure contains informational fields
    which are set by fitting functions provided by this unit.

    Different functions initialize different sets of  fields,  so  you  should
    read documentation on specific function you used in order  to  know  which
    fields are initialized.
        
        TerminationType filled by all solvers:
                        * positive values, usually 1, denote success
                        * negative values denote various failure scenarios

        TaskRCond       reciprocal of task's condition number
        IterationsCount number of internal iterations
        
        VarIdx          if user-supplied gradient contains errors  which  were
                        detected by nonlinear fitter, this  field  is  set  to
                        index  of  the  first  component  of gradient which is
                        suspected to be spoiled by bugs.

        RMSError        RMS error
        AvgError        average error
        AvgRelError     average relative error (for non-zero Y[I])
        MaxError        maximum error

        WRMSError       weighted RMS error

        CovPar          covariance matrix for parameters, filled by some solvers
        ErrPar          vector of errors in parameters, filled by some solvers
        ErrCurve        vector of fit errors -  variability  of  the  best-fit
                        curve, filled by some solvers.
        Noise           vector of per-point noise estimates, filled by
                        some solvers.
        R2              coefficient of determination (non-weighted, non-adjusted),
                        filled by some solvers.
    *************************************************************************/
    public class lsfitreport : apobject
    {
        public int terminationtype;
        public double taskrcond;
        public int iterationscount;
        public int varidx;
        public double rmserror;
        public double avgerror;
        public double avgrelerror;
        public double maxerror;
        public double wrmserror;
        public double[,] covpar;
        public double[] errpar;
        public double[] errcurve;
        public double[] noise;
        public double r2;
        public lsfitreport()
        {
            init();
        }
        public override void init()
        {
            covpar = new double[0, 0];
            errpar = new double[0];
            errcurve = new double[0];
            noise = new double[0];
        }
        public override apobject make_copy()
        {
            lsfitreport _result = new lsfitreport();
            _result.terminationtype = terminationtype;
            _result.taskrcond = taskrcond;
            _result.iterationscount = iterationscount;
            _result.varidx = varidx;
            _result.rmserror = rmserror;
            _result.avgerror = avgerror;
            _result.avgrelerror = avgrelerror;
            _result.maxerror = maxerror;
            _result.wrmserror = wrmserror;
            _result.covpar = (double[,])covpar.Clone();
            _result.errpar = (double[])errpar.Clone();
            _result.errcurve = (double[])errcurve.Clone();
            _result.noise = (double[])noise.Clone();
            _result.r2 = r2;
            return _result;
        }
    };


    /*************************************************************************
    Nonlinear fitter.

    You should use ALGLIB functions to work with fitter.
    Never try to access its fields directly!
    *************************************************************************/
    public class lsfitstate : apobject
    {
        public int optalgo;
        public int m;
        public int k;
        public double epsx;
        public int maxits;
        public double stpmax;
        public bool xrep;
        public double[] c0;
        public double[] c1;
        public double[] s;
        public double[] bndl;
        public double[] bndu;
        public double[,] taskx;
        public double[] tasky;
        public int npoints;
        public double[] taskw;
        public int nweights;
        public int wkind;
        public int wits;
        public double diffstep;
        public double teststep;
        public double[,] cleic;
        public int nec;
        public int nic;
        public bool xupdated;
        public bool needf;
        public bool needfg;
        public bool needfgh;
        public int pointindex;
        public double[] x;
        public double[] c;
        public double f;
        public double[] g;
        public double[,] h;
        public double[] wcur;
        public int[] tmpct;
        public double[] tmp;
        public double[] tmpf;
        public double[,] tmpjac;
        public double[,] tmpjacw;
        public double tmpnoise;
        public int repiterationscount;
        public int repterminationtype;
        public int repvaridx;
        public double reprmserror;
        public double repavgerror;
        public double repavgrelerror;
        public double repmaxerror;
        public double repwrmserror;
        public lsfitreport rep;
        public minlm.minlmstate optstate;
        public minlm.minlmreport optrep;
        public int prevnpt;
        public int prevalgo;
        public rcommstate rstate;
        public lsfitstate()
        {
            init();
        }
        public override void init()
        {
            c0 = new double[0];
            c1 = new double[0];
            s = new double[0];
            bndl = new double[0];
            bndu = new double[0];
            taskx = new double[0, 0];
            tasky = new double[0];
            taskw = new double[0];
            cleic = new double[0, 0];
            x = new double[0];
            c = new double[0];
            g = new double[0];
            h = new double[0, 0];
            wcur = new double[0];
            tmpct = new int[0];
            tmp = new double[0];
            tmpf = new double[0];
            tmpjac = new double[0, 0];
            tmpjacw = new double[0, 0];
            rep = new lsfitreport();
            optstate = new minlm.minlmstate();
            optrep = new minlm.minlmreport();
            rstate = new rcommstate();
        }
        public override apobject make_copy()
        {
            lsfitstate _result = new lsfitstate();
            _result.optalgo = optalgo;
            _result.m = m;
            _result.k = k;
            _result.epsx = epsx;
            _result.maxits = maxits;
            _result.stpmax = stpmax;
            _result.xrep = xrep;
            _result.c0 = (double[])c0.Clone();
            _result.c1 = (double[])c1.Clone();
            _result.s = (double[])s.Clone();
            _result.bndl = (double[])bndl.Clone();
            _result.bndu = (double[])bndu.Clone();
            _result.taskx = (double[,])taskx.Clone();
            _result.tasky = (double[])tasky.Clone();
            _result.npoints = npoints;
            _result.taskw = (double[])taskw.Clone();
            _result.nweights = nweights;
            _result.wkind = wkind;
            _result.wits = wits;
            _result.diffstep = diffstep;
            _result.teststep = teststep;
            _result.cleic = (double[,])cleic.Clone();
            _result.nec = nec;
            _result.nic = nic;
            _result.xupdated = xupdated;
            _result.needf = needf;
            _result.needfg = needfg;
            _result.needfgh = needfgh;
            _result.pointindex = pointindex;
            _result.x = (double[])x.Clone();
            _result.c = (double[])c.Clone();
            _result.f = f;
            _result.g = (double[])g.Clone();
            _result.h = (double[,])h.Clone();
            _result.wcur = (double[])wcur.Clone();
            _result.tmpct = (int[])tmpct.Clone();
            _result.tmp = (double[])tmp.Clone();
            _result.tmpf = (double[])tmpf.Clone();
            _result.tmpjac = (double[,])tmpjac.Clone();
            _result.tmpjacw = (double[,])tmpjacw.Clone();
            _result.tmpnoise = tmpnoise;
            _result.repiterationscount = repiterationscount;
            _result.repterminationtype = repterminationtype;
            _result.repvaridx = repvaridx;
            _result.reprmserror = reprmserror;
            _result.repavgerror = repavgerror;
            _result.repavgrelerror = repavgrelerror;
            _result.repmaxerror = repmaxerror;
            _result.repwrmserror = repwrmserror;
            _result.rep = (lsfitreport)rep.make_copy();
            _result.optstate = (minlm.minlmstate)optstate.make_copy();
            _result.optrep = (minlm.minlmreport)optrep.make_copy();
            _result.prevnpt = prevnpt;
            _result.prevalgo = prevalgo;
            _result.rstate = (rcommstate)rstate.make_copy();
            return _result;
        }
    };




    /*************************************************************************
    This  subroutine fits piecewise linear curve to points with Ramer-Douglas-
    Peucker algorithm, which stops after generating specified number of linear
    sections.

    IMPORTANT:
    * it does NOT perform least-squares fitting; it  builds  curve,  but  this
      curve does not minimize some least squares metric.  See  description  of
      RDP algorithm (say, in Wikipedia) for more details on WHAT is performed.
    * this function does NOT work with parametric curves  (i.e.  curves  which
      can be represented as {X(t),Y(t)}. It works with curves   which  can  be
      represented as Y(X). Thus,  it  is  impossible  to  model  figures  like
      circles  with  this  functions.
      If  you  want  to  work  with  parametric   curves,   you   should   use
      ParametricRDPFixed() function provided  by  "Parametric"  subpackage  of
      "Interpolation" package.

    INPUT PARAMETERS:
        X       -   array of X-coordinates:
                    * at least N elements
                    * can be unordered (points are automatically sorted)
                    * this function may accept non-distinct X (see below for
                      more information on handling of such inputs)
        Y       -   array of Y-coordinates:
                    * at least N elements
        N       -   number of elements in X/Y
        M       -   desired number of sections:
                    * at most M sections are generated by this function
                    * less than M sections can be generated if we have N<M
                      (or some X are non-distinct).

    OUTPUT PARAMETERS:
        X2      -   X-values of corner points for piecewise approximation,
                    has length NSections+1 or zero (for NSections=0).
        Y2      -   Y-values of corner points,
                    has length NSections+1 or zero (for NSections=0).
        NSections-  number of sections found by algorithm, NSections<=M,
                    NSections can be zero for degenerate datasets
                    (N<=1 or all X[] are non-distinct).

    NOTE: X2/Y2 are ordered arrays, i.e. (X2[0],Y2[0]) is  a  first  point  of
          curve, (X2[NSection-1],Y2[NSection-1]) is the last point.

      -- ALGLIB --
         Copyright 02.10.2014 by Bochkanov Sergey
    *************************************************************************/
    public static void lstfitpiecewiselinearrdpfixed(double[] x,
        double[] y,
        int n,
        int m,
        ref double[] x2,
        ref double[] y2,
        ref int nsections,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        int k0 = 0;
        int k1 = 0;
        int k2 = 0;
        double[] buf0 = new double[0];
        double[] buf1 = new double[0];
        double[,] sections = new double[0, 0];
        double[] points = new double[0];
        double v = 0;
        int worstidx = 0;
        double worsterror = 0;
        int idx0 = 0;
        int idx1 = 0;
        double e0 = 0;
        double e1 = 0;
        double[] heaperrors = new double[0];
        int[] heaptags = new int[0];

        x = (double[])x.Clone();
        y = (double[])y.Clone();
        x2 = new double[0];
        y2 = new double[0];
        nsections = 0;

        ap.assert(n >= 0, "LSTFitPiecewiseLinearRDPFixed: N<0");
        ap.assert(m >= 1, "LSTFitPiecewiseLinearRDPFixed: M<1");
        ap.assert(ap.len(x) >= n, "LSTFitPiecewiseLinearRDPFixed: Length(X)<N");
        ap.assert(ap.len(y) >= n, "LSTFitPiecewiseLinearRDPFixed: Length(Y)<N");
        if (n <= 1)
        {
            nsections = 0;
            return;
        }

        //
        // Sort points.
        // Handle possible ties (tied values are replaced by their mean)
        //
        tsort.tagsortfastr(ref x, ref y, ref buf0, ref buf1, n, _params);
        i = 0;
        while (i <= n - 1)
        {
            j = i + 1;
            v = y[i];
            while (j <= n - 1 && (double)(x[j]) == (double)(x[i]))
            {
                v = v + y[j];
                j = j + 1;
            }
            v = v / (j - i);
            for (k = i; k <= j - 1; k++)
            {
                y[k] = v;
            }
            i = j;
        }

        //
        // Handle degenerate case x[0]=x[N-1]
        //
        if ((double)(x[n - 1]) == (double)(x[0]))
        {
            nsections = 0;
            return;
        }

        //
        // Prepare first section
        //
        rdpanalyzesection(x, y, 0, n - 1, ref worstidx, ref worsterror, _params);
        sections = new double[m, 4];
        heaperrors = new double[m];
        heaptags = new int[m];
        nsections = 1;
        sections[0, 0] = 0;
        sections[0, 1] = n - 1;
        sections[0, 2] = worstidx;
        sections[0, 3] = worsterror;
        heaperrors[0] = worsterror;
        heaptags[0] = 0;
        ap.assert((double)(sections[0, 1]) == (double)(n - 1), "RDP algorithm: integrity check failed");

        //
        // Main loop.
        // Repeatedly find section with worst error and divide it.
        // Terminate after M-th section, or because of other reasons (see loop internals).
        //
        while (nsections < m)
        {

            //
            // Break if worst section has zero error.
            // Store index of worst section to K.
            //
            if ((double)(heaperrors[0]) == (double)(0))
            {
                break;
            }
            k = heaptags[0];

            //
            // K-th section is divided in two:
            // * first  one spans interval from X[Sections[K,0]] to X[Sections[K,2]]
            // * second one spans interval from X[Sections[K,2]] to X[Sections[K,1]]
            //
            // First section is stored at K-th position, second one is appended to the table.
            // Then we update heap which stores pairs of (error,section_index)
            //
            k0 = (int)Math.Round(sections[k, 0]);
            k1 = (int)Math.Round(sections[k, 1]);
            k2 = (int)Math.Round(sections[k, 2]);
            rdpanalyzesection(x, y, k0, k2, ref idx0, ref e0, _params);
            rdpanalyzesection(x, y, k2, k1, ref idx1, ref e1, _params);
            sections[k, 0] = k0;
            sections[k, 1] = k2;
            sections[k, 2] = idx0;
            sections[k, 3] = e0;
            tsort.tagheapreplacetopi(ref heaperrors, ref heaptags, nsections, e0, k, _params);
            sections[nsections, 0] = k2;
            sections[nsections, 1] = k1;
            sections[nsections, 2] = idx1;
            sections[nsections, 3] = e1;
            tsort.tagheappushi(ref heaperrors, ref heaptags, ref nsections, e1, nsections, _params);
        }

        //
        // Convert from sections to points
        //
        points = new double[nsections + 1];
        k = (int)Math.Round(sections[0, 1]);
        for (i = 0; i <= nsections - 1; i++)
        {
            points[i] = (int)Math.Round(sections[i, 0]);
            if ((double)(x[(int)Math.Round(sections[i, 1])]) > (double)(x[k]))
            {
                k = (int)Math.Round(sections[i, 1]);
            }
        }
        points[nsections] = k;
        tsort.tagsortfast(ref points, ref buf0, nsections + 1, _params);

        //
        // Output sections:
        // * first NSection elements of X2/Y2 are filled by x/y at left boundaries of sections
        // * last element of X2/Y2 is filled by right boundary of rightmost section
        // * X2/Y2 is sorted by ascending of X2
        //
        x2 = new double[nsections + 1];
        y2 = new double[nsections + 1];
        for (i = 0; i <= nsections; i++)
        {
            x2[i] = x[(int)Math.Round(points[i])];
            y2[i] = y[(int)Math.Round(points[i])];
        }
    }


    /*************************************************************************
    This  subroutine fits piecewise linear curve to points with Ramer-Douglas-
    Peucker algorithm, which stops after achieving desired precision.

    IMPORTANT:
    * it performs non-least-squares fitting; it builds curve, but  this  curve
      does not minimize some least squares  metric.  See  description  of  RDP
      algorithm (say, in Wikipedia) for more details on WHAT is performed.
    * this function does NOT work with parametric curves  (i.e.  curves  which
      can be represented as {X(t),Y(t)}. It works with curves   which  can  be
      represented as Y(X). Thus, it is impossible to model figures like circles
      with this functions.
      If  you  want  to  work  with  parametric   curves,   you   should   use
      ParametricRDPFixed() function provided  by  "Parametric"  subpackage  of
      "Interpolation" package.

    INPUT PARAMETERS:
        X       -   array of X-coordinates:
                    * at least N elements
                    * can be unordered (points are automatically sorted)
                    * this function may accept non-distinct X (see below for
                      more information on handling of such inputs)
        Y       -   array of Y-coordinates:
                    * at least N elements
        N       -   number of elements in X/Y
        Eps     -   positive number, desired precision.
        

    OUTPUT PARAMETERS:
        X2      -   X-values of corner points for piecewise approximation,
                    has length NSections+1 or zero (for NSections=0).
        Y2      -   Y-values of corner points,
                    has length NSections+1 or zero (for NSections=0).
        NSections-  number of sections found by algorithm,
                    NSections can be zero for degenerate datasets
                    (N<=1 or all X[] are non-distinct).

    NOTE: X2/Y2 are ordered arrays, i.e. (X2[0],Y2[0]) is  a  first  point  of
          curve, (X2[NSection-1],Y2[NSection-1]) is the last point.

      -- ALGLIB --
         Copyright 02.10.2014 by Bochkanov Sergey
    *************************************************************************/
    public static void lstfitpiecewiselinearrdp(double[] x,
        double[] y,
        int n,
        double eps,
        ref double[] x2,
        ref double[] y2,
        ref int nsections,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        double[] buf0 = new double[0];
        double[] buf1 = new double[0];
        double[] xtmp = new double[0];
        double[] ytmp = new double[0];
        double v = 0;
        int npts = 0;

        x = (double[])x.Clone();
        y = (double[])y.Clone();
        x2 = new double[0];
        y2 = new double[0];
        nsections = 0;

        ap.assert(n >= 0, "LSTFitPiecewiseLinearRDP: N<0");
        ap.assert((double)(eps) > (double)(0), "LSTFitPiecewiseLinearRDP: Eps<=0");
        ap.assert(ap.len(x) >= n, "LSTFitPiecewiseLinearRDP: Length(X)<N");
        ap.assert(ap.len(y) >= n, "LSTFitPiecewiseLinearRDP: Length(Y)<N");
        if (n <= 1)
        {
            nsections = 0;
            return;
        }

        //
        // Sort points.
        // Handle possible ties (tied values are replaced by their mean)
        //
        tsort.tagsortfastr(ref x, ref y, ref buf0, ref buf1, n, _params);
        i = 0;
        while (i <= n - 1)
        {
            j = i + 1;
            v = y[i];
            while (j <= n - 1 && (double)(x[j]) == (double)(x[i]))
            {
                v = v + y[j];
                j = j + 1;
            }
            v = v / (j - i);
            for (k = i; k <= j - 1; k++)
            {
                y[k] = v;
            }
            i = j;
        }

        //
        // Handle degenerate case x[0]=x[N-1]
        //
        if ((double)(x[n - 1]) == (double)(x[0]))
        {
            nsections = 0;
            return;
        }

        //
        // Prepare data for recursive algorithm
        //
        xtmp = new double[n];
        ytmp = new double[n];
        npts = 2;
        xtmp[0] = x[0];
        ytmp[0] = y[0];
        xtmp[1] = x[n - 1];
        ytmp[1] = y[n - 1];
        rdprecursive(x, y, 0, n - 1, eps, xtmp, ytmp, ref npts, _params);

        //
        // Output sections:
        // * first NSection elements of X2/Y2 are filled by x/y at left boundaries of sections
        // * last element of X2/Y2 is filled by right boundary of rightmost section
        // * X2/Y2 is sorted by ascending of X2
        //
        nsections = npts - 1;
        x2 = new double[npts];
        y2 = new double[npts];
        for (i = 0; i <= nsections; i++)
        {
            x2[i] = xtmp[i];
            y2[i] = ytmp[i];
        }
        tsort.tagsortfastr(ref x2, ref y2, ref buf0, ref buf1, npts, _params);
    }


    /*************************************************************************
    Fitting by polynomials in barycentric form. This function provides  simple
    unterface for unconstrained unweighted fitting. See  PolynomialFitWC()  if
    you need constrained fitting.

    The task is linear, thus the linear least  squares  solver  is  used.  The
    complexity of this computational scheme is O(N*M^2), mostly  dominated  by
    the least squares solver

    SEE ALSO:
        PolynomialFitWC()

    NOTES:
        you can convert P from barycentric form  to  the  power  or  Chebyshev
        basis with PolynomialBar2Pow() or PolynomialBar2Cheb() functions  from
        POLINT subpackage.

    INPUT PARAMETERS:
        X   -   points, array[0..N-1].
        Y   -   function values, array[0..N-1].
        N   -   number of points, N>0
                * if given, only leading N elements of X/Y are used
                * if not given, automatically determined from sizes of X/Y
        M   -   number of basis functions (= polynomial_degree + 1), M>=1

    OUTPUT PARAMETERS:
        P   -   interpolant in barycentric form for Rep.TerminationType>0.
                undefined for Rep.TerminationType<0.
        Rep -   fitting report. The following fields are set:
                    * Rep.TerminationType is a completion code which is always
                      set to 1 (success)
                    * RMSError      rms error on the (X,Y).
                    * AvgError      average error on the (X,Y).
                    * AvgRelError   average relative error on the non-zero Y
                    * MaxError      maximum error
                                    NON-WEIGHTED ERRORS ARE CALCULATED

      ! FREE EDITION OF ALGLIB:
      ! 
      ! Free Edition of ALGLIB supports following important features for  this
      ! function:
      ! * C++ version: x64 SIMD support using C++ intrinsics
      ! * C#  version: x64 SIMD support using NET5/NetCore hardware intrinsics
      !
      ! We  recommend  you  to  read  'Compiling ALGLIB' section of the ALGLIB
      ! Reference Manual in order  to  find  out  how to activate SIMD support
      ! in ALGLIB.

      ! COMMERCIAL EDITION OF ALGLIB:
      ! 
      ! Commercial Edition of ALGLIB includes following important improvements
      ! of this function:
      ! * high-performance native backend with same C# interface (C# version)
      ! * multithreading support (C++ and C# versions)
      ! * hardware vendor (Intel) implementations of linear algebra primitives
      !   (C++ and C# versions, x86/x64 platform)
      ! 
      ! We recommend you to read 'Working with commercial version' section  of
      ! ALGLIB Reference Manual in order to find out how to  use  performance-
      ! related features provided by commercial edition of ALGLIB.

      -- ALGLIB PROJECT --
         Copyright 10.12.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void polynomialfit(double[] x,
        double[] y,
        int n,
        int m,
        ratint.barycentricinterpolant p,
        polynomialfitreport rep,
        xparams _params)
    {
        int i = 0;
        double[] w = new double[0];
        double[] xc = new double[0];
        double[] yc = new double[0];
        int[] dc = new int[0];

        ap.assert(n > 0, "PolynomialFit: N<=0!");
        ap.assert(m > 0, "PolynomialFit: M<=0!");
        ap.assert(ap.len(x) >= n, "PolynomialFit: Length(X)<N!");
        ap.assert(ap.len(y) >= n, "PolynomialFit: Length(Y)<N!");
        ap.assert(apserv.isfinitevector(x, n, _params), "PolynomialFit: X contains infinite or NaN values!");
        ap.assert(apserv.isfinitevector(y, n, _params), "PolynomialFit: Y contains infinite or NaN values!");
        w = new double[n];
        for (i = 0; i <= n - 1; i++)
        {
            w[i] = 1;
        }
        polynomialfitwc(x, y, w, n, xc, yc, dc, 0, m, p, rep, _params);
    }


    /*************************************************************************
    Weighted  fitting by polynomials in barycentric form, with constraints  on
    function values or first derivatives.

    Small regularizing term is used when solving constrained tasks (to improve
    stability).

    Task is linear, so linear least squares solver is used. Complexity of this
    computational scheme is O(N*M^2), mostly dominated by least squares solver

    SEE ALSO:
        PolynomialFit()

    NOTES:
        you can convert P from barycentric form  to  the  power  or  Chebyshev
        basis with PolynomialBar2Pow() or PolynomialBar2Cheb() functions  from
        the POLINT subpackage.

    INPUT PARAMETERS:
        X   -   points, array[0..N-1].
        Y   -   function values, array[0..N-1].
        W   -   weights, array[0..N-1]
                Each summand in square  sum  of  approximation deviations from
                given  values  is  multiplied  by  the square of corresponding
                weight. Fill it by 1's if you don't  want  to  solve  weighted
                task.
        N   -   number of points, N>0.
                * if given, only leading N elements of X/Y/W are used
                * if not given, automatically determined from sizes of X/Y/W
        XC  -   points where polynomial values/derivatives are constrained,
                array[0..K-1].
        YC  -   values of constraints, array[0..K-1]
        DC  -   array[0..K-1], types of constraints:
                * DC[i]=0   means that P(XC[i])=YC[i]
                * DC[i]=1   means that P'(XC[i])=YC[i]
                SEE BELOW FOR IMPORTANT INFORMATION ON CONSTRAINTS
        K   -   number of constraints, 0<=K<M.
                K=0 means no constraints (XC/YC/DC are not used in such cases)
        M   -   number of basis functions (= polynomial_degree + 1), M>=1

    OUTPUT PARAMETERS:
        P   -   interpolant in barycentric form for Rep.TerminationType>0.
                undefined for Rep.TerminationType<0.
        Rep -   fitting report. The following fields are set:
                    * Rep.TerminationType is a completion code:
                      * set to  1 on success
                      * set to -3 on failure due to  problematic  constraints:
                        either too many  constraints,  degenerate  constraints
                        or inconsistent constraints were passed
                    * RMSError      rms error on the (X,Y).
                    * AvgError      average error on the (X,Y).
                    * AvgRelError   average relative error on the non-zero Y
                    * MaxError      maximum error
                                    NON-WEIGHTED ERRORS ARE CALCULATED

    IMPORTANT:
        this subroitine doesn't calculate task's condition number for K<>0.

    SETTING CONSTRAINTS - DANGERS AND OPPORTUNITIES:

    Setting constraints can lead  to undesired  results,  like ill-conditioned
    behavior, or inconsistency being detected. From the other side,  it allows
    us to improve quality of the fit. Here we summarize  our  experience  with
    constrained regression splines:
    * even simple constraints can be inconsistent, see  Wikipedia  article  on
      this subject: http://en.wikipedia.org/wiki/Birkhoff_interpolation
    * the  greater  is  M (given  fixed  constraints),  the  more chances that
      constraints will be consistent
    * in the general case, consistency of constraints is NOT GUARANTEED.
    * in the one special cases, however, we can  guarantee  consistency.  This
      case  is:  M>1  and constraints on the function values (NOT DERIVATIVES)

    Our final recommendation is to use constraints  WHEN  AND  ONLY  when  you
    can't solve your task without them. Anything beyond  special  cases  given
    above is not guaranteed and may result in inconsistency.

      ! FREE EDITION OF ALGLIB:
      ! 
      ! Free Edition of ALGLIB supports following important features for  this
      ! function:
      ! * C++ version: x64 SIMD support using C++ intrinsics
      ! * C#  version: x64 SIMD support using NET5/NetCore hardware intrinsics
      !
      ! We  recommend  you  to  read  'Compiling ALGLIB' section of the ALGLIB
      ! Reference Manual in order  to  find  out  how to activate SIMD support
      ! in ALGLIB.

      ! COMMERCIAL EDITION OF ALGLIB:
      ! 
      ! Commercial Edition of ALGLIB includes following important improvements
      ! of this function:
      ! * high-performance native backend with same C# interface (C# version)
      ! * multithreading support (C++ and C# versions)
      ! * hardware vendor (Intel) implementations of linear algebra primitives
      !   (C++ and C# versions, x86/x64 platform)
      ! 
      ! We recommend you to read 'Working with commercial version' section  of
      ! ALGLIB Reference Manual in order to find out how to  use  performance-
      ! related features provided by commercial edition of ALGLIB.

      -- ALGLIB PROJECT --
         Copyright 10.12.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void polynomialfitwc(double[] x,
        double[] y,
        double[] w,
        int n,
        double[] xc,
        double[] yc,
        int[] dc,
        int k,
        int m,
        ratint.barycentricinterpolant p,
        polynomialfitreport rep,
        xparams _params)
    {
        double xa = 0;
        double xb = 0;
        double sa = 0;
        double sb = 0;
        double[] xoriginal = new double[0];
        double[] yoriginal = new double[0];
        double[] y2 = new double[0];
        double[] w2 = new double[0];
        double[] tmp = new double[0];
        double[] tmp2 = new double[0];
        double[] bx = new double[0];
        double[] by = new double[0];
        double[] bw = new double[0];
        int i = 0;
        int j = 0;
        double u = 0;
        double v = 0;
        double s = 0;
        int relcnt = 0;
        lsfitreport lrep = new lsfitreport();

        x = (double[])x.Clone();
        y = (double[])y.Clone();
        w = (double[])w.Clone();
        xc = (double[])xc.Clone();
        yc = (double[])yc.Clone();

        ap.assert(n > 0, "PolynomialFitWC: N<=0!");
        ap.assert(m > 0, "PolynomialFitWC: M<=0!");
        ap.assert(k >= 0, "PolynomialFitWC: K<0!");
        ap.assert(k < m, "PolynomialFitWC: K>=M!");
        ap.assert(ap.len(x) >= n, "PolynomialFitWC: Length(X)<N!");
        ap.assert(ap.len(y) >= n, "PolynomialFitWC: Length(Y)<N!");
        ap.assert(ap.len(w) >= n, "PolynomialFitWC: Length(W)<N!");
        ap.assert(ap.len(xc) >= k, "PolynomialFitWC: Length(XC)<K!");
        ap.assert(ap.len(yc) >= k, "PolynomialFitWC: Length(YC)<K!");
        ap.assert(ap.len(dc) >= k, "PolynomialFitWC: Length(DC)<K!");
        ap.assert(apserv.isfinitevector(x, n, _params), "PolynomialFitWC: X contains infinite or NaN values!");
        ap.assert(apserv.isfinitevector(y, n, _params), "PolynomialFitWC: Y contains infinite or NaN values!");
        ap.assert(apserv.isfinitevector(w, n, _params), "PolynomialFitWC: X contains infinite or NaN values!");
        ap.assert(apserv.isfinitevector(xc, k, _params), "PolynomialFitWC: XC contains infinite or NaN values!");
        ap.assert(apserv.isfinitevector(yc, k, _params), "PolynomialFitWC: YC contains infinite or NaN values!");
        for (i = 0; i <= k - 1; i++)
        {
            ap.assert(dc[i] == 0 || dc[i] == 1, "PolynomialFitWC: one of DC[] is not 0 or 1!");
        }

        //
        // Scale X, Y, XC, YC.
        // Solve scaled problem using internal Chebyshev fitting function.
        //
        intfitserv.lsfitscalexy(ref x, ref y, ref w, n, ref xc, ref yc, dc, k, ref xa, ref xb, ref sa, ref sb, ref xoriginal, ref yoriginal, _params);
        internalchebyshevfit(x, y, w, n, xc, yc, dc, k, m, ref tmp, lrep, _params);
        rep.terminationtype = lrep.terminationtype;
        if (rep.terminationtype <= 0)
        {
            return;
        }

        //
        // Generate barycentric model and scale it
        // * BX, BY store barycentric model nodes
        // * FMatrix is reused (remember - it is at least MxM, what we need)
        //
        // Model intialization is done in O(M^2). In principle, it can be
        // done in O(M*log(M)), but before it we solved task with O(N*M^2)
        // complexity, so it is only a small amount of total time spent.
        //
        bx = new double[m];
        by = new double[m];
        bw = new double[m];
        tmp2 = new double[m];
        s = 1;
        for (i = 0; i <= m - 1; i++)
        {
            if (m != 1)
            {
                u = Math.Cos(Math.PI * i / (m - 1));
            }
            else
            {
                u = 0;
            }
            v = 0;
            for (j = 0; j <= m - 1; j++)
            {
                if (j == 0)
                {
                    tmp2[j] = 1;
                }
                else
                {
                    if (j == 1)
                    {
                        tmp2[j] = u;
                    }
                    else
                    {
                        tmp2[j] = 2 * u * tmp2[j - 1] - tmp2[j - 2];
                    }
                }
                v = v + tmp[j] * tmp2[j];
            }
            bx[i] = u;
            by[i] = v;
            bw[i] = s;
            if (i == 0 || i == m - 1)
            {
                bw[i] = 0.5 * bw[i];
            }
            s = -s;
        }
        ratint.barycentricbuildxyw(bx, by, bw, m, p, _params);
        ratint.barycentriclintransx(p, 2 / (xb - xa), -((xa + xb) / (xb - xa)), _params);
        ratint.barycentriclintransy(p, sb - sa, sa, _params);

        //
        // Scale absolute errors obtained from LSFitLinearW.
        // Relative error should be calculated separately
        // (because of shifting/scaling of the task)
        //
        rep.taskrcond = lrep.taskrcond;
        rep.rmserror = lrep.rmserror * (sb - sa);
        rep.avgerror = lrep.avgerror * (sb - sa);
        rep.maxerror = lrep.maxerror * (sb - sa);
        rep.avgrelerror = 0;
        relcnt = 0;
        for (i = 0; i <= n - 1; i++)
        {
            if ((double)(yoriginal[i]) != (double)(0))
            {
                rep.avgrelerror = rep.avgrelerror + Math.Abs(ratint.barycentriccalc(p, xoriginal[i], _params) - yoriginal[i]) / Math.Abs(yoriginal[i]);
                relcnt = relcnt + 1;
            }
        }
        if (relcnt != 0)
        {
            rep.avgrelerror = rep.avgrelerror / relcnt;
        }
    }


    /*************************************************************************
    This function calculates value of four-parameter logistic (4PL)  model  at
    specified point X. 4PL model has following form:

        F(x|A,B,C,D) = D+(A-D)/(1+Power(x/C,B))

    INPUT PARAMETERS:
        X       -   current point, X>=0:
                    * zero X is correctly handled even for B<=0
                    * negative X results in exception.
        A, B, C, D- parameters of 4PL model:
                    * A is unconstrained
                    * B is unconstrained; zero or negative values are handled
                      correctly.
                    * C>0, non-positive value results in exception
                    * D is unconstrained
                    
    RESULT:
        model value at X

    NOTE: if B=0, denominator is assumed to be equal to 2.0 even  for  zero  X
          (strictly speaking, 0^0 is undefined).

    NOTE: this function also throws exception  if  all  input  parameters  are
          correct, but overflow was detected during calculations.
          
    NOTE: this function performs a lot of checks;  if  you  need  really  high
          performance, consider evaluating model  yourself,  without  checking
          for degenerate cases.
          
        
      -- ALGLIB PROJECT --
         Copyright 14.05.2014 by Bochkanov Sergey
    *************************************************************************/
    public static double logisticcalc4(double x,
        double a,
        double b,
        double c,
        double d,
        xparams _params)
    {
        double result = 0;

        ap.assert(math.isfinite(x), "LogisticCalc4: X is not finite");
        ap.assert(math.isfinite(a), "LogisticCalc4: A is not finite");
        ap.assert(math.isfinite(b), "LogisticCalc4: B is not finite");
        ap.assert(math.isfinite(c), "LogisticCalc4: C is not finite");
        ap.assert(math.isfinite(d), "LogisticCalc4: D is not finite");
        ap.assert((double)(x) >= (double)(0), "LogisticCalc4: X is negative");
        ap.assert((double)(c) > (double)(0), "LogisticCalc4: C is non-positive");

        //
        // Check for degenerate cases
        //
        if ((double)(b) == (double)(0))
        {
            result = 0.5 * (a + d);
            return result;
        }
        if ((double)(x) == (double)(0))
        {
            if ((double)(b) > (double)(0))
            {
                result = a;
            }
            else
            {
                result = d;
            }
            return result;
        }

        //
        // General case
        //
        result = d + (a - d) / (1.0 + Math.Pow(x / c, b));
        ap.assert(math.isfinite(result), "LogisticCalc4: overflow during calculations");
        return result;
    }


    /*************************************************************************
    This function calculates value of five-parameter logistic (5PL)  model  at
    specified point X. 5PL model has following form:

        F(x|A,B,C,D,G) = D+(A-D)/Power(1+Power(x/C,B),G)

    INPUT PARAMETERS:
        X       -   current point, X>=0:
                    * zero X is correctly handled even for B<=0
                    * negative X results in exception.
        A, B, C, D, G- parameters of 5PL model:
                    * A is unconstrained
                    * B is unconstrained; zero or negative values are handled
                      correctly.
                    * C>0, non-positive value results in exception
                    * D is unconstrained
                    * G>0, non-positive value results in exception
                    
    RESULT:
        model value at X

    NOTE: if B=0, denominator is assumed to be equal to Power(2.0,G) even  for
          zero X (strictly speaking, 0^0 is undefined).

    NOTE: this function also throws exception  if  all  input  parameters  are
          correct, but overflow was detected during calculations.
          
    NOTE: this function performs a lot of checks;  if  you  need  really  high
          performance, consider evaluating model  yourself,  without  checking
          for degenerate cases.
          
        
      -- ALGLIB PROJECT --
         Copyright 14.05.2014 by Bochkanov Sergey
    *************************************************************************/
    public static double logisticcalc5(double x,
        double a,
        double b,
        double c,
        double d,
        double g,
        xparams _params)
    {
        double result = 0;

        ap.assert(math.isfinite(x), "LogisticCalc5: X is not finite");
        ap.assert(math.isfinite(a), "LogisticCalc5: A is not finite");
        ap.assert(math.isfinite(b), "LogisticCalc5: B is not finite");
        ap.assert(math.isfinite(c), "LogisticCalc5: C is not finite");
        ap.assert(math.isfinite(d), "LogisticCalc5: D is not finite");
        ap.assert(math.isfinite(g), "LogisticCalc5: G is not finite");
        ap.assert((double)(x) >= (double)(0), "LogisticCalc5: X is negative");
        ap.assert((double)(c) > (double)(0), "LogisticCalc5: C is non-positive");
        ap.assert((double)(g) > (double)(0), "LogisticCalc5: G is non-positive");

        //
        // Check for degenerate cases
        //
        if ((double)(b) == (double)(0))
        {
            result = d + (a - d) / Math.Pow(2.0, g);
            return result;
        }
        if ((double)(x) == (double)(0))
        {
            if ((double)(b) > (double)(0))
            {
                result = a;
            }
            else
            {
                result = d;
            }
            return result;
        }

        //
        // General case
        //
        result = d + (a - d) / Math.Pow(1.0 + Math.Pow(x / c, b), g);
        ap.assert(math.isfinite(result), "LogisticCalc5: overflow during calculations");
        return result;
    }


    /*************************************************************************
    This function fits four-parameter logistic (4PL) model  to  data  provided
    by user. 4PL model has following form:

        F(x|A,B,C,D) = D+(A-D)/(1+Power(x/C,B))

    Here:
        * A, D - unconstrained (see LogisticFit4EC() for constrained 4PL)
        * B>=0
        * C>0
        
    IMPORTANT: output of this function is constrained in  such  way that  B>0.
               Because 4PL model is symmetric with respect to B, there  is  no
               need to explore  B<0.  Constraining  B  makes  algorithm easier
               to stabilize and debug.
               Users  who  for  some  reason  prefer to work with negative B's
               should transform output themselves (swap A and D, replace B  by
               -B).
               
    4PL fitting is implemented as follows:
    * we perform small number of restarts from random locations which helps to
      solve problem of bad local extrema. Locations are only partially  random
      - we use input data to determine good  initial  guess,  but  we  include
      controlled amount of randomness.
    * we perform Levenberg-Marquardt fitting with very  tight  constraints  on
      parameters B and C - it allows us to find good  initial  guess  for  the
      second stage without risk of running into "flat spot".
    * second  Levenberg-Marquardt  round  is   performed   without   excessive
      constraints. Results from the previous round are used as initial guess.
    * after fitting is done, we compare results with best values found so far,
      rewrite "best solution" if needed, and move to next random location.
      
    Overall algorithm is very stable and is not prone to  bad  local  extrema.
    Furthermore, it automatically scales when input data have  very  large  or
    very small range.

    INPUT PARAMETERS:
        X       -   array[N], stores X-values.
                    MUST include only non-negative numbers  (but  may  include
                    zero values). Can be unsorted.
        Y       -   array[N], values to fit.
        N       -   number of points. If N is less than  length  of  X/Y, only
                    leading N elements are used.
                    
    OUTPUT PARAMETERS:
        A, B, C, D- parameters of 4PL model
        Rep     -   fitting report. This structure has many fields,  but  ONLY
                    ONES LISTED BELOW ARE SET:
                    * Rep.IterationsCount - number of iterations performed
                    * Rep.RMSError - root-mean-square error
                    * Rep.AvgError - average absolute error
                    * Rep.AvgRelError - average relative error (calculated for
                      non-zero Y-values)
                    * Rep.MaxError - maximum absolute error
                    * Rep.R2 - coefficient of determination,  R-squared.  This
                      coefficient   is  calculated  as  R2=1-RSS/TSS  (in case
                      of nonlinear  regression  there  are  multiple  ways  to
                      define R2, each of them giving different results).

    NOTE: for stability reasons the B parameter is restricted by [1/1000,1000]
          range. It prevents  algorithm from making trial steps  deep into the
          area of bad parameters.

    NOTE: after  you  obtained  coefficients,  you  can  evaluate  model  with
          LogisticCalc4() function.

    NOTE: if you need better control over fitting process than provided by this
          function, you may use LogisticFit45X().
                    
    NOTE: step is automatically scaled according to scale of parameters  being
          fitted before we compare its length with EpsX. Thus,  this  function
          can be used to fit data with very small or very large values without
          changing EpsX.
        

      -- ALGLIB PROJECT --
         Copyright 14.02.2014 by Bochkanov Sergey
    *************************************************************************/
    public static void logisticfit4(double[] x,
        double[] y,
        int n,
        ref double a,
        ref double b,
        ref double c,
        ref double d,
        lsfitreport rep,
        xparams _params)
    {
        double g = 0;

        x = (double[])x.Clone();
        y = (double[])y.Clone();
        a = 0;
        b = 0;
        c = 0;
        d = 0;

        logisticfit45x(x, y, n, Double.NaN, Double.NaN, true, 0.0, 0.0, 0, ref a, ref b, ref c, ref d, ref g, rep, _params);
    }


    /*************************************************************************
    This function fits four-parameter logistic (4PL) model  to  data  provided
    by user, with optional constraints on parameters A and D.  4PL  model  has
    following form:

        F(x|A,B,C,D) = D+(A-D)/(1+Power(x/C,B))

    Here:
        * A, D - with optional equality constraints
        * B>=0
        * C>0
        
    IMPORTANT: output of this function is constrained in  such  way that  B>0.
               Because 4PL model is symmetric with respect to B, there  is  no
               need to explore  B<0.  Constraining  B  makes  algorithm easier
               to stabilize and debug.
               Users  who  for  some  reason  prefer to work with negative B's
               should transform output themselves (swap A and D, replace B  by
               -B).
               
    4PL fitting is implemented as follows:
    * we perform small number of restarts from random locations which helps to
      solve problem of bad local extrema. Locations are only partially  random
      - we use input data to determine good  initial  guess,  but  we  include
      controlled amount of randomness.
    * we perform Levenberg-Marquardt fitting with very  tight  constraints  on
      parameters B and C - it allows us to find good  initial  guess  for  the
      second stage without risk of running into "flat spot".
    * second  Levenberg-Marquardt  round  is   performed   without   excessive
      constraints. Results from the previous round are used as initial guess.
    * after fitting is done, we compare results with best values found so far,
      rewrite "best solution" if needed, and move to next random location.
      
    Overall algorithm is very stable and is not prone to  bad  local  extrema.
    Furthermore, it automatically scales when input data have  very  large  or
    very small range.

    INPUT PARAMETERS:
        X       -   array[N], stores X-values.
                    MUST include only non-negative numbers  (but  may  include
                    zero values). Can be unsorted.
        Y       -   array[N], values to fit.
        N       -   number of points. If N is less than  length  of  X/Y, only
                    leading N elements are used.
        CnstrLeft-  optional equality constraint for model value at the   left
                    boundary (at X=0). Specify NAN (Not-a-Number)  if  you  do
                    not need constraint on the model value at X=0 (in C++  you
                    can pass alglib::fp_nan as parameter, in  C#  it  will  be
                    Double.NaN).
                    See  below,  section  "EQUALITY  CONSTRAINTS"   for   more
                    information about constraints.
        CnstrRight- optional equality constraint for model value at X=infinity.
                    Specify NAN (Not-a-Number) if you do not  need  constraint
                    on the model value (in C++  you can pass alglib::fp_nan as
                    parameter, in  C# it will  be Double.NaN).
                    See  below,  section  "EQUALITY  CONSTRAINTS"   for   more
                    information about constraints.
                    
    OUTPUT PARAMETERS:
        A, B, C, D- parameters of 4PL model
        Rep     -   fitting report. This structure has many fields,  but  ONLY
                    ONES LISTED BELOW ARE SET:
                    * Rep.IterationsCount - number of iterations performed
                    * Rep.RMSError - root-mean-square error
                    * Rep.AvgError - average absolute error
                    * Rep.AvgRelError - average relative error (calculated for
                      non-zero Y-values)
                    * Rep.MaxError - maximum absolute error
                    * Rep.R2 - coefficient of determination,  R-squared.  This
                      coefficient   is  calculated  as  R2=1-RSS/TSS  (in case
                      of nonlinear  regression  there  are  multiple  ways  to
                      define R2, each of them giving different results).

    NOTE: for stability reasons the B parameter is restricted by [1/1000,1000]
          range. It prevents  algorithm from making trial steps  deep into the
          area of bad parameters.

    NOTE: after  you  obtained  coefficients,  you  can  evaluate  model  with
          LogisticCalc4() function.

    NOTE: if you need better control over fitting process than provided by this
          function, you may use LogisticFit45X().
                    
    NOTE: step is automatically scaled according to scale of parameters  being
          fitted before we compare its length with EpsX. Thus,  this  function
          can be used to fit data with very small or very large values without
          changing EpsX.

    EQUALITY CONSTRAINTS ON PARAMETERS

    4PL/5PL solver supports equality constraints on model values at  the  left
    boundary (X=0) and right  boundary  (X=infinity).  These  constraints  are
    completely optional and you can specify both of them, only  one  -  or  no
    constraints at all.

    Parameter  CnstrLeft  contains  left  constraint (or NAN for unconstrained
    fitting), and CnstrRight contains right  one.  For  4PL,  left  constraint
    ALWAYS corresponds to parameter A, and right one is ALWAYS  constraint  on
    D. That's because 4PL model is normalized in such way that B>=0.
        

      -- ALGLIB PROJECT --
         Copyright 14.02.2014 by Bochkanov Sergey
    *************************************************************************/
    public static void logisticfit4ec(double[] x,
        double[] y,
        int n,
        double cnstrleft,
        double cnstrright,
        ref double a,
        ref double b,
        ref double c,
        ref double d,
        lsfitreport rep,
        xparams _params)
    {
        double g = 0;

        x = (double[])x.Clone();
        y = (double[])y.Clone();
        a = 0;
        b = 0;
        c = 0;
        d = 0;

        logisticfit45x(x, y, n, cnstrleft, cnstrright, true, 0.0, 0.0, 0, ref a, ref b, ref c, ref d, ref g, rep, _params);
    }


    /*************************************************************************
    This function fits five-parameter logistic (5PL) model  to  data  provided
    by user. 5PL model has following form:

        F(x|A,B,C,D,G) = D+(A-D)/Power(1+Power(x/C,B),G)

    Here:
        * A, D - unconstrained
        * B - unconstrained
        * C>0
        * G>0
        
    IMPORTANT: unlike in  4PL  fitting,  output  of  this  function   is   NOT
               constrained in  such  way that B is guaranteed to be  positive.
               Furthermore,  unlike  4PL,  5PL  model  is  NOT  symmetric with
               respect to B, so you can NOT transform model to equivalent one,
               with B having desired sign (>0 or <0).
        
    5PL fitting is implemented as follows:
    * we perform small number of restarts from random locations which helps to
      solve problem of bad local extrema. Locations are only partially  random
      - we use input data to determine good  initial  guess,  but  we  include
      controlled amount of randomness.
    * we perform Levenberg-Marquardt fitting with very  tight  constraints  on
      parameters B and C - it allows us to find good  initial  guess  for  the
      second stage without risk of running into "flat spot".  Parameter  G  is
      fixed at G=1.
    * second  Levenberg-Marquardt  round  is   performed   without   excessive
      constraints on B and C, but with G still equal to 1.  Results  from  the
      previous round are used as initial guess.
    * third Levenberg-Marquardt round relaxes constraints on G  and  tries  two
      different models - one with B>0 and one with B<0.
    * after fitting is done, we compare results with best values found so far,
      rewrite "best solution" if needed, and move to next random location.
      
    Overall algorithm is very stable and is not prone to  bad  local  extrema.
    Furthermore, it automatically scales when input data have  very  large  or
    very small range.

    INPUT PARAMETERS:
        X       -   array[N], stores X-values.
                    MUST include only non-negative numbers  (but  may  include
                    zero values). Can be unsorted.
        Y       -   array[N], values to fit.
        N       -   number of points. If N is less than  length  of  X/Y, only
                    leading N elements are used.
                    
    OUTPUT PARAMETERS:
        A,B,C,D,G-  parameters of 5PL model
        Rep     -   fitting report. This structure has many fields,  but  ONLY
                    ONES LISTED BELOW ARE SET:
                    * Rep.IterationsCount - number of iterations performed
                    * Rep.RMSError - root-mean-square error
                    * Rep.AvgError - average absolute error
                    * Rep.AvgRelError - average relative error (calculated for
                      non-zero Y-values)
                    * Rep.MaxError - maximum absolute error
                    * Rep.R2 - coefficient of determination,  R-squared.  This
                      coefficient   is  calculated  as  R2=1-RSS/TSS  (in case
                      of nonlinear  regression  there  are  multiple  ways  to
                      define R2, each of them giving different results).

    NOTE: for better stability B  parameter is restricted by [+-1/1000,+-1000]
          range, and G is restricted by [1/10,10] range. It prevents algorithm
          from making trial steps deep into the area of bad parameters.

    NOTE: after  you  obtained  coefficients,  you  can  evaluate  model  with
          LogisticCalc5() function.

    NOTE: if you need better control over fitting process than provided by this
          function, you may use LogisticFit45X().
                    
    NOTE: step is automatically scaled according to scale of parameters  being
          fitted before we compare its length with EpsX. Thus,  this  function
          can be used to fit data with very small or very large values without
          changing EpsX.
        

      -- ALGLIB PROJECT --
         Copyright 14.02.2014 by Bochkanov Sergey
    *************************************************************************/
    public static void logisticfit5(double[] x,
        double[] y,
        int n,
        ref double a,
        ref double b,
        ref double c,
        ref double d,
        ref double g,
        lsfitreport rep,
        xparams _params)
    {
        x = (double[])x.Clone();
        y = (double[])y.Clone();
        a = 0;
        b = 0;
        c = 0;
        d = 0;
        g = 0;

        logisticfit45x(x, y, n, Double.NaN, Double.NaN, false, 0.0, 0.0, 0, ref a, ref b, ref c, ref d, ref g, rep, _params);
    }


    /*************************************************************************
    This function fits five-parameter logistic (5PL) model  to  data  provided
    by user, subject to optional equality constraints on parameters A  and  D.
    5PL model has following form:

        F(x|A,B,C,D,G) = D+(A-D)/Power(1+Power(x/C,B),G)

    Here:
        * A, D - with optional equality constraints
        * B - unconstrained
        * C>0
        * G>0
        
    IMPORTANT: unlike in  4PL  fitting,  output  of  this  function   is   NOT
               constrained in  such  way that B is guaranteed to be  positive.
               Furthermore,  unlike  4PL,  5PL  model  is  NOT  symmetric with
               respect to B, so you can NOT transform model to equivalent one,
               with B having desired sign (>0 or <0).
        
    5PL fitting is implemented as follows:
    * we perform small number of restarts from random locations which helps to
      solve problem of bad local extrema. Locations are only partially  random
      - we use input data to determine good  initial  guess,  but  we  include
      controlled amount of randomness.
    * we perform Levenberg-Marquardt fitting with very  tight  constraints  on
      parameters B and C - it allows us to find good  initial  guess  for  the
      second stage without risk of running into "flat spot".  Parameter  G  is
      fixed at G=1.
    * second  Levenberg-Marquardt  round  is   performed   without   excessive
      constraints on B and C, but with G still equal to 1.  Results  from  the
      previous round are used as initial guess.
    * third Levenberg-Marquardt round relaxes constraints on G  and  tries  two
      different models - one with B>0 and one with B<0.
    * after fitting is done, we compare results with best values found so far,
      rewrite "best solution" if needed, and move to next random location.
      
    Overall algorithm is very stable and is not prone to  bad  local  extrema.
    Furthermore, it automatically scales when input data have  very  large  or
    very small range.

    INPUT PARAMETERS:
        X       -   array[N], stores X-values.
                    MUST include only non-negative numbers  (but  may  include
                    zero values). Can be unsorted.
        Y       -   array[N], values to fit.
        N       -   number of points. If N is less than  length  of  X/Y, only
                    leading N elements are used.
        CnstrLeft-  optional equality constraint for model value at the   left
                    boundary (at X=0). Specify NAN (Not-a-Number)  if  you  do
                    not need constraint on the model value at X=0 (in C++  you
                    can pass alglib::fp_nan as parameter, in  C#  it  will  be
                    Double.NaN).
                    See  below,  section  "EQUALITY  CONSTRAINTS"   for   more
                    information about constraints.
        CnstrRight- optional equality constraint for model value at X=infinity.
                    Specify NAN (Not-a-Number) if you do not  need  constraint
                    on the model value (in C++  you can pass alglib::fp_nan as
                    parameter, in  C# it will  be Double.NaN).
                    See  below,  section  "EQUALITY  CONSTRAINTS"   for   more
                    information about constraints.
                    
    OUTPUT PARAMETERS:
        A,B,C,D,G-  parameters of 5PL model
        Rep     -   fitting report. This structure has many fields,  but  ONLY
                    ONES LISTED BELOW ARE SET:
                    * Rep.IterationsCount - number of iterations performed
                    * Rep.RMSError - root-mean-square error
                    * Rep.AvgError - average absolute error
                    * Rep.AvgRelError - average relative error (calculated for
                      non-zero Y-values)
                    * Rep.MaxError - maximum absolute error
                    * Rep.R2 - coefficient of determination,  R-squared.  This
                      coefficient   is  calculated  as  R2=1-RSS/TSS  (in case
                      of nonlinear  regression  there  are  multiple  ways  to
                      define R2, each of them giving different results).

    NOTE: for better stability B  parameter is restricted by [+-1/1000,+-1000]
          range, and G is restricted by [1/10,10] range. It prevents algorithm
          from making trial steps deep into the area of bad parameters.

    NOTE: after  you  obtained  coefficients,  you  can  evaluate  model  with
          LogisticCalc5() function.

    NOTE: if you need better control over fitting process than provided by this
          function, you may use LogisticFit45X().
                    
    NOTE: step is automatically scaled according to scale of parameters  being
          fitted before we compare its length with EpsX. Thus,  this  function
          can be used to fit data with very small or very large values without
          changing EpsX.

    EQUALITY CONSTRAINTS ON PARAMETERS

    5PL solver supports equality constraints on model  values  at   the   left
    boundary (X=0) and right  boundary  (X=infinity).  These  constraints  are
    completely optional and you can specify both of them, only  one  -  or  no
    constraints at all.

    Parameter  CnstrLeft  contains  left  constraint (or NAN for unconstrained
    fitting), and CnstrRight contains right  one.

    Unlike 4PL one, 5PL model is NOT symmetric with respect to  change in sign
    of B. Thus, negative B's are possible, and left constraint  may  constrain
    parameter A (for positive B's)  -  or  parameter  D  (for  negative  B's).
    Similarly changes meaning of right constraint.

    You do not have to decide what parameter to  constrain  -  algorithm  will
    automatically determine correct parameters as fitting progresses. However,
    question highlighted above is important when you interpret fitting results.
        

      -- ALGLIB PROJECT --
         Copyright 14.02.2014 by Bochkanov Sergey
    *************************************************************************/
    public static void logisticfit5ec(double[] x,
        double[] y,
        int n,
        double cnstrleft,
        double cnstrright,
        ref double a,
        ref double b,
        ref double c,
        ref double d,
        ref double g,
        lsfitreport rep,
        xparams _params)
    {
        x = (double[])x.Clone();
        y = (double[])y.Clone();
        a = 0;
        b = 0;
        c = 0;
        d = 0;
        g = 0;

        logisticfit45x(x, y, n, cnstrleft, cnstrright, false, 0.0, 0.0, 0, ref a, ref b, ref c, ref d, ref g, rep, _params);
    }


    /*************************************************************************
    This is "expert" 4PL/5PL fitting function, which can be used if  you  need
    better control over fitting process than provided  by  LogisticFit4()  or
    LogisticFit5().

    This function fits model of the form

        F(x|A,B,C,D)   = D+(A-D)/(1+Power(x/C,B))           (4PL model)

    or

        F(x|A,B,C,D,G) = D+(A-D)/Power(1+Power(x/C,B),G)    (5PL model)
        
    Here:
        * A, D - unconstrained
        * B>=0 for 4PL, unconstrained for 5PL
        * C>0
        * G>0 (if present)

    INPUT PARAMETERS:
        X       -   array[N], stores X-values.
                    MUST include only non-negative numbers  (but  may  include
                    zero values). Can be unsorted.
        Y       -   array[N], values to fit.
        N       -   number of points. If N is less than  length  of  X/Y, only
                    leading N elements are used.
        CnstrLeft-  optional equality constraint for model value at the   left
                    boundary (at X=0). Specify NAN (Not-a-Number)  if  you  do
                    not need constraint on the model value at X=0 (in C++  you
                    can pass alglib::fp_nan as parameter, in  C#  it  will  be
                    Double.NaN).
                    See  below,  section  "EQUALITY  CONSTRAINTS"   for   more
                    information about constraints.
        CnstrRight- optional equality constraint for model value at X=infinity.
                    Specify NAN (Not-a-Number) if you do not  need  constraint
                    on the model value (in C++  you can pass alglib::fp_nan as
                    parameter, in  C# it will  be Double.NaN).
                    See  below,  section  "EQUALITY  CONSTRAINTS"   for   more
                    information about constraints.
        Is4PL   -   whether 4PL or 5PL models are fitted
        LambdaV -   regularization coefficient, LambdaV>=0.
                    Set it to zero unless you know what you are doing.
        EpsX    -   stopping condition (step size), EpsX>=0.
                    Zero value means that small step is automatically chosen.
                    See notes below for more information.
        RsCnt   -   number of repeated restarts from  random  points.  4PL/5PL
                    models are prone to problem of bad local extrema. Utilizing
                    multiple random restarts allows  us  to  improve algorithm
                    convergence.
                    RsCnt>=0.
                    Zero value means that function automatically choose  small
                    amount of restarts (recommended).
                    
    OUTPUT PARAMETERS:
        A, B, C, D- parameters of 4PL model
        G       -   parameter of 5PL model; for Is4PL=True, G=1 is returned.
        Rep     -   fitting report. This structure has many fields,  but  ONLY
                    ONES LISTED BELOW ARE SET:
                    * Rep.IterationsCount - number of iterations performed
                    * Rep.RMSError - root-mean-square error
                    * Rep.AvgError - average absolute error
                    * Rep.AvgRelError - average relative error (calculated for
                      non-zero Y-values)
                    * Rep.MaxError - maximum absolute error
                    * Rep.R2 - coefficient of determination,  R-squared.  This
                      coefficient   is  calculated  as  R2=1-RSS/TSS  (in case
                      of nonlinear  regression  there  are  multiple  ways  to
                      define R2, each of them giving different results).
                    
    NOTE: for better stability B  parameter is restricted by [+-1/1000,+-1000]
          range, and G is restricted by [1/10,10] range. It prevents algorithm
          from making trial steps deep into the area of bad parameters.

    NOTE: after  you  obtained  coefficients,  you  can  evaluate  model  with
          LogisticCalc5() function.

    NOTE: step is automatically scaled according to scale of parameters  being
          fitted before we compare its length with EpsX. Thus,  this  function
          can be used to fit data with very small or very large values without
          changing EpsX.

    EQUALITY CONSTRAINTS ON PARAMETERS

    4PL/5PL solver supports equality constraints on model values at  the  left
    boundary (X=0) and right  boundary  (X=infinity).  These  constraints  are
    completely optional and you can specify both of them, only  one  -  or  no
    constraints at all.

    Parameter  CnstrLeft  contains  left  constraint (or NAN for unconstrained
    fitting), and CnstrRight contains right  one.  For  4PL,  left  constraint
    ALWAYS corresponds to parameter A, and right one is ALWAYS  constraint  on
    D. That's because 4PL model is normalized in such way that B>=0.

    For 5PL model things are different. Unlike  4PL  one,  5PL  model  is  NOT
    symmetric with respect to  change  in  sign  of  B. Thus, negative B's are
    possible, and left constraint may constrain parameter A (for positive B's)
    - or parameter D (for negative B's). Similarly changes  meaning  of  right
    constraint.

    You do not have to decide what parameter to  constrain  -  algorithm  will
    automatically determine correct parameters as fitting progresses. However,
    question highlighted above is important when you interpret fitting results.
        

      -- ALGLIB PROJECT --
         Copyright 14.02.2014 by Bochkanov Sergey
    *************************************************************************/
    public static void logisticfit45x(double[] x,
        double[] y,
        int n,
        double cnstrleft,
        double cnstrright,
        bool is4pl,
        double lambdav,
        double epsx,
        int rscnt,
        ref double a,
        ref double b,
        ref double c,
        ref double d,
        ref double g,
        lsfitreport rep,
        xparams _params)
    {
        int i = 0;
        int outerit = 0;
        int nz = 0;
        double v = 0;
        double[] p0 = new double[0];
        double[] p1 = new double[0];
        double[] p2 = new double[0];
        double[] bndl = new double[0];
        double[] bndu = new double[0];
        double[] s = new double[0];
        double[] bndl1 = new double[0];
        double[] bndu1 = new double[0];
        double[] bndl2 = new double[0];
        double[] bndu2 = new double[0];
        double[,] z = new double[0, 0];
        hqrnd.hqrndstate rs = new hqrnd.hqrndstate();
        minlm.minlmstate state = new minlm.minlmstate();
        minlm.minlmreport replm = new minlm.minlmreport();
        int maxits = 0;
        double fbest = 0;
        double flast = 0;
        double scalex = 0;
        double scaley = 0;
        double[] bufx = new double[0];
        double[] bufy = new double[0];
        double fposb = 0;
        double fnegb = 0;

        x = (double[])x.Clone();
        y = (double[])y.Clone();
        a = 0;
        b = 0;
        c = 0;
        d = 0;
        g = 0;

        ap.assert(math.isfinite(epsx), "LogisticFitX: EpsX is infinite/NAN");
        ap.assert(math.isfinite(lambdav), "LogisticFitX: LambdaV is infinite/NAN");
        ap.assert(math.isfinite(cnstrleft) || Double.IsNaN(cnstrleft), "LogisticFitX: CnstrLeft is NOT finite or NAN");
        ap.assert(math.isfinite(cnstrright) || Double.IsNaN(cnstrright), "LogisticFitX: CnstrRight is NOT finite or NAN");
        ap.assert((double)(lambdav) >= (double)(0), "LogisticFitX: negative LambdaV");
        ap.assert(n > 0, "LogisticFitX: N<=0");
        ap.assert(rscnt >= 0, "LogisticFitX: RsCnt<0");
        ap.assert((double)(epsx) >= (double)(0), "LogisticFitX: EpsX<0");
        ap.assert(ap.len(x) >= n, "LogisticFitX: Length(X)<N");
        ap.assert(ap.len(y) >= n, "LogisticFitX: Length(Y)<N");
        ap.assert(apserv.isfinitevector(x, n, _params), "LogisticFitX: X contains infinite/NAN values");
        ap.assert(apserv.isfinitevector(y, n, _params), "LogisticFitX: X contains infinite/NAN values");
        hqrnd.hqrndseed(2211, 1033044, rs, _params);
        clearreport(rep, _params);
        if ((double)(epsx) == (double)(0))
        {
            epsx = 1.0E-10;
        }
        if (rscnt == 0)
        {
            rscnt = 4;
        }
        maxits = 1000;

        //
        // Sort points by X.
        // Determine number of zero and non-zero values.
        //
        tsort.tagsortfastr(ref x, ref y, ref bufx, ref bufy, n, _params);
        ap.assert((double)(x[0]) >= (double)(0), "LogisticFitX: some X[] are negative");
        nz = n;
        for (i = 0; i <= n - 1; i++)
        {
            if ((double)(x[i]) > (double)(0))
            {
                nz = i;
                break;
            }
        }

        //
        // For NZ=N (all X[] are zero) special code is used.
        // For NZ<N we use general-purpose code.
        //
        rep.iterationscount = 0;
        if (nz == n)
        {

            //
            // NZ=N, degenerate problem.
            // No need to run optimizer.
            //
            v = 0.0;
            for (i = 0; i <= n - 1; i++)
            {
                v = v + y[i];
            }
            v = v / n;
            if (math.isfinite(cnstrleft))
            {
                a = cnstrleft;
            }
            else
            {
                a = v;
            }
            b = 1;
            c = 1;
            if (math.isfinite(cnstrright))
            {
                d = cnstrright;
            }
            else
            {
                d = a;
            }
            g = 1;
            logisticfit45errors(x, y, n, a, b, c, d, g, rep, _params);
            return;
        }

        //
        // Non-degenerate problem.
        // Determine scale of data.
        //
        scalex = x[nz + (n - nz) / 2];
        ap.assert((double)(scalex) > (double)(0), "LogisticFitX: internal error");
        v = 0.0;
        for (i = 0; i <= n - 1; i++)
        {
            v = v + y[i];
        }
        v = v / n;
        scaley = 0.0;
        for (i = 0; i <= n - 1; i++)
        {
            scaley = scaley + math.sqr(y[i] - v);
        }
        scaley = Math.Sqrt(scaley / n);
        if ((double)(scaley) == (double)(0))
        {
            scaley = 1.0;
        }
        s = new double[5];
        s[0] = scaley;
        s[1] = 0.1;
        s[2] = scalex;
        s[3] = scaley;
        s[4] = 0.1;
        p0 = new double[5];
        p0[0] = 0;
        p0[1] = 0;
        p0[2] = 0;
        p0[3] = 0;
        p0[4] = 0;
        bndl = new double[5];
        bndu = new double[5];
        bndl1 = new double[5];
        bndu1 = new double[5];
        bndl2 = new double[5];
        bndu2 = new double[5];
        minlm.minlmcreatevj(5, n + 5, p0, state, _params);
        minlm.minlmsetscale(state, s, _params);
        minlm.minlmsetcond(state, epsx, maxits, _params);
        minlm.minlmsetxrep(state, true, _params);
        p1 = new double[5];
        p2 = new double[5];

        //
        // Is it 4PL problem?
        //
        if (is4pl)
        {

            //
            // Run outer iterations
            //
            a = 0;
            b = 1;
            c = 1;
            d = 1;
            g = 1;
            fbest = math.maxrealnumber;
            for (outerit = 0; outerit <= rscnt - 1; outerit++)
            {

                //
                // Prepare initial point; use B>0
                //
                if (math.isfinite(cnstrleft))
                {
                    p1[0] = cnstrleft;
                }
                else
                {
                    p1[0] = y[0] + 0.15 * scaley * (hqrnd.hqrnduniformr(rs, _params) - 0.5);
                }
                p1[1] = 0.5 + hqrnd.hqrnduniformr(rs, _params);
                p1[2] = x[nz + hqrnd.hqrnduniformi(rs, n - nz, _params)];
                if (math.isfinite(cnstrright))
                {
                    p1[3] = cnstrright;
                }
                else
                {
                    p1[3] = y[n - 1] + 0.25 * scaley * (hqrnd.hqrnduniformr(rs, _params) - 0.5);
                }
                p1[4] = 1.0;

                //
                // Run optimization with tight constraints and increased regularization
                //
                if (math.isfinite(cnstrleft))
                {
                    bndl[0] = cnstrleft;
                    bndu[0] = cnstrleft;
                }
                else
                {
                    bndl[0] = Double.NegativeInfinity;
                    bndu[0] = Double.PositiveInfinity;
                }
                bndl[1] = 0.5;
                bndu[1] = 2.0;
                bndl[2] = 0.5 * scalex;
                bndu[2] = 2.0 * scalex;
                if (math.isfinite(cnstrright))
                {
                    bndl[3] = cnstrright;
                    bndu[3] = cnstrright;
                }
                else
                {
                    bndl[3] = Double.NegativeInfinity;
                    bndu[3] = Double.PositiveInfinity;
                }
                bndl[4] = 1.0;
                bndu[4] = 1.0;
                minlm.minlmsetbc(state, bndl, bndu, _params);
                logisticfitinternal(x, y, n, is4pl, 100 * lambdav, state, replm, ref p1, ref flast, _params);
                rep.iterationscount = rep.iterationscount + replm.iterationscount;

                //
                // Relax constraints, run optimization one more time
                //
                bndl[1] = 0.1;
                bndu[1] = 10.0;
                bndl[2] = math.machineepsilon * scalex;
                bndu[2] = scalex / math.machineepsilon;
                minlm.minlmsetbc(state, bndl, bndu, _params);
                logisticfitinternal(x, y, n, is4pl, lambdav, state, replm, ref p1, ref flast, _params);
                rep.iterationscount = rep.iterationscount + replm.iterationscount;

                //
                // Relax constraints more, run optimization one more time
                //
                bndl[1] = 0.01;
                bndu[1] = 100.0;
                minlm.minlmsetbc(state, bndl, bndu, _params);
                logisticfitinternal(x, y, n, is4pl, lambdav, state, replm, ref p1, ref flast, _params);
                rep.iterationscount = rep.iterationscount + replm.iterationscount;

                //
                // Relax constraints ever more, run optimization one more time
                //
                bndl[1] = 0.001;
                bndu[1] = 1000.0;
                minlm.minlmsetbc(state, bndl, bndu, _params);
                logisticfitinternal(x, y, n, is4pl, lambdav, state, replm, ref p1, ref flast, _params);
                rep.iterationscount = rep.iterationscount + replm.iterationscount;

                //
                // Compare results with best value found so far.
                //
                if ((double)(flast) < (double)(fbest))
                {
                    a = p1[0];
                    b = p1[1];
                    c = p1[2];
                    d = p1[3];
                    g = p1[4];
                    fbest = flast;
                }
            }
            logisticfit45errors(x, y, n, a, b, c, d, g, rep, _params);
            return;
        }

        //
        // Well.... we have 5PL fit, and we have to test two separate branches:
        // B>0 and B<0, because of asymmetry in the curve. First, we run optimization
        // with tight constraints two times, in order to determine better sign for B.
        //
        // Run outer iterations
        //
        a = 0;
        b = 1;
        c = 1;
        d = 1;
        g = 1;
        fbest = math.maxrealnumber;
        for (outerit = 0; outerit <= rscnt - 1; outerit++)
        {

            //
            // First, we try positive B.
            //
            p1[0] = y[0] + 0.15 * scaley * (hqrnd.hqrnduniformr(rs, _params) - 0.5);
            p1[1] = 0.5 + hqrnd.hqrnduniformr(rs, _params);
            p1[2] = x[nz + hqrnd.hqrnduniformi(rs, n - nz, _params)];
            p1[3] = y[n - 1] + 0.25 * scaley * (hqrnd.hqrnduniformr(rs, _params) - 0.5);
            p1[4] = 1.0;
            bndl1[0] = Double.NegativeInfinity;
            bndu1[0] = Double.PositiveInfinity;
            bndl1[1] = 0.5;
            bndu1[1] = 2.0;
            bndl1[2] = 0.5 * scalex;
            bndu1[2] = 2.0 * scalex;
            bndl1[3] = Double.NegativeInfinity;
            bndu1[3] = Double.PositiveInfinity;
            bndl1[4] = 0.5;
            bndu1[4] = 2.0;
            if (math.isfinite(cnstrleft))
            {
                p1[0] = cnstrleft;
                bndl1[0] = cnstrleft;
                bndu1[0] = cnstrleft;
            }
            if (math.isfinite(cnstrright))
            {
                p1[3] = cnstrright;
                bndl1[3] = cnstrright;
                bndu1[3] = cnstrright;
            }
            minlm.minlmsetbc(state, bndl1, bndu1, _params);
            logisticfitinternal(x, y, n, is4pl, 100 * lambdav, state, replm, ref p1, ref fposb, _params);
            rep.iterationscount = rep.iterationscount + replm.iterationscount;

            //
            // Second attempt - with negative B (constraints are still tight).
            //
            p2[0] = y[n - 1] + 0.15 * scaley * (hqrnd.hqrnduniformr(rs, _params) - 0.5);
            p2[1] = -(0.5 + hqrnd.hqrnduniformr(rs, _params));
            p2[2] = x[nz + hqrnd.hqrnduniformi(rs, n - nz, _params)];
            p2[3] = y[0] + 0.25 * scaley * (hqrnd.hqrnduniformr(rs, _params) - 0.5);
            p2[4] = 1.0;
            bndl2[0] = Double.NegativeInfinity;
            bndu2[0] = Double.PositiveInfinity;
            bndl2[1] = -2.0;
            bndu2[1] = -0.5;
            bndl2[2] = 0.5 * scalex;
            bndu2[2] = 2.0 * scalex;
            bndl2[3] = Double.NegativeInfinity;
            bndu2[3] = Double.PositiveInfinity;
            bndl2[4] = 0.5;
            bndu2[4] = 2.0;
            if (math.isfinite(cnstrleft))
            {
                p2[3] = cnstrleft;
                bndl2[3] = cnstrleft;
                bndu2[3] = cnstrleft;
            }
            if (math.isfinite(cnstrright))
            {
                p2[0] = cnstrright;
                bndl2[0] = cnstrright;
                bndu2[0] = cnstrright;
            }
            minlm.minlmsetbc(state, bndl2, bndu2, _params);
            logisticfitinternal(x, y, n, is4pl, 100 * lambdav, state, replm, ref p2, ref fnegb, _params);
            rep.iterationscount = rep.iterationscount + replm.iterationscount;

            //
            // Select best version of B sign
            //
            if ((double)(fposb) < (double)(fnegb))
            {

                //
                // Prepare relaxed constraints assuming that B is positive
                //
                bndl1[1] = 0.1;
                bndu1[1] = 10.0;
                bndl1[2] = math.machineepsilon * scalex;
                bndu1[2] = scalex / math.machineepsilon;
                bndl1[4] = 0.1;
                bndu1[4] = 10.0;
                minlm.minlmsetbc(state, bndl1, bndu1, _params);
                logisticfitinternal(x, y, n, is4pl, lambdav, state, replm, ref p1, ref flast, _params);
                rep.iterationscount = rep.iterationscount + replm.iterationscount;

                //
                // Prepare stronger relaxation of constraints
                //
                bndl1[1] = 0.01;
                bndu1[1] = 100.0;
                minlm.minlmsetbc(state, bndl1, bndu1, _params);
                logisticfitinternal(x, y, n, is4pl, lambdav, state, replm, ref p1, ref flast, _params);
                rep.iterationscount = rep.iterationscount + replm.iterationscount;

                //
                // Prepare stronger relaxation of constraints
                //
                bndl1[1] = 0.001;
                bndu1[1] = 1000.0;
                minlm.minlmsetbc(state, bndl1, bndu1, _params);
                logisticfitinternal(x, y, n, is4pl, lambdav, state, replm, ref p1, ref flast, _params);
                rep.iterationscount = rep.iterationscount + replm.iterationscount;

                //
                // Compare results with best value found so far.
                //
                if ((double)(flast) < (double)(fbest))
                {
                    a = p1[0];
                    b = p1[1];
                    c = p1[2];
                    d = p1[3];
                    g = p1[4];
                    fbest = flast;
                }
            }
            else
            {

                //
                // Prepare relaxed constraints assuming that B is negative
                //
                bndl2[1] = -10.0;
                bndu2[1] = -0.1;
                bndl2[2] = math.machineepsilon * scalex;
                bndu2[2] = scalex / math.machineepsilon;
                bndl2[4] = 0.1;
                bndu2[4] = 10.0;
                minlm.minlmsetbc(state, bndl2, bndu2, _params);
                logisticfitinternal(x, y, n, is4pl, lambdav, state, replm, ref p2, ref flast, _params);
                rep.iterationscount = rep.iterationscount + replm.iterationscount;

                //
                // Prepare stronger relaxation
                //
                bndl2[1] = -100.0;
                bndu2[1] = -0.01;
                minlm.minlmsetbc(state, bndl2, bndu2, _params);
                logisticfitinternal(x, y, n, is4pl, lambdav, state, replm, ref p2, ref flast, _params);
                rep.iterationscount = rep.iterationscount + replm.iterationscount;

                //
                // Prepare stronger relaxation
                //
                bndl2[1] = -1000.0;
                bndu2[1] = -0.001;
                minlm.minlmsetbc(state, bndl2, bndu2, _params);
                logisticfitinternal(x, y, n, is4pl, lambdav, state, replm, ref p2, ref flast, _params);
                rep.iterationscount = rep.iterationscount + replm.iterationscount;

                //
                // Compare results with best value found so far.
                //
                if ((double)(flast) < (double)(fbest))
                {
                    a = p2[0];
                    b = p2[1];
                    c = p2[2];
                    d = p2[3];
                    g = p2[4];
                    fbest = flast;
                }
            }
        }
        logisticfit45errors(x, y, n, a, b, c, d, g, rep, _params);
    }


    /*************************************************************************
    Weghted rational least  squares  fitting  using  Floater-Hormann  rational
    functions  with  optimal  D  chosen  from  [0,9],  with  constraints   and
    individual weights.

    Equidistant  grid  with M node on [min(x),max(x)]  is  used to build basis
    functions. Different values of D are tried, optimal D (least WEIGHTED root
    mean square error) is chosen.  Task  is  linear,  so  linear least squares
    solver  is  used.  Complexity  of  this  computational  scheme is O(N*M^2)
    (mostly dominated by the least squares solver).

    SEE ALSO
    * BarycentricFitFloaterHormann(), "lightweight" fitting without invididual
      weights and constraints.

    INPUT PARAMETERS:
        X   -   points, array[0..N-1].
        Y   -   function values, array[0..N-1].
        W   -   weights, array[0..N-1]
                Each summand in square  sum  of  approximation deviations from
                given  values  is  multiplied  by  the square of corresponding
                weight. Fill it by 1's if you don't  want  to  solve  weighted
                task.
        N   -   number of points, N>0.
        XC  -   points where function values/derivatives are constrained,
                array[0..K-1].
        YC  -   values of constraints, array[0..K-1]
        DC  -   array[0..K-1], types of constraints:
                * DC[i]=0   means that S(XC[i])=YC[i]
                * DC[i]=1   means that S'(XC[i])=YC[i]
                SEE BELOW FOR IMPORTANT INFORMATION ON CONSTRAINTS
        K   -   number of constraints, 0<=K<M.
                K=0 means no constraints (XC/YC/DC are not used in such cases)
        M   -   number of basis functions ( = number_of_nodes), M>=2.

    OUTPUT PARAMETERS:
        B   -   barycentric interpolant. Undefined for rep.terminationtype<0.
        Rep -   fitting report. The following fields are set:
                * Rep.TerminationType is a completion code:
                  * set to  1 on success
                  * set to -3 on failure due to  problematic  constraints:
                    either too many  constraints,  degenerate  constraints
                    or inconsistent constraints were passed
                * DBest         best value of the D parameter
                * RMSError      rms error on the (X,Y).
                * AvgError      average error on the (X,Y).
                * AvgRelError   average relative error on the non-zero Y
                * MaxError      maximum error
                                NON-WEIGHTED ERRORS ARE CALCULATED

    IMPORTANT:
        this subroutine doesn't calculate task's condition number for K<>0.

    SETTING CONSTRAINTS - DANGERS AND OPPORTUNITIES:

    Setting constraints can lead  to undesired  results,  like ill-conditioned
    behavior, or inconsistency being detected. From the other side,  it allows
    us to improve quality of the fit. Here we summarize  our  experience  with
    constrained barycentric interpolants:
    * excessive  constraints  can  be  inconsistent.   Floater-Hormann   basis
      functions aren't as flexible as splines (although they are very smooth).
    * the more evenly constraints are spread across [min(x),max(x)],  the more
      chances that they will be consistent
    * the  greater  is  M (given  fixed  constraints),  the  more chances that
      constraints will be consistent
    * in the general case, consistency of constraints IS NOT GUARANTEED.
    * in the several special cases, however, we CAN guarantee consistency.
    * one of this cases is constraints on the function  VALUES at the interval
      boundaries. Note that consustency of the  constraints  on  the  function
      DERIVATIVES is NOT guaranteed (you can use in such cases  cubic  splines
      which are more flexible).
    * another  special  case  is ONE constraint on the function value (OR, but
      not AND, derivative) anywhere in the interval

    Our final recommendation is to use constraints  WHEN  AND  ONLY  WHEN  you
    can't solve your task without them. Anything beyond  special  cases  given
    above is not guaranteed and may result in inconsistency.

      ! FREE EDITION OF ALGLIB:
      ! 
      ! Free Edition of ALGLIB supports following important features for  this
      ! function:
      ! * C++ version: x64 SIMD support using C++ intrinsics
      ! * C#  version: x64 SIMD support using NET5/NetCore hardware intrinsics
      !
      ! We  recommend  you  to  read  'Compiling ALGLIB' section of the ALGLIB
      ! Reference Manual in order  to  find  out  how to activate SIMD support
      ! in ALGLIB.

      ! COMMERCIAL EDITION OF ALGLIB:
      ! 
      ! Commercial Edition of ALGLIB includes following important improvements
      ! of this function:
      ! * high-performance native backend with same C# interface (C# version)
      ! * multithreading support (C++ and C# versions)
      ! * hardware vendor (Intel) implementations of linear algebra primitives
      !   (C++ and C# versions, x86/x64 platform)
      ! 
      ! We recommend you to read 'Working with commercial version' section  of
      ! ALGLIB Reference Manual in order to find out how to  use  performance-
      ! related features provided by commercial edition of ALGLIB.

      -- ALGLIB PROJECT --
         Copyright 18.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void barycentricfitfloaterhormannwc(double[] x,
        double[] y,
        double[] w,
        int n,
        double[] xc,
        double[] yc,
        int[] dc,
        int k,
        int m,
        ratint.barycentricinterpolant b,
        barycentricfitreport rep,
        xparams _params)
    {
        int d = 0;
        int i = 0;
        double wrmscur = 0;
        double wrmsbest = 0;
        ratint.barycentricinterpolant locb = new ratint.barycentricinterpolant();
        barycentricfitreport locrep = new barycentricfitreport();
        int locinfo = 0;

        ap.assert(n > 0, "BarycentricFitFloaterHormannWC: N<=0!");
        ap.assert(m > 0, "BarycentricFitFloaterHormannWC: M<=0!");
        ap.assert(k >= 0, "BarycentricFitFloaterHormannWC: K<0!");
        ap.assert(k < m, "BarycentricFitFloaterHormannWC: K>=M!");
        ap.assert(ap.len(x) >= n, "BarycentricFitFloaterHormannWC: Length(X)<N!");
        ap.assert(ap.len(y) >= n, "BarycentricFitFloaterHormannWC: Length(Y)<N!");
        ap.assert(ap.len(w) >= n, "BarycentricFitFloaterHormannWC: Length(W)<N!");
        ap.assert(ap.len(xc) >= k, "BarycentricFitFloaterHormannWC: Length(XC)<K!");
        ap.assert(ap.len(yc) >= k, "BarycentricFitFloaterHormannWC: Length(YC)<K!");
        ap.assert(ap.len(dc) >= k, "BarycentricFitFloaterHormannWC: Length(DC)<K!");
        ap.assert(apserv.isfinitevector(x, n, _params), "BarycentricFitFloaterHormannWC: X contains infinite or NaN values!");
        ap.assert(apserv.isfinitevector(y, n, _params), "BarycentricFitFloaterHormannWC: Y contains infinite or NaN values!");
        ap.assert(apserv.isfinitevector(w, n, _params), "BarycentricFitFloaterHormannWC: X contains infinite or NaN values!");
        ap.assert(apserv.isfinitevector(xc, k, _params), "BarycentricFitFloaterHormannWC: XC contains infinite or NaN values!");
        ap.assert(apserv.isfinitevector(yc, k, _params), "BarycentricFitFloaterHormannWC: YC contains infinite or NaN values!");
        for (i = 0; i <= k - 1; i++)
        {
            ap.assert(dc[i] == 0 || dc[i] == 1, "BarycentricFitFloaterHormannWC: one of DC[] is not 0 or 1!");
        }

        //
        // Find optimal D
        //
        // Info is -3 by default (degenerate constraints).
        // If LocInfo will always be equal to -3, Info will remain equal to -3.
        // If at least once LocInfo will be -4, Info will be -4.
        //
        wrmsbest = math.maxrealnumber;
        rep.dbest = -1;
        rep.terminationtype = -3;
        for (d = 0; d <= Math.Min(9, n - 1); d++)
        {
            barycentricfitwcfixedd(x, y, w, n, xc, yc, dc, k, m, d, ref locinfo, locb, locrep, _params);
            ap.assert((locinfo == -4 || locinfo == -3) || locinfo > 0, "BarycentricFitFloaterHormannWC: unexpected result from BarycentricFitWCFixedD!");
            if (locinfo > 0)
            {

                //
                // Calculate weghted RMS
                //
                wrmscur = 0;
                for (i = 0; i <= n - 1; i++)
                {
                    wrmscur = wrmscur + math.sqr(w[i] * (y[i] - ratint.barycentriccalc(locb, x[i], _params)));
                }
                wrmscur = Math.Sqrt(wrmscur / n);
                if ((double)(wrmscur) < (double)(wrmsbest) || rep.dbest < 0)
                {
                    ratint.barycentriccopy(locb, b, _params);
                    rep.dbest = d;
                    rep.terminationtype = 1;
                    rep.rmserror = locrep.rmserror;
                    rep.avgerror = locrep.avgerror;
                    rep.avgrelerror = locrep.avgrelerror;
                    rep.maxerror = locrep.maxerror;
                    rep.taskrcond = locrep.taskrcond;
                    wrmsbest = wrmscur;
                }
            }
            else
            {
                if (locinfo != -3 && rep.terminationtype < 0)
                {
                    rep.terminationtype = locinfo;
                }
            }
        }
    }


    /*************************************************************************
    Rational least squares fitting using  Floater-Hormann  rational  functions
    with optimal D chosen from [0,9].

    Equidistant  grid  with M node on [min(x),max(x)]  is  used to build basis
    functions. Different values of D are tried, optimal  D  (least  root  mean
    square error) is chosen.  Task  is  linear, so linear least squares solver
    is used. Complexity  of  this  computational  scheme is  O(N*M^2)  (mostly
    dominated by the least squares solver).

    INPUT PARAMETERS:
        X   -   points, array[0..N-1].
        Y   -   function values, array[0..N-1].
        N   -   number of points, N>0.
        M   -   number of basis functions ( = number_of_nodes), M>=2.

    OUTPUT PARAMETERS:
        B   -   barycentric interpolant.
        Rep -   fitting report. The following fields are set:
                * Rep.TerminationType is a completion code, always set to 1
                * DBest         best value of the D parameter
                * RMSError      rms error on the (X,Y).
                * AvgError      average error on the (X,Y).
                * AvgRelError   average relative error on the non-zero Y
                * MaxError      maximum error
                                NON-WEIGHTED ERRORS ARE CALCULATED

      ! FREE EDITION OF ALGLIB:
      ! 
      ! Free Edition of ALGLIB supports following important features for  this
      ! function:
      ! * C++ version: x64 SIMD support using C++ intrinsics
      ! * C#  version: x64 SIMD support using NET5/NetCore hardware intrinsics
      !
      ! We  recommend  you  to  read  'Compiling ALGLIB' section of the ALGLIB
      ! Reference Manual in order  to  find  out  how to activate SIMD support
      ! in ALGLIB.

      ! COMMERCIAL EDITION OF ALGLIB:
      ! 
      ! Commercial Edition of ALGLIB includes following important improvements
      ! of this function:
      ! * high-performance native backend with same C# interface (C# version)
      ! * multithreading support (C++ and C# versions)
      ! * hardware vendor (Intel) implementations of linear algebra primitives
      !   (C++ and C# versions, x86/x64 platform)
      ! 
      ! We recommend you to read 'Working with commercial version' section  of
      ! ALGLIB Reference Manual in order to find out how to  use  performance-
      ! related features provided by commercial edition of ALGLIB.

      -- ALGLIB PROJECT --
         Copyright 18.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void barycentricfitfloaterhormann(double[] x,
        double[] y,
        int n,
        int m,
        ratint.barycentricinterpolant b,
        barycentricfitreport rep,
        xparams _params)
    {
        double[] w = new double[0];
        double[] xc = new double[0];
        double[] yc = new double[0];
        int[] dc = new int[0];
        int i = 0;

        ap.assert(n > 0, "BarycentricFitFloaterHormann: N<=0!");
        ap.assert(m > 0, "BarycentricFitFloaterHormann: M<=0!");
        ap.assert(ap.len(x) >= n, "BarycentricFitFloaterHormann: Length(X)<N!");
        ap.assert(ap.len(y) >= n, "BarycentricFitFloaterHormann: Length(Y)<N!");
        ap.assert(apserv.isfinitevector(x, n, _params), "BarycentricFitFloaterHormann: X contains infinite or NaN values!");
        ap.assert(apserv.isfinitevector(y, n, _params), "BarycentricFitFloaterHormann: Y contains infinite or NaN values!");
        w = new double[n];
        for (i = 0; i <= n - 1; i++)
        {
            w[i] = 1;
        }
        barycentricfitfloaterhormannwc(x, y, w, n, xc, yc, dc, 0, m, b, rep, _params);
    }


    /*************************************************************************
    Weighted fitting by cubic  spline,  with constraints on function values or
    derivatives.

    Equidistant grid with M-2 nodes on [min(x,xc),max(x,xc)] is  used to build
    basis functions. Basis functions are cubic splines with continuous  second
    derivatives  and  non-fixed first  derivatives  at  interval  ends.  Small
    regularizing term is used  when  solving  constrained  tasks  (to  improve
    stability).

    Task is linear, so linear least squares solver is used. Complexity of this
    computational scheme is O(N*M^2), mostly dominated by least squares solver

    IMPORTANT: ALGLIB has a much faster version  of  the  cubic spline fitting
               function - spline1dfit(). This function performs least  squares
               fit in O(max(M,N)) time/memory. However, it  does  not  support
               constraints.
                                    
    INPUT PARAMETERS:
        X   -   points, array[0..N-1].
        Y   -   function values, array[0..N-1].
        W   -   weights, array[0..N-1]
                Each summand in square  sum  of  approximation deviations from
                given  values  is  multiplied  by  the square of corresponding
                weight. Fill it by 1's if you don't  want  to  solve  weighted
                task.
        N   -   number of points (optional):
                * N>0
                * if given, only first N elements of X/Y/W are processed
                * if not given, automatically determined from X/Y/W sizes
        XC  -   points where spline values/derivatives are constrained,
                array[0..K-1].
        YC  -   values of constraints, array[0..K-1]
        DC  -   array[0..K-1], types of constraints:
                * DC[i]=0   means that S(XC[i])=YC[i]
                * DC[i]=1   means that S'(XC[i])=YC[i]
                SEE BELOW FOR IMPORTANT INFORMATION ON CONSTRAINTS
        K   -   number of constraints (optional):
                * 0<=K<M.
                * K=0 means no constraints (XC/YC/DC are not used)
                * if given, only first K elements of XC/YC/DC are used
                * if not given, automatically determined from XC/YC/DC
        M   -   number of basis functions ( = number_of_nodes+2), M>=4.

    OUTPUT PARAMETERS:
        S   -   spline interpolant.
        Rep     -   fitting report. The following fields are set:
                    * Rep.TerminationType is a completion code:
                      * set to  1 on success
                      * set to -3 on failure due to  problematic  constraints:
                        either too many  constraints,  degenerate  constraints
                        or inconsistent constraints were passed
                    * RMSError      rms error on the (X,Y).
                    * AvgError      average error on the (X,Y).
                    * AvgRelError   average relative error on the non-zero Y
                    * MaxError      maximum error
                                    NON-WEIGHTED ERRORS ARE CALCULATED

    IMPORTANT:
        this subroitine doesn't calculate task's condition number for K<>0.


    ORDER OF POINTS

    Subroutine automatically sorts points, so caller may pass unsorted array.

    SETTING CONSTRAINTS - DANGERS AND OPPORTUNITIES:

    Setting constraints can lead  to undesired  results,  like ill-conditioned
    behavior, or inconsistency being detected. From the other side,  it allows
    us to improve quality of the fit. Here we summarize  our  experience  with
    constrained regression splines:
    * excessive constraints can be inconsistent. Splines are  piecewise  cubic
      functions, and it is easy to create an example, where  large  number  of
      constraints  concentrated  in  small  area will result in inconsistency.
      Just because spline is not flexible enough to satisfy all of  them.  And
      same constraints spread across the  [min(x),max(x)]  will  be  perfectly
      consistent.
    * the more evenly constraints are spread across [min(x),max(x)],  the more
      chances that they will be consistent
    * the  greater  is  M (given  fixed  constraints),  the  more chances that
      constraints will be consistent
    * in the general case, consistency of constraints IS NOT GUARANTEED.
    * in the several special cases, however, we CAN guarantee consistency.
    * one of this cases is constraints  on  the  function  values  AND/OR  its
      derivatives at the interval boundaries.
    * another  special  case  is ONE constraint on the function value (OR, but
      not AND, derivative) anywhere in the interval

    Our final recommendation is to use constraints  WHEN  AND  ONLY  WHEN  you
    can't solve your task without them. Anything beyond  special  cases  given
    above is not guaranteed and may result in inconsistency.

      ! FREE EDITION OF ALGLIB:
      ! 
      ! Free Edition of ALGLIB supports following important features for  this
      ! function:
      ! * C++ version: x64 SIMD support using C++ intrinsics
      ! * C#  version: x64 SIMD support using NET5/NetCore hardware intrinsics
      !
      ! We  recommend  you  to  read  'Compiling ALGLIB' section of the ALGLIB
      ! Reference Manual in order  to  find  out  how to activate SIMD support
      ! in ALGLIB.

      ! COMMERCIAL EDITION OF ALGLIB:
      ! 
      ! Commercial Edition of ALGLIB includes following important improvements
      ! of this function:
      ! * high-performance native backend with same C# interface (C# version)
      ! * multithreading support (C++ and C# versions)
      ! * hardware vendor (Intel) implementations of linear algebra primitives
      !   (C++ and C# versions, x86/x64 platform)
      ! 
      ! We recommend you to read 'Working with commercial version' section  of
      ! ALGLIB Reference Manual in order to find out how to  use  performance-
      ! related features provided by commercial edition of ALGLIB.

      -- ALGLIB PROJECT --
         Copyright 18.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dfitcubicwc(double[] x,
        double[] y,
        double[] w,
        int n,
        double[] xc,
        double[] yc,
        int[] dc,
        int k,
        int m,
        spline1d.spline1dinterpolant s,
        spline1d.spline1dfitreport rep,
        xparams _params)
    {
        int i = 0;

        ap.assert(n >= 1, "Spline1DFitCubicWC: N<1!");
        ap.assert(m >= 4, "Spline1DFitCubicWC: M<4!");
        ap.assert(k >= 0, "Spline1DFitCubicWC: K<0!");
        ap.assert(k < m, "Spline1DFitCubicWC: K>=M!");
        ap.assert(ap.len(x) >= n, "Spline1DFitCubicWC: Length(X)<N!");
        ap.assert(ap.len(y) >= n, "Spline1DFitCubicWC: Length(Y)<N!");
        ap.assert(ap.len(w) >= n, "Spline1DFitCubicWC: Length(W)<N!");
        ap.assert(ap.len(xc) >= k, "Spline1DFitCubicWC: Length(XC)<K!");
        ap.assert(ap.len(yc) >= k, "Spline1DFitCubicWC: Length(YC)<K!");
        ap.assert(ap.len(dc) >= k, "Spline1DFitCubicWC: Length(DC)<K!");
        ap.assert(apserv.isfinitevector(x, n, _params), "Spline1DFitCubicWC: X contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(y, n, _params), "Spline1DFitCubicWC: Y contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(w, n, _params), "Spline1DFitCubicWC: Y contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(xc, k, _params), "Spline1DFitCubicWC: X contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(yc, k, _params), "Spline1DFitCubicWC: Y contains infinite or NAN values!");
        for (i = 0; i <= k - 1; i++)
        {
            ap.assert(dc[i] == 0 || dc[i] == 1, "Spline1DFitCubicWC: DC[i] is neither 0 or 1!");
        }
        spline1dfitinternal(0, x, y, w, n, xc, yc, dc, k, m, s, rep, _params);
    }


    /*************************************************************************
    Weighted  fitting  by Hermite spline,  with constraints on function values
    or first derivatives.

    Equidistant grid with M nodes on [min(x,xc),max(x,xc)] is  used  to  build
    basis functions. Basis functions are Hermite splines.  Small  regularizing
    term is used when solving constrained tasks (to improve stability).

    Task is linear, so linear least squares solver is used. Complexity of this
    computational scheme is O(N*M^2), mostly dominated by least squares solver

    IMPORTANT: ALGLIB has a much faster version  of  the  cubic spline fitting
               function - spline1dfit(). This function performs least  squares
               fit in O(max(M,N)) time/memory. However, it  does  not  support
               constraints.
                                    
    INPUT PARAMETERS:
        X   -   points, array[0..N-1].
        Y   -   function values, array[0..N-1].
        W   -   weights, array[0..N-1]
                Each summand in square  sum  of  approximation deviations from
                given  values  is  multiplied  by  the square of corresponding
                weight. Fill it by 1's if you don't  want  to  solve  weighted
                task.
        N   -   number of points (optional):
                * N>0
                * if given, only first N elements of X/Y/W are processed
                * if not given, automatically determined from X/Y/W sizes
        XC  -   points where spline values/derivatives are constrained,
                array[0..K-1].
        YC  -   values of constraints, array[0..K-1]
        DC  -   array[0..K-1], types of constraints:
                * DC[i]=0   means that S(XC[i])=YC[i]
                * DC[i]=1   means that S'(XC[i])=YC[i]
                SEE BELOW FOR IMPORTANT INFORMATION ON CONSTRAINTS
        K   -   number of constraints (optional):
                * 0<=K<M.
                * K=0 means no constraints (XC/YC/DC are not used)
                * if given, only first K elements of XC/YC/DC are used
                * if not given, automatically determined from XC/YC/DC
        M   -   number of basis functions (= 2 * number of nodes),
                M>=4,
                M IS EVEN!

    OUTPUT PARAMETERS:
        S   -   spline interpolant.
        Rep     -   fitting report. The following fields are set:
                    * Rep.TerminationType is a completion code:
                      * set to  1 on success
                      * set to -3 on failure due to  problematic  constraints:
                        either too many  constraints,  degenerate  constraints
                        or inconsistent constraints were passed
                      * RMSError      rms error on the (X,Y).
                    * AvgError      average error on the (X,Y).
                    * AvgRelError   average relative error on the non-zero Y
                    * MaxError      maximum error
                                    NON-WEIGHTED ERRORS ARE CALCULATED

    IMPORTANT:
        this subroitine doesn't calculate task's condition number for K<>0.

    IMPORTANT:
        this subroitine supports only even M's


    ORDER OF POINTS

    Subroutine automatically sorts points, so caller may pass unsorted array.

    SETTING CONSTRAINTS - DANGERS AND OPPORTUNITIES:

    Setting constraints can lead  to undesired  results,  like ill-conditioned
    behavior, or inconsistency being detected. From the other side,  it allows
    us to improve quality of the fit. Here we summarize  our  experience  with
    constrained regression splines:
    * excessive constraints can be inconsistent. Splines are  piecewise  cubic
      functions, and it is easy to create an example, where  large  number  of
      constraints  concentrated  in  small  area will result in inconsistency.
      Just because spline is not flexible enough to satisfy all of  them.  And
      same constraints spread across the  [min(x),max(x)]  will  be  perfectly
      consistent.
    * the more evenly constraints are spread across [min(x),max(x)],  the more
      chances that they will be consistent
    * the  greater  is  M (given  fixed  constraints),  the  more chances that
      constraints will be consistent
    * in the general case, consistency of constraints is NOT GUARANTEED.
    * in the several special cases, however, we can guarantee consistency.
    * one of this cases is  M>=4  and   constraints  on   the  function  value
      (AND/OR its derivative) at the interval boundaries.
    * another special case is M>=4  and  ONE  constraint on the function value
      (OR, BUT NOT AND, derivative) anywhere in [min(x),max(x)]

    Our final recommendation is to use constraints  WHEN  AND  ONLY  when  you
    can't solve your task without them. Anything beyond  special  cases  given
    above is not guaranteed and may result in inconsistency.

      ! FREE EDITION OF ALGLIB:
      ! 
      ! Free Edition of ALGLIB supports following important features for  this
      ! function:
      ! * C++ version: x64 SIMD support using C++ intrinsics
      ! * C#  version: x64 SIMD support using NET5/NetCore hardware intrinsics
      !
      ! We  recommend  you  to  read  'Compiling ALGLIB' section of the ALGLIB
      ! Reference Manual in order  to  find  out  how to activate SIMD support
      ! in ALGLIB.

      ! COMMERCIAL EDITION OF ALGLIB:
      ! 
      ! Commercial Edition of ALGLIB includes following important improvements
      ! of this function:
      ! * high-performance native backend with same C# interface (C# version)
      ! * multithreading support (C++ and C# versions)
      ! * hardware vendor (Intel) implementations of linear algebra primitives
      !   (C++ and C# versions, x86/x64 platform)
      ! 
      ! We recommend you to read 'Working with commercial version' section  of
      ! ALGLIB Reference Manual in order to find out how to  use  performance-
      ! related features provided by commercial edition of ALGLIB.

      -- ALGLIB PROJECT --
         Copyright 18.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dfithermitewc(double[] x,
        double[] y,
        double[] w,
        int n,
        double[] xc,
        double[] yc,
        int[] dc,
        int k,
        int m,
        spline1d.spline1dinterpolant s,
        spline1d.spline1dfitreport rep,
        xparams _params)
    {
        int i = 0;

        ap.assert(n >= 1, "Spline1DFitHermiteWC: N<1!");
        ap.assert(m >= 4, "Spline1DFitHermiteWC: M<4!");
        ap.assert(m % 2 == 0, "Spline1DFitHermiteWC: M is odd!");
        ap.assert(k >= 0, "Spline1DFitHermiteWC: K<0!");
        ap.assert(k < m, "Spline1DFitHermiteWC: K>=M!");
        ap.assert(ap.len(x) >= n, "Spline1DFitHermiteWC: Length(X)<N!");
        ap.assert(ap.len(y) >= n, "Spline1DFitHermiteWC: Length(Y)<N!");
        ap.assert(ap.len(w) >= n, "Spline1DFitHermiteWC: Length(W)<N!");
        ap.assert(ap.len(xc) >= k, "Spline1DFitHermiteWC: Length(XC)<K!");
        ap.assert(ap.len(yc) >= k, "Spline1DFitHermiteWC: Length(YC)<K!");
        ap.assert(ap.len(dc) >= k, "Spline1DFitHermiteWC: Length(DC)<K!");
        ap.assert(apserv.isfinitevector(x, n, _params), "Spline1DFitHermiteWC: X contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(y, n, _params), "Spline1DFitHermiteWC: Y contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(w, n, _params), "Spline1DFitHermiteWC: Y contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(xc, k, _params), "Spline1DFitHermiteWC: X contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(yc, k, _params), "Spline1DFitHermiteWC: Y contains infinite or NAN values!");
        for (i = 0; i <= k - 1; i++)
        {
            ap.assert(dc[i] == 0 || dc[i] == 1, "Spline1DFitHermiteWC: DC[i] is neither 0 or 1!");
        }
        spline1dfitinternal(1, x, y, w, n, xc, yc, dc, k, m, s, rep, _params);
    }


    /*************************************************************************
    Deprecated fitting function with O(N*M^2+M^3) running time. Superseded  by
    spline1dfit().

      -- ALGLIB PROJECT --
         Copyright 18.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dfitcubicdeprecated(double[] x,
        double[] y,
        int n,
        int m,
        spline1d.spline1dinterpolant s,
        spline1d.spline1dfitreport rep,
        xparams _params)
    {
        int i = 0;
        double[] w = new double[0];
        double[] xc = new double[0];
        double[] yc = new double[0];
        int[] dc = new int[0];

        ap.assert(n >= 1, "Spline1DFitCubic: N<1!");
        ap.assert(m >= 4, "Spline1DFitCubic: M<4!");
        ap.assert(ap.len(x) >= n, "Spline1DFitCubic: Length(X)<N!");
        ap.assert(ap.len(y) >= n, "Spline1DFitCubic: Length(Y)<N!");
        ap.assert(apserv.isfinitevector(x, n, _params), "Spline1DFitCubic: X contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(y, n, _params), "Spline1DFitCubic: Y contains infinite or NAN values!");
        w = new double[n];
        for (i = 0; i <= n - 1; i++)
        {
            w[i] = 1;
        }
        spline1dfitcubicwc(x, y, w, n, xc, yc, dc, 0, m, s, rep, _params);
    }


    /*************************************************************************
    Deprecated fitting function with O(N*M^2+M^3) running time. Superseded  by
    spline1dfit().

      -- ALGLIB PROJECT --
         Copyright 18.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void spline1dfithermitedeprecated(double[] x,
        double[] y,
        int n,
        int m,
        spline1d.spline1dinterpolant s,
        spline1d.spline1dfitreport rep,
        xparams _params)
    {
        int i = 0;
        double[] w = new double[0];
        double[] xc = new double[0];
        double[] yc = new double[0];
        int[] dc = new int[0];

        ap.assert(n >= 1, "Spline1DFitHermite: N<1!");
        ap.assert(m >= 4, "Spline1DFitHermite: M<4!");
        ap.assert(m % 2 == 0, "Spline1DFitHermite: M is odd!");
        ap.assert(ap.len(x) >= n, "Spline1DFitHermite: Length(X)<N!");
        ap.assert(ap.len(y) >= n, "Spline1DFitHermite: Length(Y)<N!");
        ap.assert(apserv.isfinitevector(x, n, _params), "Spline1DFitHermite: X contains infinite or NAN values!");
        ap.assert(apserv.isfinitevector(y, n, _params), "Spline1DFitHermite: Y contains infinite or NAN values!");
        w = new double[n];
        for (i = 0; i <= n - 1; i++)
        {
            w[i] = 1;
        }
        spline1dfithermitewc(x, y, w, n, xc, yc, dc, 0, m, s, rep, _params);
    }


    /*************************************************************************
    Weighted linear least squares fitting.

    QR decomposition is used to reduce task to MxM, then triangular solver  or
    SVD-based solver is used depending on condition number of the  system.  It
    allows to maximize speed and retain decent accuracy.

    IMPORTANT: if you want to perform  polynomial  fitting,  it  may  be  more
               convenient to use PolynomialFit() function. This function gives
               best  results  on  polynomial  problems  and  solves  numerical
               stability  issues  which  arise  when   you   fit   high-degree
               polynomials to your data.

    INPUT PARAMETERS:
        Y       -   array[0..N-1] Function values in  N  points.
        W       -   array[0..N-1]  Weights  corresponding to function  values.
                    Each summand in square  sum  of  approximation  deviations
                    from  given  values  is  multiplied  by  the   square   of
                    corresponding weight.
        FMatrix -   a table of basis functions values, array[0..N-1, 0..M-1].
                    FMatrix[I, J] - value of J-th basis function in I-th point.
        N       -   number of points used. N>=1.
        M       -   number of basis functions, M>=1.

    OUTPUT PARAMETERS:
        C       -   decomposition coefficients, array[0..M-1]
        Rep     -   fitting report. Following fields are set:
                    * Rep.TerminationType always set to 1 (success)
                    * Rep.TaskRCond     reciprocal of condition number
                    * R2                non-adjusted coefficient of determination
                                        (non-weighted)
                    * RMSError          rms error on the (X,Y).
                    * AvgError          average error on the (X,Y).
                    * AvgRelError       average relative error on the non-zero Y
                    * MaxError          maximum error
                                        NON-WEIGHTED ERRORS ARE CALCULATED
                    
    ERRORS IN PARAMETERS                
                    
    This  solver  also  calculates different kinds of errors in parameters and
    fills corresponding fields of report:
    * Rep.CovPar        covariance matrix for parameters, array[K,K].
    * Rep.ErrPar        errors in parameters, array[K],
                        errpar = sqrt(diag(CovPar))
    * Rep.ErrCurve      vector of fit errors - standard deviations of empirical
                        best-fit curve from "ideal" best-fit curve built  with
                        infinite number of samples, array[N].
                        errcurve = sqrt(diag(F*CovPar*F')),
                        where F is functions matrix.
    * Rep.Noise         vector of per-point estimates of noise, array[N]
                
    NOTE:       noise in the data is estimated as follows:
                * for fitting without user-supplied  weights  all  points  are
                  assumed to have same level of noise, which is estimated from
                  the data
                * for fitting with user-supplied weights we assume that  noise
                  level in I-th point is inversely proportional to Ith weight.
                  Coefficient of proportionality is estimated from the data.
                
    NOTE:       we apply small amount of regularization when we invert squared
                Jacobian and calculate covariance matrix. It  guarantees  that
                algorithm won't divide by zero  during  inversion,  but  skews
                error estimates a bit (fractional error is about 10^-9).
                
                However, we believe that this difference is insignificant  for
                all practical purposes except for the situation when you  want
                to compare ALGLIB results with "reference"  implementation  up
                to the last significant digit.
                
    NOTE:       covariance matrix is estimated using  correction  for  degrees
                of freedom (covariances are divided by N-M instead of dividing
                by N).

      ! FREE EDITION OF ALGLIB:
      ! 
      ! Free Edition of ALGLIB supports following important features for  this
      ! function:
      ! * C++ version: x64 SIMD support using C++ intrinsics
      ! * C#  version: x64 SIMD support using NET5/NetCore hardware intrinsics
      !
      ! We  recommend  you  to  read  'Compiling ALGLIB' section of the ALGLIB
      ! Reference Manual in order  to  find  out  how to activate SIMD support
      ! in ALGLIB.

      ! COMMERCIAL EDITION OF ALGLIB:
      ! 
      ! Commercial Edition of ALGLIB includes following important improvements
      ! of this function:
      ! * high-performance native backend with same C# interface (C# version)
      ! * multithreading support (C++ and C# versions)
      ! * hardware vendor (Intel) implementations of linear algebra primitives
      !   (C++ and C# versions, x86/x64 platform)
      ! 
      ! We recommend you to read 'Working with commercial version' section  of
      ! ALGLIB Reference Manual in order to find out how to  use  performance-
      ! related features provided by commercial edition of ALGLIB.
                                        
      -- ALGLIB --
         Copyright 17.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void lsfitlinearw(double[] y,
        double[] w,
        double[,] fmatrix,
        int n,
        int m,
        ref double[] c,
        lsfitreport rep,
        xparams _params)
    {
        c = new double[0];

        ap.assert(n >= 1, "LSFitLinearW: N<1!");
        ap.assert(m >= 1, "LSFitLinearW: M<1!");
        ap.assert(ap.len(y) >= n, "LSFitLinearW: length(Y)<N!");
        ap.assert(apserv.isfinitevector(y, n, _params), "LSFitLinearW: Y contains infinite or NaN values!");
        ap.assert(ap.len(w) >= n, "LSFitLinearW: length(W)<N!");
        ap.assert(apserv.isfinitevector(w, n, _params), "LSFitLinearW: W contains infinite or NaN values!");
        ap.assert(ap.rows(fmatrix) >= n, "LSFitLinearW: rows(FMatrix)<N!");
        ap.assert(ap.cols(fmatrix) >= m, "LSFitLinearW: cols(FMatrix)<M!");
        ap.assert(apserv.apservisfinitematrix(fmatrix, n, m, _params), "LSFitLinearW: FMatrix contains infinite or NaN values!");
        lsfitlinearinternal(y, w, fmatrix, n, m, ref c, rep, _params);
    }


    /*************************************************************************
    Weighted constained linear least squares fitting.

    This  is  variation  of LSFitLinearW(), which searchs for min|A*x=b| given
    that  K  additional  constaints  C*x=bc are satisfied. It reduces original
    task to modified one: min|B*y-d| WITHOUT constraints,  then LSFitLinearW()
    is called.

    IMPORTANT: if you want to perform  polynomial  fitting,  it  may  be  more
               convenient to use PolynomialFit() function. This function gives
               best  results  on  polynomial  problems  and  solves  numerical
               stability  issues  which  arise  when   you   fit   high-degree
               polynomials to your data.

    INPUT PARAMETERS:
        Y       -   array[0..N-1] Function values in  N  points.
        W       -   array[0..N-1]  Weights  corresponding to function  values.
                    Each summand in square  sum  of  approximation  deviations
                    from  given  values  is  multiplied  by  the   square   of
                    corresponding weight.
        FMatrix -   a table of basis functions values, array[0..N-1, 0..M-1].
                    FMatrix[I,J] - value of J-th basis function in I-th point.
        CMatrix -   a table of constaints, array[0..K-1,0..M].
                    I-th row of CMatrix corresponds to I-th linear constraint:
                    CMatrix[I,0]*C[0] + ... + CMatrix[I,M-1]*C[M-1] = CMatrix[I,M]
        N       -   number of points used. N>=1.
        M       -   number of basis functions, M>=1.
        K       -   number of constraints, 0 <= K < M
                    K=0 corresponds to absence of constraints.

    OUTPUT PARAMETERS:
        C       -   decomposition coefficients, array[0..M-1]
        Rep     -   fitting report. The following fields are set:
                    * Rep.TerminationType is a completion code:
                      * set to  1 on success
                      * set to -3 on failure due to  problematic  constraints:
                        either too many  constraints (M or  more),  degenerate
                        constraints (some constraints are repetead  twice)  or
                        inconsistent constraints are specified
                    * R2                non-adjusted coefficient of determination
                                        (non-weighted)
                    * RMSError          rms error on the (X,Y).
                    * AvgError          average error on the (X,Y).
                    * AvgRelError       average relative error on the non-zero Y
                    * MaxError          maximum error
                                        NON-WEIGHTED ERRORS ARE CALCULATED

    IMPORTANT:
        this subroitine doesn't calculate task's condition number for K<>0.
                    
    ERRORS IN PARAMETERS                
                    
    This  solver  also  calculates different kinds of errors in parameters and
    fills corresponding fields of report:
    * Rep.CovPar        covariance matrix for parameters, array[K,K].
    * Rep.ErrPar        errors in parameters, array[K],
                        errpar = sqrt(diag(CovPar))
    * Rep.ErrCurve      vector of fit errors - standard deviations of empirical
                        best-fit curve from "ideal" best-fit curve built  with
                        infinite number of samples, array[N].
                        errcurve = sqrt(diag(F*CovPar*F')),
                        where F is functions matrix.
    * Rep.Noise         vector of per-point estimates of noise, array[N]

    IMPORTANT:  errors  in  parameters  are  calculated  without  taking  into
                account boundary/linear constraints! Presence  of  constraints
                changes distribution of errors, but there is no  easy  way  to
                account for constraints when you calculate covariance matrix.
                
    NOTE:       noise in the data is estimated as follows:
                * for fitting without user-supplied  weights  all  points  are
                  assumed to have same level of noise, which is estimated from
                  the data
                * for fitting with user-supplied weights we assume that  noise
                  level in I-th point is inversely proportional to Ith weight.
                  Coefficient of proportionality is estimated from the data.
                
    NOTE:       we apply small amount of regularization when we invert squared
                Jacobian and calculate covariance matrix. It  guarantees  that
                algorithm won't divide by zero  during  inversion,  but  skews
                error estimates a bit (fractional error is about 10^-9).
                
                However, we believe that this difference is insignificant  for
                all practical purposes except for the situation when you  want
                to compare ALGLIB results with "reference"  implementation  up
                to the last significant digit.
                
    NOTE:       covariance matrix is estimated using  correction  for  degrees
                of freedom (covariances are divided by N-M instead of dividing
                by N).

      ! FREE EDITION OF ALGLIB:
      ! 
      ! Free Edition of ALGLIB supports following important features for  this
      ! function:
      ! * C++ version: x64 SIMD support using C++ intrinsics
      ! * C#  version: x64 SIMD support using NET5/NetCore hardware intrinsics
      !
      ! We  recommend  you  to  read  'Compiling ALGLIB' section of the ALGLIB
      ! Reference Manual in order  to  find  out  how to activate SIMD support
      ! in ALGLIB.

      ! COMMERCIAL EDITION OF ALGLIB:
      ! 
      ! Commercial Edition of ALGLIB includes following important improvements
      ! of this function:
      ! * high-performance native backend with same C# interface (C# version)
      ! * multithreading support (C++ and C# versions)
      ! * hardware vendor (Intel) implementations of linear algebra primitives
      !   (C++ and C# versions, x86/x64 platform)
      ! 
      ! We recommend you to read 'Working with commercial version' section  of
      ! ALGLIB Reference Manual in order to find out how to  use  performance-
      ! related features provided by commercial edition of ALGLIB.

      -- ALGLIB --
         Copyright 07.09.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void lsfitlinearwc(double[] y,
        double[] w,
        double[,] fmatrix,
        double[,] cmatrix,
        int n,
        int m,
        int k,
        ref double[] c,
        lsfitreport rep,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double[] tau = new double[0];
        double[,] q = new double[0, 0];
        double[,] f2 = new double[0, 0];
        double[] tmp = new double[0];
        double[] c0 = new double[0];
        double v = 0;
        int i_ = 0;

        y = (double[])y.Clone();
        cmatrix = (double[,])cmatrix.Clone();
        c = new double[0];

        ap.assert(n >= 1, "LSFitLinearWC: N<1!");
        ap.assert(m >= 1, "LSFitLinearWC: M<1!");
        ap.assert(k >= 0, "LSFitLinearWC: K<0!");
        ap.assert(ap.len(y) >= n, "LSFitLinearWC: length(Y)<N!");
        ap.assert(apserv.isfinitevector(y, n, _params), "LSFitLinearWC: Y contains infinite or NaN values!");
        ap.assert(ap.len(w) >= n, "LSFitLinearWC: length(W)<N!");
        ap.assert(apserv.isfinitevector(w, n, _params), "LSFitLinearWC: W contains infinite or NaN values!");
        ap.assert(ap.rows(fmatrix) >= n, "LSFitLinearWC: rows(FMatrix)<N!");
        ap.assert(ap.cols(fmatrix) >= m, "LSFitLinearWC: cols(FMatrix)<M!");
        ap.assert(apserv.apservisfinitematrix(fmatrix, n, m, _params), "LSFitLinearWC: FMatrix contains infinite or NaN values!");
        ap.assert(ap.rows(cmatrix) >= k, "LSFitLinearWC: rows(CMatrix)<K!");
        ap.assert(ap.cols(cmatrix) >= m + 1 || k == 0, "LSFitLinearWC: cols(CMatrix)<M+1!");
        ap.assert(apserv.apservisfinitematrix(cmatrix, k, m + 1, _params), "LSFitLinearWC: CMatrix contains infinite or NaN values!");
        if (k >= m)
        {
            rep.terminationtype = -3;
            return;
        }

        //
        // Solve
        //
        if (k == 0)
        {

            //
            // no constraints
            //
            lsfitlinearinternal(y, w, fmatrix, n, m, ref c, rep, _params);
        }
        else
        {

            //
            // First, find general form solution of constraints system:
            // * factorize C = L*Q
            // * unpack Q
            // * fill upper part of C with zeros (for RCond)
            //
            // We got C=C0+Q2'*y where Q2 is lower M-K rows of Q.
            //
            ortfac.rmatrixlq(cmatrix, k, m, ref tau, _params);
            ortfac.rmatrixlqunpackq(cmatrix, k, m, tau, m, ref q, _params);
            for (i = 0; i <= k - 1; i++)
            {
                for (j = i + 1; j <= m - 1; j++)
                {
                    cmatrix[i, j] = 0.0;
                }
            }
            if ((double)(rcond.rmatrixlurcondinf(cmatrix, k, _params)) < (double)(1000 * math.machineepsilon))
            {
                rep.terminationtype = -3;
                return;
            }
            tmp = new double[k];
            for (i = 0; i <= k - 1; i++)
            {
                if (i > 0)
                {
                    v = 0.0;
                    for (i_ = 0; i_ <= i - 1; i_++)
                    {
                        v += cmatrix[i, i_] * tmp[i_];
                    }
                }
                else
                {
                    v = 0;
                }
                tmp[i] = (cmatrix[i, m] - v) / cmatrix[i, i];
            }
            c0 = new double[m];
            for (i = 0; i <= m - 1; i++)
            {
                c0[i] = 0;
            }
            for (i = 0; i <= k - 1; i++)
            {
                v = tmp[i];
                for (i_ = 0; i_ <= m - 1; i_++)
                {
                    c0[i_] = c0[i_] + v * q[i, i_];
                }
            }

            //
            // Second, prepare modified matrix F2 = F*Q2' and solve modified task
            //
            tmp = new double[Math.Max(n, m) + 1];
            f2 = new double[n, m - k];
            RawBlas.matrixvectormultiply(fmatrix, 0, n - 1, 0, m - 1, false, c0, 0, m - 1, -1.0, ref y, 0, n - 1, 1.0, _params);
            ablas.rmatrixgemm(n, m - k, m, 1.0, fmatrix, 0, 0, 0, q, k, 0, 1, 0.0, f2, 0, 0, _params);
            lsfitlinearinternal(y, w, f2, n, m - k, ref tmp, rep, _params);
            rep.taskrcond = -1;
            if (rep.terminationtype <= 0)
            {
                return;
            }

            //
            // then, convert back to original answer: C = C0 + Q2'*Y0
            //
            c = new double[m];
            for (i_ = 0; i_ <= m - 1; i_++)
            {
                c[i_] = c0[i_];
            }
            RawBlas.matrixvectormultiply(q, k, m - 1, 0, m - 1, true, tmp, 0, m - k - 1, 1.0, ref c, 0, m - 1, 1.0, _params);
        }
    }


    /*************************************************************************
    Linear least squares fitting.

    QR decomposition is used to reduce task to MxM, then triangular solver  or
    SVD-based solver is used depending on condition number of the  system.  It
    allows to maximize speed and retain decent accuracy.

    IMPORTANT: if you want to perform  polynomial  fitting,  it  may  be  more
               convenient to use PolynomialFit() function. This function gives
               best  results  on  polynomial  problems  and  solves  numerical
               stability  issues  which  arise  when   you   fit   high-degree
               polynomials to your data.

    INPUT PARAMETERS:
        Y       -   array[0..N-1] Function values in  N  points.
        FMatrix -   a table of basis functions values, array[0..N-1, 0..M-1].
                    FMatrix[I, J] - value of J-th basis function in I-th point.
        N       -   number of points used. N>=1.
        M       -   number of basis functions, M>=1.

    OUTPUT PARAMETERS:
        C       -   decomposition coefficients, array[0..M-1]
        Rep     -   fitting report. Following fields are set:
                    * Rep.TerminationType is a completion code, always set  to
                      1 which denotes success
                    * Rep.TaskRCond     reciprocal of condition number
                    * R2                non-adjusted coefficient of determination
                                        (non-weighted)
                    * RMSError          rms error on the (X,Y).
                    * AvgError          average error on the (X,Y).
                    * AvgRelError       average relative error on the non-zero Y
                    * MaxError          maximum error
                                        NON-WEIGHTED ERRORS ARE CALCULATED
                    
    ERRORS IN PARAMETERS                
                    
    This  solver  also  calculates different kinds of errors in parameters and
    fills corresponding fields of report:
    * Rep.CovPar        covariance matrix for parameters, array[K,K].
    * Rep.ErrPar        errors in parameters, array[K],
                        errpar = sqrt(diag(CovPar))
    * Rep.ErrCurve      vector of fit errors - standard deviations of empirical
                        best-fit curve from "ideal" best-fit curve built  with
                        infinite number of samples, array[N].
                        errcurve = sqrt(diag(F*CovPar*F')),
                        where F is functions matrix.
    * Rep.Noise         vector of per-point estimates of noise, array[N]
                
    NOTE:       noise in the data is estimated as follows:
                * for fitting without user-supplied  weights  all  points  are
                  assumed to have same level of noise, which is estimated from
                  the data
                * for fitting with user-supplied weights we assume that  noise
                  level in I-th point is inversely proportional to Ith weight.
                  Coefficient of proportionality is estimated from the data.
                
    NOTE:       we apply small amount of regularization when we invert squared
                Jacobian and calculate covariance matrix. It  guarantees  that
                algorithm won't divide by zero  during  inversion,  but  skews
                error estimates a bit (fractional error is about 10^-9).
                
                However, we believe that this difference is insignificant  for
                all practical purposes except for the situation when you  want
                to compare ALGLIB results with "reference"  implementation  up
                to the last significant digit.
                
    NOTE:       covariance matrix is estimated using  correction  for  degrees
                of freedom (covariances are divided by N-M instead of dividing
                by N).

      ! FREE EDITION OF ALGLIB:
      ! 
      ! Free Edition of ALGLIB supports following important features for  this
      ! function:
      ! * C++ version: x64 SIMD support using C++ intrinsics
      ! * C#  version: x64 SIMD support using NET5/NetCore hardware intrinsics
      !
      ! We  recommend  you  to  read  'Compiling ALGLIB' section of the ALGLIB
      ! Reference Manual in order  to  find  out  how to activate SIMD support
      ! in ALGLIB.

      ! COMMERCIAL EDITION OF ALGLIB:
      ! 
      ! Commercial Edition of ALGLIB includes following important improvements
      ! of this function:
      ! * high-performance native backend with same C# interface (C# version)
      ! * multithreading support (C++ and C# versions)
      ! * hardware vendor (Intel) implementations of linear algebra primitives
      !   (C++ and C# versions, x86/x64 platform)
      ! 
      ! We recommend you to read 'Working with commercial version' section  of
      ! ALGLIB Reference Manual in order to find out how to  use  performance-
      ! related features provided by commercial edition of ALGLIB.

      -- ALGLIB --
         Copyright 17.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void lsfitlinear(double[] y,
        double[,] fmatrix,
        int n,
        int m,
        ref double[] c,
        lsfitreport rep,
        xparams _params)
    {
        double[] w = new double[0];
        int i = 0;

        c = new double[0];

        ap.assert(n >= 1, "LSFitLinear: N<1!");
        ap.assert(m >= 1, "LSFitLinear: M<1!");
        ap.assert(ap.len(y) >= n, "LSFitLinear: length(Y)<N!");
        ap.assert(apserv.isfinitevector(y, n, _params), "LSFitLinear: Y contains infinite or NaN values!");
        ap.assert(ap.rows(fmatrix) >= n, "LSFitLinear: rows(FMatrix)<N!");
        ap.assert(ap.cols(fmatrix) >= m, "LSFitLinear: cols(FMatrix)<M!");
        ap.assert(apserv.apservisfinitematrix(fmatrix, n, m, _params), "LSFitLinear: FMatrix contains infinite or NaN values!");
        w = new double[n];
        for (i = 0; i <= n - 1; i++)
        {
            w[i] = 1;
        }
        lsfitlinearinternal(y, w, fmatrix, n, m, ref c, rep, _params);
    }


    /*************************************************************************
    Constained linear least squares fitting.

    This  is  variation  of LSFitLinear(),  which searchs for min|A*x=b| given
    that  K  additional  constaints  C*x=bc are satisfied. It reduces original
    task to modified one: min|B*y-d| WITHOUT constraints,  then  LSFitLinear()
    is called.

    IMPORTANT: if you want to perform  polynomial  fitting,  it  may  be  more
               convenient to use PolynomialFit() function. This function gives
               best  results  on  polynomial  problems  and  solves  numerical
               stability  issues  which  arise  when   you   fit   high-degree
               polynomials to your data.

    INPUT PARAMETERS:
        Y       -   array[0..N-1] Function values in  N  points.
        FMatrix -   a table of basis functions values, array[0..N-1, 0..M-1].
                    FMatrix[I,J] - value of J-th basis function in I-th point.
        CMatrix -   a table of constaints, array[0..K-1,0..M].
                    I-th row of CMatrix corresponds to I-th linear constraint:
                    CMatrix[I,0]*C[0] + ... + CMatrix[I,M-1]*C[M-1] = CMatrix[I,M]
        N       -   number of points used. N>=1.
        M       -   number of basis functions, M>=1.
        K       -   number of constraints, 0 <= K < M
                    K=0 corresponds to absence of constraints.

    OUTPUT PARAMETERS:
        C       -   decomposition coefficients, array[0..M-1]
        Rep     -   fitting report. Following fields are set:
                    * Rep.TerminationType is a completion code:
                      * set to  1 on success
                      * set to -3 on failure due to  problematic  constraints:
                        either too many  constraints (M or  more),  degenerate
                        constraints (some constraints are repetead  twice)  or
                        inconsistent constraints are specified
                    * R2                non-adjusted coefficient of determination
                                        (non-weighted)
                    * RMSError          rms error on the (X,Y).
                    * AvgError          average error on the (X,Y).
                    * AvgRelError       average relative error on the non-zero Y
                    * MaxError          maximum error
                                        NON-WEIGHTED ERRORS ARE CALCULATED

    IMPORTANT:
        this subroitine doesn't calculate task's condition number for K<>0.
                    
    ERRORS IN PARAMETERS                
                    
    This  solver  also  calculates different kinds of errors in parameters and
    fills corresponding fields of report:
    * Rep.CovPar        covariance matrix for parameters, array[K,K].
    * Rep.ErrPar        errors in parameters, array[K],
                        errpar = sqrt(diag(CovPar))
    * Rep.ErrCurve      vector of fit errors - standard deviations of empirical
                        best-fit curve from "ideal" best-fit curve built  with
                        infinite number of samples, array[N].
                        errcurve = sqrt(diag(F*CovPar*F')),
                        where F is functions matrix.
    * Rep.Noise         vector of per-point estimates of noise, array[N]

    IMPORTANT:  errors  in  parameters  are  calculated  without  taking  into
                account boundary/linear constraints! Presence  of  constraints
                changes distribution of errors, but there is no  easy  way  to
                account for constraints when you calculate covariance matrix.
                
    NOTE:       noise in the data is estimated as follows:
                * for fitting without user-supplied  weights  all  points  are
                  assumed to have same level of noise, which is estimated from
                  the data
                * for fitting with user-supplied weights we assume that  noise
                  level in I-th point is inversely proportional to Ith weight.
                  Coefficient of proportionality is estimated from the data.
                
    NOTE:       we apply small amount of regularization when we invert squared
                Jacobian and calculate covariance matrix. It  guarantees  that
                algorithm won't divide by zero  during  inversion,  but  skews
                error estimates a bit (fractional error is about 10^-9).
                
                However, we believe that this difference is insignificant  for
                all practical purposes except for the situation when you  want
                to compare ALGLIB results with "reference"  implementation  up
                to the last significant digit.
                
    NOTE:       covariance matrix is estimated using  correction  for  degrees
                of freedom (covariances are divided by N-M instead of dividing
                by N).

      ! FREE EDITION OF ALGLIB:
      ! 
      ! Free Edition of ALGLIB supports following important features for  this
      ! function:
      ! * C++ version: x64 SIMD support using C++ intrinsics
      ! * C#  version: x64 SIMD support using NET5/NetCore hardware intrinsics
      !
      ! We  recommend  you  to  read  'Compiling ALGLIB' section of the ALGLIB
      ! Reference Manual in order  to  find  out  how to activate SIMD support
      ! in ALGLIB.

      ! COMMERCIAL EDITION OF ALGLIB:
      ! 
      ! Commercial Edition of ALGLIB includes following important improvements
      ! of this function:
      ! * high-performance native backend with same C# interface (C# version)
      ! * multithreading support (C++ and C# versions)
      ! * hardware vendor (Intel) implementations of linear algebra primitives
      !   (C++ and C# versions, x86/x64 platform)
      ! 
      ! We recommend you to read 'Working with commercial version' section  of
      ! ALGLIB Reference Manual in order to find out how to  use  performance-
      ! related features provided by commercial edition of ALGLIB.

      -- ALGLIB --
         Copyright 07.09.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void lsfitlinearc(double[] y,
        double[,] fmatrix,
        double[,] cmatrix,
        int n,
        int m,
        int k,
        ref double[] c,
        lsfitreport rep,
        xparams _params)
    {
        double[] w = new double[0];
        int i = 0;

        y = (double[])y.Clone();
        c = new double[0];

        ap.assert(n >= 1, "LSFitLinearC: N<1!");
        ap.assert(m >= 1, "LSFitLinearC: M<1!");
        ap.assert(k >= 0, "LSFitLinearC: K<0!");
        ap.assert(ap.len(y) >= n, "LSFitLinearC: length(Y)<N!");
        ap.assert(apserv.isfinitevector(y, n, _params), "LSFitLinearC: Y contains infinite or NaN values!");
        ap.assert(ap.rows(fmatrix) >= n, "LSFitLinearC: rows(FMatrix)<N!");
        ap.assert(ap.cols(fmatrix) >= m, "LSFitLinearC: cols(FMatrix)<M!");
        ap.assert(apserv.apservisfinitematrix(fmatrix, n, m, _params), "LSFitLinearC: FMatrix contains infinite or NaN values!");
        ap.assert(ap.rows(cmatrix) >= k, "LSFitLinearC: rows(CMatrix)<K!");
        ap.assert(ap.cols(cmatrix) >= m + 1 || k == 0, "LSFitLinearC: cols(CMatrix)<M+1!");
        ap.assert(apserv.apservisfinitematrix(cmatrix, k, m + 1, _params), "LSFitLinearC: CMatrix contains infinite or NaN values!");
        w = new double[n];
        for (i = 0; i <= n - 1; i++)
        {
            w[i] = 1;
        }
        lsfitlinearwc(y, w, fmatrix, cmatrix, n, m, k, ref c, rep, _params);
    }


    /*************************************************************************
    Weighted nonlinear least squares fitting using function values only.

    Combination of numerical differentiation and secant updates is used to
    obtain function Jacobian.

    Nonlinear task min(F(c)) is solved, where

        F(c) = (w[0]*(f(c,x[0])-y[0]))^2 + ... + (w[n-1]*(f(c,x[n-1])-y[n-1]))^2,

        * N is a number of points,
        * M is a dimension of a space points belong to,
        * K is a dimension of a space of parameters being fitted,
        * w is an N-dimensional vector of weight coefficients,
        * x is a set of N points, each of them is an M-dimensional vector,
        * c is a K-dimensional vector of parameters being fitted

    This subroutine uses only f(c,x[i]).

    INPUT PARAMETERS:
        X       -   array[0..N-1,0..M-1], points (one row = one point)
        Y       -   array[0..N-1], function values.
        W       -   weights, array[0..N-1]
        C       -   array[0..K-1], initial approximation to the solution,
        N       -   number of points, N>1
        M       -   dimension of space
        K       -   number of parameters being fitted
        DiffStep-   numerical differentiation step;
                    should not be very small or large;
                    large = loss of accuracy
                    small = growth of round-off errors

    OUTPUT PARAMETERS:
        State   -   structure which stores algorithm state

      -- ALGLIB --
         Copyright 18.10.2008 by Bochkanov Sergey
    *************************************************************************/
    public static void lsfitcreatewf(double[,] x,
        double[] y,
        double[] w,
        double[] c,
        int n,
        int m,
        int k,
        double diffstep,
        lsfitstate state,
        xparams _params)
    {
        int i = 0;
        int i_ = 0;

        ap.assert(n >= 1, "LSFitCreateWF: N<1!");
        ap.assert(m >= 1, "LSFitCreateWF: M<1!");
        ap.assert(k >= 1, "LSFitCreateWF: K<1!");
        ap.assert(ap.len(c) >= k, "LSFitCreateWF: length(C)<K!");
        ap.assert(apserv.isfinitevector(c, k, _params), "LSFitCreateWF: C contains infinite or NaN values!");
        ap.assert(ap.len(y) >= n, "LSFitCreateWF: length(Y)<N!");
        ap.assert(apserv.isfinitevector(y, n, _params), "LSFitCreateWF: Y contains infinite or NaN values!");
        ap.assert(ap.len(w) >= n, "LSFitCreateWF: length(W)<N!");
        ap.assert(apserv.isfinitevector(w, n, _params), "LSFitCreateWF: W contains infinite or NaN values!");
        ap.assert(ap.rows(x) >= n, "LSFitCreateWF: rows(X)<N!");
        ap.assert(ap.cols(x) >= m, "LSFitCreateWF: cols(X)<M!");
        ap.assert(apserv.apservisfinitematrix(x, n, m, _params), "LSFitCreateWF: X contains infinite or NaN values!");
        ap.assert(math.isfinite(diffstep), "LSFitCreateWF: DiffStep is not finite!");
        ap.assert((double)(diffstep) > (double)(0), "LSFitCreateWF: DiffStep<=0!");
        state.teststep = 0;
        state.diffstep = diffstep;
        state.npoints = n;
        state.nweights = n;
        state.wkind = 1;
        state.m = m;
        state.k = k;
        lsfitsetcond(state, 0.0, 0, _params);
        lsfitsetstpmax(state, 0.0, _params);
        lsfitsetxrep(state, false, _params);
        state.taskx = new double[n, m];
        state.tasky = new double[n];
        state.taskw = new double[n];
        state.c = new double[k];
        state.c0 = new double[k];
        state.c1 = new double[k];
        for (i_ = 0; i_ <= k - 1; i_++)
        {
            state.c0[i_] = c[i_];
        }
        for (i_ = 0; i_ <= k - 1; i_++)
        {
            state.c1[i_] = c[i_];
        }
        state.x = new double[m];
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.taskw[i_] = w[i_];
        }
        for (i = 0; i <= n - 1; i++)
        {
            for (i_ = 0; i_ <= m - 1; i_++)
            {
                state.taskx[i, i_] = x[i, i_];
            }
            state.tasky[i] = y[i];
        }
        state.s = new double[k];
        state.bndl = new double[k];
        state.bndu = new double[k];
        for (i = 0; i <= k - 1; i++)
        {
            state.s[i] = 1.0;
            state.bndl[i] = Double.NegativeInfinity;
            state.bndu[i] = Double.PositiveInfinity;
        }
        state.optalgo = 0;
        state.prevnpt = -1;
        state.prevalgo = -1;
        state.nec = 0;
        state.nic = 0;
        minlm.minlmcreatev(k, n, state.c0, diffstep, state.optstate, _params);
        lsfitclearrequestfields(state, _params);
        state.rstate.ia = new int[5 + 1];
        state.rstate.ra = new double[8 + 1];
        state.rstate.stage = -1;
    }


    /*************************************************************************
    Nonlinear least squares fitting using function values only.

    Combination of numerical differentiation and secant updates is used to
    obtain function Jacobian.

    Nonlinear task min(F(c)) is solved, where

        F(c) = (f(c,x[0])-y[0])^2 + ... + (f(c,x[n-1])-y[n-1])^2,

        * N is a number of points,
        * M is a dimension of a space points belong to,
        * K is a dimension of a space of parameters being fitted,
        * w is an N-dimensional vector of weight coefficients,
        * x is a set of N points, each of them is an M-dimensional vector,
        * c is a K-dimensional vector of parameters being fitted

    This subroutine uses only f(c,x[i]).

    INPUT PARAMETERS:
        X       -   array[0..N-1,0..M-1], points (one row = one point)
        Y       -   array[0..N-1], function values.
        C       -   array[0..K-1], initial approximation to the solution,
        N       -   number of points, N>1
        M       -   dimension of space
        K       -   number of parameters being fitted
        DiffStep-   numerical differentiation step;
                    should not be very small or large;
                    large = loss of accuracy
                    small = growth of round-off errors

    OUTPUT PARAMETERS:
        State   -   structure which stores algorithm state

      -- ALGLIB --
         Copyright 18.10.2008 by Bochkanov Sergey
    *************************************************************************/
    public static void lsfitcreatef(double[,] x,
        double[] y,
        double[] c,
        int n,
        int m,
        int k,
        double diffstep,
        lsfitstate state,
        xparams _params)
    {
        int i = 0;
        int i_ = 0;

        ap.assert(n >= 1, "LSFitCreateF: N<1!");
        ap.assert(m >= 1, "LSFitCreateF: M<1!");
        ap.assert(k >= 1, "LSFitCreateF: K<1!");
        ap.assert(ap.len(c) >= k, "LSFitCreateF: length(C)<K!");
        ap.assert(apserv.isfinitevector(c, k, _params), "LSFitCreateF: C contains infinite or NaN values!");
        ap.assert(ap.len(y) >= n, "LSFitCreateF: length(Y)<N!");
        ap.assert(apserv.isfinitevector(y, n, _params), "LSFitCreateF: Y contains infinite or NaN values!");
        ap.assert(ap.rows(x) >= n, "LSFitCreateF: rows(X)<N!");
        ap.assert(ap.cols(x) >= m, "LSFitCreateF: cols(X)<M!");
        ap.assert(apserv.apservisfinitematrix(x, n, m, _params), "LSFitCreateF: X contains infinite or NaN values!");
        ap.assert(ap.rows(x) >= n, "LSFitCreateF: rows(X)<N!");
        ap.assert(ap.cols(x) >= m, "LSFitCreateF: cols(X)<M!");
        ap.assert(apserv.apservisfinitematrix(x, n, m, _params), "LSFitCreateF: X contains infinite or NaN values!");
        ap.assert(math.isfinite(diffstep), "LSFitCreateF: DiffStep is not finite!");
        ap.assert((double)(diffstep) > (double)(0), "LSFitCreateF: DiffStep<=0!");
        state.teststep = 0;
        state.diffstep = diffstep;
        state.npoints = n;
        state.wkind = 0;
        state.m = m;
        state.k = k;
        lsfitsetcond(state, 0.0, 0, _params);
        lsfitsetstpmax(state, 0.0, _params);
        lsfitsetxrep(state, false, _params);
        state.taskx = new double[n, m];
        state.tasky = new double[n];
        state.c = new double[k];
        state.c0 = new double[k];
        state.c1 = new double[k];
        for (i_ = 0; i_ <= k - 1; i_++)
        {
            state.c0[i_] = c[i_];
        }
        for (i_ = 0; i_ <= k - 1; i_++)
        {
            state.c1[i_] = c[i_];
        }
        state.x = new double[m];
        for (i = 0; i <= n - 1; i++)
        {
            for (i_ = 0; i_ <= m - 1; i_++)
            {
                state.taskx[i, i_] = x[i, i_];
            }
            state.tasky[i] = y[i];
        }
        state.s = new double[k];
        state.bndl = new double[k];
        state.bndu = new double[k];
        for (i = 0; i <= k - 1; i++)
        {
            state.s[i] = 1.0;
            state.bndl[i] = Double.NegativeInfinity;
            state.bndu[i] = Double.PositiveInfinity;
        }
        state.optalgo = 0;
        state.prevnpt = -1;
        state.prevalgo = -1;
        state.nec = 0;
        state.nic = 0;
        minlm.minlmcreatev(k, n, state.c0, diffstep, state.optstate, _params);
        lsfitclearrequestfields(state, _params);
        state.rstate.ia = new int[5 + 1];
        state.rstate.ra = new double[8 + 1];
        state.rstate.stage = -1;
    }


    /*************************************************************************
    Weighted nonlinear least squares fitting using gradient only.

    Nonlinear task min(F(c)) is solved, where

        F(c) = (w[0]*(f(c,x[0])-y[0]))^2 + ... + (w[n-1]*(f(c,x[n-1])-y[n-1]))^2,
        
        * N is a number of points,
        * M is a dimension of a space points belong to,
        * K is a dimension of a space of parameters being fitted,
        * w is an N-dimensional vector of weight coefficients,
        * x is a set of N points, each of them is an M-dimensional vector,
        * c is a K-dimensional vector of parameters being fitted
        
    This subroutine uses only f(c,x[i]) and its gradient.
        
    INPUT PARAMETERS:
        X       -   array[0..N-1,0..M-1], points (one row = one point)
        Y       -   array[0..N-1], function values.
        W       -   weights, array[0..N-1]
        C       -   array[0..K-1], initial approximation to the solution,
        N       -   number of points, N>1
        M       -   dimension of space
        K       -   number of parameters being fitted
        CheapFG -   boolean flag, which is:
                    * True  if both function and gradient calculation complexity
                            are less than O(M^2).  An improved  algorithm  can
                            be  used  which corresponds  to  FGJ  scheme  from
                            MINLM unit.
                    * False otherwise.
                            Standard Jacibian-bases  Levenberg-Marquardt  algo
                            will be used (FJ scheme).

    OUTPUT PARAMETERS:
        State   -   structure which stores algorithm state

    See also:
        LSFitResults
        LSFitCreateFG (fitting without weights)
        LSFitCreateWFGH (fitting using Hessian)
        LSFitCreateFGH (fitting using Hessian, without weights)

      -- ALGLIB --
         Copyright 17.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void lsfitcreatewfg(double[,] x,
        double[] y,
        double[] w,
        double[] c,
        int n,
        int m,
        int k,
        bool cheapfg,
        lsfitstate state,
        xparams _params)
    {
        int i = 0;
        int i_ = 0;

        ap.assert(n >= 1, "LSFitCreateWFG: N<1!");
        ap.assert(m >= 1, "LSFitCreateWFG: M<1!");
        ap.assert(k >= 1, "LSFitCreateWFG: K<1!");
        ap.assert(ap.len(c) >= k, "LSFitCreateWFG: length(C)<K!");
        ap.assert(apserv.isfinitevector(c, k, _params), "LSFitCreateWFG: C contains infinite or NaN values!");
        ap.assert(ap.len(y) >= n, "LSFitCreateWFG: length(Y)<N!");
        ap.assert(apserv.isfinitevector(y, n, _params), "LSFitCreateWFG: Y contains infinite or NaN values!");
        ap.assert(ap.len(w) >= n, "LSFitCreateWFG: length(W)<N!");
        ap.assert(apserv.isfinitevector(w, n, _params), "LSFitCreateWFG: W contains infinite or NaN values!");
        ap.assert(ap.rows(x) >= n, "LSFitCreateWFG: rows(X)<N!");
        ap.assert(ap.cols(x) >= m, "LSFitCreateWFG: cols(X)<M!");
        ap.assert(apserv.apservisfinitematrix(x, n, m, _params), "LSFitCreateWFG: X contains infinite or NaN values!");
        state.teststep = 0;
        state.diffstep = 0;
        state.npoints = n;
        state.nweights = n;
        state.wkind = 1;
        state.m = m;
        state.k = k;
        lsfitsetcond(state, 0.0, 0, _params);
        lsfitsetstpmax(state, 0.0, _params);
        lsfitsetxrep(state, false, _params);
        state.taskx = new double[n, m];
        state.tasky = new double[n];
        state.taskw = new double[n];
        state.c = new double[k];
        state.c0 = new double[k];
        state.c1 = new double[k];
        for (i_ = 0; i_ <= k - 1; i_++)
        {
            state.c0[i_] = c[i_];
        }
        for (i_ = 0; i_ <= k - 1; i_++)
        {
            state.c1[i_] = c[i_];
        }
        state.x = new double[m];
        state.g = new double[k];
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.taskw[i_] = w[i_];
        }
        for (i = 0; i <= n - 1; i++)
        {
            for (i_ = 0; i_ <= m - 1; i_++)
            {
                state.taskx[i, i_] = x[i, i_];
            }
            state.tasky[i] = y[i];
        }
        state.s = new double[k];
        state.bndl = new double[k];
        state.bndu = new double[k];
        for (i = 0; i <= k - 1; i++)
        {
            state.s[i] = 1.0;
            state.bndl[i] = Double.NegativeInfinity;
            state.bndu[i] = Double.PositiveInfinity;
        }
        state.optalgo = 1;
        state.prevnpt = -1;
        state.prevalgo = -1;
        state.nec = 0;
        state.nic = 0;
        if (cheapfg)
        {
            minlm.minlmcreatevgj(k, n, state.c0, state.optstate, _params);
        }
        else
        {
            minlm.minlmcreatevj(k, n, state.c0, state.optstate, _params);
        }
        lsfitclearrequestfields(state, _params);
        state.rstate.ia = new int[5 + 1];
        state.rstate.ra = new double[8 + 1];
        state.rstate.stage = -1;
    }


    /*************************************************************************
    Nonlinear least squares fitting using gradient only, without individual
    weights.

    Nonlinear task min(F(c)) is solved, where

        F(c) = ((f(c,x[0])-y[0]))^2 + ... + ((f(c,x[n-1])-y[n-1]))^2,

        * N is a number of points,
        * M is a dimension of a space points belong to,
        * K is a dimension of a space of parameters being fitted,
        * x is a set of N points, each of them is an M-dimensional vector,
        * c is a K-dimensional vector of parameters being fitted

    This subroutine uses only f(c,x[i]) and its gradient.

    INPUT PARAMETERS:
        X       -   array[0..N-1,0..M-1], points (one row = one point)
        Y       -   array[0..N-1], function values.
        C       -   array[0..K-1], initial approximation to the solution,
        N       -   number of points, N>1
        M       -   dimension of space
        K       -   number of parameters being fitted
        CheapFG -   boolean flag, which is:
                    * True  if both function and gradient calculation complexity
                            are less than O(M^2).  An improved  algorithm  can
                            be  used  which corresponds  to  FGJ  scheme  from
                            MINLM unit.
                    * False otherwise.
                            Standard Jacibian-bases  Levenberg-Marquardt  algo
                            will be used (FJ scheme).

    OUTPUT PARAMETERS:
        State   -   structure which stores algorithm state

      -- ALGLIB --
         Copyright 17.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void lsfitcreatefg(double[,] x,
        double[] y,
        double[] c,
        int n,
        int m,
        int k,
        bool cheapfg,
        lsfitstate state,
        xparams _params)
    {
        int i = 0;
        int i_ = 0;

        ap.assert(n >= 1, "LSFitCreateFG: N<1!");
        ap.assert(m >= 1, "LSFitCreateFG: M<1!");
        ap.assert(k >= 1, "LSFitCreateFG: K<1!");
        ap.assert(ap.len(c) >= k, "LSFitCreateFG: length(C)<K!");
        ap.assert(apserv.isfinitevector(c, k, _params), "LSFitCreateFG: C contains infinite or NaN values!");
        ap.assert(ap.len(y) >= n, "LSFitCreateFG: length(Y)<N!");
        ap.assert(apserv.isfinitevector(y, n, _params), "LSFitCreateFG: Y contains infinite or NaN values!");
        ap.assert(ap.rows(x) >= n, "LSFitCreateFG: rows(X)<N!");
        ap.assert(ap.cols(x) >= m, "LSFitCreateFG: cols(X)<M!");
        ap.assert(apserv.apservisfinitematrix(x, n, m, _params), "LSFitCreateFG: X contains infinite or NaN values!");
        ap.assert(ap.rows(x) >= n, "LSFitCreateFG: rows(X)<N!");
        ap.assert(ap.cols(x) >= m, "LSFitCreateFG: cols(X)<M!");
        ap.assert(apserv.apservisfinitematrix(x, n, m, _params), "LSFitCreateFG: X contains infinite or NaN values!");
        state.teststep = 0;
        state.diffstep = 0;
        state.npoints = n;
        state.wkind = 0;
        state.m = m;
        state.k = k;
        lsfitsetcond(state, 0.0, 0, _params);
        lsfitsetstpmax(state, 0.0, _params);
        lsfitsetxrep(state, false, _params);
        state.taskx = new double[n, m];
        state.tasky = new double[n];
        state.c = new double[k];
        state.c0 = new double[k];
        state.c1 = new double[k];
        for (i_ = 0; i_ <= k - 1; i_++)
        {
            state.c0[i_] = c[i_];
        }
        for (i_ = 0; i_ <= k - 1; i_++)
        {
            state.c1[i_] = c[i_];
        }
        state.x = new double[m];
        state.g = new double[k];
        for (i = 0; i <= n - 1; i++)
        {
            for (i_ = 0; i_ <= m - 1; i_++)
            {
                state.taskx[i, i_] = x[i, i_];
            }
            state.tasky[i] = y[i];
        }
        state.s = new double[k];
        state.bndl = new double[k];
        state.bndu = new double[k];
        for (i = 0; i <= k - 1; i++)
        {
            state.s[i] = 1.0;
            state.bndl[i] = Double.NegativeInfinity;
            state.bndu[i] = Double.PositiveInfinity;
        }
        state.optalgo = 1;
        state.prevnpt = -1;
        state.prevalgo = -1;
        state.nec = 0;
        state.nic = 0;
        if (cheapfg)
        {
            minlm.minlmcreatevgj(k, n, state.c0, state.optstate, _params);
        }
        else
        {
            minlm.minlmcreatevj(k, n, state.c0, state.optstate, _params);
        }
        lsfitclearrequestfields(state, _params);
        state.rstate.ia = new int[5 + 1];
        state.rstate.ra = new double[8 + 1];
        state.rstate.stage = -1;
    }


    /*************************************************************************
    Weighted nonlinear least squares fitting using gradient/Hessian.

    Nonlinear task min(F(c)) is solved, where

        F(c) = (w[0]*(f(c,x[0])-y[0]))^2 + ... + (w[n-1]*(f(c,x[n-1])-y[n-1]))^2,

        * N is a number of points,
        * M is a dimension of a space points belong to,
        * K is a dimension of a space of parameters being fitted,
        * w is an N-dimensional vector of weight coefficients,
        * x is a set of N points, each of them is an M-dimensional vector,
        * c is a K-dimensional vector of parameters being fitted

    This subroutine uses f(c,x[i]), its gradient and its Hessian.

    INPUT PARAMETERS:
        X       -   array[0..N-1,0..M-1], points (one row = one point)
        Y       -   array[0..N-1], function values.
        W       -   weights, array[0..N-1]
        C       -   array[0..K-1], initial approximation to the solution,
        N       -   number of points, N>1
        M       -   dimension of space
        K       -   number of parameters being fitted

    OUTPUT PARAMETERS:
        State   -   structure which stores algorithm state

      -- ALGLIB --
         Copyright 17.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void lsfitcreatewfgh(double[,] x,
        double[] y,
        double[] w,
        double[] c,
        int n,
        int m,
        int k,
        lsfitstate state,
        xparams _params)
    {
        int i = 0;
        int i_ = 0;

        ap.assert(n >= 1, "LSFitCreateWFGH: N<1!");
        ap.assert(m >= 1, "LSFitCreateWFGH: M<1!");
        ap.assert(k >= 1, "LSFitCreateWFGH: K<1!");
        ap.assert(ap.len(c) >= k, "LSFitCreateWFGH: length(C)<K!");
        ap.assert(apserv.isfinitevector(c, k, _params), "LSFitCreateWFGH: C contains infinite or NaN values!");
        ap.assert(ap.len(y) >= n, "LSFitCreateWFGH: length(Y)<N!");
        ap.assert(apserv.isfinitevector(y, n, _params), "LSFitCreateWFGH: Y contains infinite or NaN values!");
        ap.assert(ap.len(w) >= n, "LSFitCreateWFGH: length(W)<N!");
        ap.assert(apserv.isfinitevector(w, n, _params), "LSFitCreateWFGH: W contains infinite or NaN values!");
        ap.assert(ap.rows(x) >= n, "LSFitCreateWFGH: rows(X)<N!");
        ap.assert(ap.cols(x) >= m, "LSFitCreateWFGH: cols(X)<M!");
        ap.assert(apserv.apservisfinitematrix(x, n, m, _params), "LSFitCreateWFGH: X contains infinite or NaN values!");
        state.teststep = 0;
        state.diffstep = 0;
        state.npoints = n;
        state.nweights = n;
        state.wkind = 1;
        state.m = m;
        state.k = k;
        lsfitsetcond(state, 0.0, 0, _params);
        lsfitsetstpmax(state, 0.0, _params);
        lsfitsetxrep(state, false, _params);
        state.taskx = new double[n, m];
        state.tasky = new double[n];
        state.taskw = new double[n];
        state.c = new double[k];
        state.c0 = new double[k];
        state.c1 = new double[k];
        for (i_ = 0; i_ <= k - 1; i_++)
        {
            state.c0[i_] = c[i_];
        }
        for (i_ = 0; i_ <= k - 1; i_++)
        {
            state.c1[i_] = c[i_];
        }
        state.h = new double[k, k];
        state.x = new double[m];
        state.g = new double[k];
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            state.taskw[i_] = w[i_];
        }
        for (i = 0; i <= n - 1; i++)
        {
            for (i_ = 0; i_ <= m - 1; i_++)
            {
                state.taskx[i, i_] = x[i, i_];
            }
            state.tasky[i] = y[i];
        }
        state.s = new double[k];
        state.bndl = new double[k];
        state.bndu = new double[k];
        for (i = 0; i <= k - 1; i++)
        {
            state.s[i] = 1.0;
            state.bndl[i] = Double.NegativeInfinity;
            state.bndu[i] = Double.PositiveInfinity;
        }
        state.optalgo = 2;
        state.prevnpt = -1;
        state.prevalgo = -1;
        state.nec = 0;
        state.nic = 0;
        minlm.minlmcreatefgh(k, state.c0, state.optstate, _params);
        lsfitclearrequestfields(state, _params);
        state.rstate.ia = new int[5 + 1];
        state.rstate.ra = new double[8 + 1];
        state.rstate.stage = -1;
    }


    /*************************************************************************
    Nonlinear least squares fitting using gradient/Hessian, without individial
    weights.

    Nonlinear task min(F(c)) is solved, where

        F(c) = ((f(c,x[0])-y[0]))^2 + ... + ((f(c,x[n-1])-y[n-1]))^2,

        * N is a number of points,
        * M is a dimension of a space points belong to,
        * K is a dimension of a space of parameters being fitted,
        * x is a set of N points, each of them is an M-dimensional vector,
        * c is a K-dimensional vector of parameters being fitted

    This subroutine uses f(c,x[i]), its gradient and its Hessian.

    INPUT PARAMETERS:
        X       -   array[0..N-1,0..M-1], points (one row = one point)
        Y       -   array[0..N-1], function values.
        C       -   array[0..K-1], initial approximation to the solution,
        N       -   number of points, N>1
        M       -   dimension of space
        K       -   number of parameters being fitted

    OUTPUT PARAMETERS:
        State   -   structure which stores algorithm state


      -- ALGLIB --
         Copyright 17.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void lsfitcreatefgh(double[,] x,
        double[] y,
        double[] c,
        int n,
        int m,
        int k,
        lsfitstate state,
        xparams _params)
    {
        int i = 0;
        int i_ = 0;

        ap.assert(n >= 1, "LSFitCreateFGH: N<1!");
        ap.assert(m >= 1, "LSFitCreateFGH: M<1!");
        ap.assert(k >= 1, "LSFitCreateFGH: K<1!");
        ap.assert(ap.len(c) >= k, "LSFitCreateFGH: length(C)<K!");
        ap.assert(apserv.isfinitevector(c, k, _params), "LSFitCreateFGH: C contains infinite or NaN values!");
        ap.assert(ap.len(y) >= n, "LSFitCreateFGH: length(Y)<N!");
        ap.assert(apserv.isfinitevector(y, n, _params), "LSFitCreateFGH: Y contains infinite or NaN values!");
        ap.assert(ap.rows(x) >= n, "LSFitCreateFGH: rows(X)<N!");
        ap.assert(ap.cols(x) >= m, "LSFitCreateFGH: cols(X)<M!");
        ap.assert(apserv.apservisfinitematrix(x, n, m, _params), "LSFitCreateFGH: X contains infinite or NaN values!");
        state.teststep = 0;
        state.diffstep = 0;
        state.npoints = n;
        state.wkind = 0;
        state.m = m;
        state.k = k;
        lsfitsetcond(state, 0.0, 0, _params);
        lsfitsetstpmax(state, 0.0, _params);
        lsfitsetxrep(state, false, _params);
        state.taskx = new double[n, m];
        state.tasky = new double[n];
        state.c = new double[k];
        state.c0 = new double[k];
        state.c1 = new double[k];
        for (i_ = 0; i_ <= k - 1; i_++)
        {
            state.c0[i_] = c[i_];
        }
        for (i_ = 0; i_ <= k - 1; i_++)
        {
            state.c1[i_] = c[i_];
        }
        state.h = new double[k, k];
        state.x = new double[m];
        state.g = new double[k];
        for (i = 0; i <= n - 1; i++)
        {
            for (i_ = 0; i_ <= m - 1; i_++)
            {
                state.taskx[i, i_] = x[i, i_];
            }
            state.tasky[i] = y[i];
        }
        state.s = new double[k];
        state.bndl = new double[k];
        state.bndu = new double[k];
        for (i = 0; i <= k - 1; i++)
        {
            state.s[i] = 1.0;
            state.bndl[i] = Double.NegativeInfinity;
            state.bndu[i] = Double.PositiveInfinity;
        }
        state.optalgo = 2;
        state.prevnpt = -1;
        state.prevalgo = -1;
        state.nec = 0;
        state.nic = 0;
        minlm.minlmcreatefgh(k, state.c0, state.optstate, _params);
        lsfitclearrequestfields(state, _params);
        state.rstate.ia = new int[5 + 1];
        state.rstate.ra = new double[8 + 1];
        state.rstate.stage = -1;
    }


    /*************************************************************************
    Stopping conditions for nonlinear least squares fitting.

    INPUT PARAMETERS:
        State   -   structure which stores algorithm state
        EpsX    -   >=0
                    The subroutine finishes its work if  on  k+1-th  iteration
                    the condition |v|<=EpsX is fulfilled, where:
                    * |.| means Euclidian norm
                    * v - scaled step vector, v[i]=dx[i]/s[i]
                    * dx - ste pvector, dx=X(k+1)-X(k)
                    * s - scaling coefficients set by LSFitSetScale()
        MaxIts  -   maximum number of iterations. If MaxIts=0, the  number  of
                    iterations   is    unlimited.   Only   Levenberg-Marquardt
                    iterations  are  counted  (L-BFGS/CG  iterations  are  NOT
                    counted because their cost is very low compared to that of
                    LM).

    NOTE

    Passing EpsX=0  and  MaxIts=0  (simultaneously)  will  lead  to  automatic
    stopping criterion selection (according to the scheme used by MINLM unit).


      -- ALGLIB --
         Copyright 17.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void lsfitsetcond(lsfitstate state,
        double epsx,
        int maxits,
        xparams _params)
    {
        ap.assert(math.isfinite(epsx), "LSFitSetCond: EpsX is not finite!");
        ap.assert((double)(epsx) >= (double)(0), "LSFitSetCond: negative EpsX!");
        ap.assert(maxits >= 0, "LSFitSetCond: negative MaxIts!");
        state.epsx = epsx;
        state.maxits = maxits;
    }


    /*************************************************************************
    This function sets maximum step length

    INPUT PARAMETERS:
        State   -   structure which stores algorithm state
        StpMax  -   maximum step length, >=0. Set StpMax to 0.0,  if you don't
                    want to limit step length.

    Use this subroutine when you optimize target function which contains exp()
    or  other  fast  growing  functions,  and optimization algorithm makes too
    large  steps  which  leads  to overflow. This function allows us to reject
    steps  that  are  too  large  (and  therefore  expose  us  to the possible
    overflow) without actually calculating function value at the x+stp*d.

    NOTE: non-zero StpMax leads to moderate  performance  degradation  because
    intermediate  step  of  preconditioned L-BFGS optimization is incompatible
    with limits on step size.

      -- ALGLIB --
         Copyright 02.04.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void lsfitsetstpmax(lsfitstate state,
        double stpmax,
        xparams _params)
    {
        ap.assert((double)(stpmax) >= (double)(0), "LSFitSetStpMax: StpMax<0!");
        state.stpmax = stpmax;
    }


    /*************************************************************************
    This function turns on/off reporting.

    INPUT PARAMETERS:
        State   -   structure which stores algorithm state
        NeedXRep-   whether iteration reports are needed or not
        
    When reports are needed, State.C (current parameters) and State.F (current
    value of fitting function) are reported.


      -- ALGLIB --
         Copyright 15.08.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void lsfitsetxrep(lsfitstate state,
        bool needxrep,
        xparams _params)
    {
        state.xrep = needxrep;
    }


    /*************************************************************************
    This function sets scaling coefficients for underlying optimizer.

    ALGLIB optimizers use scaling matrices to test stopping  conditions  (step
    size and gradient are scaled before comparison with tolerances).  Scale of
    the I-th variable is a translation invariant measure of:
    a) "how large" the variable is
    b) how large the step should be to make significant changes in the function

    Generally, scale is NOT considered to be a form of preconditioner.  But LM
    optimizer is unique in that it uses scaling matrix both  in  the  stopping
    condition tests and as Marquardt damping factor.

    Proper scaling is very important for the algorithm performance. It is less
    important for the quality of results, but still has some influence (it  is
    easier  to  converge  when  variables  are  properly  scaled, so premature
    stopping is possible when very badly scalled variables are  combined  with
    relaxed stopping conditions).

    INPUT PARAMETERS:
        State   -   structure stores algorithm state
        S       -   array[N], non-zero scaling coefficients
                    S[i] may be negative, sign doesn't matter.

      -- ALGLIB --
         Copyright 14.01.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void lsfitsetscale(lsfitstate state,
        double[] s,
        xparams _params)
    {
        int i = 0;

        ap.assert(ap.len(s) >= state.k, "LSFitSetScale: Length(S)<K");
        for (i = 0; i <= state.k - 1; i++)
        {
            ap.assert(math.isfinite(s[i]), "LSFitSetScale: S contains infinite or NAN elements");
            ap.assert((double)(s[i]) != (double)(0), "LSFitSetScale: S contains infinite or NAN elements");
            state.s[i] = Math.Abs(s[i]);
        }
    }


    /*************************************************************************
    This function sets boundary constraints for underlying optimizer

    Boundary constraints are inactive by default (after initial creation).
    They are preserved until explicitly turned off with another SetBC() call.

    INPUT PARAMETERS:
        State   -   structure stores algorithm state
        BndL    -   lower bounds, array[K].
                    If some (all) variables are unbounded, you may specify
                    very small number or -INF (latter is recommended because
                    it will allow solver to use better algorithm).
        BndU    -   upper bounds, array[K].
                    If some (all) variables are unbounded, you may specify
                    very large number or +INF (latter is recommended because
                    it will allow solver to use better algorithm).

    NOTE 1: it is possible to specify BndL[i]=BndU[i]. In this case I-th
    variable will be "frozen" at X[i]=BndL[i]=BndU[i].

    NOTE 2: unlike other constrained optimization algorithms, this solver  has
    following useful properties:
    * bound constraints are always satisfied exactly
    * function is evaluated only INSIDE area specified by bound constraints

      -- ALGLIB --
         Copyright 14.01.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void lsfitsetbc(lsfitstate state,
        double[] bndl,
        double[] bndu,
        xparams _params)
    {
        int i = 0;
        int k = 0;

        k = state.k;
        ap.assert(ap.len(bndl) >= k, "LSFitSetBC: Length(BndL)<K");
        ap.assert(ap.len(bndu) >= k, "LSFitSetBC: Length(BndU)<K");
        for (i = 0; i <= k - 1; i++)
        {
            ap.assert(math.isfinite(bndl[i]) || Double.IsNegativeInfinity(bndl[i]), "LSFitSetBC: BndL contains NAN or +INF");
            ap.assert(math.isfinite(bndu[i]) || Double.IsPositiveInfinity(bndu[i]), "LSFitSetBC: BndU contains NAN or -INF");
            if (math.isfinite(bndl[i]) && math.isfinite(bndu[i]))
            {
                ap.assert((double)(bndl[i]) <= (double)(bndu[i]), "LSFitSetBC: BndL[i]>BndU[i]");
            }
            state.bndl[i] = bndl[i];
            state.bndu[i] = bndu[i];
        }
    }


    /*************************************************************************
    This function sets linear constraints for underlying optimizer

    Linear constraints are inactive by default (after initial creation).
    They are preserved until explicitly turned off with another SetLC() call.

    INPUT PARAMETERS:
        State   -   structure stores algorithm state
        C       -   linear constraints, array[K,N+1].
                    Each row of C represents one constraint, either equality
                    or inequality (see below):
                    * first N elements correspond to coefficients,
                    * last element corresponds to the right part.
                    All elements of C (including right part) must be finite.
        CT      -   type of constraints, array[K]:
                    * if CT[i]>0, then I-th constraint is C[i,*]*x >= C[i,n+1]
                    * if CT[i]=0, then I-th constraint is C[i,*]*x  = C[i,n+1]
                    * if CT[i]<0, then I-th constraint is C[i,*]*x <= C[i,n+1]
        K       -   number of equality/inequality constraints, K>=0:
                    * if given, only leading K elements of C/CT are used
                    * if not given, automatically determined from sizes of C/CT

    IMPORTANT: if you have linear constraints, it is strongly  recommended  to
               set scale of variables with lsfitsetscale(). QP solver which is
               used to calculate linearly constrained steps heavily relies  on
               good scaling of input problems.
               
    NOTE: linear  (non-box)  constraints  are  satisfied only approximately  -
          there  always  exists some violation due  to  numerical  errors  and
          algorithmic limitations.

    NOTE: general linear constraints  add  significant  overhead  to  solution
          process. Although solver performs roughly same amount of  iterations
          (when compared  with  similar  box-only  constrained  problem), each
          iteration   now    involves  solution  of  linearly  constrained  QP
          subproblem, which requires ~3-5 times more Cholesky  decompositions.
          Thus, if you can reformulate your problem in such way  this  it  has
          only box constraints, it may be beneficial to do so.

      -- ALGLIB --
         Copyright 29.04.2017 by Bochkanov Sergey
    *************************************************************************/
    public static void lsfitsetlc(lsfitstate state,
        double[,] c,
        int[] ct,
        int k,
        xparams _params)
    {
        int i = 0;
        int n = 0;
        int i_ = 0;

        n = state.k;

        //
        // First, check for errors in the inputs
        //
        ap.assert(k >= 0, "LSFitSetLC: K<0");
        ap.assert(ap.cols(c) >= n + 1 || k == 0, "LSFitSetLC: Cols(C)<N+1");
        ap.assert(ap.rows(c) >= k, "LSFitSetLC: Rows(C)<K");
        ap.assert(ap.len(ct) >= k, "LSFitSetLC: Length(CT)<K");
        ap.assert(apserv.apservisfinitematrix(c, k, n + 1, _params), "LSFitSetLC: C contains infinite or NaN values!");

        //
        // Handle zero K
        //
        if (k == 0)
        {
            state.nec = 0;
            state.nic = 0;
            return;
        }

        //
        // Equality constraints are stored first, in the upper
        // NEC rows of State.CLEIC matrix. Inequality constraints
        // are stored in the next NIC rows.
        //
        // NOTE: we convert inequality constraints to the form
        // A*x<=b before copying them.
        //
        apserv.rmatrixsetlengthatleast(ref state.cleic, k, n + 1, _params);
        state.nec = 0;
        state.nic = 0;
        for (i = 0; i <= k - 1; i++)
        {
            if (ct[i] == 0)
            {
                for (i_ = 0; i_ <= n; i_++)
                {
                    state.cleic[state.nec, i_] = c[i, i_];
                }
                state.nec = state.nec + 1;
            }
        }
        for (i = 0; i <= k - 1; i++)
        {
            if (ct[i] != 0)
            {
                if (ct[i] > 0)
                {
                    for (i_ = 0; i_ <= n; i_++)
                    {
                        state.cleic[state.nec + state.nic, i_] = -c[i, i_];
                    }
                }
                else
                {
                    for (i_ = 0; i_ <= n; i_++)
                    {
                        state.cleic[state.nec + state.nic, i_] = c[i, i_];
                    }
                }
                state.nic = state.nic + 1;
            }
        }
    }


    /*************************************************************************
    NOTES:

    1. this algorithm is somewhat unusual because it works with  parameterized
       function f(C,X), where X is a function argument (we  have  many  points
       which are characterized by different  argument  values),  and  C  is  a
       parameter to fit.

       For example, if we want to do linear fit by f(c0,c1,x) = c0*x+c1,  then
       x will be argument, and {c0,c1} will be parameters.
       
       It is important to understand that this algorithm finds minimum in  the
       space of function PARAMETERS (not arguments), so it  needs  derivatives
       of f() with respect to C, not X.
       
       In the example above it will need f=c0*x+c1 and {df/dc0,df/dc1} = {x,1}
       instead of {df/dx} = {c0}.

    2. Callback functions accept C as the first parameter, and X as the second

    3. If  state  was  created  with  LSFitCreateFG(),  algorithm  needs  just
       function   and   its   gradient,   but   if   state   was  created with
       LSFitCreateFGH(), algorithm will need function, gradient and Hessian.
       
       According  to  the  said  above,  there  ase  several  versions of this
       function, which accept different sets of callbacks.
       
       This flexibility opens way to subtle errors - you may create state with
       LSFitCreateFGH() (optimization using Hessian), but call function  which
       does not accept Hessian. So when algorithm will request Hessian,  there
       will be no callback to call. In this case exception will be thrown.
       
       Be careful to avoid such errors because there is no way to find them at
       compile time - you can see them at runtime only.

      -- ALGLIB --
         Copyright 17.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static bool lsfititeration(lsfitstate state,
        xparams _params)
    {
        bool result = new bool();
        double lx = 0;
        double lf = 0;
        double ld = 0;
        double rx = 0;
        double rf = 0;
        double rd = 0;
        int n = 0;
        int m = 0;
        int k = 0;
        double v = 0;
        double vv = 0;
        double relcnt = 0;
        int i = 0;
        int j = 0;
        int j1 = 0;
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
            k = state.rstate.ia[2];
            i = state.rstate.ia[3];
            j = state.rstate.ia[4];
            j1 = state.rstate.ia[5];
            lx = state.rstate.ra[0];
            lf = state.rstate.ra[1];
            ld = state.rstate.ra[2];
            rx = state.rstate.ra[3];
            rf = state.rstate.ra[4];
            rd = state.rstate.ra[5];
            v = state.rstate.ra[6];
            vv = state.rstate.ra[7];
            relcnt = state.rstate.ra[8];
        }
        else
        {
            n = 359;
            m = -58;
            k = -919;
            i = -909;
            j = 81;
            j1 = 255;
            lx = 74.0;
            lf = -788.0;
            ld = 809.0;
            rx = 205.0;
            rf = -838.0;
            rd = 939.0;
            v = -526.0;
            vv = 763.0;
            relcnt = -541.0;
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
        if (state.rstate.stage == 4)
        {
            goto lbl_4;
        }
        if (state.rstate.stage == 5)
        {
            goto lbl_5;
        }
        if (state.rstate.stage == 6)
        {
            goto lbl_6;
        }
        if (state.rstate.stage == 7)
        {
            goto lbl_7;
        }
        if (state.rstate.stage == 8)
        {
            goto lbl_8;
        }
        if (state.rstate.stage == 9)
        {
            goto lbl_9;
        }
        if (state.rstate.stage == 10)
        {
            goto lbl_10;
        }
        if (state.rstate.stage == 11)
        {
            goto lbl_11;
        }
        if (state.rstate.stage == 12)
        {
            goto lbl_12;
        }
        if (state.rstate.stage == 13)
        {
            goto lbl_13;
        }

        //
        // Routine body
        //

        //
        // Init
        //
        if (state.wkind == 1)
        {
            ap.assert(state.npoints == state.nweights, "LSFitFit: number of points is not equal to the number of weights");
        }
        state.repvaridx = -1;
        n = state.npoints;
        m = state.m;
        k = state.k;
        apserv.ivectorsetlengthatleast(ref state.tmpct, state.nec + state.nic, _params);
        for (i = 0; i <= state.nec - 1; i++)
        {
            state.tmpct[i] = 0;
        }
        for (i = 0; i <= state.nic - 1; i++)
        {
            state.tmpct[state.nec + i] = -1;
        }
        minlm.minlmsetcond(state.optstate, state.epsx, state.maxits, _params);
        minlm.minlmsetstpmax(state.optstate, state.stpmax, _params);
        minlm.minlmsetxrep(state.optstate, state.xrep, _params);
        minlm.minlmsetscale(state.optstate, state.s, _params);
        minlm.minlmsetbc(state.optstate, state.bndl, state.bndu, _params);
        minlm.minlmsetlc(state.optstate, state.cleic, state.tmpct, state.nec + state.nic, _params);

        //
        //  Check that user-supplied gradient is correct
        //
        lsfitclearrequestfields(state, _params);
        if (!((double)(state.teststep) > (double)(0) && state.optalgo == 1))
        {
            goto lbl_14;
        }
        for (i = 0; i <= k - 1; i++)
        {
            state.c[i] = state.c0[i];
            if (math.isfinite(state.bndl[i]))
            {
                state.c[i] = Math.Max(state.c[i], state.bndl[i]);
            }
            if (math.isfinite(state.bndu[i]))
            {
                state.c[i] = Math.Min(state.c[i], state.bndu[i]);
            }
        }
        state.needfg = true;
        i = 0;
    lbl_16:
        if (i > k - 1)
        {
            goto lbl_18;
        }
        ap.assert((double)(state.bndl[i]) <= (double)(state.c[i]) && (double)(state.c[i]) <= (double)(state.bndu[i]), "LSFitIteration: internal error(State.C is out of bounds)");
        v = state.c[i];
        j = 0;
    lbl_19:
        if (j > n - 1)
        {
            goto lbl_21;
        }
        for (i_ = 0; i_ <= m - 1; i_++)
        {
            state.x[i_] = state.taskx[j, i_];
        }
        state.c[i] = v - state.teststep * state.s[i];
        if (math.isfinite(state.bndl[i]))
        {
            state.c[i] = Math.Max(state.c[i], state.bndl[i]);
        }
        lx = state.c[i];
        state.rstate.stage = 0;
        goto lbl_rcomm;
    lbl_0:
        lf = state.f;
        ld = state.g[i];
        state.c[i] = v + state.teststep * state.s[i];
        if (math.isfinite(state.bndu[i]))
        {
            state.c[i] = Math.Min(state.c[i], state.bndu[i]);
        }
        rx = state.c[i];
        state.rstate.stage = 1;
        goto lbl_rcomm;
    lbl_1:
        rf = state.f;
        rd = state.g[i];
        state.c[i] = (lx + rx) / 2;
        if (math.isfinite(state.bndl[i]))
        {
            state.c[i] = Math.Max(state.c[i], state.bndl[i]);
        }
        if (math.isfinite(state.bndu[i]))
        {
            state.c[i] = Math.Min(state.c[i], state.bndu[i]);
        }
        state.rstate.stage = 2;
        goto lbl_rcomm;
    lbl_2:
        state.c[i] = v;
        if (!optserv.derivativecheck(lf, ld, rf, rd, state.f, state.g[i], rx - lx, _params))
        {
            state.repvaridx = i;
            state.repterminationtype = -7;
            result = false;
            return result;
        }
        j = j + 1;
        goto lbl_19;
    lbl_21:
        i = i + 1;
        goto lbl_16;
    lbl_18:
        state.needfg = false;
    lbl_14:

        //
        // Fill WCur by weights:
        // * for WKind=0 unit weights are chosen
        // * for WKind=1 we use user-supplied weights stored in State.TaskW
        //
        apserv.rvectorsetlengthatleast(ref state.wcur, n, _params);
        for (i = 0; i <= n - 1; i++)
        {
            state.wcur[i] = 1.0;
            if (state.wkind == 1)
            {
                state.wcur[i] = state.taskw[i];
            }
        }

    //
    // Optimize
    //
    lbl_22:
        if (!minlm.minlmiteration(state.optstate, _params))
        {
            goto lbl_23;
        }
        if (!state.optstate.needfi)
        {
            goto lbl_24;
        }

        //
        // calculate f[] = wi*(f(xi,c)-yi)
        //
        i = 0;
    lbl_26:
        if (i > n - 1)
        {
            goto lbl_28;
        }
        for (i_ = 0; i_ <= k - 1; i_++)
        {
            state.c[i_] = state.optstate.x[i_];
        }
        for (i_ = 0; i_ <= m - 1; i_++)
        {
            state.x[i_] = state.taskx[i, i_];
        }
        state.pointindex = i;
        lsfitclearrequestfields(state, _params);
        state.needf = true;
        state.rstate.stage = 3;
        goto lbl_rcomm;
    lbl_3:
        state.needf = false;
        vv = state.wcur[i];
        state.optstate.fi[i] = vv * (state.f - state.tasky[i]);
        i = i + 1;
        goto lbl_26;
    lbl_28:
        goto lbl_22;
    lbl_24:
        if (!state.optstate.needf)
        {
            goto lbl_29;
        }

        //
        // calculate F = sum (wi*(f(xi,c)-yi))^2
        //
        state.optstate.f = 0;
        i = 0;
    lbl_31:
        if (i > n - 1)
        {
            goto lbl_33;
        }
        for (i_ = 0; i_ <= k - 1; i_++)
        {
            state.c[i_] = state.optstate.x[i_];
        }
        for (i_ = 0; i_ <= m - 1; i_++)
        {
            state.x[i_] = state.taskx[i, i_];
        }
        state.pointindex = i;
        lsfitclearrequestfields(state, _params);
        state.needf = true;
        state.rstate.stage = 4;
        goto lbl_rcomm;
    lbl_4:
        state.needf = false;
        vv = state.wcur[i];
        state.optstate.f = state.optstate.f + math.sqr(vv * (state.f - state.tasky[i]));
        i = i + 1;
        goto lbl_31;
    lbl_33:
        goto lbl_22;
    lbl_29:
        if (!state.optstate.needfg)
        {
            goto lbl_34;
        }

        //
        // calculate F/gradF
        //
        state.optstate.f = 0;
        for (i = 0; i <= k - 1; i++)
        {
            state.optstate.g[i] = 0;
        }
        i = 0;
    lbl_36:
        if (i > n - 1)
        {
            goto lbl_38;
        }
        for (i_ = 0; i_ <= k - 1; i_++)
        {
            state.c[i_] = state.optstate.x[i_];
        }
        for (i_ = 0; i_ <= m - 1; i_++)
        {
            state.x[i_] = state.taskx[i, i_];
        }
        state.pointindex = i;
        lsfitclearrequestfields(state, _params);
        state.needfg = true;
        state.rstate.stage = 5;
        goto lbl_rcomm;
    lbl_5:
        state.needfg = false;
        vv = state.wcur[i];
        state.optstate.f = state.optstate.f + math.sqr(vv * (state.f - state.tasky[i]));
        v = math.sqr(vv) * 2 * (state.f - state.tasky[i]);
        for (i_ = 0; i_ <= k - 1; i_++)
        {
            state.optstate.g[i_] = state.optstate.g[i_] + v * state.g[i_];
        }
        i = i + 1;
        goto lbl_36;
    lbl_38:
        goto lbl_22;
    lbl_34:
        if (!state.optstate.needfij)
        {
            goto lbl_39;
        }

        //
        // calculate Fi/jac(Fi)
        //
        i = 0;
    lbl_41:
        if (i > n - 1)
        {
            goto lbl_43;
        }
        for (i_ = 0; i_ <= k - 1; i_++)
        {
            state.c[i_] = state.optstate.x[i_];
        }
        for (i_ = 0; i_ <= m - 1; i_++)
        {
            state.x[i_] = state.taskx[i, i_];
        }
        state.pointindex = i;
        lsfitclearrequestfields(state, _params);
        state.needfg = true;
        state.rstate.stage = 6;
        goto lbl_rcomm;
    lbl_6:
        state.needfg = false;
        vv = state.wcur[i];
        state.optstate.fi[i] = vv * (state.f - state.tasky[i]);
        for (i_ = 0; i_ <= k - 1; i_++)
        {
            state.optstate.j[i, i_] = vv * state.g[i_];
        }
        i = i + 1;
        goto lbl_41;
    lbl_43:
        goto lbl_22;
    lbl_39:
        if (!state.optstate.needfgh)
        {
            goto lbl_44;
        }

        //
        // calculate F/grad(F)/hess(F)
        //
        state.optstate.f = 0;
        for (i = 0; i <= k - 1; i++)
        {
            state.optstate.g[i] = 0;
        }
        for (i = 0; i <= k - 1; i++)
        {
            for (j = 0; j <= k - 1; j++)
            {
                state.optstate.h[i, j] = 0;
            }
        }
        i = 0;
    lbl_46:
        if (i > n - 1)
        {
            goto lbl_48;
        }
        for (i_ = 0; i_ <= k - 1; i_++)
        {
            state.c[i_] = state.optstate.x[i_];
        }
        for (i_ = 0; i_ <= m - 1; i_++)
        {
            state.x[i_] = state.taskx[i, i_];
        }
        state.pointindex = i;
        lsfitclearrequestfields(state, _params);
        state.needfgh = true;
        state.rstate.stage = 7;
        goto lbl_rcomm;
    lbl_7:
        state.needfgh = false;
        vv = state.wcur[i];
        state.optstate.f = state.optstate.f + math.sqr(vv * (state.f - state.tasky[i]));
        v = math.sqr(vv) * 2 * (state.f - state.tasky[i]);
        for (i_ = 0; i_ <= k - 1; i_++)
        {
            state.optstate.g[i_] = state.optstate.g[i_] + v * state.g[i_];
        }
        for (j = 0; j <= k - 1; j++)
        {
            v = 2 * math.sqr(vv) * state.g[j];
            for (i_ = 0; i_ <= k - 1; i_++)
            {
                state.optstate.h[j, i_] = state.optstate.h[j, i_] + v * state.g[i_];
            }
            v = 2 * math.sqr(vv) * (state.f - state.tasky[i]);
            for (i_ = 0; i_ <= k - 1; i_++)
            {
                state.optstate.h[j, i_] = state.optstate.h[j, i_] + v * state.h[j, i_];
            }
        }
        i = i + 1;
        goto lbl_46;
    lbl_48:
        goto lbl_22;
    lbl_44:
        if (!state.optstate.xupdated)
        {
            goto lbl_49;
        }

        //
        // Report new iteration
        //
        for (i_ = 0; i_ <= k - 1; i_++)
        {
            state.c[i_] = state.optstate.x[i_];
        }
        state.f = state.optstate.f;
        lsfitclearrequestfields(state, _params);
        state.xupdated = true;
        state.rstate.stage = 8;
        goto lbl_rcomm;
    lbl_8:
        state.xupdated = false;
        goto lbl_22;
    lbl_49:
        goto lbl_22;
    lbl_23:

        //
        // Extract results
        //
        // NOTE: reverse communication protocol used by this unit does NOT
        //       allow us to reallocate State.C[] array. Thus, we extract
        //       results to the temporary variable in order to avoid possible
        //       reallocation.
        //
        minlm.minlmresults(state.optstate, ref state.c1, state.optrep, _params);
        state.repterminationtype = state.optrep.terminationtype;
        state.repiterationscount = state.optrep.iterationscount;

        //
        // calculate errors
        //
        if (state.repterminationtype <= 0)
        {
            goto lbl_51;
        }

        //
        // Calculate RMS/Avg/Max/... errors
        //
        state.reprmserror = 0;
        state.repwrmserror = 0;
        state.repavgerror = 0;
        state.repavgrelerror = 0;
        state.repmaxerror = 0;
        relcnt = 0;
        i = 0;
    lbl_53:
        if (i > n - 1)
        {
            goto lbl_55;
        }
        for (i_ = 0; i_ <= k - 1; i_++)
        {
            state.c[i_] = state.c1[i_];
        }
        for (i_ = 0; i_ <= m - 1; i_++)
        {
            state.x[i_] = state.taskx[i, i_];
        }
        state.pointindex = i;
        lsfitclearrequestfields(state, _params);
        state.needf = true;
        state.rstate.stage = 9;
        goto lbl_rcomm;
    lbl_9:
        state.needf = false;
        v = state.f;
        vv = state.wcur[i];
        state.reprmserror = state.reprmserror + math.sqr(v - state.tasky[i]);
        state.repwrmserror = state.repwrmserror + math.sqr(vv * (v - state.tasky[i]));
        state.repavgerror = state.repavgerror + Math.Abs(v - state.tasky[i]);
        if ((double)(state.tasky[i]) != (double)(0))
        {
            state.repavgrelerror = state.repavgrelerror + Math.Abs(v - state.tasky[i]) / Math.Abs(state.tasky[i]);
            relcnt = relcnt + 1;
        }
        state.repmaxerror = Math.Max(state.repmaxerror, Math.Abs(v - state.tasky[i]));
        i = i + 1;
        goto lbl_53;
    lbl_55:
        state.reprmserror = Math.Sqrt(state.reprmserror / n);
        state.repwrmserror = Math.Sqrt(state.repwrmserror / n);
        state.repavgerror = state.repavgerror / n;
        if ((double)(relcnt) != (double)(0))
        {
            state.repavgrelerror = state.repavgrelerror / relcnt;
        }

        //
        // Calculate covariance matrix
        //
        apserv.rmatrixsetlengthatleast(ref state.tmpjac, n, k, _params);
        apserv.rvectorsetlengthatleast(ref state.tmpf, n, _params);
        apserv.rvectorsetlengthatleast(ref state.tmp, k, _params);
        if ((double)(state.diffstep) <= (double)(0))
        {
            goto lbl_56;
        }

        //
        // Compute Jacobian by means of numerical differentiation
        //
        lsfitclearrequestfields(state, _params);
        state.needf = true;
        i = 0;
    lbl_58:
        if (i > n - 1)
        {
            goto lbl_60;
        }
        for (i_ = 0; i_ <= m - 1; i_++)
        {
            state.x[i_] = state.taskx[i, i_];
        }
        state.pointindex = i;
        state.rstate.stage = 10;
        goto lbl_rcomm;
    lbl_10:
        state.tmpf[i] = state.f;
        j = 0;
    lbl_61:
        if (j > k - 1)
        {
            goto lbl_63;
        }
        v = state.c[j];
        lx = v - state.diffstep * state.s[j];
        state.c[j] = lx;
        if (math.isfinite(state.bndl[j]))
        {
            state.c[j] = Math.Max(state.c[j], state.bndl[j]);
        }
        state.rstate.stage = 11;
        goto lbl_rcomm;
    lbl_11:
        lf = state.f;
        rx = v + state.diffstep * state.s[j];
        state.c[j] = rx;
        if (math.isfinite(state.bndu[j]))
        {
            state.c[j] = Math.Min(state.c[j], state.bndu[j]);
        }
        state.rstate.stage = 12;
        goto lbl_rcomm;
    lbl_12:
        rf = state.f;
        state.c[j] = v;
        if ((double)(rx) != (double)(lx))
        {
            state.tmpjac[i, j] = (rf - lf) / (rx - lx);
        }
        else
        {
            state.tmpjac[i, j] = 0;
        }
        j = j + 1;
        goto lbl_61;
    lbl_63:
        i = i + 1;
        goto lbl_58;
    lbl_60:
        state.needf = false;
        goto lbl_57;
    lbl_56:

        //
        // Jacobian is calculated with user-provided analytic gradient
        //
        lsfitclearrequestfields(state, _params);
        state.needfg = true;
        i = 0;
    lbl_64:
        if (i > n - 1)
        {
            goto lbl_66;
        }
        for (i_ = 0; i_ <= m - 1; i_++)
        {
            state.x[i_] = state.taskx[i, i_];
        }
        state.pointindex = i;
        state.rstate.stage = 13;
        goto lbl_rcomm;
    lbl_13:
        state.tmpf[i] = state.f;
        for (j = 0; j <= k - 1; j++)
        {
            state.tmpjac[i, j] = state.g[j];
        }
        i = i + 1;
        goto lbl_64;
    lbl_66:
        state.needfg = false;
    lbl_57:
        for (i = 0; i <= k - 1; i++)
        {
            state.tmp[i] = 0.0;
        }
        estimateerrors(state.tmpjac, state.tmpf, state.tasky, state.wcur, state.tmp, state.s, n, k, state.rep, ref state.tmpjacw, 0, _params);
    lbl_51:
        result = false;
        return result;

    //
    // Saving state
    //
    lbl_rcomm:
        result = true;
        state.rstate.ia[0] = n;
        state.rstate.ia[1] = m;
        state.rstate.ia[2] = k;
        state.rstate.ia[3] = i;
        state.rstate.ia[4] = j;
        state.rstate.ia[5] = j1;
        state.rstate.ra[0] = lx;
        state.rstate.ra[1] = lf;
        state.rstate.ra[2] = ld;
        state.rstate.ra[3] = rx;
        state.rstate.ra[4] = rf;
        state.rstate.ra[5] = rd;
        state.rstate.ra[6] = v;
        state.rstate.ra[7] = vv;
        state.rstate.ra[8] = relcnt;
        return result;
    }


    /*************************************************************************
    Nonlinear least squares fitting results.

    Called after return from LSFitFit().

    INPUT PARAMETERS:
        State   -   algorithm state

    OUTPUT PARAMETERS:
        C       -   array[K], solution
        Rep     -   optimization report. On success following fields are set:
                    * TerminationType   completion code:
                        * -8    optimizer   detected  NAN/INF  in  the  target
                                function and/or gradient
                        * -7    gradient verification failed.
                                See LSFitSetGradientCheck() for more information.
                        * -3    inconsistent constraints
                        *  2    relative step is no more than EpsX.
                        *  5    MaxIts steps was taken
                        *  7    stopping conditions are too stringent,
                                further improvement is impossible
                    * R2                non-adjusted coefficient of determination
                                        (non-weighted)
                    * RMSError          rms error on the (X,Y).
                    * AvgError          average error on the (X,Y).
                    * AvgRelError       average relative error on the non-zero Y
                    * MaxError          maximum error
                                        NON-WEIGHTED ERRORS ARE CALCULATED
                    * WRMSError         weighted rms error on the (X,Y).
                    
    ERRORS IN PARAMETERS                
                    
    This  solver  also  calculates different kinds of errors in parameters and
    fills corresponding fields of report:
    * Rep.CovPar        covariance matrix for parameters, array[K,K].
    * Rep.ErrPar        errors in parameters, array[K],
                        errpar = sqrt(diag(CovPar))
    * Rep.ErrCurve      vector of fit errors - standard deviations of empirical
                        best-fit curve from "ideal" best-fit curve built  with
                        infinite number of samples, array[N].
                        errcurve = sqrt(diag(J*CovPar*J')),
                        where J is Jacobian matrix.
    * Rep.Noise         vector of per-point estimates of noise, array[N]

    IMPORTANT:  errors  in  parameters  are  calculated  without  taking  into
                account boundary/linear constraints! Presence  of  constraints
                changes distribution of errors, but there is no  easy  way  to
                account for constraints when you calculate covariance matrix.
                
    NOTE:       noise in the data is estimated as follows:
                * for fitting without user-supplied  weights  all  points  are
                  assumed to have same level of noise, which is estimated from
                  the data
                * for fitting with user-supplied weights we assume that  noise
                  level in I-th point is inversely proportional to Ith weight.
                  Coefficient of proportionality is estimated from the data.
                
    NOTE:       we apply small amount of regularization when we invert squared
                Jacobian and calculate covariance matrix. It  guarantees  that
                algorithm won't divide by zero  during  inversion,  but  skews
                error estimates a bit (fractional error is about 10^-9).
                
                However, we believe that this difference is insignificant  for
                all practical purposes except for the situation when you  want
                to compare ALGLIB results with "reference"  implementation  up
                to the last significant digit.
                
    NOTE:       covariance matrix is estimated using  correction  for  degrees
                of freedom (covariances are divided by N-M instead of dividing
                by N).

      -- ALGLIB --
         Copyright 17.08.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void lsfitresults(lsfitstate state,
        ref double[] c,
        lsfitreport rep,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int i_ = 0;

        c = new double[0];

        clearreport(rep, _params);
        rep.terminationtype = state.repterminationtype;
        rep.varidx = state.repvaridx;
        if (rep.terminationtype > 0)
        {
            c = new double[state.k];
            for (i_ = 0; i_ <= state.k - 1; i_++)
            {
                c[i_] = state.c1[i_];
            }
            rep.rmserror = state.reprmserror;
            rep.wrmserror = state.repwrmserror;
            rep.avgerror = state.repavgerror;
            rep.avgrelerror = state.repavgrelerror;
            rep.maxerror = state.repmaxerror;
            rep.iterationscount = state.repiterationscount;
            rep.covpar = new double[state.k, state.k];
            rep.errpar = new double[state.k];
            rep.errcurve = new double[state.npoints];
            rep.noise = new double[state.npoints];
            rep.r2 = state.rep.r2;
            for (i = 0; i <= state.k - 1; i++)
            {
                for (j = 0; j <= state.k - 1; j++)
                {
                    rep.covpar[i, j] = state.rep.covpar[i, j];
                }
                rep.errpar[i] = state.rep.errpar[i];
            }
            for (i = 0; i <= state.npoints - 1; i++)
            {
                rep.errcurve[i] = state.rep.errcurve[i];
                rep.noise[i] = state.rep.noise[i];
            }
        }
    }


    /*************************************************************************
    This  subroutine  turns  on  verification  of  the  user-supplied analytic
    gradient:
    * user calls this subroutine before fitting begins
    * LSFitFit() is called
    * prior to actual fitting, for  each  point  in  data  set  X_i  and  each
      component  of  parameters  being  fited C_j algorithm performs following
      steps:
      * two trial steps are made to C_j-TestStep*S[j] and C_j+TestStep*S[j],
        where C_j is j-th parameter and S[j] is a scale of j-th parameter
      * if needed, steps are bounded with respect to constraints on C[]
      * F(X_i|C) is evaluated at these trial points
      * we perform one more evaluation in the middle point of the interval
      * we  build  cubic  model using function values and derivatives at trial
        points and we compare its prediction with actual value in  the  middle
        point
      * in case difference between prediction and actual value is higher  than
        some predetermined threshold, algorithm stops with completion code -7;
        Rep.VarIdx is set to index of the parameter with incorrect derivative.
    * after verification is over, algorithm proceeds to the actual optimization.

    NOTE 1: verification needs N*K (points count * parameters count)  gradient
            evaluations. It is very costly and you should use it only for  low
            dimensional  problems,  when  you  want  to  be  sure  that you've
            correctly calculated analytic derivatives. You should not  use  it
            in the production code  (unless  you  want  to  check  derivatives
            provided by some third party).

    NOTE 2: you  should  carefully  choose  TestStep. Value which is too large
            (so large that function behaviour is significantly non-cubic) will
            lead to false alarms. You may use  different  step  for  different
            parameters by means of setting scale with LSFitSetScale().

    NOTE 3: this function may lead to false positives. In case it reports that
            I-th  derivative was calculated incorrectly, you may decrease test
            step  and  try  one  more  time  - maybe your function changes too
            sharply  and  your  step  is  too  large for such rapidly chanding
            function.

    NOTE 4: this function works only for optimizers created with LSFitCreateWFG()
            or LSFitCreateFG() constructors.
            
    INPUT PARAMETERS:
        State       -   structure used to store algorithm state
        TestStep    -   verification step:
                        * TestStep=0 turns verification off
                        * TestStep>0 activates verification

      -- ALGLIB --
         Copyright 15.06.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void lsfitsetgradientcheck(lsfitstate state,
        double teststep,
        xparams _params)
    {
        ap.assert(math.isfinite(teststep), "LSFitSetGradientCheck: TestStep contains NaN or Infinite");
        ap.assert((double)(teststep) >= (double)(0), "LSFitSetGradientCheck: invalid argument TestStep(TestStep<0)");
        state.teststep = teststep;
    }


    /*************************************************************************
    This function analyzes section of curve for processing by RDP algorithm:
    given set of points X,Y with indexes [I0,I1] it returns point with
    worst deviation from linear model (non-parametric version which sees curve
    as Y(x)).

    Input parameters:
        X, Y        -   SORTED arrays.
        I0,I1       -   interval (boundaries included) to process
        Eps         -   desired precision
        
    OUTPUT PARAMETERS:
        WorstIdx    -   index of worst point
        WorstError  -   error at worst point
        
    NOTE: this function guarantees that it returns exactly zero for a section
          with less than 3 points.

      -- ALGLIB PROJECT --
         Copyright 02.10.2014 by Bochkanov Sergey
    *************************************************************************/
    private static void rdpanalyzesection(double[] x,
        double[] y,
        int i0,
        int i1,
        ref int worstidx,
        ref double worsterror,
        xparams _params)
    {
        int i = 0;
        double xleft = 0;
        double xright = 0;
        double vx = 0;
        double ve = 0;
        double a = 0;
        double b = 0;

        worstidx = 0;
        worsterror = 0;

        xleft = x[i0];
        xright = x[i1];
        if (i1 - i0 + 1 < 3 || (double)(xright) == (double)(xleft))
        {
            worstidx = i0;
            worsterror = 0.0;
            return;
        }
        a = (y[i1] - y[i0]) / (xright - xleft);
        b = (y[i0] * xright - y[i1] * xleft) / (xright - xleft);
        worstidx = -1;
        worsterror = 0;
        for (i = i0 + 1; i <= i1 - 1; i++)
        {
            vx = x[i];
            ve = Math.Abs(a * vx + b - y[i]);
            if (((double)(vx) > (double)(xleft) && (double)(vx) < (double)(xright)) && (double)(ve) > (double)(worsterror))
            {
                worsterror = ve;
                worstidx = i;
            }
        }
    }


    /*************************************************************************
    Recursive splitting of interval [I0,I1] (right boundary included) with RDP
    algorithm (non-parametric version which sees curve as Y(x)).

    Input parameters:
        X, Y        -   SORTED arrays.
        I0,I1       -   interval (boundaries included) to process
        Eps         -   desired precision
        XOut,YOut   -   preallocated output arrays large enough to store result;
                        XOut[0..1], YOut[0..1] contain first and last points of
                        curve
        NOut        -   must contain 2 on input
        
    OUTPUT PARAMETERS:
        XOut, YOut  -   curve generated by RDP algorithm, UNSORTED
        NOut        -   number of points in curve

      -- ALGLIB PROJECT --
         Copyright 02.10.2014 by Bochkanov Sergey
    *************************************************************************/
    private static void rdprecursive(double[] x,
        double[] y,
        int i0,
        int i1,
        double eps,
        double[] xout,
        double[] yout,
        ref int nout,
        xparams _params)
    {
        int worstidx = 0;
        double worsterror = 0;

        ap.assert((double)(eps) > (double)(0), "RDPRecursive: internal error, Eps<0");
        rdpanalyzesection(x, y, i0, i1, ref worstidx, ref worsterror, _params);
        if ((double)(worsterror) <= (double)(eps))
        {
            return;
        }
        xout[nout] = x[worstidx];
        yout[nout] = y[worstidx];
        nout = nout + 1;
        if (worstidx - i0 < i1 - worstidx)
        {
            rdprecursive(x, y, i0, worstidx, eps, xout, yout, ref nout, _params);
            rdprecursive(x, y, worstidx, i1, eps, xout, yout, ref nout, _params);
        }
        else
        {
            rdprecursive(x, y, worstidx, i1, eps, xout, yout, ref nout, _params);
            rdprecursive(x, y, i0, worstidx, eps, xout, yout, ref nout, _params);
        }
    }


    /*************************************************************************
    Internal 4PL/5PL fitting function.

    Accepts X, Y and already initialized and prepared MinLMState structure.
    On input P1 contains initial guess, on output it contains solution.  FLast
    stores function value at P1.
    *************************************************************************/
    private static void logisticfitinternal(double[] x,
        double[] y,
        int n,
        bool is4pl,
        double lambdav,
        minlm.minlmstate state,
        minlm.minlmreport replm,
        ref double[] p1,
        ref double flast,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        double ta = 0;
        double tb = 0;
        double tc = 0;
        double td = 0;
        double tg = 0;
        double vp0 = 0;
        double vp1 = 0;

        flast = 0;

        minlm.minlmrestartfrom(state, p1, _params);
        while (minlm.minlmiteration(state, _params))
        {
            ta = state.x[0];
            tb = state.x[1];
            tc = state.x[2];
            td = state.x[3];
            tg = state.x[4];
            if (state.xupdated)
            {

                //
                // Save best function value obtained so far.
                //
                flast = state.f;
                continue;
            }
            if (state.needfi || state.needfij)
            {

                //
                // Function vector and Jacobian
                //
                for (i = 0; i <= n - 1; i++)
                {
                    ap.assert((double)(x[i]) >= (double)(0), "LogisticFitInternal: integrity error");

                    //
                    // Handle zero X
                    //
                    if ((double)(x[i]) == (double)(0))
                    {
                        if ((double)(tb) >= (double)(0))
                        {

                            //
                            // Positive or zero TB, limit X^TB subject to X->+0 is equal to zero.
                            //
                            state.fi[i] = ta - y[i];
                            if (state.needfij)
                            {
                                state.j[i, 0] = 1;
                                state.j[i, 1] = 0;
                                state.j[i, 2] = 0;
                                state.j[i, 3] = 0;
                                state.j[i, 4] = 0;
                            }
                        }
                        else
                        {

                            //
                            // Negative TB, limit X^TB subject to X->+0 is equal to +INF.
                            //
                            state.fi[i] = td - y[i];
                            if (state.needfij)
                            {
                                state.j[i, 0] = 0;
                                state.j[i, 1] = 0;
                                state.j[i, 2] = 0;
                                state.j[i, 3] = 1;
                                state.j[i, 4] = 0;
                            }
                        }
                        continue;
                    }

                    //
                    // Positive X.
                    // Prepare VP0/VP1, it may become infinite or nearly overflow in some rare cases,
                    // handle these cases
                    //
                    vp0 = Math.Pow(x[i] / tc, tb);
                    if (is4pl)
                    {
                        vp1 = 1 + vp0;
                    }
                    else
                    {
                        vp1 = Math.Pow(1 + vp0, tg);
                    }
                    if ((!math.isfinite(vp1) || (double)(vp0) > (double)(1.0E50)) || (double)(vp1) > (double)(1.0E50))
                    {

                        //
                        // VP0/VP1 are not finite, assume that it is +INF or -INF
                        //
                        state.fi[i] = td - y[i];
                        if (state.needfij)
                        {
                            state.j[i, 0] = 0;
                            state.j[i, 1] = 0;
                            state.j[i, 2] = 0;
                            state.j[i, 3] = 1;
                            state.j[i, 4] = 0;
                        }
                        continue;
                    }

                    //
                    // VP0/VP1 are finite, normal processing
                    //
                    if (is4pl)
                    {
                        state.fi[i] = td + (ta - td) / vp1 - y[i];
                        if (state.needfij)
                        {
                            state.j[i, 0] = 1 / vp1;
                            state.j[i, 1] = -((ta - td) * vp0 * Math.Log(x[i] / tc) / math.sqr(vp1));
                            state.j[i, 2] = (ta - td) * (tb / tc) * vp0 / math.sqr(vp1);
                            state.j[i, 3] = 1 - 1 / vp1;
                            state.j[i, 4] = 0;
                        }
                    }
                    else
                    {
                        state.fi[i] = td + (ta - td) / vp1 - y[i];
                        if (state.needfij)
                        {
                            state.j[i, 0] = 1 / vp1;
                            state.j[i, 1] = (ta - td) * -tg * Math.Pow(1 + vp0, -tg - 1) * vp0 * Math.Log(x[i] / tc);
                            state.j[i, 2] = (ta - td) * -tg * Math.Pow(1 + vp0, -tg - 1) * vp0 * -(tb / tc);
                            state.j[i, 3] = 1 - 1 / vp1;
                            state.j[i, 4] = -((ta - td) / vp1 * Math.Log(1 + vp0));
                        }
                    }
                }

                //
                // Add regularizer
                //
                for (i = 0; i <= 4; i++)
                {
                    state.fi[n + i] = lambdav * state.x[i];
                    if (state.needfij)
                    {
                        for (j = 0; j <= 4; j++)
                        {
                            state.j[n + i, j] = 0.0;
                        }
                        state.j[n + i, i] = lambdav;
                    }
                }

                //
                // Done
                //
                continue;
            }
            ap.assert(false, "LogisticFitX: internal error");
        }
        minlm.minlmresultsbuf(state, ref p1, replm, _params);
        ap.assert(replm.terminationtype > 0, "LogisticFitX: internal error");
    }


    /*************************************************************************
    Calculate errors for 4PL/5PL fit.
    Leaves other fields of Rep unchanged, so caller should properly initialize
    it with ClearRep() call.

      -- ALGLIB PROJECT --
         Copyright 28.04.2017 by Bochkanov Sergey
    *************************************************************************/
    private static void logisticfit45errors(double[] x,
        double[] y,
        int n,
        double a,
        double b,
        double c,
        double d,
        double g,
        lsfitreport rep,
        xparams _params)
    {
        int i = 0;
        int k = 0;
        double v = 0;
        double rss = 0;
        double tss = 0;
        double meany = 0;


        //
        // Calculate errors
        //
        rep.rmserror = 0;
        rep.avgerror = 0;
        rep.avgrelerror = 0;
        rep.maxerror = 0;
        k = 0;
        rss = 0.0;
        tss = 0.0;
        meany = 0.0;
        for (i = 0; i <= n - 1; i++)
        {
            meany = meany + y[i];
        }
        meany = meany / n;
        for (i = 0; i <= n - 1; i++)
        {

            //
            // Calculate residual from regression
            //
            if ((double)(x[i]) > (double)(0))
            {
                v = d + (a - d) / Math.Pow(1.0 + Math.Pow(x[i] / c, b), g) - y[i];
            }
            else
            {
                if ((double)(b) >= (double)(0))
                {
                    v = a - y[i];
                }
                else
                {
                    v = d - y[i];
                }
            }

            //
            // Update RSS (residual sum of squares) and TSS (total sum of squares)
            // which are used to calculate coefficient of determination.
            //
            // NOTE: we use formula R2 = 1-RSS/TSS because it has nice property of
            //       being equal to 0.0 if and only if model perfectly fits data.
            //
            //       When we fit nonlinear models, there are exist multiple ways of
            //       determining R2, each of them giving different results. Formula
            //       above is the most intuitive one.
            //
            rss = rss + v * v;
            tss = tss + math.sqr(y[i] - meany);

            //
            // Update errors
            //
            rep.rmserror = rep.rmserror + math.sqr(v);
            rep.avgerror = rep.avgerror + Math.Abs(v);
            if ((double)(y[i]) != (double)(0))
            {
                rep.avgrelerror = rep.avgrelerror + Math.Abs(v / y[i]);
                k = k + 1;
            }
            rep.maxerror = Math.Max(rep.maxerror, Math.Abs(v));
        }
        rep.rmserror = Math.Sqrt(rep.rmserror / n);
        rep.avgerror = rep.avgerror / n;
        if (k > 0)
        {
            rep.avgrelerror = rep.avgrelerror / k;
        }
        rep.r2 = 1.0 - rss / tss;
    }


    /*************************************************************************
    Internal spline fitting subroutine

      -- ALGLIB PROJECT --
         Copyright 08.09.2009 by Bochkanov Sergey
    *************************************************************************/
    private static void spline1dfitinternal(int st,
        double[] x,
        double[] y,
        double[] w,
        int n,
        double[] xc,
        double[] yc,
        int[] dc,
        int k,
        int m,
        spline1d.spline1dinterpolant s,
        spline1d.spline1dfitreport rep,
        xparams _params)
    {
        double[,] fmatrix = new double[0, 0];
        double[,] cmatrix = new double[0, 0];
        double[] y2 = new double[0];
        double[] w2 = new double[0];
        double[] sx = new double[0];
        double[] sy = new double[0];
        double[] sd = new double[0];
        double[] tmp = new double[0];
        double[] xoriginal = new double[0];
        double[] yoriginal = new double[0];
        lsfitreport lrep = new lsfitreport();
        double v0 = 0;
        double v1 = 0;
        double v2 = 0;
        double mx = 0;
        spline1d.spline1dinterpolant s2 = new spline1d.spline1dinterpolant();
        int i = 0;
        int j = 0;
        int relcnt = 0;
        double xa = 0;
        double xb = 0;
        double sa = 0;
        double sb = 0;
        double bl = 0;
        double br = 0;
        double decay = 0;
        int i_ = 0;

        x = (double[])x.Clone();
        y = (double[])y.Clone();
        w = (double[])w.Clone();
        xc = (double[])xc.Clone();
        yc = (double[])yc.Clone();

        ap.assert(st == 0 || st == 1, "Spline1DFit: internal error!");
        if (st == 0 && m < 4)
        {
            ap.assert(false, "LSFIT: integrity check 5729 failed");
            return;
        }
        if (st == 1 && m < 4)
        {
            ap.assert(false, "LSFIT: integrity check 6229 failed");
            return;
        }
        if ((n < 1 || k < 0) || k >= m)
        {
            ap.assert(false, "LSFIT: integrity check 6729 failed");
            return;
        }
        for (i = 0; i <= k - 1; i++)
        {
            if (dc[i] < 0)
            {
                ap.assert(false, "LSFIT: integrity check 7329 failed");
            }
            if (dc[i] > 1)
            {
                ap.assert(false, "LSFIT: integrity check 7529 failed");
            }
        }
        if (st == 1 && m % 2 != 0)
        {

            //
            // Hermite fitter must have even number of basis functions
            //
            ap.assert(false, "LSFIT: integrity check 8229 failed");
            return;
        }

        //
        // weight decay for correct handling of task which becomes
        // degenerate after constraints are applied
        //
        decay = 10000 * math.machineepsilon;

        //
        // Scale X, Y, XC, YC
        //
        intfitserv.lsfitscalexy(ref x, ref y, ref w, n, ref xc, ref yc, dc, k, ref xa, ref xb, ref sa, ref sb, ref xoriginal, ref yoriginal, _params);

        //
        // allocate space, initialize:
        // * SX     -   grid for basis functions
        // * SY     -   values of basis functions at grid points
        // * FMatrix-   values of basis functions at X[]
        // * CMatrix-   values (derivatives) of basis functions at XC[]
        //
        y2 = new double[n + m];
        w2 = new double[n + m];
        fmatrix = new double[n + m, m];
        if (k > 0)
        {
            cmatrix = new double[k, m + 1];
        }
        if (st == 0)
        {

            //
            // allocate space for cubic spline
            //
            sx = new double[m - 2];
            sy = new double[m - 2];
            for (j = 0; j <= m - 2 - 1; j++)
            {
                sx[j] = (double)(2 * j) / (double)(m - 2 - 1) - 1;
            }
        }
        if (st == 1)
        {

            //
            // allocate space for Hermite spline
            //
            sx = new double[m / 2];
            sy = new double[m / 2];
            sd = new double[m / 2];
            for (j = 0; j <= m / 2 - 1; j++)
            {
                sx[j] = (double)(2 * j) / (double)(m / 2 - 1) - 1;
            }
        }

        //
        // Prepare design and constraints matrices:
        // * fill constraints matrix
        // * fill first N rows of design matrix with values
        // * fill next M rows of design matrix with regularizing term
        // * append M zeros to Y
        // * append M elements, mean(abs(W)) each, to W
        //
        for (j = 0; j <= m - 1; j++)
        {

            //
            // prepare Jth basis function
            //
            if (st == 0)
            {

                //
                // cubic spline basis
                //
                for (i = 0; i <= m - 2 - 1; i++)
                {
                    sy[i] = 0;
                }
                bl = 0;
                br = 0;
                if (j < m - 2)
                {
                    sy[j] = 1;
                }
                if (j == m - 2)
                {
                    bl = 1;
                }
                if (j == m - 1)
                {
                    br = 1;
                }
                spline1d.spline1dbuildcubic(sx, sy, m - 2, 1, bl, 1, br, s2, _params);
            }
            if (st == 1)
            {

                //
                // Hermite basis
                //
                for (i = 0; i <= m / 2 - 1; i++)
                {
                    sy[i] = 0;
                    sd[i] = 0;
                }
                if (j % 2 == 0)
                {
                    sy[j / 2] = 1;
                }
                else
                {
                    sd[j / 2] = 1;
                }
                spline1d.spline1dbuildhermite(sx, sy, sd, m / 2, s2, _params);
            }

            //
            // values at X[], XC[]
            //
            for (i = 0; i <= n - 1; i++)
            {
                fmatrix[i, j] = spline1d.spline1dcalc(s2, x[i], _params);
            }
            for (i = 0; i <= k - 1; i++)
            {
                ap.assert(dc[i] >= 0 && dc[i] <= 2, "Spline1DFit: internal error!");
                spline1d.spline1ddiff(s2, xc[i], ref v0, ref v1, ref v2, _params);
                if (dc[i] == 0)
                {
                    cmatrix[i, j] = v0;
                }
                if (dc[i] == 1)
                {
                    cmatrix[i, j] = v1;
                }
                if (dc[i] == 2)
                {
                    cmatrix[i, j] = v2;
                }
            }
        }
        for (i = 0; i <= k - 1; i++)
        {
            cmatrix[i, m] = yc[i];
        }
        for (i = 0; i <= m - 1; i++)
        {
            for (j = 0; j <= m - 1; j++)
            {
                if (i == j)
                {
                    fmatrix[n + i, j] = decay;
                }
                else
                {
                    fmatrix[n + i, j] = 0;
                }
            }
        }
        y2 = new double[n + m];
        w2 = new double[n + m];
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            y2[i_] = y[i_];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            w2[i_] = w[i_];
        }
        mx = 0;
        for (i = 0; i <= n - 1; i++)
        {
            mx = mx + Math.Abs(w[i]);
        }
        mx = mx / n;
        for (i = 0; i <= m - 1; i++)
        {
            y2[n + i] = 0;
            w2[n + i] = mx;
        }

        //
        // Solve constrained task
        //
        if (k > 0)
        {

            //
            // solve using regularization
            //
            lsfitlinearwc(y2, w2, fmatrix, cmatrix, n + m, m, k, ref tmp, lrep, _params);
        }
        else
        {

            //
            // no constraints, no regularization needed
            //
            lsfitlinearwc(y, w, fmatrix, cmatrix, n, m, k, ref tmp, lrep, _params);
        }
        rep.terminationtype = lrep.terminationtype;
        if (rep.terminationtype < 0)
        {
            return;
        }

        //
        // Generate spline and scale it
        //
        if (st == 0)
        {

            //
            // cubic spline basis
            //
            for (i_ = 0; i_ <= m - 2 - 1; i_++)
            {
                sy[i_] = tmp[i_];
            }
            spline1d.spline1dbuildcubic(sx, sy, m - 2, 1, tmp[m - 2], 1, tmp[m - 1], s, _params);
        }
        if (st == 1)
        {

            //
            // Hermite basis
            //
            for (i = 0; i <= m / 2 - 1; i++)
            {
                sy[i] = tmp[2 * i];
                sd[i] = tmp[2 * i + 1];
            }
            spline1d.spline1dbuildhermite(sx, sy, sd, m / 2, s, _params);
        }
        spline1d.spline1dlintransx(s, 2 / (xb - xa), -((xa + xb) / (xb - xa)), _params);
        spline1d.spline1dlintransy(s, sb - sa, sa, _params);

        //
        // Scale absolute errors obtained from LSFitLinearW.
        // Relative error should be calculated separately
        // (because of shifting/scaling of the task)
        //
        rep.taskrcond = lrep.taskrcond;
        rep.rmserror = lrep.rmserror * (sb - sa);
        rep.avgerror = lrep.avgerror * (sb - sa);
        rep.maxerror = lrep.maxerror * (sb - sa);
        rep.avgrelerror = 0;
        relcnt = 0;
        for (i = 0; i <= n - 1; i++)
        {
            if ((double)(yoriginal[i]) != (double)(0))
            {
                rep.avgrelerror = rep.avgrelerror + Math.Abs(spline1d.spline1dcalc(s, xoriginal[i], _params) - yoriginal[i]) / Math.Abs(yoriginal[i]);
                relcnt = relcnt + 1;
            }
        }
        if (relcnt != 0)
        {
            rep.avgrelerror = rep.avgrelerror / relcnt;
        }
    }


    /*************************************************************************
    Internal fitting subroutine
    *************************************************************************/
    private static void lsfitlinearinternal(double[] y,
        double[] w,
        double[,] fmatrix,
        int n,
        int m,
        ref double[] c,
        lsfitreport rep,
        xparams _params)
    {
        double threshold = 0;
        double[,] ft = new double[0, 0];
        double[,] q = new double[0, 0];
        double[,] l = new double[0, 0];
        double[,] r = new double[0, 0];
        double[] b = new double[0];
        double[] wmod = new double[0];
        double[] tau = new double[0];
        double[] nzeros = new double[0];
        double[] s = new double[0];
        int i = 0;
        int j = 0;
        double v = 0;
        double[] sv = new double[0];
        double[,] u = new double[0, 0];
        double[,] vt = new double[0, 0];
        double[] tmp = new double[0];
        double[] utb = new double[0];
        double[] sutb = new double[0];
        int relcnt = 0;
        int i_ = 0;

        c = new double[0];

        clearreport(rep, _params);
        ap.assert(!(n < 1 || m < 1), "LSFIT: integrity check 2508 failed");
        rep.terminationtype = 1;
        threshold = Math.Sqrt(math.machineepsilon);

        //
        // Degenerate case, needs special handling
        //
        if (n < m)
        {

            //
            // Create design matrix.
            //
            ft = new double[n, m];
            b = new double[n];
            wmod = new double[n];
            for (j = 0; j <= n - 1; j++)
            {
                v = w[j];
                for (i_ = 0; i_ <= m - 1; i_++)
                {
                    ft[j, i_] = v * fmatrix[j, i_];
                }
                b[j] = w[j] * y[j];
                wmod[j] = 1;
            }

            //
            // LQ decomposition and reduction to M=N
            //
            c = new double[m];
            for (i = 0; i <= m - 1; i++)
            {
                c[i] = 0;
            }
            rep.taskrcond = 0;
            ortfac.rmatrixlq(ft, n, m, ref tau, _params);
            ortfac.rmatrixlqunpackq(ft, n, m, tau, n, ref q, _params);
            ortfac.rmatrixlqunpackl(ft, n, m, ref l, _params);
            lsfitlinearinternal(b, wmod, l, n, n, ref tmp, rep, _params);
            if (rep.terminationtype <= 0)
            {
                return;
            }
            for (i = 0; i <= n - 1; i++)
            {
                v = tmp[i];
                for (i_ = 0; i_ <= m - 1; i_++)
                {
                    c[i_] = c[i_] + v * q[i, i_];
                }
            }
            return;
        }

        //
        // N>=M. Generate design matrix and reduce to N=M using
        // QR decomposition.
        //
        ft = new double[n, m];
        b = new double[n];
        for (j = 0; j <= n - 1; j++)
        {
            v = w[j];
            for (i_ = 0; i_ <= m - 1; i_++)
            {
                ft[j, i_] = v * fmatrix[j, i_];
            }
            b[j] = w[j] * y[j];
        }
        ortfac.rmatrixqr(ft, n, m, ref tau, _params);
        ortfac.rmatrixqrunpackq(ft, n, m, tau, m, ref q, _params);
        ortfac.rmatrixqrunpackr(ft, n, m, ref r, _params);
        tmp = new double[m];
        for (i = 0; i <= m - 1; i++)
        {
            tmp[i] = 0;
        }
        for (i = 0; i <= n - 1; i++)
        {
            v = b[i];
            for (i_ = 0; i_ <= m - 1; i_++)
            {
                tmp[i_] = tmp[i_] + v * q[i, i_];
            }
        }
        b = new double[m];
        for (i_ = 0; i_ <= m - 1; i_++)
        {
            b[i_] = tmp[i_];
        }

        //
        // R contains reduced MxM design upper triangular matrix,
        // B contains reduced Mx1 right part.
        //
        // Determine system condition number and decide
        // should we use triangular solver (faster) or
        // SVD-based solver (more stable).
        //
        // We can use LU-based RCond estimator for this task.
        //
        rep.taskrcond = rcond.rmatrixlurcondinf(r, m, _params);
        if ((double)(rep.taskrcond) > (double)(threshold))
        {

            //
            // use QR-based solver
            //
            c = new double[m];
            c[m - 1] = b[m - 1] / r[m - 1, m - 1];
            for (i = m - 2; i >= 0; i--)
            {
                v = 0.0;
                for (i_ = i + 1; i_ <= m - 1; i_++)
                {
                    v += r[i, i_] * c[i_];
                }
                c[i] = (b[i] - v) / r[i, i];
            }
        }
        else
        {

            //
            // use SVD-based solver
            //
            if (!svd.rmatrixsvd(r, m, m, 1, 1, 2, ref sv, ref u, ref vt, _params))
            {
                ap.assert(false, "LSFitLinearXX: critical failure - internal SVD solver failed");
                return;
            }
            utb = new double[m];
            sutb = new double[m];
            for (i = 0; i <= m - 1; i++)
            {
                utb[i] = 0;
            }
            for (i = 0; i <= m - 1; i++)
            {
                v = b[i];
                for (i_ = 0; i_ <= m - 1; i_++)
                {
                    utb[i_] = utb[i_] + v * u[i, i_];
                }
            }
            if ((double)(sv[0]) > (double)(0))
            {
                rep.taskrcond = sv[m - 1] / sv[0];
                for (i = 0; i <= m - 1; i++)
                {
                    if ((double)(sv[i]) > (double)(threshold * sv[0]))
                    {
                        sutb[i] = utb[i] / sv[i];
                    }
                    else
                    {
                        sutb[i] = 0;
                    }
                }
            }
            else
            {
                rep.taskrcond = 0;
                for (i = 0; i <= m - 1; i++)
                {
                    sutb[i] = 0;
                }
            }
            c = new double[m];
            for (i = 0; i <= m - 1; i++)
            {
                c[i] = 0;
            }
            for (i = 0; i <= m - 1; i++)
            {
                v = sutb[i];
                for (i_ = 0; i_ <= m - 1; i_++)
                {
                    c[i_] = c[i_] + v * vt[i, i_];
                }
            }
        }

        //
        // calculate errors
        //
        rep.rmserror = 0;
        rep.avgerror = 0;
        rep.avgrelerror = 0;
        rep.maxerror = 0;
        relcnt = 0;
        for (i = 0; i <= n - 1; i++)
        {
            v = 0.0;
            for (i_ = 0; i_ <= m - 1; i_++)
            {
                v += fmatrix[i, i_] * c[i_];
            }
            rep.rmserror = rep.rmserror + math.sqr(v - y[i]);
            rep.avgerror = rep.avgerror + Math.Abs(v - y[i]);
            if ((double)(y[i]) != (double)(0))
            {
                rep.avgrelerror = rep.avgrelerror + Math.Abs(v - y[i]) / Math.Abs(y[i]);
                relcnt = relcnt + 1;
            }
            rep.maxerror = Math.Max(rep.maxerror, Math.Abs(v - y[i]));
        }
        rep.rmserror = Math.Sqrt(rep.rmserror / n);
        rep.avgerror = rep.avgerror / n;
        if (relcnt != 0)
        {
            rep.avgrelerror = rep.avgrelerror / relcnt;
        }
        nzeros = new double[n];
        s = new double[m];
        for (i = 0; i <= m - 1; i++)
        {
            s[i] = 0;
        }
        for (i = 0; i <= n - 1; i++)
        {
            for (j = 0; j <= m - 1; j++)
            {
                s[j] = s[j] + math.sqr(fmatrix[i, j]);
            }
            nzeros[i] = 0;
        }
        for (i = 0; i <= m - 1; i++)
        {
            if ((double)(s[i]) != (double)(0))
            {
                s[i] = Math.Sqrt(1 / s[i]);
            }
            else
            {
                s[i] = 1;
            }
        }
        estimateerrors(fmatrix, nzeros, y, w, c, s, n, m, rep, ref r, 1, _params);
    }


    /*************************************************************************
    Internal subroutine
    *************************************************************************/
    private static void lsfitclearrequestfields(lsfitstate state,
        xparams _params)
    {
        state.needf = false;
        state.needfg = false;
        state.needfgh = false;
        state.xupdated = false;
    }


    /*************************************************************************
    Internal subroutine, calculates barycentric basis functions.
    Used for efficient simultaneous calculation of N basis functions.

      -- ALGLIB --
         Copyright 17.08.2009 by Bochkanov Sergey
    *************************************************************************/
    private static void barycentriccalcbasis(ratint.barycentricinterpolant b,
        double t,
        ref double[] y,
        xparams _params)
    {
        double s2 = 0;
        double s = 0;
        double v = 0;
        int i = 0;
        int j = 0;
        int i_ = 0;


        //
        // special case: N=1
        //
        if (b.n == 1)
        {
            y[0] = 1;
            return;
        }

        //
        // Here we assume that task is normalized, i.e.:
        // 1. abs(Y[i])<=1
        // 2. abs(W[i])<=1
        // 3. X[] is ordered
        //
        // First, we decide: should we use "safe" formula (guarded
        // against overflow) or fast one?
        //
        s = Math.Abs(t - b.x[0]);
        for (i = 0; i <= b.n - 1; i++)
        {
            v = b.x[i];
            if ((double)(v) == (double)(t))
            {
                for (j = 0; j <= b.n - 1; j++)
                {
                    y[j] = 0;
                }
                y[i] = 1;
                return;
            }
            v = Math.Abs(t - v);
            if ((double)(v) < (double)(s))
            {
                s = v;
            }
        }
        s2 = 0;
        for (i = 0; i <= b.n - 1; i++)
        {
            v = s / (t - b.x[i]);
            v = v * b.w[i];
            y[i] = v;
            s2 = s2 + v;
        }
        v = 1 / s2;
        for (i_ = 0; i_ <= b.n - 1; i_++)
        {
            y[i_] = v * y[i_];
        }
    }


    /*************************************************************************
    This is internal function for Chebyshev fitting.

    It assumes that input data are normalized:
    * X/XC belong to [-1,+1],
    * mean(Y)=0, stddev(Y)=1.

    It does not checks inputs for errors.

    This function is used to fit general (shifted) Chebyshev models, power
    basis models or barycentric models.

    INPUT PARAMETERS:
        X   -   points, array[0..N-1].
        Y   -   function values, array[0..N-1].
        W   -   weights, array[0..N-1]
        N   -   number of points, N>0.
        XC  -   points where polynomial values/derivatives are constrained,
                array[0..K-1].
        YC  -   values of constraints, array[0..K-1]
        DC  -   array[0..K-1], types of constraints:
                * DC[i]=0   means that P(XC[i])=YC[i]
                * DC[i]=1   means that P'(XC[i])=YC[i]
        K   -   number of constraints, 0<=K<M.
                K=0 means no constraints (XC/YC/DC are not used in such cases)
        M   -   number of basis functions (= polynomial_degree + 1), M>=1

    OUTPUT PARAMETERS:
        C   -   interpolant in Chebyshev form; [-1,+1] is used as base interval
        Rep -   report, same format as in LSFitLinearW() subroutine.
                Following fields are set:
                * TerminationType
                * RMSError      rms error on the (X,Y).
                * AvgError      average error on the (X,Y).
                * AvgRelError   average relative error on the non-zero Y
                * MaxError      maximum error
                                NON-WEIGHTED ERRORS ARE CALCULATED

    IMPORTANT:
        this subroitine doesn't calculate task's condition number for K<>0.

      -- ALGLIB PROJECT --
         Copyright 10.12.2009 by Bochkanov Sergey
    *************************************************************************/
    private static void internalchebyshevfit(double[] x,
        double[] y,
        double[] w,
        int n,
        double[] xc,
        double[] yc,
        int[] dc,
        int k,
        int m,
        ref double[] c,
        lsfitreport rep,
        xparams _params)
    {
        double[] y2 = new double[0];
        double[] w2 = new double[0];
        double[] tmp = new double[0];
        double[] tmp2 = new double[0];
        double[] tmpdiff = new double[0];
        double[] bx = new double[0];
        double[] by = new double[0];
        double[] bw = new double[0];
        double[,] fmatrix = new double[0, 0];
        double[,] cmatrix = new double[0, 0];
        int i = 0;
        int j = 0;
        double mx = 0;
        double decay = 0;
        int i_ = 0;

        xc = (double[])xc.Clone();
        yc = (double[])yc.Clone();
        c = new double[0];

        clearreport(rep, _params);

        //
        // weight decay for correct handling of task which becomes
        // degenerate after constraints are applied
        //
        decay = 10000 * math.machineepsilon;

        //
        // allocate space, initialize/fill:
        // * FMatrix-   values of basis functions at X[]
        // * CMatrix-   values (derivatives) of basis functions at XC[]
        // * fill constraints matrix
        // * fill first N rows of design matrix with values
        // * fill next M rows of design matrix with regularizing term
        // * append M zeros to Y
        // * append M elements, mean(abs(W)) each, to W
        //
        y2 = new double[n + m];
        w2 = new double[n + m];
        tmp = new double[m];
        tmpdiff = new double[m];
        fmatrix = new double[n + m, m];
        if (k > 0)
        {
            cmatrix = new double[k, m + 1];
        }

        //
        // Fill design matrix, Y2, W2:
        // * first N rows with basis functions for original points
        // * next M rows with decay terms
        //
        for (i = 0; i <= n - 1; i++)
        {

            //
            // prepare Ith row
            // use Tmp for calculations to avoid multidimensional arrays overhead
            //
            for (j = 0; j <= m - 1; j++)
            {
                if (j == 0)
                {
                    tmp[j] = 1;
                }
                else
                {
                    if (j == 1)
                    {
                        tmp[j] = x[i];
                    }
                    else
                    {
                        tmp[j] = 2 * x[i] * tmp[j - 1] - tmp[j - 2];
                    }
                }
            }
            for (i_ = 0; i_ <= m - 1; i_++)
            {
                fmatrix[i, i_] = tmp[i_];
            }
        }
        for (i = 0; i <= m - 1; i++)
        {
            for (j = 0; j <= m - 1; j++)
            {
                if (i == j)
                {
                    fmatrix[n + i, j] = decay;
                }
                else
                {
                    fmatrix[n + i, j] = 0;
                }
            }
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            y2[i_] = y[i_];
        }
        for (i_ = 0; i_ <= n - 1; i_++)
        {
            w2[i_] = w[i_];
        }
        mx = 0;
        for (i = 0; i <= n - 1; i++)
        {
            mx = mx + Math.Abs(w[i]);
        }
        mx = mx / n;
        for (i = 0; i <= m - 1; i++)
        {
            y2[n + i] = 0;
            w2[n + i] = mx;
        }

        //
        // fill constraints matrix
        //
        for (i = 0; i <= k - 1; i++)
        {

            //
            // prepare Ith row
            // use Tmp for basis function values,
            // TmpDiff for basos function derivatives
            //
            for (j = 0; j <= m - 1; j++)
            {
                if (j == 0)
                {
                    tmp[j] = 1;
                    tmpdiff[j] = 0;
                }
                else
                {
                    if (j == 1)
                    {
                        tmp[j] = xc[i];
                        tmpdiff[j] = 1;
                    }
                    else
                    {
                        tmp[j] = 2 * xc[i] * tmp[j - 1] - tmp[j - 2];
                        tmpdiff[j] = 2 * (tmp[j - 1] + xc[i] * tmpdiff[j - 1]) - tmpdiff[j - 2];
                    }
                }
            }
            if (dc[i] == 0)
            {
                for (i_ = 0; i_ <= m - 1; i_++)
                {
                    cmatrix[i, i_] = tmp[i_];
                }
            }
            if (dc[i] == 1)
            {
                for (i_ = 0; i_ <= m - 1; i_++)
                {
                    cmatrix[i, i_] = tmpdiff[i_];
                }
            }
            cmatrix[i, m] = yc[i];
        }

        //
        // Solve constrained task
        //
        if (k > 0)
        {

            //
            // solve using regularization
            //
            lsfitlinearwc(y2, w2, fmatrix, cmatrix, n + m, m, k, ref c, rep, _params);
        }
        else
        {

            //
            // no constraints, no regularization needed
            //
            lsfitlinearwc(y, w, fmatrix, cmatrix, n, m, 0, ref c, rep, _params);
        }
    }


    /*************************************************************************
    Internal Floater-Hormann fitting subroutine for fixed D
    *************************************************************************/
    private static void barycentricfitwcfixedd(double[] x,
        double[] y,
        double[] w,
        int n,
        double[] xc,
        double[] yc,
        int[] dc,
        int k,
        int m,
        int d,
        ref int info,
        ratint.barycentricinterpolant b,
        barycentricfitreport rep,
        xparams _params)
    {
        double[,] fmatrix = new double[0, 0];
        double[,] cmatrix = new double[0, 0];
        double[] y2 = new double[0];
        double[] w2 = new double[0];
        double[] sx = new double[0];
        double[] sy = new double[0];
        double[] sbf = new double[0];
        double[] xoriginal = new double[0];
        double[] yoriginal = new double[0];
        double[] tmp = new double[0];
        lsfitreport lrep = new lsfitreport();
        double v0 = 0;
        double v1 = 0;
        double mx = 0;
        ratint.barycentricinterpolant b2 = new ratint.barycentricinterpolant();
        int i = 0;
        int j = 0;
        int relcnt = 0;
        double xa = 0;
        double xb = 0;
        double sa = 0;
        double sb = 0;
        double decay = 0;
        int i_ = 0;

        x = (double[])x.Clone();
        y = (double[])y.Clone();
        w = (double[])w.Clone();
        xc = (double[])xc.Clone();
        yc = (double[])yc.Clone();
        info = 0;

        if (((n < 1 || m < 2) || k < 0) || k >= m)
        {
            info = -1;
            return;
        }
        for (i = 0; i <= k - 1; i++)
        {
            info = 0;
            if (dc[i] < 0)
            {
                info = -1;
            }
            if (dc[i] > 1)
            {
                info = -1;
            }
            if (info < 0)
            {
                return;
            }
        }

        //
        // weight decay for correct handling of task which becomes
        // degenerate after constraints are applied
        //
        decay = 10000 * math.machineepsilon;

        //
        // Scale X, Y, XC, YC
        //
        intfitserv.lsfitscalexy(ref x, ref y, ref w, n, ref xc, ref yc, dc, k, ref xa, ref xb, ref sa, ref sb, ref xoriginal, ref yoriginal, _params);

        //
        // allocate space, initialize:
        // * FMatrix-   values of basis functions at X[]
        // * CMatrix-   values (derivatives) of basis functions at XC[]
        //
        y2 = new double[n + m];
        w2 = new double[n + m];
        fmatrix = new double[n + m, m];
        if (k > 0)
        {
            cmatrix = new double[k, m + 1];
        }
        y2 = new double[n + m];
        w2 = new double[n + m];

        //
        // Prepare design and constraints matrices:
        // * fill constraints matrix
        // * fill first N rows of design matrix with values
        // * fill next M rows of design matrix with regularizing term
        // * append M zeros to Y
        // * append M elements, mean(abs(W)) each, to W
        //
        sx = new double[m];
        sy = new double[m];
        sbf = new double[m];
        for (j = 0; j <= m - 1; j++)
        {
            sx[j] = (double)(2 * j) / (double)(m - 1) - 1;
        }
        for (i = 0; i <= m - 1; i++)
        {
            sy[i] = 1;
        }
        ratint.barycentricbuildfloaterhormann(sx, sy, m, d, b2, _params);
        mx = 0;
        for (i = 0; i <= n - 1; i++)
        {
            barycentriccalcbasis(b2, x[i], ref sbf, _params);
            for (i_ = 0; i_ <= m - 1; i_++)
            {
                fmatrix[i, i_] = sbf[i_];
            }
            y2[i] = y[i];
            w2[i] = w[i];
            mx = mx + Math.Abs(w[i]) / n;
        }
        for (i = 0; i <= m - 1; i++)
        {
            for (j = 0; j <= m - 1; j++)
            {
                if (i == j)
                {
                    fmatrix[n + i, j] = decay;
                }
                else
                {
                    fmatrix[n + i, j] = 0;
                }
            }
            y2[n + i] = 0;
            w2[n + i] = mx;
        }
        if (k > 0)
        {
            for (j = 0; j <= m - 1; j++)
            {
                for (i = 0; i <= m - 1; i++)
                {
                    sy[i] = 0;
                }
                sy[j] = 1;
                ratint.barycentricbuildfloaterhormann(sx, sy, m, d, b2, _params);
                for (i = 0; i <= k - 1; i++)
                {
                    ap.assert(dc[i] >= 0 && dc[i] <= 1, "BarycentricFit: internal error!");
                    ratint.barycentricdiff1(b2, xc[i], ref v0, ref v1, _params);
                    if (dc[i] == 0)
                    {
                        cmatrix[i, j] = v0;
                    }
                    if (dc[i] == 1)
                    {
                        cmatrix[i, j] = v1;
                    }
                }
            }
            for (i = 0; i <= k - 1; i++)
            {
                cmatrix[i, m] = yc[i];
            }
        }

        //
        // Solve constrained task
        //
        if (k > 0)
        {

            //
            // solve using regularization
            //
            lsfitlinearwc(y2, w2, fmatrix, cmatrix, n + m, m, k, ref tmp, lrep, _params);
        }
        else
        {

            //
            // no constraints, no regularization needed
            //
            lsfitlinearwc(y, w, fmatrix, cmatrix, n, m, k, ref tmp, lrep, _params);
        }
        info = lrep.terminationtype;
        if (info < 0)
        {
            return;
        }

        //
        // Generate interpolant and scale it
        //
        for (i_ = 0; i_ <= m - 1; i_++)
        {
            sy[i_] = tmp[i_];
        }
        ratint.barycentricbuildfloaterhormann(sx, sy, m, d, b, _params);
        ratint.barycentriclintransx(b, 2 / (xb - xa), -((xa + xb) / (xb - xa)), _params);
        ratint.barycentriclintransy(b, sb - sa, sa, _params);

        //
        // Scale absolute errors obtained from LSFitLinearW.
        // Relative error should be calculated separately
        // (because of shifting/scaling of the task)
        //
        rep.taskrcond = lrep.taskrcond;
        rep.rmserror = lrep.rmserror * (sb - sa);
        rep.avgerror = lrep.avgerror * (sb - sa);
        rep.maxerror = lrep.maxerror * (sb - sa);
        rep.avgrelerror = 0;
        relcnt = 0;
        for (i = 0; i <= n - 1; i++)
        {
            if ((double)(yoriginal[i]) != (double)(0))
            {
                rep.avgrelerror = rep.avgrelerror + Math.Abs(ratint.barycentriccalc(b, xoriginal[i], _params) - yoriginal[i]) / Math.Abs(yoriginal[i]);
                relcnt = relcnt + 1;
            }
        }
        if (relcnt != 0)
        {
            rep.avgrelerror = rep.avgrelerror / relcnt;
        }
    }


    private static void clearreport(lsfitreport rep,
        xparams _params)
    {
        rep.taskrcond = 0;
        rep.iterationscount = 0;
        rep.varidx = -1;
        rep.rmserror = 0;
        rep.avgerror = 0;
        rep.avgrelerror = 0;
        rep.maxerror = 0;
        rep.wrmserror = 0;
        rep.r2 = 0;
        rep.covpar = new double[0, 0];
        rep.errpar = new double[0];
        rep.errcurve = new double[0];
        rep.noise = new double[0];
    }


    /*************************************************************************
    This internal function estimates covariance matrix and other error-related
    information for linear/nonlinear least squares model.

    It has a bit awkward interface, but it can be used  for  both  linear  and
    nonlinear problems.

    INPUT PARAMETERS:
        F1  -   array[0..N-1,0..K-1]:
                * for linear problems - matrix of function values
                * for nonlinear problems - Jacobian matrix
        F0  -   array[0..N-1]:
                * for linear problems - must be filled with zeros
                * for nonlinear problems - must store values of function being
                  fitted
        Y   -   array[0..N-1]:
                * for linear and nonlinear problems - must store target values
        W   -   weights, array[0..N-1]:
                * for linear and nonlinear problems - weights
        X   -   array[0..K-1]:
                * for linear and nonlinear problems - current solution
        S   -   array[0..K-1]:
                * its components should be strictly positive
                * squared inverse of this diagonal matrix is used as damping
                  factor for covariance matrix (linear and nonlinear problems)
                * for nonlinear problems, when scale of the variables is usually
                  explicitly given by user, you may use scale vector for this
                  parameter
                * for linear problems you may set this parameter to
                  S=sqrt(1/diag(F'*F))
                * this parameter is automatically rescaled by this function,
                  only relative magnitudes of its components (with respect to
                  each other) matter.
        N   -   number of points, N>0.
        K   -   number of dimensions
        Rep -   structure which is used to store results
        Z   -   additional matrix which, depending on ZKind, may contain some
                information used to accelerate calculations - or just can be
                temporary buffer:
                * for ZKind=0       Z contains no information, just temporary
                                    buffer which can be resized and used as needed
                * for ZKind=1       Z contains triangular matrix from QR
                                    decomposition of W*F1. This matrix can be used
                                    to speedup calculation of covariance matrix.
                                    It should not be changed by algorithm.
        ZKind-  contents of Z

    OUTPUT PARAMETERS:

    * Rep.CovPar        covariance matrix for parameters, array[K,K].
    * Rep.ErrPar        errors in parameters, array[K],
                        errpar = sqrt(diag(CovPar))
    * Rep.ErrCurve      vector of fit errors - standard deviations of empirical
                        best-fit curve from "ideal" best-fit curve built  with
                        infinite number of samples, array[N].
                        errcurve = sqrt(diag(J*CovPar*J')),
                        where J is Jacobian matrix.
    * Rep.Noise         vector of per-point estimates of noise, array[N]
    * Rep.R2            coefficient of determination (non-weighted)

    Other fields of Rep are not changed.

    IMPORTANT:  errors  in  parameters  are  calculated  without  taking  into
                account boundary/linear constraints! Presence  of  constraints
                changes distribution of errors, but there is no  easy  way  to
                account for constraints when you calculate covariance matrix.
                
    NOTE:       noise in the data is estimated as follows:
                * for fitting without user-supplied  weights  all  points  are
                  assumed to have same level of noise, which is estimated from
                  the data
                * for fitting with user-supplied weights we assume that  noise
                  level in I-th point is inversely proportional to Ith weight.
                  Coefficient of proportionality is estimated from the data.
                
    NOTE:       we apply small amount of regularization when we invert squared
                Jacobian and calculate covariance matrix. It  guarantees  that
                algorithm won't divide by zero  during  inversion,  but  skews
                error estimates a bit (fractional error is about 10^-9).
                
                However, we believe that this difference is insignificant  for
                all practical purposes except for the situation when you  want
                to compare ALGLIB results with "reference"  implementation  up
                to the last significant digit.

      -- ALGLIB PROJECT --
         Copyright 10.12.2009 by Bochkanov Sergey
    *************************************************************************/
    private static void estimateerrors(double[,] f1,
        double[] f0,
        double[] y,
        double[] w,
        double[] x,
        double[] s,
        int n,
        int k,
        lsfitreport rep,
        ref double[,] z,
        int zkind,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int j1 = 0;
        double v = 0;
        double noisec = 0;
        matinv.matinvreport invrep = new matinv.matinvreport();
        int nzcnt = 0;
        double avg = 0;
        double rss = 0;
        double tss = 0;
        double sz = 0;
        double ss = 0;
        int i_ = 0;

        s = (double[])s.Clone();


        //
        // Compute NZCnt - count of non-zero weights
        //
        nzcnt = 0;
        for (i = 0; i <= n - 1; i++)
        {
            if ((double)(w[i]) != (double)(0))
            {
                nzcnt = nzcnt + 1;
            }
        }

        //
        // Compute R2
        //
        if (nzcnt > 0)
        {
            avg = 0.0;
            for (i = 0; i <= n - 1; i++)
            {
                if ((double)(w[i]) != (double)(0))
                {
                    avg = avg + y[i];
                }
            }
            avg = avg / nzcnt;
            rss = 0.0;
            tss = 0.0;
            for (i = 0; i <= n - 1; i++)
            {
                if ((double)(w[i]) != (double)(0))
                {
                    v = 0.0;
                    for (i_ = 0; i_ <= k - 1; i_++)
                    {
                        v += f1[i, i_] * x[i_];
                    }
                    v = v + f0[i];
                    rss = rss + math.sqr(v - y[i]);
                    tss = tss + math.sqr(y[i] - avg);
                }
            }
            if ((double)(tss) != (double)(0))
            {
                rep.r2 = Math.Max(1.0 - rss / tss, 0.0);
            }
            else
            {
                rep.r2 = 1.0;
            }
        }
        else
        {
            rep.r2 = 0;
        }

        //
        // Compute estimate of proportionality between noise in the data and weights:
        //     NoiseC = mean(per-point-noise*per-point-weight)
        // Noise level (standard deviation) at each point is equal to NoiseC/W[I].
        //
        if (nzcnt > k)
        {
            noisec = 0.0;
            for (i = 0; i <= n - 1; i++)
            {
                if ((double)(w[i]) != (double)(0))
                {
                    v = 0.0;
                    for (i_ = 0; i_ <= k - 1; i_++)
                    {
                        v += f1[i, i_] * x[i_];
                    }
                    v = v + f0[i];
                    noisec = noisec + math.sqr((v - y[i]) * w[i]);
                }
            }
            noisec = Math.Sqrt(noisec / (nzcnt - k));
        }
        else
        {
            noisec = 0.0;
        }

        //
        // Two branches on noise level:
        // * NoiseC>0   normal situation
        // * NoiseC=0   degenerate case CovPar is filled by zeros
        //
        apserv.rmatrixsetlengthatleast(ref rep.covpar, k, k, _params);
        if ((double)(noisec) > (double)(0))
        {

            //
            // Normal situation: non-zero noise level
            //
            ap.assert(zkind == 0 || zkind == 1, "LSFit: internal error in EstimateErrors() function");
            if (zkind == 0)
            {

                //
                // Z contains no additional information which can be used to speed up
                // calculations. We have to calculate covariance matrix on our own:
                // * Compute scaled Jacobian N*J, where N[i,i]=WCur[I]/NoiseC, store in Z
                // * Compute Z'*Z, store in CovPar
                // * Apply moderate regularization to CovPar and compute matrix inverse.
                //   In case inverse failed, increase regularization parameter and try
                //   again.
                //
                apserv.rmatrixsetlengthatleast(ref z, n, k, _params);
                for (i = 0; i <= n - 1; i++)
                {
                    v = w[i] / noisec;
                    for (i_ = 0; i_ <= k - 1; i_++)
                    {
                        z[i, i_] = v * f1[i, i_];
                    }
                }

                //
                // Convert S to automatically scaled damped matrix:
                // * calculate SZ - sum of diagonal elements of Z'*Z
                // * calculate SS - sum of diagonal elements of S^(-2)
                // * overwrite S by (SZ/SS)*S^(-2)
                // * now S has approximately same magnitude as giagonal of Z'*Z
                //
                sz = 0;
                for (i = 0; i <= n - 1; i++)
                {
                    for (j = 0; j <= k - 1; j++)
                    {
                        sz = sz + z[i, j] * z[i, j];
                    }
                }
                if ((double)(sz) == (double)(0))
                {
                    sz = 1;
                }
                ss = 0;
                for (j = 0; j <= k - 1; j++)
                {
                    ss = ss + 1 / math.sqr(s[j]);
                }
                for (j = 0; j <= k - 1; j++)
                {
                    s[j] = sz / ss / math.sqr(s[j]);
                }

                //
                // Calculate damped inverse inv(Z'*Z+S).
                // We increase damping factor V until Z'*Z become well-conditioned.
                //
                v = 1.0E3 * math.machineepsilon;
                do
                {
                    ablas.rmatrixsyrk(k, n, 1.0, z, 0, 0, 2, 0.0, rep.covpar, 0, 0, true, _params);
                    for (i = 0; i <= k - 1; i++)
                    {
                        rep.covpar[i, i] = rep.covpar[i, i] + v * s[i];
                    }
                    matinv.spdmatrixinverse(rep.covpar, k, true, invrep, _params);
                    v = 10 * v;
                }
                while (invrep.terminationtype <= 0);
                for (i = 0; i <= k - 1; i++)
                {
                    for (j = i + 1; j <= k - 1; j++)
                    {
                        rep.covpar[j, i] = rep.covpar[i, j];
                    }
                }
            }
            if (zkind == 1)
            {

                //
                // We can reuse additional information:
                // * Z contains R matrix from QR decomposition of W*F1 
                // * After multiplication by 1/NoiseC we get Z_mod = N*F1, where diag(N)=w[i]/NoiseC
                // * Such triangular Z_mod is a Cholesky factor from decomposition of J'*N'*N*J.
                //   Thus, we can calculate covariance matrix as inverse of the matrix given by
                //   its Cholesky decomposition. It allow us to avoid time-consuming calculation
                //   of J'*N'*N*J in CovPar - complexity is reduced from O(N*K^2) to O(K^3), which
                //   is quite good because K is usually orders of magnitude smaller than N.
                //
                // First, convert S to automatically scaled damped matrix:
                // * calculate SZ - sum of magnitudes of diagonal elements of Z/NoiseC
                // * calculate SS - sum of diagonal elements of S^(-1)
                // * overwrite S by (SZ/SS)*S^(-1)
                // * now S has approximately same magnitude as giagonal of Z'*Z
                //
                sz = 0;
                for (j = 0; j <= k - 1; j++)
                {
                    sz = sz + Math.Abs(z[j, j] / noisec);
                }
                if ((double)(sz) == (double)(0))
                {
                    sz = 1;
                }
                ss = 0;
                for (j = 0; j <= k - 1; j++)
                {
                    ss = ss + 1 / s[j];
                }
                for (j = 0; j <= k - 1; j++)
                {
                    s[j] = sz / ss / s[j];
                }

                //
                // Calculate damped inverse of inv((Z+v*S)'*(Z+v*S))
                // We increase damping factor V until matrix become well-conditioned.
                //
                v = 1.0E3 * math.machineepsilon;
                do
                {
                    for (i = 0; i <= k - 1; i++)
                    {
                        for (j = i; j <= k - 1; j++)
                        {
                            rep.covpar[i, j] = z[i, j] / noisec;
                        }
                        rep.covpar[i, i] = rep.covpar[i, i] + v * s[i];
                    }
                    matinv.spdmatrixcholeskyinverse(rep.covpar, k, true, invrep, _params);
                    v = 10 * v;
                }
                while (invrep.terminationtype <= 0);
                for (i = 0; i <= k - 1; i++)
                {
                    for (j = i + 1; j <= k - 1; j++)
                    {
                        rep.covpar[j, i] = rep.covpar[i, j];
                    }
                }
            }
        }
        else
        {

            //
            // Degenerate situation: zero noise level, covariance matrix is zero.
            //
            for (i = 0; i <= k - 1; i++)
            {
                for (j = 0; j <= k - 1; j++)
                {
                    rep.covpar[j, i] = 0;
                }
            }
        }

        //
        // Estimate erorrs in parameters, curve and per-point noise
        //
        apserv.rvectorsetlengthatleast(ref rep.errpar, k, _params);
        apserv.rvectorsetlengthatleast(ref rep.errcurve, n, _params);
        apserv.rvectorsetlengthatleast(ref rep.noise, n, _params);
        for (i = 0; i <= k - 1; i++)
        {
            rep.errpar[i] = Math.Sqrt(rep.covpar[i, i]);
        }
        for (i = 0; i <= n - 1; i++)
        {

            //
            // ErrCurve[I] is sqrt(P[i,i]) where P=J*CovPar*J'
            //
            v = 0.0;
            for (j = 0; j <= k - 1; j++)
            {
                for (j1 = 0; j1 <= k - 1; j1++)
                {
                    v = v + f1[i, j] * rep.covpar[j, j1] * f1[i, j1];
                }
            }
            rep.errcurve[i] = Math.Sqrt(v);

            //
            // Noise[i] is filled using weights and current estimate of noise level
            //
            if ((double)(w[i]) != (double)(0))
            {
                rep.noise[i] = noisec / w[i];
            }
            else
            {
                rep.noise[i] = 0;
            }
        }
    }


}
