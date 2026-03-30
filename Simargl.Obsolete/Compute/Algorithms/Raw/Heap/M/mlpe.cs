using System;

#pragma warning disable CS8618
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

public class mlpe
{
    /*************************************************************************
    Neural networks ensemble
    *************************************************************************/
    public class mlpensemble : apobject
    {
        public int ensemblesize;
        public double[] weights;
        public double[] columnmeans;
        public double[] columnsigmas;
        public mlpbase.multilayerperceptron network;
        public double[] y;
        public mlpensemble()
        {
            init();
        }
        public override void init()
        {
            weights = new double[0];
            columnmeans = new double[0];
            columnsigmas = new double[0];
            network = new mlpbase.multilayerperceptron();
            y = new double[0];
        }
        public override apobject make_copy()
        {
            mlpensemble _result = new mlpensemble();
            _result.ensemblesize = ensemblesize;
            _result.weights = (double[])weights.Clone();
            _result.columnmeans = (double[])columnmeans.Clone();
            _result.columnsigmas = (double[])columnsigmas.Clone();
            _result.network = (mlpbase.multilayerperceptron)network.make_copy();
            _result.y = (double[])y.Clone();
            return _result;
        }
    };




    public const int mlpefirstversion = 1;


    /*************************************************************************
    Like MLPCreate0, but for ensembles.

      -- ALGLIB --
         Copyright 18.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpecreate0(int nin,
        int nout,
        int ensemblesize,
        mlpensemble ensemble,
        xparams _params)
    {
        mlpbase.multilayerperceptron net = new mlpbase.multilayerperceptron();

        mlpbase.mlpcreate0(nin, nout, net, _params);
        mlpecreatefromnetwork(net, ensemblesize, ensemble, _params);
    }


    /*************************************************************************
    Like MLPCreate1, but for ensembles.

      -- ALGLIB --
         Copyright 18.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpecreate1(int nin,
        int nhid,
        int nout,
        int ensemblesize,
        mlpensemble ensemble,
        xparams _params)
    {
        mlpbase.multilayerperceptron net = new mlpbase.multilayerperceptron();

        mlpbase.mlpcreate1(nin, nhid, nout, net, _params);
        mlpecreatefromnetwork(net, ensemblesize, ensemble, _params);
    }


    /*************************************************************************
    Like MLPCreate2, but for ensembles.

      -- ALGLIB --
         Copyright 18.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpecreate2(int nin,
        int nhid1,
        int nhid2,
        int nout,
        int ensemblesize,
        mlpensemble ensemble,
        xparams _params)
    {
        mlpbase.multilayerperceptron net = new mlpbase.multilayerperceptron();

        mlpbase.mlpcreate2(nin, nhid1, nhid2, nout, net, _params);
        mlpecreatefromnetwork(net, ensemblesize, ensemble, _params);
    }


    /*************************************************************************
    Like MLPCreateB0, but for ensembles.

      -- ALGLIB --
         Copyright 18.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpecreateb0(int nin,
        int nout,
        double b,
        double d,
        int ensemblesize,
        mlpensemble ensemble,
        xparams _params)
    {
        mlpbase.multilayerperceptron net = new mlpbase.multilayerperceptron();

        mlpbase.mlpcreateb0(nin, nout, b, d, net, _params);
        mlpecreatefromnetwork(net, ensemblesize, ensemble, _params);
    }


    /*************************************************************************
    Like MLPCreateB1, but for ensembles.

      -- ALGLIB --
         Copyright 18.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpecreateb1(int nin,
        int nhid,
        int nout,
        double b,
        double d,
        int ensemblesize,
        mlpensemble ensemble,
        xparams _params)
    {
        mlpbase.multilayerperceptron net = new mlpbase.multilayerperceptron();

        mlpbase.mlpcreateb1(nin, nhid, nout, b, d, net, _params);
        mlpecreatefromnetwork(net, ensemblesize, ensemble, _params);
    }


    /*************************************************************************
    Like MLPCreateB2, but for ensembles.

      -- ALGLIB --
         Copyright 18.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpecreateb2(int nin,
        int nhid1,
        int nhid2,
        int nout,
        double b,
        double d,
        int ensemblesize,
        mlpensemble ensemble,
        xparams _params)
    {
        mlpbase.multilayerperceptron net = new mlpbase.multilayerperceptron();

        mlpbase.mlpcreateb2(nin, nhid1, nhid2, nout, b, d, net, _params);
        mlpecreatefromnetwork(net, ensemblesize, ensemble, _params);
    }


    /*************************************************************************
    Like MLPCreateR0, but for ensembles.

      -- ALGLIB --
         Copyright 18.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpecreater0(int nin,
        int nout,
        double a,
        double b,
        int ensemblesize,
        mlpensemble ensemble,
        xparams _params)
    {
        mlpbase.multilayerperceptron net = new mlpbase.multilayerperceptron();

        mlpbase.mlpcreater0(nin, nout, a, b, net, _params);
        mlpecreatefromnetwork(net, ensemblesize, ensemble, _params);
    }


    /*************************************************************************
    Like MLPCreateR1, but for ensembles.

      -- ALGLIB --
         Copyright 18.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpecreater1(int nin,
        int nhid,
        int nout,
        double a,
        double b,
        int ensemblesize,
        mlpensemble ensemble,
        xparams _params)
    {
        mlpbase.multilayerperceptron net = new mlpbase.multilayerperceptron();

        mlpbase.mlpcreater1(nin, nhid, nout, a, b, net, _params);
        mlpecreatefromnetwork(net, ensemblesize, ensemble, _params);
    }


    /*************************************************************************
    Like MLPCreateR2, but for ensembles.

      -- ALGLIB --
         Copyright 18.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpecreater2(int nin,
        int nhid1,
        int nhid2,
        int nout,
        double a,
        double b,
        int ensemblesize,
        mlpensemble ensemble,
        xparams _params)
    {
        mlpbase.multilayerperceptron net = new mlpbase.multilayerperceptron();

        mlpbase.mlpcreater2(nin, nhid1, nhid2, nout, a, b, net, _params);
        mlpecreatefromnetwork(net, ensemblesize, ensemble, _params);
    }


    /*************************************************************************
    Like MLPCreateC0, but for ensembles.

      -- ALGLIB --
         Copyright 18.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpecreatec0(int nin,
        int nout,
        int ensemblesize,
        mlpensemble ensemble,
        xparams _params)
    {
        mlpbase.multilayerperceptron net = new mlpbase.multilayerperceptron();

        mlpbase.mlpcreatec0(nin, nout, net, _params);
        mlpecreatefromnetwork(net, ensemblesize, ensemble, _params);
    }


    /*************************************************************************
    Like MLPCreateC1, but for ensembles.

      -- ALGLIB --
         Copyright 18.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpecreatec1(int nin,
        int nhid,
        int nout,
        int ensemblesize,
        mlpensemble ensemble,
        xparams _params)
    {
        mlpbase.multilayerperceptron net = new mlpbase.multilayerperceptron();

        mlpbase.mlpcreatec1(nin, nhid, nout, net, _params);
        mlpecreatefromnetwork(net, ensemblesize, ensemble, _params);
    }


    /*************************************************************************
    Like MLPCreateC2, but for ensembles.

      -- ALGLIB --
         Copyright 18.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpecreatec2(int nin,
        int nhid1,
        int nhid2,
        int nout,
        int ensemblesize,
        mlpensemble ensemble,
        xparams _params)
    {
        mlpbase.multilayerperceptron net = new mlpbase.multilayerperceptron();

        mlpbase.mlpcreatec2(nin, nhid1, nhid2, nout, net, _params);
        mlpecreatefromnetwork(net, ensemblesize, ensemble, _params);
    }


    /*************************************************************************
    Creates ensemble from network. Only network geometry is copied.

      -- ALGLIB --
         Copyright 17.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpecreatefromnetwork(mlpbase.multilayerperceptron network,
        int ensemblesize,
        mlpensemble ensemble,
        xparams _params)
    {
        int i = 0;
        int ccount = 0;
        int wcount = 0;
        int i_ = 0;
        int i1_ = 0;

        ap.assert(ensemblesize > 0, "MLPECreate: incorrect ensemble size!");

        //
        // Copy network
        //
        mlpbase.mlpcopy(network, ensemble.network, _params);

        //
        // network properties
        //
        if (mlpbase.mlpissoftmax(network, _params))
        {
            ccount = mlpbase.mlpgetinputscount(ensemble.network, _params);
        }
        else
        {
            ccount = mlpbase.mlpgetinputscount(ensemble.network, _params) + mlpbase.mlpgetoutputscount(ensemble.network, _params);
        }
        wcount = mlpbase.mlpgetweightscount(ensemble.network, _params);
        ensemble.ensemblesize = ensemblesize;

        //
        // weights, means, sigmas
        //
        ensemble.weights = new double[ensemblesize * wcount];
        ensemble.columnmeans = new double[ensemblesize * ccount];
        ensemble.columnsigmas = new double[ensemblesize * ccount];
        for (i = 0; i <= ensemblesize * wcount - 1; i++)
        {
            ensemble.weights[i] = math.randomreal() - 0.5;
        }
        for (i = 0; i <= ensemblesize - 1; i++)
        {
            i1_ = (0) - (i * ccount);
            for (i_ = i * ccount; i_ <= (i + 1) * ccount - 1; i_++)
            {
                ensemble.columnmeans[i_] = network.columnmeans[i_ + i1_];
            }
            i1_ = (0) - (i * ccount);
            for (i_ = i * ccount; i_ <= (i + 1) * ccount - 1; i_++)
            {
                ensemble.columnsigmas[i_] = network.columnsigmas[i_ + i1_];
            }
        }

        //
        // temporaries, internal buffers
        //
        ensemble.y = new double[mlpbase.mlpgetoutputscount(ensemble.network, _params)];
    }


    /*************************************************************************
    Copying of MLPEnsemble strucure

    INPUT PARAMETERS:
        Ensemble1 -   original

    OUTPUT PARAMETERS:
        Ensemble2 -   copy

      -- ALGLIB --
         Copyright 17.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpecopy(mlpensemble ensemble1,
        mlpensemble ensemble2,
        xparams _params)
    {
        int ccount = 0;
        int wcount = 0;
        int i_ = 0;


        //
        // Unload info
        //
        if (mlpbase.mlpissoftmax(ensemble1.network, _params))
        {
            ccount = mlpbase.mlpgetinputscount(ensemble1.network, _params);
        }
        else
        {
            ccount = mlpbase.mlpgetinputscount(ensemble1.network, _params) + mlpbase.mlpgetoutputscount(ensemble1.network, _params);
        }
        wcount = mlpbase.mlpgetweightscount(ensemble1.network, _params);

        //
        // Allocate space
        //
        ensemble2.weights = new double[ensemble1.ensemblesize * wcount];
        ensemble2.columnmeans = new double[ensemble1.ensemblesize * ccount];
        ensemble2.columnsigmas = new double[ensemble1.ensemblesize * ccount];
        ensemble2.y = new double[mlpbase.mlpgetoutputscount(ensemble1.network, _params)];

        //
        // Copy
        //
        ensemble2.ensemblesize = ensemble1.ensemblesize;
        for (i_ = 0; i_ <= ensemble1.ensemblesize * wcount - 1; i_++)
        {
            ensemble2.weights[i_] = ensemble1.weights[i_];
        }
        for (i_ = 0; i_ <= ensemble1.ensemblesize * ccount - 1; i_++)
        {
            ensemble2.columnmeans[i_] = ensemble1.columnmeans[i_];
        }
        for (i_ = 0; i_ <= ensemble1.ensemblesize * ccount - 1; i_++)
        {
            ensemble2.columnsigmas[i_] = ensemble1.columnsigmas[i_];
        }
        mlpbase.mlpcopy(ensemble1.network, ensemble2.network, _params);
    }


    /*************************************************************************
    Randomization of MLP ensemble

      -- ALGLIB --
         Copyright 17.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void mlperandomize(mlpensemble ensemble,
        xparams _params)
    {
        int i = 0;
        int wcount = 0;

        wcount = mlpbase.mlpgetweightscount(ensemble.network, _params);
        for (i = 0; i <= ensemble.ensemblesize * wcount - 1; i++)
        {
            ensemble.weights[i] = math.randomreal() - 0.5;
        }
    }


    /*************************************************************************
    Return ensemble properties (number of inputs and outputs).

      -- ALGLIB --
         Copyright 17.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpeproperties(mlpensemble ensemble,
        ref int nin,
        ref int nout,
        xparams _params)
    {
        nin = 0;
        nout = 0;

        nin = mlpbase.mlpgetinputscount(ensemble.network, _params);
        nout = mlpbase.mlpgetoutputscount(ensemble.network, _params);
    }


    /*************************************************************************
    Return normalization type (whether ensemble is SOFTMAX-normalized or not).

      -- ALGLIB --
         Copyright 17.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static bool mlpeissoftmax(mlpensemble ensemble,
        xparams _params)
    {
        bool result = new bool();

        result = mlpbase.mlpissoftmax(ensemble.network, _params);
        return result;
    }


    /*************************************************************************
    Procesing

    INPUT PARAMETERS:
        Ensemble-   neural networks ensemble
        X       -   input vector,  array[0..NIn-1].
        Y       -   (possibly) preallocated buffer; if size of Y is less than
                    NOut, it will be reallocated. If it is large enough, it
                    is NOT reallocated, so we can save some time on reallocation.


    OUTPUT PARAMETERS:
        Y       -   result. Regression estimate when solving regression  task,
                    vector of posterior probabilities for classification task.

      -- ALGLIB --
         Copyright 17.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpeprocess(mlpensemble ensemble,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        int i = 0;
        int es = 0;
        int wc = 0;
        int cc = 0;
        double v = 0;
        int nout = 0;
        int i_ = 0;
        int i1_ = 0;

        if (ap.len(y) < mlpbase.mlpgetoutputscount(ensemble.network, _params))
        {
            y = new double[mlpbase.mlpgetoutputscount(ensemble.network, _params)];
        }
        es = ensemble.ensemblesize;
        wc = mlpbase.mlpgetweightscount(ensemble.network, _params);
        if (mlpbase.mlpissoftmax(ensemble.network, _params))
        {
            cc = mlpbase.mlpgetinputscount(ensemble.network, _params);
        }
        else
        {
            cc = mlpbase.mlpgetinputscount(ensemble.network, _params) + mlpbase.mlpgetoutputscount(ensemble.network, _params);
        }
        v = (double)1 / (double)es;
        nout = mlpbase.mlpgetoutputscount(ensemble.network, _params);
        for (i = 0; i <= nout - 1; i++)
        {
            y[i] = 0;
        }
        for (i = 0; i <= es - 1; i++)
        {
            i1_ = (i * wc) - (0);
            for (i_ = 0; i_ <= wc - 1; i_++)
            {
                ensemble.network.weights[i_] = ensemble.weights[i_ + i1_];
            }
            i1_ = (i * cc) - (0);
            for (i_ = 0; i_ <= cc - 1; i_++)
            {
                ensemble.network.columnmeans[i_] = ensemble.columnmeans[i_ + i1_];
            }
            i1_ = (i * cc) - (0);
            for (i_ = 0; i_ <= cc - 1; i_++)
            {
                ensemble.network.columnsigmas[i_] = ensemble.columnsigmas[i_ + i1_];
            }
            mlpbase.mlpprocess(ensemble.network, x, ref ensemble.y, _params);
            for (i_ = 0; i_ <= nout - 1; i_++)
            {
                y[i_] = y[i_] + v * ensemble.y[i_];
            }
        }
    }


    /*************************************************************************
    'interactive'  variant  of  MLPEProcess  for  languages  like Python which
    support constructs like "Y = MLPEProcess(LM,X)" and interactive mode of the
    interpreter

    This function allocates new array on each call,  so  it  is  significantly
    slower than its 'non-interactive' counterpart, but it is  more  convenient
    when you call it from command line.

      -- ALGLIB --
         Copyright 17.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpeprocessi(mlpensemble ensemble,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        y = new double[0];

        mlpeprocess(ensemble, x, ref y, _params);
    }


    /*************************************************************************
    Calculation of all types of errors

      -- ALGLIB --
         Copyright 17.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpeallerrorsx(mlpensemble ensemble,
        double[,] densexy,
        sparse.sparsematrix sparsexy,
        int datasetsize,
        int datasettype,
        int[] idx,
        int subset0,
        int subset1,
        int subsettype,
        smp.shared_pool buf,
        mlpbase.modelerrors rep,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int nin = 0;
        int nout = 0;
        bool iscls = new bool();
        int srcidx = 0;
        hpccores.mlpbuffers pbuf = null;
        mlpbase.modelerrors rep0 = new mlpbase.modelerrors();
        mlpbase.modelerrors rep1 = new mlpbase.modelerrors();
        int i_ = 0;
        int i1_ = 0;


        //
        // Get network information
        //
        nin = mlpbase.mlpgetinputscount(ensemble.network, _params);
        nout = mlpbase.mlpgetoutputscount(ensemble.network, _params);
        iscls = mlpbase.mlpissoftmax(ensemble.network, _params);

        //
        // Retrieve buffer, prepare, process data, recycle buffer
        //
        smp.ae_shared_pool_retrieve(buf, ref pbuf);
        if (iscls)
        {
            bdss.dserrallocate(nout, ref pbuf.tmp0, _params);
        }
        else
        {
            bdss.dserrallocate(-nout, ref pbuf.tmp0, _params);
        }
        apserv.rvectorsetlengthatleast(ref pbuf.x, nin, _params);
        apserv.rvectorsetlengthatleast(ref pbuf.y, nout, _params);
        apserv.rvectorsetlengthatleast(ref pbuf.desiredy, nout, _params);
        for (i = subset0; i <= subset1 - 1; i++)
        {
            srcidx = -1;
            if (subsettype == 0)
            {
                srcidx = i;
            }
            if (subsettype == 1)
            {
                srcidx = idx[i];
            }
            ap.assert(srcidx >= 0, "MLPEAllErrorsX: internal error");
            if (datasettype == 0)
            {
                for (i_ = 0; i_ <= nin - 1; i_++)
                {
                    pbuf.x[i_] = densexy[srcidx, i_];
                }
            }
            if (datasettype == 1)
            {
                sparse.sparsegetrow(sparsexy, srcidx, ref pbuf.x, _params);
            }
            mlpeprocess(ensemble, pbuf.x, ref pbuf.y, _params);
            if (mlpbase.mlpissoftmax(ensemble.network, _params))
            {
                if (datasettype == 0)
                {
                    pbuf.desiredy[0] = densexy[srcidx, nin];
                }
                if (datasettype == 1)
                {
                    pbuf.desiredy[0] = sparse.sparseget(sparsexy, srcidx, nin, _params);
                }
            }
            else
            {
                if (datasettype == 0)
                {
                    i1_ = (nin) - (0);
                    for (i_ = 0; i_ <= nout - 1; i_++)
                    {
                        pbuf.desiredy[i_] = densexy[srcidx, i_ + i1_];
                    }
                }
                if (datasettype == 1)
                {
                    for (j = 0; j <= nout - 1; j++)
                    {
                        pbuf.desiredy[j] = sparse.sparseget(sparsexy, srcidx, nin + j, _params);
                    }
                }
            }
            bdss.dserraccumulate(ref pbuf.tmp0, pbuf.y, pbuf.desiredy, _params);
        }
        bdss.dserrfinish(ref pbuf.tmp0, _params);
        rep.relclserror = pbuf.tmp0[0];
        rep.avgce = pbuf.tmp0[1] / Math.Log(2);
        rep.rmserror = pbuf.tmp0[2];
        rep.avgerror = pbuf.tmp0[3];
        rep.avgrelerror = pbuf.tmp0[4];
        smp.ae_shared_pool_recycle(buf, ref pbuf);
    }


    /*************************************************************************
    Calculation of all types of errors on dataset given by sparse matrix

      -- ALGLIB --
         Copyright 10.09.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpeallerrorssparse(mlpensemble ensemble,
        sparse.sparsematrix xy,
        int npoints,
        ref double relcls,
        ref double avgce,
        ref double rms,
        ref double avg,
        ref double avgrel,
        xparams _params)
    {
        int i = 0;
        double[] buf = new double[0];
        double[] workx = new double[0];
        double[] y = new double[0];
        double[] dy = new double[0];
        int nin = 0;
        int nout = 0;
        int i_ = 0;
        int i1_ = 0;

        relcls = 0;
        avgce = 0;
        rms = 0;
        avg = 0;
        avgrel = 0;

        nin = mlpbase.mlpgetinputscount(ensemble.network, _params);
        nout = mlpbase.mlpgetoutputscount(ensemble.network, _params);
        if (mlpbase.mlpissoftmax(ensemble.network, _params))
        {
            dy = new double[1];
            bdss.dserrallocate(nout, ref buf, _params);
        }
        else
        {
            dy = new double[nout];
            bdss.dserrallocate(-nout, ref buf, _params);
        }
        for (i = 0; i <= npoints - 1; i++)
        {
            sparse.sparsegetrow(xy, i, ref workx, _params);
            mlpeprocess(ensemble, workx, ref y, _params);
            if (mlpbase.mlpissoftmax(ensemble.network, _params))
            {
                dy[0] = workx[nin];
            }
            else
            {
                i1_ = (nin) - (0);
                for (i_ = 0; i_ <= nout - 1; i_++)
                {
                    dy[i_] = workx[i_ + i1_];
                }
            }
            bdss.dserraccumulate(ref buf, y, dy, _params);
        }
        bdss.dserrfinish(ref buf, _params);
        relcls = buf[0];
        avgce = buf[1];
        rms = buf[2];
        avg = buf[3];
        avgrel = buf[4];
    }


    /*************************************************************************
    Relative classification error on the test set

    INPUT PARAMETERS:
        Ensemble-   ensemble
        XY      -   test set
        NPoints -   test set size

    RESULT:
        percent of incorrectly classified cases.
        Works both for classifier betwork and for regression networks which
    are used as classifiers.

      -- ALGLIB --
         Copyright 17.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static double mlperelclserror(mlpensemble ensemble,
        double[,] xy,
        int npoints,
        xparams _params)
    {
        double result = 0;
        mlpbase.modelerrors rep = new mlpbase.modelerrors();

        mlpeallerrorsx(ensemble, xy, ensemble.network.dummysxy, npoints, 0, ensemble.network.dummyidx, 0, npoints, 0, ensemble.network.buf, rep, _params);
        result = rep.relclserror;
        return result;
    }


    /*************************************************************************
    Average cross-entropy (in bits per element) on the test set

    INPUT PARAMETERS:
        Ensemble-   ensemble
        XY      -   test set
        NPoints -   test set size

    RESULT:
        CrossEntropy/(NPoints*LN(2)).
        Zero if ensemble solves regression task.

      -- ALGLIB --
         Copyright 17.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static double mlpeavgce(mlpensemble ensemble,
        double[,] xy,
        int npoints,
        xparams _params)
    {
        double result = 0;
        mlpbase.modelerrors rep = new mlpbase.modelerrors();

        mlpeallerrorsx(ensemble, xy, ensemble.network.dummysxy, npoints, 0, ensemble.network.dummyidx, 0, npoints, 0, ensemble.network.buf, rep, _params);
        result = rep.avgce;
        return result;
    }


    /*************************************************************************
    RMS error on the test set

    INPUT PARAMETERS:
        Ensemble-   ensemble
        XY      -   test set
        NPoints -   test set size

    RESULT:
        root mean square error.
        Its meaning for regression task is obvious. As for classification task
    RMS error means error when estimating posterior probabilities.

      -- ALGLIB --
         Copyright 17.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static double mlpermserror(mlpensemble ensemble,
        double[,] xy,
        int npoints,
        xparams _params)
    {
        double result = 0;
        mlpbase.modelerrors rep = new mlpbase.modelerrors();

        mlpeallerrorsx(ensemble, xy, ensemble.network.dummysxy, npoints, 0, ensemble.network.dummyidx, 0, npoints, 0, ensemble.network.buf, rep, _params);
        result = rep.rmserror;
        return result;
    }


    /*************************************************************************
    Average error on the test set

    INPUT PARAMETERS:
        Ensemble-   ensemble
        XY      -   test set
        NPoints -   test set size

    RESULT:
        Its meaning for regression task is obvious. As for classification task
    it means average error when estimating posterior probabilities.

      -- ALGLIB --
         Copyright 17.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static double mlpeavgerror(mlpensemble ensemble,
        double[,] xy,
        int npoints,
        xparams _params)
    {
        double result = 0;
        mlpbase.modelerrors rep = new mlpbase.modelerrors();

        mlpeallerrorsx(ensemble, xy, ensemble.network.dummysxy, npoints, 0, ensemble.network.dummyidx, 0, npoints, 0, ensemble.network.buf, rep, _params);
        result = rep.avgerror;
        return result;
    }


    /*************************************************************************
    Average relative error on the test set

    INPUT PARAMETERS:
        Ensemble-   ensemble
        XY      -   test set
        NPoints -   test set size

    RESULT:
        Its meaning for regression task is obvious. As for classification task
    it means average relative error when estimating posterior probabilities.

      -- ALGLIB --
         Copyright 17.02.2009 by Bochkanov Sergey
    *************************************************************************/
    public static double mlpeavgrelerror(mlpensemble ensemble,
        double[,] xy,
        int npoints,
        xparams _params)
    {
        double result = 0;
        mlpbase.modelerrors rep = new mlpbase.modelerrors();

        mlpeallerrorsx(ensemble, xy, ensemble.network.dummysxy, npoints, 0, ensemble.network.dummyidx, 0, npoints, 0, ensemble.network.buf, rep, _params);
        result = rep.avgrelerror;
        return result;
    }


    /*************************************************************************
    Serializer: allocation

      -- ALGLIB --
         Copyright 19.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpealloc(serializer s,
        mlpensemble ensemble,
        xparams _params)
    {
        s.alloc_entry();
        s.alloc_entry();
        s.alloc_entry();
        apserv.allocrealarray(s, ensemble.weights, -1, _params);
        apserv.allocrealarray(s, ensemble.columnmeans, -1, _params);
        apserv.allocrealarray(s, ensemble.columnsigmas, -1, _params);
        mlpbase.mlpalloc(s, ensemble.network, _params);
    }


    /*************************************************************************
    Serializer: serialization

      -- ALGLIB --
         Copyright 14.03.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpeserialize(serializer s,
        mlpensemble ensemble,
        xparams _params)
    {
        s.serialize_int(scodes.getmlpeserializationcode(_params));
        s.serialize_int(mlpefirstversion);
        s.serialize_int(ensemble.ensemblesize);
        apserv.serializerealarray(s, ensemble.weights, -1, _params);
        apserv.serializerealarray(s, ensemble.columnmeans, -1, _params);
        apserv.serializerealarray(s, ensemble.columnsigmas, -1, _params);
        mlpbase.mlpserialize(s, ensemble.network, _params);
    }


    /*************************************************************************
    Serializer: unserialization

      -- ALGLIB --
         Copyright 14.03.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpeunserialize(serializer s,
        mlpensemble ensemble,
        xparams _params)
    {
        int i0 = 0;
        int i1 = 0;


        //
        // check correctness of header
        //
        i0 = s.unserialize_int();
        ap.assert(i0 == scodes.getmlpeserializationcode(_params), "MLPEUnserialize: stream header corrupted");
        i1 = s.unserialize_int();
        ap.assert(i1 == mlpefirstversion, "MLPEUnserialize: stream header corrupted");

        //
        // Create network
        //
        ensemble.ensemblesize = s.unserialize_int();
        apserv.unserializerealarray(s, ref ensemble.weights, _params);
        apserv.unserializerealarray(s, ref ensemble.columnmeans, _params);
        apserv.unserializerealarray(s, ref ensemble.columnsigmas, _params);
        mlpbase.mlpunserialize(s, ensemble.network, _params);

        //
        // Allocate termoraries
        //
        ensemble.y = new double[mlpbase.mlpgetoutputscount(ensemble.network, _params)];
    }


}
