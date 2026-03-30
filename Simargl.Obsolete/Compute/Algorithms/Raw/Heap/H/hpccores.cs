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

public class hpccores
{
    /*************************************************************************
    This structure stores  temporary  buffers  used  by  gradient  calculation
    functions for neural networks.
    *************************************************************************/
    public class mlpbuffers : apobject
    {
        public int chunksize;
        public int ntotal;
        public int nin;
        public int nout;
        public int wcount;
        public double[] batch4buf;
        public double[] hpcbuf;
        public double[,] xy;
        public double[,] xy2;
        public double[] xyrow;
        public double[] x;
        public double[] y;
        public double[] desiredy;
        public double e;
        public double[] g;
        public double[] tmp0;
        public mlpbuffers()
        {
            init();
        }
        public override void init()
        {
            batch4buf = new double[0];
            hpcbuf = new double[0];
            xy = new double[0, 0];
            xy2 = new double[0, 0];
            xyrow = new double[0];
            x = new double[0];
            y = new double[0];
            desiredy = new double[0];
            g = new double[0];
            tmp0 = new double[0];
        }
        public override apobject make_copy()
        {
            mlpbuffers _result = new mlpbuffers();
            _result.chunksize = chunksize;
            _result.ntotal = ntotal;
            _result.nin = nin;
            _result.nout = nout;
            _result.wcount = wcount;
            _result.batch4buf = (double[])batch4buf.Clone();
            _result.hpcbuf = (double[])hpcbuf.Clone();
            _result.xy = (double[,])xy.Clone();
            _result.xy2 = (double[,])xy2.Clone();
            _result.xyrow = (double[])xyrow.Clone();
            _result.x = (double[])x.Clone();
            _result.y = (double[])y.Clone();
            _result.desiredy = (double[])desiredy.Clone();
            _result.e = e;
            _result.g = (double[])g.Clone();
            _result.tmp0 = (double[])tmp0.Clone();
            return _result;
        }
    };




    /*************************************************************************
    Prepares HPC compuations  of  chunked  gradient with HPCChunkedGradient().
    You  have to call this function  before  calling  HPCChunkedGradient() for
    a new set of weights. You have to call it only once, see example below:

    HOW TO PROCESS DATASET WITH THIS FUNCTION:
        Grad:=0
        HPCPrepareChunkedGradient(Weights, WCount, NTotal, NOut, Buf)
        foreach chunk-of-dataset do
            HPCChunkedGradient(...)
        HPCFinalizeChunkedGradient(Buf, Grad)

    *************************************************************************/
    public static void hpcpreparechunkedgradient(double[] weights,
        int wcount,
        int ntotal,
        int nin,
        int nout,
        mlpbuffers buf,
        xparams _params)
    {
        int i = 0;
        int batch4size = 0;
        int chunksize = 0;

        chunksize = 4;
        batch4size = 3 * chunksize * ntotal + chunksize * (2 * nout + 1);
        if (ap.rows(buf.xy) < chunksize || ap.cols(buf.xy) < nin + nout)
        {
            buf.xy = new double[chunksize, nin + nout];
        }
        if (ap.rows(buf.xy2) < chunksize || ap.cols(buf.xy2) < nin + nout)
        {
            buf.xy2 = new double[chunksize, nin + nout];
        }
        if (ap.len(buf.xyrow) < nin + nout)
        {
            buf.xyrow = new double[nin + nout];
        }
        if (ap.len(buf.x) < nin)
        {
            buf.x = new double[nin];
        }
        if (ap.len(buf.y) < nout)
        {
            buf.y = new double[nout];
        }
        if (ap.len(buf.desiredy) < nout)
        {
            buf.desiredy = new double[nout];
        }
        if (ap.len(buf.batch4buf) < batch4size)
        {
            buf.batch4buf = new double[batch4size];
        }
        if (ap.len(buf.hpcbuf) < wcount)
        {
            buf.hpcbuf = new double[wcount];
        }
        if (ap.len(buf.g) < wcount)
        {
            buf.g = new double[wcount];
        }
        if (!hpcpreparechunkedgradientx(weights, wcount, buf.hpcbuf, _params))
        {
            for (i = 0; i <= wcount - 1; i++)
            {
                buf.hpcbuf[i] = 0.0;
            }
        }
        buf.wcount = wcount;
        buf.ntotal = ntotal;
        buf.nin = nin;
        buf.nout = nout;
        buf.chunksize = chunksize;
    }


    /*************************************************************************
    Finalizes HPC compuations  of  chunked gradient with HPCChunkedGradient().
    You  have to call this function  after  calling  HPCChunkedGradient()  for
    a new set of weights. You have to call it only once, see example below:

    HOW TO PROCESS DATASET WITH THIS FUNCTION:
        Grad:=0
        HPCPrepareChunkedGradient(Weights, WCount, NTotal, NOut, Buf)
        foreach chunk-of-dataset do
            HPCChunkedGradient(...)
        HPCFinalizeChunkedGradient(Buf, Grad)

    *************************************************************************/
    public static void hpcfinalizechunkedgradient(mlpbuffers buf,
        double[] grad,
        xparams _params)
    {
        int i = 0;

        if (!hpcfinalizechunkedgradientx(buf.hpcbuf, buf.wcount, grad, _params))
        {
            for (i = 0; i <= buf.wcount - 1; i++)
            {
                grad[i] = grad[i] + buf.hpcbuf[i];
            }
        }
    }


    /*************************************************************************
    Fast kernel for chunked gradient.

    *************************************************************************/
    public static bool hpcchunkedgradient(double[] weights,
        int[] structinfo,
        double[] columnmeans,
        double[] columnsigmas,
        double[,] xy,
        int cstart,
        int csize,
        double[] batch4buf,
        double[] hpcbuf,
        ref double e,
        bool naturalerrorfunc,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    Fast kernel for chunked processing.

    *************************************************************************/
    public static bool hpcchunkedprocess(double[] weights,
        int[] structinfo,
        double[] columnmeans,
        double[] columnsigmas,
        double[,] xy,
        int cstart,
        int csize,
        double[] batch4buf,
        double[] hpcbuf,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    Stub function.

      -- ALGLIB routine --
         14.06.2013
         Bochkanov Sergey
    *************************************************************************/
    private static bool hpcpreparechunkedgradientx(double[] weights,
        int wcount,
        double[] hpcbuf,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


    /*************************************************************************
    Stub function.

      -- ALGLIB routine --
         14.06.2013
         Bochkanov Sergey
    *************************************************************************/
    private static bool hpcfinalizechunkedgradientx(double[] buf,
        int wcount,
        double[] grad,
        xparams _params)
    {
        bool result = new bool();

        result = false;
        return result;
    }


}
