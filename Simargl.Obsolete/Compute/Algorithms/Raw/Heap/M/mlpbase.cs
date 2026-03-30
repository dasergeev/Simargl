using System;

#pragma warning disable CS3008
#pragma warning disable CS8618
#pragma warning disable CS8604
#pragma warning disable CS8600
#pragma warning disable CS8631
#pragma warning disable CS8602
#pragma warning disable CS0219
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

public class mlpbase
{
    /*************************************************************************
    Model's errors:
        * RelCLSError   -   fraction of misclassified cases.
        * AvgCE         -   acerage cross-entropy
        * RMSError      -   root-mean-square error
        * AvgError      -   average error
        * AvgRelError   -   average relative error
        
    NOTE 1: RelCLSError/AvgCE are zero on regression problems.

    NOTE 2: on classification problems  RMSError/AvgError/AvgRelError  contain
            errors in prediction of posterior probabilities
    *************************************************************************/
    public class modelerrors : apobject
    {
        public double relclserror;
        public double avgce;
        public double rmserror;
        public double avgerror;
        public double avgrelerror;
        public modelerrors()
        {
            init();
        }
        public override void init()
        {
        }
        public override apobject make_copy()
        {
            modelerrors _result = new modelerrors();
            _result.relclserror = relclserror;
            _result.avgce = avgce;
            _result.rmserror = rmserror;
            _result.avgerror = avgerror;
            _result.avgrelerror = avgrelerror;
            return _result;
        }
    };


    /*************************************************************************
    This structure is used to store MLP error and gradient.
    *************************************************************************/
    public class smlpgrad : apobject
    {
        public double f;
        public double[] g;
        public smlpgrad()
        {
            init();
        }
        public override void init()
        {
            g = new double[0];
        }
        public override apobject make_copy()
        {
            smlpgrad _result = new smlpgrad();
            _result.f = f;
            _result.g = (double[])g.Clone();
            return _result;
        }
    };


    public class multilayerperceptron : apobject
    {
        public int hlnetworktype;
        public int hlnormtype;
        public int[] hllayersizes;
        public int[] hlconnections;
        public int[] hlneurons;
        public int[] structinfo;
        public double[] weights;
        public double[] columnmeans;
        public double[] columnsigmas;
        public double[] neurons;
        public double[] dfdnet;
        public double[] derror;
        public double[] x;
        public double[] y;
        public double[,] xy;
        public double[] xyrow;
        public double[] nwbuf;
        public int[] integerbuf;
        public modelerrors err;
        public double[] rndbuf;
        public smp.shared_pool buf;
        public smp.shared_pool gradbuf;
        public double[,] dummydxy;
        public sparse.sparsematrix dummysxy;
        public int[] dummyidx;
        public smp.shared_pool dummypool;
        public multilayerperceptron()
        {
            init();
        }
        public override void init()
        {
            hllayersizes = new int[0];
            hlconnections = new int[0];
            hlneurons = new int[0];
            structinfo = new int[0];
            weights = new double[0];
            columnmeans = new double[0];
            columnsigmas = new double[0];
            neurons = new double[0];
            dfdnet = new double[0];
            derror = new double[0];
            x = new double[0];
            y = new double[0];
            xy = new double[0, 0];
            xyrow = new double[0];
            nwbuf = new double[0];
            integerbuf = new int[0];
            err = new modelerrors();
            rndbuf = new double[0];
            buf = new smp.shared_pool();
            gradbuf = new smp.shared_pool();
            dummydxy = new double[0, 0];
            dummysxy = new sparse.sparsematrix();
            dummyidx = new int[0];
            dummypool = new smp.shared_pool();
        }
        public override apobject make_copy()
        {
            multilayerperceptron _result = new multilayerperceptron();
            _result.hlnetworktype = hlnetworktype;
            _result.hlnormtype = hlnormtype;
            _result.hllayersizes = (int[])hllayersizes.Clone();
            _result.hlconnections = (int[])hlconnections.Clone();
            _result.hlneurons = (int[])hlneurons.Clone();
            _result.structinfo = (int[])structinfo.Clone();
            _result.weights = (double[])weights.Clone();
            _result.columnmeans = (double[])columnmeans.Clone();
            _result.columnsigmas = (double[])columnsigmas.Clone();
            _result.neurons = (double[])neurons.Clone();
            _result.dfdnet = (double[])dfdnet.Clone();
            _result.derror = (double[])derror.Clone();
            _result.x = (double[])x.Clone();
            _result.y = (double[])y.Clone();
            _result.xy = (double[,])xy.Clone();
            _result.xyrow = (double[])xyrow.Clone();
            _result.nwbuf = (double[])nwbuf.Clone();
            _result.integerbuf = (int[])integerbuf.Clone();
            _result.err = (modelerrors)err.make_copy();
            _result.rndbuf = (double[])rndbuf.Clone();
            _result.buf = (smp.shared_pool)buf.make_copy();
            _result.gradbuf = (smp.shared_pool)gradbuf.make_copy();
            _result.dummydxy = (double[,])dummydxy.Clone();
            _result.dummysxy = (sparse.sparsematrix)dummysxy.make_copy();
            _result.dummyidx = (int[])dummyidx.Clone();
            _result.dummypool = (smp.shared_pool)dummypool.make_copy();
            return _result;
        }
    };




    public const int mlpvnum = 7;
    public const int mlpfirstversion = 0;
    public const int nfieldwidth = 4;
    public const int hlconnfieldwidth = 5;
    public const int hlnfieldwidth = 4;
    public const int gradbasecasecost = 50000;
    public const int microbatchsize = 64;


    /*************************************************************************
    This function returns number of weights  updates  which  is  required  for
    gradient calculation problem to be splitted.
    *************************************************************************/
    public static int mlpgradsplitcost(xparams _params)
    {
        int result = 0;

        result = gradbasecasecost;
        return result;
    }


    /*************************************************************************
    This  function  returns  number  of elements in subset of dataset which is
    required for gradient calculation problem to be splitted.
    *************************************************************************/
    public static int mlpgradsplitsize(xparams _params)
    {
        int result = 0;

        result = microbatchsize;
        return result;
    }


    /*************************************************************************
    Creates  neural  network  with  NIn  inputs,  NOut outputs, without hidden
    layers, with linear output layer. Network weights are  filled  with  small
    random values.

      -- ALGLIB --
         Copyright 04.11.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpcreate0(int nin,
        int nout,
        multilayerperceptron network,
        xparams _params)
    {
        int[] lsizes = new int[0];
        int[] ltypes = new int[0];
        int[] lconnfirst = new int[0];
        int[] lconnlast = new int[0];
        int layerscount = 0;
        int lastproc = 0;

        layerscount = 1 + 3;

        //
        // Allocate arrays
        //
        lsizes = new int[layerscount - 1 + 1];
        ltypes = new int[layerscount - 1 + 1];
        lconnfirst = new int[layerscount - 1 + 1];
        lconnlast = new int[layerscount - 1 + 1];

        //
        // Layers
        //
        addinputlayer(nin, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addbiasedsummatorlayer(nout, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addactivationlayer(-5, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);

        //
        // Create
        //
        mlpcreate(nin, nout, lsizes, ltypes, lconnfirst, lconnlast, layerscount, false, network, _params);
        fillhighlevelinformation(network, nin, 0, 0, nout, false, true, _params);
    }


    /*************************************************************************
    Same  as  MLPCreate0,  but  with  one  hidden  layer  (NHid  neurons) with
    non-linear activation function. Output layer is linear.

      -- ALGLIB --
         Copyright 04.11.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpcreate1(int nin,
        int nhid,
        int nout,
        multilayerperceptron network,
        xparams _params)
    {
        int[] lsizes = new int[0];
        int[] ltypes = new int[0];
        int[] lconnfirst = new int[0];
        int[] lconnlast = new int[0];
        int layerscount = 0;
        int lastproc = 0;

        layerscount = 1 + 3 + 3;

        //
        // Allocate arrays
        //
        lsizes = new int[layerscount - 1 + 1];
        ltypes = new int[layerscount - 1 + 1];
        lconnfirst = new int[layerscount - 1 + 1];
        lconnlast = new int[layerscount - 1 + 1];

        //
        // Layers
        //
        addinputlayer(nin, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addbiasedsummatorlayer(nhid, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addactivationlayer(1, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addbiasedsummatorlayer(nout, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addactivationlayer(-5, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);

        //
        // Create
        //
        mlpcreate(nin, nout, lsizes, ltypes, lconnfirst, lconnlast, layerscount, false, network, _params);
        fillhighlevelinformation(network, nin, nhid, 0, nout, false, true, _params);
    }


    /*************************************************************************
    Same as MLPCreate0, but with two hidden layers (NHid1 and  NHid2  neurons)
    with non-linear activation function. Output layer is linear.
     $ALL

      -- ALGLIB --
         Copyright 04.11.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpcreate2(int nin,
        int nhid1,
        int nhid2,
        int nout,
        multilayerperceptron network,
        xparams _params)
    {
        int[] lsizes = new int[0];
        int[] ltypes = new int[0];
        int[] lconnfirst = new int[0];
        int[] lconnlast = new int[0];
        int layerscount = 0;
        int lastproc = 0;

        layerscount = 1 + 3 + 3 + 3;

        //
        // Allocate arrays
        //
        lsizes = new int[layerscount - 1 + 1];
        ltypes = new int[layerscount - 1 + 1];
        lconnfirst = new int[layerscount - 1 + 1];
        lconnlast = new int[layerscount - 1 + 1];

        //
        // Layers
        //
        addinputlayer(nin, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addbiasedsummatorlayer(nhid1, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addactivationlayer(1, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addbiasedsummatorlayer(nhid2, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addactivationlayer(1, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addbiasedsummatorlayer(nout, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addactivationlayer(-5, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);

        //
        // Create
        //
        mlpcreate(nin, nout, lsizes, ltypes, lconnfirst, lconnlast, layerscount, false, network, _params);
        fillhighlevelinformation(network, nin, nhid1, nhid2, nout, false, true, _params);
    }


    /*************************************************************************
    Creates  neural  network  with  NIn  inputs,  NOut outputs, without hidden
    layers with non-linear output layer. Network weights are filled with small
    random values.

    Activation function of the output layer takes values:

        (B, +INF), if D>=0

    or

        (-INF, B), if D<0.


      -- ALGLIB --
         Copyright 30.03.2008 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpcreateb0(int nin,
        int nout,
        double b,
        double d,
        multilayerperceptron network,
        xparams _params)
    {
        int[] lsizes = new int[0];
        int[] ltypes = new int[0];
        int[] lconnfirst = new int[0];
        int[] lconnlast = new int[0];
        int layerscount = 0;
        int lastproc = 0;
        int i = 0;

        layerscount = 1 + 3;
        if ((double)(d) >= (double)(0))
        {
            d = 1;
        }
        else
        {
            d = -1;
        }

        //
        // Allocate arrays
        //
        lsizes = new int[layerscount - 1 + 1];
        ltypes = new int[layerscount - 1 + 1];
        lconnfirst = new int[layerscount - 1 + 1];
        lconnlast = new int[layerscount - 1 + 1];

        //
        // Layers
        //
        addinputlayer(nin, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addbiasedsummatorlayer(nout, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addactivationlayer(3, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);

        //
        // Create
        //
        mlpcreate(nin, nout, lsizes, ltypes, lconnfirst, lconnlast, layerscount, false, network, _params);
        fillhighlevelinformation(network, nin, 0, 0, nout, false, false, _params);

        //
        // Turn on ouputs shift/scaling.
        //
        for (i = nin; i <= nin + nout - 1; i++)
        {
            network.columnmeans[i] = b;
            network.columnsigmas[i] = d;
        }
    }


    /*************************************************************************
    Same as MLPCreateB0 but with non-linear hidden layer.

      -- ALGLIB --
         Copyright 30.03.2008 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpcreateb1(int nin,
        int nhid,
        int nout,
        double b,
        double d,
        multilayerperceptron network,
        xparams _params)
    {
        int[] lsizes = new int[0];
        int[] ltypes = new int[0];
        int[] lconnfirst = new int[0];
        int[] lconnlast = new int[0];
        int layerscount = 0;
        int lastproc = 0;
        int i = 0;

        layerscount = 1 + 3 + 3;
        if ((double)(d) >= (double)(0))
        {
            d = 1;
        }
        else
        {
            d = -1;
        }

        //
        // Allocate arrays
        //
        lsizes = new int[layerscount - 1 + 1];
        ltypes = new int[layerscount - 1 + 1];
        lconnfirst = new int[layerscount - 1 + 1];
        lconnlast = new int[layerscount - 1 + 1];

        //
        // Layers
        //
        addinputlayer(nin, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addbiasedsummatorlayer(nhid, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addactivationlayer(1, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addbiasedsummatorlayer(nout, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addactivationlayer(3, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);

        //
        // Create
        //
        mlpcreate(nin, nout, lsizes, ltypes, lconnfirst, lconnlast, layerscount, false, network, _params);
        fillhighlevelinformation(network, nin, nhid, 0, nout, false, false, _params);

        //
        // Turn on ouputs shift/scaling.
        //
        for (i = nin; i <= nin + nout - 1; i++)
        {
            network.columnmeans[i] = b;
            network.columnsigmas[i] = d;
        }
    }


    /*************************************************************************
    Same as MLPCreateB0 but with two non-linear hidden layers.

      -- ALGLIB --
         Copyright 30.03.2008 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpcreateb2(int nin,
        int nhid1,
        int nhid2,
        int nout,
        double b,
        double d,
        multilayerperceptron network,
        xparams _params)
    {
        int[] lsizes = new int[0];
        int[] ltypes = new int[0];
        int[] lconnfirst = new int[0];
        int[] lconnlast = new int[0];
        int layerscount = 0;
        int lastproc = 0;
        int i = 0;

        layerscount = 1 + 3 + 3 + 3;
        if ((double)(d) >= (double)(0))
        {
            d = 1;
        }
        else
        {
            d = -1;
        }

        //
        // Allocate arrays
        //
        lsizes = new int[layerscount - 1 + 1];
        ltypes = new int[layerscount - 1 + 1];
        lconnfirst = new int[layerscount - 1 + 1];
        lconnlast = new int[layerscount - 1 + 1];

        //
        // Layers
        //
        addinputlayer(nin, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addbiasedsummatorlayer(nhid1, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addactivationlayer(1, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addbiasedsummatorlayer(nhid2, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addactivationlayer(1, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addbiasedsummatorlayer(nout, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addactivationlayer(3, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);

        //
        // Create
        //
        mlpcreate(nin, nout, lsizes, ltypes, lconnfirst, lconnlast, layerscount, false, network, _params);
        fillhighlevelinformation(network, nin, nhid1, nhid2, nout, false, false, _params);

        //
        // Turn on ouputs shift/scaling.
        //
        for (i = nin; i <= nin + nout - 1; i++)
        {
            network.columnmeans[i] = b;
            network.columnsigmas[i] = d;
        }
    }


    /*************************************************************************
    Creates  neural  network  with  NIn  inputs,  NOut outputs, without hidden
    layers with non-linear output layer. Network weights are filled with small
    random values. Activation function of the output layer takes values [A,B].

      -- ALGLIB --
         Copyright 30.03.2008 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpcreater0(int nin,
        int nout,
        double a,
        double b,
        multilayerperceptron network,
        xparams _params)
    {
        int[] lsizes = new int[0];
        int[] ltypes = new int[0];
        int[] lconnfirst = new int[0];
        int[] lconnlast = new int[0];
        int layerscount = 0;
        int lastproc = 0;
        int i = 0;

        layerscount = 1 + 3;

        //
        // Allocate arrays
        //
        lsizes = new int[layerscount - 1 + 1];
        ltypes = new int[layerscount - 1 + 1];
        lconnfirst = new int[layerscount - 1 + 1];
        lconnlast = new int[layerscount - 1 + 1];

        //
        // Layers
        //
        addinputlayer(nin, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addbiasedsummatorlayer(nout, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addactivationlayer(1, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);

        //
        // Create
        //
        mlpcreate(nin, nout, lsizes, ltypes, lconnfirst, lconnlast, layerscount, false, network, _params);
        fillhighlevelinformation(network, nin, 0, 0, nout, false, false, _params);

        //
        // Turn on outputs shift/scaling.
        //
        for (i = nin; i <= nin + nout - 1; i++)
        {
            network.columnmeans[i] = 0.5 * (a + b);
            network.columnsigmas[i] = 0.5 * (a - b);
        }
    }


    /*************************************************************************
    Same as MLPCreateR0, but with non-linear hidden layer.

      -- ALGLIB --
         Copyright 30.03.2008 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpcreater1(int nin,
        int nhid,
        int nout,
        double a,
        double b,
        multilayerperceptron network,
        xparams _params)
    {
        int[] lsizes = new int[0];
        int[] ltypes = new int[0];
        int[] lconnfirst = new int[0];
        int[] lconnlast = new int[0];
        int layerscount = 0;
        int lastproc = 0;
        int i = 0;

        layerscount = 1 + 3 + 3;

        //
        // Allocate arrays
        //
        lsizes = new int[layerscount - 1 + 1];
        ltypes = new int[layerscount - 1 + 1];
        lconnfirst = new int[layerscount - 1 + 1];
        lconnlast = new int[layerscount - 1 + 1];

        //
        // Layers
        //
        addinputlayer(nin, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addbiasedsummatorlayer(nhid, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addactivationlayer(1, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addbiasedsummatorlayer(nout, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addactivationlayer(1, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);

        //
        // Create
        //
        mlpcreate(nin, nout, lsizes, ltypes, lconnfirst, lconnlast, layerscount, false, network, _params);
        fillhighlevelinformation(network, nin, nhid, 0, nout, false, false, _params);

        //
        // Turn on outputs shift/scaling.
        //
        for (i = nin; i <= nin + nout - 1; i++)
        {
            network.columnmeans[i] = 0.5 * (a + b);
            network.columnsigmas[i] = 0.5 * (a - b);
        }
    }


    /*************************************************************************
    Same as MLPCreateR0, but with two non-linear hidden layers.

      -- ALGLIB --
         Copyright 30.03.2008 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpcreater2(int nin,
        int nhid1,
        int nhid2,
        int nout,
        double a,
        double b,
        multilayerperceptron network,
        xparams _params)
    {
        int[] lsizes = new int[0];
        int[] ltypes = new int[0];
        int[] lconnfirst = new int[0];
        int[] lconnlast = new int[0];
        int layerscount = 0;
        int lastproc = 0;
        int i = 0;

        layerscount = 1 + 3 + 3 + 3;

        //
        // Allocate arrays
        //
        lsizes = new int[layerscount - 1 + 1];
        ltypes = new int[layerscount - 1 + 1];
        lconnfirst = new int[layerscount - 1 + 1];
        lconnlast = new int[layerscount - 1 + 1];

        //
        // Layers
        //
        addinputlayer(nin, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addbiasedsummatorlayer(nhid1, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addactivationlayer(1, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addbiasedsummatorlayer(nhid2, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addactivationlayer(1, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addbiasedsummatorlayer(nout, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addactivationlayer(1, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);

        //
        // Create
        //
        mlpcreate(nin, nout, lsizes, ltypes, lconnfirst, lconnlast, layerscount, false, network, _params);
        fillhighlevelinformation(network, nin, nhid1, nhid2, nout, false, false, _params);

        //
        // Turn on outputs shift/scaling.
        //
        for (i = nin; i <= nin + nout - 1; i++)
        {
            network.columnmeans[i] = 0.5 * (a + b);
            network.columnsigmas[i] = 0.5 * (a - b);
        }
    }


    /*************************************************************************
    Creates classifier network with NIn  inputs  and  NOut  possible  classes.
    Network contains no hidden layers and linear output  layer  with  SOFTMAX-
    normalization  (so  outputs  sums  up  to  1.0  and  converge to posterior
    probabilities).

      -- ALGLIB --
         Copyright 04.11.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpcreatec0(int nin,
        int nout,
        multilayerperceptron network,
        xparams _params)
    {
        int[] lsizes = new int[0];
        int[] ltypes = new int[0];
        int[] lconnfirst = new int[0];
        int[] lconnlast = new int[0];
        int layerscount = 0;
        int lastproc = 0;

        ap.assert(nout >= 2, "MLPCreateC0: NOut<2!");
        layerscount = 1 + 2 + 1;

        //
        // Allocate arrays
        //
        lsizes = new int[layerscount - 1 + 1];
        ltypes = new int[layerscount - 1 + 1];
        lconnfirst = new int[layerscount - 1 + 1];
        lconnlast = new int[layerscount - 1 + 1];

        //
        // Layers
        //
        addinputlayer(nin, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addbiasedsummatorlayer(nout - 1, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addzerolayer(ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);

        //
        // Create
        //
        mlpcreate(nin, nout, lsizes, ltypes, lconnfirst, lconnlast, layerscount, true, network, _params);
        fillhighlevelinformation(network, nin, 0, 0, nout, true, true, _params);
    }


    /*************************************************************************
    Same as MLPCreateC0, but with one non-linear hidden layer.

      -- ALGLIB --
         Copyright 04.11.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpcreatec1(int nin,
        int nhid,
        int nout,
        multilayerperceptron network,
        xparams _params)
    {
        int[] lsizes = new int[0];
        int[] ltypes = new int[0];
        int[] lconnfirst = new int[0];
        int[] lconnlast = new int[0];
        int layerscount = 0;
        int lastproc = 0;

        ap.assert(nout >= 2, "MLPCreateC1: NOut<2!");
        layerscount = 1 + 3 + 2 + 1;

        //
        // Allocate arrays
        //
        lsizes = new int[layerscount - 1 + 1];
        ltypes = new int[layerscount - 1 + 1];
        lconnfirst = new int[layerscount - 1 + 1];
        lconnlast = new int[layerscount - 1 + 1];

        //
        // Layers
        //
        addinputlayer(nin, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addbiasedsummatorlayer(nhid, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addactivationlayer(1, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addbiasedsummatorlayer(nout - 1, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addzerolayer(ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);

        //
        // Create
        //
        mlpcreate(nin, nout, lsizes, ltypes, lconnfirst, lconnlast, layerscount, true, network, _params);
        fillhighlevelinformation(network, nin, nhid, 0, nout, true, true, _params);
    }


    /*************************************************************************
    Same as MLPCreateC0, but with two non-linear hidden layers.

      -- ALGLIB --
         Copyright 04.11.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpcreatec2(int nin,
        int nhid1,
        int nhid2,
        int nout,
        multilayerperceptron network,
        xparams _params)
    {
        int[] lsizes = new int[0];
        int[] ltypes = new int[0];
        int[] lconnfirst = new int[0];
        int[] lconnlast = new int[0];
        int layerscount = 0;
        int lastproc = 0;

        ap.assert(nout >= 2, "MLPCreateC2: NOut<2!");
        layerscount = 1 + 3 + 3 + 2 + 1;

        //
        // Allocate arrays
        //
        lsizes = new int[layerscount - 1 + 1];
        ltypes = new int[layerscount - 1 + 1];
        lconnfirst = new int[layerscount - 1 + 1];
        lconnlast = new int[layerscount - 1 + 1];

        //
        // Layers
        //
        addinputlayer(nin, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addbiasedsummatorlayer(nhid1, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addactivationlayer(1, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addbiasedsummatorlayer(nhid2, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addactivationlayer(1, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addbiasedsummatorlayer(nout - 1, ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);
        addzerolayer(ref lsizes, ref ltypes, ref lconnfirst, ref lconnlast, ref lastproc, _params);

        //
        // Create
        //
        mlpcreate(nin, nout, lsizes, ltypes, lconnfirst, lconnlast, layerscount, true, network, _params);
        fillhighlevelinformation(network, nin, nhid1, nhid2, nout, true, true, _params);
    }


    /*************************************************************************
    Copying of neural network

    INPUT PARAMETERS:
        Network1 -   original

    OUTPUT PARAMETERS:
        Network2 -   copy

      -- ALGLIB --
         Copyright 04.11.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpcopy(multilayerperceptron network1,
        multilayerperceptron network2,
        xparams _params)
    {
        mlpcopyshared(network1, network2, _params);
    }


    /*************************************************************************
    Copying of neural network (second parameter is passed as shared object).

    INPUT PARAMETERS:
        Network1 -   original

    OUTPUT PARAMETERS:
        Network2 -   copy

      -- ALGLIB --
         Copyright 04.11.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpcopyshared(multilayerperceptron network1,
        multilayerperceptron network2,
        xparams _params)
    {
        int wcount = 0;
        int i = 0;
        hpccores.mlpbuffers buf = new hpccores.mlpbuffers();
        smlpgrad sgrad = new smlpgrad();


        //
        // Copy scalar and array fields
        //
        network2.hlnetworktype = network1.hlnetworktype;
        network2.hlnormtype = network1.hlnormtype;
        apserv.copyintegerarray(network1.hllayersizes, ref network2.hllayersizes, _params);
        apserv.copyintegerarray(network1.hlconnections, ref network2.hlconnections, _params);
        apserv.copyintegerarray(network1.hlneurons, ref network2.hlneurons, _params);
        apserv.copyintegerarray(network1.structinfo, ref network2.structinfo, _params);
        apserv.copyrealarray(network1.weights, ref network2.weights, _params);
        apserv.copyrealarray(network1.columnmeans, ref network2.columnmeans, _params);
        apserv.copyrealarray(network1.columnsigmas, ref network2.columnsigmas, _params);
        apserv.copyrealarray(network1.neurons, ref network2.neurons, _params);
        apserv.copyrealarray(network1.dfdnet, ref network2.dfdnet, _params);
        apserv.copyrealarray(network1.derror, ref network2.derror, _params);
        apserv.copyrealarray(network1.x, ref network2.x, _params);
        apserv.copyrealarray(network1.y, ref network2.y, _params);
        apserv.copyrealarray(network1.nwbuf, ref network2.nwbuf, _params);
        apserv.copyintegerarray(network1.integerbuf, ref network2.integerbuf, _params);

        //
        // copy buffers
        //
        wcount = mlpgetweightscount(network1, _params);
        smp.ae_shared_pool_set_seed(network2.buf, buf);
        sgrad.g = new double[wcount];
        sgrad.f = 0.0;
        for (i = 0; i <= wcount - 1; i++)
        {
            sgrad.g[i] = 0.0;
        }
        smp.ae_shared_pool_set_seed(network2.gradbuf, sgrad);
    }


    /*************************************************************************
    This function compares architectures of neural networks.  Only  geometries
    are compared, weights and other parameters are not tested.

      -- ALGLIB --
         Copyright 20.06.2013 by Bochkanov Sergey
    *************************************************************************/
    public static bool mlpsamearchitecture(multilayerperceptron network1,
        multilayerperceptron network2,
        xparams _params)
    {
        bool result = new bool();
        int i = 0;
        int ninfo = 0;

        ap.assert(ap.len(network1.structinfo) > 0 && ap.len(network1.structinfo) >= network1.structinfo[0], "MLPSameArchitecture: Network1 is uninitialized");
        ap.assert(ap.len(network2.structinfo) > 0 && ap.len(network2.structinfo) >= network2.structinfo[0], "MLPSameArchitecture: Network2 is uninitialized");
        result = false;
        if (network1.structinfo[0] != network2.structinfo[0])
        {
            return result;
        }
        ninfo = network1.structinfo[0];
        for (i = 0; i <= ninfo - 1; i++)
        {
            if (network1.structinfo[i] != network2.structinfo[i])
            {
                return result;
            }
        }
        result = true;
        return result;
    }


    /*************************************************************************
    This function copies tunable  parameters (weights/means/sigmas)  from  one
    network to another with same architecture. It  performs  some  rudimentary
    checks that architectures are same, and throws exception if check fails.

    It is intended for fast copying of states between two  network  which  are
    known to have same geometry.

    INPUT PARAMETERS:
        Network1 -   source, must be correctly initialized
        Network2 -   target, must have same architecture

    OUTPUT PARAMETERS:
        Network2 -   network state is copied from source to target

      -- ALGLIB --
         Copyright 20.06.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpcopytunableparameters(multilayerperceptron network1,
        multilayerperceptron network2,
        xparams _params)
    {
        int i = 0;
        int ninfo = 0;
        int nin = 0;
        int nout = 0;
        int wcount = 0;

        ap.assert(ap.len(network1.structinfo) > 0 && ap.len(network1.structinfo) >= network1.structinfo[0], "MLPCopyTunableParameters: Network1 is uninitialized");
        ap.assert(ap.len(network2.structinfo) > 0 && ap.len(network2.structinfo) >= network2.structinfo[0], "MLPCopyTunableParameters: Network2 is uninitialized");
        ap.assert(network1.structinfo[0] == network2.structinfo[0], "MLPCopyTunableParameters: Network1 geometry differs from that of Network2");
        ninfo = network1.structinfo[0];
        for (i = 0; i <= ninfo - 1; i++)
        {
            ap.assert(network1.structinfo[i] == network2.structinfo[i], "MLPCopyTunableParameters: Network1 geometry differs from that of Network2");
        }
        mlpproperties(network1, ref nin, ref nout, ref wcount, _params);
        for (i = 0; i <= wcount - 1; i++)
        {
            network2.weights[i] = network1.weights[i];
        }
        if (mlpissoftmax(network1, _params))
        {
            for (i = 0; i <= nin - 1; i++)
            {
                network2.columnmeans[i] = network1.columnmeans[i];
                network2.columnsigmas[i] = network1.columnsigmas[i];
            }
        }
        else
        {
            for (i = 0; i <= nin + nout - 1; i++)
            {
                network2.columnmeans[i] = network1.columnmeans[i];
                network2.columnsigmas[i] = network1.columnsigmas[i];
            }
        }
    }


    /*************************************************************************
    This  function  exports  tunable   parameters  (weights/means/sigmas) from
    network to contiguous array. Nothing is guaranteed about array format, the
    only thing you can count for is that MLPImportTunableParameters() will  be
    able to parse it.

    It is intended for fast copying of states between network and backup array

    INPUT PARAMETERS:
        Network     -   source, must be correctly initialized
        P           -   array to use. If its size is enough to store data,  it
                        is reused.

    OUTPUT PARAMETERS:
        P           -   array which stores network parameters, resized if needed
        PCount      -   number of parameters stored in array.

      -- ALGLIB --
         Copyright 20.06.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpexporttunableparameters(multilayerperceptron network,
        ref double[] p,
        ref int pcount,
        xparams _params)
    {
        int i = 0;
        int k = 0;
        int nin = 0;
        int nout = 0;
        int wcount = 0;

        pcount = 0;

        ap.assert(ap.len(network.structinfo) > 0 && ap.len(network.structinfo) >= network.structinfo[0], "MLPExportTunableParameters: Network is uninitialized");
        mlpproperties(network, ref nin, ref nout, ref wcount, _params);
        if (mlpissoftmax(network, _params))
        {
            pcount = wcount + 2 * nin;
            apserv.rvectorsetlengthatleast(ref p, pcount, _params);
            k = 0;
            for (i = 0; i <= wcount - 1; i++)
            {
                p[k] = network.weights[i];
                k = k + 1;
            }
            for (i = 0; i <= nin - 1; i++)
            {
                p[k] = network.columnmeans[i];
                k = k + 1;
                p[k] = network.columnsigmas[i];
                k = k + 1;
            }
        }
        else
        {
            pcount = wcount + 2 * (nin + nout);
            apserv.rvectorsetlengthatleast(ref p, pcount, _params);
            k = 0;
            for (i = 0; i <= wcount - 1; i++)
            {
                p[k] = network.weights[i];
                k = k + 1;
            }
            for (i = 0; i <= nin + nout - 1; i++)
            {
                p[k] = network.columnmeans[i];
                k = k + 1;
                p[k] = network.columnsigmas[i];
                k = k + 1;
            }
        }
    }


    /*************************************************************************
    This  function imports  tunable   parameters  (weights/means/sigmas) which
    were exported by MLPExportTunableParameters().

    It is intended for fast copying of states between network and backup array

    INPUT PARAMETERS:
        Network     -   target:
                        * must be correctly initialized
                        * must have same geometry as network used to export params
        P           -   array with parameters

      -- ALGLIB --
         Copyright 20.06.2013 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpimporttunableparameters(multilayerperceptron network,
        double[] p,
        xparams _params)
    {
        int i = 0;
        int k = 0;
        int nin = 0;
        int nout = 0;
        int wcount = 0;

        ap.assert(ap.len(network.structinfo) > 0 && ap.len(network.structinfo) >= network.structinfo[0], "MLPImportTunableParameters: Network is uninitialized");
        mlpproperties(network, ref nin, ref nout, ref wcount, _params);
        if (mlpissoftmax(network, _params))
        {
            k = 0;
            for (i = 0; i <= wcount - 1; i++)
            {
                network.weights[i] = p[k];
                k = k + 1;
            }
            for (i = 0; i <= nin - 1; i++)
            {
                network.columnmeans[i] = p[k];
                k = k + 1;
                network.columnsigmas[i] = p[k];
                k = k + 1;
            }
        }
        else
        {
            k = 0;
            for (i = 0; i <= wcount - 1; i++)
            {
                network.weights[i] = p[k];
                k = k + 1;
            }
            for (i = 0; i <= nin + nout - 1; i++)
            {
                network.columnmeans[i] = p[k];
                k = k + 1;
                network.columnsigmas[i] = p[k];
                k = k + 1;
            }
        }
    }


    /*************************************************************************
    Serialization of MultiLayerPerceptron strucure

    INPUT PARAMETERS:
        Network -   original

    OUTPUT PARAMETERS:
        RA      -   array of real numbers which stores network,
                    array[0..RLen-1]
        RLen    -   RA lenght

      -- ALGLIB --
         Copyright 29.03.2008 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpserializeold(multilayerperceptron network,
        ref double[] ra,
        ref int rlen,
        xparams _params)
    {
        int i = 0;
        int ssize = 0;
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        int sigmalen = 0;
        int offs = 0;
        int i_ = 0;
        int i1_ = 0;

        ra = new double[0];
        rlen = 0;


        //
        // Unload info
        //
        ssize = network.structinfo[0];
        nin = network.structinfo[1];
        nout = network.structinfo[2];
        wcount = network.structinfo[4];
        if (mlpissoftmax(network, _params))
        {
            sigmalen = nin;
        }
        else
        {
            sigmalen = nin + nout;
        }

        //
        //  RA format:
        //      LEN         DESRC.
        //      1           RLen
        //      1           version (MLPVNum)
        //      1           StructInfo size
        //      SSize       StructInfo
        //      WCount      Weights
        //      SigmaLen    ColumnMeans
        //      SigmaLen    ColumnSigmas
        //
        rlen = 3 + ssize + wcount + 2 * sigmalen;
        ra = new double[rlen - 1 + 1];
        ra[0] = rlen;
        ra[1] = mlpvnum;
        ra[2] = ssize;
        offs = 3;
        for (i = 0; i <= ssize - 1; i++)
        {
            ra[offs + i] = network.structinfo[i];
        }
        offs = offs + ssize;
        i1_ = (0) - (offs);
        for (i_ = offs; i_ <= offs + wcount - 1; i_++)
        {
            ra[i_] = network.weights[i_ + i1_];
        }
        offs = offs + wcount;
        i1_ = (0) - (offs);
        for (i_ = offs; i_ <= offs + sigmalen - 1; i_++)
        {
            ra[i_] = network.columnmeans[i_ + i1_];
        }
        offs = offs + sigmalen;
        i1_ = (0) - (offs);
        for (i_ = offs; i_ <= offs + sigmalen - 1; i_++)
        {
            ra[i_] = network.columnsigmas[i_ + i1_];
        }
        offs = offs + sigmalen;
    }


    /*************************************************************************
    Unserialization of MultiLayerPerceptron strucure

    INPUT PARAMETERS:
        RA      -   real array which stores network

    OUTPUT PARAMETERS:
        Network -   restored network

      -- ALGLIB --
         Copyright 29.03.2008 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpunserializeold(double[] ra,
        multilayerperceptron network,
        xparams _params)
    {
        int i = 0;
        int ssize = 0;
        int ntotal = 0;
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        int sigmalen = 0;
        int offs = 0;
        int i_ = 0;
        int i1_ = 0;

        ap.assert((int)Math.Round(ra[1]) == mlpvnum, "MLPUnserialize: incorrect array!");

        //
        // Unload StructInfo from IA
        //
        offs = 3;
        ssize = (int)Math.Round(ra[2]);
        network.structinfo = new int[ssize - 1 + 1];
        for (i = 0; i <= ssize - 1; i++)
        {
            network.structinfo[i] = (int)Math.Round(ra[offs + i]);
        }
        offs = offs + ssize;

        //
        // Unload info from StructInfo
        //
        ssize = network.structinfo[0];
        nin = network.structinfo[1];
        nout = network.structinfo[2];
        ntotal = network.structinfo[3];
        wcount = network.structinfo[4];
        if (network.structinfo[6] == 0)
        {
            sigmalen = nin + nout;
        }
        else
        {
            sigmalen = nin;
        }

        //
        // Allocate space for other fields
        //
        network.weights = new double[wcount - 1 + 1];
        network.columnmeans = new double[sigmalen - 1 + 1];
        network.columnsigmas = new double[sigmalen - 1 + 1];
        network.neurons = new double[ntotal - 1 + 1];
        network.nwbuf = new double[Math.Max(wcount, 2 * nout) - 1 + 1];
        network.dfdnet = new double[ntotal - 1 + 1];
        network.x = new double[nin - 1 + 1];
        network.y = new double[nout - 1 + 1];
        network.derror = new double[ntotal - 1 + 1];

        //
        // Copy parameters from RA
        //
        i1_ = (offs) - (0);
        for (i_ = 0; i_ <= wcount - 1; i_++)
        {
            network.weights[i_] = ra[i_ + i1_];
        }
        offs = offs + wcount;
        i1_ = (offs) - (0);
        for (i_ = 0; i_ <= sigmalen - 1; i_++)
        {
            network.columnmeans[i_] = ra[i_ + i1_];
        }
        offs = offs + sigmalen;
        i1_ = (offs) - (0);
        for (i_ = 0; i_ <= sigmalen - 1; i_++)
        {
            network.columnsigmas[i_] = ra[i_ + i1_];
        }
        offs = offs + sigmalen;
    }


    /*************************************************************************
    Randomization of neural network weights

      -- ALGLIB --
         Copyright 06.11.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void mlprandomize(multilayerperceptron network,
        xparams _params)
    {
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        int ntotal = 0;
        int istart = 0;
        hqrnd.hqrndstate r = new hqrnd.hqrndstate();
        int entrysize = 0;
        int entryoffs = 0;
        int neuronidx = 0;
        int neurontype = 0;
        double vmean = 0;
        double vvar = 0;
        int i = 0;
        int n1 = 0;
        int n2 = 0;
        double desiredsigma = 0;
        int montecarlocnt = 0;
        double ef = 0;
        double ef2 = 0;
        double v = 0;
        double wscale = 0;

        hqrnd.hqrndrandomize(r, _params);
        mlpproperties(network, ref nin, ref nout, ref wcount, _params);
        ntotal = network.structinfo[3];
        istart = network.structinfo[5];
        desiredsigma = 0.5;
        montecarlocnt = 20;

        //
        // Stage 1:
        // * Network.Weights is filled by standard deviation of weights
        // * default values: sigma=1
        //
        for (i = 0; i <= wcount - 1; i++)
        {
            network.weights[i] = 1.0;
        }

        //
        // Stage 2:
        // * assume that input neurons have zero mean and unit standard deviation
        // * assume that constant neurons have zero standard deviation
        // * perform forward pass along neurons
        // * for each non-input non-constant neuron:
        //   * calculate mean and standard deviation of neuron's output
        //     assuming that we know means/deviations of neurons which feed it
        //     and assuming that weights has unit variance and zero mean.
        // * for each nonlinear neuron additionally we perform backward pass:
        //   * scale variances of weights which feed it in such way that neuron's
        //     input has unit standard deviation
        //
        // NOTE: this algorithm assumes that each connection feeds at most one
        //       non-linear neuron. This assumption can be incorrect in upcoming
        //       architectures with strong neurons. However, algorithm should
        //       work smoothly even in this case.
        //
        // During this stage we use Network.RndBuf, which is grouped into NTotal
        // entries, each of them having following format:
        //
        // Buf[Offset+0]        mean value of neuron's output
        // Buf[Offset+1]        standard deviation of neuron's output
        // 
        //
        //
        entrysize = 2;
        apserv.rvectorsetlengthatleast(ref network.rndbuf, entrysize * ntotal, _params);
        for (neuronidx = 0; neuronidx <= ntotal - 1; neuronidx++)
        {
            neurontype = network.structinfo[istart + neuronidx * nfieldwidth + 0];
            entryoffs = entrysize * neuronidx;
            if (neurontype == -2)
            {

                //
                // Input neuron: zero mean, unit variance.
                //
                network.rndbuf[entryoffs + 0] = 0.0;
                network.rndbuf[entryoffs + 1] = 1.0;
                continue;
            }
            if (neurontype == -3)
            {

                //
                // "-1" neuron: mean=-1, zero variance.
                //
                network.rndbuf[entryoffs + 0] = -1.0;
                network.rndbuf[entryoffs + 1] = 0.0;
                continue;
            }
            if (neurontype == -4)
            {

                //
                // "0" neuron: mean=0, zero variance.
                //
                network.rndbuf[entryoffs + 0] = 0.0;
                network.rndbuf[entryoffs + 1] = 0.0;
                continue;
            }
            if (neurontype == 0)
            {

                //
                // Adaptive summator neuron:
                // * calculate its mean and variance.
                // * we assume that weights of this neuron have unit variance and zero mean.
                // * thus, neuron's output is always have zero mean
                // * as for variance, it is a bit more interesting:
                //   * let n[i] is i-th input neuron
                //   * let w[i] is i-th weight
                //   * we assume that n[i] and w[i] are independently distributed
                //   * Var(n0*w0+n1*w1+...) = Var(n0*w0)+Var(n1*w1)+...
                //   * Var(X*Y) = mean(X)^2*Var(Y) + mean(Y)^2*Var(X) + Var(X)*Var(Y)
                //   * mean(w[i])=0, var(w[i])=1
                //   * Var(n[i]*w[i]) = mean(n[i])^2 + Var(n[i])
                //
                n1 = network.structinfo[istart + neuronidx * nfieldwidth + 2];
                n2 = n1 + network.structinfo[istart + neuronidx * nfieldwidth + 1] - 1;
                vmean = 0.0;
                vvar = 0.0;
                for (i = n1; i <= n2; i++)
                {
                    vvar = vvar + math.sqr(network.rndbuf[entrysize * i + 0]) + math.sqr(network.rndbuf[entrysize * i + 1]);
                }
                network.rndbuf[entryoffs + 0] = vmean;
                network.rndbuf[entryoffs + 1] = Math.Sqrt(vvar);
                continue;
            }
            if (neurontype == -5)
            {

                //
                // Linear activation function
                //
                i = network.structinfo[istart + neuronidx * nfieldwidth + 2];
                vmean = network.rndbuf[entrysize * i + 0];
                vvar = math.sqr(network.rndbuf[entrysize * i + 1]);
                if ((double)(vvar) > (double)(0))
                {
                    wscale = desiredsigma / Math.Sqrt(vvar);
                }
                else
                {
                    wscale = 1.0;
                }
                randomizebackwardpass(network, i, wscale, _params);
                network.rndbuf[entryoffs + 0] = vmean * wscale;
                network.rndbuf[entryoffs + 1] = desiredsigma;
                continue;
            }
            if (neurontype > 0)
            {

                //
                // Nonlinear activation function:
                // * scale its inputs
                // * estimate mean/sigma of its output using Monte-Carlo method
                //   (we simulate different inputs with unit deviation and
                //   sample activation function output on such inputs)
                //
                i = network.structinfo[istart + neuronidx * nfieldwidth + 2];
                vmean = network.rndbuf[entrysize * i + 0];
                vvar = math.sqr(network.rndbuf[entrysize * i + 1]);
                if ((double)(vvar) > (double)(0))
                {
                    wscale = desiredsigma / Math.Sqrt(vvar);
                }
                else
                {
                    wscale = 1.0;
                }
                randomizebackwardpass(network, i, wscale, _params);
                ef = 0.0;
                ef2 = 0.0;
                vmean = vmean * wscale;
                for (i = 0; i <= montecarlocnt - 1; i++)
                {
                    v = vmean + desiredsigma * hqrnd.hqrndnormal(r, _params);
                    ef = ef + v;
                    ef2 = ef2 + v * v;
                }
                ef = ef / montecarlocnt;
                ef2 = ef2 / montecarlocnt;
                network.rndbuf[entryoffs + 0] = ef;
                network.rndbuf[entryoffs + 1] = Math.Max(ef2 - ef * ef, 0.0);
                continue;
            }
            ap.assert(false, "MLPRandomize: unexpected neuron type");
        }

        //
        // Stage 3: generate weights.
        //
        for (i = 0; i <= wcount - 1; i++)
        {
            network.weights[i] = network.weights[i] * hqrnd.hqrndnormal(r, _params);
        }
    }


    /*************************************************************************
    Randomization of neural network weights and standartisator

      -- ALGLIB --
         Copyright 10.03.2008 by Bochkanov Sergey
    *************************************************************************/
    public static void mlprandomizefull(multilayerperceptron network,
        xparams _params)
    {
        int i = 0;
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        int ntotal = 0;
        int istart = 0;
        int offs = 0;
        int ntype = 0;

        mlpproperties(network, ref nin, ref nout, ref wcount, _params);
        ntotal = network.structinfo[3];
        istart = network.structinfo[5];

        //
        // Process network
        //
        mlprandomize(network, _params);
        for (i = 0; i <= nin - 1; i++)
        {
            network.columnmeans[i] = math.randomreal() - 0.5;
            network.columnsigmas[i] = math.randomreal() + 0.5;
        }
        if (!mlpissoftmax(network, _params))
        {
            for (i = 0; i <= nout - 1; i++)
            {
                offs = istart + (ntotal - nout + i) * nfieldwidth;
                ntype = network.structinfo[offs + 0];
                if (ntype == 0)
                {

                    //
                    // Shifts are changed only for linear outputs neurons
                    //
                    network.columnmeans[nin + i] = 2 * math.randomreal() - 1;
                }
                if (ntype == 0 || ntype == 3)
                {

                    //
                    // Scales are changed only for linear or bounded outputs neurons.
                    // Note that scale randomization preserves sign.
                    //
                    network.columnsigmas[nin + i] = Math.Sign(network.columnsigmas[nin + i]) * (1.5 * math.randomreal() + 0.5);
                }
            }
        }
    }


    /*************************************************************************
    Internal subroutine.

      -- ALGLIB --
         Copyright 30.03.2008 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpinitpreprocessor(multilayerperceptron network,
        double[,] xy,
        int ssize,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int jmax = 0;
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        int ntotal = 0;
        int istart = 0;
        int offs = 0;
        int ntype = 0;
        double[] means = new double[0];
        double[] sigmas = new double[0];
        double s = 0;

        mlpproperties(network, ref nin, ref nout, ref wcount, _params);
        ntotal = network.structinfo[3];
        istart = network.structinfo[5];

        //
        // Means/Sigmas
        //
        if (mlpissoftmax(network, _params))
        {
            jmax = nin - 1;
        }
        else
        {
            jmax = nin + nout - 1;
        }
        means = new double[jmax + 1];
        sigmas = new double[jmax + 1];
        for (i = 0; i <= jmax; i++)
        {
            means[i] = 0;
            sigmas[i] = 0;
        }
        for (i = 0; i <= ssize - 1; i++)
        {
            for (j = 0; j <= jmax; j++)
            {
                means[j] = means[j] + xy[i, j];
            }
        }
        for (i = 0; i <= jmax; i++)
        {
            means[i] = means[i] / ssize;
        }
        for (i = 0; i <= ssize - 1; i++)
        {
            for (j = 0; j <= jmax; j++)
            {
                sigmas[j] = sigmas[j] + math.sqr(xy[i, j] - means[j]);
            }
        }
        for (i = 0; i <= jmax; i++)
        {
            sigmas[i] = Math.Sqrt(sigmas[i] / ssize);
        }

        //
        // Inputs
        //
        for (i = 0; i <= nin - 1; i++)
        {
            network.columnmeans[i] = means[i];
            network.columnsigmas[i] = sigmas[i];
            if ((double)(network.columnsigmas[i]) == (double)(0))
            {
                network.columnsigmas[i] = 1;
            }
        }

        //
        // Outputs
        //
        if (!mlpissoftmax(network, _params))
        {
            for (i = 0; i <= nout - 1; i++)
            {
                offs = istart + (ntotal - nout + i) * nfieldwidth;
                ntype = network.structinfo[offs + 0];

                //
                // Linear outputs
                //
                if (ntype == 0)
                {
                    network.columnmeans[nin + i] = means[nin + i];
                    network.columnsigmas[nin + i] = sigmas[nin + i];
                    if ((double)(network.columnsigmas[nin + i]) == (double)(0))
                    {
                        network.columnsigmas[nin + i] = 1;
                    }
                }

                //
                // Bounded outputs (half-interval)
                //
                if (ntype == 3)
                {
                    s = means[nin + i] - network.columnmeans[nin + i];
                    if ((double)(s) == (double)(0))
                    {
                        s = Math.Sign(network.columnsigmas[nin + i]);
                    }
                    if ((double)(s) == (double)(0))
                    {
                        s = 1.0;
                    }
                    network.columnsigmas[nin + i] = Math.Sign(network.columnsigmas[nin + i]) * Math.Abs(s);
                    if ((double)(network.columnsigmas[nin + i]) == (double)(0))
                    {
                        network.columnsigmas[nin + i] = 1;
                    }
                }
            }
        }
    }


    /*************************************************************************
    Internal subroutine.
    Initialization for preprocessor based on a sample.

    INPUT
        Network -   initialized neural network;
        XY      -   sample, given by sparse matrix;
        SSize   -   sample size.

    OUTPUT
        Network -   neural network with initialised preprocessor.

      -- ALGLIB --
         Copyright 26.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpinitpreprocessorsparse(multilayerperceptron network,
        sparse.sparsematrix xy,
        int ssize,
        xparams _params)
    {
        int jmax = 0;
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        int ntotal = 0;
        int istart = 0;
        int offs = 0;
        int ntype = 0;
        double[] means = new double[0];
        double[] sigmas = new double[0];
        double s = 0;
        int i = 0;
        int j = 0;

        mlpproperties(network, ref nin, ref nout, ref wcount, _params);
        ntotal = network.structinfo[3];
        istart = network.structinfo[5];

        //
        // Means/Sigmas
        //
        if (mlpissoftmax(network, _params))
        {
            jmax = nin - 1;
        }
        else
        {
            jmax = nin + nout - 1;
        }
        means = new double[jmax + 1];
        sigmas = new double[jmax + 1];
        for (i = 0; i <= jmax; i++)
        {
            means[i] = 0;
            sigmas[i] = 0;
        }
        for (i = 0; i <= ssize - 1; i++)
        {
            sparse.sparsegetrow(xy, i, ref network.xyrow, _params);
            for (j = 0; j <= jmax; j++)
            {
                means[j] = means[j] + network.xyrow[j];
            }
        }
        for (i = 0; i <= jmax; i++)
        {
            means[i] = means[i] / ssize;
        }
        for (i = 0; i <= ssize - 1; i++)
        {
            sparse.sparsegetrow(xy, i, ref network.xyrow, _params);
            for (j = 0; j <= jmax; j++)
            {
                sigmas[j] = sigmas[j] + math.sqr(network.xyrow[j] - means[j]);
            }
        }
        for (i = 0; i <= jmax; i++)
        {
            sigmas[i] = Math.Sqrt(sigmas[i] / ssize);
        }

        //
        // Inputs
        //
        for (i = 0; i <= nin - 1; i++)
        {
            network.columnmeans[i] = means[i];
            network.columnsigmas[i] = sigmas[i];
            if ((double)(network.columnsigmas[i]) == (double)(0))
            {
                network.columnsigmas[i] = 1;
            }
        }

        //
        // Outputs
        //
        if (!mlpissoftmax(network, _params))
        {
            for (i = 0; i <= nout - 1; i++)
            {
                offs = istart + (ntotal - nout + i) * nfieldwidth;
                ntype = network.structinfo[offs + 0];

                //
                // Linear outputs
                //
                if (ntype == 0)
                {
                    network.columnmeans[nin + i] = means[nin + i];
                    network.columnsigmas[nin + i] = sigmas[nin + i];
                    if ((double)(network.columnsigmas[nin + i]) == (double)(0))
                    {
                        network.columnsigmas[nin + i] = 1;
                    }
                }

                //
                // Bounded outputs (half-interval)
                //
                if (ntype == 3)
                {
                    s = means[nin + i] - network.columnmeans[nin + i];
                    if ((double)(s) == (double)(0))
                    {
                        s = Math.Sign(network.columnsigmas[nin + i]);
                    }
                    if ((double)(s) == (double)(0))
                    {
                        s = 1.0;
                    }
                    network.columnsigmas[nin + i] = Math.Sign(network.columnsigmas[nin + i]) * Math.Abs(s);
                    if ((double)(network.columnsigmas[nin + i]) == (double)(0))
                    {
                        network.columnsigmas[nin + i] = 1;
                    }
                }
            }
        }
    }


    /*************************************************************************
    Internal subroutine.
    Initialization for preprocessor based on a subsample.

    INPUT PARAMETERS:
        Network -   network initialized with one of the network creation funcs
        XY      -   original dataset; one sample = one row;
                    first NIn columns contain inputs,
                    next NOut columns - desired outputs.
        SetSize -   real size of XY, SetSize>=0;
        Idx     -   subset of SubsetSize elements, array[SubsetSize]:
                    * Idx[I] stores row index in the original dataset which is
                      given by XY. Gradient is calculated with respect to rows
                      whose indexes are stored in Idx[].
                    * Idx[]  must store correct indexes; this function  throws
                      an  exception  in  case  incorrect index (less than 0 or
                      larger than rows(XY)) is given
                    * Idx[]  may  store  indexes  in  any  order and even with
                      repetitions.
        SubsetSize- number of elements in Idx[] array.

    OUTPUT:
        Network -   neural network with initialised preprocessor.
        
    NOTE: when  SubsetSize<0 is used full dataset by call MLPInitPreprocessor
          function.

      -- ALGLIB --
         Copyright 23.08.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpinitpreprocessorsubset(multilayerperceptron network,
        double[,] xy,
        int setsize,
        int[] idx,
        int subsetsize,
        xparams _params)
    {
        int jmax = 0;
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        int ntotal = 0;
        int istart = 0;
        int offs = 0;
        int ntype = 0;
        double[] means = new double[0];
        double[] sigmas = new double[0];
        double s = 0;
        int npoints = 0;
        int i = 0;
        int j = 0;

        ap.assert(setsize >= 0, "MLPInitPreprocessorSubset: SetSize<0");
        if (subsetsize < 0)
        {
            mlpinitpreprocessor(network, xy, setsize, _params);
            return;
        }
        ap.assert(subsetsize <= ap.len(idx), "MLPInitPreprocessorSubset: SubsetSize>Length(Idx)");
        npoints = setsize;
        for (i = 0; i <= subsetsize - 1; i++)
        {
            ap.assert(idx[i] >= 0, "MLPInitPreprocessorSubset: incorrect index of XY row(Idx[I]<0)");
            ap.assert(idx[i] <= npoints - 1, "MLPInitPreprocessorSubset: incorrect index of XY row(Idx[I]>Rows(XY)-1)");
        }
        mlpproperties(network, ref nin, ref nout, ref wcount, _params);
        ntotal = network.structinfo[3];
        istart = network.structinfo[5];

        //
        // Means/Sigmas
        //
        if (mlpissoftmax(network, _params))
        {
            jmax = nin - 1;
        }
        else
        {
            jmax = nin + nout - 1;
        }
        means = new double[jmax + 1];
        sigmas = new double[jmax + 1];
        for (i = 0; i <= jmax; i++)
        {
            means[i] = 0;
            sigmas[i] = 0;
        }
        for (i = 0; i <= subsetsize - 1; i++)
        {
            for (j = 0; j <= jmax; j++)
            {
                means[j] = means[j] + xy[idx[i], j];
            }
        }
        for (i = 0; i <= jmax; i++)
        {
            means[i] = means[i] / subsetsize;
        }
        for (i = 0; i <= subsetsize - 1; i++)
        {
            for (j = 0; j <= jmax; j++)
            {
                sigmas[j] = sigmas[j] + math.sqr(xy[idx[i], j] - means[j]);
            }
        }
        for (i = 0; i <= jmax; i++)
        {
            sigmas[i] = Math.Sqrt(sigmas[i] / subsetsize);
        }

        //
        // Inputs
        //
        for (i = 0; i <= nin - 1; i++)
        {
            network.columnmeans[i] = means[i];
            network.columnsigmas[i] = sigmas[i];
            if ((double)(network.columnsigmas[i]) == (double)(0))
            {
                network.columnsigmas[i] = 1;
            }
        }

        //
        // Outputs
        //
        if (!mlpissoftmax(network, _params))
        {
            for (i = 0; i <= nout - 1; i++)
            {
                offs = istart + (ntotal - nout + i) * nfieldwidth;
                ntype = network.structinfo[offs + 0];

                //
                // Linear outputs
                //
                if (ntype == 0)
                {
                    network.columnmeans[nin + i] = means[nin + i];
                    network.columnsigmas[nin + i] = sigmas[nin + i];
                    if ((double)(network.columnsigmas[nin + i]) == (double)(0))
                    {
                        network.columnsigmas[nin + i] = 1;
                    }
                }

                //
                // Bounded outputs (half-interval)
                //
                if (ntype == 3)
                {
                    s = means[nin + i] - network.columnmeans[nin + i];
                    if ((double)(s) == (double)(0))
                    {
                        s = Math.Sign(network.columnsigmas[nin + i]);
                    }
                    if ((double)(s) == (double)(0))
                    {
                        s = 1.0;
                    }
                    network.columnsigmas[nin + i] = Math.Sign(network.columnsigmas[nin + i]) * Math.Abs(s);
                    if ((double)(network.columnsigmas[nin + i]) == (double)(0))
                    {
                        network.columnsigmas[nin + i] = 1;
                    }
                }
            }
        }
    }


    /*************************************************************************
    Internal subroutine.
    Initialization for preprocessor based on a subsample.

    INPUT PARAMETERS:
        Network -   network initialized with one of the network creation funcs
        XY      -   original dataset, given by sparse matrix;
                    one sample = one row;
                    first NIn columns contain inputs,
                    next NOut columns - desired outputs.
        SetSize -   real size of XY, SetSize>=0;
        Idx     -   subset of SubsetSize elements, array[SubsetSize]:
                    * Idx[I] stores row index in the original dataset which is
                      given by XY. Gradient is calculated with respect to rows
                      whose indexes are stored in Idx[].
                    * Idx[]  must store correct indexes; this function  throws
                      an  exception  in  case  incorrect index (less than 0 or
                      larger than rows(XY)) is given
                    * Idx[]  may  store  indexes  in  any  order and even with
                      repetitions.
        SubsetSize- number of elements in Idx[] array.
        
    OUTPUT:
        Network -   neural network with initialised preprocessor.
        
    NOTE: when SubsetSize<0 is used full dataset by call
          MLPInitPreprocessorSparse function.
          
      -- ALGLIB --
         Copyright 26.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpinitpreprocessorsparsesubset(multilayerperceptron network,
        sparse.sparsematrix xy,
        int setsize,
        int[] idx,
        int subsetsize,
        xparams _params)
    {
        int jmax = 0;
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        int ntotal = 0;
        int istart = 0;
        int offs = 0;
        int ntype = 0;
        double[] means = new double[0];
        double[] sigmas = new double[0];
        double s = 0;
        int npoints = 0;
        int i = 0;
        int j = 0;

        ap.assert(setsize >= 0, "MLPInitPreprocessorSparseSubset: SetSize<0");
        if (subsetsize < 0)
        {
            mlpinitpreprocessorsparse(network, xy, setsize, _params);
            return;
        }
        ap.assert(subsetsize <= ap.len(idx), "MLPInitPreprocessorSparseSubset: SubsetSize>Length(Idx)");
        npoints = setsize;
        for (i = 0; i <= subsetsize - 1; i++)
        {
            ap.assert(idx[i] >= 0, "MLPInitPreprocessorSparseSubset: incorrect index of XY row(Idx[I]<0)");
            ap.assert(idx[i] <= npoints - 1, "MLPInitPreprocessorSparseSubset: incorrect index of XY row(Idx[I]>Rows(XY)-1)");
        }
        mlpproperties(network, ref nin, ref nout, ref wcount, _params);
        ntotal = network.structinfo[3];
        istart = network.structinfo[5];

        //
        // Means/Sigmas
        //
        if (mlpissoftmax(network, _params))
        {
            jmax = nin - 1;
        }
        else
        {
            jmax = nin + nout - 1;
        }
        means = new double[jmax + 1];
        sigmas = new double[jmax + 1];
        for (i = 0; i <= jmax; i++)
        {
            means[i] = 0;
            sigmas[i] = 0;
        }
        for (i = 0; i <= subsetsize - 1; i++)
        {
            sparse.sparsegetrow(xy, idx[i], ref network.xyrow, _params);
            for (j = 0; j <= jmax; j++)
            {
                means[j] = means[j] + network.xyrow[j];
            }
        }
        for (i = 0; i <= jmax; i++)
        {
            means[i] = means[i] / subsetsize;
        }
        for (i = 0; i <= subsetsize - 1; i++)
        {
            sparse.sparsegetrow(xy, idx[i], ref network.xyrow, _params);
            for (j = 0; j <= jmax; j++)
            {
                sigmas[j] = sigmas[j] + math.sqr(network.xyrow[j] - means[j]);
            }
        }
        for (i = 0; i <= jmax; i++)
        {
            sigmas[i] = Math.Sqrt(sigmas[i] / subsetsize);
        }

        //
        // Inputs
        //
        for (i = 0; i <= nin - 1; i++)
        {
            network.columnmeans[i] = means[i];
            network.columnsigmas[i] = sigmas[i];
            if ((double)(network.columnsigmas[i]) == (double)(0))
            {
                network.columnsigmas[i] = 1;
            }
        }

        //
        // Outputs
        //
        if (!mlpissoftmax(network, _params))
        {
            for (i = 0; i <= nout - 1; i++)
            {
                offs = istart + (ntotal - nout + i) * nfieldwidth;
                ntype = network.structinfo[offs + 0];

                //
                // Linear outputs
                //
                if (ntype == 0)
                {
                    network.columnmeans[nin + i] = means[nin + i];
                    network.columnsigmas[nin + i] = sigmas[nin + i];
                    if ((double)(network.columnsigmas[nin + i]) == (double)(0))
                    {
                        network.columnsigmas[nin + i] = 1;
                    }
                }

                //
                // Bounded outputs (half-interval)
                //
                if (ntype == 3)
                {
                    s = means[nin + i] - network.columnmeans[nin + i];
                    if ((double)(s) == (double)(0))
                    {
                        s = Math.Sign(network.columnsigmas[nin + i]);
                    }
                    if ((double)(s) == (double)(0))
                    {
                        s = 1.0;
                    }
                    network.columnsigmas[nin + i] = Math.Sign(network.columnsigmas[nin + i]) * Math.Abs(s);
                    if ((double)(network.columnsigmas[nin + i]) == (double)(0))
                    {
                        network.columnsigmas[nin + i] = 1;
                    }
                }
            }
        }
    }


    /*************************************************************************
    Returns information about initialized network: number of inputs, outputs,
    weights.

      -- ALGLIB --
         Copyright 04.11.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpproperties(multilayerperceptron network,
        ref int nin,
        ref int nout,
        ref int wcount,
        xparams _params)
    {
        nin = 0;
        nout = 0;
        wcount = 0;

        nin = network.structinfo[1];
        nout = network.structinfo[2];
        wcount = network.structinfo[4];
    }


    /*************************************************************************
    Returns number of "internal", low-level neurons in the network (one  which
    is stored in StructInfo).

      -- ALGLIB --
         Copyright 04.11.2007 by Bochkanov Sergey
    *************************************************************************/
    public static int mlpntotal(multilayerperceptron network,
        xparams _params)
    {
        int result = 0;

        result = network.structinfo[3];
        return result;
    }


    /*************************************************************************
    Returns number of inputs.

      -- ALGLIB --
         Copyright 19.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static int mlpgetinputscount(multilayerperceptron network,
        xparams _params)
    {
        int result = 0;

        result = network.structinfo[1];
        return result;
    }


    /*************************************************************************
    Returns number of outputs.

      -- ALGLIB --
         Copyright 19.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static int mlpgetoutputscount(multilayerperceptron network,
        xparams _params)
    {
        int result = 0;

        result = network.structinfo[2];
        return result;
    }


    /*************************************************************************
    Returns number of weights.

      -- ALGLIB --
         Copyright 19.10.2011 by Bochkanov Sergey
    *************************************************************************/
    public static int mlpgetweightscount(multilayerperceptron network,
        xparams _params)
    {
        int result = 0;

        result = network.structinfo[4];
        return result;
    }


    /*************************************************************************
    Tells whether network is SOFTMAX-normalized (i.e. classifier) or not.

      -- ALGLIB --
         Copyright 04.11.2007 by Bochkanov Sergey
    *************************************************************************/
    public static bool mlpissoftmax(multilayerperceptron network,
        xparams _params)
    {
        bool result = new bool();

        result = network.structinfo[6] == 1;
        return result;
    }


    /*************************************************************************
    This function returns total number of layers (including input, hidden and
    output layers).

      -- ALGLIB --
         Copyright 25.03.2011 by Bochkanov Sergey
    *************************************************************************/
    public static int mlpgetlayerscount(multilayerperceptron network,
        xparams _params)
    {
        int result = 0;

        result = ap.len(network.hllayersizes);
        return result;
    }


    /*************************************************************************
    This function returns size of K-th layer.

    K=0 corresponds to input layer, K=CNT-1 corresponds to output layer.

    Size of the output layer is always equal to the number of outputs, although
    when we have softmax-normalized network, last neuron doesn't have any
    connections - it is just zero.

      -- ALGLIB --
         Copyright 25.03.2011 by Bochkanov Sergey
    *************************************************************************/
    public static int mlpgetlayersize(multilayerperceptron network,
        int k,
        xparams _params)
    {
        int result = 0;

        ap.assert(k >= 0 && k < ap.len(network.hllayersizes), "MLPGetLayerSize: incorrect layer index");
        result = network.hllayersizes[k];
        return result;
    }


    /*************************************************************************
    This function returns offset/scaling coefficients for I-th input of the
    network.

    INPUT PARAMETERS:
        Network     -   network
        I           -   input index

    OUTPUT PARAMETERS:
        Mean        -   mean term
        Sigma       -   sigma term, guaranteed to be nonzero.

    I-th input is passed through linear transformation
        IN[i] = (IN[i]-Mean)/Sigma
    before feeding to the network

      -- ALGLIB --
         Copyright 25.03.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpgetinputscaling(multilayerperceptron network,
        int i,
        ref double mean,
        ref double sigma,
        xparams _params)
    {
        mean = 0;
        sigma = 0;

        ap.assert(i >= 0 && i < network.hllayersizes[0], "MLPGetInputScaling: incorrect (nonexistent) I");
        mean = network.columnmeans[i];
        sigma = network.columnsigmas[i];
        if ((double)(sigma) == (double)(0))
        {
            sigma = 1;
        }
    }


    /*************************************************************************
    This function returns offset/scaling coefficients for I-th output of the
    network.

    INPUT PARAMETERS:
        Network     -   network
        I           -   input index

    OUTPUT PARAMETERS:
        Mean        -   mean term
        Sigma       -   sigma term, guaranteed to be nonzero.

    I-th output is passed through linear transformation
        OUT[i] = OUT[i]*Sigma+Mean
    before returning it to user. In case we have SOFTMAX-normalized network,
    we return (Mean,Sigma)=(0.0,1.0).

      -- ALGLIB --
         Copyright 25.03.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpgetoutputscaling(multilayerperceptron network,
        int i,
        ref double mean,
        ref double sigma,
        xparams _params)
    {
        mean = 0;
        sigma = 0;

        ap.assert(i >= 0 && i < network.hllayersizes[ap.len(network.hllayersizes) - 1], "MLPGetOutputScaling: incorrect (nonexistent) I");
        if (network.structinfo[6] == 1)
        {
            mean = 0;
            sigma = 1;
        }
        else
        {
            mean = network.columnmeans[network.hllayersizes[0] + i];
            sigma = network.columnsigmas[network.hllayersizes[0] + i];
        }
    }


    /*************************************************************************
    This function returns information about Ith neuron of Kth layer

    INPUT PARAMETERS:
        Network     -   network
        K           -   layer index
        I           -   neuron index (within layer)

    OUTPUT PARAMETERS:
        FKind       -   activation function type (used by MLPActivationFunction())
                        this value is zero for input or linear neurons
        Threshold   -   also called offset, bias
                        zero for input neurons
                        
    NOTE: this function throws exception if layer or neuron with  given  index
    do not exists.

      -- ALGLIB --
         Copyright 25.03.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpgetneuroninfo(multilayerperceptron network,
        int k,
        int i,
        ref int fkind,
        ref double threshold,
        xparams _params)
    {
        fkind = 0;
        threshold = 0;

        mlpgetneuroninfox(network, k, i, ref network.integerbuf, ref fkind, ref threshold, _params);
    }


    /*************************************************************************
    This function returns information about connection from I0-th neuron of
    K0-th layer to I1-th neuron of K1-th layer.

    INPUT PARAMETERS:
        Network     -   network
        K0          -   layer index
        I0          -   neuron index (within layer)
        K1          -   layer index
        I1          -   neuron index (within layer)

    RESULT:
        connection weight (zero for non-existent connections)

    This function:
    1. throws exception if layer or neuron with given index do not exists.
    2. returns zero if neurons exist, but there is no connection between them

      -- ALGLIB --
         Copyright 25.03.2011 by Bochkanov Sergey
    *************************************************************************/
    public static double mlpgetweight(multilayerperceptron network,
        int k0,
        int i0,
        int k1,
        int i1,
        xparams _params)
    {
        double result = 0;

        result = mlpgetweightx(network, k0, i0, k1, i1, ref network.integerbuf, _params);
        return result;
    }


    /*************************************************************************
    This function sets offset/scaling coefficients for I-th input of the
    network.

    INPUT PARAMETERS:
        Network     -   network
        I           -   input index
        Mean        -   mean term
        Sigma       -   sigma term (if zero, will be replaced by 1.0)

    NTE: I-th input is passed through linear transformation
        IN[i] = (IN[i]-Mean)/Sigma
    before feeding to the network. This function sets Mean and Sigma.

      -- ALGLIB --
         Copyright 25.03.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpsetinputscaling(multilayerperceptron network,
        int i,
        double mean,
        double sigma,
        xparams _params)
    {
        ap.assert(i >= 0 && i < network.hllayersizes[0], "MLPSetInputScaling: incorrect (nonexistent) I");
        ap.assert(math.isfinite(mean), "MLPSetInputScaling: infinite or NAN Mean");
        ap.assert(math.isfinite(sigma), "MLPSetInputScaling: infinite or NAN Sigma");
        if ((double)(sigma) == (double)(0))
        {
            sigma = 1;
        }
        network.columnmeans[i] = mean;
        network.columnsigmas[i] = sigma;
    }


    /*************************************************************************
    This function sets offset/scaling coefficients for I-th output of the
    network.

    INPUT PARAMETERS:
        Network     -   network
        I           -   input index
        Mean        -   mean term
        Sigma       -   sigma term (if zero, will be replaced by 1.0)

    OUTPUT PARAMETERS:

    NOTE: I-th output is passed through linear transformation
        OUT[i] = OUT[i]*Sigma+Mean
    before returning it to user. This function sets Sigma/Mean. In case we
    have SOFTMAX-normalized network, you can not set (Sigma,Mean) to anything
    other than(0.0,1.0) - this function will throw exception.

      -- ALGLIB --
         Copyright 25.03.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpsetoutputscaling(multilayerperceptron network,
        int i,
        double mean,
        double sigma,
        xparams _params)
    {
        ap.assert(i >= 0 && i < network.hllayersizes[ap.len(network.hllayersizes) - 1], "MLPSetOutputScaling: incorrect (nonexistent) I");
        ap.assert(math.isfinite(mean), "MLPSetOutputScaling: infinite or NAN Mean");
        ap.assert(math.isfinite(sigma), "MLPSetOutputScaling: infinite or NAN Sigma");
        if (network.structinfo[6] == 1)
        {
            ap.assert((double)(mean) == (double)(0), "MLPSetOutputScaling: you can not set non-zero Mean term for classifier network");
            ap.assert((double)(sigma) == (double)(1), "MLPSetOutputScaling: you can not set non-unit Sigma term for classifier network");
        }
        else
        {
            if ((double)(sigma) == (double)(0))
            {
                sigma = 1;
            }
            network.columnmeans[network.hllayersizes[0] + i] = mean;
            network.columnsigmas[network.hllayersizes[0] + i] = sigma;
        }
    }


    /*************************************************************************
    This function modifies information about Ith neuron of Kth layer

    INPUT PARAMETERS:
        Network     -   network
        K           -   layer index
        I           -   neuron index (within layer)
        FKind       -   activation function type (used by MLPActivationFunction())
                        this value must be zero for input neurons
                        (you can not set activation function for input neurons)
        Threshold   -   also called offset, bias
                        this value must be zero for input neurons
                        (you can not set threshold for input neurons)

    NOTES:
    1. this function throws exception if layer or neuron with given index do
       not exists.
    2. this function also throws exception when you try to set non-linear
       activation function for input neurons (any kind of network) or for output
       neurons of classifier network.
    3. this function throws exception when you try to set non-zero threshold for
       input neurons (any kind of network).

      -- ALGLIB --
         Copyright 25.03.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpsetneuroninfo(multilayerperceptron network,
        int k,
        int i,
        int fkind,
        double threshold,
        xparams _params)
    {
        int ncnt = 0;
        int istart = 0;
        int highlevelidx = 0;
        int activationoffset = 0;

        ap.assert(math.isfinite(threshold), "MLPSetNeuronInfo: infinite or NAN Threshold");

        //
        // convenience vars
        //
        ncnt = ap.len(network.hlneurons) / hlnfieldwidth;
        istart = network.structinfo[5];

        //
        // search
        //
        network.integerbuf[0] = k;
        network.integerbuf[1] = i;
        highlevelidx = apserv.recsearch(network.hlneurons, hlnfieldwidth, 2, 0, ncnt, network.integerbuf, _params);
        ap.assert(highlevelidx >= 0, "MLPSetNeuronInfo: incorrect (nonexistent) layer or neuron index");

        //
        // activation function
        //
        if (network.hlneurons[highlevelidx * hlnfieldwidth + 2] >= 0)
        {
            activationoffset = istart + network.hlneurons[highlevelidx * hlnfieldwidth + 2] * nfieldwidth;
            network.structinfo[activationoffset + 0] = fkind;
        }
        else
        {
            ap.assert(fkind == 0, "MLPSetNeuronInfo: you try to set activation function for neuron which can not have one");
        }

        //
        // Threshold
        //
        if (network.hlneurons[highlevelidx * hlnfieldwidth + 3] >= 0)
        {
            network.weights[network.hlneurons[highlevelidx * hlnfieldwidth + 3]] = threshold;
        }
        else
        {
            ap.assert((double)(threshold) == (double)(0), "MLPSetNeuronInfo: you try to set non-zero threshold for neuron which can not have one");
        }
    }


    /*************************************************************************
    This function modifies information about connection from I0-th neuron of
    K0-th layer to I1-th neuron of K1-th layer.

    INPUT PARAMETERS:
        Network     -   network
        K0          -   layer index
        I0          -   neuron index (within layer)
        K1          -   layer index
        I1          -   neuron index (within layer)
        W           -   connection weight (must be zero for non-existent
                        connections)

    This function:
    1. throws exception if layer or neuron with given index do not exists.
    2. throws exception if you try to set non-zero weight for non-existent
       connection

      -- ALGLIB --
         Copyright 25.03.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpsetweight(multilayerperceptron network,
        int k0,
        int i0,
        int k1,
        int i1,
        double w,
        xparams _params)
    {
        int ccnt = 0;
        int highlevelidx = 0;

        ccnt = ap.len(network.hlconnections) / hlconnfieldwidth;

        //
        // check params
        //
        ap.assert(k0 >= 0 && k0 < ap.len(network.hllayersizes), "MLPSetWeight: incorrect (nonexistent) K0");
        ap.assert(i0 >= 0 && i0 < network.hllayersizes[k0], "MLPSetWeight: incorrect (nonexistent) I0");
        ap.assert(k1 >= 0 && k1 < ap.len(network.hllayersizes), "MLPSetWeight: incorrect (nonexistent) K1");
        ap.assert(i1 >= 0 && i1 < network.hllayersizes[k1], "MLPSetWeight: incorrect (nonexistent) I1");
        ap.assert(math.isfinite(w), "MLPSetWeight: infinite or NAN weight");

        //
        // search
        //
        network.integerbuf[0] = k0;
        network.integerbuf[1] = i0;
        network.integerbuf[2] = k1;
        network.integerbuf[3] = i1;
        highlevelidx = apserv.recsearch(network.hlconnections, hlconnfieldwidth, 4, 0, ccnt, network.integerbuf, _params);
        if (highlevelidx >= 0)
        {
            network.weights[network.hlconnections[highlevelidx * hlconnfieldwidth + 4]] = w;
        }
        else
        {
            ap.assert((double)(w) == (double)(0), "MLPSetWeight: you try to set non-zero weight for non-existent connection");
        }
    }


    /*************************************************************************
    Neural network activation function

    INPUT PARAMETERS:
        NET         -   neuron input
        K           -   function index (zero for linear function)

    OUTPUT PARAMETERS:
        F           -   function
        DF          -   its derivative
        D2F         -   its second derivative

      -- ALGLIB --
         Copyright 04.11.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpactivationfunction(double net,
        int k,
        ref double f,
        ref double df,
        ref double d2f,
        xparams _params)
    {
        double net2 = 0;
        double arg = 0;
        double root = 0;
        double r = 0;

        f = 0;
        df = 0;
        d2f = 0;

        if (k == 0 || k == -5)
        {
            f = net;
            df = 1;
            d2f = 0;
            return;
        }
        if (k == 1)
        {

            //
            // TanH activation function
            //
            if ((double)(Math.Abs(net)) < (double)(100))
            {
                f = Math.Tanh(net);
            }
            else
            {
                f = Math.Sign(net);
            }
            df = 1 - f * f;
            d2f = -(2 * f * df);
            return;
        }
        if (k == 3)
        {

            //
            // EX activation function
            //
            if ((double)(net) >= (double)(0))
            {
                net2 = net * net;
                arg = net2 + 1;
                root = Math.Sqrt(arg);
                f = net + root;
                r = net / root;
                df = 1 + r;
                d2f = (root - net * r) / arg;
            }
            else
            {
                f = Math.Exp(net);
                df = f;
                d2f = f;
            }
            return;
        }
        if (k == 2)
        {
            f = Math.Exp(-math.sqr(net));
            df = -(2 * net * f);
            d2f = -(2 * (f + df * net));
            return;
        }
        f = 0;
        df = 0;
        d2f = 0;
    }


    /*************************************************************************
    Procesing

    INPUT PARAMETERS:
        Network -   neural network
        X       -   input vector,  array[0..NIn-1].

    OUTPUT PARAMETERS:
        Y       -   result. Regression estimate when solving regression  task,
                    vector of posterior probabilities for classification task.

    See also MLPProcessI

      -- ALGLIB --
         Copyright 04.11.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpprocess(multilayerperceptron network,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        if (ap.len(y) < network.structinfo[2])
        {
            y = new double[network.structinfo[2]];
        }
        mlpinternalprocessvector(network.structinfo, network.weights, network.columnmeans, network.columnsigmas, ref network.neurons, ref network.dfdnet, x, ref y, _params);
    }


    /*************************************************************************
    'interactive'  variant  of  MLPProcess  for  languages  like  Python which
    support constructs like "Y = MLPProcess(NN,X)" and interactive mode of the
    interpreter

    This function allocates new array on each call,  so  it  is  significantly
    slower than its 'non-interactive' counterpart, but it is  more  convenient
    when you call it from command line.

      -- ALGLIB --
         Copyright 21.09.2010 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpprocessi(multilayerperceptron network,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        y = new double[0];

        mlpprocess(network, x, ref y, _params);
    }


    /*************************************************************************
    Error of the neural network on dataset.

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
        Network     -   neural network;
        XY          -   training  set,  see  below  for  information  on   the
                        training set format;
        NPoints     -   points count.

    RESULT:
        sum-of-squares error, SUM(sqr(y[i]-desired_y[i])/2)

    DATASET FORMAT:

    This  function  uses  two  different  dataset formats - one for regression
    networks, another one for classification networks.

    For regression networks with NIn inputs and NOut outputs following dataset
    format is used:
    * dataset is given by NPoints*(NIn+NOut) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, next NOut columns are outputs

    For classification networks with NIn inputs and NClasses clases  following
    dataset format is used:
    * dataset is given by NPoints*(NIn+1) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, last column stores class number (from 0 to
      NClasses-1).

      -- ALGLIB --
         Copyright 04.11.2007 by Bochkanov Sergey
    *************************************************************************/
    public static double mlperror(multilayerperceptron network,
        double[,] xy,
        int npoints,
        xparams _params)
    {
        double result = 0;

        ap.assert(ap.rows(xy) >= npoints, "MLPError: XY has less than NPoints rows");
        if (npoints > 0)
        {
            if (mlpissoftmax(network, _params))
            {
                ap.assert(ap.cols(xy) >= mlpgetinputscount(network, _params) + 1, "MLPError: XY has less than NIn+1 columns");
            }
            else
            {
                ap.assert(ap.cols(xy) >= mlpgetinputscount(network, _params) + mlpgetoutputscount(network, _params), "MLPError: XY has less than NIn+NOut columns");
            }
        }
        mlpallerrorsx(network, xy, network.dummysxy, npoints, 0, network.dummyidx, 0, npoints, 0, network.buf, network.err, _params);
        result = math.sqr(network.err.rmserror) * npoints * mlpgetoutputscount(network, _params) / 2;
        return result;
    }


    /*************************************************************************
    Error of the neural network on dataset given by sparse matrix.

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
        Network     -   neural network
        XY          -   training  set,  see  below  for  information  on   the
                        training set format. This function checks  correctness
                        of  the  dataset  (no  NANs/INFs,  class  numbers  are
                        correct) and throws exception when  incorrect  dataset
                        is passed.  Sparse  matrix  must  use  CRS  format for
                        storage.
        NPoints     -   points count, >=0

    RESULT:
        sum-of-squares error, SUM(sqr(y[i]-desired_y[i])/2)

    DATASET FORMAT:

    This  function  uses  two  different  dataset formats - one for regression
    networks, another one for classification networks.

    For regression networks with NIn inputs and NOut outputs following dataset
    format is used:
    * dataset is given by NPoints*(NIn+NOut) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, next NOut columns are outputs

    For classification networks with NIn inputs and NClasses clases  following
    dataset format is used:
    * dataset is given by NPoints*(NIn+1) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, last column stores class number (from 0 to
      NClasses-1).
      
      -- ALGLIB --
         Copyright 23.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static double mlperrorsparse(multilayerperceptron network,
        sparse.sparsematrix xy,
        int npoints,
        xparams _params)
    {
        double result = 0;

        ap.assert(sparse.sparseiscrs(xy, _params), "MLPErrorSparse: XY is not in CRS format.");
        ap.assert(sparse.sparsegetnrows(xy, _params) >= npoints, "MLPErrorSparse: XY has less than NPoints rows");
        if (npoints > 0)
        {
            if (mlpissoftmax(network, _params))
            {
                ap.assert(sparse.sparsegetncols(xy, _params) >= mlpgetinputscount(network, _params) + 1, "MLPErrorSparse: XY has less than NIn+1 columns");
            }
            else
            {
                ap.assert(sparse.sparsegetncols(xy, _params) >= mlpgetinputscount(network, _params) + mlpgetoutputscount(network, _params), "MLPErrorSparse: XY has less than NIn+NOut columns");
            }
        }
        mlpallerrorsx(network, network.dummydxy, xy, npoints, 1, network.dummyidx, 0, npoints, 0, network.buf, network.err, _params);
        result = math.sqr(network.err.rmserror) * npoints * mlpgetoutputscount(network, _params) / 2;
        return result;
    }


    /*************************************************************************
    Natural error function for neural network, internal subroutine.

    NOTE: this function is single-threaded. Unlike other  error  function,  it
    receives no speed-up from being executed in SMP mode.

      -- ALGLIB --
         Copyright 04.11.2007 by Bochkanov Sergey
    *************************************************************************/
    public static double mlperrorn(multilayerperceptron network,
        double[,] xy,
        int ssize,
        xparams _params)
    {
        double result = 0;
        int i = 0;
        int k = 0;
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        double e = 0;
        int i_ = 0;
        int i1_ = 0;

        mlpproperties(network, ref nin, ref nout, ref wcount, _params);
        result = 0;
        for (i = 0; i <= ssize - 1; i++)
        {

            //
            // Process vector
            //
            for (i_ = 0; i_ <= nin - 1; i_++)
            {
                network.x[i_] = xy[i, i_];
            }
            mlpprocess(network, network.x, ref network.y, _params);

            //
            // Update error function
            //
            if (network.structinfo[6] == 0)
            {

                //
                // Least squares error function
                //
                i1_ = (nin) - (0);
                for (i_ = 0; i_ <= nout - 1; i_++)
                {
                    network.y[i_] = network.y[i_] - xy[i, i_ + i1_];
                }
                e = 0.0;
                for (i_ = 0; i_ <= nout - 1; i_++)
                {
                    e += network.y[i_] * network.y[i_];
                }
                result = result + e / 2;
            }
            else
            {

                //
                // Cross-entropy error function
                //
                k = (int)Math.Round(xy[i, nin]);
                if (k >= 0 && k < nout)
                {
                    result = result + safecrossentropy(1, network.y[k], _params);
                }
            }
        }
        return result;
    }


    /*************************************************************************
    Classification error of the neural network on dataset.

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
        Network     -   neural network;
        XY          -   training  set,  see  below  for  information  on   the
                        training set format;
        NPoints     -   points count.

    RESULT:
        classification error (number of misclassified cases)

    DATASET FORMAT:

    This  function  uses  two  different  dataset formats - one for regression
    networks, another one for classification networks.

    For regression networks with NIn inputs and NOut outputs following dataset
    format is used:
    * dataset is given by NPoints*(NIn+NOut) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, next NOut columns are outputs

    For classification networks with NIn inputs and NClasses clases  following
    dataset format is used:
    * dataset is given by NPoints*(NIn+1) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, last column stores class number (from 0 to
      NClasses-1).

      -- ALGLIB --
         Copyright 04.11.2007 by Bochkanov Sergey
    *************************************************************************/
    public static int mlpclserror(multilayerperceptron network,
        double[,] xy,
        int npoints,
        xparams _params)
    {
        int result = 0;

        ap.assert(ap.rows(xy) >= npoints, "MLPClsError: XY has less than NPoints rows");
        if (npoints > 0)
        {
            if (mlpissoftmax(network, _params))
            {
                ap.assert(ap.cols(xy) >= mlpgetinputscount(network, _params) + 1, "MLPClsError: XY has less than NIn+1 columns");
            }
            else
            {
                ap.assert(ap.cols(xy) >= mlpgetinputscount(network, _params) + mlpgetoutputscount(network, _params), "MLPClsError: XY has less than NIn+NOut columns");
            }
        }
        mlpallerrorsx(network, xy, network.dummysxy, npoints, 0, network.dummyidx, 0, npoints, 0, network.buf, network.err, _params);
        result = (int)Math.Round(npoints * network.err.relclserror);
        return result;
    }


    /*************************************************************************
    Relative classification error on the test set.

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
        Network     -   neural network;
        XY          -   training  set,  see  below  for  information  on   the
                        training set format;
        NPoints     -   points count.

    RESULT:
    Percent   of incorrectly   classified  cases.  Works  both  for classifier
    networks and general purpose networks used as classifiers.

    DATASET FORMAT:

    This  function  uses  two  different  dataset formats - one for regression
    networks, another one for classification networks.

    For regression networks with NIn inputs and NOut outputs following dataset
    format is used:
    * dataset is given by NPoints*(NIn+NOut) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, next NOut columns are outputs

    For classification networks with NIn inputs and NClasses clases  following
    dataset format is used:
    * dataset is given by NPoints*(NIn+1) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, last column stores class number (from 0 to
      NClasses-1).

      -- ALGLIB --
         Copyright 25.12.2008 by Bochkanov Sergey
    *************************************************************************/
    public static double mlprelclserror(multilayerperceptron network,
        double[,] xy,
        int npoints,
        xparams _params)
    {
        double result = 0;

        ap.assert(ap.rows(xy) >= npoints, "MLPRelClsError: XY has less than NPoints rows");
        if (npoints > 0)
        {
            if (mlpissoftmax(network, _params))
            {
                ap.assert(ap.cols(xy) >= mlpgetinputscount(network, _params) + 1, "MLPRelClsError: XY has less than NIn+1 columns");
            }
            else
            {
                ap.assert(ap.cols(xy) >= mlpgetinputscount(network, _params) + mlpgetoutputscount(network, _params), "MLPRelClsError: XY has less than NIn+NOut columns");
            }
        }
        if (npoints > 0)
        {
            result = (double)mlpclserror(network, xy, npoints, _params) / (double)npoints;
        }
        else
        {
            result = 0.0;
        }
        return result;
    }


    /*************************************************************************
    Relative classification error on the test set given by sparse matrix.

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
        Network     -   neural network;
        XY          -   training  set,  see  below  for  information  on   the
                        training set format. Sparse matrix must use CRS format
                        for storage.
        NPoints     -   points count, >=0.

    RESULT:
    Percent   of incorrectly   classified  cases.  Works  both  for classifier
    networks and general purpose networks used as classifiers.

    DATASET FORMAT:

    This  function  uses  two  different  dataset formats - one for regression
    networks, another one for classification networks.

    For regression networks with NIn inputs and NOut outputs following dataset
    format is used:
    * dataset is given by NPoints*(NIn+NOut) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, next NOut columns are outputs

    For classification networks with NIn inputs and NClasses clases  following
    dataset format is used:
    * dataset is given by NPoints*(NIn+1) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, last column stores class number (from 0 to
      NClasses-1).
      
      -- ALGLIB --
         Copyright 09.08.2012 by Bochkanov Sergey
    *************************************************************************/
    public static double mlprelclserrorsparse(multilayerperceptron network,
        sparse.sparsematrix xy,
        int npoints,
        xparams _params)
    {
        double result = 0;

        ap.assert(sparse.sparseiscrs(xy, _params), "MLPRelClsErrorSparse: sparse matrix XY is not in CRS format.");
        ap.assert(sparse.sparsegetnrows(xy, _params) >= npoints, "MLPRelClsErrorSparse: sparse matrix XY has less than NPoints rows");
        if (npoints > 0)
        {
            if (mlpissoftmax(network, _params))
            {
                ap.assert(sparse.sparsegetncols(xy, _params) >= mlpgetinputscount(network, _params) + 1, "MLPRelClsErrorSparse: sparse matrix XY has less than NIn+1 columns");
            }
            else
            {
                ap.assert(sparse.sparsegetncols(xy, _params) >= mlpgetinputscount(network, _params) + mlpgetoutputscount(network, _params), "MLPRelClsErrorSparse: sparse matrix XY has less than NIn+NOut columns");
            }
        }
        mlpallerrorsx(network, network.dummydxy, xy, npoints, 1, network.dummyidx, 0, npoints, 0, network.buf, network.err, _params);
        result = network.err.relclserror;
        return result;
    }


    /*************************************************************************
    Average cross-entropy  (in bits  per element) on the test set.

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
        Network     -   neural network;
        XY          -   training  set,  see  below  for  information  on   the
                        training set format;
        NPoints     -   points count.

    RESULT:
    CrossEntropy/(NPoints*LN(2)).
    Zero if network solves regression task.

    DATASET FORMAT:

    This  function  uses  two  different  dataset formats - one for regression
    networks, another one for classification networks.

    For regression networks with NIn inputs and NOut outputs following dataset
    format is used:
    * dataset is given by NPoints*(NIn+NOut) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, next NOut columns are outputs

    For classification networks with NIn inputs and NClasses clases  following
    dataset format is used:
    * dataset is given by NPoints*(NIn+1) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, last column stores class number (from 0 to
      NClasses-1).

      -- ALGLIB --
         Copyright 08.01.2009 by Bochkanov Sergey
    *************************************************************************/
    public static double mlpavgce(multilayerperceptron network,
        double[,] xy,
        int npoints,
        xparams _params)
    {
        double result = 0;

        ap.assert(ap.rows(xy) >= npoints, "MLPAvgCE: XY has less than NPoints rows");
        if (npoints > 0)
        {
            if (mlpissoftmax(network, _params))
            {
                ap.assert(ap.cols(xy) >= mlpgetinputscount(network, _params) + 1, "MLPAvgCE: XY has less than NIn+1 columns");
            }
            else
            {
                ap.assert(ap.cols(xy) >= mlpgetinputscount(network, _params) + mlpgetoutputscount(network, _params), "MLPAvgCE: XY has less than NIn+NOut columns");
            }
        }
        mlpallerrorsx(network, xy, network.dummysxy, npoints, 0, network.dummyidx, 0, npoints, 0, network.buf, network.err, _params);
        result = network.err.avgce;
        return result;
    }


    /*************************************************************************
    Average  cross-entropy  (in bits  per element)  on the  test set  given by
    sparse matrix.

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
        Network     -   neural network;
        XY          -   training  set,  see  below  for  information  on   the
                        training set format. This function checks  correctness
                        of  the  dataset  (no  NANs/INFs,  class  numbers  are
                        correct) and throws exception when  incorrect  dataset
                        is passed.  Sparse  matrix  must  use  CRS  format for
                        storage.
        NPoints     -   points count, >=0.

    RESULT:
    CrossEntropy/(NPoints*LN(2)).
    Zero if network solves regression task.

    DATASET FORMAT:

    This  function  uses  two  different  dataset formats - one for regression
    networks, another one for classification networks.

    For regression networks with NIn inputs and NOut outputs following dataset
    format is used:
    * dataset is given by NPoints*(NIn+NOut) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, next NOut columns are outputs

    For classification networks with NIn inputs and NClasses clases  following
    dataset format is used:
    * dataset is given by NPoints*(NIn+1) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, last column stores class number (from 0 to
      NClasses-1).
      
      -- ALGLIB --
         Copyright 9.08.2012 by Bochkanov Sergey
    *************************************************************************/
    public static double mlpavgcesparse(multilayerperceptron network,
        sparse.sparsematrix xy,
        int npoints,
        xparams _params)
    {
        double result = 0;

        ap.assert(sparse.sparseiscrs(xy, _params), "MLPAvgCESparse: sparse matrix XY is not in CRS format.");
        ap.assert(sparse.sparsegetnrows(xy, _params) >= npoints, "MLPAvgCESparse: sparse matrix XY has less than NPoints rows");
        if (npoints > 0)
        {
            if (mlpissoftmax(network, _params))
            {
                ap.assert(sparse.sparsegetncols(xy, _params) >= mlpgetinputscount(network, _params) + 1, "MLPAvgCESparse: sparse matrix XY has less than NIn+1 columns");
            }
            else
            {
                ap.assert(sparse.sparsegetncols(xy, _params) >= mlpgetinputscount(network, _params) + mlpgetoutputscount(network, _params), "MLPAvgCESparse: sparse matrix XY has less than NIn+NOut columns");
            }
        }
        mlpallerrorsx(network, network.dummydxy, xy, npoints, 1, network.dummyidx, 0, npoints, 0, network.buf, network.err, _params);
        result = network.err.avgce;
        return result;
    }


    /*************************************************************************
    RMS error on the test set given.

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
        Network     -   neural network;
        XY          -   training  set,  see  below  for  information  on   the
                        training set format;
        NPoints     -   points count.

    RESULT:
    Root mean  square error. Its meaning for regression task is obvious. As for
    classification  task,  RMS  error  means  error  when estimating  posterior
    probabilities.

    DATASET FORMAT:

    This  function  uses  two  different  dataset formats - one for regression
    networks, another one for classification networks.

    For regression networks with NIn inputs and NOut outputs following dataset
    format is used:
    * dataset is given by NPoints*(NIn+NOut) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, next NOut columns are outputs

    For classification networks with NIn inputs and NClasses clases  following
    dataset format is used:
    * dataset is given by NPoints*(NIn+1) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, last column stores class number (from 0 to
      NClasses-1).

      -- ALGLIB --
         Copyright 04.11.2007 by Bochkanov Sergey
    *************************************************************************/
    public static double mlprmserror(multilayerperceptron network,
        double[,] xy,
        int npoints,
        xparams _params)
    {
        double result = 0;

        ap.assert(ap.rows(xy) >= npoints, "MLPRMSError: XY has less than NPoints rows");
        if (npoints > 0)
        {
            if (mlpissoftmax(network, _params))
            {
                ap.assert(ap.cols(xy) >= mlpgetinputscount(network, _params) + 1, "MLPRMSError: XY has less than NIn+1 columns");
            }
            else
            {
                ap.assert(ap.cols(xy) >= mlpgetinputscount(network, _params) + mlpgetoutputscount(network, _params), "MLPRMSError: XY has less than NIn+NOut columns");
            }
        }
        mlpallerrorsx(network, xy, network.dummysxy, npoints, 0, network.dummyidx, 0, npoints, 0, network.buf, network.err, _params);
        result = network.err.rmserror;
        return result;
    }


    /*************************************************************************
    RMS error on the test set given by sparse matrix.

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
        Network     -   neural network;
        XY          -   training  set,  see  below  for  information  on   the
                        training set format. This function checks  correctness
                        of  the  dataset  (no  NANs/INFs,  class  numbers  are
                        correct) and throws exception when  incorrect  dataset
                        is passed.  Sparse  matrix  must  use  CRS  format for
                        storage.
        NPoints     -   points count, >=0.

    RESULT:
    Root mean  square error. Its meaning for regression task is obvious. As for
    classification  task,  RMS  error  means  error  when estimating  posterior
    probabilities.

    DATASET FORMAT:

    This  function  uses  two  different  dataset formats - one for regression
    networks, another one for classification networks.

    For regression networks with NIn inputs and NOut outputs following dataset
    format is used:
    * dataset is given by NPoints*(NIn+NOut) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, next NOut columns are outputs

    For classification networks with NIn inputs and NClasses clases  following
    dataset format is used:
    * dataset is given by NPoints*(NIn+1) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, last column stores class number (from 0 to
      NClasses-1).
      
      -- ALGLIB --
         Copyright 09.08.2012 by Bochkanov Sergey
    *************************************************************************/
    public static double mlprmserrorsparse(multilayerperceptron network,
        sparse.sparsematrix xy,
        int npoints,
        xparams _params)
    {
        double result = 0;

        ap.assert(sparse.sparseiscrs(xy, _params), "MLPRMSErrorSparse: sparse matrix XY is not in CRS format.");
        ap.assert(sparse.sparsegetnrows(xy, _params) >= npoints, "MLPRMSErrorSparse: sparse matrix XY has less than NPoints rows");
        if (npoints > 0)
        {
            if (mlpissoftmax(network, _params))
            {
                ap.assert(sparse.sparsegetncols(xy, _params) >= mlpgetinputscount(network, _params) + 1, "MLPRMSErrorSparse: sparse matrix XY has less than NIn+1 columns");
            }
            else
            {
                ap.assert(sparse.sparsegetncols(xy, _params) >= mlpgetinputscount(network, _params) + mlpgetoutputscount(network, _params), "MLPRMSErrorSparse: sparse matrix XY has less than NIn+NOut columns");
            }
        }
        mlpallerrorsx(network, network.dummydxy, xy, npoints, 1, network.dummyidx, 0, npoints, 0, network.buf, network.err, _params);
        result = network.err.rmserror;
        return result;
    }


    /*************************************************************************
    Average absolute error on the test set.

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
        Network     -   neural network;
        XY          -   training  set,  see  below  for  information  on   the
                        training set format;
        NPoints     -   points count.

    RESULT:
    Its meaning for regression task is obvious. As for classification task, it
    means average error when estimating posterior probabilities.

    DATASET FORMAT:

    This  function  uses  two  different  dataset formats - one for regression
    networks, another one for classification networks.

    For regression networks with NIn inputs and NOut outputs following dataset
    format is used:
    * dataset is given by NPoints*(NIn+NOut) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, next NOut columns are outputs

    For classification networks with NIn inputs and NClasses clases  following
    dataset format is used:
    * dataset is given by NPoints*(NIn+1) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, last column stores class number (from 0 to
      NClasses-1).

      -- ALGLIB --
         Copyright 11.03.2008 by Bochkanov Sergey
    *************************************************************************/
    public static double mlpavgerror(multilayerperceptron network,
        double[,] xy,
        int npoints,
        xparams _params)
    {
        double result = 0;

        ap.assert(ap.rows(xy) >= npoints, "MLPAvgError: XY has less than NPoints rows");
        if (npoints > 0)
        {
            if (mlpissoftmax(network, _params))
            {
                ap.assert(ap.cols(xy) >= mlpgetinputscount(network, _params) + 1, "MLPAvgError: XY has less than NIn+1 columns");
            }
            else
            {
                ap.assert(ap.cols(xy) >= mlpgetinputscount(network, _params) + mlpgetoutputscount(network, _params), "MLPAvgError: XY has less than NIn+NOut columns");
            }
        }
        mlpallerrorsx(network, xy, network.dummysxy, npoints, 0, network.dummyidx, 0, npoints, 0, network.buf, network.err, _params);
        result = network.err.avgerror;
        return result;
    }


    /*************************************************************************
    Average absolute error on the test set given by sparse matrix.

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
        Network     -   neural network;
        XY          -   training  set,  see  below  for  information  on   the
                        training set format. This function checks  correctness
                        of  the  dataset  (no  NANs/INFs,  class  numbers  are
                        correct) and throws exception when  incorrect  dataset
                        is passed.  Sparse  matrix  must  use  CRS  format for
                        storage.
        NPoints     -   points count, >=0.

    RESULT:
    Its meaning for regression task is obvious. As for classification task, it
    means average error when estimating posterior probabilities.

    DATASET FORMAT:

    This  function  uses  two  different  dataset formats - one for regression
    networks, another one for classification networks.

    For regression networks with NIn inputs and NOut outputs following dataset
    format is used:
    * dataset is given by NPoints*(NIn+NOut) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, next NOut columns are outputs

    For classification networks with NIn inputs and NClasses clases  following
    dataset format is used:
    * dataset is given by NPoints*(NIn+1) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, last column stores class number (from 0 to
      NClasses-1).
      
      -- ALGLIB --
         Copyright 09.08.2012 by Bochkanov Sergey
    *************************************************************************/
    public static double mlpavgerrorsparse(multilayerperceptron network,
        sparse.sparsematrix xy,
        int npoints,
        xparams _params)
    {
        double result = 0;

        ap.assert(sparse.sparseiscrs(xy, _params), "MLPAvgErrorSparse: XY is not in CRS format.");
        ap.assert(sparse.sparsegetnrows(xy, _params) >= npoints, "MLPAvgErrorSparse: XY has less than NPoints rows");
        if (npoints > 0)
        {
            if (mlpissoftmax(network, _params))
            {
                ap.assert(sparse.sparsegetncols(xy, _params) >= mlpgetinputscount(network, _params) + 1, "MLPAvgErrorSparse: XY has less than NIn+1 columns");
            }
            else
            {
                ap.assert(sparse.sparsegetncols(xy, _params) >= mlpgetinputscount(network, _params) + mlpgetoutputscount(network, _params), "MLPAvgErrorSparse: XY has less than NIn+NOut columns");
            }
        }
        mlpallerrorsx(network, network.dummydxy, xy, npoints, 1, network.dummyidx, 0, npoints, 0, network.buf, network.err, _params);
        result = network.err.avgerror;
        return result;
    }


    /*************************************************************************
    Average relative error on the test set.

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
        Network     -   neural network;
        XY          -   training  set,  see  below  for  information  on   the
                        training set format;
        NPoints     -   points count.

    RESULT:
    Its meaning for regression task is obvious. As for classification task, it
    means  average  relative  error  when  estimating posterior probability of
    belonging to the correct class.

    DATASET FORMAT:

    This  function  uses  two  different  dataset formats - one for regression
    networks, another one for classification networks.

    For regression networks with NIn inputs and NOut outputs following dataset
    format is used:
    * dataset is given by NPoints*(NIn+NOut) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, next NOut columns are outputs

    For classification networks with NIn inputs and NClasses clases  following
    dataset format is used:
    * dataset is given by NPoints*(NIn+1) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, last column stores class number (from 0 to
      NClasses-1).

      -- ALGLIB --
         Copyright 11.03.2008 by Bochkanov Sergey
    *************************************************************************/
    public static double mlpavgrelerror(multilayerperceptron network,
        double[,] xy,
        int npoints,
        xparams _params)
    {
        double result = 0;

        ap.assert(ap.rows(xy) >= npoints, "MLPAvgRelError: XY has less than NPoints rows");
        if (npoints > 0)
        {
            if (mlpissoftmax(network, _params))
            {
                ap.assert(ap.cols(xy) >= mlpgetinputscount(network, _params) + 1, "MLPAvgRelError: XY has less than NIn+1 columns");
            }
            else
            {
                ap.assert(ap.cols(xy) >= mlpgetinputscount(network, _params) + mlpgetoutputscount(network, _params), "MLPAvgRelError: XY has less than NIn+NOut columns");
            }
        }
        mlpallerrorsx(network, xy, network.dummysxy, npoints, 0, network.dummyidx, 0, npoints, 0, network.buf, network.err, _params);
        result = network.err.avgrelerror;
        return result;
    }


    /*************************************************************************
    Average relative error on the test set given by sparse matrix.

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
        Network     -   neural network;
        XY          -   training  set,  see  below  for  information  on   the
                        training set format. This function checks  correctness
                        of  the  dataset  (no  NANs/INFs,  class  numbers  are
                        correct) and throws exception when  incorrect  dataset
                        is passed.  Sparse  matrix  must  use  CRS  format for
                        storage.
        NPoints     -   points count, >=0.

    RESULT:
    Its meaning for regression task is obvious. As for classification task, it
    means  average  relative  error  when  estimating posterior probability of
    belonging to the correct class.

    DATASET FORMAT:

    This  function  uses  two  different  dataset formats - one for regression
    networks, another one for classification networks.

    For regression networks with NIn inputs and NOut outputs following dataset
    format is used:
    * dataset is given by NPoints*(NIn+NOut) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, next NOut columns are outputs

    For classification networks with NIn inputs and NClasses clases  following
    dataset format is used:
    * dataset is given by NPoints*(NIn+1) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, last column stores class number (from 0 to
      NClasses-1).
      
      -- ALGLIB --
         Copyright 09.08.2012 by Bochkanov Sergey
    *************************************************************************/
    public static double mlpavgrelerrorsparse(multilayerperceptron network,
        sparse.sparsematrix xy,
        int npoints,
        xparams _params)
    {
        double result = 0;

        ap.assert(sparse.sparseiscrs(xy, _params), "MLPAvgRelErrorSparse: XY is not in CRS format.");
        ap.assert(sparse.sparsegetnrows(xy, _params) >= npoints, "MLPAvgRelErrorSparse: XY has less than NPoints rows");
        if (npoints > 0)
        {
            if (mlpissoftmax(network, _params))
            {
                ap.assert(sparse.sparsegetncols(xy, _params) >= mlpgetinputscount(network, _params) + 1, "MLPAvgRelErrorSparse: XY has less than NIn+1 columns");
            }
            else
            {
                ap.assert(sparse.sparsegetncols(xy, _params) >= mlpgetinputscount(network, _params) + mlpgetoutputscount(network, _params), "MLPAvgRelErrorSparse: XY has less than NIn+NOut columns");
            }
        }
        mlpallerrorsx(network, network.dummydxy, xy, npoints, 1, network.dummyidx, 0, npoints, 0, network.buf, network.err, _params);
        result = network.err.avgrelerror;
        return result;
    }


    /*************************************************************************
    Gradient calculation

    INPUT PARAMETERS:
        Network -   network initialized with one of the network creation funcs
        X       -   input vector, length of array must be at least NIn
        DesiredY-   desired outputs, length of array must be at least NOut
        Grad    -   possibly preallocated array. If size of array is smaller
                    than WCount, it will be reallocated. It is recommended to
                    reuse previously allocated array to reduce allocation
                    overhead.

    OUTPUT PARAMETERS:
        E       -   error function, SUM(sqr(y[i]-desiredy[i])/2,i)
        Grad    -   gradient of E with respect to weights of network, array[WCount]
        
      -- ALGLIB --
         Copyright 04.11.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpgrad(multilayerperceptron network,
        double[] x,
        double[] desiredy,
        ref double e,
        ref double[] grad,
        xparams _params)
    {
        int i = 0;
        int nout = 0;
        int ntotal = 0;

        e = 0;


        //
        // Alloc
        //
        apserv.rvectorsetlengthatleast(ref grad, network.structinfo[4], _params);

        //
        // Prepare dError/dOut, internal structures
        //
        mlpprocess(network, x, ref network.y, _params);
        nout = network.structinfo[2];
        ntotal = network.structinfo[3];
        e = 0;
        for (i = 0; i <= ntotal - 1; i++)
        {
            network.derror[i] = 0;
        }
        for (i = 0; i <= nout - 1; i++)
        {
            network.derror[ntotal - nout + i] = network.y[i] - desiredy[i];
            e = e + math.sqr(network.y[i] - desiredy[i]) / 2;
        }

        //
        // gradient
        //
        mlpinternalcalculategradient(network, network.neurons, network.weights, ref network.derror, ref grad, false, _params);
    }


    /*************************************************************************
    Gradient calculation (natural error function is used)

    INPUT PARAMETERS:
        Network -   network initialized with one of the network creation funcs
        X       -   input vector, length of array must be at least NIn
        DesiredY-   desired outputs, length of array must be at least NOut
        Grad    -   possibly preallocated array. If size of array is smaller
                    than WCount, it will be reallocated. It is recommended to
                    reuse previously allocated array to reduce allocation
                    overhead.

    OUTPUT PARAMETERS:
        E       -   error function, sum-of-squares for regression networks,
                    cross-entropy for classification networks.
        Grad    -   gradient of E with respect to weights of network, array[WCount]

      -- ALGLIB --
         Copyright 04.11.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpgradn(multilayerperceptron network,
        double[] x,
        double[] desiredy,
        ref double e,
        ref double[] grad,
        xparams _params)
    {
        double s = 0;
        int i = 0;
        int nout = 0;
        int ntotal = 0;

        e = 0;


        //
        // Alloc
        //
        apserv.rvectorsetlengthatleast(ref grad, network.structinfo[4], _params);

        //
        // Prepare dError/dOut, internal structures
        //
        mlpprocess(network, x, ref network.y, _params);
        nout = network.structinfo[2];
        ntotal = network.structinfo[3];
        for (i = 0; i <= ntotal - 1; i++)
        {
            network.derror[i] = 0;
        }
        e = 0;
        if (network.structinfo[6] == 0)
        {

            //
            // Regression network, least squares
            //
            for (i = 0; i <= nout - 1; i++)
            {
                network.derror[ntotal - nout + i] = network.y[i] - desiredy[i];
                e = e + math.sqr(network.y[i] - desiredy[i]) / 2;
            }
        }
        else
        {

            //
            // Classification network, cross-entropy
            //
            s = 0;
            for (i = 0; i <= nout - 1; i++)
            {
                s = s + desiredy[i];
            }
            for (i = 0; i <= nout - 1; i++)
            {
                network.derror[ntotal - nout + i] = s * network.y[i] - desiredy[i];
                e = e + safecrossentropy(desiredy[i], network.y[i], _params);
            }
        }

        //
        // gradient
        //
        mlpinternalcalculategradient(network, network.neurons, network.weights, ref network.derror, ref grad, true, _params);
    }


    /*************************************************************************
    Batch gradient calculation for a set of inputs/outputs

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
        Network -   network initialized with one of the network creation funcs
        XY      -   original dataset in dense format; one sample = one row:
                    * first NIn columns contain inputs,
                    * for regression problem, next NOut columns store
                      desired outputs.
                    * for classification problem, next column (just one!)
                      stores class number.
        SSize   -   number of elements in XY
        Grad    -   possibly preallocated array. If size of array is smaller
                    than WCount, it will be reallocated. It is recommended to
                    reuse previously allocated array to reduce allocation
                    overhead.

    OUTPUT PARAMETERS:
        E       -   error function, SUM(sqr(y[i]-desiredy[i])/2,i)
        Grad    -   gradient of E with respect to weights of network, array[WCount]

      -- ALGLIB --
         Copyright 04.11.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpgradbatch(multilayerperceptron network,
        double[,] xy,
        int ssize,
        ref double e,
        ref double[] grad,
        xparams _params)
    {
        int i = 0;
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        int subset0 = 0;
        int subset1 = 0;
        int subsettype = 0;
        smlpgrad sgrad = null;

        e = 0;

        ap.assert(ssize >= 0, "MLPGradBatchSparse: SSize<0");
        subset0 = 0;
        subset1 = ssize;
        subsettype = 0;
        mlpproperties(network, ref nin, ref nout, ref wcount, _params);
        apserv.rvectorsetlengthatleast(ref grad, wcount, _params);
        smp.ae_shared_pool_first_recycled(network.gradbuf, ref sgrad);
        while (sgrad != null)
        {
            sgrad.f = 0.0;
            for (i = 0; i <= wcount - 1; i++)
            {
                sgrad.g[i] = 0.0;
            }
            smp.ae_shared_pool_next_recycled(network.gradbuf, ref sgrad);
        }
        mlpgradbatchx(network, xy, network.dummysxy, ssize, 0, network.dummyidx, subset0, subset1, subsettype, network.buf, network.gradbuf, _params);
        e = 0.0;
        for (i = 0; i <= wcount - 1; i++)
        {
            grad[i] = 0.0;
        }
        smp.ae_shared_pool_first_recycled(network.gradbuf, ref sgrad);
        while (sgrad != null)
        {
            e = e + sgrad.f;
            for (i = 0; i <= wcount - 1; i++)
            {
                grad[i] = grad[i] + sgrad.g[i];
            }
            smp.ae_shared_pool_next_recycled(network.gradbuf, ref sgrad);
        }
    }


    /*************************************************************************
    Batch gradient calculation for a set  of inputs/outputs  given  by  sparse
    matrices

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
        Network -   network initialized with one of the network creation funcs
        XY      -   original dataset in sparse format; one sample = one row:
                    * MATRIX MUST BE STORED IN CRS FORMAT
                    * first NIn columns contain inputs.
                    * for regression problem, next NOut columns store
                      desired outputs.
                    * for classification problem, next column (just one!)
                      stores class number.
        SSize   -   number of elements in XY
        Grad    -   possibly preallocated array. If size of array is smaller
                    than WCount, it will be reallocated. It is recommended to
                    reuse previously allocated array to reduce allocation
                    overhead.

    OUTPUT PARAMETERS:
        E       -   error function, SUM(sqr(y[i]-desiredy[i])/2,i)
        Grad    -   gradient of E with respect to weights of network, array[WCount]

      -- ALGLIB --
         Copyright 26.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpgradbatchsparse(multilayerperceptron network,
        sparse.sparsematrix xy,
        int ssize,
        ref double e,
        ref double[] grad,
        xparams _params)
    {
        int i = 0;
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        int subset0 = 0;
        int subset1 = 0;
        int subsettype = 0;
        smlpgrad sgrad = null;

        e = 0;

        ap.assert(ssize >= 0, "MLPGradBatchSparse: SSize<0");
        ap.assert(sparse.sparseiscrs(xy, _params), "MLPGradBatchSparse: sparse matrix XY must be in CRS format.");
        subset0 = 0;
        subset1 = ssize;
        subsettype = 0;
        mlpproperties(network, ref nin, ref nout, ref wcount, _params);
        apserv.rvectorsetlengthatleast(ref grad, wcount, _params);
        smp.ae_shared_pool_first_recycled(network.gradbuf, ref sgrad);
        while (sgrad != null)
        {
            sgrad.f = 0.0;
            for (i = 0; i <= wcount - 1; i++)
            {
                sgrad.g[i] = 0.0;
            }
            smp.ae_shared_pool_next_recycled(network.gradbuf, ref sgrad);
        }
        mlpgradbatchx(network, network.dummydxy, xy, ssize, 1, network.dummyidx, subset0, subset1, subsettype, network.buf, network.gradbuf, _params);
        e = 0.0;
        for (i = 0; i <= wcount - 1; i++)
        {
            grad[i] = 0.0;
        }
        smp.ae_shared_pool_first_recycled(network.gradbuf, ref sgrad);
        while (sgrad != null)
        {
            e = e + sgrad.f;
            for (i = 0; i <= wcount - 1; i++)
            {
                grad[i] = grad[i] + sgrad.g[i];
            }
            smp.ae_shared_pool_next_recycled(network.gradbuf, ref sgrad);
        }
    }


    /*************************************************************************
    Batch gradient calculation for a subset of dataset

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
        Network -   network initialized with one of the network creation funcs
        XY      -   original dataset in dense format; one sample = one row:
                    * first NIn columns contain inputs,
                    * for regression problem, next NOut columns store
                      desired outputs.
                    * for classification problem, next column (just one!)
                      stores class number.
        SetSize -   real size of XY, SetSize>=0;
        Idx     -   subset of SubsetSize elements, array[SubsetSize]:
                    * Idx[I] stores row index in the original dataset which is
                      given by XY. Gradient is calculated with respect to rows
                      whose indexes are stored in Idx[].
                    * Idx[]  must store correct indexes; this function  throws
                      an  exception  in  case  incorrect index (less than 0 or
                      larger than rows(XY)) is given
                    * Idx[]  may  store  indexes  in  any  order and even with
                      repetitions.
        SubsetSize- number of elements in Idx[] array:
                    * positive value means that subset given by Idx[] is processed
                    * zero value results in zero gradient
                    * negative value means that full dataset is processed
        Grad      - possibly  preallocated array. If size of array is  smaller
                    than WCount, it will be reallocated. It is  recommended to
                    reuse  previously  allocated  array  to  reduce allocation
                    overhead.

    OUTPUT PARAMETERS:
        E         - error function, SUM(sqr(y[i]-desiredy[i])/2,i)
        Grad      - gradient  of  E  with  respect   to  weights  of  network,
                    array[WCount]

      -- ALGLIB --
         Copyright 26.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpgradbatchsubset(multilayerperceptron network,
        double[,] xy,
        int setsize,
        int[] idx,
        int subsetsize,
        ref double e,
        ref double[] grad,
        xparams _params)
    {
        int i = 0;
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        int npoints = 0;
        int subset0 = 0;
        int subset1 = 0;
        int subsettype = 0;
        smlpgrad sgrad = null;

        e = 0;

        ap.assert(setsize >= 0, "MLPGradBatchSubset: SetSize<0");
        ap.assert(subsetsize <= ap.len(idx), "MLPGradBatchSubset: SubsetSize>Length(Idx)");
        npoints = setsize;
        if (subsetsize < 0)
        {
            subset0 = 0;
            subset1 = setsize;
            subsettype = 0;
        }
        else
        {
            subset0 = 0;
            subset1 = subsetsize;
            subsettype = 1;
            for (i = 0; i <= subsetsize - 1; i++)
            {
                ap.assert(idx[i] >= 0, "MLPGradBatchSubset: incorrect index of XY row(Idx[I]<0)");
                ap.assert(idx[i] <= npoints - 1, "MLPGradBatchSubset: incorrect index of XY row(Idx[I]>Rows(XY)-1)");
            }
        }
        mlpproperties(network, ref nin, ref nout, ref wcount, _params);
        apserv.rvectorsetlengthatleast(ref grad, wcount, _params);
        smp.ae_shared_pool_first_recycled(network.gradbuf, ref sgrad);
        while (sgrad != null)
        {
            sgrad.f = 0.0;
            for (i = 0; i <= wcount - 1; i++)
            {
                sgrad.g[i] = 0.0;
            }
            smp.ae_shared_pool_next_recycled(network.gradbuf, ref sgrad);
        }
        mlpgradbatchx(network, xy, network.dummysxy, setsize, 0, idx, subset0, subset1, subsettype, network.buf, network.gradbuf, _params);
        e = 0.0;
        for (i = 0; i <= wcount - 1; i++)
        {
            grad[i] = 0.0;
        }
        smp.ae_shared_pool_first_recycled(network.gradbuf, ref sgrad);
        while (sgrad != null)
        {
            e = e + sgrad.f;
            for (i = 0; i <= wcount - 1; i++)
            {
                grad[i] = grad[i] + sgrad.g[i];
            }
            smp.ae_shared_pool_next_recycled(network.gradbuf, ref sgrad);
        }
    }


    /*************************************************************************
    Batch gradient calculation for a set of inputs/outputs  for  a  subset  of
    dataset given by set of indexes.

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
        Network -   network initialized with one of the network creation funcs
        XY      -   original dataset in sparse format; one sample = one row:
                    * MATRIX MUST BE STORED IN CRS FORMAT
                    * first NIn columns contain inputs,
                    * for regression problem, next NOut columns store
                      desired outputs.
                    * for classification problem, next column (just one!)
                      stores class number.
        SetSize -   real size of XY, SetSize>=0;
        Idx     -   subset of SubsetSize elements, array[SubsetSize]:
                    * Idx[I] stores row index in the original dataset which is
                      given by XY. Gradient is calculated with respect to rows
                      whose indexes are stored in Idx[].
                    * Idx[]  must store correct indexes; this function  throws
                      an  exception  in  case  incorrect index (less than 0 or
                      larger than rows(XY)) is given
                    * Idx[]  may  store  indexes  in  any  order and even with
                      repetitions.
        SubsetSize- number of elements in Idx[] array:
                    * positive value means that subset given by Idx[] is processed
                    * zero value results in zero gradient
                    * negative value means that full dataset is processed
        Grad      - possibly  preallocated array. If size of array is  smaller
                    than WCount, it will be reallocated. It is  recommended to
                    reuse  previously  allocated  array  to  reduce allocation
                    overhead.

    OUTPUT PARAMETERS:
        E       -   error function, SUM(sqr(y[i]-desiredy[i])/2,i)
        Grad    -   gradient  of  E  with  respect   to  weights  of  network,
                    array[WCount]

    NOTE: when  SubsetSize<0 is used full dataset by call MLPGradBatchSparse
          function.
        
      -- ALGLIB --
         Copyright 26.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpgradbatchsparsesubset(multilayerperceptron network,
        sparse.sparsematrix xy,
        int setsize,
        int[] idx,
        int subsetsize,
        ref double e,
        ref double[] grad,
        xparams _params)
    {
        int i = 0;
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        int npoints = 0;
        int subset0 = 0;
        int subset1 = 0;
        int subsettype = 0;
        smlpgrad sgrad = null;

        e = 0;

        ap.assert(setsize >= 0, "MLPGradBatchSparseSubset: SetSize<0");
        ap.assert(subsetsize <= ap.len(idx), "MLPGradBatchSparseSubset: SubsetSize>Length(Idx)");
        ap.assert(sparse.sparseiscrs(xy, _params), "MLPGradBatchSparseSubset: sparse matrix XY must be in CRS format.");
        npoints = setsize;
        if (subsetsize < 0)
        {
            subset0 = 0;
            subset1 = setsize;
            subsettype = 0;
        }
        else
        {
            subset0 = 0;
            subset1 = subsetsize;
            subsettype = 1;
            for (i = 0; i <= subsetsize - 1; i++)
            {
                ap.assert(idx[i] >= 0, "MLPGradBatchSparseSubset: incorrect index of XY row(Idx[I]<0)");
                ap.assert(idx[i] <= npoints - 1, "MLPGradBatchSparseSubset: incorrect index of XY row(Idx[I]>Rows(XY)-1)");
            }
        }
        mlpproperties(network, ref nin, ref nout, ref wcount, _params);
        apserv.rvectorsetlengthatleast(ref grad, wcount, _params);
        smp.ae_shared_pool_first_recycled(network.gradbuf, ref sgrad);
        while (sgrad != null)
        {
            sgrad.f = 0.0;
            for (i = 0; i <= wcount - 1; i++)
            {
                sgrad.g[i] = 0.0;
            }
            smp.ae_shared_pool_next_recycled(network.gradbuf, ref sgrad);
        }
        mlpgradbatchx(network, network.dummydxy, xy, setsize, 1, idx, subset0, subset1, subsettype, network.buf, network.gradbuf, _params);
        e = 0.0;
        for (i = 0; i <= wcount - 1; i++)
        {
            grad[i] = 0.0;
        }
        smp.ae_shared_pool_first_recycled(network.gradbuf, ref sgrad);
        while (sgrad != null)
        {
            e = e + sgrad.f;
            for (i = 0; i <= wcount - 1; i++)
            {
                grad[i] = grad[i] + sgrad.g[i];
            }
            smp.ae_shared_pool_next_recycled(network.gradbuf, ref sgrad);
        }
    }


    /*************************************************************************
    Internal function which actually calculates batch gradient for a subset or
    full dataset, which can be represented in different formats.

    THIS FUNCTION IS NOT INTENDED TO BE USED BY ALGLIB USERS!

      -- ALGLIB --
         Copyright 26.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpgradbatchx(multilayerperceptron network,
        double[,] densexy,
        sparse.sparsematrix sparsexy,
        int datasetsize,
        int datasettype,
        int[] idx,
        int subset0,
        int subset1,
        int subsettype,
        smp.shared_pool buf,
        smp.shared_pool gradbuf,
        xparams _params)
    {
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        int rowsize = 0;
        int srcidx = 0;
        int cstart = 0;
        int csize = 0;
        int j = 0;
        double problemcost = 0;
        hpccores.mlpbuffers buf2 = null;
        int len0 = 0;
        int len1 = 0;
        hpccores.mlpbuffers pbuf = null;
        smlpgrad sgrad = null;
        int i_ = 0;

        ap.assert(datasetsize >= 0, "MLPGradBatchX: SetSize<0");
        ap.assert(datasettype == 0 || datasettype == 1, "MLPGradBatchX: DatasetType is incorrect");
        ap.assert(subsettype == 0 || subsettype == 1, "MLPGradBatchX: SubsetType is incorrect");

        //
        // Determine network and dataset properties
        //
        mlpproperties(network, ref nin, ref nout, ref wcount, _params);
        if (mlpissoftmax(network, _params))
        {
            rowsize = nin + 1;
        }
        else
        {
            rowsize = nin + nout;
        }

        //
        // Split problem.
        //
        // Splitting problem allows us to reduce  effect  of  single-precision
        // arithmetics (SSE-optimized version of MLPChunkedGradient uses single
        // precision  internally, but converts them to  double precision after
        // results are exported from HPC buffer to network). Small batches are
        // calculated in single precision, results are  aggregated  in  double
        // precision, and it allows us to avoid accumulation  of  errors  when
        // we process very large batches (tens of thousands of items).
        //
        // NOTE: it is important to use real arithmetics for ProblemCost
        //       because ProblemCost may be larger than MAXINT.
        //
        problemcost = subset1 - subset0;
        problemcost = problemcost * wcount * 2;
        if ((double)(problemcost) >= (double)(apserv.smpactivationlevel(_params)) && subset1 - subset0 >= 2 * microbatchsize)
        {
            if (_trypexec_mlpgradbatchx(network, densexy, sparsexy, datasetsize, datasettype, idx, subset0, subset1, subsettype, buf, gradbuf, _params))
            {
                return;
            }
        }
        if (subset1 - subset0 >= 2 * microbatchsize && (double)(problemcost) > (double)(apserv.spawnlevel(_params)))
        {
            apserv.splitlength(subset1 - subset0, microbatchsize, ref len0, ref len1, _params);
            mlpgradbatchx(network, densexy, sparsexy, datasetsize, datasettype, idx, subset0, subset0 + len0, subsettype, buf, gradbuf, _params);
            mlpgradbatchx(network, densexy, sparsexy, datasetsize, datasettype, idx, subset0 + len0, subset1, subsettype, buf, gradbuf, _params);
            return;
        }

        //
        // Chunked processing
        //
        smp.ae_shared_pool_retrieve(gradbuf, ref sgrad);
        smp.ae_shared_pool_retrieve(buf, ref pbuf);
        hpccores.hpcpreparechunkedgradient(network.weights, wcount, mlpntotal(network, _params), nin, nout, pbuf, _params);
        cstart = subset0;
        while (cstart < subset1)
        {

            //
            // Determine size of current chunk and copy it to PBuf.XY
            //
            csize = Math.Min(subset1, cstart + pbuf.chunksize) - cstart;
            for (j = 0; j <= csize - 1; j++)
            {
                srcidx = -1;
                if (subsettype == 0)
                {
                    srcidx = cstart + j;
                }
                if (subsettype == 1)
                {
                    srcidx = idx[cstart + j];
                }
                ap.assert(srcidx >= 0, "MLPGradBatchX: internal error");
                if (datasettype == 0)
                {
                    for (i_ = 0; i_ <= rowsize - 1; i_++)
                    {
                        pbuf.xy[j, i_] = densexy[srcidx, i_];
                    }
                }
                if (datasettype == 1)
                {
                    sparse.sparsegetrow(sparsexy, srcidx, ref pbuf.xyrow, _params);
                    for (i_ = 0; i_ <= rowsize - 1; i_++)
                    {
                        pbuf.xy[j, i_] = pbuf.xyrow[i_];
                    }
                }
            }

            //
            // Process chunk and advance line pointer
            //
            mlpchunkedgradient(network, pbuf.xy, 0, csize, pbuf.batch4buf, pbuf.hpcbuf, ref sgrad.f, false, _params);
            cstart = cstart + pbuf.chunksize;
        }
        hpccores.hpcfinalizechunkedgradient(pbuf, sgrad.g, _params);
        smp.ae_shared_pool_recycle(buf, ref pbuf);
        smp.ae_shared_pool_recycle(gradbuf, ref sgrad);
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_mlpgradbatchx(multilayerperceptron network,
        double[,] densexy,
        sparse.sparsematrix sparsexy,
        int datasetsize,
        int datasettype,
        int[] idx,
        int subset0,
        int subset1,
        int subsettype,
        smp.shared_pool buf,
        smp.shared_pool gradbuf, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    Batch gradient calculation for a set of inputs/outputs
    (natural error function is used)

    INPUT PARAMETERS:
        Network -   network initialized with one of the network creation funcs
        XY      -   set of inputs/outputs; one sample = one row;
                    first NIn columns contain inputs,
                    next NOut columns - desired outputs.
        SSize   -   number of elements in XY
        Grad    -   possibly preallocated array. If size of array is smaller
                    than WCount, it will be reallocated. It is recommended to
                    reuse previously allocated array to reduce allocation
                    overhead.

    OUTPUT PARAMETERS:
        E       -   error function, sum-of-squares for regression networks,
                    cross-entropy for classification networks.
        Grad    -   gradient of E with respect to weights of network, array[WCount]

      -- ALGLIB --
         Copyright 04.11.2007 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpgradnbatch(multilayerperceptron network,
        double[,] xy,
        int ssize,
        ref double e,
        ref double[] grad,
        xparams _params)
    {
        int i = 0;
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        hpccores.mlpbuffers pbuf = null;

        e = 0;


        //
        // Alloc
        //
        mlpproperties(network, ref nin, ref nout, ref wcount, _params);
        smp.ae_shared_pool_retrieve(network.buf, ref pbuf);
        hpccores.hpcpreparechunkedgradient(network.weights, wcount, mlpntotal(network, _params), nin, nout, pbuf, _params);
        apserv.rvectorsetlengthatleast(ref grad, wcount, _params);
        for (i = 0; i <= wcount - 1; i++)
        {
            grad[i] = 0;
        }
        e = 0;
        i = 0;
        while (i <= ssize - 1)
        {
            mlpchunkedgradient(network, xy, i, Math.Min(ssize, i + pbuf.chunksize) - i, pbuf.batch4buf, pbuf.hpcbuf, ref e, true, _params);
            i = i + pbuf.chunksize;
        }
        hpccores.hpcfinalizechunkedgradient(pbuf, grad, _params);
        smp.ae_shared_pool_recycle(network.buf, ref pbuf);
    }


    /*************************************************************************
    Batch Hessian calculation (natural error function) using R-algorithm.
    Internal subroutine.

      -- ALGLIB --
         Copyright 26.01.2008 by Bochkanov Sergey.
         
         Hessian calculation based on R-algorithm described in
         "Fast Exact Multiplication by the Hessian",
         B. A. Pearlmutter,
         Neural Computation, 1994.
    *************************************************************************/
    public static void mlphessiannbatch(multilayerperceptron network,
        double[,] xy,
        int ssize,
        ref double e,
        ref double[] grad,
        ref double[,] h,
        xparams _params)
    {
        e = 0;

        mlphessianbatchinternal(network, xy, ssize, true, ref e, ref grad, ref h, _params);
    }


    /*************************************************************************
    Batch Hessian calculation using R-algorithm.
    Internal subroutine.

      -- ALGLIB --
         Copyright 26.01.2008 by Bochkanov Sergey.

         Hessian calculation based on R-algorithm described in
         "Fast Exact Multiplication by the Hessian",
         B. A. Pearlmutter,
         Neural Computation, 1994.
    *************************************************************************/
    public static void mlphessianbatch(multilayerperceptron network,
        double[,] xy,
        int ssize,
        ref double e,
        ref double[] grad,
        ref double[,] h,
        xparams _params)
    {
        e = 0;

        mlphessianbatchinternal(network, xy, ssize, false, ref e, ref grad, ref h, _params);
    }


    /*************************************************************************
    Internal subroutine, shouldn't be called by user.
    *************************************************************************/
    public static void mlpinternalprocessvector(int[] structinfo,
        double[] weights,
        double[] columnmeans,
        double[] columnsigmas,
        ref double[] neurons,
        ref double[] dfdnet,
        double[] x,
        ref double[] y,
        xparams _params)
    {
        int i = 0;
        int n1 = 0;
        int n2 = 0;
        int w1 = 0;
        int w2 = 0;
        int ntotal = 0;
        int nin = 0;
        int nout = 0;
        int istart = 0;
        int offs = 0;
        double net = 0;
        double f = 0;
        double df = 0;
        double d2f = 0;
        double mx = 0;
        bool perr = new bool();
        int i_ = 0;
        int i1_ = 0;


        //
        // Read network geometry
        //
        nin = structinfo[1];
        nout = structinfo[2];
        ntotal = structinfo[3];
        istart = structinfo[5];

        //
        // Inputs standartisation and putting in the network
        //
        for (i = 0; i <= nin - 1; i++)
        {
            if ((double)(columnsigmas[i]) != (double)(0))
            {
                neurons[i] = (x[i] - columnmeans[i]) / columnsigmas[i];
            }
            else
            {
                neurons[i] = x[i] - columnmeans[i];
            }
        }

        //
        // Process network
        //
        for (i = 0; i <= ntotal - 1; i++)
        {
            offs = istart + i * nfieldwidth;
            if (structinfo[offs + 0] > 0 || structinfo[offs + 0] == -5)
            {

                //
                // Activation function
                //
                mlpactivationfunction(neurons[structinfo[offs + 2]], structinfo[offs + 0], ref f, ref df, ref d2f, _params);
                neurons[i] = f;
                dfdnet[i] = df;
                continue;
            }
            if (structinfo[offs + 0] == 0)
            {

                //
                // Adaptive summator
                //
                n1 = structinfo[offs + 2];
                n2 = n1 + structinfo[offs + 1] - 1;
                w1 = structinfo[offs + 3];
                w2 = w1 + structinfo[offs + 1] - 1;
                i1_ = (n1) - (w1);
                net = 0.0;
                for (i_ = w1; i_ <= w2; i_++)
                {
                    net += weights[i_] * neurons[i_ + i1_];
                }
                neurons[i] = net;
                dfdnet[i] = 1.0;
                apserv.touchint(ref n2, _params);
                continue;
            }
            if (structinfo[offs + 0] < 0)
            {
                perr = true;
                if (structinfo[offs + 0] == -2)
                {

                    //
                    // input neuron, left unchanged
                    //
                    perr = false;
                }
                if (structinfo[offs + 0] == -3)
                {

                    //
                    // "-1" neuron
                    //
                    neurons[i] = -1;
                    perr = false;
                }
                if (structinfo[offs + 0] == -4)
                {

                    //
                    // "0" neuron
                    //
                    neurons[i] = 0;
                    perr = false;
                }
                ap.assert(!perr, "MLPInternalProcessVector: internal error - unknown neuron type!");
                continue;
            }
        }

        //
        // Extract result
        //
        i1_ = (ntotal - nout) - (0);
        for (i_ = 0; i_ <= nout - 1; i_++)
        {
            y[i_] = neurons[i_ + i1_];
        }

        //
        // Softmax post-processing or standardisation if needed
        //
        ap.assert(structinfo[6] == 0 || structinfo[6] == 1, "MLPInternalProcessVector: unknown normalization type!");
        if (structinfo[6] == 1)
        {

            //
            // Softmax
            //
            mx = y[0];
            for (i = 1; i <= nout - 1; i++)
            {
                mx = Math.Max(mx, y[i]);
            }
            net = 0;
            for (i = 0; i <= nout - 1; i++)
            {
                y[i] = Math.Exp(y[i] - mx);
                net = net + y[i];
            }
            for (i = 0; i <= nout - 1; i++)
            {
                y[i] = y[i] / net;
            }
        }
        else
        {

            //
            // Standardisation
            //
            for (i = 0; i <= nout - 1; i++)
            {
                y[i] = y[i] * columnsigmas[nin + i] + columnmeans[nin + i];
            }
        }
    }


    /*************************************************************************
    Serializer: allocation

      -- ALGLIB --
         Copyright 14.03.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpalloc(serializer s,
        multilayerperceptron network,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        int fkind = 0;
        double threshold = 0;
        double v0 = 0;
        double v1 = 0;
        int nin = 0;
        int nout = 0;
        int[] integerbuf = new int[0];

        nin = network.hllayersizes[0];
        nout = network.hllayersizes[ap.len(network.hllayersizes) - 1];
        s.alloc_entry();
        s.alloc_entry();
        s.alloc_entry();
        apserv.allocintegerarray(s, network.hllayersizes, -1, _params);
        for (i = 1; i <= ap.len(network.hllayersizes) - 1; i++)
        {
            for (j = 0; j <= network.hllayersizes[i] - 1; j++)
            {
                mlpgetneuroninfox(network, i, j, ref integerbuf, ref fkind, ref threshold, _params);
                s.alloc_entry();
                s.alloc_entry();
                for (k = 0; k <= network.hllayersizes[i - 1] - 1; k++)
                {
                    s.alloc_entry();
                }
            }
        }
        for (j = 0; j <= nin - 1; j++)
        {
            mlpgetinputscaling(network, j, ref v0, ref v1, _params);
            s.alloc_entry();
            s.alloc_entry();
        }
        for (j = 0; j <= nout - 1; j++)
        {
            mlpgetoutputscaling(network, j, ref v0, ref v1, _params);
            s.alloc_entry();
            s.alloc_entry();
        }
    }


    /*************************************************************************
    Serializer: serialization

      -- ALGLIB --
         Copyright 14.03.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpserialize(serializer s,
        multilayerperceptron network,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        int fkind = 0;
        double threshold = 0;
        double v0 = 0;
        double v1 = 0;
        int nin = 0;
        int nout = 0;
        int[] integerbuf = new int[0];

        nin = network.hllayersizes[0];
        nout = network.hllayersizes[ap.len(network.hllayersizes) - 1];
        s.serialize_int(scodes.getmlpserializationcode(_params));
        s.serialize_int(mlpfirstversion);
        s.serialize_bool(mlpissoftmax(network, _params));
        apserv.serializeintegerarray(s, network.hllayersizes, -1, _params);
        for (i = 1; i <= ap.len(network.hllayersizes) - 1; i++)
        {
            for (j = 0; j <= network.hllayersizes[i] - 1; j++)
            {
                mlpgetneuroninfox(network, i, j, ref integerbuf, ref fkind, ref threshold, _params);
                s.serialize_int(fkind);
                s.serialize_double(threshold);
                for (k = 0; k <= network.hllayersizes[i - 1] - 1; k++)
                {
                    s.serialize_double(mlpgetweightx(network, i - 1, k, i, j, ref integerbuf, _params));
                }
            }
        }
        for (j = 0; j <= nin - 1; j++)
        {
            mlpgetinputscaling(network, j, ref v0, ref v1, _params);
            s.serialize_double(v0);
            s.serialize_double(v1);
        }
        for (j = 0; j <= nout - 1; j++)
        {
            mlpgetoutputscaling(network, j, ref v0, ref v1, _params);
            s.serialize_double(v0);
            s.serialize_double(v1);
        }
    }


    /*************************************************************************
    Serializer: unserialization

      -- ALGLIB --
         Copyright 14.03.2011 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpunserialize(serializer s,
        multilayerperceptron network,
        xparams _params)
    {
        int i0 = 0;
        int i1 = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        int fkind = 0;
        double threshold = 0;
        double v0 = 0;
        double v1 = 0;
        int nin = 0;
        int nout = 0;
        bool issoftmax = new bool();
        int[] layersizes = new int[0];


        //
        // check correctness of header
        //
        i0 = s.unserialize_int();
        ap.assert(i0 == scodes.getmlpserializationcode(_params), "MLPUnserialize: stream header corrupted");
        i1 = s.unserialize_int();
        ap.assert(i1 == mlpfirstversion, "MLPUnserialize: stream header corrupted");

        //
        // Create network
        //
        issoftmax = s.unserialize_bool();
        apserv.unserializeintegerarray(s, ref layersizes, _params);
        ap.assert((ap.len(layersizes) == 2 || ap.len(layersizes) == 3) || ap.len(layersizes) == 4, "MLPUnserialize: too many hidden layers!");
        nin = layersizes[0];
        nout = layersizes[ap.len(layersizes) - 1];
        if (ap.len(layersizes) == 2)
        {
            if (issoftmax)
            {
                mlpcreatec0(layersizes[0], layersizes[1], network, _params);
            }
            else
            {
                mlpcreate0(layersizes[0], layersizes[1], network, _params);
            }
        }
        if (ap.len(layersizes) == 3)
        {
            if (issoftmax)
            {
                mlpcreatec1(layersizes[0], layersizes[1], layersizes[2], network, _params);
            }
            else
            {
                mlpcreate1(layersizes[0], layersizes[1], layersizes[2], network, _params);
            }
        }
        if (ap.len(layersizes) == 4)
        {
            if (issoftmax)
            {
                mlpcreatec2(layersizes[0], layersizes[1], layersizes[2], layersizes[3], network, _params);
            }
            else
            {
                mlpcreate2(layersizes[0], layersizes[1], layersizes[2], layersizes[3], network, _params);
            }
        }

        //
        // Load neurons and weights
        //
        for (i = 1; i <= ap.len(layersizes) - 1; i++)
        {
            for (j = 0; j <= layersizes[i] - 1; j++)
            {
                fkind = s.unserialize_int();
                threshold = s.unserialize_double();
                mlpsetneuroninfo(network, i, j, fkind, threshold, _params);
                for (k = 0; k <= layersizes[i - 1] - 1; k++)
                {
                    v0 = s.unserialize_double();
                    mlpsetweight(network, i - 1, k, i, j, v0, _params);
                }
            }
        }

        //
        // Load standartizator
        //
        for (j = 0; j <= nin - 1; j++)
        {
            v0 = s.unserialize_double();
            v1 = s.unserialize_double();
            mlpsetinputscaling(network, j, v0, v1, _params);
        }
        for (j = 0; j <= nout - 1; j++)
        {
            v0 = s.unserialize_double();
            v1 = s.unserialize_double();
            mlpsetoutputscaling(network, j, v0, v1, _params);
        }
    }


    /*************************************************************************
    Calculation of all types of errors on subset of dataset.

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
        Network -   network initialized with one of the network creation funcs
        XY      -   original dataset; one sample = one row;
                    first NIn columns contain inputs,
                    next NOut columns - desired outputs.
        SetSize -   real size of XY, SetSize>=0;
        Subset  -   subset of SubsetSize elements, array[SubsetSize];
        SubsetSize- number of elements in Subset[] array:
                    * if SubsetSize>0, rows of XY with indices Subset[0]...
                      ...Subset[SubsetSize-1] are processed
                    * if SubsetSize=0, zeros are returned
                    * if SubsetSize<0, entire dataset is  processed;  Subset[]
                      array is ignored in this case.

    OUTPUT PARAMETERS:
        Rep     -   it contains all type of errors.

      -- ALGLIB --
         Copyright 04.09.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpallerrorssubset(multilayerperceptron network,
        double[,] xy,
        int setsize,
        int[] subset,
        int subsetsize,
        modelerrors rep,
        xparams _params)
    {
        int idx0 = 0;
        int idx1 = 0;
        int idxtype = 0;

        ap.assert(ap.rows(xy) >= setsize, "MLPAllErrorsSubset: XY has less than SetSize rows");
        if (setsize > 0)
        {
            if (mlpissoftmax(network, _params))
            {
                ap.assert(ap.cols(xy) >= mlpgetinputscount(network, _params) + 1, "MLPAllErrorsSubset: XY has less than NIn+1 columns");
            }
            else
            {
                ap.assert(ap.cols(xy) >= mlpgetinputscount(network, _params) + mlpgetoutputscount(network, _params), "MLPAllErrorsSubset: XY has less than NIn+NOut columns");
            }
        }
        if (subsetsize >= 0)
        {
            idx0 = 0;
            idx1 = subsetsize;
            idxtype = 1;
        }
        else
        {
            idx0 = 0;
            idx1 = setsize;
            idxtype = 0;
        }
        mlpallerrorsx(network, xy, network.dummysxy, setsize, 0, subset, idx0, idx1, idxtype, network.buf, rep, _params);
    }


    /*************************************************************************
    Calculation of all types of errors on subset of dataset.

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
        Network -   network initialized with one of the network creation funcs
        XY      -   original dataset given by sparse matrix;
                    one sample = one row;
                    first NIn columns contain inputs,
                    next NOut columns - desired outputs.
        SetSize -   real size of XY, SetSize>=0;
        Subset  -   subset of SubsetSize elements, array[SubsetSize];
        SubsetSize- number of elements in Subset[] array:
                    * if SubsetSize>0, rows of XY with indices Subset[0]...
                      ...Subset[SubsetSize-1] are processed
                    * if SubsetSize=0, zeros are returned
                    * if SubsetSize<0, entire dataset is  processed;  Subset[]
                      array is ignored in this case.

    OUTPUT PARAMETERS:
        Rep     -   it contains all type of errors.


      -- ALGLIB --
         Copyright 04.09.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpallerrorssparsesubset(multilayerperceptron network,
        sparse.sparsematrix xy,
        int setsize,
        int[] subset,
        int subsetsize,
        modelerrors rep,
        xparams _params)
    {
        int idx0 = 0;
        int idx1 = 0;
        int idxtype = 0;

        ap.assert(sparse.sparseiscrs(xy, _params), "MLPAllErrorsSparseSubset: XY is not in CRS format.");
        ap.assert(sparse.sparsegetnrows(xy, _params) >= setsize, "MLPAllErrorsSparseSubset: XY has less than SetSize rows");
        if (setsize > 0)
        {
            if (mlpissoftmax(network, _params))
            {
                ap.assert(sparse.sparsegetncols(xy, _params) >= mlpgetinputscount(network, _params) + 1, "MLPAllErrorsSparseSubset: XY has less than NIn+1 columns");
            }
            else
            {
                ap.assert(sparse.sparsegetncols(xy, _params) >= mlpgetinputscount(network, _params) + mlpgetoutputscount(network, _params), "MLPAllErrorsSparseSubset: XY has less than NIn+NOut columns");
            }
        }
        if (subsetsize >= 0)
        {
            idx0 = 0;
            idx1 = subsetsize;
            idxtype = 1;
        }
        else
        {
            idx0 = 0;
            idx1 = setsize;
            idxtype = 0;
        }
        mlpallerrorsx(network, network.dummydxy, xy, setsize, 1, subset, idx0, idx1, idxtype, network.buf, rep, _params);
    }


    /*************************************************************************
    Error of the neural network on subset of dataset.

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
        Network   -     neural network;
        XY        -     training  set,  see  below  for  information  on   the
                        training set format;
        SetSize   -     real size of XY, SetSize>=0;
        Subset    -     subset of SubsetSize elements, array[SubsetSize];
        SubsetSize-     number of elements in Subset[] array:
                        * if SubsetSize>0, rows of XY with indices Subset[0]...
                          ...Subset[SubsetSize-1] are processed
                        * if SubsetSize=0, zeros are returned
                        * if SubsetSize<0, entire dataset is  processed;  Subset[]
                          array is ignored in this case.

    RESULT:
        sum-of-squares error, SUM(sqr(y[i]-desired_y[i])/2)

    DATASET FORMAT:

    This  function  uses  two  different  dataset formats - one for regression
    networks, another one for classification networks.

    For regression networks with NIn inputs and NOut outputs following dataset
    format is used:
    * dataset is given by NPoints*(NIn+NOut) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, next NOut columns are outputs

    For classification networks with NIn inputs and NClasses clases  following
    dataset format is used:
    * dataset is given by NPoints*(NIn+1) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, last column stores class number (from 0 to
      NClasses-1).

      -- ALGLIB --
         Copyright 04.09.2012 by Bochkanov Sergey
    *************************************************************************/
    public static double mlperrorsubset(multilayerperceptron network,
        double[,] xy,
        int setsize,
        int[] subset,
        int subsetsize,
        xparams _params)
    {
        double result = 0;
        int idx0 = 0;
        int idx1 = 0;
        int idxtype = 0;

        ap.assert(ap.rows(xy) >= setsize, "MLPErrorSubset: XY has less than SetSize rows");
        if (setsize > 0)
        {
            if (mlpissoftmax(network, _params))
            {
                ap.assert(ap.cols(xy) >= mlpgetinputscount(network, _params) + 1, "MLPErrorSubset: XY has less than NIn+1 columns");
            }
            else
            {
                ap.assert(ap.cols(xy) >= mlpgetinputscount(network, _params) + mlpgetoutputscount(network, _params), "MLPErrorSubset: XY has less than NIn+NOut columns");
            }
        }
        if (subsetsize >= 0)
        {
            idx0 = 0;
            idx1 = subsetsize;
            idxtype = 1;
        }
        else
        {
            idx0 = 0;
            idx1 = setsize;
            idxtype = 0;
        }
        mlpallerrorsx(network, xy, network.dummysxy, setsize, 0, subset, idx0, idx1, idxtype, network.buf, network.err, _params);
        result = math.sqr(network.err.rmserror) * (idx1 - idx0) * mlpgetoutputscount(network, _params) / 2;
        return result;
    }


    /*************************************************************************
    Error of the neural network on subset of sparse dataset.

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
        Network   -     neural network;
        XY        -     training  set,  see  below  for  information  on   the
                        training set format. This function checks  correctness
                        of  the  dataset  (no  NANs/INFs,  class  numbers  are
                        correct) and throws exception when  incorrect  dataset
                        is passed.  Sparse  matrix  must  use  CRS  format for
                        storage.
        SetSize   -     real size of XY, SetSize>=0;
                        it is used when SubsetSize<0;
        Subset    -     subset of SubsetSize elements, array[SubsetSize];
        SubsetSize-     number of elements in Subset[] array:
                        * if SubsetSize>0, rows of XY with indices Subset[0]...
                          ...Subset[SubsetSize-1] are processed
                        * if SubsetSize=0, zeros are returned
                        * if SubsetSize<0, entire dataset is  processed;  Subset[]
                          array is ignored in this case.

    RESULT:
        sum-of-squares error, SUM(sqr(y[i]-desired_y[i])/2)

    DATASET FORMAT:

    This  function  uses  two  different  dataset formats - one for regression
    networks, another one for classification networks.

    For regression networks with NIn inputs and NOut outputs following dataset
    format is used:
    * dataset is given by NPoints*(NIn+NOut) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, next NOut columns are outputs

    For classification networks with NIn inputs and NClasses clases  following
    dataset format is used:
    * dataset is given by NPoints*(NIn+1) matrix
    * each row corresponds to one example
    * first NIn columns are inputs, last column stores class number (from 0 to
      NClasses-1).

      -- ALGLIB --
         Copyright 04.09.2012 by Bochkanov Sergey
    *************************************************************************/
    public static double mlperrorsparsesubset(multilayerperceptron network,
        sparse.sparsematrix xy,
        int setsize,
        int[] subset,
        int subsetsize,
        xparams _params)
    {
        double result = 0;
        int idx0 = 0;
        int idx1 = 0;
        int idxtype = 0;

        ap.assert(sparse.sparseiscrs(xy, _params), "MLPErrorSparseSubset: XY is not in CRS format.");
        ap.assert(sparse.sparsegetnrows(xy, _params) >= setsize, "MLPErrorSparseSubset: XY has less than SetSize rows");
        if (setsize > 0)
        {
            if (mlpissoftmax(network, _params))
            {
                ap.assert(sparse.sparsegetncols(xy, _params) >= mlpgetinputscount(network, _params) + 1, "MLPErrorSparseSubset: XY has less than NIn+1 columns");
            }
            else
            {
                ap.assert(sparse.sparsegetncols(xy, _params) >= mlpgetinputscount(network, _params) + mlpgetoutputscount(network, _params), "MLPErrorSparseSubset: XY has less than NIn+NOut columns");
            }
        }
        if (subsetsize >= 0)
        {
            idx0 = 0;
            idx1 = subsetsize;
            idxtype = 1;
        }
        else
        {
            idx0 = 0;
            idx1 = setsize;
            idxtype = 0;
        }
        mlpallerrorsx(network, network.dummydxy, xy, setsize, 1, subset, idx0, idx1, idxtype, network.buf, network.err, _params);
        result = math.sqr(network.err.rmserror) * (idx1 - idx0) * mlpgetoutputscount(network, _params) / 2;
        return result;
    }


    /*************************************************************************
    Calculation of all types of errors at once for a subset or  full  dataset,
    which can be represented in different formats.

    THIS INTERNAL FUNCTION IS NOT INTENDED TO BE USED BY ALGLIB USERS!

      -- ALGLIB --
         Copyright 26.07.2012 by Bochkanov Sergey
    *************************************************************************/
    public static void mlpallerrorsx(multilayerperceptron network,
        double[,] densexy,
        sparse.sparsematrix sparsexy,
        int datasetsize,
        int datasettype,
        int[] idx,
        int subset0,
        int subset1,
        int subsettype,
        smp.shared_pool buf,
        modelerrors rep,
        xparams _params)
    {
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        int rowsize = 0;
        bool iscls = new bool();
        int srcidx = 0;
        int cstart = 0;
        int csize = 0;
        int j = 0;
        hpccores.mlpbuffers pbuf = null;
        int len0 = 0;
        int len1 = 0;
        modelerrors rep0 = new modelerrors();
        modelerrors rep1 = new modelerrors();
        double problemcost = 0;
        int i_ = 0;
        int i1_ = 0;

        ap.assert(datasetsize >= 0, "MLPAllErrorsX: SetSize<0");
        ap.assert(datasettype == 0 || datasettype == 1, "MLPAllErrorsX: DatasetType is incorrect");
        ap.assert(subsettype == 0 || subsettype == 1, "MLPAllErrorsX: SubsetType is incorrect");

        //
        // Determine network properties
        //
        mlpproperties(network, ref nin, ref nout, ref wcount, _params);
        iscls = mlpissoftmax(network, _params);

        //
        // Split problem.
        //
        // Splitting problem allows us to reduce  effect  of  single-precision
        // arithmetics (SSE-optimized version of MLPChunkedProcess uses single
        // precision  internally, but converts them to  double precision after
        // results are exported from HPC buffer to network). Small batches are
        // calculated in single precision, results are  aggregated  in  double
        // precision, and it allows us to avoid accumulation  of  errors  when
        // we process very large batches (tens of thousands of items).
        //
        // NOTE: it is important to use real arithmetics for ProblemCost
        //       because ProblemCost may be larger than MAXINT.
        //
        problemcost = subset1 - subset0;
        problemcost = problemcost * wcount * 2;
        if ((double)(problemcost) >= (double)(apserv.smpactivationlevel(_params)) && subset1 - subset0 >= 2 * microbatchsize)
        {
            if (_trypexec_mlpallerrorsx(network, densexy, sparsexy, datasetsize, datasettype, idx, subset0, subset1, subsettype, buf, rep, _params))
            {
                return;
            }
        }
        if (subset1 - subset0 >= 2 * microbatchsize && (double)(problemcost) > (double)(apserv.spawnlevel(_params)))
        {
            apserv.splitlength(subset1 - subset0, microbatchsize, ref len0, ref len1, _params);
            mlpallerrorsx(network, densexy, sparsexy, datasetsize, datasettype, idx, subset0, subset0 + len0, subsettype, buf, rep0, _params);
            mlpallerrorsx(network, densexy, sparsexy, datasetsize, datasettype, idx, subset0 + len0, subset1, subsettype, buf, rep1, _params);
            rep.relclserror = (len0 * rep0.relclserror + len1 * rep1.relclserror) / (len0 + len1);
            rep.avgce = (len0 * rep0.avgce + len1 * rep1.avgce) / (len0 + len1);
            rep.rmserror = Math.Sqrt((len0 * math.sqr(rep0.rmserror) + len1 * math.sqr(rep1.rmserror)) / (len0 + len1));
            rep.avgerror = (len0 * rep0.avgerror + len1 * rep1.avgerror) / (len0 + len1);
            rep.avgrelerror = (len0 * rep0.avgrelerror + len1 * rep1.avgrelerror) / (len0 + len1);
            return;
        }

        //
        // Retrieve and prepare
        //
        smp.ae_shared_pool_retrieve(buf, ref pbuf);
        if (iscls)
        {
            rowsize = nin + 1;
            bdss.dserrallocate(nout, ref pbuf.tmp0, _params);
        }
        else
        {
            rowsize = nin + nout;
            bdss.dserrallocate(-nout, ref pbuf.tmp0, _params);
        }

        //
        // Processing
        //
        hpccores.hpcpreparechunkedgradient(network.weights, wcount, mlpntotal(network, _params), nin, nout, pbuf, _params);
        cstart = subset0;
        while (cstart < subset1)
        {

            //
            // Determine size of current chunk and copy it to PBuf.XY
            //
            csize = Math.Min(subset1, cstart + pbuf.chunksize) - cstart;
            for (j = 0; j <= csize - 1; j++)
            {
                srcidx = -1;
                if (subsettype == 0)
                {
                    srcidx = cstart + j;
                }
                if (subsettype == 1)
                {
                    srcidx = idx[cstart + j];
                }
                ap.assert(srcidx >= 0, "MLPAllErrorsX: internal error");
                if (datasettype == 0)
                {
                    for (i_ = 0; i_ <= rowsize - 1; i_++)
                    {
                        pbuf.xy[j, i_] = densexy[srcidx, i_];
                    }
                }
                if (datasettype == 1)
                {
                    sparse.sparsegetrow(sparsexy, srcidx, ref pbuf.xyrow, _params);
                    for (i_ = 0; i_ <= rowsize - 1; i_++)
                    {
                        pbuf.xy[j, i_] = pbuf.xyrow[i_];
                    }
                }
            }

            //
            // Unpack XY and process (temporary code, to be replaced by chunked processing)
            //
            for (j = 0; j <= csize - 1; j++)
            {
                for (i_ = 0; i_ <= rowsize - 1; i_++)
                {
                    pbuf.xy2[j, i_] = pbuf.xy[j, i_];
                }
            }
            mlpchunkedprocess(network, pbuf.xy2, 0, csize, pbuf.batch4buf, pbuf.hpcbuf, _params);
            for (j = 0; j <= csize - 1; j++)
            {
                for (i_ = 0; i_ <= nin - 1; i_++)
                {
                    pbuf.x[i_] = pbuf.xy2[j, i_];
                }
                i1_ = (nin) - (0);
                for (i_ = 0; i_ <= nout - 1; i_++)
                {
                    pbuf.y[i_] = pbuf.xy2[j, i_ + i1_];
                }
                if (iscls)
                {
                    pbuf.desiredy[0] = pbuf.xy[j, nin];
                }
                else
                {
                    i1_ = (nin) - (0);
                    for (i_ = 0; i_ <= nout - 1; i_++)
                    {
                        pbuf.desiredy[i_] = pbuf.xy[j, i_ + i1_];
                    }
                }
                bdss.dserraccumulate(ref pbuf.tmp0, pbuf.y, pbuf.desiredy, _params);
            }

            //
            // Process chunk and advance line pointer
            //
            cstart = cstart + pbuf.chunksize;
        }
        bdss.dserrfinish(ref pbuf.tmp0, _params);
        rep.relclserror = pbuf.tmp0[0];
        rep.avgce = pbuf.tmp0[1] / Math.Log(2);
        rep.rmserror = pbuf.tmp0[2];
        rep.avgerror = pbuf.tmp0[3];
        rep.avgrelerror = pbuf.tmp0[4];

        //
        // Recycle
        //
        smp.ae_shared_pool_recycle(buf, ref pbuf);
    }


    /*************************************************************************
    Serial stub for GPL edition.
    *************************************************************************/
    public static bool _trypexec_mlpallerrorsx(multilayerperceptron network,
        double[,] densexy,
        sparse.sparsematrix sparsexy,
        int datasetsize,
        int datasettype,
        int[] idx,
        int subset0,
        int subset1,
        int subsettype,
        smp.shared_pool buf,
        modelerrors rep, xparams _params)
    {
        return false;
    }


    /*************************************************************************
    Internal subroutine: adding new input layer to network
    *************************************************************************/
    private static void addinputlayer(int ncount,
        ref int[] lsizes,
        ref int[] ltypes,
        ref int[] lconnfirst,
        ref int[] lconnlast,
        ref int lastproc,
        xparams _params)
    {
        lsizes[0] = ncount;
        ltypes[0] = -2;
        lconnfirst[0] = 0;
        lconnlast[0] = 0;
        lastproc = 0;
    }


    /*************************************************************************
    Internal subroutine: adding new summator layer to network
    *************************************************************************/
    private static void addbiasedsummatorlayer(int ncount,
        ref int[] lsizes,
        ref int[] ltypes,
        ref int[] lconnfirst,
        ref int[] lconnlast,
        ref int lastproc,
        xparams _params)
    {
        lsizes[lastproc + 1] = 1;
        ltypes[lastproc + 1] = -3;
        lconnfirst[lastproc + 1] = 0;
        lconnlast[lastproc + 1] = 0;
        lsizes[lastproc + 2] = ncount;
        ltypes[lastproc + 2] = 0;
        lconnfirst[lastproc + 2] = lastproc;
        lconnlast[lastproc + 2] = lastproc + 1;
        lastproc = lastproc + 2;
    }


    /*************************************************************************
    Internal subroutine: adding new summator layer to network
    *************************************************************************/
    private static void addactivationlayer(int functype,
        ref int[] lsizes,
        ref int[] ltypes,
        ref int[] lconnfirst,
        ref int[] lconnlast,
        ref int lastproc,
        xparams _params)
    {
        ap.assert(functype > 0 || functype == -5, "AddActivationLayer: incorrect function type");
        lsizes[lastproc + 1] = lsizes[lastproc];
        ltypes[lastproc + 1] = functype;
        lconnfirst[lastproc + 1] = lastproc;
        lconnlast[lastproc + 1] = lastproc;
        lastproc = lastproc + 1;
    }


    /*************************************************************************
    Internal subroutine: adding new zero layer to network
    *************************************************************************/
    private static void addzerolayer(ref int[] lsizes,
        ref int[] ltypes,
        ref int[] lconnfirst,
        ref int[] lconnlast,
        ref int lastproc,
        xparams _params)
    {
        lsizes[lastproc + 1] = 1;
        ltypes[lastproc + 1] = -4;
        lconnfirst[lastproc + 1] = 0;
        lconnlast[lastproc + 1] = 0;
        lastproc = lastproc + 1;
    }


    /*************************************************************************
    This routine adds input layer to the high-level description of the network.

    It modifies Network.HLConnections and Network.HLNeurons  and  assumes that
    these  arrays  have  enough  place  to  store  data.  It accepts following
    parameters:
        Network     -   network
        ConnIdx     -   index of the first free entry in the HLConnections
        NeuroIdx    -   index of the first free entry in the HLNeurons
        StructInfoIdx-  index of the first entry in the low level description
                        of the current layer (in the StructInfo array)
        NIn         -   number of inputs
                        
    It modified Network and indices.
    *************************************************************************/
    private static void hladdinputlayer(multilayerperceptron network,
        ref int connidx,
        ref int neuroidx,
        ref int structinfoidx,
        int nin,
        xparams _params)
    {
        int i = 0;
        int offs = 0;

        offs = hlnfieldwidth * neuroidx;
        for (i = 0; i <= nin - 1; i++)
        {
            network.hlneurons[offs + 0] = 0;
            network.hlneurons[offs + 1] = i;
            network.hlneurons[offs + 2] = -1;
            network.hlneurons[offs + 3] = -1;
            offs = offs + hlnfieldwidth;
        }
        neuroidx = neuroidx + nin;
        structinfoidx = structinfoidx + nin;
    }


    /*************************************************************************
    This routine adds output layer to the high-level description of
    the network.

    It modifies Network.HLConnections and Network.HLNeurons  and  assumes that
    these  arrays  have  enough  place  to  store  data.  It accepts following
    parameters:
        Network     -   network
        ConnIdx     -   index of the first free entry in the HLConnections
        NeuroIdx    -   index of the first free entry in the HLNeurons
        StructInfoIdx-  index of the first entry in the low level description
                        of the current layer (in the StructInfo array)
        WeightsIdx  -   index of the first entry in the Weights array which
                        corresponds to the current layer
        K           -   current layer index
        NPrev       -   number of neurons in the previous layer
        NOut        -   number of outputs
        IsCls       -   is it classifier network?
        IsLinear    -   is it network with linear output?

    It modified Network and ConnIdx/NeuroIdx/StructInfoIdx/WeightsIdx.
    *************************************************************************/
    private static void hladdoutputlayer(multilayerperceptron network,
        ref int connidx,
        ref int neuroidx,
        ref int structinfoidx,
        ref int weightsidx,
        int k,
        int nprev,
        int nout,
        bool iscls,
        bool islinearout,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int neurooffs = 0;
        int connoffs = 0;

        ap.assert((iscls && islinearout) || !iscls, "HLAddOutputLayer: internal error");
        neurooffs = hlnfieldwidth * neuroidx;
        connoffs = hlconnfieldwidth * connidx;
        if (!iscls)
        {

            //
            // Regression network
            //
            for (i = 0; i <= nout - 1; i++)
            {
                network.hlneurons[neurooffs + 0] = k;
                network.hlneurons[neurooffs + 1] = i;
                network.hlneurons[neurooffs + 2] = structinfoidx + 1 + nout + i;
                network.hlneurons[neurooffs + 3] = weightsidx + nprev + (nprev + 1) * i;
                neurooffs = neurooffs + hlnfieldwidth;
            }
            for (i = 0; i <= nprev - 1; i++)
            {
                for (j = 0; j <= nout - 1; j++)
                {
                    network.hlconnections[connoffs + 0] = k - 1;
                    network.hlconnections[connoffs + 1] = i;
                    network.hlconnections[connoffs + 2] = k;
                    network.hlconnections[connoffs + 3] = j;
                    network.hlconnections[connoffs + 4] = weightsidx + i + j * (nprev + 1);
                    connoffs = connoffs + hlconnfieldwidth;
                }
            }
            connidx = connidx + nprev * nout;
            neuroidx = neuroidx + nout;
            structinfoidx = structinfoidx + 2 * nout + 1;
            weightsidx = weightsidx + nout * (nprev + 1);
        }
        else
        {

            //
            // Classification network
            //
            for (i = 0; i <= nout - 2; i++)
            {
                network.hlneurons[neurooffs + 0] = k;
                network.hlneurons[neurooffs + 1] = i;
                network.hlneurons[neurooffs + 2] = -1;
                network.hlneurons[neurooffs + 3] = weightsidx + nprev + (nprev + 1) * i;
                neurooffs = neurooffs + hlnfieldwidth;
            }
            network.hlneurons[neurooffs + 0] = k;
            network.hlneurons[neurooffs + 1] = i;
            network.hlneurons[neurooffs + 2] = -1;
            network.hlneurons[neurooffs + 3] = -1;
            for (i = 0; i <= nprev - 1; i++)
            {
                for (j = 0; j <= nout - 2; j++)
                {
                    network.hlconnections[connoffs + 0] = k - 1;
                    network.hlconnections[connoffs + 1] = i;
                    network.hlconnections[connoffs + 2] = k;
                    network.hlconnections[connoffs + 3] = j;
                    network.hlconnections[connoffs + 4] = weightsidx + i + j * (nprev + 1);
                    connoffs = connoffs + hlconnfieldwidth;
                }
            }
            connidx = connidx + nprev * (nout - 1);
            neuroidx = neuroidx + nout;
            structinfoidx = structinfoidx + nout + 2;
            weightsidx = weightsidx + (nout - 1) * (nprev + 1);
        }
    }


    /*************************************************************************
    This routine adds hidden layer to the high-level description of
    the network.

    It modifies Network.HLConnections and Network.HLNeurons  and  assumes that
    these  arrays  have  enough  place  to  store  data.  It accepts following
    parameters:
        Network     -   network
        ConnIdx     -   index of the first free entry in the HLConnections
        NeuroIdx    -   index of the first free entry in the HLNeurons
        StructInfoIdx-  index of the first entry in the low level description
                        of the current layer (in the StructInfo array)
        WeightsIdx  -   index of the first entry in the Weights array which
                        corresponds to the current layer
        K           -   current layer index
        NPrev       -   number of neurons in the previous layer
        NCur        -   number of neurons in the current layer

    It modified Network and ConnIdx/NeuroIdx/StructInfoIdx/WeightsIdx.
    *************************************************************************/
    private static void hladdhiddenlayer(multilayerperceptron network,
        ref int connidx,
        ref int neuroidx,
        ref int structinfoidx,
        ref int weightsidx,
        int k,
        int nprev,
        int ncur,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int neurooffs = 0;
        int connoffs = 0;

        neurooffs = hlnfieldwidth * neuroidx;
        connoffs = hlconnfieldwidth * connidx;
        for (i = 0; i <= ncur - 1; i++)
        {
            network.hlneurons[neurooffs + 0] = k;
            network.hlneurons[neurooffs + 1] = i;
            network.hlneurons[neurooffs + 2] = structinfoidx + 1 + ncur + i;
            network.hlneurons[neurooffs + 3] = weightsidx + nprev + (nprev + 1) * i;
            neurooffs = neurooffs + hlnfieldwidth;
        }
        for (i = 0; i <= nprev - 1; i++)
        {
            for (j = 0; j <= ncur - 1; j++)
            {
                network.hlconnections[connoffs + 0] = k - 1;
                network.hlconnections[connoffs + 1] = i;
                network.hlconnections[connoffs + 2] = k;
                network.hlconnections[connoffs + 3] = j;
                network.hlconnections[connoffs + 4] = weightsidx + i + j * (nprev + 1);
                connoffs = connoffs + hlconnfieldwidth;
            }
        }
        connidx = connidx + nprev * ncur;
        neuroidx = neuroidx + ncur;
        structinfoidx = structinfoidx + 2 * ncur + 1;
        weightsidx = weightsidx + ncur * (nprev + 1);
    }


    /*************************************************************************
    This function fills high level information about network created using
    internal MLPCreate() function.

    This function does NOT examine StructInfo for low level information, it
    just expects that network has following structure:

        input neuron            \
        ...                      | input layer
        input neuron            /
        
        "-1" neuron             \
        biased summator          |
        ...                      |
        biased summator          | hidden layer(s), if there are exists any
        activation function      |
        ...                      |
        activation function     /
        
        "-1" neuron            \
        biased summator         | output layer:
        ...                     |
        biased summator         | * we have NOut summators/activators for regression networks
        activation function     | * we have only NOut-1 summators and no activators for classifiers
        ...                     | * we have "0" neuron only when we have classifier
        activation function     |
        "0" neuron              /


      -- ALGLIB --
         Copyright 30.03.2008 by Bochkanov Sergey
    *************************************************************************/
    private static void fillhighlevelinformation(multilayerperceptron network,
        int nin,
        int nhid1,
        int nhid2,
        int nout,
        bool iscls,
        bool islinearout,
        xparams _params)
    {
        int idxweights = 0;
        int idxstruct = 0;
        int idxneuro = 0;
        int idxconn = 0;

        ap.assert((iscls && islinearout) || !iscls, "FillHighLevelInformation: internal error");

        //
        // Preparations common to all types of networks
        //
        idxweights = 0;
        idxneuro = 0;
        idxstruct = 0;
        idxconn = 0;
        network.hlnetworktype = 0;

        //
        // network without hidden layers
        //
        if (nhid1 == 0)
        {
            network.hllayersizes = new int[2];
            network.hllayersizes[0] = nin;
            network.hllayersizes[1] = nout;
            if (!iscls)
            {
                network.hlconnections = new int[hlconnfieldwidth * nin * nout];
                network.hlneurons = new int[hlnfieldwidth * (nin + nout)];
                network.hlnormtype = 0;
            }
            else
            {
                network.hlconnections = new int[hlconnfieldwidth * nin * (nout - 1)];
                network.hlneurons = new int[hlnfieldwidth * (nin + nout)];
                network.hlnormtype = 1;
            }
            hladdinputlayer(network, ref idxconn, ref idxneuro, ref idxstruct, nin, _params);
            hladdoutputlayer(network, ref idxconn, ref idxneuro, ref idxstruct, ref idxweights, 1, nin, nout, iscls, islinearout, _params);
            return;
        }

        //
        // network with one hidden layers
        //
        if (nhid2 == 0)
        {
            network.hllayersizes = new int[3];
            network.hllayersizes[0] = nin;
            network.hllayersizes[1] = nhid1;
            network.hllayersizes[2] = nout;
            if (!iscls)
            {
                network.hlconnections = new int[hlconnfieldwidth * (nin * nhid1 + nhid1 * nout)];
                network.hlneurons = new int[hlnfieldwidth * (nin + nhid1 + nout)];
                network.hlnormtype = 0;
            }
            else
            {
                network.hlconnections = new int[hlconnfieldwidth * (nin * nhid1 + nhid1 * (nout - 1))];
                network.hlneurons = new int[hlnfieldwidth * (nin + nhid1 + nout)];
                network.hlnormtype = 1;
            }
            hladdinputlayer(network, ref idxconn, ref idxneuro, ref idxstruct, nin, _params);
            hladdhiddenlayer(network, ref idxconn, ref idxneuro, ref idxstruct, ref idxweights, 1, nin, nhid1, _params);
            hladdoutputlayer(network, ref idxconn, ref idxneuro, ref idxstruct, ref idxweights, 2, nhid1, nout, iscls, islinearout, _params);
            return;
        }

        //
        // Two hidden layers
        //
        network.hllayersizes = new int[4];
        network.hllayersizes[0] = nin;
        network.hllayersizes[1] = nhid1;
        network.hllayersizes[2] = nhid2;
        network.hllayersizes[3] = nout;
        if (!iscls)
        {
            network.hlconnections = new int[hlconnfieldwidth * (nin * nhid1 + nhid1 * nhid2 + nhid2 * nout)];
            network.hlneurons = new int[hlnfieldwidth * (nin + nhid1 + nhid2 + nout)];
            network.hlnormtype = 0;
        }
        else
        {
            network.hlconnections = new int[hlconnfieldwidth * (nin * nhid1 + nhid1 * nhid2 + nhid2 * (nout - 1))];
            network.hlneurons = new int[hlnfieldwidth * (nin + nhid1 + nhid2 + nout)];
            network.hlnormtype = 1;
        }
        hladdinputlayer(network, ref idxconn, ref idxneuro, ref idxstruct, nin, _params);
        hladdhiddenlayer(network, ref idxconn, ref idxneuro, ref idxstruct, ref idxweights, 1, nin, nhid1, _params);
        hladdhiddenlayer(network, ref idxconn, ref idxneuro, ref idxstruct, ref idxweights, 2, nhid1, nhid2, _params);
        hladdoutputlayer(network, ref idxconn, ref idxneuro, ref idxstruct, ref idxweights, 3, nhid2, nout, iscls, islinearout, _params);
    }


    /*************************************************************************
    Internal subroutine.

      -- ALGLIB --
         Copyright 04.11.2007 by Bochkanov Sergey
    *************************************************************************/
    private static void mlpcreate(int nin,
        int nout,
        int[] lsizes,
        int[] ltypes,
        int[] lconnfirst,
        int[] lconnlast,
        int layerscount,
        bool isclsnet,
        multilayerperceptron network,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int ssize = 0;
        int ntotal = 0;
        int wcount = 0;
        int offs = 0;
        int nprocessed = 0;
        int wallocated = 0;
        int[] localtemp = new int[0];
        int[] lnfirst = new int[0];
        int[] lnsyn = new int[0];
        hpccores.mlpbuffers buf = new hpccores.mlpbuffers();
        smlpgrad sgrad = new smlpgrad();


        //
        // Check
        //
        ap.assert(layerscount > 0, "MLPCreate: wrong parameters!");
        ap.assert(ltypes[0] == -2, "MLPCreate: wrong LTypes[0] (must be -2)!");
        for (i = 0; i <= layerscount - 1; i++)
        {
            ap.assert(lsizes[i] > 0, "MLPCreate: wrong LSizes!");
            ap.assert(lconnfirst[i] >= 0 && (lconnfirst[i] < i || i == 0), "MLPCreate: wrong LConnFirst!");
            ap.assert(lconnlast[i] >= lconnfirst[i] && (lconnlast[i] < i || i == 0), "MLPCreate: wrong LConnLast!");
        }

        //
        // Build network geometry
        //
        lnfirst = new int[layerscount - 1 + 1];
        lnsyn = new int[layerscount - 1 + 1];
        ntotal = 0;
        wcount = 0;
        for (i = 0; i <= layerscount - 1; i++)
        {

            //
            // Analyze connections.
            // This code must throw an assertion in case of unknown LTypes[I]
            //
            lnsyn[i] = -1;
            if (ltypes[i] >= 0 || ltypes[i] == -5)
            {
                lnsyn[i] = 0;
                for (j = lconnfirst[i]; j <= lconnlast[i]; j++)
                {
                    lnsyn[i] = lnsyn[i] + lsizes[j];
                }
            }
            else
            {
                if ((ltypes[i] == -2 || ltypes[i] == -3) || ltypes[i] == -4)
                {
                    lnsyn[i] = 0;
                }
            }
            ap.assert(lnsyn[i] >= 0, "MLPCreate: internal error #0!");

            //
            // Other info
            //
            lnfirst[i] = ntotal;
            ntotal = ntotal + lsizes[i];
            if (ltypes[i] == 0)
            {
                wcount = wcount + lnsyn[i] * lsizes[i];
            }
        }
        ssize = 7 + ntotal * nfieldwidth;

        //
        // Allocate
        //
        network.structinfo = new int[ssize - 1 + 1];
        network.weights = new double[wcount - 1 + 1];
        if (isclsnet)
        {
            network.columnmeans = new double[nin - 1 + 1];
            network.columnsigmas = new double[nin - 1 + 1];
        }
        else
        {
            network.columnmeans = new double[nin + nout - 1 + 1];
            network.columnsigmas = new double[nin + nout - 1 + 1];
        }
        network.neurons = new double[ntotal - 1 + 1];
        network.nwbuf = new double[Math.Max(wcount, 2 * nout) - 1 + 1];
        network.integerbuf = new int[3 + 1];
        network.dfdnet = new double[ntotal - 1 + 1];
        network.x = new double[nin - 1 + 1];
        network.y = new double[nout - 1 + 1];
        network.derror = new double[ntotal - 1 + 1];

        //
        // Fill structure:
        // * first, fill by dummy values to avoid spurious reports by Valgrind
        // * then fill global info header
        //
        for (i = 0; i <= ssize - 1; i++)
        {
            network.structinfo[i] = -999999;
        }
        network.structinfo[0] = ssize;
        network.structinfo[1] = nin;
        network.structinfo[2] = nout;
        network.structinfo[3] = ntotal;
        network.structinfo[4] = wcount;
        network.structinfo[5] = 7;
        if (isclsnet)
        {
            network.structinfo[6] = 1;
        }
        else
        {
            network.structinfo[6] = 0;
        }

        //
        // Fill structure: neuron connections
        //
        nprocessed = 0;
        wallocated = 0;
        for (i = 0; i <= layerscount - 1; i++)
        {
            for (j = 0; j <= lsizes[i] - 1; j++)
            {
                offs = network.structinfo[5] + nprocessed * nfieldwidth;
                network.structinfo[offs + 0] = ltypes[i];
                if (ltypes[i] == 0)
                {

                    //
                    // Adaptive summator:
                    // * connections with weights to previous neurons
                    //
                    network.structinfo[offs + 1] = lnsyn[i];
                    network.structinfo[offs + 2] = lnfirst[lconnfirst[i]];
                    network.structinfo[offs + 3] = wallocated;
                    wallocated = wallocated + lnsyn[i];
                    nprocessed = nprocessed + 1;
                }
                if (ltypes[i] > 0 || ltypes[i] == -5)
                {

                    //
                    // Activation layer:
                    // * each neuron connected to one (only one) of previous neurons.
                    // * no weights
                    //
                    network.structinfo[offs + 1] = 1;
                    network.structinfo[offs + 2] = lnfirst[lconnfirst[i]] + j;
                    network.structinfo[offs + 3] = -1;
                    nprocessed = nprocessed + 1;
                }
                if ((ltypes[i] == -2 || ltypes[i] == -3) || ltypes[i] == -4)
                {
                    nprocessed = nprocessed + 1;
                }
            }
        }
        ap.assert(wallocated == wcount, "MLPCreate: internal error #1!");
        ap.assert(nprocessed == ntotal, "MLPCreate: internal error #2!");

        //
        // Fill weights by small random values
        // Initialize means and sigmas
        //
        for (i = 0; i <= nin - 1; i++)
        {
            network.columnmeans[i] = 0;
            network.columnsigmas[i] = 1;
        }
        if (!isclsnet)
        {
            for (i = 0; i <= nout - 1; i++)
            {
                network.columnmeans[nin + i] = 0;
                network.columnsigmas[nin + i] = 1;
            }
        }
        mlprandomize(network, _params);

        //
        // Seed buffers
        //
        smp.ae_shared_pool_set_seed(network.buf, buf);
        sgrad.g = new double[wcount];
        sgrad.f = 0.0;
        for (i = 0; i <= wcount - 1; i++)
        {
            sgrad.g[i] = 0.0;
        }
        smp.ae_shared_pool_set_seed(network.gradbuf, sgrad);
    }


    /*************************************************************************
    This function returns information about Ith neuron of Kth layer.

      -- ALGLIB --
         Copyright 25.03.2011 by Bochkanov Sergey
    *************************************************************************/
    private static void mlpgetneuroninfox(multilayerperceptron network,
        int k,
        int i,
        ref int[] integerbuf,
        ref int fkind,
        ref double threshold,
        xparams _params)
    {
        int ncnt = 0;
        int istart = 0;
        int highlevelidx = 0;
        int activationoffset = 0;

        fkind = 0;
        threshold = 0;

        ablasf.iallocv(2, ref integerbuf, _params);
        ncnt = ap.len(network.hlneurons) / hlnfieldwidth;
        istart = network.structinfo[5];

        //
        // search
        //
        integerbuf[0] = k;
        integerbuf[1] = i;
        highlevelidx = apserv.recsearch(network.hlneurons, hlnfieldwidth, 2, 0, ncnt, integerbuf, _params);
        ap.assert(highlevelidx >= 0, "MLPGetNeuronInfo: incorrect (nonexistent) layer or neuron index");

        //
        // 1. find offset of the activation function record in the
        //
        if (network.hlneurons[highlevelidx * hlnfieldwidth + 2] >= 0)
        {
            activationoffset = istart + network.hlneurons[highlevelidx * hlnfieldwidth + 2] * nfieldwidth;
            fkind = network.structinfo[activationoffset + 0];
        }
        else
        {
            fkind = 0;
        }
        if (network.hlneurons[highlevelidx * hlnfieldwidth + 3] >= 0)
        {
            threshold = network.weights[network.hlneurons[highlevelidx * hlnfieldwidth + 3]];
        }
        else
        {
            threshold = 0;
        }
    }


    /*************************************************************************
    This function returns information about connection from I0-th neuron of
    K0-th layer to I1-th neuron of K1-th layer.

    INPUT PARAMETERS:
        Network     -   network
        K0          -   layer index
        I0          -   neuron index (within layer)
        K1          -   layer index
        I1          -   neuron index (within layer)

    RESULT:
        connection weight (zero for non-existent connections)

    This function:
    1. throws exception if layer or neuron with given index do not exists.
    2. returns zero if neurons exist, but there is no connection between them

      -- ALGLIB --
         Copyright 25.03.2011 by Bochkanov Sergey
    *************************************************************************/
    private static double mlpgetweightx(multilayerperceptron network,
        int k0,
        int i0,
        int k1,
        int i1,
        ref int[] integerbuf,
        xparams _params)
    {
        double result = 0;
        int ccnt = 0;
        int highlevelidx = 0;

        ablasf.iallocv(4, ref integerbuf, _params);
        ccnt = ap.len(network.hlconnections) / hlconnfieldwidth;

        //
        // check params
        //
        ap.assert(k0 >= 0 && k0 < ap.len(network.hllayersizes), "MLPGetWeight: incorrect (nonexistent) K0");
        ap.assert(i0 >= 0 && i0 < network.hllayersizes[k0], "MLPGetWeight: incorrect (nonexistent) I0");
        ap.assert(k1 >= 0 && k1 < ap.len(network.hllayersizes), "MLPGetWeight: incorrect (nonexistent) K1");
        ap.assert(i1 >= 0 && i1 < network.hllayersizes[k1], "MLPGetWeight: incorrect (nonexistent) I1");

        //
        // search
        //
        integerbuf[0] = k0;
        integerbuf[1] = i0;
        integerbuf[2] = k1;
        integerbuf[3] = i1;
        highlevelidx = apserv.recsearch(network.hlconnections, hlconnfieldwidth, 4, 0, ccnt, integerbuf, _params);
        if (highlevelidx >= 0)
        {
            result = network.weights[network.hlconnections[highlevelidx * hlconnfieldwidth + 4]];
        }
        else
        {
            result = 0;
        }
        return result;
    }


    /*************************************************************************
    Internal subroutine for Hessian calculation.

    WARNING! Unspeakable math far beyong human capabilities :)
    *************************************************************************/
    private static void mlphessianbatchinternal(multilayerperceptron network,
        double[,] xy,
        int ssize,
        bool naturalerr,
        ref double e,
        ref double[] grad,
        ref double[,] h,
        xparams _params)
    {
        int nin = 0;
        int nout = 0;
        int wcount = 0;
        int ntotal = 0;
        int istart = 0;
        int i = 0;
        int j = 0;
        int k = 0;
        int kl = 0;
        int offs = 0;
        int n1 = 0;
        int n2 = 0;
        int w1 = 0;
        int w2 = 0;
        double s = 0;
        double t = 0;
        double v = 0;
        double et = 0;
        bool bflag = new bool();
        double f = 0;
        double df = 0;
        double d2f = 0;
        double deidyj = 0;
        double mx = 0;
        double q = 0;
        double z = 0;
        double s2 = 0;
        double expi = 0;
        double expj = 0;
        double[] x = new double[0];
        double[] desiredy = new double[0];
        double[] gt = new double[0];
        double[] zeros = new double[0];
        double[,] rx = new double[0, 0];
        double[,] ry = new double[0, 0];
        double[,] rdx = new double[0, 0];
        double[,] rdy = new double[0, 0];
        int i_ = 0;
        int i1_ = 0;

        e = 0;

        mlpproperties(network, ref nin, ref nout, ref wcount, _params);
        ntotal = network.structinfo[3];
        istart = network.structinfo[5];

        //
        // Prepare
        //
        x = new double[nin - 1 + 1];
        desiredy = new double[nout - 1 + 1];
        zeros = new double[wcount - 1 + 1];
        gt = new double[wcount - 1 + 1];
        rx = new double[ntotal + nout - 1 + 1, wcount - 1 + 1];
        ry = new double[ntotal + nout - 1 + 1, wcount - 1 + 1];
        rdx = new double[ntotal + nout - 1 + 1, wcount - 1 + 1];
        rdy = new double[ntotal + nout - 1 + 1, wcount - 1 + 1];
        e = 0;
        for (i = 0; i <= wcount - 1; i++)
        {
            zeros[i] = 0;
        }
        for (i_ = 0; i_ <= wcount - 1; i_++)
        {
            grad[i_] = zeros[i_];
        }
        for (i = 0; i <= wcount - 1; i++)
        {
            for (i_ = 0; i_ <= wcount - 1; i_++)
            {
                h[i, i_] = zeros[i_];
            }
        }

        //
        // Process
        //
        for (k = 0; k <= ssize - 1; k++)
        {

            //
            // Process vector with MLPGradN.
            // Now Neurons, DFDNET and DError contains results of the last run.
            //
            for (i_ = 0; i_ <= nin - 1; i_++)
            {
                x[i_] = xy[k, i_];
            }
            if (mlpissoftmax(network, _params))
            {

                //
                // class labels outputs
                //
                kl = (int)Math.Round(xy[k, nin]);
                for (i = 0; i <= nout - 1; i++)
                {
                    if (i == kl)
                    {
                        desiredy[i] = 1;
                    }
                    else
                    {
                        desiredy[i] = 0;
                    }
                }
            }
            else
            {

                //
                // real outputs
                //
                i1_ = (nin) - (0);
                for (i_ = 0; i_ <= nout - 1; i_++)
                {
                    desiredy[i_] = xy[k, i_ + i1_];
                }
            }
            if (naturalerr)
            {
                mlpgradn(network, x, desiredy, ref et, ref gt, _params);
            }
            else
            {
                mlpgrad(network, x, desiredy, ref et, ref gt, _params);
            }

            //
            // grad, error
            //
            e = e + et;
            for (i_ = 0; i_ <= wcount - 1; i_++)
            {
                grad[i_] = grad[i_] + gt[i_];
            }

            //
            // Hessian.
            // Forward pass of the R-algorithm
            //
            for (i = 0; i <= ntotal - 1; i++)
            {
                offs = istart + i * nfieldwidth;
                for (i_ = 0; i_ <= wcount - 1; i_++)
                {
                    rx[i, i_] = zeros[i_];
                }
                for (i_ = 0; i_ <= wcount - 1; i_++)
                {
                    ry[i, i_] = zeros[i_];
                }
                if (network.structinfo[offs + 0] > 0 || network.structinfo[offs + 0] == -5)
                {

                    //
                    // Activation function
                    //
                    n1 = network.structinfo[offs + 2];
                    for (i_ = 0; i_ <= wcount - 1; i_++)
                    {
                        rx[i, i_] = ry[n1, i_];
                    }
                    v = network.dfdnet[i];
                    for (i_ = 0; i_ <= wcount - 1; i_++)
                    {
                        ry[i, i_] = v * rx[i, i_];
                    }
                    continue;
                }
                if (network.structinfo[offs + 0] == 0)
                {

                    //
                    // Adaptive summator
                    //
                    n1 = network.structinfo[offs + 2];
                    n2 = n1 + network.structinfo[offs + 1] - 1;
                    w1 = network.structinfo[offs + 3];
                    w2 = w1 + network.structinfo[offs + 1] - 1;
                    for (j = n1; j <= n2; j++)
                    {
                        v = network.weights[w1 + j - n1];
                        for (i_ = 0; i_ <= wcount - 1; i_++)
                        {
                            rx[i, i_] = rx[i, i_] + v * ry[j, i_];
                        }
                        rx[i, w1 + j - n1] = rx[i, w1 + j - n1] + network.neurons[j];
                    }
                    for (i_ = 0; i_ <= wcount - 1; i_++)
                    {
                        ry[i, i_] = rx[i, i_];
                    }
                    continue;
                }
                if (network.structinfo[offs + 0] < 0)
                {
                    bflag = true;
                    if (network.structinfo[offs + 0] == -2)
                    {

                        //
                        // input neuron, left unchanged
                        //
                        bflag = false;
                    }
                    if (network.structinfo[offs + 0] == -3)
                    {

                        //
                        // "-1" neuron, left unchanged
                        //
                        bflag = false;
                    }
                    if (network.structinfo[offs + 0] == -4)
                    {

                        //
                        // "0" neuron, left unchanged
                        //
                        bflag = false;
                    }
                    ap.assert(!bflag, "MLPHessianNBatch: internal error - unknown neuron type!");
                    continue;
                }
            }

            //
            // Hessian. Backward pass of the R-algorithm.
            //
            // Stage 1. Initialize RDY
            //
            for (i = 0; i <= ntotal + nout - 1; i++)
            {
                for (i_ = 0; i_ <= wcount - 1; i_++)
                {
                    rdy[i, i_] = zeros[i_];
                }
            }
            if (network.structinfo[6] == 0)
            {

                //
                // Standardisation.
                //
                // In context of the Hessian calculation standardisation
                // is considered as additional layer with weightless
                // activation function:
                //
                // F(NET) := Sigma*NET
                //
                // So we add one more layer to forward pass, and
                // make forward/backward pass through this layer.
                //
                for (i = 0; i <= nout - 1; i++)
                {
                    n1 = ntotal - nout + i;
                    n2 = ntotal + i;

                    //
                    // Forward pass from N1 to N2
                    //
                    for (i_ = 0; i_ <= wcount - 1; i_++)
                    {
                        rx[n2, i_] = ry[n1, i_];
                    }
                    v = network.columnsigmas[nin + i];
                    for (i_ = 0; i_ <= wcount - 1; i_++)
                    {
                        ry[n2, i_] = v * rx[n2, i_];
                    }

                    //
                    // Initialization of RDY
                    //
                    for (i_ = 0; i_ <= wcount - 1; i_++)
                    {
                        rdy[n2, i_] = ry[n2, i_];
                    }

                    //
                    // Backward pass from N2 to N1:
                    // 1. Calculate R(dE/dX).
                    // 2. No R(dE/dWij) is needed since weight of activation neuron
                    //    is fixed to 1. So we can update R(dE/dY) for
                    //    the connected neuron (note that Vij=0, Wij=1)
                    //
                    df = network.columnsigmas[nin + i];
                    for (i_ = 0; i_ <= wcount - 1; i_++)
                    {
                        rdx[n2, i_] = df * rdy[n2, i_];
                    }
                    for (i_ = 0; i_ <= wcount - 1; i_++)
                    {
                        rdy[n1, i_] = rdy[n1, i_] + rdx[n2, i_];
                    }
                }
            }
            else
            {

                //
                // Softmax.
                //
                // Initialize RDY using generalized expression for ei'(yi)
                // (see expression (9) from p. 5 of "Fast Exact Multiplication by the Hessian").
                //
                // When we are working with softmax network, generalized
                // expression for ei'(yi) is used because softmax
                // normalization leads to ei, which depends on all y's
                //
                if (naturalerr)
                {

                    //
                    // softmax + cross-entropy.
                    // We have:
                    //
                    // S = sum(exp(yk)),
                    // ei = sum(trn)*exp(yi)/S-trn_i
                    //
                    // j=i:   d(ei)/d(yj) = T*exp(yi)*(S-exp(yi))/S^2
                    // j<>i:  d(ei)/d(yj) = -T*exp(yi)*exp(yj)/S^2
                    //
                    t = 0;
                    for (i = 0; i <= nout - 1; i++)
                    {
                        t = t + desiredy[i];
                    }
                    mx = network.neurons[ntotal - nout];
                    for (i = 0; i <= nout - 1; i++)
                    {
                        mx = Math.Max(mx, network.neurons[ntotal - nout + i]);
                    }
                    s = 0;
                    for (i = 0; i <= nout - 1; i++)
                    {
                        network.nwbuf[i] = Math.Exp(network.neurons[ntotal - nout + i] - mx);
                        s = s + network.nwbuf[i];
                    }
                    for (i = 0; i <= nout - 1; i++)
                    {
                        for (j = 0; j <= nout - 1; j++)
                        {
                            if (j == i)
                            {
                                deidyj = t * network.nwbuf[i] * (s - network.nwbuf[i]) / math.sqr(s);
                                for (i_ = 0; i_ <= wcount - 1; i_++)
                                {
                                    rdy[ntotal - nout + i, i_] = rdy[ntotal - nout + i, i_] + deidyj * ry[ntotal - nout + i, i_];
                                }
                            }
                            else
                            {
                                deidyj = -(t * network.nwbuf[i] * network.nwbuf[j] / math.sqr(s));
                                for (i_ = 0; i_ <= wcount - 1; i_++)
                                {
                                    rdy[ntotal - nout + i, i_] = rdy[ntotal - nout + i, i_] + deidyj * ry[ntotal - nout + j, i_];
                                }
                            }
                        }
                    }
                }
                else
                {

                    //
                    // For a softmax + squared error we have expression
                    // far beyond human imagination so we dont even try
                    // to comment on it. Just enjoy the code...
                    //
                    // P.S. That's why "natural error" is called "natural" -
                    // compact beatiful expressions, fast code....
                    //
                    mx = network.neurons[ntotal - nout];
                    for (i = 0; i <= nout - 1; i++)
                    {
                        mx = Math.Max(mx, network.neurons[ntotal - nout + i]);
                    }
                    s = 0;
                    s2 = 0;
                    for (i = 0; i <= nout - 1; i++)
                    {
                        network.nwbuf[i] = Math.Exp(network.neurons[ntotal - nout + i] - mx);
                        s = s + network.nwbuf[i];
                        s2 = s2 + math.sqr(network.nwbuf[i]);
                    }
                    q = 0;
                    for (i = 0; i <= nout - 1; i++)
                    {
                        q = q + (network.y[i] - desiredy[i]) * network.nwbuf[i];
                    }
                    for (i = 0; i <= nout - 1; i++)
                    {
                        z = -q + (network.y[i] - desiredy[i]) * s;
                        expi = network.nwbuf[i];
                        for (j = 0; j <= nout - 1; j++)
                        {
                            expj = network.nwbuf[j];
                            if (j == i)
                            {
                                deidyj = expi / math.sqr(s) * ((z + expi) * (s - 2 * expi) / s + expi * s2 / math.sqr(s));
                            }
                            else
                            {
                                deidyj = expi * expj / math.sqr(s) * (s2 / math.sqr(s) - 2 * z / s - (expi + expj) / s + (network.y[i] - desiredy[i]) - (network.y[j] - desiredy[j]));
                            }
                            for (i_ = 0; i_ <= wcount - 1; i_++)
                            {
                                rdy[ntotal - nout + i, i_] = rdy[ntotal - nout + i, i_] + deidyj * ry[ntotal - nout + j, i_];
                            }
                        }
                    }
                }
            }

            //
            // Hessian. Backward pass of the R-algorithm
            //
            // Stage 2. Process.
            //
            for (i = ntotal - 1; i >= 0; i--)
            {

                //
                // Possible variants:
                // 1. Activation function
                // 2. Adaptive summator
                // 3. Special neuron
                //
                offs = istart + i * nfieldwidth;
                if (network.structinfo[offs + 0] > 0 || network.structinfo[offs + 0] == -5)
                {
                    n1 = network.structinfo[offs + 2];

                    //
                    // First, calculate R(dE/dX).
                    //
                    mlpactivationfunction(network.neurons[n1], network.structinfo[offs + 0], ref f, ref df, ref d2f, _params);
                    v = d2f * network.derror[i];
                    for (i_ = 0; i_ <= wcount - 1; i_++)
                    {
                        rdx[i, i_] = df * rdy[i, i_];
                    }
                    for (i_ = 0; i_ <= wcount - 1; i_++)
                    {
                        rdx[i, i_] = rdx[i, i_] + v * rx[i, i_];
                    }

                    //
                    // No R(dE/dWij) is needed since weight of activation neuron
                    // is fixed to 1.
                    //
                    // So we can update R(dE/dY) for the connected neuron.
                    // (note that Vij=0, Wij=1)
                    //
                    for (i_ = 0; i_ <= wcount - 1; i_++)
                    {
                        rdy[n1, i_] = rdy[n1, i_] + rdx[i, i_];
                    }
                    continue;
                }
                if (network.structinfo[offs + 0] == 0)
                {

                    //
                    // Adaptive summator
                    //
                    n1 = network.structinfo[offs + 2];
                    n2 = n1 + network.structinfo[offs + 1] - 1;
                    w1 = network.structinfo[offs + 3];
                    w2 = w1 + network.structinfo[offs + 1] - 1;

                    //
                    // First, calculate R(dE/dX).
                    //
                    for (i_ = 0; i_ <= wcount - 1; i_++)
                    {
                        rdx[i, i_] = rdy[i, i_];
                    }

                    //
                    // Then, calculate R(dE/dWij)
                    //
                    for (j = w1; j <= w2; j++)
                    {
                        v = network.neurons[n1 + j - w1];
                        for (i_ = 0; i_ <= wcount - 1; i_++)
                        {
                            h[j, i_] = h[j, i_] + v * rdx[i, i_];
                        }
                        v = network.derror[i];
                        for (i_ = 0; i_ <= wcount - 1; i_++)
                        {
                            h[j, i_] = h[j, i_] + v * ry[n1 + j - w1, i_];
                        }
                    }

                    //
                    // And finally, update R(dE/dY) for connected neurons.
                    //
                    for (j = w1; j <= w2; j++)
                    {
                        v = network.weights[j];
                        for (i_ = 0; i_ <= wcount - 1; i_++)
                        {
                            rdy[n1 + j - w1, i_] = rdy[n1 + j - w1, i_] + v * rdx[i, i_];
                        }
                        rdy[n1 + j - w1, j] = rdy[n1 + j - w1, j] + network.derror[i];
                    }
                    continue;
                }
                if (network.structinfo[offs + 0] < 0)
                {
                    bflag = false;
                    if ((network.structinfo[offs + 0] == -2 || network.structinfo[offs + 0] == -3) || network.structinfo[offs + 0] == -4)
                    {

                        //
                        // Special neuron type, no back-propagation required
                        //
                        bflag = true;
                    }
                    ap.assert(bflag, "MLPHessianNBatch: unknown neuron type!");
                    continue;
                }
            }
        }
    }


    /*************************************************************************
    Internal subroutine

    Network must be processed by MLPProcess on X
    *************************************************************************/
    private static void mlpinternalcalculategradient(multilayerperceptron network,
        double[] neurons,
        double[] weights,
        ref double[] derror,
        ref double[] grad,
        bool naturalerrorfunc,
        xparams _params)
    {
        int i = 0;
        int n1 = 0;
        int n2 = 0;
        int w1 = 0;
        int w2 = 0;
        int ntotal = 0;
        int istart = 0;
        int nin = 0;
        int nout = 0;
        int offs = 0;
        double dedf = 0;
        double dfdnet = 0;
        double v = 0;
        double fown = 0;
        double deown = 0;
        double net = 0;
        double mx = 0;
        bool bflag = new bool();
        int i_ = 0;
        int i1_ = 0;


        //
        // Read network geometry
        //
        nin = network.structinfo[1];
        nout = network.structinfo[2];
        ntotal = network.structinfo[3];
        istart = network.structinfo[5];

        //
        // Pre-processing of dError/dOut:
        // from dError/dOut(normalized) to dError/dOut(non-normalized)
        //
        ap.assert(network.structinfo[6] == 0 || network.structinfo[6] == 1, "MLPInternalCalculateGradient: unknown normalization type!");
        if (network.structinfo[6] == 1)
        {

            //
            // Softmax
            //
            if (!naturalerrorfunc)
            {
                mx = network.neurons[ntotal - nout];
                for (i = 0; i <= nout - 1; i++)
                {
                    mx = Math.Max(mx, network.neurons[ntotal - nout + i]);
                }
                net = 0;
                for (i = 0; i <= nout - 1; i++)
                {
                    network.nwbuf[i] = Math.Exp(network.neurons[ntotal - nout + i] - mx);
                    net = net + network.nwbuf[i];
                }
                i1_ = (0) - (ntotal - nout);
                v = 0.0;
                for (i_ = ntotal - nout; i_ <= ntotal - 1; i_++)
                {
                    v += network.derror[i_] * network.nwbuf[i_ + i1_];
                }
                for (i = 0; i <= nout - 1; i++)
                {
                    fown = network.nwbuf[i];
                    deown = network.derror[ntotal - nout + i];
                    network.nwbuf[nout + i] = (-v + deown * fown + deown * (net - fown)) * fown / math.sqr(net);
                }
                for (i = 0; i <= nout - 1; i++)
                {
                    network.derror[ntotal - nout + i] = network.nwbuf[nout + i];
                }
            }
        }
        else
        {

            //
            // Un-standardisation
            //
            for (i = 0; i <= nout - 1; i++)
            {
                network.derror[ntotal - nout + i] = network.derror[ntotal - nout + i] * network.columnsigmas[nin + i];
            }
        }

        //
        // Backpropagation
        //
        for (i = ntotal - 1; i >= 0; i--)
        {

            //
            // Extract info
            //
            offs = istart + i * nfieldwidth;
            if (network.structinfo[offs + 0] > 0 || network.structinfo[offs + 0] == -5)
            {

                //
                // Activation function
                //
                dedf = network.derror[i];
                dfdnet = network.dfdnet[i];
                derror[network.structinfo[offs + 2]] = derror[network.structinfo[offs + 2]] + dedf * dfdnet;
                continue;
            }
            if (network.structinfo[offs + 0] == 0)
            {

                //
                // Adaptive summator
                //
                n1 = network.structinfo[offs + 2];
                n2 = n1 + network.structinfo[offs + 1] - 1;
                w1 = network.structinfo[offs + 3];
                w2 = w1 + network.structinfo[offs + 1] - 1;
                dedf = network.derror[i];
                dfdnet = 1.0;
                v = dedf * dfdnet;
                i1_ = (n1) - (w1);
                for (i_ = w1; i_ <= w2; i_++)
                {
                    grad[i_] = v * neurons[i_ + i1_];
                }
                i1_ = (w1) - (n1);
                for (i_ = n1; i_ <= n2; i_++)
                {
                    derror[i_] = derror[i_] + v * weights[i_ + i1_];
                }
                continue;
            }
            if (network.structinfo[offs + 0] < 0)
            {
                bflag = false;
                if ((network.structinfo[offs + 0] == -2 || network.structinfo[offs + 0] == -3) || network.structinfo[offs + 0] == -4)
                {

                    //
                    // Special neuron type, no back-propagation required
                    //
                    bflag = true;
                }
                ap.assert(bflag, "MLPInternalCalculateGradient: unknown neuron type!");
                continue;
            }
        }
    }


    private static void mlpchunkedgradient(multilayerperceptron network,
        double[,] xy,
        int cstart,
        int csize,
        double[] batch4buf,
        double[] hpcbuf,
        ref double e,
        bool naturalerrorfunc,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        int kl = 0;
        int ntotal = 0;
        int nin = 0;
        int nout = 0;
        int offs = 0;
        double f = 0;
        double df = 0;
        double d2f = 0;
        double v = 0;
        double vv = 0;
        double s = 0;
        double fown = 0;
        double deown = 0;
        bool bflag = new bool();
        int istart = 0;
        int entrysize = 0;
        int dfoffs = 0;
        int derroroffs = 0;
        int entryoffs = 0;
        int neuronidx = 0;
        int srcentryoffs = 0;
        int srcneuronidx = 0;
        int srcweightidx = 0;
        int neurontype = 0;
        int nweights = 0;
        int offs0 = 0;
        int offs1 = 0;
        int offs2 = 0;
        double v0 = 0;
        double v1 = 0;
        double v2 = 0;
        double v3 = 0;
        double s0 = 0;
        double s1 = 0;
        double s2 = 0;
        double s3 = 0;
        int chunksize = 0;

        chunksize = 4;
        ap.assert(csize <= chunksize, "MLPChunkedGradient: internal error (CSize>ChunkSize)");

        //
        // Try to use HPC core, if possible
        //
        if (hpccores.hpcchunkedgradient(network.weights, network.structinfo, network.columnmeans, network.columnsigmas, xy, cstart, csize, batch4buf, hpcbuf, ref e, naturalerrorfunc, _params))
        {
            return;
        }

        //
        // Read network geometry, prepare data
        //
        nin = network.structinfo[1];
        nout = network.structinfo[2];
        ntotal = network.structinfo[3];
        istart = network.structinfo[5];
        entrysize = 12;
        dfoffs = 4;
        derroroffs = 8;

        //
        // Fill Batch4Buf by zeros.
        //
        // THIS STAGE IS VERY IMPORTANT!
        //
        // We fill all components of entry - neuron values, dF/dNET, dError/dF.
        // It allows us to easily handle  situations  when  CSize<ChunkSize  by
        // simply  working  with  ALL  components  of  Batch4Buf,  without ever
        // looking at CSize. The idea is that dError/dF for  absent  components
        // will be initialized by zeros - and won't be  rewritten  by  non-zero
        // values during backpropagation.
        //
        for (i = 0; i <= entrysize * ntotal - 1; i++)
        {
            batch4buf[i] = 0;
        }

        //
        // Forward pass:
        // 1. Load data into Batch4Buf. If CSize<ChunkSize, data are padded by zeros.
        // 2. Perform forward pass through network
        //
        for (i = 0; i <= nin - 1; i++)
        {
            entryoffs = entrysize * i;
            for (j = 0; j <= csize - 1; j++)
            {
                if ((double)(network.columnsigmas[i]) != (double)(0))
                {
                    batch4buf[entryoffs + j] = (xy[cstart + j, i] - network.columnmeans[i]) / network.columnsigmas[i];
                }
                else
                {
                    batch4buf[entryoffs + j] = xy[cstart + j, i] - network.columnmeans[i];
                }
            }
        }
        for (neuronidx = 0; neuronidx <= ntotal - 1; neuronidx++)
        {
            entryoffs = entrysize * neuronidx;
            offs = istart + neuronidx * nfieldwidth;
            neurontype = network.structinfo[offs + 0];
            if (neurontype > 0 || neurontype == -5)
            {

                //
                // "activation function" neuron, which takes value of neuron SrcNeuronIdx
                // and applies activation function to it.
                //
                // This neuron has no weights and no tunable parameters.
                //
                srcneuronidx = network.structinfo[offs + 2];
                srcentryoffs = entrysize * srcneuronidx;
                mlpactivationfunction(batch4buf[srcentryoffs + 0], neurontype, ref f, ref df, ref d2f, _params);
                batch4buf[entryoffs + 0] = f;
                batch4buf[entryoffs + 0 + dfoffs] = df;
                mlpactivationfunction(batch4buf[srcentryoffs + 1], neurontype, ref f, ref df, ref d2f, _params);
                batch4buf[entryoffs + 1] = f;
                batch4buf[entryoffs + 1 + dfoffs] = df;
                mlpactivationfunction(batch4buf[srcentryoffs + 2], neurontype, ref f, ref df, ref d2f, _params);
                batch4buf[entryoffs + 2] = f;
                batch4buf[entryoffs + 2 + dfoffs] = df;
                mlpactivationfunction(batch4buf[srcentryoffs + 3], neurontype, ref f, ref df, ref d2f, _params);
                batch4buf[entryoffs + 3] = f;
                batch4buf[entryoffs + 3 + dfoffs] = df;
                continue;
            }
            if (neurontype == 0)
            {

                //
                // "adaptive summator" neuron, whose output is a weighted sum of inputs.
                // It has weights, but has no activation function.
                //
                nweights = network.structinfo[offs + 1];
                srcneuronidx = network.structinfo[offs + 2];
                srcentryoffs = entrysize * srcneuronidx;
                srcweightidx = network.structinfo[offs + 3];
                v0 = 0;
                v1 = 0;
                v2 = 0;
                v3 = 0;
                for (j = 0; j <= nweights - 1; j++)
                {
                    v = network.weights[srcweightidx];
                    srcweightidx = srcweightidx + 1;
                    v0 = v0 + v * batch4buf[srcentryoffs + 0];
                    v1 = v1 + v * batch4buf[srcentryoffs + 1];
                    v2 = v2 + v * batch4buf[srcentryoffs + 2];
                    v3 = v3 + v * batch4buf[srcentryoffs + 3];
                    srcentryoffs = srcentryoffs + entrysize;
                }
                batch4buf[entryoffs + 0] = v0;
                batch4buf[entryoffs + 1] = v1;
                batch4buf[entryoffs + 2] = v2;
                batch4buf[entryoffs + 3] = v3;
                batch4buf[entryoffs + 0 + dfoffs] = 1;
                batch4buf[entryoffs + 1 + dfoffs] = 1;
                batch4buf[entryoffs + 2 + dfoffs] = 1;
                batch4buf[entryoffs + 3 + dfoffs] = 1;
                continue;
            }
            if (neurontype < 0)
            {
                bflag = false;
                if (neurontype == -2)
                {

                    //
                    // Input neuron, left unchanged
                    //
                    bflag = true;
                }
                if (neurontype == -3)
                {

                    //
                    // "-1" neuron
                    //
                    batch4buf[entryoffs + 0] = -1;
                    batch4buf[entryoffs + 1] = -1;
                    batch4buf[entryoffs + 2] = -1;
                    batch4buf[entryoffs + 3] = -1;
                    batch4buf[entryoffs + 0 + dfoffs] = 0;
                    batch4buf[entryoffs + 1 + dfoffs] = 0;
                    batch4buf[entryoffs + 2 + dfoffs] = 0;
                    batch4buf[entryoffs + 3 + dfoffs] = 0;
                    bflag = true;
                }
                if (neurontype == -4)
                {

                    //
                    // "0" neuron
                    //
                    batch4buf[entryoffs + 0] = 0;
                    batch4buf[entryoffs + 1] = 0;
                    batch4buf[entryoffs + 2] = 0;
                    batch4buf[entryoffs + 3] = 0;
                    batch4buf[entryoffs + 0 + dfoffs] = 0;
                    batch4buf[entryoffs + 1 + dfoffs] = 0;
                    batch4buf[entryoffs + 2 + dfoffs] = 0;
                    batch4buf[entryoffs + 3 + dfoffs] = 0;
                    bflag = true;
                }
                ap.assert(bflag, "MLPChunkedGradient: internal error - unknown neuron type!");
                continue;
            }
        }

        //
        // Intermediate phase between forward and backward passes.
        //
        // For regression networks:
        // * forward pass is completely done (no additional post-processing is
        //   needed).
        // * before starting backward pass, we have to  calculate  dError/dOut
        //   for output neurons. We also update error at this phase.
        //
        // For classification networks:
        // * in addition to forward pass we  apply  SOFTMAX  normalization  to
        //   output neurons.
        // * after applying normalization, we have to  calculate  dError/dOut,
        //   which is calculated in two steps:
        //   * first, we calculate derivative of error with respect to SOFTMAX
        //     normalized outputs (normalized dError)
        //   * then,  we calculate derivative of error with respect to  values
        //     of outputs BEFORE normalization was applied to them
        //
        ap.assert(network.structinfo[6] == 0 || network.structinfo[6] == 1, "MLPChunkedGradient: unknown normalization type!");
        if (network.structinfo[6] == 1)
        {

            //
            // SOFTMAX-normalized network.
            //
            // First,  calculate (V0,V1,V2,V3)  -  component-wise  maximum
            // of output neurons. This vector of maximum  values  will  be
            // used for normalization  of  outputs  prior  to  calculating
            // exponentials.
            //
            // NOTE: the only purpose of this stage is to prevent overflow
            //       during calculation of exponentials.  With  this stage
            //       we  make  sure  that  all exponentials are calculated
            //       with non-positive argument. If you load (0,0,0,0)  to
            //       (V0,V1,V2,V3), your program will continue  working  -
            //       although with less robustness.
            //
            entryoffs = entrysize * (ntotal - nout);
            v0 = batch4buf[entryoffs + 0];
            v1 = batch4buf[entryoffs + 1];
            v2 = batch4buf[entryoffs + 2];
            v3 = batch4buf[entryoffs + 3];
            entryoffs = entryoffs + entrysize;
            for (i = 1; i <= nout - 1; i++)
            {
                v = batch4buf[entryoffs + 0];
                if (v > v0)
                {
                    v0 = v;
                }
                v = batch4buf[entryoffs + 1];
                if (v > v1)
                {
                    v1 = v;
                }
                v = batch4buf[entryoffs + 2];
                if (v > v2)
                {
                    v2 = v;
                }
                v = batch4buf[entryoffs + 3];
                if (v > v3)
                {
                    v3 = v;
                }
                entryoffs = entryoffs + entrysize;
            }

            //
            // Then,  calculate exponentials and place them to part of the
            // array which  is  located  past  the  last  entry.  We  also
            // calculate sum of exponentials which will be stored past the
            // exponentials.
            //
            entryoffs = entrysize * (ntotal - nout);
            offs0 = entrysize * ntotal;
            s0 = 0;
            s1 = 0;
            s2 = 0;
            s3 = 0;
            for (i = 0; i <= nout - 1; i++)
            {
                v = Math.Exp(batch4buf[entryoffs + 0] - v0);
                s0 = s0 + v;
                batch4buf[offs0 + 0] = v;
                v = Math.Exp(batch4buf[entryoffs + 1] - v1);
                s1 = s1 + v;
                batch4buf[offs0 + 1] = v;
                v = Math.Exp(batch4buf[entryoffs + 2] - v2);
                s2 = s2 + v;
                batch4buf[offs0 + 2] = v;
                v = Math.Exp(batch4buf[entryoffs + 3] - v3);
                s3 = s3 + v;
                batch4buf[offs0 + 3] = v;
                entryoffs = entryoffs + entrysize;
                offs0 = offs0 + chunksize;
            }
            offs0 = entrysize * ntotal + 2 * nout * chunksize;
            batch4buf[offs0 + 0] = s0;
            batch4buf[offs0 + 1] = s1;
            batch4buf[offs0 + 2] = s2;
            batch4buf[offs0 + 3] = s3;

            //
            // Now we have:
            // * Batch4Buf[0...EntrySize*NTotal-1] stores:
            //   * NTotal*ChunkSize neuron output values (SOFTMAX normalization
            //     was not applied to these values),
            //   * NTotal*ChunkSize values of dF/dNET (derivative of neuron
            //     output with respect to its input)
            //   * NTotal*ChunkSize zeros in the elements which correspond to
            //     dError/dOut (derivative of error with respect to neuron output).
            // * Batch4Buf[EntrySize*NTotal...EntrySize*NTotal+ChunkSize*NOut-1] -
            //   stores exponentials of last NOut neurons.
            // * Batch4Buf[EntrySize*NTotal+ChunkSize*NOut-1...EntrySize*NTotal+ChunkSize*2*NOut-1]
            //   - can be used for temporary calculations
            // * Batch4Buf[EntrySize*NTotal+ChunkSize*2*NOut...EntrySize*NTotal+ChunkSize*2*NOut+ChunkSize-1]
            //   - stores sum-of-exponentials
            //
            // Block below calculates derivatives of error function with respect 
            // to non-SOFTMAX-normalized output values of last NOut neurons.
            //
            // It is quite complicated; we do not describe algebra behind it,
            // but if you want you may check it yourself :)
            //
            if (naturalerrorfunc)
            {

                //
                // Calculate  derivative  of  error  with respect to values of
                // output  neurons  PRIOR TO SOFTMAX NORMALIZATION. Because we
                // use natural error function (cross-entropy), we  can  do  so
                // very easy.
                //
                offs0 = entrysize * ntotal + 2 * nout * chunksize;
                for (k = 0; k <= csize - 1; k++)
                {
                    s = batch4buf[offs0 + k];
                    kl = (int)Math.Round(xy[cstart + k, nin]);
                    offs1 = (ntotal - nout) * entrysize + derroroffs + k;
                    offs2 = entrysize * ntotal + k;
                    for (i = 0; i <= nout - 1; i++)
                    {
                        if (i == kl)
                        {
                            v = 1;
                        }
                        else
                        {
                            v = 0;
                        }
                        vv = batch4buf[offs2];
                        batch4buf[offs1] = vv / s - v;
                        e = e + safecrossentropy(v, vv / s, _params);
                        offs1 = offs1 + entrysize;
                        offs2 = offs2 + chunksize;
                    }
                }
            }
            else
            {

                //
                // SOFTMAX normalization makes things very difficult.
                // Sorry, we do not dare to describe this esoteric math
                // in details.
                //
                offs0 = entrysize * ntotal + chunksize * 2 * nout;
                for (k = 0; k <= csize - 1; k++)
                {
                    s = batch4buf[offs0 + k];
                    kl = (int)Math.Round(xy[cstart + k, nin]);
                    vv = 0;
                    offs1 = entrysize * ntotal + k;
                    offs2 = entrysize * ntotal + nout * chunksize + k;
                    for (i = 0; i <= nout - 1; i++)
                    {
                        fown = batch4buf[offs1];
                        if (i == kl)
                        {
                            deown = fown / s - 1;
                        }
                        else
                        {
                            deown = fown / s;
                        }
                        batch4buf[offs2] = deown;
                        vv = vv + deown * fown;
                        e = e + deown * deown / 2;
                        offs1 = offs1 + chunksize;
                        offs2 = offs2 + chunksize;
                    }
                    offs1 = entrysize * ntotal + k;
                    offs2 = entrysize * ntotal + nout * chunksize + k;
                    for (i = 0; i <= nout - 1; i++)
                    {
                        fown = batch4buf[offs1];
                        deown = batch4buf[offs2];
                        batch4buf[(ntotal - nout + i) * entrysize + derroroffs + k] = (-vv + deown * fown + deown * (s - fown)) * fown / math.sqr(s);
                        offs1 = offs1 + chunksize;
                        offs2 = offs2 + chunksize;
                    }
                }
            }
        }
        else
        {

            //
            // Regression network with sum-of-squares function.
            //
            // For each NOut of last neurons:
            // * calculate difference between actual and desired output
            // * calculate dError/dOut for this neuron (proportional to difference)
            // * store in in last 4 components of entry (these values are used
            //   to start backpropagation)
            // * update error
            //
            for (i = 0; i <= nout - 1; i++)
            {
                v0 = network.columnsigmas[nin + i];
                v1 = network.columnmeans[nin + i];
                entryoffs = entrysize * (ntotal - nout + i);
                offs0 = entryoffs;
                offs1 = entryoffs + derroroffs;
                for (j = 0; j <= csize - 1; j++)
                {
                    v = batch4buf[offs0 + j] * v0 + v1 - xy[cstart + j, nin + i];
                    batch4buf[offs1 + j] = v * v0;
                    e = e + v * v / 2;
                }
            }
        }

        //
        // Backpropagation
        //
        for (neuronidx = ntotal - 1; neuronidx >= 0; neuronidx--)
        {
            entryoffs = entrysize * neuronidx;
            offs = istart + neuronidx * nfieldwidth;
            neurontype = network.structinfo[offs + 0];
            if (neurontype > 0 || neurontype == -5)
            {

                //
                // Activation function
                //
                srcneuronidx = network.structinfo[offs + 2];
                srcentryoffs = entrysize * srcneuronidx;
                offs0 = srcentryoffs + derroroffs;
                offs1 = entryoffs + derroroffs;
                offs2 = entryoffs + dfoffs;
                batch4buf[offs0 + 0] = batch4buf[offs0 + 0] + batch4buf[offs1 + 0] * batch4buf[offs2 + 0];
                batch4buf[offs0 + 1] = batch4buf[offs0 + 1] + batch4buf[offs1 + 1] * batch4buf[offs2 + 1];
                batch4buf[offs0 + 2] = batch4buf[offs0 + 2] + batch4buf[offs1 + 2] * batch4buf[offs2 + 2];
                batch4buf[offs0 + 3] = batch4buf[offs0 + 3] + batch4buf[offs1 + 3] * batch4buf[offs2 + 3];
                continue;
            }
            if (neurontype == 0)
            {

                //
                // Adaptive summator
                //
                nweights = network.structinfo[offs + 1];
                srcneuronidx = network.structinfo[offs + 2];
                srcentryoffs = entrysize * srcneuronidx;
                srcweightidx = network.structinfo[offs + 3];
                v0 = batch4buf[entryoffs + derroroffs + 0];
                v1 = batch4buf[entryoffs + derroroffs + 1];
                v2 = batch4buf[entryoffs + derroroffs + 2];
                v3 = batch4buf[entryoffs + derroroffs + 3];
                for (j = 0; j <= nweights - 1; j++)
                {
                    offs0 = srcentryoffs;
                    offs1 = srcentryoffs + derroroffs;
                    v = network.weights[srcweightidx];
                    hpcbuf[srcweightidx] = hpcbuf[srcweightidx] + batch4buf[offs0 + 0] * v0 + batch4buf[offs0 + 1] * v1 + batch4buf[offs0 + 2] * v2 + batch4buf[offs0 + 3] * v3;
                    batch4buf[offs1 + 0] = batch4buf[offs1 + 0] + v * v0;
                    batch4buf[offs1 + 1] = batch4buf[offs1 + 1] + v * v1;
                    batch4buf[offs1 + 2] = batch4buf[offs1 + 2] + v * v2;
                    batch4buf[offs1 + 3] = batch4buf[offs1 + 3] + v * v3;
                    srcentryoffs = srcentryoffs + entrysize;
                    srcweightidx = srcweightidx + 1;
                }
                continue;
            }
            if (neurontype < 0)
            {
                bflag = false;
                if ((neurontype == -2 || neurontype == -3) || neurontype == -4)
                {

                    //
                    // Special neuron type, no back-propagation required
                    //
                    bflag = true;
                }
                ap.assert(bflag, "MLPInternalCalculateGradient: unknown neuron type!");
                continue;
            }
        }
    }


    private static void mlpchunkedprocess(multilayerperceptron network,
        double[,] xy,
        int cstart,
        int csize,
        double[] batch4buf,
        double[] hpcbuf,
        xparams _params)
    {
        int i = 0;
        int j = 0;
        int ntotal = 0;
        int nin = 0;
        int nout = 0;
        int offs = 0;
        double f = 0;
        double df = 0;
        double d2f = 0;
        double v = 0;
        bool bflag = new bool();
        int istart = 0;
        int entrysize = 0;
        int entryoffs = 0;
        int neuronidx = 0;
        int srcentryoffs = 0;
        int srcneuronidx = 0;
        int srcweightidx = 0;
        int neurontype = 0;
        int nweights = 0;
        int offs0 = 0;
        double v0 = 0;
        double v1 = 0;
        double v2 = 0;
        double v3 = 0;
        double s0 = 0;
        double s1 = 0;
        double s2 = 0;
        double s3 = 0;
        int chunksize = 0;

        chunksize = 4;
        ap.assert(csize <= chunksize, "MLPChunkedProcess: internal error (CSize>ChunkSize)");

        //
        // Try to use HPC core, if possible
        //
        if (hpccores.hpcchunkedprocess(network.weights, network.structinfo, network.columnmeans, network.columnsigmas, xy, cstart, csize, batch4buf, hpcbuf, _params))
        {
            return;
        }

        //
        // Read network geometry, prepare data
        //
        nin = network.structinfo[1];
        nout = network.structinfo[2];
        ntotal = network.structinfo[3];
        istart = network.structinfo[5];
        entrysize = 4;

        //
        // Fill Batch4Buf by zeros.
        //
        // THIS STAGE IS VERY IMPORTANT!
        //
        // We fill all components of entry - neuron values, dF/dNET, dError/dF.
        // It allows us to easily handle  situations  when  CSize<ChunkSize  by
        // simply  working  with  ALL  components  of  Batch4Buf,  without ever
        // looking at CSize.
        //
        for (i = 0; i <= entrysize * ntotal - 1; i++)
        {
            batch4buf[i] = 0;
        }

        //
        // Forward pass:
        // 1. Load data into Batch4Buf. If CSize<ChunkSize, data are padded by zeros.
        // 2. Perform forward pass through network
        //
        for (i = 0; i <= nin - 1; i++)
        {
            entryoffs = entrysize * i;
            for (j = 0; j <= csize - 1; j++)
            {
                if ((double)(network.columnsigmas[i]) != (double)(0))
                {
                    batch4buf[entryoffs + j] = (xy[cstart + j, i] - network.columnmeans[i]) / network.columnsigmas[i];
                }
                else
                {
                    batch4buf[entryoffs + j] = xy[cstart + j, i] - network.columnmeans[i];
                }
            }
        }
        for (neuronidx = 0; neuronidx <= ntotal - 1; neuronidx++)
        {
            entryoffs = entrysize * neuronidx;
            offs = istart + neuronidx * nfieldwidth;
            neurontype = network.structinfo[offs + 0];
            if (neurontype > 0 || neurontype == -5)
            {

                //
                // "activation function" neuron, which takes value of neuron SrcNeuronIdx
                // and applies activation function to it.
                //
                // This neuron has no weights and no tunable parameters.
                //
                srcneuronidx = network.structinfo[offs + 2];
                srcentryoffs = entrysize * srcneuronidx;
                mlpactivationfunction(batch4buf[srcentryoffs + 0], neurontype, ref f, ref df, ref d2f, _params);
                batch4buf[entryoffs + 0] = f;
                mlpactivationfunction(batch4buf[srcentryoffs + 1], neurontype, ref f, ref df, ref d2f, _params);
                batch4buf[entryoffs + 1] = f;
                mlpactivationfunction(batch4buf[srcentryoffs + 2], neurontype, ref f, ref df, ref d2f, _params);
                batch4buf[entryoffs + 2] = f;
                mlpactivationfunction(batch4buf[srcentryoffs + 3], neurontype, ref f, ref df, ref d2f, _params);
                batch4buf[entryoffs + 3] = f;
                continue;
            }
            if (neurontype == 0)
            {

                //
                // "adaptive summator" neuron, whose output is a weighted sum of inputs.
                // It has weights, but has no activation function.
                //
                nweights = network.structinfo[offs + 1];
                srcneuronidx = network.structinfo[offs + 2];
                srcentryoffs = entrysize * srcneuronidx;
                srcweightidx = network.structinfo[offs + 3];
                v0 = 0;
                v1 = 0;
                v2 = 0;
                v3 = 0;
                for (j = 0; j <= nweights - 1; j++)
                {
                    v = network.weights[srcweightidx];
                    srcweightidx = srcweightidx + 1;
                    v0 = v0 + v * batch4buf[srcentryoffs + 0];
                    v1 = v1 + v * batch4buf[srcentryoffs + 1];
                    v2 = v2 + v * batch4buf[srcentryoffs + 2];
                    v3 = v3 + v * batch4buf[srcentryoffs + 3];
                    srcentryoffs = srcentryoffs + entrysize;
                }
                batch4buf[entryoffs + 0] = v0;
                batch4buf[entryoffs + 1] = v1;
                batch4buf[entryoffs + 2] = v2;
                batch4buf[entryoffs + 3] = v3;
                continue;
            }
            if (neurontype < 0)
            {
                bflag = false;
                if (neurontype == -2)
                {

                    //
                    // Input neuron, left unchanged
                    //
                    bflag = true;
                }
                if (neurontype == -3)
                {

                    //
                    // "-1" neuron
                    //
                    batch4buf[entryoffs + 0] = -1;
                    batch4buf[entryoffs + 1] = -1;
                    batch4buf[entryoffs + 2] = -1;
                    batch4buf[entryoffs + 3] = -1;
                    bflag = true;
                }
                if (neurontype == -4)
                {

                    //
                    // "0" neuron
                    //
                    batch4buf[entryoffs + 0] = 0;
                    batch4buf[entryoffs + 1] = 0;
                    batch4buf[entryoffs + 2] = 0;
                    batch4buf[entryoffs + 3] = 0;
                    bflag = true;
                }
                ap.assert(bflag, "MLPChunkedProcess: internal error - unknown neuron type!");
                continue;
            }
        }

        //
        // SOFTMAX normalization or scaling.
        //
        ap.assert(network.structinfo[6] == 0 || network.structinfo[6] == 1, "MLPChunkedProcess: unknown normalization type!");
        if (network.structinfo[6] == 1)
        {

            //
            // SOFTMAX-normalized network.
            //
            // First,  calculate (V0,V1,V2,V3)  -  component-wise  maximum
            // of output neurons. This vector of maximum  values  will  be
            // used for normalization  of  outputs  prior  to  calculating
            // exponentials.
            //
            // NOTE: the only purpose of this stage is to prevent overflow
            //       during calculation of exponentials.  With  this stage
            //       we  make  sure  that  all exponentials are calculated
            //       with non-positive argument. If you load (0,0,0,0)  to
            //       (V0,V1,V2,V3), your program will continue  working  -
            //       although with less robustness.
            //
            entryoffs = entrysize * (ntotal - nout);
            v0 = batch4buf[entryoffs + 0];
            v1 = batch4buf[entryoffs + 1];
            v2 = batch4buf[entryoffs + 2];
            v3 = batch4buf[entryoffs + 3];
            entryoffs = entryoffs + entrysize;
            for (i = 1; i <= nout - 1; i++)
            {
                v = batch4buf[entryoffs + 0];
                if (v > v0)
                {
                    v0 = v;
                }
                v = batch4buf[entryoffs + 1];
                if (v > v1)
                {
                    v1 = v;
                }
                v = batch4buf[entryoffs + 2];
                if (v > v2)
                {
                    v2 = v;
                }
                v = batch4buf[entryoffs + 3];
                if (v > v3)
                {
                    v3 = v;
                }
                entryoffs = entryoffs + entrysize;
            }

            //
            // Then,  calculate exponentials and place them to part of the
            // array which  is  located  past  the  last  entry.  We  also
            // calculate sum of exponentials.
            //
            entryoffs = entrysize * (ntotal - nout);
            offs0 = entrysize * ntotal;
            s0 = 0;
            s1 = 0;
            s2 = 0;
            s3 = 0;
            for (i = 0; i <= nout - 1; i++)
            {
                v = Math.Exp(batch4buf[entryoffs + 0] - v0);
                s0 = s0 + v;
                batch4buf[offs0 + 0] = v;
                v = Math.Exp(batch4buf[entryoffs + 1] - v1);
                s1 = s1 + v;
                batch4buf[offs0 + 1] = v;
                v = Math.Exp(batch4buf[entryoffs + 2] - v2);
                s2 = s2 + v;
                batch4buf[offs0 + 2] = v;
                v = Math.Exp(batch4buf[entryoffs + 3] - v3);
                s3 = s3 + v;
                batch4buf[offs0 + 3] = v;
                entryoffs = entryoffs + entrysize;
                offs0 = offs0 + chunksize;
            }

            //
            // Write SOFTMAX-normalized values to the output array.
            //
            offs0 = entrysize * ntotal;
            for (i = 0; i <= nout - 1; i++)
            {
                if (csize > 0)
                {
                    xy[cstart + 0, nin + i] = batch4buf[offs0 + 0] / s0;
                }
                if (csize > 1)
                {
                    xy[cstart + 1, nin + i] = batch4buf[offs0 + 1] / s1;
                }
                if (csize > 2)
                {
                    xy[cstart + 2, nin + i] = batch4buf[offs0 + 2] / s2;
                }
                if (csize > 3)
                {
                    xy[cstart + 3, nin + i] = batch4buf[offs0 + 3] / s3;
                }
                offs0 = offs0 + chunksize;
            }
        }
        else
        {

            //
            // Regression network with sum-of-squares function.
            //
            // For each NOut of last neurons:
            // * calculate difference between actual and desired output
            // * calculate dError/dOut for this neuron (proportional to difference)
            // * store in in last 4 components of entry (these values are used
            //   to start backpropagation)
            // * update error
            //
            for (i = 0; i <= nout - 1; i++)
            {
                v0 = network.columnsigmas[nin + i];
                v1 = network.columnmeans[nin + i];
                entryoffs = entrysize * (ntotal - nout + i);
                for (j = 0; j <= csize - 1; j++)
                {
                    xy[cstart + j, nin + i] = batch4buf[entryoffs + j] * v0 + v1;
                }
            }
        }
    }


    /*************************************************************************
    Returns T*Ln(T/Z), guarded against overflow/underflow.
    Internal subroutine.
    *************************************************************************/
    private static double safecrossentropy(double t,
        double z,
        xparams _params)
    {
        double result = 0;
        double r = 0;

        if ((double)(t) == (double)(0))
        {
            result = 0;
        }
        else
        {
            if ((double)(Math.Abs(z)) > (double)(1))
            {

                //
                // Shouldn't be the case with softmax,
                // but we just want to be sure.
                //
                if ((double)(t / z) == (double)(0))
                {
                    r = math.minrealnumber;
                }
                else
                {
                    r = t / z;
                }
            }
            else
            {

                //
                // Normal case
                //
                if ((double)(z) == (double)(0) || (double)(Math.Abs(t)) >= (double)(math.maxrealnumber * Math.Abs(z)))
                {
                    r = math.maxrealnumber;
                }
                else
                {
                    r = t / z;
                }
            }
            result = t * Math.Log(r);
        }
        return result;
    }


    /*************************************************************************
    This function performs backward pass of neural network randimization:
    * it assumes that Network.Weights stores standard deviation of weights
      (weights are not generated yet, only their deviations are present)
    * it sets deviations of weights which feed NeuronIdx-th neuron to specified value
    * it recursively passes to deeper neuron and modifies their weights
    * it stops after encountering nonlinear neurons, linear activation function,
      input neurons, "0" and "-1" neurons

      -- ALGLIB --
         Copyright 27.06.2013 by Bochkanov Sergey
    *************************************************************************/
    private static void randomizebackwardpass(multilayerperceptron network,
        int neuronidx,
        double v,
        xparams _params)
    {
        int istart = 0;
        int neurontype = 0;
        int n1 = 0;
        int n2 = 0;
        int w1 = 0;
        int w2 = 0;
        int offs = 0;
        int i = 0;

        istart = network.structinfo[5];
        neurontype = network.structinfo[istart + neuronidx * nfieldwidth + 0];
        if (neurontype == -2)
        {

            //
            // Input neuron - stop
            //
            return;
        }
        if (neurontype == -3)
        {

            //
            // "-1" neuron: stop
            //
            return;
        }
        if (neurontype == -4)
        {

            //
            // "0" neuron: stop
            //
            return;
        }
        if (neurontype == 0)
        {

            //
            // Adaptive summator neuron:
            // * modify deviations of its weights
            // * recursively call this function for its inputs
            //
            offs = istart + neuronidx * nfieldwidth;
            n1 = network.structinfo[offs + 2];
            n2 = n1 + network.structinfo[offs + 1] - 1;
            w1 = network.structinfo[offs + 3];
            w2 = w1 + network.structinfo[offs + 1] - 1;
            for (i = w1; i <= w2; i++)
            {
                network.weights[i] = v;
            }
            for (i = n1; i <= n2; i++)
            {
                randomizebackwardpass(network, i, v, _params);
            }
            return;
        }
        if (neurontype == -5)
        {

            //
            // Linear activation function: stop
            //
            return;
        }
        if (neurontype > 0)
        {

            //
            // Nonlinear activation function: stop
            //
            return;
        }
        ap.assert(false, "RandomizeBackwardPass: unexpected neuron type");
    }


}
