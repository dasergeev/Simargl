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

public class mlptrain
{
    /*************************************************************************
    Training report:
        * RelCLSError   -   fraction of misclassified cases.
        * AvgCE         -   acerage cross-entropy
        * RMSError      -   root-mean-square error
        * AvgError      -   average error
        * AvgRelError   -   average relative error
        * NGrad         -   number of gradient calculations
        * NHess         -   number of Hessian calculations
        * NCholesky     -   number of Cholesky decompositions
        
    NOTE 1: RelCLSError/AvgCE are zero on regression problems.

    NOTE 2: on classification problems  RMSError/AvgError/AvgRelError  contain
            errors in prediction of posterior probabilities
    *************************************************************************/
    public class mlpreport : apobject
    {
        public double relclserror;
        public double avgce;
        public double rmserror;
        public double avgerror;
        public double avgrelerror;
        public int ngrad;
        public int nhess;
        public int ncholesky;
        public mlpreport()
        {
            init();
        }
        public override void init()
        {
        }
        public override apobject make_copy()
        {
            mlpreport _result = new mlpreport();
            _result.relclserror = relclserror;
            _result.avgce = avgce;
            _result.rmserror = rmserror;
            _result.avgerror = avgerror;
            _result.avgrelerror = avgrelerror;
            _result.ngrad = ngrad;
            _result.nhess = nhess;
            _result.ncholesky = ncholesky;
            return _result;
        }
    };


    /*************************************************************************
    Cross-validation estimates of generalization error
    *************************************************************************/
    public class mlpcvreport : apobject
    {
        public double relclserror;
        public double avgce;
        public double rmserror;
        public double avgerror;
        public double avgrelerror;
        public mlpcvreport()
        {
            init();
        }
        public override void init()
        {
        }
        public override apobject make_copy()
        {
            mlpcvreport _result = new mlpcvreport();
            _result.relclserror = relclserror;
            _result.avgce = avgce;
            _result.rmserror = rmserror;
            _result.avgerror = avgerror;
            _result.avgrelerror = avgrelerror;
            return _result;
        }
    };


    /*************************************************************************
    Temporary data structures used by following functions:
    * TrainNetworkX
    * StartTrainingX
    * ContinueTrainingX

    This structure contains:
    * network being trained
    * fully initialized LBFGS optimizer (we have to call MinLBFGSRestartFrom()
      before using it; usually it is done by StartTrainingX() function).
    * additional temporary arrays
      
    This structure should be initialized with InitMLPTrnSession() call.
    *************************************************************************/
    public class smlptrnsession : apobject
    {
        public double[] bestparameters;
        public double bestrmserror;
        public bool randomizenetwork;
        public mlpbase.multilayerperceptron network;
        public minlbfgs.minlbfgsstate optimizer;
        public minlbfgs.minlbfgsreport optimizerrep;
        public double[] wbuf0;
        public double[] wbuf1;
        public int[] allminibatches;
        public int[] currentminibatch;
        public rcommstate rstate;
        public int algoused;
        public int minibatchsize;
        public hqrnd.hqrndstate generator;
        public smlptrnsession()
        {
            init();
        }
        public override void init()
        {
            bestparameters = new double[0];
            network = new mlpbase.multilayerperceptron();
            optimizer = new minlbfgs.minlbfgsstate();
            optimizerrep = new minlbfgs.minlbfgsreport();
            wbuf0 = new double[0];
            wbuf1 = new double[0];
            allminibatches = new int[0];
            currentminibatch = new int[0];
            rstate = new rcommstate();
            generator = new hqrnd.hqrndstate();
        }
        public override apobject make_copy()
        {
            smlptrnsession _result = new smlptrnsession();
            _result.bestparameters = (double[])bestparameters.Clone();
            _result.bestrmserror = bestrmserror;
            _result.randomizenetwork = randomizenetwork;
            _result.network = (mlpbase.multilayerperceptron)network.make_copy();
            _result.optimizer = (minlbfgs.minlbfgsstate)optimizer.make_copy();
            _result.optimizerrep = (minlbfgs.minlbfgsreport)optimizerrep.make_copy();
            _result.wbuf0 = (double[])wbuf0.Clone();
            _result.wbuf1 = (double[])wbuf1.Clone();
            _result.allminibatches = (int[])allminibatches.Clone();
            _result.currentminibatch = (int[])currentminibatch.Clone();
            _result.rstate = (rcommstate)rstate.make_copy();
            _result.algoused = algoused;
            _result.minibatchsize = minibatchsize;
            _result.generator = (hqrnd.hqrndstate)generator.make_copy();
            return _result;
        }
    };


    /*************************************************************************
    Temporary data structures used by following functions:
    * TrainEnsembleX

    This structure contains:
    * two arrays which can be used to store training and validation subsets
    * sessions for MLP training

    This structure should be initialized with InitMLPETrnSession() call.
    *************************************************************************/
    public class mlpetrnsession : apobject
    {
        public int[] trnsubset;
        public int[] valsubset;
        public smp.shared_pool mlpsessions;
        public mlpreport mlprep;
        public mlpbase.multilayerperceptron network;
        public mlpetrnsession()
        {
            init();
        }
        public override void init()
        {
            trnsubset = new int[0];
            valsubset = new int[0];
            mlpsessions = new smp.shared_pool();
            mlprep = new mlpreport();
            network = new mlpbase.multilayerperceptron();
        }
        public override apobject make_copy()
        {
            mlpetrnsession _result = new mlpetrnsession();
            _result.trnsubset = (int[])trnsubset.Clone();
            _result.valsubset = (int[])valsubset.Clone();
            _result.mlpsessions = (smp.shared_pool)mlpsessions.make_copy();
            _result.mlprep = (mlpreport)mlprep.make_copy();
            _result.network = (mlpbase.multilayerperceptron)network.make_copy();
            return _result;
        }
    };


    /*************************************************************************
    Trainer object for neural network.

    You should not try to access fields of this object directly -  use  ALGLIB
    functions to work with this object.
    *************************************************************************/
    public class mlptrainer : apobject
    {
        public int nin;
        public int nout;
        public bool rcpar;
        public int lbfgsfactor;
        public double decay;
        public double wstep;
        public int maxits;
        public int datatype;
        public int npoints;
        public double[,] densexy;
        public sparse.sparsematrix sparsexy;
        public smlptrnsession session;
        public int ngradbatch;
        public int[] subset;
        public int subsetsize;
        public int[] valsubset;
        public int valsubsetsize;
        public int algokind;
        public int minibatchsize;
        public mlptrainer()
        {
            init();
        }
        public override void init()
        {
            densexy = new double[0, 0];
            sparsexy = new sparse.sparsematrix();
            session = new smlptrnsession();
            subset = new int[0];
            valsubset = new int[0];
        }
        public override apobject make_copy()
        {
            mlptrainer _result = new mlptrainer();
            _result.nin = nin;
            _result.nout = nout;
            _result.rcpar = rcpar;
            _result.lbfgsfactor = lbfgsfactor;
            _result.decay = decay;
            _result.wstep = wstep;
            _result.maxits = maxits;
            _result.datatype = datatype;
            _result.npoints = npoints;
            _result.densexy = (double[,])densexy.Clone();
            _result.sparsexy = (sparse.sparsematrix)sparsexy.make_copy();
            _result.session = (smlptrnsession)session.make_copy();
            _result.ngradbatch = ngradbatch;
            _result.subset = (int[])subset.Clone();
            _result.subsetsize = subsetsize;
            _result.valsubset = (int[])valsubset.Clone();
            _result.valsubsetsize = valsubsetsize;
            _result.algokind = algokind;
            _result.minibatchsize = minibatchsize;
            return _result;
        }
    };


    /*************************************************************************
    Internal record for parallelization function MLPFoldCV.
    *************************************************************************/
    public class mlpparallelizationcv : apobject
    {
        public mlpbase.multilayerperceptron network;
        public mlpreport rep;
        public int[] subset;
        public int subsetsize;
        public double[] xyrow;
        public double[] y;
        public int ngrad;
        public smp.shared_pool trnpool;
        public mlpparallelizationcv()
        {
            init();
        }
        public override void init()
        {
            network = new mlpbase.multilayerperceptron();
            rep = new mlpreport();
            subset = new int[0];
            xyrow = new double[0];
            y = new double[0];
            trnpool = new smp.shared_pool();
        }
        public override apobject make_copy()
        {
            mlpparallelizationcv _result = new mlpparallelizationcv();
            _result.network = (mlpbase.multilayerperceptron)network.make_copy();
            _result.rep = (mlpreport)rep.make_copy();
            _result.subset = (int[])subset.Clone();
            _result.subsetsize = subsetsize;
            _result.xyrow = (double[])xyrow.Clone();
            _result.y = (double[])y.Clone();
            _result.ngrad = ngrad;
            _result.trnpool = (smp.shared_pool)trnpool.make_copy();
            return _result;
        }
    };




    public const double mindecay = 0.001;
    public const int defaultlbfgsfactor = 6;


    /*************************************************************************
    Neural network training  using  modified  Levenberg-Marquardt  with  exact
    Hessian calculation and regularization. Subroutine trains  neural  network
    with restarts from random positions. Algorithm is well  suited  for  small
    and medium scale problems (hundreds of weights).

    INPUT PARAMETERS:
        Network     -   neural network with initialized geometry
        XY          -   training set
        NPoints     -   training set size
        Decay       -   weight decay constant, >=0.001
                        Decay term 'Decay*||Weights||^2' is added to error
                        function.
                        If you don't know what Decay to choose, use 0.001.
        Restarts    -   number of restarts from random position, >0.
                        If you don't know what Restarts to choose, use 2.

    OUTPUT PARAMETERS:
        Network     -   trained neural network.
        Info        -   return code:
                        * -9, if internal matrix inverse subroutine failed
                        * -2, if there is a point with class number
                              outside of [0..NOut-1].
                        * -1, if wrong parameters specified
                              (NPoints<0, Restarts<1).
                        *  2, if task has been solved.
        Rep         -   training report

      -- ALGLIB --
         Copyright 10.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void mlptrainlm(mlpbase.multilayerperceptron network,
        double[,] xy,
        int npoints,
        double decay,
        int restarts,
        ref int info,
        mlpreport rep,
        xparams _params)
    {
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        double lmsteptol = 0;
        int i = 0;
        int k = 0;
        double v = 0;
        double e = 0;
        double enew = 0;
        double xnorm2 = 0;
        double stepnorm = 0;
        double[] g = new double[0];
        double[] d = new double[0];
        double[,] h = new double[0, 0];
        double[,] hmod = new double[0, 0];
        double[,] z = new double[0, 0];
        bool spd = new bool();
        double nu = 0;
        double lambdav = 0;
        double lambdaup = 0;
        double lambdadown = 0;
        minlbfgs.minlbfgsreport internalrep = new minlbfgs.minlbfgsreport();
        minlbfgs.minlbfgsstate state = new minlbfgs.minlbfgsstate();
        double[] x = new double[0];
        double[] y = new double[0];
        double[] wbase = new double[0];
        double[] wdir = new double[0];
        double[] wt = new double[0];
        double[] wx = new double[0];
        int pass = 0;
        double[] wbest = new double[0];
        double ebest = 0;
        matinv.matinvreport invrep = new matinv.matinvreport();
        directdensesolvers.densesolverreport solverrep = new directdensesolvers.densesolverreport();
        int i_ = 0;

        info = 0;

        mlpbase.mlpproperties(network, ref nin, ref nout, ref wcount, _params);
        lambdaup = 10;
        lambdadown = 0.3;
        lmsteptol = 0.001;

        //
        // Test for inputs
        //
        if (npoints <= 0 || restarts < 1)
        {
            info = -1;
            return;
        }
        if (mlpbase.mlpissoftmax(network, _params))
        {
            for (i = 0; i <= npoints - 1; i++)
            {
                if ((int)Math.Round(xy[i, nin]) < 0 || (int)Math.Round(xy[i, nin]) >= nout)
                {
                    info = -2;
                    return;
                }
            }
        }
        decay = Math.Max(decay, mindecay);
        info = 2;

        //
        // Initialize data
        //
        rep.ngrad = 0;
        rep.nhess = 0;
        rep.ncholesky = 0;

        //
        // General case.
        // Prepare task and network. Allocate space.
        //
        mlpbase.mlpinitpreprocessor(network, xy, npoints, _params);
        g = new double[wcount - 1 + 1];
        h = new double[wcount - 1 + 1, wcount - 1 + 1];
        hmod = new double[wcount - 1 + 1, wcount - 1 + 1];
        wbase = new double[wcount - 1 + 1];
        wdir = new double[wcount - 1 + 1];
        wbest = new double[wcount - 1 + 1];
        wt = new double[wcount - 1 + 1];
        wx = new double[wcount - 1 + 1];
        ebest = math.maxrealnumber;

        //
        // Multiple passes
        //
        for (pass = 1; pass <= restarts; pass++)
        {

            //
            // Initialize weights
            //
            mlpbase.mlprandomize(network, _params);

            //
            // First stage of the hybrid algorithm: LBFGS
            //
            for (i_ = 0; i_ <= wcount - 1; i_++)
            {
                wbase[i_] = network.weights[i_];
            }
            minlbfgs.minlbfgscreate(wcount, Math.Min(wcount, 5), wbase, state, _params);
            minlbfgs.minlbfgssetcond(state, 0, 0, 0, Math.Max(25, wcount), _params);
            while (minlbfgs.minlbfgsiteration(state, _params))
            {

                //
                // gradient
                //
                for (i_ = 0; i_ <= wcount - 1; i_++)
                {
                    network.weights[i_] = state.x[i_];
                }
                mlpbase.mlpgradbatch(network, xy, npoints, ref state.f, ref state.g, _params);

                //
                // weight decay
                //
                v = 0.0;
                for (i_ = 0; i_ <= wcount - 1; i_++)
                {
                    v += network.weights[i_] * network.weights[i_];
                }
                state.f = state.f + 0.5 * decay * v;
                for (i_ = 0; i_ <= wcount - 1; i_++)
                {
                    state.g[i_] = state.g[i_] + decay * network.weights[i_];
                }

                //
                // next iteration
                //
                rep.ngrad = rep.ngrad + 1;
            }
            minlbfgs.minlbfgsresults(state, ref wbase, internalrep, _params);
            for (i_ = 0; i_ <= wcount - 1; i_++)
            {
                network.weights[i_] = wbase[i_];
            }

            //
            // Second stage of the hybrid algorithm: LM
            //
            // Initialize H with identity matrix,
            // G with gradient,
            // E with regularized error.
            //
            mlpbase.mlphessianbatch(network, xy, npoints, ref e, ref g, ref h, _params);
            v = 0.0;
            for (i_ = 0; i_ <= wcount - 1; i_++)
            {
                v += network.weights[i_] * network.weights[i_];
            }
            e = e + 0.5 * decay * v;
            for (i_ = 0; i_ <= wcount - 1; i_++)
            {
                g[i_] = g[i_] + decay * network.weights[i_];
            }
            for (k = 0; k <= wcount - 1; k++)
            {
                h[k, k] = h[k, k] + decay;
            }
            rep.nhess = rep.nhess + 1;
            lambdav = 0.001;
            nu = 2;
            while (true)
            {

                //
                // 1. HMod = H+lambda*I
                // 2. Try to solve (H+Lambda*I)*dx = -g.
                //    Increase lambda if left part is not positive definite.
                //
                for (i = 0; i <= wcount - 1; i++)
                {
                    for (i_ = 0; i_ <= wcount - 1; i_++)
                    {
                        hmod[i, i_] = h[i, i_];
                    }
                    hmod[i, i] = hmod[i, i] + lambdav;
                }
                spd = trfac.spdmatrixcholesky(hmod, wcount, true, _params);
                rep.ncholesky = rep.ncholesky + 1;
                if (!spd)
                {
                    lambdav = lambdav * lambdaup * nu;
                    nu = nu * 2;
                    continue;
                }
                directdensesolvers.spdmatrixcholeskysolve(hmod, wcount, true, g, ref wdir, solverrep, _params);
                if (solverrep.terminationtype < 0)
                {
                    lambdav = lambdav * lambdaup * nu;
                    nu = nu * 2;
                    continue;
                }
                for (i_ = 0; i_ <= wcount - 1; i_++)
                {
                    wdir[i_] = -1 * wdir[i_];
                }

                //
                // Lambda found.
                // 1. Save old w in WBase
                // 1. Test some stopping criterions
                // 2. If error(w+wdir)>error(w), increase lambda
                //
                for (i_ = 0; i_ <= wcount - 1; i_++)
                {
                    network.weights[i_] = network.weights[i_] + wdir[i_];
                }
                xnorm2 = 0.0;
                for (i_ = 0; i_ <= wcount - 1; i_++)
                {
                    xnorm2 += network.weights[i_] * network.weights[i_];
                }
                stepnorm = 0.0;
                for (i_ = 0; i_ <= wcount - 1; i_++)
                {
                    stepnorm += wdir[i_] * wdir[i_];
                }
                stepnorm = Math.Sqrt(stepnorm);
                enew = mlpbase.mlperror(network, xy, npoints, _params) + 0.5 * decay * xnorm2;
                if ((double)(stepnorm) < (double)(lmsteptol * (1 + Math.Sqrt(xnorm2))))
                {
                    break;
                }
                if ((double)(enew) > (double)(e))
                {
                    lambdav = lambdav * lambdaup * nu;
                    nu = nu * 2;
                    continue;
                }

                //
                // Optimize using inv(cholesky(H)) as preconditioner
                //
                matinv.rmatrixtrinverse(hmod, wcount, true, false, invrep, _params);
                if (invrep.terminationtype <= 0)
                {

                    //
                    // if matrix can't be inverted then exit with errors
                    // TODO: make WCount steps in direction suggested by HMod
                    //
                    info = -9;
                    return;
                }
                for (i_ = 0; i_ <= wcount - 1; i_++)
                {
                    wbase[i_] = network.weights[i_];
                }
                for (i = 0; i <= wcount - 1; i++)
                {
                    wt[i] = 0;
                }
                minlbfgs.minlbfgscreatex(wcount, wcount, wt, 1, 0.0, state, _params);
                minlbfgs.minlbfgssetcond(state, 0, 0, 0, 5, _params);
                while (minlbfgs.minlbfgsiteration(state, _params))
                {

                    //
                    // gradient
                    //
                    for (i = 0; i <= wcount - 1; i++)
                    {
                        v = 0.0;
                        for (i_ = i; i_ <= wcount - 1; i_++)
                        {
                            v += state.x[i_] * hmod[i, i_];
                        }
                        network.weights[i] = wbase[i] + v;
                    }
                    mlpbase.mlpgradbatch(network, xy, npoints, ref state.f, ref g, _params);
                    for (i = 0; i <= wcount - 1; i++)
                    {
                        state.g[i] = 0;
                    }
                    for (i = 0; i <= wcount - 1; i++)
                    {
                        v = g[i];
                        for (i_ = i; i_ <= wcount - 1; i_++)
                        {
                            state.g[i_] = state.g[i_] + v * hmod[i, i_];
                        }
                    }

                    //
                    // weight decay
                    // grad(x'*x) = A'*(x0+A*t)
                    //
                    v = 0.0;
                    for (i_ = 0; i_ <= wcount - 1; i_++)
                    {
                        v += network.weights[i_] * network.weights[i_];
                    }
                    state.f = state.f + 0.5 * decay * v;
                    for (i = 0; i <= wcount - 1; i++)
                    {
                        v = decay * network.weights[i];
                        for (i_ = i; i_ <= wcount - 1; i_++)
                        {
                            state.g[i_] = state.g[i_] + v * hmod[i, i_];
                        }
                    }

                    //
                    // next iteration
                    //
                    rep.ngrad = rep.ngrad + 1;
                }
                minlbfgs.minlbfgsresults(state, ref wt, internalrep, _params);

                //
                // Accept new position.
                // Calculate Hessian
                //
                for (i = 0; i <= wcount - 1; i++)
                {
                    v = 0.0;
                    for (i_ = i; i_ <= wcount - 1; i_++)
                    {
                        v += wt[i_] * hmod[i, i_];
                    }
                    network.weights[i] = wbase[i] + v;
                }
                mlpbase.mlphessianbatch(network, xy, npoints, ref e, ref g, ref h, _params);
                v = 0.0;
                for (i_ = 0; i_ <= wcount - 1; i_++)
                {
                    v += network.weights[i_] * network.weights[i_];
                }
                e = e + 0.5 * decay * v;
                for (i_ = 0; i_ <= wcount - 1; i_++)
                {
                    g[i_] = g[i_] + decay * network.weights[i_];
                }
                for (k = 0; k <= wcount - 1; k++)
                {
                    h[k, k] = h[k, k] + decay;
                }
                rep.nhess = rep.nhess + 1;

                //
                // Update lambda
                //
                lambdav = lambdav * lambdadown;
                nu = 2;
            }

            //
            // update WBest
            //
            v = 0.0;
            for (i_ = 0; i_ <= wcount - 1; i_++)
            {
                v += network.weights[i_] * network.weights[i_];
            }
            e = 0.5 * decay * v + mlpbase.mlperror(network, xy, npoints, _params);
            if ((double)(e) < (double)(ebest))
            {
                ebest = e;
                for (i_ = 0; i_ <= wcount - 1; i_++)
                {
                    wbest[i_] = network.weights[i_];
                }
            }
        }

        //
        // copy WBest to output
        //
        for (i_ = 0; i_ <= wcount - 1; i_++)
        {
            network.weights[i_] = wbest[i_];
        }
    }


    /*************************************************************************
    Neural  network  training  using  L-BFGS  algorithm  with  regularization.
    Subroutine  trains  neural  network  with  restarts from random positions.
    Algorithm  is  well  suited  for  problems  of  any dimensionality (memory
    requirements and step complexity are linear by weights number).

    INPUT PARAMETERS:
        Network     -   neural network with initialized geometry
        XY          -   training set
        NPoints     -   training set size
        Decay       -   weight decay constant, >=0.001
                        Decay term 'Decay*||Weights||^2' is added to error
                        function.
                        If you don't know what Decay to choose, use 0.001.
        Restarts    -   number of restarts from random position, >0.
                        If you don't know what Restarts to choose, use 2.
        WStep       -   stopping criterion. Algorithm stops if  step  size  is
                        less than WStep. Recommended value - 0.01.  Zero  step
                        size means stopping after MaxIts iterations.
        MaxIts      -   stopping   criterion.  Algorithm  stops  after  MaxIts
                        iterations (NOT gradient  calculations).  Zero  MaxIts
                        means stopping when step is sufficiently small.

    OUTPUT PARAMETERS:
        Network     -   trained neural network.
        Info        -   return code:
                        * -8, if both WStep=0 and MaxIts=0
                        * -2, if there is a point with class number
                              outside of [0..NOut-1].
                        * -1, if wrong parameters specified
                              (NPoints<0, Restarts<1).
                        *  2, if task has been solved.
        Rep         -   training report

      -- ALGLIB --
         Copyright 09.12.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void mlptrainlbfgs(mlpbase.multilayerperceptron network,
        double[,] xy,
        int npoints,
        double decay,
        int restarts,
        double wstep,
        int maxits,
        ref int info,
        mlpreport rep,
        xparams _params)
    {
        int i = 0;
        int pass = 0;
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        double[] w = new double[0];
        double[] wbest = new double[0];
        double e = 0;
        double v = 0;
        double ebest = 0;
        minlbfgs.minlbfgsreport internalrep = new minlbfgs.minlbfgsreport();
        minlbfgs.minlbfgsstate state = new minlbfgs.minlbfgsstate();
        int i_ = 0;

        info = 0;


        //
        // Test inputs, parse flags, read network geometry
        //
        if ((double)(wstep) == (double)(0) && maxits == 0)
        {
            info = -8;
            return;
        }
        if (((npoints <= 0 || restarts < 1) || (double)(wstep) < (double)(0)) || maxits < 0)
        {
            info = -1;
            return;
        }
        mlpbase.mlpproperties(network, ref nin, ref nout, ref wcount, _params);
        if (mlpbase.mlpissoftmax(network, _params))
        {
            for (i = 0; i <= npoints - 1; i++)
            {
                if ((int)Math.Round(xy[i, nin]) < 0 || (int)Math.Round(xy[i, nin]) >= nout)
                {
                    info = -2;
                    return;
                }
            }
        }
        decay = Math.Max(decay, mindecay);
        info = 2;

        //
        // Prepare
        //
        mlpbase.mlpinitpreprocessor(network, xy, npoints, _params);
        w = new double[wcount - 1 + 1];
        wbest = new double[wcount - 1 + 1];
        ebest = math.maxrealnumber;

        //
        // Multiple starts
        //
        rep.ncholesky = 0;
        rep.nhess = 0;
        rep.ngrad = 0;
        for (pass = 1; pass <= restarts; pass++)
        {

            //
            // Process
            //
            mlpbase.mlprandomize(network, _params);
            for (i_ = 0; i_ <= wcount - 1; i_++)
            {
                w[i_] = network.weights[i_];
            }
            minlbfgs.minlbfgscreate(wcount, Math.Min(wcount, 10), w, state, _params);
            minlbfgs.minlbfgssetcond(state, 0.0, 0.0, wstep, maxits, _params);
            while (minlbfgs.minlbfgsiteration(state, _params))
            {
                for (i_ = 0; i_ <= wcount - 1; i_++)
                {
                    network.weights[i_] = state.x[i_];
                }
                mlpbase.mlpgradnbatch(network, xy, npoints, ref state.f, ref state.g, _params);
                v = 0.0;
                for (i_ = 0; i_ <= wcount - 1; i_++)
                {
                    v += network.weights[i_] * network.weights[i_];
                }
                state.f = state.f + 0.5 * decay * v;
                for (i_ = 0; i_ <= wcount - 1; i_++)
                {
                    state.g[i_] = state.g[i_] + decay * network.weights[i_];
                }
                rep.ngrad = rep.ngrad + 1;
            }
            minlbfgs.minlbfgsresults(state, ref w, internalrep, _params);
            for (i_ = 0; i_ <= wcount - 1; i_++)
            {
                network.weights[i_] = w[i_];
            }

            //
            // Compare with best
            //
            v = 0.0;
            for (i_ = 0; i_ <= wcount - 1; i_++)
            {
                v += network.weights[i_] * network.weights[i_];
            }
            e = mlpbase.mlperrorn(network, xy, npoints, _params) + 0.5 * decay * v;
            if ((double)(e) < (double)(ebest))
            {
                for (i_ = 0; i_ <= wcount - 1; i_++)
                {
                    wbest[i_] = network.weights[i_];
                }
                ebest = e;
            }
        }

        //
        // The best network
        //
        for (i_ = 0; i_ <= wcount - 1; i_++)
        {
            network.weights[i_] = wbest[i_];
        }
    }


    /*************************************************************************
    Neural network training using early stopping (base algorithm - L-BFGS with
    regularization).

    INPUT PARAMETERS:
        Network     -   neural network with initialized geometry
        TrnXY       -   training set
        TrnSize     -   training set size, TrnSize>0
        ValXY       -   validation set
        ValSize     -   validation set size, ValSize>0
        Decay       -   weight decay constant, >=0.001
                        Decay term 'Decay*||Weights||^2' is added to error
                        function.
                        If you don't know what Decay to choose, use 0.001.
        Restarts    -   number of restarts, either:
                        * strictly positive number - algorithm make specified
                          number of restarts from random position.
                        * -1, in which case algorithm makes exactly one run
                          from the initial state of the network (no randomization).
                        If you don't know what Restarts to choose, choose one
                        one the following:
                        * -1 (deterministic start)
                        * +1 (one random restart)
                        * +5 (moderate amount of random restarts)

    OUTPUT PARAMETERS:
        Network     -   trained neural network.
        Info        -   return code:
                        * -2, if there is a point with class number
                              outside of [0..NOut-1].
                        * -1, if wrong parameters specified
                              (NPoints<0, Restarts<1, ...).
                        *  2, task has been solved, stopping  criterion  met -
                              sufficiently small step size.  Not expected  (we
                              use  EARLY  stopping)  but  possible  and not an
                              error.
                        *  6, task has been solved, stopping  criterion  met -
                              increasing of validation set error.
        Rep         -   training report

    NOTE:

    Algorithm stops if validation set error increases for  a  long  enough  or
    step size is small enought  (there  are  task  where  validation  set  may
    decrease for eternity). In any case solution returned corresponds  to  the
    minimum of validation set error.

      -- ALGLIB --
         Copyright 10.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void mlptraines(mlpbase.multilayerperceptron network,
        double[,] trnxy,
        int trnsize,
        double[,] valxy,
        int valsize,
        double decay,
        int restarts,
        ref int info,
        mlpreport rep,
        xparams _params)
    {
        int i = 0;
        int pass = 0;
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        double[] w = new double[0];
        double[] wbest = new double[0];
        double e = 0;
        double v = 0;
        double ebest = 0;
        double[] wfinal = new double[0];
        double efinal = 0;
        int itcnt = 0;
        int itbest = 0;
        minlbfgs.minlbfgsreport internalrep = new minlbfgs.minlbfgsreport();
        minlbfgs.minlbfgsstate state = new minlbfgs.minlbfgsstate();
        double wstep = 0;
        bool needrandomization = new bool();
        int i_ = 0;

        info = 0;

        wstep = 0.001;

        //
        // Test inputs, parse flags, read network geometry
        //
        if (((trnsize <= 0 || valsize <= 0) || (restarts < 1 && restarts != -1)) || (double)(decay) < (double)(0))
        {
            info = -1;
            return;
        }
        if (restarts == -1)
        {
            needrandomization = false;
            restarts = 1;
        }
        else
        {
            needrandomization = true;
        }
        mlpbase.mlpproperties(network, ref nin, ref nout, ref wcount, _params);
        if (mlpbase.mlpissoftmax(network, _params))
        {
            for (i = 0; i <= trnsize - 1; i++)
            {
                if ((int)Math.Round(trnxy[i, nin]) < 0 || (int)Math.Round(trnxy[i, nin]) >= nout)
                {
                    info = -2;
                    return;
                }
            }
            for (i = 0; i <= valsize - 1; i++)
            {
                if ((int)Math.Round(valxy[i, nin]) < 0 || (int)Math.Round(valxy[i, nin]) >= nout)
                {
                    info = -2;
                    return;
                }
            }
        }
        info = 2;

        //
        // Prepare
        //
        mlpbase.mlpinitpreprocessor(network, trnxy, trnsize, _params);
        w = new double[wcount - 1 + 1];
        wbest = new double[wcount - 1 + 1];
        wfinal = new double[wcount - 1 + 1];
        efinal = math.maxrealnumber;
        for (i = 0; i <= wcount - 1; i++)
        {
            wfinal[i] = 0;
        }

        //
        // Multiple starts
        //
        rep.ncholesky = 0;
        rep.nhess = 0;
        rep.ngrad = 0;
        for (pass = 1; pass <= restarts; pass++)
        {

            //
            // Process
            //
            if (needrandomization)
            {
                mlpbase.mlprandomize(network, _params);
            }
            ebest = mlpbase.mlperror(network, valxy, valsize, _params);
            for (i_ = 0; i_ <= wcount - 1; i_++)
            {
                wbest[i_] = network.weights[i_];
            }
            itbest = 0;
            itcnt = 0;
            for (i_ = 0; i_ <= wcount - 1; i_++)
            {
                w[i_] = network.weights[i_];
            }
            minlbfgs.minlbfgscreate(wcount, Math.Min(wcount, 10), w, state, _params);
            minlbfgs.minlbfgssetcond(state, 0.0, 0.0, wstep, 0, _params);
            minlbfgs.minlbfgssetxrep(state, true, _params);
            while (minlbfgs.minlbfgsiteration(state, _params))
            {

                //
                // Calculate gradient
                //
                if (state.needfg)
                {
                    for (i_ = 0; i_ <= wcount - 1; i_++)
                    {
                        network.weights[i_] = state.x[i_];
                    }
                    mlpbase.mlpgradnbatch(network, trnxy, trnsize, ref state.f, ref state.g, _params);
                    v = 0.0;
                    for (i_ = 0; i_ <= wcount - 1; i_++)
                    {
                        v += network.weights[i_] * network.weights[i_];
                    }
                    state.f = state.f + 0.5 * decay * v;
                    for (i_ = 0; i_ <= wcount - 1; i_++)
                    {
                        state.g[i_] = state.g[i_] + decay * network.weights[i_];
                    }
                    rep.ngrad = rep.ngrad + 1;
                }

                //
                // Validation set
                //
                if (state.xupdated)
                {
                    for (i_ = 0; i_ <= wcount - 1; i_++)
                    {
                        network.weights[i_] = state.x[i_];
                    }
                    e = mlpbase.mlperror(network, valxy, valsize, _params);
                    if ((double)(e) < (double)(ebest))
                    {
                        ebest = e;
                        for (i_ = 0; i_ <= wcount - 1; i_++)
                        {
                            wbest[i_] = network.weights[i_];
                        }
                        itbest = itcnt;
                    }
                    if (itcnt > 30 && (double)(itcnt) > (double)(1.5 * itbest))
                    {
                        info = 6;
                        break;
                    }
                    itcnt = itcnt + 1;
                }
            }
            minlbfgs.minlbfgsresults(state, ref w, internalrep, _params);

            //
            // Compare with final answer
            //
            if ((double)(ebest) < (double)(efinal))
            {
                for (i_ = 0; i_ <= wcount - 1; i_++)
                {
                    wfinal[i_] = wbest[i_];
                }
                efinal = ebest;
            }
        }

        //
        // The best network
        //
        for (i_ = 0; i_ <= wcount - 1; i_++)
        {
            network.weights[i_] = wfinal[i_];
        }
    }


    /*************************************************************************
    Cross-validation estimate of generalization error.

    Base algorithm - L-BFGS.

    INPUT PARAMETERS:
        Network     -   neural network with initialized geometry.   Network is
                        not changed during cross-validation -  it is used only
                        as a representative of its architecture.
        XY          -   training set.
        SSize       -   training set size
        Decay       -   weight  decay, same as in MLPTrainLBFGS
        Restarts    -   number of restarts, >0.
                        restarts are counted for each partition separately, so
                        total number of restarts will be Restarts*FoldsCount.
        WStep       -   stopping criterion, same as in MLPTrainLBFGS
        MaxIts      -   stopping criterion, same as in MLPTrainLBFGS
        FoldsCount  -   number of folds in k-fold cross-validation,
                        2<=FoldsCount<=SSize.
                        recommended value: 10.

    OUTPUT PARAMETERS:
        Info        -   return code, same as in MLPTrainLBFGS
        Rep         -   report, same as in MLPTrainLM/MLPTrainLBFGS
        CVRep       -   generalization error estimates

      -- ALGLIB --
         Copyright 09.12.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpkfoldcvlbfgs(mlpbase.multilayerperceptron network,
        double[,] xy,
        int npoints,
        double decay,
        int restarts,
        double wstep,
        int maxits,
        int foldscount,
        ref int info,
        mlpreport rep,
        mlpcvreport cvrep,
        xparams _params)
    {
        info = 0;

        mlpkfoldcvgeneral(network, xy, npoints, decay, restarts, foldscount, false, wstep, maxits, ref info, rep, cvrep, _params);
    }


    /*************************************************************************
    Cross-validation estimate of generalization error.

    Base algorithm - Levenberg-Marquardt.

    INPUT PARAMETERS:
        Network     -   neural network with initialized geometry.   Network is
                        not changed during cross-validation -  it is used only
                        as a representative of its architecture.
        XY          -   training set.
        SSize       -   training set size
        Decay       -   weight  decay, same as in MLPTrainLBFGS
        Restarts    -   number of restarts, >0.
                        restarts are counted for each partition separately, so
                        total number of restarts will be Restarts*FoldsCount.
        FoldsCount  -   number of folds in k-fold cross-validation,
                        2<=FoldsCount<=SSize.
                        recommended value: 10.

    OUTPUT PARAMETERS:
        Info        -   return code, same as in MLPTrainLBFGS
        Rep         -   report, same as in MLPTrainLM/MLPTrainLBFGS
        CVRep       -   generalization error estimates

      -- ALGLIB --
         Copyright 09.12.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpkfoldcvlm(mlpbase.multilayerperceptron network,
        double[,] xy,
        int npoints,
        double decay,
        int restarts,
        int foldscount,
        ref int info,
        mlpreport rep,
        mlpcvreport cvrep,
        xparams _params)
    {
        info = 0;

        mlpkfoldcvgeneral(network, xy, npoints, decay, restarts, foldscount, true, 0.0, 0, ref info, rep, cvrep, _params);
    }


    /*************************************************************************
    This function estimates generalization error using cross-validation on the
    current dataset with current training settings.

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
        S           -   trainer object
        Network     -   neural network. It must have same number of inputs and
                        output/classes as was specified during creation of the
                        trainer object. Network is not changed  during  cross-
                        validation and is not trained - it  is  used  only  as
                        representative of its architecture. I.e., we  estimate
                        generalization properties of  ARCHITECTURE,  not  some
                        specific network.
        NRestarts   -   number of restarts, >=0:
                        * NRestarts>0  means  that  for  each cross-validation
                          round   specified  number   of  random  restarts  is
                          performed,  with  best  network  being  chosen after
                          training.
                        * NRestarts=0 is same as NRestarts=1
        FoldsCount  -   number of folds in k-fold cross-validation:
                        * 2<=FoldsCount<=size of dataset
                        * recommended value: 10.
                        * values larger than dataset size will be silently
                          truncated down to dataset size

    OUTPUT PARAMETERS:
        Rep         -   structure which contains cross-validation estimates:
                        * Rep.RelCLSError - fraction of misclassified cases.
                        * Rep.AvgCE - acerage cross-entropy
                        * Rep.RMSError - root-mean-square error
                        * Rep.AvgError - average error
                        * Rep.AvgRelError - average relative error
                        
    NOTE: when no dataset was specified with MLPSetDataset/SetSparseDataset(),
          or subset with only one point  was  given,  zeros  are  returned  as
          estimates.

    NOTE: this method performs FoldsCount cross-validation  rounds,  each  one
          with NRestarts random starts.  Thus,  FoldsCount*NRestarts  networks
          are trained in total.

    NOTE: Rep.RelCLSError/Rep.AvgCE are zero on regression problems.

    NOTE: on classification problems Rep.RMSError/Rep.AvgError/Rep.AvgRelError
          contain errors in prediction of posterior probabilities.
            
      -- ALGLIB --
         Copyright 23.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpkfoldcv(mlptrainer s,
        mlpbase.multilayerperceptron network,
        int nrestarts,
        int foldscount,
        mlpreport rep,
        xparams _params)
    {
        smp.shared_pool pooldatacv = new smp.shared_pool();
        mlpparallelizationcv datacv = new mlpparallelizationcv();
        mlpparallelizationcv sdatacv = null;
        double[,] cvy = new double[0, 0];
        int[] folds = new int[0];
        double[] buf = new double[0];
        double[] dy = new double[0];
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        int rowsize = 0;
        int ntype = 0;
        int ttype = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        hqrnd.hqrndstate rs = new hqrnd.hqrndstate();
        int i_ = 0;
        int i1_ = 0;

        if (!mlpbase.mlpissoftmax(network, _params))
        {
            ntype = 0;
        }
        else
        {
            ntype = 1;
        }
        if (s.rcpar)
        {
            ttype = 0;
        }
        else
        {
            ttype = 1;
        }
        ap.assert(ntype == ttype, "MLPKFoldCV: type of input network is not similar to network type in trainer object");
        ap.assert(s.npoints >= 0, "MLPKFoldCV: possible trainer S is not initialized(S.NPoints<0)");
        mlpbase.mlpproperties(network, ref nin, ref nout, ref wcount, _params);
        ap.assert(s.nin == nin, "MLPKFoldCV:  number of inputs in trainer is not equal to number of inputs in network");
        ap.assert(s.nout == nout, "MLPKFoldCV:  number of outputs in trainer is not equal to number of outputs in network");
        ap.assert(nrestarts >= 0, "MLPKFoldCV: NRestarts<0");
        ap.assert(foldscount >= 2, "MLPKFoldCV: FoldsCount<2");
        if (foldscount > s.npoints)
        {
            foldscount = s.npoints;
        }
        rep.relclserror = 0;
        rep.avgce = 0;
        rep.rmserror = 0;
        rep.avgerror = 0;
        rep.avgrelerror = 0;
        hqrnd.hqrndrandomize(rs, _params);
        rep.ngrad = 0;
        rep.nhess = 0;
        rep.ncholesky = 0;
        if (s.npoints == 0 || s.npoints == 1)
        {
            return;
        }

        //
        // Read network geometry, test parameters
        //
        if (s.rcpar)
        {
            rowsize = nin + nout;
            dy = new double[nout];
            bdss.dserrallocate(-nout, ref buf, _params);
        }
        else
        {
            rowsize = nin + 1;
            dy = new double[1];
            bdss.dserrallocate(nout, ref buf, _params);
        }

        //
        // Folds
        //
        folds = new int[s.npoints];
        for (i = 0; i <= s.npoints - 1; i++)
        {
            folds[i] = i * foldscount / s.npoints;
        }
        for (i = 0; i <= s.npoints - 2; i++)
        {
            j = i + hqrnd.hqrnduniformi(rs, s.npoints - i, _params);
            if (j != i)
            {
                k = folds[i];
                folds[i] = folds[j];
                folds[j] = k;
            }
        }
        cvy = new double[s.npoints, nout];

        //
        // Initialize SEED-value for shared pool
        //
        datacv.ngrad = 0;
        mlpbase.mlpcopy(network, datacv.network, _params);
        datacv.subset = new int[s.npoints];
        datacv.xyrow = new double[rowsize];
        datacv.y = new double[nout];

        //
        // Create shared pool
        //
        smp.ae_shared_pool_set_seed(pooldatacv, datacv);

        //
        // Parallelization
        //
        mthreadcv(s, rowsize, nrestarts, folds, 0, foldscount, cvy, pooldatacv, wcount, _params);

        //
        // Calculate value for NGrad
        //
        smp.ae_shared_pool_first_recycled(pooldatacv, ref sdatacv);
        while (sdatacv != null)
        {
            rep.ngrad = rep.ngrad + sdatacv.ngrad;
            smp.ae_shared_pool_next_recycled(pooldatacv, ref sdatacv);
        }

        //
        // Connect of results and calculate cross-validation error
        //
        for (i = 0; i <= s.npoints - 1; i++)
        {
            if (s.datatype == 0)
            {
                for (i_ = 0; i_ <= rowsize - 1; i_++)
                {
                    datacv.xyrow[i_] = s.densexy[i, i_];
                }
            }
            if (s.datatype == 1)
            {
                sparse.sparsegetrow(s.sparsexy, i, ref datacv.xyrow, _params);
            }
            for (i_ = 0; i_ <= nout - 1; i_++)
            {
                datacv.y[i_] = cvy[i, i_];
            }
            if (s.rcpar)
            {
                i1_ = (nin) - (0);
                for (i_ = 0; i_ <= nout - 1; i_++)
                {
                    dy[i_] = datacv.xyrow[i_ + i1_];
                }
            }
            else
            {
                dy[0] = datacv.xyrow[nin];
            }
            bdss.dserraccumulate(ref buf, datacv.y, dy, _params);
        }
        bdss.dserrfinish(ref buf, _params);
        rep.relclserror = buf[0];
        rep.avgce = buf[1];
        rep.rmserror = buf[2];
        rep.avgerror = buf[3];
        rep.avgrelerror = buf[4];
    }


    /*************************************************************************
    Creation of the network trainer object for regression networks

    INPUT PARAMETERS:
        NIn         -   number of inputs, NIn>=1
        NOut        -   number of outputs, NOut>=1

    OUTPUT PARAMETERS:
        S           -   neural network trainer object.
                        This structure can be used to train any regression
                        network with NIn inputs and NOut outputs.

      -- ALGLIB --
         Copyright 23.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpcreatetrainer(int nin,
        int nout,
        mlptrainer s,
        xparams _params)
    {
        ap.assert(nin >= 1, "MLPCreateTrainer: NIn<1.");
        ap.assert(nout >= 1, "MLPCreateTrainer: NOut<1.");
        s.nin = nin;
        s.nout = nout;
        s.rcpar = true;
        s.lbfgsfactor = defaultlbfgsfactor;
        s.decay = 1.0E-6;
        mlpsetcond(s, 0, 0, _params);
        s.datatype = 0;
        s.npoints = 0;
        mlpsetalgobatch(s, _params);
    }


    /*************************************************************************
    Creation of the network trainer object for classification networks

    INPUT PARAMETERS:
        NIn         -   number of inputs, NIn>=1
        NClasses    -   number of classes, NClasses>=2

    OUTPUT PARAMETERS:
        S           -   neural network trainer object.
                        This structure can be used to train any classification
                        network with NIn inputs and NOut outputs.

      -- ALGLIB --
         Copyright 23.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpcreatetrainercls(int nin,
        int nclasses,
        mlptrainer s,
        xparams _params)
    {
        ap.assert(nin >= 1, "MLPCreateTrainerCls: NIn<1.");
        ap.assert(nclasses >= 2, "MLPCreateTrainerCls: NClasses<2.");
        s.nin = nin;
        s.nout = nclasses;
        s.rcpar = false;
        s.lbfgsfactor = defaultlbfgsfactor;
        s.decay = 1.0E-6;
        mlpsetcond(s, 0, 0, _params);
        s.datatype = 0;
        s.npoints = 0;
        mlpsetalgobatch(s, _params);
    }


    /*************************************************************************
    This function sets "current dataset" of the trainer object to  one  passed
    by user.

    INPUT PARAMETERS:
        S           -   trainer object
        XY          -   training  set,  see  below  for  information  on   the
                        training set format. This function checks  correctness
                        of  the  dataset  (no  NANs/INFs,  class  numbers  are
                        correct) and throws exception when  incorrect  dataset
                        is passed.
        NPoints     -   points count, >=0.

    DATASET FORMAT:

    This  function  uses  two  different  dataset formats - one for regression
    networks, another one for classification networks.

    For regression networks with NIn inputs and NOut outputs following dataset
    format is used:
    * dataset is given by NPoints*(NIn+NOut) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, next NOut columns are outputs

    For classification networks with NIn inputs and NClasses clases  following
    datasetformat is used:
    * dataset is given by NPoints*(NIn+1) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, last column stores class number (from 0 to
      NClasses-1).
      
      -- ALGLIB --
         Copyright 23.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpsetdataset(mlptrainer s,
        double[,] xy,
        int npoints,
        xparams _params)
    {
        int ndim = 0;
        int i = 0;
        int j = 0;

        ap.assert(s.nin >= 1, "MLPSetDataset: possible parameter S is not initialized or spoiled(S.NIn<=0).");
        ap.assert(npoints >= 0, "MLPSetDataset: NPoint<0");
        ap.assert(npoints <= ap.rows(xy), "MLPSetDataset: invalid size of matrix XY(NPoint more then rows of matrix XY)");
        s.datatype = 0;
        s.npoints = npoints;
        if (npoints == 0)
        {
            return;
        }
        if (s.rcpar)
        {
            ap.assert(s.nout >= 1, "MLPSetDataset: possible parameter S is not initialized or is spoiled(NOut<1 for regression).");
            ndim = s.nin + s.nout;
            ap.assert(ndim <= ap.cols(xy), "MLPSetDataset: invalid size of matrix XY(too few columns in matrix XY).");
            ap.assert(apserv.apservisfinitematrix(xy, npoints, ndim, _params), "MLPSetDataset: parameter XY contains Infinite or NaN.");
        }
        else
        {
            ap.assert(s.nout >= 2, "MLPSetDataset: possible parameter S is not initialized or is spoiled(NClasses<2 for classifier).");
            ndim = s.nin + 1;
            ap.assert(ndim <= ap.cols(xy), "MLPSetDataset: invalid size of matrix XY(too few columns in matrix XY).");
            ap.assert(apserv.apservisfinitematrix(xy, npoints, ndim, _params), "MLPSetDataset: parameter XY contains Infinite or NaN.");
            for (i = 0; i <= npoints - 1; i++)
            {
                ap.assert((int)Math.Round(xy[i, s.nin]) >= 0 && (int)Math.Round(xy[i, s.nin]) < s.nout, "MLPSetDataset: invalid parameter XY(in classifier used nonexistent class number: either XY[.,NIn]<0 or XY[.,NIn]>=NClasses).");
            }
        }
        apserv.rmatrixsetlengthatleast(ref s.densexy, npoints, ndim, _params);
        for (i = 0; i <= npoints - 1; i++)
        {
            for (j = 0; j <= ndim - 1; j++)
            {
                s.densexy[i, j] = xy[i, j];
            }
        }
    }


    /*************************************************************************
    This function sets "current dataset" of the trainer object to  one  passed
    by user (sparse matrix is used to store dataset).

    INPUT PARAMETERS:
        S           -   trainer object
        XY          -   training  set,  see  below  for  information  on   the
                        training set format. This function checks  correctness
                        of  the  dataset  (no  NANs/INFs,  class  numbers  are
                        correct) and throws exception when  incorrect  dataset
                        is passed. Any  sparse  storage  format  can be  used:
                        Hash-table, CRS...
        NPoints     -   points count, >=0

    DATASET FORMAT:

    This  function  uses  two  different  dataset formats - one for regression
    networks, another one for classification networks.

    For regression networks with NIn inputs and NOut outputs following dataset
    format is used:
    * dataset is given by NPoints*(NIn+NOut) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, next NOut columns are outputs

    For classification networks with NIn inputs and NClasses clases  following
    datasetformat is used:
    * dataset is given by NPoints*(NIn+1) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, last column stores class number (from 0 to
      NClasses-1).
      
      -- ALGLIB --
         Copyright 23.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpsetsparsedataset(mlptrainer s,
        sparse.sparsematrix xy,
        int npoints,
        xparams _params)
    {
        double v = 0;
        int t0 = 0;
        int t1 = 0;
        int i = 0;
        int j = 0;


        //
        // Check correctness of the data
        //
        ap.assert(s.nin > 0, "MLPSetSparseDataset: possible parameter S is not initialized or spoiled(S.NIn<=0).");
        ap.assert(npoints >= 0, "MLPSetSparseDataset: NPoint<0");
        ap.assert(npoints <= sparse.sparsegetnrows(xy, _params), "MLPSetSparseDataset: invalid size of sparse matrix XY(NPoint more then rows of matrix XY)");
        if (npoints > 0)
        {
            t0 = 0;
            t1 = 0;
            if (s.rcpar)
            {
                ap.assert(s.nout >= 1, "MLPSetSparseDataset: possible parameter S is not initialized or is spoiled(NOut<1 for regression).");
                ap.assert(s.nin + s.nout <= sparse.sparsegetncols(xy, _params), "MLPSetSparseDataset: invalid size of sparse matrix XY(too few columns in sparse matrix XY).");
                while (sparse.sparseenumerate(xy, ref t0, ref t1, ref i, ref j, ref v, _params))
                {
                    if (i < npoints && j < s.nin + s.nout)
                    {
                        ap.assert(math.isfinite(v), "MLPSetSparseDataset: sparse matrix XY contains Infinite or NaN.");
                    }
                }
            }
            else
            {
                ap.assert(s.nout >= 2, "MLPSetSparseDataset: possible parameter S is not initialized or is spoiled(NClasses<2 for classifier).");
                ap.assert(s.nin + 1 <= sparse.sparsegetncols(xy, _params), "MLPSetSparseDataset: invalid size of sparse matrix XY(too few columns in sparse matrix XY).");
                while (sparse.sparseenumerate(xy, ref t0, ref t1, ref i, ref j, ref v, _params))
                {
                    if (i < npoints && j <= s.nin)
                    {
                        if (j != s.nin)
                        {
                            ap.assert(math.isfinite(v), "MLPSetSparseDataset: sparse matrix XY contains Infinite or NaN.");
                        }
                        else
                        {
                            ap.assert((math.isfinite(v) && (int)Math.Round(v) >= 0) && (int)Math.Round(v) < s.nout, "MLPSetSparseDataset: invalid sparse matrix XY(in classifier used nonexistent class number: either XY[.,NIn]<0 or XY[.,NIn]>=NClasses).");
                        }
                    }
                }
            }
        }

        //
        // Set dataset
        //
        s.datatype = 1;
        s.npoints = npoints;
        sparse.sparsecopytocrs(xy, s.sparsexy, _params);
    }


    /*************************************************************************
    This function sets weight decay coefficient which is used for training.

    INPUT PARAMETERS:
        S           -   trainer object
        Decay       -   weight  decay  coefficient,  >=0.  Weight  decay  term
                        'Decay*||Weights||^2' is added to error  function.  If
                        you don't know what Decay to choose, use 1.0E-3.
                        Weight decay can be set to zero,  in this case network
                        is trained without weight decay.

    NOTE: by default network uses some small nonzero value for weight decay.

      -- ALGLIB --
         Copyright 23.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpsetdecay(mlptrainer s,
        double decay,
        xparams _params)
    {
        ap.assert(math.isfinite(decay), "MLPSetDecay: parameter Decay contains Infinite or NaN.");
        ap.assert((double)(decay) >= (double)(0), "MLPSetDecay: Decay<0.");
        s.decay = decay;
    }


    /*************************************************************************
    This function sets stopping criteria for the optimizer.

    INPUT PARAMETERS:
        S           -   trainer object
        WStep       -   stopping criterion. Algorithm stops if  step  size  is
                        less than WStep. Recommended value - 0.01.  Zero  step
                        size means stopping after MaxIts iterations.
                        WStep>=0.
        MaxIts      -   stopping   criterion.  Algorithm  stops  after  MaxIts
                        epochs (full passes over entire dataset).  Zero MaxIts
                        means stopping when step is sufficiently small.
                        MaxIts>=0.

    NOTE: by default, WStep=0.005 and MaxIts=0 are used. These values are also
          used when MLPSetCond() is called with WStep=0 and MaxIts=0.
          
    NOTE: these stopping criteria are used for all kinds of neural training  -
          from "conventional" networks to early stopping ensembles. When  used
          for "conventional" networks, they are  used  as  the  only  stopping
          criteria. When combined with early stopping, they used as ADDITIONAL
          stopping criteria which can terminate early stopping algorithm.

      -- ALGLIB --
         Copyright 23.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpsetcond(mlptrainer s,
        double wstep,
        int maxits,
        xparams _params)
    {
        ap.assert(math.isfinite(wstep), "MLPSetCond: parameter WStep contains Infinite or NaN.");
        ap.assert((double)(wstep) >= (double)(0), "MLPSetCond: WStep<0.");
        ap.assert(maxits >= 0, "MLPSetCond: MaxIts<0.");
        if ((double)(wstep) != (double)(0) || maxits != 0)
        {
            s.wstep = wstep;
            s.maxits = maxits;
        }
        else
        {
            s.wstep = 0.005;
            s.maxits = 0;
        }
    }


    /*************************************************************************
    This function sets training algorithm: batch training using L-BFGS will be
    used.

    This algorithm:
    * the most robust for small-scale problems, but may be too slow for  large
      scale ones.
    * perfoms full pass through the dataset before performing step
    * uses conditions specified by MLPSetCond() for stopping
    * is default one used by trainer object

    INPUT PARAMETERS:
        S           -   trainer object

      -- ALGLIB --
         Copyright 23.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpsetalgobatch(mlptrainer s,
        xparams _params)
    {
        s.algokind = 0;
    }


    /*************************************************************************
    This function trains neural network passed to this function, using current
    dataset (one which was passed to MLPSetDataset() or MLPSetSparseDataset())
    and current training settings. Training  from  NRestarts  random  starting
    positions is performed, best network is chosen.

    Training is performed using current training algorithm.

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
        S           -   trainer object
        Network     -   neural network. It must have same number of inputs and
                        output/classes as was specified during creation of the
                        trainer object.
        NRestarts   -   number of restarts, >=0:
                        * NRestarts>0 means that specified  number  of  random
                          restarts are performed, best network is chosen after
                          training
                        * NRestarts=0 means that current state of the  network
                          is used for training.

    OUTPUT PARAMETERS:
        Network     -   trained network

    NOTE: when no dataset was specified with MLPSetDataset/SetSparseDataset(),
          network  is  filled  by zero  values.  Same  behavior  for functions
          MLPStartTraining and MLPContinueTraining.

    NOTE: this method uses sum-of-squares error function for training.

      -- ALGLIB --
         Copyright 23.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void mlptrainnetwork(mlptrainer s,
        mlpbase.multilayerperceptron network,
        int nrestarts,
        mlpreport rep,
        xparams _params)
    {
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        int ntype = 0;
        int ttype = 0;
        smp.shared_pool trnpool = new smp.shared_pool();

        ap.assert(s.npoints >= 0, "MLPTrainNetwork: parameter S is not initialized or is spoiled(S.NPoints<0)");
        if (!mlpbase.mlpissoftmax(network, _params))
        {
            ntype = 0;
        }
        else
        {
            ntype = 1;
        }
        if (s.rcpar)
        {
            ttype = 0;
        }
        else
        {
            ttype = 1;
        }
        ap.assert(ntype == ttype, "MLPTrainNetwork: type of input network is not similar to network type in trainer object");
        mlpbase.mlpproperties(network, ref nin, ref nout, ref wcount, _params);
        ap.assert(s.nin == nin, "MLPTrainNetwork: number of inputs in trainer is not equal to number of inputs in network");
        ap.assert(s.nout == nout, "MLPTrainNetwork: number of outputs in trainer is not equal to number of outputs in network");
        ap.assert(nrestarts >= 0, "MLPTrainNetwork: NRestarts<0.");

        //
        // Train
        //
        mlptrainnetworkx(s, nrestarts, -1, s.subset, -1, s.subset, 0, network, rep, true, trnpool, _params);
    }


    /*************************************************************************
    IMPORTANT: this is an "expert" version of the MLPTrain() function.  We  do
               not recommend you to use it unless you are pretty sure that you
               need ability to monitor training progress.

    This function performs step-by-step training of the neural  network.  Here
    "step-by-step" means that training  starts  with  MLPStartTraining() call,
    and then user subsequently calls MLPContinueTraining() to perform one more
    iteration of the training.

    After call to this function trainer object remembers network and  is ready
    to  train  it.  However,  no  training  is  performed  until first call to 
    MLPContinueTraining() function. Subsequent calls  to MLPContinueTraining()
    will advance training progress one iteration further.

    EXAMPLE:
        >
        > ...initialize network and trainer object....
        >
        > MLPStartTraining(Trainer, Network, True)
        > while MLPContinueTraining(Trainer, Network) do
        >     ...visualize training progress...
        >

    INPUT PARAMETERS:
        S           -   trainer object
        Network     -   neural network. It must have same number of inputs and
                        output/classes as was specified during creation of the
                        trainer object.
        RandomStart -   randomize network before training or not:
                        * True  means  that  network  is  randomized  and  its
                          initial state (one which was passed to  the  trainer
                          object) is lost.
                        * False  means  that  training  is  started  from  the
                          current state of the network
                        
    OUTPUT PARAMETERS:
        Network     -   neural network which is ready to training (weights are
                        initialized, preprocessor is initialized using current
                        training set)

    NOTE: this method uses sum-of-squares error function for training.

    NOTE: it is expected that trainer object settings are NOT  changed  during
          step-by-step training, i.e. no  one  changes  stopping  criteria  or
          training set during training. It is possible and there is no defense
          against  such  actions,  but  algorithm  behavior  in  such cases is
          undefined and can be unpredictable.

      -- ALGLIB --
         Copyright 23.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpstarttraining(mlptrainer s,
        mlpbase.multilayerperceptron network,
        bool randomstart,
        xparams _params)
    {
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        int ntype = 0;
        int ttype = 0;

        ap.assert(s.npoints >= 0, "MLPStartTraining: parameter S is not initialized or is spoiled(S.NPoints<0)");
        if (!mlpbase.mlpissoftmax(network, _params))
        {
            ntype = 0;
        }
        else
        {
            ntype = 1;
        }
        if (s.rcpar)
        {
            ttype = 0;
        }
        else
        {
            ttype = 1;
        }
        ap.assert(ntype == ttype, "MLPStartTraining: type of input network is not similar to network type in trainer object");
        mlpbase.mlpproperties(network, ref nin, ref nout, ref wcount, _params);
        ap.assert(s.nin == nin, "MLPStartTraining: number of inputs in trainer is not equal to number of inputs in the network.");
        ap.assert(s.nout == nout, "MLPStartTraining: number of outputs in trainer is not equal to number of outputs in the network.");

        //
        // Initialize temporaries
        //
        initmlptrnsession(network, randomstart, s, s.session, _params);

        //
        // Train network
        //
        mlpstarttrainingx(s, randomstart, -1, s.subset, -1, s.session, _params);

        //
        // Update network
        //
        mlpbase.mlpcopytunableparameters(s.session.network, network, _params);
    }


    /*************************************************************************
    IMPORTANT: this is an "expert" version of the MLPTrain() function.  We  do
               not recommend you to use it unless you are pretty sure that you
               need ability to monitor training progress.
               
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

    This function performs step-by-step training of the neural  network.  Here
    "step-by-step" means that training starts  with  MLPStartTraining()  call,
    and then user subsequently calls MLPContinueTraining() to perform one more
    iteration of the training.

    This  function  performs  one  more  iteration of the training and returns
    either True (training continues) or False (training stopped). In case True
    was returned, Network weights are updated according to the  current  state
    of the optimization progress. In case False was  returned,  no  additional
    updates is performed (previous update of  the  network weights moved us to
    the final point, and no additional updates is needed).

    EXAMPLE:
        >
        > [initialize network and trainer object]
        >
        > MLPStartTraining(Trainer, Network, True)
        > while MLPContinueTraining(Trainer, Network) do
        >     [visualize training progress]
        >

    INPUT PARAMETERS:
        S           -   trainer object
        Network     -   neural  network  structure,  which  is  used to  store
                        current state of the training process.
                        
    OUTPUT PARAMETERS:
        Network     -   weights of the neural network  are  rewritten  by  the
                        current approximation.

    NOTE: this method uses sum-of-squares error function for training.

    NOTE: it is expected that trainer object settings are NOT  changed  during
          step-by-step training, i.e. no  one  changes  stopping  criteria  or
          training set during training. It is possible and there is no defense
          against  such  actions,  but  algorithm  behavior  in  such cases is
          undefined and can be unpredictable.
          
    NOTE: It  is  expected that Network is the same one which  was  passed  to
          MLPStartTraining() function.  However,  THIS  function  checks  only
          following:
          * that number of network inputs is consistent with trainer object
            settings
          * that number of network outputs/classes is consistent with  trainer
            object settings
          * that number of network weights is the same as number of weights in
            the network passed to MLPStartTraining() function
          Exception is thrown when these conditions are violated.
          
          It is also expected that you do not change state of the  network  on
          your own - the only party who has right to change network during its
          training is a trainer object. Any attempt to interfere with  trainer
          may lead to unpredictable results.
          

      -- ALGLIB --
         Copyright 23.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static bool mlpcontinuetraining(mlptrainer s,
        mlpbase.multilayerperceptron network,
        xparams _params)
    {
        bool result = new bool();
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        int ntype = 0;
        int ttype = 0;
        int i_ = 0;

        ap.assert(s.npoints >= 0, "MLPContinueTraining: parameter S is not initialized or is spoiled(S.NPoints<0)");
        if (s.rcpar)
        {
            ttype = 0;
        }
        else
        {
            ttype = 1;
        }
        if (!mlpbase.mlpissoftmax(network, _params))
        {
            ntype = 0;
        }
        else
        {
            ntype = 1;
        }
        ap.assert(ntype == ttype, "MLPContinueTraining: type of input network is not similar to network type in trainer object.");
        mlpbase.mlpproperties(network, ref nin, ref nout, ref wcount, _params);
        ap.assert(s.nin == nin, "MLPContinueTraining: number of inputs in trainer is not equal to number of inputs in the network.");
        ap.assert(s.nout == nout, "MLPContinueTraining: number of outputs in trainer is not equal to number of outputs in the network.");
        result = mlpcontinuetrainingx(s, s.subset, -1, ref s.ngradbatch, s.session, _params);
        if (result)
        {
            for (i_ = 0; i_ <= wcount - 1; i_++)
            {
                network.weights[i_] = s.session.network.weights[i_];
            }
        }
        return result;
    }


    /*************************************************************************
    Training neural networks ensemble using  bootstrap  aggregating (bagging).
    Modified Levenberg-Marquardt algorithm is used as base training method.

    INPUT PARAMETERS:
        Ensemble    -   model with initialized geometry
        XY          -   training set
        NPoints     -   training set size
        Decay       -   weight decay coefficient, >=0.001
        Restarts    -   restarts, >0.

    OUTPUT PARAMETERS:
        Ensemble    -   trained model
        Info        -   return code:
                        * -2, if there is a point with class number
                              outside of [0..NClasses-1].
                        * -1, if incorrect parameters was passed
                              (NPoints<0, Restarts<1).
                        *  2, if task has been solved.
        Rep         -   training report.
        OOBErrors   -   out-of-bag generalization error estimate

      -- ALGLIB --
         Copyright 17.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpebagginglm(mlpe.mlpensemble ensemble,
        double[,] xy,
        int npoints,
        double decay,
        int restarts,
        ref int info,
        mlpreport rep,
        mlpcvreport ooberrors,
        xparams _params)
    {
        info = 0;

        mlpebagginginternal(ensemble, xy, npoints, decay, restarts, 0.0, 0, true, ref info, rep, ooberrors, _params);
    }


    /*************************************************************************
    Training neural networks ensemble using  bootstrap  aggregating (bagging).
    L-BFGS algorithm is used as base training method.

    INPUT PARAMETERS:
        Ensemble    -   model with initialized geometry
        XY          -   training set
        NPoints     -   training set size
        Decay       -   weight decay coefficient, >=0.001
        Restarts    -   restarts, >0.
        WStep       -   stopping criterion, same as in MLPTrainLBFGS
        MaxIts      -   stopping criterion, same as in MLPTrainLBFGS

    OUTPUT PARAMETERS:
        Ensemble    -   trained model
        Info        -   return code:
                        * -8, if both WStep=0 and MaxIts=0
                        * -2, if there is a point with class number
                              outside of [0..NClasses-1].
                        * -1, if incorrect parameters was passed
                              (NPoints<0, Restarts<1).
                        *  2, if task has been solved.
        Rep         -   training report.
        OOBErrors   -   out-of-bag generalization error estimate

      -- ALGLIB --
         Copyright 17.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpebagginglbfgs(mlpe.mlpensemble ensemble,
        double[,] xy,
        int npoints,
        double decay,
        int restarts,
        double wstep,
        int maxits,
        ref int info,
        mlpreport rep,
        mlpcvreport ooberrors,
        xparams _params)
    {
        info = 0;

        mlpebagginginternal(ensemble, xy, npoints, decay, restarts, wstep, maxits, false, ref info, rep, ooberrors, _params);
    }


    /*************************************************************************
    Training neural networks ensemble using early stopping.

    INPUT PARAMETERS:
        Ensemble    -   model with initialized geometry
        XY          -   training set
        NPoints     -   training set size
        Decay       -   weight decay coefficient, >=0.001
        Restarts    -   restarts, >0.

    OUTPUT PARAMETERS:
        Ensemble    -   trained model
        Info        -   return code:
                        * -2, if there is a point with class number
                              outside of [0..NClasses-1].
                        * -1, if incorrect parameters was passed
                              (NPoints<0, Restarts<1).
                        *  6, if task has been solved.
        Rep         -   training report.
        OOBErrors   -   out-of-bag generalization error estimate

      -- ALGLIB --
         Copyright 10.03.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpetraines(mlpe.mlpensemble ensemble,
        double[,] xy,
        int npoints,
        double decay,
        int restarts,
        ref int info,
        mlpreport rep,
        xparams _params)
    {
        int i = 0;
        int k = 0;
        int ccount = 0;
        int pcount = 0;
        double[,] trnxy = new double[0, 0];
        double[,] valxy = new double[0, 0];
        int trnsize = 0;
        int valsize = 0;
        int tmpinfo = 0;
        mlpreport tmprep = new mlpreport();
        mlpbase.modelerrors moderr = new mlpbase.modelerrors();
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        int i_ = 0;
        int i1_ = 0;

        info = 0;

        nin = mlpbase.mlpgetinputscount(ensemble.network, _params);
        nout = mlpbase.mlpgetoutputscount(ensemble.network, _params);
        wcount = mlpbase.mlpgetweightscount(ensemble.network, _params);
        if ((npoints < 2 || restarts < 1) || (double)(decay) < (double)(0))
        {
            info = -1;
            return;
        }
        if (mlpbase.mlpissoftmax(ensemble.network, _params))
        {
            for (i = 0; i <= npoints - 1; i++)
            {
                if ((int)Math.Round(xy[i, nin]) < 0 || (int)Math.Round(xy[i, nin]) >= nout)
                {
                    info = -2;
                    return;
                }
            }
        }
        info = 6;

        //
        // allocate
        //
        if (mlpbase.mlpissoftmax(ensemble.network, _params))
        {
            ccount = nin + 1;
            pcount = nin;
        }
        else
        {
            ccount = nin + nout;
            pcount = nin + nout;
        }
        trnxy = new double[npoints, ccount];
        valxy = new double[npoints, ccount];
        rep.ngrad = 0;
        rep.nhess = 0;
        rep.ncholesky = 0;

        //
        // train networks
        //
        for (k = 0; k <= ensemble.ensemblesize - 1; k++)
        {

            //
            // Split set
            //
            do
            {
                trnsize = 0;
                valsize = 0;
                for (i = 0; i <= npoints - 1; i++)
                {
                    if ((double)(math.randomreal()) < (double)(0.66))
                    {

                        //
                        // Assign sample to training set
                        //
                        for (i_ = 0; i_ <= ccount - 1; i_++)
                        {
                            trnxy[trnsize, i_] = xy[i, i_];
                        }
                        trnsize = trnsize + 1;
                    }
                    else
                    {

                        //
                        // Assign sample to validation set
                        //
                        for (i_ = 0; i_ <= ccount - 1; i_++)
                        {
                            valxy[valsize, i_] = xy[i, i_];
                        }
                        valsize = valsize + 1;
                    }
                }
            }
            while (!(trnsize != 0 && valsize != 0));

            //
            // Train
            //
            mlptraines(ensemble.network, trnxy, trnsize, valxy, valsize, decay, restarts, ref tmpinfo, tmprep, _params);
            if (tmpinfo < 0)
            {
                info = tmpinfo;
                return;
            }

            //
            // save results
            //
            i1_ = (0) - (k * wcount);
            for (i_ = k * wcount; i_ <= (k + 1) * wcount - 1; i_++)
            {
                ensemble.weights[i_] = ensemble.network.weights[i_ + i1_];
            }
            i1_ = (0) - (k * pcount);
            for (i_ = k * pcount; i_ <= (k + 1) * pcount - 1; i_++)
            {
                ensemble.columnmeans[i_] = ensemble.network.columnmeans[i_ + i1_];
            }
            i1_ = (0) - (k * pcount);
            for (i_ = k * pcount; i_ <= (k + 1) * pcount - 1; i_++)
            {
                ensemble.columnsigmas[i_] = ensemble.network.columnsigmas[i_ + i1_];
            }
            rep.ngrad = rep.ngrad + tmprep.ngrad;
            rep.nhess = rep.nhess + tmprep.nhess;
            rep.ncholesky = rep.ncholesky + tmprep.ncholesky;
        }
        mlpe.mlpeallerrorsx(ensemble, xy, ensemble.network.dummysxy, npoints, 0, ensemble.network.dummyidx, 0, npoints, 0, ensemble.network.buf, moderr, _params);
        rep.relclserror = moderr.relclserror;
        rep.avgce = moderr.avgce;
        rep.rmserror = moderr.rmserror;
        rep.avgerror = moderr.avgerror;
        rep.avgrelerror = moderr.avgrelerror;
    }


    /*************************************************************************
    This function trains neural network ensemble passed to this function using
    current dataset and early stopping training algorithm. Each early stopping
    round performs NRestarts  random  restarts  (thus,  EnsembleSize*NRestarts
    training rounds is performed in total).

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
        S           -   trainer object;
        Ensemble    -   neural network ensemble. It must have same  number  of
                        inputs and outputs/classes  as  was  specified  during
                        creation of the trainer object.
        NRestarts   -   number of restarts, >=0:
                        * NRestarts>0 means that specified  number  of  random
                          restarts are performed during each ES round;
                        * NRestarts=0 is silently replaced by 1.

    OUTPUT PARAMETERS:
        Ensemble    -   trained ensemble;
        Rep         -   it contains all type of errors.
        
    NOTE: this training method uses BOTH early stopping and weight decay!  So,
          you should select weight decay before starting training just as  you
          select it before training "conventional" networks.

    NOTE: when no dataset was specified with MLPSetDataset/SetSparseDataset(),
          or  single-point  dataset  was  passed,  ensemble  is filled by zero
          values.

    NOTE: this method uses sum-of-squares error function for training.

      -- ALGLIB --
         Copyright 22.08.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void mlptrainensemblees(mlptrainer s,
        mlpe.mlpensemble ensemble,
        int nrestarts,
        mlpreport rep,
        xparams _params)
    {
        int nin = 0;
        int nout = 0;
        int ntype = 0;
        int ttype = 0;
        smp.shared_pool esessions = new smp.shared_pool();
        apserv.sinteger sgrad = new apserv.sinteger();
        mlpbase.modelerrors tmprep = new mlpbase.modelerrors();

        ap.assert(s.npoints >= 0, "MLPTrainEnsembleES: parameter S is not initialized or is spoiled(S.NPoints<0)");
        if (!mlpe.mlpeissoftmax(ensemble, _params))
        {
            ntype = 0;
        }
        else
        {
            ntype = 1;
        }
        if (s.rcpar)
        {
            ttype = 0;
        }
        else
        {
            ttype = 1;
        }
        ap.assert(ntype == ttype, "MLPTrainEnsembleES: internal error - type of input network is not similar to network type in trainer object");
        nin = mlpbase.mlpgetinputscount(ensemble.network, _params);
        ap.assert(s.nin == nin, "MLPTrainEnsembleES: number of inputs in trainer is not equal to number of inputs in ensemble network");
        nout = mlpbase.mlpgetoutputscount(ensemble.network, _params);
        ap.assert(s.nout == nout, "MLPTrainEnsembleES: number of outputs in trainer is not equal to number of outputs in ensemble network");
        ap.assert(nrestarts >= 0, "MLPTrainEnsembleES: NRestarts<0.");

        //
        // Initialize parameter Rep
        //
        rep.relclserror = 0;
        rep.avgce = 0;
        rep.rmserror = 0;
        rep.avgerror = 0;
        rep.avgrelerror = 0;
        rep.ngrad = 0;
        rep.nhess = 0;
        rep.ncholesky = 0;

        //
        // Allocate
        //
        apserv.ivectorsetlengthatleast(ref s.subset, s.npoints, _params);
        apserv.ivectorsetlengthatleast(ref s.valsubset, s.npoints, _params);

        //
        // Start training
        //
        // NOTE: ESessions is not initialized because MLPTrainEnsembleX
        //       needs uninitialized pool.
        //
        sgrad.val = 0;
        mlptrainensemblex(s, ensemble, 0, ensemble.ensemblesize, nrestarts, 0, sgrad, true, esessions, _params);
        rep.ngrad = sgrad.val;

        //
        // Calculate errors.
        //
        if (s.datatype == 0)
        {
            mlpe.mlpeallerrorsx(ensemble, s.densexy, s.sparsexy, s.npoints, 0, ensemble.network.dummyidx, 0, s.npoints, 0, ensemble.network.buf, tmprep, _params);
        }
        if (s.datatype == 1)
        {
            mlpe.mlpeallerrorsx(ensemble, s.densexy, s.sparsexy, s.npoints, 1, ensemble.network.dummyidx, 0, s.npoints, 0, ensemble.network.buf, tmprep, _params);
        }
        rep.relclserror = tmprep.relclserror;
        rep.avgce = tmprep.avgce;
        rep.rmserror = tmprep.rmserror;
        rep.avgerror = tmprep.avgerror;
        rep.avgrelerror = tmprep.avgrelerror;
    }


    /*************************************************************************
    Internal cross-validation subroutine
    *************************************************************************/
    private static void mlpkfoldcvgeneral(mlpbase.multilayerperceptron n,
        double[,] xy,
        int npoints,
        double decay,
        int restarts,
        int foldscount,
        bool lmalgorithm,
        double wstep,
        int maxits,
        ref int info,
        mlpreport rep,
        mlpcvreport cvrep,
        xparams _params)
    {
        int i = 0;
        int fold = 0;
        int j = 0;
        int k = 0;
        mlpbase.multilayerperceptron network = new mlpbase.multilayerperceptron();
        int nin = 0;
        int nout = 0;
        int rowlen = 0;
        int wcount = 0;
        int nclasses = 0;
        int tssize = 0;
        int cvssize = 0;
        double[,] cvset = new double[0, 0];
        double[,] testset = new double[0, 0];
        int[] folds = new int[0];
        int relcnt = 0;
        mlpreport internalrep = new mlpreport();
        double[] x = new double[0];
        double[] y = new double[0];
        int i_ = 0;

        info = 0;


        //
        // Read network geometry, test parameters
        //
        mlpbase.mlpproperties(n, ref nin, ref nout, ref wcount, _params);
        if (mlpbase.mlpissoftmax(n, _params))
        {
            nclasses = nout;
            rowlen = nin + 1;
        }
        else
        {
            nclasses = -nout;
            rowlen = nin + nout;
        }
        if ((npoints <= 0 || foldscount < 2) || foldscount > npoints)
        {
            info = -1;
            return;
        }
        mlpbase.mlpcopy(n, network, _params);

        //
        // K-fold out cross-validation.
        // First, estimate generalization error
        //
        testset = new double[npoints - 1 + 1, rowlen - 1 + 1];
        cvset = new double[npoints - 1 + 1, rowlen - 1 + 1];
        x = new double[nin - 1 + 1];
        y = new double[nout - 1 + 1];
        mlpkfoldsplit(xy, npoints, nclasses, foldscount, false, ref folds, _params);
        cvrep.relclserror = 0;
        cvrep.avgce = 0;
        cvrep.rmserror = 0;
        cvrep.avgerror = 0;
        cvrep.avgrelerror = 0;
        rep.ngrad = 0;
        rep.nhess = 0;
        rep.ncholesky = 0;
        relcnt = 0;
        for (fold = 0; fold <= foldscount - 1; fold++)
        {

            //
            // Separate set
            //
            tssize = 0;
            cvssize = 0;
            for (i = 0; i <= npoints - 1; i++)
            {
                if (folds[i] == fold)
                {
                    for (i_ = 0; i_ <= rowlen - 1; i_++)
                    {
                        testset[tssize, i_] = xy[i, i_];
                    }
                    tssize = tssize + 1;
                }
                else
                {
                    for (i_ = 0; i_ <= rowlen - 1; i_++)
                    {
                        cvset[cvssize, i_] = xy[i, i_];
                    }
                    cvssize = cvssize + 1;
                }
            }

            //
            // Train on CV training set
            //
            if (lmalgorithm)
            {
                mlptrainlm(network, cvset, cvssize, decay, restarts, ref info, internalrep, _params);
            }
            else
            {
                mlptrainlbfgs(network, cvset, cvssize, decay, restarts, wstep, maxits, ref info, internalrep, _params);
            }
            if (info < 0)
            {
                cvrep.relclserror = 0;
                cvrep.avgce = 0;
                cvrep.rmserror = 0;
                cvrep.avgerror = 0;
                cvrep.avgrelerror = 0;
                return;
            }
            rep.ngrad = rep.ngrad + internalrep.ngrad;
            rep.nhess = rep.nhess + internalrep.nhess;
            rep.ncholesky = rep.ncholesky + internalrep.ncholesky;

            //
            // Estimate error using CV test set
            //
            if (mlpbase.mlpissoftmax(network, _params))
            {

                //
                // classification-only code
                //
                cvrep.relclserror = cvrep.relclserror + mlpbase.mlpclserror(network, testset, tssize, _params);
                cvrep.avgce = cvrep.avgce + mlpbase.mlperrorn(network, testset, tssize, _params);
            }
            for (i = 0; i <= tssize - 1; i++)
            {
                for (i_ = 0; i_ <= nin - 1; i_++)
                {
                    x[i_] = testset[i, i_];
                }
                mlpbase.mlpprocess(network, x, ref y, _params);
                if (mlpbase.mlpissoftmax(network, _params))
                {

                    //
                    // Classification-specific code
                    //
                    k = (int)Math.Round(testset[i, nin]);
                    for (j = 0; j <= nout - 1; j++)
                    {
                        if (j == k)
                        {
                            cvrep.rmserror = cvrep.rmserror + math.sqr(y[j] - 1);
                            cvrep.avgerror = cvrep.avgerror + Math.Abs(y[j] - 1);
                            cvrep.avgrelerror = cvrep.avgrelerror + Math.Abs(y[j] - 1);
                            relcnt = relcnt + 1;
                        }
                        else
                        {
                            cvrep.rmserror = cvrep.rmserror + math.sqr(y[j]);
                            cvrep.avgerror = cvrep.avgerror + Math.Abs(y[j]);
                        }
                    }
                }
                else
                {

                    //
                    // Regression-specific code
                    //
                    for (j = 0; j <= nout - 1; j++)
                    {
                        cvrep.rmserror = cvrep.rmserror + math.sqr(y[j] - testset[i, nin + j]);
                        cvrep.avgerror = cvrep.avgerror + Math.Abs(y[j] - testset[i, nin + j]);
                        if ((double)(testset[i, nin + j]) != (double)(0))
                        {
                            cvrep.avgrelerror = cvrep.avgrelerror + Math.Abs((y[j] - testset[i, nin + j]) / testset[i, nin + j]);
                            relcnt = relcnt + 1;
                        }
                    }
                }
            }
        }
        if (mlpbase.mlpissoftmax(network, _params))
        {
            cvrep.relclserror = cvrep.relclserror / npoints;
            cvrep.avgce = cvrep.avgce / (Math.Log(2) * npoints);
        }
        cvrep.rmserror = Math.Sqrt(cvrep.rmserror / (npoints * nout));
        cvrep.avgerror = cvrep.avgerror / (npoints * nout);
        if (relcnt > 0)
        {
            cvrep.avgrelerror = cvrep.avgrelerror / relcnt;
        }
        info = 1;
    }


    /*************************************************************************
    Subroutine prepares K-fold split of the training set.

    NOTES:
        "NClasses>0" means that we have classification task.
        "NClasses<0" means regression task with -NClasses real outputs.
    *************************************************************************/
    private static void mlpkfoldsplit(double[,] xy,
        int npoints,
        int nclasses,
        int foldscount,
        bool stratifiedsplits,
        ref int[] folds,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        hqrnd.hqrndstate rs = new hqrnd.hqrndstate();

        folds = new int[0];


        //
        // test parameters
        //
        ap.assert(npoints > 0, "MLPKFoldSplit: wrong NPoints!");
        ap.assert(nclasses > 1 || nclasses < 0, "MLPKFoldSplit: wrong NClasses!");
        ap.assert(foldscount >= 2 && foldscount <= npoints, "MLPKFoldSplit: wrong FoldsCount!");
        ap.assert(!stratifiedsplits, "MLPKFoldSplit: stratified splits are not supported!");

        //
        // Folds
        //
        hqrnd.hqrndrandomize(rs, _params);
        folds = new int[npoints - 1 + 1];
        for (i = 0; i <= npoints - 1; i++)
        {
            folds[i] = i * foldscount / npoints;
        }
        for (i = 0; i <= npoints - 2; i++)
        {
            j = i + hqrnd.hqrnduniformi(rs, npoints - i, _params);
            if (j != i)
            {
                k = folds[i];
                folds[i] = folds[j];
                folds[j] = k;
            }
        }
    }


    /*************************************************************************
    Internal subroutine for parallelization function MLPFoldCV.


    INPUT PARAMETERS:
        S         -   trainer object;
        RowSize   -   row size(eitherNIn+NOut or NIn+1);
        NRestarts -   number of restarts(>=0);
        Folds     -   cross-validation set;
        Fold      -   the number of first cross-validation(>=0);
        DFold     -   the number of second cross-validation(>=Fold+1);
        CVY       -   parameter which stores  the result is returned by network,
                      training on I-th cross-validation set.
                      It has to be preallocated.
        PoolDataCV-   parameter for parallelization.
        WCount    -   number of weights in network, used to make decisions on
                      parallelization.
        
    NOTE: There are no checks on the parameters correctness.

      -- ALGLIB --
         Copyright 25.09.2012 by Bochkanov Sergey
    *************************************************************************/
    private static void mthreadcv(mlptrainer s,
        int rowsize,
        int nrestarts,
        int[] folds,
        int fold,
        int dfold,
        double[,] cvy,
        smp.shared_pool pooldatacv,
        int wcount,
        xparams _params)
    {
        mlpparallelizationcv datacv = null;
        int i = 0;
        int i_ = 0;

        if (fold == dfold - 1)
        {

            //
            // Separate set
            //
            smp.ae_shared_pool_retrieve(pooldatacv, ref datacv);
            datacv.subsetsize = 0;
            for (i = 0; i <= s.npoints - 1; i++)
            {
                if (folds[i] != fold)
                {
                    datacv.subset[datacv.subsetsize] = i;
                    datacv.subsetsize = datacv.subsetsize + 1;
                }
            }

            //
            // Train on CV training set
            //
            mlptrainnetworkx(s, nrestarts, -1, datacv.subset, datacv.subsetsize, datacv.subset, 0, datacv.network, datacv.rep, true, datacv.trnpool, _params);
            datacv.ngrad = datacv.ngrad + datacv.rep.ngrad;

            //
            // Estimate error using CV test set
            //
            for (i = 0; i <= s.npoints - 1; i++)
            {
                if (folds[i] == fold)
                {
                    if (s.datatype == 0)
                    {
                        for (i_ = 0; i_ <= rowsize - 1; i_++)
                        {
                            datacv.xyrow[i_] = s.densexy[i, i_];
                        }
                    }
                    if (s.datatype == 1)
                    {
                        sparse.sparsegetrow(s.sparsexy, i, ref datacv.xyrow, _params);
                    }
                    mlpbase.mlpprocess(datacv.network, datacv.xyrow, ref datacv.y, _params);
                    for (i_ = 0; i_ <= s.nout - 1; i_++)
                    {
                        cvy[i, i_] = datacv.y[i_];
                    }
                }
            }
            smp.ae_shared_pool_recycle(pooldatacv, ref datacv);
        }
        else
        {
            ap.assert(fold < dfold - 1, "MThreadCV: internal error(Fold>DFold-1).");

            //
            // We expect that minimum number of iterations before convergence is 100.
            // Hence is our approach to evaluation of task complexity.
            //
            if ((double)(Math.Max(nrestarts, 1) * apserv.rmul3(2 * wcount, s.npoints, 100, _params)) >= (double)(apserv.smpactivationlevel(_params)))
            {
                if (_trypexec_mthreadcv(s, rowsize, nrestarts, folds, fold, dfold, cvy, pooldatacv, wcount, _params))
                {
                    return;
                }
            }

            //
            // Split task
            //
            mthreadcv(s, rowsize, nrestarts, folds, fold, (fold + dfold) / 2, cvy, pooldatacv, wcount, _params);
            mthreadcv(s, rowsize, nrestarts, folds, (fold + dfold) / 2, dfold, cvy, pooldatacv, wcount, _params);
        }
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_mthreadcv(mlptrainer s,
        int rowsize,
        int nrestarts,
        int[] folds,
        int fold,
        int dfold,
        double[,] cvy,
        smp.shared_pool pooldatacv,
        int wcount, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    This function trains neural network passed to this function, using current
    dataset (one which was passed to MLPSetDataset() or MLPSetSparseDataset())
    and current training settings. Training  from  NRestarts  random  starting
    positions is performed, best network is chosen.

    This function is inteded to be used internally. It may be used in  several
    settings:
    * training with ValSubsetSize=0, corresponds  to  "normal"  training  with
      termination  criteria  based on S.MaxIts (steps count) and S.WStep (step
      size). Training sample is given by TrnSubset/TrnSubsetSize.
    * training with ValSubsetSize>0, corresponds to  early  stopping  training
      with additional MaxIts/WStep stopping criteria. Training sample is given
      by TrnSubset/TrnSubsetSize, validation sample  is  given  by  ValSubset/
      ValSubsetSize.

      -- ALGLIB --
         Copyright 13.08.2012 by Bochkanov Sergey
    *************************************************************************/
    private static void mlptrainnetworkx(mlptrainer s,
        int nrestarts,
        int algokind,
        int[] trnsubset,
        int trnsubsetsize,
        int[] valsubset,
        int valsubsetsize,
        mlpbase.multilayerperceptron network,
        mlpreport rep,
        bool isrootcall,
        smp.shared_pool sessions,
        xparams _params)
    {
        mlpbase.modelerrors modrep = new mlpbase.modelerrors();
        double eval = 0;
        double ebest = 0;
        int ngradbatch = 0;
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        int pcount = 0;
        int itbest = 0;
        int itcnt = 0;
        int ntype = 0;
        int ttype = 0;
        bool rndstart = new bool();
        int i = 0;
        int nr0 = 0;
        int nr1 = 0;
        mlpreport rep0 = new mlpreport();
        mlpreport rep1 = new mlpreport();
        bool randomizenetwork = new bool();
        double bestrmserror = 0;
        smlptrnsession psession = null;
        int i_ = 0;

        mlpbase.mlpproperties(network, ref nin, ref nout, ref wcount, _params);

        //
        // Process root call
        //
        if (isrootcall)
        {

            //
            // Try parallelization
            // We expect that minimum number of iterations before convergence is 100.
            // Hence is our approach to evaluation of task complexity.
            //
            if ((double)(Math.Max(nrestarts, 1) * apserv.rmul3(2 * wcount, s.npoints, 100, _params)) >= (double)(apserv.smpactivationlevel(_params)))
            {
                if (_trypexec_mlptrainnetworkx(s, nrestarts, algokind, trnsubset, trnsubsetsize, valsubset, valsubsetsize, network, rep, isrootcall, sessions, _params))
                {
                    return;
                }
            }

            //
            // Check correctness of parameters
            //
            ap.assert(algokind == 0 || algokind == -1, "MLPTrainNetworkX: unexpected AlgoKind");
            ap.assert(s.npoints >= 0, "MLPTrainNetworkX: internal error - parameter S is not initialized or is spoiled(S.NPoints<0)");
            if (s.rcpar)
            {
                ttype = 0;
            }
            else
            {
                ttype = 1;
            }
            if (!mlpbase.mlpissoftmax(network, _params))
            {
                ntype = 0;
            }
            else
            {
                ntype = 1;
            }
            ap.assert(ntype == ttype, "MLPTrainNetworkX: internal error - type of the training network is not similar to network type in trainer object");
            ap.assert(s.nin == nin, "MLPTrainNetworkX: internal error - number of inputs in trainer is not equal to number of inputs in the training network.");
            ap.assert(s.nout == nout, "MLPTrainNetworkX: internal error - number of outputs in trainer is not equal to number of outputs in the training network.");
            ap.assert(nrestarts >= 0, "MLPTrainNetworkX: internal error - NRestarts<0.");
            ap.assert(ap.len(trnsubset) >= trnsubsetsize, "MLPTrainNetworkX: internal error - parameter TrnSubsetSize more than input subset size(Length(TrnSubset)<TrnSubsetSize)");
            for (i = 0; i <= trnsubsetsize - 1; i++)
            {
                ap.assert(trnsubset[i] >= 0 && trnsubset[i] <= s.npoints - 1, "MLPTrainNetworkX: internal error - parameter TrnSubset contains incorrect index(TrnSubset[I]<0 or TrnSubset[I]>S.NPoints-1)");
            }
            ap.assert(ap.len(valsubset) >= valsubsetsize, "MLPTrainNetworkX: internal error - parameter ValSubsetSize more than input subset size(Length(ValSubset)<ValSubsetSize)");
            for (i = 0; i <= valsubsetsize - 1; i++)
            {
                ap.assert(valsubset[i] >= 0 && valsubset[i] <= s.npoints - 1, "MLPTrainNetworkX: internal error - parameter ValSubset contains incorrect index(ValSubset[I]<0 or ValSubset[I]>S.NPoints-1)");
            }

            //
            // Train
            //
            randomizenetwork = nrestarts > 0;
            initmlptrnsessions(network, randomizenetwork, s, sessions, _params);
            mlptrainnetworkx(s, nrestarts, algokind, trnsubset, trnsubsetsize, valsubset, valsubsetsize, network, rep, false, sessions, _params);

            //
            // Choose best network
            //
            bestrmserror = math.maxrealnumber;
            smp.ae_shared_pool_first_recycled(sessions, ref psession);
            while (psession != null)
            {
                if ((double)(psession.bestrmserror) < (double)(bestrmserror))
                {
                    mlpbase.mlpimporttunableparameters(network, psession.bestparameters, _params);
                    bestrmserror = psession.bestrmserror;
                }
                smp.ae_shared_pool_next_recycled(sessions, ref psession);
            }

            //
            // Calculate errors
            //
            if (s.datatype == 0)
            {
                mlpbase.mlpallerrorssubset(network, s.densexy, s.npoints, trnsubset, trnsubsetsize, modrep, _params);
            }
            if (s.datatype == 1)
            {
                mlpbase.mlpallerrorssparsesubset(network, s.sparsexy, s.npoints, trnsubset, trnsubsetsize, modrep, _params);
            }
            rep.relclserror = modrep.relclserror;
            rep.avgce = modrep.avgce;
            rep.rmserror = modrep.rmserror;
            rep.avgerror = modrep.avgerror;
            rep.avgrelerror = modrep.avgrelerror;

            //
            // Done
            //
            return;
        }

        //
        // Split problem, if we have more than 1 restart
        //
        if (nrestarts >= 2)
        {

            //
            // Divide problem with NRestarts into two: NR0 and NR1.
            //
            nr0 = nrestarts / 2;
            nr1 = nrestarts - nr0;
            mlptrainnetworkx(s, nr0, algokind, trnsubset, trnsubsetsize, valsubset, valsubsetsize, network, rep0, false, sessions, _params);
            mlptrainnetworkx(s, nr1, algokind, trnsubset, trnsubsetsize, valsubset, valsubsetsize, network, rep1, false, sessions, _params);

            //
            // Aggregate results
            //
            rep.ngrad = rep0.ngrad + rep1.ngrad;
            rep.nhess = rep0.nhess + rep1.nhess;
            rep.ncholesky = rep0.ncholesky + rep1.ncholesky;

            //
            // Done :)
            //
            return;
        }

        //
        // Execution with NRestarts=1 or NRestarts=0:
        // * NRestarts=1 means that network is restarted from random position
        // * NRestarts=0 means that network is not randomized
        //
        ap.assert(nrestarts == 0 || nrestarts == 1, "MLPTrainNetworkX: internal error");
        rep.ngrad = 0;
        rep.nhess = 0;
        rep.ncholesky = 0;
        smp.ae_shared_pool_retrieve(sessions, ref psession);
        if (((s.datatype == 0 || s.datatype == 1) && s.npoints > 0) && trnsubsetsize != 0)
        {

            //
            // Train network using combination of early stopping and step-size
            // and step-count based criteria. Network state with best value of
            // validation set error is stored in WBuf0. When validation set is
            // zero, most recent state of network is stored.
            //
            rndstart = nrestarts != 0;
            ngradbatch = 0;
            eval = 0;
            ebest = 0;
            itbest = 0;
            itcnt = 0;
            mlpstarttrainingx(s, rndstart, algokind, trnsubset, trnsubsetsize, psession, _params);
            if (s.datatype == 0)
            {
                ebest = mlpbase.mlperrorsubset(psession.network, s.densexy, s.npoints, valsubset, valsubsetsize, _params);
            }
            if (s.datatype == 1)
            {
                ebest = mlpbase.mlperrorsparsesubset(psession.network, s.sparsexy, s.npoints, valsubset, valsubsetsize, _params);
            }
            for (i_ = 0; i_ <= wcount - 1; i_++)
            {
                psession.wbuf0[i_] = psession.network.weights[i_];
            }
            while (mlpcontinuetrainingx(s, trnsubset, trnsubsetsize, ref ngradbatch, psession, _params))
            {
                if (s.datatype == 0)
                {
                    eval = mlpbase.mlperrorsubset(psession.network, s.densexy, s.npoints, valsubset, valsubsetsize, _params);
                }
                if (s.datatype == 1)
                {
                    eval = mlpbase.mlperrorsparsesubset(psession.network, s.sparsexy, s.npoints, valsubset, valsubsetsize, _params);
                }
                if ((double)(eval) <= (double)(ebest) || valsubsetsize == 0)
                {
                    for (i_ = 0; i_ <= wcount - 1; i_++)
                    {
                        psession.wbuf0[i_] = psession.network.weights[i_];
                    }
                    ebest = eval;
                    itbest = itcnt;
                }
                if (itcnt > 30 && (double)(itcnt) > (double)(1.5 * itbest))
                {
                    break;
                }
                itcnt = itcnt + 1;
            }
            for (i_ = 0; i_ <= wcount - 1; i_++)
            {
                psession.network.weights[i_] = psession.wbuf0[i_];
            }
            rep.ngrad = ngradbatch;
        }
        else
        {
            for (i = 0; i <= wcount - 1; i++)
            {
                psession.network.weights[i] = 0;
            }
        }

        //
        // Evaluate network performance and update PSession.BestParameters/BestRMSError
        // (if needed).
        //
        if (s.datatype == 0)
        {
            mlpbase.mlpallerrorssubset(psession.network, s.densexy, s.npoints, trnsubset, trnsubsetsize, modrep, _params);
        }
        if (s.datatype == 1)
        {
            mlpbase.mlpallerrorssparsesubset(psession.network, s.sparsexy, s.npoints, trnsubset, trnsubsetsize, modrep, _params);
        }
        if ((double)(modrep.rmserror) < (double)(psession.bestrmserror))
        {
            mlpbase.mlpexporttunableparameters(psession.network, ref psession.bestparameters, ref pcount, _params);
            psession.bestrmserror = modrep.rmserror;
        }

        //
        // Move session back to pool
        //
        smp.ae_shared_pool_recycle(sessions, ref psession);
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_mlptrainnetworkx(mlptrainer s,
        int nrestarts,
        int algokind,
        int[] trnsubset,
        int trnsubsetsize,
        int[] valsubset,
        int valsubsetsize,
        mlpbase.multilayerperceptron network,
        mlpreport rep,
        bool isrootcall,
        smp.shared_pool sessions, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    This function trains neural network ensemble passed to this function using
    current dataset and early stopping training algorithm. Each early stopping
    round performs NRestarts  random  restarts  (thus,  EnsembleSize*NRestarts
    training rounds is performed in total).


      -- ALGLIB --
         Copyright 22.08.2012 by Bochkanov Sergey
    *************************************************************************/
    private static void mlptrainensemblex(mlptrainer s,
        mlpe.mlpensemble ensemble,
        int idx0,
        int idx1,
        int nrestarts,
        int trainingmethod,
        apserv.sinteger ngrad,
        bool isrootcall,
        smp.shared_pool esessions,
        xparams _params)
    {
        int pcount = 0;
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        int trnsubsetsize = 0;
        int valsubsetsize = 0;
        int k0 = 0;
        apserv.sinteger ngrad0 = new apserv.sinteger();
        apserv.sinteger ngrad1 = new apserv.sinteger();
        mlpetrnsession psession = null;
        hqrnd.hqrndstate rs = new hqrnd.hqrndstate();
        int i_ = 0;
        int i1_ = 0;

        nin = mlpbase.mlpgetinputscount(ensemble.network, _params);
        nout = mlpbase.mlpgetoutputscount(ensemble.network, _params);
        wcount = mlpbase.mlpgetweightscount(ensemble.network, _params);
        if (mlpbase.mlpissoftmax(ensemble.network, _params))
        {
            pcount = nin;
        }
        else
        {
            pcount = nin + nout;
        }
        if (nrestarts <= 0)
        {
            nrestarts = 1;
        }

        //
        // Handle degenerate case
        //
        if (s.npoints < 2)
        {
            for (i = idx0; i <= idx1 - 1; i++)
            {
                for (j = 0; j <= wcount - 1; j++)
                {
                    ensemble.weights[i * wcount + j] = 0.0;
                }
                for (j = 0; j <= pcount - 1; j++)
                {
                    ensemble.columnmeans[i * pcount + j] = 0.0;
                    ensemble.columnsigmas[i * pcount + j] = 1.0;
                }
            }
            return;
        }

        //
        // Process root call
        //
        if (isrootcall)
        {

            //
            // Try parallelization
            // We expect that minimum number of iterations before convergence is 100.
            // Hence is our approach to evaluation of task complexity.
            //
            if ((double)(Math.Max(nrestarts, 1) * (idx1 - idx0) * apserv.rmul3(2 * wcount, s.npoints, 100, _params)) >= (double)(apserv.smpactivationlevel(_params)))
            {
                if (_trypexec_mlptrainensemblex(s, ensemble, idx0, idx1, nrestarts, trainingmethod, ngrad, isrootcall, esessions, _params))
                {
                    return;
                }
            }

            //
            // Prepare:
            // * prepare MLPETrnSessions
            // * fill ensemble by zeros (helps to detect errors)
            //
            initmlpetrnsessions(ensemble.network, s, esessions, _params);
            for (i = idx0; i <= idx1 - 1; i++)
            {
                for (j = 0; j <= wcount - 1; j++)
                {
                    ensemble.weights[i * wcount + j] = 0.0;
                }
                for (j = 0; j <= pcount - 1; j++)
                {
                    ensemble.columnmeans[i * pcount + j] = 0.0;
                    ensemble.columnsigmas[i * pcount + j] = 0.0;
                }
            }

            //
            // Train in non-root mode and exit
            //
            mlptrainensemblex(s, ensemble, idx0, idx1, nrestarts, trainingmethod, ngrad, false, esessions, _params);
            return;
        }

        //
        // Split problem
        //
        if (idx1 - idx0 >= 2)
        {
            k0 = (idx1 - idx0) / 2;
            ngrad0.val = 0;
            ngrad1.val = 0;
            mlptrainensemblex(s, ensemble, idx0, idx0 + k0, nrestarts, trainingmethod, ngrad0, false, esessions, _params);
            mlptrainensemblex(s, ensemble, idx0 + k0, idx1, nrestarts, trainingmethod, ngrad1, false, esessions, _params);
            ngrad.val = ngrad0.val + ngrad1.val;
            return;
        }

        //
        // Retrieve and prepare session
        //
        smp.ae_shared_pool_retrieve(esessions, ref psession);

        //
        // Train
        //
        hqrnd.hqrndrandomize(rs, _params);
        for (k = idx0; k <= idx1 - 1; k++)
        {

            //
            // Split set
            //
            trnsubsetsize = 0;
            valsubsetsize = 0;
            if (trainingmethod == 0)
            {
                do
                {
                    trnsubsetsize = 0;
                    valsubsetsize = 0;
                    for (i = 0; i <= s.npoints - 1; i++)
                    {
                        if ((double)(math.randomreal()) < (double)(0.66))
                        {

                            //
                            // Assign sample to training set
                            //
                            psession.trnsubset[trnsubsetsize] = i;
                            trnsubsetsize = trnsubsetsize + 1;
                        }
                        else
                        {

                            //
                            // Assign sample to validation set
                            //
                            psession.valsubset[valsubsetsize] = i;
                            valsubsetsize = valsubsetsize + 1;
                        }
                    }
                }
                while (!(trnsubsetsize != 0 && valsubsetsize != 0));
            }
            if (trainingmethod == 1)
            {
                valsubsetsize = 0;
                trnsubsetsize = s.npoints;
                for (i = 0; i <= s.npoints - 1; i++)
                {
                    psession.trnsubset[i] = hqrnd.hqrnduniformi(rs, s.npoints, _params);
                }
            }

            //
            // Train
            //
            mlptrainnetworkx(s, nrestarts, -1, psession.trnsubset, trnsubsetsize, psession.valsubset, valsubsetsize, psession.network, psession.mlprep, true, psession.mlpsessions, _params);
            ngrad.val = ngrad.val + psession.mlprep.ngrad;

            //
            // Save results
            //
            i1_ = (0) - (k * wcount);
            for (i_ = k * wcount; i_ <= (k + 1) * wcount - 1; i_++)
            {
                ensemble.weights[i_] = psession.network.weights[i_ + i1_];
            }
            i1_ = (0) - (k * pcount);
            for (i_ = k * pcount; i_ <= (k + 1) * pcount - 1; i_++)
            {
                ensemble.columnmeans[i_] = psession.network.columnmeans[i_ + i1_];
            }
            i1_ = (0) - (k * pcount);
            for (i_ = k * pcount; i_ <= (k + 1) * pcount - 1; i_++)
            {
                ensemble.columnsigmas[i_] = psession.network.columnsigmas[i_ + i1_];
            }
        }

        //
        // Recycle session
        //
        smp.ae_shared_pool_recycle(esessions, ref psession);
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_mlptrainensemblex(mlptrainer s,
        mlpe.mlpensemble ensemble,
        int idx0,
        int idx1,
        int nrestarts,
        int trainingmethod,
        apserv.sinteger ngrad,
        bool isrootcall,
        smp.shared_pool esessions, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    This function performs step-by-step training of the neural  network.  Here
    "step-by-step" means that training  starts  with  MLPStartTrainingX  call,
    and then user subsequently calls MLPContinueTrainingX  to perform one more
    iteration of the training.

    After call to this function trainer object remembers network and  is ready
    to  train  it.  However,  no  training  is  performed  until first call to 
    MLPContinueTraining() function. Subsequent calls  to MLPContinueTraining()
    will advance traing progress one iteration further.


      -- ALGLIB --
         Copyright 13.08.2012 by Bochkanov Sergey
    *************************************************************************/
    private static void mlpstarttrainingx(mlptrainer s,
        bool randomstart,
        int algokind,
        int[] subset,
        int subsetsize,
        smlptrnsession session,
        xparams _params)
    {
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        int ntype = 0;
        int ttype = 0;
        int i = 0;


        //
        // Check parameters
        //
        ap.assert(s.npoints >= 0, "MLPStartTrainingX: internal error - parameter S is not initialized or is spoiled(S.NPoints<0)");
        ap.assert(algokind == 0 || algokind == -1, "MLPStartTrainingX: unexpected AlgoKind");
        if (s.rcpar)
        {
            ttype = 0;
        }
        else
        {
            ttype = 1;
        }
        if (!mlpbase.mlpissoftmax(session.network, _params))
        {
            ntype = 0;
        }
        else
        {
            ntype = 1;
        }
        ap.assert(ntype == ttype, "MLPStartTrainingX: internal error - type of the resulting network is not similar to network type in trainer object");
        mlpbase.mlpproperties(session.network, ref nin, ref nout, ref wcount, _params);
        ap.assert(s.nin == nin, "MLPStartTrainingX: number of inputs in trainer is not equal to number of inputs in the network.");
        ap.assert(s.nout == nout, "MLPStartTrainingX: number of outputs in trainer is not equal to number of outputs in the network.");
        ap.assert(ap.len(subset) >= subsetsize, "MLPStartTrainingX: internal error - parameter SubsetSize more than input subset size(Length(Subset)<SubsetSize)");
        for (i = 0; i <= subsetsize - 1; i++)
        {
            ap.assert(subset[i] >= 0 && subset[i] <= s.npoints - 1, "MLPStartTrainingX: internal error - parameter Subset contains incorrect index(Subset[I]<0 or Subset[I]>S.NPoints-1)");
        }

        //
        // Prepare session
        //
        minlbfgs.minlbfgssetcond(session.optimizer, 0.0, 0.0, s.wstep, s.maxits, _params);
        if (s.npoints > 0 && subsetsize != 0)
        {
            if (randomstart)
            {
                mlpbase.mlprandomize(session.network, _params);
            }
            minlbfgs.minlbfgsrestartfrom(session.optimizer, session.network.weights, _params);
        }
        else
        {
            for (i = 0; i <= wcount - 1; i++)
            {
                session.network.weights[i] = 0;
            }
        }
        if (algokind == -1)
        {
            session.algoused = s.algokind;
            if (s.algokind == 1)
            {
                session.minibatchsize = s.minibatchsize;
            }
        }
        else
        {
            session.algoused = 0;
        }
        hqrnd.hqrndrandomize(session.generator, _params);
        session.rstate.ia = new int[15 + 1];
        session.rstate.ra = new double[1 + 1];
        session.rstate.stage = -1;
    }


    /*************************************************************************
    This function performs step-by-step training of the neural  network.  Here
    "step-by-step" means  that training starts  with  MLPStartTrainingX  call,
    and then user subsequently calls MLPContinueTrainingX  to perform one more
    iteration of the training.

    This  function  performs  one  more  iteration of the training and returns
    either True (training continues) or False (training stopped). In case True
    was returned, Network weights are updated according to the  current  state
    of the optimization progress. In case False was  returned,  no  additional
    updates is performed (previous update of  the  network weights moved us to
    the final point, and no additional updates is needed).

    EXAMPLE:
        >
        > [initialize network and trainer object]
        >
        > MLPStartTraining(Trainer, Network, True)
        > while MLPContinueTraining(Trainer, Network) do
        >     [visualize training progress]
        >


      -- ALGLIB --
         Copyright 13.08.2012 by Bochkanov Sergey
    *************************************************************************/
    private static bool mlpcontinuetrainingx(mlptrainer s,
        int[] subset,
        int subsetsize,
        ref int ngradbatch,
        smlptrnsession session,
        xparams _params)
    {
        bool result = new bool();
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        int twcount = 0;
        int ntype = 0;
        int ttype = 0;
        double decay = 0;
        double v = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        int trnsetsize = 0;
        int epoch = 0;
        int minibatchcount = 0;
        int minibatchidx = 0;
        int cursize = 0;
        int idx0 = 0;
        int idx1 = 0;
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
        if (session.rstate.stage >= 0)
        {
            nin = session.rstate.ia[0];
            nout = session.rstate.ia[1];
            wcount = session.rstate.ia[2];
            twcount = session.rstate.ia[3];
            ntype = session.rstate.ia[4];
            ttype = session.rstate.ia[5];
            i = session.rstate.ia[6];
            j = session.rstate.ia[7];
            k = session.rstate.ia[8];
            trnsetsize = session.rstate.ia[9];
            epoch = session.rstate.ia[10];
            minibatchcount = session.rstate.ia[11];
            minibatchidx = session.rstate.ia[12];
            cursize = session.rstate.ia[13];
            idx0 = session.rstate.ia[14];
            idx1 = session.rstate.ia[15];
            decay = session.rstate.ra[0];
            v = session.rstate.ra[1];
        }
        else
        {
            nin = 359;
            nout = -58;
            wcount = -919;
            twcount = -909;
            ntype = 81;
            ttype = 255;
            i = 74;
            j = -788;
            k = 809;
            trnsetsize = 205;
            epoch = -838;
            minibatchcount = 939;
            minibatchidx = -526;
            cursize = 763;
            idx0 = -541;
            idx1 = -698;
            decay = -900.0;
            v = -318.0;
        }
        if (session.rstate.stage == 0)
        {
            goto lbl_0;
        }

        //
        // Routine body
        //

        //
        // Check correctness of inputs
        //
        ap.assert(s.npoints >= 0, "MLPContinueTrainingX: internal error - parameter S is not initialized or is spoiled(S.NPoints<0).");
        if (s.rcpar)
        {
            ttype = 0;
        }
        else
        {
            ttype = 1;
        }
        if (!mlpbase.mlpissoftmax(session.network, _params))
        {
            ntype = 0;
        }
        else
        {
            ntype = 1;
        }
        ap.assert(ntype == ttype, "MLPContinueTrainingX: internal error - type of the resulting network is not similar to network type in trainer object.");
        mlpbase.mlpproperties(session.network, ref nin, ref nout, ref wcount, _params);
        ap.assert(s.nin == nin, "MLPContinueTrainingX: internal error - number of inputs in trainer is not equal to number of inputs in the network.");
        ap.assert(s.nout == nout, "MLPContinueTrainingX: internal error - number of outputs in trainer is not equal to number of outputs in the network.");
        ap.assert(ap.len(subset) >= subsetsize, "MLPContinueTrainingX: internal error - parameter SubsetSize more than input subset size(Length(Subset)<SubsetSize).");
        for (i = 0; i <= subsetsize - 1; i++)
        {
            ap.assert(subset[i] >= 0 && subset[i] <= s.npoints - 1, "MLPContinueTrainingX: internal error - parameter Subset contains incorrect index(Subset[I]<0 or Subset[I]>S.NPoints-1).");
        }

        //
        // Quick exit on empty training set
        //
        if (s.npoints == 0 || subsetsize == 0)
        {
            result = false;
            return result;
        }

        //
        // Minibatch training
        //
        if (session.algoused == 1)
        {
            ap.assert(false, "MINIBATCH TRAINING IS NOT IMPLEMENTED YET");
        }

        //
        // Last option: full batch training
        //
        decay = s.decay;
    lbl_1:
        if (!minlbfgs.minlbfgsiteration(session.optimizer, _params))
        {
            goto lbl_2;
        }
        if (!session.optimizer.xupdated)
        {
            goto lbl_3;
        }
        for (i_ = 0; i_ <= wcount - 1; i_++)
        {
            session.network.weights[i_] = session.optimizer.x[i_];
        }
        session.rstate.stage = 0;
        goto lbl_rcomm;
    lbl_0:
    lbl_3:
        for (i_ = 0; i_ <= wcount - 1; i_++)
        {
            session.network.weights[i_] = session.optimizer.x[i_];
        }
        if (s.datatype == 0)
        {
            mlpbase.mlpgradbatchsubset(session.network, s.densexy, s.npoints, subset, subsetsize, ref session.optimizer.f, ref session.optimizer.g, _params);
        }
        if (s.datatype == 1)
        {
            mlpbase.mlpgradbatchsparsesubset(session.network, s.sparsexy, s.npoints, subset, subsetsize, ref session.optimizer.f, ref session.optimizer.g, _params);
        }

        //
        // Increment number of operations performed on batch gradient
        //
        ngradbatch = ngradbatch + 1;
        v = 0.0;
        for (i_ = 0; i_ <= wcount - 1; i_++)
        {
            v += session.network.weights[i_] * session.network.weights[i_];
        }
        session.optimizer.f = session.optimizer.f + 0.5 * decay * v;
        for (i_ = 0; i_ <= wcount - 1; i_++)
        {
            session.optimizer.g[i_] = session.optimizer.g[i_] + decay * session.network.weights[i_];
        }
        goto lbl_1;
    lbl_2:
        minlbfgs.minlbfgsresultsbuf(session.optimizer, ref session.network.weights, session.optimizerrep, _params);
        result = false;
        return result;

    //
    // Saving state
    //
    lbl_rcomm:
        result = true;
        session.rstate.ia[0] = nin;
        session.rstate.ia[1] = nout;
        session.rstate.ia[2] = wcount;
        session.rstate.ia[3] = twcount;
        session.rstate.ia[4] = ntype;
        session.rstate.ia[5] = ttype;
        session.rstate.ia[6] = i;
        session.rstate.ia[7] = j;
        session.rstate.ia[8] = k;
        session.rstate.ia[9] = trnsetsize;
        session.rstate.ia[10] = epoch;
        session.rstate.ia[11] = minibatchcount;
        session.rstate.ia[12] = minibatchidx;
        session.rstate.ia[13] = cursize;
        session.rstate.ia[14] = idx0;
        session.rstate.ia[15] = idx1;
        session.rstate.ra[0] = decay;
        session.rstate.ra[1] = v;
        return result;
    }


    /*************************************************************************
    Internal bagging subroutine.

      -- ALGLIB --
         Copyright 19.02.2009 by Bochkanov Sergey
    *************************************************************************/
    private static void mlpebagginginternal(mlpe.mlpensemble ensemble,
        double[,] xy,
        int npoints,
        double decay,
        int restarts,
        double wstep,
        int maxits,
        bool lmalgorithm,
        ref int info,
        mlpreport rep,
        mlpcvreport ooberrors,
        xparams _params)
    {
        double[,] xys = new double[0, 0];
        bool[] s = new bool[0];
        double[,] oobbuf = new double[0, 0];
        int[] oobcntbuf = new int[0];
        double[] x = new double[0];
        double[] y = new double[0];
        double[] dy = new double[0];
        double[] dsbuf = new double[0];
        int ccnt = 0;
        int pcnt = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        double v = 0;
        mlpreport tmprep = new mlpreport();
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        hqrnd.hqrndstate rs = new hqrnd.hqrndstate();
        int i_ = 0;
        int i1_ = 0;

        info = 0;

        nin = mlpbase.mlpgetinputscount(ensemble.network, _params);
        nout = mlpbase.mlpgetoutputscount(ensemble.network, _params);
        wcount = mlpbase.mlpgetweightscount(ensemble.network, _params);

        //
        // Test for inputs
        //
        if ((!lmalgorithm && (double)(wstep) == (double)(0)) && maxits == 0)
        {
            info = -8;
            return;
        }
        if (((npoints <= 0 || restarts < 1) || (double)(wstep) < (double)(0)) || maxits < 0)
        {
            info = -1;
            return;
        }
        if (mlpbase.mlpissoftmax(ensemble.network, _params))
        {
            for (i = 0; i <= npoints - 1; i++)
            {
                if ((int)Math.Round(xy[i, nin]) < 0 || (int)Math.Round(xy[i, nin]) >= nout)
                {
                    info = -2;
                    return;
                }
            }
        }

        //
        // allocate temporaries
        //
        info = 2;
        rep.ngrad = 0;
        rep.nhess = 0;
        rep.ncholesky = 0;
        ooberrors.relclserror = 0;
        ooberrors.avgce = 0;
        ooberrors.rmserror = 0;
        ooberrors.avgerror = 0;
        ooberrors.avgrelerror = 0;
        if (mlpbase.mlpissoftmax(ensemble.network, _params))
        {
            ccnt = nin + 1;
            pcnt = nin;
        }
        else
        {
            ccnt = nin + nout;
            pcnt = nin + nout;
        }
        xys = new double[npoints, ccnt];
        s = new bool[npoints];
        oobbuf = new double[npoints, nout];
        oobcntbuf = new int[npoints];
        x = new double[nin];
        y = new double[nout];
        if (mlpbase.mlpissoftmax(ensemble.network, _params))
        {
            dy = new double[1];
        }
        else
        {
            dy = new double[nout];
        }
        for (i = 0; i <= npoints - 1; i++)
        {
            for (j = 0; j <= nout - 1; j++)
            {
                oobbuf[i, j] = 0;
            }
        }
        for (i = 0; i <= npoints - 1; i++)
        {
            oobcntbuf[i] = 0;
        }

        //
        // main bagging cycle
        //
        hqrnd.hqrndrandomize(rs, _params);
        for (k = 0; k <= ensemble.ensemblesize - 1; k++)
        {

            //
            // prepare dataset
            //
            for (i = 0; i <= npoints - 1; i++)
            {
                s[i] = false;
            }
            for (i = 0; i <= npoints - 1; i++)
            {
                j = hqrnd.hqrnduniformi(rs, npoints, _params);
                s[j] = true;
                for (i_ = 0; i_ <= ccnt - 1; i_++)
                {
                    xys[i, i_] = xy[j, i_];
                }
            }

            //
            // train
            //
            if (lmalgorithm)
            {
                mlptrainlm(ensemble.network, xys, npoints, decay, restarts, ref info, tmprep, _params);
            }
            else
            {
                mlptrainlbfgs(ensemble.network, xys, npoints, decay, restarts, wstep, maxits, ref info, tmprep, _params);
            }
            if (info < 0)
            {
                return;
            }

            //
            // save results
            //
            rep.ngrad = rep.ngrad + tmprep.ngrad;
            rep.nhess = rep.nhess + tmprep.nhess;
            rep.ncholesky = rep.ncholesky + tmprep.ncholesky;
            i1_ = (0) - (k * wcount);
            for (i_ = k * wcount; i_ <= (k + 1) * wcount - 1; i_++)
            {
                ensemble.weights[i_] = ensemble.network.weights[i_ + i1_];
            }
            i1_ = (0) - (k * pcnt);
            for (i_ = k * pcnt; i_ <= (k + 1) * pcnt - 1; i_++)
            {
                ensemble.columnmeans[i_] = ensemble.network.columnmeans[i_ + i1_];
            }
            i1_ = (0) - (k * pcnt);
            for (i_ = k * pcnt; i_ <= (k + 1) * pcnt - 1; i_++)
            {
                ensemble.columnsigmas[i_] = ensemble.network.columnsigmas[i_ + i1_];
            }

            //
            // OOB estimates
            //
            for (i = 0; i <= npoints - 1; i++)
            {
                if (!s[i])
                {
                    for (i_ = 0; i_ <= nin - 1; i_++)
                    {
                        x[i_] = xy[i, i_];
                    }
                    mlpbase.mlpprocess(ensemble.network, x, ref y, _params);
                    for (i_ = 0; i_ <= nout - 1; i_++)
                    {
                        oobbuf[i, i_] = oobbuf[i, i_] + y[i_];
                    }
                    oobcntbuf[i] = oobcntbuf[i] + 1;
                }
            }
        }

        //
        // OOB estimates
        //
        if (mlpbase.mlpissoftmax(ensemble.network, _params))
        {
            bdss.dserrallocate(nout, ref dsbuf, _params);
        }
        else
        {
            bdss.dserrallocate(-nout, ref dsbuf, _params);
        }
        for (i = 0; i <= npoints - 1; i++)
        {
            if (oobcntbuf[i] != 0)
            {
                v = (double)1 / (double)oobcntbuf[i];
                for (i_ = 0; i_ <= nout - 1; i_++)
                {
                    y[i_] = v * oobbuf[i, i_];
                }
                if (mlpbase.mlpissoftmax(ensemble.network, _params))
                {
                    dy[0] = xy[i, nin];
                }
                else
                {
                    i1_ = (nin) - (0);
                    for (i_ = 0; i_ <= nout - 1; i_++)
                    {
                        dy[i_] = v * xy[i, i_ + i1_];
                    }
                }
                bdss.dserraccumulate(ref dsbuf, y, dy, _params);
            }
        }
        bdss.dserrfinish(ref dsbuf, _params);
        ooberrors.relclserror = dsbuf[0];
        ooberrors.avgce = dsbuf[1];
        ooberrors.rmserror = dsbuf[2];
        ooberrors.avgerror = dsbuf[3];
        ooberrors.avgrelerror = dsbuf[4];
    }


    /*************************************************************************
    This function initializes temporaries needed for training session.


      -- ALGLIB --
         Copyright 01.07.2013 by Bochkanov Sergey
    *************************************************************************/
    private static void initmlptrnsession(mlpbase.multilayerperceptron networktrained,
        bool randomizenetwork,
        mlptrainer trainer,
        smlptrnsession session,
        xparams _params)
    {
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        int pcount = 0;
        int[] dummysubset = new int[0];


        //
        // Prepare network:
        // * copy input network to Session.Network
        // * re-initialize preprocessor and weights if RandomizeNetwork=True
        //
        mlpbase.mlpcopy(networktrained, session.network, _params);
        if (randomizenetwork)
        {
            ap.assert(trainer.datatype == 0 || trainer.datatype == 1, "InitTemporaries: unexpected Trainer.DataType");
            if (trainer.datatype == 0)
            {
                mlpbase.mlpinitpreprocessorsubset(session.network, trainer.densexy, trainer.npoints, dummysubset, -1, _params);
            }
            if (trainer.datatype == 1)
            {
                mlpbase.mlpinitpreprocessorsparsesubset(session.network, trainer.sparsexy, trainer.npoints, dummysubset, -1, _params);
            }
            mlpbase.mlprandomize(session.network, _params);
            session.randomizenetwork = true;
        }
        else
        {
            session.randomizenetwork = false;
        }

        //
        // Determine network geometry and initialize optimizer 
        //
        mlpbase.mlpproperties(session.network, ref nin, ref nout, ref wcount, _params);
        minlbfgs.minlbfgscreate(wcount, Math.Min(wcount, trainer.lbfgsfactor), session.network.weights, session.optimizer, _params);
        minlbfgs.minlbfgssetxrep(session.optimizer, true, _params);

        //
        // Create buffers
        //
        session.wbuf0 = new double[wcount];
        session.wbuf1 = new double[wcount];

        //
        // Initialize session result
        //
        mlpbase.mlpexporttunableparameters(session.network, ref session.bestparameters, ref pcount, _params);
        session.bestrmserror = math.maxrealnumber;
    }


    /*************************************************************************
    This function initializes temporaries needed for training session.

    *************************************************************************/
    private static void initmlptrnsessions(mlpbase.multilayerperceptron networktrained,
        bool randomizenetwork,
        mlptrainer trainer,
        smp.shared_pool sessions,
        xparams _params)
    {
        int[] dummysubset = new int[0];
        smlptrnsession t = new smlptrnsession();
        smlptrnsession p = null;

        if (smp.ae_shared_pool_is_initialized(sessions))
        {

            //
            // Pool was already initialized.
            // Clear sessions stored in the pool.
            //
            smp.ae_shared_pool_first_recycled(sessions, ref p);
            while (p != null)
            {
                ap.assert(mlpbase.mlpsamearchitecture(p.network, networktrained, _params), "InitMLPTrnSessions: internal consistency error");
                p.bestrmserror = math.maxrealnumber;
                smp.ae_shared_pool_next_recycled(sessions, ref p);
            }
        }
        else
        {

            //
            // Prepare session and seed pool
            //
            initmlptrnsession(networktrained, randomizenetwork, trainer, t, _params);
            smp.ae_shared_pool_set_seed(sessions, t);
        }
    }


    /*************************************************************************
    This function initializes temporaries needed for ensemble training.

    *************************************************************************/
    private static void initmlpetrnsession(mlpbase.multilayerperceptron individualnetwork,
        mlptrainer trainer,
        mlpetrnsession session,
        xparams _params)
    {
        int[] dummysubset = new int[0];


        //
        // Prepare network:
        // * copy input network to Session.Network
        // * re-initialize preprocessor and weights if RandomizeNetwork=True
        //
        mlpbase.mlpcopy(individualnetwork, session.network, _params);
        initmlptrnsessions(individualnetwork, true, trainer, session.mlpsessions, _params);
        apserv.ivectorsetlengthatleast(ref session.trnsubset, trainer.npoints, _params);
        apserv.ivectorsetlengthatleast(ref session.valsubset, trainer.npoints, _params);
    }


    /*************************************************************************
    This function initializes temporaries needed for training session.

    *************************************************************************/
    private static void initmlpetrnsessions(mlpbase.multilayerperceptron individualnetwork,
        mlptrainer trainer,
        smp.shared_pool sessions,
        xparams _params)
    {
        mlpetrnsession t = new mlpetrnsession();

        if (!smp.ae_shared_pool_is_initialized(sessions))
        {
            initmlpetrnsession(individualnetwork, trainer, t, _params);
            smp.ae_shared_pool_set_seed(sessions, t);
        }
    }


}
